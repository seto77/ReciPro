using System;
using System.Linq;
using System.Threading.Tasks;

namespace Crystallography
{
    /// <summary>
    /// 二次元検出器クラス
    /// </summary>
    [Serializable]
    public class AreaDetector
    {
        /// <summary>
        /// Detectorのカメラ長(mm)
        /// </summary>
        public double CameraLength { get; set; }

        /// <summary>
        /// 入射線源の波長 (nm)
        /// </summary>
        public double WaveLength { get { return WaveProperty.WaveLength; } set { WaveProperty.WaveLength = value; } }

        /// <summary>
        /// 入射線の種類
        /// </summary>
        public WaveSource WaveSource { get { return WaveProperty.Source; } set { WaveProperty.Source = value; } }

        /// <summary>
        /// 入射線の発散角 (radian)
        /// </summary>
        public double Convergence { get { return WaveProperty.Convergence; } set { WaveProperty.Convergence = value; } }

        /// <summary>
        /// 入射線の単色性
        /// </summary>
        public double Monochromaticity { get { return WaveProperty.Monochromaticity; } set { WaveProperty.Monochromaticity = value; } }

        /// <summary>
        /// 入射線のプロパティ
        /// </summary>
        public WaveProperty WaveProperty { set; get; }

        /// <summary>
        /// Detectorの幅(ピクセル)
        /// </summary>
        public int ImageWidth { get; set; }

        /// <summary>
        /// Detectorの高さ(ピクセル)
        /// </summary>
        public int ImageHeight { get; set; }

        /// <summary>
        /// Detectorの全ピクセル数
        /// </summary>
        public int ImageLength { get { return ImageWidth * ImageHeight; } }

        /// <summary>
        /// Detectorの解像度 (mm/pixel)
        /// </summary>
        public double Resolution { get; set; }

        /// <summary>
        /// Detectorの中心(ダイレクトスポット)位置 (ピクセル単位)
        /// </summary>
        public PointD Center { get; set; }

        /// <summary>
        /// マスクされたピクセル
        /// </summary>
        public bool[] MaskedArea { get; set; }

        /// <summary>
        /// ピクセルが対応する逆空間ベクトル
        /// </summary>
        public Vector3DBase[] ReciprocalVectors { get; set; }

        /// <summary>
        /// ピクセルが対応する逆空間面積
        /// </summary>
        public double[] ReciprocalAreas { get; set; }

        /// <summary>
        /// 逆空間における最大Z値
        /// </summary>
        public double MaxReciproZ { get; set; }

        public AreaDetector(int width, int height, double resolution, PointD center, WaveProperty waveProperty, double cameraLength)
        {
            ImageWidth = width;
            ImageHeight = height;
            Resolution = resolution;
            Center = center;
            WaveProperty = waveProperty;
            CameraLength = cameraLength;
        }

        //public byte[] PolycrystallineImageArray;

        /// <summary>
        /// ピクセル位置X,Yが相当する逆空間ベクトルを返す
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector3DBase convertClientToReciprocalSpace(double x, double y)
        {
            //まずフィルム上の位置を取得
            PointD p = new PointD((x - Center.X) * Resolution, -(y - Center.Y) * Resolution);
            double pLen = p.Length;
            //次に、それを逆空間上の点に変換
            //まず、2θを求める
            double twoTheta = Math.Atan(pLen / CameraLength);
            double sinTheta = Math.Sin(twoTheta / 2);
            double sinThetaSquare = sinTheta * sinTheta;
            double Z = (1 - Math.Cos(twoTheta)) / WaveLength;
            double temp = 1 / pLen * Math.Sqrt((4 * sinThetaSquare / WaveLength / WaveLength) - Z * Z);
            return new Vector3DBase(p.X * temp, p.Y * temp, Z);
        }

        private delegate double[] calcReciporocalAreaDelegate(int startHeight, int endHeight);

        /// <summary>
        /// ピクセルが対応する逆空間ベクトルを計算し、ReciprocalVectorsにセットする
        /// </summary>
        /// <returns></returns>
        private void SetReciporocalVectors()
        {
            ReciprocalVectors = new Vector3DBase[ImageWidth * ImageHeight];
            Parallel.For(0, ImageHeight, y =>
            {
                for (int x = 0; x < ImageWidth; x++)
                    ReciprocalVectors[y * ImageWidth + x] = convertClientToReciprocalSpace(x, y);
            });
        }

        public void SetReciprocalSpace()
        {
            SetReciporocalArea();
            SetReciporocalVectors();
            setMaxReciprocalZ();
        }

        public void setMaxReciprocalZ()
        {
            if (MaskedArea == null || MaskedArea.Length != ImageLength)
            {
                if (ReciprocalAreas != null && ReciprocalAreas.Length > 0)
                    MaxReciproZ = new[] { ReciprocalVectors[0].Z, ReciprocalVectors[ImageWidth - 1].Z, ReciprocalVectors[(ImageHeight - 1) * ImageWidth].Z, ReciprocalVectors[ImageLength - 1].Z }.Max();
                else
                {
                    MaxReciproZ = new[] {
                        convertClientToReciprocalSpace(0, 0).Z,
                        convertClientToReciprocalSpace(ImageWidth-1, 0).Z,
                        convertClientToReciprocalSpace(0, ImageHeight-1).Z,
                        convertClientToReciprocalSpace(ImageWidth-1, ImageHeight-1).Z
                    }.Max();
                }
            }
            else
            {
                double max = double.NegativeInfinity;
                for (int h = 0; h < ImageHeight; h++)
                    for (int w = 0; w < ImageWidth; w++)
                        if (!MaskedArea[h * ImageWidth + w])
                            max = Math.Max(((h - Center.Y) * (h - Center.Y) + (w - Center.X) * (w - Center.X)) * Resolution, max);
                MaxReciproZ = (1 - Math.Cos(Math.Atan(Math.Sqrt(max) / CameraLength))) / WaveLength;
            }
        }

        /// <summary>
        /// ピクセルが対応する逆空間の面積を計算し、ReciprocalAreasにセットする
        /// </summary>
        private void SetReciporocalArea()
        {
            int thread = Environment.ProcessorCount;
            ReciprocalAreas = new double[ImageWidth * ImageHeight];
            Parallel.For(0, thread, i =>
                {
                    Vector3DBase[] beforeBottom = new Vector3DBase[ImageWidth];
                    Vector3DBase top, bottom, right, left, beforeRight;
                    int start = (ImageHeight / thread) * i;
                    int end = Math.Min((ImageHeight / thread) * (i + 1), ImageHeight);
                    for (int x = 0; x < ImageWidth; x++)
                        beforeBottom[x] = convertClientToReciprocalSpace(x, start - 0.5) * 10;
                    for (int y = start; y < end; y++)
                    {
                        beforeRight = convertClientToReciprocalSpace(-0.5, y) * 10;
                        for (int x = 0; x < ImageWidth; x++)
                        {
                            right = convertClientToReciprocalSpace(x + 0.5, y) * 10;
                            left = beforeRight;
                            bottom = convertClientToReciprocalSpace(x, y + 0.5) * 10;
                            top = beforeBottom[x];
                            ReciprocalAreas[y * ImageWidth + x] = Vector3DBase.VectorProduct(right - left, top - bottom).Length;
                            beforeRight = right;
                            beforeBottom[x] = bottom;
                        }
                    }
                }

                );
        }
    }
}
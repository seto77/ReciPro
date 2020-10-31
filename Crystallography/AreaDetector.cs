using System;
using System.Linq;
using System.ServiceModel.Description;
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
        /// ピクセルが対応する逆空間ベクトルと面積
        /// </summary>
        public ((double X, double Y, double Z) Vec, double Area)[] Reciprocal { get; set; }

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
            double px = (x - Center.X) * Resolution, py = -(y - Center.Y) * Resolution, len = Math.Sqrt(px * px + py * py);
            //次に、それを逆空間上の点に変換
            //まず、sinθを求める
            double theta = Math.Atan(len / CameraLength) / 2;
            double sin = Math.Sin(theta);
            //var Z = (1 - Math.Cos(twoTheta)) / WaveLength;
            var Z = 2 * sin * sin / WaveLength;
            //var temp = 1 / pLen * Math.Sqrt((4 * sinTheta * sinTheta / WaveLength / WaveLength) - Z * Z);
            var temp = Z / len * Math.Sqrt(1 / (sin * sin) - 1);
            return new Vector3DBase(px * temp, py * temp, Z);
        }


        public void SetReciprocalSpace()
        {
            SetReciporocalAreaAndVectors();
            setMaxReciprocalZ();
        }

        public void setMaxReciprocalZ()
        {
            if (MaskedArea == null || MaskedArea.Length != ImageLength)
            {
                if (Reciprocal != null && Reciprocal.Length > 0)
                    MaxReciproZ = new[] { Reciprocal[0].Vec.Z, Reciprocal[ImageWidth - 1].Vec.Z, Reciprocal[(ImageHeight - 1) * ImageWidth].Vec.Z, Reciprocal[ImageLength - 1].Vec.Z }.Max();
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
        /// ピクセルが対応する逆空間の面積およびベクトルを計算し、ReciprocalAreasにセットする
        /// </summary>
        private void SetReciporocalAreaAndVectors()
        {
            Reciprocal = new ((double X, double Y, double Z) Vec, double Area)[ImageWidth * ImageHeight];
            Parallel.For(0, ImageHeight, y =>
            {
                for (int x = 0; x < ImageWidth; x++)
                {
                    var right = convertClientToReciprocalSpace(x + 0.5, y);
                    var left = convertClientToReciprocalSpace(x - 0.5, y);
                    var bottom = convertClientToReciprocalSpace(x, y + 0.5);
                    var top = convertClientToReciprocalSpace(x, y - 0.5);

                    Reciprocal[y * ImageWidth + x] = (convertClientToReciprocalSpace(x, y).Tuple, Vector3DBase.VectorProduct(right - left, top - bottom).Length);

                }
            });
        }
    }
}
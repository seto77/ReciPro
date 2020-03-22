using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Threading;


namespace Crystallography
{


    [Serializable()]
    public class Crystallite : ICloneable, IComparable
    {
        public Crystallite()
        {
        }
        public Crystallite(Matrix3D mat, double density, double size)
        {
            Mat = mat;
            double temp = (mat.E11 + mat.E22 + mat.E33 - 1) / 2;
            if (temp > 1) temp = 1;
            if (temp < -1) temp = -1;
            Diagonal = Math.Acos(temp);
            Density = density;
            Size = size;
            index = null;
        }

        public object Clone()
        {
            return (Crystallite)this.MemberwiseClone();
        }
        public int CompareTo(object obj)
        {
            return (Diagonal).CompareTo(((Crystallite)obj).Diagonal);
        }
        public Matrix3D Mat;
        public double Density;
        public double Size;

        [XmlIgnoreAttribute]
        public List<int> index;
        [XmlIgnoreAttribute]
        public List<int> pos;
        [XmlIgnoreAttribute]
        public double Diagonal;
        [XmlIgnoreAttribute]
        public Matrix3D Strain;
    }
    
    [Serializable()]
    public class PolyCrystal
    {
        public Matrix3D Strain = new Matrix3D(0, 0, 0, 0, 0, 0, 0, 0, 0);
        public Matrix3D Stress = new Matrix3D(0, 0, 0, 0, 0, 0, 0, 0, 0);
        public double HillCoefficient = 0;

        public Crystal BaseCrystal;
        public Matrix3D BaseRotation = new Matrix3D();
        public List<Matrix3D> BaseRotationList = new List<Matrix3D>();
       
        public Crystallite[] Crysatallites;
        public double CameraLength;
        public double WaveLength;
        public WaveSource WaveSource;
        
        public PolyCrystal(Crystal baseCrystal,Crystallite[] eachCrystals, double cameraLength, double waveLength, WaveSource waveSource, Matrix3D strain, Matrix3D stress, double hill)
        {
            BaseCrystal = baseCrystal;
            CameraLength = cameraLength;
            WaveLength = waveLength;
            Crysatallites = eachCrystals;
            WaveSource = waveSource;

            if (strain != null)
                Strain = strain;
            if (stress != null)
                Stress = stress;
            HillCoefficient = hill;
        }

        public void ResetBaseRotation()
        {
            BaseRotationList.Clear();
        }

        public void SetGVector(int width, int height, PointD center, double resolution)
        {
            double left = -center.X * resolution;
            double right = (width - center.X) * resolution;
            double top = -center.Y * resolution;
            double bottom = (height - center.Y) * resolution;
            double length = Statistics.Max(
                Math.Sqrt(left * left + top * top), Math.Sqrt(left * left + bottom * bottom),
                Math.Sqrt(right * right + top * top), Math.Sqrt(right * right + bottom * bottom)
                );

            BaseCrystal.SetVectorOfG(WaveLength / 2 / Math.Sin(Math.Atan(length / CameraLength) / 2.0), WaveSource);
            for (int i = 0; i < BaseCrystal.VectorOfG.Count; i++)
                if (BaseCrystal.VectorOfG[i].Extinction.Length != 0)
                    BaseCrystal.VectorOfG.RemoveAt(i--);
        }

        //Vector3DBase[] rVec;
        double[] rVecX;
        double[] rVecY;
        double[] rVecZ;
        double[] rVecArea;
        //List<Vector3DBase> gVector = new List<Vector3DBase>();
        List<double> gVectorX = new List<double>();
        List<double> gVectorY = new List<double>();
        List<double> gVectorZ = new List<double>();
        List<double> intensity=new List<double>();
        double maxReciproZ;

        private Vector3DBase convertClientToReciprocalSpace(double x, double y, int imageWidth, int imageHeight, double resolution, PointD center)
        {
            //まずフィルム上の位置を取得
            PointD p = new PointD((x - center.X) * resolution, -(y - center.Y) * resolution);
            double pLen = p.Length();
            //次に、それを逆空間上の点に変換
            //まず、2θを求める
            double twoTheta = Math.Atan(pLen / CameraLength);
            double sinTheta = Math.Sin(twoTheta / 2);
            double sinThetaSquare = sinTheta * sinTheta;
            double Z = (1 - Math.Cos(twoTheta)) / WaveLength;
            double temp = 1 / pLen * Math.Sqrt((4 * sinThetaSquare / WaveLength / WaveLength) - Z * Z);
            return new Vector3DBase(p.X * temp, p.Y * temp, Z);
        }


        delegate double[] calcReciporocalAreaDelegate(int startHeight, int endHeight);

        /// <summary>
        /// 各ピクセルを逆空間座標に変換する
        /// </summary>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <param name="resolution"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public Vector3DBase[] calcReciporocalPosition(int imageWidth, int imageHeight, double resolution, PointD center)
        {
            Vector3DBase[] rVec = new Vector3DBase[imageWidth * imageHeight];
            for (int y = 0; y < imageHeight; y++)
                Parallel.For(0, imageWidth, x =>
                {
                    rVec[y * imageWidth + x] = convertClientToReciprocalSpace(x, y, imageWidth, imageHeight, resolution, center);
                });
            return rVec;
        }


        /// <summary>
        /// 各ピクセルの逆空間での面積を計算する
        /// </summary>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <param name="resolution"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public double[] calcReciporocalArea(int imageWidth, int imageHeight, double resolution, PointD center)
        {
            int thread = 8;

            int[] divHeight = new int[thread + 1];
            for (int i = 0; i < thread; i++)
                divHeight[i] = imageHeight / thread * i;
            divHeight[thread] = imageHeight;

            IAsyncResult[] ar = new IAsyncResult[thread];

            //各ピクセルの逆空間での面積を計算するラムダ式の作成
            calcReciporocalAreaDelegate[] calcArea = new calcReciporocalAreaDelegate[thread];
            for (int j = 0; j < thread; j++)
                calcArea[j] = new calcReciporocalAreaDelegate((startHeight, endHeight) =>
                {
                    Vector3DBase[] beforeBottom = new Vector3DBase[imageWidth];
                    Vector3DBase top, bottom, right, left, beforeRight;
                    double[] temp = new double[imageWidth * (endHeight - startHeight)];
                    int count = 0;
                    for (int n = 0; n < imageWidth; n++)
                        beforeBottom[n] = convertClientToReciprocalSpace(n, startHeight - 0.5, imageWidth, imageHeight, resolution, center);
                    for (int m = startHeight; m < endHeight; m++)
                    {
                        beforeRight = convertClientToReciprocalSpace(-0.5, m, imageWidth, imageHeight, resolution, center);
                        for (int n = 0; n < imageWidth; n++)
                        {
                            right = convertClientToReciprocalSpace(n + 0.5, m, imageWidth, imageHeight, resolution, center);
                            left = beforeRight;
                            bottom = convertClientToReciprocalSpace(n, m + 0.5, imageWidth, imageHeight, resolution, center);
                            top = beforeBottom[n];
                            temp[count++] = Vector3DBase.VectorProduct(right - left, top - bottom).Length();
                            beforeRight = right;
                            beforeBottom[n] = bottom;
                        }
                    }
                    return temp;
                });

            //各ピクセルの逆空間での面積を計算するラムダ式の実行
            double[] rVecArea = new double[imageWidth * imageHeight];
            for (int i = 0; i < thread; i++)
                ar[i] = calcArea[i].BeginInvoke(divHeight[i], divHeight[i + 1], null, null);
            for (int i = 0; i < thread; i++)
            {
                double[] tempArea = calcArea[i].EndInvoke(ar[i]);
                int count = 0;
                for (int m = divHeight[i]; m < divHeight[i + 1]; m++)
                    for (int n = 0; n < imageWidth; n++)
                        rVecArea[n + m * imageWidth] = tempArea[count++];
            }
            return rVecArea;
        }

        private PointD beforeCenter = new PointD(double.NaN, double.NaN);

        public double[] GetSimulatedPattern(Matrix3D rotation,
             int imageWidth, int imageHeight, double resolution, PointD center,
             double convergentAngle, double monochromaticity, bool resetIndex, bool renewRvector, bool renewGvector)
        {
            return GetSimulatedPattern(rotation, Crysatallites, imageWidth, imageHeight, resolution, center, null, null,
            convergentAngle, monochromaticity, resetIndex, renewRvector, renewGvector);
        }


        public double[] GetSimulatedPattern(Matrix3D rotation, Crystallite[] crystals,
            int imageWidth, int imageHeight, double resolution, PointD center, Vector3DBase[] reciprocalVec, double[] reciprocalArea,
            double convergentAngle, double monochromaticity, bool resetIndex, bool renewRvector, bool renewGvector)
        {
            BaseRotation = rotation;
            if (!BaseRotationList.Contains(rotation))
            {
                BaseRotationList.Add(rotation);
                resetIndex = true;
            }

            //まず各種定数の設定
            if (reciprocalVec == null || reciprocalVec.Length != imageWidth * imageHeight)
            {
               Vector3DBase[] rVec = calcReciporocalPosition(imageWidth, imageHeight, resolution, center);//各ピクセルを逆空間座標に変換する
               rVecX = new double[rVec.Length];
               rVecY = new double[rVec.Length];
               rVecZ = new double[rVec.Length];
               for (int i = 0; i < rVec.Length; i++)
               {
                   rVecX[i] = rVec[i].X;
                   rVecY[i] = rVec[i].Y;
                   rVecZ[i] = rVec[i].Z;
               }

                rVecArea = calcReciporocalArea(imageWidth, imageHeight, resolution, center);//各ピクセルの逆空間上での面積を計算する

            }
            else
            {
                Vector3DBase[] rVec = reciprocalVec;
                rVecX = new double[rVec.Length];
                rVecY = new double[rVec.Length];
                rVecZ = new double[rVec.Length];
                for (int i = 0; i < rVec.Length; i++)
                {
                    rVecX[i] = rVec[i].X;
                    rVecY[i] = rVec[i].Y;
                    rVecZ[i] = rVec[i].Z;
                }
                rVecArea = reciprocalArea;
            }
            //画面の四隅のエワルド球で最もZ値が大きいものを算出
            maxReciproZ = Math.Max(Math.Max(rVecZ[0], rVecZ[imageWidth - 1]), Math.Max(rVecZ[(imageHeight - 1) * imageWidth], rVecZ[rVecZ.Length - 1]));
            if (renewGvector || gVectorX.Count != BaseCrystal.VectorOfG.Count)
            {
                gVectorX = new List<double>();
                gVectorY = new List<double>();
                gVectorZ = new List<double>();
                for (int k = 0; k < BaseCrystal.VectorOfG.Count; k++)
                {
                    gVectorX.Add(BaseCrystal.VectorOfG[k].X);
                    gVectorY.Add(BaseCrystal.VectorOfG[k].Y);
                    gVectorZ.Add(BaseCrystal.VectorOfG[k].Z);
                    double cos2theta = (1 - BaseCrystal.VectorOfG[k].Length2() * WaveLength * WaveLength / 2.0);
                    intensity.Add(BaseCrystal.VectorOfG[k].RelativeIntensity * (1 + cos2theta * cos2theta));//トムソン散乱の補正
                }
            }
            if (resetIndex)
                Parallel.For(0, Crysatallites.Length, i =>
                {
                    Crysatallites[i].index = null;
                });

            double[] pixelsSum = getSimulatedPattern(crystals, imageWidth, imageHeight, center.X, center.Y, resolution, convergentAngle, monochromaticity);

            Parallel.For(0, pixelsSum.Length, i =>
            {
                if (pixelsSum[i] != 0)
                    pixelsSum[i] *= rVecArea[i];
            });

            return pixelsSum;
        }

        private double[] getSimulatedPattern(Crystallite[] crystals, int imageWidth, int imageHeight, double centerX, double centerY, double resolution, double convergence, double monochromaticity)
        {
            return getSimulatedPattern(crystals, imageWidth, imageHeight, centerX, centerY, resolution, convergence, monochromaticity,16);
        }
        object lockThis=new object();
        private double[] getSimulatedPattern(Crystallite[] crystals, int imageWidth, int imageHeight, double centerX, double centerY, double resolution, double convergence, double monochromaticity, int threadNumber)
        {
            Elasticity e = new Elasticity(BaseCrystal.ElasticStiffness, Elasticity.Mode.Stiffness);
            bool strainFree = Stress.E11 == 0 && Stress.E21 == 0 && Stress.E31 == 0 && Stress.E21 == 0 && Stress.E22 == 0 && Stress.E23 == 0 && Stress.E31 == 0 && Stress.E32 == 0 && Stress.E33 == 0 &&
               Strain.E11 == 0 && Strain.E21 == 0 && Strain.E31 == 0 && Strain.E21 == 0 && Strain.E22 == 0 && Strain.E23 == 0 && Strain.E31 == 0 && Strain.E32 == 0 && Strain.E33 == 0;

            double convergenceHK = Math.Sin(convergence / 2) / WaveLength;//逆空間での、収束による逆格子点のにじみ(X線軸垂直な方向)
            double convergenceHK2 = convergenceHK * convergenceHK;
            //double convergenceZ = (1 - Math.Cos(convergence / 2)) / WaveLength;//逆空間での、収束による逆格子点のにじみ(X線軸平行な方向) //この成分は無視することにしよう

            double ewaldR = 1 / WaveLength;
            double ewaldR2 = ewaldR * ewaldR;
            double maxVlength2 = 2 * maxReciproZ * ewaldR;
            double monochromaticity2 = monochromaticity * monochromaticity;
            double maxMonochroHK2 = maxVlength2 * monochromaticity2;   //Z
            double size = crystals[0].Size;
            double reciprocalPointSize2 = 1 / size / size / 4;
            double vol = size * size * size;
            double maxExcitationError = Math.Sqrt(reciprocalPointSize2 + maxMonochroHK2 + convergenceHK2);

            int[] directions = new int[] { 0, -1, 1, imageWidth, -imageWidth };

            Func<int, int, double[]>[] thread = new Func<int, int, double[]>[threadNumber];
            for (int q = 0; q < threadNumber; q++)
                thread[q] = new Func<int, int, double[]>((start, end) =>
                    {
                        double[] pixels = new double[imageHeight * imageWidth];
                        Matrix3D strainInv = new Matrix3D();
                        Matrix3D rotation;
                        for (int i = start; i < end; i++)
                        {    //crystal[i].index が設定されていないか、設定されていて1以上の時。 設定されていて、0の時はキャンセル。
                            if (crystals[i].index == null || crystals[i].index.Count > 0)
                            {
                                if (strainFree)
                                    rotation = BaseRotation * crystals[i].Mat;
                                else
                                {
                                    crystals[i].Strain = e.GetStrainByHill(BaseCrystal.Symmetry, crystals[i].Mat, Stress, Strain, HillCoefficient);
                                    strainInv = (crystals[i].Strain + new Matrix3D()).Inverse();
                                    rotation = BaseRotation * strainInv * crystals[i].Mat;
                                }
                                double x = 0, y = 0, z = 0;
                                if (crystals[i].index == null)//crystal[i].index が設定されていないときは、エワルド球に近い(回折条件に引っ掛かる)逆格子ベクトルを探索
                                {
                                    crystals[i].index = new List<int>();
                                    for (int j = 0; j < BaseRotationList.Count; j++)
                                    {
                                        Matrix3D rot;
                                        if (strainFree)
                                            rot = BaseRotationList.Count == 1 ? rotation : BaseRotationList[j] * crystals[i].Mat;
                                        else
                                            rot = BaseRotationList.Count == 1 ? rotation : BaseRotationList[j] * strainInv * crystals[i].Mat;

                                        for (int n = 0; n < gVectorX.Count; n++)
                                            if (BaseRotationList.Count == 1 || !crystals[i].index.Contains(n))
                                            {
                                                z = n % 2 == 0 ? rot.E31 * gVectorX[n] + rot.E32 * gVectorY[n] + rot.E33 * gVectorZ[n] : -z;
                                                if (z > -maxExcitationError * 2 && maxReciproZ + maxExcitationError * 2 > z)
                                                {
                                                    x = rot.E11 * gVectorX[n] + rot.E12 * gVectorY[n] + rot.E13 * gVectorZ[n];
                                                    y = rot.E21 * gVectorX[n] + rot.E22 * gVectorY[n] + rot.E23 * gVectorZ[n];
                                                    double ptX = CameraLength / resolution / (ewaldR - z) * x + centerX + 0.5;
                                                    double ptY = -CameraLength / resolution / (ewaldR - z) * y + centerY + 0.5;
                                                    if (ptY < imageHeight && ptY > -1 && ptX < imageWidth && ptX > -1)
                                                    {
                                                        double xyLength2 = x * x + y * y;
                                                        double vLength2 = xyLength2 + z * z;
                                                        double distanceToEwaldCenter2 = vLength2 + ewaldR2 - 2 * z * ewaldR;
                                                        double distanceToEwaldCenter = Math.Sqrt(distanceToEwaldCenter2);
                                                        double dev2 = ewaldR2 + distanceToEwaldCenter2 - 2 * ewaldR * distanceToEwaldCenter;
                                                        if (dev2 / 4 < reciprocalPointSize2 + convergenceHK2 + maxMonochroHK2)
                                                        {
                                                            double sin2Theta2 = xyLength2 / distanceToEwaldCenter2;
                                                            double cos2Theta = (ewaldR - z) / distanceToEwaldCenter;
                                                            double monochroHK2 = vLength2 * monochromaticity2;
                                                            if (dev2 / 4 < reciprocalPointSize2 + convergenceHK2 * sin2Theta2 +
                                                                monochroHK2 * (distanceToEwaldCenter - ewaldR * cos2Theta) * (distanceToEwaldCenter - ewaldR * cos2Theta) / vLength2)
                                                                crystals[i].index.Add(n);
                                                        }
                                                    }
                                                }
                                            }
                                    }
                                }
                                double hk2square = reciprocalPointSize2 + convergenceHK2; // (-y,x,0)方向の半値幅
                                foreach (int n in crystals[i].index)
                                {
                                    x = rotation.E11 * gVectorX[n] + rotation.E12 * gVectorY[n] + rotation.E13 * gVectorZ[n];
                                    y = rotation.E21 * gVectorX[n] + rotation.E22 * gVectorY[n] + rotation.E23 * gVectorZ[n];
                                    z = rotation.E31 * gVectorX[n] + rotation.E32 * gVectorY[n] + rotation.E33 * gVectorZ[n];
                                    double xyLength2 = x * x + y * y;
                                    double vLength2 = xyLength2 + z * z;
                                    double monochroHK2 = vLength2 * monochromaticity2;

                                    double hk1square = hk2square + monochroHK2 * vLength2 / xyLength2;//(x,y,0)方向の半値幅
                                    double hk3square = reciprocalPointSize2 + monochroHK2 * z * z / xyLength2;// (0,0,z)方向の半値幅

                                    double baseX = x / Math.Sqrt(xyLength2);
                                    double baseY = y / Math.Sqrt(xyLength2);

                                    double intensityTemp = 1 / Math.Sqrt(hk1square * 1000 * hk2square * 1000 * hk3square * 1000) * vol / 1000 * intensity[n];// *crystals[i].Density; 

                                    List<int> comp = new List<int>();//計算済みピクセル
                                    List<int> rim = new List<int>(new int[]{
                                         (int)(-(CameraLength / resolution / (ewaldR - z)) * y + centerY + 0.5) * imageWidth + (int)((CameraLength / resolution / (ewaldR - z)) * x + centerX + 0.5) });//外縁ピクセル
                                    for (int j= 0 ; j< rim.Count; j++)//rim.RemoveAt(0))//現在のピクセル位置から、周辺強度を計算していって、半値幅の2倍以上になったら終了
                                        for (int k = j==0 ? 0 : 1; k < directions.Length; k++)
                                        {
                                            int pos = rim[j] + directions[k];
                                            if (!comp.Contains(pos) && pos > -1 && pos < pixels.Length)
                                            {
                                                comp.Add(pos);
                                                double devX = x - rVecX[pos], devY = y - rVecY[pos], devZ = z - rVecZ[pos];
                                                double hk1Dev = baseX * devX + baseY * devY, hk2Dev = -baseY * devX + baseX * devY;
                                                double dev2 = hk1Dev * hk1Dev / hk1square + hk2Dev * hk2Dev / hk2square + devZ * devZ / hk3square;
                                                if (dev2 < 5.0)
                                                {
                                                    double temp = intensityTemp / (1.0 + dev2);
                                                    pixels[pos] += temp;
                                                    if (k != 0)
                                                        rim.Add(pos);
                                                }
                                            }
                                        }
                                }
                            }
                        }
                        return pixels;
                    });


            double[] pixelSum = new double[imageHeight * imageWidth];
            IAsyncResult[] ar = new IAsyncResult[threadNumber];
            for (int t = 0; t < threadNumber; t++)
                ar[t] = thread[t].BeginInvoke(t * crystals.Length / threadNumber, Math.Min(crystals.Length, (t + 1) * crystals.Length / threadNumber), null, null);
            for (int t = 0; t < threadNumber; t++)
            {
                double[] temp = thread[t].EndInvoke(ar[t]);
                for (int i = 0; i < pixelSum.Length; i++)
                    if (temp[i] != 0)
                        pixelSum[i] += temp[i];
            }


            return pixelSum;
        }

        public static object lockObject = new object(); 
        static Random rn = new Random();
        public static Crystallite[] GenerateCrystals(Matrix3D seed, int number, double directionalDensity, double size, double sizeSigma)
        {
            Crystallite[] crystals = new Crystallite[number];
            ParallelOptions p = new ParallelOptions();
            p.MaxDegreeOfParallelism = 16;
            Parallel.For(0, number, p, i =>
            {
                double r1, r2, r3;
                lock (lockObject)
                {
                    r1 = rn.NextDouble();
                    r2 = rn.NextDouble();
                    r3 = rn.NextDouble();
                }
                double rotZ = 2 * r1 - 1;
                double rotTheta = r2 * 2 * Math.PI;
                double rotX = Math.Sqrt(1 - rotZ * rotZ) * Math.Sin(rotTheta);
                double rotY = Math.Sqrt(1 - rotZ * rotZ) * Math.Cos(rotTheta);
                Vector3DBase vec = new Vector3DBase(rotX, rotY, rotZ);
                double angle = r3 * directionalDensity + directionalDensity * 0.1; //Statistics.NormalDistribution(0, directionalDensity);
                Matrix3D rot = Matrix3D.Rot(vec, angle);
                crystals[i] = new Crystallite(rot * seed, 1, size);
            });
            return crystals;
        }

        public static Crystallite[] GenerateCrystals(int number, double size, double sizeSigma)
        {
            Crystallite[] crystals = new Crystallite[number];
            for (int i = 0; i < number; i++)
            {
                Matrix3D mat = Matrix3D.GenerateRamdomRotationMatrix();
                crystals[i] = new Crystallite(mat, 1, Statistics.LogNormalDistribution(size, sizeSigma));
            }
            return crystals;
        }






    }
}

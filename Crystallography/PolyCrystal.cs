using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using System.Diagnostics;
using System.Security.RightsManagement;

namespace Crystallography
{
    [Serializable()]
    public class Crystallite
    {
        #region フィールド、プロパティ

        public static Matrix3D TiltMatrix = Matrix3D.Rot(new Vector3DBase(0.2, 0.1, 0), Math.PI / 180 * 5);//適当に傾ける行列を作成

        [XmlIgnoreAttribute]
        public Crystal BaseCrystal = null;

        /// <summary>
        /// 角度分解能 Degree単位
        /// </summary>
        [XmlIgnoreAttribute]
        public double AngleResolution { get { return BaseCrystal.AngleResolution; } set { BaseCrystal.AngleResolution = value; } }

        /// <summary>
        /// 一つの結晶子が受け持つ角度を分割する数
        /// </summary>
        [XmlIgnoreAttribute]
        public int SubDivision { get { return BaseCrystal.SubDivision; } set { BaseCrystal.SubDivision = value; } }

        /// <summary>
        /// 結晶子のサイズ
        /// </summary>
        [XmlIgnoreAttribute]
        public double GrainSize { get { return BaseCrystal.GrainSize; } set { BaseCrystal.GrainSize = value; } }

        /// <summary>
        /// 多結晶体全体のRotation　
        /// </summary>
        public Matrix3D WholeRotation = Matrix3D.IdentityMatrix;

        public int SquareDivision { get { return (int)(90.0 / BaseCrystal.AngleResolution); } }
        public int RotationDivision { get { return (int)(360.0 / BaseCrystal.AngleResolution); } }

        /// <summary>
        /// 全結晶子の数
        /// </summary>
        public int TotalCrystalline { get { return 6 * SquareDivision * SquareDivision * RotationDivision; } }

        public int ImageWidh { get; set; }
        public int ImageHeight { get; set; }

        //[n][] n番目の結晶方位について、エワルド球に近い逆格子ベクトルの番号
        public ushort[][] ValidIndex;

        //[n][m][] n番目の結晶方位の、m番目の逆格子ベクトルが、考慮すべきSubRotation番号
        public ushort[][][] ValidSubRotNum;

        /// <summary>
        /// PixelIndex[n][]:ｎ番目の角度範囲の結晶子が寄与する画素の番号
        /// PixelIntensityと同期して使う
        /// </summary>
        public (int Index,double Intensity)[][] Pixel;

        /// <summary>
        /// PixelIndices[n][]:ｎ番目の角度範囲の結晶子が寄与する画素の強度
        /// PixelIndexと同期して使う
        /// </summary>
        //public double[][] PixelIntensity;

        /// <summary>
        /// SpotsPosition[n][]: n番目の角度の結晶が寄与する回折スポットの位置と強度 x,y: スポット位置, z:強度
        /// </summary>
        //public PointD[][] SpotsPosition;

        /// <summary>
        /// ｎ番目の角度範囲の結晶子が受け持つ立体角
        /// </summary>
        public double[] SolidAngle;

        /// <summary>
        /// ｎ番目の角度範囲の結晶子の濃度
        /// </summary>
        public double[] Density;

        //[n][angle][] n番目の逆格子ベクトルが ある回転角(angle)でエワルド球に一致したとき、そのピクセルを原点とした、強度順で並び替えたピクセル位置
        public int[][][] SpotShapesSortedIndex;

        //SpotShapesSortedIndexにおける一周(360°)の分割数
        public int SpotShapesAngleDivision = 360;

        public double[] DeviationThreshold;

        /// <summary>
        /// 結晶子の回転行列
        /// </summary>
        public Matrix3D[] Rotations = null;

        private Matrix3D[] Rotations1 = null;
        private Matrix3D[] Rotations2 = null;

        /// <summary>
        /// 結晶子の回転行列の前半 (z軸の方向に対応)
        /// </summary>
        private Matrix3D[] SubRotations1;

        /// <summary>
        /// 結晶の回転行列の後半 (Z軸の回転に対応)
        /// </summary>
        private Matrix3D[] SubRotations2;

        /// <summary>
        /// 全逆格子ベクトルの数
        /// </summary>
        public int G_VectorNumber { get { return G_Vector != null ? G_Vector.Length : -1; } }

        /// <summary>
        /// 逆格子ベクトル
        /// </summary>
        public Vector3DBase[] G_Vector = null;

        /// <summary>
        /// 逆格子ベクトルの半値幅
        /// </summary>
        public double[] G_Hk1, G_Hk2, G_Hk3;

        public double[] G_Intensity2;

        /// <summary>
        /// 逆格子ベクトルの強度
        /// </summary>
        public double[] G_Intensity = null;

        #endregion フィールド、プロパティ

        public event ProgressChangedEventHandler ProgressChanged;

        
        Stopwatch stopwatch = new Stopwatch();
        private object lockObj = new object();
        public Crystallite()
        {
         
        }


        public Crystallite(Crystal baseCrystal)
            : this()
        {
            BaseCrystal = baseCrystal;
            setCrystallineRotation();
            setCrystallineSolidAngle();
            Density = new double[TotalCrystalline];

            for (int i = 0; i < TotalCrystalline; i++)
                Density[i] = 1;
            if (vec == null || vec[0].Length != SquareDivision)
                initializeVec();
        }

        public Crystallite(Crystal baseCrystal, double[] density)
            : this()
        {
            BaseCrystal = baseCrystal;
            setCrystallineRotation();
            setCrystallineSolidAngle();
            if (TotalCrystalline == density.Length)
                Density = density;
            else
            {
                Density = new double[TotalCrystalline];
                for (int i = 0; i < TotalCrystalline; i++)
                    Density[i] = 1;
            }
            if (vec == null || vec[0].Length != SquareDivision)
                initializeVec();
        }

        public void SetGVector(AreaDetector detector, bool applyTiltMatrix = true, bool removeZeroIntensity = true)
        {
            if (BaseCrystal != null)
                SetGVector(BaseCrystal, detector, applyTiltMatrix, removeZeroIntensity);
        }

        /// <summary>
        /// 結晶の逆格子ベクトルを初期化
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="center"></param>
        /// <param name="resolution"></param>
        /// <param name="maskedArea"></param>
        public void SetGVector(Crystal crystal, AreaDetector detector, bool applyTiltMatrix = true, bool removeZeroIntensity = true)
        {
            ImageWidh = detector.ImageWidth;
            ImageHeight = detector.ImageHeight;

            double length = 0;
            if (detector.MaskedArea == null || detector.MaskedArea.Length != detector.ImageWidth * detector.ImageHeight)
            { //画面の四隅のエワルド球で最もZ値が大きいものを算出
                double left = -detector.Center.X * detector.Resolution;
                double right = (detector.ImageWidth - detector.Center.X) * detector.Resolution;
                double top = -detector.Center.Y * detector.Resolution;
                double bottom = (detector.ImageHeight - detector.Center.Y) * detector.Resolution;
                length = Statistics.Max(
                    Math.Sqrt(left * left + top * top), Math.Sqrt(left * left + bottom * bottom),
                    Math.Sqrt(right * right + top * top), Math.Sqrt(right * right + bottom * bottom)
                    );
            }
            else
            { //マスクされていない中で最もZ値が大きいものを算出
                double max = double.NegativeInfinity;
                for (int h = 0; h < detector.ImageHeight; h++)
                    for (int w = 0; w < detector.ImageWidth; w++)
                        if (!detector.MaskedArea[h * detector.ImageWidth + w])
                            max = Math.Max(Math.Sqrt((h - detector.Center.Y) * (h - detector.Center.Y) + (w - detector.Center.X) * (w - detector.Center.X)) * detector.Resolution, max);
                length = max;
            }
            crystal.SetVectorOfG(detector.WaveLength / 2 / Math.Sin(Math.Atan(length / detector.CameraLength) / 2.0), detector.WaveSource);
            List<Vector3D> temp = new List<Vector3D>();
            for (int i = 0; i < crystal.VectorOfG.Count; i++)
                if (removeZeroIntensity)
                {
                    if (crystal.VectorOfG[i].Extinction.Length == 0)
                        temp.Add(crystal.VectorOfG[i]);
                }
                else
                {
                    var s = crystal.VectorOfG[i].Extinction;
                    if (s.Length == 0 || (s[0] != "I" && s[0] != "F" && s[0] != "A" && s[0] != "B" && s[0] != "C" && s[0] != "R"))
                        temp.Add(crystal.VectorOfG[i]);
                }

            crystal.VectorOfG = temp;

            G_Vector = new Vector3DBase[crystal.VectorOfG.Count];
            G_Intensity = new double[crystal.VectorOfG.Count];
            G_Hk1 = new double[crystal.VectorOfG.Count];
            G_Hk2 = new double[crystal.VectorOfG.Count];
            G_Hk3 = new double[crystal.VectorOfG.Count];
            G_Intensity2 = new double[crystal.VectorOfG.Count];
            for (int k = 0; k < crystal.VectorOfG.Count; k++)
            {
                if (applyTiltMatrix)
                    G_Vector[k] = TiltMatrix * crystal.VectorOfG[k];//傾き行列をかけて置く
                else
                    G_Vector[k] = crystal.VectorOfG[k];

                double cos2theta = (1 - crystal.VectorOfG[k].Length2* detector.WaveLength * detector.WaveLength / 2.0);//cos2theta = 1-2*(sinTheta)^2 = 1 - 2* ( wavelentgh^2/d^2 / 4)
                G_Intensity[k] = crystal.VectorOfG[k].RelativeIntensity * (1 + cos2theta * cos2theta);//トムソン散乱の補正

                double l2 = G_Vector[k].Length2;

                double ewaldR = 1 / detector.WaveLength, ewaldR2 = ewaldR * ewaldR;
                double monochromaticity2 = detector.Monochromaticity * detector.Monochromaticity;
                double reciprocalPointSize = 1 / BaseCrystal.GrainSize / 2, reciprocalPointSize2 = reciprocalPointSize * reciprocalPointSize;
                double vol = BaseCrystal.GrainSize * BaseCrystal.GrainSize * BaseCrystal.GrainSize;

                //半値幅を保存
                double convergenceHK = Math.Sin(detector.Convergence / 2) / detector.WaveLength;//逆空間での、収束による逆格子点のにじみ(X線軸垂直な方向)
                G_Hk2[k] = reciprocalPointSize2 + convergenceHK * convergenceHK; // (-y,x,0)方向の半値幅
                G_Hk1[k] = G_Hk2[k] + monochromaticity2 * l2 * (1 - l2 / 4 / ewaldR2);//(x,y,0)方向の半値幅
                G_Hk3[k] = reciprocalPointSize2 + monochromaticity2 * l2 * l2 / 4 / ewaldR2;// (0,0,z)方向の半値幅

                G_Intensity2[k] = 1 / Math.Sqrt(G_Hk1[k] * 1000 * G_Hk2[k] * 1000 * G_Hk3[k] * 1000) * vol * G_Intensity[k];
            }

            //最大強度を規格化
            double maxInt = G_Intensity2.Max() / 1000;
            DeviationThreshold = new double[G_Vector.Length];
            for (int i = 0; i < G_Intensity2.Length; i++)
            {
                G_Intensity2[i] /= maxInt;
                DeviationThreshold[i] = Math.Max(3, Math.Log10(G_Intensity2[i]) * 3);//最小で3, 最大で9
            }

            setSpotShapes(detector);
        }

        /// <summary>
        /// 各結晶子が受け持つ
        /// </summary>
        /// <param name="detector"></param>
        public void setSpotShapes(AreaDetector detector)
        {
            SpotShapesSortedIndex = new int[G_Vector.Length][][];

            Parallel.For(0, G_Vector.Length, gNum =>
            {
                SpotShapesSortedIndex[gNum] = new int[SpotShapesAngleDivision][];

                //エワルド球面に一致したときの、ダイレクトスポットからこの逆格子点までの実距離
                double length = Math.Tan(detector.WaveLength * G_Vector[gNum].Length) * detector.CameraLength;
                double xyLength = Math.Sin(detector.WaveLength * G_Vector[gNum].Length) / detector.WaveLength, xyLength2 = xyLength * xyLength;
                double z = 1 / detector.WaveLength * (1 - Math.Cos(detector.WaveLength * G_Vector[gNum].Length));

                //角度分割
                for (int angle = 0; angle < SpotShapesAngleDivision; angle++)
                {
                    double originX = length / detector.Resolution * Math.Cos(angle / 180.0 * Math.PI) + detector.Center.X;
                    double originY = -length / detector.Resolution * Math.Sin(angle / 180.0 * Math.PI) + detector.Center.Y;

                    double x = xyLength * Math.Cos(angle / 180.0 * Math.PI);
                    double y = xyLength * Math.Sin(angle / 180.0 * Math.PI);

                    var pixels = new Dictionary<int, double>();

                    for (int devX = -4; devX <= 4; devX++)
                        for (int devY = -4; devY <= 4; devY++)
                        {
                            Vector3DBase v = detector.convertClientToReciprocalSpace(devX + originX, devY + originY);
                            double temp1 = x * v.X + y * v.Y - xyLength2;
                            double temp2 = x * v.Y - y * v.X;
                            double temp3 = z - v.Z;
                            double dev2 = temp1 * temp1 / xyLength2 / G_Hk1[gNum] + temp2 * temp2 / xyLength2 / G_Hk2[gNum] + temp3 * temp3 / G_Hk3[gNum];
                            if (dev2 < DeviationThreshold[gNum] * 1.5)
                                pixels.Add(devY * detector.ImageHeight + devX, -dev2);
                        }
                    var vs2 = pixels.OrderByDescending(e => e.Value);
                    List<int> index = new List<int>();
                    foreach (var v in vs2)
                        index.Add(v.Key);
                    SpotShapesSortedIndex[gNum][angle] = index.ToArray();
                }
            }
            );
            /*
            //同じ配列が大量に出てくるので、メモリ節約のために、まとめる

            for (int i = 0; i < G_Vector.Length * SpotShapesAngleDivision - 1; i++)
            {
                int gNum1 = i / SpotShapesAngleDivision, angle1 = i % SpotShapesAngleDivision;

                for (int j = i + 1; j < G_Vector.Length * SpotShapesAngleDivision; j++)
                {
                    int gNum2 = j / SpotShapesAngleDivision, angle2 = j % SpotShapesAngleDivision;
                    if (SpotShapesSortedIndex[gNum1][angle1].Length == SpotShapesSortedIndex[gNum2][angle2].Length)
                    {
                        bool flag = true;
                        for (int k = 0; k < SpotShapesSortedIndex[gNum1][angle1].Length && flag; k++)
                            if (SpotShapesSortedIndex[gNum1][angle1][k] != SpotShapesSortedIndex[gNum2][angle2][k])
                                flag = false;
                        if (flag)
                        {
                            SpotShapesSortedIndex[gNum1][angle1] = SpotShapesSortedIndex[gNum2][angle2];
                            break;
                        }
                    }
                }
            }
            GC.Collect();
            */
        }

        #region お蔵入り中

        public static object lockObject = new object();
        private static Random rn = new Random();

        /// <summary>
        ///
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="number"></param>
        /// <param name="directionalDensity"></param>
        /// <returns></returns>
        public static Matrix3D[] GenerateBiasedOrientation(Matrix3D seed, int number, double directionalDensity)
        {
            Matrix3D[] mat = new Matrix3D[number];
            ParallelOptions p = new ParallelOptions();
            p.MaxDegreeOfParallelism = Environment.ProcessorCount;
            Parallel.For(0, number, p, i =>
            {
                double r1, r2, r3;
                lock (lockObject)
                {
                    r1 = rn.NextDouble();
                    r2 = rn.NextDouble() * 2 * Math.PI;
                    r3 = rn.NextDouble();
                }
                double rotZ = 2 * r1 - 1;
                double rotX = Math.Sqrt(1 - rotZ * rotZ) * Math.Sin(r2);
                double rotY = Math.Sqrt(1 - rotZ * rotZ) * Math.Cos(r2);

                double angle = r3 * directionalDensity + directionalDensity * 0.1; //Statistics.NormalDistribution(0, directionalDensity);

                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);
                double oneMinusCos = 1 - cos;

                double nxyc = rotX * rotY * oneMinusCos;
                double nyzc = rotY * rotZ * oneMinusCos;
                double nzxc = rotZ * rotX * oneMinusCos;
                double nxs = rotX * sin;
                double nys = rotY * sin;
                double nzs = rotZ * sin;

                Matrix3D m = new Matrix3D(
                    oneMinusCos * rotX * rotX + cos, nxyc + nzs, nzxc - nys,
                    nxyc - nzs, oneMinusCos * rotY * rotY + cos, nyzc + nxs,
                    nzxc + nys, nyzc - nxs, oneMinusCos * rotZ * rotZ + cos);

                mat[i] = m * seed;
            });
            return mat;
        }

        public static Matrix3D[] GenerateRandomOrientation(int number)
        {
            Matrix3D[] mat = new Matrix3D[number];
            for (int i = 0; i < number; i++)
            {
                mat[i] = Matrix3D.GenerateRamdomRotationMatrix();
            }
            return mat;
        }

        #endregion お蔵入り中

        /// <summary>
        /// 近い方位を計算するためのプライベート変数
        /// </summary>
        private Vector3DBase[][][] vec;

        private void initializeVec()
        {
            Vector3DBase v = new Vector3DBase(0, 0, 1);
            vec = new Vector3DBase[6][][];
            for (int plane = 0; plane < 6; plane++)
            {
                vec[plane] = new Vector3DBase[SquareDivision][];
                for (int y = 0; y < SquareDivision; y++)
                {
                    vec[plane][y] = new Vector3DBase[SquareDivision];
                    for (int x = 0; x < SquareDivision; x++)
                        vec[plane][y][x] = Rotations[GetIndex(plane, x, y, 0)] * v;
                }
            }
        }

        /// <summary>
        /// n番目の結晶子に近い方位を計算し、指定された密度で返す
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public void GetBiasedDirection(int n, ref int[] densityIndex, ref double[] densityValue, double range, double ratio)
        {
            if (vec == null || vec[0].Length != SquareDivision)
                initializeVec();

            Matrix3D baseRotation = Rotations[n].Inverse();
            double thresholdMax = Math.Cos(range * 3);
            double threshold = 1 + 2 * Math.Cos(range * 2);

            List<int>[] key = new List<int>[SquareDivision];
            List<double>[] value = new List<double>[SquareDivision];

            //まず、n番目のplane,x,y,rotを取得
            int basePlane = 0, baseX = 0, baseY = 0, baseRot = 0;
            GetIndex(n, ref basePlane, ref baseX, ref baseY, ref baseRot);

            //次に、rotを固定して、すべてのplane, x, yの行列を計算する
            Parallel.For(0, SquareDivision, y =>
            {
                key[y] = new List<int>();
                value[y] = new List<double>();
                for (int plane = 0; plane < 6; plane++)
                    for (int x = 0; x < SquareDivision; x++)
                    {
                        if (vec[basePlane][baseY][baseX] * vec[plane][y][x] > thresholdMax)
                            for (int rot = 0; rot < RotationDivision; rot++)
                            {
                                Matrix3D m = Rotations[GetIndex(plane, x, y, rot)];
                                double a = m.E11 * baseRotation.E11 + m.E12 * baseRotation.E21 + m.E13 * baseRotation.E31
                                         + m.E21 * baseRotation.E12 + m.E22 * baseRotation.E22 + m.E23 * baseRotation.E32
                                         + m.E31 * baseRotation.E13 + m.E32 * baseRotation.E23 + m.E33 * baseRotation.E33;
                                if (a > threshold)
                                {
                                    double d = Math.Acos(Math.Min(1.0, (a - 1.0) / 2.0)) / range;
                                    key[y].Add(GetIndex(plane, x, y, rot));
                                    value[y].Add(1 / (1 + d * d));
                                }
                            }
                    }
            });

            for (int i = 1; i < key.Length; i++)
            {
                key[0].AddRange(key[i]);
                value[0].AddRange(value[i]);
            }
            double sum = value[0].Sum();
            if (sum > 0)
            {
                for (int i = 0; i < value[0].Count; i++)
                    value[0][i] *= ratio * TotalCrystalline / sum;
                densityIndex = key[0].ToArray();
                densityValue = value[0].ToArray();
            }
            else
            {
                densityIndex = new int[] { 0 };
                densityValue = new double[] { 0 };
                ;
            }
        }

        /// <summary>
        /// 通し番号n から、面番号planeやxy座標を返す
        /// </summary>
        /// <param name="n"></param>
        /// <param name="plane"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rot"></param>
        public void GetIndex(int n, ref int plane, ref int x, ref int y, ref int rot)
        {
            plane = n / (TotalCrystalline / 6); //0~5
            n = (n - plane * TotalCrystalline / 6);
            rot = n % RotationDivision;
            n = n / RotationDivision;
            x = n % SquareDivision;
            y = n / SquareDivision;
        }

        public int GetIndex(int plane, int x, int y, int rot)
        {
            return plane * SquareDivision * SquareDivision * RotationDivision
                + y * SquareDivision * RotationDivision
                + x * RotationDivision
                + rot;
        }

        /// <summary>
        /// 指定した角度分解能で、全球を分割し、角度情報をRotations, SubRotationsに格納
        /// </summary>
        public void setCrystallineRotation()
        {
            int n = 0;
            Rotations1 = new Matrix3D[SquareDivision * SquareDivision];
            for (int y = 0; y < SquareDivision; y++)
                for (int x = 0; x < SquareDivision; x++)
                {
                    double a = Math.Tan(((x + 0.5) / SquareDivision - 0.5) * Math.PI / 2);
                    double b = Math.Tan(((y + 0.5) / SquareDivision - 0.5) * Math.PI / 2);
                    double norm = Math.Sqrt(a * a + b * b + 1);
                    double u = a / norm, v = b / norm;
                    double cosTheta = 1 / norm, sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);
                    double cosPhi = u / sinTheta, sinPhi = v / sinTheta;
                    if (sinTheta == 0)
                    { cosPhi = 1; sinPhi = 0; }
                    Rotations1[n++] = new Matrix3D(cosPhi, -sinPhi, 0, sinPhi, cosPhi, 0, 0, 0, 1) * new Matrix3D(1, 0, 0, 0, cosTheta, sinTheta, 0, -sinTheta, cosTheta);
                }

            n = 0;
            Rotations2 = new Matrix3D[RotationDivision];
            for (int rot = 0; rot < RotationDivision; rot++)
            {
                double cosKsi = Math.Cos(2 * Math.PI * rot / RotationDivision), sinKsi = Math.Sin(2 * Math.PI * rot / RotationDivision);
                Rotations2[n++] = new Matrix3D(cosKsi, -sinKsi, 0, sinKsi, cosKsi, 0, 0, 0, 1);
            }

            n = 0;
            Rotations = new Matrix3D[TotalCrystalline];
            for (int plane = 0; plane < 6; plane++)
                for (int y = 0; y < SquareDivision; y++)
                    for (int x = 0; x < SquareDivision; x++)
                        for (int rot = 0; rot < RotationDivision; rot++)
                            Rotations[n++] = convertRotationByPlane(Rotations1[y * SquareDivision + x] * Rotations2[rot], plane);

            n = 0;
            SubRotations1 = new Matrix3D[BaseCrystal.SubDivision * BaseCrystal.SubDivision * SquareDivision * SquareDivision];
            for (int y = 0; y < SquareDivision * BaseCrystal.SubDivision; y++)
                for (int x = 0; x < SquareDivision * BaseCrystal.SubDivision; x++)
                {
                    double a = Math.Tan(((x + 0.5) / SquareDivision / BaseCrystal.SubDivision - 0.5) * Math.PI / 2);
                    double b = Math.Tan(((y + 0.5) / SquareDivision / BaseCrystal.SubDivision - 0.5) * Math.PI / 2);
                    double norm = Math.Sqrt(a * a + b * b + 1);
                    double u = a / norm, v = b / norm;
                    double cosTheta = 1 / norm, sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);
                    double cosPhi = u / sinTheta, sinPhi = v / sinTheta;
                    if (sinTheta == 0)
                    { cosPhi = 1; sinPhi = 0; }
                    SubRotations1[n++] = new Matrix3D(cosPhi, -sinPhi, 0, sinPhi, cosPhi, 0, 0, 0, 1) * new Matrix3D(1, 0, 0, 0, cosTheta, sinTheta, 0, -sinTheta, cosTheta);
                }

            n = 0;
            SubRotations2 = new Matrix3D[BaseCrystal.SubDivision * RotationDivision];
            for (int rot = 0; rot < RotationDivision * BaseCrystal.SubDivision; rot++)
            {
                double cosKsi = Math.Cos(2 * Math.PI * rot / RotationDivision / SubDivision), sinKsi = Math.Sin(2 * Math.PI * rot / RotationDivision / SubDivision);
                SubRotations2[n++] = new Matrix3D(cosKsi, -sinKsi, 0, sinKsi, cosKsi, 0, 0, 0, 1);
            }
        }

        private Matrix3D rotationX = Matrix3D.Rot(new Vector3DBase(1, 0, 0), Math.PI);
        private Matrix3D rotationY = Matrix3D.Rot(new Vector3DBase(0, 1, 0), Math.PI);
        private Matrix3D rotationZ = Matrix3D.Rot(new Vector3DBase(0, 0, 1), Math.PI);

        public Matrix3D convertRotationByPlane(Matrix3D m, int plane)
        {
            switch (plane)
            {
                default: return m;
                case 1: return m.ExchangeYZX();
                case 2: return m.ExchangeZXY();
                case 3: return m.ExchangeX_Y_Z();
                case 4: return m.ExchangeY_Z_X();
                case 5: return m.ExchangeZ_X_Y();
            }
        }

        /// <summary>
        /// あるplane,x,y,rotに属するSubRotations配列を返す。あらかじめsetCrystallineRotation()で初期化しておく必要がある
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rot"></param>
        /// <returns></returns>
        private Matrix3D[] getSubRotations(int plane, int x, int y, int rot)
        {
            Matrix3D[] m = new Matrix3D[SubDivision * SubDivision * SubDivision];
            int n = 0;
            for (int i = 0; i < SubDivision; i++)
                for (int j = 0; j < SubDivision; j++)
                    for (int k = 0; k < SubDivision; k++)
                    {
                        m[n] = SubRotations1[SubDivision * SubDivision * SquareDivision * y + SubDivision * x + SubDivision * SquareDivision * i + j]
                            * SubRotations2[SubDivision * rot + k];
                        m[n] = convertRotationByPlane(m[n], plane);
                        n++;
                    }
            return m;
        }

        /// <summary>
        /// あるplane,x,y,rotに属するSubRotations配列を返す。あらかじめsetCrystallineRotation()で初期化しておく必要がある
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rot"></param>
        /// <returns></returns>
        private Matrix3D[] getSubRotations(int num)
        {
            int plane = num / (TotalCrystalline / 6); //0~5
            num -= plane * TotalCrystalline / 6;
            int rot = num % RotationDivision;
            num /= RotationDivision;
            int x = num % SquareDivision;
            int y = num / SquareDivision;
            return getSubRotations(plane, x, y, rot);
        }

        /// <summary>
        /// ある角度範囲が受け持つ立体角を初期化
        /// </summary>
        private void setCrystallineSolidAngle()
        {
            SolidAngle = new double[TotalCrystalline];

            for (int y = 0; y < SquareDivision; y++)
                for (int x = 0; x < SquareDivision; x++)
                {
                    List<PointD> pt = new List<PointD>();
                    for (double i = 0; i < 2; i++)
                        for (double j = 0; j < 2; j++)
                        {
                            double a = Math.Tan(((x + i) / SquareDivision - 0.5) * Math.PI / 2);
                            double b = Math.Tan(((y + j) / SquareDivision - 0.5) * Math.PI / 2);
                            pt.Add(Stereonet.ConvertVectorToSchmidt(new Vector3D(a, b, 1)) * 10);
                        }
                    double area = Geometriy.GetPolygonalArea(new[] { pt[0], pt[1], pt[3], pt[2] });
                    for (int plane = 0; plane < 6; plane++)
                        for (int rot = 0; rot < RotationDivision; rot++)
                            SolidAngle[plane * SquareDivision * SquareDivision * RotationDivision + y * SquareDivision * RotationDivision + x * RotationDivision + rot] = area;
                }
        }

        /// <summary>
        /// 各Crystallineの回折が寄与するピクセルのindexと指数を計算し、PixelIndexとPixelIntensityに格納する
        /// </summary>
        /// <param name="detector">AreaDetectorクラスの情報を与える</param>
        public void SetDiffractedPixels(AreaDetector detector)
        {
            stopwatch.Restart();

            Pixel = new (int Index, double Intensity)[TotalCrystalline][];
            //PixelIntensity = new double[TotalCrystalline][];

            var elasticity = new Elasticity(DenseMatrix.OfArray(BaseCrystal.ElasticStiffness), Elasticity.Mode.Stiffness);
            bool strainFree = BaseCrystal.Stress.IsZero() && BaseCrystal.Strain.IsZero();
            bool rotationFree = WholeRotation.IsIdentity();
            double ewaldR = 1 / detector.WaveLength, ewaldR2 = ewaldR * ewaldR;
            double monochromaticity2 = detector.Monochromaticity * detector.Monochromaticity;
            double maxMonochroHK2 = 2 * detector.MaxReciproZ * ewaldR * monochromaticity2;   //Z
            double size = GrainSize;
            double reciprocalPointSize = 1 / size / 2, reciprocalPointSize2 = reciprocalPointSize * reciprocalPointSize;
            double vol = size * size * size;

            int subDivision3 = SubDivision * SubDivision * SubDivision;

            double maxConvergenceHK = Math.Sin(BaseCrystal.AngleResolution / 180.0 * Math.PI / 2) / detector.WaveLength;//逆空間での、収束による逆格子点のにじみ(X線軸垂直な方向)
            if (SubDivision == 1)
                maxConvergenceHK = Math.Sin(detector.Convergence / 2) / detector.WaveLength;
            double maxConvergenceHK2 = maxConvergenceHK * maxConvergenceHK;
            double maxExcitationError2 = reciprocalPointSize2 + maxMonochroHK2 + maxConvergenceHK2, maxExcitationError = Math.Sqrt(maxExcitationError2);
            double maxHk2square = reciprocalPointSize2 + maxConvergenceHK2; // (-y,x,0)方向の半値幅

            if (ValidIndex == null)
            {
                ValidIndex = new ushort[TotalCrystalline][];
                ValidSubRotNum = new ushort[TotalCrystalline][][];
            }
            if (ValidSubRotNum == null)
                ValidSubRotNum = new ushort[TotalCrystalline][][];

            var div = 100;
            for (int i = 0; i < div; i++)
            {
                double ratio = (double)i / div, sec = stopwatch.ElapsedMilliseconds / 1000.0;
                ProgressChanged?.Invoke(this, new ProgressChangedEventArgs((int)(100.0 * ratio), new object[] { ratio, $"Elapsed: {sec:f2}sec.  Remaining: {sec * (1 - ratio) / ratio:f2}sec." }));

                Parallel.For(TotalCrystalline / div * i, Math.Min(TotalCrystalline, TotalCrystalline / div * (i + 1)), num =>
                   {
                       var baseRotation = new Matrix3D(Rotations[num]);
                       double x = 0, y = 0, z = 0;

                       if (ValidIndex[num] == null)
                       {
                           var index = new List<ushort>();

                           if (!strainFree) baseRotation = (elasticity.GetStrainByHill(BaseCrystal.Symmetry, baseRotation, BaseCrystal.Stress, BaseCrystal.Strain, BaseCrystal.HillCoefficient) + new Matrix3D()).Inverse() * baseRotation;
                           if (!rotationFree) baseRotation = WholeRotation * baseRotation;
                           #region  エワルド球に近い(回折条件に引っ掛かる)逆格子ベクトルを探索
                           for (ushort n = 0; n < G_Vector.Length; n++)
                           {
                               Vector3DBase g = G_Vector[n];
                               z = n % 2 == 0 ? baseRotation.E31 * g.X + baseRotation.E32 * g.Y + baseRotation.E33 * g.Z : -z;
                               if (z > -maxExcitationError * 2 && detector.MaxReciproZ + maxExcitationError * 2 > z)
                               {
                                   double rz = ewaldR - z;
                                   x = baseRotation.E11 * g.X + baseRotation.E12 * g.Y + baseRotation.E13 * g.Z;
                                   y = baseRotation.E21 * g.X + baseRotation.E22 * g.Y + baseRotation.E23 * g.Z;
                                   double ptX = detector.CameraLength / detector.Resolution / rz * x + detector.Center.X;
                                   double ptY = -detector.CameraLength / detector.Resolution / rz * y + detector.Center.Y;
                                   if (ptX < detector.ImageWidth - 1 && ptX > 0 && ptY < detector.ImageHeight - 1 && ptY > 0)
                                   {
                                       //エワルド球面が、試料近傍で平面近似できるとして計算する  楕円(x-X)^2/hk1^2 + (z-Z)^2/hk3^2 == 1 と 直線 y = X/(R-Z) x + R(1-sqrt(X^2/(R-Z)^2+1)) の連立方程式を解く
                                       double xyLength2 = x * x + y * y;
                                       double hk1square = maxHk2square + monochromaticity2 * xyLength2;//(x,y,0)方向の半値幅
                                       double hk3square = reciprocalPointSize2 + monochromaticity2 * z * z;// (0,0,z)方向の半値幅
                                       double hk3per1 = hk3square / hk1square;
                                       double sqrt = Math.Sqrt(1 + xyLength2 / rz / rz);
                                       double a = hk3per1 + xyLength2 / rz / rz;
                                       double b2 = (hk3per1 + (ewaldR * sqrt - ewaldR + z) / rz) * (hk3per1 + (ewaldR * sqrt - ewaldR + z) / rz) * xyLength2;
                                       double c = -hk3square * 4 + 2 * ewaldR * (ewaldR - rz * sqrt - z) + hk3per1 * xyLength2 + ewaldR2 * xyLength2 / rz / rz + z * z;//最初の項hk3へ書ける数値が許容半値幅の二乗
                                       if (b2 - a * c >= 0)
                                           index.Add(n);
                                   }
                               }
                           }
                           #endregion エワルド球に近い逆格子ベクトルを探索　ここまで

                           ValidIndex[num] = index.ToArray();
                       }

                       if (ValidIndex[num].Length > 0)
                       {
                           var rotations = getSubRotations(num);
                           var pixel = new List<(int Index, double Intensity)>();

                           if (ValidSubRotNum[num] == null)
                           {//validSubRotNumを初期化
                               ValidSubRotNum[num] = new ushort[ValidIndex[num].Length][];
                               for (int gNum = 0; gNum < ValidIndex[num].Length; gNum++)
                               {
                                   ValidSubRotNum[num][gNum] = new ushort[subDivision3];
                                   for (ushort rotNum = 0; rotNum < subDivision3; rotNum++)
                                       ValidSubRotNum[num][gNum][rotNum] = rotNum;
                               }
                           }

                           #region 探索した逆格子ベクトルの回折位置・強度計算 ここから
                           int count = 0;
                           foreach (int gNum in ValidIndex[num])
                           {
                               var subRotNum = new List<ushort>();

                               foreach (ushort m in ValidSubRotNum[num][count])
                               {
                                   if (!strainFree) rotations[m] = (elasticity.GetStrainByHill(BaseCrystal.Symmetry, rotations[m], BaseCrystal.Stress, BaseCrystal.Strain, BaseCrystal.HillCoefficient) + new Matrix3D()).Inverse() * rotations[m];

                                   x = rotations[m].E11 * G_Vector[gNum].X + rotations[m].E12 * G_Vector[gNum].Y + rotations[m].E13 * G_Vector[gNum].Z;
                                   y = rotations[m].E21 * G_Vector[gNum].X + rotations[m].E22 * G_Vector[gNum].Y + rotations[m].E23 * G_Vector[gNum].Z;
                                   z = rotations[m].E31 * G_Vector[gNum].X + rotations[m].E32 * G_Vector[gNum].Y + rotations[m].E33 * G_Vector[gNum].Z;

                                   int angle = (int)((Math.Atan2(y, x) / Math.PI + 1.0) * SpotShapesAngleDivision / 2);

                                   double xyLength2 = x * x + y * y;
                                   double xyLength = Math.Sqrt(xyLength2);

                                   //初期位置の計算
                                   double rz = ewaldR - z;
                                   double XY = (G_Hk3[gNum] / G_Hk1[gNum] + (ewaldR * Math.Sqrt(1 + xyLength2 / rz / rz) - ewaldR + z) / rz) / (G_Hk3[gNum] / G_Hk1[gNum] + xyLength2 / rz / rz) * xyLength;
                                   double d = detector.CameraLength / detector.Resolution / Math.Sqrt(ewaldR2 - XY * XY) / xyLength * XY;
                                   int startPosition = (int)(-d * y + detector.Center.Y + 0.5) * detector.ImageWidth + (int)(d * x + detector.Center.X + 0.5);
                                   double tempIntensity = G_Intensity2[gNum] / subDivision3 * SolidAngle[num];
                                   bool flag = true;

                                   for (int j = 0; j < SpotShapesSortedIndex[gNum][angle].Length; j++)//現在のピクセル位置から、周辺強度を計算していって、半値幅の2倍以上になったら終了
                                   {
                                       int pos = startPosition + SpotShapesSortedIndex[gNum][angle][j];
                                       if (pos > -1 && pos < detector.ImageLength)
                                       {
                                           double temp1 = x * detector.ReciprocalVectors[pos].X + y * detector.ReciprocalVectors[pos].Y - xyLength2;
                                           double temp2 = x * detector.ReciprocalVectors[pos].Y - y * detector.ReciprocalVectors[pos].X;
                                           double temp3 = z - detector.ReciprocalVectors[pos].Z;
                                           double dev2 = temp1 * temp1 / xyLength2 / G_Hk1[gNum] + temp2 * temp2 / xyLength2 / G_Hk2[gNum] + temp3 * temp3 / G_Hk3[gNum];
                                           if (dev2 > DeviationThreshold[gNum] * 1.3)
                                               break;
                                           else if (dev2 < DeviationThreshold[gNum])
                                           {
                                               //double temp = Math.Exp(-dev2)  * tempIntensity * detector.ReciprocalAreas[pos];
                                               double temp = tempIntensity / (1.0 + dev2) * detector.ReciprocalAreas[pos];

                                               double eta = 0;
                                               temp = (eta / (1 + dev2) + (1 - eta) * Math.Sqrt(Math.PI * Math.Log(2)) * Math.Pow(2, -dev2)) * tempIntensity * detector.ReciprocalAreas[pos];

                                               int n = pixel.FindIndex(e => e.Index == pos);
                                               if (n == -1)
                                                   pixel.Add((pos, temp));
                                               else
                                                   pixel[n] = (pixel[n].Index, pixel[n].Intensity + temp);

                                               if (flag)//一回でも成功したらsubRotNumに加える
                                               {
                                                   subRotNum.Add(m);
                                                   flag = false;
                                               }
                                           }
                                       }
                                   }
                               }
                               ValidSubRotNum[num][count++] = subRotNum.ToArray();
                           }
                           #endregion

                           Pixel[num] = pixel.ToArray();
                       }
                   });//Parallel.Forおしまい

            }
        }

        public Vector3D[] GetSpotPosition(AreaDetector detector, Matrix3D m, bool calculateOnlySpotPositon = false)
        {
            double ewaldR = 1 / detector.WaveLength, ewaldR2 = ewaldR * ewaldR;
            double monochromaticity2 = detector.Monochromaticity * detector.Monochromaticity;
            double maxMonochroHK2 = 2 * detector.MaxReciproZ * ewaldR * monochromaticity2;   //Z
            double size = GrainSize;
            double reciprocalPointSize = 1 / size / 2, reciprocalPointSize2 = reciprocalPointSize * reciprocalPointSize;

            double maxConvergenceHK = Math.Sin(detector.Convergence / 2) / detector.WaveLength;//逆空間での、収束による逆格子点のにじみ(X線軸垂直な方向)
            double maxConvergenceHK2 = maxConvergenceHK * maxConvergenceHK;
            double maxExcitationError2 = reciprocalPointSize2 + maxMonochroHK2 + maxConvergenceHK2, maxExcitationError = Math.Sqrt(maxExcitationError2);
            double maxHk2square = reciprocalPointSize2 + maxConvergenceHK2; // (-y,x,0)方向の半値幅

            List<Vector3D> list = new List<Vector3D>();
            //エワルド球に近い(回折条件に引っ掛かる)逆格子ベクトルを探索
            for (int n = 0; n < G_Vector.Length; n++)
            {
                Vector3DBase g = G_Vector[n];
                double z = m.E31 * g.X + m.E32 * g.Y + m.E33 * g.Z;
                if (z > -maxExcitationError * 2 && detector.MaxReciproZ + maxExcitationError * 2 > z)
                {
                    double rz = ewaldR - z;
                    double x = m.E11 * g.X + m.E12 * g.Y + m.E13 * g.Z;
                    double y = m.E21 * g.X + m.E22 * g.Y + m.E23 * g.Z;
                    double ptX = detector.CameraLength / detector.Resolution / rz * x + detector.Center.X;
                    double ptY = -detector.CameraLength / detector.Resolution / rz * y + detector.Center.Y;
                    if (ptX < detector.ImageWidth - 1 && ptX > 0 && ptY < detector.ImageHeight - 1 && ptY > 0)
                    {
                        //エワルド球面が、試料近傍で平面近似できるとして計算する  楕円(x-X)^2/hk1^2 + (z-Z)^2/hk3^2 == 1 と 直線 y = X/(R-Z) x + R(1-sqrt(X^2/(R-Z)^2+1)) の連立方程式を解く
                        double xyLength2 = x * x + y * y;
                        double hk1square = maxHk2square + monochromaticity2 * xyLength2;//(x,y,0)方向の半値幅
                        double hk3square = reciprocalPointSize2 + monochromaticity2 * z * z;// (0,0,z)方向の半値幅
                        double hk3per1 = hk3square / hk1square;
                        double sqrt = Math.Sqrt(1 + xyLength2 / rz / rz);
                        double a = hk3per1 + xyLength2 / rz / rz;
                        double b2 = (hk3per1 + (ewaldR * sqrt - ewaldR + z) / rz) * (hk3per1 + (ewaldR * sqrt - ewaldR + z) / rz) * xyLength2;
                        double c = -hk3square * 4 + 2 * ewaldR * (ewaldR - rz * sqrt - z) + hk3per1 * xyLength2 + ewaldR2 * xyLength2 / rz / rz + z * z;//最初の項hk3へ書ける数値が許容半値幅の二乗
                        if (b2 - a * c >= 0)
                        {
                            double xyLength = Math.Sqrt(xyLength2);
                            //初期位置の計算
                            double XY = (G_Hk3[n] / G_Hk1[n] + (ewaldR * Math.Sqrt(1 + xyLength2 / rz / rz) - ewaldR + z) / rz) / (G_Hk3[n] / G_Hk1[n] + xyLength2 / rz / rz) * xyLength;
                            double d = detector.CameraLength / detector.Resolution / Math.Sqrt(ewaldR2 - XY * XY) / xyLength * XY;
                            double temp1 = x - x * ewaldR / rz / sqrt;
                            double temp3 = z - (-ewaldR / sqrt + ewaldR);
                            double dev2 = temp1 * temp1 / xyLength2 / G_Hk1[n] + temp3 * temp3 / G_Hk3[n];
                            double eta = 0;
                            double intensity = (eta / (1 + dev2) + (1 - eta) * Math.Sqrt(Math.PI * Math.Log(2)) * Math.Pow(2, -dev2)) * G_Intensity2[n];

                            list.Add(new Vector3D(d * x + detector.Center.X, -d * y + detector.Center.Y, intensity));
                            list[list.Count - 1].h = BaseCrystal.VectorOfG[n].h;
                            list[list.Count - 1].k = BaseCrystal.VectorOfG[n].k;
                            list[list.Count - 1].l = BaseCrystal.VectorOfG[n].l;
                        }
                    }
                }
            } //エワルド球に近い逆格子ベクトルを探索　ここまで

            return list.ToArray();
        }

        /// <summary>
        /// 与えられた密度から、回折パターンを計算.
        /// 予め、PixelIndexやPixelIntnsityをSetDiffractedPixels()で設定しておく必要があり。
        /// </summary>
        /// <param name="detector"></param>
        /// <param name="density"></param>
        /// <returns></returns>
        public double[] GetSimulatedPattern(AreaDetector detector, double[] density = null)
        {
            if (density == null)
                density = Density;

            double[] pixels = new double[detector.ImageLength];
            for (int i = 0; i < TotalCrystalline; i++)
                if (Pixel[i] != null)
                    for (int j = 0; j < Pixel[i].Length; j++)
                        pixels[Pixel[i][j].Index] += Pixel[i][j].Intensity * density[i];
            return pixels;
        }

        //double[] tempPixels;
        /// <summary>
        /// 与えられた密度から、回折パターンを計算.
        /// 予め、PixelIndexやPixelIntnsityをSetDiffractedPixels()で設定しておく必要があり。
        /// </summary>
        /// <param name="detector"></param>
        /// <param name="density"></param>
        /// <returns></returns>
        public double[] GetSimulatedPattern(AreaDetector detector, int[] densityIndex, double[] densityValue)
        {
            /*    if (tempPixels == null || tempPixels.Length != detector.ImageLength)
                    tempPixels = new double[detector.ImageLength];
                for (int i = 0; i < tempPixels.Length; i++)
                    tempPixels[i] = 0;
                */
            double[] pixels = new double[detector.ImageLength];
            for (int i = 0; i < densityIndex.Length; i++)
            {
                int index = densityIndex[i];
                if (Pixel[index] != null)
                    for (int j = 0; j < Pixel[index].Length; j++)
                        pixels[Pixel[index][j].Index] += Pixel[index][j].Intensity * densityValue[i];
            }
            return pixels;
        }
    }

    #region お蔵入り中のPowderクラス

    /// <summary>
    /// 多結晶体を取り扱う.
    /// </summary>
    [Serializable()]
    public static class Powder
    {
        //public double GrainSize=1;

        //public Crystal BaseCrystal;
        //public Matrix3D BaseRotation = new Matrix3D();
        //public List<Matrix3D> BaseRotationList = new List<Matrix3D>();

        //public Crystallite[] Crysatallites;

        //Vector3DBase[] gVector;
        //double[] intensity = new double[0];

        //public double[][] CrystalliteRotations;
        //public List<int>[] indexeCandidates;

        // public PolyCrystal(Crystal baseCrystal,PolycrystallineProperty[] eachCrystals)
        // {
        //BaseCrystal = baseCrystal;
        //Crysatallites = eachCrystals;
        //GrainSize = grainSize;

        // if (strain != null)
        //     baseCrystal.Strain = strain;
        // if (stress != null)
        //     baseCrystal.Stress = stress;
        // baseCrystal.HillCoefficient = hill;
        // }

        //public void ResetBaseRotation()
        //{
        //    BaseRotationList.Clear();
        //}

        // private PointD beforeCenter = new PointD(double.NaN, double.NaN);

        public static double[] GetSimulatedPattern
            (Crystal crystal, Crystallite crystallites, bool resetIndex, AreaDetector detector, YusaGonio gonio = null)
        {
            return new double[0];
        }

        /// <summary>
        /// 多結晶体のディフラクションパターンを計算する
        /// </summary>
        /// <param name="rotation">回転行列</param>
        /// <param name="resetIndex">既に計算済みの回折に寄与する逆格子ベクトルをリセットする。もともと計算していなかったらこの値に関わらず再計算する。</param>
        /// <param name="renewRvector">イメージ中の各ピクセルが対応する逆空間ベクトルおよび面積をリセットする。もともと計算していなかったらこの値に関わらず再計算する。</param>
        /// <param name="renewGvector">逆格子点のベクトルや強度をリセットする。もともと計算していなかったらこの値に関わらず再計算する。</param>
        /// <param name="crystallites">計算対象のCrystallite配列。省略すると、既定のCrystallitesを計算する。</param>
        /// <param name="reciprocalVec">イメージ中の各ピクセルが対応する逆空間ベクトル。省略してもよい。</param>
        /// <param name="reciprocalArea">イメージ中の各ピクセルが対応する逆空間面積。省略してもよい。</param>
        /// <param name="gonio">YusaGonioを使用した場合の、設定値。省略してもよい。</param>
        /// <returns></returns>
        /* public static double[] GetSimulatedPattern
             (Crystal crystal, Crystallite crystallites, bool resetIndex, AreaDetector detector, YusaGonio gonio = null)
         {
             if (crystallites == null)
                 crystallites = crystal.Crystallites;

             //BaseRotation = rotation;
             //if (!BaseRotationList.Contains(rotation))
             //{
             //    BaseRotationList.Add(rotation);
             //    resetIndex = true;
             //}

             //まず各種定数の設定
             //if (renewRvector)
             detector.SetReciprocalSpace();

             //if (resetIndex)
             //    crystallites.Index = new List<int>[crystallites.TotalCrystalline];

             double[] pixelsSum = getSimulatedPattern(crystal, crystallites, detector, gonio);

             Parallel.For(0, pixelsSum.Length, i =>
             {
                 if (pixelsSum[i] != 0)
                     pixelsSum[i] *= detector.ReciprocalAreas [i] * 0.0001;
             });

             return pixelsSum;
         }*/

        /* private double[] getSimulatedPattern(Crystallite[] crystals, int imageWidth, int imageHeight, double centerX, double centerY, double resolution, double convergence, double monochromaticity)
         {
             return getSimulatedPattern(crystals, imageWidth, imageHeight, centerX, centerY, resolution, convergence, monochromaticity, Environment.ProcessorCount);
         }*/
        /*static object lockThis=new object();
        private static double[] getSimulatedPattern(Crystal crystal, Crystallite crystallites, AreaDetector detector, YusaGonio gonio = null, int threadNumber = 0)
        {
            if (threadNumber == 0)
                threadNumber =  Environment.ProcessorCount;

            double[] pixels = new double[detector.ImageHeight * detector.ImageWidth];

            Elasticity e = new Elasticity(crystal.ElasticStiffness, Elasticity.Mode.Stiffness);
            bool strainFree = crystal.Stress.IsZero() && crystal.Strain.IsZero();
            bool rotationFree =  crystallites.WholeRotation.IsIdentity();

            double convergenceHK = Math.Sin(detector.Convergence / 2) / detector.WaveLength;//逆空間での、収束による逆格子点のにじみ(X線軸垂直な方向)
            double convergenceHK2 = convergenceHK * convergenceHK;
            //double convergenceZ = (1 - Math.Cos(convergence / 2)) / WaveLength;//逆空間での、収束による逆格子点のにじみ(X線軸平行な方向) //この成分は無視することにしよう

            double ewaldR = 1 / detector.WaveLength;
            double ewaldR2 = ewaldR * ewaldR;
            double monochromaticity2 = detector.Monochromaticity * detector.Monochromaticity;
            double maxMonochroHK2 = 2 * detector.MaxReciproZ * ewaldR * monochromaticity2;   //Z
            double size = crystallites.GrainSizes[0];
            double reciprocalPointSize = 1 / size / 2;
            double reciprocalPointSize2 = reciprocalPointSize * reciprocalPointSize;
            double vol = size * size * size;
            double maxExcitationError2 = reciprocalPointSize2 + maxMonochroHK2 + convergenceHK2;
            double maxExcitationError = Math.Sqrt(maxExcitationError2);

            double hk2square = reciprocalPointSize2 + convergenceHK2; // (-y,x,0)方向の半値幅

            int[] directions = new int[] { 0, -detector.ImageWidth - 1, -detector.ImageWidth, -detector.ImageWidth + 1, -1, 1, detector.ImageWidth - 1, detector.ImageWidth, detector.ImageWidth + 1 };
            ParallelOptions p = new ParallelOptions();
            p.MaxDegreeOfParallelism = threadNumber;

            //YusaGonioMatrixを初期化
            int repetation = 1;
            if (gonio != null && gonio.Valid)
                repetation = 30 * 30;
            Matrix3D[] yusaGonioMatrix = new Matrix3D[repetation];
            for (int l = 0; l < repetation; l++)
            {
                yusaGonioMatrix[l] = new Matrix3D();
                if (repetation != 1)
                {
                    if (gonio.Rx)
                        yusaGonioMatrix[l] = Matrix3D.Rot(new Vector3D(0, 0, 1), 2 * Math.PI * (double)l / repetation) * yusaGonioMatrix[l];
                    //z,yの格子間隔を計算
                    int zNum = (int)Math.Sqrt(repetation);
                    int yNum = (int)Math.Sqrt(repetation);
                    double zStart = -gonio.Rz_OscillationRange / 180 * Math.PI;
                    double yStart = -gonio.Ry_OscillationRange / 180 * Math.PI;
                    double zDiv = (gonio.Rz_OscillationRange * 2) / zNum / 180 * Math.PI;
                    double yDiv = (gonio.Ry_OscillationRange * 2) / yNum / 180 * Math.PI;
                    yusaGonioMatrix[l] = Matrix3D.Rot(new Vector3D(0, 1, 0), zStart + (l % zNum) * zDiv) * yusaGonioMatrix[l];
                    yusaGonioMatrix[l] = Matrix3D.Rot(new Vector3D(1, 0, 0), yStart + (l / yNum) * yDiv) * yusaGonioMatrix[l];
                }
            }
            //YusaGonioMatrixを初期化ここまで

            for (int l = 0; l < repetation; l++)
            {
                if (repetation != 1)
                    for (int m = 0; m < crystallites.TotalCrystalline; m++)
                        crystallites.Index[m] = null;

                //エワルド球に近い(回折条件に引っ掛かる)逆格子ベクトルを探索
                Parallel.For(0, crystallites.TotalCrystalline, p, i =>
                {
                    if (crystallites.Index[i] == null)//crystal[i].index が設定されていないとき
                    {
                        Matrix3D rotation = crystallites.CrystallineRotations[i];
                        if (repetation != 1)
                            rotation = yusaGonioMatrix[l] * rotation;

                        if (!strainFree)
                            rotation = (e.GetStrainByHill(crystal.Symmetry, crystallites.CrystallineRotations[i], crystal.Stress, crystal.Strain, crystal.HillCoefficient) + new Matrix3D()).Inverse() * rotation;
                        if (crystallites.Index[i] == null)
                            crystallites.Index[i] = new List<int>();

                       // for (int j = 0; j < crystallites.WholeRotations.Count; j++)
                        {
                            if (!rotationFree)
                                rotation = crystallites.WholeRotation * rotation;

                            double x = 0, y = 0, z = 0;
                            for (int n = 0; n < crystallites.G_Vector.Length; n++)
                            {
                                Vector3DBase g = crystallites.G_Vector[n];
                                z = n % 2 == 0 ? rotation.E31 * g.X + rotation.E32 * g.Y + rotation.E33 * g.Z : -z;
                                if (z > -maxExcitationError * 2 && detector.MaxReciproZ + maxExcitationError * 2 > z)
                                {
                                    double rz = ewaldR - z;
                                    x = rotation.E11 * g.X + rotation.E12 * g.Y + rotation.E13 * g.Z;
                                    y = rotation.E21 * g.X + rotation.E22 * g.Y + rotation.E23 * g.Z;
                                    double ptX = detector.CameraLength / detector.Resolution / rz * x + detector.Center.X;
                                    double ptY = -detector.CameraLength / detector.Resolution / rz * y + detector.Center.Y;
                                    if (ptX < detector.ImageWidth - 1 && ptX > 0 && ptY < detector.ImageHeight - 1 && ptY > 0)
                                    {
                                        //エワルド球面が、試料近傍で平面近似できるとして計算する  楕円(x-X)^2/hk1^2 + (z-Z)^2/hk3^2 == 1 と 直線 y = X/(R-Z) x + R(1-sqrt(X^2/(R-Z)^2+1)) の連立方程式を解く
                                        double xyLength2 = x * x + y * y;
                                        double hk1square = hk2square + monochromaticity2 * xyLength2;//(x,y,0)方向の半値幅
                                        double hk3square = reciprocalPointSize2 + monochromaticity2 * z * z;// (0,0,z)方向の半値幅
                                        double hk3per1 = hk3square / hk1square;
                                        double sqrt = Math.Sqrt(1 + xyLength2 / rz / rz);
                                        double a = hk3per1 + xyLength2 / rz / rz;
                                        double b2 = (hk3per1 + (ewaldR * sqrt - ewaldR + z) / rz) * (hk3per1 + (ewaldR * sqrt - ewaldR + z) / rz) * xyLength2;
                                        double c = -hk3square * 4 + 2 * ewaldR * (ewaldR - rz * sqrt - z) + hk3per1 * xyLength2 + ewaldR2 * xyLength2 / rz / rz + z * z;//最初の項hk3へ書ける数値が許容半値幅の二乗
                                        if (b2 - a * c >= 0 && !crystallites.Index[i].Contains(n))
                                            crystallites.Index[i].Add(n);
                                    }
                                }
                            }
                        }
                    }
                });//エワルド球に近い逆格子ベクトルを探索　ここまで

                //探索した逆格子ベクトルの回折位置・強度計算
                Parallel.For(0, crystallites.TotalCrystalline, p, i =>
                {
                    if (crystallites.Index[i].Count > 0)
                    {
                        Matrix3D rotation = crystallites.CrystallineRotations[i];

                        if (repetation != 1)
                            rotation = yusaGonioMatrix[l] * rotation;

                        if (!strainFree)
                            rotation = (e.GetStrainByHill(crystal.Symmetry, crystallites.CrystallineRotations[i],crystal.Stress, crystal.Strain, crystal.HillCoefficient) + new Matrix3D()).Inverse() * rotation;

                        //if (!rotationFree)
                        //    rotation = BaseRotation * rotation;

                        double x = 0, y = 0, z = 0;

                        foreach (int n in crystallites.Index[i])
                        {
                            Vector3DBase g = crystallites.G_Vector[n];
                            x = rotation.E11 * g.X + rotation.E12 * g.Y + rotation.E13 * g.Z;
                            y = rotation.E21 * g.X + rotation.E22 * g.Y + rotation.E23 * g.Z;
                            z = rotation.E31 * g.X + rotation.E32 * g.Y + rotation.E33 *g.Z;

                            double xyLength2 = x * x + y * y;
                            double xyLength = Math.Sqrt(xyLength2);
                            double hk1square = hk2square + monochromaticity2 * xyLength2;//(x,y,0)方向の半値幅
                            double hk3square = reciprocalPointSize2 + monochromaticity2 * z * z;// (0,0,z)方向の半値幅
                            double intensityTemp = 1 / Math.Sqrt(hk1square * 1000 * hk2square * 1000 * hk3square * 1000) * vol / 1000 * crystallites.G_Intensity[n];// *crystals[i].Density;
                            if (repetation != 1)
                                intensityTemp = intensityTemp / repetation;

                            List<int> comp = new List<int>();//計算済みピクセル
                            //初期位置の計算
                            double rz = ewaldR - z;
                            double a = hk3square / hk1square + xyLength2 / rz / rz;
                            double b = (hk3square / hk1square + (ewaldR * Math.Sqrt(1 + xyLength2 / rz / rz) - ewaldR + z) / rz) * xyLength;
                            double XY = Math.Abs(b / a);
                            double d = detector.CameraLength / detector.Resolution / Math.Sqrt(ewaldR2 - XY * XY) / xyLength * XY;
                            List<int> rim = new List<int>(new int[] { (int)(-d * y + detector.Center.Y + 0.5) * detector.ImageWidth + (int)(d * x + detector.Center.X + 0.5) });//初期外縁ピクセル

                            for (int j = 0; j < rim.Count; j++)//rim.RemoveAt(0))//現在のピクセル位置から、周辺強度を計算していって、半値幅の2倍以上になったら終了
                                for (int k = j == 0 ? 0 : 1; k < directions.Length; k++)
                                {
                                    int pos = rim[j] + directions[k];
                                    if ((j == 0 || !comp.Contains(pos)) && pos > -1 && pos < pixels.Length)
                                    {
                                        comp.Add(pos);
                                        double temp1 = x * detector.ReciprocalVectors[pos].X + y * detector.ReciprocalVectors[pos].Y - xyLength2;
                                        double temp2 = x * detector.ReciprocalVectors[pos].Y - y * detector.ReciprocalVectors[pos].X;
                                        double dev2 = temp1 * temp1 / xyLength2 / hk1square + temp2 * temp2 / xyLength2 / hk2square + (z - detector.ReciprocalVectors[pos].Z) * (z - detector.ReciprocalVectors[pos].Z) / hk3square;
                                        if (dev2 < 5.0)
                                        {
                                            double temp = Math.Exp(-dev2) * intensityTemp;//double temp = intensityTemp / (1.0 + dev2);
                                            lock (lockThis)
                                            {
                                                pixels[pos] += temp;
                                                if (k != 0)
                                                    rim.Add(pos);
                                            }
                                        }
                                    }
                                }
                        }
                    }
                });//探索した逆格子ベクトルの回折位置・強度計算 ここまで
            }

            if (repetation != 1)//YusaGonioの時だけ、もう一度スキャン
                for (int l = 0; l < repetation; l++)
                {
                    //エワルド球に近い(回折条件に引っ掛かる)逆格子ベクトルを探索
                    Parallel.For(0, crystallites.TotalCrystalline, p, i =>
                    {
                        Matrix3D rotation = crystallites.CrystallineRotations[i];
                        rotation = yusaGonioMatrix[l] * rotation;

                        if (!strainFree)
                            rotation = (e.GetStrainByHill(crystal.Symmetry, crystallites.CrystallineRotations[i], crystal.Stress, crystal.Strain, crystal.HillCoefficient) + new Matrix3D()).Inverse() * rotation;
                        if (crystallites.Index[i] == null)
                            crystallites.Index[i] = new List<int>();

                        //for (int j = 0; j < crystallites.WholeRotations.Count; j++)
                        {
                            if (!rotationFree)
                                rotation = crystallites.WholeRotation * rotation;

                            double x = 0, y = 0, z = 0;
                            for (int n = 0; n < crystallites.G_VectorNumber; n++)
                            {
                                Vector3DBase g = crystallites.G_Vector[n];
                                z = n % 2 == 0 ? rotation.E31 * g.X + rotation.E32 * g.Y + rotation.E33 * g.Z : -z;
                                if (z > -maxExcitationError * 2 && detector.MaxReciproZ + maxExcitationError * 2 > z)
                                {
                                    double rz = ewaldR - z;
                                    x = rotation.E11 * g.X + rotation.E12 * g.Y + rotation.E13 * g.Z;
                                    y = rotation.E21 * g.X + rotation.E22 * g.Y + rotation.E23 * g.Z;
                                    double ptX = detector.CameraLength / detector.Resolution / rz * x + detector.Center.X;
                                    double ptY = -detector.CameraLength / detector.Resolution / rz * y + detector.Center.Y;
                                    if (ptX < detector.ImageWidth - 1 && ptX > 0 && ptY < detector.ImageHeight - 1 && ptY > 0)
                                    {
                                        //エワルド球面が、試料近傍で平面近似できるとして計算する  楕円(x-X)^2/hk1^2 + (z-Z)^2/hk3^2 == 1 と 直線 y = X/(R-Z) x + R(1-sqrt(X^2/(R-Z)^2+1)) の連立方程式を解く
                                        double xyLength2 = x * x + y * y;
                                        double hk1square = hk2square + monochromaticity2 * xyLength2;//(x,y,0)方向の半値幅
                                        double hk3square = reciprocalPointSize2 + monochromaticity2 * z * z;// (0,0,z)方向の半値幅
                                        double hk3per1 = hk3square / hk1square;
                                        double sqrt = Math.Sqrt(1 + xyLength2 / rz / rz);
                                        double a = hk3per1 + xyLength2 / rz / rz;
                                        double b2 = (hk3per1 + (ewaldR * sqrt - ewaldR + z) / rz) * (hk3per1 + (ewaldR * sqrt - ewaldR + z) / rz) * xyLength2;
                                        double c = -hk3square * 4 + 2 * ewaldR * (ewaldR - rz * sqrt - z) + hk3per1 * xyLength2 + ewaldR2 * xyLength2 / rz / rz + z * z;//最初の項hk3へ書ける数値が許容半値幅の二乗
                                        if (b2 - a * c >= 0 && !crystallites.Index[i].Contains(n))
                                            crystallites.Index[i].Add(n);
                                    }
                                }
                            }
                        }
                    });//エワルド球に近い逆格子ベクトルを探索　ここまで
                }

            return pixels;
        }

      */
    }

    #endregion お蔵入り中のPowderクラス
}
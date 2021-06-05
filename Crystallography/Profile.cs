using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Xml.Serialization;

namespace Crystallography
{
    [Serializable]
    public class Profile : ICloneable
    {
        public object Clone()
        {
            Profile p = (Profile)this.MemberwiseClone();
            p.Pt = new List<PointD>(Pt.ToArray());
            return p;
        }

        public string text;

        public List<PointD> Pt;
        public List<PointD> Err;

        public System.Drawing.Color Color = System.Drawing.Color.Blue;

        public float LineWidth = 1f;

        public RectangleD Range
        { get { return new RectangleD(new PointD(Pt.Min(p => p.X), Pt.Min(p => p.Y)), new PointD(Pt.Max(p => p.X), Pt.Max(p => p.Y))); } }

        public Profile()
        {
            Pt = new List<PointD>();
            Err = new List<PointD>();
        }

        public Profile(IEnumerable<PointD> pt) : this()
        {
            Pt.AddRange(pt);
        }

        public Profile(Color color) : this()
        {
            Color = color;
        }

        public Profile(IEnumerable<PointD> pt, Color color) : this()
        {
            Pt.AddRange(pt);
            Color = color;
        }

        public double MaxX => Pt.Max(p => p.X);
        public double MinX => Pt.Min(p => p.X);
        public double MaxY => Pt.Max(p => p.Y);
        public double MinY => Pt.Min(p => p.Y);

        public Profile CopyTo()
        {
            Profile p = new Profile();

            p.Pt = new List<PointD>();
            p.Err = new List<PointD>();
            for (int i = 0; i < Pt.Count; i++)
                p.Pt.Add(Pt[i]);

            for (int i = 0; i < Err.Count; i++)
                p.Err.Add(Err[i]);

            p.text = text;
            return p;
        }

        /// <summary>
        /// 指定されたxの値に対するerrを返す
        /// </summary>
        /// <param name="x"></param>
        /// <param name="pointNum"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public double GetErr(double x)
        {
            if (Err.Count != Pt.Count)
                return 0;

            //まず点の位置を探す
            if (Err[0].X > x)
                return Err[0].Y;
            else if (Err[^1].X < x)
                return Err[^1].Y;
            else
            {
                for (int i = 0; i < Err.Count - 1; i++)
                    if (Err[i].X == x)
                        return Err[i].Y;
                    else if (Err[i].X <= x && x <= Err[i + 1].X)
                        return (Err[i].Y + Err[i + 1].Y) / 2;
            }
            return 0;
        }

        /// <summary>
        /// 指定されたxの値に対してorder次関数で補間した値を返す
        /// </summary>
        /// <param name="x"></param>
        /// <param name="pointNum"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public double GetValue(double x, int pointNum, int order, bool eachside = false)
        {
            if (pointNum == 2 && order == 1)//探索する点数が2, オーダーが1の時は、高速化のため以下のルーチンで
            {
                int pos = 0;//この値と、この値+1の間のindexにxが存在する
                if (x > Pt[Pt.Count * 3 / 4].X)
                    pos = Pt.Count * 3 / 4;
                else if (x > Pt[Pt.Count / 2].X)
                    pos = Pt.Count / 2;
                else if (x > Pt[Pt.Count / 4].X)
                    pos = Pt.Count / 4;

                if (x <= Pt[0].X)
                    return (Pt[0].Y - Pt[1].Y) / (Pt[0].X - Pt[1].X) * (x - Pt[1].X) + Pt[1].Y;
                else if (Pt[^1].X < x)
                    return (Pt[^2].Y - Pt[^1].Y) / (Pt[^2].X - Pt[^1].X) * (x - Pt[^1].X) + Pt[^1].Y;
                else
                    for (; pos < Pt.Count - 1; pos++)
                    {
                        if (Pt[pos].X < x && x < Pt[pos + 1].X)
                            return (Pt[pos].Y - Pt[pos + 1].Y) / (Pt[pos].X - Pt[pos + 1].X) * (x - Pt[pos + 1].X) + Pt[pos + 1].Y;
                        else if (Pt[pos + 1].X == x)
                            return Pt[pos + 1].Y;
                    }
                return (Pt[pos].Y - Pt[pos + 1].Y) / (Pt[pos].X - Pt[pos + 1].X) * (x - Pt[pos + 1].X) + Pt[pos + 1].Y;
            }

            return GetValues(new double[] { x }, pointNum, order, eachside, false)[0];
        }

        /// <summary>
        /// 指定されたxの値に対してorder次関数で補間した値を返す
        /// </summary>
        /// <param name="x"></param>
        /// <param name="pointNum">探索する点数</param>
        /// <param name="order">フィッティングする次数</param>
        /// <param name="eachside">両サイドでなるべく均等な点数を採用</param>
        /// <param name="shareSameFunction"></param>
        /// <returns></returns>
        public double[] GetValues(double[] x, int pointNum, int order, bool eachside = false, bool shareSameFunction = false)
        {
            if (pointNum < 2) pointNum = 2;
            if (order > pointNum - 1) order = pointNum - 1;
            double[] value = new double[x.Length];

            /*   var srcPts = new List<List<PointD>>(); //Xに同じ値が含まれている場合(吸収端のような不連続なプロファイル)を考慮して、分離しておく
               for (int i=0; i< Pt.Count; i++)
               {
                   var temp = new List<PointD>();
                   for (int j = i; j < Pt.Count; j++)
                       if (j == 0 || Pt[j - 1].X != Pt[j].X)
                           temp.Add(Pt[j]);
                       else
                           break;
               }
               */

            for (int n = 0; n < x.Length; n++)
            {
                bool flag = true;
                //まず点の位置を探す
                int position = 0;//この値と この値+1の間のindexにxが存在する
                if (Pt[0].X > x[n])
                    position = -1;
                else if (Pt[^1].X < x[n])
                    position = Pt.Count - 1;
                else
                    for (int i = 0; i < Pt.Count - 1; i++)
                        if (Pt[i + 1].X == x[n])
                        {
                            value[n] = Pt[i + 1].Y;
                            flag = false;
                            break;
                        }
                        else if (Pt[i].X <= x[n] && x[n] <= Pt[i + 1].X)
                        {
                            position = i;
                            break;
                        }
                if (flag)
                {
                    //次に、この点から前後にPointNumだけ近い点を探す
                    var pt = new List<PointD>();

                    if (eachside)//両側でなるべく均等な点数がほしいとき
                    {
                        int i = 1;
                        while (pt.Count < pointNum)
                        {
                            if (position - i + 1 >= 0)
                                pt.Add(Pt[position - i + 1]);
                            if (position + i < Pt.Count)
                                pt.Add(Pt[position + i]);
                            i++;
                        }
                    }
                    else//とにかく近い点を見つけるとき
                    {
                        for (int i = Math.Max(position - pointNum, 0); i < Math.Min(position + pointNum + 1, Pt.Count); i++)
                            pt.Add(Pt[i]);
                        while (pt.Count > pointNum)
                            pt.RemoveAt(Math.Abs(pt[0].X - x[n]) > Math.Abs(pt[^1].X - x[n]) ? 0 : pt.Count - 1);
                    }

                    if (pt.Count < pointNum)
                    {
                        pointNum = pt.Count;
                        if (order > pointNum - 1)
                            order = pointNum - 1;
                    }

                    //計算精度のため、xの範囲を1から+2に変換する 式は X = c1 x + c2;
                    double c1 = 1.0 / (pt[^1].X - pt[0].X);
                    double c2 = 1 - pt[0].X * c1;

                    var m = new DenseMatrix(pointNum, order + 1);
                    var y = new DenseMatrix(pointNum, 1);
                    for (int j = 0; j < pointNum; j++)
                    {
                        y[j, 0] = pt[j].Y;
                        for (int i = 0; i < order + 1; i++)
                            m[j, i] = Math.Pow(c1 * pt[j].X + c2, i);
                    }

                    var a = (m.TransposeThisAndMultiply(m)).Inverse() * m.TransposeThisAndMultiply(y);
                    if (a == null || a.ColumnCount == 0)
                        value[n] = pt[position >= 0 ? position : 0].Y;
                    else
                    {
                        if (shareSameFunction)
                        {
                            for (int i = 0; i < value.Length; i++)
                                for (int j = 0; j < order + 1; j++)
                                    value[i] += a[j, 0] * Math.Pow(c1 * x[i] + c2, j);
                            return value;
                        }
                        value[n] = 0;
                        for (int j = 0; j < order + 1; j++)
                            value[n] += a[j, 0] * Math.Pow(c1 * x[n] + c2, j);
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// Savitzky and Golay Smoothingの結果を返す
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public Profile SmoothingSavitzkyGolay(int m, int n)
        {
            Profile p = Smoothing.SavitzkyGolay(this, m, n);
            p.Color = this.Color;
            p.text = this.text;
            return p;
        }

        /// <summary>
        /// n階微分の結果をかえす
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Profile Differential(int n)
        {
            Profile p = new Profile();
            p.Color = this.Color;
            p.text = this.text;
            if (n < 1)
                return p;
            else
            {
                for (int i = 0; i < Pt.Count - 1; i++)
                {
                    if (double.IsInfinity((Pt[i + 1].Y - Pt[i].Y) / (Pt[i + 1].X - Pt[i].X)) || double.IsNaN((Pt[i + 1].Y - Pt[i].Y) / (Pt[i + 1].X - Pt[i].X)))
                        p.Pt.Add(new PointD((Pt[i].X + Pt[i + 1].X) / 2.0, 0));
                    else
                        p.Pt.Add(new PointD((Pt[i].X + Pt[i + 1].X) / 2.0, (Pt[i + 1].Y - Pt[i].Y) / (Pt[i + 1].X - Pt[i].X)));
                }
                if (n == 1)
                    return p;
                else
                    return p.Differential(n - 1);
            }
        }

        public void Clear()
        {
            Pt = new List<PointD>();
            Err = new List<PointD>();
        }

        public void Sort()
        {
            Pt.Sort();
            Err.Sort();
        }

        public override string ToString()
        {
            if (text == null)
                return "";
            else
                return text.ToString();
        }

        public PointD[][] GetPointsWithinRectangle(RectangleD rect)
        {
            return Geometriy.GetPointsWithinRectangle(this.Pt, rect);
        }
    }

    [Serializable]
    public enum HorizontalAxis
    {
        Angle, d, WaveNumber, Length, EnergyXray, EnergyElectron, EnergyNeutron, NeutronTOF, none
    }

    [Serializable]
    public enum WaveSource
    {
        Xray, Electron, Neutron, None
    }

    [Serializable]
    public enum WaveColor {
        Monochrome, FlatWhite, CustomWhite, None
    }

    [Serializable]
    public enum DiffractionProfileMode
    {
        Concentric, Radial
    }

    [Serializable]
    public enum BackgroundMode
    {
        BSplineCurve, ReferrenceProfile
    }

    

    [Serializable]
    public class DiffractionProfile : ICloneable
    {
        //Masking関連ここから
        [Serializable]
        public class MaskingRange
        {
            public double[] X = new double[2];

            public MaskingRange()
            {
                X[0] = X[1] = 0;
            }

            public MaskingRange(double x1, double x2)
            {
                X[0] = x1;
                X[1] = x2;
            }

            public double Maximum => Math.Max(X[0], X[1]);

            public double Minimum => Math.Min(X[0], X[1]);

            public override string ToString() 
                => X[0] < X[1] ? $"{X[0]:g8} - {X[1]:g8}" : $"{X[1]:g8} - {X[0]:g8}";
        }

        public List<MaskingRange> maskingRanges = new List<MaskingRange>();

        public bool SortMaskRanges()
        {
            bool flag = false;
            for (int i = 0; i < maskingRanges.Count - 1; i++)
                if (Math.Max(maskingRanges[i].X[0], maskingRanges[i].X[1]) > Math.Min(maskingRanges[i + 1].X[0], maskingRanges[i + 1].X[1]))
                    flag = true;

            if (flag)
            {
                List<double> temp = new List<double>();
                for (int i = 0; i < maskingRanges.Count; i++)
                {
                    temp.Add(maskingRanges[i].X[0]);
                    temp.Add(maskingRanges[i].X[1]);
                }
                temp.Sort();
                for (int i = 0; i < maskingRanges.Count; i++)
                {
                    if (maskingRanges[i].X[0] != temp[i * 2]) maskingRanges[i].X[0] = temp[i * 2];
                    if (maskingRanges[i].X[1] != temp[i * 2 + 1]) maskingRanges[i].X[1] = temp[i * 2 + 1];
                }
            }
            return flag;
        }

        public void SetMaskRange(int[] index, double x)
        {
            if (index != null && index.Length == 2 && index[0] >= 0 && index[0] < maskingRanges.Count && index[1] >= 0 && index[1] < 2)
                maskingRanges[index[0]].X[index[1]] = x;
        }

        public void DeleteMaskRange(int index)
        {
            if (maskingRanges.Count < index)
                maskingRanges.RemoveAt(index);
        }

        private int interpolationOrder = 2;

        public int InterpolationOrder
        {
            set
            {
                if (value > 0)
                {
                    interpolationOrder = value;
                }
            }
            get => interpolationOrder;
        }

        private int interpolationPoints = 20;

        public int InterpolationPoints
        {
            set
            {
                if (value > 0)
                {
                    interpolationPoints = value;
                }
            }
            get => interpolationPoints;
        }

        public bool DoesMaskAndInterpolate = false;
        //Masking関連ここまで

        public object Clone()
        {
            DiffractionProfile dp = (DiffractionProfile)this.MemberwiseClone();
            dp.OriginalProfile = (Profile)this.OriginalProfile.Clone();
            dp.Profile = (Profile)this.Profile.Clone();
            dp.InterpolatedProfile = (Profile)this.InterpolatedProfile.Clone();
            dp.SmoothedProfile = (Profile)this.SmoothedProfile.Clone();
            dp.Kalpha2RemovedProfile = (Profile)this.Kalpha2RemovedProfile.Clone();
            dp.ConvertedProfile = (Profile)this.ConvertedProfile.Clone();
            dp.BackgroundProfile = (Profile)this.BackgroundProfile.Clone();
            return dp;
        }

        public Profile OriginalProfile;//ソースのプロファイル

        public PointD[] BgPoints;

        [XmlIgnoreAttribute]
        public Profile ConvertedProfile;//軸変換後のプロファイル

        [XmlIgnoreAttribute]
        public Profile InterpolatedProfile;//

        [XmlIgnoreAttribute]
        public Profile SmoothedProfile;//

        [XmlIgnoreAttribute]
        public Profile Kalpha2RemovedProfile;//

        [XmlIgnoreAttribute]
        public Profile BackgroundProfile;

        [XmlIgnoreAttribute]
        public Profile Profile;

        public HorizontalAxis SrcAxisMode;
        public double SrcWaveLength;
        public double SrcTakeoffAngle;

        public double SrcTofAngle;
        public double SrcTofLength;

        /// <summary>
        /// プロファイルモード
        /// </summary>
        public DiffractionProfileMode Mode = DiffractionProfileMode.Concentric;

        public HorizontalAxis DestAxisMode;
        public double DestWaveLength;
        public double DestTakeoffAngle;
        public double DestTofAngle;
        public double DestTofLength;

        //ノーマライズ関連ここから
        /// <summary>
        /// 強度のノーマライズをするかどうか
        /// </summary>
        public bool DoesNormarizeIntensity = false;

        /// <summary>
        /// 縦軸にかける係数
        /// </summary>
        public double NormarizeRangeStart = 0;

        public double NormarizeRangeEnd = 180;
        public bool NormarizeAsAverage = true;
        public double NormarizeIntensity = 1000;

        //スムージング関連ここから
        public bool DoesSmoothing = false;

        public int SazitkyGorayM = 3, SazitkyGorayN = 3;

        //2θシフトここから
        public bool DoesTwoThetaOffset = false;

        public double TwoThetaOffsetCoeff0 = 0, TwoThetaOffsetCoeff1 = 0, TwoThetaOffsetCoeff2 = 0;

        //Kalpha2除去ここから
        public bool DoesRemoveKalpha2 = false;

        public double Kalpha1 = 0, Kalpha2 = 0;

        //shift関連
        public bool IsShiftX = false;

        public double ShiftX = 0;

        //FFT関連
        public bool DoesBandpassFilter = false;

        public bool DoesLowPath = false, DoesHighPath = false;
        public double LowPathLimit = double.NaN, HighPathLimit = double.NaN;

        /// <summary>
        /// count per second モードかどうか
        /// </summary>
        public bool IsCPS = true;

        /// <summary>
        /// 露出時間
        /// </summary>
        public double ExposureTime = 1;

        /// <summary>
        /// 縦軸をログスケールにするかどうか
        /// </summary>
        public bool IsLogIntensity = false;

        //描画線の設定
        public float LineWidth = 1f;

        public int? ColorARGB;

        //生データの波の種類、波長など
        public WaveSource WaveSource;

        public WaveColor WaveColor;
        public int XrayElementNumber;
        public XrayLine XrayLine;
        public double ElectronAccVolatage;

        //バックグランド設定関連
        public int BgPointsNumber = 15;

        public bool SubtractBackground = false;
        public BackgroundMode BgMode = BackgroundMode.BSplineCurve;
        public Profile BackgroundReferrenceProfile = null;
        public double BackgroundReferrenceScale = 1;

        public string Name;

        public string Comment { get; set; }

        public bool IsLPOmain = false;
        public bool IsLPOchild = false;

        public double[] ImageArray = null;
        public double ImageScale = 0;
        public int ImageWidth = 0;
        public int ImageHeight = 0;

        public override string ToString() => Name.ToString();

        public DiffractionProfile()
        {
            BgPoints = Array.Empty<PointD>();

            OriginalProfile = new Profile();
            ConvertedProfile = new Profile();
            SmoothedProfile = new Profile();
            Kalpha2RemovedProfile = new Profile();
            InterpolatedProfile = new Profile();
            Profile = new Profile();
            BackgroundProfile = new Profile();
            SrcAxisMode = HorizontalAxis.Angle;

            WaveSource = WaveSource.Xray;

            XrayElementNumber = 0;
            XrayLine = XrayLine.Ka1;

            ElectronAccVolatage = 200;

            ColorARGB = null;
        }

               public void CopyParameter(DiffractionProfile defaultDP)
        {
            DoesSmoothing = defaultDP.DoesSmoothing;

            SazitkyGorayM = defaultDP.SazitkyGorayM;
            SazitkyGorayN = defaultDP.SazitkyGorayN;

            SubtractBackground = defaultDP.SubtractBackground;
        }

        /// <summary>
        /// クライアント座標のptを受取り、オリジナル座標に変換して追加する
        /// </summary>
        /// <param name="pt"></param>
        public void AddBgPoints(PointD pt)
        {
            Profile profile = new Profile();
            for (int i = 0; i < BgPoints.Length; i++)
                profile.Pt.Add(BgPoints[i]);

            PointD newPt = ConvertDestToSrc(pt);
            if (!double.IsNaN(newPt.X) && !double.IsInfinity(newPt.X))
                profile.Pt.Add(newPt);
            profile.Sort();
            BgPoints = profile.Pt.ToArray();
        }

        public void DeleteBgPoints(int index)
        {
            Profile profile = new Profile();
            for (int i = 0; i < BgPoints.Length; i++)
                profile.Pt.Add(BgPoints[i]);
            profile.Pt.RemoveAt(index);
            BgPoints = profile.Pt.ToArray();
        }

        /// <summary>
        /// Srcの横軸をDestの横軸に変換する
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>

        private PointD convertSrcToDest(PointD pt)
        {
            double x =
                HorizontalAxisConverter.Convert(pt.X, SrcAxisMode, SrcWaveLength, SrcTakeoffAngle, SrcTofAngle, SrcTofLength, DestAxisMode, DestWaveLength, DestTakeoffAngle, DestTofAngle, DestTofLength);
            double y = pt.Y;
            if (IsCPS && ExposureTime > 0)
                y = y / ExposureTime;
            if (IsLogIntensity)
                y = Math.Log10(y);

            return new PointD(x, y);
        }

        public PointD[] ConvertSrcToDest(PointD[] pt)
        {
            List<PointD> dest = new List<PointD>();
            for (int i = 0; i < pt.Length; i++)
            {
                PointD p = convertSrcToDest(pt[i]);
                if (!double.IsNaN(p.X) && !double.IsInfinity(p.X) && !double.IsNaN(p.Y) && !double.IsInfinity(p.Y))
                    dest.Add(p);
            }
            //強度のノーマライズ　stepが一定値でなくなった時の対応

            /*
            if (dest.Count> 0)
            {
                List<double> steps = new List<double>();
                for (int i = 0; i < dest.Count - 1; i++)
                    steps.Add(Math.Abs(dest[i].X - dest[i + 1].X));
                steps.Add(steps[steps.Count - 1]);
                double average = steps.Average();
                for (int i = 0; i < dest.Count; i++)
                    dest[i].Y *= average / steps[i];
            }
            */
            return dest.ToArray();
        }

        public PointD ConvertDestToSrc(PointD pt)
        {
            double x =
                HorizontalAxisConverter.Convert(pt.X, DestAxisMode, DestWaveLength, DestTakeoffAngle, DestTofAngle, DestTofLength, SrcAxisMode, SrcWaveLength, SrcTakeoffAngle, SrcTofAngle, SrcTofLength);
            double y = pt.Y;
            if (IsCPS && ExposureTime != 0)
                y *= ExposureTime;
            if (IsLogIntensity)
                y = Math.Pow(10, y);
            return new PointD(x, y);
        }

        public PointD[] ConvertDestToSrc(PointD[] pt)
        {
            List<PointD> dest = new List<PointD>();
            for (int i = 0; i < pt.Length; i++)
            {
                PointD p = ConvertDestToSrc(pt[i]);
                if (!double.IsNaN(p.X) && !double.IsInfinity(p.X) && !double.IsNaN(p.Y) && !double.IsInfinity(p.Y))
                    dest.Add(p);
            }
            return dest.ToArray();
        }

        /// <summary>
        /// 横軸条件はそのままで、縦軸ノーマライズを施しOriginalProfileからConvertedProfileを生成する。
        /// </summary>
        public void SetConvertedProfile()
        {
            SetConvertedProfile(SrcAxisMode, SrcWaveLength, SrcTakeoffAngle, SrcTofAngle, SrcTofLength);
        }

        /// <summary>
        /// 横軸変換および縦軸ノーマライズを施しOriginalProfileからConvertedProfileを生成する。
        /// </summary>
        public void SetConvertedProfile(HorizontalAxis destAxisMode, double destWavelength, double destTakeoffAngle, double destTofAngle, double destTofLength)
        {
            DestAxisMode = destAxisMode;
            DestWaveLength = destWavelength;
            DestTakeoffAngle = destTakeoffAngle;
            DestTofAngle = destTofAngle;
            DestTofLength = destTofLength;

            ConvertedProfile.Clear();

            PointD[] pt = ConvertSrcToDest(OriginalProfile.Pt.ToArray());
            PointD[] err = ConvertSrcToDest(OriginalProfile.Err.ToArray());

            //ここから、2ThetaOffset処理
            if (IsShiftX)
            {
                for (int i = 0; i < pt.Length; i++)
                    pt[i].X += ShiftX;
                for (int i = 0; i < err.Length; i++)
                    err[i].X += ShiftX;
            }

            if (DoesTwoThetaOffset && destAxisMode == HorizontalAxis.Angle)
            {
                for (int i = 0; i < pt.Length; i++)
                {
                    var tan = Math.Tan(pt[i].X / 360.0 * Math.PI);
                    pt[i].X += TwoThetaOffsetCoeff0 + TwoThetaOffsetCoeff1 * tan + TwoThetaOffsetCoeff2 * tan * tan;
                }
                for (int i = 0; i < err.Length; i++)
                {
                    var tan = Math.Tan(err[i].X / 360.0 * Math.PI);
                    err[i].X += TwoThetaOffsetCoeff0 + TwoThetaOffsetCoeff1 * tan + TwoThetaOffsetCoeff2 * tan * tan;
                }
            }

            //ここまで

            ConvertedProfile.Pt.AddRange(pt);
            ConvertedProfile.Err.AddRange(err);

            ConvertedProfile.Sort();//ここまでプロファイルソートを完了

            SetMaskingProfile();
        }

        /// <summary>
        /// Maskされた領域を補完し、ConvertedProfileからInterpolatedProfileを作成する
        /// </summary>
        public void SetMaskingProfile()
        {
            InterpolatedProfile.Clear();
            for (int i = 0; i < ConvertedProfile.Pt.Count; i++)
                InterpolatedProfile.Pt.Add(ConvertedProfile.Pt[i]);
            for (int i = 0; i < ConvertedProfile.Err.Count; i++)
                InterpolatedProfile.Err.Add(ConvertedProfile.Err[i]);

            if (DoesMaskAndInterpolate && maskingRanges.Count > 0)
            {
                var x = new List<List<double>>();
                for (int i = 0; i < maskingRanges.Count; i++)
                {
                    x.Add(new List<double>());
                    for (int j = 0; j < InterpolatedProfile.Pt.Count && InterpolatedProfile.Pt[j].X < maskingRanges[i].Maximum; j++)
                        if (InterpolatedProfile.Pt[j].X > maskingRanges[i].Minimum)
                        {
                            x[i].Add(InterpolatedProfile.Pt[j].X);
                            InterpolatedProfile.Pt.RemoveAt(j);
                            j--;
                        }
                }
                var y = new List<List<double>>();
                for (int i = 0; i < x.Count; i++)
                {
                    y.Add(new List<double>());
                    y[i].AddRange(InterpolatedProfile.GetValues(x[i].ToArray(), InterpolationPoints * 2, InterpolationOrder, true, true));
                }
                for (int i = 0; i < x.Count; i++)
                    for (int j = 0; j < x[i].Count; j++)
                        InterpolatedProfile.Pt.Add(new PointD(x[i][j], y[i][j]));
                InterpolatedProfile.Sort();
            }
            SetSmoothingProfile();
        }

        /// <summary>
        /// スムージングとFFTを施しInterpolatedProfileからSmoothedProfileを作成する。
        /// </summary>
        public void SetSmoothingProfile()
        {
            SmoothedProfile.Clear();
            //スムージング処理
            if (DoesSmoothing)
                SmoothedProfile = Smoothing.SavitzkyGolay(InterpolatedProfile, SazitkyGorayM, SazitkyGorayN);
            else
                for (int i = 0; i < InterpolatedProfile.Pt.Count; i++)
                    SmoothedProfile.Pt.Add(InterpolatedProfile.Pt[i]);

            for (int i = 0; i < InterpolatedProfile.Err.Count; i++)
                SmoothedProfile.Err.Add(InterpolatedProfile.Err[i]);

            if (DoesBandpassFilter && SmoothedProfile.Pt.Count > 2)
            {
                Complex[] src = new Complex[SmoothedProfile.Pt.Count];
                for (int i = 0; i < src.Length; i++)
                    src[i] = new Complex(SmoothedProfile.Pt[i].Y, 0);

                Complex[] fft = Fourier.FFT(src, FourierDirectionEnum.Forward);
                double center = fft.Length / 2.0;
                double unit = SmoothedProfile.Pt[1].X - SmoothedProfile.Pt[0].X;

                if (DoesHighPath && HighPathLimit > 0)//特定の周波数以下をカットする
                {
                    double length = HighPathLimit * src.Length * unit;
                    for (int i = 1; i < center; i++)
                        if (i < length / 2)
                            fft[i] *= 0;
                        else if (i < length)
                            fft[i] *= (i - length / 2) / (length / 2);
                }

                if (DoesLowPath && LowPathLimit > 0)//特定の周波数以上をカットする
                {
                    double length = LowPathLimit * src.Length * unit;
                    for (int i = 0; i < center; i++)
                        if (i > length * 2)
                            fft[i] *= 0;
                        else if (i > length)
                            fft[i] *= 1 - (i - length) / length;
                }

                for (int i = (int)(center + 1); i < src.Length; i++)
                {
                    fft[i] = Complex.Conjugate(fft[src.Length - i]);
                }
                src = Fourier.FFT(fft, FourierDirectionEnum.Inverse);

                for (int i = 1; i < src.Length; i++)
                {
                    // SmoothedProfile.Pt[i].Y = src[i].Magnitude;
                    SmoothedProfile.Pt[i] = new PointD(SmoothedProfile.Pt[i].X, src[i].Magnitude);
                }
            }
            SetKalpha2RemovedProfile();
        }

        /// <summary>
        /// Kalpha2を除去する
        /// </summary>
        public void SetKalpha2RemovedProfile()
        {
            Kalpha2RemovedProfile.Clear();

            for (int i = 0; i < SmoothedProfile.Pt.Count; i++)
                Kalpha2RemovedProfile.Pt.Add(SmoothedProfile.Pt[i]);
            for (int i = 0; i < SmoothedProfile.Err.Count; i++)
                Kalpha2RemovedProfile.Err.Add(SmoothedProfile.Err[i]);

            if (DoesRemoveKalpha2 && WaveSource == WaveSource.Xray && XrayElementNumber != 0 && XrayLine == XrayLine.Ka1)
            {
                double alpha1 = AtomConstants.CharacteristicXrayWavelength(XrayElementNumber, Crystallography.XrayLine.Ka1);
                double alpha2 = AtomConstants.CharacteristicXrayWavelength(XrayElementNumber, Crystallography.XrayLine.Ka2);
                double startY = Kalpha2RemovedProfile.Pt[0].Y * 2 / 3;

                List<double> theta = new List<double>();
                for (int i = 0; i < Kalpha2RemovedProfile.Pt.Count; i++)
                {
                    double d = alpha2 * 0.5 / Math.Sin(Kalpha2RemovedProfile.Pt[i].X / 360 * Math.PI);
                    theta.Add(Math.Asin(alpha1 * 0.5 / d) * 360 / Math.PI);
                }
                for (int i = 0; i < Kalpha2RemovedProfile.Pt.Count; i++)
                {
                    if (theta[i] < Kalpha2RemovedProfile.Pt[0].X)
                        //Kalpha2RemovedProfile.Pt[i].Y -= startY * 0.5;
                        Kalpha2RemovedProfile.Pt[i] = new PointD(Kalpha2RemovedProfile.Pt[i].X, Kalpha2RemovedProfile.Pt[i].Y - startY * 0.5);
                    else
                        //    Kalpha2RemovedProfile.Pt[i].Y -= Kalpha2RemovedProfile.GetValue(theta[i], 2, 1) * 0.5;
                        Kalpha2RemovedProfile.Pt[i] = new PointD(Kalpha2RemovedProfile.Pt[i].X, Kalpha2RemovedProfile.Pt[i].Y - Kalpha2RemovedProfile.GetValue(theta[i], 2, 1) * 0.5);
                }
            }

            SetBackGroundProfile();
        }

        /// <summary>
        /// バックグラウンド処理を施しSmoothedProfileからBackGroundProfileおよびProfileを作成する。
        /// </summary>
        public void SetBackGroundProfile()
        {
            Profile.Clear();
            for (int i = 0; i < Kalpha2RemovedProfile.Pt.Count; i++)
                Profile.Pt.Add(Kalpha2RemovedProfile.Pt[i]);
            for (int i = 0; i < Kalpha2RemovedProfile.Err.Count; i++)
                Profile.Err.Add(Kalpha2RemovedProfile.Err[i]);

            //必要であれば、ノーマライズ処理
            Normarize();

            //バックグラウンド処理
            if (BackgroundProfile.Pt == null || BackgroundProfile.Pt.Count != Profile.Pt.Count)
            {
                BackgroundProfile.Pt = new List<PointD>();
                for (int i = 0; i < Profile.Pt.Count; i++)
                    BackgroundProfile.Pt.Add(new PointD(Profile.Pt[i].X, 0));
            }
            if (SubtractBackground)
            {
                if (BgMode == BackgroundMode.BSplineCurve && BgPoints != null)
                {
                    BackgroundProfile = Spline.GetSpline(ConvertSrcToDest(BgPoints), ConvertedProfile);
                    for (int i = 0; i < Profile.Pt.Count; i++)
                        Profile.Pt[i] = new PointD(Profile.Pt[i].X, Profile.Pt[i].Y - BackgroundProfile.Pt[i].Y);
                }
                else if (BgMode == BackgroundMode.ReferrenceProfile && BackgroundReferrenceProfile != null)
                {
                    for (int i = 0; i < Profile.Pt.Count; i++)
                    {
                        double x = Profile.Pt[i].X;
                        double y = BackgroundReferrenceProfile.GetValue(x, 1, 0) * BackgroundReferrenceScale;
                        BackgroundProfile.Pt[i] = new PointD(x, y);
                        Profile.Pt[i] = new PointD(x, Profile.Pt[i].Y - y);
                    }
                }
            }

            //バックグラウンド処理ここまで
        }

        private void Normarize()
        {
            if (DoesNormarizeIntensity)
            {
                double temp = NormarizeAsAverage ? 0 : double.NegativeInfinity;
                int count = 0;
                for (int i = 0; i < Profile.Pt.Count && Profile.Pt[i].X < NormarizeRangeEnd; i++)
                {
                    if (Profile.Pt[i].X > NormarizeRangeStart)
                    {
                        if (NormarizeAsAverage)
                            temp += Profile.Pt[i].Y;
                        else if (temp < Profile.Pt[i].Y)
                            temp = Profile.Pt[i].Y;
                        count++;
                    }
                }
                if (double.IsNegativeInfinity(temp) || count == 0) return;

                double factor = NormarizeAsAverage ? NormarizeIntensity / (temp / count) : NormarizeIntensity / temp;

                for (int i = 0; i < Profile.Pt.Count; i++)
                    //Profile.Pt[i].Y *= factor;
                    Profile.Pt[i] = new PointD(Profile.Pt[i].X, Profile.Pt[i].Y * factor);
                for (int i = 0; i < Profile.Err.Count; i++)
                    //Profile.Err[i].Y *= factor;
                    Profile.Err[i] = new PointD(Profile.Err[i].X, Profile.Err[i].Y * factor);
            }
        }

        /// <summary>
        /// SmoothProfileを対象にBgPointsを自動で探す
        /// </summary>
        public void getBgPointsAuto()
        {
            int i;
            Profile p0 = Smoothing.SavitzkyGolay(SmoothedProfile, 3, 3);

            //一階微分
            Profile p1 = new Profile();
            for (i = 0; i < p0.Pt.Count - 1; i++)
                p1.Pt.Add(new PointD((p0.Pt[i].X + p0.Pt[i + 1].X) / 2, (p0.Pt[i + 1].Y - p0.Pt[i].Y) / (p0.Pt[i + 1].X - p0.Pt[i].X)));
            p1 = Smoothing.SavitzkyGolay(p1, 3, 3);

            //二階微分
            Profile p2 = new Profile();
            List<double> y = new List<double>();
            for (i = 0; i < p1.Pt.Count - 1; i++)
                p2.Pt.Add(new PointD((p1.Pt[i].X + p1.Pt[i + 1].X) / 2, (p1.Pt[i + 1].Y - p1.Pt[i].Y) / (p1.Pt[i + 1].X - p1.Pt[i].X)));
            p2 = Smoothing.SavitzkyGolay(p2, 3, 3);

            for (i = 0; i < p2.Pt.Count; i++)
                y.Add(p2.Pt[i].Y);

            //p2のY値を規格化
            double d = Statistics.Deviation(y.ToArray());
            for (i = 0; i < p2.Pt.Count; i++)
                y[i] /= d;

            //30ポイント以上小さな値が続いて、かつ前後100ポイント以上はなれているポイントをさがす、
            double temp = 1;
            double tempMax = double.PositiveInfinity;
            int tempXindex = 0;
            List<double> X = new List<double>();
            List<int> Xindex = new List<int>();
            bool flag = true;
            int range = (int)(p1.Pt.Count / 3.0 / BgPointsNumber);
            do
            {
                tempMax = double.PositiveInfinity;
                for (i = 0; i < p2.Pt.Count - 30; i++)
                {
                    temp = 0;
                    flag = true;
                    //もしrangeの範囲に既にXindexがあったらスキップする
                    foreach (int xindex in Xindex)
                        if (Math.Abs(i - xindex) < range)
                        {
                            i = xindex + range;
                            flag = false;
                            break;
                        }
                    if (flag)
                    {
                        for (int j = 0; j < 30; j++)
                            temp += Math.Abs(y[i + j]);
                        if (temp < tempMax && temp < 2)
                        {
                            tempMax = temp;
                            tempXindex = i + 15;
                        }
                    }
                }
                if (tempMax < 10)
                {
                    X.Add(p2.Pt[tempXindex].X);
                    Xindex.Add(tempXindex);
                }
            } while (X.Count < BgPointsNumber && tempMax < 10);

            if (X.Count < 3)
                return;
            else//３点以上見つけられたら
            {
                List<PointD> bgPoints = new List<PointD>();
                X.Sort();
                double tempY;
                for (i = 0; i < X.Count; i++)
                {
                    tempMax = double.PositiveInfinity;
                    temp = 0;
                    //まずその点に最も近いp0中の点をさがす
                    int k = 0;
                    for (int j = 0; j < p0.Pt.Count; j++)
                    {
                        temp = Math.Abs(p0.Pt[j].X - X[i]);
                        if (temp < tempMax)
                        {
                            tempMax = temp;
                            k = j;
                        }
                    }
                    if (k != 0)//見つけた点の周り5ポイントの平均をバックグラウンドとする
                    {
                        tempY = 0;
                        for (int l = k - 5; l <= k + 5 && l < p0.Pt.Count; l++)
                            tempY += p0.Pt[l].Y / 11.0;
                        bgPoints.Add(new PointD(p0.Pt[k].X, tempY));
                    }
                }

                double minimum = double.PositiveInfinity;
                for (i = 0; i < bgPoints.Count; i++)
                    if (minimum > bgPoints[i].Y)
                        minimum = bgPoints[i].Y;

                BgPoints = ConvertDestToSrc(bgPoints.ToArray());
            }
        }

        public void RemoveKalpha2()
        {
            double alpha1 = AtomConstants.CharacteristicXrayWavelength(XrayElementNumber, Crystallography.XrayLine.Ka1);
            double alpha2 = AtomConstants.CharacteristicXrayWavelength(XrayElementNumber, Crystallography.XrayLine.Ka2);
            double startY = OriginalProfile.Pt[0].Y * 2 / 3;
            for (int i = 0; i < OriginalProfile.Pt.Count; i++)
            {
                double d = alpha2 / 2 / Math.Sin(OriginalProfile.Pt[i].X / 360 * Math.PI);
                double theta = Math.Asin(alpha1 / 2 / d) * 360 / Math.PI;
                if (theta < OriginalProfile.Pt[0].X)
                    //OriginalProfile.Pt[i].Y -= startY * 0.5;
                    OriginalProfile.Pt[i] -= new PointD(0, startY * 0.5);

                if (theta > OriginalProfile.Pt[0].X)
                    //OriginalProfile.Pt[i].Y -= OriginalProfile.GetValue(theta, 2, 1) * 0.5;
                    OriginalProfile.Pt[i] -= new PointD(0, OriginalProfile.GetValue(theta, 2, 1) * 0.5);
            }
        }

        /// <summary>
        /// Ka2を除去する静的メソッド。
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="alpha1"></param>
        /// <param name="alpha2"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static Profile RemoveKalpha2(Profile profile, double alpha1, double alpha2, double ratio)
        {
            var targetProfile = new Profile();
            for (int i = 0; i < profile.Pt.Count; i++)
                targetProfile.Pt.Add(new PointD(profile.Pt[i].X, profile.Pt[i].Y));

            double startY = profile.Pt[0].Y * 2 / 3;

            for (int i = 0; i < profile.Pt.Count; i++)
            {
                double d = alpha2 / 2 / Math.Sin(profile.Pt[i].X / 360 * Math.PI);
                double theta = Math.Asin(alpha1 / 2 / d) * 360 / Math.PI;

                if (theta < profile.Pt[0].X)
                    //targetProfile.Pt[i].Y -= startY * ratio;
                    targetProfile.Pt[i] -= new PointD(0, startY * ratio);

                if (theta > targetProfile.Pt[0].X)
                    //targetProfile.Pt[i].Y -= targetProfile.GetValue(theta, 2, 1) * ratio;
                    targetProfile.Pt[i] -= new PointD(0, targetProfile.GetValue(theta, 2, 1) * ratio);
            }

            return targetProfile;
        }
    }

    public static class HorizontalAxisConverter
    {
        #region 横軸を変換するメソッド群

        public static double Convert(
            double x, HorizontalAxis srcAxisMode, double srcWavelength, double srcTakeoffAngle, double srcTofAngle, double srcTofLength,
            HorizontalAxis destAxisMode, double destWavelength, double destTakeoffAngle, double destTofAngle, double destTofLength)
        {
            //入出力が同じだったらそのまま
            if (
                (srcAxisMode == HorizontalAxis.Angle && destAxisMode == HorizontalAxis.Angle && srcWavelength == destWavelength) ||
                (srcAxisMode == HorizontalAxis.d && destAxisMode == HorizontalAxis.d) ||
                (srcAxisMode == HorizontalAxis.EnergyXray && destAxisMode == HorizontalAxis.EnergyXray && destTakeoffAngle == srcTakeoffAngle) ||
                (srcAxisMode == HorizontalAxis.NeutronTOF && destAxisMode == HorizontalAxis.NeutronTOF && srcTofAngle == destTofAngle && srcTofLength == destTofLength) ||
                (srcAxisMode == HorizontalAxis.WaveNumber && destAxisMode == HorizontalAxis.WaveNumber)
                )
                return x;

            //一旦すべてをd値に変換
            double d = x;
            if (srcAxisMode == HorizontalAxis.Angle) d = TwoThetaToD(x / 180 * Math.PI, srcWavelength);
            else if (srcAxisMode == HorizontalAxis.EnergyXray) d = XrayEnergyToD(x, srcTakeoffAngle);
            else if (srcAxisMode == HorizontalAxis.NeutronTOF) d = NeutronTofToD(x, srcTofAngle, srcTofLength);
            else if (srcAxisMode == HorizontalAxis.WaveNumber) d = WaveNumberToD(x);

            if (destAxisMode == HorizontalAxis.Angle)
                return DToTwoTheta(d, destWavelength) / Math.PI * 180.0;
            else if (destAxisMode == HorizontalAxis.d)
                return d * 10;
            else if (destAxisMode == HorizontalAxis.EnergyXray)
                return DToXrayEnergy(d, destTakeoffAngle);
            else if (destAxisMode == HorizontalAxis.EnergyElectron)
                return DToElectronEnergy(d, destTakeoffAngle);
            else if (destAxisMode == HorizontalAxis.NeutronTOF)
                return DToTOF(d, destTofAngle, destTofLength);
            else if (destAxisMode == HorizontalAxis.WaveNumber)
                return DToWaveNumber(d) / 10.0;
            else
                return double.NaN;
        }

        /// <summary>
        /// d -> Wavenumber  d値(nm)を与えると、波数(1/nm)を返す
        /// </summary>
        /// <param name="d"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double DToWaveNumber(double d)
        {
            return Math.PI * 2 / d;
        }

        /// <summary>
        /// Wavenumber -> d   波数(1/nm)を与えると、仮想的なd値(nm)を返す
        /// </summary>
        /// <param name="d"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double WaveNumberToD(double wavenumber)
        {
            return Math.PI * 2 / wavenumber;
        }

        /// <summary>
        /// TOF -> d   TOF(μs)と取り出し角(radian)、距離(m)を与えると、ブラッグ条件を満たすd値(nm)を返す
        /// </summary>
        /// <param name="d"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double NeutronTofToD(double TOF, double angle, double length)
        {
            return UniversalConstants.Convert.NeutronVelocityToWavelength(length / TOF) / Math.Sin(angle / 2) / 2.0;
        }

        /// <summary>
        /// d -> TOF   d値(nm)と仮想的な取り出し角(radian)、距離(m)を与えると、ブラッグ条件を満たす仮想的なTOF(μs)を返す
        /// </summary>
        /// <param name="d"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double DToTOF(double d, double angle, double length)
        {
            return length / UniversalConstants.Convert.WavelengthToNeutronVelocity(2.0 * d * Math.Sin(angle / 2));
        }

        /// <summary>
        /// d -> E   面間隔d(nm)と仮想的な取り出し角(2Θ)を与えるとブラッグ条件を満たす仮想的な電磁波のエネルギー(eV)を返す
        /// </summary>
        /// <param name="d"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double DToXrayEnergy(double d, double takeoffAngle)
        {
            return UniversalConstants.Convert.WavelengthToXrayEnergy(2.0 * d * Math.Sin(takeoffAngle / 2.0));
        }

        /// <summary>
        /// d -> E   面間隔d(nm)と仮想的な取り出し角(2Θ)を与えるとブラッグ条件を満たす仮想的な電子のエネルギー(keV)を返す
        /// </summary>
        /// <param name="d"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double DToElectronEnergy(double d, double takeoffAngle)
        {
            return UniversalConstants.Convert.WaveLengthToElectronEnergy(2.0 * d * Math.Sin(takeoffAngle / 2.0))*1000;
        }

        /// <summary>
        /// d -> E   面間隔d(nm)と仮想的な取り出し角(2Θ)を与えるとブラッグ条件を満たす仮想的な中性子のエネルギー(eV)を返す
        /// </summary>
        /// <param name="d"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double DToNeutronEnergy(double d, double takeoffAngle)
        {
            return UniversalConstants.Convert.WaveLengthToNeutronEnergy(2.0 * d * Math.Sin(takeoffAngle / 2.0));
        }

        /// <summary>
        /// E -> d  電磁波のエネルギー(eV)と取り出し角を与えるとブラッグ条件を満たす面間隔d(nm)の値を返す
        /// </summary>
        /// <param name="energy"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double XrayEnergyToD(double energy, double takeoffAngle)
        {
            return UniversalConstants.Convert.EnergyToXrayWaveLength(energy) / 2.0 / Math.Sin(takeoffAngle / 2.0);
        }

        /// <summary>
        /// E -> d  電子のエネルギー(eV)と取り出し角を与えるとブラッグ条件を満たす面間隔d(nm)の値を返す
        /// </summary>
        /// <param name="energy"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double ElectronEnergyToD(double energy, double takeoffAngle)
        {
            return UniversalConstants.Convert.EnergyToElectronWaveLength(energy/1000) / 2.0 / Math.Sin(takeoffAngle / 2.0);
        }

        /// <summary>
        /// E -> d  中性子のエネルギー(eV)と取り出し角を与えるとブラッグ条件を満たす面間隔d(nm)の値を返す
        /// </summary>
        /// <param name="energy"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double NeutronEnergyToD(double energy, double takeoffAngle)
        {
            return UniversalConstants.Convert.EnergyToNeutronWaveLength(energy) / 2.0 / Math.Sin(takeoffAngle / 2.0);
        }

        /// <summary>
        /// d -> 2θ 面間隔d(nm)と仮想的な入射線の波長(nm)を与えるとブラッグ条件を満たす仮想的な回折角(2θ)を返す
        /// </summary>
        /// <param name="wavelength"></param>
        /// <returns></returns>
        public static double DToTwoTheta(double d, double wavelength)
        {
            return 2 * Math.Asin(wavelength / 2.0 / d);
        }

        /// <summary>
        /// E -> 2θ  電磁波のエネルギー(eV)、取り出し角(takeoffAngle)、仮想的な入射線の波長(nm)を与えると仮想的な回折角を返す
        /// </summary>
        /// <param name="energy"></param>
        /// <param name="waveLength"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double XrayEnergyToTwoTheta(double energy, double takeoffAngle, double waveLength)
        {
            return 2 * Math.Asin(waveLength / 2.0 / XrayEnergyToD(energy, takeoffAngle));
        }

        /// <summary>
        ///  2θ -> E  ブラッグ角と、入射線波長(nm)、仮想的なEDX取り出し角(takeoffAngle)からブラッグ条件を満たす仮想的な電磁波波長(nm)をかえす
        /// </summary>
        /// <param name="twoTheta"></param>
        /// <param name="waveLength"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double TwoThetaToXrayEnergy(double twoTheta, double waveLength, double takeoffAngle)
        {
            return DToXrayEnergy(waveLength / 2.0 / Math.Sin(twoTheta / 2.0), takeoffAngle);
        }

        /// <summary>
        /// 2θ -> d   ブラッグ角と入射線波長からブラッグ条件を満たす面間隔d(nm)を返す
        /// </summary>
        /// <param name="twoTheta"></param>
        /// <param name="waveLength"></param>
        /// <returns></returns>
        public static double TwoThetaToD(double twoTheta, double waveLength)
        {
            return waveLength / Math.Sin(twoTheta / 2.0) / 2.0;
        }

        #endregion 横軸を変換するメソッド群
    }
}
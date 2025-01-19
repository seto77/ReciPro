using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Crystallography;

[Serializable]
public class Profile : ICloneable
{
    #region プロパティ、フィールド
    public string text;

    public List<PointD> Pt;
    public List<PointD> Err;

    public Color Color = Color.Blue;

    public float LineWidth = 1f;

    public RectangleD Range => new RectangleD(new PointD(Pt.Min(p => p.X), Pt.Min(p => p.Y)), new PointD(Pt.Max(p => p.X), Pt.Max(p => p.Y)));
    public double MaxX => Pt.Max(p => p.X);
    public double MinX => Pt.Min(p => p.X);
    public double MaxY => Pt.Max(p => p.Y);
    public double MinY => Pt.Min(p => p.Y);

    #endregion

    #region コンストラクタ
    public Profile()
    {
        Pt = [];
        Err = [];
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
    #endregion

    #region クリア、ソート、コピー
    public void Clear()
    {
        Pt = [];
        Err = [];
    }

    public void Sort()
    {
        Pt.Sort();
        Err.Sort();
    }
    public object Clone()
    {
        Profile p = (Profile)this.MemberwiseClone();
        p.Pt = new List<PointD>([.. Pt]);
        return p;
    }
    public Profile CopyTo()
    {
        Profile p = new Profile();

        p.Pt = [];
        p.Err = [];
        for (int i = 0; i < Pt.Count; i++)
            p.Pt.Add(Pt[i]);

        for (int i = 0; i < Err.Count; i++)
            p.Err.Add(Err[i]);

        p.text = text;
        return p;
    }
    #endregion

    #region 各種メソッド

    public override string ToString() => text == null ? "" : text.ToString();

    #region GetErr 指定されたxの値に対するerrを返す
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
    #endregion

    #region  GetValue 指定されたxの値に対してorder次関数で補間した値を返す
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

        return GetValues([x], pointNum, order, eachside, false)[0];
    }
    #endregion

    #region GetValues　指定されたxの値に対してorder次関数で補間した値を返す
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
    #endregion

    #region SmoothingSavitzkyGolay():  Savitzky and Golay Smoothingの結果を返す
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
    #endregion

    #region Differential: n階微分の結果をかえす
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
    #endregion

    public PointD[][] GetPointsWithinRectangle(RectangleD rect) => Geometry.GetPointsWithinRectangle(this.Pt, rect);

    #region ToGSAS: GSAS形式に変換する
    /// <summary>
    /// GSAS形式に変換
    /// </summary>
    /// <param name="name"></param>
    /// <param name="profile"></param>
    /// <param name="axis"></param>
    /// <returns></returns>
    public static string[] ToGSAS(string name, Profile profile, HorizontalAxis axis)
    {
        var sb = new List<string>();
        double div = axis == HorizontalAxis.NeutronTOF ? 1 : 100;

        //一行目
        sb.Add(name);

        //二行目
        var ptCount = profile.Pt.Count;
        var startAngle = profile.Pt[0].X * div;
        var stepAngle = (profile.Pt[1].X - profile.Pt[0].X) * div;
        sb.Add($"BANK 1 {ptCount} {ptCount} CONST {startAngle:f2} {stepAngle:f2} 0 0 FXYE");

        //errが有効なデータかどうかを判定
        bool validErr = true;
        if (profile.Err != null && profile.Err.Count == profile.Pt.Count)
        {
            for (int i = 0; i < ptCount; i++)
                if (profile.Err[i].IsNaN)
                    validErr = false;
        }
        else
            validErr = false;

        for (int i = 0; i < ptCount; i++)
        {
            var value = new string[3];
            value[0] = (profile.Pt[i].X * div).ToString("g12");
            value[1] = profile.Pt[i].Y.ToString("g12");
            if (validErr)
                value[2] = profile.Err[i].Y.ToString("g12");
            else
                value[2] = Math.Sqrt(profile.Pt[i].Y).ToString("g12");

            var y = profile.Pt[i].Y.ToString("g7");

            for (int j = 0; j < value.Length; j++)
            {
                if (value[j].Length > 11)
                    value[j] = value[j][..11];
                if (value[j].EndsWith("."))
                    value[j] = " " + y[..10];
                while (value[j].Length < 11)
                    value[j] = " " + value[j];
            }
            sb.Add($" {value[0]} {value[1]} {value[2]}");
        }

        return [.. sb];
    }
    #endregion

    #endregion
}

#region 各種列挙
/// <summary>
/// 横軸の種類 (Angle, d, WaveNumber, Length, EnergyXray, EnergyElectron, EnergyNeutron, NeutronTOF, none)
/// </summary>
[Serializable]
public enum HorizontalAxis { Angle, d, WaveNumber, Length, EnergyXray, EnergyElectron, EnergyNeutron, NeutronTOF, None }

/// <summary>
/// 波の種類 (Xray, Electron, Neutron, None)
/// </summary>
[Serializable]
public enum WaveSource { Xray, Electron, Neutron, None }

/// <summary>
/// 波の色 (Monochrome, FlatWhite, CustomWhite, None)
/// </summary>
[Serializable]
public enum WaveColor { Monochrome, FlatWhite, CustomWhite, None }

/// <summary>
/// プロファイルモード (Concentric, Radial)
/// </summary>
[Serializable]
public enum DiffractionProfileMode { Concentric, Radial }

/// <summary>
/// バックグランドモード (BSplineCurve, ReferrenceProfile)
/// </summary>
[Serializable]
public enum BackgroundMode { BSplineCurve, ReferrenceProfile }

#endregion

[Serializable]
public class DiffractionProfile2 : ICloneable
{
    #region マスク関連ここから
    [Serializable]
    public class MaskingRange
    {
        public double[] X = new double[2];

        public MaskingRange() => X[0] = X[1] = 0;

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

    public List<MaskingRange> maskingRanges = [];

    public bool SortMaskRanges()
    {
        bool flag = false;
        for (int i = 0; i < maskingRanges.Count - 1; i++)
            if (Math.Max(maskingRanges[i].X[0], maskingRanges[i].X[1]) > Math.Min(maskingRanges[i + 1].X[0], maskingRanges[i + 1].X[1]))
                flag = true;

        if (flag)
        {
            List<double> temp = [];
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
    #endregion

    public object Clone()
    {
        var dp = (DiffractionProfile2)MemberwiseClone();
        dp.SourceProfile = (Profile)SourceProfile.Clone();
        dp.Profile = (Profile)Profile.Clone();
        dp.InterpolatedProfile = (Profile)InterpolatedProfile.Clone();
        dp.SmoothedProfile = (Profile)SmoothedProfile.Clone();
        dp.Kalpha2RemovedProfile = (Profile)Kalpha2RemovedProfile.Clone();
        dp.ConvertedProfile = (Profile)ConvertedProfile.Clone();
        dp.BackgroundProfile = (Profile)BackgroundProfile.Clone();
        return dp;
    }

    #region プロパティ

    #region プロファイル
    /// <summary>
    /// ソースプロファイル ()
    /// </summary>
    public Profile SourceProfile;

    public PointD[] BgPoints;

    /// <summary>
    /// 軸変換後のプロファイル. SetConvertedProfile()を呼び出すことで生成される.
    /// </summary>
    [XmlIgnore]
    public Profile ConvertedProfile;

    /// <summary>
    /// 補完されたプロファイル. ConvertedProfileの後に生成される。
    /// </summary>
    [XmlIgnore]
    public Profile InterpolatedProfile;

    /// <summary>
    /// スムージングされたプロファイル. InterpolatedProfileの後に生成される.
    /// </summary>
    [XmlIgnore]
    public Profile SmoothedProfile;

    /// <summary>
    /// Kalpha2除去がされたプロファイル. SmoothedProfileの後に生成される.
    /// </summary>
    [XmlIgnore]
    public Profile Kalpha2RemovedProfile;

    /// <summary>
    /// バックグランドプロファイル. Kalpha2RemovedProfileの後に生成される.
    /// </summary>
    [XmlIgnore]
    public Profile BackgroundProfile;

    /// <summary>
    /// 最終プロファイル. Kalpha2RemovedProfileの後に生成される.
    /// </summary>
    [XmlIgnore]
    public Profile Profile;
    #endregion

    /// <summary>
    /// プロファイルモード
    /// </summary>
    public DiffractionProfileMode Mode = DiffractionProfileMode.Concentric;

    /// <summary>
    /// ソースプロファイルに関するプロパティ
    /// </summary>
    public HorizontalAxisProperty SrcProperty;

    /// <summary>
    /// 最終プロファイルのプロパティ
    /// </summary>
    public HorizontalAxisProperty DstProperty;

    #region ノーマライズ関連
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
    #endregion

    #region スムージング関連
    public bool DoesSmoothing = false;

    public int SazitkyGorayM = 3, SazitkyGorayN = 3;
    #endregion

    #region 2θシフト
    public bool DoesTwoThetaOffset = false;

    public double TwoThetaOffsetCoeff0 = 0, TwoThetaOffsetCoeff1 = 0, TwoThetaOffsetCoeff2 = 0;
    #endregion

    #region Kalpha2除去
    //Kalpha2除去ここから
    public bool DoesRemoveKalpha2 = false;

    public double Kalpha1 = 0, Kalpha2 = 0;
    #endregion

    #region シフト
    //shift関連
    public bool IsShiftX = false;

    public double ShiftX = 0;
    #endregion

    #region FFT
    //FFT関連
    public bool DoesBandpassFilter = false;

    public bool DoesLowPath = false, DoesHighPath = false;
    public double LowPathLimit = double.NaN, HighPathLimit = double.NaN;
    #endregion

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

    #region /バックグランド設定関連
    public int BgPointsNumber = 15;

    public bool SubtractBackground = false;
    public BackgroundMode BgMode = BackgroundMode.BSplineCurve;
    public Profile BackgroundReferrenceProfile = null;
    public double BackgroundReferrenceScale = 1;
    #endregion

    public string Name;

    public string Comment { get; set; }

    public bool IsLPOmain = false;
    public bool IsLPOchild = false;

    #region イメージ関連
    public double[] ImageArray = null;
    public double ImageScale = 0;
    public int ImageWidth = 0, ImageHeight = 0;
    #endregion

    public override string ToString() => Name;

    #endregion

    #region コンストラクタ
    public DiffractionProfile2()
    {
        BgPoints = [];

        SourceProfile = new Profile();
        ConvertedProfile = new Profile();
        SmoothedProfile = new Profile();
        Kalpha2RemovedProfile = new Profile();
        InterpolatedProfile = new Profile();
        Profile = new Profile();
        BackgroundProfile = new Profile();
        SrcProperty.AxisMode = HorizontalAxis.Angle;

        SrcProperty.WaveSource = WaveSource.Xray;

        SrcProperty.XrayElementNumber = 0;
        SrcProperty.XrayLine = XrayLine.Ka1;

        SrcProperty.ElectronAccVolatage = 200;

        ColorARGB = null;
    }
    #endregion

    public void CopyParameter(DiffractionProfile2 defaultDP)
    {
        DoesSmoothing = defaultDP.DoesSmoothing;
        SazitkyGorayM = defaultDP.SazitkyGorayM;
        SazitkyGorayN = defaultDP.SazitkyGorayN;
        SubtractBackground = defaultDP.SubtractBackground;
    }

    #region バックグラウンド制御点の追加/削除
    /// <summary>
    /// クライアント座標のptを受取り、オリジナル座標に変換して追加する
    /// </summary>
    /// <param name="pt"></param>
    public void AddBgPoints(PointD pt)
    {
        var profile = new Profile(BgPoints);

        PointD newPt = ConvertDestToSrc(pt);
        if (!double.IsNaN(newPt.X) && !double.IsInfinity(newPt.X))
            profile.Pt.Add(newPt);
        profile.Sort();
        BgPoints = [.. profile.Pt];
    }

    public void DeleteBgPoints(int index)
    {
        var profile = new Profile(BgPoints);
        profile.Pt.RemoveAt(index);
        BgPoints = [.. profile.Pt];
    }
    #endregion

    /// <summary>
    /// 横軸条件はそのままで、縦軸ノーマライズを施しOriginalProfileからConvertedProfileを生成する。
    /// </summary>
    public void SetConvertedProfile()
    {
        SetConvertedProfile(DstProperty);
    }

    #region 一連の変換 SourceProfile => MaskingProfile => SmoothingProfile =>  Kalpha2RemovedProfile => BackgroundProfile & Profile

    /// <summary>
    /// 横軸変換および縦軸ノーマライズを施しOriginalProfileからConvertedProfileを生成する。最後にSetMaskingProfile()を実行する.
    /// </summary>
    public void SetConvertedProfile(HorizontalAxisProperty property)
    {
        if (SourceProfile == null)
            return;

        DstProperty = property;

        ConvertedProfile.Clear();

        PointD[] pt = ConvertSrcToDest([.. SourceProfile.Pt]);
        PointD[] err = ConvertSrcToDest([.. SourceProfile.Err]);

        //ここから、2ThetaOffset処理
        if (IsShiftX)
        {
            for (int i = 0; i < pt.Length; i++)
                pt[i] = new PointD(pt[i].X + ShiftX, pt[i].Y);
            for (int i = 0; i < err.Length; i++)
                err[i] = new PointD(err[i].X + ShiftX, err[i].Y);
        }

        if (DoesTwoThetaOffset && DstProperty.AxisMode == HorizontalAxis.Angle)
        {
            for (int i = 0; i < pt.Length; i++)
            {
                var tan = Math.Tan(pt[i].X / 360.0 * Math.PI);
                pt[i] = new PointD(pt[i].X + TwoThetaOffsetCoeff0 + TwoThetaOffsetCoeff1 * tan + TwoThetaOffsetCoeff2 * tan * tan, pt[i].Y);
            }
            for (int i = 0; i < err.Length; i++)
            {
                var tan = Math.Tan(err[i].X / 360.0 * Math.PI);
                err[i] = new PointD(err[i].X + TwoThetaOffsetCoeff0 + TwoThetaOffsetCoeff1 * tan + TwoThetaOffsetCoeff2 * tan * tan, err[i].Y);
            }
        }

        //ここまで

        ConvertedProfile.Pt.AddRange(pt);
        ConvertedProfile.Err.AddRange(err);

        ConvertedProfile.Sort();//ここまでプロファイルソートを完了

        SetMaskingProfile();
    }

    /// <summary>
    /// Maskされた領域を補完し、ConvertedProfileからInterpolatedProfileを作成する. 最後にSetSmoothingProfile()を実行する。
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
                x.Add([]);
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
                y.Add([]);
                y[i].AddRange(InterpolatedProfile.GetValues([.. x[i]], InterpolationPoints * 2, InterpolationOrder, true, true));
            }
            for (int i = 0; i < x.Count; i++)
                for (int j = 0; j < x[i].Count; j++)
                    InterpolatedProfile.Pt.Add(new PointD(x[i][j], y[i][j]));
            InterpolatedProfile.Sort();
        }
        SetSmoothingProfile();
    }

    /// <summary>
    /// スムージングとFFTを施しInterpolatedProfileからSmoothedProfileを作成する。最後にSetKalpha2RemovedProfile()を実行する。
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
    /// Kalpha2を除去する。最後にSetBackGroundProfile()を実行する。
    /// </summary>
    public void SetKalpha2RemovedProfile()
    {
        Kalpha2RemovedProfile.Clear();

        for (int i = 0; i < SmoothedProfile.Pt.Count; i++)
            Kalpha2RemovedProfile.Pt.Add(SmoothedProfile.Pt[i]);
        for (int i = 0; i < SmoothedProfile.Err.Count; i++)
            Kalpha2RemovedProfile.Err.Add(SmoothedProfile.Err[i]);

        if (DoesRemoveKalpha2 && SrcProperty.WaveSource == WaveSource.Xray && SrcProperty.XrayElementNumber != 0 && SrcProperty.XrayLine == XrayLine.Ka1)
        {
            double alpha1 = AtomStatic.CharacteristicXrayWavelength(SrcProperty.XrayElementNumber, XrayLine.Ka1);
            double alpha2 = AtomStatic.CharacteristicXrayWavelength(SrcProperty.XrayElementNumber, XrayLine.Ka2);
            double startY = Kalpha2RemovedProfile.Pt[0].Y * 2 / 3;

            var theta = new List<double>();
            for (int i = 0; i < Kalpha2RemovedProfile.Pt.Count; i++)
            {
                var d = alpha2 * 0.5 / Math.Sin(Kalpha2RemovedProfile.Pt[i].X / 360 * Math.PI);
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
            BackgroundProfile.Pt = [];
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
    #endregion

    #region ノーマライズ
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
    #endregion

    #region バックグランド制御点の検索
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
        List<double> y = [];
        for (i = 0; i < p1.Pt.Count - 1; i++)
            p2.Pt.Add(new PointD((p1.Pt[i].X + p1.Pt[i + 1].X) / 2, (p1.Pt[i + 1].Y - p1.Pt[i].Y) / (p1.Pt[i + 1].X - p1.Pt[i].X)));
        p2 = Smoothing.SavitzkyGolay(p2, 3, 3);

        for (i = 0; i < p2.Pt.Count; i++)
            y.Add(p2.Pt[i].Y);

        //p2のY値を規格化
        double d = Statistics.Deviation([.. y]);
        for (i = 0; i < p2.Pt.Count; i++)
            y[i] /= d;

        //30ポイント以上小さな値が続いて、かつ前後100ポイント以上はなれているポイントをさがす、
        double temp = 1;
        double tempMax = double.PositiveInfinity;
        int tempXindex = 0;
        List<double> X = [];
        List<int> Xindex = [];
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
            List<PointD> bgPoints = [];
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

            BgPoints = ConvertDestToSrc([.. bgPoints]);
        }
    }
    #endregion

    #region Kalpha2除去
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
    #endregion


    #region 横軸を変換するメソッド群

    #region X軸の変換 Src => Dest
    /// <summary>
    /// Srcの横軸をDestの横軸に変換する
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    private PointD convertSrcToDest(PointD pt)
    {
        var pts = HorizontalAxisConverter.Convert([pt.X], SrcProperty, DstProperty);
        if (pts.Length == 0)
            return new PointD(0, 0);
        var x = pts[0];
        var y = pt.Y;
        if (IsCPS && ExposureTime > 0)
            y /= ExposureTime;
        if (IsLogIntensity)
            y = Math.Log10(y);

        return new PointD(x, y);
    }

    public PointD[] ConvertSrcToDest(PointD[] pt)
    {
        var dest = new List<PointD>();
        for (int i = 0; i < pt.Length; i++)
        {
            var p = convertSrcToDest(pt[i]);
            if (!double.IsNaN(p.X) && !double.IsInfinity(p.X) && !double.IsNaN(p.Y) && !double.IsInfinity(p.Y))
                dest.Add(p);
        }
        #region お蔵?
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
        #endregion
        return [.. dest];
    }
    #endregion

    #region X軸の変換 Dest=>Src
    /// <summary>
    /// Destの横軸をSrcの横軸に変換する
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public PointD ConvertDestToSrc(PointD pt)
    {
        var x = HorizontalAxisConverter.Convert([pt.X], DstProperty, SrcProperty)[0];
        var y = pt.Y;
        if (IsCPS && ExposureTime != 0)
            y *= ExposureTime;
        if (IsLogIntensity)
            y = Math.Pow(10, y);
        return new PointD(x, y);
    }

    /// <summary>
    /// Destの横軸をSrcの横軸に変換する
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public PointD[] ConvertDestToSrc(PointD[] pt)
    {
        var dest = new List<PointD>();
        for (int i = 0; i < pt.Length; i++)
        {
            var p = ConvertDestToSrc(pt[i]);
            if (!double.IsNaN(p.X) && !double.IsInfinity(p.X) && !double.IsNaN(p.Y) && !double.IsInfinity(p.Y))
                dest.Add(p);
        }
        return [.. dest];
    }
    #endregion

    #endregion
}

#region HorizontalAxisProperty プロファイルの性質を表す構造体

[Serializable]
public record struct HorizontalAxisProperty
{
    #region プロパティ
    /// <summary>
    /// 横軸の種類
    /// </summary>
    public HorizontalAxis AxisMode { get; set; } = HorizontalAxis.Angle;
    /// <summary>
    /// 入射波の種類
    /// </summary>
    public WaveSource WaveSource { get; set; } = WaveSource.Xray;
    /// <summary>
    /// 入射波の色
    /// </summary>
    public WaveColor WaveColor { get; set; } = WaveColor.Monochrome;
    /// <summary>
    /// 入射波がモノクロの時の入射波の波長 (nm単位)
    /// </summary>
    public double WaveLength { get; set; } = 0.4;

    /// <summary>
    /// 入射波が特性X線の時のターゲット原子番号 (0はカスタム)
    /// </summary>
    public int XrayElementNumber { get; set; } = 29;
    /// <summary>
    /// 入射線が特性X線の時のライン
    /// </summary>
    public XrayLine XrayLine { get; set; } = XrayLine.Ka1;


    /// <summary>
    /// 入射波が電子線の場合の加速電圧
    /// </summary>
    public double ElectronAccVolatage { get; set; } = 200;


    /// <summary>
    /// 入射波が白色の時の Takeoff angle (radian)
    /// </summary>
    public double EnergyTakeoffAngle { get; set; } = 5.0 / 180.0 * Math.PI;

    /// <summary>
    /// ソースプロファイルが白色TOF時の角度
    /// </summary>
    public double TofAngle { get; set; } = Math.PI / 4;
    /// <summary>
    /// 白色TOF時の検出器距離(m)
    /// </summary>
    public double TofLength { get; set; } = 25;


    /// <summary>
    /// 横軸が角度の時の2θの時の単位
    /// </summary>
    public AngleUnitEnum TwoThetaUnit { get; set; } = AngleUnitEnum.Radian;
    public readonly string TwoThetaUnitText => TwoThetaUnit switch
    {
        AngleUnitEnum.Radian => "rad",
        AngleUnitEnum.Degree => "°",
        _ => "",
    };

    /// <summary>
    /// 横軸がD値の時の単位
    /// </summary>
    public LengthUnitEnum DspacingUnit { get; set; } = LengthUnitEnum.Angstrom;
    public string DspacingUnitText => DspacingUnit switch
    {
        LengthUnitEnum.Angstrom => "Å",
        LengthUnitEnum.NanoMeter => "nm",
        _ => "",
    };

    /// <summary>
    /// 横軸が波数(2π/d) の時の単位
    /// </summary>
    public LengthUnitEnum WaveNumberUnit { get; set; } = LengthUnitEnum.NanoMeterInverse;
    public readonly string WaveNumberUnitText => WaveNumberUnit switch
    {
        LengthUnitEnum.NanoMeterInverse => "nm⁻¹",
        LengthUnitEnum.AngstromInverse => "Å⁻¹",
        _ => "",
    };

    /// <summary>
    /// 入射波が白色の時のエネルギーの単位
    /// </summary>
    public EnergyUnitEnum EnergyUnit { get; set; } = EnergyUnitEnum.eV;

    public readonly string EnegyUnitText => EnergyUnit switch
    {
        EnergyUnitEnum.eV => "eV",
        EnergyUnitEnum.KeV => "KeV",
        EnergyUnitEnum.MeV => "MeV",
        _ => "",
    };


    /// <summary>
    /// 白色TOF時の 時間単位
    /// </summary>
    public TimeUnitEnum TofTimeUnit { get; set; } = TimeUnitEnum.MicroSecond;
    public readonly string TofTimeUnitText => TofTimeUnit switch
    {
        TimeUnitEnum.NanoSecond => "ns",
        TimeUnitEnum.MicroSecond => "µs",
        TimeUnitEnum.MilliSecond => "ms",
        _ => "",
    };



    #endregion

    /// <summary>
    /// 基本コンストラクタ
    /// </summary>
    /// <param name="axisMode"></param>
    /// <param name="waveSource"></param>
    /// <param name="waveColor"></param>
    /// <param name="waveLength"></param>
    /// <param name="xrayElementNumber"></param>
    /// <param name="xrayLine"></param>
    /// <param name="electronAccVolatage"></param>
    /// <param name="energyTakeoffAngle"></param>
    /// <param name="tofAngle"></param>
    /// <param name="tofLength"></param>
    /// <param name="twoThetaUnit"></param>
    /// <param name="dspacingUnit"></param>
    /// <param name="waveNumberUnit"></param>
    /// <param name="energyUnit"></param>
    /// <param name="tofTimeUnit"></param>
    public HorizontalAxisProperty(HorizontalAxis axisMode, WaveSource waveSource, WaveColor waveColor, double waveLength, int xrayElementNumber, XrayLine xrayLine, double electronAccVolatage,
        double energyTakeoffAngle, double tofAngle, double tofLength, AngleUnitEnum twoThetaUnit, LengthUnitEnum dspacingUnit, LengthUnitEnum waveNumberUnit, EnergyUnitEnum energyUnit, TimeUnitEnum tofTimeUnit)
    {
        AxisMode = axisMode;
        WaveSource = waveSource;
        WaveColor = waveColor;
        WaveLength = waveLength;
        XrayElementNumber = xrayElementNumber;
        XrayLine = xrayLine;
        ElectronAccVolatage = electronAccVolatage;
        EnergyTakeoffAngle = energyTakeoffAngle;
        TofAngle = tofAngle;
        TofLength = tofLength;
        TwoThetaUnit = twoThetaUnit;
        DspacingUnit = dspacingUnit;
        WaveNumberUnit = waveNumberUnit;
        EnergyUnit = energyUnit;
        TofTimeUnit = tofTimeUnit;
    }

    /// <summary>
    /// 特性X専用コンストラクタ
    /// </summary>
    /// <param name="xrayElementNumber"></param>
    /// <param name="xrayLine"></param>
    /// <param name="twoThetaUnit"></param>
    public HorizontalAxisProperty(int xrayElementNumber, XrayLine xrayLine, AngleUnitEnum twoThetaUnit)
    {
        AxisMode = HorizontalAxis.Angle;
        WaveSource = WaveSource.Xray;
        WaveColor = WaveColor.Monochrome;
        XrayElementNumber = xrayElementNumber;
        XrayLine = xrayLine;
        TwoThetaUnit = twoThetaUnit;
    }

    /// <summary>
    /// 任意の波長の単色線 + 角度分散のコンストラクタ
    /// </summary>
    /// <param name="waveSource"></param>
    /// <param name="waveLength"></param>
    /// <param name="twoThetaUnit"></param>
    public HorizontalAxisProperty(WaveSource waveSource, double waveLength, AngleUnitEnum twoThetaUnit)
    {
        WaveSource = waveSource;
        if (waveSource == WaveSource.Xray)
            XrayElementNumber = 0;
        WaveColor = WaveColor.Monochrome;
        AxisMode = HorizontalAxis.Angle;

        WaveLength = waveLength;
        TwoThetaUnit = twoThetaUnit;
    }


    /// <summary>
    /// 白色専用コンストラクタ
    /// </summary>
    /// <param name="xrayElementNumber"></param>
    /// <param name="xrayLine"></param>
    /// <param name="twoThetaUnit"></param>
    public HorizontalAxisProperty(WaveSource waveSource, double energyTakeoffAngle, EnergyUnitEnum energyUnit)

    {
        WaveSource = waveSource;
        if (waveSource == WaveSource.Electron)
            AxisMode = HorizontalAxis.EnergyElectron;
        else if (waveSource == WaveSource.Xray)
            AxisMode = HorizontalAxis.EnergyXray;
        else
            AxisMode = HorizontalAxis.EnergyNeutron;

        WaveColor = WaveColor.FlatWhite;
        EnergyTakeoffAngle = energyTakeoffAngle;
        EnergyUnit = energyUnit;
    }


    public HorizontalAxisProperty(double tofAngle, double tofLength, TimeUnitEnum tofTimeUnit)
    {
        AxisMode = HorizontalAxis.NeutronTOF;
        WaveSource = WaveSource.Neutron;
        WaveColor = WaveColor.FlatWhite;
        TofAngle = tofAngle;
        TofLength = tofLength;
        TofTimeUnit = tofTimeUnit;
    }

}
#endregion

#region HorizontalAxisConverter 横軸を変換するクラス

/// <summary>
/// 横軸を変換するクラス
/// </summary>
public static class HorizontalAxisConverter
{
    public static double[] Convert(double[] x, HorizontalAxisProperty src, HorizontalAxisProperty dst)
    {
        #region はじめにSrcとDstのAxisモードが等しいときをチェック

        if (src.AxisMode == dst.AxisMode)
        {
            //横軸が散乱角で入射X線の波長が等しいとき
            if (src.AxisMode == HorizontalAxis.Angle && src.WaveLength == dst.WaveLength)
            {
                if (src.TwoThetaUnit == dst.TwoThetaUnit)
                    return x;
                else if (src.TwoThetaUnit == AngleUnitEnum.Radian)
                    return x.Select(x => x / Math.PI * 180).ToArray();
                else
                    return x.Select(x => x * Math.PI / 180).ToArray();
            }

            //横軸がd値の時
            if (src.AxisMode == HorizontalAxis.d)
            {
                if (src.DspacingUnit == dst.DspacingUnit)
                    return x;
                else if (src.DspacingUnit == LengthUnitEnum.Angstrom && dst.DspacingUnit == LengthUnitEnum.NanoMeter)
                    return x.Select(x => x * 0.1).ToArray();
                else if (src.DspacingUnit == LengthUnitEnum.NanoMeter && dst.DspacingUnit == LengthUnitEnum.Angstrom)
                    return x.Select(x => x * 10).ToArray();
            }

            //横軸がエネルギーでTakeoffAngleも等しいとき
            if ((src.AxisMode == HorizontalAxis.EnergyXray || src.AxisMode == HorizontalAxis.EnergyElectron || src.AxisMode == HorizontalAxis.EnergyNeutron) && dst.EnergyTakeoffAngle == src.EnergyTakeoffAngle)
            {
                if (src.EnergyUnit == dst.EnergyUnit)
                    return x;
                else if (src.EnergyUnit == EnergyUnitEnum.eV)
                    return dst.EnergyUnit == EnergyUnitEnum.KeV ? x.Select(x => x / 1_000).ToArray() : x.Select(x => x / 1_000_000).ToArray();
                else if (src.EnergyUnit == EnergyUnitEnum.KeV)
                    return dst.EnergyUnit == EnergyUnitEnum.eV ? x.Select(x => x * 1_000).ToArray() : x.Select(x => x / 1_000).ToArray();
                else if (src.EnergyUnit == EnergyUnitEnum.MeV)
                    return dst.EnergyUnit == EnergyUnitEnum.eV ? x.Select(x => x * 1_000_000).ToArray() : x.Select(x => x * 1_000).ToArray();
            }

            //横軸がNeutron TOFで、TOF角度もTOF距離も等しいとき
            if (src.AxisMode == HorizontalAxis.NeutronTOF && src.TofAngle == dst.TofAngle && src.TofLength == dst.TofLength)
            {
                if (src.TofTimeUnit == dst.TofTimeUnit)
                    return x;
                else if (src.TofTimeUnit == TimeUnitEnum.MicroSecond && dst.TofTimeUnit == TimeUnitEnum.NanoSecond)
                    return x.Select(x => x * 1_000).ToArray();
                else if (src.TofTimeUnit == TimeUnitEnum.NanoSecond && dst.TofTimeUnit == TimeUnitEnum.MicroSecond)
                    return x.Select(x => x / 1_000).ToArray();
            }

            //横軸が波数の時
            if (src.AxisMode == HorizontalAxis.WaveNumber)
            {
                if (src.WaveNumberUnit == dst.WaveNumberUnit)
                    return x;
                else if (src.WaveNumberUnit == LengthUnitEnum.AngstromInverse && dst.WaveNumberUnit == LengthUnitEnum.NanoMeterInverse)
                    return x.Select(x => x * 10).ToArray();
                else if (src.WaveNumberUnit == LengthUnitEnum.NanoMeterInverse && dst.WaveNumberUnit == LengthUnitEnum.AngstromInverse)
                    return x.Select(x => x * 0.1).ToArray();
            }
        }
        #endregion

        var d = ConvertToD(x, src);

        return ConvertFromD(d, dst);
    }


    /// <summary>
    /// 全てのxを srcに基づいてd値(nm)に変換
    /// </summary>
    /// <param name="x"></param>
    /// <param name="src"></param>
    /// <returns></returns>
    public static double[] ConvertToD(double[] x, HorizontalAxisProperty src)
    {
        var d = Array.Empty<double>();
        if (src.AxisMode == HorizontalAxis.d)
        {
            if (src.DspacingUnit == LengthUnitEnum.Angstrom)
                d = x.Select(d => d * 0.1).ToArray();
            else if (src.DspacingUnit == LengthUnitEnum.NanoMeter)
                d = x;
        }

        else if (src.AxisMode == HorizontalAxis.Angle)
        {
            var twoTheta = Array.Empty<double>();
            if (src.TwoThetaUnit == AngleUnitEnum.Degree)
                d = TwoThetaInDegreeToD(x, src.WaveLength);
            else
                d = TwoThetaInRadianToD(x, src.WaveLength);
        }
        else if (src.AxisMode == HorizontalAxis.EnergyXray || src.AxisMode == HorizontalAxis.EnergyElectron || src.AxisMode == HorizontalAxis.EnergyNeutron)
        {
            var energy = Array.Empty<double>();
            if (src.EnergyUnit == EnergyUnitEnum.eV)
                energy = x;
            else if (src.EnergyUnit == EnergyUnitEnum.KeV)
                energy = x.Select(x => x * 1_000).ToArray();
            else if (src.EnergyUnit == EnergyUnitEnum.MeV)
                energy = x.Select(x => x * 1_000_000).ToArray();

            if (src.AxisMode == HorizontalAxis.EnergyXray)
                d = XrayEnergyToD(energy, src.EnergyTakeoffAngle);
            else if (src.AxisMode == HorizontalAxis.EnergyElectron)
                d = ElectronEnergyToD(energy, src.EnergyTakeoffAngle);
            else if (src.AxisMode == HorizontalAxis.EnergyNeutron)
                d = NeutronEnergyToD(energy, src.EnergyTakeoffAngle);
        }
        else if (src.AxisMode == HorizontalAxis.NeutronTOF)
        {
            var tof = Array.Empty<double>();
            if (src.TofTimeUnit == TimeUnitEnum.MicroSecond)
                tof = x;
            else if (src.TofTimeUnit == TimeUnitEnum.NanoSecond)
                tof = x.Select(x => x / 1000).ToArray();

            d = NeutronTofToD(tof, src.TofAngle, src.TofLength);
        }
        else if (src.AxisMode == HorizontalAxis.WaveNumber)
        {
            if (src.WaveNumberUnit == LengthUnitEnum.NanoMeterInverse)
                d = WaveNumberToD(x);
            else if (src.WaveNumberUnit == LengthUnitEnum.AngstromInverse)
                d = WaveNumberToD(x.Select(x => x * 10));
        }
        return d;
    }
    /// <summary>
    /// xを srcに基づいてd値(nm)に変換
    /// </summary>
    /// <param name="x"></param>
    /// <param name="src"></param>
    /// <returns></returns>
    public static double ConvertToD(double x, HorizontalAxisProperty src) => ConvertToD([x], src)[0];

    /// <summary>
    ///  全てのd値(nm)をdstに基づいて変換
    /// </summary>
    /// <param name="d"></param>
    /// <param name="dst"></param>
    /// <returns></returns>
    public static double[] ConvertFromD(double[] d, HorizontalAxisProperty dst)
    {
        #region 最後にd値(nm単位)を目的の軸に変換
        if (dst.AxisMode == HorizontalAxis.Angle)
        {
            return dst.TwoThetaUnit == AngleUnitEnum.Degree ? DToTwoThetaInDegree(d, dst.WaveLength) : DToTwoThetaInRadian(d, dst.WaveLength);
        }
        else if (dst.AxisMode == HorizontalAxis.d)
            return dst.DspacingUnit == LengthUnitEnum.NanoMeter ? d : d.Select(d => d * 10).ToArray();
        else if (dst.AxisMode == HorizontalAxis.EnergyXray || dst.AxisMode == HorizontalAxis.EnergyElectron || dst.AxisMode == HorizontalAxis.EnergyNeutron)
        {
            double[] energy = [];
            if (dst.AxisMode == HorizontalAxis.EnergyXray)
                energy = DToXrayEnergy(d, dst.EnergyTakeoffAngle);
            else if (dst.AxisMode == HorizontalAxis.EnergyElectron)
                energy = DToElectronEnergy(d, dst.EnergyTakeoffAngle);
            else if (dst.AxisMode == HorizontalAxis.EnergyNeutron)
                energy = DToNeutronEnergy(d, dst.EnergyTakeoffAngle);

            if (dst.EnergyUnit == EnergyUnitEnum.eV)
                return energy;
            else if (dst.EnergyUnit == EnergyUnitEnum.KeV)
                return energy.Select(e => e / 1_000).ToArray();
            else if (dst.EnergyUnit == EnergyUnitEnum.MeV)
                return energy.Select(e => e / 1_000_000).ToArray();
        }
        else if (dst.AxisMode == HorizontalAxis.NeutronTOF)
        {
            d = DToTOF(d, dst.TofAngle, dst.TofLength);
            if (dst.TofTimeUnit == TimeUnitEnum.MicroSecond)
                return d;
            else if (dst.TofTimeUnit == TimeUnitEnum.NanoSecond)
                return d.Select(d => d * 1_000).ToArray();
        }
        else if (dst.AxisMode == HorizontalAxis.WaveNumber)
        {
            d = DToWaveNumber(d);
            if (dst.WaveNumberUnit == LengthUnitEnum.NanoMeterInverse)
                return d;
            else if (dst.WaveNumberUnit == LengthUnitEnum.AngstromInverse)
                return d.Select(d => d / 10).ToArray();
        }
        #endregion

        return d;
    }

    /// <summary>
    /// d値(nm)をdstに基づいて変換
    /// </summary>
    /// <param name="d"></param>
    /// <param name="dst"></param>
    /// <returns></returns>
    public static double ConvertFromD(double d, HorizontalAxisProperty dst) => ConvertFromD([d], dst)[0];

    #region 横軸を変換するメソッド群

    /// <summary>
    /// d -> Wavenumber  d値(nm)を与えると、波数(2π/nm)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double DToWaveNumber(double d) => Math.PI * 2 / d;
    /// <summary>
    /// d -> Wavenumber  d値(nm)を与えると、波数(2π/nm)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static double[] DToWaveNumber(IEnumerable<double> d) => d.Select(e => Math.PI * 2 / e).ToArray();


    /// <summary>
    /// Wavenumber -> d   波数(2π/nm)を与えると、仮想的なd値(nm)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double WaveNumberToD(double wavenumber) => Math.PI * 2 / wavenumber;
    /// <summary>
    /// Wavenumber -> d   波数(2π/nm)を与えると、仮想的なd値(nm)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] WaveNumberToD(IEnumerable<double> wavenumber) => wavenumber.Select(e => Math.PI * 2 / e).ToArray();


    /// <summary>
    /// TOF -> d   TOF(μs)と取り出し角(radian)、距離(m)を与えると、ブラッグ条件を満たすd値(nm)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double NeutronTofToD(double TOF, double angle, double length) => UniversalConstants.Convert.NeutronVelocityToWavelength(length / TOF) / Math.Sin(angle / 2) / 2.0;
    /// <summary>
    /// TOF -> d   TOF(μs)と取り出し角(radian)、距離(m)を与えると、ブラッグ条件を満たすd値(nm)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] NeutronTofToD(IEnumerable<double> TOF, double angle, double length) => TOF.Select(e => UniversalConstants.Convert.NeutronVelocityToWavelength(length / e) / Math.Sin(angle / 2) / 2.0).ToArray();


    /// <summary>
    /// d -> TOF   d値(nm)と仮想的な取り出し角(radian)、距離(m)を与えると、ブラッグ条件を満たす仮想的なTOF(μs)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double DToTOF(double d, double angle, double length) => length / UniversalConstants.Convert.WavelengthToNeutronVelocity(2.0 * d * Math.Sin(angle / 2));
    /// <summary>
    /// d -> TOF   d値(nm)と仮想的な取り出し角(radian)、距離(m)を与えると、ブラッグ条件を満たす仮想的なTOF(μs)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] DToTOF(IEnumerable<double> d, double angle, double length) => d.Select(e => length / UniversalConstants.Convert.WavelengthToNeutronVelocity(2.0 * e * Math.Sin(angle / 2))).ToArray();


    /// <summary>
    /// d -> E   面間隔d(nm)と仮想的な取り出し角(2Θ)を与えるとブラッグ条件を満たす仮想的な電磁波のエネルギー(eV)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double DToXrayEnergy(double d, double takeoffAngle) => UniversalConstants.Convert.WavelengthToXrayEnergy(2.0 * d * Math.Sin(takeoffAngle / 2.0));
    /// <summary>
    /// d -> E   面間隔d(nm)と仮想的な取り出し角(2Θ)を与えるとブラッグ条件を満たす仮想的な電磁波のエネルギー(eV)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] DToXrayEnergy(IEnumerable<double> d, double takeoffAngle) => d.Select(e => UniversalConstants.Convert.WavelengthToXrayEnergy(2.0 * e * Math.Sin(takeoffAngle / 2.0))).ToArray();


    /// <summary>
    /// d -> E   面間隔d(nm)と仮想的な取り出し角(2Θ)を与えるとブラッグ条件を満たす仮想的な電子のエネルギー(keV)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double DToElectronEnergy(double d, double takeoffAngle) => UniversalConstants.Convert.WaveLengthToElectronEnergy(2.0 * d * Math.Sin(takeoffAngle / 2.0)) * 1000;
    /// <summary>
    /// d -> E   面間隔d(nm)と仮想的な取り出し角(2Θ)を与えるとブラッグ条件を満たす仮想的な電子のエネルギー(keV)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] DToElectronEnergy(IEnumerable<double> d, double takeoffAngle) => d.Select(e => UniversalConstants.Convert.WaveLengthToElectronEnergy(2.0 * e * Math.Sin(takeoffAngle / 2.0)) * 1000).ToArray();


    /// <summary>
    /// d -> E   面間隔d(nm)と仮想的な取り出し角(2Θ)を与えるとブラッグ条件を満たす仮想的な中性子のエネルギー(eV)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double DToNeutronEnergy(double d, double takeoffAngle) => UniversalConstants.Convert.WaveLengthToNeutronEnergy(2.0 * d * Math.Sin(takeoffAngle / 2.0));
    /// <summary>
    /// d -> E   面間隔d(nm)と仮想的な取り出し角(2Θ)を与えるとブラッグ条件を満たす仮想的な中性子のエネルギー(eV)を返す
    /// </summary>
    /// <param name="d"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] DToNeutronEnergy(IEnumerable<double> d, double takeoffAngle) => d.Select(e => UniversalConstants.Convert.WaveLengthToNeutronEnergy(2.0 * e * Math.Sin(takeoffAngle / 2.0))).ToArray();


    /// <summary>
    /// E -> d  電磁波のエネルギー(eV)と取り出し角を与えるとブラッグ条件を満たす面間隔d(nm)の値を返す
    /// </summary>
    /// <param name="energy"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double XrayEnergyToD(double energy, double takeoffAngle) => UniversalConstants.Convert.EnergyToXrayWaveLength(energy) / 2.0 / Math.Sin(takeoffAngle / 2.0);
    /// <summary>
    /// E -> d  電磁波のエネルギー(eV)と取り出し角を与えるとブラッグ条件を満たす面間隔d(nm)の値を返す
    /// </summary>
    /// <param name="energy"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] XrayEnergyToD(IEnumerable<double> energy, double takeoffAngle) => energy.Select(e => UniversalConstants.Convert.EnergyToXrayWaveLength(e) / 2.0 / Math.Sin(takeoffAngle / 2.0)).ToArray();


    /// <summary>
    /// E -> d  電子のエネルギー(eV)と取り出し角を与えるとブラッグ条件を満たす面間隔d(nm)の値を返す
    /// </summary>
    /// <param name="energy"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double ElectronEnergyToD(double energy, double takeoffAngle) => UniversalConstants.Convert.EnergyToElectronWaveLength(energy / 1000) / 2.0 / Math.Sin(takeoffAngle / 2.0);
    /// <summary>
    /// E -> d  電子のエネルギー(eV)と取り出し角を与えるとブラッグ条件を満たす面間隔d(nm)の値を返す
    /// </summary>
    /// <param name="energy"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] ElectronEnergyToD(IEnumerable<double> energy, double takeoffAngle) => energy.Select(e => UniversalConstants.Convert.EnergyToElectronWaveLength(e / 1000) / 2.0 / Math.Sin(takeoffAngle / 2.0)).ToArray();


    /// <summary>
    /// E -> d  中性子のエネルギー(eV)と取り出し角を与えるとブラッグ条件を満たす面間隔d(nm)の値を返す
    /// </summary>
    /// <param name="energy"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double NeutronEnergyToD(double energy, double takeoffAngle) => UniversalConstants.Convert.EnergyToNeutronWaveLength(energy) / 2.0 / Math.Sin(takeoffAngle / 2.0);
    /// <summary>
    /// E -> d  中性子のエネルギー(eV)と取り出し角を与えるとブラッグ条件を満たす面間隔d(nm)の値を返す
    /// </summary>
    /// <param name="energy"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] NeutronEnergyToD(IEnumerable<double> energy, double takeoffAngle) => energy.Select(e => UniversalConstants.Convert.EnergyToNeutronWaveLength(e) / 2.0 / Math.Sin(takeoffAngle / 2.0)).ToArray();





    /// <summary>
    /// E -> 2θ  電磁波のエネルギー(eV)、取り出し角(takeoffAngle)、仮想的な入射線の波長(nm)を与えると仮想的な回折角を返す
    /// </summary>
    /// <param name="energy"></param>
    /// <param name="waveLength"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double XrayEnergyToTwoTheta(double energy, double takeoffAngle, double waveLength) => 2 * Math.Asin(waveLength / 2.0 / XrayEnergyToD(energy, takeoffAngle));
    /// <summary>
    /// E -> 2θ  電磁波のエネルギー(eV)、取り出し角(takeoffAngle)、仮想的な入射線の波長(nm)を与えると仮想的な回折角を返す
    /// </summary>
    /// <param name="energy"></param>
    /// <param name="waveLength"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] XrayEnergyToTwoTheta(IEnumerable<double> energy, double takeoffAngle, double waveLength) => energy.Select(e => 2 * Math.Asin(waveLength / 2.0 / XrayEnergyToD(e, takeoffAngle))).ToArray();


    /// <summary>
    ///  2θ -> E  ブラッグ角と、入射線波長(nm)、仮想的なEDX取り出し角(takeoffAngle)からブラッグ条件を満たす仮想的な電磁波波長(nm)をかえす
    /// </summary>
    /// <param name="twoTheta"></param>
    /// <param name="waveLength"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double TwoThetaToXrayEnergy(double twoTheta, double waveLength, double takeoffAngle) => DToXrayEnergy(waveLength / 2.0 / Math.Sin(twoTheta / 2.0), takeoffAngle);
    /// <summary>
    ///  2θ -> E  ブラッグ角と、入射線波長(nm)、仮想的なEDX取り出し角(takeoffAngle)からブラッグ条件を満たす仮想的な電磁波波長(nm)をかえす
    /// </summary>
    /// <param name="twoTheta"></param>
    /// <param name="waveLength"></param>
    /// <param name="takeoffAngle"></param>
    /// <returns></returns>
    public static double[] TwoThetaToXrayEnergy(IEnumerable<double> twoTheta, double waveLength, double takeoffAngle) => twoTheta.Select(e => DToXrayEnergy(waveLength / 2.0 / Math.Sin(e / 2.0), takeoffAngle)).ToArray();

    /// <summary>
    /// 2θ(rad) -> d(nm)   ブラッグ角(radian)と入射線波長からブラッグ条件を満たす面間隔d(nm)を返す
    /// </summary>
    /// <param name="twoTheta"></param>
    /// <param name="waveLength"></param>
    /// <returns></returns>
    public static double TwoThetaInRadianToD(double twoTheta, double waveLength) => waveLength / Math.Sin(twoTheta / 2.0) / 2.0;
    /// <summary>
    /// 2θ(rad) -> d(nm)   ブラッグ角(radian)と入射線波長からブラッグ条件を満たす面間隔d(nm)を返す
    /// </summary>
    /// <param name="twoTheta"></param>
    /// <param name="waveLength"></param>
    /// <returns></returns>
    public static double[] TwoThetaInRadianToD(IEnumerable<double> twoTheta, double waveLength) => twoTheta.Select(e => waveLength / Math.Sin(e / 2.0) / 2.0).ToArray();

    /// <summary>
    /// 2θ(deg) -> d(nm)   ブラッグ角(degree)と入射線波長からブラッグ条件を満たす面間隔d(nm)を返す
    /// </summary>
    /// <param name="twoTheta"></param>
    /// <param name="waveLength"></param>
    /// <returns></returns>
    public static double TwoThetaInDegreeToD(double twoTheta, double waveLength) => waveLength / Math.Sin(twoTheta / 180.0 * Math.PI / 2.0) / 2.0;
    /// <summary>
    /// 2θ(deg) -> d(nm)   ブラッグ角(degree)と入射線波長からブラッグ条件を満たす面間隔d(nm)を返す
    /// </summary>
    /// <param name="twoTheta"></param>
    /// <param name="waveLength"></param>
    /// <returns></returns>
    public static double[] TwoThetaInDegreeToD(IEnumerable<double> twoTheta, double waveLength) => twoTheta.Select(e => waveLength / Math.Sin(e / 180.0 * Math.PI / 2.0) / 2.0).ToArray();


    /// <summary>
    /// d(nm) -> 2θ(rad) 面間隔d(nm)と仮想的な入射線の波長(nm)を与えるとブラッグ条件を満たす仮想的な回折角(2θ)を返す
    /// </summary>
    /// <param name="wavelength"></param>
    /// <returns></returns>
    public static double DToTwoThetaInRadian(double d, double wavelength) => 2 * Math.Asin(wavelength / 2.0 / d);
    /// <summary>
    /// d(nm) -> 2θ(rad) 面間隔d(nm)と仮想的な入射線の波長(nm)を与えるとブラッグ条件を満たす仮想的な回折角(2θ)を返す
    /// </summary>
    /// <param name="wavelength"></param>
    /// <returns></returns>
    public static double[] DToTwoThetaInRadian(IEnumerable<double> d, double wavelength) => d.Select(e => 2 * Math.Asin(wavelength / 2.0 / e)).ToArray();
    /// <summary>
    /// d(nm) -> 2θ(deg) 面間隔d(nm)と仮想的な入射線の波長(nm)を与えるとブラッグ条件を満たす仮想的な回折角(2θ)を返す
    /// </summary>
    /// <param name="wavelength"></param>
    /// <returns></returns>
    public static double DToTwoThetaInDegree(double d, double wavelength) => 2 * Math.Asin(wavelength / 2.0 / d) / Math.PI * 180;
    /// <summary>
    /// d(nm) -> 2θ(deg) 面間隔d(nm)と仮想的な入射線の波長(nm)を与えるとブラッグ条件を満たす仮想的な回折角(2θ)を返す
    /// </summary>
    /// <param name="wavelength"></param>
    /// <returns></returns>
    public static double[] DToTwoThetaInDegree(IEnumerable<double> d, double wavelength) => d.Select(e => 2 * Math.Asin(wavelength / 2.0 / e) / Math.PI * 180).ToArray();


    #endregion 横軸を変換するメソッド群
}
#endregion

#region 古いDiffractionProfileクラス 後方互換性のために維持
[Serializable]
public class DiffractionProfile : ICloneable
{
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

    //Masking関連ここから
    [Serializable]
    public class MaskingRange
    {
        public double[] X = new double[2];
        public override string ToString()
            => X[0] < X[1] ? $"{X[0]:g8} - {X[1]:g8}" : $"{X[1]:g8} - {X[0]:g8}";
    }

    public List<MaskingRange> maskingRanges = [];
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
    public Profile OriginalProfile;//ソースのプロファイル

    public PointD[] BgPoints;

    [XmlIgnore]
    public Profile ConvertedProfile;//軸変換後のプロファイル

    [XmlIgnore]
    public Profile InterpolatedProfile;//

    [XmlIgnore]
    public Profile SmoothedProfile;//

    [XmlIgnore]
    public Profile Kalpha2RemovedProfile;//

    [XmlIgnore]
    public Profile BackgroundProfile;

    [XmlIgnore]
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


    public DiffractionProfile2 ConvertToDiffractionProfile2()
    {
        return new DiffractionProfile2()
        {
            BackgroundProfile = BackgroundProfile,
            BackgroundReferrenceProfile = BackgroundReferrenceProfile,
            BackgroundReferrenceScale = BackgroundReferrenceScale,
            BgMode = BgMode,
            BgPoints = BgPoints,
            BgPointsNumber = BgPointsNumber,
            ColorARGB = ColorARGB,
            Comment = Comment,
            IsLPOmain = IsLPOmain,
            IsLPOchild = IsLPOchild,
            ConvertedProfile = ConvertedProfile,
            DoesBandpassFilter = DoesBandpassFilter,
            DoesHighPath = DoesHighPath,
            DoesLowPath = DoesLowPath,
            DoesMaskAndInterpolate = DoesMaskAndInterpolate,
            DoesNormarizeIntensity = DoesNormarizeIntensity,
            DoesRemoveKalpha2 = DoesRemoveKalpha2,
            DoesSmoothing = DoesSmoothing,
            DoesTwoThetaOffset = DoesTwoThetaOffset,
            DstProperty = new HorizontalAxisProperty(DestAxisMode, WaveSource, WaveColor,
      DestWaveLength, XrayElementNumber, XrayLine, ElectronAccVolatage, DestTakeoffAngle, DestTofAngle, DestTofLength,
      AngleUnitEnum.Degree, LengthUnitEnum.Angstrom, LengthUnitEnum.NanoMeterInverse, EnergyUnitEnum.eV, TimeUnitEnum.MicroSecond),
            ExposureTime = ExposureTime,
            HighPathLimit = HighPathLimit,
            ImageArray = ImageArray,
            ImageHeight = ImageHeight,
            ImageScale = ImageScale,
            ImageWidth = ImageWidth,
            InterpolatedProfile = InterpolatedProfile,
            InterpolationOrder = InterpolationOrder,
            InterpolationPoints = InterpolationPoints,
            IsCPS = IsCPS,
            IsLogIntensity = IsLogIntensity,
            IsShiftX = IsShiftX,
            Kalpha1 = Kalpha1,
            Kalpha2 = Kalpha2,
            Kalpha2RemovedProfile = Kalpha2RemovedProfile,
            LineWidth = LineWidth,
            LowPathLimit = LowPathLimit,
            maskingRanges = [],
            Mode = Mode,
            Name = Name,
            NormarizeAsAverage = NormarizeAsAverage,
            NormarizeIntensity = NormarizeIntensity,
            NormarizeRangeEnd = NormarizeRangeEnd,
            NormarizeRangeStart = NormarizeRangeStart,
            Profile = Profile,
            SazitkyGorayM = SazitkyGorayM,
            SazitkyGorayN = SazitkyGorayN,
            ShiftX = ShiftX,
            SmoothedProfile = SmoothedProfile,
            SourceProfile = OriginalProfile,
            SrcProperty = new HorizontalAxisProperty(SrcAxisMode, WaveSource, WaveColor,
      SrcWaveLength, 0, XrayLine, ElectronAccVolatage, SrcTakeoffAngle, SrcTofAngle, SrcTofLength,
      AngleUnitEnum.Degree, LengthUnitEnum.Angstrom, LengthUnitEnum.NanoMeterInverse, EnergyUnitEnum.eV, TimeUnitEnum.MicroSecond),
            SubtractBackground = SubtractBackground,
            TwoThetaOffsetCoeff0 = TwoThetaOffsetCoeff0,
            TwoThetaOffsetCoeff1 = TwoThetaOffsetCoeff1,
            TwoThetaOffsetCoeff2 = TwoThetaOffsetCoeff2,
        };

    }


    public DiffractionProfile()
    {
        BgPoints = [];

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


}

#endregion
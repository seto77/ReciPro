using MemoryPack;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Crystallography;

//必要最小限の情報だけを保存するクラス
//[ProtoContract]
[Serializable()]
[MemoryPackable]
public partial class Crystal2
{
    #region フィールド プライベートメンバーの場合[MemoryPackInclude]が必要
    [MemoryPackInclude]
    private byte[][] cellBytes;

    public int argb;

    public float density;

    public string name;

    public string note;

    public string jour;

    public string auth;

    public string sect;

    public string formula;//計算可能な場合は。

    public short sym;

    public List<Atoms2> atoms;

    public float[] d;//強度8位までのd値

    public string fileName;

    #endregion

    #region プロパティ
    /// <summary>
    /// a,b,c,alpha,beta,gammaの順番. 単位はAと度. エラーは 「9.726|5|」のような形式で表現
    /// </summary>
    [MemoryPackIgnore]
    public string[] CellTexts
    {
        get => cellBytes == null ? null : Array.ConvertAll(cellBytes, ToString);
        set
        {
            if (value != null)
                cellBytes = Array.ConvertAll(value, ToBytes);
        }
    }


    /// <summary>
    /// a,b,c,α,β,γ の順番. Getのみ. 長さはA, 角度は度単位.
    /// </summary>
    [MemoryPackIgnore]
    public ((double A, double B, double C, double Alpha, double Beta, double Gamma) Values, (double A, double B, double C, double Alpha, double Beta, double Gamma) Errors) Cell
    {
        get
        {
            var c = CellTexts.Select(t => Decompose(t)).ToArray();
            return ((c[0].Value, c[1].Value, c[2].Value, c[3].Value, c[4].Value, c[5].Value),
                     (c[0].Error, c[1].Error, c[2].Error, c[3].Error, c[4].Error, c[5].Error));
        }
    }

    /// <summary>
    /// a,b,c,α,β,γ の順番. Getのみ. 長さはA, 角度は度単位. エラーの値は含まない.
    /// </summary>
    [MemoryPackIgnore]
    public (double A, double B, double C, double Alpha, double Beta, double Gamma) CellOnlyValue => ((
                DecomposeOnlyValue(CellTexts[0]), DecomposeOnlyValue(CellTexts[1]), DecomposeOnlyValue(CellTexts[2]),
                DecomposeOnlyValue(CellTexts[3]), DecomposeOnlyValue(CellTexts[4]), DecomposeOnlyValue(CellTexts[5])));


    /// <summary>
    /// a,b,c,α,β,γ の順番. Getのみ. 長さはnm, 角度はradian.
    /// </summary>
    [MemoryPackIgnore]
    public ((double A, double B, double C, double Alpha, double Beta, double Gamma) Values, (double A, double B, double C, double Alpha, double Beta, double Gamma) Errors) Cell_nm_radian
    {
        get
        {
            var (Values, Errors) = Cell;
            return ((Values.A / 10, Values.B / 10, Values.C / 10, Values.Alpha / 180 * Math.PI, Values.Beta / 180 * Math.PI, Values.Gamma / 180 * Math.PI),
                    (Errors.A / 10, Errors.B / 10, Errors.C / 10, Errors.Alpha / 180 * Math.PI, Errors.Beta / 180 * Math.PI, Errors.Gamma / 180 * Math.PI));
        }
    }

    /// <summary>
    /// a,b,c,α,β,γ の順番. Getのみ. 長さはnm, 角度はradian. エラーの値は含まない.
    /// </summary>
    [MemoryPackIgnore]
    public (double A, double B, double C, double Alpha, double Beta, double Gamma) CellOnlyValue_nm_radian
    {
        get
        {
            var (A, B, C, Alpha, Beta, Gamma) = CellOnlyValue;
            return ((A / 10, B / 10, C / 10, Alpha / 180 * Math.PI, Beta / 180 * Math.PI, Gamma / 180 * Math.PI));
        }
    }
    #endregion

    public Crystal2()
    {
    }

    public Crystal ToCrystal() => GetCrystal(this);

    public static Crystal GetCrystal(Crystal2 c)
    {
        if (c == null)
            return null;

        var cell = c.Cell_nm_radian;

        var atom = new List<Atoms>();
        foreach (Atoms2 a in c.atoms)
        {
            var pos = a.PositionTexts.Select(x => Decompose(x, c.sym)).ToArray();
            var occ = Decompose(a.OccText);
            var iso = Decompose(a.IsoText);
            //Atoms2の単位はA^2なので、nm^2に変換
            iso = (iso.Value / 100, double.IsNaN(iso.Error) ? iso.Error : iso.Error / 100);

            (double Value, double Error)[] aniso = a.AnisoTexts != null ? a.AnisoTexts.Select(x => Decompose(x)).ToArray() :
                 new[] { (0.0, double.NaN), (0.0, double.NaN), (0.0, double.NaN), (0.0, double.NaN), (0.0, double.NaN), (0.0, double.NaN) };

            var anisoValues = a.IsU ? aniso.Select(an => an.Value / 100).ToArray() : aniso.Select(an => an.Value).ToArray();
            var anisoErrors = a.IsU ? aniso.Select(an => an.Error / 100).ToArray() : aniso.Select(an => an.Error).ToArray();
            var _atom = new Atoms(
                    a.Label, a.AtomNo, a.SubXray, a.SubElectron, null, c.sym,
                    new Vector3D(pos[0].Value, pos[1].Value, pos[2].Value, false),
                    new Vector3D(pos[0].Error, pos[1].Error, pos[2].Error, false),
                    occ.Value, occ.Error,
                    new DiffuseScatteringFactor(a.IsU ? DiffuseScatteringFactor.Type.U : DiffuseScatteringFactor.Type.B, a.IsIso,
                        iso.Value, iso.Error, anisoValues, anisoErrors, cell.Values)
                    );
            atom.Add(_atom);
            atom[^1].ResetVesta();

            //AtomNoが255(重水素D)だった時の処理
            if (atom[^1].AtomicNumber == 255)
            {
                atom[^1].AtomicNumber = 1;
                atom[^1].Isotope = [0.0, 100.0, 0.0];
            }
        }

        var bonds = Bonds.GetVestaBonds(atom.Select(a => a.AtomicNumber));

        return new Crystal(cell.Values, cell.Errors, c.sym, c.name, System.Drawing.Color.FromArgb(c.argb), new Matrix3D(), [.. atom], (c.note, c.auth, c.jour, c.sect), bonds);
    }

    public static Crystal2 FromCrystal(Crystal c)
    {
        if (c == null) return null;
        var c2 = new Crystal2
        {
            sym = (short)c.SymmetrySeriesNumber,
            name = c.Name,
            note = c.Note,
            argb = c.Argb,
            auth = c.PublAuthorName,
            sect = c.PublSectionTitle,
            jour = c.Journal,
            formula = c.ChemicalFormulaSum,
            density = (float)c.Density,
            CellTexts = [
                Compose(c.A * 10, c.A_err * 10), Compose(c.B * 10, c.B_err * 10), Compose(c.C * 10, c.C_err * 10),
                Compose(c.Alpha /Math.PI*180, c.Alpha_err/Math.PI*180), Compose(c.Beta /Math.PI*180, c.Beta_err/Math.PI*180), Compose(c.Gamma /Math.PI*180, c.Gamma_err/Math.PI*180) ],
            atoms = []
        };

        foreach (Atoms a in c.Atoms)
        {
            var atom2 = new Atoms2
            {
                Label = a.Label,
                AtomNo = (byte)a.AtomicNumber,
                SubXray = (byte)a.SubNumberXray,
                SubElectron = (byte)a.SubNumberElectron,
                PositionTexts = [Compose(a.X, a.X_err), Compose(a.Y, a.Y_err), Compose(a.Z, a.Z_err)],
                OccText = Compose(a.Occ, a.Occ_err),
                IsIso = a.Dsf.UseIso,
                IsU = a.Dsf.OriginalType == DiffuseScatteringFactor.Type.U,
                IsoText = Compose(a.Dsf.Iso * 100, a.Dsf.Iso_err * 100),
            };

            atom2.AnisoTexts = atom2.IsU ?
                [ Compose(a.Dsf.Aniso11*100, a.Dsf.Aniso11_err*100),
                            Compose(a.Dsf.Aniso22*100, a.Dsf.Aniso22_err*100),
                            Compose(a.Dsf.Aniso33*100, a.Dsf.Aniso33_err*100),
                            Compose(a.Dsf.Aniso12*100, a.Dsf.Aniso12_err*100),
                            Compose(a.Dsf.Aniso23*100, a.Dsf.Aniso23_err*100),
                            Compose(a.Dsf.Aniso31*100, a.Dsf.Aniso31_err*100)] :
                [ Compose(a.Dsf.Aniso11, a.Dsf.Aniso11_err),
                            Compose(a.Dsf.Aniso22, a.Dsf.Aniso22_err),
                            Compose(a.Dsf.Aniso33, a.Dsf.Aniso33_err),
                            Compose(a.Dsf.Aniso12, a.Dsf.Aniso12_err),
                            Compose(a.Dsf.Aniso23, a.Dsf.Aniso23_err),
                            Compose(a.Dsf.Aniso31, a.Dsf.Aniso31_err)];

            c2.atoms.Add(atom2);
        }
        return c2;
    }

    [MemoryPackIgnore]
    private static readonly CultureInfo culture = CultureInfo.InvariantCulture;
    [MemoryPackIgnore]
    private static readonly NumberStyles style = NumberStyles.Number;
    [MemoryPackIgnore]
    private static readonly StringComparison Ord = StringComparison.Ordinal;
    [MemoryPackIgnore]
    static readonly string[] toStringDic =
        [
            #region 
            "00"
            ,"10"
            ,"20"
            ,"30"
            ,"40"
            ,"50"
            ,"60"
            ,"70"
            ,"80"
            ,"90"
            ,".0"
            ,"/0"
            ,"-0"
            ,"|0"
            ,"E0"
            ,"0"
            ,"01"
            ,"11"
            ,"21"
            ,"31"
            ,"41"
            ,"51"
            ,"61"
            ,"71"
            ,"81"
            ,"91"
            ,".1"
            ,"/1"
            ,"-1"
            ,"|1"
            ,"E1"
            ,"1"
            ,"02"
            ,"12"
            ,"22"
            ,"32"
            ,"42"
            ,"52"
            ,"62"
            ,"72"
            ,"82"
            ,"92"
            ,".2"
            ,"/2"
            ,"-2"
            ,"|2"
            ,"E2"
            ,"2"
            ,"03"
            ,"13"
            ,"23"
            ,"33"
            ,"43"
            ,"53"
            ,"63"
            ,"73"
            ,"83"
            ,"93"
            ,".3"
            ,"/3"
            ,"-3"
            ,"|3"
            ,"E3"
            ,"3"
            ,"04"
            ,"14"
            ,"24"
            ,"34"
            ,"44"
            ,"54"
            ,"64"
            ,"74"
            ,"84"
            ,"94"
            ,".4"
            ,"/4"
            ,"-4"
            ,"|4"
            ,"E4"
            ,"4"
            ,"05"
            ,"15"
            ,"25"
            ,"35"
            ,"45"
            ,"55"
            ,"65"
            ,"75"
            ,"85"
            ,"95"
            ,".5"
            ,"/5"
            ,"-5"
            ,"|5"
            ,"E5"
            ,"5"
            ,"06"
            ,"16"
            ,"26"
            ,"36"
            ,"46"
            ,"56"
            ,"66"
            ,"76"
            ,"86"
            ,"96"
            ,".6"
            ,"/6"
            ,"-6"
            ,"|6"
            ,"E6"
            ,"6"
            ,"07"
            ,"17"
            ,"27"
            ,"37"
            ,"47"
            ,"57"
            ,"67"
            ,"77"
            ,"87"
            ,"97"
            ,".7"
            ,"/7"
            ,"-7"
            ,"|7"
            ,"E7"
            ,"7"
            ,"08"
            ,"18"
            ,"28"
            ,"38"
            ,"48"
            ,"58"
            ,"68"
            ,"78"
            ,"88"
            ,"98"
            ,".8"
            ,"/8"
            ,"-8"
            ,"|8"
            ,"E8"
            ,"8"
            ,"09"
            ,"19"
            ,"29"
            ,"39"
            ,"49"
            ,"59"
            ,"69"
            ,"79"
            ,"89"
            ,"99"
            ,".9"
            ,"/9"
            ,"-9"
            ,"|9"
            ,"E9"
            ,"9"
            ,"0."
            ,"1."
            ,"2."
            ,"3."
            ,"4."
            ,"5."
            ,"6."
            ,"7."
            ,"8."
            ,"9."
            ,".."
            ,"/."
            ,"-."
            ,"|."
            ,"E."
            ,"."
            ,"0/"
            ,"1/"
            ,"2/"
            ,"3/"
            ,"4/"
            ,"5/"
            ,"6/"
            ,"7/"
            ,"8/"
            ,"9/"
            ,"./"
            ,"//"
            ,"-/"
            ,"|/"
            ,"E/"
            ,"/"
            ,"0-"
            ,"1-"
            ,"2-"
            ,"3-"
            ,"4-"
            ,"5-"
            ,"6-"
            ,"7-"
            ,"8-"
            ,"9-"
            ,".-"
            ,"/-"
            ,"--"
            ,"|-"
            ,"E-"
            ,"-"
            ,"0|"
            ,"1|"
            ,"2|"
            ,"3|"
            ,"4|"
            ,"5|"
            ,"6|"
            ,"7|"
            ,"8|"
            ,"9|"
            ,".|"
            ,"/|"
            ,"-|"
            ,"||"
            ,"E|"
            ,"|"
            ,"0E"
            ,"1E"
            ,"2E"
            ,"3E"
            ,"4E"
            ,"5E"
            ,"6E"
            ,"7E"
            ,"8E"
            ,"9E"
            ,".E"
            ,"/E"
            ,"-E"
            ,"|E"
            ,"EE"
            ,"E"
            ,"0"
            ,"1"
            ,"2"
            ,"3"
            ,"4"
            ,"5"
            ,"6"
            ,"7"
            ,"8"
            ,"9"
            ,"."
            ,"/"
            ,"-"
            ,"|"
            ,"E"
            ,""
            #endregion
        ];

    //静的コンストラクタ
    //static Crystal2()
    //{
    //    for (int i = 0; i < 16; i++)
    //        for (int j = 0; j < 16; j++)
    //        {
    //            var s1 = i == 15 ? "" : new string(new[] { toCharDic[i] });
    //            var s2 = j == 15 ? "" : new string(new[] { toCharDic[j] });

    //            toStringDic.Add((byte)(i + j * 16), s1 + s2);
    //        }
    //}

    [MemoryPackIgnore]
    static readonly FrozenDictionary<char, byte> toByteDic = new Dictionary<char, byte>()
    {
        { '0', 0 },
        { '1', 1 },
        { '2', 2 },
        { '3', 3 },
        { '4', 4 },
        { '5', 5 },
        { '6', 6 },
        { '7', 7 },
        { '8', 8 },
        { '9', 9 },
        { '.', 10 },
        { '/', 11 },
        { '-', 12 },
        { '(', 13 },
        { ')', 13 },
        { 'E', 14 },
    }.ToFrozenDictionary();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static byte[] ToBytes(string s)
    {
        s = s.Trim().TrimEnd().Replace('e', 'E');
        if (s.Length == 0 || s == "?" || s == "NaN")
            return [(byte)255];
        else if (s == "0")
            return [(byte)(240 + 0)];
        else
        {
            if (s.StartsWith("0.", Ord))
            {
                if (s == "0.")
                    s = "0";
                else
                    s = s[1..];
            }
            else if (s.StartsWith("-0.", Ord))
                s = s.Replace("-0.", "-.");
            try
            {
                var result = new byte[(s.Length + 1) / 2];
                for (int i = 0; i < s.Length; i += 2)
                {
                    if (i + 1 < s.Length)
                    {
                        if (toByteDic.TryGetValue(s[i], out var b1) && toByteDic.TryGetValue(s[i + 1], out var b2))
                            result[i / 2] = (byte)(b1 + (b2 << 4));
                    }
                    else
                    {
                        if (toByteDic.TryGetValue(s[i], out var b1))
                            result[i / 2] = (byte)(b1 + 240);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                if (AssemblyState.IsDebug)
                    MessageBox.Show(e.ToString());
                return [(byte)255];
            }
        }
    }

    public static string ToString(byte[] bytes)
    {
        var sb = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
            sb.Append(toStringDic[b]);
        return sb.ToString();
    }

    private static (double Value, double Error) Decompose(string str) => Decompose(str, false);
    public static (double Value, double Error) Decompose(string str, int sgnum) => Decompose(str, sgnum >= 430 && sgnum <= 488);


    /// <summary>
    /// 9.726|5|, 1.234|12|E-6 のような文字列を、ValueとErrorに分解してタプルで返す. 
    /// 例外の場合は(double.NaN,double.NaN). 括弧が存在しない場合、Errorはdouble.NaN. 
    /// </summary>
    /// <param name="str"></param>
    /// <param name="IsHex"></param>
    /// <returns></returns>
    public static (double Value, double Error) Decompose(string str, bool IsHex = false)
    {
        var expValue = 1.0;
        int i;
        if ((i = str.IndexOf("E", Ord)) > 0)
        {
            _ = double.TryParse("1" + str[i..], style, culture, out expValue);
            str = str[..i];
        }
        string valStr;
        double err;
        if ((i = str.IndexOf("|", Ord)) > 0)
        {
            valStr = str[0..i];
            if (str.Length - 1 > i && double.TryParse(str[(i + 1)..^1], style, culture, out err))
            {
                var j = valStr.IndexOf(".", Ord);
                if (j >= 0 && valStr.Length - j - 1 > 0)
                    err *= Math.Pow(10, -valStr.Length + j + 1);
            }
            else
                err = double.NaN;
        }
        else
        {
            valStr = str;
            err = double.NaN;
        }

        if (valStr == "0")
            return (0, err * expValue);

        if (IsHex)
        {
            if (str.Contains(".0833"))
                return (1.0 / 12.0, err);
            else if (str.Contains(".167") || str.Contains(".1667") || str.Contains(".16667") || str.Contains(".166667"))
                return (1.0 / 6.0, err);
            else if (str.Contains(".333"))
                return (1.0 / 3.0, err);
            else if (str.Contains(".667") || str.Contains(".6667") || str.Contains(".66667") || str.Contains(".666667"))
                return (2.0 / 3.0, err);
            else if (str.Contains(".4167") || str.Contains(".41667") || str.Contains(".416667"))
                return (5.0 / 12.0, err);
            else if (str.Contains(".5833"))
                return (7.0 / 12.0, err);
            else if (str.Contains(".8333")|| str.Contains(".83333"))
                return (5.0 / 6.0, err);
            else if (str.Contains(".9167") || str.Contains(".91667") || str.Contains(".916667"))
                return (11.0 / 12.0, err);
        }

        if ((i = valStr.IndexOf("/", Ord)) >= 0)
        {
            if (double.TryParse(valStr.AsSpan()[0..i], style, culture, out var temp0)
                && double.TryParse(valStr.AsSpan()[(i + 1)..^0], style, culture, out var temp1))
                return (temp0 / temp1, double.NaN);
            else
                return (double.NaN, double.NaN);
        }

        return double.TryParse(valStr, style, culture, out var val) ? (val * expValue, err * expValue) : (double.NaN, double.NaN);
    }

    /// <summary>
    /// 9.726|5|, 1.234|12|E-6 のような文字列を、Valueの部分だけ返す. 
    /// 格子定数を変換するときだけに呼ばれる.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static double DecomposeOnlyValue(string str)
    {
        var s = str.AsSpan();
        var expValue = 1.0;
        if (s.Contains('E'))
        {
            int i = s.IndexOf('E');
            _ = double.TryParse("1" + s[i..].ToString(), style, culture, out expValue);
            s = s[..i];
        }

        if (s.Contains('|'))
            s = s[0..s.IndexOf('|')];

        return double.TryParse(s, style, culture, out var val) ? val * expValue : double.NaN;
    }

    public static string Compose(double val, double err = double.NaN)
    {
        if (double.IsNaN(val))
            return "";
        else if (double.IsNaN(err) || err <= 0)
            return val.ToString();
        else
        {
            //まず、誤差を 23E-6 みたいな形にする
            var temp = err.ToString("E1");
            var errStr = temp[..temp.IndexOf("E", Ord)].Replace(".", "");
            var errLog = Convert.ToInt32(temp[(temp.IndexOf("E", Ord) + 1)..]) - 1;

            //vを取りあえず十分な精度で出力する
            var valStr = val.ToString("E15");
            var valLog = Convert.ToInt32(valStr[(valStr.IndexOf("E", Ord) + 1)..]);

            var result = valLog >= errLog ? valStr[..(valLog - errLog + 2)] : val.ToString("E0")[..1];

            if (valLog < errLog)
                for (int i = 0; i < errLog - valLog; i++)
                    errStr += "0";

            result += "(" + errStr + ")";

            if (valLog != 0)
                result += $"E{valLog}";

            return result;
        }
    }
}

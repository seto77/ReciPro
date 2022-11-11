using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MessagePack;


namespace Crystallography;

//必要最小限の情報だけを保存するクラス
//[ProtoContract]
[Serializable()]
[MessagePackObject]
public class Crystal2
{
    #region フィールド シリアル化対象 [Key(#)]が必須
    [Key(0)]
    private byte[][] cellBytes;
    [Key(6)]
    public int argb;
    [Key(7)]
    public float density;
    [Key(8)]
    public string name;
    [Key(9)]
    public string note;
    [Key(10)]
    public string jour;
    [Key(11)]
    public string auth;
    [Key(12)]
    public string sect;
    [Key(13)]
    public string formula;//計算可能な場合は。
    [Key(14)]
    public short sym;
    [Key(15)]
    public List<Atoms2> atoms;
    [Key(17)]
    public float[] d;//強度8位までのd値
    [Key(18)]
    public string fileName;

    #endregion

    #region プロパティ
    /// <summary>
    /// a,b,c,alpha,beta,gammaの順番. 単位はAと度. エラーは 「9.726|5|」のような形式で表現
    /// </summary>
    [IgnoreMember]
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
    [IgnoreMember]
    public ((double A, double B, double C, double Alpha, double Beta, double Gamma) Values, (double A, double B, double C, double Alpha, double Beta, double Gamma) Errors) Cell
    {
        get
        {
            var c = CellTexts.Select(t => Decompose2(t)).ToArray();
            return ((c[0].Value, c[1].Value, c[2].Value, c[3].Value, c[4].Value, c[5].Value),
                     (c[0].Error, c[1].Error, c[2].Error, c[3].Error, c[4].Error, c[5].Error));
        }
    }

    /// <summary>
    /// a,b,c,α,β,γ の順番. Getのみ. 長さはnm, 角度はradian.
    /// </summary>
    [IgnoreMember]
    public ((double A, double B, double C, double Alpha, double Beta, double Gamma) Values, (double A, double B, double C, double Alpha, double Beta, double Gamma) Errors) Cell_nm_radian
    {
        get
        {
            var (Values, Errors) = Cell;
            return ((Values.A / 10, Values.B / 10, Values.C / 10, Values.Alpha / 180 * Math.PI, Values.Beta / 180 * Math.PI, Values.Gamma / 180 * Math.PI),
                    (Errors.A / 10, Errors.B / 10, Errors.C / 10, Errors.Alpha / 180 * Math.PI, Errors.Beta / 180 * Math.PI, Errors.Gamma / 180 * Math.PI));
        }
    }
    #endregion

    public Crystal2()
    {
        atoms = new List<Atoms2>();
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
            atom.Add(_atom );
            atom[^1].ResetVesta();

            //AtomNoが255(重水素D)だった時の処理
            if(atom[^1].AtomicNumber==255)
            {
                atom[^1].AtomicNumber = 1;
                atom[^1].Isotope = new[] { 0.0, 100.0, 0.0 };
            }
        }

        var bonds = Bonds.GetVestaBonds(atom.Select(a => a.AtomicNumber));

        var crystal = new Crystal(cell.Values, cell.Errors,
            c.sym, c.name, System.Drawing.Color.FromArgb(c.argb), new Matrix3D(),
            atom.ToArray(), (c.note, c.auth, GetFullJournal(c.jour), GetFullTitle(c.sect)), bonds);

        return crystal;
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
            sect = GetShortTitle(c.PublSectionTitle),
            jour = GetShortJournal(c.Journal),
            formula = c.ChemicalFormulaSum,
            density = (float)c.Density,
            CellTexts = new[] {
                Compose(c.A * 10, c.A_err * 10), Compose(c.B * 10, c.B_err * 10), Compose(c.C * 10, c.C_err * 10),
                Compose(c.Alpha /Math.PI*180, c.Alpha_err/Math.PI*180), Compose(c.Beta /Math.PI*180, c.Beta_err/Math.PI*180), Compose(c.Gamma /Math.PI*180, c.Gamma_err/Math.PI*180) },
            atoms = new List<Atoms2>()
        };

        foreach (Atoms a in c.Atoms)
        {
            var atom2 = new Atoms2
            {
                Label = a.Label,
                AtomNo = (byte)a.AtomicNumber,
                SubXray = (byte)a.SubNumberXray,
                SubElectron = (byte)a.SubNumberElectron,
                PositionTexts = new[] { Compose(a.X, a.X_err), Compose(a.Y, a.Y_err), Compose(a.Z, a.Z_err) },
                OccText = Compose(a.Occ, a.Occ_err),
                IsIso = a.Dsf.UseIso,
                IsU = a.Dsf.OriginalType == DiffuseScatteringFactor.Type.U,
                IsoText = Compose(a.Dsf.Iso * 100, a.Dsf.Iso_err * 100),
            };

            atom2.AnisoTexts = atom2.IsU ?
                new[] { Compose(a.Dsf.Aniso11*100, a.Dsf.Aniso11_err*100),
                            Compose(a.Dsf.Aniso22*100, a.Dsf.Aniso22_err*100),
                            Compose(a.Dsf.Aniso33*100, a.Dsf.Aniso33_err*100),
                            Compose(a.Dsf.Aniso12*100, a.Dsf.Aniso12_err*100),
                            Compose(a.Dsf.Aniso23*100, a.Dsf.Aniso23_err*100),
                            Compose(a.Dsf.Aniso31*100, a.Dsf.Aniso31_err*100)} :
                new[] { Compose(a.Dsf.Aniso11, a.Dsf.Aniso11_err),
                            Compose(a.Dsf.Aniso22, a.Dsf.Aniso22_err),
                            Compose(a.Dsf.Aniso33, a.Dsf.Aniso33_err),
                            Compose(a.Dsf.Aniso12, a.Dsf.Aniso12_err),
                            Compose(a.Dsf.Aniso23, a.Dsf.Aniso23_err),
                            Compose(a.Dsf.Aniso31, a.Dsf.Aniso31_err)};

            c2.atoms.Add(atom2);
        }
        return c2;
    }

    public static string GetFullJournal(string shortJournal)
    {
        if (shortJournal != null && shortJournal.Contains("##"))
        {
            string number = shortJournal.Substring(shortJournal.IndexOf("##", StringComparison.Ordinal), 4);
            string journal = number switch
            {
                #region ## => 雑誌名
                "##01" => "American Mineralogist",
                "##02" => "Canadian Mineralogist",
                "##03" => "Acta Crystallographica",
                "##04" => "Bulletin de la Societe Francaise de Mineralogie et de Cristallographie",
                "##05" => "Bulletin of the Chemical Society of Japan",
                "##06" => "Canadian Journal of Chemistry",
                "##07" => "Chemische Berichte",
                "##08" => "Clays and Clay Minerals",
                "##09" => "Comptes Rendus Hebdomadaires des Seances de l'Academie des Sciences",
                "##10" => "Contributions to Mineralogy and Petrology",
                "##11" => "Doklady Akademii Nauk SSSR",
                "##12" => "Dopovidi Akademii Nauk Ukrains'koi RSR Seriya B=> Geologichni Khimichni ta Biologichni Nauki",
                "##13" => "European Journal of Mineralogy",
                "##14" => "Gazzetta Chimica Italiana",
                "##15" => "Inorganic Chemistry",
                "##16" => "Inorganica Chimica Acta",
                "##17" => "Izvestiya Akademii Nauk SSSR Neorganicheskie Materialy",
                "##18" => "Journal of Chemical Physics",
                "##19" => "Journal of Inorganic and Nuclear Chemistry",
                "##20" => "Journal of Physical Chemistry",
                "##21" => "Journal of Solid State Chemistry",
                "##22" => "Journal of the American Ceramic Society",
                "##23" => "Journal of the American Chemical Society",
                "##24" => "Journal of the Chemical Society",
                "##25" => "Journal of the Less-Common Metals",
                "##26" => "Kristallografiya",
                "##27" => "Materials Research Bulletin",
                "##28" => "Mineralogical Magazine",
                "##29" => "Nature",
                "##30" => "Naturwissenschaften",
                "##31" => "Neues Jahrbuch fuer Mineralogie. Monatshefte",
                "##32" => "Neues Jahrbuch fur Mineralogie, Monatshefte",
                "##33" => "Physics and Chemistry of Minerals",
                "##34" => "Zeitschrift fuer Anorganische und Allgemeine Chemie",
                "##35" => "Zeitschrift fuer Kristallographie",
                "##36" => "Zeitschrift fur Kristallographie",
                "##37" => "Comptes Rendus Hebdomadaires des Seances de lAcademie des Sciences",
                "##38" => "Dalton transactions",
                "##39" => "Journal of Organic Chemistry",
                "##40" => "Organic & Biomolecular Chemistry",
                "##41" => "Organometallics",
                "##43" => "Chemical communications(Cambridge, England)",
                "##44" => "Materials Chemistry Frontiers",
                "##45" => "Monatshefte fuer Chemie und verwandte Teile anderer Wissenschaften",
                "##46" => "New Journal of Chemistry",
                "##47" => "Organic letters",
                _ => "",
                #endregion
            };
            return shortJournal.Replace(number, journal);
        }
        else
            return shortJournal;
    }

    public static string GetShortJournal(string fullJournal)
    {
        string journal = "";
        if (fullJournal != null)
            journal = fullJournal;
        #region 雑誌名 => ##数値
        journal = Regex.Replace(journal, "American Mineralogist", "##01", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Canadian Mineralogist", "##02", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Acta Crystallographica", "##03", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Bulletin de la Societe Francaise de Mineralogie et de Cristallographie", "##04", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Bulletin of the Chemical Society of Japan", "##05", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Canadian Journal of Chemistry", "##06", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Chemische Berichte", "##07", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Clays and Clay Minerals", "##08", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Comptes Rendus Hebdomadaires des Seances de l'Academie des Sciences", "##09", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Contributions to Mineralogy and Petrology", "##10", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Doklady Akademii Nauk SSSR", "##11", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Dopovidi Akademii Nauk Ukrains'koi RSR Seriya B: Geologichni Khimichni ta Biologichni Nauki", "##12", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "European Journal of Mineralogy", "##13", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Gazzetta Chimica Italiana", "##14", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Inorganic Chemistry", "##15", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Inorganica Chimica Acta", "##16", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Izvestiya Akademii Nauk SSSR Neorganicheskie Materialy", "##17", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Journal of Chemical Physics", "##18", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Journal of Inorganic and Nuclear Chemistry", "##19", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Journal of Physical Chemistry", "##20", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Journal of Solid State Chemistry", "##21", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Journal of the American Ceramic Society", "##22", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Journal of the American Chemical Society", "##23", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Journal of the Chemical Society", "##24", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Journal of the Less-Common Metals", "##25", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Kristallografiya", "##26", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Materials Research Bulletin", "##27", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Mineralogical Magazine", "##28", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Nature", "##29", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Naturwissenschaften", "##30", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Neues Jahrbuch fuer Mineralogie. Monatshefte", "##31", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Neues Jahrbuch fur Mineralogie, Monatshefte", "##31", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Physics and Chemistry of Minerals", "##33", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Zeitschrift fuer Anorganische und Allgemeine Chemie", "##34", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Zeitschrift fuer Kristallographie", "##35", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Zeitschrift fur Kristallographie", "##36", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Comptes Rendus Hebdomadaires des Seances de lAcademie des Sciences", "##37", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Dalton transactions", "##38", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Journal of Organic Chemistry", "##39", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Organic &amp;  Biomolecular Chemistry", "##40", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Organic &amp; Biomolecular Chemistry", "##40", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Organometallics", "##41");
        journal = Regex.Replace(journal, "Chemical communications(Cambridge, England)", "##43", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Materials Chemistry Frontiers", "##44", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Monatshefte fuer Chemie und verwandte Teile anderer Wissenschaften", "##45", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "New Journal of Chemistry", "##46", RegexOptions.IgnoreCase);
        journal = Regex.Replace(journal, "Organic letters", "##47", RegexOptions.IgnoreCase);
        #endregion
        return journal;
    }

    public static string GetFullTitle(string shortTitle)
    {
        string title = shortTitle;
        if (title.Contains("##"))
        {
            #region ##数値 => キーワード
            title = title.Replace("##01", "The crystal structure");
            title = title.Replace("##02", "Crystal structure of");
            title = title.Replace("##03", "crystal structure of");
            title = title.Replace("##04", "powder diffraction");
            title = title.Replace("##05", "Rietveld refinement of");
            title = title.Replace("##06", "Second edition. Interscience Publishers, New York, New York Note:");
            title = title.Replace("##07", "Single-crystal structure refinements");
            title = title.Replace("##08", "Structural variation");
            title = title.Replace("##09", "Structure refinement of");
            title = title.Replace("##10", "Structure refinements of");
            title = title.Replace("##11", "structure refinement of");
            title = title.Replace("##12", "structure refinements of");
            title = title.Replace("##13", "_cod_database_code");
            title = title.Replace("##14", "_database_code_amcsd");
            #endregion
        }
        return title;
    }

    public static string GetShortTitle(string fullTitle)
    {
        string title = fullTitle;

        #region キーワード => ##数値
        title = title.Replace("The crystal structure", "##01");
        title = title.Replace("Crystal structure of", "##02");
        title = title.Replace("crystal structure of", "##03");
        title = title.Replace("powder diffraction", "##04");
        title = title.Replace("Rietveld refinement of", "##05");
        title = title.Replace("Second edition. Interscience Publishers, New York, New York Note:", "##06");
        title = title.Replace("Single-crystal structure refinements", "##07");
        title = title.Replace("Structural variation", "##08");
        title = title.Replace("Structure refinement of", "##09");
        title = title.Replace("Structure refinements of", "##10");
        title = title.Replace("structure refinement of", "##11");
        title = title.Replace("structure refinements of", "##12");
        title = title.Replace("_cod_database_code", "##13");
        title = title.Replace("_database_code_amcsd", "##14");
        #endregion
        return title;
    }

    static readonly char[] toCharDic = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '/', '-', '|', 'E' };
    static readonly Dictionary<byte, string> toStringDic = new();

    static readonly Dictionary<char, byte> toByteDic = new()
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
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static byte[] ToBytes(string s)
    {
        s = s.Trim().TrimEnd().Replace('e', 'E');
        if (s.Length == 0 || s == "?" || s == "NaN")
            return new[] { (byte)255 };
        else if (s == "0")
            return new[] { (byte)(240 + 0) };
        else
        {
            if (s.StartsWith("0.", StringComparison.Ordinal))
            {
                if (s == "0.")
                    s = "0";
                else
                    s = s[1..];
            }
            else if (s.StartsWith("-0.", StringComparison.Ordinal))
                s = s.Replace("-0.", "-.");
            try
            {
                var result = new List<byte>();
                for (int i = 0; i < s.Length; i += 2)
                {
                    if (i + 1 < s.Length)
                        result.Add((byte)(toByteDic[s[i]] + (toByteDic[s[i + 1]] << 4)));
                    else
                        result.Add((byte)(toByteDic[s[i]] + 240));
                }
                return result.ToArray();
            }
            catch (Exception e)
            {
                if (AssemblyState.IsDebug)
                    MessageBox.Show(e.ToString());
                return new[] { (byte)255 };
            }
        }
    }

    public static string ToString(byte[] bytes)
    {
        if (bytes.Length == 0)
            return "";
        else
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(toStringDic[b]);
            return sb.ToString();
        }
    }

    //静的コンストラクタ
    static Crystal2()
    {
        for (int i = 0; i < 16; i++)
            for (int j = 0; j < 16; j++)
            {
                var s1 = i == 15 ? "" : new string(new[] { toCharDic[i] });
                var s2 = j == 15 ? "" : new string(new[] { toCharDic[j] });

                toStringDic.Add((byte)(i + j * 16), s1 + s2);
            }
    }

    private static (double Value, double Error) Decompose2(string str) => Decompose(str, false);
    public static (double Value, double Error) Decompose(string str, int sgnum) => Decompose(str, sgnum >= 430 && sgnum <= 488);

    private static readonly CultureInfo culture = CultureInfo.InvariantCulture;
    private static readonly NumberStyles style = NumberStyles.Number;

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
        if ((i = str.IndexOf("E", StringComparison.Ordinal)) > 0)
        {
            _ = double.TryParse("1" + str[i..], style, culture, out expValue);
            str = str[..i];
        }
        string valStr;
        double err;
        if ((i = str.IndexOf("|", StringComparison.Ordinal)) > 0)
        {
            valStr = str.AsSpan()[0..i].ToString();

            if (double.TryParse(str.AsSpan()[(i + 1)..^1], style, culture, out err))
            {
                var j = valStr.IndexOf(".", StringComparison.Ordinal);
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
            else if (str.Contains(".8333"))
                return (5.0 / 6.0, err);
            else if (str.Contains(".9167") || str.Contains(".91667") || str.Contains(".916667"))
                return (11.0 / 12.0, err);
        }

        if ((i = valStr.IndexOf("/", StringComparison.Ordinal)) >= 0)
        {
            if (double.TryParse(valStr.AsSpan()[0..i], style, culture, out var temp0)
                && double.TryParse(valStr.AsSpan()[(i + 1)..^0], style, culture, out var temp1))
                return (temp0 / temp1, double.NaN);
            else
                return (double.NaN, double.NaN);
        }

        return double.TryParse(valStr, style, culture, out var val) ? (val * expValue, err * expValue) : (double.NaN, double.NaN);
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
            var errStr = temp[..temp.IndexOf("E", StringComparison.Ordinal)].Replace(".", "");
            var errLog = Convert.ToInt32(temp[(temp.IndexOf("E", StringComparison.Ordinal) + 1)..]) - 1;

            //vを取りあえず十分な精度で出力する
            var valStr = val.ToString("E15");
            var valLog = Convert.ToInt32(valStr[(valStr.IndexOf("E", StringComparison.Ordinal) + 1)..]);

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

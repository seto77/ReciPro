using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using MathNet.Numerics;
using MathNet.Numerics.Integration;
//using ProtoBuf;
using MessagePack;


namespace Crystallography
{
    //必要最小限の情報だけを保存するクラス
    //[ProtoContract]
    [Serializable()]
    [MessagePackObject]
    public class Crystal2
    {
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

        /// <summary>
        /// a,b,c,alpha,beta,gammaの順番. 単位はAと度. エラーは 「9.726(5)」のような形式で表現
        /// </summary>
        [IgnoreMember]
        public string[] CellTexts
        {
            get => cellBytes?.Select(x => ToString(x)).ToArray();
            set => cellBytes = value?.Select(x => ToBytes(x)).ToArray();
        }

        /// <summary>
        /// a,b,c,α,β,γ の順番. Getのみ. 長さはA, 角度は度単位.
        /// </summary>
        [IgnoreMember]
        public ((double A, double B, double C, double Alpha, double Beta, double Gamma) Values, (double A, double B, double C, double Alpha, double Beta, double Gamma) Errors) Cell
        {
            get
            {
                var c = CellTexts.Select(x => Decompose(x)).ToArray();
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

        public Crystal2()
        {
            atoms = new List<Atoms2>();
        }

        public Crystal ToCrystal() => GetCrystal(this);

        public static Crystal GetCrystal(Crystal2 c)
        {
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

                atom.Add(
                    new Atoms(
                        a.Label, a.AtomNo, a.SubXray, a.SubElectron, null, c.sym,
                        new Vector3D(pos[0].Value, pos[1].Value, pos[2].Value, false),
                        new Vector3D(pos[0].Error, pos[1].Error, pos[2].Error, false),
                        occ.Value, occ.Error,
                        new DiffuseScatteringFactor(a.IsU ? DiffuseScatteringFactor.Type.U : DiffuseScatteringFactor.Type.B, a.IsIso,
                            iso.Value, iso.Error, anisoValues, anisoErrors, cell.Values)
                        )
                    );
                atom[atom.Count - 1].ResetVesta();
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
            Crystal2 c2 = new Crystal2
            {
                sym = (short)c.SymmetrySeriesNumber,
                name = c.Name,
                note = c.Note,
                argb = c.Argb,
                auth = c.PublAuthorName,
                sect = GetShortTitle(c.PublSectionTitle),
                jour = GetShortJournal(c.Journal),
                formula = c.ChemicalFormulaSum,
                density = (float)c.Density
            };

            c2.CellTexts = new[] {
                Compose(c.A * 10, c.A_err * 10), Compose(c.B * 10, c.B_err * 10), Compose(c.C * 10, c.C_err * 10),
                Compose(c.Alpha /Math.PI*180, c.Alpha_err/Math.PI*180), Compose(c.Beta /Math.PI*180, c.Beta_err/Math.PI*180), Compose(c.Gamma /Math.PI*180, c.Gamma_err/Math.PI*180) };

            c2.atoms = new List<Atoms2>();
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
                string number = shortJournal.Substring(shortJournal.IndexOf("##"), 4);
                string journal = "";
                switch (number)
                {
                    case "##01": journal = "American Mineralogist"; break;
                    case "##02": journal = "Canadian Mineralogist"; break;
                    case "##03": journal = "Acta Crystallographica"; break;
                    case "##04": journal = "Bulletin de la Societe Francaise de Mineralogie et de Cristallographie"; break;
                    case "##05": journal = "Bulletin of the Chemical Society of Japan"; break;
                    case "##06": journal = "Canadian Journal of Chemistry"; break;
                    case "##07": journal = "Chemische Berichte"; break;
                    case "##08": journal = "Clays and Clay Minerals"; break;
                    case "##09": journal = "Comptes Rendus Hebdomadaires des Seances de l'Academie des Sciences"; break;
                    case "##10": journal = "Contributions to Mineralogy and Petrology"; break;
                    case "##11": journal = "Doklady Akademii Nauk SSSR"; break;
                    case "##12": journal = "Dopovidi Akademii Nauk Ukrains'koi RSR Seriya B: Geologichni Khimichni ta Biologichni Nauki"; break;
                    case "##13": journal = "European Journal of Mineralogy"; break;
                    case "##14": journal = "Gazzetta Chimica Italiana"; break;
                    case "##15": journal = "Inorganic Chemistry"; break;
                    case "##16": journal = "Inorganica Chimica Acta"; break;
                    case "##17": journal = "Izvestiya Akademii Nauk SSSR Neorganicheskie Materialy"; break;
                    case "##18": journal = "Journal of Chemical Physics"; break;
                    case "##19": journal = "Journal of Inorganic and Nuclear Chemistry"; break;
                    case "##20": journal = "Journal of Physical Chemistry"; break;
                    case "##21": journal = "Journal of Solid State Chemistry"; break;
                    case "##22": journal = "Journal of the American Ceramic Society"; break;
                    case "##23": journal = "Journal of the American Chemical Society"; break;
                    case "##24": journal = "Journal of the Chemical Society"; break;
                    case "##25": journal = "Journal of the Less-Common Metals"; break;
                    case "##26": journal = "Kristallografiya"; break;
                    case "##27": journal = "Materials Research Bulletin"; break;
                    case "##28": journal = "Mineralogical Magazine"; break;
                    case "##29": journal = "Nature"; break;
                    case "##30": journal = "Naturwissenschaften"; break;
                    case "##31": journal = "Neues Jahrbuch fuer Mineralogie. Monatshefte"; break;
                    case "##32": journal = "Neues Jahrbuch fur Mineralogie, Monatshefte"; break;
                    case "##33": journal = "Physics and Chemistry of Minerals"; break;
                    case "##34": journal = "Zeitschrift fuer Anorganische und Allgemeine Chemie"; break;
                    case "##35": journal = "Zeitschrift fuer Kristallographie"; break;
                    case "##36": journal = "Zeitschrift fur Kristallographie"; break;
                    case "##37": journal = "Comptes Rendus Hebdomadaires des Seances de lAcademie des Sciences"; break;
                    case "##38": journal = "Dalton transactions"; break;
                    case "##39": journal = "Journal of Organic Chemistry"; break;
                    case "##40": journal = "Organic & Biomolecular Chemistry"; break;
                    case "##41": journal = "Organometallics"; break;
                    case "##43": journal = "Chemical communications(Cambridge, England)"; break;
                    case "##44": journal = "Materials Chemistry Frontiers"; break;
                    case "##45": journal = "Monatshefte fuer Chemie und verwandte Teile anderer Wissenschaften"; break;
                    case "##46": journal = "New Journal of Chemistry"; break;
                    case "##47": journal = "Organic letters"; break;

                }
                return shortJournal.Replace(number, journal);
            }
            return shortJournal;
        }

        public static string GetShortJournal(string fullJournal)
        {
            string journal = "";
            if (fullJournal != null)
                journal = fullJournal;
            journal = journal.Replace("American Mineralogist", "##01");
            journal = journal.Replace("Canadian Mineralogist", "##02");
            journal = journal.Replace("Acta Crystallographica", "##03");
            journal = journal.Replace("Bulletin de la Societe Francaise de Mineralogie et de Cristallographie", "##04");
            journal = journal.Replace("Bulletin of the Chemical Society of Japan", "##05");
            journal = journal.Replace("Canadian Journal of Chemistry", "##06");
            journal = journal.Replace("Chemische Berichte", "##07");
            journal = journal.Replace("Clays and Clay Minerals", "##08");
            journal = journal.Replace("Comptes Rendus Hebdomadaires des Seances de l'Academie des Sciences", "##09");
            journal = journal.Replace("Contributions to Mineralogy and Petrology", "##10");
            journal = journal.Replace("Doklady Akademii Nauk SSSR", "##11");
            journal = journal.Replace("Dopovidi Akademii Nauk Ukrains'koi RSR Seriya B: Geologichni Khimichni ta Biologichni Nauki", "##12");
            journal = journal.Replace("European Journal of Mineralogy", "##13");
            journal = journal.Replace("Gazzetta Chimica Italiana", "##14");
            journal = journal.Replace("Inorganic Chemistry", "##15");
            journal = journal.Replace("Inorganic chemistry", "##15");
            journal = journal.Replace("Inorganica Chimica Acta", "##16");
            journal = journal.Replace("Izvestiya Akademii Nauk SSSR Neorganicheskie Materialy", "##17");
            journal = journal.Replace("Journal of Chemical Physics", "##18");
            journal = journal.Replace("Journal of Inorganic and Nuclear Chemistry", "##19");
            journal = journal.Replace("Journal of Physical Chemistry", "##20");
            journal = journal.Replace("Journal of physical chemistry", "##20");
            journal = journal.Replace("journal of physical chemistry", "##20");
            journal = journal.Replace("Journal of Solid State Chemistry", "##21");
            journal = journal.Replace("Journal of the American Ceramic Society", "##22");
            journal = journal.Replace("Journal of the American Chemical Society", "##23");
            journal = journal.Replace("Journal of the Chemical Society", "##24");
            journal = journal.Replace("Journal of the Less-Common Metals", "##25");
            journal = journal.Replace("Kristallografiya", "##26");
            journal = journal.Replace("Materials Research Bulletin", "##27");
            journal = journal.Replace("Mineralogical Magazine", "##28");
            journal = journal.Replace("Nature", "##29");
            journal = journal.Replace("Naturwissenschaften", "##30");
            journal = journal.Replace("Neues Jahrbuch fuer Mineralogie. Monatshefte", "##31");
            journal = journal.Replace("Neues Jahrbuch fur Mineralogie, Monatshefte", "##32");
            journal = journal.Replace("Physics and Chemistry of Minerals", "##33");
            journal = journal.Replace("Zeitschrift fuer Anorganische und Allgemeine Chemie", "##34");
            journal = journal.Replace("Zeitschrift fuer Kristallographie", "##35");
            journal = journal.Replace("Zeitschrift f\\ur Kristallographie", "##35");
            journal = journal.Replace("Zeitschrift fur Kristallographie", "##36");
            journal = journal.Replace("Comptes Rendus Hebdomadaires des Seances de lAcademie des Sciences", "##37");
            journal = journal.Replace("Dalton transactions", "##38");
            journal = journal.Replace("Journal of Organic Chemistry", "##39");
            journal = journal.Replace("journal of organic chemistry", "##39");
            journal = journal.Replace("Organic &amp;  Biomolecular Chemistry", "##40");
            journal = journal.Replace("Organic &amp;  biomolecular Chemistry", "##40");
            journal = journal.Replace("Organic &amp; Biomolecular Chemistry", "##40");
            journal = journal.Replace("Organic &amp; biomolecular chemistry", "##40");
            journal = journal.Replace("Organometallics", "##41");
            journal = journal.Replace("Chemical communications(Cambridge, England)", "##43");
            journal = journal.Replace("Materials Chemistry Frontiers", "##44");
            journal = journal.Replace("Monatshefte fuer Chemie und verwandte Teile anderer Wissenschaften", "##45");
            journal = journal.Replace("New Journal of Chemistry", "##46");
            journal = journal.Replace("Organic letters", "##47");
            


            return journal;

        }

        public static string GetFullTitle(string shortTitle)
        {
            string title = shortTitle;
            if (title.Contains("##"))
            {
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

            }
            return title;
        }

        public static string GetShortTitle(string fullTitle)
        {
            string title = fullTitle;

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
            return title;
        }

        static readonly char[] toCharDic = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '/', '-', '|', 'E' };

        static readonly Dictionary<char, byte> toByteDic = new Dictionary<char, byte>() {
            {'0', 0},{'1',1 },{'2', 2 },{'3', 3 },{'4', 4 }, {'5', 5 }, {'6', 6}, {'7', 7 },
            {'8', 8},{'9', 9},{'.', 10},{'/', 11},{'-', 12}, {'(', 13}, {')', 13}, {'E', 14},
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ToBytes(string s)
        {
            s = s.Trim().TrimEnd().Replace('e', 'E');
            if (s.Length == 0 || s == "?"|| s == "NaN")
                return new[] { (byte)255 };
            else if (s == "0")
                return new[] { (byte)(240 + 0) };
            else
            {
                if (s.StartsWith("0."))
                {
                    if (s == "0.")
                        s = "0";
                    else
                        s = s.Substring(1, s.Length - 1);
                }
                else if (s.StartsWith("-0."))
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
#if DEBUG
                    MessageBox.Show(e.ToString());
#endif
                    return new[] { (byte)255 };

                }
            }
        }

        public static string ToString(byte[] b)
        {
            if (b.Length == 0 || (b.Length == 1 && b[0] == 255))
                return "";
            else
            {
                var s = "";
                var flag = 0;
                for (int i = 0; i < b.Length; i++)
                    foreach (var val in new[] { b[i] & 0x0f, b[i] >> 4 })
                        if (val == 13)//"|"のとき//括弧に変換
                        {
                            if ((flag++) == 0)//一回目
                                s+="(";
                            else
                                s+=")";
                        }
                        else if (val != 15)
                            s+=toCharDic[val];
                return s;

            }
        }


        public static (double Value, double Error) Decompose(string str, int sgnum)
            => Decompose(str, sgnum >= 430 && sgnum <= 488);

        /// <summary>
        /// 9.726(5), 1.234(12)E-6 のような文字列を、ValueとErrorに分解してタプルで返す. 
        /// 例外の場合は(double.NaN,double.NaN). 括弧が存在しない場合、Errorはdouble.NaN. 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="IsHex"></param>
        /// <returns></returns>
        public static (double Value, double Error) Decompose(string str, bool IsHex = false)
        {
            try
            {
                var i = str.ToUpper().IndexOf("E");
                var exp = "";
                if (i > 0)
                {
                    exp = str.Substring(i).TrimEnd();
                    str = str.Substring(0, i).TrimStart().TrimEnd();
                }

                string val = str, err = "";
                i = str.IndexOf("(");
                if (i > 0)
                {
                    val = str.Substring(0, i).TrimStart().TrimEnd();
                    err = str.Substring(i + 1, str.IndexOf(")") - i - 1);
                }

                if (val.Length == 0)
                    return (0, double.NaN);

                var errVal = err != "" ? (err + exp).ToDouble() : double.NaN;
                var decimalPlaces = val.Length - val.IndexOf(".") - 1; //小数点以下の桁数
                if (!double.IsNaN(errVal) && decimalPlaces > 0)
                    errVal *= Math.Pow(10, -decimalPlaces);
                
                if (IsHex)
                {
                    if (str.Contains(".0833"))
                        return (1.0 / 12.0, errVal);
                    else if (str.Contains(".167") || str.Contains(".1667") || str.Contains(".16667") || str.Contains(".166667"))
                        return (1.0 / 6.0, errVal);
                    else if (str.Contains(".333"))
                        return (1.0 / 3.0, errVal);
                    else if (str.Contains(".667") || str.Contains(".6667") || str.Contains(".66667") || str.Contains(".666667"))
                        return (2.0 / 3.0, errVal);
                    else if (str.Contains(".4167") || str.Contains(".41667") || str.Contains(".416667"))
                        return (5.0 / 12.0, errVal);
                    else if (str.Contains(".5833") )
                        return (7.0 / 12.0, errVal);
                    else if (str.Contains(".8333") )
                        return (5.0 / 6.0, errVal);
                    else if (str.Contains(".9167") || str.Contains(".91667") || str.Contains(".916667"))
                        return (11.0 / 12.0, errVal);
                }

                return ((val + exp).ToDouble(), errVal);
            }
            catch
            {
                return (double.NaN, double.NaN);
            }
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
                var errStr = temp.Substring(0, temp.IndexOf("E")).Replace(".", "");
                var errLog = Convert.ToInt32(temp.Substring(temp.IndexOf("E") + 1)) - 1;

                //vを取りあえず十分な精度で出力する
                var valStr = val.ToString("E15");
                var valLog = Convert.ToInt32(valStr.Substring(valStr.IndexOf("E") + 1));

                var result = valLog >= errLog ? valStr.Substring(0, valLog - errLog + 2) : val.ToString("E0").Substring(0, 1);
                
                if(valLog < errLog)
                for (int i = 0; i < errLog - valLog; i++)
                    errStr += "0";

                result += "(" + errStr + ")";

                if (valLog != 0)
                    result += "E" + valLog.ToString();

                return result;
            }
        }

    }


    /// <summary>
    /// Atoms2クラス
    /// </summary>
    [Serializable()]
    [MessagePackObject]
    public class Atoms2
    {
        [Key(0)]
        public string Label;
        [Key(1)]
        private byte[][] positionBytes;//x,y,z の 順番
        [Key(3)]
        private byte[] occBytes;//Occ 
        [Key(4)]
        public byte SubXray;
        [Key(5)]
        public byte SubElectron;
        [Key(6)]
        public byte AtomNo;
        [Key(7)]
        public bool IsU;
        [Key(8)]
        public bool IsIso;
        [Key(9)]
        private byte[] isoBytes;//B(U)iso
        [Key(10)]
        private byte[][] anisoBytes;//B(U)11, B(U)22, B(U)33, B(U)12, B(U)23, B(U)31の順番

        /// <summary>
        /// x,y,zの順番. 無次元
        /// </summary>
        [IgnoreMember]
        public string[] PositionTexts
        {
            get => positionBytes?.Select(x => Crystal2.ToString(x)).ToArray();
            set => positionBytes = value?.Select(x => Crystal2.ToBytes(x)).ToArray();
        }

        /// <summary>
        /// Occ. 無次元
        /// </summary>
        [IgnoreMember]
        public string OccText { get => Crystal2.ToString(occBytes); set => occBytes = Crystal2.ToBytes(value); }


        /// <summary>
        /// 単位は Å^2. 
        /// </summary>
        [IgnoreMember]
        public string IsoText
        {
            get => Crystal2.ToString(isoBytes);
            set => isoBytes = Crystal2.ToBytes(value);
        }

        /// <summary>
        /// Bの場合は、無次元. Uの場合、Å^2. 
        /// </summary>
        [IgnoreMember]
        public string[] AnisoTexts
        {
            get => anisoBytes?.Select(x => Crystal2.ToString(x)).ToArray();
            set => anisoBytes = value?.Select(x => Crystal2.ToBytes(x)).ToArray();
        }

     


        public Atoms2() { }
        /// <summary>
        /// コンストラクタ. Uの単位はÅ
        /// </summary>
        /// <param name="label"></param>
        /// <param name="atomNo"></param>
        /// <param name="sfx"></param>
        /// <param name="sfe"></param>
        /// <param name="pos"></param>
        /// <param name="occ"></param>
        /// <param name="isIso"></param>
        /// <param name="isU"></param>
        /// <param name="iso"></param>
        /// <param name="aniso"></param>
        public Atoms2(string label, int atomNo, int sfx, int sfe, string[] pos, string occ, bool isIso, bool isU, string iso, string[] aniso)

        {
            PositionTexts = pos;

            Label = label;

            OccText = occ;

            SubXray = (byte)sfx;
            SubElectron = (byte)sfe;
            AtomNo = (byte)atomNo;

            IsIso = isIso;
            IsU = isU;

            AnisoTexts = aniso;
            IsoText = iso;

        }
    }
}
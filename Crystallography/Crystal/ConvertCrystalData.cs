using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using V3 = OpenTK.Vector3d;



namespace Crystallography;

public class ConvertCrystalData
{
    static System.StringComparison Ord = System.StringComparison.Ordinal;

    #region CrystalList(xml形式)の読み込み/書き込み
    public static bool SaveCrystalListXml(Crystal[] crystals, string filename)
    {
        try
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Crystal[]));
            var fs = new FileStream(filename, FileMode.Create);
            serializer.Serialize(fs, crystals);
            fs.Close();
            return true;
        }
        catch { return false; }
    }


    //CrystalListを読み込むとき
    public static Crystal[] ConvertToCrystalList(string filename)
    {
        var cry = Array.Empty<Crystal>();
        if (filename.ToLower().EndsWith("xml", Ord))//XML形式のリストを読み込んだとき
        {
            if (new FileInfo(filename).Length > 10000000)//なぜかファイルが3GBとかになったことが有ったので、それに対する対処. 10MB以上だったらスキップすることにした.
                return cry;

            #region old code
            //プロパティ文字列が変更にたいする対処
            try
            {
                var reader = new StreamReader(filename, Encoding.GetEncoding("UTF-8"));
                var strList = new List<string>();
                string tempstr;
                while ((tempstr = reader.ReadLine()) != null)
                {
                    tempstr = tempstr.Replace("Kprime0", "Kp0");
                    tempstr = tempstr.Replace("Birch_Murnaghan", "BM3");
                    strList.Add(tempstr);
                }

                reader.Close();

                //filename = filename + "_";//検証のためファイルネーム変更

                var writer = new StreamWriter(filename, false, Encoding.GetEncoding("UTF-8"));
                for (int i = 0; i < strList.Count; i++)
                    writer.WriteLine(strList[i]);
                writer.Flush();
                writer.Close();
            }
            catch { return null; };
            //プロパティ文字列が変更にたいする対処　ここまで
            #endregion old code
            try
            {
                using var fs = new FileStream(filename, FileMode.Open);
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Crystal[]));


                var worker = new BackgroundWorker();
                worker.DoWork += (sender, e) => cry = (Crystal[])serializer.Deserialize(fs);
                worker.RunWorkerAsync();

                var timeout = 10000;
                var sw = new Stopwatch();
                for (int i = 0; i < timeout; i++)
                {
                    if (cry.Length != 0)
                        break;
                    if (i == timeout - 1)
                    {
                        throw new Exception();
                    }
                    Thread.Sleep(1);
                }


            }
            catch { }
        }
        else if (filename.EndsWith("out", Ord))//SMAP形式を読み込んだとき
        {
            var stringList = new List<string>();
            string strTemp;
            var reader = new StreamReader(filename);
            while ((strTemp = reader.ReadLine()) != null)
                stringList.Add(strTemp);
            reader.Close();
            cry = ConvertFromSMAP(stringList.ToArray());
        }

        for (int i = 0; i < cry.Length; i++)
        {
            cry[i].Reset();
            cry[i].SaveInitialCellConstants();
        }

        return cry;
    }

    #endregion

    #region SMAPの出力ファイル(*.out)読込

    /// <summary>
    /// SMAPのoutファイル読み込み
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Crystal[] ConvertFromSMAP(string[] str)
    {
        string wavelength = "";
        string ChemicalFormula = "";
        double a = 0, b = 0, c = 0, alpha = 0, beta = 0, gamma = 0;
        int spaceGroupSeriesNum = 0;
        //空間群
        int line = 0;

        for (; line < str.Length; line++)
            if (str[line].StartsWith(" wave length", Ord))
            {
                wavelength = str[line];
                break;
            }
        for (; line < str.Length; line++)
            if (str[line].StartsWith(" Chemical Formula", Ord))
            {
                ChemicalFormula = str[line];
                break;
            }
        for (; line < str.Length; line++)
            if (str[line].StartsWith(" Cell Constants", Ord))
            {
                string[] cellconstants = str[line + 1].Split(' ', true);
                a = Convert.ToDouble(cellconstants[0]) / 10.0;
                b = Convert.ToDouble(cellconstants[1]) / 10.0;
                c = Convert.ToDouble(cellconstants[2]) / 10.0;
                alpha = Convert.ToDouble(cellconstants[3]) / 180 * Math.PI;
                beta = Convert.ToDouble(cellconstants[4]) / 180 * Math.PI;
                gamma = Convert.ToDouble(cellconstants[5]) / 180 * Math.PI;
                break;
            }

        for (; line < str.Length; line++)
            if (str[line].StartsWith(" Space Group Number", Ord))
            {
                string s = (str[line].Split('=', true)[1]).Trim();
                int num = Convert.ToInt32(s.Substring(1, 3));
                int sub = Convert.ToInt32(s.Substring(4, 1));
                spaceGroupSeriesNum = SymmetryStatic.GetSeriesNumber(num, sub);
                break;
            }

        int n = 0;
        //ここから原始座標読み取り
        var cry = new List<Crystal>();
        while (line < str.Length)
        {
            for (; line < str.Length; line++)
                if (str[line].StartsWith("No =", Ord))
                {
                    var tempCrystal = new Crystal((a, b, c, alpha, beta, gamma), null, spaceGroupSeriesNum,
                        ChemicalFormula.Split('=', true)[1].Trim() + "-" + (n++).ToString(), Color.Blue);
                    tempCrystal.Note = wavelength + "  " + ChemicalFormula + "\r\n" + str[line];
                    line++;
                    for (; line < str.Length; line++)
                    {
                        if (str[line].Length == 0)
                            break;
                        string[] s = str[line].Split(' ', true);
                        string label = s[0];
                        //ラベル名から元素を決める
                        string temp;

                        int atomicNumber = 0;
                        for (int q = label.Length; q > 0; q--)
                        {
                            temp = label[..q];
                            for (int k = 0; k <= 96; k++)
                            {
                                if (AtomStatic.AtomicName(k) == temp)
                                {
                                    atomicNumber = k;
                                    break;
                                }
                            }
                        }
                        double x = ZeroToOne(Convert.ToDouble(s[2]));
                        double y = ZeroToOne(Convert.ToDouble(s[3]));
                        double z = ZeroToOne(Convert.ToDouble(s[4]));

                        tempCrystal.AddAtoms(label, atomicNumber, 0, 0, null, x, y, z, 1, new DiffuseScatteringFactor(), true);
                    }
                    //SetOpenGL_property(tempCrystal);
                    cry.Add(tempCrystal);
                }
        }
        return cry.ToArray();
    }

    /// <summary>
    /// ConvertFromSMAPから呼び出される
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double ZeroToOne(double x)
    {
        if (x < 0)
            while (x < 0)
                x++;
        else if (x > 1)
            while (x > 1)
                x--;
        return x;
    }

    #endregion SMAPの出力ファイル(*.out)読込

    #region AMCとCIFの読み込みインターフェース ConvertCrystal(filename)  

    public static Crystal2 ConvertToCrystal2(string fileName)
    {
        try
        {
            Crystal2 c2 = null;
            if (fileName.EndsWith("amc", Ord))
                c2 = ConvertFromAmc(fileName);
            else if (fileName.EndsWith("cif", Ord))
                c2 = ConvertFromCIF(fileName);

            if (c2 != null)
            {
                c2.fileName = Path.GetFileNameWithoutExtension(fileName);
                return c2;
            }
            else
                return null;
        }
        catch (Exception e)
        {
            if (AssemblyState.IsDebug)
                _ = System.Windows.Forms.MessageBox.Show(fileName + " " + e.Message);
            return null;
        }
    }

    public static Crystal ConvertToCrystal(string fileName)
    {
        try
        {
            if (fileName.EndsWith("amc", Ord))
            {
                var c = ConvertFromAmc(fileName);
                return c?.ToCrystal();
            }
            else if (fileName.EndsWith("cif", Ord))
            {
                var c = ConvertFromCIF(fileName);
                return c?.ToCrystal();
            }
            else
                return null;
        }
        catch (Exception e)
        {
            if (Crystallography.AssemblyState.IsDebug)
                System.Windows.Forms.MessageBox.Show(e.Message);
            return null;
        }

    }
    #endregion

    #region amcファイルの読み込み

    private static Crystal2 ConvertFromAmc(string fileName)
    {
        using var reader = new StreamReader(fileName);
        var stringList = new List<string>();
        string strTemp;
        while ((strTemp = reader.ReadLine()) != null)
            if (strTemp != "")
                stringList.Add(strTemp);
        return ConvertFromAmc(stringList.ToArray());
    }


    /// <summary>
    /// amcファイルの読み込み
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static Crystal2 ConvertFromAmc(string[] str)
    {
        var n = 0;
        if (str[n].Length == 0)
            n++;

        var Name = str[n];//結晶の名前

        n++; if (str.Length <= n) return null;

        var AuthorName = str[n];//著者の名前
        n++; if (str.Length <= n) return null;
        while (str[n][^1] < '.' || str[n][^1] > '9')
        {
            AuthorName += ", " + str[n];
            n++; if (str.Length <= n) return null;
        }

        var Reference = str[n];//引用文献
        n++; if (str.Length <= n) return null;

        var Title = str[n];//文献タイトル
        n++; if (str.Length <= n) return null;

        //ここで格子定数、対称性とタイトル
        Crystal2 crystal;
        while ((crystal = CellParamForAmc(str[n])) == null)
        {
            if (str[n].Length > 0 && char.IsLower(str[n][0]))
                Title += " " + str[n];
            else
                Title += "\r\n" + str[n];
            n++;
            if (str.Length <= n) return null;
        }



        if (Title.Contains("_cod_database_code"))
            Title = Title.Replace("_cod_database_code", "\r\n_cod_database_code");
        else if (Title.Contains("_database_code_amcsd"))
            Title = Title.Replace("_database_code_amcsd", "\r\n_database_code_amcsd");

        n++; if (str.Length <= n) return null;

        double xShift = 0, yShift = 0, zShift = 0;
        if (!str[n].StartsWith("atom", Ord) && str[n].Split(" ", true).Length == 3)
        {
            xShift = ConvertToDouble(str[n].Split(" ", true)[0]);
            yShift = ConvertToDouble(str[n].Split(" ", true)[1]);
            zShift = ConvertToDouble(str[n].Split(" ", true)[2]);
            n++; if (str.Length <= n) return null;
        }

        //2ndセッティングにもかかわらず、shift量がゼロでないときは、1stセッティングに戻す。
        if ((xShift != 0 || yShift != 0 || zShift != 0) && SymmetryStatic.SpaceGroupListWithoutSpace[crystal.sym].EndsWith("(2)", Ord))
            crystal.sym--;


        //ここから原子座標の読み取り
        bool IsOcc = false, IsisoUsed = false, IsanisoUsed = false, IsUtypeUsed = false;
        if (str[n].Contains("occ"))
            IsOcc = true;


        if (str[n].Contains("Uiso") || str[n].Contains("uiso"))
        {
            IsisoUsed = true;
            IsUtypeUsed = true;
        }
        else if (str[n].Contains("Biso") || str[n].Contains("biso"))
        {
            IsisoUsed = true;
            IsUtypeUsed = false;
        }

        if (str[n].Contains("U(1,1)") || str[n].Contains("u(1,1)"))
        {
            IsanisoUsed = true;
            IsUtypeUsed = true;
        }
        else if (str[n].Contains("B(1,1)") || str[n].Contains("b(1,1)"))
        {
            IsanisoUsed = true;
            IsUtypeUsed = false;
        }

        var tempStr = str[n].Split(" ", true);
        var l = new int[tempStr.Length + 1];
        l[0] = 0;

        for (int i = 0; i < tempStr.Length; i++)//各入力値の文字位置を決める。
            l[i + 1] = str[n].IndexOf(tempStr[i]) + tempStr[i].Length;
        for (int i = n + 1; i < str.Length; i++)//最初のatomラベルだけ例外があるようなのでそれに対処
            if (l[1] < str[i].Split(" ", true)[0].Length)
                l[1] = str[i].Split(" ", true)[0].Length;

        //三方あるいは六方
        bool isHex = crystal.sym >= 430 && crystal.sym <= 488;

        var atoms = new List<Atoms2>();
        for (int i = n + 1; i < str.Length; i++)
        {//原子座標読み取りループ開始
            str[i] = str[i].PadRight(str[n].Length, ' ');

            var item = str[i].Split(" ", true);
            if (item.Length != l.Length - 1)
            {
                item = new string[l.Length - 1];
                for (int k = 0; k < item.Length; k++)
                {
                    item[k] = str[i][l[k]..l[k + 1]].Trim().TrimEnd();
                    item[k] = item[k].Replace(',', '.');//たまにカンマとピリオドが間違えられている
                    if (item[k].Length > 3 && item[k][1] == ' ')
                    {
                        //二文字目がスペースの場合は、数字がずれている可能性が考えられる。(.123 .456 => ".12", "3 .456" )　
                        //この場合は、二文字目までを削除して対応する
                        item[k] = item[k][2..];
                    }
                }
            }

            int j = 0;
            var label = item[j++];
            var x = item[j++];
            var y = item[j++];
            var z = item[j++];

            if (xShift != 0 || yShift != 0 || zShift != 0)
            {
                x = (x.ToDouble() + xShift).ToString("f8").TrimEnd(new[] { '0' });
                y = (y.ToDouble() + yShift).ToString("f8").TrimEnd(new[] { '0' });
                z = (z.ToDouble() + zShift).ToString("f8").TrimEnd(new[] { '0' });
            }

            var occ = "1";
            if (IsOcc)
            {
                occ = item[j++];
                if (occ.Length == 0) occ = "1";
            }

            var iso = "";
            if (IsisoUsed)
                iso = item[j++];

            var aniso = new string[6];
            if (IsanisoUsed)
                for (int k = 0; k < 6; k++)
                    aniso[k] = item[j++];
            else
                aniso = null;

            //ラベル名から元素を決める
            var atomicNumber = 0;
            for (int q = label.Length; q > 0 && atomicNumber == 0; q--)
            {
                var temp = label[..q];
                for (int k = 0; k <= 96 && atomicNumber == 0; k++)
                {
                    if (AtomStatic.AtomicName(k) == temp)
                    {
                        atomicNumber = k;
                        q = -1;
                        break;
                    }
                }
                if (temp == "OH") //OH基のとき
                    atomicNumber = -1;
                else if (temp == "D") //重水素のとき
                    atomicNumber = 255;
                else if (temp == "Wat" || temp == "WAT" || temp == "wat") //水のとき
                    atomicNumber = -2;
            }

            var IsIso = aniso == null || aniso.All(e => e.Length == 0);

            if (atomicNumber > 0)
            {
                atoms.Add(new Atoms2(label, (byte)atomicNumber, 0, 0, new[] { x, y, z }, occ, IsIso, IsUtypeUsed, iso, aniso));
            }
            else if (atomicNumber == -1)//"OH"のときの対処
            {
                atoms.Add(new Atoms2(label, 1, 0, 0, new[] { x, y, z }, occ, IsIso, IsUtypeUsed, iso, aniso));
                atoms.Add(new Atoms2(label, 8, 0, 0, new[] { x, y, z }, occ, IsIso, IsUtypeUsed, iso, aniso));
            }
            else if (atomicNumber == -2)//"Wat"水のときの対処
            {
                atoms.Add(new Atoms2(label, 1, 0, 0, new[] { x, y, z }, occ, IsIso, IsUtypeUsed, iso, aniso));
                atoms.Add(new Atoms2(label, 1, 0, 0, new[] { x, y, z }, occ, IsIso, IsUtypeUsed, iso, aniso));
                atoms.Add(new Atoms2(label, 8, 0, 0, new[] { x, y, z }, occ, IsIso, IsUtypeUsed, iso, aniso));
            }
        }
        crystal.name = Name;
        crystal.sect = Title;
        crystal.auth = AuthorName;
        crystal.jour = Reference;
        crystal.atoms = atoms;
        return crystal;
    }

    private static Crystal2 CellParamForAmc(string str)
    {
        double A, B, C, Alfa, Beta, Gamma;
        int symmetrySeriesNumber = -1;
        var s = str.Split(" ", true);
        if (s.Length != 7)
            return null;
        try
        {
            if (Miscellaneous.IsDecimalPointComma)
                for (int i = 0; i < 6; i++) s[i] = s[i].Replace('.', ',');
            else
                for (int i = 0; i < 6; i++) s[i] = s[i].Replace(',', '.');
            A = Convert.ToDouble(s[0]);
            B = Convert.ToDouble(s[1]);
            C = Convert.ToDouble(s[2]);
            Alfa = Convert.ToDouble(s[3]);
            Beta = Convert.ToDouble(s[4]);
            Gamma = Convert.ToDouble(s[5]);
        }
        catch { return null; }
        string SgName = s[6];

        SgName = SgName.Replace("_", "sub");
        bool isAsterisk = SgName.Contains('*');
        SgName = SgName.Replace("*", "");

        #region 空間群の場合分け

        if (SgName == "Pm3") SgName = "Pm-3";
        else if (SgName == "Pn3") SgName = "Pn-3";
        else if (SgName == "Fm3") SgName = "Fm-3";
        else if (SgName == "Fd3") SgName = "Fd-3";
        else if (SgName == "Im3") SgName = "Im-3";
        else if (SgName == "Pa3") SgName = "Pa-3";
        else if (SgName == "Ia3") SgName = "Ia-3";

        else if (SgName == "Pm3m") SgName = "Pm-3m";
        else if (SgName == "Pn3n") SgName = "Pm-3m";
        else if (SgName == "Pm3n") SgName = "Pm-3n";
        else if (SgName == "Pn3m") SgName = "Pn-3m";
        else if (SgName == "Fm3m") SgName = "Fm-3m";
        else if (SgName == "Fm3c") SgName = "Fm-3c";
        else if (SgName == "Fd3m") SgName = "Fd-3m";
        else if (SgName == "Fd3c") SgName = "Fd-3c";
        else if (SgName == "Im3m") SgName = "Im-3m";
        else if (SgName == "Ia3d") SgName = "Ia-3d";

        else if (SgName == "I2sub1/a-3") SgName = "Ia3";

        else if (SgName == "R-32/c") SgName = "R-3c";

        else if (SgName == "P2" && Alfa == 90.0 && Gamma == 90.0) SgName = "P121";
        else if (SgName == "P2" && Alfa == 90.0 && Beta == 90.0) SgName = "P112";
        else if (SgName == "P2" && Beta == 90.0 && Gamma == 90.0) SgName = "P211";
        else if (SgName == "P2sub1" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12sub11";
        else if (SgName == "P2sub1" && Alfa == 90.0 && Beta == 90.0) SgName = "P112sub1";
        else if (SgName == "P2sub1" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub111";
        else if (SgName == "C2" && Alfa == 90.0 && Gamma == 90.0) SgName = "C121";
        else if (SgName == "A2" && Alfa == 90.0 && Gamma == 90.0) SgName = "A121";
        else if (SgName == "I2" && Alfa == 90.0 && Gamma == 90.0) SgName = "I121";
        else if (SgName == "A2" && Alfa == 90.0 && Beta == 90.0) SgName = "A112";
        else if (SgName == "B2" && Alfa == 90.0 && Beta == 90.0) SgName = "B112";
        else if (SgName == "I2" && Alfa == 90.0 && Beta == 90.0) SgName = "I112";
        else if (SgName == "B2" && Beta == 90.0 && Gamma == 90.0) SgName = "B211";
        else if (SgName == "C2" && Beta == 90.0 && Gamma == 90.0) SgName = "C211";
        else if (SgName == "I2" && Beta == 90.0 && Gamma == 90.0) SgName = "I211";
        else if (SgName == "Pm" && Alfa == 90.0 && Gamma == 90.0) SgName = "P1m1";
        else if (SgName == "Pm" && Alfa == 90.0 && Beta == 90.0) SgName = "P11m";
        else if (SgName == "Pm" && Beta == 90.0 && Gamma == 90.0) SgName = "Pm11";
        else if (SgName == "Pc" && Alfa == 90.0 && Gamma == 90.0) SgName = "P1c1";
        else if (SgName == "Pn" && Alfa == 90.0 && Gamma == 90.0) SgName = "P1n1";
        else if (SgName == "Pa" && Alfa == 90.0 && Gamma == 90.0) SgName = "P1a1";
        else if (SgName == "Pa" && Alfa == 90.0 && Beta == 90.0) SgName = "P11a";
        else if (SgName == "Pn" && Alfa == 90.0 && Beta == 90.0) SgName = "P11n";
        else if (SgName == "Pb" && Alfa == 90.0 && Beta == 90.0) SgName = "P11b";
        else if (SgName == "Pb" && Beta == 90.0 && Gamma == 90.0) SgName = "Pb11";
        else if (SgName == "Pn" && Beta == 90.0 && Gamma == 90.0) SgName = "Pn11";
        else if (SgName == "Pc" && Beta == 90.0 && Gamma == 90.0) SgName = "Pc11";
        else if (SgName == "Cm" && Alfa == 90.0 && Gamma == 90.0) SgName = "C1m1";
        else if (SgName == "Am" && Alfa == 90.0 && Gamma == 90.0) SgName = "A1m1";
        else if (SgName == "Im" && Alfa == 90.0 && Gamma == 90.0) SgName = "I1m1";
        else if (SgName == "Am" && Alfa == 90.0 && Beta == 90.0) SgName = "A11m";
        else if (SgName == "Bm" && Alfa == 90.0 && Beta == 90.0) SgName = "B11m";
        else if (SgName == "Im" && Alfa == 90.0 && Beta == 90.0) SgName = "I11m";
        else if (SgName == "Bm" && Beta == 90.0 && Gamma == 90.0) SgName = "Bm11";
        else if (SgName == "Cm" && Beta == 90.0 && Gamma == 90.0) SgName = "Cm11";
        else if (SgName == "Im" && Beta == 90.0 && Gamma == 90.0) SgName = "Im11";
        else if (SgName == "Cc" && Alfa == 90.0 && Gamma == 90.0) SgName = "C1c1";
        else if (SgName == "An" && Alfa == 90.0 && Gamma == 90.0) SgName = "A1n1";
        else if (SgName == "Ia" && Alfa == 90.0 && Gamma == 90.0) SgName = "I1a1";
        else if (SgName == "Aa" && Alfa == 90.0 && Gamma == 90.0) SgName = "A1a1";
        else if (SgName == "Cn" && Alfa == 90.0 && Gamma == 90.0) SgName = "C1n1";
        else if (SgName == "Ic" && Alfa == 90.0 && Gamma == 90.0) SgName = "I1c1";
        else if (SgName == "Aa" && Alfa == 90.0 && Beta == 90.0) SgName = "A11a";
        else if (SgName == "Bn" && Alfa == 90.0 && Beta == 90.0) SgName = "B11n";
        else if (SgName == "Ib" && Alfa == 90.0 && Beta == 90.0) SgName = "I11b";
        else if (SgName == "Bb" && Alfa == 90.0 && Beta == 90.0) SgName = "B11b";
        else if (SgName == "An" && Alfa == 90.0 && Beta == 90.0) SgName = "A11n";
        else if (SgName == "Ia" && Alfa == 90.0 && Beta == 90.0) SgName = "I11a";
        else if (SgName == "Bb" && Beta == 90.0 && Gamma == 90.0) SgName = "Bb11";
        else if (SgName == "Cn" && Beta == 90.0 && Gamma == 90.0) SgName = "Cn11";
        else if (SgName == "Ic" && Beta == 90.0 && Gamma == 90.0) SgName = "Ic11";
        else if (SgName == "Cc" && Beta == 90.0 && Gamma == 90.0) SgName = "Cc11";
        else if (SgName == "Bn" && Beta == 90.0 && Gamma == 90.0) SgName = "Bn11";
        else if (SgName == "Ib" && Beta == 90.0 && Gamma == 90.0) SgName = "Ib11";
        else if (SgName == "P2/m" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12/m1";
        else if (SgName == "P2/m" && Alfa == 90.0 && Beta == 90.0) SgName = "P112/m";
        else if (SgName == "P2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/m11";
        else if (SgName == "P2sub/m" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12sub1/m1";
        else if (SgName == "P2sub/m" && Alfa == 90.0 && Beta == 90.0) SgName = "P112sub1/m";
        else if (SgName == "P2sub/m" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/m11";
        else if (SgName == "C2/m" && Alfa == 90.0 && Gamma == 90.0) SgName = "C12/m1";
        else if (SgName == "A2/m" && Alfa == 90.0 && Gamma == 90.0) SgName = "A12/m1";
        else if (SgName == "I2/m" && Alfa == 90.0 && Gamma == 90.0) SgName = "I12/m1";
        else if (SgName == "A2/m" && Alfa == 90.0 && Beta == 90.0) SgName = "A112/m";
        else if (SgName == "B2/m" && Alfa == 90.0 && Beta == 90.0) SgName = "B112/m";
        else if (SgName == "I2/m" && Alfa == 90.0 && Beta == 90.0) SgName = "I112/m";
        else if (SgName == "B2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "B2/m11";
        else if (SgName == "C2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "C2/m11";
        else if (SgName == "I2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "I2/m11";
        else if (SgName == "P2/c" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12/c1";
        else if (SgName == "P2/n" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12/n1";
        else if (SgName == "P2/a" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12/a1";
        else if (SgName == "P2/a" && Alfa == 90.0 && Beta == 90.0) SgName = "P112/a";
        else if (SgName == "P2/n" && Alfa == 90.0 && Beta == 90.0) SgName = "P112/n";
        else if (SgName == "P2/b" && Alfa == 90.0 && Beta == 90.0) SgName = "P112/b";
        else if (SgName == "P2/b" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/b11";
        else if (SgName == "P2/n" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/n11";
        else if (SgName == "P2/c" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/c11";
        else if (SgName == "P2sub1/c" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12sub1/c1";
        else if (SgName == "P2sub1/n" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12sub1/n1";
        else if (SgName == "P2sub1/a" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12sub1/a1";
        else if (SgName == "P2sub1/a" && Alfa == 90.0 && Beta == 90.0) SgName = "P112sub1/a";
        else if (SgName == "P2sub1/n" && Alfa == 90.0 && Beta == 90.0) SgName = "P112sub1/n";
        else if (SgName == "P2sub1/b" && Alfa == 90.0 && Beta == 90.0) SgName = "P112sub1/b";
        else if (SgName == "P2sub1/b" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/b11";
        else if (SgName == "P2sub1/n" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/n11";
        else if (SgName == "P2sub1/c" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/c11";
        else if (SgName == "C2/c" && Alfa == 90.0 && Gamma == 90.0) SgName = "C12/c1";
        else if (SgName == "A2/n" && Alfa == 90.0 && Gamma == 90.0) SgName = "A12/n1";
        else if (SgName == "I2/a" && Alfa == 90.0 && Gamma == 90.0) SgName = "I12/a1";
        else if (SgName == "A2/a" && Alfa == 90.0 && Gamma == 90.0) SgName = "A12/a1";
        else if (SgName == "C2/n" && Alfa == 90.0 && Gamma == 90.0) SgName = "C12/n1";
        else if (SgName == "I2/c" && Alfa == 90.0 && Gamma == 90.0) SgName = "I12/c1";
        else if (SgName == "A2/a" && Alfa == 90.0 && Beta == 90.0) SgName = "A112/a";
        else if (SgName == "B2/n" && Alfa == 90.0 && Beta == 90.0) SgName = "B112/n";
        else if (SgName == "I2/b" && Alfa == 90.0 && Beta == 90.0) SgName = "I112/b";
        else if (SgName == "B2/b" && Alfa == 90.0 && Beta == 90.0) SgName = "B112/b";
        else if (SgName == "A2/n" && Alfa == 90.0 && Beta == 90.0) SgName = "A112/n";
        else if (SgName == "I2/a" && Alfa == 90.0 && Beta == 90.0) SgName = "I112/a";
        else if (SgName == "B2/b" && Beta == 90.0 && Gamma == 90.0) SgName = "B2/b11";
        else if (SgName == "C2/n" && Beta == 90.0 && Gamma == 90.0) SgName = "C2/n11";
        else if (SgName == "I2/c" && Beta == 90.0 && Gamma == 90.0) SgName = "I2/c11";
        else if (SgName == "C2/c" && Beta == 90.0 && Gamma == 90.0) SgName = "C2/c11";
        else if (SgName == "B2/n" && Beta == 90.0 && Gamma == 90.0) SgName = "B2/n11";
        else if (SgName == "I2/b" && Beta == 90.0 && Gamma == 90.0) SgName = "I2/b11";
        #endregion amcファイルの読み込み

        //文字列を含んでいて、かつ、文字数が少ない空間群を選択する (C1とC121などを見分けるため)
        int length = int.MaxValue;

        var sg = SymmetryStatic.SpaceGroupListWithoutSpace;
        for (int i = 0; i < SymmetryStatic.TotalSpaceGroupNumber; i++)
        {
            if (sg[i].Length < length)
            {
                var sg_list = new List<string>(new[] { sg[i] });

                if (sg[i].Contains('e'))
                {
                    sg_list.Add(sg[i].Replace('e', 'a'));
                    sg_list.Add(sg[i].Replace('e', 'b'));
                    sg_list.Add(sg[i].Replace('e', 'c'));
                }

                if (sg_list.Any(s => s.IndexOf(SgName, 0) != -1))
                {
                    symmetrySeriesNumber = i;
                    length = sg[i].Length;
                }
            }
        }
        if (symmetrySeriesNumber == -1)
            return null;
        //Rhombohedoralのときの処置
        if (A == B && B == C && Alfa == Beta && Beta == Gamma && SymmetryStatic.Symmetries[symmetrySeriesNumber].SpaceGroupHMStr.Contains("Hex", StringComparison.CurrentCulture))
            symmetrySeriesNumber++;

        //Asteriskの時(2nd setting)の処理
        if (isAsterisk && sg[symmetrySeriesNumber].EndsWith("(1)", Ord))
            symmetrySeriesNumber++;


        if (symmetrySeriesNumber >= 0)
        {
            var r = new Random();
            return new Crystal2
            {

                CellTexts = new[] { s[0], s[1], s[2], s[3], s[4], s[5] },
                sym = (short)symmetrySeriesNumber,
                argb = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)).ToArgb()
            };
        }
        else
            return null;
    }

    private static double ConvertToDouble(string str) => ConvertToDouble(str, false);

    private static double ConvertToDouble(string str, bool IsHex)
    {
        try
        {
            if (str.Trim().Length == 0)
                return 0;
            else if (str.IndexOf('/') > 0)
            {
                var temp = str.Split('/', true);
                return temp.Length == 2 ? Convert.ToDouble(temp[0]) / Convert.ToDouble(temp[1]) : 0;
            }
            else if (IsHex)
            {
                if (str.Contains(".0833")) return 1.0 / 12.0;
                else if (str.Contains(".1667")) return 1.0 / 6.0;
                else if (str.Contains(".16667")) return 1.0 / 6.0;
                else if (str.Contains(".166667")) return 1.0 / 6.0;
                else if (str.Contains(".333333")) return 1.0 / 3.0;
                else if (str.Contains(".33333")) return 1.0 / 3.0;
                else if (str.Contains(".3333")) return 1.0 / 3.0;
                else if (str.Contains(".4167")) return 5.0 / 12.0;
                else if (str.Contains(".5833")) return 7.0 / 12.0;
                else if (str.Contains(".666667")) return 2.0 / 3.0;
                else if (str.Contains(".66667")) return 2.0 / 3.0;
                else if (str.Contains(".6667")) return 2.0 / 3.0;
                else if (str.Contains(".8333")) return 5.0 / 6.0;
                else if (str.Contains(".9167")) return 11.0 / 12.0;

                if (str.Contains(",0833")) return 1.0 / 12.0;
                else if (str.Contains(",1667")) return 1.0 / 6.0;
                else if (str.Contains(",16667")) return 1.0 / 6.0;
                else if (str.Contains(",166667")) return 1.0 / 6.0;
                else if (str.Contains(",333333")) return 1.0 / 3.0;
                else if (str.Contains(",33333")) return 1.0 / 3.0;
                else if (str.Contains(",3333")) return 1.0 / 3.0;
                else if (str.Contains(",4167")) return 5.0 / 12.0;
                else if (str.Contains(",5833")) return 7.0 / 12.0;
                else if (str.Contains(",666667")) return 2.0 / 3.0;
                else if (str.Contains(",66667")) return 2.0 / 3.0;
                else if (str.Contains(",6667")) return 2.0 / 3.0;
                else if (str.Contains(",8333")) return 5.0 / 6.0;
                else if (str.Contains(",9167")) return 11.0 / 12.0;
            }

            return Convert.ToDouble(str);
        }
        catch
        {
            return 0;
        }
    }

    #endregion

    #region CIFファイルの読み込み
    static readonly Random r = new Random();

    static readonly string[] ignoreWords1 = new[] { "_shelx_hkl_", "_shelx_fab_", "_shelx_res_" };
    static readonly string[] ignoreWords2 = new[] { "_refln", "_geom", "_platon" };
    private static Crystal2 ConvertFromCIF(string fileName)
    {
        var sb = new StringBuilder();

        var stringList = new List<string>();
        using (var reader = new StreamReader(fileName))
        {
            var strTemp = reader.ReadToEnd();
            if (strTemp.Contains("\r\n"))
                strTemp = strTemp.Replace("\r\n", "\n");
            if (strTemp.Contains('\r'))
                strTemp = strTemp.Replace("\r", "\n");

            stringList = strTemp.Split('\n', true).ToList();
        }

        foreach (var word in ignoreWords1)
        {
            int start = -1, end = -1;
            while ((start = stringList.IndexOf(word + "file")) > -1 &&
                (end = stringList.FindIndex(s => s.StartsWith(word + "checksum", Ord))) > -1)
                stringList.RemoveRange(Math.Min(start, end), Math.Abs(start - end) + 1);
        }
        foreach (var word in ignoreWords2)
        {
            int start = -1;
            while ((start = stringList.FindIndex(s => s.StartsWith(word, Ord)) - 1) > -1)
            {
                if (stringList[start] == "loop_")
                {
                    var end = start + 1;
                    while (stringList[end].StartsWith(word, Ord))
                        end++;

                    for (; end < stringList.Count; end++)
                        if (stringList[end] == "loop_" || stringList[end].StartsWith("_", Ord) || stringList[end].StartsWith("#", Ord))
                            break;
                    stringList.RemoveRange(start, end - start);
                }
                else
                    stringList.RemoveAt(start + 1);
            }
        }
        return ConvertFromCIF(stringList);
    }


    /// <summary>
    /// CIFファイルを読み込む
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static Crystal2 ConvertFromCIF(List<string> str)
    {
        //まず ;と; で囲まれている複数にわたる行を一行にする
        //var str = new List<string>();
        var note = "";
        if (str[0].StartsWith("data", Ord))
            note = str[0];
        for (int n = 0; n < str.Count; n++)
        {
            while ((str[n].StartsWith("#", Ord) || str[n].Trim().Length == 0) && n < str.Count - 1)
                str.RemoveAt(n);
            //全ての先頭行の空白あるいはタブを削除する
            str[n] = str[n].Replace("\t", " ");
            str[n] = str[n].TrimEnd(' ').TrimStart(' ');

            if (str[n].Trim().StartsWith(";", Ord))//;で始まる行を見つけたら
            {
                int m = n + 1;

                var temp = new StringBuilder();
                //次に;が出てくるところまですすめてまとめて一行にする
                //但し、次の行が「loop_」の場合や「_」で始まる場合は、;を一文字だけ消して終了
                if (str[m].StartsWith("loop_", Ord) || str[m].StartsWith("_", Ord))
                    str[n] = "";
                else
                {
                    while (m >= str.Count || !str[m].StartsWith(";", Ord))
                        temp.Append(str[m++] + " ");
                    str[n] = "'" + temp.ToString().Trim().TrimEnd() + "'";
                    str.RemoveRange(n + 1, m - n);
                }
            }
        }

        //次に'あるいは"で囲まれている文字列中の空白を偶然出てこないような文字列に変換する
        for (int n = 0; n < str.Count; n++)
        {
            if (str[n].Contains("''"))//''という文字列が含まれていたら
            {
                var temp = str[n].Remove(0, str[n].IndexOf("'"));
                temp = temp.Replace("''", "薔");
                str[n] = str[n].Remove(str[n].IndexOf("'")) + temp;
            }

            if (str[n].Contains('\''))//'が含まれていたら
            {
                while (str[n].Contains('\''))
                {
                    var firstIndex = str[n].IndexOf('\'');
                    var next = str[n].IndexOf('\'', firstIndex + 1);
                    if (next == -1)
                        break;
                    var substring = str[n].Substring(firstIndex, next - firstIndex + 1);
                    substring = substring.Replace(" ", "薔");
                    substring = substring.Replace("'", "");

                    str[n] = $"{str[n][..firstIndex]}{substring}{str[n][(next + 1)..]}";
                }
            }

            if (str[n].Contains('"'))//\"が含まれていたら
            {
                while (str[n].Contains('"'))
                {
                    var firstIndex = str[n].IndexOf('"');
                    var next = str[n].IndexOf('"', firstIndex + 1);
                    if (next == -1)
                        break;
                    var substring = str[n].Substring(firstIndex, next - firstIndex + 1);
                    substring = substring.Replace(" ", "薔");
                    substring = substring.Replace("\"", "");
                    str[n] = $"{str[n][..firstIndex]}{substring}{str[n][(next + 1)..]}";
                }
            }
        }

        //次にloop_に続く行が　"label  data"だった時に対応
        for (int n = 0; n < str.Count - 1; n++)
        {
            if (str[n].StartsWith("loop_", Ord) && str[n + 1].Contains(' '))
            {
                var temp = str[n + 1].Split(' ', true);
                str[n + 1] = temp[0];
                str.Insert(n + 2, temp[1]);
            }
        }

        var CIF = new List<List<(string Label, string Data)>>();
        for (int n = 0; n < str.Count; n++)
        {
            if (str[n].Trim().StartsWith("_", Ord))
            {//単体アイテムのとき
                var temp = str[n].Split(' ', true);
                string tempLabel = temp[0], tempData;
                if (temp.Length == 2)
                    tempData = temp[1];
                else if (temp.Length == 1)
                {
                    n++;
                    tempData = str[n];
                }
                else
                    tempData = "";
                CIF.Add(new List<(string Label, string Data)>(new[] { (tempLabel, tempData.Replace("薔", " ")) }));
            }
            else if (str[n].Trim().StartsWith("loop_", Ord))
            {//ループのとき
                var tempLoopLabels = new List<string>();
                var tempLoopDatas = new List<string>();
                n++;
                //"_"で始まるラベルを数える
                while (n < str.Count && str[n].Trim().StartsWith("_", Ord))
                    tempLoopLabels.Add(str[n++].Trim());

                //次に"_"か"loop_"か"#End of"で始まる行が出てくるまでループ
                while (n < str.Count && !str[n].Trim().StartsWith("_", Ord) && !str[n].Trim().StartsWith("loop_", Ord) && !str[n].Trim().StartsWith("#End of", Ord))
                {
                    var temp = str[n].Split(' ', true);
                    for (int i = 0; i < temp.Length; i++)
                        tempLoopDatas.Add(temp[i]);
                    n++;
                }
                n--;
                if (tempLoopDatas.Count % tempLoopLabels.Count == 0)
                {
                    for (int i = 0; i < tempLoopDatas.Count / tempLoopLabels.Count; i++)
                    {
                        var cif_Group = new List<(string Label, string Data)>();
                        for (int j = 0; j < tempLoopLabels.Count; j++)
                            cif_Group.Add((tempLoopLabels[j], tempLoopDatas[i * tempLoopLabels.Count + j].Replace("薔", " ")));
                        CIF.Add(cif_Group);
                    }
                }
            }
        }
        //ここまででCIF_Groupクラスのリストが完成

        string a = "", b = "", c = "", alpha = "", beta = "", gamma = "";
        string name = "", sectionTitle = "", journalNameFull = "", journalCodenASTM = "";
        string volume = "", year = "", pageFirst = "", pageLast = "", issue = "";
        StringBuilder journal = new StringBuilder();
        List<string> spaceGroupNameHM = new(), spaceGroupNameHall = new();
        string chemical_formula_sum = "", chemical_formula_structural = "";
        int symmetry_Int_Tables_number = -1;
        var author = new List<string>();
        var operations = new List<string>();

        for (int i = 0; i < CIF.Count; i++)
            for (int j = 0; j < CIF[i].Count; j++)
            {
                var label = CIF[i][j].Label;
                var data = CIF[i][j].Data;

                if (label.StartsWith("_chemical_name", Ord)) name += data + " ";

                //ここから格子定数
                if (label == "_cell_length_a") a = data;
                else if (label == "_cell_length_b") b = data;
                else if (label == "_cell_length_c") c = data;
                else if (label == "_cell_angle_alpha") alpha = data;
                else if (label == "_cell_angle_beta") beta = data;
                else if (label == "_cell_angle_gamma") gamma = data;
                //ここからジャーナル情報
                else if (label == "_publ_author_name") author.Add(data);
                else if (label == "_publ_section_title") sectionTitle = data;
                else if (label == "_journal_name_full") journalNameFull = data;
                else if (label == "_journal_coden_ASTM") journalCodenASTM = data;
                else if (label == "_journal_year") year = data;
                else if (label == "_journal_volume") volume = data;
                else if (label == "_journal_page_first") pageFirst = data;
                else if (label == "_journal_page_last") pageLast = data;
                else if (label == "_journal_issue") issue = data;
                //ここから対称性
                else if (label.Contains("_space_group_name_H-M")) spaceGroupNameHM.Add(data);
                else if (label.Contains("_space_group_name_Hall")) spaceGroupNameHall.Add(data);
                else if (label == "_Int_Tables_number") int.TryParse(data, out symmetry_Int_Tables_number);
                else if (label == "_chemical_formula_sum") chemical_formula_sum = data;
                else if (label == "_chemical_formula_structural") chemical_formula_structural = data;
                else if (label == "_space_group_symop_operation_xyz") operations.Add(data);
                else if (label == "_symmetry_equiv_pos_as_xyz") operations.Add(data);
            }

        if (a == "" || b == "" || c == "" || alpha == "" || beta == "" || gamma == "") return null;

        if (name.Length == 0 || name == "?" || name == "? ?" || name.Trim().Length == 0)
            name = chemical_formula_sum;

        #region 空間群を調べる部分
        //空間群を検索
        int sgnum = 0;
        if (spaceGroupNameHM.Count == 0 && spaceGroupNameHall.Count == 0)
            sgnum = 0;
        else
        {
            for (int i = 0; i < Math.Max(spaceGroupNameHall.Count, spaceGroupNameHM.Count); i++)
            {
                var HM = i < spaceGroupNameHM.Count ? spaceGroupNameHM[i] : "";
                var Hall = i < spaceGroupNameHall.Count ? spaceGroupNameHall[i] : "";
                sgnum = SearchSGseriesNumberForCIF(HM, Hall, symmetry_Int_Tables_number, a == b && b == c && alpha == beta && beta == gamma, alpha == "90", beta == "90", gamma == "90");
                if (sgnum != -1)
                    break;
            }
        }

        if (sgnum == -1)
            return null;

        #region 対象操作がCIFファイル中に記載されている場合は、本当に現在の空間群でよいかどうかをチェック

        var shift = new V3(0, 0, 0);
        var p = new V3(0.111, 0.234, 0.457);//適当な一般位置
        var tempAtom = WyckoffPosition.GetEquivalentAtomsPosition((p.X, p.Y, p.Z), sgnum).Atom;

        if (operations.Count != 0 && operations.Count == tempAtom.Length)
        {
            var th = 0.0000001;
            var prms = new[] { "x", "y", "z" }.Select(s => Expression.Parameter(typeof(double), s)).ToArray();
            //文字列からラムダ式を返すローカル関数
            Func<double, double, double, V3> func(string sExpr)
            {
                try
                {
                    sExpr = sExpr.Replace(" ", "").Replace(",+", ",").TrimStart(new[] { '+' });
                    sExpr = "new [] {" + sExpr.Replace("/", ".0/").Replace(".0.0", ".0") + "}";//分子に小数点を加える

                    var f = DynamicExpressionParser.ParseLambda(prms, typeof(double[]), sExpr).Compile() as Func<double, double, double, double[]>;
                    return (x, y, z) => { var d = f(x, y, z); return new V3(d[0], d[1], d[2]); };
                }
                catch (Exception e)
                {
                    if (AssemblyState.IsDebug)
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    return null;
                }
            }

            var funcs = operations.Select(s => func(s)).ToArray();
            if (funcs.All(f => f != null))
            {
                var shiftCandidates = new[] { 0, 0.125, 0.25, -0.125, -0.25 };

                var temp = tempAtom.Select(a => norm(new V3(a.X, a.Y, a.Z))).ToList();
                temp.Sort((o1, o2) => Math.Abs(o1.X - o2.X) > th ? o1.X.CompareTo(o2.X) : (Math.Abs(o1.Y - o2.Y) > th ? o1.Y.CompareTo(o2.Y) : o1.Z.CompareTo(o2.Z)));
                var source = temp.Select((pos, i) => (pos, i)).ToList();

                var match = false;
                foreach (var sX in shiftCandidates)
                    foreach (var sY in shiftCandidates)
                        foreach (var sZ in shiftCandidates)
                            if (!match)
                            {
                                var target = funcs.Select(f => f(p.X + sX, p.Y + sY, p.Z + sZ)).Select(r => norm(new V3(r.X - sX, r.Y - sY, r.Z - sY))).ToList();
                                target.Sort((o1, o2) => Math.Abs(o1.X - o2.X) > th ? o1.X.CompareTo(o2.X) : (Math.Abs(o1.Y - o2.Y) > th ? o1.Y.CompareTo(o2.Y) : o1.Z.CompareTo(o2.Z)));
                                if (source.All(o => (o.pos - target[o.i]).Length < th))
                                {
                                    match = true;
                                    shift = new V3(sX, sY, sZ);
                                }

                            }
            }
        }
        #endregion

        #endregion

        //"_atom_site_label"というラベルを含むCIF番号をリストする。 (最初の連続した番号のみを使う)
        var atomCIF = new List<List<(string Label, string Data)>>();
        bool flag = false;
        for (int i = 0; i < CIF.Count; i++)
            if (flag || atomCIF.Count == 0)
            {
                if (CIF[i].Exists(item => item.Label == "_atom_site_label"))
                {
                    atomCIF.Add(CIF[i]);
                    flag = true;
                }
                else
                    flag = false;
            }

        var atoms = new List<Atoms2>();
        foreach (var cif in atomCIF)
        {
            //原子の情報
            string atomLabel = "", atomSymbol = "", thermalDisplaceType = "";
            string x = "", y = "", z = "", occ = "1";
            string uIso = "", u11 = "", u22 = "", u33 = "", u12 = "", u13 = "", u23 = "";
            string bIso = "", b11 = "", b22 = "", b33 = "", b12 = "", b13 = "", b23 = "";

            //まず基本的な原子位置や占有率などの情報を探す
            occ = "1";
            for (int j = 0; j < cif.Count; j++)
            {
                var label = cif[j].Label;
                var data = cif[j].Data;
                if (label == "_atom_site_type_symbol") atomSymbol = data;
                else if (label == "_atom_site_label") atomLabel = data;
                else if (label == "_atom_site_thermal_displace_type") thermalDisplaceType = data;
                else if (data != "?" && data != ".")
                {
                    if (label == "_atom_site_fract_x") x = data;// + shift.X;
                    else if (label == "_atom_site_fract_y") y = data;// + shift.X;
                    else if (label == "_atom_site_fract_z") z = data;// + shift.X;
                    else if (label == "_atom_site_occupancy") occ = data;
                    else if (label == "_atom_site_U_iso_or_equiv") uIso = data;
                    else if (label == "_atom_site_B_iso_or_equiv") bIso = data;
                }
            }

            if (x == "0021|" || y == "0021|" || z == "0021|")
            {

            }

            if (shift.X != 0 || shift.Y != 0 || shift.Z != 0)
            {
                var _x = Crystal2.Decompose(x, sgnum);
                var _y = Crystal2.Decompose(y, sgnum);
                var _z = Crystal2.Decompose(z, sgnum);
                x = Crystal2.Compose(_x.Value + shift.X, _x.Error);
                y = Crystal2.Compose(_y.Value + shift.Y, _y.Error);
                z = Crystal2.Compose(_z.Value + shift.Z, _z.Error);
            }

            //次に異方性の温度散乱因子をさがす (等方性の温度因子は既に上のループで読み込まれている)

            for (int k = 0; k < CIF.Count; k++)
            {
                if (CIF[k].Exists(item => item.Label == "_atom_site_aniso_label" && item.Data == atomLabel))
                {
                    for (int l = 0; l < CIF[k].Count; l++)
                    {
                        var label = CIF[k][l].Label;
                        var data = CIF[k][l].Data;
                        if (data != "?" && data != ".")
                        {
                            if (label == "_atom_site_aniso_U_11") u11 = data;
                            else if (label == "_atom_site_aniso_U_22") u22 = data;
                            else if (label == "_atom_site_aniso_U_33") u33 = data;
                            else if (label == "_atom_site_aniso_U_12") u12 = data;
                            else if (label == "_atom_site_aniso_U_13") u13 = data;
                            else if (label == "_atom_site_aniso_U_23") u23 = data;
                            else if (label == "_atom_site_aniso_B_11") b11 = data;
                            else if (label == "_atom_site_aniso_B_22") b22 = data;
                            else if (label == "_atom_site_aniso_B_33") b33 = data;
                            else if (label == "_atom_site_aniso_B_12") b12 = data;
                            else if (label == "_atom_site_aniso_B_13") b13 = data;
                            else if (label == "_atom_site_aniso_B_23") b23 = data;
                        }
                    }
                    break;
                }
            }
            //ラベル名から元素を探す
            int atomicNumber = 0;
            var atomName = atomSymbol.Length == 0 ? atomLabel : atomSymbol;

            for (int q = atomName.Length; q > 0 && atomicNumber == 0; q--)
            {
                var temp = atomName[..q];
                for (int k = 0; k <= 96 && atomicNumber == 0; k++)
                {
                    if (AtomStatic.AtomicName(k).ToLower() == temp.ToLower())
                        atomicNumber = k;
                }

                if (temp == "OH")
                {
                    atomicNumber = -1;
                    break;
                }
                else if (temp == "D")
                {
                    atomicNumber = 255;
                    break;
                }
            }

            //Bタイプが全て ”” だったら、Uタイプと判定
            var isU = bIso.Length == 0 && b11.Length == 0 && b12.Length == 0 && b13.Length == 0 && b22.Length == 0 && b23.Length == 0 && b33.Length == 0;
            //非等方性が全て ”” だったら、等方性と判断
            var isIso = isU ?
                u11.Length == 0 && u12.Length == 0 && u13.Length == 0 && u22.Length == 0 && u23.Length == 0 && u33.Length == 0 :
                b11.Length == 0 && b12.Length == 0 && b13.Length == 0 && b22.Length == 0 && b23.Length == 0 && b33.Length == 0;

            var iso = isU ? uIso : bIso;

            if (iso.Length == 0)
                iso = "0";

            var aniso = isU ? //11, 22, 33, 12, 23, 31の順番
                new[] { u11, u22, u33, u12, u23, u13 } :
                new[] { b11, b22, b33, b12, b23, b13 };

            if (atomicNumber > 0)
                atoms.Add(new Atoms2(atomLabel, atomicNumber, 0, 0, new[] { x, y, z }, occ, isIso, isU, iso, aniso));
            else if (atomicNumber == -1)//"OH"のときの対処
            {
                atoms.Add(new Atoms2(atomLabel, 1, 0, 0, new[] { x, y, z }, occ, isIso, isU, iso, aniso));
                atoms.Add(new Atoms2(atomLabel, 8, 0, 0, new[] { x, y, z }, occ, isIso, isU, iso, aniso));
            }
        }

        if (journalNameFull != "" || journalCodenASTM != "") journal.Append(journalNameFull).Append(' ').Append(journalCodenASTM);
        if (issue != "") journal.Append(", ").Append(issue);
        if (volume != "") journal.Append(", ").Append(volume);
        if (year != "") journal.Append('(').Append(year).Append(')');
        if (pageFirst != "") journal.Append(", ").Append(pageFirst);
        if (pageLast != "") journal.Append('-').Append(pageLast);

        var authours = new StringBuilder();
        foreach (string s in author)
            authours.Append(s).Append("; ");

        return new Crystal2
        {
            CellTexts = new[] { a, b, c, alpha, beta, gamma },
            sym = (short)sgnum,
            name = name,
            atoms = atoms,
            auth = authours.ToString(),
            jour = journal.ToString(),
            sect = sectionTitle,
            argb = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)).ToArgb()
        };
    }

    private static V3 norm(V3 v)
    {
        var d = new[] { v.X, v.Y, v.Z };
        for (int i = 0; i < 3; i++)
        {
            while (d[i] > 0.9999999)
                d[i]--;
            while (d[i] < -0.0000001)
                d[i]++;
        }
        return new V3(d[0], d[1], d[2]);
    }

    private static int SearchSGseriesNumberForCIF(string SgNameHM, string SgNameHall, int SpaceGroupNumber, bool isRhomboShape, bool isAlpha90, bool isBeta90, bool isGamma90)
    {
        int symmetrySeriesNumber = -1;
        if (SgNameHall != "")
        {
            var hall = SgNameHall.Replace(" ", "");
            for (int i = 0; i < SymmetryStatic.TotalSpaceGroupNumber; i++)
                if (hall == SymmetryStatic.Symmetries[i].SpaceGroupHallStr)
                {
                    symmetrySeriesNumber = i;
                    break;
                }
            if (symmetrySeriesNumber != -1)
                return symmetrySeriesNumber;
        }
        SgNameHM = SgNameHM.Replace("_", " ");
        SgNameHM = SgNameHM.Replace("{hexagonalal axes}", " ");
        SgNameHM = SgNameHM.Replace("{rhombohedral axes}", " ");

        SgNameHM = SgNameHM.TrimStart(' ').TrimEnd(' ');
        if (SgNameHM.EndsWith("RS", Ord) || SgNameHM.EndsWith("HR", Ord))
            SgNameHM = SgNameHM.Remove(SgNameHM.Length - 2, 2).TrimEnd(' ');

        if (SgNameHM.EndsWith("H", Ord) || SgNameHM.EndsWith("h", Ord) || SgNameHM.EndsWith("R", Ord) || SgNameHM.EndsWith("r", Ord))
            SgNameHM = SgNameHM.Remove(SgNameHM.Length - 1, 1).TrimEnd(' ');

        if (SgNameHM.EndsWith(":", Ord))
            SgNameHM = SgNameHM.Remove(SgNameHM.Length - 1, 1).TrimEnd(' ');

        bool IsOrigineChoice2 = false;
        if (SgNameHM.EndsWith("Z", Ord))//最後にZがついていたらOriginChoice2
        {
            IsOrigineChoice2 = true;
            SgNameHM = SgNameHM.TrimEnd('Z').TrimEnd();
        }
        if (SgNameHM.EndsWith("S", Ord))//最後がSだったらOriginChoice1
            SgNameHM = SgNameHM.TrimEnd('S').TrimEnd();

        if (SgNameHM.EndsWith("S1", Ord))//最後がS1だったらOriginChoice1
            SgNameHM = SgNameHM.Replace("S1", "").TrimEnd();
        if (SgNameHM.EndsWith("Z1", Ord))//最後がZ1だったらOriginChoice1
            SgNameHM = SgNameHM.Replace("Z1", "").TrimEnd();
        if (SgNameHM.EndsWith(":S1", Ord))//最後が:S1だったらOriginChoice1
            SgNameHM = SgNameHM.Replace(":S1", "").TrimEnd();
        if (SgNameHM.EndsWith(":1", Ord))//最後が:1だったらOriginChoice1
            SgNameHM = SgNameHM.Replace(":1", "").TrimEnd();

        if (SgNameHM.EndsWith("S2", Ord))//最後がS2だったらOriginChoice2
        {
            SgNameHM = SgNameHM.Replace("S2", "").TrimEnd();
            IsOrigineChoice2 = true;
        }
        if (SgNameHM.EndsWith("Z2", Ord))//最後がZ2だったらOriginChoice2
        {
            SgNameHM = SgNameHM.Replace("Z2", "").TrimEnd();
            IsOrigineChoice2 = true;
        }
        if (SgNameHM.EndsWith(":S2", Ord))//最後が:S2だったらOriginChoice2
        {
            SgNameHM = SgNameHM.Replace(":S2", "").TrimEnd();
            IsOrigineChoice2 = true;
        }
        if (SgNameHM.EndsWith(":2", Ord))//最後が:S2だったらOriginChoice2
        {
            SgNameHM = SgNameHM.Replace(":2", "").TrimEnd();
            IsOrigineChoice2 = true;
        }
        if (SgNameHM.EndsWith("O2", Ord))//最後が:S2だったらOriginChoice2
        {
            SgNameHM = SgNameHM.Replace("O2", "").TrimEnd();
            IsOrigineChoice2 = true;
        }

        SgNameHM = SgNameHM.Replace("~", "");

        //一文字目以降の英字は全て小文字に
        SgNameHM = SgNameHM[0] + SgNameHM.Remove(0, 1).ToLower();

        SgNameHM = SgNameHM.Replace("P(-1)", "P-1");

        SgNameHM = SgNameHM.Replace("R 32", "R32");

        SgNameHM = SgNameHM.Replace("P3(1)21", "P 3sub1 2 1");
        SgNameHM = SgNameHM.Replace("P3(2)21", "P 3sub2 2 1");
        SgNameHM = SgNameHM.Replace("P3121", "P 3sub1 2 1");
        SgNameHM = SgNameHM.Replace("P3221", "P 3sub2 2 1");
        SgNameHM = SgNameHM.Replace("P 31 21", "P 3sub1 2 1");
        SgNameHM = SgNameHM.Replace("P 32 21", "P 3sub2 2 1");
        SgNameHM = SgNameHM.Replace("P321", "P 3 2 1");

        SgNameHM = SgNameHM.Replace("P61", "P 6sub1");
        SgNameHM = SgNameHM.Replace("P62", "P 6sub2");
        SgNameHM = SgNameHM.Replace("P63", "P 6sub3");
        SgNameHM = SgNameHM.Replace("P64", "P 6sub4");
        SgNameHM = SgNameHM.Replace("P65", "P 6sub5");

        SgNameHM = SgNameHM.Replace("I41", "I 4sub1");
        SgNameHM = SgNameHM.Replace("P42", "P 4sub2");

        SgNameHM = SgNameHM.Replace("P42/m b c", "P 4sub2/m b c");

        SgNameHM = SgNameHM.Replace("P4322", "P 4sub3 2 2");

        SgNameHM = SgNameHM.Replace(" 61", " 6sub1");
        SgNameHM = SgNameHM.Replace(" 62", " 6sub2");
        SgNameHM = SgNameHM.Replace(" 63", " 6sub3");
        SgNameHM = SgNameHM.Replace(" 64", " 6sub4");
        SgNameHM = SgNameHM.Replace(" 65", " 6sub5");
        SgNameHM = SgNameHM.Replace(" 41", " 4sub1");
        SgNameHM = SgNameHM.Replace(" 42", " 4sub2");
        SgNameHM = SgNameHM.Replace(" 43", " 4sub3");
        SgNameHM = SgNameHM.Replace(" 31", " 3sub1");
        SgNameHM = SgNameHM.Replace(" 32", " 3sub2");
        SgNameHM = SgNameHM.Replace(" 21", " 2sub1");

        SgNameHM = SgNameHM.Replace("2(1)", " 2sub1");
        SgNameHM = SgNameHM.Replace("3(1)", " 3sub1");
        SgNameHM = SgNameHM.Replace("3(2)", " 3sub2");
        SgNameHM = SgNameHM.Replace("4(1)", " 4sub1");
        SgNameHM = SgNameHM.Replace("4(2)", " 4sub2");
        SgNameHM = SgNameHM.Replace("4(3)", " 4sub3");
        SgNameHM = SgNameHM.Replace("6(1)", " 6sub1");
        SgNameHM = SgNameHM.Replace("6(2)", " 6sub2");
        SgNameHM = SgNameHM.Replace("6(3)", " 6sub3");
        SgNameHM = SgNameHM.Replace("6(4)", " 6sub4");
        SgNameHM = SgNameHM.Replace("6(5)", " 6sub5");

        SgNameHM = SgNameHM.Replace("21", " 2sub1");

        SgNameHM = SgNameHM.Replace("  ", " ");

        string temp = SgNameHM.Replace(" ", "");
        #region
        if (temp == "Pm3") SgNameHM = "P m -3";
        else if (temp == "Pn3") SgNameHM = "P n -3";
        else if (temp == "Fm3") SgNameHM = "F m -3";
        else if (temp == "Fd3") SgNameHM = "F d -3";
        else if (temp == "Im3") SgNameHM = "I m -3";
        else if (temp == "Pa3") SgNameHM = "P a -3";
        else if (temp == "Ia3") SgNameHM = "I a -3";

        else if (temp == "Pm3m") SgNameHM = "P m -3 m";
        else if (temp == "Pn3n") SgNameHM = "P m -3 m";
        else if (temp == "Pm3n") SgNameHM = "P m -3 n";
        else if (temp == "Pn3m") SgNameHM = "P n -3 m";
        else if (temp == "Fm3m") SgNameHM = "F m -3 m";
        else if (temp == "Fm3c") SgNameHM = "F m -3 c";
        else if (temp == "Fd3m") SgNameHM = "F d -3 m";
        else if (temp == "Fd3c") SgNameHM = "F d -3 c";
        else if (temp == "Im3m") SgNameHM = "I m -3 m";
        else if (temp == "Ia3d") SgNameHM = "I a -3 d";
        else if (temp == "I2sub1/a-3") SgNameHM = "I a 3";


        else if (temp == "R-32/c") SgNameHM = "R -3 c";

        else if (temp == "P2" && isAlpha90 && isGamma90) SgNameHM = "P 1 2 1";
        else if (temp == "P2" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 2";
        else if (temp == "P2" && isBeta90 && isGamma90) SgNameHM = "P 2 1 1";
        else if (temp == "P2sub1" && isAlpha90 && isGamma90) SgNameHM = "P 1 2sub1 1";
        else if (temp == "P2sub1" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 2sub1";
        else if (temp == "P2sub1" && isBeta90 && isGamma90) SgNameHM = "P 2sub1 1 1";
        else if (temp == "C2" && isAlpha90 && isGamma90) SgNameHM = "C 1 2 1";
        else if (temp == "A2" && isAlpha90 && isGamma90) SgNameHM = "A 1 2 1";
        else if (temp == "I2" && isAlpha90 && isGamma90) SgNameHM = "I 1 2 1";
        else if (temp == "A2" && isAlpha90 && isBeta90) SgNameHM = "A 1 1 2";
        else if (temp == "B2" && isAlpha90 && isBeta90) SgNameHM = "B 1 1 2";
        else if (temp == "I2" && isAlpha90 && isBeta90) SgNameHM = "I 1 1 2";
        else if (temp == "B2" && isBeta90 && isGamma90) SgNameHM = "B 2 1 1";
        else if (temp == "C2" && isBeta90 && isGamma90) SgNameHM = "C 2 1 1";
        else if (temp == "I2" && isBeta90 && isGamma90) SgNameHM = "I 2 1 1";
        else if (temp == "Pm" && isAlpha90 && isGamma90) SgNameHM = "P 1 m 1";
        else if (temp == "Pm" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 m";
        else if (temp == "Pm" && isBeta90 && isGamma90) SgNameHM = "P m 1 1";
        else if (temp == "Pc" && isAlpha90 && isGamma90) SgNameHM = "P 1 c 1";
        else if (temp == "Pn" && isAlpha90 && isGamma90) SgNameHM = "P 1 n 1";
        else if (temp == "Pa" && isAlpha90 && isGamma90) SgNameHM = "P 1 a 1";
        else if (temp == "Pa" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 a";
        else if (temp == "Pn" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 n";
        else if (temp == "Pb" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 b";
        else if (temp == "Pb" && isBeta90 && isGamma90) SgNameHM = "P b 1 1";
        else if (temp == "Pn" && isBeta90 && isGamma90) SgNameHM = "P n 1 1";
        else if (temp == "Pc" && isBeta90 && isGamma90) SgNameHM = "P c 1 1";
        else if (temp == "Cm" && isAlpha90 && isGamma90) SgNameHM = "C 1 m 1";
        else if (temp == "Am" && isAlpha90 && isGamma90) SgNameHM = "A 1 m 1";
        else if (temp == "Im" && isAlpha90 && isGamma90) SgNameHM = "I 1 m 1";
        else if (temp == "Am" && isAlpha90 && isBeta90) SgNameHM = "A 1 1 m";
        else if (temp == "Bm" && isAlpha90 && isBeta90) SgNameHM = "B 1 1 m";
        else if (temp == "Im" && isAlpha90 && isBeta90) SgNameHM = "I 1 1 m";
        else if (temp == "Bm" && isBeta90 && isGamma90) SgNameHM = "B m 1 1";
        else if (temp == "Cm" && isBeta90 && isGamma90) SgNameHM = "C m 1 1";
        else if (temp == "Im" && isBeta90 && isGamma90) SgNameHM = "I m 1 1";
        else if (temp == "Cc" && isAlpha90 && isGamma90) SgNameHM = "C 1 c 1";
        else if (temp == "An" && isAlpha90 && isGamma90) SgNameHM = "A 1 n 1";
        else if (temp == "Ia" && isAlpha90 && isGamma90) SgNameHM = "I 1 a 1";
        else if (temp == "Aa" && isAlpha90 && isGamma90) SgNameHM = "A 1 a 1";
        else if (temp == "Cn" && isAlpha90 && isGamma90) SgNameHM = "C 1 n 1";
        else if (temp == "Ic" && isAlpha90 && isGamma90) SgNameHM = "I 1 c 1";
        else if (temp == "Aa" && isAlpha90 && isBeta90) SgNameHM = "A 1 1 a";
        else if (temp == "Bn" && isAlpha90 && isBeta90) SgNameHM = "B 1 1 n";
        else if (temp == "Ib" && isAlpha90 && isBeta90) SgNameHM = "I 1 1 b";
        else if (temp == "Bb" && isAlpha90 && isBeta90) SgNameHM = "B 1 1 b";
        else if (temp == "An" && isAlpha90 && isBeta90) SgNameHM = "A 1 1 n";
        else if (temp == "Ia" && isAlpha90 && isBeta90) SgNameHM = "I 1 1 a";
        else if (temp == "Bb" && isBeta90 && isGamma90) SgNameHM = "B b 1 1";
        else if (temp == "Cn" && isBeta90 && isGamma90) SgNameHM = "C n 1 1";
        else if (temp == "Ic" && isBeta90 && isGamma90) SgNameHM = "I c 1 1";
        else if (temp == "Cc" && isBeta90 && isGamma90) SgNameHM = "C c 1 1";
        else if (temp == "Bn" && isBeta90 && isGamma90) SgNameHM = "B n 1 1";
        else if (temp == "Ib" && isBeta90 && isGamma90) SgNameHM = "I b 1 1";
        else if (temp == "P2/m" && isAlpha90 && isGamma90) SgNameHM = "P 1 2/m 1";
        else if (temp == "P2/m" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 2/m";
        else if (temp == "P2/m" && isBeta90 && isGamma90) SgNameHM = "P 2/m 1 1";
        else if (temp == "P2sub/m" && isAlpha90 && isGamma90) SgNameHM = "P 1 2sub1/m 1";
        else if (temp == "P2sub/m" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 2sub1/m";
        else if (temp == "P2sub/m" && isBeta90 && isGamma90) SgNameHM = "P 2sub1/m 1 1";
        else if (temp == "C2/m" && isAlpha90 && isGamma90) SgNameHM = "C 1 2/m 1";
        else if (temp == "A2/m" && isAlpha90 && isGamma90) SgNameHM = "A 1 2/m 1";
        else if (temp == "I2/m" && isAlpha90 && isGamma90) SgNameHM = "I 1 2/m 1";
        else if (temp == "A2/m" && isAlpha90 && isBeta90) SgNameHM = "A 1 1 2/m";
        else if (temp == "B2/m" && isAlpha90 && isBeta90) SgNameHM = "B 1 1 2/m";
        else if (temp == "I2/m" && isAlpha90 && isBeta90) SgNameHM = "I 1 1 2/m";
        else if (temp == "B2/m" && isBeta90 && isGamma90) SgNameHM = "B 2/m 1 1";
        else if (temp == "C2/m" && isBeta90 && isGamma90) SgNameHM = "C 2/m 1 1";
        else if (temp == "I2/m" && isBeta90 && isGamma90) SgNameHM = "I 2/m 1 1";
        else if (temp == "P2/c" && isAlpha90 && isGamma90) SgNameHM = "P 1 2/c 1";
        else if (temp == "P2/n" && isAlpha90 && isGamma90) SgNameHM = "P 1 2/n 1";
        else if (temp == "P2/a" && isAlpha90 && isGamma90) SgNameHM = "P 1 2/a 1";
        else if (temp == "P2/a" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 2/a";
        else if (temp == "P2/n" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 2/n";
        else if (temp == "P2/b" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 2/b";
        else if (temp == "P2/b" && isBeta90 && isGamma90) SgNameHM = "P 2/b 1 1";
        else if (temp == "P2/n" && isBeta90 && isGamma90) SgNameHM = "P 2/n 1 1";
        else if (temp == "P2/c" && isBeta90 && isGamma90) SgNameHM = "P 2/c 1 1";
        else if (temp == "P2sub1/c" && isAlpha90 && isGamma90) SgNameHM = "P 1 2sub1/c 1";
        else if (temp == "P2sub1/n" && isAlpha90 && isGamma90) SgNameHM = "P 1 2sub1/n 1";
        else if (temp == "P2sub1/a" && isAlpha90 && isGamma90) SgNameHM = "P 1 2sub1/a 1";
        else if (temp == "P2sub1/a" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 2sub1/a";
        else if (temp == "P2sub1/n" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 2sub1/n";
        else if (temp == "P2sub1/b" && isAlpha90 && isBeta90) SgNameHM = "P 1 1 2sub1/b";
        else if (temp == "P2sub1/b" && isBeta90 && isGamma90) SgNameHM = "P 2sub1/b 1 1";
        else if (temp == "P2sub1/n" && isBeta90 && isGamma90) SgNameHM = "P 2sub1/n 1 1";
        else if (temp == "P2sub1/c" && isBeta90 && isGamma90) SgNameHM = "P 2sub1/c 1 1";
        else if (temp == "C2/c" && isAlpha90 && isGamma90) SgNameHM = "C 1 2/c 1";
        else if (temp == "A2/n" && isAlpha90 && isGamma90) SgNameHM = "A 1 2/n 1";
        else if (temp == "I2/a" && isAlpha90 && isGamma90) SgNameHM = "I 1 2/a 1";
        else if (temp == "A2/a" && isAlpha90 && isGamma90) SgNameHM = "A 1 2/a 1";
        else if (temp == "C2/n" && isAlpha90 && isGamma90) SgNameHM = "C 1 2/n 1";
        else if (temp == "I2/c" && isAlpha90 && isGamma90) SgNameHM = "I 1 2/c 1";
        else if (temp == "A2/a" && isAlpha90 && isBeta90) SgNameHM = "A 1 1 2/a";
        else if (temp == "B2/n" && isAlpha90 && isBeta90) SgNameHM = "B 1 1 2/n";
        else if (temp == "I2/b" && isAlpha90 && isBeta90) SgNameHM = "I 1 1 2/b";
        else if (temp == "B2/b" && isAlpha90 && isBeta90) SgNameHM = "B 1 1 2/b";
        else if (temp == "A2/n" && isAlpha90 && isBeta90) SgNameHM = "A 1 1 2/n";
        else if (temp == "I2/a" && isAlpha90 && isBeta90) SgNameHM = "I 1 1 2/a";
        else if (temp == "B2/b" && isBeta90 && isGamma90) SgNameHM = "B 2/b 1 1";
        else if (temp == "C2/n" && isBeta90 && isGamma90) SgNameHM = "C 2/n 1 1";
        else if (temp == "I2/c" && isBeta90 && isGamma90) SgNameHM = "I 2/c 1 1";
        else if (temp == "C2/c" && isBeta90 && isGamma90) SgNameHM = "C 2/c 1 1";
        else if (temp == "B2/n" && isBeta90 && isGamma90) SgNameHM = "B 2/n 1 1";
        else if (temp == "I2/b" && isBeta90 && isGamma90) SgNameHM = "I 2/b 1 1";
        #endregion

        //文字列を含んでいて、かつ、文字数が少ない空間群を選択する (C1とC121などを見分けるため)

        int length = int.MaxValue;
        var sg = SymmetryStatic.SpaceGroupListWithoutSpace;
        SgNameHM = SgNameHM.Replace(" ", "");
        for (int i = 0; i < SymmetryStatic.TotalSpaceGroupNumber; i++)
        {
            if (sg[i].Length < length)
            {
                var sg_list = new List<string>(new[] { sg[i] });

                if (sg[i].Contains('e'))
                {
                    sg_list.Add(sg[i].Replace('e', 'a'));
                    sg_list.Add(sg[i].Replace('e', 'b'));
                    sg_list.Add(sg[i].Replace('e', 'c'));
                }

                if (sg_list.Any(s => s.IndexOf(SgNameHM, 0) != -1))
                {
                    symmetrySeriesNumber = i;
                    length = sg[i].Length;
                }
            }
        }

        //Rhombohedoralのときの処置
        if (isRhomboShape && SymmetryStatic.Symmetries[symmetrySeriesNumber].SpaceGroupHMStr.Contains("Hex", StringComparison.Ordinal))
            symmetrySeriesNumber++;

        //originChoiceが2のときの対処
        if (IsOrigineChoice2 && SymmetryStatic.Symmetries[symmetrySeriesNumber].SpaceGroupHMStr.Contains("(1)"))
            symmetrySeriesNumber++;

        if (SpaceGroupNumber >= 1 && SpaceGroupNumber <= 230)
            if (symmetrySeriesNumber >= 1 || SpaceGroupNumber != SymmetryStatic.Symmetries[symmetrySeriesNumber].SpaceGroupNumber)
                for (int i = 0; i < SymmetryStatic.TotalSpaceGroupNumber; i++)
                    if (SymmetryStatic.Symmetries[i].SpaceGroupNumber == SpaceGroupNumber)
                    {
                        symmetrySeriesNumber = i;
                        break;
                    }

        return symmetrySeriesNumber;
    }


    #endregion

    #region CIFファイルへの変換

    public static string ConvertToCIF(Crystal crystal)
    {
        var sb = new StringBuilder();
        sb.AppendLine("# This file is exported from \"" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + "\"");
        sb.AppendLine("# http://pmsl.planet.sci.kobe-u.ac.jp/~seto");

        sb.AppendLine("data_global");
        sb.AppendLine("_chemical_name '" + crystal.Name + "'");

        sb.AppendLine("loop_");
        sb.AppendLine("_publ_author_name");
        foreach (string str in crystal.PublAuthorName.Split(',', true))
            sb.AppendLine("'" + str.Trim() + "'");

        sb.AppendLine("_journal_name '" + crystal.Journal + "'");

        #region 論文タイトル
        sb.AppendLine("_publ_section_title");
        sb.AppendLine(";");
        string title = "";
        foreach (string t in crystal.PublSectionTitle.Split(' ', true))
        {
            if ((title + " " + t).Length > 80)
            {
                sb.AppendLine(title);
                title = "";
            }
            title += " " + t;
        }
        if (title != "")
            sb.AppendLine(title);
        sb.AppendLine(";");
        #endregion

        sb.AppendLine("_chemical_formula_sum '" + crystal.ChemicalFormulaSum + "'");
        sb.AppendLine("_cell_length_a " + (crystal.A * 10).ToString("f6"));
        sb.AppendLine("_cell_length_b " + (crystal.B * 10).ToString("f6"));
        sb.AppendLine("_cell_length_c " + (crystal.C * 10).ToString("f6"));
        sb.AppendLine("_cell_angle_alpha " + (crystal.Alpha / Math.PI * 180).ToString("f6"));
        sb.AppendLine("_cell_angle_beta " + (crystal.Beta / Math.PI * 180).ToString("f6"));
        sb.AppendLine("_cell_angle_gamma " + (crystal.Gamma / Math.PI * 180).ToString("f6"));
        sb.AppendLine("_cell_volume " + (crystal.Volume * 1000).ToString("f6"));
        sb.AppendLine("_exptl_crystal_density_diffrn " + crystal.Density.ToString("f6"));

        var sym = crystal.Symmetry;
        sb.AppendLine("_space_group_IT_number " + sym.SpaceGroupNumber);
        sb.AppendLine("_symmetry_cell_setting '" + sym.CrystalSystemStr + "'");
        var hm = sym.SpaceGroupHMStr;
        hm = hm.Replace("Hex", "");
        hm = hm.Replace("Rho", "");
        sb.AppendLine("_symmetry_space_group_name_H-M '" + hm + "'");
        sb.AppendLine("_symmetry_space_group_name_Hall '" + sym.SpaceGroupHallStr + "'");

        #region 原子の等価位置
        sb.AppendLine("loop_");
        sb.AppendLine("_symmetry_equiv_pos_as_xyz");
        bool[][] flag = Array.Empty<bool[]>();
        if (sym.LatticeTypeStr == "P") flag = new[] { new[] { false, false, false } };
        else if (sym.LatticeTypeStr == "A") flag = new[] { new[] { false, false, false }, new[] { false, true, true } };
        else if (sym.LatticeTypeStr == "B") flag = new[] { new[] { false, false, false }, new[] { true, false, true } };
        else if (sym.LatticeTypeStr == "C") flag = new[] { new[] { false, false, false }, new[] { true, true, false } };
        else if (sym.LatticeTypeStr == "I") flag = new[] { new[] { false, false, false }, new[] { true, true, true } };
        else if (sym.LatticeTypeStr == "F") flag = new[] { new[] { false, false, false }, new[] { false, true, true }, new[] { true, false, true }, new[] { true, true, false } };

        foreach (string wp in SymmetryStatic.WyckoffPositions[crystal.SymmetrySeriesNumber][0].PositionStr)
        {
            if (sym.SpaceGroupHMsubStr != "H")
            {
                for (int i = 0; i < flag.Length; i++)
                {
                    string[] xyz = wp.Split(',', true);
                    for (int j = 0; j < flag[i].Length; j++)
                    {
                        if (flag[i][j])
                        {
                            if (xyz[j].EndsWith("+1/2", Ord)) xyz[j] = xyz[j].Replace("+1/2", "");
                            else if (xyz[j].EndsWith("+1/4", Ord)) xyz[j] = xyz[j].Replace("+1/4", "+3/4");
                            else if (xyz[j].EndsWith("+3/4", Ord)) xyz[j] = xyz[j].Replace("+3/4", "+1/4");
                            else xyz[j] += "+1/2";
                        }
                    }
                    sb.AppendLine($"  '{xyz[0]},{xyz[1]},{xyz[2]}'");
                }
            }
            else//R格子のHexaセッティングのとき
            {
                sb.AppendLine("  '" + wp + "'");//(0,0,0)
                                                //(1/3,2/3,2/3)
                string[] xyz = wp.Split(',', true);
                xyz[0] += "+1/3";
                xyz[1] += "+2/3";
                if (xyz[2].EndsWith("+1/2", Ord)) xyz[2] = xyz[2].Replace("+1/2", "+1/6");
                else xyz[2] += "+2/3";
                sb.AppendLine($"  '{xyz[0]},{xyz[1]},{xyz[2]}'");
                //(2/3,1/3,1/3)
                xyz = wp.Split(',', true);
                xyz[0] += "+2/3";
                xyz[1] += "+1/3";
                if (xyz[2].EndsWith("+1/2", Ord)) xyz[2] = xyz[2].Replace("+1/2", "+5/6");
                else xyz[2] += "+1/3";
                sb.AppendLine($"  '{xyz[0]},{xyz[1]},{xyz[2]}'");
            }
        }
        #endregion

        #region 各原子の情報
        sb.AppendLine("loop_");
        sb.AppendLine("_atom_site_label");
        sb.AppendLine("_atom_site_type_symbol");
        sb.AppendLine("_atom_site_fract_x");
        sb.AppendLine("_atom_site_fract_y");
        sb.AppendLine("_atom_site_fract_z");
        sb.AppendLine("_atom_site_occupancy");
        sb.AppendLine("_atom_site_U_iso_or_equiv");

        foreach (var a in crystal.Atoms)
        {
            var u = double.IsNaN(a.Dsf.Uiso) ? 0 : a.Dsf.Uiso;
            sb.AppendLine($"{a.Label} {AtomStatic.AtomicName(a.AtomicNumber)} {a.X:f5} {a.Y:f5} {a.Z:f5} {a.Occ:f5} {u:f5}");
        }

        sb.AppendLine("loop_");
        sb.AppendLine("_atom_site_aniso_label");
        sb.AppendLine("_atom_site_aniso_U_11");
        sb.AppendLine("_atom_site_aniso_U_22");
        sb.AppendLine("_atom_site_aniso_U_33");
        sb.AppendLine("_atom_site_aniso_U_23");
        sb.AppendLine("_atom_site_aniso_U_13");
        sb.AppendLine("_atom_site_aniso_U_12");
        foreach (var a in crystal.Atoms)
        {
            if (!a.Dsf.UseIso)
            {
                var u11 = double.IsNaN(a.Dsf.U11) ? 0 : a.Dsf.U11;
                var u22 = double.IsNaN(a.Dsf.U22) ? 0 : a.Dsf.U22;
                var u33 = double.IsNaN(a.Dsf.U33) ? 0 : a.Dsf.U33;
                var u23 = double.IsNaN(a.Dsf.U23) ? 0 : a.Dsf.U23;
                var u31 = double.IsNaN(a.Dsf.U31) ? 0 : a.Dsf.U31;
                var u12 = double.IsNaN(a.Dsf.U12) ? 0 : a.Dsf.U12;
                sb.AppendLine($"{a.Label} {u11:f5} {u22:f5} {u33:f5} {u23:f5} {u31:f5} {u12:f5}");
            }
        }
        #endregion

        return sb.ToString();
    }
    #endregion

    #region OpenGLのためのBond設定
    public static void SetOpenGL_property(Crystal c)
    {
        foreach (Atoms a in c.Atoms)
            a.ResetVesta();
        c.Bonds = Bonds.GetVestaBonds(c.Atoms.Select(a => a.ElementName).Distinct());

        #region お蔵入り
        /*
        //先ず原子の色、半径を設定
        foreach (Atoms a in c.Atoms)
        {
            switch (a.ElementName)
            {
#region イオン半径、色を設定
                case "1: H": a.Radius = 0.005f; a.Argb = Color.FromArgb(98, 11, 181).ToArgb(); break;
                case "2: He": a.Radius = 0.05f; a.Argb = Color.FromArgb(137, 250, 106).ToArgb(); break;
                case "3: Li": a.Radius = 0.38f; a.Argb = Color.FromArgb(43, 200, 157).ToArgb(); break;
                case "4: Be": a.Radius = 0.135f; a.Argb = Color.FromArgb(161, 34, 128).ToArgb(); break;
                case "5: B": a.Radius = 0.055f; a.Argb = Color.FromArgb(249, 251, 174).ToArgb(); break;
                case "6: C": a.Radius = 0.075f; a.Argb = Color.FromArgb(211, 45, 173).ToArgb(); break;
                case "7: N": a.Radius = 0.08f; a.Argb = Color.FromArgb(248, 240, 241).ToArgb(); break;
                case "8: O": a.Radius = 0.71f; a.Argb = Color.FromArgb(255, 0, 0).ToArgb(); break;
                case "9: F": a.Radius = 0.665f; a.Argb = Color.FromArgb(153, 165, 227).ToArgb(); break;
                case "10: Ne": a.Radius = 0.005f; a.Argb = Color.FromArgb(65, 223, 157).ToArgb(); break;
                case "11: Na": a.Radius = 0.59f; a.Argb = Color.FromArgb(187, 195, 36).ToArgb(); break;
                case "12: Mg": a.Radius = 0.445f; a.Argb = Color.FromArgb(193, 162, 107).ToArgb(); break;
                case "13: Al": a.Radius = 0.195f; a.Argb = Color.FromArgb(153, 46, 158).ToArgb(); break;
                case "14: Si": a.Radius = 0.20f; a.Argb = Color.FromArgb(0, 0, 255).ToArgb(); break;
                case "15: P": a.Radius = 0.145f; a.Argb = Color.FromArgb(101, 199, 183).ToArgb(); break;
                case "16: S": a.Radius = 0.92f; a.Argb = Color.FromArgb(87, 191, 62).ToArgb(); break;
                case "17: Cl": a.Radius = 0.905f; a.Argb = Color.FromArgb(23, 36, 16).ToArgb(); break;
                case "18: Ar": a.Radius = 0.05f; a.Argb = Color.FromArgb(203, 82, 236).ToArgb(); break;
                case "19: K": a.Radius = 0.755f; a.Argb = Color.FromArgb(216, 96, 67).ToArgb(); break;
                case "20: Ca": a.Radius = 0.56f; a.Argb = Color.FromArgb(35, 159, 207).ToArgb(); break;
                case "21: Sc": a.Radius = 0.435f; a.Argb = Color.FromArgb(165, 191, 130).ToArgb(); break;
                case "22: Ti": a.Radius = 0.25f; a.Argb = Color.FromArgb(64, 112, 111).ToArgb(); break;
                case "23: V": a.Radius = 0.29f; a.Argb = Color.FromArgb(158, 54, 69).ToArgb(); break;
                case "24: Cr": a.Radius = 0.3075f; a.Argb = Color.FromArgb(43, 48, 188).ToArgb(); break;
                case "25: Mn": a.Radius = 0.29f; a.Argb = Color.FromArgb(69, 122, 233).ToArgb(); break;
                case "26: Fe": a.Radius = 0.400f; a.Argb = Color.FromArgb(23, 239, 57).ToArgb(); break;
                case "27: Co": a.Radius = 0.305f; a.Argb = Color.FromArgb(119, 8, 111).ToArgb(); break;
                case "28: Ni": a.Radius = 0.3f; a.Argb = Color.FromArgb(190, 121, 182).ToArgb(); break;
                case "29: Cu": a.Radius = 0.285f; a.Argb = Color.FromArgb(215, 226, 61).ToArgb(); break;
                case "30: Zn": a.Radius = 0.37f; a.Argb = Color.FromArgb(122, 74, 64).ToArgb(); break;
                case "31: Ga": a.Radius = 0.31f; a.Argb = Color.FromArgb(22, 97, 30).ToArgb(); break;
                case "32: Ge": a.Radius = 0.265f; a.Argb = Color.FromArgb(60, 177, 233).ToArgb(); break;
                case "33: As": a.Radius = 0.23f; a.Argb = Color.FromArgb(159, 16, 53).ToArgb(); break;
                case "34: Se": a.Radius = 0.99f; a.Argb = Color.FromArgb(250, 56, 55).ToArgb(); break;
                case "35: Br": a.Radius = 0.98f; a.Argb = Color.FromArgb(221, 111, 159).ToArgb(); break;
                case "36: Kr": a.Radius = 0.05f; a.Argb = Color.FromArgb(225, 104, 113).ToArgb(); break;
                case "37: Rb": a.Radius = 0.805f; a.Argb = Color.FromArgb(51, 135, 205).ToArgb(); break;
                case "38: Sr": a.Radius = 0.63f; a.Argb = Color.FromArgb(158, 31, 36).ToArgb(); break;
                case "39: Y": a.Radius = 0.45f; a.Argb = Color.FromArgb(131, 105, 94).ToArgb(); break;
                case "40: Zr": a.Radius = 0.36f; a.Argb = Color.FromArgb(237, 15, 134).ToArgb(); break;
                case "41: Nb": a.Radius = 0.32f; a.Argb = Color.FromArgb(236, 148, 12).ToArgb(); break;
                case "42: Mo": a.Radius = 0.295f; a.Argb = Color.FromArgb(246, 92, 178).ToArgb(); break;
                case "43: Tc": a.Radius = 0.3f; a.Argb = Color.FromArgb(103, 200, 138).ToArgb(); break;
                case "44: Ru": a.Radius = 0.18f; a.Argb = Color.FromArgb(227, 4, 84).ToArgb(); break;
                case "45: Rh": a.Radius = 0.275f; a.Argb = Color.FromArgb(191, 235, 99).ToArgb(); break;
                case "46: Pd": a.Radius = 0.3075f; a.Argb = Color.FromArgb(150, 156, 76).ToArgb(); break;
                case "47: Ag": a.Radius = 0.47f; a.Argb = Color.FromArgb(55, 51, 185).ToArgb(); break;
                case "48: Cd": a.Radius = 0.475f; a.Argb = Color.FromArgb(84, 100, 150).ToArgb(); break;
                case "49: In": a.Radius = 0.4f; a.Argb = Color.FromArgb(47, 80, 243).ToArgb(); break;
                case "50: Sn": a.Radius = 0.345f; a.Argb = Color.FromArgb(180, 73, 238).ToArgb(); break;
                case "51: Sb": a.Radius = 0.38f; a.Argb = Color.FromArgb(210, 79, 249).ToArgb(); break;
                case "52: Te": a.Radius = 0.485f; a.Argb = Color.FromArgb(92, 27, 100).ToArgb(); break;
                case "53: I": a.Radius = 1.1f; a.Argb = Color.FromArgb(224, 110, 46).ToArgb(); break;
                case "54: Xe": a.Radius = 0.2f; a.Argb = Color.FromArgb(169, 220, 216).ToArgb(); break;
                case "55: Cs": a.Radius = 0.87f; a.Argb = Color.FromArgb(244, 226, 13).ToArgb(); break;
                case "56: Ba": a.Radius = 0.71f; a.Argb = Color.FromArgb(177, 2, 109).ToArgb(); break;
                case "57: La": a.Radius = 0.58f; a.Argb = Color.FromArgb(194, 79, 196).ToArgb(); break;
                case "58: Ce": a.Radius = 0.5715f; a.Argb = Color.FromArgb(88, 221, 142).ToArgb(); break;
                case "59: Pr": a.Radius = 0.563f; a.Argb = Color.FromArgb(247, 156, 100).ToArgb(); break;
                case "60: Nd": a.Radius = 0.4915f; a.Argb = Color.FromArgb(252, 189, 121).ToArgb(); break;
                case "61: Pm": a.Radius = 0.5465f; a.Argb = Color.FromArgb(209, 104, 146).ToArgb(); break;
                case "62: Sm": a.Radius = 0.479f; a.Argb = Color.FromArgb(133, 7, 117).ToArgb(); break;
                case "63: Eu": a.Radius = 0.625f; a.Argb = Color.FromArgb(226, 218, 141).ToArgb(); break;
                case "64: Gd": a.Radius = 0.5265f; a.Argb = Color.FromArgb(26, 83, 227).ToArgb(); break;
                case "65: Tb": a.Radius = 0.52f; a.Argb = Color.FromArgb(160, 216, 140).ToArgb(); break;
                case "66: Dy": a.Radius = 0.485f; a.Argb = Color.FromArgb(13, 71, 184).ToArgb(); break;
                case "67: Ho": a.Radius = 0.5075f; a.Argb = Color.FromArgb(104, 109, 153).ToArgb(); break;
                case "68: Er": a.Radius = 0.502f; a.Argb = Color.FromArgb(21, 168, 23).ToArgb(); break;
                case "69: Tm": a.Radius = 0.497f; a.Argb = Color.FromArgb(221, 220, 184).ToArgb(); break;
                case "70: Yb": a.Radius = 0.434f; a.Argb = Color.FromArgb(23, 151, 178).ToArgb(); break;
                case "71: Lu": a.Radius = 0.4305f; a.Argb = Color.FromArgb(108, 190, 142).ToArgb(); break;
                case "72: Hf": a.Radius = 0.355f; a.Argb = Color.FromArgb(84, 253, 37).ToArgb(); break;
                case "73: Ta": a.Radius = 0.34f; a.Argb = Color.FromArgb(160, 247, 238).ToArgb(); break;
                case "74: W": a.Radius = 0.21f; a.Argb = Color.FromArgb(4, 203, 171).ToArgb(); break;
                case "75: Re": a.Radius = 0.275f; a.Argb = Color.FromArgb(87, 31, 14).ToArgb(); break;
                case "76: Os": a.Radius = 0.245f; a.Argb = Color.FromArgb(218, 170, 3).ToArgb(); break;
                case "77: Ir": a.Radius = 0.3125f; a.Argb = Color.FromArgb(245, 219, 180).ToArgb(); break;
                case "78: Pt": a.Radius = 0.3125f; a.Argb = Color.FromArgb(43, 17, 113).ToArgb(); break;
                case "79: Au": a.Radius = 0.425f; a.Argb = Color.FromArgb(77, 63, 211).ToArgb(); break;
                case "80: Hg": a.Radius = 0.48f; a.Argb = Color.FromArgb(62, 50, 156).ToArgb(); break;
                case "81: Tl": a.Radius = 0.375f; a.Argb = Color.FromArgb(80, 119, 140).ToArgb(); break;
                case "82: Pb": a.Radius = 0.645f; a.Argb = Color.FromArgb(243, 58, 152).ToArgb(); break;
                case "83: Bi": a.Radius = 0.515f; a.Argb = Color.FromArgb(123, 248, 40).ToArgb(); break;
                case "84: Po": a.Radius = 0.335f; a.Argb = Color.FromArgb(213, 176, 49).ToArgb(); break;
                case "85: At": a.Radius = 0.31f; a.Argb = Color.FromArgb(230, 228, 32).ToArgb(); break;
                case "86: Rn": a.Radius = 0.05f; a.Argb = Color.FromArgb(115, 62, 60).ToArgb(); break;
                case "87: Fr": a.Radius = 0.9f; a.Argb = Color.FromArgb(155, 85, 177).ToArgb(); break;
                case "88: Ra": a.Radius = 0.85f; a.Argb = Color.FromArgb(199, 47, 152).ToArgb(); break;
                case "89: Ac": a.Radius = 0.56f; a.Argb = Color.FromArgb(214, 227, 230).ToArgb(); break;
                case "90: Th": a.Radius = 0.525f; a.Argb = Color.FromArgb(153, 241, 22).ToArgb(); break;
                case "91: Pa": a.Radius = 0.45f; a.Argb = Color.FromArgb(80, 156, 227).ToArgb(); break;
                case "92: U": a.Radius = 0.365f; a.Argb = Color.FromArgb(44, 59, 209).ToArgb(); break;
                case "93: Np": a.Radius = 0.49f; a.Argb = Color.FromArgb(10, 253, 55).ToArgb(); break;
                case "94: Pu": a.Radius = 0.48f; a.Argb = Color.FromArgb(95, 160, 240).ToArgb(); break;
                case "95: Am": a.Radius = 0.425f; a.Argb = Color.FromArgb(252, 12, 39).ToArgb(); break;
                case "96: Cm": a.Radius = 0.425f; a.Argb = Color.FromArgb(217, 174, 152).ToArgb(); break;
                case "97: Bk": a.Radius = 0.415f; a.Argb = Color.FromArgb(54, 244, 120).ToArgb(); break;
                case "98: Cf": a.Radius = 0.4105f; a.Argb = Color.FromArgb(161, 45, 164).ToArgb(); break;
#endregion イオン半径、色を設定
            }
            if (!elementList.Contains(a.ElementName))
                elementList.Add(a.ElementName);
        }
        */
        #endregion
    }
    #endregion
}

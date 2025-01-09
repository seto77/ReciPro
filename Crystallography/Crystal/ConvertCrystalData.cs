using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using V3 = OpenTK.Mathematics.Vector3d;

namespace Crystallography;

public class ConvertCrystalData
{
    static readonly System.StringComparison Ord = System.StringComparison.Ordinal;

    #region CrystalList(xml�`��)�̓ǂݍ���/��������
    public static bool SaveCrystalListXml(Crystal[] crystals, string filename)
    {
        if (crystals != null && crystals.Length != 0 && crystals[0].FlexibleMode)
            crystals[0].FlexiblePlane = crystals[0].Plane;

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


    //CrystalList��ǂݍ��ނƂ�
    public static Crystal[] ConvertToCrystalList(string filename)
    {
        var cry = Array.Empty<Crystal>();
        if (filename.ToLower().EndsWith("xml", Ord))//XML�`���̃��X�g��ǂݍ��񂾂Ƃ�
        {
            if (new FileInfo(filename).Length > 10000000)//�Ȃ����t�@�C����3GB�Ƃ��ɂȂ������Ƃ��L�����̂ŁA����ɑ΂���Ώ�. 10MB�ȏゾ������X�L�b�v���邱�Ƃɂ���.
                return cry;

            #region old code
            //�v���p�e�B�����񂪕ύX�ɂ�������Ώ�
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

                //filename = filename + "_";//���؂̂��߃t�@�C���l�[���ύX

                var writer = new StreamWriter(filename, false, Encoding.GetEncoding("UTF-8"));
                for (int i = 0; i < strList.Count; i++)
                    writer.WriteLine(strList[i]);
                writer.Flush();
                writer.Close();
            }
            catch { return null; };
            //�v���p�e�B�����񂪕ύX�ɂ�������Ώ��@�����܂�
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

                foreach (var c in cry.Where(e => e.FlexiblePlane != null && e.FlexiblePlane.Count > 0))
                    c.Plane = c.FlexiblePlane;
            }
            catch { }
        }
        else if (filename.EndsWith("out", Ord))//SMAP�`����ǂݍ��񂾂Ƃ�
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

    #region SMAP�̏o�̓t�@�C��(*.out)�Ǎ�

    /// <summary>
    /// SMAP��out�t�@�C���ǂݍ���
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Crystal[] ConvertFromSMAP(string[] str)
    {
        string wavelength = "";
        string ChemicalFormula = "";
        double a = 0, b = 0, c = 0, alpha = 0, beta = 0, gamma = 0;
        int spaceGroupSeriesNum = 0;
        //��ԌQ
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
        //�������猴�n���W�ǂݎ��
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
                        //���x�������猳�f�����߂�
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
    /// ConvertFromSMAP����Ăяo�����
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

    #endregion SMAP�̏o�̓t�@�C��(*.out)�Ǎ�

    #region AMC��CIF�̓ǂݍ��݃C���^�[�t�F�[�X ConvertCrystal(filename)  

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
            if (AssemblyState.IsDebug)
                System.Windows.Forms.MessageBox.Show(e.Message);
            return null;
        }

    }
    #endregion

    #region amc�t�@�C���̓ǂݍ���

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
    /// amc�t�@�C���̓ǂݍ���
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static Crystal2 ConvertFromAmc(string[] str)
    {
        var n = 0;
        if (str[n].Length == 0)
            n++;

        var Name = str[n];//�����̖��O

        n++; if (str.Length <= n) return null;

        var AuthorName = str[n];//���҂̖��O
        n++; if (str.Length <= n) return null;
        while ((str[n][^1] < '.' || str[n][^1] > '9') && !str[n].Contains("doi.org") && !str[n].Contains("DOI: ")
            && !str[n].Contains("Mineralogy") )//�s�̍Ō�̕����������ł͂Ȃ��Ƃ��@(�������A"doi.org"�̕����񂪑��݂���Ƃ��͏��O) 
        {
            AuthorName += ", " + str[n];
            n++; if (str.Length <= n) return null;
        }

        var Reference = str[n];//���p����
        n++; if (str.Length <= n) return null;

        var Title = str[n];//�����^�C�g��
        n++; if (str.Length <= n) return null;

        //�����Ŋi�q�萔�A�Ώ̐��ƃ^�C�g��
        Crystal2 crystal;
        string extra;
        while (((crystal,extra) = CellParamForAmc(str[n])) == (null,null))
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

        //2nd�Z�b�e�B���O�ɂ�������炸�Ashift�ʂ��[���łȂ��Ƃ��́A1st�Z�b�e�B���O�ɖ߂��B
        if ((xShift != 0 || yShift != 0 || zShift != 0) && SymmetryStatic.SpaceGroupListWithoutSpace[crystal.sym].EndsWith("(2)", Ord))
            crystal.sym--;


        //�������猴�q���W�̓ǂݎ��
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

        for (int i = 0; i < tempStr.Length; i++)//�e���͒l�̕����ʒu�����߂�B
            l[i + 1] = str[n].IndexOf(tempStr[i]) + tempStr[i].Length;
        for (int i = n + 1; i < str.Length; i++)//�ŏ���atom���x��������O������悤�Ȃ̂ł���ɑΏ�
            if (l[1] < str[i].Split(" ", true)[0].Length)
                l[1] = str[i].Split(" ", true)[0].Length;

        //�O�����邢�͘Z��
        bool isHex = crystal.sym >= 430 && crystal.sym <= 488;

        var atoms = new List<Atoms2>();
        for (int i = n + 1; i < str.Length; i++)
        {//���q���W�ǂݎ�胋�[�v�J�n
            str[i] = str[i].PadRight(str[n].Length, ' ');

            var item = str[i].Split(" ", true);
            if (item.Length != l.Length - 1)
            {
                item = new string[l.Length - 1];
                for (int k = 0; k < item.Length; k++)
                {
                    item[k] = str[i][l[k]..l[k + 1]].Trim().TrimEnd();
                    item[k] = item[k].Replace(',', '.');//���܂ɃJ���}�ƃs���I�h���ԈႦ���Ă���
                    if (item[k].Length > 3 && item[k][1] == ' ')
                    {
                        //�񕶎��ڂ��X�y�[�X�̏ꍇ�́A����������Ă���\�����l������B(.123 .456 => ".12", "3 .456" )�@
                        //���̏ꍇ�́A�񕶎��ڂ܂ł��폜���đΉ�����
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
                x = (x.ToDouble() + xShift).ToString("f8").TrimEnd(['0']);
                y = (y.ToDouble() + yShift).ToString("f8").TrimEnd(['0']);
                z = (z.ToDouble() + zShift).ToString("f8").TrimEnd(['0']);
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

            //���x�������猳�f�����߂�
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
                if (temp == "OH") //OH��̂Ƃ�
                    atomicNumber = -1;
                else if (temp == "D") //�d���f�̂Ƃ�
                    atomicNumber = 255;
                else if (temp == "Wat" || temp == "WAT" || temp == "wat") //���̂Ƃ�
                    atomicNumber = -2;
            }

            var IsIso = aniso == null || aniso.All(e => e.Length == 0);

            if (atomicNumber > 0)
            {
                atoms.Add(new Atoms2(label, (byte)atomicNumber, 0, 0, [x, y, z], occ, IsIso, IsUtypeUsed, iso, aniso));
            }
            else if (atomicNumber == -1)//"OH"�̂Ƃ��̑Ώ�
            {
                atoms.Add(new Atoms2(label, 1, 0, 0, [x, y, z], occ, IsIso, IsUtypeUsed, iso, aniso));
                atoms.Add(new Atoms2(label, 8, 0, 0, [x, y, z], occ, IsIso, IsUtypeUsed, iso, aniso));
            }
            else if (atomicNumber == -2)//"Wat"���̂Ƃ��̑Ώ�
            {
                atoms.Add(new Atoms2(label, 1, 0, 0, [x, y, z], occ, IsIso, IsUtypeUsed, iso, aniso));
                atoms.Add(new Atoms2(label, 1, 0, 0, [x, y, z], occ, IsIso, IsUtypeUsed, iso, aniso));
                atoms.Add(new Atoms2(label, 8, 0, 0, [x, y, z], occ, IsIso, IsUtypeUsed, iso, aniso));
            }
        }
        crystal.name = Name;
        crystal.sect = Title;
        crystal.auth = AuthorName;
        crystal.jour = Reference;
        crystal.atoms = atoms;

        //�Ō��,extra (�t�@�C�����̋�Ԋi�q�ƁA�\�t�g���̋�Ԋi�q�̋L�����Ⴄ�ꍇ�̂�)������
        if (extra != null)
        {
            var c = crystal.ToCrystal();
            (double X, double Y, double Z)[] translation = extra switch
            {
                "A" => [(0, 0, 0), (0.0, 0.5, 0.5)],
                "B" => [(0, 0, 0), (0.5, 0.0, 0.5)],
                "C" => [(0, 0, 0), (0.5, 0.5, 0)],
                "I" => [(0, 0, 0), (0.5, 0.5, 0.5)],
                "F" => [(0, 0, 0), (0.0, 0.5, 0.5), (0.5, 0.0, 0.5), (0.5, 0.5, 0)],
                _ => [(0, 0, 0)],
            };

            var atomsEx = new List<Atoms>();
            foreach (var a in c.Atoms)
            {
                foreach (var (X, Y, Z) in translation) 
                    atomsEx.Add(new Atoms(a.Label,a.AtomicNumber,a.SubNumberXray,a.SubNumberElectron,a.Isotope,a.SymmetrySeriesNumber,
                        new Vector3DBase( a.X+X,a.Y+Y,a.Z+Z ), a.PositionError,a.Occ,a.Occ_err,a.Dsf,a.Material,a.Radius,true,false));
            }
            c.Atoms = [.. atomsEx];
            crystal = c.ToCrystal2();
        }



        return crystal;
    }

    private static (Crystal2, string) CellParamForAmc(string str)
    {
        string extra = null;

        double A, B, C, Alpha, Beta, Gamma;
        int symmetrySeriesNumber = -1;
        var s = str.Split(" ", true);
        if (s.Length != 7)
            return (null,null);
        try
        {
            for (int i = 0; i < 6; i++) if (s[i].EndsWith(',')) s[i] = s[i].TrimEnd(','); //�Ō��','�������Ă���Ƃ��͍폜


            //if (Miscellaneous.IsDecimalPointComma)
            //    for (int i = 0; i < 6; i++) s[i] = s[i].Replace('.', ',');
            //else
            //    for (int i = 0; i < 6; i++) s[i] = s[i].Replace(',', '.');
            A = s[0].ToDouble();
            B = s[1].ToDouble();
            C = s[2].ToDouble();
            Alpha = s[3].ToDouble();
            Beta = s[4].ToDouble();
            Gamma = s[5].ToDouble();
        }
        catch { return (null, null); }
        string SgName = s[6];

        SgName = SgName.Replace("_", "sub");
        bool isAsterisk = SgName.Contains('*');
        SgName = SgName.Replace("*", "");

        if (SgName.Contains(":2"))
            isAsterisk = true;
        SgName = SgName.Replace(":1", "");
        SgName = SgName.Replace(":2", "");

        #region ��ԌQ�̏ꍇ����

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
        else if (SgName == "P4/m-32/m") SgName = "Pm-3m";

        else if (SgName == "I2sub1/a-3") SgName = "Ia-3";

        else if (SgName == "R-32/c") SgName = "R-3c";

        else if (SgName == "P2" && Alpha == 90.0 && Gamma == 90.0) SgName = "P121";
        else if (SgName == "P2" && Alpha == 90.0 && Beta == 90.0) SgName = "P112";
        else if (SgName == "P2" && Beta == 90.0 && Gamma == 90.0) SgName = "P211";
        else if (SgName == "P2sub1" && Alpha == 90.0 && Gamma == 90.0) SgName = "P12sub11";
        else if (SgName == "P2sub1" && Alpha == 90.0 && Beta == 90.0) SgName = "P112sub1";
        else if (SgName == "P2sub1" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub111";
        else if (SgName == "C2" && Alpha == 90.0 && Gamma == 90.0) SgName = "C121";
        else if (SgName == "A2" && Alpha == 90.0 && Gamma == 90.0) SgName = "A121";
        else if (SgName == "I2" && Alpha == 90.0 && Gamma == 90.0) SgName = "I121";
        else if (SgName == "A2" && Alpha == 90.0 && Beta == 90.0) SgName = "A112";
        else if (SgName == "B2" && Alpha == 90.0 && Beta == 90.0) SgName = "B112";
        else if (SgName == "I2" && Alpha == 90.0 && Beta == 90.0) SgName = "I112";
        else if (SgName == "B2" && Beta == 90.0 && Gamma == 90.0) SgName = "B211";
        else if (SgName == "C2" && Beta == 90.0 && Gamma == 90.0) SgName = "C211";
        else if (SgName == "I2" && Beta == 90.0 && Gamma == 90.0) SgName = "I211";
        else if (SgName == "Pm" && Alpha == 90.0 && Gamma == 90.0) SgName = "P1m1";
        else if (SgName == "Pm" && Alpha == 90.0 && Beta == 90.0) SgName = "P11m";
        else if (SgName == "Pm" && Beta == 90.0 && Gamma == 90.0) SgName = "Pm11";
        else if (SgName == "Pc" && Alpha == 90.0 && Gamma == 90.0) SgName = "P1c1";
        else if (SgName == "Pn" && Alpha == 90.0 && Gamma == 90.0) SgName = "P1n1";
        else if (SgName == "Pa" && Alpha == 90.0 && Gamma == 90.0) SgName = "P1a1";
        else if (SgName == "Pa" && Alpha == 90.0 && Beta == 90.0) SgName = "P11a";
        else if (SgName == "Pn" && Alpha == 90.0 && Beta == 90.0) SgName = "P11n";
        else if (SgName == "Pb" && Alpha == 90.0 && Beta == 90.0) SgName = "P11b";
        else if (SgName == "Pb" && Beta == 90.0 && Gamma == 90.0) SgName = "Pb11";
        else if (SgName == "Pn" && Beta == 90.0 && Gamma == 90.0) SgName = "Pn11";
        else if (SgName == "Pc" && Beta == 90.0 && Gamma == 90.0) SgName = "Pc11";
        else if (SgName == "Cm" && Alpha == 90.0 && Gamma == 90.0) SgName = "C1m1";
        else if (SgName == "Am" && Alpha == 90.0 && Gamma == 90.0) SgName = "A1m1";
        else if (SgName == "Im" && Alpha == 90.0 && Gamma == 90.0) SgName = "I1m1";
        else if (SgName == "Am" && Alpha == 90.0 && Beta == 90.0) SgName = "A11m";
        else if (SgName == "Bm" && Alpha == 90.0 && Beta == 90.0) SgName = "B11m";
        else if (SgName == "Im" && Alpha == 90.0 && Beta == 90.0) SgName = "I11m";
        else if (SgName == "Bm" && Beta == 90.0 && Gamma == 90.0) SgName = "Bm11";
        else if (SgName == "Cm" && Beta == 90.0 && Gamma == 90.0) SgName = "Cm11";
        else if (SgName == "Im" && Beta == 90.0 && Gamma == 90.0) SgName = "Im11";
        else if (SgName == "Cc" && Alpha == 90.0 && Gamma == 90.0) SgName = "C1c1";
        else if (SgName == "An" && Alpha == 90.0 && Gamma == 90.0) SgName = "A1n1";
        else if (SgName == "Ia" && Alpha == 90.0 && Gamma == 90.0) SgName = "I1a1";
        else if (SgName == "Aa" && Alpha == 90.0 && Gamma == 90.0) SgName = "A1a1";
        else if (SgName == "Cn" && Alpha == 90.0 && Gamma == 90.0) SgName = "C1n1";
        else if (SgName == "Ic" && Alpha == 90.0 && Gamma == 90.0) SgName = "I1c1";
        else if (SgName == "Aa" && Alpha == 90.0 && Beta == 90.0) SgName = "A11a";
        else if (SgName == "Bn" && Alpha == 90.0 && Beta == 90.0) SgName = "B11n";
        else if (SgName == "Ib" && Alpha == 90.0 && Beta == 90.0) SgName = "I11b";
        else if (SgName == "Bb" && Alpha == 90.0 && Beta == 90.0) SgName = "B11b";
        else if (SgName == "An" && Alpha == 90.0 && Beta == 90.0) SgName = "A11n";
        else if (SgName == "Ia" && Alpha == 90.0 && Beta == 90.0) SgName = "I11a";
        else if (SgName == "Bb" && Beta == 90.0 && Gamma == 90.0) SgName = "Bb11";
        else if (SgName == "Cn" && Beta == 90.0 && Gamma == 90.0) SgName = "Cn11";
        else if (SgName == "Ic" && Beta == 90.0 && Gamma == 90.0) SgName = "Ic11";
        else if (SgName == "Cc" && Beta == 90.0 && Gamma == 90.0) SgName = "Cc11";
        else if (SgName == "Bn" && Beta == 90.0 && Gamma == 90.0) SgName = "Bn11";
        else if (SgName == "Ib" && Beta == 90.0 && Gamma == 90.0) SgName = "Ib11";
        else if (SgName == "P2/m" && Alpha == 90.0 && Gamma == 90.0) SgName = "P12/m1";
        else if (SgName == "P2/m" && Alpha == 90.0 && Beta == 90.0) SgName = "P112/m";
        else if (SgName == "P2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/m11";
        else if (SgName == "P2sub/m" && Alpha == 90.0 && Gamma == 90.0) SgName = "P12sub1/m1";
        else if (SgName == "P2sub/m" && Alpha == 90.0 && Beta == 90.0) SgName = "P112sub1/m";
        else if (SgName == "P2sub/m" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/m11";
        else if (SgName == "C2/m" && Alpha == 90.0 && Gamma == 90.0) SgName = "C12/m1";
        else if (SgName == "A2/m" && Alpha == 90.0 && Gamma == 90.0) SgName = "A12/m1";
        else if (SgName == "I2/m" && Alpha == 90.0 && Gamma == 90.0) SgName = "I12/m1";
        else if (SgName == "A2/m" && Alpha == 90.0 && Beta == 90.0) SgName = "A112/m";
        else if (SgName == "B2/m" && Alpha == 90.0 && Beta == 90.0) SgName = "B112/m";
        else if (SgName == "I2/m" && Alpha == 90.0 && Beta == 90.0) SgName = "I112/m";
        else if (SgName == "B2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "B2/m11";
        else if (SgName == "C2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "C2/m11";
        else if (SgName == "I2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "I2/m11";
        else if (SgName == "P2/c" && Alpha == 90.0 && Gamma == 90.0) SgName = "P12/c1";
        else if (SgName == "P2/n" && Alpha == 90.0 && Gamma == 90.0) SgName = "P12/n1";
        else if (SgName == "P2/a" && Alpha == 90.0 && Gamma == 90.0) SgName = "P12/a1";
        else if (SgName == "P2/a" && Alpha == 90.0 && Beta == 90.0) SgName = "P112/a";
        else if (SgName == "P2/n" && Alpha == 90.0 && Beta == 90.0) SgName = "P112/n";
        else if (SgName == "P2/b" && Alpha == 90.0 && Beta == 90.0) SgName = "P112/b";
        else if (SgName == "P2/b" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/b11";
        else if (SgName == "P2/n" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/n11";
        else if (SgName == "P2/c" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/c11";
        else if (SgName == "P2sub1/c" && Alpha == 90.0 && Gamma == 90.0) SgName = "P12sub1/c1";
        else if (SgName == "P2sub1/n" && Alpha == 90.0 && Gamma == 90.0) SgName = "P12sub1/n1";
        else if (SgName == "P2sub1/a" && Alpha == 90.0 && Gamma == 90.0) SgName = "P12sub1/a1";
        else if (SgName == "P2sub1/a" && Alpha == 90.0 && Beta == 90.0) SgName = "P112sub1/a";
        else if (SgName == "P2sub1/n" && Alpha == 90.0 && Beta == 90.0) SgName = "P112sub1/n";
        else if (SgName == "P2sub1/b" && Alpha == 90.0 && Beta == 90.0) SgName = "P112sub1/b";
        else if (SgName == "P2sub1/b" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/b11";
        else if (SgName == "P2sub1/n" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/n11";
        else if (SgName == "P2sub1/c" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/c11";
        else if (SgName == "C2/c" && Alpha == 90.0 && Gamma == 90.0) SgName = "C12/c1";
        else if (SgName == "A2/n" && Alpha == 90.0 && Gamma == 90.0) SgName = "A12/n1";
        else if (SgName == "I2/a" && Alpha == 90.0 && Gamma == 90.0) SgName = "I12/a1";
        else if (SgName == "A2/a" && Alpha == 90.0 && Gamma == 90.0) SgName = "A12/a1";
        else if (SgName == "C2/n" && Alpha == 90.0 && Gamma == 90.0) SgName = "C12/n1";
        else if (SgName == "I2/c" && Alpha == 90.0 && Gamma == 90.0) SgName = "I12/c1";
        else if (SgName == "A2/a" && Alpha == 90.0 && Beta == 90.0) SgName = "A112/a";
        else if (SgName == "B2/n" && Alpha == 90.0 && Beta == 90.0) SgName = "B112/n";
        else if (SgName == "I2/b" && Alpha == 90.0 && Beta == 90.0) SgName = "I112/b";
        else if (SgName == "B2/b" && Alpha == 90.0 && Beta == 90.0) SgName = "B112/b";
        else if (SgName == "A2/n" && Alpha == 90.0 && Beta == 90.0) SgName = "A112/n";
        else if (SgName == "I2/a" && Alpha == 90.0 && Beta == 90.0) SgName = "I112/a";
        else if (SgName == "B2/b" && Beta == 90.0 && Gamma == 90.0) SgName = "B2/b11";
        else if (SgName == "C2/n" && Beta == 90.0 && Gamma == 90.0) SgName = "C2/n11";
        else if (SgName == "I2/c" && Beta == 90.0 && Gamma == 90.0) SgName = "I2/c11";
        else if (SgName == "C2/c" && Beta == 90.0 && Gamma == 90.0) SgName = "C2/c11";
        else if (SgName == "B2/n" && Beta == 90.0 && Gamma == 90.0) SgName = "B2/n11";
        else if (SgName == "I2/b" && Beta == 90.0 && Gamma == 90.0) SgName = "I2/b11";

        else if (SgName == "C2sub1" && Alpha == 90.0 && Gamma == 90.0) { SgName = "P12sub11"; extra = "C"; }
        else if (SgName == "C2sub1" && Alpha == 90.0 && Beta == 90.0) { SgName = "P112sub1"; extra = "C"; }
        else if (SgName == "C2sub1" && Beta == 90.0 && Gamma == 90.0) { SgName = "P2sub111"; extra = "C"; }
        else if (SgName == "C2/a" && Alpha == 90.0 && Gamma == 90.0) { SgName = "P12/a1"; extra = "C"; }
        else if (SgName == "C2/a" && Alpha == 90.0 && Beta == 90.0) { SgName = "P112/a"; extra = "C"; }
        else if (SgName == "C2sub1/a" && Alpha == 90.0 && Gamma == 90.0) { SgName = "P12sub1/a1"; extra = "C"; }
        else if (SgName == "C2sub1/a" && Alpha == 90.0 && Beta == 90.0) { SgName = "P112sub1/a"; extra = "C"; }

        else if (SgName == "F2/m" && Alpha == 90.0 && Gamma == 90.0) { SgName = "P12/m1"; extra = "F"; }
        else if (SgName == "F2/m" && Beta == 90.0 && Gamma == 90.0) { SgName = "P2/m11"; extra = "F"; }
        else if (SgName == "F2/m" && Alpha == 90.0 && Beta == 90.0) { SgName = "P112/m"; extra = "F"; }

        else if (SgName == "B2sub1" && Alpha == 90.0 && Gamma == 90.0) { SgName = "P12sub11"; extra = "B"; }
        else if (SgName == "B2sub1" && Alpha == 90.0 && Beta == 90.0) { SgName = "P112sub1"; extra = "B"; }
        else if (SgName == "B2sub1" && Beta == 90.0 && Gamma == 90.0) { SgName = "P2sub111"; extra = "B"; }
        else if (SgName == "B2sub1/m" && Alpha == 90.0 && Gamma == 90.0) { SgName = "P12sub1/m1"; extra = "B"; }
        else if (SgName == "B2sub1/m" && Alpha == 90.0 && Beta == 90.0) { SgName = "P112sub1/m"; extra = "B"; }
        else if (SgName == "B2sub1/m" && Beta == 90.0 && Gamma == 90.0) { SgName = "P2sub1/m11"; extra = "B"; }
        
        else if (SgName == "Imab" && Beta == 90.0 && Gamma == 90.0) { SgName = "Pmab"; extra = "I"; }

        else if (SgName == "F4/mmm") { SgName = "P4/mmm"; extra = "F"; }

        #endregion amc�t�@�C���̓ǂݍ���

        //��������܂�ł��āA���A�����������Ȃ���ԌQ��I������ (C1��C121�Ȃǂ��������邽��)
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
            return (null, null);
        //Rhombohedoral�̂Ƃ��̏��u
        if (A == B && B == C && Alpha == Beta && Beta == Gamma && SymmetryStatic.Symmetries[symmetrySeriesNumber].SpaceGroupHMStr.Contains("Hex", StringComparison.CurrentCulture))
            symmetrySeriesNumber++;

        //Asterisk�̎�(2nd setting)�̏���
        if (isAsterisk && sg[symmetrySeriesNumber].EndsWith("(1)", Ord))
            symmetrySeriesNumber++;


        if (symmetrySeriesNumber >= 0)
        {
            var r = new Random();
            return (new Crystal2
            {

                CellTexts = [s[0], s[1], s[2], s[3], s[4], s[5]],
                sym = (short)symmetrySeriesNumber,
                argb = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)).ToArgb()
            },extra);
        }
        else
            return (null, null);
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

    #region CIF�t�@�C���̓ǂݍ���
    static readonly Random r = new();

    static readonly string[] ignoreWords1 = ["_shelx_hkl_", "_shelx_fab_", "_shelx_res_"];
    static readonly string[] ignoreWords2 = ["_refln", "_geom", "_platon"];
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

            stringList = [.. strTemp.Split('\n', true)];
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
    /// CIF�t�@�C����ǂݍ���
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static Crystal2 ConvertFromCIF(List<string> str)
    {
        //�܂� ;��; �ň͂܂�Ă��镡���ɂ킽��s����s�ɂ���
        //var str = new List<string>();
        var note = "";
        if (str[0].StartsWith("data", Ord))
            note = str[0];
        for (int n = 0; n < str.Count; n++)
        {
            while ((str[n].StartsWith("#", Ord) || str[n].Trim().Length == 0) && n < str.Count - 1)
                str.RemoveAt(n);
            //�S�Ă̐擪�s�̋󔒂��邢�̓^�u���폜����
            str[n] = str[n].Replace("\t", " ");
            str[n] = str[n].TrimEnd(' ').TrimStart(' ');

            if (str[n].Trim().StartsWith(";", Ord))//;�Ŏn�܂�s����������
            {
                int m = n + 1;

                var temp = new StringBuilder();
                //����;���o�Ă���Ƃ���܂ł����߂Ă܂Ƃ߂Ĉ�s�ɂ���
                //�A���A���̍s���uloop_�v�̏ꍇ��u_�v�Ŏn�܂�ꍇ�́A;���ꕶ�����������ďI��
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

        if (str[^1].StartsWith("#"))
            str[^1] = "#End of data";


        //����'���邢��"�ň͂܂�Ă��镶���񒆂̋󔒂����R�o�Ă��Ȃ��悤�ȕ�����ɕϊ�����
        for (int n = 0; n < str.Count; n++)
        {
            if (str[n].Contains("''"))//''�Ƃ��������񂪊܂܂�Ă�����
            {
                var temp = str[n].Remove(0, str[n].IndexOf("'"));
                temp = temp.Replace("''", "�K");
                str[n] = str[n].Remove(str[n].IndexOf("'")) + temp;
            }

            if (str[n].Contains('\''))//'���܂܂�Ă�����
            {
                while (str[n].Contains('\''))
                {
                    var firstIndex = str[n].IndexOf('\'');
                    var next = str[n].IndexOf('\'', firstIndex + 1);
                    if (next == -1)
                        break;
                    var substring = str[n].Substring(firstIndex, next - firstIndex + 1);
                    substring = substring.Replace(" ", "�K");
                    substring = substring.Replace("'", "");

                    str[n] = $"{str[n][..firstIndex]}{substring}{str[n][(next + 1)..]}";
                }
            }

            if (str[n].Contains('"'))//\"���܂܂�Ă�����
            {
                while (str[n].Contains('"'))
                {
                    var firstIndex = str[n].IndexOf('"');
                    var next = str[n].IndexOf('"', firstIndex + 1);
                    if (next == -1)
                        break;
                    var substring = str[n].Substring(firstIndex, next - firstIndex + 1);
                    substring = substring.Replace(" ", "�K");
                    substring = substring.Replace("\"", "");
                    str[n] = $"{str[n][..firstIndex]}{substring}{str[n][(next + 1)..]}";
                }
            }
        }

        //����loop_�ɑ����s���@"label  data"���������ɑΉ�
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
            {//�P�̃A�C�e���̂Ƃ�
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
                CIF.Add(new List<(string Label, string Data)>(new[] { (tempLabel, tempData.Replace("�K", " ")) }));
            }
            else if (str[n].Trim().StartsWith("loop_", Ord))
            {//���[�v�̂Ƃ�
                var tempLoopLabels = new List<string>();
                var tempLoopDatas = new List<string>();
                n++;
                //"_"�Ŏn�܂郉�x���𐔂���
                while (n < str.Count && str[n].Trim().StartsWith("_", Ord))
                    tempLoopLabels.Add(str[n++].Trim());

                //����"_"��"loop_"��"#End of"�Ŏn�܂�s���o�Ă���܂Ń��[�v
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
                            cif_Group.Add((tempLoopLabels[j], tempLoopDatas[i * tempLoopLabels.Count + j].Replace("�K", " ")));
                        CIF.Add(cif_Group);
                    }
                }
            }
        }
        //�����܂ł�CIF_Group�N���X�̃��X�g������

        //�i�q�萔�́ACIF�t�@�C�����ɉ�����L�ڂ���Ă���ꍇ�����邽�߁A���X�g�ɂ���B
        List<(int index, string value)> aList = [], bList = [], cList = [], alphaList = [], betaList = [], gammaList = [];

        string name = "", sectionTitle = "", journalNameFull = "", journalCodenASTM = "";
        string volume = "", year = "", pageFirst = "", pageLast = "", issue = "";
        var journal = new StringBuilder();
        List<string> spaceGroupNameHM = [], spaceGroupNameHall = [];
        string chemical_formula_sum = "", chemical_formula_structural = "";
        var symmetry_Int_Tables_number = -1;
        var author = new List<string>();
        var operations = new List<string>();

        for (int i = 0; i < CIF.Count; i++)
            for (int j = 0; j < CIF[i].Count; j++)
            {
                var label = CIF[i][j].Label;
                var data = CIF[i][j].Data;

                if (label.StartsWith("_chemical_name", Ord)) name += data + " ";

                //��������i�q�萔
                if (label == "_cell_length_a") aList.Add((i, data));
                else if (label == "_cell_length_b") bList.Add((i, data));
                else if (label == "_cell_length_c") cList.Add((i, data));
                else if (label == "_cell_angle_alpha") alphaList.Add((i, data));
                else if (label == "_cell_angle_beta") betaList.Add((i, data));
                else if (label == "_cell_angle_gamma") gammaList.Add((i, data));

                //��������W���[�i�����
                else if (label == "_publ_author_name") author.Add(data);
                else if (label == "_publ_section_title") sectionTitle = data;
                else if (label == "_journal_name_full") journalNameFull = data;
                else if (label == "_journal_coden_ASTM") journalCodenASTM = data;
                else if (label == "_journal_year") year = data;
                else if (label == "_journal_volume") volume = data;
                else if (label == "_journal_page_first") pageFirst = data;
                else if (label == "_journal_page_last") pageLast = data;
                else if (label == "_journal_issue") issue = data;
                //��������Ώ̐�
                else if (label.Contains("_space_group_name_H-M")) spaceGroupNameHM.Add(data);
                else if (label.Contains("_space_group_name_Hall")) spaceGroupNameHall.Add(data);
                else if (label == "_Int_Tables_number") int.TryParse(data, out symmetry_Int_Tables_number);
                else if (label == "_chemical_formula_sum") chemical_formula_sum = data;
                else if (label == "_chemical_formula_structural") chemical_formula_structural = data;
                else if (label == "_space_group_symop_operation_xyz") operations.Add(data);
                else if (label == "_symmetry_equiv_pos_as_xyz") operations.Add(data);
            }

        if (aList.Count == 0 || bList.Count == 0 || cList.Count == 0 || alphaList.Count == 0 || betaList.Count == 0 || gammaList.Count == 0) return null;

        if (name.Length == 0 || name == "?" || name == "? ?" || name.Trim().Length == 0)
            name = chemical_formula_sum;

        //"_atom_site_label"�Ƃ������x�����܂�CIF�ԍ������X�g����B (�ŏ��̘A�������ԍ��݂̂��g��)
        //����ɁA�i�q�萔List�̒��ŁA�ǂꂪ�������l�Ȃ̂��𔻕ʂ���
        string a = "", b = "", c = "", alpha = "", beta = "", gamma = "";
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
                else if (atomCIF.Count != 0)
                {
                    flag = false;
                    a = aList.Count == 1 ? aList[0].value : aList.Last(e => e.index < i).value;
                    b = bList.Count == 1 ? bList[0].value : bList.Last(e => e.index < i).value;
                    c = cList.Count == 1 ? cList[0].value : cList.Last(e => e.index < i).value;
                    alpha = alphaList.Count == 1 ? alphaList[0].value : alphaList.Last(e => e.index < i).value;
                    beta = betaList.Count == 1 ? betaList[0].value : betaList.Last(e => e.index < i).value;
                    gamma = gammaList.Count == 1 ? gammaList[0].value : gammaList.Last(e => e.index < i).value;
                }
            }
        if (a == "") a = aList[0].value;
        if (b == "") b = bList[0].value;
        if (c == "") c = cList[0].value;
        if (alpha == "") alpha = alphaList[0].value;
        if (beta == "") beta = betaList[0].value;
        if (gamma == "") gamma = gammaList[0].value;

        #region ��ԌQ�𒲂ׂ镔��
        //��ԌQ������
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

        #region �Ώۑ��삪CIF�t�@�C�����ɋL�ڂ���Ă���ꍇ�́A�{���Ɍ��݂̋�ԌQ�ł悢���ǂ������`�F�b�N

        var shift = new V3(0, 0, 0);
        var p = new V3(0.111, 0.234, 0.457);//�K���Ȉ�ʈʒu
        var tempAtom = WyckoffPosition.GetEquivalentAtomsPosition((p.X, p.Y, p.Z), sgnum).Atom;

        if (operations.Count != 0 && operations.Count == tempAtom.Length)
        {
            var th = 0.0000001;
            var prms = new[] { "x", "y", "z" }.Select(s => Expression.Parameter(typeof(double), s)).ToArray();
            //�����񂩂烉���_����Ԃ����[�J���֐�
            Func<double, double, double, V3> func(string sExpr)
            {
                try
                {
                    sExpr = sExpr.Replace(" ", "").Replace(",+", ",").TrimStart(['+']);
                    sExpr = "new [] {" + sExpr.Replace("/", ".0/").Replace(".0.0", ".0") + "}";//���q�ɏ����_��������

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

        var atoms = new List<Atoms2>();
        foreach (var cif in atomCIF)
        {
            //���q�̏��
            string atomLabel = "", atomSymbol = "", thermalDisplaceType = "";
            string x = "", y = "", z = "", occ = "1";
            string uIso = "", u11 = "", u22 = "", u33 = "", u12 = "", u13 = "", u23 = "";
            string bIso = "", b11 = "", b22 = "", b33 = "", b12 = "", b13 = "", b23 = "";

            //�܂���{�I�Ȍ��q�ʒu���L���Ȃǂ̏���T��
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

            //���Ɉٕ����̉��x�U�����q�������� (�������̉��x���q�͊��ɏ�̃��[�v�œǂݍ��܂�Ă���)

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
            //���x�������猳�f��T��
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

            //B�^�C�v���S�� �h�h ��������AU�^�C�v�Ɣ���
            var isU = bIso.Length == 0 && b11.Length == 0 && b12.Length == 0 && b13.Length == 0 && b22.Length == 0 && b23.Length == 0 && b33.Length == 0;
            //�񓙕������S�� �h�h ��������A�������Ɣ��f
            var isIso = isU ?
                u11.Length == 0 && u12.Length == 0 && u13.Length == 0 && u22.Length == 0 && u23.Length == 0 && u33.Length == 0 :
                b11.Length == 0 && b12.Length == 0 && b13.Length == 0 && b22.Length == 0 && b23.Length == 0 && b33.Length == 0;

            var iso = isU ? uIso : bIso;

            if (iso.Length == 0)
                iso = "0";

            string[] aniso = isU ? //11, 22, 33, 12, 23, 31�̏���
                [u11, u22, u33, u12, u23, u13] :
                [b11, b22, b33, b12, b23, b13];

            if (atomicNumber > 0)
                atoms.Add(new Atoms2(atomLabel, atomicNumber, 0, 0, [x, y, z], occ, isIso, isU, iso, aniso));
            else if (atomicNumber == -1)//"OH"�̂Ƃ��̑Ώ�
            {
                atoms.Add(new Atoms2(atomLabel, 1, 0, 0, [x, y, z], occ, isIso, isU, iso, aniso));
                atoms.Add(new Atoms2(atomLabel, 8, 0, 0, [x, y, z], occ, isIso, isU, iso, aniso));
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
            CellTexts = [a, b, c, alpha, beta, gamma],
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

        SgNameHM = SgNameHM.TrimStart(' ').TrimEnd(' ');


        if (!SgNameHM.Contains(' ') && SgNameHM.Contains('_'))
            for (int i = 1; i < SgNameHM.Length - 1; i++)
                if (SgNameHM[i] == '_' && '0' < SgNameHM[i - 1] && '9' > SgNameHM[i - 1] && '0' < SgNameHM[i + 1] && '9' > SgNameHM[i + 1])
                {
                    SgNameHM = SgNameHM.Remove(i, 1);
                    SgNameHM = SgNameHM.Insert(i, "sub");
                    i += 3;
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
        if (SgNameHM.EndsWith("Z", Ord))//�Ō��Z�����Ă�����OriginChoice2
        {
            IsOrigineChoice2 = true;
            SgNameHM = SgNameHM.TrimEnd('Z').TrimEnd();
        }
        if (SgNameHM.EndsWith("S", Ord))//�ŌオS��������OriginChoice1
            SgNameHM = SgNameHM.TrimEnd('S').TrimEnd();

        if (SgNameHM.EndsWith("S1", Ord))//�ŌオS1��������OriginChoice1
            SgNameHM = SgNameHM.Replace("S1", "").TrimEnd();
        if (SgNameHM.EndsWith("Z1", Ord))//�ŌオZ1��������OriginChoice1
            SgNameHM = SgNameHM.Replace("Z1", "").TrimEnd();
        if (SgNameHM.EndsWith(":S1", Ord))//�Ōオ:S1��������OriginChoice1
            SgNameHM = SgNameHM.Replace(":S1", "").TrimEnd();
        if (SgNameHM.EndsWith(":1", Ord))//�Ōオ:1��������OriginChoice1
            SgNameHM = SgNameHM.Replace(":1", "").TrimEnd();

        if (SgNameHM.EndsWith("S2", Ord))//�ŌオS2��������OriginChoice2
        {
            SgNameHM = SgNameHM.Replace("S2", "").TrimEnd();
            IsOrigineChoice2 = true;
        }
        if (SgNameHM.EndsWith("Z2", Ord))//�ŌオZ2��������OriginChoice2
        {
            SgNameHM = SgNameHM.Replace("Z2", "").TrimEnd();
            IsOrigineChoice2 = true;
        }
        if (SgNameHM.EndsWith(":S2", Ord))//�Ōオ:S2��������OriginChoice2
        {
            SgNameHM = SgNameHM.Replace(":S2", "").TrimEnd();
            IsOrigineChoice2 = true;
        }
        if (SgNameHM.EndsWith(":2", Ord))//�Ōオ:S2��������OriginChoice2
        {
            SgNameHM = SgNameHM.Replace(":2", "").TrimEnd();
            IsOrigineChoice2 = true;
        }
        if (SgNameHM.EndsWith("O2", Ord))//�Ōオ:S2��������OriginChoice2
        {
            SgNameHM = SgNameHM.Replace("O2", "").TrimEnd();
            IsOrigineChoice2 = true;
        }

        SgNameHM = SgNameHM.Replace("~", "");

        //�ꕶ���ڈȍ~�̉p���͑S�ď�������
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

        //��������܂�ł��āA���A�����������Ȃ���ԌQ��I������ (C1��C121�Ȃǂ��������邽��)

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

        //Rhombohedoral�̂Ƃ��̏��u
        if (isRhomboShape && SymmetryStatic.Symmetries[symmetrySeriesNumber].SpaceGroupHMStr.Contains("Hex", StringComparison.Ordinal))
            symmetrySeriesNumber++;

        //originChoice��2�̂Ƃ��̑Ώ�
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

    #region CIF�t�@�C���ւ̕ϊ�

    public static string ConvertToCIF(Crystal crystal)
    {
        var sb = new StringBuilder();
        sb.AppendLine("# This file is exported from \"" + Process.GetCurrentProcess().ProcessName + "\"");
        sb.AppendLine("# https://github.com/seto77/");

        sb.AppendLine("data_global");
        sb.AppendLine("_chemical_name '" + crystal.Name + "'");

        sb.AppendLine("loop_");
        sb.AppendLine("_publ_author_name");
        foreach (string str in crystal.PublAuthorName.Split(',', true))
            sb.AppendLine("'" + str.Trim() + "'");

        sb.AppendLine("_journal_name '" + crystal.Journal + "'");

        #region �_���^�C�g��
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

        #region �i�q�萔�A�Ώ̐�
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
        #endregion

        #region ���q�̓����ʒu
        sb.AppendLine("loop_");
        sb.AppendLine("_symmetry_equiv_pos_as_xyz");
        bool[][] flag = Array.Empty<bool[]>();
        if (sym.LatticeTypeStr == "P") flag = [[false, false, false]];
        else if (sym.LatticeTypeStr == "A") flag = [[false, false, false], [false, true, true]];
        else if (sym.LatticeTypeStr == "B") flag = [[false, false, false], [true, false, true]];
        else if (sym.LatticeTypeStr == "C") flag = [[false, false, false], [true, true, false]];
        else if (sym.LatticeTypeStr == "I") flag = [[false, false, false], [true, true, true]];
        else if (sym.LatticeTypeStr == "F") flag = [[false, false, false], [false, true, true], [true, false, true], [true, true, false]];

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
            else//R�i�q��Hexa�Z�b�e�B���O�̂Ƃ�
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

        #region �e���q�̏��
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
            var u = double.IsNaN(a.Dsf.Uiso) ? 0 : a.Dsf.Uiso * 100;
            sb.AppendLine($"{a.Label} {AtomStatic.AtomicName(a.AtomicNumber)} {a.X:f6} {a.Y:f6} {a.Z:f6} {a.Occ:f6} {u:f6}");
        }


        if (crystal.Atoms.Any(a => !a.Dsf.UseIso))
        {
            sb.AppendLine("loop_");
            sb.AppendLine("_atom_site_aniso_label");
            sb.AppendLine("_atom_site_aniso_U_11");
            sb.AppendLine("_atom_site_aniso_U_22");
            sb.AppendLine("_atom_site_aniso_U_33");
            sb.AppendLine("_atom_site_aniso_U_23");
            sb.AppendLine("_atom_site_aniso_U_13");
            sb.AppendLine("_atom_site_aniso_U_12");
            foreach (var a in crystal.Atoms.Where(e => !e.Dsf.UseIso))
            {
                var u11 = double.IsNaN(a.Dsf.U11) ? 0 : a.Dsf.U11 * 100;
                var u22 = double.IsNaN(a.Dsf.U22) ? 0 : a.Dsf.U22 * 100;
                var u33 = double.IsNaN(a.Dsf.U33) ? 0 : a.Dsf.U33 * 100;
                var u23 = double.IsNaN(a.Dsf.U23) ? 0 : a.Dsf.U23 * 100;
                var u31 = double.IsNaN(a.Dsf.U31) ? 0 : a.Dsf.U31 * 100;
                var u12 = double.IsNaN(a.Dsf.U12) ? 0 : a.Dsf.U12 * 100;
                sb.AppendLine($"{a.Label} {u11:f6} {u22:f6} {u33:f6} {u23:f6} {u31:f6} {u12:f6}");
            }
        }
        #endregion

        return sb.ToString();
    }
    #endregion

    #region OpenGL�̂��߂�Bond�ݒ�
    public static void SetOpenGL_property(Crystal c)
    {
        foreach (Atoms a in c.Atoms)
            a.ResetVesta();
        c.Bonds = Bonds.GetVestaBonds(c.Atoms.Select(a => a.ElementName).Distinct());

        #region ��������
        /*
        //�悸���q�̐F�A���a��ݒ�
        foreach (Atoms a in c.Atoms)
        {
            switch (a.ElementName)
            {
#region �C�I�����a�A�F��ݒ�
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
#endregion �C�I�����a�A�F��ݒ�
            }
            if (!elementList.Contains(a.ElementName))
                elementList.Add(a.ElementName);
        }
        */
        #endregion
    }
    #endregion
}

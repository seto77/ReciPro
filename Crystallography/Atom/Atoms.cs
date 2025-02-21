using System;
using System.Numerics;
using System.Xml.Serialization;

namespace Crystallography;

[Serializable()]
public class Atoms : System.IEquatable<Atoms>, ICloneable
{
    #region 基本メソッド
    public object Clone()
    {
        Atoms atoms = (Atoms)this.MemberwiseClone();
        for (int i = 0; i < Atom.Length; i++)
            atoms.Atom[i] = (Vector3D)Atom[i].Clone();
        return atoms;
    }

    public override string ToString()
    {
        return $"{Label}\t{ElementName}\t{GetStringFromDouble(X)}\t{GetStringFromDouble(Y)}\t{GetStringFromDouble(Z)}\t{GetStringFromDouble(Occ)}\t{Multiplicity}\t{WyckoffLeter}\t{SiteSymmetry}";
    }

    public bool Equals(Atoms obj)
    {
        Atoms atoms = obj;
        return atoms.Label == Label && atoms.X == X && atoms.Y == Y && atoms.Z == Z && atoms.Occ == Occ;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Atoms);
    }

    public override int GetHashCode()
    {
        return new { X, Y, Z, Occ, Label, WyckoffNumber, SymmetrySeriesNumber, Dsf, Atom, Texture }.GetHashCode();
    }
    #endregion

    #region フィールド、プロパティ

    public int ID;

    [XmlIgnore]
    public Vector3D[] Atom = [];

    public double X, Y, Z;
    public double X_err, Y_err, Z_err;


    public double Occ, Occ_err;
    public int AtomicNumber;

    public int SubNumberXray = 0;
    public int SubNumberElectron = 0;
    public double[] Isotope = null;

    public string Label;

    public string ElementName;

    [XmlIgnore]
    public string WyckoffLeter, SiteSymmetry;

    [XmlIgnore]
    public int WyckoffNumber;

    [XmlIgnore]
    public int Multiplicity;

    public DiffuseScatteringFactor Dsf;

    [XmlIgnore]
    public int SymmetrySeriesNumber;

    //public string str;

    public int Argb;

    public float Radius = 0.6f;

    public float Ambient = Material.DefaultTexture.Ambient;//環境光
    public float Diffusion = Material.DefaultTexture.Diffuse;//拡散光
    public float Emission = Material.DefaultTexture.Emission;//自己証明
    public float Shininess = Material.DefaultTexture.SpecularPow;//反射光の強度
    public float Specular = Material.DefaultTexture.Specular;//反射光

    [XmlIgnore]
    public Vector3DBase PositionError => new(X_err, Y_err, Z_err);
    [XmlIgnore]
    public Vector3DBase Position => new(X, Y, Z);

    [XmlIgnore]
    public (float Ambient, float Diffusion, float Specular, float Shininess, float Emission) Texture
    {
        get => (Ambient, Diffusion, Specular, Shininess, Emission);
        set
        {
            Ambient = value.Ambient;
            Diffusion = value.Diffusion;
            Specular = value.Specular;
            Shininess = value.Shininess;
            Emission = value.Emission;
        }
    }

    public Material Material => new(Argb, (Ambient, Diffusion, Specular, Shininess, Emission));

    /// <summary>
    /// OpenGL描画時に、ラベルを表示するか
    /// </summary>
    public bool ShowLabel = false;

    /// <summary>
    /// OpenGLの描画時に有効にするかどうか
    /// </summary>
    public bool GLEnabled = true;
    #endregion

    #region コンストラクタ

    public Atoms()
    {
        //Atom = new List<Vector3D>();
    }


    /// <summary>
    /// ワイコフポジションだけ指定して、原子位置は実態のないコンストラクタ
    /// </summary>
    /// <param name="wyk"></param>
    public Atoms(WyckoffPosition wyk, string label, int atomicNumber, int subXray, int subElectron, double[] isotope,
         double occ, DiffuseScatteringFactor dsf)
    {
        SymmetrySeriesNumber = wyk.SymmetrySeriesNumber;

        Label = label;

        this.Occ = 1;

        WyckoffLeter = wyk.WyckoffLetter;
        SiteSymmetry = wyk.SiteSymmetry;
        Multiplicity = wyk.Multiplicity;
        WyckoffNumber = wyk.WyckoffNumber;

        Atom = new Vector3D[Multiplicity];
        for (int i = 0; i < Multiplicity; i++)
            Atom[i] = new Vector3D(0, 0, 0);

        Dsf = dsf;

        //Asf = new AtomicScatteringFactor(atomicNumber, subXray, subElectron);
        SubNumberXray = subXray;
        SubNumberElectron = subElectron;
        AtomicNumber = atomicNumber;
        Isotope = isotope;
        ElementName = AtomicNumber.ToString() + ": " + AtomStatic.AtomicName(atomicNumber);

        //NaturalIsotopicAbundance = true;
        Isotope = isotope;
    }

    /// <summary>
    /// 基本コンストラクタ
    /// </summary>
    /// <param name="label"></param>
    /// <param name="atomicNumber"></param>
    /// <param name="subXray"></param>
    /// <param name="subElectron"></param>
    /// <param name="isotope"></param>
    /// <param name="symmetrySeriesNumber"></param>
    /// <param name="pos"></param>
    /// <param name="occ"></param>
    /// <param name="dsf"></param>
    public Atoms(string label, int atomicNumber, int subXray, int subElectron, double[] isotope, int symmetrySeriesNumber,
        Vector3DBase pos, double occ, DiffuseScatteringFactor dsf)
    {
        SymmetrySeriesNumber = symmetrySeriesNumber;

        Label = label;

        Occ = occ;
        X = pos.X;
        Y = pos.Y;
        Z = pos.Z;

        var temp = WyckoffPosition.GetEquivalentAtomsPosition((X, Y, Z), symmetrySeriesNumber);
        WyckoffLeter = temp.WyckoffLeter;
        SiteSymmetry = temp.SiteSymmetry;
        Multiplicity = temp.Multiplicity;
        WyckoffNumber = temp.WyckoffNumber;

        Atom = temp.Atom;
        Dsf = dsf;

        SubNumberXray = subXray;
        SubNumberElectron = subElectron;
        Isotope = isotope ?? [];
        AtomicNumber = atomicNumber;
        ElementName = AtomicNumber.ToString() + ": " + AtomStatic.AtomicName(atomicNumber);
    }

    /// <summary>
    /// 基本コンストラクタ + エラー
    /// </summary>
    /// <param name="label"></param>
    /// <param name="atomicNumber"></param>
    /// <param name="subXray"></param>
    /// <param name="subElectron"></param>
    /// <param name="isotope"></param>
    /// <param name="symmetrySeriesNumber"></param>
    /// <param name="pos"></param>
    /// <param name="pos_err"></param>
    /// <param name="occ"></param>
    /// <param name="occ_err"></param>
    /// <param name="dsf"></param>
    public Atoms(string label, int atomicNumber, int subXray, int subElectron, double[] isotope, int symmetrySeriesNumber,
       Vector3DBase pos, Vector3DBase pos_err, double occ, double occ_err, DiffuseScatteringFactor dsf)
        : this(label, atomicNumber, subXray, subElectron, isotope, symmetrySeriesNumber, pos, occ, dsf)
    {

        X_err = pos_err.X;
        Y_err = pos_err.Y;
        Z_err = pos_err.Z;
        Occ_err = occ_err;
    }

    /// <summary>
    /// 基本コンストラクタ + エラー + Material
    /// </summary>
    /// <param name="label"></param>
    /// <param name="atomicNumber"></param>
    /// <param name="subXray"></param>
    /// <param name="subElectron"></param>
    /// <param name="isotope"></param>
    /// <param name="symmetrySeriesNumber"></param>
    /// <param name="pos"></param>
    /// <param name="pos_err"></param>
    /// <param name="occ"></param>
    /// <param name="occ_err"></param>
    /// <param name="dsf"></param>
    /// <param name="mat"></param>
    /// <param name="radius"></param>
    public Atoms(string label, int atomicNumber, int subXray, int subElectron, double[] isotope, int symmetrySeriesNumber,
       Vector3DBase pos, Vector3DBase pos_err, double occ, double occ_err,
        DiffuseScatteringFactor dsf, Material mat, float radius, bool glEnabled = true, bool showLabel = false)
        : this(label, atomicNumber, subXray, subElectron, isotope, symmetrySeriesNumber, pos, pos_err, occ, occ_err, dsf)
    {
        Radius = radius;
        if (mat != null)
        {
            Argb = mat.Argb;
            Texture = mat.Texture;
        }
        GLEnabled = glEnabled;
        ShowLabel = showLabel;

    }
    #endregion

    #region ResetSymmetry  対称性をリセットして、等価な原子位置や、ワイコフ位置などを再設定する。
    /// <summary>
    /// 対称性をリセットして、等価な原子位置や、ワイコフ位置などを再設定する。
    /// </summary>
    /// <param name="symmetrySeriesNumber"></param>
    public void ResetSymmetry(int symmetrySeriesNumber)
    {
        SymmetrySeriesNumber = symmetrySeriesNumber;

        var temp = WyckoffPosition.GetEquivalentAtomsPosition((X, Y, Z), symmetrySeriesNumber);
        WyckoffLeter = temp.WyckoffLeter;
        SiteSymmetry = temp.SiteSymmetry;
        Multiplicity = temp.Multiplicity;
        WyckoffNumber = temp.WyckoffNumber;
        ElementName = $"{AtomicNumber}: {AtomStatic.AtomicName(AtomicNumber)}";

        Atom = temp.Atom;
    }
    #endregion

    #region ResetVesta()  Vestaの色、材質にセットする。
    /// <summary>
    /// Vestaの色、材質にセットする。
    /// </summary>
    public void ResetVesta()
    {
        Texture = Material.DefaultTexture;
        (Radius, Argb) =AtomStatic.GetVesta(AtomicNumber);
    }
    #endregion

    #region 原子位置の乱数変化、微小変化      テストコード
    /// <summary>
    /// 多重度を保ったまま、原子位置を乱数的的に変化させる。ワイコフ位置も多重度をもとにきまる
    /// </summary>
    /// <param name="r"></param>
    public static void RandomizeKeepintMultiplicity(Random r)
    {
    }

    /// <summary>
    /// ワイコフ位置を保ったまま、原子位置を乱数的に変化させる
    /// </summary>
    /// <param name="seed"></param>
    public void RandomizeKeepingWykoff(Random r)
    {
        var wyk = SymmetryStatic.WyckoffPositions[SymmetrySeriesNumber][WyckoffNumber];
        if (wyk.Free.X) X = r.NextDouble();
        if (wyk.Free.Y) Y = r.NextDouble();
        if (wyk.Free.Z) Z = r.NextDouble();

        Atom = SymmetryStatic.WyckoffPositions[SymmetrySeriesNumber][WyckoffNumber].GeneratePositions(X, Y, Z);
        X = Atom[0].X;
        Y = Atom[0].Y;
        Z = Atom[0].Z;
    }

    /// <summary>
    /// ワイコフ位置を保ったまま、原子位置を現在の位置からわずかに変化させる
    /// </summary>
    /// <param name="threshold">動かす最大値 (相対位置) </param>
    public void ShakeKeepingWykoff(double threshold, Random r)
    {
        var wyk = SymmetryStatic.WyckoffPositions[SymmetrySeriesNumber][WyckoffNumber];
        if (wyk.Free.X) X += (r.NextDouble() - 1) * (r.NextDouble() - 1) * 4 * threshold;
        if (wyk.Free.Y) Y += (r.NextDouble() - 1) * (r.NextDouble() - 1) * 4 * threshold;
        if (wyk.Free.Z) Z += (r.NextDouble() - 1) * (r.NextDouble() - 1) * 4 * threshold;

        Atom = SymmetryStatic.WyckoffPositions[SymmetrySeriesNumber][WyckoffNumber].GeneratePositions(X, Y, Z);
        X = Atom[0].X;
        Y = Atom[0].Y;
        Z = Atom[0].Z;
    }
    #endregion

    #region 電子線、X線、中性子の、原子散乱因子の計算 
    /// <summary>
    /// 電子線の原子散乱因子を計算 s2の単位はnm^-2
    /// </summary>
    /// <param name="S2">S2: (sin(theta)/ramda)^2, unit is nm^-2</param>
    /// <returns></returns>
    public double GetAtomicScatteringFactorForElectron(double s2)
        => AtomStatic.ElectronScatteringPeng[AtomicNumber][SubNumberElectron].Factor(s2) * Occ;

    /// <summary>
    /// X線の原子散乱因子を計算 s2の単位はnm^-2
    /// </summary>
    /// <param name="s2"> unit is nm^-2</param>
    /// <returns></returns>
    public double GetAtomicScatteringFactorForXray(double s2)
        => AtomStatic.XrayScatteringWK[AtomicNumber][SubNumberXray].Factor(s2) * Occ;

    public Complex GetAtomicScatteringFactorForNeutron()
    {
        if (Isotope != null && Isotope.Length == AtomStatic.IsotopeAbundance[AtomicNumber].Count)
        {
            var f = new Complex();
            for (int i = 0; i < AtomStatic.IsotopeAbundance[AtomicNumber].Count; i++)
                f += AtomStatic.NeutronCoherentScattering[AtomicNumber][i + 1] * Isotope[i] / 100.0;
            return f * Occ;
        }
        else
            return AtomStatic.NeutronCoherentScattering[AtomicNumber][0] * Occ;
    }
    #endregion

    #region 静的メソッド
    public static string GetStringFromDouble(double d)
    {
        if (Math.Abs(d - 0.125) < 0.000000001) return "1/8";
        else if (Math.Abs(d - 0.375) < 0.000000001) return "3/8";
        else if (Math.Abs(d - 0.625) < 0.000000001) return "5/8";
        else if (Math.Abs(d - 0.875) < 0.000000001) return "7/8";
        else if (Math.Abs(d - 0.25) < 0.000000001) return "1/4";
        else if (Math.Abs(d - 0.75) < 0.000000001) return "3/4";
        else if (Math.Abs(d - 0.5) < 0.000000001) return "1/2";
        else if (Math.Abs(d - 1.0 / 3.0) < 0.000000001) return "1/3";
        else if (Math.Abs(d - 2.0 / 3.0) < 0.000000001) return "2/3";
        else if (Math.Abs(d - 1.0 / 6.0) < 0.000000001) return "1/6";
        else if (Math.Abs(d - 5.0 / 6.0) < 0.000000001) return "5/6";
        else if (Math.Abs(d - 1.0 / 12.0) < 0.000000001) return "1/12";
        else if (Math.Abs(d - 5.0 / 12.0) < 0.000000001) return "5/12";
        else if (Math.Abs(d - 7.0 / 12.0) < 0.000000001) return "7/12";
        else if (Math.Abs(d - 11.0 / 12.0) < 0.000000001) return "11/12";
        else if (Math.Abs(d - 1.0 / 24.0) < 0.000000001) return "1/24";
        else if (Math.Abs(d - 5.0 / 24.0) < 0.000000001) return "5/24";
        else if (Math.Abs(d - 7.0 / 24.0) < 0.000000001) return "7/24";
        else if (Math.Abs(d - 11.0 / 24.0) < 0.000000001) return "11/24";
        else if (Math.Abs(d - 13.0 / 24.0) < 0.000000001) return "13/24";
        else if (Math.Abs(d - 17.0 / 24.0) < 0.000000001) return "17/24";
        else if (Math.Abs(d - 19.0 / 24.0) < 0.000000001) return "19/24";
        else if (Math.Abs(d - 23.0 / 24.0) < 0.000000001) return "23/24";
        else return d.ToString("g6");
    }

    public static double GetDoubleFromString(string s)
    {
        if (s == "1/8" || s == "1.0/8.0") return 1.0 / 8.0;
        else if (s == "3/8" || s == "3.0/8.0") return 3.0 / 8.0;
        else if (s == "5/8" || s == "5.0/8.0") return 5.0 / 8.0;
        else if (s == "7/8" || s == "7.0/8.0") return 7.0 / 8.0;
        else if (s == "1/4" || s == "1.0/4.0") return 1.0 / 4.0;
        else if (s == "3/4" || s == "3.0/4.0") return 3.0 / 4.0;
        else if (s == "1/2" || s == "1.0/2.0") return 1.0 / 2.0;
        else if (s == "1/3" || s == "1.0/3.0") return 1.0 / 3.0;
        else if (s == "2/3" || s == "2.0/3.0") return 2.0 / 3.0;
        else if (s == "1/6" || s == "1.0/6.0") return 1.0 / 6.0;
        else if (s == "5/6" || s == "5.0/6.0") return 5.0 / 6.0;
        else if (s == "1/12" || s == "1.0/12.0") return 1.0 / 12.0;
        else if (s == "5/12" || s == "5.0/12.0") return 5.0 / 12.0;
        else if (s == "7/12" || s == "7.0/12.0") return 7.0 / 12.0;
        else if (s == "11/12" || s == "11.0/12.0") return 11.0 / 12.0;
        else if (s == "1/12" || s == "1.0/12.0") return 1.0 / 12.0;
        else if (s == "5/12" || s == "5.0/12.0") return 5.0 / 12.0;
        else if (s == "7/12" || s == "7.0/12.0") return 7.0 / 12.0;
        else if (s == "11/12" || s == "11.0/12.0") return 11.0 / 12.0;
        else if (s == "13/12" || s == "13.0/12.0") return 13.0 / 12.0;
        else if (s == "17/12" || s == "17.0/12.0") return 17.0 / 12.0;
        else if (s == "19/12" || s == "19.0/12.0") return 19.0 / 12.0;
        else if (s == "23/12" || s == "23.0/12.0") return 23.0 / 12.0;
        else if (s == "0.3333") return 1.0 / 3.0;
        else if (s == "0.6667") return 2.0 / 3.0;
        else
            try { return Convert.ToDouble(s); }
            catch { System.Windows.Forms.MessageBox.Show("Please input a valid value"); return 0; }
    }
    #endregion
}

[Serializable()]
public class DiffuseScatteringFactor
{
    #region プロパティ、フィールド
    const double PI2 = Math.PI * Math.PI;
    public enum Type { U, B }
    //Biomolecular Crystallography: Principles, Practice, and Application to Structural Biology
    //641ページ

    #region B type. Getのみ
    /// <summary>
    /// unit: nm^2
    /// </summary>
    public double Biso => OriginalType == Type.B ? Iso : Iso * PI2 * 8;

    /// <summary>
    /// unit: nm^2. g=000の時のBiso. Acta Cryst. (1959). 12, 609 , Hamilton の式に従って、Bisoを計算
    /// </summary>
    public double Biso000 => (B11 * a2 + B22 * b2 + B33 * c2 + 2 * B12 * ab + 2 * B23 * bc + 2 * B31 * ca) * 4.0 / 3.0;

    /// <summary>
    /// 温度因子がゼロの場合はtrue
    /// </summary>
    public bool IsZero => UseIso ? Biso == 0 : B11 == 0 && B22 == 0 && B33 == 0 && B12 == 0 && B23 == 0 && B31 == 0;

    /// <summary>
    /// unit: nm^2
    /// </summary>
    public double Biso_err => OriginalType == Type.B ? Iso_err : Iso_err * PI2 * 8;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B11 => OriginalType == Type.B ? Aniso11 : Aniso11 * coeff11;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B22 => OriginalType == Type.B ? Aniso22 : Aniso22 * coeff22;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B33 => OriginalType == Type.B ? Aniso33 : Aniso33 * coeff33;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B12 => OriginalType == Type.B ? Aniso12 : Aniso12 * coeff12;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B23 => OriginalType == Type.B ? Aniso23 : Aniso23 * coeff23;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B31 => OriginalType == Type.B ? Aniso31 : Aniso31 * coeff31;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B11_err => OriginalType == Type.B ? Aniso11_err : Aniso11_err * coeff11;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B22_err => OriginalType == Type.B ? Aniso22_err : Aniso22_err * coeff22;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B33_err => OriginalType == Type.B ? Aniso33_err : Aniso33_err * coeff33;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B12_err => OriginalType == Type.B ? Aniso12_err : Aniso12_err * coeff12;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B23_err => OriginalType == Type.B ? Aniso23_err : Aniso23_err * coeff23;
    /// <summary>
    /// unit: none
    /// </summary>
    public double B31_err => OriginalType == Type.B ? Aniso31_err : Aniso31_err * coeff31;

    #endregion

    #region U type. Getのみ
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double Uiso => OriginalType == Type.U ? Iso : Iso / PI2 / 8;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double Uiso_err => OriginalType == Type.U ? Iso_err : Iso_err / PI2 / 8;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U11 => OriginalType == Type.U ? Aniso11 : Aniso11 / coeff11;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U22 => OriginalType == Type.U ? Aniso22 : Aniso22 / coeff22;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U33 => OriginalType == Type.U ? Aniso33 : Aniso33 / coeff33;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U12 => OriginalType == Type.U ? Aniso12 : Aniso12 / coeff12;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U23 => OriginalType == Type.U ? Aniso23 : Aniso23 / coeff23;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U31 => OriginalType == Type.U ? Aniso31 : Aniso31 / coeff31;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U11_err => OriginalType == Type.U ? Aniso11_err : Aniso11_err / coeff11;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U22_err => OriginalType == Type.U ? Aniso22_err : Aniso22_err / coeff22;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U33_err => OriginalType == Type.U ? Aniso33_err : Aniso33_err / coeff33;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U12_err => OriginalType == Type.U ? Aniso12_err : Aniso12_err / coeff12;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U23_err => OriginalType == Type.U ? Aniso23_err : Aniso23_err / coeff23;
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double U31_err => OriginalType == Type.U ? Aniso31_err : Aniso31_err / coeff31;
    #endregion

    #region オリジナルの値 
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double Iso { get; set; }
    /// <summary>
    /// 単位は nm^2
    /// </summary>
    public double Iso_err { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso11 { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso22 { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso33 { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso12 { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso23 { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso31 { get; set; }

    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso11_err { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso22_err { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso33_err { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso12_err { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso23_err { get; set; }
    /// <summary>
    /// 単位は Uの場合 nm^2,  Bの場合　無次元
    /// </summary>
    public double Aniso31_err { get; set; }

    #endregion

    public bool UseIso { get; set; }
    public Type OriginalType { get; set; } = Type.B;

    [XmlIgnore]
    public (double A, double B, double C, double Alpha, double Beta, double Gamma) Cell
    {
        get => cell;
        set
        {
            cell = value;
            a2 = cell.A * cell.A;
            b2 = cell.B * cell.B;
            c2 = cell.C * cell.C;
            ab = cell.A * cell.B;
            bc = cell.B * cell.C;
            ca = cell.C * cell.A;
            var cosAlpha = Math.Cos(cell.Alpha);
            var sinAlpha = Math.Sin(cell.Alpha);
            var cosBeta = Math.Cos(cell.Beta);
            var sinBeta = Math.Sin(cell.Beta);
            var cosGamma = Math.Cos(cell.Gamma);
            var sinGamma = Math.Sin(cell.Gamma);
            var v = cell.A * cell.B * cell.C * Math.Sqrt(1 - cosAlpha * cosAlpha - cosBeta * cosBeta - cosGamma * cosGamma + 2 * cosAlpha * cosBeta * cosGamma);
            var aStar = bc * sinAlpha / v;
            var bStar = ca * sinBeta / v;
            var cStar = ab * sinGamma / v;
            var cosAlphaStar = (cosBeta * cosGamma - cosAlpha) / sinBeta / sinGamma;
            var cosBetaStar = (cosGamma * cosAlpha - cosBeta) / sinGamma / sinAlpha;
            var cosGammaStar = (cosAlpha * cosBeta - cosGamma) / sinAlpha / sinBeta;
            coeff11 = PI2 * 2 * aStar * aStar;
            coeff22 = PI2 * 2 * bStar * bStar;
            coeff33 = PI2 * 2 * cStar * cStar;
            coeff12 = PI2 * 2 * aStar * bStar * cosGammaStar;
            coeff23 = PI2 * 2 * bStar * cStar * cosAlphaStar;
            coeff31 = PI2 * 2 * cStar * aStar * cosBetaStar;
        }
    }
    private (double A, double B, double C, double Alpha, double Beta, double Gamma) cell = (0, 0, 0, 0, 0, 0);

    private double a2, b2, c2, ab, bc, ca, coeff11, coeff22, coeff33, coeff12, coeff23, coeff31;
    #endregion

    #region コンストラクタ
    public DiffuseScatteringFactor()
    {
    }

    /// <summary>
    /// コンストラクタ. B##は無次元, 他はnm^2. Cellの 単位は nm & radians.
    /// </summary>
    /// <param name="t"></param>
    /// <param name="useIso"></param>
    /// <param name="iso"></param>
    /// <param name="iso_err"></param>
    /// <param name="aniso"></param>
    /// <param name="aniso_err"></param>
    /// <param name="cell"></param>
    public DiffuseScatteringFactor(Type t, bool useIso, double iso, double iso_err, double[] aniso, double[] aniso_err,
        (double A, double B, double C, double Alpha, double Beta, double Gamma) cell)
    {
        OriginalType = t;
        UseIso = useIso;
        Iso = iso;
        Iso_err = iso_err;
        Aniso11 = aniso[0];
        Aniso22 = aniso[1];
        Aniso33 = aniso[2];
        Aniso12 = aniso[3];
        Aniso23 = aniso[4];
        Aniso31 = aniso[5];
        Aniso11_err = aniso_err[0];
        Aniso22_err = aniso_err[1];
        Aniso33_err = aniso_err[2];
        Aniso12_err = aniso_err[3];
        Aniso23_err = aniso_err[4];
        Aniso31_err = aniso_err[5];

        Cell = cell;
    }
    #endregion
}

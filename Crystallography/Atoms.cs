using MathNet.Numerics.Integration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Xml.Serialization;

namespace Crystallography
{
    /// <summary>
    /// Structure の概要の説明です。
    /// </summary>
    ///
    [Serializable()]
    public class Atoms : System.IEquatable<Atoms>, ICloneable
    {
        public object Clone()
        {
            Atoms atoms = (Atoms)this.MemberwiseClone();
            for (int i = 0; i < Atom.Count; i++)
                atoms.Atom[i] = (Vector3D)Atom[i].Clone();
            return atoms;
        }

        public int ID;

        [XmlIgnore]
        public List<Vector3D> Atom = new();

        public double X, Y, Z;
        public double X_err, Y_err, Z_err;
        public double Occ, Occ_err;
        public int AtomicNumber;

        public int SubNumberXray = 0;
        public int SubNumberElectron = 0;
        public double[] Isotope = null;

        public string Label;

        [XmlIgnore]
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

        /// <summary>
        /// OpenGL描画時に、ラベルを表示するか
        /// </summary>
        public bool ShowLabel = false;

        /// <summary>
        /// OpenGLの描画時に有効にするかどうか
        /// </summary>
        public bool GLEnabled = true;

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

            Atom = new List<Vector3D>();
            for (int i = 0; i < Multiplicity; i++)
                Atom.Add(new Vector3D(0, 0, 0));

            Dsf = dsf;

            //Asf = new AtomicScatteringFactor(atomicNumber, subXray, subElectron);
            SubNumberXray = subXray;
            SubNumberElectron = subElectron;
            AtomicNumber = atomicNumber;
            Isotope = isotope;
            ElementName = AtomicNumber.ToString() + ": " + AtomConstants.AtomicName(atomicNumber);

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
            Vector3D pos, double occ, DiffuseScatteringFactor dsf)
        {
            SymmetrySeriesNumber = symmetrySeriesNumber;

            Label = label;

            Occ = occ;
            X = pos.X;
            Y = pos.Y;
            Z = pos.Z;

            var temp = WyckoffPosition.GetEquivalentAtomsPosition(pos, symmetrySeriesNumber);
            WyckoffLeter = temp.WyckoffLeter;
            SiteSymmetry = temp.SiteSymmetry;
            Multiplicity = temp.Multiplicity;
            WyckoffNumber = temp.WyckoffNumber;

            Atom = temp.Atom;
            Dsf = dsf;

            SubNumberXray = subXray;
            SubNumberElectron = subElectron;
            Isotope = isotope ?? Array.Empty<double>();
            AtomicNumber = atomicNumber;
            ElementName = AtomicNumber.ToString() + ": " + AtomConstants.AtomicName(atomicNumber);
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
           Vector3D pos, Vector3D pos_err, double occ, double occ_err, DiffuseScatteringFactor dsf)
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
           Vector3D pos, Vector3D pos_err, double occ, double occ_err,
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

        public void ResetSymmetry(int symmetrySeriesNumber)
        {
            SymmetrySeriesNumber = symmetrySeriesNumber;

            var temp = WyckoffPosition.GetEquivalentAtomsPosition(new Vector3D(X, Y, Z), symmetrySeriesNumber);
            WyckoffLeter = temp.WyckoffLeter;
            SiteSymmetry = temp.SiteSymmetry;
            Multiplicity = temp.Multiplicity;
            WyckoffNumber = temp.WyckoffNumber;
            ElementName = $"{AtomicNumber}: {AtomConstants.AtomicName(AtomicNumber)}";

            Atom = temp.Atom;
        }

        public void ResetVesta()
        {
            #region Vestaの色設定
            Texture = Material.DefaultTexture;
            switch (AtomicNumber)
            {
                case 1: Radius = (float)(0.46 * 0.4); Argb = Color.FromArgb(255, 204, 204).ToArgb(); break;
                case 2: Radius = (float)(1.22 * 0.4); Argb = Color.FromArgb(252, 233, 207).ToArgb(); break;
                case 3: Radius = (float)(1.57 * 0.4); Argb = Color.FromArgb(134, 224, 116).ToArgb(); break;
                case 4: Radius = (float)(1.12 * 0.4); Argb = Color.FromArgb(95, 216, 123).ToArgb(); break;
                case 5: Radius = (float)(0.81 * 0.4); Argb = Color.FromArgb(32, 162, 15).ToArgb(); break;
                case 6: Radius = (float)(0.77 * 0.4); Argb = Color.FromArgb(129, 73, 41).ToArgb(); break;
                case 7: Radius = (float)(0.74 * 0.4); Argb = Color.FromArgb(176, 186, 230).ToArgb(); break;
                case 8: Radius = (float)(0.74 * 0.4); Argb = Color.FromArgb(255, 3, 0).ToArgb(); break;
                case 9: Radius = (float)(0.72 * 0.4); Argb = Color.FromArgb(176, 186, 230).ToArgb(); break;
                case 10: Radius = (float)(1.6 * 0.4); Argb = Color.FromArgb(255, 56, 181).ToArgb(); break;
                case 11: Radius = (float)(1.91 * 0.4); Argb = Color.FromArgb(250, 221, 61).ToArgb(); break;
                case 12: Radius = (float)(1.6 * 0.4); Argb = Color.FromArgb(252, 124, 22).ToArgb(); break;
                case 13: Radius = (float)(1.43 * 0.4); Argb = Color.FromArgb(129, 179, 214).ToArgb(); break;
                case 14: Radius = (float)(1.18 * 0.4); Argb = Color.FromArgb(27, 59, 250).ToArgb(); break;
                case 15: Radius = (float)(1.1 * 0.4); Argb = Color.FromArgb(193, 156, 195).ToArgb(); break;
                case 16: Radius = (float)(1.04 * 0.4); Argb = Color.FromArgb(255, 250, 0).ToArgb(); break;
                case 17: Radius = (float)(0.99 * 0.4); Argb = Color.FromArgb(50, 252, 3).ToArgb(); break;
                case 18: Radius = (float)(1.92 * 0.4); Argb = Color.FromArgb(207, 254, 197).ToArgb(); break;
                case 19: Radius = (float)(2.35 * 0.4); Argb = Color.FromArgb(161, 34, 247).ToArgb(); break;
                case 20: Radius = (float)(1.97 * 0.4); Argb = Color.FromArgb(91, 150, 190).ToArgb(); break;
                case 21: Radius = (float)(1.64 * 0.4); Argb = Color.FromArgb(182, 99, 172).ToArgb(); break;
                case 22: Radius = (float)(1.47 * 0.4); Argb = Color.FromArgb(120, 202, 255).ToArgb(); break;
                case 23: Radius = (float)(1.35 * 0.4); Argb = Color.FromArgb(230, 26, 0).ToArgb(); break;
                case 24: Radius = (float)(1.29 * 0.4); Argb = Color.FromArgb(0, 0, 158).ToArgb(); break;
                case 25: Radius = (float)(1.37 * 0.4); Argb = Color.FromArgb(169, 9, 158).ToArgb(); break;
                case 26: Radius = (float)(1.26 * 0.4); Argb = Color.FromArgb(181, 114, 0).ToArgb(); break;
                case 27: Radius = (float)(1.25 * 0.4); Argb = Color.FromArgb(0, 0, 175).ToArgb(); break;
                case 28: Radius = (float)(1.25 * 0.4); Argb = Color.FromArgb(184, 188, 190).ToArgb(); break;
                case 29: Radius = (float)(1.28 * 0.4); Argb = Color.FromArgb(34, 71, 221).ToArgb(); break;
                case 30: Radius = (float)(1.37 * 0.4); Argb = Color.FromArgb(143, 144, 130).ToArgb(); break;
                case 31: Radius = (float)(1.53 * 0.4); Argb = Color.FromArgb(159, 228, 116).ToArgb(); break;
                case 32: Radius = (float)(1.22 * 0.4); Argb = Color.FromArgb(126, 111, 166).ToArgb(); break;
                case 33: Radius = (float)(1.21 * 0.4); Argb = Color.FromArgb(117, 208, 87).ToArgb(); break;
                case 34: Radius = (float)(1.04 * 0.4); Argb = Color.FromArgb(154, 239, 16).ToArgb(); break;
                case 35: Radius = (float)(1.14 * 0.4); Argb = Color.FromArgb(127, 49, 3).ToArgb(); break;
                case 36: Radius = (float)(1.98 * 0.4); Argb = Color.FromArgb(250, 193, 243).ToArgb(); break;
                case 37: Radius = (float)(2.5 * 0.4); Argb = Color.FromArgb(255, 0, 153).ToArgb(); break;
                case 38: Radius = (float)(2.15 * 0.4); Argb = Color.FromArgb(0, 255, 39).ToArgb(); break;
                case 39: Radius = (float)(1.82 * 0.4); Argb = Color.FromArgb(103, 152, 142).ToArgb(); break;
                case 40: Radius = (float)(1.6 * 0.4); Argb = Color.FromArgb(0, 255, 0).ToArgb(); break;
                case 41: Radius = (float)(1.47 * 0.4); Argb = Color.FromArgb(76, 179, 118).ToArgb(); break;
                case 42: Radius = (float)(1.4 * 0.4); Argb = Color.FromArgb(180, 134, 176).ToArgb(); break;
                case 43: Radius = (float)(1.35 * 0.4); Argb = Color.FromArgb(205, 175, 203).ToArgb(); break;
                case 44: Radius = (float)(1.34 * 0.4); Argb = Color.FromArgb(207, 184, 174).ToArgb(); break;
                case 45: Radius = (float)(1.34 * 0.4); Argb = Color.FromArgb(206, 210, 171).ToArgb(); break;
                case 46: Radius = (float)(1.37 * 0.4); Argb = Color.FromArgb(194, 196, 185).ToArgb(); break;
                case 47: Radius = (float)(1.44 * 0.4); Argb = Color.FromArgb(184, 188, 190).ToArgb(); break;
                case 48: Radius = (float)(1.52 * 0.4); Argb = Color.FromArgb(243, 31, 220).ToArgb(); break;
                case 49: Radius = (float)(1.67 * 0.4); Argb = Color.FromArgb(215, 129, 187).ToArgb(); break;
                case 50: Radius = (float)(1.58 * 0.4); Argb = Color.FromArgb(155, 143, 186).ToArgb(); break;
                case 51: Radius = (float)(1.41 * 0.4); Argb = Color.FromArgb(216, 131, 80).ToArgb(); break;
                case 52: Radius = (float)(1.37 * 0.4); Argb = Color.FromArgb(173, 162, 82).ToArgb(); break;
                case 53: Radius = (float)(1.33 * 0.4); Argb = Color.FromArgb(143, 31, 139).ToArgb(); break;
                case 54: Radius = (float)(2.18 * 0.4); Argb = Color.FromArgb(155, 161, 248).ToArgb(); break;
                case 55: Radius = (float)(2.72 * 0.4); Argb = Color.FromArgb(15, 255, 185).ToArgb(); break;
                case 56: Radius = (float)(2.24 * 0.4); Argb = Color.FromArgb(30, 240, 45).ToArgb(); break;
                case 57: Radius = (float)(1.88 * 0.4); Argb = Color.FromArgb(90, 196, 73).ToArgb(); break;
                case 58: Radius = (float)(1.82 * 0.4); Argb = Color.FromArgb(209, 253, 6).ToArgb(); break;
                case 59: Radius = (float)(1.82 * 0.4); Argb = Color.FromArgb(253, 226, 6).ToArgb(); break;
                case 60: Radius = (float)(1.82 * 0.4); Argb = Color.FromArgb(252, 142, 7).ToArgb(); break;
                case 61: Radius = (float)(1.81 * 0.4); Argb = Color.FromArgb(0, 0, 245).ToArgb(); break;
                case 62: Radius = (float)(1.81 * 0.4); Argb = Color.FromArgb(253, 6, 125).ToArgb(); break;
                case 63: Radius = (float)(2.06 * 0.4); Argb = Color.FromArgb(251, 8, 213).ToArgb(); break;
                case 64: Radius = (float)(1.79 * 0.4); Argb = Color.FromArgb(192, 4, 255).ToArgb(); break;
                case 65: Radius = (float)(1.77 * 0.4); Argb = Color.FromArgb(113, 4, 254).ToArgb(); break;
                case 66: Radius = (float)(1.77 * 0.4); Argb = Color.FromArgb(49, 6, 253).ToArgb(); break;
                case 67: Radius = (float)(1.76 * 0.4); Argb = Color.FromArgb(7, 66, 251).ToArgb(); break;
                case 68: Radius = (float)(1.75 * 0.4); Argb = Color.FromArgb(73, 115, 59).ToArgb(); break;
                case 69: Radius = (float)(1 * 0.4); Argb = Color.FromArgb(0, 0, 224).ToArgb(); break;
                case 70: Radius = (float)(1.94 * 0.4); Argb = Color.FromArgb(39, 253, 244).ToArgb(); break;
                case 71: Radius = (float)(1.72 * 0.4); Argb = Color.FromArgb(38, 253, 181).ToArgb(); break;
                case 72: Radius = (float)(1.59 * 0.4); Argb = Color.FromArgb(180, 180, 89).ToArgb(); break;
                case 73: Radius = (float)(1.47 * 0.4); Argb = Color.FromArgb(183, 155, 86).ToArgb(); break;
                case 74: Radius = (float)(1.41 * 0.4); Argb = Color.FromArgb(142, 138, 128).ToArgb(); break;
                case 75: Radius = (float)(1.37 * 0.4); Argb = Color.FromArgb(179, 177, 142).ToArgb(); break;
                case 76: Radius = (float)(1.35 * 0.4); Argb = Color.FromArgb(201, 177, 121).ToArgb(); break;
                case 77: Radius = (float)(1.36 * 0.4); Argb = Color.FromArgb(201, 207, 115).ToArgb(); break;
                case 78: Radius = (float)(1.39 * 0.4); Argb = Color.FromArgb(204, 198, 191).ToArgb(); break;
                case 79: Radius = (float)(1.44 * 0.4); Argb = Color.FromArgb(254, 179, 56).ToArgb(); break;
                case 80: Radius = (float)(1.55 * 0.4); Argb = Color.FromArgb(211, 184, 204).ToArgb(); break;
                case 81: Radius = (float)(1.71 * 0.4); Argb = Color.FromArgb(150, 137, 109).ToArgb(); break;
                case 82: Radius = (float)(1.75 * 0.4); Argb = Color.FromArgb(83, 83, 91).ToArgb(); break;
                case 83: Radius = (float)(1.82 * 0.4); Argb = Color.FromArgb(210, 48, 248).ToArgb(); break;
                case 84: Radius = (float)(1.77 * 0.4); Argb = Color.FromArgb(0, 0, 255).ToArgb(); break;
                case 85: Radius = (float)(0.62 * 0.4); Argb = Color.FromArgb(0, 0, 255).ToArgb(); break;
                case 86: Radius = (float)(0.8 * 0.4); Argb = Color.FromArgb(255, 255, 0).ToArgb(); break;
                case 87: Radius = (float)(1 * 0.4); Argb = Color.FromArgb(0, 0, 0).ToArgb(); break;
                case 88: Radius = (float)(2.35 * 0.4); Argb = Color.FromArgb(110, 170, 89).ToArgb(); break;
                case 89: Radius = (float)(2.03 * 0.4); Argb = Color.FromArgb(100, 158, 115).ToArgb(); break;
                case 90: Radius = (float)(1.8 * 0.4); Argb = Color.FromArgb(38, 254, 120).ToArgb(); break;
                case 91: Radius = (float)(1.63 * 0.4); Argb = Color.FromArgb(41, 251, 53).ToArgb(); break;
                case 92: Radius = (float)(1.56 * 0.4); Argb = Color.FromArgb(122, 162, 170).ToArgb(); break;
                case 93: Radius = (float)(1.56 * 0.4); Argb = Color.FromArgb(77, 77, 77).ToArgb(); break;
                case 94: Radius = (float)(1.64 * 0.4); Argb = Color.FromArgb(77, 77, 77).ToArgb(); break;
                case 95: Radius = (float)(1.73 * 0.4); Argb = Color.FromArgb(77, 77, 77).ToArgb(); break;
                case 96: Radius = (float)(0.8 * 0.4); Argb = Color.FromArgb(77, 77, 77).ToArgb(); break;
            }
            #endregion
        }



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
            if (wyk.FreedomX) X = r.NextDouble();
            if (wyk.FreedomY) Y = r.NextDouble();
            if (wyk.FreedomZ) Z = r.NextDouble();

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
            if (wyk.FreedomX) X += (r.NextDouble() - 1) * (r.NextDouble() - 1) * 4 * threshold;
            if (wyk.FreedomY) Y += (r.NextDouble() - 1) * (r.NextDouble() - 1) * 4 * threshold;
            if (wyk.FreedomZ) Z += (r.NextDouble() - 1) * (r.NextDouble() - 1) * 4 * threshold;

            Atom = SymmetryStatic.WyckoffPositions[SymmetrySeriesNumber][WyckoffNumber].GeneratePositions(X, Y, Z);
            X = Atom[0].X;
            Y = Atom[0].Y;
            Z = Atom[0].Z;
        }

        /*
		//原子を加える。
        public bool AddAtom(double X, double Y, double Z)
        {
            while (X < 0 || Y < 0 || Z < 0 || X >= 1 || Y >= 1 || Z >= 1)
            {
                if (X < 0) X += 1; if (Y < 0) Y += 1; if (Z < 0) Z += 1; if (X >= 1) X -= 1; if (Y >= 1) Y -= 1; if (Z >= 1) Z -= 1;
            }

            if (Atom.Count > 0)
            {
                //当たり判定
                for (int i = 0; i < Atom.Count; i++)
                {
                    int xStart = (X < 0.00001) ? -1 : 0;
                    int yStart = (Y < 0.00001) ? -1 : 0;
                    int zStart = (Z < 0.00001) ? -1 : 0;
                    int xEnd = (X > 0.99999) ? 1 : 0;
                    int yEnd = (Y > 0.99999) ? 1 : 0;
                    int zEnd = (Z > 0.99999) ? 1 : 0;
                    for (int xx = xStart; xx <= xEnd; xx++)
                        for (int yy = yStart; yy <= yEnd; yy++)
                            for (int zz = zStart; zz <= zEnd; zz++)
                                if ((X - xx - Atom[i].X) * (X - xx - Atom[i].X) + (Y - yy - Atom[i].Y) * (Y - yy - Atom[i].Y) + (Z - zz - Atom[i].Z) * (Z - zz - Atom[i].Z) < 0.00000001)
                                    return false;
                }
                Atom.Add(new Vector3D(X, Y, Z,false));
            }
            else
            {
                Atom.Add(new Vector3D(X, Y, Z, false));
                this.X = X;
                this.Y = Y;
                this.Z = Z;
            }
            return true;
        }
        */

        /// <summary>
        /// //電子線の原子散乱因子を計算 s2の単位はnm^-2
        /// </summary>
        /// <param name="S2">S2: (sin(theta)/ramda)^2, unit is nm^-2</param>
        /// <returns></returns>
        public double GetAtomicScatteringFactorForElectron(double s2)
            => AtomConstants.ElectronScattering[AtomicNumber][SubNumberElectron].Factor(s2) * Occ;

        /// <summary>
        /// X線の原子散乱因子を計算 s2の単位はnm^-2
        /// </summary>
        /// <param name="s2"> unit is nm^-2</param>
        /// <returns></returns>
        public double GetAtomicScatteringFactorForXray(double s2)
            => AtomConstants.XrayScattering[AtomicNumber][SubNumberXray].Factor(s2) * Occ;

        public Complex GetAtomicScatteringFactorForNeutron()
        {
            if (Isotope != null && Isotope.Length == AtomConstants.IsotopeAbundance[AtomicNumber].Count)
            {
                var f = new Complex();
                for (int i = 0; i < AtomConstants.IsotopeAbundance[AtomicNumber].Count; i++)
                    f += AtomConstants.NeutronCoherentScattering[AtomicNumber][i + 1] * Isotope[i] / 100.0;
                return f * Occ;
            }
            else
                return AtomConstants.NeutronCoherentScattering[AtomicNumber][0] * Occ;
        }

        public override string ToString()
        {
            return $"{Label}\t{ElementName}\t{GetStringFromDouble(X)}\t{GetStringFromDouble(Y)}\t{GetStringFromDouble(Z)}\t{GetStringFromDouble(Occ)}\t{Multiplicity}\t{WyckoffLeter}\t{SiteSymmetry}";
        }

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
                catch { System.Windows.Forms.MessageBox.Show("数値を入力してください"); return 0; }
        }

        public bool Equals(Atoms obj)
        {
            Atoms atoms = obj;
            if (atoms.Label == Label && atoms.X == X && atoms.Y == Y && atoms.Z == Z && atoms.Occ == Occ)
                return true;
            else
                return false;
        }
    }

    #region 原子散乱因子の係数クラス
    /*   [Serializable()]
       public class AtomicScatteringFactor {
           public int AtomicNumber;//原子番号
           public string AtomicName;//原子の名前

           public int ValenceForXray;//価数
           public int SubNumberForXray;//X線用のSub番号
           public int ScatteringNumberForXray;//X線用の通し番号
           public string MethodForXray;//X線の計算方法
           public double[] X_A = new double[4];
           public double[] X_B = new double[4];
           public double X_C;

           public int ValenceForElectron;//価数
           public int SubNumberForElectron;//電子線用のSub番号
           public int ScatteringNumberForElectron;//電子線用の通し番号
           public string MethodForElectron;//電子線の計算方法
           public double[] E_A = new double[5];
           public double[] E_B = new double[5];

           public string Methods;

           public AtomicScatteringFactor()
           {
           }

           public AtomicScatteringFactor(int atomicNumber, int subXray, int subElectron)
           {
               SetCoefficientForXray(atomicNumber, subXray);
               SetCoefficientForElectron(atomicNumber, subElectron);
           }

           #region 原子番号、Sub番号を対応付けるプロパティ群
           private static List<List<int>> _X;
           /// <summary>
           /// X[AtomicNumber][SubNumber] = SeriesNumber
           /// </summary>
           public static List<List<int>> X
           {
               set { _X = value; }
               get
               {
                   if (_X == null)
                       setList();
                   return _X;
               }
           }

           private static List<List<int>> _x;
           /// <summary>
           /// // x[SeriesNumber][0] = AtomicNummber,  x[series][1] = SubNumber
           /// </summary>
           public static List<List<int>> x
           {
               set { _x = value; }
               get
               {
                   if (_x == null)
                       setList();
                   return _x;
               }
           }

           private static List<List<int>> _E;
            /// <summary>
           /// E[AtomicNumber][SubNumber] = SeriesNumber
           /// </summary>
           public static List<List<int>> E
           {
               set { _E = value; }
               get
               {
                   if (_E == null)
                       setList();
                   return _E;
               }
           }

           private static List<List<int>> _e;
           /// <summary>
           /// // e[SeriesNumber][0] = AtomicNummber,  e[series][1] = SubNumber
           /// </summary>
           public static List<List<int>> e
           {
               set { _e = value; }
               get
               {
                   if (_e == null)
                       setList();
                   return _e;
               }
           }

           private static void setList()
           {
               _X = new List<List<int>>();
               _x = new List<List<int>>();

               for (int i = 0; i <= 98; i++)//原子番号の上限まで
                   _X.Add(new List<int>());

               for (int i = 0; i <= 211; i++)//全データ数
                   _x.Add(new List<int>());

               for (int i = 0; i <= 211; i++)
               {
                   AtomicScatteringFactor asf = new AtomicScatteringFactor();
                   asf.SetCoefficientForXray(i);
                   int atomicNo = asf.AtomicNumber;
                   int subNo = asf.SubNumberForXray;

                   _x[asf.ScatteringNumberForXray].Add(atomicNo);
                   _x[asf.ScatteringNumberForXray].Add(subNo);

                   if (_X[atomicNo].Count < subNo + 1)
                       while (_X[atomicNo].Count < subNo + 1)
                           _X[atomicNo].Add(0);

                   _X[atomicNo][subNo] = asf.ScatteringNumberForXray;
               }

               _E = new List<List<int>>();
               _e = new List<List<int>>();

               for (int i = 0; i <= 98; i++)//原子番号の上限まで
                   _E.Add(new List<int>());

               for (int i = 0; i <= 204; i++)//全データ数
                   _e.Add(new List<int>());

               for (int i = 0; i <= 204; i++)
               {
                   AtomicScatteringFactor asf = new AtomicScatteringFactor();
                   asf.SetCoefficientForElectron(i);
                   int atomicNo = asf.AtomicNumber;
                   int subNo = asf.SubNumberForElectron;

                   _e[asf.ScatteringNumberForElectron].Add(atomicNo);
                   _e[asf.ScatteringNumberForElectron].Add(subNo);

                   if (_E[atomicNo].Count < subNo + 1)
                       while (_E[atomicNo].Count < subNo + 1)
                           _E[atomicNo].Add(0);

                   _E[atomicNo][subNo] = asf.ScatteringNumberForElectron;
               }
           }
           #endregion 原子番号、Sub番号を対応付けるプロパティ群

           public void SetCoefficientForXray(int atomicNumber, int subNumber)
           {
               if (atomicNumber < 0 || atomicNumber > X.Count-1)
                   atomicNumber = subNumber = 0;
               else if (X[atomicNumber].Count <= subNumber)
                   subNumber = 0;

               int num = X[atomicNumber][subNumber];
               SetCoefficientForXray(num);
           }
           public void SetCoefficientForXray(int num)
           {
               int[] i=new int[3];
               double[] d=new double[9];
               string[] s=new string [2];
               int v = 0;
               switch (num)
               {
                   #region
                   //通し番号,原子番号,sub,a1,b1,a2,b2,a3,b3,a4,b4,c,Note
                   case 0: i = new int[] { 0, 0, 0 }; d = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; v = 0; s = new string[] { "Unknown", "Unknown" }; break;
                   case 1: i = new int[] { 1, 1, 0 }; d = new double[] { 0.493002, 10.5109, 0.322912, 26.1257, 0.140191, 3.14236, 0.04081, 57.7997, 0.003038 }; v = 0; s = new string[] { "H", "H: " }; break;
                   case 2: i = new int[] { 2, 1, 1 }; d = new double[] { 0.897661, 53.1368, 0.565616, 15.187, 0.415815, 186.576, 0.116973, 3.56709, 0.002389 }; v = -1; s = new string[] { "H", "H1-: HF" }; break;
                   case 3: i = new int[] { 3, 2, 0 }; d = new double[] { 0.8734, 9.1037, 0.6309, 3.3568, 0.3112, 22.9276, 0.178, 0.9821, 0.0064 }; v = 0; s = new string[] { "He", "He: RHF" }; break;
                   case 4: i = new int[] { 4, 3, 0 }; d = new double[] { 1.1282, 3.9546, 0.7508, 1.0524, 0.6175, 85.3905, 0.4653, 168.261, 0.0377 }; v = 0; s = new string[] { "Li", "Li: RHF" }; break;
                   case 5: i = new int[] { 5, 3, 1 }; d = new double[] { 0.6968, 4.6237, 0.7888, 1.9557, 0.3414, 0.6316, 0.1563, 10.0953, 0.0167 }; v = +1; s = new string[] { "Li", "Li1+: RHF" }; break;
                   case 6: i = new int[] { 6, 4, 0 }; d = new double[] { 1.5919, 43.6427, 1.1278, 1.8623, 0.5391, 103.483, 0.7029, 0.542, 0.0385 }; v = 0; s = new string[] { "Be", "Be: RHF" }; break;
                   case 7: i = new int[] { 7, 4, 1 }; d = new double[] { 6.2603, 0.0027, 0.8849, 0.8313, 0.7993, 2.2758, 0.1647, 5.1146, -6.1092 }; v = +2; s = new string[] { "Be", "Be2+: RHF" }; break;
                   case 8: i = new int[] { 8, 5, 0 }; d = new double[] { 2.0545, 23.2185, 1.3326, 1.021, 1.0979, 60.3498, 0.7068, 0.1403, -0.1932 }; v = 0; s = new string[] { "B", "B: RHF" }; break;
                   case 9: i = new int[] { 9, 6, 0 }; d = new double[] { 2.31, 20.8439, 1.02, 10.2075, 1.5886, 0.5687, 0.865, 51.6512, 0.2156 }; v = 0; s = new string[] { "C", "C: RHF" }; break;
                   case 10: i = new int[] { 10, 6, 1 }; d = new double[] { 2.26069, 22.6907, 1.56165, 0.656665, 1.05075, 9.75618, 0.839259, 55.5949, 0.286977 }; v = 0; s = new string[] { "C", "C val: HF" }; break;
                   case 11: i = new int[] { 11, 7, 0 }; d = new double[] { 12.2126, 0.0057, 3.1322, 9.8933, 2.0125, 28.9975, 1.1663, 0.5826, -11.529 }; v = 0; s = new string[] { "N", "N: RHF" }; break;
                   case 12: i = new int[] { 12, 8, 0 }; d = new double[] { 3.0485, 13.2771, 2.2868, 5.7011, 1.5463, 0.3239, 0.867, 32.9089, 0.2508 }; v = 0; s = new string[] { "O", "O: RHF" }; break;
                   case 13: i = new int[] { 13, 8, 1 }; d = new double[] { 4.1916, 12.8573, 1.63969, 4.17236, 1.52673, 47.0179, -20.307, -0.01404, 21.9412 }; v = -1; s = new string[] { "O", "O1-: HF" }; break;
                   case 14: i = new int[] { 14, 9, 0 }; d = new double[] { 3.5392, 10.2825, 2.6412, 4.2944, 1.517, 0.2615, 1.0243, 26.1476, 0.2776 }; v = 0; s = new string[] { "F", "F: RHF" }; break;
                   case 15: i = new int[] { 15, 9, 1 }; d = new double[] { 3.6322, 5.27756, 3.51057, 14.7353, 1.26064, 0.442258, 0.940706, 47.3437, 0.653396 }; v = -1; s = new string[] { "F", "F1-: HF" }; break;
                   case 16: i = new int[] { 16, 10, 0 }; d = new double[] { 3.9553, 8.4042, 3.1125, 3.4262, 1.4546, 0.2306, 1.1251, 21.7184, 0.3515 }; v = 0; s = new string[] { "Ne", "Ne: RHF" }; break;
                   case 17: i = new int[] { 17, 11, 0 }; d = new double[] { 4.7626, 3.285, 3.1736, 8.8422, 1.2674, 0.3136, 1.1128, 129.424, 0.676 }; v = 0; s = new string[] { "Na", "Na: RHF" }; break;
                   case 18: i = new int[] { 18, 11, 1 }; d = new double[] { 3.2565, 2.6671, 3.9362, 6.1153, 1.3998, 0.2001, 1.0032, 14.039, 0.404 }; v = +1; s = new string[] { "Na", "Na1+: RHF" }; break;
                   case 19: i = new int[] { 19, 12, 0 }; d = new double[] { 5.4204, 2.8275, 2.1735, 79.2611, 1.2269, 0.3808, 2.3073, 7.1937, 0.8584 }; v = 0; s = new string[] { "Mg", "Mg: RHF" }; break;
                   case 20: i = new int[] { 20, 12, 1 }; d = new double[] { 3.4988, 2.1676, 3.8378, 4.7542, 1.3284, 0.185, 0.8497, 10.1411, 0.4853 }; v = +2; s = new string[] { "Mg", "Mg2+: RHF" }; break;
                   case 21: i = new int[] { 21, 13, 0 }; d = new double[] { 6.4202, 3.0387, 1.9002, 0.7426, 1.5936, 31.5472, 1.9646, 85.0886, 1.1151 }; v = 0; s = new string[] { "Al", "Al: RHF" }; break;
                   case 22: i = new int[] { 22, 13, 1 }; d = new double[] { 4.17448, 1.93816, 3.3876, 4.14553, 1.20296, 0.228753, 0.528137, 8.28524, 0.706786 }; v = +3; s = new string[] { "Al", "Al3+: HF" }; break;
                   case 23: i = new int[] { 23, 14, 0 }; d = new double[] { 6.2915, 2.4386, 3.0353, 32.3337, 1.9891, 0.6785, 1.541, 81.6937, 1.1407 }; v = 0; s = new string[] { "Si", "Si: RHF" }; break;
                   case 24: i = new int[] { 24, 14, 1 }; d = new double[] { 4.43918, 1.64167, 3.20345, 3.43757, 1.19453, 0.2149, 0.41653, 6.65365, 0.746297 }; v = +4; s = new string[] { "Si", "Si4+: HF" }; break;
                   case 25: i = new int[] { 25, 14, 2 }; d = new double[] { 5.66269, 2.6652, 3.07164, 38.6634, 2.62446, 0.916946, 1.3932, 93.5458, 1.24707 }; v = 0; s = new string[] { "Si", "Si val: HF" }; break;
                   case 26: i = new int[] { 26, 15, 0 }; d = new double[] { 6.4345, 1.9067, 4.1791, 27.157, 1.78, 0.526, 1.4908, 68.1645, 1.1149 }; v = 0; s = new string[] { "P", "P: RHF" }; break;
                   case 27: i = new int[] { 27, 16, 0 }; d = new double[] { 6.9053, 1.4679, 5.2034, 22.2151, 1.4379, 0.2536, 1.5863, 56.172, 0.8669 }; v = 0; s = new string[] { "S", "S: RHF" }; break;
                   case 28: i = new int[] { 28, 17, 0 }; d = new double[] { 11.4604, 0.0104, 7.1964, 1.1662, 6.2556, 18.5194, 1.6455, 47.7784, -9.5574 }; v = 0; s = new string[] { "Cl", "Cl: RHF" }; break;
                   case 29: i = new int[] { 29, 17, 1 }; d = new double[] { 18.2915, 0.0066, 7.2084, 1.1717, 6.5337, 19.5424, 2.3386, 60.4486, -16.378 }; v = -1; s = new string[] { "Cl", "Cl1-: RHF" }; break;
                   case 30: i = new int[] { 30, 18, 0 }; d = new double[] { 7.4845, 0.9072, 6.7723, 14.8407, 0.6539, 43.8983, 1.6442, 33.3929, 1.4445 }; v = 0; s = new string[] { "Ar", "Ar: RHF" }; break;
                   case 31: i = new int[] { 31, 19, 0 }; d = new double[] { 8.2186, 12.7949, 7.4398, 0.7748, 1.0519, 213.187, 0.8659, 41.6841, 1.4228 }; v = 0; s = new string[] { "K", "K: RHF" }; break;
                   case 32: i = new int[] { 32, 19, 1 }; d = new double[] { 7.9578, 12.6331, 7.4917, 0.7674, 6.359, -0.002, 1.1915, 31.9128, -4.9978 }; v = +1; s = new string[] { "K", "K1+: RHF" }; break;
                   case 33: i = new int[] { 33, 20, 0 }; d = new double[] { 8.6266, 10.4421, 7.3873, 0.6599, 1.5899, 85.7484, 1.0211, 178.437, 1.3751 }; v = 0; s = new string[] { "Ca", "Ca: RHF" }; break;
                   case 34: i = new int[] { 34, 20, 1 }; d = new double[] { 15.6348, -0.0074, 7.9518, 0.6089, 8.4372, 10.3116, 0.8537, 25.9905, -14.875 }; v = +2; s = new string[] { "Ca", "Ca2+: RHF" }; break;
                   case 35: i = new int[] { 35, 21, 0 }; d = new double[] { 9.189, 9.0213, 7.3679, 0.5729, 1.6409, 136.108, 1.468, 51.3531, 1.3329 }; v = 0; s = new string[] { "Sc", "Sc: RHF" }; break;
                   case 36: i = new int[] { 36, 21, 1 }; d = new double[] { 13.4008, 0.29854, 8.0273, 7.9629, 1.65943, -0.28604, 1.57936, 16.0662, -6.6667 }; v = +3; s = new string[] { "Sc", "Sc3+: HF" }; break;
                   case 37: i = new int[] { 37, 22, 0 }; d = new double[] { 9.7595, 7.8508, 7.3558, 0.5, 1.6991, 35.6338, 1.9021, 116.105, 1.2807 }; v = 0; s = new string[] { "Ti", "Ti: " }; break;
                   case 38: i = new int[] { 38, 22, 1 }; d = new double[] { 9.11423, 7.5243, 7.62174, 0.457585, 2.2793, 19.5361, 0.087899, 61.6558, 0.897155 }; v = +2; s = new string[] { "Ti", "Ti2+: HF" }; break;
                   case 39: i = new int[] { 39, 22, 2 }; d = new double[] { 17.7344, 0.22061, 8.73816, 7.04716, 5.25691, -0.15762, 1.92134, 15.9768, -14.652 }; v = +3; s = new string[] { "Ti", "Ti3+: HF" }; break;
                   case 40: i = new int[] { 40, 22, 3 }; d = new double[] { 19.5114, 0.178847, 8.23473, 6.67018, 2.01341, -0.29263, 1.5208, 12.9464, -13.28 }; v = +4; s = new string[] { "Ti", "Ti4+: HF" }; break;
                   case 41: i = new int[] { 41, 23, 0 }; d = new double[] { 10.2971, 6.8657, 7.3511, 0.4385, 2.0703, 26.8938, 2.0571, 102.478, 1.2199 }; v = 0; s = new string[] { "V", "V: RHF" }; break;
                   case 42: i = new int[] { 42, 23, 1 }; d = new double[] { 10.106, 6.8818, 7.3541, 0.4409, 2.2884, 20.3004, 0.0223, 115.122, 1.2298 }; v = +2; s = new string[] { "V", "V2+: RHF" }; break;
                   case 43: i = new int[] { 43, 23, 2 }; d = new double[] { 9.43141, 6.39535, 7.7419, 0.383349, 2.15343, 15.1908, 0.016865, 63.969, 0.656565 }; v = +3; s = new string[] { "V", "V3+: HF" }; break;
                   case 44: i = new int[] { 44, 23, 3 }; d = new double[] { 15.6887, 0.679003, 8.14208, 5.40135, 2.03081, 9.97278, -9.576, 0.940464, 1.7143 }; v = +5; s = new string[] { "V", "V5+: HF" }; break;
                   case 45: i = new int[] { 45, 24, 0 }; d = new double[] { 10.6406, 6.1038, 7.3537, 0.392, 3.324, 20.2626, 1.4922, 98.7399, 1.1832 }; v = 0; s = new string[] { "Cr", "Cr: RHF" }; break;
                   case 46: i = new int[] { 46, 24, 1 }; d = new double[] { 9.54034, 5.66078, 7.7509, 0.344261, 3.58274, 13.3075, 0.509107, 32.4224, 0.616898 }; v = +2; s = new string[] { "Cr", "Cr2+: HF" }; break;
                   case 47: i = new int[] { 47, 24, 2 }; d = new double[] { 9.6809, 5.59463, 7.81136, 0.334393, 2.87603, 12.8288, 0.113575, 32.8761, 0.518275 }; v = +3; s = new string[] { "Cr", "Cr3+: HF" }; break;
                   case 48: i = new int[] { 48, 25, 0 }; d = new double[] { 11.2819, 5.3409, 7.3573, 0.3432, 3.0193, 17.8674, 2.2441, 83.7543, 1.0896 }; v = 0; s = new string[] { "Mn", "Mn: RHF" }; break;
                   case 49: i = new int[] { 49, 25, 1 }; d = new double[] { 10.8061, 5.2796, 7.362, 0.3435, 3.5268, 14.343, 0.2184, 41.3235, 1.0874 }; v = +2; s = new string[] { "Mn", "Mn2+: RHF" }; break;
                   case 50: i = new int[] { 50, 25, 2 }; d = new double[] { 9.84521, 4.91797, 7.87194, 0.294393, 3.56531, 10.8171, 0.323613, 24.1281, 0.393974 }; v = +3; s = new string[] { "Mn", "Mn3+: HF" }; break;
                   case 51: i = new int[] { 51, 25, 3 }; d = new double[] { 9.96253, 4.8485, 7.97057, 0.283303, 2.76067, 10.4852, 0.054447, 27.573, 0.251877 }; v = +4; s = new string[] { "Mn", "Mn4+: HF" }; break;
                   case 52: i = new int[] { 52, 26, 0 }; d = new double[] { 11.7695, 4.7611, 7.3573, 0.3072, 3.5222, 15.3535, 2.3045, 76.8805, 1.0369 }; v = 0; s = new string[] { "Fe", "Fe: RHF" }; break;
                   case 53: i = new int[] { 53, 26, 1 }; d = new double[] { 11.0424, 4.6538, 7.374, 0.3053, 4.1346, 12.0546, 0.4399, 31.2809, 1.0097 }; v = +2; s = new string[] { "Fe", "Fe2+: RHF" }; break;
                   case 54: i = new int[] { 54, 26, 2 }; d = new double[] { 11.1764, 4.6147, 7.3863, 0.3005, 3.3948, 11.6729, 0.0724, 38.5566, 0.9707 }; v = +3; s = new string[] { "Fe", "Fe3+: RHF" }; break;
                   case 55: i = new int[] { 55, 27, 0 }; d = new double[] { 12.2841, 4.2791, 7.3409, 0.2784, 4.0034, 13.5359, 2.3488, 71.1692, 1.0118 }; v = 0; s = new string[] { "Co", "Co: RHF" }; break;
                   case 56: i = new int[] { 56, 27, 1 }; d = new double[] { 11.2296, 4.1231, 7.3883, 0.2726, 4.7393, 10.2443, 0.7108, 25.6466, 0.9324 }; v = +2; s = new string[] { "Co", "Co2+: RHF" }; break;
                   case 57: i = new int[] { 57, 27, 2 }; d = new double[] { 10.338, 3.90969, 7.88173, 0.238668, 4.76795, 8.35583, 0.725591, 18.3491, 118.349 }; v = +3; s = new string[] { "Co", "Co3+: HF" }; break;
                   case 58: i = new int[] { 58, 28, 0 }; d = new double[] { 12.8376, 3.8785, 7.292, 0.2565, 4.4438, 12.1763, 2.38, 66.3421, 1.0341 }; v = 0; s = new string[] { "Ni", "Ni: RHF" }; break;
                   case 59: i = new int[] { 59, 28, 1 }; d = new double[] { 11.4166, 3.6766, 7.4005, 0.2449, 5.3442, 8.873, 0.9773, 22.1626, 0.8614 }; v = +2; s = new string[] { "Ni", "Ni2+: RHF" }; break;
                   case 60: i = new int[] { 60, 28, 2 }; d = new double[] { 10.7806, 3.5477, 7.75868, 0.22314, 5.22746, 7.64468, 0.847114, 16.9673, 0.386044 }; v = +3; s = new string[] { "Ni", "Ni3+: HF" }; break;
                   case 61: i = new int[] { 61, 29, 0 }; d = new double[] { 13.338, 3.5828, 7.1676, 0.247, 5.6158, 11.3966, 1.6735, 64.8126, 1.191 }; v = 0; s = new string[] { "Cu", "Cu: RHF" }; break;
                   case 62: i = new int[] { 62, 29, 1 }; d = new double[] { 11.9475, 3.3669, 7.3573, 0.2274, 6.2455, 8.6625, 1.5578, 25.8487, 0.89 }; v = +1; s = new string[] { "Cu", "Cu1+: RHF" }; break;
                   case 63: i = new int[] { 63, 29, 2 }; d = new double[] { 11.8168, 3.37484, 7.11181, 0.244078, 5.78135, 7.9876, 1.14523, 19.897, 1.14431 }; v = +2; s = new string[] { "Cu", "Cu2+: HF" }; break;
                   case 64: i = new int[] { 64, 30, 0 }; d = new double[] { 14.0743, 3.2655, 7.0318, 0.2333, 5.1652, 10.3163, 2.41, 58.7097, 1.3041 }; v = 0; s = new string[] { "Zn", "Zn: RHF" }; break;
                   case 65: i = new int[] { 65, 30, 1 }; d = new double[] { 11.9719, 2.9946, 7.3862, 0.2031, 6.4668, 7.0826, 1.394, 18.0995, 0.7807 }; v = +2; s = new string[] { "Zn", "Zn2+: RHF" }; break;
                   case 66: i = new int[] { 66, 31, 0 }; d = new double[] { 15.2354, 3.0669, 6.7006, 0.2412, 4.3591, 10.7805, 2.9623, 61.4135, 1.7189 }; v = 0; s = new string[] { "Ga", "Ga: RHF" }; break;
                   case 67: i = new int[] { 67, 31, 1 }; d = new double[] { 12.692, 2.81262, 6.69883, 0.22789, 6.06692, 6.36441, 1.0066, 14.4122, 1.53545 }; v = +3; s = new string[] { "Ga", "Ga3+: HF" }; break;
                   case 68: i = new int[] { 68, 32, 0 }; d = new double[] { 16.0816, 2.8509, 6.3747, 0.2516, 3.7068, 11.4468, 3.683, 54.7625, 2.1313 }; v = 0; s = new string[] { "Ge", "Ge: RHF" }; break;
                   case 69: i = new int[] { 69, 32, 1 }; d = new double[] { 12.9172, 2.53718, 6.70003, 0.205855, 6.06791, 5.47913, 0.859041, 11.603, 1.45572 }; v = +4; s = new string[] { "Ge", "Ge4+: HF" }; break;
                   case 70: i = new int[] { 70, 33, 0 }; d = new double[] { 16.6723, 2.6345, 6.0701, 0.2647, 3.4313, 12.9479, 4.2779, 47.7972, 2.531 }; v = 0; s = new string[] { "As", "As: RHF" }; break;
                   case 71: i = new int[] { 71, 34, 0 }; d = new double[] { 17.0006, 2.4098, 5.8196, 0.2726, 3.9731, 15.2372, 4.3543, 43.8163, 2.8409 }; v = 0; s = new string[] { "Se", "Se: RHF" }; break;
                   case 72: i = new int[] { 72, 35, 0 }; d = new double[] { 17.1789, 2.1723, 5.2358, 16.5796, 5.6377, 0.2609, 3.9851, 41.4328, 2.9557 }; v = 0; s = new string[] { "Br", "Br: RHF" }; break;
                   case 73: i = new int[] { 73, 35, 1 }; d = new double[] { 17.1718, 2.2059, 6.3338, 19.3345, 5.5754, 0.2871, 3.7272, 58.1535, 3.1776 }; v = -1; s = new string[] { "Br", "Br1-: RHF" }; break;
                   case 74: i = new int[] { 74, 36, 0 }; d = new double[] { 17.3555, 1.9384, 6.7286, 16.5623, 5.5493, 0.2261, 3.5375, 39.3972, 2.825 }; v = 0; s = new string[] { "Kr", "Kr: RHF" }; break;
                   case 75: i = new int[] { 75, 37, 0 }; d = new double[] { 17.1784, 1.7888, 9.6435, 17.3151, 5.1399, 0.2748, 1.5292, 164.934, 3.4873 }; v = 0; s = new string[] { "Rb", "Rb: RHF" }; break;
                   case 76: i = new int[] { 76, 37, 1 }; d = new double[] { 17.5816, 1.7139, 7.6598, 14.7957, 5.8981, 0.1603, 2.7817, 31.2087, 2.0782 }; v = +1; s = new string[] { "Rb", "Rb1+: RHF" }; break;
                   case 77: i = new int[] { 77, 38, 0 }; d = new double[] { 17.5663, 1.5564, 9.8184, 14.0988, 5.422, 0.1664, 2.6694, 132.376, 2.5064 }; v = 0; s = new string[] { "Sr", "Sr: RHF" }; break;
                   case 78: i = new int[] { 78, 38, 1 }; d = new double[] { 18.0874, 1.4907, 8.1373, 12.6963, 2.5654, 24.5651, -34.193, -0.0138, 41.4025 }; v = +2; s = new string[] { "Sr", "Sr2+: RHF" }; break;
                   case 79: i = new int[] { 79, 39, 0 }; d = new double[] { 17.776, 1.4029, 10.2946, 12.8006, 5.72629, 0.125599, 3.26588, 104.354, 1.91213 }; v = 0; s = new string[] { "Y", "Y: *RHF" }; break;
                   case 80: i = new int[] { 80, 39, 1 }; d = new double[] { 17.9268, 1.35417, 9.1531, 11.2145, 1.76795, 22.6599, -33.108, -0.01319, 40.2602 }; v = +3; s = new string[] { "Y", "Y3+: *DS" }; break;
                   case 81: i = new int[] { 81, 40, 0 }; d = new double[] { 17.8765, 1.27618, 10.948, 11.916, 5.41732, 0.117622, 3.65721, 87.6627, 2.06929 }; v = 0; s = new string[] { "Zr", "Zr: *RHF" }; break;
                   case 82: i = new int[] { 82, 40, 1 }; d = new double[] { 18.1668, 1.2148, 10.0562, 10.1483, 1.01118, 21.6054, -2.6479, -0.10276, 9.41454 }; v = +4; s = new string[] { "Zr", "Zr4+: *DS" }; break;
                   case 83: i = new int[] { 83, 41, 0 }; d = new double[] { 17.6142, 1.18865, 12.0144, 11.766, 4.04183, 0.204785, 3.53346, 69.7957, 3.75591 }; v = 0; s = new string[] { "Nb", "Nb: *RHF" }; break;
                   case 84: i = new int[] { 84, 41, 1 }; d = new double[] { 19.8812, 0.019175, 18.0653, 1.13305, 11.0177, 10.1621, 1.94715, 28.3389, -12.912 }; v = +3; s = new string[] { "Nb", "Nb3+: *DS" }; break;
                   case 85: i = new int[] { 85, 41, 2 }; d = new double[] { 17.9163, 1.12446, 13.3417, 0.028781, 10.799, 9.28206, 0.337905, 25.7228, -6.3934 }; v = +5; s = new string[] { "Nb", "Nb5+: *DS" }; break;
                   case 86: i = new int[] { 86, 42, 0 }; d = new double[] { 3.7025, 0.2772, 17.2356, 1.0958, 12.8876, 11.004, 3.7429, 61.6584, 4.3875 }; v = 0; s = new string[] { "Mo", "Mo: RHF" }; break;
                   case 87: i = new int[] { 87, 42, 1 }; d = new double[] { 21.1664, 0.014734, 18.2017, 1.03031, 11.7423, 9.53659, 2.30951, 26.6307, -14.421 }; v = +3; s = new string[] { "Mo", "Mo3+: *DS" }; break;
                   case 88: i = new int[] { 88, 42, 2 }; d = new double[] { 21.0149, 0.014345, 18.0992, 1.02238, 11.4632, 8.78809, 0.740625, 23.3452, -14.316 }; v = +5; s = new string[] { "Mo", "Mo5+: *DS" }; break;
                   case 89: i = new int[] { 89, 42, 3 }; d = new double[] { 17.8871, 1.03649, 11.175, 8.48061, 6.57891, 0.058881, 0, 0, 0.344941 }; v = +6; s = new string[] { "Mo", "Mo6+: *DS" }; break;
                   case 90: i = new int[] { 90, 43, 0 }; d = new double[] { 19.1301, 0.864132, 11.0948, 8.14487, 4.64901, 21.5707, 2.71263, 86.8472, 5.40428 }; v = 0; s = new string[] { "Tc", "Tc: *RHF" }; break;
                   case 91: i = new int[] { 91, 44, 0 }; d = new double[] { 19.2674, 0.80852, 12.9182, 8.43467, 4.86337, 24.7997, 1.56756, 94.2928, 5.37874 }; v = 0; s = new string[] { "Ru", "Ru: *RHF" }; break;
                   case 92: i = new int[] { 92, 44, 1 }; d = new double[] { 18.5638, 0.847329, 13.2885, 8.37164, 9.32602, 0.017662, 3.00964, 22.887, -3.1892 }; v = +3; s = new string[] { "Ru", "Ru3+: *DS" }; break;
                   case 93: i = new int[] { 93, 44, 2 }; d = new double[] { 18.5003, 0.844582, 13.1787, 8.12534, 4.71304, 0.036495, 2.18535, 20.8504, 1.42357 }; v = +4; s = new string[] { "Ru", "Ru4+: *DS" }; break;
                   case 94: i = new int[] { 94, 45, 0 }; d = new double[] { 19.2957, 0.751536, 14.3501, 8.21758, 4.73425, 25.8749, 1.28918, 98.6062, 5.328 }; v = 0; s = new string[] { "Rh", "Rh: *RHF" }; break;
                   case 95: i = new int[] { 95, 45, 1 }; d = new double[] { 18.8785, 0.764252, 14.1259, 7.84438, 3.32515, 21.2487, -6.1989, -0.01036, 11.8678 }; v = +3; s = new string[] { "Rh", "Rh3+: *DS" }; break;
                   case 96: i = new int[] { 96, 45, 2 }; d = new double[] { 18.8545, 0.760825, 13.9806, 7.62436, 2.53464, 19.3317, -5.6526, -0.0102, 11.2835 }; v = +4; s = new string[] { "Rh", "Rh4+: *DS" }; break;
                   case 97: i = new int[] { 97, 46, 0 }; d = new double[] { 19.3319, 0.698655, 15.5017, 7.98929, 5.29537, 25.2052, 0.605844, 76.8986, 5.26593 }; v = 0; s = new string[] { "Pd", "Pd: *RHF" }; break;
                   case 98: i = new int[] { 98, 46, 1 }; d = new double[] { 19.1701, 0.696219, 15.2096, 7.55573, 4.32234, 22.5057, 0, 0, 5.2916 }; v = +2; s = new string[] { "Pd", "Pd2+: *DS" }; break;
                   case 99: i = new int[] { 99, 46, 2 }; d = new double[] { 19.2493, 0.683839, 14.79, 7.14833, 2.89289, 17.9144, -7.9492, 0.005127, 13.0174 }; v = +4; s = new string[] { "Pd", "Pd4+: *DS" }; break;
                   case 100: i = new int[] { 100, 47, 0 }; d = new double[] { 19.2808, 0.6446, 16.6885, 7.4726, 4.8045, 24.6605, 1.0463, 99.8156, 5.179 }; v = 0; s = new string[] { "Ag", "Ag: RHF" }; break;
                   case 101: i = new int[] { 101, 47, 1 }; d = new double[] { 19.1812, 0.646179, 15.9719, 7.19123, 5.27475, 21.7326, 0.357534, 66.1147, 5.21572 }; v = +1; s = new string[] { "Ag", "Ag1+: *DS" }; break;
                   case 102: i = new int[] { 102, 47, 2 }; d = new double[] { 19.1643, 0.645643, 16.2456, 7.18544, 4.3709, 21.4072, 0, 0, 5.21404 }; v = +2; s = new string[] { "Ag", "Ag2+: *DS" }; break;
                   case 103: i = new int[] { 103, 48, 0 }; d = new double[] { 19.2214, 0.5946, 17.6444, 6.9089, 4.461, 24.7008, 1.6029, 87.4825, 5.0694 }; v = 0; s = new string[] { "Cd", "Cd: RHF" }; break;
                   case 104: i = new int[] { 104, 48, 1 }; d = new double[] { 19.1514, 0.597922, 17.2535, 6.80639, 4.47128, 20.2521, 0, 0, 5.11937 }; v = +2; s = new string[] { "Cd", "Cd2+: *DS" }; break;
                   case 105: i = new int[] { 105, 49, 0 }; d = new double[] { 19.1624, 0.5476, 18.5596, 6.3776, 4.2948, 25.8499, 2.0396, 92.8029, 4.9391 }; v = 0; s = new string[] { "In", "In: RHF" }; break;
                   case 106: i = new int[] { 106, 49, 1 }; d = new double[] { 19.1045, 0.551522, 18.1108, 6.3247, 3.78897, 17.3595, 0, 0, 4.99635 }; v = +3; s = new string[] { "In", "In3+: *DS" }; break;
                   case 107: i = new int[] { 107, 50, 0 }; d = new double[] { 19.1889, 5.8303, 19.1005, 0.5031, 4.4585, 26.8909, 2.4663, 83.9571, 4.7821 }; v = 0; s = new string[] { "Sn", "Sn: RHF" }; break;
                   case 108: i = new int[] { 108, 50, 1 }; d = new double[] { 19.1094, 0.5036, 19.0548, 5.8378, 4.5648, 23.3752, 0.487, 62.2061, 4.7861 }; v = +2; s = new string[] { "Sn", "Sn2+: RHF" }; break;
                   case 109: i = new int[] { 109, 50, 2 }; d = new double[] { 18.9333, 5.764, 19.7131, 0.4655, 3.4182, 14.0049, 0.0193, -0.7583, 3.9182 }; v = +4; s = new string[] { "Sn", "Sn4+: RHF" }; break;
                   case 110: i = new int[] { 110, 51, 0 }; d = new double[] { 19.6418, 5.3034, 19.0455, 0.4607, 5.0371, 27.9074, 2.6827, 75.2825, 4.5909 }; v = 0; s = new string[] { "Sb", "Sb: RHF" }; break;
                   case 111: i = new int[] { 111, 51, 1 }; d = new double[] { 18.9755, 0.467196, 18.933, 5.22126, 5.10789, 19.5902, 0.288753, 55.5113, 4.69626 }; v = +3; s = new string[] { "Sb", "Sb3+: *DS" }; break;
                   case 112: i = new int[] { 112, 51, 2 }; d = new double[] { 19.8685, 5.44853, 19.0302, 0.467973, 2.41253, 14.1259, 0, 0, 4.69263 }; v = +5; s = new string[] { "Sb", "Sb5+: *DS" }; break;
                   case 113: i = new int[] { 113, 52, 0 }; d = new double[] { 19.9644, 4.81742, 19.0138, 0.420885, 6.14487, 28.5284, 2.5239, 70.8403, 4.352 }; v = 0; s = new string[] { "Te", "Te: *RHF" }; break;
                   case 114: i = new int[] { 114, 53, 0 }; d = new double[] { 20.1472, 4.347, 18.9949, 0.3814, 7.5138, 27.766, 2.2735, 66.8776, 4.0712 }; v = 0; s = new string[] { "I", "I: RHF" }; break;
                   case 115: i = new int[] { 115, 53, 1 }; d = new double[] { 20.2332, 4.3579, 18.997, 0.3815, 7.8069, 29.5259, 2.8868, 84.9304, 4.0714 }; v = -1; s = new string[] { "I", "I1-: RHF" }; break;
                   case 116: i = new int[] { 116, 54, 0 }; d = new double[] { 20.2933, 3.9282, 19.0298, 0.344, 8.9767, 26.4659, 1.99, 64.2658, 3.7118 }; v = 0; s = new string[] { "Xe", "Xe: RHF" }; break;
                   case 117: i = new int[] { 117, 55, 0 }; d = new double[] { 20.3892, 3.569, 19.1062, 0.3107, 10.662, 24.3879, 1.4953, 213.904, 3.3352 }; v = 0; s = new string[] { "Cs", "Cs: RHF" }; break;
                   case 118: i = new int[] { 118, 55, 1 }; d = new double[] { 20.3524, 3.552, 19.1278, 0.3086, 10.2821, 23.7128, 0.9615, 59.4565, 3.2791 }; v = +1; s = new string[] { "Cs", "Cs1+: RHF" }; break;
                   case 119: i = new int[] { 119, 56, 0 }; d = new double[] { 20.3361, 3.216, 19.297, 0.2756, 10.888, 20.2073, 2.6959, 167.202, 2.7731 }; v = 0; s = new string[] { "Ba", "Ba: RHF" }; break;
                   case 120: i = new int[] { 120, 56, 1 }; d = new double[] { 20.1807, 3.21367, 19.1136, 0.28331, 10.9054, 20.0558, 0.773634, 51.746, 3.02902 }; v = +2; s = new string[] { "Ba", "Ba2+: *DS" }; break;
                   case 121: i = new int[] { 121, 57, 0 }; d = new double[] { 20.578, 2.94817, 19.599, 0.244475, 11.3727, 18.7726, 3.28719, 133.124, 2.14678 }; v = 0; s = new string[] { "La", "La: *RHF" }; break;
                   case 122: i = new int[] { 122, 57, 1 }; d = new double[] { 20.2489, 2.9207, 19.3763, 0.250698, 11.6323, 17.8211, 0.336048, 54.9453, 2.4086 }; v = +3; s = new string[] { "La", "La3+: *DS" }; break;
                   case 123: i = new int[] { 123, 58, 0 }; d = new double[] { 21.1671, 2.81219, 19.7695, 0.226836, 11.8513, 17.6083, 3.33049, 127.113, 1.86264 }; v = 0; s = new string[] { "Ce", "Ce: *RHF" }; break;
                   case 124: i = new int[] { 124, 58, 1 }; d = new double[] { 20.8036, 2.77691, 19.559, 0.23154, 11.9369, 16.5408, 0.612376, 43.1692, 2.09013 }; v = +3; s = new string[] { "Ce", "Ce3+: *DS" }; break;
                   case 125: i = new int[] { 125, 58, 2 }; d = new double[] { 20.3235, 2.65941, 19.8186, 0.21885, 12.1233, 15.7992, 0.144583, 62.2355, 1.5918 }; v = +4; s = new string[] { "Ce", "Ce4+: *DS" }; break;
                   case 126: i = new int[] { 126, 59, 0 }; d = new double[] { 22.044, 2.77393, 19.6697, 0.222087, 12.3856, 16.7669, 2.82428, 143.644, 2.0583 }; v = 0; s = new string[] { "Pr", "Pr: *RHF" }; break;
                   case 127: i = new int[] { 127, 59, 1 }; d = new double[] { 21.3727, 2.6452, 19.7491, 0.214299, 12.1329, 15.323, 0.97518, 36.4065, 1.77132 }; v = +3; s = new string[] { "Pr", "Pr3+: *DS" }; break;
                   case 128: i = new int[] { 128, 59, 2 }; d = new double[] { 20.9413, 2.54467, 20.0539, 0.202481, 12.4668, 14.8137, 0.296689, 45.4643, 1.24285 }; v = +4; s = new string[] { "Pr", "Pr4+: *DS" }; break;
                   case 129: i = new int[] { 129, 60, 0 }; d = new double[] { 22.6845, 2.66248, 19.6847, 0.210628, 12.774, 15.885, 2.85137, 137.903, 1.98486 }; v = 0; s = new string[] { "Nd", "Nd: *RHF" }; break;
                   case 130: i = new int[] { 130, 60, 1 }; d = new double[] { 21.961, 2.52722, 19.9339, 0.199237, 12.12, 14.1783, 1.51031, 30.8717, 1.47588 }; v = +3; s = new string[] { "Nd", "Nd3+: *DS" }; break;
                   case 131: i = new int[] { 131, 61, 0 }; d = new double[] { 23.3405, 2.5627, 19.6095, 0.202088, 13.1235, 15.1009, 2.87516, 132.721, 2.02876 }; v = 0; s = new string[] { "Pm", "Pm: *RHF" }; break;
                   case 132: i = new int[] { 132, 61, 1 }; d = new double[] { 22.5527, 2.4174, 20.1108, 0.185769, 12.0671, 13.1275, 2.07492, 27.4491, 1.19499 }; v = +3; s = new string[] { "Pm", "Pm3+: *DS" }; break;
                   case 133: i = new int[] { 133, 62, 0 }; d = new double[] { 24.0042, 2.47274, 19.4258, 0.196451, 13.4396, 14.3996, 2.89604, 128.007, 2.20963 }; v = 0; s = new string[] { "Sm", "Sm: *RHF" }; break;
                   case 134: i = new int[] { 134, 62, 1 }; d = new double[] { 23.1504, 2.31641, 20.2599, 0.174081, 11.9202, 12.1571, 2.71488, 24.8242, 0.954586 }; v = +3; s = new string[] { "Sm", "Sm3+: *DS" }; break;
                   case 135: i = new int[] { 135, 63, 0 }; d = new double[] { 24.6274, 2.3879, 19.0886, 0.1942, 13.7603, 13.7546, 2.9227, 123.174, 2.5745 }; v = 0; s = new string[] { "Eu", "Eu: RHF" }; break;
                   case 136: i = new int[] { 136, 63, 1 }; d = new double[] { 24.0063, 2.27783, 19.9504, 0.17353, 11.8034, 11.6096, 3.87243, 26.5156, 1.36389 }; v = +2; s = new string[] { "Eu", "Eu2+: *DS" }; break;
                   case 137: i = new int[] { 137, 63, 2 }; d = new double[] { 23.7497, 2.22258, 20.3745, 0.16394, 11.8509, 11.311, 3.26503, 22.9966, 0.759344 }; v = +3; s = new string[] { "Eu", "Eu3+: *DS" }; break;
                   case 138: i = new int[] { 138, 64, 0 }; d = new double[] { 25.0709, 2.25341, 19.0798, 0.181951, 13.8518, 12.9331, 3.54545, 101.398, 2.4196 }; v = 0; s = new string[] { "Gd", "Gd: *RHF" }; break;
                   case 139: i = new int[] { 139, 64, 1 }; d = new double[] { 24.3466, 2.13553, 20.4208, 0.155525, 11.8708, 10.5782, 3.7149, 21.7029, 0.645089 }; v = +3; s = new string[] { "Gd", "Gd3+: *DS" }; break;
                   case 140: i = new int[] { 140, 65, 0 }; d = new double[] { 25.8976, 2.24256, 18.2185, 0.196143, 14.3167, 12.6648, 2.95354, 115.362, 3.58324 }; v = 0; s = new string[] { "Tb", "Tb: *RHF" }; break;
                   case 141: i = new int[] { 141, 65, 1 }; d = new double[] { 24.9559, 2.05601, 20.3271, 0.149525, 12.2471, 10.0499, 3.773, 21.2773, 0.691967 }; v = +3; s = new string[] { "Tb", "Tb3+: *DS" }; break;
                   case 142: i = new int[] { 142, 66, 0 }; d = new double[] { 26.507, 2.1802, 17.6383, 0.202172, 14.5596, 12.1899, 2.96577, 111.874, 4.29728 }; v = 0; s = new string[] { "Dy", "Dy: *RHF" }; break;
                   case 143: i = new int[] { 143, 66, 1 }; d = new double[] { 25.5395, 1.9804, 20.2861, 0.143384, 11.9812, 9.34972, 4.50073, 19.581, 0.68969 }; v = +3; s = new string[] { "Dy", "Dy3+: *DS" }; break;
                   case 144: i = new int[] { 144, 67, 0 }; d = new double[] { 26.9049, 2.07051, 17.294, 0.19794, 14.5583, 11.4407, 3.63837, 92.6566, 4.56796 }; v = 0; s = new string[] { "Ho", "Ho: *RHF" }; break;
                   case 145: i = new int[] { 145, 67, 1 }; d = new double[] { 26.1296, 1.91072, 20.0994, 0.139358, 11.9788, 8.80018, 4.93676, 18.5908, 0.852795 }; v = +3; s = new string[] { "Ho", "Ho3+: *DS" }; break;
                   case 146: i = new int[] { 146, 68, 0 }; d = new double[] { 27.6563, 2.07356, 16.4285, 0.223545, 14.9779, 11.3604, 2.98233, 105.703, 5.92046 }; v = 0; s = new string[] { "Er", "Er: *RHF" }; break;
                   case 147: i = new int[] { 147, 68, 1 }; d = new double[] { 26.722, 1.84659, 19.7748, 0.13729, 12.1506, 8.36225, 5.17379, 17.8974, 1.17613 }; v = +3; s = new string[] { "Er", "Er3+: *DS" }; break;
                   case 148: i = new int[] { 148, 69, 0 }; d = new double[] { 28.1819, 2.02859, 15.8851, 0.238849, 15.1542, 10.9975, 2.98706, 102.961, 6.75621 }; v = 0; s = new string[] { "Tm", "Tm: *RHF" }; break;
                   case 149: i = new int[] { 149, 69, 1 }; d = new double[] { 27.3083, 1.78711, 19.332, 0.136974, 12.3339, 7.96778, 5.38348, 17.2922, 1.63929 }; v = +3; s = new string[] { "Tm", "Tm3+: *DS" }; break;
                   case 150: i = new int[] { 150, 70, 0 }; d = new double[] { 28.6641, 1.9889, 15.4345, 0.257119, 15.3087, 10.6647, 2.98963, 100.417, 7.56672 }; v = 0; s = new string[] { "Yb", "Yb: *RHF" }; break;
                   case 151: i = new int[] { 151, 70, 1 }; d = new double[] { 28.1209, 1.78503, 17.6817, 0.15997, 13.3335, 8.18304, 5.14657, 20.39, 3.70983 }; v = +2; s = new string[] { "Yb", "Yb2+: *DS" }; break;
                   case 152: i = new int[] { 152, 70, 2 }; d = new double[] { 27.8917, 1.73272, 18.7614, 0.13879, 12.6072, 7.64412, 5.47647, 16.8153, 2.26001 }; v = +3; s = new string[] { "Yb", "Yb3+: *DS" }; break;
                   case 153: i = new int[] { 153, 71, 0 }; d = new double[] { 28.9476, 1.90182, 15.2208, 9.98519, 15.1, 0.261033, 3.71601, 84.3298, 7.97628 }; v = 0; s = new string[] { "Lu", "Lu: *RHF" }; break;
                   case 154: i = new int[] { 154, 71, 1 }; d = new double[] { 28.4628, 1.68216, 18.121, 0.142292, 12.8429, 7.33727, 5.59415, 16.3535, 2.97573 }; v = +3; s = new string[] { "Lu", "Lu3+: *DS" }; break;
                   case 155: i = new int[] { 155, 72, 0 }; d = new double[] { 29.144, 1.83262, 15.1726, 9.5999, 14.7586, 0.275116, 4.30013, 72.029, 8.58154 }; v = 0; s = new string[] { "Hf", "Hf: *RHF" }; break;
                   case 156: i = new int[] { 156, 72, 1 }; d = new double[] { 28.8131, 1.59136, 18.4601, 0.128903, 12.7285, 6.76232, 5.59927, 14.0366, 2.39699 }; v = +4; s = new string[] { "Hf", "Hf4+: *DS" }; break;
                   case 157: i = new int[] { 157, 73, 0 }; d = new double[] { 29.2024, 1.77333, 15.2293, 9.37046, 14.5135, 0.295977, 4.76492, 63.3644, 9.24354 }; v = 0; s = new string[] { "Ta", "Ta: *RHF" }; break;
                   case 158: i = new int[] { 158, 73, 1 }; d = new double[] { 29.1587, 1.50711, 18.8407, 0.116741, 12.8268, 6.31524, 5.38695, 12.4244, 1.78555 }; v = +5; s = new string[] { "Ta", "Ta5+: *DS" }; break;
                   case 159: i = new int[] { 159, 74, 0 }; d = new double[] { 29.0818, 1.72029, 15.43, 9.2259, 14.4327, 0.321703, 5.11982, 57.056, 9.8875 }; v = 0; s = new string[] { "W", "W: *RHF" }; break;
                   case 160: i = new int[] { 160, 74, 1 }; d = new double[] { 29.4936, 1.42755, 19.3763, 0.104621, 13.0544, 5.93667, 5.06412, 11.1972, 1.01074 }; v = +6; s = new string[] { "W", "W6+: *DS" }; break;
                   case 161: i = new int[] { 161, 75, 0 }; d = new double[] { 28.7621, 1.67191, 15.7189, 9.09227, 14.5564, 0.3505, 5.44174, 52.0861, 10.472 }; v = 0; s = new string[] { "Re", "Re: *RHF" }; break;
                   case 162: i = new int[] { 162, 76, 0 }; d = new double[] { 28.1894, 1.62903, 16.155, 8.97948, 14.9305, 0.382661, 5.67589, 48.1647, 11.0005 }; v = 0; s = new string[] { "Os", "Os: *RHF" }; break;
                   case 163: i = new int[] { 163, 76, 1 }; d = new double[] { 30.419, 1.37113, 15.2637, 6.84706, 14.7458, 0.165191, 5.06795, 18.003, 6.49804 }; v = +4; s = new string[] { "Os", "Os4+: *DS" }; break;
                   case 164: i = new int[] { 164, 77, 0 }; d = new double[] { 27.3049, 1.59279, 16.7296, 8.86553, 15.6115, 0.417916, 5.83377, 45.0011, 11.4722 }; v = 0; s = new string[] { "Ir", "Ir: *RHF" }; break;
                   case 165: i = new int[] { 165, 77, 1 }; d = new double[] { 30.4156, 1.34323, 15.862, 7.10909, 13.6145, 0.204633, 5.82008, 20.3254, 8.27903 }; v = +3; s = new string[] { "Ir", "Ir3+: *DS" }; break;
                   case 166: i = new int[] { 166, 77, 2 }; d = new double[] { 30.7058, 1.30923, 15.5512, 6.71983, 14.2326, 0.167252, 5.53672, 17.4911, 6.96824 }; v = +4; s = new string[] { "Ir", "Ir4+: *DS" }; break;
                   case 167: i = new int[] { 167, 78, 0 }; d = new double[] { 27.0059, 1.51293, 17.7639, 8.81174, 15.7131, 0.424593, 5.7837, 38.6103, 11.6883 }; v = 0; s = new string[] { "Pt", "Pt: *RHF" }; break;
                   case 168: i = new int[] { 168, 78, 1 }; d = new double[] { 29.8429, 1.32927, 16.7224, 7.38979, 13.2153, 0.263297, 6.35234, 22.9426, 9.85329 }; v = +2; s = new string[] { "Pt", "Pt2+: *DS" }; break;
                   case 169: i = new int[] { 169, 78, 2 }; d = new double[] { 30.9612, 1.24813, 15.9829, 6.60834, 13.7348, 0.16864, 5.92034, 16.9392, 7.39534 }; v = +4; s = new string[] { "Pt", "Pt4+: *DS" }; break;
                   case 170: i = new int[] { 170, 79, 0 }; d = new double[] { 16.8819, 0.4611, 18.5913, 8.6216, 25.5582, 1.4826, 5.86, 36.3956, 12.0658 }; v = 0; s = new string[] { "Au", "Au: RHF" }; break;
                   case 171: i = new int[] { 171, 79, 1 }; d = new double[] { 28.0109, 1.35321, 17.8204, 7.7395, 14.3359, 0.356752, 6.58077, 26.4043, 11.2299 }; v = +1; s = new string[] { "Au", "Au1+: *DS" }; break;
                   case 172: i = new int[] { 172, 79, 2 }; d = new double[] { 30.6886, 1.2199, 16.9029, 6.82872, 12.7801, 0.212867, 6.52354, 18.659, 9.0968 }; v = +3; s = new string[] { "Au", "Au3+: *DS" }; break;
                   case 173: i = new int[] { 173, 80, 0 }; d = new double[] { 20.6809, 0.545, 19.0417, 8.4484, 21.6575, 1.5729, 5.9676, 38.3246, 12.6089 }; v = 0; s = new string[] { "Hg", "Hg: RHF" }; break;
                   case 174: i = new int[] { 174, 80, 1 }; d = new double[] { 25.0853, 1.39507, 18.4973, 7.65105, 16.8883, 0.443378, 6.48216, 28.2262, 12.0205 }; v = +1; s = new string[] { "Hg", "Hg1+: *DS" }; break;
                   case 175: i = new int[] { 175, 80, 2 }; d = new double[] { 29.5641, 1.21152, 18.06, 7.05639, 12.8374, 0.284738, 6.89912, 20.7482, 10.6268 }; v = +2; s = new string[] { "Hg", "Hg2+: *DS" }; break;
                   case 176: i = new int[] { 176, 81, 0 }; d = new double[] { 27.5446, 0.65515, 19.1584, 8.70751, 15.538, 1.96347, 5.52593, 45.8149, 13.1746 }; v = 0; s = new string[] { "Tl", "Tl: *RHF" }; break;
                   case 177: i = new int[] { 177, 81, 1 }; d = new double[] { 21.3985, 1.4711, 20.4723, 0.517394, 18.7478, 7.43463, 6.82847, 28.8482, 12.5258 }; v = +1; s = new string[] { "Tl", "Tl1+: *DS" }; break;
                   case 178: i = new int[] { 178, 81, 2 }; d = new double[] { 30.8695, 1.1008, 18.3841, 6.53852, 11.9328, 0.219074, 7.00574, 17.2114, 9.8027 }; v = +3; s = new string[] { "Tl", "Tl3+: *DS" }; break;
                   case 179: i = new int[] { 179, 82, 0 }; d = new double[] { 31.0617, 0.6902, 13.0637, 2.3576, 18.442, 8.618, 5.9696, 47.2579, 13.4118 }; v = 0; s = new string[] { "Pb", "Pb: RHF" }; break;
                   case 180: i = new int[] { 180, 82, 1 }; d = new double[] { 21.7886, 1.3366, 19.5682, 0.488383, 19.1406, 6.7727, 7.01107, 23.8132, 12.4734 }; v = +2; s = new string[] { "Pb", "Pb2+: *DS" }; break;
                   case 181: i = new int[] { 181, 82, 2 }; d = new double[] { 32.1244, 1.00566, 18.8003, 6.10926, 12.0175, 0.147041, 6.96886, 14.714, 8.08428 }; v = +4; s = new string[] { "Pb", "Pb4+: *DS" }; break;
                   case 182: i = new int[] { 182, 83, 0 }; d = new double[] { 33.3689, 0.704, 12.951, 2.9238, 16.5877, 8.7937, 6.4692, 48.0093, 13.5782 }; v = 0; s = new string[] { "Bi", "Bi: RHF" }; break;
                   case 183: i = new int[] { 183, 83, 1 }; d = new double[] { 21.8053, 1.2356, 19.5026, 6.24149, 19.1053, 0.469999, 7.10295, 20.3185, 12.4711 }; v = +3; s = new string[] { "Bi", "Bi3+: *DS" }; break;
                   case 184: i = new int[] { 184, 83, 2 }; d = new double[] { 33.5364, 0.91654, 25.0946, 0.039042, 19.2497, 5.71414, 6.91555, 12.8285, -6.7994 }; v = +5; s = new string[] { "Bi", "Bi5+: *DS" }; break;
                   case 185: i = new int[] { 185, 84, 0 }; d = new double[] { 34.6726, 0.700999, 15.4733, 3.55078, 13.1138, 9.55642, 7.02588, 47.0045, 13.677 }; v = 0; s = new string[] { "Po", "Po: *RHF" }; break;
                   case 186: i = new int[] { 186, 85, 0 }; d = new double[] { 35.3163, 0.68587, 19.0211, 3.97458, 9.49887, 11.3824, 7.42518, 45.4715, 13.7108 }; v = 0; s = new string[] { "At", "At: *RHF" }; break;
                   case 187: i = new int[] { 187, 86, 0 }; d = new double[] { 35.5631, 0.6631, 21.2816, 4.0691, 8.0037, 14.0422, 7.4433, 44.2473, 13.6905 }; v = 0; s = new string[] { "Rn", "Rn: RHF" }; break;
                   case 188: i = new int[] { 188, 87, 0 }; d = new double[] { 35.9299, 0.646453, 23.0547, 4.17619, 12.1439, 23.1052, 2.11253, 150.645, 13.7247 }; v = 0; s = new string[] { "Fr", "Fr: *RHF" }; break;
                   case 189: i = new int[] { 189, 88, 0 }; d = new double[] { 35.763, 0.616341, 22.9064, 3.87135, 12.4739, 19.9887, 3.21097, 142.325, 13.6211 }; v = 0; s = new string[] { "Ra", "Ra: *RHF" }; break;
                   case 190: i = new int[] { 190, 88, 1 }; d = new double[] { 35.215, 0.604909, 21.67, 3.5767, 7.91342, 12.601, 7.65078, 29.8436, 13.5431 }; v = +2; s = new string[] { "Ra", "Ra2+: *DS" }; break;
                   case 191: i = new int[] { 191, 89, 0 }; d = new double[] { 35.6597, 0.589092, 23.1032, 3.65155, 12.5977, 18.599, 4.08655, 117.02, 13.5266 }; v = 0; s = new string[] { "Ac", "Ac: *RHF" }; break;
                   case 192: i = new int[] { 192, 89, 1 }; d = new double[] { 35.1736, 0.579689, 22.1112, 3.41437, 8.19216, 12.9187, 7.05545, 25.9443, 13.4637 }; v = +3; s = new string[] { "Ac", "Ac3+: *DS" }; break;
                   case 193: i = new int[] { 193, 90, 0 }; d = new double[] { 35.5645, 0.563359, 23.4219, 3.46204, 12.7473, 17.8309, 4.80703, 99.1722, 13.4314 }; v = 0; s = new string[] { "Th", "Th: *RHF" }; break;
                   case 194: i = new int[] { 194, 90, 1 }; d = new double[] { 35.1007, 0.555054, 22.4418, 3.24498, 9.78554, 13.4661, 5.29444, 23.9533, 13.376 }; v = +4; s = new string[] { "Th", "Th4+: *DS" }; break;
                   case 195: i = new int[] { 195, 91, 0 }; d = new double[] { 35.8847, 0.547751, 23.2948, 3.41519, 14.1891, 16.9235, 4.17287, 105.251, 13.4287 }; v = 0; s = new string[] { "Pa", "Pa: *RHF" }; break;
                   case 196: i = new int[] { 196, 92, 0 }; d = new double[] { 36.0228, 0.5293, 23.4128, 3.3253, 14.9491, 16.0927, 4.188, 100.613, 13.3966 }; v = 0; s = new string[] { "U", "U: RHF" }; break;
                   case 197: i = new int[] { 197, 92, 1 }; d = new double[] { 35.5747, 0.52048, 22.5259, 3.12293, 12.2165, 12.7148, 5.37073, 26.3394, 13.3092 }; v = +3; s = new string[] { "U", "U3+: *DS" }; break;
                   case 198: i = new int[] { 198, 92, 2 }; d = new double[] { 35.3715, 0.516598, 22.5326, 3.05053, 12.0291, 12.5723, 4.7984, 23.4582, 13.2671 }; v = +4; s = new string[] { "U", "U4+: *DS" }; break;
                   case 199: i = new int[] { 199, 92, 3 }; d = new double[] { 34.8509, 0.507079, 22.7584, 2.8903, 14.0099, 13.1767, 1.21457, 25.2017, 13.1665 }; v = +6; s = new string[] { "U", "U6+: *DS" }; break;
                   case 200: i = new int[] { 200, 93, 0 }; d = new double[] { 36.1874, 0.511929, 23.5964, 3.25396, 15.6402, 15.3622, 4.1855, 97.4908, 13.3573 }; v = 0; s = new string[] { "Np", "Np: *RHF" }; break;
                   case 201: i = new int[] { 201, 93, 1 }; d = new double[] { 35.7074, 0.502322, 22.613, 3.03807, 12.9898, 12.1449, 5.43227, 25.4928, 13.2544 }; v = +3; s = new string[] { "Np", "Np3+: *DS" }; break;
                   case 202: i = new int[] { 202, 93, 2 }; d = new double[] { 35.5103, 0.498626, 22.5787, 2.96627, 12.7766, 11.9484, 4.92159, 22.7502, 13.2116 }; v = +4; s = new string[] { "Np", "Np4+: *DS" }; break;
                   case 203: i = new int[] { 203, 93, 3 }; d = new double[] { 35.0136, 0.48981, 22.7286, 2.81099, 14.3884, 12.33, 1.75669, 22.6581, 13.113 }; v = +6; s = new string[] { "Np", "Np6+: *DS" }; break;
                   case 204: i = new int[] { 204, 94, 0 }; d = new double[] { 36.5254, 0.499384, 23.8083, 3.26371, 16.7707, 14.9455, 3.47947, 105.98, 13.3812 }; v = 0; s = new string[] { "Pu", "Pu: *RHF" }; break;
                   case 205: i = new int[] { 205, 94, 1 }; d = new double[] { 35.84, 0.484938, 22.7169, 2.96118, 13.5807, 11.5331, 5.66016, 24.3992, 13.1991 }; v = +3; s = new string[] { "Pu", "Pu3+: *DS" }; break;
                   case 206: i = new int[] { 206, 94, 2 }; d = new double[] { 35.6493, 0.481422, 22.646, 2.8902, 13.3595, 11.316, 5.18831, 21.8301, 13.1555 }; v = +4; s = new string[] { "Pu", "Pu4+: *DS" }; break;
                   case 207: i = new int[] { 207, 94, 3 }; d = new double[] { 35.1736, 0.473204, 22.7181, 2.73848, 14.7635, 11.553, 2.28678, 20.9303, 13.0582 }; v = +6; s = new string[] { "Pu", "Pu6+: *DS" }; break;
                   case 208: i = new int[] { 208, 95, 0 }; d = new double[] { 36.6706, 0.483629, 24.0992, 3.20647, 17.3415, 14.3136, 3.49331, 102.273, 13.3592 }; v = 0; s = new string[] { "Am", "Am: *RHF" }; break;
                   case 209: i = new int[] { 209, 96, 0 }; d = new double[] { 36.6488, 0.465154, 24.4096, 3.08997, 17.399, 13.4346, 4.21665, 88.4834, 13.2887 }; v = 0; s = new string[] { "Cm", "Cm: *RHF" }; break;
                   case 210: i = new int[] { 210, 97, 0 }; d = new double[] { 36.7881, 0.451018, 24.7736, 3.04619, 17.8919, 12.8946, 4.23284, 86.003, 13.2754 }; v = 0; s = new string[] { "Bk", "Bk: *RHF" }; break;
                   case 211: i = new int[] { 211, 98, 0 }; d = new double[] { 36.9185, 0.437533, 25.1995, 3.00775, 18.3317, 12.4044, 4.24391, 83.7881, 13.2674 }; v = 0; s = new string[] { "Cf", "Cf: *RHF" }; break;

                   #endregion 原子散乱因子の係数クラス
               }
               X_A[0]=d[0];X_B[0]=d[1];X_A[1]=d[2];X_B[1]=d[3];
               X_A[2]=d[4];X_B[2]=d[5];X_A[3]=d[6];X_B[3]=d[7];
               X_C=d[8];
               ScatteringNumberForXray=i[0];
               AtomicNumber=i[1];
               SubNumberForXray=i[2];
               AtomicName=s[0];
               MethodForXray = s[1];
               ValenceForXray = v;
           }

           public void SetCoefficientForElectron(int atomicNumber, int subNumber)
           {
               if (atomicNumber < 0 || atomicNumber > E.Count - 1)
                   atomicNumber = subNumber = 0;
               else if (E[atomicNumber].Count <= subNumber)
                   subNumber = 0;

               int num = E[atomicNumber][subNumber];

               SetCoefficientForElectron(num);
           }

           public void SetCoefficientForElectron(int num)
           {
               int[] i = new int[0];
               double[] d = new double[0];
               string[] s = new string[0];
               int v = 0;
               switch (num)
               {
                   #region
                   //通し番号,原子番号,sub,a1,b1,a2,b2,a3,b3,a4,b4,c,Note
                   default: i = new int[] { 0, 0, 0 }; d = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; v = 0; s = new string[] { "Unkonwn", "Unkonwn" }; break;
                   case 1: i = new int[] { 1, 1, 0 }; d = new double[] { 0.0349, 0.1201, 0.197, 0.0573, 0.1195, 0.5347, 3.5867, 12.3471, 18.9525, 38.6269 }; v = 0; s = new string[] { "H", "H: HF" }; break;
                   case 2: i = new int[] { 2, 1, 1 }; d = new double[] { 0.14, 0.649, 1.37, 0.337, 0.787, 0.984, 8.67, 38.9, 111, 166 }; v = -1; s = new string[] { "H", "H1-: HF" }; break;
                   case 3: i = new int[] { 3, 2, 0 }; d = new double[] { 0.0317, 0.0838, 0.1526, 0.1334, 0.0164, 0.2507, 1.4751, 4.4938, 12.6646, 31.1653 }; v = 0; s = new string[] { "He", "He: RHF" }; break;
                   case 4: i = new int[] { 4, 3, 0 }; d = new double[] { 0.075, 0.2249, 0.5548, 1.4954, 0.9354, 0.3864, 2.9383, 15.3829, 53.5545, 138.7337 }; v = 0; s = new string[] { "Li", "Li: RHF" }; break;
                   case 5: i = new int[] { 5, 3, 1 }; d = new double[] { 0.0046, 0.0165, 0.0435, 0.0649, 0.027, 0.0358, 0.239, 0.879, 2.64, 7.09 }; v = +1; s = new string[] { "Li", "Li1+: RHF" }; break;
                   case 6: i = new int[] { 6, 4, 0 }; d = new double[] { 0.078, 0.221, 0.674, 1.3867, 0.6925, 0.3131, 2.2381, 10.1517, 30.9061, 78.3273 }; v = 0; s = new string[] { "Be", "Be: RHF" }; break;
                   case 7: i = new int[] { 7, 4, 1 }; d = new double[] { 0.0034, 0.0103, 0.0233, 0.0325, 0.012, 0.0267, 0.162, 0.531, 1.48, 3.88 }; v = +2; s = new string[] { "Be", "Be2+: RHF" }; break;
                   case 8: i = new int[] { 8, 5, 0 }; d = new double[] { 0.0909, 0.2551, 0.7738, 1.2136, 0.4606, 0.2995, 2.1155, 8.3816, 24.1292, 63.1314 }; v = 0; s = new string[] { "B", "B: RHF" }; break;
                   case 9: i = new int[] { 9, 6, 0 }; d = new double[] { 0.0893, 0.2563, 0.757, 1.0487, 0.3575, 0.2465, 1.71, 6.4094, 18.6113, 50.2523 }; v = 0; s = new string[] { "C", "C: RHF" }; break;
                   case 10: i = new int[] { 10, 7, 0 }; d = new double[] { 0.1022, 0.3219, 0.7982, 0.8197, 0.1715, 0.2451, 1.7481, 6.1925, 17.3894, 48.1431 }; v = 0; s = new string[] { "N", "N: RHF" }; break;
                   case 11: i = new int[] { 11, 8, 0 }; d = new double[] { 0.0974, 0.2921, 0.691, 0.699, 0.2039, 0.2067, 1.3815, 4.6943, 12.7105, 32.4726 }; v = 0; s = new string[] { "O", "O: RHF" }; break;
                   case 12: i = new int[] { 12, 8, 1 }; d = new double[] { 0.205, 0.628, 1.17, 1.03, 0.29, 0.397, 2.64, 8.8, 27.1, 91.8 }; v = -1; s = new string[] { "O", "O1-: HF" }; break;
                   case 13: i = new int[] { 13, 8, 2 }; d = new double[] { 0.0421, 0.21, 0.852, 1.82, 1.17, 0.0609, 0.559, 2.96, 11.5, 37.7 }; v = -2; s = new string[] { "O", "O2-: HF" }; break;
                   case 14: i = new int[] { 14, 9, 0 }; d = new double[] { 0.1083, 0.3175, 0.6487, 0.5846, 0.1421, 0.2057, 1.3439, 4.2788, 11.3932, 28.7881 }; v = 0; s = new string[] { "F", "F: RHF" }; break;
                   case 15: i = new int[] { 15, 9, 1 }; d = new double[] { 0.134, 0.391, 0.814, 0.928, 0.347, 0.228, 1.47, 4.68, 13.2, 36 }; v = -1; s = new string[] { "F", "F1-: RHF" }; break;
                   case 16: i = new int[] { 16, 10, 0 }; d = new double[] { 0.1269, 0.3535, 0.5582, 0.4674, 0.146, 0.22, 1.3779, 4.0203, 9.4934, 23.1278 }; v = 0; s = new string[] { "Ne", "Ne: RHF" }; break;
                   case 17: i = new int[] { 17, 11, 0 }; d = new double[] { 0.2142, 0.6853, 0.7692, 1.6589, 1.4482, 0.3334, 2.3446, 10.083, 48.3037, 138.27 }; v = 0; s = new string[] { "Na", "Na: RHF" }; break;
                   case 18: i = new int[] { 18, 11, 1 }; d = new double[] { 0.0256, 0.0919, 0.297, 0.514, 0.199, 0.0397, 0.287, 1.18, 3.75, 10.8 }; v = +1; s = new string[] { "Na", "Na1+: RHF" }; break;
                   case 19: i = new int[] { 19, 12, 0 }; d = new double[] { 0.2314, 0.6866, 0.9677, 2.1882, 1.1339, 0.3278, 2.272, 10.9241, 39.2898, 101.9748 }; v = 0; s = new string[] { "Mg", "Mg: RHF" }; break;
                   case 20: i = new int[] { 20, 12, 1 }; d = new double[] { 0.021, 0.0672, 0.198, 0.368, 0.174, 0.0331, 0.222, 0.838, 2.48, 6.75 }; v = +2; s = new string[] { "Mg", "Mg2+: HF" }; break;
                   case 21: i = new int[] { 21, 13, 0 }; d = new double[] { 0.239, 0.6573, 1.2011, 2.5586, 1.2312, 0.3138, 2.1063, 10.4163, 34.4552, 98.5344 }; v = 0; s = new string[] { "Al", "Al: RHF" }; break;
                   case 22: i = new int[] { 22, 13, 1 }; d = new double[] { 0.0192, 0.0579, 0.163, 0.284, 0.114, 0.0306, 0.198, 0.713, 2.04, 5.25 }; v = +3; s = new string[] { "Al", "Al3+: HF" }; break;
                   case 23: i = new int[] { 23, 14, 0 }; d = new double[] { 0.2519, 0.6372, 1.3795, 2.5082, 1.05, 0.3075, 2.0174, 9.6746, 29.3744, 80.4732 }; v = 0; s = new string[] { "Si", "Si: RHF" }; break;
                   case 24: i = new int[] { 24, 14, 1 }; d = new double[] { 0.192, 0.289, 0.1, -0.0728, 0.0012, 0.359, 1.96, 9.34, 11.1, 13.4 }; v = +4; s = new string[] { "Si", "Si4+: RHF" }; break;
                   case 25: i = new int[] { 25, 15, 0 }; d = new double[] { 0.2548, 0.6106, 1.4541, 2.3204, 0.8477, 0.2908, 1.874, 8.5176, 24.3434, 63.2996 }; v = 0; s = new string[] { "P", "P: RHF" }; break;
                   case 26: i = new int[] { 26, 16, 0 }; d = new double[] { 0.2497, 0.5628, 1.3899, 2.1865, 0.7715, 0.2681, 1.6711, 7.0267, 19.5377, 50.3888 }; v = 0; s = new string[] { "S", "S: RHF" }; break;
                   case 27: i = new int[] { 27, 17, 0 }; d = new double[] { 0.2443, 0.5397, 1.3919, 2.0197, 0.6621, 0.2468, 1.5242, 6.1537, 16.6687, 42.3086 }; v = 0; s = new string[] { "Cl", "Cl: RHF" }; break;
                   case 28: i = new int[] { 28, 17, 1 }; d = new double[] { 0.265, 0.596, 1.6, 2.69, 1.23, 0.252, 1.56, 6.21, 17.8, 47.8 }; v = -1; s = new string[] { "Cl", "Cl1-: RHF" }; break;
                   case 29: i = new int[] { 29, 18, 0 }; d = new double[] { 0.2385, 0.5017, 1.3428, 1.8899, 0.6079, 0.2289, 1.3694, 5.2561, 14.0928, 35.5361 }; v = 0; s = new string[] { "Ar", "Ar: RHF" }; break;
                   case 30: i = new int[] { 30, 19, 0 }; d = new double[] { 0.4115, 1.4031, 2.2784, 2.6742, 2.2162, 0.3703, 3.3874, 13.1029, 68.9592, 194.4329 }; v = 0; s = new string[] { "K", "K: RHF" }; break;
                   case 31: i = new int[] { 31, 19, 1 }; d = new double[] { 0.199, 0.396, 0.928, 1.45, 0.45, 0.192, 1.1, 3.91, 9.75, 23.4 }; v = +1; s = new string[] { "K", "K1+: RHF" }; break;
                   case 32: i = new int[] { 32, 20, 0 }; d = new double[] { 0.4054, 1.388, 2.1602, 3.7532, 2.2063, 0.3499, 3.0991, 11.9608, 53.9353, 142.3892 }; v = 0; s = new string[] { "Ca", "Ca: RHF" }; break;
                   case 33: i = new int[] { 33, 20, 1 }; d = new double[] { 0.164, 0.327, 0.743, 1.16, 0.307, 0.157, 0.894, 3.15, 7.67, 17.7 }; v = +2; s = new string[] { "Ca", "Ca2+: RHF" }; break;
                   case 34: i = new int[] { 34, 21, 0 }; d = new double[] { 0.3787, 1.2181, 2.0594, 3.2618, 2.387, 0.3133, 2.5856, 9.5813, 41.7688, 116.7282 }; v = 0; s = new string[] { "Se", "Se: RHF" }; break;
                   case 35: i = new int[] { 35, 21, 1 }; d = new double[] { 0.163, 0.307, 0.716, 0.88, 0.139, 0.157, 0.899, 3.06, 7.05, 16.1 }; v = +3; s = new string[] { "Sc", "Sc3+: HF" }; break;
                   case 36: i = new int[] { 36, 22, 0 }; d = new double[] { 0.3825, 1.2598, 2.0008, 3.0617, 2.0694, 0.304, 2.4863, 9.2783, 39.0751, 109.4583 }; v = 0; s = new string[] { "Ti", "Ti: RHF" }; break;
                   case 37: i = new int[] { 37, 22, 1 }; d = new double[] { 0.399, 1.04, 1.21, -0.0797, 0.352, 0.376, 2.74, 8.1, 14.2, 23.2 }; v = +2; s = new string[] { "Ti", "Ti2+: HF" }; break;
                   case 38: i = new int[] { 38, 22, 2 }; d = new double[] { 0.364, 0.919, 1.35, -0.933, 0.589, 0.364, 2.67, 8.18, 11.8, 14.9 }; v = +3; s = new string[] { "Ti", "Ti3+: HF" }; break;
                   case 39: i = new int[] { 39, 22, 3 }; d = new double[] { 0.116, 0.256, 0.565, 0.772, 0.132, 0.108, 0.655, 2.38, 5.51, 12.3 }; v = +4; s = new string[] { "Ti", "Ti4+: HF" }; break;
                   case 40: i = new int[] { 40, 23, 0 }; d = new double[] { 0.3876, 1.275, 1.9109, 2.8314, 1.8979, 0.2967, 2.378, 8.7981, 35.9528, 101.7201 }; v = 0; s = new string[] { "V", "V: RHF" }; break;
                   case 41: i = new int[] { 41, 23, 1 }; d = new double[] { 0.317, 0.939, 1.49, -1.31, 1.47, 0.269, 2.09, 7.22, 15.2, 17.6 }; v = +2; s = new string[] { "V", "V2+: RHF" }; break;
                   case 42: i = new int[] { 42, 23, 2 }; d = new double[] { 0.341, 0.805, 0.942, 0.0783, 0.156, 0.321, 2.23, 5.99, 13.4, 16.9 }; v = +3; s = new string[] { "V", "V3+: HF" }; break;
                   case 43: i = new int[] { 43, 23, 3 }; d = new double[] { 0.0367, 0.124, 0.244, 0.723, 0.435, 0.033, 0.222, 0.824, 2.8, 6.7 }; v = +5; s = new string[] { "V", "V5+: HF" }; break;
                   case 44: i = new int[] { 44, 24, 0 }; d = new double[] { 0.4046, 1.3696, 1.8941, 2.08, 1.2196, 0.2986, 2.3958, 9.1406, 37.4701, 113.7121 }; v = 0; s = new string[] { "Cr", "Cr: RHF" }; break;
                   case 45: i = new int[] { 45, 24, 1 }; d = new double[] { 0.237, 0.634, 1.23, 0.713, 0.0859, 0.177, 1.35, 4.3, 12.2, 39 }; v = +2; s = new string[] { "Cr", "Cr2+: HF" }; break;
                   case 46: i = new int[] { 46, 24, 2 }; d = new double[] { 0.393, 1.05, 1.62, -1.15, 0.407, 0.359, 2.57, 8.68, 11, 15.8 }; v = +3; s = new string[] { "Cr", "Cr3+: HF" }; break;
                   case 47: i = new int[] { 47, 24, 3 }; d = new double[] { 0.132, 0.292, 0.703, 0.692, 0.0959, 0.109, 0.695, 2.39, 5.65, 14.7 }; v = +4; s = new string[] { "Cr", "Cr4+: HF" }; break;
                   case 48: i = new int[] { 48, 25, 0 }; d = new double[] { 0.3796, 1.2094, 1.7815, 2.542, 1.5937, 0.2699, 2.0455, 7.4726, 31.0604, 91.5622 }; v = 0; s = new string[] { "Mn", "Mn: RHF" }; break;
                   case 49: i = new int[] { 49, 25, 1 }; d = new double[] { 0.0576, 0.21, 0.604, 1.32, 0.659, 0.0398, 0.284, 1.29, 4.23, 14.5 }; v = +2; s = new string[] { "Mn", "Mn2+: RHF" }; break;
                   case 50: i = new int[] { 50, 25, 2 }; d = new double[] { 0.116, 0.523, 0.881, 0.589, 0.214, 0.0117, 0.876, 3.06, 6.44, 14.3 }; v = +3; s = new string[] { "Mn", "Mn3+: HF" }; break;
                   case 51: i = new int[] { 51, 25, 3 }; d = new double[] { 0.381, 1.83, -1.33, 0.995, 0.0618, 0.354, 2.72, 3.47, 5.47, 16.1 }; v = +4; s = new string[] { "Mn", "Mn4+: HF" }; break;
                   case 52: i = new int[] { 52, 26, 0 }; d = new double[] { 0.3946, 1.2725, 1.7031, 2.314, 1.4795, 0.2717, 2.0443, 7.6007, 29.9714, 86.2265 }; v = 0; s = new string[] { "Fe", "Fe: RHF" }; break;
                   case 53: i = new int[] { 53, 26, 1 }; d = new double[] { 0.307, 0.838, 1.11, 0.28, 0.277, 0.23, 1.62, 4.87, 10.7, 19.2 }; v = +2; s = new string[] { "Fe", "Fe2+: RHF" }; break;
                   case 54: i = new int[] { 54, 26, 2 }; d = new double[] { 0.198, 0.387, 0.889, 0.709, 0.117, 0.154, 0.893, 2.62, 6.65, 18 }; v = +3; s = new string[] { "Fe", "Fe3+: RHF" }; break;
                   case 55: i = new int[] { 55, 27, 0 }; d = new double[] { 0.4118, 1.3161, 1.6493, 2.193, 1.283, 0.2742, 2.0372, 7.7205, 29.968, 84.9383 }; v = 0; s = new string[] { "Co", "Co: RHF" }; break;
                   case 56: i = new int[] { 56, 27, 1 }; d = new double[] { 0.213, 0.488, 0.998, 0.828, 0.23, 0.148, 0.939, 2.78, 7.31, 20.7 }; v = +2; s = new string[] { "Co", "Co2+: RHF" }; break;
                   case 57: i = new int[] { 57, 27, 2 }; d = new double[] { 0.331, 0.487, 0.729, 0.608, 0.131, 0.267, 1.41, 2.89, 6.45, 15.8 }; v = +3; s = new string[] { "Co", "Co3+: HF" }; break;
                   case 58: i = new int[] { 58, 28, 0 }; d = new double[] { 0.386, 1.1765, 1.5451, 2.073, 1.3814, 0.2478, 1.766, 6.3107, 25.2204, 74.3146 }; v = 0; s = new string[] { "Ni", "Ni: RHF" }; break;
                   case 59: i = new int[] { 59, 28, 1 }; d = new double[] { 0.338, 0.982, 1.32, -3.56, 3.62, 0.237, 1.67, 5.73, 11.4, 12.1 }; v = +2; s = new string[] { "Ni", "Ni2+: RHF" }; break;
                   case 60: i = new int[] { 60, 28, 2 }; d = new double[] { 0.347, 0.877, 0.79, 0.0538, 0.192, 0.26, 1.71, 4.75, 7.51, 13 }; v = +3; s = new string[] { "Ni", "Ni3+: HF" }; break;
                   case 61: i = new int[] { 61, 29, 0 }; d = new double[] { 0.4314, 1.3208, 1.5236, 1.4671, 0.8562, 0.2694, 1.9223, 7.3474, 28.9892, 90.6246 }; v = 0; s = new string[] { "Cu", "Cu: RHF" }; break;
                   case 62: i = new int[] { 62, 29, 1 }; d = new double[] { 0.312, 0.812, 1.11, 0.794, 0.257, 0.201, 1.31, 3.8, 10.5, 28.2 }; v = +1; s = new string[] { "Cu", "Cu1+: RHF" }; break;
                   case 63: i = new int[] { 63, 29, 2 }; d = new double[] { 0.224, 0.544, 0.97, 0.727, 0.182, 0.145, 0.933, 2.69, 7.11, 19.4 }; v = +2; s = new string[] { "Cu", "Cu2+: HF" }; break;
                   case 64: i = new int[] { 64, 30, 0 }; d = new double[] { 0.4288, 1.2646, 1.4472, 1.8294, 1.0934, 0.2593, 1.7998, 6.75, 25.586, 73.5284 }; v = 0; s = new string[] { "Zn", "Zn: RHF" }; break;
                   case 65: i = new int[] { 65, 30, 1 }; d = new double[] { 0.252, 0.6, 0.917, 0.663, 0.161, 0.161, 1.01, 2.76, 7.08, 19 }; v = +2; s = new string[] { "Zn", "Zn2+: RHF" }; break;
                   case 66: i = new int[] { 66, 31, 0 }; d = new double[] { 0.4818, 1.4032, 1.6561, 2.4605, 1.1054, 0.2825, 1.9785, 8.7546, 32.5238, 98.5523 }; v = 0; s = new string[] { "Ga", "Ga: RHF" }; break;
                   case 67: i = new int[] { 67, 31, 1 }; d = new double[] { 0.391, 0.947, 0.69, 0.0709, 0.0653, 0.264, 1.65, 4.82, 10.7, 15.2 }; v = +3; s = new string[] { "Ga", "Ga3+: HF" }; break;
                   case 68: i = new int[] { 68, 32, 0 }; d = new double[] { 0.4655, 1.3014, 1.6088, 2.6998, 1.3003, 0.2647, 1.7926, 7.6071, 26.5541, 77.5238 }; v = 0; s = new string[] { "Ge", "Ge: RHF" }; break;
                   case 69: i = new int[] { 69, 32, 1 }; d = new double[] { 0.346, 0.83, 0.599, 0.0949, -0.0217, 0.232, 1.45, 4.08, 13.2, 29.5 }; v = +4; s = new string[] { "Ge", "Ge4+: HF" }; break;
                   case 70: i = new int[] { 70, 33, 0 }; d = new double[] { 0.4517, 1.2229, 1.5852, 2.7958, 1.2638, 0.2493, 1.6436, 6.8154, 22.3681, 62.039 }; v = 0; s = new string[] { "As", "As: RHF" }; break;
                   case 71: i = new int[] { 71, 34, 0 }; d = new double[] { 0.4477, 1.1678, 1.5843, 2.8087, 1.1956, 0.2405, 1.5442, 6.3231, 19.461, 52.0233 }; v = 0; s = new string[] { "Se", "Se: RHF" }; break;
                   case 72: i = new int[] { 72, 35, 0 }; d = new double[] { 0.4798, 1.1948, 1.8695, 2.6953, 0.8203, 0.2504, 1.5963, 6.9653, 19.8492, 50.3233 }; v = 0; s = new string[] { "Br", "Br: RHF" }; break;
                   case 73: i = new int[] { 73, 35, 1 }; d = new double[] { 0.125, 0.563, 1.43, 3.52, 3.22, 0.053, 0.469, 2.15, 11.1, 38.9 }; v = -1; s = new string[] { "Br", "Br1-: RHF" }; break;
                   case 74: i = new int[] { 74, 36, 0 }; d = new double[] { 0.4546, 1.0993, 1.7696, 2.7068, 0.8672, 0.2309, 1.4279, 5.9449, 16.6752, 42.2243 }; v = 0; s = new string[] { "Kr", "Kr: RHF" }; break;
                   case 75: i = new int[] { 75, 37, 0 }; d = new double[] { 1.016, 2.8528, 3.5466, -7.7804, 12.1148, 0.4853, 5.0925, 25.7851, 130.4515, 138.6775 }; v = 0; s = new string[] { "Rb", "Rb: RHF" }; break;
                   case 76: i = new int[] { 76, 37, 1 }; d = new double[] { 0.368, 0.884, 1.14, 2.26, 0.881, 0.187, 1.12, 3.98, 10.9, 26.6 }; v = +1; s = new string[] { "Rb", "Rb1+: RHF" }; break;
                   case 77: i = new int[] { 77, 38, 0 }; d = new double[] { 0.6703, 1.4926, 3.3368, 4.46, 3.1501, 0.319, 2.2287, 10.3504, 52.3291, 151.2216 }; v = 0; s = new string[] { "Sr", "Sr: RHF" }; break;
                   case 78: i = new int[] { 78, 38, 1 }; d = new double[] { 0.346, 0.804, 0.988, 1.89, 0.609, 0.176, 1.04, 3.59, 9.32, 21.4 }; v = +2; s = new string[] { "Sr", "Sr2+: RHF" }; break;
                   case 79: i = new int[] { 79, 39, 0 }; d = new double[] { 0.6894, 1.5474, 3.245, 4.2126, 2.9764, 0.3189, 2.2904, 10.0062, 44.0771, 125.012 }; v = 0; s = new string[] { "y", "y: *RHF" }; break;
                   case 80: i = new int[] { 80, 39, 1 }; d = new double[] { 0.465, 0.923, 2.41, -2.31, 2.48, 0.24, 1.43, 6.45, 9.97, 12.2 }; v = +2; s = new string[] { "Y", "Y2+: *DS" }; break;
                   case 81: i = new int[] { 81, 40, 0 }; d = new double[] { 0.6719, 1.4684, 3.1668, 3.9557, 2.892, 0.3036, 2.1249, 8.9236, 36.8458, 108.2049 }; v = 0; s = new string[] { "Zr", "Zr: *RHF" }; break;
                   case 82: i = new int[] { 82, 40, 1 }; d = new double[] { 0.234, 0.642, 0.747, 1.47, 0.377, 0.113, 0.736, 2.54, 6.72, 14.7 }; v = +4; s = new string[] { "Zr", "Zr4+: *DS" }; break;
                   case 83: i = new int[] { 83, 41, 0 }; d = new double[] { 0.6123, 1.2677, 3.0348, 3.3841, 2.3683, 0.2709, 1.7683, 7.2489, 27.9465, 98.5624 }; v = 0; s = new string[] { "Nb", "Nb: *RHF" }; break;
                   case 84: i = new int[] { 84, 41, 1 }; d = new double[] { 0.377, 0.749, 1.29, 1.61, 0.481, 0.184, 1.02, 3.8, 9.44, 25.7 }; v = +3; s = new string[] { "Nb", "Nb3+: *DS" }; break;
                   case 85: i = new int[] { 85, 41, 2 }; d = new double[] { 0.0828, 0.271, 0.654, 1.24, 0.829, 0.0369, 0.261, 0.957, 3.94, 9.44 }; v = +5; s = new string[] { "Nb", "Nb5+: *DS" }; break;
                   case 86: i = new int[] { 86, 42, 0 }; d = new double[] { 0.6773, 1.4798, 3.1788, 3.0824, 1.8384, 0.292, 2.0606, 8.1129, 30.5336, 100.0658 }; v = 0; s = new string[] { "Mo", "Mo: *RHF" }; break;
                   case 87: i = new int[] { 87, 42, 1 }; d = new double[] { 0.401, 0.756, 1.38, 1.58, 0.497, 0.191, 1.06, 3.84, 9.38, 24.6 }; v = +3; s = new string[] { "Mo", "Mo3+: *DS" }; break;
                   case 88: i = new int[] { 88, 42, 2 }; d = new double[] { 0.479, 0.846, 15.6, -15.2, 1.6, 0.241, 1.46, 6.79, 7.13, 10.4 }; v = +5; s = new string[] { "Mo", "Mo5+: *DS" }; break;
                   case 89: i = new int[] { 89, 42, 3 }; d = new double[] { 0.203, 0.567, 0.646, 1.16, 0.171, 0.0971, 0.647, 2.28, 5.61, 12.4 }; v = +6; s = new string[] { "Mo", "Mo6+: *DS" }; break;
                   case 90: i = new int[] { 90, 43, 0 }; d = new double[] { 0.7082, 1.6392, 3.1993, 3.4327, 1.8711, 0.2976, 2.2106, 8.5246, 33.1456, 96.6377 }; v = 0; s = new string[] { "Te", "Te: *RHF" }; break;
                   case 91: i = new int[] { 91, 44, 0 }; d = new double[] { 0.6735, 1.4934, 3.0966, 2.7254, 1.5597, 0.2773, 1.9716, 7.3249, 26.6891, 90.5581 }; v = 0; s = new string[] { "Ru", "Ru: *RHF" }; break;
                   case 92: i = new int[] { 92, 44, 1 }; d = new double[] { 0.428, 0.773, 1.55, 1.46, 0.486, 0.191, 1.09, 3.82, 9.08, 21.7 }; v = +3; s = new string[] { "Ru", "Ru3+: *DS" }; break;
                   case 93: i = new int[] { 93, 44, 2 }; d = new double[] { 0.282, 0.653, 1.14, 1.53, 0.418, 0.125, 0.753, 2.85, 7.01, 17.5 }; v = +4; s = new string[] { "Ru", "Ru4+: *DS" }; break;
                   case 94: i = new int[] { 94, 45, 0 }; d = new double[] { 0.6413, 1.369, 2.9854, 2.6952, 1.5433, 0.258, 1.7721, 6.3854, 23.2549, 85.1517 }; v = 0; s = new string[] { "Rh", "Rh: *RHF" }; break;
                   case 95: i = new int[] { 95, 45, 1 }; d = new double[] { 0.352, 0.723, 1.5, 1.63, 0.499, 0.151, 0.878, 3.28, 8.16, 20.7 }; v = +3; s = new string[] { "Rh", "Rh3+: *DS" }; break;
                   case 96: i = new int[] { 96, 45, 2 }; d = new double[] { 0.397, 0.725, 1.51, 1.19, 0.251, 0.177, 1.01, 3.62, 8.56, 18.9 }; v = +4; s = new string[] { "Rh", "Rh4+: *DS" }; break;
                   case 97: i = new int[] { 97, 46, 0 }; d = new double[] { 0.5904, 1.1775, 2.6519, 2.2875, 0.8689, 0.2324, 1.5019, 5.1591, 15.5428, 46.8213 }; v = 0; s = new string[] { "Pd", "Pd: *RHF" }; break;
                   case 98: i = new int[] { 98, 46, 1 }; d = new double[] { 0.935, 3.11, 24.6, -43.6, 21.1, 0.393, 4.06, 43.1, 54, 69.8 }; v = +2; s = new string[] { "Pd", "Pd2+: *DS" }; break;
                   case 99: i = new int[] { 99, 46, 2 }; d = new double[] { 0.348, 0.64, 1.22, 1.45, 0.427, 0.151, 0.832, 2.85, 6.59, 15.6 }; v = +4; s = new string[] { "Pd", "Pd4+: *DS" }; break;
                   case 100: i = new int[] { 100, 47, 0 }; d = new double[] { 0.6377, 1.379, 2.8294, 2.3631, 1.4553, 0.2466, 1.6974, 5.7656, 20.0943, 76.7372 }; v = 0; s = new string[] { "Ag", "Ag: RHF" }; break;
                   case 101: i = new int[] { 101, 47, 1 }; d = new double[] { 0.503, 0.94, 2.17, 1.99, 0.726, 0.199, 1.19, 4.05, 11.3, 32.4 }; v = +1; s = new string[] { "Ag", "Ag1+: *DS" }; break;
                   case 102: i = new int[] { 102, 47, 2 }; d = new double[] { 0.431, 0.756, 1.72, 1.78, 0.526, 0.175, 0.979, 3.3, 8.24, 21.4 }; v = +2; s = new string[] { "Ag", "Ag2+: *DS" }; break;
                   case 103: i = new int[] { 103, 48, 0 }; d = new double[] { 0.6364, 1.4247, 2.7802, 2.5973, 1.7886, 0.2407, 1.6823, 5.6588, 20.7219, 69.1109 }; v = 0; s = new string[] { "Cd", "Cd: RHF" }; break;
                   case 104: i = new int[] { 104, 48, 1 }; d = new double[] { 0.425, 0.745, 1.73, 1.74, 0.487, 0.168, 0.944, 3.14, 7.84, 20.4 }; v = +2; s = new string[] { "Cd", "Cd2+: *DS" }; break;
                   case 105: i = new int[] { 105, 49, 0 }; d = new double[] { 0.6768, 1.6589, 2.774, 3.1835, 2.1326, 0.2522, 1.8545, 6.2936, 25.1457, 84.5448 }; v = 0; s = new string[] { "In", "In: RHF" }; break;
                   case 106: i = new int[] { 106, 49, 1 }; d = new double[] { 0.417, 0.755, 1.59, 1.36, 0.451, 0.164, 0.96, 3.08, 7.03, 16.1 }; v = +3; s = new string[] { "In", "In3+: *DS" }; break;
                   case 107: i = new int[] { 107, 50, 0 }; d = new double[] { 0.7224, 1.961, 2.7161, 3.5603, 1.8972, 0.2651, 2.0604, 7.3011, 27.5493, 81.3349 }; v = 0; s = new string[] { "Sn", "Sn: RHF" }; break;
                   case 108: i = new int[] { 108, 50, 1 }; d = new double[] { 0.797, 2.13, 2.15, -1.64, 2.72, 0.317, 2.51, 9.04, 24.2, 26.4 }; v = +2; s = new string[] { "Sn", "Sn2+: RHF" }; break;
                   case 109: i = new int[] { 109, 50, 2 }; d = new double[] { 0.261, 0.642, 1.53, 1.36, 0.177, 0.0957, 0.625, 2.51, 6.31, 15.9 }; v = +4; s = new string[] { "Sn", "Sn4+: RHF" }; break;
                   case 110: i = new int[] { 110, 51, 0 }; d = new double[] { 0.7106, 1.9247, 2.6149, 3.8322, 1.8899, 0.2562, 1.9646, 6.8852, 24.7648, 68.9168 }; v = 0; s = new string[] { "Sb", "Sb: RHF" }; break;
                   case 111: i = new int[] { 111, 51, 1 }; d = new double[] { 0.552, 1.14, 1.87, 1.36, 0.414, 0.212, 1.42, 4.21, 12.5, 29 }; v = +3; s = new string[] { "Sb", "Sb3+: *DS" }; break;
                   case 112: i = new int[] { 112, 51, 2 }; d = new double[] { 0.377, 0.588, 1.22, 1.18, 0.244, 0.151, 0.812, 2.4, 5.27, 11.9 }; v = +5; s = new string[] { "Sb", "Sb5+: *DS" }; break;
                   case 113: i = new int[] { 113, 52, 0 }; d = new double[] { 0.6947, 1.869, 2.5356, 4.0013, 1.8955, 0.2459, 1.8542, 6.4411, 22.173, 59.2206 }; v = 0; s = new string[] { "Te", "Te: *RHF" }; break;
                   case 114: i = new int[] { 114, 53, 0 }; d = new double[] { 0.7047, 1.9484, 2.594, 4.1526, 1.5057, 0.2455, 1.8638, 6.7639, 21.8007, 56.4395 }; v = 0; s = new string[] { "I", "I: RHF" }; break;
                   case 115: i = new int[] { 115, 53, 1 }; d = new double[] { 0.901, 2.8, 5.61, -8.69, 12.6, 0.312, 2.59, 14.1, 34.4, 39.5 }; v = +1; s = new string[] { "I", "I1+: RHF" }; break;
                   case 116: i = new int[] { 116, 54, 0 }; d = new double[] { 0.6737, 1.7908, 2.4129, 4.21, 1.7058, 0.2305, 1.689, 5.8218, 18.3928, 47.2496 }; v = 0; s = new string[] { "Xe", "Xe: RHF" }; break;
                   case 117: i = new int[] { 117, 55, 0 }; d = new double[] { 1.2704, 3.8018, 5.6618, 0.9205, 4.8105, 0.4356, 4.2058, 23.4342, 136.7783, 171.7561 }; v = 0; s = new string[] { "Cs", "Cs: RHF" }; break;
                   case 118: i = new int[] { 118, 55, 1 }; d = new double[] { 0.587, 1.4, 1.87, 3.48, 1.67, 0.2, 1.38, 4.12, 13, 31.8 }; v = +1; s = new string[] { "Cs", "Cs1+: RHF" }; break;
                   case 119: i = new int[] { 119, 56, 0 }; d = new double[] { 0.9049, 2.6076, 4.8498, 5.1603, 4.7388, 0.3066, 2.4363, 12.1821, 54.6135, 161.9978 }; v = 0; s = new string[] { "Ba", "Ba: RHF" }; break;
                   case 120: i = new int[] { 120, 56, 1 }; d = new double[] { 0.733, 2.05, 23, -152, 134, 0.258, 1.96, 11.8, 14.4, 14.9 }; v = +2; s = new string[] { "Ba", "Ba2+: *DS" }; break;
                   case 121: i = new int[] { 121, 57, 0 }; d = new double[] { 0.8405, 2.3863, 4.6139, 5.1514, 4.7949, 0.2791, 2.141, 10.34, 41.9148, 132.0204 }; v = 0; s = new string[] { "La", "La: *RHF" }; break;
                   case 122: i = new int[] { 122, 57, 1 }; d = new double[] { 0.493, 1.1, 1.5, 2.7, 1.08, 0.167, 1.11, 3.11, 9.61, 21.2 }; v = +3; s = new string[] { "La", "La3+: *DS" }; break;
                   case 123: i = new int[] { 123, 58, 0 }; d = new double[] { 0.8551, 2.3915, 4.5772, 5.0278, 4.5118, 0.2805, 2.12, 10.1808, 42.0633, 130.9893 }; v = 0; s = new string[] { "Ce", "Ce: *RHF" }; break;
                   case 124: i = new int[] { 124, 58, 1 }; d = new double[] { 0.56, 1.35, 1.59, 2.63, 0.706, 0.19, 1.3, 3.93, 10.7, 23.8 }; v = +3; s = new string[] { "Ce", "Ce3+: *DS" }; break;
                   case 125: i = new int[] { 125, 58, 2 }; d = new double[] { 0.483, 1.09, 1.34, 2.45, 0.797, 0.165, 1.1, 3.02, 8.85, 18.8 }; v = +4; s = new string[] { "Ce", "Ce4+: *DS" }; break;
                   case 126: i = new int[] { 126, 59, 0 }; d = new double[] { 0.9096, 2.5313, 4.5266, 4.6376, 4.369, 0.2939, 2.2471, 10.8266, 48.8842, 147.602 }; v = 0; s = new string[] { "Pr", "Pr: *RMF" }; break;
                   case 127: i = new int[] { 127, 59, 1 }; d = new double[] { 0.663, 1.73, 2.35, 0.351, 1.59, 0.226, 1.61, 6.33, 11, 16.9 }; v = +3; s = new string[] { "Pr", "Pr3+: *DS" }; break;
                   case 128: i = new int[] { 128, 59, 2 }; d = new double[] { 0.521, 1.19, 1.33, 2.36, 0.69, 0.177, 1.17, 3.28, 8.94, 19.3 }; v = +4; s = new string[] { "Pr", "Pr4+: *DS" }; break;
                   case 129: i = new int[] { 129, 60, 0 }; d = new double[] { 0.8807, 2.4183, 4.4448, 4.6858, 4.1725, 0.2802, 2.0836, 10.0357, 47.4506, 146.9976 }; v = 0; s = new string[] { "Nd", "Nd: *RHF" }; break;
                   case 130: i = new int[] { 130, 60, 1 }; d = new double[] { 0.501, 1.18, 1.45, 2.53, 0.92, 0.162, 1.08, 3.06, 8.8, 19.6 }; v = +3; s = new string[] { "Nd", "Nd3+: *DS" }; break;
                   case 131: i = new int[] { 131, 61, 0 }; d = new double[] { 0.9471, 2.5463, 4.3523, 4.4789, 3.908, 0.2977, 2.2276, 10.5762, 49.3619, 145.358 }; v = 0; s = new string[] { "Pm", "Pm: *RHF" }; break;
                   case 132: i = new int[] { 132, 61, 1 }; d = new double[] { 0.496, 1.2, 1.47, 2.43, 0.943, 0.156, 1.05, 3.07, 8.56, 19.2 }; v = +3; s = new string[] { "Pm", "Pm3+: *DS" }; break;
                   case 133: i = new int[] { 133, 62, 0 }; d = new double[] { 0.9699, 2.5837, 4.2778, 4.4575, 3.5985, 0.3003, 2.2447, 10.6487, 50.7994, 146.4179 }; v = 0; s = new string[] { "Srn", "Srn: *RHF" }; break;
                   case 134: i = new int[] { 134, 62, 1 }; d = new double[] { 0.518, 1.24, 1.43, 2.4, 0.781, 0.163, 1.08, 3.11, 8.52, 19.1 }; v = +3; s = new string[] { "Sm", "Sm3+: *DS" }; break;
                   case 135: i = new int[] { 135, 63, 0 }; d = new double[] { 0.8694, 2.2413, 3.9196, 3.9694, 4.5498, 0.2653, 1.859, 8.3998, 36.7397, 125.7089 }; v = 0; s = new string[] { "Eu", "Eu: RHF" }; break;
                   case 136: i = new int[] { 136, 63, 1 }; d = new double[] { 0.613, 1.53, 1.84, 2.46, 0.714, 0.19, 1.27, 4.18, 10.7, 26.2 }; v = +2; s = new string[] { "Eu", "Eu2+: *DS" }; break;
                   case 137: i = new int[] { 137, 63, 2 }; d = new double[] { 0.496, 1.21, 1.45, 2.36, 0.774, 0.152, 1.01, 2.95, 8.18, 18.5 }; v = +3; s = new string[] { "Eu", "Eu3+: *DS" }; break;
                   case 138: i = new int[] { 138, 64, 0 }; d = new double[] { 0.9673, 2.4702, 4.1148, 4.4972, 3.2099, 0.2909, 2.1014, 9.7067, 43.427, 125.9474 }; v = 0; s = new string[] { "Gd", "Gd: *RHF" }; break;
                   case 139: i = new int[] { 139, 64, 1 }; d = new double[] { 0.49, 1.19, 1.42, 2.3, 0.795, 0.148, 0.974, 2.81, 7.78, 17.7 }; v = +3; s = new string[] { "Gd", "Gd3+: *DS" }; break;
                   case 140: i = new int[] { 140, 65, 0 }; d = new double[] { 0.9325, 2.3673, 3.8791, 3.9674, 3.7996, 0.2761, 1.9511, 8.9296, 41.5937, 131.0122 }; v = 0; s = new string[] { "Tb", "Tb: *RHF" }; break;
                   case 141: i = new int[] { 141, 65, 1 }; d = new double[] { 0.503, 1.22, 1.42, 2.24, 0.71, 0.15, 0.982, 2.86, 7.77, 17.7 }; v = +3; s = new string[] { "Tb", "Tb3+: *DS" }; break;
                   case 142: i = new int[] { 142, 66, 0 }; d = new double[] { 0.9505, 2.3705, 3.8218, 4.0471, 3.4451, 0.2773, 1.9469, 8.8862, 43.0938, 133.1396 }; v = 0; s = new string[] { "Dy", "Dy: *RHF" }; break;
                   case 143: i = new int[] { 143, 66, 1 }; d = new double[] { 0.503, 1.24, 1.44, 2.17, 0.643, 0.148, 0.97, 2.88, 7.73, 17.6 }; v = +3; s = new string[] { "Dy", "Dy3+: *DS" }; break;
                   case 144: i = new int[] { 144, 67, 0 }; d = new double[] { 0.9248, 2.2428, 3.6182, 3.791, 3.7912, 0.266, 1.8183, 7.9655, 33.1129, 101.8139 }; v = 0; s = new string[] { "Ho", "Ho: *RHF" }; break;
                   case 145: i = new int[] { 145, 67, 1 }; d = new double[] { 0.456, 1.17, 1.43, 2.15, 0.692, 0.129, 0.869, 2.61, 7.24, 16.7 }; v = +3; s = new string[] { "Ho", "Ho3+: *DS" }; break;
                   case 146: i = new int[] { 146, 68, 0 }; d = new double[] { 1.0373, 2.4824, 3.6558, 3.8925, 3.0056, 0.2944, 2.0797, 9.4156, 45.8056, 132.772 }; v = 0; s = new string[] { "Er", "Er: *RHF" }; break;
                   case 147: i = new int[] { 147, 68, 1 }; d = new double[] { 0.522, 1.28, 1.46, 2.05, 0.508, 0.15, 0.964, 2.93, 7.72, 17.8 }; v = +3; s = new string[] { "Er", "Er3+: *DS" }; break;
                   case 148: i = new int[] { 148, 69, 0 }; d = new double[] { 1.0075, 2.3787, 3.544, 3.6932, 3.1759, 0.2816, 1.9486, 8.7162, 41.842, 125.032 }; v = 0; s = new string[] { "Tm", "Tm: *RHF" }; break;
                   case 149: i = new int[] { 149, 69, 1 }; d = new double[] { 0.475, 1.2, 1.42, 2.05, 0.584, 0.132, 0.864, 2.6, 7.09, 16.6 }; v = +3; s = new string[] { "Tm", "Tm3+: *DS" }; break;
                   case 150: i = new int[] { 150, 70, 0 }; d = new double[] { 1.0347, 2.3911, 3.4619, 3.6556, 3.0052, 0.2855, 1.9679, 8.7619, 42.3304, 125.6499 }; v = 0; s = new string[] { "Yb", "Yb: *RHF" }; break;
                   case 151: i = new int[] { 151, 70, 1 }; d = new double[] { 0.508, 1.37, 1.76, 2.23, 0.584, 0.136, 0.922, 3.12, 8.72, 23.7 }; v = +2; s = new string[] { "Yb", "Yb2+: *DS" }; break;
                   case 152: i = new int[] { 152, 70, 2 }; d = new double[] { 0.498, 1.22, 1.39, 1.97, 0.559, 0.138, 0.881, 2.63, 6.99, 16.3 }; v = +3; s = new string[] { "Yb", "Yb3+: *DS" }; break;
                   case 153: i = new int[] { 153, 71, 0 }; d = new double[] { 0.9927, 2.2436, 3.3554, 3.7813, 3.0994, 0.2701, 1.8073, 7.8112, 34.4849, 103.3526 }; v = 0; s = new string[] { "Lu", "Lu: *RHF" }; break;
                   case 154: i = new int[] { 154, 71, 1 }; d = new double[] { 0.483, 1.21, 1.41, 1.94, 0.522, 0.131, 0.845, 2.57, 6.88, 16.2 }; v = +3; s = new string[] { "Lu", "Lu3+: *DS" }; break;
                   case 155: i = new int[] { 155, 72, 0 }; d = new double[] { 1.0295, 2.2911, 3.411, 3.9497, 2.4925, 0.2761, 1.8625, 8.0961, 34.2712, 98.5295 }; v = 0; s = new string[] { "Hf", "Hf: *RHF" }; break;
                   case 156: i = new int[] { 156, 72, 1 }; d = new double[] { 0.522, 1.22, 1.37, 1.68, 0.312, 0.145, 0.896, 2.74, 6.91, 16.1 }; v = +4; s = new string[] { "Hf", "Hf4+: *DS" }; break;
                   case 157: i = new int[] { 157, 73, 0 }; d = new double[] { 1.019, 2.2291, 3.4097, 3.9252, 2.2679, 0.2694, 1.7962, 7.6944, 31.0942, 91.1089 }; v = 0; s = new string[] { "Ta", "Ta: *RHF" }; break;
                   case 158: i = new int[] { 158, 73, 1 }; d = new double[] { 0.569, 1.26, 0.979, 1.29, 0.551, 0.161, 0.972, 2.76, 5.4, 10.9 }; v = +5; s = new string[] { "Ta", "Ta5+: *DS" }; break;
                   case 159: i = new int[] { 159, 74, 0 }; d = new double[] { 0.9853, 2.1167, 3.357, 3.7981, 2.2798, 0.2569, 1.6745, 7.0098, 26.9234, 81.391 }; v = 0; s = new string[] { "W", "W: *RHF" }; break;
                   case 160: i = new int[] { 160, 74, 1 }; d = new double[] { 0.181, 0.873, 1.18, 1.48, 0.562, 0.0118, 0.442, 1.52, 4.35, 9.42 }; v = +6; s = new string[] { "W", "W6+: *DS" }; break;
                   case 161: i = new int[] { 161, 75, 0 }; d = new double[] { 0.9914, 2.0858, 3.4531, 3.8812, 1.8526, 0.2548, 1.6518, 6.8845, 26.7234, 81.7215 }; v = 0; s = new string[] { "Re", "Re: *RHF" }; break;
                   case 162: i = new int[] { 162, 76, 0 }; d = new double[] { 0.9813, 2.0322, 3.3665, 3.6235, 1.9741, 0.2487, 1.5973, 6.4737, 23.2817, 70.9254 }; v = 0; s = new string[] { "Os", "Os: *RHF" }; break;
                   case 163: i = new int[] { 163, 76, 1 }; d = new double[] { 0.586, 1.31, 1.63, 1.71, 0.54, 0.155, 0.938, 3.19, 7.84, 19.3 }; v = +4; s = new string[] { "Os", "Os4+: *DS" }; break;
                   case 164: i = new int[] { 164, 77, 0 }; d = new double[] { 1.0194, 2.0645, 3.4425, 3.4914, 1.6976, 0.2554, 1.6475, 6.5966, 23.2269, 70.0272 }; v = 0; s = new string[] { "Ir", "Ir: *RHF" }; break;
                   case 165: i = new int[] { 165, 77, 1 }; d = new double[] { 0.692, 1.37, 1.8, 1.97, 0.804, 0.182, 1.04, 3.47, 8.51, 21.2 }; v = +3; s = new string[] { "Ir", "Ir3+: *DS" }; break;
                   case 166: i = new int[] { 166, 77, 2 }; d = new double[] { 0.653, 1.29, 1.5, 1.74, 0.683, 0.174, 0.992, 3.14, 7.22, 17.2 }; v = +4; s = new string[] { "Ir", "Ir4+: *DS" }; break;
                   case 167: i = new int[] { 167, 78, 0 }; d = new double[] { 0.9148, 1.8096, 3.2134, 3.2953, 1.5754, 0.2263, 1.3813, 5.3243, 17.5987, 60.0171 }; v = 0; s = new string[] { "Pt", "Pt: *RHF" }; break;
                   case 168: i = new int[] { 168, 78, 1 }; d = new double[] { 0.872, 1.68, 2.63, 1.93, 0.475, 0.223, 1.35, 4.99, 13.6, 33 }; v = +2; s = new string[] { "Pt", "Pt2+: *DS" }; break;
                   case 169: i = new int[] { 169, 78, 2 }; d = new double[] { 0.55, 1.21, 1.62, 1.95, 0.61, 0.142, 0.833, 2.81, 7.21, 17.7 }; v = +4; s = new string[] { "Pt", "Pt4+: *DS" }; break;
                   case 170: i = new int[] { 170, 79, 0 }; d = new double[] { 0.9674, 1.8916, 3.3993, 3.0524, 1.2607, 0.2358, 1.4712, 5.6758, 18.7119, 61.5286 }; v = 0; s = new string[] { "Au", "Au: RHF" }; break;
                   case 171: i = new int[] { 171, 79, 1 }; d = new double[] { 0.811, 1.57, 2.63, 2.68, 0.998, 0.201, 1.18, 4.25, 12.1, 34.4 }; v = +1; s = new string[] { "Au", "Au1+: *DS" }; break;
                   case 172: i = new int[] { 172, 79, 2 }; d = new double[] { 0.722, 1.39, 1.94, 1.94, 0.699, 0.184, 1.06, 3.58, 8.56, 20.4 }; v = +3; s = new string[] { "Au", "Au3+: *DS" }; break;
                   case 173: i = new int[] { 173, 80, 0 }; d = new double[] { 1.0033, 1.9469, 3.4396, 3.1548, 1.418, 0.2413, 1.5298, 5.8009, 19.452, 60.5753 }; v = 0; s = new string[] { "Hg", "Hg: RHF" }; break;
                   case 174: i = new int[] { 174, 80, 1 }; d = new double[] { 0.796, 1.56, 2.72, 2.76, 1.18, 0.194, 1.14, 4.21, 12.4, 36.2 }; v = +1; s = new string[] { "Hg", "Hg1+: *DS" }; break;
                   case 175: i = new int[] { 175, 80, 2 }; d = new double[] { 0.773, 1.49, 2.45, 2.23, 0.57, 0.191, 1.12, 4, 10.8, 27.6 }; v = +2; s = new string[] { "Hg", "Hg2+: *DS" }; break;
                   case 176: i = new int[] { 176, 81, 0 }; d = new double[] { 1.0689, 2.1038, 3.6039, 3.4927, 1.8283, 0.254, 1.6715, 6.3509, 23.1531, 78.7099 }; v = 0; s = new string[] { "TI", "TI: *RHF" }; break;
                   case 177: i = new int[] { 177, 81, 1 }; d = new double[] { 0.82, 1.57, 2.78, 2.82, 1.31, 0.197, 1.16, 4.23, 12.7, 35.7 }; v = +1; s = new string[] { "Tl", "Tl1+: *DS" }; break;
                   case 178: i = new int[] { 178, 81, 2 }; d = new double[] { 0.836, 1.43, 0.394, 2.51, 1.5, 0.208, 1.2, 2.57, 4.86, 13.5 }; v = +3; s = new string[] { "Tl", "Tl3+: *DS" }; break;
                   case 179: i = new int[] { 179, 82, 0 }; d = new double[] { 1.0891, 2.1867, 3.616, 3.8031, 1.8994, 0.2552, 1.7174, 6.5131, 23.917, 74.7039 }; v = 0; s = new string[] { "Pb", "Pb: RHF" }; break;
                   case 180: i = new int[] { 180, 82, 1 }; d = new double[] { 0.755, 1.44, 2.48, 2.45, 1.03, 0.181, 1.05, 3.75, 10.6, 27.9 }; v = +2; s = new string[] { "Pb", "Pb2+: *DS" }; break;
                   case 181: i = new int[] { 181, 82, 2 }; d = new double[] { 0.583, 1.14, 1.6, 2.06, 0.662, 0.144, 0.796, 2.58, 6.22, 14.8 }; v = +4; s = new string[] { "Pb", "Pb4+: *DS" }; break;
                   case 182: i = new int[] { 182, 83, 0 }; d = new double[] { 1.1007, 2.2306, 3.5689, 4.1549, 2.0382, 0.2546, 1.7351, 6.4948, 23.6464, 70.378 }; v = 0; s = new string[] { "Bi", "Bi: RHF" }; break;
                   case 183: i = new int[] { 183, 83, 1 }; d = new double[] { 0.708, 1.35, 2.28, 2.18, 0.797, 0.17, 0.981, 3.44, 9.41, 23.7 }; v = +3; s = new string[] { "Bi", "Bi3+: *DS" }; break;
                   case 184: i = new int[] { 184, 83, 2 }; d = new double[] { 0.654, 1.18, 1.25, 1.66, 0.778, 0.162, 0.905, 2.68, 5.14, 11.2 }; v = +5; s = new string[] { "Bi", "Bi5+: *DS" }; break;
                   case 185: i = new int[] { 185, 84, 0 }; d = new double[] { 1.1568, 2.4353, 3.6459, 4.4064, 1.7179, 0.2648, 1.8786, 7.1749, 25.1766, 69.2821 }; v = 0; s = new string[] { "Po", "Po: *RHF" }; break;
                   case 186: i = new int[] { 186, 85, 0 }; d = new double[] { 1.0909, 2.1976, 3.3831, 4.67, 2.1277, 0.2466, 1.6707, 6.0197, 20.7657, 57.2663 }; v = 0; s = new string[] { "At", "At: *RHF" }; break;
                   case 187: i = new int[] { 187, 86, 0 }; d = new double[] { 1.0756, 2.163, 3.3178, 4.8852, 2.0489, 0.2402, 1.6169, 5.7644, 19.4568, 52.5009 }; v = 0; s = new string[] { "Rn", "Rn: RHF" }; break;
                   case 188: i = new int[] { 188, 87, 0 }; d = new double[] { 1.4282, 3.5081, 5.6767, 4.1964, 3.8946, 0.3183, 2.6889, 13.4816, 54.3866, 200.8321 }; v = 0; s = new string[] { "Fr", "Fr: *RHF" }; break;
                   case 189: i = new int[] { 189, 88, 0 }; d = new double[] { 1.3127, 3.1243, 5.2988, 5.3891, 5.4133, 0.2887, 2.2897, 10.8276, 43.5389, 145.6109 }; v = 0; s = new string[] { "Ra", "Ra: *RHF" }; break;
                   case 190: i = new int[] { 190, 88, 1 }; d = new double[] { 0.911, 1.65, 2.53, 3.62, 1.58, 0.204, 1.26, 4.03, 12.6, 30 }; v = +2; s = new string[] { "Ra", "Ra2+: *DS" }; break;
                   case 191: i = new int[] { 191, 89, 0 }; d = new double[] { 1.3128, 3.1021, 5.3385, 5.9611, 4.7562, 0.2861, 2.2509, 10.5287, 41.7796, 128.2973 }; v = 0; s = new string[] { "Ac", "Ac: *RHF" }; break;
                   case 192: i = new int[] { 192, 89, 1 }; d = new double[] { 0.915, 1.64, 2.26, 3.18, 1.25, 0.205, 1.28, 3.92, 11.3, 25.1 }; v = +3; s = new string[] { "Ac", "Ac3+: *DS" }; break;
                   case 193: i = new int[] { 193, 90, 0 }; d = new double[] { 1.2553, 2.9178, 5.0862, 6.1206, 4.7122, 0.2701, 2.0636, 9.3051, 34.5977, 107.92 }; v = 0; s = new string[] { "Th", "Th: *RHF" }; break;
                   case 194: i = new int[] { 194, 91, 0 }; d = new double[] { 1.3218, 3.1444, 5.4371, 5.6444, 4.0107, 0.2827, 2.225, 10.2454, 41.1162, 124.4449 }; v = 0; s = new string[] { "Pa", "Pa: *RHF" }; break;
                   case 195: i = new int[] { 195, 92, 0 }; d = new double[] { 1.3382, 3.2043, 5.4558, 5.4839, 3.6342, 0.2838, 2.2452, 10.2519, 41.7251, 124.9023 }; v = 0; s = new string[] { "U", "U: RHF" }; break;
                   case 196: i = new int[] { 196, 92, 1 }; d = new double[] { 1.14, 2.48, 3.61, 1.13, 0.9, 0.25, 1.84, 7.39, 18, 22.7 }; v = +3; s = new string[] { "U", "U3+: *DS" }; break;
                   case 197: i = new int[] { 197, 92, 2 }; d = new double[] { 1.09, 2.32, 12, -9.11, 2.15, 0.243, 1.75, 7.79, 8.31, 16.5 }; v = +4; s = new string[] { "U", "U4+: *DS" }; break;
                   case 198: i = new int[] { 198, 92, 3 }; d = new double[] { 0.687, 1.14, 1.83, 2.53, 0.957, 0.154, 0.861, 2.58, 7.7, 15.9 }; v = +6; s = new string[] { "U", "U6+: *DS" }; break;
                   case 199: i = new int[] { 199, 93, 0 }; d = new double[] { 1.5193, 4.0053, 6.5327, -0.1402, 6.7489, 0.3213, 2.8206, 14.8878, 68.9103, 81.7257 }; v = 0; s = new string[] { "Np", "Np: *RHF" }; break;
                   case 200: i = new int[] { 200, 94, 0 }; d = new double[] { 1.3517, 3.2937, 5.3213, 4.6466, 3.5714, 0.2813, 2.2418, 9.9952, 42.7939, 132.1739 }; v = 0; s = new string[] { "Pu", "Pu: *RHF" }; break;
                   case 201: i = new int[] { 201, 95, 0 }; d = new double[] { 1.2135, 2.7962, 4.7545, 4.5731, 4.4786, 0.2483, 1.8437, 7.5421, 29.3841, 112.4579 }; v = 0; s = new string[] { "Am", "Am: *RHF" }; break;
                   case 202: i = new int[] { 202, 96, 0 }; d = new double[] { 1.2937, 3.11, 5.0393, 4.7546, 3.5031, 0.2638, 2.0341, 8.7101, 35.2992, 109.4972 }; v = 0; s = new string[] { "Cm", "Cm: *RHF" }; break;
                   case 203: i = new int[] { 203, 97, 0 }; d = new double[] { 1.2915, 3.1023, 4.9309, 4.6009, 3.4661, 0.2611, 2.0023, 8.4377, 34.1559, 105.8911 }; v = 0; s = new string[] { "Bk", "Bk: *RHF" }; break;
                   case 204: i = new int[] { 204, 98, 0 }; d = new double[] { 1.2089, 2.7391, 4.3482, 4.0047, 4.6497, 0.2421, 1.7487, 6.7262, 23.2153, 80.3108 }; v = 0; s = new string[] { "Cf", "Cf: *RHF" }; break;

                   #endregion
               }
               E_A[0] = d[0]; E_A[1] = d[1]; E_A[2] = d[2]; E_A[3] = d[3]; E_A[4] = d[4];
               E_B[0] = d[5]; E_B[1] = d[6]; E_B[2] = d[7]; E_B[3] = d[8]; E_B[4] = d[9];
               ScatteringNumberForElectron = i[0];
               AtomicNumber = i[1];
               SubNumberForElectron = i[2];
               AtomicName = s[0];
               MethodForElectron = s[1];
               ValenceForElectron = v;
           }
       }*/
    #endregion

    [Serializable()]
    public class DiffuseScatteringFactor
    {
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
        public double Uiso => OriginalType == Type.U ? Iso : Iso / PI2 / 8;
        public double Uiso_err => OriginalType == Type.U ? Iso_err : Iso_err / PI2 / 8;
        public double U11 => OriginalType == Type.U ? Aniso11 : Aniso11 / coeff11;
        public double U22 => OriginalType == Type.U ? Aniso22 : Aniso22 / coeff22;
        public double U33 => OriginalType == Type.U ? Aniso33 : Aniso33 / coeff33;
        public double U12 => OriginalType == Type.U ? Aniso12 : Aniso12 / coeff12;
        public double U23 => OriginalType == Type.U ? Aniso23 : Aniso23 / coeff23;
        public double U31 => OriginalType == Type.U ? Aniso31 : Aniso31 / coeff31;
        public double U11_err => OriginalType == Type.U ? Aniso11_err : Aniso11_err / coeff11;
        public double U22_err => OriginalType == Type.U ? Aniso22_err : Aniso22_err / coeff22;
        public double U33_err => OriginalType == Type.U ? Aniso33_err : Aniso33_err / coeff33;
        public double U12_err => OriginalType == Type.U ? Aniso12_err : Aniso12_err / coeff12;
        public double U23_err => OriginalType == Type.U ? Aniso23_err : Aniso23_err / coeff23;
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

        public (double A, double B, double C, double Alpha, double Beta, double Gamma) Cell
        {
            get => cell;
            set
            {
                cell = value;
                var cosAlpha = Math.Cos(cell.Alpha);
                var sinAlpha = Math.Sin(cell.Alpha);
                var cosBeta = Math.Cos(cell.Beta);
                var sinBeta = Math.Sin(cell.Beta);
                var cosGamma = Math.Cos(cell.Gamma);
                var sinGamma = Math.Sin(cell.Gamma);
                var v = cell.A * cell.B * cell.C * Math.Sqrt(1 - cosAlpha * cosAlpha - cosBeta * cosBeta - cosGamma * cosGamma + 2 * cosAlpha * cosBeta * cosGamma);
                var aStar = cell.B * cell.C * sinAlpha / v;
                var bStar = cell.C * cell.A * sinBeta / v;
                var cStar = cell.A * cell.B * sinGamma / v;
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

        private double coeff11, coeff22, coeff33, coeff12, coeff23, coeff31;

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
    }
}
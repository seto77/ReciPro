using System;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;

namespace Crystallography
{
    /// <summary>
    /// 境界を表現するクラス. 主にReciProのStructure Viewerから呼び出される.
    /// </summary>
    [Serializable()]
    public class Bound
    {
        public (int H, int K, int L) Index { get; set; }

        [XmlIgnore]
        public Color Color { get => Color.FromArgb(ColorArgb); }
        public int ColorArgb { get; set; } = Color.Gray.ToArgb();

        public bool Equivalency { get; set; } = true;
        public (double X, double Y, double Z, double D)[] PlaneParams { get; set; }
        public double Distance { get; set; }
        public double MultipleOfD { get; set; }

        public bool Enabled { get; set; } = true;

        public Bound()
        { }

        /// <summary>
        /// Boundクラスを生成. distanceはnm単位であることに注意
        /// </summary>
        /// <param name="enabled"></param>
        /// <param name="crystal"></param>
        /// <param name="h"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="equivalency"></param>
        /// <param name="distance"></param>
        /// <param name="argb"></param>
        public Bound(bool enabled, Crystal crystal, int h, int k, int l, bool equivalency, double distance, int argb) : this()
        {
            Enabled = enabled;
            ColorArgb = argb;
            Equivalency = equivalency;
            Distance = distance;
            Index = (h, k, l);

            Reset(crystal);
        }

        public void Reset(Crystal crystal)
        {
            var gBase = Index.H * crystal.A_Star + Index.K * crystal.B_Star + Index.L * crystal.C_Star;
            MultipleOfD = Distance * gBase.Length;

            (int H, int K, int L)[] planes = Equivalency ?
                SymmetryStatic.GenerateEquivalentPlanes(Index.H, Index.K, Index.L, crystal.Symmetry, false)
                : new[] { Index };

            PlaneParams = planes.Select(p =>
            {
                var g = p.H * crystal.A_Star + p.K * crystal.B_Star + p.L * crystal.C_Star;
                g.NormarizeThis();
                return (g.X, g.Y, g.Z, Distance);
            }).ToArray();
        }
    }

    /// <summary>
    /// 境界を表現するクラス. 主にReciProのStructure Viewerから呼び出される.
    /// </summary>
    [Serializable()]
    public class LatticePlane
    {
        public (int H, int K, int L) Index { get; set; }
        public double Translation { get; set; } = 0;
        public double D { get; set; } = 0;
        public bool Enabled { get; set; } = true;

        public (double X, double Y, double Z, double D) PlaneParam { get; set; }

        [XmlIgnore]
        public Color Color => Color.FromArgb(ColorArgb);

        public int ColorArgb { get; set; } = Color.Gray.ToArgb();

        public LatticePlane()
        { }

        public LatticePlane(bool enabled, Crystal crystal, int h, int k, int l, double translation, int argb):this()
        {
            Enabled = enabled;
            ColorArgb = argb;
            Translation = translation;
            Index = (h, k, l);

            Reset(crystal);
        }

        public void Reset(Crystal crystal)
        {
            var g = Index.H * crystal.A_Star + Index.K * crystal.B_Star + Index.L * crystal.C_Star;
            var d = 1 / g.Length;
            g.NormarizeThis();
            PlaneParam = (g.X, g.Y, g.Z, d);
        }
    }
}
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

        /// <summary>
        /// 平行移動量(nm)
        /// </summary>
        public double Translation { get; set; }

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
        /// <param name="translation"></param>
        /// <param name="argb"></param>
        public Bound(bool enabled, Crystal crystal, int h, int k, int l, bool equivalency, double distance, double translation, int argb) : this()
        {
            Enabled = enabled;
            ColorArgb = argb;
            Equivalency = equivalency;
            Distance = distance;
            Translation = translation;
            Index = (h, k, l);

            Reset(crystal);
        }

        public void Reset(Crystal crystal)
        {
            var gBase = Index.H * crystal.A_Star + Index.K * crystal.B_Star + Index.L * crystal.C_Star;
            MultipleOfD = Distance * gBase.Length;

            (int H, int K, int L)[] planes = Equivalency ? SymmetryStatic.GenerateEquivalentPlanes(Index, crystal.Symmetry, false) : [Index];

            if (Equivalency && planes.Length == 2 && Translation != 0)
            {
                var g1 = planes[0].H * crystal.A_Star + planes[0].K * crystal.B_Star + planes[0].L * crystal.C_Star;
                var g2 = planes[1].H * crystal.A_Star + planes[1].K * crystal.B_Star + planes[1].L * crystal.C_Star;
                g1.NormarizeThis();
                g2.NormarizeThis();
                PlaneParams = [(g1.X, g1.Y, g1.Z, Distance + Translation), (g2.X, g2.Y, g2.Z, Distance - Translation)];
            }
            else
            {
                PlaneParams = planes.Select(p =>
                {
                    var g = p.H * crystal.A_Star + p.K * crystal.B_Star + p.L * crystal.C_Star;
                    g.NormarizeThis();
                    return (g.X, g.Y, g.Z, Distance);
                }).ToArray();
            }
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

        public LatticePlane(bool enabled, Crystal crystal, int h, int k, int l, double translation, int argb) : this()
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
using System;
using System.Drawing;
using System.Xml.Serialization;

namespace Crystallography
{
    /// <summary>
    /// 境界を表現するクラス. 主にReciProのStructure Viewerから呼び出される.
    /// </summary>
    [Serializable()]
    public class Bound
    {
        //[NonSerialized] [XmlIgnore]

        public enum UnitEnum { D_spacing, Angstrom }

        public UnitEnum Unit { get; } = UnitEnum.D_spacing;
        public (int H, int K, int L) BaseIndex { get; set; }

        [XmlIgnore]
        public Color Color { get => Color.FromArgb(ColorArgb); }

        public int ColorArgb { get; set; } = Color.Gray.ToArgb();

        public bool Equivalency { get; set; } = true;
        public double[][] PlaneParams { get; }
        public double Distance { get; set; }
        public double D { get; }

        public Bound()
        { }

        public Bound(Crystal crystal, int h, int k, int l, bool equivalency, double distance, UnitEnum unit, int argb)
        {
            ColorArgb = argb;
            Equivalency = equivalency;
            Unit = unit;
            Distance = distance;
            BaseIndex = (h, k, l);

            var planes = new (int H, int K, int L)[1];
            if (equivalency)
                planes = SymmetryStatic.GenerateEquivalentPlanes(h, k, l, crystal.Symmetry);
            else
                planes[0] = (h, k, l);

            var gBase = h * crystal.A_Star + k * crystal.B_Star + l * crystal.C_Star;
            D = 1 / gBase.Length;
            var d = Unit == UnitEnum.D_spacing ? D * distance : distance;

            PlaneParams = new double[planes.Length][];
            for (int i = 0; i < planes.Length; i++)
            {
                var g = planes[i].H * crystal.A_Star + planes[i].K * crystal.B_Star + planes[i].L * crystal.C_Star;
                g.NormarizeThis();
                PlaneParams[i] = new double[] { g.X, g.Y, g.Z, d };
            }
        }
    }
}
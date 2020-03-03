using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Crystallography
{
    [Serializable()]
    public class Bonds
    {
        public string Element1;
        public string Element2;

        public float MinLength;
        public float MaxLength;

        public float Radius;

        public float BondTransParency;

        //public System.Drawing.Color BondColor;
        public int ArgbBond;

        public float PolyhedronTransParency;

        public bool ShowPolyhedron;
        public bool ShowCenterAtom;
        public bool ShowVertexAtom;
        public bool ShowInnerBonds;

        //public System.Drawing.Color PolyhedronColor;
        public int ArgbPolyhedron;

        public bool ShowEdges;
        public float EdgeLineWidth;

        //public System.Drawing.Color EdgeColor;
        public int ArgbEdge;

        [XmlIgnoreAttribute]
        public List<int[]> pairID = new List<int[]>();

        public Bonds()
        {
        }

        public Bonds(string element1, string element2, float minLength, float maxLength, float radius, float bondTranParency,
            System.Drawing.Color bondColor, float polyhedronTransParency, bool showPolyhedron, bool showCenterAtom, bool showVertexAtom,
            bool showInnerBonds, System.Drawing.Color polyhedronColor, bool showEdges, float edgeLineWidth, System.Drawing.Color edgeColor)
        {
            Element1 = element1;
            Element2 = element2;

            MinLength = minLength;
            MaxLength = maxLength;

            Radius = radius;

            BondTransParency = bondTranParency;

            ArgbBond = bondColor.ToArgb();
            PolyhedronTransParency = polyhedronTransParency;

            ShowPolyhedron = showPolyhedron;
            ShowCenterAtom = showCenterAtom;
            ShowVertexAtom = showVertexAtom;
            ShowInnerBonds = showInnerBonds;
            ArgbPolyhedron = polyhedronColor.ToArgb();

            ShowEdges = showEdges;
            EdgeLineWidth = edgeLineWidth;
            ArgbEdge = edgeColor.ToArgb();
        }

        public override string ToString()
        {
            string str = "";
            str += Element1 + "              ";
            str = str.Remove(8);
            str += Element2 + "              ";
            str = str.Remove(16);
            str += MinLength.ToString("f3") + "              ";
            str = str.Remove(24);
            str += MaxLength.ToString("f3") + "              ";
            str = str.Remove(32);
            return str;
        }
    }
}
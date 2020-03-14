using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ProtoBuf;

namespace Crystallography
{
    [ProtoContract]
    [Serializable()]
    public class Bonds
    {
        [ProtoMember(301)] public string Element1;
        [ProtoMember(302)] public string Element2;

        [ProtoMember(303)] public float MinLength;
        [ProtoMember(304)] public float MaxLength;

        [ProtoMember(305)] public float Radius;

        [ProtoMember(306)] public float BondTransParency;

        //public System.Drawing.Color BondColor;
        [ProtoMember(307)] public int ArgbBond;

        [ProtoMember(308)] public float PolyhedronTransParency;

        [ProtoMember(309)] public bool ShowPolyhedron;
        [ProtoMember(310)] public bool ShowCenterAtom;
        [ProtoMember(311)] public bool ShowVertexAtom;
        [ProtoMember(312)] public bool ShowInnerBonds;

        //public System.Drawing.Color PolyhedronColor;
        [ProtoMember(313)] public int ArgbPolyhedron;

        [ProtoMember(314)] public bool ShowEdges;
        [ProtoMember(315)] public float EdgeLineWidth;

        //public System.Drawing.Color EdgeColor;
        [ProtoMember(316)] public int ArgbEdge;

        [XmlIgnoreAttribute]
        [ProtoIgnore]
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
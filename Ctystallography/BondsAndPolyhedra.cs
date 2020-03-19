using System;
using System.Collections.Generic;
using System.Xml.Serialization;
//using ProtoBuf;
using MessagePack;

namespace Crystallography
{
    //[ProtoContract]
    [Serializable()]
    [MessagePackObject]
    public class Bonds
    {
        [Key(0)]
        public string Element1;
        [Key(1)]
        public string Element2;
        [Key(2)]
        public float MinLength;
        [Key(3)]
        public float MaxLength;
        [Key(4)]
        public float Radius;
        [Key(5)]
        public float BondTransParency;
        [Key(6)]
        public int ArgbBond;
        [Key(7)]
        public float PolyhedronTransParency;
        [Key(8)]
        public bool ShowPolyhedron;
        [Key(9)]
        public bool ShowCenterAtom;
        [Key(10)]
        public bool ShowVertexAtom;
        [Key(11)]
        public bool ShowInnerBonds;
        [Key(12)]
        public int ArgbPolyhedron;
        [Key(13)]
        public bool ShowEdges;
        [Key(14)]
        public float EdgeLineWidth;
        [Key(15)]
        public int ArgbEdge;
        
        
        [XmlIgnore]
        //[ProtoIgnore]
        [IgnoreMember]
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
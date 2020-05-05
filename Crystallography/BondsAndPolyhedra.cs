using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MessagePack;
using System.Drawing;

namespace Crystallography
{
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
        
        public string[] ElementList;
        [Key(16)]
        public bool Enabled = true;
        [Key(17)]
        public bool ShowBond = true;
        [Key(18)]
        public bool UseFixedColor = false;
        
        [XmlIgnore]
        //[ProtoIgnore]
        [IgnoreMember]
        public List<int[]> pairID = new List<int[]>();

        public Bonds()
        {
        }

        public Bonds(bool enabled,
            string[] elementList, string element1, string element2, float minLength, float maxLength,
            bool showBond, float radius, float bondTranParency,
            bool showPolyhedron, bool showCenterAtom, bool showVertexAtom, bool showInnerBonds,
            float polyhedronTransParency, bool showEdges, float edgeLineWidth)
        {
            Enabled = enabled;

            ElementList = elementList;
            Element1 = element1;
            Element2 = element2;

            MinLength = minLength;
            MaxLength = maxLength;

            ShowBond = showBond;
            Radius = radius;

            BondTransParency = bondTranParency;

            PolyhedronTransParency = polyhedronTransParency;

            ShowPolyhedron = showPolyhedron;
            ShowCenterAtom = showCenterAtom;
            ShowVertexAtom = showVertexAtom;
            ShowInnerBonds = showInnerBonds;

            ShowEdges = showEdges;
            EdgeLineWidth = edgeLineWidth;

            UseFixedColor = false;
        }

        public Bonds(bool enabled,
           string[] elementList, string element1, string element2, double minLength, double maxLength,
           bool showBond, double radius, double bondTranParency,
           bool showPolyhedron, bool showCenterAtom, bool showVertexAtom, bool showInnerBonds,
           double polyhedronTransParency, bool showEdges, double edgeLineWidth)
            :this(enabled,
            elementList, element1, element2, (float) minLength, (float) maxLength,
            showBond, (float) radius, (float) bondTranParency,
            showPolyhedron,  showCenterAtom, showVertexAtom,showInnerBonds,
           (float) polyhedronTransParency, showEdges, (float) edgeLineWidth)
        { }




        public Bonds(bool enabled,
             string[] elementList, string element1, string element2, float minLength, float maxLength,
             bool showBond, float radius, float bondTranParency,Color bondColor, 
             bool showPolyhedron,  bool showCenterAtom, bool showVertexAtom, bool showInnerBonds,
             float polyhedronTransParency, Color polyhedronColor, bool showEdges, float edgeLineWidth, Color edgeColor)
             : this(enabled,elementList, element1, element2, minLength, maxLength,
             showBond,  radius,  bondTranParency,
             showPolyhedron,  showCenterAtom, showVertexAtom,showInnerBonds,
             polyhedronTransParency, showEdges, edgeLineWidth)
        {
            ArgbBond = bondColor.ToArgb();
            ArgbPolyhedron = polyhedronColor.ToArgb();
            ArgbEdge = edgeColor.ToArgb();
            UseFixedColor = true;
        }
        public Bonds(bool enabled,
         string[] elementList, string element1, string element2, double minLength, double maxLength,
          bool showBond, double radius, double bondTranParency, Color bondColor,
          bool showPolyhedron, bool showCenterAtom, bool showVertexAtom, bool showInnerBonds,
          double polyhedronTransParency, Color polyhedronColor, bool showEdges, double edgeLineWidth, Color edgeColor)
         : this(enabled, elementList, element1, element2, (float)minLength, (float)maxLength,
          showBond, (float)radius, (float)bondTranParency, bondColor,
          showPolyhedron, showCenterAtom, showVertexAtom, showInnerBonds,
          (float)polyhedronTransParency, polyhedronColor, showEdges, (float)edgeLineWidth, edgeColor)
        {

        }


    }
}
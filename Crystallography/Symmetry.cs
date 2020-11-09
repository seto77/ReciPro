using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Crystallography
{
    /// <summary>
    /// Symmetry ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
    /// </summary>
    [Serializable()]
    public class Symmetry
    {
        //sub,SF,Hall,HM,HM_full,1é≤p,1é≤v,2é≤p,2é≤v,3é≤p,3é≤v,ì_åQÅAÉâÉEÉGåQÅAåãèªån
        public string SpaceGroupHMsubStr;

        public string SpaceGroupSFStr;
        public string SpaceGroupHallStr;
        public string SpaceGroupHMStr;
        public string SpaceGroupHMfullStr;
        public string MainAxis;
        public string LatticeTypeStr;

        public string StrSE1p;
        public string StrSE1v;
        public string StrSE2p;
        public string StrSE2v;
        public string StrSE3p;
        public string StrSE3v;
        public string PointGroupHMStr;
        public string PointGroupSFStr;
        public string LaueGroupStr;
        public string CrystalSystemStr;

        //Unknown;530ãÛä‘åQÇÃî‘çÜ(í Çµî‘çÜ		ãÛä‘åQî‘çÜ	ãÛä‘åQÇÃSubî‘çÜ		ì_åQî‘çÜ	ÉâÉEÉGåQî‘çÜ	åãèªånî‘çÜ)
        public int SeriesNumber, SpaceGroupNumber, SpaceGroupSubNumber, PointGroupNumber, LaueGroupNumber, CrystalSystemNumber;

        [XmlIgnoreAttribute]
        public string[] ExtinctionRuleStr;

        public bool IsPlaneRootIndex(int h, int k, int l) => SymmetryStatic.IsRootIndex((h, k, l), this);

        public bool IsPlaneRootIndex((int h, int k, int l) index) => SymmetryStatic.IsRootIndex(index, this);

        public List<Func<int, int, int, string>> CheckExtinctionFunc { get; set; } = new List<Func<int, int, int, string>>();

        public string[] CheckExtinctionRule((int h, int k, int l) index) 
            => CheckExtinctionFunc.Select(check => check(index.h, index.k, index.l)).Where(str => str != null).ToArray();

        public string[] CheckExtinctionRule(int h, int k, int l)
            => CheckExtinctionFunc.Select(check => check(h, k, l)).Where(str => str != null).ToArray();

        public enum CrystalSytem { Unknown, Triclinic, Monoclinic, Orthorhombic, Tetragonal, Trigonal, Hexagonal, Cubic }

        public enum LatticeType { P, A, B, C, I, F, R }
    }
}
using MessagePack;
using System;

namespace Crystallography
{
    /// <summary>
    /// Atoms2クラス
    /// </summary>
    [Serializable()]
    [MessagePackObject]
    public class Atoms2
    {
        [Key(0)]
        public string Label;
        [Key(1)]
        private byte[][] positionBytes;//x,y,z の 順番
        [Key(3)]
        private byte[] occBytes;//Occ 
        [Key(4)]
        public byte SubXray;
        [Key(5)]
        public byte SubElectron;
        [Key(6)]
        public byte AtomNo;
        [Key(7)]
        public bool IsU;
        [Key(8)]
        public bool IsIso;
        [Key(9)]
        private byte[] isoBytes;//B(U)iso
        [Key(10)]
        private byte[][] anisoBytes;//B(U)11, B(U)22, B(U)33, B(U)12, B(U)23, B(U)31の順番

        /// <summary>
        /// x,y,zの順番. 無次元
        /// </summary>
        [IgnoreMember]
        public string[] PositionTexts
        {
            get => positionBytes == null ? null : Array.ConvertAll(positionBytes, Crystal2.ToString);
            set
            {
                if (value != null)
                    positionBytes = Array.ConvertAll(value, Crystal2.ToBytes);
            }
        }

        /// <summary>
        /// Occ. 無次元
        /// </summary>
        [IgnoreMember]
        public string OccText { get => Crystal2.ToString(occBytes); set => occBytes = Crystal2.ToBytes(value); }

        /// <summary>
        /// 単位は Å^2. 
        /// </summary>
        [IgnoreMember]
        public string IsoText { get => Crystal2.ToString(isoBytes); set => isoBytes = Crystal2.ToBytes(value); }

        /// <summary>
        /// Bの場合は、無次元. Uの場合、Å^2. 
        /// </summary>
        [IgnoreMember]
        public string[] AnisoTexts
        {
            get => anisoBytes == null ? null : Array.ConvertAll(anisoBytes, Crystal2.ToString);
            set
            {
                if (value != null)
                    anisoBytes = Array.ConvertAll(value, Crystal2.ToBytes);
            }
        }

        public Atoms2() { }
        /// <summary>
        /// コンストラクタ. Uの単位はÅ
        /// </summary>
        /// <param name="label"></param>
        /// <param name="atomNo"></param>
        /// <param name="sfx"></param>
        /// <param name="sfe"></param>
        /// <param name="pos"></param>
        /// <param name="occ"></param>
        /// <param name="isIso"></param>
        /// <param name="isU"></param>
        /// <param name="iso"></param>
        /// <param name="aniso"></param>
        public Atoms2(string label, int atomNo, int sfx, int sfe, string[] pos, string occ, bool isIso, bool isU, string iso, string[] aniso)

        {
            PositionTexts = pos;

            Label = label;

            OccText = occ;

            SubXray = (byte)sfx;
            SubElectron = (byte)sfe;
            AtomNo = (byte)atomNo;

            IsIso = isIso;
            IsU = isU;

            AnisoTexts = aniso;
            IsoText = iso;

        }
    }
}

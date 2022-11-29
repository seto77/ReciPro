using MemoryPack;
using System;

namespace Crystallography;

/// <summary>
/// Atoms2クラス
/// </summary>
[Serializable()]
[MemoryPackable]
public partial class Atoms2
{
    public string Label;
    [MemoryPackInclude]
    private byte[][] positionBytes;//x,y,z の 順番
    [MemoryPackInclude]
    private byte[] occBytes;//Occ 
    public byte SubXray;
    public byte SubElectron;
    public byte AtomNo;//原子番号 ただし、255は重水素D
    public bool IsU;
    public bool IsIso;
    [MemoryPackInclude]
    private byte[] isoBytes;//B(U)iso
    [MemoryPackInclude]
    private byte[][] anisoBytes;//B(U)11, B(U)22, B(U)33, B(U)12, B(U)23, B(U)31の順番

    /// <summary>
    /// x,y,zの順番. 無次元
    /// </summary>
    [MemoryPackIgnore]
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
    [MemoryPackIgnore]
    public string OccText { get => Crystal2.ToString(occBytes); set => occBytes = Crystal2.ToBytes(value); }

    /// <summary>
    /// 単位は Å^2. 
    /// </summary>
    [MemoryPackIgnore]
    public string IsoText { get => Crystal2.ToString(isoBytes); set => isoBytes = Crystal2.ToBytes(value); }

    /// <summary>
    /// Bの場合は、無次元. Uの場合、Å^2. 
    /// </summary>
    [MemoryPackIgnore]
    public string[] AnisoTexts
    {
        get => anisoBytes == null ? null : Array.ConvertAll(anisoBytes, Crystal2.ToString);
        set
        {
            if (value != null)
                anisoBytes = Array.ConvertAll(value, Crystal2.ToBytes);
        }
    }

    [MemoryPackConstructor]
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
    public Atoms2(in string label, in int atomNo, in int sfx, in int sfe, string[] pos, in string occ, in bool isIso, in bool isU, in string iso, string[] aniso)

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

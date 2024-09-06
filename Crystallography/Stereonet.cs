using System;

namespace Crystallography;

/// <summary>
/// ステレオネット投影に関する静的メソッドを提供する
/// </summary>
public static class Stereonet
{
    /// <summary>
    /// ウルフネット上の点を返す
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public static PointD ConvertVectorToWulff(Vector3DBase vec)
    {
        if (vec == null) return new PointD(-100, -100);
        var v = Vector3DBase.Normarize(vec);
        return v.Z >= -0.999999 ? new PointD(v.X / (1 + v.Z), v.Y / (1 + v.Z)) : new PointD(-100, -100);
    }

    /// <summary>
    /// シュミット上の点を返す
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public static PointD ConvertVectorToSchmidt(Vector3DBase vec)
    {
        if (vec == null) return new PointD(-100, -100);
        var v = Vector3DBase.Normarize(vec);
        return v.Z >= -0.999999 ? new PointD(v.X / Math.Sqrt(1 + v.Z), v.Y / Math.Sqrt(1 + v.Z)) : new PointD(-100, -100);
    }

    public static PointD ConvertVectorToSchmidt(OpenTK.Vector3d vec)
        =>ConvertVectorToSchmidt(new Vector3DBase(vec.X, vec.Y, vec.Z));

}
using System;
using C4 = OpenTK.Mathematics.Color4;

namespace Crystallography;

/// <summary>
/// OpenGL用の色と材質を表すクラス Crystal.OpenGLで頻繁に使われるので、仕様変更は注意
/// </summary>
public class Material
{
    static byte toByte(in float val) => (byte)Math.Min(Math.Max(val * 255f, 0f), 255f);
    static byte toByte20(in float val) => (byte)Math.Min(Math.Max(val * 12.75f, 0f), 255f);

    #region 標準の材質
    /// <summary>
    /// 標準の材質のタプル.  Ambient = 0.2f, Diffuse = 0.5f, Specular = 0.6f, SpecularPow = 4.0f, Emission = 0.4f
    /// </summary>
    public static (float Ambient, float Diffuse, float Specular, float SpecularPow, float Emission) DefaultTexture => (0.2f, 0.5f, 0.6f, 4.0f, 0.4f);
    /// <summary>
    /// /// 標準の材質のfloat配列.  [0] = Ambient = 0.2f, [1] = Diffuse = 0.5f, [2] = Specular = 0.6f, [3] = SpecularPow = 4.0f, [4] = Emission = 0.4f
    /// </summary>
    public static float[] DefaultTextureArray => [0.2f, 0.5f, 0.6f, 4.0f, 0.4f];
    #endregion

    #region プロパティ、フィールド
    /// <summary>
    /// Materialの色. OpenTK.Graphics.Color4 構造体. 
    /// </summary>
    public C4 Color { get => new((byte)(argb >> 16 & 0xff), (byte)(argb >> 8 & 0xff), (byte)(argb & 0xff), (byte)(argb >> 24 & 0xff)); set => argb = value.ToArgb(); }
    /// <summary>
    /// Materialの色を表す Argb (読み取り専用)
    /// </summary>
    public int Argb { get => argb; }
    public int argb;

    #region Materialの材質
    /// <summary>
    /// 放射 (=自己発光). 0から1まで. 法線と始点が一致する場合に強くなる
    /// </summary>
    public float Emission { get => emission / 255f; set => emission = toByte(value); }
    private byte emission;

    /// <summary>
    /// 環境光. 0から1まで. この量だけ底上げされる。
    /// </summary>
    public float Ambient { get => ambient / 255f; set => ambient = toByte(value); }
    private byte ambient;

    /// <summary>
    /// 拡散光. 0から1まで. 法線と光源が一致する場合に強くなる
    /// </summary>
    public float Diffuse { get => diffuse / 255f; set => diffuse = toByte(value); }
    public byte diffuse;

    /// <summary>
    /// 反射光. 0から1まで. 入射角度と反射角度が等しい場合に強くなる
    /// </summary>
    public float Specular { get => specular / 255f; set => specular = toByte(value); }
    private byte specular;

    /// <summary>
    /// 表面の粗さ. 0から20まで. 高くすると、反射光領域は小さく、強度は強くなる。
    /// </summary>
    public float SpecularPower { get => specularPower / 12.75f; set => specularPower = toByte20(value); }
    private byte specularPower;

    /// <summary>
    /// 材質をまとめたタプルプロパティ. 読み取り/書き込み可能.
    /// </summary>
    public (float Ambient, float Diffuse, float Specular, float SpecularPow, float Emission) Texture
    {
        set { Ambient = value.Ambient; Diffuse = value.Diffuse; Specular = value.Specular; SpecularPower = value.SpecularPow; Emission = value.Emission; }
        get => (Ambient, Diffuse, Specular, SpecularPower, Emission);
    }

    #endregion

    #endregion

    #region コンストラクタ
    //基本コンストラクタ
    public Material(in C4 _color, float _ambient, float _diffuse, float _specular, float _specularPow, float _emission, float _transparency = -1f)
    {
        argb = _transparency != -1f ? new C4(_color.R, _color.G, _color.B, _transparency).ToArgb() : _color.ToArgb();
        ambient = toByte(_ambient);
        diffuse = toByte(_diffuse);
        specular = toByte(_specular);
        specularPower = toByte(_specularPow);
        emission = toByte(_emission);

    }
    public Material(in C4 color, in (float Ambient, float Diffuse, float Specular, float SpecularPow, float Emission) tex, float transparency = -1f)
        : this(color, tex.Ambient, tex.Diffuse, tex.Specular, tex.SpecularPow, tex.Emission, transparency) { }

    public Material(in C4 color) : this(color, DefaultTexture) { }
    public Material(in C4 color, in float transparency) : this(color, DefaultTexture, transparency) { }
    public Material(in C4 color, in double transparency) : this(color, DefaultTexture, (float)transparency) { }

    public Material(in int argb, in (float Ambient, float Diffuse, float Specular, float SpecularPow, float Emission) tex, float transparency = -1f) :
        this(new C4((byte)(argb >> 16 & 0xff), (byte)(argb >> 8 & 0xff), (byte)(argb & 0xff), (byte)(argb >> 24 & 0xff)), tex, transparency)
    { }

    public Material(in int argb, in float transparency = -1f) : this(argb, DefaultTexture, transparency) { }

    public Material(int argb, double transparency) : this(argb, DefaultTexture, (float)transparency) { }
    #endregion
}
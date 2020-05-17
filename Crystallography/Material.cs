using C4 = OpenTK.Graphics.Color4;
using V4f = OpenTK.Vector4;

namespace Crystallography
{
    /// <summary>
    /// OpenGL用の色と材質を表すクラス Crystal.OpenGLで頻繁に使われるので、仕様変更は注意
    /// </summary>
    public class Material
    {
        #region 標準の材質
        /// <summary>
        /// 標準の材質のタプル.  Ambient = 0.2f, Diffuse = 0.5f, Specular = 0.6f, SpecularPow = 4.0f, Emission = 0.4f
        /// </summary>
        public static  (float Ambient, float Diffuse, float Specular, float SpecularPow, float Emission) DefaultTexture => (0.2f, 0.5f, 0.6f, 4.0f, 0.4f);
        /// <summary>
        /// /// 標準の材質のfloat配列.  [0] = Ambient = 0.2f, [1] = Diffuse = 0.5f, [2] = Specular = 0.6f, [3] = SpecularPow = 4.0f, [4] = Emission = 0.4f
        /// </summary>
        public static float[] DefaultTextureArray => new[]{0.2f, 0.5f, 0.6f, 4.0f, 0.4f};
        #endregion

        #region プロパティ、フィールド
        /// <summary>
        /// Materialの色. OpenTK.Graphics.Color4 構造体. 
        /// </summary>
        public C4 Color { get; set; }

        /// <summary>
        /// Materialの色を表す Argb (読み取り専用)
        /// </summary>
        public int Argb { get => Color.ToArgb(); }

        #region Materialの材質
        /// <summary>
        /// 放射 (=自己発光). 0から1まで. 法線と始点が一致する場合に強くなる
        /// </summary>
        public float Emission { get; set; }

        /// <summary>
        /// 環境光. 0から1まで. この量だけ底上げされる。
        /// </summary>
        public float Ambient { get; set; }

        /// <summary>
        /// 拡散光. 0から1まで. 法線と光源が一致する場合に強くなる
        /// </summary>
        public float Diffuse { get; set; }

        /// <summary>
        /// 反射光. 0から1まで. 入射角度と反射角度が等しい場合に強くなる
        /// </summary>
        public float Specular { get; set; }

        /// <summary>
        /// 表面の粗さ. 0から20まで. 高くすると、反射光領域は小さく、強度は強くなる。
        /// </summary>
        public float SpecularPower { get; set; }

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
        public Material(C4 color, float ambient, float diffuse, float specular, float specularPow, float emission, float transparency = -1f)
        {
            Color = transparency != -1f ?
                new C4(color.R, color.G, color.B, transparency) : color;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            SpecularPower = specularPow;
            Emission = emission;

        }
        public Material(C4 color, (float Ambient, float Diffuse, float Specular, float SpecularPow, float Emission) tex, float transparency = -1f)
            : this(color, tex.Ambient, tex.Diffuse, tex.Specular, tex.SpecularPow, tex.Emission, transparency) { }

        public Material(C4 color) : this(color, DefaultTexture) { }
        public Material(C4 color, float transparency) : this(color, DefaultTexture, transparency) { }
        public Material(C4 color, double transparency) : this(color, DefaultTexture, (float)transparency) { }

        public Material(int argb, (float Ambient, float Diffuse, float Specular, float SpecularPow, float Emission) tex, float transparency = -1f) :
            this(new C4((byte)(argb >>16 & 0xff), (byte)(argb >> 8 & 0xff), (byte)(argb & 0xff), (byte)(argb >>24 & 0xff)), tex, transparency)
        { }

        public Material(int argb, float transparency = -1f) : this(argb, DefaultTexture, transparency) { }

        public Material(int argb, double transparency) : this(argb, DefaultTexture, (float)transparency) { }
        #endregion
    }
}
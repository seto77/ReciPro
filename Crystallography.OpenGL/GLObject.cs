#region using
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics; // 260420Cl 追加 静的コンストラクタでの例外ログ出力用
using System.Management;
using System.Threading;
using ZLinq;
#endregion

#region 定義
using V4f = OpenTK.Mathematics.Vector4;
using V4d = OpenTK.Mathematics.Vector4d;
using V3f = OpenTK.Mathematics.Vector3;
using V3d = OpenTK.Mathematics.Vector3d;
using V2f = OpenTK.Mathematics.Vector2;
using V2d = OpenTK.Mathematics.Vector2d;
using C4 = OpenTK.Mathematics.Color4;
using M4d = OpenTK.Mathematics.Matrix4d;
using M4f = OpenTK.Mathematics.Matrix4;
using M3d = OpenTK.Mathematics.Matrix3d;
using PT = OpenTK.Graphics.OpenGL4.PrimitiveType;

#endregion 定義

namespace Crystallography.OpenGL;

#region Vertex 頂点構造体
/// <summary>頂点要素（シェーダの頂点要素と合わせる）</summary>
public readonly struct Vertex
{
    /// <summary>0: テクスチャ無しポリゴン. 1: テクスチャ有りポリゴン. 2: 文字列.</summary>
    public readonly int ObjType;

    public readonly int Argb;

    public readonly V3f Position;

    public readonly V3f Normal;

    public readonly V2f Uv;

    /// <summary>テクスチャ無しのポリゴン</summary>
    /// <param name="position"></param>
    /// <param name="normal"></param>
    /// <param name="argb"></param>
    public Vertex(V3f position, V3f normal, int argb)
    {
        Position = position;
        Normal = normal;
        Argb = argb;
        Uv = V2f.Zero; // (260320Ch) Zero 定数で初期値の意図を明確にする
        ObjType = 0;
    }

    /// <summary>テクスチャ無しのポリゴン</summary>
    /// <param name="position"></param>
    /// <param name="normal"></param>
    /// <param name="argb"></param>
    public Vertex(V3f position, int argb)
    {
        Position = position;
        Normal = V3f.Zero; // (260320Ch) default normal を共有 Zero で表す
        Argb = argb;
        Uv = V2f.Zero;
        ObjType = 0;
    }

    public Vertex(V4f position, V3f normal, int argb) : this(new V3f(position), normal, argb) { }


    /// <summary>
    /// 文字列のコンストラクタ
    /// 文字列の場合はNormalが中心座標, Positionは中心からのシフト量(X,Yはピクセル単位、Zはワールド単位で、回転の影響を受けない)を表す. 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="normal"></param>
    /// <param name="uv"></param>
    public Vertex(V3f position, V3f normal, V2f uv)
    {
        if (GLControlAlpha.DisableTextRendering)
            position = V3f.Zero; // (260320Ch) 文字描画無効時も共有 Zero を使う
        Position = position;
        Normal = normal;
        Argb = 0;
        Uv = uv;
        ObjType = 2;
    }

    public static readonly int Stride = Marshal.SizeOf<Vertex>(); // (260320Ch) generic overload でダミー値生成を避ける
}
#endregion

#region Enum
public enum DrawingMode { Surfaces = 1, Edges = 2, SurfacesAndEdges = 4, Points = 8, Text = 16 }
#endregion

#region Locationクラス
public class Location
{
    internal int Texture { get; set; } = -1;
    internal int Emission { get; set; } = -1;
    internal int Ambient { get; set; } = -1;
    internal int Diffuse { get; set; } = -1;
    internal int Specular { get; set; } = -1;
    internal int SpecularPower { get; set; } = -1;
    internal int UseFixedArgb { get; set; } = -1;
    internal int FixedArgb { get; set; } = -1;
    internal int IgnoreNormalSides { get; set; } = -1;
    internal int RenderPass { get; set; } = -1;

    internal int PassPPLL1Index = -1;
    internal int PassPPLL2Index = -1;
    internal int PassNormalIndex = -1;
    internal int VertexPathLocation { get; set; } = -1;
    internal int VertexMeshIndex { get; set; } = -1;
    internal int VertexTextIndex { get; set; } = -1;
    internal int FragmentPathLocation { get; set; } = -1;
    internal int FragmentSurfaceIndex { get; set; } = -1;
    internal int FragmentTextIndex { get; set; } = -1;
    internal int Position { get; set; } = -1;
    internal int Normal { get; set; } = -1;
    internal int Argb { get; set; } = -1;

    internal int Uv { get; set; } = -1;
    internal int ObjType { get; set; } = -1;
    internal int ObjectMatrix { get; set; } = -1;
}
#endregion

#region GLObjectクラス (抽象クラス)
/// <summary>OpenGLで描画するオブジェクトを表現する抽象クラス</summary>
//[Guid("71D52F24-787B-4646-AC8E-2910CC38E267")]
public abstract class GLObject
{
    #region static private な フィールド & プロパティ

    // private static readonly Dictionary<int, Location> Location = [];
    private static readonly Dictionary<(int ContextKey, int Program), Location> Location = []; // (260319Ch) 複数 GL context 間で program 番号が衝突しても location を取り違えない
    internal static int CurrentFragmentRenderPassIndex = -1; // (260319Ch) PPLL / DDP の RenderPass も UseProgram 後に毎 draw 明示する
    internal static bool CurrentDepthTestEnabled = true; // (260319Ch) clip 描画後にレンダリング phase ごとの depth test state を復元する

    private static readonly int sizeOfInt = sizeof(int);
    private static readonly int sizeOfUInt = sizeof(uint);
    private static readonly List<(string Product, string Version)> GraphicsInfo;
    private static int serialNumber = 0;
    public static readonly Lock LockObj = new();

    public static float LineWidthStatic = 1f;

    #endregion

    #region internalな フィールド & プロパティ

    internal (int VBO, int VAO, int EBO) Obj;

    internal int Program = -1;
    internal int ContextKey = 0; // (260319Ch) GPU 側 cache を GLControl 単位で分離するための key

    /// <summary>頂点</summary>
    internal Vertex[] Vertices;

    /// <summary>頂点の順番リスト (全てのタイプが連結されている)</summary>
    internal uint[] Indices;

    /// <summary>プリミティブのタイプおよびそのタイプの順番リストの長さ</summary>
    internal (PT Type, int Count)[] Primitives;

    #endregion

    #region publicな フィールド & プロパティ
    
    /// <summary>自由に情報を格納するためのTag</summary>
    public object Tag = null;

    /// <summary>オブジェクトを一意に識別する番号</summary>
    public int SerialNumber;

    /// <summary>物体に外接する外接球の中心座標 (wは常に1にする)</summary>
    public V4d CircumscribedSphereCenter = new(0, 0, 0, 1);

    /// <summary>物体に外接する外接球の半径</summary>
    public double CircumscribedSphereRadius = 0;

    /// <summary>法線方向の正負を無視する</summary>
    public bool IgnoreNormalSides;

    /// <summary>クリップされた場合、断面ポリゴンを描画するかどうか. 初期値はtrue. Polygon(平面)はfalse</summary>
    public bool ShowClippedSection = true;

    /// <summary>描画モード</summary>
    public DrawingMode Mode;

    /// <summary>素材</summary>
    public Material Material;

    /// <summary>線の太さ (edgeを描画する場合)</summary>
    public float LineWidth = 1f;

    /// <summary>描画するかどうか</summary>
    public bool Rendered = true;

    /// <summary>trueの時は(Vertexの色ではなく)Material構造体中のColorが使われる</summary>
    public bool UseFixedArgb = true;

    /// <summary>Z sorting で透明度を計算する時のZを一時期的に格納する変数</summary>
    public double Z;

    /// <summary>物体の回転状態や並進状態を表す.</summary>
    public M4f ObjectMatrix = M4f.Identity;


    #endregion publicなフィールド

    #region コンストラクタ、デストラクタ
    /// <summary>静的コンストラクタ</summary>
    static GLObject()
    {
        //ビデオカード検索
        GraphicsInfo = [];
        // 260420Cl WMI リポジトリ破損等で ManagementException が発生しても静的コンストラクタを通すため try/catch で保護 (GLControlAlpha 側と同じ対策)
        try
        {
            using var searcher = new ManagementObjectSearcher(new SelectQuery("Win32_VideoController")); // (260320Ch) WMI searcher を確実に破棄する
            using var videoControllers = searcher.Get();
            foreach (ManagementObject envVar in videoControllers)
                GraphicsInfo.Add((Convert.ToString(envVar["name"]) ?? string.Empty, Convert.ToString(envVar["DriverVersion"]) ?? string.Empty)); // (260320Ch) null 安全な文字列化へ統一する
        }
        catch (Exception e)
        {
            Debug.WriteLine($"GLObject: WMI Win32_VideoController query failed: {e.Message}");
        }

        // var flag = GraphicsInfo.Select(g => g.Product.ToLower()).Any(p => p.Contains("nvidia") || p.Contains("amd"));
        var flag = GraphicsInfo.Any(static g => g.Product.Contains("nvidia", StringComparison.OrdinalIgnoreCase) || g.Product.Contains("amd", StringComparison.OrdinalIgnoreCase)); // (260320Ch) ToLower の一時文字列を作らず大小無視で判定する
        Cone.Default = (1, flag ? 24 : 16);
        Pipe.Default = (1, flag ? 24 : 16);
        Sphere.DefaultSlices = flag ? 4 : 3;
    }


    /// <summary>コンストラクタ、デストラクタ</summary>
    /// <param name="material"></param>
    /// <param name="mode"></param>
    public GLObject(Material material, DrawingMode mode)
    {
        Material = material;
        Mode = mode;
        lock (LockObj)
            SerialNumber = serialNumber++;
    }

    public void Dispose()
    {
        // Sphere.DefaultDictionary.Remove(Program);
        // Cylinder.DefaultDictionary.Remove(Program);
        Sphere.DefaultDictionary.Remove((ContextKey, Program)); // (260319Ch) 別 context の default VAO を巻き込まない
        Cylinder.DefaultDictionary.Remove((ContextKey, Program)); // (260319Ch)
        // if (this is TextObject t && TextObject.DefaultDictionaly.ContainsKey((Program, t.TextureNum)))
        //     TextObject.DefaultDictionaly.Remove((Program, t.TextureNum));
        if (this is TextObject t && TextObject.DefaultDictionaly.ContainsKey((t.ContextCacheKey, Program, t.TextureNum)))
            TextObject.DefaultDictionaly.Remove((t.ContextCacheKey, Program, t.TextureNum)); // (260319Ch) text VAO cache は context 単位で破棄する
        GL.DeleteBuffers(1, ref Obj.VBO);
        GL.DeleteBuffers(1, ref Obj.EBO);
        GL.DeleteVertexArrays(1, ref Obj.VAO);
    }

    #endregion

    #region 静的メソッド
    /// <summary>静的メソッド. program番号をセットし、各バッファオブジェクトなどGPUに転送する. 描画前に必ず一度実行する必要がある。</summary>
    /// <param name="program"></param>
    /// <param name="objects"></param>
    // public static void Generate(int program, IEnumerable<GLObject> objects)
    public static void Generate(int program, IEnumerable<GLObject> objects, int contextKey = 0) // (260319Ch) location / default VAO cache を context ごとに分離
    {
        if (program < 0) return;
        ArgumentNullException.ThrowIfNull(objects);

        //Locationを取得
        // if (!Location.TryGetValue(program, out var location))
        if (!Location.TryGetValue((contextKey, program), out var location))
        {
            // Location.Add(program, GetLocation(program));
            // location = Location[program];
            Location.Add((contextKey, program), GetLocation(program)); // (260319Ch) 同じ program 番号でも別 context なら別 location を持つ
            location = Location[(contextKey, program)];
        }

        //VertexAttribPointerのパラメータを取得
        //var prms = new (int loc, int size, VertexAttribPointerType type, bool normarized, int stride, int offset)[]
        //{
        //        (location.ObjType, 1, VertexAttribPointerType.Byte, false, Vertex.Stride, 0),//ObjTYpe
        //        (location.Argb, 1, VertexAttribPointerType.Int, false, Vertex.Stride, sizeOfInt),//色
        //        (location.Position, 3, VertexAttribPointerType.Float, false, Vertex.Stride, sizeOfInt *2), //頂点位置
        //        (location.Normal, 3, VertexAttribPointerType.Float, true, Vertex.Stride, sizeOfInt *2 + V3f.SizeInBytes),//法線
        //        (location.Uv, 2, VertexAttribPointerType.Float, false, Vertex.Stride, sizeOfInt *2 + 2 * V3f.SizeInBytes)//テクスチャ座標
        //};
        var integerAttribs = new (int loc, int size, VertexAttribIntegerType type, int stride, int offset)[]
        {
                (location.ObjType, 1, VertexAttribIntegerType.Int, Vertex.Stride, 0),//ObjType // (260319Ch) core profile では整数属性を IPointer で渡す
                (location.Argb, 1, VertexAttribIntegerType.Int, Vertex.Stride, sizeOfInt),//色 // (260319Ch) GLSL の int 入力へそのまま渡す
        };
        var floatAttribs = new (int loc, int size, VertexAttribPointerType type, bool normarized, int stride, int offset)[]
        {
                (location.Position, 3, VertexAttribPointerType.Float, false, Vertex.Stride, sizeOfInt *2), //頂点位置
                (location.Normal, 3, VertexAttribPointerType.Float, true, Vertex.Stride, sizeOfInt *2 + V3f.SizeInBytes),//法線
                (location.Uv, 2, VertexAttribPointerType.Float, false, Vertex.Stride, sizeOfInt *2 + 2 * V3f.SizeInBytes)//テクスチャ座標
        };

        foreach (var o in objects)
        {
            if (o.Vertices is not { Length: > 0 })
                continue; // (260320Ch) LINQ の Where を介さずその場でスキップする

            o.Program = program;
            o.ContextKey = contextKey; // (260319Ch) Render 時にも context 単位の location cache を引けるよう保持する

            if (o is TextObject t)
            {
                // if (TextObject.DefaultDictionaly.TryGetValue((program, t.TextureNum), out var def))
                if (TextObject.DefaultDictionaly.TryGetValue((t.ContextCacheKey, program, t.TextureNum), out var def))
                    o.Obj = def;//Defaultテキストであれば、VAO, VBO, EBOをセットしておしまい。
                else
                {
                    GenerateSub(o);
                    TextObject.DefaultDictionaly.Add((t.ContextCacheKey, program, t.TextureNum), (o.Obj)); // (260319Ch) 別 context の text VAO を誤再利用しない
                }
            }
            else
            {
                Dictionary<(int ContextKey, int Program), (int VBO, int VAO, int EBO)> dic = null; // (260319Ch) default VAO も context 単位で再利用
                if (o is Sphere s && s.UseDefault)
                    dic = Sphere.DefaultDictionary;
                else if (o is Cylinder c && c.UseDefault)
                    dic = Cylinder.DefaultDictionary;

                // if (dic != null && dic.TryGetValue(program, out var def))
                if (dic != null && dic.TryGetValue((o.ContextKey, program), out var def))
                    o.Obj = def;//Default形状であれば、VAO, VBO, EBOをセットしておしまい。
                else
                {
                    GenerateSub(o);
                    // dic?.Add(program, o.Obj);
                    dic?.Add((o.ContextKey, program), o.Obj); // (260319Ch) 別 context の VAO を誤再利用しない
                }
            }
        }

        //ローカル関数
        void GenerateSub(GLObject o)
        {
            // VBO作成
            GL.GenBuffers(1, out o.Obj.VBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, o.Obj.VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(o.Vertices.Length * Vertex.Stride), o.Vertices, BufferUsageHint.DynamicDraw);
            // EBO作成
            GL.GenBuffers(1, out o.Obj.EBO);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, o.Obj.EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeOfUInt * o.Indices.Length), o.Indices, BufferUsageHint.DynamicDraw);
            // VAO作成
            GL.GenVertexArrays(1, out o.Obj.VAO);
            GL.BindVertexArray(o.Obj.VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, o.Obj.VBO);
            //VertexAttribPointerをセット
            foreach (var (loc, size, type, stride, offset) in integerAttribs)
            {
                if (loc == -1) continue;
                GL.EnableVertexAttribArray(loc);
                GL.VertexAttribIPointer(loc, size, type, stride, new IntPtr(offset));
            }
            foreach (var (loc, size, type, normarized, stride, offset) in floatAttribs)
            {
                if (loc == -1) continue;
                GL.EnableVertexAttribArray(loc);
                GL.VertexAttribPointer(loc, size, type, normarized, stride, offset);
            }
        }
    }

    /// <summary>静的メソッド. パラメータのロケーションをセット</summary>
    /// <param name="Program"></param>
    public static Location GetLocation(int Program)
    {
        var loc = new Location
        {
            
            Argb = GL.GetAttribLocation(Program, "vArgb"),
            Position = GL.GetAttribLocation(Program, "vPosition"),
            Normal = GL.GetAttribLocation(Program, "vNormal"),
            Uv = GL.GetAttribLocation(Program, "vUv"),
            ObjType = GL.GetAttribLocation(Program, "vType"),
            ObjectMatrix = GL.GetUniformLocation(Program, "ObjectMatrix"),
            Texture = GL.GetUniformLocation(Program, "Texture"),
            Emission = GL.GetUniformLocation(Program, "Emission"),
            Ambient = GL.GetUniformLocation(Program, "Ambient"),
            Diffuse = GL.GetUniformLocation(Program, "Diffuse"),
            Specular = GL.GetUniformLocation(Program, "Specular"),
            SpecularPower = GL.GetUniformLocation(Program, "SpecularPower"),
            UseFixedArgb = GL.GetUniformLocation(Program, "UseFixedArgb"),
            IgnoreNormalSides = GL.GetUniformLocation(Program, "IgnoreNormalSides"),
            FixedArgb = GL.GetUniformLocation(Program, "FixedArgb"),
            VertexPathLocation = GL.GetSubroutineUniformLocation(Program, ShaderType.VertexShader, "VertexPath"),
            VertexMeshIndex = GL.GetSubroutineIndex(Program, ShaderType.VertexShader, "renderMeshVertex"),
            VertexTextIndex = GL.GetSubroutineIndex(Program, ShaderType.VertexShader, "renderTextVertex"),
            FragmentPathLocation = GL.GetSubroutineUniformLocation(Program, ShaderType.FragmentShader, "FragmentPath"),
            FragmentSurfaceIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "shadeSurfaceFragment"),
            FragmentTextIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "shadeTextFragment")
        };


        if (GraphicsInfo.All(info => !info.Product.Contains("Parallels")))
        {
            // loc.PassPPLL1Index = GL.GetProgramResourceIndex(Program, ProgramInterface.FragmentSubroutine, "passPPLL1");
            // loc.PassPPLL2Index = GL.GetProgramResourceIndex(Program, ProgramInterface.FragmentSubroutine, "passPPLL2");
            // loc.PassNormalIndex = GL.GetProgramResourceIndex(Program, ProgramInterface.FragmentSubroutine, "passNormal");
            loc.PassPPLL1Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passPPLL1"); // (260319Ch) UniformSubroutines 用の正しい index を取得する
            loc.PassPPLL2Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passPPLL2"); // (260319Ch)
            loc.PassNormalIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passNormal"); // (260319Ch)
            loc.RenderPass = GL.GetProgramResourceLocation(Program, ProgramInterface.FragmentSubroutineUniform, "RenderPass");
        }

        if (loc.Position == -1)

            throw new Exception("cannot find location!");

        return loc;
    }

    #endregion

    /// <summary>
    /// program番号をセットし、各バッファオブジェクトなどGPUに転送する. 描画前に必ず一度実行する必要がある。
    /// 内部的には、静的メソッド Generate(int program, GLObject[] objs)を呼び出す。
    /// </summary>
    /// <param name="program"></param>
    // public void Generate(int program) => Generate(program, [this]);
    public void Generate(int program, int contextKey = 0) => Generate(program, [this], contextKey); // (260319Ch) 呼び出し元 control の context key を伝搬する

    /// レンダリングを実行. Progaramが正しくセットされていない(Generate()をしていない)場合は例外が発生
    /// </summary>
    private void Render()
    {
        if (Primitives == null)
            return;

        GL.UseProgram(Program);
        var location = Location[(ContextKey, Program)]; // (260319Ch) location cache は current control の context key で引く
        var isText = this is TextObject;
        // if (shaderPathPrms.Program != Program || shaderPathPrms.IsText != isText)
        { // (260319Ch) subroutine state は context ごとなので、複数 GLControl では毎回明示する
            if (location.VertexPathLocation != -1)
            {
                var vertexIndex = isText ? location.VertexTextIndex : location.VertexMeshIndex;
                if (vertexIndex != -1)
                    GL.UniformSubroutines(ShaderType.VertexShader, 1, ref vertexIndex);
            }

            if (location.RenderPass != -1 && CurrentFragmentRenderPassIndex != -1)
            {
                var renderPassIndex = CurrentFragmentRenderPassIndex;
                GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref renderPassIndex); // (260319Ch) PPLL pass1/pass2 は UseProgram 後に設定し直す
            }
            else if (location.FragmentPathLocation != -1 && location.RenderPass == -1)
            {
                var fragmentIndex = isText ? location.FragmentTextIndex : location.FragmentSurfaceIndex;
                if (fragmentIndex != -1)
                    GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref fragmentIndex);
            }
        }

        if (this is TextObject text && text.TextureNum != -1)
        {
            GL.ActiveTexture(TextureUnit.Texture0); // (260319Ch) PPLL 合成後でも text sampler が常に unit 0 を向くよう明示する
            GL.Uniform1(location.Texture, 0);
            GL.BindTexture(TextureTarget.Texture2D, text.TextureNum);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
        }

        // if ((Mode == DrawingMode.SurfacesAndEdges || Mode == DrawingMode.Edges) && LineWidth != LineWidthStatic)
        if (Mode == DrawingMode.SurfacesAndEdges || Mode == DrawingMode.Edges)
            GL.LineWidth(LineWidth); // (260319Ch) 線幅 state は context ごとなので毎回設定する

        GL.BindVertexArray(Obj.VAO);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, Obj.EBO);

        int offset = 0;
        
        foreach (var (t, len) in Primitives)
        {
            if ((t == PT.Triangles || t == PT.TriangleStrip || t == PT.TriangleFan)
                && (Mode == DrawingMode.Surfaces || Mode == DrawingMode.SurfacesAndEdges || Mode == DrawingMode.Text))
                SetMaterialAndDrawElements(true, t, len, offset);
            else
            {
                if ((t == PT.Lines || t == PT.LinesAdjacency || t == PT.LineLoop || t == PT.LineStrip) && (Mode == DrawingMode.Edges || Mode == DrawingMode.SurfacesAndEdges))
                    SetMaterialAndDrawElements(false, t, len, offset);
                else if (t == PT.Points && Mode == DrawingMode.Points)
                    SetMaterialAndDrawElements(false, t, len, offset);
            }
            offset += len * sizeOfUInt;
        }
    }

    /// <summary>物体の材質と要素をGPUに送信する。Render()関数から呼び出される。</summary>
    /// <param name="drawSurfaces">Surfaceモードか否か</param>
    private void SetMaterialAndDrawElements(bool drawSurfaces, PT mode, int count, int offset)
    {
        var location = Location[(ContextKey, Program)]; // (260319Ch) 別 context の uniform location を参照しない
        var renew = prms.ContextKey != ContextKey || prms.Program != Program;

        if (renew || prms.objmatrix != ObjectMatrix)
            GL.UniformMatrix4(location.ObjectMatrix, false, ref ObjectMatrix);

        (float emi, float amb, float dif, float spe) = drawSurfaces ?
            (Material.Emission, Material.Ambient, Material.Diffuse, Material.Specular) : (0f, 1f, 0f, 0f);

        if (renew || emi != prms.emi)
            GL.Uniform1(location.Emission, emi);

        if (renew || amb != prms.amb)
            GL.Uniform1(location.Ambient, amb);

        if (renew || dif != prms.dif)
            GL.Uniform1(location.Diffuse, dif);

        if (renew || spe != prms.spe)
            GL.Uniform1(location.Specular, spe);

        if (renew || prms.spePow != Material.SpecularPower)
            GL.Uniform1(location.SpecularPower, Material.SpecularPower);

        if (renew || prms.UseFixedArgb != UseFixedArgb)
            GL.Uniform1(location.UseFixedArgb, UseFixedArgb ? 1 : 0);//Trueが1なのはなぜか分からないが、これで動く。

        if (renew || prms.argb != Material.Argb)
            GL.Uniform1(location.FixedArgb, Material.Argb);

        if (renew || prms.ignoreNormal != IgnoreNormalSides)
            GL.Uniform1(location.IgnoreNormalSides, IgnoreNormalSides ? 1 : 0);

        //if (IgnoreNormalSides)
        //{
        //    GLable(false, EnableCap.CullFace);//CullFace無効化
        //    GL.DrawElements(mode, count, DrawElementsType.UnsignedInt, offset);
        //    GL.GetUniformSubroutine(ShaderType.FragmentShader, RenderPassLocation, out int renderPassIndex);  //レンダーパスを取得
        //    GLable(renderPassIndex == PassNormalIndex, EnableCap.CullFace);//CullFaceを元に戻す
        //}
        //else
        GL.DrawElements(mode, count, DrawElementsType.UnsignedInt, offset);//CullFaceは常に無効

        prms = (ContextKey, Program, emi, amb, dif, spe, Material.SpecularPower, Material.Argb, IgnoreNormalSides, UseFixedArgb, ObjectMatrix); // (260319Ch) uniform cache も context を跨がない
    }
    // static private (int Program, float emi, float amb, float dif, float spe, float spePow, int argb, bool ignoreNormal, bool UseFixedArgb, M4f objmatrix)
    //      prms = (-1, 0, 0, 0, 0, 0, 0, false, false, M4f.Identity);
    static private (int ContextKey, int Program, float emi, float amb, float dif, float spe, float spePow, int argb, bool ignoreNormal, bool UseFixedArgb, M4f objmatrix)
         prms = (0, -1, 0, 0, 0, 0, 0, 0, false, false, M4f.Identity); // (260319Ch) 複数 GLControl で uniform cache が干渉しないよう context key を含める


    /// <summary>
    /// レンダリングを実行. Progaramが正しくセットされていない(Generate()をしていない)場合は例外が発生. 
    /// PPLLモードはCullFaceとDepthTestが無効、通常モードはCullFaceとDepthTestが有効
    /// </summary>
    /// <param name="clip">Clip平面</param>
    public void Render(Clip clip = null)
    {
        if (!Rendered) return;
        GL.UseProgram(Program); // (260319Ch) clip uniform 更新前に現在の object program を明示する
        var activeRenderPassIndex = CurrentFragmentRenderPassIndex; // (260319Ch) clip の stencil/path 切替後も現在の OIT pass を復元できるよう保持する

        //クリップ無効か、有効だが全てのクリップ面の内側にある場合
        if (clip == null || clip.PrmsD.Count == 0 || clip.PrmsD.All(p => V4d.Dot(p, CircumscribedSphereCenter) - CircumscribedSphereRadius > 0))
        {
            Render();
        }
        else if (!clip.PrmsD.Any(p => V4d.Dot(p, CircumscribedSphereCenter) + CircumscribedSphereRadius < 0))//クリップ有効でクリップ面を切るようなオブジェクトの場合
        {
            if (clip.PrmsD.Count < 200)//クリップ面が多すぎる場合は計算しない
            {
                //クリップ対象となるクリップ平面を検索
                var indices = clip.PrmsD.Select((p, i) => Math.Abs(V4d.Dot(p, CircumscribedSphereCenter)) < CircumscribedSphereRadius ? i : -1).Where(i => i != -1);
                if (!ShowClippedSection)//クリップセクションを表示しない場合
                {
                    clip.EnableClips(Program, indices);//全クリップ有効化 // (260319Ch) mesh/text の各 program に対して clip uniform を設定する
                    Render(); //物体全体の描画
                    Clip.DisableAllClips();//全クリップ無効化
                }
                else///クリップセクションを表示する場合
                {
                    GL.Enable(EnableCap.StencilTest);//Stencilテスト有効
                    foreach (var i in indices)
                    {
                        DepthTest(false);//Depthテスト無効

                        if (Location[(ContextKey, Program)].PassNormalIndex != -1)//PPLLモードの場合は
                            GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref Location[(ContextKey, Program)].PassNormalIndex);//サブルーチンをNormalにする

                        GL.Clear(ClearBufferMask.StencilBufferBit);//ステンシルバッファークリア
                        GL.ColorMask(false, false, false, false); //色は全くかきこまない
                        GL.Enable(EnableCap.CullFace);//CullFace有効
                        GL.StencilFunc(StencilFunction.Always, 0, 0);//Stencil Funcを設定 (Always)
                        GL.UseProgram(Program); // (260319Ch) clip.Render() が別 program を使った後でも元へ戻す
                        clip.EnableClips(Program, [i]);//i番目のクリップのみ有効化
                                                      //裏面のみ描画(ステンシル値だけ書き込む)
                        GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.IncrWrap);//ステンシル値「+1」
                        GL.CullFace(TriangleFace.Front); //260317Cl CullFaceMode→TriangleFace 表面をカリング
                        
                      
                        Render();
                        //表面のみ描画(→差分をとってマスク画像にする)
                        GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.DecrWrap);//ステンシル値「-1」
                        GL.CullFace(TriangleFace.Back);//260317Cl CullFaceMode→TriangleFace 裏面をカリング
                        Render();
                        //ここまででステンシル完成(0以外が有効)

                        //ここからクリップ平面を描画
                        // if (Location[(ContextKey, Program)].PassPPLL1Index != -1)//PPLLモードの場合は
                        // if (Location[(ContextKey, Program)].PassPPLL1Index != -1 && CurrentFragmentRenderPassIndex == Location[(ContextKey, Program)].PassPPLL1Index)
                        //     GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref Location[(ContextKey, Program)].PassPPLL1Index);//サブルーチンをPPLL1に戻す
                        if (activeRenderPassIndex != -1)
                            GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref activeRenderPassIndex); // (260319Ch) PPLL だけでなく DDP でも clip plane を現在の render pass へ戻す

                        GL.ColorMask(true, true, true, true);
                        GL.StencilFunc(StencilFunction.Notequal, 0, ~0); //ステンシル値が0でない部分が描画(切断面の描画). ~0 は補数(11111111)
                        GL.Disable(EnableCap.CullFace);//CullFace無効化
                        GL.UseProgram(Program); // (260319Ch) clip plane 描画後に対象 object の program へ戻す
                        clip.EnableClips(Program, indices.Where(j => j != i));//i番目のクリップ以外を有効化
                        // DepthTest(Location[(ContextKey, Program)].PassNormalIndex == -1);//Depthテストを元に戻す (Z-sortモードは有効)
                        // var restoreDepthTest = Location[(ContextKey, Program)].PassNormalIndex == -1 || CurrentFragmentRenderPassIndex == Location[(ContextKey, Program)].PassNormalIndex;
                        // DepthTest(restoreDepthTest); // (260319Ch) ZSORT/PPLL prepass/PPLL pass1 で元の depth state を正しく復元する
                        DepthTest(CurrentDepthTestEnabled); // (260319Ch) ZSORT / PPLL / DDP すべてで現在の phase の depth state をそのまま復元する
                        clip.Render(i, Material);//i番目のクリップ面を描画
                    }
                    GL.Disable(EnableCap.StencilTest);//Stencilテスト無効化
                    GL.UseProgram(Program); // (260319Ch) 最後の本体描画前に program を戻す
                    clip.EnableClips(Program, indices);//全クリップ有効化
                    Render(); //物体全体の描画
                    Clip.DisableAllClips();//全クリップ無効化
                }
            }
        }
    }

    internal static readonly V3d vZ = new(0, 0, 1);

    public static void DepthTest(bool flag)
    {
        GLable(flag, EnableCap.DepthTest);
        GL.DepthMask(flag);
    }

    public static void GLable(bool flag, EnableCap cap)
    {
        if (flag) GL.Enable(cap);
        else GL.Disable(cap);
    }
}
#endregion

#region クリップ
/// <summary>描画範囲をクリップ(切り取る)する.</summary>
public class Clip
{
    private readonly List<Quads> Planes = [];
    private readonly List<float> PrmsF = [];
    public List<V4d> PrmsD = [];
    private int Program = 0;
    private readonly Dictionary<int, (int ClipPlanesLocation, int ClipNumLocation)> ProgramLocations = []; // (260319Ch) mesh/text で uniform location を分けて保持する

    public Clip(params V4d[] planeParams)
    {
        for (int i = 0; i < planeParams.Length; i++)
        {
            var prms = planeParams[i] / new V3d(planeParams[i]).Length;
            var norm = new V3d(prms);
            var rot = GLGeometry.CreateRotationFromZ(norm);

           //var rot2 =  rot * norm;

            var pts = new[] { new V3d(100, 0, 0), new V3d(0, 100, 0), new V3d(-100, 0, 0), new V3d(0, -100, 0) };
            for (int j = 0; j < pts.Length; j++)
                pts[j] = rot * pts[j] - norm * prms.W * 1.0005;

            var obj = new Quads(pts[0], pts[1], pts[2], pts[3], new Material(0), DrawingMode.Surfaces)
            {
                UseFixedArgb = true,
                IgnoreNormalSides = false,
                Primitives = [(PT.TriangleFan, 0)]
            };
            Planes.Add(obj);
            PrmsF.AddRange(prms.ToArrayF());
            PrmsD.Add(prms);
        }
    }

    public void Generate(int program, bool generateRenderPlanes = true)
    {
        if (generateRenderPlanes)
        {
            Program = program;
            Planes.ForEach(p => p.Generate(program));
        }

        ProgramLocations[program] = (GL.GetUniformLocation(program, "ClipPlanes"), GL.GetUniformLocation(program, "ClipNum"));
    }

    public void Render(int index, Material mat)
    {
        Planes[index].Material = mat;
        Planes[index].Render();
    }

    public void EnableClips(int program, IEnumerable<int> indices)
    {
        if (!ProgramLocations.TryGetValue(program, out var locations))
            return;

        // int count = indices.Count();
        var enabledIndices = indices as int[] ?? indices.ToArray(); // (260319Ch) Count/Contains の多重列挙を避ける
        int count = enabledIndices.Length;
        GL.Uniform1(locations.ClipNumLocation, count);

        for (int i = 0; i < count; i++)
            GL.Enable(EnableCap.ClipDistance0 + i);
        for (int i = count; i < 8; i++)
            GL.Disable(EnableCap.ClipDistance0 + i);

        if (count > 0)
        {
            var prmsSpan = CollectionsMarshal.AsSpan(PrmsF);
            var clipPlanes = ArrayPool<float>.Shared.Rent(count * 4); // (260319Ch) 頻繁なクリップ切替の短命配列をプールする
            try
            {
                for (int i = 0; i < count; i++)
                {
                    int srcOffset = enabledIndices[i] * 4;
                    int dstOffset = i * 4;
                    clipPlanes[dstOffset] = prmsSpan[srcOffset];
                    clipPlanes[dstOffset + 1] = prmsSpan[srcOffset + 1];
                    clipPlanes[dstOffset + 2] = prmsSpan[srcOffset + 2];
                    clipPlanes[dstOffset + 3] = prmsSpan[srcOffset + 3];
                }
                GL.Uniform4(locations.ClipPlanesLocation, count, clipPlanes);
            }
            finally
            {
                ArrayPool<float>.Shared.Return(clipPlanes, clearArray: false);
            }
        }
    }
    public static void DisableAllClips()
    {
        for (int i = 0; i < 8; i++)
            GL.Disable(EnableCap.ClipDistance0 + i);
    }
}
#endregion

#region 線分
public class Lines : GLObject
{
    public Lines(V3d[] vertices, float lineWidth, Material mat) : base(mat, DrawingMode.Edges)
    {
        ShowClippedSection = false;//クリップ断面は表示しない
        IgnoreNormalSides = true;//裏表を無視する
        LineWidth = lineWidth;
        var center = TkEx.Average(vertices);// new V3d(vertices.Average(v => v.X), vertices.Average(v => v.Y), vertices.Average(v => v.Z));
        CircumscribedSphereCenter = new V4d(center, 1);
        CircumscribedSphereRadius = vertices.Max(v => (v - center).Length);
        Vertices = vertices.Select(v => new Vertex(v.ToV3f(), mat.Argb)).ToArray();
        Indices = ValueEnumerable.Range(0, vertices.Length).Select(i => (uint)i).ToArray();
        Primitives = [(PT.LineStrip, vertices.Length)];
    }
}

#endregion

#region 三角形、四角形、円板、多角形
/// <summary>多角形 (凸多角形) 点集合が完全に平面に乗らない場合でも、最小二乗法で法線を求める</summary>
public class Polygon : GLObject
{
    public Polygon(Material mat, DrawingMode mode) : base(mat, mode) { }

    public Polygon(Vector3DBase[] vertices, Material mat, DrawingMode mode)
        : this(vertices.Select(v => new V3d(v.X, v.Y, v.Z)).ToArray(), mat, mode) { }

    public Polygon(Material mat, DrawingMode mode, params V3d[] vertices) : this(vertices.ToArray(), mat, mode) { }

    /// <summary></summary>
    /// <param name="vertices"></param>
    /// <param name="mat"></param>
    /// <param name="mode"></param>
    //260317Cl 変更: vertices.Count()の繰り返し呼出しを回避するためToArray具体化
    public Polygon(IEnumerable<V3d> vertices, Material mat, DrawingMode mode) : base(mat, mode)
    {
        ShowClippedSection = false;//クリップ断面は表示しない
        IgnoreNormalSides = true;//裏表を無視する

        var vecs = vertices as V3d[] ?? vertices.ToArray();
        if (vecs.Length == 3)//三角形の場合は特別処理
        {
            var center = (vecs[0] + vecs[1] + vecs[2]) / 3;
            CircumscribedSphereCenter = new V4d(center, 1);
            CircumscribedSphereRadius = vecs.Max(v => (v - center).Length);
            var normF = V3d.Cross(vecs[0] - vecs[1], vecs[1] - vecs[2]).ToV3f();
            Vertices = [.. vecs.Select(p => new Vertex(p.ToV3f(), normF, mat.Argb))];
            Indices = [0, 1, 2, 0, 1, 2, 0, 1, 2];
            Primitives = new (PT Type, int Count)[3];
            Primitives[0] = (PT.Triangles, 3);//surfaces
            Primitives[1] = (PT.LineLoop, 3);//edges
            Primitives[2] = (PT.Points, 3);//points
        }
        else
        {
            var center = TkEx.Average(vecs);// new V3d(vecs.Average(v => v.X), vecs.Average(v => v.Y), vecs.Average(v => v.Z));
            CircumscribedSphereCenter = new V4d(center, 1);
            CircumscribedSphereRadius = vecs.Max(v => (v - center).Length);
            var polygonInfo = GLGeometry.PolygonInfo(vecs, V3d.Zero);
            var normF = polygonInfo.Norm.ToV3f();
            var centerF = polygonInfo.Center.ToV3f();
            var indicesList = polygonInfo.Indices;
            indicesList.Insert(0, (uint)vecs.Length);
            var indicesArray = indicesList.ToArray();

            //最終処理
            Vertices = new Vertex[vecs.Length + 1];
            vecs.Select(p => new Vertex(p.ToV3f(), normF, mat.Argb)).ToArray().AsSpan().CopyTo(Vertices.AsSpan(0, vecs.Length));
            Vertices[^1] = new Vertex(centerF, normF, mat.Argb);

            Indices = new uint[indicesArray.Length * 3 - 4];
            Primitives = new (PT Type, int Count)[3];
            //260317Cl 変更: Array.Copy → Span.CopyTo
            //surfaces
            indicesArray.AsSpan().CopyTo(Indices.AsSpan(0, indicesArray.Length));
            Primitives[0] = (PT.TriangleFan, indicesList.Count);
            //edges
            indicesArray.AsSpan(2).CopyTo(Indices.AsSpan(indicesArray.Length, indicesArray.Length - 2));
            Primitives[1] = (PT.LineLoop, indicesList.Count - 2);
            //points
            indicesArray.AsSpan(2).CopyTo(Indices.AsSpan(indicesArray.Length * 2 - 2, indicesArray.Length - 2));
            Primitives[2] = (PT.Points, indicesList.Count - 2);
        }
    }
    /// <summary>多角形を分解する (Quadsにして返す)</summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public Polygon[] Decompose(int order = 1)
    {
        //ここから本体

        V3d[] inputs;
        if (Primitives[0].Type == PT.Triangles)
            inputs = [.. Vertices.Select(v => v.Position.ToV3d())];
        else
        {
            inputs = new V3d[Primitives[0].Count - 2];
            for (int i = 1, n = 0; i < Primitives[0].Count - 1; i++, n++)
                inputs[n] = Vertices[Indices[i]].Position.ToV3d();
        }

        var outputs = decompose([.. inputs], order);

        var results = new Polygon[outputs.Length];
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = new Polygon(Material, Mode)
            {
                ShowClippedSection = false,//クリップ断面は表示しない
                IgnoreNormalSides = true,//裏表を無視する
                Vertices = outputs[i].Select(v => new Vertex(v.ToV3f(), Vertices[0].Normal, Vertices[0].Argb)).ToArray(),
                //260317Cl 変更: Enumerable.Range → ValueEnumerable.Range
                Indices = ValueEnumerable.Range(0, outputs[i].Length).Select(val => (uint)val).ToArray()
            };

            results[i].Primitives = [(PT.Triangles, results[i].Indices.Length)];

            var center = TkEx.Average(outputs[i]);
            results[i].CircumscribedSphereCenter = new V4d(center, 1);
            results[i].CircumscribedSphereRadius = outputs[i].Max(v => (v - center).Length);

            results[i].Tag = Tag;
            results[i].Rendered = Rendered;

            //results[i].Material.Color = new C4((float)rn.NextDouble(), (float)rn.NextDouble(), (float)rn.NextDouble(), 1);
        }
        return results;
    }


    /// <summary>Decomposeから呼びばれる再帰的関数</summary>
    /// <param name="srcVertex"></param>
    /// <param name="ord"></param>
    /// <returns></returns>
    static V3d[][] decompose(V3d[] srcVertex, int ord)
    {
        if (ord == 0)//ゼロの場合、これ以上分解しない
            return [srcVertex];
        else
        {
            //頂点と、頂点間の中点を、交互に追加. 
            var newVertices = new List<V3d>(srcVertex.Length + 1)
            {
                (srcVertex[^1] + srcVertex[0]) / 2,
                srcVertex[0]
            };
            for (int i = 1; i < srcVertex.Length; i++)
            {
                newVertices.Add((srcVertex[i - 1] + srcVertex[i]) / 2);
                newVertices.Add(srcVertex[i]);
            }

            var center = TkEx.Average(srcVertex);//中心を算出

            //中心と新しい頂点を組み合わせて、3角形を作る
            var resultVertices = new List<V3d[]>();
            for (int i = 0; i < newVertices.Count; i++)
            {
                var j = i < newVertices.Count - 1 ? i + 1 : 0;
                resultVertices.Add([center, newVertices[i], newVertices[j]]);
            }
            //新しく出来た頂点群を再帰的に分割する
            return [.. resultVertices.SelectMany(v => decompose(v, ord - 1))];
        }
    }
}

/// <summary>三角形</summary>
/// <remarks>
/// 三角形
/// </remarks>
/// <param name="a">頂点a</param>
/// <param name="b">頂点b</param>
/// <param name="c">頂点c</param>
/// <param name="mat"></param>
/// <param name="mode"></param>
public class Triangle(V3d a, V3d b, V3d c, Material mat, DrawingMode mode) : Polygon([a, b, c], mat, mode)
{
    public Triangle(Vector3DBase a, Vector3DBase b, Vector3DBase c, Material mat, DrawingMode mode)
   : this(new V3d(a.X, a.Y, a.Z), new V3d(b.X, b.Y, b.Z), new V3d(c.X, c.Y, c.Z), mat, mode) { }
}

/// <summary>四角形</summary>
public class Quads : Polygon
{
    public Quads(Vector3DBase a, Vector3DBase b, Vector3DBase c, Vector3DBase d, Material mat, DrawingMode mode)
    : this(new V3d(a.X, a.Y, a.Z), new V3d(b.X, b.Y, b.Z), new V3d(c.X, c.Y, c.Z), new V3d(d.X, d.Y, d.Z), mat, mode) { }

    public Quads(V3d a, V3d b, V3d c, V3d d, Material mat, DrawingMode mode)
        : base([a, b, c, d], mat, mode) { }

    public Quads(V3d[] vertices, Material mat, DrawingMode mode)
        : base(vertices, mat, mode) { }
}

/// <summary>円 (ディスク)</summary>
public class Disk(V3d origin, V3d normal, double radius, Material mat, DrawingMode mode, int slices = 60) : Polygon(
         //260317Cl 変更: Enumerable.Range → ValueEnumerable.Range
         ValueEnumerable.Range(0, slices).Select(i =>
             {
                var (sin, cos)= Math.SinCos((double)i / slices * 2 * Math.PI);
                 var p = radius * new V2d(sin, cos);
                 M3d rot;
                 if (normal == vZ)
                     rot = M3d.Identity;
                 else
                     rot = M3d.CreateFromAxisAngle(V3d.Cross(normal, vZ), V3d.CalculateAngle(vZ, normal));
                 var x = rot.M11 * p.X + rot.M12 * p.Y + origin.X;
                 var y = rot.M21 * p.X + rot.M22 * p.Y + origin.Y;
                 var z = rot.M31 * p.X + rot.M32 * p.Y + origin.Z;
                 return new V3d(x, y, z);
             }).ToArray(), mat, mode)
{
    public Disk(Vector3DBase origin, Vector3DBase normal, double radius, Material mat, DrawingMode mode, int slices = 60)
        : this(new V3d(origin.X, origin.Y, origin.Z), new V3d(normal.X, normal.Y, normal.Z), radius, mat, mode, slices) { }

    public Disk(V3d origin, V3d normal, double radius, float lineWidth, Material mat, DrawingMode mode, int slices = 60)
        : this(origin, normal, radius, mat, mode, slices)
    { LineWidth = lineWidth; }
}

/// <summary>穴あきディスク</summary>
public class HoledDisk : GLObject
{
    public double RadiusInner, RadiusOuter;

    public V3d Origin, Normal;
    public HoledDisk(V3d origin, V3d normal, double radius1, double radius2, Material mat, DrawingMode mode, int slices = 60):base(mat,mode)
    {
        RadiusInner=Math.Min(radius1, radius2);
        RadiusOuter=Math.Max(radius1, radius2);
        Origin = origin;
        Normal = normal;

        CircumscribedSphereCenter = new V4d(origin, 1);
        CircumscribedSphereRadius = RadiusOuter;

        //260317Cl 変更: Enumerable.Range → ValueEnumerable.Range
        var t = ValueEnumerable.Range(0, slices).Select(i => Math.SinCos((double)i / slices * 2 * Math.PI)).ToArray();
        //var coss = Enumerable.Range(0, slices).Select(i => Math.Cos((double)i / slices * 2 * Math.PI)).ToArray();

        IgnoreNormalSides = true;

        M3d rotMat;
        if (normal == vZ)
            rotMat = M3d.Identity;
        else
            rotMat = M3d.CreateFromAxisAngle(V3d.Cross(normal, vZ), V3d.CalculateAngle(vZ, normal));

        var vertices = new V3d[slices * 2];

        for (int i = 0; i < slices; i++)
        {
            vertices[i] = new V3d(RadiusOuter * t[i].Sin, RadiusOuter * t[i].Cos, 0);
            vertices[i+slices] = new V3d(RadiusInner * t[i].Sin, RadiusInner * t[i].Cos, 0);
        }

        List<int> indicesTmp = [];
        for (int i = 0; i < slices; i++)
        {
            if (i < slices - 1) {
                //indicesTmp.AddRange([i, i + 1, i + slices + 1, i + slices]);
                indicesTmp.AddRange([i, i + 1, i + slices + 1]);
                indicesTmp.AddRange([i, i + slices + 1, i + slices]);
            }
            else { 
                //indicesTmp.AddRange([i, 0, slices + 1, i + slices]);
                indicesTmp.AddRange([i, 0, slices + 1]);
                indicesTmp.AddRange([i, slices + 1, i + slices]);
            }
        }
        var types = new List<PT>();
        var indices = new List<int[]>();

        types.Add(PT.Triangles);
        indices.Add([.. indicesTmp]);

        types.Add(PT.LineLoop);
        indices.Add([.. indicesTmp]);

        types.Add(PT.Points);
        indices.Add([.. ValueEnumerable.Range(0, vertices.Length)]);

        Vertices = new Vertex[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
            Vertices[i] = new Vertex((rotMat * vertices[i] + origin).ToV3f(), normal.ToV3f(), mat.Argb);

        Indices = indices.SelectMany(i => i).Select(i => (uint)i).ToArray();

        Primitives = types.Select((t, i) => (t, indices[i].Length)).ToArray();
    }

    public HoledDisk(Vector3DBase origin, Vector3DBase normal, double radius1, double radius2, Material mat, DrawingMode mode, int slices = 60)
        : this(new V3d(origin.X, origin.Y, origin.Z), new V3d(normal.X, normal.Y, normal.Z), radius1, radius2, mat, mode, slices) { }

    public HoledDisk(V3d origin, V3d normal, double radius1, double radius2, float lineWidth, Material mat, DrawingMode mode, int slices = 60)
        : this(origin, normal, radius1, radius2, mat, mode, slices)
    { LineWidth = lineWidth; }
}

#endregion

#region 多面体、立方体
/// <summary>多面体 (凸多面体)</summary>
public class Polyhedron : GLObject
{
    public Polyhedron(IEnumerable<Vector3DBase> vertices, Material mat, DrawingMode mode)
        : this(vertices.Select(v => new V3d(v.X, v.Y, v.Z)), mat, mode) { }

    /// <summary></summary>
    /// <param name="vertices"></param>
    /// <param name="mat"></param>
    /// <param name="mode"></param>
    public Polyhedron(IEnumerable<V3d> vertices, Material mat, DrawingMode mode) : base(mat, mode)
    {
        var center = TkEx.Average(vertices);
        CircumscribedSphereCenter = new V4d(center, 1);
        CircumscribedSphereRadius = vertices.Max(v => (v - center).Length);

        //任意の三点を選び、平面方程式を作り、それらが最も端面であるかを評価し、端面である場合はリストに加える
        var candidates = new List<V3d[]>();

        var vrs = vertices.ToArray();
        for (int i = 0; i < vrs.Length - 2; i++)
            for (int j = i + 1; j < vrs.Length - 1; j++)
                for (int k = j + 1; k < vrs.Length; k++)
                {
                    V3d A = vrs[i], B = vrs[j], C = vrs[k];
                    var V = V3d.Cross(C - A, A - B);

                    if (vrs.All(v => V3d.Dot(v - A, V) < 0.0000001) || vrs.All(v => V3d.Dot(v - A, V) > -0.0000001))
                        if (candidates.All(cand => !(cand.Contains(A) && cand.Contains(B) && cand.Contains(C))))
                            candidates.Add([.. vrs.Where(v => Math.Abs(V3d.Dot(v - A, V)) < 0.0000001)]);
                }

        //各面を構成する頂点集合に対して
        var vList = new List<Vertex>();
        var iList2 = new List<List<uint>>();
        var types = new List<PT>();
        var offset = (uint)0;
        foreach (var cand in CollectionsMarshal.AsSpan(candidates))
        {
            if (cand.Length == 3)
            {
                var norm = V3d.Cross(cand[0] - cand[1], cand[2] - cand[1]).ToV3f();
                vList.AddRange(cand.Select(p => new Vertex(p.ToV3f(), norm, mat.Argb)));//多面体頂点を追加

                for(int i = 0;i<3; i++)
                    iList2.Add([offset + 0, offset + 1, offset + 2]);
                types.AddRange([PT.Triangles, PT.LineLoop, PT.Points]);
                offset += 3;
            }
            else
            {
                var polygonInfo = GLGeometry.PolygonInfo(cand, center);

                vList.AddRange(cand.Select(p => new Vertex(p.ToV3f(), polygonInfo.Norm.ToV3f(), mat.Argb)));//多面体頂点を追加
                vList.Add(new Vertex(polygonInfo.Center.ToV3f(), polygonInfo.Norm.ToV3f(), mat.Argb));//多面体中心を追加

                var iTemp = new List<uint>([(uint)(vList.Count - 1)]);//多面体中心のインデックスを追加
                var offsetIndices = polygonInfo.Indices.Select(n => n + offset).ToList();
                iTemp.AddRange(offsetIndices);//多面体頂点のインデックスを追加

                iList2.Add(iTemp);
                types.Add(PT.TriangleFan);

                offsetIndices.RemoveAt(0);
                iList2.Add(offsetIndices);
                iList2.Add(offsetIndices);
                types.Add(PT.LineLoop);
                types.Add(PT.Points);

                offset += (uint)(cand.Length + 1);
            }
        }
        IgnoreNormalSides = true;

        Vertices = [.. vList];
        Indices = [.. iList2.SelectMany(i => i)];

        Primitives = [.. types.Select((t, i) => (t, iList2[i].Count))];
    }

    /// <summary>
    /// PolyhedronをPolygonに分解する.
    /// orderを指定すると、分解したPolygonをさらに分割する.
    /// </summary>
    public Polygon[] ToPolygons(int order = 0)
    {
        var p = new Polygon[Primitives.Length / 3];

        for (int i = 0, offsetIndices = 0, offsetVertices = 0; i < p.Length; i++)
        {
            p[i] = new Polygon(Material, Mode) { Primitives = [Primitives[i * 3], Primitives[i * 3 + 1], Primitives[i * 3 + 2]] };

            p[i].Indices = new uint[p[i].Primitives.Sum(o => o.Count)];

            Array.Copy(Indices, offsetIndices, p[i].Indices, 0, p[i].Indices.Length);

            p[i].Indices = [.. p[i].Indices.Select(j => (uint)(j - offsetVertices))];
            offsetIndices += p[i].Indices.Length;

            p[i].Vertices = new Vertex[p[i].Indices.Distinct().Count()];
            Array.Copy(Vertices, offsetVertices, p[i].Vertices, 0, p[i].Vertices.Length);
            offsetVertices += p[i].Vertices.Length;

            var center = TkEx.Average(p[i].Vertices.Select(e => e.Position));
            p[i].CircumscribedSphereCenter = new V4d(center, 1);
            p[i].CircumscribedSphereRadius = p[i].Vertices.Max(v => (v.Position - center).Length);
            p[i].Rendered = Rendered;
            p[i].IgnoreNormalSides = true;
            p[i].ShowClippedSection = false;
            p[i].Tag = Tag;
        }
        if (order == 0)
            return p;
        else
        {
            var polygons = p.SelectMany(o => o.Decompose(order)).ToArray();
            return polygons;
        }
    }
}

/// <summary>平行六面体(原点と3辺のベクトルで定義)</summary>
public class Parallelepiped(V3d o, V3d a, V3d b, V3d c, Material mat, DrawingMode mode)
    : Polyhedron([o, o + a, o + b, o + c, o + a + b, o + b + c, o + c + a, o + a + b + c], mat, mode)
{
}

#endregion

#region 球体 楕円球、真球
/// <summary>楕円球 (原点と3方向のベクトルで定義される).  6*(2*slices+1)^2 の頂点が生成される</summary>
public class Ellipsoid : GLObject
{
    public static int DefaultSlices = 2;

    public V3d Origin { get; set; }
    public V3d RadiusVector1 { get; set; }
    public V3d RadiusVector2 { get; set; }
    public V3d RadiusVector3 { get; set; }

    public Ellipsoid(Vector3DBase o, Vector3DBase a, Vector3DBase b, Vector3DBase c, Material mat, DrawingMode mode, int slices = 0)
    : this(new V3d(o.X, o.Y, o.Z), new V3d(a.X, a.Y, a.Z), new V3d(b.X, b.Y, b.Z), new V3d(c.X, c.Y, c.Z), mat, mode, slices) { }

    /// <summary>楕円球 (中心位置と3方向のベクトルで定義される).  6*(2*slices+1)^2 の頂点が生成される</summary>
    /// <param name="o">中心位置</param>
    /// <param name="v1">中心位置からのベクトル1</param>
    /// <param name="v2">中心位置からのベクトル2</param>
    /// <param name="v3">中心位置からのベクトル3</param>
    /// <param name="mat">素材</param>
    /// <param name="mode">描画モード</param>
    /// <param name="slices">分割数. 6*(2*slices+1)^2 の頂点が生成される. </param>
    public Ellipsoid(in V3d o, in V3d v1, in V3d v2, in V3d v3, Material mat, DrawingMode mode, int slices = 0) : base(mat, mode)
    {
        Origin = o;
        RadiusVector1 = v1;
        RadiusVector2 = v2;
        RadiusVector3 = v3;

        CircumscribedSphereCenter = new V4d(o, 1);
        CircumscribedSphereRadius = new[] { v1.Length, v2.Length, v3.Length }.Max();

        if (slices == 0)
        {
            if (v1.LengthSquared == v2.LengthSquared && v2.LengthSquared == v3.LengthSquared)
            {
                if (Sphere.DefaultIndices != null)
                {
                    Vertices = Sphere.DefaultVertices;
                    Indices = Sphere.DefaultIndices;
                    Primitives = Sphere.DefaultPrimitives;
                    ObjectMatrix = new M4d(new V4d(v1, 0), new V4d(v2, 0), new V4d(v3, 0), new V4d(o, 1)).ToM4f();
                    return;
                }
                else
                    slices = Sphere.DefaultSlices;
            }
            else
                slices = DefaultSlices;
        }

        var transMat = new M4d { Column0 = new V4d(v1, 0), Column1 = new V4d(v2, 0), Column2 = new V4d(v3, 0), Column3 = new V4d(o, 1) };

        //さいころの6面方向
        M3d[] rot = [
                new M3d(1, 0, 0, 0, 1, 0, 0, 0, 1),
                new M3d(1, 0, 0, 0, 0, -1, 0, 1, 0),
                new M3d(1, 0, 0, 0, -1, 0, 0, 0, -1),
                new M3d(1, 0, 0, 0, 0, 1, 0, -1, 0),
                new M3d(0, 0, 1, 0, 1, 0, -1, 0, 0),
                new M3d(0, 0, -1, 0, 1, 0, 1, 0, 0), ];

        Vertices = new Vertex[rot.Length * (slices * 2 + 1) * (slices * 2 + 1)];
        for (int i = 0, j = 0; i < rot.Length; i++)
            for (int h = -slices; h <= slices; h++)
                for (int w = -slices; w <= slices; w++)
                {
                    var n = new V4d(V3d.Normalize(rot[i] * new V3d(w, h, slices)), 1);
                    var v = transMat * n;
                    Vertices[j++] = new Vertex(v.ToV3f(), n.ToV3f(), mat.Argb);
                }

        var types = new List<PT>(3);
        var indices = new List<int[]>();

        types.Add(PT.Points);
        //260317Cl 変更: Enumerable.Range → ValueEnumerable.Range
        indices.Add([.. ValueEnumerable.Range(0, Vertices.Length)]);

        var indexListSurfaces = new List<int>(16 * rot.Length * slices * slices);
        var indexListEdges = new List<int>(rot.Length * 8 * (slices * slices * 2 + slices));
        for (int i = 0; i < rot.Length; i++)
            for (int h = 0; h < 2 * slices; h++)
                for (int w = 0; w < 2 * slices; w++)
                {
                    int current = i * (2 * slices + 1) * (2 * slices + 1) + h * (2 * slices + 1) + w;
                    //indexListSurfaces.AddRange([current, current + 1, current + 2 * slices + 2, current + 2 * slices + 1]);
                    indexListSurfaces.AddRange([current, current + 1, current + 2 * slices + 2]);
                    indexListSurfaces.AddRange([current,  current + 2 * slices + 2, current + 2 * slices + 1]);

                    indexListEdges.AddRange([current, current + 1, current, current + 2 * slices + 1]);
                    if (h == 2 * slices - 1)
                        indexListEdges.AddRange([current + 2 * slices + 2, current + 2 * slices + 1]);
                    if (w == 2 * slices - 1)
                        indexListEdges.AddRange([current + 1, current + 2 * slices + 2]);
                }
        types.Add(PT.Triangles);
        indices.Add([.. indexListSurfaces]);

        types.Add(PT.Lines);
        indices.Add([.. indexListEdges]);

        Indices = indices.AsValueEnumerable().SelectMany(i => i).Select(i => (uint)i).ToArray();
        Primitives = types.AsValueEnumerable().Select((t, i) => (t, indices[i].Length)).ToArray();
    }
}

/// <summary>Spehere 球体 (原点と半径で定義される)</summary>
public class Sphere : Ellipsoid
{
    public double Radius;
    public bool UseDefault { get; set; } = false;

    public Sphere(Vector3DBase o, double radius, Material mat, DrawingMode mode, int slices = 0)
      : this(new V3d(o.X, o.Y, o.Z), radius, mat, mode, slices) { }

    /// <summary>球体 (原点と半径で定義される)　6*(2*slices+1)^2 の頂点が生成される</summary>
    /// <param name="o">中心位置</param>
    /// <param name="radius">半径</param>
    /// <param name="mat">素材</param>
    /// <param name="mode">描画モード</param>
    /// <param name="slices">分割数.　6*(2*slices+1)^2 の頂点が生成される</param>
    public Sphere(V3d o, double radius, Material mat, DrawingMode mode, int slices = 0)
       : base(o, new V3d(radius, 0, 0), new V3d(0, radius, 0), new V3d(0, 0, radius), mat, mode, slices)
    { Radius = radius; UseDefault = slices == 0; }

    public new static int DefaultSlices { get => defaultSlices; set { defaultSlices = value; SetDefaultSphere(); } }
    private static int defaultSlices = 3;

    public static Vertex[] DefaultVertices;
    public static uint[] DefaultIndices;
    public static (PT Type, int Count)[] DefaultPrimitives;

    /// <summary>Default形状ついて、Program番号と(VBO, VAO, EBO)を対応付けるDictionary.</summary>
    // static public Dictionary<int, (int VBO, int VAO, int EBO)> DefaultDictionary { get; set; } = [];
    static public Dictionary<(int ContextKey, int Program), (int VBO, int VAO, int EBO)> DefaultDictionary { get; set; } = []; // (260319Ch) default sphere VAO は context 単位で分離

    static Sphere() => SetDefaultSphere();
    static void SetDefaultSphere()
    {
        DefaultVertices = null;
        DefaultIndices = null;
        DefaultPrimitives = null;
        var sphere = new Sphere(new V3d(0, 0, 0), 1, new Material(0), DrawingMode.Edges, DefaultSlices);
        DefaultIndices = sphere.Indices;
        DefaultVertices = sphere.Vertices;
        DefaultPrimitives = sphere.Primitives;
    }
}

#endregion

#region パイプ、円錐、円柱

/// <summary>
/// パイプ (始点の位置、始点から終点へのベクトル、始点側の半径, 終点側の半径 で定義される)
/// slicesは高さの分割数, stacksは経線の分割数
/// </summary>
public class Pipe : GLObject
{
    public static (int Slices, int Stacks) Default = (1, 16);

    public double Radius1, Radius2;
    public V3d Origin, Vector;

    public Pipe(Vector3DBase o, Vector3DBase vec, double r1, double r2, Material mat, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
        : this(new V3d(o.X, o.Y, o.Z), new V3d(vec.X, vec.Y, vec.Z), r1, r2, mat, mode, sole, slices, stacks) { }

    public Pipe(in V3d o, in V3d vec, in double r1, in double r2, Material mat, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
        : this(in o, in vec, in r1, in r2, mat, null, mode, sole, slices, stacks) { }

    /// <summary>パイプ</summary>
    /// <param name="o">始点の位置</param>
    /// <param name="vec">始点から終点へのベクトル</param>
    /// <param name="r1">始点側の半径</param>
    /// <param name="r2">終点側の半径</param>
    /// <param name="mat1">素材</param>
    /// <param name="mat2">素材 (指定しない場合はmat1と同じ, 指定した場合は半分がこれになる)</param>
    /// <param name="mode">描画モード</param>
    /// <param name="sole">trueの場合は底面を描画する</param>
    /// <param name="slices">高さの分割数</param>
    /// <param name="stacks">円周の分割数</param>
    public Pipe(in V3d o, in V3d vec, in double r1, in double r2, Material mat1, Material mat2, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
        : base(mat1, mode)
    {
        ShowClippedSection = true;
        Origin = o;
        Vector = vec;
        var rotMat = GLGeometry.CreateRotationFromZ(vec);//回転行列を計算

        Radius1 = r1;
        Radius2 = r2;
        if (mat2 == null)
        {
            UseFixedArgb = true;
            mat2 = mat1;
        }
        else
            UseFixedArgb = false;

        var height = vec.Length;
        CircumscribedSphereCenter = new V4d(o + vec / 2.0, 1);
        //var maxR = Math.Max(r1, r2);
        CircumscribedSphereRadius = Math.Max(height / 2, Math.Max(r1, r2)); //Math.Sqrt(height * height / 4 + maxR * maxR);

        if (slices == 0 && stacks == 0)
        {
            if (r1 == r2)//Cylinderの場合
            {
                if (Cylinder.DefaultIndices != null && UseFixedArgb)
                {
                    Vertices = Cylinder.DefaultVertices;
                    Indices = Cylinder.DefaultIndices;
                    Primitives = Cylinder.DefaultPrimitives;
                    ObjectMatrix = new M4d(r1 * new V4d(rotMat.Column0, 0), r1 * new V4d(rotMat.Column1, 0), height * new V4d(rotMat.Column2, 0), new V4d(o, 1)).ToM4f();
                    return;
                }
                slices = Cylinder.Default.Slices;
                stacks = Cylinder.Default.Stacks;
            }
            else if (r2 == 0)//Coneの場合
            {
                if (Cone.DefaultIndices != null && UseFixedArgb)
                {
                    var transMat = new M4d(rotMat) * new M4d(r2, 0, 0, 0, 0, r2, 0, 0, 0, 0, vec.Length, 0, 0, 0, 0, 1);
                    transMat.Column3 = new V4d(o, 1);

                    Vertices = Cone.DefaultVertices.Select(vrs => new Vertex(transMat.ToM4f() * new V4f(vrs.Position), rotMat.ToM3f() * vrs.Normal, mat1.Argb)).ToArray();
                    Indices = Cone.DefaultIndices;
                    Primitives = Cone.DefaultPrimitives;
                    return;
                }
                slices = Cone.Default.Slices;
                stacks = Cone.Default.Stacks;
            }
            else
            {
                slices = Default.Slices;
                stacks = Default.Stacks;
            }
        }

        List<V3d> v = [], n = [];
        List<int> c = [];
        //まず側面
        for (int h = 0; h <= slices; h++)
            for (int t = 0; t < stacks; t++)
            {
                var (sin, cos)=Math.SinCos((double)t / stacks * Math.PI * 2);
                //double sin = Math.Sin((double)t / stacks * Math.PI * 2), cos = Math.Cos((double)t / stacks * Math.PI * 2);
                double r = r1 * (1 - (double)h / slices) + r2 * h / slices;
                double z = (double)h / slices * height;
                v.Add(new V3d(r * sin, r * cos, z));
                n.Add(new V3d(r2 * height * sin, r2 * height * cos, -r2 * (r2 - r1)));
                c.Add(h < slices / 2 ? mat1.Argb : mat2.Argb);
            }

        var current = 0;
        var indiceSide = new List<int>(4 * slices * stacks);
        for (int h = 0; h < slices; h++)
            for (int t = 0; t < stacks; t++)
            {
                current = h * stacks + t;
                if (t < stacks - 1)
                {
                    //indiceSide.AddRange([current, current + stacks, current + 1 + stacks, current + 1]);
                    indiceSide.AddRange([current, current + stacks, current + 1 + stacks]);
                    indiceSide.AddRange([current,  current + 1 + stacks, current + 1]);
                }
                else
                {
                    //indiceSide.AddRange([current, current + stacks, current + 1, current + 1 - stacks]);
                    indiceSide.AddRange([current, current + stacks, current + 1, ]);
                    indiceSide.AddRange([current,  current + 1, current + 1 - stacks]);
                }
            }
        var types = new List<PT>();
        var indices = new List<int[]>();

        types.Add(PT.Triangles);
        indices.Add([.. indiceSide]);

        types.Add(PT.LineLoop);
        indices.Add([.. indiceSide]);

        types.Add(PT.Points);
        indices.Add(ValueEnumerable.Range(0, v.Count).ToArray());

        //底面
        if (sole)
        {
            //始点側
            if (r1 > 0)
            {
                v.Add(new V3d(0, 0, 0));
                n.Add(-vZ);
                c.Add(mat1.Argb);
                var indicesTop = new List<int> { v.Count - 1 };
                var center = v.Count - 1;
                for (int t = 0; t < stacks; t++)
                {
                    v.Add(v[t]);
                    n.Add(-vZ);
                    c.Add(mat1.Argb);
                    indicesTop.Add(v.Count - 1);
                }
                indicesTop.Add(v.Count - stacks);
                types.Add(PT.TriangleFan);
                indices.Add([.. indicesTop]);
            }
            //終点側
            if (r2 > 0)
            {
                v.Add(new V3d(0, 0, height));
                n.Add(vZ);
                c.Add(mat2.Argb);
                var indicesBottom = new List<int> { v.Count - 1 };
                var center = v.Count - 1;
                for (int t = 0; t < stacks; t++)
                {
                    v.Add(v[(slices + 1) * stacks - t - 1]);
                    n.Add(vZ);
                    c.Add(mat2.Argb);
                    indicesBottom.Add(v.Count - 1);
                }
                indicesBottom.Add(v.Count - stacks);
                types.Add(PT.TriangleFan);
                indices.Add([.. indicesBottom]);
            }
        }

        Vertices = new Vertex[v.Count];
        for (int i = 0; i < v.Count; i++)
            Vertices[i] = new Vertex((rotMat * v[i] + o).ToV3f(), (rotMat * n[i]).ToV3f(), c[i]);

        Indices = indices.AsValueEnumerable().SelectMany(i => i).Select(i => (uint)i).ToArray();

        Primitives = types.Select((t, i) => (t, indices[i].Length)).ToArray();
    }
}

/// <summary>
/// 円錐 (頂点の位置、頂点からの底面中心のベクトル、半径で定義される)
/// slicesは高さの分割数, stacksは経線の分割数
/// </summary>
public class Cone : Pipe
{
    /// <summary>Default形状ついて、Program番号と(VBO, VAO, EBO)を対応付けるDictionary.</summary>
    // static public Dictionary<int, (int VBO, int VAO, int EBO)> DefaultDictionary { get; set; } = [];
    static public Dictionary<(int ContextKey, int Program), (int VBO, int VAO, int EBO)> DefaultDictionary { get; set; } = []; // (260319Ch) default cone VAO は context 単位で分離

    private static (int Slices, int Stacks) _Default = (1, 16);
    public new static (int Slices, int Stacks) Default
    {
        get => _Default;
        set
        {
            _Default = value;
            SetDefaultCone();
        }
    }


    public static Vertex[] DefaultVertices;
    public static uint[] DefaultIndices;
    public static (PT Type, int Count)[] DefaultPrimitives;


    public bool UseDefault = false;

    public Cone(Vector3DBase o, Vector3DBase vec, double r, Material mat, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
        : this(new V3d(o.X, o.Y, o.Z), new V3d(vec.X, vec.Y, vec.Z), r, mat, mode, sole, slices, stacks) { UseDefault = slices == 0; }

    /// <summary>円錐</summary>
    /// <param name="o">頂点の位置</param>
    /// <param name="vec">頂点から底面中心へのベクトル</param>
    /// <param name="r">底面の半径</param>
    /// <param name="mat">素材</param>
    /// <param name="mode">描画モード</param>
    /// <param name="sole">trueの場合は底面を描画する</param>
    /// <param name="slices">高さの分割数</param>
    /// <param name="stacks">経線の分割数</param>
    public Cone(in V3d o, in V3d vec, double r, Material mat, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
       : base(in o, in vec, 0, r, mat, mode, sole, slices, stacks)
    { }

    static Cone()
    {
        SetDefaultCone();
    }
    static void SetDefaultCone()
    {
        DefaultVertices = null;
        DefaultIndices = null;
        DefaultPrimitives = null;
        var cone = new Cone(new V3d(0, 0, 0), new V3d(0, 0, 1), 1, new Material(0), DrawingMode.Edges, true, 0, 0);
        DefaultIndices = cone.Indices;
        DefaultVertices = cone.Vertices;
        DefaultPrimitives = cone.Primitives;
    }

}

/// <summary>
/// 円柱 (始点の位置、始点から終点へのベクトル、半径で定義される)
/// slicesは高さの分割数, stacksは経線の分割数
/// </summary>
public class Cylinder : Pipe
{
    public bool UseDefault { get; set; } = false;
    public Cylinder(Vector3DBase o, Vector3DBase vec, double r, Material mat, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
       : this(new V3d(o.X, o.Y, o.Z), new V3d(vec.X, vec.Y, vec.Z), r, mat, mode, sole, slices, stacks) { }

    public Cylinder(Vector3DBase o, Vector3DBase vec, double r, Material mat1, Material mat2, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
       : this(new V3d(o.X, o.Y, o.Z), new V3d(vec.X, vec.Y, vec.Z), r, mat1, mat2, mode, sole, slices, stacks) { }

    /// <summary>円柱 (始点の位置、始点から終点へのベクトル、半径で定義される)</summary>
    /// <param name="o">始点の位置</param>
    /// <param name="vec">始点から終点へのベクトル</param>
    /// <param name="r">半径</param>
    /// <param name="mat">素材</param>
    /// <param name="mode">描画モード</param>
    /// <param name="sole">trueの場合は底面を描画する</param>
    /// <param name="slices">高さの分割数</param>
    /// <param name="stacks">経線の分割数</param>
    public Cylinder(in V3d o, in V3d vec, in double r, Material mat, in DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
       : base(in o, in vec, r, r, mat, mode, sole, slices, stacks) { UseDefault = slices == 0; }

    public Cylinder(in V3d o, in V3d vec, double r, Material mat1, Material mat2, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
     : base(in o, in vec, r, r, mat1, mat2, mode, sole, slices, stacks) { UseDefault = slices == 0; }


    /// <summary>Default形状ついて、Program番号と(VBO, VAO, EBO)を対応付けるDictionary.</summary>
    // static public Dictionary<int, (int VBO, int VAO, int EBO)> DefaultDictionary { get; set; } = [];
    static public Dictionary<(int ContextKey, int Program), (int VBO, int VAO, int EBO)> DefaultDictionary { get; set; } = []; // (260319Ch) default cylinder VAO は context 単位で分離

    public new static (int Slices, int Stacks) Default { get => _Default; set { _Default = value; SetDefaultCylinder(); } }
    private static (int Slices, int Stacks) _Default = (1, 16);

#pragma warning disable CA2211 // 非定数フィールドは表示されません
    public static Vertex[] DefaultVertices;
    public static uint[] DefaultIndices;
    public static (PT Type, int Count)[] DefaultPrimitives;
#pragma warning restore CA2211 // 非定数フィールドは表示されません

    static Cylinder() => SetDefaultCylinder();

    static void SetDefaultCylinder()
    {
        DefaultVertices = null;
        DefaultIndices = null;
        DefaultPrimitives = null;
        var cylinder = new Cylinder(new V3d(0, 0, 0), new V3d(0, 0, 1), 1, new Material(0), DrawingMode.Edges, true, 0, 0);
        DefaultIndices = cylinder.Indices;
        DefaultVertices = cylinder.Vertices;
        DefaultPrimitives = cylinder.Primitives;

    }
}

#endregion

#region Torus (ドーナッツ)
/// <summary>Torus (ドーナッツ).  中心(V3)、法線(V3), 大半径(double), 小半径(double)で定義される</summary>
public class Torus : GLObject
{
    public const int DefaultSlices1 = 45;
    public const int DefaultSlices2 = 10;

    public V3d Origin;
    public V3d Normal;
    public double Radius1;
    public double Radius2;

    /// <summary>Torus (ドーナッツ).  中心(V3)、法線(V3), 大半径(double), 小半径(double)で定義される</summary>
    /// <param name="o">中心位置</param>
    /// <param name="a">中心位置からのベクトル1</param>
    /// <param name="b">中心位置からのベクトル2</param>
    /// <param name="c">中心位置からのベクトル3</param>
    /// <param name="mat">素材</param>
    /// <param name="mode">描画モード</param>
    /// <param name="slices">分割数. 6*(2*slices+1)^2 の頂点が生成される. </param>
    public Torus(V3d origin, V3d norm, double r1, double r2, Material mat, DrawingMode mode, int slices1 = DefaultSlices1, int slices2 = DefaultSlices2) : base(mat, mode)
    {
        Origin = origin;
        Normal = norm.Normalized();
        Radius1 = r1;
        Radius2 = r2;

        CircumscribedSphereCenter = new V4d(Origin, 1);
        CircumscribedSphereRadius = r1 + r2;

        //まず、中心(r1,0,0)でx軸に直交し半径r2の円の頂点を計算
        var circleVertex = new List<(V3d Position, V3d Normal)>();
        for (int i = 0; i < slices2; i++)
        {
            var theta = (double)i / slices2 * 2 * Math.PI;
            var (sin,cos)=Math.SinCos(theta);
            var normal = r2 * new V3d(cos, 0, sin);
            var position = new V3d(r1, 0, 0) + normal;
            circleVertex.Add((position, normal));
        }

        //次に、この円をZ軸の周りに回転させてすべての頂点を計算
        for (int i = 1; i < slices1; i++)
        {
            var rot = M3d.CreateRotationZ((double)i / slices1 * 2 * Math.PI);
            for (int j = 0; j < slices2; j++)
                circleVertex.Add((rot * circleVertex[j].Position, rot * circleVertex[j].Normal));
        }
        //GLGeometry.
        //すべての頂点を座標変換し、vListに追加
        var vList = new List<Vertex>();
        //var transMat = M3d.CreateFromAxisAngle(V3d.Cross(vZ, Normal), V3d.CalculateAngle(Normal, vZ)).ToMatrix4D();
        var transMat = GLGeometry.CreateRotationToZ(norm).ToMatrix4d();
        transMat.Column3 = new V4d(origin, 1);

        for (int i = 0; i < circleVertex.Count; i++)
        {
            var position = transMat * circleVertex[i].Position.ToV4d();
            var normal = transMat * circleVertex[i].Normal.ToV4d();
            vList.Add(new Vertex(position.ToV3f(), normal.ToV3f(), mat.Argb));
        }
        //最後に、surfacesやedgeを計算しやすくするため、最初のslices個分の頂点を追加
        for (int i = 0; i < slices2; i++)
            vList.Add(vList[i]);

        Vertices = [.. vList];

        var types = new List<PT>();
        var indices = new List<int[]>();

        //Points
        types.Add(PT.Points);
        //260317Cl 変更: Enumerable.Range → ValueEnumerable.Range
        indices.Add(ValueEnumerable.Range(0, Vertices.Length).ToArray());

        //SurfacesとEdges
        var indexListSurfaces = new List<int>();
        var indexListEdges = new List<int>();
        for (int i = 0; i < slices1; i++)
            for (int j = 0; j < slices2 - 1; j++)
            {
                int current = i * slices2 + j;

                //indexListSurfaces.AddRange([current, current + 1, current + slices2 + 1, current + slices2]);
                indexListSurfaces.AddRange([current, current + 1, current + slices2 + 1]);
                indexListSurfaces.AddRange([current, current + slices2 + 1, current + slices2]);
                indexListEdges.AddRange([current, current + 1, current, current + slices2]);

                if (j == 0)
                {
                    //indexListSurfaces.AddRange([current, current + slices2, current + 2 * slices2 - 1, current + slices2 - 1]);
                    indexListSurfaces.AddRange([current, current + slices2, current + 2 * slices2 - 1]);
                    indexListSurfaces.AddRange([current, current + 2 * slices2 - 1, current + slices2 - 1]);
                    indexListEdges.AddRange([current, current + slices2, current, current + slices2 - 1]);
                }
            }
        types.Add(PT.Triangles);
        indices.Add([.. indexListSurfaces]);

        types.Add(PT.Lines);
        indices.Add([.. indexListEdges]);

        Indices = [.. indices.SelectMany(i => i).Select(i => (uint)i)];

        Primitives = [.. types.Select((t, i) => (t, indices[i].Length))];
    }
}
#endregion

#region メッシュ
/// <summary>メッシュ</summary>
public class Mesh : GLObject
{
    public Mesh(double[] data, int width, Material mat, DrawingMode mode) : base(mat, mode)
    {
        var max = data.Max();
        var height = data.Length / width;
        var scale = Math.Sqrt(data.Length);

        var positions = new List<V3f>();
        for (int h = 0; h < height; h++)
            for (int w = 0; w < width; w++)
                positions.Add(new V3f((float)((w - width / 2.0) / scale), (float)((h - height / 2.0) / scale), (float)(data[h * width + w] / max)));

        var minColor = new V4f(0.1f, 0.1f, 1f, 1);
        var maxColor = new V4f(1f, 0.1f, 0.1f, 1);

        var vList = new Vertex[data.Length];
        for (int h = 0; h < height; h++)
            for (int w = 0; w < width; w++)
            {
                V3f norm;
                int index = h * width + w;
                if (h == 0 || w == 0 || h == height - 1 || w == width - 1)
                    norm = new V3f(0, 0, 0);
                else
                {
                    var pts = new List<V3d>();
                    foreach (var i in new[] { index, index - 1, index + 1, index - width, index + width })
                        pts.Add(new V3d(positions[i].X, positions[i].Y, positions[i].Z));
                    var param = Geometry.GetPlaneEquationFromPoints(pts);

                    norm = new V3f((float)param.A, (float)param.B, (float)param.C);
                    if (norm[2] < 0)
                        norm = -norm;
                }
                var c = positions[index].Z * maxColor + (1 - positions[index].Z) * minColor;
                vList[index] = new Vertex(positions[index], norm, new C4(c.X, c.Y, c.Z, c.W).ToArgb());
            }

        var indicesList = new List<int>();
        for (int h = 0; h < height - 1; h++)
            for (int w = 0; w < width - 1; w++)
            {
                int i = h * width + w;
                indicesList.AddRange([i, i + 1, i + width + 1]);
                indicesList.AddRange([i, i + width + 1, i + width,]);
            }
        Vertices = [.. vList];
        Indices = [.. indicesList.Select(i => (uint)i)];
        Primitives = [(PT.Triangles, indicesList.Count)];

    }
}

/// <summary>
/// 頂点ごとの色を使う三角形メッシュ。
/// MasterPattern 3D preview のように大量のセルを 1 個の GLObject へまとめたいときに使う。
/// </summary>
public class ColoredSurfaceMesh : GLObject
{
    public ColoredSurfaceMesh(V3d[] positions, int[] argbs, uint[] indices, Material mat, DrawingMode mode = DrawingMode.Surfaces) : base(mat, mode)
    {
        if (positions == null)
            throw new ArgumentNullException(nameof(positions));
        if (argbs == null)
            throw new ArgumentNullException(nameof(argbs));
        if (indices == null)
            throw new ArgumentNullException(nameof(indices));
        if (positions.Length != argbs.Length)
            throw new ArgumentException("positions and argbs must have the same length."); // (260321Ch)

        Vertices = GC.AllocateUninitializedArray<Vertex>(positions.Length);
        for (int i = 0; i < positions.Length; i++)
        {
            var pos = positions[i];
            var norm = pos.LengthSquared > 1e-24 ? pos.Normalized() : V3d.UnitZ;
            Vertices[i] = new Vertex(pos.ToV3f(), norm.ToV3f(), argbs[i]);
        }

        Indices = [.. indices];
        Primitives = [(PT.Triangles, indices.Length)];
        UseFixedArgb = false; // (260321Ch) Material 固定色ではなく、各頂点の ARGB を使う
        CircumscribedSphereCenter = new V4d(0, 0, 0, 1);
        CircumscribedSphereRadius = positions.Length == 0 ? 0 : positions.Max(p => p.Length);
    }
}
#endregion

#region 文字オブジェクト
/// <summary>文字オブジェクト</summary>
public class TextObject : GLObject
{
    // private static readonly Dictionary<(int program, string Text, float FontSize, int Argb, bool WhiteEdge), (int TextureNum, Vertex[] Vertices)> dic = [];
    private static readonly Dictionary<(int ContextKey, int Program, string Text, float FontSize, int Argb, bool WhiteEdge), (int TextureNum, Vertex[] Vertices)> dic = []; // (260319Ch) GL context を跨ぐ text texture 再利用を防ぐ

    // public static readonly Dictionary<(int Program, int TextureNum), (int VBO, int VAO, int EBO)> DefaultDictionaly = [];
    public static readonly Dictionary<(int ContextKey, int Program, int TextureNum), (int VBO, int VAO, int EBO)> DefaultDictionaly = []; // (260319Ch) text VAO/VBO cache も context ごとに分離

    private static readonly V2f p00 = new(0, 0), p01 = new(0, 1), p10 = new(1, 0), p11 = new(1, 1);
    private static readonly uint[] indices = [0, 1, 2, 3];
    private static readonly (PT, int)[] primitives = [(PT.TriangleFan, 4)];
    private static readonly (float X, float Y)[] whiteEdgeOffsets = [(0f, 0f), (0f, 1f), (0f, 2f), (1f, 2f), (2f, 2f), (2f, 1f), (2f, 0f), (1f, 0f)]; // (260319Ch) 輪郭オフセットは毎回新規配列を作らず共有する

    public int TextureNum = -1;
    public double Popout = 0;
    internal int ContextCacheKey = 0; // (260319Ch) text texture/VAO cache の context 識別子

    internal static void ClearContextCache(int contextKey)
    {
        if (contextKey == 0)
            return;

        // var textureKeys = dic.Keys.Where(key => key.program == program).ToArray();
        var textureKeys = dic.Keys.Where(key => key.ContextKey == contextKey).ToArray(); // (260319Ch) control 切替時に stale text texture を持ち越さない
        if (textureKeys.Length > 0)
        {
            // var textureIds = textureKeys.Select(key => dic[key].TextureNum).Distinct().ToArray();
            // foreach (var textureId in textureIds)
            //     GL.DeleteTexture(textureId);
            // (260319Ch) context sharing 環境では他 control が同じ texture object を参照中の可能性があるため、ここでは dictionary だけ無効化する
            foreach (var key in textureKeys)
                dic.Remove(key);
        }

        var vaoKeys = DefaultDictionaly.Keys.Where(key => key.ContextKey == contextKey).ToArray(); // (260319Ch) text VAO cache も同時にクリアする
        foreach (var key in vaoKeys)
            DefaultDictionaly.Remove(key);
    }

    public TextObject(string text, float fontSize, Vector3DBase position, double popout, bool whiteEdge, Material mat, GLControlAlpha glControl, int program =-1)
        : this(text, fontSize, position.ToOpenTK(), popout, whiteEdge, mat, glControl, program) { }

    public TextObject(string text, float fontSize, in V3d position, double popout, bool whiteEdge, Material mat, int program)
        : this(text, fontSize, position, popout, whiteEdge, mat, null, program) { }

    public TextObject(string text, float fontSize, in V3d position, double popout, bool whiteEdge, Material mat, GLControlAlpha glControl, int program=-1) : base(mat, DrawingMode.Text)
    {
        if (GLControlAlpha.DisableTextRendering || text==null) return;
        text = text.Trim();

        // if (text != "" || fontSize > 0)
        if (text != "" && fontSize > 0) // (260319Ch) 空文字列ではテクスチャ生成に進まない
        {
            if (glControl != null)
            {
                ContextCacheKey = glControl.CacheKey; // (260319Ch) 同じ program 番号でも別 context なら cache を分離する
                if (program == -1)
                {
                    glControl.MakeCurrent();
                    // program = glControl.FragShader == GLControlAlpha.FragShaders.PPLL ?
                    //     glControl.Program :
                    //     glControl.TextProgram > 0 ? glControl.TextProgram : glControl.Program; // (260319Ch) PPLL text を linked-list に戻した案
                    program = glControl.TextProgram > 0 ? glControl.TextProgram : glControl.Program; // (260319Ch) PPLL text は専用 overlay program に戻す
                }
            }
            Indices = indices;
            Primitives = primitives;
            CircumscribedSphereCenter = new V4d(position, 1);
            ObjectMatrix = new M4f(new V4f(1, 0, 0, 0), new V4f(0, 1, 0, 0), new V4f(0, 0, 1, 0), new V4f(position.ToV3f(), 1));
            ShowClippedSection = false;//クリップ断面は表示しない
            Popout = popout;

            if (dic.TryGetValue((ContextCacheKey, program, text, fontSize, mat.Argb, whiteEdge), out var obj))//辞書に登録されている場合
            {
                TextureNum = obj.TextureNum;
                Vertices = obj.Vertices;
            }
            else //辞書に登録されていない場合
            {
                using var fnt = new Font("Tahoma", fontSize);// (260319Ch) GDI オブジェクトの解放漏れを防ぐ
                var strSize = TextRenderer.MeasureText(text, fnt, new Size(600, 100),
                    TextFormatFlags.Left); //文字列を描画するときの大体の大きさを計測する

                var width = (ushort)strSize.Width;
                var height = (ushort)strSize.Height;

                using var bmp = new Bitmap(width, height);
                using var g = Graphics.FromImage(bmp);//ImageオブジェクトのGraphicsオブジェクトを作成する
                // g.Clear(Color.Transparent); // (260319Ch) 旧コードでは明示クリアしていなかった
                g.Clear(Color.Transparent); // (260319Ch) PPLL overlay で背景 alpha が残らないよう bitmap を透明初期化する
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                if (whiteEdge)
                {
                    using var whiteBrush = new SolidBrush(Color.FromArgb(128, Color.White));
                    foreach (var (x, y) in whiteEdgeOffsets)
                        g.DrawString(text, fnt, whiteBrush, new RectangleF(x, y, width, height));
                }

                using var textBrush = new SolidBrush(Color.FromArgb(mat.Argb));
                g.DrawString(text, fnt, textBrush, new RectangleF(1f, 1f, width, height));

                var argbList = new List<byte>(BitmapConverter.ToByteRGBA(bmp));// (260319Ch) データの並び順に注意

                #region  余白の部分をトリムする
                bool isTransparentRow(int row)
                {
                    int rowOffset = row * width * 4;
                    for (int x = 0; x < width; x++)
                        if (argbList[rowOffset + x * 4 + 3] != 0)
                            return false;
                    return true;
                }

                bool isTransparentLeftColumn()
                {
                    int stride = width * 4;
                    for (int y = 0; y < height; y++)
                        if (argbList[y * stride + 3] != 0)
                            return false;
                    return true;
                }

                bool isTransparentRightColumn()
                {
                    int stride = width * 4;
                    int alphaOffset = stride - 1;
                    for (int y = 0; y < height; y++)
                        if (argbList[y * stride + alphaOffset] != 0)
                            return false;
                    return true;
                }

                while (height > 0 && isTransparentRow(0))
                {
                    argbList.RemoveRange(0, width * 4);
                    height--;
                }
                while (height > 0 && isTransparentRow(height - 1))
                {
                    argbList.RemoveRange((height - 1) * width * 4, width * 4);
                    height--;
                }

                while (width > 0 && isTransparentLeftColumn())
                {
                    for (int h = height - 1; h >= 0; h--)
                        argbList.RemoveRange(h * width * 4, 4);
                    width--;
                }
                while (width > 0 && isTransparentRightColumn())
                {
                    for (int h = height; h > 0; h--)
                        argbList.RemoveRange(h * width * 4 - 4, 4);
                    width--;
                }

                if (width == 0 || height == 0)
                    return; // (260319Ch) 全透明テクスチャは GPU リソースを作らない
                #endregion

                //空いてるテクスチャID番号を調べ、TextureNumberに格納 
                TextureNum = GL.GenTexture();
                // テクスチャをバインドする
                GL.BindBuffer(BufferTarget.PixelUnpackBuffer, 0); // (260319Ch) PPLL 用 clearBuf が bind 済みでも text bitmap は CPU メモリから直接 upload する
                GL.BindTexture(TextureTarget.Texture2D, TextureNum);
                //テクスチャ用バッファに色情報を流し込む
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, argbList.ToArray());
                // テクスチャのアンバインド
                GL.BindTexture(TextureTarget.Texture2D, 0);

                Vertices =
                [
                        new Vertex(new V3f(-width / 2f, +height / 2f, (float)popout), new V3f() ,p00),
                        new Vertex(new V3f(+width / 2f, +height / 2f, (float)popout), new V3f() ,p10),
                        new Vertex(new V3f(+width / 2f, -height / 2f, (float)popout), new V3f() ,p11),
                        new Vertex(new V3f(-width / 2f, -height / 2f, (float)popout), new V3f() ,p01)
                 ];

                //辞書に登録
                dic.Add((ContextCacheKey, program, text, fontSize, mat.Argb, whiteEdge), (TextureNum, Vertices));
            }
        }

    }

}
#endregion

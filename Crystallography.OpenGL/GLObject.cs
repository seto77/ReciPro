using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using System.Windows.Forms;
using System.Drawing;

#region 定義

using V4f = OpenTK.Vector4;
using V4d = OpenTK.Vector4d;
using V3f = OpenTK.Vector3;
using V3d = OpenTK.Vector3d;
using V2f = OpenTK.Vector2;
using V2d = OpenTK.Vector2d;
using C4 = OpenTK.Graphics.Color4;
using M4d = OpenTK.Matrix4d;
using M4f = OpenTK.Matrix4;
using M3d = OpenTK.Matrix3d;
using PT = OpenTK.Graphics.OpenGL4.PrimitiveType;

#endregion 定義



namespace Crystallography.OpenGL
{
    #region Vertex 頂点クラス
    /// <summary>
    /// 頂点要素（シェーダの頂点要素と合わせる）
    /// </summary>
    public struct Vertex
    {
        /// <summary>
        /// 0: テクスチャ無しポリゴン、1: テクスチャ有りポリゴン、2: 文字列. 
        /// 文字列の場合はNormalが中心座標, Positionは中心からのシフト量(X,Yはピクセル単位、Zはワールド単位で、回転の影響を受けない)を表す. 
        /// </summary>
        public readonly int Mode;

        public readonly int Argb;

        public readonly V3f Position;

        public readonly V3f Normal;

        public readonly V2f Uv;

        public Vertex(V3f position, V3f normal, int argb)
        {
            Position = position;
            Normal = normal;
            Argb = argb;
            Uv = new V2f(0, 0);
            Mode = 0;
        }
        public Vertex(V4f position, V3f normal, int argb) : this(new V3f(position), normal, argb) { }

        public Vertex(V3f position, V3f normal, V2f uv, int argb, int mode)
        {
            Position = position;
            Normal = normal;
            Argb = argb;
            Uv = uv;
            Mode = mode;
        }

        public static readonly int Stride = Marshal.SizeOf(default(Vertex));
    }
    #endregion

    #region Enum
    public enum DrawingMode { Surfaces = 1, Edges = 2, SurfacesAndEdges = 4, Points = 8, Text = 16 }
    #endregion

    #region クラス
    public class Location
    {
        internal int TextureLocation { get; set; } = 1;
        internal int EmissionLocation { get; set; } = -1;
        internal int AmbientLocation { get; set; } = -1;
        internal int DiffuseLocation { get; set; } = -1;
        internal int SpecularLocation { get; set; } = -1;
        internal int SpecularPowerLocation { get; set; } = -1;
        internal int UseFixedArgbLocation { get; set; } = -1;
        internal int FixedArgbLocation { get; set; } = -1;
        internal int IgnoreNormalSidesLocation { get; set; } = -1;
        internal int RenderPassLocation { get; set; } = -1;

        internal int PassOIT1Index = -1;
        internal int PassOIT2Index = -1;
        internal int PassNormalIndex = -1;
        internal int PositionLocation { get; set; } = -1;
        internal int NormalLocation { get; set; } = -1;
        internal int ArgbLocation { get; set; } = -1;

        internal int UvLocation { get; set; } = -1;
        internal int ModeLocation { get; set; } = -1;
        internal int ObjectMatrixLocation { get; set; } = -1;
    }
    #endregion

    #region GLObjectクラス (抽象クラス)
    /// <summary>
    /// OpenGLで描画するオブジェクトを表現する抽象クラス
    /// </summary>
    abstract public class GLObject
    {
        #region static private な フィールド & プロパティ

        private static readonly Dictionary<int, Location> Location = new Dictionary<int, Location>();

        #endregion

        #region internalな フィールド & プロパティ

        internal int VBO, VAO, EBO;

        internal int Program = -1;
        /// <summary>
        /// 頂点
        /// </summary>
        internal Vertex[] Vertices;
        /// <summary>
        /// 頂点の順番リスト (全てのタイプが連結されている)
        /// </summary>
        internal uint[] Indices;
        /// <summary>
        /// タイプ
        /// </summary>
        internal PT[] Types;
        /// <summary>
        /// 各タイプの順番リストの長さ
        /// </summary>
        internal int[] TypeCounts;
        #endregion

        #region publicな フィールド & プロパティ
        /// <summary>
        /// 自由に情報を格納するためのTag
        /// </summary>
        public object Tag = null;

        /// <summary>
        /// オブジェクトを一意に識別する番号
        /// </summary>
        public int SerialNumber;

        /// <summary>
        /// 物体に外接する外接球の中心座標 (wは常に1にする)
        /// </summary>
        public V4d CircumscribedSphereCenter = new V4d(0, 0, 0, 1);

        /// <summary>
        /// 物体に外接する外接球の半径
        /// </summary>
        public double CircumscribedSphereRadius = 0;

        /// <summary>
        /// 法線方向の正負を無視する
        /// </summary>
        public bool IgnoreNormalSides;

        /// <summary>
        /// クリップされた場合、断面ポリゴンを描画するかどうか. 初期値はtrue. Polygon(平面)はfalse
        /// </summary>
        public bool ShowClippedSection = true;

        /// <summary>
        /// 描画モード
        /// </summary>
        public DrawingMode Mode;

        /// <summary>
        /// 素材
        /// </summary>
        public Material Material;

        /// <summary>
        /// 描画するかどうか
        /// </summary>
        public bool Rendered = true;

        /// <summary>
        /// trueの時は(Vertexの色ではなく)Material構造体中のColorが使われる
        /// </summary>
        public bool UseFixedArgb = true;

        /// <summary>
        /// Z sorting で透明度を計算する時のZを一時期的に格納する変数
        /// </summary>
        public double Z;

        /// <summary>
        /// 物体の回転状態や並進状態を表す. 
        /// </summary>
        public M4f ObjectMatrix = M4f.Identity;


        #endregion publicなフィールド

        #region コンストラクタ、デストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="material"></param>
        /// <param name="mode"></param>
        public GLObject(Material material, DrawingMode mode)
        {
            Material = material;
            Mode = mode;
            lock (GLGeometry.LockObj)
                SerialNumber = GLGeometry.SerialNumber++;
        }

        public void Dispose()
        {
            if (Sphere.DefaultDictionary.ContainsKey(Program))
                Sphere.DefaultDictionary.Remove(Program);
            if (Cylinder.DefaultDictionary.ContainsKey(Program))
                Cylinder.DefaultDictionary.Remove(Program);

            GL.DeleteBuffers(1, ref VBO);
            GL.DeleteBuffers(1, ref EBO);
            GL.DeleteVertexArrays(1, ref VAO);
        }

        #endregion

        /// <summary>
        /// program番号をセットし、各バッファオブジェクトなどGPUに転送する. 描画前に必ず一度実行する必要がある。
        /// </summary>
        /// <param name="program"></param>
        public void Generate(int program)
        {
            if (program < 0 || Vertices == null || Vertices.Length == 0) 
                return;

            Program = program;

            //Locationを取得
            if (!Location.ContainsKey(Program))
            {
                Location.Add(Program, new Location());
                SetLocation(Program);
            }
            //Default形状であれば、VAO, VBO, EBOをセットしておしまい。
            var dic = this is Sphere s && s.UseDefault ? Sphere.DefaultDictionary : this is Cylinder c && c.UseDefault ? Cylinder.DefaultDictionary : null;
            if (dic != null && dic.TryGetValue(Program, out var objects))
            {
                VBO = objects.VBO;
                EBO = objects.EBO;
                VAO = objects.VAO;
                return;
            }

            //VBO, EBO, VAO の数値(名前?)を取得
            GL.GenBuffers(1, out VBO); 
            GL.GenBuffers(1, out EBO); 
            GL.GenVertexArrays(1, out VAO);

            dic?.Add(Program, (VBO, VAO, EBO));

            // VBO作成
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vertices.Length * Vertex.Stride), Vertices, BufferUsageHint.DynamicDraw);

            // EBO作成
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * Indices.Length), Indices, BufferUsageHint.DynamicDraw);

            // VAO作成
            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            //モード
            GL.EnableVertexAttribArray(Location[Program].ModeLocation);
            GL.VertexAttribPointer(Location[Program].ModeLocation, 1, VertexAttribPointerType.Int, false, Vertex.Stride, 0);
            //色
            GL.EnableVertexAttribArray(Location[Program].ArgbLocation);
            GL.VertexAttribPointer(Location[Program].ArgbLocation, 1, VertexAttribPointerType.Int, false, Vertex.Stride, sizeof(int));
            //頂点位置
            GL.EnableVertexAttribArray(Location[Program].PositionLocation);
            GL.VertexAttribPointer(Location[Program].PositionLocation, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 2 * sizeof(int));
            //法線
            GL.EnableVertexAttribArray(Location[Program].NormalLocation);
            GL.VertexAttribPointer(Location[Program].NormalLocation, 3, VertexAttribPointerType.Float, true, Vertex.Stride, 2 * sizeof(int) + V3f.SizeInBytes);
            //テクスチャ座標
            GL.EnableVertexAttribArray(Location[Program].UvLocation);
            GL.VertexAttribPointer(Location[Program].UvLocation, 2, VertexAttribPointerType.Float, false, Vertex.Stride, 2 * sizeof(int) + 2 * V3f.SizeInBytes);

            //TextObjectの場合は、ここでテクスチャーを転送
            if (this is TextObject t && t.TextureNum != -1 && t.Texture != null)
            {
                // テクスチャをバインドする
                GL.BindTexture(TextureTarget.Texture2D, t.TextureNum);
                //テクスチャ用バッファに色情報を流し込む
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, t.Size[0], t.Size[1], 0, PixelFormat.Rgba, PixelType.UnsignedByte, t.Texture);
                // テクスチャのアンバインド
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
        }

        /// <summary>
        /// パラメータのロケーションをセット
        /// </summary>
        /// <param name="Program"></param>
        public void SetLocation(int Program)
        {

            Location[Program].ModeLocation = GL.GetAttribLocation(Program, "vObjType");
            Location[Program].ArgbLocation = GL.GetAttribLocation(Program, "vArgb");
            Location[Program].PositionLocation = GL.GetAttribLocation(Program, "vPosition");
            Location[Program].NormalLocation = GL.GetAttribLocation(Program, "vNormal");
            Location[Program].UvLocation = GL.GetAttribLocation(Program, "vUv");
            if (Location[Program].ModeLocation == -1 || Location[Program].UvLocation == -1 || Location[Program].PositionLocation == -1
                || Location[Program].NormalLocation == -1 || Location[Program].ArgbLocation == -1)
                throw new Exception("cannot find location!");

            Location[Program].ObjectMatrixLocation = GL.GetUniformLocation(Program, "ObjectMatrix");

            Location[Program].TextureLocation = GL.GetUniformLocation(Program, "Texture");

            Location[Program].EmissionLocation = GL.GetUniformLocation(Program, "Emission");
            Location[Program].AmbientLocation = GL.GetUniformLocation(Program, "Ambient");
            Location[Program].DiffuseLocation = GL.GetUniformLocation(Program, "Diffuse");
            Location[Program].SpecularLocation = GL.GetUniformLocation(Program, "Specular");
            Location[Program].SpecularPowerLocation = GL.GetUniformLocation(Program, "SpecularPower");
            Location[Program].UseFixedArgbLocation = GL.GetUniformLocation(Program, "UseFixedArgb");
            Location[Program].IgnoreNormalSidesLocation = GL.GetUniformLocation(Program, "IgnoreNormalSides");
            Location[Program].FixedArgbLocation = GL.GetUniformLocation(Program, "FixedArgb");

            Location[Program].PassOIT1Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passOIT1");
            Location[Program].PassOIT2Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passOIT2");
            Location[Program].PassNormalIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passNormal");
            Location[Program].RenderPassLocation = GL.GetSubroutineUniformLocation(Program, ShaderType.FragmentShader, "RenderPass");
        }

        /// レンダリングを実行. Progaramが正しくセットされていない(Generate()をしていない)場合は例外が発生
        /// </summary>
        private void Render()
        {
            if (this is TextObject text && text.TextureNum != -1)
            {
                GL.Uniform1(Location[Program].TextureLocation, 0);
                GL.BindTexture(TextureTarget.Texture2D, text.TextureNum);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
            }

            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            for (int i = 0, offset = 0; i < Types.Length; i++)
            {
                var t = Types[i];
                var len = TypeCounts[i];
                if ((t == PT.Triangles || t == PT.TriangleStrip || t == PT.TriangleFan || t == PT.Quads)
                    && (Mode == DrawingMode.Surfaces || Mode == DrawingMode.SurfacesAndEdges || Mode == DrawingMode.Text))
                    SetMaterialAndDrawElements(true, t, len, offset);
                else
                {
                    if ((t == PT.Lines || t == PT.LinesAdjacency || t == PT.LineLoop) && (Mode == DrawingMode.Edges || Mode == DrawingMode.SurfacesAndEdges))
                        SetMaterialAndDrawElements(false, Types[i], len, offset);
                    else if (t == PT.Points && Mode == DrawingMode.Points)
                        SetMaterialAndDrawElements(false, t, len, offset);
                }
                offset += len * sizeof(uint);
            }
        }

        /// <summary>
        /// 物体の材質と要素をGPUに送信する。Render()関数から呼び出される。
        /// </summary>
        /// <param name="drawSurfaces">Surfaceモードか否か</param>
        private void SetMaterialAndDrawElements(bool drawSurfaces, PT mode, int count, int offset)
        {
            GL.UniformMatrix4(Location[Program].ObjectMatrixLocation, false, ref ObjectMatrix);

            var renew = prms.Program != Program;

            (float emi, float amb, float dif, float spe) = drawSurfaces ?
                (Material.Emission, Material.Ambient, Material.Diffuse, Material.Specular) : (0f, 1f, 0f, 0f);

            if (renew || emi != prms.emi)
                GL.Uniform1(Location[Program].EmissionLocation, emi);

            if (renew || amb != prms.amb)
                GL.Uniform1(Location[Program].AmbientLocation, amb);

            if (renew || dif != prms.dif)
                GL.Uniform1(Location[Program].DiffuseLocation, dif);

            if (renew || spe != prms.spe)
                GL.Uniform1(Location[Program].SpecularLocation, spe);

            if (renew || prms.spePow != Material.SpecularPower)
                GL.Uniform1(Location[Program].SpecularPowerLocation, Material.SpecularPower);

            if (renew || prms.UseFixedArgb != UseFixedArgb)
                GL.Uniform1(Location[Program].UseFixedArgbLocation, UseFixedArgb ? 1 : 0);//Trueが1なのはなぜか分からないが、これで動く。

            if (renew || prms.argb != Material.Argb)
                GL.Uniform1(Location[Program].FixedArgbLocation, Material.Argb);

            if (renew || IgnoreNormalSides != prms.ignoreNormal)
                GL.Uniform1(Location[Program].IgnoreNormalSidesLocation, IgnoreNormalSides ? 1 : 0);

            //if (IgnoreNormalSides)
            //{
            //    GLable(false, EnableCap.CullFace);//CullFace無効化
            //    GL.DrawElements(mode, count, DrawElementsType.UnsignedInt, offset);
            //    GL.GetUniformSubroutine(ShaderType.FragmentShader, RenderPassLocation, out int renderPassIndex);  //レンダーパスを取得
            //    GLable(renderPassIndex == PassNormalIndex, EnableCap.CullFace);//CullFaceを元に戻す
            //}
            //else
            GL.DrawElements(mode, count, DrawElementsType.UnsignedInt, offset);//CullFaceは常に無効

            prms = (Program, emi, amb, dif, spe, Material.SpecularPower, Material.Argb, IgnoreNormalSides, UseFixedArgb);
        }
        static private (int Program, float emi, float amb, float dif, float spe, float spePow, int argb, bool ignoreNormal, bool UseFixedArgb)
             prms = (-1, 0, 0, 0, 0, 0, 0, false, false);


        /// <summary>
        /// レンダリングを実行. Progaramが正しくセットされていない(Generate()をしていない)場合は例外が発生. OITモードはCullFaceとDepthTestが無効、通常モードはCullFaceとDepthTestが有効
        /// </summary>
        /// <param name="clip">Clip平面</param>
        /// <param name="oit">OIT(order independent transparency)モードかどうか</param>
        public void Render(Clip clip = null)
        {
            if (!Rendered) return;

            //クリップ無効か、有効だが全てのクリップ面の内側にある場合
            if (clip == null || clip.PrmsD.Count == 0 || clip.PrmsD.All(p => V4d.Dot(p, CircumscribedSphereCenter) - CircumscribedSphereRadius > 0))
            {
                Render();
            }
            else if (!clip.PrmsD.Any(p => V4d.Dot(p, CircumscribedSphereCenter) + CircumscribedSphereRadius < 0))//クリップ有効でクリップ面を切るようなオブジェクトの場合
            {
                //クリップ対象となるクリップ平面を検索
                var indices = clip.PrmsD.Select((p, i) => Math.Abs(V4d.Dot(p, CircumscribedSphereCenter)) < CircumscribedSphereRadius ? i : -1).Where(i => i != -1);
                if (!ShowClippedSection)//クリップセクションを表示しない場合
                {
                    clip.EnableClips(indices);//全クリップ有効化
                    Render(); //物体全体の描画
                    Clip.DisableAllClips();//全クリップ無効化
                }
                else///クリップセクションを表示する場合
                {
                    GL.Enable(EnableCap.StencilTest);//Stencilテスト有効
                    foreach (var i in indices)
                    {
                        DepthTest(false);//Depthテスト無効

                        if (Location[Program].PassNormalIndex != -1)//OITモードの場合は
                            GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref Location[Program].PassNormalIndex);//サブルーチンをNormalにする

                        GL.Clear(ClearBufferMask.StencilBufferBit);//ステンシルバッファークリア
                        GL.ColorMask(false, false, false, false); //色は全くかきこまない
                        GL.Enable(EnableCap.CullFace);//CullFace有効
                        GL.StencilFunc(StencilFunction.Always, 0, 0);//Stencil Funcを設定 (Always)
                        clip.EnableClips(new[] { i });//i番目のクリップのみ有効化
                        //裏面のみ描画(ステンシル値だけ書き込む)
                        GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.IncrWrap);//ステンシル値「+1」
                        GL.CullFace(CullFaceMode.Front); //表面をカリング
                        Render();
                        //表面のみ描画(→差分をとってマスク画像にする)
                        GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.DecrWrap);//ステンシル値「-1」
                        GL.CullFace(CullFaceMode.Back);//裏面をカリング
                        Render();
                        //ここまででステンシル完成(0以外が有効)

                        //ここからクリップ平面を描画
                        if (Location[Program].PassOIT1Index != -1)//OITモードの場合は
                            GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref Location[Program].PassOIT1Index);//サブルーチンをOIT1に戻す

                        GL.ColorMask(true, true, true, true);
                        GL.StencilFunc(StencilFunction.Notequal, 0, ~0); //ステンシル値が0でない部分が描画(切断面の描画). ~0 は補数(11111111)
                        GL.Disable(EnableCap.CullFace);//CullFace無効化
                        clip.EnableClips(indices.Where(j => j != i));//i番目のクリップ以外を有効化
                        DepthTest(Location[Program].PassNormalIndex == -1);//Depthテストを元に戻す (Z-sortモードは有効)
                        clip.Render(i, Material);//i番目のクリップ面を描画
                    }
                    GL.Disable(EnableCap.StencilTest);//Stencilテスト無効化
                    clip.EnableClips(indices);//全クリップ有効化
                    Render(); //物体全体の描画
                    Clip.DisableAllClips();//全クリップ無効化
                }
            }
        }

        internal static readonly V3d vZ = new V3d(0, 0, 1);

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
    /// <summary>
    /// 描画範囲をクリップ(切り取る)する.
    /// </summary>
    public class Clip
    {
        private readonly List<Quads> Planes = new List<Quads>();
        private readonly List<float> PrmsF = new List<float>();
        public List<V4d> PrmsD = new List<V4d>();
        private int Program = 0;
        private int ClipPlanesLocation = 0, ClipNumLocation = 0;

        public Clip(params V4d[] planeParams)
        {
            for (int i = 0; i < planeParams.Length; i++)
            {
                var prms = planeParams[i] / new V3d(planeParams[i]).Length;
                var norm = new V3d(prms);
                var rot = GLGeometry.CreateRotationFromZ(norm);

                var pts = new[] { new V3d(100, 0, 0), new V3d(0, 100, 0), new V3d(-100, 0, 0), new V3d(0, -100, 0) };
                for (int j = 0; j < pts.Length; j++)
                    pts[j] = rot.Mult(pts[j]) - norm * prms.W * 1.0005;

                var obj = new Quads(pts[0], pts[1], pts[2], pts[3], new Material(0), DrawingMode.Surfaces)
                {
                    UseFixedArgb = true,
                    IgnoreNormalSides = false,
                    Types = new[] { PT.TriangleFan }
                };
                Planes.Add(obj);
                PrmsF.AddRange(prms.ToArrayF());
                PrmsD.Add(prms);
            }
        }

        public void Generate(int program)
        {
            Program = program;
            Planes.ForEach(p => p.Generate(program));
            ClipPlanesLocation = GL.GetUniformLocation(Program, "ClipPlanes");
            ClipNumLocation = GL.GetUniformLocation(Program, "ClipNum");
        }

        public void Render(int index, Material mat)
        {
            Planes[index].Material = mat;
            Planes[index].Render();
        }

        public void EnableClips(IEnumerable<int> indices)
        {
            int count = indices.Count();
            GL.Uniform1(ClipNumLocation, count);

            for (int i = 0; i < count; i++)
                GL.Enable(EnableCap.ClipDistance0 + i);
            for (int i = count; i < 8; i++)
                GL.Disable(EnableCap.ClipDistance0 + i);

            if (count > 0)
                GL.Uniform4(ClipPlanesLocation, indices.Count(), PrmsF.Where((v, i) => indices.Contains(i / 4)).ToArray());
        }
        public static void DisableAllClips()
        {
            for (int i = 0; i < 8; i++)
                GL.Disable(EnableCap.ClipDistance0 + i);
        }
    }
    #endregion

    #region 三角形、四角形、円板、多角形
    /// <summary>
    /// 多角形 (凸多角形) 点集合が完全に平面に乗らない場合でも、最小二乗法で法線を求める
    /// </summary>
    public class Polygon : GLObject
    {
        public Polygon(Material mat, DrawingMode mode) : base(mat, mode)        {        }

        public Polygon(Vector3DBase[] vertices, Material mat, DrawingMode mode)
            : this(vertices.Select(v => new V3d(v.X, v.Y, v.Z)).ToArray(), mat, mode)        {        }

        public Polygon(Material mat, DrawingMode mode, params V3d[] vertices) : this(vertices.ToArray(), mat, mode)        {        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="mat"></param>
        /// <param name="mode"></param>
        public Polygon(IEnumerable< V3d> vertices, Material mat, DrawingMode mode) : base(mat, mode)
        {
            ShowClippedSection = false;//クリップ断面は表示しない
            IgnoreNormalSides = true;//裏表を無視する
            var center = new V3d(vertices.Average(v => v.X), vertices.Average(v => v.Y), vertices.Average(v => v.Z));
            CircumscribedSphereCenter = new V4d(center, 1);
            CircumscribedSphereRadius = vertices.Max(v => (v - center).Length);

            var polygonInfo = GLGeometry.PolygonInfo(vertices, V3d.Zero);
            var normF = polygonInfo.Norm.ToV3f();
            var centerF = polygonInfo.Center.ToV3f();
            var tempList = polygonInfo.Indices;

            var veticesList = new List<Vertex>(vertices.Select(p => new Vertex(p.ToV3f(), normF, mat.Argb)));
            veticesList.Add(new Vertex(centerF, normF, mat.Argb));

            var indicesList = new List<int> { veticesList.Count - 1 };
            indicesList.AddRange(tempList);

            //最終処理
            Vertices = veticesList.ToArray();

            var lists = new List<int[]>();
            var types = new List<PT>();
            //surfaces
            lists.Add(indicesList.ToArray());
            types.Add(PT.TriangleFan);
            //edges
            indicesList.RemoveRange(0, 2);
            lists.Add(indicesList.ToArray());
            types.Add(PT.LineLoop);
            //points
            lists.Add(indicesList.ToArray());
            types.Add(PT.Points);

            Indices = lists.SelectMany(i => i).Select(i => (uint)i).ToArray();
            TypeCounts = lists.Select(i => i.Length).ToArray();
            Types = types.ToArray();
        }

        /// <summary>
        /// 多角形を分解する (Quadsにして返す)
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Polygon[] Decompose(int order = 1)
        {
            //ここから本体
            var inputs = new List<V3d>();
            for (int i = 1; i < TypeCounts[0] - 1; i++)
                inputs.Add(Vertices[Indices[i]].Position.ToV3d());

            var outputs = decompose(inputs.ToArray(), order);

            var results = new Polygon[outputs.Length];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = new Polygon(Material, Mode);
                results[i].ShowClippedSection = false;//クリップ断面は表示しない
                results[i].IgnoreNormalSides = true;//裏表を無視する

                results[i].Vertices = outputs[i].Select(v => new Vertex(v.ToV3f(), Vertices[0].Normal, Vertices[0].Argb)).ToArray();
                results[i].Types = new[] { PT.Quads };

                results[i].Indices = Enumerable.Range(0, outputs[i].Length).Select(val => (uint)val).ToArray();

                results[i].TypeCounts = new[] { results[i].Indices.Length };

                var center = Extensions.Average(outputs[i]);
                results[i].CircumscribedSphereCenter = new V4d(center, 1);
                results[i].CircumscribedSphereRadius = outputs[i].Max(v => (v - center).Length);

                results[i].Tag = Tag;
                results[i].Rendered = Rendered;

                //results[i].Material.Color = new C4((float)rn.NextDouble(), (float)rn.NextDouble(), (float)rn.NextDouble(), 1);
            }

            return results.ToArray();
        }


        /// <summary>
        /// Decomposeから呼びばれる再帰的関数
        /// </summary>
        /// <param name="srcVertex"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        static V3d[][] decompose(V3d[] srcVertex, int ord)
        {
            if (ord == 0)//ゼロの場合、これ以上分解しない
            {
                return new[] { srcVertex };
            }
            else
            {
                //頂点と、頂点間の中点を、交互に追加. 偶数番目が中点になるように.
                var newVertices = new List<V3d>();
                newVertices.Add((srcVertex[srcVertex.Length - 1] + srcVertex[0]) / 2);
                newVertices.Add(srcVertex[0]);
                for (int i = 1; i < srcVertex.Length; i++)
                {
                    newVertices.Add((srcVertex[i - 1] + srcVertex[i]) / 2);
                    newVertices.Add(srcVertex[i]);
                }

                var center = Extensions.Average(srcVertex);//中心を算出

                //中心と新しい頂点を組み合わせて、可能であれば凸な4角形、無理なら3角形を作る
                var resultVertices = new List<V3d[]>();
                int n0 = 0, n1 = 1, n2 = 2;
                while (true)
                {
                    if (n2 < newVertices.Count && n0 % 2 == 0) // n0が奇数(すなわち中点)で、四角形を作る余裕があるとき
                    {
                        var quads = new[] { center, newVertices[n0], newVertices[n1], newVertices[n2] };
                        if (GLGeometry.PolygonInfo(quads, V3d.Zero).Indices.Length == 5) //四角形が凸になるか判定
                            resultVertices.Add(quads);
                        else
                            resultVertices.Add(new[] { center, newVertices[n0], newVertices[n1] });
                    }
                    else
                        resultVertices.Add(new[] { center, newVertices[n0], newVertices[n1] });

                    if (resultVertices[resultVertices.Count - 1].Length == 4)//四角形の時
                    {
                        if (n2 == 0)
                            break;
                        n0 += 2; n1 += 2; n2 += 2;
                    }
                    else//三角形の時
                    {
                        if (n1 == 0)
                            break;
                        n0++; n1++; n2++;
                    }

                    n1 = n1 < newVertices.Count ? n1 : n1 - newVertices.Count;
                    n2 = n2 < newVertices.Count ? n2 : n2 - newVertices.Count;
                }
                //新しく出来た頂点群を再帰的に分割する
                return resultVertices.SelectMany(v => decompose(v, ord - 1)).ToArray();
            }
        }
    }

    /// <summary>
    /// 三角形
    /// </summary>
    public class Triangle : Polygon
    {
        public Triangle(Vector3DBase a, Vector3DBase b, Vector3DBase c, Material mat, DrawingMode mode)
       : this(new V3d(a.X, a.Y, a.Z), new V3d(b.X, b.Y, b.Z), new V3d(c.X, c.Y, c.Z), mat, mode) { }

        /// <summary>
        /// 三角形
        /// </summary>
        /// <param name="a">頂点a</param>
        /// <param name="b">頂点b</param>
        /// <param name="c">頂点c</param>
        /// <param name="mat"></param>
        /// <param name="mode"></param>
        public Triangle(V3d a, V3d b, V3d c, Material mat, DrawingMode mode)
            : base(new V3d[] { a, b, c }, mat, mode) { }
    }

    /// <summary>
    /// 四角形
    /// </summary>
    public class Quads : Polygon
    {
        public Quads(Vector3DBase a, Vector3DBase b, Vector3DBase c, Vector3DBase d, Material mat, DrawingMode mode)
        : this(new V3d(a.X, a.Y, a.Z), new V3d(b.X, b.Y, b.Z), new V3d(c.X, c.Y, c.Z), new V3d(d.X, d.Y, d.Z), mat, mode) { }

        public Quads(V3d a, V3d b, V3d c, V3d d, Material mat, DrawingMode mode)
            : base(new V3d[] { a, b, c, d }, mat, mode) { }
    }

    /// <summary>
    /// 円 (ディスク)
    /// </summary>
    public class Disk : Polygon
    {
        public Disk(Vector3DBase origin, Vector3DBase normal, double radius, Material mat, DrawingMode mode, int slices = 60)
            : this(new V3d(origin.X, origin.Y, origin.Z), new V3d(normal.X, normal.Y, normal.Z), radius, mat, mode, slices) { }

        public Disk(V3d origin, V3d normal, double radius, Material mat, DrawingMode mode, int slices = 60)
            : base(
                 Enumerable.Range(0, slices).Select(i =>
                 {
                     var p = radius * new V2d(Math.Sin((double)i / slices * 2 * Math.PI), Math.Cos((double)i / slices * 2 * Math.PI));
                     M3d rot;
                     if (normal == vZ)
                         rot = M3d.Identity;
                     else
                         rot = M3d.CreateFromAxisAngle(V3d.Cross(normal, vZ), V3d.CalculateAngle(vZ, normal));
                     double x = rot.M11 * p.X + rot.M12 * p.Y + origin.X;
                     double y = rot.M21 * p.X + rot.M22 * p.Y + origin.Y;
                     double z = rot.M31 * p.X + rot.M32 * p.Y + origin.Z;
                     return new V3d(x, y, z);
                 }).ToArray(), mat, mode)
        { }
    }

    #endregion

    #region 多面体、立方体
    /// <summary>
    /// 多面体 (凸多面体)
    /// </summary>
    public class Polyhedron : GLObject
    {
        public Polyhedron(IEnumerable<Vector3DBase> vertices, Material mat, DrawingMode mode)
            : this(vertices.Select(v => new V3d(v.X, v.Y, v.Z)), mat, mode) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="mat"></param>
        /// <param name="mode"></param>
        public Polyhedron(IEnumerable< V3d> vertices, Material mat, DrawingMode mode) : base(mat, mode)
        {
            var center = new V3d(vertices.Average(v => v.X), vertices.Average(v => v.Y), vertices.Average(v => v.Z));
            CircumscribedSphereCenter = new V4d(center, 1);
            CircumscribedSphereRadius = vertices.Max(v => (v - center).Length);

            //任意の三点を選び、平面方程式を作り、それらが最も端面であるかを評価し、端面である場合はリストに加える
            var candidates = new List<V3d[]>();
            var vrs = vertices.ToArray();
            for (int i = 0; i < vrs.Length; i++)
                for (int j = i + 1; j < vrs.Length; j++)
                    for (int k = j + 1; k < vrs.Length; k++)
                    {
                        V3d A = vrs[i], B = vrs[j], C = vrs[k];
                        V3d V = new V3d(
                            (B.Y - A.Y) * (C.Z - A.Z) - (C.Y - A.Y) * (B.Z - A.Z),
                            (B.Z - A.Z) * (C.X - A.X) - (C.Z - A.Z) * (B.X - A.X),
                            (B.X - A.X) * (C.Y - A.Y) - (C.X - A.X) * (B.Y - A.Y)
                         );
                        var d = -V3d.Dot(V, A);

                        if (vrs.All(v => V3d.Dot(v, V) + d < 0.0000001) || vrs.All(v => V3d.Dot(v, V) + d > -0.0000001))
                            if (candidates.All(cand => !(cand.Contains(vrs[i]) && cand.Contains(vrs[j]) && cand.Contains(vrs[k]))))
                                candidates.Add(vrs.Where(v => Math.Abs(V3d.Dot(v, V) + d) < 0.0000001).ToArray());
                    }

            //各面を構成する頂点集合に対して
            var vList = new List<Vertex>();
            var iList2 = new List<IEnumerable< int>>();
            var types = new List<PT>();
            var offset = 0;
            foreach (var cand in candidates)
            {
                var polygonInfo = GLGeometry.PolygonInfo(cand, center);

                vList.AddRange(cand.Select(p => new Vertex(p.ToV3f(), polygonInfo.Norm.ToV3f(), mat.Argb)));//多面体頂点を追加
                vList.Add(new Vertex(polygonInfo.Center.ToV3f(), polygonInfo.Norm.ToV3f(), mat.Argb));//多面体中心を追加

                var iTemp = new List<int>(new[] { vList.Count - 1 });//多面体中心のインデックスを追加
                var offsetIndices = polygonInfo.Indices.Select(n => n + offset).ToList();
                iTemp.AddRange(offsetIndices);//多面体頂点のインデックスを追加

                iList2.Add(iTemp);
                types.Add(PT.TriangleFan);

                offsetIndices.RemoveAt(0);
                iList2.Add(offsetIndices);
                iList2.Add(offsetIndices);
                types.Add(PT.LineLoop);
                types.Add(PT.Points);

                offset += cand.Length + 1;
            }

            Vertices = vList.ToArray();
            Indices = iList2.SelectMany(i => i).Select(i => (uint)i).ToArray();
            TypeCounts = iList2.Select(i => i.Count()).ToArray();
            Types = types.ToArray();


        }

        /// <summary>
        /// PolyhedronをPolygonに分解する.
        /// orderを指定すると、分解したPolygonをさらに分割する.
        /// </summary>
        public Polygon[] ToPolygons(int order = 0)
        {
            var p = new Polygon[Types.Length / 3];

            for (int i = 0, offsetIndices = 0, offsetVetices = 0; i < p.Length; i++)
            {
                p[i] = new Polygon(Material, Mode);

                p[i].TypeCounts = new[] { TypeCounts[i * 3], TypeCounts[i * 3 + 1], TypeCounts[i * 3 + 2] };

                p[i].Types = new[] { Types[i * 3], Types[i * 3 + 1], Types[i * 3 + 2] };

                p[i].Indices = new uint[p[i].TypeCounts.Sum()];
                Array.Copy(Indices, offsetIndices, p[i].Indices, 0, p[i].Indices.Length);
                p[i].Indices = p[i].Indices.Select(i => (uint)(i - offsetVetices)).ToArray();
                offsetIndices += p[i].Indices.Length;

                p[i].Vertices = new Vertex[p[i].Indices.Distinct().Count()];
                Array.Copy(Vertices, offsetVetices, p[i].Vertices, 0, p[i].Vertices.Length);
                offsetVetices += p[i].Vertices.Length;

                var center = new V3f(p[i].Vertices.Average(v => v.Position.X), p[i].Vertices.Average(v => v.Position.Y), p[i].Vertices.Average(v => v.Position.Z));
                p[i].CircumscribedSphereCenter = new V4d(center.X, center.Y, center.Z, 1);
                p[i].CircumscribedSphereRadius = p[i].Vertices.Max(v => (new V3f(v.Position) - center).Length);
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

    /// <summary>
    /// 平行六面体(原点と3辺のベクトルで定義)
    /// </summary>
    public class Parallelepiped : Polyhedron
    {
        public Parallelepiped(V3d o, V3d a, V3d b, V3d c, Material mat, DrawingMode mode)
            : base(new[] { o, o + a, o + b, o + c, o + a + b, o + b + c, o + c + a, o + a + b + c }, mat, mode) { }
    }

    #endregion

    #region 球体 楕円球、真球
    /// <summary>
    /// 楕円球 (原点と3方向のベクトルで定義される).  6*(2*slices+1)^2 の頂点が生成される
    /// </summary>
    public class Ellipsoid : GLObject
    {
        public static int DefaultSlices = 2;

        public V3d Origin { get; set; }
        public V3d RadiusVector1 { get; set; }
        public V3d RadiusVector2 { get; set; }
        public V3d RadiusVector3 { get; set; }

        public Ellipsoid(Vector3DBase o, Vector3DBase a, Vector3DBase b, Vector3DBase c, Material mat, DrawingMode mode, int slices = 0)
        : this(new V3d(o.X, o.Y, o.Z), new V3d(a.X, a.Y, a.Z), new V3d(b.X, b.Y, b.Z), new V3d(c.X, c.Y, c.Z), mat, mode, slices) { }

        /// <summary>
        /// 楕円球 (中心位置と3方向のベクトルで定義される).  6*(2*slices+1)^2 の頂点が生成される
        /// </summary>
        /// <param name="o">中心位置</param>
        /// <param name="v1">中心位置からのベクトル1</param>
        /// <param name="v2">中心位置からのベクトル2</param>
        /// <param name="v3">中心位置からのベクトル3</param>
        /// <param name="mat">素材</param>
        /// <param name="mode">描画モード</param>
        /// <param name="slices">分割数. 6*(2*slices+1)^2 の頂点が生成される. </param>
        public Ellipsoid(V3d o, V3d v1, V3d v2, V3d v3, Material mat, DrawingMode mode, int slices = 0) : base(mat, mode)
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
                        TypeCounts = Sphere.DefaultTypeCounts;
                        Types = Sphere.DefaultTypes;
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
            var rot = new[] {
                new M3d(1, 0, 0, 0, 1, 0, 0, 0, 1),
                new M3d(1, 0, 0, 0, 0, -1, 0, 1, 0),
                new M3d(1, 0, 0, 0, -1, 0, 0, 0, -1),
                new M3d(1, 0, 0, 0, 0, 1, 0, -1, 0),
                new M3d(0, 0, 1, 0, 1, 0, -1, 0, 0),
                new M3d(0, 0, -1, 0, 1, 0, 1, 0, 0), };

            var vList = new List<Vertex>();
            for (int i = 0; i < rot.Length; i++)
                for (int h = -slices; h <= slices; h++)
                    for (int w = -slices; w <= slices; w++)
                    {
                        var n = new V4d(V3d.Normalize(rot[i].Mult(new V3d(w, h, slices))), 1);
                        var v = transMat.Mult(n);
                        vList.Add(new Vertex(v.ToV3f(), n.ToV3f(), mat.Argb));
                    }
            Vertices = vList.ToArray();

            var types = new List<PT>();
            var indices = new List<int[]>();

            types.Add(PT.Points);
            indices.Add(Enumerable.Range(0, Vertices.Length).ToArray());

            var indexListSurfaces = new List<int>();
            var indexListEdges = new List<int>();
            for (int i = 0; i < rot.Length; i++)
                for (int h = 0; h < 2 * slices; h++)
                    for (int w = 0; w < 2 * slices; w++)
                    {
                        int current = i * (2 * slices + 1) * (2 * slices + 1) + h * (2 * slices + 1) + w;
                        indexListSurfaces.AddRange(new[] { current, current + 1, current + 2 * slices + 2, current + 2 * slices + 1 });

                        indexListEdges.AddRange(new[] { current, current + 1, current, current + 2 * slices + 1 });
                        if (h == 2 * slices - 1)
                            indexListEdges.AddRange(new[] { current + 2 * slices + 2, current + 2 * slices + 1 });
                        if (w == 2 * slices - 1)
                            indexListEdges.AddRange(new[] { current + 1, current + 2 * slices + 2 });
                    }
            types.Add(PT.Quads);
            indices.Add(indexListSurfaces.ToArray());

            types.Add(PT.Lines);
            indices.Add(indexListEdges.ToArray());

            Indices = indices.SelectMany(i => i).Select(i => (uint)i).ToArray();
            TypeCounts = indices.Select(i => i.Length).ToArray();
            Types = types.ToArray();

        }
    }

    /// <summary>
    /// Spehere 球体 (原点と半径で定義される)
    /// </summary>
    public class Sphere : Ellipsoid
    {
        public double Radius;
        public bool UseDefault { get; set; } = false;

        public Sphere(Vector3DBase o, double radius, Material mat, DrawingMode mode, int slices = 0)
          : this(new V3d(o.X, o.Y, o.Z), radius, mat, mode, slices) { }

        /// <summary>
        /// 球体 (原点と半径で定義される)　6*(2*slices+1)^2 の頂点が生成される
        /// </summary>
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
        public static int[] DefaultTypeCounts;
        public static PT[] DefaultTypes;

        /// <summary>
        /// Default形状ついて、Program番号と(VBO, VAO, EBO)を対応付けるDictionary.
        /// </summary>
        static public Dictionary<int, (int VBO, int VAO, int EBO)> DefaultDictionary { get; set; } = new Dictionary<int, (int VBO, int VAO, int EBO)>();

        static Sphere() => SetDefaultSphere();
        static void SetDefaultSphere()
        {
            DefaultVertices = null;
            DefaultIndices = null;
            DefaultTypeCounts = null;
            DefaultTypes = null;
            var sphere = new Sphere(new V3d(0, 0, 0), 1, new Material(0), DrawingMode.Edges, DefaultSlices);
            DefaultIndices = sphere.Indices;
            DefaultTypeCounts = sphere.TypeCounts;
            DefaultVertices = sphere.Vertices;
            DefaultTypes = sphere.Types;
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

        public Pipe(V3d o, V3d vec, double r1, double r2, Material mat, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
            : this(o, vec, r1, r2, mat, null, mode, sole, slices, stacks) { }

        /// <summary>
        /// パイプ
        /// </summary>
        /// <param name="o">始点の位置</param>
        /// <param name="vec">始点から終点へのベクトル</param>
        /// <param name="r1">始点側の半径</param>
        /// <param name="r2">終点側の半径</param>
        /// <param name="mat">素材</param>
        /// <param name="mode">描画モード</param>
        /// <param name="sole">trueの場合は底面を描画する</param>
        /// <param name="slices">高さの分割数</param>
        /// <param name="stacks">円周の分割数</param>
        public Pipe(V3d o, V3d vec, double r1, double r2, Material mat1, Material mat2, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
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
            var maxR = Math.Max(r1, r2);
            CircumscribedSphereRadius = Math.Sqrt(height * height / 4 + maxR * maxR);

            if (slices == 0 && stacks == 0)
            {
                if (r1 == r2)//Crylinderの場合
                {
                    if (Cylinder.DefaultIndices != null && UseFixedArgb)
                    {
                        Vertices = Cylinder.DefaultVertices;
                        Indices = Cylinder.DefaultIndices;
                        TypeCounts = Cylinder.DefaultTypeCounts;
                        Types = Cylinder.DefaultTypes;
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

                        Vertices = Cone.DefaultVertices.Select(v => new Vertex(transMat.Mult(new V4f(v.Position)), rotMat.Mult(v.Normal), mat1.Argb)).ToArray();
                        Indices = Cone.DefaultIndices;
                        TypeCounts = Cone.DefaultTypeCounts;
                        Types = Cone.DefaultTypes;
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

            List<V3d> v = new List<V3d>(), n = new List<V3d>();
            List<int> c = new List<int>();
            //まず側面
            for (int h = 0; h <= slices; h++)
                for (int t = 0; t < stacks; t++)
                {
                    double sin = Math.Sin((double)t / stacks * Math.PI * 2), cos = Math.Cos((double)t / stacks * Math.PI * 2);
                    double r = r1 * (1 - (double)h / slices) + r2 * h / slices;
                    double z = (double)h / slices * height;
                    v.Add(new V3d(r * sin, r * cos, z));
                    n.Add(new V3d(r2 * height * sin, r2 * height * cos, -r2 * (r2 - r1)));
                    c.Add(h < slices / 2 ? mat1.Argb : mat2.Argb);
                }

            var current = 0;
            var indiceSide = new List<int>();
            for (int h = 0; h < slices; h++)
                for (int t = 0; t < stacks; t++)
                {
                    current = h * stacks + t;
                    if (t < stacks - 1)
                        indiceSide.AddRange(new[] { current, current + stacks, current + 1 + stacks, current + 1 });
                    else
                        indiceSide.AddRange(new[] { current, current + stacks, current + 1, current + 1 - stacks });
                }
            var types = new List<PT>();
            var indices = new List<int[]>();

            types.Add(PT.Quads);
            indices.Add(indiceSide.ToArray());

            types.Add(PT.LineLoop);
            indices.Add(indiceSide.ToArray());

            types.Add(PT.Points);
            indices.Add(Enumerable.Range(0, v.Count).ToArray());

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
                    indices.Add(indicesTop.ToArray());
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
                    indices.Add(indicesBottom.ToArray());
                }
            }

            var vList = new List<Vertex>();
            for (int i = 0; i < v.Count; i++)
                vList.Add(new Vertex((rotMat.Mult(v[i]) + o).ToV3f(), rotMat.Mult(n[i]).ToV3f(), c[i]));

            Vertices = vList.ToArray();
            Indices = indices.SelectMany(i => i).Select(i => (uint)i).ToArray();
            TypeCounts = indices.Select(i => i.Length).ToArray();
            Types = types.ToArray();
        }
    }

    /// <summary>
    /// 円錐 (頂点の位置、頂点からの底面中心のベクトル、半径で定義される)
    /// slicesは高さの分割数, stacksは経線の分割数
    /// </summary>
    public class Cone : Pipe
    {
        /// <summary>
        /// Default形状ついて、Program番号と(VBO, VAO, EBO)を対応付けるDictionary.
        /// </summary>
        static public Dictionary<int, (int VBO, int VAO, int EBO)> DefaultDictionary { get; set; } = new Dictionary<int, (int VBO, int VAO, int EBO)>();

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
        public static int[] DefaultTypeCounts;
        public static PT[] DefaultTypes;


        public bool UseDefault = false;

        public Cone(Vector3DBase o, Vector3DBase vec, double r, Material mat, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
            : this(new V3d(o.X, o.Y, o.Z), new V3d(vec.X, vec.Y, vec.Z), r, mat, mode, sole, slices, stacks) { UseDefault = slices == 0; }

        /// <summary>
        /// 円錐
        /// </summary>
        /// <param name="o">頂点の位置</param>
        /// <param name="vec">頂点から底面中心へのベクトル</param>
        /// <param name="r">底面の半径</param>
        /// <param name="mat">素材</param>
        /// <param name="mode">描画モード</param>
        /// <param name="sole">trueの場合は底面を描画する</param>
        /// <param name="slices">高さの分割数</param>
        /// <param name="stacks">経線の分割数</param>
        public Cone(V3d o, V3d vec, double r, Material mat, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
           : base(o, vec, 0, r, mat, mode, sole, slices, stacks)
        { }

        static Cone()
        {
            SetDefaultCone();
        }
        static void SetDefaultCone()
        {
            DefaultVertices = null;
            DefaultIndices = null;
            DefaultTypeCounts = null;
            DefaultTypes = null;
            var cone = new Cone(new V3d(0, 0, 0), new V3d(0, 0, 1), 1, new Material(0), DrawingMode.Edges, true, 0, 0);
            DefaultIndices = cone.Indices;
            DefaultTypeCounts = cone.TypeCounts;
            DefaultVertices = cone.Vertices;
            DefaultTypes = cone.Types;
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

        /// <summary>
        /// 円柱 (始点の位置、始点から終点へのベクトル、半径で定義される)
        /// </summary>
        /// <param name="o">始点の位置</param>
        /// <param name="vec">始点から終点へのベクトル</param>
        /// <param name="r">半径</param>
        /// <param name="mat">素材</param>
        /// <param name="mode">描画モード</param>
        /// <param name="sole">trueの場合は底面を描画する</param>
        /// <param name="slices">高さの分割数</param>
        /// <param name="stacks">経線の分割数</param>
        public Cylinder(V3d o, V3d vec, double r, Material mat, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
           : base(o, vec, r, r, mat, mode, sole, slices, stacks) { UseDefault = slices == 0; }

        public Cylinder(V3d o, V3d vec, double r, Material mat1, Material mat2, DrawingMode mode, bool sole = true, int slices = 0, int stacks = 0)
         : base(o, vec, r, r, mat1, mat2, mode, sole, slices, stacks) { UseDefault = slices == 0; }


        /// <summary>
        /// Default形状ついて、Program番号と(VBO, VAO, EBO)を対応付けるDictionary.
        /// </summary>
        static public Dictionary<int, (int VBO, int VAO, int EBO)> DefaultDictionary { get; set; } = new Dictionary<int, (int VBO, int VAO, int EBO)>();
        
        public new static (int Slices, int Stacks) Default { get => _Default; set { _Default = value; SetDefaultCylinder(); } }       
        private static (int Slices, int Stacks) _Default = (1, 16);

        public static Vertex[] DefaultVertices;
        public static uint[] DefaultIndices;
        public static int[] DefaultTypeCounts;
        public static PT[] DefaultTypes;

        static Cylinder() => SetDefaultCylinder();

        static void SetDefaultCylinder()
        {
            DefaultVertices = null;
            DefaultIndices = null;
            DefaultTypeCounts = null;
            DefaultTypes = null;
            var cylinder = new Cylinder(new V3d(0, 0, 0), new V3d(0, 0, 1), 1, new Material(0), DrawingMode.Edges, true, 0, 0);
            DefaultIndices = cylinder.Indices;
            DefaultTypeCounts = cylinder.TypeCounts;
            DefaultVertices = cylinder.Vertices;
            DefaultTypes = cylinder.Types;
        }
    }

    #endregion

    #region Torus (ドーナッツ)
    /// <summary>
    /// Torus (ドーナッツ).  中心(V3)、法線(V3), 大半径(double), 小半径(double)で定義される
    /// </summary>
    public class Torus : GLObject
    {
        public const int DefaultSlices1 = 45;
        public const int DefaultSlices2 = 10;

        public V3d Origin;
        public V3d Normal;
        public double Radius1;
        public double Radius2;

        /// <summary>
        /// Torus (ドーナッツ).  中心(V3)、法線(V3), 大半径(double), 小半径(double)で定義される
        /// </summary>
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
                var normal = r2 * new V3d(Math.Cos(theta), 0, Math.Sin(theta));
                var position = new V3d(r1, 0, 0) + normal;
                circleVertex.Add((position, normal));
            }

            //次に、この円をZ軸の周りに回転させてすべての頂点を計算
            for (int i = 1; i < slices1; i++)
            {
                var rot = M3d.CreateRotationZ((double)i / slices1 * 2 * Math.PI);
                for (int j = 0; j < slices2; j++)
                    circleVertex.Add((rot.Mult(circleVertex[j].Position), rot.Mult(circleVertex[j].Normal)));
            }
            //GLGeometry.
            //すべての頂点を座標変換し、vListに追加
            var vList = new List<Vertex>();
            //var transMat = M3d.CreateFromAxisAngle(V3d.Cross(vZ, Normal), V3d.CalculateAngle(Normal, vZ)).ToMatrix4D();
            var transMat = GLGeometry.CreateRotationToZ(norm).ToMatrix4D();
            transMat.Column3 = new V4d(origin, 1);
            for (int i = 0; i < circleVertex.Count; i++)
            {
                var position = transMat.Mult(circleVertex[i].Position.ToV4d());
                var normal = transMat.Mult(circleVertex[i].Normal.ToV4d());
                vList.Add(new Vertex(position.ToV3f(), normal.ToV3f(), mat.Argb));
            }
            //最後に、surfacesやedgeを計算しやすくするため、最初のslices個分の頂点を追加
            for (int i = 0; i < slices2; i++)
                vList.Add(vList[i]);

            Vertices = vList.ToArray();

            var types = new List<PT>();
            var indices = new List<int[]>();

            //Points
            types.Add(PT.Points);
            indices.Add(Enumerable.Range(0, Vertices.Length).ToArray());

            //SurfacesとEdges
            var indexListSurfaces = new List<int>();
            var indexListEdges = new List<int>();
            for (int i = 0; i < slices1; i++)
                for (int j = 0; j < slices2 - 1; j++)
                {
                    int current = i * slices2 + j;

                    indexListSurfaces.AddRange(new[] { current, current + 1, current + slices2 + 1, current + slices2 });
                    indexListEdges.AddRange(new[] { current, current + 1, current, current + slices2 });

                    if (j == 0)
                    {
                        indexListSurfaces.AddRange(new[] { current, current + slices2, current + 2 * slices2 - 1, current + slices2 - 1 });
                        indexListEdges.AddRange(new[] { current, current + slices2, current, current + slices2 - 1 });
                    }
                }
            types.Add(PT.Quads);
            indices.Add(indexListSurfaces.ToArray());

            types.Add(PT.Lines);
            indices.Add(indexListEdges.ToArray());

            Indices = indices.SelectMany(i => i).Select(i => (uint)i).ToArray();
            TypeCounts = indices.Select(i => i.Length).ToArray();
            Types = types.ToArray();
        }
    }
    #endregion

    #region メッシュ
    /// <summary>
    /// メッシュ
    /// </summary>
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
                        var pts = new List<Vector3DBase>();
                        foreach (var i in new[] { index, index - 1, index + 1, index - width, index + width })
                            pts.Add(new Vector3DBase(positions[i].X, positions[i].Y, positions[i].Z));
                        var param = Geometriy.GetPlaneEquationFromPoints(pts);

                        norm = new V3f((float)param[0], (float)param[1], (float)param[2]);
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
                    indicesList.AddRange(new[] { i, i + 1, i + width + 1, i + width, });
                }
            Vertices = vList.ToArray();
            Indices = indicesList.Select(i => (uint)i).ToArray();
            TypeCounts = new[] { indicesList.Count() };

            Types = new[] { PT.Quads };
        }
    }
    #endregion

    #region 文字オブジェクト
    /// <summary>
    /// 文字オブジェクト
    /// </summary>
    public class TextObject : GLObject
    {
        private static Dictionary<(string Text, float FontSize, int Argb, bool WhiteEdge), (int TextureNum, int Width, int Height)> dic
            = new Dictionary<(string Text, float FontSize, int Argb, bool WhiteEdge), (int TextureNum, int Width, int Height)>();
        private static PT[] types = new[] { PT.Quads };
        private static uint[] indices = new[]{(uint)0, (uint)1, (uint)2, (uint)3 };
        private static int[] typeCounts = new[]{4 };

        public int TextureNum = -1;
        public int[] Size = null;
        public byte[] Texture = null;

        public TextObject(string text, float fontSize, Vector3DBase position, double popout, bool whiteEdge, Material mat)
            : this(text, fontSize, new V3d(position.X, position.Y, position.Z), popout, whiteEdge, mat) { }
        public TextObject(string text, float fontSize, V3d position, double popout, bool whiteEdge, Material mat) : base(mat, DrawingMode.Text)
        {
            text = text.Trim();
            if (text != "")
            {
                int width, height;
                if (!dic.TryGetValue((text, fontSize, mat.Argb, whiteEdge), out var obj))
                {
                    var fnt = new Font("Tahoma", fontSize);//フォントオブジェクトの作成
                    var strSize = TextRenderer.MeasureText(text, fnt, new Size(600, 100), TextFormatFlags.NoPadding); //文字列を描画するときの大きさを計測する
                    var bmp = new Bitmap(strSize.Width + 2, strSize.Height + 2);
                    var g = Graphics.FromImage(bmp);//ImageオブジェクトのGraphicsオブジェクトを作成する
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    if (whiteEdge)
                        foreach (var (x, y) in new[] { (0f, 0f), (0f, 1f), (0f, 2f), (1f, 2f), (2f, 2f), (2f, 1f), (2f, 0f), (1f, 0f) })
                            g.DrawString(text, fnt, new SolidBrush(Color.FromArgb(128, Color.White)), new RectangleF(x, y, bmp.Width, bmp.Height));

                    g.DrawString(text, fnt, new SolidBrush(Color.FromArgb(mat.Argb)), new RectangleF(1f, 1f, bmp.Width, bmp.Height));
                    fnt.Dispose();//リソースを解放する
                    g.Dispose();//リソースを解放する

                    width = bmp.Width;
                    height = bmp.Height;

                    var argbList = BitmapConverter.ToByteARGB(bmp).ToList();//データの並び順はBGRA

                    #region  余白の部分をトリムする
                    while (argbList.Where((b, i) => i < width * 4).All(b => b == 0))
                    {
                        argbList.RemoveRange(0, width * 4);
                        height--;
                    }
                    while (argbList.Where((b, i) => i >= (height - 1) * width * 4).All(b => b == 0))
                    {
                        argbList.RemoveRange((height - 1) * width * 4, width * 4);
                        height--;
                    }

                    while (argbList.Where((b, i) => i % (width * 4) == 3).All(b => b == 0))
                    {
                        for (int h = height - 1; h >= 0; h--)
                            argbList.RemoveRange(h * width * 4, 4);
                        width--;
                    }
                    while (argbList.Where((b, i) => i % (width * 4) == width * 4 - 1).All(b => b == 0))
                    {
                        for (int h = height; h > 0; h--)
                            argbList.RemoveRange(h * width * 4 - 4, 4);
                        width--;
                    }
                    #endregion

                    //並び順をRGBAに変更
                    for (int i = 0; i < argbList.Count; i += 4) { var t = argbList[i]; argbList[i] = argbList[i + 2]; argbList[i + 2] = t; }

                    //空いてるテクスチャID番号を調べ、TextureNumberに格納 (実際の転送はGenerateで行う)
                    TextureNum = GL.GenTexture();
                    //辞書に登録
                    dic.Add((text, fontSize, mat.Argb, whiteEdge), (TextureNum, width, height));
                    Size = new[] { width, height };
                    Texture = argbList.ToArray();
                }
                else
                {
                    TextureNum = obj.TextureNum;
                    width = obj.Width;
                    height = obj.Height;
                }

                ShowClippedSection = false;//クリップ断面は表示しない

                Vertices = new[] {
                    new Vertex(new V3f(-width/2f,+ height/2f,(float)popout), position.ToV3f() ,new V2f(0,0), mat.Argb, 2),
                    new Vertex(new V3f(+width/2f,+ height/2f,(float)popout), position.ToV3f() ,new V2f(1,0), mat.Argb, 2),
                    new Vertex(new V3f(+width/2f,- height/2f,(float)popout), position.ToV3f() ,new V2f(1,1), mat.Argb, 2),
                    new Vertex(new V3f(-width/2f,- height/2f,(float)popout), position.ToV3f() ,new V2f(0,1), mat.Argb, 2) };

                CircumscribedSphereCenter = new V4d(position, 1);
                Types = types;
                Indices = indices;
                TypeCounts = typeCounts;
            }
        }
    }


    #endregion

}
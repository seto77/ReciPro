using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

#region 定義

using V4f = OpenTK.Vector4;
using V4d = OpenTK.Vector4d;
using V3f = OpenTK.Vector3;
using V3d = OpenTK.Vector3d;
using V2d = OpenTK.Vector2d;
using C4 = OpenTK.Graphics.Color4;
using M4d = OpenTK.Matrix4d;
using M3d = OpenTK.Matrix3d;
using PT = OpenTK.Graphics.OpenGL4.PrimitiveType;

#endregion 定義



namespace Crystallography.OpenGL
{
    /// <summary>
    /// 頂点要素（シェーダの頂点要素と合わせる）
    /// </summary>
    public struct Vertex
    {
        public readonly V4f Position;
        public readonly V3f Normal;
        public readonly V4f Color;

        public Vertex(V3f position, V3f normal, V4f color)
        {
            Position = new V4f(position, 1);
            Normal = normal;
            Color = color;
        }

        public Vertex(V4f position, V3f normal, V4f color)
        {
            Position = position;
            Normal = normal;
            Color = color;
        }

        public static readonly int Stride = Marshal.SizeOf(default(Vertex));
    }

    /// <summary>
    /// 素材要素
    /// </summary>
    public struct Material
    {
        /// <summary>
        /// Color
        /// </summary>
        public C4 Color { get => _Color; set { _Color = value; ColorV = new V4f(_Color.R, _Color.G, _Color.B, _Color.A); } }

        private C4 _Color;
        internal V4f ColorV;

        /// <summary>
        /// 放射 (=自己発光). 法線と始点が一致する場合に強くなる
        /// </summary>
        public float Emission { get; set; }

        /// <summary>
        /// 環境光.この量だけ底上げされる。
        /// </summary>
        public float Ambient { get; set; }

        /// <summary>
        /// 拡散光. 法線と光源が一致する場合に強くなる
        /// </summary>
        public float Diffuse { get; set; }

        /// <summary>
        /// 反射光. 入射角度と反射角度が等しい場合に強くなる
        /// </summary>
        public float Specular { get; set; }

        /// <summary>
        /// 表面の粗さ. 高くすると、反射光領域は小さく、強度は強くなる。
        /// </summary>
        public float SpecularPower { get; set; }

        public Material(C4 color, float ambient, float diffuse, float specular, float specularPow, float emission)
        {
            _Color = color;
            ColorV = new V4f(_Color.R, _Color.G, _Color.B, _Color.A);
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            SpecularPower = specularPow;
            Emission = emission;
        }

        public Material(C4 color, double ambient, double diffuse, double specular, double specularPow, double emission)
            : this(color, (float)ambient, (float)diffuse, (float)specular, (float)specularPow, (float)emission) { }

        public Material(int argb, double ambient, double diffuse, double specular, double specularPow, double emission)
           : this(new C4(System.Drawing.Color.FromArgb(argb).R / 255f, System.Drawing.Color.FromArgb(argb).G / 255f, System.Drawing.Color.FromArgb(argb).B / 255f, System.Drawing.Color.FromArgb(argb).A / 255f),
                 (float)ambient, (float)diffuse, (float)specular, (float)specularPow, (float)emission)
        { }

        public Material(int argb, double tranparency, double ambient, double diffuse, double specular, double specularPow, double emission)
           : this(new C4(System.Drawing.Color.FromArgb(argb).R / 255f, System.Drawing.Color.FromArgb(argb).G / 255f, System.Drawing.Color.FromArgb(argb).B / 255f, (float)tranparency),
                 (float)ambient, (float)diffuse, (float)specular, (float)specularPow, (float)emission)
        { }

        public Material(System.Drawing.Color color, double ambient, double diffuse, double specular, double specularPow, double emission)
            : this(new C4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f), (float)ambient, (float)diffuse, (float)specular, (float)specularPow, (float)emission) { }

        public Material(double red, double green, double blue, double alpha, double ambient, double diffuse, double specular, double specularPow, double emission)
            : this(new C4((float)red, (float)green, (float)blue, (float)alpha), (float)ambient, (float)diffuse, (float)specular, (float)specularPow, (float)emission) { }
    }

    public enum DrawingMode { Surfaces = 1, Edges = 2, SurfacesAndEdges = 4, Points = 8 }

    /// <summary>
    /// OpenGLで描画するオブジェクトを表現する抽象クラス
    /// </summary>
    abstract public class GLObject
    {
        internal int VBO, VAO, EBO;
        internal int Program = -1;
        internal Vertex[] Vertices;
        internal PT[] Types;
        internal int[][] Indices;
        internal int EmissionLocation = -1, AmbientLocation = -1, DiffuseLocation = -1, SpecularLocation = -1, SpecularPowerLocation = -1;
        internal int UseFixedColorLocation = -1, FixedColorLocation = -1, IgnoreNormalSidesLocation = -1, RenderPassLocation = -1;
        internal int passOIT1Index = -1, passOIT2Index = -1, passNormalIndex = -1;

        #region publicなフィールド

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
        public bool UseFixedColor = false;

        #endregion publicなフィールド

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
            GL.DeleteBuffers(1, ref VBO);
            GL.DeleteBuffers(1, ref EBO);
            GL.DeleteVertexArrays(1, ref VAO);
        }

        /// <summary>
        /// program番号をセットし、各バッファオブジェクトなどGPUに転送する. 描画前に必ず一度実行する必要がある。
        /// </summary>
        /// <param name="program"></param>
        public void Generate(int program)
        {
            if (program < 0 || Vertices == null || Vertices.Length == 0)
                return;
            Program = program;

            // VBO作成
            GL.GenBuffers(1, out VBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vertices.Length * Vertex.Stride), Vertices, BufferUsageHint.DynamicDraw);

            // EBO作成
            var indicesList = new List<int>();
            for (int i = 0; i < Indices.Length; i++)
                indicesList.AddRange(Indices[i].ToArray());
            var indices = indicesList.Select(i => (uint)i).ToArray();
            GL.GenBuffers(1, out EBO);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * indices.Length), indices, BufferUsageHint.DynamicDraw);

            // VAO作成
            GL.GenVertexArrays(1, out VAO);
            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            var positionLocation = GL.GetAttribLocation(Program, "Position");
            var normalLocation = GL.GetAttribLocation(Program, "Normal");
            var colorLocation = GL.GetAttribLocation(Program, "Color");
            if (positionLocation == -1 || normalLocation == -1 || colorLocation == -1)
                throw new Exception("cannot find location!");

            //頂点位置
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 4, VertexAttribPointerType.Float, false, Vertex.Stride, 0);
            //法線
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, true, Vertex.Stride, V4f.SizeInBytes);
            //色
            GL.EnableVertexAttribArray(colorLocation);
            GL.VertexAttribPointer(colorLocation, 4, VertexAttribPointerType.Float, false, Vertex.Stride, V4f.SizeInBytes + V3f.SizeInBytes);

            //インデックスを取得
            EmissionLocation = GL.GetUniformLocation(Program, "Emission");
            AmbientLocation = GL.GetUniformLocation(Program, "Ambient");
            DiffuseLocation = GL.GetUniformLocation(Program, "Diffuse");
            SpecularLocation = GL.GetUniformLocation(Program, "Specular");
            SpecularPowerLocation = GL.GetUniformLocation(Program, "SpecularPower");
            UseFixedColorLocation = GL.GetUniformLocation(program, "UseFixedColor");
            IgnoreNormalSidesLocation = GL.GetUniformLocation(program, "IgnoreNormalSides");
            FixedColorLocation = GL.GetUniformLocation(program, "FixedColor");
            passOIT1Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passOIT1");
            passOIT2Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passOIT2");
            passNormalIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passNormal");
            RenderPassLocation = GL.GetSubroutineUniformLocation(Program, ShaderType.FragmentShader, "RenderPass");
        }

        /// <summary>
        /// 物体の材質をGPUに送信する。Render()関数から呼び出される。
        /// </summary>
        /// <param name="drawSurfaces">Surfaceモードか否か</param>
        private void SetMatetrial(bool drawSurfaces)
        {
            GL.Uniform1(EmissionLocation, drawSurfaces ? Material.Emission : 0);
            GL.Uniform1(AmbientLocation, drawSurfaces ? Material.Ambient : 1);
            GL.Uniform1(DiffuseLocation, drawSurfaces ? Material.Diffuse : 0);
            GL.Uniform1(SpecularLocation, drawSurfaces ? Material.Specular : 0);
            GL.Uniform1(SpecularPowerLocation, Material.SpecularPower);
            GL.Uniform1(IgnoreNormalSidesLocation, IgnoreNormalSides ? 1 : 0);//true

            if (UseFixedColor)
            {
                GL.Uniform1(UseFixedColorLocation, 1);//true
                GL.Uniform4(FixedColorLocation, ref Material.ColorV);
            }
            else
                GL.Uniform1(UseFixedColorLocation, 0);
        }

        /// <summary>
        /// レンダリングを実行. Progaramが正しくセットされていない(Generate()をしていない)場合は例外が発生
        /// </summary>
        private void Render()
        {
            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            int offset = 0;
            for (int i = 0; i < Types.Length; i++)
            {
                if ((Types[i] == PT.Triangles || Types[i] == PT.TriangleStrip || Types[i] == PT.TriangleFan || Types[i] == PT.Quads) && (Mode == DrawingMode.Surfaces || Mode == DrawingMode.SurfacesAndEdges))
                    SetMaterialAndDrawElements(true, Types[i], Indices[i].Length, offset);
                else
                {
                    if ((Types[i] == PT.Lines || Types[i] == PT.LinesAdjacency || Types[i] == PT.LineLoop) && (Mode == DrawingMode.Edges || Mode == DrawingMode.SurfacesAndEdges))
                        SetMaterialAndDrawElements(false, Types[i], Indices[i].Length, offset);
                    else if (Types[i] == PT.Points && Mode == DrawingMode.Points)
                        SetMaterialAndDrawElements(false, Types[i], Indices[i].Length, offset);
                }
                offset += Indices[i].Length * sizeof(uint);
            }
        }

        private void SetMaterialAndDrawElements(bool drawSurfaces, PT mode, int count, int offset)
        {
            SetMatetrial(drawSurfaces);
            if (IgnoreNormalSides)
            {
                GL.Disable(EnableCap.CullFace);//CullFace無効化
                GL.DrawElements(mode, count, DrawElementsType.UnsignedInt, offset);
                GL.GetUniformSubroutine(ShaderType.FragmentShader, RenderPassLocation, out int renderPassIndex);  //レンダーパスを取得
                GLable(renderPassIndex == passNormalIndex, EnableCap.CullFace);//CullFaceを元に戻す
            }
            else
                GL.DrawElements(mode, count, DrawElementsType.UnsignedInt, offset);
        }

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
                    GL.GetUniformSubroutine(ShaderType.FragmentShader, RenderPassLocation, out int renderPassIndex);  //レンダーパスを取得
                    GL.Enable(EnableCap.StencilTest);//Stencilテスト有効
                    foreach (var i in indices)
                    {
                        DepthTest(false);//Depthテスト無効
                        GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passNormalIndex);//サブルーチンを通常パスにする
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
                        GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref renderPassIndex);//サブルーチンを元に戻す
                        GL.ColorMask(true, true, true, true);
                        GL.StencilFunc(StencilFunction.Notequal, 0, ~0); //ステンシル値が0でない部分が描画(切断面の描画). ~0 は補数(11111111)
                        GL.Disable(EnableCap.CullFace);//CullFace無効化
                        clip.EnableClips(indices.Where(j => j != i));//i番目のクリップ以外を有効化
                        DepthTest(renderPassIndex == passNormalIndex);//Depthテストを元に戻す (通常モードは有効)
                        clip.Render(i, Material);//i番目のクリップ面を描画
                    }
                    GLable(renderPassIndex == passNormalIndex, EnableCap.CullFace);//CullFaceを元に戻す (通常モードは有効)
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

                var obj = new Quads(pts[0], pts[1], pts[2], pts[3], new Material(1, 0, 0, 1, 0, 1, 0, 0, 0), DrawingMode.Surfaces)
                {
                    UseFixedColor = true,
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

    /// <summary>
    /// 多角形 (凸多角形) 点集合が完全に平面に乗らない場合でも、最小二乗法で法線を求める
    /// </summary>
    public class Polygon : GLObject
    {
        public Polygon(Vector3DBase[] vertices, Material mat, DrawingMode mode) : this(vertices.Select(v => new V3d(v.X, v.Y, v.Z)).ToArray(), mat, mode)
        {
        }

        public Polygon(Material mat, DrawingMode mode, params V3d[] vertices) : this(vertices.ToArray(), mat, mode)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="mat"></param>
        /// <param name="mode"></param>
        public Polygon(V3d[] vertices, Material mat, DrawingMode mode) : base(mat, mode)
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

            var veticesList = new List<Vertex>(vertices.Select(p => new Vertex(p.ToV3f(), normF, mat.ColorV)));
            veticesList.Add(new Vertex(centerF, normF, mat.ColorV));

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

            Indices = lists.ToArray();
            Types = types.ToArray();
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

    /// <summary>
    /// 多面体 (凸多面体)
    /// </summary>
    public class Polyhedron : GLObject
    {
        public Polyhedron(Vector3DBase[] vertices, Material mat, DrawingMode mode)
            : this(vertices.Select(v => new V3d(v.X, v.Y, v.Z)).ToArray(), mat, mode) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="mat"></param>
        /// <param name="mode"></param>
        public Polyhedron(V3d[] vertices, Material mat, DrawingMode mode) : base(mat, mode)
        {
            var center = new V3d(vertices.Average(v => v.X), vertices.Average(v => v.Y), vertices.Average(v => v.Z));
            CircumscribedSphereCenter = new V4d(center, 1);
            CircumscribedSphereRadius = vertices.Max(v => (v - center).Length);

            //任意の三点を選び、平面方程式を作り、それらが最も端面であるかを評価し、端面である場合はリストに加える
            var candidates = new List<List<V3d>>();
            for (int i = 0; i < vertices.Length; i++)
                for (int j = i + 1; j < vertices.Length; j++)
                    for (int k = j + 1; k < vertices.Length; k++)
                    {
                        V3d A = vertices[i], B = vertices[j], C = vertices[k];
                        V3d V = new V3d(
                            (B.Y - A.Y) * (C.Z - A.Z) - (C.Y - A.Y) * (B.Z - A.Z),
                            (B.Z - A.Z) * (C.X - A.X) - (C.Z - A.Z) * (B.X - A.X),
                            (B.X - A.X) * (C.Y - A.Y) - (C.X - A.X) * (B.Y - A.Y)
                         );
                        var d = -V3d.Dot(V, A);

                        if (vertices.All(v => V3d.Dot(v, V) + d < 0.0000001) || vertices.All(v => V3d.Dot(v, V) + d > -0.0000001))
                            if (candidates.All(cand => !(cand.Contains(vertices[i]) && cand.Contains(vertices[j]) && cand.Contains(vertices[k]))))
                                candidates.Add(vertices.Where(v => Math.Abs(V3d.Dot(v, V) + d) < 0.0000001).ToList());
                    }

            //各面を構成する頂点集合に対して
            var vList = new List<Vertex>();
            var iList2 = new List<int[]>();
            var types = new List<PT>();
            var offset = 0;
            foreach (var cand in candidates)
            {
                var polygonInfo = GLGeometry.PolygonInfo(cand.ToArray(), center);

                vList.AddRange(cand.Select(p => new Vertex(p.ToV3f(), polygonInfo.Norm.ToV3f(), mat.ColorV)));//多面体頂点を追加
                vList.Add(new Vertex(polygonInfo.Center.ToV3f(), polygonInfo.Norm.ToV3f(), mat.ColorV));//多面体中心を追加

                var iTemp = new List<int>(new[] { vList.Count - 1 });//多面体中心のインデックスを追加
                var offsetIndices = polygonInfo.Indices.Select(n => n + offset).ToList();
                iTemp.AddRange(offsetIndices.ToArray());//多面体頂点のインデックスを追加

                iList2.Add(iTemp.ToArray());
                types.Add(PT.TriangleFan);

                offsetIndices.RemoveAt(0);
                iList2.Add(offsetIndices.ToArray());
                iList2.Add(offsetIndices.ToArray());
                types.Add(PT.LineLoop);
                types.Add(PT.Points);

                offset += cand.Count + 1;
            }

            Vertices = vList.ToArray();
            Indices = iList2.ToArray();
            Types = types.ToArray();
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

    /// <summary>
    /// 楕円球 (原点と3方向のベクトルで定義される).  6*(2*slices+1)^2 の頂点が生成される
    /// </summary>
    public class Ellipsoid : GLObject
    {
        public const int DefaultSlices = 3;

        public V3d Origin { get; set; }
        public V3d RadiusVector1 { get; set; }
        public V3d RadiusVector2 { get; set; }
        public V3d RadiusVector3 { get; set; }

        public Ellipsoid(Vector3DBase o, Vector3DBase a, Vector3DBase b, Vector3DBase c, Material mat, DrawingMode mode, int slices = DefaultSlices)
        : this(new V3d(o.X, o.Y, o.Z), new V3d(a.X, a.Y, a.Z), new V3d(b.X, b.Y, b.Z), new V3d(c.X, c.Y, c.Z), mat, mode, slices) { }

        /// <summary>
        /// 楕円球 (中心位置と3方向のベクトルで定義される).  6*(2*slices+1)^2 の頂点が生成される
        /// </summary>
        /// <param name="o">中心位置</param>
        /// <param name="a">中心位置からのベクトル1</param>
        /// <param name="b">中心位置からのベクトル2</param>
        /// <param name="c">中心位置からのベクトル3</param>
        /// <param name="mat">素材</param>
        /// <param name="mode">描画モード</param>
        /// <param name="slices">分割数. 6*(2*slices+1)^2 の頂点が生成される. </param>
        public Ellipsoid(V3d o, V3d a, V3d b, V3d c, Material mat, DrawingMode mode, int slices = DefaultSlices) : base(mat, mode)
        {
            Origin = o;
            RadiusVector1 = a;
            RadiusVector2 = b;
            RadiusVector3 = c;

            CircumscribedSphereCenter = new V4d(o, 1);
            CircumscribedSphereRadius = new[] { a.Length, b.Length, c.Length }.Max();

            var transMat = new M4d
            {
                Column0 = new V4d(a, 0),
                Column1 = new V4d(b, 0),
                Column2 = new V4d(c, 0),
                Column3 = new V4d(o, 1)
            };

            if (slices == DefaultSlices && a.LengthSquared == b.LengthSquared && b.LengthSquared == c.LengthSquared && Sphere.DefaultIndices != null)
            {
                Vertices = Sphere.DefaultVertices.Select(v => new Vertex(transMat.Mult(v.Position), v.Normal, mat.ColorV)).ToArray();
                Indices = Sphere.DefaultIndices;
                Types = Sphere.DefaultTypes;
            }
            else
            {
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
                            vList.Add(new Vertex(v.ToV3f(), n.ToV3f(), mat.ColorV));
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

                Indices = indices.ToArray();
                Types = types.ToArray();
            }
        }
    }

    /// <summary>
    /// Spehere 球体 (原点と半径で定義される)
    /// </summary>
    public class Sphere : Ellipsoid
    {
        public double Radius;

        public Sphere(Vector3DBase o, double radius, Material mat, DrawingMode mode, int slices = DefaultSlices)
          : this(new V3d(o.X, o.Y, o.Z), radius, mat, mode, slices) { }

        /// <summary>
        /// 球体 (原点と半径で定義される)　6*(2*slices+1)^2 の頂点が生成される
        /// </summary>
        /// <param name="o">中心位置</param>
        /// <param name="radius">半径</param>
        /// <param name="mat">素材</param>
        /// <param name="mode">描画モード</param>
        /// <param name="slices">分割数.　6*(2*slices+1)^2 の頂点が生成される</param>
        public Sphere(V3d o, double radius, Material mat, DrawingMode mode, int slices = DefaultSlices)
           : base(o, new V3d(radius, 0, 0), new V3d(0, radius, 0), new V3d(0, 0, radius), mat, mode, slices)
        { Radius = radius; }

        public static readonly ParallelQuery<Vertex> DefaultVertices;
        public static readonly int[][] DefaultIndices;
        public static readonly PT[] DefaultTypes;

        static Sphere()
        {
            var sphere = new Sphere(new V3d(0, 0, 0), 1, new Material(0, 0, 0, 0, 0, 0), DrawingMode.Edges, DefaultSlices);
            DefaultIndices = sphere.Indices;
            DefaultVertices = sphere.Vertices.AsParallel();
            DefaultTypes = sphere.Types;
        }
    }

    /// <summary>
    /// パイプ (始点の位置、始点から終点へのベクトル、始点側の半径, 終点側の半径 で定義される)
    /// slicesは高さの分割数, stacksは経線の分割数
    /// </summary>
    public class Pipe : GLObject
    {
        public const int DefaultSlices = 2;
        public const int DefaultStacks = 8;

        public double Radius1, Radius2;
        public V3d Origin, Vector;

        public Pipe(Vector3DBase o, Vector3DBase vec, double r1, double r2, Material mat, DrawingMode mode, int slices = DefaultSlices, int stacks = DefaultStacks)
            : this(new V3d(o.X, o.Y, o.Z), new V3d(vec.X, vec.Y, vec.Z), r1, r2, mat, mode, slices, stacks) { }

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
        public Pipe(V3d o, V3d vec, double r1, double r2, Material mat, DrawingMode mode, int slices = DefaultSlices, int stacks = DefaultStacks) : base(mat, mode)
        {
            ShowClippedSection = true;
            Origin = o;
            Vector = vec;
            Radius1 = r1;
            Radius2 = r2;

            var height = vec.Length;
            CircumscribedSphereCenter = new V4d(new V3d(o + vec / 2.0), 1);
            var maxR = Math.Max(r1, r2);
            CircumscribedSphereRadius = Math.Sqrt(height * height / 4 + maxR * maxR);

            if (slices == DefaultSlices && stacks == DefaultStacks && r1 == r2 && Cylinder.DefaultIndices != null)//デフォルトのシリンダーの場合
            {
                var rotMat = GLGeometry.CreateRotationFromZ(vec);//回転行列を計算
                var transMat = new M4d(rotMat) * new M4d(r1, 0, 0, 0, 0, r1, 0, 0, 0, 0, vec.Length, 0, 0, 0, 0, 1);
                transMat.Column3 = new V4d(o, 1);

                Vertices = Cylinder.DefaultVertices.Select(v => new Vertex(transMat.Mult(v.Position), rotMat.Mult(v.Normal), mat.ColorV)).ToArray();
                Indices = Cylinder.DefaultIndices;
                Types = Cylinder.DefaultTypes;
            }
            else if (slices == DefaultSlices && stacks == DefaultStacks &&  r2 ==0 && Cone.DefaultIndices != null)//デフォルトのコーンの場合
            {
                var rotMat = GLGeometry.CreateRotationFromZ(vec);//回転行列を計算
                var transMat = new M4d(rotMat) * new M4d(r2, 0, 0, 0, 0, r2, 0, 0, 0, 0, vec.Length, 0, 0, 0, 0, 1);
                transMat.Column3 = new V4d(o, 1);

                Vertices = Cone.DefaultVertices.Select(v => new Vertex(transMat.Mult(v.Position), rotMat.Mult(v.Normal), mat.ColorV)).ToArray();
                Indices = Cone.DefaultIndices;
                Types = Cone.DefaultTypes;
            }
            else
            {
                List<V3d> v = new List<V3d>(), n = new List<V3d>();

                //まず側面
                for (int h = 0; h <= slices; h++)
                    for (int t = 0; t < stacks; t++)
                    {
                        double sin = Math.Sin((double)t / stacks * Math.PI * 2), cos = Math.Cos((double)t / stacks * Math.PI * 2);
                        double r = r1 * (1 - (double)h / slices) + r2 * h / slices;
                        double z = (double)h / slices * height;
                        v.Add(new V3d(r * sin, r * cos, z));
                        n.Add(new V3d(r2 * height * sin, r2 * height * cos, -r2 * (r2 - r1)));
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
                //始点側
                if (r1 > 0)
                {
                    v.Add(new V3d(0, 0, 0));
                    n.Add(-vZ);
                    var indicesTop = new List<int> { v.Count - 1 };
                    int center = v.Count - 1;
                    for (int t = 0; t < stacks; t++)
                    {
                        v.Add(v[t]);
                        n.Add(-vZ);
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
                    var indicesBottom = new List<int> { v.Count - 1 };
                    int center = v.Count - 1;
                    for (int t = 0; t < stacks; t++)
                    {
                        v.Add(v[(slices + 1) * stacks - t - 1]);
                        n.Add(vZ);
                        indicesBottom.Add(v.Count - 1);
                    }
                    indicesBottom.Add(v.Count - stacks);
                    types.Add(PT.TriangleFan);
                    indices.Add(indicesBottom.ToArray());
                }

                var rotMat = GLGeometry.CreateRotationFromZ(vec);

                var vList = new List<Vertex>();
                for (int i = 0; i < v.Count; i++)
                    vList.Add(new Vertex((rotMat.Mult(v[i]) + o).ToV3f(), rotMat.Mult(n[i]).ToV3f(), mat.ColorV));

                Vertices = vList.ToArray();
                Indices = indices.ToArray();
                Types = types.ToArray();
            }
        }
    }

    /// <summary>
    /// 円錐 (頂点の位置、頂点からの底面中心のベクトル、半径で定義される)
    /// slicesは高さの分割数, stacksは経線の分割数
    /// </summary>
    public class Cone : Pipe
    {
        public static readonly ParallelQuery<Vertex> DefaultVertices;
        public static readonly int[][] DefaultIndices;
        public static readonly PT[] DefaultTypes;

        public Cone(Vector3DBase o, Vector3DBase vec, double r, Material mat, DrawingMode mode, int slices = DefaultSlices, int stacks = DefaultStacks)
            : this(new V3d(o.X, o.Y, o.Z), new V3d(vec.X, vec.Y, vec.Z), r, mat, mode, slices, stacks) { }

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
        public Cone(V3d o, V3d vec, double r, Material mat, DrawingMode mode, int slices = DefaultSlices, int stacks = DefaultStacks)
           : base(o, vec, 0, r, mat, mode, slices, stacks)
        { }

        static Cone()
        {
            var cone = new Cone(new V3d(0, 0, 0), new V3d(0, 0, 1), 1, new Material(0, 0, 0, 0, 0, 0), DrawingMode.Edges, DefaultSlices, DefaultStacks);
            DefaultIndices = cone.Indices;
            DefaultVertices = cone.Vertices.AsParallel();
            DefaultTypes = cone.Types;
        }
    }

    /// <summary>
    /// 円柱 (始点の位置、始点から終点へのベクトル、半径で定義される)
    /// slicesは高さの分割数, stacksは経線の分割数
    /// </summary>
    public class Cylinder : Pipe
    {
        public static readonly ParallelQuery<Vertex> DefaultVertices;
        public static readonly int[][] DefaultIndices;
        public static readonly PT[] DefaultTypes;
        public Cylinder(Vector3DBase o, Vector3DBase vec, double r, Material mat, DrawingMode mode, int slices = DefaultSlices, int stacks = DefaultStacks)
           : this(new V3d(o.X, o.Y, o.Z), new V3d(vec.X, vec.Y, vec.Z), r, mat, mode, slices, stacks) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="o">始点の位置</param>
        /// <param name="vec">始点から終点へのベクトル</param>
        /// <param name="r">半径</param>
        /// <param name="mat">素材</param>
        /// <param name="mode">描画モード</param>
        /// <param name="sole">trueの場合は底面を描画する</param>
        /// <param name="slices">高さの分割数</param>
        /// <param name="stacks">経線の分割数</param>
        public Cylinder(V3d o, V3d vec, double r, Material mat, DrawingMode mode, int slices = DefaultSlices, int stacks = DefaultStacks)
           : base(o, vec, r, r, mat, mode, slices, stacks)
        { }

       

        static Cylinder()
        {
            var cylinder = new Cylinder(new V3d(0, 0, 0), new V3d(0, 0, 1), 1, new Material(0, 0, 0, 0, 0, 0), DrawingMode.Edges, DefaultSlices, DefaultStacks);
            DefaultIndices = cylinder.Indices;
            DefaultVertices = cylinder.Vertices.AsParallel();
            DefaultTypes = cylinder.Types;
        }
    }


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
            M3d rot;
            for (int i = 1; i < slices1; i++)
            {
                rot = M3d.CreateRotationZ((double)i / slices1 * 2 * Math.PI);
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
                vList.Add(new Vertex(position.ToV3f(), normal.ToV3f(), mat.ColorV));
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

            Indices = indices.ToArray();
            Types = types.ToArray();
        }
    }


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
                    vList[index] = new Vertex(positions[index], norm, c);
                }

            var indicesList = new List<int>();
            for (int h = 0; h < height - 1; h++)
                for (int w = 0; w < width - 1; w++)
                {
                    int i = h * width + w;
                    indicesList.AddRange(new[] { i, i + 1, i + width + 1, i + width, });
                }

            Vertices = vList.ToArray();
            Indices = new[] { indicesList.ToArray() };

            Types = new[] { PrimitiveType.Quads };
        }
    }
}
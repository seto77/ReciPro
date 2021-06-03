using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Col4 = OpenTK.Graphics.Color4;
using Mat4d = OpenTK.Matrix4d;
using Mat4f = OpenTK.Matrix4;
using Vec2d = OpenTK.Vector2d;
using Vec3d = OpenTK.Vector3d;
using Vec3f = OpenTK.Vector3;
using System.Management;
using System.Runtime.ExceptionServices;

namespace Crystallography.OpenGL
{
    unsafe public partial class GLControlAlpha : UserControl
    {
        #region フィールド
        private Mat4f m4id = Mat4f.Identity;
        private readonly int CounterBuffer = 0;
        private readonly int LinkedListBuffer = 1;
        private readonly uint[] buffers = new uint[2] { 0, 0 };
        private uint headPtrTex = 0, clearBuf = 0;

        private Clip Clip = null;
        private readonly List<GLObject> glObjects = new();
        private readonly ParallelQuery<GLObject> glObjectsP;
        private readonly GLObject quad = null;

        private readonly int eyePositionLocation = 0;
        private readonly int viewportSizeLocation = 0;
        private readonly int lightPositionLocation = 0;
        private readonly int viewMatrixLocation = 0;
        private readonly int projMatrixLocation = 0;
        private readonly int worldMatrixLocation = 0;
        private int passOIT1Index = 0;
        private int passOIT2Index = 0;

        private readonly int depthCueingNearLocation = 0;
        private readonly int depthCueingFarLocation = 0;
        private readonly int depthCueingEnabledLocation = 0;
        #endregion フィールド

        #region Enum

        public enum ProjectionModes { Perspective, Orhographic }

        public enum RotationModes { Object, View, Light }

        public enum TranslatingModes { Object, View }

        public enum FragShaders { OIT, ZSORT}

        #endregion Enum

        #region イベント
        public new MouseEventHandler MouseMove;
        public new MouseEventHandler MouseDown;
        public new MouseEventHandler MouseUp;

        public new PaintEventHandler Paint;


        /// <summary>
        /// WorldMatrixが変化したときに発生するイベント. 
        /// </summary>
        [Browsable(true)]
        [Description("WorldMatrixが変化したときに発生するイベント. マウスで回転させた場合や、WorldMatrixに直接setしたときに発生。")]
        public event EventHandler WorldMatrixChanged;

        #endregion イベント

        #region static な property, field. OpengGLのバージョン関連
        /// <summary>
        /// OpenGLを無効にするか。 Versionチェックが出来ないときなどに、Trueになる。
        /// </summary>
        public static bool DisablingOpenGL { get; set; }

        /// <summary>
        /// 動作しているOpenGLのバージョン (3桁整数, 430など)
        /// </summary>
        public static int Version { get; }

        /// <summary>
        /// 動作しているOpenGLのバージョン (文字列, 4.3.0など)
        /// </summary>
        public static string VersionStr => $"{Version / 100}.{Version / 10 % 10}.{Version % 10}";

        /// <summary>
        /// Z-sortのために最低必要なOpenGLのバージョン (3桁整数, 330など)
        /// </summary>
        public static int VersionForZsort { get; } = 150;
        /// <summary>
        /// Z-sortのために最低必要なOpenGLのバージョン (文字列, 3.3.0など)
        /// </summary>
        public static string VersionForZsortStr => $"{VersionForZsort / 100}.{VersionForZsort / 10 % 10}.{VersionForZsort % 10}";
        /// <summary>
        /// Z-sortの条件を満たしているか
        /// </summary>
        public static bool ZsortEnabled => VersionForZsort <= Version;


        /// <summary>
        /// OITのために最低必要なOpenGLのバージョン (3桁整数, 430など)
        /// </summary>
        public static int VersionForOit { get; } = 430;
        /// <summary>
        /// OITのために最低必要なOpenGLのバージョン (文字列, 3.3.0など)
        /// </summary>
        public static string VersionForOitStr => $"{VersionForOit / 100}.{VersionForOit / 10 % 10}.{VersionForOit % 10}";

        /// <summary>
        /// OITのバージョンを満たしているか.
        /// </summary>
        public static bool OitEnabled => VersionForOit <= Version;



        #endregion

        #region プロパティ

        /// <summary>
        /// VisualStudioデザイナーの編集の時はTrue
        /// </summary>
        public new bool DesignMode
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    return true;
                Control ctrl = this;
                while (ctrl != null)
                {
                    if (ctrl.Site != null && ctrl.Site.DesignMode)
                        return true;
                    ctrl = ctrl.Parent;
                }
                return false;
            }
        }

        public int Program { get; } = -1;

        #region OIT関連
        // This is the maximum supported framebuffer width and height. We
        // could support higher resolutions, but this is reasonable for
        // this application

        /// <summary>
        /// 画像の最大幅 (OITのパラメータ)
        /// </summary>
        [Category("Rendering properties")]
        public int MaxWidth { get; set; } = 2560;

        /// <summary>
        /// 画像の最大高さ (OITのパラメータ)
        /// </summary>
        [Category("Rendering properties")]
        public int MaxHeight { get; set; } = 1440;

        /// <summary>
        /// OIT時の最大ノード数を決める係数. NodeCoefficient * MaxWidth * MaxHeight * 24 bytes　(=5*float+ 1*uint)  分のメモリーが確保される
        /// ここの数値をどれくらい大きくするか。オリジナルでは20にしていたが。。。
        /// この値を変更しても、SetShader()は実行されない (その後FragShaderを変更する必要あり)。
        /// </summary>
        [Category("Rendering properties")]
        public int NodeCoefficient { get; set; } = 10;


        /// <summary>
        /// OIT時に、どれだけの数の重なり合いを考慮するかをパラメータ.
        /// </summary>
        [Category("Rendering properties")]
        public int MaxFragments { get; set; } = 100;

        #endregion

        /// <summary>
        /// 透明度計算としてZsort(要150以上) あるいはOIT (Order Independent Transparency, 要430以上)を用いるか.
        /// コンストラクタのみで設定可能
        /// </summary>
        [Category("Rendering properties")]
        public FragShaders FragShader { get; } = FragShaders.ZSORT;


        #region Depth Cueing
        /// <summary>
        /// Depth cueingのプロパティ。変更すると、SetDepthCueing()が走る.
        /// </summary>
        [Category("Rendering properties")]
        public (bool Enabled, double Zfar, double Znear) DepthCueing {
            get => depthCueing;
            set
            {
                depthCueing = value;
                if(!DesignMode && Program != -1)
                    setDepthCueing();
            } 
        }

        private (bool Enabled, double Zfar, double Znear) depthCueing = (false, -1.5, 0.5);

        #endregion

        #region マウス関連
        /// <summary>
        /// マウスによる回転操作を許可するか
        /// </summary>
        [Category("Mouse Operation")]
        public bool AllowMouseRotation { get; set; } = true;

        /// <summary>
        /// マウスによる平行移動操作を許可するか
        /// </summary>
        [Category("Mouse Operation")]
        public bool AllowMouseTranslating { get; set; } = true;

        /// <summary>
        /// マウスによるスケーリング操作を許可するか
        /// </summary>
        [Category("Mouse Operation")]
        public bool AllowMouseScaling { get; set; } = true;

        [Category("Mouse Operation")]
        public RotationModes RotationMode { get; set; } = RotationModes.Object;

        [Category("Mouse Operation")]
        public TranslatingModes TranslatingMode { get; set; } = TranslatingModes.View;

        #endregion


        /// <summary>
        /// 投影モード
        /// </summary>
        [Category("Rendering properties")]
        public ProjectionModes ProjectionMode { get => projectionMode; set { projectionMode = value; setProjMatrix(); } }
        private ProjectionModes projectionMode = ProjectionModes.Orhographic;

        /// <summary>
        /// バックグラウンドカラー
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Rendering properties")]
        public Col4 BackgroundColor { get => backgroundColor; set { backgroundColor = value; Render(); } }
        private Col4 backgroundColor = Col4.White;

        #region Geometry関連

        /// <summary>
        /// 光源(Light)の位置
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Vec3d LightPosition { get => lightPosition; set { lightPosition = value; lightPositionF = value.ToV3f(); Render(); } }

        private Vec3d lightPosition = new(10, 10, 10);
        private Vec3f lightPositionF = new(10, 10, 10);

        /// <summary>
        /// 回転中心座標 (ワールド回転および光源に対して共通)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Vec3d RotationCenter { get; set; } = new Vec3d(0, 0, 0);

        /// <summary>
        /// ワールド座標系マトリックス. setすると、Renderし、WorldMatrixChangedイベントが発生
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Mat4d WorldMatrix
        {
            get => worldMatrix;
            set
            {
                worldMatrix = value;
                worldMatrixF = WorldMatrix.ToM4f();
                Render();
                WorldMatrixChanged?.Invoke(this, new EventArgs());
            }
        }

        private Mat4d worldMatrix = Mat4d.Identity;
        private Mat4f worldMatrixF = Mat4f.Identity;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [Category("Geometry")]
        public Matrix3D WorldMatrixEx
        {
            get => worldMatrix.ToMatrix3D();
            set
            {
                if (value != null)
                {
                    worldMatrix.M11 = value.E11; worldMatrix.M12 = value.E12; worldMatrix.M13 = value.E13;
                    worldMatrix.M21 = value.E21; worldMatrix.M22 = value.E22; worldMatrix.M23 = value.E23;
                    worldMatrix.M31 = value.E31; worldMatrix.M32 = value.E32; worldMatrix.M33 = value.E33;
                    worldMatrixF = worldMatrix.ToM4f();
                    Render();
                }
            }
        }

        public static List<(string Product, string Version)> GraphicsInfo { get; set; } = new List<(string Product, string Version)>();

        /// <summary>
        /// カメラの位置
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Vec3d ViewFrom { get => viewFrom; set { viewFrom = value; viewFromF = value.ToV3f(); setViewMatrix(); } }

        private Vec3d viewFrom = new(0, 0, 20);
        private Vec3f viewFromF = new(0, 0, 20);

        /// <summary>
        /// カメラのターゲット
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Vec3d ViewTo { get => viewTo; set { viewTo = value; setViewMatrix(); } }

        private Vec3d viewTo = new(0, 0, 0);

        /// <summary>
        /// カメラの上方向
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Vec3d ViewUp { get => viewUp; set { viewUp = value; setViewMatrix(); } }

        private Vec3d viewUp = new(0, 1, 0);

        /// <summary>
        /// カメラ(ビュー)マトリックス
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Mat4d ViewMatrix { get => viewMatrix; set { viewMatrix = value; viewMatrixF = value.ToM4f(); Render(); } }

        private Mat4d viewMatrix = Mat4d.Identity;
        private Mat4f viewMatrixF = Mat4f.Identity;

        private void setViewMatrix() => ViewMatrix = Mat4d.LookAt(ViewFrom, ViewTo, ViewUp);

        /// <summary>
        /// 投影面における中心位置(2次元)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Vec2d ProjCenter { get => projCenter; set { projCenter = value; setProjMatrix(); } }

        private Vec2d projCenter = new(0, 0);

        /// <summary>
        /// 投影面の横の長さ(GL空間での単位)
        /// </summary>
        [Category("Geometry")]
        public double ProjWidth { get => projWidth; set { projWidth = value; setProjMatrix(); } }

        private double projWidth = 4f;

        /// <summary>
        /// 投影面のアスペクト比
        /// </summary>
        [Category("Geometry")]
        private double ProjAspect { get => glControl == null ? 0 : (double)glControl.ClientSize.Height / glControl.ClientSize.Width; }

        /// <summary>
        /// プロジェクション(投影)マトリックス
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Mat4d ProjMatrix { get => projMatrix; set { projMatrix = value; projMatrixF = value.ToM4f(); Render(); } }

        private Mat4d projMatrix = Mat4d.Identity;
        private Mat4f projMatrixF = Mat4f.Identity;

        private void setProjMatrix()
        {

            double x = projCenter.X, y = projCenter.Y, w = ProjWidth / 2, h = ProjWidth * ProjAspect / 2;
            double left = x - w, right = x + w, bottom = y - h, top = y + h;
            double zNear = 1f, coeff = zNear / viewFrom.Length;
            if (projectionMode == ProjectionModes.Orhographic)
                ProjMatrix = Mat4d.CreateOrthographicOffCenter(left, right, bottom, top, -1000, 1000);
            else
                ProjMatrix = Mat4d.CreatePerspectiveOffCenter(left * coeff, right * coeff, bottom * coeff, top * coeff, zNear, 1000);
        }
        #endregion


        #endregion プロパティ

        #region ロード関連
        private readonly GLControl glControl;// = new GLControl();
        private readonly Graphics glControlGraphics;

        /// <summary>
        /// //バージョンチェック。メモリの例外（通常のCatchでは捉えられない）を吐くので別メソッドにした。
        /// </summary>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]
        private static string[] CheckVersion()
        {
            try
            {
                var ver = GL.GetString(StringName.Version).Substring(0, 5).Split(new[] { '.', ' ' });
                return ver.Length != 3 ? null : ver;
            }
            catch { throw new Exception(); }
        }

        /// <summary>
        /// GLContorolのGraphicsを得る。メモリの例外（通常のCatchでは捉えられない）を吐くので別メソッドにした。
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]

        private static Graphics getGraphics(GLControl control)
        {
            try { return control.CreateGraphics(); }
            catch { return null; }
        }

        /// <summary>
        /// 静的コンストラクタ. バージョン情報などはここでチェック
        /// </summary>
        static GLControlAlpha()
        {
            //ビデオカード検索
            var searcher = new ManagementObjectSearcher(new SelectQuery("Win32_VideoController"));
            foreach (var envVar in searcher.Get())
                GraphicsInfo.Add((envVar["name"].ToString(), envVar["DriverVersion"].ToString()));

            var glcontrol = new GLControl();
            glcontrol.MakeCurrent();

            //バージョンチェック
            var ver = CheckVersion();
            if (ver == null)
            {
                DisablingOpenGL = true;
                return;
            }
            Version = 0;
            if (int.TryParse(ver[0], out var temp0))
                Version += temp0 * 100;
            if (int.TryParse(ver[1], out var temp1))
                Version += temp1 * 10;
            if (int.TryParse(ver[2], out var temp2))
                Version += temp2;
        }

        /// <summary>
        /// コンストラクタ. ZsortかOITかは、コンストラクタで決める. 生成後に変更はできない。
        /// </summary>
        public GLControlAlpha(FragShaders shaders = FragShaders.ZSORT)
        {
            InitializeComponent();

            if (DisablingOpenGL || DesignMode || !ZsortEnabled)
                return;

            FragShader = shaders;

            #region glControlの初期化
            SuspendLayout();
            // glControlのコンストラクタで、GraphicsModeを指定する必要があるが、これをするとデザイナが壊れるので、ここに書く。
            var gMode = new GraphicsMode(GraphicsMode.Default.ColorFormat, GraphicsMode.Default.Depth, 8, FragShader == FragShaders.ZSORT ? 2 : 0);
            glControl = new GLControl(gMode)
            {
                AutoScaleMode = AutoScaleMode.Dpi,
                BackColor = Color.White,
                Name = "glControl",
                Dock = DockStyle.Fill,
                VSync = false,
            };
            Controls.Add(glControl);

            glControl.Paint += glControl_Paint;
            glControl.MouseDown += glControl_MouseDown;
            glControl.MouseMove += glControl_MouseMove;
            glControl.MouseWheel += GlControl_MouseWheel;
            glControl.MouseUp += glControl_MouseUp;
            glControl.Resize += glControl_Resize;

            var g = getGraphics(glControl);
            if (g == null)
                return;
            glControlGraphics = g;

            ResumeLayout();
            //glControlの再初期化ここまで
            #endregion

            glControl.MakeCurrent();

            //Shader転送
            var frag = FragShader == FragShaders.ZSORT ? Properties.Resources.fragZSORT :
                Properties.Resources.fragOIT.Replace("MAX_FRAGMENTS ##", $"MAX_FRAGMENTS {MaxFragments}");
            Program = CreateShader(Properties.Resources.vert, Properties.Resources.geom, frag);

            GL.UseProgram(Program);

            //Index取得
            viewportSizeLocation = GL.GetUniformLocation(Program, "ViewportSize");
            eyePositionLocation = GL.GetUniformLocation(Program, "EyePosition");
            lightPositionLocation = GL.GetUniformLocation(Program, "LightPosition");
            viewMatrixLocation = GL.GetUniformLocation(Program, "ViewMatrix");
            projMatrixLocation = GL.GetUniformLocation(Program, "ProjMatrix");
            worldMatrixLocation = GL.GetUniformLocation(Program, "WorldMatrix");
            depthCueingEnabledLocation = GL.GetUniformLocation(Program, "DepthCueing");
            depthCueingFarLocation = GL.GetUniformLocation(Program, "Far");
            depthCueingNearLocation = GL.GetUniformLocation(Program, "Near");

            GL.Disable(EnableCap.CullFace);//CullFaceは常に無効
            GL.Enable(EnableCap.Texture2D);

            if (FragShader == FragShaders.OIT)
            {
                initShaderStorage(); //Shader storage初期化

                GL.Disable(EnableCap.DepthTest);//oitモードの時、 DepthTest無効、

                passOIT1Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passOIT1");
                passOIT2Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passOIT2");

                quad = new Quads(new Vec3d(-1, -1, 1), new Vec3d(1, -1, 1), new Vec3d(1, 1, 1), new Vec3d(-1, 1, 1), new Material(0), DrawingMode.Surfaces);
                quad.Generate(Program);
            }
            else
            {//Zsortモードの時、DepthTest有効
                //GL.Enable(EnableCap.CullFace);
                //GL.CullFace(CullFaceMode.Back);
                GL.Enable(EnableCap.DepthTest);
                GL.DepthMask(true);

                GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
                GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
                GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);

                GL.Enable(EnableCap.LineSmooth);
                GL.Enable(EnableCap.PolygonSmooth);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            }

            Clip?.Generate(Program);
            setDepthCueing();

            glObjectsP = glObjects.AsParallel();

            setViewMatrix();
            setProjMatrix();
            setDepthCueing();
        }


        #region　Shaderの作成 (CreateShader)
        /// <summary>
        /// シェーダーを作成する。SetShaderから呼ばれる
        /// </summary>
        /// <param name="vertexShaderCode"></param>
        /// <param name="fragmentShaderCode"></param>
        /// <returns></returns>
        private static int CreateShader(string vertexShaderCode, string geometryShaderCode, string fragmentShaderCode)
        {
            int vshader = GL.CreateShader(ShaderType.VertexShader);
            //int gshader = GL.CreateShader(ShaderType.GeometryShader);
            int fshader = GL.CreateShader(ShaderType.FragmentShader);

            // Vertex shader
            GL.ShaderSource(vshader, vertexShaderCode);
            GL.CompileShader(vshader);
            GL.GetShaderInfoLog(vshader, out string info);
            GL.GetShader(vshader, ShaderParameter.CompileStatus, out int status_code);
            if (status_code != 1)
            {
                if (AssemblyState.IsDebug)
                    MessageBox.Show("Error in vertex shader ");
                throw new ApplicationException(info);
            }

            /*
            // Geometry shader
            GL.ShaderSource(gshader, geometryShaderCode);
            GL.CompileShader(gshader);
            GL.GetShaderInfoLog(gshader, out info);
            GL.GetShader(gshader, ShaderParameter.CompileStatus, out status_code);
            if (status_code != 1)
                throw new ApplicationException(info);
            */

            // Fragment shader
            GL.ShaderSource(fshader, fragmentShaderCode);
            GL.CompileShader(fshader);
            GL.GetShaderInfoLog(fshader, out info);
            GL.GetShader(fshader, ShaderParameter.CompileStatus, out status_code);
            if (status_code != 1)
            {
                if (AssemblyState.IsDebug)
                    MessageBox.Show("Error in fragment shader ");
                throw new ApplicationException(info);
            }

            int program = GL.CreateProgram();
            GL.AttachShader(program, vshader);
            //GL.AttachShader(program, gshader);
            GL.AttachShader(program, fshader);

            GL.DeleteShader(vshader);
            //GL.DeleteShader(gshader);
            GL.DeleteShader(fshader);

            GL.LinkProgram(program);
            return program;
        }

        #endregion ロード関連

        #region Shader Storage の初期化
        /// <summary>
        /// Shader Storage の初期化
        /// </summary>
        private void initShaderStorage()
        {
            GL.GenBuffers(2, buffers);
            var maxNodes = (uint)(NodeCoefficient * MaxWidth * MaxHeight);
            var nodeSize = 5 * sizeof(float) + sizeof(uint); // The size of a linked list node

            // Our atomic counter
            GL.BindBufferBase(BufferRangeTarget.AtomicCounterBuffer, 0, buffers[CounterBuffer]);
            GL.BufferData(BufferTarget.AtomicCounterBuffer, sizeof(uint), IntPtr.Zero, BufferUsageHint.DynamicDraw);

            // The buffer for the head pointers, as an image texture
            GL.GenTextures(1, out headPtrTex);
            GL.BindTexture(TextureTarget.Texture2D, headPtrTex);
            GL.TexStorage2D(TextureTarget2d.Texture2D, 1, SizedInternalFormat.R32ui, MaxWidth, MaxHeight);
            GL.BindImageTexture(0, headPtrTex, 0, false, 0, TextureAccess.ReadWrite, SizedInternalFormat.R32ui);

            // The buffer of linked lists
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, buffers[LinkedListBuffer]);
            GL.BufferData(BufferTarget.ShaderStorageBuffer, (int)(maxNodes * nodeSize), IntPtr.Zero, BufferUsageHint.DynamicDraw);
            GL.Uniform1(GL.GetUniformLocation(Program, "MaxNodes"), maxNodes);
            var headPtrClearBuf = Enumerable.Repeat(0xffffffff, MaxWidth * MaxHeight).ToArray();
            GL.GenBuffers(1, out clearBuf);
            GL.BindBuffer(BufferTarget.PixelUnpackBuffer, clearBuf);
            GL.BufferData(BufferTarget.PixelUnpackBuffer, headPtrClearBuf.Length * sizeof(uint), headPtrClearBuf, BufferUsageHint.StaticDraw);
        }
        #endregion Shader Storage の初期化
        #endregion

        #region GlObjectの追加/削除

        public void MakeCurrent() => glControl.MakeCurrent();

        public void AddObjects(GLObject obj)
        {
            if (Program < 1) return;
            glControl.MakeCurrent();
            obj.Generate(Program);
            glObjects.Add(obj);
        }

        public void AddObjects(IEnumerable<GLObject> objs)
        {
            if (Program < 1) return;
            glControl.MakeCurrent();
            GLObject.Generate(Program, objs);
            glObjects.AddRange(objs);
        }

        public void DeleteObjects(GLObject obj)
        {
            if (Program < 1) return;
            glControl.MakeCurrent();
            obj.Dispose();
            glObjects.Remove(obj);
        }

        public void DeleteAllObjects()
        {
            if (Program < 1 || glObjects.Count == 0) return;
            glControl.MakeCurrent();

            foreach (var o in glObjectsP.Distinct(o => o.VAO))
                o.Dispose();

            glObjects.Clear();
        }

        #endregion

        #region クリップ操作

        /// <summary>
        /// Clipをセットする。nullをセットした場合はクリップが無効化される
        /// </summary>
        /// <param name="clip"></param>
        public void SetClip(Clip clip)
        {
            glControl.MakeCurrent();
            Clip = clip;
            clip?.Generate(Program);
        }

        #endregion

        #region レンダリング
        public bool SkipRendering = false;

        /// <summary>
        /// レンダリング
        /// </summary>
        public void Render()
        {
            if (InvokeRequired)//別スレッドから呼び出されたとき Invokeして呼びなおす
            {
                Invoke(new Action(() => Render()), null);
                return;
            }

            if (SkipRendering || Program < 1)
                return;

            glControl.MakeCurrent();

            if (glObjects == null || glObjects.Count == 0 || glObjectsP.All(obj => obj.Rendered == false))
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(BackgroundColor);
                glControl.SwapBuffers();//swap
                return;
            }

            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
            GL.Uniform2(viewportSizeLocation, new Vector2(glControl.ClientSize.Width, glControl.ClientSize.Height));

            //マトリックスをセット
            GL.Uniform3(eyePositionLocation, viewFromF);
            GL.Uniform3(lightPositionLocation, lightPositionF);
            GL.UniformMatrix4(viewMatrixLocation, false, ref viewMatrixF);
            GL.UniformMatrix4(projMatrixLocation, false, ref projMatrixF);
            GL.UniformMatrix4(worldMatrixLocation, false, ref worldMatrixF);

            if (FragShader == FragShaders.OIT)//oitモードの時
            {
                var bgcolor = BackgroundColor.ToV3f();
                GL.Uniform3(GL.GetUniformLocation(Program, "BgColor"), ref bgcolor);

                //clearBuffers();
                uint zero = 0;
                GL.BindBufferBase(BufferRangeTarget.AtomicCounterBuffer, 0, buffers[CounterBuffer]);
                GL.BufferSubData(BufferTarget.AtomicCounterBuffer, (IntPtr)0, sizeof(uint), (IntPtr)(&zero));
                GL.BindBuffer(BufferTarget.PixelUnpackBuffer, clearBuf);
                GL.BindTexture(TextureTarget.Texture2D, headPtrTex);
                GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, MaxWidth, MaxHeight, PixelFormat.RedInteger, PixelType.UnsignedInt, IntPtr.Zero);

                //pass1();
                GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passOIT1Index);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                glObjects.ForEach(o => o.Render(Clip));// draw scene

                //pass2();
                GL.MemoryBarrier(MemoryBarrierFlags.ShaderStorageBarrierBit);
                GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passOIT2Index);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.UniformMatrix4(viewMatrixLocation, false, ref m4id);
                GL.UniformMatrix4(projMatrixLocation, false, ref m4id);
                GL.UniformMatrix4(worldMatrixLocation, false, ref m4id);
                quad?.Generate(Program);//理由はよく分からんが、Generateしておかないと、うまく描画できないことが多い
                quad?.Render(null);// Draw a screen filler
            }
            else//Zsortモードの時
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(BackgroundColor);

                //描画対称に透明なものが一つでもあるとき
                if (glObjectsP.Any(o => o.Rendered && (o.Material.Color.A != 1 || o is TextObject)))
                {
                    var rot = worldMatrix.Inverted();
                    glObjectsP.ForAll(o => o.Z = o.Rendered && (o.Material.Color.A != 1 || o is TextObject) ? rot.Mult(o.CircumscribedSphereCenter).Z : double.NegativeInfinity);
                    glObjects.Sort((o1, o2) => o1.Z.CompareTo(o2.Z));
                }
                glObjects.ForEach(o => o.Render(Clip));// draw scene
            }
            glControl.SwapBuffers();//swap
            GL.Finish();

            Paint?.Invoke(this, new PaintEventArgs(glControlGraphics, glControl.ClientRectangle));
        }
        #endregion

        #region マウス操作
        private Point lastMousePosition = new();

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove?.Invoke(sender, e);

            double dx = e.X - lastMousePosition.X, dy = lastMousePosition.Y - e.Y;

            if (e.Button == MouseButtons.Left && AllowMouseRotation)
            {
                double x = e.X - glControl.ClientSize.Width / 2.0, y = e.Y - glControl.Height / 2.0;
                var r = Math.Min(glControl.ClientSize.Width / 2.0, glControl.Height / 2.0);
                if (r * r * 0.7 > x * x + y * y)
                {
                    WorldMatrix *= Mat4d.CreateRotationX((float)(-dy / 100)) * Mat4d.CreateRotationY((float)(dx / 100));
                }
                else
                {
                    var lastx = lastMousePosition.X - glControl.ClientSize.Width / 2.0;
                    var lasty = lastMousePosition.Y - glControl.ClientSize.Height / 2.0;
                    var angle = Math.Atan2(x, y) - Math.Atan2(lastx, lasty);
                    WorldMatrix *= Mat4d.CreateRotationZ((float)angle);
                }
            }
            else if (e.Button == MouseButtons.Middle && AllowMouseTranslating)
            {
                var shiftX = (float)(ProjWidth / glControl.ClientSize.Width * dx);
                var shiftY = (float)(ProjWidth / glControl.ClientSize.Width * dy);
                ProjCenter = new Vec2d(projCenter.X - shiftX, projCenter.Y - shiftY);
                //setProjMatrix();
            }
            else if (e.Button == MouseButtons.Right && AllowMouseScaling)
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control && ProjectionMode == ProjectionModes.Perspective)
                    SetPerspectiveDistance(viewFrom.Length * (float)(1 + dy * 0.005));
                else
                {
                    var coeff = 1 + dy * 0.005;
                    if (coeff > 0.5)
                        ProjWidth *= coeff;
                }
            }

            lastMousePosition = new Point(e.X, e.Y);
        }

        private void GlControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None && AllowMouseScaling)
            {
                var coeff = 1 - e.Delta * 0.0008;
                if (coeff > 0.5)
                    ProjWidth *= coeff;
            }

        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);

            //右ダブルクリックは、Orthographic <=> Perspective の切り替え
            if (e.Clicks == 2 && e.Button == MouseButtons.Right && (ModifierKeys & Keys.Control) == Keys.Control)
                ProjectionMode = ProjectionMode == ProjectionModes.Orhographic ? ProjectionModes.Perspective : ProjectionModes.Orhographic;
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp?.Invoke(sender, e);
        }

        #endregion

        #region Perspective distanceの設定
        public void SetPerspectiveDistance(double distance)
        {
            SkipRendering = true;
            viewFrom = viewFrom.Normalized() * distance;
            viewFromF = viewFromF.Normalized() * (float)distance;
            setViewMatrix();
            SkipRendering = false;
            setProjMatrix();

        } 
        #endregion

        #region GLControlのイベント

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode) return;
            Render();
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            setProjMatrix();
        }

        #endregion

        #region ビットマップ画像の作成

        public Bitmap GenerateBitmap()
        {
            glControl.MakeCurrent();
            var bmp = new Bitmap(glControl.ClientSize.Width, glControl.Height);
            System.Drawing.Imaging.BitmapData data = bmp.LockBits(Rectangle.FromLTRB(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, bmp.Width, bmp.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            return BitmapConverter.FlipVertically(bmp);
        }

        #endregion

        #region Depth Cueingの設定

        private void setDepthCueing()
        {
            glControl.MakeCurrent();
            GL.Uniform1(depthCueingEnabledLocation, DepthCueing.Enabled ? 1 : 0);
            GL.Uniform1(depthCueingNearLocation, (float)DepthCueing.Znear);
            GL.Uniform1(depthCueingFarLocation, (float)DepthCueing.Zfar);
            GL.Finish();
            Render();
        }


        #endregion

    }
}
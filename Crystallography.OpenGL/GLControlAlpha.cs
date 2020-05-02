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

namespace Crystallography.OpenGL
{
    unsafe public partial class GLControlAlpha : UserControl
    {
        #region フィールド
        private Mat4f m4id = Mat4f.Identity;
        private int CounterBuffer = 0, LinkedListBuffer = 1;
        private uint[] buffers = new uint[2] { 0, 0 };
        private uint headPtrTex = 0, clearBuf = 0;

        private Clip Clip = null;
        private List<GLObject> glObjects = new List<GLObject>();
        private GLObject quad = null;

        private int eyePositionIndex = 0;
        private int lightPositionIndex = 0;
        private int viewMatrixIndex = 0;
        private int projMatrixIndex = 0;
        private int worldMatrixIndex = 0;
        private int passOIT1Index = 0;
        private int passOIT2Index = 0;
        private int passNormalIndex = 0;
        #endregion フィールド

        #region Enum

        public enum ProjectionModes { Perspective, Orhographic }

        public enum RotationModes { Object, View, Light }

        public enum TranslatingModes { Object, View }

        public enum RenderingTransparencyModes { Always, NotAlways, Never }

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

        /// <summary>
        /// OpenGLのバージョン (3桁整数, 430など)
        /// </summary>
        private int verCur = 0;

        public string GLVersionCurrent
        {
            get
            {
                int v1 = verCur / 100, v2 = (verCur - v1 * 100) / 10, v3 = verCur - v1 * 100 - v2 * 10;
                return v1.ToString() + "." + v2.ToString() + "." + v3.ToString();
            }
        }

        private int verReq { get; } = 430;
        public string GLVersionRequired { get => "4.3.0"; }

        public bool GLRequirement { get => verCur >= verReq; }

        private int Program { get; set; } = -1;

        // This is the maximum supported framebuffer width and height. We
        // could support higher resolutions, but this is reasonable for
        // this application

        /// <summary>
        /// 画像の最大幅
        /// </summary>
        [Category("Rendering properties")]
        public int MaxWidth { get; set; } = 2560;

        /// <summary>
        /// 画像の最大高さ
        /// </summary>
        [Category("Rendering properties")]
        public int MaxHeight { get; set; } = 1440;

        /// <summary>
        /// 最大ノード数を決める係数. NodeCoefficient * MaxWidth * MaxHeight * 24 bytes　(=5*float+ 1*uint)  分のメモリーが確保される
        /// ここの数値をどれくらい大きくするか。オリジナルでは20にしていたが。。。
        /// </summary>
        [Category("Rendering properties")]
        public int NodeCoefficient { get; set; } = 4;

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

        /// <summary>
        /// 投影モード
        /// </summary>
        [Category("Rendering properties")]
        public ProjectionModes ProjectionMode { get => projectionMode; set { projectionMode = value; setProjMatrix(); } }

        private ProjectionModes projectionMode = ProjectionModes.Orhographic;

        /// <summary>
        /// Order Independent Transparency modeを有効にするかどうか
        /// </summary>
        [Category("Rendering properties")]
        public RenderingTransparencyModes RenderingTransparency { get; set; } = RenderingTransparencyModes.NotAlways;

        /// <summary>
        /// バックグラウンドカラー
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Rendering properties")]
        public Col4 BackgroundColor { get => backgroundColor; set { backgroundColor = value; Render(); } }

        private Col4 backgroundColor = Col4.White;

        /// <summary>
        /// 光源(Light)の位置
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Vec3d LightPosition { get => lightPosition; set { lightPosition = value; lightPositionF = value.ToV3f(); Render(); } }

        private Vec3d lightPosition = new Vec3d(10, 10, 10);
        private Vec3f lightPositionF = new Vec3f(10, 10, 10);

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

        /// <summary>
        /// カメラの位置
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Vec3d ViewFrom { get => viewFrom; set { viewFrom = value; viewFromF = value.ToV3f(); setViewMatrix(); } }

        private Vec3d viewFrom = new Vec3d(0, 0, 20);
        private Vec3f viewFromF = new Vec3f(0, 0, 20);

        /// <summary>
        /// カメラのターゲット
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Vec3d ViewTo { get => viewTo; set { viewTo = value; setViewMatrix(); } }

        private Vec3d viewTo = new Vec3d(0, 0, 0);

        /// <summary>
        /// カメラの上方向
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Geometry")]
        public Vec3d ViewUp { get => viewUp; set { viewUp = value; setViewMatrix(); } }

        private Vec3d viewUp = new Vec3d(0, 1, 0);

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

        private Vec2d projCenter = new Vec2d(0, 0);

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

        public bool DisablingOpenGL { get; set; } = false;

        #endregion プロパティ

        #region ロード関連
        private System.Timers.Timer timer = new System.Timers.Timer(100);
        private GLControl glControl = new GLControl();
        private Graphics glControlGraphics;

        public GLControlAlpha()
        {
            if (DisablingOpenGL)
                return;

            InitializeComponent();

            if (DesignMode) return;

            // glControlのコンストラクタで、GraphicsModeを指定する必要があるが、これをするとデザイナが壊れるので、ここに書く。
            glControl = new OpenTK.GLControl(new GraphicsMode(GraphicsMode.Default.ColorFormat, GraphicsMode.Default.Depth, 8))
            {
                AutoScaleMode = AutoScaleMode.Dpi,
                BackColor = Color.White,
                Location = new Point(0, 0),
                Name = "glControl",
                Size = new Size(this.Size.Width, this.Size.Height),
                Dock = DockStyle.Fill,
                TabIndex = 1,
                VSync = false,
            };
            glControl.Load += glControl_Load;
            glControl.Paint += glControl_Paint;
            glControl.MouseDown += glControl_MouseDown;
            glControl.MouseMove += glControl_MouseMove;
            glControl.MouseUp += glControl_MouseUp;
            glControl.Resize += glControl_Resize;

            Controls.Add(glControl);
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            if (DisablingOpenGL)
                return;

            //バージョンチェック
            var ver = GL.GetString(StringName.Version).Substring(0, 5).Split(new[] { '.' });
            verCur = Convert.ToInt32(ver[0]) * 100 + Convert.ToInt32(ver[1]) * 10 + Convert.ToInt32(ver[2]);
            if (verCur < verReq)
                return;

            glControlGraphics = glControl.CreateGraphics();

            glControl.MakeCurrent();

            Program = CreateShader(Properties.Resources.vert, Properties.Resources.geom, Properties.Resources.frag);

            GL.UseProgram(Program);

            eyePositionIndex = GL.GetUniformLocation(Program, "EyePosition");
            lightPositionIndex = GL.GetUniformLocation(Program, "LightPosition");
            viewMatrixIndex = GL.GetUniformLocation(Program, "ViewMatrix");
            projMatrixIndex = GL.GetUniformLocation(Program, "ProjMatrix");
            worldMatrixIndex = GL.GetUniformLocation(Program, "WorldMatrix");

            passOIT1Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passOIT1");
            passOIT2Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passOIT2");
            passNormalIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passNormal");

            initShaderStorage();

            setViewMatrix();
            setProjMatrix();

            quad = new Quads(new Vec3d(-1, -1, 1), new Vec3d(1, -1, 1), new Vec3d(1, 1, 1), new Vec3d(-1, 1, 1), new Material(1, 1, 1, 1, 1, 1, 1, 1, 1), DrawingMode.Surfaces);
            quad.Generate(Program);

            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Render(true);
            timer.Stop();
        }

        #region　Shaderの作成
        /// <summary>
        /// シェーダーを作成する。ロード時に1回だけ呼ばれる
        /// </summary>

        /// <param name="vertexShaderCode"></param>
        /// <param name="fragmentShaderCode"></param>
        /// <returns></returns>
        private int CreateShader(string vertexShaderCode, string geometryShaderCode, string fragmentShaderCode)
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
                throw new ApplicationException(info);

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
                throw new ApplicationException(info);

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
            foreach (var obj in objs)
            {
                obj.Generate(Program);
                glObjects.Add(obj);
            }
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
            if (Program < 1) return;
            glControl.MakeCurrent();
            while (glObjects.Count != 0)
            {
                glObjects[0].Dispose();
                glObjects.RemoveAt(0);
            }
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
        public void Render(bool oit = false)
        {
            if (InvokeRequired)//別スレッドから呼び出されたとき Invokeして呼びなおす
            {
                Invoke(new Action(() => Render(oit)), null);
                return;
            }
            timer.Stop();

            if (SkipRendering || Program < 1)
                return;

            if (RenderingTransparency == RenderingTransparencyModes.Always)
                oit = true;
            else if (RenderingTransparency == RenderingTransparencyModes.Never)
                oit = false;

            glControl.MakeCurrent();

            if (glObjects == null || glObjects.Count == 0 || glObjects.All(obj => obj.Rendered == false))
            {
                GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passNormalIndex);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(BackgroundColor);
                glControl.SwapBuffers();//swap
                return;
            }

            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);

            //マトリックスをセット
            GL.Uniform3(eyePositionIndex, ref viewFromF);
            GL.Uniform3(lightPositionIndex, ref lightPositionF);
            GL.UniformMatrix4(viewMatrixIndex, false, ref viewMatrixF);
            GL.UniformMatrix4(projMatrixIndex, false, ref projMatrixF);
            GL.UniformMatrix4(worldMatrixIndex, false, ref worldMatrixF);

            if (oit)//oitモードの時、 CullFace無効、DepthTest無効、
            {
                GL.Disable(EnableCap.CullFace);
                GL.Disable(EnableCap.DepthTest);
                var bgcolor = BackgroundColor.ToV4f();
                GL.Uniform4(GL.GetUniformLocation(Program, "BgColor"), ref bgcolor);

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
                GL.UniformMatrix4(viewMatrixIndex, false, ref m4id);
                GL.UniformMatrix4(projMatrixIndex, false, ref m4id);
                GL.UniformMatrix4(worldMatrixIndex, false, ref m4id);
                quad.Render(null);// Draw a screen filler
            }
            else//通常モードの時、 CullFace有効、DepthTest有効
            {
                GL.Enable(EnableCap.CullFace);
                GL.CullFace(CullFaceMode.Back);
                GL.Enable(EnableCap.DepthTest);
                GL.DepthMask(true);

                GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passNormalIndex);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(BackgroundColor);
                glObjects.ForEach(o => o.Render(Clip));// draw scene
            }
            glControl.SwapBuffers();//swap
            GL.Finish();

            Paint?.Invoke(this, new PaintEventArgs(glControlGraphics, glControl.ClientRectangle));

            timer.Start();
        }

        #endregion

        #region マウス操作
        private Point lastMousePosition = new Point();

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
                {
                    SkipRendering = true;
                    viewFrom *= (float)(1 + dy * 0.005);
                    setViewMatrix();
                    SkipRendering = false;
                    setProjMatrix();
                }
                else
                {
                    var coeff = 1 + dy * 0.005;
                    if (coeff > 0.5)
                        ProjWidth *= coeff;
                }
            }

            lastMousePosition = new Point(e.X, e.Y);
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(sender, e);

            //右ダブルクリックは、Orthographic <=> Perspective の切り替え
            if (e.Clicks == 2 && e.Button == MouseButtons.Right && (ModifierKeys & Keys.Control) == Keys.Control)
                ProjectionMode = ProjectionMode == ProjectionModes.Orhographic ? ProjectionModes.Perspective : ProjectionModes.Orhographic;
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp?.Invoke(sender, e);
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
    }
}
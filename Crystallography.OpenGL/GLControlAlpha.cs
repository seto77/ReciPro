#region using
using OpenTK.Graphics.OpenGL;
using OpenTK.GLControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics; // 260420Cl 追加 静的コンストラクタでの例外ログ出力用
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Col4 = OpenTK.Mathematics.Color4;
using Mat4d = OpenTK.Mathematics.Matrix4d;
using Mat4f = OpenTK.Mathematics.Matrix4;
using Vec2d = OpenTK.Mathematics.Vector2d;
using Vec3d = OpenTK.Mathematics.Vector3d;
using Vec3f = OpenTK.Mathematics.Vector3;
using OpenTK.Mathematics;
#endregion

namespace Crystallography.OpenGL;

public unsafe partial class GLControlAlpha : UserControl
{
    private sealed class ProgramLocations
    {
        internal int ViewportSize { get; init; }
        internal int EyePosition { get; init; }
        internal int LightPosition { get; init; }
        internal int ViewMatrix { get; init; }
        internal int ProjMatrix { get; init; }
        internal int WorldMatrix { get; init; }
        internal int BackgroundColor { get; init; }
        internal int DepthCueingEnabled { get; init; }
        internal int DepthCueingFar { get; init; }
        internal int DepthCueingNear { get; init; }
        internal int DepthTexture { get; init; } // (260319Ch) PPLL text overlay samples the mesh prepass depth at the label anchor point
        internal int UseAnchorVisibility { get; init; } // (260319Ch) Enable whole-label visibility from the anchor point instead of per-fragment depth clipping
        internal int DepthBlenderTexture { get; init; } // (260319Ch) DDP ping-pong depth interval texture
        internal int FrontBlenderTexture { get; init; } // (260319Ch) DDP front accumulation / final composite texture
        internal int BackBlenderTexture { get; init; } // (260319Ch) DDP back accumulation / final composite texture
        internal int SceneTexture { get; init; } // (260319Ch) Post FXAA samples the already rendered scene color
    }

    #region static な property, field. OpengGLのバージョン関連
    /// <summary>OpenGLを無効にするか。 Versionチェックが出来ないときなどに、Trueになる。</summary>
    public static bool DisablingOpenGL { get; set; }

    /// <summary>動作しているOpenGLのバージョン (3桁整数, 430など)</summary>
    public static int Version { get; }

    /// <summary>動作しているOpenGLのバージョン (文字列, 4.3.0など)</summary>
    public static string VersionStr => $"{Version / 100}.{Version / 10 % 10}.{Version % 10}";

    /// <summary>Z-sortのために最低必要なOpenGLのバージョン (3桁整数, 410など)</summary>
    //public static int VersionForZsort { get; } = 150;
    public static int VersionForZsort { get; } = 410; // (260319Ch) 共通描画パスの下限を OpenGL 4.1 core に引き上げる
    /// <summary>Z-sortのために最低必要なOpenGLのバージョン (文字列, 4.1.0など)</summary>
    public static string VersionForZsortStr => $"{VersionForZsort / 100}.{VersionForZsort / 10 % 10}.{VersionForZsort % 10}";
    /// <summary>Z-sortの条件を満たしているか</summary>
    public static bool ZsortEnabled => VersionForZsort <= Version;

    /// <summary>PPLLのために最低必要なOpenGLのバージョン (3桁整数, 430など)</summary>
    public static int VersionForPpll { get; } = 430;
    /// <summary>PPLLのために最低必要なOpenGLのバージョン (文字列, 3.3.0など)</summary>
    public static string VersionForPpllStr => $"{VersionForPpll / 100}.{VersionForPpll / 10 % 10}.{VersionForPpll % 10}";

    /// <summary>PPLLのバージョンを満たしているか.</summary>
    public static bool PpllEnabled => VersionForPpll <= Version;

    /// <summary>DDPのために最低必要なOpenGLのバージョン (3桁整数, 410など)</summary>
    public static int VersionForDdp { get; } = 410; // (260319Ch) Dual depth peeling は draw-buffers blend を使う 4.1 core を前提とする
    public static string VersionForDdpStr => $"{VersionForDdp / 100}.{VersionForDdp / 10 % 10}.{VersionForDdp % 10}";
    public static bool DdpEnabled => VersionForDdp <= Version;

    private static readonly System.Version VersionForZsortApi = new(4, 1); // (260319Ch) 通常描画は 4.1 core context を要求
    private static readonly System.Version VersionForDdpApi = new(4, 1); // (260319Ch) DDP も 4.1 core context で動かす
    private static readonly System.Version VersionForPpllApi = new(4, 3); // (260319Ch) Ppll は SSBO / atomic counter のため 4.3 を維持

    private static int N = 0;
    private static int CacheKeySeed = 0; // (260319Ch) TextObject cache を GL context ごとに分離するための連番

    #endregion

    #region フィールド
    private Mat4f m4id = Mat4f.Identity;
    private readonly int CounterBuffer = 0;
    private readonly int LinkedListBuffer = 1;
    private readonly uint[] buffers = [0, 0];
    private uint headPtrTex = 0, clearBuf = 0, textDepthTex = 0, postProcessColorTex = 0; // (260319Ch) Post FXAA keeps a copy of the fully rendered scene
    private int postProcessTextureWidth = 0, postProcessTextureHeight = 0; // (260319Ch) FXAA texture follows the current viewport size instead of the MaxWidth/MaxHeight budget
    private int textDepthFbo = 0; // 260319Cl depth-only FBO: prepass renders directly into textDepthTex, avoiding unreliable CopyTexSubImage2D for depth
    private readonly int[] ddpFramebuffers = [0, 0];
    private readonly uint[] ddpDepthTextures = [0, 0];
    private uint ddpFrontBlenderTexture = 0, ddpBackBlenderTexture = 0;
    private int ddpQuery = 0;

    private Clip Clip = null;
    private readonly List<GLObject> glObjects = [];
    private readonly GLObject quad = null;
    private readonly Action renderAction;
    private readonly Dictionary<int, ProgramLocations> programLocations = []; // (260319Ch) ZSORT は mesh/text の 2 program を持てるようにする
    private int passPpll1Index = 0;
    private int passPpll2Index = 0;
    private int passNormalIndex = 0; // (260319Ch) Ppll では text を最後に通常 alpha blend で重ねる
    private int passPpllTextIndex = 0; // (260319Ch) PPLL text は linked-list を再合成して半透明遮蔽を反映する
    private int passDdpInitIndex = 0;
    private int passDdpPeelIndex = 0;
    private int passDdpCompositeIndex = 0;

    private GLControl glControl;// = new GLControl();
    private readonly Graphics glControlGraphics;

    private static readonly DrawBuffersEnum[] ddpDepthDrawBuffers = [DrawBuffersEnum.ColorAttachment0];
    private static readonly DrawBuffersEnum[] ddpPeelDrawBuffers = [DrawBuffersEnum.ColorAttachment0, DrawBuffersEnum.ColorAttachment1, DrawBuffersEnum.ColorAttachment2];
    #endregion フィールド

    #region Enum
    public enum ProjectionModes { Perspective, Orhographic }
    public enum RotationModes { Object, View, Light }
    public enum TranslatingModes { Object, View }
    public enum PostAntiAliasingModes { None, FXAA } // (260319Ch) True MSAA is difficult for OIT paths, so allow a post AA fallback
    public enum FragShaders {
        /// <summary>Per-Pixel Linked List (PPLL)</summary>
        PPLL,
        /// <summary>Object-level Z (depth) sorting with alpha blending (ZSORT)</summary>
        ZSORT,
        /// <summary>Double depth peeling (DDP)</summary>
        DDP

    }
    #endregion Enum

    #region イベント
    public new MouseEventHandler MouseMove;
    public new MouseEventHandler MouseDown;
    public new MouseEventHandler MouseUp;

    public new PaintEventHandler Paint;


    /// <summary>WorldMatrixが変化したときに発生するイベント.</summary>
    [Browsable(true)]
    [Description("WorldMatrixが変化したときに発生するイベント. マウスで回転させた場合や、WorldMatrixに直接 set したときに発生。")]
    public event EventHandler WorldMatrixChanged;

    #endregion イベント

    #region プロパティ

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    public string ToolTip { set { toolTip.SetToolTip(glControl, value); } }

    /// <summary>VisualStudioデザイナーの編集の時はTrue</summary>
    public new bool DesignMode
    {
        get
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;
            System.Windows.Forms.Control ctrl = this;
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
    public int TextProgram { get; } = -1; // (260319Ch) ZSORT text path は専用 shader program に分離する
    public int PostProcessProgram { get; } = -1; // (260319Ch) PPLL/DDP の後段 AA は別 fragment shader で合成する
    public int CacheKey { get; } = 0; // (260319Ch) context 跨ぎの text texture cache 衝突を避ける

    #region PPLL関連
    // This is the maximum supported framebuffer width and height. We
    // could support higher resolutions, but this is reasonable for
    // this application

    /// <summary>画像の最大幅 (PPLLのパラメータ)</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Rendering properties")]
    public int MaxWidth { get; set; } = 2560;

    /// <summary>画像の最大高さ (PPLLのパラメータ)</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Rendering properties")]
    public int MaxHeight { get; set; } = 1440;

    /// <summary>
    /// PPLL時の最大ノード数を決める係数. NodeCoefficient * MaxWidth * MaxHeight * 24 bytes　(=5*float+ 1*uint)  分のメモリーが確保される
    /// ここの数値をどれくらい大きくするか。オリジナルでは20にしていたが。。。
    /// この値を変更しても、SetShader()は実行されない (その後FragShaderを変更する必要あり)。
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Rendering properties")]
    public int NodeCoefficient { get; set; } = 10;

    /// <summary>PPLL時に、どれだけの数の重なり合いを考慮するかをパラメータ.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Rendering properties")]
    public int MaxFragments { get; set; } = 100;

    /// <summary>DDP 時に最大何回 peel pass を回すか. 1 pass で前後 2 層ずつ進む。</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Rendering properties")]
    public int DualDepthPeelingPasses { get; set; } = 12; // (260319Ch) 品質と速度の中間点として 24 層相当を既定値にする

    /// <summary>
    /// DDP peel loop の終了判定に occlusion query を使うか。
    /// 既定では GPU/CPU stall を避けるため無効。
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Rendering properties")]
    public bool DualDepthPeelingUseOcclusionQuery { get; set; } = false; // (260319Ch) 速度優先の既定値では query の同期待ちを避ける

    /// <summary>OIT 系パスの最終画像に対する後段 AA.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Rendering properties")]
    public PostAntiAliasingModes PostAntiAliasing
    {
        get => postAntiAliasing;
        set
        {
            postAntiAliasing = value;
            if (!DesignMode && Program != -1)
                Render();
        }
    }

    #endregion

    /// <summary>
    /// 透明度計算としてZsort(要410以上) あるいはPPLL (要430以上)を用いるか.
    /// コンストラクタのみで設定可能
    /// </summary>
    [Category("Rendering properties")]
    public FragShaders FragShader { get; } = FragShaders.ZSORT;

    #region Depth Cueing
    /// <summary>Depth cueingのプロパティ。変更すると、SetDepthCueing()が走る.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Rendering properties")]
    public (bool Enabled, double Zfar, double Znear) DepthCueing
    {
        get => depthCueing;
        set
        {
            depthCueing = value;
            if (!DesignMode && Program != -1)
                setDepthCueing();
        }
    }

    private (bool Enabled, double Zfar, double Znear) depthCueing = (false, -1.5, 0.5);
    private PostAntiAliasingModes postAntiAliasing = PostAntiAliasingModes.None; // (260319Ch) PPLL/DDP の後段 FXAA 設定

    #endregion

    public static bool DisableTextRendering { get; set; } = true;

    #region マウス関連
    /// <summary>マウスによる回転操作を許可するか</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Mouse Operation")]
    public bool AllowMouseRotation { get; set; } = true;

    /// <summary>マウスによる平行移動操作を許可するか</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Mouse Operation")]
    public bool AllowMouseTranslating { get; set; } = true;

    /// <summary>マウスによるスケーリング操作を許可するか</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Mouse Operation")]
    public bool AllowMouseScaling { get; set; } = true;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Mouse Operation")]
    public RotationModes RotationMode { get; set; } = RotationModes.Object;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Mouse Operation")]
    public TranslatingModes TranslatingMode { get; set; } = TranslatingModes.View;

    #endregion


    /// <summary>投影モード</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Rendering properties")]
    public ProjectionModes ProjectionMode { get => projectionMode; set { projectionMode = value; setProjMatrix(); } }
    private ProjectionModes projectionMode = ProjectionModes.Orhographic;

    /// <summary>バックグラウンドカラー</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Rendering properties")]
    public Col4 BackgroundColor { get => backgroundColor; set { backgroundColor = value; Render(); } }
    private Col4 backgroundColor = Col4.White;

    #region Geometry関連

    /// <summary>光源(Light)の位置</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Geometry")]
    public Vec3d LightPosition { get => lightPosition; set { lightPosition = value; lightPositionF = value.ToV3f(); Render(); } }

    private Vec3d lightPosition = new(10, 10, 10);
    private Vec3f lightPositionF = new(10, 10, 10);

    /// <summary>回転中心座標 (ワールド回転および光源に対して共通)</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Geometry")]
    public Vec3d RotationCenter { get; set; } = new Vec3d(0, 0, 0);

    /// <summary>ワールド座標系マトリックス. setすると、Renderし、WorldMatrixChangedイベントが発生</summary>
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
            // WorldMatrixChanged?.Invoke(this, new EventArgs());
            WorldMatrixChanged?.Invoke(this, EventArgs.Empty); // (260320Ch) 不要な EventArgs 生成を避ける
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
    public static List<(string Product, string Version)> GraphicsInfo { get; set; } = [];

    /// <summary>カメラの位置</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Geometry")]
    public Vec3d ViewFrom { get => viewFrom; set { viewFrom = value; viewFromF = value.ToV3f(); setViewMatrix(); } }

    private Vec3d viewFrom = new(0, 0, 20);
    private Vec3f viewFromF = new(0, 0, 20);

    /// <summary>カメラのターゲット</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Geometry")]
    public Vec3d ViewTo { get => viewTo; set { viewTo = value; setViewMatrix(); } }

    private Vec3d viewTo = new(0, 0, 0);

    /// <summary>カメラの上方向</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Geometry")]
    public Vec3d ViewUp { get => viewUp; set { viewUp = value; setViewMatrix(); } }

    private Vec3d viewUp = new(0, 1, 0);

    /// <summary>カメラ(ビュー)マトリックス</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Geometry")]
    public Mat4d ViewMatrix { get => viewMatrix; set { viewMatrix = value; viewMatrixF = value.ToM4f(); Render(); } }

    private Mat4d viewMatrix = Mat4d.Identity;
    private Mat4f viewMatrixF = Mat4f.Identity;

    private void setViewMatrix() => ViewMatrix = Mat4d.LookAt(ViewFrom, ViewTo, ViewUp);

    /// <summary>投影面における中心位置(2次元)</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Geometry")]
    public Vec2d ProjCenter { get => projCenter; set { projCenter = value; setProjMatrix(); } }

    private Vec2d projCenter = new(0, 0);

    /// <summary>投影面の横の長さ(GL空間での単位)</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
    [Category("Geometry")]
    public double ProjWidth { get => projWidth; set { projWidth = value; setProjMatrix(); } }

    private double projWidth = 4f;

    /// <summary>投影面のアスペクト比</summary>
    [Category("Geometry")]
    // private double ProjAspect => glControl == null ? 0 : (double)glControl.ClientSize.Height / glControl.ClientSize.Width;
    private double ProjAspect => glControl is null || glControl.ClientSize.Width <= 0 || glControl.ClientSize.Height <= 0 ? 0d : (double)glControl.ClientSize.Height / glControl.ClientSize.Width; // (260320Ch) 0 除算や退化アスペクト比を避ける

    /// <summary>プロジェクション(投影)マトリックス</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Geometry")]
    public Mat4d ProjMatrix { get => projMatrix; set { projMatrix = value; projMatrixF = value.ToM4f(); Render(); } }

    private Mat4d projMatrix = Mat4d.Identity;
    private Mat4f projMatrixF = Mat4f.Identity;
    private void setProjMatrix()
    {
        if (glControl is null || glControl.ClientSize.Width <= 0 || glControl.ClientSize.Height <= 0)
            return; // (260320Ch) 初期化途中や最小化直後のゼロサイズでは投影行列を更新しない

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
    /// <summary>//バージョンチェック。メモリの例外（通常のCatchでは捉えられない）を吐くので別メソッドにした。</summary>
    /// <returns></returns>
    //[HandleProcessCorruptedStateExceptions] //260317Cl 削除 SYSLIB0032: .NET Core以降では無視される属性
    private static string[] CheckVersion()
    {
        try
        {
            var versionString = GL.GetString(StringName.Version);
            var ver = versionString?
                .Split(['.', ' '], StringSplitOptions.RemoveEmptyEntries)
                .Where(s => s.All(char.IsDigit))
                .Take(3)
                .ToArray();

            return ver switch
            {
                null or { Length: 0 } => null,
                { Length: 1 } => [ver[0], "0", "0"],
                { Length: 2 } => [ver[0], ver[1], "0"],
                _ => ver,
            }; // (260320Ch) 長さごとの補完を switch 式で簡潔にまとめる
        }
        // catch { throw new Exception(); }
        catch { return null; } // (260320Ch) probe 失敗は呼び出し元のフォールバックに委ねる
    }

    private static GLControlSettings createGlControlSettings(FragShaders shaderMode, bool forVersionProbe = false)
    {
        var apiVersion = shaderMode == FragShaders.PPLL ? VersionForPpllApi :
            shaderMode == FragShaders.DDP ? VersionForDdpApi : VersionForZsortApi; // (260319Ch) DDP は OpenGL 4.1 core context で作成する
        return new GLControlSettings()
        {
            APIVersion = apiVersion,
            NumberOfSamples = forVersionProbe ? 0 : shaderMode == FragShaders.ZSORT ? 2 : 0,
            StencilBits = 8,
            DepthBits = 16,
            Profile = OpenTK.Windowing.Common.ContextProfile.Core,
        };
    }

    private static int checkSupportedVersion(params FragShaders[] shaderModes)
    {
        foreach (var shaderMode in shaderModes)
        {
            try
            {
                using var glcontrol = new GLControl(createGlControlSettings(shaderMode, true));
                glcontrol.MakeCurrent();
                var ver = CheckVersion();
                if (ver == null)
                    continue;

                int version = 0;
                if (int.TryParse(ver[0], out var temp0))
                    version += temp0 * 100;
                if (int.TryParse(ver[1], out var temp1))
                    version += temp1 * 10;
                if (int.TryParse(ver[2], out var temp2))
                    version += temp2;
                if (version > 0)
                    return version;
            }
            catch
            {
                // (260319Ch) 要求バージョンを満たさない context 生成はここで安全に握りつぶし、次候補へフォールバックする。
            }
        }
        return 0;
    }

    /// <summary>GLControlのGraphicsを得る。メモリの例外（通常のCatchでは捉えられない）を吐くので別メソッドにした。</summary>
    /// <param name="control"></param>
    /// <returns></returns>
    //[HandleProcessCorruptedStateExceptions] //260317Cl 削除 SYSLIB0032: .NET Core以降では無視される属性
    private static Graphics getGraphics(GLControl control)
    {
        try { return control.CreateGraphics(); }
        catch { return null; }
    }

    /// <summary>静的コンストラクタ. バージョン情報などはここでチェック</summary>
    static GLControlAlpha()
    {
        // 260420Cl WMI/OpenGL 初期化を try/catch で囲む。
        //   issues #48 #55 で Windows 10 環境において WMI リポジトリ破損等で
        //   ManagementException("Classe non valide") が発生し、静的コンストラクタが
        //   TypeInitializationException を投げてアプリ全体が起動不能になる事例が報告されている。
        //   ここで例外を握り潰し、OpenGL 無効化フォールバック(DisablingOpenGL=true)へ倒す。

        //ビデオカード検索
        try // 260420Cl 追加 WMI クエリを try/catch で保護
        {
            using var searcher = new ManagementObjectSearcher(new SelectQuery("Win32_VideoController")); // (260320Ch) WMI リソースを確実に解放する
            using var videoControllers = searcher.Get();
            foreach (ManagementObject envVar in videoControllers)
                GraphicsInfo.Add((Convert.ToString(envVar["name"]) ?? string.Empty, Convert.ToString(envVar["DriverVersion"]) ?? string.Empty)); // (260320Ch) null 安全に GPU 情報を記録する
        }
        catch (Exception e) // 260420Cl 追加 WMI リポジトリ破損や Win32_VideoController 未提供環境で落ちないようにする
        {
            Debug.WriteLine($"GLControlAlpha: WMI Win32_VideoController query failed: {e.Message}");
        }

        //if(GraphicsInfo.Count>1 && GraphicsInfo[0].Product.Contains("Radeon"))
        //    DisableTextRendering = true;

        //using var glcontrol = new GLControl();
        //glcontrol.MakeCurrent();
        //var ver = CheckVersion();
        try // 260420Cl 追加 OpenGL コンテキスト生成/シェーダ検査中の例外もフォールバック処理へ倒す
        {
            Version = checkSupportedVersion(FragShaders.PPLL, FragShaders.ZSORT); // (260319Ch) まず 4.3、次に 4.1 を試して実際に使える上限を判定
        }
        catch (Exception e) // 260420Cl 追加 ドライバ不良でもアプリ自体は起動を継続させる
        {
            Debug.WriteLine($"GLControlAlpha: OpenGL version check failed: {e.Message}");
            Version = 0;
        }

        if (Version == 0)
        {
            DisablingOpenGL = true;
            return;
        }
    }

    /// <summary>コンストラクタ. Zsort, DDP, PPLLは、コンストラクタで決める. 生成後に変更はできない。</summary>
    public GLControlAlpha(FragShaders shaders = FragShaders.ZSORT)
    {
        InitializeComponent();
        renderAction = Render; // (260319Ch) Invoke 用 delegate を使い回して描画要求時の小さな確保を避ける
        CacheKey = System.Threading.Interlocked.Increment(ref CacheKeySeed); // (260319Ch) control ごとに一意の cache key を振る

        //if (DisablingOpenGL || DesignMode || !ZsortEnabled)
        if (DisablingOpenGL || DesignMode || !ZsortEnabled)
            return;

        if (shaders == FragShaders.PPLL && !PpllEnabled)
            shaders = FragShaders.ZSORT; // (260319Ch) 4.3 未満では安全側で ZSORT にフォールバックする
        if (shaders == FragShaders.DDP && !DdpEnabled)
            shaders = FragShaders.ZSORT; // (260319Ch) DDP も 4.1 未満なら通常描画へ戻す

        FragShader = shaders;

        #region glControlの初期化
        SuspendLayout();
        // glControlのコンストラクタで、GraphicsModeを指定する必要があるが、これをするとデザイナが壊れるので、ここに書く。
        //var setting = new GLControlSettings()
        //{
        //    NumberOfSamples = FragShader == FragShaders.ZSORT ? 2 : 0,
        //    StencilBits = 8,
        //    DepthBits = 16,
        //    Profile = OpenTK.Windowing.Common.ContextProfile.Core,
        //};
        var setting = createGlControlSettings(FragShader); // (260319Ch) shader 要件に応じて 4.1 / 4.3 core context を明示要求する

        glControl = new GLControl(setting)
        {
            BackColor = Color.White,
            Name = $"glControl{N}",
            Text = $"glControl{N++}",
            Dock = DockStyle.Fill,
        };
      
        glControl.Paint += glControl_Paint;
        glControl.MouseDown += glControl_MouseDown;
        glControl.MouseMove += glControl_MouseMove;
        glControl.MouseWheel += GlControl_MouseWheel;
        glControl.MouseUp += glControl_MouseUp;
        glControl.Resize += glControl_Resize;

        Controls.Add(glControl);
        ResumeLayout();

        var g = getGraphics(glControl);
        if (g == null)
            return;
        glControlGraphics = g;
        
        //glControlの再初期化ここまで
        #endregion

        //Shader転送
        var frag = FragShader == FragShaders.ZSORT ?
            Properties.Resources.fragZSORT :
            FragShader == FragShaders.DDP ?
            Properties.Resources.fragDDP : // (260319Ch) DDP は fragDDP.c を使う
            Properties.Resources.fragPPLL.Replace("MAX_FRAGMENTS ##", $"MAX_FRAGMENTS {MaxFragments}");

        // var textProgram = Program; // (260319Ch) 旧来は text も mesh と同じ program/subroutine で処理していた
        Program = CreateShader(Properties.Resources.vert, Properties.Resources.geom, frag);
        // if (FragShader == FragShaders.ZSORT)
        //     TextProgram = CreateShader(Properties.Resources.vertText, Properties.Resources.geom, Properties.Resources.fragZSORTText);
        // TextProgram = CreateShader(Properties.Resources.vertText, Properties.Resources.geom, Properties.Resources.fragZSORTText); // (260319Ch) 旧案: PPLL でも ZSORT 用 text shader を共用していた
        var textFrag = FragShader == FragShaders.PPLL ? Properties.Resources.fragPPLLText : Properties.Resources.fragZSORTText;
        TextProgram = CreateShader(Properties.Resources.vertText, Properties.Resources.geom, textFrag); // (260319Ch) PPLL text は alpha-only shader に分けて黒四角を避ける
        // PostProcessProgram = Program; // (260319Ch) 旧案: 後段 AA なしで main program の出力をそのまま表示する
        if (FragShader != FragShaders.ZSORT)
            PostProcessProgram = CreateShader(Properties.Resources.vert, Properties.Resources.geom, Properties.Resources.fragFXAA); // (260319Ch) PPLL/DDP は最後に FXAA を掛けられるようにする

        registerProgramLocations(Program); // (260319Ch) mesh program 用の共通 uniform location を登録
        if (TextProgram > 0)
            registerProgramLocations(TextProgram); // (260319Ch) text program にも同じ view/proj/depth-cueing を配る
        if (PostProcessProgram > 0)
            registerProgramLocations(PostProcessProgram); // (260319Ch) post FXAA も ViewportSize / SceneTexture を共有登録する

        GL.UseProgram(Program);

        GL.Disable(EnableCap.CullFace);//CullFaceは常に無効

        if (FragShader == FragShaders.PPLL)
        {
            initShaderStorage(); //Shader storage初期化
            initPostProcessTexture(); // (260319Ch) PPLL の最終カラーを FXAA 用 texture にコピーできるようにする

            GL.Disable(EnableCap.DepthTest);//PPLLモードの時、 DepthTest無効、

            // passPpll1Index = GL.GetProgramResourceIndex(Program, ProgramInterface.FragmentSubroutine, "passPPLL1");
            // passPpll2Index = GL.GetProgramResourceIndex(Program, ProgramInterface.FragmentSubroutine, "passPPLL2");
            passPpll1Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passPPLL1"); // (260319Ch) UniformSubroutines には subroutine index を渡す
            passPpll2Index = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passPPLL2"); // (260319Ch) resource index ではなく API 対応の index を使う
            passNormalIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passNormal"); // (260319Ch) text overlay 用の通常描画 path
            passPpllTextIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passLabelOverlay"); // (260319Ch) text pixel ごとに linked-list を再合成する PPLL 専用 path

            quad = new Quads(new Vec3d(-1, -1, 1), new Vec3d(1, -1, 1), new Vec3d(1, 1, 1), new Vec3d(-1, 1, 1), new Material(0), DrawingMode.Surfaces);
            quad.Generate(Program, CacheKey); // (260319Ch) fullscreen quad も control ごとの context key で生成する
        }
        else if (FragShader == FragShaders.DDP)
        {
            initDualDepthPeeling(); // (260319Ch) DDP 用 ping-pong FBO と accumulation texture を初期化する
            initPostProcessTexture(); // (260319Ch) DDP でも final composite を FXAA へ渡せるようにする

            GL.Disable(EnableCap.DepthTest); // (260319Ch) DDP の peel pass 自体は depth test を使わず custom min/max depth で進める
            GL.DepthMask(false);

            passDdpInitIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passDdpInit");
            passDdpPeelIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passDdpPeel");
            passDdpCompositeIndex = GL.GetSubroutineIndex(Program, ShaderType.FragmentShader, "passDdpComposite");

            quad = new Quads(new Vec3d(-1, -1, 1), new Vec3d(1, -1, 1), new Vec3d(1, 1, 1), new Vec3d(-1, 1, 1), new Material(0), DrawingMode.Surfaces);
            quad.Generate(Program, CacheKey); // (260319Ch) DDP final composite でも fullscreen quad を使う
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
        if (TextProgram > 0)
            Clip?.Generate(TextProgram, false); // (260319Ch) text 側は clip plane の uniform だけ共有し、断面 polygon は mesh 側で維持する
        setDepthCueing();

        setViewMatrix();
        setProjMatrix();
        setDepthCueing();

        this.Disposed += GLControlAlpha_Disposed;
    }

    private void GLControlAlpha_Disposed(object sender, EventArgs e)
    {
        Clip = null;
        glControl = null;
        glObjects.Clear();
        glControlGraphics.Dispose();
    }

    #region　Shaderの作成 (CreateShader)
    /// <summary>シェーダーを作成する。SetShaderから呼ばれる</summary>
    /// <param name="vertexShaderCode"></param>
    /// <param name="fragmentShaderCode"></param>
    /// <returns></returns>
    private int CreateShader(string vertexShaderCode, string geometryShaderCode, string fragmentShaderCode)
    {
        glControl.MakeCurrent();

        //なんかよくわからないが、GL.CreateProgram()を複数回呼び出すことで強制的に program の数値を進める. これをすると複数のGlControlを立ち上げてもバグらない。
        for (int i = 0; i < N; i++) GL.CreateProgram();
        
        int program = GL.CreateProgram();

        static int CompileShader(ShaderType type, string source)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);
            if (status == 0)
                throw new Exception($"Failed to compile {type}: {GL.GetShaderInfoLog(shader)}");
            return shader;
        }

        int vshader = CompileShader(ShaderType.VertexShader, vertexShaderCode);
        int fshader = CompileShader(ShaderType.FragmentShader, fragmentShaderCode);

        GL.AttachShader(program, vshader);
        GL.AttachShader(program, fshader);

        GL.LinkProgram(program);

        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string log = GL.GetProgramInfoLog(program);
            throw new Exception($"Could not link program: {log}");
        }

        GL.DetachShader(program, vshader);
        GL.DetachShader(program, fshader);

        GL.DeleteShader(vshader);
        GL.DeleteShader(fshader);
       
        return program;
    }

    private void registerProgramLocations(int program)
    {
        GL.UseProgram(program);
        programLocations[program] = new ProgramLocations
        {
            ViewportSize = GL.GetUniformLocation(program, "ViewportSize"),
            EyePosition = GL.GetUniformLocation(program, "EyePosition"),
            LightPosition = GL.GetUniformLocation(program, "LightPosition"),
            ViewMatrix = GL.GetUniformLocation(program, "ViewMatrix"),
            ProjMatrix = GL.GetUniformLocation(program, "ProjMatrix"),
            WorldMatrix = GL.GetUniformLocation(program, "WorldMatrix"),
            BackgroundColor = GL.GetUniformLocation(program, "BgColor"), // (260319Ch) PPLL と text depth cueing の両方で使い回す
            DepthCueingEnabled = GL.GetUniformLocation(program, "DepthCueing"),
            DepthCueingFar = GL.GetUniformLocation(program, "Far"),
            DepthCueingNear = GL.GetUniformLocation(program, "Near"),
            DepthTexture = GL.GetUniformLocation(program, "DepthTexture"),
            UseAnchorVisibility = GL.GetUniformLocation(program, "UseAnchorVisibility"),
            DepthBlenderTexture = GL.GetUniformLocation(program, "DepthBlenderTex"),
            FrontBlenderTexture = GL.GetUniformLocation(program, "FrontBlenderTex"),
            BackBlenderTexture = GL.GetUniformLocation(program, "BackBlenderTex"),
            SceneTexture = GL.GetUniformLocation(program, "SceneTexture")
        };
    }

    private void applyFrameUniforms(int program)
    {
        if (program < 1 || !programLocations.TryGetValue(program, out var locations))
            return;

        GL.UseProgram(program);
        GL.Uniform2(locations.ViewportSize, new Vector2(glControl.ClientSize.Width, glControl.ClientSize.Height));
        GL.Uniform3(locations.EyePosition, viewFromF);
        GL.Uniform3(locations.LightPosition, lightPositionF);
        GL.UniformMatrix4(locations.ViewMatrix, false, ref viewMatrixF);
        GL.UniformMatrix4(locations.ProjMatrix, false, ref projMatrixF);
        GL.UniformMatrix4(locations.WorldMatrix, false, ref worldMatrixF);

        var bgcolor = BackgroundColor.ToV3f();
        if (locations.BackgroundColor != -1)
            GL.Uniform3(locations.BackgroundColor, ref bgcolor);
    }

    private void applyDepthCueing(int program)
    {
        if (program < 1 || !programLocations.TryGetValue(program, out var locations))
            return;

        GL.UseProgram(program);
        GL.Uniform1(locations.DepthCueingEnabled, DepthCueing.Enabled ? 1 : 0);
        GL.Uniform1(locations.DepthCueingNear, (float)DepthCueing.Znear);
        GL.Uniform1(locations.DepthCueingFar, (float)DepthCueing.Zfar);
    }

    private void copyPpllTextDepthTexture()
    {
        if (textDepthTex == 0 || glControl == null)
            return;

        var width = Math.Min(glControl.ClientSize.Width, MaxWidth);
        var height = Math.Min(glControl.ClientSize.Height, MaxHeight);
        if (width <= 0 || height <= 0)
            return;

        GL.ActiveTexture(TextureUnit.Texture1);
        GL.BindTexture(TextureTarget.Texture2D, textDepthTex);
        GL.CopyTexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, 0, 0, width, height); // (260319Ch) Snapshot the mesh depth prepass so text visibility can be judged from the label anchor
        GL.ActiveTexture(TextureUnit.Texture0);
    }

    private void bindPpllTextDepthTexture()
    {
        if (TextProgram < 1 || textDepthTex == 0 || !programLocations.TryGetValue(TextProgram, out var locations))
            return;

        GL.UseProgram(TextProgram);
        if (locations.DepthTexture != -1)
            GL.Uniform1(locations.DepthTexture, 1);
        if (locations.UseAnchorVisibility != -1)
            GL.Uniform1(locations.UseAnchorVisibility, 1);

        GL.ActiveTexture(TextureUnit.Texture1);
        GL.BindTexture(TextureTarget.Texture2D, textDepthTex);
        GL.ActiveTexture(TextureUnit.Texture0);
    }

    private void initPostProcessTexture()
    {
        if (PostProcessProgram < 1 || postProcessColorTex != 0)
            return;

        GL.GenTextures(1, out postProcessColorTex);
        ensurePostProcessTextureSize();
    }

    private void ensurePostProcessTextureSize()
    {
        if (postProcessColorTex == 0 || glControl == null)
            return;

        var width = Math.Min(glControl.ClientSize.Width, MaxWidth);
        var height = Math.Min(glControl.ClientSize.Height, MaxHeight);
        if (width <= 0 || height <= 0)
            return;
        if (postProcessTextureWidth == width && postProcessTextureHeight == height)
            return;

        // GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, MaxWidth, MaxHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
        // (260319Ch) 旧案: MaxWidth/MaxHeight 固定 texture に部分コピーしていたため、最終表示で左下に縮んで見えた
        GL.BindTexture(TextureTarget.Texture2D, postProcessColorTex);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero); // (260319Ch) FXAA texture tracks the actual viewport size so UV=0..1 maps to the copied scene
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        GL.BindTexture(TextureTarget.Texture2D, 0);
        postProcessTextureWidth = width;
        postProcessTextureHeight = height;
    }

    private bool shouldApplyPostAntiAliasing()
    {
        // return PostAntiAliasing != PostAntiAliasingModes.None; // (260319Ch) 旧案: mode だけで unconditional に有効化する
        return PostAntiAliasing == PostAntiAliasingModes.FXAA
            && PostProcessProgram > 0
            && postProcessColorTex != 0
            && FragShader != FragShaders.ZSORT; // (260319Ch) ZSORT は window-system MSAA を維持し、PPLL/DDP のみ post AA を使う
    }

    private void copyPostProcessColorTexture()
    {
        if (!shouldApplyPostAntiAliasing() || glControl == null)
            return;

        ensurePostProcessTextureSize();
        var width = Math.Min(glControl.ClientSize.Width, MaxWidth);
        var height = Math.Min(glControl.ClientSize.Height, MaxHeight);
        if (width <= 0 || height <= 0)
            return;

        GL.ReadBuffer(ReadBufferMode.Back);
        GL.ActiveTexture(TextureUnit.Texture4);
        GL.BindTexture(TextureTarget.Texture2D, postProcessColorTex);
        GL.CopyTexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, 0, 0, width, height); // (260319Ch) Copy the finished scene once, then run FXAA on that texture
        GL.ActiveTexture(TextureUnit.Texture0);
    }

    private void bindPostProcessSceneTexture()
    {
        if (PostProcessProgram < 1 || postProcessColorTex == 0 || !programLocations.TryGetValue(PostProcessProgram, out var locations))
            return;

        GL.UseProgram(PostProcessProgram);
        if (locations.SceneTexture != -1)
            GL.Uniform1(locations.SceneTexture, 4);
        GL.ActiveTexture(TextureUnit.Texture4);
        GL.BindTexture(TextureTarget.Texture2D, postProcessColorTex);
        GL.ActiveTexture(TextureUnit.Texture0);
    }

    private void resolvePostAntiAliasing()
    {
        if (!shouldApplyPostAntiAliasing())
            return;

        copyPostProcessColorTexture();
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
        GL.Disable(EnableCap.DepthTest);
        GL.DepthMask(false);
        GL.Disable(EnableCap.Blend);
        GL.ColorMask(true, true, true, true);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        applyFrameUniforms(PostProcessProgram);
        bindPostProcessSceneTexture();
        renderFullscreenQuad(PostProcessProgram);
    }

    private void bindDdpTextures(uint depthTexture = 0, uint frontTexture = 0, uint backTexture = 0)
    {
        if (Program < 1 || !programLocations.TryGetValue(Program, out var locations))
            return;

        GL.UseProgram(Program);

        if (locations.DepthBlenderTexture != -1)
        {
            GL.Uniform1(locations.DepthBlenderTexture, 1);
            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, depthTexture);
        }

        if (locations.FrontBlenderTexture != -1)
        {
            GL.Uniform1(locations.FrontBlenderTexture, 2);
            GL.ActiveTexture(TextureUnit.Texture2);
            GL.BindTexture(TextureTarget.Texture2D, frontTexture);
        }

        if (locations.BackBlenderTexture != -1)
        {
            GL.Uniform1(locations.BackBlenderTexture, 3);
            GL.ActiveTexture(TextureUnit.Texture3);
            GL.BindTexture(TextureTarget.Texture2D, backTexture);
        }

        GL.ActiveTexture(TextureUnit.Texture0);
    }

    private int getProgramForObject(GLObject obj)
    {
        // if (TextProgram > 0 && obj is TextObject && FragShader != FragShaders.PPLL)
        // if (TextProgram > 0 && obj is TextObject)
        if (TextProgram > 0 && obj is TextObject && FragShader != FragShaders.PPLL)
            return TextProgram;
        // (260319Ch) text は専用 program を維持するが、PPLL だけは main program の linked-list を読んで半透明遮蔽込みで再合成する
        return Program;
    }

    #endregion ロード関連

    #region Shader Storage の初期化
    /// <summary>Shader Storage の初期化</summary>
    private void initShaderStorage()
    {
        glControl.MakeCurrent();

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

        GL.GenTextures(1, out textDepthTex);
        GL.BindTexture(TextureTarget.Texture2D, textDepthTex);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent24, MaxWidth, MaxHeight, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero); // (260319Ch) Copy the PPLL mesh depth prepass so text can decide visibility from one anchor sample
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

        // 260319Cl depth-only FBO: prepass renders directly into textDepthTex so no CopyTexSubImage2D is needed
        GL.GenFramebuffers(1, out textDepthFbo);
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, textDepthFbo);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, (int)textDepthTex, 0);
        GL.DrawBuffer(DrawBufferMode.None); // 260319Cl depth-only FBO needs no color buffer
        GL.ReadBuffer(ReadBufferMode.None);
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0); // 260319Cl back to default framebuffer

        // The buffer of linked lists
        GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, buffers[LinkedListBuffer]);
        GL.BufferData(BufferTarget.ShaderStorageBuffer, (int)(maxNodes * nodeSize), IntPtr.Zero, BufferUsageHint.DynamicDraw);
        GL.Uniform1(GL.GetUniformLocation(Program, "MaxNodes"), maxNodes);
        // var headPtrClearBuf = Enumerable.Repeat(0xffffffff, MaxWidth * MaxHeight).ToArray();
        var headPtrClearBuf = GC.AllocateUninitializedArray<uint>(MaxWidth * MaxHeight); // (260320Ch) uint[] を直接確保して余計な変換を避ける
        Array.Fill(headPtrClearBuf, uint.MaxValue); // (260320Ch) head pointer のクリア値 0xFFFFFFFF をまとめて初期化する
        GL.GenBuffers(1, out clearBuf);
        GL.BindBuffer(BufferTarget.PixelUnpackBuffer, clearBuf);
        GL.BufferData(BufferTarget.PixelUnpackBuffer, headPtrClearBuf.Length * sizeof(uint), headPtrClearBuf, BufferUsageHint.StaticDraw);
        // GL.BindBuffer(BufferTarget.PixelUnpackBuffer, clearBuf);
        GL.BindBuffer(BufferTarget.PixelUnpackBuffer, 0); // (260319Ch) 初回 PPLL 切替前の text texture upload が PBO 経由に化けないよう unbind しておく
        GL.BindTexture(TextureTarget.Texture2D, 0); // (260319Ch) PPLL の補助 texture bind を通常描画へ持ち越さない
    }

    private void initDualDepthPeeling()
    {
        glControl.MakeCurrent();

        GL.GenFramebuffers(2, ddpFramebuffers);
        GL.GenTextures(2, ddpDepthTextures);

        for (int i = 0; i < ddpDepthTextures.Length; i++)
        {
            GL.BindTexture(TextureTarget.Texture2D, ddpDepthTextures[i]);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rg32f, MaxWidth, MaxHeight, 0, PixelFormat.Rg, PixelType.Float, IntPtr.Zero); // (260319Ch) DDP の min/max depth ping-pong
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        }

        GL.GenTextures(1, out ddpFrontBlenderTexture);
        GL.BindTexture(TextureTarget.Texture2D, ddpFrontBlenderTexture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba16f, MaxWidth, MaxHeight, 0, PixelFormat.Rgba, PixelType.Float, IntPtr.Zero); // (260319Ch) Front accumulation keeps premultiplied RGB + remaining transmittance
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

        GL.GenTextures(1, out ddpBackBlenderTexture);
        GL.BindTexture(TextureTarget.Texture2D, ddpBackBlenderTexture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba16f, MaxWidth, MaxHeight, 0, PixelFormat.Rgba, PixelType.Float, IntPtr.Zero); // (260319Ch) Back accumulation is blended over the background each peel pass
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

        for (int i = 0; i < ddpFramebuffers.Length; i++)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ddpFramebuffers[i]);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, (int)ddpDepthTextures[i], 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment1, TextureTarget.Texture2D, (int)ddpFrontBlenderTexture, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment2, TextureTarget.Texture2D, (int)ddpBackBlenderTexture, 0);
        }

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        GL.BindTexture(TextureTarget.Texture2D, 0);
        GL.GenQueries(1, out ddpQuery); // (260319Ch) 既定では使わないが、開発用に occlusion query path は残しておく
    }

    private void clearDdpDepthAttachment(int framebufferIndex)
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, ddpFramebuffers[framebufferIndex]);
        GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
        GL.ClearColor(-1f, -1f, 0f, 0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.DrawBuffers(ddpDepthDrawBuffers.Length, ddpDepthDrawBuffers);
    }

    private void clearDdpAccumulationAttachments()
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, ddpFramebuffers[0]);

        GL.DrawBuffer(DrawBufferMode.ColorAttachment1);
        GL.ClearColor(0f, 0f, 0f, 1f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        var bg = BackgroundColor;
        GL.DrawBuffer(DrawBufferMode.ColorAttachment2);
        GL.ClearColor(bg.R, bg.G, bg.B, 1f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.DrawBuffers(ddpPeelDrawBuffers.Length, ddpPeelDrawBuffers);
    }

    private void enableDdpBlendState()
    {
        GL.Enable(EnableCap.Blend);
        GL.BlendEquationSeparate(0, BlendEquationMode.Max, BlendEquationMode.Max);
        GL.BlendFuncSeparate(0, BlendingFactorSrc.One, BlendingFactorDest.One, BlendingFactorSrc.One, BlendingFactorDest.One);

        GL.BlendEquationSeparate(1, BlendEquationMode.FuncAdd, BlendEquationMode.FuncAdd);
        GL.BlendFuncSeparate(1, BlendingFactorSrc.DstAlpha, BlendingFactorDest.One, BlendingFactorSrc.Zero, BlendingFactorDest.OneMinusSrcAlpha); // (260319Ch) Front accumulation uses under blending

        GL.BlendEquationSeparate(2, BlendEquationMode.FuncAdd, BlendEquationMode.FuncAdd);
        GL.BlendFuncSeparate(2, BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha, BlendingFactorSrc.Zero, BlendingFactorDest.One); // (260319Ch) Back accumulation uses regular over blending while alpha stays opaque
    }

    private void renderFullscreenQuad(int program)
    {
        if (program < 1 || quad == null || !programLocations.TryGetValue(program, out var locations))
            return;

        GL.UseProgram(program);
        GL.UniformMatrix4(locations.ViewMatrix, false, ref m4id);
        GL.UniformMatrix4(locations.ProjMatrix, false, ref m4id);
        GL.UniformMatrix4(locations.WorldMatrix, false, ref m4id);
        quad.Generate(program, CacheKey); // (260319Ch) fullscreen quad は念のため毎回 current context に揃え直す
        quad.Render(null);
    }
    #endregion Shader Storage の初期化
    #endregion

    #region GlObjectの追加/削除
    public void MakeCurrent() => glControl?.MakeCurrent(); // 260420Cl DisablingOpenGL 時は glControl が null のため null 条件演算子でガード

    public void AddObjects(GLObject obj)
    {
        if (Program < 1) return;
        glControl.MakeCurrent();
        obj.Generate(getProgramForObject(obj), CacheKey); // (260319Ch) text は専用 program に転送しつつ context key を保持する
        glObjects.Add(obj);
    }

    public void AddObjects(IEnumerable<GLObject> objs)
    {
        if (Program < 1) return;
        glControl.MakeCurrent();
        // GLObject.Generate(Program, objs);
        // glObjects.AddRange(objs);
        var bufferedObjects = objs as IList<GLObject> ?? objs.ToList(); // (260319Ch) 遅延列挙の二重走査を避ける
        if (bufferedObjects.Count == 0)
            return;
        var meshObjects = new List<GLObject>(bufferedObjects.Count);
        List<GLObject> textObjects = null;
        foreach (var obj in bufferedObjects)
        {
            if (getProgramForObject(obj) == Program)
            {
                meshObjects.Add(obj);
            }
            else
            {
                textObjects ??= [];
                textObjects.Add(obj);
            }
        }

        if (meshObjects.Count > 0)
            GLObject.Generate(Program, meshObjects, CacheKey);
        if (textObjects != null && textObjects.Count > 0)
            GLObject.Generate(TextProgram, textObjects, CacheKey); // (260319Ch) text は ZSORT 専用 fragment shader へまとめて転送
        glObjects.AddRange(bufferedObjects);
    }
    public void Replace(int i, GLObject obj)
    {
        if (Program < 1 || glObjects.Count == 0) return;
        glControl.MakeCurrent();

        if (i < glObjects.Count)
        {
            obj.Generate(getProgramForObject(obj), CacheKey);
            glObjects[i] = obj;
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
        if (Program < 1 || glObjects.Count == 0) return;
        
        glControl.MakeCurrent();
        var disposedVaos = new HashSet<int>(); // (260319Ch) PLINQ + Distinct の代わりに単純な重複除去で十分
        foreach (var obj in CollectionsMarshal.AsSpan(glObjects))
        {
            if (disposedVaos.Add(obj.Obj.VAO))
                obj.Dispose();
        }
        TextObject.ClearContextCache(CacheKey); // (260319Ch) transparency mode 切替時に text texture cache を必ず作り直す
        glObjects.Clear();
    }


    #endregion

    #region クリップ操作

    /// <summary>Clipをセットする。nullをセットした場合はクリップが無効化される</summary>
    /// <param name="clip"></param>
    public void SetClip(Clip clip)
    {
        if (Program < 1 || glControl == null) return; // 260420Cl 追加 DisablingOpenGL 時の二次的 NullReferenceException を回避
        glControl.MakeCurrent();
        Clip = clip;
        clip?.Generate(Program);
        if (TextProgram > 0)
            clip?.Generate(TextProgram, false); // (260319Ch) text も clip distance を使えるよう uniform location を登録する
    }

    #endregion

    #region レンダリング
    public bool SkipRendering = false;

    /// <summary>レンダリング</summary>
    public void Render()
    {
        if (InvokeRequired)//別スレッドから呼び出されたとき Invokeして呼びなおす
        {
            // Invoke(new Action(() => Render()), null);
            Invoke(renderAction); // (260319Ch) 毎回ラムダを作らず既存 delegate を再利用
            return;
        }

        if (SkipRendering || Program < 1)
            return;

        glControl.MakeCurrent();

        if (glObjects.Count == 0 || !hasRenderableObject())
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(BackgroundColor);
            glControl.SwapBuffers();//swap
            return;
        }

        GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
        applyFrameUniforms(Program);
        if (TextProgram > 0)
            applyFrameUniforms(TextProgram);

        if (FragShader == FragShaders.PPLL)//PPLLモードの時
        {
            // GLObject.CurrentFragmentRenderPassIndex = passNormalIndex; // (260319Ch) PPLL text を linked-list に戻した案
            // GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passNormalIndex);
            // GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            // renderObjects(); // (260319Ch) 旧案: text も PPLL の linked-list に積んで z-order を保つ
            // (260319Ch) 現案: text は linked-list 生成後に各 glyph pixel だけ scene を再合成して置き換える
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.ColorMask(true, true, true, true);
            GL.Disable(EnableCap.DepthTest);
            GL.DepthMask(false);

            //clearBuffers();
            uint zero = 0;
            GL.BindBufferBase(BufferRangeTarget.AtomicCounterBuffer, 0, buffers[CounterBuffer]);
            GL.BufferSubData(BufferTarget.AtomicCounterBuffer, (IntPtr)0, sizeof(uint), (IntPtr)(&zero));
            GL.BindBuffer(BufferTarget.PixelUnpackBuffer, clearBuf);
            GL.BindTexture(TextureTarget.Texture2D, headPtrTex);
            var headPointerWidth = Math.Min(glControl.ClientSize.Width, MaxWidth);
            var headPointerHeight = Math.Min(glControl.ClientSize.Height, MaxHeight);
            // GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, MaxWidth, MaxHeight, PixelFormat.RedInteger, PixelType.UnsignedInt, IntPtr.Zero);
            if (headPointerWidth > 0 && headPointerHeight > 0)
                GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, headPointerWidth, headPointerHeight, PixelFormat.RedInteger, PixelType.UnsignedInt, IntPtr.Zero); // (260319Ch) Clear only the active viewport region of the head-pointer texture

            //pass1();
            GLObject.CurrentFragmentRenderPassIndex = passPpll1Index; // (260319Ch) scene draw ごとに passPPLL1 を再設定できるよう保持
            GLObject.CurrentDepthTestEnabled = false; // (260319Ch) linked-list への書き込み本体は depth test 無効
            GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passPpll1Index);
            GL.Disable(EnableCap.DepthTest);
            GL.DepthMask(false);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            renderObjects(includeTextObjects: false, includeNonTextObjects: true); // (260319Ch) text は linked-list から外し、後段 overlay で描く

            //pass2();
            // GL.MemoryBarrier(MemoryBarrierFlags.ShaderStorageBarrierBit);
            GL.MemoryBarrier(
                MemoryBarrierFlags.ShaderStorageBarrierBit |
                MemoryBarrierFlags.ShaderImageAccessBarrierBit |
                MemoryBarrierFlags.AtomicCounterBarrierBit); // (260319Ch) PPLL pass2 で SSBO / image / atomic counter の更新を確実に可視化する
            GLObject.CurrentFragmentRenderPassIndex = passPpll2Index; // (260319Ch) fullscreen quad でも pass2 を維持する
            GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passPpll2Index);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            var locations = programLocations[Program];
            GL.UniformMatrix4(locations.ViewMatrix, false, ref m4id);
            GL.UniformMatrix4(locations.ProjMatrix, false, ref m4id);
            GL.UniformMatrix4(locations.WorldMatrix, false, ref m4id);
            quad?.Generate(Program, CacheKey);//理由はよく分からんが、Generateしておかないと、うまく描画できないことが多い
            quad?.Render(null);// Draw a screen filler

            if (hasRenderableTextObject())
            {
                // applyFrameUniforms(TextProgram); // (260319Ch) 旧案: text は depth prepass 後に専用 shader で重ねる
                applyFrameUniforms(Program); // (260319Ch) main PPLL program で glyph pixel ごとに linked-list を再合成する
                GLObject.CurrentFragmentRenderPassIndex = passPpllTextIndex;
                GLObject.CurrentDepthTestEnabled = false;
                GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passPpllTextIndex);
                GL.Disable(EnableCap.StencilTest);
                GL.Disable(EnableCap.CullFace);
                GL.Disable(EnableCap.DepthTest);
                GL.DepthMask(false);
                GL.ColorMask(true, true, true, true);
                GL.BindBuffer(BufferTarget.PixelUnpackBuffer, 0);
                GL.Disable(EnableCap.Blend); // (260319Ch) shader が最終色を直接再計算するので framebuffer blend は不要
                renderObjects(includeTextObjects: true, includeNonTextObjects: false); // (260319Ch) 半透明前景を含む scene + label をそのピクセルだけ再構成する
            }

            GLObject.CurrentFragmentRenderPassIndex = -1; // (260319Ch) PPLL draw 完了後に共有 state を戻す
        }
        else if (FragShader == FragShaders.DDP)
        {
            var renderTextOverlay = hasRenderableTextObject();

            if (renderTextOverlay)
            {
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                GLObject.CurrentFragmentRenderPassIndex = passDdpInitIndex;
                GLObject.CurrentDepthTestEnabled = true; // (260319Ch) DDP 後の text overlay 用に通常の depth prepass を別で持つ
                GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passDdpInitIndex);
                GL.DepthMask(true);
                GL.Clear(ClearBufferMask.DepthBufferBit);
                GL.ColorMask(false, false, false, false);
                GL.Enable(EnableCap.DepthTest);
                renderObjectsForPpllTextDepthPrepass(); // (260319Ch) DDP でも label 遮蔽は mesh depth prepass を使う
                GL.ColorMask(true, true, true, true);
            }

            clearDdpAccumulationAttachments();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ddpFramebuffers[0]);
            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
            clearDdpDepthAttachment(0);
            GL.DrawBuffers(ddpDepthDrawBuffers.Length, ddpDepthDrawBuffers);
            GLObject.CurrentFragmentRenderPassIndex = passDdpInitIndex;
            GLObject.CurrentDepthTestEnabled = false; // (260319Ch) DDP の depth interval 更新は custom MAX blend だけで行う
            GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passDdpInitIndex);
            GL.Disable(EnableCap.DepthTest);
            GL.DepthMask(false);
            enableDdpBlendState(); // (260319Ch) 初期 pass も MAX blend で outer depth interval を作る
            renderObjects(includeTextObjects: false, includeNonTextObjects: true);

            int readIndex = 0;
            int writeIndex = 1;
            for (int peelPass = 0; peelPass < DualDepthPeelingPasses; peelPass++)
            {
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, ddpFramebuffers[writeIndex]);
                GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
                clearDdpDepthAttachment(writeIndex);
                GL.DrawBuffers(ddpPeelDrawBuffers.Length, ddpPeelDrawBuffers);
                bindDdpTextures(ddpDepthTextures[readIndex], ddpFrontBlenderTexture, ddpBackBlenderTexture);

                GLObject.CurrentFragmentRenderPassIndex = passDdpPeelIndex;
                GLObject.CurrentDepthTestEnabled = false;
                GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passDdpPeelIndex);
                GL.Disable(EnableCap.DepthTest);
                GL.DepthMask(false);
                enableDdpBlendState();

                // GL.BeginQuery(QueryTarget.AnySamplesPassed, ddpQuery);
                if (DualDepthPeelingUseOcclusionQuery)
                    GL.BeginQuery(QueryTarget.AnySamplesPassed, ddpQuery); // (260319Ch) 開発用: 空 pass で早期終了したい場合だけ query を使う
                renderObjects(includeTextObjects: false, includeNonTextObjects: true);
                // GL.EndQuery(QueryTarget.AnySamplesPassed);
                if (DualDepthPeelingUseOcclusionQuery)
                    GL.EndQuery(QueryTarget.AnySamplesPassed);

                // GL.GetQueryObject(ddpQuery, GetQueryObjectParam.QueryResult, out int passHasSamples);
                if (DualDepthPeelingUseOcclusionQuery)
                {
                    GL.GetQueryObject(ddpQuery, GetQueryObjectParam.QueryResult, out int passHasSamples);
                    if (passHasSamples == 0)
                        break; // (260319Ch) これ以上 peel する層が無いので終了
                }

                (readIndex, writeIndex) = (writeIndex, readIndex);
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
            GL.ClearColor(BackgroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            bindDdpTextures(0, ddpFrontBlenderTexture, ddpBackBlenderTexture);
            GLObject.CurrentFragmentRenderPassIndex = passDdpCompositeIndex;
            GLObject.CurrentDepthTestEnabled = false;
            GL.UniformSubroutines(ShaderType.FragmentShader, 1, ref passDdpCompositeIndex);
            GL.Disable(EnableCap.DepthTest);
            GL.DepthMask(false);
            GL.Disable(EnableCap.Blend);
            renderFullscreenQuad(Program);

            if (renderTextOverlay)
            {
                applyFrameUniforms(TextProgram);
                GL.Disable(EnableCap.StencilTest);
                GL.Disable(EnableCap.CullFace);
                GL.Enable(EnableCap.DepthTest);
                GL.DepthFunc(DepthFunction.Less);
                GL.DepthMask(false);
                GL.ColorMask(true, true, true, true);
                GL.Enable(EnableCap.Blend);
                GL.BlendEquation(BlendEquationMode.FuncAdd);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                GLObject.CurrentDepthTestEnabled = true;
                renderObjects(includeTextObjects: true, includeNonTextObjects: false); // (260319Ch) DDP の text も prepass depth と比較して overlay する
                GL.Disable(EnableCap.Blend);
                GL.DepthFunc(DepthFunction.Less);
            }

            GLObject.CurrentFragmentRenderPassIndex = -1;
        }
        else//Zsortモードの時
        {
            GLObject.CurrentFragmentRenderPassIndex = -1; // (260319Ch) ZSORT 側では PPLL pass index を使わない
            GLObject.CurrentDepthTestEnabled = true; // (260319Ch) ZSORT は通常の depth test + alpha blend
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(BackgroundColor);

            //描画対称に透明なものが一つでもあるとき
            if (requiresTransparencySorting())
                updateTransparentOrder();
            renderObjects();// draw scene
        }
        // glControl.SwapBuffers(); // (260319Ch) 旧案: final image をそのまま back buffer から表示する
        if (shouldApplyPostAntiAliasing())
            resolvePostAntiAliasing(); // (260319Ch) PPLL/DDP は最終画像へ 1 回だけ FXAA を掛けてジャギーを和らげる
        glControl.SwapBuffers();
        // GL.Finish();
        // (260319Ch) 毎フレームの強制 GPU 完了待ちは CPU/GPU を直列化してしまうので通常描画では行わない
        
        Paint?.Invoke(this, new PaintEventArgs(glControlGraphics, glControl.ClientRectangle));
    }

    private bool hasRenderableObject()
    {
        foreach (var obj in CollectionsMarshal.AsSpan(glObjects))
        {
            if (obj.Rendered)
                return true;
        }
        return false;
    }

    private bool requiresTransparencySorting()
    {
        foreach (var obj in CollectionsMarshal.AsSpan(glObjects))
        {
            if (obj.Rendered && (obj.Material.Color.A != 1 || obj is TextObject))
                return true;
        }
        return false;
    }

    private bool hasRenderableTextObject()
    {
        foreach (var obj in CollectionsMarshal.AsSpan(glObjects))
        {
            if (obj.Rendered && obj is TextObject)
                return true;
        }
        return false;
    }

    private void renderObjectsForPpllTextDepthPrepass()
    {
        foreach (var obj in CollectionsMarshal.AsSpan(glObjects))
        {
            if (!obj.Rendered || obj is TextObject)
                continue;

            // if (obj.Material.Color.A >= 0.999f)
            if (obj is Sphere || obj.Material.Color.A >= 0.999f) // (260319Ch) ラベル遮蔽用 prepass では前面の原子球を透明でも必ず深度へ入れる
                obj.Render(Clip); // (260319Ch) 半透明 sphere が前面にあるときも背面ラベルを隠せるようにする
        }
    }

    private void updateTransparentOrder()
    {
        var rot = worldMatrix.Inverted(); // (260319Ch) UI スレッド上の単純ループに戻し、PLINQ の分割/同期コストを避ける
        foreach (var obj in CollectionsMarshal.AsSpan(glObjects))
        {
            if (obj.Rendered && (obj.Material.Color.A != 1 || obj is TextObject))
            {
                obj.Z = (rot * obj.CircumscribedSphereCenter).Z;
                if (obj is TextObject t)
                    obj.Z += t.Popout;
            }
            else
            {
                obj.Z = double.NegativeInfinity;
            }
        }
        glObjects.Sort((o1, o2) => o1.Z.CompareTo(o2.Z));
    }

    private void renderObjects()
    {
        foreach (var obj in CollectionsMarshal.AsSpan(glObjects))
            obj.Render(Clip);
    }

    private void renderObjects(bool includeTextObjects, bool includeNonTextObjects)
    {
        foreach (var obj in CollectionsMarshal.AsSpan(glObjects))
        {
            if (obj is TextObject)
            {
                if (includeTextObjects)
                    obj.Render(Clip);
            }
            else if (includeNonTextObjects)
            {
                obj.Render(Clip);
            }
        }
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
                if (RotationMode == RotationModes.Object)
                    WorldMatrix *= Mat4d.CreateRotationX((float)(-dy / 100)) * Mat4d.CreateRotationY((float)(dx / 100));
                else if (RotationMode == RotationModes.View)
                    ViewMatrix *= Mat4d.CreateRotationX((float)(-dy / 100)) * Mat4d.CreateRotationY((float)(dx / 100));
            }
            else
            {
                var lastx = lastMousePosition.X - glControl.ClientSize.Width / 2.0;
                var lasty = lastMousePosition.Y - glControl.ClientSize.Height / 2.0;
                var angle = Math.Atan2(x, y) - Math.Atan2(lastx, lasty);
                if (RotationMode == RotationModes.Object)
                    WorldMatrix *= Mat4d.CreateRotationZ((float)angle);
                else if (RotationMode == RotationModes.View)
                    ViewMatrix *= Mat4d.CreateRotationZ((float)angle);
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
        if (Program < 1 || glControl == null) return null; // 260420Cl 追加 DisablingOpenGL 時は null を返す (呼び出し側は null チェックするか操作失敗として扱う)
        glControl.MakeCurrent();
        var bmp = new Bitmap(glControl.ClientSize.Width, glControl.Height);
        var data = bmp.LockBits(Rectangle.FromLTRB(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        GL.ReadPixels(0, 0, bmp.Width, bmp.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
        bmp.UnlockBits(data);
        bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
        return bmp;
    }

    #endregion

    #region Depth Cueingの設定
    private void setDepthCueing()
    {
        glControl.MakeCurrent();
        applyDepthCueing(Program);
        if (TextProgram > 0)
            applyDepthCueing(TextProgram);
        // GL.Finish();
        // (260319Ch) uniform 更新直後に Render() するだけなので、ここで同期完了待ちを入れる必要はない
        Render();
    }
    #endregion

}

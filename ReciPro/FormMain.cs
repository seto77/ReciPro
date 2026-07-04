#region using
using Crystallography.OpenGL;
using IronPython.Hosting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;//260415Cl 追加: Registry() 内の rw() ローカル関数のため
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Col4 = OpenTK.Mathematics.Color4;
using Vec3 = OpenTK.Mathematics.Vector3d;
#endregion

namespace ReciPro;

public partial class FormMain : FormBase
{
    #region LibraryImport
    [DllImport("user32")]
    private static extern short GetAsyncKeyState(int nVirtKey);
    #endregion

    #region WebClientの派生クラス
    //private static readonly HttpClient httpClient = new();

    //private async Task DownloadAsync(string url, string filename)
    //{
    //    using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
    //    using (var response = await httpClient.SendAsync(request))
    //    {
    //        if (response.StatusCode == HttpStatusCode.OK)
    //        {
    //            using (var content = response.Content)
    //            using (var stream = await content.ReadAsStreamAsync())
    //            using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
    //            {
    //                stream.CopyTo(fileStream);
    //            }
    //        }
    //    }
    //}
    #endregion

    #region クリップボード監視

    private IntPtr NextHandle;
    private const int WM_DRAWCLIPBOARD = 0x0308;
    private const int WM_CHANGECBCHAIN = 0x030D;

    [DllImport("user32")]
    private static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

    [DllImport("user32")]
    private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private extern static int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    protected override void WndProc(ref System.Windows.Forms.Message msg)
    {
        switch (msg.Msg)
        {
            case WM_DRAWCLIPBOARD:
                if (Clipboard.GetDataObject().GetDataPresent(typeof(byte[])))
                {
                    var bytes = (byte[])Clipboard.GetDataObject().GetData(typeof(byte[]));
                    if (bytes[0] == Crystal2.ID)
                    {
                        var c2 = Crystal2.Deserialize(bytes[1..]);
                        crystalControl.Crystal = Crystal2.GetCrystal(c2);
                    }
                }

                if ((int)NextHandle != 0)
                    SendMessage(NextHandle, msg.Msg, msg.WParam, msg.LParam);
                break;

            case WM_CHANGECBCHAIN:
                if (msg.WParam == NextHandle)
                    NextHandle = msg.LParam;
                else if ((int)NextHandle != 0)
                    SendMessage(NextHandle, msg.Msg, msg.WParam, msg.LParam);
                break;
        }
        base.WndProc(ref msg);
    }

    #endregion クリップボード監視

    #region プロパティ、フィールド、イベントハンドラ

    /// <summary>VisualStudioデザイナーの編集の時はTrue</summary>
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

    public FormStructureViewer FormStructureViewer;
    public FormDiffractionSimulator FormDiffractionSimulator;
    public FormStereonet FormStereonet;
    public FormSpotIDv1 FormSpotIDv1;
    public FormSpotIDV2 FormSpotIDv2;
    public FormCalculator FormCalculator;
    public FormPolycrystallineDiffractionSimulator FormPolycrystallineDiffractionSimulator;
    public FormRotationMatrix FormRotation;
    public FormImageSimulator FormImageSimulator;
    public FormCrystalDatabase FormCrystalDatabase;
    public FormMovie FormMovie;
    public FormTrajectory FormTrajectory;
    private Macro macro;
    public FormMacro FormMacro;
    public FormEBSD FormEBSD;

    private Crystallography.Controls.CommonDialog commonDialog;
    private GLControlAlpha glControlAxes;
    public bool DisableOpenGL
    {
        get => disableOpneGLToolStripMenuItem != null && disableOpneGLToolStripMenuItem.Checked;
        set => disableOpneGLToolStripMenuItem.Checked = value;
    }
    public bool DisableTextRendering
    {
        get => toolStripMenuItemDisableTextRendering != null && toolStripMenuItemDisableTextRendering.Checked;
        set => toolStripMenuItemDisableTextRendering.Checked = value;
    }

    public bool DisableNative
    {
        get => toolStripMenuItemDisableNative != null && toolStripMenuItemDisableNative.Checked;
        set => toolStripMenuItemDisableNative.Checked = value;
    }
    //260405Cl 追加
    public bool UseMKL
    {
        get => toolStripMenuItemUseMKL != null && toolStripMenuItemUseMKL.Checked;
        set => toolStripMenuItemUseMKL.Checked = value;
    }

    // 260428Cl 追加: ダークモード切替。Application.SetColorMode は Application.Run より前に呼ぶ必要があるため、
    // 永続化は Program.ReadDarkMode/WriteDarkMode (DWORD レジストリ) 側で行う。Reg.RW 経路は使わない。
    public bool DarkMode
    {
        get => toolStripMenuItemDarkMode != null && toolStripMenuItemDarkMode.Checked;
        set => toolStripMenuItemDarkMode.Checked = value;
    }
    
    // 4 指数 (Miller-Bravais) 表記を有効にするかどうか。レジストリに保存。
    // 実際の表示切替は MillerBravaisActive で「UseMillerBravais && 晶系が4指数可」を判定してから行う。 // (260426Ch) 名称 typo 修正
    public bool UseMillerBravais
    {
        get => toolStripMenuItemUseMillerBravais != null && toolStripMenuItemUseMillerBravais.Checked;
        set => toolStripMenuItemUseMillerBravais.Checked = value;
    }

    // 260613Cl 追加: 動力学計算でイオン散乱因子 (Peng IonFull) を使うか。OFF(既定)=中性原子 IAM。レジストリに保存。
    // 切替は BetheMethod.ElasticIonModel (static, 全結晶共有) を Neutral⇔IonFull に変える。詳細は ElasticIonModel enum。
    public bool UseIonicScattering
    {
        get => toolStripMenuItemIonicScattering != null && toolStripMenuItemIonicScattering.Checked;
        set => toolStripMenuItemIonicScattering.Checked = value;
    }

    /// <summary>UseMillerBravais が有効かつ現在結晶が 4 指数対応の場合のみ true</summary>
    // 260607Cl: 起動時の Registry(Read) で UseMillerBravais が復元されると結晶ロード前に
    // CheckedChanged → UpdatePlaneIndices が走り、Crystal が null で NRE になっていた。null ガードを追加。
    public bool MillerBravaisActive => UseMillerBravais && Crystal != null && Crystal.MillerBravaisCapable;

    //260421Cl 追加: 面 (hkl) を 3 指数 / 4 指数どちらで表示するかを useMB で切替えるヘルパー。
    public static string PlaneString(int h, int k, int l, bool useMB, string sep = " ")
        => useMB ? $"{h}{sep}{k}{sep}{-(h + k)}{sep}{l}" : $"{h}{sep}{k}{sep}{l}";
    //260618Cl 変更: 二値 (非en→Japanese) から「ja のみ Japanese・他は English fallback」へ (多言語化 §11.4 step5)。
    //  旧式だと新言語 (de/fr/…) で Macro サンプル等が日本語側へ落ちてしまうため、英語をフォールバックにする。
    //旧: public static Languages Language => Thread.CurrentThread.CurrentUICulture.Name == "en" ? Languages.English : Languages.Japanese;
    public static Languages Language => Thread.CurrentThread.CurrentUICulture.Name == "ja" ? Languages.Japanese : Languages.English;
    // 260519Cl 変更: NumericUpDown → NumericBox に伴い RadianValue で簡略化
    //public double Phi { get => (double)numericUpDownEulerPhi.Value / 180.0 * Math.PI; set => numericUpDownEulerPhi.Value = (decimal)(value / Math.PI * 180.0); }
    //public double Theta { get => (double)numericUpDownEulerTheta.Value / 180.0 * Math.PI; set => numericUpDownEulerTheta.Value = (decimal)(value / Math.PI * 180.0); }
    //public double Psi { get => (double)numericUpDownEulerPsi.Value / 180.0 * Math.PI; set => numericUpDownEulerPsi.Value = (decimal)(value / Math.PI * 180.0); }
    public double Phi { get => numericBoxEulerPhi.RadianValue; set => numericBoxEulerPhi.RadianValue = value; }
    public double Theta { get => numericBoxEulerTheta.RadianValue; set => numericBoxEulerTheta.RadianValue = value; }
    public double Psi { get => numericBoxEulerPsi.RadianValue; set => numericBoxEulerPsi.RadianValue = value; }

    public static string UserAppDataPath => new DirectoryInfo(Application.UserAppDataPath).Parent.FullName + @"\";

    public Crystal Crystal { get => crystalControl.Crystal; set => crystalControl.Crystal = Crystal; }

    public Crystal[] Crystals
    {
        get
        {
            if (listBox.SelectedItems.Count == 1)
                return [Crystal];
            else
            {
                var crystals = listBox.SelectedItems.Cast<Crystal>().ToArray();
                for (int i = 0; i < crystals.Length; i++)
                    if (crystals[i] == (Crystal)listBox.SelectedItem)
                        crystals[i] = Crystal;
                return crystals;
            }
        }
    }
    public bool SkipProgressEvent { get; set; } = false;
    private readonly IProgress<(long, long, long, string)> ip;//IReport
    public bool YusaGonioMode { get; set; }

    private readonly Stopwatch sw = new();
    public bool SkipDrawing { get; set; } = false;

    public string CurrentZoneAxis { get; set; } = "";

    public int SelectedCrystalIndex { get => listBox.SelectedIndex; set => listBox.SelectedIndex = value; }
    // 260415Cl 追加 結晶リストの全件数 (listBox.Items.Count の公開ラッパー)。マクロ API の CrystalList.Count から使用。
    public int CrystalCount => listBox.Items.Count;

    #endregion

    #region コンストラクト、ロード

    /// <summary>コンストラクタ</summary>
    public FormMain()
    {

        // var endmembers = new[] { "A5X2", "A3Y4", "B5X2", "B3Y4" };
        // var space = new CompositionSpace(endmembers);

        // var groups = new[]
        // {
        //     new SiteGroup("Cation", new[]{"A","B"}),
        //     new SiteGroup("Anion",  new[]{"X","Y"})
        // };

        //var model = new SiteBasedParameterization(space, groups, maxExtentSubsetSize: 1);

        //Console.WriteLine($"AffineDim={space.AffineDim}, ParamDim={model.ParameterDim}");
        //Console.WriteLine("Symbolic formula:");
        //Console.WriteLine(model.BuildSymbolicFormula());

        // 任意混合の組成
        //var w = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[] { 0.2, 0.1, 0.3, 0.4 });
        //var c = space.ComposeFromWeights(w);

        //var p = model.ToParameters(c);
        //Console.WriteLine("Parameters:");
        //foreach (var kv in p) Console.WriteLine($"  {kv.Key} = {kv.Value}");

        //var c2 = model.FromParameters(p);
        //Console.WriteLine($"Reconstruction L2 error = {(c - c2).L2Norm()}");




        //var test = SymmetryStatic.IsRootAxis((-1, 3, 5), new Symmetry(460), out var indices);

        //カルチャーを決めるため、レジストリ読込 (InitializeComponentの前に読み込む)
        if (!DesignMode)
            Registry(Reg.Mode.Read);

        //260522Cl 追加: --capture で言語を強制指定された場合はレジストリ値より優先する (スクショ一括取得用)
        if (GuiCapture.ForcedUICulture != null)
            Thread.CurrentThread.CurrentUICulture = GuiCapture.ForcedUICulture;

        InitializeComponent();
        HelpPage = "0-main-window"; //260529Cl 追加

        //260529Cl 追加: F1 オンラインヘルプの URL 解決ロジックを登録 (起動時に 1 回)。
        //Controls 側フォームは ReciPro 固有の URL を知らないため、ここで一括して組み立てる。
        //260622Cl 変更: F1 ヘルプ URL を SupportedCultures.HelpCulture 駆動に (旧: ja/en 二値判定)。
        //  Pages マニュアルは mkdocs-static-i18n で英語(en)をサイトルート (/ReciPro/) へ、他言語を /ReciPro/<lang>/ へ出す。
        //  HelpCulture の値そのものが「そのマニュアルが整備済みか」を表す (未整備言語は "en" のまま英語マニュアルへ)。
        FormBase.HelpUrlResolver = f =>
            string.IsNullOrEmpty(f.HelpPage) ? HelpBaseUrl() : $"{HelpBaseUrl()}{f.HelpPage}/";
        //260529Cl 旧コード (コメントアウト保存):
        //FormBase.HelpUrlResolver = f =>
        //{
        //    var lang = Thread.CurrentThread.CurrentUICulture.Name == "ja" ? "ja" : "en";
        //    return string.IsNullOrEmpty(f.HelpPage)
        //        ? (lang == "ja" ? "https://seto77.github.io/ReciPro/ja/" : "https://seto77.github.io/ReciPro/")
        //        : $"https://seto77.github.io/ReciPro/{lang}/{f.HelpPage}/";
        //};

        //260529Cl 追加: Crystallography.Controls 側 (CrystalControl 内) で生成される子フォームの
        //HelpPage は ReciPro からインスタンスに触れるここで設定する。
        crystalControl.FormSymmetryInformation.HelpPage = "2-symmetry-information";
        // 260623Cl: HelpPage スラッグを実ページ名へ修正。"3-scattering-factor" は存在せず (実ページは 3-beam-interaction)、
        //   ja のみ redirect_maps、en も /en/ stub だけで HelpBaseUrl() のルート URL は 404 だった。HelpCulture flip で
        //   9 言語に 404 が波及するため根本修正 (redirect 増設より実スラッグに合わせる)。
        //crystalControl.FormBeamInteraction.HelpPage = "3-scattering-factor"; // 旧 (260529Cl): 実ページと不一致
        crystalControl.FormBeamInteraction.HelpPage = "3-beam-interaction";

        //260413Cl DPI スケーリング補正 (ListBox.ColumnWidth は自動スケール対象外)
        listBox.ColumnWidth = (int)(listBox.ColumnWidth * DeviceDpi / 96.0);

        toolStripMenuItemDisableNative.Enabled = NativeWrapper.Enabled;
        if (!NativeWrapper.Enabled)
        {
            toolStripMenuItemDisableNative.Checked = true;
            if (!string.IsNullOrWhiteSpace(NativeWrapper.LastLoadError))
                toolStripMenuItemDisableNative.ToolTipText = NativeWrapper.LastLoadError;
        }

        //260611Cl 追加: ARM64 には MKL ネイティブが存在しない (x86/x64 専用、ダウンロード先 nupkg も x64 固定) ため
        //Use MKL メニューを非表示にする。レジストリに残った UseMKL=true (同一マシンの x64 エミュ実行が保存し得る) は
        //起動時の既存フォールバック (DLL 不在 → チェック解除) が無害化する
        if (RuntimeInformation.ProcessArchitecture == Architecture.Arm64)
            toolStripMenuItemUseMKL.Visible = false;


        if (DesignMode)
            return;

        //MainWindowの場所を読み込むため (InitializeComponentの後に読み込む)
        //260405Cl 起動時はUseMKLのCheckedChangedを抑制 (ip未初期化、ダウンロードダイアログ抑制のため)
        toolStripMenuItemUseMKL.CheckedChanged -= toolStripMenuItemUseMKL_CheckedChanged;
        Registry(Reg.Mode.Read); //260524Cl: 強制カルチャ (--capture) の再適用は Registry() 内に集約済み (全 Read 呼び出しを一括カバー)
        toolStripMenuItemUseMKL.CheckedChanged += toolStripMenuItemUseMKL_CheckedChanged;

        //260405Cl 起動時: UseMKLがチェックされていてDLLが存在する場合のみMKLを有効化
        if (toolStripMenuItemUseMKL.Checked && MklFilesExist())
        {
            if (MathNet.Numerics.Control.TryUseNativeMKL())
                BetheMethod.MklEnabled = true;
            else
                toolStripMenuItemUseMKL.Checked = false; //ロード失敗時はチェックを外す
        }
        else if (toolStripMenuItemUseMKL.Checked && !MklFilesExist())
        {
            toolStripMenuItemUseMKL.Checked = false; //DLLがない場合はチェックを外す
        }

        sw.Restart();

        ip = new Progress<(long, long, long, string)>(reportProgress);//IReport

        //this.SetStyle(ControlStyles.ResizeRedraw, true); //20250804にコメントアウト (浜根氏の問題に対処)
        // ダブルバッファリング
        //this.SetStyle(ControlStyles.DoubleBuffer, true);//20250804にコメントアウト (浜根氏の問題に対処)
        //this.SetStyle(ControlStyles.UserPaint, true);//20250804にコメントアウト (浜根氏の問題に対処)
        //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//20250804にコメントアウト (浜根氏の問題に対処)

    }

    /// <summary>フォームロード時</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FormMain_Load(object sender, EventArgs e)
    {
        if (DesignMode) return;

        //260602Cl 追加: Portable ZIP 版は実行フォルダに README-PORTABLE.txt を同梱する (MSI 版には無い)。
        //         この場合 MSI ベースの自動更新は成り立たないため「Check Updates」メニューを隠す。
        if (File.Exists(Path.Combine(AppContext.BaseDirectory, "README-PORTABLE.txt")))
            checkUpdatesToolStripMenuItem.Visible = false;

        Crystallography.Controls.FormCaptureGUI.InstallShortcutFilter(); // 260323Cl 追加: Ctrl+Shift+Alt+C ショートカット

        // 260617Cl 変更 / 260618Cl: 二値 ja/en 判定から SupportedCultures 駆動へ。言語メニュー項目を
        //   Designer 固定でなく PopulateLanguageMenu で動的生成し、その後に現在カルチャのチェックを付ける
        //   (多言語化 §11.4 step6: 言語を増やすときは SupportedCultures.cs で Released=true にするだけ)。
        // 旧: englishToolStripMenuItem.Checked = ...Name != "ja"; japaneseToolStripMenuItem.Checked = ...Name == "ja";
        PopulateLanguageMenu(); // 260618Cl 追加
        UpdateLanguageMenuChecks(Crystallography.SupportedCultures.Current.Name);

        // 260428Cl 追加: ダークモード設定の復元 (CheckedChanged で書込が走らないようハンドラを一旦外す)。
        toolStripMenuItemDarkMode.CheckedChanged -= toolStripMenuItemDarkMode_CheckedChanged;
        toolStripMenuItemDarkMode.Checked = Program.ReadDarkMode();
        toolStripMenuItemDarkMode.CheckedChanged += toolStripMenuItemDarkMode_CheckedChanged;

        commonDialog = new Crystallography.Controls.CommonDialog
        {
            Owner = this,
            DialogMode = Crystallography.Controls.CommonDialog.DialogModeEnum.Initialize,
            Software = Version.Software,
            VersionAndDate = Version.VersionAndDate,
            Author = Version.Author,
            History = Version.History,
            Hint = Version.Hint,
            // 260519Cl 変更: 高 DPI で 600px が物理ピクセル扱いとなり日本語版で特に狭く見えていたため LogicalToDeviceUnits で論理→物理変換
            Width = LogicalToDeviceUnits(600),
            Location = new Point(this.Location.X, this.Location.Y)
        };

        commonDialog.Show();

        commonDialog.Progress = ("Now Loading...Initializing OpenGL.", 0.1);

        //OpenGLがDisableかどうかを決めるためレジストリ読込
        Registry(Reg.Mode.Read);
        if ((ModifierKeys & Keys.Control) == Keys.Control)//Controlキーが押されていた場合は強制的にOpenGLをDisableに。
        {
            DisableOpenGL = true;
            Registry(Reg.Mode.Write);
        }
        // 260612Cl 追加: CI の GUI 起動 smoke (attach-arm64-experimental.yml) 用に、環境変数でも 3D を無効化
        // できるようにする。GPU 無しの hosted runner では GL コンテキスト作成が WndProc 内で失敗して
        // ThreadExceptionDialog ("Microsoft .NET") がモーダル表示され、起動がブロックするため (run 27383602242)。
        // 変数名は GLControlAlpha の静的初期化スキップ (本丸のガード、同名変数を参照) と共通。
        // Ctrl キー強制と違い一時的な診断用なのでレジストリには保存しない
        if (Environment.GetEnvironmentVariable("CRYSTALLOGRAPHY_DISABLE_OPENGL") == "1")
            DisableOpenGL = true;
        GLControlAlpha.DisableTextRendering = DisableTextRendering;

        #region ここでglControlコントロールを追加. Mac環境の対応のため。
        if (!disableOpneGLToolStripMenuItem.Checked)
        {
            try
            {
                glControlAxes = new GLControlAlpha
                {
                    AllowMouseRotation = false,
                    AllowMouseScaling = false,
                    AllowMouseTranslating = false,
                    Name = "glControlAxes",
                    ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
                    ProjWidth = 2.7,
                    ProjCenter = new OpenTK.Mathematics.Vector2d(0, 0.2),
                    RotationMode = GLControlAlpha.RotationModes.Object,
                    LightPosition = new Vec3(100, 100, 100),
                };
                glControlAxes.MouseDown += panelAxes_MouseDown;
                glControlAxes.MouseMove += panelAxes_MouseMove;
                groupBoxCurrentDirection.Controls.Add(glControlAxes);
                //glControlAxes.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during initializing GLControl");
                MessageBox.Show(ex.Message);
                disableOpneGLToolStripMenuItem.Checked = true;
            }
        }

        if (glControlAxes != null)
        {
            labelCurrentIndex.Dock = DockStyle.None;

            glControlAxes.Dock = DockStyle.Top;
            glControlAxes.Height = glControlAxes.Width + 20;
            glControlAxes.SendToBack();

            labelCurrentIndex.BackColor = Color.White;
            labelCurrentIndex.BringToFront();
        }
        #endregion

        commonDialog.Progress = ("Now Loading...Initializing 'Macro' form.", 0.12);
        macro = new Macro(this);
        FormMacro = new FormMacro(Python.CreateEngine(), macro) { Visible = false };

        commonDialog.Progress = ("Now Loading...Initializing 'Rotation' form.", 0.14);
        FormRotation = new FormRotationMatrix { FormMain = this, Visible = false };

        commonDialog.Progress = ("Now Loading...Initializing 'Structure Viewer' form.", 0.18);
        FormStructureViewer = new FormStructureViewer { formMain = this, Visible = false };
        FormStructureViewer.KeyDown += new KeyEventHandler(FormMain_KeyDown);
        FormStructureViewer.KeyUp += new KeyEventHandler(FormMain_KeyUp);

        commonDialog.Progress = ("Now Loading...Initializing 'Stereonet' form.", 0.25);
        FormStereonet = new FormStereonet { formMain = this, Visible = false };
        FormStereonet.KeyDown += new KeyEventHandler(FormMain_KeyDown);
        FormStereonet.KeyUp += new KeyEventHandler(FormMain_KeyUp);

        commonDialog.Progress = ("Now Loading...Initializing 'Crystal database' form.", 0.30);
        FormCrystalDatabase = new FormCrystalDatabase { FormMain = this, Visible = false };

        commonDialog.Progress = ("Now Loading...Initializing 'Crystal diffraction' form.", 0.35);
        FormDiffractionSimulator = new FormDiffractionSimulator { formMain = this, Visible = false };
        FormDiffractionSimulator.KeyDown += new KeyEventHandler(FormMain_KeyDown);
        FormDiffractionSimulator.KeyUp += new KeyEventHandler(FormMain_KeyUp);
        FormDiffractionSimulator.VisibleChanged += FormElectronDiffraction_VisibleChanged;

        commonDialog.Progress = ("Now Loading...Initializing 'HRTEM/STEM Image Simulator' form.", 0.40);
        FormImageSimulator = new FormImageSimulator { FormMain = this, Visible = false };

        commonDialog.Progress = ("Now Loading...Initializing 'Powder diffraction' form.", 0.45);
        FormPolycrystallineDiffractionSimulator = new FormPolycrystallineDiffractionSimulator { formMain = this, Visible = false };
        FormPolycrystallineDiffractionSimulator.VisibleChanged += formPolycrystallineDiffractionSimulator_VisibleChanged;

        commonDialog.Progress = ("Now Loading...Initializing 'Movie' form.", 0.5);
        FormMovie = new FormMovie() { FormMain = this, Visible = false };

        commonDialog.Progress = ("Now Loading...Initializing 'Trajectory Simulator' form.", 0.53);
        FormTrajectory = new FormTrajectory() { FormMain = this, Visible = false };

        commonDialog.Progress = ("Now Loading...Initializing 'EBSD Simulator' form.", 0.57);
        FormEBSD = new FormEBSD() { FormMain = this, Visible = false };

        commonDialog.Progress = ("Now Loading...Initializing 'TEM ID' form.", 0.6);
        FormSpotIDv1 = new FormSpotIDv1 { formMain = this, Visible = false };
        FormSpotIDv1.KeyDown += new KeyEventHandler(FormMain_KeyDown);
        FormSpotIDv1.KeyUp += new KeyEventHandler(FormMain_KeyUp);
        FormSpotIDv1.Visible = false;
        FormSpotIDv1.VisibleChanged += FormTEMID_VisibleChanged;

        commonDialog.Progress = ("Now Loading...Initializing 'Spot ID' form.", 0.65);
        FormSpotIDv2 = new FormSpotIDV2 { FormMain = this, Visible = false };

        commonDialog.Progress = ("Now Loading...Initializing 'Calculator' form.", 0.70);
        FormCalculator = new FormCalculator { Owner = this, Visible = false };
        FormCalculator.KeyDown += new KeyEventHandler(FormMain_KeyDown);
        FormCalculator.KeyUp += new KeyEventHandler(FormMain_KeyUp);
        FormCalculator.FormClosing += new FormClosingEventHandler(formCalculator_FormClosing);

        commonDialog.Progress = ("Now Loading...Initializing clipboard viewer.", 0.75);
        NextHandle = SetClipboardViewer(this.Handle);

        commonDialog.Progress = ("Now Loading...Initialize Crystal class.", 0.80);
        Crystal = new Crystal();

        commonDialog.Progress = ("Now Loading...Setting default crystal list.", 0.85);
        //var appPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\"; // 260612Cl 変更前
        // 260612Cl 変更: single-file publish では Assembly.Location が空文字になり (IL3000)、appPath が壊れて
        // 直後の initial.xml / AMCSD.cdb3 のコピーが失敗する。AppContext.BaseDirectory は single-file でも
        // 通常配置でも exe のフォルダを返す
        var appPath = AppContext.BaseDirectory;
        //default.xmlをinitial.xmlとしてコピー
        //if (!File.Exists(UserAppDataPath + "initial.xml"))
        File.Copy(appPath + "initial.xml", UserAppDataPath + "initial.xml", true);

        //ユーザーパスにdefault.xmlが存在しない場合、あるいは壊れている場合は、実行フォルダのinitial.xmlをdefault.xmlとしてユーザーパスにコピー
        if (!File.Exists(UserAppDataPath + "default.xml") || new FileInfo(UserAppDataPath + "default.xml").Length < 200)
            File.Copy(appPath + "initial.xml", UserAppDataPath + "default.xml", true);

        //初期結晶リストを読み込み
        ReadCrystalList(UserAppDataPath + "default.xml", false, true);

        //何らかの理由(前回が不正終了だったなど)でdefalut.xmlが壊れている場合はinitial.xmlを読み込む
        if (listBox.Items.Count == 0)
            ReadCrystalList(UserAppDataPath + "initial.xml", false, true);

        //ReciProSetup.msiは削除 アンインストール出来なくなるかもしれないので、削除はやめた方がいいかも 20240208
        //if (File.Exists(UserAppDataPath + "ReciProSetup.msi"))
        //    File.Delete(UserAppDataPath + "ReciProSetup.msi");

        commonDialog.Progress = ("Now Loading...Setting crystal database.", 0.90);
        //AMCSDをコピー
        File.Copy(appPath + "AMCSD.cdb3", UserAppDataPath + "AMCSD.cdb3", true);

        //UserAppDataPathに空フォルダがあったら削除
        foreach (var dir in Directory.GetDirectories(UserAppDataPath))
            if (!Directory.EnumerateFileSystemEntries(dir).Any())
                Directory.Delete(dir);


        commonDialog.Progress = ("Now Loading...Reading registries again.", 0.98);

        //ReadInitialRegistry();
        Registry(Reg.Mode.Read);

        //260613Cl イオン散乱因子: Registry 復元後に static モデルを確定させ、以降のユーザー操作では Σq 警告を出す (起動復元では出さない)。
        BetheMethod.ElasticIonModel = UseIonicScattering ? ElasticIonModel.IonFull : ElasticIonModel.Neutral;
        ionMenuReady = true;

        // 260420Cl 追加: レジストリから復元した Bounds が画面外 (モニタ構成変更や最小化 Bounds=-32000 の残存) の場合にプライマリ画面へ再配置する (#55 関連)。
        WindowLocation.EnsureVisible(this);

        //Text = "ReciPro  " + Version.VersionAndDate; // 260612Cl 変更前
        //if (glControlAxes == null)
        //    Text += "  (3D rendering disable mode)";
        // 260612Cl 変更: += の 2 段階追記だと、1 回目の代入直後に FormBase が末尾へ付ける "(F1: Help)" 案内の
        // 後ろに追記する形になり、案内が中間に残ったまま末尾へ二重付与される (StripHelpSuffix は末尾一致のみ)。
        // タイトルは一括で組み立てて 1 回だけ代入する (3D 無効モード = Mac/Wine や CI smoke で発症していた)
        //Text = "ReciPro  " + Version.VersionAndDate + (glControlAxes == null ? "  (3D rendering disable mode)" : ""); // 260703Cl 変更前
        // 260703Cl 変更: どの配布版が動いているか特定できるよう、タイトルにアーキテクチャ (x64/arm64) と配布形態 (msi/zip) を付記する。
        //   例: "ReciPro  ver4.942(2026/07/01, x64msi)"。配布形態は README-PORTABLE.txt の有無で判定
        //   (portable ZIP のみ同梱・MSI staging は release.yml の leak 検査で排除済み。上の Check Updates 非表示判定と同じ方法)。
        var arch = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant(); // "x64" / "arm64"
        var package = File.Exists(Path.Combine(AppContext.BaseDirectory, "README-PORTABLE.txt")) ? "zip" : "msi";
        Text = "ReciPro  " + Version.VersionAndDate[..^1] + $",{arch}{package})" + (glControlAxes == null ? "  (3D rendering disable mode)" : "");

        commonDialog.Progress = ("Initializing has been finished successfully. You can close this window.", 1.0);
        if (commonDialog.AutomaticallyClose)
            commonDialog.Visible = false;

        toolStripStatusLabel.Text = $"Startup time: {StatusBarHelper.FormatElapsed(sw.Elapsed)}";// 260520Cl 時間表記を StatusBarHelper.FormatElapsed で統一

        if (disableOpneGLToolStripMenuItem.Checked)
        {
            toolStripButtonStructureViewer.Enabled = false;
            toolStripButtonRotation.Enabled = false;
            glControlAxes?.Visible = false;
        }
        powderDiffractionFunctionsToolStripMenuItem_CheckedChanged(sender, e);


        //コマンドライン引数の解釈
        var args = Environment.GetCommandLineArgs();
        if (args != null)
        {
            if (args.Contains("/m"))//mをつけるとマクロ
            {
                var filename = args.First(a => a.EndsWith(".mcr") && File.Exists(a));
                if (filename != null)
                {
                    this.Visible = true;
                    // 260428Cl Thread.Sleep + Application.DoEvents の二重待機を Refresh + 短時間 Sleep に整理
                    this.Refresh();
                    Thread.Sleep(100);
                    var reader = new StreamReader(filename, Encoding.GetEncoding("UTF-8"));
                    FormMacro.RunMacro(reader.ReadToEnd());
                }
            }

            if (args.Contains("/x"))//xがあると、実行後に閉じる
                Close();
        }
    }

    /// <summary>クローズ時</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        //FormCalculator.Close();
        //FormStereonet.Close();
        //FormStructureViewer.Close();
        //FormDiffractionSimulator.Close();
        e.Cancel = false;
        // 260420Cl 追加: 最小化状態で閉じると Bounds = {-32000, -32000, ...} が保存されて次回起動時に画面外になる (#55 関連)。
        // 最小化/最大化時は WindowState を一旦 Normal に戻し、RestoreBounds 相当を保存する。
        if (WindowState != FormWindowState.Normal)
            WindowState = FormWindowState.Normal;
        //SaveInitialRegistry();
        Registry(Reg.Mode.Write);

        Properties.Settings.Default.Save();

        ChangeClipboardChain(this.Handle, NextHandle);

        var cry = new List<Crystal>();
        for (int i = 0; i < listBox.Items.Count; i++)
            cry.Add((Crystal)listBox.Items[i]);
        ConvertCrystalData.SaveCrystalListXml([.. cry], UserAppDataPath + "default.xml");
    }
    #endregion

    #region レジストリ操作
    private void Registry(Reg.Mode mode)
    {
        using var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Crystallography\\ReciPro");
        try
        {
            if (key == null) return;

            if (mode == Reg.Mode.Write)
                key.SetValue("Version", Version.VersionValue);

            //260415Cl 追加: ラムダ式ショートハンド。owner 変数の一次受けを廃止し、
            //Reg.RW(key, mode, owner, nameof(owner.X), owner.X) という冗長な呼び出しをrw(() => owner.X) の一行にまとめるためのローカル関数。
            void rw<T>(Expression<Func<T>> e) => Reg.RW(key, mode, e);

            //260415Cl Reg.RW<string>(key, mode, Thread.CurrentThread.CurrentUICulture, "Name");
            rw(() => Thread.CurrentThread.CurrentUICulture.Name);

            //260524Cl 追加: --capture の強制カルチャは Registry(Read) のたびにレジストリ値で上書きされてしまう
            //(直上の rw が CultureInfo.Name を読むため)。Read の直後に必ず強制カルチャへ戻すことで、
            //ctor だけでなく FormMain_Load 内の Registry(Read) (行360) 後に生成される子フォーム
            //(FormStructureViewer など) も強制カルチャで構築させる。これをしないと英語ページに日本語 GUI 等が出る。
            if (mode == Reg.Mode.Read && GuiCapture.ForcedUICulture != null)
                Thread.CurrentThread.CurrentUICulture = GuiCapture.ForcedUICulture;

            //260415Cl owner 一次受け廃止 + rw() ラムダ式呼び出しに書き換え
            rw(() => Bounds);
            //WindowLocation.Adjust(this);//20250804にコメントアウト (浜根氏の問題に対処)
            rw(() => DisableOpenGL);
            rw(() => DisableTextRendering);

            if (commonDialog == null)
                return;

            //Crystallography.Native.dllを使うかどうか
            if (mode == Reg.Mode.Read && toolStripMenuItemDisableNative.Enabled)
            {
                rw(() => DisableNative);
                BetheMethod.EigenEnabled = !toolStripMenuItemDisableNative.Checked;
            }
            //else if (mode == Reg.Mode.Write)
            //    rw(() => DisableNative);
            //260611Cl ロード失敗時 (メニュー無効+強制チェック) の状態をユーザー設定として永続化しない。
            //         native 欠落ビルドを一度起動すると DisableNative=true が保存され、以後 DLL が正常な起動でも
            //         EigenEnabled=false (CBED の Auto が Eigen_MKL=MathNet 経路に倒れる) が再現する事故が WoA 検証で発生
            else if (mode == Reg.Mode.Write && toolStripMenuItemDisableNative.Enabled)
                rw(() => DisableNative);

            //260405Cl MKLライブラリを使うかどうか
            //rw(() => UseMKL);
            //260611Cl ARM64 (メニュー非表示) では保存しない: 同一マシンの x64 エミュ実行が保存した UseMKL 設定を
            //arm64 実行が false で上書きしないため (DisableNative の交差汚染と同類の対策)。読込は従来通り
            if (mode == Reg.Mode.Read || toolStripMenuItemUseMKL.Visible)
                rw(() => UseMKL);

            //260421Cl Miller-Bravais (4指数) 表記を使うかどうか
            rw(() => UseMillerBravais);

            //260613Cl 動力学計算でイオン散乱因子 (Peng IonFull) を使うかどうか
            rw(() => UseIonicScattering);

            //260415Cl Reg.RW(key, mode, commonDialog, nameof(commonDialog.AutomaticallyClose), commonDialog.AutomaticallyClose);
            rw(() => commonDialog.AutomaticallyClose);

            if (FormStereonet == null)
                return;

            #region Stereonet
            rw(() => FormStereonet.Bounds);
            #endregion

            #region SpotIDv1
            rw(() => FormSpotIDv1.Bounds);
            #endregion

            #region DiffractionSimulator
            FormDiffractionSimulator.CancelSetVector = true;

            rw(() => FormDiffractionSimulator.Bounds);
            rw(() => FormDiffractionSimulator.Resolution);
            rw(() => FormDiffractionSimulator.ResolutionUnit);
            rw(() => FormDiffractionSimulator.FlipHorizontally);
            rw(() => FormDiffractionSimulator.FlipVertically);
            rw(() => FormDiffractionSimulator.NegativeImage);
            rw(() => FormDiffractionSimulator.IsCenterFixed);

            rw(() => FormDiffractionSimulator.waveLengthControl.WaveSource);
            rw(() => FormDiffractionSimulator.waveLengthControl.Energy);
            rw(() => FormDiffractionSimulator.waveLengthControl.XrayWaveSourceElementNumber);
            rw(() => FormDiffractionSimulator.waveLengthControl.XrayWaveSourceLine);

            rw(() => FormDiffractionSimulator.FormDiffractionSimulatorGeometry.FootX);
            rw(() => FormDiffractionSimulator.FormDiffractionSimulatorGeometry.FootY);
            rw(() => FormDiffractionSimulator.FormDiffractionSimulatorGeometry.CameraLength2);
            rw(() => FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorWidth);
            rw(() => FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorHeight);
            rw(() => FormDiffractionSimulator.FormDiffractionSimulatorGeometry.Tau);
            rw(() => FormDiffractionSimulator.FormDiffractionSimulatorGeometry.Phi);
            FormDiffractionSimulator.CancelSetVector = false;
            #endregion

            #region ImageSimulator

            rw(() => FormImageSimulator.Bounds);
            rw(() => FormImageSimulator.Setting);
            rw(() => FormImageSimulator.FormPresets.Settings);
            #endregion

            #region SpotID_V2
            rw(() => FormSpotIDv2.NearestNeighbor);
            rw(() => FormSpotIDv2.FittingRange);
            rw(() => FormSpotIDv2.ToleranceLength);
            #endregion

            if (mode == Reg.Mode.Read)
                FormMacro.ZippedMacros = (byte[])key.GetValue("Macro", Array.Empty<byte>());
            else
                key.SetValue("Macro", FormMacro.ZippedMacros);
        }
        finally
        {
            key.Close();
            key.Dispose();
        }

    }
    #endregion レジストリ操作

    #region Axisの描画関連

    //軸の情報を表示する部分
    public void DrawAxes()
    {
        if (glControlAxes == null)
            return;
        glControlAxes.WorldMatrixEx = Crystal?.RotationMatrix.Transpose();
    }

    private void ResetAxes()
    {
        if (glControlAxes == null || Crystal.A == 0 || Crystal.B == 0 || Crystal.C == 0)
            return;

        var max = new[] { Crystal.A, Crystal.B, Crystal.C }.Max();
        var vec = new[] { Crystal.A_Axis / max, Crystal.B_Axis / max, Crystal.C_Axis / max };
        var color = new[] { Col4.Red, Col4.Green, Col4.Blue };
        var label = new[] { "a", "b", "c" };
        var obj = new List<GLObject>(10);
        for (int i = 0; i < 3; i++)
        {
            obj.Add(new Cylinder(-vec[i], vec[i] * 2 - 0.3 * vec[i].Normarize(), 0.075, new Material(color[i]), DrawingMode.Surfaces));
            obj.Add(new Cone(vec[i], -0.3 * vec[i].Normarize(), 0.15, new Material(color[i]), DrawingMode.Surfaces));
            obj.Add(new TextObject(label[i], 13, vec[i] + 0.1 * vec[i].Normarize(), 0, true, new Material(color[i]), glControlAxes));
        }
        obj.Add(new Sphere(new Vec3(0, 0, 0), 0.2, new Material(Col4.Gray), DrawingMode.Surfaces));

        glControlAxes.DeleteAllObjects();
        glControlAxes.AddObjects(obj);

        DrawAxes();
    }

    private void panelAxes_MouseDown(object sender, MouseEventArgs e)
    {
        if (glControlAxes == null) return;

        glControlAxes.Focus();
        if (e.Button == MouseButtons.Right && e.Clicks == 2)
        {
            var bmp = glControlAxes.GenerateBitmap();
            if (bmp != null)
                Clipboard.SetDataObject(bmp, true, 10, 100);
        }
    }

    private Point lastPosAxes;

    private void panelAxes_MouseMove(object sender, MouseEventArgs e)
    {
        if (glControlAxes == null) return;

        if (e.Button == MouseButtons.Left)
        {
            int dx = e.X - lastPosAxes.X, dy = lastPosAxes.Y - e.Y;
            Rotate((-dy, dx, 0), Math.Sqrt(dx * dx + dy * dy) / 360 * Math.PI);
        }
        lastPosAxes = e.Location;
    }

    #endregion Axisの描画関連

    #region 回転操作

    /// <summary>回転量と回転角度を指定して、全フォームに回転命令を出す</summary>
    /// <param name="axis"></param>
    /// <param name="angle"></param>
    public void Rotate((double X, double Y, double Z) axis, double angle) => Rotate(new Vector3DBase(axis.X, axis.Y, axis.Z), angle);

    /// <summary>回転量と回転角度を指定して、全フォームに回転命令を出す</summary>
    /// <param name="axis"></param>
    /// <param name="angle"></param>
    public void Rotate(Vector3DBase axis, double angle)
    {
        if (angle == 0) return;

        axis = axis.Normarize();

        if (FormRotation.Linked)//FormRotationのリンクが有効な場合は、FormRotation側で回転状況を制御する
        {
            FormRotation.SetRotation(Matrix3D.Rot(axis, angle) * Crystal.RotationMatrix);
            return;
        }

        for (int i = 0; i < Crystals.Length; i++)
        {
            Matrix3D rot;
            if (!checkBoxFixAxis.Checked && !checkBoxFixePlane.Checked && !FormRotation.Linked)
                rot = Matrix3D.Rot(axis, angle);
            else
            {
                var (u, v, w) = indexControlAxis.Values;
                var (h, k, l) = indexControlPlane.Values;
                var newAxis = checkBoxFixAxis.Checked ?
                     Crystals[i].RotationMatrix * (u * Crystal.A_Axis + v * Crystal.B_Axis + w * Crystal.C_Axis) :
                     Crystals[i].RotationMatrix * (h * Crystal.A_Star + k * Crystal.B_Star + l * Crystal.C_Star);                // 260422Cl HKLControl revert
                if (Vector3DBase.AngleBetVectors(newAxis, axis) < Math.PI / 2)
                    rot = Matrix3D.Rot(newAxis, angle);
                else
                    rot = Matrix3D.Rot(newAxis, -angle);
            }
            Crystals[i].RotationMatrix = rot * Crystals[i].RotationMatrix;
        }
        SetRotation(Crystals[0].RotationMatrix);
    }

    /// <summary>回転行列を指定して、全フォームの回転状態をセットする</summary>
    /// <param name="mat"></param>
    public void SetRotation(Matrix3D mat)
    {
        if (InvokeRequired)//別スレッドから呼び出されたとき Invokeして呼びなおす
        {
            Invoke(new Action(() => SetRotation(mat)), null);
            return;
        }
        Crystal.RotationMatrix = mat;
        if (FormStructureViewer.Visible)
            FormStructureViewer.Draw();

        if (FormStereonet.Visible)
            FormStereonet.Draw();

        if (FormDiffractionSimulator.Visible)
            FormDiffractionSimulator.RotationChanged();
        //FormDiffractionSimulator.Draw();

        if (FormImageSimulator.Visible)
            FormImageSimulator.RotationChanged();

        if (FormEBSD.Visible)
            FormEBSD.Draw();

        if (SkipEulerChange && FormRotation.Visible)//Euler angle が直接入力された時
            FormRotation.SetRotation();

        DrawAxes();

        if (!SkipEulerChange)//Euler Angle が直接入力されてない時
        {
            var euler = Euler.FromMatrix(Crystal.RotationMatrix);
            SkipEulerChange = true;
            // 260519Cl 変更: NumericUpDown → NumericBox (RadianValue setter で簡略化)
            //numericUpDownEulerPhi.Value = (decimal)(euler.Phi / Math.PI * 180);
            //numericUpDownEulerTheta.Value = (decimal)(euler.Theta / Math.PI * 180);
            //numericUpDownEulerPsi.Value = (decimal)(euler.Psi / Math.PI * 180);
            numericBoxEulerPhi.RadianValue = euler.Phi;
            numericBoxEulerTheta.RadianValue = euler.Theta;
            numericBoxEulerPsi.RadianValue = euler.Psi;
            SkipEulerChange = false;

            if (FormRotation.Visible)
                FormRotation.SetRotation();

            SetNearestUVW();
        }
    }


    public void SetRotation(double phi, double theta, double psi)
    {
        SetRotation(Matrix3D.Rot(phi, theta, psi));
    }

    #endregion

    #region 回転ボタン

    //角度リセットボタン
    private void ButtonReset_Click(object sender, EventArgs e)
    {
        timer.Stop();
        SetRotation(new Matrix3D());
    }

    private void ButtonDirection_Click(object sender, EventArgs e)
    {
        var v = (sender as Button).Name switch
        {
            "buttonTopRight" => new Vector3DBase(-1, 1, 0),
            "buttonRight" => new Vector3DBase(0, 1, 0),
            "buttonBottomRight" => new Vector3DBase(1, 1, 0),
            "buttonBottom" => new Vector3DBase(1, 0, 0),
            "buttonBottomLeft" => new Vector3DBase(1, -1, 0),
            "buttonLeft" => new Vector3DBase(0, -1, 0),
            "buttonTopLeft" => new Vector3DBase(-1, -1, 0),
            "buttonTop" => new Vector3DBase(-1, 0, 0),
            "buttonClock" => new Vector3DBase(0, 0, -1),
            "buttonAntiClock" => new Vector3DBase(0, 0, 1),
            _ => new Vector3DBase(0, 0, 1)
        };

        if (checkBoxAnimation.Checked)
            StartAnimation(v);
        else
            Rotate(v, numericBoxStep.RadianValue);
    }

    private readonly Stopwatch stopwatchAnimation = new();
    private long ellapseTime = 0;

    private void StartAnimation(Vector3DBase v)
    {
        timer.Stop();
        stopwatchAnimation.Restart();
        ellapseTime = 0;
        rotationAxisAnimation = v;
        timer.Start();
    }

    private Vector3DBase rotationAxisAnimation = new Vector3D(0, 0, 1);
    private int timerCounter = 1;

    private void Timer_Tick(object sender, EventArgs e)
    {
        double differenceTime = stopwatchAnimation.ElapsedMilliseconds - ellapseTime;
        ellapseTime = stopwatchAnimation.ElapsedMilliseconds;
        if (timerCounter++ % 5 == 0)
        {
            toolStripStatusLabel.Text = $"Frame rate: {1000.0 / differenceTime:f1} frm/s";
            timerCounter = 1;
        }

        double angle = differenceTime / 1000.0 * numericBoxStep.RadianValue;
        Rotate(rotationAxisAnimation, angle);
    }
    private void CheckBoxAnimation_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxAnimation.Checked)
            numericBoxStep.FooterText = "°/s";
        else
        {
            numericBoxStep.FooterText = "°";
            timer.Stop();
        }
    }

    #endregion 回転ボタン

    #region hklやuvwベクトルでの回転指定

    public void SetPlane(int h, int k, int l) => indexControlPlane.Values = (h, k, l);
    public void SetAxis(int u, int v, int w) => indexControlAxis.Values = (u, v, w);
    public void ProjectAlongAxis() => buttonSetVector_Click(buttonSetAxis, new EventArgs());
    public void ProjectAlongPlane() => buttonSetVector_Click(buttonSetPlane, new EventArgs());

    private void buttonSetVector_Click(object sender, EventArgs e)
    {
        if (Crystal == null) return;
        var (u, v, w) = indexControlAxis.Values;
        var (h, k, l) = indexControlPlane.Values;                                                         // 260422Cl HKLControl revert

        Vector3DBase xVector, yVector, zVector;
        Vector3DBase aAxis = Crystal.A_Axis, bAxis = Crystal.B_Axis, cAxis = Crystal.C_Axis;
        var matrixInverse = Matrix3D.Inverse(new Matrix3D(aAxis, bAxis, cAxis));
        var aStar = new Vector3D(matrixInverse.E11, matrixInverse.E12, matrixInverse.E13);
        var bStar = new Vector3D(matrixInverse.E21, matrixInverse.E22, matrixInverse.E23);
        var cStar = new Vector3D(matrixInverse.E31, matrixInverse.E32, matrixInverse.E33);
        //軸を立てるとき
        if (((Button)sender).Name == "buttonSetAxis" && !(u == 0 && v == 0 && w == 0))
        {
            //まず立てる軸のベクトルを探す
            zVector = u * aAxis + v * bAxis + w * cAxis;
            zVector.NormarizeThis();
            //上向きのベクトルを決める
            if (u * h + v * k + w * l != 0 || (h == 0 && k == 0 && l == 0))//正しく設定されていないときはhkl面を設定してやる
            {
                if (u == 0 && v != 0 && w != 0) { h = 1; k = 0; l = 0; }
                else if (u != 0 && v == 0 && w != 0) { h = 0; k = 1; l = 0; }
                else if (u != 0 && v != 0 && w == 0) { h = 0; k = 0; l = 1; }
                else if (u == 0 && v == 0 && w != 0) { h = 1; k = 0; l = 0; }
                else if (u != 0 && v == 0 && w == 0) { h = 0; k = 1; l = 0; }
                else if (u == 0 && v != 0 && w == 0) { h = 0; k = 0; l = 1; }
                else { h = v; k = -u; l = 0; }
            }
            yVector = h * aStar + k * bStar + l * cStar;
            yVector.NormarizeThis();
        }//面を立てるとき
        else if (((Button)sender).Name == "buttonSetPlane" && !(h == 0 && k == 0 && l == 0))
        {
            //まず立てる面のベクトルを探す
            zVector = h * aStar + k * Crystal.B_Star + l * cStar;
            zVector.NormarizeThis();
            //上向きのベクトルを決める
            if (u * h + v * k + w * l != 0 || (u == 0 && v == 0 && w == 0))//正しく設定されていないときはhkl面を設定してやる
            {
                if (h == 0 && k != 0 && l != 0) { u = 1; v = 0; w = 0; }
                else if (h != 0 && k == 0 && l != 0) { u = 0; v = 1; w = 0; }
                else if (h != 0 && k != 0 && l == 0) { u = 0; v = 0; w = 1; }
                else if (h == 0 && k == 0 && l != 0) { u = 1; v = 0; w = 0; }
                else if (h != 0 && k == 0 && l == 0) { u = 0; v = 1; w = 0; }
                else if (h == 0 && k != 0 && l == 0) { u = 0; v = 0; w = 1; }
                else { u = k; v = -h; w = 0; }
            }
            yVector = u * aAxis + v * bAxis + w * cStar;
            yVector.NormarizeThis();
        }
        else
            return;

        xVector = Vector3D.VectorProduct(yVector, zVector);
        //xVector,yVector,zVectorが(100),(010),(001)に一致すればいいのだから　
        var matrix = Matrix3D.Inverse(new Matrix3D(xVector, yVector, zVector));
        SetRotation(matrix);
    }

    #endregion ベクトルでの回転指定

    #region オイラー角度を直接入力した場合

    public bool SkipEulerChange = false;

    /// <summary>オイラー角の入力ボックスからの変更イベント</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    // 260519Cl 変更: NumericUpDown → NumericBox (旧シグネチャ: numericUpDownEulerAngle_ValueChanged)
    private void numericBoxEulerAngle_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEulerChange) return;
        SkipEulerChange = true;
        if (numericBoxEulerPhi.Value > 180)
            numericBoxEulerPhi.Value -= 360;
        if (numericBoxEulerPhi.Value < -180)
            numericBoxEulerPhi.Value += 360;

        if (numericBoxEulerTheta.Value > 180)
            numericBoxEulerTheta.Value -= 360;
        if (numericBoxEulerTheta.Value < -180)
            numericBoxEulerTheta.Value += 360;

        if (numericBoxEulerPsi.Value > 180)
            numericBoxEulerPsi.Value -= 360;
        if (numericBoxEulerPsi.Value < -180)
            numericBoxEulerPsi.Value += 360;

        SetRotation(Matrix3D.Rot(numericBoxEulerPhi.RadianValue, numericBoxEulerTheta.RadianValue, numericBoxEulerPsi.RadianValue));

        SkipEulerChange = false;
        SetNearestUVW();
    }




    #endregion オイラー角度を直接入力したばあい

    #region 他のFunctionを起動、連携
    private void powderDiffractionFunctionsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
    {
        toolStripButtonDiffractionPoly.Visible = powderDiffractionFunctionToolStripMenuItem.Checked;
        toolStripSeparator19.Visible = powderDiffractionFunctionToolStripMenuItem.Checked;
    }

    // 260323Cl 追加
    private void captureGUIToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new Crystallography.Controls.FormCaptureGUI { Owner = this }.Show(); // 260521Cl Phase6: Owner統一(§2.7)
    }
    private void FormTEMID_VisibleChanged(object sender, EventArgs e)
    {
        listBox.SelectionMode = FormSpotIDv1.Visible || FormDiffractionSimulator.Visible || FormPolycrystallineDiffractionSimulator.Visible ?
            SelectionMode.MultiExtended : SelectionMode.One;
    }

    private void FormElectronDiffraction_VisibleChanged(object sender, EventArgs e)
    {
        listBox.SelectionMode = FormSpotIDv1.Visible || FormDiffractionSimulator.Visible || FormPolycrystallineDiffractionSimulator.Visible ?
            SelectionMode.MultiExtended : SelectionMode.One;
    }

    private void formPolycrystallineDiffractionSimulator_VisibleChanged(object sender, EventArgs e)
    {
        listBox.SelectionMode = FormSpotIDv1.Visible || FormDiffractionSimulator.Visible || FormPolycrystallineDiffractionSimulator.Visible ?
            SelectionMode.MultiExtended : SelectionMode.One;
    }

    private void crystalControl_BeamInteraction_VisibleChanged(object sender, EventArgs e) => toolStripButtonBeamInteraction.Checked = crystalControl.FormBeamInteraction.Visible;

    private void CrystalControl_SymmetryInformation_VisibleChanged(object sender, EventArgs e) => toolStripButtonSymmetryInformation.Checked = crystalControl.FormSymmetryInformation.Visible;

    /// <summary>ToolStripボタンを押されたら、各機能を起動/終了する</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void toolStripButtons_MouseDown(object sender, MouseEventArgs e)
    {
        var button = sender as ToolStripButton;
        Form form;
        if (button.Name.Contains("Structure"))
            form = FormStructureViewer;
        else if (button.Name.Contains("Database"))
            form = FormCrystalDatabase;
        else if (button.Name.Contains("Symmetry"))
            form = crystalControl.FormSymmetryInformation;
        else if (button.Name.Contains("BeamInteraction"))
            form = crystalControl.FormBeamInteraction;
        else if (button.Name.Contains("Rotation"))
            form = FormRotation;
        else if (button.Name.Contains("Stereonet"))
            form = FormStereonet;
        else if (button.Name.Contains("DiffractionSingle"))
            form = FormDiffractionSimulator;
        else if (button.Name.Contains("ImageSimulator"))
            form = FormImageSimulator;
        else if (button.Name.Contains("SpotIDv1"))
            form = FormSpotIDv1;
        else if (button.Name.Contains("SpotIDv2"))
            form = FormSpotIDv2;
        else if (button.Name.Contains("Trajectory"))
            form = FormTrajectory;
        else if (button.Name.Contains("EBSD"))
            form = FormEBSD;
        else
            form = FormPolycrystallineDiffractionSimulator;

        if (e.Clicks == 1)
        {
            if (!form.Visible)
            {
                form.Visible = true;
                form.WindowState = FormWindowState.Normal;
                WindowLocation.Adjust(form);
            }
            else if (form.WindowState == FormWindowState.Minimized)
                form.WindowState = FormWindowState.Normal;
            else
                form.Visible = false;
        }
        else if (e.Clicks == 2)
        {
            form.Visible = true;
            form.WindowState = FormWindowState.Normal;
            form.BringToFront();
        }
        button.Checked = form.Visible;
    }

    private void toolStripButtonPolycrystallineDiffraction_CheckedChanged(object sender, EventArgs e)
    {
        FormPolycrystallineDiffractionSimulator.Visible = toolStripButtonDiffractionPoly.Checked;
        ListBox_SelectedIndexChanged(listBox, e);
    }

    private void formCalculator_FormClosing(object sender, FormClosingEventArgs e)
    {
        FormCalculator.Visible = false;
        e.Cancel = true;
    }


    #endregion

    #region CrystalControlからのCrystalChangedイベント
    private void crystalControl_CrystalChanged(object sender, EventArgs e)
    {
        if (crystalControl.Crystal != null)
        {
            var euler = Euler.FromMatrix(Crystal.RotationMatrix);
            SkipEulerChange = true;
            // 260519Cl 変更: NumericUpDown → NumericBox (RadianValue setter で簡略化)
            //numericUpDownEulerPhi.Value = (decimal)(euler.Phi / Math.PI * 180);
            //numericUpDownEulerTheta.Value = (decimal)(euler.Theta / Math.PI * 180);
            //numericUpDownEulerPsi.Value = (decimal)(euler.Psi / Math.PI * 180);
            numericBoxEulerPhi.RadianValue = euler.Phi;
            numericBoxEulerTheta.RadianValue = euler.Theta;
            numericBoxEulerPsi.RadianValue = euler.Psi;
            SkipEulerChange = false;

            numericBoxMaxUVW_ValueChanged(sender, e);

            UpdatePlaneIndices();                                                                                                                 // 260421Cl 結晶切替時に4指数表示を更新

            if (SkipDrawing) return;

            if (FormStructureViewer != null && FormStructureViewer.Visible)
                FormStructureViewer.SetGLObjects(crystalControl.Crystal);

            if (FormStereonet != null && FormStereonet.Visible)
                FormStereonet.SetCrystal();

            if (FormDiffractionSimulator != null && FormDiffractionSimulator.Visible)
                FormDiffractionSimulator.SetCrystal();

            if (FormSpotIDv2 != null && FormSpotIDv2.Visible)
                FormSpotIDv2.SetCrystal();

            if (FormRotation != null && FormRotation.Visible)
                FormRotation.SetRotation();

            if (FormImageSimulator != null && FormImageSimulator.Visible)
                FormImageSimulator.RotationChanged();

            if (FormEBSD != null && FormEBSD.Visible)
                FormEBSD.SetCrystal();

            ResetAxes();
        }
    }
    #endregion

    #region リストボックス関連

    // 260520Cl 改名: 内部命名を表示テキスト/機能に一致 — buttonUpper→buttonUp, buttonLower→buttonDown, buttonChange→buttonReplace (ハンドラ ButtonUpper_Click→ButtonUp_Click / ButtonLower_Click→ButtonDown_Click も)
    private void ButtonUp_Click(object sender, EventArgs e) => MoveUp();

    public void MoveUp()
    {
        var n = listBox.SelectedIndex;
        if (n <= 0) return;
        object o = listBox.SelectedItem;
        listBox.Items.Remove(listBox.SelectedItem);
        listBox.Items.Insert(n - 1, o);
        listBox.SelectedIndex = n - 1;
        // 260428Cl Application.DoEvents() を削除 (リスト更新は WinForms 標準で十分)
    }

    private void ButtonDown_Click(object sender, EventArgs e) => MoveDown();
    public void MoveDown()
    {
        int n = listBox.SelectedIndex;
        if (n >= listBox.Items.Count - 1) return;
        object o = listBox.SelectedItem;
        listBox.Items.Remove(listBox.SelectedItem);
        listBox.Items.Insert(n + 1, o);
        listBox.SelectedIndex = n + 1;
        // 260428Cl Application.DoEvents() を削除
    }

    [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("USER32.DLL")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
    private void ButtonAdd_Click(object sender, EventArgs e)
    {

        IntPtr calcWindow = FindWindow(null, "Power Automate");

        if (SetForegroundWindow(calcWindow))
            SendKeys.Send("{ENTER}");

        AddCrystal();
    }

    public void AddCrystal()
    {
        if (crystalControl.StrainControlVisible) return;

        crystalControl.GenerateFromInterface();
        if (crystalControl.Crystal != null)
            listBox.Items.Add(crystalControl.Crystal);
        listBox.SelectedIndex = listBox.Items.Count - 1;
        // 260428Cl Application.DoEvents() を削除
    }

    private void buttonDuplicate_Click(object sender, EventArgs e) => DuplicateCrystal();

    public void DuplicateCrystal()
    {
        if (listBox.SelectedIndex < 0) return;
        var c = (Crystal)listBox.SelectedItem;
        var newCrystal = new Crystal(c);

        var index = newCrystal.Name.LastIndexOf(" #");
        if (index >= 0 && int.TryParse(newCrystal.Name[(index + 2)..], out int num))
            newCrystal.Name = newCrystal.Name[0..(index + 2)] + (num + 1).ToString();
        else
            newCrystal.Name += " #1";

        listBox.Items.Insert(listBox.SelectedIndex + 1, newCrystal);
        listBox.SelectedIndex++;
        // 260428Cl Application.DoEvents() を削除
    }


    private void ButtonDelete_Click(object sender, EventArgs e) => DeleteCrystal();

    public void DeleteCrystal()
    {
        if (listBox.SelectedIndex >= 0)
        {
            var n = listBox.SelectedIndex;
            listBox.Items.Remove(listBox.SelectedItem);
            if (listBox.Items.Count > n)
                listBox.SelectedIndex = n;
            else
                listBox.SelectedIndex = n - 1;
            // 260428Cl Application.DoEvents() を削除
        }
    }

    private void ButtonAllClear_Click(object sender, EventArgs e) => CrystalListClear();
    public void CrystalListClear() => listBox.Items.Clear();

    private void ButtonReplace_Click(object sender, EventArgs e) => ReplaceCrystal();
    public void ReplaceCrystal()
    {
        if (crystalControl.StrainControlVisible) return;

        if (listBox.SelectedIndex < 0) return;

        crystalControl.GenerateFromInterface();

        if (crystalControl.Crystal != null && listBox.SelectedIndex >= 0)
            listBox.Items[listBox.SelectedIndex] = crystalControl.Crystal;
    }


    private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listBox.SelectedIndex >= 0)
            crystalControl.Crystal = (Crystal)listBox.SelectedItem;
        DrawAxes();
    }

    /// <summary>
    /// (260523Ch) --capture 専用の代表結晶選択。
    /// 通常起動時の初期選択は従来どおり変更しないが、マニュアル/GUI監査用スクリーンショットでは
    /// 初期値の Au だと「重元素かつ単純構造」の絵になり、典型例として偏りやすい。
    /// そのため一括キャプチャ時だけ、結晶リストから Spinel など指定名を含む結晶を探して選択する。
    /// FormTrajectory など FormMain.Crystal を参照して計算するフォームも、この選択結果を代表状態として使う。
    /// </summary>
    /// <param name="preferredName">結晶名に含まれていてほしい文字列。既定は "spinel"。</param>
    /// <returns>指定名を含む結晶を見つけて選択できた場合は true。</returns>
    internal bool PrepareCaptureCrystalSelection(string preferredName = "spinel")
    {
        if (string.IsNullOrWhiteSpace(preferredName))
            return false;

        for (var i = 0; i < listBox.Items.Count; i++)
        {
            if (listBox.Items[i] is not Crystal crystal || string.IsNullOrEmpty(crystal.Name))
                continue;

            if (crystal.Name.Contains(preferredName, StringComparison.OrdinalIgnoreCase))
            {
                // SelectedCrystalIndex = 0; // 旧実装相当: --capture では初期選択 Au のまま撮影・計算していた
                SelectedCrystalIndex = i; // (260523Ch) --capture の代表結晶は Spinel にして構造/計算結果を典型例に近づける
                Application.DoEvents();
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 260523Cl 追加: --capture 用。crystalControl が保持する結晶依存の子フォーム
    /// (FormSymmetryInformation / FormBeamInteraction) を返す。
    /// これらは Crystallography.Controls アセンブリにあり、かつ親から CrystalControl を注入されて
    /// 初めて動く (単独 reflection 生成では Load で NullReferenceException)。そのため --capture の
    /// reflection 列挙では撮れず、spinel 選択済みの FormMain が持つ配線済みインスタンスを GuiCapture へ渡す。
    /// </summary>
    internal IEnumerable<Form> EnumerateCaptureCrystalDependentForms()
    {
        if (crystalControl == null)
            yield break;
        if (crystalControl.FormSymmetryInformation != null)
            yield return crystalControl.FormSymmetryInformation;
        // 260705Cl 追加: group-subgroup 関係ブラウザ (Phase 2)。FormSymmetryInformation の「Group Relations...」ボタンと
        // 同じ経路 (ShowGroupRelations) でインスタンスを用意し、reflection 単独生成 (親結晶なし) を回避する。
        if (crystalControl.FormSymmetryInformation?.ShowGroupRelations() is { } groupRelations)
            yield return groupRelations;
        if (crystalControl.FormBeamInteraction != null)
            yield return crystalControl.FormBeamInteraction;
        // 260524Cl 追加: FormStructureViewer は reflection 単独生成だと formMain=null で結晶が無く、原子タブ等が空で
        // クロップされない。FormMain が保持する配線済みインスタンスを渡すと、Show 時の VisibleChanged で
        // 現在結晶 (spinel) が SetGLObjects され、原子タブ含め代表状態で撮れる (reflection 版を上書き)。
        if (FormStructureViewer != null)
            yield return FormStructureViewer;
        // 260524Cl 追加: 回折スポット情報 (FormDiffractionBeamTable) の表は「動力学効果」で計算しないと空になる。
        // FormDiffractionSimulator 側で動力学計算を走らせて配線済みの表を populate し、それを撮る (reflection 単独生成の空表版を上書き)。
        if (FormDiffractionSimulator?.PrepareCaptureSpotInfoForGuiAudit() is { } diffractionSpotInfo)
            yield return diffractionSpotInfo;
    }

    private void listBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (renameTextBox != null && groupBoxCrystalList.Controls.Contains(renameTextBox))
            renameTextBox_Leave(sender, e);

        var index = listBox.IndexFromPoint(e.Location);

        if (e.Button == MouseButtons.Right)
        {
            if ((uint)index < (uint)listBox.Items.Count)
            {
                if (listBox.SelectedIndex != index)
                    listBox.SelectedIndex = index;
                contextMenuStripListBox.Show(listBox, e.Location);
            }
        }
    }

    /// <summary>選択結晶をCIF形式で保存</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void exportAsCIFFormatToolStripMenuItem_Click(object sender, EventArgs e) => ExportCIF();

    private void duplicateToolStripMenuItem_Click(object sender, EventArgs e) => DuplicateCrystal();

    private void deleteToolStripMenuItem_Click(object sender, EventArgs e) => DeleteCrystal();

    #region リストボックス上で結晶の名前を変更
    TextBox renameTextBox;
    private void renameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var pos = new Point(-1, -1);
        for (int x = 0; x < listBox.Width; x += listBox.ColumnWidth)
            for (int y = 0; y < listBox.Height; y += listBox.ItemHeight)
                if (listBox.SelectedIndex == listBox.IndexFromPoint(x, y))
                    pos = new Point(x, y);

        if (pos.X < 0) return;

        if (renameTextBox == null)//初回だけ初期化
        {
            renameTextBox = new TextBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = listBox.Font,
                Margin = new Padding(0),
                Padding = new Padding(0),
                BackColor = Color.LightYellow,
                Size = new Size(listBox.ColumnWidth + 2, listBox.ItemHeight),
            };
            renameTextBox.Leave += (s, ev) => renameTextBox_Leave(s, ev);
            renameTextBox.KeyDown += (s, ev) => renameTextBox_KeyDown(s, ev);
        }
        renameTextBox.Location = new Point(listBox.Location.X + pos.X, listBox.Location.Y + pos.Y);
        renameTextBox.Text = listBox.SelectedItem.ToString();
        renameTextBox.Tag = listBox.SelectedIndex;
        groupBoxCrystalList.Controls.Add(renameTextBox);
        renameTextBox.BringToFront();
        renameTextBox.Focus();
    }
    void renameTextBox_Leave(object sender, EventArgs e)
    {
        if (groupBoxCrystalList.Controls.Contains(renameTextBox) && renameTextBox.Tag is int index)
        {
            var crystal = (Crystal)listBox.Items[index];
            crystal.Name = renameTextBox.Text;
            listBox.Items[index] = crystal;
            groupBoxCrystalList.Controls.Remove(renameTextBox);
        }
    }

    void renameTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            renameTextBox_Leave(sender, e);

        if (e.KeyCode == Keys.Escape)
        {
            renameTextBox.Text = listBox.SelectedItem.ToString();
            renameTextBox_Leave(sender, e);
        }
    }
    #endregion 

    #endregion リストボックス関連

    #region 結晶リストの読み込み/書き込み
    public void ReadCrystalList(string fileName = "", bool showSelectionDialog = false, bool clearPresentList = false)
    {
        if (fileName == "")
        {
            var dlg = new OpenFileDialog { Filter = "xml|*.xml" };
            if (dlg.ShowDialog() == DialogResult.OK)
                fileName = dlg.FileName;
            else
                return;
        }

        var cry = new List<Crystal>();
        var list = ConvertCrystalData.ConvertToCrystalList(fileName);
        if (list == null)
            return;
        cry.AddRange(list);
        if (showSelectionDialog)
        {
            var formCrystalSelection = new FormCrystalSelection { LoadMode = true };
            formCrystalSelection.SetCrystalList(cry);
            formCrystalSelection.Location = new Point(this.Location.X + this.Width / 2 - formCrystalSelection.Width / 2, this.Location.Y + this.Height / 2 - formCrystalSelection.Height / 2);
            if (formCrystalSelection.ShowDialog() == DialogResult.OK)
            {
                cry.Clear();
                cry.AddRange(formCrystalSelection.CheckedCrystalList);
            }
            else return;
        }

        if (cry.Count != 0)
        {
            if (clearPresentList)
                listBox.Items.Clear();

            foreach (var c in cry)
                listBox.Items.Add(c);

            if (listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;
        }
    }

    private void SaveCrystalDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var cry = new List<Crystal>();
        for (int i = 0; i < listBox.Items.Count; i++)
            cry.Add((Crystal)listBox.Items[i]);

        var formCrystalSelection = new FormCrystalSelection { LoadMode = false };
        formCrystalSelection.SetCrystalList(cry);
        if (formCrystalSelection.ShowDialog() == DialogResult.OK)
        {
            var Dlg = new System.Windows.Forms.SaveFileDialog { Filter = "xml|*.xml" };
            try
            {
                if (Dlg.ShowDialog() == DialogResult.OK)
                    ConvertCrystalData.SaveCrystalListXml(formCrystalSelection.CheckedCrystalList, Dlg.FileName);
            }
            catch
            {
                // 260617Cl 変更: 日本語固定リテラルは英語(neutral)UIで未翻訳に見えるため英語化 (Phase 0)。本格 localize は Phase 4。
                MessageBox.Show("Failed to write the file."); // 旧: "ファイルが書き込みません"
            }
        }
    }

    #endregion

    #region FileMenu

    private void readCrystalFromCIFOrAMCFileToolStripMenuItem_Click(object sender, EventArgs e) => ReadCrystal();


    public void ReadCrystal(string fileName = "")
    {
        if (fileName == "")
        {
            var dlg = new OpenFileDialog { Filter = "cif, amc|*.cif;*.amc" };
            if (dlg.ShowDialog() == DialogResult.OK)
                fileName = dlg.FileName;
            else
                return;
        }

        crystalControl.ReadCrystal(fileName);
    }

    private void readCrystalDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var dlg = new OpenFileDialog { Filter = "xml, out|*.xml;*.out" };
        if (dlg.ShowDialog() == DialogResult.OK)
            ReadCrystalList(dlg.FileName, true, true);
    }

    private void readCrystalDataAndAddtoolStripMenuItem_Click(object sender, EventArgs e)
    {
        var dlg = new OpenFileDialog { Filter = "xml, out|*.xml;*.out" };
        if (dlg.ShowDialog() == DialogResult.OK)
            ReadCrystalList(dlg.FileName, true, false);
    }

    private void ToolStripMenuItemReadInitialCrystalList_Click(object sender, EventArgs e)
        => ReadCrystalList(UserAppDataPath + "initial.xml", false, true);

    /// <summary>260622Cl 追加: 現在の UI 言語に対応する GitHub Pages マニュアルの基底 URL を返す。
    /// 英語(HelpCulture=="en")はサイトルート、他言語は /ReciPro/&lt;lang&gt;/ (mkdocs-static-i18n の出力構造)。
    /// マニュアル未整備の言語は SupportedCultures.HelpCulture が "en" のままなので英語マニュアルへ落ちる。</summary>
    public static string HelpBaseUrl()
    {
        var help = Crystallography.SupportedCultures.Current.HelpCulture;
        return help == "en" ? "https://seto77.github.io/ReciPro/" : $"https://seto77.github.io/ReciPro/{help}/";
    }

    private void helpwebToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // 260602Cl: yseto.net 旧 web マニュアル(現在は GitHub Pages への移転スタブ)と同梱の旧 PDF(2023年版) を廃止し、
        //           GitHub Pages のマニュアルを UI 言語別に開くよう変更。
        // 260622Cl: ja/en 二値判定を HelpBaseUrl() (SupportedCultures.HelpCulture 駆動) に統一。
        Process.Start(new ProcessStartInfo(HelpBaseUrl()) { UseShellExecute = true });
        // 260602Cl 旧コード (コメントアウト保存):
        //Process.Start(new ProcessStartInfo(
        //    Thread.CurrentThread.CurrentUICulture.Name == "ja"
        //        ? "https://seto77.github.io/ReciPro/ja/"
        //        : "https://seto77.github.io/ReciPro/") { UseShellExecute = true });

        // 260602Cl 旧コード (コメントアウト保存):
        //if (Language != Languages.English)
        //    Process.Start(new ProcessStartInfo("https://yseto.net/soft/recipro/") { UseShellExecute = true });
        //else
        //{
        //    var fn = "\\doc\\ReciProManual(" + (Language == Languages.English ? "en" : "ja") + ").pdf";
        //    var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //    var f = new FormPDF(appPath + fn) { Text = "ReciPro manual" };
        //    f.Show();
        //}
    }
    private void hintToolStripMenuItem_Click(object sender, EventArgs e)
    {
        commonDialog.DialogMode = Crystallography.Controls.CommonDialog.DialogModeEnum.Hint;
        commonDialog.Visible = true;

    }
    private void versionHistoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
        commonDialog.DialogMode = Crystallography.Controls.CommonDialog.DialogModeEnum.History;
        commonDialog.Visible = true;
    }

    private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
    {
        commonDialog.DialogMode = Crystallography.Controls.CommonDialog.DialogModeEnum.License;
        commonDialog.Visible = true;
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

    private void toolTipToolStripMenuItem_CheckedChanged(object sender, EventArgs e) => toolTip.Active = toolTipToolStripMenuItem.Checked;
    private void toolStripMenuItem1_Click(object sender, EventArgs e) => listBox.Items.Clear();
    private void toolStripMenuItemExportCIF_Click(object sender, EventArgs e) => ExportCIF();

    public void ExportCIF(string filename = "")
    {
        if (filename == "")
        {
            var dlg = new SaveFileDialog { Filter = " *.cif| *.cif", FileName = Crystal.Name + ".cif" };
            if (dlg.ShowDialog() == DialogResult.OK)
                filename = dlg.FileName;
            else
                return;
        }
        Crystal.ExportCIF(filename);
    }


    private void languageToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // 260617Cl 変更: english/japanese 二択固定から、項目の Tag (CultureInfo 名) 駆動の N 言語対応へ (Phase 0)。
        // 切替は CurrentUICulture を更新するのみ (構築済みフォームは再ローカライズしない=従来通り再起動方式)。
        // 旧:
        //   englishToolStripMenuItem.Checked = ((ToolStripMenuItem)sender).Name.Contains("english");
        //   japaneseToolStripMenuItem.Checked = !englishToolStripMenuItem.Checked;
        //   Thread.CurrentThread.CurrentUICulture = englishToolStripMenuItem.Checked ? new System.Globalization.CultureInfo("en") : new System.Globalization.CultureInfo("ja");
        // 260623Cl 変更: 「再起動が必要」の手動再起動から自動再起動方式へ (作者承認・方針 §4-E)。
        //   構築済みフォームの live re-localize は ApplyResources/コード側 Loc/フォント/AutoScale の全再適用が要り
        //   投資対効果が悪いため採らない。代わりに、選択カルチャを CurrentUICulture へ入れてから Application.Restart()
        //   で自分を再起動する。Restart は FormClosing を発火 → Registry(Reg.Mode.Write) が CurrentUICulture.Name を
        //   永続化 → 再起動後の Registry(Reg.Mode.Read) が新言語を復元する、という既存の保存/復元経路にそのまま乗る。
        var culture = Crystallography.SupportedCultures.Resolve(LanguageMenuItemCulture((ToolStripMenuItem)sender));

        // 既に現在の言語を選んだだけなら何もしない (再クリックでの無用な再起動を防ぐ)。
        if (culture.Name == Crystallography.SupportedCultures.Current.Name)
        {
            UpdateLanguageMenuChecks(culture.Name);
            return;
        }

        // 切替には再起動が要る (作業中の状態は失われる) ことを、まだ切り替わっていない現在の言語で確認する。
        var msg = Crystallography.Localization.Loc(
            en: $"Switching the display language to \"{culture.NativeName}\" requires restarting ReciPro.\nUnsaved work will be lost. Restart now?",
            ja: $"表示言語を「{culture.NativeName}」に切り替えるには ReciPro の再起動が必要です。\n保存していない作業は失われます。今すぐ再起動しますか？");
        if (MessageBox.Show(this, msg, Crystallography.Localization.Loc(en: "Change language", ja: "言語の変更"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            // キャンセル: メニューのチェックを現在の言語へ戻す (新言語に先走ってチェックされたままにしない)。
            UpdateLanguageMenuChecks(Crystallography.SupportedCultures.Current.Name);
            return;
        }

        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture.Name);
        UpdateLanguageMenuChecks(culture.Name);
        RestartApplicationForLanguageChange(); // 260625Ch Application.Restart() に頼らず、先に新プロセスを起動してから終了する
        //Application.Restart(); // 260625Ch 旧: FormClosing→Registry(Write) 頼みだと終了後に新プロセスが起動しない環境があった
        // 旧 (260617Cl): 再起動せず CurrentUICulture を更新するのみ → 構築済みフォームは英語のままで手動再起動が必要だった。
    }

    private void RestartApplicationForLanguageChange()
    {
        // 260625Ch 追加: 新プロセスが起動直後に新言語を読めるよう、FormClosing を待たずに言語値を先行保存する。
        // ここで CurrentUICulture は既に新言語へ切替済み。後続の FormClosing でも同じ値が保存されるため、言語値は競合しない。
        Registry(Reg.Mode.Write);

        var startInfo = new ProcessStartInfo
        {
            FileName = !string.IsNullOrEmpty(Application.ExecutablePath) ? Application.ExecutablePath : Environment.ProcessPath,
            WorkingDirectory = Environment.CurrentDirectory,
            UseShellExecute = true,
        };

        try
        {
            if (Process.Start(startInfo) == null)
                throw new InvalidOperationException("Process.Start returned null.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this,
                Crystallography.Localization.Loc(
                    en: $"Failed to restart ReciPro.\n{ex.Message}",
                    ja: $"ReciPro の再起動に失敗しました。\n{ex.Message}"),
                Crystallography.Localization.Loc(en: "Change language", ja: "言語の変更"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Close();
    }

    // 260617Cl 追加: 言語メニュー各項目のチェックを現在カルチャに合わせて更新する (Load 時と切替時で共用。Phase 0)。
    private void UpdateLanguageMenuChecks(string currentName)
    {
        foreach (ToolStripItem it in languageToolStripMenuItem.DropDownItems)
            if (it is ToolStripMenuItem mi)
                mi.Checked = LanguageMenuItemCulture(mi) == currentName;
    }

    // 260618Cl 追加: 言語メニュー項目を中央 allow-list (SupportedCultures) から動的生成する (多言語化 §11.4 step6)。
    //   Released=true の言語だけを各々の自言語表記 (NativeName) で並べ、Tag に CultureInfo 名を入れて
    //   既存の Tag 駆動の切替 (languageToolStripMenuItem_Click) / チェック更新 (UpdateLanguageMenuChecks) に乗せる。
    //   → 言語を増やすときは SupportedCultures.cs で Released=true にするだけでメニューに出る (Designer 編集不要)。
    //   260623Cl: 切替は自動再起動方式になった (languageToolStripMenuItem_Click が確認の上 Application.Restart())。
    //   そのため各項目は自言語表記のみにする (旧: "(requires restart)" 注記付き → 再起動の案内は確認ダイアログへ移譲)。
    private void PopulateLanguageMenu()
    {
        languageToolStripMenuItem.DropDownItems.Clear();
        foreach (var c in Crystallography.SupportedCultures.All)
        {
            if (!c.Released)
                continue;
            //260623Cl 旧: var item = new ToolStripMenuItem($"{c.NativeName} (requires restart)") { ... };
            var item = new ToolStripMenuItem(c.NativeName) { Tag = c.Name, Name = c.Name + "ToolStripMenuItem" };
            item.Click += languageToolStripMenuItem_Click;
            languageToolStripMenuItem.DropDownItems.Add(item);
        }
    }

    // 260617Cl 追加 / 260618Cl: 言語メニュー項目が表すカルチャ名。PopulateLanguageMenu が生成する項目は
    //   Tag (CultureInfo 名) を必ず持つ。Name からの english/japanese 解決は旧 Designer 項目向けの後方互換。
    private static string LanguageMenuItemCulture(ToolStripMenuItem item)
        => (item.Tag as string) ?? (item.Name.Contains("english") ? "en" : item.Name.Contains("japanese") ? "ja" : null);
    private void githubPageToolStripMenuItem_Click(object sender, EventArgs e)
        => Process.Start(new ProcessStartInfo("https://github.com/seto77/ReciPro") { UseShellExecute = true });
    private void reportBugsRequestsOrCommentsToolStripMenuItem1_Click(object sender, EventArgs e)
        => Process.Start(new ProcessStartInfo("https://github.com/seto77/ReciPro/issues") { UseShellExecute = true });
    // 260602Cl: githubWikiToolStripMenuItem("Help (Manual)") は helpweb("使い方(WEB)") と同一の GitHub Pages を開く重複だったため削除。helpweb 側に統合。


    //260421Cl 追加: メニューチェック変更ハンドラ (Designer からバインド)
    private void toolStripMenuItemUseMillerBravais_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePlaneIndices();
        // 現在の結晶が 4 指数非対応の場合、トグルしても描画結果は変わらないので Draw() 省略    // 260422Cl
        if (FormDiffractionSimulator != null && FormDiffractionSimulator.Visible && MillerBravaisActive)
            FormDiffractionSimulator.Draw();
    }

    // 260613Cl 追加: イオン散乱因子 (Peng IonFull) の有効/無効切替。BetheMethod.ElasticIonModel は static (全結晶の getU が参照)。
    // 動力学計算は開始時に uDictionary を Clear するが、計算せず表示だけ更新する経路 (SpotInfo 等) のため現結晶のキャッシュもここで破棄する。
    private bool ionMenuReady = false;// 起動時の Registry 復元では Σq 警告を出さないためのフラグ
    private void toolStripMenuItemIonicScattering_CheckedChanged(object sender, EventArgs e)
    {
        BetheMethod.ElasticIonModel = UseIonicScattering ? ElasticIonModel.IonFull : ElasticIonModel.Neutral;
        Crystal?.Bethe?.ClearUCache();
        // 260614Cl IonFull の g≠0 単極子は電荷中性 (Σq=0) を前提とする。非中性結晶では getU が自動的に単極子を落とし
        //   有限部分のみ (IonScreened) にフォールバックする。ここでは「そう振る舞う」ことをユーザーに知らせる (起動時の復元では出さない)。
        if (ionMenuReady && UseIonicScattering && Crystal?.Bethe != null)
        {
            double q = Crystal.Bethe.NetCellCharge();
            if (Math.Abs(q) > 1e-6)
            {
                bool ja = Thread.CurrentThread.CurrentUICulture.Name == "ja";
                MessageBox.Show(this,
                    ja ? $"単位胞の正味電荷が 0 ではありません (Σq = {q.ToString("+0.###;-0.###")})。\ng≠0 の単極子項は電荷中性 (Σq=0) を前提とするため、この結晶では単極子を加えず、割当イオンの有限部分のみ (遮蔽イオン形) で動力学計算します。"
                       : $"The unit cell is not charge-neutral (Σq = {q.ToString("+0.###;-0.###")}).\nThe g≠0 monopole term assumes a neutral cell (Σq = 0), so for this crystal the monopole is dropped and only the finite ionic parts (screened-ion form) are used in the dynamical calculation.",
                    ja ? "イオン散乱因子" : "Ionic scattering factor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    // 260428Cl 追加: ダークモード切替ハンドラ。レジストリに保存し、再起動を促す。
    // Application.SetColorMode は Run 後にも呼べるが、既存コントロールの色は完全には更新されないため、再起動推奨。
    private void toolStripMenuItemDarkMode_CheckedChanged(object sender, EventArgs e)
    {
        Program.WriteDarkMode(DarkMode);
        Application.SetColorMode(DarkMode ? SystemColorMode.Dark : SystemColorMode.Classic);
    }

    #endregion FileMenu

    #region キーストロークイベント
    private void FormMain_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.Shift && e.KeyCode == Keys.D)
            toolStripButtonDiffractionSingle.Checked = !toolStripButtonDiffractionSingle.Checked;
        else if (e.Control && e.Shift && e.KeyCode == Keys.V)
            toolStripButtonStructureViewer.Checked = !toolStripButtonStructureViewer.Checked;
        else if (e.Control && e.Shift && e.KeyCode == Keys.S)
            toolStripButtonStereonet.Checked = !toolStripButtonStereonet.Checked;
        else if (e.Control && e.Shift && e.KeyCode == Keys.T)
            toolStripButtonSpotIDv2.Checked = !toolStripButtonSpotIDv2.Checked;
        else if (e.Control)//Ctrlを素早く2回おすと計算機をだす。
            if (sw.IsRunning)
            {
                sw.Stop();
                if (sw.ElapsedMilliseconds < 100)
                    FormCalculator.Visible = !FormCalculator.Visible;
                sw.Reset();
            }

        //方向キーの制御　　Left = 37,Up = 38,Right = 39,Down = 40,
        if (e.Control && e.Shift)
        {
            //if (formStructureViewer.panelMain.Focused || formStructureViewer.panelAxes.Focused
            //    || formStereonet.panel.Focused || formElectronDiffraction.panel.Focused)
            {
                bool left = GetAsyncKeyState(37) != 0;
                bool up = GetAsyncKeyState(38) != 0;
                bool right = GetAsyncKeyState(39) != 0;
                bool down = GetAsyncKeyState(40) != 0;
                if (up && left)
                    buttonTopLeft.PerformClick();
                else if (up && right)
                    buttonTopRight.PerformClick();
                else if (down && left)
                    buttonBottomLeft.PerformClick();
                else if (down && right)
                    buttonBottomRight.PerformClick();
                else if (up)
                    buttonTop.PerformClick();
                else if (down)
                    buttonBottom.PerformClick();
                else if (right)
                    buttonRight.PerformClick();
                else if (left)
                    buttonLeft.PerformClick();
            }
        }
    }

    private void FormMain_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyValue == 17)
            sw.Start();
    }
    #endregion

    #region マクロメニュー関連
    private void editorToolStripMenuItem_Click(object sender, EventArgs e) => FormMacro.Visible = true;

    public void SetMacroToMenu(string[] name)
    {
        if (macroToolStripMenuItem.DropDownItems.Count == 1)
            macroToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        for (int i = macroToolStripMenuItem.DropDownItems.Count - 1; i > 1; i--)
            macroToolStripMenuItem.DropDownItems.RemoveAt(i);

        for (int i = 0; i < name.Length; i++)
        {
            var item = new ToolStripMenuItem(name[i]) { Name = name[i] };
            item.Click += macroMenuItem_Click;
            macroToolStripMenuItem.DropDownItems.Add(item);
        }
    }
    void macroMenuItem_Click(object sender, EventArgs e) => FormMacro.RunMacroName(((ToolStripMenuItem)sender).Name, false);

    #endregion

    #region ドラッグドロップ
    private void FormMain_DragDrop(object sender, DragEventArgs e)
    {
        var fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        if (fileName.Length == 1)
        {
            if ((fileName[0].ToLower().EndsWith("xml") || fileName[0].ToLower().EndsWith("out") || fileName[0].ToLower().EndsWith("cdb2")))
            {
                var dr = MessageBox.Show(this, "Read the list as a new list (if select 'No', add the list to the end of the present one",
                    "Option", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Cancel)
                    return;
                else if (dr == DialogResult.Yes)
                    ReadCrystalList(fileName[0], true, true);
                else
                    ReadCrystalList(fileName[0], true, false);
            }
            else if (fileName[0].ToLower().EndsWith("cif") || fileName[0].ToLower().EndsWith("amc"))
            {
                crystalControl.FormCrystal_DragDrop(sender, e);
            }
        }
    }

    private void FormMain_DragEnter(object sender, DragEventArgs e)
        => e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;

    #endregion

    #region ProgramUpdates
    //260317Cl WebClient→HttpClient (ProgramUpdates.DownloadFileWithProgressAsync使用)
    //private void checkUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
    private async void checkUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        toolStripProgressBar.Visible = true;

        //260613Cl arm64 ビルドはアーキ専用インストーラを取得する (WiX 移行 Phase C)。x64 (エミュ実行含む) は従来どおり ReciProSetup.msi
        //(var Title, var Message, var NeedUpdate, var URL, var Path) = ProgramUpdates.Check(Version.Software, Version.VersionAndDate);
        //260613Cl アセット改名 (作者決定): 新クライアントは新名称 (ReciPro-setup.msi / ReciPro-setup_arm64.msi) を明示参照する。
        //         旧名 ReciProSetup.msi は旧クライアント (installerAsset 空 = ProgramUpdates 既定の {software}Setup.msi) の
        //         自動更新互換のため release に同一バイトのコピーとして数年間併置される (release.yml)
        //var installerAsset = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "ReciProSetup-arm64.msi" : "";
        var installerAsset = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "ReciPro-setup_arm64.msi" : "ReciPro-setup.msi";
        (var Title, var Message, var NeedUpdate, var URL, var Path) = ProgramUpdates.Check(Version.Software, Version.VersionAndDate, installerAsset);

        if (!NeedUpdate)
            MessageBox.Show(Message, Title, MessageBoxButtons.OK);
        else if (MessageBox.Show(Message, Title, MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            sw.Restart();
            try
            {
                var progress = new Progress<(long Current, long Total, long ElapsedMilliseconds, string Message)>(p => ip.Report(p));
                await ProgramUpdates.DownloadFileWithProgressAsync(URL, Path, progress, sw);
                if (ProgramUpdates.Execute(Path))
                    Close();
                else
                    MessageBox.Show($"Failed to download {Path}. \r\nSorry!", "Error!");
            }
            catch
            {
                MessageBox.Show($"Failed update check. \r\nServer may be down. \r\nAccess https://github.com/seto77/{Version.Software}/releases/latest", "Error");
            }
        }

    }

    //260405Cl 追加: MKLライブラリのダウンロードと有効化
    private static readonly string[] MklFileNames = ["libMathNetNumericsMKL.dll", "libiomp5md.dll"];
    private static string MklDirectory => AppDomain.CurrentDomain.BaseDirectory;

    private static bool MklFilesExist() => MklFileNames.All(f => File.Exists(Path.Combine(MklDirectory, f)));

    private async void toolStripMenuItemUseMKL_CheckedChanged(object sender, EventArgs e)
    {
        if (toolStripMenuItemUseMKL.Checked)
        {
            if (!MklFilesExist())
            {
                var result = MessageBox.Show(
                    "MKL library is not found.\r\nDownload Intel MKL native library (~55 MB)?",
                    "Use MKL Library", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    toolStripMenuItemUseMKL.CheckedChanged -= toolStripMenuItemUseMKL_CheckedChanged;
                    toolStripMenuItemUseMKL.Checked = false;
                    toolStripMenuItemUseMKL.CheckedChanged += toolStripMenuItemUseMKL_CheckedChanged;
                    return;
                }

                toolStripProgressBar.Visible = true;
                sw.Restart();
                try
                {
                    var progress = new Progress<(long Current, long Total, long ElapsedMilliseconds, string Message)>(p => ip.Report(p));
                    await DownloadAndExtractMklAsync(progress);
                    toolStripProgressBar.Visible = false;
                    toolStripStatusLabel.Text = "MKL library downloaded successfully.";
                }
                catch (Exception ex)
                {
                    toolStripProgressBar.Visible = false;
                    toolStripMenuItemUseMKL.CheckedChanged -= toolStripMenuItemUseMKL_CheckedChanged;
                    toolStripMenuItemUseMKL.Checked = false;
                    toolStripMenuItemUseMKL.CheckedChanged += toolStripMenuItemUseMKL_CheckedChanged;
                    MessageBox.Show($"Failed to download MKL library.\r\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // MKLを有効化
            if (MathNet.Numerics.Control.TryUseNativeMKL())
            {
                BetheMethod.MklEnabled = true;
                toolStripStatusLabel.Text = "MKL library enabled.";
            }
            else
            {
                toolStripMenuItemUseMKL.CheckedChanged -= toolStripMenuItemUseMKL_CheckedChanged;
                toolStripMenuItemUseMKL.Checked = false;
                toolStripMenuItemUseMKL.CheckedChanged += toolStripMenuItemUseMKL_CheckedChanged;
                MessageBox.Show("Failed to load MKL library.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            // MKLを無効化 (managed実装にフォールバック)
            MathNet.Numerics.Control.UseManaged();
            BetheMethod.MklEnabled = false;
            toolStripStatusLabel.Text = "MKL library disabled. Using managed implementation.";
        }
    }

    private static readonly System.Net.Http.HttpClient mklHttpClient = new();

    private async Task DownloadAndExtractMklAsync(IProgress<(long Current, long Total, long ElapsedMilliseconds, string Message)> progress)
    {
        var nupkgUrl = "https://www.nuget.org/api/v2/package/MathNet.Numerics.MKL.Win-x64/3.0.0";
        var tempFile = Path.Combine(Path.GetTempPath(), "MathNet.Numerics.MKL.Win-x64.3.0.0.nupkg");

        try
        {
            // NuGetパッケージをダウンロード
            using (var response = await mklHttpClient.GetAsync(new Uri(nupkgUrl), System.Net.Http.HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var totalBytes = response.Content.Headers.ContentLength ?? -1;
                using var contentStream = await response.Content.ReadAsStreamAsync();
                using var fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
                var buffer = new byte[8192];
                long bytesRead = 0;
                int read;
                long counter = 0;
                while ((read = await contentStream.ReadAsync(buffer)) > 0)
                {
                    await fileStream.WriteAsync(buffer.AsMemory(0, read));
                    bytesRead += read;
                    if (counter++ % 10 == 0)
                        progress?.Report(ProgramUpdates.ProgressMessage(bytesRead, totalBytes, sw));
                }
            }

            // nupkgはZIPファイルなので展開してDLLを取り出す
            using var archive = System.IO.Compression.ZipFile.OpenRead(tempFile);
            foreach (var fileName in MklFileNames)
            {
                var entry = archive.GetEntry($"runtimes/win-x64/native/{fileName}");
                if (entry != null)
                {
                    var destPath = Path.Combine(MklDirectory, fileName);
                    using var entryStream = entry.Open();
                    using var destStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    entryStream.CopyTo(destStream);
                }
            }
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    /// <summary>進捗状況を更新</summary>
    /// <param name="current"></param>
    /// <param name="total"></param>
    /// <param name="elapsedMilliseconds">経過時間</param>
    /// <param name="message">メッセージ</param>
    /// <param name="interval">何回に一回更新するか</param>
    /// <param name="sleep"></param>
    /// <param name="showPercentage"></param>
    /// <param name="showElapsedTime"></param>
    /// <param name="showRemainTime"></param>
    // 260520Cl digit 引数を廃止 (SetProgress の正準書式に移行し未使用化。呼び出し元はタプル overload のみで未指定)
    // 旧シグネチャ: private void reportProgress(long current, long total, long elapsedMilliseconds, string message, int sleep = 0, bool showPercentage = true, bool showElapsedTime = true, bool showRemainTime = true, int digit = 1)
    private void reportProgress(long current, long total, long elapsedMilliseconds, string message,
        int sleep = 0, bool showPercentage = true, bool showElapsedTime = true, bool showRemainTime = true)
    {
        if (SkipProgressEvent || current > total)
            return;
        SkipProgressEvent = true;
        try
        {
            // 260520Cl StatusBarHelper.SetProgress に集約 (旧実装は pct/elapsed/remaining を手書き連結。digit 引数→正準書式へ移行)
            //toolStripProgressBar.Maximum = int.MaxValue;
            //var ratio = (double)current / total;
            //toolStripProgressBar.Value = (int)(ratio * toolStripProgressBar.Maximum);
            //var elapsedSec = elapsedMilliseconds / 1000.0;
            //var format = $"f{digit}";
            //if (showPercentage) message += $" Completed: {(ratio * 100).ToString(format)} %.";
            //if (showElapsedTime) message += $" Elapsed: {elapsedSec.ToString(format)} s";
            //if (showRemainTime) message += $" Remaining: {(elapsedSec / current * (total - current)).ToString(format)} s";
            //toolStripStatusLabel.Text = message;
            var ratio = total > 0 ? (double)current / total : 0;
            StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabel, ratio, message,
                showElapsedTime ? TimeSpan.FromMilliseconds(elapsedMilliseconds) : (TimeSpan?)null,
                showRemaining: showRemainTime, showPercent: showPercentage);
            // 260428Cl Application.DoEvents() を削除 (Progress<T> 経由で UI スレッドにポストされるため不要、再入の元)

            if (sleep != 0) Thread.Sleep(sleep);
        }
        catch (Exception e)
        {
            if (AssemblyState.IsDebug)
                MessageBox.Show(e.Message);
        }
        SkipProgressEvent = false;
    }
    private void reportProgress((long current, long total, long elapsedMilliseconds, string message) o)
        => reportProgress(o.current, o.total, o.elapsedMilliseconds, o.message);

    #endregion

    #region 最も近いUVWを検索
    private void labelCurrentIndex_DoubleClick(object sender, EventArgs e)
        => numericBoxMaxUVW.Visible = !numericBoxMaxUVW.Visible;
    private void numericBoxMaxUVW_ValueChanged(object sender, EventArgs e)
    {
        if (Crystal == null || Crystal.A == 0 || Crystal.B == 0 || Crystal.C == 0) return;
        if (Crystal.A_Axis == null)
            Crystal.SetAxis();

        uvwIndices.Clear();

        int limit = numericBoxMaxUVW.ValueInteger;
        for (int u = -limit; u <= limit; u++)
            for (int v = -limit + Math.Abs(u); v <= limit - Math.Abs(u); v++)
                for (int w = -limit + Math.Abs(u) + Math.Abs(v); w <= limit - Math.Abs(u) - Math.Abs(v); w++)
                {
                    //既約かどうかチェック
                    bool flag = true;
                    for (int i = 2; i <= limit / 2; i++)
                        if (u % i == 0 && v % i == 0 && w % i == 0)
                        {
                            flag = false;
                            break;
                        }
                    if ((u == 0 && v == 0 && Math.Abs(w) != 1) || (Math.Abs(u) != 1 && v == 0 && w == 0) || (u == 0 && Math.Abs(v) != 1 && w == 0))
                        flag = false;
                    if (flag)
                        uvwIndices.Add((u, v, w, (u * Crystal.A_Axis + v * Crystal.B_Axis + w * Crystal.C_Axis).Length));
                }
        SetNearestUVW();
    }

    private readonly List<(int U, int V, int W, double Length)> uvwIndices = [];

    private void SetNearestUVW()//最も近いuvwを検索
    {
        if (Crystal == null || Crystal.A == 0 || Crystal.B == 0 || Crystal.C == 0) return;
        if (Crystal.A_Axis == null)
            Crystal.SetAxis();

        if (uvwIndices.Count == 0)
            numericBoxMaxUVW_ValueChanged(new object(), new EventArgs());
        else
        {
            double aZ = (Crystal.RotationMatrix * Crystal.A_Axis).Z, bZ = (Crystal.RotationMatrix * Crystal.B_Axis).Z, cZ = (Crystal.RotationMatrix * Crystal.C_Axis).Z;
            var (U, V, W, _) = uvwIndices.MaxBy(e => (e.U * aZ + e.V * bZ + e.W * cZ) / e.Length);

            CurrentZoneAxis = $"[{U} {V} {W}]";
            labelCurrentIndex.Text = CurrentZoneAxis;
        }
    }
    #endregion

    #region Miller-Bravais (4 指数) 表示の同期

    //260421Cl 追加: UseMillerBravais と晶系に応じて配下フォームの 4 指数表示を更新する。
    // Crystal 変更時 / メニューチェック変更時 などから呼ぶ。
    //260422Cl FormMain/FormMovie の Plane 入力は NumericBox ベースに revert したため、
    // 現状 4 指数表示の切替対象は FormDiffractionSpotInfo の i 列のみ。
    //260425Cl FormStereonet / FormImageSimulator も同期対象に追加
    public void UpdatePlaneIndices()
    {
        var active = MillerBravaisActive;
        FormDiffractionSimulator?.UpdatePlaneIndices();
        // 260517Cl 削除: FormStructureViewer.UpdatePlaneIndices() は no-op だったため。crystalControl.MillerBravais の同期で十分。
        FormStereonet?.UpdatePlaneIndices();
        FormImageSimulator?.UpdatePlaneIndices();
        if (FormMovie != null) FormMovie.MillerBravaisIndex = active; // 260517Cl 追加: FormMovie の i 軸表示も同期させる
        crystalControl.MillerBravais = active;

        indexControlPlane.MillerBravais = active;
        indexControlPlane.BoxWidth = active ? (int)(indexControlAxis.BoxWidth * 0.8) : indexControlAxis.BoxWidth;      // 260424Cl 4指数表示時はI列分の幅を確保するため H/K/L を狭める
        indexControlPlane.UpDownWidth = active ? (int)(indexControlAxis.UpDownWidth * 0.8) : indexControlAxis.UpDownWidth;
    }


    #endregion

    #region 晶帯軸/結晶面 設定
    private void checkBoxFixAxis_CheckedChanged(object sender, EventArgs e)
    {
        if (indexControlAxis.IsZero)
        {
            checkBoxFixAxis.Checked = false;
            return;
        }
        if (checkBoxFixAxis.Checked)
            checkBoxFixePlane.Checked = false;
    }

    private void checkBoxFixPlane_CheckedChanged(object sender, EventArgs e)
    {
        if (indexControlPlane.IsZero)
        {
            checkBoxFixePlane.Checked = false;
            return;
        }
        if (checkBoxFixePlane.Checked)
            checkBoxFixAxis.Checked = false;
    }

    #endregion


}


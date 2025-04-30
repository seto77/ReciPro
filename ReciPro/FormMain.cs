#region using
using Crystallography.OpenGL;
using IronPython.Hosting;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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

public partial class FormMain : Form
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
                    var c2 = Crystal2.Deserialize(bytes);
                    crystalControl.Crystal = Crystal2.GetCrystal(c2);
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

    public FormStructureViewer FormStructureViewer;
    public FormDiffractionSimulator FormDiffractionSimulator;
    public FormStereonet FormStereonet;
    public FormSpotIDv1 FormTEMID;
    public FormSpotIDV2 FormSpotID;
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
    public bool DisableOpenGL { get => disableOpneGLToolStripMenuItem.Checked; set => disableOpneGLToolStripMenuItem.Checked = value; }
    public bool DisableTextRendering { get => toolStripMenuItemDisableTextRendering.Checked; set => toolStripMenuItemDisableTextRendering.Checked = value; }
    public static Languages Language => Thread.CurrentThread.CurrentUICulture.Name == "en" ? Languages.English : Languages.Japanese;
    public double Phi { get => (double)numericUpDownEulerPhi.Value / 180.0 * Math.PI; set => numericUpDownEulerPhi.Value = (decimal)(value / Math.PI * 180.0); }
    public double Theta { get => (double)numericUpDownEulerTheta.Value / 180.0 * Math.PI; set => numericUpDownEulerTheta.Value = (decimal)(value / Math.PI * 180.0); }
    public double Psi { get => (double)numericUpDownEulerPsi.Value / 180.0 * Math.PI; set => numericUpDownEulerPsi.Value = (decimal)(value / Math.PI * 180.0); }

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

    #endregion

    #region コンストラクト、ロード

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public FormMain()
    {
        //カルチャーを決めるため、レジストリ読込 (InitializeComponentの前に読み込む)
        if (!DesignMode)
            Registry(Reg.Mode.Read);


        InitializeComponent();

        if (DesignMode)
            return;

        //MainWindowの場所を読み込むため (InitializeComponentの後にに読み込む)
        Registry(Reg.Mode.Read);

        sw.Restart();

        ip = new Progress<(long, long, long, string)>(reportProgress);//IReport

        this.SetStyle(ControlStyles.ResizeRedraw, true);
        // ダブルバッファリング
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

    }

    /// <summary>
    /// フォームロード時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FormMain_Load(object sender, EventArgs e)
    {
        if (DesignMode) return;

        englishToolStripMenuItem.Checked = Thread.CurrentThread.CurrentUICulture.Name != "ja";
        japaneseToolStripMenuItem.Checked = Thread.CurrentThread.CurrentUICulture.Name == "ja";

        commonDialog = new Crystallography.Controls.CommonDialog
        {
            Owner = this,
            DialogMode = Crystallography.Controls.CommonDialog.DialogModeEnum.Initialize,
            Software = Version.Software,
            VersionAndDate = Version.VersionAndDate,
            Author = Version.Author,
            History = Version.History,
            Hint = Version.Hint,
            Width = 600,
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
                glControlAxes.MouseDown += new MouseEventHandler(panelAxes_MouseDown);
                glControlAxes.MouseMove += new MouseEventHandler(panelAxes_MouseMove);
                groupBoxCurrentDirection.Controls.Add(glControlAxes);
                //glControlAxes.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during initializing GLcontrol");
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
        FormTEMID = new FormSpotIDv1 { formMain = this, Visible = false };
        FormTEMID.KeyDown += new KeyEventHandler(FormMain_KeyDown);
        FormTEMID.KeyUp += new KeyEventHandler(FormMain_KeyUp);
        FormTEMID.Visible = false;
        FormTEMID.VisibleChanged += FormTEMID_VisibleChanged;

        commonDialog.Progress = ("Now Loading...Initializing 'Spot ID' form.", 0.65);
        FormSpotID = new FormSpotIDV2 { FormMain = this, Visible = false };

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
        var appPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\";
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
        //StdDbをコピー
        File.Copy(appPath + "StdDb.cdb3", UserAppDataPath + "StdDb.cdb3", true);

        //UserAppDataPathに空フォルダがあったら削除
        foreach (var dir in Directory.GetDirectories(UserAppDataPath))
            if (!Directory.EnumerateFileSystemEntries(dir).Any())
                Directory.Delete(dir);


        commonDialog.Progress = ("Now Loading...Reading registries again.", 0.98);

        //ReadInitialRegistry();
        Registry(Reg.Mode.Read);

        Text = "ReciPro  " + Version.VersionAndDate;
        if (glControlAxes == null)
            Text += "  (3D rendering disable mode)";

        commonDialog.Progress = ("Initializing has been finished successfully. You can close this window.", 1.0);
        if (commonDialog.AutomaticallyClose)
            commonDialog.Visible = false;

        toolStripStatusLabel.Text = $"Startup time: {sw.ElapsedMilliseconds:#,0} ms.";

        if (disableOpneGLToolStripMenuItem.Checked)
        {
            toolStripButtonStructureViewer.Enabled = false;
            toolStripButtonRotation.Enabled = false;
            if (glControlAxes != null)
                glControlAxes.Visible = false;
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
                    Thread.Sleep(100);
                    Application.DoEvents();
                    Thread.Sleep(100);
                    var reader = new StreamReader(filename, Encoding.GetEncoding("UTF-8"));
                    FormMacro.RunMacro(reader.ReadToEnd());
                }
            }

            if (args.Contains("/x"))//xがあると、実行後に閉じる
                Close();
        }
    }

    /// <summary>
    /// クローズ時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        //FormCalculator.Close();
        //FormStereonet.Close();
        //FormStructureViewer.Close();
        //FormDiffractionSimulator.Close();
        e.Cancel = false;
        //SaveInitialRegistry();
        Registry(Reg.Mode.Write);

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

            Reg.RW<string>(key, mode, Thread.CurrentThread.CurrentUICulture, "Name");

            Reg.RW<Rectangle>(key, mode, this, "Bounds");
            WindowLocation.Adjust(this);

            Reg.RW<bool>(key, mode, this, "DisableOpenGL");
            Reg.RW<bool>(key, mode, this, "DisableTextRendering");

            if (commonDialog == null)
                return;

            Reg.RW<bool>(key, mode, commonDialog, "AutomaticallyClose");

            if (FormStereonet == null)
                return;

            Reg.RW<Rectangle>(key, mode, FormStereonet, "Bounds");

            Reg.RW<Rectangle>(key, mode, FormTEMID, "Bounds");

            #region DiffractionSimulator

            FormDiffractionSimulator.CancelSetVector = true;

            Reg.RW<Rectangle>(key, mode, FormDiffractionSimulator, "Bounds");


            Reg.RW<WaveSource>(key, mode, FormDiffractionSimulator.waveLengthControl, "WaveSource");
            Reg.RW<double>(key, mode, FormDiffractionSimulator.waveLengthControl, "Energy");
            Reg.RW<int>(key, mode, FormDiffractionSimulator.waveLengthControl, "XrayWaveSourceElementNumber");
            Reg.RW<XrayLine>(key, mode, FormDiffractionSimulator.waveLengthControl, "XrayWaveSourceLine");

            FormDiffractionSimulator.CancelSetVector = false;

            Reg.RW<double>(key, mode, FormDiffractionSimulator.FormDiffractionSimulatorGeometry, "FootX");
            Reg.RW<double>(key, mode, FormDiffractionSimulator.FormDiffractionSimulatorGeometry, "FootY");
            Reg.RW<double>(key, mode, FormDiffractionSimulator.FormDiffractionSimulatorGeometry, "CameraLength2");
            Reg.RW<int>(key, mode, FormDiffractionSimulator.FormDiffractionSimulatorGeometry, "DetectorWidth");
            Reg.RW<int>(key, mode, FormDiffractionSimulator.FormDiffractionSimulatorGeometry, "DetectorHeight");
            Reg.RW<double>(key, mode, FormDiffractionSimulator.FormDiffractionSimulatorGeometry, "Tau");
            Reg.RW<double>(key, mode, FormDiffractionSimulator.FormDiffractionSimulatorGeometry, "Phi");

            Reg.RW<double>(key, mode, FormDiffractionSimulator, "Resolution");
            Reg.RW<LengthUnitEnum>(key, mode, FormDiffractionSimulator, "ResolutionUnit");

            Reg.RW<bool>(key, mode, FormDiffractionSimulator, "FlipHorizontally");
            Reg.RW<bool>(key, mode, FormDiffractionSimulator, "FlipVertically");
            Reg.RW<bool>(key, mode, FormDiffractionSimulator, "NegativeImage");

            #endregion

            #region ImageSimulator
            Reg.RW<Rectangle>(key, mode, FormImageSimulator, "Bounds");
            Reg.RW<ImageSimulatorSetting>(key, mode, FormImageSimulator, "Setting");
            Reg.RW<ImageSimulatorSetting[]>(key, mode, this.FormImageSimulator.FormPresets, "Settings");
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

    /// <summary>
    /// 回転量と回転角度を指定して、全フォームに回転命令を出す
    /// </summary>
    /// <param name="axis"></param>
    /// <param name="angle"></param>
    public void Rotate((double X, double Y, double Z) axis, double angle) => Rotate(new Vector3DBase(axis.X, axis.Y, axis.Z), angle);

    /// <summary>
    /// 回転量と回転角度を指定して、全フォームに回転命令を出す
    /// </summary>
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
                var newAxis = checkBoxFixAxis.Checked ?
                     Crystals[i].RotationMatrix * (numericBoxAxisU.Value * Crystal.A_Axis + numericBoxAxisV.Value * Crystal.B_Axis + numericBoxAxisW.Value * Crystal.C_Axis) :
                     Crystals[i].RotationMatrix * (numericBoxPlaneH.Value * Crystal.A_Star + numericBoxPlaneK.Value * Crystal.B_Star + numericBoxPlaneL.Value * Crystal.C_Star);
                if (Vector3DBase.AngleBetVectors(newAxis, axis) < Math.PI / 2)
                    rot = Matrix3D.Rot(newAxis, angle);
                else
                    rot = Matrix3D.Rot(newAxis, -angle);
            }
            Crystals[i].RotationMatrix = rot * Crystals[i].RotationMatrix;
        }
        SetRotation(Crystals[0].RotationMatrix);
    }

    /// <summary>
    /// 回転行列を指定して、全フォームの回転状態をセットする
    /// </summary>
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
            FormDiffractionSimulator.Draw();

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
            numericUpDownEulerPhi.Value = (decimal)(euler.Phi / Math.PI * 180);
            numericUpDownEulerTheta.Value = (decimal)(euler.Theta / Math.PI * 180);
            numericUpDownEulerPsi.Value = (decimal)(euler.Psi / Math.PI * 180);
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
            toolStripStatusLabel.Text = $"Frame rate: {1000.0 / differenceTime:f1} frm/sec.";
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

    public void SetPlane(int h, int k, int l)
    {
        numericBoxPlaneH.Value = h;
        numericBoxPlaneK.Value = k;
        numericBoxPlaneL.Value = l;
    }
    public void SetAxis(int u, int v, int w)
    {
        numericBoxAxisU.Value = u;
        numericBoxAxisV.Value = v;
        numericBoxAxisW.Value = w;
    }
    public void ProjectAlongAxis() => buttonSetVector_Click(buttonSetAxis, new EventArgs());
    public void ProjectAlongPlane() => buttonSetVector_Click(buttonSetPlane, new EventArgs());

    private void buttonSetVector_Click(object sender, EventArgs e)
    {
        if (Crystal == null) return;
        double u = numericBoxAxisU.Value, v = numericBoxAxisV.Value, w = numericBoxAxisW.Value;
        double h = numericBoxPlaneH.Value, k = numericBoxPlaneK.Value, l = numericBoxPlaneL.Value;

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

    /// <summary>
    /// オイラー角の入力ボックスからの変更イベント
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericUpDownEulerAngle_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEulerChange) return;
        SkipEulerChange = true;
        if (numericUpDownEulerPhi.Value > 180)
            numericUpDownEulerPhi.Value -= 360;
        if (numericUpDownEulerPhi.Value < -180)
            numericUpDownEulerPhi.Value += 360;

        if (numericUpDownEulerTheta.Value > 180)
            numericUpDownEulerTheta.Value -= 360;
        if (numericUpDownEulerTheta.Value < -180)
            numericUpDownEulerTheta.Value += 360;

        if (numericUpDownEulerPsi.Value > 180)
            numericUpDownEulerPsi.Value -= 360;
        if (numericUpDownEulerPsi.Value < -180)
            numericUpDownEulerPsi.Value += 360;

        var phi = (double)numericUpDownEulerPhi.Value / 180.0 * Math.PI;
        var theta = (double)numericUpDownEulerTheta.Value / 180.0 * Math.PI;
        var psi = (double)numericUpDownEulerPsi.Value / 180.0 * Math.PI;

        SetRotation(Matrix3D.Rot(phi, theta, psi));

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
    private void FormTEMID_VisibleChanged(object sender, EventArgs e)
    {
        listBox.SelectionMode = FormTEMID.Visible || FormDiffractionSimulator.Visible || FormPolycrystallineDiffractionSimulator.Visible ?
            SelectionMode.MultiExtended : SelectionMode.One;
    }

    private void FormElectronDiffraction_VisibleChanged(object sender, EventArgs e)
    {
        listBox.SelectionMode = FormTEMID.Visible || FormDiffractionSimulator.Visible || FormPolycrystallineDiffractionSimulator.Visible ?
            SelectionMode.MultiExtended : SelectionMode.One;
    }

    private void formPolycrystallineDiffractionSimulator_VisibleChanged(object sender, EventArgs e)
    {
        listBox.SelectionMode = FormTEMID.Visible || FormDiffractionSimulator.Visible || FormPolycrystallineDiffractionSimulator.Visible ?
            SelectionMode.MultiExtended : SelectionMode.One;
    }

    private void crystalControl_ScatteringFactor_VisibleChanged(object sender, EventArgs e) => toolStripButtonScatteringFactor.Checked = crystalControl.FormScatteringFactor.Visible;

    private void CrystalControl_SymmetryInformation_VisibleChanged(object sender, EventArgs e) => toolStripButtonSymmetryInformation.Checked = crystalControl.FormSymmetryInformation.Visible;

    /// <summary>
    /// ToolStripボタンを押されたら、各機能を起動/終了する
    /// </summary>
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
        else if (button.Name.Contains("Scattering"))
            form = crystalControl.FormScatteringFactor;
        else if (button.Name.Contains("Rotation"))
            form = FormRotation;
        else if (button.Name.Contains("Stereonet"))
            form = FormStereonet;
        else if (button.Name.Contains("DiffractionSingle"))
            form = FormDiffractionSimulator;
        else if (button.Name.Contains("ImageSimulator"))
            form = FormImageSimulator;
        else if (button.Name.Contains("SpotIDv1"))
            form = FormTEMID;
        else if (button.Name.Contains("SpotIDv2"))
            form = FormSpotID;
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

    async private void toolStripButtonElectronDiffraction_CheckedChanged(object sender, EventArgs e)
    {
        //Electron diffractionを表示した直後、なぜかグラフィックボックスがグレーになってしまうので、その対処.
        await Task.Delay(200);
        if (FormDiffractionSimulator.Visible)
            FormDiffractionSimulator.Draw();
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
            numericUpDownEulerPhi.Value = (decimal)(euler.Phi / Math.PI * 180);
            numericUpDownEulerTheta.Value = (decimal)(euler.Theta / Math.PI * 180);
            numericUpDownEulerPsi.Value = (decimal)(euler.Psi / Math.PI * 180);
            SkipEulerChange = false;

            numericBoxMaxUVW_ValueChanged(sender, e);

            if (SkipDrawing) return;

            if (FormStructureViewer.Visible)
                FormStructureViewer.SetGLObjects(crystalControl.Crystal);
            if (FormStereonet.Visible)
                FormStereonet.SetCrystal();
            if (FormDiffractionSimulator.Visible)
                FormDiffractionSimulator.SetCrystal();
            if (FormSpotID.Visible)
                FormSpotID.SetCrystal();
            if (FormRotation.Visible)
                FormRotation.SetRotation();
            if (FormImageSimulator.Visible)
                FormImageSimulator.RotationChanged();

            if (FormEBSD.Visible)
                FormEBSD.SetCrystal();

            ResetAxes();
        }
    }
    #endregion

    #region リストボックス関連

    private void ButtonUpper_Click(object sender, EventArgs e) => MoveUp();

    public void MoveUp()
    {
        var n = listBox.SelectedIndex;
        if (n <= 0) return;
        object o = listBox.SelectedItem;
        listBox.Items.Remove(listBox.SelectedItem);
        listBox.Items.Insert(n - 1, o);
        listBox.SelectedIndex = n - 1;
        Application.DoEvents();
    }

    private void ButtonLower_Click(object sender, EventArgs e) => MoveDown();
    public void MoveDown()
    {
        int n = listBox.SelectedIndex;
        if (n >= listBox.Items.Count - 1) return;
        object o = listBox.SelectedItem;
        listBox.Items.Remove(listBox.SelectedItem);
        listBox.Items.Insert(n + 1, o);
        listBox.SelectedIndex = n + 1;
        Application.DoEvents();
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
        Application.DoEvents();
    }

    private void buttonDuplicate_Click(object sender, EventArgs e) => DuplicateCrystal();

    public void DuplicateCrystal()
    {
        if (listBox.SelectedIndex < 0) return;
        var c = (Crystal)listBox.SelectedItem;
        var newCrystal = new Crystal(c);

        var num = 0;

        var index = newCrystal.Name.LastIndexOf(" #");
        if (index >= 0 && int.TryParse(newCrystal.Name[(index+2)..], out num))
            newCrystal.Name = newCrystal.Name[0..(index+2)] + (num + 1).ToString();
        else
            newCrystal.Name += " #1";

        listBox.Items.Insert(listBox.SelectedIndex + 1, newCrystal);
        listBox.SelectedIndex++;
        Application.DoEvents();
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
            Application.DoEvents();
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

    private void listBox_MouseDown(object sender, MouseEventArgs e)
    {
        if(renameTextBox != null && groupBoxCrystalList.Controls.Contains(renameTextBox))
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

    /// <summary>
    /// 選択結晶をCIF形式で保存
    /// </summary>
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
                MessageBox.Show("ファイルが書き込みません");
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

    private void helpwebToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Language != Languages.English)
            Process.Start(new ProcessStartInfo("https://yseto.net/soft/recipro/") { UseShellExecute = true });
        else
        {
            var fn = "\\doc\\ReciProManual(" + (Language == Languages.English ? "en" : "ja") + ").pdf";
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var f = new FormPDF(appPath + fn) { Text = "ReciPro manual" };
            f.Show();
        }
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
        englishToolStripMenuItem.Checked = ((ToolStripMenuItem)sender).Name.Contains("english");
        japaneseToolStripMenuItem.Checked = !englishToolStripMenuItem.Checked;
        Thread.CurrentThread.CurrentUICulture = englishToolStripMenuItem.Checked ? new System.Globalization.CultureInfo("en") : new System.Globalization.CultureInfo("ja");
    }
    private void githubPageToolStripMenuItem_Click(object sender, EventArgs e)
        => Process.Start(new ProcessStartInfo("https://github.com/seto77/ReciPro") { UseShellExecute = true });
    private void reportBugsRequestsOrCommentsToolStripMenuItem1_Click(object sender, EventArgs e)
        => Process.Start(new ProcessStartInfo("https://github.com/seto77/ReciPro/issues") { UseShellExecute = true });

    private void githubWikiToolStripMenuItem_Click(object sender, EventArgs e)
        => Process.Start(new ProcessStartInfo("https://github.com/seto77/ReciPro/wiki") { UseShellExecute = true });

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
    private void checkUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        toolStripProgressBar.Visible = true;

        (var Title, var Message, var NeedUpdate, var URL, var Path) = ProgramUpdates.Check(Version.Software, Version.VersionAndDate);

        if (!NeedUpdate)
            MessageBox.Show(Message, Title, MessageBoxButtons.OK);
        else if (MessageBox.Show(Message, Title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            using (var wc = new WebClient())
            {
                long counter = 1;
                wc.DownloadProgressChanged += (s, ev) =>
                {
                    if (counter++ % 10 == 0)
                        ip.Report(ProgramUpdates.ProgressMessage(ev, sw));
                };

                wc.DownloadFileCompleted += (s, ev) =>
                {
                    if (ProgramUpdates.Execute(Path))
                        Close();
                    else
                        MessageBox.Show($"Failed to download {Path}. \r\nSorry!", "Error!");
                };
                sw.Restart();
                try
                {
                    wc.DownloadFileAsync(new Uri(URL), Path);
                }
                catch
                {
                    MessageBox.Show($"Failed update check. \r\nServer may be down. \r\nAccess https://github.com/seto77/{Version.Software}/releases/latest", "Error");
                }
            }

    }


    /// <summary>
    /// 進捗状況を更新
    /// </summary>
    /// <param name="current"></param>
    /// <param name="total"></param>
    /// <param name="elapsedMilliseconds">経過時間</param>
    /// <param name="message">メッセージ</param>
    /// <param name="interval">何回に一回更新するか</param>
    /// <param name="sleep"></param>
    /// <param name="showPercentage"></param>
    /// <param name="showElapsedTime"></param>
    /// <param name="showRemainTime"></param>
    /// <param name="digit"></param>
    private void reportProgress(long current, long total, long elapsedMilliseconds, string message,
        int sleep = 0, bool showPercentage = true, bool showElapsedTime = true, bool showRemainTime = true, int digit = 1)
    {
        if (SkipProgressEvent || current > total)
            return;
        SkipProgressEvent = true;
        try
        {
            toolStripProgressBar.Maximum = int.MaxValue;
            var ratio = (double)current / total;
            toolStripProgressBar.Value = (int)(ratio * toolStripProgressBar.Maximum);
            var ellapsedSec = elapsedMilliseconds / 1000.0;
            var format = $"f{digit}";

            if (showPercentage) message += $" Completed: {(ratio * 100).ToString(format)} %.";
            if (showElapsedTime) message += $" Elapsed: {ellapsedSec.ToString(format)} s.";
            if (showRemainTime) message += $" Remaining: {(ellapsedSec / current * (total - current)).ToString(format)} s.";

            toolStripStatusLabel.Text = message;

            Application.DoEvents();

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
    {
        numericBoxMaxUVW.Visible = !numericBoxMaxUVW.Visible;
    }
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

    #region 晶帯軸/結晶面 設定
    private void checkBoxFixAxis_CheckedChanged(object sender, EventArgs e)
    {
        if (numericBoxAxisU.Value == 0 && numericBoxAxisV.Value == 0 && numericBoxAxisW.Value == 0)
        {
            checkBoxFixAxis.Checked = false;
            return;
        }
        if (checkBoxFixAxis.Checked)
            checkBoxFixePlane.Checked = false;
    }

    private void checkBoxFixPlane_CheckedChanged(object sender, EventArgs e)
    {
        if (numericBoxPlaneH.Value == 0 && numericBoxPlaneK.Value == 0 && numericBoxPlaneL.Value == 0)
        {
            checkBoxFixePlane.Checked = false;
            return;
        }
        if (checkBoxFixePlane.Checked)
            checkBoxFixAxis.Checked = false;
    }
    #endregion





}

#region using
using Crystallography;
using Crystallography.Controls;
using Crystallography.Controls.Numeric;
using Crystallography.OpenGL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Col4 = OpenTK.Graphics.Color4;
using Vec3 = OpenTK.Vector3d;

#endregion

namespace ReciPro
{
    public partial class FormMain : Form
    {
        [DllImport("user32")]
        private static extern short GetAsyncKeyState(int nVirtKey);

        #region WebClientの派生クラス
        private static readonly HttpClient httpClient = new HttpClient();

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
                    if ((Clipboard.GetDataObject()).GetDataPresent(typeof(Crystal2)))
                    {
                        var data = Clipboard.GetDataObject();
                        var c2 = (Crystal2)data.GetData(typeof(Crystal2));
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

        public FormStructureViewer FormStructureViewer;
        public FormDiffractionSimulator FormDiffractionSimulator;
        public FormStereonet FormStereonet;
        public FormTEMID FormTEMID;
        public FormSpotID FormSpotID;
        public FormCalculator FormCalculator;
        public FormPolycrystallineDiffractionSimulator FormPolycrystallineDiffractionSimulator;
        public FormRotationMatrix FormRotation;
        public FormImageSimulator FormImageSimulator;
        public FormCrystalDatabase FormCrystalDatabase;
        private Crystallography.Controls.CommonDialog commonDialog;
        private GLControlAlpha glControlAxes;

        public static Languages Language { get => Thread.CurrentThread.CurrentUICulture.Name=="en" ? Languages.English : Languages.Japanese; }
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
                    return new[] { Crystal };
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
        public bool YusaGonioMode { get; set; } = false;

        private readonly Stopwatch sw = new();
        public bool SkipDrawing { get; set; } = false;

        #endregion

        #region コンストラクト、ロード

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMain()
        {
            if (DesignMode)
                return;
            
            sw.Restart();

            var regKey = Registry.CurrentUser.CreateSubKey("Software\\Crystallography\\ReciPro");
            try
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                    regKey.SetValue("DisableOpenGL", true);

                var culture = (string)regKey.GetValue("Culture", Thread.CurrentThread.CurrentUICulture.Name);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture.ToLower().StartsWith("ja") ? "ja" : "en");

            }
            catch { }
            InitializeComponent();
            ip = new Progress<(long, long, long, string)>(o => reportProgress(o));//IReport
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
                History = Version.History,
                Hint = Version.Hint,
            };

            commonDialog.Show();

            try { ReadInitialRegistry(); }
            catch { MessageBox.Show("failed reading registries."); }

            commonDialog.Progress = ("Now Loading...Initializing OpenGL.", 0.1);

            //ここでglControlコントロールを追加. Mac環境の対応のため。
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
                        ProjWidth = 2.6,
                        RotationMode = GLControlAlpha.RotationModes.Object,
                        Dock = DockStyle.Fill,
                        LightPosition = new Vec3(100, 100, 100)
                    };
                    glControlAxes.MouseDown += new MouseEventHandler(panelAxes_MouseDown);
                    glControlAxes.MouseMove += new MouseEventHandler(panelAxes_MouseMove);
                    groupBoxCurrentDirection.Controls.Add(glControlAxes);
                    glControlAxes.BringToFront();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during initializing GLcontrol");
                    MessageBox.Show(ex.Message);
                    disableOpneGLToolStripMenuItem.Checked = true;
                    var regKey = Registry.CurrentUser.CreateSubKey("Software\\Crystallography\\ReciPro");
                    regKey.SetValue("DisableOpenGL", true);
                }
            }

            commonDialog.Progress = ("Now Loading...Initializing 'Rotation' form.", 0.15);
            FormRotation = new FormRotationMatrix { FormMain = this, Visible = false };

            commonDialog.Progress = ("Now Loading...Initializing 'Structure Viewer' form.", 0.2);
            FormStructureViewer = new FormStructureViewer { formMain = this, Visible = false };
            FormStructureViewer.KeyDown += new KeyEventHandler(FormMain_KeyDown);
            FormStructureViewer.KeyUp += new KeyEventHandler(FormMain_KeyUp);

            commonDialog.Progress = ("Now Loading...Initializing 'Stereonet' form.", 0.3);
            FormStereonet = new FormStereonet { formMain = this, Visible = false };
            FormStereonet.KeyDown += new KeyEventHandler(FormMain_KeyDown);
            FormStereonet.KeyUp += new KeyEventHandler(FormMain_KeyUp);

            commonDialog.Progress = ("Now Loading...Initializing 'Crystal database' form.", 0.35);
            FormCrystalDatabase = new FormCrystalDatabase { FormMain = this, Visible = false };

            commonDialog.Progress = ("Now Loading...Initializing 'Crystal diffraction' form.", 0.4);
            FormDiffractionSimulator = new FormDiffractionSimulator { formMain = this, Visible = false };
            FormDiffractionSimulator.KeyDown += new KeyEventHandler(FormMain_KeyDown);
            FormDiffractionSimulator.KeyUp += new KeyEventHandler(FormMain_KeyUp);
            FormDiffractionSimulator.VisibleChanged += FormElectronDiffraction_VisibleChanged;

            commonDialog.Progress = ("Now Loading...Initializing 'HRTEM/STEM Image Simulator' form.", 0.45);
            FormImageSimulator = new FormImageSimulator { FormMain = this, Visible = false };

            commonDialog.Progress = ("Now Loading...Initializing 'Powder diffraction' form.", 0.5);
            FormPolycrystallineDiffractionSimulator = new FormPolycrystallineDiffractionSimulator { formMain = this, Visible = false };
            FormPolycrystallineDiffractionSimulator.VisibleChanged += formPolycrystallineDiffractionSimulator_VisibleChanged;

            commonDialog.Progress = ("Now Loading...Initializing 'TEM ID' form.", 0.6);
            FormTEMID = new FormTEMID { formMain = this, Visible = false };
            FormTEMID.KeyDown += new KeyEventHandler(FormMain_KeyDown);
            FormTEMID.KeyUp += new KeyEventHandler(FormMain_KeyUp);
            FormTEMID.Visible = false;
            FormTEMID.VisibleChanged += FormTEMID_VisibleChanged;

            commonDialog.Progress = ("Now Loading...Initializing 'Spot ID' form.", 0.7);
            FormSpotID = new FormSpotID { FormMain = this, Visible = false };

            commonDialog.Progress = ("Now Loading...Initializing 'Calculator' form.", 0.8);
            FormCalculator = new FormCalculator { Owner = this, Visible = false };
            FormCalculator.KeyDown += new KeyEventHandler(FormMain_KeyDown);
            FormCalculator.KeyUp += new KeyEventHandler(FormMain_KeyUp);
            FormCalculator.FormClosing += new FormClosingEventHandler(formCalculator_FormClosing);

            commonDialog.Progress = ("Now Loading...Initializing clipboard viewer.", 0.9);
            NextHandle = SetClipboardViewer(this.Handle);

            commonDialog.Progress = ("Now Loading...Setting CrystalChanged event.", 0.91);
            crystalControl.CrystalChanged += new EventHandler(crystalControl_CrystalChanged);

            commonDialog.Progress = ("Now Loading...Initialize Crystal class.", 0.92);
            Crystal = new Crystal();

            commonDialog.Progress = ("Now Loading...Setting default crystal list.", 0.94);
            var appPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\";
            //default.xmlをinitial.xmlとしてコピー
            //if (!File.Exists(UserAppDataPath + "initial.xml"))
            File.Copy(appPath + "initial.xml", UserAppDataPath + "initial.xml", true);

            //ユーザーパスにdefault.xmlが存在しない場合、あるいは壊れている場合は、実行フォルダのinitial.xmlをdefault.xmlとしてユーザーパスにコピー
            if (!File.Exists(UserAppDataPath + "default.xml") || new FileInfo(UserAppDataPath + "default.xml").Length < 200)
                File.Copy(appPath + "initial.xml", UserAppDataPath + "default.xml", true);

            //初期結晶リストを読み込み
            readCrystalList(UserAppDataPath + "default.xml", false, true);

            //何らかの理由(前回が不正終了だったなど)でdefalut.xmlが壊れている場合はinitial.xmlを読み込む
            if (listBox.Items.Count == 0)
                readCrystalList(UserAppDataPath + "initial.xml", false, true);

            //ReciProSetup.msiは削除
            if (File.Exists(UserAppDataPath + "ReciProSetup.msi"))
                File.Delete(UserAppDataPath + "ReciProSetup.msi");

            //StdDbをコピー
            File.Copy(appPath + "StdDb.cdb3", UserAppDataPath + "StdDb.cdb3", true);

            //UserAppDataPathに空フォルダがあったら削除
            foreach (var dir in Directory.GetDirectories(UserAppDataPath))
                if (!Directory.EnumerateFileSystemEntries(dir).Any())
                    Directory.Delete(dir);

            commonDialog.Progress = ("Now Loading...Reading registries again.", 0.98);
            ReadInitialRegistry();

            this.Text = "ReciPro  " + Version.VersionAndDate;

            commonDialog.Progress = ("Initializing has been finished successfully. You can close this window.", 1.0);
            if (commonDialog.AutomaticallyClose)
                commonDialog.Visible = false;

            toolStripStatusLabel.Text = "Startup time: " + sw.ElapsedMilliseconds + " ms.";

            if (disableOpneGLToolStripMenuItem.Checked)
            {
                toolStripButtonStructureViewer.Enabled = false;
                toolStripButtonRotation.Enabled = false;
                if (glControlAxes != null)
                    glControlAxes.Visible = false;
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
            SaveInitialRegistry();
            ChangeClipboardChain(this.Handle, NextHandle);

            var cry = new List<Crystal>();
            for (int i = 0; i < listBox.Items.Count; i++)
                cry.Add((Crystal)listBox.Items[i]);
            ConvertCrystalData.SaveCrystalListXml(cry.ToArray(), UserAppDataPath + "default.xml");
        }
        #endregion

        #region レジストリ操作

        //レジストリの読み込み
        private void ReadInitialRegistry()
        {
            object o;

            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Crystallography\\ReciPro");

            if (regKey == null) return;
            if (this != null && (int)regKey.GetValue("formMainLocationX", this.Location.X) >= 0)
            {
                disableOpneGLToolStripMenuItem.Checked = (string)regKey.GetValue("DisableOpenGL") == "True";

                this.Width = (int)regKey.GetValue("formMainWidth", this.Width);
                this.Height = (int)regKey.GetValue("formMainHeight", this.Height);
                this.Location = new Point((int)regKey.GetValue("formMainLocationX", this.Location.X), (int)regKey.GetValue("formMainLocationY", this.Location.Y));
            }

            if (commonDialog != null)
            {
                commonDialog.Location = new Point(this.Location.X + this.Width / 2 - commonDialog.Width / 2, this.Location.Y + this.Height / 2 - commonDialog.Height / 2);
                commonDialog.AutomaticallyClose = (string)regKey.GetValue("initialDialog.AutomaricallyClose", "False") == "True";
            }

            if (FormStructureViewer != null && (int)regKey.GetValue("formStructureViewerLocationX", this.FormStructureViewer.Location.X) >= 0)
            {
                this.FormStructureViewer.Width = (int)regKey.GetValue("formStructureViewerWidth", this.FormStructureViewer.Width);
                this.FormStructureViewer.Height = (int)regKey.GetValue("formCrystalHeight", this.FormStructureViewer.Height);
                this.FormStructureViewer.Location = new Point((int)regKey.GetValue("formStructureViewerLocationX", this.FormStructureViewer.Location.X),
                    (int)regKey.GetValue("formStructureViewerLocationY", this.FormStructureViewer.Location.Y));
            }

            if (FormStereonet != null && (int)regKey.GetValue("formStereonetLocationY", this.FormStereonet.Location.Y) >= 0)
            {
                this.FormStereonet.Width = (int)regKey.GetValue("formStereonetWidth", this.FormStereonet.Width);
                this.FormStereonet.Height = (int)regKey.GetValue("formStereonetHeight", this.FormStereonet.Height);
                this.FormStereonet.Location = new Point((int)regKey.GetValue("formStereonetLocationX", this.FormStereonet.Location.X),
                    (int)regKey.GetValue("formStereonetLocationY", this.FormStereonet.Location.Y));

                FormStereonet.colorControl10DegLine.Argb = (int)regKey.GetValue("formStereonet.colorControl10DegLine.Argb", this.FormStereonet.colorControl10DegLine.Argb);
                FormStereonet.colorControl1DegLine.Argb = (int)regKey.GetValue("formStereonet.colorControl1DegLine.Argb", this.FormStereonet.colorControl1DegLine.Argb);
                FormStereonet.colorControl90DegLine.Argb = (int)regKey.GetValue("formStereonet.colorControl90DegLine.Argb", this.FormStereonet.colorControl90DegLine.Argb);
                FormStereonet.colorControlBackGround.Argb = (int)regKey.GetValue("formStereonet.colorControlBackGround.Argb", this.FormStereonet.colorControlBackGround.Argb);
                FormStereonet.colorControlGeneralAxis.Argb = (int)regKey.GetValue("formStereonet.colorControlGeneralAxis.Argb", this.FormStereonet.colorControlGeneralAxis.Argb);
                FormStereonet.colorControlGeneralPlane.Argb = (int)regKey.GetValue("formStereonet.colorControlGeneralPlane.Argb", this.FormStereonet.colorControlGeneralPlane.Argb);
                FormStereonet.colorControlGreatCircle.Argb = (int)regKey.GetValue("formStereonet.colorControlGreatCircle.Argb", this.FormStereonet.colorControlGreatCircle.Argb);
                FormStereonet.colorControlSmallCircle.Argb = (int)regKey.GetValue("formStereonet.colorControlSmallCircle.Argb", this.FormStereonet.colorControlSmallCircle.Argb);
                FormStereonet.colorControlString.Argb = (int)regKey.GetValue("formStereonet.colorControlString.Argb", this.FormStereonet.colorControlString.Argb);
                FormStereonet.colorControlUniqueAxis.Argb = (int)regKey.GetValue("formStereonet.colorControlUniqueAxis.Argb", this.FormStereonet.colorControlUniqueAxis.Argb);
                FormStereonet.colorControlUniquePlane.Argb = (int)regKey.GetValue("formStereonet.colorControlUniquePlane.Argb", this.FormStereonet.colorControlUniquePlane.Argb);
            }
            if (FormDiffractionSimulator != null && (int)regKey.GetValue("formElectronDiffractionLocationY", this.FormDiffractionSimulator.Location.Y) >= 0)
            {
                FormDiffractionSimulator.CancelSetVector = true;
                this.FormDiffractionSimulator.Width = (int)regKey.GetValue("formElectronDiffractionWidth", this.FormDiffractionSimulator.Width);
                this.FormDiffractionSimulator.Height = (int)regKey.GetValue("formElectronDiffractionHeight", this.FormDiffractionSimulator.Height);
                this.FormDiffractionSimulator.Location = new Point((int)regKey.GetValue("formElectronDiffractionLocationX", this.FormDiffractionSimulator.Location.X),
                    (int)regKey.GetValue("formElectronDiffractionLocationY", this.FormDiffractionSimulator.Location.Y));

                FormDiffractionSimulator.colorControlBackGround.Color = Color.FromArgb((int)regKey.GetValue("formElectronDiffraction.pictureBoxBackGround.BackColor", FormDiffractionSimulator.colorControlBackGround.Color.ToArgb()));
                FormDiffractionSimulator.colorControlDefectLine.Color = Color.FromArgb((int)regKey.GetValue("formElectronDiffraction.pictureBoxDefectLine.BackColor", FormDiffractionSimulator.colorControlDefectLine.Color.ToArgb()));
                FormDiffractionSimulator.colorControlExcessLine.Color = Color.FromArgb((int)regKey.GetValue("formElectronDiffraction.pictureBoxExcessLine.BackColor", FormDiffractionSimulator.colorControlExcessLine.Color.ToArgb()));
                FormDiffractionSimulator.colorControlForbiddenLattice.Color = Color.FromArgb((int)regKey.GetValue("formElectronDiffraction.pictureBoxForbiddenLattice.BackColor", FormDiffractionSimulator.colorControlForbiddenLattice.Color.ToArgb()));
                FormDiffractionSimulator.colorControlScrewGlide.Color = Color.FromArgb((int)regKey.GetValue("formElectronDiffraction.pictureBoxForbiddenScrewGlide.BackColor", FormDiffractionSimulator.colorControlScrewGlide.Color.ToArgb()));
                FormDiffractionSimulator.colorControlNoCondition.Color = Color.FromArgb((int)regKey.GetValue("formElectronDiffraction.pictureBoxNoCondition.BackColor", FormDiffractionSimulator.colorControlNoCondition.Color.ToArgb()));
                FormDiffractionSimulator.colorControlOrigin.Color = Color.FromArgb((int)regKey.GetValue("formElectronDiffraction.pictureBoxOrigin.BackColor", FormDiffractionSimulator.colorControlOrigin.Color.ToArgb()));
                FormDiffractionSimulator.colorControlString.Color = Color.FromArgb((int)regKey.GetValue("formElectronDiffraction.pictureBoxString.BackColor", FormDiffractionSimulator.colorControlString.Color.ToArgb()));

                FormDiffractionSimulator.FormElectronDiffraction_Load(new object(), new EventArgs());//.Visible = true;

                if ((o = regKey.GetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.FootX")) != null) FormDiffractionSimulator.FormDiffractionSimulatorGeometry.FootX = Convert.ToDouble((string)o);
                if ((o = regKey.GetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.FootY")) != null) FormDiffractionSimulator.FormDiffractionSimulatorGeometry.FootY = Convert.ToDouble((string)o);
                if ((o = regKey.GetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.CameraLength2")) != null)
                    FormDiffractionSimulator.FormDiffractionSimulatorGeometry.CameraLength2 = Convert.ToDouble((string)o);
                if ((o = regKey.GetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.DetectorWidth")) != null) FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorWidth = Convert.ToInt32((string)o);
                if ((o = regKey.GetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.DetectorHeight")) != null) FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorHeight = Convert.ToInt32((string)o);
                if ((o = regKey.GetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.DetectorPixelSize")) != null) FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorPixelSize = Convert.ToDouble((string)o);
                if ((o = regKey.GetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.Tau")) != null) FormDiffractionSimulator.FormDiffractionSimulatorGeometry.Tau = Convert.ToDouble((string)o);
                if ((o = regKey.GetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.Phi")) != null) FormDiffractionSimulator.FormDiffractionSimulatorGeometry.Phi = Convert.ToDouble((string)o);

                double resolution = Convert.ToDouble((string)regKey.GetValue("formElectronDiffraction.numericUpDownResolution.Value", FormDiffractionSimulator.numericBoxResolution.Value.ToString()));

                FormDiffractionSimulator.numericBoxResolution.Value = Math.Min(Math.Max(FormDiffractionSimulator.numericBoxResolution.Minimum, resolution), FormDiffractionSimulator.numericBoxResolution.Maximum);

                FormDiffractionSimulator.waveLengthControl.WaveSource =
                    (WaveSource)Enum.Parse(typeof(WaveSource), (string)regKey.GetValue("formElectronDiffraction.waveLengthControl.WaveSource", FormDiffractionSimulator.waveLengthControl.WaveSource.ToString()));

                FormDiffractionSimulator.waveLengthControl.Energy = Convert.ToDouble(regKey.GetValue("formElectronDiffraction.waveLengthControl.Energy", FormDiffractionSimulator.waveLengthControl.Energy));

                FormDiffractionSimulator.waveLengthControl.XrayWaveSourceElementNumber =
                    (int)regKey.GetValue("formElectronDiffraction.waveLengthControl.XrayWaveSourceElementNumber", FormDiffractionSimulator.waveLengthControl.XrayWaveSourceElementNumber);
                FormDiffractionSimulator.waveLengthControl.XrayWaveSourceLine =
                    (XrayLine)System.Enum.Parse(typeof(XrayLine), (string)regKey.GetValue("formElectronDiffraction.waveLengthControl.XrayWaveSourceLine", FormDiffractionSimulator.waveLengthControl.XrayWaveSourceLine.ToString()));
                FormDiffractionSimulator.CancelSetVector = false;
            }
            if (FormTEMID != null && (int)regKey.GetValue("formTEMIDLocationY", this.FormTEMID.Location.Y) >= 0)
            {
                this.FormTEMID.Width = (int)regKey.GetValue("formTEMIDWidth", this.FormTEMID.Width);
                this.FormTEMID.Height = (int)regKey.GetValue("formTEMIDHeight", this.FormTEMID.Height);
                this.FormTEMID.Location = new Point((int)regKey.GetValue("formTEMIDLocationX", this.FormTEMID.Location.X),
                (int)regKey.GetValue("formTEMIDLocationY", this.FormTEMID.Location.Y));
            }

            regKey.Close();
        }

        //.iniファイルを書き込み
        private void SaveInitialRegistry()
        {
            if (resetRegistryToolStripMenuItem.Checked)
            {
                Registry.CurrentUser.DeleteSubKey("Software\\Crystallography\\ReciPro");
                return;
            }
            var regKey = Registry.CurrentUser.CreateSubKey("Software\\Crystallography\\ReciPro");

            if (regKey == null) return;

            regKey.SetValue("Culture", Thread.CurrentThread.CurrentUICulture.Name);
            regKey.SetValue("DisableOpenGL", disableOpneGLToolStripMenuItem.Checked);

            regKey.SetValue("formMainWidth", this.Width);
            regKey.SetValue("formMainHeight", this.Height);
            regKey.SetValue("formMainLocationX", this.Location.X);
            regKey.SetValue("formMainLocationY", this.Location.Y);
            regKey.SetValue("initialDialog.AutomaricallyClose", commonDialog.AutomaticallyClose);

            regKey.SetValue("formStructureViewerWidth", this.FormStructureViewer.Width);
            regKey.SetValue("formStructureViewerHeight", this.FormStructureViewer.Height);
            regKey.SetValue("formStructureViewerLocationX", this.FormStructureViewer.Location.X);
            regKey.SetValue("formStructureViewerLocationY", this.FormStructureViewer.Location.Y);
            regKey.SetValue("formStereonetWidth", this.FormStereonet.Width);
            regKey.SetValue("formStereonetHeight", this.FormStereonet.Height);
            regKey.SetValue("formStereonetLocationX", this.FormStereonet.Location.X);
            regKey.SetValue("formStereonetLocationY", this.FormStereonet.Location.Y);
            regKey.SetValue("formElectronDiffractionWidth", this.FormDiffractionSimulator.Width);
            regKey.SetValue("formElectronDiffractionHeight", this.FormDiffractionSimulator.Height);
            regKey.SetValue("formElectronDiffractionLocationX", this.FormDiffractionSimulator.Location.X);
            regKey.SetValue("formElectronDiffractionLocationY", this.FormDiffractionSimulator.Location.Y);
            regKey.SetValue("formTEMIDWidth", this.FormTEMID.Width);
            regKey.SetValue("formTEMIDHeight", this.FormTEMID.Height);
            regKey.SetValue("formTEMIDLocationX", this.FormTEMID.Location.X);
            regKey.SetValue("formTEMIDLocationY", this.FormTEMID.Location.Y);

            regKey.SetValue("formElectronDiffraction.pictureBoxBackGround.BackColor", this.FormDiffractionSimulator.colorControlBackGround.Color.ToArgb());
            regKey.SetValue("formElectronDiffraction.pictureBoxDefectLine.BackColor", this.FormDiffractionSimulator.colorControlDefectLine.Color.ToArgb());
            regKey.SetValue("formElectronDiffraction.pictureBoxExcessLine.BackColor", this.FormDiffractionSimulator.colorControlExcessLine.Color.ToArgb());
            regKey.SetValue("formElectronDiffraction.pictureBoxForbiddenLattice.BackColor", this.FormDiffractionSimulator.colorControlForbiddenLattice.Color.ToArgb());
            regKey.SetValue("formElectronDiffraction.pictureBoxForbiddenScrewGlide.BackColor", this.FormDiffractionSimulator.colorControlScrewGlide.Color.ToArgb());
            regKey.SetValue("formElectronDiffraction.pictureBoxNoCondition.BackColor", this.FormDiffractionSimulator.colorControlNoCondition.Color.ToArgb());
            regKey.SetValue("formElectronDiffraction.pictureBoxOrigin.BackColor", this.FormDiffractionSimulator.colorControlOrigin.Color.ToArgb());
            regKey.SetValue("formElectronDiffraction.pictureBoxString.BackColor", this.FormDiffractionSimulator.colorControlString.Color.ToArgb());

            if (FormDiffractionSimulator.FormDiffractionSimulatorGeometry != null)
            {
                regKey.SetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.FootX", FormDiffractionSimulator.FormDiffractionSimulatorGeometry.FootX.ToString());
                regKey.SetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.FootY", FormDiffractionSimulator.FormDiffractionSimulatorGeometry.FootY.ToString());
                regKey.SetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.CameraLength2", FormDiffractionSimulator.FormDiffractionSimulatorGeometry.CameraLength2.ToString());
                regKey.SetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.DetectorWidth", FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorWidth.ToString());
                regKey.SetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.DetectorHeight", FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorHeight.ToString());
                regKey.SetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.DetectorPixelSize", FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorPixelSize.ToString());
                regKey.SetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.Tau", FormDiffractionSimulator.FormDiffractionSimulatorGeometry.Tau.ToString());
                regKey.SetValue("FormElectronDiffraction.FormDiffractionSimulatorGeometry.Phi", FormDiffractionSimulator.FormDiffractionSimulatorGeometry.Phi.ToString());
            }
            //regKey.SetValue("formElectronDiffraction.numericUpDownPictureResolution.Value", formElectronDiffraction.formOverlapPicture.numericUpDownPictureResolution.Value.ToString());
            regKey.SetValue("formElectronDiffraction.numericUpDownResolution.Value", FormDiffractionSimulator.numericBoxResolution.Value.ToString());

            regKey.SetValue("formElectronDiffraction.waveLengthControl.WaveSource", FormDiffractionSimulator.waveLengthControl.WaveSource);
            //regKey.SetValue("formElectronDiffraction.waveLengthControl.WaveLength", FormDiffractionSimulator.waveLengthControl.WaveLength);
            regKey.SetValue("formElectronDiffraction.waveLengthControl.Energy", FormDiffractionSimulator.waveLengthControl.Energy);
            regKey.SetValue("formElectronDiffraction.waveLengthControl.XrayWaveSourceElementNumber", FormDiffractionSimulator.waveLengthControl.XrayWaveSourceElementNumber);
            regKey.SetValue("formElectronDiffraction.waveLengthControl.XrayWaveSourceLine", FormDiffractionSimulator.waveLengthControl.XrayWaveSourceLine);

            regKey.SetValue("formStereonet.colorControl10DegLine.Argb", this.FormStereonet.colorControl10DegLine.Argb);
            regKey.SetValue("formStereonet.colorControl1DegLine.Argb", this.FormStereonet.colorControl1DegLine.Argb);
            regKey.SetValue("formStereonet.colorControl90DegLine.Argb", this.FormStereonet.colorControl90DegLine.Argb);
            regKey.SetValue("formStereonet.colorControlBackGround.Argb", this.FormStereonet.colorControlBackGround.Argb);
            regKey.SetValue("formStereonet.colorControlGeneralAxis.Argb", this.FormStereonet.colorControlGeneralAxis.Argb);
            regKey.SetValue("formStereonet.colorControlGeneralPlane.Argb", this.FormStereonet.colorControlGeneralPlane.Argb);
            regKey.SetValue("formStereonet.colorControlGreatCircle.Argb", this.FormStereonet.colorControlGreatCircle.Argb);
            regKey.SetValue("formStereonet.colorControlSmallCircle.Argb", this.FormStereonet.colorControlSmallCircle.Argb);
            regKey.SetValue("formStereonet.colorControlString.Argb", this.FormStereonet.colorControlString.Argb);
            regKey.SetValue("formStereonet.colorControlUniqueAxis.Argb", this.FormStereonet.colorControlUniqueAxis.Argb);
            regKey.SetValue("formStereonet.colorControlUniquePlane.Argb", this.FormStereonet.colorControlUniquePlane.Argb);

            regKey.Close();
        }

        #endregion レジストリ操作

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
            axis = axis.Normarize();
            if (angle == 0) return;

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

            if (SkipEulerChange && FormRotation.Visible)//Euler angle が直接入力された時
                FormRotation.SetRotation();

            DrawAxes();

            if (!SkipEulerChange)//Euler Angle が直接入力されてない時
            {
                var euler = Euler.GetEulerAngle(Crystal.RotationMatrix);
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

        #endregion

        #region 他のFunctionとの連携

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

        private void toolStripButtonSpotID_CheckedChanged(object sender, EventArgs e)
            => FormSpotID.Visible = toolStripButtonSpotID.Checked;
        private void toolStripButtonSymmetryInformation_CheckedChanged(object sender, EventArgs e)
            => crystalControl.SymmetryInformationVisible = toolStripButtonSymmetryInformation.Checked;
        private void toolStripButtonScatteringFactor_CheckedChanged(object sender, EventArgs e)
            => crystalControl.ScatteringFactorVisible = toolStripButtonScatteringFactor.Checked;
        private void toolStripButtonStructureViewer_CheckedChanged(object sender, EventArgs e)
            => FormStructureViewer.Visible = toolStripButtonStructureViewer.Checked;
        private void toolStripButtonStereonet_CheckedChanged(object sender, EventArgs e)
            => FormStereonet.Visible = toolStripButtonStereonet.Checked;
        private void ToolStripButtonRotation_CheckedChanged(object sender, EventArgs e)
            => FormRotation.Visible = toolStripButtonRotation.Checked;

        async private void toolStripButtonElectronDiffraction_CheckedChanged(object sender, EventArgs e)
        {
            FormDiffractionSimulator.Visible = toolStripButtonElectronDiffraction.Checked;

            //Electron diffractionを表示した直後、なぜかグラフィックボックスがグレーになってしまうので、その対処.
            await Task.Delay(200);
            if (FormDiffractionSimulator.Visible)
                FormDiffractionSimulator.Draw();
        }
        private void toolStripButtonImageSimulation_CheckedChanged(object sender, EventArgs e)
            => FormImageSimulator.Visible = toolStripButtonImageSimulation.Checked;
        private void toolStripButtonPolycrystallineDiffraction_CheckedChanged(object sender, EventArgs e)
        {
            FormPolycrystallineDiffractionSimulator.Visible = toolStripButtonPolycrystallineDiffraction.Checked;
            listBox_SelectedIndexChanged(listBox, e);
        }

        private void toolStripButtonTemID_CheckedChanged(object sender, EventArgs e)
            => FormTEMID.Visible = toolStripButtonTEMID.Checked;
        private void scatteringFactor_VisibleChanged(object sender, EventArgs e)
            => toolStripButtonScatteringFactor.Checked = crystalControl.ScatteringFactorVisible;
        private void symmetryInformation_VisibleChanged(object sender, EventArgs e)
            => toolStripButtonSymmetryInformation.Checked = crystalControl.SymmetryInformationVisible;

        private void toolStripButtonDatabase_CheckedChanged(object sender, EventArgs e)
            => FormCrystalDatabase.Visible = toolStripButtonDatabase.Checked;

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
                var euler = Euler.GetEulerAngle(Crystal.RotationMatrix);
                SkipEulerChange = true;
                numericUpDownEulerPhi.Value = (decimal)(euler.Phi / Math.PI * 180);
                numericUpDownEulerTheta.Value = (decimal)(euler.Theta / Math.PI * 180);
                numericUpDownEulerPsi.Value = (decimal)(euler.Psi / Math.PI * 180);
                SkipEulerChange = false;

                SetNearestUVW();

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

                crystalControl_CrystalChanged_1(sender, e);
                DrawAxes();
            }
        }
        #endregion

        #region 回転ボタン

        //角度リセットボタン
        private void buttonReset_Click(object sender, EventArgs e)
        {
            timer.Stop();
            SetRotation(new Matrix3D());
        }

        private void buttonDirection_Click(object sender, EventArgs e)
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
                startAnimation(v);
            else
                Rotate(v, numericBoxStep.RadianValue);
        }

        private readonly Stopwatch stopwatchAnimation = new();
        private long ellapseTime = 0;

        private void startAnimation(Vector3DBase v)
        {
            timer.Stop();
            stopwatchAnimation.Reset();
            stopwatchAnimation.Start();
            ellapseTime = 0;
            rotationAxisAnimation = v;
            timer.Start();
        }

        private Vector3DBase rotationAxisAnimation = new Vector3D(0, 0, 1);
        private int timerCounter = 1;

        private void timer_Tick(object sender, EventArgs e)
        {
            double differenceTime = stopwatchAnimation.ElapsedMilliseconds - ellapseTime;
            ellapseTime = stopwatchAnimation.ElapsedMilliseconds;
            double angle = (differenceTime) / 1000.0 * numericBoxStep.RadianValue;
            if (timerCounter++ % 10 == 0)
            {
                toolStripStatusLabel.Text = "Frame rate: " + (1000.0 / differenceTime).ToString("f1") + "frm/sec.";
                timerCounter = 1;
            }
            Rotate(rotationAxisAnimation, angle);
        }
        private void checkBoxAnimation_CheckedChanged(object sender, EventArgs e)
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

        #region ベクトルでの回転指定

        private void buttonSetVector_Click(object sender, EventArgs e)
        {
            if (Crystal == null) return;
            double u = numericBoxAxisU.Value, v = numericBoxAxisV.Value, w = numericBoxAxisW.Value;
            double h = numericBoxPlaneH.Value, k = numericBoxPlaneK.Value, l = numericBoxPlaneL.Value;

            Vector3D xVector, yVector, zVector;
            Vector3D aAxis = Crystal.A_Axis, bAxis = Crystal.B_Axis, cAxis = Crystal.C_Axis;
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

        #region オイラー角度を直接入力したばあい

        public bool SkipEulerChange = false;

        private void numericUpDownPhi_ValueChanged(object sender, EventArgs e)
        {
            if (SkipEulerChange) return;
            SkipEulerChange = true;
            if (numericUpDownEulerPhi.Value > 180)
                numericUpDownEulerPhi.Value -= 360;
            if (numericUpDownEulerPhi.Value < -180)
                numericUpDownEulerPhi.Value += 360;

            if (numericUpDownEulerPsi.Value > 180)
                numericUpDownEulerPsi.Value -= 360;
            if (numericUpDownEulerPsi.Value < -180)
                numericUpDownEulerPsi.Value += 360;

            var phi = (double)numericUpDownEulerPhi.Value / 180.0 * Math.PI;
            var theta = (double)numericUpDownEulerTheta.Value / 180.0 * Math.PI;
            var psi = (double)numericUpDownEulerPsi.Value / 180.0 * Math.PI;

            double cosPhi = Math.Cos(phi), sinPhi = Math.Sin(phi);
            double cosTheta = Math.Cos(theta), sinTheta = Math.Sin(theta);
            double cosPsi = Math.Cos(psi), sinPsi = Math.Sin(psi);

            var matrix = new Matrix3D(
                cosPhi * cosPsi - cosTheta * sinPhi * sinPsi,
                sinPhi * cosPsi + cosTheta * cosPhi * sinPsi,
                sinPsi * sinTheta,

                -cosPhi * sinPsi - cosTheta * sinPhi * cosPsi,
                -sinPhi * sinPsi + cosTheta * cosPhi * cosPsi,
                cosPsi * sinTheta,

                sinTheta * sinPhi,
                -sinTheta * cosPhi,
                cosTheta

                );
            SetRotation(matrix);

            SkipEulerChange = false;
        }

        #endregion オイラー角度を直接入力したばあい

        #region リストボックス関連

        private void buttonUpper_Click(object sender, EventArgs e)
        {
            var n = listBox.SelectedIndex;
            if (n <= 0) return;
            object o = listBox.SelectedItem;
            listBox.Items.Remove(listBox.SelectedItem);
            listBox.Items.Insert(n - 1, o);
            listBox.SelectedIndex = n - 1;
        }

        private void buttonLower_Click(object sender, EventArgs e)
        {
            int n = listBox.SelectedIndex;
            if (n >= listBox.Items.Count - 1) return;
            object o = listBox.SelectedItem;
            listBox.Items.Remove(listBox.SelectedItem);
            listBox.Items.Insert(n + 1, o);
            listBox.SelectedIndex = n + 1;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (crystalControl.StrainControlVisible) return;

            crystalControl.GenerateFromInterface();
            if (crystalControl.Crystal != null)
                listBox.Items.Add(crystalControl.Crystal);
            listBox.SelectedIndex = -1;
            listBox.SelectedIndex = listBox.Items.Count - 1;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex >= 0)
            {
                var n = listBox.SelectedIndex;
                listBox.Items.Remove(listBox.SelectedItem);
                if (listBox.Items.Count > n)
                    listBox.SelectedIndex = n;
                else
                    listBox.SelectedIndex = n - 1;
            }
        }

        private void buttonAllClear_Click(object sender, EventArgs e) => listBox.Items.Clear();

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (crystalControl.StrainControlVisible) return;

            if (listBox.SelectedIndex < 0) return;

            crystalControl.GenerateFromInterface();

            if (crystalControl.Crystal != null && listBox.SelectedIndex >= 0)
                listBox.Items[listBox.SelectedIndex] = crystalControl.Crystal;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex >= 0)
                crystalControl.Crystal = (Crystal)listBox.SelectedItem;
            DrawAxes();
        }

        #endregion リストボックス関連

        #region 結晶データの読み込み/書き込み
        private void readCrystalList(string fileName, bool showSelectionDialog, bool clearPresentList)
        {
            var cry = new List<Crystal>();
            var list = ConvertCrystalData.ConvertToCrystalList(fileName);
            if (list == null)
                return;
            cry.AddRange(list);
            if (showSelectionDialog)
            {
                var formCrystalSelection = new FormCrystalSelection
                {
                    LoadMode = true
                };
                formCrystalSelection.SetCrystalList(cry);
                formCrystalSelection.Location = new Point(this.Location.X + this.Width / 2 - formCrystalSelection.Width / 2, this.Location.Y + this.Height / 2 - formCrystalSelection.Height / 2);
                if (formCrystalSelection.ShowDialog() == DialogResult.OK)
                {
                    cry.Clear();
                    cry.AddRange(formCrystalSelection.CheckedCrystalList);
                }
                else return;
            }

            if (cry.Any())
            {
                if (clearPresentList)
                    listBox.Items.Clear();

                foreach (var c in cry)
                    listBox.Items.Add(c);
                if (listBox.Items.Count > 0)
                    listBox.SelectedIndex = 0;
            }
        }

        private void saveCrystalDataToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void readCrystalDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "xml, out|*.xml;*.out" };
            if (dlg.ShowDialog() == DialogResult.OK)
                readCrystalList(dlg.FileName, true, true);
        }

        private void readCrystalDataAndAddtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "xml, out|*.xml;*.out" };
            if (dlg.ShowDialog() == DialogResult.OK)
                readCrystalList(dlg.FileName, true, false);
        }

        private void ToolStripMenuItemReadInitialCrystalList_Click(object sender, EventArgs e)
            => readCrystalList(UserAppDataPath + "initial.xml", false, true);

        private void helpwebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fn = "\\doc\\ReciProManual(" + (Language == Languages.English ? "en" : "ja") + ").pdf";
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var f = new FormPDF(appPath+fn);
            f.ShowDialog();
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

        private void ngenCompileToolStripMenuItem_Click(object sender, EventArgs e) => Ngen.Compile();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void toolTipToolStripMenuItem_CheckedChanged(object sender, EventArgs e) => toolTip.Active = toolTipToolStripMenuItem.Checked;
        private void toolStripMenuItem1_Click(object sender, EventArgs e) => listBox.Items.Clear();
        private void toolStripMenuItemExportCIF_Click(object sender, EventArgs e) => crystalControl.exportThisCrystalAsCIFToolStripMenuItem_Click(sender, e);

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            englishToolStripMenuItem.Checked = ((ToolStripMenuItem)sender).Name.Contains("english");
            japaneseToolStripMenuItem.Checked = !englishToolStripMenuItem.Checked;
            Thread.CurrentThread.CurrentUICulture = englishToolStripMenuItem.Checked ? new System.Globalization.CultureInfo("en") : new System.Globalization.CultureInfo("ja");
        }
        private void githubPageToolStripMenuItem_Click(object sender, EventArgs e) 
            => Process.Start(new ProcessStartInfo("https://github.com/seto77/ReciPro") { UseShellExecute=true});
        private void reportBugsRequestsOrCommentsToolStripMenuItem1_Click(object sender, EventArgs e) 
            => Process.Start(new ProcessStartInfo("https://github.com/seto77/ReciPro/issues") { UseShellExecute = true });

        #endregion FileMenu

        #region キーストロークイベント
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.D)
                toolStripButtonElectronDiffraction.Checked = !toolStripButtonElectronDiffraction.Checked;
            else if (e.Control && e.Shift && e.KeyCode == Keys.V)
                toolStripButtonStructureViewer.Checked = !toolStripButtonStructureViewer.Checked;
            else if (e.Control && e.Shift && e.KeyCode == Keys.S)
                toolStripButtonStereonet.Checked = !toolStripButtonStereonet.Checked;
            else if (e.Control && e.Shift && e.KeyCode == Keys.T)
                toolStripButtonSpotID.Checked = !toolStripButtonSpotID.Checked;
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

        #region Axisの描画関連

        //軸の情報を表示する部分
        public void DrawAxes()
        {
            if (glControlAxes == null)
                return;
            glControlAxes.WorldMatrixEx = Crystal?.RotationMatrix.Transpose();
        }

        private void crystalControl_CrystalChanged_1(object sender, EventArgs e)
        {
            if (glControlAxes == null || Crystal.A == 0 || Crystal.B == 0 || Crystal.C == 0)
                return;

            var max = new[] { Crystal.A, Crystal.B, Crystal.C }.Max();
            var vec = new[] { Crystal.A_Axis / max, Crystal.B_Axis / max, Crystal.C_Axis / max };
            var color = new[] { Col4.Red, Col4.Green, Col4.Blue };
            var label = new[] { "a", "b", "c" };
            var obj = new List<GLObject>();
            for (int i = 0; i < 3; i++)
            {
                obj.Add(new Cylinder(-vec[i], vec[i] * 2 - 0.3 * vec[i].Normarize(), 0.075, new Material(color[i]), DrawingMode.Surfaces));
                obj.Add(new Cone(vec[i], -0.3 * vec[i].Normarize(), 0.15, new Material(color[i]), DrawingMode.Surfaces));
                obj.Add(new TextObject(label[i], 13, vec[i] + 0.1 * vec[i].Normarize(), 0, true, new Material(color[i])));
            }
            obj.Add(new Sphere(new Vec3(0, 0, 0), 0.12, new Material(Col4.Gray), DrawingMode.Surfaces));

            glControlAxes.DeleteAllObjects();
            glControlAxes.AddObjects(obj);
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

        #region ドラッグドロップ
        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            var fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileName.Length == 1 && (fileName[0].ToLower().EndsWith("xml") || fileName[0].ToLower().EndsWith("out") || fileName[0].ToLower().EndsWith("cdb2")))
            {
                var dr = MessageBox.Show(this, "Read the list as a new list (if select 'No', add the list to the end of the present one",
                    "Option", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Cancel)
                    return;
                else if (dr == DialogResult.Yes)
                    readCrystalList(fileName[0], true, true);
                else
                    readCrystalList(fileName[0], true, false);
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
                            MessageBox.Show($"Failed to downlod {Path}. \r\nSorry!", "Error!");
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
        /// <param name="showEllapsedTime"></param>
        /// <param name="showRemainTime"></param>
        /// <param name="digit"></param>
        private void reportProgress(long current, long total, long elapsedMilliseconds, string message,
            int sleep = 0, bool showPercentage = true, bool showEllapsedTime = true, bool showRemainTime = true, int digit = 1)
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
                if (showEllapsedTime) message += $" Elappsed: {ellapsedSec.ToString(format)} s.";
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

        private void numericBoxMaxUVW_ValueChanged(object sender, EventArgs e)
        {
            if (Crystal == null || Crystal.A == 0 || Crystal.B == 0 || Crystal.C == 0) return;
            if (Crystal.A_Axis == null)
                Crystal.SetAxis();

            uvwIndices = new List<(int U, int V, int W)>();
            uvwLength2 = new List<double>();

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
                        {
                            uvwIndices.Add((u, v, w));
                            uvwLength2.Add((u * Crystal.A_Axis + v * Crystal.B_Axis + w * Crystal.C_Axis).Length2);
                        }
                    }
            SetNearestUVW();
        }

        private List<(int U, int V, int W)> uvwIndices = new();
        private List<double> uvwLength2 = new();

        private void SetNearestUVW()//最も近いuvwを検索
        {
            if (Crystal == null || Crystal.A == 0 || Crystal.B == 0 || Crystal.C == 0) return;
            if (Crystal.A_Axis == null)
                Crystal.SetAxis();

            if (uvwIndices.Count == 0)
            {
                numericBoxMaxUVW_ValueChanged(new object(), new EventArgs());
                return;
            }

            (int U, int V, int W) bestIndex = (0, 0, 0);
            double aZ = (Crystal.RotationMatrix * Crystal.A_Axis).Z, bZ = (Crystal.RotationMatrix * Crystal.B_Axis).Z, cZ = (Crystal.RotationMatrix * Crystal.C_Axis).Z;
            double dev = 0, temp;

            for (int i = 0; i < uvwIndices.Count; i++)
            {
                var z = uvwIndices[i].U * aZ + uvwIndices[i].V * bZ + uvwIndices[i].W * cZ;
                if (z > 0 && dev < (temp = z * z / uvwLength2[i]))
                {
                    dev = temp;
                    bestIndex = uvwIndices[i];
                }
            }
            labelCurrentIndexU.Text = bestIndex.U.ToString();
            labelCurrentIndexV.Text = bestIndex.V.ToString();
            labelCurrentIndexW.Text = bestIndex.W.ToString();
        }
        #endregion

        #region 晶体軸/結晶面 設定
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
}
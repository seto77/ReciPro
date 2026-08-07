// 260807Cl 新規作成: ALCHEMI シミュレータの子フォーム (A4′、設計 §5.5)。
//
// ⚠**表示文字列はここ (Designer) に英語で置き、コンストラクタの ApplyLocalization() が
//   Localization.Loc で 11 言語へ差し替える**。既存フォームの多くは resources.ApplyResources +
//   culture resx だが、新規フォームでその経路を手書きすると「フォーム resx へ手書きした文字列は
//   VS デザイナの再シリアライズで黙って消える」既知の事故を踏む。Loc はコード側なので消えない
//   (FormDiffractionSimulator の comboBoxKikuchiMode.Items が同じ理由で既に Loc 経路)。
//   後で resx へ揃えたくなったら CodeLocalizer で移行できる。
//
// レイアウトは Designer.cs 内で完結させる (コードでの動的生成はしない = リポジトリの規律)。
using Crystallography.Controls;
using System.Windows.Forms;

namespace ReciPro
{
    partial class FormALCHEMI
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            toolTip = new ToolTip(components);
            panelLeft = new Panel();
            groupBoxScan = new GroupBox();
            numericBoxAxisH = new NumericBox();
            numericBoxAxisK = new NumericBox();
            numericBoxAxisL = new NumericBox();
            numericBoxRange = new NumericBox();
            numericBoxPoints = new NumericBox();
            labelThetaB = new Label();
            groupBoxThickness = new GroupBox();
            numericBoxThicknessStart = new NumericBox();
            numericBoxThicknessEnd = new NumericBox();
            numericBoxThicknessStep = new NumericBox();
            groupBoxCalculation = new GroupBox();
            numericBoxMaxNumOfBloch = new NumericBox();
            checkBoxDechannelling = new CheckBox();
            comboBoxSolver = new ComboBox();
            labelSolver = new Label();
            groupBoxChannels = new GroupBox();
            checkedListBoxChannels = new CheckedListBox();
            groupBoxSites = new GroupBox();
            checkedListBoxSites = new CheckedListBox();
            buttonSimulate = new Button();
            buttonStop = new Button();
            tabControl = new TabControl();
            tabPageCurve = new TabPage();
            graphControl = new GraphControl();
            panelCurveFooter = new Panel();
            labelThickness = new Label();
            trackBarThickness = new TrackBar();
            labelThicknessValue = new Label();
            labelNormalization = new Label();
            comboBoxNormalization = new ComboBox();
            labelXAxis = new Label();
            comboBoxXAxis = new ComboBox();
            checkBoxShowBragg = new CheckBox();
            buttonExport = new Button();
            labelStats = new Label();
            labelBasis = new Label();
            tabPage2DMap = new TabPage();
            tabPageFit = new TabPage();
            statusStrip = new StatusStrip();
            toolStripProgressBar = new ToolStripProgressBar();
            toolStripStatusLabel = new ToolStripStatusLabel();
            saveFileDialog = new SaveFileDialog();

            toolTip.IsBalloon = true;
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 100;
            captureExtender.SetCapture(this, true);

            panelLeft.SuspendLayout();
            groupBoxScan.SuspendLayout();
            groupBoxThickness.SuspendLayout();
            groupBoxCalculation.SuspendLayout();
            groupBoxChannels.SuspendLayout();
            groupBoxSites.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageCurve.SuspendLayout();
            panelCurveFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarThickness).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();

            #region 左ペイン: 走査
            groupBoxScan.Controls.Add(numericBoxAxisH);
            groupBoxScan.Controls.Add(numericBoxAxisK);
            groupBoxScan.Controls.Add(numericBoxAxisL);
            groupBoxScan.Controls.Add(numericBoxRange);
            groupBoxScan.Controls.Add(numericBoxPoints);
            groupBoxScan.Controls.Add(labelThetaB);
            groupBoxScan.Location = new System.Drawing.Point(6, 4);
            groupBoxScan.Name = "groupBoxScan";
            groupBoxScan.Size = new System.Drawing.Size(346, 104);
            groupBoxScan.TabIndex = 0;
            groupBoxScan.TabStop = false;
            groupBoxScan.Text = "Rocking scan";

            numericBoxAxisH.DecimalPlaces = 0;
            numericBoxAxisH.HeaderText = "Tilt axis  (";
            numericBoxAxisH.Location = new System.Drawing.Point(8, 20);
            numericBoxAxisH.Maximum = 20D;
            numericBoxAxisH.Minimum = -20D;
            numericBoxAxisH.Name = "numericBoxAxisH";
            numericBoxAxisH.Size = new System.Drawing.Size(112, 22);
            numericBoxAxisH.TabIndex = 0;
            numericBoxAxisH.Value = 1D;//既定は (1 0 0) 系統反射列

            numericBoxAxisK.DecimalPlaces = 0;
            numericBoxAxisK.Location = new System.Drawing.Point(120, 20);
            numericBoxAxisK.Maximum = 20D;
            numericBoxAxisK.Minimum = -20D;
            numericBoxAxisK.Name = "numericBoxAxisK";
            numericBoxAxisK.Size = new System.Drawing.Size(52, 22);
            numericBoxAxisK.TabIndex = 1;
            numericBoxAxisK.Value = 0D;

            numericBoxAxisL.DecimalPlaces = 0;
            numericBoxAxisL.FooterText = ")";
            numericBoxAxisL.Location = new System.Drawing.Point(172, 20);
            numericBoxAxisL.Maximum = 20D;
            numericBoxAxisL.Minimum = -20D;
            numericBoxAxisL.Name = "numericBoxAxisL";
            numericBoxAxisL.Size = new System.Drawing.Size(66, 22);
            numericBoxAxisL.TabIndex = 2;
            numericBoxAxisL.Value = 0D;

            numericBoxRange.DecimalPlaces = 2;
            numericBoxRange.FooterText = "mrad";
            numericBoxRange.HeaderText = "Range  ±";
            numericBoxRange.Location = new System.Drawing.Point(8, 46);
            numericBoxRange.Maximum = 60D;
            numericBoxRange.Minimum = 0.01D;
            numericBoxRange.Name = "numericBoxRange";
            numericBoxRange.Size = new System.Drawing.Size(178, 22);
            numericBoxRange.TabIndex = 3;
            numericBoxRange.Value = 8D;

            numericBoxPoints.DecimalPlaces = 0;
            numericBoxPoints.HeaderText = "Points";
            numericBoxPoints.Location = new System.Drawing.Point(190, 46);
            numericBoxPoints.Maximum = 1001D;
            numericBoxPoints.Minimum = 3D;
            numericBoxPoints.Name = "numericBoxPoints";
            numericBoxPoints.Size = new System.Drawing.Size(148, 22);
            numericBoxPoints.TabIndex = 4;
            numericBoxPoints.Value = 101D;

            labelThetaB.AutoSize = true;
            labelThetaB.Location = new System.Drawing.Point(8, 76);
            labelThetaB.Name = "labelThetaB";
            labelThetaB.Size = new System.Drawing.Size(60, 15);
            labelThetaB.TabIndex = 5;
            labelThetaB.Text = "-";
            #endregion

            #region 左ペイン: 厚み
            groupBoxThickness.Controls.Add(numericBoxThicknessStart);
            groupBoxThickness.Controls.Add(numericBoxThicknessEnd);
            groupBoxThickness.Controls.Add(numericBoxThicknessStep);
            groupBoxThickness.Location = new System.Drawing.Point(6, 112);
            groupBoxThickness.Name = "groupBoxThickness";
            groupBoxThickness.Size = new System.Drawing.Size(346, 50);
            groupBoxThickness.TabIndex = 1;
            groupBoxThickness.TabStop = false;
            groupBoxThickness.Text = "Thickness";

            numericBoxThicknessStart.DecimalPlaces = 1;
            numericBoxThicknessStart.HeaderText = "from";
            numericBoxThicknessStart.Location = new System.Drawing.Point(8, 20);
            numericBoxThicknessStart.Maximum = 10000D;
            numericBoxThicknessStart.Minimum = 0D;
            numericBoxThicknessStart.Name = "numericBoxThicknessStart";
            numericBoxThicknessStart.Size = new System.Drawing.Size(106, 22);
            numericBoxThicknessStart.TabIndex = 0;
            numericBoxThicknessStart.Value = 10D;

            numericBoxThicknessEnd.DecimalPlaces = 1;
            numericBoxThicknessEnd.HeaderText = "to";
            numericBoxThicknessEnd.Location = new System.Drawing.Point(114, 20);
            numericBoxThicknessEnd.Maximum = 10000D;
            numericBoxThicknessEnd.Minimum = 0D;
            numericBoxThicknessEnd.Name = "numericBoxThicknessEnd";
            numericBoxThicknessEnd.Size = new System.Drawing.Size(96, 22);
            numericBoxThicknessEnd.TabIndex = 1;
            numericBoxThicknessEnd.Value = 100D;

            numericBoxThicknessStep.DecimalPlaces = 1;
            numericBoxThicknessStep.FooterText = "nm";
            numericBoxThicknessStep.HeaderText = "step";
            numericBoxThicknessStep.Location = new System.Drawing.Point(210, 20);
            numericBoxThicknessStep.Maximum = 1000D;
            numericBoxThicknessStep.Minimum = 0.1D;
            numericBoxThicknessStep.Name = "numericBoxThicknessStep";
            numericBoxThicknessStep.Size = new System.Drawing.Size(128, 22);
            numericBoxThicknessStep.TabIndex = 2;
            numericBoxThicknessStep.Value = 10D;
            #endregion

            #region 左ペイン: 計算条件
            groupBoxCalculation.Controls.Add(numericBoxMaxNumOfBloch);
            groupBoxCalculation.Controls.Add(checkBoxDechannelling);
            groupBoxCalculation.Controls.Add(labelSolver);
            groupBoxCalculation.Controls.Add(comboBoxSolver);
            groupBoxCalculation.Location = new System.Drawing.Point(6, 166);
            groupBoxCalculation.Name = "groupBoxCalculation";
            groupBoxCalculation.Size = new System.Drawing.Size(346, 78);
            groupBoxCalculation.TabIndex = 2;
            groupBoxCalculation.TabStop = false;
            groupBoxCalculation.Text = "Calculation";

            numericBoxMaxNumOfBloch.DecimalPlaces = 0;
            numericBoxMaxNumOfBloch.HeaderText = "Max. beams";
            numericBoxMaxNumOfBloch.Location = new System.Drawing.Point(8, 20);
            numericBoxMaxNumOfBloch.Maximum = 2000D;
            numericBoxMaxNumOfBloch.Minimum = 1D;
            numericBoxMaxNumOfBloch.Name = "numericBoxMaxNumOfBloch";
            numericBoxMaxNumOfBloch.Size = new System.Drawing.Size(166, 22);
            numericBoxMaxNumOfBloch.TabIndex = 0;
            numericBoxMaxNumOfBloch.Value = 120D;

            labelSolver.AutoSize = true;
            labelSolver.Location = new System.Drawing.Point(180, 24);
            labelSolver.Name = "labelSolver";
            labelSolver.Size = new System.Drawing.Size(42, 15);
            labelSolver.TabIndex = 1;
            labelSolver.Text = "Solver";

            comboBoxSolver.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSolver.Location = new System.Drawing.Point(228, 20);
            comboBoxSolver.Name = "comboBoxSolver";
            comboBoxSolver.Size = new System.Drawing.Size(110, 23);
            comboBoxSolver.TabIndex = 2;

            checkBoxDechannelling.AutoSize = true;
            checkBoxDechannelling.Checked = true;
            checkBoxDechannelling.CheckState = CheckState.Checked;
            checkBoxDechannelling.Location = new System.Drawing.Point(10, 50);
            checkBoxDechannelling.Name = "checkBoxDechannelling";
            checkBoxDechannelling.Size = new System.Drawing.Size(200, 19);
            checkBoxDechannelling.TabIndex = 3;
            checkBoxDechannelling.Text = "Include the dechannelled component";
            checkBoxDechannelling.UseVisualStyleBackColor = true;
            #endregion

            #region 左ペイン: チャネル / サイト
            groupBoxChannels.Controls.Add(checkedListBoxChannels);
            groupBoxChannels.Location = new System.Drawing.Point(6, 248);
            groupBoxChannels.Name = "groupBoxChannels";
            groupBoxChannels.Size = new System.Drawing.Size(346, 156);
            groupBoxChannels.TabIndex = 3;
            groupBoxChannels.TabStop = false;
            groupBoxChannels.Text = "Ionization channels";

            checkedListBoxChannels.CheckOnClick = true;
            checkedListBoxChannels.Dock = DockStyle.Fill;
            checkedListBoxChannels.FormattingEnabled = true;
            checkedListBoxChannels.IntegralHeight = false;
            checkedListBoxChannels.Name = "checkedListBoxChannels";
            checkedListBoxChannels.TabIndex = 0;

            groupBoxSites.Controls.Add(checkedListBoxSites);
            groupBoxSites.Location = new System.Drawing.Point(6, 408);
            groupBoxSites.Name = "groupBoxSites";
            groupBoxSites.Size = new System.Drawing.Size(346, 150);
            groupBoxSites.TabIndex = 4;
            groupBoxSites.TabStop = false;
            groupBoxSites.Text = "Site hypotheses";

            checkedListBoxSites.CheckOnClick = true;
            checkedListBoxSites.Dock = DockStyle.Fill;
            checkedListBoxSites.FormattingEnabled = true;
            checkedListBoxSites.IntegralHeight = false;
            checkedListBoxSites.Name = "checkedListBoxSites";
            checkedListBoxSites.TabIndex = 0;
            #endregion

            #region 左ペイン: 実行
            buttonSimulate.Location = new System.Drawing.Point(6, 566);
            buttonSimulate.Name = "buttonSimulate";
            buttonSimulate.Size = new System.Drawing.Size(240, 30);
            buttonSimulate.TabIndex = 5;
            buttonSimulate.Text = "Simulate";
            buttonSimulate.UseVisualStyleBackColor = true;
            buttonSimulate.Click += buttonSimulate_Click;

            buttonStop.Location = new System.Drawing.Point(252, 566);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new System.Drawing.Size(100, 30);
            buttonStop.TabIndex = 6;
            buttonStop.Text = "Stop";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Visible = false;
            buttonStop.Click += buttonStop_Click;

            panelLeft.Controls.Add(groupBoxScan);
            panelLeft.Controls.Add(groupBoxThickness);
            panelLeft.Controls.Add(groupBoxCalculation);
            panelLeft.Controls.Add(groupBoxChannels);
            panelLeft.Controls.Add(groupBoxSites);
            panelLeft.Controls.Add(buttonSimulate);
            panelLeft.Controls.Add(buttonStop);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new System.Drawing.Size(358, 604);
            panelLeft.TabIndex = 0;
            #endregion

            #region 右ペイン: タブ (Curve / 2D map / Fit)
            //設計 §5.5 + §9-17 の作者決定: タブ構造は最初から作り、v1 では未実装タブを**非表示**にする
            //(disabled で見せもしない)。ここでは Controls.Add しないことで実現し、TabPage の実体だけ残す
            tabControl.Controls.Add(tabPageCurve);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.TabIndex = 1;

            tabPageCurve.Controls.Add(graphControl);
            tabPageCurve.Controls.Add(panelCurveFooter);
            tabPageCurve.Name = "tabPageCurve";
            tabPageCurve.Padding = new Padding(3);
            tabPageCurve.TabIndex = 0;
            tabPageCurve.Text = "Curve";
            tabPageCurve.UseVisualStyleBackColor = true;

            tabPage2DMap.Name = "tabPage2DMap";
            tabPage2DMap.TabIndex = 1;
            tabPage2DMap.Text = "2D map";
            tabPage2DMap.UseVisualStyleBackColor = true;

            tabPageFit.Name = "tabPageFit";
            tabPageFit.TabIndex = 2;
            tabPageFit.Text = "Fit";
            tabPageFit.UseVisualStyleBackColor = true;

            graphControl.Dock = DockStyle.Fill;
            graphControl.Name = "graphControl";
            graphControl.TabIndex = 0;

            panelCurveFooter.Controls.Add(labelThickness);
            panelCurveFooter.Controls.Add(trackBarThickness);
            panelCurveFooter.Controls.Add(labelThicknessValue);
            panelCurveFooter.Controls.Add(labelNormalization);
            panelCurveFooter.Controls.Add(comboBoxNormalization);
            panelCurveFooter.Controls.Add(labelXAxis);
            panelCurveFooter.Controls.Add(comboBoxXAxis);
            panelCurveFooter.Controls.Add(checkBoxShowBragg);
            panelCurveFooter.Controls.Add(buttonExport);
            panelCurveFooter.Controls.Add(labelStats);
            panelCurveFooter.Controls.Add(labelBasis);
            panelCurveFooter.Dock = DockStyle.Bottom;
            panelCurveFooter.Name = "panelCurveFooter";
            panelCurveFooter.Size = new System.Drawing.Size(200, 116);
            panelCurveFooter.TabIndex = 1;

            //厚みセレクタ — 実曲線を見て分かったとおり、サイト信号は厚みで符号すら変わるので最上段に置く
            labelThickness.AutoSize = true;
            labelThickness.Location = new System.Drawing.Point(6, 8);
            labelThickness.Name = "labelThickness";
            labelThickness.Size = new System.Drawing.Size(64, 15);
            labelThickness.TabIndex = 0;
            labelThickness.Text = "Thickness";

            trackBarThickness.AutoSize = false;
            trackBarThickness.Location = new System.Drawing.Point(76, 4);
            trackBarThickness.Maximum = 0;
            trackBarThickness.Name = "trackBarThickness";
            trackBarThickness.Size = new System.Drawing.Size(240, 26);
            trackBarThickness.TabIndex = 1;
            trackBarThickness.TickStyle = TickStyle.BottomRight;
            trackBarThickness.Scroll += trackBarThickness_Scroll;

            labelThicknessValue.AutoSize = true;
            labelThicknessValue.Location = new System.Drawing.Point(322, 8);
            labelThicknessValue.Name = "labelThicknessValue";
            labelThicknessValue.Size = new System.Drawing.Size(50, 15);
            labelThicknessValue.TabIndex = 2;
            labelThicknessValue.Text = "-";

            labelNormalization.AutoSize = true;
            labelNormalization.Location = new System.Drawing.Point(6, 40);
            labelNormalization.Name = "labelNormalization";
            labelNormalization.Size = new System.Drawing.Size(84, 15);
            labelNormalization.TabIndex = 3;
            labelNormalization.Text = "Normalization";

            comboBoxNormalization.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNormalization.Location = new System.Drawing.Point(96, 36);
            comboBoxNormalization.Name = "comboBoxNormalization";
            comboBoxNormalization.Size = new System.Drawing.Size(180, 23);
            comboBoxNormalization.TabIndex = 4;
            comboBoxNormalization.SelectedIndexChanged += display_Changed;

            labelXAxis.AutoSize = true;
            labelXAxis.Location = new System.Drawing.Point(288, 40);
            labelXAxis.Name = "labelXAxis";
            labelXAxis.Size = new System.Drawing.Size(42, 15);
            labelXAxis.TabIndex = 5;
            labelXAxis.Text = "X axis";

            comboBoxXAxis.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxXAxis.Location = new System.Drawing.Point(336, 36);
            comboBoxXAxis.Name = "comboBoxXAxis";
            comboBoxXAxis.Size = new System.Drawing.Size(120, 23);
            comboBoxXAxis.TabIndex = 6;
            comboBoxXAxis.SelectedIndexChanged += display_Changed;

            checkBoxShowBragg.AutoSize = true;
            checkBoxShowBragg.Checked = true;
            checkBoxShowBragg.CheckState = CheckState.Checked;
            checkBoxShowBragg.Location = new System.Drawing.Point(468, 38);
            checkBoxShowBragg.Name = "checkBoxShowBragg";
            checkBoxShowBragg.Size = new System.Drawing.Size(150, 19);
            checkBoxShowBragg.TabIndex = 7;
            checkBoxShowBragg.Text = "Bragg conditions";
            checkBoxShowBragg.UseVisualStyleBackColor = true;
            checkBoxShowBragg.CheckedChanged += display_Changed;

            buttonExport.Location = new System.Drawing.Point(628, 34);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new System.Drawing.Size(110, 26);
            buttonExport.TabIndex = 8;
            buttonExport.Text = "Export CSV";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += buttonExport_Click;

            labelStats.AutoSize = true;
            labelStats.Location = new System.Drawing.Point(6, 66);
            labelStats.Name = "labelStats";
            labelStats.Size = new System.Drawing.Size(10, 15);
            labelStats.TabIndex = 9;
            labelStats.Text = "";

            labelBasis.AutoSize = true;
            labelBasis.Location = new System.Drawing.Point(6, 90);
            labelBasis.Name = "labelBasis";
            labelBasis.Size = new System.Drawing.Size(10, 15);
            labelBasis.TabIndex = 10;
            labelBasis.Text = "";
            #endregion

            #region ステータスバー
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripProgressBar, toolStripStatusLabel });
            statusStrip.Location = new System.Drawing.Point(0, 604);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new System.Drawing.Size(1180, 22);
            statusStrip.SizingGrip = false;
            statusStrip.TabIndex = 2;

            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new System.Drawing.Size(160, 16);

            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new System.Drawing.Size(20, 17);
            toolStripStatusLabel.Text = "   ";

            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
            #endregion

            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(1180, 626);
            Controls.Add(tabControl);
            Controls.Add(panelLeft);
            Controls.Add(statusStrip);
            MinimumSize = new System.Drawing.Size(1000, 560);
            Name = "FormALCHEMI";
            ShowIcon = false;
            Text = "ALCHEMI simulator";
            FormClosing += FormALCHEMI_FormClosing;

            panelLeft.ResumeLayout(false);
            groupBoxScan.ResumeLayout(false);
            groupBoxScan.PerformLayout();
            groupBoxThickness.ResumeLayout(false);
            groupBoxCalculation.ResumeLayout(false);
            groupBoxCalculation.PerformLayout();
            groupBoxChannels.ResumeLayout(false);
            groupBoxSites.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabPageCurve.ResumeLayout(false);
            panelCurveFooter.ResumeLayout(false);
            panelCurveFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarThickness).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolTip toolTip;
        private Panel panelLeft;
        private GroupBox groupBoxScan;
        private NumericBox numericBoxAxisH, numericBoxAxisK, numericBoxAxisL, numericBoxRange, numericBoxPoints;
        private Label labelThetaB;
        private GroupBox groupBoxThickness;
        private NumericBox numericBoxThicknessStart, numericBoxThicknessEnd, numericBoxThicknessStep;
        private GroupBox groupBoxCalculation;
        private NumericBox numericBoxMaxNumOfBloch;
        private CheckBox checkBoxDechannelling;
        private Label labelSolver;
        private ComboBox comboBoxSolver;
        private GroupBox groupBoxChannels;
        private CheckedListBox checkedListBoxChannels;
        private GroupBox groupBoxSites;
        private CheckedListBox checkedListBoxSites;
        private Button buttonSimulate, buttonStop;
        private TabControl tabControl;
        private TabPage tabPageCurve, tabPage2DMap, tabPageFit;
        private GraphControl graphControl;
        private Panel panelCurveFooter;
        private Label labelThickness, labelThicknessValue, labelNormalization, labelXAxis, labelStats, labelBasis;
        private TrackBar trackBarThickness;
        private ComboBox comboBoxNormalization, comboBoxXAxis;
        private CheckBox checkBoxShowBragg;
        private Button buttonExport;
        private StatusStrip statusStrip;
        private ToolStripProgressBar toolStripProgressBar;
        private ToolStripStatusLabel toolStripStatusLabel;
        private SaveFileDialog saveFileDialog;
    }
}

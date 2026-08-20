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
            //260820Cl 変更 (作者指示): 反射指数 (hkl) の入力は FormMain 等と同じ IndexControl に統一
            //numericBoxAxisH = new NumericBox();
            //numericBoxAxisK = new NumericBox();
            //numericBoxAxisL = new NumericBox();
            labelRow = new Label();
            indexControlRow = new IndexControl();
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
            labelAngularSpread = new Label();//260809Cl 追加
            comboBoxAngularSpread = new ComboBox();//260809Cl 追加
            numericBoxSpreadFwhm = new NumericBox();//260809Cl 追加
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
            //260820Cl 変更 (作者指示): 厚みセレクタ (Label + TrackBar + Label) を NumericBox 1 個に統合
            //labelThickness = new Label();
            //trackBarThickness = new TrackBar();
            //labelThicknessValue = new Label();
            numericBoxThickness = new NumericBox();
            labelNormalization = new Label();
            comboBoxNormalization = new ComboBox();
            labelXAxis = new Label();
            comboBoxXAxis = new ComboBox();
            checkBoxShowBragg = new CheckBox();
            buttonExport = new Button();
            //260820Cl 変更 (作者決定): labelStats / labelBasis を廃止し、ReadOnly・Multiline・縦スクロールの TextBox 1 個に統合
            //labelStats = new Label();
            //labelBasis = new Label();
            textBoxDiagnostics = new TextBox();
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
            //((System.ComponentModel.ISupportInitialize)trackBarThickness).BeginInit(); //260820Cl 廃止
            statusStrip.SuspendLayout();
            SuspendLayout();

            #region 左ペイン: 走査
            //groupBoxScan.Controls.Add(numericBoxAxisH); //260820Cl 廃止
            //groupBoxScan.Controls.Add(numericBoxAxisK); //260820Cl 廃止
            //groupBoxScan.Controls.Add(numericBoxAxisL); //260820Cl 廃止
            groupBoxScan.Controls.Add(labelRow); //260820Cl 追加
            groupBoxScan.Controls.Add(indexControlRow); //260820Cl 追加
            groupBoxScan.Controls.Add(numericBoxRange);
            groupBoxScan.Controls.Add(numericBoxPoints);
            groupBoxScan.Controls.Add(labelThetaB);
            groupBoxScan.Location = new System.Drawing.Point(6, 4);
            groupBoxScan.Name = "groupBoxScan";
            //groupBoxScan.Size = new System.Drawing.Size(346, 104); //260820Cl 変更: IndexControl (h k l ヘッダ付き・高さ 41) のぶん +16
            groupBoxScan.Size = new System.Drawing.Size(346, 120);
            groupBoxScan.TabIndex = 0;
            groupBoxScan.TabStop = false;
            groupBoxScan.Text = "Rocking scan";

            //260820Cl 変更 (作者指示): NumericBox 3 個 (旧ヘッダ "Tilt axis (" は誤解を招く — 傾斜軸は g に垂直な別ベクトル) を
            //廃止し、IndexControl (Mode=Plane → "(h k l)" ヘッダ付き) に置き換えた。入力は系統反射列 g の反射指数であって
            //方向指数 [uvw] ではないことが見た目で分かる。
            //numericBoxAxisH.DecimalPlaces = 0;
            //numericBoxAxisH.HeaderText = "Tilt axis  (";
            //numericBoxAxisH.Location = new System.Drawing.Point(8, 20);
            //numericBoxAxisH.Maximum = 20D;
            //numericBoxAxisH.Minimum = -20D;
            //numericBoxAxisH.Name = "numericBoxAxisH";
            //numericBoxAxisH.Size = new System.Drawing.Size(112, 22);
            //numericBoxAxisH.TabIndex = 0;
            //numericBoxAxisH.Value = 1D;//既定は (1 0 0) 系統反射列
            //
            //numericBoxAxisK.DecimalPlaces = 0;
            //numericBoxAxisK.Location = new System.Drawing.Point(120, 20);
            //numericBoxAxisK.Maximum = 20D;
            //numericBoxAxisK.Minimum = -20D;
            //numericBoxAxisK.Name = "numericBoxAxisK";
            //numericBoxAxisK.Size = new System.Drawing.Size(52, 22);
            //numericBoxAxisK.TabIndex = 1;
            //numericBoxAxisK.Value = 0D;
            //
            //numericBoxAxisL.DecimalPlaces = 0;
            //numericBoxAxisL.FooterText = ")";
            //numericBoxAxisL.Location = new System.Drawing.Point(172, 20);
            //numericBoxAxisL.Maximum = 20D;
            //numericBoxAxisL.Minimum = -20D;
            //numericBoxAxisL.Name = "numericBoxAxisL";
            //numericBoxAxisL.Size = new System.Drawing.Size(66, 22);
            //numericBoxAxisL.TabIndex = 2;
            //numericBoxAxisL.Value = 0D;

            labelRow.AutoSize = true;
            labelRow.Location = new System.Drawing.Point(8, 36);
            labelRow.Name = "labelRow";
            labelRow.Size = new System.Drawing.Size(50, 15);
            labelRow.TabIndex = 0;
            labelRow.Text = "Row  g =";

            indexControlRow.BoxWidth = 40;
            indexControlRow.Location = new System.Drawing.Point(100, 16);
            indexControlRow.Maximum = 20;
            indexControlRow.Mode = IndexControl.ModeEnum.Plane;
            indexControlRow.Name = "indexControlRow";
            indexControlRow.Size = new System.Drawing.Size(160, 41); //AutoSize=true なので実幅は BoxWidth から決まる (この値は見た目に効かない)
            indexControlRow.TabIndex = 1;
            //indexControlRow.Values = (1, 0, 0); //260820Cl 変更: タプルリテラルは VS デザイナー (CodeDOM) が解釈できず「170 行のコードを処理できません」になる
            indexControlRow.Values = new System.ValueTuple<int, int, int>(1, 0, 0);//既定は (1 0 0) 系統反射列。HKLValuesConverter の InstanceDescriptor が生成するのと同じ形

            numericBoxRange.DecimalPlaces = 2;
            numericBoxRange.FooterText = "mrad";
            numericBoxRange.HeaderText = "Range  ±";
            //numericBoxRange.Location = new System.Drawing.Point(8, 46); //260820Cl 変更 (+16)
            numericBoxRange.Location = new System.Drawing.Point(8, 62);
            numericBoxRange.Maximum = 60D;
            numericBoxRange.Minimum = 0.01D;
            numericBoxRange.Name = "numericBoxRange";
            numericBoxRange.ShowUpDown = true; //260820Cl 追加 (他フォームと同じ慣習)
            numericBoxRange.SmartIncrement = true; //260820Cl 追加
            numericBoxRange.Size = new System.Drawing.Size(178, 22);
            numericBoxRange.TabIndex = 3;
            numericBoxRange.Value = 8D;

            numericBoxPoints.DecimalPlaces = 0;
            numericBoxPoints.HeaderText = "Points";
            //numericBoxPoints.Location = new System.Drawing.Point(190, 46); //260820Cl 変更 (+16)
            numericBoxPoints.Location = new System.Drawing.Point(190, 62);
            numericBoxPoints.Maximum = 1001D;
            numericBoxPoints.Minimum = 3D;
            numericBoxPoints.Name = "numericBoxPoints";
            numericBoxPoints.ShowUpDown = true; //260820Cl 追加 (他フォームと同じ慣習)
            //numericBoxPoints.SmartIncrement = true; //260820Cl 追加 → /simplify2 で撤回: 101→110 と偶数へ誘導され θ=0 の標本が消える
            numericBoxPoints.UpDown_Increment = 2D; //260820Cl 追加 (/simplify2): 奇偶を保つ刻み
            numericBoxPoints.Size = new System.Drawing.Size(148, 22);
            numericBoxPoints.TabIndex = 4;
            numericBoxPoints.Value = 101D;

            labelThetaB.AutoSize = true;
            //labelThetaB.Location = new System.Drawing.Point(8, 76); //260820Cl 変更 (+16)
            labelThetaB.Location = new System.Drawing.Point(8, 92);
            labelThetaB.Name = "labelThetaB";
            labelThetaB.Size = new System.Drawing.Size(60, 15);
            labelThetaB.TabIndex = 5;
            labelThetaB.Text = "-";
            #endregion

            #region 左ペイン: 厚み
            groupBoxThickness.Controls.Add(numericBoxThicknessStart);
            groupBoxThickness.Controls.Add(numericBoxThicknessEnd);
            groupBoxThickness.Controls.Add(numericBoxThicknessStep);
            //groupBoxThickness.Location = new System.Drawing.Point(6, 112); //260820Cl 変更 (+16)
            groupBoxThickness.Location = new System.Drawing.Point(6, 128);
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
            numericBoxThicknessStart.ShowUpDown = true; //260820Cl 追加 (他フォームと同じ慣習)
            numericBoxThicknessStart.SmartIncrement = true; //260820Cl 追加
            numericBoxThicknessStart.Size = new System.Drawing.Size(106, 22);
            numericBoxThicknessStart.TabIndex = 0;
            numericBoxThicknessStart.Value = 10D;

            numericBoxThicknessEnd.DecimalPlaces = 1;
            numericBoxThicknessEnd.HeaderText = "to";
            numericBoxThicknessEnd.Location = new System.Drawing.Point(114, 20);
            numericBoxThicknessEnd.Maximum = 10000D;
            numericBoxThicknessEnd.Minimum = 0D;
            numericBoxThicknessEnd.Name = "numericBoxThicknessEnd";
            numericBoxThicknessEnd.ShowUpDown = true; //260820Cl 追加 (他フォームと同じ慣習)
            numericBoxThicknessEnd.SmartIncrement = true; //260820Cl 追加
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
            numericBoxThicknessStep.ShowUpDown = true; //260820Cl 追加 (他フォームと同じ慣習)
            numericBoxThicknessStep.SmartIncrement = true; //260820Cl 追加
            numericBoxThicknessStep.Size = new System.Drawing.Size(128, 22);
            numericBoxThicknessStep.TabIndex = 2;
            numericBoxThicknessStep.Value = 10D;
            #endregion

            #region 左ペイン: 計算条件
            groupBoxCalculation.Controls.Add(numericBoxMaxNumOfBloch);
            groupBoxCalculation.Controls.Add(checkBoxDechannelling);
            groupBoxCalculation.Controls.Add(labelSolver);
            groupBoxCalculation.Controls.Add(comboBoxSolver);
            groupBoxCalculation.Controls.Add(labelAngularSpread);//260809Cl 追加
            groupBoxCalculation.Controls.Add(comboBoxAngularSpread);//260809Cl 追加
            groupBoxCalculation.Controls.Add(numericBoxSpreadFwhm);//260809Cl 追加
            //groupBoxCalculation.Location = new System.Drawing.Point(6, 166); //260820Cl 変更 (+16)
            groupBoxCalculation.Location = new System.Drawing.Point(6, 182);
            groupBoxCalculation.Name = "groupBoxCalculation";
            //260809Cl 変更: 角度広がりの行を足したので 78 → 104 (旧: new System.Drawing.Size(346, 78))。
            //下の groupBoxChannels / groupBoxSites を 13px ずつ縮めて吸収し、フォーム高さは変えない
            groupBoxCalculation.Size = new System.Drawing.Size(346, 104);
            groupBoxCalculation.TabIndex = 2;
            groupBoxCalculation.TabStop = false;
            groupBoxCalculation.Text = "Calculation";

            numericBoxMaxNumOfBloch.DecimalPlaces = 0;
            numericBoxMaxNumOfBloch.HeaderText = "Max. beams";
            numericBoxMaxNumOfBloch.Location = new System.Drawing.Point(8, 20);
            //260810Cl 変更: 2000 → 1600 (作者判断)。1600 は F(s) テーブルの s グリッド上限 16 Å⁻¹ と
            //対になる設計目標である。AlchemiCheck basis の実測では **N = 1600 での要求 s の最大は
            //10.54 Å⁻¹** (β-AlCo a=2.861 Å、80–400 kV で E0 非依存) なので、ここで打ち切る限り
            //基底が収録範囲 16 Å⁻¹ を使い切ることはない (余裕 1.52×)。上限を上げる場合は
            //テーブル側の s グリッドと必ずセットで見直すこと
            numericBoxMaxNumOfBloch.Maximum = 1600D;
            numericBoxMaxNumOfBloch.Minimum = 1D;
            numericBoxMaxNumOfBloch.Name = "numericBoxMaxNumOfBloch";
            numericBoxMaxNumOfBloch.ShowUpDown = true; //260820Cl 追加 (他フォームと同じ慣習)
            //numericBoxMaxNumOfBloch.SmartIncrement = true; //260820Cl 追加 → /simplify2 で撤回: 非グリッド値 (155 等) で 1 クリックが増分以外の量 (有効 2 桁への丸め) を動かす
            numericBoxMaxNumOfBloch.UpDown_Increment = 10D; //260820Cl 追加 (/simplify2)
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

            //260809Cl 追加: 角度広がり (設計 §3.6、指示書 ①)。方位軸上の後処理なので engine には渡さない。
            //処理順は「畳み込み → 規格化」に固定してあるので、曲線タブの規格化より前段に置く
            labelAngularSpread.AutoSize = true;
            labelAngularSpread.Location = new System.Drawing.Point(8, 78);
            labelAngularSpread.Name = "labelAngularSpread";
            labelAngularSpread.Size = new System.Drawing.Size(90, 15);
            labelAngularSpread.TabIndex = 4;
            labelAngularSpread.Text = "Angular spread";

            comboBoxAngularSpread.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAngularSpread.Location = new System.Drawing.Point(112, 74);
            comboBoxAngularSpread.Name = "comboBoxAngularSpread";
            //comboBoxAngularSpread.Size = new System.Drawing.Size(100, 23); //260820Cl 変更: FWHM にスピンが付いた分を譲る
            comboBoxAngularSpread.Size = new System.Drawing.Size(86, 23);
            comboBoxAngularSpread.TabIndex = 5;

            numericBoxSpreadFwhm.DecimalPlaces = 2;
            numericBoxSpreadFwhm.Enabled = false;//既定 = None
            numericBoxSpreadFwhm.FooterText = "mrad";
            numericBoxSpreadFwhm.HeaderText = "FWHM";
            //numericBoxSpreadFwhm.Location = new System.Drawing.Point(216, 74); //260820Cl 変更
            numericBoxSpreadFwhm.Location = new System.Drawing.Point(202, 74);
            numericBoxSpreadFwhm.Maximum = 20D;
            numericBoxSpreadFwhm.Minimum = 0.01D;
            numericBoxSpreadFwhm.Name = "numericBoxSpreadFwhm";
            numericBoxSpreadFwhm.ShowUpDown = true; //260820Cl 追加 (他フォームと同じ慣習)
            numericBoxSpreadFwhm.SmartIncrement = true; //260820Cl 追加
            //numericBoxSpreadFwhm.Size = new System.Drawing.Size(122, 22); //260820Cl 変更: スピン分 +14
            numericBoxSpreadFwhm.Size = new System.Drawing.Size(136, 22);
            numericBoxSpreadFwhm.TabIndex = 6;
            numericBoxSpreadFwhm.Value = 1D;
            #endregion

            #region 左ペイン: チャネル / サイト
            groupBoxChannels.Controls.Add(checkedListBoxChannels);
            //260809Cl 変更: groupBoxCalculation が 26px 伸びたぶんを、下の 2 リストを 13px ずつ縮めて吸収する
            //(旧: Location(6, 248) / Size(346, 156))。フォーム高さと Simulate ボタンの位置は不変
            //260820Cl 変更: groupBoxScan が 16px 伸びたぶんをチャネル一覧で吸収 (旧: Location(6, 274) / Size(346, 143))。
            //groupBoxSites 以下と Simulate ボタンの位置は不変
            groupBoxChannels.Location = new System.Drawing.Point(6, 290);
            groupBoxChannels.Name = "groupBoxChannels";
            groupBoxChannels.Size = new System.Drawing.Size(346, 127);
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
            groupBoxSites.Location = new System.Drawing.Point(6, 421);//260809Cl 変更 (旧: 408)
            groupBoxSites.Name = "groupBoxSites";
            groupBoxSites.Size = new System.Drawing.Size(346, 137);//260809Cl 変更 (旧: 150)
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
            //260820Cl 追加 (作者指示): 他フォーム (CBED / EBSD / ImageSimulator / Trajectory / DynamicCompression) と同じ主要アクション色
            buttonSimulate.BackColor = System.Drawing.Color.SteelBlue;
            buttonSimulate.ForeColor = System.Drawing.Color.White;
            buttonSimulate.Location = new System.Drawing.Point(6, 566);
            buttonSimulate.Name = "buttonSimulate";
            buttonSimulate.Size = new System.Drawing.Size(240, 30);
            buttonSimulate.TabIndex = 5;
            buttonSimulate.Text = "Simulate";
            //buttonSimulate.UseVisualStyleBackColor = true; //260820Cl 変更: BackColor を効かせるため false
            buttonSimulate.UseVisualStyleBackColor = false;
            buttonSimulate.Click += buttonSimulate_Click;

            //260820Cl 追加 (作者指示): 他フォームと同じ停止ボタン色
            buttonStop.BackColor = System.Drawing.Color.IndianRed;
            buttonStop.ForeColor = System.Drawing.Color.White;
            buttonStop.Location = new System.Drawing.Point(252, 566);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new System.Drawing.Size(100, 30);
            buttonStop.TabIndex = 6;
            buttonStop.Text = "Stop";
            //buttonStop.UseVisualStyleBackColor = true; //260820Cl 変更
            buttonStop.UseVisualStyleBackColor = false;
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

            //panelCurveFooter.Controls.Add(labelThickness); //260820Cl 廃止
            //panelCurveFooter.Controls.Add(trackBarThickness); //260820Cl 廃止
            //panelCurveFooter.Controls.Add(labelThicknessValue); //260820Cl 廃止
            panelCurveFooter.Controls.Add(numericBoxThickness); //260820Cl 追加
            panelCurveFooter.Controls.Add(labelNormalization);
            panelCurveFooter.Controls.Add(comboBoxNormalization);
            panelCurveFooter.Controls.Add(labelXAxis);
            panelCurveFooter.Controls.Add(comboBoxXAxis);
            panelCurveFooter.Controls.Add(checkBoxShowBragg);
            panelCurveFooter.Controls.Add(buttonExport);
            //panelCurveFooter.Controls.Add(labelStats); //260820Cl 廃止
            //panelCurveFooter.Controls.Add(labelBasis); //260820Cl 廃止
            panelCurveFooter.Controls.Add(textBoxDiagnostics); //260820Cl 追加
            panelCurveFooter.Dock = DockStyle.Bottom;
            panelCurveFooter.Name = "panelCurveFooter";
            //panelCurveFooter.Size = new System.Drawing.Size(200, 116); //260820Cl 変更: 診断テキストボックス 4 行分 (下余白込み) に合わせて 116→136
            panelCurveFooter.Size = new System.Drawing.Size(200, 136);
            panelCurveFooter.TabIndex = 1;

            //厚みセレクタ — 実曲線を見て分かったとおり、サイト信号は厚みで符号すら変わるので最上段に置く
            //260820Cl 変更 (作者指示): Label + TrackBar + Label の 3 個を NumericBox 1 個へ。スピンで計算済み厚みを順送りし
            //(UpDown_Increment は run 後に刻みへ設定)、直接入力は最寄りの計算済み厚みへスナップする (FormALCHEMI.cs DrawCurves)。
            //labelThickness.AutoSize = true;
            //labelThickness.Location = new System.Drawing.Point(6, 8);
            //labelThickness.Name = "labelThickness";
            //labelThickness.Size = new System.Drawing.Size(64, 15);
            //labelThickness.TabIndex = 0;
            //labelThickness.Text = "Thickness";
            //
            //trackBarThickness.AutoSize = false;
            //trackBarThickness.Location = new System.Drawing.Point(76, 4);
            //trackBarThickness.Maximum = 0;
            //trackBarThickness.Name = "trackBarThickness";
            //trackBarThickness.Size = new System.Drawing.Size(240, 26);
            //trackBarThickness.TabIndex = 1;
            //trackBarThickness.TickStyle = TickStyle.BottomRight;
            //trackBarThickness.Scroll += trackBarThickness_Scroll;
            //
            //labelThicknessValue.AutoSize = true;
            //labelThicknessValue.Location = new System.Drawing.Point(322, 8);
            //labelThicknessValue.Name = "labelThicknessValue";
            //labelThicknessValue.Size = new System.Drawing.Size(50, 15);
            //labelThicknessValue.TabIndex = 2;
            //labelThicknessValue.Text = "-";

            numericBoxThickness.DecimalPlaces = 1;
            numericBoxThickness.FooterText = "nm";
            numericBoxThickness.HeaderText = "Thickness";
            numericBoxThickness.Location = new System.Drawing.Point(6, 6);
            numericBoxThickness.Name = "numericBoxThickness";
            numericBoxThickness.ShowUpDown = true;
            numericBoxThickness.Size = new System.Drawing.Size(200, 22);
            numericBoxThickness.TabIndex = 0;
            numericBoxThickness.ValueChanged += numericBoxThickness_ValueChanged;

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

            //260820Cl 変更 (作者決定): AutoSize 1 行ラベル 2 本 (labelStats / labelBasis) は親パネル幅を超えた分が黙って
            //クリップされ、⑧⑬ で「全 run に出す」と決めた Experimental タグや条件付き警告が画面外に出ていた。
            //ReadOnly・Multiline・縦スクロールの TextBox 1 個に統合し、幅は左右 Anchor で親に追従させる。
            //labelStats.AutoSize = true;
            //labelStats.Location = new System.Drawing.Point(6, 66);
            //labelStats.Name = "labelStats";
            //labelStats.Size = new System.Drawing.Size(10, 15);
            //labelStats.TabIndex = 9;
            //labelStats.Text = "";
            //
            //labelBasis.AutoSize = true;
            //labelBasis.Location = new System.Drawing.Point(6, 90);
            //labelBasis.Name = "labelBasis";
            //labelBasis.Size = new System.Drawing.Size(10, 15);
            //labelBasis.TabIndex = 10;
            //labelBasis.Text = "";

            textBoxDiagnostics.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDiagnostics.Location = new System.Drawing.Point(6, 66);
            textBoxDiagnostics.Multiline = true;
            textBoxDiagnostics.Name = "textBoxDiagnostics";
            textBoxDiagnostics.ReadOnly = true;
            textBoxDiagnostics.ScrollBars = ScrollBars.Vertical;
            textBoxDiagnostics.Size = new System.Drawing.Size(188, 64); //260820Cl 4 行分 (15 px × 4 + 枠)
            textBoxDiagnostics.TabIndex = 9;
            textBoxDiagnostics.TabStop = false;
            textBoxDiagnostics.Text = "";
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
            //((System.ComponentModel.ISupportInitialize)trackBarThickness).EndInit(); //260820Cl 廃止
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolTip toolTip;
        private Panel panelLeft;
        private GroupBox groupBoxScan;
        //private NumericBox numericBoxAxisH, numericBoxAxisK, numericBoxAxisL, numericBoxRange, numericBoxPoints; //260820Cl 変更
        private NumericBox numericBoxRange, numericBoxPoints;
        private Label labelRow; //260820Cl 追加
        private IndexControl indexControlRow; //260820Cl 追加: 系統反射列 g の反射指数 (hkl)
        private Label labelThetaB;
        private GroupBox groupBoxThickness;
        private NumericBox numericBoxThicknessStart, numericBoxThicknessEnd, numericBoxThicknessStep;
        private GroupBox groupBoxCalculation;
        private NumericBox numericBoxMaxNumOfBloch;
        private CheckBox checkBoxDechannelling;
        private Label labelSolver;
        private ComboBox comboBoxSolver;
        private Label labelAngularSpread;//260809Cl 追加
        private ComboBox comboBoxAngularSpread;//260809Cl 追加
        private NumericBox numericBoxSpreadFwhm;//260809Cl 追加
        private GroupBox groupBoxChannels;
        private CheckedListBox checkedListBoxChannels;
        private GroupBox groupBoxSites;
        private CheckedListBox checkedListBoxSites;
        private Button buttonSimulate, buttonStop;
        private TabControl tabControl;
        private TabPage tabPageCurve, tabPage2DMap, tabPageFit;
        private GraphControl graphControl;
        private Panel panelCurveFooter;
        //private Label labelThickness, labelThicknessValue, labelNormalization, labelXAxis, labelStats, labelBasis; //260820Cl 変更
        //private Label labelThickness, labelThicknessValue, labelNormalization, labelXAxis; //260820Cl 変更
        private Label labelNormalization, labelXAxis;
        private NumericBox numericBoxThickness; //260820Cl 追加: グラフに表示する厚み (計算済みの値を順送り)
        private TextBox textBoxDiagnostics; //260820Cl 追加: 基底診断・Experimental タグ・警告・コントラスト統計をまとめて出す
        //private TrackBar trackBarThickness; //260820Cl 廃止
        private ComboBox comboBoxNormalization, comboBoxXAxis;
        private CheckBox checkBoxShowBragg;
        private Button buttonExport;
        private StatusStrip statusStrip;
        private ToolStripProgressBar toolStripProgressBar;
        private ToolStripStatusLabel toolStripStatusLabel;
        private SaveFileDialog saveFileDialog;
    }
}

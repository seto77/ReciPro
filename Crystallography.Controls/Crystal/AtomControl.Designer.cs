namespace Crystallography.Controls
{
    partial class AtomControl
    {
        /// <summary>必要なデザイナ変数です。</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>使用中のリソースをすべてクリーンアップします。</summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AtomControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            radioButtonIsotoropy = new System.Windows.Forms.RadioButton();
            radioButtonAnisotropy = new System.Windows.Forms.RadioButton();
            flowLayoutPanelIso = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxBiso = new NumericBox();
            numericBoxBisoerr = new NumericBox();
            labelX_ = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            textBoxLabel = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            comboBoxAtom = new System.Windows.Forms.ComboBox();
            checkBoxDetailAtomicPositionError = new System.Windows.Forms.CheckBox();
            tabControl = new System.Windows.Forms.TabControl();
            tabPageElementAndPosition = new System.Windows.Forms.TabPage();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            numericBoxOccerr = new NumericBox();
            numericBoxOcc = new NumericBox();
            numericBoxZ = new NumericBox();
            numericBoxYerr = new NumericBox();
            numericBoxXerr = new NumericBox();
            numericBoxY = new NumericBox();
            numericBoxX = new NumericBox();
            numericBoxZerr = new NumericBox();
            tabPageOriginShift = new System.Windows.Forms.TabPage();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonOriginShiftPlus = new System.Windows.Forms.RadioButton();
            radioButtonOriginShiftMinus = new System.Windows.Forms.RadioButton();
            buttonOriginShift1 = new System.Windows.Forms.Button();
            buttonOriginShift2 = new System.Windows.Forms.Button();
            buttonOriginShift8 = new System.Windows.Forms.Button();
            buttonOriginShift7 = new System.Windows.Forms.Button();
            buttonOriginShift6 = new System.Windows.Forms.Button();
            buttonOriginShift5 = new System.Windows.Forms.Button();
            buttonOriginShift4 = new System.Windows.Forms.Button();
            buttonOriginShift3 = new System.Windows.Forms.Button();
            buttonOriginShift9 = new System.Windows.Forms.Button();
            label7 = new System.Windows.Forms.Label();
            buttonOriginShiftCustom = new System.Windows.Forms.Button();
            numericBoxOriginShiftZ = new NumericBox();
            numericBoxOriginShiftY = new NumericBox();
            numericBoxOriginShiftX = new NumericBox();
            tabPageDebyeWaller = new System.Windows.Forms.TabPage();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            label14 = new System.Windows.Forms.Label();
            labelDimension = new System.Windows.Forms.Label();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            label3 = new System.Windows.Forms.Label();
            radioButtonDebyeWallerTypeU = new System.Windows.Forms.RadioButton();
            radioButtonDebyeWallerTypeB = new System.Windows.Forms.RadioButton();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            label13 = new System.Windows.Forms.Label();
            checkBoxDetailsDebyeWallerError = new System.Windows.Forms.CheckBox();
            flowLayoutPanelAniso2 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxB22 = new NumericBox();
            numericBoxB22err = new NumericBox();
            numericBoxB23 = new NumericBox();
            numericBoxB23err = new NumericBox();
            numericBoxB33 = new NumericBox();
            numericBoxB33err = new NumericBox();
            flowLayoutPanelAniso1 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxB11 = new NumericBox();
            numericBoxB11err = new NumericBox();
            numericBoxB12 = new NumericBox();
            numericBoxB12err = new NumericBox();
            numericBoxB13 = new NumericBox();
            numericBoxB13err = new NumericBox();
            tabPageScatteringFactor = new System.Windows.Forms.TabPage();
            richTextBoxIsotope = new System.Windows.Forms.RichTextBox();
            label34 = new System.Windows.Forms.Label();
            buttonEditIsotopeAbundance = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            comboBoxScatteringFactorElectron = new System.Windows.Forms.ComboBox();
            comboBoxNeutron = new System.Windows.Forms.ComboBox();
            comboBoxScatteringFactorXray = new System.Windows.Forms.ComboBox();
            label6 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            tabPageAppearance = new System.Windows.Forms.TabPage();
            numericBoxAlpha = new NumericBox();
            numericBoxEmission = new NumericBox();
            numericBoxShininess = new NumericBox();
            numericBoxSpecular = new NumericBox();
            numericBoxDiffusion = new NumericBox();
            numericBoxAmbient = new NumericBox();
            checkBoxShowLabel = new System.Windows.Forms.CheckBox();
            label10 = new System.Windows.Forms.Label();
            label37 = new System.Windows.Forms.Label();
            label38 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label35 = new System.Windows.Forms.Label();
            label36 = new System.Windows.Forms.Label();
            numericBoxAtomRadius = new NumericBox();
            colorControlAtomColor = new ColorControl();
            toolTip = new System.Windows.Forms.ToolTip(components);
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            buttonApplyToSameElement = new System.Windows.Forms.Button();
            buttonAddAtom = new System.Windows.Forms.Button();
            buttonChange = new System.Windows.Forms.Button();
            buttonApplyToAllElements = new System.Windows.Forms.Button();
            buttonAtomUp = new System.Windows.Forms.Button();
            buttonAtomDown = new System.Windows.Forms.Button();
            buttonDeleteAtom = new System.Windows.Forms.Button();
            dataGridView = new DpiAwareDataGridView();
            enabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            labelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            elementDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            xDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            yDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            zDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            occDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            multiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            wyckLetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            siteSymDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bindingSource = new System.Windows.Forms.BindingSource(components);
            dataSet = new DataSet();
            panel1 = new System.Windows.Forms.Panel();
            flowLayoutPanelIso.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageElementAndPosition.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tabPageOriginShift.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            tabPageDebyeWaller.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanelAniso2.SuspendLayout();
            flowLayoutPanelAniso1.SuspendLayout();
            tabPageScatteringFactor.SuspendLayout();
            tabPageAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // radioButtonIsotoropy
            // 
            resources.ApplyResources(radioButtonIsotoropy, "radioButtonIsotoropy");
            toolTip.SetToolTip(radioButtonIsotoropy, resources.GetString("radioButtonIsotoropy.ToolTip")); // 260531Cl
            radioButtonIsotoropy.Checked = true;
            radioButtonIsotoropy.Name = "radioButtonIsotoropy";
            radioButtonIsotoropy.TabStop = true;
            radioButtonIsotoropy.CheckedChanged += radioButtonIsotoropy_CheckedChanged;
            // 
            // radioButtonAnisotropy
            // 
            resources.ApplyResources(radioButtonAnisotropy, "radioButtonAnisotropy");
            toolTip.SetToolTip(radioButtonAnisotropy, resources.GetString("radioButtonAnisotropy.ToolTip")); // 260531Cl
            radioButtonAnisotropy.Name = "radioButtonAnisotropy";
            radioButtonAnisotropy.CheckedChanged += radioButtonIsotoropy_CheckedChanged;
            // 
            // flowLayoutPanelIso
            // 
            resources.ApplyResources(flowLayoutPanelIso, "flowLayoutPanelIso");
            flowLayoutPanelIso.Controls.Add(numericBoxBiso);
            flowLayoutPanelIso.Controls.Add(numericBoxBisoerr);
            flowLayoutPanelIso.Name = "flowLayoutPanelIso";
            // 
            // numericBoxBiso
            // 
            resources.ApplyResources(numericBoxBiso, "numericBoxBiso");
            toolTip.SetToolTip(numericBoxBiso, resources.GetString("numericBoxBiso.ToolTip")); // 260531Cl
            numericBoxBiso.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBiso.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBiso.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBiso.Name = "numericBoxBiso";
            numericBoxBiso.RoundErrorAccuracy = 8;
            numericBoxBiso.SkipEventDuringInput = false;
            numericBoxBiso.SmartIncrement = true;
            numericBoxBiso.ValueFontSize = 9F;
            numericBoxBiso.ThousandsSeparator = true;
            // 
            // numericBoxBisoerr
            // 
            numericBoxBisoerr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBisoerr.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxBisoerr, "numericBoxBisoerr");
            toolTip.SetToolTip(numericBoxBisoerr, resources.GetString("numericBoxBisoerr.ToolTip")); // 260531Cl
            numericBoxBisoerr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBisoerr.Name = "numericBoxBisoerr";
            numericBoxBisoerr.RoundErrorAccuracy = 8;
            numericBoxBisoerr.SkipEventDuringInput = false;
            numericBoxBisoerr.SmartIncrement = true;
            numericBoxBisoerr.ValueFontSize = 9F;
            numericBoxBisoerr.ThousandsSeparator = true;
            // 
            // labelX_
            // 
            resources.ApplyResources(labelX_, "labelX_");
            labelX_.Name = "labelX_";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // textBoxLabel
            // 
            resources.ApplyResources(textBoxLabel, "textBoxLabel");
            textBoxLabel.Name = "textBoxLabel";
            toolTip.SetToolTip(textBoxLabel, resources.GetString("textBoxLabel.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // comboBoxAtom
            // 
            resources.ApplyResources(comboBoxAtom, "comboBoxAtom");
            comboBoxAtom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxAtom.Items.AddRange(new object[] { resources.GetString("comboBoxAtom.Items"), resources.GetString("comboBoxAtom.Items1"), resources.GetString("comboBoxAtom.Items2"), resources.GetString("comboBoxAtom.Items3"), resources.GetString("comboBoxAtom.Items4"), resources.GetString("comboBoxAtom.Items5"), resources.GetString("comboBoxAtom.Items6"), resources.GetString("comboBoxAtom.Items7"), resources.GetString("comboBoxAtom.Items8"), resources.GetString("comboBoxAtom.Items9"), resources.GetString("comboBoxAtom.Items10"), resources.GetString("comboBoxAtom.Items11"), resources.GetString("comboBoxAtom.Items12"), resources.GetString("comboBoxAtom.Items13"), resources.GetString("comboBoxAtom.Items14"), resources.GetString("comboBoxAtom.Items15"), resources.GetString("comboBoxAtom.Items16"), resources.GetString("comboBoxAtom.Items17"), resources.GetString("comboBoxAtom.Items18"), resources.GetString("comboBoxAtom.Items19"), resources.GetString("comboBoxAtom.Items20"), resources.GetString("comboBoxAtom.Items21"), resources.GetString("comboBoxAtom.Items22"), resources.GetString("comboBoxAtom.Items23"), resources.GetString("comboBoxAtom.Items24"), resources.GetString("comboBoxAtom.Items25"), resources.GetString("comboBoxAtom.Items26"), resources.GetString("comboBoxAtom.Items27"), resources.GetString("comboBoxAtom.Items28"), resources.GetString("comboBoxAtom.Items29"), resources.GetString("comboBoxAtom.Items30"), resources.GetString("comboBoxAtom.Items31"), resources.GetString("comboBoxAtom.Items32"), resources.GetString("comboBoxAtom.Items33"), resources.GetString("comboBoxAtom.Items34"), resources.GetString("comboBoxAtom.Items35"), resources.GetString("comboBoxAtom.Items36"), resources.GetString("comboBoxAtom.Items37"), resources.GetString("comboBoxAtom.Items38"), resources.GetString("comboBoxAtom.Items39"), resources.GetString("comboBoxAtom.Items40"), resources.GetString("comboBoxAtom.Items41"), resources.GetString("comboBoxAtom.Items42"), resources.GetString("comboBoxAtom.Items43"), resources.GetString("comboBoxAtom.Items44"), resources.GetString("comboBoxAtom.Items45"), resources.GetString("comboBoxAtom.Items46"), resources.GetString("comboBoxAtom.Items47"), resources.GetString("comboBoxAtom.Items48"), resources.GetString("comboBoxAtom.Items49"), resources.GetString("comboBoxAtom.Items50"), resources.GetString("comboBoxAtom.Items51"), resources.GetString("comboBoxAtom.Items52"), resources.GetString("comboBoxAtom.Items53"), resources.GetString("comboBoxAtom.Items54"), resources.GetString("comboBoxAtom.Items55"), resources.GetString("comboBoxAtom.Items56"), resources.GetString("comboBoxAtom.Items57"), resources.GetString("comboBoxAtom.Items58"), resources.GetString("comboBoxAtom.Items59"), resources.GetString("comboBoxAtom.Items60"), resources.GetString("comboBoxAtom.Items61"), resources.GetString("comboBoxAtom.Items62"), resources.GetString("comboBoxAtom.Items63"), resources.GetString("comboBoxAtom.Items64"), resources.GetString("comboBoxAtom.Items65"), resources.GetString("comboBoxAtom.Items66"), resources.GetString("comboBoxAtom.Items67"), resources.GetString("comboBoxAtom.Items68"), resources.GetString("comboBoxAtom.Items69"), resources.GetString("comboBoxAtom.Items70"), resources.GetString("comboBoxAtom.Items71"), resources.GetString("comboBoxAtom.Items72"), resources.GetString("comboBoxAtom.Items73"), resources.GetString("comboBoxAtom.Items74"), resources.GetString("comboBoxAtom.Items75"), resources.GetString("comboBoxAtom.Items76"), resources.GetString("comboBoxAtom.Items77"), resources.GetString("comboBoxAtom.Items78"), resources.GetString("comboBoxAtom.Items79"), resources.GetString("comboBoxAtom.Items80"), resources.GetString("comboBoxAtom.Items81"), resources.GetString("comboBoxAtom.Items82"), resources.GetString("comboBoxAtom.Items83"), resources.GetString("comboBoxAtom.Items84"), resources.GetString("comboBoxAtom.Items85"), resources.GetString("comboBoxAtom.Items86"), resources.GetString("comboBoxAtom.Items87"), resources.GetString("comboBoxAtom.Items88"), resources.GetString("comboBoxAtom.Items89"), resources.GetString("comboBoxAtom.Items90"), resources.GetString("comboBoxAtom.Items91"), resources.GetString("comboBoxAtom.Items92"), resources.GetString("comboBoxAtom.Items93"), resources.GetString("comboBoxAtom.Items94"), resources.GetString("comboBoxAtom.Items95"), resources.GetString("comboBoxAtom.Items96"), resources.GetString("comboBoxAtom.Items97") });
            comboBoxAtom.Name = "comboBoxAtom";
            toolTip.SetToolTip(comboBoxAtom, resources.GetString("comboBoxAtom.ToolTip"));
            comboBoxAtom.SelectedIndexChanged += comboBoxAtom_SelectedIndexChanged;
            // 
            // checkBoxDetailAtomicPositionError
            // 
            resources.ApplyResources(checkBoxDetailAtomicPositionError, "checkBoxDetailAtomicPositionError");
            toolTip.SetToolTip(checkBoxDetailAtomicPositionError, resources.GetString("checkBoxDetailAtomicPositionError.ToolTip")); // 260531Cl
            checkBoxDetailAtomicPositionError.Name = "checkBoxDetailAtomicPositionError";
            checkBoxDetailAtomicPositionError.UseVisualStyleBackColor = true;
            checkBoxDetailAtomicPositionError.CheckedChanged += checkBoxAtomicPositionError_CheckedChanged;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageElementAndPosition);
            tabControl.Controls.Add(tabPageOriginShift);
            tabControl.Controls.Add(tabPageDebyeWaller);
            tabControl.Controls.Add(tabPageScatteringFactor);
            tabControl.Controls.Add(tabPageAppearance);
            resources.ApplyResources(tabControl, "tabControl");
            tabControl.HotTrack = true;
            tabControl.Multiline = true;
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabPageElementAndPosition
            // 
            tabPageElementAndPosition.BackColor = System.Drawing.Color.Transparent;
            tabPageElementAndPosition.Controls.Add(tableLayoutPanel1);
            tabPageElementAndPosition.Controls.Add(checkBoxDetailAtomicPositionError);
            tabPageElementAndPosition.Controls.Add(labelX_);
            resources.ApplyResources(tabPageElementAndPosition, "tabPageElementAndPosition");
            tabPageElementAndPosition.Name = "tabPageElementAndPosition";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(numericBoxOccerr, 7, 0);
            tableLayoutPanel1.Controls.Add(numericBoxOcc, 6, 0);
            tableLayoutPanel1.Controls.Add(numericBoxZ, 3, 2);
            tableLayoutPanel1.Controls.Add(numericBoxYerr, 4, 1);
            tableLayoutPanel1.Controls.Add(numericBoxXerr, 4, 0);
            tableLayoutPanel1.Controls.Add(numericBoxY, 3, 1);
            tableLayoutPanel1.Controls.Add(numericBoxX, 3, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(textBoxLabel, 1, 0);
            tableLayoutPanel1.Controls.Add(comboBoxAtom, 1, 1);
            tableLayoutPanel1.Controls.Add(numericBoxZerr, 4, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // numericBoxOccerr
            // 
            numericBoxOccerr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxOccerr.DecimalPlaces = 6;
            numericBoxOccerr.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxOccerr, "numericBoxOccerr");
            toolTip.SetToolTip(numericBoxOccerr, resources.GetString("numericBoxOccerr.ToolTip")); // 260531Cl
            numericBoxOccerr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxOccerr.Name = "numericBoxOccerr";
            numericBoxOccerr.SkipEventDuringInput = false;
            numericBoxOccerr.SmartIncrement = true;
            numericBoxOccerr.ValueFontSize = 9F;
            numericBoxOccerr.ThousandsSeparator = true;
            numericBoxOccerr.TrimEndZero = true;
            // 
            // numericBoxOcc
            // 
            resources.ApplyResources(numericBoxOcc, "numericBoxOcc");
            numericBoxOcc.BackColor = System.Drawing.SystemColors.Control;
            numericBoxOcc.DecimalPlaces = 6;
            numericBoxOcc.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxOcc.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxOcc.Name = "numericBoxOcc";
            numericBoxOcc.ShowFraction = true;
            numericBoxOcc.SkipEventDuringInput = false;
            numericBoxOcc.SmartIncrement = true;
            numericBoxOcc.ValueFontSize = 9F;
            numericBoxOcc.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxOcc, resources.GetString("numericBoxOcc.ToolTip1"));
            numericBoxOcc.TrimEndZero = true;
            // 
            // numericBoxZ
            // 
            resources.ApplyResources(numericBoxZ, "numericBoxZ");
            numericBoxZ.BackColor = System.Drawing.SystemColors.Control;
            numericBoxZ.DecimalPlaces = 6;
            numericBoxZ.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxZ.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxZ.Name = "numericBoxZ";
            numericBoxZ.ShowFraction = true;
            numericBoxZ.SkipEventDuringInput = false;
            numericBoxZ.SmartIncrement = true;
            numericBoxZ.ValueFontSize = 9F;
            numericBoxZ.ThousandsSeparator = true;
            numericBoxZ.TrimEndZero = true;
            // 
            // numericBoxYerr
            // 
            numericBoxYerr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxYerr.DecimalPlaces = 6;
            numericBoxYerr.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxYerr, "numericBoxYerr");
            toolTip.SetToolTip(numericBoxYerr, resources.GetString("numericBoxYerr.ToolTip")); // 260531Cl
            numericBoxYerr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxYerr.Name = "numericBoxYerr";
            numericBoxYerr.SkipEventDuringInput = false;
            numericBoxYerr.SmartIncrement = true;
            numericBoxYerr.ValueFontSize = 9F;
            numericBoxYerr.ThousandsSeparator = true;
            numericBoxYerr.TrimEndZero = true;
            // 
            // numericBoxXerr
            // 
            numericBoxXerr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxXerr.DecimalPlaces = 6;
            numericBoxXerr.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxXerr, "numericBoxXerr");
            toolTip.SetToolTip(numericBoxXerr, resources.GetString("numericBoxXerr.ToolTip")); // 260531Cl
            numericBoxXerr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxXerr.Name = "numericBoxXerr";
            numericBoxXerr.SkipEventDuringInput = false;
            numericBoxXerr.SmartIncrement = true;
            numericBoxXerr.ValueFontSize = 9F;
            numericBoxXerr.ThousandsSeparator = true;
            numericBoxXerr.TrimEndZero = true;
            // 
            // numericBoxY
            // 
            resources.ApplyResources(numericBoxY, "numericBoxY");
            numericBoxY.BackColor = System.Drawing.SystemColors.Control;
            numericBoxY.DecimalPlaces = 6;
            numericBoxY.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxY.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxY.Name = "numericBoxY";
            numericBoxY.ShowFraction = true;
            numericBoxY.SkipEventDuringInput = false;
            numericBoxY.SmartIncrement = true;
            numericBoxY.ValueFontSize = 9F;
            numericBoxY.ThousandsSeparator = true;
            numericBoxY.TrimEndZero = true;
            // 
            // numericBoxX
            // 
            resources.ApplyResources(numericBoxX, "numericBoxX");
            numericBoxX.BackColor = System.Drawing.SystemColors.Control;
            numericBoxX.DecimalPlaces = 6;
            numericBoxX.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxX.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxX.Name = "numericBoxX";
            numericBoxX.ShowFraction = true;
            numericBoxX.SkipEventDuringInput = false;
            numericBoxX.SmartIncrement = true;
            numericBoxX.ValueFontSize = 9F;
            numericBoxX.ThousandsSeparator = true;
            numericBoxX.TrimEndZero = true;
            // 
            // numericBoxZerr
            // 
            numericBoxZerr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxZerr.DecimalPlaces = 6;
            numericBoxZerr.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxZerr, "numericBoxZerr");
            toolTip.SetToolTip(numericBoxZerr, resources.GetString("numericBoxZerr.ToolTip")); // 260531Cl
            numericBoxZerr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxZerr.Name = "numericBoxZerr";
            numericBoxZerr.SkipEventDuringInput = false;
            numericBoxZerr.SmartIncrement = true;
            numericBoxZerr.ValueFontSize = 9F;
            numericBoxZerr.ThousandsSeparator = true;
            numericBoxZerr.TrimEndZero = true;
            // 
            // tabPageOriginShift
            // 
            tabPageOriginShift.Controls.Add(flowLayoutPanel3);
            tabPageOriginShift.Controls.Add(label7);
            tabPageOriginShift.Controls.Add(buttonOriginShiftCustom);
            tabPageOriginShift.Controls.Add(numericBoxOriginShiftZ);
            tabPageOriginShift.Controls.Add(numericBoxOriginShiftY);
            tabPageOriginShift.Controls.Add(numericBoxOriginShiftX);
            resources.ApplyResources(tabPageOriginShift, "tabPageOriginShift");
            tabPageOriginShift.Name = "tabPageOriginShift";
            captureExtender.SetCapture(tabPageOriginShift, true); //260529Cl: マニュアル用にタブ単位クロップを撮る
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(radioButtonOriginShiftPlus);
            flowLayoutPanel3.Controls.Add(radioButtonOriginShiftMinus);
            flowLayoutPanel3.Controls.Add(buttonOriginShift1);
            flowLayoutPanel3.Controls.Add(buttonOriginShift2);
            flowLayoutPanel3.Controls.Add(buttonOriginShift8);
            flowLayoutPanel3.Controls.Add(buttonOriginShift7);
            flowLayoutPanel3.Controls.Add(buttonOriginShift6);
            flowLayoutPanel3.Controls.Add(buttonOriginShift5);
            flowLayoutPanel3.Controls.Add(buttonOriginShift4);
            flowLayoutPanel3.Controls.Add(buttonOriginShift3);
            flowLayoutPanel3.Controls.Add(buttonOriginShift9);
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // radioButtonOriginShiftPlus
            // 
            resources.ApplyResources(radioButtonOriginShiftPlus, "radioButtonOriginShiftPlus");
            toolTip.SetToolTip(radioButtonOriginShiftPlus, resources.GetString("radioButtonOriginShiftPlus.ToolTip")); // 260531Cl
            radioButtonOriginShiftPlus.Checked = true;
            radioButtonOriginShiftPlus.Name = "radioButtonOriginShiftPlus";
            radioButtonOriginShiftPlus.TabStop = true;
            radioButtonOriginShiftPlus.UseVisualStyleBackColor = true;
            // 
            // radioButtonOriginShiftMinus
            // 
            resources.ApplyResources(radioButtonOriginShiftMinus, "radioButtonOriginShiftMinus");
            toolTip.SetToolTip(radioButtonOriginShiftMinus, resources.GetString("radioButtonOriginShiftMinus.ToolTip")); // 260531Cl
            radioButtonOriginShiftMinus.Name = "radioButtonOriginShiftMinus";
            radioButtonOriginShiftMinus.UseVisualStyleBackColor = true;
            // 
            // buttonOriginShift1
            // 
            resources.ApplyResources(buttonOriginShift1, "buttonOriginShift1");
            toolTip.SetToolTip(buttonOriginShift1, resources.GetString("buttonOriginShift1.ToolTip")); // 260531Cl
            buttonOriginShift1.BackColor = System.Drawing.Color.MediumSeaGreen;
            buttonOriginShift1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOriginShift1.Name = "buttonOriginShift1";
            buttonOriginShift1.Tag = "0.125 0.125 0.125";
            buttonOriginShift1.UseVisualStyleBackColor = false;
            buttonOriginShift1.Click += buttonOriginShift_Click;
            // 
            // buttonOriginShift2
            // 
            resources.ApplyResources(buttonOriginShift2, "buttonOriginShift2");
            toolTip.SetToolTip(buttonOriginShift2, resources.GetString("buttonOriginShift2.ToolTip")); // 260531Cl
            buttonOriginShift2.BackColor = System.Drawing.Color.MediumSeaGreen;
            buttonOriginShift2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOriginShift2.Name = "buttonOriginShift2";
            buttonOriginShift2.Tag = "0.25 0.25 0.25";
            buttonOriginShift2.UseVisualStyleBackColor = false;
            buttonOriginShift2.Click += buttonOriginShift_Click;
            // 
            // buttonOriginShift8
            // 
            resources.ApplyResources(buttonOriginShift8, "buttonOriginShift8");
            toolTip.SetToolTip(buttonOriginShift8, resources.GetString("buttonOriginShift8.ToolTip")); // 260531Cl
            buttonOriginShift8.BackColor = System.Drawing.Color.MediumSeaGreen;
            buttonOriginShift8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOriginShift8.Name = "buttonOriginShift8";
            buttonOriginShift8.Tag = "0.25 -0.25 0.25";
            buttonOriginShift8.UseVisualStyleBackColor = false;
            buttonOriginShift8.Click += buttonOriginShift_Click;
            // 
            // buttonOriginShift7
            // 
            resources.ApplyResources(buttonOriginShift7, "buttonOriginShift7");
            toolTip.SetToolTip(buttonOriginShift7, resources.GetString("buttonOriginShift7.ToolTip")); // 260531Cl
            buttonOriginShift7.BackColor = System.Drawing.Color.MediumSeaGreen;
            buttonOriginShift7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOriginShift7.Name = "buttonOriginShift7";
            buttonOriginShift7.Tag = "0.25 -0.25 0";
            buttonOriginShift7.UseVisualStyleBackColor = false;
            buttonOriginShift7.Click += buttonOriginShift_Click;
            // 
            // buttonOriginShift6
            // 
            resources.ApplyResources(buttonOriginShift6, "buttonOriginShift6");
            toolTip.SetToolTip(buttonOriginShift6, resources.GetString("buttonOriginShift6.ToolTip")); // 260531Cl
            buttonOriginShift6.BackColor = System.Drawing.Color.MediumSeaGreen;
            buttonOriginShift6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOriginShift6.Name = "buttonOriginShift6";
            buttonOriginShift6.Tag = "0.25 0.25 0";
            buttonOriginShift6.UseVisualStyleBackColor = false;
            buttonOriginShift6.Click += buttonOriginShift_Click;
            // 
            // buttonOriginShift5
            // 
            resources.ApplyResources(buttonOriginShift5, "buttonOriginShift5");
            toolTip.SetToolTip(buttonOriginShift5, resources.GetString("buttonOriginShift5.ToolTip")); // 260531Cl
            buttonOriginShift5.BackColor = System.Drawing.Color.MediumSeaGreen;
            buttonOriginShift5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOriginShift5.Name = "buttonOriginShift5";
            buttonOriginShift5.Tag = "0 0.25 0.25";
            buttonOriginShift5.UseVisualStyleBackColor = false;
            buttonOriginShift5.Click += buttonOriginShift_Click;
            // 
            // buttonOriginShift4
            // 
            resources.ApplyResources(buttonOriginShift4, "buttonOriginShift4");
            toolTip.SetToolTip(buttonOriginShift4, resources.GetString("buttonOriginShift4.ToolTip")); // 260531Cl
            buttonOriginShift4.BackColor = System.Drawing.Color.MediumSeaGreen;
            buttonOriginShift4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOriginShift4.Name = "buttonOriginShift4";
            buttonOriginShift4.Tag = "0.25 0 0.25";
            buttonOriginShift4.UseVisualStyleBackColor = false;
            buttonOriginShift4.Click += buttonOriginShift_Click;
            // 
            // buttonOriginShift3
            // 
            resources.ApplyResources(buttonOriginShift3, "buttonOriginShift3");
            toolTip.SetToolTip(buttonOriginShift3, resources.GetString("buttonOriginShift3.ToolTip")); // 260531Cl
            buttonOriginShift3.BackColor = System.Drawing.Color.MediumSeaGreen;
            buttonOriginShift3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOriginShift3.Name = "buttonOriginShift3";
            buttonOriginShift3.Tag = "0 0.25 0.125";
            buttonOriginShift3.UseVisualStyleBackColor = false;
            buttonOriginShift3.Click += buttonOriginShift_Click;
            // 
            // buttonOriginShift9
            // 
            resources.ApplyResources(buttonOriginShift9, "buttonOriginShift9");
            toolTip.SetToolTip(buttonOriginShift9, resources.GetString("buttonOriginShift9.ToolTip")); // 260531Cl
            buttonOriginShift9.BackColor = System.Drawing.Color.MediumSeaGreen;
            buttonOriginShift9.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOriginShift9.Name = "buttonOriginShift9";
            buttonOriginShift9.Tag = "0 0.25 -0.125";
            buttonOriginShift9.UseVisualStyleBackColor = false;
            buttonOriginShift9.Click += buttonOriginShift_Click;
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip")); // 260531Cl
            label7.Name = "label7";
            // 
            // buttonOriginShiftCustom
            // 
            resources.ApplyResources(buttonOriginShiftCustom, "buttonOriginShiftCustom");
            toolTip.SetToolTip(buttonOriginShiftCustom, resources.GetString("buttonOriginShiftCustom.ToolTip")); // 260531Cl
            buttonOriginShiftCustom.BackColor = System.Drawing.Color.MediumSeaGreen;
            buttonOriginShiftCustom.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOriginShiftCustom.Name = "buttonOriginShiftCustom";
            buttonOriginShiftCustom.Tag = "+0.5";
            buttonOriginShiftCustom.UseVisualStyleBackColor = false;
            buttonOriginShiftCustom.Click += buttonOriginShift_Click;
            // 
            // numericBoxOriginShiftZ
            // 
            resources.ApplyResources(numericBoxOriginShiftZ, "numericBoxOriginShiftZ");
            toolTip.SetToolTip(numericBoxOriginShiftZ, resources.GetString("numericBoxOriginShiftZ.ToolTip")); // 260531Cl
            numericBoxOriginShiftZ.BackColor = System.Drawing.SystemColors.Control;
            numericBoxOriginShiftZ.DecimalPlaces = 4;
            numericBoxOriginShiftZ.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxOriginShiftZ.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxOriginShiftZ.Maximum = 1D;
            numericBoxOriginShiftZ.Minimum = -1D;
            numericBoxOriginShiftZ.Name = "numericBoxOriginShiftZ";
            numericBoxOriginShiftZ.ShowFraction = true;
            numericBoxOriginShiftZ.SkipEventDuringInput = false;
            numericBoxOriginShiftZ.SmartIncrement = true;
            numericBoxOriginShiftZ.ValueFontSize = 9F;
            numericBoxOriginShiftZ.ThousandsSeparator = true;
            // 
            // numericBoxOriginShiftY
            // 
            resources.ApplyResources(numericBoxOriginShiftY, "numericBoxOriginShiftY");
            toolTip.SetToolTip(numericBoxOriginShiftY, resources.GetString("numericBoxOriginShiftY.ToolTip")); // 260531Cl
            numericBoxOriginShiftY.BackColor = System.Drawing.SystemColors.Control;
            numericBoxOriginShiftY.DecimalPlaces = 4;
            numericBoxOriginShiftY.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxOriginShiftY.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxOriginShiftY.Maximum = 1D;
            numericBoxOriginShiftY.Minimum = -1D;
            numericBoxOriginShiftY.Name = "numericBoxOriginShiftY";
            numericBoxOriginShiftY.ShowFraction = true;
            numericBoxOriginShiftY.SkipEventDuringInput = false;
            numericBoxOriginShiftY.SmartIncrement = true;
            numericBoxOriginShiftY.ValueFontSize = 9F;
            numericBoxOriginShiftY.ThousandsSeparator = true;
            // 
            // numericBoxOriginShiftX
            // 
            resources.ApplyResources(numericBoxOriginShiftX, "numericBoxOriginShiftX");
            toolTip.SetToolTip(numericBoxOriginShiftX, resources.GetString("numericBoxOriginShiftX.ToolTip")); // 260531Cl
            numericBoxOriginShiftX.BackColor = System.Drawing.SystemColors.Control;
            numericBoxOriginShiftX.DecimalPlaces = 4;
            numericBoxOriginShiftX.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxOriginShiftX.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxOriginShiftX.Maximum = 1D;
            numericBoxOriginShiftX.Minimum = -1D;
            numericBoxOriginShiftX.Name = "numericBoxOriginShiftX";
            numericBoxOriginShiftX.ShowFraction = true;
            numericBoxOriginShiftX.SkipEventDuringInput = false;
            numericBoxOriginShiftX.SmartIncrement = true;
            numericBoxOriginShiftX.ValueFontSize = 9F;
            numericBoxOriginShiftX.ThousandsSeparator = true;
            // 
            // tabPageDebyeWaller
            // 
            tabPageDebyeWaller.BackColor = System.Drawing.Color.Transparent;
            tabPageDebyeWaller.Controls.Add(flowLayoutPanel6);
            tabPageDebyeWaller.Controls.Add(flowLayoutPanelIso);
            tabPageDebyeWaller.Controls.Add(flowLayoutPanel5);
            tabPageDebyeWaller.Controls.Add(flowLayoutPanel4);
            tabPageDebyeWaller.Controls.Add(checkBoxDetailsDebyeWallerError);
            tabPageDebyeWaller.Controls.Add(flowLayoutPanelAniso2);
            tabPageDebyeWaller.Controls.Add(flowLayoutPanelAniso1);
            resources.ApplyResources(tabPageDebyeWaller, "tabPageDebyeWaller");
            tabPageDebyeWaller.Name = "tabPageDebyeWaller";
            toolTip.SetToolTip(tabPageDebyeWaller, resources.GetString("tabPageDebyeWaller.ToolTip"));
            // 
            // flowLayoutPanel6
            // 
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Controls.Add(label14);
            flowLayoutPanel6.Controls.Add(labelDimension);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            toolTip.SetToolTip(label14, resources.GetString("label14.ToolTip")); // 260531Cl
            label14.Name = "label14";
            // 
            // labelDimension
            // 
            resources.ApplyResources(labelDimension, "labelDimension");
            toolTip.SetToolTip(labelDimension, resources.GetString("labelDimension.ToolTip")); // 260531Cl
            labelDimension.Name = "labelDimension";
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(label3);
            flowLayoutPanel5.Controls.Add(radioButtonDebyeWallerTypeU);
            flowLayoutPanel5.Controls.Add(radioButtonDebyeWallerTypeB);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip")); // 260531Cl
            label3.Name = "label3";
            // 
            // radioButtonDebyeWallerTypeU
            // 
            resources.ApplyResources(radioButtonDebyeWallerTypeU, "radioButtonDebyeWallerTypeU");
            radioButtonDebyeWallerTypeU.Name = "radioButtonDebyeWallerTypeU";
            toolTip.SetToolTip(radioButtonDebyeWallerTypeU, resources.GetString("radioButtonDebyeWallerTypeU.ToolTip"));
            radioButtonDebyeWallerTypeU.CheckedChanged += radioButtonDebyeWallerTypeU_CheckedChanged;
            // 
            // radioButtonDebyeWallerTypeB
            // 
            resources.ApplyResources(radioButtonDebyeWallerTypeB, "radioButtonDebyeWallerTypeB");
            radioButtonDebyeWallerTypeB.Checked = true;
            radioButtonDebyeWallerTypeB.Name = "radioButtonDebyeWallerTypeB";
            radioButtonDebyeWallerTypeB.TabStop = true;
            toolTip.SetToolTip(radioButtonDebyeWallerTypeB, resources.GetString("radioButtonDebyeWallerTypeB.ToolTip"));
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(label13);
            flowLayoutPanel4.Controls.Add(radioButtonIsotoropy);
            flowLayoutPanel4.Controls.Add(radioButtonAnisotropy);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip")); // 260531Cl
            label13.Name = "label13";
            // 
            // checkBoxDetailsDebyeWallerError
            // 
            resources.ApplyResources(checkBoxDetailsDebyeWallerError, "checkBoxDetailsDebyeWallerError");
            toolTip.SetToolTip(checkBoxDetailsDebyeWallerError, resources.GetString("checkBoxDetailsDebyeWallerError.ToolTip")); // 260531Cl
            checkBoxDetailsDebyeWallerError.Name = "checkBoxDetailsDebyeWallerError";
            checkBoxDetailsDebyeWallerError.UseVisualStyleBackColor = true;
            checkBoxDetailsDebyeWallerError.CheckedChanged += checkBoxDebyeWallerError_CheckedChanged;
            // 
            // flowLayoutPanelAniso2
            // 
            resources.ApplyResources(flowLayoutPanelAniso2, "flowLayoutPanelAniso2");
            flowLayoutPanelAniso2.Controls.Add(numericBoxB22);
            flowLayoutPanelAniso2.Controls.Add(numericBoxB22err);
            flowLayoutPanelAniso2.Controls.Add(numericBoxB23);
            flowLayoutPanelAniso2.Controls.Add(numericBoxB23err);
            flowLayoutPanelAniso2.Controls.Add(numericBoxB33);
            flowLayoutPanelAniso2.Controls.Add(numericBoxB33err);
            flowLayoutPanelAniso2.Name = "flowLayoutPanelAniso2";
            // 
            // numericBoxB22
            // 
            numericBoxB22.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB22.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB22, "numericBoxB22");
            toolTip.SetToolTip(numericBoxB22, resources.GetString("numericBoxB22.ToolTip")); // 260531Cl
            numericBoxB22.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB22.Name = "numericBoxB22";
            numericBoxB22.RoundErrorAccuracy = 8;
            numericBoxB22.SkipEventDuringInput = false;
            numericBoxB22.SmartIncrement = true;
            numericBoxB22.ValueFontSize = 9F;
            numericBoxB22.ThousandsSeparator = true;
            // 
            // numericBoxB22err
            // 
            numericBoxB22err.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB22err.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB22err, "numericBoxB22err");
            toolTip.SetToolTip(numericBoxB22err, resources.GetString("numericBoxB22err.ToolTip")); // 260531Cl
            numericBoxB22err.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB22err.Name = "numericBoxB22err";
            numericBoxB22err.RoundErrorAccuracy = 8;
            numericBoxB22err.SkipEventDuringInput = false;
            numericBoxB22err.SmartIncrement = true;
            numericBoxB22err.ValueFontSize = 9F;
            numericBoxB22err.ThousandsSeparator = true;
            // 
            // numericBoxB23
            // 
            numericBoxB23.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB23.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB23, "numericBoxB23");
            toolTip.SetToolTip(numericBoxB23, resources.GetString("numericBoxB23.ToolTip")); // 260531Cl
            numericBoxB23.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB23.Name = "numericBoxB23";
            numericBoxB23.RoundErrorAccuracy = 8;
            numericBoxB23.SkipEventDuringInput = false;
            numericBoxB23.SmartIncrement = true;
            numericBoxB23.ValueFontSize = 9F;
            numericBoxB23.ThousandsSeparator = true;
            // 
            // numericBoxB23err
            // 
            numericBoxB23err.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB23err.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB23err, "numericBoxB23err");
            toolTip.SetToolTip(numericBoxB23err, resources.GetString("numericBoxB23err.ToolTip")); // 260531Cl
            numericBoxB23err.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB23err.Name = "numericBoxB23err";
            numericBoxB23err.RoundErrorAccuracy = 8;
            numericBoxB23err.SkipEventDuringInput = false;
            numericBoxB23err.SmartIncrement = true;
            numericBoxB23err.ValueFontSize = 9F;
            numericBoxB23err.ThousandsSeparator = true;
            // 
            // numericBoxB33
            // 
            numericBoxB33.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB33.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB33, "numericBoxB33");
            toolTip.SetToolTip(numericBoxB33, resources.GetString("numericBoxB33.ToolTip")); // 260531Cl
            numericBoxB33.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB33.Name = "numericBoxB33";
            numericBoxB33.RoundErrorAccuracy = 8;
            numericBoxB33.SkipEventDuringInput = false;
            numericBoxB33.SmartIncrement = true;
            numericBoxB33.ValueFontSize = 9F;
            numericBoxB33.ThousandsSeparator = true;
            // 
            // numericBoxB33err
            // 
            numericBoxB33err.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB33err.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB33err, "numericBoxB33err");
            toolTip.SetToolTip(numericBoxB33err, resources.GetString("numericBoxB33err.ToolTip")); // 260531Cl
            numericBoxB33err.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB33err.Name = "numericBoxB33err";
            numericBoxB33err.RoundErrorAccuracy = 8;
            numericBoxB33err.SkipEventDuringInput = false;
            numericBoxB33err.SmartIncrement = true;
            numericBoxB33err.ValueFontSize = 9F;
            numericBoxB33err.ThousandsSeparator = true;
            // 
            // flowLayoutPanelAniso1
            // 
            resources.ApplyResources(flowLayoutPanelAniso1, "flowLayoutPanelAniso1");
            flowLayoutPanelAniso1.Controls.Add(numericBoxB11);
            flowLayoutPanelAniso1.Controls.Add(numericBoxB11err);
            flowLayoutPanelAniso1.Controls.Add(numericBoxB12);
            flowLayoutPanelAniso1.Controls.Add(numericBoxB12err);
            flowLayoutPanelAniso1.Controls.Add(numericBoxB13);
            flowLayoutPanelAniso1.Controls.Add(numericBoxB13err);
            flowLayoutPanelAniso1.Name = "flowLayoutPanelAniso1";
            // 
            // numericBoxB11
            // 
            numericBoxB11.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB11.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB11, "numericBoxB11");
            toolTip.SetToolTip(numericBoxB11, resources.GetString("numericBoxB11.ToolTip")); // 260531Cl
            numericBoxB11.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB11.Name = "numericBoxB11";
            numericBoxB11.RoundErrorAccuracy = 8;
            numericBoxB11.SkipEventDuringInput = false;
            numericBoxB11.SmartIncrement = true;
            numericBoxB11.ValueFontSize = 9F;
            numericBoxB11.ThousandsSeparator = true;
            // 
            // numericBoxB11err
            // 
            numericBoxB11err.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB11err.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB11err, "numericBoxB11err");
            toolTip.SetToolTip(numericBoxB11err, resources.GetString("numericBoxB11err.ToolTip")); // 260531Cl
            numericBoxB11err.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB11err.Name = "numericBoxB11err";
            numericBoxB11err.RoundErrorAccuracy = 8;
            numericBoxB11err.SkipEventDuringInput = false;
            numericBoxB11err.SmartIncrement = true;
            numericBoxB11err.ValueFontSize = 9F;
            numericBoxB11err.ThousandsSeparator = true;
            // 
            // numericBoxB12
            // 
            numericBoxB12.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB12.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB12, "numericBoxB12");
            toolTip.SetToolTip(numericBoxB12, resources.GetString("numericBoxB12.ToolTip")); // 260531Cl
            numericBoxB12.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB12.Name = "numericBoxB12";
            numericBoxB12.RoundErrorAccuracy = 8;
            numericBoxB12.SkipEventDuringInput = false;
            numericBoxB12.SmartIncrement = true;
            numericBoxB12.ValueFontSize = 9F;
            numericBoxB12.ThousandsSeparator = true;
            // 
            // numericBoxB12err
            // 
            numericBoxB12err.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB12err.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB12err, "numericBoxB12err");
            toolTip.SetToolTip(numericBoxB12err, resources.GetString("numericBoxB12err.ToolTip")); // 260531Cl
            numericBoxB12err.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB12err.Name = "numericBoxB12err";
            numericBoxB12err.RoundErrorAccuracy = 8;
            numericBoxB12err.SkipEventDuringInput = false;
            numericBoxB12err.SmartIncrement = true;
            numericBoxB12err.ValueFontSize = 9F;
            numericBoxB12err.ThousandsSeparator = true;
            // 
            // numericBoxB13
            // 
            numericBoxB13.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB13.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB13, "numericBoxB13");
            toolTip.SetToolTip(numericBoxB13, resources.GetString("numericBoxB13.ToolTip")); // 260531Cl
            numericBoxB13.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB13.Name = "numericBoxB13";
            numericBoxB13.RoundErrorAccuracy = 8;
            numericBoxB13.SkipEventDuringInput = false;
            numericBoxB13.SmartIncrement = true;
            numericBoxB13.ValueFontSize = 9F;
            numericBoxB13.ThousandsSeparator = true;
            // 
            // numericBoxB13err
            // 
            numericBoxB13err.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB13err.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxB13err, "numericBoxB13err");
            toolTip.SetToolTip(numericBoxB13err, resources.GetString("numericBoxB13err.ToolTip")); // 260531Cl
            numericBoxB13err.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB13err.Name = "numericBoxB13err";
            numericBoxB13err.RoundErrorAccuracy = 8;
            numericBoxB13err.SkipEventDuringInput = false;
            numericBoxB13err.SmartIncrement = true;
            numericBoxB13err.ValueFontSize = 9F;
            numericBoxB13err.ThousandsSeparator = true;
            // 
            // tabPageScatteringFactor
            // 
            tabPageScatteringFactor.BackColor = System.Drawing.Color.Transparent;
            tabPageScatteringFactor.Controls.Add(richTextBoxIsotope);
            tabPageScatteringFactor.Controls.Add(label34);
            tabPageScatteringFactor.Controls.Add(buttonEditIsotopeAbundance);
            tabPageScatteringFactor.Controls.Add(label5);
            tabPageScatteringFactor.Controls.Add(comboBoxScatteringFactorElectron);
            tabPageScatteringFactor.Controls.Add(comboBoxNeutron);
            tabPageScatteringFactor.Controls.Add(comboBoxScatteringFactorXray);
            tabPageScatteringFactor.Controls.Add(label6);
            tabPageScatteringFactor.Controls.Add(label4);
            resources.ApplyResources(tabPageScatteringFactor, "tabPageScatteringFactor");
            tabPageScatteringFactor.Name = "tabPageScatteringFactor";
            // 
            // richTextBoxIsotope
            // 
            resources.ApplyResources(richTextBoxIsotope, "richTextBoxIsotope");
            richTextBoxIsotope.BackColor = System.Drawing.SystemColors.Control;
            richTextBoxIsotope.BorderStyle = System.Windows.Forms.BorderStyle.None;
            richTextBoxIsotope.Name = "richTextBoxIsotope";
            richTextBoxIsotope.ReadOnly = true;
            // 
            // label34
            // 
            resources.ApplyResources(label34, "label34");
            label34.Name = "label34";
            // 
            // buttonEditIsotopeAbundance
            // 
            buttonEditIsotopeAbundance.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            resources.ApplyResources(buttonEditIsotopeAbundance, "buttonEditIsotopeAbundance");
            toolTip.SetToolTip(buttonEditIsotopeAbundance, resources.GetString("buttonEditIsotopeAbundance.ToolTip")); // 260531Cl
            buttonEditIsotopeAbundance.Name = "buttonEditIsotopeAbundance";
            buttonEditIsotopeAbundance.UseVisualStyleBackColor = true;
            buttonEditIsotopeAbundance.Click += buttonEditIsotopeAbundance_Click;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip")); // 260531Cl
            label5.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            label5.Name = "label5";
            // 
            // comboBoxScatteringFactorElectron
            // 
            comboBoxScatteringFactorElectron.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            comboBoxScatteringFactorElectron.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScatteringFactorElectron.DropDownWidth = 120;
            resources.ApplyResources(comboBoxScatteringFactorElectron, "comboBoxScatteringFactorElectron");
            comboBoxScatteringFactorElectron.Name = "comboBoxScatteringFactorElectron";
            toolTip.SetToolTip(comboBoxScatteringFactorElectron, resources.GetString("comboBoxScatteringFactorElectron.ToolTip"));
            comboBoxScatteringFactorElectron.SelectedIndexChanged += comboBoxAtomSub_SelectedIndexChanged;
            // 
            // comboBoxNeutron
            // 
            comboBoxNeutron.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            comboBoxNeutron.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxNeutron.DropDownWidth = 120;
            resources.ApplyResources(comboBoxNeutron, "comboBoxNeutron");
            toolTip.SetToolTip(comboBoxNeutron, resources.GetString("comboBoxNeutron.ToolTip")); // 260531Cl
            comboBoxNeutron.Items.AddRange(new object[] { resources.GetString("comboBoxNeutron.Items"), resources.GetString("comboBoxNeutron.Items1") });
            comboBoxNeutron.Name = "comboBoxNeutron";
            comboBoxNeutron.SelectedIndexChanged += comboBoxNeutron_SelectedIndexChanged;
            // 
            // comboBoxScatteringFactorXray
            // 
            comboBoxScatteringFactorXray.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            comboBoxScatteringFactorXray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScatteringFactorXray.DropDownWidth = 120;
            resources.ApplyResources(comboBoxScatteringFactorXray, "comboBoxScatteringFactorXray");
            comboBoxScatteringFactorXray.Name = "comboBoxScatteringFactorXray";
            toolTip.SetToolTip(comboBoxScatteringFactorXray, resources.GetString("comboBoxScatteringFactorXray.ToolTip"));
            comboBoxScatteringFactorXray.SelectedIndexChanged += comboBoxAtomSub_SelectedIndexChanged;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip")); // 260531Cl
            label6.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip")); // 260531Cl
            label4.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            label4.Name = "label4";
            // 
            // tabPageAppearance
            // 
            tabPageAppearance.Controls.Add(numericBoxAlpha);
            tabPageAppearance.Controls.Add(numericBoxEmission);
            tabPageAppearance.Controls.Add(numericBoxShininess);
            tabPageAppearance.Controls.Add(numericBoxSpecular);
            tabPageAppearance.Controls.Add(numericBoxDiffusion);
            tabPageAppearance.Controls.Add(numericBoxAmbient);
            tabPageAppearance.Controls.Add(checkBoxShowLabel);
            tabPageAppearance.Controls.Add(label10);
            tabPageAppearance.Controls.Add(label37);
            tabPageAppearance.Controls.Add(label38);
            tabPageAppearance.Controls.Add(label11);
            tabPageAppearance.Controls.Add(label35);
            tabPageAppearance.Controls.Add(label36);
            tabPageAppearance.Controls.Add(numericBoxAtomRadius);
            tabPageAppearance.Controls.Add(colorControlAtomColor);
            resources.ApplyResources(tabPageAppearance, "tabPageAppearance");
            tabPageAppearance.Name = "tabPageAppearance";
            captureExtender.SetCapture(tabPageAppearance, true); //260529Cl: マニュアル用にタブ単位クロップを撮る
            // 
            // numericBoxAlpha
            // 
            resources.ApplyResources(numericBoxAlpha, "numericBoxAlpha");
            toolTip.SetToolTip(numericBoxAlpha, resources.GetString("numericBoxAlpha.ToolTip")); // 260531Cl
            numericBoxAlpha.BackColor = System.Drawing.SystemColors.Control;
            numericBoxAlpha.DecimalPlaces = 1;
            numericBoxAlpha.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxAlpha.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxAlpha.Maximum = 1D;
            numericBoxAlpha.Minimum = 0D;
            numericBoxAlpha.Name = "numericBoxAlpha";
            numericBoxAlpha.ShowUpDown = true;
            numericBoxAlpha.SkipEventDuringInput = false;
            numericBoxAlpha.SmartIncrement = true;
            numericBoxAlpha.ValueForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            numericBoxAlpha.ValueFontSize = 9F;
            numericBoxAlpha.ThousandsSeparator = true;
            numericBoxAlpha.UpDown_Increment = 0.1D;
            // 
            // numericBoxEmission
            // 
            resources.ApplyResources(numericBoxEmission, "numericBoxEmission");
            toolTip.SetToolTip(numericBoxEmission, resources.GetString("numericBoxEmission.ToolTip")); // 260531Cl
            numericBoxEmission.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEmission.DecimalPlaces = 1;
            numericBoxEmission.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxEmission.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxEmission.Maximum = 1D;
            numericBoxEmission.Minimum = 0D;
            numericBoxEmission.Name = "numericBoxEmission";
            numericBoxEmission.ShowUpDown = true;
            numericBoxEmission.SkipEventDuringInput = false;
            numericBoxEmission.SmartIncrement = true;
            numericBoxEmission.ValueForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            numericBoxEmission.ValueFontSize = 9F;
            numericBoxEmission.ThousandsSeparator = true;
            numericBoxEmission.UpDown_Increment = 0.1D;
            // 
            // numericBoxShininess
            // 
            resources.ApplyResources(numericBoxShininess, "numericBoxShininess");
            toolTip.SetToolTip(numericBoxShininess, resources.GetString("numericBoxShininess.ToolTip")); // 260531Cl
            numericBoxShininess.BackColor = System.Drawing.SystemColors.Control;
            numericBoxShininess.DecimalPlaces = 1;
            numericBoxShininess.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxShininess.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxShininess.Maximum = 20D;
            numericBoxShininess.Minimum = 0D;
            numericBoxShininess.Name = "numericBoxShininess";
            numericBoxShininess.ShowUpDown = true;
            numericBoxShininess.SkipEventDuringInput = false;
            numericBoxShininess.SmartIncrement = true;
            numericBoxShininess.ValueForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            numericBoxShininess.ValueFontSize = 9F;
            numericBoxShininess.ThousandsSeparator = true;
            numericBoxShininess.UpDown_Increment = 0.1D;
            // 
            // numericBoxSpecular
            // 
            resources.ApplyResources(numericBoxSpecular, "numericBoxSpecular");
            toolTip.SetToolTip(numericBoxSpecular, resources.GetString("numericBoxSpecular.ToolTip")); // 260531Cl
            numericBoxSpecular.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSpecular.DecimalPlaces = 1;
            numericBoxSpecular.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxSpecular.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxSpecular.Maximum = 1D;
            numericBoxSpecular.Minimum = 0D;
            numericBoxSpecular.Name = "numericBoxSpecular";
            numericBoxSpecular.ShowUpDown = true;
            numericBoxSpecular.SkipEventDuringInput = false;
            numericBoxSpecular.SmartIncrement = true;
            numericBoxSpecular.ValueForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            numericBoxSpecular.ValueFontSize = 9F;
            numericBoxSpecular.ThousandsSeparator = true;
            numericBoxSpecular.UpDown_Increment = 0.1D;
            // 
            // numericBoxDiffusion
            // 
            resources.ApplyResources(numericBoxDiffusion, "numericBoxDiffusion");
            toolTip.SetToolTip(numericBoxDiffusion, resources.GetString("numericBoxDiffusion.ToolTip")); // 260531Cl
            numericBoxDiffusion.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDiffusion.DecimalPlaces = 1;
            numericBoxDiffusion.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDiffusion.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDiffusion.Maximum = 1D;
            numericBoxDiffusion.Minimum = 0D;
            numericBoxDiffusion.Name = "numericBoxDiffusion";
            numericBoxDiffusion.ShowUpDown = true;
            numericBoxDiffusion.SkipEventDuringInput = false;
            numericBoxDiffusion.SmartIncrement = true;
            numericBoxDiffusion.ValueForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            numericBoxDiffusion.ValueFontSize = 9F;
            numericBoxDiffusion.ThousandsSeparator = true;
            numericBoxDiffusion.UpDown_Increment = 0.1D;
            // 
            // numericBoxAmbient
            // 
            resources.ApplyResources(numericBoxAmbient, "numericBoxAmbient");
            toolTip.SetToolTip(numericBoxAmbient, resources.GetString("numericBoxAmbient.ToolTip")); // 260531Cl
            numericBoxAmbient.BackColor = System.Drawing.SystemColors.Control;
            numericBoxAmbient.DecimalPlaces = 1;
            numericBoxAmbient.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxAmbient.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxAmbient.Maximum = 1D;
            numericBoxAmbient.Minimum = 0D;
            numericBoxAmbient.Name = "numericBoxAmbient";
            numericBoxAmbient.ShowUpDown = true;
            numericBoxAmbient.SkipEventDuringInput = false;
            numericBoxAmbient.SmartIncrement = true;
            numericBoxAmbient.ValueForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            numericBoxAmbient.ValueFontSize = 9F;
            numericBoxAmbient.ThousandsSeparator = true;
            numericBoxAmbient.UpDown_Increment = 0.1D;
            // 
            // checkBoxShowLabel
            // 
            resources.ApplyResources(checkBoxShowLabel, "checkBoxShowLabel");
            toolTip.SetToolTip(checkBoxShowLabel, resources.GetString("checkBoxShowLabel.ToolTip")); // 260531Cl
            checkBoxShowLabel.Name = "checkBoxShowLabel";
            checkBoxShowLabel.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            toolTip.SetToolTip(label10, resources.GetString("label10.ToolTip")); // 260531Cl
            label10.Name = "label10";
            // 
            // label37
            // 
            resources.ApplyResources(label37, "label37");
            toolTip.SetToolTip(label37, resources.GetString("label37.ToolTip")); // 260531Cl
            label37.Name = "label37";
            // 
            // label38
            // 
            resources.ApplyResources(label38, "label38");
            toolTip.SetToolTip(label38, resources.GetString("label38.ToolTip")); // 260531Cl
            label38.Name = "label38";
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip")); // 260531Cl
            label11.Name = "label11";
            // 
            // label35
            // 
            resources.ApplyResources(label35, "label35");
            toolTip.SetToolTip(label35, resources.GetString("label35.ToolTip")); // 260531Cl
            label35.Name = "label35";
            // 
            // label36
            // 
            resources.ApplyResources(label36, "label36");
            toolTip.SetToolTip(label36, resources.GetString("label36.ToolTip")); // 260531Cl
            label36.Name = "label36";
            // 
            // numericBoxAtomRadius
            // 
            resources.ApplyResources(numericBoxAtomRadius, "numericBoxAtomRadius");
            numericBoxAtomRadius.BackColor = System.Drawing.SystemColors.Control;
            numericBoxAtomRadius.DecimalPlaces = 3;
            numericBoxAtomRadius.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxAtomRadius.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxAtomRadius.Name = "numericBoxAtomRadius";
            numericBoxAtomRadius.ShowUpDown = true;
            numericBoxAtomRadius.SkipEventDuringInput = false;
            numericBoxAtomRadius.SmartIncrement = true;
            numericBoxAtomRadius.ValueForeColor = System.Drawing.SystemColors.ControlText;
            numericBoxAtomRadius.ValueFontSize = 9F;
            numericBoxAtomRadius.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxAtomRadius, resources.GetString("numericBoxAtomRadius.ToolTip1"));
            // 
            // colorControlAtomColor
            // 
            colorControlAtomColor.Argb = -986896;
            resources.ApplyResources(colorControlAtomColor, "colorControlAtomColor");
            toolTip.SetToolTip(colorControlAtomColor, resources.GetString("colorControlAtomColor.ToolTip")); // 260531Cl
            colorControlAtomColor.BackColor = System.Drawing.SystemColors.Control;
            colorControlAtomColor.Blue = 240;
            colorControlAtomColor.BlueF = 0.9411765F;
            colorControlAtomColor.BoxSize = new System.Drawing.Size(20, 20);
            colorControlAtomColor.Color = System.Drawing.Color.FromArgb(240, 240, 240);
            colorControlAtomColor.Green = 240;
            colorControlAtomColor.GreenF = 0.9411765F;
            colorControlAtomColor.Name = "colorControlAtomColor";
            colorControlAtomColor.Red = 240;
            colorControlAtomColor.RedF = 0.9411765F;
            // 
            // buttonApplyToSameElement
            // 
            resources.ApplyResources(buttonApplyToSameElement, "buttonApplyToSameElement");
            buttonApplyToSameElement.BackColor = System.Drawing.Color.SteelBlue;
            buttonApplyToSameElement.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonApplyToSameElement.Name = "buttonApplyToSameElement";
            toolTip.SetToolTip(buttonApplyToSameElement, resources.GetString("buttonApplyToSameElement.ToolTip"));
            buttonApplyToSameElement.UseVisualStyleBackColor = false;
            buttonApplyToSameElement.Click += buttonChangeToSameElement_Click;
            // 
            // buttonAddAtom
            // 
            resources.ApplyResources(buttonAddAtom, "buttonAddAtom");
            buttonAddAtom.BackColor = System.Drawing.Color.SteelBlue;
            buttonAddAtom.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonAddAtom.Name = "buttonAddAtom";
            toolTip.SetToolTip(buttonAddAtom, resources.GetString("buttonAddAtom.ToolTip"));
            buttonAddAtom.UseVisualStyleBackColor = false;
            buttonAddAtom.Click += buttonAdd_Click;
            // 
            // buttonChange
            // 
            resources.ApplyResources(buttonChange, "buttonChange");
            buttonChange.BackColor = System.Drawing.Color.SteelBlue;
            buttonChange.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonChange.Name = "buttonChange";
            toolTip.SetToolTip(buttonChange, resources.GetString("buttonChange.ToolTip"));
            buttonChange.UseVisualStyleBackColor = false;
            buttonChange.Click += buttonChange_Click;
            // 
            // buttonApplyToAllElements
            // 
            resources.ApplyResources(buttonApplyToAllElements, "buttonApplyToAllElements");
            buttonApplyToAllElements.BackColor = System.Drawing.Color.SteelBlue;
            buttonApplyToAllElements.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonApplyToAllElements.Name = "buttonApplyToAllElements";
            toolTip.SetToolTip(buttonApplyToAllElements, resources.GetString("buttonApplyToAllElements.ToolTip"));
            buttonApplyToAllElements.UseVisualStyleBackColor = false;
            buttonApplyToAllElements.Click += buttonApplyToAllElements_Click;
            // 
            // buttonAtomUp
            // 
            resources.ApplyResources(buttonAtomUp, "buttonAtomUp");
            buttonAtomUp.BackColor = System.Drawing.SystemColors.Control;
            buttonAtomUp.ForeColor = System.Drawing.SystemColors.ControlText;
            buttonAtomUp.Name = "buttonAtomUp";
            toolTip.SetToolTip(buttonAtomUp, resources.GetString("buttonAtomUp.ToolTip"));
            buttonAtomUp.UseVisualStyleBackColor = true;
            buttonAtomUp.Click += buttonUp_Click;
            // 
            // buttonAtomDown
            // 
            resources.ApplyResources(buttonAtomDown, "buttonAtomDown");
            buttonAtomDown.BackColor = System.Drawing.SystemColors.Control;
            buttonAtomDown.ForeColor = System.Drawing.SystemColors.ControlText;
            buttonAtomDown.Name = "buttonAtomDown";
            toolTip.SetToolTip(buttonAtomDown, resources.GetString("buttonAtomDown.ToolTip"));
            buttonAtomDown.UseVisualStyleBackColor = true;
            buttonAtomDown.Click += buttonDown_Click;
            // 
            // buttonDeleteAtom
            // 
            resources.ApplyResources(buttonDeleteAtom, "buttonDeleteAtom");
            buttonDeleteAtom.BackColor = System.Drawing.Color.IndianRed;
            buttonDeleteAtom.ForeColor = System.Drawing.Color.White;
            buttonDeleteAtom.Name = "buttonDeleteAtom";
            toolTip.SetToolTip(buttonDeleteAtom, resources.GetString("buttonDeleteAtom.ToolTip"));
            buttonDeleteAtom.UseVisualStyleBackColor = false;
            buttonDeleteAtom.Click += buttonDelete_Click;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { enabledColumn, labelDataGridViewTextBoxColumn, elementDataGridViewTextBoxColumn, xDataGridViewTextBoxColumn, yDataGridViewTextBoxColumn, zDataGridViewTextBoxColumn, occDataGridViewTextBoxColumn, multiDataGridViewTextBoxColumn, wyckLetDataGridViewTextBoxColumn, siteSymDataGridViewTextBoxColumn });
            dataGridView.DataSource = bindingSource;
            resources.ApplyResources(dataGridView, "dataGridView");
            toolTip.SetToolTip(dataGridView, resources.GetString("dataGridView.ToolTip")); // 260531Cl
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView.CellValueChanged += dataGridViewAtom_CellValueChanged;
            dataGridView.CurrentCellDirtyStateChanged += dataGridView_CurrentCellDirtyStateChanged;
            // 
            // enabledColumn
            // 
            enabledColumn.DataPropertyName = "Enabled";
            resources.ApplyResources(enabledColumn, "enabledColumn");
            enabledColumn.Name = "enabledColumn";
            enabledColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // labelDataGridViewTextBoxColumn
            // 
            labelDataGridViewTextBoxColumn.DataPropertyName = "Label";
            resources.ApplyResources(labelDataGridViewTextBoxColumn, "labelDataGridViewTextBoxColumn");
            labelDataGridViewTextBoxColumn.Name = "labelDataGridViewTextBoxColumn";
            labelDataGridViewTextBoxColumn.ReadOnly = true;
            labelDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // elementDataGridViewTextBoxColumn
            // 
            elementDataGridViewTextBoxColumn.DataPropertyName = "Element";
            resources.ApplyResources(elementDataGridViewTextBoxColumn, "elementDataGridViewTextBoxColumn");
            elementDataGridViewTextBoxColumn.Name = "elementDataGridViewTextBoxColumn";
            elementDataGridViewTextBoxColumn.ReadOnly = true;
            elementDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            elementDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // xDataGridViewTextBoxColumn
            // 
            xDataGridViewTextBoxColumn.DataPropertyName = "X";
            resources.ApplyResources(xDataGridViewTextBoxColumn, "xDataGridViewTextBoxColumn");
            xDataGridViewTextBoxColumn.Name = "xDataGridViewTextBoxColumn";
            xDataGridViewTextBoxColumn.ReadOnly = true;
            xDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // yDataGridViewTextBoxColumn
            // 
            yDataGridViewTextBoxColumn.DataPropertyName = "Y";
            resources.ApplyResources(yDataGridViewTextBoxColumn, "yDataGridViewTextBoxColumn");
            yDataGridViewTextBoxColumn.Name = "yDataGridViewTextBoxColumn";
            yDataGridViewTextBoxColumn.ReadOnly = true;
            yDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // zDataGridViewTextBoxColumn
            // 
            zDataGridViewTextBoxColumn.DataPropertyName = "Z";
            resources.ApplyResources(zDataGridViewTextBoxColumn, "zDataGridViewTextBoxColumn");
            zDataGridViewTextBoxColumn.Name = "zDataGridViewTextBoxColumn";
            zDataGridViewTextBoxColumn.ReadOnly = true;
            zDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // occDataGridViewTextBoxColumn
            // 
            occDataGridViewTextBoxColumn.DataPropertyName = "Occ.";
            resources.ApplyResources(occDataGridViewTextBoxColumn, "occDataGridViewTextBoxColumn");
            occDataGridViewTextBoxColumn.Name = "occDataGridViewTextBoxColumn";
            occDataGridViewTextBoxColumn.ReadOnly = true;
            occDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // multiDataGridViewTextBoxColumn
            // 
            multiDataGridViewTextBoxColumn.DataPropertyName = "Multi.";
            resources.ApplyResources(multiDataGridViewTextBoxColumn, "multiDataGridViewTextBoxColumn");
            multiDataGridViewTextBoxColumn.Name = "multiDataGridViewTextBoxColumn";
            multiDataGridViewTextBoxColumn.ReadOnly = true;
            multiDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // wyckLetDataGridViewTextBoxColumn
            // 
            wyckLetDataGridViewTextBoxColumn.DataPropertyName = "Wyck. Let.";
            resources.ApplyResources(wyckLetDataGridViewTextBoxColumn, "wyckLetDataGridViewTextBoxColumn");
            wyckLetDataGridViewTextBoxColumn.Name = "wyckLetDataGridViewTextBoxColumn";
            wyckLetDataGridViewTextBoxColumn.ReadOnly = true;
            wyckLetDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // siteSymDataGridViewTextBoxColumn
            // 
            siteSymDataGridViewTextBoxColumn.DataPropertyName = "Site Sym.";
            resources.ApplyResources(siteSymDataGridViewTextBoxColumn, "siteSymDataGridViewTextBoxColumn");
            siteSymDataGridViewTextBoxColumn.Name = "siteSymDataGridViewTextBoxColumn";
            siteSymDataGridViewTextBoxColumn.ReadOnly = true;
            siteSymDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bindingSource
            // 
            bindingSource.DataMember = "DataTableAtom";
            bindingSource.DataSource = dataSet;
            bindingSource.CurrentChanged += bindingSource_PositionChanged;
            bindingSource.PositionChanged += bindingSource_PositionChanged;
            // 
            // dataSet
            // 
            dataSet.DataSetName = "DataSet";
            dataSet.Namespace = "http://tempuri.org/DataSet1.xsd";
            dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonAtomUp);
            panel1.Controls.Add(buttonAtomDown);
            panel1.Controls.Add(buttonDeleteAtom);
            panel1.Controls.Add(buttonApplyToAllElements);
            panel1.Controls.Add(buttonApplyToSameElement);
            panel1.Controls.Add(buttonChange);
            panel1.Controls.Add(buttonAddAtom);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // AtomControl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(dataGridView);
            Controls.Add(panel1);
            Controls.Add(tabControl);
            Name = "AtomControl";
            flowLayoutPanelIso.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabPageElementAndPosition.ResumeLayout(false);
            tabPageElementAndPosition.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabPageOriginShift.ResumeLayout(false);
            tabPageOriginShift.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            tabPageDebyeWaller.ResumeLayout(false);
            tabPageDebyeWaller.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanelAniso2.ResumeLayout(false);
            flowLayoutPanelAniso1.ResumeLayout(false);
            tabPageScatteringFactor.ResumeLayout(false);
            tabPageScatteringFactor.PerformLayout();
            tabPageAppearance.ResumeLayout(false);
            tabPageAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private NumericBox numericBoxB11err;
        private NumericBox numericBoxB12err;
        private NumericBox numericBoxB13err;
        private NumericBox numericBoxB22err;
        private NumericBox numericBoxB23err;
        private NumericBox numericBoxB33err;
        private NumericBox numericBoxBisoerr;
        private System.Windows.Forms.RadioButton radioButtonIsotoropy;
        private System.Windows.Forms.RadioButton radioButtonAnisotropy;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelIso;
        private NumericBox numericBoxX;
        private System.Windows.Forms.Label labelX_;
        private NumericBox numericBoxXerr;
        private NumericBox numericBoxY;
        private NumericBox numericBoxYerr;
        private NumericBox numericBoxZ;
        private NumericBox numericBoxZerr;
        private NumericBox numericBoxOcc;
        private NumericBox numericBoxOccerr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAtom;
        private NumericBox numericBoxB11;
        private NumericBox numericBoxB12;
        private NumericBox numericBoxB13;
        private NumericBox numericBoxB22;
        private NumericBox numericBoxB23;
        private NumericBox numericBoxB33;
        private NumericBox numericBoxBiso;
        private System.Windows.Forms.CheckBox checkBoxDetailAtomicPositionError;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageElementAndPosition;
        private System.Windows.Forms.TabPage tabPageDebyeWaller;
        private System.Windows.Forms.TabPage tabPageScatteringFactor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxScatteringFactorElectron;
        private System.Windows.Forms.ComboBox comboBoxScatteringFactorXray;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAniso2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAniso1;
        private System.Windows.Forms.ComboBox comboBoxNeutron;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button buttonEditIsotopeAbundance;
        private System.Windows.Forms.RichTextBox richTextBoxIsotope;
        private System.Windows.Forms.CheckBox checkBoxDetailsDebyeWallerError;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPageAppearance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label37;
        private ColorControl colorControlAtomColor;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private NumericBox numericBoxAlpha;
        private NumericBox numericBoxEmission;
        private NumericBox numericBoxShininess;
        private NumericBox numericBoxSpecular;
        private NumericBox numericBoxDiffusion;
        private NumericBox numericBoxAmbient;
        private NumericBox numericBoxAtomRadius;
        private System.Windows.Forms.Button buttonApplyToSameElement;
        private System.Windows.Forms.Button buttonAddAtom;
        private System.Windows.Forms.Button buttonAtomUp;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.Button buttonAtomDown;
        private System.Windows.Forms.Button buttonDeleteAtom;
        private System.Windows.Forms.BindingSource bindingSource;
        private DataSet dataSet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPageOriginShift;
        private System.Windows.Forms.Button buttonOriginShift1;
        private System.Windows.Forms.Button buttonOriginShift2;
        private System.Windows.Forms.Label label7;
        private NumericBox numericBoxOriginShiftZ;
        private NumericBox numericBoxOriginShiftY;
        private NumericBox numericBoxOriginShiftX;
        private System.Windows.Forms.Button buttonOriginShiftCustom;
        private System.Windows.Forms.RadioButton radioButtonOriginShiftPlus;
        private System.Windows.Forms.RadioButton radioButtonOriginShiftMinus;
        private System.Windows.Forms.Button buttonOriginShift6;
        private System.Windows.Forms.Button buttonOriginShift7;
        private System.Windows.Forms.Button buttonOriginShift4;
        private System.Windows.Forms.Button buttonOriginShift3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button buttonOriginShift8;
        private System.Windows.Forms.Button buttonOriginShift5;
        private System.Windows.Forms.Button buttonOriginShift9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn elementDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn occDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn multiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wyckLetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteSymDataGridViewTextBoxColumn;
        // public System.Windows.Forms.DataGridView dataGridView; // 260518Cl 旧実装
        public DpiAwareDataGridView dataGridView; // 260518Cl
        private System.Windows.Forms.CheckBox checkBoxShowLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonDebyeWallerTypeU;
        private System.Windows.Forms.RadioButton radioButtonDebyeWallerTypeB;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelDimension;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonApplyToAllElements;
    }
}

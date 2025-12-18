namespace Crystallography.Controls
{
    partial class SymmetryControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymmetryControl));
            groupBox4 = new System.Windows.Forms.GroupBox();
            panel2 = new System.Windows.Forms.Panel();
            radioButtonNanoMeter = new System.Windows.Forms.RadioButton();
            radioButtonAngstrom = new System.Windows.Forms.RadioButton();
            label1 = new System.Windows.Forms.Label();
            checkBoxShowError = new System.Windows.Forms.CheckBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            label48 = new System.Windows.Forms.Label();
            label46 = new System.Windows.Forms.Label();
            numericBoxBeta = new NumericBox();
            numericBoxAlpha = new NumericBox();
            label47 = new System.Windows.Forms.Label();
            numericBoxGammaErr = new NumericBox();
            label23 = new System.Windows.Forms.Label();
            numericBoxAlphaErr = new NumericBox();
            numericBoxBetaErr = new NumericBox();
            numericBoxA = new NumericBox();
            label26 = new System.Windows.Forms.Label();
            labelLengthUnitC = new System.Windows.Forms.Label();
            numericBoxGamma = new NumericBox();
            labelLengthUnitB = new System.Windows.Forms.Label();
            numericBoxBErr = new NumericBox();
            numericBoxB = new NumericBox();
            label24 = new System.Windows.Forms.Label();
            label25 = new System.Windows.Forms.Label();
            label28 = new System.Windows.Forms.Label();
            label27 = new System.Windows.Forms.Label();
            numericBoxC = new NumericBox();
            numericBoxCErr = new NumericBox();
            numericBoxAErr = new NumericBox();
            labelLengthUnitA = new System.Windows.Forms.Label();
            groupBoxSymmetry = new System.Windows.Forms.GroupBox();
            comboBoxSpaceGroup = new System.Windows.Forms.ComboBox();
            comboBoxPointGroup = new System.Windows.Forms.ComboBox();
            comboBoxCrystalSystem = new System.Windows.Forms.ComboBox();
            label20 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            textBoxSearch = new System.Windows.Forms.TextBox();
            label21 = new System.Windows.Forms.Label();
            comboBoxSearchResult = new System.Windows.Forms.ComboBox();
            panel1 = new System.Windows.Forms.Panel();
            toolTip = new System.Windows.Forms.ToolTip(components);
            groupBox4.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBoxSymmetry.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(panel2);
            groupBox4.Controls.Add(checkBoxShowError);
            groupBox4.Controls.Add(tableLayoutPanel1);
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Controls.Add(radioButtonNanoMeter);
            panel2.Controls.Add(radioButtonAngstrom);
            panel2.Controls.Add(label1);
            panel2.Name = "panel2";
            // 
            // radioButtonNanoMeter
            // 
            resources.ApplyResources(radioButtonNanoMeter, "radioButtonNanoMeter");
            radioButtonNanoMeter.Name = "radioButtonNanoMeter";
            radioButtonNanoMeter.UseVisualStyleBackColor = true;
            radioButtonNanoMeter.CheckedChanged += radioButtonNanoMeter_CheckedChanged;
            // 
            // radioButtonAngstrom
            // 
            resources.ApplyResources(radioButtonAngstrom, "radioButtonAngstrom");
            radioButtonAngstrom.Checked = true;
            radioButtonAngstrom.Name = "radioButtonAngstrom";
            radioButtonAngstrom.TabStop = true;
            radioButtonAngstrom.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // checkBoxShowError
            // 
            resources.ApplyResources(checkBoxShowError, "checkBoxShowError");
            checkBoxShowError.Name = "checkBoxShowError";
            toolTip.SetToolTip(checkBoxShowError, resources.GetString("checkBoxShowError.ToolTip"));
            checkBoxShowError.UseVisualStyleBackColor = true;
            checkBoxShowError.CheckedChanged += checkBoxShowError_CheckedChanged;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(label48, 7, 2);
            tableLayoutPanel1.Controls.Add(label46, 7, 1);
            tableLayoutPanel1.Controls.Add(numericBoxBeta, 5, 1);
            tableLayoutPanel1.Controls.Add(numericBoxAlpha, 5, 0);
            tableLayoutPanel1.Controls.Add(label47, 7, 0);
            tableLayoutPanel1.Controls.Add(numericBoxGammaErr, 6, 2);
            tableLayoutPanel1.Controls.Add(label23, 0, 0);
            tableLayoutPanel1.Controls.Add(numericBoxAlphaErr, 6, 0);
            tableLayoutPanel1.Controls.Add(numericBoxBetaErr, 6, 1);
            tableLayoutPanel1.Controls.Add(numericBoxA, 1, 0);
            tableLayoutPanel1.Controls.Add(label26, 4, 0);
            tableLayoutPanel1.Controls.Add(labelLengthUnitC, 3, 2);
            tableLayoutPanel1.Controls.Add(numericBoxGamma, 5, 2);
            tableLayoutPanel1.Controls.Add(labelLengthUnitB, 3, 1);
            tableLayoutPanel1.Controls.Add(numericBoxBErr, 2, 1);
            tableLayoutPanel1.Controls.Add(numericBoxB, 1, 1);
            tableLayoutPanel1.Controls.Add(label24, 0, 1);
            tableLayoutPanel1.Controls.Add(label25, 0, 2);
            tableLayoutPanel1.Controls.Add(label28, 4, 2);
            tableLayoutPanel1.Controls.Add(label27, 4, 1);
            tableLayoutPanel1.Controls.Add(numericBoxC, 1, 2);
            tableLayoutPanel1.Controls.Add(numericBoxCErr, 2, 2);
            tableLayoutPanel1.Controls.Add(numericBoxAErr, 2, 0);
            tableLayoutPanel1.Controls.Add(labelLengthUnitA, 3, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label48
            // 
            resources.ApplyResources(label48, "label48");
            label48.Name = "label48";
            // 
            // label46
            // 
            resources.ApplyResources(label46, "label46");
            label46.Name = "label46";
            // 
            // numericBoxBeta
            // 
            resources.ApplyResources(numericBoxBeta, "numericBoxBeta");
            numericBoxBeta.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBeta.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBeta.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBeta.Name = "numericBoxBeta";
            numericBoxBeta.RestrictLimitValue = false;
            numericBoxBeta.RoundErrorAccuracy = 12;
            numericBoxBeta.SkipEventDuringInput = false;
            numericBoxBeta.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxBeta, resources.GetString("numericBoxBeta.ToolTip1"));
            numericBoxBeta.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // numericBoxAlpha
            // 
            resources.ApplyResources(numericBoxAlpha, "numericBoxAlpha");
            numericBoxAlpha.BackColor = System.Drawing.SystemColors.Control;
            numericBoxAlpha.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxAlpha.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxAlpha.Name = "numericBoxAlpha";
            numericBoxAlpha.RestrictLimitValue = false;
            numericBoxAlpha.RoundErrorAccuracy = 12;
            numericBoxAlpha.SkipEventDuringInput = false;
            numericBoxAlpha.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxAlpha, resources.GetString("numericBoxAlpha.ToolTip1"));
            numericBoxAlpha.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // label47
            // 
            resources.ApplyResources(label47, "label47");
            label47.Name = "label47";
            // 
            // numericBoxGammaErr
            // 
            resources.ApplyResources(numericBoxGammaErr, "numericBoxGammaErr");
            numericBoxGammaErr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxGammaErr.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxGammaErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxGammaErr.Name = "numericBoxGammaErr";
            numericBoxGammaErr.RestrictLimitValue = false;
            numericBoxGammaErr.RoundErrorAccuracy = 12;
            numericBoxGammaErr.SkipEventDuringInput = false;
            numericBoxGammaErr.SmartIncrement = true;
            numericBoxGammaErr.TabStop = false;
            numericBoxGammaErr.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // label23
            // 
            resources.ApplyResources(label23, "label23");
            label23.Name = "label23";
            // 
            // numericBoxAlphaErr
            // 
            resources.ApplyResources(numericBoxAlphaErr, "numericBoxAlphaErr");
            numericBoxAlphaErr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxAlphaErr.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxAlphaErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxAlphaErr.Name = "numericBoxAlphaErr";
            numericBoxAlphaErr.RestrictLimitValue = false;
            numericBoxAlphaErr.RoundErrorAccuracy = 12;
            numericBoxAlphaErr.SkipEventDuringInput = false;
            numericBoxAlphaErr.SmartIncrement = true;
            numericBoxAlphaErr.TabStop = false;
            numericBoxAlphaErr.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // numericBoxBetaErr
            // 
            resources.ApplyResources(numericBoxBetaErr, "numericBoxBetaErr");
            numericBoxBetaErr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBetaErr.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBetaErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBetaErr.Name = "numericBoxBetaErr";
            numericBoxBetaErr.RestrictLimitValue = false;
            numericBoxBetaErr.RoundErrorAccuracy = 12;
            numericBoxBetaErr.SkipEventDuringInput = false;
            numericBoxBetaErr.SmartIncrement = true;
            numericBoxBetaErr.TabStop = false;
            numericBoxBetaErr.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // numericBoxA
            // 
            resources.ApplyResources(numericBoxA, "numericBoxA");
            numericBoxA.BackColor = System.Drawing.SystemColors.Control;
            numericBoxA.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxA.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxA.Name = "numericBoxA";
            numericBoxA.RestrictLimitValue = false;
            numericBoxA.RoundErrorAccuracy = 10;
            numericBoxA.SkipEventDuringInput = false;
            numericBoxA.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxA, resources.GetString("numericBoxA.ToolTip1"));
            numericBoxA.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // label26
            // 
            resources.ApplyResources(label26, "label26");
            label26.Name = "label26";
            // 
            // labelLengthUnitC
            // 
            resources.ApplyResources(labelLengthUnitC, "labelLengthUnitC");
            labelLengthUnitC.Name = "labelLengthUnitC";
            // 
            // numericBoxGamma
            // 
            resources.ApplyResources(numericBoxGamma, "numericBoxGamma");
            numericBoxGamma.BackColor = System.Drawing.SystemColors.Control;
            numericBoxGamma.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxGamma.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxGamma.Name = "numericBoxGamma";
            numericBoxGamma.RestrictLimitValue = false;
            numericBoxGamma.RoundErrorAccuracy = 12;
            numericBoxGamma.SkipEventDuringInput = false;
            numericBoxGamma.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxGamma, resources.GetString("numericBoxGamma.ToolTip1"));
            numericBoxGamma.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // labelLengthUnitB
            // 
            resources.ApplyResources(labelLengthUnitB, "labelLengthUnitB");
            labelLengthUnitB.Name = "labelLengthUnitB";
            // 
            // numericBoxBErr
            // 
            resources.ApplyResources(numericBoxBErr, "numericBoxBErr");
            numericBoxBErr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBErr.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBErr.Name = "numericBoxBErr";
            numericBoxBErr.RestrictLimitValue = false;
            numericBoxBErr.RoundErrorAccuracy = 12;
            numericBoxBErr.SkipEventDuringInput = false;
            numericBoxBErr.SmartIncrement = true;
            numericBoxBErr.TabStop = false;
            numericBoxBErr.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // numericBoxB
            // 
            resources.ApplyResources(numericBoxB, "numericBoxB");
            numericBoxB.BackColor = System.Drawing.SystemColors.Control;
            numericBoxB.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxB.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxB.Name = "numericBoxB";
            numericBoxB.RestrictLimitValue = false;
            numericBoxB.RoundErrorAccuracy = 12;
            numericBoxB.SkipEventDuringInput = false;
            numericBoxB.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxB, resources.GetString("numericBoxB.ToolTip1"));
            numericBoxB.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // label24
            // 
            resources.ApplyResources(label24, "label24");
            label24.Name = "label24";
            // 
            // label25
            // 
            resources.ApplyResources(label25, "label25");
            label25.Name = "label25";
            // 
            // label28
            // 
            resources.ApplyResources(label28, "label28");
            label28.Name = "label28";
            // 
            // label27
            // 
            resources.ApplyResources(label27, "label27");
            label27.Name = "label27";
            // 
            // numericBoxC
            // 
            resources.ApplyResources(numericBoxC, "numericBoxC");
            numericBoxC.BackColor = System.Drawing.SystemColors.Control;
            numericBoxC.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxC.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxC.Name = "numericBoxC";
            numericBoxC.RestrictLimitValue = false;
            numericBoxC.RoundErrorAccuracy = 12;
            numericBoxC.SkipEventDuringInput = false;
            numericBoxC.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxC, resources.GetString("numericBoxC.ToolTip1"));
            numericBoxC.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // numericBoxCErr
            // 
            resources.ApplyResources(numericBoxCErr, "numericBoxCErr");
            numericBoxCErr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCErr.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCErr.Name = "numericBoxCErr";
            numericBoxCErr.RestrictLimitValue = false;
            numericBoxCErr.RoundErrorAccuracy = 12;
            numericBoxCErr.SkipEventDuringInput = false;
            numericBoxCErr.SmartIncrement = true;
            numericBoxCErr.TabStop = false;
            numericBoxCErr.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // numericBoxAErr
            // 
            resources.ApplyResources(numericBoxAErr, "numericBoxAErr");
            numericBoxAErr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxAErr.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxAErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxAErr.Name = "numericBoxAErr";
            numericBoxAErr.RestrictLimitValue = false;
            numericBoxAErr.RoundErrorAccuracy = 12;
            numericBoxAErr.SkipEventDuringInput = false;
            numericBoxAErr.SmartIncrement = true;
            numericBoxAErr.TabStop = false;
            numericBoxAErr.ValueChanged += numericBoxCellConstants_ValueChanged;
            // 
            // labelLengthUnitA
            // 
            resources.ApplyResources(labelLengthUnitA, "labelLengthUnitA");
            labelLengthUnitA.Name = "labelLengthUnitA";
            // 
            // groupBoxSymmetry
            // 
            groupBoxSymmetry.Controls.Add(comboBoxSpaceGroup);
            groupBoxSymmetry.Controls.Add(comboBoxPointGroup);
            groupBoxSymmetry.Controls.Add(comboBoxCrystalSystem);
            groupBoxSymmetry.Controls.Add(label20);
            groupBoxSymmetry.Controls.Add(label17);
            groupBoxSymmetry.Controls.Add(label19);
            groupBoxSymmetry.Controls.Add(textBoxSearch);
            groupBoxSymmetry.Controls.Add(label21);
            groupBoxSymmetry.Controls.Add(comboBoxSearchResult);
            resources.ApplyResources(groupBoxSymmetry, "groupBoxSymmetry");
            groupBoxSymmetry.Name = "groupBoxSymmetry";
            groupBoxSymmetry.TabStop = false;
            // 
            // comboBoxSpaceGroup
            // 
            resources.ApplyResources(comboBoxSpaceGroup, "comboBoxSpaceGroup");
            comboBoxSpaceGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            comboBoxSpaceGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSpaceGroup.DropDownWidth = 250;
            comboBoxSpaceGroup.Name = "comboBoxSpaceGroup";
            toolTip.SetToolTip(comboBoxSpaceGroup, resources.GetString("comboBoxSpaceGroup.ToolTip"));
            comboBoxSpaceGroup.DrawItem += comboBoxSpaceGroup_DrawItem;
            comboBoxSpaceGroup.SelectedIndexChanged += comboBoxSpaceGroup_SelectedIndexChanged;
            // 
            // comboBoxPointGroup
            // 
            resources.ApplyResources(comboBoxPointGroup, "comboBoxPointGroup");
            comboBoxPointGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            comboBoxPointGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxPointGroup.Name = "comboBoxPointGroup";
            toolTip.SetToolTip(comboBoxPointGroup, resources.GetString("comboBoxPointGroup.ToolTip"));
            comboBoxPointGroup.DrawItem += comboBoxSpaceGroup_DrawItem;
            comboBoxPointGroup.SelectedIndexChanged += comboBoxPointGroup_SelectedIndexChanged;
            // 
            // comboBoxCrystalSystem
            // 
            resources.ApplyResources(comboBoxCrystalSystem, "comboBoxCrystalSystem");
            comboBoxCrystalSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxCrystalSystem.Items.AddRange(new object[] { resources.GetString("comboBoxCrystalSystem.Items"), resources.GetString("comboBoxCrystalSystem.Items1"), resources.GetString("comboBoxCrystalSystem.Items2"), resources.GetString("comboBoxCrystalSystem.Items3"), resources.GetString("comboBoxCrystalSystem.Items4"), resources.GetString("comboBoxCrystalSystem.Items5"), resources.GetString("comboBoxCrystalSystem.Items6"), resources.GetString("comboBoxCrystalSystem.Items7") });
            comboBoxCrystalSystem.Name = "comboBoxCrystalSystem";
            toolTip.SetToolTip(comboBoxCrystalSystem, resources.GetString("comboBoxCrystalSystem.ToolTip"));
            comboBoxCrystalSystem.SelectedIndexChanged += comboBoxCrystalSystem_SelectedIndexChanged;
            // 
            // label20
            // 
            resources.ApplyResources(label20, "label20");
            label20.Name = "label20";
            toolTip.SetToolTip(label20, resources.GetString("label20.ToolTip"));
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            toolTip.SetToolTip(label17, resources.GetString("label17.ToolTip"));
            // 
            // label19
            // 
            resources.ApplyResources(label19, "label19");
            label19.Name = "label19";
            toolTip.SetToolTip(label19, resources.GetString("label19.ToolTip"));
            // 
            // textBoxSearch
            // 
            resources.ApplyResources(textBoxSearch, "textBoxSearch");
            textBoxSearch.Name = "textBoxSearch";
            toolTip.SetToolTip(textBoxSearch, resources.GetString("textBoxSearch.ToolTip"));
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // label21
            // 
            resources.ApplyResources(label21, "label21");
            label21.Name = "label21";
            toolTip.SetToolTip(label21, resources.GetString("label21.ToolTip"));
            // 
            // comboBoxSearchResult
            // 
            resources.ApplyResources(comboBoxSearchResult, "comboBoxSearchResult");
            comboBoxSearchResult.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            comboBoxSearchResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSearchResult.DropDownWidth = 200;
            comboBoxSearchResult.Name = "comboBoxSearchResult";
            comboBoxSearchResult.DrawItem += comboBoxSpaceGroup_DrawItem;
            comboBoxSearchResult.SelectedIndexChanged += comboBoxSearchResult_SelectedIndexChanged;
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // SymmetryControl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(groupBox4);
            Controls.Add(panel1);
            Controls.Add(groupBoxSymmetry);
            Name = "SymmetryControl";
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBoxSymmetry.ResumeLayout(false);
            groupBoxSymmetry.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox4;
        private NumericBox numericBoxGammaErr;
        private NumericBox numericBoxBetaErr;
        private NumericBox numericBoxAlphaErr;
        private NumericBox numericBoxAlpha;
        private NumericBox numericBoxGamma;
        private NumericBox numericBoxBeta;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private NumericBox numericBoxAErr;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private NumericBox numericBoxCErr;
        private NumericBox numericBoxBErr;
        private System.Windows.Forms.Label label25;
        private NumericBox numericBoxA;
        private NumericBox numericBoxB;
        private NumericBox numericBoxC;
        private System.Windows.Forms.GroupBox groupBoxSymmetry;
        public System.Windows.Forms.ComboBox comboBoxSpaceGroup;
        public System.Windows.Forms.ComboBox comboBoxPointGroup;
        public System.Windows.Forms.ComboBox comboBoxCrystalSystem;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.ComboBox comboBoxSearchResult;
        private System.Windows.Forms.CheckBox checkBoxShowError;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RadioButton radioButtonAngstrom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonNanoMeter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelLengthUnitA;
        private System.Windows.Forms.Label labelLengthUnitB;
        private System.Windows.Forms.Label labelLengthUnitC;


    }
}
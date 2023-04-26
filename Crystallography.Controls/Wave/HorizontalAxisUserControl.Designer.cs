namespace Crystallography.Controls
{
    partial class HorizontalAxisUserControl
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HorizontalAxisUserControl));
            groupBox1 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            groupBox4 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonXray = new System.Windows.Forms.RadioButton();
            radioButtonElectron = new System.Windows.Forms.RadioButton();
            radioButtonNeutron = new System.Windows.Forms.RadioButton();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            groupBox3 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonMonochro = new System.Windows.Forms.RadioButton();
            radioButtonFlatWhite = new System.Windows.Forms.RadioButton();
            radioButtonCustomWhite = new System.Windows.Forms.RadioButton();
            groupBoxTwoTheta = new System.Windows.Forms.GroupBox();
            waveLengthControl = new WaveLengthControl();
            groupBox2 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel15 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonTwoTheta = new System.Windows.Forms.RadioButton();
            radioButtonDspacing = new System.Windows.Forms.RadioButton();
            radioButtonWavenumber = new System.Windows.Forms.RadioButton();
            radioButtonEnergy = new System.Windows.Forms.RadioButton();
            radioButtonTOF = new System.Windows.Forms.RadioButton();
            groupBoxOption = new System.Windows.Forms.GroupBox();
            flowLayoutPanel12 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelTwoTheta = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonAngleUnitDegree = new System.Windows.Forms.RadioButton();
            radioButtonAngleUnitRadian = new System.Windows.Forms.RadioButton();
            flowLayoutPanelDspacing = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonDspacingUnitAng = new System.Windows.Forms.RadioButton();
            radioButtonDspacingUnitNm = new System.Windows.Forms.RadioButton();
            flowLayoutPanelWavenumber = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonWavenumberAngInv = new System.Windows.Forms.RadioButton();
            radioButtonWavenumberUnitNmInv = new System.Windows.Forms.RadioButton();
            flowLayoutPanelEnergy = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonEnergyUnitEv = new System.Windows.Forms.RadioButton();
            radioButtonEnergyUnitKev = new System.Windows.Forms.RadioButton();
            radioButtonEnergyUnitMev = new System.Windows.Forms.RadioButton();
            numericBoxTwoTheta = new NumericBox();
            flowLayoutPanelNeutronTOF = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonTofUnitMicroSec = new System.Windows.Forms.RadioButton();
            radioButtonTofUnitNanoSec = new System.Windows.Forms.RadioButton();
            numericBoxTofTakeOffAngle = new NumericBox();
            numericBoxTofDistance = new NumericBox();
            toolTip = new System.Windows.Forms.ToolTip(components);
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            groupBox1.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            groupBox4.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            groupBox3.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            groupBoxTwoTheta.SuspendLayout();
            groupBox2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel15.SuspendLayout();
            groupBoxOption.SuspendLayout();
            flowLayoutPanel12.SuspendLayout();
            flowLayoutPanelTwoTheta.SuspendLayout();
            flowLayoutPanelDspacing.SuspendLayout();
            flowLayoutPanelWavenumber.SuspendLayout();
            flowLayoutPanelEnergy.SuspendLayout();
            flowLayoutPanelNeutronTOF.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(flowLayoutPanel7);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // flowLayoutPanel7
            // 
            resources.ApplyResources(flowLayoutPanel7, "flowLayoutPanel7");
            flowLayoutPanel7.Controls.Add(flowLayoutPanel5);
            flowLayoutPanel7.Controls.Add(groupBoxTwoTheta);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(groupBox4);
            flowLayoutPanel5.Controls.Add(groupBox3);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            // 
            // groupBox4
            // 
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Controls.Add(flowLayoutPanel4);
            groupBox4.Controls.Add(flowLayoutPanel1);
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            toolTip.SetToolTip(groupBox4, resources.GetString("groupBox4.ToolTip"));
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(radioButtonXray);
            flowLayoutPanel4.Controls.Add(radioButtonElectron);
            flowLayoutPanel4.Controls.Add(radioButtonNeutron);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // radioButtonXray
            // 
            resources.ApplyResources(radioButtonXray, "radioButtonXray");
            radioButtonXray.Checked = true;
            radioButtonXray.Name = "radioButtonXray";
            radioButtonXray.TabStop = true;
            radioButtonXray.UseVisualStyleBackColor = true;
            radioButtonXray.CheckedChanged += radioButtonWaveSource_CheckedChanged;
            // 
            // radioButtonElectron
            // 
            resources.ApplyResources(radioButtonElectron, "radioButtonElectron");
            radioButtonElectron.Name = "radioButtonElectron";
            radioButtonElectron.UseVisualStyleBackColor = true;
            radioButtonElectron.CheckedChanged += radioButtonWaveSource_CheckedChanged;
            // 
            // radioButtonNeutron
            // 
            resources.ApplyResources(radioButtonNeutron, "radioButtonNeutron");
            radioButtonNeutron.Name = "radioButtonNeutron";
            radioButtonNeutron.UseVisualStyleBackColor = true;
            radioButtonNeutron.CheckedChanged += radioButtonWaveSource_CheckedChanged;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // groupBox3
            // 
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Controls.Add(flowLayoutPanel2);
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(radioButtonMonochro);
            flowLayoutPanel2.Controls.Add(radioButtonFlatWhite);
            flowLayoutPanel2.Controls.Add(radioButtonCustomWhite);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // radioButtonMonochro
            // 
            resources.ApplyResources(radioButtonMonochro, "radioButtonMonochro");
            radioButtonMonochro.Checked = true;
            radioButtonMonochro.Name = "radioButtonMonochro";
            radioButtonMonochro.TabStop = true;
            radioButtonMonochro.UseVisualStyleBackColor = true;
            radioButtonMonochro.CheckedChanged += radioButtonMonochro_CheckedChanged;
            // 
            // radioButtonFlatWhite
            // 
            resources.ApplyResources(radioButtonFlatWhite, "radioButtonFlatWhite");
            radioButtonFlatWhite.Name = "radioButtonFlatWhite";
            toolTip.SetToolTip(radioButtonFlatWhite, resources.GetString("radioButtonFlatWhite.ToolTip"));
            radioButtonFlatWhite.UseVisualStyleBackColor = true;
            radioButtonFlatWhite.CheckedChanged += radioButtonMonochro_CheckedChanged;
            // 
            // radioButtonCustomWhite
            // 
            resources.ApplyResources(radioButtonCustomWhite, "radioButtonCustomWhite");
            radioButtonCustomWhite.Name = "radioButtonCustomWhite";
            radioButtonCustomWhite.UseVisualStyleBackColor = true;
            radioButtonCustomWhite.CheckedChanged += radioButtonMonochro_CheckedChanged;
            // 
            // groupBoxTwoTheta
            // 
            resources.ApplyResources(groupBoxTwoTheta, "groupBoxTwoTheta");
            groupBoxTwoTheta.Controls.Add(waveLengthControl);
            groupBoxTwoTheta.Name = "groupBoxTwoTheta";
            groupBoxTwoTheta.TabStop = false;
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            waveLengthControl.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl.Energy = 8.04114721D;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = false;
            waveLengthControl.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            waveLengthControl.WaveLength = 0.1541871066667D;
            waveLengthControl.WaveSource = WaveSource.Xray;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            waveLengthControl.XrayWaveSourceLine = XrayLine.Ka1;
            waveLengthControl.WavelengthChanged += waveLengthControl_WavelengthChanged;
            // 
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(flowLayoutPanel3);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(flowLayoutPanel15);
            flowLayoutPanel3.Controls.Add(groupBoxOption);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // flowLayoutPanel15
            // 
            resources.ApplyResources(flowLayoutPanel15, "flowLayoutPanel15");
            flowLayoutPanel15.Controls.Add(radioButtonTwoTheta);
            flowLayoutPanel15.Controls.Add(radioButtonDspacing);
            flowLayoutPanel15.Controls.Add(radioButtonWavenumber);
            flowLayoutPanel15.Controls.Add(radioButtonEnergy);
            flowLayoutPanel15.Controls.Add(radioButtonTOF);
            flowLayoutPanel15.Name = "flowLayoutPanel15";
            // 
            // radioButtonTwoTheta
            // 
            resources.ApplyResources(radioButtonTwoTheta, "radioButtonTwoTheta");
            radioButtonTwoTheta.Checked = true;
            radioButtonTwoTheta.Name = "radioButtonTwoTheta";
            radioButtonTwoTheta.TabStop = true;
            radioButtonTwoTheta.UseVisualStyleBackColor = true;
            radioButtonTwoTheta.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonDspacing
            // 
            resources.ApplyResources(radioButtonDspacing, "radioButtonDspacing");
            radioButtonDspacing.Name = "radioButtonDspacing";
            radioButtonDspacing.UseVisualStyleBackColor = true;
            radioButtonDspacing.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonWavenumber
            // 
            resources.ApplyResources(radioButtonWavenumber, "radioButtonWavenumber");
            radioButtonWavenumber.Name = "radioButtonWavenumber";
            radioButtonWavenumber.UseVisualStyleBackColor = true;
            radioButtonWavenumber.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonEnergy
            // 
            resources.ApplyResources(radioButtonEnergy, "radioButtonEnergy");
            radioButtonEnergy.Name = "radioButtonEnergy";
            radioButtonEnergy.UseVisualStyleBackColor = true;
            radioButtonEnergy.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonTOF
            // 
            resources.ApplyResources(radioButtonTOF, "radioButtonTOF");
            radioButtonTOF.Name = "radioButtonTOF";
            radioButtonTOF.UseVisualStyleBackColor = true;
            radioButtonTOF.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // groupBoxOption
            // 
            resources.ApplyResources(groupBoxOption, "groupBoxOption");
            groupBoxOption.Controls.Add(flowLayoutPanel12);
            groupBoxOption.Name = "groupBoxOption";
            groupBoxOption.TabStop = false;
            // 
            // flowLayoutPanel12
            // 
            resources.ApplyResources(flowLayoutPanel12, "flowLayoutPanel12");
            flowLayoutPanel12.Controls.Add(flowLayoutPanelTwoTheta);
            flowLayoutPanel12.Controls.Add(flowLayoutPanelDspacing);
            flowLayoutPanel12.Controls.Add(flowLayoutPanelWavenumber);
            flowLayoutPanel12.Controls.Add(flowLayoutPanelEnergy);
            flowLayoutPanel12.Controls.Add(flowLayoutPanelNeutronTOF);
            flowLayoutPanel12.Name = "flowLayoutPanel12";
            // 
            // flowLayoutPanelTwoTheta
            // 
            resources.ApplyResources(flowLayoutPanelTwoTheta, "flowLayoutPanelTwoTheta");
            flowLayoutPanelTwoTheta.Controls.Add(radioButtonAngleUnitDegree);
            flowLayoutPanelTwoTheta.Controls.Add(radioButtonAngleUnitRadian);
            flowLayoutPanelTwoTheta.Name = "flowLayoutPanelTwoTheta";
            // 
            // radioButtonAngleUnitDegree
            // 
            resources.ApplyResources(radioButtonAngleUnitDegree, "radioButtonAngleUnitDegree");
            radioButtonAngleUnitDegree.Checked = true;
            radioButtonAngleUnitDegree.Name = "radioButtonAngleUnitDegree";
            radioButtonAngleUnitDegree.TabStop = true;
            radioButtonAngleUnitDegree.UseVisualStyleBackColor = true;
            radioButtonAngleUnitDegree.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonAngleUnitRadian
            // 
            resources.ApplyResources(radioButtonAngleUnitRadian, "radioButtonAngleUnitRadian");
            radioButtonAngleUnitRadian.Name = "radioButtonAngleUnitRadian";
            radioButtonAngleUnitRadian.UseVisualStyleBackColor = true;
            radioButtonAngleUnitRadian.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // flowLayoutPanelDspacing
            // 
            resources.ApplyResources(flowLayoutPanelDspacing, "flowLayoutPanelDspacing");
            flowLayoutPanelDspacing.Controls.Add(radioButtonDspacingUnitAng);
            flowLayoutPanelDspacing.Controls.Add(radioButtonDspacingUnitNm);
            flowLayoutPanelDspacing.Name = "flowLayoutPanelDspacing";
            // 
            // radioButtonDspacingUnitAng
            // 
            resources.ApplyResources(radioButtonDspacingUnitAng, "radioButtonDspacingUnitAng");
            radioButtonDspacingUnitAng.Checked = true;
            radioButtonDspacingUnitAng.Name = "radioButtonDspacingUnitAng";
            radioButtonDspacingUnitAng.TabStop = true;
            radioButtonDspacingUnitAng.UseVisualStyleBackColor = true;
            radioButtonDspacingUnitAng.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonDspacingUnitNm
            // 
            resources.ApplyResources(radioButtonDspacingUnitNm, "radioButtonDspacingUnitNm");
            radioButtonDspacingUnitNm.Name = "radioButtonDspacingUnitNm";
            radioButtonDspacingUnitNm.UseVisualStyleBackColor = true;
            radioButtonDspacingUnitNm.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // flowLayoutPanelWavenumber
            // 
            resources.ApplyResources(flowLayoutPanelWavenumber, "flowLayoutPanelWavenumber");
            flowLayoutPanelWavenumber.Controls.Add(radioButtonWavenumberAngInv);
            flowLayoutPanelWavenumber.Controls.Add(radioButtonWavenumberUnitNmInv);
            flowLayoutPanelWavenumber.Name = "flowLayoutPanelWavenumber";
            // 
            // radioButtonWavenumberAngInv
            // 
            resources.ApplyResources(radioButtonWavenumberAngInv, "radioButtonWavenumberAngInv");
            radioButtonWavenumberAngInv.Checked = true;
            radioButtonWavenumberAngInv.Name = "radioButtonWavenumberAngInv";
            radioButtonWavenumberAngInv.TabStop = true;
            radioButtonWavenumberAngInv.UseVisualStyleBackColor = true;
            radioButtonWavenumberAngInv.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonWavenumberUnitNmInv
            // 
            resources.ApplyResources(radioButtonWavenumberUnitNmInv, "radioButtonWavenumberUnitNmInv");
            radioButtonWavenumberUnitNmInv.Name = "radioButtonWavenumberUnitNmInv";
            radioButtonWavenumberUnitNmInv.UseVisualStyleBackColor = true;
            radioButtonWavenumberUnitNmInv.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // flowLayoutPanelEnergy
            // 
            resources.ApplyResources(flowLayoutPanelEnergy, "flowLayoutPanelEnergy");
            flowLayoutPanelEnergy.Controls.Add(radioButtonEnergyUnitEv);
            flowLayoutPanelEnergy.Controls.Add(radioButtonEnergyUnitKev);
            flowLayoutPanelEnergy.Controls.Add(radioButtonEnergyUnitMev);
            flowLayoutPanelEnergy.Controls.Add(numericBoxTwoTheta);
            flowLayoutPanelEnergy.Name = "flowLayoutPanelEnergy";
            // 
            // radioButtonEnergyUnitEv
            // 
            resources.ApplyResources(radioButtonEnergyUnitEv, "radioButtonEnergyUnitEv");
            radioButtonEnergyUnitEv.Checked = true;
            radioButtonEnergyUnitEv.Name = "radioButtonEnergyUnitEv";
            radioButtonEnergyUnitEv.TabStop = true;
            radioButtonEnergyUnitEv.UseVisualStyleBackColor = true;
            radioButtonEnergyUnitEv.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonEnergyUnitKev
            // 
            resources.ApplyResources(radioButtonEnergyUnitKev, "radioButtonEnergyUnitKev");
            radioButtonEnergyUnitKev.Name = "radioButtonEnergyUnitKev";
            radioButtonEnergyUnitKev.UseVisualStyleBackColor = true;
            radioButtonEnergyUnitKev.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonEnergyUnitMev
            // 
            resources.ApplyResources(radioButtonEnergyUnitMev, "radioButtonEnergyUnitMev");
            radioButtonEnergyUnitMev.Name = "radioButtonEnergyUnitMev";
            radioButtonEnergyUnitMev.UseVisualStyleBackColor = true;
            radioButtonEnergyUnitMev.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // numericBoxTwoTheta
            // 
            resources.ApplyResources(numericBoxTwoTheta, "numericBoxTwoTheta");
            numericBoxTwoTheta.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTwoTheta.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTwoTheta.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTwoTheta.Name = "numericBoxTwoTheta";
            numericBoxTwoTheta.RoundErrorAccuracy = -1;
            numericBoxTwoTheta.SkipEventDuringInput = false;
            numericBoxTwoTheta.SmartIncrement = true;
            numericBoxTwoTheta.ThonsandsSeparator = true;
            numericBoxTwoTheta.ValueChanged += numericBoxTwoTheta_ValueChanged;
            // 
            // flowLayoutPanelNeutronTOF
            // 
            resources.ApplyResources(flowLayoutPanelNeutronTOF, "flowLayoutPanelNeutronTOF");
            flowLayoutPanelNeutronTOF.Controls.Add(radioButtonTofUnitMicroSec);
            flowLayoutPanelNeutronTOF.Controls.Add(radioButtonTofUnitNanoSec);
            flowLayoutPanelNeutronTOF.Controls.Add(numericBoxTofTakeOffAngle);
            flowLayoutPanelNeutronTOF.Controls.Add(numericBoxTofDistance);
            flowLayoutPanelNeutronTOF.Name = "flowLayoutPanelNeutronTOF";
            // 
            // radioButtonTofUnitMicroSec
            // 
            resources.ApplyResources(radioButtonTofUnitMicroSec, "radioButtonTofUnitMicroSec");
            radioButtonTofUnitMicroSec.Checked = true;
            radioButtonTofUnitMicroSec.Name = "radioButtonTofUnitMicroSec";
            radioButtonTofUnitMicroSec.TabStop = true;
            radioButtonTofUnitMicroSec.UseVisualStyleBackColor = true;
            radioButtonTofUnitMicroSec.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonTofUnitNanoSec
            // 
            resources.ApplyResources(radioButtonTofUnitNanoSec, "radioButtonTofUnitNanoSec");
            radioButtonTofUnitNanoSec.Name = "radioButtonTofUnitNanoSec";
            radioButtonTofUnitNanoSec.UseVisualStyleBackColor = true;
            radioButtonTofUnitNanoSec.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // numericBoxTofTakeOffAngle
            // 
            resources.ApplyResources(numericBoxTofTakeOffAngle, "numericBoxTofTakeOffAngle");
            numericBoxTofTakeOffAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTofTakeOffAngle.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTofTakeOffAngle.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTofTakeOffAngle.Name = "numericBoxTofTakeOffAngle";
            numericBoxTofTakeOffAngle.RadianValue = 1.5707963267948966D;
            numericBoxTofTakeOffAngle.RoundErrorAccuracy = -1;
            numericBoxTofTakeOffAngle.SkipEventDuringInput = false;
            numericBoxTofTakeOffAngle.SmartIncrement = true;
            numericBoxTofTakeOffAngle.ThonsandsSeparator = true;
            numericBoxTofTakeOffAngle.Value = 90D;
            numericBoxTofTakeOffAngle.ValueChanged += numericBoxTwoTheta_ValueChanged;
            // 
            // numericBoxTofDistance
            // 
            resources.ApplyResources(numericBoxTofDistance, "numericBoxTofDistance");
            numericBoxTofDistance.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTofDistance.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTofDistance.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTofDistance.Name = "numericBoxTofDistance";
            numericBoxTofDistance.RadianValue = 0.73303828583761843D;
            numericBoxTofDistance.RoundErrorAccuracy = -1;
            numericBoxTofDistance.SkipEventDuringInput = false;
            numericBoxTofDistance.SmartIncrement = true;
            numericBoxTofDistance.ThonsandsSeparator = true;
            numericBoxTofDistance.Value = 42D;
            numericBoxTofDistance.ValueChanged += numericBoxTwoTheta_ValueChanged;
            // 
            // flowLayoutPanel6
            // 
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Controls.Add(groupBox1);
            flowLayoutPanel6.Controls.Add(groupBox2);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            // 
            // HorizontalAxisUserControl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(flowLayoutPanel6);
            Name = "HorizontalAxisUserControl";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            groupBoxTwoTheta.ResumeLayout(false);
            groupBoxTwoTheta.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel15.ResumeLayout(false);
            flowLayoutPanel15.PerformLayout();
            groupBoxOption.ResumeLayout(false);
            groupBoxOption.PerformLayout();
            flowLayoutPanel12.ResumeLayout(false);
            flowLayoutPanel12.PerformLayout();
            flowLayoutPanelTwoTheta.ResumeLayout(false);
            flowLayoutPanelTwoTheta.PerformLayout();
            flowLayoutPanelDspacing.ResumeLayout(false);
            flowLayoutPanelDspacing.PerformLayout();
            flowLayoutPanelWavenumber.ResumeLayout(false);
            flowLayoutPanelWavenumber.PerformLayout();
            flowLayoutPanelEnergy.ResumeLayout(false);
            flowLayoutPanelEnergy.PerformLayout();
            flowLayoutPanelNeutronTOF.ResumeLayout(false);
            flowLayoutPanelNeutronTOF.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonDspacing;
        private System.Windows.Forms.RadioButton radioButtonEnergy;
        private System.Windows.Forms.GroupBox groupBoxTwoTheta;
        private Crystallography.Controls.NumericBox numericBoxTwoTheta;
        private WaveLengthControl waveLengthControl;
        private System.Windows.Forms.RadioButton radioButtonTOF;
        private NumericBox numericBoxTofDistance;
        private System.Windows.Forms.RadioButton radioButtonNeutron;
        private System.Windows.Forms.RadioButton radioButtonElectron;
        private System.Windows.Forms.RadioButton radioButtonXray;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private NumericBox numericBoxTofTakeOffAngle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton radioButtonMonochro;
        private System.Windows.Forms.RadioButton radioButtonFlatWhite;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonTwoTheta;
        public System.Windows.Forms.RadioButton radioButtonTofUnitNanoSec;
        public System.Windows.Forms.RadioButton radioButtonTofUnitMicroSec;
        private System.Windows.Forms.RadioButton radioButtonEnergyUnitEv;
        private System.Windows.Forms.RadioButton radioButtonEnergyUnitKev;
        public System.Windows.Forms.RadioButton radioButtonDspacingUnitAng;
        public System.Windows.Forms.RadioButton radioButtonDspacingUnitNm;
        private System.Windows.Forms.RadioButton radioButtonCustomWhite;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel15;
        private System.Windows.Forms.RadioButton radioButtonWavenumber;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel12;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDspacing;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelWavenumber;
        public System.Windows.Forms.RadioButton radioButtonWavenumberUnitNmInv;
        public System.Windows.Forms.RadioButton radioButtonWavenumberAngInv;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelEnergy;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelNeutronTOF;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBoxOption;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTwoTheta;
        public System.Windows.Forms.RadioButton radioButtonAngleUnitRadian;
        private System.Windows.Forms.RadioButton radioButtonEnergyUnitMev;
        public System.Windows.Forms.RadioButton radioButtonAngleUnitDegree;
    }
}

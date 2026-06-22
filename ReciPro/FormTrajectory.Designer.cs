namespace ReciPro
{
    partial class FormTrajectory
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        // (260323Ch) renamed numeric container controls:
        // groupBox1 -> groupBoxEnergyDistribution
        // groupBox2 -> groupBoxPenetrationDepth
        // groupBox4 -> groupBoxSurfaceDistance
        // groupBox5 -> groupBoxDirectionDistribution
        // groupBox6 -> groupBoxStatistics
        // flowLayoutPanel1 -> flowLayoutPanelViewAlong
        // flowLayoutPanel2 -> flowLayoutPanelProfiles
        // panel1 -> panelCalculationConditions
        // panel2 -> panelStereonetOptions
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTrajectory));
            toolTip = new System.Windows.Forms.ToolTip(components);
            waveLengthControl = new WaveLengthControl();
            buttonSimulate = new System.Windows.Forms.Button();
            numericBoxSampleTilt = new NumericBox();
            buttonViewFromX = new System.Windows.Forms.Button();
            buttonViewFromZ = new System.Windows.Forms.Button();
            numericBoxCalcNum = new NumericBox();
            numericBoxDrawNum = new NumericBox();
            checkBoxDrawAxes = new System.Windows.Forms.CheckBox();
            checkBoxDrawGuidCircles = new System.Windows.Forms.CheckBox();
            buttonSurfaceNormal = new System.Windows.Forms.Button();
            checkBoxDrawAbsorved = new System.Windows.Forms.CheckBox();
            radioButtonStandardDeviation = new System.Windows.Forms.RadioButton();
            radioButtonAverageEnergy = new System.Windows.Forms.RadioButton();
            radioButtonFrequency = new System.Windows.Forms.RadioButton();
            checkBoxDrawAxesInStereonet = new System.Windows.Forms.CheckBox();
            labelBSEenergy = new System.Windows.Forms.Label();
            labelStoppingPower = new System.Windows.Forms.Label();
            labelCrossSection = new System.Windows.Forms.Label();
            labelMeanFreePath = new System.Windows.Forms.Label();
            labelBSEratio = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            checkBoxDrawPathAfterEscape = new System.Windows.Forms.CheckBox();
            poleFigureControl = new PoleFigureControl2();
            paneltTrajectory = new System.Windows.Forms.Panel();
            graphControlEnergyProfile = new GraphControl();
            groupBoxEnergyDistribution = new System.Windows.Forms.GroupBox();
            groupBoxPenetrationDepth = new System.Windows.Forms.GroupBox();
            graphControlDepthProfile = new GraphControl();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            groupBoxSurfaceDistance = new System.Windows.Forms.GroupBox();
            graphControlDistance = new GraphControl();
            flowLayoutPanelViewAlong = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelProfiles = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxDirectionDistribution = new System.Windows.Forms.GroupBox();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxStatistics = new System.Windows.Forms.GroupBox();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            panel2 = new System.Windows.Forms.Panel();
            groupBoxEnergyDistribution.SuspendLayout();
            groupBoxPenetrationDepth.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBoxSurfaceDistance.SuspendLayout();
            flowLayoutPanelViewAlong.SuspendLayout();
            flowLayoutPanelProfiles.SuspendLayout();
            groupBoxDirectionDistribution.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            groupBoxStatistics.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel9.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 100;
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            waveLengthControl.DirectionWaveEnergy = System.Windows.Forms.FlowDirection.LeftToRight;
            waveLengthControl.DirectionWhole = System.Windows.Forms.FlowDirection.LeftToRight;
            waveLengthControl.Energy = 20D;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = false;
            toolTip.SetToolTip(waveLengthControl, resources.GetString("waveLengthControl.ToolTip"));
            waveLengthControl.WaveLength = 0.008588514105D;
            waveLengthControl.WaveSource = WaveSource.Electron;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            // 
            // buttonSimulate
            // 
            resources.ApplyResources(buttonSimulate, "buttonSimulate");
            buttonSimulate.BackColor = System.Drawing.Color.SteelBlue;
            buttonSimulate.ForeColor = System.Drawing.Color.White;
            buttonSimulate.Name = "buttonSimulate";
            toolTip.SetToolTip(buttonSimulate, resources.GetString("buttonSimulate.ToolTip"));
            buttonSimulate.UseVisualStyleBackColor = false;
            buttonSimulate.Click += buttonCalculate_Click;
            // 
            // numericBoxSampleTilt
            // 
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxSampleTilt, "numericBoxSampleTilt");
            numericBoxSampleTilt.Maximum = 0D;
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxSampleTilt, resources.GetString("numericBoxSampleTilt.ToolTip"));
            numericBoxSampleTilt.UpDown_Increment = 10D;
            numericBoxSampleTilt.ValueBoxWidth = 50;
            // 
            // buttonViewFromX
            // 
            resources.ApplyResources(buttonViewFromX, "buttonViewFromX");
            buttonViewFromX.Name = "buttonViewFromX";
            toolTip.SetToolTip(buttonViewFromX, resources.GetString("buttonViewFromX.ToolTip"));
            buttonViewFromX.UseVisualStyleBackColor = true;
            buttonViewFromX.Click += buttonViewFromX_Click;
            // 
            // buttonViewFromZ
            // 
            resources.ApplyResources(buttonViewFromZ, "buttonViewFromZ");
            buttonViewFromZ.Name = "buttonViewFromZ";
            toolTip.SetToolTip(buttonViewFromZ, resources.GetString("buttonViewFromZ.ToolTip"));
            buttonViewFromZ.UseVisualStyleBackColor = true;
            buttonViewFromZ.Click += buttonViewFromZ_Click;
            // 
            // numericBoxCalcNum
            // 
            numericBoxCalcNum.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalcNum.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxCalcNum, "numericBoxCalcNum");
            numericBoxCalcNum.Maximum = 1000000D;
            numericBoxCalcNum.Minimum = 100D;
            numericBoxCalcNum.Name = "numericBoxCalcNum";
            numericBoxCalcNum.RadianValue = 1745.3292519943295D;
            numericBoxCalcNum.ShowUpDown = true;
            numericBoxCalcNum.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxCalcNum, resources.GetString("numericBoxCalcNum.ToolTip"));
            numericBoxCalcNum.Value = 100000D;
            numericBoxCalcNum.ValueBoxWidth = 60;
            // 
            // numericBoxDrawNum
            // 
            numericBoxDrawNum.BackColor = System.Drawing.Color.Transparent;
            numericBoxDrawNum.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxDrawNum, "numericBoxDrawNum");
            numericBoxDrawNum.Maximum = 100000D;
            numericBoxDrawNum.Minimum = 1D;
            numericBoxDrawNum.Name = "numericBoxDrawNum";
            numericBoxDrawNum.RadianValue = 8.7266462599716466D;
            numericBoxDrawNum.ShowUpDown = true;
            numericBoxDrawNum.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxDrawNum, resources.GetString("numericBoxDrawNum.ToolTip"));
            numericBoxDrawNum.Value = 500D;
            numericBoxDrawNum.ValueBoxWidth = 44;
            numericBoxDrawNum.ValueChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // checkBoxDrawAxes
            // 
            resources.ApplyResources(checkBoxDrawAxes, "checkBoxDrawAxes");
            checkBoxDrawAxes.Checked = true;
            checkBoxDrawAxes.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxes.Name = "checkBoxDrawAxes";
            toolTip.SetToolTip(checkBoxDrawAxes, resources.GetString("checkBoxDrawAxes.ToolTip"));
            checkBoxDrawAxes.UseVisualStyleBackColor = true;
            checkBoxDrawAxes.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // checkBoxDrawGuidCircles
            // 
            resources.ApplyResources(checkBoxDrawGuidCircles, "checkBoxDrawGuidCircles");
            checkBoxDrawGuidCircles.Checked = true;
            checkBoxDrawGuidCircles.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawGuidCircles.Name = "checkBoxDrawGuidCircles";
            toolTip.SetToolTip(checkBoxDrawGuidCircles, resources.GetString("checkBoxDrawGuidCircles.ToolTip"));
            checkBoxDrawGuidCircles.UseVisualStyleBackColor = true;
            checkBoxDrawGuidCircles.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // buttonSurfaceNormal
            // 
            resources.ApplyResources(buttonSurfaceNormal, "buttonSurfaceNormal");
            buttonSurfaceNormal.Name = "buttonSurfaceNormal";
            toolTip.SetToolTip(buttonSurfaceNormal, resources.GetString("buttonSurfaceNormal.ToolTip"));
            buttonSurfaceNormal.UseVisualStyleBackColor = true;
            buttonSurfaceNormal.Click += buttonViewFromSurfaceNormal_Click;
            // 
            // checkBoxDrawAbsorved
            // 
            resources.ApplyResources(checkBoxDrawAbsorved, "checkBoxDrawAbsorved");
            checkBoxDrawAbsorved.Name = "checkBoxDrawAbsorved";
            toolTip.SetToolTip(checkBoxDrawAbsorved, resources.GetString("checkBoxDrawAbsorved.ToolTip"));
            checkBoxDrawAbsorved.UseVisualStyleBackColor = true;
            checkBoxDrawAbsorved.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // radioButtonStandardDeviation
            // 
            resources.ApplyResources(radioButtonStandardDeviation, "radioButtonStandardDeviation");
            radioButtonStandardDeviation.Name = "radioButtonStandardDeviation";
            radioButtonStandardDeviation.TabStop = true;
            toolTip.SetToolTip(radioButtonStandardDeviation, resources.GetString("radioButtonStandardDeviation.ToolTip"));
            radioButtonStandardDeviation.UseVisualStyleBackColor = true;
            radioButtonStandardDeviation.CheckedChanged += radioButtonFrequency_CheckedChanged;
            // 
            // radioButtonAverageEnergy
            // 
            resources.ApplyResources(radioButtonAverageEnergy, "radioButtonAverageEnergy");
            radioButtonAverageEnergy.Name = "radioButtonAverageEnergy";
            radioButtonAverageEnergy.TabStop = true;
            toolTip.SetToolTip(radioButtonAverageEnergy, resources.GetString("radioButtonAverageEnergy.ToolTip"));
            radioButtonAverageEnergy.UseVisualStyleBackColor = true;
            radioButtonAverageEnergy.CheckedChanged += radioButtonFrequency_CheckedChanged;
            // 
            // radioButtonFrequency
            // 
            resources.ApplyResources(radioButtonFrequency, "radioButtonFrequency");
            radioButtonFrequency.Checked = true;
            radioButtonFrequency.Name = "radioButtonFrequency";
            radioButtonFrequency.TabStop = true;
            toolTip.SetToolTip(radioButtonFrequency, resources.GetString("radioButtonFrequency.ToolTip"));
            radioButtonFrequency.UseVisualStyleBackColor = true;
            radioButtonFrequency.CheckedChanged += radioButtonFrequency_CheckedChanged;
            // 
            // checkBoxDrawAxesInStereonet
            // 
            resources.ApplyResources(checkBoxDrawAxesInStereonet, "checkBoxDrawAxesInStereonet");
            checkBoxDrawAxesInStereonet.Checked = true;
            checkBoxDrawAxesInStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxesInStereonet.Name = "checkBoxDrawAxesInStereonet";
            toolTip.SetToolTip(checkBoxDrawAxesInStereonet, resources.GetString("checkBoxDrawAxesInStereonet.ToolTip"));
            checkBoxDrawAxesInStereonet.UseVisualStyleBackColor = true;
            checkBoxDrawAxesInStereonet.CheckedChanged += checkBoxDrawAxesInStereonet_CheckedChanged;
            // 
            // labelBSEenergy
            // 
            resources.ApplyResources(labelBSEenergy, "labelBSEenergy");
            labelBSEenergy.Name = "labelBSEenergy";
            toolTip.SetToolTip(labelBSEenergy, resources.GetString("labelBSEenergy.ToolTip"));
            // 
            // labelStoppingPower
            // 
            resources.ApplyResources(labelStoppingPower, "labelStoppingPower");
            labelStoppingPower.Name = "labelStoppingPower";
            toolTip.SetToolTip(labelStoppingPower, resources.GetString("labelStoppingPower.ToolTip"));
            // 
            // labelCrossSection
            // 
            resources.ApplyResources(labelCrossSection, "labelCrossSection");
            labelCrossSection.Name = "labelCrossSection";
            toolTip.SetToolTip(labelCrossSection, resources.GetString("labelCrossSection.ToolTip"));
            // 
            // labelMeanFreePath
            // 
            resources.ApplyResources(labelMeanFreePath, "labelMeanFreePath");
            labelMeanFreePath.Name = "labelMeanFreePath";
            toolTip.SetToolTip(labelMeanFreePath, resources.GetString("labelMeanFreePath.ToolTip"));
            // 
            // labelBSEratio
            // 
            resources.ApplyResources(labelBSEratio, "labelBSEratio");
            labelBSEratio.Name = "labelBSEratio";
            toolTip.SetToolTip(labelBSEratio, resources.GetString("labelBSEratio.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // checkBoxDrawPathAfterEscape
            // 
            resources.ApplyResources(checkBoxDrawPathAfterEscape, "checkBoxDrawPathAfterEscape");
            checkBoxDrawPathAfterEscape.Checked = true;
            checkBoxDrawPathAfterEscape.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawPathAfterEscape.Name = "checkBoxDrawPathAfterEscape";
            toolTip.SetToolTip(checkBoxDrawPathAfterEscape, resources.GetString("checkBoxDrawPathAfterEscape.ToolTip"));
            checkBoxDrawPathAfterEscape.UseVisualStyleBackColor = true;
            checkBoxDrawPathAfterEscape.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // poleFigureControl
            // 
            resources.ApplyResources(poleFigureControl, "poleFigureControl");
            poleFigureControl.Name = "poleFigureControl";
            toolTip.SetToolTip(poleFigureControl, resources.GetString("poleFigureControl.ToolTip"));
            // 
            // paneltTrajectory
            // 
            resources.ApplyResources(paneltTrajectory, "paneltTrajectory");
            paneltTrajectory.Name = "paneltTrajectory";
            // 
            // graphControlEnergyProfile
            // 
            resources.ApplyResources(graphControlEnergyProfile, "graphControlEnergyProfile");
            graphControlEnergyProfile.Mode = GraphControl.DrawingMode.Histogram;
            graphControlEnergyProfile.MousePositionXDigit = 3;
            graphControlEnergyProfile.MousePositionYDigit = 3;
            graphControlEnergyProfile.Name = "graphControlEnergyProfile";
            // 
            // groupBoxEnergyDistribution
            // 
            captureExtender.SetCapture(groupBoxEnergyDistribution, true);
            groupBoxEnergyDistribution.Controls.Add(graphControlEnergyProfile);
            resources.ApplyResources(groupBoxEnergyDistribution, "groupBoxEnergyDistribution");
            groupBoxEnergyDistribution.Name = "groupBoxEnergyDistribution";
            groupBoxEnergyDistribution.TabStop = false;
            // 
            // groupBoxPenetrationDepth
            // 
            groupBoxPenetrationDepth.Controls.Add(graphControlDepthProfile);
            resources.ApplyResources(groupBoxPenetrationDepth, "groupBoxPenetrationDepth");
            groupBoxPenetrationDepth.Name = "groupBoxPenetrationDepth";
            groupBoxPenetrationDepth.TabStop = false;
            // 
            // graphControlDepthProfile
            // 
            resources.ApplyResources(graphControlDepthProfile, "graphControlDepthProfile");
            graphControlDepthProfile.Mode = GraphControl.DrawingMode.Histogram;
            graphControlDepthProfile.MousePositionXDigit = 3;
            graphControlDepthProfile.MousePositionYDigit = 3;
            graphControlDepthProfile.Name = "graphControlDepthProfile";
            graphControlDepthProfile.UnitX = " µm";
            graphControlDepthProfile.UnitY = " %";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // groupBoxSurfaceDistance
            // 
            groupBoxSurfaceDistance.Controls.Add(graphControlDistance);
            resources.ApplyResources(groupBoxSurfaceDistance, "groupBoxSurfaceDistance");
            groupBoxSurfaceDistance.Name = "groupBoxSurfaceDistance";
            groupBoxSurfaceDistance.TabStop = false;
            // 
            // graphControlDistance
            // 
            resources.ApplyResources(graphControlDistance, "graphControlDistance");
            graphControlDistance.Mode = GraphControl.DrawingMode.Histogram;
            graphControlDistance.MousePositionXDigit = 3;
            graphControlDistance.MousePositionYDigit = 3;
            graphControlDistance.Name = "graphControlDistance";
            graphControlDistance.UnitX = " nm";
            graphControlDistance.UnitY = " %";
            // 
            // flowLayoutPanelViewAlong
            // 
            resources.ApplyResources(flowLayoutPanelViewAlong, "flowLayoutPanelViewAlong");
            flowLayoutPanelViewAlong.Controls.Add(buttonViewFromZ);
            flowLayoutPanelViewAlong.Controls.Add(buttonViewFromX);
            flowLayoutPanelViewAlong.Controls.Add(buttonSurfaceNormal);
            flowLayoutPanelViewAlong.Name = "flowLayoutPanelViewAlong";
            // 
            // flowLayoutPanelProfiles
            // 
            resources.ApplyResources(flowLayoutPanelProfiles, "flowLayoutPanelProfiles");
            captureExtender.SetCapture(flowLayoutPanelProfiles, true);
            flowLayoutPanelProfiles.Controls.Add(groupBoxEnergyDistribution);
            flowLayoutPanelProfiles.Controls.Add(groupBoxSurfaceDistance);
            flowLayoutPanelProfiles.Controls.Add(groupBoxPenetrationDepth);
            flowLayoutPanelProfiles.Name = "flowLayoutPanelProfiles";
            // 
            // groupBoxDirectionDistribution
            // 
            captureExtender.SetCapture(groupBoxDirectionDistribution, true);
            groupBoxDirectionDistribution.Controls.Add(poleFigureControl);
            groupBoxDirectionDistribution.Controls.Add(flowLayoutPanel5);
            resources.ApplyResources(groupBoxDirectionDistribution, "groupBoxDirectionDistribution");
            groupBoxDirectionDistribution.Name = "groupBoxDirectionDistribution";
            groupBoxDirectionDistribution.TabStop = false;
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(radioButtonFrequency);
            flowLayoutPanel5.Controls.Add(radioButtonAverageEnergy);
            flowLayoutPanel5.Controls.Add(radioButtonStandardDeviation);
            flowLayoutPanel5.Controls.Add(checkBoxDrawAxesInStereonet);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            // 
            // groupBoxStatistics
            // 
            resources.ApplyResources(groupBoxStatistics, "groupBoxStatistics");
            captureExtender.SetCapture(groupBoxStatistics, true);
            groupBoxStatistics.Controls.Add(flowLayoutPanel4);
            groupBoxStatistics.Controls.Add(flowLayoutPanel9);
            groupBoxStatistics.Controls.Add(flowLayoutPanel8);
            groupBoxStatistics.Controls.Add(flowLayoutPanel7);
            groupBoxStatistics.Controls.Add(flowLayoutPanel6);
            groupBoxStatistics.Name = "groupBoxStatistics";
            groupBoxStatistics.TabStop = false;
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(label3);
            flowLayoutPanel4.Controls.Add(labelBSEenergy);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // flowLayoutPanel9
            // 
            resources.ApplyResources(flowLayoutPanel9, "flowLayoutPanel9");
            flowLayoutPanel9.Controls.Add(label2);
            flowLayoutPanel9.Controls.Add(labelBSEratio);
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            // 
            // flowLayoutPanel8
            // 
            resources.ApplyResources(flowLayoutPanel8, "flowLayoutPanel8");
            flowLayoutPanel8.Controls.Add(label5);
            flowLayoutPanel8.Controls.Add(labelStoppingPower);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            // 
            // flowLayoutPanel7
            // 
            resources.ApplyResources(flowLayoutPanel7, "flowLayoutPanel7");
            flowLayoutPanel7.Controls.Add(label4);
            flowLayoutPanel7.Controls.Add(labelMeanFreePath);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            // 
            // flowLayoutPanel6
            // 
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Controls.Add(label6);
            flowLayoutPanel6.Controls.Add(labelCrossSection);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(buttonSimulate);
            flowLayoutPanel1.Controls.Add(numericBoxCalcNum);
            flowLayoutPanel1.Controls.Add(numericBoxSampleTilt);
            flowLayoutPanel1.Controls.Add(waveLengthControl);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // panel1
            // 
            panel1.Controls.Add(paneltTrajectory);
            panel1.Controls.Add(flowLayoutPanel3);
            panel1.Controls.Add(flowLayoutPanel1);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(flowLayoutPanelViewAlong);
            flowLayoutPanel3.Controls.Add(numericBoxDrawNum);
            flowLayoutPanel3.Controls.Add(checkBoxDrawAxes);
            flowLayoutPanel3.Controls.Add(checkBoxDrawGuidCircles);
            flowLayoutPanel3.Controls.Add(checkBoxDrawAbsorved);
            flowLayoutPanel3.Controls.Add(checkBoxDrawPathAfterEscape);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // panel2
            // 
            panel2.Controls.Add(groupBoxDirectionDistribution);
            panel2.Controls.Add(groupBoxStatistics);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // FormTrajectory
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanelProfiles);
            Controls.Add(statusStrip1);
            Name = "FormTrajectory";
            FormClosing += FormEBSD_FormClosing;
            Load += FormEBSD_Load;
            groupBoxEnergyDistribution.ResumeLayout(false);
            groupBoxPenetrationDepth.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBoxSurfaceDistance.ResumeLayout(false);
            flowLayoutPanelViewAlong.ResumeLayout(false);
            flowLayoutPanelViewAlong.PerformLayout();
            flowLayoutPanelProfiles.ResumeLayout(false);
            groupBoxDirectionDistribution.ResumeLayout(false);
            groupBoxDirectionDistribution.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            groupBoxStatistics.ResumeLayout(false);
            groupBoxStatistics.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel9.ResumeLayout(false);
            flowLayoutPanel9.PerformLayout();
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WaveLengthControl waveLengthControl;
        private System.Windows.Forms.ToolTip toolTip; // 260531Cl
        private System.Windows.Forms.Button buttonSimulate;
        private NumericBox numericBoxSampleTilt;
        private System.Windows.Forms.Button buttonViewFromX;
        private System.Windows.Forms.Button buttonViewFromZ;
        private System.Windows.Forms.Panel paneltTrajectory;
        private GraphControl graphControlEnergyProfile;
        private NumericBox numericBoxCalcNum;
        private NumericBox numericBoxDrawNum;
        private System.Windows.Forms.GroupBox groupBoxEnergyDistribution;
        private System.Windows.Forms.GroupBox groupBoxPenetrationDepth;
        private GraphControl graphControlDepthProfile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBoxSurfaceDistance;
        private GraphControl graphControlDistance;
        private PoleFigureControl2 poleFigureControl;
        private System.Windows.Forms.CheckBox checkBoxDrawAxes;
        private System.Windows.Forms.CheckBox checkBoxDrawGuidCircles;
        private System.Windows.Forms.Button buttonSurfaceNormal;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelViewAlong;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProfiles;
        private System.Windows.Forms.CheckBox checkBoxDrawAbsorved;
        private System.Windows.Forms.GroupBox groupBoxDirectionDistribution;
        private System.Windows.Forms.CheckBox checkBoxDrawAxesInStereonet;
        private System.Windows.Forms.GroupBox groupBoxStatistics;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelBSEratio;
        private System.Windows.Forms.Label labelBSEenergy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxDrawPathAfterEscape;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelMeanFreePath;
        private System.Windows.Forms.Label labelStoppingPower;
        private System.Windows.Forms.Label labelCrossSection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButtonStandardDeviation;
        private System.Windows.Forms.RadioButton radioButtonAverageEnergy;
        private System.Windows.Forms.RadioButton radioButtonFrequency;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Panel panel2;
    }
}

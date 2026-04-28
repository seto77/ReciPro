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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTrajectory));
            waveLengthControl = new WaveLengthControl();
            buttonCalc = new System.Windows.Forms.Button();
            numericBoxSampleTilt = new NumericBox();
            buttonViewFromX = new System.Windows.Forms.Button();
            buttonViewFromZ = new System.Windows.Forms.Button();
            paneltTrajectory = new System.Windows.Forms.Panel();
            graphControlEnergyProfile = new GraphControl();
            numericBoxCalcNum = new NumericBox();
            numericBoxDrawNum = new NumericBox();
            groupBoxEnergyDistribution = new System.Windows.Forms.GroupBox();
            groupBoxPenetrationDepth = new System.Windows.Forms.GroupBox();
            graphControlDepthProfile = new GraphControl();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            groupBoxSurfaceDistance = new System.Windows.Forms.GroupBox();
            graphControlDistance = new GraphControl();
            poleFigureControl = new PoleFigureControl2();
            checkBoxDrawAxes = new System.Windows.Forms.CheckBox();
            checkBoxDrawGuidCircles = new System.Windows.Forms.CheckBox();
            buttonSurfaceNormal = new System.Windows.Forms.Button();
            flowLayoutPanelViewAlong = new System.Windows.Forms.FlowLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            flowLayoutPanelProfiles = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxDrawAbsorved = new System.Windows.Forms.CheckBox();
            groupBoxDirectionDistribution = new System.Windows.Forms.GroupBox();
            radioButtonStandardDeviation = new System.Windows.Forms.RadioButton();
            radioButtonAverageEnergy = new System.Windows.Forms.RadioButton();
            radioButtonFrequency = new System.Windows.Forms.RadioButton();
            checkBoxDrawAxesInStereonet = new System.Windows.Forms.CheckBox();
            groupBoxStatistics = new System.Windows.Forms.GroupBox();
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
            panelCalculationConditions = new System.Windows.Forms.Panel();
            panelDrawingOptions = new System.Windows.Forms.Panel();
            groupBoxEnergyDistribution.SuspendLayout();
            groupBoxPenetrationDepth.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBoxSurfaceDistance.SuspendLayout();
            flowLayoutPanelViewAlong.SuspendLayout();
            flowLayoutPanelProfiles.SuspendLayout();
            groupBoxDirectionDistribution.SuspendLayout();
            groupBoxStatistics.SuspendLayout();
            panelCalculationConditions.SuspendLayout();
            panelDrawingOptions.SuspendLayout();
            SuspendLayout();
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            waveLengthControl.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl.Energy = 20D;
            waveLengthControl.Monochrome = true;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = false;
            waveLengthControl.WaveLength = 0.0085885141045000009D;
            waveLengthControl.WaveSource = WaveSource.Electron;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            waveLengthControl.XrayWaveSourceLine = XrayLine.Ka1;
            // 
            // buttonCalc
            // 
            resources.ApplyResources(buttonCalc, "buttonCalc");
            buttonCalc.BackColor = System.Drawing.Color.SteelBlue;
            buttonCalc.ForeColor = System.Drawing.Color.White;
            buttonCalc.Name = "buttonCalc";
            buttonCalc.UseVisualStyleBackColor = false;
            buttonCalc.Click += buttonCaluculate_Click;
            // 
            // numericBoxSampleTilt
            // 
            resources.ApplyResources(numericBoxSampleTilt, "numericBoxSampleTilt");
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            numericBoxSampleTilt.Maximum = 0D;
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.ShowUpDown = true;
            numericBoxSampleTilt.UpDown_Increment = 10D;
            // 
            // buttonViewFromX
            // 
            resources.ApplyResources(buttonViewFromX, "buttonViewFromX");
            buttonViewFromX.Name = "buttonViewFromX";
            buttonViewFromX.UseVisualStyleBackColor = true;
            buttonViewFromX.Click += buttonViewFromX_Click;
            // 
            // buttonViewFromZ
            // 
            resources.ApplyResources(buttonViewFromZ, "buttonViewFromZ");
            buttonViewFromZ.Name = "buttonViewFromZ";
            buttonViewFromZ.UseVisualStyleBackColor = true;
            buttonViewFromZ.Click += buttonViewFromZ_Click;
            // 
            // paneltTrajectory
            // 
            resources.ApplyResources(paneltTrajectory, "paneltTrajectory");
            paneltTrajectory.Name = "paneltTrajectory";
            // 
            // graphControlEnergyProfile
            // 
            resources.ApplyResources(graphControlEnergyProfile, "graphControlEnergyProfile");
            graphControlEnergyProfile.AllowMouseOperation = true;
            graphControlEnergyProfile.AxisLineColor = System.Drawing.Color.Gray;
            graphControlEnergyProfile.AxisTextColor = System.Drawing.Color.Black;
            graphControlEnergyProfile.AxisTextFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlEnergyProfile.AxisXTextVisible = true;
            graphControlEnergyProfile.AxisYTextVisible = true;
            graphControlEnergyProfile.BackgroundColor = System.Drawing.Color.White;
            graphControlEnergyProfile.BottomMargin = 0D;
            graphControlEnergyProfile.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlEnergyProfile.DivisionLineXVisible = true;
            graphControlEnergyProfile.DivisionLineYVisible = true;
            graphControlEnergyProfile.FixRangeHorizontal = false;
            graphControlEnergyProfile.FixRangeVertical = false;
            graphControlEnergyProfile.GraphTitle = "";
            graphControlEnergyProfile.IsIntegerX = false;
            graphControlEnergyProfile.IsIntegerY = false;
            graphControlEnergyProfile.LabelX = "kev: ";
            graphControlEnergyProfile.LabelY = "freq.: ";
            graphControlEnergyProfile.LeftMargin = 0F;
            graphControlEnergyProfile.LineWidth = 1F;
            graphControlEnergyProfile.LowerX = 0D;
            graphControlEnergyProfile.LowerY = 0D;
            graphControlEnergyProfile.MaximalX = 1D;
            graphControlEnergyProfile.MaximalY = 1D;
            graphControlEnergyProfile.MinimalX = 0D;
            graphControlEnergyProfile.MinimalY = 0D;
            graphControlEnergyProfile.Mode = GraphControl.DrawingMode.Histogram;
            graphControlEnergyProfile.MousePositionVisible = true;
            graphControlEnergyProfile.MousePositionXDigit = 3;
            graphControlEnergyProfile.MousePositionYDigit = 3;
            graphControlEnergyProfile.Name = "graphControlEnergyProfile";
            graphControlEnergyProfile.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlEnergyProfile.UnitX = "";
            graphControlEnergyProfile.UnitY = "";
            graphControlEnergyProfile.UpperPanelFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlEnergyProfile.UpperPanelVisible = true;
            graphControlEnergyProfile.UpperX = 1D;
            graphControlEnergyProfile.UpperY = 1D;
            graphControlEnergyProfile.UseLineWidth = true;
            graphControlEnergyProfile.VerticalLineColor = System.Drawing.Color.Red;
            graphControlEnergyProfile.XLog = false;
            graphControlEnergyProfile.YLog = false;
            // 
            // numericBoxCalcNum
            // 
            resources.ApplyResources(numericBoxCalcNum, "numericBoxCalcNum");
            numericBoxCalcNum.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalcNum.DecimalPlaces = 0;
            numericBoxCalcNum.Maximum = 1000000D;
            numericBoxCalcNum.Minimum = 100D;
            numericBoxCalcNum.Name = "numericBoxCalcNum";
            numericBoxCalcNum.RadianValue = 1745.3292519943295D;
            numericBoxCalcNum.ShowUpDown = true;
            numericBoxCalcNum.SmartIncrement = true;
            numericBoxCalcNum.Value = 100000D;
            // 
            // numericBoxDrawNum
            // 
            resources.ApplyResources(numericBoxDrawNum, "numericBoxDrawNum");
            numericBoxDrawNum.BackColor = System.Drawing.Color.Transparent;
            numericBoxDrawNum.DecimalPlaces = 0;
            numericBoxDrawNum.Maximum = 100000D;
            numericBoxDrawNum.Minimum = 1D;
            numericBoxDrawNum.Name = "numericBoxDrawNum";
            numericBoxDrawNum.RadianValue = 8.7266462599716466D;
            numericBoxDrawNum.ShowUpDown = true;
            numericBoxDrawNum.SmartIncrement = true;
            numericBoxDrawNum.Value = 500D;
            numericBoxDrawNum.ValueChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // groupBoxEnergyDistribution
            // 
            resources.ApplyResources(groupBoxEnergyDistribution, "groupBoxEnergyDistribution");
            captureExtender.SetCapture(groupBoxEnergyDistribution, true);
            groupBoxEnergyDistribution.Controls.Add(graphControlEnergyProfile);
            groupBoxEnergyDistribution.Name = "groupBoxEnergyDistribution";
            groupBoxEnergyDistribution.TabStop = false;
            // 
            // groupBoxPenetrationDepth
            // 
            resources.ApplyResources(groupBoxPenetrationDepth, "groupBoxPenetrationDepth");
            groupBoxPenetrationDepth.Controls.Add(graphControlDepthProfile);
            groupBoxPenetrationDepth.Name = "groupBoxPenetrationDepth";
            groupBoxPenetrationDepth.TabStop = false;
            // 
            // graphControlDepthProfile
            // 
            resources.ApplyResources(graphControlDepthProfile, "graphControlDepthProfile");
            graphControlDepthProfile.AllowMouseOperation = true;
            graphControlDepthProfile.AxisLineColor = System.Drawing.Color.Gray;
            graphControlDepthProfile.AxisTextColor = System.Drawing.Color.Black;
            graphControlDepthProfile.AxisTextFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlDepthProfile.AxisXTextVisible = true;
            graphControlDepthProfile.AxisYTextVisible = true;
            graphControlDepthProfile.BackgroundColor = System.Drawing.Color.White;
            graphControlDepthProfile.BottomMargin = 0D;
            graphControlDepthProfile.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlDepthProfile.DivisionLineXVisible = true;
            graphControlDepthProfile.DivisionLineYVisible = true;
            graphControlDepthProfile.FixRangeHorizontal = false;
            graphControlDepthProfile.FixRangeVertical = false;
            graphControlDepthProfile.GraphTitle = "";
            graphControlDepthProfile.IsIntegerX = false;
            graphControlDepthProfile.IsIntegerY = false;
            graphControlDepthProfile.LabelX = "µm: ";
            graphControlDepthProfile.LabelY = "freq.:";
            graphControlDepthProfile.LeftMargin = 0F;
            graphControlDepthProfile.LineWidth = 1F;
            graphControlDepthProfile.LowerX = 0D;
            graphControlDepthProfile.LowerY = 0D;
            graphControlDepthProfile.MaximalX = 1D;
            graphControlDepthProfile.MaximalY = 1D;
            graphControlDepthProfile.MinimalX = 0D;
            graphControlDepthProfile.MinimalY = 0D;
            graphControlDepthProfile.Mode = GraphControl.DrawingMode.Histogram;
            graphControlDepthProfile.MousePositionVisible = true;
            graphControlDepthProfile.MousePositionXDigit = 3;
            graphControlDepthProfile.MousePositionYDigit = 3;
            graphControlDepthProfile.Name = "graphControlDepthProfile";
            graphControlDepthProfile.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlDepthProfile.UnitX = "µm";
            graphControlDepthProfile.UnitY = "%";
            graphControlDepthProfile.UpperPanelFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlDepthProfile.UpperPanelVisible = true;
            graphControlDepthProfile.UpperX = 1D;
            graphControlDepthProfile.UpperY = 1D;
            graphControlDepthProfile.UseLineWidth = true;
            graphControlDepthProfile.VerticalLineColor = System.Drawing.Color.Red;
            graphControlDepthProfile.XLog = false;
            graphControlDepthProfile.YLog = false;
            // 
            // statusStrip1
            // 
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // groupBoxSurfaceDistance
            // 
            resources.ApplyResources(groupBoxSurfaceDistance, "groupBoxSurfaceDistance");
            groupBoxSurfaceDistance.Controls.Add(graphControlDistance);
            groupBoxSurfaceDistance.Name = "groupBoxSurfaceDistance";
            groupBoxSurfaceDistance.TabStop = false;
            // 
            // graphControlDistance
            // 
            resources.ApplyResources(graphControlDistance, "graphControlDistance");
            graphControlDistance.AllowMouseOperation = true;
            graphControlDistance.AxisLineColor = System.Drawing.Color.Gray;
            graphControlDistance.AxisTextColor = System.Drawing.Color.Black;
            graphControlDistance.AxisTextFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlDistance.AxisXTextVisible = true;
            graphControlDistance.AxisYTextVisible = true;
            graphControlDistance.BackgroundColor = System.Drawing.Color.White;
            graphControlDistance.BottomMargin = 0D;
            graphControlDistance.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlDistance.DivisionLineXVisible = true;
            graphControlDistance.DivisionLineYVisible = true;
            graphControlDistance.FixRangeHorizontal = false;
            graphControlDistance.FixRangeVertical = false;
            graphControlDistance.GraphTitle = "";
            graphControlDistance.IsIntegerX = false;
            graphControlDistance.IsIntegerY = false;
            graphControlDistance.LabelX = "µm: ";
            graphControlDistance.LabelY = "freq.:";
            graphControlDistance.LeftMargin = 0F;
            graphControlDistance.LineWidth = 1F;
            graphControlDistance.LowerX = 0D;
            graphControlDistance.LowerY = 0D;
            graphControlDistance.MaximalX = 1D;
            graphControlDistance.MaximalY = 1D;
            graphControlDistance.MinimalX = 0D;
            graphControlDistance.MinimalY = 0D;
            graphControlDistance.Mode = GraphControl.DrawingMode.Histogram;
            graphControlDistance.MousePositionVisible = true;
            graphControlDistance.MousePositionXDigit = 3;
            graphControlDistance.MousePositionYDigit = 3;
            graphControlDistance.Name = "graphControlDistance";
            graphControlDistance.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlDistance.UnitX = "nm";
            graphControlDistance.UnitY = "%";
            graphControlDistance.UpperPanelFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlDistance.UpperPanelVisible = true;
            graphControlDistance.UpperX = 1D;
            graphControlDistance.UpperY = 1D;
            graphControlDistance.UseLineWidth = true;
            graphControlDistance.VerticalLineColor = System.Drawing.Color.Red;
            graphControlDistance.XLog = false;
            graphControlDistance.YLog = false;
            // 
            // poleFigureControl
            // 
            resources.ApplyResources(poleFigureControl, "poleFigureControl");
            poleFigureControl.Name = "poleFigureControl";
            // 
            // checkBoxDrawAxes
            // 
            resources.ApplyResources(checkBoxDrawAxes, "checkBoxDrawAxes");
            checkBoxDrawAxes.Checked = true;
            checkBoxDrawAxes.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxes.Name = "checkBoxDrawAxes";
            checkBoxDrawAxes.UseVisualStyleBackColor = true;
            checkBoxDrawAxes.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // checkBoxDrawGuidCircles
            // 
            resources.ApplyResources(checkBoxDrawGuidCircles, "checkBoxDrawGuidCircles");
            checkBoxDrawGuidCircles.Checked = true;
            checkBoxDrawGuidCircles.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawGuidCircles.Name = "checkBoxDrawGuidCircles";
            checkBoxDrawGuidCircles.UseVisualStyleBackColor = true;
            checkBoxDrawGuidCircles.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // buttonSurfaceNormal
            // 
            resources.ApplyResources(buttonSurfaceNormal, "buttonSurfaceNormal");
            buttonSurfaceNormal.Name = "buttonSurfaceNormal";
            buttonSurfaceNormal.UseVisualStyleBackColor = true;
            buttonSurfaceNormal.Click += buttonViewFromSurfaceNormal_Click;
            // 
            // flowLayoutPanelViewAlong
            // 
            resources.ApplyResources(flowLayoutPanelViewAlong, "flowLayoutPanelViewAlong");
            flowLayoutPanelViewAlong.Controls.Add(buttonViewFromZ);
            flowLayoutPanelViewAlong.Controls.Add(buttonViewFromX);
            flowLayoutPanelViewAlong.Controls.Add(buttonSurfaceNormal);
            flowLayoutPanelViewAlong.Name = "flowLayoutPanelViewAlong";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
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
            // checkBoxDrawAbsorved
            // 
            resources.ApplyResources(checkBoxDrawAbsorved, "checkBoxDrawAbsorved");
            checkBoxDrawAbsorved.Name = "checkBoxDrawAbsorved";
            checkBoxDrawAbsorved.UseVisualStyleBackColor = true;
            checkBoxDrawAbsorved.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // groupBoxDirectionDistribution
            // 
            resources.ApplyResources(groupBoxDirectionDistribution, "groupBoxDirectionDistribution");
            captureExtender.SetCapture(groupBoxDirectionDistribution, true);
            groupBoxDirectionDistribution.Controls.Add(radioButtonStandardDeviation);
            groupBoxDirectionDistribution.Controls.Add(radioButtonAverageEnergy);
            groupBoxDirectionDistribution.Controls.Add(radioButtonFrequency);
            groupBoxDirectionDistribution.Controls.Add(poleFigureControl);
            groupBoxDirectionDistribution.Controls.Add(checkBoxDrawAxesInStereonet);
            groupBoxDirectionDistribution.Name = "groupBoxDirectionDistribution";
            groupBoxDirectionDistribution.TabStop = false;
            // 
            // radioButtonStandardDeviation
            // 
            resources.ApplyResources(radioButtonStandardDeviation, "radioButtonStandardDeviation");
            radioButtonStandardDeviation.Name = "radioButtonStandardDeviation";
            radioButtonStandardDeviation.TabStop = true;
            radioButtonStandardDeviation.UseVisualStyleBackColor = true;
            radioButtonStandardDeviation.CheckedChanged += radioButtonFrequency_CheckedChanged;
            // 
            // radioButtonAverageEnergy
            // 
            resources.ApplyResources(radioButtonAverageEnergy, "radioButtonAverageEnergy");
            radioButtonAverageEnergy.Name = "radioButtonAverageEnergy";
            radioButtonAverageEnergy.TabStop = true;
            radioButtonAverageEnergy.UseVisualStyleBackColor = true;
            radioButtonAverageEnergy.CheckedChanged += radioButtonFrequency_CheckedChanged;
            // 
            // radioButtonFrequency
            // 
            resources.ApplyResources(radioButtonFrequency, "radioButtonFrequency");
            radioButtonFrequency.Checked = true;
            radioButtonFrequency.Name = "radioButtonFrequency";
            radioButtonFrequency.TabStop = true;
            radioButtonFrequency.UseVisualStyleBackColor = true;
            radioButtonFrequency.CheckedChanged += radioButtonFrequency_CheckedChanged;
            // 
            // checkBoxDrawAxesInStereonet
            // 
            resources.ApplyResources(checkBoxDrawAxesInStereonet, "checkBoxDrawAxesInStereonet");
            checkBoxDrawAxesInStereonet.Checked = true;
            checkBoxDrawAxesInStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxesInStereonet.Name = "checkBoxDrawAxesInStereonet";
            checkBoxDrawAxesInStereonet.UseVisualStyleBackColor = true;
            checkBoxDrawAxesInStereonet.CheckedChanged += checkBoxDrawAxesInStereonet_CheckedChanged;
            // 
            // groupBoxStatistics
            // 
            resources.ApplyResources(groupBoxStatistics, "groupBoxStatistics");
            captureExtender.SetCapture(groupBoxStatistics, true);
            groupBoxStatistics.Controls.Add(labelBSEenergy);
            groupBoxStatistics.Controls.Add(labelStoppingPower);
            groupBoxStatistics.Controls.Add(labelCrossSection);
            groupBoxStatistics.Controls.Add(labelMeanFreePath);
            groupBoxStatistics.Controls.Add(labelBSEratio);
            groupBoxStatistics.Controls.Add(label3);
            groupBoxStatistics.Controls.Add(label6);
            groupBoxStatistics.Controls.Add(label5);
            groupBoxStatistics.Controls.Add(label4);
            groupBoxStatistics.Controls.Add(label2);
            groupBoxStatistics.Name = "groupBoxStatistics";
            groupBoxStatistics.TabStop = false;
            // 
            // labelBSEenergy
            // 
            resources.ApplyResources(labelBSEenergy, "labelBSEenergy");
            labelBSEenergy.Name = "labelBSEenergy";
            // 
            // labelStoppingPower
            // 
            resources.ApplyResources(labelStoppingPower, "labelStoppingPower");
            labelStoppingPower.Name = "labelStoppingPower";
            // 
            // labelCrossSection
            // 
            resources.ApplyResources(labelCrossSection, "labelCrossSection");
            labelCrossSection.Name = "labelCrossSection";
            // 
            // labelMeanFreePath
            // 
            resources.ApplyResources(labelMeanFreePath, "labelMeanFreePath");
            labelMeanFreePath.Name = "labelMeanFreePath";
            // 
            // labelBSEratio
            // 
            resources.ApplyResources(labelBSEratio, "labelBSEratio");
            labelBSEratio.Name = "labelBSEratio";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // checkBoxDrawPathAfterEscape
            // 
            resources.ApplyResources(checkBoxDrawPathAfterEscape, "checkBoxDrawPathAfterEscape");
            checkBoxDrawPathAfterEscape.Checked = true;
            checkBoxDrawPathAfterEscape.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawPathAfterEscape.Name = "checkBoxDrawPathAfterEscape";
            checkBoxDrawPathAfterEscape.UseVisualStyleBackColor = true;
            checkBoxDrawPathAfterEscape.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // panelCalculationConditions
            // 
            resources.ApplyResources(panelCalculationConditions, "panelCalculationConditions");
            captureExtender.SetCapture(panelCalculationConditions, true);
            panelCalculationConditions.Controls.Add(numericBoxCalcNum);
            panelCalculationConditions.Controls.Add(label1);
            panelCalculationConditions.Controls.Add(buttonCalc);
            panelCalculationConditions.Controls.Add(waveLengthControl);
            panelCalculationConditions.Controls.Add(numericBoxSampleTilt);
            panelCalculationConditions.Name = "panelCalculationConditions";
            // 
            // panelDrawingOptions
            // 
            resources.ApplyResources(panelDrawingOptions, "panelDrawingOptions");
            captureExtender.SetCapture(panelDrawingOptions, true);
            panelDrawingOptions.Controls.Add(checkBoxDrawAbsorved);
            panelDrawingOptions.Controls.Add(flowLayoutPanelViewAlong);
            panelDrawingOptions.Controls.Add(numericBoxDrawNum);
            panelDrawingOptions.Controls.Add(checkBoxDrawAxes);
            panelDrawingOptions.Controls.Add(checkBoxDrawGuidCircles);
            panelDrawingOptions.Controls.Add(checkBoxDrawPathAfterEscape);
            panelDrawingOptions.Name = "panelDrawingOptions";
            // 
            // FormTrajectory
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F); // 260329Cl 追加: Font→Dpi, 96dpi基準に統一
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(panelDrawingOptions);
            Controls.Add(panelCalculationConditions);
            Controls.Add(groupBoxStatistics);
            Controls.Add(groupBoxDirectionDistribution);
            Controls.Add(flowLayoutPanelProfiles);
            Controls.Add(statusStrip1);
            Controls.Add(paneltTrajectory);
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
            groupBoxStatistics.ResumeLayout(false);
            groupBoxStatistics.PerformLayout();
            panelCalculationConditions.ResumeLayout(false);
            panelCalculationConditions.PerformLayout();
            panelDrawingOptions.ResumeLayout(false);
            panelDrawingOptions.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WaveLengthControl waveLengthControl;
        private System.Windows.Forms.Button buttonCalc;
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
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Panel panelCalculationConditions;
        private System.Windows.Forms.Panel panelDrawingOptions;
    }
}

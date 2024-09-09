namespace ReciPro
{
    partial class FormEBSD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelGeometry = new System.Windows.Forms.Panel();
            numericBoxSampleTilt = new NumericBox();
            waveLengthControl1 = new WaveLengthControl();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonFromX = new System.Windows.Forms.Button();
            buttonViewFromZ = new System.Windows.Forms.Button();
            buttonViweFromSurfaceNormal = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            groupBox5 = new System.Windows.Forms.GroupBox();
            radioButtonStandardDeviation = new System.Windows.Forms.RadioButton();
            radioButtonAverageEnergy = new System.Windows.Forms.RadioButton();
            radioButtonFrequency = new System.Windows.Forms.RadioButton();
            poleFigureControl = new PoleFigureControl2();
            checkBoxDrawAxesInStereonet = new System.Windows.Forms.CheckBox();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            numericBoxDetectorTilt = new NumericBox();
            numericBoxDetRadius = new NumericBox();
            numericBoxZofDet = new NumericBox();
            numericBoxYofDet = new NumericBox();
            label2 = new System.Windows.Forms.Label();
            graphicsBox = new ImagingSolution.Control.GraphicsBox(components);
            flowLayoutPanel1.SuspendLayout();
            groupBox5.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            SuspendLayout();
            // 
            // panelGeometry
            // 
            panelGeometry.Location = new System.Drawing.Point(6, 127);
            panelGeometry.Name = "panelGeometry";
            panelGeometry.Size = new System.Drawing.Size(300, 300);
            panelGeometry.TabIndex = 0;
            // 
            // numericBoxSampleTilt
            // 
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            numericBoxSampleTilt.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxSampleTilt.FooterText = "°";
            numericBoxSampleTilt.HeaderText = "Sample tilt";
            numericBoxSampleTilt.Location = new System.Drawing.Point(3, 9);
            numericBoxSampleTilt.Margin = new System.Windows.Forms.Padding(0);
            numericBoxSampleTilt.Maximum = 90D;
            numericBoxSampleTilt.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.RadianValue = 1.2217304763960306D;
            numericBoxSampleTilt.RoundErrorAccuracy = -1;
            numericBoxSampleTilt.ShowUpDown = true;
            numericBoxSampleTilt.Size = new System.Drawing.Size(128, 26);
            numericBoxSampleTilt.TabIndex = 111;
            numericBoxSampleTilt.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxSampleTilt.UpDown_Increment = 10D;
            numericBoxSampleTilt.Value = 70D;
            numericBoxSampleTilt.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // waveLengthControl1
            // 
            waveLengthControl1.AutoSize = true;
            waveLengthControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            waveLengthControl1.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl1.Energy = 20D;
            waveLengthControl1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            waveLengthControl1.Location = new System.Drawing.Point(663, 9);
            waveLengthControl1.Margin = new System.Windows.Forms.Padding(0);
            waveLengthControl1.MaximumSize = new System.Drawing.Size(500, 500);
            waveLengthControl1.MinimumSize = new System.Drawing.Size(210, 0);
            waveLengthControl1.Monochrome = true;
            waveLengthControl1.Name = "waveLengthControl1";
            waveLengthControl1.ShowWaveSource = false;
            waveLengthControl1.Size = new System.Drawing.Size(210, 55);
            waveLengthControl1.TabIndex = 109;
            waveLengthControl1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            waveLengthControl1.WaveLength = 0.0085885141045000009D;
            waveLengthControl1.WaveSource = WaveSource.Electron;
            waveLengthControl1.XrayWaveSourceElementNumber = 0;
            waveLengthControl1.XrayWaveSourceLine = XrayLine.Ka1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(buttonFromX);
            flowLayoutPanel1.Controls.Add(buttonViewFromZ);
            flowLayoutPanel1.Controls.Add(buttonViweFromSurfaceNormal);
            flowLayoutPanel1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            flowLayoutPanel1.Location = new System.Drawing.Point(6, 430);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(300, 52);
            flowLayoutPanel1.TabIndex = 112;
            // 
            // buttonFromX
            // 
            buttonFromX.AutoSize = true;
            buttonFromX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonFromX.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonFromX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonFromX.Location = new System.Drawing.Point(0, 0);
            buttonFromX.Margin = new System.Windows.Forms.Padding(0);
            buttonFromX.Name = "buttonFromX";
            buttonFromX.Size = new System.Drawing.Size(130, 25);
            buttonFromX.TabIndex = 98;
            buttonFromX.Text = "From X (rotation axis)";
            buttonFromX.UseVisualStyleBackColor = true;
            buttonFromX.Click += buttonViewFromX_Click;
            // 
            // buttonViewFromZ
            // 
            buttonViewFromZ.AutoSize = true;
            buttonViewFromZ.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonViewFromZ.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonViewFromZ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonViewFromZ.Location = new System.Drawing.Point(130, 0);
            buttonViewFromZ.Margin = new System.Windows.Forms.Padding(0);
            buttonViewFromZ.Name = "buttonViewFromZ";
            buttonViewFromZ.Size = new System.Drawing.Size(154, 25);
            buttonViewFromZ.TabIndex = 99;
            buttonViewFromZ.Text = "From Z (=beam direction)";
            buttonViewFromZ.UseVisualStyleBackColor = true;
            buttonViewFromZ.Click += buttonViewFromZ_Click;
            // 
            // buttonViweFromSurfaceNormal
            // 
            buttonViweFromSurfaceNormal.AutoSize = true;
            buttonViweFromSurfaceNormal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonViweFromSurfaceNormal.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonViweFromSurfaceNormal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonViweFromSurfaceNormal.Location = new System.Drawing.Point(0, 25);
            buttonViweFromSurfaceNormal.Margin = new System.Windows.Forms.Padding(0);
            buttonViweFromSurfaceNormal.Name = "buttonViweFromSurfaceNormal";
            buttonViweFromSurfaceNormal.Size = new System.Drawing.Size(147, 25);
            buttonViweFromSurfaceNormal.TabIndex = 98;
            buttonViweFromSurfaceNormal.Text = "From the surface normal";
            buttonViweFromSurfaceNormal.UseVisualStyleBackColor = true;
            buttonViweFromSurfaceNormal.Click += buttonFromSurfaceNormal_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(312, 527);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 113;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(radioButtonStandardDeviation);
            groupBox5.Controls.Add(radioButtonAverageEnergy);
            groupBox5.Controls.Add(radioButtonFrequency);
            groupBox5.Controls.Add(poleFigureControl);
            groupBox5.Controls.Add(checkBoxDrawAxesInStereonet);
            groupBox5.Location = new System.Drawing.Point(312, 9);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(333, 512);
            groupBox5.TabIndex = 114;
            groupBox5.TabStop = false;
            groupBox5.Text = "Direction distribution of BSEs (the center of stereonet corresponds to the surface normal direction)";
            // 
            // radioButtonStandardDeviation
            // 
            radioButtonStandardDeviation.AutoSize = true;
            radioButtonStandardDeviation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonStandardDeviation.Location = new System.Drawing.Point(6, 60);
            radioButtonStandardDeviation.Name = "radioButtonStandardDeviation";
            radioButtonStandardDeviation.Size = new System.Drawing.Size(177, 19);
            radioButtonStandardDeviation.TabIndex = 106;
            radioButtonStandardDeviation.TabStop = true;
            radioButtonStandardDeviation.Text = "Standard deviation of energy";
            radioButtonStandardDeviation.UseVisualStyleBackColor = true;
            // 
            // radioButtonAverageEnergy
            // 
            radioButtonAverageEnergy.AutoSize = true;
            radioButtonAverageEnergy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonAverageEnergy.Location = new System.Drawing.Point(103, 39);
            radioButtonAverageEnergy.Name = "radioButtonAverageEnergy";
            radioButtonAverageEnergy.Size = new System.Drawing.Size(107, 19);
            radioButtonAverageEnergy.TabIndex = 106;
            radioButtonAverageEnergy.TabStop = true;
            radioButtonAverageEnergy.Text = "Average energy";
            radioButtonAverageEnergy.UseVisualStyleBackColor = true;
            // 
            // radioButtonFrequency
            // 
            radioButtonFrequency.AutoSize = true;
            radioButtonFrequency.Checked = true;
            radioButtonFrequency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonFrequency.Location = new System.Drawing.Point(6, 39);
            radioButtonFrequency.Name = "radioButtonFrequency";
            radioButtonFrequency.Size = new System.Drawing.Size(80, 19);
            radioButtonFrequency.TabIndex = 106;
            radioButtonFrequency.TabStop = true;
            radioButtonFrequency.Text = "Frequency";
            radioButtonFrequency.UseVisualStyleBackColor = true;
            // 
            // poleFigureControl
            // 
            poleFigureControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            poleFigureControl.Location = new System.Drawing.Point(6, 87);
            poleFigureControl.Margin = new System.Windows.Forms.Padding(4);
            poleFigureControl.Name = "poleFigureControl";
            poleFigureControl.Size = new System.Drawing.Size(322, 416);
            poleFigureControl.TabIndex = 104;
            poleFigureControl.Vectors = null;
            // 
            // checkBoxDrawAxesInStereonet
            // 
            checkBoxDrawAxesInStereonet.AutoSize = true;
            checkBoxDrawAxesInStereonet.Checked = true;
            checkBoxDrawAxesInStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxesInStereonet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxDrawAxesInStereonet.Location = new System.Drawing.Point(270, 43);
            checkBoxDrawAxesInStereonet.Name = "checkBoxDrawAxesInStereonet";
            checkBoxDrawAxesInStereonet.Size = new System.Drawing.Size(53, 34);
            checkBoxDrawAxesInStereonet.TabIndex = 105;
            checkBoxDrawAxesInStereonet.Text = "Draw\r\n axes";
            checkBoxDrawAxesInStereonet.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 557);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(1382, 22);
            statusStrip1.TabIndex = 115;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // numericBoxDetectorTilt
            // 
            numericBoxDetectorTilt.BackColor = System.Drawing.Color.Transparent;
            numericBoxDetectorTilt.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDetectorTilt.FooterText = "°";
            numericBoxDetectorTilt.HeaderText = "Detector tilt";
            numericBoxDetectorTilt.Location = new System.Drawing.Point(142, 9);
            numericBoxDetectorTilt.Margin = new System.Windows.Forms.Padding(0);
            numericBoxDetectorTilt.Maximum = 180D;
            numericBoxDetectorTilt.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxDetectorTilt.Minimum = 0D;
            numericBoxDetectorTilt.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxDetectorTilt.Name = "numericBoxDetectorTilt";
            numericBoxDetectorTilt.RadianValue = 1.5707963267948966D;
            numericBoxDetectorTilt.RoundErrorAccuracy = -1;
            numericBoxDetectorTilt.ShowUpDown = true;
            numericBoxDetectorTilt.Size = new System.Drawing.Size(137, 26);
            numericBoxDetectorTilt.TabIndex = 111;
            numericBoxDetectorTilt.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDetectorTilt.UpDown_Increment = 10D;
            numericBoxDetectorTilt.Value = 90D;
            numericBoxDetectorTilt.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // numericBoxDetRadius
            // 
            numericBoxDetRadius.BackColor = System.Drawing.Color.Transparent;
            numericBoxDetRadius.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDetRadius.FooterText = "mm";
            numericBoxDetRadius.HeaderText = "Detector radius";
            numericBoxDetRadius.Location = new System.Drawing.Point(6, 40);
            numericBoxDetRadius.Margin = new System.Windows.Forms.Padding(0);
            numericBoxDetRadius.Maximum = 180D;
            numericBoxDetRadius.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxDetRadius.Minimum = 0D;
            numericBoxDetRadius.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxDetRadius.Name = "numericBoxDetRadius";
            numericBoxDetRadius.RadianValue = 0.52359877559829882D;
            numericBoxDetRadius.RoundErrorAccuracy = -1;
            numericBoxDetRadius.ShowUpDown = true;
            numericBoxDetRadius.Size = new System.Drawing.Size(175, 26);
            numericBoxDetRadius.TabIndex = 111;
            numericBoxDetRadius.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDetRadius.UpDown_Increment = 10D;
            numericBoxDetRadius.Value = 30D;
            numericBoxDetRadius.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // numericBoxZofDet
            // 
            numericBoxZofDet.BackColor = System.Drawing.Color.Transparent;
            numericBoxZofDet.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxZofDet.FooterText = "mm";
            numericBoxZofDet.HeaderText = "Z";
            numericBoxZofDet.Location = new System.Drawing.Point(107, 98);
            numericBoxZofDet.Margin = new System.Windows.Forms.Padding(0);
            numericBoxZofDet.Maximum = 1000D;
            numericBoxZofDet.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxZofDet.Minimum = -1000D;
            numericBoxZofDet.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxZofDet.Name = "numericBoxZofDet";
            numericBoxZofDet.RadianValue = -0.3490658503988659D;
            numericBoxZofDet.RoundErrorAccuracy = -1;
            numericBoxZofDet.ShowUpDown = true;
            numericBoxZofDet.Size = new System.Drawing.Size(102, 25);
            numericBoxZofDet.TabIndex = 111;
            numericBoxZofDet.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxZofDet.UpDown_Increment = 10D;
            numericBoxZofDet.Value = -20D;
            numericBoxZofDet.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // numericBoxYofDet
            // 
            numericBoxYofDet.BackColor = System.Drawing.Color.Transparent;
            numericBoxYofDet.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxYofDet.FooterText = "mm";
            numericBoxYofDet.HeaderText = "Y";
            numericBoxYofDet.Location = new System.Drawing.Point(107, 71);
            numericBoxYofDet.Margin = new System.Windows.Forms.Padding(0);
            numericBoxYofDet.Maximum = 1000D;
            numericBoxYofDet.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxYofDet.Minimum = -1000D;
            numericBoxYofDet.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxYofDet.Name = "numericBoxYofDet";
            numericBoxYofDet.RadianValue = -0.52359877559829882D;
            numericBoxYofDet.RoundErrorAccuracy = -1;
            numericBoxYofDet.ShowUpDown = true;
            numericBoxYofDet.Size = new System.Drawing.Size(102, 25);
            numericBoxYofDet.TabIndex = 111;
            numericBoxYofDet.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxYofDet.UpDown_Increment = 10D;
            numericBoxYofDet.Value = -30D;
            numericBoxYofDet.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label2.Location = new System.Drawing.Point(3, 76);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(101, 34);
            label2.TabIndex = 116;
            label2.Text = "Coordinates of\r\n detector center";
            // 
            // graphicsBox
            // 
            graphicsBox.BackColor = System.Drawing.Color.Transparent;
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            graphicsBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            graphicsBox.Location = new System.Drawing.Point(663, 69);
            graphicsBox.Margin = new System.Windows.Forms.Padding(0, 40, 0, 0);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.Size = new System.Drawing.Size(480, 480);
            graphicsBox.TabIndex = 117;
            graphicsBox.TabStop = false;
            graphicsBox.WaitOnLoad = true;
            // 
            // FormEBSD
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1382, 579);
            Controls.Add(graphicsBox);
            Controls.Add(label2);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox5);
            Controls.Add(button2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(numericBoxYofDet);
            Controls.Add(numericBoxZofDet);
            Controls.Add(numericBoxDetRadius);
            Controls.Add(numericBoxDetectorTilt);
            Controls.Add(numericBoxSampleTilt);
            Controls.Add(waveLengthControl1);
            Controls.Add(panelGeometry);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Name = "FormEBSD";
            Text = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelGeometry;
        private NumericBox numericBoxSampleTilt;
        private WaveLengthControl waveLengthControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonViewFromZ;
        private System.Windows.Forms.Button buttonFromX;
        private System.Windows.Forms.Button buttonViweFromSurfaceNormal;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButtonStandardDeviation;
        private System.Windows.Forms.RadioButton radioButtonAverageEnergy;
        private System.Windows.Forms.RadioButton radioButtonFrequency;
        private PoleFigureControl2 poleFigureControl;
        private System.Windows.Forms.CheckBox checkBoxDrawAxesInStereonet;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private NumericBox numericBoxDetectorTilt;
        private NumericBox numericBoxDetRadius;
        private NumericBox numericBoxZofDet;
        private NumericBox numericBoxYofDet;
        private System.Windows.Forms.Label label2;
        public ImagingSolution.Control.GraphicsBox graphicsBox;
    }
}
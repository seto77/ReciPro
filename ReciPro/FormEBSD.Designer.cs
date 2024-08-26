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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEBSD));
            waveLengthControl1 = new WaveLengthControl();
            panelGeometry = new System.Windows.Forms.Panel();
            button1 = new System.Windows.Forms.Button();
            numericBoxSampleTilt = new NumericBox();
            buttonViewIsometric = new System.Windows.Forms.Button();
            buttonViewAlongBeam = new System.Windows.Forms.Button();
            paneltTrajectory = new System.Windows.Forms.Panel();
            SuspendLayout();
            // 
            // waveLengthControl1
            // 
            resources.ApplyResources(waveLengthControl1, "waveLengthControl1");
            waveLengthControl1.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl1.Energy = 20D;
            waveLengthControl1.Monochrome = true;
            waveLengthControl1.Name = "waveLengthControl1";
            waveLengthControl1.ShowWaveSource = false;
            waveLengthControl1.WaveLength = 0.0085885141045000009D;
            waveLengthControl1.WaveSource = WaveSource.Electron;
            waveLengthControl1.XrayWaveSourceElementNumber = 0;
            waveLengthControl1.XrayWaveSourceLine = XrayLine.Ka1;
            // 
            // panelGeometry
            // 
            resources.ApplyResources(panelGeometry, "panelGeometry");
            panelGeometry.Name = "panelGeometry";
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // numericBoxSampleTilt
            // 
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxSampleTilt, "numericBoxSampleTilt");
            numericBoxSampleTilt.Maximum = 90D;
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.RadianValue = 1.2217304763960306D;
            numericBoxSampleTilt.RoundErrorAccuracy = -1;
            numericBoxSampleTilt.ShowUpDown = true;
            numericBoxSampleTilt.Value = 70D;
            numericBoxSampleTilt.ValueChanged += numericBoxSampleTilt_ValueChanged;
            // 
            // buttonViewIsometric
            // 
            resources.ApplyResources(buttonViewIsometric, "buttonViewIsometric");
            buttonViewIsometric.Name = "buttonViewIsometric";
            buttonViewIsometric.UseVisualStyleBackColor = true;
            buttonViewIsometric.Click += buttonViewIsometric_Click;
            // 
            // buttonViewAlongBeam
            // 
            resources.ApplyResources(buttonViewAlongBeam, "buttonViewAlongBeam");
            buttonViewAlongBeam.Name = "buttonViewAlongBeam";
            buttonViewAlongBeam.UseVisualStyleBackColor = true;
            buttonViewAlongBeam.Click += buttonViewAlongBeam_Click;
            // 
            // paneltTrajectory
            // 
            resources.ApplyResources(paneltTrajectory, "paneltTrajectory");
            paneltTrajectory.Name = "paneltTrajectory";
            // 
            // FormEBSD
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(buttonViewIsometric);
            Controls.Add(buttonViewAlongBeam);
            Controls.Add(numericBoxSampleTilt);
            Controls.Add(button1);
            Controls.Add(paneltTrajectory);
            Controls.Add(panelGeometry);
            Controls.Add(waveLengthControl1);
            Name = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            Load += FormEBSD_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WaveLengthControl waveLengthControl1;
        private System.Windows.Forms.Panel panelGeometry;
        private System.Windows.Forms.Button button1;
        private NumericBox numericBoxSampleTilt;
        private System.Windows.Forms.Button buttonViewIsometric;
        private System.Windows.Forms.Button buttonViewAlongBeam;
        private System.Windows.Forms.Panel paneltTrajectory;
    }
}
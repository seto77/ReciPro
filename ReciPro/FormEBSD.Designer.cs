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
            panelGeometry = new System.Windows.Forms.Panel();
            numericBoxSampleTilt = new NumericBox();
            waveLengthControl1 = new WaveLengthControl();
            SuspendLayout();
            // 
            // panelGeometry
            // 
            panelGeometry.Location = new System.Drawing.Point(9, 74);
            panelGeometry.Name = "panelGeometry";
            panelGeometry.Size = new System.Drawing.Size(305, 285);
            panelGeometry.TabIndex = 0;
            // 
            // numericBoxSampleTilt
            // 
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            numericBoxSampleTilt.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxSampleTilt.FooterText = "°";
            numericBoxSampleTilt.HeaderText = "Sample tilt";
            numericBoxSampleTilt.Location = new System.Drawing.Point(9, 9);
            numericBoxSampleTilt.Margin = new System.Windows.Forms.Padding(0);
            numericBoxSampleTilt.Maximum = 90D;
            numericBoxSampleTilt.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.RoundErrorAccuracy = -1;
            numericBoxSampleTilt.ShowUpDown = true;
            numericBoxSampleTilt.Size = new System.Drawing.Size(137, 26);
            numericBoxSampleTilt.TabIndex = 111;
            numericBoxSampleTilt.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxSampleTilt.UpDown_Increment = 10D;
            // 
            // waveLengthControl1
            // 
            waveLengthControl1.AutoSize = true;
            waveLengthControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            waveLengthControl1.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl1.Energy = 20D;
            waveLengthControl1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            waveLengthControl1.Location = new System.Drawing.Point(217, 9);
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
            // FormEBSD
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 625);
            Controls.Add(numericBoxSampleTilt);
            Controls.Add(waveLengthControl1);
            Controls.Add(panelGeometry);
            Name = "FormEBSD";
            Text = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelGeometry;
        private NumericBox numericBoxSampleTilt;
        private WaveLengthControl waveLengthControl1;
    }
}
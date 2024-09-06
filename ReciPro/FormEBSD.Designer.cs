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
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonViewAlongBeam = new System.Windows.Forms.Button();
            buttonViewIsometric = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelGeometry
            // 
            panelGeometry.Location = new System.Drawing.Point(9, 67);
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
            numericBoxSampleTilt.Location = new System.Drawing.Point(9, 9);
            numericBoxSampleTilt.Margin = new System.Windows.Forms.Padding(0);
            numericBoxSampleTilt.Maximum = 90D;
            numericBoxSampleTilt.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.RadianValue = 1.2217304763960306D;
            numericBoxSampleTilt.RoundErrorAccuracy = -1;
            numericBoxSampleTilt.ShowUpDown = true;
            numericBoxSampleTilt.Size = new System.Drawing.Size(137, 26);
            numericBoxSampleTilt.TabIndex = 111;
            numericBoxSampleTilt.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxSampleTilt.UpDown_Increment = 10D;
            numericBoxSampleTilt.Value = 70D;
            // 
            // waveLengthControl1
            // 
            waveLengthControl1.AutoSize = true;
            waveLengthControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            waveLengthControl1.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl1.Energy = 20D;
            waveLengthControl1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            waveLengthControl1.Location = new System.Drawing.Point(199, 370);
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
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(buttonViewAlongBeam);
            flowLayoutPanel1.Controls.Add(buttonViewIsometric);
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel1.Location = new System.Drawing.Point(9, 370);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(178, 75);
            flowLayoutPanel1.TabIndex = 112;
            // 
            // buttonViewAlongBeam
            // 
            buttonViewAlongBeam.AutoSize = true;
            buttonViewAlongBeam.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonViewAlongBeam.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonViewAlongBeam.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonViewAlongBeam.Location = new System.Drawing.Point(0, 0);
            buttonViewAlongBeam.Margin = new System.Windows.Forms.Padding(0);
            buttonViewAlongBeam.Name = "buttonViewAlongBeam";
            buttonViewAlongBeam.Size = new System.Drawing.Size(178, 25);
            buttonViewAlongBeam.TabIndex = 99;
            buttonViewAlongBeam.Text = "From Z-axis (=beam direction)";
            buttonViewAlongBeam.UseVisualStyleBackColor = true;
            // 
            // buttonViewIsometric
            // 
            buttonViewIsometric.AutoSize = true;
            buttonViewIsometric.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonViewIsometric.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonViewIsometric.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonViewIsometric.Location = new System.Drawing.Point(0, 25);
            buttonViewIsometric.Margin = new System.Windows.Forms.Padding(0);
            buttonViewIsometric.Name = "buttonViewIsometric";
            buttonViewIsometric.Size = new System.Drawing.Size(154, 25);
            buttonViewIsometric.TabIndex = 98;
            buttonViewIsometric.Text = "From X-axis (rotation axis)";
            buttonViewIsometric.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            button1.Location = new System.Drawing.Point(0, 50);
            button1.Margin = new System.Windows.Forms.Padding(0);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(147, 25);
            button1.TabIndex = 98;
            button1.Text = "From the surface normal";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(199, 9);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 113;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FormEBSD
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 625);
            Controls.Add(button2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(numericBoxSampleTilt);
            Controls.Add(waveLengthControl1);
            Controls.Add(panelGeometry);
            Name = "FormEBSD";
            Text = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelGeometry;
        private NumericBox numericBoxSampleTilt;
        private WaveLengthControl waveLengthControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonViewAlongBeam;
        private System.Windows.Forms.Button buttonViewIsometric;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
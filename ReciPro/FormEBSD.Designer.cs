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
            SuspendLayout();
            // 
            // waveLengthControl1
            // 
            waveLengthControl1.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl1.Energy = 20D;
            resources.ApplyResources(waveLengthControl1, "waveLengthControl1");
            waveLengthControl1.Name = "waveLengthControl1";
            waveLengthControl1.ShowWaveSource = false;
            waveLengthControl1.WaveLength = 0.0085885141045000009D;
            waveLengthControl1.WaveSource = WaveSource.Electron;
            waveLengthControl1.XrayWaveSourceElementNumber = 0;
            waveLengthControl1.XrayWaveSourceLine = XrayLine.Ka1;
            // 
            // FormEBSD
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(waveLengthControl1);
            Name = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            Load += FormEBSD_Load;
            ResumeLayout(false);
        }

        #endregion

        private WaveLengthControl waveLengthControl1;
    }
}
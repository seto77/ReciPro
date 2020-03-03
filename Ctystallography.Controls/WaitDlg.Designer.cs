namespace Crystallography.Controls
{
    partial class WaitDlg
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

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitDlg));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panelProgresspar = new System.Windows.Forms.Panel();
            this.checkBoxCloseWindow = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelProgresspar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.progressBar.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar.ForeColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.MarqueeAnimationSpeed = 1;
            this.progressBar.Maximum = 1000000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelVersion.Name = "labelVersion";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.label5.Name = "label5";
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrevious
            // 
            resources.ApplyResources(this.buttonPrevious, "buttonPrevious");
            this.buttonPrevious.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Name = "label3";
            // 
            // panelProgresspar
            // 
            this.panelProgresspar.Controls.Add(this.progressBar);
            resources.ApplyResources(this.panelProgresspar, "panelProgresspar");
            this.panelProgresspar.Name = "panelProgresspar";
            // 
            // checkBoxCloseWindow
            // 
            resources.ApplyResources(this.checkBoxCloseWindow, "checkBoxCloseWindow");
            this.checkBoxCloseWindow.Checked = true;
            this.checkBoxCloseWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCloseWindow.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBoxCloseWindow.Name = "checkBoxCloseWindow";
            this.checkBoxCloseWindow.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.buttonPrevious);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.buttonNext);
            this.panel1.Name = "panel1";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.labelVersion);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.buttonOK);
            this.flowLayoutPanel2.Controls.Add(this.checkBoxCloseWindow);
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // WaitDlg
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelProgresspar);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitDlg";
            this.ShowInTaskbar = false;
            this.panelProgresspar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelProgresspar;
        private System.Windows.Forms.CheckBox checkBoxCloseWindow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}
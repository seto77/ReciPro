namespace Crystallography.Controls
{
    partial class CommonDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonDialog));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textBox = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelSoftwareAndVersion = new System.Windows.Forms.Label();
            this.labelCopyRight = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.checkBoxCloseWindow = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelSoftwareInformation = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panelOK = new System.Windows.Forms.Panel();
            this.flowLayoutPanelSoftwareInformation.SuspendLayout();
            this.panelOK.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.progressBar.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.ForeColor = System.Drawing.Color.Silver;
            this.progressBar.MarqueeAnimationSpeed = 1;
            this.progressBar.Maximum = 1000000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.Value = 50000;
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.AliceBlue;
            resources.ApplyResources(this.textBox, "textBox");
            this.textBox.ForeColor = System.Drawing.Color.DarkBlue;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelSoftwareAndVersion
            // 
            resources.ApplyResources(this.labelSoftwareAndVersion, "labelSoftwareAndVersion");
            this.labelSoftwareAndVersion.Name = "labelSoftwareAndVersion";
            // 
            // labelCopyRight
            // 
            resources.ApplyResources(this.labelCopyRight, "labelCopyRight");
            this.labelCopyRight.Name = "labelCopyRight";
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // checkBoxCloseWindow
            // 
            resources.ApplyResources(this.checkBoxCloseWindow, "checkBoxCloseWindow");
            this.checkBoxCloseWindow.Checked = true;
            this.checkBoxCloseWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCloseWindow.Name = "checkBoxCloseWindow";
            this.checkBoxCloseWindow.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelSoftwareInformation
            // 
            resources.ApplyResources(this.flowLayoutPanelSoftwareInformation, "flowLayoutPanelSoftwareInformation");
            this.flowLayoutPanelSoftwareInformation.Controls.Add(this.labelSoftwareAndVersion);
            this.flowLayoutPanelSoftwareInformation.Controls.Add(this.labelCopyRight);
            this.flowLayoutPanelSoftwareInformation.Controls.Add(this.linkLabel1);
            this.flowLayoutPanelSoftwareInformation.Name = "flowLayoutPanelSoftwareInformation";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            // 
            // panelOK
            // 
            this.panelOK.Controls.Add(this.checkBoxCloseWindow);
            this.panelOK.Controls.Add(this.buttonOK);
            this.panelOK.Controls.Add(this.buttonNext);
            resources.ApplyResources(this.panelOK, "panelOK");
            this.panelOK.Name = "panelOK";
            // 
            // CommonDialog
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.flowLayoutPanelSoftwareInformation);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panelOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CommonDialog";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommonDialog_FormClosing);
            this.flowLayoutPanelSoftwareInformation.ResumeLayout(false);
            this.flowLayoutPanelSoftwareInformation.PerformLayout();
            this.panelOK.ResumeLayout(false);
            this.panelOK.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Label labelSoftwareAndVersion;
        private System.Windows.Forms.Label labelCopyRight;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.CheckBox checkBoxCloseWindow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSoftwareInformation;
        private System.Windows.Forms.Panel panelOK;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
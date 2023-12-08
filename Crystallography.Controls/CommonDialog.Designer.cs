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
            progressBar = new System.Windows.Forms.ProgressBar();
            textBox = new System.Windows.Forms.TextBox();
            buttonOK = new System.Windows.Forms.Button();
            labelSoftwareAndVersion = new System.Windows.Forms.Label();
            labelCopyRight = new System.Windows.Forms.Label();
            buttonNext = new System.Windows.Forms.Button();
            checkBoxCloseWindow = new System.Windows.Forms.CheckBox();
            flowLayoutPanelSoftwareInformation = new System.Windows.Forms.FlowLayoutPanel();
            labelAuthor = new System.Windows.Forms.Label();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            panelOK = new System.Windows.Forms.Panel();
            flowLayoutPanelSoftwareInformation.SuspendLayout();
            panelOK.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            progressBar.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            resources.ApplyResources(progressBar, "progressBar");
            progressBar.ForeColor = System.Drawing.Color.Silver;
            progressBar.MarqueeAnimationSpeed = 1;
            progressBar.Maximum = 1000000;
            progressBar.Name = "progressBar";
            progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBar.Value = 50000;
            // 
            // textBox
            // 
            textBox.BackColor = System.Drawing.Color.AliceBlue;
            resources.ApplyResources(textBox, "textBox");
            textBox.ForeColor = System.Drawing.Color.DarkBlue;
            textBox.Name = "textBox";
            textBox.ReadOnly = true;
            // 
            // buttonOK
            // 
            resources.ApplyResources(buttonOK, "buttonOK");
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOK.Name = "buttonOK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // labelSoftwareAndVersion
            // 
            resources.ApplyResources(labelSoftwareAndVersion, "labelSoftwareAndVersion");
            labelSoftwareAndVersion.Name = "labelSoftwareAndVersion";
            // 
            // labelCopyRight
            // 
            resources.ApplyResources(labelCopyRight, "labelCopyRight");
            labelCopyRight.Name = "labelCopyRight";
            // 
            // buttonNext
            // 
            resources.ApplyResources(buttonNext, "buttonNext");
            buttonNext.Name = "buttonNext";
            buttonNext.UseVisualStyleBackColor = true;
            buttonNext.Click += buttonNext_Click;
            // 
            // checkBoxCloseWindow
            // 
            resources.ApplyResources(checkBoxCloseWindow, "checkBoxCloseWindow");
            checkBoxCloseWindow.Checked = true;
            checkBoxCloseWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxCloseWindow.Name = "checkBoxCloseWindow";
            checkBoxCloseWindow.UseVisualStyleBackColor = true;
            checkBoxCloseWindow.CheckedChanged += checkBoxCloseWindow_CheckedChanged;
            // 
            // flowLayoutPanelSoftwareInformation
            // 
            resources.ApplyResources(flowLayoutPanelSoftwareInformation, "flowLayoutPanelSoftwareInformation");
            flowLayoutPanelSoftwareInformation.Controls.Add(labelSoftwareAndVersion);
            flowLayoutPanelSoftwareInformation.Controls.Add(labelCopyRight);
            flowLayoutPanelSoftwareInformation.Controls.Add(labelAuthor);
            flowLayoutPanelSoftwareInformation.Controls.Add(linkLabel1);
            flowLayoutPanelSoftwareInformation.Name = "flowLayoutPanelSoftwareInformation";
            // 
            // labelAuthor
            // 
            resources.ApplyResources(labelAuthor, "labelAuthor");
            labelAuthor.Name = "labelAuthor";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(linkLabel1, "linkLabel1");
            linkLabel1.Name = "linkLabel1";
            linkLabel1.TabStop = true;
            // 
            // panelOK
            // 
            panelOK.Controls.Add(checkBoxCloseWindow);
            panelOK.Controls.Add(buttonOK);
            panelOK.Controls.Add(buttonNext);
            resources.ApplyResources(panelOK, "panelOK");
            panelOK.Name = "panelOK";
            // 
            // CommonDialog
            // 
            AcceptButton = buttonOK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(textBox);
            Controls.Add(flowLayoutPanelSoftwareInformation);
            Controls.Add(progressBar);
            Controls.Add(panelOK);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            Name = "CommonDialog";
            ShowIcon = false;
            FormClosing += CommonDialog_FormClosing;
            flowLayoutPanelSoftwareInformation.ResumeLayout(false);
            flowLayoutPanelSoftwareInformation.PerformLayout();
            panelOK.ResumeLayout(false);
            panelOK.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Label labelAuthor;
    }
}
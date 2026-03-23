namespace Crystallography.Controls;

partial class FormCaptureGUI
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        treeViewControls = new System.Windows.Forms.TreeView();
        buttonCapture = new System.Windows.Forms.Button();
        buttonSelectAll = new System.Windows.Forms.Button();
        buttonDeselectAll = new System.Windows.Forms.Button();
        buttonRefresh = new System.Windows.Forms.Button();
        progressBar = new System.Windows.Forms.ProgressBar();
        labelStatus = new System.Windows.Forms.Label();
        textBoxOutputDir = new System.Windows.Forms.TextBox();
        buttonSelectDir = new System.Windows.Forms.Button();
        panelTop = new System.Windows.Forms.Panel();
        panelBottom = new System.Windows.Forms.Panel();
        panelTop.SuspendLayout();
        panelBottom.SuspendLayout();
        SuspendLayout();
        //
        // panelTop
        //
        panelTop.Controls.Add(buttonRefresh);
        panelTop.Controls.Add(buttonDeselectAll);
        panelTop.Controls.Add(buttonSelectAll);
        panelTop.Dock = System.Windows.Forms.DockStyle.Top;
        panelTop.Location = new System.Drawing.Point(0, 0);
        panelTop.Name = "panelTop";
        panelTop.Size = new System.Drawing.Size(500, 36);
        panelTop.TabIndex = 0;
        //
        // buttonSelectAll
        //
        buttonSelectAll.Location = new System.Drawing.Point(6, 6);
        buttonSelectAll.Name = "buttonSelectAll";
        buttonSelectAll.Size = new System.Drawing.Size(80, 26);
        buttonSelectAll.TabIndex = 0;
        buttonSelectAll.Text = "Select All";
        buttonSelectAll.Click += buttonSelectAll_Click;
        //
        // buttonDeselectAll
        //
        buttonDeselectAll.Location = new System.Drawing.Point(92, 6);
        buttonDeselectAll.Name = "buttonDeselectAll";
        buttonDeselectAll.Size = new System.Drawing.Size(85, 26);
        buttonDeselectAll.TabIndex = 1;
        buttonDeselectAll.Text = "Deselect All";
        buttonDeselectAll.Click += buttonDeselectAll_Click;
        //
        // buttonRefresh
        //
        buttonRefresh.Location = new System.Drawing.Point(183, 6);
        buttonRefresh.Name = "buttonRefresh";
        buttonRefresh.Size = new System.Drawing.Size(75, 26);
        buttonRefresh.TabIndex = 2;
        buttonRefresh.Text = "Refresh";
        buttonRefresh.Click += buttonRefresh_Click;
        //
        // treeViewControls
        //
        treeViewControls.CheckBoxes = true;
        treeViewControls.Dock = System.Windows.Forms.DockStyle.Fill;
        treeViewControls.Location = new System.Drawing.Point(0, 36);
        treeViewControls.Name = "treeViewControls";
        treeViewControls.Size = new System.Drawing.Size(500, 440);
        treeViewControls.TabIndex = 1;
        treeViewControls.AfterCheck += treeViewControls_AfterCheck;
        //
        // panelBottom
        //
        panelBottom.Controls.Add(labelStatus);
        panelBottom.Controls.Add(progressBar);
        panelBottom.Controls.Add(buttonCapture);
        panelBottom.Controls.Add(textBoxOutputDir);
        panelBottom.Controls.Add(buttonSelectDir);
        panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
        panelBottom.Location = new System.Drawing.Point(0, 446);
        panelBottom.Name = "panelBottom";
        panelBottom.Size = new System.Drawing.Size(500, 90);
        panelBottom.TabIndex = 2;
        //
        // textBoxOutputDir (260323Cl 追加)
        //
        textBoxOutputDir.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        textBoxOutputDir.Location = new System.Drawing.Point(6, 6);
        textBoxOutputDir.Name = "textBoxOutputDir";
        textBoxOutputDir.Size = new System.Drawing.Size(406, 23);
        textBoxOutputDir.TabIndex = 0;
        textBoxOutputDir.ReadOnly = true;
        //
        // buttonSelectDir (260323Cl 追加)
        //
        buttonSelectDir.Anchor = System.Windows.Forms.AnchorStyles.Right;
        buttonSelectDir.Location = new System.Drawing.Point(418, 5);
        buttonSelectDir.Name = "buttonSelectDir";
        buttonSelectDir.Size = new System.Drawing.Size(75, 25);
        buttonSelectDir.TabIndex = 1;
        buttonSelectDir.Text = "Select...";
        buttonSelectDir.Click += buttonSelectDir_Click;
        //
        // buttonCapture
        //
        buttonCapture.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        buttonCapture.Location = new System.Drawing.Point(6, 35);
        buttonCapture.Name = "buttonCapture";
        buttonCapture.Size = new System.Drawing.Size(120, 30);
        buttonCapture.TabIndex = 2;
        buttonCapture.Text = "Capture";
        buttonCapture.Click += buttonCapture_Click;
        //
        // progressBar
        //
        progressBar.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        progressBar.Location = new System.Drawing.Point(132, 36);
        progressBar.Name = "progressBar";
        progressBar.Size = new System.Drawing.Size(362, 18);
        progressBar.TabIndex = 3;
        //
        // labelStatus
        //
        labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        labelStatus.Location = new System.Drawing.Point(132, 58);
        labelStatus.Name = "labelStatus";
        labelStatus.Size = new System.Drawing.Size(362, 20);
        labelStatus.TabIndex = 4;
        labelStatus.Text = "Ready";
        //
        // FormCaptureGUI
        //
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(500, 536);
        Controls.Add(treeViewControls);
        Controls.Add(panelBottom);
        Controls.Add(panelTop);
        Name = "FormCaptureGUI";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Capture GUI Components";
        panelTop.ResumeLayout(false);
        panelBottom.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.TreeView treeViewControls;
    private System.Windows.Forms.Button buttonCapture;
    private System.Windows.Forms.Button buttonSelectAll;
    private System.Windows.Forms.Button buttonDeselectAll;
    private System.Windows.Forms.Button buttonRefresh;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.TextBox textBoxOutputDir;
    private System.Windows.Forms.Button buttonSelectDir;
    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.Panel panelBottom;
}

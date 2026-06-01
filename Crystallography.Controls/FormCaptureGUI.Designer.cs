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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCaptureGUI)); // 260531Cl
            components = new System.ComponentModel.Container(); // (260531Ch)
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
        treeViewControls = new System.Windows.Forms.TreeView();
        buttonCapture = new System.Windows.Forms.Button();
        buttonSelectAll = new System.Windows.Forms.Button();
        buttonDeselectAll = new System.Windows.Forms.Button();
        buttonRefresh = new System.Windows.Forms.Button();
        comboBoxTargetForm = new System.Windows.Forms.ComboBox(); // 260521Cl 追加
        labelTargetForm = new System.Windows.Forms.Label(); // 260521Cl 追加
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
        panelTop.Controls.Add(comboBoxTargetForm); // 260521Cl 追加
        panelTop.Controls.Add(labelTargetForm); // 260521Cl 追加
        panelTop.Controls.Add(buttonRefresh);
        panelTop.Controls.Add(buttonDeselectAll);
        panelTop.Controls.Add(buttonSelectAll);
        panelTop.Dock = System.Windows.Forms.DockStyle.Top;
        panelTop.Location = new System.Drawing.Point(0, 0);
        panelTop.Name = "panelTop";
        // panelTop.Size = new System.Drawing.Size(500, 36); // 260521Cl 旧: 対象フォーム選択行を追加し 36→70
        panelTop.Size = new System.Drawing.Size(500, 70); // 260521Cl
        panelTop.TabIndex = 0;
        //
        // labelTargetForm (260521Cl 追加)
        //
        labelTargetForm.AutoSize = true;
        labelTargetForm.Location = new System.Drawing.Point(6, 11);
        labelTargetForm.Name = "labelTargetForm";
        toolTip.SetToolTip(labelTargetForm, resources.GetString("labelTargetForm.ToolTip")); // 260531Cl
        labelTargetForm.Text = "Target form:";
        //
        // comboBoxTargetForm (260521Cl 追加: ActiveForm 制約を解消し、開いている全フォームから対象を選択する)
        //
        comboBoxTargetForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        comboBoxTargetForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBoxTargetForm.Location = new System.Drawing.Point(82, 8);
        comboBoxTargetForm.Name = "comboBoxTargetForm";
        toolTip.SetToolTip(comboBoxTargetForm, resources.GetString("comboBoxTargetForm.ToolTip")); // 260531Cl
        comboBoxTargetForm.Size = new System.Drawing.Size(412, 23);
        comboBoxTargetForm.TabIndex = 0;
        comboBoxTargetForm.SelectedIndexChanged += comboBoxTargetForm_SelectedIndexChanged;
        //
        // buttonSelectAll
        //
        buttonSelectAll.Location = new System.Drawing.Point(6, 39);
        buttonSelectAll.Name = "buttonSelectAll";
        toolTip.SetToolTip(buttonSelectAll, resources.GetString("buttonSelectAll.ToolTip")); // 260531Cl
        buttonSelectAll.Size = new System.Drawing.Size(80, 26);
        buttonSelectAll.TabIndex = 0;
        buttonSelectAll.Text = "Select All";
        buttonSelectAll.Click += buttonSelectAll_Click;
        //
        // buttonDeselectAll
        //
        buttonDeselectAll.Location = new System.Drawing.Point(92, 39);
        buttonDeselectAll.Name = "buttonDeselectAll";
        toolTip.SetToolTip(buttonDeselectAll, resources.GetString("buttonDeselectAll.ToolTip")); // 260531Cl
        buttonDeselectAll.Size = new System.Drawing.Size(85, 26);
        buttonDeselectAll.TabIndex = 1;
        buttonDeselectAll.Text = "Deselect All";
        buttonDeselectAll.Click += buttonDeselectAll_Click;
        //
        // buttonRefresh
        //
        buttonRefresh.Location = new System.Drawing.Point(183, 39);
        buttonRefresh.Name = "buttonRefresh";
        toolTip.SetToolTip(buttonRefresh, resources.GetString("buttonRefresh.ToolTip")); // 260531Cl
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
        toolTip.SetToolTip(textBoxOutputDir, resources.GetString("textBoxOutputDir.ToolTip")); // 260531Cl
        textBoxOutputDir.Size = new System.Drawing.Size(406, 23);
        textBoxOutputDir.TabIndex = 0;
        textBoxOutputDir.ReadOnly = true;
        //
        // buttonSelectDir (260323Cl 追加)
        //
        buttonSelectDir.Anchor = System.Windows.Forms.AnchorStyles.Right;
        buttonSelectDir.Location = new System.Drawing.Point(418, 5);
        buttonSelectDir.Name = "buttonSelectDir";
        toolTip.SetToolTip(buttonSelectDir, resources.GetString("buttonSelectDir.ToolTip")); // 260531Cl
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
        toolTip.SetToolTip(buttonCapture, resources.GetString("buttonCapture.ToolTip")); // 260531Cl
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

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)

    private System.Windows.Forms.TreeView treeViewControls;
    private System.Windows.Forms.Button buttonCapture;
    private System.Windows.Forms.Button buttonSelectAll;
    private System.Windows.Forms.Button buttonDeselectAll;
    private System.Windows.Forms.Button buttonRefresh;
    private System.Windows.Forms.ComboBox comboBoxTargetForm; // 260521Cl 追加
    private System.Windows.Forms.Label labelTargetForm; // 260521Cl 追加
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.TextBox textBoxOutputDir;
    private System.Windows.Forms.Button buttonSelectDir;
    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.Panel panelBottom;
}

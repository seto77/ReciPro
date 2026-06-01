namespace Crystallography.Controls;

partial class FormSuperStructure
{
    /// <summary>Required designer variable.</summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>Clean up any resources being used.</summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSuperStructure)); // 260531Cl
            components = new System.ComponentModel.Container(); // (260531Ch)
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
        numericBoxA = new NumericBox();
        numericBoxB = new NumericBox();
        numericBoxC = new NumericBox();
        buttonOK = new System.Windows.Forms.Button();
        buttonCancel = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // numericBoxA
        // 
        numericBoxA.BackColor = System.Drawing.Color.Transparent;
        numericBoxA.HeaderText = "a × ";
        numericBoxA.Location = new System.Drawing.Point(9, 9);
        numericBoxA.Margin = new System.Windows.Forms.Padding(0);
        numericBoxA.Maximum = 100D;
        numericBoxA.MaximumSize = new System.Drawing.Size(1000, 25);
        numericBoxA.Minimum = 1D;
        numericBoxA.MinimumSize = new System.Drawing.Size(1, 25);
        numericBoxA.Name = "numericBoxA";
        toolTip.SetToolTip(numericBoxA, resources.GetString("numericBoxA.ToolTip")); // 260531Cl
        numericBoxA.RadianValue = 0.017453292519943295D;
        numericBoxA.ShowUpDown = true;
        numericBoxA.Size = new System.Drawing.Size(77, 25);
        numericBoxA.TabIndex = 0;
        numericBoxA.Value = 1D;
        // 
        // numericBoxB
        // 
        numericBoxB.BackColor = System.Drawing.Color.Transparent;
        numericBoxB.HeaderText = "b × ";
        numericBoxB.Location = new System.Drawing.Point(96, 9);
        numericBoxB.Margin = new System.Windows.Forms.Padding(0);
        numericBoxB.Maximum = 100D;
        numericBoxB.MaximumSize = new System.Drawing.Size(1000, 25);
        numericBoxB.Minimum = 1D;
        numericBoxB.MinimumSize = new System.Drawing.Size(1, 25);
        numericBoxB.Name = "numericBoxB";
        toolTip.SetToolTip(numericBoxB, resources.GetString("numericBoxB.ToolTip")); // 260531Cl
        numericBoxB.RadianValue = 0.017453292519943295D;
        numericBoxB.ShowUpDown = true;
        numericBoxB.Size = new System.Drawing.Size(77, 25);
        numericBoxB.TabIndex = 0;
        numericBoxB.Value = 1D;
        // 
        // numericBoxC
        // 
        numericBoxC.BackColor = System.Drawing.Color.Transparent;
        numericBoxC.HeaderText = "c × ";
        numericBoxC.Location = new System.Drawing.Point(187, 9);
        numericBoxC.Margin = new System.Windows.Forms.Padding(0);
        numericBoxC.Maximum = 100D;
        numericBoxC.MaximumSize = new System.Drawing.Size(1000, 25);
        numericBoxC.Minimum = 1D;
        numericBoxC.MinimumSize = new System.Drawing.Size(1, 25);
        numericBoxC.Name = "numericBoxC";
        toolTip.SetToolTip(numericBoxC, resources.GetString("numericBoxC.ToolTip")); // 260531Cl
        numericBoxC.RadianValue = 0.017453292519943295D;
        numericBoxC.ShowUpDown = true;
        numericBoxC.Size = new System.Drawing.Size(77, 25);
        numericBoxC.TabIndex = 0;
        numericBoxC.Value = 1D;
        // 
        // buttonOK
        // 
        buttonOK.AutoSize = true;
        buttonOK.Location = new System.Drawing.Point(138, 40);
        buttonOK.Name = "buttonOK";
        toolTip.SetToolTip(buttonOK, resources.GetString("buttonOK.ToolTip")); // 260531Cl
        buttonOK.Size = new System.Drawing.Size(60, 25);
        buttonOK.TabIndex = 1;
        buttonOK.Text = "OK";
        buttonOK.UseVisualStyleBackColor = true;
        buttonOK.Click += buttonOK_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.AutoSize = true;
        buttonCancel.Location = new System.Drawing.Point(204, 40);
        buttonCancel.Name = "buttonCancel";
        toolTip.SetToolTip(buttonCancel, resources.GetString("buttonCancel.ToolTip")); // 260531Cl
        buttonCancel.Size = new System.Drawing.Size(60, 25);
        buttonCancel.TabIndex = 1;
        buttonCancel.Text = "Cancel";
        buttonCancel.UseVisualStyleBackColor = true;
        buttonCancel.Click += buttonCancel_Click;
        // 
        // FormSuperStructure
        // 
        AcceptButton = buttonOK;
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        AutoSize = true;
        AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        CancelButton = buttonCancel;
        ClientSize = new System.Drawing.Size(270, 71);
        ControlBox = false;
        Controls.Add(buttonCancel);
        Controls.Add(buttonOK);
        Controls.Add(numericBoxC);
        Controls.Add(numericBoxB);
        Controls.Add(numericBoxA);
        Font = new System.Drawing.Font("Segoe UI", 9F);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        Name = "FormSuperStructure";
        ShowIcon = false;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Set superstructure size";
        TopMost = true;
        ResumeLayout(false);
        PerformLayout();

    }

    #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)

    private NumericBox numericBoxA;
    private NumericBox numericBoxB;
    private NumericBox numericBoxC;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
}
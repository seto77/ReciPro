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
        numericBoxA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
        numericBoxA.HeaderText = "a × ";
        numericBoxA.Location = new System.Drawing.Point(9, 9);
        numericBoxA.Margin = new System.Windows.Forms.Padding(0);
        numericBoxA.Maximum = 100D;
        numericBoxA.MaximumSize = new System.Drawing.Size(1000, 25);
        numericBoxA.Minimum = 1D;
        numericBoxA.MinimumSize = new System.Drawing.Size(1, 25);
        numericBoxA.Name = "numericBoxA";
        numericBoxA.RadianValue = 0.017453292519943295D;
        numericBoxA.ShowUpDown = true;
        numericBoxA.Size = new System.Drawing.Size(77, 25);
        numericBoxA.TabIndex = 0;
        numericBoxA.Value = 1D;
        // 
        // numericBoxB
        // 
        numericBoxB.BackColor = System.Drawing.Color.Transparent;
        numericBoxB.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
        numericBoxB.HeaderText = "b × ";
        numericBoxB.Location = new System.Drawing.Point(96, 9);
        numericBoxB.Margin = new System.Windows.Forms.Padding(0);
        numericBoxB.Maximum = 100D;
        numericBoxB.MaximumSize = new System.Drawing.Size(1000, 25);
        numericBoxB.Minimum = 1D;
        numericBoxB.MinimumSize = new System.Drawing.Size(1, 25);
        numericBoxB.Name = "numericBoxB";
        numericBoxB.RadianValue = 0.017453292519943295D;
        numericBoxB.ShowUpDown = true;
        numericBoxB.Size = new System.Drawing.Size(77, 25);
        numericBoxB.TabIndex = 0;
        numericBoxB.Value = 1D;
        // 
        // numericBoxC
        // 
        numericBoxC.BackColor = System.Drawing.Color.Transparent;
        numericBoxC.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
        numericBoxC.HeaderText = "c × ";
        numericBoxC.Location = new System.Drawing.Point(187, 9);
        numericBoxC.Margin = new System.Windows.Forms.Padding(0);
        numericBoxC.Maximum = 100D;
        numericBoxC.MaximumSize = new System.Drawing.Size(1000, 25);
        numericBoxC.Minimum = 1D;
        numericBoxC.MinimumSize = new System.Drawing.Size(1, 25);
        numericBoxC.Name = "numericBoxC";
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
        Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
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

    private NumericBox numericBoxA;
    private NumericBox numericBoxB;
    private NumericBox numericBoxC;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
}
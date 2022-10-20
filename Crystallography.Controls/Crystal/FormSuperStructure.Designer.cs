namespace Crystallography.Controls;

partial class FormSuperStructure
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
            this.numericBoxA = new Crystallography.Controls.NumericBox();
            this.numericBoxB = new Crystallography.Controls.NumericBox();
            this.numericBoxC = new Crystallography.Controls.NumericBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // numericBoxA
            // 
            this.numericBoxA.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxA.HeaderText = "a × ";
            this.numericBoxA.Location = new System.Drawing.Point(9, 9);
            this.numericBoxA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxA.Maximum = 100D;
            this.numericBoxA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxA.Minimum = 1D;
            this.numericBoxA.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxA.Name = "numericBoxA";
            this.numericBoxA.RadianValue = 0.017453292519943295D;
            this.numericBoxA.RoundErrorAccuracy = -1;
            this.numericBoxA.ShowUpDown = true;
            this.numericBoxA.Size = new System.Drawing.Size(77, 25);
            this.numericBoxA.TabIndex = 0;
            this.numericBoxA.Value = 1D;
            // 
            // numericBoxB
            // 
            this.numericBoxB.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxB.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxB.HeaderText = "b × ";
            this.numericBoxB.Location = new System.Drawing.Point(96, 9);
            this.numericBoxB.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxB.Maximum = 100D;
            this.numericBoxB.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxB.Minimum = 1D;
            this.numericBoxB.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxB.Name = "numericBoxB";
            this.numericBoxB.RadianValue = 0.017453292519943295D;
            this.numericBoxB.RoundErrorAccuracy = -1;
            this.numericBoxB.ShowUpDown = true;
            this.numericBoxB.Size = new System.Drawing.Size(77, 25);
            this.numericBoxB.TabIndex = 0;
            this.numericBoxB.Value = 1D;
            // 
            // numericBoxC
            // 
            this.numericBoxC.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxC.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxC.HeaderText = "c × ";
            this.numericBoxC.Location = new System.Drawing.Point(187, 9);
            this.numericBoxC.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxC.Maximum = 100D;
            this.numericBoxC.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxC.Minimum = 1D;
            this.numericBoxC.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxC.Name = "numericBoxC";
            this.numericBoxC.RadianValue = 0.017453292519943295D;
            this.numericBoxC.RoundErrorAccuracy = -1;
            this.numericBoxC.ShowUpDown = true;
            this.numericBoxC.Size = new System.Drawing.Size(77, 25);
            this.numericBoxC.TabIndex = 0;
            this.numericBoxC.Value = 1D;
            // 
            // buttonOK
            // 
            this.buttonOK.AutoSize = true;
            this.buttonOK.Location = new System.Drawing.Point(138, 40);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(60, 25);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.Location = new System.Drawing.Point(204, 40);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(60, 25);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormSuperStructure
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(270, 71);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.numericBoxC);
            this.Controls.Add(this.numericBoxB);
            this.Controls.Add(this.numericBoxA);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormSuperStructure";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set superstructure size";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private NumericBox numericBoxA;
    private NumericBox numericBoxB;
    private NumericBox numericBoxC;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
}
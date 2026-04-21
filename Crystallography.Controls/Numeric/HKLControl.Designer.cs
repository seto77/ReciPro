namespace Crystallography.Controls;

// 260421Cl 追加: HKLControl のデザイナコード (partial)
partial class HKLControl
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
        numericBoxH = new NumericBox();
        numericBoxK = new NumericBox();
        numericBoxL = new NumericBox();
        labelI = new System.Windows.Forms.Label();
        tableLayoutPanel.SuspendLayout();
        SuspendLayout();
        //
        // tableLayoutPanel
        //
        tableLayoutPanel.AutoSize = true;
        tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tableLayoutPanel.ColumnCount = 4;
        // 初期状態は ShowIIndex=false 相当。i 列を 0 幅にし、他を 33.33% で割当て。
        tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
        tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanel.Controls.Add(numericBoxH, 0, 0);
        tableLayoutPanel.Controls.Add(numericBoxK, 1, 0);
        tableLayoutPanel.Controls.Add(labelI, 2, 0);
        tableLayoutPanel.Controls.Add(numericBoxL, 3, 0);
        tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
        tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
        tableLayoutPanel.Name = "tableLayoutPanel";
        tableLayoutPanel.RowCount = 1;
        tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel.Size = new System.Drawing.Size(120, 25);
        tableLayoutPanel.TabIndex = 0;
        //
        // numericBoxH
        //
        numericBoxH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        numericBoxH.BackColor = System.Drawing.SystemColors.Control;
        numericBoxH.DecimalPlaces = 0;
        numericBoxH.Dock = System.Windows.Forms.DockStyle.Fill;
        numericBoxH.FooterBackColor = System.Drawing.SystemColors.Control;
        numericBoxH.HeaderBackColor = System.Drawing.SystemColors.Control;
        numericBoxH.Margin = new System.Windows.Forms.Padding(0);
        numericBoxH.Maximum = 50D;
        numericBoxH.Minimum = -50D;
        numericBoxH.Name = "numericBoxH";
        numericBoxH.ShowUpDown = false;
        numericBoxH.SkipEventDuringInput = false;
        //
        // numericBoxK
        //
        numericBoxK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        numericBoxK.BackColor = System.Drawing.SystemColors.Control;
        numericBoxK.DecimalPlaces = 0;
        numericBoxK.Dock = System.Windows.Forms.DockStyle.Fill;
        numericBoxK.FooterBackColor = System.Drawing.SystemColors.Control;
        numericBoxK.HeaderBackColor = System.Drawing.SystemColors.Control;
        numericBoxK.Margin = new System.Windows.Forms.Padding(0);
        numericBoxK.Maximum = 50D;
        numericBoxK.Minimum = -50D;
        numericBoxK.Name = "numericBoxK";
        numericBoxK.ShowUpDown = false;
        numericBoxK.SkipEventDuringInput = false;
        //
        // numericBoxL
        //
        numericBoxL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        numericBoxL.BackColor = System.Drawing.SystemColors.Control;
        numericBoxL.DecimalPlaces = 0;
        numericBoxL.Dock = System.Windows.Forms.DockStyle.Fill;
        numericBoxL.FooterBackColor = System.Drawing.SystemColors.Control;
        numericBoxL.HeaderBackColor = System.Drawing.SystemColors.Control;
        numericBoxL.Margin = new System.Windows.Forms.Padding(0);
        numericBoxL.Maximum = 50D;
        numericBoxL.Minimum = -50D;
        numericBoxL.Name = "numericBoxL";
        numericBoxL.ShowUpDown = false;
        numericBoxL.SkipEventDuringInput = false;
        //
        // labelI
        //
        labelI.BackColor = System.Drawing.SystemColors.ControlDark;
        labelI.Dock = System.Windows.Forms.DockStyle.Fill;
        labelI.ForeColor = System.Drawing.SystemColors.ControlText;
        labelI.Margin = new System.Windows.Forms.Padding(0);
        labelI.Name = "labelI";
        labelI.Padding = new System.Windows.Forms.Padding(0);
        labelI.Text = "0";
        labelI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        labelI.Visible = false;
        //
        // HKLControl
        //
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        AutoSize = true;
        AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        Controls.Add(tableLayoutPanel);
        Margin = new System.Windows.Forms.Padding(0);
        Name = "HKLControl";
        Size = new System.Drawing.Size(120, 25);
        tableLayoutPanel.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    private NumericBox numericBoxH;
    private NumericBox numericBoxK;
    private NumericBox numericBoxL;
    private System.Windows.Forms.Label labelI;
}

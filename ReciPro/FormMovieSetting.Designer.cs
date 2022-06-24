namespace ReciPro;

partial class FormMovieSetting
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.numericBoxSpeed = new Crystallography.Controls.NumericBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAntiClock = new System.Windows.Forms.Button();
            this.buttonClock = new System.Windows.Forms.Button();
            this.buttonTopLeft = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonBottomLeft = new System.Windows.Forms.Button();
            this.buttonBottom = new System.Windows.Forms.Button();
            this.buttonBottomRight = new System.Windows.Forms.Button();
            this.buttonTop = new System.Windows.Forms.Button();
            this.buttonTopRight = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.numericBoxDuration = new Crystallography.Controls.NumericBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(9, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(90, 159);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // numericBoxSpeed
            // 
            this.numericBoxSpeed.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxSpeed.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxSpeed.FooterText = "°/sec";
            this.numericBoxSpeed.HeaderText = "Speed";
            this.numericBoxSpeed.Location = new System.Drawing.Point(9, 6);
            this.numericBoxSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxSpeed.Maximum = 360D;
            this.numericBoxSpeed.MaximumSize = new System.Drawing.Size(1000, 27);
            this.numericBoxSpeed.Minimum = 1D;
            this.numericBoxSpeed.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxSpeed.Name = "numericBoxSpeed";
            this.numericBoxSpeed.RadianValue = 0.52359877559829882D;
            this.numericBoxSpeed.RoundErrorAccuracy = -1;
            this.numericBoxSpeed.ShowUpDown = true;
            this.numericBoxSpeed.Size = new System.Drawing.Size(156, 27);
            this.numericBoxSpeed.SmartIncrement = true;
            this.numericBoxSpeed.TabIndex = 1;
            this.numericBoxSpeed.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxSpeed.Value = 30D;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonTopLeft, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonLeft, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonBottomLeft, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonBottom, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonBottomRight, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonTop, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonTopRight, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonRight, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 64);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(156, 91);
            this.tableLayoutPanel1.TabIndex = 85;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonAntiClock, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonClock, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(39, 30);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(78, 30);
            this.tableLayoutPanel2.TabIndex = 85;
            // 
            // buttonAntiClock
            // 
            this.buttonAntiClock.BackColor = System.Drawing.SystemColors.Control;
            this.buttonAntiClock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAntiClock.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAntiClock.ForeColor = System.Drawing.Color.Gray;
            this.buttonAntiClock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAntiClock.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAntiClock.Location = new System.Drawing.Point(39, 0);
            this.buttonAntiClock.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAntiClock.Name = "buttonAntiClock";
            this.buttonAntiClock.Size = new System.Drawing.Size(39, 30);
            this.buttonAntiClock.TabIndex = 1;
            this.buttonAntiClock.Text = "⭯";
            this.buttonAntiClock.UseVisualStyleBackColor = false;
            this.buttonAntiClock.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonClock
            // 
            this.buttonClock.BackColor = System.Drawing.SystemColors.Control;
            this.buttonClock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonClock.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonClock.ForeColor = System.Drawing.Color.Gray;
            this.buttonClock.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonClock.Location = new System.Drawing.Point(0, 0);
            this.buttonClock.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClock.Name = "buttonClock";
            this.buttonClock.Size = new System.Drawing.Size(39, 30);
            this.buttonClock.TabIndex = 0;
            this.buttonClock.Text = "⭮";
            this.buttonClock.UseVisualStyleBackColor = false;
            this.buttonClock.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonTopLeft
            // 
            this.buttonTopLeft.BackColor = System.Drawing.SystemColors.Control;
            this.buttonTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTopLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonTopLeft.ForeColor = System.Drawing.Color.Gray;
            this.buttonTopLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonTopLeft.Location = new System.Drawing.Point(0, 0);
            this.buttonTopLeft.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTopLeft.Name = "buttonTopLeft";
            this.buttonTopLeft.Size = new System.Drawing.Size(39, 30);
            this.buttonTopLeft.TabIndex = 0;
            this.buttonTopLeft.Text = "⭦";
            this.buttonTopLeft.UseVisualStyleBackColor = false;
            this.buttonTopLeft.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.BackColor = System.Drawing.SystemColors.Control;
            this.buttonLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonLeft.ForeColor = System.Drawing.Color.Gray;
            this.buttonLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonLeft.Location = new System.Drawing.Point(0, 30);
            this.buttonLeft.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(39, 30);
            this.buttonLeft.TabIndex = 1;
            this.buttonLeft.Text = "⭠";
            this.buttonLeft.UseVisualStyleBackColor = false;
            this.buttonLeft.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonBottomLeft
            // 
            this.buttonBottomLeft.BackColor = System.Drawing.SystemColors.Control;
            this.buttonBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBottomLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonBottomLeft.ForeColor = System.Drawing.Color.Gray;
            this.buttonBottomLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBottomLeft.Location = new System.Drawing.Point(0, 60);
            this.buttonBottomLeft.Margin = new System.Windows.Forms.Padding(0);
            this.buttonBottomLeft.Name = "buttonBottomLeft";
            this.buttonBottomLeft.Size = new System.Drawing.Size(39, 31);
            this.buttonBottomLeft.TabIndex = 2;
            this.buttonBottomLeft.Text = "⭩";
            this.buttonBottomLeft.UseVisualStyleBackColor = false;
            this.buttonBottomLeft.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonBottom
            // 
            this.buttonBottom.BackColor = System.Drawing.SystemColors.Control;
            this.buttonBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBottom.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonBottom.ForeColor = System.Drawing.Color.Gray;
            this.buttonBottom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBottom.Location = new System.Drawing.Point(39, 60);
            this.buttonBottom.Margin = new System.Windows.Forms.Padding(0);
            this.buttonBottom.Name = "buttonBottom";
            this.buttonBottom.Size = new System.Drawing.Size(78, 31);
            this.buttonBottom.TabIndex = 4;
            this.buttonBottom.Text = "⭣";
            this.buttonBottom.UseVisualStyleBackColor = false;
            this.buttonBottom.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonBottomRight
            // 
            this.buttonBottomRight.BackColor = System.Drawing.SystemColors.Control;
            this.buttonBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBottomRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonBottomRight.ForeColor = System.Drawing.Color.Gray;
            this.buttonBottomRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBottomRight.Location = new System.Drawing.Point(117, 60);
            this.buttonBottomRight.Margin = new System.Windows.Forms.Padding(0);
            this.buttonBottomRight.Name = "buttonBottomRight";
            this.buttonBottomRight.Size = new System.Drawing.Size(39, 31);
            this.buttonBottomRight.TabIndex = 7;
            this.buttonBottomRight.Text = "⭨";
            this.buttonBottomRight.UseVisualStyleBackColor = false;
            this.buttonBottomRight.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonTop
            // 
            this.buttonTop.BackColor = System.Drawing.SystemColors.Control;
            this.buttonTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTop.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonTop.ForeColor = System.Drawing.Color.Gray;
            this.buttonTop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonTop.Location = new System.Drawing.Point(39, 0);
            this.buttonTop.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTop.Name = "buttonTop";
            this.buttonTop.Size = new System.Drawing.Size(78, 30);
            this.buttonTop.TabIndex = 3;
            this.buttonTop.Text = "⭡";
            this.buttonTop.UseVisualStyleBackColor = false;
            this.buttonTop.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonTopRight
            // 
            this.buttonTopRight.BackColor = System.Drawing.SystemColors.Control;
            this.buttonTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTopRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonTopRight.ForeColor = System.Drawing.Color.Gray;
            this.buttonTopRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonTopRight.Location = new System.Drawing.Point(117, 0);
            this.buttonTopRight.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTopRight.Name = "buttonTopRight";
            this.buttonTopRight.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.buttonTopRight.Size = new System.Drawing.Size(39, 30);
            this.buttonTopRight.TabIndex = 5;
            this.buttonTopRight.Text = "⭧";
            this.buttonTopRight.UseVisualStyleBackColor = false;
            this.buttonTopRight.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRight.ForeColor = System.Drawing.Color.Black;
            this.buttonRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonRight.Location = new System.Drawing.Point(117, 30);
            this.buttonRight.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(39, 30);
            this.buttonRight.TabIndex = 6;
            this.buttonRight.Text = "⭢";
            this.buttonRight.UseVisualStyleBackColor = false;
            this.buttonRight.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // numericBoxDuration
            // 
            this.numericBoxDuration.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxDuration.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDuration.FooterText = "sec";
            this.numericBoxDuration.HeaderText = "Duration";
            this.numericBoxDuration.Location = new System.Drawing.Point(9, 34);
            this.numericBoxDuration.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDuration.Maximum = 360D;
            this.numericBoxDuration.MaximumSize = new System.Drawing.Size(1000, 27);
            this.numericBoxDuration.Minimum = 1D;
            this.numericBoxDuration.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDuration.Name = "numericBoxDuration";
            this.numericBoxDuration.RadianValue = 0.20943951023931953D;
            this.numericBoxDuration.RoundErrorAccuracy = -1;
            this.numericBoxDuration.ShowUpDown = true;
            this.numericBoxDuration.Size = new System.Drawing.Size(156, 27);
            this.numericBoxDuration.SmartIncrement = true;
            this.numericBoxDuration.TabIndex = 1;
            this.numericBoxDuration.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDuration.Value = 12D;
            // 
            // FormMovieSetting
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(172, 188);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.numericBoxDuration);
            this.Controls.Add(this.numericBoxSpeed);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMovieSetting";
            this.ShowIcon = false;
            this.Text = "Movie setting";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private NumericBox numericBoxSpeed;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Button buttonAntiClock;
    private System.Windows.Forms.Button buttonClock;
    private System.Windows.Forms.Button buttonTopLeft;
    private System.Windows.Forms.Button buttonLeft;
    private System.Windows.Forms.Button buttonBottomLeft;
    private System.Windows.Forms.Button buttonBottom;
    private System.Windows.Forms.Button buttonBottomRight;
    private System.Windows.Forms.Button buttonTop;
    private System.Windows.Forms.Button buttonTopRight;
    private System.Windows.Forms.Button buttonRight;
    private NumericBox numericBoxDuration;
}
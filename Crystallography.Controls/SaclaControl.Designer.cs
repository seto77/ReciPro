namespace Crystallography.Controls
{
    partial class SaclaControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericBoxPixelWidth = new Crystallography.Controls.NumericBox();
            this.numericBoxPixelHeight = new Crystallography.Controls.NumericBox();
            this.numericBoxPixelSize = new Crystallography.Controls.NumericBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericalTextBoxFootY = new Crystallography.Controls.NumericBox();
            this.numericBoxFootX = new Crystallography.Controls.NumericBox();
            this.numericBoxDistance = new Crystallography.Controls.NumericBox();
            this.numericBoxTau = new Crystallography.Controls.NumericBox();
            this.numericBoxPhi = new Crystallography.Controls.NumericBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label3.Location = new System.Drawing.Point(6, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Foot";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericBoxPixelWidth);
            this.groupBox1.Controls.Add(this.numericBoxPixelHeight);
            this.groupBox1.Controls.Add(this.numericBoxPixelSize);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detector property";
            // 
            // numericalTextBoxPixelWidth
            // 
            this.numericBoxPixelWidth.AllowMouseControl = false;
            this.numericBoxPixelWidth.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxPixelWidth.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelWidth.DecimalPlaces = -1;
            this.numericBoxPixelWidth.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelWidth.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelWidth.FooterText = "pixel";
            this.numericBoxPixelWidth.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelWidth.HeaderText = "Width";
            this.numericBoxPixelWidth.Location = new System.Drawing.Point(7, 21);
            this.numericBoxPixelWidth.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelWidth.Maximum = double.PositiveInfinity;
            this.numericBoxPixelWidth.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPixelWidth.Minimum = double.NegativeInfinity;
            this.numericBoxPixelWidth.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPixelWidth.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxPixelWidth.MouseSpeed = 1D;
            this.numericBoxPixelWidth.Multiline = false;
            this.numericBoxPixelWidth.Name = "numericalTextBoxPixelWidth";
            this.numericBoxPixelWidth.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelWidth.RadianValue = 17.872171540421935D;
            this.numericBoxPixelWidth.ReadOnly = false;
            this.numericBoxPixelWidth.RestrictLimitValue = true;
            this.numericBoxPixelWidth.ShowFraction = false;
            this.numericBoxPixelWidth.ShowPositiveSign = false;
            this.numericBoxPixelWidth.ShowUpDown = false;
            this.numericBoxPixelWidth.Size = new System.Drawing.Size(122, 25);
            this.numericBoxPixelWidth.SkipEventDuringInput = false;
            this.numericBoxPixelWidth.SmartIncrement = true;
            this.numericBoxPixelWidth.TabIndex = 0;
            this.numericBoxPixelWidth.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxPixelWidth.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxPixelWidth.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelWidth.ThonsandsSeparator = true;
            this.numericBoxPixelWidth.ToolTip = "";
            this.numericBoxPixelWidth.UpDown_Increment = 1D;
            this.numericBoxPixelWidth.Value = 1024D;
            this.numericBoxPixelWidth.WordWrap = true;
            this.numericBoxPixelWidth.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxPixelWidth_ValueChanged);
            // 
            // numericalTextBoxPixelHeight
            // 
            this.numericBoxPixelHeight.AllowMouseControl = false;
            this.numericBoxPixelHeight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxPixelHeight.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelHeight.DecimalPlaces = -1;
            this.numericBoxPixelHeight.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelHeight.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelHeight.FooterText = "pixel";
            this.numericBoxPixelHeight.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelHeight.HeaderText = "Height";
            this.numericBoxPixelHeight.Location = new System.Drawing.Point(7, 46);
            this.numericBoxPixelHeight.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelHeight.Maximum = double.PositiveInfinity;
            this.numericBoxPixelHeight.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPixelHeight.Minimum = double.NegativeInfinity;
            this.numericBoxPixelHeight.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPixelHeight.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxPixelHeight.MouseSpeed = 1D;
            this.numericBoxPixelHeight.Multiline = false;
            this.numericBoxPixelHeight.Name = "numericalTextBoxPixelHeight";
            this.numericBoxPixelHeight.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelHeight.RadianValue = 17.872171540421935D;
            this.numericBoxPixelHeight.ReadOnly = false;
            this.numericBoxPixelHeight.RestrictLimitValue = true;
            this.numericBoxPixelHeight.ShowFraction = false;
            this.numericBoxPixelHeight.ShowPositiveSign = false;
            this.numericBoxPixelHeight.ShowUpDown = false;
            this.numericBoxPixelHeight.Size = new System.Drawing.Size(122, 25);
            this.numericBoxPixelHeight.SkipEventDuringInput = false;
            this.numericBoxPixelHeight.SmartIncrement = true;
            this.numericBoxPixelHeight.TabIndex = 0;
            this.numericBoxPixelHeight.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxPixelHeight.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxPixelHeight.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelHeight.ThonsandsSeparator = true;
            this.numericBoxPixelHeight.ToolTip = "";
            this.numericBoxPixelHeight.UpDown_Increment = 1D;
            this.numericBoxPixelHeight.Value = 1024D;
            this.numericBoxPixelHeight.WordWrap = true;
            this.numericBoxPixelHeight.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxPixelWidth_ValueChanged);
            // 
            // numericalTextBoxPixelSize
            // 
            this.numericBoxPixelSize.AllowMouseControl = false;
            this.numericBoxPixelSize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxPixelSize.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelSize.DecimalPlaces = -1;
            this.numericBoxPixelSize.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelSize.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelSize.FooterText = "μm";
            this.numericBoxPixelSize.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelSize.HeaderText = "Pix. size";
            this.numericBoxPixelSize.Location = new System.Drawing.Point(7, 71);
            this.numericBoxPixelSize.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelSize.Maximum = double.PositiveInfinity;
            this.numericBoxPixelSize.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPixelSize.Minimum = double.NegativeInfinity;
            this.numericBoxPixelSize.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPixelSize.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxPixelSize.MouseSpeed = 1D;
            this.numericBoxPixelSize.Multiline = false;
            this.numericBoxPixelSize.Name = "numericalTextBoxPixelSize";
            this.numericBoxPixelSize.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelSize.RadianValue = 0.0008726646259971648D;
            this.numericBoxPixelSize.ReadOnly = false;
            this.numericBoxPixelSize.RestrictLimitValue = true;
            this.numericBoxPixelSize.ShowFraction = false;
            this.numericBoxPixelSize.ShowPositiveSign = false;
            this.numericBoxPixelSize.ShowUpDown = false;
            this.numericBoxPixelSize.Size = new System.Drawing.Size(122, 25);
            this.numericBoxPixelSize.SkipEventDuringInput = false;
            this.numericBoxPixelSize.SmartIncrement = true;
            this.numericBoxPixelSize.TabIndex = 0;
            this.numericBoxPixelSize.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxPixelSize.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxPixelSize.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelSize.ThonsandsSeparator = true;
            this.numericBoxPixelSize.ToolTip = "";
            this.numericBoxPixelSize.UpDown_Increment = 1D;
            this.numericBoxPixelSize.Value = 0.05D;
            this.numericBoxPixelSize.WordWrap = true;
            this.numericBoxPixelSize.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxPixelWidth_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericalTextBoxFootY);
            this.groupBox2.Controls.Add(this.numericBoxFootX);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericBoxDistance);
            this.groupBox2.Controls.Add(this.numericBoxPhi);
            this.groupBox2.Controls.Add(this.numericBoxTau);
            this.groupBox2.Location = new System.Drawing.Point(137, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Optical property";
            // 
            // numericalTextBoxFootY
            // 
            this.numericalTextBoxFootY.AllowMouseControl = false;
            this.numericalTextBoxFootY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericalTextBoxFootY.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxFootY.DecimalPlaces = -1;
            this.numericalTextBoxFootY.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootY.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootY.FooterText = "pix";
            this.numericalTextBoxFootY.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootY.HeaderText = "y";
            this.numericalTextBoxFootY.Location = new System.Drawing.Point(147, 72);
            this.numericalTextBoxFootY.Margin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxFootY.Maximum = double.PositiveInfinity;
            this.numericalTextBoxFootY.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericalTextBoxFootY.Minimum = double.NegativeInfinity;
            this.numericalTextBoxFootY.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericalTextBoxFootY.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericalTextBoxFootY.MouseSpeed = 1D;
            this.numericalTextBoxFootY.Multiline = false;
            this.numericalTextBoxFootY.Name = "numericalTextBoxFootY";
            this.numericalTextBoxFootY.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericalTextBoxFootY.RadianValue = 8.9360857702109673D;
            this.numericalTextBoxFootY.ReadOnly = false;
            this.numericalTextBoxFootY.RestrictLimitValue = true;
            this.numericalTextBoxFootY.ShowFraction = false;
            this.numericalTextBoxFootY.ShowPositiveSign = false;
            this.numericalTextBoxFootY.ShowUpDown = false;
            this.numericalTextBoxFootY.Size = new System.Drawing.Size(88, 25);
            this.numericalTextBoxFootY.SkipEventDuringInput = false;
            this.numericalTextBoxFootY.SmartIncrement = true;
            this.numericalTextBoxFootY.TabIndex = 0;
            this.numericalTextBoxFootY.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxFootY.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxFootY.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootY.ThonsandsSeparator = true;
            this.numericalTextBoxFootY.ToolTip = "";
            this.numericalTextBoxFootY.UpDown_Increment = 1D;
            this.numericalTextBoxFootY.Value = 512D;
            this.numericalTextBoxFootY.WordWrap = true;
            this.numericalTextBoxFootY.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxPixelWidth_ValueChanged);
            // 
            // numericalTextBoxFootX
            // 
            this.numericBoxFootX.AllowMouseControl = false;
            this.numericBoxFootX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxFootX.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootX.DecimalPlaces = -1;
            this.numericBoxFootX.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxFootX.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxFootX.FooterText = "pix";
            this.numericBoxFootX.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxFootX.HeaderText = "x";
            this.numericBoxFootX.Location = new System.Drawing.Point(53, 72);
            this.numericBoxFootX.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxFootX.Maximum = double.PositiveInfinity;
            this.numericBoxFootX.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxFootX.Minimum = double.NegativeInfinity;
            this.numericBoxFootX.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxFootX.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxFootX.MouseSpeed = 1D;
            this.numericBoxFootX.Multiline = false;
            this.numericBoxFootX.Name = "numericalTextBoxFootX";
            this.numericBoxFootX.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxFootX.RadianValue = 8.9360857702109673D;
            this.numericBoxFootX.ReadOnly = false;
            this.numericBoxFootX.RestrictLimitValue = true;
            this.numericBoxFootX.ShowFraction = false;
            this.numericBoxFootX.ShowPositiveSign = false;
            this.numericBoxFootX.ShowUpDown = false;
            this.numericBoxFootX.Size = new System.Drawing.Size(88, 25);
            this.numericBoxFootX.SkipEventDuringInput = false;
            this.numericBoxFootX.SmartIncrement = true;
            this.numericBoxFootX.TabIndex = 0;
            this.numericBoxFootX.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxFootX.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxFootX.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxFootX.ThonsandsSeparator = true;
            this.numericBoxFootX.ToolTip = "";
            this.numericBoxFootX.UpDown_Increment = 1D;
            this.numericBoxFootX.Value = 512D;
            this.numericBoxFootX.WordWrap = true;
            this.numericBoxFootX.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxPixelWidth_ValueChanged);
            // 
            // numericalTextBoxDistance
            // 
            this.numericBoxDistance.AllowMouseControl = false;
            this.numericBoxDistance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDistance.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.DecimalPlaces = -1;
            this.numericBoxDistance.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.FooterText = "mm";
            this.numericBoxDistance.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.HeaderText = "Cameralength 2";
            this.numericBoxDistance.Location = new System.Drawing.Point(3, 21);
            this.numericBoxDistance.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDistance.Maximum = double.PositiveInfinity;
            this.numericBoxDistance.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDistance.Minimum = double.NegativeInfinity;
            this.numericBoxDistance.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDistance.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxDistance.MouseSpeed = 1D;
            this.numericBoxDistance.Multiline = false;
            this.numericBoxDistance.Name = "numericalTextBoxDistance";
            this.numericBoxDistance.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDistance.RadianValue = 5.2359877559829888D;
            this.numericBoxDistance.ReadOnly = false;
            this.numericBoxDistance.RestrictLimitValue = true;
            this.numericBoxDistance.ShowFraction = false;
            this.numericBoxDistance.ShowPositiveSign = false;
            this.numericBoxDistance.ShowUpDown = false;
            this.numericBoxDistance.Size = new System.Drawing.Size(187, 25);
            this.numericBoxDistance.SkipEventDuringInput = false;
            this.numericBoxDistance.SmartIncrement = true;
            this.numericBoxDistance.TabIndex = 0;
            this.numericBoxDistance.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDistance.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDistance.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.ThonsandsSeparator = true;
            this.numericBoxDistance.ToolTip = "";
            this.numericBoxDistance.UpDown_Increment = 1D;
            this.numericBoxDistance.Value = 300D;
            this.numericBoxDistance.WordWrap = true;
            this.numericBoxDistance.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxPixelWidth_ValueChanged);
            // 
            // numericalTextBoxTau
            // 
            this.numericBoxTau.AllowMouseControl = false;
            this.numericBoxTau.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxTau.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTau.DecimalPlaces = -1;
            this.numericBoxTau.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxTau.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxTau.FooterText = "°";
            this.numericBoxTau.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxTau.HeaderText = "τ";
            this.numericBoxTau.Location = new System.Drawing.Point(9, 46);
            this.numericBoxTau.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxTau.Maximum = 90D;
            this.numericBoxTau.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxTau.Minimum = -90D;
            this.numericBoxTau.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxTau.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxTau.MouseSpeed = 1D;
            this.numericBoxTau.Multiline = false;
            this.numericBoxTau.Name = "numericalTextBoxTau";
            this.numericBoxTau.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxTau.RadianValue = 0.3490658503988659D;
            this.numericBoxTau.ReadOnly = false;
            this.numericBoxTau.RestrictLimitValue = true;
            this.numericBoxTau.ShowFraction = false;
            this.numericBoxTau.ShowPositiveSign = false;
            this.numericBoxTau.ShowUpDown = false;
            this.numericBoxTau.Size = new System.Drawing.Size(83, 25);
            this.numericBoxTau.SkipEventDuringInput = false;
            this.numericBoxTau.SmartIncrement = true;
            this.numericBoxTau.TabIndex = 0;
            this.numericBoxTau.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxTau.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxTau.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxTau.ThonsandsSeparator = true;
            this.numericBoxTau.ToolTip = "";
            this.numericBoxTau.UpDown_Increment = 1D;
            this.numericBoxTau.Value = 20D;
            this.numericBoxTau.WordWrap = true;
            this.numericBoxTau.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxPixelWidth_ValueChanged);
            // 
            // numericBoxPhi
            // 
            this.numericBoxPhi.AllowMouseControl = false;
            this.numericBoxPhi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.DecimalPlaces = -1;
            this.numericBoxPhi.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPhi.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPhi.FooterText = "°";
            this.numericBoxPhi.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPhi.HeaderText = "φ";
            this.numericBoxPhi.Location = new System.Drawing.Point(107, 47);
            this.numericBoxPhi.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPhi.Maximum = 360D;
            this.numericBoxPhi.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPhi.Minimum = -360D;
            this.numericBoxPhi.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPhi.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxPhi.MouseSpeed = 1D;
            this.numericBoxPhi.Multiline = false;
            this.numericBoxPhi.Name = "numericBoxPhi";
            this.numericBoxPhi.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPhi.RadianValue = 0D;
            this.numericBoxPhi.ReadOnly = false;
            this.numericBoxPhi.RestrictLimitValue = true;
            this.numericBoxPhi.ShowFraction = false;
            this.numericBoxPhi.ShowPositiveSign = false;
            this.numericBoxPhi.ShowUpDown = false;
            this.numericBoxPhi.Size = new System.Drawing.Size(83, 25);
            this.numericBoxPhi.SkipEventDuringInput = false;
            this.numericBoxPhi.SmartIncrement = true;
            this.numericBoxPhi.TabIndex = 0;
            this.numericBoxPhi.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxPhi.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxPhi.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPhi.ThonsandsSeparator = true;
            this.numericBoxPhi.ToolTip = "";
            this.numericBoxPhi.UpDown_Increment = 1D;
            this.numericBoxPhi.Value = 0D;
            this.numericBoxPhi.WordWrap = true;
            this.numericBoxPhi.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxPixelWidth_ValueChanged);
            // 
            // SaclaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SaclaControl";
            this.Size = new System.Drawing.Size(375, 103);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NumericBox numericBoxPixelWidth;
        private NumericBox numericBoxPixelHeight;
        private NumericBox numericBoxPixelSize;
        private NumericBox numericBoxTau;
        private NumericBox numericBoxFootX;
        private NumericBox numericalTextBoxFootY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private NumericBox numericBoxDistance;
        private NumericBox numericBoxPhi;
    }
}

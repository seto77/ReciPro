namespace Crystallography.Controls
{
    partial class ScalablePictureBoxAdvanced
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScalablePictureBoxAdvanced));
            this.panelTrackBar = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarAdvancedMinimum = new Crystallography.Controls.TrackBarAdvanced();
            this.trackBarAdvancedMaximum = new Crystallography.Controls.TrackBarAdvanced();
            this.label = new System.Windows.Forms.Label();
            this.comboBoxScale2 = new System.Windows.Forms.ComboBox();
            this.comboBoxScale1 = new System.Windows.Forms.ComboBox();
            this.comboBoxGradient = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanelGradient = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelImageFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelDustScratches = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxDustScratches = new System.Windows.Forms.CheckBox();
            this.numericBoxDustScratchesRadius = new Crystallography.Controls.NumericBox();
            this.numericBoxDustScratchesThreshold = new Crystallography.Controls.NumericBox();
            this.flowLayoutPanelGaussianBlur2 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxGaussianBlur = new System.Windows.Forms.CheckBox();
            this.numericBoxGaussianFWHM = new Crystallography.Controls.NumericBox();
            this.panelUpper = new System.Windows.Forms.Panel();
            this.buttonCopyToClipBoard = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.scalablePictureBox = new Crystallography.Controls.ScalablePictureBox();
            this.graphControl = new Crystallography.Controls.GraphControl();
            this.panelTrackBar.SuspendLayout();
            this.flowLayoutPanelGradient.SuspendLayout();
            this.flowLayoutPanelImageFilter.SuspendLayout();
            this.flowLayoutPanelDustScratches.SuspendLayout();
            this.flowLayoutPanelGaussianBlur2.SuspendLayout();
            this.panelUpper.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTrackBar
            // 
            resources.ApplyResources(this.panelTrackBar, "panelTrackBar");
            this.panelTrackBar.Controls.Add(this.label2);
            this.panelTrackBar.Controls.Add(this.label3);
            this.panelTrackBar.Controls.Add(this.trackBarAdvancedMinimum);
            this.panelTrackBar.Controls.Add(this.trackBarAdvancedMaximum);
            this.panelTrackBar.Name = "panelTrackBar";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // trackBarAdvancedMinimum
            // 
            resources.ApplyResources(this.trackBarAdvancedMinimum, "trackBarAdvancedMinimum");
            this.trackBarAdvancedMinimum.ControlHeight = 22;
            this.trackBarAdvancedMinimum.DecimalPlaces = 0;
            this.trackBarAdvancedMinimum.LogScrollBar = false;
            this.trackBarAdvancedMinimum.Maximum = 65D;
            this.trackBarAdvancedMinimum.Minimum = 0D;
            this.trackBarAdvancedMinimum.Name = "trackBarAdvancedMinimum";
            this.trackBarAdvancedMinimum.NumericBoxSize = 84;
            this.trackBarAdvancedMinimum.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAdvancedMinimum.Smart_Increment = true;
            this.trackBarAdvancedMinimum.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.trackBarAdvancedMinimum.UpDown_Increment = 1D;
            this.trackBarAdvancedMinimum.Value = 0D;
            this.trackBarAdvancedMinimum.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(this.trackBarAdvancedMinimum_ValueChanged);
            // 
            // trackBarAdvancedMaximum
            // 
            resources.ApplyResources(this.trackBarAdvancedMaximum, "trackBarAdvancedMaximum");
            this.trackBarAdvancedMaximum.ControlHeight = 22;
            this.trackBarAdvancedMaximum.DecimalPlaces = 0;
            this.trackBarAdvancedMaximum.LogScrollBar = false;
            this.trackBarAdvancedMaximum.Maximum = 65535D;
            this.trackBarAdvancedMaximum.Minimum = 0D;
            this.trackBarAdvancedMaximum.Name = "trackBarAdvancedMaximum";
            this.trackBarAdvancedMaximum.NumericBoxSize = 84;
            this.trackBarAdvancedMaximum.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAdvancedMaximum.Smart_Increment = true;
            this.trackBarAdvancedMaximum.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.trackBarAdvancedMaximum.UpDown_Increment = 1D;
            this.trackBarAdvancedMaximum.Value = 0D;
            this.trackBarAdvancedMaximum.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(this.trackBarAdvancedMaximum_ValueChanged);
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            // 
            // comboBoxScale2
            // 
            this.comboBoxScale2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScale2.FormattingEnabled = true;
            this.comboBoxScale2.Items.AddRange(new object[] {
            resources.GetString("comboBoxScale2.Items"),
            resources.GetString("comboBoxScale2.Items1"),
            resources.GetString("comboBoxScale2.Items2"),
            resources.GetString("comboBoxScale2.Items3")});
            resources.ApplyResources(this.comboBoxScale2, "comboBoxScale2");
            this.comboBoxScale2.Name = "comboBoxScale2";
            this.comboBoxScale2.SelectedIndexChanged += new System.EventHandler(this.comboBoxScale_SelectedIndexChanged);
            // 
            // comboBoxScale1
            // 
            this.comboBoxScale1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScale1.FormattingEnabled = true;
            this.comboBoxScale1.Items.AddRange(new object[] {
            resources.GetString("comboBoxScale1.Items"),
            resources.GetString("comboBoxScale1.Items1")});
            resources.ApplyResources(this.comboBoxScale1, "comboBoxScale1");
            this.comboBoxScale1.Name = "comboBoxScale1";
            this.comboBoxScale1.SelectedIndexChanged += new System.EventHandler(this.comboBoxScale_SelectedIndexChanged);
            // 
            // comboBoxGradient
            // 
            this.comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGradient.FormattingEnabled = true;
            this.comboBoxGradient.Items.AddRange(new object[] {
            resources.GetString("comboBoxGradient.Items"),
            resources.GetString("comboBoxGradient.Items1")});
            resources.ApplyResources(this.comboBoxGradient, "comboBoxGradient");
            this.comboBoxGradient.Name = "comboBoxGradient";
            this.comboBoxGradient.SelectedIndexChanged += new System.EventHandler(this.comboBoxScale_SelectedIndexChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // flowLayoutPanelGradient
            // 
            resources.ApplyResources(this.flowLayoutPanelGradient, "flowLayoutPanelGradient");
            this.flowLayoutPanelGradient.Controls.Add(this.label5);
            this.flowLayoutPanelGradient.Controls.Add(this.comboBoxGradient);
            this.flowLayoutPanelGradient.Controls.Add(this.label7);
            this.flowLayoutPanelGradient.Controls.Add(this.comboBoxScale1);
            this.flowLayoutPanelGradient.Controls.Add(this.label9);
            this.flowLayoutPanelGradient.Controls.Add(this.comboBoxScale2);
            this.flowLayoutPanelGradient.Name = "flowLayoutPanelGradient";
            // 
            // flowLayoutPanelImageFilter
            // 
            resources.ApplyResources(this.flowLayoutPanelImageFilter, "flowLayoutPanelImageFilter");
            this.flowLayoutPanelImageFilter.Controls.Add(this.flowLayoutPanelDustScratches);
            this.flowLayoutPanelImageFilter.Controls.Add(this.flowLayoutPanelGaussianBlur2);
            this.flowLayoutPanelImageFilter.Name = "flowLayoutPanelImageFilter";
            // 
            // flowLayoutPanelDustScratches
            // 
            resources.ApplyResources(this.flowLayoutPanelDustScratches, "flowLayoutPanelDustScratches");
            this.flowLayoutPanelDustScratches.Controls.Add(this.checkBoxDustScratches);
            this.flowLayoutPanelDustScratches.Controls.Add(this.numericBoxDustScratchesRadius);
            this.flowLayoutPanelDustScratches.Controls.Add(this.numericBoxDustScratchesThreshold);
            this.flowLayoutPanelDustScratches.Name = "flowLayoutPanelDustScratches";
            // 
            // checkBoxDustScratches
            // 
            resources.ApplyResources(this.checkBoxDustScratches, "checkBoxDustScratches");
            this.checkBoxDustScratches.Name = "checkBoxDustScratches";
            this.checkBoxDustScratches.UseVisualStyleBackColor = true;
            this.checkBoxDustScratches.CheckedChanged += new System.EventHandler(this.imageFilterProperty_Changed);
            // 
            // numericBoxDustScratchesRadius
            // 
            this.numericBoxDustScratchesRadius.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDustScratchesRadius.DecimalPlaces = 1;
            resources.ApplyResources(this.numericBoxDustScratchesRadius, "numericBoxDustScratchesRadius");
            this.numericBoxDustScratchesRadius.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDustScratchesRadius.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDustScratchesRadius.Maximum = 5D;
            this.numericBoxDustScratchesRadius.Minimum = 0D;
            this.numericBoxDustScratchesRadius.Name = "numericBoxDustScratchesRadius";
            this.numericBoxDustScratchesRadius.RadianValue = 0.017453292519943295D;
            this.numericBoxDustScratchesRadius.RoundErrorAccuracy = -1;
            this.numericBoxDustScratchesRadius.ShowUpDown = true;
            this.numericBoxDustScratchesRadius.SkipEventDuringInput = false;
            this.numericBoxDustScratchesRadius.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDustScratchesRadius.ThonsandsSeparator = true;
            this.numericBoxDustScratchesRadius.UpDown_Increment = 0.5D;
            this.numericBoxDustScratchesRadius.Value = 1D;
            this.numericBoxDustScratchesRadius.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.imageFilterProperty_Changed);
            // 
            // numericBoxDustScratchesThreshold
            // 
            this.numericBoxDustScratchesThreshold.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDustScratchesThreshold.DecimalPlaces = 0;
            resources.ApplyResources(this.numericBoxDustScratchesThreshold, "numericBoxDustScratchesThreshold");
            this.numericBoxDustScratchesThreshold.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDustScratchesThreshold.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDustScratchesThreshold.Maximum = 10D;
            this.numericBoxDustScratchesThreshold.Minimum = 0D;
            this.numericBoxDustScratchesThreshold.Name = "numericBoxDustScratchesThreshold";
            this.numericBoxDustScratchesThreshold.RadianValue = 0.052359877559829883D;
            this.numericBoxDustScratchesThreshold.RoundErrorAccuracy = -1;
            this.numericBoxDustScratchesThreshold.ShowUpDown = true;
            this.numericBoxDustScratchesThreshold.SkipEventDuringInput = false;
            this.numericBoxDustScratchesThreshold.SmartIncrement = true;
            this.numericBoxDustScratchesThreshold.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDustScratchesThreshold.ThonsandsSeparator = true;
            this.numericBoxDustScratchesThreshold.Value = 3D;
            this.numericBoxDustScratchesThreshold.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.imageFilterProperty_Changed);
            // 
            // flowLayoutPanelGaussianBlur2
            // 
            resources.ApplyResources(this.flowLayoutPanelGaussianBlur2, "flowLayoutPanelGaussianBlur2");
            this.flowLayoutPanelGaussianBlur2.Controls.Add(this.checkBoxGaussianBlur);
            this.flowLayoutPanelGaussianBlur2.Controls.Add(this.numericBoxGaussianFWHM);
            this.flowLayoutPanelGaussianBlur2.Name = "flowLayoutPanelGaussianBlur2";
            // 
            // checkBoxGaussianBlur
            // 
            resources.ApplyResources(this.checkBoxGaussianBlur, "checkBoxGaussianBlur");
            this.checkBoxGaussianBlur.Name = "checkBoxGaussianBlur";
            this.checkBoxGaussianBlur.UseVisualStyleBackColor = true;
            this.checkBoxGaussianBlur.CheckedChanged += new System.EventHandler(this.imageFilterProperty_Changed);
            // 
            // numericBoxGaussianFWHM
            // 
            this.numericBoxGaussianFWHM.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGaussianFWHM.DecimalPlaces = 1;
            resources.ApplyResources(this.numericBoxGaussianFWHM, "numericBoxGaussianFWHM");
            this.numericBoxGaussianFWHM.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGaussianFWHM.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGaussianFWHM.Maximum = 100D;
            this.numericBoxGaussianFWHM.Minimum = 0D;
            this.numericBoxGaussianFWHM.Name = "numericBoxGaussianFWHM";
            this.numericBoxGaussianFWHM.RadianValue = 0.017453292519943295D;
            this.numericBoxGaussianFWHM.RoundErrorAccuracy = -1;
            this.numericBoxGaussianFWHM.ShowUpDown = true;
            this.numericBoxGaussianFWHM.SkipEventDuringInput = false;
            this.numericBoxGaussianFWHM.SmartIncrement = true;
            this.numericBoxGaussianFWHM.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxGaussianFWHM.ThonsandsSeparator = true;
            this.numericBoxGaussianFWHM.Value = 1D;
            this.numericBoxGaussianFWHM.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.imageFilterProperty_Changed);
            // 
            // panelUpper
            // 
            this.panelUpper.Controls.Add(this.buttonCopyToClipBoard);
            this.panelUpper.Controls.Add(this.label);
            resources.ApplyResources(this.panelUpper, "panelUpper");
            this.panelUpper.Name = "panelUpper";
            // 
            // buttonCopyToClipBoard
            // 
            resources.ApplyResources(this.buttonCopyToClipBoard, "buttonCopyToClipBoard");
            this.buttonCopyToClipBoard.Name = "buttonCopyToClipBoard";
            this.buttonCopyToClipBoard.UseVisualStyleBackColor = true;
            this.buttonCopyToClipBoard.Click += new System.EventHandler(this.buttonCopyToClipBoard_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // scalablePictureBox
            // 
            resources.ApplyResources(this.scalablePictureBox, "scalablePictureBox");
            this.scalablePictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.scalablePictureBox.FixZoomAndCenter = false;
            this.scalablePictureBox.FocusEventEnabled = false;
            this.scalablePictureBox.HorizontalFlip = false;
            this.scalablePictureBox.ManualSpotMode = false;
            this.scalablePictureBox.MouseScaling = true;
            this.scalablePictureBox.MouseTranslation = true;
            this.scalablePictureBox.Name = "scalablePictureBox";
            this.scalablePictureBox.ShowAreaRectangle = false;
            this.scalablePictureBox.ShowRimRentangle = false;
            this.scalablePictureBox.VerticalFlip = false;
            this.scalablePictureBox.Zoom = 128D;
            this.scalablePictureBox.Paint2 += new System.Windows.Forms.PaintEventHandler(this.scalablePictureBox_Paint2);
            this.scalablePictureBox.MouseMove2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox1_MouseMove2);
            this.scalablePictureBox.MouseUp2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox_MouseUp2);
            this.scalablePictureBox.MouseDown2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox_MouseDown2);
            this.scalablePictureBox.MouseWheel2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox_MouseWheel2);
            this.scalablePictureBox.DrawingAreaChanged += new Crystallography.Controls.ScalablePictureBox.DrawingAreaChangedEvent(this.scalablePictureBox_DrawingAreaChanged);
            // 
            // graphControl
            // 
            this.graphControl.AllowMouseOperation = true;
            this.graphControl.BackgroundColor = System.Drawing.Color.White;
            this.graphControl.BottomMargin = 0D;
            this.graphControl.DivisionLineColor = System.Drawing.Color.Gray;
            this.graphControl.DivisionSubLineColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.graphControl, "graphControl");
            this.graphControl.FixRangeHorizontal = false;
            this.graphControl.FixRangeVertical = false;
            this.graphControl.GraphName = "";
            this.graphControl.HorizontalGradiationTextVisivle = true;
            this.graphControl.Interpolation = false;
            this.graphControl.IsIntegerX = false;
            this.graphControl.IsIntegerY = false;
            this.graphControl.LabelX = "X:";
            this.graphControl.LabelY = "Y:";
            this.graphControl.LeftMargin = 0F;
            this.graphControl.LineColor = System.Drawing.Color.Red;
            this.graphControl.LineWidth = 1F;
            this.graphControl.LowerX = 0D;
            this.graphControl.LowerY = 0D;
            this.graphControl.MaximalX = 1D;
            this.graphControl.MaximalY = 1D;
            this.graphControl.MinimalX = 0D;
            this.graphControl.MinimalY = 0D;
            this.graphControl.Mode = Crystallography.Controls.GraphControl.DrawingMode.Histogram;
            this.graphControl.MousePositionVisible = true;
            this.graphControl.Name = "graphControl";
            this.graphControl.OriginPosition = new System.Drawing.Point(20, 20);
            this.graphControl.Smoothing = false;
            this.graphControl.TextFont = new System.Drawing.Font("メイリオ", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.graphControl.UnitX = "";
            this.graphControl.UnitY = "";
            this.graphControl.UpperText = "";
            this.graphControl.UpperTextVisible = false;
            this.graphControl.UpperX = 1D;
            this.graphControl.UpperY = 1D;
            this.graphControl.UseLineWidth = true;
            this.graphControl.VerticalGradiationTextVisivle = true;
            this.graphControl.XLog = false;
            this.graphControl.XScaleLineVisible = true;
            this.graphControl.YLog = true;
            this.graphControl.YScaleLineVisible = true;
            this.graphControl.LinePositionChanged += new Crystallography.Controls.GraphControl.LinePositionChengedEventHandler(this.graphControl_LinePositionChanged);
            // 
            // ScalablePictureBoxAdvanced
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.scalablePictureBox);
            this.Controls.Add(this.panelTrackBar);
            this.Controls.Add(this.graphControl);
            this.Controls.Add(this.flowLayoutPanelGradient);
            this.Controls.Add(this.flowLayoutPanelImageFilter);
            this.Controls.Add(this.panelUpper);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ScalablePictureBoxAdvanced";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ScalablePictureBoxAdvanced_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ScalablePictureBoxAdvanced_DragEnter);
            this.panelTrackBar.ResumeLayout(false);
            this.panelTrackBar.PerformLayout();
            this.flowLayoutPanelGradient.ResumeLayout(false);
            this.flowLayoutPanelGradient.PerformLayout();
            this.flowLayoutPanelImageFilter.ResumeLayout(false);
            this.flowLayoutPanelImageFilter.PerformLayout();
            this.flowLayoutPanelDustScratches.ResumeLayout(false);
            this.flowLayoutPanelDustScratches.PerformLayout();
            this.flowLayoutPanelGaussianBlur2.ResumeLayout(false);
            this.flowLayoutPanelGaussianBlur2.PerformLayout();
            this.panelUpper.ResumeLayout(false);
            this.panelUpper.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScalablePictureBox scalablePictureBox;
        private GraphControl graphControl;
        private TrackBarAdvanced trackBarAdvancedMaximum;
        private System.Windows.Forms.Panel panelTrackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private TrackBarAdvanced trackBarAdvancedMinimum;
        private System.Windows.Forms.Label label;
        public System.Windows.Forms.ComboBox comboBoxScale2;
        public System.Windows.Forms.ComboBox comboBoxScale1;
        public System.Windows.Forms.ComboBox comboBoxGradient;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGradient;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelImageFilter;
        private System.Windows.Forms.CheckBox checkBoxGaussianBlur;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGaussianBlur2;
        private System.Windows.Forms.Panel panelUpper;
        private System.Windows.Forms.Button buttonCopyToClipBoard;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDustScratches;
        private System.Windows.Forms.CheckBox checkBoxDustScratches;
        private NumericBox numericBoxDustScratchesThreshold;
        private NumericBox numericBoxDustScratchesRadius;
        private NumericBox numericBoxGaussianFWHM;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

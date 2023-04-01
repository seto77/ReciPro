namespace ReciPro
{
    partial class FormCTF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCTF));
            flowLayoutPanelSTEM = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonCTF_coherent = new System.Windows.Forms.RadioButton();
            radioButtonCTF_Incoherent = new System.Windows.Forms.RadioButton();
            checkBoxSinW = new System.Windows.Forms.CheckBox();
            checkBoxEs_HRTEM = new System.Windows.Forms.CheckBox();
            checkBoxEc = new System.Windows.Forms.CheckBox();
            checkBoxPCTF = new System.Windows.Forms.CheckBox();
            buttonCopyGraph = new System.Windows.Forms.Button();
            numericBoxMaxU1 = new NumericBox();
            graphControl = new GraphControl();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxCosW = new System.Windows.Forms.CheckBox();
            checkBoxEs_STEM = new System.Windows.Forms.CheckBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxACTF = new System.Windows.Forms.CheckBox();
            pictureBoxA_STEM = new System.Windows.Forms.PictureBox();
            pictureBoxA_HRTEM = new System.Windows.Forms.PictureBox();
            pictureBoxSTEM_CTFI = new System.Windows.Forms.PictureBox();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            panel2 = new System.Windows.Forms.Panel();
            flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelSTEM.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxA_STEM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxA_HRTEM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSTEM_CTFI).BeginInit();
            flowLayoutPanel5.SuspendLayout();
            panel2.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanelSTEM
            // 
            flowLayoutPanelSTEM.AutoSize = true;
            flowLayoutPanelSTEM.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanelSTEM.Controls.Add(radioButtonCTF_coherent);
            flowLayoutPanelSTEM.Controls.Add(radioButtonCTF_Incoherent);
            flowLayoutPanelSTEM.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanelSTEM.Location = new System.Drawing.Point(3, 3);
            flowLayoutPanelSTEM.Name = "flowLayoutPanelSTEM";
            flowLayoutPanelSTEM.Size = new System.Drawing.Size(706, 27);
            flowLayoutPanelSTEM.TabIndex = 60;
            // 
            // radioButtonCTF_coherent
            // 
            radioButtonCTF_coherent.AutoSize = true;
            radioButtonCTF_coherent.Checked = true;
            radioButtonCTF_coherent.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioButtonCTF_coherent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonCTF_coherent.Location = new System.Drawing.Point(3, 3);
            radioButtonCTF_coherent.Name = "radioButtonCTF_coherent";
            radioButtonCTF_coherent.Size = new System.Drawing.Size(130, 21);
            radioButtonCTF_coherent.TabIndex = 53;
            radioButtonCTF_coherent.TabStop = true;
            radioButtonCTF_coherent.Text = "Coherent imaging";
            radioButtonCTF_coherent.UseVisualStyleBackColor = true;
            radioButtonCTF_coherent.CheckedChanged += radioButtonCTF_coherent_CheckedChanged;
            // 
            // radioButtonCTF_Incoherent
            // 
            radioButtonCTF_Incoherent.AutoSize = true;
            radioButtonCTF_Incoherent.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioButtonCTF_Incoherent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonCTF_Incoherent.Location = new System.Drawing.Point(139, 3);
            radioButtonCTF_Incoherent.Name = "radioButtonCTF_Incoherent";
            radioButtonCTF_Incoherent.Size = new System.Drawing.Size(138, 21);
            radioButtonCTF_Incoherent.TabIndex = 53;
            radioButtonCTF_Incoherent.Text = "Incoherent imaging";
            radioButtonCTF_Incoherent.UseVisualStyleBackColor = true;
            // 
            // checkBoxSinW
            // 
            checkBoxSinW.AutoSize = true;
            checkBoxSinW.Checked = true;
            checkBoxSinW.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxSinW.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxSinW.Image = (System.Drawing.Image)resources.GetObject("checkBoxSinW.Image");
            checkBoxSinW.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            checkBoxSinW.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxSinW.Location = new System.Drawing.Point(3, 1);
            checkBoxSinW.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            checkBoxSinW.Name = "checkBoxSinW";
            checkBoxSinW.Size = new System.Drawing.Size(312, 32);
            checkBoxSinW.TabIndex = 5;
            checkBoxSinW.UseVisualStyleBackColor = true;
            checkBoxSinW.CheckedChanged += checkBoxSinW_CheckedChanged;
            // 
            // checkBoxEs_HRTEM
            // 
            checkBoxEs_HRTEM.AutoSize = true;
            checkBoxEs_HRTEM.Checked = true;
            checkBoxEs_HRTEM.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxEs_HRTEM.Font = new System.Drawing.Font("Segoe UI Symbol", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxEs_HRTEM.Image = (System.Drawing.Image)resources.GetObject("checkBoxEs_HRTEM.Image");
            checkBoxEs_HRTEM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxEs_HRTEM.Location = new System.Drawing.Point(201, 35);
            checkBoxEs_HRTEM.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            checkBoxEs_HRTEM.Name = "checkBoxEs_HRTEM";
            checkBoxEs_HRTEM.Size = new System.Drawing.Size(304, 32);
            checkBoxEs_HRTEM.TabIndex = 6;
            checkBoxEs_HRTEM.UseVisualStyleBackColor = true;
            checkBoxEs_HRTEM.CheckedChanged += checkBoxSinW_CheckedChanged;
            // 
            // checkBoxEc
            // 
            checkBoxEc.AutoSize = true;
            checkBoxEc.Checked = true;
            checkBoxEc.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxEc.Font = new System.Drawing.Font("Segoe UI Symbol", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxEc.Image = (System.Drawing.Image)resources.GetObject("checkBoxEc.Image");
            checkBoxEc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxEc.Location = new System.Drawing.Point(3, 69);
            checkBoxEc.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            checkBoxEc.Name = "checkBoxEc";
            checkBoxEc.Size = new System.Drawing.Size(288, 32);
            checkBoxEc.TabIndex = 7;
            checkBoxEc.UseVisualStyleBackColor = true;
            checkBoxEc.CheckedChanged += checkBoxSinW_CheckedChanged;
            // 
            // checkBoxPCTF
            // 
            checkBoxPCTF.AutoSize = true;
            checkBoxPCTF.Checked = true;
            checkBoxPCTF.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxPCTF.Font = new System.Drawing.Font("Segoe UI Symbol", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxPCTF.Image = (System.Drawing.Image)resources.GetObject("checkBoxPCTF.Image");
            checkBoxPCTF.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxPCTF.Location = new System.Drawing.Point(0, 1);
            checkBoxPCTF.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            checkBoxPCTF.Name = "checkBoxPCTF";
            checkBoxPCTF.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            checkBoxPCTF.Size = new System.Drawing.Size(349, 32);
            checkBoxPCTF.TabIndex = 8;
            checkBoxPCTF.UseVisualStyleBackColor = true;
            checkBoxPCTF.CheckedChanged += checkBoxSinW_CheckedChanged;
            // 
            // buttonCopyGraph
            // 
            buttonCopyGraph.AutoSize = true;
            buttonCopyGraph.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonCopyGraph.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonCopyGraph.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonCopyGraph.Location = new System.Drawing.Point(658, 0);
            buttonCopyGraph.Margin = new System.Windows.Forms.Padding(0);
            buttonCopyGraph.Name = "buttonCopyGraph";
            buttonCopyGraph.Size = new System.Drawing.Size(48, 27);
            buttonCopyGraph.TabIndex = 58;
            buttonCopyGraph.Text = "Copy";
            buttonCopyGraph.UseVisualStyleBackColor = true;
            buttonCopyGraph.Click += ButtonCopyGraph_Click;
            // 
            // numericBoxMaxU1
            // 
            numericBoxMaxU1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxMaxU1.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxU1.DecimalPlaces = 1;
            numericBoxMaxU1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxMaxU1.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxU1.FooterText = "nm⁻¹";
            numericBoxMaxU1.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxU1.HeaderText = "Max u";
            numericBoxMaxU1.Location = new System.Drawing.Point(522, 0);
            numericBoxMaxU1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            numericBoxMaxU1.Maximum = 20D;
            numericBoxMaxU1.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxMaxU1.Minimum = 0D;
            numericBoxMaxU1.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxMaxU1.Name = "numericBoxMaxU1";
            numericBoxMaxU1.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxMaxU1.RadianValue = 0.10471975511965977D;
            numericBoxMaxU1.RoundErrorAccuracy = -1;
            numericBoxMaxU1.ShowUpDown = true;
            numericBoxMaxU1.Size = new System.Drawing.Size(136, 27);
            numericBoxMaxU1.SmartIncrement = true;
            numericBoxMaxU1.TabIndex = 56;
            numericBoxMaxU1.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxMaxU1.ThonsandsSeparator = true;
            numericBoxMaxU1.Value = 6D;
            numericBoxMaxU1.ValueChanged += numericBoxMaxU1_ValueChanged;
            // 
            // graphControl
            // 
            graphControl.AllowMouseOperation = true;
            graphControl.AxisLineColor = System.Drawing.Color.Gray;
            graphControl.AxisTextColor = System.Drawing.Color.Black;
            graphControl.AxisTextFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControl.AxisXTextVisible = true;
            graphControl.AxisYTextVisible = true;
            graphControl.BackgroundColor = System.Drawing.Color.White;
            graphControl.BottomMargin = 0D;
            graphControl.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControl.DivisionLineXVisible = true;
            graphControl.DivisionLineYVisible = true;
            graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            graphControl.DrawingRange = (RectangleD)resources.GetObject("graphControl.DrawingRange");
            graphControl.FixRangeHorizontal = false;
            graphControl.FixRangeVertical = false;
            graphControl.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControl.GraphTitle = "";
            graphControl.Interpolation = false;
            graphControl.IsIntegerX = false;
            graphControl.IsIntegerY = false;
            graphControl.LabelX = "X:";
            graphControl.LabelY = "Y:";
            graphControl.LeftMargin = 0F;
            graphControl.LineWidth = 1F;
            graphControl.Location = new System.Drawing.Point(3, 280);
            graphControl.LowerX = 0D;
            graphControl.LowerY = 0D;
            graphControl.Margin = new System.Windows.Forms.Padding(0);
            graphControl.MaximalX = 1D;
            graphControl.MaximalY = 1D;
            graphControl.MinimalX = 0D;
            graphControl.MinimalY = 0D;
            graphControl.Mode = GraphControl.DrawingMode.Line;
            graphControl.MousePositionVisible = true;
            graphControl.MousePositionXDigit = 4;
            graphControl.MousePositionYDigit = 4;
            graphControl.Name = "graphControl";
            graphControl.OriginPosition = new System.Drawing.Point(20, 20);
            graphControl.Padding = new System.Windows.Forms.Padding(2, 0, 0, 2);
            graphControl.Profile = null;
            graphControl.Size = new System.Drawing.Size(706, 176);
            graphControl.Smoothing = false;
            graphControl.TabIndex = 57;
            graphControl.UnitX = "nm⁻¹";
            graphControl.UnitY = "";
            graphControl.UpperPanelFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControl.UpperPanelVisible = true;
            graphControl.UpperX = 1D;
            graphControl.UpperY = 1D;
            graphControl.UseLineWidth = true;
            graphControl.VerticalLineColor = System.Drawing.Color.Red;
            graphControl.XLog = false;
            graphControl.YLog = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(buttonCopyGraph);
            flowLayoutPanel1.Controls.Add(numericBoxMaxU1);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new System.Drawing.Point(3, 456);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(706, 27);
            flowLayoutPanel1.TabIndex = 62;
            // 
            // checkBoxCosW
            // 
            checkBoxCosW.AutoSize = true;
            checkBoxCosW.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxCosW.Image = (System.Drawing.Image)resources.GetObject("checkBoxCosW.Image");
            checkBoxCosW.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            checkBoxCosW.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxCosW.Location = new System.Drawing.Point(315, 1);
            checkBoxCosW.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            checkBoxCosW.Name = "checkBoxCosW";
            checkBoxCosW.Size = new System.Drawing.Size(314, 32);
            checkBoxCosW.TabIndex = 14;
            checkBoxCosW.UseVisualStyleBackColor = true;
            checkBoxCosW.CheckedChanged += checkBoxSinW_CheckedChanged;
            // 
            // checkBoxEs_STEM
            // 
            checkBoxEs_STEM.AutoSize = true;
            checkBoxEs_STEM.Checked = true;
            checkBoxEs_STEM.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxEs_STEM.Font = new System.Drawing.Font("Segoe UI Symbol", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxEs_STEM.Image = (System.Drawing.Image)resources.GetObject("checkBoxEs_STEM.Image");
            checkBoxEs_STEM.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            checkBoxEs_STEM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxEs_STEM.Location = new System.Drawing.Point(3, 35);
            checkBoxEs_STEM.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            checkBoxEs_STEM.Name = "checkBoxEs_STEM";
            checkBoxEs_STEM.Size = new System.Drawing.Size(198, 29);
            checkBoxEs_STEM.TabIndex = 9;
            checkBoxEs_STEM.UseVisualStyleBackColor = true;
            checkBoxEs_STEM.CheckedChanged += checkBoxSinW_CheckedChanged;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel3.Controls.Add(flowLayoutPanel4);
            flowLayoutPanel3.Location = new System.Drawing.Point(3, 0);
            flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new System.Drawing.Size(0, 0);
            flowLayoutPanel3.TabIndex = 13;
            flowLayoutPanel3.WrapContents = false;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new System.Drawing.Size(0, 0);
            flowLayoutPanel4.TabIndex = 13;
            flowLayoutPanel4.WrapContents = false;
            // 
            // checkBoxACTF
            // 
            checkBoxACTF.AutoSize = true;
            checkBoxACTF.Font = new System.Drawing.Font("Segoe UI Symbol", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxACTF.Image = (System.Drawing.Image)resources.GetObject("checkBoxACTF.Image");
            checkBoxACTF.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxACTF.Location = new System.Drawing.Point(0, 35);
            checkBoxACTF.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            checkBoxACTF.Name = "checkBoxACTF";
            checkBoxACTF.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            checkBoxACTF.Size = new System.Drawing.Size(351, 31);
            checkBoxACTF.TabIndex = 8;
            checkBoxACTF.UseVisualStyleBackColor = true;
            checkBoxACTF.CheckedChanged += checkBoxSinW_CheckedChanged;
            // 
            // pictureBoxA_STEM
            // 
            pictureBoxA_STEM.Image = (System.Drawing.Image)resources.GetObject("pictureBoxA_STEM.Image");
            pictureBoxA_STEM.Location = new System.Drawing.Point(360, 15);
            pictureBoxA_STEM.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            pictureBoxA_STEM.Name = "pictureBoxA_STEM";
            pictureBoxA_STEM.Size = new System.Drawing.Size(178, 44);
            pictureBoxA_STEM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBoxA_STEM.TabIndex = 11;
            pictureBoxA_STEM.TabStop = false;
            // 
            // pictureBoxA_HRTEM
            // 
            pictureBoxA_HRTEM.Image = (System.Drawing.Image)resources.GetObject("pictureBoxA_HRTEM.Image");
            pictureBoxA_HRTEM.Location = new System.Drawing.Point(544, 15);
            pictureBoxA_HRTEM.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            pictureBoxA_HRTEM.Name = "pictureBoxA_HRTEM";
            pictureBoxA_HRTEM.Size = new System.Drawing.Size(274, 44);
            pictureBoxA_HRTEM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBoxA_HRTEM.TabIndex = 12;
            pictureBoxA_HRTEM.TabStop = false;
            // 
            // pictureBoxSTEM_CTFI
            // 
            pictureBoxSTEM_CTFI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pictureBoxSTEM_CTFI.Dock = System.Windows.Forms.DockStyle.Top;
            pictureBoxSTEM_CTFI.Image = (System.Drawing.Image)resources.GetObject("pictureBoxSTEM_CTFI.Image");
            pictureBoxSTEM_CTFI.Location = new System.Drawing.Point(6, 0);
            pictureBoxSTEM_CTFI.Margin = new System.Windows.Forms.Padding(0);
            pictureBoxSTEM_CTFI.Name = "pictureBoxSTEM_CTFI";
            pictureBoxSTEM_CTFI.Size = new System.Drawing.Size(694, 75);
            pictureBoxSTEM_CTFI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBoxSTEM_CTFI.TabIndex = 10;
            pictureBoxSTEM_CTFI.TabStop = false;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.AutoSize = true;
            flowLayoutPanel5.Controls.Add(checkBoxPCTF);
            flowLayoutPanel5.Controls.Add(checkBoxACTF);
            flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new System.Drawing.Size(351, 67);
            flowLayoutPanel5.TabIndex = 17;
            flowLayoutPanel5.WrapContents = false;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.BackColor = System.Drawing.Color.White;
            panel2.Controls.Add(flowLayoutPanel7);
            panel2.Controls.Add(flowLayoutPanel6);
            panel2.Controls.Add(pictureBoxSTEM_CTFI);
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(3, 30);
            panel2.Name = "panel2";
            panel2.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            panel2.Size = new System.Drawing.Size(706, 250);
            panel2.TabIndex = 64;
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.AutoSize = true;
            flowLayoutPanel7.Controls.Add(flowLayoutPanel5);
            flowLayoutPanel7.Controls.Add(pictureBoxA_STEM);
            flowLayoutPanel7.Controls.Add(pictureBoxA_HRTEM);
            flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel7.Location = new System.Drawing.Point(6, 177);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            flowLayoutPanel7.Size = new System.Drawing.Size(694, 73);
            flowLayoutPanel7.TabIndex = 16;
            flowLayoutPanel7.WrapContents = false;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.AutoSize = true;
            flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel6.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel6.Controls.Add(checkBoxSinW);
            flowLayoutPanel6.Controls.Add(checkBoxCosW);
            flowLayoutPanel6.Controls.Add(checkBoxEs_STEM);
            flowLayoutPanel6.Controls.Add(checkBoxEs_HRTEM);
            flowLayoutPanel6.Controls.Add(checkBoxEc);
            flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel6.Location = new System.Drawing.Point(6, 75);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            flowLayoutPanel6.Size = new System.Drawing.Size(694, 102);
            flowLayoutPanel6.TabIndex = 11;
            // 
            // FormCTF
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(712, 486);
            Controls.Add(graphControl);
            Controls.Add(panel2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(flowLayoutPanelSTEM);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "FormCTF";
            Padding = new System.Windows.Forms.Padding(3);
            Text = "CTF (Contrast Transfer Function)";
            FormClosing += FormCTF_FormClosing;
            VisibleChanged += FormCTF_VisibleChanged;
            flowLayoutPanelSTEM.ResumeLayout(false);
            flowLayoutPanelSTEM.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxA_STEM).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxA_HRTEM).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSTEM_CTFI).EndInit();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSTEM;
        private System.Windows.Forms.RadioButton radioButtonCTF_coherent;
        private System.Windows.Forms.RadioButton radioButtonCTF_Incoherent;
        private System.Windows.Forms.CheckBox checkBoxSinW;
        private System.Windows.Forms.CheckBox checkBoxEs_HRTEM;
        private System.Windows.Forms.CheckBox checkBoxEc;
        private System.Windows.Forms.CheckBox checkBoxPCTF;
        private System.Windows.Forms.Button buttonCopyGraph;
        private NumericBox numericBoxMaxU1;
        private GraphControl graphControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBoxEs_STEM;
        private System.Windows.Forms.PictureBox pictureBoxSTEM_CTFI;
        private System.Windows.Forms.PictureBox pictureBoxA_STEM;
        private System.Windows.Forms.PictureBox pictureBoxA_HRTEM;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.CheckBox checkBoxCosW;
        private System.Windows.Forms.CheckBox checkBoxACTF;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
    }
}
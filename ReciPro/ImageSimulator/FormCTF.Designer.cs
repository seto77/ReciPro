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
            checkBoxCTF = new System.Windows.Forms.CheckBox();
            buttonCopyGraph = new System.Windows.Forms.Button();
            numericBoxMaxU1 = new NumericBox();
            graphControl = new GraphControl();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxSTEM_Es = new System.Windows.Forms.CheckBox();
            pictureBoxSTEM_CTFI = new System.Windows.Forms.PictureBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            pictureBoxSTEM_A = new System.Windows.Forms.PictureBox();
            pictureBoxHRTEM_A = new System.Windows.Forms.PictureBox();
            flowLayoutPanelSTEM.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSTEM_CTFI).BeginInit();
            flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSTEM_A).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxHRTEM_A).BeginInit();
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
            flowLayoutPanelSTEM.Size = new System.Drawing.Size(726, 27);
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
            checkBoxSinW.Location = new System.Drawing.Point(10, 1);
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
            checkBoxEs_HRTEM.Location = new System.Drawing.Point(322, 1);
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
            checkBoxEc.Location = new System.Drawing.Point(208, 35);
            checkBoxEc.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            checkBoxEc.Name = "checkBoxEc";
            checkBoxEc.Size = new System.Drawing.Size(287, 32);
            checkBoxEc.TabIndex = 7;
            checkBoxEc.UseVisualStyleBackColor = true;
            checkBoxEc.CheckedChanged += checkBoxSinW_CheckedChanged;
            // 
            // checkBoxCTF
            // 
            checkBoxCTF.AutoSize = true;
            checkBoxCTF.Checked = true;
            checkBoxCTF.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxCTF.Font = new System.Drawing.Font("Segoe UI Symbol", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxCTF.Image = (System.Drawing.Image)resources.GetObject("checkBoxCTF.Image");
            checkBoxCTF.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxCTF.Location = new System.Drawing.Point(0, 1);
            checkBoxCTF.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            checkBoxCTF.Name = "checkBoxCTF";
            checkBoxCTF.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            checkBoxCTF.Size = new System.Drawing.Size(322, 42);
            checkBoxCTF.TabIndex = 8;
            checkBoxCTF.UseVisualStyleBackColor = true;
            checkBoxCTF.CheckedChanged += checkBoxSinW_CheckedChanged;
            // 
            // buttonCopyGraph
            // 
            buttonCopyGraph.AutoSize = true;
            buttonCopyGraph.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonCopyGraph.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonCopyGraph.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonCopyGraph.Location = new System.Drawing.Point(678, 0);
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
            numericBoxMaxU1.Location = new System.Drawing.Point(542, 0);
            numericBoxMaxU1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            numericBoxMaxU1.Maximum = 20D;
            numericBoxMaxU1.MaximumSize = new System.Drawing.Size(1000, 27);
            numericBoxMaxU1.Minimum = 0D;
            numericBoxMaxU1.MinimumSize = new System.Drawing.Size(1, 25);
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
            graphControl.BackgroundColor = System.Drawing.Color.White;
            graphControl.BottomMargin = 0D;
            graphControl.DivisionLineColor = System.Drawing.Color.Gray;
            graphControl.DivisionSubLineColor = System.Drawing.Color.LightGray;
            graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            graphControl.FixRangeHorizontal = false;
            graphControl.FixRangeVertical = false;
            graphControl.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControl.GraphName = "";
            graphControl.HorizontalGradiationTextVisivle = true;
            graphControl.Interpolation = false;
            graphControl.IsIntegerX = false;
            graphControl.IsIntegerY = false;
            graphControl.LabelX = "X:";
            graphControl.LabelY = "Y:";
            graphControl.LeftMargin = 0F;
            graphControl.LineColor = System.Drawing.Color.Red;
            graphControl.LineWidth = 1F;
            graphControl.Location = new System.Drawing.Point(3, 219);
            graphControl.LowerX = 0D;
            graphControl.LowerY = 0D;
            graphControl.Margin = new System.Windows.Forms.Padding(0);
            graphControl.MaximalX = 1D;
            graphControl.MaximalY = 1D;
            graphControl.MinimalX = 0D;
            graphControl.MinimalY = 0D;
            graphControl.Mode = GraphControl.DrawingMode.Line;
            graphControl.MousePositionVisible = true;
            graphControl.Name = "graphControl";
            graphControl.OriginPosition = new System.Drawing.Point(20, 20);
            graphControl.Padding = new System.Windows.Forms.Padding(2, 0, 0, 2);
            graphControl.Size = new System.Drawing.Size(726, 237);
            graphControl.Smoothing = false;
            graphControl.TabIndex = 57;
            graphControl.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControl.UnitX = "";
            graphControl.UnitY = "";
            graphControl.UpperText = "";
            graphControl.UpperTextVisible = false;
            graphControl.UpperX = 1D;
            graphControl.UpperY = 1D;
            graphControl.UseLineWidth = true;
            graphControl.VerticalGradiationTextVisivle = true;
            graphControl.XLog = false;
            graphControl.XScaleLineVisible = true;
            graphControl.YLog = false;
            graphControl.YScaleLineVisible = true;
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
            flowLayoutPanel1.Size = new System.Drawing.Size(726, 27);
            flowLayoutPanel1.TabIndex = 62;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.BackColor = System.Drawing.Color.White;
            flowLayoutPanel2.Controls.Add(checkBoxSinW);
            flowLayoutPanel2.Controls.Add(checkBoxEs_HRTEM);
            flowLayoutPanel2.Controls.Add(checkBoxSTEM_Es);
            flowLayoutPanel2.Controls.Add(checkBoxEc);
            flowLayoutPanel2.Controls.Add(pictureBoxSTEM_CTFI);
            flowLayoutPanel2.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel2.Location = new System.Drawing.Point(3, 30);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            flowLayoutPanel2.Size = new System.Drawing.Size(726, 189);
            flowLayoutPanel2.TabIndex = 63;
            // 
            // checkBoxSTEM_Es
            // 
            checkBoxSTEM_Es.AutoSize = true;
            checkBoxSTEM_Es.Checked = true;
            checkBoxSTEM_Es.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxSTEM_Es.Font = new System.Drawing.Font("Segoe UI Symbol", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxSTEM_Es.Image = (System.Drawing.Image)resources.GetObject("checkBoxSTEM_Es.Image");
            checkBoxSTEM_Es.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            checkBoxSTEM_Es.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxSTEM_Es.Location = new System.Drawing.Point(10, 35);
            checkBoxSTEM_Es.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            checkBoxSTEM_Es.Name = "checkBoxSTEM_Es";
            checkBoxSTEM_Es.Size = new System.Drawing.Size(198, 29);
            checkBoxSTEM_Es.TabIndex = 9;
            checkBoxSTEM_Es.UseVisualStyleBackColor = true;
            checkBoxSTEM_Es.CheckedChanged += checkBoxSinW_CheckedChanged;
            // 
            // pictureBoxSTEM_CTFI
            // 
            pictureBoxSTEM_CTFI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pictureBoxSTEM_CTFI.Image = (System.Drawing.Image)resources.GetObject("pictureBoxSTEM_CTFI.Image");
            pictureBoxSTEM_CTFI.Location = new System.Drawing.Point(10, 68);
            pictureBoxSTEM_CTFI.Margin = new System.Windows.Forms.Padding(0);
            pictureBoxSTEM_CTFI.Name = "pictureBoxSTEM_CTFI";
            pictureBoxSTEM_CTFI.Size = new System.Drawing.Size(685, 74);
            pictureBoxSTEM_CTFI.TabIndex = 10;
            pictureBoxSTEM_CTFI.TabStop = false;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Controls.Add(checkBoxCTF);
            flowLayoutPanel3.Controls.Add(pictureBoxSTEM_A);
            flowLayoutPanel3.Controls.Add(pictureBoxHRTEM_A);
            flowLayoutPanel3.Location = new System.Drawing.Point(10, 142);
            flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new System.Drawing.Size(793, 47);
            flowLayoutPanel3.TabIndex = 13;
            flowLayoutPanel3.WrapContents = false;
            // 
            // pictureBoxSTEM_A
            // 
            pictureBoxSTEM_A.Image = (System.Drawing.Image)resources.GetObject("pictureBoxSTEM_A.Image");
            pictureBoxSTEM_A.Location = new System.Drawing.Point(325, 3);
            pictureBoxSTEM_A.Name = "pictureBoxSTEM_A";
            pictureBoxSTEM_A.Size = new System.Drawing.Size(177, 41);
            pictureBoxSTEM_A.TabIndex = 11;
            pictureBoxSTEM_A.TabStop = false;
            // 
            // pictureBoxHRTEM_A
            // 
            pictureBoxHRTEM_A.Image = (System.Drawing.Image)resources.GetObject("pictureBoxHRTEM_A.Image");
            pictureBoxHRTEM_A.Location = new System.Drawing.Point(508, 3);
            pictureBoxHRTEM_A.Name = "pictureBoxHRTEM_A";
            pictureBoxHRTEM_A.Size = new System.Drawing.Size(282, 41);
            pictureBoxHRTEM_A.TabIndex = 12;
            pictureBoxHRTEM_A.TabStop = false;
            // 
            // FormCTF
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(732, 486);
            Controls.Add(graphControl);
            Controls.Add(flowLayoutPanel2);
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
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSTEM_CTFI).EndInit();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSTEM_A).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxHRTEM_A).EndInit();
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
        private System.Windows.Forms.CheckBox checkBoxCTF;
        private System.Windows.Forms.Button buttonCopyGraph;
        private NumericBox numericBoxMaxU1;
        private GraphControl graphControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBoxSTEM_Es;
        private System.Windows.Forms.PictureBox pictureBoxSTEM_CTFI;
        private System.Windows.Forms.PictureBox pictureBoxSTEM_A;
        private System.Windows.Forms.PictureBox pictureBoxHRTEM_A;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
    }
}
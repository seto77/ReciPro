namespace ReciPro
{
    partial class FormSpotIDv2Details
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSpotIDv2Details)); // 260531Cl
            components = new System.ComponentModel.Container(); // (260531Ch)
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            captureExtender.SetCapture(this, true); // 260521Cl 追加: GUI監査キャプチャ対象 (フォーム全体)
            scalablePictureBoxAdvanced = new Crystallography.Controls.ScalablePictureBoxAdvanced();
            graphControlNWtoSE = new Crystallography.Controls.GraphControl();
            graphControlWtoE = new Crystallography.Controls.GraphControl();
            graphControlSWtoNE = new Crystallography.Controls.GraphControl();
            graphControlNtoS = new Crystallography.Controls.GraphControl();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // scalablePictureBoxAdvanced
            // 
            scalablePictureBoxAdvanced.Cursor = System.Windows.Forms.Cursors.Default;
            scalablePictureBoxAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            scalablePictureBoxAdvanced.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            scalablePictureBoxAdvanced.FrequencyGraphVisible = false;
            scalablePictureBoxAdvanced.ImageFilter_DustAndScratchesVisible = false;
            scalablePictureBoxAdvanced.ImageFilter_GaussianBlurVisible = false;
            scalablePictureBoxAdvanced.ImageFilterVisible = false;
            scalablePictureBoxAdvanced.Location = new System.Drawing.Point(0, 0);
            scalablePictureBoxAdvanced.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            scalablePictureBoxAdvanced.MousePositionLabelVisible = false;
            scalablePictureBoxAdvanced.Name = "scalablePictureBoxAdvanced";
            scalablePictureBoxAdvanced.Size = new System.Drawing.Size(391, 474);
            scalablePictureBoxAdvanced.StatusLabel = " ";
            scalablePictureBoxAdvanced.StatusProgress = 0D;
            scalablePictureBoxAdvanced.StatusVisible = false;
            scalablePictureBoxAdvanced.TabIndex = 3;
            // 
            // graphControlNWtoSE
            // 
            graphControlNWtoSE.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControlNWtoSE.GraphTitle = "";
            graphControlNWtoSE.Interpolation = false;
            graphControlNWtoSE.Location = new System.Drawing.Point(1, 18);
            graphControlNWtoSE.Margin = new System.Windows.Forms.Padding(1);
            graphControlNWtoSE.MousePositionVisible = false;
            graphControlNWtoSE.Name = "graphControlNWtoSE";
            graphControlNWtoSE.Size = new System.Drawing.Size(250, 208);
            graphControlNWtoSE.Smoothing = false;
            graphControlNWtoSE.TabIndex = 2;
            graphControlNWtoSE.UpperPanelFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // graphControlWtoE
            // 
            graphControlWtoE.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControlWtoE.GraphTitle = "";
            graphControlWtoE.Interpolation = false;
            graphControlWtoE.Location = new System.Drawing.Point(1, 18);
            graphControlWtoE.Margin = new System.Windows.Forms.Padding(1);
            graphControlWtoE.MousePositionVisible = false;
            graphControlWtoE.Name = "graphControlWtoE";
            graphControlWtoE.Size = new System.Drawing.Size(250, 208);
            graphControlWtoE.Smoothing = false;
            graphControlWtoE.TabIndex = 2;
            graphControlWtoE.UpperPanelFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // graphControlSWtoNE
            // 
            graphControlSWtoNE.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControlSWtoNE.GraphTitle = "";
            graphControlSWtoNE.Interpolation = false;
            graphControlSWtoNE.Location = new System.Drawing.Point(1, 18);
            graphControlSWtoNE.Margin = new System.Windows.Forms.Padding(1);
            graphControlSWtoNE.MousePositionVisible = false;
            graphControlSWtoNE.Name = "graphControlSWtoNE";
            graphControlSWtoNE.Size = new System.Drawing.Size(250, 208);
            graphControlSWtoNE.Smoothing = false;
            graphControlSWtoNE.TabIndex = 2;
            graphControlSWtoNE.UpperPanelFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // graphControlNtoS
            // 
            graphControlNtoS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControlNtoS.GraphTitle = "";
            graphControlNtoS.Interpolation = false;
            graphControlNtoS.Location = new System.Drawing.Point(1, 18);
            graphControlNtoS.Margin = new System.Windows.Forms.Padding(1);
            graphControlNtoS.MousePositionVisible = false;
            graphControlNtoS.Name = "graphControlNtoS";
            graphControlNtoS.Size = new System.Drawing.Size(250, 208);
            graphControlNtoS.Smoothing = false;
            graphControlNtoS.TabIndex = 2;
            graphControlNtoS.UpperPanelFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.Red;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip")); // 260531Cl
            label1.Size = new System.Drawing.Size(51, 17);
            label1.TabIndex = 5;
            label1.Text = "N to S";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor = System.Drawing.Color.Orange;
            label2.Location = new System.Drawing.Point(3, 0);
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip")); // 260531Cl
            label2.Size = new System.Drawing.Size(53, 17);
            label2.TabIndex = 5;
            label2.Text = "W to E";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label3.ForeColor = System.Drawing.Color.OrangeRed;
            label3.Location = new System.Drawing.Point(3, 0);
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip")); // 260531Cl
            label3.Size = new System.Drawing.Size(77, 17);
            label3.TabIndex = 5;
            label3.Text = "SW to NE ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label4.ForeColor = System.Drawing.Color.Purple;
            label4.Location = new System.Drawing.Point(3, 0);
            label4.Name = "label4";
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip")); // 260531Cl
            label4.Size = new System.Drawing.Size(72, 17);
            label4.TabIndex = 5;
            label4.Text = "NW to SE";
            label4.Click += new System.EventHandler(label4_Click);
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(flowLayoutPanel2);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel5);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel4);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(524, 474);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(label1);
            flowLayoutPanel2.Controls.Add(graphControlNtoS);
            flowLayoutPanel2.Cursor = System.Windows.Forms.Cursors.Default;
            flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel2.Location = new System.Drawing.Point(1, 1);
            flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(252, 227);
            flowLayoutPanel2.TabIndex = 6;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Controls.Add(label2);
            flowLayoutPanel3.Controls.Add(graphControlWtoE);
            flowLayoutPanel3.Cursor = System.Windows.Forms.Cursors.Default;
            flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel3.Location = new System.Drawing.Point(255, 1);
            flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(1);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new System.Drawing.Size(252, 227);
            flowLayoutPanel3.TabIndex = 6;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.AutoSize = true;
            flowLayoutPanel5.Controls.Add(label3);
            flowLayoutPanel5.Controls.Add(graphControlSWtoNE);
            flowLayoutPanel5.Cursor = System.Windows.Forms.Cursors.Default;
            flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel5.Location = new System.Drawing.Point(1, 230);
            flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(1);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new System.Drawing.Size(252, 227);
            flowLayoutPanel5.TabIndex = 6;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.Controls.Add(label4);
            flowLayoutPanel4.Controls.Add(graphControlNWtoSE);
            flowLayoutPanel4.Cursor = System.Windows.Forms.Cursors.Default;
            flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel4.Location = new System.Drawing.Point(255, 230);
            flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(1);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new System.Drawing.Size(252, 227);
            flowLayoutPanel4.TabIndex = 6;
            // 
            // splitContainer1
            // 
            splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(scalablePictureBoxAdvanced);
            splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel1);
            splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            splitContainer1.Size = new System.Drawing.Size(919, 474);
            splitContainer1.SplitterDistance = 391;
            splitContainer1.TabIndex = 7;
            // 
            // FormSpotDetails
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(919, 474);
            Controls.Add(splitContainer1);
            Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "FormSpotDetails";
            Text = "FormSpotDetails";
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormSpotDetails_FormClosing);
            VisibleChanged += new System.EventHandler(FormSpotDetails_VisibleChanged);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)
        private Crystallography.Controls.GraphControl graphControlNtoS;
        private Crystallography.Controls.GraphControl graphControlWtoE;
        private Crystallography.Controls.GraphControl graphControlSWtoNE;
        private Crystallography.Controls.GraphControl graphControlNWtoSE;
        private Crystallography.Controls.ScalablePictureBoxAdvanced scalablePictureBoxAdvanced;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
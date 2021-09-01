using OpenTK;
using OpenTK.Graphics;
namespace Crystallography.OpenGL
{
    partial class GLControlAlpha
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemProjectionMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxProjectionMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItemRenderTransparency = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxRenderTransparency = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRotate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxRotationMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItemTranslate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxTranslatingMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItemChange = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxChangingMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.inputRGAndB0255ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxBackColorRed = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBoxBackgroundColorG = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBoxBackgroundColorBlue = new System.Windows.Forms.ToolStripTextBox();
            this.showHintsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.contextMenuStrip.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemProjectionMode,
            this.toolStripMenuItemRenderTransparency,
            this.toolStripSeparator1,
            this.toolStripMenuItemRotate,
            this.toolStripMenuItemTranslate,
            this.toolStripMenuItemChange,
            this.toolStripSeparator2,
            this.toolStripMenuItem4,
            this.showHintsToolStripMenuItem});
            this.contextMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(202, 170);
            // 
            // toolStripMenuItemProjectionMode
            // 
            this.toolStripMenuItemProjectionMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxProjectionMode});
            this.toolStripMenuItemProjectionMode.Name = "toolStripMenuItemProjectionMode";
            this.toolStripMenuItemProjectionMode.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItemProjectionMode.Text = "Projection mode";
            // 
            // toolStripComboBoxProjectionMode
            // 
            this.toolStripComboBoxProjectionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxProjectionMode.Items.AddRange(new object[] {
            "Perspective",
            "Orthographic"});
            this.toolStripComboBoxProjectionMode.Name = "toolStripComboBoxProjectionMode";
            this.toolStripComboBoxProjectionMode.Size = new System.Drawing.Size(121, 23);
            // 
            // toolStripMenuItemRenderTransparency
            // 
            this.toolStripMenuItemRenderTransparency.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxRenderTransparency});
            this.toolStripMenuItemRenderTransparency.Name = "toolStripMenuItemRenderTransparency";
            this.toolStripMenuItemRenderTransparency.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItemRenderTransparency.Text = "Rendering transparency";
            // 
            // toolStripComboBoxRenderTransparency
            // 
            this.toolStripComboBoxRenderTransparency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxRenderTransparency.Items.AddRange(new object[] {
            "Always",
            "If possible"});
            this.toolStripComboBoxRenderTransparency.Name = "toolStripComboBoxRenderTransparency";
            this.toolStripComboBoxRenderTransparency.Size = new System.Drawing.Size(121, 23);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // toolStripMenuItemRotate
            // 
            this.toolStripMenuItemRotate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxRotationMode});
            this.toolStripMenuItemRotate.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItemRotate.Name = "toolStripMenuItemRotate";
            this.toolStripMenuItemRotate.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItemRotate.Text = "Left drag: Rotate";
            // 
            // toolStripComboBoxRotationMode
            // 
            this.toolStripComboBoxRotationMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxRotationMode.Items.AddRange(new object[] {
            "Objects",
            "View (Camera)",
            "Light"});
            this.toolStripComboBoxRotationMode.Name = "toolStripComboBoxRotationMode";
            this.toolStripComboBoxRotationMode.Size = new System.Drawing.Size(121, 23);
            // 
            // toolStripMenuItemTranslate
            // 
            this.toolStripMenuItemTranslate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxTranslatingMode});
            this.toolStripMenuItemTranslate.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItemTranslate.Name = "toolStripMenuItemTranslate";
            this.toolStripMenuItemTranslate.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItemTranslate.Text = "Middle drag: Translate";
            // 
            // toolStripComboBoxTranslatingMode
            // 
            this.toolStripComboBoxTranslatingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxTranslatingMode.Items.AddRange(new object[] {
            "View (Camera)",
            "Objects"});
            this.toolStripComboBoxTranslatingMode.Name = "toolStripComboBoxTranslatingMode";
            this.toolStripComboBoxTranslatingMode.Size = new System.Drawing.Size(121, 23);
            // 
            // toolStripMenuItemChange
            // 
            this.toolStripMenuItemChange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxChangingMode});
            this.toolStripMenuItemChange.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItemChange.Name = "toolStripMenuItemChange";
            this.toolStripMenuItemChange.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItemChange.Text = "Right drag: Change";
            // 
            // toolStripComboBoxChangingMode
            // 
            this.toolStripComboBoxChangingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxChangingMode.Items.AddRange(new object[] {
            "Field of view",
            "Sense of perspective",
            "Light distance"});
            this.toolStripComboBoxChangingMode.Name = "toolStripComboBoxChangingMode";
            this.toolStripComboBoxChangingMode.Size = new System.Drawing.Size(121, 23);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputRGAndB0255ToolStripMenuItem,
            this.toolStripTextBoxBackColorRed,
            this.toolStripTextBoxBackgroundColorG,
            this.toolStripTextBoxBackgroundColorBlue});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(201, 22);
            this.toolStripMenuItem4.Text = "Background color (RGB)";
            // 
            // inputRGAndB0255ToolStripMenuItem
            // 
            this.inputRGAndB0255ToolStripMenuItem.Enabled = false;
            this.inputRGAndB0255ToolStripMenuItem.Name = "inputRGAndB0255ToolStripMenuItem";
            this.inputRGAndB0255ToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.inputRGAndB0255ToolStripMenuItem.Text = "Input values os R, G, B (0-255)";
            // 
            // toolStripTextBoxBackColorRed
            // 
            this.toolStripTextBoxBackColorRed.ForeColor = System.Drawing.Color.Red;
            this.toolStripTextBoxBackColorRed.Name = "toolStripTextBoxBackColorRed";
            this.toolStripTextBoxBackColorRed.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBoxBackColorRed.Text = "255";
            // 
            // toolStripTextBoxBackgroundColorG
            // 
            this.toolStripTextBoxBackgroundColorG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.toolStripTextBoxBackgroundColorG.Name = "toolStripTextBoxBackgroundColorG";
            this.toolStripTextBoxBackgroundColorG.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBoxBackgroundColorG.Text = "255";
            // 
            // toolStripTextBoxBackgroundColorBlue
            // 
            this.toolStripTextBoxBackgroundColorBlue.ForeColor = System.Drawing.Color.Blue;
            this.toolStripTextBoxBackgroundColorBlue.Name = "toolStripTextBoxBackgroundColorBlue";
            this.toolStripTextBoxBackgroundColorBlue.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBoxBackgroundColorBlue.Text = "255";
            // 
            // showHintsToolStripMenuItem
            // 
            this.showHintsToolStripMenuItem.Name = "showHintsToolStripMenuItem";
            this.showHintsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.showHintsToolStripMenuItem.Text = "Show hints";
            // 
            // GLControlAlpha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "GLControlAlpha";
            this.Size = new System.Drawing.Size(99, 94);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRotate;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxRotationMode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTranslate;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxTranslatingMode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChange;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxChangingMode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemProjectionMode;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxProjectionMode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRenderTransparency;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxRenderTransparency;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxBackColorRed;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxBackgroundColorG;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxBackgroundColorBlue;
        private System.Windows.Forms.ToolStripMenuItem inputRGAndB0255ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHintsToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

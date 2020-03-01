using System.Drawing;
namespace Crystallography.Controls
{
    partial class ExRichTextBox
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
            //components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            SuspendLayout();
            Controls.Add(listBox);
            //Controls.Add(toolTipLabel);
            //toolTipLabel.Visible = false;
            //toolTipLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            //toolTipLabel.AutoSize = true;
            //toolTipLabel.Padding = new System.Windows.Forms.Padding(3,6,3,3);
            //toolTipLabel.BackColor = Color.LightGray;
           // toolTipLabel.ForeColor = Color.DarkGreen;

            listBox.Visible = false;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class NumericBoxWithMenu : NumericBox
    {

        // 260426Cl 整理: 内部用フラグなので auto-property → 単純な private フィールドに変更
        //bool skipEvent { get; set; } = false;
        private bool skipEvent = false;

        public event MyEventHandler LimitChanged;
        public NumericBoxWithMenu()
        {
            InitializeComponent();

            textBox.ContextMenuStrip = contextMenuStripBody;
            //numericUpDown.ContextMenuStrip = contextMenuStripUpDown;                                                                                // 260413Cl
            if (spinButton != null)                                                                                                                   // 260413Cl デザイン時はbaseがspinButtonを生成しないためnullガード
                spinButton.ContextMenuStrip = contextMenuStripUpDown;

            textBox.MouseUp += textBox_MouseUp;
            textBox.MouseMove += textBox_MouseMove;
            textBox.MouseDown += textBox_MouseDown;

            toolStripComboBoxMouseDirection.SelectedIndex = 0;
        }

        private void smartIncrementToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
            => SmartIncrement = smartIncrementToolStripMenuItem.Checked;

        private void toolStripComboBoxIncrement_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 260426Cl 修正: 余分なセミコロン (空文; 空ステートメント) を削除
            double.TryParse(toolStripComboBoxIncrement.Text, out double inc);
            if (inc > 0)
                UpDown_Increment = inc;
        }

        private void toolStripComboBoxDecimalPlaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipEvent)
                return;

            DecimalPlaces = toolStripComboBoxDecimalPlaces.SelectedIndex - 1;
        }

        private void thousandsSeparatorToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
            => ThonsandsSeparator = thousandsSeparatorToolStripMenuItem.Checked;

        private void toolStripMenuItemRestrictLimit_CheckedChanged(object sender, EventArgs e)
        {
            toolStripMenuItem1.Enabled = toolStripMenuItem2.Enabled = toolStripMenuItemRestrictLimit.Checked;
            RestrictLimitValue = toolStripMenuItemRestrictLimit.Checked;
        }

        private void toolStripTextBoxMaximum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == '\b');
        }

        private void allowMouseContlolToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            toolStripMenuItem3.Enabled = toolStripMenuItem4.Enabled = allowMouseContlolToolStripMenuItem.Checked;
        }

        private void contextMenuStripBody_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            double.TryParse(toolStripTextBoxMimimum.Text, out double min);
            if (min < Maximum && Minimum != min)
            {
                Minimum = min;
                LimitChanged?.Invoke(this, e);
            }
            double.TryParse(toolStripTextBoxMaximum.Text, out double max);
            if (max > Minimum && Maximum != max)
            {
                Maximum = max;
                LimitChanged?.Invoke(this, e);
            }

            double.TryParse(toolStripTextBoxMouseSpeed.Text, out double speed);
            if (speed > 0)
                mouseSpeed = speed;
        }

        #region マウスコントロールモード

        // 260426Cl 修正: 文字化けしていたコメント (旧: WFO1000 関連の壊れたコメント) を整理
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowMouseControl { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public VH_DirectionEnum MouseDirection
        {
            get => toolStripComboBoxMouseDirection.SelectedIndex == 0 ? VH_DirectionEnum.Vertical : VH_DirectionEnum.Horizontal;
            set => toolStripComboBoxMouseDirection.SelectedIndex = VH_DirectionEnum.Vertical == value ? 0 : 1;
        }

        private double mouseSpeed = 1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public double MouseSpeed
        {
            set
            {
                if (value > 0)
                {
                    mouseSpeed = value;
                    toolStripTextBoxMouseSpeed.Text = mouseSpeed.ToString();
                }
            }
            get => mouseSpeed;
        }

        // 260426Cl 整理: new Point() → 既定値の構造体に簡略化
        private Point justBeforeMousePosition;
        private bool mouseMoving = false;

        private void textBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle && mouseMoving && AllowMouseControl)
            {
                int delta = MouseDirection == VH_DirectionEnum.Vertical ? justBeforeMousePosition.Y - e.Y : e.X - justBeforeMousePosition.X;

                Value += delta * MouseSpeed;

                justBeforeMousePosition = e.Location;
            }
        }

        private void textBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                mouseMoving = true;
                justBeforeMousePosition = e.Location;
            }
        }

        private void textBox_MouseUp(object sender, MouseEventArgs e) => mouseMoving = false;


        #endregion マウスコントロールモード

        private void contextMenuStripBody_Opening(object sender, CancelEventArgs e)
        {
            skipEvent = true;
            toolStripComboBoxDecimalPlaces.SelectedIndex = DecimalPlaces >= 0 ? DecimalPlaces + 1 : 0;
            toolStripTextBoxMaximum.Text = Maximum.ToString();
            toolStripTextBoxMimimum.Text = Minimum.ToString();
            skipEvent = false;
        }
    }
}

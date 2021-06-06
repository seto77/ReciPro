using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class NumericBoxWithMenu : NumericBox
    {

        bool skipEvent { get; set; } = false;

        public event MyEventHandler LimitChanged;
        public NumericBoxWithMenu()
        {
            InitializeComponent();

            textBox.ContextMenuStrip = contextMenuStripBody;
            numericUpDown.ContextMenuStrip = contextMenuStripUpDown;

            textBox.MouseUp += textBox_MouseUp;
            textBox.MouseMove += textBox_MouseMove;
            textBox.MouseDown += textBox_MouseDown;

            toolStripComboBoxMouseDirection.SelectedIndex = 0;
        }

        private void smartIncrementToolStripMenuItem_CheckedChanged(object sender, EventArgs e) 
            => SmartIncrement = smartIncrementToolStripMenuItem.Checked;

        private void toolStripComboBoxIncrement_SelectedIndexChanged(object sender, EventArgs e)
        {
            double.TryParse(toolStripComboBoxIncrement.Text, out double inc); ;
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

        public bool AllowMouseControl { get; set; } = false;

        public VH_DirectionEnum MouseDirection
        {
            get { return toolStripComboBoxMouseDirection.SelectedIndex == 0 ? VH_DirectionEnum.Vertical : VH_DirectionEnum.Horizontal; }
            set { toolStripComboBoxMouseDirection.SelectedIndex = VH_DirectionEnum.Vertical == value ? 0 : 1; }
        }

        private double mouseSpeed = 1;

        public double MouseSpeed
        {
            set
            {
                if (value > 0)
                {
                    mouseSpeed = value;
                    string text = mouseSpeed.ToString();
                    //text = separateThousands(text);
                    toolStripTextBoxMouseSpeed.Text = text;
                }
            }
            get { return mouseSpeed; }
        }

        private Point justBeforeMousePosition = new Point();
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

        private void textBox_MouseUp(object sender, MouseEventArgs e)
        {
            mouseMoving = false;
        }


        #endregion マウスコントロールモード

        private void contextMenuStripBody_Opening(object sender, CancelEventArgs e)
        {
            skipEvent = true;
            toolStripComboBoxDecimalPlaces.SelectedIndex = DecimalPlaces >= 0 ? DecimalPlaces+1: 0;
            toolStripTextBoxMaximum.Text = Maximum.ToString();
            toolStripTextBoxMimimum.Text = Minimum.ToString();
            skipEvent = false;
        }
    }
}

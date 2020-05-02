using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class ColorControl : UserControl
    {
        public event EventHandler ColorChanged;

        [Category("Appearance")]
        public FlowDirection FlowDirection
        {
            get => flowLayoutPanel.FlowDirection;
            set
            {
                flowLayoutPanel.FlowDirection = value;
                pictureBox.Dock = (value == FlowDirection.TopDown || value == FlowDirection.BottomUp) ?
                DockStyle.Right : DockStyle.None;
            }
        }

        [Category("Appearance")]
        public Size BoxSize { get => pictureBox.Size; set => pictureBox.Size = value; }
        public string ToolTip { set => toolTip.SetToolTip(pictureBox, value); get => toolTip.GetToolTip(pictureBox); }

      
        [Category("Header/footer text")]
        public string HeaderText { set { labelHeader.Text = value; labelHeader.Visible = value != ""; } get => labelHeader.Text; }
     
        [Category("Header/footer text")]
        public Font HeaderFont { set => labelHeader.Font = value; get => labelHeader.Font; }
   
        [Category("Header/footer text")]
        public Padding HeaderMargin { set => labelHeader.Margin = value; get => labelHeader.Margin; }



        [Category("Header/footer text")]
        public string FooterText {set{ labelFooter.Text = value; labelFooter.Visible = value != ""; } get => labelFooter.Text; }
        [Category("Header/footer text")]
        public Font FooterFont { set => labelFooter.Font = value; get => labelFooter.Font; }

        [Category("Header/footer text")]
        public Padding FooterMargin { set => labelFooter.Margin = value; get => labelFooter.Margin; }


        public SolidBrush SolidBrush
        {
            set => pictureBox.BackColor = value.Color; get => new SolidBrush(pictureBox.BackColor);
        }

        [Category("Color")]
        public Color Color { set => pictureBox.BackColor = value; get => pictureBox.BackColor; }


        [Category("Color")]

        public int Argb { set => pictureBox.BackColor = Color.FromArgb(value); get => pictureBox.BackColor.ToArgb(); }

        [Category("Color")]
        public int Red
        {
            set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(value, pictureBox.BackColor.G, pictureBox.BackColor.B); }
            get { return pictureBox.BackColor.R; }
        }

        [Category("Color")]
        public int Green
        {
            set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, value, pictureBox.BackColor.B); }
            get { return pictureBox.BackColor.G; }
        }

        [Category("Color")]
        public int Blue
        {
            set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, pictureBox.BackColor.G, value); }
            get { return pictureBox.BackColor.B; }
        }

        [Category("Color")]
        public float RedF
        {
            set { if (value >= 0 && value <= 1) pictureBox.BackColor = Color.FromArgb((int)(value * 255), pictureBox.BackColor.G, pictureBox.BackColor.B); }
            get { return pictureBox.BackColor.R / 255f; }
        }

        [Category("Color")]
        public float GreenF
        {
            set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, (int)(value * 255), pictureBox.BackColor.B); }
            get { return pictureBox.BackColor.G / 255f; }
        }

        [Category("Color")]
        public float BlueF
        {
            set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, pictureBox.BackColor.G, (int)(value * 255)); }
            get { return pictureBox.BackColor.B / 255f; }
        }

        public ColorControl()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog
            {
                Color = pictureBox.BackColor,
                AllowFullOpen = true,
                AnyColor = true,
                SolidColorOnly = false,
                ShowHelp = true
            };
            colorDialog.ShowDialog();
            pictureBox.BackColor = colorDialog.Color;
        }

        private void pictureBox_BackColorChanged(object sender, EventArgs e)
        {
            ColorChanged?.Invoke(sender, e);
        }
    }
}
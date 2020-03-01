using System;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class ColorControl : UserControl
    {
        public delegate void MyEventHandler(object sender, EventArgs e);

        public event MyEventHandler ColorChanged;

        public String ToolTip
        {
            set { toolTip.SetToolTip(pictureBox, value); }
            get { return toolTip.GetToolTip(pictureBox); }
        }

        public Color Color
        {
            set { pictureBox.BackColor = value; }
            get { return pictureBox.BackColor; }
        }

        public string FooterText
        {
            set => label.Text = value;
            get => label.Text;
        }
        public Font FooterFont
        {
            set => label.Font = value;
            get => label.Font;
        }

        public SolidBrush SolidBrush
        {
            set { pictureBox.BackColor = value.Color; }
            get { return new SolidBrush(pictureBox.BackColor); }
        }

        public int Argb
        {
            set { pictureBox.BackColor = Color.FromArgb(value); }
            get { return pictureBox.BackColor.ToArgb(); }
        }

        public int Red
        {
            set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(value, pictureBox.BackColor.G, pictureBox.BackColor.B); }
            get { return pictureBox.BackColor.R; }
        }

        public int Green
        {
            set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, value, pictureBox.BackColor.B); }
            get { return pictureBox.BackColor.G; }
        }

        public int Blue
        {
            set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, pictureBox.BackColor.G, value); }
            get { return pictureBox.BackColor.B; }
        }

        public float RedF
        {
            set { if (value >= 0 && value <= 1) pictureBox.BackColor = Color.FromArgb((int)(value * 255), pictureBox.BackColor.G, pictureBox.BackColor.B); }
            get { return pictureBox.BackColor.R / 255f; }
        }

        public float GreenF
        {
            set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, (int)(value * 255), pictureBox.BackColor.B); }
            get { return pictureBox.BackColor.G / 255f; }
        }

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
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = pictureBox.BackColor;
            colorDialog.AllowFullOpen = true;
            colorDialog.AnyColor = true;
            colorDialog.SolidColorOnly = false;
            colorDialog.ShowHelp = true;
            colorDialog.ShowDialog();
            pictureBox.BackColor = colorDialog.Color;
        }

        private void pictureBox_BackColorChanged(object sender, EventArgs e)
        {
            if (ColorChanged != null)
                ColorChanged(sender, e);
        }
    }
}
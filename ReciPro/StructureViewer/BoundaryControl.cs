using Crystallography;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReciPro
{
    public partial class BoundsControl : UserControl
    {
        public Crystal Crystal { get; set; }
        public new bool Enabled { get => checkBoxEnable.Checked; set => checkBoxEnable.Checked = value; }

        public bool FullOption { get => checkBoxEquivalency.Visible; set => checkBoxEquivalency.Visible = label1.Visible = value; }

        public Color Color { get => colorControl.Color; set => colorControl.Color = value; }

        public double Distance { get => numericBoxDistance.Value; set => numericBoxDistance.Value = value; }
        public bool Equivalency { get => checkBoxEquivalency.Checked; set => checkBoxEquivalency.Checked = value; }

        public event EventHandler Delete;

        public event EventHandler Changed;

        public event EventHandler ColorChanged;

        public Bound Bound
        {
            get => new Bound(
                    Crystal,
                    numericBoxH.ValueInteger,
                    numericBoxK.ValueInteger,
                    numericBoxL.ValueInteger,
                    checkBoxEquivalency.Checked,
                    numericBoxDistance.Value,
                    colorControl.Color.ToArgb());
            set
            {
                skipEvent = true;
                checkBoxEquivalency.Checked = value.Equivalency;
                numericBoxH.Value = value.BaseIndex.H;
                numericBoxK.Value = value.BaseIndex.K;
                numericBoxL.Value = value.BaseIndex.L;
                skipEvent = false;
                numericBoxDistance.Value = value.Distance;
                colorControl.Color = value.Color;
            }
        }

        public BoundsControl()
        {
            InitializeComponent();
        }
        public BoundsControl(Crystal crystal) : base()
        {
            InitializeComponent();
            Crystal = crystal;
        }

        private void buttonDelete_Click(object sender, EventArgs e) => Delete?.Invoke(this, e);

        private bool skipEvent = false;

        private void ValueChanged(object sender, EventArgs e)
        {
            if (skipEvent) return;

            Changed?.Invoke(this, e);
        }

        private void colorControl_ColorChanged(object sender, EventArgs e)
        {
            if (skipEvent) return;

            ColorChanged?.Invoke(this, e);
        }


        private void checkBoxEquivalency_CheckedChanged(object sender, EventArgs e)
        {
            if (skipEvent) return;

            checkBoxEquivalency.Text = checkBoxEquivalency.Checked ? "{" : "(";
            label1.Text = checkBoxEquivalency.Checked ? "}" : ")";
            Changed?.Invoke(this, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
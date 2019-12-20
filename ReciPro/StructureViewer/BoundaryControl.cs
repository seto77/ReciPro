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

        public bool FullOption { get { return checkBoxEquivalency.Visible; } set { checkBoxEquivalency.Visible = radioButtonAngstrom.Visible = radioButtonDspacing.Visible = label1.Visible = value; } }

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
                    radioButtonAngstrom.Checked ? numericBoxDistance.Value / 10 : numericBoxDistance.Value,
                    radioButtonDspacing.Checked ? Bound.UnitEnum.D_spacing : Bound.UnitEnum.Angstrom,
                    colorControl.Color.ToArgb());
            set
            {
                skipEvent = true;
                checkBoxEquivalency.Checked = value.Equivalency;
                numericBoxH.Value = value.BaseIndex.H;
                numericBoxK.Value = value.BaseIndex.K;
                numericBoxL.Value = value.BaseIndex.L;
                radioButtonDspacing.Checked = value.Unit == Bound.UnitEnum.D_spacing;
                radioButtonAngstrom.Checked = value.Unit == Bound.UnitEnum.Angstrom;
                skipEvent = false;
                numericBoxDistance.Value = value.Unit == Bound.UnitEnum.D_spacing ? value.Distance : value.Distance * 10;
                colorControl.Color = value.Color;
            }
        }

        public BoundsControl(Crystal crystal) : base()
        {
            InitializeComponent();
            Crystal = crystal;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Delete?.Invoke(this, e);
        }

        private bool skipEvent = false;

        private void ValueChanged(object sender, EventArgs e)
        {
            if (skipEvent) return;
            radioButtonDspacing.Text = "d (" + (10 * Bound.D).ToString("0.000") + " Å)";

            Changed?.Invoke(this, e);
        }

        private void colorControl_ColorChanged(object sender, EventArgs e)
        {
            if (skipEvent) return;

            ColorChanged?.Invoke(this, e);
        }

        private void radioButtonAngstrom_CheckedChanged(object sender, EventArgs e)
        {
            if (skipEvent) return;

            if (radioButtonAngstrom.Checked)
                numericBoxDistance.Value *= Bound.D * 10;
            else
                numericBoxDistance.Value /= Bound.D * 10;
        }

        private void checkBoxEquivalency_CheckedChanged(object sender, EventArgs e)
        {
            if (skipEvent) return;

            checkBoxEquivalency.Text = checkBoxEquivalency.Checked ? "{" : "(";
            label1.Text = checkBoxEquivalency.Checked ? "}" : ")";
            Changed?.Invoke(this, e);
        }
    }
}
using System;
using System.Windows.Forms;

namespace ReciPro
{
    public partial class InputBox : UserControl
    {
        public InputBox() => InitializeComponent();

        private double waveLength = 0;

        public double WaveLength
        {
            set => waveLength = value;
            get => waveLength;
        }

        private double cameraLength = 0;

        public double CameraLength
        {
            set => cameraLength = value;
            get => cameraLength;
        }

        public double Length
        {
            set => numericalTextBoxLength.Value = value;
            get => numericalTextBoxLength.Value;
        }

        public delegate void MyEventHandler(object sender, EventArgs e);

        public event MyEventHandler ValueChanged;

        private void numericalTextBoxlength_Click(object sender, EventArgs e)
        {
            numericalTextBoxLength.ReadOnly = false;
            numericalTextBoxGlength.ReadOnly = true;
            numericalTextBoxDvalue.ReadOnly = true;
        }

        private void numericalTextBoxDvalue_Click(object sender, EventArgs e)
        {
            numericalTextBoxLength.ReadOnly = true;
            numericalTextBoxGlength.ReadOnly = true;
            numericalTextBoxDvalue.ReadOnly = false;
        }

        private void numericalTextBoxGlength_Click(object sender, EventArgs e)
        {
            numericalTextBoxLength.ReadOnly = true;
            numericalTextBoxGlength.ReadOnly = false;
            numericalTextBoxDvalue.ReadOnly = true;
        }

        private bool skipValueChangedEvent = false;

        private void numericalTextBoxLength_ValueChanged(object sender, EventArgs e)
        {
            if (skipValueChangedEvent) return;
            skipValueChangedEvent = true;
            numericalTextBoxDvalue.Value = 10 * waveLength / 2 / Math.Sin(Math.Atan(numericalTextBoxLength.Value / cameraLength) / 2);
            numericalTextBoxGlength.Value = 1 / numericalTextBoxDvalue.Value * 10;
            skipValueChangedEvent = false;
            ValueChanged(this, e);
        }

        private void numericalTextBoxDvalue_ValueChanged(object sender, EventArgs e)
        {
            if (skipValueChangedEvent) return;
            skipValueChangedEvent = true;
            numericalTextBoxLength.Value = cameraLength * Math.Tan(2 * Math.Asin(waveLength / 2 / numericalTextBoxDvalue.Value * 10));
            numericalTextBoxGlength.Value = 1 / numericalTextBoxDvalue.Value * 10;
            skipValueChangedEvent = false;
            ValueChanged(this, e);
        }

        private void numericalTextBoxGlength_ValueChanged(object sender, EventArgs e)
        {
            if (skipValueChangedEvent) return;
            skipValueChangedEvent = true;
            numericalTextBoxDvalue.Value = 1 / numericalTextBoxGlength.Value * 10;
            numericalTextBoxLength.Value = cameraLength * Math.Tan(2 * Math.Asin(waveLength / 2 / numericalTextBoxDvalue.Value * 10));
            skipValueChangedEvent = false;
            ValueChanged(this, e);
        }
    }
}
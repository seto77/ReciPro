using System;
using System.Windows.Forms;

namespace ReciPro;

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
        set => numericBoxLength.Value = value;
        get => numericBoxLength.Value;
    }

    public delegate void MyEventHandler(object sender, EventArgs e);

    public event MyEventHandler ValueChanged;

    private void numericBoxlength_Click(object sender, EventArgs e)
    {
        numericBoxLength.ReadOnly = false;
        numericBoxGlength.ReadOnly = true;
        numericBoxDvalue.ReadOnly = true;
    }

    private void numericBoxDvalue_Click(object sender, EventArgs e)
    {
        numericBoxLength.ReadOnly = true;
        numericBoxGlength.ReadOnly = true;
        numericBoxDvalue.ReadOnly = false;
    }

    private void numericBoxGlength_Click(object sender, EventArgs e)
    {
        numericBoxLength.ReadOnly = true;
        numericBoxGlength.ReadOnly = false;
        numericBoxDvalue.ReadOnly = true;
    }

    private bool skipValueChangedEvent = false;

    private void numericBoxLength_ValueChanged(object sender, EventArgs e)
    {
        if (skipValueChangedEvent) return;
        skipValueChangedEvent = true;
        numericBoxDvalue.Value = 10 * waveLength / 2 / Math.Sin(Math.Atan(numericBoxLength.Value / cameraLength) / 2);
        numericBoxGlength.Value = 1 / numericBoxDvalue.Value * 10;
        skipValueChangedEvent = false;
        ValueChanged(this, e);
    }

    private void numericBoxDvalue_ValueChanged(object sender, EventArgs e)
    {
        if (skipValueChangedEvent) return;
        skipValueChangedEvent = true;
        numericBoxLength.Value = cameraLength * Math.Tan(2 * Math.Asin(waveLength / 2 / numericBoxDvalue.Value * 10));
        numericBoxGlength.Value = 1 / numericBoxDvalue.Value * 10;
        skipValueChangedEvent = false;
        ValueChanged(this, e);
    }

    private void numericBoxGlength_ValueChanged(object sender, EventArgs e)
    {
        if (skipValueChangedEvent) return;
        skipValueChangedEvent = true;
        numericBoxDvalue.Value = 1 / numericBoxGlength.Value * 10;
        numericBoxLength.Value = cameraLength * Math.Tan(2 * Math.Asin(waveLength / 2 / numericBoxDvalue.Value * 10));
        skipValueChangedEvent = false;
        ValueChanged(this, e);
    }
}

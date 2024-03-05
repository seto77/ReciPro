using System.Windows.Forms;

namespace ReciPro;

public partial class InputBox : UserControl
{
    public InputBox() => InitializeComponent();

    public double WaveLength { get; set; }

    public double CameraLength { get; set; } = 0;

    public bool enabled = true;
    new public bool Enabled
    {
        set
        {
            if (!value)
            {
                numericBoxLength.ReadOnly = true;
                numericBoxGlength.ReadOnly = true;
                numericBoxDvalue.ReadOnly = true;
            }
        }
    }

    public double Length
    {
        set => numericBoxLength.Value = value;
        get => numericBoxLength.Value;
    }

    public delegate void MyEventHandler(object sender, EventArgs e);

    public event MyEventHandler ValueChanged;
    public event MyEventHandler Click2;

    private void numericBoxlength_Click(object sender, EventArgs e)
    {
        Click2?.Invoke(this, EventArgs.Empty);

        numericBoxLength.ReadOnly = false;
        numericBoxGlength.ReadOnly = true;
        numericBoxDvalue.ReadOnly = true;
    }

    private void numericBoxDvalue_Click(object sender, EventArgs e)
    {
        Click2?.Invoke(this, EventArgs.Empty);

        numericBoxLength.ReadOnly = true;
        numericBoxGlength.ReadOnly = true;
        numericBoxDvalue.ReadOnly = false;
    }

    private void numericBoxGlength_Click(object sender, EventArgs e)
    {
        Click2?.Invoke(this, EventArgs.Empty);

        numericBoxLength.ReadOnly = true;
        numericBoxGlength.ReadOnly = false;
        numericBoxDvalue.ReadOnly = true;
    }

    private bool skipValueChangedEvent = false;

    private void numericBoxLength_ValueChanged(object sender, EventArgs e)
    {
        if (skipValueChangedEvent) return;
        skipValueChangedEvent = true;
        numericBoxDvalue.Value = 10 * WaveLength / 2 / Math.Sin(Math.Atan(numericBoxLength.Value / CameraLength) / 2);
        numericBoxGlength.Value = 1 / numericBoxDvalue.Value * 10;
        skipValueChangedEvent = false;
        ValueChanged(this, e);
    }

    private void numericBoxDvalue_ValueChanged(object sender, EventArgs e)
    {
        if (skipValueChangedEvent) return;
        skipValueChangedEvent = true;
        numericBoxLength.Value = CameraLength * Math.Tan(2 * Math.Asin(WaveLength / 2 / numericBoxDvalue.Value * 10));
        numericBoxGlength.Value = 1 / numericBoxDvalue.Value * 10;
        skipValueChangedEvent = false;
        ValueChanged(this, e);
    }

    private void numericBoxGlength_ValueChanged(object sender, EventArgs e)
    {
        if (skipValueChangedEvent) return;
        skipValueChangedEvent = true;
        numericBoxDvalue.Value = 1 / numericBoxGlength.Value * 10;
        numericBoxLength.Value = CameraLength * Math.Tan(2 * Math.Asin(WaveLength / 2 / numericBoxDvalue.Value * 10));
        skipValueChangedEvent = false;
        ValueChanged(this, e);
    }

    private void label50_Click(object sender, EventArgs e)
    {
        Click2?.Invoke(this, EventArgs.Empty);
    }

    private void InputBox_Click(object sender, EventArgs e)
    {
        Click2?.Invoke(this, EventArgs.Empty);
    }
}

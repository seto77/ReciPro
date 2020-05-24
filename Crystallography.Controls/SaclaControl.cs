using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class SaclaControl : UserControl
    {
        public delegate void MyEventHandler(object sender, EventArgs e);

        public event MyEventHandler ValueChanged;

        public double PixelSize { set { numericBoxPixelSize.Value = value; } get { return numericBoxPixelSize.Value; } }
        public double PixelWidth { set { numericBoxPixelWidth.Value = value; } get { return numericBoxPixelWidth.Value; } }
        public double PixelHeight { set { numericBoxPixelHeight.Value = value; } get { return numericBoxPixelHeight.Value; } }
        public double TauRadian { set { numericBoxTau.RadianValue = value; } get { return numericBoxTau.RadianValue; } }
        public double TauDegree { set { numericBoxTau.Value = value; } get { return numericBoxTau.Value; } }
        public double PhiRadian { set { numericBoxPhi.RadianValue = value; } get { return numericBoxPhi.RadianValue; } }
        public double PhiDegree { set { numericBoxPhi.Value = value; } get { return numericBoxPhi.Value; } }
        public PointD Foot { set { numericBoxFootX.Value = value.X; numericBoxFootY.Value = value.Y; } get { return new PointD(numericBoxFootX.Value, numericBoxFootY.Value); } }
        public double CameraLength2 { set { numericBoxDistance.Value = value; } get { return numericBoxDistance.Value; } }

        public SaclaControl()
        {
            InitializeComponent();
        }

        private void numericBoxPixelWidth_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, e);
        }
    }
}
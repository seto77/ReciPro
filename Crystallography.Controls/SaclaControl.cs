using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class SaclaControl : CaptureUserControlBase
    {
        public delegate void MyEventHandler(object sender, EventArgs e);

        public event MyEventHandler ValueChanged;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public double PixelSize { set { numericBoxPixelSize.Value = value; } get { return numericBoxPixelSize.Value; } }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public double PixelWidth { set { numericBoxPixelWidth.Value = value; } get { return numericBoxPixelWidth.Value; } }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public double PixelHeight { set { numericBoxPixelHeight.Value = value; } get { return numericBoxPixelHeight.Value; } }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public double TauRadian { set { numericBoxTau.RadianValue = value; } get { return numericBoxTau.RadianValue; } }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public double TauDegree { set { numericBoxTau.Value = value; } get { return numericBoxTau.Value; } }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public double PhiRadian { set { numericBoxPhi.RadianValue = value; } get { return numericBoxPhi.RadianValue; } }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public double PhiDegree { set { numericBoxPhi.Value = value; } get { return numericBoxPhi.Value; } }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public PointD Foot { set { numericBoxFootX.Value = value.X; numericBoxFootY.Value = value.Y; } get { return new PointD(numericBoxFootX.Value, numericBoxFootY.Value); } }
        // (260322Ch) WFO1000: Microsoft ??????????????????? ???????????
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public double CameraLength2 { set { numericBoxDistance.Value = value; } get { return numericBoxDistance.Value; } }

        public SaclaControl()
        {
            InitializeComponent();
        }

        private void numericBoxPixelWidth_ValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }
    }
}

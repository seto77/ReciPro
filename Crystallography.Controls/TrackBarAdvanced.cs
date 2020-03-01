using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class TrackBarAdvanced : UserControl
    {
        public TrackBarAdvanced()
        {
            InitializeComponent();
            this.Resize += TrackBarAdvanced_Resize;
        }

        private void TrackBarAdvanced_Resize(object sender, EventArgs e)
        {
            trackBar.Location = new Point(0, 0);
            trackBar.Size = splitContainer.Panel2.ClientSize;

            numericBox.Location = new Point(0, 0);
            numericBox.Width = splitContainer.Panel1.ClientSize.Width;

            /* if (Orientation == Orientation.Vertical)
             {
                 MinimumSize = new Size(1, numericBox.Height+2);
                 MaximumSize = new Size(1000, numericBox.Height+2);
             }
             else
             {
                 MinimumSize = new Size(1, numericBox.Height);
                 MaximumSize = new Size(1000, numericBox.Height*3);
             }*/
        }

        #region プロパティ

        //public int SmallChange { get { return trackBar.SmallChange; } set { trackBar.SmallChange = value; } }
        //public int LargeChange { get { return trackBar.LargeChange; } set { trackBar.LargeChange = value; } }
        //public int TickFrequency { get { return trackBar.TickFrequency; } set { trackBar.TickFrequency = value; } }
        //public int Increment { get { return (int)numericBox.Increment; } set { numericBox.Increment = (decimal)value; } }

        private bool logScrollBar = false;

        /// <summary>
        /// スクロールバーがログスケールで動くかどうか
        /// </summary>
        public bool LogScrollBar
        {
            set { logScrollBar = value; }
            get { return logScrollBar; }
        }

        public int DecimalPlaces { get { return numericBox.DecimalPlaces; } set { numericBox.DecimalPlaces = value; } }

        [Localizable(true)]
        public string HeaderText { get { return numericBox.HeaderText; } set { numericBox.HeaderText = value; } }

        [Localizable(true)]
        public Font HeaderFont { get { return numericBox.HeaderFont; } set { numericBox.HeaderFont = value; } }

        [Localizable(true)]
        public string FooterText { get { return numericBox.FooterText; } set { numericBox.FooterText = value; } }

        [Localizable(true)]
        public Font FooterFont { get { return numericBox.FooterFont; } set { numericBox.FooterFont = value; } }

        public int NumericBoxSize { get { return splitContainer.SplitterDistance; } set { splitContainer.SplitterDistance = value; } }

        public Orientation Orientation { get { return splitContainer.Orientation; } set { splitContainer.Orientation = value; } }

        public int ControlHeight { get { return this.Height; } set { this.Height = value; } }

        public TickStyle TickStyle { get { return trackBar.TickStyle; } set { trackBar.TickStyle = value; } }

        private double _value = 0;

        public double Value
        {
            get { return _value; }
            set
            {
                try
                {
                    if (value > Maximum) value = Maximum;
                    else if (value < Minimum) value = Minimum;
                    _value = value;

                    if (!SkipTrackBarEvent)
                    {
                        SkipTrackBarEvent = true;
                        try
                        {
                            if (!LogScrollBar)
                                trackBar.Value = (int)((value - Minimum) / (Maximum - Minimum) * trackBar.Maximum + 0.5);
                            else
                            {
                                if (Minimum < 0 && Maximum > 0)//最大が0以上、最小値が0以下の場合
                                {
                                    double center = trackBar.Maximum * Math.Log(-numericBox.Minimum) / (Math.Log(numericBox.Maximum) + Math.Log(-numericBox.Minimum));

                                    if (value >= 0)
                                        trackBar.Value = (int)(Math.Log(value, Maximum) * (trackBar.Maximum - center) + center + 0.5);
                                    else
                                        trackBar.Value = (int)(center * (1 - Math.Log(-value, -Minimum)) + 0.5);
                                }
                                else
                                {
                                    trackBar.Value = (int)(Math.Log(value - Minimum, Maximum - Minimum) * trackBar.Maximum + 0.5);
                                }
                            }

                            SkipTrackBarEvent = false;
                        }
                        catch { SkipTrackBarEvent = false; }
                    }

                    if (!SkipNumericBoxEvent)
                    {
                        SkipNumericBoxEvent = true;
                        numericBox.Value = value;
                        SkipNumericBoxEvent = false;
                    }
                }
                catch { }
            }
        }

        //private double maximum = 65535;
        public double Maximum
        {
            get { return numericBox.Maximum; }
            set
            {
                if (value < Minimum)
                    value = Minimum;
                numericBox.Maximum = value;
            }
        }

        //private double minimum = 0;
        public double Minimum
        {
            get { return numericBox.Minimum; }
            set
            {
                if (value > Maximum)
                    value = Maximum;
                numericBox.Minimum = value;
            }
        }

        public double UpDown_Increment
        { get => numericBox.UpDown_Increment; set => numericBox.UpDown_Increment = value; }

        public bool Smart_Increment
        { get => numericBox.SmartIncrement; set => numericBox.SmartIncrement = value; }

        #endregion プロパティ

        #region イベント

        public delegate bool ValueChangedDelegate(object sender, double value);

        public event ValueChangedDelegate ValueChanged;

        #endregion イベント

        public bool SkipTrackBarEvent = false;

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (SkipTrackBarEvent) return;
            SkipTrackBarEvent = true;

            if (!logScrollBar)
                Value = (numericBox.Maximum - numericBox.Minimum) * ((double)trackBar.Value / trackBar.Maximum) + numericBox.Minimum;
            else
            {//logモードの時
                if (numericBox.Minimum < 0 && numericBox.Maximum > 0)//最大が0以上、最小値が0以下の場合
                {
                    int center = (int)(trackBar.Maximum * Math.Log(-numericBox.Minimum) / (Math.Log(numericBox.Maximum) + Math.Log(-numericBox.Minimum)) + 0.5);

                    if (trackBar.Value > center)
                        Value = Math.Pow(numericBox.Maximum, (double)(trackBar.Value - center) / (trackBar.Maximum - center));
                    else
                        Value = -Math.Pow(-numericBox.Minimum, (double)(center - trackBar.Value) / (center));
                }
                else
                {
                    Value = Math.Pow((numericBox.Maximum - numericBox.Minimum), (double)trackBar.Value / trackBar.Maximum) + numericBox.Minimum;
                }
            }
            if (ValueChanged != null)
                ValueChanged(this, Value);

            SkipTrackBarEvent = false;
        }

        public bool SkipNumericBoxEvent = false;

        private void numericBox_ValueChanged(object sender, EventArgs e)
        {
            if (SkipNumericBoxEvent) return;
            SkipNumericBoxEvent = true;
            Value = numericBox.Value;

            if (ValueChanged != null)
                ValueChanged(this, Value);

            SkipNumericBoxEvent = false;
        }

        private void TrackBarAdvanced_Load(object sender, EventArgs e)
        {
            this.TrackBarAdvanced_Resize(sender, e);
        }
    }
}
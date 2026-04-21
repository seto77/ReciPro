using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls;

// 260421Cl 追加: 六方晶/三方晶の Miller-Bravais 4 指数表記に対応するための HKL 入力コントロール。
// 3 指数モード (h, k, l) と 4 指数モード (h, k, i=-(h+k), l) を ShowIIndex で切替える。
// i は自動計算の読取専用表示。
[TypeConverter(typeof(DefinitionOrderTypeConverter))]
[DefaultEvent("ValueChanged")]
public partial class HKLControl : UserControl
{
    public event EventHandler ValueChanged;

    public HKLControl()
    {
        InitializeComponent();
        numericBoxH.ValueChanged += (s, e) => { UpdateIDisplay(); ValueChanged?.Invoke(this, EventArgs.Empty); };
        numericBoxK.ValueChanged += (s, e) => { UpdateIDisplay(); ValueChanged?.Invoke(this, EventArgs.Empty); };
        numericBoxL.ValueChanged += (s, e) => ValueChanged?.Invoke(this, EventArgs.Empty);
        UpdateIDisplay();
    }

    private void UpdateIDisplay() => labelI.Text = (-(numericBoxH.Value + numericBoxK.Value)).ToString("0");

    [Category("Value properties"), DefaultValue(0.0)]
    public double H { get => numericBoxH.Value; set => numericBoxH.Value = value; }

    [Category("Value properties"), DefaultValue(0.0)]
    public double K { get => numericBoxK.Value; set => numericBoxK.Value = value; }

    [Category("Value properties"), DefaultValue(0.0)]
    public double L { get => numericBoxL.Value; set => numericBoxL.Value = value; }

    [Browsable(false)]
    public double I => -(H + K);

    /// <summary>i インデックスを表示するかどうか (六方/三方 hexagonal axes のみ true)</summary>
    [Category("Appearance properties"), DefaultValue(false)]
    public bool ShowIIndex
    {
        get => showIIndex;
        set
        {
            if (showIIndex == value) return;
            showIIndex = value;
            labelI.Visible = value;
            // TableLayoutPanel の列幅は SuspendLayout 下で切替える
            tableLayoutPanel.SuspendLayout();
            if (value)
            {
                tableLayoutPanel.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 25F);
                tableLayoutPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 25F);
                tableLayoutPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 25F);
                tableLayoutPanel.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 25F);
            }
            else
            {
                tableLayoutPanel.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0F);
                tableLayoutPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 33.33333F);
                tableLayoutPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 33.33333F);
                tableLayoutPanel.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 33.33333F);
            }
            tableLayoutPanel.ResumeLayout();
            UpdateIDisplay();
        }
    }
    private bool showIIndex = false;

    [Category("Value properties"), DefaultValue(double.PositiveInfinity)]
    public double Maximum
    {
        get => numericBoxH.Maximum;
        set { numericBoxH.Maximum = value; numericBoxK.Maximum = value; numericBoxL.Maximum = value; }
    }

    [Category("Value properties"), DefaultValue(double.NegativeInfinity)]
    public double Minimum
    {
        get => numericBoxH.Minimum;
        set { numericBoxH.Minimum = value; numericBoxK.Minimum = value; numericBoxL.Minimum = value; }
    }

    [Category("Appearance properties"), DefaultValue(false)]
    public bool ShowUpDown
    {
        get => numericBoxH.ShowUpDown;
        set { numericBoxH.ShowUpDown = value; numericBoxK.ShowUpDown = value; numericBoxL.ShowUpDown = value; }
    }

    [Category("Appearance properties"), DefaultValue(-1)]
    public int DecimalPlaces
    {
        get => numericBoxH.DecimalPlaces;
        set { numericBoxH.DecimalPlaces = value; numericBoxK.DecimalPlaces = value; numericBoxL.DecimalPlaces = value; }
    }

    [Category("Appearance properties"), DefaultValue(false)]
    public bool ReadOnly
    {
        get => numericBoxH.ReadOnly;
        set { numericBoxH.ReadOnly = value; numericBoxK.ReadOnly = value; numericBoxL.ReadOnly = value; }
    }

    [Category("Appearance properties"), DefaultValue(false)]
    public bool ThousandsSeparator
    {
        get => numericBoxH.ThonsandsSeparator;
        set { numericBoxH.ThonsandsSeparator = value; numericBoxK.ThonsandsSeparator = value; numericBoxL.ThonsandsSeparator = value; }
    }
}

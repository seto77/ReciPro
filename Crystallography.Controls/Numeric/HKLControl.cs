using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls;

// 260421Cl 追加: 六方晶/三方晶の Miller-Bravais 4 指数表記に対応するための HKL 入力コントロール。
// i 値 (= -(h+k)) は常時自動計算する。GUI 上の i 表示の表示/非表示は外部から labelI.Visible で切替える
// (デザイナ管理可能)。labelI.VisibleChanged に応じて tableLayoutPanel の列幅を自動調整する。
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
        // 260422Cl labelI.Visible 変化時に自動で列幅を調整 (表示時は4列均等25%、非表示時はi列を潰して3列均等33.3%)
        labelI.VisibleChanged += (s, e) => UpdateColumnStyles();
        UpdateIDisplay();
        UpdateColumnStyles();
    }

    // 260422Cl i 値は常時裏で計算 (Visible に関わらず labelI.Text を更新)。計算コストは無視できる。
    private void UpdateIDisplay() => labelI.Text = (-(numericBoxH.Value + numericBoxK.Value)).ToString("0");

    // 260422Cl labelI.Visible に合わせて列幅を切替 (表示=25%均等、非表示=i列0px+他3列33.3%均等)
    private void UpdateColumnStyles()
    {
        tableLayoutPanel.SuspendLayout();
        var visible = labelI.Visible;
        var pct = visible ? 25F : 33.33333F;
        tableLayoutPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, pct);
        tableLayoutPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, pct);
        tableLayoutPanel.ColumnStyles[2] = visible ? new ColumnStyle(SizeType.Percent, pct) : new ColumnStyle(SizeType.Absolute, 0F);
        tableLayoutPanel.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, pct);
        tableLayoutPanel.ResumeLayout();
    }

    [Category("Value properties"), DefaultValue(0.0)]
    public double H { get => numericBoxH.Value; set => numericBoxH.Value = value; }

    [Category("Value properties"), DefaultValue(0.0)]
    public double K { get => numericBoxK.Value; set => numericBoxK.Value = value; }

    [Category("Value properties"), DefaultValue(0.0)]
    public double L { get => numericBoxL.Value; set => numericBoxL.Value = value; }

    // 260422Cl 常に裏で計算される i 値 (-(h+k))。GUI 表示とは独立。
    [Browsable(false)]
    public double I => -(H + K);

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

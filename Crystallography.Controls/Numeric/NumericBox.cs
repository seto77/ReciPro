using MathNet.Numerics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls;

[TypeConverter(typeof(DefinitionOrderTypeConverter))]
[DefaultEvent("ValueChanged")]
public partial class NumericBox : UserControl
{
    #region �C�x���g

    public delegate void MyEventHandler(object sender, EventArgs e);

    public event MyEventHandler ValueChanged;

    public event MyEventHandler ReadOnlyChanged;

    public event MyEventHandler Click2;

    #endregion �C�x���g

    #region �v���p�e�B

    /// <summary>
    /// VisualStudio�f�U�C�i�[�̕ҏW�̎���True
    /// </summary>
    public new bool DesignMode
    {
        get
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;
            System.Windows.Forms.Control ctrl = this;
            while (ctrl != null)
            {
                if (ctrl.Site != null && ctrl.Site.DesignMode)
                    return true;
                ctrl = ctrl.Parent;
            }
            return false;
        }
    }

    [DefaultValue(-1)]
    /// <summary>
    /// �ۂߌ덷�������Ă���Ƃ�(�Ⴆ��7.11�̂͂��Ȃ̂� 7.110000000000001�Ƃ��A6.011�̂͂��Ȃ̂�6.010999999999999�Ƃ�)
    /// ���̌덷���������ĕ\������
    /// </summary>
    public int RoundErrorAccuracy { get; set; } = -1;


    /// <summary>
    /// UpDown�{�^����L���ɂ��邩�ǂ���
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool ShowUpDown
    {
        get => showUpDown;
        set
        {
            showUpDown = value;
            numericUpDown.Visible = false;
            numericUpDown.Visible = showUpDown;
            Refresh();
        }
    }
    private bool showUpDown = false;

    /// <summary>
    /// UpDown�{�^�����L���ȏꍇ�AIncrement���擾/�ݒ�
    /// </summary>
    [DefaultValue(1.0)]
    [Category("Value properties")]
    public double UpDown_Increment { set; get; } = 1.0;


    /// <summary>
    /// UpDown�{�^�����L���ȏꍇ�AIncrement�������Œ������邩�ǂ���
    /// </summary>
    [DefaultValue(false)]
    [Category("Value properties")]
    public bool SmartIncrement { set; get; } = false;

    /// <summary>
    /// �ő�l
    /// </summary>
    [DefaultValue(double.PositiveInfinity)]
    [Category("Value properties")]
    public double Maximum
    {
        set
        {
            if (value > minimum)
            {
                maximum = value;

                if (RestrictLimitValue && Value > maximum)
                    Value = maximum;
            }
        }
        get => maximum;
    }
    private double maximum = double.PositiveInfinity;

    /// <summary>
    /// �ŏ��l
    /// </summary>
    [DefaultValue(double.NegativeInfinity)]
    [Category("Value properties")]
    public double Minimum
    {
        set
        {
            if (value < maximum)
            {
                minimum = value;

                if (RestrictLimitValue && Value < Minimum)
                    Value = Minimum;
            }
        }
        get => minimum;
    }
    private double minimum = double.NegativeInfinity;

    /// <summary>
    /// Maximum, Minimum�͈̔͂ɓ��͒l�𐧌�����B�͈͊O�̏ꍇ�́A�����I�ɂǂ��炩�̏ꍇ�ɕύX�����
    /// </summary>
    [DefaultValue(true)]
    [Category("Value properties")]
    public bool RestrictLimitValue { set; get; } = true;


    [DefaultValue("")]
    [Localizable(true)]
    public string ToolTip
    {
        get => toolTip.GetToolTip(textBox);
        set
        {
            toolTip.SetToolTip(textBox, value);
            toolTip.SetToolTip(labelFooter, value);
            toolTip.SetToolTip(labelHeader, value);
            toolTip.SetToolTip(this, value);

        }
    }

    [Category("Value properties")]
    public double MinimalStep => DecimalPlaces >= 0 ? Math.Pow(10, -DecimalPlaces) : 1;

    #region �w�b�_�[���t�b�^�[ �̕����A�t�H���g�A�F
    /// <summary>
    /// ���l�̑O�ɕ\������e�L�X�g
    /// </summary>
    [DefaultValue("")]
    [Localizable(true)]
    [Category("Font && Color")]
    public string HeaderText { set => labelHeader.Text = value; get => labelHeader.Text; }

    [Category("Font && Color")]
    [Localizable(true)]
    [DefaultValue(typeof(Padding), "0,2,0,0")]
    public Padding HeaderPadding { set => labelHeader.Padding = value; get => labelHeader.Padding; }

    [Localizable(true)]
    [DefaultValue(typeof(Font), "Segoe UI Symbol, 9.75pt")]
    [Category("Font && Color")]
    public Font HeaderFont { set => labelHeader.Font = value; get => labelHeader.Font; }

    [DefaultValue(typeof(Color), "ControlText")]
    [Category("Font && Color")]
    public Color HeaderForeColor { set => labelHeader.ForeColor = value; get => labelHeader.ForeColor; }

    [DefaultValue(typeof(Color), "Transparent")]
    [Category("Font && Color")]
    public Color HeaderBackColor { set => labelHeader.BackColor = value; get => labelHeader.BackColor; }

    /// <summary>
    /// ���l�̌�ɕ\������e�L�X�g
    /// </summary>
    [DefaultValue("")]
    [Category("Font && Color")]
    [Localizable(true)]
    public string FooterText { set => labelFooter.Text = value; get => labelFooter.Text; }

    [Category("Font && Color")]
    [DefaultValue(typeof(Padding), "0,2,0,0")]
    [Localizable(true)]
    public Padding FooterPadding { set => labelFooter.Padding = value; get => labelFooter.Padding; }

    [Category("Font && Color")]
    [DefaultValue(typeof(Font), "Segoe UI Symbol, 9.75pt")]
    [Localizable(true)]
    public Font FooterFont { set => labelFooter.Font = value; get => labelFooter.Font; }

    [DefaultValue(typeof(Color), "ControlText")]
    [Category("Font && Color")]
    public Color FooterForeColor { set => labelFooter.ForeColor = value; get => labelFooter.ForeColor; }

    [DefaultValue(typeof(Color), "Transparent")]
    [Category("Font && Color")]
    public Color FooterBackColor { set => labelFooter.BackColor = value; get => labelFooter.BackColor; }
    #endregion


    [DefaultValue(typeof(Color), "WindowText")]
    [Category("Font && Color")]
    public Color TextBoxForeColor { set => textBox.ForeColor = value; get => textBox.ForeColor; }

    [Category("Font && Color")]
    [DefaultValue(typeof(Color), "Window")]
    public Color TextBoxBackColor { set => textBox.BackColor = value; get => textBox.BackColor; }

    [Localizable(true)]
    [Category("Font && Color")]
    [DefaultValue(typeof(Font), "Segoe UI Symbol, 9.75pt")]
    /// <summary>
    /// font
    /// </summary>
    public Font TextFont
    {
        set
        {
            textBox.Font = value;
            numericUpDown.Font = value;
            if (Multiline)
            {
                MinimumSize = new Size(0, 0);
                MaximumSize = new Size(0, 0);
            }
            else
            {
                //this.Height = textBox.Height + 2;
                MinimumSize = new Size(1, textBox.Height - 5);
                MaximumSize = new Size(1000, textBox.Height + 5);
            }
        }
        get => textBox.Font;
    }


    /// <summary>
    /// �{��\�����邩�ǂ���
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool ShowPositiveSign { set; get; } = false;



    /// <summary>
    /// �R���g���[�����ێ����Ă���l
    /// </summary>
    [DefaultValue(0.0)]
    [Category("Value properties")]
    public double Value
    {
        set
        {
            if (InvokeRequired)
                Invoke(new Action(() => Value = value), null);
            else if (this.numericalValue != value)
            {
                if (RoundErrorAccuracy > 0)
                {
                    value = value.Round(RoundErrorAccuracy);
                }

                if (RestrictLimitValue)
                {
                    if (Maximum < value)
                        value = Maximum;
                    if (Minimum > value)
                        value = Minimum;
                }
                this.numericalValue = value;
                skipTextChangeEvent = true;
                textBox.Text = GetString();
                skipTextChangeEvent = false;
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }
        get => numericalValue;
    }
    private double numericalValue = 0;

    /// <summary>
    /// �R���g���[�����ێ����Ă���l�̐����l (get�̂�)
    /// </summary>
    [Category("Value properties")]
    [DefaultValue(0)]
    public int ValueInteger { get => (int)numericalValue; }

    /// <summary>
    /// Radian�Ƃ��Ēl�����/�擾
    /// </summary>
    [DefaultValue(0.0)]
    [Category("Value properties")]
    public double RadianValue { set => Value = value * 180.0 / Math.PI; get => Value / 180.0 * Math.PI; }

    /// <summary>
    /// 3����؂�ŃJ���}��\��������
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool ThonsandsSeparator { set { thonsandsSeparator = value; textBox.Text = GetString(); } get => thonsandsSeparator; }
    private bool thonsandsSeparator = false;

    /// <summary>
    /// �����_�ȉ��̌���
    /// </summary>
    [DefaultValue(-1)]
    [Category("Appearance properties")]
    public int DecimalPlaces
    {
        set
        {
            if (value >= -1 && value < 11)
            {
                decimalPlaces = value;
                textBox.Text = GetString();
            }
        }
        get => decimalPlaces;
    }
    private int decimalPlaces = -1;

    /// <summary>
    /// �����_�ȉ��̃[���̋L�����폜���邩�ǂ���
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool TrimEndZero { get; set; } = false;

    /// <summary>
    /// �ǂݎ���p���ǂ���
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool ReadOnly { set { textBox.ReadOnly = value; numericUpDown.Enabled = !value; } get => textBox.ReadOnly; }

    /// <summary>
    /// �����s�\�������邩�ǂ���
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool Multiline
    {
        set
        {
            textBox.Multiline = value;
            if (value)
            {
                //textBox.Dock = DockStyle.Fill;
                MinimumSize = new Size(0, 0);
                MaximumSize = new Size(0, 0);
            }
            else
            {
                //textBox.Dock = DockStyle.None;
                //this.Height = textBox.Height;
                MinimumSize = new Size(1, textBox.Height - 5);
                MaximumSize = new Size(1000, textBox.Height + 5);
            }
        }
        get => textBox.Multiline;
    }

    /// <summary>
    /// �l�������ɏo����ꍇ�A�����\�������邩
    /// </summary>
    [Category("Appearance properties")]
    [DefaultValue(false)]
    public bool ShowFraction { set; get; } = false;


    /// <summary>
    /// �l���O�p�֐��ɏo����ꍇ�A�O�p�֐��ŕ\�����邩
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool ShowTrigonomeric { set; get; } = false;

    [DefaultValue("0")]
    public new string Text
    {
        set
        {
            textBox.Text = value;
            textBox_Leave(new object(), new EventArgs());
            if (RoundErrorAccuracy > 0)
            {
                var val = Value;
                Value = val;
            }

        }
        get => numericalValue.ToString();
    }

    [Category("Appearance properties")]
    [DefaultValue(true)]
    public bool WordWrap { set => textBox.WordWrap = value; get => textBox.WordWrap; }

    [DefaultValue(true)]
    public bool SkipEventDuringInput { set; get; } = true;

    #endregion �v���p�e�B

    private void textBox_ReadOnlyChanged(object sender, EventArgs e) => ReadOnlyChanged?.Invoke(sender, e);

    private void textBox_Click(object sender, EventArgs e) => Click2?.Invoke(sender, e);

    public NumericBox()
    {
        InitializeComponent();
        if (DesignMode) return;
    }

    private void textBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if ((e.KeyChar == 13 && ModifierKeys == Keys.Shift) || (e.KeyChar == 10 && ModifierKeys == Keys.Control))
            e.Handled = true;
    }


    private bool skipTextChangeEvent = false;//�e�L�X�g�`�F���W�C�x���g���̂��L�����Z������@
    private void textBox_TextChanged(object sender, EventArgs e)
    {
        if (DesignMode)
            return;

        if (skipTextChangeEvent || SkipEventDuringInput)
            return;
        try
        {
            int count = 0, selectionLine = 0;
            for (int i = 0; i < textBox.Lines.Length; i++)
            {
                count += textBox.Lines[i].Length + 2;
                if (count > textBox.SelectionStart)
                {
                    selectionLine = i;
                    break;
                }
            }
            var formula = new string[selectionLine + 1];
            Array.Copy(textBox.Lines, formula, selectionLine + 1);
            var d = NumericalFormula.GetNumetricValue(formula);
            if (!double.IsNaN(d) && d != this.numericalValue)
            {
                if (RestrictLimitValue)
                {
                    if (d > Maximum) { d = Maximum; this.numericalValue = Maximum; textBox.Text = GetString(); }
                    if (d < minimum) { d = minimum; this.numericalValue = Minimum; textBox.Text = GetString(); }
                }

                this.numericalValue = d;
                ValueChanged?.Invoke(this, e);
            }
        }
        catch { }
    }

    private void textBox_KeyDown(object sender, KeyEventArgs e)
    {
        if ((e.Control || e.Shift) && e.KeyCode == Keys.Return)
            Calculate(sender, e);
        else if (e.KeyCode == Keys.Return && SkipEventDuringInput)
        {
            SkipEventDuringInput = false;
            textBox_TextChanged(sender, e);
            SkipEventDuringInput = true;
        }
    }

    public void Calculate(object sender, EventArgs e)
    {
        //���݂̃J�[�\���ʒu�̃e�L�X�g���v�Z����
        int count = 0;
        int selectionLine = 0;
        for (int i = 0; i < textBox.Lines.Length; i++)
        {
            count += textBox.Lines[i].Length + 2;
            if (count > textBox.SelectionStart)
            {
                selectionLine = i;
                break;
            }
        }
        string[] formula = new string[selectionLine + 1];
        Array.Copy(textBox.Lines, formula, selectionLine + 1);
        var d = NumericalFormula.GetNumetricValue(formula);
        if (!double.IsNaN(d))
        {
            skipTextChangeEvent = true;
            this.numericalValue = d;
            if (textBox.Multiline)
            {
                if (textBox.Text.IndexOf("\r\n", textBox.SelectionStart, StringComparison.Ordinal) >= 0)
                    textBox.Text = textBox.Text.Remove(textBox.Text.IndexOf("\r\n", textBox.SelectionStart, StringComparison.Ordinal));

                textBox.Text += "\r\n" + GetString();
            }
            else
            {
                textBox.Text = GetString();
            }

            this.numericalValue = d;
            ValueChanged?.Invoke(this, e);

            skipTextChangeEvent = false;
            textBox.SelectionStart = textBox.Text.Length;
        }
    }

    /// <summary>
    /// ���݂�numericalValue����e�L�X�g�{�b�N�X�̕������ݒ肷��
    /// </summary>
    /// <returns></returns>
    internal string GetString()
    {
        var threshold = DecimalPlaces >= 0 ? Math.Pow(10, -decimalPlaces) : 0.0000000001;

        if (InvokeRequired)
            return (string)Invoke(new Func<string>(GetString), null);

        string text = "";
        if (double.IsNaN(numericalValue))
            return double.NaN.ToString();

        if (numericalValue != 0 && ShowFraction) //�����ŕ\������Ƃ�
        {
            int j = (int)Math.Ceiling(numericalValue - 1);
            foreach (var denom in new[] { 2, 3, 4, 5, 6, 8, 9, 10, 11, 12 })
                for (int i = 1; i < denom && text == ""; i++)
                    if ((i == 1 || denom % i != 0) && Math.Abs(numericalValue - j - i / (double)denom) < threshold)
                        text = $"{i + (denom * j)}/{denom}";
        }
        if (numericalValue > -1 && numericalValue < 1 && ShowTrigonomeric && !text.Contains('/'))//�O�p�֐��ŕ\�� (���ɕ����\������Ă���Ƃ��͏���)
        {
            //sin�֐��� -89 <= x <= 89�͈̔͂�1���� (�x�P��)
            foreach (var a in Enumerable.Range(-89, 179))
                if (a != 0 && Math.Abs(numericalValue - Math.Sin(a / 180.0 * Math.PI)) < threshold)
                {
                    text = $"sin({a})";
                    break;
                }
        }

        if (text.Length == 0)
        {
            text = numericalValue.ToString(DecimalPlaces >= 0 ? $"f{DecimalPlaces}" : "");
            if (TrimEndZero && text.Contains('.'))
                text = text.TrimEnd(['0']).TrimEnd(['.']);

            text = separateThousands(text);
        }
        if (!text.StartsWith('-') && ShowPositiveSign && text != "0")
            text = "+" + text;

        return text;
    }

    private static string separateThousands(string valueString)
    {
        var decimalPoint = '.';
        if (valueString.Contains(','))
            decimalPoint = ',';

        var integer = valueString.Split([decimalPoint]);
        for (int i = integer[0].Length - 3; i > 0; i -= 3)
        {
            if (integer[0][i - 1] != '-')
                integer[0] = integer[0].Insert(i, ",");
        }
        valueString = integer[0];
        if (integer.Length == 2)
            valueString += decimalPoint + integer[1];
        return valueString;
    }

    private void textBox_FontChanged(object sender, EventArgs e)
    {
        /*  if (Multiline == false)
          {
              this.Height = textBox.Height+3;
              this.Width = textBox.Width+1;
          }*/
        TextFont = textBox.Font;
    }

    private void numericBox_SizeChanged(object sender, EventArgs e)
    {
        //if (Multiline == false)
        //{
        //    this.Height = textBox.Height;
        //    MinimumSize = new Size(1, textBox.Height - 2);
        //    MaximumSize = new Size(1000, textBox.Height + 2);
        //}
    }

    private void numericUpDown_ValueChanged(object sender, EventArgs e)
    {
        numericUpDown.ValueChanged -= numericUpDown_ValueChanged;

        if (SmartIncrement)
        {
            double value = Math.Abs(this.Value);

            int n = 0;
            int sign = this.Value > 0 ? 1 : -1;

            if (value != 0)
                n = (int)(Math.Floor(Math.Log10(Math.Abs(value))));
            else if (DecimalPlaces >= 0)
                n = -DecimalPlaces;
            else
                n = -1;

            double step = 0;
            if ((numericUpDown.Value == 1 && sign == 1) || numericUpDown.Value == -1 && sign == -1)
            {
                if (value < Math.Pow(10, n) * 2.0)
                    step = Math.Pow(10, n - 1);
                else if (value < Math.Pow(10, n) * 4.0)
                    step = Math.Pow(10, n - 1) * 2;
                else
                    step = Math.Pow(10, n - 1) * 5;
                if (DecimalPlaces >= 0)
                    step = Math.Max(step, Math.Pow(10, -DecimalPlaces));
                value += step;
            }
            else
            {
                if (value > Math.Pow(10, n) * 4.0)
                    step = Math.Pow(10, n - 1) * 5;
                else if (value > Math.Pow(10, n) * 2.0)
                    step = Math.Pow(10, n - 1) * 2;
                else if (value > Math.Pow(10, n) * 1.0)
                    step = Math.Pow(10, n - 1) * 1;
                else
                    step = Math.Pow(10, n - 1) * 0.5;
                if (DecimalPlaces >= 0)
                    step = Math.Max(step, Math.Pow(10, -DecimalPlaces));
                value -= step;
            }
            if (value != 0)
            {
                n = (int)(Math.Floor(Math.Log10(Math.Abs(value))));
                double a = Math.Round(value / Math.Pow(10, n - 1), MidpointRounding.ToEven);
                double b = Math.Pow(10, n - 1);
                this.Value = sign * a * b;
            }
            else
                this.Value = 0;
        }
        else
        {
            if (numericUpDown.Value == 1)
                this.Value += UpDown_Increment;
            else
                this.Value -= UpDown_Increment;
        }
        numericUpDown.Value = 0;

        numericUpDown.ValueChanged += numericUpDown_ValueChanged;
    }


    private void textBox_Leave(object sender, EventArgs e)
    {
        if (SkipEventDuringInput)
        {
            SkipEventDuringInput = false;
            textBox_TextChanged(sender, e);
            SkipEventDuringInput = true;
        }
    }

    private void textBox_Enter(object sender, EventArgs e) => textBox.SelectAll();
}
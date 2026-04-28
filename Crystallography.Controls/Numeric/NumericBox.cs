using MathNet.Numerics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZLinq;

namespace Crystallography.Controls;

// 260424Cl 追加 NumericBoxのHeader/Footerラベルの配置方向
public enum NumericBoxOrientation { Horizontal, Vertical }

[TypeConverter(typeof(DefinitionOrderTypeConverter))]
[DefaultEvent("ValueChanged")]
public partial class NumericBox : UserControlBase
{
    #region イベント

    public delegate void MyEventHandler(object sender, EventArgs e);

    public event MyEventHandler ValueChanged;

    public event MyEventHandler ReadOnlyChanged;

    public event MyEventHandler Click2;

    public new event KeyEventHandler KeyDown; //260317Cl new追加 (Control.KeyDown隠蔽を明示)

    #endregion イベント

    #region プロパティ

    // 260426Cl 削除: DesignMode は UserControlBase で同等の実装を提供しているため重複定義を撤去
    //public new bool DesignMode
    //{
    //    get
    //    {
    //        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
    //            return true;
    //        System.Windows.Forms.Control ctrl = this;
    //        while (ctrl != null)
    //        {
    //            if (ctrl.Site != null && ctrl.Site.DesignMode)
    //                return true;
    //            ctrl = ctrl.Parent;
    //        }
    //        return false;
    //    }
    //}

    /// <summary>
    /// 丸め誤差が生じているとき(例えば7.11のはずなのに 7.110000000000001とか、6.011のはずなのに6.010999999999999とか)
    /// その誤差を解消して表示する
    /// </summary>
    // 260426Cl 修正: 属性 [DefaultValue] を doc-comment の前に置いていたため doc が外れていた。順序を入れ替え
    [DefaultValue(-1)]
    public int RoundErrorAccuracy { get; set; } = -1;


    /// <summary>UpDownボタンを有効にするかどうか</summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool ShowUpDown
    {
        get => showUpDown;
        set
        {
            showUpDown = value;
            if (spinButtonPanel == null) return;                                                                                                      // 260413Cl デザイン時でもPanelはあるが念のためガード
            spinButtonPanel.Visible = false;
            spinButtonPanel.Visible = showUpDown;
            applyDockOrder();                                                                                                                         // (260428Ch)
            Refresh();
        }
    }
    private bool showUpDown = false;

    /// <summary>UpDownボタンの横幅(ピクセル)。Dock=Rightのspinボタン用Panelの幅を設定する。</summary>
    [DefaultValue(17)]                                                                                                                                // 260423Cl 追加
    [Category("Appearance properties")]
    public int UpDownWidth
    {
        get => upDownWidth;
        set
        {
            if (value < 1) value = 1;
            upDownWidth = value;
            if (spinButtonPanel == null) return;
            spinButtonPanel.Width = value;
            alignSpinButton();
        }
    }
    private int upDownWidth = 17;                                                                                                                     // 260423Cl 追加 既定値はresxと同値

    /// <summary>UpDownボタンが有効な場合、Incrementを取得/設定</summary>
    [DefaultValue(1.0)]
    [Category("Value properties")]
    public double UpDown_Increment { set; get; } = 1.0;


    /// <summary>UpDownボタンが有効な場合、Incrementを自動で調整するかどうか</summary>
    [DefaultValue(false)]
    [Category("Value properties")]
    public bool SmartIncrement { set; get; } = false;

    /// <summary>最大値</summary>
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

    /// <summary>最小値</summary>
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

    /// <summary>Maximum, Minimumの範囲に入力値を制限する。範囲外の場合は、自動的にどちらかの場合に変更される</summary>
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

    #region ヘッダー＆フッター の文字、フォント、色

    /// <summary>ヘッダ/フッタラベルの配置方向 (Horizontal: Header=Left, Footer=Right / Vertical: Header=Top, Footer=Bottom)</summary>
    [DefaultValue(NumericBoxOrientation.Horizontal)]                                                                                                  // 260424Cl 追加
    [Category("Appearance properties")]
    public NumericBoxOrientation LabelOrientation
    {
        get => labelOrientation;
        set
        {
            if (labelOrientation == value) return;
            labelOrientation = value;
            applyLabelOrientation();
        }
    }
    private NumericBoxOrientation labelOrientation = NumericBoxOrientation.Horizontal;                                                                // 260424Cl 追加

    /// <summary>数値の前に表示するテキスト</summary>
    [DefaultValue("")]
    [Localizable(true)]
    [Category("Font && Color")]
    //public string HeaderText { set => labelHeader.Text = value; get => labelHeader.Text; }                                                           // 260424Cl 変更
    public string HeaderText                                                                                                                          // 260424Cl 変更 空文字の場合はVerticalモード時にラベルを非表示化
    {
        set
        {
            if (labelHeader.Text == value) return;
            labelHeader.Text = value;
            updateLabelVisibility();
        }
        get => labelHeader.Text;
    }

    [Category("Font && Color")]
    [Localizable(true)]
    //[DefaultValue(typeof(Padding), "0,2,0,0")] // 260428Cl 変更: TextAlign=Middle 化に伴い既定 Padding を 0 に
    [DefaultValue(typeof(Padding), "0,0,0,0")]
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

    /// <summary>数値の後に表示するテキスト</summary>
    [DefaultValue("")]
    [Category("Font && Color")]
    [Localizable(true)]
    //public string FooterText { set => labelFooter.Text = value; get => labelFooter.Text; }                                                           // 260424Cl 変更
    public string FooterText                                                                                                                          // 260424Cl 変更 空文字の場合はVerticalモード時にラベルを非表示化
    {
        set
        {
            if (labelFooter.Text == value) return;
            labelFooter.Text = value;
            updateLabelVisibility();
        }
        get => labelFooter.Text;
    }

    [Category("Font && Color")]
    //[DefaultValue(typeof(Padding), "0,2,0,0")] // 260428Cl 変更: TextAlign=Middle 化に伴い既定 Padding を 0 に
    [DefaultValue(typeof(Padding), "0,0,0,0")]
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

    [Category("Appearance properties")]
    [DefaultValue(HorizontalAlignment.Left)]
    public HorizontalAlignment TextBoxTextAlign { set => textBox.TextAlign = value; get => textBox.TextAlign; } // (260427Ch) 表示専用 NumericBox でも TextBox と同じ中央寄せを使えるよう公開

    // 260428Cl 変更: TextFont は Segoe UI Variable Text 固定とし、デザイナから非表示にする (フォント差で数値の見た目が崩れるのを防ぐ)。
    // サイズだけはインスタンスごとに変えたいので TextFontSize を別途公開する。
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font TextFont
    {
        set => textBox.Font = value;                                                                                                                  // 260424Cl 変更 MinimumSize/MaximumSizeの自動クランプを撤廃
        get => textBox.Font;
    }

    // 260428Cl 追加: テキスト部のフォントサイズだけ公開する。フォントファミリは Segoe UI Variable Text 固定。
    [Category("Font && Color")]
    [DefaultValue(9.75f)]
    public float TextFontSize
    {
        get => textBox.Font.Size;
        set => textBox.Font = new Font("Segoe UI Variable Text", value, textBox.Font.Style);
    }

    // 260428Cl 追加: NumericBox 全体の Font プロパティはデザイナから非表示にする。
    // ヘッダ/フッタは HeaderFont/FooterFont、テキスト部は TextFontSize で個別管理する設計のため、
    // $this.Font を経由した一括設定はデザイナから封じる。
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Font Font { get => base.Font; set => base.Font = value; }


    /// <summary>＋を表示するかどうか</summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool ShowPositiveSign { set; get; } = false;



    /// <summary>コントロールが保持している値</summary>
    [DefaultValue(0.0)]
    [Category("Value properties")]
    public double Value
    {
        set
        {
            // 260426Cl 整理: Invoke の args 引数 (null) を省略, EventArgs.Empty を使用
            if (InvokeRequired)
                Invoke(new Action(() => Value = value));
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
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        get => numericalValue;
    }
    private double numericalValue = 0;

    /// <summary>コントロールが保持している値の整数値 (getのみ)</summary>
    [Category("Value properties")]
    [DefaultValue(0)]
    public int ValueInteger { get => (int)numericalValue; }

    /// <summary>Radianとして値を入力/取得</summary>
    [DefaultValue(0.0)]
    [Category("Value properties")]
    public double RadianValue { set => Value = value * 180.0 / Math.PI; get => Value / 180.0 * Math.PI; }

    /// <summary>3桁区切りでカンマを表示させる</summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool ThonsandsSeparator { set { thonsandsSeparator = value; textBox.Text = GetString(); } get => thonsandsSeparator; }
    private bool thonsandsSeparator = false;

    /// <summary>小数点以下の桁数</summary>
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

    /// <summary>小数点以下のゼロの記号を削除するかどうか</summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool TrimEndZero { get; set; } = false;

    /// <summary>読み取り専用かどうか</summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool ReadOnly                                                                                                                              // 260413Cl null安全化
    {
        set
        {
            textBox.ReadOnly = value;
            if (spinButton != null) spinButton.Enabled = !value;
        }
        get => textBox.ReadOnly;
    }

    /// <summary>複数行表示をするかどうか</summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool Multiline                                                                                                                             // 260424Cl 変更 MinimumSize/MaximumSizeの自動クランプを撤廃
    {
        set => textBox.Multiline = value;
        get => textBox.Multiline;
    }

    /// <summary>値が分数に出来る場合、分数表示をするか</summary>
    [Category("Appearance properties")]
    [DefaultValue(false)]
    public bool ShowFraction { set; get; } = false;


    /// <summary>値が三角関数に出来る場合、三角関数で表示するか</summary>
    [DefaultValue(false)]
    [Category("Appearance properties")]
    public bool ShowTrigonomeric { set; get; } = false;

    [DefaultValue("0")]
    public new string Text
    {
        set
        {
            textBox.Text = value;
            // 260426Cl 整理: sender に空 object、引数に new EventArgs() を渡していた箇所を素直な値へ
            textBox_Leave(this, EventArgs.Empty);
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

    #endregion プロパティ

    private void textBox_ReadOnlyChanged(object sender, EventArgs e) => ReadOnlyChanged?.Invoke(sender, e);

    private void textBox_Click(object sender, EventArgs e) => Click2?.Invoke(sender, e);

    public NumericBox()
    {
        InitializeComponent();
        // 260428Cl 変更: ベースライン整合方式を変更。
        // - MiddleLeft 方式 (em-box 中心で揃える) はフォントの行間・ascent/descent 比率の差で
        //   日本語 (BIZ UDPGothic) と Latin (Segoe UI Variable Text) の混在時に視覚的なずれが残る。
        // - TopLeft + フォントメトリクスから計算した Padding.Top で textBox のベースラインに合わせる方式に変更。
        // - また、UseCompatibleTextRendering=false (GDI/TextRenderer 描画) で日本語フォントの
        //   アンチエイリアスを改善し、TextBox (常に GDI で描画) と描画パスを揃える。
        labelHeader.UseCompatibleTextRendering = false;
        labelFooter.UseCompatibleTextRendering = false;

        applyLabelOrientation();                                                                                                                      // 260428Cl 追加: orientation 別の Dock/TextAlign を初期化

        labelHeader.FontChanged += (_, _) => updateLabelBaselines();
        labelFooter.FontChanged += (_, _) => updateLabelBaselines();
        textBox.FontChanged += (_, _) => updateLabelBaselines();
        SizeChanged += (_, _) => updateLabelBaselines();
        HandleCreated += (_, _) => updateLabelBaselines();

        if (DesignMode) return;

        // 260413Cl SpinButtonはDesigner.cs/resxに載せるとVSデザイナに剥がされるため、実行時にのみ動的生成する。
        // spinButtonPanelはDesigner.csで管理されており、そのClientRect内に配置する。
        spinButton = new SpinButton
        {
            Name = "spinButton",
            Enabled = !textBox.ReadOnly,
            Margin = new Padding(0,0,0,0),
            Padding = new Padding(0,0,0,0),
        };
        spinButton.UpClick += spinButton_UpClick;
        spinButton.DownClick += spinButton_DownClick;
        spinButtonPanel.Controls.Add(spinButton);
        spinButtonPanel.Width = upDownWidth;                                                                                                          // 260423Cl 追加 UpDownWidthプロパティ値をPanelへ反映
        spinButtonPanel.Visible = showUpDown;

        // 260413Cl textBoxの高さに追従させるためsizeChangedを購読
        textBox.SizeChanged += (_, _) => alignSpinButton();
        spinButtonPanel.SizeChanged += (_, _) => alignSpinButton();
        alignSpinButton();
    }

    // (260428Ch) Visible切替でDock順が変わることがあるため、spinButtonPanelをlabelFooterの左に戻す。
    private void applyDockOrder()
    {
        // 260428Cl: 既に正しい順序なら SetChildIndex を呼ばない (各呼び出しが Layout を発火するため)。
        if (Controls.GetChildIndex(textBox) == 0
            && Controls.GetChildIndex(spinButtonPanel) == 1
            && Controls.GetChildIndex(labelHeader) == 2
            && Controls.GetChildIndex(labelFooter) == 3)
            return;
        Controls.SetChildIndex(textBox, 0);
        Controls.SetChildIndex(spinButtonPanel, 1);
        Controls.SetChildIndex(labelHeader, 2);
        Controls.SetChildIndex(labelFooter, 3);
    }

    // 260413Cl 追加 SpinButtonをtextBoxと同じY座標・同じ高さに揃える
    // (spinButtonPanel.Top=0なので、textBox.Top(NumericBox座標系)と
    //  spinButton.Top(spinButtonPanel座標系)は同値として扱える)
    private void alignSpinButton()
    {
        if (spinButton == null || spinButtonPanel == null) return;
        spinButton.Width = spinButtonPanel.ClientSize.Width;
        spinButton.Height = textBox.Height;
        spinButton.Left = 0;
        spinButton.Top = textBox.Top - spinButtonPanel.Top;
    }

    // 260424Cl 追加 LabelOrientationに応じてlabelHeader/labelFooterのDockを切り替える
    private void applyLabelOrientation()
    {
        if (labelHeader == null || labelFooter == null) return;
        if (labelOrientation == NumericBoxOrientation.Vertical)
        {
            // 260428Cl 追加: 垂直配置ではラベルは独立行になるため、ベースライン整合用 Padding.Top をリセットし、
            // ラベル底辺/上辺を textBox に寄せる配置 (header=BottomLeft, footer=TopLeft) にする。
            labelHeader.Dock = DockStyle.Top;
            labelFooter.Dock = DockStyle.Bottom;
            labelHeader.TextAlign = ContentAlignment.BottomLeft;
            labelFooter.TextAlign = ContentAlignment.TopLeft;
        }
        else
        {
            labelHeader.Dock = DockStyle.Left;
            labelFooter.Dock = DockStyle.Right;
            labelHeader.TextAlign = ContentAlignment.TopLeft;
            labelFooter.TextAlign = ContentAlignment.TopRight;
        }
        updateLabelVisibility();
        updateLabelBaselines();                                                                                                                       // 260428Cl 追加: 配置変更時にも再計算
    }

    // 260424Cl 追加 HeaderText/FooterTextが空ならラベルを非表示にして余白(Vertical時は改行、Horizontal時はPadding分)を詰める
    private void updateLabelVisibility()
    {
        if (labelHeader == null || labelFooter == null) return;
        labelHeader.Visible = !string.IsNullOrEmpty(labelHeader.Text);
        labelFooter.Visible = !string.IsNullOrEmpty(labelFooter.Text);
        applyDockOrder();                                                                                                                             // (260428Ch) Visible更新でRight dockのfooter/spin順が入れ替わるのを防ぐ
    }




    private bool skipTextChangeEvent = false;//テキストチェンジイベント自体をキャンセルする　
    private void textBox_TextChanged(object sender, EventArgs e)
    {
        if (DesignMode)
            return;

        if (skipTextChangeEvent || SkipEventDuringInput)
            return;
        // try/catch は textBox.Lines / SelectionStart 周辺で稀に発生する境界例外への保険
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

    private void textBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if ((e.KeyChar == 13 && ModifierKeys == Keys.Shift) || (e.KeyChar == 10 && ModifierKeys == Keys.Control))
            e.Handled = true;
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

        KeyDown?.Invoke(sender, e);
    }

    public void Calculate(object sender, EventArgs e)
    {
        //現在のカーソル位置のテキストを計算する
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

    /// <summary>現在のnumericalValueからテキストボックスの文字列を設定する</summary>
    /// <returns></returns>
    internal string GetString()
    {
        var threshold = DecimalPlaces >= 0 ? Math.Pow(10, -decimalPlaces) : 0.0000000001;

        // 260426Cl 整理: Invoke の args 引数 (null) を省略
        if (InvokeRequired)
            return (string)Invoke(new Func<string>(GetString));

        string text = "";
        if (double.IsNaN(numericalValue))
            return double.NaN.ToString();

        if (numericalValue != 0 && ShowFraction) //分数で表示するとき
        {
            int j = (int)Math.Ceiling(numericalValue - 1);
            foreach (var denom in new[] { 2, 3, 4, 5, 6, 8, 9, 10, 11, 12 })
                for (int i = 1; i < denom && text == ""; i++)
                    if ((i == 1 || denom % i != 0) && Math.Abs(numericalValue - j - i / (double)denom) < threshold)
                        text = $"{i + (denom * j)}/{denom}";
        }
        if (numericalValue > -1 && numericalValue < 1 && ShowTrigonomeric && !text.Contains('/'))//三角関数で表示 (既に分数表示されているときは除く)
        {
            //sin関数は -89 <= x <= 89の範囲で1刻み (度単位)
            //260317Cl 変更: Enumerable.Range → ValueEnumerable.Range
            foreach (var a in ValueEnumerable.Range(-89, 179))
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

    // 260426Cl 整理: 古いMultiline自動高さ調整コード (コメントアウト済) は撤去。Designer.cs から購読されている本体だけ残す。
    private void textBox_FontChanged(object sender, EventArgs e) => TextFont = textBox.Font;

    // 260428Cl 追加: textBox のテキストベースラインに合わせて labelHeader/labelFooter の Padding.Top を動的調整する。
    // フォントの ascent/lineSpacing 比率から各々の rendered baseline 位置を計算し、Padding.Top で揃える。
    // 垂直配置時はラベルが独立行のためベースライン整合不要 → Padding.Top をリセット。
    private void updateLabelBaselines()
    {
        if (textBox == null || labelHeader == null || labelFooter == null) return;
        if (textBox.Font == null || labelHeader.Font == null || labelFooter.Font == null) return;
        if (textBox.ClientSize.Height <= 0) return;

        if (labelOrientation == NumericBoxOrientation.Vertical)
        {
            resetPaddingTop(labelHeader);
            resetPaddingTop(labelFooter);
            return;
        }

        // textBox の text baseline Y (NumericBox 座標系)
        var tbFont = textBox.Font;
        var tbLineH = tbFont.GetHeight();
        var tbAscent = ascentPx(tbFont);
        var tbClientH = textBox.ClientSize.Height;
        var tbBorder = (textBox.Height - tbClientH) / 2f;
        var targetY = textBox.Top + tbBorder + (tbClientH - tbLineH) / 2f + tbAscent;

        setLabelPaddingForBaseline(labelHeader, targetY);
        setLabelPaddingForBaseline(labelFooter, targetY);

        static void setLabelPaddingForBaseline(Label label, float targetY)
        {
            // TopLeft 配置: text top = label.Top + Padding.Top, baseline = text top + ascent
            // → Padding.Top = targetY - label.Top - ascent
            var newTop = Math.Max(0, (int)Math.Round(targetY - label.Top - ascentPx(label.Font)));
            if (label.Padding.Top == newTop) return;
            var p = label.Padding;
            p.Top = newTop;
            label.Padding = p;
        }

        static void resetPaddingTop(Label label)
        {
            if (label.Padding.Top == 0) return;
            var p = label.Padding;
            p.Top = 0;
            label.Padding = p;
        }

        static float ascentPx(Font font)
        {
            var fam = font.FontFamily;
            return font.GetHeight() * fam.GetCellAscent(font.Style) / fam.GetLineSpacing(font.Style);
        }
    }

    // 260426Cl 整理: Multiline時の旧自動サイズ計算 (コメントアウト済) は撤去。Designer.cs から購読されているため空でも残す。
    private void numericBox_SizeChanged(object sender, EventArgs e) { }

    // 260413Cl 追加 SpinButton置き換えに伴い、Up/Downを分離したハンドラに書き換え
    private void spinButton_UpClick(object sender, EventArgs e) => applySpinStep(+1);
    private void spinButton_DownClick(object sender, EventArgs e) => applySpinStep(-1);

    // 260413Cl 追加 旧numericUpDown_ValueChangedの処理を direction 引数化して共通化
    private void applySpinStep(int direction)
    {
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
            if ((direction == 1 && sign == 1) || (direction == -1 && sign == -1))
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
            this.Value += direction * UpDown_Increment;
        }
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

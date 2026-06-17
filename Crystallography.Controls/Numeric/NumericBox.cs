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
[ToolboxItem(true)] // 260504Cl 追加: UserControlBase の [ToolboxItem(false)] が継承されてツールボックスに出ないため明示的に true を指定
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
    [Category("Value format")] // 260521Cl 追加: デザイナカテゴリ
    [Description("表示時に丸め誤差を補正する有効桁数 (例: 7.110000001 → 7.11)。-1 で無効。")] // 260521Cl 追加
    public int RoundErrorAccuracy { get; set; } = -1;


    /// <summary>UpDownボタンを有効にするかどうか</summary>
    [DefaultValue(false)]
    [Category("Spin button")]
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
    [Category("Spin button")]
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
    [Category("Spin button")]
    public double UpDown_Increment { set; get; } = 1.0;


    /// <summary>UpDownボタンが有効な場合、Incrementを自動で調整するかどうか</summary>
    [DefaultValue(false)]
    [Category("Spin button")]
    public bool SmartIncrement { set; get; } = false;

    /// <summary>最大値</summary>
    [DefaultValue(double.PositiveInfinity)]
    [Category("Value")]
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
    [Category("Value")]
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
    [Category("Value")]
    public bool RestrictLimitValue { set; get; } = true;


    [DefaultValue("")]
    [Localizable(true)]
    [Browsable(false)] // 260531Cl 追加: デザイナのプロパティグリッドから隠す。標準 ToolTip 拡張子("ToolTip on toolTip1")と二重に並んで「どちらに書くか」迷う問題を解消。Localizable は残すので既存 resx 値は従来通り適用され、子(textBox/ラベル)への配布=hover も維持される
    [EditorBrowsable(EditorBrowsableState.Never)] // 260531Cl 追加: IntelliSense からも隠す(廃止予定プロパティ。ValueFont 等と同作法)
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // 260607Cl 追加: 廃止予定プロパティを今後一切シリアライズさせない。Browsable(false)/DefaultValue("") だけでは抑止しきれず消費側に "= \"\"" が再発し得るため。Hidden は書込のみ抑止し、ApplyResources による既存 resx 値の読込/子への配布(hover)には影響しない
    [Category("Behavior")]
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

    // 260531Cl 追加: 配置先 Form が標準 ToolTip でこの NumericBox 本体にチップを設定した場合の配布先 (内部子)。
    // これにより textBox/ラベル上で hover してもチップが表示される (UserControlBase.RelayHostToolTip 参照)。
    protected override System.Windows.Forms.Control[] GetToolTipTargets() => new System.Windows.Forms.Control[] { textBox, labelHeader, labelFooter };

    // 260531Cl 追加: 独自プロパティ由来の内部 ToolTip。親がチップを設定した場合はこれを抑止して親のバルーンへ一本化する。
    protected internal override System.Windows.Forms.ToolTip InternalToolTip => toolTip;

    [Category("Value")]
    public double MinimalStep => DecimalPlaces >= 0 ? Math.Pow(10, -DecimalPlaces) : 1;

    #region ヘッダー＆フッター の文字、フォント、色

    /// <summary>ヘッダ/フッタラベルの配置方向 (Horizontal: Header=Left, Footer=Right / Vertical: Header=Top, Footer=Bottom)</summary>
    [DefaultValue(NumericBoxOrientation.Horizontal)]                                                                                                  // 260424Cl 追加
    [Category("Header && Footer")]
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
    [Category("Header && Footer")]
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

    /// <summary>ヘッダラベル(labelHeader)の固定幅(論理px)。-1 (既定) で従来どおり AutoSize=true。
    /// >=0 で labelHeader.AutoSize=false にして Width をこの値(論理px→DPIスケール)に固定する。
    /// NumericBox を縦に並べたとき、ヘッダ文字長に依らず数値欄(textBox)の開始 X 位置を揃える用途。
    /// Horizontal 配置(ヘッダ=Left)のときのみ有効。Vertical 配置(ヘッダ=Top)では無視され従来どおり AutoSize。
    /// 文字が幅に収まらない場合は ellipsis でクリップし、hover で全文を tooltip 表示する。</summary>
    [DefaultValue(-1)]                                                                                                                                // 260617Cl 追加
    [Category("Header && Footer")]
    [Description("ヘッダラベルの固定幅(論理px)。-1 で従来どおり AutoSize。>=0 で Width を固定し、縦並び時に数値欄の開始位置を揃える。")] // 260617Cl 追加
    public int HeaderWidth                                                                                                                            // 260617Cl 追加
    {
        get => headerWidth;
        set { if (headerWidth == value) return; headerWidth = value; applyWidthMode(); }                                                              // ヘッダ/数値欄幅モードへ一括反映
    }
    private int headerWidth = -1;                                                                                                                     // 260617Cl 追加 -1=AutoSize(従来), >=0=固定幅

    [Category("Header && Footer")]
    [Localizable(true)]
    //[DefaultValue(typeof(Padding), "0,2,0,0")] // 260428Cl 変更: TextAlign=Middle 化に伴い既定 Padding を 0 に
    [DefaultValue(typeof(Padding), "0,0,0,0")]
    public Padding HeaderPadding { set => labelHeader.Padding = value; get => labelHeader.Padding; }

    [Localizable(true)]
    //[DefaultValue(typeof(Font), "Segoe UI Symbol, 9.75pt")]                                                                                          // 260607Cl 修正: "Segoe UI Symbol" は記号フォントで誤り。resx の labelHeader.Font は "Segoe UI, 9.75pt" なので属性も実値に合わせる (font policy: Segoe UI / Yu Gothic UI)
    [DefaultValue(typeof(Font), "Segoe UI, 9.75pt")]
    [Category("Header && Footer")]
    public Font HeaderFont { set => labelHeader.Font = value; get => labelHeader.Font; }

    [DefaultValue(typeof(Color), "ControlText")]
    [Category("Header && Footer")]
    public Color HeaderForeColor { set => labelHeader.ForeColor = value; get => labelHeader.ForeColor; }

    //[DefaultValue(typeof(Color), "Transparent")]                                                                                                     // 260607Cl 修正: 実測で構築直後の getter (labelHeader.BackColor) は SystemColors.Control を返す (resx に BackColor 指定が無く、未設定 Label は親 Transparent を継承せず DefaultBackColor=Control に解決)。属性を実値に合わせ、消費側 146 件の冗長 "= SystemColors.Control" を抑止
    [DefaultValue(typeof(Color), "Control")]
    [Category("Header && Footer")]
    public Color HeaderBackColor { set => labelHeader.BackColor = value; get => labelHeader.BackColor; }

    /// <summary>数値の後に表示するテキスト</summary>
    [DefaultValue("")]
    [Category("Header && Footer")]
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

    [Category("Header && Footer")]
    //[DefaultValue(typeof(Padding), "0,2,0,0")] // 260428Cl 変更: TextAlign=Middle 化に伴い既定 Padding を 0 に
    [DefaultValue(typeof(Padding), "0,0,0,0")]
    [Localizable(true)]
    public Padding FooterPadding { set => labelFooter.Padding = value; get => labelFooter.Padding; }

    [Category("Header && Footer")]
    //[DefaultValue(typeof(Font), "Segoe UI Symbol, 9.75pt")]                                                                                          // 260607Cl 修正: HeaderFont と同様 "Segoe UI Symbol" は誤り。resx の labelFooter.Font = "Segoe UI, 9.75pt" に合わせる
    [DefaultValue(typeof(Font), "Segoe UI, 9.75pt")]
    [Localizable(true)]
    public Font FooterFont { set => labelFooter.Font = value; get => labelFooter.Font; }

    [DefaultValue(typeof(Color), "ControlText")]
    [Category("Header && Footer")]
    public Color FooterForeColor { set => labelFooter.ForeColor = value; get => labelFooter.ForeColor; }

    //[DefaultValue(typeof(Color), "Transparent")]                                                                                                     // 260607Cl 修正: HeaderBackColor と同様、構築直後の getter は SystemColors.Control。属性を実値に合わせ消費側 146 件の冗長行を抑止
    [DefaultValue(typeof(Color), "Control")]
    [Category("Header && Footer")]
    public Color FooterBackColor { set => labelFooter.BackColor = value; get => labelFooter.BackColor; }
    #endregion


    [DefaultValue(typeof(Color), "WindowText")]
    [Category("Value box")]
    public Color ValueForeColor { set => textBox.ForeColor = value; get => textBox.ForeColor; }

    [Category("Value box")]
    [DefaultValue(typeof(Color), "Window")]
    public Color ValueBackColor { set => textBox.BackColor = value; get => textBox.BackColor; }

    [Category("Value box")]
    [DefaultValue(HorizontalAlignment.Left)]
    public HorizontalAlignment ValueTextAlign { set => textBox.TextAlign = value; get => textBox.TextAlign; } // (260427Ch) 表示専用 NumericBox でも TextBox と同じ中央寄せを使えるよう公開

    // 260428Cl 変更: ValueFont は Segoe UI 固定とし、デザイナから非表示にする (フォント差で数値の見た目が崩れるのを防ぐ)。 260521Cl コメント修正: 実装は new Font("Segoe UI", ...) なので "Variable Text" 表記を実態に合わせた
    // サイズだけはインスタンスごとに変えたいので ValueFontSize を別途公開する。
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font ValueFont
    {
        set => textBox.Font = value;                                                                                                                  // 260424Cl 変更 MinimumSize/MaximumSizeの自動クランプを撤廃
        get => textBox.Font;
    }

    // 260428Cl 追加: テキスト部のフォントサイズだけ公開する。フォントファミリは Segoe UI 固定 (setter の new Font("Segoe UI", ...) 参照)。
    [Category("Value box")]
    [DefaultValue(9.75f)]
    public float ValueFontSize
    {
        get => textBox.Font.Size;
        set => textBox.Font = new Font(WineCompat.Resolve("Segoe UI"), value, textBox.Font.Style); //260610Cl Wine時フォント切替
    }

    // 260428Cl 追加: NumericBox 全体の Font プロパティはデザイナから非表示にする。
    // ヘッダ/フッタは HeaderFont/FooterFont、テキスト部は ValueFontSize で個別管理する設計のため、
    // $this.Font を経由した一括設定はデザイナから封じる。
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Font Font { get => base.Font; set => base.Font = value; }


    /// <summary>＋を表示するかどうか</summary>
    [DefaultValue(false)]
    [Category("Value format")]
    public bool ShowPositiveSign { set; get; } = false;



    /// <summary>コントロールが保持している値</summary>
    [DefaultValue(0.0)]
    [Category("Value")]
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
    [Category("Value")]
    [DefaultValue(0)]
    public int ValueInteger { get => (int)numericalValue; }

    /// <summary>Radianとして値を入力/取得</summary>
    [DefaultValue(0.0)]
    [Category("Value")]
    public double RadianValue { set => Value = value * 180.0 / Math.PI; get => Value / 180.0 * Math.PI; }

    /// <summary>3桁区切りでカンマを表示させる</summary>
    [DefaultValue(false)]
    [Category("Value format")]
    public bool ThousandsSeparator { set { thousandsSeparator = value; textBox.Text = GetString(); } get => thousandsSeparator; } // 260520Cl: typo fix (ThonsandsSeparator → ThousandsSeparator)
    private bool thousandsSeparator = false;

    /// <summary>小数点以下の桁数</summary>
    [DefaultValue(-1)]
    [Category("Value format")]
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
    /// 数値書式指定子 (.NET の数値書式文字列。例: "f3", "e2", "0.###", "N0")。
    /// 空文字 (既定) または不正な書式の場合は <see cref="DecimalPlaces"/> に従う従来どおりの挙動。
    /// 有効な書式が指定されている場合は <see cref="DecimalPlaces"/> を無視してこちらを優先する。
    /// </summary>
    [DefaultValue("")]
    [Category("Value format")]
    public string FormatSpecifier // 260608Cl 追加
    {
        set
        {
            formatSpecifier = value ?? "";
            formatSpecifierValid = IsValidFormatSpecifier(formatSpecifier); // 有効性を判定してキャッシュ (判定は UserControlBase に集約)
            textBox.Text = GetString();
        }
        get => formatSpecifier;
    }
    private string formatSpecifier = ""; // 260608Cl 追加
    private bool formatSpecifierValid = false; // 260608Cl 追加 FormatSpecifier が有効な書式かどうかのキャッシュ (判定は UserControlBase.IsValidFormatSpecifier)

    /// <summary>小数点以下のゼロの記号を削除するかどうか</summary>
    [DefaultValue(false)]
    [Category("Value format")]
    public bool TrimEndZero { get; set; } = false;

    /// <summary>読み取り専用かどうか</summary>
    [DefaultValue(false)]
    [Category("Behavior")]
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
    [Category("Value box")]
    public bool Multiline                                                                                                                             // 260424Cl 変更 MinimumSize/MaximumSizeの自動クランプを撤廃
    {
        set => textBox.Multiline = value;
        get => textBox.Multiline;
    }

    /// <summary>値が分数に出来る場合、分数表示をするか</summary>
    [Category("Value format")]
    [DefaultValue(false)]
    public bool ShowFraction { set; get; } = false;


    /// <summary>値が三角関数に出来る場合、三角関数で表示するか</summary>
    [DefaultValue(false)]
    [Category("Value format")]
    public bool ShowTrigonomeric { set; get; } = false;

    [DefaultValue("0")]
    [Category("Value box")]
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

    [Category("Value box")]
    [DefaultValue(true)]
    public bool WordWrap { set => textBox.WordWrap = value; get => textBox.WordWrap; }

    [DefaultValue(true)]
    [Category("Behavior")]
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

        // 260607Cl 追加: labelHeader/labelFooter の BackColor を Label 標準既定 (SystemColors.Control) に明示固定する。
        // 未設定だと親 NumericBox.BackColor=Transparent を継承して実行時は Transparent に解決される一方、
        // 設計時シリアライザは (親未接続のタイミングで) DefaultBackColor=Control を返すため設計時/実行時で
        // getter 値が食い違い、消費側 Designer.cs に "HeaderBackColor = Control" が大量直列化されていた。
        // 明示的に Control を設定して両者を一致させ、[DefaultValue(Control)] が冗長直列化を正しく抑止する。
        // (260607Cl: 旧来の見た目=灰背景を維持。透明にしたい場合は SystemColors.Control → Color.Transparent + DefaultValue を Transparent に)
        labelHeader.BackColor = SystemColors.Control;
        labelFooter.BackColor = SystemColors.Control;

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

        // 260617Cl 追加: 数値欄幅モード (多言語化対応) の配線。親が確定したら(Flow/Table 判定のため)再適用、全幅変化で M2 ヘッダ幅を追従。
        ParentChanged += (_, _) => applyWidthMode();
        SizeChanged += (_, _) => refreshWidthMode();
        applyWidthMode();
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
        applyWidthMode();                                                                                                                             // 260617Cl 追加: 配置変更時にヘッダ固定幅(HeaderWidth)/数値欄幅モードを再評価 (Vertical では固定解除→AutoSize へ)
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
        refreshWidthMode();                                                                                                                           // 260617Cl: ヘッダ/フッタ可視変化を数値欄幅モードへ反映
    }

    #region 260617Cl 追加: 多言語化対応の「数値欄幅モード」
    // 問題: 既定では labelHeader(Dock=Left,AutoSize) | textBox(Dock=Fill) | spin/footer(Right) で全幅固定のため、
    //       HeaderText が翻訳で伸びると数値欄(Fill)が縮んで数値が読めなくなる (多言語化の最大の切れリスク)。
    // 対処: 数値欄に最低固定幅を持たせ、ヘッダの伸長を「数値欄の縮小」でなく
    //       M3=全幅の伸長(Flow/TableLayoutPanel でリフロー) / M2=ヘッダのクリップ(絶対配置で格子維持・tooltip 救済) に変える。
    // レイアウト構造(Dock)は壊さず、ヘッダの AutoSize/幅と本体の AutoSize/GetPreferredSize だけで実現する (低リスク)。
    // 詳細は .project-guidance/ReciPro_多言語化方針.md (Phase 1)。既定 OFF で従来挙動と完全一致。

    private int valueBoxWidth = -1;
    private bool autoSizeWidth = false;

    /// <summary>静的既定: アプリ起動時に >=0 (論理px) を設定すると、ValueBoxWidth 未指定の全 NumericBox に最低数値欄幅を一括適用する。
    /// 親が Flow/TableLayoutPanel なら全幅を伸ばし(リフロー)、絶対配置なら数値欄を死守してヘッダをクリップ(tooltip で全文救済)。
    /// 既定 -1 = 無効 (従来挙動)。アプリ側で 1 行設定するだけで全数値欄に最低可読幅を保証する。</summary>
    public static int DefaultValueBoxWidth { get; set; } = -1;

    /// <summary>数値欄(textBox)の固定幅(論理px)。-1 (既定) で従来どおり残り幅を Fill。
    /// >=0 でヘッダ伸長時も数値欄をこの幅に死守する。未指定(-1)でも <see cref="DefaultValueBoxWidth"/> が効く。</summary>
    [DefaultValue(-1)]
    [Category("Value box")]
    [Description("数値欄の固定幅(論理px)。-1 で従来どおり(残り幅を Fill)。>=0 でヘッダ伸長時も数値欄を死守する。")]
    public int ValueBoxWidth
    {
        get => valueBoxWidth;
        set { if (valueBoxWidth == value) return; valueBoxWidth = value; applyWidthMode(); }
    }

    /// <summary>true で数値欄を固定し全幅を AutoSize で伸ばす(Flow/TableLayoutPanel 内向け=リフローで完全可読)。
    /// false(既定)で全幅固定のままヘッダをクリップ。ValueBoxWidth 未指定+DefaultValueBoxWidth 有効時は、親が Flow/Table なら自動で true 相当に振る舞う。</summary>
    [DefaultValue(false)]
    [Category("Value box")]
    [Description("数値欄を固定し全幅を AutoSize で伸ばす(Flow/TableLayoutPanel 内向け)。false なら全幅固定でヘッダをクリップ。")]
    public bool AutoSizeWidth
    {
        get => autoSizeWidth;
        set { if (autoSizeWidth == value) return; autoSizeWidth = value; applyWidthMode(); }
    }

    // 有効な数値欄幅 (インスタンス指定優先、無ければ静的既定)。<0 ならモード無効=従来挙動。
    private int effectiveValueBoxWidth => valueBoxWidth >= 0 ? valueBoxWidth : DefaultValueBoxWidth;
    // 設計時は適用しない (Designer の再シリアライズ汚染を避け、実行時のみ反映)。
    private bool widthModeActive => effectiveValueBoxWidth >= 0 && !DesignMode;
    // M3(全幅 AutoSize): 明示 AutoSizeWidth、または静的既定経由で親が Flow/Table のとき。
    private bool useAutoSizeWidth => autoSizeWidth || (valueBoxWidth < 0 && Parent is FlowLayoutPanel or TableLayoutPanel);

    // 260617Cl 追加: HeaderWidth(>=0) によるヘッダ固定幅モードが有効か。Horizontal 配置のときのみ
    // (Vertical では labelHeader.Dock=Top で Width が無意味かつ AutoSize=false が高さを壊すため不可)。
    // DesignMode は問わない (デザイナ上でも数値欄の整列を確認できるようにする。labelHeader は内部子のため
    //  消費側フォームの Designer.cs には直列化されず、再シリアライズ汚染は起きない)。
    private bool headerWidthFixed => headerWidth >= 0 && labelOrientation == NumericBoxOrientation.Horizontal;

    private void applyWidthMode()
    {
        if (labelHeader == null || textBox == null) return;

        // 260617Cl 追加: Vertical 配置ではヘッダは独立行(Dock=Top)で「ヘッダ vs 数値欄」の横幅競合が無い。
        // よって幅モード(HeaderWidth/ValueBoxWidth=M2/M3)は一切適用せず、ヘッダは常に AutoSize=true に保つ。
        // (Dock=Top で AutoSize=false にするとヘッダ高さがテキスト/フォントに追従せず壊れるため必須のガード。
        //  headerWidthFixed は Horizontal を要求するが、併存する M2 経路には orientation ガードが無かった—ここで一元的に塞ぐ)。
        if (labelOrientation != NumericBoxOrientation.Horizontal)
        {
            if (!labelHeader.AutoSize) labelHeader.AutoSize = true;
            labelHeader.AutoEllipsis = false;
            if (AutoSize) AutoSize = false;
            return;
        }

        // --- 以下 Horizontal 配置のみ ---
        // 260617Cl 追加: HeaderWidth(>=0, Horizontal) によるヘッダ固定幅モード。
        // 有効ならヘッダの AutoSize/Width をここで確定し、以降の widthMode(ValueBoxWidth)系の
        // ヘッダ操作(M1 の AutoSize=true / M2 の動的クリップ)は抑止して固定幅を死守する。
        // (HeaderWidth と ValueBoxWidth が両方有効な場合: M3 なら GetPreferredSize が header+box+… を返し両立。
        //  M2 では全幅固定のためヘッダ固定が優先され、数値欄の最低幅は保証されない=固定幅指定側の責務)。
        bool fixedHeader = headerWidthFixed;
        if (fixedHeader)
        {
            if (labelHeader.AutoSize) labelHeader.AutoSize = false;
            labelHeader.AutoEllipsis = true;                                                                                                          // はみ出しは ellipsis(+hover tooltip)でクリップ
            int hw = LogicalToDeviceUnits(headerWidth);                                                                                               // 高DPI対応 (論理px→デバイスpx)
            if (labelHeader.Width != hw) labelHeader.Width = hw;
        }

        if (!widthModeActive)
        {
            // M1 (従来): ヘッダ AutoSize、全幅固定、ellipsis 無し。
            if (!fixedHeader)
            {
                if (!labelHeader.AutoSize) labelHeader.AutoSize = true;
                labelHeader.AutoEllipsis = false;
            }
            if (AutoSize) AutoSize = false;
            return;
        }
        if (useAutoSizeWidth)
        {
            // M3: ヘッダ AutoSize のまま(固定時を除く)、本体 AutoSize=true → GetPreferredSize が header+box+spin+footer を返す。
            if (!fixedHeader)
            {
                if (!labelHeader.AutoSize) labelHeader.AutoSize = true;
                labelHeader.AutoEllipsis = false;
            }
            if (!AutoSize) AutoSize = true;
            PerformLayout();
        }
        else
        {
            // M2: 全幅固定。ヘッダを固定幅+ellipsis にし、Fill の数値欄を effectiveValueBoxWidth に死守する。
            if (AutoSize) AutoSize = false;
            if (!fixedHeader)
            {
                labelHeader.AutoSize = false;
                labelHeader.AutoEllipsis = true;
                layoutM2HeaderWidth();
            }
        }
    }

    // M2: ヘッダ(Dock=Left,AutoSize=false)を「必要幅まで縮め、足りなければクリップ」して、Fill の数値欄を最低 box に死守する。
    // ヘッダ幅 = min(ヘッダ必要幅, 全幅 − box − spin − footer)。
    //  - ヘッダが短いフォーム: ヘッダ=必要幅 → 数値欄 = 残り(自然な広い幅) を温存 (=従来と同じ。広い数値欄を不要に削らない)。
    //  - ヘッダが長い/翻訳で伸びた場合: ヘッダ=上限でクリップ(ellipsis+tooltip) → 数値欄は最低 box を死守。
    // ＝ box は「固定」でなく「下限」。これにより既存の見た目を壊さず、翻訳で縮む時だけ保護が効く。
    private void layoutM2HeaderWidth()
    {
        if (!widthModeActive || useAutoSizeWidth || labelHeader == null) return;
        if (headerWidthFixed) return;                                                                                                                 // 260617Cl: ヘッダ固定幅モードでは applyWidthMode が幅を確定済み (動的クリップしない)
        int box = LogicalToDeviceUnits(effectiveValueBoxWidth); // 高DPI対応
        int footerW = labelFooter is { Visible: true } ? labelFooter.Width : 0;
        int spinW = spinButtonPanel is { Visible: true } ? spinButtonPanel.Width : 0;
        int maxHeader = Math.Max(0, ClientSize.Width - box - footerW - spinW);
        int desired = labelHeader.Visible ? Math.Min(labelHeader.PreferredWidth, maxHeader) : 0;
        if (labelHeader.Width != desired) labelHeader.Width = desired;
    }

    // 全幅変化(M2)/ヘッダ・フッタ可視変化 を幅モードへ反映する軽量フック。
    private void refreshWidthMode()
    {
        if (!widthModeActive || labelOrientation != NumericBoxOrientation.Horizontal) return;                                                        // 260617Cl: Vertical は幅モード非対象
        if (useAutoSizeWidth) PerformLayout(); else layoutM2HeaderWidth();
    }

    // M3: 親(Flow/Table)が問い合わせる優先サイズ = ヘッダ実必要幅 + 数値欄 + spin + footer。
    public override Size GetPreferredSize(Size proposedSize)
    {
        if (widthModeActive && useAutoSizeWidth && labelHeader != null && labelOrientation == NumericBoxOrientation.Horizontal)                       // 260617Cl: Vertical は横方向集計しない(ラベルは縦積み)
        {
            int box = LogicalToDeviceUnits(effectiveValueBoxWidth);
            int hw = labelHeader.Visible ? (headerWidthFixed ? LogicalToDeviceUnits(headerWidth) : labelHeader.PreferredWidth) : 0; // 260617Cl: 固定幅時は HeaderWidth を加算
            int fw = labelFooter is { Visible: true } ? labelFooter.PreferredWidth : 0;
            int sw = spinButtonPanel is { Visible: true } ? spinButtonPanel.Width : 0;
            return new Size(hw + box + fw + sw, Height);
        }
        return base.GetPreferredSize(proposedSize);
    }

    // 260617Cl 追加: Handle 作成時 / Per-Monitor DPI 変化時に幅モードを再適用する。
    // HeaderWidth(固定ヘッダ幅)・ValueBoxWidth(数値欄幅) は LogicalToDeviceUnits でデバイスpx化するが、
    // プロパティ設定/ParentChanged はハンドル生成前(DeviceDpi=96)に走るため、その時点では論理値=物理値のまま
    // stamp されてしまう。実際の DPI が確定する OnHandleCreated と、モニタ移動時の OnDpiChangedAfterParent で
    // 再計算しないと高DPIでヘッダ幅がずれ、縦並びの数値欄整列が崩れる (姉妹コントロール IndexControl.cs:276-289 と同じ定石)。
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);                                                                                                                      // UserControlBase の ToolTip relay を維持
        applyWidthMode();
    }

    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        applyWidthMode();
    }
    #endregion




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
            // 260608Cl 変更: FormatSpecifier が有効ならそちらを優先し DecimalPlaces を無視する。
            //               無効/空文字の場合は従来どおり DecimalPlaces ベースの書式を使う。
            //text = numericalValue.ToString(DecimalPlaces >= 0 ? $"f{DecimalPlaces}" : "");
            var format = formatSpecifierValid ? formatSpecifier : (DecimalPlaces >= 0 ? $"f{DecimalPlaces}" : "");
            text = numericalValue.ToString(format);
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
    private void textBox_FontChanged(object sender, EventArgs e) => ValueFont = textBox.Font;

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

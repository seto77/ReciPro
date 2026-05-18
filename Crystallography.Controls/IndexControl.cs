using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class IndexControl : UserControl
    {
        #region Enum

        public enum ModeEnum { Plane, Axis }
        public enum BracketEnum { Round, Angle }

        #endregion

        #region ラベル表示 (Mode / SubScript / Bracket)

        private string subScript = "";
        public string SubScript
        {
            get => subScript;
            set { subScript = value; setLabel(); }
        }

        private ModeEnum mode = ModeEnum.Plane;
        public ModeEnum Mode
        {
            get => mode;
            set { mode = value; setLabel(); }
        }

        private BracketEnum bracket = BracketEnum.Round;
        public BracketEnum Bracket
        {
            get => bracket;
            set { bracket = value; setLabel(); }
        }

        // Mode / SubScript / Bracket の値から 4 つの LaTeX ラベルと括弧文字を組み立てる
        private void setLabel()
        {
            // 260518Cl 整理: subScript の三項を 1 回だけ評価する
            var sub = string.IsNullOrEmpty(subScript) ? "" : "_" + subScript;
            if (mode == ModeEnum.Plane)
            {
                labelLaTexX.Text = "h" + sub;
                labelLaTexY.Text = "k" + sub;
                labelLaTexZ.Text = "l" + sub;
                labelLaTexW.Text = "i" + sub;
                labelLaTexStart.Text = bracket == BracketEnum.Round ? "(" : "\\{";
                labelLaTexEnd.Text = bracket == BracketEnum.Round ? ")" : "\\}";
            }
            else
            {
                labelLaTexX.Text = "u" + sub;
                labelLaTexY.Text = "v" + sub;
                labelLaTexZ.Text = "w" + sub;
                labelLaTexStart.Text = bracket == BracketEnum.Round ? "[" : "\\langle";
                labelLaTexEnd.Text = bracket == BracketEnum.Round ? "]" : "\\rangle";
            }
        }

        public bool LabelVisible { get=>labelLaTexX.Visible; set { labelLaTexX.Visible = labelLaTexY.Visible = labelLaTexZ.Visible = labelLaTexW.Visible = value; } }

        #endregion

        #region 指数値 (Values)

        // 260517Cl 追加: Designer の Property Grid からも "h, k, l" 形式で編集できるよう TypeConverter を適用
        [Category("Behavior")]
        [Description("(h, k, l) 指数。Property Grid からはカンマ区切りで入力する (例: \"2, 1, 0\")。")]
        [TypeConverter(typeof(HKLValuesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public (int X, int Y, int Z) Values
        {
            get => (numericBoxH.ValueInteger, numericBoxK.ValueInteger, numericBoxL.ValueInteger);
            // 260517Cl 追加: タプル代入で H/K/L を一括設定 (各 set が ValueChanged を発火し、i も自動更新される)
            set => (numericBoxH.Value, numericBoxK.Value, numericBoxL.Value) = (value.X, value.Y, value.Z);
        }

        // 260517Cl 追加: (h, k, l) == (0, 0, 0) のとき true。指数 (0,0,0) は無効値として弾く用途で頻出するため公開する。
        [Browsable(false)]
        public bool IsZero => numericBoxH.ValueInteger == 0 && numericBoxK.ValueInteger == 0 && numericBoxL.ValueInteger == 0;

        #endregion

        #region レイアウト切替 (MillerBravais / PlusMinus)

        // 260517Cl 追加: i 軸 (Miller-Bravais の 4 番目の指数) を表示するか。
        // true のとき labelLaTexW (i_n ヘッダ) と numericBoxI を表示し、i = -(h+k) を自動で反映する。
        // false のときは Column 5 を Absolute 0 に潰してレイアウトを詰める。
        private bool millerBravais = false;
        [Category("Behavior"), DefaultValue(false)]
        [Description("true で i 軸 (Miller-Bravais 指数) を表示し、i = -(h+k) を自動計算する。")]
        public bool MillerBravais
        {
            get => millerBravais;
            set
            {
                millerBravais = value;
                tableLayoutPanel1.ColumnStyles[5] =
                    value ? new ColumnStyle(SizeType.AutoSize) : new ColumnStyle(SizeType.Absolute, 0F);
                if (value)
                    UpdateI();
            }
        }

        // 260517Cl 追加: H/K/L 各 numericBox の手前に ± ラベル (labelLaTexPM1/PM2/PM4) を表示するか。
        // false のとき該当列 (1, 3, 6) を Absolute 0 に潰す。値範囲も連動して切替 (true: [0, Max] / false: [-Max, Max])。
        private bool plusMinus = false;
        [Category("Behavior"), DefaultValue(false)]
        [Description("true で各指数の手前に ± ラベルを表示する (numericBox は [0, Maximum])。false のときは [-Maximum, Maximum]。")]
        public bool PlusMinus
        {
            get => plusMinus;
            set
            {
                plusMinus = value;
                // 260517Cl ColumnStyle は同一インスタンスを複数列に共有できないため、列ごとに new する
                // 260518Cl 整理: 3 列ぶんの三項式をローカル関数に集約
                ColumnStyle style() => value ? new ColumnStyle(SizeType.AutoSize) : new ColumnStyle(SizeType.Absolute, 0F);
                tableLayoutPanel1.ColumnStyles[1] = style();
                tableLayoutPanel1.ColumnStyles[3] = style();
                tableLayoutPanel1.ColumnStyles[6] = style();
                ApplyRange();
            }
        }

        #endregion

        #region 値変更通知 (ValueChanged)

        // 260517Cl 追加: H/K/L のいずれかが変化したときに発火する統合イベント。
        // 親フォーム側で numericBox を個別購読しなくて済むようにする (HKLControl と同じパターン)。
        [Category("Action")]
        [Description("H/K/L のいずれかの値が変化したときに発火する。")]
        public event EventHandler ValueChanged;

        // numericBoxH/K/L すべての ValueChanged をここで受け、i 再計算と統合イベント発火を行う。
        private void OnAnyValueChanged(object sender, EventArgs e)
        {
            UpdateI();
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region 値範囲 (Maximum)

        // 260517Cl 追加: H/K/L の絶対値の上限。Minimum は PlusMinus と連動して決まる:
        //   PlusMinus=true  → [0, Maximum]
        //   PlusMinus=false → [-Maximum, Maximum]
        // numericBoxI は表示専用 (Value は自動計算で代入されるだけ) なので ±∞ でクランプを無効化する。
        private int maximum = 20;
        [Category("Behavior"), DefaultValue(20)]
        [Description("H/K/L の最大値。Minimum は PlusMinus に応じて 0 または -Maximum になる。")]
        public int Maximum
        {
            get => maximum;
            set { maximum = value; ApplyRange(); }
        }

        // PlusMinus / Maximum の変化に応じて numericBoxH/K/L の Minimum/Maximum を一括反映
        private void ApplyRange()
        {
            int min = plusMinus ? 0 : -maximum;
            numericBoxH.Maximum = numericBoxK.Maximum = numericBoxL.Maximum = maximum;
            numericBoxH.Minimum = numericBoxK.Minimum = numericBoxL.Minimum = min;
            // numericBoxI は自動計算結果を入れるだけなのでクランプ不要。±∞ にしておく
            numericBoxI.Maximum = double.PositiveInfinity;
            numericBoxI.Minimum = double.NegativeInfinity;
        }

        #endregion

        #region 入力可否 (ReadOnly)

        // 260517Cl 追加: H/K/L を一括で ReadOnly に切替。i は元から ReadOnly なので対象外。
        [Category("Behavior"), DefaultValue(false)]
        [Description("true で H/K/L 全てを ReadOnly にする (表示専用)。")]
        public bool ReadOnly
        {
            get => numericBoxH.ReadOnly;
            set
            {
                numericBoxH.ReadOnly = value;
                numericBoxK.ReadOnly = value;
                numericBoxL.ReadOnly = value;
            }
        }

        #endregion

        #region 寸法 (UpDownWidth / BoxWidth)

        public int UpDownWidth
        {
            get => numericBoxH.UpDownWidth;
            set => numericBoxH.UpDownWidth = numericBoxK.UpDownWidth = numericBoxL.UpDownWidth = value;
        }

        public int BoxWidth
        {
            get => numericBoxH.Width; // 260517Cl fix: 旧 getter が UpDownWidth を返していた typo を修正
            set
            {
                numericBoxH.Width = numericBoxK.Width = numericBoxL.Width = value;
                numericBoxI.Width = value - 16;
            }
        }

        #endregion

        public IndexControl()
        {
            InitializeComponent();
            // 260517Cl 追加: H/K/L 変化時に i 再計算 + 統合 ValueChanged 発火
            numericBoxH.ValueChanged += OnAnyValueChanged;
            numericBoxK.ValueChanged += OnAnyValueChanged;
            numericBoxL.ValueChanged += OnAnyValueChanged;
            // 260517Cl 追加: 既定値で可視/列幅を反映 (Designer は両方表示状態で生成されるため)
            MillerBravais = millerBravais;
            PlusMinus = plusMinus;
        }

        // 260517Cl 追加: i = -(h+k) を numericBoxI に反映 (MillerBravais=true のときのみ)
        private void UpdateI()
        {
            if (millerBravais)
                numericBoxI.Value = -(numericBoxH.ValueInteger + numericBoxK.ValueInteger);
        }
    }

    // 260517Cl 追加: Values ((int, int, int)) を Designer の Property Grid から
    // カンマ/空白区切り文字列で編集できるようにする。InstanceDescriptor 経由で Designer.cs に
    // `new ValueTuple<int, int, int>(h, k, l)` として serialize される。
    internal sealed class HKLValuesConverter : TypeConverter
    {
        private static readonly char[] _separators = { ',', ' ', ';', '\t' };

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => destinationType == typeof(string)
            || destinationType == typeof(InstanceDescriptor)
            || base.CanConvertTo(context, destinationType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                var parts = s.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 3
                    && int.TryParse(parts[0], NumberStyles.Integer, culture, out int x)
                    && int.TryParse(parts[1], NumberStyles.Integer, culture, out int y)
                    && int.TryParse(parts[2], NumberStyles.Integer, culture, out int z))
                    return (x, y, z);
                throw new FormatException($"'{s}' を (int, int, int) に変換できません。\"h, k, l\" 形式で入力してください。");
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is ValueTuple<int, int, int> t)
            {
                if (destinationType == typeof(string))
                    return $"{t.Item1}, {t.Item2}, {t.Item3}";
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var ctor = typeof(ValueTuple<int, int, int>).GetConstructor(new[] { typeof(int), typeof(int), typeof(int) });
                    return new InstanceDescriptor(ctor, new object[] { t.Item1, t.Item2, t.Item3 });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

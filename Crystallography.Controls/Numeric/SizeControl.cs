using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    // 260521Cl: ReciPro 各フォームに散在する「画像サイズ W×H」UI を一本化するためのコントロール。
    // IndexControl と同じ作法 (NumericBox 複数 + 統合 ValueChanged + Designer 公開プロパティ) で実装する。
    public partial class SizeControl : UserControl
    {
        public SizeControl()
        {
            InitializeComponent();
        }

        #region 見出し / 単位ラベル

        // 260521Cl: private だったものを public 化し、Designer/Property Grid から編集できるようにする。

        // 260521Cl: ラベル (見出し / × / 単位 / チェックボックス) のフォント。Font 型に string の DefaultValue は付けられないため属性は無し。
        [Category("Appearance"), Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("見出し・単位・チェックボックスのラベルフォント。")]
        public Font LabelFont { get => labelHeader.Font; set => labelHeader.Font = label2.Font = label1.Font = checkBoxKeepAspect.Font = value; }

        // 260521Cl: 数値入力部のフォントサイズ。NumericBox は ValueFont を Designer 封印 (フォント差で数値表示が崩れるのを防ぐ設計) しており、
        // 公開されている ValueFontSize 経由でサイズのみ調整する。フォントファミリは NumericBox 側で固定。
        [Category("Appearance"), DefaultValue(9.75f)]
        [Description("数値入力部 (Width/Height) のフォントサイズ (pt)。")]
        public float ValueFontSize { get => numericBoxWidth.ValueFontSize; set => numericBoxWidth.ValueFontSize = numericBoxHeight.ValueFontSize = value; }



        [Category("Appearance"), Localizable(true), DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Width/Height の手前に表示する見出しテキスト。")]
        public string HeaderText { get => labelHeader.Text; set => labelHeader.Text = value; }

        // 260521Cl 追加: 単位ラベル (label2)。改善提案 §1.6/§2.1 の規約に従い既定は "px" (旧 "pix²" は廃止)。× 区切りは label1。
        [Category("Appearance"), Localizable(true), DefaultValue("px")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Height の後ろに表示する単位テキスト (既定 \"px\")。")]
        public string UnitText { get => label2.Text; set => label2.Text = value; }

        // 260521Cl 追加: ToolTip パススルー。子コントロール全体に同じツールチップを表示する (NumericBox.ToolTip と同じ作法)。
        [Category("Appearance"), Localizable(true), DefaultValue("")]
        [Description("コントロール全体に表示するツールチップ。")]
        public string ToolTip
        {
            get => toolTip.GetToolTip(numericBoxWidth);
            set
            {
                toolTip.SetToolTip(this, value);
                toolTip.SetToolTip(numericBoxWidth, value);
                toolTip.SetToolTip(numericBoxHeight, value);
                toolTip.SetToolTip(labelHeader, value);
                toolTip.SetToolTip(label1, value);
                toolTip.SetToolTip(label2, value);
                toolTip.SetToolTip(checkBoxKeepAspect, value);
            }
        }

        #endregion

        #region 値 (Value / Width / Height)

        // 260521Cl 追加: Value 一括設定中・アスペクト同期中の再入を抑止し、ValueChanged を 1 回に束ねるフラグ
        private bool internalUpdate = false;

        // 260521Cl: private → public 化。Size には既定の TypeConverter があり Property Grid で "幅, 高さ" 編集が可能。
        [Category("Behavior")]
        [Description("画像サイズ (Width × Height)。Property Grid からは \"幅, 高さ\" 形式で編集する。")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Size Value
        {
            get => new(numericBoxWidth.ValueInteger, numericBoxHeight.ValueInteger);
            set
            {
                // 260521Cl: W/H とも変化なしなら no-op (NumericBox.Value と同じく変化時のみ ValueChanged を発火)。
                // リサイズ経路で現在値と同じ Size を書き戻す箇所が多いため、無駄なイベント発火と再入を防ぐ。
                if (value.Width == numericBoxWidth.ValueInteger && value.Height == numericBoxHeight.ValueInteger)
                    return;
                // W/H を個別代入すると ValueChanged が 2 回発火するため internalUpdate で束ね、1 回だけ発火する。
                internalUpdate = true;
                numericBoxWidth.Value = value.Width;
                numericBoxHeight.Value = value.Height;
                if (value.Height != 0)
                    aspectRatio = (double)value.Width / value.Height;
                internalUpdate = false;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // 260521Cl 追加: Macro 等の「片側だけ設定」用途向け。Property Grid には出さない (Value と二重管理になるため)。
        // 260521Cl: Control.Width/Height (コントロール自身のピクセル寸法) を隠蔽しないよう ImageWidth/ImageHeight と命名する。
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ImageWidth { get => numericBoxWidth.ValueInteger; set => numericBoxWidth.Value = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ImageHeight { get => numericBoxHeight.ValueInteger; set => numericBoxHeight.Value = value; }

        #endregion

        #region 値の範囲 / 刻み

        // 260521Cl 追加: 旧実装は 1〜9999 ハードコードだったが、用途ごとに範囲が異なる (例: FormResolution は最大 4096) ため公開する。
        private int minimum = 1;
        [Category("Behavior"), DefaultValue(1)]
        [Description("Width/Height の最小値。")]
        public int Minimum
        {
            get => minimum;
            set { minimum = value; numericBoxWidth.Minimum = numericBoxHeight.Minimum = value; }
        }

        private int maximum = 9999;
        [Category("Behavior"), DefaultValue(9999)]
        [Description("Width/Height の最大値。")]
        public int Maximum
        {
            get => maximum;
            set { maximum = value; numericBoxWidth.Maximum = numericBoxHeight.Maximum = value; }
        }

        [Category("Behavior"), DefaultValue(true)]
        [Description("true で UpDown の刻みを桁に応じて自動調整する。false のとき Increment 固定。")]
        public bool SmartIncrement
        {
            get => numericBoxWidth.SmartIncrement;
            set { numericBoxWidth.SmartIncrement = numericBoxHeight.SmartIncrement = value; }
        }

        [Category("Behavior"), DefaultValue(1.0)]
        [Description("SmartIncrement=false のときの UpDown 刻み幅 (例: 256 で 2 の冪刻みに近づける)。")]
        public double Increment
        {
            get => numericBoxWidth.UpDown_Increment;
            set { numericBoxWidth.UpDown_Increment = numericBoxHeight.UpDown_Increment = value; }
        }

        #endregion

        #region 読み取り専用

        [Category("Behavior"), DefaultValue(false)]
        [Description("true で Width/Height を読み取り専用にする (表示専用)。")]
        public bool ReadOnly
        {
            get => numericBoxWidth.ReadOnly;
            set { numericBoxWidth.ReadOnly = numericBoxHeight.ReadOnly = value; }
        }

        #endregion

        #region アスペクト比固定

        // 直近の W:H 比。KeepAspectRatio=ON の同期計算と、OFF 時の基準更新に使う。
        private double aspectRatio = 1.0;
        private bool keepAspectRatio = false;

        [Category("Behavior"), DefaultValue(false)]
        [Description("true で W:H のアスペクト比を固定する。内蔵チェックボックスと連動する。")]
        public bool KeepAspectRatio
        {
            get => keepAspectRatio;
            set
            {
                keepAspectRatio = value;
                if (checkBoxKeepAspect.Checked != value)
                    checkBoxKeepAspect.Checked = value;
                // 固定を ON にした瞬間の比を基準として捕捉する。
                if (value && numericBoxHeight.Value != 0)
                    aspectRatio = (double)numericBoxWidth.Value / numericBoxHeight.Value;
            }
        }

        [Category("Appearance"), DefaultValue(false)]
        [Description("true でアスペクト比固定チェックボックスを表示する (既定は非表示)。")]
        public bool ShowKeepAspectCheckBox
        {
            // AutoSize 列に置いているため、非表示にすると列幅が 0 に詰まる。
            get => checkBoxKeepAspect.Visible;
            set => checkBoxKeepAspect.Visible = value;
        }

        private void checkBoxKeepAspect_CheckedChanged(object sender, EventArgs e)
            => KeepAspectRatio = checkBoxKeepAspect.Checked;

        #endregion

        #region 2 の冪スナップ / 総ピクセル数ガード

        [Category("Behavior"), DefaultValue(false)]
        [Description("true で確定時に Width/Height を最も近い 2 の冪 (256, 512, …) に丸める。FFT 系シミュレーション向け。")]
        public bool SnapToPowerOfTwo { get; set; } = false;

        [Category("Behavior"), DefaultValue(0L)]
        [Description("Width×Height の上限ピクセル数 (0 で無効)。超過時は変更していない側を縮めて収める。")]
        public long MaxTotalPixels { get; set; } = 0;

        // 最も近い 2 の冪 (1 以上) を返す。下位/上位のうち近い方、等距離なら下位。
        private static int nearestPowerOfTwo(int v)
        {
            if (v < 1) return 1;
            int lower = 1 << (int)Math.Floor(Math.Log2(v));
            int upper = lower << 1;
            return (v - lower) <= (upper - v) ? lower : upper;
        }

        #endregion

        #region UpDown ボタン幅 (DPI 対応)

        // 260521Cl: IndexControl と同様、setter は 96dpi 論理値として保持し、Handle 作成/DPI 変化時に物理ピクセル化する。
        private int logicalUpDownWidth = 17;
        private bool upDownWidthSet = false;
        [Category("Appearance"), DefaultValue(17)]
        [Description("UpDown ボタン幅 (96dpi 論理値)。実行時に DeviceDpi に応じてスケールされる。")]
        public int UpDownWidth
        {
            get => logicalUpDownWidth;
            set { logicalUpDownWidth = value; upDownWidthSet = true; applyUpDownWidth(); }
        }

        private void applyUpDownWidth()
        {
            if (!upDownWidthSet) return;
            int w = LogicalToDeviceUnits(logicalUpDownWidth);
            numericBoxWidth.UpDownWidth = numericBoxHeight.UpDownWidth = w;
        }

        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); applyUpDownWidth(); }
        protected override void OnDpiChangedAfterParent(EventArgs e) { base.OnDpiChangedAfterParent(e); applyUpDownWidth(); }

        #endregion

        #region 値変更通知 (ValueChanged)

        // numericBoxのいずれかが変化したときに発火する統合イベント。
        // 親フォーム側で numericBox を個別購読しなくて済むようにする (IndexControl と同じパターン)。
        [Category("Action")]
        [Description("Width, Height のいずれかの値が変化したときに発火する。")]
        public event EventHandler ValueChanged;

        // numericBoxWidth/Height の ValueChanged を受け、スナップ → アスペクト同期 → 総px ガードを適用して統合発火する。
        private void numericBoxValueChanged(object sender, EventArgs e)
        {
            if (internalUpdate) return;
            internalUpdate = true;
            try
            {
                var source = sender as NumericBox ?? numericBoxWidth;
                var other = source == numericBoxWidth ? numericBoxHeight : numericBoxWidth;

                // 1) 2 の冪スナップ (変更された側)
                if (SnapToPowerOfTwo)
                    source.Value = nearestPowerOfTwo(source.ValueInteger);

                // 2) アスペクト比固定 (ON) / 基準アスペクト更新 (OFF)
                if (keepAspectRatio)
                {
                    // aspectRatio = W:H。W 変更時 H = W / 比、H 変更時 W = H × 比。
                    double v = source == numericBoxWidth
                        ? source.Value / aspectRatio
                        : source.Value * aspectRatio;
                    other.Value = clampToRange((int)Math.Round(v));
                    if (SnapToPowerOfTwo)
                        other.Value = nearestPowerOfTwo(other.ValueInteger);
                }
                else if (numericBoxHeight.Value != 0)
                    aspectRatio = (double)numericBoxWidth.Value / numericBoxHeight.Value;

                // 3) 総ピクセル数ガード: 超過時は変更していない側 (other) を縮める
                if (MaxTotalPixels > 0 &&
                    (long)numericBoxWidth.ValueInteger * numericBoxHeight.ValueInteger > MaxTotalPixels)
                {
                    int cap = clampToRange((int)(MaxTotalPixels / Math.Max(1, source.ValueInteger)));
                    other.Value = cap;
                }
            }
            finally { internalUpdate = false; }

            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private int clampToRange(int v) => Math.Min(maximum, Math.Max(minimum, v));

        #endregion
    }
}

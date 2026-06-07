using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls;

[TypeConverter(typeof(DefinitionOrderTypeConverter))]
[ToolboxItem(true)] // 260605Cl 追加: 基底 UserControlBase の [ToolboxItem(false)] 継承を打ち消しデザイナのツールボックスに表示
public partial class ColorControl : UserControlBase
{
    public event EventHandler ColorChanged;


    [DefaultValue(typeof(FlowDirection), "LeftToRight")]
    [Category("Appearance property")]
    public FlowDirection FlowDirection
    {
        get => flowLayoutPanel.FlowDirection;
        set
        {
            flowLayoutPanel.FlowDirection = value;
            pictureBox.Dock = (value == FlowDirection.TopDown || value == FlowDirection.BottomUp) ?
            DockStyle.Right : DockStyle.None;
        }
    }

    [DefaultValue(typeof(Size), "24,24")]
    [Category("Appearance property")]
    public Size BoxSize { get => pictureBox.Size; set => pictureBox.Size = value; }

    [DefaultValue("")]
    [Localizable(true)]
    [Browsable(false)] // 260531Cl 追加: デザイナのプロパティグリッドから隠す(標準 ToolTip 拡張子と二重表示の混乱を解消)。Localizable は残すので既存 resx 値は従来通り pictureBox に適用され hover も維持
    [EditorBrowsable(EditorBrowsableState.Never)] // 260531Cl 追加: IntelliSense からも隠す(廃止予定プロパティ)
    public string ToolTip { set => toolTip.SetToolTip(pictureBox, value); get => toolTip.GetToolTip(pictureBox); }

    // 260531Cl 追加: 配置先 Form が標準 ToolTip でこの ColorControl 本体にチップを設定した場合の配布先 (内部子)。
    // swatch (pictureBox) とラベル上で hover してもチップが表示される (UserControlBase.RelayHostToolTip 参照)。
    protected override Control[] GetToolTipTargets() => new Control[] { pictureBox, labelHeader, labelFooter };

    // 260531Cl 追加: 独自プロパティ由来の内部 ToolTip。親がチップを設定した場合はこれを抑止して親のバルーンへ一本化する。
    protected internal override ToolTip InternalToolTip => toolTip;

    [DefaultValue("")]
    [Category("Header/footer text")]
    [Localizable(true)]
    public string HeaderText { set { labelHeader.Text = value; labelHeader.Visible = value != ""; } get => labelHeader.Text; }

    //[DefaultValue("")] // 260607Cl 修正: Font プロパティに空文字の DefaultValue は不正(型不一致で常に直列化)。リフレクション検証で構築直後の labelHeader.Font は Segoe UI 9pt
    [DefaultValue(typeof(Font), "Segoe UI, 9pt")]
    [Category("Header/footer text")]
    [Localizable(true)]
    public Font HeaderFont { set => labelHeader.Font = value; get => labelHeader.Font; }

    [DefaultValue(typeof(System.Windows.Forms.Padding), "0, 0, 0, 0")] // 260607Cl
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Header/footer text")]
    [Localizable(true)]
    public Padding HeaderMargin { set => labelHeader.Margin = value; get => labelHeader.Margin; }

    [DefaultValue("")] // 260607Cl
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Header/footer text")]
    [Localizable(true)]
    public string FooterText { set { labelFooter.Text = value; labelFooter.Visible = value != ""; } get => labelFooter.Text; }


    [DefaultValue(typeof(Font), "Segoe UI, 9.75pt")] // 260607Cl
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Localizable(true)]
    [Category("Header/footer text")]
    public Font FooterFont { set => labelFooter.Font = value; get => labelFooter.Font; }

    [DefaultValue(typeof(System.Windows.Forms.Padding), "0, 0, 0, 0")] // 260607Cl
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Localizable(true)]
    [Category("Header/footer text")]
    public Padding FooterMargin { set => labelFooter.Margin = value; get => labelFooter.Margin; }


    [DefaultValue(false)]
    [Category("Color")]
    public bool Inversion { set; get; } = false;

    [DefaultValue(typeof(Color), "Control")] // 260607Cl
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Color")]
    public Color Color
    {
        set => pictureBox.BackColor = value;
        get
        {
            var color = pictureBox.BackColor;
            return Inversion ? Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B) : color;
        }
    }

    // (260322Ch) WFO1000: Microsoft ??????????????????? ???????????
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)] // 260607Cl 変更: Visible→Hidden。Color の冗長な別表現(同一色を Argb/Red/Green/Blue/*F の8通りで二重直列化)を抑止し Color 1本へ集約。実行時挙動は不変(既存 Designer.cs 行はそのまま動作、再シリアライズ時に書き出さなくなるのみ)
    [Category("Color")]
    public int Argb
    {
        set => pictureBox.BackColor = Color.FromArgb(value);
        get => Color.ToArgb();
    }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)] // 260607Cl 変更: Color のエイリアス→Hidden
    [Category("Color")]
    public int Red
    {
        set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(value, pictureBox.BackColor.G, pictureBox.BackColor.B); }
        get => Inversion ? 255 - pictureBox.BackColor.R : pictureBox.BackColor.R;
    }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)] // 260607Cl 変更: Color のエイリアス→Hidden
    [Category("Color")]
    public int Green
    {
        set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, value, pictureBox.BackColor.B); }
        get => Inversion ? 255 - pictureBox.BackColor.G : pictureBox.BackColor.G;
    }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)] // 260607Cl 変更: Color のエイリアス→Hidden
    [Category("Color")]
    public int Blue
    {
        set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, pictureBox.BackColor.G, value); }
        get => Inversion ? 255 - pictureBox.BackColor.B : pictureBox.BackColor.B;
    }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)] // 260607Cl 変更: Color のエイリアス→Hidden
    [Category("Color")]
    public float RedF
    {
        set { if (value >= 0 && value <= 1) pictureBox.BackColor = Color.FromArgb((int)(value * 255), pictureBox.BackColor.G, pictureBox.BackColor.B); }
        get => Inversion ? 1 - pictureBox.BackColor.R / 255f : pictureBox.BackColor.R / 255f;
    }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)] // 260607Cl 変更: Color のエイリアス→Hidden
    [Category("Color")]
    public float GreenF
    {
        set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, (int)(value * 255), pictureBox.BackColor.B); }
        get => Inversion ? 1 - pictureBox.BackColor.G / 255f : pictureBox.BackColor.G / 255f;
    }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)] // 260607Cl 変更: Color のエイリアス→Hidden
    [Category("Color")]
    public float BlueF
    {
        set { if (value >= 0 && value < 256) pictureBox.BackColor = Color.FromArgb(pictureBox.BackColor.R, pictureBox.BackColor.G, (int)(value * 255)); }
        get => Inversion ? 1 - pictureBox.BackColor.B / 255f : pictureBox.BackColor.B / 255f;
    }

    public ColorControl()
    {
        InitializeComponent();
    }

    private void pictureBox_Click(object sender, EventArgs e)
    {
        var colorDialog = new ColorDialog
        {
            Color = pictureBox.BackColor,
            AllowFullOpen = true,
            AnyColor = true,
            SolidColorOnly = false,
            ShowHelp = true
        };
        colorDialog.ShowDialog();
        pictureBox.BackColor = colorDialog.Color;
    }

    private void pictureBox_BackColorChanged(object sender, EventArgs e)
    {
        ColorChanged?.Invoke(sender, e);
    }

}

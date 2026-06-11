using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Crystallography.Controls;

[ToolboxItem(true)] // 260605Cl 追加: 基底 UserControlBase の [ToolboxItem(false)] 継承を打ち消しデザイナのツールボックスに表示
public partial class GraphControl : UserControlBase
{

    #region コンストラクタ、ロード
    public GraphControl()
    {
        InitializeComponent();
        //260607Ch 仕様変更: 上部/下部パネルは公開プロパティではなく、タイトル/マウス位置/範囲/Copy の表示状態から自動判定する。
    }
    #endregion

    #region イベント

    public delegate void DrawingRangeChangedEventHandler(RectangleD rectangle);

    public event DrawingRangeChangedEventHandler DrawingRangeChanged;

    public delegate void LinePositionChengedEventHandler();

    public event LinePositionChengedEventHandler LinePositionChanged;

    public delegate bool MouseEventHandler2(PointD pt);

    /// <summary>マウスが左ダブルクリックされたときのイベント。戻り値がtrueの場合は、その後の動作をスキップ</summary>
    public event MouseEventHandler2 MouseDoubleClick2;

    #endregion イベント

    #region プロパティ、フィールド

    /// <summary>グラフの描き方の列挙体</summary>
    public enum DrawingMode { Line, Histogram, Point }

    private Bitmap Bmp;
    private Graphics G;

    #region 描画囲プロパティ
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("X軸の描画範囲")]
    [DefaultValue(1.0)] //260607Cl 追加: 既定値と一致する冗長なデザイナ初期化を抑制
    public double UpperX { get => upperX; set { if (!double.IsNaN(value) && !double.IsInfinity(value)) upperX = value; } }
    private double upperX = 1;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("X軸の描画範囲")]
    [DefaultValue(0.0)] //260607Cl 追加
    public double LowerX { get => lowerX; set { if (!double.IsNaN(value) && !double.IsInfinity(value)) lowerX = value; } }
    private double lowerX = 0;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("Y軸の描画範囲")]
    [DefaultValue(1.0)] //260607Cl 追加
    public double UpperY { get => upperY; set { if (!double.IsNaN(value) && !double.IsInfinity(value)) upperY = value; } }
    private double upperY = 1;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("Y軸の描画範囲")]
    [DefaultValue(0.0)] //260607Cl 追加
    public double LowerY { get => lowerY; set { if (!double.IsNaN(value) && !double.IsInfinity(value)) lowerY = value; } }
    private double lowerY = 0;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("X軸の上下限")]
    [DefaultValue(1.0)] //260607Cl 追加
    public double MaximalX { get => maximalX; set { if (!double.IsNaN(value) && !double.IsInfinity(value)) maximalX = value; } }
    private double maximalX = 1;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("X軸の上下限")]
    [DefaultValue(0.0)] //260607Cl 追加
    public double MinimalX { get => minimalX; set { if (!double.IsNaN(value) && !double.IsInfinity(value)) minimalX = value; } }
    private double minimalX = 0;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("Y軸の上下限")]
    [DefaultValue(1.0)] //260607Cl 追加
    public double MaximalY { get => maximalY; set { if (!double.IsNaN(value) && !double.IsInfinity(value)) maximalY = value; } }
    private double maximalY = 1;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("Y軸の上下限")]
    [DefaultValue(0.0)] //260607Cl 追加
    public double MinimalY { get => minimalY; set { if (!double.IsNaN(value) && !double.IsInfinity(value)) minimalY = value; } }
    private double minimalY = 0;

    /// <summary>描画範囲の矩形</summary>
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RectangleD DrawingRange
    {
        set
        {
            if (!double.IsInfinity(value.X) && !double.IsNaN(value.X) && value.X <= MaximalX && value.X >= MinimalX)
                LowerX = value.X;

            if (!double.IsInfinity(value.Y) && !double.IsNaN(value.Y) && value.Y <= MaximalY && value.Y >= MinimalY)
                LowerY = value.Y;

            if (!double.IsInfinity(value.Width) && !double.IsNaN(value.Width) && value.Width >= 0)
                UpperX = Math.Min(LowerX + value.Width, MaximalX);

            if (!double.IsInfinity(value.Height) && !double.IsNaN(value.Height) && value.Height >= 0)
                UpperY = Math.Min(LowerY + value.Height, MaximalY);
        }
        get
        {
            if (!double.IsInfinity(LowerX) && !double.IsInfinity(LowerY) && !double.IsInfinity(UpperX) && !double.IsInfinity(UpperY))
                return new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY);
            else
                return new RectangleD(0, 0, 1, 1);
        }
    }

    #endregion

    #region 上部パネル

    //260607Ch 仕様変更: UpperPanelVisible は公開プロパティから撤去。GraphTitle / MousePositionVisible から自動判定する。
    //旧コード: public bool UpperPanelVisible { set => panelTitleAndMouse.Visible = value; get => panelTitleAndMouse.Visible; }

    [Category(" 4. 上部パネル")]
    [Description("上部パネルに表示する文字のフォント")]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof(Font), "Segoe UI, 9.5pt")] //260607Cl 修正: リフレクション検証で構築直後の labelX1.Font は 9.5pt と判明 (9pt は誤り)。実値に一致させ冗長直列化を正しく抑止
    //public Font UpperPanelFont { set => labelGraphTitle.Font = labelX1.Font = labelXValue.Font = labelY1.Font = labelYValue.Font = value; get => labelX1.Font; }//260611Cl 旧: 凡例ラベルにも反映するため block body 化
    public Font UpperPanelFont
    {
        set
        {
            labelGraphTitle.Font = labelX1.Font = labelXValue.Font = labelY1.Font = labelYValue.Font = value;
            foreach (var label in legendLabels)
                label.Font = value;//260611Ch 整理: 動的凡例ラベルにも適用
        }
        get => labelX1.Font;
    }

    /// <summary>グラフの名前</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 4. 上部パネル")]
    [Description("グラフタイトル(上部パネルの最初に表示される)")]
    [DefaultValue("")] //260607Cl 修正: 実際の既定は labelGraphTitle.Text="" (Designer.cs から Text=" " を撤去済)
    public string GraphTitle
    {
        set
        {
            labelGraphTitle.Text = value ?? "";
            //labelGraphTitle.Visible = !string.IsNullOrEmpty(labelGraphTitle.Text);//260611Ch 旧: タイトル行と親パネルの表示更新へ集約
            updateUpperPanelVisibility();// 260611Ch 整理: タイトル/凡例/マウス位置の意図値から一括更新
        }
        get => labelGraphTitle.Text;
    }//260607Ch 変更: 上部パネルはタイトルまたはマウス位置表示が有効な時だけ表示

    /// <summary>タイトル行(flowLayoutPanelTitle)に動的に並べた凡例ラベル (labelGraphTitle1, labelGraphTitle2, ...)。260611Cl 追加。</summary>
    private readonly List<Label> legendLabels = [];

    /// <summary>タイトルの右に、曲線名を曲線色で表示する凡例ラベルを並べる。引数なし(または null)で消去。
    /// ラベルが横幅に収まらないときは次の行へ折り返す (flowLayoutPanelTitle.WrapContents=既定true + AutoSize 連鎖)。260611Cl 追加。</summary>
    public void SetLegend(params (string Text, Color Color)[] items)
    {
        flowLayoutPanelTitle.SuspendLayout();//260611Cl 追加: 削除・追加のたびの再フロー計算 (折り返し有効化で増加) を一括化
        foreach (var label in legendLabels)
        {
            flowLayoutPanelTitle.Controls.Remove(label);
            label.Dispose();
        }
        legendLabels.Clear();

        if (items != null)
        {
            foreach (var (text, color) in items)
            {
                var label = new Label
                {
                    AutoSize = true,
                    Font = labelGraphTitle.Font,
                    ForeColor = color,
                    Margin = new Padding(12, 0, 0, 0),//タイトル・前の凡例との間隔
                    Name = $"labelGraphTitle{legendLabels.Count + 1}",
                    Text = text,
                };
                legendLabels.Add(label);
                flowLayoutPanelTitle.Controls.Add(label);
            }
        }
        flowLayoutPanelTitle.ResumeLayout();//260611Cl 追加
        updateUpperPanelVisibility();
    }

    /// <summary>タイトル行(タイトル文字列または凡例あり)と上部パネルの表示状態を一括更新する。260611Cl 追加。</summary>
    private void updateUpperPanelVisibility()
    {
        bool hasTitle = !string.IsNullOrEmpty(labelGraphTitle.Text);
        bool hasTitleRow = hasTitle || legendLabels.Count > 0;//260611Ch 整理: Visible getter は親非表示時に false になるため意図値で判定

        labelGraphTitle.Visible = hasTitle;
        flowLayoutPanelTitle.Visible = hasTitleRow;
        panelTitleAndMouse.Visible = hasTitleRow || mousePositionVisible;
    }

    /// <summary>マウス位置を表示するかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 4. 上部パネル")]
    [Description("マウス位置を表示するかどうか")]
    [DefaultValue(false)] //260607Cl 修正: 実際の既定は flowLayoutPanelMousePosition.Visible=false (Designer.cs)
    public bool MousePositionVisible
    {
        set
        {
            mousePositionVisible = value;
            flowLayoutPanelMousePosition.Visible = value;
            //panelTitleAndMouse.Visible = !string.IsNullOrEmpty(labelGraphTitle.Text) || mousePositionVisible;// 260610Cl 修正: 同上 (意図値から判定)
            updateUpperPanelVisibility();// 260611Ch 整理: 表示判定を共通メソッドへ集約
        }
        get => mousePositionVisible;
    }//260607Ch 変更: 親パネル非表示時も意図値を保持し、上部パネルを自動判定
    private bool mousePositionVisible = false;

    /// <summary>
    /// マウス位置の有効桁数 (-1で無指定)
    /// /// </summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 4. 上部パネル")]
    [Description("マウスX位置の有効桁数 (-1で無指定)")]
    [DefaultValue(-1)] //260607Cl 追加
    public int MousePositionXDigit { set; get; } = -1;

    /// <summary>
    /// マウスX位置の書式指定子 (.NET 数値書式文字列。例: "f3", "e2", "0.###")。
    /// 空文字 (既定) または不正な書式の場合は MousePositionXDigit に従う従来挙動。
    /// 有効な書式が指定されている場合は MousePositionXDigit を無視してこちらを優先する。
    /// </summary>
    [Category(" 4. 上部パネル")]
    [Description("マウスX位置の書式指定子 (空/不正なら MousePositionXDigit にフォールバック)")]
    [DefaultValue("")]
    public string MousePositionX_FormatSpecifier // 260608Cl 追加
    {
        get => mousePositionX_FormatSpecifier;
        set
        {
            mousePositionX_FormatSpecifier = value ?? "";
            mousePositionX_FormatSpecifierValid = IsValidFormatSpecifier(mousePositionX_FormatSpecifier); // 有効性を判定してキャッシュ (判定は UserControlBase に集約)
        }
    }
    private string mousePositionX_FormatSpecifier = ""; // 260608Cl 追加
    private bool mousePositionX_FormatSpecifierValid = false; // 260608Cl 追加

    /// <summary>
    /// マウス位置の有効桁数 (-1で無指定)
    /// /// </summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 4. 上部パネル")]
    [Description("マウスYY位置の有効桁数 (-1で無指定)")]
    [DefaultValue(-1)] //260607Cl 追加
    public int MousePositionYDigit { set; get; } = -1;

    /// <summary>
    /// マウスY位置の書式指定子 (.NET 数値書式文字列。例: "f3", "e2", "0.###")。
    /// 空文字 (既定) または不正な書式の場合は MousePositionYDigit に従う従来挙動。
    /// 有効な書式が指定されている場合は MousePositionYDigit を無視してこちらを優先する。
    /// </summary>
    [Category(" 4. 上部パネル")]
    [Description("マウスY位置の書式指定子 (空/不正なら MousePositionYDigit にフォールバック)")]
    [DefaultValue("")]
    public string MousePositionY_FormatSpecifier // 260608Cl 追加
    {
        get => mousePositionY_FormatSpecifier;
        set
        {
            mousePositionY_FormatSpecifier = value ?? "";
            mousePositionY_FormatSpecifierValid = IsValidFormatSpecifier(mousePositionY_FormatSpecifier); // 有効性を判定してキャッシュ (判定は UserControlBase に集約)
        }
    }
    private string mousePositionY_FormatSpecifier = ""; // 260608Cl 追加
    private bool mousePositionY_FormatSpecifierValid = false; // 260608Cl 追加 (判定は UserControlBase.IsValidFormatSpecifier)

    // 260608Cl 追加: マウス/マーカー座標の表示書式を解決する。
    // FormatSpecifier が有効ならそれを優先、無効/空文字なら従来どおり (log? "E":"g")+桁数 を使う。
    private string resolveXFormat(bool log) =>
        mousePositionX_FormatSpecifierValid ? mousePositionX_FormatSpecifier
        : (log ? "E" : "g") + (MousePositionXDigit == -1 ? "" : MousePositionXDigit.ToString());

    private string resolveYFormat(bool log) =>
        mousePositionY_FormatSpecifierValid ? mousePositionY_FormatSpecifier
        : (log ? "E" : "g") + (MousePositionYDigit == -1 ? "" : MousePositionYDigit.ToString());

    /// <summary>X軸の単位</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 4. 上部パネル")]
    [Description("X軸の単位")]
    [DefaultValue("")] //260607Cl 追加
    public string UnitX { get; set; } = "";

    /// <summary>Y軸の単位</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 4. 上部パネル")]
    [Description("Y軸の単位")]
    [DefaultValue("")] //260607Cl 追加
    public string UnitY { get; set; } = "";

    /// <summary>X軸のラベル</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 4. 上部パネル")]
    [Description("X軸のラベル")]
    [DefaultValue("X:")] //260607Cl 追加: 既定はlabelX1.Text="X:"(Designer.cs)
    public string LabelX { set => labelX1.Text = labelX2.Text = value; get => labelX1.Text; }

    /// <summary>Y軸のラベル</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 4. 上部パネル")]
    [Description("Y軸のラベル")]
    [DefaultValue("Y:")] //260607Cl 追加: 既定はlabelY1.Text="Y:"(Designer.cs)
    public string LabelY { set => labelY1.Text = labelY2.Text = value; get => labelY1.Text; }

    #endregion

    #region 下部パネル

    //260607Ch 仕様変更: LowerPanelVisible は公開プロパティから撤去。RangePanelVisible / CopyVisible から自動判定する。
    //旧コード: public bool LowerPanelVisible { set => panelRangeAndCopy.Visible = value; get => panelRangeAndCopy.Visible; }

    /// <summary>描画範囲を数値入力するパネル(flowLayoutPanelRange)を表示するかどうか。既定はfalse。</summary> //260606Cl 追加
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 5. 下部パネル")]
    [Description("描画範囲を数値入力するパネル(X/Yの最小・最大)を表示するかどうか")]
    [DefaultValue(false)]
    public bool RangePanelVisible
    {
        set { rangePanelVisible = value; flowLayoutPanelRange.Visible = value; if (value) updateRangeBoxes(); panelRangeAndCopy.Visible = rangePanelVisible || copyVisible; }//260607Ch 変更: 下部パネルは範囲パネルまたはCopy表示が有効な時だけ表示
        get => rangePanelVisible;
    }
    private bool rangePanelVisible = false;

    /// <summary>Copyボタン(グラフをベクター画像EMFでクリップボードへコピー)を表示するかどうか。既定はfalse。260607Cl 追加。</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 5. 下部パネル")]
    [Description("Copyボタン(グラフをベクター画像EMFでクリップボードへコピー)を表示するかどうか")]
    [DefaultValue(false)] //260607Cl 修正: 実際の既定は buttonCopy.Visible=false (Designer.cs)
    public bool CopyVisible { set { copyVisible = value; buttonCopy.Visible = value; panelRangeAndCopy.Visible = rangePanelVisible || copyVisible; } get => copyVisible; }//260607Ch 変更: 親パネル非表示時も意図値を保持し、下部パネルを自動判定
    private bool copyVisible = false;

    #endregion

    #region 軸ラベル設定

    /// <summary>X軸(下)に表示する説明ラベル。空でなければ下側の余白を自動で広げる。</summary> //260607Cl 追加
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("X軸(下)に表示する説明ラベル(空なら非表示)")]
    [DefaultValue("")]
    public string AxisLabelX { set { axisLabelX = value ?? ""; Draw(); } get => axisLabelX; }
    private string axisLabelX = "";

    /// <summary>Y軸(左)に表示する説明ラベル(反時計回り90度)。空でなければ左側の余白を自動で広げる。</summary> //260607Cl 追加
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("Y軸(左)に表示する説明ラベル(反時計回り90度。空なら非表示)")]
    [DefaultValue("")]
    public string AxisLabelY { set { axisLabelY = value ?? ""; Draw(); } get => axisLabelY; }
    private string axisLabelY = "";

    #endregion

    #region 動作プロパティ
    /// <summary>マウス操作を受け付けるかどうか</summary>
    // (260322Ch) WFO1000: Microsoft ??????????????????? ???????????
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("マウス操作を受け付けるかどうか")]
    [DefaultValue(true)] //260607Cl 追加
    public bool AllowMouseOperation { get; set; } = true;

    /// <summary>Profileを更新時、横軸を固定するかどうか(ただし、上限下限内で)</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("Profileを更新時、横軸を固定するかどうか(ただし、上限下限内で)")]
    [DefaultValue(false)] //260607Cl 追加
    public bool FixRangeHorizontal { set; get; } = false;

    /// <summary>Profileを更新時、縦軸を固定するかどうか(ただし、上限下限内で)</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("Profileを更新時、縦軸を固定するかどうか(ただし、上限下限内で)")]
    [DefaultValue(false)] //260607Cl 追加
    public bool FixRangeVertical { set; get; } = false;

    /// <summary>横軸(X)の下限を常に0に固定する(非Log時かつデータが負を含まない時のみ有効)。EDX/回折パターン等の0始点表示用。既定false。260607Cl 追加。</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("横軸(X)の下限を常に0に固定する(非Log時のみ有効)")]
    [DefaultValue(false)] //260607Cl 追加
    public bool FixLowerXToZero { set; get; } = false;
    #endregion

    #region 垂直線

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category(" 6. 垂直線")]
    [Description("垂直線のリスト")]
    public PointD[] VerticalLines
    {
        set
        {
            if (value != null)
            {
                verticalLineList.Clear();
                verticalLineList.AddRange(value);
            }
        }
        get => verticalLineList.ToArray();
    }
    private readonly List<PointD> verticalLineList = [];

    /// <summary></summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 6. 垂直線")]
    [Description("垂直線の色")]
    [DefaultValue(typeof(Color), "Red")] //260607Cl 追加
    public Color VerticalLineColor { set; get; } = Color.Red;//260603Cl レビューメモ: setterで Draw() を呼ばないため実行時の色変更が次回再描画まで反映されない(AxisLineColor / AxisTextColor / BackgroundColor も同様の不統一)。

    /// <summary>垂直線と各プロファイルの交点にマーカー(丸)と値を表示するかどうか</summary> //260603Cl 追加
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 6. 垂直線")]
    [Description("垂直線と各プロファイルの交点にマーカー(丸)と値を表示するかどうか")]
    [DefaultValue(false)] //260607Cl 追加
    public bool VerticalLineMarkerVisible { set { verticalLineMarkerVisible = value; Draw(); } get => verticalLineMarkerVisible; }
    private bool verticalLineMarkerVisible = false;

    /// <summary>交点マーカー(丸)の半径(ピクセル)</summary> //260603Cl 追加
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 6. 垂直線")]
    [Description("交点マーカー(丸)の半径(ピクセル)")]
    [DefaultValue(3.5f)] //260607Cl 追加
    public float VerticalLineMarkerRadius { set; get; } = 3.5f;

    private int selectedVerticalLineIndex = -1;

    #endregion

    #region 注釈ラベル (任意位置テキスト) 260607Cl 追加

    /// <summary>グラフ上の任意位置に描くテキスト注釈。X,Y はデータ座標。
    /// Y が NaN のときはプロット領域の上端付近に描く (回折ピークの hkl ラベル等)。260607Cl 追加。</summary>
    /// <param name="X">データ座標 X (Log 軸なら実座標で渡す。内部で log10 変換)</param>
    /// <param name="Y">データ座標 Y。double.NaN なら上端付近に配置</param>
    /// <param name="Text">描画する文字列</param>
    /// <param name="Color">文字色 (兼ガイド線色)</param>
    /// <param name="Vertical">true で反時計 90 度回転して描く (狭いピーク間隔向け)</param>
    /// <param name="GuideLine">true で X 位置にプロット全高の薄い点線ガイド縦線を引く</param>
    public readonly record struct GraphAnnotation(
        double X, double Y, string Text, Color Color, bool Vertical = false, bool GuideLine = false);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GraphAnnotation[] Annotations
    {
        //260607Cl VerticalLines と同じく setter では再描画しない (呼び出し側が AddProfiles/Draw で一括描画。二重 Draw を避ける)
        set { annotationList.Clear(); if (value != null) annotationList.AddRange(value); }
        get => annotationList.ToArray();
    }
    private readonly List<GraphAnnotation> annotationList = [];

    #endregion

    #region ピーク関数
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public PeakFunction[] Peaks { set { peaks.Clear(); peaks.AddRange(value); } get => peaks.ToArray(); }
    private List<PeakFunction> peaks = [];
    #endregion

    #region プロファイル
    /// <summary>0番目のプロファイルを設定する。複数プロファイルが設定されている場合はひとつだけにする。</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    [Category(" 1. プロファイル・表示")]
    [Browsable(false)]
    public Profile Profile
    {
        set
        {
            srcProfileList = [value];
            InitializeAxis();
            resetDrawRange();
            Draw();
        }
        get
        {
            if (srcProfileList == null || srcProfileList.Count == 0)
                return null;
            else
                return srcProfileList[0];
        }
    }
    private List<Profile> srcProfileList = [];


    [Category(" 1. プロファイル・表示")]
    [Browsable(false)]
    public Profile[] ProfileList => srcProfileList.ToArray();

    /// <summary>共通のプロファイル描画線の太さ (UseLineWidthがtrueの場合に有効)</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 1. プロファイル・表示")]
    [Description("共通のプロファイル描画線の太さ (UseLineWidthがtrueの場合に有効)")]
    [DefaultValue(1f)] //260607Cl 追加
    public float LineWidth { set { lineWidth = value; Draw(); } get => lineWidth; }
    private float lineWidth = 1f;

    /// <summary>共通のプロファイル描画線太さを使うか (使わない場合は各ProfileのlineWidthを使う)</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 1. プロファイル・表示")]
    [Description("共通のプロファイル描画線太さを使うか (使わない場合は各ProfileのlineWidthを使う))")]
    [DefaultValue(true)] //260607Cl 追加
    public bool UseLineWidth { set { useLineWidth = value; Draw(); } get => useLineWidth; }
    private bool useLineWidth = true;

    #endregion

    #region 補助線プロパティ
    /// <summary>目盛補助線の色</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("目盛補助線の色")]
    [DefaultValue(typeof(Color), "LightGray")] //260607Cl 追加
    public Color DivisionLineColor { set; get; } = Color.LightGray;

    /// <summary>X軸の補助目盛線を表示するかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("X軸の補助目盛線を表示するかどうか")]
    [DefaultValue(true)] //260607Cl 追加
    public bool DivisionLineXVisible
    {
        set
        {
            if (DivisionLineXVisible != value)
            {
                divisionLineXVisible = value;
                toolStripMenuItemScaleLineX.Checked = value;
                Draw();
            }
        }
        get => divisionLineXVisible;
    }
    private bool divisionLineXVisible = true;

    /// <summary>Y軸の目盛りを表示するかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("Y軸の補助目盛線を表示するかどうか")]
    [DefaultValue(true)] //260607Cl 追加
    public bool DivisionLineYVisible
    {
        set
        {
            if (DivisionLineYVisible != value)
            {
                divisionLineYVisible = value;
                toolStripMenuItemScaleLineY.Checked = value;
                Draw();
            }
        }
        get => divisionLineYVisible;
    }
    private bool divisionLineYVisible = true;

    #endregion

    #region 軸設定

    /// <summary>目盛線の色</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("軸線の色")]
    [DefaultValue(typeof(Color), "Gray")] //260607Cl 追加
    public Color AxisLineColor { set; get; } = Color.Gray;

    /// <summary>軸文字の色</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("軸文字の色")]
    [DefaultValue(typeof(Color), "Black")] //260607Cl 追加
    public Color AxisTextColor { set; get; } = Color.Black;

    /// <summary>軸文字の色</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("軸文字のフォント")]
    [DefaultValue(typeof(Font), "Segoe UI, 9pt")] //260607Cl 追加
    public Font AxisTextFont { set; get; } = new Font(WineCompat.Resolve("Segoe UI"), 9); //260610Cl Wine時フォント切替 (Windowsでは従来どおり Segoe UI)


    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("X軸上の数値を表示するかどうか")]
    [DefaultValue(true)] //260607Cl 追加
    public bool AxisXTextVisible { set { horizontalGradiationTextVisivle = value; Draw(); } get => horizontalGradiationTextVisivle; }
    private bool horizontalGradiationTextVisivle = true;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("Y軸上の数値を表示するかどうか")]
    [DefaultValue(true)] //260607Cl 追加
    public bool AxisYTextVisible { set { verticalGradiationTextVisivle = value; Draw(); } get => verticalGradiationTextVisivle; }
    private bool verticalGradiationTextVisivle = true;

    /// <summary>X軸が対数スケールかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("X軸が対数スケールかどうか")]
    [DefaultValue(false)] //260607Cl 追加
    public bool XLog
    {
        set
        {
            if (XLog != value)
            {
                xLog = value;
                toolStripMenuItemLogScaleX.Checked = XLog;
                InitializeAxis();
                setDrawRangeLimit();
                resetDrawRange();
                Draw();
            }
        }
        get => xLog;
    }
    private bool xLog = false;

    /// <summary>Y軸が対数スケールかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("Y軸が対数スケールかどうか")]
    [DefaultValue(false)] //260607Cl 追加
    public bool YLog
    {
        set
        {
            if (YLog != value)
            {
                yLog = value;
                toolStripMenuItemLogScaleY.Checked = YLog;
                InitializeAxis(); setDrawRangeLimit();
                resetDrawRange();
                Draw();
            }
        }
        get => yLog;
    }
    private bool yLog = false;

    /// <summary>Xの値が0以上の整数値かどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("Xの値が0以上の整数値かどうか")]
    [DefaultValue(false)] //260607Cl 追加
    public bool IsIntegerX { set { isIntegerX = value; InitializeAxis(); resetDrawRange(); Draw(); } get => isIntegerX; }
    private bool isIntegerX = false;

    /// <summary>Yの値が０以上の整数値かどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 2. 軸")]
    [Description("Yの値が0以上の整数値かどうか")]
    [DefaultValue(false)] //260607Cl 追加
    public bool IsIntegerY { set { isIntegerY = value; InitializeAxis(); resetDrawRange(); Draw(); } get => isIntegerY; }
    private bool isIntegerY = false;
    #endregion

    #region グラフ位置
    /// <summary>原点の位置(左下からのピクセル単位)</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 3. 描画範囲・操作")]
    [Description("原点の位置(左下からのピクセル単位)")]
    [DefaultValue(typeof(Point), "40, 20")] //260607Cl 追加
    public Point OriginPosition { set { userOrigin = value; Draw(); } get => userOrigin; }//260607Cl: get/setはユーザー設定値(userOrigin)。描画用の実効原点(originPosition)はrecalcDrawOriginで軸ラベル余白を加算して算出
    private Point userOrigin = new(40, 20);//260607Cl 追加: OriginPositionのユーザー設定値
    private Point originPosition = new(40, 20);//描画・座標変換で使う実効原点(userOrigin + 軸ラベル余白。recalcDrawOriginで更新)
    //260607Cl 削除: BottomMargin/LeftMargin プロパティを撤去。LeftMargin は座標変換で未使用(完全デッド)、BottomMargin は全アプリで0設定の死蔵で軸ラベル余白(mBottom)と混同を招くため。出自は PDIndexer FormMain の自前描画(結晶ピークバー領域確保)。
    #endregion

    #region その他
    /// <summary>グラフの背景色</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 1. プロファイル・表示")]
    [Description(" グラフの背景色")]
    [DefaultValue(typeof(Color), "White")] //260607Cl 追加
    public Color BackgroundColor { set; get; } = Color.White;

    /// <summary>グラフの描画モード</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category(" 1. プロファイル・表示")]
    [Description(" グラフの描画モード")]
    [DefaultValue(DrawingMode.Line)] //260607Cl 追加
    public DrawingMode Mode { set { mode = value; Draw(); } get { return mode; } }
    private DrawingMode mode = DrawingMode.Line;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool Interpolation { set { interpolation = value; Draw(); } get => interpolation; }
    private bool interpolation = false;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool Smoothing { get; set; } = false;
    private int smoothingN = 0, smoothingM = 0;

    /// <summary>ピクチャーボックスのクライアント領域のサイズ</summary>
    [Browsable(false)]
    private Size PictureBoxSize => pictureBox.ClientSize;

    #endregion

    #endregion プロパティ

    private List<Profile> destProfileList = [];

    #region プロファイルの追加、置換、スムージング
    public void AddPoint(int profileNumber, PointD pt)
    {
        srcProfileList[profileNumber].Pt.Add(pt);
        InitializeAxis();
        setDrawRangeLimit();
        resetDrawRange();
        Draw();
    }

    /// <summary>プロファイルを加える</summary>
    /// <param name="p"></param>
    public void AddProfile(Profile p)
    {
        srcProfileList.Add(p);
        InitializeAxis();
        setDrawRangeLimit();
        resetDrawRange();
        Draw();
    }

    //public void AddProfiles(Profile[] p, RectangleD drawingRange = new RectangleD(), bool showLegend = false)//260611Cl 旧シグネチャ: minimalX/maximalX 追加
    /// <param name="drawingRange">初期表示範囲。NaN の成分は全範囲のまま (例: X だけ狭めるなら new RectangleD(0, double.NaN, 2, double.NaN))。既定 (Width=0) は全範囲表示。</param>
    /// <param name="minimalX">X軸のパン・ズーム下限。NaN なら従来どおりデータ範囲から自動 (データ最小 −1% 余白)。260611Cl 追加。</param>
    /// <param name="maximalX">X軸のパン・ズーム上限。NaN なら従来どおりデータ範囲から自動 (データ最大 +1% 余白)。260611Cl 追加。</param>
    public void AddProfiles(Profile[] p, RectangleD drawingRange = new RectangleD(), bool showLegend = false, double minimalX = double.NaN, double maximalX = double.NaN)
    {
        srcProfileList.Clear();
        srcProfileList.AddRange(p);
        //260611Cl 追加: showLegend=true なら各 Profile.text を曲線色の凡例ラベルとしてタイトル行に表示 (false なら既存凡例を消去)
        SetLegend(showLegend ? [.. p.Where(q => !string.IsNullOrEmpty(q?.text)).Select(q => (q.text, q.Color))] : null);
        InitializeAxis();
        setDrawRangeLimit();
        //260611Cl 追加: X軸の上下限を明示指定 (データ由来の自動値を上書き)
        if (!double.IsNaN(minimalX)) MinimalX = minimalX;
        if (!double.IsNaN(maximalX)) MaximalX = maximalX;
        //if (drawingRange.Width == 0)//260611Cl 旧: drawingRange 指定時は resetDrawRange を呼ばず、Y の表示範囲が前回値のまま残った
        //    resetDrawRange();
        //else
        //    DrawingRange = drawingRange;
        resetDrawRange();//常に全範囲へリセットした後、drawingRange の非 NaN 成分だけ上書き (DrawingRange セッターは NaN/範囲外を無視)
        if (drawingRange.Width != 0)
            DrawingRange = drawingRange;
        Draw();
    }

    public void ReplaceProfile(int index, Profile p)
    {
        if (index < srcProfileList.Count)
            srcProfileList[index] = p;
        InitializeAxis();
        setDrawRangeLimit();
        Draw();
    }

    public void SmoothProfiles(int m, int n)
    {
        smoothingM = m;
        smoothingN = n;
        InitializeAxis();
        setDrawRangeLimit();
        Draw();
    }

    public void ClearProfile()
    {
        srcProfileList.Clear();
        destProfileList.Clear();
        SetLegend(null);//260611Cl 追加: 凡例ラベルも消去
        pictureBox.Image = new Bitmap(PictureBoxSize.Width, PictureBoxSize.Height);
        UpperX = UpperY = MaximalX = MaximalY = 1;
        LowerX = LowerY = MinimalX = MinimalY = 0;
        Draw();
    }
    #endregion

    #region 軸の初期化、変換
    public void InitializeAxis()
    {
        if (srcProfileList == null) return;
        destProfileList = [];
        for (int i = 0; i < srcProfileList.Count; i++)
        {
            var temp = new Profile();
            if (srcProfileList[i] != null && srcProfileList[i].Pt != null && srcProfileList[i].Pt.Count != 0)
                temp = convertAxis(srcProfileList[i]);
            destProfileList.Add(temp);
            setDrawRangeLimit();
        }
    }

    /// <summary>軸を変換する</summary>
    private Profile convertAxis(Profile profile)
    {
        if (profile == null || profile.Pt == null || profile.Pt.Count == 0) return null;
        var p = profile.CopyTo();
        if (Smoothing)
            p = p.SmoothingSavitzkyGolay(smoothingM, smoothingN);

        var temp = new Profile();
        double x, y;
        for (int i = 0; i < p.Pt.Count; i++)
        {
            if (!xLog)
                x = p.Pt[i].X;
            else if (p.Pt[i].X == 0)
                if (isIntegerX)
                    x = 0;
                else
                    x = double.NegativeInfinity;
            else
                x = Math.Log10(p.Pt[i].X);

            if (!yLog)
                y = p.Pt[i].Y;
            else
            {
                if (p.Pt[i].Y == 0)
                    if (IsIntegerY)
                        y = 0;
                    else
                        y = double.NegativeInfinity;
                else if (p.Pt[i].Y > 0)
                    y = Math.Log10(p.Pt[i].Y);
                else
                    y = double.NaN;
            }

            if (!double.IsNaN(x) && !double.IsNaN(y))
                temp.Pt.Add(new PointD(x, y));
        }
        return temp;
    }
    #endregion

    #region 描画範囲の設定
    /// <summary>現在のプロファイルから描画範囲の上限、下限値を設定　描画範囲は変更しない</summary>
    private void setDrawRangeLimit()
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0] == null || destProfileList[0].Pt == null || destProfileList[0].Pt.Count == 0) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;

        try
        {
            double minX = destProfileList.Where(d => d != null && d.Pt.Count != 0).Min(d => d.Pt.Min(p => p.X));
            double maxX = destProfileList.Where(d => d != null && d.Pt.Count != 0).Max(d => d.Pt.Max(p => p.X));
            double minY = destProfileList.Where(d => d != null && d.Pt.Count != 0).Min(d => d.Pt.Min(p => p.Y));
            double maxY = destProfileList.Where(d => d != null && d.Pt.Count != 0).Max(d => d.Pt.Max(p => p.Y));

            if (Miscellaneous.IsFiniteNumber(minX, maxX, minY, maxY))
            {
                MinimalX = minX;
                MaximalX = maxX;
                MinimalY = minY;
                MaximalY = maxY;
            }
            else
            {
                MinimalX = 0;
                MaximalX = 1;
                MinimalY = 0;
                MaximalY = 1;
                return;
            }

            if (!XLog)
            {
                double space = (MaximalX - MinimalX) * 0.01;
                MinimalX -= space;
                MaximalX += space;
            }
            else
            {
                if (isIntegerX)
                {
                    double space = (MaximalX - MinimalX) * 0.01;
                    MinimalX = 0;
                    MaximalX += space;
                }
                else
                {
                    double space = (MaximalX - MinimalX) * 0.01;
                    MinimalX -= space;
                    MaximalX += space;
                }
            }

            //260607Cl 横軸下限を0に固定 (非Log かつ生データの最小Xが0以上のとき。padding 後の負側余白も消して厳密に0始点へ)
            if (FixLowerXToZero && !XLog && minX >= 0)
                MinimalX = 0;

            if (!YLog)
            {
                //260603Cl レビューメモ: 全データが負のとき MaximalY *= 1.1 は上方向でなく下方向に余白を広げてしまう(符号エッジケース)。max>=0/max<0 で場合分けが必要。
                if (MinimalY > 0)
                    MinimalY = 0;
                else
                    MinimalY *= 1.1;
                MaximalY *= 1.1;
            }
            else
            {
                if (!IsIntegerY)
                {
                    double space = (MaximalY - MinimalY) * 0.01;
                    //MinimalY = 0;//260611Cl 修正: log軸で下限を 0 (=実値1) に固定すると 1 未満のデータ(X線 Rayleigh 散乱の µ/ρ 等)が描画範囲外に隠れる → XLog と同様にデータ最小値ベースへ
                    MinimalY -= space;
                    MaximalY += space;
                }
                else
                {
                    double space = (MaximalY - MinimalY) * 0.01;
                    MinimalY = 0;//カウント系(整数)は従来どおり実値1(log=0)を下限に維持
                    MaximalY += space;
                }
            }
        }
        catch
        {
            return;
        }
    }


    /// <summary>描画範囲Upper,LowerをMaximal,Minimalに設定する</summary>
    private void resetDrawRange()
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0] == null || destProfileList[0].Pt == null) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;
        if (!FixRangeHorizontal)
        {
            LowerX = MinimalX;
            UpperX = MaximalX;
        }
        if (!FixRangeVertical)
        {
            LowerY = MinimalY;
            UpperY = MaximalY;
        }
        if (LowerX < MinimalX) LowerX = MinimalX;
        if (LowerY < MinimalY) LowerY = MinimalY;
        if (UpperX > MaximalX) UpperX = MaximalX;
        if (UpperY > MaximalY) UpperY = MaximalY;

        DrawingRangeChanged?.Invoke(new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY));
    }

    /// <summary>描画範囲を設定する (ただし、上限下限の範囲内で)</summary>
    /// <param name="rect"></param>
    public void SetDrawingRange(RectangleD rect)
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0].Pt == null) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;
        LowerX = Math.Max(rect.X, MinimalX);
        LowerY = Math.Max(rect.Y, MinimalY);
        UpperX = Math.Min(rect.Width + rect.X, MaximalX);
        UpperY = Math.Min(rect.Height + rect.Y, MaximalY);
        Draw();
    }

    /// <summary>X軸の描画範囲を設定する(Y軸はそのまま)</summary>
    /// <param name="rect"></param>
    public void SetDrawingRangeX(RectangleD rect)
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0].Pt == null) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;
        LowerX = Math.Max(rect.X, MinimalX);
        UpperX = Math.Min(rect.Width + rect.X, MaximalX);
        Draw();
    }

    /// <summary>X軸の描画範囲を設定する(Y軸は描画範囲内で拡大する)</summary>
    /// <param name="rect"></param>
    public void SetDrawingRangeXandExpandY(RectangleD rect)
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0].Pt == null) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;
        LowerX = Math.Max(rect.X, MinimalX);
        UpperX = Math.Min(rect.Width + rect.X, MaximalX);

        double max = double.MinValue, min = double.MaxValue;
        for (int i = 0; i < destProfileList.Count; i++)
            for (int j = 0; j < destProfileList[i].Pt.Count; j++)
                if (destProfileList[i].Pt[j].X >= LowerX && destProfileList[i].Pt[j].X <= UpperX)
                {
                    max = Math.Max(destProfileList[i].Pt[j].Y, max);
                    min = Math.Min(destProfileList[i].Pt[j].Y, min);
                }
        if (max != double.MinValue)
            UpperY = max + (max - min) * 0.1;// Math.Abs(max)* 0.1;
        if (min != double.MaxValue)
            LowerY = min - (max - min) * 0.1;// Math.Abs(min) * 0.1;

        Draw();
    }

    private bool updatingRangeBoxes = false;//260606Cl 追加: numericBoxへの表示更新中はValueChangedを無視するためのガード

    /// <summary>現在の描画範囲(Lower/Upper)を4つのnumericBoxへ反映する。Log軸時は実座標(10^x)で表示する。 (260606Cl 追加)</summary>
    private void updateRangeBoxes()
    {
        if (!flowLayoutPanelRange.Visible) return;//パネル非表示時は更新不要
        updatingRangeBoxes = true;
        numericBoxXMin.Value = XLog ? Math.Pow(10, LowerX) : LowerX;
        numericBoxXMax.Value = XLog ? Math.Pow(10, UpperX) : UpperX;
        numericBoxYMin.Value = YLog ? Math.Pow(10, LowerY) : LowerY;
        numericBoxYMax.Value = YLog ? Math.Pow(10, UpperY) : UpperY;
        updatingRangeBoxes = false;
    }

    /// <summary>4つのnumericBoxの入力値を描画範囲に取り込んで再描画する。Log軸時は実座標入力をlog10へ変換する。 (260606Cl 追加)</summary>
    private void numericBoxRange_ValueChanged(object sender, EventArgs e)
    {
        if (updatingRangeBoxes) return;//表示更新由来の変更は無視 (無限ループ防止)

        //実座標 → 描画座標系(Log軸ならlog10)へ変換。0以下でLogが取れない場合はfallback(現値)を維持
        static double toAxis(double v, bool log, double fallback) => log ? (v > 0 ? Math.Log10(v) : fallback) : v;
        double xmin = Math.Max(toAxis(numericBoxXMin.Value, XLog, LowerX), MinimalX);
        double xmax = Math.Min(toAxis(numericBoxXMax.Value, XLog, UpperX), MaximalX);
        double ymin = Math.Max(toAxis(numericBoxYMin.Value, YLog, LowerY), MinimalY);
        double ymax = Math.Min(toAxis(numericBoxYMax.Value, YLog, UpperY), MaximalY);

        if (xmin < xmax) { LowerX = xmin; UpperX = xmax; }
        if (ymin < ymax) { LowerY = ymin; UpperY = ymax; }

        Draw();//Draw内のupdateRangeBoxesでクランプ後の値がnumericBoxへ反映される
        DrawingRangeChanged?.Invoke(new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY));
    }
    #endregion

    #region Draw
    public void Draw(bool initialize = false)
    {
        if (initialize)
        {
            InitializeAxis();
            setDrawRangeLimit();
            resetDrawRange();
        }
        recalcDrawOrigin();//260607Cl 追加: 軸ラベル余白を反映した実効原点を再計算 (マウス座標変換でも使うので早期return前に)
        updateRangeBoxes();//260606Cl 追加: 現在の描画範囲を4つのnumericBoxへ反映 (パネル非表示時は即return)
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0].Pt == null || destProfileList[0].Pt.Count == 0) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;

        try
        {
            //260603Cl レビューメモ: GDIリーク。旧 Bmp / G / pictureBox.Image を Dispose していない。高頻度再描画(縦線ドラッグ等)でハンドル/メモリが積み上がる。using化 or フィールド再利用+Dispose を検討。DrawDivision/DrawProfileLine 等の new Pen/SolidBrush/Font も都度生成・未Disposeで同様。
            Bmp = new Bitmap(PictureBoxSize.Width, PictureBoxSize.Height);
            G = Graphics.FromImage(Bmp);
            this.DoubleBuffered = true;
            renderGraph(G);//260607Cl: 描画本体を共通メソッド化 (Copy ボタンの metafile 出力と共用)
            pictureBox.Image = Bmp;
        }
        catch { }//260603Cl レビューメモ: 全例外を握り潰しているため描画不具合が無言で「真っ白」になり追跡困難。最低限 Debug.WriteLine を出すべき。
    }

    /// <summary>グラフ本体を描画する共通シーケンス。画面表示用 Bitmap と Copy 用 Metafile の双方から呼ぶ。</summary>
    //260607Cl 追加: 旧 Draw() 内に直書きされていた描画シーケンスを切り出し。Draw* ヘルパは field G を参照するため、呼び出し側で G を退避・復帰すること (buttonCopy_Click 参照)。
    private void renderGraph(Graphics g)
    {
        G = g;
        G.Clear(BackgroundColor);
        G.SmoothingMode = SmoothingMode.AntiAlias;

        DrawDivision();
        DrawAxisLabels();//260607Cl 追加: 軸の説明ラベル(下=X, 左=Y回転)を描画
        if (mode == DrawingMode.Histogram)
            DrawProfileHistogram();
        else if (mode == DrawingMode.Line)
            DrawProfileLine();
        else if (mode == DrawingMode.Point)
            DrawProfilePoint();

        if (verticalLineList.Count != 0)
            DrawLine();

        if (peaks.Count != 0)
            DrawPeaks();

        if (annotationList.Count != 0)
            DrawAnnotations();//260607Cl 追加: 任意位置テキスト注釈 (回折ピークの hkl 等)
    }
    #endregion

    #region DrawLine, DrawPeaks
    /// <summary>グラフ中にVerticalLineListで定義された垂直線を描く</summary>
    private void DrawLine()
    {
        //260603Cl: 値表示の対象とする「活線」 (選択中の線。無ければ線が1本だけのときそれ)
        int activeIndex = selectedVerticalLineIndex >= 0 && selectedVerticalLineIndex < verticalLineList.Count
            ? selectedVerticalLineIndex : (verticalLineList.Count == 1 ? 0 : -1);

        for (int i = 0; i < verticalLineList.Count; i++)
        {
            double x = verticalLineList[i].X;
            if (xLog)
            {
                if (x > 0)
                    x = Math.Log10(x);
                else
                    x = 0;
            }
            var ptStart = ConvToPicBoxCoord(x, verticalLineList[i].Y);
            if (double.IsNaN(verticalLineList[i].Y))
                ptStart.Y = 0;
            var ptEnd = new PointF((float)ptStart.X, (float)(pictureBox.Height - originPosition.Y));
            if (!double.IsNaN(ptStart.X) && !double.IsInfinity(ptStart.X))
                G.DrawLine(new Pen(VerticalLineColor, selectedVerticalLineIndex == i ? 2f : 1f), ptStart, ptEnd);

            //260603Cl 追加: 垂直線と各プロファイルの交点に丸マーカーを描画する (値はグラフに重ねず上部ラベルへ)
            if (verticalLineMarkerVisible && !double.IsNaN(x) && !double.IsInfinity(x) && x >= LowerX && x <= UpperX)
                DrawVerticalLineMarkers(x);
        }

        //260603Cl 追加: 活線とプロファイルの交点座標を上部パネルのラベルに表示する
        if (verticalLineMarkerVisible)
            UpdateMarkerReadout(activeIndex);
    }

    /// <summary>垂直線と各プロファイルの交点に丸マーカーを描画する (260603Cl 追加)</summary>
    /// <param name="transformedX">対数変換済みのX座標 (描画座標系。プロファイル点・ConvToPicBoxCoordと同じ系)</param>
    private void DrawVerticalLineMarkers(double transformedX)
    {
        for (int j = 0; j < destProfileList.Count && j < srcProfileList.Count; j++)
        {
            var dp = destProfileList[j];
            if (dp == null || dp.Pt == null || dp.Pt.Count < 2) continue;
            if (transformedX < dp.Pt[0].X || transformedX > dp.Pt[^1].X) continue;//範囲外は外挿しない

            double transformedY = dp.GetValue(transformedX, 2, 1);//隣接2点で線形補間 (描画される折れ線と一致)
            if (double.IsNaN(transformedY) || double.IsInfinity(transformedY) || transformedY < LowerY || transformedY > UpperY) continue;

            var color = srcProfileList[j] != null ? srcProfileList[j].Color : VerticalLineColor;
            var p = ConvToPicBoxCoord(transformedX, transformedY);
            float r = VerticalLineMarkerRadius;
            //260604Cl 再描画ごとの GDI ハンドル蓄積を避ける: 白縁は共有 Pens.White、塗りは using で確実に解放
            using (var brush = new SolidBrush(color))
                G.FillEllipse(brush, p.X - r, p.Y - r, r * 2, r * 2);
            G.DrawEllipse(Pens.White, p.X - r, p.Y - r, r * 2, r * 2);
        }
    }

    /// <summary>活線(activeIndex)と先頭の有効プロファイルの交点座標を上部パネルの labelXValue / labelYValue に表示する (260603Cl 追加)</summary>
    private void UpdateMarkerReadout(int activeIndex)
    {
        if (!panelTitleAndMouse.Visible) return;//260607Cl 変更: UpperPanelVisible 廃止 (旧: if (!UpperPanelVisible) return;)
        if (activeIndex < 0 || activeIndex >= verticalLineList.Count)
        {
            labelXValue.Text = labelYValue.Text = "-";
            return;
        }
        double realX = verticalLineList[activeIndex].X;
        double tx = xLog ? (realX > 0 ? Math.Log10(realX) : double.NaN) : realX;

        // 260606Cl: 全プロファイルの交点 Y 値をカンマ区切りで表示 (旧: 先頭の有効プロファイル1本のみ)
        //var yFormat = (yLog ? "E" : "g") + (MousePositionYDigit == -1 ? "" : MousePositionYDigit.ToString());
        var yFormat = resolveYFormat(yLog); // 260608Cl 変更: FormatSpecifier 優先
        var yValues = new List<string>();
        for (int j = 0; j < destProfileList.Count && j < srcProfileList.Count; j++)
        {
            var dp = destProfileList[j];
            if (dp == null || dp.Pt == null || dp.Pt.Count < 2) continue;
            if (double.IsNaN(tx) || tx < dp.Pt[0].X || tx > dp.Pt[^1].X) continue;
            double ty = dp.GetValue(tx, 2, 1);
            if (double.IsNaN(ty) || double.IsInfinity(ty)) continue;
            double realY = yLog ? Math.Pow(10, ty) : ty;//実座標に戻す
            yValues.Add(realY.ToString(yFormat));
        }
        if (yValues.Count == 0)
        {
            labelXValue.Text = labelYValue.Text = "-";
            return;
        }
        //labelXValue.Text = realX.ToString((xLog ? "E" : "g") + (MousePositionXDigit == -1 ? "" : MousePositionXDigit.ToString())) + UnitX;//260607Cl: 数値の後ろに単位(UnitX)を連結
        labelXValue.Text = realX.ToString(resolveXFormat(xLog)) + UnitX; // 260608Cl 変更: FormatSpecifier 優先 //260607Cl: 数値の後ろに単位(UnitX)を連結
        //labelYValue.Text = string.Join(", ", yValues) + UnitY;//260611Ch 旧: 長い値列の折り返し幅が未設定だった
        labelYValue.MaximumSize = new Size(Math.Max(60, flowLayoutPanelMousePosition.ClientSize.Width - labelYValue.Left - 6), 0);//260611Ch 整理: 旧 setLabelYValueText をインライン化
        labelYValue.Text = string.Join(", ", yValues) + UnitY;//260607Cl: 末尾に単位(UnitY)を連結
    }

    /// <summary>グラフ中にpeaksで定義された釣鐘型曲線を描く</summary>
    private void DrawPeaks()
    {
        for (int i = 0; i < peaks.Count; i++)
        {
            double step = peaks[i].range / 100.0;
            for (double x = peaks[i].X - peaks[i].range; x < peaks[i].X + peaks[i].range; x += step)
            {
                G.DrawLine(new Pen(VerticalLineColor, 2f), ConvToPicBoxCoord(x, peaks[i].GetValue(x, x == peaks[i].X - peaks[i].range, true)), ConvToPicBoxCoord(x + step, peaks[i].GetValue(x + step, false, true)));
            }
        }
    }
    #endregion

    #region DrawProfile
    private void DrawProfilePoint()
    {
        //総和が少ないプロファイルを前面に(最後に)書く
        float maxX = PictureBoxSize.Width, minX = originPosition.X, maxY = PictureBoxSize.Height - originPosition.Y, minY = 0;
        for (int j = 0; j < destProfileList.Count; j++)
        {
            PointD[] pt = [.. destProfileList[j].Pt];
            if (pt.Length > 0)
            {
                Brush brush = new SolidBrush(srcProfileList[j].Color);
                for (int i = 0; i < pt.Length; i++)
                {
                    PointF p = ConvToPicBoxCoord(pt[i]);
                    if (p.X > minX - 0.001 && p.X < maxX + 0.001 && p.Y > minY - 0.001 && p.Y < maxY + 0.001)//範囲内であれば
                        G.FillEllipse(brush, p.X - 1f, p.Y - 1f, 2f, 2f);
                }
            }
        }
    }

    private void DrawProfileHistogram()
    {
        //総和が少ないプロファイルを前面に(最後に)書く
        var sort = new List<PointD>();
        for (int j = 0; j < destProfileList.Count; j++)
        {
            var pt = destProfileList[j].Pt.ToArray();
            double sum = 0;
            for (int i = 0; i < pt.Length; i++)
                sum += pt[i].Y;
            sort.Add(new PointD(sum, j));
        }
        sort.Sort();
        sort.Reverse();

        float maxX = PictureBoxSize.Width, minX = originPosition.X, maxY = PictureBoxSize.Height - originPosition.Y, minY = 0;
        float zeroY = ConvToPicBoxCoord(0, 0).Y;
        for (int j = 0; j < sort.Count; j++)
        {
            var pt = destProfileList[(int)sort[j].Y].Pt.ToArray();
            if (pt.Length > 0)
            {
                var brush = new SolidBrush(srcProfileList[(int)sort[j].Y].Color);
                //var pointList = new List<List<PointF>>();
                var beforePt = ConvToPicBoxCoord(pt[0].X, pt[0].Y);
                var presentPt = ConvToPicBoxCoord(pt[0].X, pt[0].Y);
                var nextPt = ConvToPicBoxCoord(pt[0].X, pt[0].Y);
                for (int i = 0; i < pt.Length; i++)
                {
                    if (i < pt.Length - 1)
                        nextPt = ConvToPicBoxCoord(pt[i + 1].X, pt[i + 1].Y);
                    if (presentPt.X > minY - 0.001 && presentPt.X < maxX + 0.001)
                    {
                        float x = Math.Max((beforePt.X + presentPt.X) / 2f - 0.5f, minX);
                        float y = Math.Max(presentPt.Y, minY);
                        float width = Math.Min((nextPt.X - beforePt.X) / 2f + 1f, (maxX - beforePt.X) / 2f + 1f);
                        float height = Math.Min(zeroY - y, maxY - y);
                        G.FillRectangle(brush, x, y, width, height);
                    }
                    beforePt = presentPt;
                    presentPt = nextPt;
                }
            }
        }
    }

    /// <summary>線形式のプロファイルを描く  //一旦全部書いた後、要らない部分を切った方がいいのでは？</summary>
    private void DrawProfileLine()
    {
        var rect = new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY);
        if (rect.Width * rect.Height == 0)
            return;
        for (int j = 0; j < destProfileList.Count; j++)
        {
            if (srcProfileList[j] != null)
            {
                //if (!Interpolation)//補間モードではないとき
                //{
                var pen = new Pen(srcProfileList[j].Color, UseLineWidth ? LineWidth : srcProfileList[j].LineWidth) { LineJoin = LineJoin.Round };
                var pts = destProfileList[j].GetPointsWithinRectangle(rect);
                for (int i = 0; i < pts.Length; i++)
                    if (pts[i].Length > 1)
                        G.DrawLines(pen, pts[i].Select(p => ConvToPicBoxCoord(p)).ToArray());
                //}
                //else//補間モードの時
                //{
                //}
            }
        }
    }
    #endregion

    #region 目盛線
    /// <summary>上下限値(max, min)と最大目盛り数(maxDiv)から、目盛りの位置とラベルを生成する</summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="maxDiv"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    public static SortedList<double, string> GetDivisions(double min, double max, int maxDiv, bool log)
    {
        if (double.IsInfinity(min) || double.IsInfinity(max)) return [];
        var results = new SortedList<double, string>();
        double d = max - min;
        string str;

        if (!log)
        {
            double step = 1;
            double unit = Math.Pow(10, Math.Floor(Math.Log10(d / maxDiv)));
            if (d / unit < maxDiv) step = unit;
            else if (d / unit / 2 < maxDiv) step = unit * 2;
            else if (d / unit / 5 < maxDiv) step = unit * 5;
            else if (d / unit / 10 < maxDiv) step = unit * 10;

            var startI = min > 0 ? (int)(min / step) + 1 : (int)(min / step);
            for (int i = startI; i < max / step; i++)
            {
                if (min >= 0 && (max > 1000 || max < 0.001))//対数表示する場合
                    str = ((i * step) / Math.Pow(10, (int)Math.Log10(i * step))).ToString("#,#.###############") + "E" + ((int)Math.Log10(i * step)).ToString();
                else//実数表示する場合
                    str = i * step == 0 ? "0" : Math.Round(i * step, 5).ToString("#,#.###############");
                results.Add(i * step, str);
            }
        }
        else
        {
            //先ずは無条件で有効数字0桁の目盛りを設定
            for (int i = (int)min; i < max; i++)
                if (i > min)
                    results.Add(i, "1E" + i.ToString());//260603Cl レビューメモ: キー重複で ArgumentException → Draw の catch{} で握り潰され「目盛りが出ない」症状になりうる。TryAdd 化を検討(本メソッド内の他の Add 箇所も同様)。

            //有効数字1桁の目盛り(2E1,3E1,4E1など)を設定
            for (int i = (int)Math.Floor(min); i < max + 1; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    double a = Math.Log10(j) + i;
                    if (a >= min && a <= max)
                        results.Add(a, maxDiv / d < 8 ? "" : j.ToString() + "E" + i.ToString());
                }
            }

            if (d < 0.5)
            {
                double max2 = Math.Pow(10, max), min2 = Math.Pow(10, min);
                double step = 1;
                double unit = Math.Pow(10, Math.Floor(Math.Log10((max2 - min2) / maxDiv)));
                if ((max2 - min2) / unit < maxDiv) step = unit;
                else if ((max2 - min2) / unit / 2 < maxDiv) step = unit * 2;
                else if ((max2 - min2) / unit / 5 < maxDiv) step = unit * 5;
                else if ((max2 - min2) / unit / 10 < maxDiv) step = unit * 10;

                for (int i = (int)(min2 / step) + 1; i < max2 / step; i++)
                    //260317Cl 変更: ContainsKey+Add → TryAdd
                    results.TryAdd(Math.Log10(i * step), ((i * step) / Math.Pow(10, (int)Math.Floor(Math.Log10(i * step)))).ToString("#,#.###############") + "E" + ((int)Math.Floor(Math.Log10(i * step))).ToString());
            }
        }
        return results;
    }

    public void DrawDivision()
    {
        var strFont = AxisTextFont;
        var brush = new SolidBrush(AxisTextColor);
        var pen = new Pen(AxisLineColor, 1);

        //ここよりX目盛りの描画
        G.DrawLine(pen, originPosition.X, pictureBox.Height - originPosition.Y, PictureBoxSize.Width, pictureBox.Height - originPosition.Y);
        int maxDivisionNumber = (this.Width - this.originPosition.X) / 60 + 1;
        var divisions = GetDivisions(LowerX, UpperX, maxDivisionNumber, XLog);
        for (int i = 0; i < divisions.Count; i++)
        {
            pen = new Pen(AxisLineColor, 1);
            G.DrawLine(pen, ConvToPicBoxCoord(divisions.Keys[i], 0).X, PictureBoxSize.Height - originPosition.Y, ConvToPicBoxCoord(divisions.Keys[i], 0).X, PictureBoxSize.Height - originPosition.Y + 5);
            if (horizontalGradiationTextVisivle)
                G.DrawString(divisions.Values[i], strFont, brush, ConvToPicBoxCoord(divisions.Keys[i], 0).X - 2, PictureBoxSize.Height - originPosition.Y + 5);

            pen = new Pen(DivisionLineColor, 1);
            if (DivisionLineXVisible)
                G.DrawLine(pen, ConvToPicBoxCoord(divisions.Keys[i], 0).X, PictureBoxSize.Height - originPosition.Y, ConvToPicBoxCoord(divisions.Keys[i], 0).X, 0);
        }

        //ここよりY目盛りの描画
        G.DrawLine(pen, originPosition.X, 0, originPosition.X, pictureBox.Height - originPosition.Y);
        maxDivisionNumber = (this.Height - this.originPosition.Y) / 30 + 1;
        divisions = GetDivisions(LowerY, UpperY, maxDivisionNumber, YLog);

        for (int i = 0; i < divisions.Count; i++)
        {
            pen = new Pen(AxisLineColor, 1);
            G.DrawLine(pen, originPosition.X - 8, ConvToPicBoxCoord(0, divisions.Keys[i]).Y, originPosition.X, ConvToPicBoxCoord(0, divisions.Keys[i]).Y);

            //if (horizontalGradiationTextVisivle)//260603Cl 修正: Y軸ラベルの表示可否は vertical 側フラグで判定すべき (AxisYTextVisible が効くように)
            if (verticalGradiationTextVisivle)//260603Cl
                G.DrawString(divisions.Values[i], strFont, brush, originPosition.X - userOrigin.X, ConvToPicBoxCoord(0, divisions.Keys[i]).Y - 6);//260607Cl: Y軸ラベル余白(originPosition.X - userOrigin.X)の分だけ目盛り数字を右へずらし、回転ラベルとの重なりを防ぐ

            pen = new Pen(DivisionLineColor, 1);
            if (DivisionLineYVisible)
                G.DrawLine(pen, originPosition.X - 8, ConvToPicBoxCoord(0, divisions.Keys[i]).Y, PictureBoxSize.Width, ConvToPicBoxCoord(0, divisions.Keys[i]).Y);
        }
    }
    #endregion

    #region 軸ラベル描画 (260607Cl 追加)

    /// <summary>軸ラベルの有無に応じて実効原点(originPosition)を userOrigin + ラベル余白 で再計算する。 (260607Cl 追加)</summary>
    private void recalcDrawOrigin()
    {
        //ラベルが空ならその方向の余白は0。空でなければ文字高さ+2pxを余白に充てる(Y軸ラベルは回転するので占有幅=文字高さ)
        int mLeft = string.IsNullOrEmpty(axisLabelY) ? 0 : TextRenderer.MeasureText(axisLabelY, AxisTextFont).Height + 2;
        int mBottom = string.IsNullOrEmpty(axisLabelX) ? 0 : TextRenderer.MeasureText(axisLabelX, AxisTextFont).Height + 2;
        originPosition = new Point(userOrigin.X + mLeft, userOrigin.Y + mBottom);
    }

    /// <summary>X軸(下・中央)とY軸(左・中央・反時計90度)の説明ラベルを描く。 (260607Cl 追加)</summary>
    private void DrawAxisLabels()
    {
        if (string.IsNullOrEmpty(axisLabelX) && string.IsNullOrEmpty(axisLabelY)) return;
        using var brush = new SolidBrush(AxisTextColor);
        using var fmt = new StringFormat();//260607Cl: 初期化子でなく明示代入 (using中の初期化例外でリークさせない)
        fmt.Alignment = fmt.LineAlignment = StringAlignment.Center;

        if (!string.IsNullOrEmpty(axisLabelX))//X軸ラベル: グラフ領域の横中央・最下部
        {
            float h = TextRenderer.MeasureText(axisLabelX, AxisTextFont).Height;
            G.DrawString(axisLabelX, AxisTextFont, brush, originPosition.X + (PictureBoxSize.Width - originPosition.X) / 2f, PictureBoxSize.Height - h / 2f, fmt);
        }
        if (!string.IsNullOrEmpty(axisLabelY))//Y軸ラベル: 左端・グラフ領域の縦中央・反時計90度
        {
            float h = TextRenderer.MeasureText(axisLabelY, AxisTextFont).Height;
            var s = G.Save();
            G.TranslateTransform(h / 2f, (PictureBoxSize.Height - originPosition.Y) / 2f);
            G.RotateTransform(-90);//反時計回り90度
            G.DrawString(axisLabelY, AxisTextFont, brush, 0, 0, fmt);
            G.Restore(s);
        }
    }

    /// <summary>Annotations を描く: 任意 (X,Y) 位置のテキスト (+任意でガイド縦線)。Y=NaN はプロット上端付近。 (260607Cl 追加)</summary>
    private void DrawAnnotations()
    {
        using var fmt = new StringFormat();
        float top = 2f;                                          // プロット領域の上端 (y ピクセル)
        float bottom = PictureBoxSize.Height - originPosition.Y; // プロット領域の下端 (X 軸位置)
        foreach (var a in annotationList)
        {
            double x = xLog ? (a.X > 0 ? Math.Log10(a.X) : double.NaN) : a.X;
            if (double.IsNaN(x) || double.IsInfinity(x) || x < LowerX || x > UpperX) continue;//範囲外はスキップ
            float px = ConvToPicBoxCoord(x, 0).X;

            if (a.GuideLine)//薄い点線の縦ガイド
                using (var pen = new Pen(Color.FromArgb(90, a.Color), 1f) { DashStyle = DashStyle.Dot })
                    G.DrawLine(pen, px, top, px, bottom);

            if (string.IsNullOrEmpty(a.Text)) continue;
            using var brush = new SolidBrush(a.Color);

            if (double.IsNaN(a.Y))//上端付近に配置 (回折ピークの hkl ラベル)
            {
                if (a.Vertical)//反時計 90 度: 文字列の末尾が上端に来るよう Far 寄せ
                {
                    var s = G.Save();
                    G.TranslateTransform(px, top);
                    G.RotateTransform(-90);
                    fmt.Alignment = StringAlignment.Far;
                    fmt.LineAlignment = StringAlignment.Center;
                    G.DrawString(a.Text, AxisTextFont, brush, 0, 0, fmt);
                    G.Restore(s);
                }
                else
                {
                    fmt.Alignment = StringAlignment.Center;
                    fmt.LineAlignment = StringAlignment.Near;
                    G.DrawString(a.Text, AxisTextFont, brush, px, top, fmt);
                }
            }
            else//(X,Y) データ座標に配置
            {
                double y = yLog ? (a.Y > 0 ? Math.Log10(a.Y) : double.NaN) : a.Y;
                if (double.IsNaN(y) || y < LowerY || y > UpperY) continue;
                var p = ConvToPicBoxCoord(x, y);
                fmt.LineAlignment = StringAlignment.Center;
                if (a.Vertical)
                {
                    var s = G.Save();
                    G.TranslateTransform(p.X, p.Y);
                    G.RotateTransform(-90);
                    fmt.Alignment = StringAlignment.Center;
                    G.DrawString(a.Text, AxisTextFont, brush, 0, 0, fmt);
                    G.Restore(s);
                }
                else
                {
                    fmt.Alignment = StringAlignment.Center;
                    G.DrawString(a.Text, AxisTextFont, brush, p.X, p.Y, fmt);
                }
            }
        }
    }

    #endregion

    #region 座標変換関係

    private PointF ConvToPicBoxCoord(double x, double y)
    {//プロファイル座標をピクチャーボックスの座標系に変換
        return new PointF((float)((PictureBoxSize.Width - originPosition.X) / (UpperX - LowerX) * (x - LowerX)) + originPosition.X,
        (float)(PictureBoxSize.Height - originPosition.Y - (PictureBoxSize.Height - originPosition.Y) / (UpperY - LowerY) * (y - LowerY)));//260607Cl: BottomMargin(常時0・廃止)を式から除去
    }

    private PointF ConvToPicBoxCoord(PointD p)
    {//ピクチャーボックスの座標系に変換
        return new PointF((float)((PictureBoxSize.Width - originPosition.X) / (UpperX - LowerX) * (p.X - LowerX)) + originPosition.X,
            (float)(PictureBoxSize.Height - originPosition.Y - (PictureBoxSize.Height - originPosition.Y) / (UpperY - LowerY) * (p.Y - LowerY)));//260607Cl: BottomMargin除去
    }

    private PointD ConvToRealCoord(int x, int y)
    {//マウス座標をオリジナルの座標系に変換
        return new PointD(
            (double)(x - originPosition.X) / (PictureBoxSize.Width - originPosition.X) * (UpperX - LowerX) + LowerX,
            (double)(PictureBoxSize.Height - y - originPosition.Y) / (PictureBoxSize.Height - originPosition.Y) * (UpperY - LowerY) + LowerY);//260607Cl: BottomMargin除去
    }

    #endregion 座標変換関係

    #region マウス操作関連

    public Point MouseRangeStart, MouseRangeEnd;
    public PointD MouseMovingStartPt;
    public bool MouseRangingMode = false;
    public bool MouseMovingMode = false;
    public bool LineSelectMode = false;//260603Cl レビューメモ: これら内部モード状態が public フィールドで外部から書き換え可能(カプセル化が緩い)。get-only かメソッド経由が望ましい。

    #region コンテキストメニュー

    private void toolStripMenuItemLogScaleY_Click(object sender, EventArgs e) => YLog = !YLog;

    private void toolStripMenuItemLogScaleX_Click(object sender, EventArgs e) => XLog = !XLog;

    private void toolStripMenuItemScaleLineX_Click(object sender, EventArgs e) => DivisionLineXVisible = !DivisionLineXVisible;

    private void toolStripMenuItemScaleLineY_Click(object sender, EventArgs e) => DivisionLineYVisible = !DivisionLineYVisible;

    #endregion コンテキストメニュー

    private void pictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (!AllowMouseOperation) return;

        PointD pt = ConvToRealCoord(e.X, e.Y);
        if (e.Button == MouseButtons.Middle)
        {
            MouseMovingMode = true;
            MouseMovingStartPt = pt;
            pictureBox.Cursor = Cursors.SizeAll;
            return;
        }

        if (e.Button == MouseButtons.Right && e.X - originPosition.X >= 0 && PictureBoxSize.Height - e.Y - originPosition.Y >= 0)
        {
            MouseRangingMode = true;
            MouseRangeStart = new Point(e.X, e.Y);
            MouseRangeEnd = new Point(e.X, e.Y);
            return;
        }
        if (e.Button == MouseButtons.Left)
        {
            if (e.Clicks == 1)
            {
                int i = serchLineIndex(ConvToRealCoord(e.X + 3, e.Y).X, ConvToRealCoord(e.X - 3, e.Y).X);
                if (i >= 0)
                {
                    LineSelectMode = true;
                    selectedVerticalLineIndex = i;
                    Draw();
                }
            }
            else if (e.Clicks == 2)
            {
                if (MouseDoubleClick2 != null)
                    if (MouseDoubleClick2(pt))
                        return;

                if (Profile != null && Profile.Pt != null && Profile.Pt.Count != 0)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < Profile.Pt.Count; i++)
                        sb.AppendLine(Profile.Pt[i].X.ToString() + "\t" + Profile.Pt[i].Y.ToString());
                    Clipboard.SetDataObject(sb.ToString());
                }
            }
        }
    }
    /// <summary>近いラインを探す</summary>
    /// <param name="upperX"></param>
    /// <param name="lowerX"></param>
    /// <returns></returns>
    //260603Cl レビューメモ: メソッド名タイポ serchLineIndex→searchLineIndex。他にも Gradiation→Graduation, Visivle→Visible 等のタイポが点在。log軸時の lowerX<0 分岐(-Math.Pow(10,lowerX))も挙動が怪しく要確認。
    private int serchLineIndex(double upperX, double lowerX)
    {
        double dev = double.PositiveInfinity;
        int index = -1;
        if (xLog)
        {
            upperX = Math.Pow(10, upperX);

            if (lowerX > 0) lowerX = Math.Pow(10, lowerX);
            else lowerX = -Math.Pow(10, lowerX);
        }
        for (int i = 0; i < verticalLineList.Count; i++)
        {
            if (upperX > verticalLineList[i].X && lowerX < verticalLineList[i].X)
                if (dev > (upperX + lowerX) / 2 - verticalLineList[i].X)
                {
                    dev = (upperX + lowerX) / 2 - verticalLineList[i].X;
                    index = i;
                }
        }
        return index;
    }

    private void pictureBox_MouseMove(object sender, MouseEventArgs e)
    {
        if (!AllowMouseOperation) return;

        //マウスが動いたとき
        PointD pt = ConvToRealCoord(e.X, e.Y);

        if (panelTitleAndMouse.Visible && !verticalLineMarkerVisible)//260603Cl: マーカー表示中は上部ラベルを交点座標が使うのでマウス座標で上書きしない //260607Cl 変更: UpperPanelVisible 廃止→panelTitleAndMouse.Visible 直接参照
        {
            double x = pt.X;
            x = XLog ? Math.Pow(10, x) : x;
            x = IsIntegerX ? (int)(Math.Round(x)) : x;

            double y = pt.Y;
            //y = XLog ? Math.Pow(10, y) : y;//260603Cl 修正: Y値はY軸の対数フラグで変換すべき (XLog→YLog)
            y = YLog ? Math.Pow(10, y) : y;//260603Cl
            y = IsIntegerY ? (int)(Math.Round(y)) : y;

            //labelXValue.Text = x.ToString((XLog ? "E" : "g") + (MousePositionXDigit == -1 ? "" : MousePositionXDigit.ToString())) + UnitX;//260607Cl: 数値の後ろに単位(UnitX)を連結
            labelXValue.Text = x.ToString(resolveXFormat(XLog)) + UnitX; // 260608Cl 変更: FormatSpecifier 優先 //260607Cl: 数値の後ろに単位(UnitX)を連結
            //labelYValue.Text = y.ToString((YLog ? "E" : "g") + (MousePositionYDigit == -1 ? "" : MousePositionXDigit.ToString()));//260603Cl 修正: Y桁は MousePositionYDigit を使うべき
            //labelYValue.Text = y.ToString(resolveYFormat(YLog)) + UnitY; // 260611Ch 旧: 折り返し幅が未設定だった
            labelYValue.MaximumSize = new Size(Math.Max(60, flowLayoutPanelMousePosition.ClientSize.Width - labelYValue.Left - 6), 0);//260611Ch 整理: 旧 setLabelYValueText をインライン化
            labelYValue.Text = y.ToString(resolveYFormat(YLog)) + UnitY; // 260608Cl 変更: FormatSpecifier 優先 //260603Cl //260607Cl: 数値の後ろに単位(UnitY)を連結
        }

        if (MouseMovingMode)
        {
            pictureBox.Cursor = Cursors.SizeAll;
            //MouseRangingStartが現在の中心位置に来るように設定を治す
            double devX = -pt.X + MouseMovingStartPt.X;
            double devY = -pt.Y + MouseMovingStartPt.Y;

            if (devX + UpperX < MaximalX && devX + LowerX > MinimalX)
            {
                UpperX += devX;
                LowerX += devX;
            }
            if (devY + UpperY < MaximalY && devY + LowerY > MinimalY)
            {
                UpperY += devY;
                LowerY += devY;
            }
            pt = ConvToRealCoord(e.X, e.Y);
            MouseMovingStartPt = pt;
            Draw();
            return;
        }
        else
            pictureBox.Cursor = Cursors.Default;

        if (MouseRangingMode)
        {//範囲選択モードのとき
            MouseRangeEnd = new Point(e.X, e.Y);
            pictureBox.Refresh();
            return;
        }

        if (LineSelectMode && e.Button == MouseButtons.Left)
        {
            if (e.X > 3 && e.Y > 3 && e.X < PictureBoxSize.Width - 3 && e.Y < PictureBoxSize.Height - 3)
            {
                if (verticalLineList.Count > selectedVerticalLineIndex && selectedVerticalLineIndex >= 0)
                {
                    var x = XLog ? Math.Pow(10, pt.X) : pt.X;
                    verticalLineList[selectedVerticalLineIndex] = new PointD(x, verticalLineList[selectedVerticalLineIndex].Y);

                    Draw();
                    LinePositionChanged?.Invoke();
                    Refresh();
                }
            }
        }
    }

    private void pictureBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (!AllowMouseOperation) return;

        MouseMovingMode = false;
        LineSelectMode = false;

        var pt = ConvToRealCoord(e.X, e.Y);

        if (MouseRangingMode)
        {
            MouseRangingMode = false;
            MouseRangeEnd = new Point(e.X, e.Y);

            if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) < 3 || Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) < 3)
            {//選択範囲があまりにも小さすぎたら
                //縮小
                LowerX = LowerX * 1.5 - UpperX * 0.5;
                if (LowerX < MinimalX) LowerX = MinimalX;

                UpperX = UpperX * 1.5 - LowerX * 0.5;
                if (UpperX > MaximalX) UpperX = MaximalX;

                LowerY = LowerY * 1.5 - UpperY * 0.5;
                if (LowerY < MinimalY) LowerY = MinimalY;

                UpperY = UpperY * 1.5 - LowerY * 0.5;
                if (UpperY > MaximalY) UpperY = MaximalY;

                Draw();
                DrawingRangeChanged?.Invoke(new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY));
            }
            else if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) < 10 || Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) < 10)
            {//選択範囲が中途半端に小さすぎたら
                MouseRangingMode = false;
                pictureBox.Refresh();
            }
            else
            {
                double xmax = ConvToRealCoord(Math.Max(MouseRangeStart.X, MouseRangeEnd.X), 1).X;
                double xmin = ConvToRealCoord(Math.Min(MouseRangeStart.X, MouseRangeEnd.X), 1).X;
                double ymin = ConvToRealCoord(1, Math.Max(MouseRangeStart.Y, MouseRangeEnd.Y)).Y;
                double ymax = ConvToRealCoord(1, Math.Min(MouseRangeStart.Y, MouseRangeEnd.Y)).Y;
                if (xmax - xmin < 0.000001 || ymax - ymin < 0.00001) return;
                if (xmax > UpperX) xmax = UpperX;
                if (xmin < LowerX) xmin = LowerX;
                if (ymax > UpperY) ymax = UpperY;
                if (ymin < LowerY) ymin = LowerY;

                LowerX = xmin; UpperX = xmax; LowerY = ymin; UpperY = ymax;
                Draw();
                DrawingRangeChanged?.Invoke(new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY));
            }
        }
        else if (e.Button == MouseButtons.Right && (e.X - originPosition.X) * (PictureBoxSize.Height - e.Y - originPosition.Y) < 0)
        {
            toolStripMenuItemLogScaleX.Visible = e.X > originPosition.X && PictureBoxSize.Height - e.Y < originPosition.Y;
            toolStripMenuItemScaleLineX.Visible = e.X > originPosition.X && PictureBoxSize.Height - e.Y < originPosition.Y;
            toolStripMenuItemLogScaleY.Visible = e.X < originPosition.X && PictureBoxSize.Height - e.Y > originPosition.Y;
            toolStripMenuItemScaleLineY.Visible = e.X < originPosition.X && PictureBoxSize.Height - e.Y > originPosition.Y;
            MouseRangingMode = false;
            contextMenuStripY.Show(this.pictureBox, e.X, e.Y);
        }
    }

    #endregion マウス操作関連

    #region その他イベント
    private void pictureBox_Paint(object sender, PaintEventArgs e)
    {
        if (MouseRangingMode)
        {
            Pen pen = new(Brushes.Gray) { DashStyle = DashStyle.Dash };
            e.Graphics.DrawRectangle(pen, Math.Min(MouseRangeStart.X, MouseRangeEnd.X), Math.Min(MouseRangeStart.Y, MouseRangeEnd.Y),
                Math.Abs(MouseRangeStart.X - MouseRangeEnd.X), Math.Abs(MouseRangeStart.Y - MouseRangeEnd.Y));
        }
    }

    private void GraphControl_Resize(object sender, EventArgs e) => Draw();

    private void toolStripMenuItemLogScaleX_CheckedChanged(object sender, EventArgs e)
    {
        XLog = toolStripMenuItemLogScaleX.Checked;
        YLog = toolStripMenuItemLogScaleY.Checked;
    }
    public void SetInitialParameter(bool xLog, bool yLog)
    {
        this.xLog = xLog;
        this.yLog = yLog;
    }
    #endregion

    //260607Cl 追加: 描画領域をベクトル(EMF+)としてクリップボードへコピー。
    //ReciPro 各所 (ScalablePictureBox / FormStereonet / FormDiffractionSimulator / FormImageSimulator) と同じ
    //Crystallography.ClipboardMetafileHelper を使い、画面表示と同一の描画シーケンス(renderGraph)を metafile の Graphics に流す。
    private void buttonCopy_Click(object sender, EventArgs e)
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0].Pt == null || destProfileList[0].Pt.Count == 0) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;

        var savedG = G;//renderGraph が field G を上書きするため、画面用 Graphics を退避
        try
        {
            ClipboardMetafileHelper.PutDrawingOnClipboardAsEnhMetafile(this.Handle, renderGraph);
        }
        finally
        {
            G = savedG;//画面用 Graphics へ復帰 (metafile の Graphics は helper 内で破棄済み)
        }
    }
}

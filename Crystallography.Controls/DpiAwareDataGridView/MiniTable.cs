using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls;

/// <summary>
/// 数行×数列の「読み取り専用ミニ表」用 DataGridView 派生コントロール。260606Cl 追加。
/// </summary>
/// <remarks>
/// 設計方針 (260606Cl):
/// - <see cref="DpiAwareDataGridView"/> を継承し、構築時点で「読み取り専用・選択なし・スクロールなし・静かな見た目」になる。
///   呼び出し側で MakeQuiet 相当を呼ぶ必要はない (派生による自己構成)。
/// - 配色 / CellStyle / 罫線は本クラスに <b>一元化</b>し、消費側フォームでは設定させない (該当プロパティはデザイナから非表示)。
/// - 表示専用テーブルに無関係な継承プロパティ (AllowUserTo* / ReadOnly / DataSource / 各種 CellStyle 等) は
///   <c>[Browsable(false)]</c> でデザイナのプロパティグリッドから隠す (下部 region)。公開するのは <see cref="Selectable"/> /
///   <see cref="AutoFitHeight"/> など、表示専用テーブルとして意味のあるものだけ。
/// - データ投入は DataSet/バインドを使わず <see cref="SetRows"/> (丸ごと差替) / <see cref="AddRow"/> (1 行追加) で object[] を渡す。
/// - 列ヘッダーをローカライズする表は、列を <b>デザイナで定義</b>する (header/Alignment/Format は列側で設定 → resources.ApplyResources +
///   .resx/.ja.resx の既存翻訳機構に乗る)。記号のみで翻訳不要の表は <see cref="SetColumns"/> でコード生成してもよい。
/// - DPI 列幅・ヘッダ中央寄せは基底 <see cref="DpiAwareDataGridView"/> 任せ。列を AutoSize にすると基底の列幅 DPI 計算と二重化しない。
/// </remarks>
[ToolboxItem(true)]
public class MiniTable : DpiAwareDataGridView
{
    public MiniTable()
    {
        // 構造系は構築時に固定 (デザイン画面にもミニ表の姿で反映される)。
        base.ReadOnly = true;
        base.AllowUserToAddRows = false;
        base.AllowUserToDeleteRows = false;
        base.AllowUserToResizeRows = false;
        base.AllowUserToResizeColumns = false;
        base.AllowUserToOrderColumns = false;
        base.RowHeadersVisible = false;
        base.MultiSelect = false;
        base.VirtualMode = false;
        base.ScrollBars = ScrollBars.None;
        base.BorderStyle = BorderStyle.None;
        base.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        base.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // 列ごとの AutoSizeMode を使う
        base.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        base.TabStop = false;
    }

    #region 公開オプション (表示専用テーブルとして意味のあるものだけ)

    private bool selectable;
    /// <summary>1 行ハイライト (選択) を残すか。既定 false (表示専用で選択色を地色に潰す)。260606Cl 追加。</summary>
    [DefaultValue(false), Category("MiniTable")]
    public bool Selectable
    {
        get => selectable;
        set { selectable = value; if (IsHandleCreated) ApplyTheme(); }
    }

    /// <summary><see cref="SetRows"/> 後にコントロール高さを「ヘッダ + 全行」にフィットさせるか。既定 false。260606Cl 追加。</summary>
    [DefaultValue(false), Category("MiniTable")]
    public bool AutoFitHeight { get; set; }

    /// <summary><see cref="SetRows"/> 後にコントロール幅を「全列の内容幅」にフィットさせるか。既定 false。260607Cl 追加 (実験的)。</summary>
    /// <remarks>内容フィット (AllCells) 主体の表向け。Fill 列を含む表は親幅に追従させる設計なので噛み合わない (無効化推奨)。</remarks>
    [DefaultValue(false), Category("MiniTable")]
    public bool AutoFitWidth { get; set; }

    private bool allowVerticalScroll;
    /// <summary>行数がコントロール高さを超えるとき縦スクロールバーを許可するか (opt-in)。既定 false (=表示専用で非表示)。
    /// 固定高さのセルに置き行数が増減する表 (Beam Interaction のスカラ/線表など) で true にする。260606Cl 追加。</summary>
    [DefaultValue(false), Category("MiniTable")]
    public bool AllowVerticalScroll
    {
        get => allowVerticalScroll;
        set { allowVerticalScroll = value; base.ScrollBars = value ? ScrollBars.Vertical : ScrollBars.None; }
    }

    #endregion

    #region 配色 / CellStyle の一元化

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e); // 基底: DPI 列幅スケーリング + ヘッダ中央寄せ
        ApplyTheme();
    }

    protected override void OnParentChanged(EventArgs e)
    {
        base.OnParentChanged(e);
        if (IsHandleCreated)
            base.BackgroundColor = ResolveBackground();
    }

    /// <summary>
    /// DataGridView.BackgroundColor は不透明色必須。親が透明 (例: 視覚スタイル下の TabPage は BackColor=Transparent) の場合は
    /// 例外になるため Control にフォールバックする。260606Cl 追加。
    /// </summary>
    private Color ResolveBackground()
    {
        var c = Parent?.BackColor ?? SystemColors.Control;
        return c.A == 255 ? c : SystemColors.Control;
    }

    /// <summary>配色・CellStyle・選択色をここ 1 箇所で適用する (デザイナから上書きさせない)。260606Cl 追加。</summary>
    private void ApplyTheme()
    {
        base.GridColor = SystemColors.ControlLight;
        base.BackgroundColor = ResolveBackground();
        base.TabStop = Selectable;

        base.DefaultCellStyle.BackColor = SystemColors.Window;
        base.DefaultCellStyle.ForeColor = SystemColors.ControlText;

        // 控えめな交互行色 (ハイコントラストでは無効化)
        var alt = SystemInformation.HighContrast ? SystemColors.Window : Color.FromArgb(248, 248, 248);
        base.AlternatingRowsDefaultCellStyle.BackColor = alt;

        if (Selectable)
        {
            base.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            base.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            base.AlternatingRowsDefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            base.AlternatingRowsDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
        }
        else
        {
            base.DefaultCellStyle.SelectionBackColor = base.DefaultCellStyle.BackColor;
            base.DefaultCellStyle.SelectionForeColor = base.DefaultCellStyle.ForeColor;
            base.AlternatingRowsDefaultCellStyle.SelectionBackColor = alt;
            base.AlternatingRowsDefaultCellStyle.SelectionForeColor = base.DefaultCellStyle.ForeColor;
        }
    }

    #endregion

    #region データ投入 (DataSet/バインド不使用)

    /// <summary>1 行を追加する。値は double/int/string を並べて渡す。260606Cl 追加。</summary>
    /// <remarks>例: <c>AddRow("Fe", 26, 26.0, -1.13, 3.20)</c> あるいは <c>AddRow(objectArray)</c>。返り値は追加行の index。
    /// 書式・右寄せは列の DefaultCellStyle が効く。NaN/Infinity はそのまま文字列化される (空欄は null/"" を渡す)。</remarks>
    public int AddRow(params object[] values)
    {
        if (values == null || (Columns.Count > 0 && values.Length != Columns.Count))
            throw new ArgumentException("Row value count does not match column count.", nameof(values));
        return Rows.Add(values);
    }

    /// <summary>全行を <paramref name="rows"/> で丸ごと差し替える (結晶切替などのたびに呼ぶ)。260606Cl 追加。</summary>
    public void SetRows(IEnumerable<object[]> rows)
    {
        SuspendLayout();
        try
        {
            Rows.Clear();
            foreach (var row in rows)
            {
                if (row.Length != Columns.Count)
                    throw new ArgumentException("Row value count does not match column count.", nameof(rows));
                Rows.Add(row);
            }
            ClearSelection();
            CurrentCell = null;
        }
        finally
        {
            ResumeLayout();
        }
        if (AutoFitWidth)
            FitWidthToColumns();
        if (AutoFitHeight)
            FitHeightToRows();
    }

    /// <summary>全行を消す。260606Cl 追加。</summary>
    public void ClearRows() => Rows.Clear();

    /// <summary>コントロール高さを「ヘッダ + 全行」に縮める (ScrollBars=None 前提)。手動 <see cref="AddRow"/> 後は明示的に呼ぶ。260606Cl 追加。</summary>
    public void FitHeightToRows()
    {
        AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        var chrome = Height - ClientSize.Height; // 枠ぶん
        Height = chrome
            + (ColumnHeadersVisible ? ColumnHeadersHeight : 0)
            + Rows.GetRowsHeight(DataGridViewElementStates.Visible)
            + 2;
    }

    /// <summary>コントロール幅を「現在の全 (可視) 列幅の合計」に合わせる (横スクロールなし前提)。<see cref="FitHeightToRows"/> の幅版。
    /// 手動 <see cref="AddRow"/> 後は明示的に呼ぶ。260607Cl 追加 (実験的)。</summary>
    /// <remarks>
    /// 各列は自身の <see cref="DataGridViewColumn.AutoSizeMode"/> 通りに既に幅が決まっており、本メソッドはその合計に枠を足して
    /// コントロール幅へ反映するだけ。よって列ごとの指定がそのまま効く:
    /// <list type="bullet">
    /// <item>None + Width=N : 絶対 N px を固定で寄与。</item>
    /// <item>AllCells : 内容幅で寄与 (この合計に縮める対象)。</item>
    /// <item>Fill : 「残り幅を埋める」ため常にコントロール幅へ追従し合計は client 幅と一致 → 縮まない。Fill 列を含む表では無効化推奨。</item>
    /// </list>
    /// 一律内容幅に潰す <c>AutoResizeColumns(AllCells)</c> は None の絶対幅や Fill の按分を壊すため呼ばない。
    /// </remarks>
    public void FitWidthToColumns()
    {
        var chrome = Width - ClientSize.Width; // 枠 + (表示中の) 縦スクロールバー
        Width = chrome
            + (RowHeadersVisible ? RowHeadersWidth : 0)
            + Columns.GetColumnsWidth(DataGridViewElementStates.Visible)
            + 2;
    }

    #endregion

    #region 列のコード生成 (記号のみ・翻訳不要の表向け。翻訳要る表はデザイナで列定義する)

    /// <summary>ミニ表の 1 列分の定義 (<see cref="SetColumns"/> 用)。260606Cl 追加。</summary>
    /// <param name="Header">列見出し。コード生成のため resx に載らない (=翻訳されない)。翻訳要る表はデザイナ列を使う。</param>
    /// <param name="Align">セルの配置。数値列は MiddleRight、テキスト列は MiddleLeft/MiddleCenter。</param>
    /// <param name="Format">セルの DefaultCellStyle.Format (例 "g4")。値は double/int のまま渡し、表示時に整形させる。</param>
    /// <param name="Fill">true の列だけ残り幅を吸収 (Fill)。他列は内容幅 (AllCells)。Fill は 0 または 1 列。</param>
    public readonly record struct Col(
        string Header,
        DataGridViewContentAlignment Align = DataGridViewContentAlignment.MiddleLeft,
        string Format = null,
        bool Fill = false);

    /// <summary>列をコード生成する (記号のみ・翻訳不要の表向け)。1 回だけ呼ぶ。260606Cl 追加。</summary>
    public void SetColumns(params Col[] cols)
    {
        if (cols == null || cols.Length == 0)
            throw new ArgumentException("At least one column is required.", nameof(cols));
        if (cols.Count(c => c.Fill) > 1)
            throw new ArgumentException("Only one Fill column is expected.", nameof(cols));

        SuspendLayout();
        try
        {
            Rows.Clear();
            Columns.Clear();
            for (int i = 0; i < cols.Length; i++)
            {
                var col = cols[i];
                var c = new DataGridViewTextBoxColumn
                {
                    Name = "Column" + i,
                    HeaderText = col.Header,
                    ReadOnly = true,
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    AutoSizeMode = col.Fill ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.AllCells,
                };
                c.DefaultCellStyle.Alignment = col.Align;
                if (!string.IsNullOrEmpty(col.Format))
                    c.DefaultCellStyle.Format = col.Format;
                Columns.Add(c);
            }
        }
        finally
        {
            ResumeLayout();
        }
    }

    #endregion

    #region デザイナ非表示プロパティ (表示専用テーブルでは固定 / 一元化のため触らせない) 260606Cl

    // --- ユーザー操作・編集系 (常に固定) ---
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool AllowUserToAddRows { get => base.AllowUserToAddRows; set => base.AllowUserToAddRows = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool AllowUserToDeleteRows { get => base.AllowUserToDeleteRows; set => base.AllowUserToDeleteRows = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool AllowUserToResizeColumns { get => base.AllowUserToResizeColumns; set => base.AllowUserToResizeColumns = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool AllowUserToResizeRows { get => base.AllowUserToResizeRows; set => base.AllowUserToResizeRows = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool AllowUserToOrderColumns { get => base.AllowUserToOrderColumns; set => base.AllowUserToOrderColumns = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool ReadOnly { get => base.ReadOnly; set => base.ReadOnly = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool MultiSelect { get => base.MultiSelect; set => base.MultiSelect = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewEditMode EditMode { get => base.EditMode; set => base.EditMode = value; }

    // --- データバインド・仮想化 (ミニ表は非バインド・非仮想専用) ---
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new object DataSource { get => base.DataSource; set => base.DataSource = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new string DataMember { get => base.DataMember; set => base.DataMember = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool VirtualMode { get => base.VirtualMode; set => base.VirtualMode = value; }

    // --- 配色・罫線・サイズ (ApplyTheme / コンストラクタで一元化) ---
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool RowHeadersVisible { get => base.RowHeadersVisible; set => base.RowHeadersVisible = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ScrollBars ScrollBars { get => base.ScrollBars; set => base.ScrollBars = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new BorderStyle BorderStyle { get => base.BorderStyle; set => base.BorderStyle = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewCellBorderStyle CellBorderStyle { get => base.CellBorderStyle; set => base.CellBorderStyle = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Color BackgroundColor { get => base.BackgroundColor; set => base.BackgroundColor = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Color GridColor { get => base.GridColor; set => base.GridColor = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode { get => base.AutoSizeColumnsMode; set => base.AutoSizeColumnsMode = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewAutoSizeRowsMode AutoSizeRowsMode { get => base.AutoSizeRowsMode; set => base.AutoSizeRowsMode = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool EnableHeadersVisualStyles { get => base.EnableHeadersVisualStyles; set => base.EnableHeadersVisualStyles = value; }

    // --- CellStyle 一元化 (すべて ApplyTheme で管理。デザイナでは触らせない) ---
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewCellStyle DefaultCellStyle { get => base.DefaultCellStyle; set => base.DefaultCellStyle = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewCellStyle AlternatingRowsDefaultCellStyle { get => base.AlternatingRowsDefaultCellStyle; set => base.AlternatingRowsDefaultCellStyle = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewCellStyle RowsDefaultCellStyle { get => base.RowsDefaultCellStyle; set => base.RowsDefaultCellStyle = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewCellStyle ColumnHeadersDefaultCellStyle { get => base.ColumnHeadersDefaultCellStyle; set => base.ColumnHeadersDefaultCellStyle = value; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataGridViewCellStyle RowHeadersDefaultCellStyle { get => base.RowHeadersDefaultCellStyle; set => base.RowHeadersDefaultCellStyle = value; }

    #endregion
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Crystallography.Controls;

[Guid("99E21F3C-6FF6-4084-9097-A88566830F29")]
[ToolboxItem(true)] // 260605Cl 追加: 基底 UserControlBase の [ToolboxItem(false)] 継承を打ち消しデザイナのツールボックスに表示
public partial class BoundControl : UserControlBase
{
    #region プロパティ
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SkipEvent { get; set; } = false;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Crystal Crystal
    {
        get => crystal;
        set
        {
            crystal = value;
            if (crystal != null)
            {
                table.Clear();
                AddRange(Crystal.Bounds);
            }
        }
    }
    private Crystal crystal = null;

    private (int H, int K, int L) index => indexControl.Values;
    private bool equivalency { get => checkBoxEquivalency.Checked; set => checkBoxEquivalency.Checked = value; }
    public double MaximumDistance => numericBoxMaximumDistanceFromOrigin.Value / 10;

    private readonly DataSet.DataTableBoundDataTable table;


    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MillerBravaisIndex
    {
        set
        {
            iDataGridViewTextBoxColumn.Visible = value; // 260517Cl 有効化: Designer 側に iDataGridViewTextBoxColumn を追加済み
            indexControl.MillerBravais = value;
        }
    }

    #endregion

    public event EventHandler ItemsChanged;

    public BoundControl()
    {
        InitializeComponent();
        table = dataSet.DataTableBound;
        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);
    }

    #region Bound を画面下部から生成 / 画面下部にセット
    public Bound GetFromInterface() =>
        new(true, Crystal, index.H, index.K, index.L, equivalency, numericBoxDistance.Value / 10, numericBoxTranslation.Value / 10, colorControl.Argb);

    private double ReciprocalLengthHKL() =>
        (index.H * Crystal.A_Star + index.K * Crystal.B_Star + index.L * Crystal.C_Star).Length;

    public void SetToInterface(Bound b)
    {
        SkipEvent = true;
        indexControl.Values = b.Index;
        checkBoxEquivalency.Checked = b.Equivalency;
        numericBoxDistance.Value = b.Distance * 10; // Å ↔ nm
        numericBoxDistanceD.Value = numericBoxDistance.Value * 0.1 * ReciprocalLengthHKL();
        numericBoxTranslation.Value = b.Translation * 10;
        colorControl.Color = Color.FromArgb(b.ColorArgb);
        SkipEvent = false;
    }
    #endregion

    #region データベース操作

    private void NotifyChanged()
    {
        if (crystal == null) return;
        Crystal.Bounds = GetAll();
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Add(Bound bound)
    {
        if (bound == null || bound.Index == (0, 0, 0)) return;
        table.Add(bound);
        NotifyChanged();
    }

    public void AddRange(IEnumerable<Bound> bounds)
    {
        if (bounds == null) return;
        SkipEvent = true;
        foreach (var b in bounds.Where(b => b.Index != (0, 0, 0)))
            table.Add(b);
        SkipEvent = false;
        NotifyChanged();
        bindingSource_PositionChanged(this, EventArgs.Empty);
    }

    public void Delete(int i) { table.Remove(i); NotifyChanged(); }
    public void Replace(Bound bound, int i) { table.Replace(bound, i); NotifyChanged(); }
    public void Clear() { table.Clear(); NotifyChanged(); }

    public Bound[] GetAll()
    {
        var bounds = table.GetAll();
        foreach (var b in bounds) b.Reset(Crystal);
        return bounds;
    }
    #endregion

    #region 追加/削除/置換 ボタン
    private void buttonAdd_Click(object sender, EventArgs e)
    {
        var bound = GetFromInterface();
        if (bound != null && bound.Index != (0, 0, 0))
        {
            Add(bound);
            bindingSource.Position = bindingSource.Count - 1;
        }
    }

    private void buttonChange_Click(object sender, EventArgs e)
    {
        var pos = bindingSource.Position;
        if (pos < 0) return;
        Replace(GetFromInterface(), pos);
        bindingSource.Position = pos;
    }

    private void buttonDelete_Click(object sender, EventArgs e)
    {
        int pos = bindingSource.Position;
        if (pos < 0) return;
        SkipEvent = true;
        Delete(pos);
        SkipEvent = false;
        bindingSource.Position = bindingSource.Count > pos ? pos : pos - 1;
    }
    #endregion

    private void bindingSource_PositionChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        SkipEvent = true;
        if (bindingSource.Position >= 0 && bindingSource.Count > 0)
            SetToInterface(table.Get(bindingSource.Position));
        SkipEvent = false;
    }

    private void checkBoxEquivalency_CheckedChanged(object sender, EventArgs e)
    {
        indexControl.Bracket = checkBoxEquivalency.Checked ? IndexControl.BracketEnum.Angle : IndexControl.BracketEnum.Round;
        if (checkBoxImmediateUpdate.Checked)
            buttonChange_Click(sender, e);
    }

    #region numericBox イベント
    private void numericBoxDistance_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent || index == (0, 0, 0)) return;
        SkipEvent = true;
        numericBoxDistanceD.Value = numericBoxDistance.Value * 0.1 * ReciprocalLengthHKL();
        SkipEvent = false;
        if (checkBoxImmediateUpdate.Checked)
            buttonChange_Click(sender, e);
    }

    private void numericBoxDistanceD_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent || index == (0, 0, 0)) return;
        SkipEvent = true;
        numericBoxDistance.Value = numericBoxDistanceD.Value * 10 / ReciprocalLengthHKL();
        SkipEvent = false;
        if (checkBoxImmediateUpdate.Checked)
            buttonChange_Click(sender, e);
    }

    private void numericBoxMaximumDistanceFromOrigin_ValueChanged(object sender, EventArgs e) =>
        ItemsChanged?.Invoke(this, EventArgs.Empty);

    private void colorControl_ColorChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        if (checkBoxImmediateUpdate.Checked)
            buttonChange_Click(sender, e);
    }
    #endregion

    #region dataGridView イベント
    private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        if (dataGridView.CurrentCellAddress.X == 0 && dataGridView.IsCurrentCellDirty)
            dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex != 0 || e.RowIndex < 0) return;
        table.Get(bindingSource.Position).Enabled = (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    // 260517Cl 追加: i = -(h+k) を CellFormatting で表示する (LatticePlaneControl と同じパターン、DataTable には保持しない)
    private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || dataGridView.Columns[e.ColumnIndex] != iDataGridViewTextBoxColumn) return;
        var row = dataGridView.Rows[e.RowIndex];
        var h = Convert.ToInt32(row.Cells[hDataGridViewTextBoxColumn.Index].Value);
        var k = Convert.ToInt32(row.Cells[kDataGridViewTextBoxColumn.Index].Value);
        e.Value = (-h - k).ToString();
        e.FormattingApplied = true;
    }
    #endregion
}

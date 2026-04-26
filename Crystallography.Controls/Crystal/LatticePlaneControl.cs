using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class LatticePlaneControl : UserControlBase
{
    #region プロパティ, フィールド, イベントハンドラ
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
                AddRange(Crystal.LatticePlanes);
            }
        }
    }
    private Crystal crystal = null;

    public event EventHandler ItemsChanged;

    private readonly DataSet.DataTableLatticePlaneDataTable table;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MillerBravaisIndexActive { set => iDataGridViewTextBoxColumn.Visible = value; }

    #endregion

    public LatticePlaneControl()
    {
        InitializeComponent();
        table = dataSet.DataTableLatticePlane;
    }

    #region LatticePlane クラスを画面下部から生成 / にセット

    public LatticePlane GetFromInterface() =>
        new(true, Crystal, numericBoxH.ValueInteger, numericBoxK.ValueInteger, numericBoxL.ValueInteger,
            numericBoxDistance.Value, colorControl.Argb);

    public void SetToInterface(LatticePlane plane)
    {
        numericBoxH.Value = plane.Index.H;
        numericBoxK.Value = plane.Index.K;
        numericBoxL.Value = plane.Index.L;
        numericBoxDistance.Value = plane.Translation;
        colorControl.Color = Color.FromArgb(plane.ColorArgb);
    }
    #endregion

    #region データベース操作

    private void NotifyChanged()
    {
        if (crystal == null) return;
        crystal.LatticePlanes = GetAll();
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Add(LatticePlane plane)
    {
        if (plane == null || plane.Index == (0, 0, 0)) return;
        table.Add(plane);
        NotifyChanged();
    }

    public void AddRange(IEnumerable<LatticePlane> planes)
    {
        if (planes == null) return;
        SkipEvent = true;
        foreach (var p in planes.Where(p => p.Index != (0, 0, 0)))
            table.Add(p);
        SkipEvent = false;
        NotifyChanged();
        bindingSource_PositionChanged(this, EventArgs.Empty);
    }

    public void Delete(int i)
    {
        table.Remove(i);
        NotifyChanged();
    }

    public void Replace(LatticePlane plane, int i)
    {
        table.Replace(plane, i);
        NotifyChanged();
    }

    public void Clear()
    {
        table.Clear();
        NotifyChanged();
    }

    public LatticePlane[] GetAll() => table.GetAll();

    #endregion

    #region 追加/削除/置換 ボタン

    private void buttonAdd_Click(object sender, EventArgs e)
    {
        var plane = GetFromInterface();
        if (plane != null && plane.Index != (0, 0, 0))
        {
            Add(plane);
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
        SkipEvent = true; // bindingSource_PositionChanged が走らないように抑止
        Delete(pos);
        SkipEvent = false;
        bindingSource.Position = bindingSource.Count > pos ? pos : pos - 1;
    }

    #endregion

    #region bindingSource / dataGridView イベント
    private void bindingSource_PositionChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        if (bindingSource.Position >= 0 && bindingSource.Count > 0)
            SetToInterface(dataSet.DataTableLatticePlane.Get(bindingSource.Position));
    }

    private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        // チェックボックスを即時コミットして CellValueChanged を発火させる
        if (dataGridView.CurrentCellAddress.X == 0 && dataGridView.IsCurrentCellDirty)
            dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex != 0 || e.RowIndex < 0) return;
        table.Get(bindingSource.Position).Enabled = (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    private void numericBoxH_ValueChanged(object sender, EventArgs e) =>
        numericBoxI.Value = -numericBoxH.Value - numericBoxK.Value;

    private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || dataGridView.Columns[e.ColumnIndex] != iDataGridViewTextBoxColumn) return;
        var row = dataGridView.Rows[e.RowIndex];
        var h = Convert.ToInt32(row.Cells[hDataGridViewTextBoxColumn.Index].Value);
        var k = Convert.ToInt32(row.Cells[kDataGridViewTextBoxColumn.Index].Value);
        e.Value = (-h - k).ToString();
        e.FormattingApplied = true;
    }
}

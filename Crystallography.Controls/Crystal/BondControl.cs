using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class BondInputControl : CaptureUserControlBase
{
    #region プロパティ, フィールド, イベントハンドラ
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Crystal Crystal
    {
        get => crystal;
        set
        {
            crystal = value;
            if (crystal == null) return;
            ElementList = [.. crystal.Atoms.Select(a => a.ElementName).Distinct()];
            if (ElementList != null && ElementList.Length != 0)
            {
                comboBoxBondingAtom1.Items.Clear();
                comboBoxBondingAtom1.Items.AddRange(ElementList);
                comboBoxBondingAtom2.Items.Clear();
                comboBoxBondingAtom2.Items.AddRange(ElementList);
            }
            table.Clear();
            AddRange(crystal.Bonds);
        }
    }
    private Crystal crystal = null;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] ElementList { get; set; } = null;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SkipEvent { get; set; } = false;

    private readonly DataSet.DataTableBondDataTable table;

    public event EventHandler ItemsChanged;
    #endregion

    public BondInputControl()
    {
        InitializeComponent();
        table = dataSet.DataTableBond;
        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);
    }

    #region Bonds クラスを画面下部から生成 / にセット (表示は Å、内部は nm)
    public Bonds GetFromInterface()
    {
        if (ElementList.Length < 1 || comboBoxBondingAtom1.Text.Length == 0 || comboBoxBondingAtom2.Text.Length == 0)
            return null;
        return new Bonds(
            true, ElementList, comboBoxBondingAtom1.Text, comboBoxBondingAtom2.Text,
            numericBoxBondMinLength.Value / 10.0, numericBoxBondMaxLength.Value / 10.0,
            checkBoxShowBonds.Checked, numericBoxBondRadius.Value / 10.0, numericBoxBondAlpha.Value,
            checkBoxShowPolyhedron.Checked, checkBoxShowCenterAtom.Checked, checkBoxShowVertexAtoms.Checked,
            checkBoxShowInnerBonds.Checked, numericBoxPolyhedronAlpha.Value,
            checkBoxShowEdges.Checked, numericBoxEdgeWidth.Value);
    }

    public void SetToInterface(Bonds b)
    {
        checkBoxShowBonds.Checked = b.ShowBond;
        comboBoxBondingAtom1.Text = b.Element1;
        comboBoxBondingAtom2.Text = b.Element2;
        numericBoxBondMinLength.Value = b.MinLength * 10;
        numericBoxBondMaxLength.Value = b.MaxLength * 10;
        numericBoxBondRadius.Value = b.Radius * 10;
        numericBoxBondAlpha.Value = b.BondTransParency;
        colorControlBond.Color = Color.FromArgb(b.ArgbBond);
        numericBoxPolyhedronAlpha.Value = b.PolyhedronTransParency;

        checkBoxShowPolyhedron.Checked = b.ShowPolyhedron;
        checkBoxShowCenterAtom.Checked = b.ShowCenterAtom;
        checkBoxShowVertexAtoms.Checked = b.ShowVertexAtom;
        checkBoxShowInnerBonds.Checked = b.ShowInnerBonds;
        colorControlPolyhedron.Color = Color.FromArgb(b.ArgbPolyhedron);

        checkBoxShowEdges.Checked = b.ShowEdges;
        numericBoxEdgeWidth.Value = b.EdgeLineWidth;
        colorControlEdges.Color = Color.FromArgb(b.ArgbEdge);
    }
    #endregion

    private void checkBoxShowPolyhedron_CheckedChanged(object sender, EventArgs e) => groupBoxPolyhedron.Enabled = checkBoxShowPolyhedron.Checked;
    private void checkBoxShowEdges_CheckedChanged(object sender, EventArgs e) => groupBoxEdge.Enabled = checkBoxShowEdges.Checked;
    private void checkBoxShowBonds_CheckedChanged(object sender, EventArgs e) => groupBoxBonds.Enabled = checkBoxShowBonds.Checked;

    #region データベース操作

    private void NotifyChanged()
    {
        if (crystal == null) return;
        crystal.Bonds = GetAll();
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Add(Bonds bonds)
    {
        if (bonds != null) table.Add(bonds);
        NotifyChanged();
    }

    public void AddRange(IEnumerable<Bonds> bonds)
    {
        if (bonds == null) return;
        SkipEvent = true;
        foreach (var b in bonds) table.Add(b);
        crystal.Bonds = GetAll();
        SkipEvent = false;
        ItemsChanged?.Invoke(this, EventArgs.Empty);
        bindingSource_PositionChanged(this, EventArgs.Empty);
    }

    public void Delete(int i) { table.Remove(i); NotifyChanged(); }
    public void Replace(Bonds bonds, int i) { table.Replace(bonds, i); NotifyChanged(); }
    public void Clear() { table.Clear(); NotifyChanged(); }

    public Bonds[] GetAll() => table.GetAll();
    #endregion

    #region 追加/削除/置換 ボタン
    private void buttonAdd_Click(object sender, EventArgs e)
    {
        var bond = GetFromInterface();
        if (bond != null)
        {
            Add(bond);
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
        if (bindingSource.Position >= 0 && bindingSource.Count > 0)
            SetToInterface(table.Get(bindingSource.Position));
    }

    #region dataGridView イベント
    private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        // チェックボックスを即時コミット
        var x = dataGridView.CurrentCellAddress.X;
        if ((x == 0 || x == 5 || x == 6) && dataGridView.IsCurrentCellDirty)
            dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (e.ColumnIndex is 0 or 5 or 6)
        {
            var bond = table.Get(bindingSource.Position);
            var value = (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            switch (e.ColumnIndex)
            {
                case 0: bond.Enabled = value; break;
                case 5: bond.ShowBond = value; break;
                case 6: bond.ShowPolyhedron = value; break;
            }
        }
        ItemsChanged?.Invoke(this, EventArgs.Empty);
        bindingSource_PositionChanged(sender, EventArgs.Empty);
    }
    #endregion
}

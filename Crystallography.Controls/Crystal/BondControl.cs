using Crystallography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class BondInputControl : UserControl
{
    #region プロパティ, フィールド、イベントハンドラ

    public Crystal Crystal
    {
        get => crystal; set
        {
            crystal = value;

            if (crystal != null)
            {
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
    }
    private Crystal crystal = null;

    public string[] ElementList { get; set; } = null;
    public bool SkipEvent { get; set; } = false;




    private readonly DataSet.DataTableBondDataTable table;

    public event EventHandler ItemsChanged;
    #endregion,


    public BondInputControl()
    {
        InitializeComponent();
        table = dataSet.DataTableBond;
        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);

    }

    #region Bondsクラスを画面下部　から生成 /　にセット. 表示の単位は Å だが、中身は nm 単位.
    public Bonds GetFromInterface()
    {
        if (ElementList.Length < 1 || comboBoxBondingAtom1.Text.Length == 0 || comboBoxBondingAtom2.Text.Length == 0)
            return null;
        else
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
        //ElementList = b.ElementList;
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
        colorControlPlyhedron.Color = Color.FromArgb(b.ArgbPolyhedron);

        checkBoxShowEdges.Checked = b.ShowEdges;
        numericBoxEdgeWidth.Value = b.EdgeLineWidth;
        colorControlEdges.Color = Color.FromArgb(b.ArgbEdge);
    }
    #endregion

    #region チェックボックスイベント

    private void checkBoxShowPolyhedron_CheckedChanged(object sender, EventArgs e) => groupBoxPolyhedron.Enabled = checkBoxShowPolyhedron.Checked;
    private void checkBoxShowEdges_CheckedChanged(object sender, EventArgs e) => groupBoxEdge.Enabled = checkBoxShowEdges.Checked;

    private void checkBoxShowBonds_CheckedChanged(object sender, EventArgs e) => groupBoxBonds.Enabled = checkBoxShowBonds.Checked;
    #endregion

    #region データベース操作
    /// <summary>
    /// データベースにbondsを追加する
    /// </summary>
    /// <param name="bonds"></param>
    public void Add(Bonds bonds)
    {
        if (bonds != null)
            table.Add(bonds);

        crystal.Bonds = GetAll();
        ItemsChanged?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// データベースに原子を追加する
    /// </summary>
    /// <param name="bonds"></param>
    public void AddRange(IEnumerable<Bonds> bonds)
    {
        if (bonds != null)
        {
            SkipEvent = true;
            foreach (var b in bonds)
                table.Add(b);

            crystal.Bonds = GetAll();
            SkipEvent = false;
            ItemsChanged?.Invoke(this, new EventArgs());
            bindingSource_PositionChanged(new object(), new EventArgs());
        }
    }

    /// <summary>
    /// データベースのi番目の原子を削除
    /// </summary>
    /// <param name="i"></param>
    public void Delete(int i)
    {
        table.Remove(i);
        crystal.Bonds = GetAll();
        ItemsChanged?.Invoke(this, new EventArgs());

    }

    /// <summary>
    /// データベースのi番目の原子を置換
    /// </summary>
    /// <param name="bonds"></param>
    /// <param name="i"></param>
    public void Replace(Bonds bonds, int i)
    {
        table.Replace(bonds, i);
        crystal.Bonds = GetAll();
        ItemsChanged?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// データベースの原子を全て削除する
    /// </summary>
    public void Clear()
    {
        table.Clear();
        crystal.Bonds = GetAll();
        ItemsChanged?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// データベース中の全ての原子を取得
    /// </summary>
    /// <returns></returns>
    public Bonds[] GetAll() => table.GetAll();

    #endregion

    #region 追加/削除/置換 ボタン

    /// <summary>
    /// 追加ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonAdd_Click(object sender, System.EventArgs e)
    {
        var bond = GetFromInterface();
        if (bond != null)
        {
            Add(bond);
            bindingSource.Position = bindingSource.Count - 1;
        }
    }

    /// <summary>
    /// 変更ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonChange_Click(object sender, System.EventArgs e)
    {
        var pos = bindingSource.Position;
        if (pos >= 0)
        {
            Replace(GetFromInterface(), pos);
            bindingSource.Position = pos;
        }
    }

    /// <summary>
    /// 削除ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonDelete_Click(object sender, System.EventArgs e)
    {
        int pos = bindingSource.Position;
        if (pos >= 0)
        {
            SkipEvent = true;//bindingSourceAtoms_PositionChangedが呼ばれるのを防ぐ
            Delete(pos);
            SkipEvent = false;
            bindingSource.Position = bindingSource.Count > pos ? pos : pos - 1;//選択列を選択しなおす
        }
    }

    #endregion

    #region bindingSourceイベント

    /// <summary>
    /// 選択行が変更されたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void bindingSource_PositionChanged(object sender, System.EventArgs e)
    {
        if (SkipEvent) return;

        if (bindingSource.Position >= 0 && bindingSource.Count > 0)
            SetToInterface(dataSet.DataTableBond.Get(bindingSource.Position));
    }
    #endregion

    #region dataGridView イベント

    private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {//チェックボックスが変わると即座に反映させる
        var x = dataGridView.CurrentCellAddress.X;
        if ((x == 0 || x == 5 || x == 6) && dataGridView.IsCurrentCellDirty)
            dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);//コミットする
    }
    private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            if (e.ColumnIndex == 0)
                table.Get(bindingSource.Position).Enabled
                    = (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            else if (e.ColumnIndex == 5)
                table.Get(bindingSource.Position).ShowBond
                    = (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            else if (e.ColumnIndex == 6)
                table.Get(bindingSource.Position).ShowPolyhedron
                    = (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            ItemsChanged?.Invoke(this, new EventArgs());
            bindingSource_PositionChanged(sender, new EventArgs());
        }
    }
    #endregion


}

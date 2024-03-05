﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Crystallography.Controls;

[System.Runtime.InteropServices.Guid("99E21F3C-6FF6-4084-9097-A88566830F29")]
public partial class BoundControl : UserControl
{
    #region プロパティ
    public bool SkipEvent { get; set; } = false;
    // public Crystal Crystal { get; set; } = null;

    public Crystal Crystal
    {
        get => crystal; set
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

    private (int H, int K, int L) index { get => (numericBoxH.ValueInteger, numericBoxK.ValueInteger, numericBoxL.ValueInteger); }

    private bool equivalency { get => checkBoxEquivalency.Checked; set => checkBoxEquivalency.Checked = value; }

    public double MaximumDistance => numericBoxMaximumDistanceFromOrigin.Value / 10;

    private readonly DataSet.DataTableBoundDataTable table;

    #endregion

    #region イベント
    public event EventHandler ItemsChanged;
    #endregion

    #region コンストラクタ
    public BoundControl()
    {
        InitializeComponent();
        table = dataSet.DataTableBound;
        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);

    }
    #endregion

    #region　Boundを画面下部から生成 / Boundを画面下部にセット
    public Bound GetFromInterface() => new(true, Crystal, index.H, index.K, index.L, equivalency, numericBoxDistance.Value / 10, numericBoxTranslation.Value / 10, colorControl.Argb);

    public void SetToInterface(Bound b)
    {
        SkipEvent = true;
        numericBoxH.Value = b.Index.H;
        numericBoxK.Value = b.Index.K;
        numericBoxL.Value = b.Index.L;

        checkBoxEquivalency.Checked = b.Equivalency;

        numericBoxDistance.Value = b.Distance * 10;//Åとnmの変換

        numericBoxDistanceD.Value = numericBoxDistance.Value * 0.1 * (index.H * Crystal.A_Star + index.K * Crystal.B_Star + index.L * Crystal.C_Star).Length;

        numericBoxTranslation.Value = b.Translation * 10;//Åとnmの変換

        colorControl.Color = Color.FromArgb(b.ColorArgb);
        SkipEvent = false;
    }
    #endregion

    #region データベース操作
    /// <summary>
    /// データベースにbondsを追加する
    /// </summary>
    /// <param name="bonds"></param>
    public void Add(Bound bounds)
    {
        if (bounds != null && bounds.Index != (0, 0, 0))
        {
            table.Add(bounds);
            Crystal.Bounds = GetAll();
            ItemsChanged?.Invoke(this, new EventArgs());
        }

    }

    /// <summary>
    /// データベースに原子を追加する
    /// </summary>
    /// <param name="bounds"></param>
    public void AddRange(IEnumerable<Bound> bounds)
    {
        if (bounds != null)
        {
            SkipEvent = true;
            foreach (var b in bounds.Where(b => b.Index != (0, 0, 0)))
                table.Add(b);
            SkipEvent = false;
            Crystal.Bounds = GetAll();
            ItemsChanged?.Invoke(this, new EventArgs());
            bindingSource_PositionChanged(this, new EventArgs());
        }
    }

    /// <summary>
    /// データベースのi番目の原子を削除
    /// </summary>
    /// <param name="i"></param>
    public void Delete(int i)
    {
        table.Remove(i);
        Crystal.Bounds = GetAll();
        ItemsChanged?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// データベースのi番目の原子を置換
    /// </summary>
    /// <param name="bonds"></param>
    /// <param name="i"></param>
    public void Replace(Bound bounds, int i)
    {
        table.Replace(bounds, i);
        Crystal.Bounds = GetAll();
        ItemsChanged?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// データベースの境界面を全て削除する
    /// </summary>
    public void Clear()
    {
        table.Clear();
        Crystal.Bounds = GetAll();
        ItemsChanged?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// データベース中の全ての境界面を取得
    /// </summary>
    /// <returns></returns>
    public Bound[] GetAll() => table.GetAll();

    #endregion

    #region 追加/削除/置換 ボタン

    /// <summary>
    /// 追加ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonAdd_Click(object sender, System.EventArgs e)
    {
        var bound = GetFromInterface();
        if (bound != null && bound.Index != (0, 0, 0))
        {
            Add(bound);
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
    //選択Atomが変更されたとき
    private void bindingSource_PositionChanged(object sender, System.EventArgs e)
    {
        if (SkipEvent) return;

        SkipEvent = true;
        if (bindingSource.Position >= 0 && bindingSource.Count > 0)
            SetToInterface(dataSet.DataTableBound.Get(bindingSource.Position));
        SkipEvent = false;
    }
    #endregion

    #region checkBoxEquivalency
    private void checkBoxEquivalency_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxEquivalency.Checked)
        {
            label1.Text = "{"; label2.Text = "}";
        }
        else
        {
            label1.Text = "("; label2.Text = ")";
        }

        if (checkBoxImmediateUpdate.Checked)
            buttonChange_Click(sender, e);
    }


    #endregion

    #region numericBoxなどのイベント
    private void numericBoxDistance_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent || index == (0, 0, 0)) return;
        SkipEvent = true;
        numericBoxDistanceD.Value = numericBoxDistance.Value * 0.1 * (index.H * Crystal.A_Star + index.K * Crystal.B_Star + index.L * Crystal.C_Star).Length;
        SkipEvent = false;

        if (checkBoxImmediateUpdate.Checked)
            buttonChange_Click(sender, e);
    }

    private void numericBoxDistanceD_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent || index == (0, 0, 0)) return;

        SkipEvent = true;
        numericBoxDistance.Value = numericBoxDistanceD.Value * 10 / (index.H * Crystal.A_Star + index.K * Crystal.B_Star + index.L * Crystal.C_Star).Length;
        SkipEvent = false;

        if (checkBoxImmediateUpdate.Checked)
            buttonChange_Click(sender, e);

    }

    private void numericBoxMaximumDistanceFromOrigin_ValueChanged(object sender, EventArgs e) => ItemsChanged?.Invoke(this, new EventArgs());

    private void colorControl_ColorChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;

        if (checkBoxImmediateUpdate.Checked)
            buttonChange_Click(sender, e);
    }

    #endregion

    #region dataGridViewのイベント
    private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {//チェックボックスが変わると即座に反映させる
        if (dataGridView.CurrentCellAddress.X == 0 && dataGridView.IsCurrentCellDirty)
            dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);//コミットする
    }
    private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 0 && e.RowIndex >= 0)
        {
            table.Get(bindingSource.Position).Enabled =
                (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            ItemsChanged?.Invoke(this, new EventArgs());
        }
    }
    #endregion




}

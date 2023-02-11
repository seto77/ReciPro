using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class LatticePlaneControl : UserControl
{
    #region プロパティ, フィールド, イベントハンドラ
    public bool SkipEvent { get; set; } = false;
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

    private DataSet.DataTableLatticePlaneDataTable table;

    #endregion

    #region コンストラクタ
    public LatticePlaneControl()
    {
        InitializeComponent();
        table = dataSet.DataTableLatticePlane;
    }
    #endregion

    #region　LatticePlaneクラスを画面下部から生成/にセット

    public LatticePlane GetFromInterface()
    {
        return new LatticePlane(true, Crystal, numericBoxH.ValueInteger, numericBoxK.ValueInteger, numericBoxL.ValueInteger,
           numericBoxDistance.Value, colorControl.Argb);
    }

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
    /// <summary>
    /// データベースにbondsを追加する
    /// </summary>
    /// <param name="bonds"></param>
    public void Add(LatticePlane plane)
    {
        if (plane != null && plane.Index != (0, 0, 0))
        {
            table.Add(plane);
            crystal.LatticePlanes = GetAll();
            ItemsChanged?.Invoke(this, new EventArgs());
        }
    }

    /// <summary>
    /// データベースに原子を追加する
    /// </summary>
    /// <param name="bonds"></param>
    public void AddRange(IEnumerable<LatticePlane> planes)
    {
        if (planes != null)
        {
            SkipEvent = true;
            foreach (var b in planes.Where(p => p.Index != (0, 0, 0)))
                table.Add(b);
            SkipEvent = false;
            crystal.LatticePlanes = GetAll();
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
        crystal.LatticePlanes = GetAll();
        ItemsChanged?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// データベースのi番目の原子を置換
    /// </summary>
    /// <param name="bonds"></param>
    /// <param name="i"></param>
    public void Replace(LatticePlane bounds, int i)
    {
        table.Replace(bounds, i);
        crystal.LatticePlanes = GetAll();
        ItemsChanged?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// データベースの原子を全て削除する
    /// </summary>
    public void Clear()
    {
        table.Clear();
        crystal.LatticePlanes = GetAll();
        ItemsChanged?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// データベース中の全ての原子を取得
    /// </summary>
    /// <returns></returns>
    public LatticePlane[] GetAll() => table.GetAll();

    #endregion

    #region 追加/削除/置換 ボタン

    /// <summary>
    /// 追加ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonAdd_Click(object sender, System.EventArgs e)
    {
        var plane = GetFromInterface();
        if (plane != null && plane.Index != (0, 0, 0))
        {
            Add(plane);
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

        if (bindingSource.Position >= 0 && bindingSource.Count > 0)
            SetToInterface(dataSet.DataTableLatticePlane.Get(bindingSource.Position));
    }
    #endregion

    #region dataGridView イベント
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

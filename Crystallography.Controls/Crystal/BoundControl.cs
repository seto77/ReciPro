using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    [System.Runtime.InteropServices.Guid("99E21F3C-6FF6-4084-9097-A88566830F29")]
    public partial class BoundControl : UserControl
    {
        #region プロパティ
        public bool SkipEvent { get; set; } = false;
        public Crystal Crystal { get; set; } = null;
        private (int H, int K, int L) index { get => (numericBoxH.ValueInteger, numericBoxK.ValueInteger, numericBoxL.ValueInteger); }

        private bool equivalency {get => checkBoxEquivalency.Checked; set => checkBoxEquivalency.Checked = value; }

        private DataSet.DataTableBoundDataTable table;

        #endregion


        #region イベント
        public event EventHandler ItemsChanged; 
        #endregion

        public BoundControl()
        {
            InitializeComponent();
            table = dataSet.DataTableBound;
        }

        #region　Boundを画面下部から生成 / Boundを画面下部にセット
        public Bound GetFromInterface() => new Bound(true, Crystal, index.H, index.K, index.L, equivalency, numericBoxDistance.Value/10, colorControl.Argb);

        public void SetToInterface(Bound b)
        {
            numericBoxH.Value = b.Index.H;
            numericBoxK.Value = b.Index.K;
            numericBoxL.Value = b.Index.L;

            checkBoxEquivalency.Checked = b.Equivalency;

            numericBoxDistance.Value = b.Distance * 10;//Åとnmの変換

            colorControl.Color = Color.FromArgb(b.ColorArgb);
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
                foreach (var b in bounds.Where(b => b.Index != (0, 0, 0)))
                    table.Add(b);
                ItemsChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// データベースのi番目の原子を削除
        /// </summary>
        /// <param name="i"></param>
        public void Delete(int i)
        {
            table.Remove(i);
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
            ItemsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベースの原子を全て削除する
        /// </summary>
        public void Clear()
        {
            table.Clear();
            ItemsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベース中の全ての原子を取得
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
            if (bound != null && bound.Index !=(0,0,0))
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

            if (bindingSource.Position >= 0 && bindingSource.Count > 0)
                SetToInterface(dataSet.DataTableBound.Get(bindingSource.Position));
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
        }


        #endregion


        #region numericBoxのイベント
        private void numericBoxDistance_ValueChanged(object sender, EventArgs e)
        {
            if (SkipEvent || index == (0, 0, 0)) return;
            SkipEvent = true;
            numericBoxDistanceD.Value = numericBoxDistance.Value * 0.1 * (index.H * Crystal.A_Star + index.K * Crystal.B_Star + index.L * Crystal.C_Star).Length;
            SkipEvent = false;
        }

        private void numericBoxDistanceD_ValueChanged(object sender, EventArgs e)
        {
            if (SkipEvent || index == (0, 0, 0)) return;

            SkipEvent = true;
            numericBoxDistance.Value = numericBoxDistanceD.Value * 10 / (index.H * Crystal.A_Star + index.K * Crystal.B_Star + index.L * Crystal.C_Star).Length;
            SkipEvent = false;
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
}

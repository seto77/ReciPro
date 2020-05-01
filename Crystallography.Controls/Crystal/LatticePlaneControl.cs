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
    public partial class LatticePlaneControl : UserControl
    {
        public bool SkipEvent { get; set; } = false;
        public Crystal Crystal { get; set; } = null;

        public event EventHandler LatticePlaneChanged;

        public LatticePlaneControl()
        {
            InitializeComponent();
        }

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




        #region データベース操作
        /// <summary>
        /// データベースにbondsを追加する
        /// </summary>
        /// <param name="bonds"></param>
        public void Add(LatticePlane bounds)
        {
            if (bounds != null)
            {
                dataSet.DataTableLatticePlane.Add(bounds);
                LatticePlaneChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// データベースに原子を追加する
        /// </summary>
        /// <param name="bonds"></param>
        public void AddRange(IEnumerable<LatticePlane> bounds)
        {
            if (bounds != null)
            {
                foreach (var b in bounds)
                    dataSet.DataTableLatticePlane.Add(b);
                LatticePlaneChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// データベースのi番目の原子を削除
        /// </summary>
        /// <param name="i"></param>
        public void Delete(int i)
        {
            dataSet.DataTableLatticePlane.Remove(i);
            LatticePlaneChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベースのi番目の原子を置換
        /// </summary>
        /// <param name="bonds"></param>
        /// <param name="i"></param>
        public void Replace(LatticePlane bounds, int i)
        {
            dataSet.DataTableLatticePlane.Replace(bounds, i);
            LatticePlaneChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベースの原子を全て削除する
        /// </summary>
        public void Clear()
        {
            dataSet.DataTableLatticePlane.Clear();
            LatticePlaneChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベース中の全ての原子を取得
        /// </summary>
        /// <returns></returns>
        public LatticePlane[] GetAll() => dataSet.DataTableLatticePlane.GetAll();

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
            if (bound != null)
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
                bindingSource.Position = bindingSource.Count > pos ? pos : pos - 1;//選択列を選択しなおす
                SkipEvent = false;
            }
        }

        #endregion

        //選択Atomが変更されたとき
        private void bindingSource_PositionChanged(object sender, System.EventArgs e)
        {
            if (SkipEvent) return;

            if (bindingSource.Position >= 0 && bindingSource.Count > 0)
                SetToInterface(dataSet.DataTableLatticePlane.Get(bindingSource.Position));
        }


    }
}

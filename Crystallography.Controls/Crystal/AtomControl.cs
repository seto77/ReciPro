using Microsoft.Scripting.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class AtomControl : UserControl
    {
        #region プロパティ, フィールド, イベントハンドラ
        DataSet.DataTableAtomDataTable table;
        public bool SkipEvent { get; set; } = false;
        public int SymmetrySeriesNumber { get; set; } = 0;

        private bool details1 = false;

        public bool AtomicPositionError
        {
            set
            {
                details1 = value;
                if (value == false)
                {
                    tableLayoutPanel1.ColumnStyles[4].SizeType = tableLayoutPanel1.ColumnStyles[7].SizeType = SizeType.Absolute;
                    tableLayoutPanel1.ColumnStyles[4].Width = tableLayoutPanel1.ColumnStyles[7].Width = 0;

                    numericalTextBoxXerr.TabStop = numericalTextBoxYerr.TabStop = numericalTextBoxZerr.TabStop = numericalTextBoxOccerr.TabStop = false;
                }
                else
                {
                    tableLayoutPanel1.ColumnStyles[1].SizeType = tableLayoutPanel1.ColumnStyles[3].SizeType = tableLayoutPanel1.ColumnStyles[4].SizeType =
                        tableLayoutPanel1.ColumnStyles[6].SizeType = tableLayoutPanel1.ColumnStyles[7].SizeType = SizeType.Percent;

                    tableLayoutPanel1.ColumnStyles[1].Width = tableLayoutPanel1.ColumnStyles[3].Width = tableLayoutPanel1.ColumnStyles[4].Width
                        = tableLayoutPanel1.ColumnStyles[6].Width = tableLayoutPanel1.ColumnStyles[7].Width = 20;

                    numericalTextBoxXerr.TabStop = numericalTextBoxYerr.TabStop = numericalTextBoxZerr.TabStop = numericalTextBoxOccerr.TabStop = true;
                }
            }
            get => details1;
        }

        private bool details2 = false;

        public bool DebyeWallerError
        {
            set
            {
                details2 = value;
                if (value == false)
                {
                    numericBoxBiso.Width = numericalTextBoxB11.Width = numericalTextBoxB12.Width =
                        numericalTextBoxB13.Width = numericalTextBoxB22.Width = numericalTextBoxB23.Width = numericalTextBoxB33.Width = 60;

                    numericalTextBoxBisoerr.Visible = numericalTextBoxB11err.Visible = numericalTextBoxB12err.Visible = numericalTextBoxB13err.Visible = numericalTextBoxB22err.Visible
                    = numericalTextBoxB23err.Visible = numericalTextBoxB33err.Visible = false;
                }
                else
                {
                    numericBoxBiso.Width = numericalTextBoxB11.Width = numericalTextBoxB12.Width =
                        numericalTextBoxB13.Width = numericalTextBoxB22.Width = numericalTextBoxB23.Width = numericalTextBoxB33.Width = 45;

                    numericalTextBoxBisoerr.Visible = numericBoxBiso.Visible =
                    numericalTextBoxB33err.Visible = numericalTextBoxB23err.Visible =
                    numericalTextBoxB22err.Visible = numericalTextBoxB13err.Visible =
                    numericalTextBoxB12err.Visible = numericalTextBoxB11err.Visible = true;
                }
            }
            get => details2;
        }

        public bool Istoropy
        {
            set
            {
                if (value)
                    radioButtonIsotoropy.Checked = true;
                else
                    radioButtonAnisotropy.Checked = true;
            }
            get => radioButtonIsotoropy.Checked;
        }

        #region 温度因子 プロパティ
        [Category("Atom")]
        public double Biso { set => numericBoxBiso.Value = value; get => numericBoxBiso.Value; }
        [Category("Atom")]
        public double BisoErr { set => numericalTextBoxBisoerr.Value = value; get => numericalTextBoxBisoerr.Value; }
        [Category("Atom")]
        public double B11 { set => numericalTextBoxB11.Value = value; get => numericalTextBoxB11.Value; }
        [Category("Atom")]
        public double B11Err { set => numericalTextBoxB11err.Value = value; get => numericalTextBoxB11err.Value; }
        [Category("Atom")]
        public double B12 { set => numericalTextBoxB12.Value = value; get => numericalTextBoxB12.Value; }
        [Category("Atom")]
        public double B12Err { set => numericalTextBoxB12err.Value = value; get => numericalTextBoxB12err.Value; }
        [Category("Atom")]
        public double B13 { set => numericalTextBoxB13.Value = value; get => numericalTextBoxB13.Value; }
        [Category("Atom")]
        public double B13Err { set => numericalTextBoxB13err.Value = value; get => numericalTextBoxB13err.Value; }
        [Category("Atom")]
        public double B22 { set => numericalTextBoxB22.Value = value; get => numericalTextBoxB22.Value; }
        [Category("Atom")]
        public double B22Err { set => numericalTextBoxB22err.Value = value; get => numericalTextBoxB22err.Value; }
        [Category("Atom")]
        public double B23 { set => numericalTextBoxB23.Value = value; get { return numericalTextBoxB23.Value; } }
        [Category("Atom")]
        public double B23Err { set { numericalTextBoxB23err.Value = value; } get { return numericalTextBoxB23err.Value; } }
        [Category("Atom")]
        public double B33 { set { numericalTextBoxB33.Value = value; } get { return numericalTextBoxB33.Value; } }
        [Category("Atom")]
        public double B33Err { set { numericalTextBoxB33err.Value = value; } get { return numericalTextBoxB33err.Value; } }
        #endregion

        #region 原子位置 プロパティ
        [Category("Atom")]
        public double X { set { numericTextBoxX.Value = value; } get { return numericTextBoxX.Value; } }

        [Category("Atom")]
        public double XErr { set { numericalTextBoxXerr.Value = value; } get { return numericalTextBoxXerr.Value; } }

        [Category("Atom")]
        public double Y { set { numericTextBoxY.Value = value; } get { return numericTextBoxY.Value; } }

        [Category("Atom")]
        public double YErr { set { numericalTextBoxYerr.Value = value; } get { return numericalTextBoxYerr.Value; } }

        [Category("Atom")]
        public double Z { set { numericTextBoxZ.Value = value; } get { return numericTextBoxZ.Value; } }

        [Category("Atom")]
        public double ZErr { set => numericalTextBoxZerr.Value = value; get => numericalTextBoxZerr.Value; }
        #endregion

        [Category("Atom")]
        public double Occ { set => numericTextBoxOcc.Value = value; get => numericTextBoxOcc.Value; }
        [Category("Atom")]
        public double OccErr { set => numericalTextBoxOccerr.Value = value; get => numericalTextBoxOccerr.Value; }
        [Category("Atom")]
        public string Label { set => textBoxLabel.Text = value; get => textBoxLabel.Text; }
        [Category("Atom")]
        public int AtomNo { set => comboBoxAtom.SelectedIndex = value - 1; get => comboBoxAtom.SelectedIndex + 1; }

        [Category("Atom")]
        public int AtomSubNoXray { set => comboBoxScatteringFactorXray.SelectedIndex = value; get => comboBoxScatteringFactorXray.SelectedIndex; }

        [Category("Atom")]
        public int AtomSubNoElectron { set => comboBoxScatteringFactorElectron.SelectedIndex = value; get => comboBoxScatteringFactorElectron.SelectedIndex; }

        private double[] isotopicComposition;
        public double[] IsotopicComposition
        {
            set
            {
                isotopicComposition = value;
                if (isotopicComposition == null || isotopicComposition.Length != AtomConstants.IsotopeAbundance[AtomNo].Count)
                    comboBoxNeutron.SelectedIndex = 0;
                else
                    comboBoxNeutron.SelectedIndex = 1;

                comboBoxNeutron_SelectedIndexChanged(new object(), new EventArgs());
            }
            get => isotopicComposition;
        }

        #region マテリアル プロパティ
        [Category("Material properties")]
        public double Ambient { get => numericBoxAmbient.Value; set => numericBoxAmbient.Value = value; }
        [Category("Material properties")]
        public double Diffusion { get => numericBoxDiffusion.Value; set => numericBoxDiffusion.Value = value; }
        [Category("Material properties")]
        public double Specular { get => numericBoxSpecular.Value; set => numericBoxSpecular.Value = value; }
        [Category("Material properties")]
        public double Shininess { get => numericBoxShininess.Value; set => numericBoxShininess.Value = value; }
        [Category("Material properties")]
        public double Emission { get => numericBoxEmission.Value; set => numericBoxEmission.Value = value; }
        [Category("Material properties")]
        public double Alpha { get => numericBoxAlpha.Value; set => numericBoxAlpha.Value = value; }
        [Category("Material properties")]
        public double Radius { get => numericBoxAtomRadius.Value; set => numericBoxAtomRadius.Value = value; }
        [Category("Material properties")]
        public Color AtomColor { get => colorControlAtomColor.Color; set => colorControlAtomColor.Color = value; }

        #endregion


        #region Tabの表示/非表示 プロパティ
        [Category("Tab")]
        public bool ElementAndPositionTabVisible { set { elementAndPositionTabVisible = value; setTabPages(); } get => elementAndPositionTabVisible; }
        private bool elementAndPositionTabVisible = true;

        [Category("Tab")]
        public bool OriginShiftVisible { set { originShiftTabVisible = value; setTabPages(); } get => originShiftTabVisible; }
        private bool originShiftTabVisible = true;

        [Category("Tab")]
        public bool DebyeWallerTabVisible { set { debyeWallerTabVisible = value; setTabPages(); } get => debyeWallerTabVisible; }
        private bool debyeWallerTabVisible = true;

        [Category("Tab")]
        public bool ScatteringFactorTabVisible { set { scatteringFactorTabVisible = value; setTabPages(); } get => scatteringFactorTabVisible; }
        private bool scatteringFactorTabVisible = true;

        [Category("Tab")]
        public bool AppearanceTabVisible { set { appearanceTabVisible = value; setTabPages(); } get => appearanceTabVisible; }
        private bool appearanceTabVisible = true;

        [Category("Tab")]
        public int SelectedTabIndex { get => tabControl.SelectedIndex; set => tabControl.SelectedIndex = value; }
        #endregion

        public event EventHandler ItemsChanged;

        #endregion プロパティ

        #region コンストラクタ
        public AtomControl()
        {
            InitializeComponent();
            table = dataSet.DataTableAtom;
            comboBoxAtom.SelectedIndex = 0;
            comboBoxNeutron.SelectedIndex = 0;
            //   toolTip.SetTooltipToUsercontrol(this);

            //なぜか一部のnumericBoxのUp/Downが消えてしまうので、対処

            numericBoxAmbient.ShowUpDown = numericBoxDiffusion.ShowUpDown = numericBoxSpecular.ShowUpDown = numericBoxShininess.ShowUpDown =
                numericBoxEmission.ShowUpDown = numericBoxAlpha.ShowUpDown = numericBoxAtomRadius.ShowUpDown = true;
        }
        #endregion

        #region タブベージの表示/非表示制御
        private void setTabPages()
        {
            tabControl.TabPages.Clear();
            if (ElementAndPositionTabVisible)
                tabControl.TabPages.Add(tabPageElementAndPosition);

            if (originShiftTabVisible)
                tabControl.TabPages.Add(tabPageOriginShift);

            if (DebyeWallerTabVisible)
                tabControl.TabPages.Add(tabPageDebyeWaller);

            if (ScatteringFactorTabVisible)
                tabControl.TabPages.Add(tabPageScatteringFactor);

            if (AppearanceTabVisible)
                tabControl.TabPages.Add(tabPageAppearance);
        }

        #endregion

        private void radioButtonIsotoropy_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanelAniso1.Visible = flowLayoutPanelAniso2.Visible = !radioButtonIsotoropy.Checked;
            flowLayoutPanelIso.Visible = radioButtonIsotoropy.Checked;
        }

        //原子番号コンボ
        private void comboBoxAtom_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (comboBoxAtom.SelectedIndex < 0) return;
            comboBoxScatteringFactorXray.Items.Clear();
            comboBoxScatteringFactorElectron.Items.Clear();

            for (int i = 0; i < AtomConstants.XrayScattering[AtomNo].Length; i++)
                comboBoxScatteringFactorXray.Items.Add(AtomConstants.XrayScattering[AtomNo][i].Method);

            for (int i = 0; i < AtomConstants.ElectronScattering[AtomNo].Length; i++)
                comboBoxScatteringFactorElectron.Items.Add(AtomConstants.ElectronScattering[AtomNo][i].Method);

            comboBoxScatteringFactorXray.SelectedIndex = 0;
            comboBoxScatteringFactorElectron.SelectedIndex = 0;
            comboBoxNeutron.SelectedIndex = 0;
            comboBoxNeutron_SelectedIndexChanged(new object(), new EventArgs());
        }

        //散乱因子を選択変更されたら
        private void comboBoxAtomSub_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            /*    AtomicScatteringFactor asf;
                for (int n = 1; n <= 211; n++)
                {
                    asf = AtomicScatteringFactor.GetCoefficientForXray(n);
                    if (asf.Methods == (string)comboBoxScatteringFactorXray.SelectedItem)
                        atomSeriesNum = n;
                }*/
        }


        private void checkBoxAtomicPositionError_CheckedChanged(object sender, EventArgs e) => AtomicPositionError = checkBoxDetailAtomicPositionError.Checked;

        private void checkBoxDebyeWallerError_CheckedChanged(object sender, EventArgs e) => DebyeWallerError = checkBoxDetailsDebyeWallerError.Checked;


        #region 中性子関連
        private void comboBoxNeutron_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEditIsotopeAbundance.Enabled = comboBoxNeutron.SelectedIndex == 1;

            richTextBoxIsotope.Clear();
            int n = 0;
            foreach (int z in AtomConstants.IsotopeAbundance[AtomNo].Keys)
            {
                richTextBoxIsotope.SelectionColor = Color.DarkBlue;
                if (richTextBoxIsotope.Text != "")
                    richTextBoxIsotope.SelectedText = ", ";

                richTextBoxIsotope.SelectionCharOffset = 3;
                richTextBoxIsotope.SelectionFont = new Font("Tahoma", 6f, FontStyle.Regular);
                richTextBoxIsotope.SelectedText = z.ToString();

                richTextBoxIsotope.SelectionCharOffset = 0;
                richTextBoxIsotope.SelectionFont = new Font("Tahoma", 9f, FontStyle.Regular);
                richTextBoxIsotope.SelectedText = AtomConstants.AtomicName(AtomNo) + ": ";

                richTextBoxIsotope.SelectionColor = Color.Black;
                if (comboBoxNeutron.SelectedIndex == 0 || isotopicComposition == null || isotopicComposition.Length != AtomConstants.IsotopeAbundance[AtomNo].Count)
                    richTextBoxIsotope.SelectedText = AtomConstants.IsotopeAbundance[AtomNo][z].ToString();
                else
                    richTextBoxIsotope.SelectedText = isotopicComposition[n++].ToString();

                richTextBoxIsotope.SelectionColor = Color.DarkBlue;
                richTextBoxIsotope.SelectionFont = new Font("Tahoma", 9f, FontStyle.Regular);
                richTextBoxIsotope.SelectedText = "%";
                //labelIsotopeAbundance.Text +=  + ":" + AtomConstants.IsotopeAbundance[AtomNo][z].ToString() + "%, ";
            }
        }

        private void buttonEditIsotopeAbundance_Click(object sender, EventArgs e)
        {
            FormIsotopeComposition formIsotopeComposition = new FormIsotopeComposition();
            formIsotopeComposition.AtomNumber = AtomNo;
            formIsotopeComposition.IsotopicComposition = isotopicComposition;
            if (formIsotopeComposition.ShowDialog() == DialogResult.OK)
                IsotopicComposition = formIsotopeComposition.IsotopicComposition;
        }
        #endregion


        #region データベース操作
        /// <summary>
        /// データベースに原子を追加する
        /// </summary>
        /// <param name="atoms"></param>
        public void Add(Atoms atoms)
        {
            if (atoms != null)
                table.Add(atoms);

            ItemsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベースに原子を追加する
        /// </summary>
        /// <param name="atoms"></param>
        public void AddRange(IEnumerable<Atoms> atoms)
        {
            foreach (var a in atoms)
                table.Add(a);
            ItemsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// i番目の原子を削除
        /// </summary>
        /// <param name="i"></param>
        public void Delete(int i)
        {
            table.Remove(i);
            ItemsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 引数の原子をi番目と入れ替え
        /// </summary>
        /// <param name="atoms"></param>
        /// <param name="i"></param>
        public void Replace(Atoms atoms, int i)
        {
            table.Replace(atoms, i);
            ItemsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 引数原子をi番目に設定し、そのMaterial Propertyをさらに引数と同じ元素に対して適用
        /// </summary>
        /// <param name="atoms"></param>
        public void ReplaceAndCopyMaterial(Atoms atoms, int i)
        {
            table.Replace(atoms, i);
            var others = dataSet.DataTableAtom.GetAll().Where(a => a.AtomicNumber == atoms.AtomicNumber);
            foreach (var a in dataSet.DataTableAtom.GetAll().Where(a => a.AtomicNumber == atoms.AtomicNumber))
                a.Material = atoms.Material;
            ItemsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベースの原子を削除する
        /// </summary>
        /// <param name="atoms"></param>
        public void Clear()
        {
            table.Rows.Clear();
            ItemsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 指定した空間群番号に従って全ての原子の情報を再設定する。
        /// </summary>
        public void ResetSymmetry(int symmetrySeriesNumber)
        {
            SymmetrySeriesNumber = symmetrySeriesNumber;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var a = table.Get(i);
                a.ResetSymmetry(SymmetrySeriesNumber);
                table.Replace(a, i);
            }
            ItemsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベース中の全ての原子を取得
        /// </summary>
        /// <returns></returns>
        public Atoms[] GetAll() => table.GetAll();


        

        #endregion


        #region Atomクラスを画面下部から生成/に表示
        /// <summary>
        /// 引数のAtomを、画面下部に表示する
        /// </summary>
        /// <param name="atoms"></param>
        public void SetToInterface(Atoms atoms)
        {
            Label = atoms.Label; AtomNo = atoms.AtomicNumber;

            AtomSubNoXray = atoms.SubNumberXray;
            AtomSubNoElectron = atoms.SubNumberElectron;
            IsotopicComposition = atoms.Isotope;

            X = atoms.X; Y = atoms.Y; Z = atoms.Z; Occ = atoms.Occ;
            XErr = atoms.X_err; YErr = atoms.Y_err; ZErr = atoms.Z_err; OccErr = atoms.Occ_err;

            Biso = atoms.Dsf.Biso; B11 = atoms.Dsf.B11; B12 = atoms.Dsf.B12; B13 = atoms.Dsf.B31; B22 = atoms.Dsf.B22; B23 = atoms.Dsf.B23; B33 = atoms.Dsf.B33;
            BisoErr = atoms.Dsf.Biso_err; B11Err = atoms.Dsf.B11_err; B12Err = atoms.Dsf.B12_err; B13Err = atoms.Dsf.B31_err; B22Err = atoms.Dsf.B22_err; B23Err = atoms.Dsf.B23_err; B33Err = atoms.Dsf.B33_err;
            Istoropy = atoms.Dsf.IsIso;

            Ambient = atoms.Ambient; Diffusion = atoms.Diffusion; Emission = atoms.Emission; Shininess = atoms.Shininess; Specular = atoms.Specular;

            Radius = atoms.Radius; Alpha = atoms.Transparency; AtomColor = Color.FromArgb(atoms.Argb);
        }


        /// <summary>
        /// 画面下部の情報から、Atomを生成する
        /// </summary>
        /// <returns></returns>
        private Atoms GetFromInterface()
        {
            var dsf = new DiffuseScatteringFactor(Istoropy, Biso, B11, B22, B33, B12, B23, B13, BisoErr, B11Err, B22Err, B33Err, B12Err, B23Err, B13Err);
            var material = new AtomMaterial(AtomColor.ToArgb(), Ambient, Diffusion, Specular, Shininess, Emission, Alpha);

            var atoms = new Atoms(Label, AtomNo, AtomSubNoXray, AtomSubNoElectron, IsotopicComposition,
                SymmetrySeriesNumber, new Vector3D(X, Y, Z), new Vector3D(XErr, YErr, ZErr), Occ, OccErr, dsf,
                material, (float)Radius);
            return atoms;
        }
        #endregion


        #region 原子追加、削除などのボタン
        //原子追加ボタン
        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            var atoms = GetFromInterface();
            if (atoms != null)
            {
                Add(atoms);
                bindingSource.Position = bindingSource.Count - 1;
            }
        }

        //原子変更ボタン
        private void buttonChange_Click(object sender, System.EventArgs e)
        {
            var pos = bindingSource.Position;
            if (pos >= 0)
            {
                Replace(GetFromInterface(), pos);
                bindingSource.Position = pos;
            }
        }

        //編集内容を同種の元素にすべて適用する
        private void buttonChangeToSameElement_Click(object sender, EventArgs e)
        {
            var pos = bindingSource.Position;
            if (pos >= 0)
            {
                ReplaceAndCopyMaterial(GetFromInterface(), pos);
                bindingSource.Position = pos;
            }
        }

        //原子削除ボタン
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
        private void buttonUp_Click(object sender, EventArgs e)
        {
            int n = bindingSource.Position;
            if (n <= 0) return;
            table.MoveItem(n, n - 1);
            bindingSource.Position = n - 1;
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            int n = bindingSource.Position;
            if (n >= bindingSource.Count - 1) return;
            table.MoveItem(n, n + 1);
            bindingSource.Position = n + 1;
        }

        #endregion


        //選択Atomが変更されたとき
        private void bindingSource_PositionChanged(object sender, System.EventArgs e)
        {
            if (SkipEvent) return;

            if (bindingSource.Position >= 0 && bindingSource.Count > 0)
                SetToInterface(dataSet.DataTableAtom.Get(bindingSource.Position));
        }

        private void listBoxAtoms_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                /*Atoms atoms;
                if (listBoxAtoms.SelectedIndex >= 0)
                    atoms = (Atoms)listBoxAtoms.SelectedItem;
                else
                    return;
                string str = "No.\tx\t y\t  z\r\n";
                for (int i = 0; i < atoms.Atom.Count; i++)
                    str += (i + 1).ToString() + "\t" + Atoms.GetStringFromDouble(atoms.Atom[i].X) + "\t " + Atoms.GetStringFromDouble(atoms.Atom[i].Y) + "\t  " + Atoms.GetStringFromDouble(atoms.Atom[i].Z) + "\r\n";

                this.toolTip.SetToolTip(this.listBoxAtoms, str); ;
                */
                /*str = "";
                for (int j = 0; j < listBoxAtoms.Items.Count; j++)
                {
                    atoms = (Atoms)listBoxAtoms.Items[j];
                    for (int i = 0; i < atoms.Atom.Count; i++)
                    {
                        string element = atoms.ElementName.Substring(atoms.ElementName.IndexOf(' ')+1); ;
                        str += element + "," + Atoms.GetStringFromDouble(atoms.Atom[i].X) + "," + Atoms.GetStringFromDouble(atoms.Atom[i].Y) + "," + Atoms.GetStringFromDouble(atoms.Atom[i].Z) + "\r\n";
                    }
                }
                Clipboard.SetDataObject(str, false);*/
            }

            /*
            else if (e.Button == MouseButtons.Right)
            {
                if (listBoxAtoms.SelectedIndex == listBoxAtoms.IndexFromPoint(new Point(e.X, e.Y)))
                {
                    formAtomDetailedInfo = new FormAtomDetailedInfo
                    {
                        Atoms = (Atoms)listBoxAtoms.SelectedItem,
                        Location = listBoxAtoms.PointToScreen(new Point(e.X, e.Y))
                    };

                    formAtomDetailedInfo.ShowDialog();
                }
            }
            */
        }

        private void listBoxAtoms_MouseLeave(object sender, EventArgs e)
        {
            //  this.toolTip.SetToolTip(this.listBoxAtoms, "displya element, position, symmetry seeting for each atoms.");
        }

        private void buttonOriginShift_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var shift = button.Name.Contains("Custom") ?
                new Vector3DBase(numericBoxOriginShiftX.Value, numericBoxOriginShiftY.Value, numericBoxOriginShiftZ.Value) :
                new Vector3DBase((button.Tag as string).Split().Select(s => s.ToDouble()).ToArray()) * (radioButtonOriginShiftPlus.Checked ? 1 : -1);

            SkipEvent = true;
            foreach (var atoms in GetAll())
            {
                atoms.X += shift.X;
                atoms.Y += shift.Y;
                atoms.Z += shift.Y;
                atoms.ResetSymmetry(SymmetrySeriesNumber);
            }
            SkipEvent = false;
            bindingSource_PositionChanged(sender, e);

            ItemsChanged(this, e);

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonChangeToSameElement.Visible = tabControl.SelectedTab == tabPageAppearance;
        }


    }
}
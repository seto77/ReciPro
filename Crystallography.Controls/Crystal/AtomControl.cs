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

        public Crystal Crystal
        {
            get => crystal; set
            {
                crystal = value;
                
                if (crystal != null)
                {
                    table.Clear();
                    AddRange(Crystal.Atoms);
                    //なぜかEnabledカラムのVisibleが予期せず変わってしまうことがあるので、appearanceTabVisibleを使う.
                    dataGridView.Columns["enabledColumn"].Visible = appearanceTabVisible;
                }
            }
        }
        private Crystal crystal = null;

        public int SymmetrySeriesNumber { get=> crystal !=null ? crystal.SymmetrySeriesNumber: 0;} 

        DataSet.DataTableAtomDataTable table;
        public bool SkipEvent { get; set; } = false;
        

        
        public bool AtomicPositionError
        {
            set
            {
                atmicPositionError = value;
                if (value == false)
                {
                    tableLayoutPanel1.ColumnStyles[4].SizeType = tableLayoutPanel1.ColumnStyles[7].SizeType = SizeType.Absolute;
                    tableLayoutPanel1.ColumnStyles[4].Width = tableLayoutPanel1.ColumnStyles[7].Width = 0;

                    numericBoxXerr.TabStop = numericBoxYerr.TabStop = numericBoxZerr.TabStop = numericBoxOccerr.TabStop = false;
                }
                else
                {
                    tableLayoutPanel1.ColumnStyles[1].SizeType = tableLayoutPanel1.ColumnStyles[3].SizeType = tableLayoutPanel1.ColumnStyles[4].SizeType =
                        tableLayoutPanel1.ColumnStyles[6].SizeType = tableLayoutPanel1.ColumnStyles[7].SizeType = SizeType.Percent;

                    tableLayoutPanel1.ColumnStyles[1].Width = tableLayoutPanel1.ColumnStyles[3].Width = tableLayoutPanel1.ColumnStyles[4].Width
                        = tableLayoutPanel1.ColumnStyles[6].Width = tableLayoutPanel1.ColumnStyles[7].Width = 20;

                    numericBoxXerr.TabStop = numericBoxYerr.TabStop = numericBoxZerr.TabStop = numericBoxOccerr.TabStop = true;
                }
            }
            get => atmicPositionError;
        }
        private bool atmicPositionError = false;

        public bool DebyeWallerError
        {
            set
            {
                debyeWallerError = value;
                if (value == false)
                {
                    //numericBoxBiso.Width = numericBoxB11.Width = numericBoxB12.Width =
                    //    numericBoxB13.Width = numericBoxB22.Width = numericBoxB23.Width = numericBoxB33.Width = 60;

                    numericBoxBisoerr.Visible = numericBoxB11err.Visible = numericBoxB12err.Visible = numericBoxB13err.Visible = numericBoxB22err.Visible
                    = numericBoxB23err.Visible = numericBoxB33err.Visible = false;
                }
                else
                {
                    //numericBoxBiso.Width = numericBoxB11.Width = numericBoxB12.Width =
                    //    numericBoxB13.Width = numericBoxB22.Width = numericBoxB23.Width = numericBoxB33.Width = 45;

                    numericBoxBisoerr.Visible = numericBoxBiso.Visible =
                    numericBoxB33err.Visible = numericBoxB23err.Visible =
                    numericBoxB22err.Visible = numericBoxB13err.Visible =
                    numericBoxB12err.Visible = numericBoxB11err.Visible = true;
                }
            }
            get => debyeWallerError;
        }
        private bool debyeWallerError = false;

        public bool UseIsotropy
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

        public bool UseTypeU
        {
            set
            {
                if (value)
                    radioButtonDebyeWallerTypeU.Checked = true;
                else
                    radioButtonDebyeWallerTypeB.Checked = true;
            }
            get => radioButtonDebyeWallerTypeU.Checked;
        }


        #region 温度因子 プロパティ
        [Category("Atom")]
        public double Iso { set => numericBoxBiso.Value = value; get => numericBoxBiso.Value; }
        [Category("Atom")]
        public double IsoErr { set => numericBoxBisoerr.Value = value; get => numericBoxBisoerr.Value; }
        [Category("Atom")]
        public double Aniso11 { set => numericBoxB11.Value = value; get => numericBoxB11.Value; }
        [Category("Atom")]
        public double Aniso11Err { set => numericBoxB11err.Value = value; get => numericBoxB11err.Value; }
        [Category("Atom")]
        public double Aniso12 { set => numericBoxB12.Value = value; get => numericBoxB12.Value; }
        [Category("Atom")]
        public double Aniso12Err { set => numericBoxB12err.Value = value; get => numericBoxB12err.Value; }
        [Category("Atom")]
        public double Aniso13 { set => numericBoxB13.Value = value; get => numericBoxB13.Value; }
        [Category("Atom")]
        public double Aniso13Err { set => numericBoxB13err.Value = value; get => numericBoxB13err.Value; }
        [Category("Atom")]
        public double Aniso22 { set => numericBoxB22.Value = value; get => numericBoxB22.Value; }
        [Category("Atom")]
        public double Aniso22Err { set => numericBoxB22err.Value = value; get => numericBoxB22err.Value; }
        [Category("Atom")]
        public double Aniso23 { set => numericBoxB23.Value = value; get { return numericBoxB23.Value; } }
        [Category("Atom")]
        public double Aniso23Err { set { numericBoxB23err.Value = value; } get { return numericBoxB23err.Value; } }
        [Category("Atom")]
        public double Aniso33 { set { numericBoxB33.Value = value; } get { return numericBoxB33.Value; } }
        [Category("Atom")]
        public double Aniso33Err { set { numericBoxB33err.Value = value; } get { return numericBoxB33err.Value; } }
        #endregion

        #region 原子位置 プロパティ
        [Category("Atom")]
        public double X { set { numericBoxX.Value = value; } get { return numericBoxX.Value; } }

        [Category("Atom")]
        public double XErr { set { numericBoxXerr.Value = value; } get { return numericBoxXerr.Value; } }

        [Category("Atom")]
        public double Y { set { numericBoxY.Value = value; } get { return numericBoxY.Value; } }

        [Category("Atom")]
        public double YErr { set { numericBoxYerr.Value = value; } get { return numericBoxYerr.Value; } }

        [Category("Atom")]
        public double Z { set { numericBoxZ.Value = value; } get { return numericBoxZ.Value; } }

        [Category("Atom")]
        public double ZErr { set => numericBoxZerr.Value = value; get => numericBoxZerr.Value; }
        #endregion

        [Category("Atom")]
        public double Occ { set => numericBoxOcc.Value = value; get => numericBoxOcc.Value; }
        [Category("Atom")]
        public double OccErr { set => numericBoxOccerr.Value = value; get => numericBoxOccerr.Value; }
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
                if (isotopicComposition == null || isotopicComposition.Length != AtomStatic.IsotopeAbundance[AtomNo].Count)
                    comboBoxNeutron.SelectedIndex = 0;
                else
                    comboBoxNeutron.SelectedIndex = 1;

                comboBoxNeutron_SelectedIndexChanged(new object(), new EventArgs());
            }
            get => isotopicComposition;
        }

        #region マテリアル プロパティ
        [Category("Material properties")]
        public float Ambient { get => (float)numericBoxAmbient.Value; set => numericBoxAmbient.Value = value; }
        [Category("Material properties")]
        public float Diffusion { get => (float)numericBoxDiffusion.Value; set => numericBoxDiffusion.Value = value; }
        [Category("Material properties")]
        public float Specular { get => (float)numericBoxSpecular.Value; set => numericBoxSpecular.Value = value; }
        [Category("Material properties")]
        public float Shininess { get => (float)numericBoxShininess.Value; set => numericBoxShininess.Value = value; }
        [Category("Material properties")]
        public float Emission { get => (float)numericBoxEmission.Value; set => numericBoxEmission.Value = value; }
        [Category("Material properties")]
        public float Alpha { get => (float)numericBoxAlpha.Value; set => numericBoxAlpha.Value = value; }
        [Category("Material properties")]
        public double Radius { get => numericBoxAtomRadius.Value; set => numericBoxAtomRadius.Value = value; }
        [Category("Material properties")]
        public Color AtomColor { get => colorControlAtomColor.Color; set => colorControlAtomColor.Color = value; }
        [Category("Material properties")]
        public bool ShowLabel { get => checkBoxShowLabel.Checked; set => checkBoxShowLabel.Checked = value; }


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

        /// <summary>
        /// 原子のパラメータが変更された時のイベント
        /// </summary>
        public event EventHandler ItemsChanged;

        /// <summary>
        /// GLEnabledチェックが変更された時だけのイベント. (今のところFormStructureだけが受け取る)
        /// </summary>
        public event EventHandler GLEnableChanged;

        #endregion プロパティ

        #region コンストラクタ
        public AtomControl()
        {
            InitializeComponent();
            SkipEvent = true;
            table = dataSet.DataTableAtom;
            comboBoxAtom.SelectedIndex = 0;
            comboBoxNeutron.SelectedIndex = 0;
            //   toolTip.SetTooltipToUsercontrol(this);

            //なぜか一部のnumericBoxのUp/Downが消えてしまうので、対処
            numericBoxAmbient.ShowUpDown = numericBoxDiffusion.ShowUpDown = numericBoxSpecular.ShowUpDown = numericBoxShininess.ShowUpDown =
                numericBoxEmission.ShowUpDown = numericBoxAlpha.ShowUpDown = numericBoxAtomRadius.ShowUpDown = true;

            dataGridView.Columns["enabledColumn"].Visible = false;
            SkipEvent = false;
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

            labelDimension.Text = radioButtonDebyeWallerTypeB.Checked && radioButtonAnisotropy.Checked ? "None" : "Å²";
        }

        private void radioButtonDebyeWallerTypeU_CheckedChanged(object sender, EventArgs e)
        {
            var U = radioButtonDebyeWallerTypeU.Checked;
            numericBoxBiso.HeaderText = U ? "Uiso" : "Biso";
            numericBoxB11.HeaderText = U ? "U11" : "B11";
            numericBoxB22.HeaderText = U ? "U22" : "B22";
            numericBoxB33.HeaderText = U ? "U33" : "B33";
            numericBoxB12.HeaderText = U ? "U12" : "B12";
            numericBoxB23.HeaderText = U ? "U23" : "B23";
            numericBoxB13.HeaderText = U ? "U13" : "B13";

            labelDimension.Text = radioButtonDebyeWallerTypeB.Checked && radioButtonAnisotropy.Checked ? "None" : "Å²";

        }

        //原子番号コンボ
        private void comboBoxAtom_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (SkipEvent) return;
            if (comboBoxAtom.SelectedIndex < 0) return;
            comboBoxScatteringFactorXray.Items.Clear();
            comboBoxScatteringFactorElectron.Items.Clear();

            for (int i = 0; i < AtomStatic.XrayScattering[AtomNo].Length; i++)
                comboBoxScatteringFactorXray.Items.Add(AtomStatic.XrayScattering[AtomNo][i].Method);

            for (int i = 0; i < AtomStatic.ElectronScattering[AtomNo].Length; i++)
                comboBoxScatteringFactorElectron.Items.Add(AtomStatic.ElectronScattering[AtomNo][i].Method);

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
            if (SkipEvent) return;

            buttonEditIsotopeAbundance.Enabled = comboBoxNeutron.SelectedIndex == 1;

            richTextBoxIsotope.Clear();
            int n = 0;
            foreach (int z in AtomStatic.IsotopeAbundance[AtomNo].Keys)
            {
                richTextBoxIsotope.SelectionColor = Color.DarkBlue;
                if (richTextBoxIsotope.Text != "")
                    richTextBoxIsotope.SelectedText = ", ";

                richTextBoxIsotope.SelectionCharOffset = 3;
                richTextBoxIsotope.SelectionFont = new Font("Tahoma", 6f, FontStyle.Regular);
                richTextBoxIsotope.SelectedText = z.ToString();

                richTextBoxIsotope.SelectionCharOffset = 0;
                richTextBoxIsotope.SelectionFont = new Font("Tahoma", 9f, FontStyle.Regular);
                richTextBoxIsotope.SelectedText = AtomStatic.AtomicName(AtomNo) + ": ";

                richTextBoxIsotope.SelectionColor = Color.Black;
                if (comboBoxNeutron.SelectedIndex == 0 || isotopicComposition == null || isotopicComposition.Length != AtomStatic.IsotopeAbundance[AtomNo].Count)
                    richTextBoxIsotope.SelectedText = AtomStatic.IsotopeAbundance[AtomNo][z].ToString();
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
            var formIsotopeComposition = new FormIsotopeComposition { AtomNumber = AtomNo, IsotopicComposition = isotopicComposition };
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
            if (atoms != null)
            {
                SkipEvent = true;
                foreach (var a in atoms)
                    table.Add(a);
                SkipEvent = false;
                ItemsChanged?.Invoke(this, new EventArgs());
                bindingSource_PositionChanged(new object(), new EventArgs());
            }
            
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
            { 
                a.Texture = atoms.Texture;
                a.Radius = atoms.Radius;
                a.Argb = atoms.Argb;
                a.ShowLabel = atoms.ShowLabel;
            }
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
            //SymmetrySeriesNumber = symmetrySeriesNumber;
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

            #region 原子位置、占有率
            X = atoms.X; XErr = atoms.X_err;
            Y = atoms.Y; YErr = atoms.Y_err;
            Z = atoms.Z; ZErr = atoms.Z_err;
            Occ = atoms.Occ; OccErr = atoms.Occ_err;
            #endregion


            #region 温度因子関係
            UseIsotropy = atoms.Dsf.UseIso;
            UseTypeU = atoms.Dsf.OriginalType == DiffuseScatteringFactor.Type.U;

         
            Iso = UseTypeU ? atoms.Dsf.Uiso * 100 : atoms.Dsf.Biso * 100;
            Aniso11 = UseTypeU ? atoms.Dsf.U11 * 100 : atoms.Dsf.B11;
            Aniso12 = UseTypeU ? atoms.Dsf.U12 * 100 : atoms.Dsf.B12;
            Aniso13 = UseTypeU ? atoms.Dsf.U31 * 100 : atoms.Dsf.B31;
            Aniso22 = UseTypeU ? atoms.Dsf.U22 * 100 : atoms.Dsf.B22;
            Aniso23 = UseTypeU ? atoms.Dsf.U23 * 100 : atoms.Dsf.B23;
            Aniso33 = UseTypeU ? atoms.Dsf.U31 * 100 : atoms.Dsf.B31;
          
            IsoErr = UseTypeU ? atoms.Dsf.Uiso_err * 100 : atoms.Dsf.Biso_err * 100;
            Aniso11Err = UseTypeU ? atoms.Dsf.U11_err * 100 : atoms.Dsf.B11_err;
            Aniso12Err = UseTypeU ? atoms.Dsf.U12_err * 100 : atoms.Dsf.B12_err;
            Aniso13Err = UseTypeU ? atoms.Dsf.U31_err * 100 : atoms.Dsf.B31_err;
            Aniso22Err = UseTypeU ? atoms.Dsf.U22_err * 100 : atoms.Dsf.B22_err;
            Aniso23Err = UseTypeU ? atoms.Dsf.U23_err * 100 : atoms.Dsf.B23_err;
            Aniso33Err = UseTypeU ? atoms.Dsf.U31_err * 100 : atoms.Dsf.B31_err;

            #endregion

            #region Appearance関連

            Ambient = atoms.Ambient;
            Diffusion = atoms.Diffusion;
            Emission = atoms.Emission;
            Shininess = atoms.Shininess;
            Specular = atoms.Specular;

            Radius = atoms.Radius;
            AtomColor = Color.FromArgb(atoms.Argb);
            Alpha = Color.FromArgb(atoms.Argb).A / 255f;

            ShowLabel = atoms.ShowLabel;

            #endregion
        }


        /// <summary>
        /// 画面下部の情報から、Atomを生成する
        /// </summary>
        /// <returns></returns>
        private Atoms GetFromInterface()
        {

            var aniso = UseTypeU ?
                new[] { Aniso11 / 100, Aniso22 / 100, Aniso33 / 100, Aniso12 / 100, Aniso23 / 100, Aniso13 / 100 } :
                new[] { Aniso11, Aniso22, Aniso33, Aniso12, Aniso23, Aniso13 };

            var anisoErr = UseTypeU ?
                new[] { Aniso11Err / 100, Aniso22Err / 100, Aniso33Err / 100, Aniso12Err / 100, Aniso23Err / 100, Aniso13Err / 100 } :
                new[] { Aniso11Err, Aniso22Err, Aniso33Err, Aniso12Err, Aniso23Err, Aniso13Err };

            var dsf = new DiffuseScatteringFactor(UseTypeU ? DiffuseScatteringFactor.Type.U : DiffuseScatteringFactor.Type.B,
                UseIsotropy, Iso / 100, IsoErr / 100, aniso, anisoErr, Crystal.CellValue);

            var material = new Material(AtomColor.ToArgb(), (Ambient, Diffusion, Specular, Shininess, Emission), Alpha);

            var atoms = new Atoms(Label, AtomNo, AtomSubNoXray, AtomSubNoElectron, IsotopicComposition,
                SymmetrySeriesNumber, new Vector3D(X, Y, Z), new Vector3D(XErr, YErr, ZErr), Occ, OccErr, dsf,
                material, (float)Radius, true, ShowLabel);
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
            var atomArray = GetAll();
            for (int i = 0; i < atomArray.Length; i++)
            //foreach(var atoms in GetAll())
            {
                var atoms = atomArray[i];
                atoms = Deep.Copy(atoms);
                atoms.X += shift.X;
                atoms.Y += shift.Y;
                atoms.Z += shift.Y;
                atoms.ResetSymmetry(SymmetrySeriesNumber);
                table.Replace(atoms, i);
            }
            SkipEvent = false;
            bindingSource_PositionChanged(sender, e);

            ItemsChanged?.Invoke(this, e);

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonChangeToSameElement.Visible = tabControl.SelectedTab == tabPageAppearance;
        }

        private void dataGridViewAtom_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
               Crystal.Atoms[e.RowIndex].GLEnabled= table.Get(e.RowIndex).GLEnabled 
                    = (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                GLEnableChanged?.Invoke(this, new EventArgs());
            }
        }

        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //チェックボックスが変わると即座に反映させる
            var x = dataGridView.CurrentCellAddress.X;
            if ((x == 0) && dataGridView.IsCurrentCellDirty)
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);//コミットする
        }
    }
}
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    [TypeConverter(typeof(DefinitionOrderTypeConverter))]
    public partial class CrystalControl : UserControl
    {
        #region プロパティ、フィールド、イベントハンドラ

        public bool SkipEvent { get; set; } = false;

        public bool SymmetryInformationVisible { set => formSymmetryInformation.Visible = value; get => formSymmetryInformation.Visible; }

        public bool ScatteringFactorVisible { set { formScatteringFactor.Visible = value; } get => formScatteringFactor.Visible; }

        public bool StrainControlVisible { get => formStrain.Visible; }

        public int SymmetrySeriesNumber { get => symmetryControl.SymmetrySeriesNumber; set => symmetryControl.SymmetrySeriesNumber = value; }

        #region Tabページの表示/非表示プロパティ

        public bool VisibleBasicInfoTab { set { visibleBasicInfoTab = value; setTabPages(); } get => visibleBasicInfoTab; }
        private bool visibleBasicInfoTab = true;

        public bool VisibleElasticityTab { set { visibleElasticityTab = value; setTabPages(); } get { return visibleElasticityTab; } }
        private bool visibleElasticityTab = true;

        public bool VisibleAtomTab { set { visibleAtomTab = value; setTabPages(); } get => visibleAtomTab; }
        private bool visibleAtomTab = true;

        public bool VisibleBondsPolyhedraTab { set { visibleBondsPolyhedraTab = value; setTabPages(); } get => visibleBondsPolyhedraTab; }
        private bool visibleBondsPolyhedraTab = true;

        public bool VisibleReferenceTab { set { visibleReferenceTab = value; setTabPages(); } get => visibleReferenceTab; }
        private bool visibleReferenceTab = true;

        public bool VisibleEOSTab { set { visibleEOSTab = value; setTabPages(); } get => visibleEOSTab; }
        private bool visibleEOSTab = true;

        public bool VisibleStressStrainTab { set { visibleStressStrainTab = value; setTabPages(); } get => visibleStressStrainTab; }
        private bool visibleStressStrainTab = false;

        public bool VisiblePolycrystallineTab { set { visiblePolycrystallineTab = value; setTabPages(); } get => visiblePolycrystallineTab; }
        private bool visiblePolycrystallineTab = false;

        public bool VisibleBoundTab { set { visibleBoundTab = value; setTabPages(); } get => visibleBoundTab; }
        private bool visibleBoundTab = false;
        public bool VisibleLatticePlaneTab { set { visibleLatticePlaneTab = value; setTabPages(); } get => visibleLatticePlaneTab; }
        private bool visibleLatticePlaneTab = false;

        private void setTabPages()
        {
            tabControl.TabPages.Clear();
            if (VisibleBasicInfoTab) tabControl.TabPages.Add(tabPageBasicInfo);
            if (VisibleAtomTab) tabControl.TabPages.Add(tabPageAtom);
            if (VisiblePolycrystallineTab) tabControl.TabPages.Add(tabPagePolycrystalline);
            if (VisibleBoundTab) tabControl.TabPages.Add(tabPageBounds);
            if (VisibleLatticePlaneTab) tabControl.TabPages.Add(tabPageLatticePlane);
            if (VisibleReferenceTab) tabControl.TabPages.Add(tabPageReference);
            if (VisibleEOSTab) tabControl.TabPages.Add(tabPageEOS);
            if (VisibleElasticityTab) tabControl.TabPages.Add(tabPageElasticity);
            if (VisibleStressStrainTab) tabControl.TabPages.Add(tabPageStrainStress);
            if (VisiblePolycrystallineTab) tabControl.TabPages.Add(tabPagePolycrystalline);
        }

        #endregion Tabページの表示/非表示プロパティ

        public Crystal Crystal
        {
            set
            {
                crystal = value;
                if (crystal != null)
                {
                    Enabled = !crystal.FlexibleMode;
                    checkSpecialNumber();

                    SetToInterface();
                    //原子位置チェック (strain controlで選択した後、原子位置が変になってしまう問題の修正. 2017/05/29)
                    if (crystal.ChemicalFormulaZ == 1)
                    {
                        for (int i = 0; i < crystal.Atoms.Length; i++)
                            crystal.Atoms[i].ResetSymmetry(SymmetrySeriesNumber);
                        crystal.GetFormulaAndDensity();

                        SetToInterface();
                    }

                    CrystalChanged?.Invoke(this,new EventArgs());
                }
            }
            get => crystal;
        }
        private Crystal crystal;


        public (double A, double B, double C, double Alpha, double Beta, double Gamma) CellConstants
        { get => symmetryControl.CellConstants; set => symmetryControl.CellConstants = value; }

        public int DefaultTabNumber { set => tabControl.SelectedIndex = value; get => tabControl.SelectedIndex; }

        public event EventHandler CrystalChanged;

        public FormScatteringFactor formScatteringFactor;
        public FormSymmetryInformation formSymmetryInformation;
        private FormAtomDetailedInfo formAtomDetailedInfo;

        public FormStrain formStrain;


        //候補の数値
        private double[] rationalNumbers = new double[] { 1.0 / 12.0, 1.0 / 8.0, 1.0 / 6.0, 1.0 / 4.0, 1.0 / 3.0, 3.0 / 8.0, 5.0 / 12.0, 1.0 / 2.0, 7.0 / 12.0, 5.0 / 8.0, 2.0 / 3.0, 3.0 / 4.0, 5.0 / 6.0, 7.0 / 8.0, 11.0 / 12.0 };

        #endregion

        #region コンストラクタ、Loadイベント
        public CrystalControl()
        {
            InitializeComponent();

            formScatteringFactor = new FormScatteringFactor { CrystalControl = this, Visible = false };
            formSymmetryInformation = new FormSymmetryInformation { CrystalControl = this, Visible = false };
            formStrain = new FormStrain { CrystalControl = this, Visible = false };
        }
        
        private void CrystalForm_Load(object sender, System.EventArgs e)
        {
            textBoxTitle.Size = new Size(tabPageReference.Width - textBoxTitle.Location.X - 2, tabPageReference.Height - textBoxTitle.Location.Y - 2);
            formScatteringFactor.VisibleChanged += new EventHandler(formScatteringFactor_VisibleChanged);
            formSymmetryInformation.VisibleChanged += new EventHandler(formSymmetryInformation_VisibleChanged);
        }
        #endregion

        #region イベントハンドラ

        public event EventHandler ScatteringFactor_VisibleChanged;
        public event EventHandler SymmetryInformation_VisibleChanged;

        #endregion

        private void formScatteringFactor_VisibleChanged(object sender, EventArgs e) 
            => ScatteringFactor_VisibleChanged?.Invoke(sender, e);
        private void formSymmetryInformation_VisibleChanged(object sender, EventArgs e)
            => SymmetryInformation_VisibleChanged?.Invoke(sender, e);

        private void checkSpecialNumber()
        {
            //三方あるいは六方
            // if (crystal.Symmetry.SeriesNumber < 430 && crystal.Symmetry.SeriesNumber > 488) return;
            for (int i = 0; i < crystal.Atoms.Length; i++)
            {
                var pos = new Vector3D(
                    ((int)Math.Round(crystal.Atoms[i].X * 1000000)) / 1000000.0,
                    ((int)Math.Round(crystal.Atoms[i].Y * 1000000)) / 1000000.0,
                    ((int)Math.Round(crystal.Atoms[i].Z * 1000000)) / 1000000.0);
                var occ = ((int)Math.Round(crystal.Atoms[i].Occ * 1000000)) / 1000000.0;

                //bool flag = false;
                for (int j = 0; j < rationalNumbers.Length; j++)
                {
                    if (Math.Abs(rationalNumbers[j] - pos.X) < 0.0001) { pos.X = rationalNumbers[j]; }
                    if (Math.Abs(rationalNumbers[j] - pos.Y) < 0.0001) { pos.Y = rationalNumbers[j]; }
                    if (Math.Abs(rationalNumbers[j] - pos.Z) < 0.0001) { pos.Z = rationalNumbers[j]; }
                    if (Math.Abs(rationalNumbers[j] - occ) < 0.0001) { occ = rationalNumbers[j]; }
                }
                //if (flag)
                {
                    //  Atoms temp = SymmetryStatic.GetEquivalentAtomsPosition(pos, crystal.SymmetrySeriesNumber);
                    //  if (temp.Atom.Count != crystal.Atoms[i].Atom.Count)
                    {
                        Atoms a = crystal.Atoms[i];
                        crystal.Atoms[i] = new Atoms(a.Label, a.AtomicNumber, a.SubNumberXray, a.SubNumberElectron, a.Isotope, a.SymmetrySeriesNumber,
                            pos, new Vector3D(a.X_err, a.Y_err, a.Z_err), occ, a.Occ_err, a.Dsf, new AtomMaterial(a.Argb, a.Ambient, a.Diffusion, a.Specular, a.Shininess, a.Emission, a.Transparency), a.Radius);
                        crystal.GetFormulaAndDensity();
                    }
                }
            }
        }

        #region Crystalクラスを画面下部 から生成/にセット


        /// <summary>
        /// Formに入力された内容からからCrystalを生成する
        /// </summary>
        public void GenerateFromInterface()
        {
            if (SkipEvent) return;
            SkipEvent = true;

            var cell = symmetryControl.CellConstants;

            if (cell.A < 0 || cell.B < 0 || cell.C < 0 || cell.Alpha > Math.PI || cell.Beta > Math.PI || cell.Gamma > Math.PI)
            {
                SkipEvent = false;
                MessageBox.Show("Input valid cell constants");
                return;
            }

            //対称性が変更されているかもしれないので原子も改めて設定しなおす。
            atomControl.ResetSymmetry(SymmetrySeriesNumber);

            var rot = crystal != null ? crystal.RotationMatrix : new Matrix3D();

            crystal = new Crystal(
                symmetryControl.CellConstants, symmetryControl.CellConstantsErr,
                SymmetrySeriesNumber, textBoxName.Text, colorControl.Color, rot, atomControl.GetAll(),
                (textBoxMemo.Text, textBoxAuthor.Text, textBoxJournal.Text, textBoxTitle.Text),
                bondControl.GetAll(), boundControl.GetAll(), latticePlaneControl.GetAll());

            crystal.ElasticStiffness = elasticityControl1.Stiffness.ToArray();

            #region EOS関連データ （Crystalのコンストラクタに入れた方がいいかも）
            crystal.EOSCondition.A = numericBoxEOS_A.Value;
            crystal.EOSCondition.B = numericBoxEOS_B.Value;
            crystal.EOSCondition.C = numericBoxEOS_C.Value;
            crystal.EOSCondition.CellVolume0 = numericBoxEOS_V0perCell.Value;
            crystal.EOSCondition.Gamma0 = numericBoxEOS_Gamma0.Value;
            crystal.EOSCondition.K0 = numericBoxEOS_KT0.Value;
            crystal.EOSCondition.KperT = numericBoxEOS_KperT.Value;
            crystal.EOSCondition.Kprime0 = numericBoxEOS_KprimeT0.Value;
            crystal.EOSCondition.Q = numericBoxEOS_Q.Value;
            crystal.EOSCondition.T0 = numericBoxEOS_T0.Value;
            crystal.EOSCondition.ThermalPressureApproach = radioButtonMieGruneisen.Checked ? ThermalPressure.MieGruneisen : ThermalPressure.T_dependence_BM;
            crystal.EOSCondition.IsothermalPressureApproach = radioButtonBirchMurnaghan.Checked ? IsothermalPressure.Birch_Murnaghan : IsothermalPressure.Vinet;

            crystal.EOSCondition.Theta0 = numericBoxEOS_Theta0.Value;
            int n = 0;
            for (int i = 0; i < crystal.Atoms.Length; i++)
                n += crystal.Atoms[i].Atom.Count;
            crystal.EOSCondition.Z = crystal.ChemicalFormulaZ;
            if (crystal.ChemicalFormulaZ > 0)
                crystal.EOSCondition.N = n / crystal.ChemicalFormulaZ;
            crystal.DoesUseEOS = checkBoxUseEOS.Checked;
            crystal.EOSCondition.Note = textBoxEOS_Note.Text;
            crystal.EOSCondition.Temperature = numericalTextBoxTemperature.Value;
            #endregion


            #region PolyCrystallineProperty
            crystal.AngleResolution = (double)numericUpDownAngleResolution.Value;
            crystal.SubDivision = (int)numericUpDownAngleSubDivision.Value;
            crystal.GrainSize = (double)numericUpDownCrystallineSize.Value;
            if (poleFigureControl.Crystal != null)
                crystal.Crystallites = poleFigureControl.Crystal.Crystallites;
            #endregion

            SkipEvent = false;
            SetToInterface(false);

            CrystalChanged?.Invoke(this, new EventArgs());
        }


        /// <summary>
        /// 現在のCrystalによってFormのテキストボックスなどを設定する。
        /// </summary>
        /// <param name="ChangeCellParameter">コントロールのCellParaterを変化させた時はFalse</param>
        public void SetToInterface(bool ChangeCellParameter = true)
        {
            if (SkipEvent) return;
            SkipEvent = true;

            colorControl.Color = Color.FromArgb(Crystal.Argb);
            textBoxName.Text = Crystal.Name;
            textBoxMemo.Text = Crystal.Note;

            textBoxAuthor.Text = Crystal.PublAuthorName;
            textBoxJournal.Text = Crystal.Journal;
            textBoxFormula.Text = Crystal.ChemicalFormulaSum;
            textBoxTitle.Text = Crystal.PublSectionTitle;

            numericBoxDensity.Value = Crystal.Density;
            numericBoxVolume.Value = Crystal.Volume * 1000;
            numericBoxZnumber.Value = Crystal.ChemicalFormulaZ;

            SymmetrySeriesNumber = Crystal.SymmetrySeriesNumber;//SymmetrySeriesNumberをフィールドからプロパティに変更。set{}の所でコンボボックスをセットする。(20170526)

            if (ChangeCellParameter)
            {
                symmetryControl.CellConstants = (Crystal.A, Crystal.B, Crystal.C, Crystal.Alpha, Crystal.Beta, Crystal.Gamma);
                symmetryControl.CellConstantsErr = (Crystal.A_err, Crystal.B_err, Crystal.C_err, Crystal.Alpha_err, Crystal.Beta_err, Crystal.Gamma_err);
            }

            //Atomsコントロール
            atomControl.SymmetrySeriesNumber = SymmetrySeriesNumber;
            atomControl.Clear();
            atomControl.AddRange(Crystal.Atoms);


            //Bondコントロール
            bondControl.ElementList = Crystal.Atoms.Select(a => a.ElementName).ToArray();//Bonds&Polyhedra中のコンボボックスの変更
            bondControl.Clear();//listBoxBondsAndPolyhedraにBondsを追加
            bondControl.AddRange(Crystal.Bonds);

            //Boundsコントロール
            boundControl.Crystal = Crystal;
            boundControl.Clear();
            boundControl.AddRange(Crystal.Bounds);

            //LatticePlaneコントロール
            latticePlaneControl.Crystal = Crystal;
            latticePlaneControl.Clear();
            latticePlaneControl.AddRange(crystal.LatticePlanes);

            //EOS関連
            numericBoxPressure.Value = 0;
            numericBoxEOS_A.Value = crystal.EOSCondition.A;
            numericBoxEOS_B.Value = crystal.EOSCondition.B;
            numericBoxEOS_C.Value = crystal.EOSCondition.C;
            numericBoxEOS_V0perCell.Value = crystal.EOSCondition.CellVolume0;
            numericBoxEOS_Gamma0.Value = crystal.EOSCondition.Gamma0;
            numericBoxEOS_KT0.Value = crystal.EOSCondition.K0;
            numericBoxEOS_KperT.Value = crystal.EOSCondition.KperT;
            numericBoxEOS_KprimeT0.Value = crystal.EOSCondition.Kprime0;
            numericBoxEOS_Q.Value = crystal.EOSCondition.Q;
            numericBoxEOS_T0.Value = crystal.EOSCondition.T0;
            numericBoxEOS_Theta0.Value = crystal.EOSCondition.Theta0;
            checkBoxUseEOS.Checked = crystal.DoesUseEOS;
            radioButtonMieGruneisen.Checked = crystal.EOSCondition.ThermalPressureApproach == ThermalPressure.MieGruneisen;
            radioButtonTdependenceK0andV0.Checked = crystal.EOSCondition.ThermalPressureApproach == ThermalPressure.T_dependence_BM;
            radioButtonBirchMurnaghan.Checked = crystal.EOSCondition.IsothermalPressureApproach == IsothermalPressure.Birch_Murnaghan;
            radioButtonVinet.Checked = crystal.EOSCondition.IsothermalPressureApproach == IsothermalPressure.Vinet;
            textBoxEOS_Note.Text = crystal.EOSCondition.Note;
            numericalTextBoxTemperature.Value = crystal.EOSCondition.Temperature;
            numericalTextBoxEOS_State_ValueChanged(new object(), new EventArgs());

            //弾性定数関連
            elasticityControl1.Stiffness = DenseMatrix.OfArray(crystal.ElasticStiffness);

            //PolyCrystallineProperty関連
            numericUpDownAngleResolution.Value = Math.Min((decimal)crystal.AngleResolution, numericUpDownAngleResolution.Maximum);
            numericUpDownAngleSubDivision.Value = (decimal)crystal.SubDivision;
            poleFigureControl.Crystal = crystal;

            SkipEvent = false;

        }

        #endregion
    
        #region ドラッグドロップイベント

        public void FormCrystal_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileName.Length == 1)
            {
                try { Crystal = ConvertCrystalData.ConvertToCrystal(fileName[0]); }
                catch { return; }
            }
        }

        private void FormCrystal_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        #endregion ドラッグドロップイベント


        private void buttonReset_Click(object sender, EventArgs e) => Crystal = new Crystal();

        #region 右クリックメニュー

        private void importCrystalFromCIFAMCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = " *.cif; *.amc | *.cif;*.amc" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Crystal = ConvertCrystalData.ConvertToCrystal(dlg.FileName);
                }
                catch { return; }
            }
        }

        public void exportThisCrystalAsCIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Crystal != null)
            {
                var dlg = new SaveFileDialog { Filter = " *.cif| *.cif" };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(dlg.FileName, false);
                    string str = ConvertCrystalData.ConvertToCIF(Crystal);
                    sw.Write(str);
                    sw.Close();
                }
            }
        }

        private void scatteringFactorToolStripMenuItem_Click(object sender, EventArgs e) 
            => formScatteringFactor.Visible = !formScatteringFactor.Visible;

        private void symmetryInformationToolStripMenuItem_Click(object sender, EventArgs e) 
            => formSymmetryInformation.Visible = !formSymmetryInformation.Visible;

        private void sendThisCrystalToOtherSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateFromInterface();
            if (crystal != null)
                Clipboard.SetDataObject(Crystal2.GetCrystal2(crystal), true, 3, 10);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < crystal.Atoms.Length; i++)
                crystal.Atoms[i].Dsf = new DiffuseScatteringFactor(true, 0, 0, 0, 0, 0, 0, 0);
        }

        #endregion 右クリックメニュー

        private void CrystalControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.R)
                crystal.Reserved = !crystal.Reserved;
        }

        #region EOSタブの入力設定
        private void numericalTextBoxEOS_State_ValueChanged(object sender, EventArgs e)
        {
            if (SkipEvent) return;
            SkipEvent = true;
            if (numericalTextBoxEOS_V0perMol.ReadOnly && !double.IsNaN(numericBoxEOS_V0perCell.Value))
                numericalTextBoxEOS_V0perMol.Value = numericBoxEOS_V0perCell.Value * 6.0221367 / crystal.ChemicalFormulaZ / 10;
            SkipEvent = false;
            GenerateFromInterface();

            SkipEvent = false;
            if (checkBoxUseEOS.Checked)
                numericBoxPressure.Value = crystal.EOSCondition.GetPressure(crystal.Volume * 1000);
            SkipEvent = false;
        }

        private void numericalTextBoxEOS_V0perCell_Click2(object sender, EventArgs e)
        {
            if (SkipEvent) return;
            SkipEvent = true;
            numericBoxEOS_V0perCell.ReadOnly = false;
            numericalTextBoxEOS_V0perMol.ReadOnly = true;
            SkipEvent = false;
        }

        private void numericalTextBoxEOS_V0perMol_Click2(object sender, EventArgs e)
        {
            if (SkipEvent) return;
            SkipEvent = true;
            numericBoxEOS_V0perCell.ReadOnly = true;
            numericalTextBoxEOS_V0perMol.ReadOnly = false;
            SkipEvent = false;
        }

        private void numericalTextBoxEOS_V0perMol_ValueChanged(object sender, EventArgs e)
        {
            if (SkipEvent) return;
            SkipEvent = true;
            if (numericalTextBoxEOS_V0perMol.ReadOnly == false)
                numericBoxEOS_V0perCell.Value = numericalTextBoxEOS_V0perMol.Value / 6.0221367 * 10 * crystal.ChemicalFormulaZ;
            SkipEvent = false;
        }

        #endregion EOSタブの入力設定

        private void CrystalControl_Resize(object sender, EventArgs e) => tabControl.Size = new Size(Size.Width, Size.Height - 30);

        #region Polycrystalline関連

        private void buttonGenerateRandomOrientations_Click(object sender, EventArgs e)
        {
            if (this.Crystal.Crystallites == null)
                this.Crystal.SetCrystallites();
            /*
                           int[] index=new int[0];
                           double[] density=new double[0];
                           this.Crystal.Crystallites.GetBiasedDirection(Crystal.Crystallites.GetIndex(0,22,22,5), ref index, ref density, Math.PI / 180.0 * 0.1, 1);
                            //16040 2015/08ごろに辻野君に対してシミュレーションした番号

                           for (int i = 0; i < Crystal.Crystallites.Density.Length; i++)
                                     crystal.Crystallites.Density[i] = 0;

                               for (int i = 0; i < index.Length; i++)
                               Crystal.Crystallites.Density[index[i]] += density[i];
              */
            poleFigureControl.Crystal = Crystal;
        }

        public void DrawPoleFigure()
        {
            poleFigureControl.Draw(true);
        }

        #endregion Polycrystalline関連


        private void numericUpDownAngleResolution_ValueChanged(object sender, EventArgs e)
        {
            if (SkipEvent) return;
            GenerateFromInterface();
        }

        #region poleFigureの右クリックメニュー
        /// <summary>
        /// poleFigureの右クリックメニュー　読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "Database File[*.cpo]|*.cpo" };
            try
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    using (Stream stream = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        double version = (double)formatter.Deserialize(stream);
                        if (version == 1.0)
                        {
                            numericUpDownAngleResolution.Value = (decimal)((double)formatter.Deserialize(stream));
                            numericUpDownAngleSubDivision.Value = (decimal)((int)formatter.Deserialize(stream));
                            numericUpDownCrystallineSize.Value = (decimal)((double)formatter.Deserialize(stream));
                            double[] density = (double[])formatter.Deserialize(stream);
                            crystal.Crystallites = new Crystallite(Crystal, density);

                            poleFigureControl.Crystal = Crystal;
                        }
                    }
            }
            catch
            {
                MessageBox.Show("ファイルが読み込めません");
            }
        }

        /// <summary>
        /// poleFigureの右クリックメニュー　書き込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog            {                Filter = "Database File[*.cpo]|*.cpo"            };
            try
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    using (Stream stream = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, 1.0);

                        formatter.Serialize(stream, crystal.AngleResolution);
                        formatter.Serialize(stream, crystal.SubDivision);
                        formatter.Serialize(stream, crystal.GrainSize);
                        formatter.Serialize(stream, crystal.Crystallites.Density);
                    }
            }
            catch
            {
                MessageBox.Show("ファイルが書き込みません");
            }
        }

        /// <summary>
        /// poleFigureの右クリックメニュー　ctfファイルで出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region Export CTFボタンをクリックしたときの動作

            if (crystal.Crystallites == null) return;
            int maxCrystallites = 499900;

            var dlg = new SaveFileDialog            {                Filter = "*.ctf|*.ctf"            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var sw = new StreamWriter(dlg.FileName);
                sw.WriteLine("Channel Text File");
                sw.WriteLine("Prj\t OutPut from Recipro");
                sw.WriteLine("Author\t[Unknown]");
                sw.WriteLine("JobMode\tInteractive");
                sw.WriteLine("NoMeas\t" + maxCrystallites.ToString());//+ poleFigureControl1.PolyCrystal.Crysatallites.Length.ToString());
                sw.WriteLine("AcqE1\t0");
                sw.WriteLine("AcqE2\t0");
                sw.WriteLine("AcqE3\t0");
                sw.WriteLine("Euler angles refer to Sample Coordinate system (CS0)!\tMag\t0\tCoverage\t0\tDevice\t0\tKV\t0\tTiltAngle\t0\tTiltAxis\t0");
                sw.WriteLine("Phases\t1");
                sw.WriteLine("0.000;0.000;0.000\t90;90;90\t" + Crystal.Name + "\t3\t0\t3803863129_5.0.6.3\t1060505527\t[" + crystal.Name + "]");
                sw.WriteLine("Phase\tX\tY\tBands\tError\tEuler1\tEuler2\tEuler3\tMAD\tBC\tBS");

                double sum = 0;
                for (int i = 0; i < Crystal.Crystallites.TotalCrystalline; i++)
                    sum += crystal.Crystallites.Density[i] * crystal.Crystallites.SolidAngle[i];

                double tempSum = 0;
                int seed = 0;
                for (int i = 0; i < maxCrystallites; i++)
                {
                    double partialSum = (i + 0.5) / (double)maxCrystallites * sum;

                    while (tempSum + Crystal.Crystallites.Density[seed] * Crystal.Crystallites.SolidAngle[seed] < partialSum && seed < Crystal.Crystallites.Density.Length)
                    {
                        tempSum += Crystal.Crystallites.Density[seed] * Crystal.Crystallites.SolidAngle[seed];
                        seed++;
                    }

                    var euler1 = Euler.GetEulerAngle(Crystal.Crystallites.Rotations[seed]);
                    var euler = new double[] { euler1.Phi, euler1.Theta, euler1.Psi };
                    string str = "";
                    foreach (double angle in euler)
                    {
                        double d = (angle > 0 ? angle : angle + 2 * Math.PI) / Math.PI * 180;
                        if (d >= 100)
                            str += d.ToString("000.00") + "\t";
                        else if (d >= 10)
                            str += d.ToString("00.000") + "\t";
                        else
                            str += d.ToString("0.0000") + "\t";
                    }
                    sw.WriteLine("1\t0\t0\t0\t0\t" + str + "0\t0\t0");
                }
                sw.Close();
            }

            #endregion Export CTFボタンをクリックしたときの動作
        }

        /// <summary>
        /// poleFigureの右クリックメニュー　txtファイルで出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void asTXTFileAllEulerAngleAndDensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (crystal.Crystallites == null) return;

            var dlg = new SaveFileDialog            {                Filter = "*.txt|*.txt"            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(dlg.FileName))
                {
                    sw.WriteLine("Sample Name:\t" + Crystal.Name);
                    sw.WriteLine("Cell constants:\t"
                        + Crystal.A.ToString() + "\t" + Crystal.B.ToString() + "\t" + Crystal.C.ToString() + "\t"
                        + (crystal.Alpha / Math.PI * 180).ToString() + "\t" + (crystal.Beta / Math.PI * 180).ToString() + "\t" + (crystal.Gamma / Math.PI * 180).ToString());
                    sw.WriteLine("Space group:\t" + Crystal.Symmetry.SpaceGroupHMfullStr);
                    sw.WriteLine("");
                    sw.WriteLine("Euler angles refer to Sample Coordinate system");
                    sw.WriteLine("No.\tEuler1\tEuler2\tEuler3\tDensity");

                    double sum = 0;

                    double[] density = new double[Crystal.Crystallites.TotalCrystalline];
                    int[] index = new int[Crystal.Crystallites.TotalCrystalline];
                    for (int i = 0; i < Crystal.Crystallites.TotalCrystalline; i++)
                    {
                        density[i] = crystal.Crystallites.Density[i] * crystal.Crystallites.SolidAngle[i];
                        index[i] = i;
                        sum += density[i];
                    }
                    if (sender == asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem)
                    {
                        Array.Sort(density, index);
                        density = density.Reverse().ToArray();
                        index = index.Reverse().ToArray();
                    }

                    for (int i = 0; i < Crystal.Crystallites.TotalCrystalline; i++)
                    {
                        string str = i.ToString() + "\t";
                        var euler1 = Euler.GetEulerAngle(Crystal.Crystallites.Rotations[index[i]]);
                        var euler = new double[] { euler1.Phi, euler1.Theta, euler1.Psi };

                        foreach (double angle in euler)
                        {
                            double d = (angle > 0 ? angle : angle + 2 * Math.PI) / Math.PI * 180;
                            str += d.ToString("000.0000") + "\t";
                        }
                        str += (density[i] / sum * crystal.Crystallites.TotalCrystalline).ToString();
                        sw.WriteLine(str);
                    }
                }
            }
        }

        #endregion

        private void revertCellConstantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crystal.RevertInitialCellConstants();
            Crystal = crystal;
        }

        private void strainControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formStrain.crystal == null)
                formStrain.crystal = Crystal;

            formStrain.Visible = !formStrain.Visible;
        }

        private void elasticityControl1_ValueChanged(object sender, EventArgs e)
        {
            if (SkipEvent) return;

            if (elasticityControl1.Mode == Elasticity.Mode.Compliance)
                formStrain.Compliance = elasticityControl1.Compliance;
            else
                formStrain.Stiffness = elasticityControl1.Stiffness;

            formStrain.ElasticityMode = elasticityControl1.Mode;
        }

        private void atomControl_AtomsChanged(object sender, EventArgs e)
        {
            if (SkipEvent) return;
            GenerateFromInterface();
        }

        private void symmetryControl_ItemChanged(object sender, EventArgs e)
        {
            if (SkipEvent) return;
            GenerateFromInterface();
        }

        private void bondControl_ItemsChanged(object sender, EventArgs e)
        {
          //このイベントは、StructureViewerなどから直接呼び出される。
        }
    }
}
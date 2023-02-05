#region Using
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.Scripting.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static IronPython.Modules._ast;
#endregion

namespace Crystallography.Controls;

[TypeConverter(typeof(DefinitionOrderTypeConverter))]
public partial class CrystalControl : UserControl
{
    #region プロパティ、フィールド、イベントハンドラ

    public bool SkipEvent { get; set; } = false;

    public bool SymmetryInformationVisible { set => FormSymmetryInformation.Visible = value; get => FormSymmetryInformation.Visible; }

    public bool ScatteringFactorVisible { set { FormScatteringFactor.Visible = value; } get => FormScatteringFactor.Visible; }

    public bool StrainControlVisible { get => formStrain.Visible; }

    public int SymmetrySeriesNumber { get => symmetryControl.SymmetrySeriesNumber; set => symmetryControl.SymmetrySeriesNumber = value; }

    #region Tabページの表示/非表示プロパティ

    public bool VisibleBasicInfoTab { set { visibleBasicInfoTab = value; setTabPages(); } get => visibleBasicInfoTab; }
    private bool visibleBasicInfoTab = true;
    public bool VisibleAtomTab { set { visibleAtomTab = value; setTabPages(); } get => visibleAtomTab; }
    private bool visibleAtomTab = true;
    public bool VisibleElasticityTab { set { visibleElasticityTab = value; setTabPages(); } get { return visibleElasticityTab; } }
    private bool visibleElasticityTab = true;

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
        if (visibleBondsPolyhedraTab) tabControl.TabPages.Add(tabPageBondsPolyhedra);
        if (VisibleBoundTab) tabControl.TabPages.Add(tabPageBounds);
        if (VisibleLatticePlaneTab) tabControl.TabPages.Add(tabPageLatticePlane);
        if (VisibleReferenceTab) tabControl.TabPages.Add(tabPageReference);
        if (VisibleEOSTab) tabControl.TabPages.Add(tabPageEOS);
        if (VisibleElasticityTab) tabControl.TabPages.Add(tabPageElasticity);
        if (VisibleStressStrainTab) tabControl.TabPages.Add(tabPageStrainStress);
        if (VisiblePolycrystallineTab) tabControl.TabPages.Add(tabPagePolycrystalline);
    }

    #endregion Tabページの表示/非表示プロパティ

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Crystal Crystal
    {
        set
        {
            crystal = value;
            if (crystal != null)
            {
                Enabled = !crystal.FlexibleMode;

                SetToInterface();
                //原子位置チェック (strain controlで選択した後、原子位置が変になってしまう問題の修正. 2017/05/29)
                if (crystal.ChemicalFormulaZ == 1)
                {
                    for (int i = 0; i < crystal.Atoms.Length; i++)
                        crystal.Atoms[i].ResetSymmetry(SymmetrySeriesNumber);
                    crystal.GetFormulaAndDensity();

                    SetToInterface();
                }

                CrystalChanged?.Invoke(this, new EventArgs());
            }
        }
        get => crystal;
    }
    private Crystal crystal;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

    public (double A, double B, double C, double Alpha, double Beta, double Gamma) CellConstants
    { get => symmetryControl.CellConstants; set => symmetryControl.CellConstants = value; }

    public double A { get => symmetryControl.A; set => symmetryControl.A = value; }
    public double B { get => symmetryControl.B; set => symmetryControl.B = value; }
    public double C { get => symmetryControl.C; set => symmetryControl.C = value; }
    public double Alpha { get => symmetryControl.Alpha; set => symmetryControl.Alpha = value; }
    public double Beta { get => symmetryControl.Beta; set => symmetryControl.Beta = value; }
    public double Gamma { get => symmetryControl.Gamma; set => symmetryControl.Gamma = value; }



    public int DefaultTabNumber { set => tabControl.SelectedIndex = value; get => tabControl.SelectedIndex; }

    public event EventHandler CrystalChanged;

    public FormScatteringFactor FormScatteringFactor;
    public FormSymmetryInformation FormSymmetryInformation;
    //private FormAtomDetailedInfo formAtomDetailedInfo;

    public FormStrain formStrain;


    //候補の数値
    //private readonly double[] rationalNumbers
    //    = new double[] { 1.0 / 12.0, 1.0 / 8.0, 1.0 / 6.0, 1.0 / 4.0, 1.0 / 3.0, 3.0 / 8.0, 5.0 / 12.0, 1.0 / 2.0, 7.0 / 12.0, 5.0 / 8.0, 2.0 / 3.0, 3.0 / 4.0, 5.0 / 6.0, 7.0 / 8.0, 11.0 / 12.0 };

    #endregion

    #region コンストラクタ、Loadイベント
    public CrystalControl()
    {
        InitializeComponent();

        FormScatteringFactor = new FormScatteringFactor { CrystalControl = this, Visible = false };
        FormSymmetryInformation = new FormSymmetryInformation { CrystalControl = this, Visible = false };
        formStrain = new FormStrain { CrystalControl = this, Visible = false };
    }

    private void CrystalForm_Load(object sender, System.EventArgs e)
    {
        textBoxTitle.Size = new Size(tabPageReference.Width - textBoxTitle.Location.X - 2, tabPageReference.Height - textBoxTitle.Location.Y - 2);
        FormScatteringFactor.VisibleChanged += new EventHandler(formScatteringFactor_VisibleChanged);
        FormSymmetryInformation.VisibleChanged += new EventHandler(formSymmetryInformation_VisibleChanged);

        typeof(UserControl).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this, true, null);

    }



    #endregion

    #region イベントハンドラ

    public event EventHandler ScatteringFactor_VisibleChanged;
    public event EventHandler SymmetryInformation_VisibleChanged;

    #endregion

    #region Crystalクラスを画面下部 から生成/にセット

    /// <summary>
    /// Formに入力された内容からからCrystalを生成する
    /// </summary>
    public void GenerateFromInterface()
    {
        if (SkipEvent) return;
        SkipEvent = true;

        var (A, B, C, Alpha, Beta, Gamma) = symmetryControl.CellConstants;
        if (A < 0 || B < 0 || C < 0 || Alpha > Math.PI || Beta > Math.PI || Gamma > Math.PI)
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
            bondControl.GetAll(), boundControl.GetAll(), latticePlaneControl.GetAll(), eosControl.EOScondition);

        crystal.ElasticStiffness = elasticityControl1.Stiffness.ToArray();

        #region PolyCrystallineProperty
        crystal.AngleResolution = (double)numericUpDownAngleResolution.Value;
        crystal.SubDivision = (int)numericUpDownAngleSubDivision.Value;
        crystal.GrainSize = (double)numericUpDownCrystallineSize.Value;
        if (poleFigureControl.Crystal != null)
            crystal.Crystallites = poleFigureControl.Crystal.Crystallites;

        crystal.Strain = new Matrix3D(
            numericBoxStrain11.Value, numericBoxStrain12.Value, numericBoxStrain13.Value,
            numericBoxStrain12.Value, numericBoxStrain22.Value, numericBoxStrain23.Value,
            numericBoxStrain13.Value, numericBoxStrain23.Value, numericBoxStrain33.Value);
        crystal.HillCoefficient = numericBoxHill.Value;

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

        SuspendLayout();

        colorControl.Color = Color.FromArgb(Crystal.Argb);
        textBoxName.Text = Crystal.Name;
        textBoxMemo.Text = Crystal.Note;

        textBoxAuthor.Text = Crystal.PublAuthorName;
        textBoxJournal.Text = Crystal.Journal;
        textBoxFormula.Text = Crystal.ChemicalFormulaSum;
        textBoxTitle.Text = Crystal.PublSectionTitle;

        numericBoxDensity.Value = Crystal.Density;
        numericBoxVolume.Value = Crystal.Volume * 1000;
        numericBoxMolarVolume.Value = Crystal.Volume * UniversalConstants.A / Crystal.ChemicalFormulaZ * 1E-21;
        numericBoxZnumber.Value = Crystal.ChemicalFormulaZ;

        numericBoxMolarMass.Value = numericBoxDensity.Value * numericBoxMolarVolume.Value;
        numericBoxCellMass.Value = numericBoxDensity.Value * numericBoxVolume.Value;

        SymmetrySeriesNumber = Crystal.SymmetrySeriesNumber;//SymmetrySeriesNumberをフィールドからプロパティに変更。set{}の所でコンボボックスをセットする。(20170526)

        if (ChangeCellParameter)
        {
            symmetryControl.CellConstants = (Crystal.A, Crystal.B, Crystal.C, Crystal.Alpha, Crystal.Beta, Crystal.Gamma);
            symmetryControl.CellConstantsErr = (Crystal.A_err, Crystal.B_err, Crystal.C_err, Crystal.Alpha_err, Crystal.Beta_err, Crystal.Gamma_err);
        }

        //Atomsコントロール
        atomControl.Crystal = crystal;

        //Bondコントロール
        bondControl.Crystal = crystal;

        //Boundsコントロール
        boundControl.Crystal = crystal;

        //LatticePlaneコントロール
        latticePlaneControl.Crystal = crystal;

        //EOS関連
        eosControl.Crystal = crystal;


        //弾性定数関連
        elasticityControl1.Stiffness = DenseMatrix.OfArray(crystal.ElasticStiffness);

        //PolyCrystallineProperty関連
        numericUpDownAngleResolution.Value = Math.Min((decimal)crystal.AngleResolution, numericUpDownAngleResolution.Maximum);
        numericUpDownAngleSubDivision.Value = crystal.SubDivision;
        poleFigureControl.Crystal = crystal;

        SkipEvent = false;

        ResumeLayout();
    }

    #endregion

    public void ReadCrystal(string filename)
    {
        try { Crystal = ConvertCrystalData.ConvertToCrystal(filename); }
        catch (Exception ex) { if (AssemblyState.IsDebug) MessageBox.Show(ex.ToString()); return; }
    }

    #region ドラッグドロップイベント

    public void FormCrystal_DragDrop(object sender, DragEventArgs e)
    {
        string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        if (fileName.Length == 1)
            ReadCrystal(fileName[0]);
    }

    private void FormCrystal_DragEnter(object sender, DragEventArgs e)
    {
        e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;
    }

    #endregion ドラッグドロップイベント

    #region 右クリックメニュー
    private void importCrystalFromCIFAMCToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var dlg = new OpenFileDialog { Filter = " *.cif; *.amc | *.cif;*.amc" };
        if (dlg.ShowDialog() == DialogResult.OK)
            ReadCrystal(dlg.FileName);
    }

    public void exportThisCrystalAsCIFToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Crystal != null)
        {
            var dlg = new SaveFileDialog { Filter = " *.cif| *.cif" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var sw = new StreamWriter(dlg.FileName, false);
                var str = ConvertCrystalData.ConvertToCIF(Crystal);
                sw.Write(str);
                sw.Close();
            }
        }
    }

    private void scatteringFactorToolStripMenuItem_Click(object sender, EventArgs e)
        => FormScatteringFactor.Visible = !FormScatteringFactor.Visible;

    private void symmetryInformationToolStripMenuItem_Click(object sender, EventArgs e)
        => FormSymmetryInformation.Visible = !FormSymmetryInformation.Visible;

    private void sendThisCrystalToOtherSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
    {
        GenerateFromInterface();
        if (crystal != null)
            Clipboard.SetDataObject(Crystal2.FromCrystal(crystal), true, 3, 10);
    }

    private void resetToolStripMenuItem_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < crystal.Atoms.Length; i++)
            crystal.Atoms[i].Dsf = new DiffuseScatteringFactor(DiffuseScatteringFactor.Type.B, true, 0, 0, null, null, Crystal.CellValue);
    }

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

    /// <summary>
    /// 空間群P1に変換
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void convertToP1ToolStripMenuItem_Click(object sender, EventArgs e) => toSuperStructure(1, 1, 1);

    //超構造に変換
    private void convertToSuperstructureToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var dlg = new FormSuperStructure();
        if (dlg.ShowDialog() == DialogResult.OK)
            toSuperStructure(dlg.A, dlg.B, dlg.C);
    }
    private void convertToAnotherSpacegroupToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var seriesNum = symmetryControl.SymmetrySeriesNumber;
        var spNum = SymmetryStatic.NumArray[seriesNum][1];

        var list = new List<(int SeriesNum, string Notation)>();
        foreach (var n in SymmetryStatic.NumArray)
            if (spNum == n[1] && seriesNum != n[0])
                list.Add((n[0], SymmetryStatic.StrArray[n[0]][3]));//自分自身を除く、同じ空間群番号のものを追加

        if (list.Count == 0 || spNum == 1 || spNum == 2)//候補がゼロかP1かP-1は除く
            MessageBox.Show("No candidate for the space group");
        else
        {
            var dlg = new FormAnotherSpaceGroup() { Candidates = list.ToArray() };
            if (dlg.ShowDialog() == DialogResult.OK)
                toAnotherSpaceGroup(dlg.SeriesNum);
        }
    }
    #endregion 右クリックメニュー

    #region 空間群を変換する関数群

    #region 超構造に変換
    /// <summary>
    /// 超構造に変換する関数
    /// </summary>
    /// <param name="_u"></param>
    /// <param name="_v"></param>
    /// <param name="_w"></param>
    private void toSuperStructure(int _u, int _v, int _w)
    {
        GenerateFromInterface();
        crystal.SymmetrySeriesNumber = 1;

        var temp_atoms = new List<Atoms>();
        foreach (var atoms in Crystal.Atoms)
        {
            int n = 0;
            foreach (var atom in atoms.Atom)
            {
                for (double u = 0; u < _u; u++)
                    for (double v = 0; v < _v; v++)
                        for (double w = 0; w < _w; w++)
                        {
                            var x = (atom.X + u) / _u;
                            var y = (atom.Y + v) / _v;
                            var z = (atom.Z + w) / _w;

                            var x_err = atoms.X_err / _u;
                            var y_err = atoms.Y_err / _v;
                            var z_err = atoms.Z_err / _w;

                            temp_atoms.Add(new Atoms(
                                atoms.Label.TrimEnd() + "_" + n.ToString(),
                                atoms.AtomicNumber, atoms.SubNumberXray, atoms.SubNumberElectron, atoms.Isotope,
                                1,
                                new Vector3DBase(x, y, z), new Vector3DBase(x_err, y_err, z_err),
                                atoms.Occ, atoms.Occ_err,
                                atoms.Dsf,
                                atoms.Material,
                                atoms.Radius, atoms.GLEnabled, atoms.ShowLabel));
                            n++;
                        }
            }
        }
        crystal.A *= _u;
        crystal.B *= _v;
        crystal.C *= _w;
        crystal.Atoms = temp_atoms.ToArray();

        SetToInterface(true);
        GenerateFromInterface();
    }
    #endregion

    #region 別の空間群に変換
    /// <summary>
    /// 別の空間群に変換する 原子位置や格子定数のエラー、および熱散漫散乱因子の変換は考慮していない
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void toAnotherSpaceGroup(int destNum)
    {
        var srcNum = symmetryControl.SymmetrySeriesNumber;
        var crystalSystem = SymmetryStatic.NumArray[srcNum][5];
        var sgNum = SymmetryStatic.NumArray[srcNum][1];
        var srcExtra = SymmetryStatic.StrArray[srcNum][0];
        var dstExtra = SymmetryStatic.StrArray[destNum][0];

        #region monoclinicの時。軸の変換のみがあり得る。
        if (crystalSystem == 2)
        {
            if (srcExtra.Length == 1) srcExtra += "1";
            if (dstExtra.Length == 1) dstExtra += "1";

            var cry = Deep.Copy(Crystal);
            cry.SymmetrySeriesNumber = destNum;
            cry.SetAxis();
            foreach (var a in cry.Atoms)
            {
                (a.X, a.Y, a.Z) = exchangeAtomPositionMonoclinic(a.X, a.Y, a.Z, srcExtra, false);//標準セッティングに変換
                (a.X, a.Y, a.Z) = exchangeAtomPositionMonoclinic(a.X, a.Y, a.Z, dstExtra, true);//目的セッティングに変換
            }
            var (A, B, C) = convertAxisMonoclinic(cry.A_Axis, cry.B_Axis, cry.C_Axis, srcExtra, false);//標準セッティングに変換
            (A, B, C) = convertAxisMonoclinic(A, B, C, dstExtra, true);//目的セッティングに変換
            cry.A = A.Length; cry.B = B.Length; cry.C = C.Length;
            cry.Alpha = Vector3D.AngleBetVectors(B, C);
            cry.Beta = Vector3D.AngleBetVectors(C, A);
            cry.Gamma = Vector3D.AngleBetVectors(A, B);
            crystal = cry;
        }
        #endregion

        #region Orhorhombicの時. 軸の変換とOrigin Choiceの変換があり得る
        if (crystalSystem == 3)
        {
            var src = convOrtho(srcExtra);
            var dst = convOrtho(dstExtra);

            var cry = Deep.Copy(Crystal);
            cry.SymmetrySeriesNumber = destNum;
            foreach (var a in cry.Atoms)
            {
                //標準セッティングに変換
                (a.X, a.Y, a.Z) = exchangeOrtho(a.X, a.Y, a.Z, src.Setting);
                (a.X_err, a.Y_err, a.Z_err) = exchangeOrtho(a.X_err, a.Y_err, a.Z_err, src.Setting);
                //Originの処理
                if (src.Origin != dst.Origin)
                    (a.X, a.Y, a.Z) = shift(a.X, a.Y, a.Z, sgNum, src.Origin == 1);
                //目的セッティングに変換
                (a.X, a.Y, a.Z) = exchangeOrtho(a.X, a.Y, a.Z, dst.Setting, true);
                (a.X_err, a.Y_err, a.Z_err) = exchangeOrtho(a.X_err, a.Y_err, a.Z_err, dst.Setting, true);
            }
            (cry.A, cry.B, cry.C) = exchangeOrtho(cry.A, cry.B, cry.C, src.Setting, false, true);//標準セッティングに変換
            (cry.A, cry.B, cry.C) = exchangeOrtho(cry.A, cry.B, cry.C, dst.Setting, true, true);//目的セッティングに変換
            crystal = cry;
        }
        #endregion

        #region Tetragonal か Cubicの時。 Origin Choiceの変換があり得る。
        if (crystalSystem == 4 || crystalSystem == 7)
        {
            var src = convOrtho(srcExtra);
            var cry = Deep.Copy(Crystal);
            cry.SymmetrySeriesNumber = destNum;
            foreach (var a in cry.Atoms)
                (a.X, a.Y, a.Z) = shift(a.X, a.Y, a.Z, sgNum, src.Origin == 1);
            crystal = cry;
        }
        #endregion

        #region trigonalの時 RhomboとHexaの変換がありうる。
        if (crystalSystem == 5)
        {
            var cry = Deep.Copy(Crystal);
            cry.SymmetrySeriesNumber = destNum;
            cry.SetAxis();

            Vector3D srcA = cry.A_Axis, srcB = cry.B_Axis, srcC = cry.C_Axis, dstA, dstB, dstC;

            if (srcExtra == "H")
            {//HをRに変換
                foreach (var a in cry.Atoms)
                    (a.X, a.Y, a.Z) = (a.X + a.Z, -a.X + a.Y + a.Z, a.Y + a.Z);//Rセッティングに変換
                (dstA, dstB, dstC) = ((2 * srcA + srcB + srcC) / 3, (-srcA + srcB + srcC) / 3, (-srcA - srcB + srcC) / 3);
            }
            else
            {//RをHに変換
                foreach (var a in cry.Atoms)
                    (a.X, a.Y, a.Z) = ((2 * a.X - a.Y - a.Z) / 3, (a.X + a.Y - 2 * a.Z) / 3, (a.X + a.Y + a.Z) / 3);//Hセッティングに変換
                (dstA, dstB, dstC) = (srcA - srcB, srcB - srcC, srcA + srcB + srcC);
            }
            cry.A = dstA.Length; cry.B = dstB.Length; cry.C = dstC.Length;
            cry.Alpha = Vector3D.AngleBetVectors(dstB, dstC);
            cry.Beta = Vector3D.AngleBetVectors(dstC, dstA);
            cry.Gamma = Vector3D.AngleBetVectors(dstA, dstB);
            crystal = cry;
        }
        #endregion

        SetToInterface(true);
        GenerateFromInterface();
    }
    #endregion

    #region Origin choiceを変更する関数
    static (double X, double Y, double Z) shift(double x, double y, double z, int sgNum, bool to2nd)
        => sgNum switch
        {
            48 or 86 or 126 or 201 or 222 or 224 => to2nd ? (x + one4th, y + one4th, z + one4th) : (x - one4th, y - one4th, z - one4th),
            50 or 59 or 125 => to2nd ? (x + one4th, y + one4th, z) : (x - one4th, y - one4th, z),
            68 => to2nd ? (x, y + one4th, z + one4th) : (x, y - one4th, z - one4th),
            70 => to2nd ? (x + one8th, y + one8th, z + one8th) : (x - one8th, y - one8th, z - one8th),
            85 => to2nd ? (x + one4th, y - one4th, z) : (x - one4th, y + one4th, z),
            88 => to2nd ? (x, y + one4th, z + one8th) : (x, y - one4th, z - one8th),
            129 or 130 => to2nd ? (x - one4th, y + one4th, z) : (x + one4th, y - one4th, z),
            133 or 137 or 138 => to2nd ? (x - one4th, y + one4th, z - one4th) : (x + one4th, y - one4th, z + one4th),
            134 => to2nd ? (x + one4th, y - one4th, z + one4th) : (x - one4th, y + one4th, z - one4th),
            141 => to2nd ? (x, y - one4th, z + one8th) : (x, y + one4th, z - one8th),
            142 => to2nd ? (x, y + one4th, z + three8th) : (x, y - one4th, z - three8th),
            203 or 227 or 228 => to2nd ? (x + one8th, y + one8th, z + one8th) : (x - one8th, y - one8th, z - one8th),
            _ => (x, y, z)
        };
    const double one4th = 1.0 / 4.0, one8th = 1.0 / 8.0, three8th = 3.0 / 8.0;
    #endregion

    #region Monoclinic用の関数
    /// <summary>
    /// 原子位置を変換。Monoclinic用。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="setting"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    static (double X, double Y, double Z) exchangeAtomPositionMonoclinic(double x, double y, double z, string setting, bool forward = true)
        => setting switch
        {
            "b2" => forward ? (-z, y, x - z) : (z - x, y, -x),
            "b3" => forward ? (z - x, y, -x) : (-z, y, x - z),
            "-b1" => forward ? (-z, y, x) : (z, y, -x),
            "-b2" => forward ? (x + z, y, z) : (x - z, y, z),
            "-b3" => forward ? (-x, y, -z - x) : (-x, y, x - z),
            
            "c1" => forward ? (z, x, y) : (y, z, x),
            "c2" => forward ? (x - z, -z, y) : (x - y, z, -y),
            "c3" => forward ? (-x, z - x, y) : (-x, z, y - x),
            "-c1" => forward ? (x, -z, y) : (x, z, -y),
            "-c2" => forward ? (z, x + z, y) : (y - x, z, x),
            "-c3" => forward ? (-z - x, -x, y) : (-y, z, y - x),
            
            "a1" => forward ? (y, z, x) : (z, x, y),
            "a2" => forward ? (y, x - z, -z) : (y - z, x, -z),
            "a3" => forward ? (y, -x, z - x) : (-y, x, z - y),
            "-a1" => forward ? (y, x, -z) : (y, x, -z),
            "-a2" => forward ? (y, z, x + z) : (z - y, x, y),
            "-a3" => forward ? (y, -z - x, -x) : (-z, x, z - y),
            _ => (x, y, z),
        };

    static (Vector3D A, Vector3D B, Vector3D C) convertAxisMonoclinic(Vector3D a, Vector3D b, Vector3D c, string setting, bool forward = true)
        => setting switch
        {
            "b2" => forward ? (-c - a, b, a) : (c, b, -c - a),
            "b3" => forward ? (c, b, -c - a) : (-c - a, b, a),
            "-b1" => forward ? (-c, b, a) : (c, b, -a),
            "-b2" => forward ? (a, b, c - a) : (a, b, c + a),
            "-b3" => forward ? (c - a, b, -c) : (-c - a, b, -c),

            "c1" => forward ? (c, a, b) : (b, c, a),
            "c2" => forward ? (a, -c - a, b) : (a, c, -a - b),
            "c3" => forward ? (-c - a, c, b) : (-a - b, c, b),
            "-c1" => forward ? (a, -c, b) : (a, c, -b),
            "-c2" => forward ? (c - a, a, b) : (b, c, a + b),
            "-c3" => forward ? (-c, c - a, b) : (-a - b, c, -a),

            "a1" => forward ? (b, c, a) : (c, a, b),
            "a2" => forward ? (b, a, -c - a) : (b, a, -b - c),
            "a3" => forward ? (b, -c - a, c) : (-b - c, a, c),
            "-a1" => forward ? (b, a, -c) : (b, a, -c),
            "-a2" => forward ? (b, c - a, a) : (c, a, b + c),
            "-a3" => forward ? (b, -c, c - a) : (-b - c, a, -b),
            _ => (a, b, c),
        };
    #endregion

    #region Orthorhombic 用の関数
    static (double X, double Y, double Z) exchangeOrtho(double x, double y, double z, int[] setting, bool inverse = false, bool abs = false)
    {
        double[] src = new[] { x, y, z }, dst = new double[3];
        for (int i = 0; i < 3; i++)
        {
            if (!inverse)
                dst[Math.Abs(setting[i]) - 1] = setting[i] > 0 ? src[i] : -src[i];
            else
                dst[i] = setting[i] > 0 ? src[Math.Abs(setting[i]) - 1] : -src[Math.Abs(setting[i]) - 1];
        }
        return abs ? (Math.Abs(dst[0]), Math.Abs(dst[1]), Math.Abs(dst[2])) : (dst[0], dst[1], dst[2]);
    }
    static (int Origin, int[] Setting) convOrtho(string s) // extra文字列を解析可能な形に変換する。例えば2ba-cを(2, {2, 1, -3})
    {
        if (s.Length == 0) return (1, new[] { 1, 2, 3 });
        int origin = 1;
        if (s[0] == '1' || s[0] == '2')//OriginChoiceの変換があり得るのは、48, 50, 59, 68, 70, 
        {
            origin = s[0] == '1' ? 1 : 2;
            s = s[1..];
        }
        if (s.Length == 0) return (origin, new[] { 1, 2, 3 });

        int[] setting = new int[3];
        for (int i = 0; i < 3; i++)
        {
            setting[i] = s[0] != '-' ? 1 : -1;
            if (s[0] == '-')
                s = s[1..];
            setting[i] *= s[0] switch { 'a' => 1, 'b' => 2, _ => 3 };
            s = s[1..];
        }
        return (origin, setting);
    }
    #endregion

    #endregion

    #region キーボードイベント

    private void CrystalControl_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.Shift && e.KeyCode == Keys.R)
            crystal.Reserved = !crystal.Reserved;
    }
    #endregion

    #region EOS関連
    /// <summary>
    /// 外部から呼び出されることを想定.
    /// </summary>
    public void CalculateEOS() => eosControl.CalculatePressure();

    #endregion EOSタブの入力設定

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

    private void numericUpDownAngleResolution_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        GenerateFromInterface();
    }

    #endregion Polycrystalline関連

    #region poleFigureの右クリックメニュー
    /// <summary>
    /// poleFigureの右クリックメニュー　読み込み
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void readToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //2022/11/10 BinaryFormatterが使えなくなったので、取りあえずコメントアウト

        //var dlg = new OpenFileDialog { Filter = "Database File[*.cpo]|*.cpo" };
        //try
        //{
        //    if (dlg.ShowDialog() == DialogResult.OK)
        //        using (Stream stream = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
        //        {
        //            IFormatter formatter = new BinaryFormatter();
        //            double version = (double)formatter.Deserialize(stream);
        //            if (version == 1.0)
        //            {
        //                numericUpDownAngleResolution.Value = (decimal)((double)formatter.Deserialize(stream));
        //                numericUpDownAngleSubDivision.Value = (decimal)((int)formatter.Deserialize(stream));
        //                numericUpDownCrystallineSize.Value = (decimal)((double)formatter.Deserialize(stream));
        //                double[] density = (double[])formatter.Deserialize(stream);
        //                crystal.Crystallites = new Crystallite(Crystal, density);

        //                poleFigureControl.Crystal = Crystal;
        //            }
        //        }
        //}
        //catch
        //{
        //    MessageBox.Show("ファイルが読み込めません");
        //}
    }

    /// <summary>
    /// poleFigureの右クリックメニュー　書き込み
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {

        //2022/11/10 BinaryFormatterが使えなくなったので、取りあえずコメントアウト


        //var dlg = new SaveFileDialog            {                Filter = "Database File[*.cpo]|*.cpo"            };
        //try
        //{
        //    if (dlg.ShowDialog() == DialogResult.OK)
        //        using (Stream stream = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
        //        {
        //            IFormatter formatter = new BinaryFormatter();
        //            formatter.Serialize(stream, 1.0);

        //            formatter.Serialize(stream, crystal.AngleResolution);
        //            formatter.Serialize(stream, crystal.SubDivision);
        //            formatter.Serialize(stream, crystal.GrainSize);
        //            formatter.Serialize(stream, crystal.Crystallites.Density);
        //        }
        //}
        //catch
        //{
        //    MessageBox.Show("ファイルが書き込みません");
        //}
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

        var dlg = new SaveFileDialog { Filter = "*.ctf|*.ctf" };
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

                var (Phi, Theta, Psi) = Euler.GetEulerAngle(Crystal.Crystallites.Rotations[seed]);
                var euler = new double[] { Phi, Theta, Psi };
                string str = "";
                foreach (double angle in euler)
                {
                    double d = (angle > 0 ? angle : angle + 2 * Math.PI) / Math.PI * 180;
                    if (d >= 100)
                        str += $"{d:000.00}\t";
                    else if (d >= 10)
                        str += $"{d:00.000}\t";
                    else
                        str += $"{d:0.0000}\t";
                }
                sw.WriteLine($"1\t0\t0\t0\t0\t{str}0\t0\t0");
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

        var dlg = new SaveFileDialog { Filter = "*.txt|*.txt" };
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            using var sw = new StreamWriter(dlg.FileName);
            sw.WriteLine("Sample Name:\t" + Crystal.Name);
            sw.WriteLine($"Cell constants:\t{Crystal.A}\t{Crystal.B}\t{Crystal.C}\t{crystal.Alpha / Math.PI * 180}\t{crystal.Beta / Math.PI * 180}\t{crystal.Gamma / Math.PI * 180}");
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
                var (Phi, Theta, Psi) = Euler.GetEulerAngle(Crystal.Crystallites.Rotations[index[i]]);
                var euler = new double[] { Phi, Theta, Psi };

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

    #endregion

    #region 子コントロールからの変化イベント

    private void atomControl_AtomsChanged(object sender, EventArgs e) => GenerateFromInterface();

    private void symmetryControl_ItemChanged(object sender, EventArgs e) => GenerateFromInterface();

    private void bondControl_ItemsChanged(object sender, EventArgs e)
    {
        //bondControlのItemChangedイベントは、StructureViewerなどに直接登録されている。
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
    #endregion

    #region Symmetry Information と Scatetring Factorの立ち上げ、イベント

    private void buttonSymmetryInfo_Click(object sender, EventArgs e) => FormSymmetryInformation.Visible = !FormSymmetryInformation.Visible;
    private void formSymmetryInformation_VisibleChanged(object sender, EventArgs e) => SymmetryInformation_VisibleChanged?.Invoke(sender, e);

    private void buttonScatteringFactor_Click(object sender, EventArgs e) => FormScatteringFactor.Visible = !FormScatteringFactor.Visible;

    private void formScatteringFactor_VisibleChanged(object sender, EventArgs e) => ScatteringFactor_VisibleChanged?.Invoke(sender, e);

    #endregion

    #region リサイズイベント中は描画を停止する
    bool registResizeEvent = false;
    private void CrystalControl_Resize_1(object sender, EventArgs e)
    {
        if (!this.DesignMode && !registResizeEvent)
        {
            var parent = this.Parent;
            while (parent is not Form && parent != null)
                parent = parent.Parent;
            if (parent == null)
                return;
            var form = parent as Form;
            form.ResizeBegin += (s, ea) => SuspendLayout();
            form.ResizeEnd += (s, ea) => ResumeLayout();
            registResizeEvent = true;
        }
    }
    #endregion

    private void buttonStressSet_Click(object sender, EventArgs e)
    {
        GenerateFromInterface();
    }
}
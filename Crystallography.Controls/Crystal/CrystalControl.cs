using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Crystallography.Controls;

[TypeConverter(typeof(DefinitionOrderTypeConverter))]
public partial class CrystalControl : CaptureUserControlBase
{
    #region プロパティ、フィールド、イベントハンドラ

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SkipEvent { get; set; } = false;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool SymmetryInformationVisible { get => FormSymmetryInformation.Visible; set => FormSymmetryInformation.Visible = value; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ScatteringFactorVisible { get => FormScatteringFactor.Visible; set => FormScatteringFactor.Visible = value; }

    public bool StrainControlVisible => formStrain.Visible;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ColorControlVisible { get => colorControl.Visible; set => colorControl.Visible = value; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int SymmetrySeriesNumber { get => symmetryControl.SymmetrySeriesNumber; set => symmetryControl.SymmetrySeriesNumber = value; }

    #region Tab ページの表示/非表示

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool VisibleBasicInfoTab { get => visibleBasicInfoTab; set { visibleBasicInfoTab = value; setTabPages(); } }
    private bool visibleBasicInfoTab = true;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool VisibleAtomTab { get => visibleAtomTab; set { visibleAtomTab = value; setTabPages(); } }
    private bool visibleAtomTab = true;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool VisibleElasticityTab { get => visibleElasticityTab; set { visibleElasticityTab = value; setTabPages(); } }
    private bool visibleElasticityTab = true;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool VisibleBondsPolyhedraTab { get => visibleBondsPolyhedraTab; set { visibleBondsPolyhedraTab = value; setTabPages(); } }
    private bool visibleBondsPolyhedraTab = true;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool VisibleReferenceTab { get => visibleReferenceTab; set { visibleReferenceTab = value; setTabPages(); } }
    private bool visibleReferenceTab = true;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool VisibleEOSTab { get => visibleEOSTab; set { visibleEOSTab = value; setTabPages(); } }
    private bool visibleEOSTab = true;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool VisibleStressStrainTab { get => visibleStressStrainTab; set { visibleStressStrainTab = value; setTabPages(); } }
    private bool visibleStressStrainTab = false;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool VisiblePolycrystallineTab { get => visiblePolycrystallineTab; set { visiblePolycrystallineTab = value; setTabPages(); } }
    private bool visiblePolycrystallineTab = false;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool VisibleBoundTab { get => visibleBoundTab; set { visibleBoundTab = value; setTabPages(); } }
    private bool visibleBoundTab = false;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool VisibleLatticePlaneTab { get => visibleLatticePlaneTab; set { visibleLatticePlaneTab = value; setTabPages(); } }
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

    #endregion

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Crystal Crystal
    {
        get => crystal;
        set
        {
            crystal = value;
            if (crystal == null) return;

            Enabled = !crystal.FlexibleMode;
            SetToInterface();
            // 原子位置チェック (strain control で選択した後、原子位置がおかしくなる問題の修正 (2017/05/29))
            if (crystal.ChemicalFormulaZ == 1)
            {
                foreach (var a in crystal.Atoms)
                    a.ResetSymmetry(SymmetrySeriesNumber);
                crystal.GetFormulaAndDensity();
                SetToInterface();
            }
            CrystalChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    private Crystal crystal;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public (double A, double B, double C, double Alpha, double Beta, double Gamma) CellConstants
    {
        get => symmetryControl.CellConstants;
        set => symmetryControl.CellConstants = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public double A { get => symmetryControl.A; set => symmetryControl.A = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public double B { get => symmetryControl.B; set => symmetryControl.B = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public double C { get => symmetryControl.C; set => symmetryControl.C = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public double Alpha { get => symmetryControl.Alpha; set => symmetryControl.Alpha = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public double Beta { get => symmetryControl.Beta; set => symmetryControl.Beta = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public double Gamma { get => symmetryControl.Gamma; set => symmetryControl.Gamma = value; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int DefaultTabNumber { get => tabControl.SelectedIndex; set => tabControl.SelectedIndex = value; }

    public event EventHandler CrystalChanged;

    public FormScatteringFactor FormScatteringFactor;
    public FormSymmetryInformation FormSymmetryInformation;
    public FormStrain formStrain;

    #endregion

    public CrystalControl()
    {
        InitializeComponent();

        FormScatteringFactor = new FormScatteringFactor { CrystalControl = this, Visible = false };
        FormSymmetryInformation = new FormSymmetryInformation { CrystalControl = this, Visible = false };
        formStrain = new FormStrain { CrystalControl = this, Visible = false };

        FormScatteringFactor.VisibleChanged += formScatteringFactor_VisibleChanged;
        FormSymmetryInformation.VisibleChanged += formSymmetryInformation_VisibleChanged;
    }

    private void CrystalForm_Load(object sender, EventArgs e)
    {
        textBoxTitle.Size = new Size(tabPageReference.Width - textBoxTitle.Location.X - 2, tabPageReference.Height - textBoxTitle.Location.Y - 2);
        typeof(UserControl).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this, true, null);
    }

    public event EventHandler ScatteringFactor_VisibleChanged;
    public event EventHandler SymmetryInformation_VisibleChanged;

    #region Crystal クラスを画面下部から生成 / にセット

    /// <summary>画面下部に入力された内容から Crystal を生成する</summary>
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

        // 対称性が変更された可能性があるので原子も改めて設定し直す
        atomControl.ResetSymmetry(SymmetrySeriesNumber);

        var rot = crystal != null ? crystal.RotationMatrix : new Matrix3D();

        crystal = new Crystal(
            symmetryControl.CellConstants, symmetryControl.CellConstantsErr,
            SymmetrySeriesNumber, textBoxName.Text, colorControl.Color, rot, atomControl.GetAll(),
            (textBoxMemo.Text, textBoxAuthor.Text, textBoxJournal.Text, textBoxTitle.Text),
            bondControl.GetAll(), boundControl.GetAll(), latticePlaneControl.GetAll(), eosControl.EOScondition);

        crystal.ElasticStiffness = elasticityControl1.Stiffness.ToArray();

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

        SkipEvent = false;
        SetToInterface(false);
        CrystalChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>現在の Crystal によって画面下部のテキストボックスなどを設定する</summary>
    /// <param name="ChangeCellParameter">CellParameter コントロールも更新する場合 true</param>
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
        numericBoxVolumeAng.Value = Crystal.Volume * 1000;
        numericBoxCellVolumeNm.Value = Crystal.Volume;
        numericBoxMolarVolume.Value = Crystal.Volume * UniversalConstants.A / Crystal.ChemicalFormulaZ * 1E-21;
        numericBoxZnumber.Value = Crystal.ChemicalFormulaZ;
        numericBoxMolarMass.Value = numericBoxDensity.Value * numericBoxMolarVolume.Value;
        numericBoxCellMass.Value = numericBoxDensity.Value * numericBoxVolumeAng.Value;

        SymmetrySeriesNumber = Crystal.SymmetrySeriesNumber; // setter 内でコンボボックスをセットする (20170526)

        if (ChangeCellParameter)
        {
            symmetryControl.CellConstants = (Crystal.A, Crystal.B, Crystal.C, Crystal.Alpha, Crystal.Beta, Crystal.Gamma);
            symmetryControl.CellConstantsErr = (Crystal.A_err, Crystal.B_err, Crystal.C_err, Crystal.Alpha_err, Crystal.Beta_err, Crystal.Gamma_err);
        }

        atomControl.Crystal = crystal;
        bondControl.Crystal = crystal;
        boundControl.Crystal = crystal;
        latticePlaneControl.Crystal = crystal;
        eosControl.Crystal = crystal;

        elasticityControl1.Stiffness = DenseMatrix.OfArray(crystal.ElasticStiffness);

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
        catch (Exception ex) { if (AssemblyState.IsDebug) MessageBox.Show(ex.ToString()); }
    }

    #region ドラッグドロップ
    public void FormCrystal_DragDrop(object sender, DragEventArgs e)
    {
        var fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        if (fileName.Length == 1)
            ReadCrystal(fileName[0]);
    }

    private void FormCrystal_DragEnter(object sender, DragEventArgs e) =>
        e.Effect = e.Data.GetData(DataFormats.FileDrop) != null ? DragDropEffects.Copy : DragDropEffects.None;
    #endregion

    private void CrystalControl_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.Shift && e.KeyCode == Keys.R)
            crystal.Reserved = !crystal.Reserved;
    }

    /// <summary>外部から呼び出される EOS 計算</summary>
    public void CalculateEOS() => eosControl.CalculatePressure();

    #region Polycrystalline 関連

    private void buttonGenerateRandomOrientations_Click(object sender, EventArgs e)
    {
        if (Crystal.Crystallites == null)
            Crystal.SetCrystallites();
        poleFigureControl.Crystal = Crystal;
    }

    public void DrawPoleFigure() => poleFigureControl.Draw(true);

    private void numericUpDownAngleResolution_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        GenerateFromInterface();
    }
    #endregion

    #region poleFigure の右クリックメニュー

    // BinaryFormatter が使えなくなったので read/save は無効化 (2022/11/10)
    private void readToolStripMenuItem_Click(object sender, EventArgs e) { }
    private void saveToolStripMenuItem_Click(object sender, EventArgs e) { }

    /// <summary>ctf ファイルで出力 (Channel5 互換)</summary>
    private void asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (crystal.Crystallites == null) return;
        const int maxCrystallites = 499900;

        var dlg = new SaveFileDialog { Filter = "*.ctf|*.ctf" };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        var sw = new StreamWriter(dlg.FileName);
        sw.WriteLine("Channel Text File");
        sw.WriteLine("Prj\t OutPut from Recipro");
        sw.WriteLine("Author\t[Unknown]");
        sw.WriteLine("JobMode\tInteractive");
        sw.WriteLine($"NoMeas\t{maxCrystallites}");
        sw.WriteLine("AcqE1\t0");
        sw.WriteLine("AcqE2\t0");
        sw.WriteLine("AcqE3\t0");
        sw.WriteLine("Euler angles refer to Sample Coordinate system (CS0)!\tMag\t0\tCoverage\t0\tDevice\t0\tKV\t0\tTiltAngle\t0\tTiltAxis\t0");
        sw.WriteLine("Phases\t1");
        sw.WriteLine($"0.000;0.000;0.000\t90;90;90\t{Crystal.Name}\t3\t0\t3803863129_5.0.6.3\t1060505527\t[{crystal.Name}]");
        sw.WriteLine("Phase\tX\tY\tBands\tError\tEuler1\tEuler2\tEuler3\tMAD\tBC\tBS");

        double sum = 0;
        for (int i = 0; i < Crystal.Crystallites.TotalCrystalline; i++)
            sum += crystal.Crystallites.Density[i] * crystal.Crystallites.SolidAngle[i];

        double tempSum = 0;
        int seed = 0;
        for (int i = 0; i < maxCrystallites; i++)
        {
            double partialSum = (i + 0.5) / maxCrystallites * sum;
            while (tempSum + Crystal.Crystallites.Density[seed] * Crystal.Crystallites.SolidAngle[seed] < partialSum && seed < Crystal.Crystallites.Density.Length)
            {
                tempSum += Crystal.Crystallites.Density[seed] * Crystal.Crystallites.SolidAngle[seed];
                seed++;
            }

            var (Phi, Theta, Psi) = Euler.FromMatrix(Crystal.Crystallites.Rotations[seed]);
            string str = "";
            foreach (double angle in (double[])[Phi, Theta, Psi])
            {
                double d = (angle > 0 ? angle : angle + 2 * Math.PI) / Math.PI * 180;
                str += d switch
                {
                    >= 100 => $"{d:000.00}\t",
                    >= 10 => $"{d:00.000}\t",
                    _ => $"{d:0.0000}\t"
                };
            }
            sw.WriteLine($"1\t0\t0\t0\t0\t{str}0\t0\t0");
        }
        sw.Close();
    }

    /// <summary>txt ファイルで出力 (オイラー角と密度)</summary>
    private void asTXTFileAllEulerAngleAndDensityToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (crystal.Crystallites == null) return;

        var dlg = new SaveFileDialog { Filter = "*.txt|*.txt" };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        using var sw = new StreamWriter(dlg.FileName);
        sw.WriteLine($"Sample Name:\t{Crystal.Name}");
        sw.WriteLine($"Cell constants:\t{Crystal.A}\t{Crystal.B}\t{Crystal.C}\t{crystal.Alpha / Math.PI * 180}\t{crystal.Beta / Math.PI * 180}\t{crystal.Gamma / Math.PI * 180}");
        sw.WriteLine($"Space group:\t{Crystal.Symmetry.SpaceGroupHMfullStr}");
        sw.WriteLine();
        sw.WriteLine("Euler angles refer to Sample Coordinate system");
        sw.WriteLine("No.\tEuler1\tEuler2\tEuler3\tDensity");

        double sum = 0;
        var density = new double[Crystal.Crystallites.TotalCrystalline];
        var index = new int[Crystal.Crystallites.TotalCrystalline];
        for (int i = 0; i < Crystal.Crystallites.TotalCrystalline; i++)
        {
            density[i] = crystal.Crystallites.Density[i] * crystal.Crystallites.SolidAngle[i];
            index[i] = i;
            sum += density[i];
        }
        if (sender == asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem)
        {
            Array.Sort(density, index);
            density = [.. density.Reverse()];
            index = [.. index.Reverse()];
        }

        for (int i = 0; i < Crystal.Crystallites.TotalCrystalline; i++)
        {
            var (Phi, Theta, Psi) = Euler.FromMatrix(Crystal.Crystallites.Rotations[index[i]]);
            string str = $"{i}\t";
            foreach (double angle in (double[])[Phi, Theta, Psi])
            {
                double d = (angle > 0 ? angle : angle + 2 * Math.PI) / Math.PI * 180;
                str += d.ToString("000.0000") + "\t";
            }
            str += (density[i] / sum * crystal.Crystallites.TotalCrystalline).ToString();
            sw.WriteLine(str);
        }
    }
    #endregion

    #region 子コントロールの変化イベント
    private void atomControl_AtomsChanged(object sender, EventArgs e) => GenerateFromInterface();

    private void symmetryControl_ItemChanged(object sender, EventArgs e)
    {
        GenerateFromInterface();

        // 同じ空間群番号の候補が複数あって P1 / P-1 でない時のみボタン有効化
        var array = SymmetryStatic.NumArray;
        var sn = symmetryControl.SymmetrySeriesNumber;
        buttonChangeAxesOriginSetting.Enabled = convertToAnotherSpacegroupToolStripMenuItem.Enabled = array[sn][1] > 2 && array.Count(e => e[1] == array[sn][1]) > 1;
    }

    private void bondControl_ItemsChanged(object sender, EventArgs e)
    {
        // bondControl の ItemChanged は StructureViewer などに直接登録されているのでここでは何もしない
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

    #region SymmetryInformation / ScatteringFactor の表示
    private void buttonSymmetryInfo_Click(object sender, EventArgs e) => FormSymmetryInformation.Visible = !FormSymmetryInformation.Visible;
    private void formSymmetryInformation_VisibleChanged(object sender, EventArgs e) => SymmetryInformation_VisibleChanged?.Invoke(sender, e);

    private void buttonScatteringFactor_Click(object sender, EventArgs e) => FormScatteringFactor.Visible = !FormScatteringFactor.Visible;
    private void formScatteringFactor_VisibleChanged(object sender, EventArgs e) => ScatteringFactor_VisibleChanged?.Invoke(sender, e);
    #endregion

    #region リサイズ中は描画停止
    private bool registResizeEvent = false;
    private void CrystalControl_Resize_1(object sender, EventArgs e)
    {
        if (DesignMode || registResizeEvent) return;
        var parent = Parent;
        while (parent is not Form && parent != null)
            parent = parent.Parent;
        if (parent is not Form form) return;
        form.ResizeBegin += (s, ea) => SuspendLayout();
        form.ResizeEnd += (s, ea) => ResumeLayout();
        registResizeEvent = true;
    }
    #endregion

    #region 右クリックメニュー
    private void importCrystalFromCIFAMCToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var dlg = new OpenFileDialog { Filter = " *.cif; *.amc | *.cif;*.amc" };
        if (dlg.ShowDialog() == DialogResult.OK)
            ReadCrystal(dlg.FileName);
    }

    public void exportThisCrystalAsCIFToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Crystal == null) return;
        var dlg = new SaveFileDialog { Filter = " *.cif| *.cif", FileName = $"{Crystal.Name}.cif" };
        if (dlg.ShowDialog() == DialogResult.OK)
            Crystal.ExportCIF(dlg.FileName);
    }

    private void scatteringFactorToolStripMenuItem_Click(object sender, EventArgs e) => FormScatteringFactor.Visible = !FormScatteringFactor.Visible;
    private void symmetryInformationToolStripMenuItem_Click(object sender, EventArgs e) => FormSymmetryInformation.Visible = !FormSymmetryInformation.Visible;

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
        formStrain.crystal ??= Crystal;
        formStrain.Visible = !formStrain.Visible;
    }

    /// <summary>空間群 P1 に変換</summary>
    private void convertToP1ToolStripMenuItem_Click(object sender, EventArgs e) => toSuperStructure(1, 1, 1);

    /// <summary>超構造に変換</summary>
    private void convertToSuperstructureToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var dlg = new FormSuperStructure();
        if (dlg.ShowDialog() == DialogResult.OK)
            toSuperStructure(dlg.A, dlg.B, dlg.C);
    }

    private void convertToAnotherSpacegroupToolStripMenuItem_Click(object sender, EventArgs e) => ChangeAxesOriginSetting();
    private void buttonChangeAxesOriginSetting_Click(object sender, EventArgs e) => ChangeAxesOriginSetting();
    #endregion

    #region 空間群を変換する関数群

    /// <summary>超構造に変換</summary>
    private void toSuperStructure(int _u, int _v, int _w)
    {
        GenerateFromInterface();
        crystal.SymmetrySeriesNumber = 1;

        var temp_atoms = new List<Atoms>();
        foreach (var atoms in Crystal.Atoms)
        {
            int n = 0;
            foreach (var atom in atoms.Atom)
                for (double u = 0; u < _u; u++)
                    for (double v = 0; v < _v; v++)
                        for (double w = 0; w < _w; w++)
                        {
                            temp_atoms.Add(new Atoms(
                                $"{atoms.Label.TrimEnd()}_{n}",
                                atoms.AtomicNumber, atoms.SubNumberXray, atoms.SubNumberElectron, atoms.Isotope,
                                1,
                                new Vector3DBase((atom.X + u) / _u, (atom.Y + v) / _v, (atom.Z + w) / _w),
                                new Vector3DBase(atoms.X_err / _u, atoms.Y_err / _v, atoms.Z_err / _w),
                                atoms.Occ, atoms.Occ_err, atoms.Dsf, atoms.Material,
                                atoms.Radius, atoms.GLEnabled, atoms.ShowLabel));
                            n++;
                        }
        }
        crystal.A *= _u;
        crystal.B *= _v;
        crystal.C *= _w;
        crystal.Atoms = [.. temp_atoms];

        SetToInterface(true);
        GenerateFromInterface();
    }

    public void ChangeAxesOriginSetting()
    {
        var seriesNum = symmetryControl.SymmetrySeriesNumber;
        var spNum = SymmetryStatic.NumArray[seriesNum][1];

        // 自分自身を除く、同じ空間群番号のものを候補に挙げる
        var list = new List<(int SeriesNum, string Notation)>();
        foreach (var n in SymmetryStatic.NumArray)
            if (spNum == n[1] && seriesNum != n[0])
                list.Add((n[0], SymmetryStatic.StrArray[n[0]][3]));

        var dlg = new FormAnotherSpaceGroup { Candidates = [.. list] };
        if (dlg.ShowDialog() == DialogResult.OK)
            toAnotherSpaceGroup(dlg.SeriesNum);
    }

    /// <summary>別の空間群に変換 (原子位置/格子定数のエラーや熱散漫散乱因子の変換は未対応)</summary>
    public void toAnotherSpaceGroup(int destNum)
    {
        var srcNum = symmetryControl.SymmetrySeriesNumber;
        var crystalSystem = SymmetryStatic.NumArray[srcNum][5];
        var sgNum = SymmetryStatic.NumArray[srcNum][1];
        var srcExtra = SymmetryStatic.StrArray[srcNum][0];
        var dstExtra = SymmetryStatic.StrArray[destNum][0];

        if (crystalSystem == 2) // monoclinic: 軸変換のみ
        {
            if (srcExtra.Length == 1) srcExtra += "1";
            if (dstExtra.Length == 1) dstExtra += "1";

            var cry = Deep.Copy(Crystal);
            cry.SymmetrySeriesNumber = destNum;
            cry.SetAxis();
            foreach (var a in cry.Atoms)
            {
                (a.X, a.Y, a.Z) = exchangeAtomPositionMonoclinic(a.X, a.Y, a.Z, srcExtra, false);
                (a.X, a.Y, a.Z) = exchangeAtomPositionMonoclinic(a.X, a.Y, a.Z, dstExtra, true);
            }
            var (A, B, C) = convertAxisMonoclinic(cry.A_Axis, cry.B_Axis, cry.C_Axis, srcExtra, false);
            (A, B, C) = convertAxisMonoclinic(A, B, C, dstExtra, true);
            cry.A = A.Length; cry.B = B.Length; cry.C = C.Length;
            cry.Alpha = Vector3D.AngleBetVectors(B, C);
            cry.Beta = Vector3D.AngleBetVectors(C, A);
            cry.Gamma = Vector3D.AngleBetVectors(A, B);
            crystal = cry;
        }

        if (crystalSystem == 3) // Orthorhombic: 軸変換 + Origin Choice 変換
        {
            var src = convOrtho(srcExtra);
            var dst = convOrtho(dstExtra);

            var cry = Deep.Copy(Crystal);
            cry.SymmetrySeriesNumber = destNum;
            foreach (var a in cry.Atoms)
            {
                (a.X, a.Y, a.Z) = exchangeOrtho(a.X, a.Y, a.Z, src.Setting);
                (a.X_err, a.Y_err, a.Z_err) = exchangeOrtho(a.X_err, a.Y_err, a.Z_err, src.Setting);
                if (src.Origin != dst.Origin)
                    (a.X, a.Y, a.Z) = shift(a.X, a.Y, a.Z, sgNum, src.Origin == 1);
                (a.X, a.Y, a.Z) = exchangeOrtho(a.X, a.Y, a.Z, dst.Setting, true);
                (a.X_err, a.Y_err, a.Z_err) = exchangeOrtho(a.X_err, a.Y_err, a.Z_err, dst.Setting, true);
            }
            (cry.A, cry.B, cry.C) = exchangeOrtho(cry.A, cry.B, cry.C, src.Setting, false, true);
            (cry.A, cry.B, cry.C) = exchangeOrtho(cry.A, cry.B, cry.C, dst.Setting, true, true);
            crystal = cry;
        }

        if (crystalSystem is 4 or 7) // Tetragonal / Cubic: Origin Choice 変換のみ
        {
            var src = convOrtho(srcExtra);
            var cry = Deep.Copy(Crystal);
            cry.SymmetrySeriesNumber = destNum;
            foreach (var a in cry.Atoms)
                (a.X, a.Y, a.Z) = shift(a.X, a.Y, a.Z, sgNum, src.Origin == 1);
            crystal = cry;
        }

        if (crystalSystem == 5) // trigonal: Rhombo / Hexa の変換
        {
            var cry = Deep.Copy(Crystal);
            cry.SymmetrySeriesNumber = destNum;
            cry.SetAxis();

            Vector3DBase srcA = cry.A_Axis, srcB = cry.B_Axis, srcC = cry.C_Axis, dstA, dstB, dstC;
            if (srcExtra == "H")
            {
                foreach (var a in cry.Atoms)
                    (a.X, a.Y, a.Z) = (a.X + a.Z, -a.X + a.Y + a.Z, a.Y + a.Z);
                (dstA, dstB, dstC) = ((2 * srcA + srcB + srcC) / 3, (-srcA + srcB + srcC) / 3, (-srcA - srcB + srcC) / 3);
            }
            else
            {
                foreach (var a in cry.Atoms)
                    (a.X, a.Y, a.Z) = ((2 * a.X - a.Y - a.Z) / 3, (a.X + a.Y - 2 * a.Z) / 3, (a.X + a.Y + a.Z) / 3);
                (dstA, dstB, dstC) = (srcA - srcB, srcB - srcC, srcA + srcB + srcC);
            }
            cry.A = dstA.Length; cry.B = dstB.Length; cry.C = dstC.Length;
            cry.Alpha = Vector3DBase.AngleBetVectors(dstB, dstC);
            cry.Beta = Vector3DBase.AngleBetVectors(dstC, dstA);
            cry.Gamma = Vector3DBase.AngleBetVectors(dstA, dstB);
            crystal = cry;
        }

        SetToInterface(true);
        GenerateFromInterface();
    }

    private const double one4th = 1.0 / 4.0, one8th = 1.0 / 8.0, three8th = 3.0 / 8.0;

    private static (double X, double Y, double Z) shift(double x, double y, double z, int sgNum, bool to2nd)
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

    /// <summary>原子位置を変換 (Monoclinic 用)</summary>
    private static (double X, double Y, double Z) exchangeAtomPositionMonoclinic(double x, double y, double z, string setting, bool forward = true)
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

    private static (Vector3DBase A, Vector3DBase B, Vector3DBase C) convertAxisMonoclinic(Vector3DBase a, Vector3DBase b, Vector3DBase c, string setting, bool forward = true)
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

    private static (double X, double Y, double Z) exchangeOrtho(double x, double y, double z, int[] setting, bool inverse = false, bool abs = false)
    {
        double[] src = [x, y, z], dst = new double[3];
        for (int i = 0; i < 3; i++)
        {
            if (!inverse)
                dst[Math.Abs(setting[i]) - 1] = setting[i] > 0 ? src[i] : -src[i];
            else
                dst[i] = setting[i] > 0 ? src[Math.Abs(setting[i]) - 1] : -src[Math.Abs(setting[i]) - 1];
        }
        return abs ? (Math.Abs(dst[0]), Math.Abs(dst[1]), Math.Abs(dst[2])) : (dst[0], dst[1], dst[2]);
    }

    /// <summary>extra 文字列を解析 (例: "2ba-c" → (2, {2, 1, -3}))</summary>
    private static (int Origin, int[] Setting) convOrtho(string s)
    {
        if (s.Length == 0) return (1, [1, 2, 3]);
        int origin = 1;
        if (s[0] is '1' or '2') // OriginChoice 変換があるのは 48, 50, 59, 68, 70 など
        {
            origin = s[0] == '1' ? 1 : 2;
            s = s[1..];
        }
        if (s.Length == 0) return (origin, [1, 2, 3]);

        var setting = new int[3];
        for (int i = 0; i < 3; i++)
        {
            setting[i] = s[0] != '-' ? 1 : -1;
            if (s[0] == '-') s = s[1..];
            setting[i] *= s[0] switch { 'a' => 1, 'b' => 2, _ => 3 };
            s = s[1..];
        }
        return (origin, setting);
    }
    #endregion

    private void buttonStressSet_Click(object sender, EventArgs e) => GenerateFromInterface();

    /// <summary>Miller-Bravais 指数を有効化</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MillerBravais
    {
        set => FormScatteringFactor.MillerBravais = value && crystal.MillerBravaisCapable;
    }
}

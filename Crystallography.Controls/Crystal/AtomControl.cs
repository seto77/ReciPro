using Microsoft.Scripting.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class AtomControl : CaptureUserControlBase
{
    #region プロパティ, フィールド, イベントハンドラ
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Crystal Crystal
    {
        get => crystal;
        set
        {
            crystal = value;
            if (crystal == null) return;
            SuspendLayout();
            table.Clear();
            AddRange(Crystal.Atoms);
            // enabledColumn の Visible が予期せず変わることがあるので appearanceTabVisible で再設定
            dataGridView.Columns["enabledColumn"].Visible = appearanceTabVisible;
            ResumeLayout();
        }
    }
    private Crystal crystal = null;

    public int SymmetrySeriesNumber => crystal != null ? crystal.SymmetrySeriesNumber : 0;

    private readonly DataSet.DataTableAtomDataTable table;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SkipEvent { get; set; } = false;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool AtomicPositionError
    {
        get => atmicPositionError;
        set
        {
            atmicPositionError = value;
            if (!value)
            {
                tableLayoutPanel1.ColumnStyles[4].SizeType = tableLayoutPanel1.ColumnStyles[7].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[4].Width = tableLayoutPanel1.ColumnStyles[7].Width = 0;
                numericBoxXerr.TabStop = numericBoxYerr.TabStop = numericBoxZerr.TabStop = numericBoxOccerr.TabStop = false;
            }
            else
            {
                foreach (int i in (int[])[1, 3, 4, 6, 7])
                {
                    tableLayoutPanel1.ColumnStyles[i].SizeType = SizeType.Percent;
                    tableLayoutPanel1.ColumnStyles[i].Width = 20;
                }
                numericBoxXerr.TabStop = numericBoxYerr.TabStop = numericBoxZerr.TabStop = numericBoxOccerr.TabStop = true;
            }
        }
    }
    private bool atmicPositionError = false;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool DebyeWallerError
    {
        get => debyeWallerError;
        set
        {
            debyeWallerError = value;
            numericBoxBisoerr.Visible = numericBoxB11err.Visible = numericBoxB12err.Visible =
                numericBoxB13err.Visible = numericBoxB22err.Visible = numericBoxB23err.Visible = numericBoxB33err.Visible = value;
            if (value) numericBoxBiso.Visible = true;
        }
    }
    private bool debyeWallerError = false;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseIsotropy
    {
        get => radioButtonIsotoropy.Checked;
        set { if (value) radioButtonIsotoropy.Checked = true; else radioButtonAnisotropy.Checked = true; }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseTypeU
    {
        get => radioButtonDebyeWallerTypeU.Checked;
        set { if (value) radioButtonDebyeWallerTypeU.Checked = true; else radioButtonDebyeWallerTypeB.Checked = true; }
    }

    #region 温度因子 プロパティ
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Iso { get => numericBoxBiso.Value; set => numericBoxBiso.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double IsoErr { get => numericBoxBisoerr.Value; set => numericBoxBisoerr.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso11 { get => numericBoxB11.Value; set => numericBoxB11.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso11Err { get => numericBoxB11err.Value; set => numericBoxB11err.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso12 { get => numericBoxB12.Value; set => numericBoxB12.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso12Err { get => numericBoxB12err.Value; set => numericBoxB12err.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso13 { get => numericBoxB13.Value; set => numericBoxB13.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso13Err { get => numericBoxB13err.Value; set => numericBoxB13err.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso22 { get => numericBoxB22.Value; set => numericBoxB22.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso22Err { get => numericBoxB22err.Value; set => numericBoxB22err.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso23 { get => numericBoxB23.Value; set => numericBoxB23.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso23Err { get => numericBoxB23err.Value; set => numericBoxB23err.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso33 { get => numericBoxB33.Value; set => numericBoxB33.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Aniso33Err { get => numericBoxB33err.Value; set => numericBoxB33err.Value = value; }
    #endregion

    #region 原子位置 プロパティ
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double X { get => numericBoxX.Value; set => numericBoxX.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double XErr { get => numericBoxXerr.Value; set => numericBoxXerr.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Y { get => numericBoxY.Value; set => numericBoxY.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double YErr { get => numericBoxYerr.Value; set => numericBoxYerr.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Z { get => numericBoxZ.Value; set => numericBoxZ.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double ZErr { get => numericBoxZerr.Value; set => numericBoxZerr.Value = value; }
    #endregion

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double Occ { get => numericBoxOcc.Value; set => numericBoxOcc.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public double OccErr { get => numericBoxOccerr.Value; set => numericBoxOccerr.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public string Label { get => textBoxLabel.Text; set => textBoxLabel.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public int AtomNo { get => comboBoxAtom.SelectedIndex + 1; set => comboBoxAtom.SelectedIndex = value - 1; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public int AtomSubNoXray { get => comboBoxScatteringFactorXray.SelectedIndex; set => comboBoxScatteringFactorXray.SelectedIndex = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Atom")] public int AtomSubNoElectron { get => comboBoxScatteringFactorElectron.SelectedIndex; set => comboBoxScatteringFactorElectron.SelectedIndex = value; }

    private double[] isotopicComposition;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double[] IsotopicComposition
    {
        get => isotopicComposition;
        set
        {
            isotopicComposition = value;
            comboBoxNeutron.SelectedIndex = (isotopicComposition == null || isotopicComposition.Length != AtomStatic.IsotopeAbundance[AtomNo].Count) ? 0 : 1;
            comboBoxNeutron_SelectedIndexChanged(this, EventArgs.Empty);
        }
    }

    #region マテリアル プロパティ
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Material properties")] public float Ambient { get => (float)numericBoxAmbient.Value; set => numericBoxAmbient.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Material properties")] public float Diffusion { get => (float)numericBoxDiffusion.Value; set => numericBoxDiffusion.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Material properties")] public float Specular { get => (float)numericBoxSpecular.Value; set => numericBoxSpecular.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Material properties")] public float Shininess { get => (float)numericBoxShininess.Value; set => numericBoxShininess.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Material properties")] public float Emission { get => (float)numericBoxEmission.Value; set => numericBoxEmission.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Material properties")] public float Alpha { get => (float)numericBoxAlpha.Value; set => numericBoxAlpha.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Material properties")] public double Radius { get => numericBoxAtomRadius.Value; set => numericBoxAtomRadius.Value = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Material properties")] public Color AtomColor { get => colorControlAtomColor.Color; set => colorControlAtomColor.Color = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Material properties")] public bool ShowLabel { get => checkBoxShowLabel.Checked; set => checkBoxShowLabel.Checked = value; }
    #endregion

    #region Tab の表示/非表示 プロパティ
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Tab")] public bool ElementAndPositionTabVisible { get => elementAndPositionTabVisible; set { elementAndPositionTabVisible = value; setTabPages(); } }
    private bool elementAndPositionTabVisible = true;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Tab")] public bool OriginShiftVisible { get => originShiftTabVisible; set { originShiftTabVisible = value; setTabPages(); } }
    private bool originShiftTabVisible = true;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Tab")] public bool DebyeWallerTabVisible { get => debyeWallerTabVisible; set { debyeWallerTabVisible = value; setTabPages(); } }
    private bool debyeWallerTabVisible = true;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Tab")] public bool ScatteringFactorTabVisible { get => scatteringFactorTabVisible; set { scatteringFactorTabVisible = value; setTabPages(); } }
    private bool scatteringFactorTabVisible = true;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Tab")] public bool AppearanceTabVisible { get => appearanceTabVisible; set { appearanceTabVisible = value; setTabPages(); } }
    private bool appearanceTabVisible = true;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] [Category("Tab")] public int SelectedTabIndex { get => tabControl.SelectedIndex; set => tabControl.SelectedIndex = value; }
    #endregion

    /// <summary>原子のパラメータが変更された時のイベント</summary>
    public event EventHandler ItemsChanged;

    /// <summary>GLEnabled チェックが変更された時のみのイベント (現状 FormStructure のみが受け取る)</summary>
    public event EventHandler GLEnableChanged;

    #endregion

    public AtomControl()
    {
        InitializeComponent();
        if (DesignMode) return; // (260322Ch) Designer 安定化: InitializeComponent 後に design-time 追加初期化だけ止める
        SkipEvent = true;
        table = dataSet.DataTableAtom;
        comboBoxAtom.SelectedIndex = 0;
        comboBoxNeutron.SelectedIndex = 0;

        // 一部 numericBox の Up/Down が消えるので強制 ON
        numericBoxAmbient.ShowUpDown = numericBoxDiffusion.ShowUpDown = numericBoxSpecular.ShowUpDown = numericBoxShininess.ShowUpDown =
            numericBoxEmission.ShowUpDown = numericBoxAlpha.ShowUpDown = numericBoxAtomRadius.ShowUpDown = true;

        dataGridView.Columns["enabledColumn"].Visible = false;
        SkipEvent = false;

        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);
    }

    private void setTabPages()
    {
        tabControl.TabPages.Clear();
        if (ElementAndPositionTabVisible) tabControl.TabPages.Add(tabPageElementAndPosition);
        if (originShiftTabVisible) tabControl.TabPages.Add(tabPageOriginShift);
        if (DebyeWallerTabVisible) tabControl.TabPages.Add(tabPageDebyeWaller);
        if (ScatteringFactorTabVisible) tabControl.TabPages.Add(tabPageScatteringFactor);
        if (AppearanceTabVisible) tabControl.TabPages.Add(tabPageAppearance);
    }

    private void radioButtonIsotoropy_CheckedChanged(object sender, EventArgs e)
    {
        flowLayoutPanelAniso1.Visible = flowLayoutPanelAniso2.Visible = !radioButtonIsotoropy.Checked;
        flowLayoutPanelIso.Visible = radioButtonIsotoropy.Checked;
        labelDimension.Text = radioButtonDebyeWallerTypeB.Checked && radioButtonAnisotropy.Checked ? "None" : "Å²";
    }

    private void radioButtonDebyeWallerTypeU_CheckedChanged(object sender, EventArgs e)
    {
        var prefix = radioButtonDebyeWallerTypeU.Checked ? "U" : "B";
        numericBoxBiso.HeaderText = $"{prefix}iso";
        numericBoxB11.HeaderText = $"{prefix}11";
        numericBoxB22.HeaderText = $"{prefix}22";
        numericBoxB33.HeaderText = $"{prefix}33";
        numericBoxB12.HeaderText = $"{prefix}12";
        numericBoxB23.HeaderText = $"{prefix}23";
        numericBoxB13.HeaderText = $"{prefix}13";
        labelDimension.Text = radioButtonDebyeWallerTypeB.Checked && radioButtonAnisotropy.Checked ? "None" : "Å²";
    }

    private void comboBoxAtom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        if (comboBoxAtom.SelectedIndex < 0) return;

        // X 線散乱因子のメソッド一覧をコンボに追加
        comboBoxScatteringFactorXray.Items.Clear();
        foreach (var wk in AtomStatic.XrayScatteringWK[AtomNo])
            comboBoxScatteringFactorXray.Items.Add(wk.Method);
        comboBoxScatteringFactorXray.SelectedIndex = 0;

        // 電子散乱因子のメソッド一覧
        comboBoxScatteringFactorElectron.Items.Clear();
        foreach (var p in AtomStatic.ElectronScatteringPeng[AtomNo])
            comboBoxScatteringFactorElectron.Items.Add(p.Method);
        comboBoxScatteringFactorElectron.SelectedIndex = 0;

        // 中性子散乱長
        comboBoxNeutron.SelectedIndex = 0;
        comboBoxNeutron_SelectedIndexChanged(this, EventArgs.Empty);

        // 原子サイズ・色
        var (radius, argb) = AtomStatic.GetVesta(AtomNo);
        Radius = radius;
        AtomColor = Color.FromArgb(argb);
    }

    private void comboBoxAtomSub_SelectedIndexChanged(object sender, EventArgs e) { }

    private void checkBoxAtomicPositionError_CheckedChanged(object sender, EventArgs e) =>
        AtomicPositionError = checkBoxDetailAtomicPositionError.Checked;

    private void checkBoxDebyeWallerError_CheckedChanged(object sender, EventArgs e) =>
        DebyeWallerError = checkBoxDetailsDebyeWallerError.Checked;

    #region 中性子関連
    private void comboBoxNeutron_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;

        buttonEditIsotopeAbundance.Enabled = comboBoxNeutron.SelectedIndex == 1;

        richTextBoxIsotope.Clear();
        int n = 0;
        var abundance = AtomStatic.IsotopeAbundance[AtomNo];
        bool useCustomIso = comboBoxNeutron.SelectedIndex == 1 && isotopicComposition != null && isotopicComposition.Length == abundance.Count;
        var fontSmall = new Font("Tahoma", 6f, FontStyle.Regular);
        var fontNormal = new Font("Tahoma", 9f, FontStyle.Regular);
        var elementName = AtomStatic.AtomicName(AtomNo);
        foreach (int z in abundance.Keys)
        {
            richTextBoxIsotope.SelectionColor = Color.DarkBlue;
            if (richTextBoxIsotope.Text != "")
                richTextBoxIsotope.SelectedText = ", ";

            richTextBoxIsotope.SelectionCharOffset = 3;
            richTextBoxIsotope.SelectionFont = fontSmall;
            richTextBoxIsotope.SelectedText = z.ToString();

            richTextBoxIsotope.SelectionCharOffset = 0;
            richTextBoxIsotope.SelectionFont = fontNormal;
            richTextBoxIsotope.SelectedText = $"{elementName}: ";

            richTextBoxIsotope.SelectionColor = Color.Black;
            richTextBoxIsotope.SelectedText = (useCustomIso ? isotopicComposition[n++] : abundance[z]).ToString();

            richTextBoxIsotope.SelectionColor = Color.DarkBlue;
            richTextBoxIsotope.SelectionFont = fontNormal;
            richTextBoxIsotope.SelectedText = "%";
        }
    }

    private void buttonEditIsotopeAbundance_Click(object sender, EventArgs e)
    {
        var f = new FormIsotopeComposition { AtomNumber = AtomNo, IsotopicComposition = isotopicComposition };
        if (f.ShowDialog() == DialogResult.OK)
            IsotopicComposition = f.IsotopicComposition;
    }
    #endregion

    #region データベース操作
    public void Add(Atoms atoms)
    {
        if (atoms != null) table.Add(atoms);
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddRange(IEnumerable<Atoms> atoms)
    {
        if (atoms == null) return;
        SkipEvent = true;
        dataGridView.SuspendLayout();
        foreach (var a in atoms) table.Add(a);
        dataGridView.ResumeLayout();
        SkipEvent = false;
        ItemsChanged?.Invoke(this, EventArgs.Empty);
        bindingSource_PositionChanged(this, EventArgs.Empty);
    }

    public void Delete(int i) { table.Remove(i); ItemsChanged?.Invoke(this, EventArgs.Empty); }
    public void Replace(Atoms atoms, int i) { table.Replace(atoms, i); ItemsChanged?.Invoke(this, EventArgs.Empty); }
    public void Clear() { table.Rows.Clear(); ItemsChanged?.Invoke(this, EventArgs.Empty); }

    /// <summary>指定した空間群番号に従って全ての原子情報を再設定</summary>
    public void ResetSymmetry(int symmetrySeriesNumber)
    {
        for (int i = 0; i < table.Rows.Count; i++)
        {
            var a = table.Get(i);
            a.ResetSymmetry(SymmetrySeriesNumber);
            table.Replace(a, i);
        }
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    public Atoms[] GetAll() => table.GetAll();
    #endregion

    #region Atom クラスを画面下部から生成 / 表示
    public void SetToInterface(Atoms atoms)
    {
        Label = atoms.Label; AtomNo = atoms.AtomicNumber;
        AtomSubNoXray = atoms.SubNumberXray;
        AtomSubNoElectron = atoms.SubNumberElectron;
        IsotopicComposition = atoms.Isotope;

        X = atoms.X; XErr = atoms.X_err;
        Y = atoms.Y; YErr = atoms.Y_err;
        Z = atoms.Z; ZErr = atoms.Z_err;
        Occ = atoms.Occ; OccErr = atoms.Occ_err;

        UseIsotropy = atoms.Dsf.UseIso;
        UseTypeU = atoms.Dsf.OriginalType == DiffuseScatteringFactor.Type.U;

        var d = atoms.Dsf;
        Iso = (UseTypeU ? d.Uiso : d.Biso) * 100;
        Aniso11 = UseTypeU ? d.U11 * 100 : d.B11;
        Aniso12 = UseTypeU ? d.U12 * 100 : d.B12;
        Aniso13 = UseTypeU ? d.U31 * 100 : d.B31;
        Aniso22 = UseTypeU ? d.U22 * 100 : d.B22;
        Aniso23 = UseTypeU ? d.U23 * 100 : d.B23;
        Aniso33 = UseTypeU ? d.U33 * 100 : d.B33;

        IsoErr = (UseTypeU ? d.Uiso_err : d.Biso_err) * 100;
        Aniso11Err = UseTypeU ? d.U11_err * 100 : d.B11_err;
        Aniso12Err = UseTypeU ? d.U12_err * 100 : d.B12_err;
        Aniso13Err = UseTypeU ? d.U31_err * 100 : d.B31_err;
        Aniso22Err = UseTypeU ? d.U22_err * 100 : d.B22_err;
        Aniso23Err = UseTypeU ? d.U23_err * 100 : d.B23_err;
        Aniso33Err = UseTypeU ? d.U33_err * 100 : d.B33_err;

        Ambient = atoms.Ambient;
        Diffusion = atoms.Diffusion;
        Emission = atoms.Emission;
        Shininess = atoms.Shininess;
        Specular = atoms.Specular;

        Radius = atoms.Radius;
        AtomColor = Color.FromArgb(atoms.Argb);
        Alpha = Color.FromArgb(atoms.Argb).A / 255f;
        ShowLabel = atoms.ShowLabel;
    }

    private Atoms GetFromInterface()
    {
        double[] aniso = UseTypeU
            ? [Aniso11 / 100, Aniso22 / 100, Aniso33 / 100, Aniso12 / 100, Aniso23 / 100, Aniso13 / 100]
            : [Aniso11, Aniso22, Aniso33, Aniso12, Aniso23, Aniso13];

        double[] anisoErr = UseTypeU
            ? [Aniso11Err / 100, Aniso22Err / 100, Aniso33Err / 100, Aniso12Err / 100, Aniso23Err / 100, Aniso13Err / 100]
            : [Aniso11Err, Aniso22Err, Aniso33Err, Aniso12Err, Aniso23Err, Aniso13Err];

        var dsf = new DiffuseScatteringFactor(UseTypeU ? DiffuseScatteringFactor.Type.U : DiffuseScatteringFactor.Type.B,
            UseIsotropy, Iso / 100, IsoErr / 100, aniso, anisoErr, Crystal.CellValue);
        var material = new Material(AtomColor.ToArgb(), (Ambient, Diffusion, Specular, Shininess, Emission), Alpha);

        return new Atoms(Label, AtomNo, AtomSubNoXray, AtomSubNoElectron, IsotopicComposition,
            SymmetrySeriesNumber, new Vector3D(X, Y, Z), new Vector3D(XErr, YErr, ZErr), Occ, OccErr, dsf,
            material, (float)Radius, true, ShowLabel);
    }
    #endregion

    #region 原子追加・削除などのボタン
    private void buttonAdd_Click(object sender, EventArgs e)
    {
        var atoms = GetFromInterface();
        if (atoms != null)
        {
            Add(atoms);
            bindingSource.Position = bindingSource.Count - 1;
        }
    }

    private void buttonChange_Click(object sender, EventArgs e)
    {
        var pos = bindingSource.Position;
        if (pos < 0) return;
        Replace(GetFromInterface(), pos);
        bindingSource.Position = pos;
    }

    private void buttonChangeToSameElement_Click(object sender, EventArgs e)
    {
        var pos = bindingSource.Position;
        if (pos < 0) return;
        var atoms = GetFromInterface();
        Replace(atoms, pos);
        if (tabControl.SelectedTab == tabPageAppearance)
            CopyAppearance(atoms, pos);
        else if (tabControl.SelectedTab == tabPageDebyeWaller)
            CopyDebyeWaller(atoms, pos, true);
        bindingSource.Position = pos;
    }

    private void buttonApplyToAllElements_Click(object sender, EventArgs e)
    {
        var pos = bindingSource.Position;
        if (pos < 0) return;
        var atoms = GetFromInterface();
        Replace(atoms, pos);
        if (tabControl.SelectedTab == tabPageDebyeWaller)
            CopyDebyeWaller(atoms, pos, false);
        bindingSource.Position = pos;
    }

    public void CopyAppearance(Atoms atoms, int i)
    {
        foreach (var a in dataSet.DataTableAtom.GetAll().Where(a => a.AtomicNumber == atoms.AtomicNumber))
        {
            a.Texture = atoms.Texture;
            a.Radius = atoms.Radius;
            a.Argb = atoms.Argb;
            a.ShowLabel = atoms.ShowLabel;
        }
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void CopyDebyeWaller(Atoms atoms, int i, bool onlySameElements)
    {
        var others = onlySameElements
            ? dataSet.DataTableAtom.GetAll().Where(a => a.AtomicNumber == atoms.AtomicNumber)
            : dataSet.DataTableAtom.GetAll();
        foreach (var a in others) a.Dsf = atoms.Dsf;
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    private void buttonDelete_Click(object sender, EventArgs e)
    {
        int pos = bindingSource.Position;
        if (pos < 0) return;
        SkipEvent = true;
        Delete(pos);
        SkipEvent = false;
        bindingSource.Position = bindingSource.Count > pos ? pos : pos - 1;
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

    private void bindingSource_PositionChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        if (bindingSource.Position >= 0 && bindingSource.Count > 0)
            SetToInterface(table.Get(bindingSource.Position));
    }

    private void listBoxAtoms_MouseUp(object sender, MouseEventArgs e) { }
    private void listBoxAtoms_MouseLeave(object sender, EventArgs e) { }

    private void buttonOriginShift_Click(object sender, EventArgs e)
    {
        var button = sender as Button;
        var shift = button.Name.Contains("Custom")
            ? new Vector3DBase(numericBoxOriginShiftX.Value, numericBoxOriginShiftY.Value, numericBoxOriginShiftZ.Value)
            : new Vector3DBase((button.Tag as string).Split(" ", true).Select(s => s.ToDouble()).ToArray()) * (radioButtonOriginShiftPlus.Checked ? 1 : -1);

        SkipEvent = true;
        var atomArray = GetAll();
        for (int i = 0; i < atomArray.Length; i++)
        {
            var atoms = Deep.Copy(atomArray[i]);
            atoms.X += shift.X;
            atoms.Y += shift.Y;
            atoms.Z += shift.Z; // 260426Cl: 旧コードは shift.Y を Z に加算していた typo
            atoms.ResetSymmetry(SymmetrySeriesNumber);
            table.Replace(atoms, i);
        }
        SkipEvent = false;
        bindingSource_PositionChanged(sender, e);
        ItemsChanged?.Invoke(this, e);
    }

    private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        buttonApplyToSameElement.Visible = tabControl.SelectedTab == tabPageAppearance || tabControl.SelectedTab == tabPageDebyeWaller;
        buttonApplyToAllElements.Visible = tabControl.SelectedTab == tabPageDebyeWaller;
    }

    private void dataGridViewAtom_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
        var enabled = (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        Crystal.Atoms[e.RowIndex].GLEnabled = table.Get(e.RowIndex).GLEnabled = enabled;
        GLEnableChanged?.Invoke(this, EventArgs.Empty);
    }

    private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        if (dataGridView.CurrentCellAddress.X == 0 && dataGridView.IsCurrentCellDirty)
            dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }
}

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class FormStrain : FormBase
{
    public CrystalControl CrystalControl;
    public Crystal crystal;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Matrix3D StrainMatrix
    {
        get => new(
            numericBoxStrain11.Value + 1, numericBoxStrain12.Value, numericBoxStrain13.Value,
            numericBoxStrain12.Value, numericBoxStrain22.Value + 1, numericBoxStrain23.Value,
            numericBoxStrain13.Value, numericBoxStrain23.Value, numericBoxStrain33.Value + 1);
        set
        {
            numericBoxStrain11.Value = value.E11 - 1;
            numericBoxStrain12.Value = value.E12;
            numericBoxStrain13.Value = value.E13;
            numericBoxStrain22.Value = value.E22 - 1;
            numericBoxStrain23.Value = value.E23;
            numericBoxStrain33.Value = value.E33 - 1;
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Matrix<double> Compliance { get => elasticityControl1.Compliance; set => elasticityControl1.Compliance = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Matrix<double> Stiffness { get => elasticityControl1.Stiffness; set => elasticityControl1.Stiffness = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Elasticity.Mode ElasticityMode { get => elasticityControl1.Mode; set => elasticityControl1.Mode = value; }

    private bool skipCrystalChangedEvent = false;
    private Crystal originalCrystal = null;

    public FormStrain() => InitializeComponent();

    private void FormStrain_Load(object sender, EventArgs e)
        => CrystalControl.CrystalChanged += crystalControl_CrystalChanged;

    private void FormStrain_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Visible = false;
    }

    private void crystalControl_CrystalChanged(object sender, EventArgs e)
    {
        if (skipCrystalChangedEvent || !Visible) return;

        // crystalControl 側の操作で結晶が変わった時のみ呼ばれる。
        // FormStrain 自身が crystalControl を書き換える間はフラグで自分自身のイベントを無視する。
        skipCrystalChangedEvent = true;
        CrystalControl.GenerateFromInterface();
        originalCrystal = Deep.Copy(CrystalControl.Crystal);

        SetStrainedCrystal();
        skipCrystalChangedEvent = false;

        numericBoxStrain_ValueChanged(this, EventArgs.Empty);
    }

    private void numericBoxStrain_ValueChanged(object sender, EventArgs e)
    {
        skipCrystalChangedEvent = true;
        var m = StrainMatrix * new Matrix3D(originalCrystal.A_Axis, originalCrystal.B_Axis, originalCrystal.C_Axis); // この歪の計算は間違っている！直さなければ。。。

        var a = new Vector3DBase(m.E11, m.E21, m.E31);
        var b = new Vector3DBase(m.E12, m.E22, m.E32);
        var c = new Vector3DBase(m.E13, m.E23, m.E33);

        numericBoxA.Value = a.Length * 10;
        numericBoxB.Value = b.Length * 10;
        numericBoxC.Value = c.Length * 10;
        numericBoxAlpha.RadianValue = Vector3DBase.AngleBetVectors(b, c);
        numericBoxBeta.RadianValue = Vector3DBase.AngleBetVectors(c, a);
        numericBoxGamma.RadianValue = Vector3DBase.AngleBetVectors(a, b);

        CrystalControl.symmetryControl.CellConstants = (numericBoxA.Value, numericBoxB.Value, numericBoxC.Value, numericBoxAlpha.RadianValue, numericBoxBeta.RadianValue, numericBoxGamma.RadianValue);
        // 260428Cl Application.DoEvents() を削除 (UI スレッドでのプロパティ設定後の DoEvents は不要)
        skipCrystalChangedEvent = false;

        calculateStress();
    }

    private void calculateStress()
    {
        if (!Visible) return;
        if (!skipCrystalChangedEvent) return;

        var strain = DenseMatrix.OfArray(new[,] {{
            numericBoxStrain11.Value, numericBoxStrain22.Value, numericBoxStrain33.Value,
            numericBoxStrain23.Value * 2, numericBoxStrain13.Value * 2, numericBoxStrain12.Value * 2 }}).Transpose();
        var stress = elasticityControl1.Stiffness * strain;

        numericBoxStress11.Value = stress[0, 0];
        numericBoxStress22.Value = stress[1, 0];
        numericBoxStress33.Value = stress[2, 0];
        numericBoxStress23.Value = stress[3, 0];
        numericBoxStress13.Value = stress[4, 0];
        numericBoxStress12.Value = stress[5, 0];
    }

    private void FormStrain_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
        {
            skipCrystalChangedEvent = true;
            CrystalControl.GenerateFromInterface();
            originalCrystal = Deep.Copy(CrystalControl.Crystal);
            SetStrainedCrystal();
            skipCrystalChangedEvent = false;
        }
        else
        {
            resetOriginalCrystal();
        }
    }

    private void resetOriginalCrystal()
    {
        if (originalCrystal == null) return;
        skipCrystalChangedEvent = true;
        CrystalControl.Crystal = originalCrystal;
        skipCrystalChangedEvent = false;
    }

    private void SetStrainedCrystal()
    {
        skipCrystalChangedEvent = true;

        CrystalControl.SymmetrySeriesNumber = 1;
        CrystalControl.Crystal.Atoms = [];
        foreach (var atomGroup in originalCrystal.Atoms)
            foreach (var pos in atomGroup.Atom)
            {
                var atom = Deep.Copy(atomGroup);
                atom.X = pos.X;
                atom.Y = pos.Y;
                atom.Z = pos.Z;
                CrystalControl.Crystal.AddAtoms(atom);
            }
        CrystalControl.SetToInterface();
        numericBoxStrain_ValueChanged(this, EventArgs.Empty);

        skipCrystalChangedEvent = false;
    }

    private void elasticityControl1_ValueChanged(object sender, EventArgs e) => calculateStress();

    private void buttonApply_Click(object sender, EventArgs e)
    {
        originalCrystal = CrystalControl.Crystal;
        Visible = false;
    }
}

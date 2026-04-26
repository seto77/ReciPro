using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class ElasticityControl : CaptureUserControlBase
{
    public ElasticityControl() => InitializeComponent();

    public delegate void MyEventHandler(object sender, EventArgs e);

    public event MyEventHandler ValueChanged;

    private int symmetrySeriesNumber = 1;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int SymmetrySeriesNumber
    {
        get => symmetrySeriesNumber;
        set { symmetrySeriesNumber = value; SetElasticity(); }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Elasticity.Mode Mode
    {
        get => radioButtonCompliance.Checked ? Elasticity.Mode.Compliance : Elasticity.Mode.Stiffness;
        set
        {
            radioButtonStiffness.Checked = value == Elasticity.Mode.Stiffness;
            radioButtonCompliance.Checked = value == Elasticity.Mode.Compliance;
        }
    }

    private Matrix<double> compliance = new DenseMatrix(6, 6);
    private Matrix<double> stiffness = new DenseMatrix(6, 6);
    private bool skip = false;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Matrix<double> Compliance
    {
        get => compliance;
        set
        {
            if (!IsValid6x6(value)) return;
            radioButtonCompliance.Checked = true;
            compliance = value;
            ApplyMatrixToBoxes(value);
            SetElasticity();
            stiffness = compliance.TryInverse();
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Matrix<double> Stiffness
    {
        get => stiffness;
        set
        {
            if (!IsValid6x6(value)) return;
            radioButtonStiffness.Checked = true;
            stiffness = value;
            ApplyMatrixToBoxes(value);
            SetElasticity();
            compliance = stiffness.TryInverse();
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private static bool IsValid6x6(Matrix<double> m) => m != null && m.RowCount == 6 && m.ColumnCount == 6;

    private void ApplyMatrixToBoxes(Matrix<double> v)
    {
        skip = true;
        numericBox11.Value = v[0, 0]; numericBox12.Value = v[0, 1]; numericBox13.Value = v[0, 2];
        numericBox14.Value = v[0, 3]; numericBox15.Value = v[0, 4]; numericBox16.Value = v[0, 5];
        numericBox22.Value = v[1, 1]; numericBox23.Value = v[1, 2]; numericBox24.Value = v[1, 3];
        numericBox25.Value = v[1, 4]; numericBox26.Value = v[1, 5];
        numericBox33.Value = v[2, 2]; numericBox34.Value = v[2, 3]; numericBox35.Value = v[2, 4]; numericBox36.Value = v[2, 5];
        numericBox44.Value = v[3, 3]; numericBox45.Value = v[3, 4]; numericBox46.Value = v[3, 5];
        numericBox55.Value = v[4, 4]; numericBox56.Value = v[4, 5];
        numericBox66.Value = v[5, 5];
        skip = false;
    }

    private void SetElasticity()
    {
        var sym = SymmetryStatic.Symmetries[SymmetrySeriesNumber];

        // 全 numericBox を Enabled=true に戻す
        NumericBox[] all = [
            numericBox13, numericBox23,
            numericBox14, numericBox24, numericBox34,
            numericBox15, numericBox25, numericBox35, numericBox45,
            numericBox16, numericBox26, numericBox36, numericBox46, numericBox56,
            numericBox22, numericBox33, numericBox55, numericBox66 ];
        foreach (var nb in all) nb.Enabled = true;

        // 結晶系ごとに不要な要素を Enabled=false にしつつ 0 を入れる
        void Disable(params NumericBox[] boxes)
        {
            foreach (var b in boxes) { b.Enabled = false; b.Value = 0; }
        }

        switch (sym.CrystalSystemStr)
        {
            case "monoclinic":
                switch (sym.MainAxis)
                {
                    case "a":
                        Disable(numericBox15, numericBox25, numericBox35,
                                numericBox16, numericBox26, numericBox36,
                                numericBox45, numericBox46);
                        break;
                    case "b":
                        Disable(numericBox14, numericBox24, numericBox34,
                                numericBox16, numericBox26, numericBox36,
                                numericBox45, numericBox56);
                        break;
                    case "c":
                        Disable(numericBox14, numericBox24, numericBox34,
                                numericBox15, numericBox25, numericBox35,
                                numericBox46, numericBox56);
                        break;
                }
                break;

            case "orthorhombic":
                Disable(numericBox14, numericBox24, numericBox34,
                        numericBox15, numericBox25, numericBox35, numericBox45,
                        numericBox16, numericBox26, numericBox36, numericBox46, numericBox56);
                break;

            case "tetragonal":
                if (sym.PointGroupHMStr is "4" or "-4" or "4/m")
                {
                    // 16 は Enabled のまま (値が 26 = -16 の制約源として使われる)
                    // 22/23/55/26 は disable してゼロ化しても直後の ValueChanged 側で再充填される
                    Disable(numericBox22, numericBox23, numericBox55, numericBox26,
                            numericBox14, numericBox24, numericBox34,
                            numericBox15, numericBox25, numericBox35, numericBox45,
                            numericBox36, numericBox46, numericBox56);
                }
                else
                {
                    Disable(numericBox22, numericBox23, numericBox14, numericBox24, numericBox34,
                            numericBox15, numericBox25, numericBox35, numericBox45, numericBox55,
                            numericBox16, numericBox26, numericBox36, numericBox46, numericBox56);
                }
                break;

            case "trigonal":
                if (sym.PointGroupHMStr is "3" or "-3")
                {
                    Disable(numericBox22, numericBox23, numericBox24, numericBox34,
                            numericBox25, numericBox35, numericBox45, numericBox55,
                            numericBox16, numericBox26, numericBox36, numericBox46, numericBox56,
                            numericBox66);
                }
                else
                {
                    Disable(numericBox22, numericBox23, numericBox24, numericBox34,
                            numericBox15, numericBox25, numericBox35, numericBox45, numericBox55,
                            numericBox16, numericBox26, numericBox36, numericBox46, numericBox56,
                            numericBox66);
                }
                break;

            case "hexagonal":
                Disable(numericBox22, numericBox23, numericBox14, numericBox24, numericBox34,
                        numericBox15, numericBox25, numericBox35, numericBox45,
                        numericBox16, numericBox26, numericBox36, numericBox46, numericBox56,
                        numericBox55, numericBox66);
                break;

            case "cubic":
                Disable(numericBox13, numericBox23, numericBox14, numericBox24, numericBox34,
                        numericBox15, numericBox25, numericBox35, numericBox45,
                        numericBox16, numericBox26, numericBox36, numericBox46, numericBox56,
                        numericBox22, numericBox33, numericBox55, numericBox66);
                break;
        }
        numericBoxElasticiry_ValueChanged(numericBox11, EventArgs.Empty);
    }

    private void numericBoxElasticiry_ValueChanged(object sender, EventArgs e)
    {
        if (skip) return;
        if (!((NumericBox)sender).Enabled) return;
        var sym = SymmetryStatic.Symmetries[SymmetrySeriesNumber];
        switch (sym.CrystalSystemStr)
        {
            case "tetragonal":
                numericBox22.Value = numericBox11.Value;
                numericBox23.Value = numericBox13.Value;
                numericBox55.Value = numericBox44.Value;
                if (sym.PointGroupHMStr is "4" or "-4" or "4/m")
                    numericBox26.Value = -numericBox16.Value;
                break;

            case "trigonal":
                numericBox22.Value = numericBox11.Value;
                numericBox23.Value = numericBox13.Value;
                numericBox24.Value = -numericBox14.Value;
                numericBox56.Value = numericBox14.Value;
                numericBox55.Value = numericBox44.Value;
                numericBox66.Value = (numericBox11.Value - numericBox12.Value) / 2; // 260426Cl: 元コードは ... - numericBox11 で常に 0 になる typo
                if (sym.PointGroupHMStr is "3" or "-3")
                {
                    numericBox25.Value = -numericBox15.Value;
                    numericBox46.Value = -numericBox15.Value;
                }
                break;

            case "hexagonal":
                numericBox22.Value = numericBox11.Value;
                numericBox66.Value = (numericBox11.Value - numericBox12.Value) / 2;
                numericBox23.Value = numericBox13.Value;
                numericBox55.Value = numericBox44.Value;
                break;

            case "cubic":
                numericBox22.Value = numericBox33.Value = numericBox11.Value;
                numericBox55.Value = numericBox66.Value = numericBox44.Value;
                numericBox13.Value = numericBox23.Value = numericBox12.Value;
                break;
        }

        var mtx = new DenseMatrix(6, 6);
        mtx[0, 0] = numericBox11.Value;
        mtx[0, 1] = mtx[1, 0] = numericBox12.Value;
        mtx[0, 2] = mtx[2, 0] = numericBox13.Value;
        mtx[0, 3] = mtx[3, 0] = numericBox14.Value;
        mtx[0, 4] = mtx[4, 0] = numericBox15.Value;
        mtx[0, 5] = mtx[5, 0] = numericBox16.Value;

        mtx[1, 1] = numericBox22.Value;
        mtx[1, 2] = mtx[2, 1] = numericBox23.Value;
        mtx[1, 3] = mtx[3, 1] = numericBox24.Value;
        mtx[1, 4] = mtx[4, 1] = numericBox25.Value;
        mtx[1, 5] = mtx[5, 1] = numericBox26.Value;

        mtx[2, 2] = numericBox33.Value;
        mtx[2, 3] = mtx[3, 2] = numericBox34.Value;
        mtx[2, 4] = mtx[4, 2] = numericBox35.Value;
        mtx[2, 5] = mtx[5, 2] = numericBox36.Value;

        mtx[3, 3] = numericBox44.Value;
        mtx[3, 4] = mtx[4, 3] = numericBox45.Value;
        mtx[3, 5] = mtx[5, 3] = numericBox46.Value;

        mtx[4, 4] = numericBox55.Value;
        mtx[4, 5] = mtx[5, 4] = numericBox56.Value;

        mtx[5, 5] = numericBox66.Value;

        if (radioButtonStiffness.Checked)
        {
            stiffness = mtx;
            compliance = mtx.TryInverse();
        }
        else
        {
            compliance = mtx;
            stiffness = mtx.TryInverse();
        }
        ValueChanged?.Invoke(this, e);
    }

    private void radioButtonStiffness_CheckedChanged(object sender, EventArgs e)
    {
        if (radioButtonStiffness.Checked)
            Stiffness = compliance.TryInverse();
        else
            Compliance = stiffness.TryInverse();
        ValueChanged?.Invoke(this, e);
    }
}

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class EOSControl : UserControlBase
{
    #region フィールド, プロパティ
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SkipEvent { get; set; } = false;

    private Crystal crystal = null;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Crystal Crystal
    {
        get => crystal;
        set { crystal = value; if (crystal != null) setParameters(); }
    }

    public EOS EOScondition
    {
        get
        {
            if (crystal == null) return null;
            var eos = new EOS
            {
                A = numericBoxEOS_A.Value,
                B = numericBoxEOS_B.Value,
                C = numericBoxEOS_C.Value,
                CellVolume0 = numericBoxEOS_V0perCell.Value,
                Gamma0 = numericBoxEOS_Gamma0.Value,
                K0 = numericBoxEOS_K0.Value,
                KperT = numericBoxEOS_KperT.Value,
                Kp0 = numericBoxEOS_Kp0.Value,
                Kpp0 = numericBoxEOS_Kpp0.Value,
                KpInfinity = numericBoxEOS_KpInfinity.Value,
                Vinet3rd_Ita = numericBox3rdVinetIta.Value,
                Vinet3rd_Beta = numericBox3rdVinetBeta.Value,
                Vinet3rd_Psi = numericBox3rdVinetPsi.Value,
                Q = numericBoxEOS_Q.Value,
                T0 = numericBoxEOS_T0.Value,
                Theta0 = numericBoxEOS_Theta0.Value,
                Note = textBoxEOS_Note.Text,
                Temperature = numericBoxTemperature.Value,
            };

            if (radioButtonEOS_ThirdBirchMurnaghan.Checked) eos.IsothermalPressureApproach = IsothermalPressure.BM3;
            else if (radioButtonEOS_FourthBirchMunaghan.Checked) eos.IsothermalPressureApproach = IsothermalPressure.BM4;
            else if (radioButtonEOS_Vinet.Checked) eos.IsothermalPressureApproach = IsothermalPressure.Vinet;
            else if (radioButtonEOS_AP2.Checked) eos.IsothermalPressureApproach = IsothermalPressure.AP2;
            else if (radioButtonEOS_Keane.Checked) eos.IsothermalPressureApproach = IsothermalPressure.Keane;
            else if (radioButtonEOS_Vinet3rd.Checked) eos.IsothermalPressureApproach = IsothermalPressure.Vinet3;

            if (radioButtonMieGruneisen.Checked) eos.ThermalPressureApproach = ThermalPressure.MieGruneisen;
            else if (radioButtonTdependenceK0andV0.Checked) eos.ThermalPressureApproach = ThermalPressure.T_dependence_BM;

            int n = 0;
            double ze = 0;
            foreach (var atom in crystal.Atoms)
            {
                n += atom.Atom.Length;
                ze += atom.AtomicNumber * atom.Atom.Length * atom.Occ;
            }
            eos.Ze = ze;
            eos.Z = crystal.ChemicalFormulaZ;
            if (eos.Z > 0) eos.N = n / crystal.ChemicalFormulaZ;
            eos.Enabled = checkBoxUseEOS.Checked;
            return eos;
        }
    }

    public EOSControl() => InitializeComponent();

    #endregion

    public void setParameters()
    {
        if (crystal == null || crystal.EOSCondition == null) return;
        var ec = crystal.EOSCondition;

        SkipEvent = true;

        numericBoxEOS_A.Value = ec.A;
        numericBoxEOS_B.Value = ec.B;
        numericBoxEOS_C.Value = ec.C;
        numericBoxEOS_V0perCell.Value = ec.CellVolume0;
        numericBoxEOS_Gamma0.Value = ec.Gamma0;
        numericBoxEOS_K0.Value = ec.K0;
        numericBoxEOS_KperT.Value = ec.KperT;
        numericBoxEOS_Kp0.Value = ec.Kp0;
        numericBoxEOS_Kpp0.Value = ec.Kpp0;
        numericBoxEOS_KpInfinity.Value = ec.KpInfinity;
        numericBoxEOS_Q.Value = ec.Q;
        numericBoxEOS_T0.Value = ec.T0;
        numericBoxEOS_Theta0.Value = ec.Theta0;
        checkBoxUseEOS.Checked = ec.Enabled;

        switch (ec.ThermalPressureApproach)
        {
            case ThermalPressure.MieGruneisen: radioButtonMieGruneisen.Checked = true; break;
            case ThermalPressure.T_dependence_BM: radioButtonTdependenceK0andV0.Checked = true; break;
        }

        switch (ec.IsothermalPressureApproach)
        {
            case IsothermalPressure.BM3: radioButtonEOS_ThirdBirchMurnaghan.Checked = true; break;
            case IsothermalPressure.BM4: radioButtonEOS_FourthBirchMunaghan.Checked = true; break;
            case IsothermalPressure.Vinet: radioButtonEOS_Vinet.Checked = true; break;
            case IsothermalPressure.AP2: radioButtonEOS_AP2.Checked = true; break;
            case IsothermalPressure.Keane: radioButtonEOS_Keane.Checked = true; break;
            case IsothermalPressure.Vinet3: radioButtonEOS_Vinet3rd.Checked = true; break;
        }

        textBoxEOS_Note.Text = ec.Note;
        numericBoxTemperature.Value = ec.Temperature;
        CalculatePressure();
        SkipEvent = false;
    }

    public void CalculatePressure()
    {
        if (SkipEvent) return;
        SkipEvent = true;
        if (numericBoxEOS_V0perMol.ReadOnly && !double.IsNaN(numericBoxEOS_V0perCell.Value))
            numericBoxEOS_V0perMol.Value = numericBoxEOS_V0perCell.Value * 6.0221367 / crystal.ChemicalFormulaZ / 10;

        if (checkBoxUseEOS.Checked)
            // 260426Cl: 旧コードは EOScondition.Pressure に代入していたが、EOScondition の getter は毎回新規 EOS を返すため永続インスタンスへ反映されなかった
            crystal.EOSCondition.Pressure = numericBoxPressure.Value = crystal.EOSCondition.GetPressure(crystal.Volume * 1000);

        SkipEvent = false;
    }

    private void numericBoxEOS_V0perCell_Click2(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        SkipEvent = true;
        numericBoxEOS_V0perCell.ReadOnly = false;
        numericBoxEOS_V0perMol.ReadOnly = true;
        SkipEvent = false;
    }

    private void numericBoxEOS_V0perMol_Click2(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        SkipEvent = true;
        numericBoxEOS_V0perCell.ReadOnly = true;
        numericBoxEOS_V0perMol.ReadOnly = false;
        SkipEvent = false;
    }

    private void numericBoxEOS_V0perMol_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        SkipEvent = true;
        if (!numericBoxEOS_V0perMol.ReadOnly)
            numericBoxEOS_V0perCell.Value = numericBoxEOS_V0perMol.Value / 6.0221367 * 10 * crystal.ChemicalFormulaZ;
        SkipEvent = false;
    }

    private void parameters_Changed(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        numericBox3rdVinetBeta.Visible = numericBox3rdVinetIta.Visible = numericBox3rdVinetPsi.Visible = radioButtonEOS_Vinet3rd.Checked;
        numericBoxEOS_Kp0.Visible = !radioButtonEOS_Vinet3rd.Checked;
        numericBoxEOS_KpInfinity.Visible = radioButtonEOS_Keane.Checked;
        numericBoxEOS_Kpp0.Visible = radioButtonEOS_FourthBirchMunaghan.Checked;
        crystal.EOSCondition = EOScondition;
        CalculatePressure();
    }
}

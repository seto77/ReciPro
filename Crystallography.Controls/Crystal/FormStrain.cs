using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormStrain : Form
    {
        public CrystalControl CrystalControl;
        public Crystal crystal;

        public Matrix3D StrainMatrix
        {
            get
            {
                return new Matrix3D(
                     numericBoxStrain11.Value + 1, numericBoxStrain12.Value, numericBoxStrain13.Value,
                     numericBoxStrain12.Value, numericBoxStrain22.Value + 1, numericBoxStrain23.Value,
                     numericBoxStrain13.Value, numericBoxStrain23.Value, numericBoxStrain33.Value + 1);
            }
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

        public Matrix<double> Compliance { set { elasticityControl1.Compliance = value; } get { return elasticityControl1.Compliance; } }
        public Matrix<double> Stiffness { set { elasticityControl1.Stiffness = value; } get { return elasticityControl1.Stiffness; } }
        public Elasticity.Mode ElasticityMode { set { elasticityControl1.Mode = value; } get { return elasticityControl1.Mode; } }

        public FormStrain()
        {
            InitializeComponent();
        }

        private void FormStrain_Load(object sender, EventArgs e)
        {
            CrystalControl.CrystalChanged += new EventHandler(crystalControl_CrystalChanged);
        }

        private void FormStrain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private bool skipCrystalChangedEvent = false;

        private void crystalControl_CrystalChanged(object sender, EventArgs e)
        {
            if (skipCrystalChangedEvent || !this.Visible) return;

            //ここが呼ばれるのは、crystalControl側が操作されて結晶が変更される場合のみ。
            //このformStrainがcrystalControlを変更する時は、その時だけこのイベントを無視する
            skipCrystalChangedEvent = true;
            CrystalControl.GenerateFromInterface();
            originalCrystal = Deep.Copy(CrystalControl.Crystal);

            SetStrainedCrystal();
            skipCrystalChangedEvent = false;

            numericBoxStrain_ValueChanged(new object(), new EventArgs());
        }

        private void numericBoxStrain_ValueChanged(object sender, EventArgs e)
        {
            skipCrystalChangedEvent = true;
            var m = StrainMatrix * new Matrix3D(originalCrystal.A_Axis, originalCrystal.B_Axis, originalCrystal.C_Axis);//この歪の計算は間違っている！直さなければ。。。

            var a = new Vector3DBase(m.E11, m.E21, m.E31);
            var b = new Vector3DBase(m.E12, m.E22, m.E32);
            var c = new Vector3DBase(m.E13, m.E23, m.E33);

            numericBoxA.Value = a.Length* 10;
            numericBoxB.Value = b.Length* 10;
            numericBoxC.Value = c.Length* 10;
            numericBoxAlpha.RadianValue = Vector3DBase.AngleBetVectors(b, c);
            numericBoxBeta.RadianValue = Vector3DBase.AngleBetVectors(c, a);
            numericBoxGamma.RadianValue = Vector3DBase.AngleBetVectors(a, b);

            CrystalControl.symmetryControl.CellConstants = ( numericBoxA.Value, numericBoxB.Value, numericBoxC.Value, numericBoxAlpha.RadianValue, numericBoxBeta.RadianValue, numericBoxGamma.RadianValue );
            Application.DoEvents();
            skipCrystalChangedEvent = false;

            calculateStress();
        }

        private void calculateStress()
        {
            if (!this.Visible) return;
            if (!skipCrystalChangedEvent) return;

            var strain = DenseMatrix.OfArray(new double[,] {  {
                    numericBoxStrain11.Value,
                    numericBoxStrain22.Value,
                    numericBoxStrain33.Value,
                    numericBoxStrain23.Value*2,
                    numericBoxStrain13.Value*2,
                    numericBoxStrain12.Value*2 } }).Transpose();
            var stress = elasticityControl1.Stiffness * strain;

            numericBoxStress11.Value = stress[0, 0];
            numericBoxStress22.Value = stress[1, 0];
            numericBoxStress33.Value = stress[2, 0];
            numericBoxStress23.Value = stress[3, 0];
            numericBoxStress13.Value = stress[4, 0];
            numericBoxStress12.Value = stress[5, 0];
        }

        private Crystal originalCrystal = null;

        private void FormStrain_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
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
            CrystalControl.Crystal.Atoms = new Atoms[0];
            for (int i = 0; i < originalCrystal.Atoms.Length; i++)
                for (int j = 0; j < originalCrystal.Atoms[i].Atom.Count; j++)
                {
                    var atom = Deep.Copy(originalCrystal.Atoms[i]);
                    atom.X = originalCrystal.Atoms[i].Atom[j].X;
                    atom.Y = originalCrystal.Atoms[i].Atom[j].Y;
                    atom.Z = originalCrystal.Atoms[i].Atom[j].Z;
                    CrystalControl.Crystal.AddAtoms(atom);
                }
            CrystalControl.SetToInterface();
            numericBoxStrain_ValueChanged(new object(), new EventArgs());

            skipCrystalChangedEvent = false;
        }

        private void elasticityControl1_ValueChanged(object sender, EventArgs e)
        {
            calculateStress();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            originalCrystal = CrystalControl.Crystal;
            this.Visible = false;
        }
    }
}
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class ElasticityControl : UserControl
    {
        public ElasticityControl()
        {
            InitializeComponent();
        }

        public delegate void MyEventHandler(object sender, EventArgs e);

        public event MyEventHandler ValueChanged;

        private int symmetrySeriesNumber = 1;

        public int SymmetrySeriesNumber
        {
            set
            {
                symmetrySeriesNumber = value;
                SetElasticity();
            }
            get { return symmetrySeriesNumber; }
        }

        public Elasticity.Mode Mode
        {
            get
            {
                return radioButtonCompliance.Checked ? Elasticity.Mode.Compliance : Elasticity.Mode.Stiffness;
            }
            set
            {
                radioButtonStiffness.Checked = value == Elasticity.Mode.Stiffness;
                radioButtonCompliance.Checked = value == Elasticity.Mode.Compliance;
            }
        }

        private Matrix<double> compliance = new DenseMatrix(6, 6);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Matrix<double> Compliance
        {
            set
            {
                if (value == null || value.ColumnCount != 6 || value.RowCount != 6) return;
                radioButtonCompliance.Checked = true;
                compliance = value;
                skip = true;
                numericBox11.Value = value[0, 0];
                numericBox12.Value = value[0, 1];
                numericBox13.Value = value[0, 2];
                numericBox14.Value = value[0, 3];
                numericBox15.Value = value[0, 4];
                numericBox16.Value = value[0, 5];
                numericBox22.Value = value[1, 1];
                numericBox23.Value = value[1, 2];
                numericBox24.Value = value[1, 3];
                numericBox25.Value = value[1, 4];
                numericBox26.Value = value[1, 5];
                numericBox33.Value = value[2, 2];
                numericBox34.Value = value[2, 3];
                numericBox35.Value = value[2, 4];
                numericBox36.Value = value[2, 5];
                numericBox44.Value = value[3, 3];
                numericBox45.Value = value[3, 4];
                numericBox46.Value = value[3, 5];
                numericBox55.Value = value[4, 4];
                numericBox56.Value = value[4, 5];
                numericBox66.Value = value[5, 5];
                skip = false;
                SetElasticity();
                stiffness = compliance.TryInverse();
                ValueChanged?.Invoke(this, new EventArgs());
            }
            get
            { return compliance; }
        }

        private Matrix<double> stiffness = new DenseMatrix(6, 6);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Matrix<double> Stiffness
        {
            set
            {
                if (value == null || value.ColumnCount != 6 || value.RowCount != 6) return;
                radioButtonStiffness.Checked = true;
                stiffness = value;
                skip = true;
                numericBox11.Value = value[0, 0];
                numericBox12.Value = value[0, 1];
                numericBox13.Value = value[0, 2];
                numericBox14.Value = value[0, 3];
                numericBox15.Value = value[0, 4];
                numericBox16.Value = value[0, 5];
                numericBox22.Value = value[1, 1];
                numericBox23.Value = value[1, 2];
                numericBox24.Value = value[1, 3];
                numericBox25.Value = value[1, 4];
                numericBox26.Value = value[1, 5];
                numericBox33.Value = value[2, 2];
                numericBox34.Value = value[2, 3];
                numericBox35.Value = value[2, 4];
                numericBox36.Value = value[2, 5];
                numericBox44.Value = value[3, 3];
                numericBox45.Value = value[3, 4];
                numericBox46.Value = value[3, 5];
                numericBox55.Value = value[4, 4];
                numericBox56.Value = value[4, 5];
                numericBox66.Value = value[5, 5];
                skip = false;

                SetElasticity();

                compliance = stiffness.TryInverse();
                ValueChanged?.Invoke(this, new EventArgs());
            }
            get
            { return stiffness; }
        }

        private bool skip = false;

        private void SetElasticity()
        {
            Symmetry tempSym = SymmetryStatic.Get_Symmetry(SymmetrySeriesNumber);
            //いったんすべてをEnabled=trueにする
            numericBox13.Enabled = numericBox23.Enabled =
            numericBox14.Enabled = numericBox24.Enabled = numericBox34.Enabled =
            numericBox15.Enabled = numericBox25.Enabled = numericBox35.Enabled = numericBox45.Enabled =
            numericBox16.Enabled = numericBox26.Enabled = numericBox36.Enabled = numericBox46.Enabled = numericBox56.Enabled =
            numericBox22.Enabled = numericBox33.Enabled = numericBox55.Enabled = numericBox66.Enabled = true;
            switch (tempSym.CrystalSystemStr)
            {
                case "Unknown": break;
                case "triclinic":
                    break;

                case "monoclinic":
                    switch (tempSym.MainAxis)
                    {
                        case "a":
                            numericBox15.Enabled = numericBox25.Enabled = numericBox35.Enabled =
                            numericBox16.Enabled = numericBox26.Enabled = numericBox36.Enabled =
                            numericBox45.Enabled = numericBox46.Enabled = false;

                            numericBox15.Value = numericBox25.Value = numericBox35.Value =
                            numericBox16.Value = numericBox26.Value = numericBox36.Value =
                            numericBox45.Value = numericBox46.Value = 0;
                            break;

                        case "b":
                            numericBox14.Enabled = numericBox24.Enabled = numericBox34.Enabled =
                            numericBox16.Enabled = numericBox26.Enabled = numericBox36.Enabled =
                            numericBox45.Enabled = numericBox56.Enabled = false;

                            numericBox14.Value = numericBox24.Value = numericBox34.Value =
                            numericBox16.Value = numericBox26.Value = numericBox36.Value =
                            numericBox45.Value = numericBox56.Value = 0;
                            break;

                        case "c":
                            numericBox14.Enabled = numericBox24.Enabled = numericBox34.Enabled =
                            numericBox15.Enabled = numericBox25.Enabled = numericBox35.Enabled =
                            numericBox46.Enabled = numericBox56.Enabled = false;

                            numericBox14.Value = numericBox24.Value = numericBox34.Value =
                            numericBox15.Value = numericBox25.Value = numericBox35.Value =
                            numericBox46.Value = numericBox56.Value = 0;
                            break;
                    }
                    break;

                case "orthorhombic":
                    numericBox14.Enabled = numericBox24.Enabled = numericBox34.Enabled =
                    numericBox15.Enabled = numericBox25.Enabled = numericBox35.Enabled = numericBox45.Enabled =
                    numericBox16.Enabled = numericBox26.Enabled = numericBox36.Enabled = numericBox46.Enabled = numericBox56.Enabled = false;

                    numericBox14.Value = numericBox24.Value = numericBox34.Value =
                    numericBox15.Value = numericBox25.Value = numericBox35.Value = numericBox45.Value =
                    numericBox16.Value = numericBox26.Value = numericBox36.Value = numericBox46.Value = numericBox56.Value = 0;
                    break;

                case "tetragonal":
                    if (tempSym.PointGroupHMStr == "4" || tempSym.PointGroupHMStr == "-4" || tempSym.PointGroupHMStr == "4/m")
                    {
                        numericBox22.Enabled = numericBox23.Enabled =
                        numericBox14.Enabled = numericBox24.Enabled = numericBox34.Enabled =
                        numericBox15.Enabled = numericBox25.Enabled = numericBox35.Enabled = numericBox45.Enabled = numericBox55.Enabled =
                        numericBox26.Enabled = numericBox36.Enabled = numericBox46.Enabled = numericBox56.Enabled = false;

                        numericBox14.Value = numericBox24.Value = numericBox34.Value =
                        numericBox15.Value = numericBox25.Value = numericBox35.Value = numericBox45.Value =
                        numericBox36.Value = numericBox46.Value = numericBox56.Value = 0;
                    }
                    else
                    {
                        numericBox22.Enabled = numericBox23.Enabled = numericBox14.Enabled = numericBox24.Enabled = numericBox34.Enabled =
                        numericBox15.Enabled = numericBox25.Enabled = numericBox35.Enabled = numericBox45.Enabled = numericBox55.Enabled =
                        numericBox16.Enabled = numericBox26.Enabled = numericBox36.Enabled = numericBox46.Enabled = numericBox56.Enabled = false;

                        numericBox14.Value = numericBox24.Value = numericBox34.Value =
                        numericBox15.Value = numericBox25.Value = numericBox35.Value = numericBox45.Value =
                        numericBox16.Value = numericBox26.Value = numericBox36.Value = numericBox46.Value = numericBox56.Value = 0;
                    }
                    break;

                case "trigonal":
                    if (tempSym.PointGroupHMStr == "3" || tempSym.PointGroupHMStr == "-3")
                    {
                        numericBox22.Enabled = numericBox23.Enabled = numericBox24.Enabled = numericBox34.Enabled =
                        numericBox25.Enabled = numericBox35.Enabled = numericBox45.Enabled = numericBox55.Enabled =
                        numericBox16.Enabled = numericBox26.Enabled = numericBox36.Enabled = numericBox46.Enabled = numericBox56.Enabled =
                        numericBox66.Enabled = false;

                        numericBox34.Value = numericBox35.Value = numericBox45.Value =
                        numericBox16.Value = numericBox26.Value = numericBox36.Value = 0;
                    }
                    else
                    {
                        numericBox22.Enabled = numericBox23.Enabled = numericBox24.Enabled = numericBox34.Enabled =
                        numericBox15.Enabled = numericBox25.Enabled = numericBox35.Enabled = numericBox45.Enabled = numericBox55.Enabled =
                        numericBox16.Enabled = numericBox26.Enabled = numericBox36.Enabled = numericBox46.Enabled = numericBox56.Enabled =
                        numericBox66.Enabled = false;

                        numericBox34.Value = numericBox15.Value = numericBox25.Value = numericBox35.Value =
                        numericBox45.Value = numericBox16.Value = numericBox26.Value = numericBox36.Value = numericBox46.Value = 0;
                    }
                    break;

                case "hexagonal":
                    numericBox22.Enabled = numericBox23.Enabled = numericBox14.Enabled = numericBox24.Enabled = numericBox34.Enabled =
                    numericBox15.Enabled = numericBox25.Enabled = numericBox35.Enabled = numericBox45.Enabled =
                    numericBox16.Enabled = numericBox26.Enabled = numericBox36.Enabled = numericBox46.Enabled = numericBox56.Enabled =
                    numericBox55.Enabled = numericBox66.Enabled = false;

                    numericBox14.Value = numericBox24.Value = numericBox34.Value =
                    numericBox15.Value = numericBox25.Value = numericBox35.Value = numericBox45.Value =
                    numericBox16.Value = numericBox26.Value = numericBox36.Value = numericBox46.Value = numericBox56.Value = 0;
                    break;

                case "cubic":
                    numericBox13.Enabled = numericBox23.Enabled = numericBox14.Enabled = numericBox24.Enabled = numericBox34.Enabled =
                    numericBox15.Enabled = numericBox25.Enabled = numericBox35.Enabled = numericBox45.Enabled =
                    numericBox16.Enabled = numericBox26.Enabled = numericBox36.Enabled = numericBox46.Enabled = numericBox56.Enabled =
                    numericBox22.Enabled = numericBox33.Enabled = numericBox55.Enabled = numericBox66.Enabled = false;

                    numericBox14.Value = numericBox24.Value = numericBox34.Value =
                    numericBox15.Value = numericBox25.Value = numericBox35.Value = numericBox45.Value =
                    numericBox16.Value = numericBox26.Value = numericBox36.Value = numericBox46.Value = numericBox56.Value = 0;
                    break;
            }
            numericBoxElasticiry_ValueChanged(numericBox11, new EventArgs());
        }

        private void numericBoxElasticiry_ValueChanged(object sender, EventArgs e)
        {
            if (skip) return;
            if (((NumericBox)sender).Enabled == false) return;
            Symmetry tempSym = SymmetryStatic.Get_Symmetry(SymmetrySeriesNumber);
            switch (tempSym.CrystalSystemStr)
            {
                case "tetragonal":
                    if (tempSym.PointGroupHMStr == "4" || tempSym.PointGroupHMStr == "-4" || tempSym.PointGroupHMStr == "4/m")
                    {
                        numericBox22.Value = numericBox11.Value;
                        numericBox23.Value = numericBox13.Value;
                        numericBox26.Value = -numericBox16.Value;
                        numericBox55.Value = numericBox44.Value;
                    }
                    else
                    {
                        numericBox22.Value = numericBox11.Value;
                        numericBox23.Value = numericBox13.Value;
                        numericBox55.Value = numericBox44.Value;
                    }
                    break;

                case "trigonal":
                    if (tempSym.PointGroupHMStr == "3" || tempSym.PointGroupHMStr == "-3")
                    {
                        numericBox22.Value = numericBox11.Value;
                        numericBox23.Value = numericBox13.Value;
                        numericBox24.Value = -numericBox14.Value;
                        numericBox25.Value = -numericBox15.Value;
                        numericBox46.Value = -numericBox15.Value;
                        numericBox56.Value = numericBox14.Value;
                        numericBox55.Value = numericBox44.Value;
                        numericBox66.Value = (numericBox11.Value - numericBox11.Value) / 2;
                    }
                    else
                    {
                        numericBox22.Value = numericBox11.Value;
                        numericBox23.Value = numericBox13.Value;
                        numericBox24.Value = -numericBox14.Value;
                        numericBox56.Value = numericBox14.Value;
                        numericBox55.Value = numericBox44.Value;
                        numericBox66.Value = (numericBox11.Value - numericBox12.Value) / 2;
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
            mtx[0, 0] = mtx[0, 0] = numericBox11.Value;
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
}
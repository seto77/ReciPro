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
                numericalTextBox11.Value = value[0, 0];
                numericalTextBox12.Value = value[0, 1];
                numericalTextBox13.Value = value[0, 2];
                numericalTextBox14.Value = value[0, 3];
                numericalTextBox15.Value = value[0, 4];
                numericalTextBox16.Value = value[0, 5];
                numericalTextBox22.Value = value[1, 1];
                numericalTextBox23.Value = value[1, 2];
                numericalTextBox24.Value = value[1, 3];
                numericalTextBox25.Value = value[1, 4];
                numericalTextBox26.Value = value[1, 5];
                numericalTextBox33.Value = value[2, 2];
                numericalTextBox34.Value = value[2, 3];
                numericalTextBox35.Value = value[2, 4];
                numericalTextBox36.Value = value[2, 5];
                numericalTextBox44.Value = value[3, 3];
                numericalTextBox45.Value = value[3, 4];
                numericalTextBox46.Value = value[3, 5];
                numericalTextBox55.Value = value[4, 4];
                numericalTextBox56.Value = value[4, 5];
                numericalTextBox66.Value = value[5, 5];
                skip = false;
                SetElasticity();
                stiffness = compliance.TryInverse();
                if (ValueChanged != null)
                    ValueChanged(this, new EventArgs());
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
                numericalTextBox11.Value = value[0, 0];
                numericalTextBox12.Value = value[0, 1];
                numericalTextBox13.Value = value[0, 2];
                numericalTextBox14.Value = value[0, 3];
                numericalTextBox15.Value = value[0, 4];
                numericalTextBox16.Value = value[0, 5];
                numericalTextBox22.Value = value[1, 1];
                numericalTextBox23.Value = value[1, 2];
                numericalTextBox24.Value = value[1, 3];
                numericalTextBox25.Value = value[1, 4];
                numericalTextBox26.Value = value[1, 5];
                numericalTextBox33.Value = value[2, 2];
                numericalTextBox34.Value = value[2, 3];
                numericalTextBox35.Value = value[2, 4];
                numericalTextBox36.Value = value[2, 5];
                numericalTextBox44.Value = value[3, 3];
                numericalTextBox45.Value = value[3, 4];
                numericalTextBox46.Value = value[3, 5];
                numericalTextBox55.Value = value[4, 4];
                numericalTextBox56.Value = value[4, 5];
                numericalTextBox66.Value = value[5, 5];
                skip = false;

                SetElasticity();

                compliance = stiffness.TryInverse();
                if (ValueChanged != null)
                    ValueChanged(this, new EventArgs());
            }
            get
            { return stiffness; }
        }

        private bool skip = false;

        private void SetElasticity()
        {
            Symmetry tempSym = SymmetryStatic.Get_Symmetry(SymmetrySeriesNumber);
            //いったんすべてをEnabled=trueにする
            numericalTextBox13.Enabled = numericalTextBox23.Enabled =
            numericalTextBox14.Enabled = numericalTextBox24.Enabled = numericalTextBox34.Enabled =
            numericalTextBox15.Enabled = numericalTextBox25.Enabled = numericalTextBox35.Enabled = numericalTextBox45.Enabled =
            numericalTextBox16.Enabled = numericalTextBox26.Enabled = numericalTextBox36.Enabled = numericalTextBox46.Enabled = numericalTextBox56.Enabled =
            numericalTextBox22.Enabled = numericalTextBox33.Enabled = numericalTextBox55.Enabled = numericalTextBox66.Enabled = true;
            switch (tempSym.CrystalSystemStr)
            {
                case "Unknown": break;
                case "triclinic":
                    break;

                case "monoclinic":
                    switch (tempSym.MainAxis)
                    {
                        case "a":
                            numericalTextBox15.Enabled = numericalTextBox25.Enabled = numericalTextBox35.Enabled =
                            numericalTextBox16.Enabled = numericalTextBox26.Enabled = numericalTextBox36.Enabled =
                            numericalTextBox45.Enabled = numericalTextBox46.Enabled = false;

                            numericalTextBox15.Value = numericalTextBox25.Value = numericalTextBox35.Value =
                            numericalTextBox16.Value = numericalTextBox26.Value = numericalTextBox36.Value =
                            numericalTextBox45.Value = numericalTextBox46.Value = 0;
                            break;

                        case "b":
                            numericalTextBox14.Enabled = numericalTextBox24.Enabled = numericalTextBox34.Enabled =
                            numericalTextBox16.Enabled = numericalTextBox26.Enabled = numericalTextBox36.Enabled =
                            numericalTextBox45.Enabled = numericalTextBox56.Enabled = false;

                            numericalTextBox14.Value = numericalTextBox24.Value = numericalTextBox34.Value =
                            numericalTextBox16.Value = numericalTextBox26.Value = numericalTextBox36.Value =
                            numericalTextBox45.Value = numericalTextBox56.Value = 0;
                            break;

                        case "c":
                            numericalTextBox14.Enabled = numericalTextBox24.Enabled = numericalTextBox34.Enabled =
                            numericalTextBox15.Enabled = numericalTextBox25.Enabled = numericalTextBox35.Enabled =
                            numericalTextBox46.Enabled = numericalTextBox56.Enabled = false;

                            numericalTextBox14.Value = numericalTextBox24.Value = numericalTextBox34.Value =
                            numericalTextBox15.Value = numericalTextBox25.Value = numericalTextBox35.Value =
                            numericalTextBox46.Value = numericalTextBox56.Value = 0;
                            break;
                    }
                    break;

                case "orthorhombic":
                    numericalTextBox14.Enabled = numericalTextBox24.Enabled = numericalTextBox34.Enabled =
                    numericalTextBox15.Enabled = numericalTextBox25.Enabled = numericalTextBox35.Enabled = numericalTextBox45.Enabled =
                    numericalTextBox16.Enabled = numericalTextBox26.Enabled = numericalTextBox36.Enabled = numericalTextBox46.Enabled = numericalTextBox56.Enabled = false;

                    numericalTextBox14.Value = numericalTextBox24.Value = numericalTextBox34.Value =
                    numericalTextBox15.Value = numericalTextBox25.Value = numericalTextBox35.Value = numericalTextBox45.Value =
                    numericalTextBox16.Value = numericalTextBox26.Value = numericalTextBox36.Value = numericalTextBox46.Value = numericalTextBox56.Value = 0;
                    break;

                case "tetragonal":
                    if (tempSym.PointGroupHMStr == "4" || tempSym.PointGroupHMStr == "-4" || tempSym.PointGroupHMStr == "4/m")
                    {
                        numericalTextBox22.Enabled = numericalTextBox23.Enabled =
                        numericalTextBox14.Enabled = numericalTextBox24.Enabled = numericalTextBox34.Enabled =
                        numericalTextBox15.Enabled = numericalTextBox25.Enabled = numericalTextBox35.Enabled = numericalTextBox45.Enabled = numericalTextBox55.Enabled =
                        numericalTextBox26.Enabled = numericalTextBox36.Enabled = numericalTextBox46.Enabled = numericalTextBox56.Enabled = false;

                        numericalTextBox14.Value = numericalTextBox24.Value = numericalTextBox34.Value =
                        numericalTextBox15.Value = numericalTextBox25.Value = numericalTextBox35.Value = numericalTextBox45.Value =
                        numericalTextBox36.Value = numericalTextBox46.Value = numericalTextBox56.Value = 0;
                    }
                    else
                    {
                        numericalTextBox22.Enabled = numericalTextBox23.Enabled = numericalTextBox14.Enabled = numericalTextBox24.Enabled = numericalTextBox34.Enabled =
                        numericalTextBox15.Enabled = numericalTextBox25.Enabled = numericalTextBox35.Enabled = numericalTextBox45.Enabled = numericalTextBox55.Enabled =
                        numericalTextBox16.Enabled = numericalTextBox26.Enabled = numericalTextBox36.Enabled = numericalTextBox46.Enabled = numericalTextBox56.Enabled = false;

                        numericalTextBox14.Value = numericalTextBox24.Value = numericalTextBox34.Value =
                        numericalTextBox15.Value = numericalTextBox25.Value = numericalTextBox35.Value = numericalTextBox45.Value =
                        numericalTextBox16.Value = numericalTextBox26.Value = numericalTextBox36.Value = numericalTextBox46.Value = numericalTextBox56.Value = 0;
                    }
                    break;

                case "trigonal":
                    if (tempSym.PointGroupHMStr == "3" || tempSym.PointGroupHMStr == "-3")
                    {
                        numericalTextBox22.Enabled = numericalTextBox23.Enabled = numericalTextBox24.Enabled = numericalTextBox34.Enabled =
                        numericalTextBox25.Enabled = numericalTextBox35.Enabled = numericalTextBox45.Enabled = numericalTextBox55.Enabled =
                        numericalTextBox16.Enabled = numericalTextBox26.Enabled = numericalTextBox36.Enabled = numericalTextBox46.Enabled = numericalTextBox56.Enabled =
                        numericalTextBox66.Enabled = false;

                        numericalTextBox34.Value = numericalTextBox35.Value = numericalTextBox45.Value =
                        numericalTextBox16.Value = numericalTextBox26.Value = numericalTextBox36.Value = 0;
                    }
                    else
                    {
                        numericalTextBox22.Enabled = numericalTextBox23.Enabled = numericalTextBox24.Enabled = numericalTextBox34.Enabled =
                        numericalTextBox15.Enabled = numericalTextBox25.Enabled = numericalTextBox35.Enabled = numericalTextBox45.Enabled = numericalTextBox55.Enabled =
                        numericalTextBox16.Enabled = numericalTextBox26.Enabled = numericalTextBox36.Enabled = numericalTextBox46.Enabled = numericalTextBox56.Enabled =
                        numericalTextBox66.Enabled = false;

                        numericalTextBox34.Value = numericalTextBox15.Value = numericalTextBox25.Value = numericalTextBox35.Value =
                        numericalTextBox45.Value = numericalTextBox16.Value = numericalTextBox26.Value = numericalTextBox36.Value = numericalTextBox46.Value = 0;
                    }
                    break;

                case "hexagonal":
                    numericalTextBox22.Enabled = numericalTextBox23.Enabled = numericalTextBox14.Enabled = numericalTextBox24.Enabled = numericalTextBox34.Enabled =
                    numericalTextBox15.Enabled = numericalTextBox25.Enabled = numericalTextBox35.Enabled = numericalTextBox45.Enabled =
                    numericalTextBox16.Enabled = numericalTextBox26.Enabled = numericalTextBox36.Enabled = numericalTextBox46.Enabled = numericalTextBox56.Enabled =
                    numericalTextBox55.Enabled = numericalTextBox66.Enabled = false;

                    numericalTextBox14.Value = numericalTextBox24.Value = numericalTextBox34.Value =
                    numericalTextBox15.Value = numericalTextBox25.Value = numericalTextBox35.Value = numericalTextBox45.Value =
                    numericalTextBox16.Value = numericalTextBox26.Value = numericalTextBox36.Value = numericalTextBox46.Value = numericalTextBox56.Value = 0;
                    break;

                case "cubic":
                    numericalTextBox13.Enabled = numericalTextBox23.Enabled = numericalTextBox14.Enabled = numericalTextBox24.Enabled = numericalTextBox34.Enabled =
                    numericalTextBox15.Enabled = numericalTextBox25.Enabled = numericalTextBox35.Enabled = numericalTextBox45.Enabled =
                    numericalTextBox16.Enabled = numericalTextBox26.Enabled = numericalTextBox36.Enabled = numericalTextBox46.Enabled = numericalTextBox56.Enabled =
                    numericalTextBox22.Enabled = numericalTextBox33.Enabled = numericalTextBox55.Enabled = numericalTextBox66.Enabled = false;

                    numericalTextBox14.Value = numericalTextBox24.Value = numericalTextBox34.Value =
                    numericalTextBox15.Value = numericalTextBox25.Value = numericalTextBox35.Value = numericalTextBox45.Value =
                    numericalTextBox16.Value = numericalTextBox26.Value = numericalTextBox36.Value = numericalTextBox46.Value = numericalTextBox56.Value = 0;
                    break;
            }
            numericalTextBoxElasticiry_ValueChanged(numericalTextBox11, new EventArgs());
        }

        private void numericalTextBoxElasticiry_ValueChanged(object sender, EventArgs e)
        {
            if (skip) return;
            if (((NumericBox)sender).Enabled == false) return;
            Symmetry tempSym = SymmetryStatic.Get_Symmetry(SymmetrySeriesNumber);
            switch (tempSym.CrystalSystemStr)
            {
                case "tetragonal":
                    if (tempSym.PointGroupHMStr == "4" || tempSym.PointGroupHMStr == "-4" || tempSym.PointGroupHMStr == "4/m")
                    {
                        numericalTextBox22.Value = numericalTextBox11.Value;
                        numericalTextBox23.Value = numericalTextBox13.Value;
                        numericalTextBox26.Value = -numericalTextBox16.Value;
                        numericalTextBox55.Value = numericalTextBox44.Value;
                    }
                    else
                    {
                        numericalTextBox22.Value = numericalTextBox11.Value;
                        numericalTextBox23.Value = numericalTextBox13.Value;
                        numericalTextBox55.Value = numericalTextBox44.Value;
                    }
                    break;

                case "trigonal":
                    if (tempSym.PointGroupHMStr == "3" || tempSym.PointGroupHMStr == "-3")
                    {
                        numericalTextBox22.Value = numericalTextBox11.Value;
                        numericalTextBox23.Value = numericalTextBox13.Value;
                        numericalTextBox24.Value = -numericalTextBox14.Value;
                        numericalTextBox25.Value = -numericalTextBox15.Value;
                        numericalTextBox46.Value = -numericalTextBox15.Value;
                        numericalTextBox56.Value = numericalTextBox14.Value;
                        numericalTextBox55.Value = numericalTextBox44.Value;
                        numericalTextBox66.Value = (numericalTextBox11.Value - numericalTextBox11.Value) / 2;
                    }
                    else
                    {
                        numericalTextBox22.Value = numericalTextBox11.Value;
                        numericalTextBox23.Value = numericalTextBox13.Value;
                        numericalTextBox24.Value = -numericalTextBox14.Value;
                        numericalTextBox56.Value = numericalTextBox14.Value;
                        numericalTextBox55.Value = numericalTextBox44.Value;
                        numericalTextBox66.Value = (numericalTextBox11.Value - numericalTextBox12.Value) / 2;
                    }

                    break;

                case "hexagonal":
                    numericalTextBox22.Value = numericalTextBox11.Value;
                    numericalTextBox66.Value = (numericalTextBox11.Value - numericalTextBox12.Value) / 2;
                    numericalTextBox23.Value = numericalTextBox13.Value;
                    numericalTextBox55.Value = numericalTextBox44.Value;
                    break;

                case "cubic":
                    numericalTextBox22.Value = numericalTextBox33.Value = numericalTextBox11.Value;
                    numericalTextBox55.Value = numericalTextBox66.Value = numericalTextBox44.Value;
                    numericalTextBox13.Value = numericalTextBox23.Value = numericalTextBox12.Value;
                    break;
            }
            var mtx = new DenseMatrix(6, 6);
            mtx[0, 0] = mtx[0, 0] = numericalTextBox11.Value;
            mtx[0, 1] = mtx[1, 0] = numericalTextBox12.Value;
            mtx[0, 2] = mtx[2, 0] = numericalTextBox13.Value;
            mtx[0, 3] = mtx[3, 0] = numericalTextBox14.Value;
            mtx[0, 4] = mtx[4, 0] = numericalTextBox15.Value;
            mtx[0, 5] = mtx[5, 0] = numericalTextBox16.Value;

            mtx[1, 1] = numericalTextBox22.Value;
            mtx[1, 2] = mtx[2, 1] = numericalTextBox23.Value;
            mtx[1, 3] = mtx[3, 1] = numericalTextBox24.Value;
            mtx[1, 4] = mtx[4, 1] = numericalTextBox25.Value;
            mtx[1, 5] = mtx[5, 1] = numericalTextBox26.Value;

            mtx[2, 2] = numericalTextBox33.Value;
            mtx[2, 3] = mtx[3, 2] = numericalTextBox34.Value;
            mtx[2, 4] = mtx[4, 2] = numericalTextBox35.Value;
            mtx[2, 5] = mtx[5, 2] = numericalTextBox36.Value;

            mtx[3, 3] = numericalTextBox44.Value;
            mtx[3, 4] = mtx[4, 3] = numericalTextBox45.Value;
            mtx[3, 5] = mtx[5, 3] = numericalTextBox46.Value;

            mtx[4, 4] = numericalTextBox55.Value;
            mtx[4, 5] = mtx[5, 4] = numericalTextBox56.Value;

            mtx[5, 5] = numericalTextBox66.Value;

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
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        private void radioButtonStiffness_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonStiffness.Checked)
                Stiffness = compliance.TryInverse();
            else
                Compliance = stiffness.TryInverse();

            if (ValueChanged != null)
                ValueChanged(this, e);
        }
    }
}
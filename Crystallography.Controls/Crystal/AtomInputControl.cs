using System;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class AtomInputControl : UserControl
    {
        //プロパティ
        private bool details1 = false;

        public bool Details1
        {
            set
            {
                details1 = value;
                if (value == false)
                {
                    tableLayoutPanel1.ColumnStyles[4].SizeType = tableLayoutPanel1.ColumnStyles[7].SizeType = SizeType.Absolute;
                    tableLayoutPanel1.ColumnStyles[4].Width = tableLayoutPanel1.ColumnStyles[7].Width = 0;

                    numericalTextBoxXerr.TabStop = numericalTextBoxYerr.TabStop = numericalTextBoxZerr.TabStop = numericalTextBoxOccerr.TabStop = false;
                }
                else
                {
                    tableLayoutPanel1.ColumnStyles[1].SizeType = tableLayoutPanel1.ColumnStyles[3].SizeType = tableLayoutPanel1.ColumnStyles[4].SizeType =
                        tableLayoutPanel1.ColumnStyles[6].SizeType = tableLayoutPanel1.ColumnStyles[7].SizeType = SizeType.Percent;

                    tableLayoutPanel1.ColumnStyles[1].Width = tableLayoutPanel1.ColumnStyles[3].Width = tableLayoutPanel1.ColumnStyles[4].Width
                        = tableLayoutPanel1.ColumnStyles[6].Width = tableLayoutPanel1.ColumnStyles[7].Width = 20;

                    numericalTextBoxXerr.TabStop = numericalTextBoxYerr.TabStop = numericalTextBoxZerr.TabStop = numericalTextBoxOccerr.TabStop = true;
                }
            }
            get { return details1; }
        }

        private bool details2 = false;

        public bool Details2
        {
            set
            {
                details2 = value;
                if (value == false)
                {
                    numericalTextBoxBiso.Width = numericalTextBoxB11.Width = numericalTextBoxB12.Width =
                        numericalTextBoxB13.Width = numericalTextBoxB22.Width = numericalTextBoxB23.Width = numericalTextBoxB33.Width = 60;

                    labelBiso_.Visible = labelB11_.Visible = labelB12_.Visible = labelB13_.Visible = labelB22_.Visible = labelB23_.Visible = labelB33_.Visible =
                        numericalTextBoxBisoerr.Visible = numericalTextBoxB11err.Visible = numericalTextBoxB12err.Visible = numericalTextBoxB13err.Visible = numericalTextBoxB22err.Visible
                 = numericalTextBoxB23err.Visible = numericalTextBoxB33err.Visible = false;
                }
                else
                {
                    numericalTextBoxBiso.Width = numericalTextBoxB11.Width = numericalTextBoxB12.Width =
                        numericalTextBoxB13.Width = numericalTextBoxB22.Width = numericalTextBoxB23.Width = numericalTextBoxB33.Width = 45;

                    numericalTextBoxBisoerr.Visible = labelBiso_.Visible = numericalTextBoxBiso.Visible =
                    numericalTextBoxB33err.Visible = labelB33_.Visible = numericalTextBoxB23err.Visible = labelB23_.Visible =
                    numericalTextBoxB22err.Visible = labelB22_.Visible = numericalTextBoxB13err.Visible = labelB13_.Visible =
                    numericalTextBoxB12err.Visible = labelB12_.Visible = numericalTextBoxB11err.Visible = labelB11_.Visible = true;
                }
            }
            get { return details2; }
        }

        public bool Istoropy
        {
            set
            {
                if (value)
                    radioButtonIsotoropy.Checked = true;
                else
                    radioButtonAnisotropy.Checked = true;
            }
            get { return radioButtonIsotoropy.Checked; }
        }

        public double Biso
        {
            set { numericalTextBoxBiso.Value = value; }
            get { return numericalTextBoxBiso.Value; }
        }

        public double BisoErr
        {
            set { numericalTextBoxBisoerr.Value = value; }
            get { return numericalTextBoxBisoerr.Value; }
        }

        public double B11
        {
            set { numericalTextBoxB11.Value = value; }
            get { return numericalTextBoxB11.Value; }
        }

        public double B11Err
        {
            set { numericalTextBoxB11err.Value = value; }
            get { return numericalTextBoxB11err.Value; }
        }

        public double B12
        {
            set { numericalTextBoxB12.Value = value; }
            get { return numericalTextBoxB12.Value; }
        }

        public double B12Err
        {
            set { numericalTextBoxB12err.Value = value; }
            get { return numericalTextBoxB12err.Value; }
        }

        public double B13
        {
            set { numericalTextBoxB13.Value = value; }
            get { return numericalTextBoxB13.Value; }
        }

        public double B13Err
        {
            set { numericalTextBoxB13err.Value = value; }
            get { return numericalTextBoxB13err.Value; }
        }

        public double B22
        {
            set { numericalTextBoxB22.Value = value; }
            get { return numericalTextBoxB22.Value; }
        }

        public double B22Err
        {
            set { numericalTextBoxB22err.Value = value; }
            get { return numericalTextBoxB22err.Value; }
        }

        public double B23
        {
            set { numericalTextBoxB23.Value = value; }
            get { return numericalTextBoxB23.Value; }
        }

        public double B23Err
        {
            set { numericalTextBoxB23err.Value = value; }
            get { return numericalTextBoxB23err.Value; }
        }

        public double B33
        {
            set { numericalTextBoxB33.Value = value; }
            get { return numericalTextBoxB33.Value; }
        }

        public double B33Err
        {
            set { numericalTextBoxB33err.Value = value; }
            get { return numericalTextBoxB33err.Value; }
        }

        public double X
        {
            set { numericTextBoxX.Value = value; }
            get { return numericTextBoxX.Value; }
        }

        public double XErr
        {
            set { numericalTextBoxXerr.Value = value; }
            get { return numericalTextBoxXerr.Value; }
        }

        public double Y
        {
            set { numericTextBoxY.Value = value; }
            get { return numericTextBoxY.Value; }
        }

        public double YErr
        {
            set { numericalTextBoxYerr.Value = value; }
            get { return numericalTextBoxYerr.Value; }
        }

        public double Z
        {
            set { numericTextBoxZ.Value = value; }
            get { return numericTextBoxZ.Value; }
        }

        public double ZErr
        {
            set { numericalTextBoxZerr.Value = value; }
            get { return numericalTextBoxZerr.Value; }
        }

        public double Occ
        {
            set { numericTextBoxOcc.Value = value; }
            get { return numericTextBoxOcc.Value; }
        }

        public double OccErr
        {
            set { numericalTextBoxOccerr.Value = value; }
            get { return numericalTextBoxOccerr.Value; }
        }

        public string Label
        {
            set { textBoxLabel.Text = value; }
            get { return textBoxLabel.Text; }
        }

        /*public int atomSeriesNum = 0;
        public int AtomSeriesNum
        {
            set { atomSeriesNum = value; }
            get { return atomSeriesNum; }
        }*/

        public int AtomNo
        {
            set { if (value >= 0) comboBoxAtom.SelectedIndex = value - 1; }
            get { return comboBoxAtom.SelectedIndex + 1; }
        }

        public int AtomSubNoXray
        {
            set { comboBoxScatteringFactorXray.SelectedIndex = value; }
            get { return comboBoxScatteringFactorXray.SelectedIndex; }
        }

        public int AtomSubNoElectron
        {
            set { comboBoxScatteringFactorElectron.SelectedIndex = value; }
            get { return comboBoxScatteringFactorElectron.SelectedIndex; }
        }

        private double[] isotopicComposition;

        public double[] IsotopicComposition
        {
            set
            {
                isotopicComposition = value;
                if (isotopicComposition == null || isotopicComposition.Length != AtomConstants.IsotopeAbundance[AtomNo].Count)
                    comboBoxNeutron.SelectedIndex = 0;
                else
                    comboBoxNeutron.SelectedIndex = 1;

                comboBoxNeutron_SelectedIndexChanged(new object(), new EventArgs());
            }
            get { return isotopicComposition; }
        }

        private void radioButtonIsotoropy_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanelAniso1.Visible = flowLayoutPanelAniso2.Visible = !radioButtonIsotoropy.Checked;
            flowLayoutPanelIso.Visible = radioButtonIsotoropy.Checked;
        }

        public AtomInputControl()
        {
            InitializeComponent();
            //   toolTip.SetTooltipToUsercontrol(this);
        }

        private void flowLayoutPanelPosition_Resize(object sender, EventArgs e)
        {
        }

        //原子番号コンボ
        private void comboBoxAtom_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (comboBoxAtom.SelectedIndex < 0) return;
            comboBoxScatteringFactorXray.Items.Clear();
            comboBoxScatteringFactorElectron.Items.Clear();

            for (int i = 0; i < AtomConstants.XrayScattering[AtomNo].Length; i++)
                comboBoxScatteringFactorXray.Items.Add(AtomConstants.XrayScattering[AtomNo][i].Method);

            for (int i = 0; i < AtomConstants.ElectronScattering[AtomNo].Length; i++)
                comboBoxScatteringFactorElectron.Items.Add(AtomConstants.ElectronScattering[AtomNo][i].Method);

            comboBoxScatteringFactorXray.SelectedIndex = 0;
            comboBoxScatteringFactorElectron.SelectedIndex = 0;
            comboBoxNeutron.SelectedIndex = 0;
            comboBoxNeutron_SelectedIndexChanged(new object(), new EventArgs());
        }

        //散乱因子を選択変更されたら
        private void comboBoxAtomSub_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            /*    AtomicScatteringFactor asf;
                for (int n = 1; n <= 211; n++)
                {
                    asf = AtomicScatteringFactor.GetCoefficientForXray(n);
                    if (asf.Methods == (string)comboBoxScatteringFactorXray.SelectedItem)
                        atomSeriesNum = n;
                }*/
        }

        private void buttonAtomInfoDetails_Click(object sender, EventArgs e)
        {
            Details1 = checkBoxDetail1.Checked;
        }

        private void checkBoxDetails2_CheckedChanged(object sender, EventArgs e)
        {
            Details2 = checkBoxDetails2.Checked;
        }

        private void AtomInputControl_Load(object sender, EventArgs e)
        {
            comboBoxNeutron.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void textBoxLabel_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxNeutron_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEditIsotopeAbundance.Enabled = comboBoxNeutron.SelectedIndex == 1;

            richTextBoxIsotope.Clear();
            int n = 0;
            foreach (int z in AtomConstants.IsotopeAbundance[AtomNo].Keys)
            {
                richTextBoxIsotope.SelectionColor = Color.DarkBlue;
                if (richTextBoxIsotope.Text != "")
                    richTextBoxIsotope.SelectedText = ", ";

                richTextBoxIsotope.SelectionCharOffset = 3;
                richTextBoxIsotope.SelectionFont = new Font("Tahoma", 6f, FontStyle.Regular);
                richTextBoxIsotope.SelectedText = z.ToString();

                richTextBoxIsotope.SelectionCharOffset = 0;
                richTextBoxIsotope.SelectionFont = new Font("Tahoma", 9f, FontStyle.Regular);
                richTextBoxIsotope.SelectedText = AtomConstants.AtomicName(AtomNo) + ": ";

                richTextBoxIsotope.SelectionColor = Color.Black;
                if (comboBoxNeutron.SelectedIndex == 0 || isotopicComposition == null || isotopicComposition.Length != AtomConstants.IsotopeAbundance[AtomNo].Count)
                    richTextBoxIsotope.SelectedText = AtomConstants.IsotopeAbundance[AtomNo][z].ToString();
                else
                    richTextBoxIsotope.SelectedText = isotopicComposition[n++].ToString();

                richTextBoxIsotope.SelectionColor = Color.DarkBlue;
                richTextBoxIsotope.SelectionFont = new Font("Tahoma", 9f, FontStyle.Regular);
                richTextBoxIsotope.SelectedText = "%";
                //labelIsotopeAbundance.Text +=  + ":" + AtomConstants.IsotopeAbundance[AtomNo][z].ToString() + "%, ";
            }
        }

        private void buttonEditIsotopeAbundance_Click(object sender, EventArgs e)
        {
            FormIsotopeComposition formIsotopeComposition = new FormIsotopeComposition();
            formIsotopeComposition.AtomNumber = AtomNo;
            formIsotopeComposition.IsotopicComposition = isotopicComposition;
            if (formIsotopeComposition.ShowDialog() == DialogResult.OK)
                IsotopicComposition = formIsotopeComposition.IsotopicComposition;
        }

        private void numericTextBoxZ_Load(object sender, EventArgs e)
        {
        }
    }
}
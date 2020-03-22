using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Crystallography;

namespace Crystallography.Controls
{
    public partial class CrystalControl : UserControl
    {
        public CrystalControl()
        {
            InitializeComponent();

            formCrystallographicInformation = new FormCrystallographicInformation();
            formCrystallographicInformation.crystalControl = this;
            formCrystallographicInformation.Visible = false;
        }


        public int atomSeriesNum;
        public int SymmetrySeriesNumber;
        public Crystal presentCrystal;
        bool IsSkipChangeEvent;

        public delegate void MyEventHandler(Crystal crystal);
        public event MyEventHandler CrystalChanged;

        FormCrystallographicInformation formCrystallographicInformation = new FormCrystallographicInformation();


        private void CrystalForm_Load(object sender, System.EventArgs e)
        {
            string str = "Label" + "\t" + "Element" + "\t" + "X" + "\t" + "Y" + "\t" + "Z" + "\t" + "Occ" + "\t" + "Multi." + "\t" + "WyckLet" + "\t" + "SiteSym";
            listBox1.Items.Add(str);
            str = "No.\tX\tY\tZ";
        }

        bool IsComboBoxChangeEventSkip = false;
        private void comboBoxCrystalSystem_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            IsComboBoxChangeEventSkip = true;
            comboBoxPointGroup.Items.Clear();
            comboBoxSpaceGroup.Items.Clear();
            Symmetry symmetry;
            for (int n = 0; n < Symmetry.BelongingNumberOfSymmetry[comboBoxCrystalSystem.SelectedIndex].Length; n++)
            {
                symmetry = Symmetry.Get_Symmetry(Symmetry.BelongingNumberOfSymmetry[comboBoxCrystalSystem.SelectedIndex][n][0]);
                if (symmetry.CrystalSystem == comboBoxCrystalSystem.Text)
                    if (comboBoxPointGroup.Items.Contains(symmetry.PointGroupHM) == false)
                        comboBoxPointGroup.Items.Add(symmetry.PointGroupHM);
            }
            IsComboBoxChangeEventSkip = false;
            comboBoxPointGroup.SelectedIndex = 0;
            comboBoxPointGroup_SelectedIndexChanged(new object(), new System.EventArgs());
        }

        private void comboBoxPointGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (IsComboBoxChangeEventSkip) return;
            IsComboBoxChangeEventSkip = true;
            comboBoxSpaceGroup.Items.Clear();
            Symmetry symmetry;
            for (int n = 0; n < Symmetry.BelongingNumberOfSymmetry[comboBoxCrystalSystem.SelectedIndex][comboBoxPointGroup.SelectedIndex].Length; n++)
            {
                symmetry = Symmetry.Get_Symmetry(Symmetry.BelongingNumberOfSymmetry[comboBoxCrystalSystem.SelectedIndex][comboBoxPointGroup.SelectedIndex][n]);
                if (symmetry.PointGroupHM == comboBoxPointGroup.Text)
                    comboBoxSpaceGroup.Items.Add(symmetry.SpaceGroupHM);
            }
            IsComboBoxChangeEventSkip = false;
            comboBoxSpaceGroup.SelectedIndex = 0;
            comboBoxSpaceGroup_SelectedIndexChanged(new object(), new System.EventArgs());
        }

        public void comboBoxSpaceGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (IsComboBoxChangeEventSkip) return;
            SymmetrySeriesNumber = Symmetry.BelongingNumberOfSymmetry[comboBoxCrystalSystem.SelectedIndex][comboBoxPointGroup.SelectedIndex][comboBoxSpaceGroup.SelectedIndex];
            SetCrystalFromForm();
        }

        private void SetTextBox()
        {
            if (IsSkipChangeEvent) return;
            Symmetry tempSym = Symmetry.Get_Symmetry(SymmetrySeriesNumber);
            IsSkipChangeEvent = true;
            //いったんすべてをreadonly=falseにする
            numericTextBoxA.ReadOnly = numericTextBoxB.ReadOnly = numericTextBoxC.ReadOnly = numericTextBoxAlpha.ReadOnly = numericTextBoxBeta.ReadOnly = numericTextBoxGamma.ReadOnly = false;
            switch (tempSym.CrystalSystem)
            {
                case "Unknown": break;
                case "triclinic": break;
                case "monoclinic":
                    switch (tempSym.MainAxis)
                    {
                        case "a":
                            numericTextBoxBeta.ReadOnly = numericTextBoxGamma.ReadOnly = true;
                            numericTextBoxBeta.NumericalValue = numericTextBoxGamma.NumericalValue = 90; break;
                        case "b":
                            numericTextBoxAlpha.ReadOnly = numericTextBoxGamma.ReadOnly = true;
                            numericTextBoxAlpha.NumericalValue = numericTextBoxGamma.NumericalValue = 90; break;
                        case "c": numericTextBoxAlpha.ReadOnly = numericTextBoxBeta.ReadOnly = true;
                            numericTextBoxAlpha.NumericalValue = numericTextBoxBeta.NumericalValue = 90; break;
                    } break;
                case "orthorhombic":
                    numericTextBoxAlpha.ReadOnly = numericTextBoxBeta.ReadOnly = numericTextBoxGamma.ReadOnly = true;
                    numericTextBoxAlpha.NumericalValue = numericTextBoxBeta.NumericalValue = numericTextBoxGamma.NumericalValue = 90; break;
                case "tetragonal":
                    numericTextBoxB.ReadOnly = true; numericTextBoxB.NumericalValue = numericTextBoxA.NumericalValue;
                    numericTextBoxAlpha.ReadOnly = numericTextBoxBeta.ReadOnly = numericTextBoxGamma.ReadOnly = true;
                    numericTextBoxAlpha.NumericalValue = numericTextBoxBeta.NumericalValue = numericTextBoxGamma.NumericalValue = 90; break;
                case "trigonal":
                    switch (tempSym.SpaceGroupHM.IndexOf("Rho") >= 0 && tempSym.SpaceGroupHM.IndexOf("R") >= 0)
                    {
                        case false:
                            numericTextBoxB.ReadOnly = true; numericTextBoxB.NumericalValue = numericTextBoxA.NumericalValue;
                            numericTextBoxAlpha.ReadOnly = numericTextBoxBeta.ReadOnly = numericTextBoxGamma.ReadOnly = true;
                            numericTextBoxAlpha.NumericalValue = numericTextBoxBeta.NumericalValue = 90; numericTextBoxGamma.NumericalValue = 120; break;
                        case true:
                            numericTextBoxB.ReadOnly = numericTextBoxC.ReadOnly = true;
                            numericTextBoxC.NumericalValue = numericTextBoxB.NumericalValue = numericTextBoxA.NumericalValue;
                            numericTextBoxBeta.ReadOnly = numericTextBoxGamma.ReadOnly = true;
                            numericTextBoxGamma.NumericalValue = numericTextBoxBeta.NumericalValue = numericTextBoxAlpha.NumericalValue; break;
                    } break;
                case "hexagonal":
                    numericTextBoxB.ReadOnly = true;
                    numericTextBoxB.NumericalValue = numericTextBoxA.NumericalValue;
                    numericTextBoxAlpha.ReadOnly = numericTextBoxBeta.ReadOnly = numericTextBoxGamma.ReadOnly = true;
                    numericTextBoxAlpha.NumericalValue = numericTextBoxBeta.NumericalValue = 90; numericTextBoxGamma.NumericalValue = 120; break;
                case "cubic":
                    numericTextBoxB.ReadOnly = numericTextBoxC.ReadOnly = true;
                    numericTextBoxC.NumericalValue = numericTextBoxB.NumericalValue = numericTextBoxA.NumericalValue;
                    numericTextBoxAlpha.ReadOnly = numericTextBoxBeta.ReadOnly = numericTextBoxGamma.ReadOnly = true;
                    numericTextBoxAlpha.NumericalValue = numericTextBoxBeta.NumericalValue = numericTextBoxGamma.NumericalValue = 90; break;
            }
            IsSkipChangeEvent = false;
        }

        /// <summary>
        /// 現在の入力情報からCrystalを生成する
        /// </summary>
        public void SetCrystalFromForm()
        {
            if (IsSkipChangeEvent) return;
            double a = numericTextBoxA.NumericalValue;
            double b = numericTextBoxB.NumericalValue;
            double c = numericTextBoxC.NumericalValue;
            double alpha = numericTextBoxAlpha.NumericalValue;
            double beta = numericTextBoxBeta.NumericalValue;
            double gamma = numericTextBoxGamma.NumericalValue;
            if (alpha < 0 || beta < 0 || gamma < 0 || alpha > 180 || beta > 180 || gamma > 180)
            {
                MessageBox.Show("0～180の範囲で入力してください");
                return;
            }

            //対称性が変更されているかもしれないので原子も改めて設定しなおす。
            List<Atoms> atoms = new List<Atoms>();
            for (int i = 0; i < listBoxAtoms.Items.Count; i++)
            {
                Atoms atom = (Atoms)listBoxAtoms.Items[i];
                Atoms temp = new Atoms(atom.label, atom.scatteringFactorNumber, SymmetrySeriesNumber, new Vector3D(atom.X, atom.Y, atom.Z), atom.occ, atom.dsf);

                //temp.color = atom.color;
                temp.argb = atom.argb;
                temp.ambient = atom.ambient;
                temp.diffusion = atom.diffusion;
                temp.emission = atom.emission;
                temp.shininess = atom.shininess;
                temp.specular = atom.specular;
                temp.transparency = atom.transparency;
                temp.radius = atom.radius;

                listBoxAtoms.Items[i] = temp;
                atoms.Add(temp);
            }

            //Bonds&Polyhedra中のリストボックスの変更
            comboBoxBondingAtom1.Items.Clear();
            comboBoxBondingAtom2.Items.Clear();
            for (int i = 0; i < atoms.Count; i++)
                if (!comboBoxBondingAtom1.Items.Contains(atoms[i].element))
                {
                    comboBoxBondingAtom1.Items.Add(atoms[i].element);
                    comboBoxBondingAtom2.Items.Add(atoms[i].element);
                }
            //BondsをListBoxから取得
            List<Bonds> bonds = new List<Bonds>();
            for (int i = 0; i < listBoxBondsAndPolyhedra.Items.Count; i++)
                bonds.Add((Bonds)listBoxBondsAndPolyhedra.Items[i]);

            string aurhor = textBoxAuthor.Text;
            presentCrystal = new Crystal(a / 10, b / 10, c / 10, alpha * Math.PI / 180, beta * Math.PI / 180, gamma * Math.PI / 180,
                SymmetrySeriesNumber, textBoxName.Text, textBoxMemo.Text, pictureBoxColor.BackColor,
                atoms.ToArray(), aurhor, textBoxJournal.Text, textBoxTitle.Text, bonds);
            //presentCrystal.compressibilityBperA = compressibilityBperA;
            //presentCrystal.compressibilityCperA = compressibilityCperA;
            SetFormFromPresentCrystal();

            if (CrystalChanged != null)
                CrystalChanged(presentCrystal);
        }

        private void textBoxNumOnly_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar < '.' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != 3 && e.KeyChar != 22)
                e.Handled = true;
        }


        /// <summary>
        /// 外部から結晶を変更されたとき
        /// </summary>
        /// <param name="crystal"></param>
        public void ChangeCrystal(Crystal crystal)
        {
            if (crystal == null)
                return;

            presentCrystal = crystal;
            if (CrystalChanged != null)
                CrystalChanged(presentCrystal);
            SetFormFromPresentCrystal();
        }

        /// <summary>
        /// 現在のpresentCrystalにあわせてFormのテキストボックスなどを設定する。
        /// </summary>
        /// <param name="crystal"></param>
        private void SetFormFromPresentCrystal()
        {
            if (IsSkipChangeEvent) return;
            IsSkipChangeEvent = true;

            pictureBoxColor.BackColor = Color.FromArgb(presentCrystal.argb);
            textBoxName.Text = presentCrystal.name;
            textBoxMemo.Text = presentCrystal.note;

            textBoxAuthor.Text = presentCrystal.publAuthorName;
            textBoxJournal.Text = presentCrystal.journal;
            textBoxFormula.Text = presentCrystal.chemicalFormulaSum;
            textBoxTitle.Text = presentCrystal.publSectionTitle;

            textBoxFormula.Text = presentCrystal.chemicalFormulaSum;

            textBoxDensity.Text = presentCrystal.density.ToString("f5");
            textBoxVolume.Text = (presentCrystal.volume * 1000).ToString("f5");
            textBoxZnumber.Text = presentCrystal.chemicalFormulaZ.ToString();

            if (!IsSkipA) numericTextBoxA.NumericalValue = Math.Round((presentCrystal.a * 10),7);
            if (!IsSkipB) numericTextBoxB.NumericalValue = Math.Round((presentCrystal.b * 10), 7);
            if (!IsSkipC) numericTextBoxC.NumericalValue = Math.Round((presentCrystal.c * 10), 7);
            if (!IsSkipAlpha) numericTextBoxAlpha.NumericalValue = Math.Round(presentCrystal.alfa * 180 / Math.PI, 7);
            if (!IsSkipBeta) numericTextBoxBeta.NumericalValue = Math.Round(presentCrystal.beta * 180 / Math.PI, 7);
            if (!IsSkipGamma) numericTextBoxGamma.NumericalValue = Math.Round(presentCrystal.gamma * 180 / Math.PI, 7);

            comboBoxCrystalSystem.Text = presentCrystal.symmetry.CrystalSystem;
            comboBoxPointGroup.Text = presentCrystal.symmetry.PointGroupHM;
            comboBoxSpaceGroup.Text = presentCrystal.symmetry.SpaceGroupHM;

            listBoxAtoms.Items.Clear();
            Atoms atoms;
            if (presentCrystal.atoms != null)
                for (int i = 0; i < presentCrystal.atoms.Length; i++)
                {
                    atoms = presentCrystal.atoms[i];
                    listBoxAtoms.Items.Add(atoms);
                }

            //Bonds&Polyhedra中のリストボックスの変更
            comboBoxBondingAtom1.Items.Clear();
            comboBoxBondingAtom1.Text = "";
            comboBoxBondingAtom2.Items.Clear();
            comboBoxBondingAtom2.Text = "";
            for (int i = 0; i < presentCrystal.atoms.Length; i++)
                if (!comboBoxBondingAtom1.Items.Contains(presentCrystal.atoms[i].element))
                {
                    comboBoxBondingAtom1.Items.Add(presentCrystal.atoms[i].element);
                    comboBoxBondingAtom2.Items.Add(presentCrystal.atoms[i].element);
                }
            //listBoxBondsAndPolyhedraにBondsを追加
            listBoxBondsAndPolyhedra.Items.Clear();
            if (presentCrystal.bonds != null)
                for (int i = 0; i < presentCrystal.bonds.Count; i++)
                    listBoxBondsAndPolyhedra.Items.Add(presentCrystal.bonds[i]);

            IsSkipChangeEvent = false;
            SetTextBox();

        }

        private void pictureBoxColor_Click(object sender, System.EventArgs e)
        {
            if (presentCrystal == null) return;
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.Color = ((PictureBox)sender).BackColor;
            colorDialog.AllowFullOpen = true; colorDialog.AnyColor = true; colorDialog.SolidColorOnly = false; colorDialog.ShowHelp = true;
            colorDialog.ShowDialog();
            ((PictureBox)sender).BackColor = colorDialog.Color;
        }

        private void textBoxName_TextChanged(object sender, System.EventArgs e)
        {
            SetCrystalFromForm();
        }


        //原子追加ボタン
        private void buttonAddAtom_Click(object sender, System.EventArgs e)
        {
            Atoms atoms = GetAtomsFromTextBox();
            listBoxAtoms.Items.Add(atoms);
            SetCrystalFromForm();
            listBoxAtoms.SelectedIndex = listBoxAtoms.Items.Count - 1;
        }

        //原子変更ボタン
        private void buttonChangeAtom_Click(object sender, System.EventArgs e)
        {
            if (listBoxAtoms.SelectedIndex < 0) return;
            Atoms atoms = GetAtomsFromTextBox();
            int selectedIndex = listBoxAtoms.SelectedIndex;
            listBoxAtoms.Items.RemoveAt(selectedIndex);
            listBoxAtoms.Items.Insert(selectedIndex, atoms);

            SetCrystalFromForm();
            listBoxAtoms.SelectedIndex = selectedIndex;
        }

        //原子削除ボタン
        private void buttonDeleteAtom_Click(object sender, System.EventArgs e)
        {
            int selectedIndex = listBoxAtoms.SelectedIndex;
            if (listBoxAtoms.SelectedIndex < 0) return;
            else
                listBoxAtoms.Items.Remove(listBoxAtoms.SelectedItem);
            SetCrystalFromForm();
            //選択列を選択しなおす
            if (listBoxAtoms.Items.Count > selectedIndex)
                listBoxAtoms.SelectedIndex = selectedIndex;
            else
                listBoxAtoms.SelectedIndex = selectedIndex - 1;
        }

        //Bond追加
        private void buttonAddBond_Click(object sender, EventArgs e)
        {
            if (comboBoxBondingAtom1.Items.Count < 1 || comboBoxBondingAtom1.Text == "" || comboBoxBondingAtom2.Text == "") return;
            listBoxBondsAndPolyhedra.Items.Add(new Bonds(
                comboBoxBondingAtom1.Text, comboBoxBondingAtom2.Text,
                (float)numericUpDownBondMinLength.Value, (float)numericUpDownBondMaxLength.Value,
                (float)numericUpDownBondRadius.Value, (float)numericUpDownBondTrasparency.Value,
                pictureBoxBondColor.BackColor, (float)numericUpDownPolyhedronPlaneAlpha.Value,
                checkBoxShowPolyhedron.Checked, checkBoxShowCenterAtom.Checked, checkBoxShowVertexAtoms.Checked,
                checkBoxShowInnerBonds.Checked, pictureBoxPolyhedronColor.BackColor, checkBoxShowEdges.Checked,
                (float)numericUpDownEdgeLineWidth.Value, pictureBoxEdges.BackColor));
            listBoxBondsAndPolyhedra.SelectedIndex = listBoxBondsAndPolyhedra.Items.Count - 1;
            SetCrystalFromForm();
            listBoxBondsAndPolyhedra.SelectedIndex = listBoxBondsAndPolyhedra.Items.Count - 1;
        }
        //Bond変更ボタン
        private void buttonChangeBond_Click(object sender, EventArgs e)
        {
            if (listBoxBondsAndPolyhedra.SelectedIndex < 0) return;
            Bonds bonds = new Bonds(
                comboBoxBondingAtom1.Text, comboBoxBondingAtom2.Text,
                (float)numericUpDownBondMinLength.Value, (float)numericUpDownBondMaxLength.Value,
                (float)numericUpDownBondRadius.Value, (float)numericUpDownBondTrasparency.Value,
                pictureBoxBondColor.BackColor, (float)numericUpDownPolyhedronPlaneAlpha.Value,
                checkBoxShowPolyhedron.Checked, checkBoxShowCenterAtom.Checked, checkBoxShowVertexAtoms.Checked,
                checkBoxShowInnerBonds.Checked, pictureBoxPolyhedronColor.BackColor, checkBoxShowEdges.Checked,
                (float)numericUpDownEdgeLineWidth.Value, pictureBoxEdges.BackColor);
            int selectedIndex = listBoxBondsAndPolyhedra.SelectedIndex;
            listBoxBondsAndPolyhedra.Items.RemoveAt(selectedIndex);
            listBoxBondsAndPolyhedra.Items.Insert(selectedIndex, bonds);

            SetCrystalFromForm();
            listBoxBondsAndPolyhedra.SelectedIndex = selectedIndex;
        }
        //Bond削除
        private void buttonDeleteBond_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxBondsAndPolyhedra.SelectedIndex;
            if (listBoxBondsAndPolyhedra.SelectedIndex < 0) return;
            else listBoxBondsAndPolyhedra.Items.Remove(listBoxBondsAndPolyhedra.SelectedItem);
            SetCrystalFromForm();
            //選択列を選択しなおす
            if (listBoxBondsAndPolyhedra.Items.Count > selectedIndex)
                listBoxBondsAndPolyhedra.SelectedIndex = selectedIndex;
            else
                listBoxBondsAndPolyhedra.SelectedIndex = selectedIndex - 1;
        }



        //テキストボックスの入力値からatomsを返す
        private Atoms GetAtomsFromTextBox()
        {
            double x, y, z, occ, Biso, B11, B22, B33, B12, B23, B31;
            x = y = z = occ = Biso = B11 = B22 = B33 = B12 = B23 = B31 = 0;

            x = this.numericTextBoxX.NumericalValue;
            y = this.numericTextBoxY.NumericalValue;
            z = this.numericTextBoxZ.NumericalValue;
            try
            {
                occ = this.numericTextBoxOcc.NumericalValue;
                Biso = Convert.ToDouble(this.textBoxBiso.Text);
                B11 = Convert.ToDouble(this.textBoxB11.Text);
                B22 = Convert.ToDouble(this.textBoxB22.Text);
                B33 = Convert.ToDouble(this.textBoxB33.Text);
                B12 = Convert.ToDouble(this.textBoxB12.Text);
                B23 = Convert.ToDouble(this.textBoxB23.Text);
                B31 = Convert.ToDouble(this.textBoxB13.Text);
            }
            catch
            {
                MessageBox.Show("数値を入力してください。"); return new Atoms();
            }
            DiffuseScatteringFactor dsf = new DiffuseScatteringFactor(radioButtonIsotoropy.Checked, Biso, B11, B22, B33, B12, B23, B31);
            Atoms atoms = new Atoms(textBoxLabel.Text, atomSeriesNum, SymmetrySeriesNumber, new Vector3D(x, y, z), occ, dsf);

            atoms.argb = pictureBoxAtomColor.BackColor.ToArgb();
            atoms.ambient = (float)numericUpDownAtomAmbient.Value;
            atoms.diffusion = (float)numericUpDownAtomDiffusion.Value;
            atoms.emission = (float)numericUpDownAtomEmmision.Value;
            atoms.shininess = (float)numericUpDownAtomShininess.Value;
            atoms.specular = (float)numericUpDownAtomSpecular.Value;
            atoms.transparency = (float)numericUpDownAtomTransparency.Value;
            atoms.radius = (float)numericUpDownAtomRadius.Value;

            return atoms;
        }

        private void textBoxX_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar < '.' || e.KeyChar > '9') && e.KeyChar != '\b')
                e.Handled = true;
        }




        //原子番号コンボ
        private void comboBoxAtom_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (comboBoxAtom.SelectedIndex < 0) return;
            comboBoxAtomSub.Items.Clear();
            AtomicScatteringFactor asf;
            for (int n = 1; n <= 211; n++)
            {
                asf = AtomicScatteringFactor.GetCoefficient(n);
                if (asf.AtomicNumber == comboBoxAtom.SelectedIndex + 1)
                    comboBoxAtomSub.Items.Add(asf.NameSub);
            }
            comboBoxAtomSub.SelectedIndex = 0;
        }

        //散乱因子を選択変更されたら
        private void comboBoxAtomSub_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            AtomicScatteringFactor asf;
            for (int n = 1; n <= 211; n++)
            {
                asf = AtomicScatteringFactor.GetCoefficient(n);
                if (asf.NameSub == (string)comboBoxAtomSub.SelectedItem)
                    atomSeriesNum = n;
            }
        }

        //原子位置リストボックスを変更
        private void listBoxAtoms_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listBoxAtoms.SelectedIndex < 0) return;
            else
            {
                int selectedIndex = listBoxAtoms.SelectedIndex;
                SetTextBoxFromAtoms((Atoms)listBoxAtoms.SelectedItem);
                listBoxAtoms.SelectedIndex = selectedIndex;
            }
        }

        //Atomsからテキストボックスを変更
        private void SetTextBoxFromAtoms(Atoms atoms)
        {
            textBoxLabel.Text = atoms.label;
            comboBoxAtom.SelectedIndex = atoms.asf.AtomicNumber - 1;
            comboBoxAtomSub.SelectedIndex = atoms.asf.AtomicNumberSub - 1;
            numericTextBoxX.NumericalValue = atoms.X;
            numericTextBoxY.NumericalValue = atoms.Y;
            numericTextBoxZ.NumericalValue = atoms.Z;
            numericTextBoxOcc.NumericalValue = atoms.occ;
            textBoxBiso.Text = atoms.dsf.Biso.ToString();
            textBoxB11.Text = atoms.dsf.B11.ToString();
            textBoxB12.Text = atoms.dsf.B12.ToString();
            textBoxB13.Text = atoms.dsf.B31.ToString();
            textBoxB22.Text = atoms.dsf.B22.ToString();
            textBoxB23.Text = atoms.dsf.B23.ToString();
            textBoxB33.Text = atoms.dsf.B33.ToString();
            if (atoms.dsf.IsIso)
                radioButtonIsotoropy.Checked = true;
            else
                radioButtonAnisotropy.Checked = true;

            numericUpDownAtomAmbient.Value = (decimal)atoms.ambient;
            numericUpDownAtomDiffusion.Value = (decimal)atoms.diffusion;
            numericUpDownAtomEmmision.Value = (decimal)atoms.emission;
            numericUpDownAtomShininess.Value = (decimal)atoms.shininess;
            numericUpDownAtomSpecular.Value = (decimal)atoms.specular;

            numericUpDownAtomRadius.Value = (decimal)atoms.radius;
            numericUpDownAtomTransparency.Value = (decimal)atoms.transparency;

            pictureBoxAtomColor.BackColor = Color.FromArgb(atoms.argb);

        }

        private void radioButtonIsotoropy_CheckedChanged(object sender, System.EventArgs e)
        {
            if (radioButtonIsotoropy.Checked)
            {
                textBoxBiso.Enabled = true;
                textBoxB11.Enabled = textBoxB22.Enabled = textBoxB33.Enabled = textBoxB12.Enabled = textBoxB23.Enabled = textBoxB13.Enabled = false;
            }
            else
            {
                textBoxBiso.Enabled = false;
                textBoxB11.Enabled = textBoxB22.Enabled = textBoxB33.Enabled = textBoxB12.Enabled = textBoxB23.Enabled = textBoxB13.Enabled = true;
            }

        }

        private void textBoxSearch_TextChanged(object sender, System.EventArgs e)
        {
            comboBoxSearchResult.Items.Clear();
            comboBoxSearchResult.Enabled = false;
            char[] c;
            if (textBoxSearch.Text.Length == 0)
                return;
            else
                c = textBoxSearch.Text.ToCharArray();
            Symmetry sym;
            int startIndex = 0;
            int index;
            for (int n = 0; n <= 534; n++)
            {
                sym = Symmetry.Get_Symmetry(n);
                startIndex = -1;
                for (int i = 0; i < c.Length; i++)
                {
                    index = sym.SpaceGroupHM.IndexOf(c[i], startIndex + 1);
                    if (index >= 0)
                        startIndex = index;
                    else
                    {
                        startIndex = -1;
                        break;
                    }
                }
                if (startIndex >= 0)
                    comboBoxSearchResult.Items.Add(sym.SpaceGroupHM);
            }
            if (comboBoxSearchResult.Items.Count > 0)
                comboBoxSearchResult.Enabled = true;

        }

        private void comboBoxSearchResult_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Symmetry sym = Symmetry.Get_Symmetry(0);
            for (int n = 0; n <= 534; n++)
            {
                sym = Symmetry.Get_Symmetry(n);
                if (comboBoxSearchResult.Text == sym.SpaceGroupHM)
                    break;
            }
            comboBoxCrystalSystem.Text = sym.CrystalSystem;
            comboBoxPointGroup.Text = sym.PointGroupHM;
            comboBoxSpaceGroup.Text = sym.SpaceGroupHM;
        }


        private void FormCrystal_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (fileName.Length == 1)
            {
                try
                {
                    ChangeCrystal(ConvertCrystalData.ConvertToCrystal(fileName[0]));
                }
                catch { return; }
            }
        }

        private void FormCrystal_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        #region 格子乗数テキストボックスの変更イベント
        bool IsSkipA, IsSkipB, IsSkipC, IsSkipAlpha, IsSkipBeta, IsSkipGamma;

        private void textBoxA_TextChanged(object sender, EventArgs e)
        {
            IsSkipA = true;
            SetCrystalFromForm();
            IsSkipA = false;
        }

        private void textBoxB_TextChanged(object sender, EventArgs e)
        {
            IsSkipB = true;
            SetCrystalFromForm();
            IsSkipB = false;
        }

        private void textBoxC_TextChanged(object sender, EventArgs e)
        {
            IsSkipC = true;
            SetCrystalFromForm();
            IsSkipC = false;
        }

        private void textBoxAlfa_TextChanged(object sender, EventArgs e)
        {
            IsSkipAlpha = true;
            SetCrystalFromForm();
            IsSkipAlpha = false;
        }

        private void textBoxBeta_TextChanged(object sender, EventArgs e)
        {
            IsSkipBeta = true;
            SetCrystalFromForm();
            IsSkipBeta = false;
        }

        private void textBoxGamma_TextChanged(object sender, EventArgs e)
        {
            IsSkipGamma = true;
            SetCrystalFromForm();
            IsSkipGamma = false;
        }
        #endregion

        private void listBoxAtoms_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Atoms atoms;
                if (listBoxAtoms.SelectedIndex >= 0)
                    atoms = (Atoms)listBoxAtoms.SelectedItem;
                else
                    return;
                string str = "No.\tx\t y\t  z\r\n";
                for (int i = 0; i < atoms.atom.Count; i++)
                    str += (i + 1).ToString() + "\t" + Atoms.GetStringFromDouble(atoms.atom[i].X) + "\t " + Atoms.GetStringFromDouble(atoms.atom[i].Y) + "\t  " + Atoms.GetStringFromDouble(atoms.atom[i].Z) + "\r\n";


                this.toolTip.SetToolTip(this.listBoxAtoms, str); ;
            }
        }
        private void listBoxAtoms_MouseLeave(object sender, EventArgs e)
        {
            this.toolTip.SetToolTip(this.listBoxAtoms, "各原子の種類・位置・対称性などの情報を表示します。");
        }


        //タブコントロールのタブを設定
        private bool isFullTab = true;
        public bool IsFullTab
        {
            set
            {
                this.isFullTab = value;
                if (value == true)
                {
                    tabControl.TabPages.Clear();
                    tabControl.TabPages.Add(tabPageBasicInfo);
                    tabControl.TabPages.Add(tabPageAtom);
                    tabControl.TabPages.Add(tabPageAtomAdvanced);
                    tabControl.TabPages.Add(tabPageBondsPolyhedra);
                    tabControl.TabPages.Add(tabPageReference);
                }
                else
                {
                    tabControl.TabPages.Clear();
                    tabControl.TabPages.Add(tabPageBasicInfo);
                    tabControl.TabPages.Add(tabPageAtom);
                    tabControl.TabPages.Add(tabPageReference);
                }
            }
            get { return isFullTab; }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFullTab)
            {
                if (tabControl.SelectedIndex == 1)
                {
                    tabPageAtom.Controls.Add(listBox1);
                    tabPageAtom.Controls.Add(listBoxAtoms);
                    tabPageAtom.Controls.Add(buttonAddAtom);
                    tabPageAtom.Controls.Add(buttonDeleteAtom);
                    tabPageAtom.Controls.Add(buttonChangeAtom);
                }
                if (tabControl.SelectedIndex == 2)
                {
                    tabPageAtomAdvanced.Controls.Add(listBox1);
                    tabPageAtomAdvanced.Controls.Add(listBoxAtoms);
                    tabPageAtomAdvanced.Controls.Add(buttonAddAtom);
                    tabPageAtomAdvanced.Controls.Add(buttonDeleteAtom);
                    tabPageAtomAdvanced.Controls.Add(buttonChangeAtom);
                }
            }
        }


        private void pictureBoxAtomColor_Click(object sender, EventArgs e)
        {
            if (presentCrystal == null) return;
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = pictureBoxAtomColor.BackColor;
            colorDialog.AllowFullOpen = true; colorDialog.AnyColor = true; colorDialog.SolidColorOnly = false; colorDialog.ShowHelp = true;
            colorDialog.ShowDialog();
            pictureBoxAtomColor.BackColor = colorDialog.Color;
        }

        //編集内容を同種の元素にすべて適用する
        private void buttonChangeToSameElement_Click(object sender, EventArgs e)
        {
            buttonChangeAtom_Click(new object(), new EventArgs());
            if (listBoxAtoms.SelectedIndex > 0)
            {
                Atoms source = (Atoms)listBoxAtoms.SelectedItem;
                int selectedIndex = listBoxAtoms.SelectedIndex;
                for (int i = 0; i < listBoxAtoms.Items.Count; i++)
                {
                    if (i != listBoxAtoms.SelectedIndex)
                    {
                        Atoms a = (Atoms)listBoxAtoms.Items[i];
                        if (a.scatteringFactorNumber == source.scatteringFactorNumber)
                        {
                            a.radius = source.radius;
                            a.argb = source.argb;
                            a.diffusion = source.diffusion;
                            a.emission = source.emission;
                            a.specular = source.specular;
                            a.transparency = source.transparency;
                            a.shininess = source.shininess;
                            a.ambient = source.ambient;
                        }
                    }
                }
                SetCrystalFromForm();
                listBoxAtoms.SelectedIndex = selectedIndex;
            }
        }

        private void checkBoxShowPolyhedron_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxPolyhedron.Enabled = checkBoxShowPolyhedron.Checked;
        }

        private void checkBoxShowEdges_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxEdge.Enabled = checkBoxShowEdges.Checked;
        }


        private void listBoxBondsAndPolyhedra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxBondsAndPolyhedra.SelectedIndex < 0) return;
            Bonds b = (Bonds)listBoxBondsAndPolyhedra.SelectedItem;

            comboBoxBondingAtom1.Text = b.Element1;
            comboBoxBondingAtom2.Text = b.Element2;
            numericUpDownBondMinLength.Value = (decimal)b.MinLength;
            numericUpDownBondMaxLength.Value = (decimal)b.MaxLength;
            numericUpDownBondRadius.Value = (decimal)b.Radius;
            numericUpDownBondTrasparency.Value = (decimal)b.BondTransParency;
            pictureBoxBondColor.BackColor = Color.FromArgb(b.argbBond);
            numericUpDownPolyhedronPlaneAlpha.Value = (decimal)b.PolyhedronTransParency;

            checkBoxShowPolyhedron.Checked = b.ShowPolyhedron;
            checkBoxShowCenterAtom.Checked = b.ShowCenterAtom;
            checkBoxShowVertexAtoms.Checked = b.ShowVertexAtom;
            checkBoxShowInnerBonds.Checked = b.ShowInnerBonds;
            pictureBoxPolyhedronColor.BackColor = Color.FromArgb(b.argbPolyhedron);

            checkBoxShowEdges.Checked = b.ShowEdges;
            numericUpDownEdgeLineWidth.Value = (decimal)b.EdgeLineWidth;
            pictureBoxEdges.BackColor = Color.FromArgb(b.argbEdge);
        }

        private void buttonCrystallographicInformation_Click(object sender, EventArgs e)
        {
            if (formCrystallographicInformation.crystal == null)
                formCrystallographicInformation.crystal = presentCrystal;
            formCrystallographicInformation.Visible = !formCrystallographicInformation.Visible;
        }

        private void numericTextBox2_Load(object sender, EventArgs e)
        {

        }

        private void numericTextBoxGamma_Load(object sender, EventArgs e)
        {

        }

        private void numericTextBoxBeta_Load(object sender, EventArgs e)
        {

        }

        private void numericTextBoxY_Load(object sender, EventArgs e)
        {

        }

        private void radioButtonAnisotropy_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBoxB11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxB12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxB13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxB22_TextChanged(object sender, EventArgs e)
        {

        }






    }
}
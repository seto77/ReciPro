using System;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormSymmetryInformation : Form
    {
        public Crystal Crystal => CrystalControl.Crystal;
        public CrystalControl CrystalControl;

        public FormSymmetryInformation()
        {
            InitializeComponent();
        }

        private void crystalControl_CrystalChanged(object sender, EventArgs e)
        {
            ChangeCrystal();
        }

        private static void ConvertRichTextBox1(ref RichTextBox rTB)
        {
            ConvertRichTextBoxOffsetSub(ref rTB, "sub1", 12); ConvertRichTextBoxOffsetSub(ref rTB, "sub2", 12); ConvertRichTextBoxOffsetSub(ref rTB, "sub3", 12);
            ConvertRichTextBoxOffsetSub(ref rTB, "sub4", 12); ConvertRichTextBoxOffsetSub(ref rTB, "sub5", 12);

            ConvertRichTextBoxItalic(ref rTB, "P", 14); ConvertRichTextBoxItalic(ref rTB, "A", 14); ConvertRichTextBoxItalic(ref rTB, "B", 14);
            ConvertRichTextBoxItalic(ref rTB, "C", 14); ConvertRichTextBoxItalic(ref rTB, "F", 14); ConvertRichTextBoxItalic(ref rTB, "I", 14);
            ConvertRichTextBoxItalic(ref rTB, "a", 14); ConvertRichTextBoxItalic(ref rTB, "b", 14); ConvertRichTextBoxItalic(ref rTB, "c", 14);
            ConvertRichTextBoxItalic(ref rTB, "n", 14); ConvertRichTextBoxItalic(ref rTB, "d", 14); ConvertRichTextBoxItalic(ref rTB, "m", 14);
            ConvertRichTextBoxItalic(ref rTB, "u", 14); ConvertRichTextBoxItalic(ref rTB, "v", 14); ConvertRichTextBoxItalic(ref rTB, "w", 14);
            ConvertRichTextBoxItalic(ref rTB, "x", 14); ConvertRichTextBoxItalic(ref rTB, "y", 14); ConvertRichTextBoxItalic(ref rTB, "z", 14);
        }

        private static void ConvertRichTextBox2(ref RichTextBox rTB)
        {
            rTB.SelectAll();
            rTB.SelectionFont = new Font("Times New Roman", 12, FontStyle.Regular);
            rTB.SelectionCharOffset = -3;

            ConvertRichTextBoxItalic(ref rTB, "C", 12); ConvertRichTextBoxItalic(ref rTB, "D", 12); ConvertRichTextBoxItalic(ref rTB, "S", 12);
            ConvertRichTextBoxItalic(ref rTB, "T", 12); ConvertRichTextBoxItalic(ref rTB, "O", 12);

            ConvertRichTextBoxItalicSub(ref rTB, "i", 12); ConvertRichTextBoxItalicSub(ref rTB, "s", 12); ConvertRichTextBoxItalicSub(ref rTB, "h", 12);
            ConvertRichTextBoxItalicSub(ref rTB, "v", 12); ConvertRichTextBoxItalicSub(ref rTB, "d", 12);
            for (int n = 30; n > 0; n--)
            {
                string s = "^" + n.ToString();
                ConvertRichTextBoxOffsetSup(ref rTB, s, 12);
            }
        }

        private static void ConvertRichTextBox3(ref RichTextBox rTB)
        {
            rTB.SelectAll();
            rTB.SelectionFont = new Font("Times New Roman", 13, FontStyle.Regular);
            if (rTB.Text == "No Condition") return;
            ConvertRichTextBoxOffsetSub(ref rTB, "sub1", 10); ConvertRichTextBoxOffsetSub(ref rTB, "sub2", 10); ConvertRichTextBoxOffsetSub(ref rTB, "sub3", 10);
            ConvertRichTextBoxOffsetSub(ref rTB, "sub4", 10); ConvertRichTextBoxOffsetSub(ref rTB, "sub5", 10);

            ConvertRichTextBoxItalic(ref rTB, "A", 12); ConvertRichTextBoxItalic(ref rTB, "B", 12); ConvertRichTextBoxItalic(ref rTB, "C", 12);
            ConvertRichTextBoxItalic(ref rTB, "I", 12); ConvertRichTextBoxItalic(ref rTB, "F", 12); ConvertRichTextBoxItalic(ref rTB, "R", 12);
            ConvertRichTextBoxItalic(ref rTB, "h", 12); ConvertRichTextBoxItalic(ref rTB, "k", 12); ConvertRichTextBoxItalic(ref rTB, "l", 12);
            ConvertRichTextBoxItalic(ref rTB, "a", 12); ConvertRichTextBoxItalic(ref rTB, "b", 12); ConvertRichTextBoxItalic(ref rTB, "c", 12);
            ConvertRichTextBoxItalic(ref rTB, "d", 12); ConvertRichTextBoxItalic(ref rTB, " n", 12);
        }

        private static void ConvertRichTextBoxItalic(ref RichTextBox rTB, string s, int size)
        {
            int n = -1;
            while (rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase) > n)
            {
                rTB.SelectionCharOffset = 0;
                rTB.SelectionFont = new Font("Times New Roman", size, FontStyle.Italic);
                n = rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase);
            }
        }

        private static void ConvertRichTextBoxItalicSub(ref RichTextBox rTB, string s, int size)
        {
            int n = -1;
            while (rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase) > n)
            {
                rTB.SelectionCharOffset = -3;
                rTB.SelectionFont = new Font("Times New Roman", size, FontStyle.Italic);
                n = rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase);
            }
        }

        private static void ConvertRichTextBoxOffsetSub(ref RichTextBox rTB, string s, int size)
        {
            if (rTB.Find(s, 0, RichTextBoxFinds.MatchCase) > -1)
            {
                int n = -1;
                while (rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase) > n)
                {
                    rTB.SelectionFont = new Font("Times New Roman", size, FontStyle.Regular);
                    rTB.SelectionCharOffset = -3;
                    n = rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase);
                    rTB.Rtf = rTB.Rtf.Remove(rTB.Rtf.IndexOf(s), 3);
                }
            }
        }

        private static void ConvertRichTextBoxOffsetSup(ref RichTextBox rTB, string s, int size)
        {
            if (rTB.Find(s, 0, RichTextBoxFinds.MatchCase) > -1)
            {
                int n = -1;
                while (rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase) > n)
                {
                    rTB.SelectionFont = new Font("Times New Roman", size, FontStyle.Regular);
                    rTB.SelectionCharOffset = +3;
                    n = rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase);
                    rTB.Rtf = rTB.Rtf.Remove(rTB.Rtf.IndexOf("^"), 1);
                }
            }
        }

        private static void ConvertRichTextBoxReset(ref RichTextBox rTB)
        {
            rTB.SelectAll();
            rTB.SelectionFont = new Font("Times New Roman", 14, FontStyle.Regular);
        }

        private void FormCrystallographicInformation_Load(object sender, EventArgs e)
        {
            CrystalControl.CrystalChanged += new EventHandler(crystalControl_CrystalChanged);
            ChangeCrystal();
        }

        //åãèªÇïœçXÇ∑ÇÈ
        public void ChangeCrystal()
        {
            
            numericUpDown_ValueChanged(new object(), new EventArgs());
            SetWyckoffPosition();

            ConvertRichTextBoxReset(ref richTextBoxSG_HM);
            ConvertRichTextBoxReset(ref richTextBoxSG_HM_full);
            ConvertRichTextBoxReset(ref richTextBoxSG_SF);
            ConvertRichTextBoxReset(ref richTextBoxSG_Hall);
            ConvertRichTextBoxReset(ref richTextBoxPG_HM);
            ConvertRichTextBoxReset(ref richTextBoxPG_SF);
            ConvertRichTextBoxReset(ref richTextBoxLG);
            ConvertRichTextBoxReset(ref richTextBoxCS);
            ConvertRichTextBoxReset(ref richTextBoxExtinctionRule);

            richTextBoxSG_Num.Text = Crystal.Symmetry.SpaceGroupNumber.ToString() + ": " + Crystal.Symmetry.SpaceGroupSubNumber.ToString();
            richTextBoxSG_HM.Text = Crystal.Symmetry.SpaceGroupHMStr;
            richTextBoxSG_HM_full.Text = Crystal.Symmetry.SpaceGroupHMfullStr;
            richTextBoxSG_SF.Text = Crystal.Symmetry.SpaceGroupSFStr;
            richTextBoxSG_Hall.Text = Crystal.Symmetry.SpaceGroupHallStr;
            richTextBoxPG_HM.Text = Crystal.Symmetry.PointGroupHMStr;
            richTextBoxPG_SF.Text = Crystal.Symmetry.PointGroupSFStr;
            richTextBoxLG.Text = Crystal.Symmetry.LaueGroupStr;
            richTextBoxCS.Text = Crystal.Symmetry.CrystalSystemStr;
            richTextBoxExtinctionRule.Text = "";
            for (int n = 0; n < Crystal.Symmetry.ExtinctionRuleStr.Length; n++)
                richTextBoxExtinctionRule.Text += Crystal.Symmetry.ExtinctionRuleStr[n] + "\r\n";
            if (richTextBoxExtinctionRule.Text.Length == 0)
                richTextBoxExtinctionRule.Text = "No Condition";
            ConvertRichTextBox3(ref richTextBoxExtinctionRule);

            if (Crystal.Symmetry.SeriesNumber != 0)
            {
                ConvertRichTextBox1(ref richTextBoxSG_HM);
                ConvertRichTextBox1(ref richTextBoxSG_HM_full);
                ConvertRichTextBox2(ref richTextBoxSG_SF);
                ConvertRichTextBox1(ref richTextBoxSG_Hall);
                ConvertRichTextBox1(ref richTextBoxPG_HM);
                ConvertRichTextBox2(ref richTextBoxPG_SF);
                ConvertRichTextBox1(ref richTextBoxLG);

                richTextBoxCS.SelectAll();
                richTextBoxCS.SelectionFont = new Font("Times New Roman", 15, FontStyle.Regular);
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int h1 = (int)numericUpDownH1.Value;
            int k1 = (int)numericUpDownK1.Value;
            int l1 = (int)numericUpDownL1.Value;
            int h2 = (int)numericUpDownH2.Value;
            int k2 = (int)numericUpDownK2.Value;
            int l2 = (int)numericUpDownL2.Value;
            int u1 = (int)numericUpDownU1.Value;
            int v1 = (int)numericUpDownV1.Value;
            int w1 = (int)numericUpDownW1.Value;
            int u2 = (int)numericUpDownU2.Value;
            int v2 = (int)numericUpDownV2.Value;
            int w2 = (int)numericUpDownW2.Value;

            textBoxLengthPlane1.Text = (Crystal.GetLengthPlane(h1, k1, l1) * 10).ToString("f4");
            textBoxLengthPlane2.Text = (Crystal.GetLengthPlane(h2, k2, l2) * 10).ToString("f4");
            textBoxLengthAxis1.Text = (Crystal.GetLengthAxis(u1, v1, w1) * 10).ToString("f4");
            textBoxLengthAxis2.Text = (Crystal.GetLengthAxis(u2, v2, w2) * 10).ToString("f4");

            textBoxAnglePlanes.Text = (Crystal.GetAnglePlanes(h1, k1, l1, h2, k2, l2) * 180 / Math.PI).ToString("f4");
            textBoxAngleAxes.Text = (Crystal.GetAngleAxes(u1, v1, w1, u2, v2, w2) * 180 / Math.PI).ToString("f4");
            textBoxAnglePlaneAxis1.Text = (Crystal.GetAnglePlaneAxis(h1, k1, l1, u1, v1, w1) * 180 / Math.PI).ToString("f4");
            textBoxAnglePlaneAxis2.Text = (Crystal.GetAnglePlaneAxis(h2, k2, l2, u2, v2, w2) * 180 / Math.PI).ToString("f4");

            textBoxZoneAxis.Text = "[" + Crystal.GetZoneAxis(h1, k1, l1, h2, k2, l2) + " ]";
            textBoxZonePlane.Text = "(" + Crystal.GetZoneAxis(u1, v1, w1, u2, v2, w2) + " )";
        }

        private void SetWyckoffPosition()
        {
            dataSet.Tables[0].Clear();
            if (Crystal.Symmetry.LatticeTypeStr == "P")
                dataSet.Tables[0].Rows.Add(new object[] { "-", "-", "-", "(0,0,0)+", "", "", "" });
            else if (Crystal.Symmetry.LatticeTypeStr == "A")
                dataSet.Tables[0].Rows.Add(new object[] { "-", "-", "-", "(0,0,0)+", "(0,1/2,1/2)+", "", "" });
            else if (Crystal.Symmetry.LatticeTypeStr == "B")
                dataSet.Tables[0].Rows.Add(new object[] { "-", "-", "-", "(0,0,0)+", "(1/2,0,1/2)+", "", "" });
            else if (Crystal.Symmetry.LatticeTypeStr == "C")
                dataSet.Tables[0].Rows.Add(new object[] { "-", "-", "-", "(0,0,0)+", "(1/2,1/2,0)+", "", "" });
            else if (Crystal.Symmetry.LatticeTypeStr == "F")
                dataSet.Tables[0].Rows.Add(new object[] { "-", "-", "-", "(0,0,0)+", "(0,1/2,1/2)+", "(1/2,0,1/2)+", "(0,1/2,1/2)+" });
            else if (Crystal.Symmetry.LatticeTypeStr == "I")
                dataSet.Tables[0].Rows.Add(new object[] { "-", "-", "-", "(0,0,0)+", "(1/2,1/2,1/2)+", "", "" });
            else if (Crystal.Symmetry.LatticeTypeStr == "H")
                dataSet.Tables[0].Rows.Add(new object[] { "-", "-", "-", "(0,0,0)+", "(1/3,2/3,2/3)+", "(2/3,1/3,1/3)+", "" });

            Crystal.Symmetry = SymmetryStatic.Symmetries[Crystal.SymmetrySeriesNumber];

            for (int i = 0; i < SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber].Length; i++)
            {
                int len = SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].PositionStr.Length;
                for (int j = 0; j < len; j += 4)
                {
                    object[] o;
                    if (j == 0)
                    {
                        o = new object[] {
                               SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].Multiplicity,
                               SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].WyckoffLetter,
                                SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].SiteSymmetry,
                                j<len ? SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].PositionStr[j] : "",
                                j+1<len ? SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].PositionStr[j+1] : "",
                                j+2<len ? SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].PositionStr[j+2] : "",
                                j+3<len ? SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].PositionStr[j+3] : ""
                            };
                    }
                    else
                    {
                        o = new object[] {
                               "",
                              "",
                                "",
                                j<len ? SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].PositionStr[j] : "",
                                j+1<len ? SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].PositionStr[j+1] : "",
                                j+2<len ? SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].PositionStr[j+2] : "",
                                j+3<len ? SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber][i].PositionStr[j+3] : ""
                            };
                    }

                    dataSet.Tables[0].Rows.Add(o);
                }
            }
        }

        private void FormCrystallographicInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

    }
}
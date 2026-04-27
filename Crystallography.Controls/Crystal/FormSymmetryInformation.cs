using System;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class FormSymmetryInformation : FormBase
{
    public Crystal Crystal => CrystalControl.Crystal;
    public CrystalControl CrystalControl;

    public FormSymmetryInformation() => InitializeComponent(); // (260426Ch)

    private static void ConvertRichTextBox1(RichTextBox rTB)
    {
        foreach (var s in new[] { "sub1", "sub2", "sub3", "sub4", "sub5" }) // (260426Ch) 同種処理を配列ループに集約
            ConvertRichTextBoxOffsetSub(rTB, s, 12);
        foreach (var s in new[] { "P", "A", "B", "C", "F", "I", "a", "b", "c", "n", "d", "m", "u", "v", "w", "x", "y", "z" })
            ConvertRichTextBoxItalic(rTB, s, 14);
    }

    private static void ConvertRichTextBox2(RichTextBox rTB)
    {
        rTB.SelectAll();
        rTB.SelectionFont = new Font("Times New Roman", 12, FontStyle.Regular);
        rTB.SelectionCharOffset = -3;

        foreach (var s in new[] { "C", "D", "S", "T", "O" }) // (260426Ch)
            ConvertRichTextBoxItalic(rTB, s, 12);
        foreach (var s in new[] { "i", "s", "h", "v", "d" })
            ConvertRichTextBoxItalicSub(rTB, s, 12);
        for (int n = 30; n > 0; n--)
            ConvertRichTextBoxOffsetSup(rTB, $"^{n}", 12);
    }

    private static void ConvertRichTextBox3(RichTextBox rTB)
    {
        rTB.SelectAll();
        rTB.SelectionFont = new Font("Times New Roman", 13, FontStyle.Regular);
        if (rTB.Text == "No Condition") return;

        foreach (var s in new[] { "sub1", "sub2", "sub3", "sub4", "sub5" }) // (260426Ch)
            ConvertRichTextBoxOffsetSub(rTB, s, 10);
        foreach (var s in new[] { "A", "B", "C", "I", "F", "R", "h", "k", "l", "a", "b", "c", "d", " n" })
            ConvertRichTextBoxItalic(rTB, s, 12);
    }

    private static void ConvertRichTextBoxItalic(RichTextBox rTB, string s, int size)
    {
        int n = -1;
        while ((n = rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase)) >= 0) // (260426Ch) Find の二重呼び出しを削減
        {
            rTB.SelectionCharOffset = 0;
            rTB.SelectionFont = new Font("Times New Roman", size, FontStyle.Italic);
        }
    }

    private static void ConvertRichTextBoxItalicSub(RichTextBox rTB, string s, int size)
    {
        int n = -1;
        while ((n = rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase)) >= 0) // (260426Ch)
        {
            rTB.SelectionCharOffset = -3;
            rTB.SelectionFont = new Font("Times New Roman", size, FontStyle.Italic);
        }
    }

    private static void ConvertRichTextBoxOffsetSub(RichTextBox rTB, string s, int size)
    {
        int n = -1;
        while ((n = rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase)) >= 0) // (260426Ch) 事前 Find を省略
        {
            rTB.SelectionFont = new Font("Times New Roman", size, FontStyle.Regular);
            rTB.SelectionCharOffset = -3;
            rTB.Rtf = rTB.Rtf.Remove(rTB.Rtf.IndexOf(s), 3);
        }
    }

    private static void ConvertRichTextBoxOffsetSup(RichTextBox rTB, string s, int size)
    {
        int n = -1;
        while ((n = rTB.Find(s, n + 1, RichTextBoxFinds.MatchCase)) >= 0) // (260426Ch)
        {
            rTB.SelectionFont = new Font("Times New Roman", size, FontStyle.Regular);
            rTB.SelectionCharOffset = +3;
            rTB.Rtf = rTB.Rtf.Remove(rTB.Rtf.IndexOf('^'), 1);
        }
    }

    private static void ConvertRichTextBoxReset(RichTextBox rTB)
    {
        rTB.SelectAll();
        rTB.SelectionFont = new Font("Times New Roman", 14, FontStyle.Regular);
    }

    private void FormCrystallographicInformation_Load(object sender, EventArgs e)
    {
        CrystalControl.CrystalChanged += (_, _) => ChangeCrystal(); // (260426Ch) 1 行 handler をインライン化
        ChangeCrystal();
    }

    //結晶を変更する
    public void ChangeCrystal()
    {
        numericBox_ValueChanged(this, EventArgs.Empty); // (260426Ch) 不要な object/EventArgs 生成を避ける
        SetWyckoffPosition();

        foreach (var rtb in new[] { richTextBoxSG_HM, richTextBoxSG_HM_full, richTextBoxSG_SF, richTextBoxSG_Hall, richTextBoxPG_HM, richTextBoxPG_SF, richTextBoxLG, richTextBoxCS, richTextBoxExtinctionRule }) // (260426Ch)
            ConvertRichTextBoxReset(rtb);

        var symmetry = Crystal.Symmetry;
        richTextBoxSG_Num.Text = $"{symmetry.SpaceGroupNumber}: {symmetry.SpaceGroupSubNumber}";
        richTextBoxSG_HM.Text = symmetry.SpaceGroupHMStr;
        richTextBoxSG_HM_full.Text = symmetry.SpaceGroupHMfullStr;
        richTextBoxSG_SF.Text = symmetry.SpaceGroupSFStr;
        richTextBoxSG_Hall.Text = symmetry.SpaceGroupHallStr;
        richTextBoxPG_HM.Text = symmetry.PointGroupHMStr;
        richTextBoxPG_SF.Text = symmetry.PointGroupSFStr;
        richTextBoxLG.Text = symmetry.LaueGroupStr;
        richTextBoxCS.Text = symmetry.CrystalSystemStr;
        richTextBoxExtinctionRule.Text = symmetry.ExtinctionRuleStr.Length == 0 ? "No Condition" : string.Join("\r\n", symmetry.ExtinctionRuleStr) + "\r\n"; // (260426Ch) 文字列連結ループを置換
        ConvertRichTextBox3(richTextBoxExtinctionRule);

        if (symmetry.SeriesNumber != 0)
        {
            foreach (var rtb in new[] { richTextBoxSG_HM, richTextBoxSG_HM_full, richTextBoxSG_Hall, richTextBoxPG_HM, richTextBoxLG }) // (260426Ch)
                ConvertRichTextBox1(rtb);
            foreach (var rtb in new[] { richTextBoxSG_SF, richTextBoxPG_SF })
                ConvertRichTextBox2(rtb);

            richTextBoxCS.SelectAll();
            richTextBoxCS.SelectionFont = new Font("Times New Roman", 15, FontStyle.Regular);
        }
    }

    private void numericBox_ValueChanged(object sender, EventArgs e) // 260427Cl
    {
        var plane1 = (h: numericBoxH1.ValueInteger, k: numericBoxK1.ValueInteger, l: numericBoxL1.ValueInteger);
        var plane2 = (h: numericBoxH2.ValueInteger, k: numericBoxK2.ValueInteger, l: numericBoxL2.ValueInteger);
        var axis1 = (u: numericBoxU1.ValueInteger, v: numericBoxV1.ValueInteger, w: numericBoxW1.ValueInteger);
        var axis2 = (u: numericBoxU2.ValueInteger, v: numericBoxV2.ValueInteger, w: numericBoxW2.ValueInteger);

        textBoxLengthPlane1.Text = (Crystal.GetLengthPlane(plane1.h, plane1.k, plane1.l) * 10).ToString("f4");
        textBoxLengthPlane2.Text = (Crystal.GetLengthPlane(plane2.h, plane2.k, plane2.l) * 10).ToString("f4");
        textBoxLengthAxis1.Text = (Crystal.GetLengthAxis(axis1.u, axis1.v, axis1.w) * 10).ToString("f4");
        textBoxLengthAxis2.Text = (Crystal.GetLengthAxis(axis2.u, axis2.v, axis2.w) * 10).ToString("f4");

        textBoxAnglePlanes.Text = (Crystal.GetAnglePlanes(plane1.h, plane1.k, plane1.l, plane2.h, plane2.k, plane2.l) * 180 / Math.PI).ToString("f4");
        textBoxAngleAxes.Text = (Crystal.GetAngleAxes(axis1.u, axis1.v, axis1.w, axis2.u, axis2.v, axis2.w) * 180 / Math.PI).ToString("f4");
        textBoxAnglePlaneAxis1.Text = (Crystal.GetAnglePlaneAxis(plane1.h, plane1.k, plane1.l, axis1.u, axis1.v, axis1.w) * 180 / Math.PI).ToString("f4");
        textBoxAnglePlaneAxis2.Text = (Crystal.GetAnglePlaneAxis(plane2.h, plane2.k, plane2.l, axis2.u, axis2.v, axis2.w) * 180 / Math.PI).ToString("f4");

        textBoxZoneAxis.Text = $"[{Crystal.GetZoneAxis(plane1.h, plane1.k, plane1.l, plane2.h, plane2.k, plane2.l)} ]";
        textBoxZonePlane.Text = $"({Crystal.GetZoneAxis(axis1.u, axis1.v, axis1.w, axis2.u, axis2.v, axis2.w)} )";
    }

    private void SetWyckoffPosition()
    {
        var table = dataSet.Tables[0]; // (260426Ch)
        table.Clear();
        var centeringRow = Crystal.Symmetry.LatticeTypeStr switch
        {
            "P" => new object[] { "-", "-", "-", "(0,0,0)+", "", "", "" },
            "A" => ["-", "-", "-", "(0,0,0)+", "(0,1/2,1/2)+", "", ""],
            "B" => ["-", "-", "-", "(0,0,0)+", "(1/2,0,1/2)+", "", ""],
            "C" => ["-", "-", "-", "(0,0,0)+", "(1/2,1/2,0)+", "", ""],
            "F" => ["-", "-", "-", "(0,0,0)+", "(0,1/2,1/2)+", "(1/2,0,1/2)+", "(1/2,1/2,0)+"], // (260426Ch) 3 番目の F centering 座標 typo を修正
            "I" => ["-", "-", "-", "(0,0,0)+", "(1/2,1/2,1/2)+", "", ""],
            "H" => ["-", "-", "-", "(0,0,0)+", "(1/3,2/3,2/3)+", "(2/3,1/3,1/3)+", ""],
            _ => null
        };
        if (centeringRow != null)
            table.Rows.Add(centeringRow);

        Crystal.Symmetry = SymmetryStatic.Symmetries[Crystal.SymmetrySeriesNumber];

        foreach (var position in SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber])
        {
            var positions = position.PositionStr;
            int len = positions.Length;
            for (int j = 0; j < len; j += 4)
            {
                var row = new object[7];
                if (j == 0)
                {
                    row[0] = position.Multiplicity;
                    row[1] = position.WyckoffLetter;
                    row[2] = position.SiteSymmetry;
                }
                else
                {
                    row[0] = row[1] = row[2] = "";
                }
                for (int offset = 0; offset < 4; offset++)
                    row[3 + offset] = j + offset < len ? positions[j + offset] : "";

                table.Rows.Add(row);
            }
        }
    }

    private void FormCrystallographicInformation_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Visible = false; // (260426Ch)
    }

}

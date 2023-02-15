using System;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography;
public static class RichTextBoxSetting
{
    public enum Style
    {
        Regular,
        Italic,
        Bold,
        Sup,
        Sub
    }

    public static void Regular(ref RichTextBox rTB)
    {
        rTB.SelectionCharOffset = 0;
        rTB.SelectionFont = new Font(rTB.Font.FontFamily, rTB.Font.Size, FontStyle.Regular);
    }

    public static void Italic(ref RichTextBox rTB)
    {
        rTB.SelectionCharOffset = 0;
        rTB.SelectionFont = new Font(rTB.Font.FontFamily, rTB.Font.Size, FontStyle.Italic);
    }

    public static void Bold(ref RichTextBox rTB)
    {
        rTB.SelectionCharOffset = 0;
        rTB.SelectionFont = new Font(rTB.Font.FontFamily, rTB.Font.Size, FontStyle.Bold);
    }

    public static void Sub(ref RichTextBox rTB, FontStyle fontStyle)
    {
        rTB.SelectionFont = new Font(rTB.Font.FontFamily, Math.Max(rTB.Font.Size - 2, 1f), fontStyle);
        rTB.SelectionCharOffset = -3;
    }

    public static void Sub(ref RichTextBox rTB)
    {
        rTB.SelectionFont = new Font(rTB.Font.FontFamily, Math.Max(rTB.Font.Size - 2, 1f), FontStyle.Regular);
        rTB.SelectionCharOffset = -3;
    }

    public static void Sup(ref RichTextBox rTB, FontStyle fontStyle)
    {
        rTB.SelectionFont = new Font(rTB.Font.FontFamily, Math.Max(rTB.Font.Size - 2, 1f), fontStyle);
        rTB.SelectionCharOffset = +3;
    }

    public static void Sup(ref RichTextBox rTB)
    {
        rTB.SelectionFont = new Font(rTB.Font.FontFamily, Math.Max(rTB.Font.Size - 2, 1f), FontStyle.Regular);
        rTB.SelectionCharOffset = +3;
    }

    private static void ReplaceFormat(ref RichTextBox rTB, string[] s, Style style)
    {
        for (int i = 0; i < s.Length; i++)
        {
            if (rTB.Find(s[i], 0, RichTextBoxFinds.MatchCase) > -1)
            {
                int n = -1;
                while (rTB.Find(s[i], n + 1, RichTextBoxFinds.MatchCase) > n)
                {
                    rTB.SelectionFont = new Font(rTB.SelectionFont.FontFamily, rTB.SelectionFont.Size, FontStyle.Regular);
                    rTB.SelectionCharOffset = -3;
                    n = rTB.Find(s[i], n + 1, RichTextBoxFinds.MatchCase);
                    rTB.Rtf = rTB.Rtf.Remove(rTB.Rtf.IndexOf(s[i]), 3);
                }
            }
        }
    }
}
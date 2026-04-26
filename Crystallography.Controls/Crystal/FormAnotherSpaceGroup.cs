using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class FormAnotherSpaceGroup : CaptureFormBase
{
    private (int SeriesNum, string Notation)[] candidates;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public (int SeriesNum, string Notation)[] Candidates
    {
        set
        {
            candidates = value;
            listBox.Items.Clear();
            foreach (var c in value)
                listBox.Items.Add(c.Notation);
            listBox.SelectedIndex = 0;
        }
    }

    public int SeriesNum => candidates[listBox.SelectedIndex].SeriesNum;

    public FormAnotherSpaceGroup() => InitializeComponent();

    private void buttonOK_Click(object sender, EventArgs e) { DialogResult = DialogResult.OK; Close(); }
    private void buttonCancel_Click(object sender, EventArgs e) { DialogResult = DialogResult.Cancel; Close(); }

    #region 群の記号を斜体、上付き、下付などに整形する

    private static readonly Font fontSub = new("Times New Roman", 8f, FontStyle.Regular);
    private static readonly Font fontItalic = new("Times New Roman", 11f, FontStyle.Italic);
    private static readonly Font fontRegular = new("Times New Roman", 11f, FontStyle.Regular);

    private void listBoxSpaceGroup_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0) return;
        e.DrawBackground();
        string txt = ((ListBox)sender).Items[e.Index].ToString();

        float xPos = e.Bounds.Left;
        var brush = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? Brushes.White : Brushes.Black;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // 「:」を含む場合は空間群番号 + コロンまでを通常フォントで先に描く
        if (txt.Contains(':'))
        {
            var i = txt.IndexOf(':') + 1;
            e.Graphics.DrawString(txt[0..i], fontRegular, brush, xPos, e.Bounds.Y);
            xPos += e.Graphics.MeasureString(txt[0..i], fontRegular).Width - 2;
            txt = txt[i..];
        }

        while (txt.Length > 0)
        {
            if (txt.StartsWith(' ')) { /* skip whitespace */ }
            else if (txt.StartsWith("sub", StringComparison.Ordinal))
            {
                xPos -= 1;
                txt = txt[3..];
                e.Graphics.DrawString(txt[0].ToString(), fontSub, brush, xPos, e.Bounds.Y + 3);
                xPos += e.Graphics.MeasureString(txt[0].ToString(), fontSub).Width - 2;
            }
            else if (txt.StartsWith('-'))
            {
                float x = e.Graphics.MeasureString(txt[1].ToString(), fontRegular).Width;
                using var pen = new Pen(brush, 1);
                e.Graphics.DrawLine(pen, xPos + 2f, e.Bounds.Y + 1, x + xPos - 3f, e.Bounds.Y + 1);
            }
            else if (txt.StartsWith("Hex", StringComparison.Ordinal) || txt.StartsWith("Rho", StringComparison.Ordinal) || txt.StartsWith("(1)", StringComparison.Ordinal) || txt.StartsWith("(2)", StringComparison.Ordinal))
            {
                xPos += 2;
                e.Graphics.DrawString(txt[..3], fontSub, brush, xPos, e.Bounds.Y + 3);
                xPos += e.Graphics.MeasureString(txt[..3], fontSub).Width - 2;
                txt = txt[2..];
            }
            else if (txt[0] == '/')
            {
                xPos -= 1;
                e.Graphics.DrawString(txt[0].ToString(), fontRegular, brush, xPos, e.Bounds.Y);
                xPos += e.Graphics.MeasureString(txt[0].ToString(), fontRegular).Width - 5;
            }
            else
            {
                var font = char.IsDigit(txt[0]) || txt[0] == '(' || txt[0] == ')' ? fontRegular : fontItalic;
                e.Graphics.DrawString(txt[0].ToString(), font, brush, xPos, e.Bounds.Y);
                xPos += e.Graphics.MeasureString(txt[0].ToString(), font).Width - 2;
            }
            txt = txt[1..];
        }
    }
    #endregion
}

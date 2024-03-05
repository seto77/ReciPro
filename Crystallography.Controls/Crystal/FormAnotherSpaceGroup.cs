using IronPython.Runtime.Operations;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls;
public partial class FormAnotherSpaceGroup : Form
{

    private (int SeriesNum, string Notation)[] candidates;
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


    public FormAnotherSpaceGroup()
    {
        InitializeComponent();
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }


    #region 群の記号を斜体、上付き、下付などに整形する

    //下付き文字用フォント
    readonly Font fontSub = new("Times New Roman", 8f, FontStyle.Regular);
    //斜体
    readonly Font fontItalic = new("Times New Roman", 11f, FontStyle.Italic);
    //普通
    readonly Font fontRegular = new("Times New Roman", 11f, FontStyle.Regular);
    //太字
    readonly Font fontBold = new("Times New Roman", 10f, FontStyle.Bold);

    private void listBoxSpaceGroup_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0) return;
        e.DrawBackground();
        string txt = ((ListBox)sender).Items[e.Index].ToString();

        float xPos = e.Bounds.Left;
        Brush b;
        if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
            b = new SolidBrush(Color.Black);
        else
            b = new SolidBrush(Color.White);
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        //「:」が含まれている場合は、空間群番号を表すので、先に「:」までを処理する。
        if (txt.Contains(':'))
        {
            var i = txt.find(":") + 1;

            e.Graphics.DrawString(txt[0..i].ToString(), fontRegular, b, xPos, e.Bounds.Y);
            xPos += e.Graphics.MeasureString(txt[0..i].ToString(), fontRegular).Width - 2;
            txt = txt[i..];
        }

        while (txt.Length > 0)
        {
            if (txt.StartsWith(" ", StringComparison.Ordinal))
                xPos += 0;
            else if (txt.StartsWith("sub", StringComparison.Ordinal))//subで始まる時は
            {
                xPos -= 1;
                txt = txt[3..];
                e.Graphics.DrawString(txt[0].ToString(), fontSub, b, xPos, e.Bounds.Y + 3);
                xPos += e.Graphics.MeasureString(txt[0].ToString(), fontSub).Width - 2;
            }
            else if (txt.StartsWith("-", StringComparison.Ordinal))//-で始まる時は
            {
                float x = e.Graphics.MeasureString(txt[1].ToString(), fontRegular).Width;
                e.Graphics.DrawLine(new Pen(b, 1), new PointF(xPos + 2f, e.Bounds.Y + 1), new PointF(x + xPos - 3f, e.Bounds.Y + 1));
            }
            else if (txt.StartsWith("Hex", StringComparison.Ordinal) || txt.StartsWith("Rho", StringComparison.Ordinal) || txt.StartsWith("(1)", StringComparison.Ordinal) || txt.StartsWith("(2)", StringComparison.Ordinal))
            {
                xPos += 2;
                e.Graphics.DrawString(txt[..3], fontSub, b, xPos, e.Bounds.Y + 3);
                xPos += e.Graphics.MeasureString(txt[..3], fontSub).Width - 2;
                txt = txt[2..];
            }
            else if (txt[0] == '/')
            {
                xPos -= 1;
                e.Graphics.DrawString(txt[0].ToString(), fontRegular, b, xPos, e.Bounds.Y);
                xPos += e.Graphics.MeasureString(txt[0].ToString(), fontRegular).Width - 5;
            }
            else if (('0' <= txt[0] && '9' >= txt[0]) || txt[0] == '(' || txt[0] == ')')
            {
                e.Graphics.DrawString(txt[0].ToString(), fontRegular, b, xPos, e.Bounds.Y);
                xPos += e.Graphics.MeasureString(txt[0].ToString(), fontRegular).Width - 2;
            }
            else
            {
                e.Graphics.DrawString(txt[0].ToString(), fontItalic, b, xPos, e.Bounds.Y);
                xPos += e.Graphics.MeasureString(txt[0].ToString(), fontItalic).Width - 2;
            }
            txt = txt[1..];
        }

        b.Dispose();
    }
    #endregion
}

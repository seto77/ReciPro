using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class SymmetryControl : UserControlBase
{
    #region プロパティ、フィールド、イベントハンドラ
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SkipEvent { get; set; } = false;

    public int CrystalSystemIndex => comboBoxCrystalSystem.SelectedIndex;
    public int PointGroupIndex => comboBoxPointGroup.SelectedIndex;
    public int SpaceGroupIndex => comboBoxSpaceGroup.SelectedIndex;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int SymmetrySeriesNumber
    {
        get => (CrystalSystemIndex >= 0 && PointGroupIndex >= 0 && SpaceGroupIndex >= 0)
            ? SymmetryStatic.BelongingNumberOfSymmetry[CrystalSystemIndex][PointGroupIndex][SpaceGroupIndex] : 0;
        set
        {
            if (value < 0 || value > SymmetryStatic.TotalSpaceGroupNumber) return;
            var (cs, pg, sg) = SymmetryStatic.GetSystemAndGroupFromSeriesNumber(value);
            SkipEvent = true;
            SuspendLayout();

            comboBoxCrystalSystem.SelectedIndex = cs;
            comboBoxPointGroup.Items.Clear();
            comboBoxPointGroup.Items.AddRange(PointGroupArray(cs));
            comboBoxPointGroup.SelectedIndex = pg;
            comboBoxSpaceGroup.Items.Clear();
            comboBoxSpaceGroup.Items.AddRange(spaceGroupArray(cs, pg));
            comboBoxSpaceGroup.SelectedIndex = sg;

            SkipEvent = false;
            ResumeLayout();
            SetCellConstantsBySymmetry();
        }
    }

    /// <summary>長さの単位の get/set</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public LengthUnitEnum LengthUnit
    {
        get => radioButtonAngstrom.Checked ? LengthUnitEnum.Angstrom : LengthUnitEnum.NanoMeter;
        set
        {
            radioButtonAngstrom.Checked = value == LengthUnitEnum.Angstrom;
            radioButtonNanoMeter.Checked = value == LengthUnitEnum.NanoMeter;
        }
    }

    private double LengthScale => LengthUnit == LengthUnitEnum.NanoMeter ? 1.0 : 0.1; // 表示値 → nm

    /// <summary>Cell constants の get/set. 単位は nm, radian.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public (double A, double B, double C, double Alpha, double Beta, double Gamma) CellConstants
    {
        get => (numericBoxA.Value * LengthScale, numericBoxB.Value * LengthScale, numericBoxC.Value * LengthScale,
                numericBoxAlpha.RadianValue, numericBoxBeta.RadianValue, numericBoxGamma.RadianValue);
        set
        {
            SuspendLayout();
            SkipEvent = true;
            numericBoxA.Value = value.A / LengthScale;
            numericBoxB.Value = value.B / LengthScale;
            numericBoxC.Value = value.C / LengthScale;
            numericBoxAlpha.RadianValue = value.Alpha;
            numericBoxBeta.RadianValue = value.Beta;
            numericBoxGamma.RadianValue = value.Gamma;
            SkipEvent = false;
            ResumeLayout();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double A { get => numericBoxA.Value * LengthScale; set => numericBoxA.Value = value / LengthScale; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double B { get => numericBoxB.Value * LengthScale; set => numericBoxB.Value = value / LengthScale; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double C { get => numericBoxC.Value * LengthScale; set => numericBoxC.Value = value / LengthScale; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double Alpha { get => numericBoxAlpha.RadianValue; set => numericBoxAlpha.RadianValue = value; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double Beta { get => numericBoxBeta.RadianValue; set => numericBoxBeta.RadianValue = value; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double Gamma { get => numericBoxGamma.RadianValue; set => numericBoxGamma.RadianValue = value; }

    /// <summary>Cell constants error の get/set. 単位は nm, radian.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public (double AErr, double BErr, double CErr, double AlphaErr, double BetaErr, double GammaErr) CellConstantsErr
    {
        get => (numericBoxAErr.Value * LengthScale, numericBoxBErr.Value * LengthScale, numericBoxCErr.Value * LengthScale,
                numericBoxAlphaErr.RadianValue, numericBoxBetaErr.RadianValue, numericBoxGammaErr.RadianValue);
        set
        {
            SuspendLayout();
            numericBoxAErr.Value = value.AErr / LengthScale;
            numericBoxBErr.Value = value.BErr / LengthScale;
            numericBoxCErr.Value = value.CErr / LengthScale;
            numericBoxAlphaErr.RadianValue = value.AlphaErr;
            numericBoxBetaErr.RadianValue = value.BetaErr;
            numericBoxGammaErr.RadianValue = value.GammaErr;
            ResumeLayout();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowError
    {
        get => checkBoxShowError.Checked;
        set
        {
            SkipEvent = true;
            checkBoxShowError.Checked = value;
            SkipEvent = false;
            tableLayoutPanel1.ColumnStyles[2].Width = tableLayoutPanel1.ColumnStyles[6].Width = checkBoxShowError.Checked ? 25 : 0;
            numericBoxAErr.TabStop = numericBoxBErr.TabStop = numericBoxCErr.TabStop =
                numericBoxAlphaErr.TabStop = numericBoxBetaErr.TabStop = numericBoxGammaErr.TabStop = value;
        }
    }

    public event EventHandler ItemChanged;

    #endregion

    public SymmetryControl()
    {
        InitializeComponent();
        if (DesignMode) return; // (260322Ch) Designer 安定化のため InitializeComponent 後に打ち切る
        SymmetrySeriesNumber = 0;
        tableLayoutPanel1.ColumnStyles[2].Width = tableLayoutPanel1.ColumnStyles[6].Width = 0;
    }

    #region 空間群検索
    private void textBoxSearch_TextChanged(object sender, EventArgs e)
    {
        comboBoxSearchResult.Items.Clear();
        comboBoxSearchResult.Enabled = false;
        if (textBoxSearch.Text.Length == 0) return;

        var c = textBoxSearch.Text.Replace("sub", "_").ToCharArray(); // "sub" はアンダーバーに変換
        for (int n = 0; n < SymmetryStatic.TotalSpaceGroupNumber; n++)
        {
            var sym = SymmetryStatic.Symmetries[n];
            var hm = sym.SpaceGroupHMStr.Replace("sub", "_");
            int startIndex = -1;
            bool ok = true;
            foreach (var ch in c)
            {
                var index = hm.IndexOf(ch, startIndex + 1);
                if (index < 0) { ok = false; break; }
                startIndex = index;
            }
            if (ok)
                comboBoxSearchResult.Items.Add($"{sym.SpaceGroupNumber}:{sym.SpaceGroupHMStr}");
        }
        if (comboBoxSearchResult.Items.Count > 0)
            comboBoxSearchResult.Enabled = true;
    }

    private void comboBoxSearchResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        var target = comboBoxSearchResult.Text.Split(':')[1];
        var sym = Enumerable.Range(0, SymmetryStatic.TotalSpaceGroupNumber)
            .Select(n => SymmetryStatic.Symmetries[n])
            .First(s => s.SpaceGroupHMStr == target);

        comboBoxCrystalSystem.Text = sym.CrystalSystemStr;
        comboBoxPointGroup.Text = sym.PointGroupHMStr;
        comboBoxSpaceGroup.Text = $"{sym.SpaceGroupNumber}:{sym.SpaceGroupHMStr}";
    }
    #endregion

    #region 対称性コンボボックスのイベント

    private void comboBoxCrystalSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        SkipEvent = true;
        comboBoxPointGroup.Items.Clear();
        comboBoxSpaceGroup.Items.Clear();
        comboBoxPointGroup.Items.AddRange(PointGroupArray(CrystalSystemIndex));
        SkipEvent = false;
        comboBoxPointGroup.SelectedIndex = 0;
    }

    private static string[] PointGroupArray(int crystalSystemIndex) =>
        SymmetryStatic.BelongingNumberOfSymmetry[crystalSystemIndex]
            .Select(n => SymmetryStatic.Symmetries[n[0]].PointGroupHMStr).Distinct().ToArray();

    private void comboBoxPointGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        SkipEvent = true;
        comboBoxSpaceGroup.Items.Clear();
        comboBoxSpaceGroup.Items.AddRange(spaceGroupArray(CrystalSystemIndex, PointGroupIndex));
        SkipEvent = false;
        comboBoxSpaceGroup.SelectedIndex = 0;
    }

    private static string[] spaceGroupArray(int crystalSystemIndex, int pointGroupIndex) =>
        SymmetryStatic.BelongingNumberOfSymmetry[crystalSystemIndex][pointGroupIndex]
            .Select(n =>
            {
                var s = SymmetryStatic.Symmetries[n];
                return $"{s.SpaceGroupNumber}:{s.SpaceGroupHMStr}";
            }).ToArray();

    private void comboBoxSpaceGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        SetCellConstantsBySymmetry();
    }
    #endregion

    #region 空間群が変化したとき、あるいは格子定数が変化したとき呼ばれる. 対称性の制限下で格子定数を再設定
    public void SetCellConstantsBySymmetry()
    {
        if (SkipEvent) return;
        SkipEvent = true;
        SuspendLayout();

        var sym = SymmetryStatic.Symmetries[SymmetrySeriesNumber];
        switch (sym.CrystalSystemStr)
        {
            case "triclinic":
                numericBoxA.Enabled = numericBoxB.Enabled = numericBoxC.Enabled =
                    numericBoxAlpha.Enabled = numericBoxBeta.Enabled = numericBoxGamma.Enabled = true;
                break;

            case "monoclinic":
                numericBoxA.Enabled = numericBoxB.Enabled = numericBoxC.Enabled = true;
                switch (sym.MainAxis)
                {
                    case "a":
                        numericBoxAlpha.Enabled = true;
                        numericBoxBeta.Enabled = numericBoxGamma.Enabled = false;
                        numericBoxBeta.Value = numericBoxGamma.Value = 90;
                        numericBoxBetaErr.Value = numericBoxGammaErr.Value = 0;
                        break;
                    case "b":
                        numericBoxBeta.Enabled = true;
                        numericBoxAlpha.Enabled = numericBoxGamma.Enabled = false;
                        numericBoxAlpha.Value = numericBoxGamma.Value = 90;
                        numericBoxAlphaErr.Value = numericBoxGammaErr.Value = 0;
                        break;
                    case "c":
                        numericBoxGamma.Enabled = true;
                        numericBoxAlpha.Enabled = numericBoxBeta.Enabled = false;
                        numericBoxAlpha.Value = numericBoxBeta.Value = 90;
                        numericBoxAlphaErr.Value = numericBoxBetaErr.Value = 0;
                        break;
                }
                break;

            case "orthorhombic":
                numericBoxA.Enabled = numericBoxB.Enabled = numericBoxC.Enabled = true;
                numericBoxAlpha.Enabled = numericBoxBeta.Enabled = numericBoxGamma.Enabled = false;
                numericBoxAlpha.Value = numericBoxBeta.Value = numericBoxGamma.Value = 90;
                numericBoxAlphaErr.Value = numericBoxBetaErr.Value = numericBoxGammaErr.Value = 0;
                break;

            case "tetragonal":
            case "hexagonal":
                numericBoxA.Enabled = numericBoxC.Enabled = true;
                numericBoxB.Enabled = false;
                numericBoxB.Value = numericBoxA.Value;
                numericBoxBErr.Value = numericBoxAErr.Value;
                numericBoxAlpha.Enabled = numericBoxBeta.Enabled = numericBoxGamma.Enabled = false;
                numericBoxAlpha.Value = numericBoxBeta.Value = 90;
                numericBoxGamma.Value = sym.CrystalSystemStr == "hexagonal" ? 120 : 90;
                numericBoxAlphaErr.Value = numericBoxBetaErr.Value = numericBoxGammaErr.Value = 0;
                break;

            case "trigonal":
                bool rhombo = sym.SpaceGroupHMStr.Contains("Rho") && sym.SpaceGroupHMStr.Contains('R');
                if (!rhombo)
                {
                    numericBoxA.Enabled = numericBoxC.Enabled = true;
                    numericBoxB.Enabled = false;
                    numericBoxB.Value = numericBoxA.Value;
                    numericBoxBErr.Value = numericBoxAErr.Value;
                    numericBoxAlpha.Enabled = numericBoxBeta.Enabled = numericBoxGamma.Enabled = false;
                    numericBoxAlpha.Value = numericBoxBeta.Value = 90; numericBoxGamma.Value = 120;
                    numericBoxAlphaErr.Value = numericBoxBetaErr.Value = numericBoxGammaErr.Value = 0;
                }
                else
                {
                    numericBoxA.Enabled = true;
                    numericBoxB.Enabled = numericBoxC.Enabled = false;
                    numericBoxC.Value = numericBoxB.Value = numericBoxA.Value;
                    numericBoxCErr.Value = numericBoxBErr.Value = numericBoxAErr.Value;
                    numericBoxAlpha.Enabled = true;
                    numericBoxBeta.Enabled = numericBoxGamma.Enabled = false;
                    numericBoxGamma.Value = numericBoxBeta.Value = numericBoxAlpha.Value;
                }
                break;

            case "cubic":
                numericBoxA.Enabled = true;
                numericBoxB.Enabled = numericBoxC.Enabled = false;
                numericBoxC.Value = numericBoxB.Value = numericBoxA.Value;
                numericBoxCErr.Value = numericBoxBErr.Value = numericBoxAErr.Value;
                numericBoxAlpha.Enabled = numericBoxBeta.Enabled = numericBoxGamma.Enabled = false;
                numericBoxAlpha.Value = numericBoxBeta.Value = numericBoxGamma.Value = 90;
                numericBoxAlphaErr.Value = numericBoxBetaErr.Value = numericBoxGammaErr.Value = 0;
                break;
        }
        numericBoxAErr.Enabled = numericBoxA.Enabled;
        numericBoxBErr.Enabled = numericBoxB.Enabled;
        numericBoxCErr.Enabled = numericBoxC.Enabled;
        numericBoxAlphaErr.Enabled = numericBoxAlpha.Enabled;
        numericBoxBetaErr.Enabled = numericBoxBeta.Enabled;
        numericBoxGammaErr.Enabled = numericBoxGamma.Enabled;
        ResumeLayout();
        SkipEvent = false;
        ItemChanged?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    #region 群の記号を斜体・上付き・下付などに整形

    private static readonly Font fontSub = new("Times New Roman", 8f, FontStyle.Regular);
    private static readonly Font fontItalic = new("Times New Roman", 11f, FontStyle.Italic);
    private static readonly Font fontRegular = new("Times New Roman", 11f, FontStyle.Regular);

    private void comboBoxSpaceGroup_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0) return;
        e.DrawBackground();
        string txt = ((ComboBox)sender).Items[e.Index].ToString();

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
            if (txt.StartsWith(' ')) { /* skip */ }
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

    private void numericBoxCellConstants_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        if (sender is NumericBox nb && !nb.ReadOnly)
            SetCellConstantsBySymmetry();
    }

    private void checkBoxShowError_CheckedChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        ShowError = checkBoxShowError.Checked;
    }

    #region nm/Å の切り替え
    private void radioButtonNanoMeter_CheckedChanged(object sender, EventArgs e)
    {
        var (a, b, c, aErr, bErr, cErr) = (numericBoxA.Value, numericBoxB.Value, numericBoxC.Value, numericBoxAErr.Value, numericBoxBErr.Value, numericBoxCErr.Value);
        SkipEvent = true;
        // 切り替え後の単位が Angstrom なら nm→Å (×10), NanoMeter なら Å→nm (×0.1)
        var (factor, unitText) = LengthUnit == LengthUnitEnum.Angstrom ? (10.0, "Å") : (0.1, "nm");
        numericBoxA.Value = a * factor;
        numericBoxB.Value = b * factor;
        numericBoxC.Value = c * factor;
        numericBoxAErr.Value = aErr * factor;
        numericBoxBErr.Value = bErr * factor;
        numericBoxCErr.Value = cErr * factor;
        labelLengthUnitA.Text = labelLengthUnitB.Text = labelLengthUnitC.Text = unitText;
        SkipEvent = false;
    }
    #endregion
}

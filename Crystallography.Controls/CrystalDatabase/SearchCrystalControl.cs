using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class SearchCrystalControl : UserControl
    {
        public FormPeriodicTable formPeriodicTable;

        public SearchCrystalControl()
        {
            InitializeComponent();
            formPeriodicTable = new FormPeriodicTable();
        }
 
        private void checkBoxSearch_CheckedChanged(object sender, EventArgs e)
        {
            buttonPeriodicTable.Visible = checkBoxSearchElements.Checked;
            if (formPeriodicTable.Visible && !checkBoxSearchElements.Checked)
                formPeriodicTable.Visible = false;

            textBoxSearchRefference.Visible = checkBoxSearchRefference.Checked;
            textBoxSearchName.Visible = checkBoxSearchName.Checked;
            comboBoxSearchCrystalSystem.Visible = checkBoxSearchCrystalSystem.Checked;
            groupBoxCellParameter.Visible = checkBoxSearchCellParameter.Checked;
            groupBoxDspacing.Visible = checkBoxDspacing.Checked;
            groupBoxDensity.Visible = checkBoxDensity.Checked;
        }

        private void checkBoxD1_CheckedChanged(object sender, EventArgs e) => numericBoxD1.Enabled = numericBoxD1Err.Enabled = checkBoxD1.Checked;

        private void checkBoxD2_CheckedChanged(object sender, EventArgs e) => numericBoxD2.Enabled = numericBoxD2Err.Enabled = checkBoxD2.Checked;

        private void checkBoxD3_CheckedChanged(object sender, EventArgs e) => numericBoxD3.Enabled = numericBoxD3Err.Enabled = checkBoxD3.Checked;

        private void buttonPeriodicTable_Click(object sender, EventArgs e) => formPeriodicTable.Visible = true;

        public string Filter
        {
            get
            {
                var filter = new List<string>();

                //名前
                if (checkBoxSearchName.Checked && textBoxSearchName.Text != "")
                    filter.Add(string.Join(" AND ", textBoxSearchName.Text.Split().Select(s => $"Name LIKE '*{s}*'")));

                //Reference
                if (checkBoxSearchRefference.Checked && textBoxSearchRefference.Text != "")
                    filter.Add(string.Join(" AND ", textBoxSearchRefference.Text.Split().Select(s => $"(Authors LIKE '*{s}*' OR Title LIKE '*{s}*' OR Journal LIKE '*{s}*')")));

                if (checkBoxSearchCrystalSystem.Checked && comboBoxSearchCrystalSystem.SelectedIndex >= 0)
                    filter.Add($" CrystalSystem = '{comboBoxSearchCrystalSystem.Text}'");

                //元素のためのフィルター文字列
                if (checkBoxSearchElements.Checked && formPeriodicTable.IncludesStr.Length != 0)
                    filter.Add(string.Join(" AND ", formPeriodicTable.IncludesStr.Select(s => $"Elements Like '*{s}*'")));

                if (checkBoxSearchElements.Checked && formPeriodicTable.ExcludesStr.Length != 0)
                    filter.Add("NOT(" + string.Join(" OR ", formPeriodicTable.ExcludesStr.Select(s => $"Elements Like '*{s}*'")) + ")");//NOT句をつける

                //格子定数のフィルター
                if (checkBoxSearchCellParameter.Checked)
                {
                    var lenErr = numericBoxCellLengthErr.Value / 100;
                    var angErr = numericBoxCellAngleErr.Value / 100;
                    var func = new Func<string, double, double, string>((symbol, val, err) => val != 0 ? $"{symbol} >{val * (1 - err)} AND {symbol} < {val * (1 + err)}" : "");

                    filter.Add(func("A", numericBoxCellA.Value, lenErr));
                    filter.Add(func("B", numericBoxCellB.Value, lenErr));
                    filter.Add(func("C", numericBoxCellC.Value, lenErr));
                    filter.Add(func("Alpha", numericBoxCellAlpha.Value, angErr));
                    filter.Add(func("Beta", numericBoxCellBeta.Value, angErr));
                    filter.Add(func("Gamma", numericBoxCellGamma.Value, angErr));
                }

                if (checkBoxDspacing.Checked)
                {
                    var func = new Func<double, double, string>((val, err) =>
                    {
                        if (val == 0) return "";
                        var temp = new List<string>();
                        for (int j = 1; j < 9; j++)
                            temp.Add($"( D{j} > {val * (1 - err)} AND D{j} < {val * (1 + err)} )");
                        return string.Join(" OR ", temp);
                    });

                    if (checkBoxD1.Checked)
                        filter.Add(func(numericBoxD1.Value, numericBoxD1Err.Value / 100));
                    if (checkBoxD2.Checked)
                        filter.Add(func(numericBoxD2.Value, numericBoxD2Err.Value / 100));
                    if (checkBoxD3.Checked)
                        filter.Add(func(numericBoxD3.Value, numericBoxD3Err.Value / 100));
                }

                //格子定数のフィルター
                if (checkBoxDensity.Checked && numericBoxDensity.Value != 0)
                {
                    double val = numericBoxDensity.Value, err = numericBoxDensityErr.Value / 100;
                    filter.Add($"Density >{val * (1 - err)} AND Density < {val * (1 + err)}");
                }

                return string.Join(" AND ", filter.Where(f => f != "").Select(f => "(" + f + ")"));

            }
        }

    }
}

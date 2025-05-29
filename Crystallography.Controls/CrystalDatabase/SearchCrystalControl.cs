using MathNet.Numerics.Statistics.Mcmc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Community.CsharpSqlite.Sqlite3;

namespace Crystallography.Controls;
public partial class SearchCrystalControl : UserControl
{
    #region フィールド、プロパティ、イベント

    private CrystalDatabaseControl crystalDatabaseControl;
    public CrystalDatabaseControl CrystalDatabaseControl
    {
        get => crystalDatabaseControl;
        set
        {
            if (value != null)
            {
                crystalDatabaseControl = value;
                crystalDatabaseControl.DataBaseChanged += CrystalDatabaseControl_DataBaseChanged;
            }
        }
    }

    private void CrystalDatabaseControl_DataBaseChanged(object sender, EventArgs e)
    {
        buttonSearch_Click(sender, e);
    }

    public delegate void ProgressChangedEventHandler(object sender, double progress, string message);
    public event ProgressChangedEventHandler ProgressChanged;

    public FormPeriodicTable formPeriodicTable;

    #endregion

    #region コンストラクタ
    public SearchCrystalControl()
    {
        InitializeComponent();
        this.Load += SearchCrystalControl_Load;
    }

    private void SearchCrystalControl_Load(object sender, EventArgs e)
    {
        formPeriodicTable = new FormPeriodicTable();
    }
    #endregion

    #region チェックボックス
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


    bool firstTime = true;
    private void buttonPeriodicTable_Click(object sender, EventArgs e)
    {
        if (firstTime)
        {
            var parent = this.Parent;
            while (parent is not Form && parent != null)
                parent = parent.Parent;
            if (parent == null)
                return;
            var form = parent as Form;
            formPeriodicTable.Owner = form;

            formPeriodicTable.Location = formPeriodicTable.Owner.PointToScreen(new System.Drawing.Point(100, 100));
            firstTime = false;
        }
        formPeriodicTable.Visible = true;
        formPeriodicTable.BringToFront();
    }

    #endregion

    readonly Stopwatch sw = new();
    bool[] flags = [];
    private void buttonSearch_Click(object sender, EventArgs e)
    {
        if (CrystalDatabaseControl == null || backgroundWorkerSearch.IsBusy || CrystalDatabaseControl.Table.Count == 0)
            return;
        sw.Restart();
        Enabled = false;
        flags = new bool[CrystalDatabaseControl.Table.Count];
        CrystalDatabaseControl.Suspend();//バインディングを切る

        backgroundWorkerSearch.RunWorkerAsync(
            new SearchParameters(
            name: checkBoxSearchName.Checked ? textBoxSearchName.Text : "",
            reference: checkBoxSearchRefference.Checked ? textBoxSearchRefference.Text : "",
            system: checkBoxSearchCrystalSystem.Checked ? comboBoxSearchCrystalSystem.SelectedIndex : -1,
            includes: formPeriodicTable.Includes,
            excludes: formPeriodicTable.Excludes,
            lengthErr: numericBoxCellLengthErr.Value / 100,
            angleErr: numericBoxCellAngleErr.Value,
            a: numericBoxCellA.Value, b: numericBoxCellB.Value, c: numericBoxCellC.Value,
            alpha: numericBoxCellAlpha.Value, beta: numericBoxCellBeta.Value, gamma: numericBoxCellGamma.Value,
            density: checkBoxDensity.Checked ? numericBoxDensity.Value : 0,
            densityErr: numericBoxDensityErr.Value / 100,
            d1: checkBoxD1.Checked ? numericBoxD1.Value / 10 : 0,
            d2: checkBoxD2.Checked ? numericBoxD2.Value / 10 : 0,
            d3: checkBoxD3.Checked ? numericBoxD3.Value / 10 : 0,
            d1Err: numericBoxD1Err.Value / 100, d2Err: numericBoxD2Err.Value / 100, d3Err: numericBoxD3Err.Value / 100
            )
            );
    }

    public struct SearchParameters(string name, string reference, int system, byte[] includes, byte[] excludes,
        double lengthErr, double angleErr,
        double a, double b, double c, double alpha, double beta, double gamma,
        double density, double densityErr,
        double d1, double d2, double d3, double d1Err, double d2Err, double d3Err)
    {
        public string Name = name;
        public string Reference = reference;
        public int System = system;
        public byte[] Includes = includes, Excludes = excludes;
        public double LengthErr = lengthErr, AngleErr = angleErr;
        public double A = a, B = b, C = c, Alpha = alpha, Beta = beta, Gamma = gamma;
        public double density = density, densityErr = densityErr;
        public double D1 = d1, D2 = d2, D3 = d3, D1Err = d1Err, D2Err = d2Err, D3Err = d3Err;
    }

    private void backgroundWorkerSearch_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
        var prm = (SearchParameters)e.Argument;
        var table = CrystalDatabaseControl.Table;
        var dMin = double.MaxValue;
        if (prm.D1 != 0) dMin = Math.Min(dMin, prm.D1 * (1 - 2 * prm.D1Err));
        if (prm.D2 != 0) dMin = Math.Min(dMin, prm.D2 * (1 - 2 * prm.D3Err));
        if (prm.D3 != 0) dMin = Math.Min(dMin, prm.D3 * (1 - 2 * prm.D3Err));

        var ignoreScatteringFactor = checkBoxIgnoreScatteringFactor.Checked;

        bool amcsd_checked = CrystalDatabaseControl.AMCSD_Checked, cod_checked = CrystalDatabaseControl.COD_Checked;

        long time = 0;
        int count = 0;
        Parallel.For(0, table.Count, i =>
        {
            var flag = true;

            var datatype = table.GetDataType(i);

            if ((datatype == (byte)Crystal2.DataType.AMCSD && !amcsd_checked) || (datatype == (byte)Crystal2.DataType.COD && !cod_checked))
                flag = false;

            if (flag)
            {
                var cry = table.Get(i);

                //名前
                if (flag && prm.Name != "" && !cry.name.Contains(prm.Name, StringComparison.CurrentCultureIgnoreCase))
                    flag = false;

                //Reference
                if (flag && prm.Reference != "" && !(cry.jour.Contains(prm.Reference, StringComparison.CurrentCultureIgnoreCase) ||
                cry.auth.Contains(prm.Reference, StringComparison.CurrentCultureIgnoreCase) ||
                cry.sect.Contains(prm.Reference, StringComparison.CurrentCultureIgnoreCase)))
                    flag = false;

                //結晶系
                if (flag && prm.System > 0 && prm.System != SymmetryStatic.NumArray[cry.sym][5])
                    flag = false;

                //元素
                if (flag && checkBoxSearchElements.Checked)
                {
                    var elements = cry.atoms.Select(a => a.AtomNo).Distinct().ToArray();
                    if (prm.Excludes.Length != 0 && elements.Any(e => prm.Excludes.Contains(e)))
                        flag = false;
                    if (flag && prm.Includes.Length != 0 && !prm.Includes.All(e => elements.Contains(e)))
                        flag = false;
                    if (flag && cry.atoms.Count == 0)
                        flag = false;
                }

                //格子定数
                if (flag && checkBoxSearchCellParameter.Checked)
                {
                    var Values = cry.CellOnlyValue_nm_radian;
                    if (!double.IsNaN(Values.A))
                    {
                        if (prm.A != 0 && (prm.A * (1 - prm.LengthErr) > Values.A || prm.A * (1 + prm.LengthErr) < Values.A))
                            flag = false;
                        if (flag && prm.B != 0 && (prm.B * (1 - prm.LengthErr) > Values.B || prm.B * (1 + prm.LengthErr) < Values.B))
                            flag = false;
                        if (flag && prm.C != 0 && (prm.C * (1 - prm.LengthErr) > Values.C || prm.C * (1 + prm.LengthErr) < Values.C))
                            flag = false;
                        if (flag && prm.Alpha != 0 && (prm.Alpha - prm.AngleErr > Values.Alpha || prm.Alpha + prm.AngleErr < Values.Alpha))
                            flag = false;
                        if (flag && prm.Beta != 0 && (prm.Beta - prm.AngleErr > Values.Beta || prm.Beta + prm.AngleErr < Values.Beta))
                            flag = false;
                        if (flag && prm.Gamma != 0 && (prm.Gamma - prm.AngleErr > Values.Gamma || prm.Gamma + prm.AngleErr < Values.Gamma))
                            flag = false;
                    }
                }

                //密度のフィルター
                if (flag && prm.density != 0 && (prm.density * (1 - prm.densityErr) > cry.density || prm.density * (1 + prm.densityErr) < cry.density))
                    flag = false;

                //d値のフィルター
                if (flag && checkBoxDspacing.Checked)
                {
                    var dArray = cry.d;
                    if (ignoreScatteringFactor)
                    {
                        var Values = cry.CellOnlyValue_nm_radian;
                        if (!double.IsNaN(Values.A))
                            dArray = calcDlist(Values.A, Values.B, Values.C, Values.Alpha, Values.Beta, Values.Gamma, dMin);
                    }

                    if (flag && prm.D1 != 0 && !dArray.Any(d => prm.D1 * (1 - prm.D1Err) < d && prm.D1 * (1 + prm.D1Err) > d))
                        flag = false;
                    if (flag && prm.D2 != 0 && !dArray.Any(d => prm.D2 * (1 - prm.D2Err) < d && prm.D2 * (1 + prm.D2Err) > d))
                        flag = false;
                    if (flag && prm.D3 != 0 && !dArray.Any(d => prm.D3 * (1 - prm.D3Err) < d && prm.D3 * (1 + prm.D3Err) > d))
                        flag = false;
                }
            }
            flags[i] = flag;

            if (Interlocked.Increment(ref count) % 1000 == 0 && sw.ElapsedMilliseconds - time > 100)
            {
                time = sw.ElapsedMilliseconds;
                backgroundWorkerSearch.ReportProgress(count);
            }
        }
        );
    }


    static int composeKey(in int h, in int k, in int l) => ((h > 0) || (h == 0 && k > 0) || (h == 0 && k == 0 && l > 0)) ? ((h + 255) << 20) + ((k + 255) << 10) + l + 255 : -1;
    static (int h, int k, int l) decomposeKey(in int key) => (((key << 2) >> 22) - 255, ((key << 12) >> 22) - 255, ((key << 22) >> 22) - 255);

    static readonly int zeroKey = (255 << 20) + (255 << 10) + 255;
    static readonly (int h, int k, int l)[] directions = new[] { (1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };//(-1, 0, 0)は除いておく
    static float[] calcDlist(double a, double b, double c, double alpha, double beta, double gamma, double dMin)
    {
        double SinAlfa = Math.Sin(alpha), CosAlfa = Math.Cos(alpha), CosBeta = Math.Cos(beta), CosGamma = Math.Cos(gamma);
        Vector3DBase cAxis = new(0, 0, c);
        Vector3DBase bAxis = new(0, b * SinAlfa, b * CosAlfa);
        Vector3DBase aAxis = new(
        a * Math.Sqrt(1 - CosBeta * CosBeta - (CosGamma - CosAlfa * CosBeta) * (CosGamma - CosAlfa * CosBeta) / SinAlfa / SinAlfa),
        a * (CosGamma - CosAlfa * CosBeta) / SinAlfa,
        a * CosBeta);

        var MatrixInverse = new Matrix3D(aAxis, bAxis, cAxis).Inverse();
        double aX = MatrixInverse.E11, aY = MatrixInverse.E12, aZ = MatrixInverse.E13;
        double bX = MatrixInverse.E21, bY = MatrixInverse.E22, bZ = MatrixInverse.E23;
        double cX = MatrixInverse.E31, cY = MatrixInverse.E32, cZ = MatrixInverse.E33;

        var gMax = 1 / dMin;
        var shift = directions.Select(dir => (MatrixInverse * dir).Length).Max();
        var maxGnum = 8000;
        var outer = new List<(int key, double len)>() { (zeroKey, 0) };
        var gKeys = new HashSet<int>((int)(maxGnum * 1.5)) { zeroKey };
        var gList = new HashSet<double>((int)(maxGnum * 1.5));
        var minG = 0.0;

        while (gList.Count < maxGnum && (minG = outer.Min(o => o.len)) < gMax)
        {
            var end = outer.FindLastIndex(o => o.len - minG < shift * 2);
            foreach (var (key1, _) in CollectionsMarshal.AsSpan(outer)[..(end + 1)])
            {
                var (h1, k1, l1) = decomposeKey(key1);
                foreach ((int h2, int k2, int l2) in directions)
                {
                    int h = h1 + h2, k = k1 + k2, l = l1 + l2, key2 = composeKey(h, k, l);
                    if (key2 > 0 && gKeys.Add(key2))
                    {
                        double x = h * aX + k * bX + l * cX, y = h * aY + k * bY + l * cY, z = h * aZ + k * bZ + l * cZ;
                        var len = Math.Sqrt(x * x + y * y + z * z);
                        gList.Add(len);
                        outer.Add((key2, len));
                    }
                }
            }
            outer.RemoveRange(0, end + 1);
            outer.Sort((e1, e2) => e1.len.CompareTo(e2.len));
        }
        return gList.Select(g => (float)(1 / g)).ToArray();
    }


    bool skipProgressEvent = false;
    private void backgroundWorkerSearch_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
    {
        if (skipProgressEvent) return;
        try
        {
            skipProgressEvent = true;
            ProgressChanged?.Invoke(sender, (double)e.ProgressPercentage / CrystalDatabaseControl.Table.Count, $"Searching... {sw.ElapsedMilliseconds / 1000.0:f2} msec.");
        }
        catch { }
        finally { skipProgressEvent = false; }
    }

    private void backgroundWorkerSearch_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
        //最終処理
        var flagCount = flags.Count(f => f);

        if (flagCount == CrystalDatabaseControl.Table.Count)
            CrystalDatabaseControl.SearchFilter = "";
        else
        {
            for (int i = 0; i < CrystalDatabaseControl.Table.Count; i++)
                if (CrystalDatabaseControl.Table.GetFlag(i) != flags[i])
                    CrystalDatabaseControl.Table.SetFlag(i, flags[i]);

            CrystalDatabaseControl.SearchFilter = "Flag = true";
        }

        //if (flagCount == 0)
        //    CrystalDatabaseControl.Suspend();
        //else
        CrystalDatabaseControl.Resume();//バインディングを繋げる


        this.Enabled = true;

        Thread.Sleep(100);
        ProgressChanged?.Invoke(sender, 1.0, $"Completion of search. {sw.ElapsedMilliseconds / 1000.0:f2} msec.");
        Application.DoEvents();


    }
}

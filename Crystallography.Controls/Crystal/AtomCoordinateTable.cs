using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class AtomCoordinateTable : UserControlBase
{
    private readonly ReaderWriterLockSlim rwLock = new();
    private bool skipEvent { get; set; } = false;

    public AtomCoordinateTable() => InitializeComponent();

    private Crystal crystal;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Crystal Crystal
    {
        get => crystal;
        set
        {
            crystal = value;
            if (crystal?.Atoms == null || crystal.Atoms.Length == 0) return;
            skipEvent = true;
            comboBox.Items.Clear();
            foreach (var a in crystal.Atoms)
                comboBox.Items.Add(a.Label);
            skipEvent = false;
            comboBox.SelectedIndex = 0;
        }
    }

    private Atoms atom;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Atoms Atom
    {
        get => atom;
        set
        {
            atom = value;
            if (atom == null) return;
            for (int i = 0; i < crystal.Atoms.Length; i++)
                if (crystal.Atoms[i] == atom)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
        }
    }

    private bool HasAtomsAndValidIndex =>
        crystal?.Atoms != null && crystal.Atoms.Length > 0 && comboBox.SelectedIndex < crystal.Atoms.Length;

    private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (skipEvent) return;
        if (!HasAtomsAndValidIndex) return;
        atom = Crystal.Atoms[comboBox.SelectedIndex];
        RefreshTable();
    }

    private void RefreshTable()
    {
        if (!HasAtomsAndValidIndex) return;
        var atoms = Search(Crystal, Atom, (double)numericUpDownMaxLength.Value);
        dataSet.Tables[0].Clear();
        foreach (var (label, distance) in atoms)
            dataSet.Tables[0].Rows.Add([label, distance]);
        DrawGraph(atoms);
    }

    public List<(string Label, double Distance)> Search(Crystal crystal, Atoms targetAtom, double maxLengthAngstrom)
    {
        var mat = 10 * crystal.MatrixReal;
        Vector3DBase pos = mat * targetAtom.Atom[0];
        var max2 = maxLengthAngstrom * maxLengthAngstrom;
        var atoms = new List<(string Label, double Distance)>();
        // 隣接単位格子の原子位置を全探索しリストへ蓄積
        for (int max = 0; max < 8; max++)
        {
            bool flag = false;
            Parallel.For(-max, max + 1, xShift =>
            {
                for (int yShift = -max; yShift <= max; yShift++)
                    for (int zShift = -max; zShift <= max; zShift++)
                    {
                        if (Math.Abs(xShift) != max && Math.Abs(yShift) != max && Math.Abs(zShift) != max)
                            continue;
                        foreach (var atm in crystal.Atoms)
                            foreach (var v in atm.Atom)
                            {
                                var tempPos = mat * (v + new Vector3DBase(xShift, yShift, zShift));
                                if (max2 > (tempPos - pos).Length2)
                                {
                                    rwLock.EnterWriteLock();
                                    try
                                    {
                                        atoms.Add((atm.Label, (tempPos - pos).Length));
                                        flag = true; // 1個でも見つかれば外側ループ続行
                                    }
                                    finally { rwLock.ExitWriteLock(); }
                                }
                            }
                    }
            });
            if (!flag && max > 2) break;
        }
        atoms.Sort((a1, a2) => a1.Distance.CompareTo(a2.Distance));
        return atoms;
    }

    private Bitmap bmp;
    private Graphics g;
    private Point OriginPos = new(30, 30);
    private double UpperX, LowerX, UpperY, LowerY;
    private readonly Profile profile = new();
    private double BottomMargin = 0;

    private void DrawGraph(List<(string Label, double Distance)> atoms)
    {
        if (pictureBox.Width <= 0 || pictureBox.Height <= 0 || atoms.Count == 0) return;
        // 前回の bmp/g を破棄してリーク防止 (pictureBox.Image にも握られているため Image を先に外す)
        pictureBox.Image = null;
        bmp?.Dispose();
        g?.Dispose();
        bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
        g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        DoubleBuffered = true;

        double width = (double)numericUpDownWidth.Value;
        LowerX = 0;
        UpperX = atoms[^1].Distance + width * 2;
        LowerY = 0;

        // 各原子の存在範囲 [d-w, d+w] を始点(true)/終点(false) として並べる
        var controlPoints = new List<ControlPoint>(atoms.Count * 2);
        foreach (var a in atoms)
        {
            controlPoints.Add(new(a.Distance - width, true));
            controlPoints.Add(new(a.Distance + width, false));
        }
        controlPoints.Sort();

        double height = 0;
        UpperY = 2;
        profile.Clear();
        profile.Pt.Add(new PointD(-width * 2, 0));
        foreach (var cp in controlPoints)
        {
            profile.Pt.Add(new PointD(cp.X, height));
            height += cp.Flag ? 1 : -1;
            profile.Pt.Add(new PointD(cp.X, height));
            if (UpperY < height) UpperY = height;
        }
        profile.Pt.Add(new PointD(UpperX, 0));
        UpperY += 2;

        DrawHistogram();
        DrawGraduation();
        DrawLabel(atoms);

        pictureBox.Image = bmp;
    }

    private class ControlPoint(double x, bool flag) : IComparable
    {
        public double X = x;
        public bool Flag = flag;
        public int CompareTo(object obj) => X.CompareTo(((ControlPoint)obj).X);
    }

    private void DrawHistogram()
    {
        var zero = ConvToPicBoxCoord(0, 0).Y;
        for (int i = 0; i < profile.Pt.Count - 1; i++)
        {
            var p1 = ConvToPicBoxCoord(profile.Pt[i]);
            var p2 = ConvToPicBoxCoord(profile.Pt[i + 1]);
            if (Math.Abs(p1.X - p2.X) > 0.2)
                g.FillRectangle(Brushes.LawnGreen, new RectangleF(p1.X, p1.Y, p2.X - p1.X, zero - p1.Y));
        }
    }

    private void DrawLabel(List<(string Label, double Distance)> atoms)
    {
        var prevPt = new PointF(-10, -10);
        int times = 1;
        const int shiftY = 15;
        using var font = new Font("Tahoma", 9);
        for (int i = 0; i < atoms.Count; i++)
        {
            // 同じラベルかつほぼ同じ距離の原子は (n) でまとめる
            while (i < atoms.Count - 1 && Math.Abs(atoms[i].Distance - atoms[i + 1].Distance) < 1e-11 && atoms[i].Label == atoms[i + 1].Label)
            {
                times++;
                i++;
            }
            PointF pt = ConvToPicBoxCoord(atoms[i].Distance, 0);

            // 字がかぶらないように Y を調整
            pt.Y = pt.X - prevPt.X < 40 && prevPt.Y - shiftY > 0 ? prevPt.Y - shiftY : pt.Y - shiftY * 2;
            prevPt = pt;

            var label = atoms[i].Label.TrimEnd(' ');
            var text = times == 1 ? label : $"{label}({times})";
            g.DrawString(text, font, Brushes.Red, pt);
            times = 1;
        }
    }

    private static float ChooseGradiation(double range, ReadOnlySpan<double> thresholds)
    {
        int log = (int)Math.Log10(range);
        double d = range / Math.Pow(10, log);
        double basePow = Math.Pow(10, log - 1);
        if (d < thresholds[0]) return (float)(basePow);
        if (d < thresholds[1]) return (float)(2 * basePow);
        if (d < thresholds[2]) return (float)(5 * basePow);
        return (float)(10 * basePow);
    }

    private void DrawGraduation()
    {
        g.FillRectangle(Brushes.White, 0, 0, OriginPos.X, pictureBox.Height);
        g.FillRectangle(Brushes.White, 0, pictureBox.Height - OriginPos.Y, pictureBox.Width, pictureBox.Height);

        // 角度方向の目盛り
        float angleGrad = ChooseGradiation(UpperX - LowerX, [1.1, 2.2, 5.0]);
        g.DrawLine(Pens.Black, OriginPos.X, pictureBox.Height - OriginPos.Y, pictureBox.Width, pictureBox.Height - OriginPos.Y);
        using var strFont = new Font(new FontFamily("tahoma"), 8);
        for (int i = (int)(LowerX / angleGrad) + 1; i < UpperX / angleGrad; i++)
        {
            float x = ConvToPicBoxCoord(i * angleGrad, 0).X;
            g.DrawLine(Pens.Black, x, pictureBox.Height - OriginPos.Y, x, pictureBox.Height - OriginPos.Y + 5);
            g.DrawString(Math.Round(i * angleGrad, 5).ToString("#,#.###############"), strFont, Brushes.Black, x - 2, pictureBox.Height - OriginPos.Y + 5);
            g.DrawLine(Pens.LightGray, x, pictureBox.Height - OriginPos.Y, x, 0);
        }

        // 強度方向の目盛り
        float intensityGrad = ChooseGradiation(UpperY - LowerY, [1.6, 2.2, 8.0]);
        g.DrawLine(Pens.Black, OriginPos.X, 0, OriginPos.X, pictureBox.Height - OriginPos.Y);
        for (int i = (int)(LowerY / intensityGrad) + 1; i < UpperY / intensityGrad; i++)
        {
            float y = ConvToPicBoxCoord(0, i * intensityGrad).Y;
            g.DrawLine(Pens.Black, OriginPos.X - 8, y, OriginPos.X, y);
            g.DrawString((i * intensityGrad).ToString("#,#.###############"), strFont, Brushes.Black, 0, y - 6);
            g.DrawLine(Pens.LightGray, OriginPos.X - 8, y, pictureBox.Width, y);
        }
    }

    #region 座標変換
    private PointF ConvToPicBoxCoord(double x, double y) => new(
        (float)((pictureBox.Width - OriginPos.X) / (UpperX - LowerX) * (x - LowerX)) + OriginPos.X,
        (float)(pictureBox.Height - OriginPos.Y - BottomMargin - (pictureBox.Height - OriginPos.Y - BottomMargin) / (UpperY - LowerY) * (y - LowerY)));

    private PointF ConvToPicBoxCoord(PointD p) => ConvToPicBoxCoord(p.X, p.Y);
    #endregion

    private void numericUpDownWidth_ValueChanged(object sender, EventArgs e) => RefreshTable();
    private void numericUpDownMaxLength_ValueChanged(object sender, EventArgs e) => RefreshTable();
    private void AtomCoordinateTable_Resize_1(object sender, EventArgs e) => RefreshTable();
}

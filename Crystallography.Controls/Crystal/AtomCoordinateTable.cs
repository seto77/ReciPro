using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class AtomCoordinateTable : UserControl
{
    readonly ReaderWriterLockSlim rwLock = new();
    private bool skipEvent { get; set; } = false;
    public AtomCoordinateTable()
    {
        InitializeComponent();
    }

    private Crystal crystal;

    public Crystal Crystal
    {
        set
        {
            crystal = value;
            if (crystal != null && crystal.Atoms != null && crystal.Atoms.Length > 0)
            {
                skipEvent = true;
                comboBox.Items.Clear();
                for (int i = 0; i < crystal.Atoms.Length; i++)
                    comboBox.Items.Add(crystal.Atoms[i].Label);
                skipEvent = false;
                comboBox.SelectedIndex = 0;
            }
        }
        get => crystal;
    }

    private Atoms atom;

    public Atoms Atom
    {
        set
        {
            atom = value;
            if (atom != null)
            {
                for (int i = 0; i < crystal.Atoms.Length; i++)
                    if (crystal.Atoms[i] == atom)
                    {
                        comboBox.SelectedIndex = i;
                        break;
                    }
            }
        }
        get => atom;
    }

    private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (skipEvent)
            return;
        if (crystal != null && crystal.Atoms != null && crystal.Atoms.Length > 0 && comboBox.SelectedIndex < Crystal.Atoms.Length)
        {
            atom = Crystal.Atoms[comboBox.SelectedIndex];
            RefreshTable();
        }
    }





    private void RefreshTable()
    {
        if (crystal != null && crystal.Atoms != null && crystal.Atoms.Length > 0 && comboBox.SelectedIndex < Crystal.Atoms.Length)
        {
            var atom = Search(Crystal, Atom, (double)numericUpDownMaxLength.Value);
            dataSet.Tables[0].Clear();
            for (int i = 0; i < atom.Count; i++)
            {
                dataSet.Tables[0].Rows.Add(new object[] {
                atom[i].Label,
                atom[i].Distance
                });
            }
            DrawGraph(atom);
        }
    }


    public List<(string Label, double Distance)> Search(Crystal crystal, Atoms targetAtom, double maxLengthAngstrom)
    {
        var mat = 10 * crystal.MatrixReal;
        Vector3DBase pos = mat * targetAtom.Atom[0];
        var max2 = maxLengthAngstrom * maxLengthAngstrom;
        var atoms = new List<(string Label, double Distance)>();
        //まず、隣り合った単位格子の原子位置をすべて探索してCoordinatedAtom型のリストに全部入れる
        for (int max = 0; max < 8; max++)
        {
            bool flag = false;
            Parallel.For(-max, max + 1, xShift =>
            {
                for (int yShift = -max; yShift <= max; yShift++)
                    for (int zShift = -max; zShift <= max; zShift++)
                    {
                        if (Math.Abs(xShift) == max || Math.Abs(yShift) == max || Math.Abs(zShift) == max)
                        {
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
                                            flag = true;//一個でも見つけられたら続行
                                        }
                                        finally { rwLock.ExitWriteLock(); }
                                    }
                                }
                        }
                    }
            });
            if (flag == false && max > 2)
                break;
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
        if (pictureBox.Width <= 0 || pictureBox.Height <= 0 || atoms.Count == 0) return; bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
        g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        this.DoubleBuffered = true;

        //上限、下限を決める
        double width = (double)numericUpDownWidth.Value;
        LowerX = 0;
        UpperX = atoms[^1].Distance + width * 2;
        LowerY = 0;

        List<ControlPoint> controlPoint = [];
        //すべてのCoordinatedAtomにたいする始点をPositiveに、終点をNegativeに格納する
        for (int i = 0; i < atoms.Count; i++)
        {
            controlPoint.Add(new ControlPoint(atoms[i].Distance - width, true));
            controlPoint.Add(new ControlPoint(atoms[i].Distance + width, false));
        }
        controlPoint.Sort();

        double height = 0;
        UpperY = 2;
        profile.Clear();
        profile.Pt.Add(new PointD(-width * 2, 0));
        for (int i = 0; i < controlPoint.Count; i++)
        {
            profile.Pt.Add(new PointD(controlPoint[i].X, height));
            if (controlPoint[i].Flag)
                height++;
            else
                height--;
            profile.Pt.Add(new PointD(controlPoint[i].X, height));

            if (UpperY < height)
                UpperY = height;
        }
        profile.Pt.Add(new PointD(UpperX, 0));
        UpperY += 2;

        DrawHistogram(atoms);
        DrawGraduation();
        DrawLabel(atoms);

        pictureBox.Image = bmp;
    }

    private class ControlPoint(double x, bool flag) : IComparable
    {
        public double X = x;
        public bool Flag = flag;

        public int CompareTo(object obj)
        {
            return X.CompareTo(((ControlPoint)obj).X);
        }
    }

    private void DrawHistogram(List<(string Label, double Distance)> atoms)
    {
        var solidBrush = new SolidBrush(Color.LawnGreen);
        var zero = ConvToPicBoxCoord(0, 0).Y;
        for (int i = 0; i < profile.Pt.Count - 1; i++)
        {
            var p1 = ConvToPicBoxCoord(profile.Pt[i]);
            var p2 = ConvToPicBoxCoord(profile.Pt[i + 1]);
            if (Math.Abs(p1.X - p2.X) > 0.2)
                g.FillRectangle(solidBrush, new RectangleF(p1.X, p1.Y, p2.X - p1.X, zero - p1.Y));
        }
    }

    private void DrawLabel(List<(string Label, double Distance)> atoms)

    {
        var JustBeforePt = new PointF(-10, -10);
        var JustBeforeLabel = "";
        int times = 1;
        int shiftY = 15;
        Font font = new Font("Tahoma", 9);
        Brush br = new SolidBrush(Color.Red);
        for (int i = 0; i < atoms.Count; i++)
        {
            while (i < atoms.Count - 1 && Math.Abs(atoms[i].Distance - atoms[i + 1].Distance) < 0.00000000001 && atoms[i].Label == atoms[i + 1].Label)//次も同じ元素が来る場合は
            {
                times++;
                i++;
            }
            PointF pt = ConvToPicBoxCoord(atoms[i].Distance, 0);

            if (pt.X - JustBeforePt.X < 40 && JustBeforePt.Y - shiftY > 0)//字がかぶらないようにするための措置
                pt.Y = JustBeforePt.Y - shiftY;
            else
                pt.Y -= shiftY * 2;

            JustBeforePt = pt;
            JustBeforeLabel = atoms[i].Label;
            if (times == 1)
                g.DrawString(atoms[i].Label.TrimEnd(new char[] { ' ' }), font, br, pt);
            else
                g.DrawString(atoms[i].Label.TrimEnd(new char[] { ' ' }) + "(" + times.ToString() + ")", font, br, pt);
            times = 1;
        }
    }
    private void DrawGraduation()
    {
        g.FillRectangle(new SolidBrush(Color.White), 0, 0, OriginPos.X, pictureBox.Height);
        g.FillRectangle(new SolidBrush(Color.White), 0, pictureBox.Height - OriginPos.Y, pictureBox.Width, pictureBox.Height);

        float AngleGradiation;//ここより角度目盛りの描画
        double d = (UpperX - LowerX) / Math.Pow(10, (int)Math.Log10(UpperX - LowerX));
        if (d < 1.1) AngleGradiation = (float)(Math.Pow(10, (int)Math.Log10(UpperX - LowerX) - 1));
        else if (d < 2.2) AngleGradiation = (float)(2 * Math.Pow(10, (int)Math.Log10(UpperX - LowerX) - 1));
        else if (d < 5.0) AngleGradiation = (float)(5 * Math.Pow(10, (int)Math.Log10(UpperX - LowerX) - 1));
        else AngleGradiation = (float)(10 * Math.Pow(10, (int)Math.Log10(UpperX - LowerX) - 1));
        g.DrawLine(new Pen(Color.Black, 1), OriginPos.X, pictureBox.Height - OriginPos.Y, pictureBox.Width, pictureBox.Height - OriginPos.Y);
        Font strFont = new(new FontFamily("tahoma"), 8);
        for (int i = (int)(LowerX / AngleGradiation) + 1; i < UpperX / AngleGradiation; i++)
        {
            g.DrawLine(new Pen(Color.Black, 1), ConvToPicBoxCoord(i * AngleGradiation, 0).X, pictureBox.Height - OriginPos.Y, ConvToPicBoxCoord(i * AngleGradiation, 0).X, pictureBox.Height - OriginPos.Y + 5);
            g.DrawString(Math.Round(i * AngleGradiation, 5).ToString("#,#.###############"), strFont, new SolidBrush(Color.Black), ConvToPicBoxCoord(i * AngleGradiation, 0).X - 2, pictureBox.Height - OriginPos.Y + 5);
            g.DrawLine(new Pen(Color.LightGray, 1), ConvToPicBoxCoord(i * AngleGradiation, 0).X, pictureBox.Height - OriginPos.Y, ConvToPicBoxCoord(i * AngleGradiation, 0).X, 0);
        }

        float IntensityGradiation;//ここより強度目盛りの描画
        d = (UpperY - LowerY) / Math.Pow(10, (int)Math.Log10(UpperY - LowerY));
        if (d < 1.6) IntensityGradiation = (float)(Math.Pow(10, (int)Math.Log10(UpperY - LowerY) - 1));
        else if (d < 2.2) IntensityGradiation = (float)(2 * Math.Pow(10, (int)Math.Log10(UpperY - LowerY) - 1));
        else if (d < 8.0) IntensityGradiation = (float)(5 * Math.Pow(10, (int)Math.Log10(UpperY - LowerY) - 1));
        else IntensityGradiation = (float)(10 * Math.Pow(10, (int)Math.Log10(UpperY - LowerY) - 1));
        g.DrawLine(new Pen(Color.Black, 1), OriginPos.X, 0, OriginPos.X, pictureBox.Height - OriginPos.Y);
        for (int i = (int)(LowerY / IntensityGradiation) + 1; i < UpperY / IntensityGradiation; i++)
        {
            g.DrawLine(new Pen(Color.Black, 1), OriginPos.X - 8, ConvToPicBoxCoord(0, i * IntensityGradiation).Y, OriginPos.X, ConvToPicBoxCoord(0, i * IntensityGradiation).Y);
            g.DrawString((i * IntensityGradiation).ToString("#,#.###############"), strFont, new SolidBrush(Color.Black), 0, ConvToPicBoxCoord(0, i * IntensityGradiation).Y - 6);
            g.DrawLine(new Pen(Color.LightGray, 1), OriginPos.X - 8, ConvToPicBoxCoord(0, i * IntensityGradiation).Y, pictureBox.Width, ConvToPicBoxCoord(0, i * IntensityGradiation).Y);
        }
    }

    #region 座標変換関係

    private PointF ConvToPicBoxCoord(double x, double y)
    {//プロファイル座標をピクチャーボックスの座標系に変換
        return new PointF((float)((pictureBox.Width - OriginPos.X) / (UpperX - LowerX) * (x - LowerX)) + OriginPos.X,
            (float)(pictureBox.Height - OriginPos.Y - BottomMargin - (pictureBox.Height - OriginPos.Y - BottomMargin) / (UpperY - LowerY) * (y - LowerY)));
    }

    private PointF ConvToPicBoxCoord(PointD p)
    {//ピクチャーボックスの座標系に変換
        return new PointF((float)((pictureBox.Width - OriginPos.X) / (UpperX - LowerX) * (p.X - LowerX)) + OriginPos.X,
            (float)(pictureBox.Height - OriginPos.Y - BottomMargin - (pictureBox.Height - OriginPos.Y - BottomMargin) / (UpperY - LowerY) * (p.Y - LowerY)));
    }

    private PointD ConvToRealCoord(int x, int y)
    {//マウス座標をオリジナルの座標系に変換
        return new PointD(
            (double)(x - OriginPos.X) / (pictureBox.Width - OriginPos.X) * (UpperX - LowerX) + LowerX,
            (double)(pictureBox.Height - y - OriginPos.Y - BottomMargin) / (pictureBox.Height - OriginPos.Y - BottomMargin) * (UpperY - LowerY) + LowerY);
    }

    #endregion 座標変換関係


    private void numericUpDownWidth_ValueChanged(object sender, EventArgs e) => RefreshTable();

    private void numericUpDownMaxLength_ValueChanged(object sender, EventArgs e) => RefreshTable();

    private void AtomCoordinateTable_Resize_1(object sender, EventArgs e) => RefreshTable();
}
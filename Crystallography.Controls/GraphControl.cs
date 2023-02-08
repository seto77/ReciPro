using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class GraphControl : UserControl
{
    public GraphControl()
    {
        InitializeComponent();
    }

    private Bitmap Bmp;
    private Graphics G;

    #region イベント

    public delegate void DrawingRangeChangedEventHandler(RectangleD rectangle);

    public event DrawingRangeChangedEventHandler DrawingRangeChanged;

    public delegate void LinePositionChengedEventHandler();

    public event LinePositionChengedEventHandler LinePositionChanged;

    public delegate bool MouseEventHandler2(PointD pt);

    /// <summary>
    /// マウスが左ダブルクリックされたときのイベント。戻り値がtrueの場合は、その後の動作をスキップ
    /// </summary>
    public event MouseEventHandler2 MouseDoubleClick2;

    #endregion イベント

    #region プロパティ

    private double upperX = 1;
    public double UpperX { get { return upperX; } set { if (!double.IsNaN(value) && !double.IsInfinity(value)) upperX = value; } }

    private double lowerX = 0;
    public double LowerX { get { return lowerX; } set { if (!double.IsNaN(value) && !double.IsInfinity(value)) lowerX = value; } }

    private double upperY = 1;
    public double UpperY { get { return upperY; } set { if (!double.IsNaN(value) && !double.IsInfinity(value)) upperY = value; } }

    private double lowerY = 0;
    public double LowerY { get { return lowerY; } set { if (!double.IsNaN(value) && !double.IsInfinity(value)) lowerY = value; } }

    private double maximalX = 1;
    public double MaximalX { get { return maximalX; } set { if (!double.IsNaN(value) && !double.IsInfinity(value)) maximalX = value; } }

    private double minimalX = 0;
    public double MinimalX { get { return minimalX; } set { if (!double.IsNaN(value) && !double.IsInfinity(value)) minimalX = value; } }

    private double maximalY = 1;
    public double MaximalY { get { return maximalY; } set { if (!double.IsNaN(value) && !double.IsInfinity(value)) maximalY = value; } }

    private double minimalY = 0;
    public double MinimalY { get { return minimalY; } set { if (!double.IsNaN(value) && !double.IsInfinity(value)) minimalY = value; } }

    /// <summary>
    /// 描画範囲の矩形
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RectangleD DrawingRange
    {
        set
        {
            if (!double.IsInfinity(value.X) && !double.IsNaN(value.X) && value.X <= MaximalX && value.X >= MinimalX)
                LowerX = value.X;

            if (!double.IsInfinity(value.Y) && !double.IsNaN(value.Y) && value.Y <= MaximalY && value.Y >= MinimalY)
                LowerY = value.Y;

            if (!double.IsInfinity(value.Width) && !double.IsNaN(value.Width) && value.Width >= 0)
                UpperX = Math.Min(LowerX + value.Width, MaximalX);

            if (!double.IsInfinity(value.Height) && !double.IsNaN(value.Height) && value.Height >= 0)
                UpperY = Math.Min(LowerY + value.Height, MaximalY);
        }
        get
        {
            if (!double.IsInfinity(LowerX) && !double.IsInfinity(LowerY) && !double.IsInfinity(UpperX) && !double.IsInfinity(UpperY))
                return new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY);
            else
                return new RectangleD(0, 0, 1, 1);
        }
    }

    /// <summary>
    /// ピクチャーボックスのクライアント領域のサイズ
    /// </summary>
    private Size PictureBoxSize
    {
        get { return pictureBox.ClientSize; }
    }

    private bool verticalGradiationTextVisivle = true;

    public bool VerticalGradiationTextVisivle
    {
        set { verticalGradiationTextVisivle = value; Draw(); }
        get { return verticalGradiationTextVisivle; }
    }

    private bool horizontalGradiationTextVisivle = true;

    public bool HorizontalGradiationTextVisivle
    {
        set { horizontalGradiationTextVisivle = value; Draw(); }
        get { return horizontalGradiationTextVisivle; }
    }

    public Font TextFont { set { flowLayoutPanel.Font = value; } get { return flowLayoutPanel.Font; } }

    public string UpperText
    {
        get { return labelUpperText.Text; }
        set { labelUpperText.Text = value; }
    }

    private bool upperTextVisible = true;

    /// <summary>
    /// グラフ上部にマウス位置やラベル情報を示すテキストを表示するかどうか
    /// </summary>
    public bool UpperTextVisible
    {
        set
        {
            upperTextVisible = value;
            flowLayoutPanel.Visible = upperTextVisible;
        }
        get { return upperTextVisible; }
    }

    /// <summary>
    /// X軸の単位
    /// </summary>
    public string UnitX { get; set; } = "";

    /// <summary>
    /// Y軸の単位
    /// </summary>
    public string UnitY { get; set; } = "";

    private bool mousePositionVisible = true;

    /// <summary>
    /// マウス位置を表示するかどうか
    /// </summary>
    public bool MousePositionVisible
    {
        set
        {
            mousePositionVisible = value;
            labelX.Visible = labelXValue.Visible = labelY.Visible = labelYLabel.Visible = mousePositionVisible;
        }
        get { return mousePositionVisible; }
    }

    private string graphName = "";

    /// <summary>
    /// グラフの名前
    /// </summary>
    public string GraphName
    {
        set
        {
            graphName = value;
            labelUpperText.Text = graphName;
        }
        get { return graphName; }
    }

    /// <summary>
    /// マウス操作を受け付けるかどうか
    /// </summary>
    public bool AllowMouseOperation { get; set; } = true;

    private readonly List<PointD> lineList = new List<PointD>();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PointD[] LineList
    {
        set
        {
            if (value != null)
            {
                lineList.Clear();
                lineList.AddRange(value);
            }
        }
        get { return lineList.ToArray(); }
    }

    private List<PeakFunction> peaks = new();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PeakFunction[] Peaks
    {
        set { peaks.Clear(); peaks.AddRange(value); }
        get { return peaks.ToArray(); }
    }

    public Color LineColor { set; get; } = Color.Red;

    private int selectedLineIndex = -1;

    private List<Profile> srcProfileList = new();

    /// <summary>
    /// 0番目のプロファイルを設定する。複数プロファイルが設定されている場合はひとつだけにする。
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Profile Profile
    {
        set
        {
            srcProfileList = new List<Profile>();
            srcProfileList.Add(value);
            InitializeAxis();
            resetDrawRange();
            Draw();
        }
        get
        {
            if (srcProfileList == null || srcProfileList.Count == 0)
                return null;
            else
                return srcProfileList[0];
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Profile[] ProfileList
    {
        get { return srcProfileList.ToArray(); }
    }

    private float lineWidth = 1f;

    /// <summary>
    /// プロファイル描画線の太さ
    /// </summary>
    public float LineWidth
    {
        set
        {
            lineWidth = value;
            Draw();
        }
        get { return lineWidth; }
    }

    private bool useLineWidth = true;

    /// <summary>
    /// 共通のプロファイル描画線を使うか (使わない場合は各ProfileのlineWidthを使う)
    /// </summary>
    public bool UseLineWidth { set { useLineWidth = value; Draw(); } get => useLineWidth; }

    private Color divisionLineColor = Color.Gray;

    /// <summary>
    /// 目盛線の色
    /// </summary>
    public Color DivisionLineColor { set { divisionLineColor = value; Draw(); } get => divisionLineColor; }

    /// <summary>
    /// 目盛補助線の色
    /// </summary>
    public Color DivisionSubLineColor { set; get; } = Color.LightGray;

    /// <summary>
    /// 目盛文字の色
    /// </summary>
    public SolidBrush DivisionTextBrush { set; get; } = new SolidBrush(Color.Black);

    /// <summary>
    /// 背景の色
    /// </summary>
    public Color BackgroundColor { set; get; } = Color.White;

    private bool xLog = false;

    /// <summary>
    /// X軸が対数スケールかどうか
    /// </summary>
    public bool XLog
    {
        set
        {
            if (XLog != value)
            {
                xLog = value;
                toolStripMenuItemLogScaleX.Checked = XLog;
                InitializeAxis();
                setDrawRangeLimit();
                resetDrawRange();
                Draw();
            }
        }
        get => xLog;
    }

    private bool yLog = false;

    /// <summary>
    /// Y軸が対数スケールかどうか
    /// </summary>
    public bool YLog
    {
        set
        {
            if (YLog != value)
            {
                yLog = value;
                toolStripMenuItemLogScaleY.Checked = YLog;
                InitializeAxis(); setDrawRangeLimit();
                resetDrawRange();
                Draw();
            }
        }
        get => yLog;
    }

    private bool xScaleLineVisible = true;

    /// <summary>
    /// X軸の目盛りを表示するかどうか
    /// </summary>
    public bool XScaleLineVisible
    {
        set
        {
            if (XScaleLineVisible != value)
            {
                xScaleLineVisible = value;
                toolStripMenuItemScaleLineX.Checked = value;
                Draw();
            }
        }
        get { return xScaleLineVisible; }
    }

    private bool yScaleLineVisible = true;

    /// <summary>
    /// Y軸の目盛りを表示するかどうか
    /// </summary>
    public bool YScaleLineVisible
    {
        set
        {
            if (YScaleLineVisible != value)
            {
                yScaleLineVisible = value;
                toolStripMenuItemScaleLineY.Checked = value;
                Draw();
            }
        }
        get => yScaleLineVisible;
    }

    private bool isIntegerX = false;

    /// <summary>
    /// Xの値が０以上の整数値かどうか
    /// </summary>
    public bool IsIntegerX
    {
        set { isIntegerX = value; InitializeAxis(); resetDrawRange(); Draw(); }
        get => isIntegerX;
    }

    /// <summary>
    /// Yの値が０以上の整数値かどうか
    /// </summary>
    public bool IsIntegerY
    {
        set { isIntegerY = value; InitializeAxis(); resetDrawRange(); Draw(); }
        get => isIntegerY;
    }

    private bool isIntegerY = false;

    /// <summary>
    /// X軸の名称
    /// </summary>
    public string LabelX { set => labelX.Text = value; get => labelX.Text; }

    /// <summary>
    /// Y軸の名称
    /// </summary>
    public string LabelY { set => labelY.Text = value; get => labelY.Text; }

    /// <summary>
    /// 原点の位置(左下からのピクセル単位)
    /// </summary>
    public Point OriginPosition
    {
        set { originPosition = value; Draw(); }
        get => originPosition;
    }

    private Point originPosition = new Point(40, 20);

    /// <summary>
    /// 下側の余白(ピクセル単位)
    /// </summary>
    public double BottomMargin
    {
        set { bottomMargin = value; Draw(); }
        get => bottomMargin;
    }

    private double bottomMargin = 0;

    /// <summary>
    /// 左側の余白(ピクセル単位)
    /// </summary>
    public float LeftMargin
    {
        set { leftMargin = value; Draw(); }
        get => leftMargin;
    }

    private float leftMargin = 0f;

    private DrawingMode mode = DrawingMode.Line;

    /// <summary>
    /// trueの場合は棒グラフ状に表示する。falseの場合は線状に表示する
    /// </summary>
    public DrawingMode Mode
    {
        set { mode = value; Draw(); }
        get { return mode; }
    }

    private bool interpolation = false;
    public bool Interpolation { set { interpolation = value; Draw(); } get { return interpolation; } }

    public bool Smoothing { get; set; } = false;
    private int smoothingN = 0;
    private int smoothingM = 0;

    /// <summary>
    /// Profileを更新時、横軸を固定する(ただし、上限下限内で)
    /// </summary>
    private bool fixRangeHorizontal = false;

    public bool FixRangeHorizontal
    {
        set { fixRangeHorizontal = value; }
        get { return fixRangeHorizontal; }
    }

    /// <summary>
    /// Profileを更新時、縦を固定する(ただし、上限下限内で)
    /// </summary>
    private bool fixRangeVertical = false;

    public bool FixRangeVertical
    {
        set { fixRangeVertical = value; }
        get { return fixRangeVertical; }
    }

    #endregion プロパティ

    /// <summary>
    /// グラフの描き方の列挙体
    /// </summary>
    public enum DrawingMode
    {
        Line, Histogram, Point
    }

    private List<Profile> destProfileList = new List<Profile>();

    public void AddPoint(int profileNumber, PointD pt)
    {
        srcProfileList[profileNumber].Pt.Add(pt);
        InitializeAxis();
        setDrawRangeLimit();
        resetDrawRange();
        Draw();
    }

    /// <summary>
    /// プロファイルを加える
    /// </summary>
    /// <param name="p"></param>
    public void AddProfile(Profile p)
    {
        srcProfileList.Add(p);
        InitializeAxis();
        setDrawRangeLimit();
        resetDrawRange();
        Draw();
    }

    public void AddProfiles(Profile[] p, RectangleD drawingRange = new RectangleD())
    {
        srcProfileList.Clear();
        srcProfileList.AddRange(p);
        InitializeAxis();
        setDrawRangeLimit();
        if (drawingRange.Width == 0)
            resetDrawRange();
        else
            DrawingRange = drawingRange;
        Draw();
    }

    public void ReplaceProfile(int index, Profile p)
    {
        if (index < srcProfileList.Count)
            srcProfileList[index] = p;
        InitializeAxis();
        setDrawRangeLimit();
        Draw();
    }

    public void SmoothProfiles(int m, int n)
    {
        smoothingM = m;
        smoothingN = n;
        InitializeAxis();
        setDrawRangeLimit();
        Draw();
    }

    public void ClearProfile()
    {
        srcProfileList.Clear();
        destProfileList.Clear();
        pictureBox.Image = new Bitmap(PictureBoxSize.Width, PictureBoxSize.Height);
        UpperX = UpperY = MaximalX = MaximalY = 1;
        LowerX = LowerY = MinimalX = MinimalY = 0;
        Draw();
    }

    public void InitializeAxis()
    {
        if (srcProfileList == null) return;
        destProfileList = new List<Profile>();
        for (int i = 0; i < srcProfileList.Count; i++)
        {
            var temp = new Profile();
            if (srcProfileList[i] != null && srcProfileList[i].Pt != null && srcProfileList[i].Pt.Any())
                temp = convertAxis(srcProfileList[i]);
            destProfileList.Add(temp);
            setDrawRangeLimit();
        }
    }

    /// <summary>
    /// 軸を変換する
    /// </summary>
    private Profile convertAxis(Profile profile)
    {
        if (profile == null || profile.Pt == null || profile.Pt.Count == 0) return null;
        var p = profile.CopyTo();
        if (Smoothing)
            p = p.SmoothingSavitzkyGolay(smoothingM, smoothingN);

        var temp = new Profile();
        double x, y;
        for (int i = 0; i < p.Pt.Count; i++)
        {
            if (!xLog)
                x = p.Pt[i].X;
            else if (p.Pt[i].X == 0)
                if (isIntegerX)
                    x = 0;
                else
                    x = double.NegativeInfinity;
            else
                x = Math.Log10(p.Pt[i].X);

            if (!yLog)
                y = p.Pt[i].Y;
            else
            {
                if (p.Pt[i].Y == 0)
                    if (IsIntegerY)
                        y = 0;
                    else
                        y = double.NegativeInfinity;
                else if (p.Pt[i].Y > 0)
                    y = Math.Log10(p.Pt[i].Y);
                else
                    y = double.NaN;
            }

            if (!double.IsNaN(x) && !double.IsNaN(y))
                temp.Pt.Add(new PointD(x, y));
        }
        return temp;
    }

    public void SetInitialParameter(bool xLog, bool yLog)
    {
        this.xLog = xLog;
        this.yLog = yLog;
    }

    /// <summary>
    /// 現在のプロファイルから描画範囲の上限、下限値を設定　描画範囲は変更しない
    /// </summary>
    private void setDrawRangeLimit()
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0] == null || destProfileList[0].Pt == null || destProfileList[0].Pt.Count == 0) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;

        try
        {
            double minX = destProfileList.Where(d => d != null && d.Pt.Count != 0).Min(d => d.Pt.Min(p => p.X));
            double maxX = destProfileList.Where(d => d != null && d.Pt.Count != 0).Max(d => d.Pt.Max(p => p.X));
            double minY = destProfileList.Where(d => d != null && d.Pt.Count != 0).Min(d => d.Pt.Min(p => p.Y));
            double maxY = destProfileList.Where(d => d != null && d.Pt.Count != 0).Max(d => d.Pt.Max(p => p.Y));

            if (Miscellaneous.IsFiniteNumber(minX, maxX, minY, maxY))
            {
                MinimalX = minX;
                MaximalX = maxX;
                MinimalY = minY;
                MaximalY = maxY;
            }
            else
            {
                MinimalX = 0;
                MaximalX = 1;
                MinimalY = 0;
                MaximalY = 1;
                return;
            }

            if (!XLog)
            {
                double space = (MaximalX - MinimalX) * 0.01;
                MinimalX -= space;
                MaximalX += space;
            }
            else
            {
                if (isIntegerX)
                {
                    double space = (MaximalX - MinimalX) * 0.01;
                    MinimalX = 0;
                    MaximalX += space;
                }
                else
                {
                    double space = (MaximalX - MinimalX) * 0.01;
                    MinimalX -= space;
                    MaximalX += space;
                }
            }

            if (!YLog)
            {
                if (MinimalY > 0)
                    MinimalY = 0;
                else
                    MinimalY *= 1.1;
                MaximalY *= 1.1;
            }
            else
            {
                if (!IsIntegerY)
                {
                    double space = (MaximalY - MinimalY) * 0.01;
                    MinimalY = 0;
                    MaximalY += space;
                }
                else
                {
                    double space = (MaximalY - MinimalY) * 0.01;
                    MinimalY = 0;
                    MaximalY += space;
                }
            }
        }
        catch
        {
            return;
        }
    }

    /// <summary>
    /// 描画範囲Upper,LowerをMaximal,Minimalに設定する
    /// </summary>
    private void resetDrawRange()
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0] == null || destProfileList[0].Pt == null) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;
        if (!fixRangeHorizontal)
        {
            LowerX = MinimalX;
            UpperX = MaximalX;
        }
        if (!fixRangeVertical)
        {
            LowerY = MinimalY;
            UpperY = MaximalY;
        }
        if (LowerX < MinimalX) LowerX = MinimalX;
        if (LowerY < MinimalY) LowerY = MinimalY;
        if (UpperX > MaximalX) UpperX = MaximalX;
        if (UpperY > MaximalY) UpperY = MaximalY;

        DrawingRangeChanged?.Invoke(new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY));
    }

    /// <summary>
    /// 描画範囲を設定する (ただし、上限下限の範囲内で)
    /// </summary>
    /// <param name="rect"></param>
    public void SetDrawingRange(RectangleD rect)
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0].Pt == null) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;
        LowerX = Math.Max(rect.X, MinimalX);
        LowerY = Math.Max(rect.Y, MinimalY);
        UpperX = Math.Min(rect.Width + rect.X, MaximalX);
        UpperY = Math.Min(rect.Height + rect.Y, MaximalY);
        Draw();
    }

    /// <summary>
    /// X軸の描画範囲を設定する(Y軸はそのまま)
    /// </summary>
    /// <param name="rect"></param>
    public void SetDrawingRangeX(RectangleD rect)
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0].Pt == null) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;
        LowerX = Math.Max(rect.X, MinimalX);
        UpperX = Math.Min(rect.Width + rect.X, MaximalX);
        Draw();
    }

    /// <summary>
    /// X軸の描画範囲を設定する(Y軸は描画範囲内で拡大する)
    /// </summary>
    /// <param name="rect"></param>
    public void SetDrawingRangeXandExpandY(RectangleD rect)
    {
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0].Pt == null) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;
        LowerX = Math.Max(rect.X, MinimalX);
        UpperX = Math.Min(rect.Width + rect.X, MaximalX);

        double max = double.MinValue, min = double.MaxValue;
        for (int i = 0; i < destProfileList.Count; i++)
            for (int j = 0; j < destProfileList[i].Pt.Count; j++)
                if (destProfileList[i].Pt[j].X >= LowerX && destProfileList[i].Pt[j].X <= UpperX)
                {
                    max = Math.Max(destProfileList[i].Pt[j].Y, max);
                    min = Math.Min(destProfileList[i].Pt[j].Y, min);
                }
        if (max != double.MinValue)
            UpperY = max + (max - min) * 0.1;// Math.Abs(max)* 0.1;
        if (min != double.MaxValue)
            LowerY = min - (max - min) * 0.1;// Math.Abs(min) * 0.1;

        Draw();
    }

    public void Draw()
    {
        Draw(false);
    }

    public void Draw(bool initialize)
    {
        if (initialize)
        {
            InitializeAxis();
            setDrawRangeLimit();
            resetDrawRange();
        }
        if (destProfileList == null || destProfileList.Count == 0 || destProfileList[0].Pt == null || destProfileList[0].Pt.Count == 0) return;
        if (PictureBoxSize.Width <= 0 || PictureBoxSize.Height <= 0) return;

        try
        {
            Bmp = new Bitmap(PictureBoxSize.Width, PictureBoxSize.Height);
            G = Graphics.FromImage(Bmp);
            G.Clear(BackgroundColor);

            {
                G.SmoothingMode = SmoothingMode.AntiAlias;
                this.DoubleBuffered = true;

                DrawDivision();
                if (mode == DrawingMode.Histogram)
                    DrawProfileHistogram();
                else if (mode == DrawingMode.Line)
                    DrawProfileLine();
                else if (mode == DrawingMode.Point)
                    DrawProfilePoint();

                if (lineList.Any())
                    DrawLine();

                if (peaks.Any())
                    DrawPeaks();
            }
            pictureBox.Image = Bmp;
        }
        catch { }
    }

    /// <summary>
    /// グラフ中に線(LineList)で定義されたを描く
    /// </summary>
    private void DrawLine()
    {
        for (int i = 0; i < lineList.Count; i++)
        {
            double x = lineList[i].X;
            if (xLog)
            {
                if (x > 0)
                    x = Math.Log10(x);
                else
                    x = 0;
            }
            var ptStart = ConvToPicBoxCoord(x, lineList[i].Y);
            if (double.IsNaN(lineList[i].Y))
                ptStart.Y = 0;
            var ptEnd = new PointF((float)ptStart.X, (float)(pictureBox.Height - originPosition.Y));
            if (!double.IsNaN(ptStart.X) && !double.IsInfinity(ptStart.X))
                G.DrawLine(new Pen(LineColor, selectedLineIndex == i ? 2f : 1f), ptStart, ptEnd);
        }
    }

    /// <summary>
    /// グラフ中にpeaksで定義された釣鐘型曲線を描く
    /// </summary>
    private void DrawPeaks()
    {
        for (int i = 0; i < peaks.Count; i++)
        {
            double step = peaks[i].range / 100.0;
            for (double x = peaks[i].X - peaks[i].range; x < peaks[i].X + peaks[i].range; x += step)
            {
                G.DrawLine(new Pen(LineColor, 2f), ConvToPicBoxCoord(x, peaks[i].GetValue(x, x == peaks[i].X - peaks[i].range, true)), ConvToPicBoxCoord(x + step, peaks[i].GetValue(x + step, false, true)));
            }
        }
    }

    private void DrawProfilePoint()
    {
        //総和が少ないプロファイルを前面に(最後に)書く
        float maxX = PictureBoxSize.Width, minX = originPosition.X, maxY = PictureBoxSize.Height - originPosition.Y, minY = 0;
        for (int j = 0; j < destProfileList.Count; j++)
        {
            PointD[] pt = destProfileList[j].Pt.ToArray();
            if (pt.Length > 0)
            {
                Brush brush = new SolidBrush(srcProfileList[j].Color);
                for (int i = 0; i < pt.Length; i++)
                {
                    PointF p = ConvToPicBoxCoord(pt[i]);
                    if (p.X > minX - 0.001 && p.X < maxX + 0.001 && p.Y > minY - 0.001 && p.Y < maxY + 0.001)//範囲内であれば
                        G.FillEllipse(brush, p.X - 1f, p.Y - 1f, 2f, 2f);
                }
            }
        }
    }

    private void DrawProfileHistogram()
    {
        //総和が少ないプロファイルを前面に(最後に)書く
        var sort = new List<PointD>();
        for (int j = 0; j < destProfileList.Count; j++)
        {
            var pt = destProfileList[j].Pt.ToArray();
            double sum = 0;
            for (int i = 0; i < pt.Length; i++)
                sum += pt[i].Y;
            sort.Add(new PointD(sum, j));
        }
        sort.Sort();
        sort.Reverse();

        float maxX = PictureBoxSize.Width, minX = originPosition.X, maxY = PictureBoxSize.Height - originPosition.Y, minY = 0;
        float zeroY = ConvToPicBoxCoord(0, 0).Y;
        for (int j = 0; j < sort.Count; j++)
        {
            var pt = destProfileList[(int)sort[j].Y].Pt.ToArray();
            if (pt.Length > 0)
            {
                var brush = new SolidBrush(srcProfileList[(int)sort[j].Y].Color);
                //var pointList = new List<List<PointF>>();
                var beforePt = ConvToPicBoxCoord(pt[0].X, pt[0].Y);
                var presentPt = ConvToPicBoxCoord(pt[0].X, pt[0].Y);
                var nextPt = ConvToPicBoxCoord(pt[0].X, pt[0].Y);
                for (int i = 0; i < pt.Length; i++)
                {
                    if (i < pt.Length - 1)
                        nextPt = ConvToPicBoxCoord(pt[i + 1].X, pt[i + 1].Y);
                    if (presentPt.X > minY - 0.001 && presentPt.X < maxX + 0.001)
                    {
                        float x = Math.Max((beforePt.X + presentPt.X) / 2f - 0.5f, minX);
                        float y = Math.Max(presentPt.Y, minY);
                        float width = Math.Min((nextPt.X - beforePt.X) / 2f + 1f, (maxX - beforePt.X) / 2f + 1f);
                        float height = Math.Min(zeroY - y, maxY - y);
                        G.FillRectangle(brush, x, y, width, height);
                    }
                    beforePt = presentPt;
                    presentPt = nextPt;
                }
            }
        }
    }

    /// <summary>
    /// 線形式のプロファイルを描く  //一旦全部書いた後、要らない部分を切った方がいいのでは？
    /// </summary>
    private void DrawProfileLine()
    {
        var rect = new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY);
        if (rect.Width * rect.Height == 0)
            return;
        for (int j = 0; j < destProfileList.Count; j++)
        {
            if (srcProfileList[j] != null)
            {
                //if (!Interpolation)//補間モードではないとき
                //{
                var pen = new Pen(srcProfileList[j].Color, UseLineWidth ? LineWidth : srcProfileList[j].LineWidth) { LineJoin = LineJoin.Round };
                var pts = destProfileList[j].GetPointsWithinRectangle(rect);
                for (int i = 0; i < pts.Length; i++)
                    if (pts[i].Length > 1)
                        G.DrawLines(pen, pts[i].Select(p => ConvToPicBoxCoord(p)).ToArray());
                //}
                //else//補間モードの時
                //{
                //}
            }
        }
    }

    /// <summary>
    /// 上下限値(max, min)と最大目盛り数(maxDiv)から、目盛りの位置とラベルを生成する
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="maxDiv"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    public static SortedList<double, string> GetDivisions(double min, double max, int maxDiv, bool log)
    {
        if (double.IsInfinity(min) || double.IsInfinity(max)) return new SortedList<double, string>();
        var results = new SortedList<double, string>();
        double d = max - min;
        string str = "";

        if (!log)
        {
            double step = 1;
            double unit = Math.Pow(10, Math.Floor(Math.Log10(d / maxDiv)));
            if (d / unit < maxDiv) step = unit;
            else if (d / unit / 2 < maxDiv) step = unit * 2;
            else if (d / unit / 5 < maxDiv) step = unit * 5;
            else if (d / unit / 10 < maxDiv) step = unit * 10;

            for (int i = (int)(min / step) + 1; i < max / step; i++)
            {
                if (max > 1000 || max < 0.001)
                    //  str = ((i * step) / Math.Pow(10, (int)Math.Log10(max) - 1)).ToString("#,#.###############") + "E" + ((int)Math.Log10(max) - 1).ToString();
                    str = ((i * step) / Math.Pow(10, (int)Math.Log10(i * step))).ToString("#,#.###############") + "E" + ((int)Math.Log10(i * step)).ToString();
                else
                    str = Math.Round(i * step, 5).ToString("#,#.###############");
                results.Add(i * step, str);
            }
        }
        else
        {
            //先ずは無条件で有効数字0桁の目盛りを設定
            for (int i = (int)min; i < max; i++)
                if (i > min)
                    results.Add(i, "1E" + i.ToString());

            //有効数字1桁の目盛り(2E1,3E1,4E1など)を設定
            for (int i = (int)Math.Floor(min); i < max + 1; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    double a = Math.Log10(j) + i;
                    if (a >= min && a <= max)
                        results.Add(a, maxDiv / d < 8 ? "" : j.ToString() + "E" + i.ToString());
                }
            }

            if (d < 0.5)
            {
                double max2 = Math.Pow(10, max);
                double min2 = Math.Pow(10, min);
                double step = 1;
                double unit = Math.Pow(10, Math.Floor(Math.Log10((max2 - min2) / maxDiv)));
                if ((max2 - min2) / unit < maxDiv) step = unit;
                else if ((max2 - min2) / unit / 2 < maxDiv) step = unit * 2;
                else if ((max2 - min2) / unit / 5 < maxDiv) step = unit * 5;
                else if ((max2 - min2) / unit / 10 < maxDiv) step = unit * 10;

                for (int i = (int)(min2 / step) + 1; i < max2 / step; i++)
                    if (!results.ContainsKey(Math.Log10(i * step)))
                        results.Add(Math.Log10(i * step), ((i * step) / Math.Pow(10, (int)Math.Floor(Math.Log10(i * step)))).ToString("#,#.###############") + "E" + ((int)Math.Floor(Math.Log10(i * step))).ToString());
            }
        }
        return results;
    }

    public void DrawDivision()
    {
        var strFont = new Font(new FontFamily("tahoma"), 8);
        var pen = new Pen(DivisionLineColor, 1);

        //ここよりX目盛りの描画
        G.DrawLine(pen, OriginPosition.X, pictureBox.Height - OriginPosition.Y, PictureBoxSize.Width, pictureBox.Height - OriginPosition.Y);
        int maxDivisionNumber = (this.Width - this.originPosition.X) / 60 + 1;
        var divisions = GetDivisions(LowerX, UpperX, maxDivisionNumber, XLog);
        for (int i = 0; i < divisions.Count; i++)
        {
            pen = new Pen(DivisionLineColor, 1);
            G.DrawLine(pen, ConvToPicBoxCoord(divisions.Keys[i], 0).X, PictureBoxSize.Height - OriginPosition.Y, ConvToPicBoxCoord(divisions.Keys[i], 0).X, PictureBoxSize.Height - OriginPosition.Y + 5);
            if (horizontalGradiationTextVisivle)
                G.DrawString(divisions.Values[i], strFont, DivisionTextBrush, ConvToPicBoxCoord(divisions.Keys[i], 0).X - 2, PictureBoxSize.Height - OriginPosition.Y + 5);

            pen = new Pen(DivisionSubLineColor, 1);
            if (XScaleLineVisible)
                G.DrawLine(pen, ConvToPicBoxCoord(divisions.Keys[i], 0).X, PictureBoxSize.Height - OriginPosition.Y, ConvToPicBoxCoord(divisions.Keys[i], 0).X, 0);
        }

        //ここよりY目盛りの描画
        G.DrawLine(pen, OriginPosition.X, 0, OriginPosition.X, pictureBox.Height - OriginPosition.Y);
        maxDivisionNumber = (this.Height - this.originPosition.Y) / 30 + 1;
        divisions = GetDivisions(LowerY, UpperY, maxDivisionNumber, YLog);

        for (int i = 0; i < divisions.Count; i++)
        {
            pen = new Pen(DivisionLineColor, 1);
            G.DrawLine(pen, OriginPosition.X - 8, ConvToPicBoxCoord(0, divisions.Keys[i]).Y, OriginPosition.X, ConvToPicBoxCoord(0, divisions.Keys[i]).Y);

            if (horizontalGradiationTextVisivle)
                G.DrawString(divisions.Values[i], strFont, DivisionTextBrush, 0, ConvToPicBoxCoord(0, divisions.Keys[i]).Y - 6);

            pen = new Pen(DivisionSubLineColor, 1);
            if (YScaleLineVisible)
                G.DrawLine(pen, OriginPosition.X - 8, ConvToPicBoxCoord(0, divisions.Keys[i]).Y, PictureBoxSize.Width, ConvToPicBoxCoord(0, divisions.Keys[i]).Y);
        }
    }

    #region 座標変換関係

    private PointF ConvToPicBoxCoord(double x, double y)
    {//プロファイル座標をピクチャーボックスの座標系に変換
        return new PointF((float)((PictureBoxSize.Width - originPosition.X) / (UpperX - LowerX) * (x - LowerX)) + OriginPosition.X,
        (float)(PictureBoxSize.Height - originPosition.Y - BottomMargin - (PictureBoxSize.Height - OriginPosition.Y - BottomMargin) / (UpperY - LowerY) * (y - LowerY)));
    }

    private PointF ConvToPicBoxCoord(PointD p)
    {//ピクチャーボックスの座標系に変換
        return new PointF((float)((PictureBoxSize.Width - OriginPosition.X) / (UpperX - LowerX) * (p.X - LowerX)) + OriginPosition.X,
            (float)(PictureBoxSize.Height - OriginPosition.Y - BottomMargin - (PictureBoxSize.Height - OriginPosition.Y - BottomMargin) / (UpperY - LowerY) * (p.Y - LowerY)));
    }

    private PointD ConvToRealCoord(int x, int y)
    {//マウス座標をオリジナルの座標系に変換
        return new PointD(
            (double)(x - OriginPosition.X) / (PictureBoxSize.Width - OriginPosition.X) * (UpperX - LowerX) + LowerX,
            (double)(PictureBoxSize.Height - y - OriginPosition.Y - BottomMargin) / (PictureBoxSize.Height - OriginPosition.Y - BottomMargin) * (UpperY - LowerY) + LowerY);
    }

    #endregion 座標変換関係

    private void GraphControl_Resize(object sender, EventArgs e)
    {
        Draw();
    }

    /// <summary>
    /// 近いラインを探す
    /// </summary>
    /// <param name="upperX"></param>
    /// <param name="lowerX"></param>
    /// <returns></returns>
    private int serchLineIndex(double upperX, double lowerX)
    {
        double dev = double.PositiveInfinity;
        int index = -1;
        if (xLog)
        {
            upperX = Math.Pow(10, upperX);

            if (lowerX > 0) lowerX = Math.Pow(10, lowerX);
            else lowerX = -Math.Pow(10, lowerX);
        }
        for (int i = 0; i < lineList.Count; i++)
        {
            if (upperX > lineList[i].X && lowerX < lineList[i].X)
                if (dev > (upperX + lowerX) / 2 - lineList[i].X)
                {
                    dev = (upperX + lowerX) / 2 - lineList[i].X;
                    index = i;
                }
        }
        return index;
    }

    #region マウス操作関連

    public Point MouseRangeStart, MouseRangeEnd;
    public PointD MouseMovingStartPt;
    public bool MouseRangingMode = false;
    public bool MouseMovingMode = false;
    public bool LineSelectMode = false;

    private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
    {
    }

    #region コンテキストメニュー

    private void toolStripMenuItemLogScaleY_Click(object sender, EventArgs e)
    {
        YLog = !YLog;
    }

    private void toolStripMenuItemLogScaleX_Click(object sender, EventArgs e)
    {
        XLog = !XLog;
    }

    private void toolStripMenuItemScaleLineX_Click(object sender, EventArgs e)
    {
        XScaleLineVisible = !XScaleLineVisible;
    }

    private void toolStripMenuItemScaleLineY_Click(object sender, EventArgs e)
    {
        YScaleLineVisible = !YScaleLineVisible;
    }

    #endregion コンテキストメニュー

    private void pictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (!AllowMouseOperation) return;

        PointD pt = ConvToRealCoord(e.X, e.Y);
        if (e.Button == MouseButtons.Middle)
        {
            MouseMovingMode = true;
            MouseMovingStartPt = pt;
            pictureBox.Cursor = Cursors.SizeAll;
            return;
        }

        if (e.Button == MouseButtons.Right && e.X - OriginPosition.X >= 0 && PictureBoxSize.Height - e.Y - OriginPosition.Y >= 0)
        {
            MouseRangingMode = true;
            MouseRangeStart = new Point(e.X, e.Y);
            MouseRangeEnd = new Point(e.X, e.Y);
            return;
        }
        if (e.Button == MouseButtons.Left)
        {
            if (e.Clicks == 1)
            {
                int i = serchLineIndex(ConvToRealCoord(e.X + 3, e.Y).X, ConvToRealCoord(e.X - 3, e.Y).X);
                if (i >= 0)
                {
                    LineSelectMode = true;
                    selectedLineIndex = i;
                    Draw();
                }
            }
            else if (e.Clicks == 2)
            {
                if (MouseDoubleClick2 != null)
                    if (MouseDoubleClick2(pt))
                        return;

                if (Profile != null && Profile.Pt != null && Profile.Pt.Count != 0)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < Profile.Pt.Count; i++)
                        sb.AppendLine(Profile.Pt[i].X.ToString() + "\t" + Profile.Pt[i].Y.ToString());
                    Clipboard.SetDataObject(sb.ToString());
                }
            }
        }
    }

    private void pictureBox_MouseMove(object sender, MouseEventArgs e)
    {
        if (!AllowMouseOperation) return;

        //マウスが動いたとき
        PointD pt = ConvToRealCoord(e.X, e.Y);
        double x = pt.X;
        x = XLog ? Math.Pow(10, x) : x;
        x = IsIntegerX ? (int)(Math.Round(x)) : x;

        double y = pt.Y;
        y = XLog ? Math.Pow(10, y) : y;
        y = IsIntegerY ? (int)(Math.Round(y)) : y;

        if (!XLog)
            labelXValue.Text = x.ToString("g") + UnitX;
        else
            labelXValue.Text = x.ToString("E") + UnitX;
        if (!YLog)
            labelYLabel.Text = y.ToString("g") + UnitY;
        else
            labelYLabel.Text = y.ToString("E") + UnitY;

        if (MouseMovingMode)
        {
            pictureBox.Cursor = Cursors.SizeAll;
            //MouseRangingStartが現在の中心位置に来るように設定を治す
            double devX = -pt.X + MouseMovingStartPt.X;
            double devY = -pt.Y + MouseMovingStartPt.Y;

            if (devX + UpperX < MaximalX && devX + LowerX > MinimalX)
            {
                UpperX += devX;
                LowerX += devX;
            }
            if (devY + UpperY < MaximalY && devY + LowerY > MinimalY)
            {
                UpperY += devY;
                LowerY += devY;
            }
            pt = ConvToRealCoord(e.X, e.Y);
            MouseMovingStartPt = pt;
            Draw();
            return;
        }
        else
            pictureBox.Cursor = Cursors.Default;

        if (MouseRangingMode)
        {//範囲選択モードのとき
            MouseRangeEnd = new Point(e.X, e.Y);
            pictureBox.Refresh();
            return;
        }

        if (LineSelectMode && e.Button == MouseButtons.Left)
        {
            if (e.X > 3 && e.Y > 3 && e.X < PictureBoxSize.Width - 3 && e.Y < PictureBoxSize.Height - 3)
            {
                if (lineList.Count > selectedLineIndex && selectedLineIndex >= 0)
                {
                    //lineList[selectedLineIndex].X = x;
                    lineList[selectedLineIndex] = new PointD(x, lineList[selectedLineIndex].Y);

                    Draw();
                    LinePositionChanged?.Invoke();
                    Refresh();
                }
            }
        }
    }

    private void pictureBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (!AllowMouseOperation) return;

        MouseMovingMode = false;
        LineSelectMode = false;

        var pt = ConvToRealCoord(e.X, e.Y);

        if (MouseRangingMode)
        {
            MouseRangingMode = false;
            MouseRangeEnd = new Point(e.X, e.Y);

            if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) < 3 || Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) < 3)
            {//選択範囲があまりにも小さすぎたら
                //縮小
                LowerX = LowerX * 1.5 - UpperX * 0.5;
                if (LowerX < MinimalX) LowerX = MinimalX;

                UpperX = UpperX * 1.5 - LowerX * 0.5;
                if (UpperX > MaximalX) UpperX = MaximalX;

                LowerY = LowerY * 1.5 - UpperY * 0.5;
                if (LowerY < MinimalY) LowerY = MinimalY;

                UpperY = UpperY * 1.5 - LowerY * 0.5;
                if (UpperY > MaximalY) UpperY = MaximalY;

                Draw();
                DrawingRangeChanged?.Invoke(new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY));
            }
            else if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) < 10 || Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) < 10)
            {//選択範囲が中途半端に小さすぎたら
                MouseRangingMode = false;
                pictureBox.Refresh();
            }
            else
            {
                double xmax = ConvToRealCoord(Math.Max(MouseRangeStart.X, MouseRangeEnd.X), 1).X;
                double xmin = ConvToRealCoord(Math.Min(MouseRangeStart.X, MouseRangeEnd.X), 1).X;
                double ymin = ConvToRealCoord(1, Math.Max(MouseRangeStart.Y, MouseRangeEnd.Y)).Y;
                double ymax = ConvToRealCoord(1, Math.Min(MouseRangeStart.Y, MouseRangeEnd.Y)).Y;
                if (xmax - xmin < 0.000001 || ymax - ymin < 0.00001) return;
                if (xmax > UpperX) xmax = UpperX;
                if (xmin < LowerX) xmin = LowerX;
                if (ymax > UpperY) ymax = UpperY;
                if (ymin < LowerY) ymin = LowerY;

                LowerX = xmin; UpperX = xmax; LowerY = ymin; UpperY = ymax;
                Draw();
                DrawingRangeChanged?.Invoke(new RectangleD(LowerX, LowerY, UpperX - LowerX, UpperY - LowerY));
            }
        }
        else if (e.Button == MouseButtons.Right && (e.X - OriginPosition.X) * (PictureBoxSize.Height - e.Y - OriginPosition.Y) < 0)
        {
            toolStripMenuItemLogScaleX.Visible = e.X > OriginPosition.X && PictureBoxSize.Height - e.Y < OriginPosition.Y;
            toolStripMenuItemScaleLineX.Visible = e.X > OriginPosition.X && PictureBoxSize.Height - e.Y < OriginPosition.Y;
            toolStripMenuItemLogScaleY.Visible = e.X < OriginPosition.X && PictureBoxSize.Height - e.Y > OriginPosition.Y;
            toolStripMenuItemScaleLineY.Visible = e.X < OriginPosition.X && PictureBoxSize.Height - e.Y > OriginPosition.Y;
            MouseRangingMode = false;
            contextMenuStripY.Show(this.pictureBox, e.X, e.Y);
        }
    }

    #endregion マウス操作関連

    private void pictureBox_Paint(object sender, PaintEventArgs e)
    {
        if (MouseRangingMode)
        {
            Pen pen = new Pen(Brushes.Gray) { DashStyle = DashStyle.Dash };
            e.Graphics.DrawRectangle(pen, Math.Min(MouseRangeStart.X, MouseRangeEnd.X), Math.Min(MouseRangeStart.Y, MouseRangeEnd.Y),
                Math.Abs(MouseRangeStart.X - MouseRangeEnd.X), Math.Abs(MouseRangeStart.Y - MouseRangeEnd.Y));
        }
    }

    private void toolStripMenuItemLogScaleX_CheckedChanged(object sender, EventArgs e)
    {
        XLog = toolStripMenuItemLogScaleX.Checked;
        YLog = toolStripMenuItemLogScaleY.Checked;
    }
}
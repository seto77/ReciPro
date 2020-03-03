using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class DistributionGraphControl : UserControl
    {
        public DistributionGraphControl()
        {
            InitializeComponent();
        }

        public class Ellipse
        {
            public PointD Center = new PointD(0, 0);
            public Color LineColor = Color.White;
            public Color FillColor = Color.Black;

            public double Width = 0;
            public double Height = 0;
            public double LineWidth = 0;

            public Ellipse(Ellipse ellipse)
            {
                Center = ellipse.Center;
                LineColor = ellipse.LineColor;
                FillColor = ellipse.FillColor;
                Width = ellipse.Width;
                Height = ellipse.Height;
                LineWidth = ellipse.LineWidth;
            }

            public Ellipse(PointD center, double width, double height, Color fillColor, Color lineColor, double lineWidth)
            {
                Center = new PointD(center.X, center.Y);
                Width = width;
                Height = height;
                FillColor = fillColor;
                LineColor = lineColor;
                LineWidth = lineWidth;
            }
        }

        #region プロパティ

        private List<Ellipse> data = new List<Ellipse>();

        private int selectedIndex = -1;

        public int SelectedIndex
        {
            set { selectedIndex = value; }
            get { return selectedIndex; }
        }

        private Color lineColor;

        public Color LineColor
        {
            set { lineColor = value; Draw(); }
            get { return lineColor; }
        }

        private Color divisionLineColor = Color.Gray;

        public Color DivisionLineColor
        {
            set { divisionLineColor = value; Draw(); }
            get { return divisionLineColor; }
        }

        private Color divisionSubLineColor = Color.LightGray;

        public Color DivisionSubLineColor
        {
            set { divisionSubLineColor = value; Draw(); }
            get { return divisionSubLineColor; }
        }

        private SolidBrush divisionTextBrush = new SolidBrush(Color.Black);

        public SolidBrush DivisionTextBrush
        {
            set { divisionTextBrush = value; Draw(); }
            get { return divisionTextBrush; }
        }

        private Color backgroundColor = Color.White;

        public Color BackgroundColor
        {
            set { backgroundColor = value; Draw(); }
            get { return backgroundColor; }
        }

        private bool xLog = false;

        /// <summary>
        /// X軸が対数スケールかどうか
        /// </summary>
        public bool XLog
        {
            set { xLog = value; }
            get { return xLog; }
        }

        private bool yLog = false;

        /// <summary>
        /// Y軸が対数スケールかどうか
        /// </summary>
        public bool YLog
        {
            set { yLog = value; Initialize(); Draw(); }
            get { return yLog; }
        }

        private bool isIntegerX = false;

        /// <summary>
        /// Xの値が０以上の整数値かどうか
        /// </summary>
        public bool IsIntegerX
        {
            set { isIntegerX = value; Initialize(); Draw(); }
            get { return isIntegerX; }
        }

        private bool isIntegerY = false;

        /// <summary>
        /// Yの値が０以上の整数値かどうか
        /// </summary>
        public bool IsIntegerY
        {
            set { isIntegerY = value; Initialize(); Draw(); }
            get { return isIntegerY; }
        }

        public string LabelX
        {
            set { labelX.Text = value; }
            get { return labelX.Text; }
        }

        public string LabelY
        {
            set { labelY.Text = value; }
            get { return labelY.Text; }
        }

        private Point originPosition = new Point(40, 20);

        public Point OriginPosition
        {
            set { originPosition = value; Draw(); }
            get { return originPosition; }
        }

        private float bottomMargin = 0f;

        public float BottomMargin
        {
            set { bottomMargin = value; Draw(); }
            get { return bottomMargin; }
        }

        private float leftMargin = 0f;

        public float LeftMargin
        {
            set { leftMargin = value; Draw(); }
            get { return leftMargin; }
        }

        #endregion プロパティ

        private Bitmap Bmp;
        private Graphics G;
        public double UpperX, LowerX;
        public double UpperY, LowerY;
        public double MaximalX, MinimalX;
        public double MaximalY, MinimalY;

        private Ellipse[] destData;

        public void ClearData()
        {
            data.Clear();
            destData = new Ellipse[0];
        }

        public void AddData(PointD center, double width, double height, Color fillColor, Color lineColor, double lineWidth, bool ReDraw)
        {
            data.Add(new Ellipse(center, width, height, fillColor, lineColor, width));
            if (ReDraw)
            {
                Initialize();
                Draw();
            }
        }

        public void Initialize()
        {
            destData = new Ellipse[0];
            if (data != null && data.Count > 2)
            {
                convertAxis();
                setDrawRangeLimit();
                resetDrawRange();
            }
        }

        public void SetInitialParameter(bool xLog, bool yLog)
        {
            this.xLog = xLog;
            this.yLog = yLog;
        }

        /// <summary>
        /// 対数目盛の場合は軸を変換する
        /// </summary>
        private void convertAxis()
        {
            destData = new Ellipse[data.Count];
            double x = 0, y = 0;
            for (int i = 0; i < data.Count; i++)
            {
                destData[i] = new Ellipse(data[i]);
                if (!xLog)
                    x = destData[i].Center.X;
                else
                {
                    if (destData[i].Center.X == 0)
                        x = double.NegativeInfinity;
                    else
                        x = Math.Log10(destData[i].Center.X);
                }

                if (!yLog)
                    y = destData[i].Center.Y;
                else
                {
                    if (destData[i].Center.Y == 0)
                        y = double.NegativeInfinity;
                    else
                        y = Math.Log10(destData[i].Center.Y);
                }
                if (!double.IsNaN(x) && !double.IsNaN(y))
                    destData[i].Center = new PointD(x, y);
            }
        }

        /// <summary>
        /// 現在のプロファイルから描画範囲の上限、下限値を設定　描画範囲は変更しない
        /// </summary>
        private void setDrawRangeLimit()
        {
            if (destData == null || destData.Length < 2) return;
            if (pictureBox.Width <= 0 || pictureBox.Height <= 0) return;

            MinimalX = double.PositiveInfinity;
            MaximalX = double.NegativeInfinity;
            MinimalY = double.PositiveInfinity;
            MaximalY = double.NegativeInfinity;

            for (int i = 0; i < destData.Length; i++)
            {
                if (!double.IsInfinity(destData[i].Center.X) && !double.IsNaN(destData[i].Center.X))
                {
                    MinimalX = Math.Min(destData[i].Center.X, MinimalX);
                    MaximalX = Math.Max(destData[i].Center.X, MaximalX);
                }
                if (!double.IsInfinity(destData[i].Center.Y) && !double.IsNaN(destData[i].Center.Y))
                {
                    MinimalY = Math.Min(destData[i].Center.Y, MinimalY);
                    MaximalY = Math.Max(destData[i].Center.Y, MaximalY);
                }
            }
            //if (double.IsInfinity(MinimalX)) return;

            if (xLog)
            {
                MinimalX--;
                MaximalX++;
            }
            else
            {
                if (MinimalX != MaximalX)
                {
                    double margin = (MaximalX - MinimalX) * 0.1;
                    MinimalX -= margin;
                    MaximalX += margin;
                }
                else
                {
                    MinimalX--;
                    MaximalX++;
                }
            }
            if (yLog)
            {
                MinimalY--;
                MaximalY++;
            }
            else
            {
                if (MinimalY != MaximalY)
                {
                    double margin = (MaximalY - MinimalY) * 0.1;
                    MinimalY -= margin;
                    MaximalY += margin;
                }
                else
                {
                    MinimalY--;
                    MaximalY++;
                }
            }
        }

        /// <summary>
        /// 描画範囲Upper,LowerをMaximal,Minimalに設定する
        /// </summary>
        private void resetDrawRange()
        {
            LowerX = MinimalX;
            LowerY = MinimalY;
            UpperX = MaximalX;
            UpperY = MaximalY;
        }

        public void Draw()
        {
            if (pictureBox.Width <= 0 || pictureBox.Height <= 0) return;

            Bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            G = Graphics.FromImage(Bmp);
            G.Clear(BackgroundColor);

            if (destData != null && destData.Length > 2)
            {
                G.SmoothingMode = SmoothingMode.AntiAlias;
                this.DoubleBuffered = true;

                DrawDivision();
                DrawEllipses();
            }
            pictureBox.Image = Bmp;
        }

        private void DrawEllipses()
        {
            for (int i = 0; i < data.Count; i++)
            {
                PointF p = ConvToPicBoxCoord(destData[i].Center.X, destData[i].Center.Y);
                if (p.X > originPosition.X && p.X < pictureBox.ClientSize.Width && p.Y > 0 && p.Y < pictureBox.ClientSize.Height - originPosition.Y)
                {
                    G.FillEllipse(new SolidBrush(destData[i].FillColor), p.X, p.Y, (float)destData[i].Width, (float)destData[i].Height);
                    G.DrawEllipse(new Pen(destData[i].LineColor, (float)destData[i].LineWidth), p.X, p.Y, (float)destData[i].Width, (float)destData[i].Height);
                }
            }
        }

        public void DrawDivision()
        {
            float xGradiation;//ここより角度目盛りの描画
            double d = (UpperX - LowerX) / Math.Pow(10, (int)Math.Log10(UpperX - LowerX));
            if (d < 1.6) xGradiation = (float)(Math.Pow(10, (int)Math.Log10(UpperX - LowerX) - 1));
            else if (d < 3.2) xGradiation = (float)(2 * Math.Pow(10, (int)Math.Log10(UpperX - LowerX) - 1));
            else if (d < 8.0) xGradiation = (float)(5 * Math.Pow(10, (int)Math.Log10(UpperX - LowerX) - 1));
            else xGradiation = (float)(10 * Math.Pow(10, (int)Math.Log10(UpperX - LowerX) - 1));

            Pen pen = new Pen(DivisionLineColor, 1);

            G.DrawLine(pen, OriginPosition.X, pictureBox.Height - OriginPosition.Y, pictureBox.Width, pictureBox.Height - OriginPosition.Y);
            Font strFont = new Font(new FontFamily("tahoma"), 8);
            for (int i = (int)(LowerX / xGradiation) + 1; i < UpperX / xGradiation; i++)
            {
                pen = new Pen(DivisionLineColor, 1);
                G.DrawLine(pen, ConvToPicBoxCoord(i * xGradiation, 0).X, pictureBox.Height - OriginPosition.Y, ConvToPicBoxCoord(i * xGradiation, 0).X, pictureBox.Height - OriginPosition.Y + 5);
                string str = "";

                if (!xLog)
                {
                    if (UpperX > 1000)
                        str = ((i * xGradiation) / Math.Pow(10, (int)Math.Log10(UpperX) - 1)).ToString("#,#.###############") + "E" + ((int)Math.Log(UpperX) - 1).ToString();
                    else
                        str = Math.Round(i * xGradiation, 5).ToString("#,#.###############");
                }
                else
                    str = "1.E" + Math.Round(i * xGradiation, 5).ToString("g");
                G.DrawString(str, strFont, DivisionTextBrush, ConvToPicBoxCoord(i * xGradiation, 0).X - 2, pictureBox.Height - OriginPosition.Y + 5);

                pen = new Pen(divisionSubLineColor, 1);
                //if (checkBoxShowScaleLine.Checked)
                G.DrawLine(pen, ConvToPicBoxCoord(i * xGradiation, 0).X, pictureBox.Height - OriginPosition.Y, ConvToPicBoxCoord(i * xGradiation, 0).X, 0);
            }

            float yGradiation;//ここより強度目盛りの描画
            d = (UpperY - LowerY) / Math.Pow(10, (int)Math.Log10(UpperY - LowerY));
            if (d < 1.6) yGradiation = (float)(Math.Pow(10, (int)Math.Log10(UpperY - LowerY) - 1));
            else if (d < 3.2) yGradiation = (float)(2 * Math.Pow(10, (int)Math.Log10(UpperY - LowerY) - 1));
            else if (d < 8.0) yGradiation = (float)(5 * Math.Pow(10, (int)Math.Log10(UpperY - LowerY) - 1));
            else yGradiation = (float)(10 * Math.Pow(10, (int)Math.Log10(UpperY - LowerY) - 1));

            pen = new Pen(DivisionLineColor, 1);
            G.DrawLine(pen, OriginPosition.X, 0, OriginPosition.X, pictureBox.Height - OriginPosition.Y);
            for (int i = (int)(LowerY / yGradiation) + 1; i < UpperY / yGradiation; i++)
            {
                pen = new Pen(DivisionLineColor, 1);
                G.DrawLine(pen, OriginPosition.X - 8, ConvToPicBoxCoord(0, i * yGradiation).Y, OriginPosition.X, ConvToPicBoxCoord(0, i * yGradiation).Y);
                string str = "";
                if (!yLog)
                {
                    if (UpperY > 1000)
                        str = ((i * yGradiation) / Math.Pow(10, (int)Math.Log10(UpperY) - 1)).ToString("#,#.###############") + "E" + ((int)Math.Log10(UpperY) - 1).ToString();
                    else
                        str = Math.Round(i * yGradiation, 5).ToString("#,#.###############");
                }
                else
                    str = "1.E" + Math.Round(i * xGradiation, 5).ToString("g");

                G.DrawString(str, strFont, DivisionTextBrush, 0, ConvToPicBoxCoord(0, i * yGradiation).Y - 6);

                pen = new Pen(DivisionSubLineColor, 1);
                //if (checkBoxShowScaleLine.Checked)
                G.DrawLine(pen, OriginPosition.X - 8, ConvToPicBoxCoord(0, i * yGradiation).Y, pictureBox.Width, ConvToPicBoxCoord(0, i * yGradiation).Y);
            }
        }

        #region 座標変換関係

        private PointF ConvToPicBoxCoord(double x, double y)
        {//プロファイル座標をピクチャーボックスの座標系に変換
            return new PointF((float)((pictureBox.Width - originPosition.X) / (UpperX - LowerX) * (x - LowerX)) + OriginPosition.X,
                (float)(pictureBox.Height - originPosition.Y - BottomMargin - (pictureBox.Height - OriginPosition.Y - BottomMargin) / (UpperY - LowerY) * (y - LowerY)));
        }

        private PointF ConvToPicBoxCoord(PointD p)
        {//ピクチャーボックスの座標系に変換
            return new PointF((float)((pictureBox.Width - OriginPosition.X) / (UpperX - LowerX) * (p.X - LowerX)) + OriginPosition.X,
                (float)(pictureBox.Height - OriginPosition.Y - BottomMargin - (pictureBox.Height - OriginPosition.Y - BottomMargin) / (UpperY - LowerY) * (p.Y - LowerY)));
        }

        private PointD ConvToRealCoord(int x, int y)
        {//マウス座標をオリジナルの座標系に変換
            return new PointD(
                (double)(x - OriginPosition.X) / (pictureBox.Width - OriginPosition.X) * (UpperX - LowerX) + LowerX,
                (double)(pictureBox.Height - y - OriginPosition.Y - BottomMargin) / (pictureBox.Height - OriginPosition.Y - BottomMargin) * (UpperY - LowerY) + LowerY);
        }

        #endregion 座標変換関係

        private void GraphControl_Resize(object sender, EventArgs e)
        {
            Draw();
        }

        public Point MouseRangeStart, MouseRangeEnd;
        public PointD MouseMovingStartPt;
        public bool MouseRangingMode;
        public bool MouseMovingMode;

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PointD pt = ConvToRealCoord(e.X, e.Y);
            if (e.Button == MouseButtons.Middle)
            {
                MouseMovingMode = true;
                MouseMovingStartPt = pt;
                pictureBox.Cursor = Cursors.SizeAll;
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                MouseRangingMode = true;
                MouseRangeStart = new Point(e.X, e.Y);
                return;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //マウスが動いたとき
            PointD pt = ConvToRealCoord(e.X, e.Y);
            double x = xLog ? Math.Pow(10, pt.X) : pt.X;
            x = IsIntegerX ? (int)(Math.Round(x)) : x;
            double y = yLog ? Math.Pow(10, pt.Y) : pt.Y;
            y = IsIntegerY ? (int)(Math.Round(y)) : y;

            pictureBox.Cursor = Cursors.SizeAll;

            labelXValue.Text = x.ToString("g");
            labelYLabel.Text = y.ToString("g");
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
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            MouseMovingMode = false;

            if (MouseRangingMode)
            {
                MouseRangingMode = false;
                MouseRangeEnd = new Point(e.X, e.Y);

                if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) < 3 || Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) < 3)
                {//選択範囲があまりにも小さすぎたら
                    //縮小
                    LowerX = LowerX * 1.5f - UpperX * 0.5f;
                    if (LowerX < MinimalX) LowerX = MinimalX;

                    UpperX = UpperX * 1.5f - LowerX * 0.5f;
                    if (UpperX > MaximalX) UpperX = MaximalX;

                    LowerY = LowerY * 1.5f - UpperY * 0.5f;
                    if (LowerY < 0) LowerY = 0;

                    UpperY = UpperY * 1.5f - LowerY * 0.5f;
                    if (UpperY > MaximalY) UpperY = MaximalY;

                    Draw();
                    return;
                }
                else if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) < 10 || Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) < 10)
                {//選択範囲が中途半端に小さすぎたら
                    MouseRangingMode = false;
                    pictureBox.Refresh();
                    return;
                }
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
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (MouseRangingMode)
            {
                Pen pen = new Pen(Brushes.Gray);
                pen.DashStyle = DashStyle.Dash;
                e.Graphics.DrawRectangle(pen, Math.Min(MouseRangeStart.X, MouseRangeEnd.X), Math.Min(MouseRangeStart.Y, MouseRangeEnd.Y),
                    Math.Abs(MouseRangeStart.X - MouseRangeEnd.X), Math.Abs(MouseRangeStart.Y - MouseRangeEnd.Y));
            }
        }
    }
}
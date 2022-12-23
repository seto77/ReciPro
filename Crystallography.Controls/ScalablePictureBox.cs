using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

//using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls;

[Serializable]
public partial class ScalablePictureBox : UserControl
{
    public bool SkipEvent = false;
    public bool SkipDrawing = false;
    public enum SymbolShape { Circle, Cross, Line, CircleAndCross };

    #region Symbolクラス

    [Serializable]
    public class Symbol
    {
        public object Tag { get; set; } = null;

        public bool LabelVisible { get; set; } = true;
        public bool SymbolVisible { get; set; } = true;

        public SymbolShape Shape { get; set; } = SymbolShape.Cross;

        //Circleのオプション
        public float CircleRadius { set; get; }

        public PointD CircleCenter { set; get; }
        public Color CircleColor { get; set; }

        //Cross(ペケ印)のオプション
        public PointD CrossPosition { set; get; }

        public Color CrossColor1 { get; set; } = Color.Black;
        public Color CrossColor2 { get; set; } = Color.Black;
        public float CrossSize { get; set; }

        //Lineのオプション
        public PointD LinePosition1 { set; get; }

        public PointD LinePosition2 { set; get; }
        public Color LineColor { get; set; }

        //Labelのオプション
        public bool Bold { get; set; } = false;

        public bool Italic { get; set; } = false;
        public string Label { set; get; } = "";

        public Symbol()
        {
        }

        /// <summary>
        /// Circleのコンストラクタ
        /// </summary>
        /// <param name="label"></param>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="color"></param>
        public Symbol(string label, PointD position, double radius, Color color)
        {
            Shape = SymbolShape.Circle;
            Label = label;
            CircleCenter = position;
            CircleRadius = (float)radius;
            CircleColor = color;
        }

        public Symbol(string label, PointD position, double circleRadius, Color circleColor, double crossSize, Color crossColor1, Color crossColor2)
        {
            Shape = SymbolShape.CircleAndCross;
            Label = label;
            CircleCenter = position;
            CircleRadius = (float)circleRadius;
            CircleColor = circleColor;
            CrossPosition = position;
            CrossColor1 = crossColor1;
            CrossColor2 = crossColor2;
            CrossSize = (float)crossSize;
        }


        /// <summary>
        /// Crossのコンストラクタ
        /// </summary>
        /// <param name="position"></param>
        /// <param name="label"></param>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        public Symbol(string label, PointD position, Color color1, Color color2, double size)
        {
            Shape = SymbolShape.Cross;
            Label = label;
            CrossPosition = position;
            CrossColor1 = color1;
            CrossColor2 = color2;
            CrossSize = (float)size;
        }

        /// <summary>
        /// Lineのコンストラクタ
        /// </summary>
        /// <param name="label"></param>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <param name="color"></param>
        public Symbol(string label, PointD position1, PointD position2, Color color)
        {
            Shape = SymbolShape.Line;
            Label = label;
            LinePosition1 = position1;
            LinePosition2 = position2;
            LineColor = color;
        }
    }

    #endregion Symbolクラス

    public ScalablePictureBox()
    {
        InitializeComponent();
        this.DoubleBuffered = true;
        pictureBox.Location = new Point(0, 0);
        pictureBox.ClientSize = this.Size;
        pictureBox.MouseWheel += new MouseEventHandler(pictureBox_MouseWheel);

        vScrollBar.Width = 16;
        hScrollBar.Height = 16;
        pictureBox.Controls.Add(label);
        label.BackColor = Color.Transparent;
    }

    #region プロパティ

    /// <summary>
    /// VisualStudioデザイナーの編集の時はTrue
    /// </summary>
    public new bool DesignMode
    {
        get
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;
            Control ctrl = this;
            while (ctrl != null)
            {
                if (ctrl.Site != null && ctrl.Site.DesignMode)
                    return true;
                ctrl = ctrl.Parent;
            }
            return false;
        }
    }

    /// <summary>
    /// 上下方向の反転をするかどうか
    /// </summary>
    public bool VerticalFlip
    {
        set { PseudoBitmap.VerticalFlip = value; drawPictureBox(); }
        get { return PseudoBitmap.VerticalFlip; }
    }

    private bool horizontalFlip = false;

    /// <summary>
    /// 上下方向の反転をするかどうか
    /// </summary>
    public bool HorizontalFlip
    {
        set { PseudoBitmap.HorizontalFlip = value; drawPictureBox(); }
        get { return PseudoBitmap.HorizontalFlip; }
    }

    private double minZoom = 0.1f;
    private readonly double maxZoom = 128f;

    private double _Zoom = 1;

    /// <summary>
    /// 表示倍率
    /// </summary>
    public double Zoom
    {
        set
        {
            if (_Zoom != value)
            {
                _Zoom = Math.Min(maxZoom, Math.Max(minZoom, value));//閾値を超えないように
                setControlLayout();//ズームに応じたコントロールをセッティングする
                skipScrollBarEvent = true;
                checkInvalidCenter();//center位置が適切かどうかチェックする
                skipScrollBarEvent = false;

                drawPictureBox();//イメージを描画する

                if (!SkipEvent)
                    DrawingAreaChanged?.Invoke(this, Zoom, Center);
            }
        }
        get { return _Zoom; }
    }

    private PointD _Center = new(double.NaN, double.NaN);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    /// <summary>
    /// 中心ピクセル
    /// </summary>
    public PointD Center
    {
        set
        {
            if (!value.IsNaN)
            {
                _Center = value;
                if (hScrollBar == null || vScrollBar == null || _pseudoBitmap == null) return;
                skipScrollBarEvent = true;
                checkInvalidCenter();//center位置が適切かどうかチェックする
                skipScrollBarEvent = false;
                drawPictureBox();//イメージを描画する
                if (!SkipEvent)
                    DrawingAreaChanged?.Invoke(this, Zoom, Center);
            }
        }
        get => _Center.IsNaN ? new PointD(0, 0) : _Center;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    private Vector3DBase zoomAndCenter = new(1, 0, 0);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public (double Zoom, PointD Center) ZoomAndCenter
    {
        set
        {
            if (FixZoomAndCenter) return;
            skipScrollBarEvent = true;
            _Zoom = Math.Min(maxZoom, Math.Max(minZoom, value.Zoom));//閾値を超えないように
            _Center = new PointD(value.Center);
            if (hScrollBar == null || vScrollBar == null || _pseudoBitmap == null) return;
            setControlLayout();//ズームに応じたコントロールをセッティングする
            checkInvalidCenter();//center位置が適切かどうかチェックする
            skipScrollBarEvent = false;
            drawPictureBox();//イメージを描画する

            DrawingAreaChanged?.Invoke(this, Zoom, Center);
        }
        get { return (Zoom, new PointD(Center.X, Center.Y)); }
    }

    /// <summary>
    /// ZoomやCenter位置を固定するかどうか
    /// </summary>
    public bool FixZoomAndCenter { get; set; } = false;

    public Size CanvasSize { get { return pictureBox.ClientSize; } }

    /// <summary>
    /// フォーカスイベント(Enter)を有効にするかどうか
    /// </summary>
    public bool FocusEventEnabled { get; set; } = false;

    private bool showFocusRectangle = false;

    /// <summary>
    /// 外枠を表示する
    /// </summary>
    public bool ShowRimRentangle
    {
        get => showFocusRectangle;
        set
        {
            if (showFocusRectangle != value)
            {
                showFocusRectangle = value;
                this.Refresh();
            }
        }
    }

    private bool showAreaRectangle = false;

    /// <summary>
    /// AreaRectangleで指定した矩形を表示するかどうか
    /// </summary>
    public bool ShowAreaRectangle
    {
        set
        {
            if (showAreaRectangle != value)
            {
                showAreaRectangle = value;
                this.Refresh();
            }
        }
        get => showAreaRectangle;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    private RectangleD areaRentagle = new RectangleD();
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RectangleD AreaRectangle
    {
        set { areaRentagle = value; this.Refresh(); }
        get => areaRentagle;
    }



    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<Symbol> Symbols { set; get; } = new List<Symbol>();

    /// <summary>
    /// 左上に表示するテキスト
    /// </summary>
    public override string Text { get => label.Text; set => label.Text = value; }

    /// <summary>
    /// 左上に表示するテキストの色
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color TextColor { get => label.ForeColor; set => label.ForeColor = value; }

    private PseudoBitmap _pseudoBitmap = new PseudoBitmap();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PseudoBitmap PseudoBitmap
    {
        set
        {
            if (value != null)
            {
                if (value.Width * value.Height == 0) return;
                _pseudoBitmap = value;
                minZoom = Math.Min((double)(this.ClientSize.Width - 1) / _pseudoBitmap.Width, (double)(this.ClientSize.Height - 1) / _pseudoBitmap.Height);
                if (!FixZoomAndCenter)
                {
                    Zoom = minZoom;
                    Center = new PointD(value.Width / 2, value.Height / 2);
                }
                checkInvalidCenter();
            }
        }
        get { return _pseudoBitmap; }
    }

    /// <summary>
    /// マウスによるスケーリングが可能かどうか
    /// </summary>
    public bool MouseScaling { set; get; }//マウスによるスケーリングが可能かどうか

    /// <summary>
    /// マウスによる平行移動が可能かどうか
    /// </summary>
    public bool MouseTranslation { set; get; }

    private bool hScrollBarVisible = false;
    private bool vScrollBarVisible = false;

    /// <summary>
    /// スクロールバーが表示されているかどうかを取得する。読み取り専用.
    /// </summary>
    public bool ScrollBarVisible => hScrollBar.Visible || vScrollBarVisible;

    /// <summary>
    /// 現在のソース画像中の描画範囲 RectangleD　を得る。読み取り専用.
    /// </summary>
    public RectangleD DrawingArea => PseudoBitmap.GetDrawingArea(Center, Zoom, pictureBox.ClientSize);

    #endregion プロパティ

    /// <summary>
    /// centerの位置が適切かどうか(=はみ出していないか)チェックする
    /// </summary>
    /// <returns></returns>
    private void checkInvalidCenter()
    {
        if (_Center.IsNaN) return;
        if (hScrollBarVisible)//水平スクロールバーが出ているとき
        {
            if (!horizontalFlip)
            {
                if (hScrollBar.Maximum - hScrollBar.LargeChange + 1 < _Center.X)
                    _Center.X = hScrollBar.Maximum - hScrollBar.LargeChange + 1;
                else if (hScrollBar.Minimum > _Center.X)
                    _Center.X = hScrollBar.Minimum;
                hScrollBar.Value = (int)_Center.X;
            }
            else
            {
                if (hScrollBar.Maximum - hScrollBar.LargeChange + 1 < hScrollBar.Maximum - hScrollBar.Minimum - _Center.X)
                    _Center.X = -hScrollBar.Minimum + hScrollBar.LargeChange - 1;
                else if (hScrollBar.Minimum > hScrollBar.Maximum - hScrollBar.Minimum - _Center.X)
                    _Center.X = hScrollBar.Maximum - 2 * hScrollBar.Minimum;
                hScrollBar.Value = (int)(hScrollBar.Maximum - hScrollBar.Minimum - _Center.X);
            }
        }
        else
            _Center.X = (int)(PseudoBitmap.Width / 2.0 + 0.5);

        if (vScrollBarVisible)//垂直するロールバーが出ているとき
        {
            if (!VerticalFlip)
            {
                if (vScrollBar.Maximum - vScrollBar.LargeChange + 1 < _Center.Y)
                    _Center.Y = vScrollBar.Maximum - vScrollBar.LargeChange + 1;
                else if (vScrollBar.Minimum > _Center.Y)
                    _Center.Y = vScrollBar.Minimum;

                vScrollBar.Value = (int)_Center.Y;
            }
            else
            {
                if (vScrollBar.Maximum - vScrollBar.LargeChange < vScrollBar.Maximum - vScrollBar.Minimum - _Center.Y)
                    _Center.Y = -vScrollBar.Minimum + vScrollBar.LargeChange - 1;
                else if (vScrollBar.Minimum > vScrollBar.Maximum - vScrollBar.Minimum - _Center.Y)
                    _Center.Y = vScrollBar.Maximum - 2 * vScrollBar.Minimum;

                vScrollBar.Value = (int)(vScrollBar.Maximum - vScrollBar.Minimum - _Center.Y);
            }
        }
        else
            _Center.Y = (int)(PseudoBitmap.Height / 2.0 + 0.5);
    }

    public void drawPictureBox()
    {
        if (SkipEvent || SkipDrawing)
            return;


        if (PseudoBitmap != null)
        {
            if (hScrollBarVisible != hScrollBar.Visible) hScrollBar.Visible = hScrollBarVisible;
            if (vScrollBarVisible != vScrollBar.Visible) vScrollBar.Visible = vScrollBarVisible;
        }

        if (Draw != null)//Drawイベントを発生 このときは自前で描画せず、通知先に任せる。
        {
            Draw();
            this.Refresh();
            return;
        }

        if (PseudoBitmap == null || pictureBox.Size.Width == 0 || pictureBox.Size.Height == 0 || Center.IsNaN)
            return;
        pictureBox.Image = PseudoBitmap.GetImage(Center, Zoom, pictureBox.ClientSize);
        //if(pictureBox.Image !=null)
        // justBeforeImage = new Bitmap(pictureBox.Image);

        //if(showAreaRectangle)
        //    DrawAreaRectangle();

        this.Refresh();
    }

    #region マウスイベント
    private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
    {
        if (MouseWheel2 != null && MouseWheel2(this, e, ConvertToSrcPt(e.Location)))
            return;

        if (e.Delta > 0 && MouseScaling)
        {//縮小モード
            _Center = ConvertToSrcPt(e.Location);//イベントを起こさないように小文字のcenterに代入
            Zoom *= 0.5f;
        }
        else if (e.Delta < 0 && MouseScaling)
        {//拡大モード
            _Center = ConvertToSrcPt(e.Location);//イベントを起こさないように小文字のcenterに代入
            Zoom *= 2f;
        }
    }

    private Point justBeforePoint;

    private void pictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        pictureBox.Focus();

        if (MouseDown2 != null && MouseDown2(this, e, ConvertToSrcPt(e.Location)))
            return;

        if (e.Button == MouseButtons.Right && e.Clicks == 1 && MouseScaling)
        {
            MouseRangeMode = true;
            mouseRangeStart = e.Location;
        }
        else if (e.Button == MouseButtons.Right && e.Clicks == 2 && MouseScaling)
        {
            _Center = ConvertToSrcPt(e.Location);//イベントを起こさないように小文字のcenterに代入
            Zoom *= 0.5f;
        }
        else if (e.Button == MouseButtons.Left && e.Clicks == 1)
        {
            justBeforePoint = e.Location;
        }
    }
    

    private bool manualSpotMode = false;

    public bool ManualSpotMode
    {
        set { manualSpotMode = value; if (manualSpotMode) MouseRangeMode = false; }
        get { return manualSpotMode; }
    }

    public bool MouseRangeMode = false;
    private Point mouseRangeStart, mouseRangeEnd;

    private void pictureBox_MouseMove(object sender, MouseEventArgs e)
    {
        if (MouseMove2 != null && MouseMove2(this, e, ConvertToSrcPt(e.Location)))
            return;

        if (MouseRangeMode)
        {
            mouseRangeEnd = e.Location;
            pictureBox.Refresh();
        }
        else if (e.Button == MouseButtons.Left && MouseTranslation && justBeforePoint != e.Location)
        {
            Center = new PointD(Center.X + (justBeforePoint.X - e.Location.X) / Zoom, Center.Y + (justBeforePoint.Y - e.Location.Y) / Zoom);
            justBeforePoint = e.Location;
        }
    }

    private void pictureBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (MouseUp2 != null && MouseUp2(this, e, ConvertToSrcPt(e.Location)))
            return;

        if (MouseRangeMode && MouseScaling)
        {
            MouseRangeMode = false;
            mouseRangeEnd = e.Location;
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)//CTRLキーが押されていない場合
            {
                if (Math.Abs(mouseRangeStart.X - mouseRangeEnd.X) < 3 && Math.Abs(mouseRangeStart.Y - mouseRangeEnd.Y) < 3)
                {//縮小モード
                    PointD start = ConvertToSrcPt(mouseRangeStart);
                    PointD end = ConvertToSrcPt(mouseRangeEnd);
                    _Center = new PointD((start.X + end.X) / 2.0, (start.Y + end.Y) / 2.0);//イベントを起こさないように小文字のcenterに代入
                    Zoom *= 0.5f;
                }
                else if (Math.Abs(mouseRangeStart.X - mouseRangeEnd.X) > 10 && Math.Abs(mouseRangeStart.Y - mouseRangeEnd.Y) > 10)
                {//拡大モード
                    PointD start = ConvertToSrcPt(mouseRangeStart);
                    PointD end = ConvertToSrcPt(mouseRangeEnd);
                    _Center = new PointD((start.X + end.X) / 2.0, (start.Y + end.Y) / 2.0 + 0.5);//イベントを起こさないように小文字のcenterに代入
                    Zoom = ((float)pictureBox.Width / Math.Abs(start.X - end.X) + (float)pictureBox.Height / Math.Abs(start.Y - end.Y)) / 2.0f;
                }
                else
                    pictureBox.Refresh();
                return;
            }
            else//コントロールキーが押されていた場合
            {
                ShowAreaRectangle = false;
                if (Math.Abs(mouseRangeStart.X - mouseRangeEnd.X) > 10 && Math.Abs(mouseRangeStart.Y - mouseRangeEnd.Y) > 10)
                {
                    ShowAreaRectangle = true;
                    var start = ConvertToSrcPt(mouseRangeStart);
                    var end = ConvertToSrcPt(mouseRangeEnd);
                    AreaRectangle = new RectangleD(start, end);
                    MouseUp2?.Invoke(this, e, ConvertToSrcPt(e.Location));
                }
                pictureBox.Refresh();
                return;
            }
        }
    }

    private void pictureBox_MouseLeave(object sender, EventArgs e)
    {
        //drawPictureBox();
    }

    #endregion

    /// <summary>
    /// PictureBoxのPaintイベント
    /// </summary>
    public event PaintEventHandler Paint2;
    private void pictureBox_Paint(object sender, PaintEventArgs e)
    {
        Paint2?.Invoke(this, e);
        var g = e.Graphics;

        g.SmoothingMode = SmoothingMode.AntiAlias;

        if (MouseRangeMode)
        {
            var pen = new Pen(Brushes.Pink);
            if ((ModifierKeys & Keys.Control) == Keys.Control)
                pen = new Pen(Brushes.Yellow);
            pen.DashStyle = DashStyle.Dash;
            g.DrawRectangle(pen, Math.Min(mouseRangeStart.X, mouseRangeEnd.X), Math.Min(mouseRangeStart.Y, mouseRangeEnd.Y),
                Math.Abs(mouseRangeStart.X - mouseRangeEnd.X), Math.Abs(mouseRangeStart.Y - mouseRangeEnd.Y));
        }
        if (showFocusRectangle)
        {
            Brush b = new HatchBrush(HatchStyle.Percent10, Color.Green, Color.Transparent);
            g.FillRectangle(b, pictureBox.ClientRectangle);
        }
        if (ShowAreaRectangle)
        {
            var pen = new Pen(Brushes.Yellow);
            var rect = ConvertToClientRect(areaRentagle).ToRectangleF();
            g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        //drawSymbols(e)
        if (!this.DesignMode && Symbols != null)
            foreach (var s in Symbols)
            {
                if (s.SymbolVisible)
                {
                    var ff = new FontFamily("Arial");
                    if (s.Shape == SymbolShape.Cross || s.Shape == SymbolShape.CircleAndCross)
                    {
                        Pen pen1 = new(new SolidBrush(s.CrossColor1)), pen2 = new(new SolidBrush(s.CrossColor2));
                        pen1.Width = pen2.Width = s.Bold ? 2f : 1f;
                        var pt = ConvertToClientPt(s.CrossPosition).ToPointF();
                        if (pt.X > -pictureBox.ClientSize.Width && pt.X < 2 * pictureBox.ClientSize.Width && pt.Y > -pictureBox.ClientSize.Height && pt.Y < 2 * pictureBox.ClientSize.Height)
                        {
                            g.DrawLine(pen2, new PointF(pt.X - s.CrossSize + 1, pt.Y - s.CrossSize - 1), new PointF(pt.X + s.CrossSize + 1, pt.Y + s.CrossSize - 1));
                            g.DrawLine(pen2, new PointF(pt.X - s.CrossSize - 1, pt.Y - s.CrossSize + 1), new PointF(pt.X + s.CrossSize - 1, pt.Y + s.CrossSize + 1));
                            g.DrawLine(pen2, new PointF(pt.X + s.CrossSize - 1, pt.Y - s.CrossSize - 1), new PointF(pt.X - s.CrossSize - 1, pt.Y + s.CrossSize - 1));
                            g.DrawLine(pen2, new PointF(pt.X + s.CrossSize + 1, pt.Y - s.CrossSize + 1), new PointF(pt.X - s.CrossSize + 1, pt.Y + s.CrossSize + 1));

                            g.DrawLine(pen1, new PointF(pt.X - s.CrossSize, pt.Y - s.CrossSize), new PointF(pt.X + s.CrossSize, pt.Y + s.CrossSize));
                            g.DrawLine(pen1, new PointF(pt.X + s.CrossSize, pt.Y - s.CrossSize), new PointF(pt.X - s.CrossSize, pt.Y + s.CrossSize));
                            pen1.Width = pen2.Width = 1f;
                            if (s.LabelVisible)
                            {
                                var gp = new GraphicsPath();
                                gp.AddString(s.Label, ff, (int)FontStyle.Bold, s.Bold ? 18f : 16f, new PointF(pt.X + 5, pt.Y + 5), StringFormat.GenericDefault);

                                if (s.Bold)
                                {
                                    g.FillPath(new SolidBrush(s.CrossColor2), gp);
                                    g.DrawPath(pen1, gp);
                                }
                                else
                                {
                                    g.FillPath(new SolidBrush(s.CrossColor1), gp);
                                    g.DrawPath(pen2, gp);
                                }
                            }
                        }
                    }
                    if (s.Shape == SymbolShape.Circle || s.Shape == SymbolShape.CircleAndCross)
                    {
                        var pen1 = new Pen(Color.FromArgb(128, s.CircleColor)) { Width = s.Bold ? 2f : 1f };

                        var pt = ConvertToClientPt(s.CircleCenter).ToPointF();
                        if (pt.X > -pictureBox.ClientSize.Width && pt.X < 2 * pictureBox.ClientSize.Width && pt.Y > -pictureBox.ClientSize.Height && pt.Y < 2 * pictureBox.ClientSize.Height)
                        {
                            var rect = ConvertToClientRect(new RectangleD(s.CircleCenter.X - s.CircleRadius, s.CircleCenter.Y - s.CircleRadius, s.CircleRadius * 2, s.CircleRadius * 2)).ToRectangleF();
                            g.DrawEllipse(pen1, rect);
                        }
                    }
                    if (s.Shape == SymbolShape.Line)
                    {
                        var pen1 = new Pen(Color.FromArgb(128, s.LineColor)) { Width = 2f };
                        var pt1 = ConvertToClientPt(s.LinePosition1).ToPointF();
                        var pt2 = ConvertToClientPt(s.LinePosition2).ToPointF();
                        if (pt1.X > -pictureBox.ClientSize.Width && pt1.X < 2 * pictureBox.ClientSize.Width && pt1.Y > -pictureBox.ClientSize.Height && pt1.Y < 2 * pictureBox.ClientSize.Height)
                        {
                            g.DrawLine(pen1, pt1, pt2);
                        }
                    }
                }
            }
        /*
                    if (ShowSpots1)
                        drawSpots(e, Spots1, Spots1Label,Brushes.Pink, Brushes.DarkRed, ShowSpots1Label, EmphasizeNumberOfSpots1);

                    if (ShowSpots2)
                        drawSpots(e, Spots2, Spots2Label, Brushes.LightBlue, Brushes.DarkBlue, ShowSpots2Label, EmphasizeNumberOfSpots2);

                    if (ShowSpots3)
                        drawSpots(e, Spots3, Spots3Label, Brushes.LightGreen, Brushes.DarkGreen, ShowSpots3Label, EmphasizeNumberOfSpots3);
          */

    }

    private void drawSymbols(PaintEventArgs e, List<PointD> spot, List<string> spotLabel, Brush brush1, Brush brush2, bool showLabel, int? emphasizeNum)
    {
        var gp = new GraphicsPath();
        FontFamily ff = new FontFamily("Arial");
        Pen pen1 = new(brush1), pen2 = new(brush2);
        if (spot != null && spot.Count > 0)
            for (int i = 0; i < spot.Count; i++)
            {
                pen1.Width = pen2.Width = i == emphasizeNum ? 2f : 1f;

                PointF pt = ConvertToClientPt(spot[i]).ToPointF();

                e.Graphics.DrawLine(pen2, new PointF(pt.X - 4, pt.Y - 6), new PointF(pt.X + 6, pt.Y + 4));
                e.Graphics.DrawLine(pen2, new PointF(pt.X - 6, pt.Y - 4), new PointF(pt.X + 4, pt.Y + 6));
                e.Graphics.DrawLine(pen2, new PointF(pt.X + 4, pt.Y - 6), new PointF(pt.X - 6, pt.Y + 4));
                e.Graphics.DrawLine(pen2, new PointF(pt.X + 6, pt.Y - 4), new PointF(pt.X - 4, pt.Y + 6));

                e.Graphics.DrawLine(pen1, new PointF(pt.X - 5, pt.Y - 5), new PointF(pt.X + 5, pt.Y + 5));
                e.Graphics.DrawLine(pen1, new PointF(pt.X + 5, pt.Y - 5), new PointF(pt.X - 5, pt.Y + 5));
                pen1.Width = pen2.Width = 1f;
                if (showLabel)
                {
                    gp = new GraphicsPath();
                    string label = spotLabel == null || spotLabel.Count != spot.Count ? i.ToString("000") : spotLabel[i];
                    gp.AddString(label, ff, (int)FontStyle.Bold, i == emphasizeNum ? 18f : 16f, new PointF(pt.X + 5, pt.Y + 5), StringFormat.GenericDefault);

                    if (i == emphasizeNum)
                    {
                        e.Graphics.FillPath(brush2, gp);
                        e.Graphics.DrawPath(pen1, gp);
                    }
                    else
                    {
                        e.Graphics.FillPath(brush1, gp);
                        e.Graphics.DrawPath(pen2, gp);
                    }
                }
            }
    }

    /// /// <summary>
    /// コントロール全体のペイントイベント
    /// </summary>
    public event PaintEventHandler PaintControl;

    private void ScalablePictureBox_Paint(object sender, PaintEventArgs e)
    {
        PaintControl?.Invoke(sender, e);
    }

    /// <summary>
    /// コントロール全体がリサイズされたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ScalablePictureBox_Resize(object sender, EventArgs e)
    {
        if (PseudoBitmap != null)//イメージが存在するときは
        {
            ResetMinimumZoomValue();
            if (Zoom < minZoom)
                Zoom = minZoom;
            setControlLayout();
            drawPictureBox();
        }
    }

    public void ResetMinimumZoomValue()
    {
        minZoom = Math.Min((double)(this.ClientSize.Width - 1) / PseudoBitmap.Width, (double)(this.ClientSize.Height - 1) / PseudoBitmap.Height);
    }

    #region イベント

    /// <summary>
    /// マウスイベント用のデリゲート
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">e</param>
    /// <param name="pt">ソース画像座標</param>
    /// <returns>その後のイベントをキャンセルする場合: true</returns>
    public delegate bool MouseEvent(object sender, MouseEventArgs e, PointD pt);

    public event MouseEvent MouseMove2;

    public event MouseEvent MouseUp2;

    public event MouseEvent MouseDown2;

    public event MouseEvent MouseWheel2;

    public delegate void DrawEvent();

    public event DrawEvent Draw;

    public delegate void DrawingAreaChangedEvent(object sender, double zoom, PointD center);

    public event DrawingAreaChangedEvent DrawingAreaChanged;

    #endregion イベント

    /// <summary>
    /// 初期化直後、画面リサイズ後、Zoom変更後にPictureBox,ScrollBarコントロールなどを適切に配置するメソッド
    /// </summary>
    private void setControlLayout()
    {
        hScrollBar.Location = new Point(0, this.ClientSize.Height - hScrollBar.Height);
        hScrollBar.Size = new Size(this.ClientSize.Width - vScrollBar.Width, hScrollBar.Height);

        vScrollBar.Location = new Point(this.ClientSize.Width - vScrollBar.Width, 0);
        vScrollBar.Size = new Size(vScrollBar.Width, this.ClientSize.Height - hScrollBar.Height);

        if (this.ClientSize.Width <= 0 || this.ClientSize.Height <= 0) return;
        //クライアントサイズ内に収まる時
        if (this.ClientSize.Width >= PseudoBitmap.Width * _Zoom && this.ClientSize.Height >= PseudoBitmap.Height * _Zoom)
        {
            hScrollBarVisible = vScrollBarVisible = false;
            pictureBox.Location = new Point((int)((this.ClientSize.Width - PseudoBitmap.Width * _Zoom) / 2.0), (int)((this.ClientSize.Height - PseudoBitmap.Height * _Zoom) / 2.0));
            pictureBox.Width = (int)(PseudoBitmap.Width * _Zoom);
            pictureBox.Height = (int)(PseudoBitmap.Height * _Zoom);
        }
        //横スクロールだけ出すとき
        else if (this.ClientSize.Width <= PseudoBitmap.Width * _Zoom && this.ClientSize.Height - hScrollBar.Height > PseudoBitmap.Height * _Zoom)
        {
            hScrollBarVisible = true;
            vScrollBarVisible = false;

            pictureBox.Location = new Point(0, (int)((this.ClientSize.Height - hScrollBar.Height - PseudoBitmap.Height * _Zoom) / 2.0 + 0.5));
            pictureBox.Width = this.ClientSize.Width;
            pictureBox.Height = (int)(PseudoBitmap.Height * _Zoom + 0.5);

            hScrollBar.LargeChange = (int)(this.ClientSize.Width / _Zoom + 0.5);//移動量
            hScrollBar.Maximum = (int)(PseudoBitmap.Width - this.ClientSize.Width / 2.0 / _Zoom + 0.5) + hScrollBar.LargeChange;//center.Xの上限値
            hScrollBar.Minimum = (int)(this.ClientSize.Width / 2.0 / _Zoom + 0.5);//center.Xの下限値
        }
        //縦スクロールだけ出すとき
        else if (this.ClientSize.Width - vScrollBar.Width > PseudoBitmap.Width * _Zoom && this.ClientSize.Height <= PseudoBitmap.Height * _Zoom)
        {
            hScrollBarVisible = false;
            vScrollBarVisible = true;

            pictureBox.Location = new Point((int)((this.ClientSize.Width - vScrollBar.Width - PseudoBitmap.Width * _Zoom) / 2.0), 0);
            pictureBox.Width = (int)(PseudoBitmap.Width * _Zoom);
            pictureBox.Height = this.Height;

            vScrollBar.LargeChange = (int)(this.ClientSize.Height / _Zoom + 0.5);
            vScrollBar.Maximum = (int)(PseudoBitmap.Height - this.ClientSize.Height / 2.0 / _Zoom + 0.5) + vScrollBar.LargeChange;
            vScrollBar.Minimum = (int)(this.Height / 2.0 / _Zoom + 0.5);
        }
        //両方のスクロールを出さなければいけないとき
        else
        {
            hScrollBarVisible = vScrollBarVisible = true;

            pictureBox.Location = new Point(0, 0);
            pictureBox.Width = this.ClientSize.Width - vScrollBar.Width;
            pictureBox.Height = this.ClientSize.Height - hScrollBar.Height;

            int hLargeChange = (int)((this.ClientSize.Width - vScrollBar.Width) / _Zoom + 0.5);

            hScrollBar.Maximum = (int)(PseudoBitmap.Width - (this.ClientSize.Width - vScrollBar.Width) / 2.0 / _Zoom + 0.5) + hLargeChange - 1;//center.Xの上限値
            hScrollBar.Minimum = (int)((this.ClientSize.Width - vScrollBar.Width) / 2.0 / _Zoom);//center.Xの下限値
            if (hLargeChange > 0)
                hScrollBar.LargeChange = hLargeChange;//移動量

            int vLargeChange = (int)((this.ClientSize.Height - hScrollBar.Height) / _Zoom + 0.5);
            vScrollBar.Maximum = (int)(PseudoBitmap.Height - (this.ClientSize.Height - hScrollBar.Height) / 2.0 / _Zoom + 0.5) + vLargeChange - 1;
            vScrollBar.Minimum = (int)((this.ClientSize.Height - hScrollBar.Height) / 2.0 / _Zoom);
            if (vLargeChange > 0)
                vScrollBar.LargeChange = vLargeChange;
        }
    }

    private bool skipScrollBarEvent = false;

    private void vScrollBar_ValueChanged(object sender, EventArgs e)
    {
        if (skipScrollBarEvent) return;
        if (_Center.Y != vScrollBar.Value)
        {
            if (!VerticalFlip)
                Center = new PointD(_Center.X, vScrollBar.Value);
            else
                Center = new PointD(_Center.X, vScrollBar.Maximum - vScrollBar.Minimum - vScrollBar.Value);
        }
    }

    private void hScrollBar_ValueChanged(object sender, EventArgs e)
    {
        if (skipScrollBarEvent) return;
        if (_Center.X != hScrollBar.Value)
        {
            if (!horizontalFlip)
                Center = new PointD(hScrollBar.Value, _Center.Y);
            else
                Center = new PointD(hScrollBar.Maximum - hScrollBar.Minimum - hScrollBar.Value, _Center.Y);
        }
    }

    private void SpecialPictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        //範囲外をクリックされると最低倍率に戻す
        Zoom = minZoom;
    }

    #region 座標変換関連

    /// <summary>
    /// クライアントのPointをソースのPointに変換
    /// </summary>
    /// <param name="clientPt"></param>
    /// <returns></returns>
    public PointD ConvertToSrcPt(Point clientPt)
    { return ConvertToSrcPt(new PointD(clientPt.X, clientPt.Y)); }

    /// <summary>
    /// クライアントのPointDをソースのPointDに変換
    /// </summary>
    /// <param name="clientPt"></param>
    /// <returns></returns>
    public PointD ConvertToSrcPt(PointD clientPt)
    {
        double x, y;
        if (!HorizontalFlip)
            x = (clientPt.X - pictureBox.ClientSize.Width / 2.0) / _Zoom + _Center.X;
        else
            x = -(clientPt.X - pictureBox.ClientSize.Width / 2.0) / _Zoom + _Center.X;

        if (!VerticalFlip)
            y = (clientPt.Y - pictureBox.ClientSize.Height / 2.0) / _Zoom + _Center.Y;
        else
            y = -(clientPt.Y - pictureBox.ClientSize.Height / 2.0) / _Zoom + _Center.Y;

        return new PointD(x, y);
    }

    /// <summary>
    /// クライアントのRectangleをSrcのRectangleに変換
    /// </summary>
    /// <param name="clientRect"></param>
    /// <returns></returns>
    public RectangleD ConvertToSrcRect(Rectangle clientRect)
    { return ConvertToSrcRect(new RectangleD(clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height)); }

    /// <summary>
    /// クライアントのRectangleDをSrcのRectangleDに変換
    /// </summary>
    /// <param name="clientRect"></param>
    /// <returns></returns>
    public RectangleD ConvertToSrcRect(RectangleD clientRect)
    {
        PointD ul = ConvertToSrcPt(new PointD(clientRect.X, clientRect.Y));
        PointD lr = ConvertToSrcPt(new PointD(clientRect.X + clientRect.Width, clientRect.Y + clientRect.Height));
        return new RectangleD(ul, new SizeD(lr.X - ul.X, lr.Y - ul.Y));
    }

    /// <summary>
    /// ソースのPointDをクライアントのPointDに変換
    /// </summary>
    /// <param name="srcPt"></param>
    /// <returns></returns>
    public PointD ConvertToClientPt(PointD srcPt)
    {
        return new PointD(((srcPt.X - _Center.X) * Zoom + pictureBox.ClientSize.Width / 2.0f), ((srcPt.Y - _Center.Y) * Zoom + pictureBox.ClientSize.Height / 2.0f));
    }

    /// <summary>
    /// ソースのRectangleDをクライアントのRectangleDに変換
    /// </summary>
    /// <param name="srcRect"></param>
    /// <returns></returns>
    public RectangleD ConvertToClientRect(RectangleD srcRect)
    {
        PointD ul = ConvertToClientPt(new PointD(srcRect.X, srcRect.Y));
        PointD lr = ConvertToClientPt(new PointD(srcRect.X + srcRect.Width, srcRect.Y + srcRect.Height));
        return new RectangleD(ul, new SizeD(lr.X - ul.X, lr.Y - ul.Y));
    }

    #endregion 座標変換関連

    private void vScrollBar_VisibleChanged(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 現在表示している画像をBitmapクラスで返す
    /// </summary>
    /// <returns></returns>
    public Bitmap GetBitmapImage()
    {
        var bmp = new Bitmap(pictureBox.Image.Width, pictureBox.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        var g = Graphics.FromImage(bmp);
        g.DrawImage(pictureBox.Image, new Point(0, 0));

        pictureBox_Paint(new object(), new PaintEventArgs(g, pictureBox.ClientRectangle));
        return bmp;
    }

    public void SaveAsPNG()
    {
        var dlg = new SaveFileDialog() { Filter = "*.png|*.png" };
        if(dlg.ShowDialog()== DialogResult.OK)
            GetBitmapImage().Save(dlg.FileName);
    }

    public void CopyAsBitmap()
    {
        Clipboard.SetDataObject(GetBitmapImage(),true);
    }

    public void SaveAsMetafile()
    {
        metafile(true);
    }

    public void CopyAsMetafile()
    {
        metafile(false);
    }

    private void metafile(bool save)
    {
        using Graphics grfx = CreateGraphics();
        IntPtr ipHdc = grfx.GetHdc();
        MemoryStream ms = new();
        Metafile mf = new(ms, ipHdc, EmfType.EmfPlusDual);
        grfx.ReleaseHdc(ipHdc);
        grfx.Dispose();
        var g = Graphics.FromImage(mf);

        var destRect = new RectangleF(0, 0, pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
        //var srcRect = PseudoBitmap.GetDrawingArea(Center, Zoom, pictureBox.ClientSize).ToRectangleF();
        //var image = PseudoBitmap.GetImage();
        //g.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);

        var srcRect = PseudoBitmap.GetDrawingArea(Center, Zoom, pictureBox.ClientSize);
        var image = PseudoBitmap.GetImage(srcRect, srcRect.ToSize());
        g.DrawImage(image, destRect);

        pictureBox_Paint(new object(), new PaintEventArgs(g, pictureBox.ClientRectangle));

        g.Dispose();

        if (save)
        {
            SaveFileDialog dlg = new() { Filter = "*.emf|*.emf" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fsm = new(dlg.FileName, FileMode.Create, FileAccess.Write);
                fsm.Write(ms.GetBuffer(), 0, (int)ms.Length);
                fsm.Close();
            }
        }
        else
            ClipboardMetafileHelper.PutEnhMetafileOnClipboard(this.Handle, mf);
    }







    //このコントロールがフォーカスを浴びた時の処理
    private void ScalablePictureBox_Enter(object sender, EventArgs e)
    {
        if (FocusEventEnabled)
            ShowRimRentangle = true;
    }

    private void ScalablePictureBox_Leave(object sender, EventArgs e)
    {
        if (FocusEventEnabled)
            ShowRimRentangle = false;
    }
}
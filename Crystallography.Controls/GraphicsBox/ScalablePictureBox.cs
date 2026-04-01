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
public partial class ScalablePictureBox : CaptureUserControlBase
{
    private const string SymbolFontName = "Arial"; // (260322Ch) overlay 描画で使うフォント名を一箇所へ集約する
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

        /// <summary>Circleのコンストラクタ</summary>
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


        /// <summary>Crossのコンストラクタ</summary>
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

        /// <summary>Lineのコンストラクタ</summary>
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

    /// <summary>VisualStudioデザイナーの編集の時はTrue</summary>
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

    /// <summary>上下方向の反転をするかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool VerticalFlip
    {
        // set { PseudoBitmap.VerticalFlip = value; drawPictureBox(); } // (260322Ch) 旧コード: 再描画だけ行い scrollbar 側の状態同期は呼んでいなかった
        set
        {
            PseudoBitmap.VerticalFlip = value; // (260322Ch) flip 状態は PseudoBitmap を正とする
            ApplyDrawingAreaChange(updateLayout: false, raiseEvent: false); // (260322Ch) 表示反転後も scrollbar と描画範囲を同期する
        }
        get => PseudoBitmap.VerticalFlip;
    }

    // private bool horizontalFlip = false; // (260322Ch) 旧コード: HorizontalFlip をローカル field と PseudoBitmap で二重管理していた

    /// <summary>上下方向の反転をするかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool HorizontalFlip
    {
        // set { PseudoBitmap.HorizontalFlip = value; drawPictureBox(); } // (260322Ch) 旧コード: scrollbar 計算に使う状態と setter の更新先が分かれていた
        set
        {
            PseudoBitmap.HorizontalFlip = value; // (260322Ch) flip 状態を PseudoBitmap 側へ一本化する
            ApplyDrawingAreaChange(updateLayout: false, raiseEvent: false); // (260322Ch) 反転後の scrollbar 値も合わせて更新する
        }
        get => PseudoBitmap.HorizontalFlip;
    }

    private double minZoom = 0.1f;
    private readonly double maxZoom = 128f;

    private double _Zoom = 1;

    /// <summary>表示倍率</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public double Zoom
    {
        set
        {
            if (_Zoom != value)
            {
                _Zoom = ClampZoomValue(value);// (260322Ch) clamp の責務を共通化する
                ApplyDrawingAreaChange(updateLayout: true, raiseEvent: true); // (260322Ch) Zoom/Center 更新後の共通反映処理へ集約する
            }
        }
        get { return _Zoom; }
    }

    private PointD _Center = new(double.NaN, double.NaN);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    /// <summary>中心ピクセル</summary>
    public PointD Center
    {
        set
        {
            if (!value.IsNaN)
            {
                _Center = value;
                ApplyDrawingAreaChange(updateLayout: false, raiseEvent: true); // (260322Ch) Center 更新後の反映処理を共通化する
            }
        }
        get => _Center.IsNaN ? new PointD(0, 0) : _Center;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public (double Zoom, PointD Center) ZoomAndCenter
    {
        set
        {
            if (FixZoomAndCenter) return;
            _Zoom = ClampZoomValue(value.Zoom); // (260322Ch) zoom clamp のロジックを setter 間で揃える
            _Center = new PointD(value.Center);
            ApplyDrawingAreaChange(updateLayout: true, raiseEvent: true); // (260322Ch) ZoomAndCenter 反映も共通経路へ寄せる
        }
        get { return (Zoom, new PointD(Center.X, Center.Y)); }
    }

    /// <summary>ZoomやCenter位置を固定するかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool FixZoomAndCenter { get; set; } = false;

    public Size CanvasSize { get { return pictureBox.ClientSize; } }

    /// <summary>フォーカスイベント(Enter)を有効にするかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool FocusEventEnabled { get; set; } = false;

    private bool showFocusRectangle = false;

    /// <summary>外枠を表示する</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
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

    /// <summary>AreaRectangleで指定した矩形を表示するかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
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
    private RectangleD areaRentagle = new();
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RectangleD AreaRectangle
    {
        set { areaRentagle = value; this.Refresh(); }
        get => areaRentagle;
    }



    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<Symbol> Symbols { set; get; } = [];

    /// <summary>左上に表示するテキスト</summary>
    public override string Text { get => label.Text; set => label.Text = value; }

    /// <summary>左上に表示するテキストの色</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color TextColor { get => label.ForeColor; set => label.ForeColor = value; }

    private PseudoBitmap _pseudoBitmap = new();

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

    /// <summary>マウスによるスケーリングが可能かどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool MouseScaling { set; get; }//マウスによるスケーリングが可能かどうか

    /// <summary>マウスによる平行移動が可能かどうか </summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool MouseTranslation { set; get; }

    private bool hScrollBarVisible = false;
    private bool vScrollBarVisible = false;

    /// <summary>クライアント領域の左上にタイトルを表示するか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool TitleVisible { get; set; } = false;
    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public (string Text, Font Font, Color Color1, Color Color2) Title { get; set; }


    #endregion プロパティ

    /// <summary>スクロールバーが表示されているかどうかを取得する。読み取り専用.</summary>
    public bool ScrollBarVisible => hScrollBar.Visible || vScrollBarVisible;

    /// <summary>現在のソース画像中の描画範囲 RectangleD　を得る。読み取り専用.</summary>
    public RectangleD DrawingArea => PseudoBitmap.GetDrawingArea(Center, Zoom, pictureBox.ClientSize);

    private double ClampZoomValue(double value)
        => Math.Min(maxZoom, Math.Max(minZoom, value)); // (260322Ch) Zoom の上下限判定を一箇所で扱う

    private void ExecuteWithoutScrollBarEvents(Action action)
    {
        skipScrollBarEvent = true; // (260322Ch) scrollbar 同期中の再入を抑止する
        try
        {
            action?.Invoke();
        }
        finally
        {
            skipScrollBarEvent = false;
        }
    }

    private void ApplyDrawingAreaChange(bool updateLayout, bool raiseEvent)
    {
        if (hScrollBar == null || vScrollBar == null || _pseudoBitmap == null)
            return;

        ExecuteWithoutScrollBarEvents(() =>
        {
            if (updateLayout)
                setControlLayout(); // (260322Ch) Zoom 変化時だけ layout 計算を更新する
            checkInvalidCenter(); // (260322Ch) Center / flip / scrollbar の整合を共通で保つ
        });

        drawPictureBox();

        if (raiseEvent && !SkipEvent)
            DrawingAreaChanged?.Invoke(this, Zoom, Center); // (260322Ch) 反映後の通知を一箇所へ集約する
    }

    private void UpdateScrollBarVisibility()
    {
        if (PseudoBitmap == null)
            return;

        if (hScrollBarVisible != hScrollBar.Visible)
            hScrollBar.Visible = hScrollBarVisible; // (260322Ch) visible state の差分だけを反映する
        if (vScrollBarVisible != vScrollBar.Visible)
            vScrollBar.Visible = vScrollBarVisible;
    }

    private Image GetCurrentPictureImage()
    {
        if (pictureBox.Image != null)
            return pictureBox.Image; // (260322Ch) 画面上にある最新描画を優先して export に使う

        if (PseudoBitmap == null || pictureBox.ClientSize.Width == 0 || pictureBox.ClientSize.Height == 0 || Center.IsNaN)
            return null;

        return PseudoBitmap.GetImage(Center, Zoom, pictureBox.ClientSize); // (260322Ch) 画面描画前でも現在条件から再構成できるようにする
    }

    private void PaintPictureBoxOverlay(Graphics graphics)
    {
        using var args = new PaintEventArgs(graphics, pictureBox.ClientRectangle); // (260322Ch) export 時も通常 Paint と同じ overlay を流用する
        pictureBox_Paint(this, args);
    }

    private Bitmap CreateSnapshotBitmap()
    {
        var width = Math.Max(1, pictureBox.ClientSize.Width);
        var height = Math.Max(1, pictureBox.ClientSize.Height);
        var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        using var graphics = Graphics.FromImage(bitmap);

        graphics.Clear(pictureBox.BackColor); // (260322Ch) 画像未設定時でも export 背景を一定にする
        if (GetCurrentPictureImage() is Image image)
            graphics.DrawImage(image, new Rectangle(Point.Empty, new Size(width, height)));
        PaintPictureBoxOverlay(graphics);

        return bitmap;
    }

    private bool IsPointInExtendedClientArea(PointF pt)
        => pt.X > -pictureBox.ClientSize.Width
        && pt.X < 2 * pictureBox.ClientSize.Width
        && pt.Y > -pictureBox.ClientSize.Height
        && pt.Y < 2 * pictureBox.ClientSize.Height; // (260322Ch) overlay 描画の可視判定を共通化する



    /// <summary>centerの位置が適切かどうか(=はみ出していないか)チェックする</summary>
    /// <returns></returns>
    private void checkInvalidCenter()
    {
        if (_Center.IsNaN) return;
        if (hScrollBarVisible)//水平スクロールバーが出ているとき
        {
            // if (!horizontalFlip) // (260322Ch) 旧コード: local field 参照
            if (!HorizontalFlip)
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


        UpdateScrollBarVisibility(); // (260322Ch) scrollbar visible の同期を helper に集約する

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

        // if (e.Delta > 0 && MouseScaling)
        // {//縮小モード
        //     _Center = ConvertToSrcPt(e.Location);//イベントを起こさないように小文字のcenterに代入
        //     Zoom *= 0.5f;
        // }
        // else if (e.Delta < 0 && MouseScaling)
        // {//拡大モード
        //     _Center = ConvertToSrcPt(e.Location);//イベントを起こさないように小文字のcenterに代入
        //     Zoom *= 2f;
        // }
        if (e.Delta > 0 && MouseScaling)
        {//拡大モード (260322Ch) ホイール上回転でズームインするように長年の向きを反転
            _Center = ConvertToSrcPt(e.Location);//イベントを起こさないように小文字のcenterに代入
            Zoom *= 2f;
        }
        else if (e.Delta < 0 && MouseScaling)
        {//縮小モード (260322Ch) ホイール下回転でズームアウト
            _Center = ConvertToSrcPt(e.Location);//イベントを起こさないように小文字のcenterに代入
            Zoom *= 0.5f;
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
        else if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle) && e.Clicks == 1)
        {
            justBeforePoint = e.Location;
        }
    }


    private bool manualSpotMode = false;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
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
        else if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle) && MouseTranslation && justBeforePoint != e.Location)
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

    /// <summary>PictureBoxのPaintイベント</summary>
    public event PaintEventHandler Paint2;
    private void pictureBox_Paint(object sender, PaintEventArgs e)
    {
        Paint2?.Invoke(this, e);
        var g = e.Graphics;

        g.SmoothingMode = SmoothingMode.AntiAlias;

        if (MouseRangeMode)
        {
            using var pen = new Pen((ModifierKeys & Keys.Control) == Keys.Control ? Color.Yellow : Color.Pink); // (260322Ch) disposable Pen を Paint スコープ内で閉じる
            pen.DashStyle = DashStyle.Dash;
            g.DrawRectangle(pen, Math.Min(mouseRangeStart.X, mouseRangeEnd.X), Math.Min(mouseRangeStart.Y, mouseRangeEnd.Y),
                Math.Abs(mouseRangeStart.X - mouseRangeEnd.X), Math.Abs(mouseRangeStart.Y - mouseRangeEnd.Y));
        }
        if (showFocusRectangle)
        {
            using Brush b = new HatchBrush(HatchStyle.Percent10, Color.Green, Color.Transparent);
            g.FillRectangle(b, pictureBox.ClientRectangle);
        }
        if (ShowAreaRectangle)
        {
            using var pen = new Pen(Color.Yellow);
            var rect = ConvertToClientRect(areaRentagle).ToRectangleF();
            g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        //drawSymbols(e)
        if (!DesignMode)
        {
            using var symbolFontFamily = new FontFamily(SymbolFontName); // (260322Ch) symbol label 用 FontFamily を Paint ごとに再利用する
            if (Symbols != null)
                foreach (var s in Symbols)
                {
                    if (s.SymbolVisible)
                    {
                        if (s.Shape == SymbolShape.Cross || s.Shape == SymbolShape.CircleAndCross)
                        {
                            using var brush1 = new SolidBrush(s.CrossColor1);
                            using var brush2 = new SolidBrush(s.CrossColor2);
                            using Pen pen1 = new(brush1), pen2 = new(brush2);
                            pen1.Width = pen2.Width = s.Bold ? 2f : 1f;
                            var pt = ConvertToClientPt(s.CrossPosition).ToPointF();
                            if (IsPointInExtendedClientArea(pt))
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
                                    using var gp = new GraphicsPath();
                                    gp.AddString(s.Label, symbolFontFamily, (int)FontStyle.Bold, s.Bold ? 18f : 16f, new PointF(pt.X + 5, pt.Y + 5), StringFormat.GenericDefault);

                                    if (s.Bold)
                                    {
                                        g.FillPath(brush2, gp);
                                        g.DrawPath(pen1, gp);
                                    }
                                    else
                                    {
                                        g.FillPath(brush1, gp);
                                        g.DrawPath(pen2, gp);
                                    }
                                }
                            }
                        }
                        if (s.Shape == SymbolShape.Circle || s.Shape == SymbolShape.CircleAndCross)
                        {
                            using var pen1 = new Pen(Color.FromArgb(128, s.CircleColor)) { Width = s.Bold ? 2f : 1f };

                            var pt = ConvertToClientPt(s.CircleCenter).ToPointF();
                            if (IsPointInExtendedClientArea(pt))
                            {
                                var rect = ConvertToClientRect(new RectangleD(s.CircleCenter.X - s.CircleRadius, s.CircleCenter.Y - s.CircleRadius, s.CircleRadius * 2, s.CircleRadius * 2)).ToRectangleF();
                                g.DrawEllipse(pen1, rect);
                            }
                        }
                        if (s.Shape == SymbolShape.Line)
                        {
                            using var pen1 = new Pen(Color.FromArgb(128, s.LineColor)) { Width = 2f };
                            var pt1 = ConvertToClientPt(s.LinePosition1).ToPointF();
                            var pt2 = ConvertToClientPt(s.LinePosition2).ToPointF();
                            if (IsPointInExtendedClientArea(pt1))
                            {
                                g.DrawLine(pen1, pt1, pt2);
                            }
                        }
                    }
                }

            if (TitleVisible)
            {
                var ff = Title.Font.FontFamily;
                using var gp = new GraphicsPath();
                gp.AddString(Title.Text, ff, (int)FontStyle.Bold, Title.Font.Size, new PointF(0, 0), StringFormat.GenericDefault);

                using var brush = new SolidBrush(Title.Color1);
                using var pen = new Pen(Title.Color2, 1f);
                g.FillPath(brush, gp);
                g.DrawPath(pen, gp);


                //g.DrawString(Title.Text, Title.Font, Title.Brush, new Point(0, 0));

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
        using var ff = new FontFamily(SymbolFontName); // (260322Ch) 旧 helper も同じ FontFamily 共通定数を使う
        using Pen pen1 = new(brush1), pen2 = new(brush2);
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
                    using var gp = new GraphicsPath();
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

    /// /// <summary>コントロール全体のペイントイベント</summary>
    public event PaintEventHandler PaintControl;

    private void ScalablePictureBox_Paint(object sender, PaintEventArgs e) => PaintControl?.Invoke(sender, e);

    /// <summary>コントロール全体がリサイズされたとき</summary>
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

    /// <summary>マウスイベント用のデリゲート</summary>
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

    /// <summary>初期化直後、画面リサイズ後、Zoom変更後にPictureBox,ScrollBarコントロールなどを適切に配置するメソッド</summary>
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
            if (!HorizontalFlip)
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

    /// <summary>クライアントのPointをソースのPointに変換</summary>
    /// <param name="clientPt"></param>
    /// <returns></returns>
    public PointD ConvertToSrcPt(Point clientPt)
    { return ConvertToSrcPt(new PointD(clientPt.X, clientPt.Y)); }

    /// <summary>クライアントのPointDをソースのPointDに変換</summary>
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

    /// <summary>クライアントのRectangleをSrcのRectangleに変換</summary>
    /// <param name="clientRect"></param>
    /// <returns></returns>
    public RectangleD ConvertToSrcRect(Rectangle clientRect)
    { return ConvertToSrcRect(new RectangleD(clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height)); }

    /// <summary>クライアントのRectangleDをSrcのRectangleDに変換</summary>
    /// <param name="clientRect"></param>
    /// <returns></returns>
    public RectangleD ConvertToSrcRect(RectangleD clientRect)
    {
        PointD ul = ConvertToSrcPt(new PointD(clientRect.X, clientRect.Y));
        PointD lr = ConvertToSrcPt(new PointD(clientRect.X + clientRect.Width, clientRect.Y + clientRect.Height));
        return new RectangleD(ul, new SizeD(lr.X - ul.X, lr.Y - ul.Y));
    }

    /// <summary>ソースのPointDをクライアントのPointDに変換</summary>
    /// <param name="srcPt"></param>
    /// <returns></returns>
    public PointD ConvertToClientPt(PointD srcPt)
    {
        return new PointD(((srcPt.X - _Center.X) * Zoom + pictureBox.ClientSize.Width / 2.0f), ((srcPt.Y - _Center.Y) * Zoom + pictureBox.ClientSize.Height / 2.0f));
    }

    /// <summary>ソースのRectangleDをクライアントのRectangleDに変換</summary>
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

    /// <summary>現在表示している画像をBitmapクラスで返す</summary>
    /// <returns></returns>
    public Bitmap GetBitmapImage()
    {
        return CreateSnapshotBitmap(); // (260322Ch) 画面 export 用の bitmap 生成を共通 helper へ集約する
    }

    public void SaveAsPNG()
    {
        using var dlg = new SaveFileDialog() { Filter = "*.png|*.png" };
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            using var bmp = GetBitmapImage();
            bmp.Save(dlg.FileName); // (260322Ch) 保存専用の bitmap は即座に破棄する
        }
    }

    public void CopyAsBitmap()
    {
        Clipboard.SetDataObject(GetBitmapImage(), true);
    }

    public void SaveAsMetafile() => metafile(true);

    public void CopyAsMetafile() => metafile(false);

    private void metafile(bool save)
    {
        using Graphics grfx = CreateGraphics();
        IntPtr ipHdc = grfx.GetHdc();
        using MemoryStream ms = new();
        using Metafile mf = new(ms, ipHdc, EmfType.EmfPlusDual);
        grfx.ReleaseHdc(ipHdc);
        using (var g = Graphics.FromImage(mf))
        {
            var width = Math.Max(1, pictureBox.ClientSize.Width);
            var height = Math.Max(1, pictureBox.ClientSize.Height);
            g.Clear(pictureBox.BackColor); // (260322Ch) export 先でも背景を画面表示に合わせる

            if (GetCurrentPictureImage() is Image image)
                g.DrawImage(image, new Rectangle(0, 0, width, height));

            PaintPictureBoxOverlay(g); // (260322Ch) metafile export も bitmap export と同じ overlay 経路へ揃える
        }

        if (save)
        {
            using SaveFileDialog dlg = new() { Filter = "*.emf|*.emf" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using FileStream fsm = new(dlg.FileName, FileMode.Create, FileAccess.Write);
                fsm.Write(ms.GetBuffer(), 0, (int)ms.Length);
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


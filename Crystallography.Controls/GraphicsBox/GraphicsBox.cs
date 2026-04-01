using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Crystallography.Controls;

/// <summary>PictureBox の標準機能に、呼び出し側が継続して描画できる描画バッファとマウスホイール通知を加えたコントロール。</summary>
[Serializable]
public class GraphicsBox : PictureBox
{
    /// <summary>マウスホイール入力後に通知するイベントデリゲート。</summary>
    public delegate void evMouseWheeled(object sender, MouseEventArgs e);

    private Bitmap graphicsLayerBitmap = null; // (260322Ch) 描画バッファの内容を保持する
    private Graphics graphicsLayer = null; // (260322Ch) 呼び出し側が使い回せる描画バッファ用 Graphics を保持する

    /// <summary>マウスホイール入力後に通知する互換イベント。</summary>
    public event evMouseWheeled MouseWheeled;

    /// <summary>GraphicBox の既定コンストラクタ。</summary>
    public GraphicsBox()
    {
        InitializeGraphicBox();
    }

    /// <summary>コンテナへ自動登録する互換コンストラクタ。</summary>
    public GraphicsBox(IContainer container)
        : this()
    {
        container?.Add(this); // (260322Ch) designer 生成コードからそのまま使えるようにする
    }

    /// <summary>呼び出し側が描画バッファへの描画に使う Graphics を返す。返された Graphics は破棄しないこと。</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Graphics Graphics
    {
        get
        {
            EnsureGraphicsLayer();
            return graphicsLayer;
        }
    }

    /// <summary>Font を別名で公開するプロパティ。</summary>
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Font Fonts
    {
        get => Font;
        set => Font = value;
    }

    /// <summary>
    /// 現在表示される継承元のPictureBoxのImageと描画バッファを合成したスナップショットを返す。
    /// 呼び出し側で Dispose すること。
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Bitmap RenderedImage => CreateRenderedBitmap();

    /// <summary>現在表示される合成画像を新しい Bitmap として生成する。</summary>
    public Bitmap CreateRenderedBitmap()
    {
        var width = Math.Max(1, ClientSize.Width);
        var height = Math.Max(1, ClientSize.Height);
        var bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);

        // bitmap = new Bitmap(width, height); // (260322Ch) 旧案: 既定 pixel format のまま確保していた
        // DrawToBitmap(bitmap, new Rectangle(Point.Empty, bitmap.Size)); // (260322Ch) 旧案: Control 任せでスナップショットを取っていた
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.Transparent);
        using var args = new PaintEventArgs(graphics, new Rectangle(Point.Empty, bitmap.Size));
        InvokePaintBackground(this, args);
        InvokePaint(this, args); // (260322Ch) 継承元のPictureBoxのImageと描画バッファの両方を確実に合成する
        return bitmap;
    }

    /// <summary>描画バッファだけをクリアして再描画する。</summary>
    public void ClearGraphicsLayer()
    {
        EnsureGraphicsLayer();
        graphicsLayer.Clear(Color.Transparent); // (260322Ch) 継承元のPictureBoxのImage表示は残し、描画バッファだけ消す
        Invalidate();
    }

    /// <summary>描画バッファの Bitmap を取得する。呼び出し側で破棄しないこと。</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Bitmap GraphicsLayerBitmap
    {
        get
        {
            EnsureGraphicsLayer();
            return graphicsLayerBitmap;
        }
    }

    /// <summary>描画バッファを必要に応じて作り直す。</summary>
    private void EnsureGraphicsLayer()
    {
        var width = Math.Max(1, ClientSize.Width);
        var height = Math.Max(1, ClientSize.Height);
        if (graphicsLayerBitmap != null && graphicsLayerBitmap.Width == width && graphicsLayerBitmap.Height == height && graphicsLayer != null)
            return;

        RecreateGraphicsLayer(true);
    }

    /// <summary>描画バッファを再作成する。必要に応じて従前内容を左上基準で引き継ぐ。</summary>
    private void RecreateGraphicsLayer(bool preserveContents)
    {
        var width = Math.Max(1, ClientSize.Width);
        var height = Math.Max(1, ClientSize.Height);

        var previousBitmap = graphicsLayerBitmap;
        var previousGraphics = graphicsLayer;

        var nextBitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
        var nextGraphics = Graphics.FromImage(nextBitmap);
        nextGraphics.Clear(Color.Transparent); // (260322Ch) 透過の描画バッファとして保持し、背景や継承元のPictureBoxのImageを隠さない

        if (preserveContents && previousBitmap != null)
            nextGraphics.DrawImageUnscaled(previousBitmap, 0, 0); // (260322Ch) resize 後も描画内容をできるだけ残す

        graphicsLayerBitmap = nextBitmap;
        graphicsLayer = nextGraphics;

        previousGraphics?.Dispose();
        previousBitmap?.Dispose();
    }

    /// <summary>コントロール初期状態を設定する。</summary>
    private void InitializeGraphicBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.Selectable, true); // (260322Ch) マウス操作後に Focus を受けてホイールを取りやすくする
        DoubleBuffered = true;
    }

    /// <summary>PictureBox の描画後に描画バッファを重ねる。</summary>
    protected override void OnPaint(PaintEventArgs pe)
    {
        base.OnPaint(pe);
        if (graphicsLayerBitmap == null)
            return;

        pe.Graphics.DrawImageUnscaled(graphicsLayerBitmap, 0, 0); // (260322Ch) 継承元のPictureBoxのImageと描画バッファを合成して表示する
    }

    /// <summary>サイズ変更時に描画バッファを再作成する。</summary>
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        RecreateGraphicsLayer(true);
        Invalidate();
    }

    /// <summary>ハンドル作成後に描画バッファを確保する。</summary>
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        RecreateGraphicsLayer(true);
    }

    /// <summary>クリック時にフォーカスを受けてホイール入力を受けやすくする。</summary>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        // Focus(); // (260322Ch) 旧案: 条件を見ずに Focus を呼んでいた
        if (CanFocus)
            Focus();
        base.OnMouseDown(e);
    }

    /// <summary>ホイール入力後に互換イベントを通知する。</summary>
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);
        MouseWheeled?.Invoke(this, e); // (260322Ch) マウスホイール入力の処理後に通知する
    }

    /// <summary>描画バッファを破棄する。</summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            graphicsLayer?.Dispose();
            graphicsLayerBitmap?.Dispose();
            graphicsLayer = null;
            graphicsLayerBitmap = null;
        }

        base.Dispose(disposing);
    }
}

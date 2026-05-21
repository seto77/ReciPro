using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Crystallography.Controls;

// 260413Cl 新規追加
// NumericUpDownのスピンボタン部分だけをVisualStyleRendererで自前描画する軽量コントロール。
// TextBox部を持たないため横幅を自由に調整できる。
[DefaultEvent("UpClick")]
[ToolboxItem(true)]
public class SpinButton : Control
{
    private enum Part { Up, Down }
    private enum PartState { Normal, Hot, Pressed, Disabled }

    private Part? hotPart;
    private Part? pressedPart;
    private readonly Timer repeatTimer;
    private const int InitialDelayMs = 400;
    private const int RepeatIntervalMs = 50;

    [Category("Action")]
    [Description("上ボタンがクリックされたとき、および長押し中に連続発火します。")]
    public event EventHandler UpClick;

    [Category("Action")]
    [Description("下ボタンがクリックされたとき、および長押し中に連続発火します。")]
    public event EventHandler DownClick;

    public SpinButton()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint
               | ControlStyles.UserPaint
               | ControlStyles.OptimizedDoubleBuffer
               | ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.Selectable, false); // クリックしてもフォーカスを奪わない
        repeatTimer = new Timer { Interval = InitialDelayMs };
        repeatTimer.Tick += (_, _) =>
        {
            if (pressedPart == null) { repeatTimer.Stop(); return; }
            repeatTimer.Interval = RepeatIntervalMs;
            RaiseClick(pressedPart.Value);
        };
    }

    protected override Size DefaultSize => new(17, 20);

    private Rectangle UpRect => new(0, 0, Width, Height / 2);
    private Rectangle DownRect => new(0, Height / 2, Width, Height - Height / 2);

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawPart(e.Graphics, Part.Up, UpRect);
        DrawPart(e.Graphics, Part.Down, DownRect);
    }

    // 260519Cl 変更: VisualStyleRenderer / ControlPaint 経路は OS テーマ・DPI・ハイコントラスト等で
    // 見た目が変わるため、背景塗り + 三角形矢印で環境非依存に統一描画する。
    // 旧実装は下の DrawPart_Legacy / GetElement に残してある。
    // 260519Cl 整理: SystemBrushes 利用で per-paint allocation を排除、Pen は static にキャッシュ。
    private static readonly Pen borderPen = new(ControlPaint.Light(SystemColors.ControlDark));

    private void DrawPart(Graphics g, Part part, Rectangle r)
    {
        var state = GetState(part);

        var bgBrush = state switch
        {
            PartState.Pressed => SystemBrushes.ControlDark,
            PartState.Hot => SystemBrushes.ControlLight,
            _ => SystemBrushes.Control,
        };
        g.FillRectangle(bgBrush, r);
        g.DrawRectangle(borderPen, r.X, r.Y, r.Width - 1, r.Height - 1);

        var arrowBrush = state == PartState.Disabled ? SystemBrushes.GrayText : SystemBrushes.ControlText;

        // 三角形サイズはコントロール全体の寸法から算出 (Height が奇数のとき Up/Down で
        // 1px ずれて非対称化するのを防ぐため partH = min(UpRect.Height, DownRect.Height) を共有)。
        int partH = Math.Min(UpRect.Height, DownRect.Height);
        int size = Math.Max(3, Math.Min((Width - 2) / 4, (partH - 1) / 2));
        int cx = Width / 2;
        int cy = part == Part.Up ? partH / 2 : Height - partH / 2;

        for (int k = 0; k < size; k++)
        {
            int w = part == Part.Up ? 2 * k + 1 : 2 * (size - 1 - k) + 1;
            int y = cy - size / 2 + k;
            int x = cx - w / 2;
            g.FillRectangle(arrowBrush, x, y, w, 1);
        }
    }

    // 260519Cl コメントアウト: 旧 VisualStyleRenderer ベース実装。環境依存性の参照用に保存。
    //private void DrawPart_Legacy(Graphics g, Part part, Rectangle r)
    //{
    //    var state = GetState(part);
    //    if (VisualStyleRenderer.IsSupported)
    //    {
    //        var element = GetElement(part, state);
    //        if (VisualStyleRenderer.IsElementDefined(element))
    //        {
    //            new VisualStyleRenderer(element).DrawBackground(g, r);
    //            return;
    //        }
    //    }
    //    // テーマが無効な環境へのフォールバック
    //    var btn = state switch
    //    {
    //        PartState.Pressed => ButtonState.Pushed,
    //        PartState.Disabled => ButtonState.Inactive,
    //        _ => ButtonState.Normal,
    //    };
    //    ControlPaint.DrawScrollButton(g, r,
    //        part == Part.Up ? ScrollButton.Up : ScrollButton.Down, btn);
    //}

    //private static VisualStyleElement GetElement(Part part, PartState state) => (part, state) switch
    //{
    //    (Part.Up, PartState.Normal) => VisualStyleElement.Spin.Up.Normal,
    //    (Part.Up, PartState.Hot) => VisualStyleElement.Spin.Up.Hot,
    //    (Part.Up, PartState.Pressed) => VisualStyleElement.Spin.Up.Pressed,
    //    (Part.Up, PartState.Disabled) => VisualStyleElement.Spin.Up.Disabled,
    //    (Part.Down, PartState.Normal) => VisualStyleElement.Spin.Down.Normal,
    //    (Part.Down, PartState.Hot) => VisualStyleElement.Spin.Down.Hot,
    //    (Part.Down, PartState.Pressed) => VisualStyleElement.Spin.Down.Pressed,
    //    _ => VisualStyleElement.Spin.Down.Disabled,
    //};

    private PartState GetState(Part part)
    {
        if (!Enabled) return PartState.Disabled;
        if (pressedPart == part) return PartState.Pressed;
        if (hotPart == part) return PartState.Hot;
        return PartState.Normal;
    }

    private Part? HitTest(Point p)
    {
        if (UpRect.Contains(p)) return Part.Up;
        if (DownRect.Contains(p)) return Part.Down;
        return null;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        var hit = HitTest(e.Location);
        if (hit != hotPart)
        {
            hotPart = hit;
            Invalidate();
        }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        if (hotPart != null)
        {
            hotPart = null;
            Invalidate();
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button != MouseButtons.Left) return;
        var hit = HitTest(e.Location);
        if (hit == null) return;
        pressedPart = hit;
        Capture = true;
        Invalidate();
        RaiseClick(hit.Value);
        repeatTimer.Interval = InitialDelayMs;
        repeatTimer.Start();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        if (pressedPart != null)
        {
            pressedPart = null;
            Capture = false;
            repeatTimer.Stop();
            Invalidate();
        }
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        Invalidate();
    }

    private void RaiseClick(Part part)
    {
        if (part == Part.Up) UpClick?.Invoke(this, EventArgs.Empty);
        else DownClick?.Invoke(this, EventArgs.Empty);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) repeatTimer?.Dispose();
        base.Dispose(disposing);
    }
}

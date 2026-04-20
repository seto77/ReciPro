using System; // 260420Cl 追加 Math.Min 用
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public static class WindowLocation
    {
        public static void Adjust(Form form)
        {
            double fL = form.Bounds.Left, fR = form.Bounds.Right, fT = form.Bounds.Top, fB = form.Bounds.Bottom, fW = form.Size.Width, fH = form.Size.Height;

            if (Screen.AllScreens.All(s => (fL - s.Bounds.Left) / fW < -0.8 || (s.Bounds.Right - fR) / fW < -0.8 || (fT - s.Bounds.Top) / fH < -0.8 || (s.Bounds.Bottom - fB) / fH < -0.8))
            {
                var scr = Screen.AllScreens.First(e => e.DeviceName == Screen.FromControl(form).DeviceName);
                form.Location = new Point(scr.Bounds.X + 100, scr.Bounds.Y + 100);
            }
        }

        /// <summary>260420Cl 追加 ウィンドウ中心点が可視領域に無ければプライマリ画面に再配置する保守的バージョン.
        /// 既存 Adjust は 20% ヒューリスティクスで特定多画面環境で誤発火したためメインフォーム側では無効化された経緯あり (#55 関連)。
        /// こちらは中心点のみを見るので誤発火しにくい。</summary>
        public static void EnsureVisible(Form form)
        {
            var b = form.Bounds;
            var center = new Point(b.X + b.Width / 2, b.Y + b.Height / 2);
            if (Screen.AllScreens.Any(s => s.WorkingArea.Contains(center)))
                return;

            var wa = Screen.PrimaryScreen.WorkingArea;
            var w = Math.Min(b.Width, wa.Width - 100);
            var h = Math.Min(b.Height, wa.Height - 100);
            form.Bounds = new Rectangle(wa.X + 50, wa.Y + 50, w, h);
        }
    }
}

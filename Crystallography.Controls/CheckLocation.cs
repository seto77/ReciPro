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
    }
}

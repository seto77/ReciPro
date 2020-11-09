using System.Drawing;
using System.Linq;

namespace Crystallography
{
    /// <summary>
    /// Graphics クラスの描画関数にdoubleを受けられるにようにした拡張メソッド
    /// </summary>
    public static class GraphicsAlpha
    {
        public static void DrawArc(this Graphics g, Pen pen, double x, double y, double width, double height, double startAngle, double sweepAngle) 
            => g.DrawArc(pen, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        public static void DrawLines(this Graphics g, Pen pen, PointD[] points)
        {
            //if (points.Length < 2)
            //    return;
            //PointF[] pointsF = new PointF[points.Length];
            //for (int i = 0; i < points.Length; i++)
            //    pointsF[i] = new PointF((float)points[i].X, (float)points[i].Y);
            g.DrawLines(pen, points.Select(p => p.ToPointF()).ToArray());
        }

        public static void DrawLine(this Graphics g, Pen pen, double x1, double y1, double x2, double y2)
            => g.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);

        public static void FillPolygon(this Graphics g, Brush brush, PointD[] points, System.Drawing.Drawing2D.FillMode fillMode)
        {
            //var pointsF = new PointF[points.Length];
            //for (int i = 0; i < points.Length; i++)
            //    pointsF[i] = new PointF((float)points[i].X, (float)points[i].Y);
            g.FillPolygon(brush, points.Select(p=>p.ToPointF()).ToArray(), fillMode);
        }

        public static void FillRectangle(this Graphics g, Brush brush, double x, double y, double width, double height)
            => g.FillRectangle(brush, (float)x, (float)y, (float)width, (float)height);

        public static void FillPie(this Graphics g, Brush brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => g.FillPie(brush, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
    }
}
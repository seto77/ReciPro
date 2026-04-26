using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Crystallography.Controls;

/// <summary>
/// 260426Cl 追加: PoleFigureControl と PoleFigureControl2 で重複していたステレオネットの経線描画ヘルパ
/// </summary>
internal static class StereonetDrawing
{
    public static Pen MakeGridPen(int w, Color tenDegColor, Color oneDegColor) =>
        new(w % 10 == 0 ? tenDegColor : oneDegColor, w % 10 == 0 ? 0.002f : 0.0005f) { LineJoin = LineJoin.Round };

    /// <summary>
    /// Schmidt ネットの経線群を描画する。
    /// latRange=false: 角度範囲 [-π/2, π/2] で xz 平面相当、true: [0, π] で yz 平面相当。
    /// </summary>
    public static void DrawMeridians(Graphics g, int div, PointD[] pt1, PointD[] pt2,
        bool latRange, bool showOneDeg, Color tenDegColor, Color oneDegColor)
    {
        var cos2 = new double[div];
        var sin2 = new double[div];
        for (int i = 0; i < div; i++)
        {
            double d = (double)i / div * Math.PI - (latRange ? 0 : Math.PI / 2);
            cos2[i] = Math.Cos(d);
            sin2[i] = Math.Sin(d);
        }

        for (int w = 1; w < 90; w++)
        {
            if (!showOneDeg && w % 10 != 0) continue;
            using var pen = MakeGridPen(w, tenDegColor, oneDegColor);
            double theta = w * Math.PI / 180.0, cos1 = Math.Cos(theta), sin1 = Math.Sin(theta);
            for (int i = 0; i < div; i++)
            {
                pt1[i] = latRange
                    ? 1 / Math.Sqrt(1 + cos1 * sin2[i]) * new PointD(sin1 * sin2[i], cos2[i])
                    : 1 / Math.Sqrt(1 + cos2[i] * sin1) * new PointD(sin2[i] * sin1, cos1);
                pt2[i] = -pt1[i];
            }
            g.DrawLines(pen, pt1);
            g.DrawLines(pen, pt2);
        }
    }
}

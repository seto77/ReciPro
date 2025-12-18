using Mono.Unix.Native;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Windows.Devices.Input.Preview;

namespace ReciPro;

public partial class FormDiffractionSimulatorHolder : Form
{
    #region Fields and Properties
    public FormDiffractionSimulator FormDiffractionSimulator;
    private PointD centerPt = new(0, 0);
    private double mag =>
         graphicsBox.ClientSize.Width / 2.0
        / Vector3D.StereoNetPoint(new Vector3D(0, Math.Sin(numericBoxDrawingArea.RadianValue), Math.Cos(numericBoxDrawingArea.RadianValue))).Y;
    private readonly Timer timer = new();
    private Crystal crystal => FormDiffractionSimulator.formMain.Crystal;



    /// <summary>
    /// Trigonometric Function Table
    /// </summary>
    private readonly List<(double Sin, double Cos, double Tan)> TF =
        Enumerable.Range(0, 360).Select(n => (Math.Sin(n * Math.PI / 180.0), Math.Cos(n * Math.PI / 180.0), Math.Tan(n * Math.PI / 180.0))).ToList();


    private List<(Vector3DBase Vec, (int U, int V, int W) Index)> ZoneAxes = new();

    #endregion
    public FormDiffractionSimulatorHolder()
    {
        InitializeComponent();
    }

    private void FormDiffractionSimulatorHolder_Load(object sender, EventArgs e)
    {
        Draw();

        timer.Interval = 500;
        timer.Tick += ((sender, e) => graphicsBox.Refresh());
        timer.Start();
    }

    //ステレオネットを描く
    public void Draw(Graphics g = null, bool renewOutline = true)
    {
        if (graphicsBox.Width <= 0 || graphicsBox.Height <= 0) return;

        //グラフィックスボックスに描画する場合
        g ??= graphicsBox.Graphics;

        g.Clear(colorControlBackGround.Color);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        DrawOutline(g);

        graphicsBox.Refresh();
    }

    //ステレオネットの輪郭を描く
    private void DrawOutline(Graphics g)
    {
        var (magSin, magCos) = ((float)(mag * Math.Sin(numericBoxPrimaryAxisDirection.RadianValue)), (float)(mag * Math.Cos(numericBoxPrimaryAxisDirection.RadianValue)));
        g.Transform = new Matrix(magCos, -magSin, magSin, magCos,
        (float)(graphicsBox.ClientSize.Width / 2.0 - mag * centerPt.X), (float)(graphicsBox.ClientSize.Height / 2.0 + mag * centerPt.Y));

        //if (TF.Count == 0)
        //    for (int n = 0; n < 360; n++)
        //        TF.Add((Math.Sin(n * Math.PI / 180.0), Math.Cos(n * Math.PI / 180.0), Math.Tan(n * Math.PI / 180.0)));

        var pen1 = new Pen(new SolidBrush(colorControl1DegLine.Color), (float)(1 / mag));
        var pen10 = new Pen(new SolidBrush(colorControl10DegLine.Color), (float)(2 / mag));
        var pen90 = new Pen(new SolidBrush(colorControl90DegLine.Color), (float)(3 / mag));


        var wList = new List<int>();
        for (int i = 1; i < 90; i++)
            if (i % 10 != 0)
                wList.Add(i);
        for (int i = 10; i < 90; i += 10)
            wList.Add(i);

        for (int n = 0; n < wList.Count; n++)
        {
            int w = wList[n];
            if (checkBox1DegLine.Checked || w % 10 == 0)
            {
                g.DrawArc(w % 10 == 0 ? pen10 : pen1, -TF[w].Cos / (1 + TF[w].Sin), -1 / TF[w].Cos, 2 / TF[w].Cos, 2 / TF[w].Cos, (w + 90), (180 - 2 * w));
                g.DrawArc(w % 10 == 0 ? pen10 : pen1, -TF[w].Cos / (1 - TF[w].Sin), -1 / TF[w].Cos, 2 / TF[w].Cos, 2 / TF[w].Cos, (w - 90), (180 - 2 * w));

                g.DrawArc(w % 10 == 0 ? pen10 : pen1, -TF[w].Tan, -TF[w].Cos / (1 - TF[w].Sin), 2 * TF[w].Tan, 2 * TF[w].Tan, w, (180 - 2 * w));
                g.DrawArc(w % 10 == 0 ? pen10 : pen1, -TF[w].Tan, TF[w].Cos / (1 + TF[w].Sin), 2 * TF[w].Tan, 2 * TF[w].Tan, (w + 180), (180 - 2 * w));
            }
        }

        g.DrawArc(pen90, -1f, -1f, 2f, 2f, 0, 360);
        g.DrawLine(pen90, 0f, -1f, 0f, 1f);
        g.DrawLine(pen90, -1f, 0f, 1f, 0f);

        if (checkBoxTiltDirections.Checked)
        {
            var penTiltX = new Pen(new SolidBrush(colorControlTiltX.Color), (float)(2 / mag));
            g.DrawLine(penTiltX, 0f, 0f, (float)(50 / mag), 0f);
            g.DrawLine(penTiltX, (float)(50 / mag), 0f, (float)(40 / mag), (float)(5 / mag));
            g.DrawLine(penTiltX, (float)(50 / mag), 0f, (float)(40 / mag), (float)(-5 / mag));

            var penTiltY = new Pen(new SolidBrush(colorControlTiltY.Color), (float)(2 / mag));
            var sign = radioButtonTiltY_Plus.Checked ? -1 : 1;
            g.DrawLine(penTiltY, 0f, 0f, 0f, (float)(sign * 50 / mag));
            g.DrawLine(penTiltY, 0f, (float)(sign * 50 / mag), (float)(5 / mag), (float)(sign * 40 / mag));
            g.DrawLine(penTiltY, 0f, (float)(sign * 50 / mag), (float)(-5 / mag), (float)(sign * 40 / mag));
        }
        //g.DrawLine()
    }

    #region 座標変換
    private PointF convertSrcToClient(PointD pt)
 => new((float)(graphicsBox.ClientSize.Width / 2.0 + mag * (pt.X - centerPt.X)), (float)(graphicsBox.ClientSize.Height / 2.0 + mag * (pt.Y - centerPt.Y)));

    private PointF convertSrcToClient(double x, double y) => convertSrcToClient(new PointD(x, y));

    private PointD convertClientToSrc(Point pt)
        => new(centerPt.X + (pt.X - graphicsBox.ClientSize.Width / 2) / mag, centerPt.Y + (graphicsBox.ClientSize.Height / 2 - pt.Y) / mag);

    private PointD convertClientToSrc(int x, int y) => convertClientToSrc(new Point(x, y));

    private Vector3DBase convertSrcToVector(PointD pt) => Vector3D.SphereVector(pt);
    #endregion

    #region graphicsBox のイベント
    private void graphicsBox_MouseDown(object sender, MouseEventArgs e)
    {

    }

    private void graphicsBox_MouseMove(object sender, MouseEventArgs e)
    {
        var pt = convertClientToSrc(e.Location);
        var sin = Math.Sin(-numericBoxPrimaryAxisDirection.RadianValue);
        var cos = Math.Cos(-numericBoxPrimaryAxisDirection.RadianValue);
        pt = new PointD(cos * pt.X - sin * pt.Y, sin * pt.X + cos * pt.Y);
        double tiltY = Math.Asin(2 * pt.Y / (1 + pt.X * pt.X + pt.Y * pt.Y));
        double tiltX = (Math.Cos(tiltY) != 0) ? Math.Asin(2 * pt.X / (1 + pt.X * pt.X + pt.Y * pt.Y) / Math.Cos(tiltY)) : 0;
        var sign = radioButtonTiltY_Plus.Checked ? 1 : -1;
        label1MousePosition.Text = $"Tilt X: {tiltX / Math.PI * 180:f1}°, Y: {sign*tiltY/ Math.PI * 180:f1}°";
    }
    #endregion

    private void setVector()
    {
        if (crystal.A * crystal.B * crystal.C != 0)
        {
            //crystal.SetVectorOfAxis((int)numericBoxU.Value, (int)numericBoxV.Value, (int)numericBoxW.Value);
        }
    }

    private void buttonLink_Click(object sender, EventArgs e)
    {
    }

    private void checkBox1DegLine_CheckedChanged(object sender, EventArgs e) => Draw();
    private void colorControlUniqueAxis_ColorChanged(object sender, EventArgs e) => Draw();
    private void numericBoxDrawingArea_ValueChanged(object sender, EventArgs e) => Draw();
    private void numericBoxPrimaryAxisDirection_ValueChanged(object sender, EventArgs e) => Draw();

    
}

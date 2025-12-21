#region using
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;
using Windows.Devices.Radios;
#endregion 

namespace ReciPro;

public partial class FormDiffractionSimulatorHolder : Form
{
    #region Fields and Properties
    public FormDiffractionSimulator FormDiffractionSimulator;
    private PointD centerPt = new(0, 0);

    /// <summary>
    /// 親フォームから呼ばれるイベントをスキップするかどうか
    /// </summary>
    private bool skipEventFromParent = false;
    /// <summary>
    /// このフォームから呼ばれるイベントをスキップするかどうか
    /// </summary>
    private bool skipEventFromThis = false;

    /// <summary>
    /// 主軸の方向（ラジアン）
    /// </summary>
    public double PrimaryAxisDirection { get => numericBoxTiltXDirection.RadianValue; set => numericBoxTiltXDirection.RadianValue = value; }

    private int boxWidth => graphicsBox.ClientSize.Width;
    private int boxHeight => graphicsBox.ClientSize.Height;

    private double mag =>
         boxWidth / 2.0 / Vector3D.StereoNetPoint(new Vector3D(0, Math.Sin(numericBoxDrawingArea.RadianValue), Math.Cos(numericBoxDrawingArea.RadianValue))).Y;
    private readonly Timer timer = new();
    private Crystal crystal => FormDiffractionSimulator.formMain.Crystal;

    private Matrix3D NeutralDirection { get; set; } = Matrix3D.IdentityMatrix;

    private Matrix3D HolderRotation =>
        Matrix3D.RotZ(numericBoxTiltXDirection.RadianValue) *
        Matrix3D.RotY(numericBoxTiltX.RadianValue) *
        Matrix3D.RotX((radioButtonTiltY_Plus.Checked ? -1 : 1) * numericBoxTiltY.RadianValue) *
        Matrix3D.RotZ(numericBoxTiltXDirection.RadianValue).Inverse();

    /// <summary>
    /// Trigonometric Function Table
    /// </summary>
    private readonly List<(double Sin, double Cos, double Tan)> TF =
        [.. Enumerable.Range(0, 360).Select(n => n * Math.PI / 180.0).Select(e => (Math.Sin(e), Math.Cos(e), Math.Tan(e)))];


    private List<(Vector3DBase Vec, (int U, int V, int W) Index)> ZoneAxes = [];

    #endregion

    #region コンストラクタ、ロード、クローズイベント
    public FormDiffractionSimulatorHolder()
    {
        InitializeComponent();
    }

    private void FormDiffractionSimulatorHolder_Load(object sender, EventArgs e)
    {
        NeutralDirection = HolderRotation * crystal.RotationMatrix;

        setVector();
        Draw();

        timer.Interval = 500;
        timer.Tick += ((sender, e) => graphicsBox.Refresh());
        timer.Start();
    }
    private void FormDiffractionSimulatorHolder_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        this.Visible = false;
    }
    #endregion

    #region 描画
    //ステレオネットを描く
    public void Draw(Graphics g = null, bool renewOutline = true)
    {
        if (graphicsBox.Width <= 0 || graphicsBox.Height <= 0) return;

        //グラフィックスボックスに描画する場合
        g ??= graphicsBox.Graphics;

        g.Clear(colorControlBackGround.Color);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        DrawOutline(g);
        DrawStereoNet(g);

        graphicsBox.Refresh();
    }

    /// <summary>
    /// ステレオネットの輪郭を描く
    /// </summary>
    /// <param name="g"></param>
    private void DrawOutline(Graphics g)
    {
        var (magSin, magCos) = ((float)(mag * Math.Sin(PrimaryAxisDirection)), (float)(mag * Math.Cos(PrimaryAxisDirection)));
        g.Transform = new Matrix(magCos, -magSin, magSin, magCos, (float)(boxWidth / 2.0 - mag * centerPt.X), (float)(boxHeight / 2.0 + mag * centerPt.Y));

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
            var pen = w % 10 == 0 ? pen10 : pen1;
            if (checkBox1DegLine.Checked || w % 10 == 0)
            {
                g.DrawArc(pen, -TF[w].Cos / (1 + TF[w].Sin), -1 / TF[w].Cos, 2 / TF[w].Cos, 2 / TF[w].Cos, (w + 90), (180 - 2 * w));
                g.DrawArc(pen, -TF[w].Cos / (1 - TF[w].Sin), -1 / TF[w].Cos, 2 / TF[w].Cos, 2 / TF[w].Cos, (w - 90), (180 - 2 * w));

                g.DrawArc(pen, -TF[w].Tan, -TF[w].Cos / (1 - TF[w].Sin), 2 * TF[w].Tan, 2 * TF[w].Tan, w, (180 - 2 * w));
                g.DrawArc(pen, -TF[w].Tan, TF[w].Cos / (1 + TF[w].Sin), 2 * TF[w].Tan, 2 * TF[w].Tan, (w + 180), (180 - 2 * w));
            }
        }

        g.DrawArc(pen90, -1f, -1f, 2f, 2f, 0, 360);
        g.DrawLine(pen90, 0f, -1f, 0f, 1f);
        g.DrawLine(pen90, -1f, 0f, 1f, 0f);

        if (checkBoxTiltDirections.Checked)
        {
            var penTiltX = new Pen(new SolidBrush(colorControlTiltX.Color), (float)(2 / mag));
            g.DrawLine(penTiltX, 0f, 0f, 50 / mag, 0);
            g.DrawLine(penTiltX, 50 / mag, 0, 40 / mag, 5 / mag);
            g.DrawLine(penTiltX, 50 / mag, 0f, 40 / mag, -5 / mag);

            var penTiltY = new Pen(new SolidBrush(colorControlTiltY.Color), (float)(2 / mag));
            var sign = radioButtonTiltY_Plus.Checked ? -1 : 1;
            g.DrawLine(penTiltY, 0f, 0f, 0f, sign * 50 / mag);
            g.DrawLine(penTiltY, 0f, sign * 50 / mag, 5 / mag, sign * 40 / mag);
            g.DrawLine(penTiltY, 0f, sign * 50 / mag, -5 / mag, sign * 40 / mag);
        }
        //g.DrawLine()
    }

    /// <summary>
    /// ステレオネットに晶帯軸を描く
    /// </summary>
    /// <param name="g"></param>
    private void DrawStereoNet(Graphics g)
    {
        if (ZoneAxes.Count == 0)
            setVector();
        g.Transform = new Matrix((float)mag, 0, 0, (float)mag, (float)(boxWidth / 2.0 - mag * centerPt.X), (float)(boxHeight / 2.0 + mag * centerPt.Y));
        var rot = HolderRotation * crystal.RotationMatrix;

        var font = new Font("Tahoma", trackBarStrSize.Value / (float)mag / 7f);
        var brushUnique = new SolidBrush(colorControlUniqueAxis.Color);
        var brushGeneral = new SolidBrush(colorControlGeneralAxis.Color);
        var radius = trackBarPointSize.Value / mag;

        //晶帯軸の描画
        foreach (var zone in ZoneAxes)
        {
            var pt = Stereonet.ConvertVectorToWulff(rot * zone.Vec);
            if (pt.X * pt.X + pt.Y * pt.Y <= 1)
            {
                var (u, v, w) = zone.Index;
                var brush = (u, v, w) switch
                {
                    (1, 0, 0) or (0, 1, 0) or (0, 0, 1) or (-1, 0, 0) or (0, -1, 0) or (0, 0, -1) => brushUnique,
                    _ => brushGeneral,
                };

                g.FillEllipse(brush, new RectangleD(pt.X - radius, -pt.Y - radius, radius * 2, radius * 2));
                if (checkBoxShowIndexLabels.Checked)
                    g.DrawString($"[{u} {v} {w}]", font, brush, pt.X + radius, -pt.Y + radius);
            }
        }

        //ホルダー位置の描画
        radius *= 1.5;
        var ptHolder = Stereonet.ConvertVectorToWulff(HolderRotation * new Vector3DBase(0, 0, 1));
        var penHolder = new Pen(new SolidBrush(colorControlHolder.Color), (float)(3 / mag));
        g.DrawEllipse(penHolder, new RectangleD(ptHolder.X - radius, -ptHolder.Y - radius, radius * 2, radius * 2));
    }

    #endregion

    #region 座標変換
    private PointF convertSrcToClient(PointD pt)
 => new((float)(boxWidth / 2.0 + mag * (pt.X - centerPt.X)), (float)(boxHeight / 2.0 + mag * (pt.Y - centerPt.Y)));

    private PointF convertSrcToClient(double x, double y) => convertSrcToClient(new PointD(x, y));

    private PointD convertClientToSrc(Point pt)
        => new(centerPt.X + (pt.X - boxWidth / 2) / mag, centerPt.Y + (boxHeight / 2 - pt.Y) / mag);

    private PointD convertClientToSrc(int x, int y) => convertClientToSrc(new Point(x, y));

    private Vector3DBase convertSrcToVector(PointD pt) => Vector3D.SphereVector(pt);

    private (double TiltX, double TiltY) convertSrcToHolder(PointD pt)
    {
        var sin = Math.Sin(-numericBoxTiltXDirection.RadianValue);
        var cos = Math.Cos(-numericBoxTiltXDirection.RadianValue);
        pt = new PointD(cos * pt.X - sin * pt.Y, sin * pt.X + cos * pt.Y);
        double tiltY = Math.Asin(2 * pt.Y / (1 + pt.X * pt.X + pt.Y * pt.Y));
        double tiltX = (Math.Cos(tiltY) != 0) ? Math.Asin(2 * pt.X / (1 + pt.X * pt.X + pt.Y * pt.Y) / Math.Cos(tiltY)) : 0;
        var sign = radioButtonTiltY_Plus.Checked ? 1 : -1;
        return (tiltX, sign * tiltY);
    }
    #endregion

    #region graphicsBox のイベント
    private void graphicsBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && e.Clicks == 2)
        {
            var (tiltX, tiltY) = convertSrcToHolder(convertClientToSrc(e.Location));
            skipEventFromThis = true;
            numericBoxTiltX.RadianValue = tiltX;
            numericBoxTiltY.RadianValue = tiltY;
            skipEventFromThis = false;
            numericBoxTilt_ValueChanged(sender, new EventArgs());
        }
    }

    private void graphicsBox_MouseMove(object sender, MouseEventArgs e)
    {
        var (tiltX, tiltY) = convertSrcToHolder(convertClientToSrc(e.Location));
        label1MousePosition.Text = $"Tilt X: {tiltX / Math.PI * 180:f1}°, Y: {tiltY / Math.PI * 180:f1}°";
    }
    #endregion

    #region 晶帯軸をリセットし再計算
    private void setVector()
    {

        var indices = new HashSet<(int u, int v, int w)>();
        if (crystal.A * crystal.B * crystal.C != 0)
        {
            for (int u = -numericBoxU.ValueInteger; u <= numericBoxU.ValueInteger; u++)
                for (int v = -numericBoxV.ValueInteger; v <= numericBoxV.ValueInteger; v++)
                    for (int w = -numericBoxW.ValueInteger; w <= numericBoxW.ValueInteger; w++)
                    {
                        if (u == 0 && v == 0 && w == 0) continue;
                        if (checkBoxIncludingEquivalent.Checked)
                            foreach (var index in SymmetryStatic.GenerateEquivalentAxes(u, v, w, crystal.Symmetry, false))
                                indices.Add(index);
                        else
                            indices.Add((u, v, w));
                    }
        }
        ZoneAxes = indices.Select(e => ((e.u * crystal.A_Axis + e.v * crystal.B_Axis + e.w * crystal.C_Axis), (e.u, e.v, e.w))).ToList();
    }
    #endregion

    private void buttonLink_Click(object sender, EventArgs e)
    {
        skipEventFromThis = true;
        numericBoxTiltX.Value = numericBoxLinkTiltX.Value;
        numericBoxTiltY.Value = numericBoxLinkTiltY.Value;
        NeutralDirection = HolderRotation * crystal.RotationMatrix;
        skipEventFromThis = false;
        Draw();
    }

    #region その他イベント
    private void checkBox1DegLine_CheckedChanged(object sender, EventArgs e) => Draw();
    private void colorControlUniqueAxis_ColorChanged(object sender, EventArgs e) => Draw();
    private void numericBoxDrawingArea_ValueChanged(object sender, EventArgs e) => Draw();
    private void numericBoxPrimaryAxisDirection_ValueChanged(object sender, EventArgs e) => Draw();

    private void numericBoxU_ValueChanged(object sender, EventArgs e)
    {
        setVector();
        Draw();
    }
    #endregion

    #region 親フォームから呼ばれるイベント処理
    /// <summary>
    /// 親フォームから結晶変更を通知されたときの処理
    /// </summary>
    internal void CrystalChanged()
    {
        if (skipEventFromParent) return;

        NeutralDirection = HolderRotation * crystal.RotationMatrix;
        setVector();
        Draw();
    }

    /// <summary>
    /// 親フォームから回転変更を通知されたときの処理
    /// </summary>
    internal void RotationChanged()
    {
        if (skipEventFromParent) return;

        NeutralDirection = HolderRotation * crystal.RotationMatrix;
        Draw();
    }
    #endregion

    private void numericBoxTilt_ValueChanged(object sender, EventArgs e)
    {
        if (skipEventFromThis) return;

        skipEventFromParent = true;
        FormDiffractionSimulator.formMain.SetRotation(HolderRotation.Inverse() * NeutralDirection);
        skipEventFromParent = false;
        Draw();
    }

    private void FormDiffractionSimulatorHolder_KeyDown(object sender, KeyEventArgs e)
    {
        if (checkBoxEnableArrow.Checked && !e.Alt && !e.Shift && !e.Control
            && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
        {
            e.SuppressKeyPress = true;
            e.Handled = true;
            if (e.KeyCode == Keys.Left)
                numericBoxTiltX.Value -= numericBoxArrowStep.Value;
            else if (e.KeyCode == Keys.Right)
                numericBoxTiltX.Value += numericBoxArrowStep.Value;
            else if (e.KeyCode == Keys.Up)
                numericBoxTiltY.Value += numericBoxArrowStep.Value;
            else if (e.KeyCode == Keys.Down)
                numericBoxTiltY.Value -= numericBoxArrowStep.Value;
        }
    }


    private void checkBoxEnableArrow_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxEnableArrow.Checked) graphicsBox.Focus();
    }

    private void buttonRotate180_Click(object sender, EventArgs e)
    {
        FormDiffractionSimulator.formMain.Rotate((0, 0, 1), Math.PI);
    }

    private void checkBoxIncludingEquivalent_CheckedChanged(object sender, EventArgs e)
    {
        setVector(); Draw();
    }
}

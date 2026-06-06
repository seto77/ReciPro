﻿﻿#region using
using Crystallography.OpenGL;
using MathNet.Numerics;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Math;
using V3 = OpenTK.Mathematics.Vector3d;
#endregion
namespace ReciPro;

public partial class FormStereonet : FormBase
{
    #region フィールド、プロパティ
    public FormMain formMain;

    private Font strFont;
    private float pointSize;
    private Point MouseRangeStart, MouseRangeEnd = new(-1, -1);
    private bool MouseRangingMode = false;
    private PointD centerPt = new(0, 0);
    private double mag;
    private GLControlAlpha glControl;
    private bool HexagonalLattice => (formMain.Crystal.Symmetry.CrystalSystemStr == "trigonal" || formMain.Crystal.Symmetry.CrystalSystemStr == "hexagonal") && formMain.Crystal.Alpha != formMain.Crystal.Gamma;
    public Matrix3D RotationMatrix => formMain.Crystal.RotationMatrix;

    private bool skipEvent = false;

    //private string delimiter => radioButtonDelimiterNone.Checked ? "" : radioButtonDelimiterSpace.Checked ? " " : ",";
    private string delimiter => radioButtonDelimiterNone.Checked ? " " : radioButtonDelimiterSpace.Checked ? " " : ","; // 260425Cl None選択時にHAIR SPACE(U+200A,最細)を挿入

    public class IndexInfo
    {
        public (int X, int Y, int Z)[] Indices;
        public double Intensity = 1;
        public int ARGB;
        public Color Color => Color.FromArgb(ARGB);
        public IndexInfo() { }
        public IndexInfo((int X, int Y, int Z)[] indices, int argb, double intensity = 0)
        {
            Indices = indices;
            ARGB = argb;
            Intensity = intensity;
        }
        public override string ToString() => $"{Indices[0].X} {Indices[0].Y} {Indices[0].Z}";
    }

    public List<IndexInfo> ProjectedObjects = [];

    private List<IndexInfo> SpecifiedIndices = [];

    //32 64 96 128 160 192 224 255
    private static readonly int[] ColorArray = [
            Color.Red.ToArgb(),
            Color.Teal.ToArgb(),
            Color.Maroon.ToArgb(),
            Color.Fuchsia.ToArgb(),
            Color.Olive.ToArgb(),
            Color.Purple.ToArgb(),
            Color.Orange.ToArgb(),
        ];

    public bool MillerBravaisActive => formMain.MillerBravaisActive;

    #endregion

    #region 起動、終了
    public FormStereonet()
    {
        InitializeComponent();
        HelpPage = "6-stereonet"; //260529Cl 追加
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    }

    //フォームがロードされたとき
    private void FormStereonet_Load(object sender, EventArgs e)
    {

        glControl = new GLControlAlpha
        {
            AllowMouseRotation = true,
            AllowMouseScaling = true,
            AllowMouseTranslating = false,
            Name = "glControlAxes",
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 2.6,
            RotationMode = GLControlAlpha.RotationModes.Object,
            Dock = DockStyle.Fill,
            LightPosition = new V3(100, 100, 100),
            BorderStyle = BorderStyle.Fixed3D
        };
        //glControl.MouseDown += new MouseEventHandler(panelAxes_MouseDown);
        //glControl.MouseMove += new MouseEventHandler(panelAxes_MouseMove);
        splitContainer1.Panel2.Controls.Add(glControl);

        mag = Math.Min(graphicsBox.ClientSize.Width / 2.4, graphicsBox.ClientSize.Height / 2.4);
        Draw();
        lastgraphicsBoxSize = graphicsBox.ClientSize;

        splitContainer1.SplitterDistance = splitContainer1.Width / 2;
        splitContainer1.Panel2Collapsed = true;

        //YusaGonioのタブページを削除
        tabControl.TabPages.Remove(tabPage3);
    }

    private void FormStereonet_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        formMain.toolStripButtonStereonet.Checked = false;
        this.Visible = false;
    }

    private void FormStereonet_VisibleChanged(object sender, EventArgs e)
    {
        if (this.Visible)
        {
            pointSize = trackBarPointSize.Value;
            setVector();
            centerPt = new PointD(0, 0);

            mag = Math.Min(graphicsBox.ClientSize.Width / 2.4, graphicsBox.ClientSize.Height / 2.4);

            splitContainer1.BringToFront();

            Draw();
            lastgraphicsBoxSize = graphicsBox.ClientSize;

            formMain.toolStripButtonStereonet.Checked = true;
        }
    }

    /// <summary>
    /// 260524Cl 追加: --capture 用。formMain.Crystal の軸/極をステレオネットへプロットする。
    /// VisibleChanged でも setVector()+Draw() するが、撮影直前に明示的に呼んで確実に軸プロットを出す。
    /// 通常操作には影響させず、呼び出し元は GuiCapture に限定する。
    /// </summary>
    internal void PrepareCaptureForGuiAudit()
    {
        if (formMain?.Crystal == null)
            return;
        setVector();
        Draw();
    }

    #endregion

    #region 描画関連

    /// <summary>プロジェクション行列の設定を行う。</summary>
    public bool SetProjection(Graphics g)
    {
        if (g != null && graphicsBox.ClientSize.Width != 0 && graphicsBox.ClientSize.Height != 0)
            try
            {
                g.Transform =
            new Matrix(
                (float)mag, 0, 0, (float)mag,
                (float)(graphicsBox.ClientSize.Width / 2.0 - mag * centerPt.X),
                (float)(graphicsBox.ClientSize.Height / 2.0 + mag * centerPt.Y));
            }
            catch { return false; }
        return true;
    }

    //ステレオネットを描く
    public void Draw(Graphics g = null, bool renewOutline = true)
    {
        if (graphicsBox.Width <= 0 || graphicsBox.Height <= 0) return;

        //グラフィックスボックスに描画する場合
        g ??= graphicsBox.Graphics;

        if (!SetProjection(g))
            return;

        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(colorControlBackGround.Color);

        DrawOutline(g);
        DrawStereoNet(g);
        DrawCircles(g);

        Draw3D();

        if (MouseRangingMode)
        {
            using var pen = new Pen(Brushes.Gray, 1f / (float)mag);
            pen.DashStyle = DashStyle.Dash;
            var start = convertClientToSrc(MouseRangeStart);
            var end = convertClientToSrc(MouseRangeEnd);
            g.DrawRectangle(pen, (float)Math.Min(start.X, end.X), (float)Math.Min(-start.Y, -end.Y),
                (float)Math.Abs(start.X - end.X), (float)Math.Abs(start.Y - end.Y));
        }
        graphicsBox.Refresh();
    }

    private void Draw3D()
    {
        if (glControl == null || checkBoxDisplay3D.Checked) return;

        var sq2 = Math.Sqrt(2);

        V3 conv(double x, double y, double z) => radioButtonWulff.Checked ? new V3(x, y, 0) / (1 + z) : new V3(x, y, 0) / Math.Sqrt(1 + z) * sq2 + new V3(0, 0, 1);

        var glObjects = new List<GLObject>();
        glControl.DepthCueing = (true, (trackBarDepthFadingOut.Value - 10) / 2.0, 2);
        Color color10 = colorControl10DegLine.Color, color90 = colorControl90DegLine.Color;

        #region 球のアウトライン
        if (checkBox3dOptionSphere.Checked)
            for (int i = 0; i < 90; i += 10)
            {
                var mat = i % 90 == 0 ? new Material(color90, 0.4) : new Material(color10, 0.4);
                var width = i % 90 == 0 ? 2f : 1f;
                if (radioButtonOutlinePole.Checked)
                {
                    glObjects.Add(new Disk(new V3(0, 0, 0), new V3(sin[i], cos[i], 0), 1, width, mat, DrawingMode.Edges, 60));
                    glObjects.Add(new Disk(new V3(0, 0, 0), new V3(cos[i], -sin[i], 0), 1, width, mat, DrawingMode.Edges, 60));
                    glObjects.Add(new Disk(new V3(0, 0, sin[i]), new V3(0, 0, 1), cos[i], width, mat, DrawingMode.Edges, 60));
                    glObjects.Add(new Disk(new V3(0, 0, -sin[i]), new V3(0, 0, 1), cos[i], width, mat, DrawingMode.Edges, 60));
                }
                else
                {
                    glObjects.Add(new Disk(new V3(0, 0, 0), new V3(sin[i], 0, cos[i]), 1, width, mat, DrawingMode.Edges, 60));
                    glObjects.Add(new Disk(new V3(0, 0, 0), new V3(cos[i], 0, -sin[i]), 1, width, mat, DrawingMode.Edges, 60));
                    glObjects.Add(new Disk(new V3(0, sin[i], 0), new V3(0, 1, 0), cos[i], width, mat, DrawingMode.Edges, 60));
                    glObjects.Add(new Disk(new V3(0, -sin[i], 0), new V3(0, 1, 0), cos[i], width, mat, DrawingMode.Edges, 60));
                }
            }
        #endregion

        #region ステレオネットのアウトライン
        if (checkBox3dOptionStereonet.Checked)
        {
            if (radioButtonWulff.Checked)
            {
                glObjects.Add(new Disk(new V3(0, 0, 0), new V3(0, 0, 1), 1, new Material(Color.White, 0.5), DrawingMode.Surfaces, 60));
                glObjects.Add(new Lines([new(-1, 0, 0.005), new(1, 0, 0.005)], 3f, new Material(color90)));
                glObjects.Add(new Lines([new(0, -1, 0.005), new(0, 1, 0.005)], 3f, new Material(color90)));
            }
            else
            {
                glObjects.Add(new Disk(new V3(0, 0, 1), new V3(0, 0, 1), Math.Sqrt(2), new Material(Color.White, 0.5), DrawingMode.Surfaces, 60));
                glObjects.Add(new Lines([new(-sq2, 0, 1.005), new(sq2, 0, 1.005)], 3f, new Material(color90)));
                glObjects.Add(new Lines([new(0, -sq2, 1.005), new(0, sq2, 1.005)], 3f, new Material(color90)));
                glObjects.Add(new Disk(new V3(0, 0, 1), new V3(0, 0, 1), Math.Sqrt(2), 3f, new Material(color90), DrawingMode.Edges, 60));
            }

            for (int i = 10; i < 90; i += 10)
            {
                List<V3> pts1 = [], pts2 = [];
                for (int j = 0; j <= 180; j++)
                {
                    if (radioButtonOutlinePole.Checked)
                    {
                        pts1.Add(conv(cos[j] * sin[i], sin[j] * sin[i], cos[i]));
                        pts2.Add(conv(sin[i] * cos[j], cos[i] * cos[j], sin[j]));
                    }
                    else
                    {
                        pts1.Add(conv(cos[i] * cos[j], sin[i], cos[i] * sin[j]));
                        pts2.Add(conv(sin[i] * sin[j], cos[j], cos[i] * sin[j]));
                    }
                }
                glObjects.Add(new Lines([.. pts1], 2f, new Material(color10)));
                glObjects.Add(new Lines([.. pts1.Select(v => new V3(v.X, -v.Y, v.Z))], 2f, new Material(color10)));
                glObjects.Add(new Lines([.. pts2], 2f, new Material(color10)));
                glObjects.Add(new Lines([.. pts2.Select(v => new V3(-v.X, v.Y, v.Z))], 2f, new Material(color10)));
            }
        }
        #endregion

        # region 面、軸ベクトル
        Vector3D[] vector = radioButtonAxes.Checked ? [.. formMain.Crystal.VectorOfAxis] : [.. formMain.Crystal.VectorOfPlane];
        var radius = pointSize * 0.004;
        var matBase = radioButtonAxes.Checked ? formMain.Crystal.RotationMatrix * formMain.Crystal.MatrixReal : formMain.Crystal.RotationMatrix * formMain.Crystal.MatrixInverseTransposed;
        foreach (var obj in ProjectedObjects)
            foreach (var index in obj.Indices)
            {
                var v = (matBase * index).Normarize();
                var color = obj.Color;
                //var v = formMain.Crystal.RotationMatrix * vector[i].Normarize();
                //var color = i < 6 && radioButtonRange.Checked ? unique : general;
                if (!checkBox3dOptionSemisphere.Checked || v.Z > 0)
                {
                    glObjects.Add(new Sphere(v, radius, new Material(color, 1), DrawingMode.Surfaces));
                    if (checkBox3dOptionLabel.Checked)
                    {
                        string str;
                        if (MillerBravaisActive)
                            str = $"({index.X} {index.Y}{delimiter}{-index.X - index.Y}{delimiter}{index.Z})";
                        else
                            str = radioButtonAxes.Checked ? $"[{index.X}{delimiter}{index.Y}{delimiter}{index.Z}]" : $"({index.X}{delimiter}{index.Y}{delimiter}{index.Z})";

                        glObjects.Add(new TextObject(str, trackBarStrSize.Value / 8, v, radius + 0.001, true, new Material(color), glControl));
                    }
                }
                if (checkBox3dOptionStereonet.Checked && v.Z > 0)
                {
                    glObjects.Add(new Disk(conv(v.X, v.Y, v.Z) + new V3(0, 0, 0.0005), new V3(0, 0, 1), radius, new Material(color, 0.9), DrawingMode.Surfaces));
                    glObjects.Add(new Disk(conv(v.X, v.Y, v.Z) - new V3(0, 0, 0.0005), new V3(0, 0, 1), radius, new Material(color, 0.9), DrawingMode.Surfaces));

                    //projection line
                    if (checkBox3dOptionProjectionLine.Checked)
                    {
                        if (radioButtonWulff.Checked)
                            glObjects.Add(new Lines([new V3(0, 0, -1), new V3(v.X, v.Y, v.Z)], 1f, new Material(color, 0.5)));
                        else
                        {
                            var div = 100;
                            var r = (new Vector3DBase(0, 0, 1) - v).Length;
                            var rot = OpenTK.Mathematics.Matrix3d.CreateRotationZ(-Math.Atan2(v.Y, v.X));
                            var sweep = Math.Asin((1 - v.Z) / r);
                            var pts = new List<V3>();
                            for (int j = 0; j < div; j++)
                                pts.Add(new V3(0, 0, 1) + r * (rot * (new V3(Math.Cos(sweep * j / div), 0, -Math.Sin(sweep * j / div)))));
                            glObjects.Add(new Lines([.. pts], 1f, new Material(color, 0.5)));
                        }
                    }
                }
            }
        #endregion

        glControl.DeleteAllObjects();
        glControl.AddObjects(glObjects);
        glControl.Refresh();
    }

    private readonly List<double> sin = new(360);
    private readonly List<double> cos = new(360);
    private readonly List<double> tan = new(360);

    /// <summary>ステレオネットの輪郭を描く</summary>
    /// <param name="g"></param>
    private void DrawOutline(Graphics g)
    {
        if (sin.Count == 0)
            for (int n = 0; n < 360; n++)
            {
                sin.Add(Sin(n * PI / 180.0));
                cos.Add(Cos(n * PI / 180.0));
                tan.Add(Tan(n * PI / 180.0));
            }

        // 260528Cl 変更: trackBarOutlineLineWidth で太さ可変化 + Pen を using 化 (旧: 固定 1/2/3, Pen(new SolidBrush(...), ...) の SolidBrush leak)
        var lineWidth = trackBarOutlineLineWidth.Value / 5.0;

        using var pen1 = new Pen(colorControl1DegLine.Color, (float)(lineWidth / mag));
        using var pen10 = new Pen(colorControl10DegLine.Color, (float)(lineWidth * 2 / mag));
        using var pen90 = new Pen(colorControl90DegLine.Color, (float)(lineWidth * 3 / mag));

        if (this.radioButtonOutlineEquator.Checked)//赤道モードのとき
        {
            var wList = new List<int>();
            for (int i = 1; i < 90; i++)
                if (i % 10 != 0)
                    wList.Add(i);
            for (int i = 10; i < 90; i += 10)
                wList.Add(i);

            if (radioButtonWulff.Checked)//Wulffネット
            {
                for (int n = 0; n < wList.Count; n++)
                {
                    int w = wList[n];
                    if (checkBox1DegLine.Checked || w % 10 == 0)
                    {
                        g.DrawArc(w % 10 == 0 ? pen10 : pen1, -cos[w] / (1 + sin[w]), -1 / cos[w], 2 / cos[w], 2 / cos[w], (w + 90), (180 - 2 * w));
                        g.DrawArc(w % 10 == 0 ? pen10 : pen1, -cos[w] / (1 - sin[w]), -1 / cos[w], 2 / cos[w], 2 / cos[w], (w - 90), (180 - 2 * w));

                        g.DrawArc(w % 10 == 0 ? pen10 : pen1, -tan[w], -cos[w] / (1 - sin[w]), 2 * tan[w], 2 * tan[w], w, (180 - 2 * w));
                        g.DrawArc(w % 10 == 0 ? pen10 : pen1, -tan[w], cos[w] / (1 + sin[w]), 2 * tan[w], 2 * tan[w], (w + 180), (180 - 2 * w));
                    }
                }
            }
            else//Schmidtネット
            {
                var div = 1000;
                PointD[] pt1 = new PointD[div], pt2 = new PointD[div], pt3 = new PointD[div], pt4 = new PointD[div];
                double[] cos2 = new double[div], sin2 = new double[div];

                for (int i = 0; i < div; i++)
                {
                    double d = (double)i / div * Math.PI - Math.PI / 2;
                    cos2[i] = Math.Cos(d);
                    sin2[i] = Math.Sin(d);
                }

                for (int n = 0; n < wList.Count; n++)
                {
                    int w = wList[n];
                    if (checkBox1DegLine.Checked || w % 10 == 0)
                    {
                        for (int i = 0; i < div; i++)
                        {
                            pt1[i] = 1 / Math.Sqrt(1 + cos2[i] * sin[w]) * new PointD(sin2[i] * sin[w], cos[w]);
                            pt2[i] = -pt1[i];
                            pt3[i] = 1 / Math.Sqrt(1 + cos[w] * cos2[i]) * new PointD(sin[w] * cos2[i], -sin2[i]);
                            pt4[i] = -pt3[i];
                        }
                        g.DrawLines(w % 10 == 0 ? pen10 : pen1, pt1);
                        g.DrawLines(w % 10 == 0 ? pen10 : pen1, pt2);
                        g.DrawLines(w % 10 == 0 ? pen10 : pen1, pt3);
                        g.DrawLines(w % 10 == 0 ? pen10 : pen1, pt4);
                    }
                }
            }
        }
        else//極モードのとき
        {
            var wList = new List<int>();
            for (int i = 1; i < 180; i++)
                if (i % 10 != 0)
                    wList.Add(i);
            for (int i = 10; i < 180; i += 10)
                wList.Add(i);

            if (radioButtonWulff.Checked)//Wulffネット
            {
                for (int n = 0; n < wList.Count; n++)
                {
                    int w = wList[n];
                    if (this.checkBox1DegLine.Checked || w % 10 == 0)
                    {
                        g.DrawLine(w % 10 == 0 ? pen10 : pen1, -cos[w], -sin[w], cos[w], sin[w]);
                        if (w < 90)
                        {
                            double theta = w * Math.PI / 180.0;
                            g.DrawArc(w % 10 == 0 ? pen10 : pen1, -Math.Tan(theta / 2), -Math.Tan(theta / 2), 2 * Math.Tan(theta / 2), 2 * Math.Tan(theta / 2), 0, 360);
                        }
                    }
                }
            }

            else//Schmidtネット
            {
                for (int n = 0; n < wList.Count; n++)
                {
                    int w = wList[n];
                    if (this.checkBox1DegLine.Checked || w % 10 == 0)
                    {
                        double theta = w * Math.PI / 180.0;
                        g.DrawLine(w % 10 == 0 ? pen10 : pen1, -cos[w], -sin[w], cos[w], sin[w]);
                        if (w < 90)
                        {
                            double radius = Math.Sin(Math.PI / 4 - theta / 2) * Math.Sqrt(2);
                            g.DrawArc(w % 10 == 0 ? pen10 : pen1, -radius, -radius, 2 * radius, 2 * radius, 0, 360);
                        }
                    }
                }
            }
        }
        g.DrawArc(pen90, -1f, -1f, 2f, 2f, 0, 360);
        g.DrawLine(pen90, 0f, -1f, 0f, 1f);
        g.DrawLine(pen90, -1f, 0f, 1f, 0f);
    }

    /// <summary>ステレオネット中の点を描く</summary>
    /// <param name="g"></param>
    private void DrawStereoNet(Graphics g)
    {
        if (formMain == null || formMain.Crystal == null || formMain.Crystal.A * formMain.Crystal.B * formMain.Crystal.C == 0)
            return;
        var crystal = formMain.Crystal;
        var matBase = radioButtonAxes.Checked ? crystal.RotationMatrix * crystal.MatrixReal : crystal.RotationMatrix * crystal.MatrixInverseTransposed;

        var drawString = trackBarStrSize.Value != 1 && checkBoxShowIndexLabels.Checked;
        using var font = new Font("Times New Roman", trackBarStrSize.Value / (float)mag / 7f);

        Func<Vector3DBase, PointD> conv = radioButtonWulff.Checked ? Stereonet.ConvertVectorToWulff : Stereonet.ConvertVectorToSchmidt;

        foreach (var obj in ProjectedObjects)
        {
            // 260528Cl 変更: ラベル色指定モード (checkBoxSpecifyLabelColor) を追加 + GDI+ を using 化
            // textBrush は false 時に brush をエイリアスし二重 using となるが SolidBrush.Dispose は冪等のため安全
            using var brush = new SolidBrush(Color.FromArgb(obj.ARGB));
            using var textBrush = checkBoxSpecifyLabelColor.Checked ? new SolidBrush(colorControlIndexLabel.Color) : brush;
            using var pen = new Pen(Color.FromArgb(obj.ARGB), 2f / (float)mag);
            var radius = pointSize / mag;
            if (radioButtonPlanes.Checked && checkBoxReflectStructureFactor.Checked)
                radius *= Sqrt(obj.Intensity);

            foreach (var index in obj.Indices)
            {
                //晶帯軸モードか、結晶面モードのとき
                if (radioButtonAxes.Checked || radioButtonPlanes.Checked)
                {
                    var v = matBase * index;
                    if (radioButtonLowerSphere.Checked)
                        v.Z = -v.Z;
                    var srcPt = conv(v);

                    if (!formMain.YusaGonioMode)
                    {
                        if (srcPt.X * srcPt.X + srcPt.Y * srcPt.Y <= 1.2)
                        {
                            if (radioButtonUpperSphere.Checked)
                                g.FillEllipse(brush, new RectangleF((float)(srcPt.X - radius), (float)(-srcPt.Y - radius), (float)(radius * 2), (float)(radius * 2)));
                            else
                                g.DrawEllipse(pen, new RectangleF((float)(srcPt.X - radius), (float)(-srcPt.Y - radius), (float)(radius * 2), (float)(radius * 2)));

                            if (drawString)
                            {
                                string str;
                                if (MillerBravaisActive && radioButtonPlanes.Checked)
                                    str = $"({index.X}{delimiter}{index.Y}{delimiter}{-index.X - index.Y}{delimiter}{index.Z})";
                                else
                                    str = radioButtonAxes.Checked ? $"[{index.X}{delimiter}{index.Y}{delimiter}{index.Z}]" : $"({index.X}{delimiter}{index.Y}{delimiter}{index.Z})";

                                g.DrawString(str, font, textBrush, (float)(srcPt.X + radius), (float)(-srcPt.Y + radius));
                            }
                        }
                    }
                    else//YusaGonio
                    {
                        //positionRecorder[n].Add(srcPt);
                        //for (int i = 0; i < positionRecorder[n].Count; i++)
                        //{
                        //    g.FillEllipse(brushGeneral, new RectangleF((float)(positionRecorder[n][i].X - radius / 2), (float)(-positionRecorder[n][i].Y - radius / 2), (float)radius, (float)radius));
                        //    if (i != 0)
                        //        g.DrawLine(new Pen(brushGeneral, 1f), positionRecorder[n][i - 1].X, positionRecorder[n][i - 1].Y, positionRecorder[n][i].X, positionRecorder[n][i].Y);
                        //}
                    }
                }
                //菊池線モードのとき
                else
                {
                    var v = matBase * index;
                    if (radioButtonLowerSphere.Checked)
                        v.Z = -v.Z;

                    // Z軸を vに傾ける行列を計算
                    var mat = GLGeometry.CreateRotationToZ(new V3(v.X, v.Y, v.Z)).ToMatrix3D();
                    double sinθ = waveLengthControl.WaveLength * v.Length / 2, cosθ = Math.Sqrt(1 - sinθ * sinθ);

                    // 260528Cl 変更: LINQ チェーンを素朴 for ループ化 (foreach×index 毎の enumerator alloc を削減)
                    var pts = new PointF[sin.Count];
                    for (int i = 0; i < sin.Count; i++)
                    {
                        var p = conv(mat * new Vector3D(cosθ * sin[i], cosθ * cos[i], sinθ));
                        pts[i] = new PointF(-(float)p.X, (float)p.Y);
                    }

                    //有効な点を抽出
                    int validStart1 = -1, validStart2 = -1, validEnd1 = -1, validEnd2 = -1;
                    for (int i = 0; i < pts.Length; i++)
                        if (pts[i].X * pts[i].X + pts[i].Y * pts[i].Y <= 1.05)
                        {
                            if (validStart1 == -1 && validStart2 == -1)
                                validStart1 = i;
                            if (validStart1 != -1 && validStart2 == -1 && (validEnd1 == -1 || i - validEnd1 == 1))
                                validEnd1 = i;

                            if (validStart1 != -1 && validStart2 == -1 && validEnd1 != i)
                                validStart2 = i;
                            if (validStart2 != -1)
                                validEnd2 = i;
                        }

                    var col = checkBoxReflectStructureFactor.Checked ? Color.FromArgb((int)(obj.Intensity * 255), obj.Color) : obj.Color;
                    using var penKikuchi = new Pen(col, 2f / (float)mag);
                    if (validEnd1 - validStart1 != 0)
                    {
                        pts = validStart1 != -1 && validStart2 == -1 ? pts[validStart1..(validEnd1 + 1)] : [.. pts[validStart2..(validEnd2 + 1)], .. pts[validStart1..(validEnd1 + 1)]];
                        g.DrawLines(penKikuchi, pts);
                    }
                }
            }
        }

        //菊池線モードのときは晶帯軸の指数を記入
        if (radioButtonKikuchiLinePairs.Checked && drawString)
        {
            var allIndices = ProjectedObjects.SelectMany(obj => obj.Indices).ToArray();

            var dic = new Dictionary<(int u, int v, int w), int>();
            for (int i = 0; i < allIndices.Length - 1; i++)
                for (int j = i + 1; j < allIndices.Length; j++)
                {
                    var (h1, k1, l1) = allIndices[i];
                    var (h2, k2, l2) = allIndices[j];
                    var (u, v, w) = (k1 * l2 - l1 * k2, l1 * h2 - h1 * l2, h1 * k2 - k1 * h2);
                    if (u != 0 || v != 0 || w != 0)
                    {
                        var n = Algebra.Irreducible(u, v, w);
                        u /= n; v /= n; w /= n;
                        if (u < 0 || (u == 0 && v < 0) || (u == 0 && v == 0 && w < 0))
                        { u = -u; v = -v; w = -w; }

                        //260317Cl 変更: ContainsKey+indexer → TryGetValue
                        if (dic.TryGetValue((u, v, w), out var cnt))
                            dic[(u, v, w)] = cnt + 1;
                        else
                            dic.Add((u, v, w), 1);
                    }
                }

            var list = dic.OrderByDescending(e => e.Value).ToArray();
            var max = Min(allIndices.Length / 2, list.Length);
            while (max + 1 < list.Length && list[max - 1].Value == list[max].Value)
                max++;

            var radius = (float)(trackBarKikuchiPointSize.Value / mag / 2.0);
            using var brushLabel = new SolidBrush(colorControlKikuchiLabel.Color);
            using var brushPt = new SolidBrush(colorControlKikuchiPoints.Color);
            using var penPt = new Pen(colorControlKikuchiPoints.Color, 2f / (float)mag);
            var matReal = crystal.RotationMatrix * crystal.MatrixReal;
            for (int i = 0; i < max; i++)
            {
                var (u, v, w) = list[i].Key;
                for (int j = 0; j < 2; j++)
                {
                    if (j == 1)
                        (u, v, w) = (-u, -v, -w);
                    var vec = matReal * (u, v, w);
                    if (radioButtonLowerSphere.Checked)
                        vec.Z = -vec.Z;
                    var pt = conv(vec).ToPointF();

                    if (pt.X * pt.X + pt.Y * pt.Y <= 1.05)
                    {
                        if (drawString)
                            g.DrawString($"[{u}{delimiter}{v}{delimiter}{w}]", font, brushLabel, pt.X, -pt.Y);
                        if (radioButtonUpperSphere.Checked)
                            g.FillEllipse(brushPt, new RectangleF(pt.X - radius, -pt.Y - radius, radius * 2, radius * 2));
                        else
                            g.DrawEllipse(penPt, new RectangleF(pt.X - radius, -pt.Y - radius, radius * 2, radius * 2));
                    }
                }
            }
        }
    }

    /// <summary>大円描画</summary>
    /// <param name="g"></param>
    private void DrawCircles(Graphics g)
    {
        float width;
        // 260528Cl 変更: trackBarGreatCircleLineWidth で太さ可変化 (旧: 固定 0.002f, mag 非依存)
        var penWidth = trackBarGreatCircleLineWidth.Value / mag / 2.0;
        using var pen = new Pen(colorControlGreatCircle.Color, (float)penWidth);

        foreach (Vector3D v in checkedListBoxCircles.CheckedItems) // 260517Cl 型付き foreach にしてキャストを省略
        {
            var vec = Vector3D.Normarize(formMain.Crystal.RotationMatrix * v);
            if (Abs(vec.Z) > 0.9999)//大円が最外周とほぼ一致するときは
            {
                if (vec.Z > 0)
                {
                    width = (float)(1 / vec.Z);
                    g.DrawArc(pen, (float)((vec.X - 1) / vec.Z), (float)((-vec.Y - 1) / vec.Z), 2 * width, 2 * width, 0, 360);
                }
                else
                {
                    vec = -vec;
                    width = (float)(1 / vec.Z);
                    g.DrawArc(pen, (float)((vec.X - 1) / vec.Z), (float)((-vec.Y - 1) / vec.Z), 2 * width, 2 * width, 0, 360);
                }
            }
            else if (vec.Z > 0.00000000001)
            {
                width = (float)(1 / vec.Z);//これが正じゃないとだめ
                g.DrawArc(pen, (float)((vec.X - 1) / vec.Z), (float)((-vec.Y - 1) / vec.Z), 2 * width, 2 * width,
                    (float)((-Atan2(-vec.Y, -vec.X) - Asin(vec.Z)) / PI * 180), (float)(2 * Asin(vec.Z) / PI * 180));
            }
            else if (vec.Z < -0.00000000001)
            {
                vec = -vec;
                width = (float)(1 / vec.Z);
                g.DrawArc(pen, (float)((vec.X - 1) / vec.Z), (float)((-vec.Y - 1) / vec.Z), 2 * width, 2 * width,
                    (float)((-Atan2(-vec.Y, -vec.X) - Asin(vec.Z)) / PI * 180), (float)(2 * Asin(vec.Z) / PI * 180));
            }
            else
            {
                var sqrt = Sqrt(vec.X * vec.X + vec.Y * vec.Y);
                g.DrawLine(pen, (float)(vec.Y / sqrt), (float)(vec.X / sqrt), (float)(-vec.Y / sqrt), (float)(-vec.X / sqrt));
            }
        }
    }


    #endregion

    #region 座標変換

    //Src（単位なし）をClient(pixel)に変換
    private PointF convertSrcToClient(PointD pt)
        => new((float)(graphicsBox.ClientSize.Width / 2.0 + mag * (pt.X - centerPt.X)), (float)(graphicsBox.ClientSize.Height / 2.0 + mag * (pt.Y - centerPt.Y)));

    // private PointF convertSrcToClient(double x, double y) => convertSrcToClient(new PointD(x, y)); // (260322Ch) 旧実装: 未使用の短い overload helper

    private PointD convertClientToSrc(Point pt)
        => new(centerPt.X + (pt.X - graphicsBox.ClientSize.Width / 2) / mag, centerPt.Y + (graphicsBox.ClientSize.Height / 2 - pt.Y) / mag);


    /// <summary> ステレオネット上の点に対応する 3D 単位ベクトル (上半球) を返す。Schmidt のみ有効域外 (ρ²>2) で null、Wulff は常に non-null。 </summary>
    private Vector3DBase srcToSphere(PointD p) // 260517Cl 追加: 投影逆変換を MouseMove と FindIndex で共有
    {
        double X = p.X, Y = p.Y, rho2 = X * X + Y * Y;
        if (radioButtonWulff.Checked) // Wulff (stereographic) — 260517Cl alloc を 1 回に絞るため除算をスカラで先に計算
        {
            double d = 1 + rho2;
            return new Vector3DBase(2 * X / d, 2 * Y / d, (1 - rho2) / d);
        }
        if (rho2 > 2) return null;
        double s = Sqrt(2 - rho2);
        return new Vector3DBase(X * s, Y * s, 1 - rho2); // Schmidt (equal-area)
    }

    #endregion

    #region ピクチャーボックスのイベント関連

    private void graphicsBox_MouseDown(object sender, MouseEventArgs e)
    {
        graphicsBox.Focus();
        if (e.Button == MouseButtons.Right)
        {
            MouseRangingMode = true;
            MouseRangeStart = new Point(e.X, e.Y);
            if (MouseRangeEnd.X == -1 && MouseRangeEnd.Y == -1)
                MouseRangeEnd = new Point(e.X, e.Y);
            return;
        }
        if (e.Button == MouseButtons.Left && e.Clicks == 2)
        {
            if (radioButtonPlanes.Checked) radioButtonAxes.Checked = true;
            else
                radioButtonPlanes.Checked = true;
        }
    }

    private void graphicsBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (MouseRangingMode)
        {
            MouseRangingMode = false;
            MouseRangeEnd = new Point(e.X, e.Y);
            var ptStart = convertClientToSrc(MouseRangeStart);
            var ptEnd = convertClientToSrc(MouseRangeEnd);

            if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) < 2 && Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) < 2)
            {//選択範囲があまりに小さすぎたら縮小
                centerPt = (ptStart + ptEnd) / 2;
                mag *= 0.5;
                if (mag < Math.Min(graphicsBox.ClientSize.Width / 2.4, graphicsBox.ClientSize.Height / 2.4))
                {
                    centerPt = new PointD(0, 0);
                    mag = Math.Min(graphicsBox.ClientSize.Width / 2.4, graphicsBox.ClientSize.Height / 2.4);
                }
            }
            else if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) > 10 && Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) > 10)
            {
                //現在のmagと中心位置から、新しいmagと中心位置を決定する

                centerPt = (ptStart + ptEnd) / 2;
                mag = (graphicsBox.Width / Math.Abs(ptStart.X - ptEnd.X) + graphicsBox.Height / Math.Abs(ptStart.Y - ptEnd.Y)) / 2;
                if (mag > 10000)
                    mag = 10000;
            }
            Draw();
            MouseRangeEnd = new Point(-1, -1);
        }
        //formMain.ChangeRotMatrix(this);
    }

    private PointD lastMousePositionSrc = new();
    private Point lastMousePositionClient = new();

    private void graphicsBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        if (e.X > tabControl.Width || e.Y > tabControl.Height - 20)
        {
            splitContainer1.BringToFront();
            graphicsBox.Refresh();
        }
        // PointD pt = convertClientToSrc(e.X, e.Y); ; // (260322Ch) 旧実装: int overload helper を経由していた
        var pt = convertClientToSrc(new Point(e.X, e.Y)); ; // (260322Ch) その場で Point を作って直接変換する
        // 260517Cl 旧実装 (Wulff 公式固定で Schmidt 時に誤値): srcToSphere() に置換
        // var azimuth = Math.Asin(2 * pt.Y / (1 + pt.X * pt.X + pt.Y * pt.Y));
        // var tilt = (Math.Cos(azimuth) != 0) ? Math.Asin(2 * pt.X / (1 + pt.X * pt.X + pt.Y * pt.Y) / Math.Cos(azimuth)) : 0;
        var vSphere = srcToSphere(pt);
        if (vSphere != null)
        {
            var azimuth = Asin(vSphere.Y);
            var tilt = Cos(azimuth) != 0 ? Asin(vSphere.X / Cos(azimuth)) : 0;
            labelXYpos.Text = $"{tilt / PI * 180:f3}° / {azimuth / PI * 180:f3}°";
            var (axis, plane) = FindIndex(vSphere);
            // 260517Cl Miller-Bravais 表示時 (hex/trigonal) は面指数を 4 指数 (h k -h-k l) に
            labelAxisPlane.Text = $"[{axis.U} {axis.V} {axis.W}] / ({FormMain.PlaneString(plane.H, plane.K, plane.L, MillerBravaisActive)})";
        }

        //真ん中ボタンが押されながらマウスが動いたとき
        if (e.Button == MouseButtons.Middle)
        {
            centerPt += new PointD((lastMousePositionClient.X - e.X) / mag, (-lastMousePositionClient.Y + e.Y) / mag);
            Draw();
        }

        //左ボタンが押されながらマウスが動いたとき
        if (e.Button == MouseButtons.Left)
        {
            if (formMain.YusaGonioMode)
            {
                formMain.YusaGonioMode = false;
                setVector();
            }
            if ((e.X - graphicsBox.ClientSize.Width / 2) * (e.X - graphicsBox.ClientSize.Width / 2) + (e.Y - graphicsBox.ClientSize.Height / 2) * (e.Y - graphicsBox.ClientSize.Height / 2)
                < Math.Min(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height) * Math.Min(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height) * 0.18)
                formMain.Rotate((-pt.Y + lastMousePositionSrc.Y, pt.X - lastMousePositionSrc.X, 0), Vector3D.AngleBetStereoNetPoints(pt, lastMousePositionSrc));
            else
                formMain.Rotate((0, 0, 1), Math.Atan2(lastMousePositionSrc.X, lastMousePositionSrc.Y) - Math.Atan2(pt.X, pt.Y));

            //if(lastMousePositionSrc.X != pt.X || lastMousePositionSrc.Y!=pt.Y)
            //    Draw();
        }

        if (e.Button == MouseButtons.Right && MouseRangingMode)
        {
            MouseRangeEnd = new Point(e.X, e.Y);
            Draw();
        }

        lastMousePositionSrc = pt;
        lastMousePositionClient = new Point(e.X, e.Y);
    }

    #endregion ピクチャーボックスのイベント関連

    #region タブコントロールのイベント
    private void tabControl_Click(object sender, EventArgs e)
    {
        splitContainer1.SendToBack();
        graphicsBox.Refresh();
    }

    #region Appearanceタブ関連
    private void trackBarStrSize_Scroll(object sender, EventArgs e)
    {
        strFont = new Font("Tahoma", trackBarStrSize.Value / 9f);
        pointSize = trackBarPointSize.Value;
        Draw();
    }
    private void radioButtonOutlineEquator_CheckedChanged(object sender, EventArgs e) => Draw();

    private void checkBox1DegLine_CheckedChanged(object sender, EventArgs e) => Draw();
    private void colorControl_ColorChanged(object sender, EventArgs e) => Draw();
    #endregion

    #region 大円タブ関連
    private void buttonAddCircle_Click(object sender, EventArgs e)
    {
        if (radioButtonCircleByAxis.Checked)
        {
            var (u, v, w) = indexControlAxis.Values;
            if (u == 0 && v == 0 && w == 0) return;
            var vec = new Vector3D(u * formMain.Crystal.A_Axis + v * formMain.Crystal.B_Axis + w * formMain.Crystal.C_Axis) { Text = $"[{u} {v} {w}]" };
            formMain.Crystal.VectorOfPole.Add(vec);
            suppressItemCheck = true; // 260517Cl 直後の Draw() が回るので ItemCheck の BeginInvoke は抑止
            try { checkedListBoxCircles.Items.Add(vec, true); }
            finally { suppressItemCheck = false; }
            Draw();
        }
        else if (radioButtonCircleByPlanes.Checked)
        {
            var (h1, k1, l1) = indexControlCirclePlane1.Values;
            var (h2, k2, l2) = indexControlCirclePlane2.Values;

            var u = k1 * l2 - k2 * l1;
            var v = l1 * h2 - l2 * h1;
            var w = h1 * k2 - h2 * k1;
            if (u == 0 && v == 0 && w == 0) return;

            var vec = new Vector3D(u * formMain.Crystal.A_Axis + v * formMain.Crystal.B_Axis + w * formMain.Crystal.C_Axis)
            { Text = $"({FormMain.PlaneString(h1, k1, l1, MillerBravaisActive)}) & ({FormMain.PlaneString(h2, k2, l2, MillerBravaisActive)})" };

            formMain.Crystal.VectorOfPole.Add(vec);
            suppressItemCheck = true; // 260517Cl 直後の Draw() が回るので ItemCheck の BeginInvoke は抑止
            try { checkedListBoxCircles.Items.Add(vec, true); }
            finally { suppressItemCheck = false; }
            Draw();
        }
    }

    private void buttonDeleteCircle_Click(object sender, EventArgs e)
    {
        var i = checkedListBoxCircles.SelectedIndex;
        if (i > -1)
        {
            formMain.Crystal.VectorOfPole.Remove((Vector3D)checkedListBoxCircles.SelectedItem);
            checkedListBoxCircles.Items.RemoveAt(checkedListBoxCircles.SelectedIndex);

            if (checkedListBoxCircles.Items.Count > i)
                checkedListBoxCircles.SelectedIndex = i;
            else
                checkedListBoxCircles.SelectedIndex = i - 1;
        }
    }

    private void radioButtonCircleByAxis_CheckedChanged(object sender, EventArgs e)
    {
        flowLayoutPanelCircleAxis.Enabled = radioButtonCircleByAxis.Checked;
        flowLayoutPanelCirclePlanes.Enabled = radioButtonCircleByPlanes.Checked;
    }

    private bool suppressItemCheck; // 260517Cl 追加 プログラム的に Items.Add(item, true) する際に ItemCheck からの冗長な Draw を抑止

    private void checkedListBoxCircles_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (suppressItemCheck) return; // 260517Cl SetCrystal/buttonAddCircle の populate 中はスキップ（呼び出し元が明示的に Draw する）
        // ItemCheck はチェック変更前に呼ばれるので、BeginInvokeを経由する
        BeginInvoke(() => Draw());
    }

    #endregion

    #endregion

    #region メインで結晶が変更されたとき
    public void SetCrystal()
    {
        if (formMain.Crystal == null)
            return;

        UpdatePlaneIndices(redraw: false); // 260517Cl 末尾の Draw() で再描画するので、ここでは描画しない

        setVector();
        //formMain.Crystal.SetVectorOfAxis((int)numericBox1.Value, (int)numericBox2.Value, (int)numericBox3.Value);
        //formMain.Crystal.SetVectorOfPlane((int)numericBox1.Value, (int)numericBox2.Value, (int)numericBox3.Value);

        checkedListBoxCircles.Items.Clear();
        if (formMain.Crystal.VectorOfPole == null)
            formMain.Crystal.VectorOfPole = [];
        else
        {
            suppressItemCheck = true; // 260517Cl 末尾の Draw() で 1 回だけ再描画する。ItemCheck 経由の N 回の重複再描画を抑止
            try
            {
                for (int i = 0; i < formMain.Crystal.VectorOfPole.Count; i++)
                    checkedListBoxCircles.Items.Add(formMain.Crystal.VectorOfPole[i], true);
            }
            finally { suppressItemCheck = false; }
        }
        Draw();
    }
    #endregion

    #region 描画対象(面・軸・菊池線)や投影方法(等積・等角)が変更されたとき
    private void radioButtonAxes_CheckedChanged(object sender, EventArgs e)
    {
        if (!((RadioButton)sender).Checked) return;

        UpdatePlaneIndices(redraw: false); // 260517Cl 末尾の Draw() で再描画するので、ここでは描画しない

        if (radioButtonAxes.Checked)
        {
            indexControlDrawing.Mode = IndexControl.ModeEnum.Axis;
            radioButtonHighStructureFactor.Visible = false;
            if (radioButtonHighStructureFactor.Checked)
                radioButtonRange.Checked = true;
            checkBoxReflectStructureFactor.Enabled = false;
        }
        else
        {
            indexControlDrawing.Mode = IndexControl.ModeEnum.Plane;
            radioButtonHighStructureFactor.Visible = true;
            checkBoxReflectStructureFactor.Enabled = true;
        }
        //260606Cl checkBoxAnomalousDispersion.Enabled は下の setVector() で一元管理(Axes/Plane どちらでもここでは設定しない。設定しても setVector() に上書きされ競合するため)
        setVector();
        Draw();
    }
    private void radioButtonWulff_CheckedChanged(object sender, EventArgs e)
    {
        Draw(null, true);
    }

    private void checkBoxReflectStructureFactor_CheckedChanged(object sender, EventArgs e)
    {
        setVector();
        Draw();
    }
    private void checkBoxShowIndexLabels_CheckedChanged(object sender, EventArgs e)
    {
        Draw();
    }

    #endregion

    #region ファイルメニュー
    private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var bmp = new Bitmap(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height);
        var g = Graphics.FromImage(bmp);
        Draw(g);
        if (bmp != null)
        {
            var dialog = new SaveFileDialog() { Filter = "Picture File[*.png]|*.png;" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var filename = dialog.FileName;
                if (!filename.EndsWith(".png")) filename += ".png";
                bmp.Save(filename, ImageFormat.Png);
            }
        }
    }

    private void copyImageToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var bmp = new Bitmap(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height);
        var g = Graphics.FromImage(bmp);
        Draw(g);
        if (bmp != null)
            Clipboard.SetDataObject(bmp);
    }

    private void copyMetafileToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var grfx = CreateGraphics();
        var ipHdc = grfx.GetHdc();
        var ms = new MemoryStream();
        var mf = new Metafile(ms, ipHdc, EmfType.EmfPlusDual);
        grfx.ReleaseHdc(ipHdc);
        grfx.Dispose();
        var g = Graphics.FromImage(mf);
        Draw(g);
        g.Dispose();
        ClipboardMetafileHelper.PutEnhMetafileOnClipboard(this.Handle, mf);
    }

    private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            printDocument1.PrinterSettings = pageSetupDialog1.PrinterSettings;
    }

    // 印刷プレビューを表示
    private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e) => printPreviewDialog1.ShowDialog();

    private void printToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (printDialog1.ShowDialog() == DialogResult.OK)
            printDocument1.Print();
    }

    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        System.Drawing.Printing.PageSettings ps = printDocument1.PrinterSettings.DefaultPageSettings;
        //用紙サイズ取得 このサイズは1/100インチ
        var height = (ps.PaperSize.Height - ps.Margins.Top - ps.Margins.Bottom) / 100f;
        var width = (ps.PaperSize.Width - ps.Margins.Left - ps.Margins.Right) / 100f;

        if (printDocument1.PrinterSettings.DefaultPageSettings.Landscape)
        {//縦横を逆転
            (height, width) = (width, height);
        }
        //解像度300dpiのときのイメージサイズは
        //glString.Font = new Font("Tahoma", trackBarStrSize.Value / 10f / 72f * 300f);
        // var bmp = (Bitmap)graphicsBox.Image; // (260322Ch) 旧 GraphicsBox では Image から現在表示を取れていた
        // using var bmp = graphicsBox.RenderedImage; // (260322Ch) GraphicBox2 仮名時点の説明
        using var bmp = graphicsBox.RenderedImage; // (260322Ch) GraphicBox では Image と Graphics レイヤーの合成結果を明示的に取得する
        bmp.SetResolution(300, 300);

        e.Graphics.PageUnit = GraphicsUnit.Inch;
        e.Graphics.DrawImage(bmp, new PointF(ps.Margins.Top / 100f, ps.Margins.Left / 100f));
        e.HasMorePages = false;

        //glString.Font = new Font("Tahoma", trackBarStrSize.Value / 10f);
    }

    private void toolStripMenuItemSaveMovie3D_Click(object sender, EventArgs e)
    {
        formMain.FormMovie.Execute(glControl, this);
    }
    private void toolStripMenuItemSaveMovieStereonet_Click(object sender, EventArgs e)
    {
        var func = new Func<Bitmap>(() =>
        {
            var bmp = new Bitmap(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height);
            var g = Graphics.FromImage(bmp);
            Draw(g, true);
            return bmp;
        });
        formMain.FormMovie.Execute(func, this);
    }

    #endregion

    #region YusaGonio

    private List<List<PointD>> positionRecorder = [];
    private void buttonYusaModeStart_Click(object sender, EventArgs e)
    {
        setVector();
        positionRecorder = [];
        for (int i = 0; i < (radioButtonPlanes.Checked ? formMain.Crystal.VectorOfPlane.Count : formMain.Crystal.VectorOfAxis.Count); i++)
            positionRecorder.Add([]);

        //formMain.OriginalRotation = (Matrix3D)formMain.RotMatrix.Clone();
        formMain.YusaGonioMode = true;
        formMain.timer.Start();
    }

    public void buttonYusaModeStop_Click(object sender, EventArgs e)
    {
        formMain.timer.Stop();

        var sb = new StringBuilder();
        if (positionRecorder.Count > 0)
        {
            for (int i = 0; i < positionRecorder.Count; i++)
            {
                var v = radioButtonPlanes.Checked ? formMain.Crystal.VectorOfPlane[i] : formMain.Crystal.VectorOfAxis[i];
                sb.Append(v.ToString() + "\t\t");
            }
            sb.Append("\r\n");

            for (int j = 0; j < positionRecorder[0].Count; j++)
            {
                for (int i = 0; i < positionRecorder.Count; i++)
                    sb.Append(positionRecorder[i][j].X.ToString() + "\t" + positionRecorder[i][j].Y.ToString() + "\t\t");
                sb.Append("\r\n");
            }
            Clipboard.SetDataObject(sb.ToString());
        }
    }
    #endregion

    #region 描画対象の面・軸指数の設定
    private void radioButtonRange_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is RadioButton rb)
        {
            if (rb.Name == radioButtonSpecifiedIndices.Name)//SpecifiedIndicesを適切に保存・復元
            {
                if (rb.Checked == false)
                    SpecifiedIndices = ProjectedObjects;
                else
                    ProjectedObjects = SpecifiedIndices;
            }
            if (!rb.Checked) return;//チェックされたとき以外は無視
        }

        UpdatePlaneIndices(redraw: false); // 260517Cl 末尾の Draw() で再描画するので、ここでは描画しない

        indexControlDrawing.PlusMinus = radioButtonRange.Checked;

        if (radioButtonRange.Checked)
        {
            indexControlDrawing.Visible = true;
            numericBoxHighStructureFactor.Visible = false;
            buttonRemoveIndex.Visible = buttonAddIndex.Visible = false;
            checkBoxIncludingEquivalentPlanes.Visible = false;
        }
        else if (radioButtonSpecifiedIndices.Checked)
        {
            indexControlDrawing.Visible = true;
            numericBoxHighStructureFactor.Visible = false;
            buttonRemoveIndex.Visible = buttonAddIndex.Visible = true;
            checkBoxIncludingEquivalentPlanes.Visible = true;
        }
        else//構造因子順の場合
        {
            indexControlDrawing.Visible = false;
            numericBoxHighStructureFactor.Visible = true;
            buttonRemoveIndex.Visible = buttonAddIndex.Visible = false;
            checkBoxIncludingEquivalentPlanes.Visible = false;
        }
        setVector();
        Draw();
    }

    //指数範囲が変更されたとき
    private void numericUpDown_ValueChanged(object sender, EventArgs e)
    {
        setVector();
        Draw();
    }

    private void checkBoxIncludingEquivalentPlanes_CheckedChanged(object sender, EventArgs e)
    {
        setVector();
        Draw();
    }

    private void numericBoxHighStructureFactor_ValueChanged(object sender, EventArgs e)
    {
        setVector();
        Draw();
    }

    private void radioButtonUpperSphere_CheckedChanged(object sender, EventArgs e) => Draw();

    private void waveLengthControl_WaveSourceChanged(object sender, EventArgs e)
    {
        setVector();
        Draw();
    }

    //260606Cl 追加: X線異常分散(f'/f'')の ON/OFF。OFF で従来動作(分散なし)に戻る。
    private void checkBoxAnomalousDispersion_CheckedChanged(object sender, EventArgs e)
    {
        setVector();
        Draw();
    }
    #endregion

    #region リストボックス近辺のイベント
    private void colorControlIndex_ColorChanged(object sender, EventArgs e)
    {
        if (skipEvent) return;
        foreach (var item in listBoxSpecifiedIndices.SelectedItems)
        {
            var row = (IndexInfo)item;
            row.ARGB = colorControlIndex.Color.ToArgb();
        }
        listBoxSpecifiedIndices.Refresh();
        Draw();
    }

    private void listBoxSpecifiedIndices_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listBoxSpecifiedIndices.SelectedIndex > -1)
        {
            var row = (IndexInfo)listBoxSpecifiedIndices.SelectedItem;
            skipEvent = true;
            colorControlIndex.Color = Color.FromArgb(row.ARGB);
            skipEvent = false;
        }
    }

    private void buttonAddIndex_Click(object sender, EventArgs e)
    {
        var (x, y, z) = indexControlDrawing.Values;
        if (x != 0 || y != 0 || z != 0)
        {
            var argb = checkBoxRotateColor.Checked ? ColorArray[ProjectedObjects.Count % ColorArray.Length] : colorControlIndex.Argb;
            ProjectedObjects.Add(new IndexInfo([(x, y, z)], argb));
        }
        setVector();
        Draw();
    }

    private void buttonRemoveIndex_Click(object sender, EventArgs e)
    {
        var selectedIndex = listBoxSpecifiedIndices.SelectedIndex;
        if (selectedIndex > -1 && selectedIndex < ProjectedObjects.Count)
            ProjectedObjects.RemoveAt(listBoxSpecifiedIndices.SelectedIndex);
        setVector();
        Draw();
    }

    /// <summary>指数リストボックスの項目描画</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void listBoxSpecifiedIndices_DrawItem(object sender, DrawItemEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        //背景を描画する
        //項目が選択されている時は強調表示される
        e.DrawBackground();

        var ih = listBoxSpecifiedIndices.ItemHeight;
        var margin = 1;

        //ListBoxが空のときにListBoxが選択されるとe.Indexが-1になる
        if (e.Index > -1)
        {
            var row = (IndexInfo)((ListBox)sender).Items[e.Index];
            //スポットの色を表示
            using var spotBrush = new SolidBrush(Color.FromArgb(row.ARGB));
            e.Graphics.FillRectangle(spotBrush, new Rectangle(e.Bounds.X + margin, e.Bounds.Y + margin, ih - margin, ih - margin));
            //文字を描画する色の選択
            using var b = new SolidBrush(e.ForeColor);

            //文字列の描画
            if (MillerBravaisActive && !radioButtonAxes.Checked)  //ミラー・ブレヴェ指数で表示
            {
                var (h, k, l) = row.Indices[0];
                e.Graphics.DrawString($"{h} {k} {-h - k} {l}", e.Font, b, new Rectangle(e.Bounds.X + ih, e.Bounds.Y, e.Bounds.Width - ih, e.Bounds.Height));
            }
            else //通常の表示
                e.Graphics.DrawString(row.ToString(), e.Font, b, new Rectangle(e.Bounds.X + ih, e.Bounds.Y, e.Bounds.Width - ih, e.Bounds.Height));
        }

        //フォーカスを示す四角形を描画
        e.DrawFocusRectangle();
    }

    #endregion

    #region 面、軸のベクトルを計算 SetVector
    private void setVector()
    {
        if (formMain.Crystal.A * formMain.Crystal.B * formMain.Crystal.C == 0)
            return;

        //260606Cl X線異常分散トグルの有効状態を更新。異常分散は X線のみ、かつ構造因子を計算する Plane モード(=軸モードでない)でのみ意味を持つ。
        //setVector は WaveSource 変更/Axes・Plane 切替/チェック操作など全イベントで呼ばれる単一ポイント → ここを Enabled の唯一の真実とする(radioButtonAxes_CheckedChanged 側の個別設定は撤去)。
        checkBoxAnomalousDispersion.Enabled = waveLengthControl.WaveSource == WaveSource.Xray && !radioButtonAxes.Checked;
        //260606Cl X線異常分散 f'/f'' を構造因子へ反映するか。上で設定した Enabled(X線・Plane モードのみ)に集約済みの条件を再利用し二重管理を避ける。
        bool useAnomalousDispersion = checkBoxAnomalousDispersion.Checked && checkBoxAnomalousDispersion.Enabled;

        if (!radioButtonHighStructureFactor.Checked)
        {
            //範囲指定モードの時
            if (radioButtonRange.Checked)
            {
                var (x, y, z) = indexControlDrawing.Values;

                ProjectedObjects = [];
                var hash = new HashSet<(int X, int Y, int Z)>();
                for (int i = -x; i <= x; i++)
                    for (int j = -y; j <= y; j++)
                        for (int k = -z; k <= z; k++)
                            if ((i != 0 || j != 0 || k != 0) && Algebra.Irreducible(i, j, k) == 1 && hash.Add((i, j, k)))
                            {
                                var indices = radioButtonAxes.Checked ?
                                SymmetryStatic.GenerateEquivalentAxes((i, j, k), formMain.Crystal.Symmetry, false) :
                                SymmetryStatic.GenerateEquivalentPlanes((i, j, k), formMain.Crystal.Symmetry, false);

                                foreach (var index in indices)
                                    hash.Add(index);

                                ProjectedObjects.Add(new IndexInfo(indices, 0));
                            }
                #region 結晶系に従ってソート
                var sysNum = formMain.Crystal.Symmetry.CrystalSystemNumber;
                if (sysNum == 4 || sysNum == 5 || sysNum == 6)//tetragonal x>=y>=z
                    ProjectedObjects.Sort((a, b) =>
                    {
                        var (X1, Y1, Z1) = a.Indices[0];
                        var (X2, Y2, Z2) = b.Indices[0];
                        if (Abs(X1) + Abs(Y1) + Abs(Z1) != Abs(X2) + Abs(Y2) + Abs(Z2)) return (Abs(X1) + Abs(Y1) + Abs(Z1)).CompareTo(Abs(X2) + Abs(Y2) + Abs(Z2));
                        else if (Z1 != Z2) return Z1.CompareTo(Z2);
                        else if (X1 != X2) return X1.CompareTo(X2);
                        else return Y1.CompareTo(Y2);
                    });
                else if (sysNum == 2)//tetragonal x>=y>=z
                {
                    switch (formMain.Crystal.Symmetry.MainAxis)
                    {
                        case "a":
                            ProjectedObjects.Sort((a, b) =>
                            {
                                var (X1, Y1, Z1) = a.Indices[0];
                                var (X2, Y2, Z2) = b.Indices[0];
                                if (Abs(X1) + Abs(Y1) + Abs(Z1) != Abs(X2) + Abs(Y2) + Abs(Z2)) return (Abs(X1) + Abs(Y1) + Abs(Z1)).CompareTo(Abs(X2) + Abs(Y2) + Abs(Z2));
                                else if (X1 != X2) return X1.CompareTo(X2);
                                else if (Y1 != Y2) return Y1.CompareTo(Y2);
                                else return Z1.CompareTo(Z2);
                            });
                            break;
                        case "b":
                            ProjectedObjects.Sort((a, b) =>
                            {
                                var (X1, Y1, Z1) = a.Indices[0];
                                var (X2, Y2, Z2) = b.Indices[0];
                                if (Abs(X1) + Abs(Y1) + Abs(Z1) != Abs(X2) + Abs(Y2) + Abs(Z2)) return (Abs(X1) + Abs(Y1) + Abs(Z1)).CompareTo(Abs(X2) + Abs(Y2) + Abs(Z2));
                                else if (Y1 != Y2) return Y1.CompareTo(Y2);
                                else if (Z1 != Z2) return Z1.CompareTo(Z2);
                                else return X1.CompareTo(X2);
                            });
                            break;
                        case "c":
                            ProjectedObjects.Sort((a, b) =>
                            {
                                var (X1, Y1, Z1) = a.Indices[0];
                                var (X2, Y2, Z2) = b.Indices[0];
                                if (Abs(X1) + Abs(Y1) + Abs(Z1) != Abs(X2) + Abs(Y2) + Abs(Z2)) return (Abs(X1) + Abs(Y1) + Abs(Z1)).CompareTo(Abs(X2) + Abs(Y2) + Abs(Z2));
                                else if (Z1 != Z2) return Z1.CompareTo(Z2);
                                else if (X1 != X2) return X1.CompareTo(X2);
                                else return Y1.CompareTo(Y2);
                            });
                            break;
                    }
                }
                else
                {
                    ProjectedObjects.Sort((a, b) =>
                    {
                        var (X1, Y1, Z1) = a.Indices[0];
                        var (X2, Y2, Z2) = b.Indices[0];
                        if (Abs(X1) + Abs(Y1) + Abs(Z1) != Abs(X2) + Abs(Y2) + Abs(Z2)) return (Abs(X1) + Abs(Y1) + Abs(Z1)).CompareTo(Abs(X2) + Abs(Y2) + Abs(Z2));
                        else if (X1 != X2) return X1.CompareTo(X2);
                        else if (Y1 != Y2) return Y1.CompareTo(Y2);
                        else return Z1.CompareTo(Z2);
                    });
                }
                #endregion

                for (int i = 0; i < ProjectedObjects.Count; i++)
                    ProjectedObjects[i].ARGB = checkBoxRotateColor.Checked ? ColorArray[i % ColorArray.Length] : colorControlIndex.Argb;
            }
            //特定指数モードの時
            else
            {
                foreach (var obj in ProjectedObjects)
                {
                    var (x, y, z) = obj.Indices[0];
                    if (checkBoxIncludingEquivalentPlanes.Checked)
                        obj.Indices = radioButtonAxes.Checked ?
                            SymmetryStatic.GenerateEquivalentAxes((x, y, z), formMain.Crystal.Symmetry, false, false) :
                            SymmetryStatic.GenerateEquivalentPlanes((x, y, z), formMain.Crystal.Symmetry, false, false);
                    else
                        obj.Indices = [(x, y, z)];
                }
            }

            if (!radioButtonAxes.Checked && ProjectedObjects.Count > 0)//軸モード出ないときは構造因子も計算
            {
                foreach (var obj in ProjectedObjects)
                    obj.Intensity = formMain.Crystal.GetStructureFactor(waveLengthControl.WaveSource, obj.Indices[0], xrayEnergyKeV: waveLengthControl.Energy, anomalousDispersion: useAnomalousDispersion).MagnitudeSquared();//260606Cl X線異常分散を反映(既定ON)
                var max = ProjectedObjects.Max(o => o.Intensity);
                foreach (var obj in ProjectedObjects)
                    obj.Intensity /= max;
            }

        }
        //構造因子順モードの時 (結晶面モードか菊池線モードのときしかこのモードにならない)
        else
        {
            int n = numericBoxHighStructureFactor.ValueInteger;

            formMain.Crystal.SetVectorOfG(0.0001, waveLengthControl.WaveSource, n * 40, xrayEnergyKeV: waveLengthControl.Energy, anomalousDispersion: useAnomalousDispersion);//260606Cl X線異常分散を反映(既定ON)
            var vec = formMain.Crystal.VectorOfG;

            Array.Sort(vec, (g1, g2) => g2.RawIntensity.CompareTo(g1.RawIntensity));
            var maxIntenxity = vec[0].RawIntensity;
            foreach (var v in vec)
                v.RelativeIntensity = v.RawIntensity / maxIntenxity;

            if (radioButtonPlanes.Checked)
                for (int i = 1; i < n * 10; i++)
                    for (int j = 0; j < i; j++)
                        if (!Crystal.CheckIrreducible(vec[i].Index, vec[j].Index))
                        {
                            vec[i].Flag1 = true;
                            break;
                        }

            ProjectedObjects = [];
            var hash = new HashSet<(int h, int k, int l)>();
            var count = 0;
            for (int i = 0; i < vec.Length; i++)
                if (!vec[i].Flag1 && count < n && hash.Add((vec[i].Index)))
                {
                    var indices = SymmetryStatic.GenerateEquivalentPlanes(vec[i].Index, formMain.Crystal.Symmetry, false, true);
                    foreach (var index in indices)
                        hash.Add(index);
                    var argb = checkBoxRotateColor.Checked ? ColorArray[ProjectedObjects.Count % ColorArray.Length] : colorControlIndex.Argb;
                    ProjectedObjects.Add(new IndexInfo(indices, argb, vec[i].RelativeIntensity));
                    count += indices.Length;
                }
        }

        //リストボックスを更新
        listBoxSpecifiedIndices.Items.Clear();
        foreach (var obj in ProjectedObjects)
            listBoxSpecifiedIndices.Items.Add(obj);
    }

    private void checkBoxRotateColor_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxRotateColor.Checked)
        {
            skipEvent = true;
            for (int i = 0; i < listBoxSpecifiedIndices.Items.Count; i++)
            {
                var row = (IndexInfo)listBoxSpecifiedIndices.Items[i];
                row.ARGB = ColorArray[i % ColorArray.Length];
                listBoxSpecifiedIndices.Refresh();
            }
            skipEvent = false;
            Draw();
        }
    }

    #endregion

    #region テストコード
    private void button1_Click(object sender, EventArgs e)
    {
        formMain.Crystal.VectorOfAxis = [];
        int div = 30;
        for (double i = -div; i < div; i++)
            for (double j = -div; j < div; j++)
            {
                double tilt = Math.PI * i / div;
                double azim = Math.PI * j / div;

                formMain.Crystal.VectorOfAxis.Add
                    (new Vector3D(Math.Sin(tilt) * Math.Sin(azim), Math.Cos(azim), Math.Cos(tilt) * Math.Sin(azim)));
            }

        /*   for (double i = 0; i < div; i++)
               for (double j = 0; j < div; j++)
                   formMain.crystal.VectorOfAxis.Add(new Vector3D(Math.Tan(((i + 0.5) / div - 0.5) * Math.PI / 2), Math.Tan(((j + 0.5) / div - 0.5) * Math.PI / 2), -1));
           for (double i = 0; i < div; i++)
               for (double j = 0; j < div; j++)

               {
                   formMain.crystal.VectorOfAxis.Add(new Vector3D(Math.Tan(((i + 0.5) / div - 0.5) * Math.PI / 2), Math.Tan(((j + 0.5) / div - 0.5) * Math.PI / 2), 1));
                   formMain.crystal.VectorOfAxis.Add(new Vector3D(Math.Tan(((i + 0.5) / div - 0.5) * Math.PI / 2), Math.Tan(((j + 0.5) / div - 0.5) * Math.PI / 2), -1));
                   formMain.crystal.VectorOfAxis.Add(new Vector3D(Math.Tan(((i + 0.5) / div - 0.5) * Math.PI / 2), 1, Math.Tan(((j + 0.5) / div - 0.5) * Math.PI / 2)));
                   formMain.crystal.VectorOfAxis.Add(new Vector3D(Math.Tan(((i + 0.5) / div - 0.5) * Math.PI / 2), -1, Math.Tan(((j + 0.5) / div - 0.5) * Math.PI / 2)));
                   formMain.crystal.VectorOfAxis.Add(new Vector3D(1, Math.Tan(((i + 0.5) / div - 0.5) * Math.PI / 2), Math.Tan(((j + 0.5) / div - 0.5) * Math.PI / 2)));
                   formMain.crystal.VectorOfAxis.Add(new Vector3D(-1, Math.Tan(((i + 0.5) / div - 0.5) * Math.PI / 2), Math.Tan(((j + 0.5) / div - 0.5) * Math.PI / 2)));
                   //formMain.crystal.VectorOfAxis.Add(new Vector3D(i, -div, j));
                   //formMain.crystal.VectorOfAxis.Add(new Vector3D(div, i, j));
                   //formMain.crystal.VectorOfAxis.Add(new Vector3D(-div, i, j));
               }
        */

        /*
         * div = 1350;
         Random rn = new Random();
         for (int k = 0; k < div; k++)
         {
             formMain.crystal.VectorOfAxis.Add(Vector3D.RandomVector(rn));
         }
         * */
        /* div = 1350;
         double beforePhi=0;
         for (int k = 0; k < div; k++)
         {
             double h = -1 + 2 * (double)k / (div - 1);
             double theta = Math.Acos(h);
             double phi = (1 - h * h) == 0 ? 0 : beforePhi + 3.6/ Math.Sqrt(div * (1 - h * h));
             formMain.crystal.VectorOfAxis.Add(new Vector3D(Math.Cos(phi) * Math.Sin(theta), Math.Sin(phi) * Math.Sin(theta), Math.Cos(theta)));
             beforePhi = phi;
         }
        */

        Draw();
    }
    #endregion

    #region 3D描画の設定
    private void checkBoxDisplay3D_CheckedChanged(object sender, EventArgs e)
    {
        splitContainer1.Panel2Collapsed = !checkBoxDisplay3D.Checked;
        groupBox3DOptions.Visible = checkBoxDisplay3D.Checked;
    }

    private void button3D_reset_Click(object sender, EventArgs e)
    {
        glControl.ProjWidth = 2.4;
        glControl.WorldMatrix = OpenTK.Mathematics.Matrix4d.Identity;
    }

    private void checkBox3dOptionSphere_CheckedChanged(object sender, EventArgs e) => Draw3D();

    private void checkBox3dOptionProjectionLine_CheckedChanged(object sender, EventArgs e) => Draw3D();

    private void trackBarDepthFadingOut_Scroll(object sender, EventArgs e) => Draw3D();
    #endregion

    #region フォーム全体のイベント
    private void FormStereonet_Paint(object sender, PaintEventArgs e) => Draw();

    //フォームの大きさが変更されたとき

    private Size lastgraphicsBoxSize = new(0, 0);

    private void formStereonet_Resize(object sender, EventArgs e)
    {
        if (graphicsBox.ClientSize.Width > 0 && graphicsBox.ClientSize.Height > 0 && lastgraphicsBoxSize.Width > 0 && lastgraphicsBoxSize.Height > 0)
            mag *= ((double)graphicsBox.ClientSize.Width / lastgraphicsBoxSize.Width + (double)graphicsBox.ClientSize.Height / lastgraphicsBoxSize.Height) / 2.0;

        if (mag > 10000 || double.IsNaN(mag))
            mag = 10000;
        else if (mag < Math.Max(graphicsBox.ClientSize.Width / 2.4, graphicsBox.ClientSize.Height / 2.4))
        {
            centerPt = new PointD(0, 0);
            mag = Math.Min(graphicsBox.ClientSize.Width / 2.4, graphicsBox.ClientSize.Height / 2.4);
        }

        Draw();
        if (graphicsBox.ClientSize.Width != 0 && graphicsBox.ClientSize.Height != 0)
            lastgraphicsBoxSize = graphicsBox.ClientSize;
    }

    #endregion

    #region 晶帯軸・格子面の指数探索 
    /// <summary> 3D 単位ベクトル vSphere (ラボ系・上半球) に最も近い晶帯軸/格子面の指数を返す。 </summary>
    private ((int U, int V, int W) Axis, (int H, int K, int L) Plane) FindIndex(Vector3DBase vSphere) // 260517Cl 実装
    {
        //3つの指数の絶対値の合計が、以下の値になる範囲で、最も近い指数を探す。
        int sumMax = 30;

        if (formMain == null || formMain.Crystal == null || formMain.Crystal.A * formMain.Crystal.B * formMain.Crystal.C == 0)
            return ((0, 0, 0), (0, 0, 0));

        // 下半球モードは Z 反転 (DrawStereoNet と整合)。caller の vSphere を破壊しないよう新規生成
        var v = radioButtonLowerSphere.Checked ? new Vector3DBase(vSphere.X, vSphere.Y, -vSphere.Z) : vSphere;

        // R は直交回転行列なので R^T = R^-1。target を結晶 Cartesian 系に戻して loop 内の行列積を省く
        var crystal = formMain.Crystal;
        var target = crystal.RotationMatrix.Transpose() * v;

        double bestScoreA = -2, bestScoreP = -2;
        (int U, int V, int W) bestUvw = (0, 0, 0), bestHkl = (0, 0, 0);
        for (int u = -sumMax, rem1 = sumMax - Abs(u); u <= sumMax; rem1 = sumMax - Abs(++u))
            for (int vv = -rem1, rem2 = rem1 - Abs(vv); vv <= rem1; rem2 = rem1 - Abs(++vv))
                for (int w = -rem2; w <= rem2; w++)
                {
                    if ((u | vv | w) == 0 || Algebra.Irreducible(u, vv, w) != 1) continue;
                    var dirA = crystal.MatrixReal * (u, vv, w);
                    double cosA = dirA * target / dirA.Length;
                    if (cosA > bestScoreA) { bestScoreA = cosA; bestUvw = (u, vv, w); }
                    var dirP = crystal.MatrixInverseTransposed * (u, vv, w);
                    double cosP = dirP * target / dirP.Length;
                    if (cosP > bestScoreP) { bestScoreP = cosP; bestHkl = (u, vv, w); }
                }
        return (bestUvw, bestHkl);
    }
    #endregion
    public void UpdatePlaneIndices(bool redraw = true) // 260517Cl 末尾 Draw() の有無を呼び出し側で選べるよう既定引数化（連続 Draw 重複を抑える）
    {
        indexControlDrawing.MillerBravais = MillerBravaisActive && radioButtonPlanes.Checked;
        indexControlCirclePlane1.MillerBravais = indexControlCirclePlane2.MillerBravais = MillerBravaisActive;
        if (redraw) Draw();
    }
    private void radioButtonDelimiterNone_CheckedChanged(object sender, EventArgs e) => Draw();
}


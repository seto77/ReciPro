#region using
using Crystallography.Mathematics;
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
using Windows.Devices.Radios;
using V3 = OpenTK.Mathematics.Vector3d;
#endregion
namespace ReciPro;

public partial class FormStereonet : Form
{
    #region �t�B�[���h�A�v���p�e�B
    public FormMain formMain;

    private Font strFont;
    private float pointSize;
    private Point MouseRangeStart, MouseRangeEnd = new(-1, -1);
    private bool MouseRangingMode = false;
    private PointD centerPt = new(0, 0);
    private double mag;
    private GLControlAlpha glControl;

    public Matrix3D RotationMatrix => formMain.Crystal.RotationMatrix;

    #endregion

    #region �N���A�I��
    public FormStereonet()
    {
        InitializeComponent();
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    }

    //�t�H�[�������[�h���ꂽ�Ƃ�
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

        //YusaGonio�̃^�u�y�[�W���폜
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

    #endregion

    #region �`��֘A

    /// <summary>
    /// �v���W�F�N�V�����s��̐ݒ���s���B
    /// </summary>
    public bool SetProjection(Graphics g)
    {
        if (g != null && graphicsBox.ClientSize.Width != 0 && graphicsBox.ClientSize.Height != 0)
            try
            {
                g.Transform =
            new Matrix(
                (float)mag, 0, 0, (float)mag,
                (float)(graphicsBox.ClientSize.Width / 2.0 - mag * centerPt.X),
                (float)(graphicsBox.ClientSize.Height / 2.0 + mag * centerPt.Y));//(float)centerPt.X, (float)centerPt.Y);
            }
            catch { return false; }
        return true;
    }

    //�X�e���I�l�b�g��`��
    public void Draw(Graphics g = null, bool renewOutline = true)
    {
        if (graphicsBox.Width <= 0 || graphicsBox.Height <= 0) return;

        //�O���t�B�b�N�X�{�b�N�X�ɕ`�悷��ꍇ
        g ??= graphicsBox.Graphics;

        if (!SetProjection(g))
            return;

        g.Clear(colorControlBackGround.Color);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        DrawOutline(g);
        DrawStereoNet(g);
        DrawCircles(g);

        Draw3D();

        if (MouseRangingMode)
        {
            var pen = new Pen(Brushes.Gray, 1f / (float)mag) { DashStyle = DashStyle.Dash };
            var start = convertClientToSrc(MouseRangeStart);
            var end = convertClientToSrc(MouseRangeEnd);
            g.DrawRectangle(pen, (float)Math.Min(start.X, end.X), (float)Math.Min(-start.Y, -end.Y),
                (float)Math.Abs(start.X - end.X), (float)Math.Abs(start.Y - end.Y));
        }
        graphicsBox.Refresh();
    }

    private void Draw3D()
    {
        if (glControl == null) return;

        var sq2 = Math.Sqrt(2);

        V3 conv(double x, double y, double z) => radioButtonWulff.Checked ? new V3(x, y, 0) / (1 + z) : new V3(x, y, 0) / Math.Sqrt(1 + z) * sq2 + new V3(0, 0, 1);

        var glObjects = new List<GLObject>();
        glControl.DepthCueing = (true, (trackBarDepthFadingOut.Value - 10) / 2.0, 2);
        Color color10 = colorControl10DegLine.Color, color90 = colorControl90DegLine.Color;

        #region ���̃A�E�g���C��
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

        #region �X�e���I�l�b�g�̃A�E�g���C��
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
                glObjects.Add(new Lines(pts1.Select(v => new V3(v.X, -v.Y, v.Z)).ToArray(), 2f, new Material(color10)));
                glObjects.Add(new Lines([.. pts2], 2f, new Material(color10)));
                glObjects.Add(new Lines(pts2.Select(v => new V3(-v.X, v.Y, v.Z)).ToArray(), 2f, new Material(color10)));
            }
        }
        #endregion

        # region �ʁA���x�N�g��
        var unique = radioButtonAxes.Checked ? colorControlUniqueAxis.Color : colorControlUniquePlane.Color;
        var general = radioButtonAxes.Checked ? colorControlGeneralAxis.Color : colorControlGeneralPlane.Color;
        Vector3D[] vector = radioButtonAxes.Checked ? [.. formMain.Crystal.VectorOfAxis] : [.. formMain.Crystal.VectorOfPlane];
        var radius = pointSize * 0.004;
        for (int i = 0; i < vector.Length; i++)
        {
            var v = formMain.Crystal.RotationMatrix * vector[i].Normarize();
            var color = i < 6 && radioButtonRange.Checked ? unique : general;
            if (!checkBox3dOptionSemisphere.Checked || v.Z > 0)
            {
                glObjects.Add(new Sphere(v, radius, new Material(color, 1), DrawingMode.Surfaces));
                if (checkBox3dOptionLabel.Checked)
                    glObjects.Add(new TextObject(glControl, vector[i].Text, trackBarStrSize.Value / 8, v, radius + 0.001, true, new Material(color)));
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
                            pts.Add(new V3(0, 0, 1) + r * rot.Mult(new V3(Math.Cos(sweep * j / div), 0, -Math.Sin(sweep * j / div))));
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

    //�X�e���I�l�b�g�̗֊s��`��
    private void DrawOutline(Graphics g)
    {
        if (sin.Count == 0)
            for (int n = 0; n < 360; n++)
            {
                sin.Add(Math.Sin(n * Math.PI / 180.0));
                cos.Add(Math.Cos(n * Math.PI / 180.0));
                tan.Add(Math.Tan(n * Math.PI / 180.0));
            }

        var pen1 = new Pen(new SolidBrush(colorControl1DegLine.Color), (float)(1 / mag));
        var pen10 = new Pen(new SolidBrush(colorControl10DegLine.Color), (float)(2 / mag));
        var pen90 = new Pen(new SolidBrush(colorControl90DegLine.Color), (float)(3 / mag));

        if (this.radioButtonOutlineEquator.Checked)//�ԓ����[�h�̂Ƃ�
        {
            var wList = new List<int>();
            for (int i = 1; i < 90; i++)
                if (i % 10 != 0)
                    wList.Add(i);
            for (int i = 10; i < 90; i += 10)
                wList.Add(i);

            if (radioButtonWulff.Checked)//Wulff�l�b�g
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
            else//Schmidt�l�b�g
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
        else//�Ƀ��[�h�̂Ƃ�
        {
            var wList = new List<int>();
            for (int i = 1; i < 180; i++)
                if (i % 10 != 0)
                    wList.Add(i);
            for (int i = 10; i < 180; i += 10)
                wList.Add(i);

            if (radioButtonWulff.Checked)//Wulff�l�b�g
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

            else//Schmidt�l�b�g
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

    //�X�e���I�l�b�g���̓_��`��
    private void DrawStereoNet(Graphics g)
    {
        if (formMain == null || formMain.Crystal.A * formMain.Crystal.B * formMain.Crystal.C == 0)
            return;
        var crystal = formMain.Crystal;
        var rot = crystal.RotationMatrix;

        Vector3D[] vector = radioButtonAxes.Checked ? [.. formMain.Crystal.VectorOfAxis] : [.. formMain.Crystal.VectorOfPlane];
        var drawString = trackBarStrSize.Value != 1 && checkBoxShowIndexLabels.Checked;
        var font = new Font("Tahoma", trackBarStrSize.Value / (float)mag / 7f);
        var brushUnique = new SolidBrush(radioButtonAxes.Checked ? colorControlUniqueAxis.Color : colorControlUniquePlane.Color);
        var brushGeneral = new SolidBrush(radioButtonAxes.Checked ? colorControlGeneralAxis.Color : colorControlGeneralPlane.Color);
        var penUnique = new Pen(radioButtonAxes.Checked ? colorControlUniqueAxis.Color : colorControlUniquePlane.Color, 2f / (float)mag);
        var penGeneral = new Pen(radioButtonAxes.Checked ? colorControlGeneralAxis.Color : colorControlGeneralPlane.Color, 2f / (float)mag);

        Func<Vector3DBase, PointD> conv = radioButtonWulff.Checked ? Stereonet.ConvertVectorToWulff : Stereonet.ConvertVectorToSchmidt;

        for (int n = 0; n < vector.Length; n++)
        {
            //���ю����[�h���A�����ʃ��[�h�̂Ƃ�
            if (radioButtonAxes.Checked || radioButtonPlanes.Checked)
            {
                var radius = pointSize / mag;
                if (radioButtonPlanes.Checked && checkBoxReflectStructureFactor.Checked)
                    radius *= Math.Sqrt(vector[n].RelativeIntensity);

                var v = rot * vector[n];
                if (radioButtonLowerSphere.Checked)
                    v.Z = -v.Z;
                var srcPt = conv(v);

                if (!formMain.YusaGonioMode)
                {
                    var brush = n < 6 && radioButtonRange.Checked ? brushUnique : brushGeneral;
                    var pen = n < 6 && radioButtonRange.Checked ? penUnique : penGeneral;
                    if (srcPt.X * srcPt.X + srcPt.Y * srcPt.Y <= 1.2)
                    {
                        if (radioButtonUpperSphere.Checked)
                            g.FillEllipse(brush, new RectangleF((float)(srcPt.X - radius), (float)(-srcPt.Y - radius), (float)(radius * 2), (float)(radius * 2)));
                        else
                            g.DrawEllipse(pen, new RectangleF((float)(srcPt.X - radius), (float)(-srcPt.Y - radius), (float)(radius * 2), (float)(radius * 2)));
                        if (drawString)
                            g.DrawString(vector[n].Text, font, brush, (float)(srcPt.X + radius), (float)(-srcPt.Y + radius));
                    }
                }
                else//YusaGonio
                {
                    positionRecorder[n].Add(srcPt);
                    for (int i = 0; i < positionRecorder[n].Count; i++)
                    {
                        g.FillEllipse(brushGeneral, new RectangleF((float)(positionRecorder[n][i].X - radius / 2), (float)(-positionRecorder[n][i].Y - radius / 2), (float)radius, (float)radius));
                        if (i != 0)
                            g.DrawLine(new Pen(brushGeneral, 1f), positionRecorder[n][i - 1].X, positionRecorder[n][i - 1].Y, positionRecorder[n][i].X, positionRecorder[n][i].Y);
                    }
                }
            }
            //�e�r�����[�h�̂Ƃ�
            else
            {
                var v = rot * vector[n];
                if (radioButtonLowerSphere.Checked)
                    v.Z = -v.Z;

                // Z���� v�ɌX����s����v�Z
                var mat = GLGeometry.CreateRotationToZ(new V3(v.X, v.Y, v.Z)).ToMatrix3D();
                double sin�� = waveLengthControl.WaveLength * v.Length / 2, cos�� = Math.Sqrt(1 - sin�� * sin��);

                var pts = Enumerable.Range(0, sin.Count)
                    .Select(i => conv(mat * new Vector3D(cos�� * sin[i], cos�� * cos[i], sin��)))
                    .Select(p => new PointF(-(float)p.X, (float)p.Y)).ToArray();

                //�L���ȓ_�𒊏o
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

                var col = checkBoxReflectStructureFactor.Checked ? Color.FromArgb((int)(vector[n].RelativeIntensity * 255), colorControlKikuchi.Color) : colorControlKikuchi.Color;
                var pen = new Pen(col, 2f / (float)mag);
                if (validEnd1 - validStart1 != 0)
                {
                    pts = validStart1 != -1 && validStart2 == -1 ? pts[validStart1..(validEnd1 + 1)] : [.. pts[validStart2..(validEnd2 + 1)], .. pts[validStart1..(validEnd1 + 1)]];
                    g.DrawLines(pen, pts);
                }
            }
        }

        //�e�r�����[�h�̂Ƃ��͏��ю��̎w�����L��
        if (radioButtonKikuchiLinePairs.Checked)
        {
            var dic = new Dictionary<(int u, int v, int w), int>();
            for (int i = 0; i < vector.Length-1; i++)
                for (int j = i + 1; j < vector.Length; j++)
                {
                    var (h1, k1, l1) = vector[i].Index;
                    var (h2, k2, l2) = vector[j].Index;
                    var (u, v, w) = (k1 * l2 - l1 * k2, l1 * h2 - h1 * l2, h1 * k2 - k1 * h2);
                    if (u != 0 || v != 0 || w != 0)
                    {
                        var n = Algebra.Irreducible(u, v, w);
                        u /= n; v /= n; w /= n;
                        if (u < 0 || (u == 0 && v < 0) || (u == 0 && v == 0 && w < 0))
                        { u = -u; v = -v; w = -w; }

                        if (dic.ContainsKey((u, v, w)))
                            dic[(u, v, w)]++;
                        else
                            dic.Add((u, v, w), 1);
                    }
                }

            var list = dic.OrderByDescending(e => e.Value).ToArray();
            var max = Math.Min(vector.Length / 2, list.Length);
            while (max + 1 < list.Length && list[max - 1].Value == list[max].Value)
                max++;

            var radius = 2f / (float)mag;
            var brushLabel = new SolidBrush(colorControlString.Color);
            var brushPt = new SolidBrush(colorControlKikuchi.Color);
            var penPt = new Pen(colorControlKikuchi.Color, 2f / (float)mag);
            for (int i = 0; i < max; i++)
            {
                var (u, v, w) = list[i].Key;
                for (int j = 0; j < 2; j++)
                {
                    if (j == 1) 
                    { u = -u; v = -v; w = -w; }
                    var vec = rot * (u * crystal.A_Axis + v * crystal.B_Axis + w * crystal.C_Axis);
                    if (radioButtonLowerSphere.Checked)
                        vec.Z = -vec.Z;
                    var pt = conv(vec).ToPointF();

                    if (pt.X * pt.X + pt.Y * pt.Y <= 1.05)
                    {
                        if(drawString)
                            g.DrawString($"[{u}{v}{w}]", font, brushLabel, pt.X, -pt.Y);
                        if (radioButtonUpperSphere.Checked)
                            g.FillEllipse(brushPt, new RectangleF(pt.X - radius, -pt.Y - radius, radius * 2, radius * 2));
                        else
                            g.DrawEllipse(penPt, new RectangleF(pt.X - radius, -pt.Y - radius, radius * 2, radius * 2));
                    }
                }
            }
        }
    }

    /// <summary>
    /// ��~�`��
    /// </summary>
    /// <param name="g"></param>
    private void DrawCircles(Graphics g)
    {
        //��~�`��
        Vector3D vec;
        float width;
        var pen = new Pen(colorControlGreatCircle.Color, 0.002f);
        for (int i = 0; i < checkedListBoxCircles.CheckedItems.Count; i++)
        {
            vec = Vector3D.Normarize(formMain.Crystal.RotationMatrix * (Vector3D)(checkedListBoxCircles.CheckedItems[i]));
            if (Math.Abs(vec.Z) > 0.9999)//��~���ŊO���Ƃقڈ�v����Ƃ���
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
                width = (float)(1 / vec.Z);//���ꂪ������Ȃ��Ƃ���
                g.DrawArc(pen, (float)((vec.X - 1) / vec.Z), (float)((-vec.Y - 1) / vec.Z), 2 * width, 2 * width,
                    (float)((-Math.Atan2(-vec.Y, -vec.X) - Math.Asin(vec.Z)) / Math.PI * 180), (float)(2 * Math.Asin(vec.Z) / Math.PI * 180));
            }
            else if (vec.Z < -0.00000000001)
            {
                vec = -vec;
                width = (float)(1 / vec.Z);
                g.DrawArc(pen, (float)((vec.X - 1) / vec.Z), (float)((-vec.Y - 1) / vec.Z), 2 * width, 2 * width,
                    (float)((-Math.Atan2(-vec.Y, -vec.X) - Math.Asin(vec.Z)) / Math.PI * 180), (float)(2 * Math.Asin(vec.Z) / Math.PI * 180));
            }
            else
            {
                var sqrt = Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
                g.DrawLine(pen, (float)(vec.Y / sqrt), (float)(vec.X / sqrt), (float)(-vec.Y / sqrt), (float)(-vec.X / sqrt));
            }
        }
    }

    //Src�i�P�ʂȂ��j��Client(pixel)�ɕϊ�
    private PointF convertSrcToClient(PointD pt)
        => new((float)(graphicsBox.ClientSize.Width / 2.0 + mag * (pt.X - centerPt.X)), (float)(graphicsBox.ClientSize.Height / 2.0 + mag * (pt.Y - centerPt.Y)));

    private PointF convertSrcToClient(double x, double y) => convertSrcToClient(new PointD(x, y));

    private PointD convertClientToSrc(Point pt)
        => new(centerPt.X + (pt.X - graphicsBox.ClientSize.Width / 2) / mag, centerPt.Y + (graphicsBox.ClientSize.Height / 2 - pt.Y) / mag);

    private PointD convertClientToSrc(int x, int y) => convertClientToSrc(new Point(x, y));

    #endregion

    #region �s�N�`���[�{�b�N�X�̃C�x���g�֘A

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
            {//�I��͈͂����܂�ɏ�����������k��
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
                //���݂�mag�ƒ��S�ʒu����A�V����mag�ƒ��S�ʒu�����肷��

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
        PointD pt = convertClientToSrc(e.X, e.Y); ;
        double azimuth = Math.Asin(2 * pt.Y / (1 + pt.X * pt.X + pt.Y * pt.Y));
        double tilt = (Math.Cos(azimuth) != 0) ? Math.Asin(2 * pt.X / (1 + pt.X * pt.X + pt.Y * pt.Y) / Math.Cos(azimuth)) : 0;
        labelXpos.Text = "Tilt X: " + (azimuth / Math.PI * 180).ToString("f3") + "��";
        labelYpos.Text = "Tilt Y: " + (tilt / Math.PI * 180).ToString("f3") + "��";

        //�^�񒆃{�^����������Ȃ���}�E�X���������Ƃ�
        if (e.Button == MouseButtons.Middle)
        {
            centerPt += new PointD((lastMousePositionClient.X - e.X) / mag, (-lastMousePositionClient.Y + e.Y) / mag);
            Draw();
        }

        //���{�^����������Ȃ���}�E�X���������Ƃ�
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

    #endregion �s�N�`���[�{�b�N�X�̃C�x���g�֘A

    #region �^�u�R���g���[���̃C�x���g
    private void tabControl_Click(object sender, EventArgs e)
    {
        splitContainer1.SendToBack();
        graphicsBox.Refresh();
    }

    #region Appearance�^�u�֘A
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

    #region ��~�^�u�֘A
    private void buttonAddCircle_Click(object sender, EventArgs e)
    {
        if (radioButtonCircleByAxis.Checked)
        {
            var u = (int)numericUpDownCircleU.Value;
            var v = (int)numericUpDownCircleV.Value;
            var w = (int)numericUpDownCircleW.Value;
            if (u == 0 && v == 0 && w == 0) return;
            var vec = new Vector3D(u * formMain.Crystal.A_Axis + v * formMain.Crystal.B_Axis + w * formMain.Crystal.C_Axis) { Text = $"[{u} {v} {w}]" };
            formMain.Crystal.VectorOfPole.Add(vec);
            checkedListBoxCircles.Items.Add(vec, true);
            Draw();
        }
        else if (radioButtonCircleByPlanes.Checked)
        {
            var h1 = (int)numericUpDownCircleH1.Value;
            var h2 = (int)numericUpDownCircleH2.Value;
            var k1 = (int)numericUpDownCircleK1.Value;
            var k2 = (int)numericUpDownCircleK2.Value;
            var l1 = (int)numericUpDownCircleL1.Value;
            var l2 = (int)numericUpDownCircleL2.Value;

            var u = k1 * l2 - k2 * l1;
            var v = l1 * h2 - l2 * h1;
            var w = h1 * k2 - h2 * k1;
            if (u == 0 && v == 0 && w == 0) return;

            var vec = new Vector3D(u * formMain.Crystal.A_Axis + v * formMain.Crystal.B_Axis + w * formMain.Crystal.C_Axis) { Text = $"({h1} {k1} {l1}) & ({h2} {k2} {l2})" };

            formMain.Crystal.VectorOfPole.Add(vec);
            checkedListBoxCircles.Items.Add(vec, true);
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
        panelAxis.Enabled = radioButtonCircleByAxis.Checked;
        panelPlanes.Enabled = radioButtonCircleByPlanes.Checked;
    }
    #endregion

    #endregion

    #region ���C���Ō������ύX���ꂽ�Ƃ�
    public void SetCrystal()
    {
        setVector();
        //formMain.Crystal.SetVectorOfAxis((int)numericBox1.Value, (int)numericBox2.Value, (int)numericBox3.Value);
        //formMain.Crystal.SetVectorOfPlane((int)numericBox1.Value, (int)numericBox2.Value, (int)numericBox3.Value);

        checkedListBoxCircles.Items.Clear();
        if (formMain.Crystal.VectorOfPole == null)
            formMain.Crystal.VectorOfPole = [];
        else
            for (int i = 0; i < formMain.Crystal.VectorOfPole.Count; i++)
                checkedListBoxCircles.Items.Add(formMain.Crystal.VectorOfPole[i], true);
        Draw();
    }
    #endregion

    #region �`��Ώ�(�ʁE���E�e�r��)�Ⓤ�e���@(���ρE���p)���ύX���ꂽ�Ƃ�
    private void radioButtonAxes_CheckedChanged(object sender, EventArgs e)
    {
        if (!((RadioButton)sender).Checked) return;

        if (radioButtonAxes.Checked)
        {
            labelHU.Text = "u"; labelKV.Text = "v"; labelLW.Text = "w";
            radioButtonHighStructureFactor.Visible = false;
            if (radioButtonHighStructureFactor.Checked)
                radioButtonRange.Checked = true;
            checkBoxReflectStructureFactor.Enabled = false;
        }
        else
        {
            labelHU.Text = "h"; labelKV.Text = "k"; labelLW.Text = "l";
            radioButtonHighStructureFactor.Visible = true;
            checkBoxReflectStructureFactor.Enabled = true;

        }
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

    #region �t�@�C�����j���[
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

    // ����v���r���[��\��
    private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e) => printPreviewDialog1.ShowDialog();

    private void printToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (printDialog1.ShowDialog() == DialogResult.OK)
            printDocument1.Print();
    }

    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        System.Drawing.Printing.PageSettings ps = printDocument1.PrinterSettings.DefaultPageSettings;
        //�p���T�C�Y�擾 ���̃T�C�Y��1/100�C���`
        var height = (ps.PaperSize.Height - ps.Margins.Top - ps.Margins.Bottom) / 100f;
        var width = (ps.PaperSize.Width - ps.Margins.Left - ps.Margins.Right) / 100f;

        if (printDocument1.PrinterSettings.DefaultPageSettings.Landscape)
        {//�c�����t�]
            (height, width) = (width, height);
        }
        //�𑜓x300dpi�̂Ƃ��̃C���[�W�T�C�Y��
        //glString.Font = new Font("Tahoma", trackBarStrSize.Value / 10f / 72f * 300f);
        var bmp = (Bitmap)graphicsBox.Image;

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

    #region �`��Ώۂ̖ʁE���w���̐ݒ�
    private void radioButtonRange_CheckedChanged(object sender, EventArgs e)
    {
        if (!((RadioButton)sender).Checked) return;

        numericBox1.HeaderText = numericBox2.HeaderText = numericBox3.HeaderText =
            radioButtonRange.Checked ? "�}" : "";

        if (radioButtonRange.Checked)
        {
            numericBox1.Minimum = numericBox2.Minimum = numericBox3.Minimum = 0;
            tableLayoutPanel3.Visible = true;
            numericBoxHighStructureFactor.Visible = false;
            panelSpecifiedIndices.Visible = false;

        }
        else if (radioButtonSpecifiedIndices.Checked)
        {
            numericBox1.Minimum = numericBox2.Minimum = numericBox3.Minimum = -numericBox1.Maximum;
            tableLayoutPanel3.Visible = true;
            numericBoxHighStructureFactor.Visible = false;
            panelSpecifiedIndices.Visible = true;
        }
        else//�\�����q���̏ꍇ
        {
            tableLayoutPanel3.Visible = false;
            numericBoxHighStructureFactor.Visible = true;
            panelSpecifiedIndices.Visible = false;
        }
        setVector();
        Draw();
    }

    //�w���͈͂��ύX���ꂽ�Ƃ�
    private void numericUpDown_ValueChanged(object sender, EventArgs e)
    {
        setVector();
        Draw();
    }
    private void buttonAddIndex_Click(object sender, EventArgs e)
    {
        int index1 = (int)numericBox1.Value, index2 = (int)numericBox2.Value, index3 = (int)numericBox3.Value;
        if (index1 != 0 || index2 != 0 || index3 != 0)
            listBoxSpecifiedIndices.Items.Add(index1.ToString() + " " + index2.ToString() + " " + index3.ToString());
        setVector();
        Draw();
    }

    private void buttonRemoveIndex_Click(object sender, EventArgs e)
    {
        if (listBoxSpecifiedIndices.SelectedIndex > -1)
            listBoxSpecifiedIndices.Items.RemoveAt(listBoxSpecifiedIndices.SelectedIndex);
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

    #region �ʁA���̃x�N�g�����v�Z
    private void setVector()
    {
        if (formMain.Crystal.A * formMain.Crystal.B * formMain.Crystal.C != 0)
        {
            if (radioButtonRange.Checked)//�͈͎w�胂�[�h�̎�
            {
                formMain.Crystal.SetVectorOfAxis((int)numericBox1.Value, (int)numericBox2.Value, (int)numericBox3.Value);
                formMain.Crystal.SetVectorOfPlane((int)numericBox1.Value, (int)numericBox2.Value, (int)numericBox3.Value, waveLengthControl.WaveSource);
            }
            else if (radioButtonSpecifiedIndices.Checked)//����w�����[�h�̎�
            {
                var planeIndices = new List<(int H, int K, int L)>();
                var axisIndices = new List<(int U, int V, int W)>();
                foreach (object o in listBoxSpecifiedIndices.Items)
                {
                    var str = ((string)o).Split([' ']);
                    int x = Convert.ToInt32(str[0]), y = Convert.ToInt32(str[1]), z = Convert.ToInt32(str[2]);
                    if (!checkBoxIncludingEquivalentPlanes.Checked)
                    {
                        planeIndices.Add((x, y, z));
                        axisIndices.Add((x, y, z));
                    }
                    else
                    {
                        axisIndices.AddRange(SymmetryStatic.GenerateEquivalentAxes(x, y, z, formMain.Crystal.Symmetry));
                        planeIndices.AddRange(SymmetryStatic.GenerateEquivalentPlanes(x, y, z, formMain.Crystal.Symmetry));
                    }
                }
                formMain.Crystal.SetVectorOfAxis([.. axisIndices]);
                formMain.Crystal.SetVectorOfPlane([.. planeIndices], waveLengthControl.WaveSource);
            }
            else//�\�����q�����[�h�̎� (�����ʂ��e�r���̂Ƃ��������̃��[�h�ɂȂ�Ȃ�)
            {
                int n = numericBoxHighStructureFactor.ValueInteger;

                formMain.Crystal.SetVectorOfG(0.0001, waveLengthControl.WaveSource, n * 40);
                var vec = formMain.Crystal.VectorOfG;
                Array.Sort(vec, (g1, g2) => g2.RawIntensity.CompareTo(g1.RawIntensity));
                var maxIntenxity = vec[0].RawIntensity;
                foreach (var v in vec)
                {
                    v.RelativeIntensity = v.RawIntensity / maxIntenxity;
                    v.Text = $"({v.Text.Replace(" ", "")})";
                }

                formMain.Crystal.VectorOfPlane = [];

                if (radioButtonPlanes.Checked)
                    for (int i = 1; i < n * 10; i++)
                        for (int j = 0; j < i; j++)
                            if (!Crystal.CheckIrreducible(vec[i].Index, vec[j].Index))
                            {
                                vec[i].Flag1 = true;
                                break;
                            }

                for (int i = 0; i < vec.Length; i++)
                    if (!vec[i].Flag1)
                    {
                        if (formMain.Crystal.VectorOfPlane.Count < n)
                            formMain.Crystal.VectorOfPlane.Add(vec[i]);
                        else
                        {
                            while ((vec[i - 1].RawIntensity - vec[i].RawIntensity) / vec[i - 1].RawIntensity < 1E-8)
                                formMain.Crystal.VectorOfPlane.Add(vec[i++]);
                            break;
                        }
                    }
            }
        }
    }
    #endregion

    #endregion

    #region �e�X�g�R�[�h
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

    #region 3D�`��̐ݒ�
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

    #region �t�H�[���S�̂̃C�x���g
    private void FormStereonet_Paint(object sender, PaintEventArgs e) => Draw();

    //�t�H�[���̑傫�����ύX���ꂽ�Ƃ�

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
}

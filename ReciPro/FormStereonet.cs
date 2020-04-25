using Crystallography;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ReciPro
{
    public partial class FormStereonet : Form
    {
        public FormMain formMain;

        private Font strFont;
        private float pointSize;
        private Point MouseRangeStart, MouseRangeEnd = new Point(-1, -1);
        private bool MouseRangingMode = false;
        private PointD centerPt = new PointD(0, 0);
        private double mag;

        public Matrix3D RotationMatrix => formMain.Crystal.RotationMatrix;

        public FormStereonet()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        //フォームがロードされたとき
        private void FormStereonet_Load(object sender, EventArgs e)
        {
            mag = Math.Min(graphicsBox.ClientSize.Width / 2.4, graphicsBox.ClientSize.Height / 2.4);
            Draw();
            lastgraphicsBoxSize = graphicsBox.ClientSize;
        }

        private void FormStereonet_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                pointSize = trackBarPointSize.Value;
                setVector();
                centerPt = new PointD(0, 0);

                mag = Math.Min(graphicsBox.ClientSize.Width / 2.4, graphicsBox.ClientSize.Height / 2.4);

                graphicsBox.BringToFront();

                Draw();
                lastgraphicsBoxSize = graphicsBox.ClientSize;
            }
        }

        /// <summary>
        /// プロジェクション行列の設定を行う。
        /// </summary>
        public bool SetProjection(Graphics g)
        {
            if (g != null && graphicsBox.ClientSize.Width != 0 && graphicsBox.ClientSize.Height != 0)
                try
                {
                    g.Transform =
                new System.Drawing.Drawing2D.Matrix(
                    (float)mag, 0, 0, (float)mag,
                    (float)(graphicsBox.ClientSize.Width / 2.0 - mag * centerPt.X),
                    (float)(graphicsBox.ClientSize.Height / 2.0 + mag * centerPt.Y));//(float)centerPt.X, (float)centerPt.Y);
                }
                catch { return false; }
            return true;
        }

        //ステレオネットを描く
        public void Draw(Graphics g = null, bool renewOutline = true)
        {
            if (graphicsBox.Width <= 0 || graphicsBox.Height <= 0) return;

            if (g == null)//グラフィックスボックスに描画する場合
                g = graphicsBox.Graphics;

            if (!SetProjection(g))
                return;

            g.Clear(colorControlBackGround.Color);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawOutline(g);
            DrawStereoNet(g);
            DrawCircles(g);

            if (MouseRangingMode)
            {
                Pen pen = new Pen(Brushes.Gray, 1f / (float)mag);
                pen.DashStyle = DashStyle.Dash;
                var start = convertClientToSrc(MouseRangeStart);
                var end = convertClientToSrc(MouseRangeEnd);
                g.DrawRectangle(pen, (float)Math.Min(start.X, end.X), (float)Math.Min(-start.Y, -end.Y),
                    (float)Math.Abs(start.X - end.X), (float)Math.Abs(start.Y - end.Y));
            }
            graphicsBox.Refresh();
        }

        private List<double> sin = new List<double>();
        private List<double> cos = new List<double>();
        private List<double> tan = new List<double>();

        //ステレオネットの輪郭を描く
        private void DrawOutline(Graphics g)
        {
            var pen1 = new Pen(new SolidBrush(colorControl1DegLine.Color), (float)(1 / mag));
            var pen10 = new Pen(new SolidBrush(colorControl10DegLine.Color), (float)(2 / mag));
            var pen90 = new Pen(new SolidBrush(colorControl90DegLine.Color), (float)(3 / mag));

            if (sin.Count == 0)
            {
                for (int n = 0; n < 180; n++)
                {
                    sin.Add(Math.Sin(n * Math.PI / 180.0));
                    cos.Add(Math.Cos(n * Math.PI / 180.0));
                    tan.Add(Math.Tan(n * Math.PI / 180.0));
                }
            }

            if (this.radioButtonOutlineEquator.Checked)//赤道モードのとき
            {
                List<int> wList = new List<int>();
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
                        if (this.checkBox1DegLine.Checked || w % 10 == 0)
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
                        if (this.checkBox1DegLine.Checked || w % 10 == 0)
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
                List<int> wList = new List<int>();
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

        //ステレオネット中の点を描く
        private void DrawStereoNet(Graphics g)
        {
            if (formMain.Crystal.A * formMain.Crystal.B * formMain.Crystal.C == 0)
                return;
            var vector = radioButtonAxes.Checked ? formMain.Crystal.VectorOfAxis.ToArray() : formMain.Crystal.VectorOfPlane.ToArray();
            var drawString = trackBarStrSize.Value != 1;
            var unique = radioButtonAxes.Checked ? colorControlUniqueAxis.Color : colorControlUniquePlane.Color;
            var general = radioButtonAxes.Checked ? colorControlGeneralAxis.Color : colorControlGeneralPlane.Color;
            var font = new Font("Tahoma", trackBarStrSize.Value / (float)mag / 7f);
            var radius = pointSize / mag;

            for (int n = 0; n < vector.Length; n++)
            {
                PointD srcPt;
                if (radioButtonWulff.Checked)
                    srcPt = Stereonet.ConvertVectorToWulff(formMain.Crystal.RotationMatrix * vector[n]);
                else
                    srcPt = Stereonet.ConvertVectorToSchmidt(formMain.Crystal.RotationMatrix * vector[n]);

                if (!formMain.YusaGonioMode)
                {
                    var brush = new SolidBrush(n < 6 && radioButtonRange.Checked ? unique : general);
                    if (srcPt.X * srcPt.X + srcPt.Y * srcPt.Y <= 1.2)
                    {
                        g.FillEllipse(brush, new RectangleF((float)(srcPt.X - radius), (float)(-srcPt.Y - radius), (float)(radius * 2), (float)(radius * 2)));
                        if (drawString)
                            g.DrawString(vector[n].Index, font, brush, (float)(srcPt.X + radius), (float)(-srcPt.Y + radius));
                    }
                }
                else
                {
                    positionRecorder[n].Add(srcPt);
                    for (int i = 0; i < positionRecorder[n].Count; i++)
                    {
                        //glAlpha.FillCircle(general, positionRecorder[n][i].X, positionRecorder[n][i].Y, pointSize / mag / 2, 60);
                        g.FillEllipse(new SolidBrush(general), new RectangleF((float)(positionRecorder[n][i].X - radius / 2), (float)(-positionRecorder[n][i].Y - radius / 2), (float)radius, (float)radius));
                        if (i != 0)
                            g.DrawLine(new Pen(new SolidBrush(general), 1f), positionRecorder[n][i - 1].X, positionRecorder[n][i - 1].Y, positionRecorder[n][i].X, positionRecorder[n][i].Y);
                    }
                }
            }
        }

        private void DrawCircles(Graphics g)
        {
            //大円描画
            Vector3D vec; float width;
            Pen pen = new Pen(colorControlGreatCircle.Color, 0.002f);
            for (int i = 0; i < checkedListBoxCircles.CheckedItems.Count; i++)
            {
                vec = Vector3D.Normarize(formMain.Crystal.RotationMatrix * (Vector3D)(checkedListBoxCircles.CheckedItems[i]));
                if (Math.Abs(vec.Z) > 0.9999)//大円が最外周とほぼ一致するときは
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
                    double sqrt = Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
                    g.DrawLine(pen, (float)(vec.Y / sqrt), (float)(vec.X / sqrt), (float)(-vec.Y / sqrt), (float)(-vec.X / sqrt));
                }
            }
        }

        //Src（単位なし）をClient(pixel)に変換
        private PointF convertSrcToClient(PointD pt) => new PointF((float)(graphicsBox.ClientSize.Width / 2.0 + mag * (pt.X - centerPt.X)), (float)(graphicsBox.ClientSize.Height / 2.0 + mag * (pt.Y - centerPt.Y)));

        private PointF convertSrcToClient(double x, double y) => convertSrcToClient(new PointD(x, y));

        private PointD convertClientToSrc(Point pt)
        {
            var p = new PointD(centerPt.X + (pt.X - graphicsBox.ClientSize.Width / 2) / mag, centerPt.Y + (graphicsBox.ClientSize.Height / 2 - pt.Y) / mag);
            return p;
        }

        private PointD convertClientToSrc(int x, int y) => convertClientToSrc(new Point(x, y));

        //指数範囲が変更されたとき
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            setVector();
            Draw();
        }

        private void setVector()
        {
            if (formMain.Crystal.A * formMain.Crystal.B * formMain.Crystal.C != 0)
            {
                if (radioButtonRange.Checked)
                {
                    formMain.Crystal.SetVectorOfAxis((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
                    formMain.Crystal.SetVectorOfPlane((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
                }
                else if (radioButtonSpecifiedIndices.Checked)
                {
                    var planeIndices = new List<(int H, int K, int L)>();
                    var axisIndices = new List<(int U, int V, int W)>();
                    foreach (object o in listBoxSpecifiedIndices.Items)
                    {
                        string[] str = ((string)o).Split(new char[] { ' ' });
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
                    formMain.Crystal.SetVectorOfAxis(axisIndices.ToArray());
                    formMain.Crystal.SetVectorOfPlane(planeIndices.ToArray());
                }
            }
        }

        //フォームの大きさが変更されたとき

        private Size lastgraphicsBoxSize = new Size(0, 0);

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

        private void FormStereonet_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            formMain.toolStripButtonStereonet.Checked = false;
        }

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

        private PointD lastMousePositionSrc = new PointD();
        private Point lastMousePositionClient = new Point();

        private void graphicsBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X > tabControl.Width || e.Y > tabControl.Height - 20)
            {
                graphicsBox.BringToFront();
                graphicsBox.Refresh();
            }
            PointD pt = convertClientToSrc(e.X, e.Y); ;
            double azimuth = Math.Asin(2 * pt.Y / (1 + pt.X * pt.X + pt.Y * pt.Y));
            double tilt = (Math.Cos(azimuth) != 0) ? Math.Asin(2 * pt.X / (1 + pt.X * pt.X + pt.Y * pt.Y) / Math.Cos(azimuth)) : 0;
            labelXpos.Text = "Tilt X: " + (azimuth / Math.PI * 180).ToString("f3") + "°";
            labelYpos.Text = "Tilt Y: " + (tilt / Math.PI * 180).ToString("f3") + "°";

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

        private void trackBarStrSize_Scroll(object sender, EventArgs e)
        {
            strFont = new Font("Tahoma", trackBarStrSize.Value / 9f);
            pointSize = trackBarPointSize.Value;
            Draw();
        }

        public void SetCrystal()
        {
            formMain.Crystal.SetVectorOfAxis((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            formMain.Crystal.SetVectorOfPlane((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);

            checkedListBoxCircles.Items.Clear();
            if (formMain.Crystal.VectorOfPole == null)
                formMain.Crystal.VectorOfPole = new List<Vector3D>();
            else
                for (int i = 0; i < formMain.Crystal.VectorOfPole.Count; i++)
                    checkedListBoxCircles.Items.Add(formMain.Crystal.VectorOfPole[i], true);
            Draw();
        }

        private void tabControl_Click(object sender, EventArgs e)
        {
            graphicsBox.SendToBack();
            graphicsBox.Refresh();
        }

        private void radioButtonOutlineEquator_CheckedChanged(object sender, EventArgs e) => Draw();

        private void checkBox1DegLine_CheckedChanged(object sender, EventArgs e) => Draw();

        private void radioButtonAxes_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
            if (radioButtonAxes.Checked)
                labelHU.Text = "u            v             w";
            else
                labelHU.Text = "h            k             l";
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var bmp = new Bitmap(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height);
            var g = Graphics.FromImage(bmp);
            Draw(g);
            if (bmp != null)
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = "Picture File[*.png]|*.png;";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = dialog.FileName;
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

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e) =>
            // 印刷プレビューを表示
            printPreviewDialog1.ShowDialog();

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Drawing.Printing.PageSettings ps = printDocument1.PrinterSettings.DefaultPageSettings;
            //用紙サイズ取得 このサイズは1/100インチ
            float height = (ps.PaperSize.Height - ps.Margins.Top - ps.Margins.Bottom) / 100f;
            float width = (ps.PaperSize.Width - ps.Margins.Left - ps.Margins.Right) / 100f;

            if (printDocument1.PrinterSettings.DefaultPageSettings.Landscape)
            {//縦横を逆転
                float temp = width; width = height; height = temp;
            }
            //解像度300dpiのときのイメージサイズは
            //glString.Font = new Font("Tahoma", trackBarStrSize.Value / 10f / 72f * 300f);
            var bmp = (Bitmap)graphicsBox.Image;

            bmp.SetResolution(300, 300);

            e.Graphics.PageUnit = GraphicsUnit.Inch;
            e.Graphics.DrawImage(bmp, new PointF(ps.Margins.Top / 100f, ps.Margins.Left / 100f));
            e.HasMorePages = false;

            //glString.Font = new Font("Tahoma", trackBarStrSize.Value / 10f);
        }

        private void buttonAddCircle_Click(object sender, EventArgs e)
        {
            if (radioButtonCircleByAxis.Checked)
            {
                int u = (int)numericUpDownCircleU.Value;
                int v = (int)numericUpDownCircleV.Value;
                int w = (int)numericUpDownCircleW.Value;
                if (u == 0 && v == 0 && w == 0) return;
                Vector3D vec = u * formMain.Crystal.A_Axis + v * formMain.Crystal.B_Axis + w * formMain.Crystal.C_Axis;
                vec.text = "[" + u.ToString() + " " + v.ToString() + " " + w.ToString() + "]";
                formMain.Crystal.VectorOfPole.Add(vec);
                checkedListBoxCircles.Items.Add(vec, true);
                Draw();
            }
            else if (radioButtonCircleByPlanes.Checked)
            {
                int h1 = (int)numericUpDownCircleH1.Value;
                int h2 = (int)numericUpDownCircleH2.Value;
                int k1 = (int)numericUpDownCircleK1.Value;
                int k2 = (int)numericUpDownCircleK2.Value;
                int l1 = (int)numericUpDownCircleL1.Value;
                int l2 = (int)numericUpDownCircleL2.Value;

                int u = k1 * l2 - k2 * l1;
                int v = l1 * h2 - l2 * h1;
                int w = h1 * k2 - h2 * k1;
                if (u == 0 && v == 0 && w == 0) return;

                Vector3D vec = u * formMain.Crystal.A_Axis + v * formMain.Crystal.B_Axis + w * formMain.Crystal.C_Axis;

                vec.text = "(" + h1.ToString() + " " + k1.ToString() + " " + l1.ToString() + ") & (" + h2.ToString() + " " + k2.ToString() + " " + l2.ToString() + ")";
                formMain.Crystal.VectorOfPole.Add(vec);
                checkedListBoxCircles.Items.Add(vec, true);
                Draw();
            }
        }

        private void buttonDeleteCircle_Click(object sender, EventArgs e)
        {
            if (checkedListBoxCircles.SelectedIndex > -1)
                formMain.Crystal.VectorOfPole.Remove((Vector3D)checkedListBoxCircles.SelectedItem);
            checkedListBoxCircles.Items.RemoveAt(checkedListBoxCircles.SelectedIndex);
        }

        private void radioButtonCircleByAxis_CheckedChanged(object sender, EventArgs e)
        {
            panelAxis.Enabled = radioButtonCircleByAxis.Checked;
            panelPlanes.Enabled = radioButtonCircleByPlanes.Checked;
        }

        private void colorControl_ColorChanged(object sender, EventArgs e) => Draw();

        private List<List<PointD>> positionRecorder = new List<List<PointD>>();

        private void buttonYusaModeStart_Click(object sender, EventArgs e)
        {
            setVector();
            positionRecorder = new List<List<PointD>>();
            for (int i = 0; i < (radioButtonPlanes.Checked ? formMain.Crystal.VectorOfPlane.Count : formMain.Crystal.VectorOfAxis.Count); i++)
                positionRecorder.Add(new List<PointD>());

            //formMain.OriginalRotation = (Matrix3D)formMain.RotMatrix.Clone();
            formMain.YusaGonioMode = true;
            formMain.timer.Start();
        }

        public void buttonYusaModeStop_Click(object sender, EventArgs e)
        {
            formMain.timer.Stop();

            StringBuilder sb = new StringBuilder();
            if (positionRecorder.Count > 0)
            {
                for (int i = 0; i < positionRecorder.Count; i++)
                {
                    Vector3D v = radioButtonPlanes.Checked ? formMain.Crystal.VectorOfPlane[i] : formMain.Crystal.VectorOfAxis[i];
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

        private void radioButtonRange_CheckedChanged(object sender, EventArgs e)
        {
            labelPlusMinus1.Visible = labelPlusMinus2.Visible = labelPlusMinus3.Visible = radioButtonRange.Checked;
            panelSpecifiedIndices.Visible = !radioButtonRange.Checked;

            if(radioButtonRange.Checked)
                numericUpDown1.Minimum = numericUpDown2.Minimum = numericUpDown3.Minimum = 0;
            else
                numericUpDown1.Minimum = numericUpDown2.Minimum = numericUpDown3.Minimum = -numericUpDown1.Maximum;

            setVector();
            Draw();
        }

        private void buttonAddIndex_Click(object sender, EventArgs e)
        {
            int index1 = (int)numericUpDown1.Value, index2 = (int)numericUpDown2.Value, index3 = (int)numericUpDown3.Value;
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

        private void button1_Click(object sender, EventArgs e)
        {
            formMain.Crystal.VectorOfAxis = new List<Vector3D>();
            int div = 15;
            div = 30;
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

        private void FormStereonet_Paint(object sender, PaintEventArgs e) => Draw();
    }
}
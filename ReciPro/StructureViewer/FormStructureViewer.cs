using Crystallography;
using Crystallography.Controls;
using Crystallography.OpenGL;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using C4 = OpenTK.Graphics.Color4;
using V3 = OpenTK.Vector3d;
using V4 = OpenTK.Vector4d;

namespace ReciPro
{
    public partial class FormStructureViewer : Form
    {
        #region フィールド、プロパティ、

        public Crystal Crystal;

        public FormMain formMain;
        private FormAtom formAtom;

        private V3 shift;
        private Matrix3d axes;
        private List<(V4 prm, Color color)> bounds;

        private BoundControl boundControl;
        private LatticePlaneControl latticePlaneControl;
        private AtomControl atomControl;
        private BondInputControl bondControl;
      
        public List<GLObject> GLObjects = new List<GLObject>();

        public readonly object lockObj = new object();

        private readonly List<V3> dirs = new List<V3> { new V3(1, 0, 0), new V3(-1, 0, 0), new V3(0, 1, 0), new V3(0, -1, 0), new V3(0, 0, 1), new V3(0, 0, -1) };
        private readonly List<V3> vrts = new List<V3> { new V3(.5, .5, .5), new V3(-.5, .5, .5), new V3(.5, -.5, .5), new V3(.5, .5, -.5), new V3(.5, -.5, -.5), new V3(-.5, .5, -.5), new V3(-.5, -.5, .5), new V3(-.5, -.5, -.5) };

        private bool skipSetCrystal = false;

        public bool SkipEvent { get; set; } = false;

        public List<GLControlAlpha> legendControls = new List<GLControlAlpha>();
        public List<Label> legendLabels = new List<Label>();
        public List<FlowLayoutPanel> legendPanels = new List<FlowLayoutPanel>();


        #endregion フィールド

        #region ローカルクラス
        private class atomID
        {
            public bool IsInside;
            public Atoms Atoms;
            public int N;

            public atomID(Atoms atoms, bool isInside, int n)
            {
                IsInside = isInside;
                Atoms = atoms;
                N = n;
            }
        }

        private class bondID
        {
            public int SerialNumber1, SerialNumber2;

            public bondID(int serialNumber1, int serialNumber2)
            {
                SerialNumber1 = serialNumber1;
                SerialNumber2 = serialNumber2;
            }
        }

        private class cellID { }
        private class latticeID { }
        private class boundsID { }
        #endregion

        #region コンストラクタ

        public FormStructureViewer() => InitializeComponent();

        private void FormStructureViewer_Load(object sender, EventArgs e)
        {
            formAtom = new FormAtom();
            formAtom.formStructureViewer = this;
            AddOwnedForm(formAtom);

            glControlLight.AddObjects(new Sphere(new V3(0, 0, 0), 1.0, new Material(C4.Gray, 0.2, 0.7, 0.7, 50, 0.2), DrawingMode.Surfaces));
            glControlMain.LightPosition = glControlLight.LightPosition = glControlAxes.LightPosition = new V3(100, 100, 100);
            glControlMain.ViewFrom = glControlLight.ViewFrom = glControlAxes.ViewFrom = new V3(0, 0, 50);
            glControlLight.ProjWidth = glControlAxes.ProjWidth = 2.2;
            glControlMain.ProjWidth = 5f;

            tabControlBoundOption.ItemSize = new Size(0, 1);

            //各種ユーザーコントロール
            boundControl = formMain.crystalControl.boundControl;
            latticePlaneControl = formMain.crystalControl.latticePlaneControl;
            atomControl = formMain.crystalControl.atomControl;
            bondControl = formMain.crystalControl.bondControl;

            tabPageBond.Controls.Add(bondControl);
            tabPageBoundPlane.Controls.Add(boundControl);
            tabPageLatticePlane.Controls.Add(latticePlaneControl);
            latticePlaneControl.BringToFront();


            boundControl.ItemsChanged += BoundControl_BoundsChanged;
            latticePlaneControl.ItemsChanged += LatticePlaneControl_LatticePlaneChanged;
            bondControl.ItemsChanged += BondControl_BondsChanged;
            atomControl.ItemsChanged += AtomControl_ItemsChanged;

            flowLayoutPanelLegend.AutoSize = true;
        }

      

        #endregion コンストラクタ

        private void initAxesMatrix()
        {
            axes.Row0 = new V3(Crystal.A_Axis.X, Crystal.B_Axis.X, Crystal.C_Axis.X);
            axes.Row1 = new V3(Crystal.A_Axis.Y, Crystal.B_Axis.Y, Crystal.C_Axis.Y);
            axes.Row2 = new V3(Crystal.A_Axis.Z, Crystal.B_Axis.Z, Crystal.C_Axis.Z);
            int n = Crystal.Symmetry.CrystalSystemNumber;
            shift = (n >= 5 && n <= 6) ? 
                new V3(0, 0, 0) : 
                (axes.Column0 + axes.Column1 + axes.Column2) / 2;
        }

        /// <summary>
        /// 境界面を初期化
        /// </summary>
        private void initBounds()
        {
            bounds = new List<(V4 prm, Color color)>();
            foreach (var bc in boundControl.GetAll().Where(b => b.Enabled && b.PlaneParams != null && b.Index != (0, 0, 0)))
                foreach (var (X, Y, Z, D) in bc.PlaneParams)
                    bounds.Add((new V4(X, Y, Z, D), bc.Color));
            
            if (radioButtonBoundUnitCell.Checked || !Geometriy.Enclosed(bounds.Select(b => b.prm.ToArray()).ToArray()))
            {//境界条件としてUnit cellが選択されているか、Planeが選択されているが描画範囲が閉じていない場合 、単位格子を境界とする
                bounds = new List<(V4 prms, Color color)>()
                {
                    (new V4(axes.Column0.Normalized(),axes.Column0.Length * (numericBoxACenter.Value + numericBoxARange.Value)) , Color.Gray),
                    (new V4(-axes.Column0.Normalized(),axes.Column0.Length *  -(numericBoxACenter.Value - numericBoxARange.Value)), Color.Gray),
                    (new V4(axes.Column1.Normalized(),axes.Column1.Length *  (numericBoxBCenter.Value + numericBoxBRange.Value)), Color.Gray),
                    (new V4(-axes.Column1.Normalized(),axes.Column1.Length * -(numericBoxBCenter.Value - numericBoxBRange.Value)), Color.Gray),
                    (new V4(axes.Column2.Normalized(),axes.Column2.Length * (numericBoxCCenter.Value + numericBoxCRange.Value)), Color.Gray),
                    (new V4(-axes.Column2.Normalized(),axes.Column2.Length * -(numericBoxCCenter.Value - numericBoxCRange.Value)), Color.Gray),
                };
            }
        }

        #region Bounds (境界面) GLObjects の生成

        /// <summary>
        /// 境界面オブジェクトを生成
        /// </summary>
        private void setBoundPlanes()
        {
            if (bounds == null)
                return;
            //境界面を追加
            for (int i = 0; i < bounds.Count; i++)
            {
                var vertices = Geometriy.GetClippedPolygon(i, bounds.Select(b => b.prm.ToArray()).ToArray());
                var mat = new Material(bounds[i].color.ToArgb(), numericBoxBoundPlanesOpacity.Value, 0.2, 0.8, 0.8, 50, 0.2);
                if (vertices.Length >= 3)
                {
                    GLObjects.Add(
                        new Polygon(vertices.Select(v => new V3(v[0], v[1], v[2])).ToArray(), mat, DrawingMode.SurfacesAndEdges)
                        {
                            Rendered = checkBoxShowBoundPlanes.Checked,
                            Tag = new boundsID()
                        });
                }
            }
            glControlMain.SetClip(checkBoxClipObjects.Checked ? new Clip(bounds.Select(b => b.prm).ToArray()) : null);
        }

        #endregion

        #region 原子 GLObjectsの生成

        /// <summary>
        /// 原子オブジェクトを生成
        /// </summary>
        public void setAtoms()
        {
            if (checkBoxHideAllAtoms.Checked) return;

            //閾値. 描画範囲がこの数値分超えたとしても、一応原子座標は計算しておいて、ボンドの有無を考慮し、最終的には消す
            double threshold = -0.4;

            //まず検索対象とするCellの範囲を決める
            var cells = new List<V3>() { new V3(0, 0, 0) };
            var outer = new List<V3>() { new V3(0, 0, 0) };
            while (outer.Count != 0 && cells.Count < 1000000)
            {
                var outerOld = outer.ToList();
                outerOld.ForEach(baseCell => dirs.Select(dir => dir + baseCell).ToList().ForEach(targetCell =>
                {
                    if (!cells.Contains(targetCell) && vrts.Any(vrt => bounds.Min(b => V4.Dot(b.prm, new V4(axes.Mult((targetCell + vrt)), 1)) > threshold)))
                    {
                        cells.Add(targetCell);
                        outer.Add(targetCell);
                    }
                }));
                outerOld.ForEach(cell => outer.Remove(cell));
            }

            //原子を追加
            Parallel.For(0, Crystal.Atoms.Length, i =>
            {
                var atoms = Crystal.Atoms[i];
                var mat = new Material(atoms.Argb, atoms.Transparency, atoms.Ambient, atoms.Diffusion, atoms.Specular, atoms.Shininess, atoms.Emission);
                //位置が全く同じ原子が存在する場合は、最もOccが大きいものを選ぶ。それが複数ある場合は、indexが若い方を選ぶ
                if (Crystal.Atoms.Where((a, j) => i != j && atoms.X == a.X && atoms.Y == a.Y && atoms.Z == a.Z && (atoms.Occ < a.Occ || (atoms.Occ <= a.Occ && i > j))).Count() == 0)
                {
                    foreach (var atom in atoms.Atom.Select(v => new V3(v.X, v.Y, v.Z)))
                        foreach (var pos in cells.Select(t => t + atom).Select(p => new V4(axes.Mult(p) - shift, 1)))
                        {
                            var min = bounds.Min(b => V4.Dot(pos, b.prm));
                            if (min > threshold)
                            {
                                var sphere = new Sphere(new V3(pos), atoms.Radius * 0.1, mat, DrawingMode.Surfaces)
                                {
                                    Rendered = min > -0.0000001,
                                    Tag = new atomID(atoms, min > -0.0000001, i)
                                };
                                lock (lockObj)
                                    GLObjects.Add(sphere);
                            }
                        }
                }
            }
            );
        }
        #endregion

        #region Bonds(結合)とPolyhedra (配位多面体)オブジェクトの生成

        /// <summary>
        /// 結合(Bonds)と配位多面体(Polyhera)オブジェクトを生成
        /// </summary>
        private void setBondsAndPolyhera()
        {
            var GLObjectsP = GLObjects.AsParallel();
            //bondsとpolyhedraを追加
            foreach (var bond in Crystal.Bonds)
            {
                var bondMat = new Material(bond.ArgbBond, bond.BondTransParency, 0.2, 0.8, 0.8, 50, 0.2);
                var polyhedronMat = new Material(bond.ArgbPolyhedron, bond.PolyhedronTransParency, 0.2, 0.8, 0.8, 50, 0.2);
                var elementCenters = GLObjectsP.Where(obj => obj.Tag is atomID id && id.Atoms.ElementName == bond.Element1 && id.IsInside).Cast<Sphere>().ToList();
                var elementVertices = GLObjectsP.Where(obj => obj.Tag is atomID id && id.Atoms.ElementName == bond.Element2).Cast<Sphere>().ToList();
                Parallel.ForEach(elementCenters, c =>
                {
                    var vertices = elementVertices.Where(e2 => (e2.Origin - c.Origin).Length > bond.MinLength * 0.1 && (e2.Origin - c.Origin).Length < bond.MaxLength * 0.1);
                    if (vertices.Count() > 0)
                    {
                        foreach (var v in vertices)
                        {
                            var cylinder = new Cylinder(c.Origin, v.Origin - c.Origin, bond.Radius * 0.1, bondMat, DrawingMode.Surfaces);
                            cylinder.Tag = new bondID(c.SerialNumber, v.SerialNumber);
                            cylinder.ShowClippedSection = false;
                            lock (lockObj)
                            {
                                GLObjects.Add(cylinder);
                                GLObjects.First(obj => obj.SerialNumber == v.SerialNumber).Rendered = true;
                            }
                        }

                        if (bond.ShowPolyhedron)
                        {
                            if (vertices.Count() == 3)
                            {
                                var polygon = new Polygon(vertices.Select(v => v.Origin).ToArray(), polyhedronMat, bond.ShowEdges ? DrawingMode.SurfacesAndEdges : DrawingMode.Surfaces);
                                lock (lockObj)
                                    GLObjects.Add(polygon);
                            }
                            else if (vertices.Count() > 3)
                            {
                                var polyhedron = new Polyhedron(vertices.Select(v => v.Origin).ToArray(), polyhedronMat, bond.ShowEdges ? DrawingMode.SurfacesAndEdges : DrawingMode.Surfaces);
                                polyhedron.ShowClippedSection = false;
                                lock (lockObj)
                                    GLObjects.Add(polyhedron);
                            }
                        }
                    }
                }
                );
            }
        }

        #endregion

        #region 余分な原子を削除
        /// <summary>
        /// 余分な原子を削除する
        /// </summary>
        private void removeObjects()
        {
            //var GLObjectsP = GLObjects.AsParallel();
            //ボンドを構成する原子だが、描画範囲外のため孤立してしまった原子を削除
            var vertexSerials = GLObjects.Where(obj => obj is Cylinder).Select(obj => (obj.Tag as bondID).SerialNumber2).Distinct().ToList();
            var vertexNs = vertexSerials.Select(serial => (GLObjects.First(obj => obj.SerialNumber == serial).Tag as atomID).N).Distinct().ToList();
            var removeList = GLObjects.Where(obj => obj is Sphere).Where(obj => vertexNs.Contains(((atomID)obj.Tag).N)).Where(obj => !vertexSerials.Contains(obj.SerialNumber)).ToList();
            foreach (var obj in removeList)
                obj.Rendered = false;

            for (int i = 0; i < GLObjects.Count; i++)
                if (GLObjects[i].Rendered == false)
                    GLObjects.RemoveAt(i--);
        }
        #endregion

        /// <summary>
        /// GLObjectsを転送する
        /// </summary>
        private void transferGLObjects()
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            glControlMain.DeleteAllObjects();
            glControlMain.AddObjects(GLObjects);
            toolStripLabelStatusInitialization.Text += " and sent to OpenGL (" + sw.ElapsedMilliseconds + " ms.)    ";
        }

        #region 単位格子面オブジェクトを生成
        /// <summary>
        /// 単位格子面オブジェクトを生成
        /// </summary>
        private void setUnitCellPlanes()
        {
            while (GLObjects.Count(obj => obj.Tag is cellID) != 0)
            {
                glControlMain.DeleteObjects(GLObjects.First(obj => obj.Tag is cellID));
                GLObjects.Remove(GLObjects.First(obj => obj.Tag is cellID));
            }

            var cellVertices = new[] { new V3(0), axes.Column0, axes.Column1, axes.Column2, axes.Column0 + axes.Column1, axes.Column1 + axes.Column2, axes.Column2 + axes.Column0, axes.Column0 + axes.Column1 + axes.Column2 };
            var translation = axes.Mult(new V3(numericBoxCellTransrationA.Value, numericBoxCellTransrationB.Value, numericBoxCellTransrationC.Value)) + shift;

            cellVertices = cellVertices.Select(v => v - translation).ToArray();
            var cellPlaneMat = new Material(colorControlCellPlane.Argb, numericBoxCellPlaneAlpha.Value, 0.2, 0.8, 0.8, 50, 0.2);
            var cellPlane = new Polyhedron(cellVertices, cellPlaneMat, DrawingMode.Surfaces);
            cellPlane.Tag = new cellID();
            var cellEdgeMat = new Material(colorControlCellEdge.Argb, 1, 0.2, 0.8, 0.8, 50, 0.2);
            var cellEdge = new Polyhedron(cellVertices, cellEdgeMat, DrawingMode.Edges);
            cellEdge.Tag = new cellID();

            //cellPlane.UseFixedColor = true;
            cellPlane.Rendered = false;
            cellEdge.Rendered = false;

            if (checkBoxUnitCell.Checked)
            {
                cellPlane.Rendered = checkBoxCellShowPlane.Checked;
                cellEdge.Rendered = checkBoxCellShowEdge.Checked;
            }

            GLObjects.Add(cellPlane);
            glControlMain.AddObjects(cellPlane);
            GLObjects.Add(cellEdge);
            glControlMain.AddObjects(cellEdge);

            Draw();
        }
        #endregion

        #region 結晶格子面 GLObjectsの生成

        /// <summary>
        /// 格子面オブジェクトを生成
        /// </summary>
        public void SetLatticePlanes()
        {

            while (GLObjects.Count(obj => obj.Tag is latticeID) != 0)
            {
                glControlMain.DeleteObjects(GLObjects.First(obj => obj.Tag is latticeID));
                GLObjects.Remove(GLObjects.First(obj => obj.Tag is latticeID));
            }

            var latticePlanes = new List<((double X, double Y, double Z, double D), double t, Color color)>();
            foreach (var p in latticePlaneControl.GetAll().Where(p => p.Enabled && p.Index != (0, 0, 0)))
                latticePlanes.Add((p.PlaneParam, p.Translation, p.Color));

            var boundArray = bounds.Select(b => new[] { b.prm[0], b.prm[1], b.prm[2], b.prm[3] * 1.2 }).ToArray();

            foreach (var (prms, t, color) in latticePlanes)
            {
                var mat = new Material(color.ToArgb(), numericBoxLatticePlaneOpacity.Value, 0.2, 0.8, 0.8, 50, 0.2);
                int n = 0;
                var flag = true;
                var prm = new[] { prms.X, prms.Y, prms.Z, 0 };
                while (flag)
                {
                    var verticesList = new List<double[][]>();
                    for (int i = 0; i < (n == 0 ? 1 : 2); i++)
                        verticesList.Add(Geometriy.GetClippedPolygon(new[] { prms.X, prms.Y, prms.Z, ((i == 0 ? n : -n) + t) * prms.D }, boundArray));

                    flag = false;
                    foreach (var vertices in verticesList.Where(v => v.Length >= 3))
                    {
                        var polygon = new Polygon(vertices.Select(v => new V3(v[0], v[1], v[2])).ToArray(), mat, DrawingMode.SurfacesAndEdges);
                        polygon.Tag = new latticeID();
                        GLObjects.Add(polygon);
                        glControlMain.AddObjects(polygon);
                        flag = true;
                    }
                    n++;
                }
            }
            Draw();
        }
        #endregion

        #region 結晶格子軸を生成

        /// <summary>
        /// 格子軸GLControlを生成
        /// </summary>
        private void setAxesControl()
        {
            var cry = formMain.Crystal;
            var max = new[] { cry.A, cry.B, cry.C }.Max();
            var vec = new[] { cry.A_Axis / max, cry.B_Axis / max, cry.C_Axis / max };
            var color = new[] { C4.Red, C4.Green, C4.Blue };

            var obj = new List<GLObject>();
            var mat = new Material(C4.White, 0.2, 0.7, 0.8, 50, 0.2);
            for (int i = 0; i < 3; i++)
            {
                mat.Color = color[i];
                obj.Add(new Cylinder(-vec[i], vec[i] * 2 - 0.3 * vec[i].Normarize(), 0.075, mat, DrawingMode.Surfaces));
                obj.Add(new Cone(vec[i], -0.3 * vec[i].Normarize(), 0.15, mat, DrawingMode.Surfaces));
            }
            mat.Color = C4.Gray;
            obj.Add(new Sphere(new V3(0, 0, 0), 0.12, mat, DrawingMode.Surfaces));

            glControlAxes.DeleteAllObjects();
            glControlAxes.AddObjects(obj);
        }
        #endregion

        /// <summary>
        /// 結晶構造をセッティング
        /// </summary>
        /// <param name="_crystal">nullでない場合は、Bounds と Lattice planes に関するコントロールがリセットされる</param>
        public void SetGLObjects(Crystal _crystal = null)
        {
            if (skipSetCrystal) return;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            if(_crystal!=null)
                SetLegend();

            Crystal = _crystal == null ? formMain.Crystal : _crystal;

            atomCoordinateTable1.Crystal = Crystal;

            GLObjects = new List<GLObject>(); //GLObjectsを初期化

            initAxesMatrix(); //結晶軸マトリックスを初期化

            setUnitCellPlanes();//単位格子面の描画

            initBounds();//境界条件を初期化;

            SetLatticePlanes();//格子面オブジェクトを生成

            setBoundPlanes();//境界面オブジェクトを生成

            setAtoms();//原子オブジェクトを生成

            setBondsAndPolyhera();//結合と多面体オブジェクトを生成

            removeObjects();//余計な原子を削除

            //glControlLegend.Size = new Size(glControlLegend.Size.Width, crystal.Atoms.Length * 20);
            // SetLatticePlaneProperty();

            toolStripLabelStatusInitialization.Text = GLObjects.Count + " objects were created (" + sw.ElapsedMilliseconds + " ms)";

            transferGLObjects();

            
            setAxesControl();//結晶軸を表示するGLControl

            Draw();
        }

        #region Draw

        /// <summary>
        /// OpenGLによる描画を行う。
        /// </summary>
        public void Draw()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var world = formMain.Crystal.RotationMatrix.Transpose(); ;
            //WorldMatrixを代入したら自動でRender()も行われる
            glControlMain.WorldMatrixEx = world;
            glControlAxes.WorldMatrixEx = world;

            //for (int i = 0; i < legendControls.Count; i++)
            //    legendControls[i].WorldMatrixEx = world;

            toolStripLabelStatusRendering.Text = "Rendering time: " + sw.ElapsedMilliseconds + " ms.";
        }


        #endregion Draw

        #region その他イベント

        private void FormStructureViewer_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Draw();
        }

        private void FormStructureViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            formMain.toolStripButtonStructureViewer.Checked = false;
        }

        private void FormStructureViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.C)
                Clipboard.SetDataObject(glControlMain.GenerateBitmap());
        }

        private void FormStructureViewer_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)//現れたときメインウィンドウの結晶を表示する
                if (formMain.crystalControl.Crystal != null)
                    SetGLObjects(formMain.crystalControl.Crystal);

            MoveAtomControl(Visible && tabControl.SelectedTab == tabPageAtom);
        }

        #endregion その他イベント

        #region マウスイベント

        private Point lastPosMain = new Point();
        private Point lastPosLight = new Point();
        private Point lastPosAxes = new Point();

        //Point animatitonStartPt, animationEndPt;

        /// <summary>
        /// マウスドラッグで回転
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControlMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > tabControl.Width || e.Y > tabControl.Height - 20)
                tabControl.SendToBack();

            if (e.Button == MouseButtons.Left)
            {
                var rot = getRotation(e, glControlMain.ClientSize, lastPosMain, false);
                formMain.Rotate((rot.X, rot.Y, rot.Z), rot.W);
            }
            lastPosMain = new Point(e.X, e.Y);
        }

        private void glControlAxes_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var rot = getRotation(e, glControlAxes.ClientSize, lastPosAxes, true);
                formMain.Rotate((rot.X, rot.Y, rot.Z), rot.W);
            }
            lastPosAxes = new Point(e.X, e.Y);
        }

        private void glControlLight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var rot = getRotation(e, glControlLight.ClientSize, lastPosLight, true);
                var rotMat = Matrix3d.CreateFromAxisAngle(-new V3(rot), rot.W);
                if (rotMat.M11 == double.NaN) return;
                glControlLight.LightPosition = glControlMain.LightPosition = glControlAxes.LightPosition = rotMat.Mult(glControlLight.LightPosition);
            }
            lastPosLight = new Point(e.X, e.Y);
        }

        private V4 getRotation(MouseEventArgs e, Size size, Point lastPos, bool ignoreZRotation)
        {
            float dx = e.X - lastPos.X, dy = lastPos.Y - e.Y;
            if (ignoreZRotation)
                return new V4(-dy, dx, 0, Math.Sqrt(dx * dx + dy * dy) / 360 * Math.PI);
            else
            {
                double x = e.X - size.Width / 2.0, y = e.Y - size.Height / 2.0, r = Math.Min(size.Width / 2.0, size.Height / 2.0);
                if (r * r * 0.7 > x * x + y * y)
                    return new V4(-dy, dx, 0, Math.Sqrt(dx * dx + dy * dy) / 360 * Math.PI);
                else
                {
                    double lastx = lastPos.X - size.Width / 2.0, lasty = lastPos.Y - size.Height / 2.0;
                    var angle = Math.Atan2(x, y) - Math.Atan2(lastx, lasty);
                    return new V4(0, 0, 1, angle);
                }
            }
        }

        /// <summary>
        /// ピクチャーボックスをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelMain_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left && e.Clicks == 2) || (e.Button == MouseButtons.Right && e.Clicks == 1))
            {
                //「幾何学」フォルダの「直線と点の距離.docx」を参照
                var ex = 2.0 * e.X / glControlMain.ClientSize.Width - 1;
                var ey = 1 - 2.0 * e.Y / glControlMain.ClientSize.Height;

                var m = Matrix4d.Transpose(glControlMain.ViewMatrix * glControlMain.ProjMatrix);
                double M11 = m.M11 - ex * m.M41, M12 = m.M12 - ex * m.M42, M13 = m.M13 - ex * m.M43, M14 = m.M14 - ex * m.M44;
                double M21 = m.M21 - ey * m.M41, M22 = m.M22 - ey * m.M42, M23 = m.M23 - ey * m.M43, M24 = m.M24 - ey * m.M44;

                double p = M13 * M22 - M23 * M12, q = M23 * M11 - M13 * M21, r = M12 * M21 - M22 * M11;
                double a = (M14 * M22 - M12 * M24) / r, b = (M24 * M11 - M14 * M21) / r, c = 0;
                double p2 = p * p, q2 = q * q, r2 = r * r, pq = p * q, qr = q * r, rp = r * p;

                var rot = Matrix4d.Transpose(glControlMain.WorldMatrix);

                var depthList = new SortedList<double, int>();
                for (int i = 0; i < GLObjects.Count; i++)
                    if (GLObjects[i] is Sphere sphere)
                    {
                        var origin = rot.Mult(new V4(sphere.Origin, 1));
                        double x = origin.X - a, y = origin.Y - b, z = origin.Z - c;
                        if (sphere.Radius * sphere.Radius > ((q2 + r2) * x * x + (r2 + p2) * y * y + (p2 + q2) * z * z - 2 * (pq * x * y + qr * y * z + rp * z * x)) / (p2 + q2 + r2))
                            depthList.Add(origin.Z, i);
                    }
                if (depthList.Count > 0)
                {
                    var sphere = GLObjects[depthList.Last().Value] as Sphere;
                    textBoxInformation.AppendText((sphere.Tag as atomID).Atoms.Label + " (" + sphere.Origin.X + ", " + sphere.Origin.Y + ", " + sphere.Origin.Z + ")\r\n");
                    //sphere.Mode = sphere.Mode == DrawingMode.SurfacesAndEdges ? DrawingMode.Surfaces : DrawingMode.SurfacesAndEdges;
                    glControlMain.Render();
                }
            }
            #region お蔵入り ?

            /*
            //原子選択
            if ((e.Button == MouseButtons.Left && e.Clicks == 2) || (e.Button == MouseButtons.Right && e.Clicks == 1))
            {
                double[] A = BoudaryTest(matrix, mouse.X, mouse.Y);
                int selectedAtom = -1;
                double selectedAtomZ = double.PositiveInfinity;
                for (int i = atoms.Count - 1; i >= 0; i--)
                    if (atoms[i].IsDraw)
                    {
                        double Ax = atoms[i].position.X, Ay = atoms[i].position.Y, Az = atoms[i].position.Z;
                        double z = (matrix * generateMat(new double[] { Ax, Ay, Az, 1 }, 4, 1))[2, 0];
                        if (selectedAtomZ > z)
                        {
                            double[] a = new double[] { Ax * Ax, Ay * Ay, Az * Az, Ax * Ay, Ay * Az, Az * Ax, Ax, Ay, Az };
                            if (atoms[i].radius * atoms[i].radius > a[0] * A[0] + a[1] * A[1] + a[2] * A[2] + a[3] * A[3] + a[4] * A[4] + a[5] * A[5] + a[6] * A[6] + a[7] * A[7] + a[8] * A[8] + A[9])
                            {
                                selectedAtom = i;
                                selectedAtomZ = z;
                                atomCoordinateTable1.Atom = crystal.Atoms[atoms[selectedAtom].MainID];
                            }
                        }
                    }
                if (e.Button == MouseButtons.Left && selectedAtom < 0)
                {
                    for (int i = atoms.Count - 1; i >= 0; i--)
                        atoms[i].selectedNo = 0;
                    selectedAtomCount = 0;
                }
                else if (e.Button == MouseButtons.Left && selectedAtom >= 0 && atoms[selectedAtom].selectedNo != 0)
                {
                    int n = 0;
                    if (atoms[selectedAtom].selectedNo == 1)
                        for (int i = atoms.Count - 1; i >= 0 && n < 2; i--)
                        {
                            if (atoms[i].selectedNo == 2) { atoms[i].selectedNo = 1; n++; }
                            else if (atoms[i].selectedNo == 3) { atoms[i].selectedNo = 2; n++; }
                        }
                    if (atoms[selectedAtom].selectedNo == 2)
                        for (int i = atoms.Count - 1; i >= 0; i--)
                            if (atoms[i].selectedNo == 3) { atoms[i].selectedNo = 2; break; }
                    atoms[selectedAtom].selectedNo = 0;
                    selectedAtomCount--;
                }
                else if (e.Button == MouseButtons.Left && selectedAtom >= 0 && atoms[selectedAtom].selectedNo == 0)
                {
                    selectedAtomCount++;
                    if (selectedAtomCount == 4)
                    {
                        for (int i = atoms.Count - 1; i >= 0; i--)
                            atoms[i].selectedNo = 0;
                        selectedAtomCount = 1;
                        atoms[selectedAtom].selectedNo = 1;
                    }
                    else
                        atoms[selectedAtom].selectedNo = selectedAtomCount;
                }
                else if (e.Button == MouseButtons.Right && selectedAtom >= 0 && atoms[selectedAtom].selectedNo != 0)
                {
                    formAtom.SkipChange = true;
                    formAtom.Location = new Point(this.Location.X + splitContainer1.Location.X + e.X + 20, this.Location.Y + splitContainer1.Location.Y + e.Y + 50);
                    formAtom.StartPosition = FormStartPosition.Manual;
                    formAtom.pictureBoxAtomColor.BackColor = Color.FromArgb(atoms[selectedAtom].colorSource);
                    formAtom.numericUpDownAtomTransparency.Value = (decimal)atoms[selectedAtom].matSource[0];
                    formAtom.numericUpDownAtomAmbient.Value = (decimal)atoms[selectedAtom].matSource[1];
                    formAtom.numericUpDownAtomDiffusion.Value = (decimal)atoms[selectedAtom].matSource[2];
                    formAtom.numericUpDownAtomSpecular.Value = (decimal)atoms[selectedAtom].matSource[3];
                    formAtom.numericUpDownAtomEmmision.Value = (decimal)atoms[selectedAtom].matSource[4];
                    formAtom.numericUpDownAtomShininess.Value = (decimal)atoms[selectedAtom].matSource[5];
                    formAtom.numericUpDownAtomRadius.Value = (decimal)atoms[selectedAtom].radius;
                    formAtom.selectedAtom = selectedAtom;

                    formAtom.checkBoxIsDraw.Checked = atoms[selectedAtom].IsDraw;
                    formAtom.SkipChange = false;
                    formAtom.SetOriginal();
                    formAtom.ShowDialog();
                }

                textBoxInformation.Text = "";
                if (selectedAtomCount > 0)
                {
                    atom[] a = new atom[3];
                    string str = "";
                    int[] list = new int[] { 0, 0, 0 };
                    for (int i = atoms.Count - 1; i >= 0; i--)
                        if (atoms[i].selectedNo == 1) a[0] = atoms[i];
                        else if (atoms[i].selectedNo == 2) a[1] = atoms[i];
                        else if (atoms[i].selectedNo == 3) a[2] = atoms[i];
                    if (a[0] != null)
                        str += "Atom 1:  " + "label: " + a[0].Label + "  element: " + a[0].element + "  ID: " + a[0].MainID.ToString() + "-" + a[0].SubID.ToString() +
                        "  Pos.: " + "(" + a[0].position.X.ToString("f3") + "," + a[0].position.Y.ToString("f3") + "," + a[0].position.Z.ToString("f3") + ")[Å] " +
                        "(" + a[0].positionRatio.X.ToString("f3") + "," + a[0].positionRatio.Y.ToString("f3") + "," + a[0].positionRatio.Z.ToString("f3") + ")[Cell]\r\n";
                    if (a[1] != null)
                        str += "Atom 2:  " + "label: " + a[1].Label + "  element: " + a[1].element + "  ID: " + a[1].MainID.ToString() + "-" + a[1].SubID.ToString() +
                        "  Pos.: " + "(" + a[1].position.X.ToString("f3") + "," + a[1].position.Y.ToString("f3") + "," + a[1].position.Z.ToString("f3") + ")[Å] " +
                        "(" + a[1].positionRatio.X.ToString("f3") + "," + a[1].positionRatio.Y.ToString("f3") + "," + a[1].positionRatio.Z.ToString("f3") + ")[Cell]\r\n";
                    if (a[2] != null)
                        str += "Atom 3:  " + "label: " + a[2].Label + "  element: " + a[2].element + "  ID: " + a[2].MainID.ToString() + "-" + a[2].SubID.ToString() +
                        "  Pos.: " + "(" + a[2].position.X.ToString("f3") + "," + a[2].position.Y.ToString("f3") + "," + a[2].position.Z.ToString("f3") + ")[Å] " +
                        "(" + a[2].positionRatio.X.ToString("f3") + "," + a[2].positionRatio.Y.ToString("f3") + "," + a[2].positionRatio.Z.ToString("f3") + ")[Cell]\r\n";

                    if (a[2] != null)
                    {
                        str += "Distance[Å]:" +
                            "     Atom 1-2:  " + ((Vector3D)(a[0].position - a[1].position)).Length().ToString("f4") +
                            "     Atom 2-3:  " + ((Vector3D)(a[1].position - a[2].position)).Length().ToString("f4") +
                            "     Atom 3-1:  " + ((Vector3D)(a[2].position - a[0].position)).Length().ToString("f4") + "\r\n";
                        str += "Angle[°]:" +
                            "     Atom 1-2-3:  " + (Vector3D.AngleBetVectors(a[0].position - a[1].position, a[2].position - a[1].position) / Math.PI * 180).ToString("f4") +
                            "     Atom 2-3-1:  " + (Vector3D.AngleBetVectors(a[1].position - a[2].position, a[0].position - a[2].position) / Math.PI * 180).ToString("f4") +
                            "     Atom 3-1-2:  " + (Vector3D.AngleBetVectors(a[2].position - a[0].position, a[1].position - a[0].position) / Math.PI * 180).ToString("f4");
                    }
                    else if (a[1] != null)
                        str += "Distance[Å]:" + "     Atom 1 to 2:  " + ((Vector3D)(a[0].position - a[1].position)).Length().ToString("f4") + "\r\n";

                    textBoxInformation.Text = str;
                }
                Draw();
            }
            */
            #endregion
        }

        #endregion マウスイベント

        #region Unit cell タブ関連
        private void checkBoxShowUnitCell_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxShowUnitCell.Enabled = checkBoxUnitCell.Checked;
            setUnitCellPlanes();
        }
        #endregion

        #region イメージ保存orコピー

        // イメージを保存する
        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = null;
            var name = (sender as ToolStripMenuItem).Name.ToLower();
            if (name.Contains("main"))
                bmp = glControlMain.GenerateBitmap();
            else if (name.Contains("axes"))
                bmp = glControlAxes.GenerateBitmap();
            else if (name.Contains("light"))
                bmp = glControlLight.GenerateBitmap();

            if (bmp != null)
            {
                if (name.Contains("save"))
                {
                    var dialog = new SaveFileDialog { Filter = "Picture File[*.png]|*.png;" };
                    if (dialog.ShowDialog() == DialogResult.OK)
                        bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                    Clipboard.SetDataObject(bmp, true, 10, 100);
            }
        }

        #endregion イメージ保存orコピー

        #region toolStripButton ライト、結晶軸、凡例、ブースト
        private void toolStripButtonCrystalAxes_CheckedChanged(object sender, EventArgs e) => glControlAxes.Visible = toolStripButtonCrystalAxes.Checked;

        private void toolStripButtonLightingBall_CheckedChanged(object sender, EventArgs e) => glControlLight.Visible = toolStripButtonLightDirection.Checked;

        private void toolStripButtonBoost_CheckedChanged(object sender, EventArgs e)
        => glControlMain.RenderingTransparency = toolStripButtonBoost.Checked ? GLControlAlpha.RenderingTransparencyModes.NotAlways : GLControlAlpha.RenderingTransparencyModes.Always;

        private void toolStripButtonLegend_CheckedChanged(object sender, EventArgs e) => SetLegend();

        #endregion

        #region 印刷関連

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*
            System.Drawing.Printing.PageSettings ps = printDocument1.PrinterSettings.DefaultPageSettings;
            //用紙サイズ取得 このサイズは1/100インチ
            float height = (ps.PaperSize.Height - ps.Margins.Top - ps.Margins.Bottom) / 100f;
            float width = (ps.PaperSize.Width - ps.Margins.Left - ps.Margins.Right) / 100f;

            if (printDocument1.PrinterSettings.DefaultPageSettings.Landscape)
            {//縦横を逆転
                float temp = width; width = height; height = temp;
            }
            //解像度300dpiのときのイメージサイズは
            Bitmap bmp = glAlpha.GenerateBitmap(glControlMain, (int)(width * 300), (int)(height * 300));

            bmp.SetResolution(300, 300);

            e.Graphics.PageUnit = GraphicsUnit.Inch;
            e.Graphics.DrawImage(bmp, new PointF(ps.Margins.Top / 100f, ps.Margins.Left / 100f));
            e.HasMorePages = false;
            */
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.PrinterSettings = pageSetupDialog1.PrinterSettings;
        }

        private void printPerviewToolStripMenuItem_Click(object sender, EventArgs e) =>
            // 印刷プレビューを表示
            printPreviewDialog1.ShowDialog();

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        #endregion 印刷関連

        #region Bounds(境界)　関連　イベント
        private void radioButtonUnitCell_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipEvent)
                return;
            tabControlBoundOption.SelectedIndex = radioButtonBoundUnitCell.Checked ? 0 : 1;
            SetGLObjects();
        }
        private void numericBoxCMax_ValueChanged(object sender, EventArgs e)
        {
            if (SkipEvent)
                return;
            SetGLObjects();
        }

        private void buttonSetCenterOrRange_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                SkipEvent = true;
                if (button.Name.Contains("Center"))
                    numericBoxACenter.Value = numericBoxBCenter.Value = numericBoxCCenter.Value = Convert.ToDouble(button.Tag as string);
                else
                    numericBoxARange.Value = numericBoxBRange.Value = numericBoxCRange.Value = Convert.ToDouble(button.Tag as string);
                SkipEvent = true;

                SetGLObjects();
            }
        }
        
        private void checkBoxShowBoundPlanes_CheckedChanged(object sender, EventArgs e)
        {
            SetGLObjects(null);
        }

        /// <summary>
        /// 境界面のコントロールに変化があったとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoundControl_BoundsChanged(object sender, EventArgs e)
        {
            if (SkipEvent)
                return;
            if (formMain.crystalControl.SkipEvent)//crystalControlが更新中の時の変更はキャンセル
                return;
            SetGLObjects(null);
        }

     

        #endregion

        #region LatticePlane 関連　イベント
        private void LatticePlaneControl_LatticePlaneChanged(object sender, EventArgs e)
        {
            if (SkipEvent)
                return;
            if (formMain.crystalControl.SkipEvent)//crystalControlが更新中の時の変更はキャンセル
                return;
            SetGLObjects(null);
        }

        private void numericBoxLatticePlaneOpacity_ValueChanged(object sender, EventArgs e)
        {
            if (SkipEvent)
                return;
            SetGLObjects(null);
        }

        #endregion

        #region Bond 関連イベント
        private void BondControl_BondsChanged(object sender, EventArgs e)
        {
            if (SkipEvent || formMain.crystalControl.SkipEvent)//crystalControlが更新中の時の変更はキャンセル
                return;
            SetGLObjects(null);
        }

        #endregion

        #region Atom コントロール関連イベント
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) => MoveAtomControl(tabControl.SelectedTab == tabPageAtom);

        private void MoveAtomControl(bool flag)
        {
            if (flag)
            {
                TopMost = true;
                atomControl.DebyeWallerTabVisible = atomControl.ScatteringFactorTabVisible = false;
                atomControl.AppearanceTabVisible = true;
                formMain.crystalControl.tabPageAtom.Controls.Add(labelMessage);
                tabPageAtom.Controls.Add(atomControl);
                TopMost = false;

            }
            else
            {
                atomControl.DebyeWallerTabVisible = atomControl.ScatteringFactorTabVisible = true;
                atomControl.AppearanceTabVisible = false;
                tabPageAtom.Controls.Add(labelMessage);
                formMain.crystalControl.tabPageAtom.Controls.Add(atomControl);
            }
        }

        private void AtomControl_ItemsChanged(object sender, EventArgs e)
        {
        }

        private void SetLegend()
        {
            glControlMain.SkipRendering = true;
            
            legendControls = new List<GLControlAlpha>();
            legendLabels = new List<Label>();
            legendPanels.Clear();
            flowLayoutPanelLegend.Controls.Clear();


            if (!toolStripButtonLegend.Checked)
            {
                glControlMain.SkipRendering = false;
                return;
            }
            

            var atoms = atomControl.GetAll();
            flowLayoutPanelLegend.SuspendLayout();

          

            for (int i = 0; i < atoms.Length; i++)
            {
                legendControls.Add(new GLControlAlpha
                {
                    AllowMouseRotation=false,
                    AllowMouseScaling=false,
                    AllowMouseTranslating=false,
                    Width = 60,
                    Height = 60,
                    BorderStyle = BorderStyle.Fixed3D,
                    DisablingOpenGL = false,
                    MaxHeight = 1,
                    MaxWidth = 1,
                    Name = "legend" + i.ToString(),
                    NodeCoefficient = 1,
                    ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
                    ProjWidth = 2.2D,
                    RenderingTransparency = GLControlAlpha.RenderingTransparencyModes.Never,
                    RotationMode = GLControlAlpha.RotationModes.Object,
                    TranslatingMode = GLControlAlpha.TranslatingModes.View,
                    LightPosition = glControlLight.LightPosition,
                    WorldMatrix = glControlLight.WorldMatrix,
                    ViewFrom = glControlLight.ViewFrom
                });

                legendLabels.Add(new Label
                {
                    Text = atoms[i].Label,
                    Font = Font,
                    AutoSize = true,
                    Margin = new Padding(3, 0, 0, 0),
                });

                legendPanels.Add(new FlowLayoutPanel
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    FlowDirection = FlowDirection.TopDown,
                    Margin = new Padding(1, 1, 1, 8),
                }) ;


                legendPanels[i].Controls.Add(legendControls[i]);
                legendPanels[i].Controls.Add(legendLabels[i]);
                flowLayoutPanelLegend.Controls.Add(legendPanels[i]);
            }
            flowLayoutPanelLegend.ResumeLayout();

            var maxRadius = atoms.Max(a => a.Radius);
            for (int i = 0; i < atoms.Length; i++)
            {
                legendControls[i].AddObjects(
                    new Sphere(new V3(0, 0, 0), atoms[i].Radius/maxRadius,
                    new Material(atoms[i].Argb, atoms[i].Transparency, atoms[i].Ambient, atoms[i].Diffusion, atoms[i].Specular, atoms[i].Shininess, atoms[i].Emission),
                    DrawingMode.Surfaces));
            }
            glControlMain.SkipRendering = false;


        }

        #endregion


    }
}
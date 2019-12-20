using Crystallography;
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
        #region フィールド

        public Crystal Crystal;

        public FormMain formMain;
        private FormAtom formAtom;

        private V3 shift;
        private Matrix3d axes;
        private List<(V4 prm, Color color)> bounds;

        #endregion フィールド

        public List<GLObject> GLObjects = new List<GLObject>();

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

        public readonly object lockObj = new object();

        #region 境界(Bounds)のFlowLayoutPanel関連

        private void checkBoxShowBoundPlanes_CheckedChanged(object sender, EventArgs e)
        {
            SetGLObjects(null);
        }

        private void AddBoundary_Click(object sender, EventArgs e)
        {
            skipSetCrystal = true;
            BoundsControl bc = new BoundsControl(Crystal);
            bc.Changed += Bounds_Changed;
            bc.ColorChanged += Bounds_Changed;
            bc.Delete += Bounds_Delete;
            flowLayoutPanelBounds.Controls.Add(bc);
            if (Crystal.Bounds == null)
                Crystal.Bounds = new List<Bound>();
            Crystal.Bounds.Add(bc.Bound);
            skipSetCrystal = false;
            SetGLObjects(null);
        }

        private void Bounds_Changed(object sender, EventArgs e)
        {
            var bc = (BoundsControl)sender;
            var index = flowLayoutPanelBounds.Controls.IndexOf(bc);
            Crystal.Bounds[index] = bc.Bound;
            SetGLObjects(null);
        }

        private void Bounds_Delete(object sender, EventArgs e)
        {
            var bc = (BoundsControl)sender;
            var index = flowLayoutPanelBounds.Controls.IndexOf(bc);
            Crystal.Bounds.RemoveAt(index);
            flowLayoutPanelBounds.Controls.Remove(bc);
            SetGLObjects(null);
        }

        /// <summary>
        /// 結晶そのものが変更されたときに呼び出されて、境界条件をパネルに表示する
        /// </summary>
        /// <param name="crystal"></param>
        public void initBoundsControl(Crystal crystal)
        {
            skipSetCrystal = true;

            flowLayoutPanelBounds.Controls.Clear();

            if (crystal.Bounds == null || crystal.Bounds.Count == 0)
            {
                int n = crystal.Symmetry.CrystalSystemNumber;
                if (n >= 0 && n <= 3)
                    crystal.Bounds = new List<Bound> {
                        new Bound(crystal, 1, 0, 0, true, 0.7, Bound.UnitEnum.D_spacing, Color.Green.ToArgb()),
                        new Bound(crystal, 0, 1, 0, true, 0.7, Bound.UnitEnum.D_spacing, Color.Green.ToArgb()),
                        new Bound(crystal, 0, 0, 1, true, 0.7, Bound.UnitEnum.D_spacing, Color.Green.ToArgb()),
                        };
                else if (n >= 4 && n <= 6)
                    crystal.Bounds = new List<Bound> {
                        new Bound(crystal, 1, 0, 0, true, 0.7, Bound.UnitEnum.D_spacing, Color.Green.ToArgb()),
                        new Bound(crystal, 0, 0, 1, true, 0.7, Bound.UnitEnum.D_spacing, Color.Green.ToArgb()),
                        };
                else
                    crystal.Bounds = new List<Bound> {
                        new Bound(crystal, 1, 0, 0, true, 0.75, Bound.UnitEnum.D_spacing, Color.Green.ToArgb()),
                        };
            }

            foreach (var bound in crystal.Bounds)
            {
                BoundsControl bc = new BoundsControl(Crystal);
                bc.Bound = bound;
                bc.Enabled = true;
                bc.Changed += Bounds_Changed;
                bc.ColorChanged += Bounds_Changed;
                bc.Delete += Bounds_Delete;
                flowLayoutPanelBounds.Controls.Add(bc);
            }
            skipSetCrystal = false;
        }

        #endregion 境界(Bounds)のFlowLayoutPanel関連

        #region 格子面の(Lattice Planes)のFlowLayoutPanel関連

        private void AddLatticePlane_Click(object sender, EventArgs e)
        {
            skipSetCrystal = true;
            BoundsControl bc = new BoundsControl(Crystal)
            {
                FullOption = false,
                Distance = 0,
                Equivalency = false,
                Color = Color.Purple
            };
            bc.numericBoxDistance.Maximum = 0.5;
            bc.numericBoxDistance.Minimum = -0.5;
            bc.Changed += LatticePlanes_Changed;
            bc.ColorChanged += LatticePlanes_Changed;
            bc.Delete += LatticePlanes_Delete;
            flowLayoutPanelLatticePlanes.Controls.Add(bc);
            if (Crystal.LatticePlanes == null)
                Crystal.LatticePlanes = new List<Bound>();
            Crystal.LatticePlanes.Add(bc.Bound);
            skipSetCrystal = false;
            SetGLObjects(null);
        }

        private void LatticePlanes_Changed(object sender, EventArgs e)
        {
            var bc = (BoundsControl)sender;
            var index = flowLayoutPanelLatticePlanes.Controls.IndexOf(bc);
            Crystal.LatticePlanes[index] = bc.Bound;
            SetLatticePlanes();
        }

        private void LatticePlanes_Delete(object sender, EventArgs e)
        {
            var bc = (BoundsControl)sender;
            var index = flowLayoutPanelLatticePlanes.Controls.IndexOf(bc);
            Crystal.LatticePlanes.RemoveAt(index);
            flowLayoutPanelLatticePlanes.Controls.Remove(bc);
            SetLatticePlanes();
        }

        private void initLatticePlanesControl(Crystal crystal)
        {
            skipSetCrystal = true;
            flowLayoutPanelLatticePlanes.Controls.Clear();
            if (crystal.LatticePlanes != null && crystal.LatticePlanes.Count != 0)
            {
                foreach (var plane in crystal.LatticePlanes)
                {
                    BoundsControl bc = new BoundsControl(Crystal)
                    {
                        Bound = plane,
                        FullOption = false,
                        Equivalency = false
                    };
                    bc.numericBoxDistance.Maximum = 0.5;
                    bc.numericBoxDistance.Minimum = -0.5;
                    bc.Changed += LatticePlanes_Changed;
                    bc.ColorChanged += LatticePlanes_Changed;
                    bc.Delete += LatticePlanes_Delete;
                    flowLayoutPanelLatticePlanes.Controls.Add(bc);
                }
            }
            skipSetCrystal = false;
        }

        #endregion 格子面の(Lattice Planes)のFlowLayoutPanel関連

        private void initAxesMatrix()
        {
            axes.Row0 = new V3(Crystal.A_Axis.X, Crystal.B_Axis.X, Crystal.C_Axis.X);
            axes.Row1 = new V3(Crystal.A_Axis.Y, Crystal.B_Axis.Y, Crystal.C_Axis.Y);
            axes.Row2 = new V3(Crystal.A_Axis.Z, Crystal.B_Axis.Z, Crystal.C_Axis.Z);
            int n = Crystal.Symmetry.CrystalSystemNumber;
            if (n >= 5 && n <= 6)
            {
                shift = new V3(0, 0, 0);
            }
            else
            {
                shift = (axes.Column0 + axes.Column1 + axes.Column2) / 2;
            }
        }

        private void initBounds()
        {
            bounds = new List<(V4 prm, Color color)>();
            foreach (var bc in flowLayoutPanelBounds.Controls.Cast<BoundsControl>().Where(bc => bc.Enabled && bc.Bound.D != 0 && !double.IsInfinity(bc.Bound.D)))
                bounds.AddRange(bc.Bound.PlaneParams.Select(p => (new V4(p[0], p[1], p[2], p[3]), bc.Bound.Color)));

            //描画範囲が閉じているかどうかを判定. 閉じていない場合は、強制的に単位格子を境界とする
            if (!Geometriy.Enclosed(bounds.Select(b => b.prm.ToArray()).ToArray()))
                bounds = new List<(V4 prms, Color color)>() {
                    (new V4(axes.Column0.Normalized(),axes.Column0.Length/2) , Color.Gray),
                    (new V4(-axes.Column0.Normalized(),axes.Column0.Length/2), Color.Gray),
                    (new V4(axes.Column1.Normalized(),axes.Column1.Length/2), Color.Gray),
                    (new V4(-axes.Column1.Normalized(),axes.Column1.Length/2), Color.Gray),
                    (new V4(axes.Column2.Normalized(),axes.Column2.Length/2), Color.Gray),
                    (new V4(-axes.Column2.Normalized(),axes.Column2.Length/2), Color.Gray),
                };
        }

        /// <summary>
        /// 境界面オブジェクトを生成
        /// </summary>
        private void setBoundPlanes()
        {
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

        private readonly List<V3> dirs = new List<V3> { new V3(1, 0, 0), new V3(-1, 0, 0), new V3(0, 1, 0), new V3(0, -1, 0), new V3(0, 0, 1), new V3(0, 0, -1) };
        private readonly List<V3> vrts = new List<V3> { new V3(.5, .5, .5), new V3(-.5, .5, .5), new V3(.5, -.5, .5), new V3(.5, .5, -.5), new V3(.5, -.5, -.5), new V3(-.5, .5, -.5), new V3(-.5, -.5, .5), new V3(-.5, -.5, -.5) };

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

            // for (int i = 0; i < Crystal.Atoms.Length; i++)
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
                                lock (lockObj)
                                    GLObjects.Add(new Sphere(new V3(pos), atoms.Radius * 0.1, mat, DrawingMode.Surfaces)
                                    {
                                        Rendered = min > -0.0000001,
                                        Tag = new atomID(atoms, min > -0.0000001, i)
                                    });
                            }
                        }
                }
            }
            );
        }

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

        /// <summary>
        /// GLObjectsを転送する
        /// </summary>
        private void transferGLObjects()
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            glControlMain.DeleteAllObjects();
            glControlMain.AddObjects(GLObjects);
            toolStripLabelStatusInitialization.Text += " and sent to OpenGL shader (" + sw.ElapsedMilliseconds + " ms.)    ";
        }

        private bool skipSetCrystal = false;

        /// <summary>
        /// 結晶構造をセッティング
        /// </summary>
        /// <param name="_crystal">nullでない場合は、Bounds と Lattice planes に関するコントロールがリセットされる</param>
        public void SetGLObjects(Crystal _crystal = null)
        {
            if (skipSetCrystal) return;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Crystal = _crystal == null ? formMain.Crystal : _crystal;

            atomCoordinateTable1.Crystal = Crystal;

            GLObjects = new List<GLObject>(); //GLObjectsを初期化

            initAxesMatrix(); //結晶軸マトリックスを初期化

            setUnitCellPlanes();//単位格子面の描画

            if (_crystal != null)//
                initBoundsControl(Crystal);//flowLayoutPanelBoundsをセット

            initBounds();//境界条件を初期化;

            if (_crystal != null)
                initLatticePlanesControl(Crystal);//flowLayoutPanelLatticePlanesをセット

            SetLatticePlanes();//格子面オブジェクトを生成

            setBoundPlanes();//境界面オブジェクトを生成

            setAtoms();//原子オブジェクトを生成

            setBondsAndPolyhera();//結合と多面体オブジェクトを生成

            removeObjects();//余計な原子を削除

            //glControlLegend.Size = new Size(glControlLegend.Size.Width, crystal.Atoms.Length * 20);
            // SetLatticePlaneProperty();

            toolStripLabelStatusInitialization.Text = GLObjects.Count + " objects were created (" + sw.ElapsedMilliseconds + " ms)";

            transferGLObjects();

            //結晶軸を表示するGLControl
            setAxesControl();

            Draw();
        }

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
            var cellPlaneMat = new Material(pictureBoxCellPlaneColor.BackColor.ToArgb(), (float)numericUpDownCellPlaneAlpha.Value, 0.2, 0.8, 0.8, 50, 0.2);
            var cellPlane = new Polyhedron(cellVertices, cellPlaneMat, DrawingMode.Surfaces);
            cellPlane.Tag = new cellID();
            var cellEdgeMat = new Material(pictureBoxCellEdgeColor.BackColor.ToArgb(), 1, 0.2, 0.8, 0.8, 50, 0.2);
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

            var latticePlanes = new List<(V3 norm, double d, double t, Color color)>();
            foreach (var bc in flowLayoutPanelLatticePlanes.Controls.Cast<BoundsControl>().Where(bc => bc.Enabled))
                latticePlanes.AddRange(bc.Bound.PlaneParams.Select(p => (new V3(p[0], p[1], p[2]), bc.Bound.D, bc.Distance, bc.Bound.Color)));

            var boundArray = bounds.Select(b => new[] { b.prm[0], b.prm[1], b.prm[2], b.prm[3] * 1.2 }).ToArray();

            foreach (var (norm, d, t, color) in latticePlanes)
            {
                var mat = new Material(color.ToArgb(), numericBoxLatticePlaneOpacity.Value, 0.2, 0.8, 0.8, 50, 0.2);
                int n = 0;
                var flag = true;
                var prm = new[] { norm.X, norm.Y, norm.Z, 0 };
                while (flag)
                {
                    var verticesList = new List<double[][]>();
                    for (int i = 0; i < (n == 0 ? 1 : 2); i++)
                        verticesList.Add(Geometriy.GetClippedPolygon(new[] { norm.X, norm.Y, norm.Z, ((i == 0 ? n : -n) + t) * d }, boundArray));

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

        #region コンストラクタ

        public FormStructureViewer()
        {
            InitializeComponent();
        }

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

            numericBoxBoundPlanesOpacity.ShowUpDown = true;
            numericBoxLatticePlaneOpacity.ShowUpDown = true;
        }

        #endregion コンストラクタ

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
            //glControlMain.Render();
            glControlAxes.WorldMatrixEx = world;
            toolStripLabelStatusRendering.Text = "Rendering time: " + sw.ElapsedMilliseconds + " ms.    ";
        }

        //凡例を表示する
        public void DrawLegend()
        {/*

            if (crystal == null || crystal.Atoms.Length <= 0) return;
            if (contextLegend == null) return;
            contextLegend.MakeCurrent();
            InitGL();

            double maxR = 0;
            for (int i = 0; i < crystal.Atoms.Length; i++)
                maxR = Math.Max(crystal.Atoms[i].Radius, maxR);

            SetProjectionLegend(maxR * 2.2 * crystal.Atoms.Length);

            // 描画前の設定など
            gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);//バッファのクリア
            gl.MatrixMode(MatrixMode.Modelview);
            gl.LoadIdentity();//モデルビュー変換行列のリセット
            gl.Enable(EnableCap.Normalize);//法線ベクトルを規格化

            gl.Enable(EnableCap.DepthTest);
            gl.DepthFunc(DepthFunction.Less);

            //カメラの位置と向きを適用
            glu.LookAt(0, 0, CamPosZ, 0, 0, 0, 0, 1, 0);

            //ライティングの設定を適用
            gl.PushMatrix();
            gl.MultMatrix(GetNormarize(matrixLight));
            glp.Lighting = true;
            gl.Enable(EnableCap.Lighting);
            gl.Light(LightName.Light0, LightParameter.Position, new float[] { 0f, 0f, 1f, 0f });
            gl.Enable(EnableCap.Light0);
            gl.PopMatrix();

            //球を描く
           IntPtr temp = glu.NewQuadric();

           for (int i = 0; i < crystal.Atoms.Length; i++)
            {
                gl.PushMatrix();

               // if (crystal.Atoms.Length == 1)
               //     gl.Translate(maxR * 0.55, maxR * 1.1, 0);
               // else
                    gl.Translate(maxR * 1.1, maxR * ((crystal.Atoms.Length - i - 1) + 0.5) * 2.2, 0);

                new Material("", Color.FromArgb(crystal.Atoms[i].Argb), crystal.Atoms[i].Transparency, crystal.Atoms[i].Ambient,
                       crystal.Atoms[i].Diffusion, crystal.Atoms[i].Specular, crystal.Atoms[i].Emission, crystal.Atoms[i].Shininess).ApplyMaterialParams();
                gl.Begin(BeginMode.Polygon);
                glu.QuadricNormal(temp, QuadricNormal.Smooth);//法線の設定
                glu.QuadricDrawStyle(temp,QuadricDrawStyle.Fill);//オブジェクトの描画タイプを設定（省略可）

                if (crystal.Atoms.Length == 1)
                    glu.Sphere(temp, crystal.Atoms[i].Radius * 0.5, 40, 40);//球を描画
                else
                    glu.Sphere(temp, crystal.Atoms[i].Radius, 40, 40);//球を描画

                gl.End();
                gl.PopMatrix();
            }

            //字を書く
            try
            {
                gl.Disable(EnableCap.Lighting);
                for (int i = 0; i < crystal.Atoms.Length; i++)
                {
                    gl.PushMatrix();
                 //   if (crystal.Atoms.Length == 1)
                 //   {
                 //       gl.Translate(0, maxR * 1.1, 0);
                 //       glStringLegend.DrawString(crystal.Atoms[i].Label, colorControlLabel.Color, maxR * 1.65, 0);
                 //   }
                 //   else
                    {
                        gl.Translate(maxR * 1.1, maxR * ((crystal.Atoms.Length - i - 1) +0.3) * 2.2, 0);
                        glStringLegend.DrawString(crystal.Atoms[i].Label, colorControlLabel.Color, maxR * 3.3, 0);
                    }
                    gl.PopMatrix();
                }
                gl.Enable(EnableCap.Lighting);
            }
            catch { }

            gl.Finish();
            contextLegend.SwapBuffers();
            */
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
        }

        #endregion マウスイベント

        private void checkBoxShowUnitCell_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxShowUnitCell.Enabled = checkBoxUnitCell.Checked;
            setUnitCellPlanes();
        }

        private void pictureBoxColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog
            {
                Color = ((PictureBox)sender).BackColor,
                AllowFullOpen = true,
                AnyColor = true,
                SolidColorOnly = false,
                ShowHelp = true
            };
            colorDialog.ShowDialog();
            ((PictureBox)sender).BackColor = colorDialog.Color;
        }

        private void FormStructureViewer_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)//現れたときメインウィンドウの結晶を表示する
                if (formMain.crystalControl.Crystal != null)
                    SetGLObjects(formMain.crystalControl.Crystal);
        }

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
                    var dialog = new SaveFileDialog();
                    dialog.Filter = "Picture File[*.png]|*.png;";
                    if (dialog.ShowDialog() == DialogResult.OK)
                        bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                    Clipboard.SetDataObject(bmp, true, 10, 100);
            }
        }

        #endregion イメージ保存orコピー

        private void tabControl1_Click(object sender, EventArgs e)
        {
            tabControl.BringToFront();
        }

        private void checkBoxShowCrystalAxes_CheckedChanged(object sender, EventArgs e)
        {
            glControlAxes.Visible = toolStripButtonCrystalAxes.Checked;
        }

        private void checkBoxShowLightingBall_CheckedChanged(object sender, EventArgs e)
        {
            glControlLight.Visible = toolStripButtonLightDirection.Checked;
        }

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

        private void printPerviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 印刷プレビューを表示
            printPreviewDialog1.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        #endregion 印刷関連

        private void toolStripButtonBoost_CheckedChanged(object sender, EventArgs e)
        {
            glControlMain.RenderingTransparency = toolStripButtonBoost.Checked ? GLControlAlpha.RenderingTransparencyModes.NotAlways : GLControlAlpha.RenderingTransparencyModes.Always;
        }

        private void FormStructureViewer_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
        }

        private void FormStructureViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.C)
                Clipboard.SetDataObject(glControlMain.GenerateBitmap());
        }
    }
}
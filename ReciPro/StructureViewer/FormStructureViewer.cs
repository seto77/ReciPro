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
using System.Threading;
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

        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

        private V3 shift;
        private Matrix3d axes;
        private List<(V4 prm, Color color)> bounds;

        public List<GLObject> GLObjects = new List<GLObject>();

        public readonly object lockObj = new object();

        private readonly List<V3> dirs = new List<V3> { new V3(1, 0, 0), new V3(-1, 0, 0), new V3(0, 1, 0), new V3(0, -1, 0), new V3(0, 0, 1), new V3(0, 0, -1) };
        private readonly List<V3> vrts = new List<V3> { new V3(.5, .5, .5), new V3(-.5, .5, .5), new V3(.5, -.5, .5), new V3(.5, .5, -.5), new V3(.5, -.5, -.5), new V3(-.5, .5, -.5), new V3(-.5, -.5, .5), new V3(-.5, -.5, -.5) };

        private bool skipSetCrystal = false;

        public bool SkipEvent { get; set; } = false;

        private BoundControl boundControl;
        private LatticePlaneControl latticePlaneControl;
        private AtomControl atomControl;
        private BondInputControl bondControl;

        private List<GLControlAlpha> legendControls = new List<GLControlAlpha>();
        private List<Label> legendLabels = new List<Label>();
        private List<FlowLayoutPanel> legendPanels = new List<FlowLayoutPanel>();

        private Stopwatch sw = new Stopwatch();

        private ParallelQuery<(int Index, V3 Pos, Material Mat, double Radius)> atomsP;

        private Crystallography.OpenGL.GLControlAlpha glControlLight;
        private Crystallography.OpenGL.GLControlAlpha glControlMain;
        private Crystallography.OpenGL.GLControlAlpha glControlAxes;

        (double ambient, double diffuse, double specular, double specularPow, double emission) defaultMat = (0.2, 0.5, 0.6, 4, 0.4);

        #endregion

        #region ローカルクラス
        private class atomID
        {
            public bool IsInside;
            //public Atoms Atoms;
            public int Index;

            public atomID(int index, bool isInside)
            {
                IsInside = isInside;
                Index = index;
                //N = n;
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
            if (DesignMode) return;

            formAtom = new FormAtom();
            formAtom.formStructureViewer = this;
            AddOwnedForm(formAtom);

            #region デザイナが壊れないようにここでGLコントロールを追加
            // glControlAxes
            glControlAxes = new GLControlAlpha
            {
                AllowMouseRotation = false,
                AllowMouseScaling = false,
                AllowMouseTranslating = false,
                BorderStyle = BorderStyle.Fixed3D,
                DisablingOpenGL = false,
                MaxHeight = 1,
                MaxWidth = 1,
                Name = "glControlAxes",
                NodeCoefficient = 1,
                ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
                ProjWidth = 4D,
                FragShader = GLControlAlpha.FragShaders.ZSORT,
                RotationMode = GLControlAlpha.RotationModes.Object,
                TranslatingMode = GLControlAlpha.TranslatingModes.View,
                Location = new Point(0, 0),
                Size = new Size(numericBoxAxesSize.ValueInteger, numericBoxAxesSize.ValueInteger),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            glControlAxes.MouseMove += glControlAxes_MouseMove;

            // glControlLight
            glControlLight = new GLControlAlpha
            {
                AllowMouseRotation = false,
                AllowMouseScaling = false,
                AllowMouseTranslating = false,
                BorderStyle = BorderStyle.Fixed3D,
                DisablingOpenGL = false,
                MaxHeight = 1,
                MaxWidth = 1,
                Name = "glControlLight",
                NodeCoefficient = 1,
                ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
                ProjWidth = 4D,
                FragShader = GLControlAlpha.FragShaders.ZSORT,
                RotationMode = GLControlAlpha.RotationModes.Object,
                TranslatingMode = GLControlAlpha.TranslatingModes.View,
                Location = new Point(0, 0),
                Size = new Size(numericBoxLightSize.ValueInteger, numericBoxLightSize.ValueInteger),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            glControlLight.MouseMove += glControlLight_MouseMove;


            // glControlMain
            glControlMain = new GLControlAlpha
            {
                AllowMouseRotation = false,
                AllowMouseScaling = true,
                AllowMouseTranslating = true,
                BorderStyle = BorderStyle.Fixed3D,
                DisablingOpenGL = false,
                MaxHeight = 1440,
                MaxWidth = 2560,
                Name = "glControlMain",
                NodeCoefficient = 4,
                ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
                ProjWidth = 4D,
                FragShader = GLControlAlpha.FragShaders.ZSORT,
                RotationMode = GLControlAlpha.RotationModes.Object,
                TranslatingMode = GLControlAlpha.TranslatingModes.View,
                Dock = DockStyle.Fill
            };
            glControlMain.MouseDown += panelMain_MouseDown;
            glControlMain.MouseMove += glControlMain_MouseMove;

            // splitContainer1.Panel1にglControlを追加
            splitContainer1.Panel1.Controls.Add(glControlAxes);
            splitContainer1.Panel1.Controls.Add(glControlLight);
            splitContainer1.Panel1.Controls.Add(glControlMain);
            glControlAxes.Location = new Point(0, glControlMain.Height - glControlAxes.Height);

            #endregion

            #region GLコントロールの初期化
            glControlLight.AddObjects(new Sphere(new V3(0, 0, 0), 1.0, new Material(C4.Gray, defaultMat), DrawingMode.Surfaces));
            glControlMain.LightPosition = glControlLight.LightPosition = glControlAxes.LightPosition = new V3(100, 100, 100);
            glControlMain.ViewFrom = glControlLight.ViewFrom = glControlAxes.ViewFrom = new V3(0, 0, 50);
            glControlLight.ProjWidth = glControlAxes.ProjWidth = 2.2;
            glControlMain.ProjWidth = 3f;
            #endregion

            #region ビデオカードの設定
            foreach (var info in glControlMain.GraphicsInfo)
            {
                labelGraphicsCard.Text += info.Product + "  ";
                labelGraphicsDriver.Text += info.Version + "  ";
            }
            labelOpenGLversion.Text += glControlMain.VersionStr;

            if (glControlMain.GraphicsInfo.Select(g => g.Product.ToLower()).Any(p => p.Contains("nvidia") || p.Contains("amd")))
                 comboBoxRenderignQuality.SelectedIndex = 1;
            else
                comboBoxRenderignQuality.SelectedIndex = 0;

            #endregion
        
            #region 各種ユーザーコントロールの追加
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

            #endregion


            #region コントロールの追加設定

            flowLayoutPanelLegend.AutoSize = true;
            flowLayoutPanelLegend.SendToBack();

            tabControlBoundOption.ItemSize = new Size(0, 1);

            comboBoxProjectionMode.SelectedIndex = 0;
            comboBoxTransparency.SelectedIndex = 0;

            checkBoxDepthCueing_CheckedChanged(new object(), new EventArgs());

            #endregion
        }



        #endregion コンストラクタ

        #region 結晶軸行列を設定
            private void initAxesMatrix()
        {

            axes.Row0 = new V3(Crystal.A_Axis.X, Crystal.B_Axis.X, Crystal.C_Axis.X);
            axes.Row1 = new V3(Crystal.A_Axis.Y, Crystal.B_Axis.Y, Crystal.C_Axis.Y);
            axes.Row2 = new V3(Crystal.A_Axis.Z, Crystal.B_Axis.Z, Crystal.C_Axis.Z);
            int n = Crystal.Symmetry.CrystalSystemNumber;
            if (n == 5 || n == 6) //trigonalとhexagonalの時
            {
                shift = new V3(0, 0, 0);
                numericBoxCellTransrationA.Value = numericBoxCellTransrationB.Value = numericBoxCellTransrationC.Value = 0.5;
            }
            else
            {
                shift = (axes.Column0 + axes.Column1 + axes.Column2) / 2;
                numericBoxCellTransrationA.Value = numericBoxCellTransrationB.Value = numericBoxCellTransrationC.Value = 0.0;
            }
        }
        #endregion

        #region Bounds (境界面)を初期化
        /// <summary>
        /// 境界面を初期化
        /// </summary>
        private void initBounds()
        {
            

            sw.Restart();

            bounds = new List<(V4 prm, Color color)>();
            foreach (var bc in boundControl.GetAll().Where(b => b.Enabled && b.PlaneParams != null && b.Index != (0, 0, 0)))
                foreach (var (X, Y, Z, D) in bc.PlaneParams)
                    bounds.Add((new V4(X, Y, Z, D), bc.Color));

            if (radioButtonBoundUnitCell.Checked || !Geometriy.Enclosed(bounds.Select(b => b.prm.ToArray()).ToArray()))
            {//境界条件としてUnit cellが選択されているか、Planeが選択されているが描画範囲が閉じていない場合 、単位格子を境界とする

                var inv = Matrix3d.Invert(axes);


                bounds = new List<(V4 prms, Color color)>()
                {
                    (new V4(inv.Row0.Normalized(),1/inv.Row0.Length * (numericBoxACenter.Value + numericBoxARange.Value)) , Color.Gray),
                    (new V4(-inv.Row0.Normalized(),1/inv.Row0.Length *  -(numericBoxACenter.Value - numericBoxARange.Value)), Color.Gray),
                    (new V4(inv.Row1.Normalized(),1/inv.Row1.Length *  (numericBoxBCenter.Value + numericBoxBRange.Value)), Color.Gray),
                    (new V4(-inv.Row1.Normalized(),1/inv.Row1.Length * -(numericBoxBCenter.Value - numericBoxBRange.Value)), Color.Gray),
                    (new V4(inv.Row2.Normalized(),1/inv.Row2.Length * (numericBoxCCenter.Value + numericBoxCRange.Value)), Color.Gray),
                    (new V4(-inv.Row2.Normalized(),1/inv.Row2.Length * -(numericBoxCCenter.Value - numericBoxCRange.Value)), Color.Gray),
                };
            }

            textBoxInformation.AppendText("Initialization of bounds: " + sw.ElapsedMilliseconds + "ms.\r\n");
        }

        #endregion

        #region Bounds (境界面) の GLObjects の生成

        /// <summary>
        /// 境界面オブジェクトを生成
        /// </summary>
        private void setBoundPlanes()
        {
            sw.Restart();
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

            textBoxInformation.AppendText("Generation of bound planes: " + sw.ElapsedMilliseconds + "ms.\r\n");
        }

        #endregion

        #region 原子 GLObjectsの生成

        /// <summary>
        /// 原子オブジェクトを生成
        /// </summary>
        public void setAtoms()
        {
            sw.Restart();

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
            atomsP.ForAll(o =>
            {
                foreach (var pos in cells.Select(t => axes.Mult(t + o.Pos) - shift).ToList())
                {
                    var min = bounds.Min(b => V4.Dot(new V4(pos, 1), b.prm));
                    if (min > threshold)
                    {
                        var sphere = new Sphere(pos, o.Radius, o.Mat, DrawingMode.Surfaces);
                        sphere.Rendered = min > -0.0000001;
                        sphere.Tag = new atomID(o.Index, sphere.Rendered);
                        lock (lockObj)
                            GLObjects.Add(sphere);
                    }
                }
            });

            textBoxInformation.AppendText("Generation of aoms: " + sw.ElapsedMilliseconds + "ms.\r\n");
        }
        public void setAtomsP()
        {
            var list = new List<(int Index, V3 Pos, Material Mat, double Radius)>();
            //位置が全く同じ原子が存在する場合は、最もOccが大きいものを選ぶ。それが複数ある場合は、indexが若い方を選ぶ
            for (int i = 0; i < Crystal.Atoms.Length; i++)
            {
                var a = Crystal.Atoms[i];
                if (Crystal.Atoms.Where((b, j) => i != j && a.X == b.X && a.Y == b.Y && a.Z == b.Z && (a.Occ < b.Occ || (a.Occ <= b.Occ && i > j))).Count() == 0)
                {
                    var mat = new Material(a.Argb, a.Transparency, a.Ambient, a.Diffusion, a.Specular, a.Shininess, a.Emission);
                    var radius = a.Radius * 0.1;
                    foreach (var pos in a.Atom.Select(v => new V3(v.X, v.Y, v.Z)))
                        list.Add((i, pos, mat, radius));
                }
               
            }
            atomsP = list.AsParallel();
        }

        #endregion

        #region Bonds(結合)とPolyhedra (配位多面体)オブジェクトの生成

        /// <summary>
        /// 結合(Bonds)と配位多面体(Polyhera)オブジェクトを生成
        /// </summary>
        private void setBondsAndPolyhera()
        {
            sw.Restart();
            //まず、頂点原子の辞書を作る

            var bonds = bondControl.GetAll().Where(b => b.Enabled).ToList();
            var dic = new Dictionary<string, (int Index, V3 Origin, double Radius, bool IsInside, Material BondMat, Material PolyMat, int SerialNumber)[]>();
            bonds.ForEach(bond =>
            {
                foreach (var element in new[] { bond.Element1, bond.Element2 })
                {
                    if (!dic.ContainsKey(element))
                        dic.Add(element, GLObjects.Select((Obj, Index) => (Obj, Index))
                       .Where(e => e.Obj.Tag is atomID id && Crystal.Atoms[id.Index].ElementName == element).Select(e =>
                       {
                           var s = e.Obj as Sphere;
                           var BondMat = new Material(new C4(s.Material.Color.R, s.Material.Color.G, s.Material.Color.B, bond.BondTransParency), defaultMat);
                           var PolyMat = new Material(new C4(s.Material.Color.R, s.Material.Color.G, s.Material.Color.B, bond.PolyhedronTransParency), defaultMat);
                           return (e.Index, s.Origin, s.Radius, (s.Tag as atomID).IsInside, BondMat, PolyMat, s.SerialNumber);
                       }).ToArray());
                }
            });

            //bondsとpolyhedraを追加
            bonds.ForEach(bond =>
            {
                double min2 = bond.MinLength * bond.MinLength * 0.01, max2 = bond.MaxLength * bond.MaxLength * 0.01;
                double radius = bond.Radius * 0.1;

                var polyhedronMode = bond.ShowEdges ? DrawingMode.SurfacesAndEdges : DrawingMode.Surfaces;

                Parallel.ForEach(dic[bond.Element1].Where(e => e.IsInside), c =>
                {
                    var vertices = dic[bond.Element2].Where(v => (v.Origin - c.Origin).LengthSquared < max2 && (v.Origin - c.Origin).LengthSquared > min2).ToList();

                    if (vertices.Count > 0)
                    {
                        foreach (var v in vertices) //Bond
                        {
                            var vec = v.Origin - c.Origin;//中心間を結ぶベクトル
                            var length = vec.Length;//中心間を結ぶベクトルの長さ
                            var m = c.Origin + (length - c.Radius - v.Radius) * vec / length;//中間地点

                            var cylinder1 = new Cylinder(c.Origin, m - c.Origin, radius, c.BondMat, DrawingMode.Surfaces)
                            { Tag = new bondID(c.SerialNumber, v.SerialNumber), ShowClippedSection = false, Rendered = bond.ShowBond };

                            var cylinder2 = new Cylinder(m, v.Origin - m, radius, v.BondMat, DrawingMode.Surfaces)
                            { Tag = new bondID(c.SerialNumber, v.SerialNumber), ShowClippedSection = false, Rendered = bond.ShowBond };

                            lock (lockObj)
                            {
                                GLObjects.Add(cylinder1);
                                GLObjects.Add(cylinder2);
                                GLObjects[v.Index].Rendered = true;
                            }
                        }
                        if (vertices.Count == 3)
                        {
                            var polygon = new Polygon(vertices.Select(v => v.Origin).ToArray(), c.PolyMat, polyhedronMode)
                            { Rendered = bond.ShowPolyhedron };
                            lock (lockObj)
                                GLObjects.Add(polygon);
                        }
                        else if (vertices.Count > 3)
                        {
                            var polyhedron = new Polyhedron(vertices.Select(v => v.Origin).ToArray(), c.PolyMat, polyhedronMode)
                            { Rendered = bond.ShowPolyhedron, ShowClippedSection = false };

                            lock (lockObj)
                                GLObjects.AddRange(polyhedron.ToPolygons(glControlMain.FragShader == GLControlAlpha.FragShaders.ZSORT ? 0 : 0));
                            //order=2で、12個くらいに分割 => 計算時間がかかりすぎるので、取りあえずゼロに
                        }
                    }
                });
            });
            textBoxInformation.AppendText("Generation of bonds & polyhedra: " + sw.ElapsedMilliseconds + "ms.\r\n");
        }

        #endregion

        #region 余分な原子を削除
        /// <summary>
        /// 余分な原子を削除する
        /// </summary>
        private void removeObjects()
        {
            sw.Restart();
            //ボンドを構成する原子だが、描画範囲外のため孤立してしまった原子を削除

            //ボンドを構成するvertex側の原子のシリアル番号を取得
            var vertexSerials = GLObjects
                .Where(obj => obj is Cylinder)
                .Select(obj => (obj.Tag as bondID).SerialNumber2)
                .Distinct().ToList();

            //範囲外であり、なおかつ、上のシリアル番号に含まれない原子を取得
            var removeList = GLObjects
                .Where(obj => obj.Tag is atomID id && !id.IsInside && !vertexSerials.Contains(obj.SerialNumber))
                .ToList();

            foreach (var obj in removeList)
                GLObjects.Remove(obj);

            textBoxInformation.AppendText("Remove tentative atoms: " + sw.ElapsedMilliseconds + "ms.\r\n");
        }
        #endregion

        #region GLObjectsをシェーダに転送

        /// <summary>
        /// GLObjectsを転送する
        /// </summary>
        private void transferGLObjects()
        {
            sw.Restart();
            glControlMain.DeleteAllObjects();
            glControlMain.AddObjects(GLObjects);
            toolStripLabelStatusInitialization.Text += " and sent to OpenGL (" + sw.ElapsedMilliseconds + " ms.)    ";
            textBoxInformation.AppendText("Trasfer: " + sw.ElapsedMilliseconds + "ms.\r\n");

        } 
        #endregion

        #region 単位格子面オブジェクトを生成
        /// <summary>
        /// 単位格子面オブジェクトを生成
        /// </summary>
        private void setUnitCellPlanes()
        {
            sw.Restart();

            while (GLObjects.Count(obj => obj.Tag is cellID) != 0)
            {
                glControlMain.DeleteObjects(GLObjects.First(obj => obj.Tag is cellID));
                GLObjects.Remove(GLObjects.First(obj => obj.Tag is cellID));
            }

            var cellVertices = new[] { new V3(0), axes.Column0, axes.Column1, axes.Column2, axes.Column0 + axes.Column1, axes.Column1 + axes.Column2, axes.Column2 + axes.Column0, axes.Column0 + axes.Column1 + axes.Column2 };
            var translation = axes.Mult(new V3(numericBoxCellTransrationA.Value, numericBoxCellTransrationB.Value, numericBoxCellTransrationC.Value)) + shift;
            cellVertices = cellVertices.Select(v => v - translation).ToArray();
            
            var cellPlaneMat = new Material(colorControlCellPlane.Argb, numericBoxCellPlaneAlpha.Value, defaultMat);
            var cellPlane = new Polyhedron(cellVertices, cellPlaneMat, DrawingMode.Surfaces);
            cellPlane.Tag = new cellID();

            var cellEdgeMat = new Material(colorControlCellEdge.Argb, 1.0, defaultMat);
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

            //ZSortの時は、order=4で256分割
            var planes = cellPlane.ToPolygons(glControlMain.FragShader == GLControlAlpha.FragShaders.ZSORT ? 4 : 0);
            GLObjects.AddRange(planes);
            glControlMain.AddObjects(planes);

            var edges = cellEdge.ToPolygons();
            GLObjects.AddRange(edges);
            glControlMain.AddObjects(edges);

            textBoxInformation.AppendText("Generation of cell planes: " + sw.ElapsedMilliseconds + "ms.\r\n");

            Draw();


            //テストコード
          //  new Polygon(new[] { new V3 (0,0,0), new V3(1, 0, 0), new V3(0, 1, 0) }, cellEdgeMat, DrawingMode.Edges).Decompose();
        }
        #endregion

        #region 結晶格子面 GLObjectsの生成

        /// <summary>
        /// 格子面オブジェクトを生成
        /// </summary>
        public void SetLatticePlanes()
        {
            sw.Restart();

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
                var mat = new Material(color.ToArgb(), numericBoxLatticePlaneOpacity.Value, defaultMat);
                int n = 0;
                var flag = true;
                var prm = new[] { prms.X, prms.Y, prms.Z, 0 };
                while (flag)
                {
                    var verticesList = new List<double[][]>();
                    for (int i = 0; i < (n == 0 ? 1 : 2); i++)
                        verticesList.Add(Geometriy.GetClippedPolygon(new[] { prms.X, prms.Y, prms.Z, ((i == 0 ? n : -n) + t) * prms.D }, boundArray));

                    flag = false;
                    foreach (var verticesArray in verticesList.Where(v => v.Length >= 3))
                    {
                        var vertices = verticesArray.Select(v => new V3(v[0], v[1], v[2])).ToArray();

                        var plane = new Polygon(vertices, mat, DrawingMode.Surfaces);
                        var edge = new Polygon(vertices, mat, DrawingMode.Edges);

                        plane.Tag = edge.Tag = new latticeID();

                        var planesSub = plane.Decompose(glControlMain.FragShader == GLControlAlpha.FragShaders.ZSORT ? 4 : 0);

                        GLObjects.AddRange(planesSub);
                        glControlMain.AddObjects(planesSub);

                        GLObjects.Add(edge);
                        glControlMain.AddObjects(edge);

                        flag = true;
                    }
                    n++;
                }
            }
            textBoxInformation.AppendText("Generation of lattice planes: " + sw.ElapsedMilliseconds + "ms.\r\n");


            Draw();
        }
        #endregion

        #region 結晶格子軸を生成

        /// <summary>
        /// 格子軸GLControlを生成
        /// </summary>
        private void setAxesControl()
        {
            sw.Restart();

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

            //textBoxInformation.AppendText("Generation of crystal axis control: " + sw.ElapsedMilliseconds + "ms.\r\n");

        }
        #endregion

        #region 結晶構造をセッティング (SetGLObjects)

        /// <summary>
        /// 結晶構造をセッティング
        /// </summary>
        /// <param name="_crystal">nullでない場合は、Bounds と Lattice planes に関するコントロールがリセットされる</param>
        public void SetGLObjects(Crystal _crystal = null)
        {
            if (skipSetCrystal) return;

            textBoxInformation.Text = "";

            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (_crystal != null)
                SetLegend();

            Crystal = _crystal == null ? formMain.Crystal : _crystal;

            atomCoordinateTable1.Crystal = Crystal;//

            GLObjects = new List<GLObject>(); //GLObjectsを初期化

            initAxesMatrix(); //結晶軸マトリックスを初期化

            setUnitCellPlanes();//単位格子面の描画

            initBounds();//境界条件を初期化;

            SetLatticePlanes();//格子面オブジェクトを生成

            setBoundPlanes();//境界面オブジェクトを生成

            if (_crystal != null)
                setAtomsP();

            setAtoms();//原子オブジェクトを生成

            setBondsAndPolyhera();//結合と多面体オブジェクトを生成

            removeObjects();//余計な原子を削除

            toolStripLabelStatusInitialization.Text = GLObjects.Count + " objects were created (" + sw.ElapsedMilliseconds + " ms)";

            transferGLObjects(); //

            setAxesControl();//結晶軸を表示するGLControl

            Draw(); //
        } 
        #endregion

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
                if (double.IsNaN( rotMat.M11)) return;
                var pos = rotMat.Mult(glControlLight.LightPosition);
                if (double.IsNaN(pos.X)) return;
                glControlLight.LightPosition = glControlMain.LightPosition = glControlAxes.LightPosition = pos;
                foreach (var c in legendControls)
                    c.LightPosition = glControlLight.LightPosition;
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
                    textBoxInformation.AppendText(
                        Crystal.Atoms[(sphere.Tag as atomID).Index].Label 
                        + " (" + sphere.Origin.X + ", " + sphere.Origin.Y + ", " + sphere.Origin.Z + ")\r\n");
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
                SkipEvent = false;

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
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) 
            => MoveAtomControl(tabControl.SelectedTab == tabPageAtom);
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
            //AtomControlのItemsChangedイベントは拾わなくて良い。
            //CrystalControlでこのイベントが拾われ、GenerateFromInterface()されたあと、
            //FormMainから通知が来る。
        }



        #endregion

        #region 凡例の描画

        /// <summary>
        /// 凡例部分の描画
        /// </summary>
        private void SetLegend()
        {
            sw.Restart();

            if (!toolStripButtonLegend.Checked)
            {
                flowLayoutPanelLegend.Controls.Clear();
            }
            else
            {
                var size = new Size(numericBoxLegendSize.ValueInteger, numericBoxLegendSize.ValueInteger);
                glControlMain.SkipRendering = true;

                var atoms = atomControl.GetAll();
                if (atoms.Length == 0)
                    return;

                flowLayoutPanelLegend.SuspendLayout();

                for (int i = 0; i < Math.Max(atoms.Length, legendControls.Count); i++)
                {
                    if (i < atoms.Length)
                    {
                        if (legendControls.Count <= i)
                        {
                            legendControls.Add(new GLControlAlpha
                            {
                                AllowMouseRotation = false,
                                AllowMouseScaling = false,
                                AllowMouseTranslating = false,
                                DisablingOpenGL = false,
                                Name = "legend" + i.ToString(),
                                NodeCoefficient = 1,
                                ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
                                ProjWidth = 2.2D,
                                FragShader = GLControlAlpha.FragShaders.ZSORT,
                                LightPosition = glControlLight.LightPosition,
                                WorldMatrix = glControlLight.WorldMatrix,
                                ViewFrom = glControlLight.ViewFrom
                            });

                            legendLabels.Add(new Label { Font = Font, AutoSize = true });

                            legendPanels.Add(new FlowLayoutPanel
                            {
                                AutoSize = true,
                                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                                FlowDirection = FlowDirection.TopDown,
                                Margin = new Padding(1, 1, 1, 8),
                            });

                            legendPanels[i].Controls.Add(legendControls[i]);
                            legendPanels[i].Controls.Add(legendLabels[i]);
                        }
                        legendLabels[i].Text = atoms[i].Label;
                        legendControls[i].DeleteAllObjects();
                        legendControls[i].Size = size;
                        
                        flowLayoutPanelLegend.Controls.Add(legendPanels[i]);
                    }
                    else
                        flowLayoutPanelLegend.Controls.Remove(legendPanels[i]);
                }
                flowLayoutPanelLegend.ResumeLayout();

                var maxRadius = atoms.Max(a => a.Radius);
                for (int i = 0; i < atoms.Length; i++)
                {
                    legendLabels[i].Margin = new Padding((size.Width - legendLabels[i].Size.Width) / 2 + 3, 0, 0, 0);
                    legendControls[i].AddObjects(
                        new Sphere(new V3(0, 0, 0), atoms[i].Radius / maxRadius,
                        new Material(atoms[i].Argb, atoms[i].Transparency, atoms[i].Ambient, atoms[i].Diffusion, atoms[i].Specular, atoms[i].Shininess, atoms[i].Emission),
                        DrawingMode.Surfaces));
                    legendControls[i].LightPosition = glControlLight.LightPosition;
                }
            }
            glControlMain.SkipRendering = false;

            textBoxInformation.AppendText("Generation of legend control: " + sw.ElapsedMilliseconds + "ms.\r\n");
        }

        #endregion

        #region 凡例、光源方向、結晶軸のサイズ変更
        private void numericBoxLegendSize_ValueChanged(object sender, EventArgs e)
        {
            SetLegend();
        }

        private void numericBoxAxesSize_ValueChanged(object sender, EventArgs e)
        {
            glControlAxes.Size = new Size(numericBoxAxesSize.ValueInteger, numericBoxAxesSize.ValueInteger);
            glControlAxes.Location = new Point(0, glControlMain.Size.Height - glControlAxes.Size.Height);
        }

        private void numericBoxLightSize_ValueChanged(object sender, EventArgs e)
        {
            glControlLight.Size = new Size(numericBoxLightSize.ValueInteger, numericBoxLightSize.ValueInteger);
        }
        #endregion

        #region Vestaと同じような見栄えにする。
        private void buttonLikeVesta_Click(object sender, EventArgs e)
        {
            skipSetCrystal = true;

            foreach (var atoms in formMain.crystalControl.Crystal.Atoms)
                atoms.ResetVesta();

            ConvertCrystalData.SetOpenGL_property(formMain.crystalControl.Crystal);

            formMain.crystalControl.bondControl.Clear();
            formMain.crystalControl.bondControl.AddRange(formMain.crystalControl.Crystal.Bonds);
            skipSetCrystal = false;
            SetGLObjects(formMain.crystalControl.Crystal);
        }




        #endregion

        #region 描画品質を決定
        private void comboBoxRenderignQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRenderignQuality.SelectedIndex == 0)
            {
                glControlMain.NodeCoefficient = 4;
                Cone.Default = (1, 8);
                Cylinder.Default = (1, 8);
                Sphere.DefaultSlices = 2;

            }
            else if (comboBoxRenderignQuality.SelectedIndex == 1)
            {
                glControlMain.NodeCoefficient = 16;
                Cone.Default = (1, 16);
                Cylinder.Default = (1, 16);
                Sphere.DefaultSlices = 3;

            }
            else
            {
                glControlMain.NodeCoefficient = 32;
                Cone.Default = (1, 32);
                Cylinder.Default = (1, 32);
                Sphere.DefaultSlices = 5;
            }
            if (atomControl != null)
                SetGLObjects(formMain.crystalControl.Crystal);
        }

        private void comboBoxTransparency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTransparency.SelectedIndex == 0)
            {
                glControlMain.NodeCoefficient = 0;
                glControlMain.FragShader = GLControlAlpha.FragShaders.ZSORT;
            }
            else
            {
                if (glControlMain.Version < glControlMain.VersionForOIT)
                {
                    MessageBox.Show("OIT (order independent transparency) mode requires OpenGL 4.3 or later,\r\n"+
                        " but the current version is " + glControlMain.VersionStr + ". Sorry.", "Caution!");
                    comboBoxTransparency.SelectedIndex = 0;
                    return;
                }
                ;
                glControlMain.NodeCoefficient = 20;
                glControlMain.MaxFragments = 120;
                glControlMain.FragShader = GLControlAlpha.FragShaders.OIT;
            }
            if (atomControl != null)
                SetGLObjects(formMain.crystalControl.Crystal);
        }

        #endregion

        #region ProjectionMode (Perspectiveか、Orhographicか)などの設定
        private void comboBoxProjectionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            trackBarPerspective.Enabled = comboBoxProjectionMode.SelectedIndex == 1;

            glControlMain.ProjectionMode = comboBoxProjectionMode.SelectedIndex == 0 ?
                    GLControlAlpha.ProjectionModes.Orhographic : GLControlAlpha.ProjectionModes.Perspective;
        }

        private void trackBarPerspective_Scroll(object sender, EventArgs e)
        {
            var x = Math.Pow(51.0, 1.0 / 100.0);
            glControlMain.SetPerspectiveDistance(Math.Pow(x,trackBarPerspective.Value)-1);
        }

        #endregion

        #region Depth cueingの設定

        private void checkBoxDepthCueing_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxDepthCueing.Enabled = checkBoxDepthCueing.Checked;
            //なぜか更新されないことがあるので、2回実行する
            glControlMain.DepthCueing = (checkBoxDepthCueing.Checked, trackBarAdvancedDepthCueingFar.Value / 10.0, trackBarAdvancedDepthCueingNear.Value / 10.0);
            glControlMain.DepthCueing = (checkBoxDepthCueing.Checked, trackBarAdvancedDepthCueingFar.Value / 10.0, trackBarAdvancedDepthCueingNear.Value / 10.0);
        }
        private bool trackBarAdvanced2_ValueChanged(object sender, double value)
        {
            checkBoxDepthCueing_CheckedChanged(sender, new EventArgs());
            return false;
        }
        #endregion


    }
}
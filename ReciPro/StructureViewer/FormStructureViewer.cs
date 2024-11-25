#region using
using Crystallography.OpenGL;
using Microsoft.Scripting.Utils;
using OpenTK;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using C4 = OpenTK.Graphics.Color4;
using M3d = OpenTK.Matrix3d;
using V3 = OpenTK.Vector3d;
using V4 = OpenTK.Vector4d;
#endregion

namespace ReciPro;
public partial class FormStructureViewer : Form
{
    #region �t�B�[���h�A�v���p�e�B�A

    private static readonly List<int> neighborKeys = [];

    public Crystal Crystal;

    public FormMain formMain;
    //private FormAtom formAtom;

    /// <summary>
    /// ���q�ʒu���V�t�g������x�N�g��. �P�ʊi�q�̕`����A���̃x�N�g���ɏ]���ăV�t�g����
    /// </summary>
    private V3 shift;

    private Matrix3d axes;
    private List<(V4 prm, Color color)> bounds;

    public List<GLObject> GLObjects = [];
    private readonly ParallelQuery<GLObject> GLObjectsP;

    public readonly object lockObj1 = new();
    public readonly object lockObj2 = new();
    public readonly object lockObj3 = new();


    private bool skipSetCrystal = false;

    public bool SkipEvent { get; set; } = false;

    private BoundControl boundControl;
    private LatticePlaneControl latticePlaneControl;
    private AtomControl atomControl;
    private BondInputControl bondControl;

    private readonly List<GLControlAlpha> legendControls = [];
    private readonly List<Label> legendLabels = [];
    private readonly List<FlowLayoutPanel> legendPanels = [];

    private readonly Stopwatch sw = new();

    private ParallelQuery<(int Index, V3 Pos, Material Mat, double Radius)> enabledAtomsP;
    private Atoms[] enabledAtoms;

    private GLControlAlpha glControlLight;
    private GLControlAlpha glControlMain;
    private GLControlAlpha glControlMainZsort;
    private GLControlAlpha glControlMainOIT;
    private GLControlAlpha glControlAxes;
    #endregion

    #region ���[�J���N���X
    private class atomID(in int index, in bool isInside, in int cellKey)
    {
        /// <summary>
        /// Bounds�̓������ǂ���
        /// </summary>
        public bool IsInside = isInside;
        /// <summary>
        /// �����\���t�@�C���̉��Ԗڂ̌��q�� (�����Ȃ��͓̂����ԍ��ɂȂ�)
        /// </summary>
        public int Index = index;
        /// <summary>
        /// �������Ă���Z���̏ꏊ
        /// </summary>
        public int CellKey = cellKey;

        public static int ComposeKey(int a, int b, int c) => a * 512 * 512 + b * 512 + c;
        public static int ComposeKey(V3 v) => ComposeKey((short)v.X, (short)v.Y, (short)v.Z);
    }

    private class bondID(in int serialNumber1, in int serialNumber2)
    {
        public readonly int SerialNumber1 = serialNumber1, SerialNumber2 = serialNumber2;
    }

    private class cellID { }
    private class latticeID { }
    private class boundsID { }
    #endregion

    #region �R���X�g���N�^

    public FormStructureViewer()
    {
        InitializeComponent();
        GLObjectsP = GLObjects.AsParallel();

        for (int a = -1; a < 2; a++)
            for (int b = -1; b < 2; b++)
                for (int c = -1; c < 2; c++)
                    neighborKeys.Add(atomID.ComposeKey(a, b, c));
    }

    private void FormStructureViewer_Load(object sender, EventArgs e)
    {
        if (DesignMode) return;

        #region �f�U�C�i�����Ȃ��悤�ɂ�����GL�R���g���[����ǉ�

        // glControlAxes
        glControlAxes = new GLControlAlpha
        {
            AllowMouseRotation = false,
            AllowMouseScaling = false,
            AllowMouseTranslating = false,
            BorderStyle = BorderStyle.Fixed3D,
            MaxHeight = 1,
            MaxWidth = 1,
            Name = "glControlAxes",
            NodeCoefficient = 1,
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 4D,
            RotationMode = GLControlAlpha.RotationModes.Object,
            TranslatingMode = GLControlAlpha.TranslatingModes.View,
            Location = new Point(0, 0),
            Size = new Size(numericBoxAxesSize.ValueInteger, numericBoxAxesSize.ValueInteger),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left
        };
        glControlAxes.MouseMove += glControlAxes_MouseMove;
        glControlAxes.SuspendLayout();

        // glControlLight
        glControlLight = new GLControlAlpha
        {
            AllowMouseRotation = false,
            AllowMouseScaling = false,
            AllowMouseTranslating = false,
            BorderStyle = BorderStyle.Fixed3D,
            MaxHeight = 1,
            MaxWidth = 1,
            Name = "glControlLight",
            NodeCoefficient = 1,
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 4D,
            RotationMode = GLControlAlpha.RotationModes.Object,
            TranslatingMode = GLControlAlpha.TranslatingModes.View,
            Location = new Point(0, 0),
            Size = new Size(numericBoxLightSize.ValueInteger, numericBoxLightSize.ValueInteger),
            Anchor = AnchorStyles.Top | AnchorStyles.Left
        };
        glControlLight.SuspendLayout();
        glControlLight.MouseMove += glControlLight_MouseMove;

        // glControlMainZsort
        glControlMainZsort = new GLControlAlpha
        {
            AllowMouseRotation = false,
            AllowMouseScaling = true,
            AllowMouseTranslating = true,
            BorderStyle = BorderStyle.Fixed3D,
            MaxHeight = 1,
            MaxWidth = 1,
            Name = "glControlMainZsort",
            NodeCoefficient = 1,
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 4D,
            RotationMode = GLControlAlpha.RotationModes.Object,
            TranslatingMode = GLControlAlpha.TranslatingModes.View,
            Dock = DockStyle.Fill
        };
        glControlMainZsort.SuspendLayout();
        glControlMainZsort.MouseDown += glControlMain_MouseDown;
        glControlMainZsort.MouseMove += glControlMain_MouseMove;

        glControlMainOIT = new GLControlAlpha(GLControlAlpha.FragShaders.OIT)
        {
            AllowMouseRotation = false,
            AllowMouseScaling = true,
            AllowMouseTranslating = true,
            BorderStyle = BorderStyle.Fixed3D,
            MaxHeight = 1440,
            MaxWidth = 2560,
            Name = "glControlMainOIT",
            NodeCoefficient = 10,
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 4D,
            RotationMode = GLControlAlpha.RotationModes.Object,
            TranslatingMode = GLControlAlpha.TranslatingModes.View,
            Dock = DockStyle.Fill
        };
        glControlMainOIT.SuspendLayout();
        glControlMainOIT.MouseDown += glControlMain_MouseDown;
        glControlMainOIT.MouseMove += glControlMain_MouseMove;
        glControlMainOIT.Visible = false;

        glControlMain = glControlMainZsort;

        // splitContainer1.Panel1��glControl��ǉ�
        splitContainer1.SuspendLayout();
        splitContainer1.Panel1.Controls.Add(glControlAxes);
        splitContainer1.Panel1.Controls.Add(glControlLight);
        splitContainer1.Panel1.Controls.Add(glControlMainZsort);
        glControlAxes.Location = new Point(0, glControlMain.Height - glControlAxes.Height);

        #endregion

        #region GL�R���g���[���̏�����
        glControlLight.AddObjects(new Sphere(new V3(0, 0, 0), 1.0, new Material(C4.Gray), DrawingMode.Surfaces));
        glControlMain.LightPosition = glControlLight.LightPosition = glControlAxes.LightPosition = new V3(100, 100, 100);
        glControlMain.ViewFrom = glControlLight.ViewFrom = glControlAxes.ViewFrom = new V3(0, 0, 50);
        glControlLight.ProjWidth = 2.2;
        glControlAxes.ProjWidth = 2.6;
        glControlMain.ProjWidth = 3f;

        glControlMainZsort.ToolTip = "Main viewer\r\n" +
            "  Left drag: Rotate objects\r\n" +
            "  Right drag up/down: Zoom in/out\r\n" +
            "  Wheel up/down: Zoom in/out\r\n" +
            "  Middle drag: Drag the image";

        glControlLight.ToolTip = "Light direction\r\n" +
            "  Left drag: Change the light direction";

        glControlAxes.ToolTip = "Crystal axes\r\n" +
            "  Left drag: Rotate objects";

        #endregion

        #region �r�f�I�J�[�h�̐ݒ�
        foreach (var info in GLControlAlpha.GraphicsInfo)
        {
            labelGraphicsCard.Text += info.Product + "  ";
            labelGraphicsDriver.Text += info.Version + "  ";
        }
        labelOpenGLversion.Text += GLControlAlpha.VersionStr;

        if (GLControlAlpha.GraphicsInfo.Select(g => g.Product.ToLower()).Any(p => p.Contains("nvidia") || p.Contains("amd")))
            comboBoxRenderignQuality.SelectedIndex = 1;
        else
            comboBoxRenderignQuality.SelectedIndex = 0;

        Sphere.DefaultDictionary.Clear();
        Cylinder.DefaultDictionary.Clear();

        #endregion

        #region �e�탆�[�U�[�R���g���[���̒ǉ�

        boundControl = formMain.crystalControl.boundControl;
        latticePlaneControl = formMain.crystalControl.latticePlaneControl;
        atomControl = formMain.crystalControl.atomControl;
        bondControl = formMain.crystalControl.bondControl;

        tabPageBond.SuspendLayout();
        tabPageBond.Controls.Add(bondControl);
        tabPageBoundPlane.SuspendLayout();
        tabPageBoundPlane.Controls.Add(boundControl);
        tabPageLatticePlane.SuspendLayout();
        tabPageLatticePlane.Controls.Add(latticePlaneControl);
        latticePlaneControl.BringToFront();


        boundControl.ItemsChanged += BoundControl_BoundsChanged;
        latticePlaneControl.ItemsChanged += LatticePlaneControl_LatticePlaneChanged;
        bondControl.ItemsChanged += BondControl_BondsChanged;
        atomControl.ItemsChanged += AtomControl_ItemsChanged;
        atomControl.GLEnableChanged += AtomControl_GLEnableChanged;

        #endregion

        #region �R���g���[���̒ǉ��ݒ�

        flowLayoutPanelLegend.AutoSize = true;
        flowLayoutPanelLegend.SendToBack();

        tabControlBoundOption.ItemSize = new Size(0, 1);

        comboBoxProjectionMode.SelectedIndex = 0;
        comboBoxTransparency.SelectedIndex = 0;

        checkBoxDepthCueing_CheckedChanged(new object(), new EventArgs());

        #endregion

        #region ResumuLayout()
        glControlMainOIT.ResumeLayout();
        glControlMainZsort.ResumeLayout();
        glControlLight.ResumeLayout();
        glControlAxes.ResumeLayout();
        splitContainer1.ResumeLayout();
        tabPageBond.ResumeLayout();
        tabPageBoundPlane.ResumeLayout();
        tabPageLatticePlane.ResumeLayout();
        #endregion

        SkipEvent = true;
        numericBoxClientWidth.Value = splitContainer1.Panel1.Width;
        numericBoxClientHeight.Value = splitContainer1.Panel1.Height;
        SkipEvent = false;
    }

    private void FormStructureViewer_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)//���ꂽ�Ƃ����C���E�B���h�E�̌�����\������
        {
            if (formMain.crystalControl.Crystal != null)
                SetGLObjects(formMain.crystalControl.Crystal);
            formMain.toolStripButtonStructureViewer.Checked = true;
        }
        MoveAtomControl(Visible && tabControl.SelectedTab == tabPageAtom);
    }
    private void FormStructureViewer_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        formMain.toolStripButtonStructureViewer.Checked = false;
        this.Visible = false;
    }

    #endregion �R���X�g���N�^

    #region �������s���ݒ�
    private void initAxesMatrix()
    {
        axes.Row0 = new V3(Crystal.A_Axis.X, Crystal.B_Axis.X, Crystal.C_Axis.X);
        axes.Row1 = new V3(Crystal.A_Axis.Y, Crystal.B_Axis.Y, Crystal.C_Axis.Y);
        axes.Row2 = new V3(Crystal.A_Axis.Z, Crystal.B_Axis.Z, Crystal.C_Axis.Z);

        shift = axes.Column0 * numericBoxProjectionCenterX.Value
            + axes.Column1 * numericBoxProjectionCenterY.Value
            + axes.Column2 * numericBoxProjectionCenterZ.Value;
    }
    #endregion

    #region Bounds (���E��)��������
    /// <summary>
    /// ���E�ʂ�������
    /// </summary>
    private void initBounds()
    {
        sw.Restart();
        bounds = [];


        if (radioButtonBoundUnitCell.Checked)//���E�����Ƃ���Unit cell���I������Ă���ꍇ
        {
            var inv = Matrix3d.Invert(axes);
            bounds =
                [
                    (new V4(inv.Row0.Normalized(),1/inv.Row0.Length * (numericBoxACenter.Value + numericBoxARange.Value)) , Color.Red),
                    (new V4(-inv.Row0.Normalized(),1/inv.Row0.Length *  -(numericBoxACenter.Value - numericBoxARange.Value)), Color.Red),
                    (new V4(inv.Row1.Normalized(),1/inv.Row1.Length *  (numericBoxBCenter.Value + numericBoxBRange.Value)), Color.Green),
                    (new V4(-inv.Row1.Normalized(),1/inv.Row1.Length * -(numericBoxBCenter.Value - numericBoxBRange.Value)), Color.Green),
                    (new V4(inv.Row2.Normalized(),1/inv.Row2.Length * (numericBoxCCenter.Value + numericBoxCRange.Value)), Color.Blue),
                    (new V4(-inv.Row2.Normalized(),1/inv.Row2.Length * -(numericBoxCCenter.Value - numericBoxCRange.Value)), Color.Blue),
                ];

        }
        else//Plane���I������Ă���ꍇ
        {
            foreach (var bc in boundControl.GetAll().Where(b => b.Enabled && b.PlaneParams != null && b.Index != (0, 0, 0)))
                foreach (var (X, Y, Z, D) in bc.PlaneParams)
                    bounds.Add((new V4(X, Y, Z, D), bc.Color));
            if (!Geometry.Enclosed(bounds.Select(b => b.prm.ToArray()).ToArray()))//�`��͈͂����Ă��Ȃ��ꍇ
            {
                //���ʂɋ߂��|���S�����쐬
                //���������6�ʕ���
                var rot = new[] {
                new M3d(1, 0, 0, 0, 1, 0, 0, 0, 1),
                new M3d(1, 0, 0, 0, 0, -1, 0, 1, 0),
                new M3d(1, 0, 0, 0, -1, 0, 0, 0, -1),
                new M3d(1, 0, 0, 0, 0, 1, 0, -1, 0),
                new M3d(0, 0, 1, 0, 1, 0, -1, 0, 0),
                new M3d(0, 0, -1, 0, 1, 0, 1, 0, 0), };

                var slices = 3;
                var maxDistance = boundControl.MaximumDistance;
                for (int i = 0; i < rot.Length; i++)
                    for (int h = -slices; h <= slices; h++)
                        for (int w = -slices; w <= slices; w++)
                        {
                            var n = new V4(V3.Normalize(rot[i].Mult(new V3(w, h, slices))), maxDistance);
                            bounds.Add((n, Color.Gray));
                        }

            }
        }

        textBoxCalcInformation.AppendText($"Initialization of bounds: {sw.ElapsedMilliseconds}ms.\r\n");
    }

    #endregion

    #region Bounds (���E��) �� GLObjects �̐���

    /// <summary>
    /// ���E�ʃI�u�W�F�N�g�𐶐�
    /// </summary>
    private void setBoundPlanes()
    {
        sw.Restart();
        if (bounds == null)
            return;
        //���E�ʂ�ǉ�
        if (checkBoxShowBoundPlanes.Checked)
        {
            var boundsArray = bounds.Select(b => b.prm.ToArray()).ToArray();
            Parallel.For(0, bounds.Count, i =>
            {
                var vertices = Geometry.GetClippedPolygon(i, boundsArray);
                var mat = new Material(bounds[i].color, numericBoxBoundPlanesOpacity.Value);
                if (vertices != null && vertices.Length >= 3)
                {
                    var polygon = new Polygon(vertices.Select(v => new V3(v[0], v[1], v[2])).ToArray(), mat, DrawingMode.SurfacesAndEdges)
                    {
                        Rendered = checkBoxShowBoundPlanes.Checked,
                        Tag = new boundsID()
                    }.Decompose(glControlMain.FragShader == GLControlAlpha.FragShaders.ZSORT ? 3 : 0);

                    lock (lockObj1)
                        GLObjects.AddRange(polygon);
                }
            });
        }

        glControlMain.SetClip(checkBoxClipObjects.Checked ? new Clip(bounds.Select(b => b.prm).ToArray()) : null);

        textBoxCalcInformation.AppendText($"Generation of bound planes: {sw.ElapsedMilliseconds}ms.\r\n");
    }

    #endregion

    #region ���q GLObjects�̐���

    /// <summary>
    /// ���q�I�u�W�F�N�g�𐶐�
    /// </summary>
    public void setAtoms()
    {
        sw.Restart();

        if (checkBoxHideAllAtoms.Checked) return;

        //臒l. �`��͈͂����̐��l���������Ƃ��Ă��A�ꉞ���q���W�͌v�Z���Ă����āA�{���h�̗L�����l�����A�K�v�Ȃ���΍ŏI�I�ɏ���
        var threshold = -0.001;
        if (checkBoxShowBondedAtoms.Checked)
        {
            var bonds = bondControl.GetAll().Where(b => b.Enabled);
            threshold = bonds.Any() ? -bonds.Max(b => b.MaxLength) * 1.01 : -0.1;
            threshold = Math.Max(-0.5, threshold);
        }

        //���q��ǉ�
        List<V3> dirs = [new V3(1, 0, 0), new V3(-1, 0, 0), new V3(0, 1, 0), new V3(0, -1, 0), new V3(0, 0, 1), new V3(0, 0, -1)];
        List<V3> outer = [new(0, 0, 0)];
        List<V3> whole = [];

        while (outer.Count != 0 && whole.Count < 10000)
        {
            List<V3> outerNew = [];
            foreach (var baseCell in outer)
                foreach (var dir in whole.Count == 0 ? [new V3(0, 0, 0)] : dirs)
                {
                    var cell = baseCell + dir;
                    if (!whole.Contains(cell))
                    {
                        whole.Add(cell);

                        var spheres = new List<Sphere>();
                        enabledAtomsP.ForAll(o =>
                        {
                            var pos = axes.Mult(cell + o.Pos) - shift;
                            var min = bounds.Min(b => V4.Dot(new V4(pos, 1), b.prm));
                            if (min > threshold)
                            {
                                var sphere = new Sphere(pos, o.Radius, o.Mat, DrawingMode.Surfaces)
                                {
                                    Rendered = min > -0.0000001,
                                    Tag = new atomID(o.Index, min > -0.0000001, atomID.ComposeKey(cell))
                                };
                                lock (lockObj1)
                                    spheres.Add(sphere);
                            }
                        });

                        if (spheres.Count != 0)
                        {
                            outerNew.Add(cell);
                            GLObjects.AddRange(spheres);
                        }
                    }
                }
            outer = outerNew;
        }

        textBoxCalcInformation.AppendText($"Generation of aoms: {sw.ElapsedMilliseconds}ms.\r\n");
    }

    public void SetAtomsP()
    {
        sw.Restart();

        var list = new List<(int Index, V3 Pos, Material Mat, double Radius)>();
        //�ʒu���S���������q�����݂���ꍇ�́A�ł�Occ���傫�����̂�I�ԁB���ꂪ��������ꍇ�́Aindex���Ⴂ����I��

        enabledAtoms = Crystal.Atoms.Where(a => a.GLEnabled).ToArray();

        for (int i = 0; i < enabledAtoms.Length; i++)
        {
            var a = enabledAtoms[i];
            if (enabledAtoms.Where((b, j) => i != j && a.X == b.X && a.Y == b.Y && a.Z == b.Z && (a.Occ < b.Occ || (a.Occ <= b.Occ && i > j))).Any() == false)
            {
                var mat = new Material(a.Argb, a.Texture);
                var radius = a.Radius * 0.1;//nm��A�ɕϊ�
                foreach (var pos in a.Atom.Select(v => new V3(v.X, v.Y, v.Z)))
                    list.Add((i, pos, mat, radius));
            }
        }
        enabledAtomsP = list.AsParallel();

        textBoxCalcInformation.AppendText($"Selection of enabled aoms: {sw.ElapsedMilliseconds}ms.\r\n");
    }

    #endregion

    #region Bonds(����)��Polyhedra (�z�ʑ��ʑ�)�I�u�W�F�N�g�̐���

    /// <summary>
    /// Bond�̒��_��\�����߂̃e���|�����[�ȃN���X
    /// </summary>
    private readonly struct bondVertex(in int objIndex, in int atomIndex, in int cellKey, in V3 origin, in int serial, in double r, in Material bondMat, in Material polyMat)
    {
        public readonly int ObjIndex = objIndex, AtomIndex = atomIndex, Key = cellKey, Serial = serial;
        public readonly V3 O = origin;
        public readonly double R = r;
        public readonly Material BondMat = bondMat, PolyMat = polyMat;
    }

    /// <summary>
    /// ����(Bonds)�Ɣz�ʑ��ʑ�(Polyhera)�I�u�W�F�N�g�𐶐�
    /// </summary>
    private void setBondsAndPolyhera()
    {
        sw.Restart();
        static bool within(double d, double max, double min) => d < max && d > min;
        var dic1 = new Dictionary<string, bondVertex[]>();
        var dicMaterial = new Dictionary<int, Material>();
        bondControl.GetAll().Where(b => b.Enabled && (b.ShowPolyhedron || b.ShowBond)).ToList().ForEach(bond =>
        {
            double min = Math.Max(bond.MinLength, 0), min2 = min * min, max = bond.MaxLength, max2 = max * max, radius = bond.Radius;
            var polyhedronMode = bond.ShowEdges ? DrawingMode.SurfacesAndEdges : DrawingMode.Surfaces;
            float bondTrans = bond.BondTransParency, polyTrans = bond.PolyhedronTransParency;

            //�ŏ��ɁA���S�ƒ��_��dic1�Ɋi�[����. �������Ă����΁A2��ڂɏo�Ă����Ƃ��A�Č������Ȃ��čς�.
            foreach (var element in (new[] { bond.Element1, bond.Element2 }).Where(element => !dic1.ContainsKey(element)))
            {
                dic1.Add(element,
                    [
                        .. GLObjectsP.Select((GLObject Obj, int ObjIndex) => (Obj, ObjIndex))
                        .Where(e => e.Obj.Tag is atomID id && enabledAtoms[id.Index].ElementName == element).Select(e =>
                        {
                            var s = e.Obj as Sphere;
                            var id = s.Tag as atomID;
                            if (!dicMaterial.TryGetValue(id.Index, out var bondMat))
                            {
                                bondMat = new Material(s.Material.Color, bondTrans);
                                lock (lockObj1)
                                    dicMaterial.TryAdd(id.Index, bondMat);
                            }
                            if (!dicMaterial.TryGetValue(id.Index + 1000, out var polyMat))
                            {
                                polyMat = new Material(s.Material.Color, polyTrans);
                                lock (lockObj1)
                                    dicMaterial.TryAdd(id.Index + 1000, polyMat);
                            }
                            return new bondVertex(e.ObjIndex, id.Index, id.CellKey, s.Origin, s.SerialNumber, s.Radius, bondMat, polyMat);
                        }).OrderBy(o => o.Key),
                    ]);
            }

            //���S���_�ɑ΂���A���_�̃��X�g����C�ɍ쐬
            var dic2 = new Dictionary<int, int[]>();//���S�ɑ΂��钸�_���X�g��Dictionary
            var coord = new Dictionary<int, int>(); //���q�ԍ��Ɣz�ʐ���ۑ�����Dictionary
            var cArray = dic1[bond.Element1];
            var vArray = dic1[bond.Element2];
            Parallel.ForEach(cArray.Select(o => o.Key).Distinct(), ckey =>
            {
                //�����Ώۂ̒��_�̃C���f�b�N�X���쐬
                var vertexIndices = neighborKeys.Select(key => key + ckey)
                .Select(key => (Start: Array.FindIndex(vArray, d => d.Key == key), End: Array.FindLastIndex(vArray, d => d.Key == key)))
                .Where(d => d.Start >= 0).SelectMany(d => Enumerable.Range(d.Start, d.End - d.Start + 1)).ToArray();

                int start = cArray.FindIndex(d => d.Key == ckey), end = Array.FindLastIndex(cArray, d => d.Key == ckey) + 1;
                for (int i = start; i < end; i++)
                {
                    //�{���h�����̏����𖞂������_�C���f�b�N�X������
                    var vIndices = vertexIndices.Where(j => within((vArray[j].O - cArray[i].O).LengthSquared, max2, min2)).ToArray();
                    int m = vIndices.Length, index = cArray[i].AtomIndex;
                    if (m > 0)
                        lock (lockObj2)
                        {
                            if (!checkBoxShowBondedAtoms.Checked)
                                dic2.Add(i, vIndices);
                            else if (!coord.TryGetValue(index, out int n))//�܂�coord�ɉ����ǉ�����Ă��Ȃ��ꍇ���A�͈͊O�̌������Ă��錴�q��`�悵�Ȃ��ꍇ
                            {
                                dic2.Add(i, vIndices);
                                coord.Add(index, m);
                            }
                            else if (n <= m)
                            {
                                dic2.Add(i, vIndices);
                                if (n < m)//�z�ʐ��̍ő�l���X�V���ꂽ�ꍇ�́A�z�ʐ��̕s���S��vertices������
                                {
                                    coord[index] = m;
                                    dic2.Where(o => cArray[o.Key].AtomIndex == index && o.Value.Length < m).ToList().ForEach(o => dic2.Remove(o.Key));
                                }
                            }
                        }
                }
            });

            //���_��Rendered��True�ɕύX
            var dic2P = dic2.AsParallel();
            dic2P.SelectMany(d => d.Value.Select(v2 => vArray[v2].ObjIndex)).Distinct().ForAll(index => GLObjects[index].Rendered = true);
            dic2P.Select(d => cArray[d.Key].ObjIndex).Distinct().ForAll(index => GLObjects[index].Rendered = true);

            //bonds��polyhedra��ǉ�
            dic2P.ForAll(d =>
            {
                var c = cArray[d.Key];
                var vIndices = d.Value;

                var objects = new GLObject[vIndices.Length * 2];
                int n = 0;
                foreach (var i in vIndices)
                {
                    var v = vArray[i];
                    var vec = v.O - c.O;//���S�Ԃ����ԃx�N�g��
                    var m = (1 + (c.R - v.R) / vec.Length) * vec / 2;//���Ԓn�_
                    var tag = new bondID(c.Serial, v.Serial);
                    objects[n++] = new Cylinder(c.O, m, radius, c.BondMat, DrawingMode.Surfaces) { Tag = tag, Rendered = bond.ShowBond };
                    objects[n++] = new Cylinder(v.O, m - vec, radius, v.BondMat, DrawingMode.Surfaces) { Tag = tag, Rendered = bond.ShowBond };
                }
                lock (lockObj3)
                    GLObjects.AddRange(objects);

                if (bond.ShowPolyhedron && vIndices.Length > 2)
                {
                    if (vIndices.Length == 3)
                    {
                        var poly = new Polygon(vIndices.Select(v => vArray[v].O), c.PolyMat, polyhedronMode) { Rendered = bond.ShowPolyhedron };
                        lock (lockObj3)
                            GLObjects.Add(poly);
                    }
                    else
                    {
                        var poly = new Polyhedron(vIndices.Select(v => vArray[v].O), c.PolyMat, polyhedronMode) { Rendered = bond.ShowPolyhedron, ShowClippedSection = false }.ToPolygons();//order=2�ŁA12���炢�ɕ��� => �v�Z���Ԃ������肷����̂ŁA����ς��߁B
                        lock (lockObj3)
                            GLObjects.AddRange(poly);
                    }
                }

            });
        });

        textBoxCalcInformation.AppendText($"Generation of bonds & polyhedra: {sw.ElapsedMilliseconds}ms.\r\n");
    }

    #endregion

    #region �]���Ȍ��q���폜
    /// <summary>
    /// �]���Ȍ��q���폜����
    /// </summary>
    private void removeObjects()
    {
        sw.Restart();
        //�{���h���\�����錴�q�����A�`��͈͊O�̂��ߌǗ����Ă��܂������q���폜

        //�{���h���\������vertex���̌��q�̃V���A���ԍ����擾
        var vertexSerials = GLObjectsP
            .Where(obj => obj is Cylinder)
            .SelectMany(obj => new[] { (obj.Tag as bondID).SerialNumber1, (obj.Tag as bondID).SerialNumber2 })
            .Distinct().ToList();

        //�͈͊O�ł���A�Ȃ����A��̃V���A���ԍ��Ɋ܂܂�Ȃ����q���擾
        int n = 0;
        GLObjectsP.ForAll(o =>
        {
            if (o.Tag is atomID id && !id.IsInside && !vertexSerials.Contains(o.SerialNumber))
            {
                o.Z = double.NegativeInfinity;
                Interlocked.Increment(ref n);
            }
        });

        GLObjects.Sort((o1, o2) => o1.Z.CompareTo(o2.Z));//���񉻂���Ȃ��Ă��\�������݂����B�B�B
        GLObjects.RemoveRange(0, n);

        textBoxCalcInformation.AppendText($"Remove tentative atoms: {sw.ElapsedMilliseconds}ms.\r\n");
    }
    #endregion

    #region ���x���̕�����`��
    private void setLabel()
    {
        sw.Restart();
        var labelSize = (float)numericBoxLabelSize.Value;
        var edge = checkBoxLabelWhiteEdge.Checked;

        glControlMainZsort.MakeCurrent();

        foreach (var s in GLObjects.Where(o => o.Rendered && o is Sphere).Cast<Sphere>().ToArray())
        {
            var index = (s.Tag as atomID).Index;
            var mat = radioButtonUseMaterialColor.Checked ? s.Material : new Material(colorControlLabelColor.Color, 1);
            var text = new TextObject(enabledAtoms[index].Label, labelSize, s.Origin, s.Radius + 0.01, edge, mat) { Rendered = enabledAtoms[index].ShowLabel };
            GLObjects.Add(text);
        }
        textBoxCalcInformation.AppendText($"Generation of label objects: {sw.ElapsedMilliseconds}ms.\r\n");
    }

    #endregion

    #region GLObjects���V�F�[�_�ɓ]��
    /// <summary>
    /// GLObjects��]������
    /// </summary>
    private void transferGLObjects()
    {
        sw.Restart();
        glControlMain.DeleteAllObjects();
        glControlMain.AddObjects(GLObjects);
        toolStripLabelStatusInitialization.Text += $" and sent to OpenGL ({sw.ElapsedMilliseconds} ms.)  ";
        textBoxCalcInformation.AppendText($"Trasfer: {sw.ElapsedMilliseconds}ms.\r\n");

    }
    #endregion

    #region �P�ʊi�q�ʃI�u�W�F�N�g�𐶐�
    /// <summary>
    /// �P�ʊi�q�ʃI�u�W�F�N�g�𐶐�
    /// </summary>
    private void setUnitCellPlanes()
    {
        sw.Restart();

        while (GLObjects.Any(obj => obj.Tag is cellID))
        {
            glControlMain.DeleteObjects(GLObjects.First(obj => obj.Tag is cellID));
            GLObjects.Remove(GLObjects.First(obj => obj.Tag is cellID));
        }

        var t = axes.Mult(new V3(numericBoxCellTransrationA.Value, numericBoxCellTransrationB.Value, numericBoxCellTransrationC.Value)) + shift;
        V3 zero = new(0), c0 = axes.Column0, c1 = axes.Column1, c2 = axes.Column2;

        //�G�b�W�̕`��
        if (checkBoxUnitCell.Checked && checkBoxCellShowEdge.Checked)
        {
            V3[][][] axesArray = [
                    [[zero, c0], [c1, c1 + c0], [c2, c2 + c0], [c1 + c2, c1 + c2 + c0]],
                [[zero, c1], [c2, c2 + c1], [c0, c0 + c1], [c2 + c0, c2 + c0 + c1]],
                [[zero, c2], [c0, c0 + c2], [c1, c1 + c2], [c0 + c1, c0 + c1 + c2]]
                ];

            int[] colors = radioButtonCellEdgeColorAll.Checked ?
                [colorControlCellEdge.Argb, colorControlCellEdge.Argb, colorControlCellEdge.Argb] :
                [colorControlCellEdgeA.Argb, colorControlCellEdgeB.Argb, colorControlCellEdgeC.Argb];

            for (int i = 0; i < 3; i++)
                foreach (var edge in axesArray[i])
                {
                    var line = new Lines(edge.Select(e => e - t).ToArray(), trackBarCellEdgeWidth.Value, new Material(colors[i])) { Tag = new cellID() };
                    GLObjects.Add(line);
                    glControlMain.AddObjects(line);
                }
        }

        if (checkBoxUnitCell.Checked && checkBoxCellShowPlane.Checked)
        {
            //�ʂ̕`��
            V3[][][] planesArray = [
                    [[zero, c1, c1 + c2, c2], [c0, c0 + c1, c0 + c1 + c2, c2 + c0]],
                [[zero, c2, c2 + c0, c0], [c1, c1 + c2, c1 + c2 + c0, c0 + c1]],
                [[zero, c0, c0 + c1, c1], [c2, c2 + c0, c2 + c0 + c1, c1 + c2]]
                ];
            int[] colors = radioButtonCellPlaneColorAll.Checked ?
                   [colorControlCellPlane.Argb, colorControlCellPlane.Argb, colorControlCellPlane.Argb] :
                   [colorControlCellPlaneA.Argb, colorControlCellPlaneB.Argb, colorControlCellPlaneC.Argb];

            for (int i = 0; i < 3; i++)
                foreach (var edge in planesArray[i])
                {
                    var plane = new Polygon(edge.Select(e => e - t).ToArray(), new Material(colors[i], numericBoxCellPlaneAlpha.Value), DrawingMode.Surfaces)
                    { Tag = new cellID() }.Decompose(glControlMain.FragShader == GLControlAlpha.FragShaders.ZSORT ? 3 : 0);
                    GLObjects.AddRange(plane);
                    glControlMain.AddObjects(plane);
                }
        }
        textBoxCalcInformation.AppendText("Generation of cell planes: " + sw.ElapsedMilliseconds + "ms.\r\n");

    }
    #endregion

    #region �����i�q�� GLObjects�̐���

    /// <summary>
    /// �i�q�ʃI�u�W�F�N�g�𐶐�
    /// </summary>
    public void SetLatticePlanes()
    {
        sw.Restart();

        while (GLObjects.Any(obj => obj.Tag is latticeID))
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
            var mat = new Material(color.ToArgb(), numericBoxLatticePlaneOpacity.Value);
            int n = 0;
            var flag = true;
            var prm = new[] { prms.X, prms.Y, prms.Z, 0 };
            while (flag)
            {
                var verticesList = new List<double[][]>();
                for (int i = 0; i < (n == 0 ? 1 : 2); i++)
                {
                    var vertices = Geometry.GetClippedPolygon([prms.X, prms.Y, prms.Z, ((i == 0 ? n : -n) + t) * prms.D], boundArray);
                    if(vertices!=null)
                        verticesList.Add(vertices);
                }
                flag = false;
                foreach (var verticesArray in verticesList.Where(v => v.Length >= 3))
                {
                    var vertices = verticesArray.Select(v => new V3(v[0], v[1], v[2])).ToArray();

                    var plane = new Polygon(vertices, mat, DrawingMode.Surfaces);
                    var edge = new Polygon(vertices, mat, DrawingMode.Edges);

                    plane.Tag = edge.Tag = new latticeID();

                    // ZSort�̎��́Aorder = 3��64����
                    var planesSub = plane.Decompose(glControlMain.FragShader == GLControlAlpha.FragShaders.ZSORT ? 3 : 0);

                    GLObjects.AddRange(planesSub);
                    glControlMain.AddObjects(planesSub);

                    GLObjects.Add(edge);
                    glControlMain.AddObjects(edge);

                    flag = true;
                }
                n++;
            }
        }
        textBoxCalcInformation.AppendText("Generation of lattice planes: " + sw.ElapsedMilliseconds + "ms.\r\n");
    }
    #endregion

    #region �����i�q���𐶐�

    /// <summary>
    /// �i�q��GLControl�𐶐�
    /// </summary>
    private void setAxesControl()
    {
        sw.Restart();

        var cry = formMain.Crystal;
        var max = new[] { cry.A, cry.B, cry.C }.Max();
        var vec = new[] { cry.A_Axis / max, cry.B_Axis / max, cry.C_Axis / max };
        var color = new[] { C4.Red, C4.Green, C4.Blue };

        var obj = new List<GLObject>(10);
        var label = new[] { "a", "b", "c" };
        for (int i = 0; i < 3; i++)
        {
            obj.Add(new Cylinder(-vec[i], vec[i] * 2 - 0.3 * vec[i].Normarize(), 0.075, new Material(color[i]), DrawingMode.Surfaces));
            obj.Add(new Cone(vec[i], -0.3 * vec[i].Normarize(), 0.15, new Material(color[i]), DrawingMode.Surfaces));
            obj.Add(new TextObject(label[i], 11, vec[i] + 0.1 * vec[i].Normarize(), 0, true, new Material(color[i])));
        }
        obj.Add(new Sphere(new V3(0, 0, 0), 0.12, new Material(C4.Gray), DrawingMode.Surfaces));

        glControlAxes.DeleteAllObjects();
        glControlAxes.AddObjects(obj);
    }
    #endregion

    #region �����\�����Z�b�e�B���O (SetGLObjects)

    /// <summary>
    /// �����\�����Z�b�e�B���O
    /// </summary>
    /// <param name="_crystal">null�łȂ��ꍇ�́ABounds �� Lattice planes �Ɋւ���R���g���[�������Z�b�g�����</param>
    public void SetGLObjects(Crystal _crystal = null)
    {
        if (skipSetCrystal) return;

        textBoxCalcInformation.Clear();

        var sw = new Stopwatch();
        sw.Start();

        Crystal = _crystal ?? formMain.Crystal;

        if (_crystal != null)
        {
            atomCoordinateTable1.Crystal = Crystal;
            textBoxCalcInformation.AppendText($"Calculate coordinates table: {sw.ElapsedMilliseconds} ms.\r\n");
        }

        GLObjects.Clear(); //GLObjects��������

        initAxesMatrix(); //�������}�g���b�N�X��������

        setUnitCellPlanes();//�P�ʊi�q�ʂ̕`��

        initBounds();//���E������������;

        SetLatticePlanes();//�i�q�ʃI�u�W�F�N�g�𐶐�

        setBoundPlanes();//���E�ʃI�u�W�F�N�g�𐶐�

        if (_crystal != null)
        {
            SetLegend();
            SetAtomsP();
            setCheckBoxShowLabelState();
        }

        setAtoms();//���q�I�u�W�F�N�g�𐶐�

        setBondsAndPolyhera();//�����Ƒ��ʑ̃I�u�W�F�N�g�𐶐�

        setLabel();//���x���𐶐�

        removeObjects();//�]�v�Ȍ��q���폜

        toolStripLabelStatusInitialization.Text = GLObjects.Count + " objects were created (" + sw.ElapsedMilliseconds + " ms)";

        transferGLObjects(); //

        setAxesControl();//��������\������GLControl

        Draw();
    }
    #endregion

    #region Draw
    /// <summary>
    /// OpenGL�ɂ��`����s���B
    /// </summary>
    public void Draw()
    {
        var sw = new Stopwatch();
        sw.Start();

        var world = formMain.Crystal.RotationMatrix.Transpose();
        //WorldMatrix���������玩����Render()���s����
        glControlMain.WorldMatrixEx = world;
        glControlAxes.WorldMatrixEx = world;

        toolStripLabelStatusRendering.Text = $"Rendering time: {sw.ElapsedMilliseconds} ms.";
    }


    #endregion Draw

    #region �}�E�X�C�x���g

    private Point lastPosMain;
    private Point lastPosLight;
    private Point lastPosAxes;

    //Point animatitonStartPt, animationEndPt;

    /// <summary>
    /// �}�E�X�h���b�O�ŉ�]
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
            if (double.IsNaN(rotMat.M11)) return;
            var pos = rotMat.Mult(glControlLight.LightPosition);
            if (double.IsNaN(pos.X)) return;
            glControlLight.LightPosition = glControlMain.LightPosition = glControlAxes.LightPosition = pos;
            foreach (var c in legendControls)
                c.LightPosition = glControlLight.LightPosition;
        }
        lastPosLight = new Point(e.X, e.Y);
    }

    private static V4 getRotation(MouseEventArgs e, Size size, Point lastPos, bool ignoreZRotation)
    {
        double dx = e.X - lastPos.X, dy = lastPos.Y - e.Y;
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
    /// �s�N�`���[�{�b�N�X���N���b�N�����Ƃ����q�̔z�ʊ��Ȃǂ�\������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void glControlMain_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && e.Clicks == 2)
        {
            var index = SearchAtom(e.X, e.Y);
            if (index > 0)
            {
                tabControl.SelectedIndex = 6;

                List<string> result = [];
                var atom = GLObjects[index] as Sphere;
                var label = enabledAtoms[(atom.Tag as atomID).Index].Label.TrimStart().TrimEnd();

                result.Add($"\r\n--------------------------------------------------------------\r\n");
                result.Add($"Selected Atom: {label} ({atom.Origin.X * 10:f3}, {atom.Origin.Y * 10:f3}, {atom.Origin.Z * 10:f3}) \t�E�E�E0\r\n\r\n");

                //�ŋߐڌ��q��T��
                var tmp = Crystal.Search(enabledAtoms[(atom.Tag as atomID).Index], 1);
                for (int i = 0; i < tmp.Count - 1; i++)
                    if (tmp[i].X == tmp[i + 1].X && tmp[i].Y == tmp[i + 1].Y && tmp[i].Z == tmp[i + 1].Z)
                        tmp.RemoveAt(i--);


                tmp = tmp.Select(t => (t.X * 10, t.Y * 10, t.Z * 10, t.Distance * 10, t.Label.TrimStart().TrimEnd())).ToList();
                result.Add("(The following coordinates are relative to the selected atom)\r\n");

                int n = 1;//0�Ԗڂ͎������g�Ȃ̂ŁA1����͂��߂�
                int count = 0;//�����p�����߂�����
                for (int i = 1; n < 9 || i < 3; i++)
                {
                    result.Add($"{Miscellaneous.Ordinal(i)} nearest: {tmp[n].Distance:f3} ��");
                    do
                    {
                        result.Add($"\t{tmp[n].Label} ({tmp[n].X:f3}, {tmp[n].Y:f3}, {tmp[n].Z:f3}) \t�E�E�E{n} \r\n");
                        n++;
                    } while (tmp[n].Distance - tmp[n - 1].Distance < 1E-4);

                    if (count == 0 || tmp[n - 1].Distance / tmp[1].Distance < 1.2)
                        count = n;
                }

                for (int i = 1; i < result.Count; i++)
                    if (result[i - 1].StartsWith('\t') && result[i].StartsWith('\t'))
                        result[i] = result[i].Insert(0, "\t\t");

                result.Add($"\r\n");
                result.Add($"Bond angles");
                for (int i = 1; i < Math.Max(count, 3); i++)
                {
                    for (int j = i + 1; j < Math.Max(count, 3); j++)
                    {
                        var angle = Vector3DBase.AngleBetVectors(new Vector3DBase(tmp[i].X, tmp[i].Y, tmp[i].Z), new Vector3DBase(tmp[j].X, tmp[j].Y, tmp[j].Z));
                        result.Add(i == 1 && j == 2 ? "\t\t" : "\t\t\t");
                        result.Add($"{tmp[i].Label}({i})-{label}(0)-{tmp[j].Label}({j}): {angle / Math.PI * 180:f2}��\r\n");
                    }
                }

                foreach (var res in result)
                    textBoxAtomInformation.AppendText(res);

                glControlMain.Render();
            }

        }
        #region �������� ?

        /*
        //���q�I��
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
                    "  Pos.: " + "(" + a[0].position.X.ToString("f3") + "," + a[0].position.Y.ToString("f3") + "," + a[0].position.Z.ToString("f3") + ")[��] " +
                    "(" + a[0].positionRatio.X.ToString("f3") + "," + a[0].positionRatio.Y.ToString("f3") + "," + a[0].positionRatio.Z.ToString("f3") + ")[Cell]\r\n";
                if (a[1] != null)
                    str += "Atom 2:  " + "label: " + a[1].Label + "  element: " + a[1].element + "  ID: " + a[1].MainID.ToString() + "-" + a[1].SubID.ToString() +
                    "  Pos.: " + "(" + a[1].position.X.ToString("f3") + "," + a[1].position.Y.ToString("f3") + "," + a[1].position.Z.ToString("f3") + ")[��] " +
                    "(" + a[1].positionRatio.X.ToString("f3") + "," + a[1].positionRatio.Y.ToString("f3") + "," + a[1].positionRatio.Z.ToString("f3") + ")[Cell]\r\n";
                if (a[2] != null)
                    str += "Atom 3:  " + "label: " + a[2].Label + "  element: " + a[2].element + "  ID: " + a[2].MainID.ToString() + "-" + a[2].SubID.ToString() +
                    "  Pos.: " + "(" + a[2].position.X.ToString("f3") + "," + a[2].position.Y.ToString("f3") + "," + a[2].position.Z.ToString("f3") + ")[��] " +
                    "(" + a[2].positionRatio.X.ToString("f3") + "," + a[2].positionRatio.Y.ToString("f3") + "," + a[2].positionRatio.Z.ToString("f3") + ")[Cell]\r\n";

                if (a[2] != null)
                {
                    str += "Distance[��]:" +
                        "     Atom 1-2:  " + ((Vector3D)(a[0].position - a[1].position)).Length().ToString("f4") +
                        "     Atom 2-3:  " + ((Vector3D)(a[1].position - a[2].position)).Length().ToString("f4") +
                        "     Atom 3-1:  " + ((Vector3D)(a[2].position - a[0].position)).Length().ToString("f4") + "\r\n";
                    str += "Angle[��]:" +
                        "     Atom 1-2-3:  " + (Vector3D.AngleBetVectors(a[0].position - a[1].position, a[2].position - a[1].position) / Math.PI * 180).ToString("f4") +
                        "     Atom 2-3-1:  " + (Vector3D.AngleBetVectors(a[1].position - a[2].position, a[0].position - a[2].position) / Math.PI * 180).ToString("f4") +
                        "     Atom 3-1-2:  " + (Vector3D.AngleBetVectors(a[2].position - a[0].position, a[1].position - a[0].position) / Math.PI * 180).ToString("f4");
                }
                else if (a[1] != null)
                    str += "Distance[��]:" + "     Atom 1 to 2:  " + ((Vector3D)(a[0].position - a[1].position)).Length().ToString("f4") + "\r\n";

                textBoxInformation.Text = str;
            }
            Draw();
        }
        */
        #endregion
    }

    private int SearchAtom(int X, int Y)
    {
        //�u�􉽊w�v�t�H���_�́u�����Ɠ_�̋���.docx�v���Q��
        var ex = 2.0 * X / glControlMain.ClientSize.Width - 1;
        var ey = 1 - 2.0 * Y / glControlMain.ClientSize.Height;

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
                    if (!depthList.ContainsKey(origin.Z))
                        depthList.Add(origin.Z, i);
            }

        return depthList.Count != 0 ? depthList.Last().Value : -1;

    }

    #endregion �}�E�X�C�x���g

    #region Unit cell �^�u�֘A
    private void unitCell_PropertyChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;

        groupBoxShowUnitCell.Enabled = checkBoxUnitCell.Checked;
        setUnitCellPlanes();
        Draw();
    }
    #endregion

    #region �C���[�W�ۑ�or�R�s�[

    // �C���[�W��ۑ�����
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

    #endregion �C���[�W�ۑ�or�R�s�[

    #region toolStripButton ���C�g�A�������A�}��A�u�[�X�g
    private void toolStripButtonCrystalAxes_CheckedChanged(object sender, EventArgs e) => glControlAxes.Visible = toolStripButtonCrystalAxes.Checked;

    private void toolStripButtonLightingBall_CheckedChanged(object sender, EventArgs e) => glControlLight.Visible = toolStripButtonLightDirection.Checked;

    private void toolStripButtonLegend_CheckedChanged(object sender, EventArgs e) => SetLegend();

    #endregion

    #region ����֘A

    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        /*
        System.Drawing.Printing.PageSettings ps = printDocument1.PrinterSettings.DefaultPageSettings;
        //�p���T�C�Y�擾 ���̃T�C�Y��1/100�C���`
        float height = (ps.PaperSize.Height - ps.Margins.Top - ps.Margins.Bottom) / 100f;
        float width = (ps.PaperSize.Width - ps.Margins.Left - ps.Margins.Right) / 100f;

        if (printDocument1.PrinterSettings.DefaultPageSettings.Landscape)
        {//�c�����t�]
            float temp = width; width = height; height = temp;
        }
        //�𑜓x300dpi�̂Ƃ��̃C���[�W�T�C�Y��
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
        // ����v���r���[��\��
        printPreviewDialog1.ShowDialog();

    private void printToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (printDialog1.ShowDialog() == DialogResult.OK)
            printDocument1.Print();
    }

    #endregion ����֘A

    #region Bounds(���E)�@�֘A�@�C�x���g
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
    /// ���E�ʂ̃R���g���[���ɕω����������Ƃ�
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BoundControl_BoundsChanged(object sender, EventArgs e)
    {
        if (SkipEvent)
            return;
        if (formMain.crystalControl.SkipEvent)//crystalControl���X�V���̎��̕ύX�̓L�����Z��
            return;
        SetGLObjects(null);
    }

    #endregion

    #region LatticePlane �֘A�@�C�x���g
    private void LatticePlaneControl_LatticePlaneChanged(object sender, EventArgs e)
    {
        if (SkipEvent)
            return;
        if (formMain.crystalControl.SkipEvent)//crystalControl���X�V���̎��̕ύX�̓L�����Z��
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

    #region Bond �֘A�C�x���g
    private void BondControl_BondsChanged(object sender, EventArgs e)
    {
        if (SkipEvent || formMain.crystalControl.SkipEvent)//crystalControl���X�V���̎��̕ύX�̓L�����Z��
            return;
        SetGLObjects(null);
    }

    #endregion

    #region Atom �R���g���[���֘A�C�x���g
    private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        => MoveAtomControl(tabControl.SelectedTab == tabPageAtom);
    private void MoveAtomControl(bool flag)
    {
        if (flag)
        {
            TopMost = true;
            atomControl.DebyeWallerTabVisible = atomControl.ScatteringFactorTabVisible = false;
            atomControl.AppearanceTabVisible = true;
            atomControl.dataGridView.Columns["enabledColumn"].Visible = true;
            formMain.crystalControl.tabPageAtom.Controls.Add(labelMessage);
            tabPageAtom.Controls.Add(atomControl);
            TopMost = false;
        }
        else
        {
            atomControl.DebyeWallerTabVisible = atomControl.ScatteringFactorTabVisible = true;
            atomControl.AppearanceTabVisible = false;
            atomControl.dataGridView.Columns["enabledColumn"].Visible = false;

            tabPageAtom.Controls.Add(labelMessage);
            formMain.crystalControl.tabPageAtom.Controls.Add(atomControl);
        }
    }

    private void AtomControl_ItemsChanged(object sender, EventArgs e)
    {
        //AtomControl��ItemsChanged�C�x���g�͏E��Ȃ��ėǂ��B
        //CrystalControl�ł��̃C�x���g���E���AGenerateFromInterface()���ꂽ���ƁA
        //FormMain����ʒm������B
    }
    private void AtomControl_GLEnableChanged(object sender, EventArgs e)
    {
        if (SkipEvent || formMain.crystalControl.SkipEvent)//crystalControl���X�V���̎��̕ύX�̓L�����Z��
            return;
        SetGLObjects(formMain.crystalControl.Crystal);
    }
    #endregion

    #region �}��̕`��
    /// <summary>
    /// �}�ᕔ���̕`��
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

            var atoms = atomControl.GetAll().Where(a => a.GLEnabled).ToList();
            if (atoms.Count == 0)
                return;
            if (atoms.Count > 40 && !checkBoxGroupByElement.Checked)
            {
                checkBoxGroupByElement.Checked = true;
                return;
            }

            if (checkBoxGroupByElement.Checked)
            {
                foreach (var num in atoms.Select(a => a.AtomicNumber).Distinct().ToList())
                    while (atoms.Count(a => a.AtomicNumber == num) > 1)
                        atoms.Remove(atoms.First(a => a.AtomicNumber == num));
            }

            //�R���g���[��������Ȃ�������ǉ�
            if (legendControls.Count < atoms.Count)
                for (int i = legendControls.Count; i < atoms.Count; i++)
                {
                    //GLControlAlpha�쐬
                    legendControls.Add(new GLControlAlpha
                    {
                        AllowMouseRotation = false,
                        AllowMouseScaling = false,
                        AllowMouseTranslating = false,
                        Name = $"legend{i}",
                        ProjWidth = 2.2D,
                        LightPosition = glControlLight.LightPosition,
                        WorldMatrix = glControlLight.WorldMatrix,
                        ViewFrom = glControlLight.ViewFrom
                    });
                    legendControls[i].SkipRendering = true;
                    legendControls[i].MouseDown += legendControl_MouseClick;

                    //Label�쐬
                    legendLabels.Add(new Label { Font = Font, AutoSize = true });
                    //FlowLayoutPanel�쐬

                    legendPanels.Add(new FlowLayoutPanel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, FlowDirection = FlowDirection.TopDown, Margin = new Padding(1, 1, 1, 8), });
                    legendPanels[i].Controls.AddRange([legendControls[i], legendLabels[i]]);

                }
            //�ǉ������܂�

            glControlMain.SkipRendering = true;
            flowLayoutPanelLegend.SuspendLayout();

            //�\������Ă���R���g���[��������������폜
            while (atoms.Count < flowLayoutPanelLegend.Controls.Count)
                flowLayoutPanelLegend.Controls.RemoveAt(atoms.Count);

            var maxRadius = atoms.Max(a => a.Radius);
            //�}���ݒ�
            for (int i = 0; i < atoms.Count; i++)
            {
                var a = atoms[i];

                legendLabels[i].Text = checkBoxGroupByElement.Checked ? $"{a.AtomicNumber}: {AtomStatic.AtomicName(a.AtomicNumber)}" : a.Label;
                legendLabels[i].Margin = new Padding((size.Width - legendLabels[i].Size.Width) / 2, 0, 0, 0);

                legendControls[i].SkipRendering = true;
                legendControls[i].Tag = legendLabels[i].Text;
                legendControls[i].DeleteAllObjects();
                if (legendControls[i].Size != size)
                    legendControls[i].Size = size;
                legendControls[i].AddObjects(new Sphere(new V3(0, 0, 0), a.Radius / maxRadius, new Material(a.Argb, a.Texture), DrawingMode.Surfaces));
                legendControls[i].SkipRendering = false;
                legendControls[i].Render();

                legendControls[i].ToolTip = $"Legend for {legendLabels[i].Text}\r\n  Click to show/hide the label";

                if (flowLayoutPanelLegend.Controls.Count <= i)
                    flowLayoutPanelLegend.Controls.Add(legendPanels[i]);
            }

            flowLayoutPanelLegend.ResumeLayout();
            glControlMain.SkipRendering = false;
        }

        textBoxCalcInformation.AppendText($"Generation of legend control: {sw.ElapsedMilliseconds}ms.\r\n");

        SkipEvent = true;
        numericBoxClientWidth.Value = glControlMain.ClientSize.Width;
        numericBoxClientHeight.Value = glControlMain.ClientSize.Height;
        SkipEvent = false;
    }

    private void legendControl_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            var s = (sender as GLControlAlpha).Tag as string;

            var list = enabledAtoms.Where(a => checkBoxGroupByElement.Checked ? a.ElementName == s : a.Label == s).ToList();

            if (list.Count != 0)
            {
                var showLabel = !list[0].ShowLabel;
                list.ForEach(a => a.ShowLabel = showLabel);
                SetGLObjects();
            }
            setCheckBoxShowLabelState();
        }
    }
    #endregion

    #region �}��A���������A�������̃T�C�Y�ύX
    private void numericBoxLegendSize_ValueChanged(object sender, EventArgs e) => SetLegend();

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

    #region Vesta�Ɠ����悤�Ȍ��h���ɂ���B
    private void toolStripButtonLikeVesta_Click(object sender, EventArgs e)
    {
        skipSetCrystal = true;

        foreach (var atoms in formMain.crystalControl.Crystal.Atoms)
            atoms.ResetVesta();

        formMain.crystalControl.bondControl.Clear();
        ConvertCrystalData.SetOpenGL_property(formMain.crystalControl.Crystal);
        formMain.crystalControl.bondControl.AddRange(formMain.crystalControl.Crystal.Bonds);
        skipSetCrystal = false;
        SetGLObjects(formMain.crystalControl.Crystal);
    }
    #endregion

    #region �`��i��������
    private void comboBoxRenderignQuality_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxRenderignQuality.SelectedIndex == 0)
        {
            Cone.Default = (1, 16);
            Cylinder.Default = (1, 16);
            Sphere.DefaultSlices = 3;
        }
        else if (comboBoxRenderignQuality.SelectedIndex == 1)
        {
            Cone.Default = (1, 24);
            Cylinder.Default = (1, 24);
            Sphere.DefaultSlices = 4;
        }
        else
        {
            Cone.Default = (1, 48);
            Cylinder.Default = (1, 48);
            Sphere.DefaultSlices = 6;
        }
        if (atomControl != null)
            SetGLObjects(formMain.crystalControl.Crystal);
    }


    private void comboBoxTransparency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((comboBoxTransparency.SelectedIndex == 0 && glControlMainZsort.Visible) || (comboBoxTransparency.SelectedIndex == 1 && glControlMainOIT.Visible))
            return;//�ύX�����������牽�����Ȃ��B

        if (comboBoxTransparency.SelectedIndex == 1 && !GLControlAlpha.OitEnabled)
        {
            MessageBox.Show("OIT (order independent transparency) mode requires OpenGL 4.3 or later,\r\n" +
                " but the current version is " + GLControlAlpha.VersionStr + ". Sorry.", "Caution!");
            comboBoxTransparency.SelectedIndex = 0;
            return;
        }

        var swap = new Action<GLControlAlpha, GLControlAlpha>((g1, g2) =>
        {
            g1.ViewFrom = g2.ViewFrom;
            g1.ViewMatrix = g2.ViewMatrix;
            g1.WorldMatrix = g2.WorldMatrix;
            g1.ProjMatrix = g2.ProjMatrix;
            g1.LightPosition = g2.LightPosition;
            g1.ProjWidth = g2.ProjWidth;
            g1.ProjCenter = g2.ProjCenter;
            g1.ProjectionMode = g2.ProjectionMode;
        });

        var gNew = comboBoxTransparency.SelectedIndex == 0 ? glControlMainZsort : glControlMainOIT;
        var gOld = comboBoxTransparency.SelectedIndex == 0 ? glControlMainOIT : glControlMainZsort;

        gOld.Visible = false;
        splitContainer1.Panel1.Controls.Remove(gOld);

        gNew.DeleteAllObjects();
        gOld.DeleteAllObjects();

        swap(gNew, gOld);

        glControlMain = gNew;
        splitContainer1.Panel1.Controls.Add(gNew);
        flowLayoutPanelLegend.SendToBack();
        gNew.Visible = true;

        checkBoxDepthCueing_CheckedChanged(sender, e);

        if (atomControl != null)
            SetGLObjects(formMain.crystalControl.Crystal);
    }

    #endregion

    #region ProjectionMode (Perspective���AOrhographic��)�Ȃǂ̐ݒ�
    private void comboBoxProjectionMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        trackBarPerspective.Enabled = comboBoxProjectionMode.SelectedIndex == 1;

        glControlMain.ProjectionMode = comboBoxProjectionMode.SelectedIndex == 0 ?
                GLControlAlpha.ProjectionModes.Orhographic : GLControlAlpha.ProjectionModes.Perspective;
    }

    private void trackBarPerspective_Scroll(object sender, EventArgs e)
    {
        var x = Math.Pow(51.0, 1.0 / 100.0);
        glControlMain.SetPerspectiveDistance(Math.Pow(x, trackBarPerspective.Value) - 1);
    }

    #endregion

    #region Depth cueing�̐ݒ�
    private void checkBoxDepthCueing_CheckedChanged(object sender, EventArgs e)
    {
        groupBoxDepthCueing.Enabled = checkBoxDepthFadingOut.Checked;
        //�Ȃ����X�V����Ȃ����Ƃ�����̂ŁA2����s����
        glControlMain.DepthCueing = (checkBoxDepthFadingOut.Checked, trackBarAdvancedDepthCueingFar.Value / 10.0, trackBarAdvancedDepthCueingNear.Value / 10.0);
        glControlMain.DepthCueing = (checkBoxDepthFadingOut.Checked, trackBarAdvancedDepthCueingFar.Value / 10.0, trackBarAdvancedDepthCueingNear.Value / 10.0);
    }
    private bool trackBarAdvanced2_ValueChanged(object sender, double value)
    {
        checkBoxDepthCueing_CheckedChanged(sender, new EventArgs());
        return false;
    }


    #endregion

    #region ���x���̐F��t�H���g�T�C�Y�֘A
    private void numericBoxLabelSize_ValueChanged(object sender, EventArgs e)
    {
        colorControlLabelColor.Enabled = radioButtonLabelUseFixedColor.Checked;
        SetGLObjects();
    }

    private void checkBoxShowLabel_CheckedChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        if (enabledAtoms != null && enabledAtoms.Length != 0)
        {
            foreach (var a in enabledAtoms)
                a.ShowLabel = checkBoxShowLabel.Checked;
            SetGLObjects();
        }
    }

    private void setCheckBoxShowLabelState()
    {
        if (enabledAtoms == null || enabledAtoms.Length == 0)
            return;

        SkipEvent = true;
        if (enabledAtoms.All(e => e.ShowLabel))
            checkBoxShowLabel.CheckState = CheckState.Checked;
        else if (enabledAtoms.All(e => !e.ShowLabel))
            checkBoxShowLabel.CheckState = CheckState.Unchecked;
        else
            checkBoxShowLabel.CheckState = CheckState.Indeterminate;
        SkipEvent = false;
    }


    #endregion

    #region ���̑��C�x���g


    private void FormStructureViewer_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.Shift && e.KeyCode == Keys.C)
            Clipboard.SetDataObject(glControlMain.GenerateBitmap());
    }

    /// <summary>
    /// �p�x�����Z�b�g����
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void toolStripButtonResetRotation_Click(object sender, EventArgs e) => formMain.SetRotation(new Matrix3D());

    private void FormStructureViewer_ResizeBegin(object sender, EventArgs e)
    {
        SuspendLayout();
    }

    private void FormStructureViewer_ResizeEnd(object sender, EventArgs e)
    {
        ResumeLayout();

        SkipEvent = true;
        numericBoxClientWidth.Value = glControlMain.ClientSize.Width;
        numericBoxClientHeight.Value = glControlMain.ClientSize.Height;
        SkipEvent = false;
    }
    private void splitContainer1_Panel1_ClientSizeChanged(object sender, EventArgs e)
    {

    }

    private void numericBoxClientWidth_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        SkipEvent = true;

        var currentWidth = glControlMain.ClientSize.Width;
        var currentHeight = glControlMain.ClientSize.Height;

        this.Size = new Size(this.Size.Width + numericBoxClientWidth.ValueInteger - currentWidth, this.Size.Height + numericBoxClientHeight.ValueInteger - currentHeight);

        numericBoxClientWidth.Value = glControlMain.ClientSize.Width;
        numericBoxClientHeight.Value = glControlMain.ClientSize.Height;

        SkipEvent = false;

    }

    #endregion ���̑��C�x���g

    #region ����ۑ�
    private void SaveMovieMainImageToolStripMenuItem_Click(object sender, EventArgs e)
    {
        formMain.FormMovie.Execute((sender as ToolStripMenuItem).Name.Contains("MainImage") ? glControlMain : glControlAxes, this);
    }
    #endregion

    #region ���e���S
    private void radioButtonScreenCenter1_CheckedChanged(object sender, EventArgs e)
    {
        flowLayoutPanelProjectionCenter.Enabled = radioButtonProjectionCenterCustom.Checked;

        SkipEvent = true;
        if (radioButtonProjectionCenter1.Checked)
            numericBoxProjectionCenterX.Value = numericBoxProjectionCenterY.Value = numericBoxProjectionCenterZ.Value = 0;
        else if (radioButtonProjectionCenter2.Checked)
            numericBoxProjectionCenterX.Value = numericBoxProjectionCenterY.Value = numericBoxProjectionCenterZ.Value = 0.5;
        SkipEvent = false;

        numericBoxProjectionCenterX_ValueChanged(sender, e);
    }

    private void numericBoxProjectionCenterX_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;

        SkipEvent = true;
        numericBoxCellTransrationA.Value = 0.5 - numericBoxProjectionCenterX.Value;
        numericBoxCellTransrationB.Value = 0.5 - numericBoxProjectionCenterY.Value;
        numericBoxCellTransrationC.Value = 0.5 - numericBoxProjectionCenterZ.Value;
        SkipEvent = false;

        SetGLObjects();
    }
    #endregion


}

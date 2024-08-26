using Crystallography.OpenGL;
using ImagingSolution.Control;
using IronPython.Runtime;
using OpenTK;
using OpenTK.Graphics;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ReciPro;
using V3 = OpenTK.Vector3d;
public partial class FormEBSD : Form
{
    public FormMain FormMain;

    private GLControlAlpha glControlGeometry;
    private GLControlAlpha glControlTrajectory;

    #region コンストラクタ、Load, Closing
    public FormEBSD()
    {
        InitializeComponent();
    }

    private void FormEBSD_Load(object sender, EventArgs e)
    {
        glControlGeometry = new GLControlAlpha(GLControlAlpha.FragShaders.OIT)
        {
            AllowMouseRotation = true,
            AllowMouseScaling = true,
            AllowMouseTranslating = false,
            Name = "glControlAxes",
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 8.0,
            RotationMode = GLControlAlpha.RotationModes.Object,
            Dock = DockStyle.Fill,
            LightPosition = new V3(100, 100, 100),
            BorderStyle = BorderStyle.Fixed3D,

            WorldMatrix = Matrix4d.CreateRotationZ(-Math.PI / 8) * Matrix4d.CreateRotationX(-0.4 * Math.PI)
        };
        panelGeometry.Controls.Add(glControlGeometry);

        glControlTrajectory = new GLControlAlpha()
        {
            AllowMouseRotation = true,
            AllowMouseScaling = true,
            AllowMouseTranslating = false,
            Name = "glControlAxes",
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 8.0,
            RotationMode = GLControlAlpha.RotationModes.Object,
            Dock = DockStyle.Fill,
            LightPosition = new V3(100, 100, 100),
            BorderStyle = BorderStyle.Fixed3D,

            WorldMatrix = Matrix4d.CreateRotationX(0.5 * Math.PI)
        };
        paneltTrajectory.Controls.Add(glControlTrajectory);


        DrawGeometry();
        DrawTrajectory();
    }

   

    private void FormEBSD_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonEBSD.Checked = false;
        this.Visible = false;
    }
    #endregion

    private void button1_Click(object sender, EventArgs e)
    {
        DrawTrajectory();
        DrawGeometry();
    }

    private void buttonViewIsometric_Click(object sender, EventArgs e) 
        => glControlGeometry.WorldMatrix = Matrix4d.CreateRotationZ(-Math.PI / 8) * Matrix4d.CreateRotationX(-0.4 * Math.PI);
    private void buttonViewAlongBeam_Click(object sender, EventArgs e) => glControlGeometry.WorldMatrix = Matrix4d.Identity;

    private void numericBoxSampleTilt_ValueChanged(object sender, EventArgs e) => DrawGeometry();


    #region 入射電子、試料、検出器の幾何学を3Dで表示
    /// <summary>
    /// 試料と電子線が交差する位置は常に(0,0,0)
    /// </summary>
    public void DrawGeometry()
    {
        var glObjects = new List<GLObject>();

        //試料の傾き
        var rot = Matrix3D.RotY(numericBoxSampleTilt.RadianValue);

        //試料を示す直方体
        var sample = new Parallelepiped(rot * new V3(-1, -1, -0.2), rot * new V3(2, 0, 0), rot * new V3(0, 2, 0), rot * new V3(0, 0, 0.2), new Material(Color4.AliceBlue), DrawingMode.SurfacesAndEdges);
        glObjects.Add(sample);

        //検出器面
        var detector = new Parallelepiped(new V3(2.99, -1.5, -1.5), new V3(0, 3, 0), new V3(0, 0, 3), new V3(0.2, 0, 0), new Material(Color4.GreenYellow, 0.7), DrawingMode.Surfaces);
        glObjects.Add(detector);

        //ポールピース
        glObjects.AddRange(
            [
            new Pipe(new V3(0,0,2), new V3(0,0,1), 0.15,0.16,new Material(Color4.Gray), DrawingMode.Surfaces,false){ IgnoreNormalSides=true},
            new Pipe(new V3(0,0,2), new V3(0,0,1), 0.30,1.5,new Material(Color4.Gray), DrawingMode.Surfaces,false){ IgnoreNormalSides=true},
            new HoledDisk(new V3(0,0,2), new V3(0,0,1),0.15,0.3, new Material(Color4.Gray),    DrawingMode.Surfaces){ IgnoreNormalSides=true},
            new HoledDisk(new V3(0,0,3), new V3(0,0,1),0.15,1.5, new Material(Color4.Gray),    DrawingMode.Surfaces){ IgnoreNormalSides=true},
            ]);

        //電子線方向を示す矢印
        glObjects.AddRange(
            [
                new Cone(new V3(0, 0, 0), new V3(0, 0, 2.5), 0.1, new Material(Color4.Yellow,0.7), DrawingMode.Surfaces){ IgnoreNormalSides=true},
                new Cone(new V3(0, 0, 0), new V3(3, 0, 0), 1, new Material(Color4.Yellow, 0.7), DrawingMode.Surfaces) { IgnoreNormalSides = true }
            ]);

        //結晶のa, b, c軸を表す矢印

        glControlGeometry.DeleteAllObjects();
        glControlGeometry.AddObjects(glObjects);
        glControlGeometry.Refresh();
    }
    #endregion

    #region 試料内で電子が散乱する様子をモンテカルロシミュレーション

    private void DrawTrajectory()
    {
        var glObjects = new List<GLObject>();

        glObjects.Add(new Quads(new V3(5, 5, 0), new V3(-5, 5, 0), new V3(-5, -5, 0), new V3(5, -5, 0), new Material(Color4.AliceBlue,0.7), DrawingMode.Surfaces));


        glControlTrajectory.DeleteAllObjects();
        glControlTrajectory.AddObjects(glObjects);
        glControlTrajectory.Refresh();
    }


    private (V3 p, double e)[] MonteCarlo()
    {

        return null;
    }

    #endregion


}


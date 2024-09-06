using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crystallography;
using Crystallography.Controls;
using Crystallography.OpenGL;
using OpenTK;
using V3 = OpenTK.Vector3d;
using C4 = OpenTK.Graphics.Color4;

namespace ReciPro;

public partial class FormEBSD : Form
{
    #region フィールド、プロパティ
    public FormMain FormMain;
    public GLControlAlpha glControlGeometry;

    #endregion

    #region コンストラクタ、ロード、クローズ
    public FormEBSD()
    {
        InitializeComponent();

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

            WorldMatrix =/* Matrix4d.CreateRotationZ(-Math.PI / 8) * */Matrix4d.CreateRotationX(-0.5 * Math.PI)
        };
        panelGeometry.Controls.Add(glControlGeometry);

    }

    private void FormEBSD_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonEBSD.Checked = false;
        Visible = false;
    }
    #endregion

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
            new Pipe(new V3(0,0,2), new V3(0,0,1), 0.15,0.16,new Material(C4.Gray), DrawingMode.Surfaces,false){ IgnoreNormalSides=true},
            new Pipe(new V3(0,0,2), new V3(0,0,1), 0.30,1.5,new Material(C4.Gray), DrawingMode.Surfaces,false){ IgnoreNormalSides=true},
            new HoledDisk(new V3(0,0,2), new V3(0,0,1),0.15,0.3, new Material(C4.Gray),    DrawingMode.Surfaces){ IgnoreNormalSides=true},
            new HoledDisk(new V3(0,0,3), new V3(0,0,1),0.15,1.5, new Material(C4.Gray),    DrawingMode.Surfaces){ IgnoreNormalSides=true},
            ]);

        //電子線方向を示す矢印
        glObjects.AddRange(
            [
                new Cone(new V3(0, 0, 0), new V3(0, 0, 2.5), 0.1, new Material(C4.Yellow,0.7), DrawingMode.Surfaces){ IgnoreNormalSides=true},
                new Cone(new V3(0, 0, 0), new V3(3, 0, 0), 1, new Material(C4.Yellow, 0.7), DrawingMode.Surfaces) { IgnoreNormalSides = true }
            ]);

        //結晶のa, b, c軸を表す矢印

        glControlGeometry.DeleteAllObjects();
        glControlGeometry.AddObjects(glObjects);
        glControlGeometry.Refresh();
    }
    #endregion

}

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
using V4 = OpenTK.Vector4d;
using M3 = OpenTK.Matrix3d;
using C4 = OpenTK.Graphics.Color4;
using System.Diagnostics;
using OpenTK.Graphics;
using static IronPython.Modules._ast;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static IronPython.SQLite.PythonSQLite;

namespace ReciPro;

public partial class FormEBSD : Form
{
    #region フィールド、プロパティ
    public FormMain FormMain;
    public GLControlAlpha glControlGeo;
    private Stopwatch sw = new();
    #endregion

    #region コンストラクタ、ロード、クローズ
    public FormEBSD()
    {
        InitializeComponent();

        glControlGeo = new GLControlAlpha()
        {
            AllowMouseRotation = true,
            AllowMouseScaling = true,
            AllowMouseTranslating = false,
            Name = "glControlAxes",
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 200.0,
            RotationMode = GLControlAlpha.RotationModes.Object,
            Dock = DockStyle.Fill,
            LightPosition = new V3(100, 100, 100),
            BorderStyle = BorderStyle.Fixed3D,

            WorldMatrix = Matrix4d.CreateRotationY(-Math.PI / 2) * Matrix4d.CreateRotationZ(-Math.PI / 2),
        };
        panelGeometry.Controls.Add(glControlGeo);

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
        var samRot = Matrix3D.RotX(numericBoxSampleTilt.RadianValue);
        //試料を示す直方体
        var sample = new Parallelepiped(samRot * new V3(-15, -15, -2), samRot * new V3(30, 0, 0), samRot * new V3(0, 30, 0), samRot * new V3(0, 0, 2), new Material(C4.AliceBlue), DrawingMode.SurfacesAndEdges);
        glObjects.Add(sample);

        //検出器の傾き
        var detTilt = numericBoxDetectorTilt.RadianValue;
        var detR = numericBoxDetRadius.Value;
        double detY = numericBoxYofDet.Value, detZ = numericBoxZofDet.Value;
        var detector = new Cylinder(new V3(0, detY, detZ), new V3(0, -Math.Sin(detTilt), -Math.Cos(detTilt)), detR, new Material(C4.GreenYellow, 0.7), DrawingMode.Surfaces, true, 2, 180);
        glObjects.Add(detector);

        //各種スケール

        //if (checkBoxDrawAxes.Checked)
        {
            glControlGeo.MakeCurrent();
            var len = 50;
            //X軸
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(len, 0, 0)], 3f, new Material(C4.OrangeRed)));
            glControlGeo.MakeCurrent();
            glObjects.Add(new TextObject("+X", 10f, new V3(len, 0, 0), 100, true, new Material(C4.OrangeRed)));

            //Y軸
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, len, 0)], 3f, new Material(C4.YellowGreen)));
            glControlGeo.MakeCurrent();
            glObjects.Add(new TextObject("+Y", 10f, new V3(0, len, 0), 100, true, new Material(C4.YellowGreen)));

            //Z軸 = beam
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, 0, len)], 3f, new Material(C4.MediumPurple)));
            glControlGeo.MakeCurrent();
            glObjects.Add(new TextObject("+Z (=beam)", 10f, new V3(0, 0, len), 100, true, new Material(C4.MediumPurple)));
        }

        glObjects.AddRange( Enumerable.Range(0, 60).Select(e =>
        {
            var θ = e / 30.0 * Math.PI;
            var p = M3.CreateRotationX(detTilt).Mult(numericBoxDetRadius.Value* new V3(Math.Sin(θ), Math.Cos(θ), 0));
            return new Lines([new V3(0, 0, 0), new (p.X, p.Y + numericBoxYofDet.Value, p.Z + numericBoxZofDet.Value)], 1f, new Material(C4.Yellow, 0.7));
        }));

        //電子線方向を示す矢印
        glObjects.Add( new Cone(new V3(0, 0, 0), new V3(0, 0, 100), 5, new Material(C4.Yellow,0.7), DrawingMode.Surfaces){ IgnoreNormalSides=true});

        //結晶のa, b, c軸を表す矢印

        glControlGeo.DeleteAllObjects();
        glControlGeo.AddObjects(glObjects);
        glControlGeo.Refresh();
        //OpenGL描画ここまで

        //ステレオネット上に検出器の輪郭を描画
        M3 samRot2 = M3.CreateRotationX(numericBoxSampleTilt.RadianValue), detRot= M3.CreateRotationX(detTilt);
        var step = 60;
        var f = new Func<int, PointD>(i =>
        {
            var θ = 2.0*Math.PI * i / step;
            var p = detRot.Mult(detR * new V3(Math.Sin(θ), Math.Cos(θ), 0));
            return Stereonet.ConvertVectorToSchmidt(samRot2.Mult(p + new V3(0, detY, detZ)));
        });
        poleFigureControl.Lines = [(Enumerable.Range(0, step + 1).Select(e => f(e)).ToArray(), 2, Color.Red)];
        poleFigureControl.Draw();
        //ステレオネット上に検出器の輪郭を描画 ここまで

    }
    #endregion

    private void button2_Click(object sender, EventArgs e)
    {
        var cry = FormMain.Crystal;
        cry.GetFormulaAndDensity();
        var sum1 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity * a.AtomicNumber);
        var sum2 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity);
        var sum3 = cry.Atoms.Sum(a => a.Multiplicity);
        //試料の平均原子番号. 各元素の重量比で加重平均//double Z = 79;// 79 29 13;
        double Z = sum1 / sum2;
        //試料の平均原子量 (g/mol)
        double A = sum2 / sum3; //196.96 63.55 26.98;
        //試料の密度 (g/cm^3)
        double ρ = cry.Density; // 19.32 8.96 2.70 

        //入射電子のエネルギー (kev)
        double energy = waveLengthControl1.Energy;

        //サンプルの傾き
        double tilt = numericBoxSampleTilt.RadianValue, cosTilt = Math.Cos(tilt), sinTilt = Math.Sin(tilt);

        var monte = new MonteCarlo(Z, A, ρ, energy, tilt);

        //飛程計算ループ
        sw.Restart();
        var loop = 20_000;
        var bse = new (V3 pos, V3 vec, double e)[loop];
        Parallel.For(0, loop, i => bse[i] = monte.GetBackscatteredElectrons());

        toolStripStatusLabel1.Text = $"{sw.ElapsedMilliseconds} msec. ellapsed for {loop} backscattered electrons.";


        //ステレオネット描画
        if (radioButtonFrequency.Checked)
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Histogram;
        else if (radioButtonAverageEnergy.Checked)
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Average;
        else
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Sigma;
        M3 rot = M3.CreateRotationX(tilt);
        poleFigureControl.Vectors = bse.Where(e => e.e > monte.ThresholdKev).Select(e => new V4(rot.Mult(e.vec), e.e)).ToArray();
    }

    private void buttonViewFromZ_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = Matrix4d.Identity;

    private void buttonViewFromX_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = Matrix4d.CreateRotationY(-Math.PI / 2) * Matrix4d.CreateRotationZ(-Math.PI / 2);

    private void buttonFromSurfaceNormal_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = Matrix4d.CreateRotationX(-numericBoxSampleTilt.RadianValue);

    private void numericBoxDetRadius_ValueChanged(object sender, EventArgs e) => DrawGeometry();
}

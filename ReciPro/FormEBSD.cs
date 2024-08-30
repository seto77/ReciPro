using Crystallography.OpenGL;
using ImagingSolution.Control;
using IronPython.Runtime;
using Microsoft.Scripting.Utils;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReciPro;
using V3 = OpenTK.Vector3d;
public partial class FormEBSD : Form
{
    public FormMain FormMain;

    private GLControlAlpha glControlGeometry;
    private GLControlAlpha glControlTrajectory;

    int seed = 0;
    object lockObj=new object();

    Stopwatch sw = new();

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

            WorldMatrix =/* Matrix4d.CreateRotationZ(-Math.PI / 8) * */Matrix4d.CreateRotationX(-0.5 * Math.PI)
        };
        panelGeometry.Controls.Add(glControlGeometry);

        glControlTrajectory = new GLControlAlpha()
        {
            AllowMouseRotation = true,
            AllowMouseScaling = true,
            AllowMouseTranslating = false,
            Name = "glControlAxes",
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 4.0,
            RotationMode = GLControlAlpha.RotationModes.Object,
            Dock = DockStyle.Fill,
            LightPosition = new V3(100, 100, 100),
            BorderStyle = BorderStyle.Fixed3D,
            DepthCueing = ( false,0,0),

            WorldMatrix = Matrix4d.CreateRotationX(-0.5 * Math.PI)
            
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
        var tilt = numericBoxSampleTilt.RadianValue;
        List<GLObject> glObjects = [
            new Disk(new V3(0,0,0),new V3(Math.Sin(tilt),0,Math.Cos(tilt)), 1, new Material(Color4.AliceBlue, 0.7), DrawingMode.Surfaces)
        ];

        var tan = Math.Tan(-numericBoxSampleTilt.RadianValue);
        var list = new List<(V3 p, double e)[]>();
        sw.Restart();
        Parallel.For(0, numericBoxCalcNum.ValueInteger, i =>//for (int i = 0; i < 10000; i++)
        {
            var trajectry = MonteCarlo();
            if (trajectry[^1].p.X * tan < trajectry[^1].p.Z)
                lock (lockObj)
                    list.Add(trajectry);
        });

        toolStripStatusLabel1.Text = $"{sw.ElapsedMilliseconds} ms ellapsed for {numericBoxCalcNum.ValueInteger} trajectories.";
      
        int colorDiv = 16;//16段階で色を変化させていく
        var initialEnergy = waveLengthControl1.Energy;
        for (int i = 0; i < list.Count && i < numericBoxDrawNum.ValueInteger; i++)
        {
            var trajectry = list[i];
            int start = 0, end=0;
            for (int j = colorDiv; j >= 1 && end< trajectry.Length; j--)
            {
                var mat = new Material(new Color4(255, (byte)(255 * (colorDiv - j) / colorDiv), (byte)(255 * (colorDiv - j) / colorDiv), (byte)(255 * j / colorDiv)));

                end = trajectry.FindIndex(t => t.e < (double)(j - 1) / colorDiv * initialEnergy);
                if (end == -1)
                    end = trajectry.Length;
                glObjects.Add(new Lines(trajectry[start..end].Select(e => e.p).ToArray(), 1f, mat));
             
                start = end - 1;
            }

            var r = trajectry[^2].e / waveLengthControl1.Energy;
            var v = (trajectry[^1].p - trajectry[^2].p).Normalized() * r;
            var matBackScattered = new Material(new Color4((byte)(255 * (1 - r)), (byte)(255 * (1 - r)), 255, (byte)(255 * r)));

            glObjects.Add(new Lines([trajectry[^2].p, trajectry[^2].p + v], 1f, matBackScattered));
        }

        glControlTrajectory.DeleteAllObjects();
        glControlTrajectory.AddObjects(glObjects);
        glControlTrajectory.Refresh();

        //エネルギー分布を描画 ここから
        double step = 0.2;//kev単位
        double lower = 2, upper = initialEnergy;
        var histogram = new MathNet.Numerics.Statistics.Histogram(list.Select(e => e[^1].e), (int)((initialEnergy - lower) / step), 2, initialEnergy);
        var pts = new List<PointD>();
        for (int i = 0; i < histogram.BucketCount; i++)
            pts.Add(new PointD((histogram[i].UpperBound + histogram[i].LowerBound) / 2, (double)histogram[i].Count / list.Count));
        pts.Add(new PointD(initialEnergy + step / 2, 0));
        graphControl1.ClearProfile();
        graphControl1.Profile = new Profile(pts);
        graphControl1.MaximalX = initialEnergy * 1.05;
        graphControl1.UpperX = graphControl1.MaximalX;
        //エネルギー分布を描画 ここまで
    }


    private (V3 p, double e)[] MonteCarlo()
    {
        //Electron beam-specimen interactions and simulation methods in microscopy 2018
        //Eqs (2.38), (2.41), (2.42) などを参考

        //ここから、シミュレーション中に変化しない変数を定義
        //試料の平均原子番号. 各元素の重量比で加重平均
        var Z = 13;// 79 29 13;
        //試料の平均原子量 (g/mol)
        var A = 26.98; //196.96 63.55 26.98;
        //試料の密度 (g/cm^3)
        var ρ = 2.70; // 19.32 8.96 2.70 //FormMain.Crystal.Density;

        //トータル散乱断面積の計算中に出てくる定数
        var coeff1 = Z * UniversalConstants.e0 * UniversalConstants.e0 / (8.0 * Math.PI * UniversalConstants.ε0);
        //平均自由行程のところに出てくる定数
        var coeff2 = A / UniversalConstants.A / ρ * 1E21;// / Math.PI;
        //阻止能の計算中に出てくる定数
        var coeff3 = -Z * UniversalConstants.A * ρ * 1E3 / (A * 1E-3) * Math.Pow(UniversalConstants.e0, 4)
            / 4 / Math.PI / UniversalConstants.ε0 / UniversalConstants.ε0 / UniversalConstants.eV_joule * 1E-9 * 1E-3;
        //阻止能の計算中に出てくる物質依存の定数 k    Joy and Luo (1989)によれば 6C:0.77, 13Al: 0.815, 14Si: 0.822, 28Ni: 0.83, 29Cu: 0.83,  47Ag:0.852, 79Au: 0.851
        //取りあえず対数近似した値を使う
        var k = 0.0299 * Math.Log(Z) + 0.7307;
        //阻止能の計算中に出てくる物質依存の定数 J (eV) Z<=12の時は J=11.5evにするらしい (Joy&Luo 1989)
        var J = Z <= 12 ? 11.5 * Z : (9.76 * Z + 58.5 / Math.Pow(Z, 0.19));
        //ここまで、シミュレーション中に変化しない変数を定義

        var r = new Random(Interlocked.Increment(ref seed));

        var trajectory = new List<(V3 p, double e)>() { (new V3(0, 0, 0), waveLengthControl1.Energy) };
        var tan = Math.Tan(-numericBoxSampleTilt.RadianValue);
        var vec = new V3(0, 0, -1);

        //電子エネルギーが2kev以下になるまでループ
        while (trajectory[^1].e > 2 && trajectory[^1].p.X * tan >= trajectory[^1].p.Z)
        {
            //電子の質量 (kg) × 電子の速度の2乗 (m^2/s^2)
            var mv2 = UniversalConstants.Convert.EnergyToElectronMass(trajectory[^1].e) * UniversalConstants.Convert.EnergyToElectronVelositySquared(trajectory[^1].e);
            //散乱係数
            var α = 0.0034 * Math.Pow(Z, 2.0 / 3.0) / trajectory[^1].e;
            //トータル散乱断面積 (nm^2)
            var t1 = coeff1 / mv2;
            var σ_E = t1 * t1 * 4 * Math.PI / α / (α + 1) * 1E18;
            //弾性散乱平均自由行程 (nm) 
            var λ_el = coeff2 / σ_E;
            //阻止能 (Joy and Luo 1989) (kev/nm単位)
            var sp = coeff3 / mv2 * Math.Log(1.166 * k + 0.583 * mv2 / UniversalConstants.eV_joule / J);
            //var sp2 = -7.85 * ρ  * Z / A / kev  * Math.Log(1.166 * kev / J);

            //乱数発生
            double rnd1 = r.NextDouble();

            //飛行距離 s
            var s = -λ_el * Math.Log(rnd1);

            if (trajectory.Count == 1)
                trajectory.Add((s * vec, trajectory[^1].e + s * sp));
            else
            {
                double rnd2 = r.NextDouble(), rnd3 = r.NextDouble();
                double cosθ = 1 - 2 * α * rnd2 / (1 + α - rnd2), sinθ = Math.Sqrt(1 - cosθ * cosθ);
                var φ = 2 * Math.PI * rnd3;
                var rot = GLGeometry.CreateRotationFromZ(vec);
                vec = rot.Mult(new V3(sinθ * Math.Cos(φ), sinθ * Math.Sin(φ), cosθ));
                trajectory.Add((trajectory[^1].p + s * vec, trajectory[^1].e + s * sp));
            }
        }
        return trajectory.Select(e => (e.p / 1000, e.e)).ToArray();
    }

    #endregion


}


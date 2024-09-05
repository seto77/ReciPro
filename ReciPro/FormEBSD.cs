#region using
using Crystallography.OpenGL;
using IronPython.Runtime;
using Microsoft.Scripting.Utils;
using OpenTK;
using OpenTK.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using V3 = OpenTK.Vector3d;
#endregion

namespace ReciPro;
public partial class FormEBSD : Form
{
    #region フィールド、プロパティ
    public FormMain FormMain;

    // private GLControlAlpha glControlGeometry;
    private GLControlAlpha glControlTrajectory;

    private object lockObj = new();

    List<(V3 p, double e)[]> Trajectories = [];
    private double EnergyThreshold = 2;
    private readonly Stopwatch sw = new();
    #endregion

    #region コンストラクタ、Load, Closing
    public FormEBSD()
    {
        InitializeComponent();
    }

    private void FormEBSD_Load(object sender, EventArgs e)
    {
        //glControlGeometry = new GLControlAlpha(GLControlAlpha.FragShaders.OIT)
        //{
        //    AllowMouseRotation = true,
        //    AllowMouseScaling = true,
        //    AllowMouseTranslating = false,
        //    Name = "glControlAxes",
        //    ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
        //    ProjWidth = 8.0,
        //    RotationMode = GLControlAlpha.RotationModes.Object,
        //    Dock = DockStyle.Fill,
        //    LightPosition = new V3(100, 100, 100),
        //    BorderStyle = BorderStyle.Fixed3D,

        //    WorldMatrix =/* Matrix4d.CreateRotationZ(-Math.PI / 8) * */Matrix4d.CreateRotationX(-0.5 * Math.PI)
        //};
        //panelGeometry.Controls.Add(glControlGeometry);

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
            DepthCueing = (false, 0, 0),

            WorldMatrix = Matrix4d.CreateRotationY(-Math.PI / 2) * Matrix4d.CreateRotationZ(-Math.PI / 2)

        };
        paneltTrajectory.Controls.Add(glControlTrajectory);
    }

    private void FormEBSD_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonEBSD.Checked = false;
        this.Visible = false;
    }
    #endregion

    private void buttonCaluculate_Click(object sender, EventArgs e)
    {
        CalcMonteCarlo();
        DrawStatistics();
        Draw3D();
    }

    #region 3D描画をどの方向から眺めるかのボタン
    private void buttonViewIsometric_Click(object sender, EventArgs e)
        => glControlTrajectory.WorldMatrix = Matrix4d.CreateRotationY(-Math.PI / 2) * Matrix4d.CreateRotationZ(-Math.PI / 2);
    private void buttonViewAlongBeam_Click(object sender, EventArgs e)
        => glControlTrajectory.WorldMatrix = Matrix4d.Identity;

    private void button1_Click(object sender, EventArgs e)
        => glControlTrajectory.WorldMatrix = Matrix4d.CreateRotationX(-numericBoxSampleTilt.RadianValue);

    #endregion

    #region 入射電子、試料、検出器の幾何学を3Dで表示
    /// <summary>
    /// 試料と電子線が交差する位置は常に(0,0,0)
    /// </summary>
    //public void DrawGeometry()
    //{
    //    var glObjects = new List<GLObject>();

    //    //試料の傾き
    //    var rot = Matrix3D.RotY(numericBoxSampleTilt.RadianValue);

    //    //試料を示す直方体
    //    var sample = new Parallelepiped(rot * new V3(-1, -1, -0.2), rot * new V3(2, 0, 0), rot * new V3(0, 2, 0), rot * new V3(0, 0, 0.2), new Material(Color4.AliceBlue), DrawingMode.SurfacesAndEdges);
    //    glObjects.Add(sample);

    //    //検出器面
    //    var detector = new Parallelepiped(new V3(2.99, -1.5, -1.5), new V3(0, 3, 0), new V3(0, 0, 3), new V3(0.2, 0, 0), new Material(Color4.GreenYellow, 0.7), DrawingMode.Surfaces);
    //    glObjects.Add(detector);

    //    //ポールピース
    //    glObjects.AddRange(
    //        [
    //        new Pipe(new V3(0,0,2), new V3(0,0,1), 0.15,0.16,new Material(Color4.Gray), DrawingMode.Surfaces,false){ IgnoreNormalSides=true},
    //        new Pipe(new V3(0,0,2), new V3(0,0,1), 0.30,1.5,new Material(Color4.Gray), DrawingMode.Surfaces,false){ IgnoreNormalSides=true},
    //        new HoledDisk(new V3(0,0,2), new V3(0,0,1),0.15,0.3, new Material(Color4.Gray),    DrawingMode.Surfaces){ IgnoreNormalSides=true},
    //        new HoledDisk(new V3(0,0,3), new V3(0,0,1),0.15,1.5, new Material(Color4.Gray),    DrawingMode.Surfaces){ IgnoreNormalSides=true},
    //        ]);

    //    //電子線方向を示す矢印
    //    glObjects.AddRange(
    //        [
    //            new Cone(new V3(0, 0, 0), new V3(0, 0, 2.5), 0.1, new Material(Color4.Yellow,0.7), DrawingMode.Surfaces){ IgnoreNormalSides=true},
    //            new Cone(new V3(0, 0, 0), new V3(3, 0, 0), 1, new Material(Color4.Yellow, 0.7), DrawingMode.Surfaces) { IgnoreNormalSides = true }
    //        ]);

    //    //結晶のa, b, c軸を表す矢印

    //    glControlGeometry.DeleteAllObjects();
    //    glControlGeometry.AddObjects(glObjects);
    //    glControlGeometry.Refresh();
    //}
    #endregion

    #region 指定した条件でモンテカルロ法による飛程計算をおこなう。結果はList<(V3 p, double e)[]> Trajectories に格納される
    public void CalcMonteCarlo()
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

        var (CrossSection, MeanFreePath, StoppingPower) = MonteCarlo.GetParameters(Z, A, ρ, energy);
        labelCrossSection.Text = $"{CrossSection:g3} nm² @ {energy} kev"; 
        labelMeanFreePath.Text = $"{MeanFreePath:f2} nm @ {energy} kev";
        labelStoppingPower.Text = $"{StoppingPower*1000:f2} ev/nm @ {energy} kev";

        double tilt = numericBoxSampleTilt.RadianValue, cosTilt = Math.Cos(tilt), sinTilt = Math.Sin(tilt);

        //飛程計算ループ
        sw.Restart();
        //var list = new List<(V3 p, double e)[]>();
        Trajectories = [];
        Parallel.For(0, numericBoxCalcNum.ValueInteger, i =>//for (int i = 0; i < 10000; i++)
        {
            var trajectry = MonteCarlo.GetTrajectories(Z, A, ρ, energy, tilt, EnergyThreshold);
            //if (trajectry[^1].e > threshold)
            lock (lockObj)
                Trajectories.Add(trajectry);
        });
        toolStripStatusLabel1.Text = $"{sw.ElapsedMilliseconds} msec. ellapsed for {numericBoxCalcNum.ValueInteger} trajectories.";
    }
    #endregion

    #region 統計情報を計算
    private void DrawStatistics()
    {
        double tilt = numericBoxSampleTilt.RadianValue, cosTilt = Math.Cos(tilt), sinTilt = Math.Sin(tilt);
        double energy = waveLengthControl1.Energy;

        

        var BSEs = Trajectories.Where(e => e[^1].e > EnergyThreshold);
        var count = BSEs.Count();

        labelBSEratio.Text = $"{100.0 * count / Trajectories.Count:f2} %";
        labelBSEenergy.Text = $"{BSEs.Average(e => e[^1].e):f2} kev";

        //エネルギー分布を描画 ここから
        {
            double step = 0.2;//kev単位
            double lower = 2, upper = energy;
            int nBuckets = (int)((upper - lower) / step);
            var histogram = new MathNet.Numerics.Statistics.Histogram(BSEs.Select(e => e[^1].e), nBuckets, lower, lower + nBuckets * step);
            var pts = new List<PointD>();
            for (int i = 0; i < histogram.BucketCount; i++)
                pts.Add(new PointD((histogram[i].UpperBound + histogram[i].LowerBound) / 2, (double)histogram[i].Count / count));
            pts.Add(new PointD(energy + step / 2, 0));
            graphControlEnergyProfile.ClearProfile();
            graphControlEnergyProfile.Profile = new Profile(pts);
            graphControlEnergyProfile.MaximalX = upper * 1.05;
            graphControlEnergyProfile.UpperX = graphControlEnergyProfile.MaximalX;
        }
        //エネルギー分布を描画 ここまで


        //最大深さ分布　ここから
        {
            var depths = BSEs.Select(e1 => e1.Max(e2 => sinTilt * e2.p.Y - cosTilt * e2.p.Z));
            double lower = 0, upper = depths.Max();
            double step = (int)(upper * 100.0) / 100.0 / 50.0;//µm単位
            int nBuckets = (int)((upper - lower) / step + 1);
            var histogram = new MathNet.Numerics.Statistics.Histogram(depths, nBuckets, lower, lower + nBuckets * step);
            var pts = new List<PointD>();
            for (int i = 0; i < histogram.BucketCount; i++)
                pts.Add(new PointD((histogram[i].UpperBound + histogram[i].LowerBound) / 2, (double)histogram[i].Count / count));
            graphControlDepthProfile.ClearProfile();
            graphControlDepthProfile.Profile = new Profile(pts);

        }
        //ここまで


        //EBSDに寄与する電子深さ分布　ここから
        {
            var depths = BSEs.Select(e => 1000 * (sinTilt * e[^2].p.Y - cosTilt * e[^2].p.Z));
            double lower = 0, upper = depths.Max();
            double step = (int)(upper * 100.0) / 100.0 / 50.0;//nm単位
            int nBuckets = (int)((upper - lower) / step + 1);
            var histogram = new MathNet.Numerics.Statistics.Histogram(depths, nBuckets, lower, lower + nBuckets * step);
            var pts = new List<PointD>();
            for (int i = 0; i < histogram.BucketCount; i++)
                pts.Add(new PointD((histogram[i].UpperBound + histogram[i].LowerBound) / 2, (double)histogram[i].Count / count));
            graphControlDepthEBSD.ClearProfile();
            graphControlDepthEBSD.Profile = new Profile(pts);
        }
        //ここまで

        //表面に沿った方向の距離分布
        {
            var distances = BSEs.Select(e1 => e1.Max(e2 =>
            {
                var y = cosTilt * e2.p.Y + sinTilt * e2.p.Z;
                return Math.Sqrt(e2.p.X * e2.p.X + y * y);
            }));

            double lower = 0, upper = distances.Max();
            double step = (int)(upper * 100.0) / 100.0 / 50.0;//µm単位
            int nBuckets = (int)((upper - lower) / step + 1);
            var histogram = new MathNet.Numerics.Statistics.Histogram(distances, nBuckets, lower, lower + nBuckets * step);
            var pts = new List<PointD>();
            for (int i = 0; i < histogram.BucketCount; i++)
                pts.Add(new PointD((histogram[i].UpperBound + histogram[i].LowerBound) / 2, (double)histogram[i].Count / count));
            graphControlDistance.ClearProfile();
            graphControlDistance.Profile = new Profile(pts);
        }
        //ここまで

        //ステレオネット描画
        var rot = Matrix3d.CreateRotationX(tilt);
        if (checkBoxDrawAxesInStereonet.Checked)
            poleFigureControl.Symbols = [
                (Stereonet.ConvertVectorToSchmidt(new Vector3DBase(1, 0,0)), 0.02, Color.OrangeRed, true, "+X"),
            (Stereonet.ConvertVectorToSchmidt(rot.Mult(new V3(0, -1, 0)).ToVector3DBase()), 0.02, Color.YellowGreen, true, "-Y"),
            (Stereonet.ConvertVectorToSchmidt(rot.Mult(new V3(0, 1, 0)).ToVector3DBase()), 0.02, Color.YellowGreen, true, "+Y"),
            (Stereonet.ConvertVectorToSchmidt(rot.Mult(new V3(0, 0, 1)).ToVector3DBase()), 0.02, Color.MediumPurple, true, "+Z (=beam)")
                ];
        else
            poleFigureControl.Symbols = [];

        poleFigureControl.Vectors = BSEs.Select(e => rot.Mult(e[^1].p - e[^2].p)).ToArray();
    }

    #endregion

    #region OpenGLを用いて三次元の飛程を表示
    private void Draw3D()
    {
        double tilt = numericBoxSampleTilt.RadianValue, cosTilt = Math.Cos(tilt), sinTilt = Math.Sin(tilt);
        double energy = waveLengthControl1.Energy;

        var list = new List<(V3 p, double e)[]>();
        for (int i = 0; i < Trajectories.Count && list.Count < numericBoxDrawNum.ValueInteger; i++)
        {
            if (checkBoxDrawAbsorved.Checked || Trajectories[i][^1].e > EnergyThreshold)
                list.Add(Trajectories[i]);
        }

        //ここから OpenGL描画
        List<GLObject> glObjects = [];
        int colorDiv = 16;//16段階で色を変化させていく
        foreach (var trajectry in list)
        {
            int start = 0, end = 0;
            var mat = new Material(Color4.Black);
            for (int j = colorDiv; j >= 1 && end < trajectry.Length; j--)
            {
                if (trajectry[^1].e > EnergyThreshold)
                    mat = new Material(new Color4(255, (byte)(255 * (colorDiv - j) / colorDiv), (byte)(255 * (colorDiv - j) / colorDiv), (byte)(255 * j / colorDiv)));
                else
                    mat = new Material(new Color4((byte)(255 * (colorDiv - j) / colorDiv), (byte)(255 * (colorDiv - j) / colorDiv), 255, (byte)(255 * j / colorDiv)));
                end = trajectry.FindIndex(t => t.e < (double)(j - 1) / colorDiv * energy);
                if (end == -1)
                    end = trajectry.Length;
                glObjects.Add(new Lines(trajectry[start..end].Select(e => e.p).ToArray(), 1f, mat));

                start = end - 1;
            }


            if (checkBoxDrawPathAfterEscape.Checked && trajectry[^1].e > EnergyThreshold && trajectry.Length > 1)
            {
                var r = trajectry[^2].e / waveLengthControl1.Energy;
                var v = (trajectry[^1].p - trajectry[^2].p).Normalized() * 0.1 * r;
                var matBackScattered = new Material(new Color4(255, (byte)(128 * (1 - r)+127), (byte)(255 * (1 - r)), (byte)(200 * r )));
                glObjects.Add(new Lines([trajectry[^2].p, trajectry[^2].p + v], 1f, matBackScattered));
            }
        }

        double maxLength = list[..numericBoxDrawNum.ValueInteger].Max(e1 => e1.Max(e2 =>
        {
            var y = cosTilt * e2.p.Y + sinTilt * e2.p.Z;
            return Math.Sqrt(e2.p.X * e2.p.X + y * y);
        }));
        var scaleStep = maxLength switch
        {
             < 1 => 0.01,
             < 5 => 0.05,
             < 10 => 0.1,
             < 50 => 0.5,
             < 100 => 1,
            _=>5
        };
       
        var limit = (int)(maxLength / scaleStep + 1);

        if (checkBoxDrawGuidCircles.Checked)
        {
            var circleArray = Enumerable.Range(0, 361)
                .Select(e => new V3(Math.Cos(e / 180.0 * Math.PI), Math.Sin(e / 180.0 * Math.PI) * cosTilt, Math.Sin(e / 180.0 * Math.PI) * sinTilt));
            for (int i = 1; i <= limit; i++)
            {
                glObjects.Add(new Lines(circleArray.Select(e => e * i * scaleStep).ToArray(), i % 5 == 0 ? 2f : 1f, new Material(Color4.LightGray)));
                if (i % 10 == 0)
                    glObjects.Add(new TextObject($"{i * scaleStep:0.0} µm", 10f, new V3(0, cosTilt, sinTilt) * i * scaleStep, 1000, true, new Material(Color4.Black)));
            }
        }

        if (checkBoxDrawAxes.Checked)
        {
            var len = limit * scaleStep * 0.5;
            //X軸
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(len, 0, 0)], 3f, new Material(Color4.OrangeRed)));
            glObjects.Add(new TextObject("+X", 10f, new V3(len, 0, 0), 1000, true, new Material(Color4.OrangeRed)));

            //Y軸
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, len, 0)], 3f, new Material(Color4.YellowGreen)));
            glObjects.Add(new TextObject("+Y", 10f, new V3(0, len, 0), 1000, true, new Material(Color4.YellowGreen)));

            //Z軸 = beam
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, 0, len)], 3f, new Material(Color4.MediumPurple)));
            glObjects.Add(new TextObject("+Z (=beam)", 10f, new V3(0, 0, len), 1000, true, new Material(Color4.MediumPurple)));
        }

        glControlTrajectory.ProjWidth = maxLength * 2;
        glControlTrajectory.DeleteAllObjects();
        glControlTrajectory.AddObjects(glObjects);
        glControlTrajectory.Refresh();
        //OpenGLここまで
    }
    #endregion 

    private void checkBoxDrawAxes_CheckedChanged(object sender, EventArgs e) => Draw3D();
    private void checkBoxDrawAxesInStereonet_CheckedChanged(object sender, EventArgs e) => DrawStatistics();
}

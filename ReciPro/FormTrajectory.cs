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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using V3 = OpenTK.Vector3d;
using V4 = OpenTK.Vector4d;
#endregion

namespace ReciPro;
public partial class FormTrajectory : Form
{
    #region フィールド、プロパティ
    public FormMain FormMain;

    private GLControlAlpha glControlTrajectory;

    private readonly Lock lockObj = new();

    (V3 p, double e)[][] Trajectories = [];
    private double EnergyThreshold = 2;
    private readonly Stopwatch sw = new();
    #endregion

    #region コンストラクタ、Load, Closing
    public FormTrajectory()
    {
        InitializeComponent();
    }

    private void FormEBSD_Load(object sender, EventArgs e)
    {
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
        FormMain.toolStripButtonTrajectorySimulator.Checked = false;
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
    private void buttonViewFromX_Click(object sender, EventArgs e)
        => glControlTrajectory.WorldMatrix = Matrix4d.CreateRotationY(-Math.PI / 2) * Matrix4d.CreateRotationZ(-Math.PI / 2);
    private void buttonViewFromZ_Click(object sender, EventArgs e)
        => glControlTrajectory.WorldMatrix = Matrix4d.Identity;

    private void buttonViewFromSurfaceNormal_Click(object sender, EventArgs e)
        => glControlTrajectory.WorldMatrix = Matrix4d.CreateRotationX(-numericBoxSampleTilt.RadianValue);

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
        double energy = waveLengthControl.Energy;
        
        //サンプルの傾き
        double tilt = numericBoxSampleTilt.RadianValue, cosTilt = Math.Cos(tilt), sinTilt = Math.Sin(tilt);

        var monte = new MonteCarlo(Z, A, ρ, energy, tilt);

        var (_, CrossSection, MeanFreePath, StoppingPower) = monte.GetParameters(energy);
        labelCrossSection.Text = $"{CrossSection:g3} nm² @ {energy} kev";
        labelMeanFreePath.Text = $"{MeanFreePath:f2} nm @ {energy} kev";
        labelStoppingPower.Text = $"{StoppingPower * 1000:f2} ev/nm @ {energy} kev";

        //飛程計算ループ
        sw.Restart();
        Trajectories = new (V3 p, double e)[numericBoxCalcNum.ValueInteger][];
        Parallel.For(0, Trajectories.Length, i => Trajectories[i] = monte.GetTrajectories());
        
        toolStripStatusLabel1.Text = $"{sw.ElapsedMilliseconds} msec. ellapsed for {numericBoxCalcNum.ValueInteger} trajectories.";
    }
    #endregion

    #region 統計情報を計算
    private void DrawStatistics()
    {
        double tilt = numericBoxSampleTilt.RadianValue, cosTilt = Math.Cos(tilt), sinTilt = Math.Sin(tilt);
        double energy = waveLengthControl.Energy;

        var BSEs = Trajectories.Where(e => e[^1].e > EnergyThreshold);
        var count = BSEs.Count();

        labelBSEratio.Text = $"{100.0 * count / Trajectories.Length:f2} %";
        labelBSEenergy.Text = $"{BSEs.Average(e => e[^1].e):f2} kev";

        //エネルギー分布を描画 ここから
        {
            double step = 0.1;//kev単位
            double lower = 0, upper = energy - EnergyThreshold;
            int nBuckets = (int)((upper - lower) / step);
            var histogram = new MathNet.Numerics.Statistics.Histogram(BSEs.Select(e => energy - e[^1].e), nBuckets, lower, lower + nBuckets * step);
            var pts = new List<PointD>();
            for (int i = 0; i < histogram.BucketCount; i++)
                pts.Add(new PointD((histogram[i].UpperBound + histogram[i].LowerBound) / 2, (double)histogram[i].Count / count));
            pts.Add(new PointD(energy + step / 2, 0));
            graphControlEnergyProfile.ClearProfile();
            graphControlEnergyProfile.Profile = new Profile(pts);
            graphControlEnergyProfile.MaximalX = upper;
            graphControlEnergyProfile.UpperX = upper*0.5;
            graphControlEnergyProfile.Draw();
        }
        //エネルギー分布を描画 ここまで

        //最大深さ分布　ここから
        {
            var depths = BSEs.Select(e1 => e1.Max(e2 => sinTilt * e2.p.Y - cosTilt * e2.p.Z));
            double lower = 0, upper = depths.Max();
            double step = (int)(upper * 100.0) / 400.0 / 50.0;//µm単位
            int nBuckets = (int)((upper - lower) / step + 1);
            var histogram = new MathNet.Numerics.Statistics.Histogram(depths, nBuckets, lower, lower + nBuckets * step);
            var pts = new List<PointD>();
            for (int i = 0; i < histogram.BucketCount; i++)
                pts.Add(new PointD((histogram[i].UpperBound + histogram[i].LowerBound) / 2, (double)histogram[i].Count / count));
            graphControlDepthProfile.ClearProfile();
            graphControlDepthProfile.Profile = new Profile(pts);
            graphControlDepthProfile.UpperX = upper * 0.5;
            graphControlDepthProfile.Draw();

        }
        //ここまで

        //EBSDに寄与する電子深さ分布　ここから
        //{
        //    var depths = BSEs.Select(e => 1000 * (sinTilt * e[^2].p.Y - cosTilt * e[^2].p.Z));
        //    double lower = 0, upper = depths.Max();
        //    double step = (int)(upper * 100.0) / 100.0 / 50.0;//nm単位
        //    int nBuckets = (int)((upper - lower) / step + 1);
        //    var histogram = new MathNet.Numerics.Statistics.Histogram(depths, nBuckets, lower, lower + nBuckets * step);
        //    var pts = new List<PointD>();
        //    for (int i = 0; i < histogram.BucketCount; i++)
        //        pts.Add(new PointD((histogram[i].UpperBound + histogram[i].LowerBound) / 2, (double)histogram[i].Count / count));
        //    graphControlDepthEBSD.ClearProfile();
        //    graphControlDepthEBSD.Profile = new Profile(pts);
        //}
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
        if (radioButtonFrequency.Checked)
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Histogram;
        else if (radioButtonAverageEnergy.Checked)
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Average;
        else
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Sigma;

        var rot = Matrix3d.CreateRotationX(tilt);

        //ステレオネット内にXYZ軸を描画
        if (checkBoxDrawAxesInStereonet.Checked)
            poleFigureControl.Circles = [
                (Stereonet.ConvertVectorToSchmidt(new Vector3DBase(-1, 0,0)), 0.02, Color.OrangeRed, true, "+X"),
                (Stereonet.ConvertVectorToSchmidt(rot.Mult(new V3(0, -1, 0)).ToVector3DBase()), 0.02, Color.YellowGreen, true, "+Y"),
                (Stereonet.ConvertVectorToSchmidt(rot.Mult(new V3(0, 1, 0)).ToVector3DBase()), 0.02, Color.YellowGreen, true, "-Y"),
                (Stereonet.ConvertVectorToSchmidt(rot.Mult(new V3(0, 0, -1)).ToVector3DBase()), 0.02, Color.MediumPurple, true, "+Z"),
                (Stereonet.ConvertVectorToSchmidt(rot.Mult(new V3(0, 0, 1)).ToVector3DBase()), 0.02, Color.MediumPurple, true, "-Z")
                ];
        else
            poleFigureControl.Circles = [];

        poleFigureControl.Vectors = BSEs.Select(e => new V4(rot.Mult(e[^1].p - e[^2].p), e[^1].e)).ToArray();
        //最大深さ分布を求めるためのテストコード
        // poleFigureControl.Vectors = BSEs.Select(e1 => new V4(rot.Mult(e1[^1].p - e1[^2].p), e1.Max(e2 => sinTilt * e2.p.Y - cosTilt * e2.p.Z))).ToArray();
    }
    #endregion

    #region OpenGLを用いて三次元の飛程を表示
    private void Draw3D()
    {
        double tilt = numericBoxSampleTilt.RadianValue, cosTilt = Math.Cos(tilt), sinTilt = Math.Sin(tilt);
        double energy = waveLengthControl.Energy;

        var list = new List<(V3 p, double e)[]>();
        for (int i = 0; i < Trajectories.Length && list.Count < numericBoxDrawNum.ValueInteger; i++)
        {
            if (checkBoxDrawAbsorved.Checked || Trajectories[i][^1].e > EnergyThreshold)
                list.Add(Trajectories[i]);
        }

        double maxLength = list.Max(e1 => e1.Max(e2 =>
        {
            var y = cosTilt * e2.p.Y + sinTilt * e2.p.Z;
            return Math.Sqrt(e2.p.X * e2.p.X + y * y);
        }));

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
                var r = trajectry[^2].e / waveLengthControl.Energy;
                var v = (trajectry[^1].p - trajectry[^2].p).Normalized() * r * maxLength / 2;
                var matBackScattered = new Material(new Color4(255, (byte)(128 * (1 - r) + 127), (byte)(255 * (1 - r)), (byte)(200 * r)));
                glObjects.Add(new Lines([trajectry[^2].p, trajectry[^2].p + v], 1f, matBackScattered));
            }
        }

        var scaleStep = maxLength switch { < 1 => 0.01, < 5 => 0.05, < 10 => 0.1, < 50 => 0.5, < 100 => 1, _ => 5 };
        var limit = (int)(maxLength / scaleStep + 1);
        if (checkBoxDrawGuidCircles.Checked)
        {
            var circleArray = Enumerable.Range(0, 361)
                .Select(e => new V3(Math.Cos(e / 180.0 * Math.PI), Math.Sin(e / 180.0 * Math.PI) * cosTilt, Math.Sin(e / 180.0 * Math.PI) * sinTilt));
            for (int i = 1; i <= limit; i++)
            {
                glObjects.Add(new Lines(circleArray.Select(e => e * i * scaleStep).ToArray(), i % 5 == 0 ? 2f : 1f, new Material(Color4.LightGray)));
                glControlTrajectory.MakeCurrent();
                if (i % 10 == 0)
                    glObjects.Add(new TextObject($"{i * scaleStep:0.0} µm", 10f, new V3(0, cosTilt, sinTilt) * i * scaleStep, 1000, true, new Material(Color4.Black)));
            }
        }

        if (checkBoxDrawAxes.Checked)
        {
            var len = limit * scaleStep * 0.5;
            //X軸
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(len, 0, 0)], 3f, new Material(Color4.OrangeRed)));
            glControlTrajectory.MakeCurrent();
            glObjects.Add(new TextObject("+X", 10f, new V3(len, 0, 0), 1000, true, new Material(Color4.OrangeRed)));

            //Y軸
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, -len, 0)], 3f, new Material(Color4.YellowGreen)));
            glControlTrajectory.MakeCurrent();
            glObjects.Add(new TextObject("+Y", 10f, new V3(0, -len, 0), 1000, true, new Material(Color4.YellowGreen)));

            //Z軸 = beam
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, 0, -len)], 3f, new Material(Color4.MediumPurple)));
            glControlTrajectory.MakeCurrent();
            glObjects.Add(new TextObject("+Z (=beam)", 10f, new V3(0, 0, -len), 1000, true, new Material(Color4.MediumPurple)));
        }

        glControlTrajectory.ProjWidth = maxLength * 2.05;
        glControlTrajectory.DeleteAllObjects();
        glControlTrajectory.AddObjects(glObjects);
        glControlTrajectory.Refresh();
        //OpenGLここまで
    }
    #endregion

    #region イベント処理
    private void checkBoxDrawAxes_CheckedChanged(object sender, EventArgs e) => Draw3D();
    private void checkBoxDrawAxesInStereonet_CheckedChanged(object sender, EventArgs e) => DrawStatistics();

    private void radioButtonFrequency_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is RadioButton radioButton && radioButton.Checked)
            DrawStatistics();
    }
    #endregion
}

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
        MonteCarlo();

        List<GLObject> glObjects =
        [
            new Quads(new V3(5, 5, 0), new V3(-5, 5, 0), new V3(-5, -5, 0), new V3(5, -5, 0), new Material(Color4.AliceBlue, 0.7), DrawingMode.Surfaces)
        ];


        glControlTrajectory.DeleteAllObjects();
        glControlTrajectory.AddObjects(glObjects);
        glControlTrajectory.Refresh();
    }


    private (V3 p, double e)[] MonteCarlo()
    {
        //Electron beam-specimen interactions and simulation methods in microscopy 2018
        //Eqs (2.38), (2.41), (2.42) などを参考

        //ここから、シミュレーション中に変化しない変数を定義
        //試料の平均原子番号
        //各元素の重量比で加重平均
        var Z = 29;
        //試料の平均原子量 (g/mol)
        var A = 63.546;
        //試料の密度 (g/cm^3)
        var ρ = 8.94;// FormMain.Crystal.Density;
        var ρ_gram_per_nm3 = 8.94E-21;

        //トータル散乱断面積の計算中に出てくる定数
        var coeff1 = Z * UniversalConstants.e0 * UniversalConstants.e0 / (8.0 * Math.PI * UniversalConstants.ε0);
        //平均自由行程のところに出てくる定数
        var coeff2 = A / UniversalConstants.A / ρ_gram_per_nm3 / Math.PI;
        //阻止能の計算中に出てくる定数
        var coeff3 = -Z * UniversalConstants.A * ρ * 1E3 / (A * 1E-3) * Math.Pow(UniversalConstants.e0, 4) 
            / 4 / Math.PI / UniversalConstants.ε0 / UniversalConstants.ε0 / UniversalConstants.eV_joule * 1E-9;
        //阻止能の計算中に出てくる物質依存の定数 k    Joy and Luo (1989)によれば 6C:0.77, 13Al: 0.815, 14Si: 0.822, 28Ni: 0.83, 29Cu: 0.83,  47Ag:0.852, 79Au: 0.851
        //取りあえず対数近似した値を使う
        var k = 0.0299 * Math.Log(Z) + 0.7307;
        //阻止能の計算中に出てくる物質依存の定数 J (eV) Z<=12の時は J=11.5evにするらしい (Joy&Luo 1989)
        var J = Z<= 12 ? 11.5 * Z : (9.76 * Z + 58.5 / Math.Pow(Z, 0.19));
        //ここまで、シミュレーション中に変化しない変数を定義

        //電子エネルギー (kev) 初期値はwaveLengthControlから取得
        var kev = waveLengthControl1.Energy;

        //電子エネルギーが2kev以下になるまでループ
        while (kev > 2)
        {
            //電子の質量 (kg) × 電子の速度の2乗 (m^2/s^2)
            var mv2 = UniversalConstants.Convert.EnergyToElectronMass(kev) * UniversalConstants.Convert.EnergyToElectronVelositySquared(kev);
            //散乱係数
            var α = 0.0034 * Math.Pow(Z, 2.0 / 3.0) / kev;
            //トータル散乱断面積 (nm^2)
            var t1 = coeff1 / mv2;
            var σ_E = t1 * t1 * 4 * Math.PI / α / (α + 1) * 1E18;
            //弾性散乱平均自由行程 (nm)  注(2.33)の式は分母のpiが抜けているっぽい。。たぶん。。
            var λ_el = coeff2 / σ_E;
            //阻止能 (Joy and Luo 1989) (ev/nm単位)
            var sp = coeff3 / mv2 * Math.Log(1.166 * k + 0.583 * mv2 / UniversalConstants.eV_joule / J);
            //var sp2 = -7.85 * ρ  * Z / A / kev  * Math.Log(1.166 * kev / J);
        }
        return null;
    }

    #endregion


}


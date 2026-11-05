#region using
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
#endregion

namespace ReciPro;

//260903Cl 変更: ReciPro のどのウィンドウが前面でも矢印キーを拾えるよう IMessageFilter を実装
//旧: public partial class FormDiffractionSimulatorHolder : FormBase
public partial class FormDiffractionSimulatorHolder : FormBase, IMessageFilter
{
    #region Fields and Properties
    public FormDiffractionSimulator FormDiffractionSimulator;
    private PointD centerPt = new(0, 0);

    /// <summary>親フォームから呼ばれるイベントをスキップするかどうか</summary>
    private bool skipEventFromParent = false;
    /// <summary>このフォームから呼ばれるイベントをスキップするかどうか</summary>
    private bool skipEventFromThis = false;

    /// <summary>主軸の方向（ラジアン）</summary>
    public double PrimaryAxisDirection { get => numericBoxTiltXDirection.RadianValue; set => numericBoxTiltXDirection.RadianValue = value; }

    private int boxWidth => graphicsBox.ClientSize.Width;
    private int boxHeight => graphicsBox.ClientSize.Height;

    private double mag =>
         boxWidth / 2.0 / Vector3D.StereoNetPoint(new Vector3D(0, Math.Sin(numericBoxDrawingArea.RadianValue), Math.Cos(numericBoxDrawingArea.RadianValue))).Y;
    private readonly Timer timer = new();
    private Crystal crystal => FormDiffractionSimulator.formMain.Crystal;

    //private Matrix3D NeutralDirection { get; set; } = Matrix3D.IdentityMatrix; // 261105Cl 変更前
    /// <summary>ホルダーを傾けていないときの結晶の方位 (今の方位 = HolderRotation⁻¹ · NeutralDirection)。
    /// 261105Cl 変更: 設定のたびに、今の傾斜で結晶があるはずの方位と、その結晶を控える (<see cref="numericBoxTilt_ValueChanged"/> で、ほかの窓で回されたかを見る)</summary>
    private Matrix3D NeutralDirection
    {
        get => neutralDirection;
        set
        {
            neutralDirection = value;
            expectedRotation = HolderRotation.Inverse() * value;
            expectedCrystal = FormDiffractionSimulator?.formMain?.Crystal;
        }
    }
    private Matrix3D neutralDirection = Matrix3D.IdentityMatrix; // 261105Cl 追加
    /// <summary>261105Cl 追加: この窓が最後に結晶の方位を控えた (または置いた) ときの方位 (写し) と結晶</summary>
    private Matrix3D expectedRotation = Matrix3D.IdentityMatrix;
    private Crystal expectedCrystal;

    private Matrix3D HolderRotation =>
        Matrix3D.RotZ(numericBoxTiltXDirection.RadianValue) *
        Matrix3D.RotY(numericBoxTiltX.RadianValue) *
        Matrix3D.RotX((radioButtonTiltY_Plus.Checked ? -1 : 1) * numericBoxTiltY.RadianValue) *
        Matrix3D.RotZ(numericBoxTiltXDirection.RadianValue).Inverse();

    /// <summary>Trigonometric Function Table</summary>
    private readonly List<(double Sin, double Cos, double Tan)> TF =
        [.. Enumerable.Range(0, 360).Select(n => n * Math.PI / 180.0).Select(e => (Math.Sin(e), Math.Cos(e), Math.Tan(e)))];


    private List<(Vector3DBase Vec, (int U, int V, int W) Index)> ZoneAxes = [];

    #endregion

    #region コンストラクタ、ロード、クローズイベント
    public FormDiffractionSimulatorHolder()
    {
        InitializeComponent();
        HelpPage = "7-diffraction-simulator"; //260801Cl 追加: 未設定だと F1 がマニュアルのトップページを開いてしまう
    }

    //260903Cl 追加: アプリ全体の矢印キーを拾うメッセージフィルタの登録/解除。ハンドルの生成/破棄と対にしておく
    //(FormBorderStyle 等の変更で WinForms がハンドルを再生成しても登録が消えない)。有効条件は PreFilterMessage 内で判定する。
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        Application.AddMessageFilter(this);
    }
    protected override void OnHandleDestroyed(EventArgs e)
    {
        Application.RemoveMessageFilter(this);
        base.OnHandleDestroyed(e);
    }

    private void FormDiffractionSimulatorHolder_Load(object sender, EventArgs e)
    {
        NeutralDirection = HolderRotation * crystal.RotationMatrix;

        setVector();
        Draw();

        timer.Interval = 500;
        timer.Tick += ((sender, e) => graphicsBox.Refresh());
        timer.Start();
    }
    private void FormDiffractionSimulatorHolder_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        this.Visible = false;
    }

    /// <summary>261105Cl 追加 (監査 #18): 再表示のたびに、今の結晶の方位を「今の傾斜での方位」として NeutralDirection を作り直し、晶帯軸も今の結晶で作り直す。
    /// 親は窓が見えているときしか RotationChanged / CrystalChanged を呼ばず、Load は初回の表示でしか来ないので、閉じている間に方位 (や結晶) を変えて
    /// 開き直すと NeutralDirection が古いまま残り、Tilt を動かすと結晶が昔の方位へ飛んでいた (方位 = HolderRotation⁻¹ · NeutralDirection)</summary>
    protected override void OnVisibleChanged(EventArgs e)
    {
        base.OnVisibleChanged(e);
        if (!Visible || FormDiffractionSimulator?.formMain?.Crystal == null) return;
        NeutralDirection = HolderRotation * crystal.RotationMatrix;
        setVector();
        Draw();
    }
    #endregion

    #region 描画
    //ステレオネットを描く
    public void Draw(Graphics g = null, bool renewOutline = true)
    {
        if (graphicsBox.Width <= 0 || graphicsBox.Height <= 0) return;
        //260903Cl 追加: graphicsBox.Resize は InitializeComponent 中 (ResumeLayout / DPI スケール) にも発火し、
        //親 FormDiffractionSimulator は生成後の初期化子で代入されるため、未配線なら描かずに戻る (setVector が crystal を触る)
        if (FormDiffractionSimulator?.formMain?.Crystal == null) return;

        //グラフィックスボックスに描画する場合
        g ??= graphicsBox.Graphics;

        g.Clear(colorControlBackGround.Color);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        DrawOutline(g);
        DrawStereoNet(g);

        graphicsBox.Refresh();
    }

    /// <summary>
    /// 260524Cl 追加: --capture 用。Show しただけでは左のステレオネットが空 (白) になるため、傾斜方向ステレオネットを描画する。
    /// 親 FormDiffractionSimulator (formMain.Crystal) が配線済みである必要がある。呼び出し元は GuiCapture に限定する。
    /// </summary>
    internal void PrepareCaptureForGuiAudit()
    {
        if (FormDiffractionSimulator?.formMain?.Crystal == null)
            return;
        Draw();
    }

    /// <summary>ステレオネットの輪郭を描く</summary>
    /// <param name="g"></param>
    private void DrawOutline(Graphics g)
    {
        var (sin, cos) = Math.SinCos(PrimaryAxisDirection);
        var (magSin, magCos) = ((float)(mag * sin), (float)(mag * cos));
        //g.Transform = new Matrix(magCos, -magSin, magSin, magCos, (float)(boxWidth / 2.0 - mag * centerPt.X), (float)(boxHeight / 2.0 + mag * centerPt.Y)); // (260611Ch) 旧: Matrix が未解放
        using var transform = new Matrix(magCos, -magSin, magSin, magCos, (float)(boxWidth / 2.0 - mag * centerPt.X), (float)(boxHeight / 2.0 + mag * centerPt.Y)); // (260611Ch)
        g.Transform = transform; // (260611Ch)

        //var pen1 = new Pen(new SolidBrush(colorControl1DegLine.Color), (float)(1 / mag)); // (260611Ch) 旧: Pen/内部 SolidBrush が未解放
        using var pen1 = new Pen(colorControl1DegLine.Color, (float)(1 / mag)); // (260611Ch)
        using var pen10 = new Pen(colorControl10DegLine.Color, (float)(2 / mag)); // (260611Ch)
        using var pen90 = new Pen(colorControl90DegLine.Color, (float)(3 / mag)); // (260611Ch)

        var wList = new List<int>();
        for (int i = 1; i < 90; i++)
            if (i % 10 != 0)
                wList.Add(i);
        for (int i = 10; i < 90; i += 10)
            wList.Add(i);

        for (int n = 0; n < wList.Count; n++)
        {
            int w = wList[n];
            var pen = w % 10 == 0 ? pen10 : pen1;
            if (checkBox1DegLine.Checked || w % 10 == 0)
            {
                g.DrawArc(pen, -TF[w].Cos / (1 + TF[w].Sin), -1 / TF[w].Cos, 2 / TF[w].Cos, 2 / TF[w].Cos, (w + 90), (180 - 2 * w));
                g.DrawArc(pen, -TF[w].Cos / (1 - TF[w].Sin), -1 / TF[w].Cos, 2 / TF[w].Cos, 2 / TF[w].Cos, (w - 90), (180 - 2 * w));

                g.DrawArc(pen, -TF[w].Tan, -TF[w].Cos / (1 - TF[w].Sin), 2 * TF[w].Tan, 2 * TF[w].Tan, w, (180 - 2 * w));
                g.DrawArc(pen, -TF[w].Tan, TF[w].Cos / (1 + TF[w].Sin), 2 * TF[w].Tan, 2 * TF[w].Tan, (w + 180), (180 - 2 * w));
            }
        }

        g.DrawArc(pen90, -1f, -1f, 2f, 2f, 0, 360);
        g.DrawLine(pen90, 0f, -1f, 0f, 1f);
        g.DrawLine(pen90, -1f, 0f, 1f, 0f);

        if (checkBoxTiltDirections.Checked)
        {
            //var penTiltX = new Pen(new SolidBrush(colorControlTiltX.Color), (float)(2 / mag)); // (260611Ch) 旧: Pen/内部 SolidBrush が未解放
            using var penTiltX = new Pen(colorControlTiltX.Color, (float)(2 / mag)); // (260611Ch)
            g.DrawLine(penTiltX, 0f, 0f, 50 / mag, 0);
            g.DrawLine(penTiltX, 50 / mag, 0, 40 / mag, 5 / mag);
            g.DrawLine(penTiltX, 50 / mag, 0f, 40 / mag, -5 / mag);

            //var penTiltY = new Pen(new SolidBrush(colorControlTiltY.Color), (float)(2 / mag)); // (260611Ch) 旧: Pen/内部 SolidBrush が未解放
            using var penTiltY = new Pen(colorControlTiltY.Color, (float)(2 / mag)); // (260611Ch)
            var sign = radioButtonTiltY_Plus.Checked ? -1 : 1;
            g.DrawLine(penTiltY, 0f, 0f, 0f, sign * 50 / mag);
            g.DrawLine(penTiltY, 0f, sign * 50 / mag, 5 / mag, sign * 40 / mag);
            g.DrawLine(penTiltY, 0f, sign * 50 / mag, -5 / mag, sign * 40 / mag);
        }
    }

    /// <summary>ステレオネットに晶帯軸を描く</summary>
    /// <param name="g"></param>
    private void DrawStereoNet(Graphics g)
    {
        if (ZoneAxes.Count == 0)
            setVector();
        //g.Transform = new Matrix((float)mag, 0, 0, (float)mag, (float)(boxWidth / 2.0 - mag * centerPt.X), (float)(boxHeight / 2.0 + mag * centerPt.Y)); // (260611Ch) 旧: Matrix が未解放
        using var transform = new Matrix((float)mag, 0, 0, (float)mag, (float)(boxWidth / 2.0 - mag * centerPt.X), (float)(boxHeight / 2.0 + mag * centerPt.Y)); // (260611Ch)
        g.Transform = transform; // (260611Ch)
        var rot = HolderRotation * crystal.RotationMatrix;

        //var font = new Font(WineCompat.Resolve("Tahoma"), trackBarStrSize.Value / (float)mag / 7f); //260610Cl Wine時フォント切替 // (260611Ch) 旧: 未解放
        using var font = new Font(WineCompat.Resolve("Tahoma"), trackBarStrSize.Value / (float)mag / 7f); //260610Cl Wine時フォント切替 // (260611Ch)
        using var brushUnique = new SolidBrush(colorControlUniqueAxis.Color); // (260611Ch)
        using var brushGeneral = new SolidBrush(colorControlGeneralAxis.Color); // (260611Ch)
        var radius = trackBarPointSize.Value / mag;

        //晶帯軸の描画
        foreach (var zone in ZoneAxes)
        {
            var pt = Stereonet.ConvertVectorToWulff(rot * zone.Vec);
            if (pt.X * pt.X + pt.Y * pt.Y <= 1)
            {
                var (u, v, w) = zone.Index;
                var brush = (u, v, w) switch
                {
                    (1, 0, 0) or (0, 1, 0) or (0, 0, 1) or (-1, 0, 0) or (0, -1, 0) or (0, 0, -1) => brushUnique,
                    _ => brushGeneral,
                };

                g.FillEllipse(brush, new RectangleD(pt.X - radius, -pt.Y - radius, radius * 2, radius * 2));
                if (checkBoxShowIndexLabels.Checked)
                    g.DrawString($"[{u} {v} {w}]", font, brush, pt.X + radius, -pt.Y + radius);
            }
        }

        //ホルダー位置の描画
        radius *= 1.5;
        var ptHolder = Stereonet.ConvertVectorToWulff(HolderRotation * new Vector3DBase(0, 0, 1));
        //var penHolder = new Pen(new SolidBrush(colorControlHolder.Color), (float)(3 / mag)); // (260611Ch) 旧: Pen/内部 SolidBrush が未解放
        using var penHolder = new Pen(colorControlHolder.Color, (float)(3 / mag)); // (260611Ch)
        g.DrawEllipse(penHolder, new RectangleD(ptHolder.X - radius, -ptHolder.Y - radius, radius * 2, radius * 2));
    }

    #endregion

    #region 座標変換
    private PointF convertSrcToClient(PointD pt)
 => new((float)(boxWidth / 2.0 + mag * (pt.X - centerPt.X)), (float)(boxHeight / 2.0 + mag * (pt.Y - centerPt.Y)));

    private PointF convertSrcToClient(double x, double y) => convertSrcToClient(new PointD(x, y));

    private PointD convertClientToSrc(Point pt)
        => new(centerPt.X + (pt.X - boxWidth / 2) / mag, centerPt.Y + (boxHeight / 2 - pt.Y) / mag);

    private PointD convertClientToSrc(int x, int y) => convertClientToSrc(new Point(x, y));

    private Vector3DBase convertSrcToVector(PointD pt) => Vector3D.SphereVector(pt);

    private (double TiltX, double TiltY) convertSrcToHolder(PointD pt)
    {
        var (sin, cos) = Math.SinCos(-numericBoxTiltXDirection.RadianValue);
        pt = new PointD(cos * pt.X - sin * pt.Y, sin * pt.X + cos * pt.Y);
        //260903Cl 変更: 外周付近では丸め誤差で Asin の引数が 1 をわずかに超えて NaN になるためクランプ。
        //旧の Math.Cos(tiltY) != 0 は double の厳密比較で決して真にならず (cos(π/2)=6e-17)、ガードとして機能していなかった。
        //旧:
        //double tiltY = Math.Asin(2 * pt.Y / (1 + pt.X * pt.X + pt.Y * pt.Y));
        //double tiltX = (Math.Cos(tiltY) != 0) ? Math.Asin(2 * pt.X / (1 + pt.X * pt.X + pt.Y * pt.Y) / Math.Cos(tiltY)) : 0;
        double r2 = 1 + pt.X * pt.X + pt.Y * pt.Y;
        double tiltY = Math.Asin(Math.Clamp(2 * pt.Y / r2, -1, 1));
        double cosY = Math.Cos(tiltY);
        double tiltX = cosY > 1e-12 ? Math.Asin(Math.Clamp(2 * pt.X / r2 / cosY, -1, 1)) : 0;
        var sign = radioButtonTiltY_Plus.Checked ? 1 : -1;
        return (tiltX, sign * tiltY);
    }
    #endregion

    #region graphicsBox のイベント
    private void graphicsBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && e.Clicks == 2)
        {
            var (tiltX, tiltY) = convertSrcToHolder(convertClientToSrc(e.Location));
            skipEventFromThis = true;
            numericBoxTiltX.RadianValue = tiltX;
            numericBoxTiltY.RadianValue = tiltY;
            skipEventFromThis = false;
            numericBoxTilt_ValueChanged(sender, EventArgs.Empty);
        }
    }

    private void graphicsBox_MouseMove(object sender, MouseEventArgs e)
    {
        var (tiltX, tiltY) = convertSrcToHolder(convertClientToSrc(e.Location));
        label1MousePosition.Text = $"Tilt X: {tiltX / Math.PI * 180:f1}°, Y: {tiltY / Math.PI * 180:f1}°";
    }
    #endregion

    #region 晶帯軸をリセットし再計算
    private void setVector()
    {

        var indices = new HashSet<(int u, int v, int w)>();
        if (crystal.A * crystal.B * crystal.C != 0)
        {
            var (U, V, W) = indexControl.Values;
            for (int u = -U; u <= U; u++)
                for (int v = -V; v <= V; v++)
                    for (int w = -W; w <= W; w++)
                    {
                        if (u == 0 && v == 0 && w == 0) continue;
                        if (checkBoxIncludingEquivalent.Checked)
                            foreach (var index in SymmetryStatic.GenerateEquivalentAxes((u, v, w), crystal.Symmetry, false))
                                indices.Add(index);
                        else
                            indices.Add((u, v, w));
                    }
        }
        ZoneAxes = indices.Select(e => ((e.u * crystal.A_Axis + e.v * crystal.B_Axis + e.w * crystal.C_Axis), (e.u, e.v, e.w))).ToList();
    }
    #endregion

    private void buttonLink_Click(object sender, EventArgs e)
    {
        skipEventFromThis = true;
        numericBoxTiltX.Value = numericBoxLinkTiltX.Value;
        numericBoxTiltY.Value = numericBoxLinkTiltY.Value;
        NeutralDirection = HolderRotation * crystal.RotationMatrix;
        skipEventFromThis = false;
        Draw();
    }

    #region その他イベント
    private void checkBox1DegLine_CheckedChanged(object sender, EventArgs e) => Draw();
    private void colorControlUniqueAxis_ColorChanged(object sender, EventArgs e) => Draw();
    private void numericBoxDrawingArea_ValueChanged(object sender, EventArgs e) => Draw();
    //private void numericBoxPrimaryAxisDirection_ValueChanged(object sender, EventArgs e) => Draw(); // 261105Cl 変更前
    /// <summary>傾斜軸の向き (numericBoxTiltXDirection)・Tilt Y の極性 (radioButtonTiltY_Plus) の変更。
    /// 261105Cl 変更 (監査 #18): どちらも HolderRotation を変えるが、結晶は実際には動いていないので、今の方位を保ったまま NeutralDirection を作り直す。
    /// 以前は作り直さなかったので、次に Tilt を動かしたとき 方位 = H_new⁻¹ · H_old · R となって結晶が飛んでいた</summary>
    private void numericBoxPrimaryAxisDirection_ValueChanged(object sender, EventArgs e)
    {
        if (FormDiffractionSimulator?.formMain?.Crystal != null) // InitializeComponent 中は親が未配線
            NeutralDirection = HolderRotation * crystal.RotationMatrix;
        Draw();
    }

    private void numericBoxU_ValueChanged(object sender, EventArgs e)
    {
        setVector();
        Draw();
    }
    #endregion

    #region 親フォームから呼ばれるイベント処理
    /// <summary>親フォームから結晶変更を通知されたときの処理</summary>
    internal void CrystalChanged()
    {
        if (skipEventFromParent) return;

        NeutralDirection = HolderRotation * crystal.RotationMatrix;
        setVector();
        Draw();
    }

    /// <summary>親フォームから回転変更を通知されたときの処理</summary>
    internal void RotationChanged()
    {
        if (skipEventFromParent) return;

        NeutralDirection = HolderRotation * crystal.RotationMatrix;
        Draw();
    }
    #endregion

    private void numericBoxTilt_ValueChanged(object sender, EventArgs e)
    {
        if (skipEventFromThis) return;

        // 261105Cl 追加: 親 (回折シミュレータ) が隠れていると、主窓での回転・結晶の切り替えがこの窓に届かない (FormMain.SetRotation・SetCrystal は
        //   親が見えているときしか中継しない。この窓は親を閉じても開いたまま残る)。そのまま Tilt を動かすと結晶が昔の方位・前の結晶の方位へ飛んでいた。
        //   控えた方位・結晶と今のものが違えば、ほかの窓で回した (替えた) ので、変更前の傾斜 H_old = N · R_expected⁻¹ のもとでの今の方位として N を取り直す
        static bool same(Matrix3D a, Matrix3D b) =>
            Math.Abs(a.E11 - b.E11) < 1e-9 && Math.Abs(a.E12 - b.E12) < 1e-9 && Math.Abs(a.E13 - b.E13) < 1e-9 &&
            Math.Abs(a.E21 - b.E21) < 1e-9 && Math.Abs(a.E22 - b.E22) < 1e-9 && Math.Abs(a.E23 - b.E23) < 1e-9 &&
            Math.Abs(a.E31 - b.E31) < 1e-9 && Math.Abs(a.E32 - b.E32) < 1e-9 && Math.Abs(a.E33 - b.E33) < 1e-9;
        var crystalChanged = !ReferenceEquals(expectedCrystal, crystal);
        if (crystalChanged || !same(expectedRotation, crystal.RotationMatrix))
        {
            NeutralDirection = NeutralDirection * expectedRotation.Inverse() * crystal.RotationMatrix;
            if (crystalChanged) setVector();
        }

        skipEventFromParent = true;
        FormDiffractionSimulator.formMain.SetRotation(HolderRotation.Inverse() * NeutralDirection);
        skipEventFromParent = false;
        expectedRotation = HolderRotation.Inverse() * NeutralDirection; // 261105Cl 追加: いま置いた方位 (写し)
        Draw();
    }

    //260731Cl: 修飾キー無しの矢印キーは WinForms の前処理 (方向キーによるフォーカス移動) に消費され、KeyPreview = true でも
    //Form の KeyDown まで届かない (v.4.920 以降 graphicsBox が Selectable になり無反応化)。当初は ProcessCmdKey で拾っていた。
    //260903Cl 変更: ProcessCmdKey はこのフォームがアクティブなときしか呼ばれないため、Application.AddMessageFilter に登録した
    //PreFilterMessage で UI スレッド全体の WM_KEYDOWN を先取りする方式に置き換えた。これなら ReciPro のどのウィンドウが前面でも
    //(このフォームが Visible かつ checkBoxEnableArrow.Checked なら) 矢印キーでホルダーを傾けられる。
    //メッセージフィルタは同一スレッド内のメッセージにしか掛からないので、他アプリのキー入力を横取りすることは無い。
    //261105Cl 変更 (作者決定 D25): ほかの窓の入力欄の矢印キーまで奪っていたので、この窓がアクティブなときだけに絞った (PreFilterMessage の ActiveForm 判定)。
    //フィルタ方式は残す (この窓の中ではフォーカスのあるコントロールより先に拾える)
    //旧:
    //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    //{
    //    if (checkBoxEnableArrow.Checked && tiltByArrowKey(keyData))
    //        return true;
    //    return base.ProcessCmdKey(ref msg, keyData);
    //}
    public bool PreFilterMessage(ref Message m)
    {
        const int WM_KEYDOWN = 0x0100;
        if (m.Msg != WM_KEYDOWN) return false;
        var key = (Keys)(int)m.WParam;
        if (key is not (Keys.Left or Keys.Right or Keys.Up or Keys.Down)) return false; //安価な判定を先に
        //if (!Visible || !checkBoxEnableArrow.Checked) return false; // 261105Cl 変更前: どの窓が前面でも矢印キーを奪い、主窓などの入力欄でカーソルを動かせなかった
        // 261105Cl 変更 (作者決定 D25): この窓が前面 (アクティブ) のときだけ傾きに使う。ほかの窓 (この窓から開いたダイアログを含む) では素通しにする
        if (!Visible || !checkBoxEnableArrow.Checked || ActiveForm != this) return false; // 261105Cl
        //修飾キー併用時は tiltByArrowKey 側の完全一致で弾かれる
        return tiltByArrowKey(key | ModifierKeys);
    }

    /// <summary>260731Cl 追加: 修飾キーの付かない矢印キーならホルダーを傾けて true を返す</summary>
    private bool tiltByArrowKey(Keys keyData)
    {
        //Alt / Shift / Ctrl 併用時は keyData に修飾ビットが乗るため、ここの完全一致で弾かれる
        switch (keyData)
        {
            case Keys.Left: numericBoxTiltX.Value -= numericBoxArrowStep.Value; return true;
            case Keys.Right: numericBoxTiltX.Value += numericBoxArrowStep.Value; return true;
            case Keys.Up: numericBoxTiltY.Value += numericBoxArrowStep.Value; return true;
            case Keys.Down: numericBoxTiltY.Value -= numericBoxArrowStep.Value; return true;
            default: return false;
        }
    }

    //260731Cl 変更: 傾斜処理は tiltByArrowKey に集約し、ProcessCmdKey から呼ぶようにした。
    //260903Cl 削除: PreFilterMessage が先に消費するので KeyDown ハンドラは到達不能。Designer の配線ごと外した。
    //旧:
    //private void FormDiffractionSimulatorHolder_KeyDown(object sender, KeyEventArgs e)
    //{
    //    if (checkBoxEnableArrow.Checked && !e.Alt && !e.Shift && !e.Control
    //        && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
    //    {
    //        e.SuppressKeyPress = true;
    //        e.Handled = true;
    //        if (e.KeyCode == Keys.Left)
    //            numericBoxTiltX.Value -= numericBoxArrowStep.Value;
    //        else if (e.KeyCode == Keys.Right)
    //            numericBoxTiltX.Value += numericBoxArrowStep.Value;
    //        else if (e.KeyCode == Keys.Up)
    //            numericBoxTiltY.Value += numericBoxArrowStep.Value;
    //        else if (e.KeyCode == Keys.Down)
    //            numericBoxTiltY.Value -= numericBoxArrowStep.Value;
    //    }
    //}
    //private void FormDiffractionSimulatorHolder_KeyDown(object sender, KeyEventArgs e)
    //{
    //    if (checkBoxEnableArrow.Checked && tiltByArrowKey(e.KeyData))
    //    {
    //        e.SuppressKeyPress = true;
    //        e.Handled = true;
    //    }
    //}


    private void checkBoxEnableArrow_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxEnableArrow.Checked) graphicsBox.Focus();
    }

    private void buttonRotate180_Click(object sender, EventArgs e)
    {
        //FormDiffractionSimulator.formMain.Rotate((0, 0, 1), Math.PI); // 261105Cl 変更前: 主窓で「軸/面を固定」していると、その結晶軸 (面法線) のまわりに回っていた
        FormDiffractionSimulator.formMain.Rotate((0, 0, 1), Math.PI, ignoreFixedAxis: true); // 261105Cl 変更 (作者決定 D25): 常にビーム軸 (画面に垂直) のまわり
    }

    private void checkBoxIncludingEquivalent_CheckedChanged(object sender, EventArgs e)
    {
        setVector(); Draw();
    }

    //260903Cl 変更: 兄弟フォーム (FormDiffractionSimulator 等) と同じく graphicsBox.Resize に配線し、名前も合わせた
    //旧: private void FormDiffractionSimulatorHolder_Resize(object sender, EventArgs e)
    private void graphicsBox_Resize(object sender, EventArgs e)
    {
        Draw();
    }
}


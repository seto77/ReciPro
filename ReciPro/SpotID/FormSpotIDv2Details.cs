using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ReciPro;

public partial class FormSpotIDv2Details : FormBase
{
    public FormSpotIDV2 FormSpotID;
    public int BoxWide => scalablePictureBoxAdvanced.PictureSize.Width;
    public int BoxHeight => scalablePictureBoxAdvanced.PictureSize.Height;

    // 260718Cl 追加: 詳細ビュー専用の独立 PseudoBitmap と、その生成元になった main 側 SrcValuesGray 参照。
    // SrcValuesGray 参照 (=元画像/フィルタ結果) と Width が変わらない限り再利用し、毎回の再生成を避ける。
    private PseudoBitmap detailPseudoBitmap;
    private double[] detailPseudoBitmapSource;

    public FormSpotIDv2Details() { InitializeComponent(); HelpPage = "11-spot-id-v2"; } //260529Cl HelpPage 追加

    public void SetData(bool renewImage = true)
    {
        // 260718Cl 追加: GuiCapture が本フォームを単独 (パラメータレス ctor) で生成すると FormSpotID が
        // 未配線 (null) になり、以降の FormSpotID 参照で NullReferenceException になる。親が未設定なら何もしない。
        if (FormSpotID == null)
            return;

        if (renewImage)
        {
            // 260718Cl 変更: メインフォームと同一の PseudoBitmap インスタンスを共有していたが、詳細ビュー側の再描画
            // (Zoom/Center 設定→GetImage) で共有表示バッファ destBmp が破棄され、メイン画像の PictureBox が破棄済み
            // Bitmap を描画 → GDI 例外 (Image.get_Size: Parameter is not valid) → メイン画像領域が赤い×表示になって
            // いた。詳細ビュー用に独立した PseudoBitmap を持ち (グレーソース配列 SrcValuesGray のみ参照共有・表示バッ
            // ファは各自独立)、共有を断つ。
            // 旧: scalablePictureBoxAdvanced.PseudoBitmap = FormSpotID.scalablePictureBoxAdvanced.PseudoBitmap;
            // 260718Cl 追加: 独立インスタンスは SrcValuesGray 参照 (=元画像/フィルタ結果) と Width が変わったときだけ
            // 作り直す。明るさ・コントラスト・ズーム操作では SrcValuesGray は不変で、詳細ビューは常に gray-linear 表示
            // のため作り直しても見た目は同じ。毎回 new すると initFilter が大きな配列を確保し、ScalablePictureBox の
            // setter は旧インスタンスを Dispose しないため表示バッファ destBmp が GDI ハンドルごとリークしていた。
            var mainPb = FormSpotID.scalablePictureBoxAdvanced.PseudoBitmap;
            var mainGray = mainPb?.SrcValuesGray;
            if (mainGray != null)
            {
                if (detailPseudoBitmap == null || !ReferenceEquals(detailPseudoBitmapSource, mainGray) || detailPseudoBitmap.Width != mainPb.Width)
                {
                    detailPseudoBitmap = new PseudoBitmap(mainGray, mainPb.Width);
                    detailPseudoBitmapSource = mainGray;
                }
                scalablePictureBoxAdvanced.PseudoBitmap = detailPseudoBitmap;
            }
            else
                scalablePictureBoxAdvanced.PseudoBitmap = mainPb; // グレー値未ロード時のみ共有 (自前生成しない)
        }

        if (FormSpotID.bindingSourceObsSpots.Current == null)
            return;

        var srcValues = scalablePictureBoxAdvanced.PseudoBitmap.SrcValuesGray;

        if (srcValues == null)
            return;

        var pixelWidth = scalablePictureBoxAdvanced.PseudoBitmap.Width;
        int selectedIndex;
        try
        {
            selectedIndex = (int)((DataRowView)FormSpotID.bindingSourceObsSpots.Current).Row["No"];
        }
        catch
        {
            return;
        }

        var (_, _, Range, X0, Y0, H1, H2, Theta, Eta, A, B0, Bx, By, _) = FormSpotID.dataSet.DataTableSpot.GetPrms(selectedIndex);
        var funcs = new List<Marquardt.Function>
            {
                new Marquardt.Function(Marquardt.FuncType.PV2E, X0, Y0, H1, H2, Theta, Eta, A),
                new Marquardt.Function(Marquardt.FuncType.Plane, B0, Bx, By)
            };

        var srcList = new List<(double x, double y, double z)>();
        var calcList = new List<(double x, double y, double z)>();
        var bgList = new List<(double x, double y, double z)>();
        for (int h = 0; h < srcValues.Length / pixelWidth; h++)
            for (int w = 0; w < pixelWidth; w++)
                if ((w - X0) * (w - X0) + (h - Y0) * (h - Y0) < (Range + 2) * (Range + 2))
                {
                    srcList.Add((w, h, srcValues[h * pixelWidth + w]));
                    bgList.Add((w, h, funcs[1].GetValue(w, h)));
                    calcList.Add((w, h, funcs[0].GetValue(w, h) + funcs[1].GetValue(w, h)));
                }

        var src = srcList.AsParallel();
        var calc = calcList.AsParallel();
        var bg = bgList.AsParallel();

        //scalablePictureBoxAdvancedのシンボルとしてN-S方向, W-E方向、などの線をセット
        var sqrt2 = Math.Sqrt(2);
        scalablePictureBoxAdvanced.Symbols = new List<ScalablePictureBox.Symbol>(FormSpotID.scalablePictureBoxAdvanced.Symbols);

        PointD pt1, pt2;
        Profile obsProfile, calcProfile, bgProfile;
        //SW - NE
        pt1 = new PointD(X0 - Range / sqrt2, Y0 + Range / sqrt2);
        pt2 = new PointD(X0 + Range / sqrt2, Y0 - Range / sqrt2);
        scalablePictureBoxAdvanced.Symbols.Add(new ScalablePictureBox.Symbol("", pt1, pt2, Color.OrangeRed));
        obsProfile = new Profile(ImageProcess.GetLineProfile(src, pt1, pt2, 1, 1), Color.Blue);
        calcProfile = new Profile(ImageProcess.GetLineProfile(calc, pt1, pt2, 1, 1), Color.Red);
        bgProfile = new Profile(ImageProcess.GetLineProfile(bg, pt1, pt2, 1, 1), Color.Pink);
        graphControlSWtoNE.ClearProfile();
        graphControlSWtoNE.AddProfiles(new[] { obsProfile, calcProfile, bgProfile });

        //NW-SE
        pt1 = new PointD(X0 - Range / sqrt2, Y0 - Range / sqrt2);
        pt2 = new PointD(X0 + Range / sqrt2, Y0 + Range / sqrt2);
        scalablePictureBoxAdvanced.Symbols.Add(new ScalablePictureBox.Symbol("", pt1, pt2, Color.Purple));
        obsProfile = new Profile(ImageProcess.GetLineProfile(src, pt1, pt2, 1, 1), Color.Blue);
        calcProfile = new Profile(ImageProcess.GetLineProfile(calc, pt1, pt2, 1, 1), Color.Red);
        bgProfile = new Profile(ImageProcess.GetLineProfile(bg, pt1, pt2, 1, 1), Color.Pink);
        graphControlNWtoSE.ClearProfile();
        graphControlNWtoSE.AddProfiles(new[] { obsProfile, calcProfile, bgProfile });

        //N-S
        pt1 = new PointD(X0, Y0 - Range);
        pt2 = new PointD(X0, Y0 + Range);
        scalablePictureBoxAdvanced.Symbols.Add(new ScalablePictureBox.Symbol("", pt1, pt2, Color.Red));
        obsProfile = new Profile(ImageProcess.GetLineProfile(src, pt1, pt2, 1, 1), Color.Blue);
        calcProfile = new Profile(ImageProcess.GetLineProfile(calc, pt1, pt2, 1, 1), Color.Red);
        bgProfile = new Profile(ImageProcess.GetLineProfile(bg, pt1, pt2, 1, 1), Color.Pink);
        graphControlNtoS.ClearProfile();
        graphControlNtoS.AddProfiles(new[] { obsProfile, calcProfile, bgProfile });

        //W-E
        pt1 = new PointD(X0 - Range, Y0);
        pt2 = new PointD(X0 + Range, Y0);
        scalablePictureBoxAdvanced.Symbols.Add(new ScalablePictureBox.Symbol("", pt1, pt2, Color.Orange));
        obsProfile = new Profile(ImageProcess.GetLineProfile(src, pt1, pt2, 1, 1), Color.Blue);
        calcProfile = new Profile(ImageProcess.GetLineProfile(calc, pt1, pt2, 1, 1), Color.Red);
        bgProfile = new Profile(ImageProcess.GetLineProfile(bg, pt1, pt2, 1, 1), Color.Pink);
        graphControlWtoE.ClearProfile();
        graphControlWtoE.AddProfiles(new[] { obsProfile, calcProfile, bgProfile });

        //中心位置や倍率を設定
        scalablePictureBoxAdvanced.ZoomAndCenter = (BoxWide / Range / 4, new PointD(X0, Y0));
    }

    private void FormSpotDetails_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        // 260718Cl 追加: 単独生成 (GuiCapture) で FormSpotID 未配線のときは親チェックボックス更新をスキップ
        if (FormSpotID != null)
            FormSpotID.checkBoxDetailsOfSpot.Checked = false;
    }

    private void FormSpotDetails_VisibleChanged(object sender, EventArgs e)
    {
        if (this.Visible)
            SetData();
    }

    private void label4_Click(object sender, EventArgs e)
    {

    }
}


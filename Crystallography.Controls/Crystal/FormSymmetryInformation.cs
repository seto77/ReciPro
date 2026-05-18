#region using, namespace
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D; // 260504Cl
using System.Drawing.Text; // 260504Cl
using System.Text.RegularExpressions; // 260427Cl
using System.Windows.Forms;

namespace Crystallography.Controls;
#endregion
public partial class FormSymmetryInformation : FormBase
{
    #region プロパティ
    /// <summary>表示対象の <see cref="Crystallography.Crystal"/>。<see cref="CrystalControl"/> から委譲。</summary>
    public Crystal Crystal => CrystalControl.Crystal;

    /// <summary>結晶情報を保持する親の <see cref="CrystalControl"/>。Load 時に CrystalChanged を購読する。</summary>
    public CrystalControl CrystalControl;

    /// <summary>4-index (Miller-Bravais) 表記の入力欄 (i-axis) を表示するかどうか。trigonal/hexagonal で true を想定。</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MillerBravais { get => indexControlPlane1.MillerBravais; set => indexControlPlane1.MillerBravais = indexControlPlane2.MillerBravais = value; }

    #endregion 

    #region コンストラクタ、ロード、クローズ
    /// <summary>デザイナ生成のコントロールを初期化する。</summary>
    public FormSymmetryInformation()
    {
        InitializeComponent(); // (260426Ch)
    }

    /// <summary>
    /// Load イベントハンドラ。<see cref="CrystalControl.CrystalChanged"/> を購読し、初期表示として <see cref="ChangeCrystal"/> を呼ぶ。
    /// </summary>
    private void FormCrystallographicInformation_Load(object sender, EventArgs e)
    {
        CrystalControl.CrystalChanged += (_, _) => ChangeCrystal(); // (260426Ch) 1 行 handler をインライン化
        // 260429Cl 追加: GraphicsBox サイズ変更時に図を再描画
        graphicsBoxSymmetryElements.SizeChanged += (_, _) => UpdateDiagrams();
        graphicsBoxGeneralPositions.SizeChanged += (_, _) => UpdateDiagrams();
        ChangeCrystal();
    }

    /// <summary>
    /// FormClosing イベントハンドラ。閉じる代わりに非表示にし、フォームのインスタンスを再利用する。
    /// </summary>
    private void FormCrystallographicInformation_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Visible = false; // (260426Ch)
    }
    #endregion

    #region 文字列をlatexへ変換
    /// <summary>結晶対称性シンボル文字列を WpfMath 用 math-mode LaTeX へ変換する。</summary>
    /// <param name="str">変換対象。</param>
    /// <param name="sfStyle">Schoenflies 系 (SG_SF, PG_SF) のとき true。</param>
    /// <param name="plain">単語 (CrystalSystem) を <c>\mathrm{}</c> でラップ。</param>
    /// <param name="spaced">HM 系の対称要素軸間に <c>\,</c> を挿入。</param>
    /// <param name="noBar">ExtinctionRule の indices 接頭辞のみ <c>-h/-k/-l</c> を overline 化。</param>
    private static string ToLatex(string str, bool sfStyle = false, bool plain = false, bool spaced = false, bool noBar = false)
    {
        if (string.IsNullOrEmpty(str)) return string.Empty;
        if (plain || str == "Unknown") return $@"\mathrm{{{str}}}";

        if (sfStyle)
        {
            var caret = str.IndexOf('^');
            var main = caret >= 0 ? str[..caret] : str;
            var sup = caret >= 0 ? str[(caret + 1)..] : "";
            var result = main.Length > 1 ? $"{main[0]}_{{{main[1..]}}}" : main;
            if (sup.Length > 0)
                result += $"^{{{sup}}}";
            return result;
        }

        // 260427Cl: HM 末尾の補助記号 ("Hex"/"Rho" の三方晶系設定 / "(1)"/"(2)" の原点選択) を一旦取り外し、
        // 最後に "\,\,_{...}" (二重 thin space + 下付き) で独立した小さなラベルとして再付与。
        // 先に取り外しておかないと spaced regex が "H" を格子文字、"(" を非ブロックと扱って表示が崩れる。
        string suffix = "";
        var sfxMatch = SuffixRegex().Match(str);
        if (sfxMatch.Success)
        {
            var s = sfxMatch.Value;
            // (N) は math mode の () + digit でアップライト描画されるのでそのまま、Hex/Rho は \mathrm{} で italic 化を防ぐ。
            var inner = s[0] == '(' ? s : $@"\mathrm{{{s}}}";
            suffix = $@"\,\,_{{{inner}}}";
            str = str[..^s.Length];
        }
        // 260427Cl: Hall シンボルの " (例: -R32"c) は WpfMath が解釈できないので '' (二重プライム) に置換。
        str = str.Replace("\"", "''");

        // axis ブロック単位 (格子文字 / 回転軸±スクリュー±鏡面 / 単独鏡面) で \, を挿入。
        // 連続するブロック同士の境目だけにマッチさせるので "=" 区切りや単一ブロックは無視される。
        // (?<!su) は単独鏡面 [mabcdn] に対する保険: 例 "P2sub1=..." で 2sub1 の lookahead が =
        // で失敗した後に、エンジンが "sub" の b を mirror plane として再マッチしないようガードする。
        // これがないと "P\,2sub\,1=..." となり、後段の sub1→_1 置換が効かなくなる。
        if (spaced)
            str = AxisBlockBoundaryRegex().Replace(str, @"$1\,");

        for (int n = 1; n <= 5; n++)
            str = str.Replace($"sub{n}", $"_{n}");
        // "-N" (digit) → "\bar{N}". HM では P-3m → P\bar{3}m、ExtinctionRule では [01-1] → [01\bar{1}]。
        // 条件式部の "2h-k=4n" 等は "-letter" なのでこの置換にかからず literal のまま。
        for (int n = 0; n <= 9; n++)
            str = str.Replace($"-{n}", $"\\bar{{{n}}}");
        str = str.Replace("⊥", @"\perp ");
        str = str.Replace("//", @"\parallel "); // 260427Cl: ExtinctionRule の "2sub1//[100]" 等を ∥ 記号で描画
        // 連続する条件式の境目 (例: "...=2n k+l=2n" → "...=2n, k+l=2n") にカンマを挿入。
        // ExtinctionRule の F centering 等でしか出ない \dn[空白][letter] パターン限定なので副作用なし。
        str = ConditionSeparatorRegex().Replace(str, ", ");
        // ExtinctionRule の indices 接頭辞 (最初の ":" まで) のみ "-h/-k/-l" を \bar{...} に変換。
        // 例: "h-hl: 2h-l=4n: d⊥[110]" → "h\bar{h}l: 2h-l=4n: d\perp [110]" (条件式の "2h-l" や説明部はそのまま)
        if (noBar)
        {
            var colonIdx = str.IndexOf(':');
            if (colonIdx > 0)
                str = IndicesPrefixBarRegex().Replace(str[..colonIdx], @"\bar{$1}") + str[colonIdx..];
        }
        return str + suffix; // 三方晶系 Hex/Rho 接尾辞があれば末尾に下付きで再付与
    }

    [GeneratedRegex(@"(Hex|Rho|\(\d\))$")]
    private static partial Regex SuffixRegex();

    [GeneratedRegex(@"([PABCFIHR]|-?\d(?:sub\d)?(?:/[mabcdn])?|(?<!su)[mabcdn])(?=[PABCFIHR]|-?\d(?:sub\d)?(?:/[mabcdn])?|(?<!su)[mabcdn])")]
    private static partial Regex AxisBlockBoundaryRegex();

    [GeneratedRegex(@"(?<=\dn)\s+(?=[a-zA-Z])")]
    private static partial Regex ConditionSeparatorRegex();

    [GeneratedRegex(@"-([hkl])")]
    private static partial Regex IndicesPrefixBarRegex();
    #endregion

    #region 出現則用のLabelLatex生成
    // 260427Cl: 結晶切替の度に N 個の LabelLaTeX を生成するので Font/Padding は static で共有 (Font は IDisposable だがアプリ生存期間)。
    private static readonly Font ExtinctionRuleFont = new("Segoe UI", 13F);
    private static readonly Padding ExtinctionRuleMargin = new(0, 0, 0, 2);

    /// <summary>
    /// <see cref="flowLayoutPanelExtinctionRule"/> に積む 1 行ぶんの <see cref="LabelLaTeX"/> を生成する (260427Cl 追加)。
    /// </summary>
    /// <param name="latex">行に描画する LaTeX 文字列。</param>
    /// <returns>AutoSize 有効・Segoe UI 13pt・縁取り 0.6 で初期化した <see cref="LabelLaTeX"/>。</returns>
    private static LabelLaTeX MakeExtinctionRuleLabel(string latex) => new()
    {
        AutoSize = true,
        Font = ExtinctionRuleFont,
        Margin = ExtinctionRuleMargin,
        Thickness = 0.6,
        Text = latex,
    };
    #endregion

    #region ChangeCrystal() 結晶が変更されたとき 

    /// <summary>現在の <see cref="Crystal"/> の対称性情報をフォーム上の各コントロールへ反映する。CrystalControl.CrystalChanged からも呼ばれる。</summary>
    public void ChangeCrystal()
    {
        numericBox_ValueChanged(this, EventArgs.Empty); // (260426Ch) 不要な object/EventArgs 生成を避ける
        SetWyckoffPosition();


        var symmetry = Crystal.Symmetry;

        // 260506Cl 追加: 点群が変わったら、その点群の既定 test point で numericBoxPosition* をリセット。
        // 同じ点群のままで空間群だけが切り替わった場合は、ユーザーが numericBoxPosition* に入れた値を保持する。
        // radioButtonDirection* も同様に、非 ortho → ortho に切り替わった瞬間 (初回含む) だけ既定 C に戻す。
        // 両者まとめて SkipEvent でガードし、ChangeCrystal 末尾の UpdateDiagrams() に再描画を一本化する。
        SkipEvent = true;
        try
        {
            if (symmetry.PointGroupNumber != _previousPointGroupNumber)
            {
                _previousPointGroupNumber = symmetry.PointGroupNumber;
                var (tx, ty, tz) = SymmetryDiagramPositions.GetTestPoint(symmetry);
                numericBoxPositionA.Value = tx;
                numericBoxPositionB.Value = ty;
                numericBoxPositionC.Value = tz;
            }

            bool isOrtho = symmetry.CrystalSystemNumber == 3;
            radioButtonDirectionA.Enabled = radioButtonDirectionB.Enabled = radioButtonDirectionC.Enabled = isOrtho;
            if (!isOrtho)
                SetSelectedDirection(SymmetryDiagramCommon.ResolveProjectionAxis(symmetry, ProjectionAxis.C));
            else if (!_previousIsOrtho)
                SetSelectedDirection(ProjectionAxis.C);
            _previousIsOrtho = isOrtho;
        }
        finally { SkipEvent = false; }

        labelLaTexNumber.Text = $"{symmetry.SpaceGroupNumber}: {symmetry.SpaceGroupSubNumber}";

        // 260427Cl 追加: LabelLaTeX 各種への流し込み (richTextBox 群と並走表示)。
        // 空間群・点群 HM 系 (HM, HM_full, PG_HM, LG) は対称要素軸ごとに thin space で区切る。
        // Hall は表記体系が異なるため spaced 指定なし。
        labelLaTexSG_HM.Text = ToLatex(symmetry.SpaceGroupHMStr, spaced: true);
        labelLaTexHM_full.Text = ToLatex(symmetry.SpaceGroupHMfullStr, spaced: true);
        labelLaTexSG_SF.Text = ToLatex(symmetry.SpaceGroupSFStr, sfStyle: true);
        labelLaTexSG_Hall.Text = ToLatex(symmetry.SpaceGroupHallStr, spaced: true);//やっぱりスペースはあった方がいい
        labelLaTexPG_HM.Text = ToLatex(symmetry.PointGroupHMStr, spaced: true);
        labelLaTexPG_SF.Text = ToLatex(symmetry.PointGroupSFStr, sfStyle: true);
        //labelLaTexLG.Text = ToLatex(symmetry.LaueGroupStr, spaced: true);
        labelLaTexCS.Text = ToLatex(symmetry.CrystalSystemStr, plain: true);

        // 260427Cl 追加: ExtinctionRule は 1 行 1 LabelLaTeX で FlowLayoutPanel に積む (AutoScroll でスクロール)。
        // hkl 算術式中の "-h"/"-1" は字面通りに残したいので noBar:true。
        flowLayoutPanelExtinctionRule.SuspendLayout();
        // Controls.Clear() は子を Dispose しないため、LabelLaTeX が保持する Bitmap が GC まで残る。
        // Control.Dispose() は内部で Parent.Controls.Remove(this) を呼ぶため後ろから index で回す
        // (前から foreach するとコレクション変更中の列挙で例外になる)。Dispose 後は Controls が空になるので Clear 不要。
        for (int i = flowLayoutPanelExtinctionRule.Controls.Count - 1; i >= 0; i--)
            flowLayoutPanelExtinctionRule.Controls[i].Dispose();
        var rules = symmetry.ExtinctionRuleStr;
        if (rules == null || rules.Length == 0)
            flowLayoutPanelExtinctionRule.Controls.Add(MakeExtinctionRuleLabel(@"\mathrm{No\ Condition}"));
        else
            foreach (var rule in rules)
                flowLayoutPanelExtinctionRule.Controls.Add(MakeExtinctionRuleLabel(ToLatex(rule, noBar: true)));
        flowLayoutPanelExtinctionRule.ResumeLayout(true);

        // 260429Cl 追加: 対称要素・一般位置の図を再描画
        UpdateDiagrams();
    }

    // 260429Cl 追加: 前回 render 時の状態を保持して、SizeChanged 多発・初期 Load 時の重複 render を抑制
    private int _renderedSeriesNumber = -1;
    // 260506Cl 改: 各図のキャッシュキーに axis を畳み込む。Gen 側はさらに test point を含める。
    // 初期値はデフォルト (Empty Size, A) だが、初回呼び出しは _renderedSeriesNumber=-1 で seriesChanged=true となり強制再描画されるので問題ない。
    private (Size Size, ProjectionAxis Axis) _renderedKeyElem;
    private (Size Size, ProjectionAxis Axis, double X, double Y, double Z) _renderedKeyGen;
    // 260506Cl 追加: 点群追跡 (-1=未設定)、直前の ortho 状態 (false 初期値で初回 ortho 進入時に既定 C へ落とす)、および ChangeCrystal 中のイベント抑止フラグ。
    private int _previousPointGroupNumber = -1;
    private bool _previousIsOrtho;
    private bool SkipEvent;

    /// <summary>(260506Cl 追加) radioButtonDirection* の現在の選択を <see cref="ProjectionAxis"/> として取得。</summary>
    private ProjectionAxis SelectedDirection =>
        radioButtonDirectionA.Checked ? ProjectionAxis.A :
        radioButtonDirectionB.Checked ? ProjectionAxis.B : ProjectionAxis.C;

    /// <summary>(260506Cl 追加) <paramref name="axis"/> に該当する radioButtonDirection* を Checked に。
    /// 呼び出し側で <see cref="SkipEvent"/>=true にしておくこと (CheckedChanged 連鎖で UpdateDiagrams が走らないように)。</summary>
    private void SetSelectedDirection(ProjectionAxis axis)
    {
        radioButtonDirectionA.Checked = axis == ProjectionAxis.A;
        radioButtonDirectionB.Checked = axis == ProjectionAxis.B;
        radioButtonDirectionC.Checked = axis == ProjectionAxis.C;
    }

    /// <summary>260429Cl 追加: graphicsBoxSymmetryElements / graphicsBoxGeneralPositions を再描画する。
    /// ChangeCrystal および両 GraphicsBox の Resize から呼ばれる。Crystal 変化があれば両図、
    /// サイズだけが変わった場合はその箱だけを再描画して無駄な render を避ける。
    /// (260506Cl) 一般位置図は test point 変化でも再描画。対称要素図は test point に依存しないので不要。</summary>
    private void UpdateDiagrams()
    {
        int sn = Crystal.SymmetrySeriesNumber;
        bool seriesChanged = sn != _renderedSeriesNumber;
        _renderedSeriesNumber = sn;
        var axis = SelectedDirection;

        var keyElem = (graphicsBoxSymmetryElements.ClientSize, axis);
        if (seriesChanged || keyElem != _renderedKeyElem)
        {
            graphicsBoxSymmetryElements.Image?.Dispose();
            graphicsBoxSymmetryElements.Image = SymmetryDiagramElements.RenderSymmetryElements(sn, keyElem.ClientSize, axis);
            _renderedKeyElem = keyElem;
        }

        var testPoint = (numericBoxPositionA.Value, numericBoxPositionB.Value, numericBoxPositionC.Value);
        var keyGen = (graphicsBoxGeneralPositions.ClientSize, axis, testPoint.Item1, testPoint.Item2, testPoint.Item3);
        if (seriesChanged || keyGen != _renderedKeyGen)
        {
            graphicsBoxGeneralPositions.Image?.Dispose();
            graphicsBoxGeneralPositions.Image = SymmetryDiagramPositions.RenderGeneralPositions(sn, keyGen.ClientSize, axis, testPoint);
            _renderedKeyGen = keyGen;
        }
    }

    /// <summary>260506Cl 追加: numericBoxPosition* の ValueChanged に紐付く handler。
    /// ユーザーが test point を変更したら一般位置図のみを再描画する。
    /// ChangeCrystal 経由の reset 中は <see cref="SkipEvent"/> で抑止して 3 連発を防ぐ。</summary>
    private void numericBoxPosition_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        UpdateDiagrams();
    }

    /// <summary>260506Cl 追加: radioButtonDirection* の CheckedChanged に紐付く handler。
    /// 投影軸が切り替わったら対称要素図と一般位置図の両方を再描画する。
    /// ユーザー操作では radio ペアの off→on と on→off で CheckedChanged が二重発火するので、
    /// Checked 側 (= 新しく ON になった方) だけ反応させて UpdateDiagrams を 1 回に絞る。</summary>
    private void radioButtonDirection_CheckedChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        if (sender is RadioButton rb && !rb.Checked) return;
        UpdateDiagrams();
    }
    #endregion

    #region 面間隔の計算、軸間、軸面間の角度計算
    /// <summary>
    /// 平面 (h₁k₁l₁, h₂k₂l₂) と軸 (u₁v₁w₁, u₂v₂w₂) の入力欄が変化したとき、
    /// 面間距離・軸長・面間角・軸間角・面と軸のなす角・zone axis を再計算して各表示欄を更新する (260427Cl)。
    /// </summary>
    private void numericBox_ValueChanged(object sender, EventArgs e)
    {
        (int h,int k,int l) plane1 = indexControlPlane1.Values, plane2 = indexControlPlane2.Values;
        (int u, int v, int w) axis1 =indexControlAxis1.Values, axis2 = indexControlAxis2.Values;

        numericBoxLengthPlane1.Value = Crystal.GetLengthPlane(plane1.h, plane1.k, plane1.l) * 10; // (260427Ch)
        numericBoxLengthPlane2.Value = Crystal.GetLengthPlane(plane2.h, plane2.k, plane2.l) * 10; // (260427Ch)
        numericBoxLengthAxis1.Value = Crystal.GetLengthAxis(axis1.u, axis1.v, axis1.w) * 10; // (260427Ch)
        numericBoxLengthAxis2.Value = Crystal.GetLengthAxis(axis2.u, axis2.v, axis2.w) * 10; // (260427Ch)

        numericBoxAnglePlanes.Value = Crystal.GetAnglePlanes(plane1.h, plane1.k, plane1.l, plane2.h, plane2.k, plane2.l) * 180 / Math.PI; // (260427Ch)
        numericBoxAngleAxes.Value = Crystal.GetAngleAxes(axis1.u, axis1.v, axis1.w, axis2.u, axis2.v, axis2.w) * 180 / Math.PI; // (260427Ch)
        numericBoxAnglePlaneAxis1.Value = Crystal.GetAnglePlaneAxis(plane1.h, plane1.k, plane1.l, axis1.u, axis1.v, axis1.w) * 180 / Math.PI; // (260427Ch)
        numericBoxAnglePlaneAxis2.Value = Crystal.GetAnglePlaneAxis(plane2.h, plane2.k, plane2.l, axis2.u, axis2.v, axis2.w) * 180 / Math.PI; // (260427Ch)

        textBoxZoneAxis.Text = $"[{Crystal.GetZoneAxis(plane1.h, plane1.k, plane1.l, plane2.h, plane2.k, plane2.l)} ]";
        textBoxZonePlane.Text = $"({Crystal.GetZoneAxis(axis1.u, axis1.v, axis1.w, axis2.u, axis2.v, axis2.w)} )";
    }
    #endregion

    #region ワイコフ位置の設定
    /// <summary>現在の空間群の lattice centering と Wyckoff position を <see cref="dataSet"/> 内の Wyckoff テーブルへ書き込む。</summary>
    /// <remarks> 1 ポジションあたり座標が 4 個を超える場合は 4 個ずつ複数行に分割して追加する。</remarks>
    private void SetWyckoffPosition()
    {
        var table = dataSet.Tables[0]; // (260426Ch)
        table.Clear();
        var centeringRow = Crystal.Symmetry.LatticeTypeStr switch
        {
            "P" => new object[] { "-", "-", "-", "(0,0,0)+", "", "", "" },
            "A" => ["-", "-", "-", "(0,0,0)+", "(0,1/2,1/2)+", "", ""],
            "B" => ["-", "-", "-", "(0,0,0)+", "(1/2,0,1/2)+", "", ""],
            "C" => ["-", "-", "-", "(0,0,0)+", "(1/2,1/2,0)+", "", ""],
            "F" => ["-", "-", "-", "(0,0,0)+", "(0,1/2,1/2)+", "(1/2,0,1/2)+", "(1/2,1/2,0)+"], // (260426Ch) 3 番目の F centering 座標 typo を修正
            "I" => ["-", "-", "-", "(0,0,0)+", "(1/2,1/2,1/2)+", "", ""],
            "H" => ["-", "-", "-", "(0,0,0)+", "(1/3,2/3,2/3)+", "(2/3,1/3,1/3)+", ""],
            _ => null
        };
        if (centeringRow != null)
            table.Rows.Add(centeringRow);

        Crystal.Symmetry = SymmetryStatic.Symmetries[Crystal.SymmetrySeriesNumber];

        foreach (var position in SymmetryStatic.WyckoffPositions[Crystal.SymmetrySeriesNumber])
        {
            var positions = position.PositionStr;
            int len = positions.Length;
            for (int j = 0; j < len; j += 4)
            {
                var row = new object[7];
                if (j == 0)
                {
                    row[0] = position.Multiplicity;
                    row[1] = position.WyckoffLetter;
                    row[2] = position.SiteSymmetry;
                }
                else
                {
                    row[0] = row[1] = row[2] = "";
                }
                for (int offset = 0; offset < 4; offset++)
                    row[3 + offset] = j + offset < len ? positions[j + offset] : "";

                table.Rows.Add(row);
            }
        }
    }
    #endregion

    #region 対称要素図 / 一般位置図のクリップボードコピー (260504Cl 追加)

    private void buttonCopySymmetryElements_Click(object sender, EventArgs e)
    {
        int sn = Crystal.SymmetrySeriesNumber;
        var size = graphicsBoxSymmetryElements.ClientSize;
        var axis = SelectedDirection;
        if (radioButtonEmf.Checked)
            CopyAsMetafile(g => SymmetryDiagramElements.DrawSymmetryElements(g, size, sn, axis));
        else
            Clipboard.SetDataObject(SymmetryDiagramElements.RenderSymmetryElements(sn, size, axis), true);
    }

    private void buttonCopyGeneralPositions_Click(object sender, EventArgs e)
    {
        int sn = Crystal.SymmetrySeriesNumber;
        var size = graphicsBoxGeneralPositions.ClientSize;
        var axis = SelectedDirection;
        var testPoint = (numericBoxPositionA.Value, numericBoxPositionB.Value, numericBoxPositionC.Value);
        if (radioButtonEmf.Checked)
            CopyAsMetafile(g => SymmetryDiagramPositions.DrawGeneralPositions(g, size, sn, axis, testPoint));
        else
            Clipboard.SetDataObject(SymmetryDiagramPositions.RenderGeneralPositions(sn, size, axis, testPoint), true);
    }

    /// <summary>EMF+ クリップボードコピーの共通設定 (背景白・AntiAlias) を済ませてから <paramref name="drawDiagram"/> を呼ぶ。</summary>
    private void CopyAsMetafile(Action<Graphics> drawDiagram)
        => ClipboardMetafileHelper.PutDrawingOnClipboardAsEnhMetafile(Handle, g =>
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.Clear(Color.White);
            drawDiagram(g);
        });
    #endregion
}

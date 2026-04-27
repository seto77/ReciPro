#region using, namespace
using System;
using System.ComponentModel;
using System.Drawing;
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
    public bool MillerBravais { get => flowLayoutPanelI1.Visible; set => flowLayoutPanelI1.Visible = flowLayoutPanelI2.Visible = value; }

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
       labelLaTexSG_Num.Text = $"{symmetry.SpaceGroupNumber}: {symmetry.SpaceGroupSubNumber}";

        // 260427Cl 追加: LabelLaTeX 各種への流し込み (richTextBox 群と並走表示)。
        // 空間群・点群 HM 系 (HM, HM_full, PG_HM, LG) は対称要素軸ごとに thin space で区切る。
        // Hall は表記体系が異なるため spaced 指定なし。
        labelLaTexSG_HM.Text = ToLatex(symmetry.SpaceGroupHMStr, spaced: true);
        labelLaTexHM_full.Text = ToLatex(symmetry.SpaceGroupHMfullStr, spaced: true);
        labelLaTexSG_SF.Text = ToLatex(symmetry.SpaceGroupSFStr, sfStyle: true);
        labelLaTexSG_Hall.Text = ToLatex(symmetry.SpaceGroupHallStr, spaced: true);//やっぱりスペースはあった方がいい
        labelLaTexPG_HM.Text = ToLatex(symmetry.PointGroupHMStr, spaced: true);
        labelLaTexPG_SF.Text = ToLatex(symmetry.PointGroupSFStr, sfStyle: true);
        labelLaTexLG.Text = ToLatex(symmetry.LaueGroupStr, spaced: true);
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
    }
    #endregion

    #region 面間隔の計算、軸間、軸面間の角度計算
    /// <summary>
    /// 平面 (h₁k₁l₁, h₂k₂l₂) と軸 (u₁v₁w₁, u₂v₂w₂) の入力欄が変化したとき、
    /// 面間距離・軸長・面間角・軸間角・面と軸のなす角・zone axis を再計算して各表示欄を更新する (260427Cl)。
    /// </summary>
    private void numericBox_ValueChanged(object sender, EventArgs e)
    {
        var plane1 = (h: numericBoxH1.ValueInteger, k: numericBoxK1.ValueInteger, l: numericBoxL1.ValueInteger);
        var plane2 = (h: numericBoxH2.ValueInteger, k: numericBoxK2.ValueInteger, l: numericBoxL2.ValueInteger);
        var axis1 = (u: numericBoxU1.ValueInteger, v: numericBoxV1.ValueInteger, w: numericBoxW1.ValueInteger);
        var axis2 = (u: numericBoxU2.ValueInteger, v: numericBoxV2.ValueInteger, w: numericBoxW2.ValueInteger);

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
   
}

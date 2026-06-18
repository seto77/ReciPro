using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ReciPro;

// 260617Cl 追加: 多言語化方針 Phase 1 のオーバーフロー/重なり診断ツール。
// 目的: 翻訳で文字列長が変わったときに「ラベル/ボタンが切れる・重なる＝読めなくなる」箇所を、目視でなく機械的に検出する。
//   各テキスト保持コントロールについて TextRenderer.MeasureText の必要幅と実幅 (AutoSize=False) を比較して切れを、
//   AutoSize=True は plain panel 内での兄弟 Bounds 交差で重なりを検出し、TSV へ出力する。
//   ToolStrip/メニューの固定幅項目 (AutoSize=False) も検査する (auto-size 項目は内容に合わせるので対象外)。
// 疑似ローカライズ: inflate (例 1.4) を与えると「実テキストが N 倍に伸びたら切れるか」を、実翻訳が無くても先出しできる。
//   AutoSize コントロールは伸びれば自分が成長するので、inflate が効くのは固定幅 (=真の切れリスク) のコントロール。
// 起動: Program.cs の --diagnose [culture] [inflatePercent] から呼ぶ (--capture と異なり CopyFromScreen を使わず画面外で測る)。
internal static partial class GuiCapture
{
    // 切れ/はみ出しの許容誤差 (MeasureText とレンダラの差・丸め)。これ以下は無視。
    private const int OverflowTolerancePx = 2;
    // Warning と Error の境 (不足ピクセル)。codex 合意の「2px 以内=丸め、3〜5px 超=error」に沿う。
    private const int OverflowErrorPx = 6;

    /// <summary>--capture と --diagnose で共用: reflection 単独生成した子フォームへ FormMain / 親情報を注入する
    /// (Show 時の NRE 回避＋結晶依存描画の配線)。260617Cl に GuiCapture.Run の inline 分岐から切り出し。</summary>
    private static void WireCrystalDependencies(Form form, FormMain captureFormMain)
    {
        if (form is FormTrajectory trajectory)
            trajectory.FormMain = captureFormMain; // (260523Ch) FormTrajectory は単独生成だと Simulate 時に FormMain.Crystal を参照できない
        else if (form is FormEBSD ebsd)
            ebsd.FormMain = captureFormMain; // 260524Cl: Build MasterPattern が FormMain.Crystal を参照するため注入
        else if (form is FormImageSimulator imageSimulator)
            imageSimulator.FormMain = captureFormMain; // 260524Cl: Simulate が FormMain.Crystal を参照するため注入
        else if (form is FormStereonet stereonet)
            stereonet.formMain = captureFormMain; // 260524Cl: 軸/極のプロットは formMain.Crystal が必要
        else if (form is FormRotationMatrix rotation)
            rotation.FormMain = captureFormMain; // 260524Cl: GL の描画 (SetRotation) は FormMain の Euler 角を参照
        else if (form is FormDiffractionSimulator diffractionSimulator)
            diffractionSimulator.formMain = captureFormMain; // 260524Cl: 回折スポット描画 (Draw) は formMain.Crystal が必要
        else if (form is FormDiffractionSimulatorHolder holder)
            holder.FormDiffractionSimulator = captureFormMain?.FormDiffractionSimulator; // 260524Cl: ステレオネット描画の配線
        else if (form is FormSpotIDV2 spotID)
            spotID.FormMain = captureFormMain; // 260524Cl: スポット同定が FormMain を参照
    }

    /// <summary>全フォームを画面外に構築してテキストの切れ/重なりを測り、TSV を outFile へ書き出す。</summary>
    public static void Diagnose(string outFile, double inflate = 1.0)
    {
        var culture = (ForcedUICulture ?? Thread.CurrentThread.CurrentUICulture).Name;
        var rows = new List<string>
        {
            // Actual/Needed は幅判定では px 幅、Label の折り返し判定では px 高さ (Reason に明記)。
            string.Join("\t", "Culture", "Form", "Control", "Type", "Text", "Font", "Actual", "Needed", "Deficit", "Severity", "Reason")
        };
        void Trace(string s) => Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}\t{s}");

        // フォーム Load/Show で投げられる例外を握りつぶす (未処理例外のモーダルダイアログでハングしないため)。
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => Trace($"ThreadException\t{e.Exception.GetType().Name}: {e.Exception.Message}");
        Trace($"diagnose start (culture={culture}, inflate={inflate:0.00}) -> {outFile}");

        // ReciPro アセンブリ内のパラメータレス ctor を持つ Form (FormMain を先頭に)。--capture と同じ列挙。
        var types = typeof(FormMain).Assembly.GetTypes()
            .Where(t => typeof(Form).IsAssignableFrom(t) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null)
            .OrderBy(t => t == typeof(FormMain) ? 0 : 1).ThenBy(t => t.Name)
            .ToList();

        int forms = 0;
        FormMain main = null;
        foreach (var type in types)
        {
            Form form = null;
            try
            {
                if (ForcedUICulture != null)
                    Thread.CurrentThread.CurrentUICulture = ForcedUICulture;
                form = (Form)Activator.CreateInstance(type);
                if (form is FormMain mf) main = mf;
                else WireCrystalDependencies(form, main);

                ShowOffScreen(form, Trace);
                if (form is FormMain)
                    PrepareSpecialCaptureState(form, Trace); // spinel 選択 (結晶依存の子フォーム供給に必須)
                Settle(form, 60, Trace);

                DiagnoseForm(form, type.Name, culture, inflate, rows);
                forms++;
            }
            catch (Exception ex) { Trace($"{type.Name}\tFAIL\t{ex.GetType().Name}: {ex.Message}"); }
            finally
            {
                if (!ReferenceEquals(form, main)) { try { form?.Dispose(); } catch { /* 破棄時例外は無視 */ } }
            }
        }

        // 結晶依存で reflection 列挙では作れない子フォーム (FormBeamInteraction / FormSymmetryInformation /
        // FormStructureViewer)。spinel 選択済みの FormMain が保持する配線済みインスタンスを画面外表示して測る。
        if (main != null)
        {
            foreach (var child in main.EnumerateCaptureCrystalDependentForms())
            {
                try
                {
                    ShowOffScreen(child, Trace);
                    Settle(child, 60, Trace);
                    DiagnoseForm(child, child.GetType().Name, culture, inflate, rows);
                    forms++;
                    try { child.Hide(); } catch { /* FormMain が所有・破棄するので Hide のみ */ }
                }
                catch (Exception ex) { Trace($"{child.GetType().Name}\tFAIL\t{ex.GetType().Name}: {ex.Message}"); }
            }
        }

        try { main?.Close(); main?.Dispose(); } catch { /* 破棄時例外は無視 */ }

        var full = System.IO.Path.GetFullPath(outFile);
        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(full));
        System.IO.File.WriteAllLines(full, rows);
        int findings = rows.Count - 1;
        int errors = rows.Skip(1).Count(r => r.Contains("\tError\t"));
        Trace($"diagnose done: {forms} forms, {findings} findings ({errors} error) -> {full}");
    }

    private static void ShowOffScreen(Form form, Action<string> trace)
    {
        form.StartPosition = FormStartPosition.Manual;
        form.ShowInTaskbar = false;
        form.Location = new Point(-32000, -32000); // 画面外 (診断は CopyFromScreen を使わないので可)
        try { form.Show(); } catch (Exception ex) { trace($"{form.GetType().Name}\tWARN Show\t{ex.GetType().Name}: {ex.Message}"); }
    }

    private static void DiagnoseForm(Form form, string name, string culture, double inflate, List<string> rows)
    {
        // 260618Cl 追加: 診断側で UiFont.Apply を冪等に再適用してフォント sweep を保証する。
        //   FormBase.OnLoad は base.OnLoad(e) の後に UiFont.Apply(this) を呼ぶが、base.OnLoad が
        //   Load ハンドラ内で例外 (GL 無効時の NRE 等。FormStructureViewer/FormEBSD で発生) を投げると
        //   Apply に到達せず、フォントが resx(Segoe UI) のまま測定され CJK overflow を過少報告する。
        //   ここで再適用すれば Load 失敗フォームでも実カルチャのフォントで測れる
        //   (正常 Load 済みは Resolve が同一インスタンスを返すので no-op)。
        try { Crystallography.Controls.UiFont.Apply(form); } catch { /* 部分構築フォームは測れる範囲で測る */ }
        try { form.PerformLayout(); } catch { /* レイアウト例外は無視して測れるものだけ測る */ }
        // EnumerateControls (root を含む) / EnumerateToolStripItems(Form) は GuiCapture.cs 側 (capture と共用) を再利用。
        foreach (var c in EnumerateControls(form))
            DiagnoseControl(c, name, culture, inflate, rows);
        foreach (var it in EnumerateToolStripItems(form))
            DiagnoseToolStripItem(it, name, culture, inflate, rows);
    }

    private static void DiagnoseControl(Control c, string form, string culture, double inflate, List<string> rows)
    {
        if (!c.Visible || string.IsNullOrWhiteSpace(c.Text)) return; // 空白のみのラベル (スペーサ) は対象外
        // テキストを表示する代表的なリーフ型のみ。ButtonBase=Button/CheckBox/RadioButton。
        if (c is not (Label or ButtonBase or GroupBox or LinkLabel)) return;
        // 260617Cl: NumericBox 系は自己管理 (数値欄の最低幅を死守し、ヘッダは固定幅+ellipsis+tooltip) のため内部 (labelHeader/textBox) は測らない。
        for (var a = c.Parent; a != null; a = a.Parent)
            if (a.GetType().Name.Contains("NumericBox")) return;

        // 260617Cl: 擬似ローカライズ (inflate>1) の伸長予測は「翻訳される語」にのみ意味がある。記号/単位/短いインデックス
        //   (° ± ∓ % θ mm kV l1 等) は翻訳されず伸びないので擬似モードでは予測対象外 (実カルチャ inflate=1.0 は実テキストを測るので素通し)。
        if (inflate > 1.0 && !IsLikelyTranslatable(c.Text)) return;

        int glyph = c is CheckBox or RadioButton ? 18 : c is ButtonBase ? 12 : c is GroupBox ? 8 : 4; // グリフ/枠の余白概算

        if (c.AutoSize)
        {
            // AutoSize は文字に合わせて伸びるので自テキストには「切れ」ない。代わりに 2 つを見る:
            //   (1) WouldCollide: 直親が固定(再配置/成長しない)なら、伸びた分だけ右隣兄弟へ食い込むか予測。
            //   (2) ClippedByParent: 祖先を遡り、最初の固定祖先のクライアント右端で切れるか
            //       (AutoSize/Flow の祖先は子に合わせ成長/再配置するので、その祖先の右端を上位へ持ち上げて評価)。
            var p = c.Parent;
            if (p == null) return;
            // 260617Cl: 「翻訳で伸びた分 (inflation 増分) だけ右へ食い込むか」を現状幅 (c.Right) 基準で予測する。
            //   c.Right + 増分なら inflate=1.0 で増分0 → deficit≤tol となり baseline はクリーン。
            int growth = (int)Math.Ceiling(TextRenderer.MeasureText(c.Text, c.Font).Width * (inflate - 1.0));
            int grownRight = c.Right + growth;

            // (1) 右隣兄弟との衝突は、直親が再配置/成長しない場合のみ予測する (Flow/Table/AutoSize 親は吸収する)。
            if (p is not FlowLayoutPanel and not TableLayoutPanel && !p.AutoSize)
            {
                Control nearest = null;
                foreach (Control s in p.Controls)
                {
                    if (ReferenceEquals(s, c) || !s.Visible || s.Width == 0) continue;
                    if (s.Left < c.Right - OverflowTolerancePx) continue;  // 右隣のみ (左/既に重なるものは除外)
                    if (s.Bottom <= c.Top || s.Top >= c.Bottom) continue;  // 垂直に重ならない = 別の行
                    if (nearest == null || s.Left < nearest.Left) nearest = s;
                }
                if (nearest != null)
                {
                    int deficit = grownRight - nearest.Left;
                    if (deficit > OverflowTolerancePx)
                    {
                        rows.Add(Row(culture, form, c.Name, c.GetType().Name, c.Text, c.Font, c.Width, c.Width + growth, deficit,
                            deficit > OverflowErrorPx ? "Error" : "Warning", $"WouldCollide:{nearest.Name}"));
                        return;
                    }
                }
            }

            // (2) 260618Cl 追加: 祖先のクライアント右端で切れるか。groupBox 内で唯一/最右の AutoSize コントロール
            //   や、AutoSize FlowLayoutPanel が固定 groupBox を食み出す例 (ja の「等角投影 (Wulff)」ラジオ) を拾う。
            //   従来は Flow/Table/AutoSize 親を丸ごと早期 return しており、これらの入れ子クリップを見逃していた。
            var (clipDeficit, clipper) = AncestorRightClip(c, grownRight);
            if (clipDeficit > OverflowTolerancePx)
                rows.Add(Row(culture, form, c.Name, c.GetType().Name, c.Text, c.Font, c.Width, c.Width + growth, clipDeficit,
                    clipDeficit > OverflowErrorPx ? "Error" : "Warning", $"ClippedByParent:{clipper}"));
        }
        else if (c is Label or LinkLabel)
        {
            // 固定サイズの Label は幅内で折り返す。幅でなく「折り返した行数 × 行高 がラベル高さを超えるか」で切れを見る。
            // inflate 倍に伸びたテキストが何行になるかを 1 行幅から見積もる (baseline で 1 行に収まるラベルは出ない)。
            // 260617Cl: 折り返しには改行機会 (空白/CJK文字間) が要る。空白の無い単一トークン (記号/単位/変数名:
            //   ° ± ∓ % mm kV l1 θ 等) は幅が足りなくても折り返せず (クリップするだけ) 2 行にならない。
            //   これらは翻訳もされないので、WrapsBeyondHeight の誤検出 (幅 < 自テキスト幅のラベル) を防ぐ。
            if (!HasWrapOpportunity(c.Text)) return;
            var one = TextRenderer.MeasureText(c.Text, c.Font);
            int avail = Math.Max(1, c.Width);
            int lines = Math.Max(1, (int)Math.Ceiling(one.Width * inflate / avail));
            int neededH = lines * one.Height;
            int deficit = neededH - c.Height;
            if (deficit <= OverflowTolerancePx) return;
            rows.Add(Row(culture, form, c.Name, c.GetType().Name, c.Text, c.Font, c.Height, neededH, deficit,
                deficit > OverflowErrorPx ? "Error" : "Warning", $"WrapsBeyondHeight({lines}lines)"));
        }
        else
        {
            // 固定サイズの Button/CheckBox/RadioButton/GroupBox: 1 行テキストが幅に収まるか。
            int neededW = (int)Math.Ceiling(TextRenderer.MeasureText(c.Text, c.Font).Width * inflate) + glyph;
            int deficit = neededW - c.Width;
            if (deficit <= OverflowTolerancePx) return;
            rows.Add(Row(culture, form, c.Name, c.GetType().Name, c.Text, c.Font, c.Width, neededW, deficit,
                deficit > OverflowErrorPx ? "Error" : "Warning", "TextClipped"));
        }
    }

    private static void DiagnoseToolStripItem(ToolStripItem it, string form, string culture, double inflate, List<string> rows)
    {
        if (!it.Visible || string.IsNullOrWhiteSpace(it.Text) || it.Width <= 0) return;
        if (it.AutoSize) return; // auto-size 項目は内容に合わせるので切れない。固定幅 (status label 等) のみ対象。
        if (it.DisplayStyle is ToolStripItemDisplayStyle.Image or ToolStripItemDisplayStyle.None) return; // テキスト非表示

        int imageW = it.Image != null && it.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText ? it.Image.Width + 4 : 0;
        int neededW = (int)Math.Ceiling(TextRenderer.MeasureText(it.Text, it.Font).Width * inflate) + imageW + 12;
        int deficit = neededW - it.Width;
        if (deficit <= OverflowTolerancePx) return;
        rows.Add(Row(culture, form, it.Name, it.GetType().Name, it.Text, it.Font, it.Width, neededW, deficit,
            deficit > OverflowErrorPx ? "Error" : "Warning", "ToolStripTextClipped"));
    }

    // 260618Cl 追加: c の右端が、いずれかの祖先のクライアント右端で切れるか (＝親にクリップされるか) を遡って判定。
    //   AutoSize/AutoSize-FlowLayoutPanel の祖先は子に合わせて成長/再配置するので切らず、その祖先自身の右端
    //   (予測はみ出し分を足して) を上位へ持ち上げ、最初の「固定 (AutoSize でない)」祖先で確定する。
    //   AutoScroll 祖先はスクロール可なのでクリップなし。grownRight は c.Parent のクライアント座標での予測右端。
    private static (int deficit, string clipper) AncestorRightClip(Control c, int grownRight)
    {
        int right = grownRight;
        for (var p = c.Parent; p != null; p = p.Parent)
        {
            if (p is ScrollableControl { AutoScroll: true }) return (0, "");
            int deficit = right - p.ClientSize.Width;
            if (!p.AutoSize)
                return deficit > OverflowTolerancePx ? (deficit, p.Name) : (0, "");
            // p は AutoSize で c を吸収 (c は p に収まる)。c "自身" の右端を p の親座標へ変換 (p.Left を足す) して
            // 継続する。コンテナの右端 (p.Right) でなく c の右端を追うことで、行の中央にある通過コントロール
            // (例: 「×」「px」) を誤検出せず、実際に祖先右端を越える最右コントロールだけを拾う。
            right += p.Left;
        }
        return (0, "");
    }

    // 260617Cl 追加: テキストが (幅不足時に) 複数行へ折り返せる改行機会を持つか。
    //   空白で折り返し可。CJK/かなは文字間で折り返せるので 2 文字以上あれば可。それ以外の単一トークン
    //   (° ± ∓ % mm kV l1 θ 等の記号/単位/変数名) は折り返せない (クリップするだけ) → WrapsBeyondHeight 対象外。
    private static bool HasWrapOpportunity(string text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        int cjk = 0;
        foreach (char ch in text)
        {
            if (char.IsWhiteSpace(ch)) return true;
            if (ch >= 0x3040) cjk++; // ひらがな以降 (かな/CJK 漢字/ハングル等) は文字間で折り返し可
        }
        return cjk >= 2;
    }

    // 260617Cl 追加: テキストが翻訳されうる語を含むか (擬似ローカライズの伸長予測の前提)。
    //   連続するアルファベット 3 文字以上、または CJK/かな文字を含めば「語」とみなす。
    //   記号(° ± ∓ % θ)/単位(mm kV Å)/短いインデックス(l1 l2 X:)は false = 翻訳されず擬似伸長は無意味。
    private static bool IsLikelyTranslatable(string text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        int run = 0;
        foreach (char ch in text)
        {
            if (ch >= 0x3040) return true; // かな/CJK 漢字/ハングル等は短くても語
            if (char.IsLetter(ch)) { if (++run >= 3) return true; }
            else run = 0;
        }
        return false;
    }

    private static string Row(string culture, string form, string ctrl, string type, string text, Font font,
        int actualW, int neededW, int deficit, string severity, string reason)
        => string.Join("\t", culture, form, ctrl, type,
            (text ?? "").Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' '),
            $"{font.Name} {font.Size:0.##}pt", actualW, neededW, deficit, severity, reason);
}

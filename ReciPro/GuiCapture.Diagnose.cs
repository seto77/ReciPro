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
    // 260726Cl 旧: Dock 並びの合計はみ出し用のゆるい閾値。Margin を誤って合算していたぶんの偽陽性
    //   (子 1 個あたり数 px) を打ち消すために 20px も要っていた。Margin 加算をやめたので OverflowErrorPx に統一。
    // private const int DockOverflowTolerancePx = 20;

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

        // try { main?.Close(); main?.Dispose(); } catch { /* 破棄時例外は無視 */ }  // 260726Cl 旧
        // 260726Cl: Close() は FormMain_FormClosing → Registry(Reg.Mode.Write) を発火させ、
        //   (a) 強制カルチャ (--diagnose ru なら "ru") を UI 言語としてレジストリへ焼き付け、
        //   (b) ShowOffScreen が入れた画面外 Bounds (-32000,-32000) を保存し、
        //   (c) default.xml (結晶リスト) まで上書きしてしまう。
        //   11 言語ぶん回すと最後の言語で普段の ReciPro が起動するようになる実害があった。
        //   Form.Dispose() は FormClosing/FormClosed を発火しないので、破棄だけ行う
        //   (直後に Program.cs が Environment.Exit(0) するため、通常終了処理は不要)。
        try { main?.Dispose(); } catch { /* 破棄時例外は無視 */ }

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
        {
            // 260726Cl: DiagnoseWidget は ListBox.ItemHeight (LB_GETITEMHEIGHT)・ComboBox.Items の列挙・
            //   GetItemText など投げ得るプロパティに触るようになった。1 コントロールの例外を素通しすると
            //   Diagnose 側の per-form catch まで飛んで、そのフォームの残りが 1 件も測られずに消える。
            try
            {
                DiagnoseControl(c, name, culture, inflate, rows);
                DiagnoseWidget(c, name, culture, inflate, rows); // 260726Cl 追加: 複合/リスト系コントロールの切れ
            }
            catch { /* 測れないコントロールは飛ばし、同じフォームの残りは測る */ }
        }
        foreach (var it in EnumerateToolStripItems(form))
            DiagnoseToolStripItem(it, name, culture, inflate, rows);
    }

    private static void DiagnoseControl(Control c, string form, string culture, double inflate, List<string> rows)
    {
        if (!c.Visible || string.IsNullOrWhiteSpace(c.Text)) return; // 空白のみのラベル (スペーサ) は対象外
        // テキストを表示する代表的なリーフ型のみ。ButtonBase=Button/CheckBox/RadioButton。
        if (c is not (Label or ButtonBase or GroupBox or LinkLabel)) return;
        // 260617Cl: NumericBox 系は自己管理 (数値欄の最低幅を死守し、ヘッダは固定幅+ellipsis+tooltip) のため内部 (labelHeader/textBox) は測らない。
        // 260620Cl: ColorControl も同様に自己管理する複合コントロール (内部 labelHeader/labelFooter/pictureBox)。
        //   HeaderText「Color」等が groupBox 詰まり (伸長 NumericBox 隣接) で内部ラベルとして誤検出され、
        //   deficit が文字長と無相関になる (例 ru「Цвет」最短なのに最大 deficit) ため除外する。
        //   注: ColorControl 内部 footer の長文オーバーフローは culture resx の FooterText 翻訳で個別対処済。
        // 260623Cl: WaveLengthControl も自己管理する複合UC (UC自体 AutoSize=true ＋ 単位行 flowLayoutPanel1 AutoSize=true・WrapContents 既定)。
        //   内部 label2「Unit」/radioButtonUnitAngstrom「Å」/radioButtonUnitNanoMeter「nm」が、訳語伸長時に
        //   AutoSize 連鎖＋フロー折返しを診断が追えず ClippedByParent:FormBeamInteraction(=AutoSize祖先を遡った先のフォーム) と
        //   誤検出される (def が文字長と無相関・nm「nm」最短なのに def114)。単位「Å/nm」は短縮不可で実体は自己解決するため除外。
        // for (var a = c.Parent; a != null; a = a.Parent)
        //     if (a.GetType().Name.Contains("NumericBox")) return;  // 260617Cl 旧
        //     if (a.GetType().Name.Contains("NumericBox") || a.GetType().Name.Contains("ColorControl") || a.GetType().Name.Contains("WaveLengthControl")) return;  // 260620Cl / 260623Cl WaveLengthControl 追加
        if (IsSelfManagedComposite(c)) return;  // 260726Cl: 同じ祖先走査が DiagnoseWidget 側と二重化していたので集約

        // 260617Cl: 擬似ローカライズ (inflate>1) の伸長予測は「翻訳される語」にのみ意味がある。記号/単位/短いインデックス
        //   (° ± ∓ % θ mm kV l1 等) は翻訳されず伸びないので擬似モードでは予測対象外 (実カルチャ inflate=1.0 は実テキストを測るので素通し)。
        if (inflate > 1.0 && !IsLikelyTranslatable(c.Text)) return;

        // int glyph = c is CheckBox or RadioButton ? 18 : c is ButtonBase ? 12 : c is GroupBox ? 8 : 4; // 260726Cl 旧: GroupBox/既定アームは下記のとおり到達不能になった

        // 260726Cl 追加: GroupBox のタイトルは AutoSize の有無に関わらず幅で切れる。
        //   WinForms の GroupBox.GetPreferredSize は子のレイアウトしか見ずキャプション幅を考慮しないため、
        //   AutoSize=true でもタイトルは伸びない。さらに Dock=Top だと幅は親に固定される。
        //   実例: ru の FormMain groupBoxCurrentDirection (AutoSize=true・Dock=Top・幅142) は
        //   「Текущая ориентация」の末尾「я」が切れているのに、従来の AutoSize 分岐では素通りしていた。
        if (c is GroupBox)
        {
            // GroupBoxRenderer はキャプションを左 13px・右 8px 内側の矩形へ WordBreak 付き (パディング込み) で描くので、
            // 実効幅は Width−21。入り切らない語は 2 行目へ回って本文領域に重なる/消えるため、1px 足りないだけでも
            // 視覚的な損失は大きい。よって他の判定より厳しく「0 超」で報告する。
            // 実測較正: de「Aktuelle Orientierung」(幅142・2語目が丸ごと消える) と
            //   ja「回折波の数」(幅74・2 行目「数」が本文に重なる) の両方をこの式が拾う。
            int neededTitle = Needed(c.Text, c.Font, inflate, 21);
            int titleDeficit = neededTitle - c.Width;
            if (titleDeficit > 0)
                rows.Add(Row(culture, form, c.Name, c.GetType().Name, c.Text, c.Font, c.Width, neededTitle, titleDeficit,
                    Sev(titleDeficit), "TextClipped"));
            // 260726Cl: 固定幅 GroupBox はキャプション規則 (pad=21) が旧 TextClipped (glyph=8) の上位互換なのでここで終了。
            //   AutoSize GroupBox は下の AutoSize 分岐 (WouldCollide/ClippedByParent) へ落とす。無条件 return にすると
            //   本改修前まで効いていたこの 2 判定が AutoSize GroupBox から丸ごと消えてしまう。
            if (!c.AutoSize) return;
        }

        if (c.AutoSize)
        {
            // 260726Cl 追加: Dock=Top/Bottom/Fill・MaximumSize・TableLayoutPanel の列幅などで幅を押さえられている
            //   AutoSize コントロールは、文字ぶんに伸びられないので実際には切れる。WinForms 自身が返す
            //   PreferredSize (この文字を出すのに要る大きさ) と実幅を比べれば、押さえ込みの原因に依らず検出できる。
            //   PreferredSize は ButtonBase 系でキャッシュされず毎回テキストを測り直すのでローカルへ 1 回だけ取る。
            int preferred = c.PreferredSize.Width, shortfall = preferred - c.Width;
            if (shortfall > OverflowTolerancePx)
            {
                rows.Add(Row(culture, form, c.Name, c.GetType().Name, c.Text, c.Font, c.Width, preferred, shortfall,
                    Sev(shortfall), $"AutoSizeConstrained(Dock={c.Dock})"));
                return;
            }

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
                    // 260726Cl 検討メモ: 「c が既に伸びて s に食い込んでいる」ケースを拾おうと条件を
                    //   `s.Left <= c.Left` へ緩めたが、意図的に重ねてある兄弟 (排他表示のボタン対、複数列に
                    //   またがるヘッダラベル「h k l」など) を大量に誤検出したため元へ戻した。
                    //   既に重なっている組は ChildOverflowsParent / ClippedByParent 側で拾う方針。
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
                            Sev(deficit), $"WouldCollide:{nearest.Name}"));
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
                    Sev(clipDeficit), $"ClippedByParent:{clipper}"));
        }
        else if (c is Label or LinkLabel)
        {
            // 固定サイズの Label は幅内で折り返す。幅でなく「折り返した行数 × 行高 がラベル高さを超えるか」で切れを見る。
            // inflate 倍に伸びたテキストが何行になるかを 1 行幅から見積もる (baseline で 1 行に収まるラベルは出ない)。
            // 260617Cl: 折り返しには改行機会 (空白/CJK文字間) が要る。空白の無い単一トークン (記号/単位/変数名:
            //   ° ± ∓ % mm kV l1 θ 等) は幅が足りなくても折り返せず (クリップするだけ) 2 行にならない。
            //   これらは翻訳もされないので、WrapsBeyondHeight の誤検出 (幅 < 自テキスト幅のラベル) を防ぐ。
            if (!HasWrapOpportunity(c.Text)) return;
            ReportWrap(TextRenderer.MeasureText(c.Text, c.Font), 0);
        }
        else
        {
            // ここへ来るのは非 AutoSize の ButtonBase のみ (Label/LinkLabel は上の分岐、GroupBox は更に上で return)。
            int glyph = c is CheckBox or RadioButton ? 18 : 12; // 260726Cl: グリフ/枠の余白概算 (旧 glyph 式の GroupBox/既定アームは到達不能だった)
            var one = TextRenderer.MeasureText(c.Text, c.Font);
            // 260726Cl 追加: Button/CheckBox/RadioButton は高さに 2 行以上入るなら WinForms が幅で折り返して描く。
            //   従来は一律「1 行が幅に収まるか」で測っていたため、2 行前提でデザインされたコントロールが
            //   全言語で偽陽性になっていた (FormPolycrystallineDiffractionSimulator の radioButtonZigzagScan は
            //   205x52 = 2 行ぶんの高さがあり、en では実際には折り返して収まっている)。
            if (c.Height >= one.Height * 2 && HasWrapOpportunity(c.Text))
            {
                ReportWrap(one, glyph);
                return;
            }

            // 固定サイズの Button/CheckBox/RadioButton: 1 行テキストが幅に収まるか。
            int neededW = (int)Math.Ceiling(one.Width * inflate) + glyph;
            int deficit = neededW - c.Width;
            if (deficit <= OverflowTolerancePx) return;
            rows.Add(Row(culture, form, c.Name, c.GetType().Name, c.Text, c.Font, c.Width, neededW, deficit,
                Sev(deficit), "TextClipped"));
        }

        // 260726Cl 追加: 「幅で折り返した結果、高さが足りるか」の共通判定 (Label は glyph=0、ButtonBase はグリフ幅ぶん狭い)。
        //   Label 側と ButtonBase 側に同じ 9 行が二重化しており、折返し式の再較正が 2 箇所必要になっていたので集約。
        void ReportWrap(Size one, int glyph)
        {
            int availW = Math.Max(1, c.Width - glyph);
            int lines = Math.Max(1, (int)Math.Ceiling(one.Width * inflate / availW));
            int neededH = lines * one.Height;
            int deficit = neededH - c.Height;
            if (deficit <= OverflowTolerancePx) return;
            rows.Add(Row(culture, form, c.Name, c.GetType().Name, c.Text, c.Font, c.Height, neededH, deficit,
                Sev(deficit), $"WrapsBeyondHeight({lines}lines)"));
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
            Sev(deficit), "ToolStripTextClipped"));
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

    // ────────────────────────────────────────────────────────────────────────────────────────
    // 260726Cl 追加 (作者要望「全コントロール・全言語で文字溢れ/切れを確認」): DiagnoseControl が対象に
    // していない複合・リスト系コントロールの切れ検査。DiagnoseControl は Label/ButtonBase/GroupBox/LinkLabel
    // しか見ないため、次の種別は 11 言語分の翻訳が伸びても検出できていなかった。
    //   ① DataGridView : 列見出しが列幅に収まるか (固定幅列は "…" で切れる。AutoSize 列は自然に deficit≤0)
    //   ② TabControl   : タブ見出しの合計幅がクライアント幅を超えるか (超えると矢印スクロール/行増で見出しが隠れる)
    //   ③ ComboBox / ListBox / CheckedListBox : 項目テキストがリスト幅に収まるか
    //   ④ ListView (Details) : 列見出しが列幅に収まるか
    //   ⑤ ToolStrip    : 項目がオーバーフロー (») へ押し出されていないか
    //   ⑥ AutoEllipsis : NumericBox/ColorControl/WaveLengthControl の固定幅ヘッダ/フッタが "…" で切れていないか
    // ⑥ は DiagnoseControl が「自己管理コントロール」として意図的に除外している領域だが、HeaderWidth を
    // 固定した箇所は実際に ellipsis で読めなくなる。折り返し (WrapsBeyondHeight) ではなく幅のみの規則で測る。
    private static void DiagnoseWidget(Control c, string form, string culture, double inflate, List<string> rows)
    {
        if (!c.Visible || c.Width <= 0 || c.Height <= 0) return;

        DiagnoseContainer(c, form, culture, rows); // 260726Cl: 折り返し/親はみ出し (テキスト種別に依らない構造的な溢れ)

        switch (c)
        {
            case DataGridView dgv when dgv.ColumnHeadersVisible:
                // 見出しが折り返す設定 (WrapMode=True かつ見出し高さ AutoSize) なら高さが伸びて切れないので対象外。
                if (dgv.ColumnHeadersDefaultCellStyle.WrapMode == DataGridViewTriState.True
                    && dgv.ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.AutoSize)
                    break;
                // 260726Cl: DataGridViewCell.Style の getter は未設定だと空 Style を「生成して」返す (診断が対象を書き換える)
                //   ので HasStyle で守る。フォールバックは列に依らないのでループ外へ。
                var headerFont = dgv.ColumnHeadersDefaultCellStyle.Font ?? dgv.Font;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!col.Visible || string.IsNullOrWhiteSpace(col.HeaderText)) continue;
                    var f = (col.HeaderCell is { HasStyle: true } hc ? hc.Style.Font : null) ?? headerFont;
                    // 見出しセルの内側余白 (左右パディング 2px×2 + 罫線) ぶんを足して必要幅とする。
                    Report(rows, culture, form, $"{c.Name}.{col.Name}", "DataGridViewColumn", col.HeaderText, f,
                        col.Width, Needed(col.HeaderText, f, inflate, 6), "GridHeaderClipped", inflate);
                }
                break;

            case TabControl { TabCount: > 0 } tc when tc.Alignment is TabAlignment.Top or TabAlignment.Bottom:
                // 個々のタブ見出し (SizeMode=Fixed だと文字がタブ幅を超える)。Normal は文字に合わせるので deficit≤0。
                for (int i = 0; i < tc.TabCount; i++)
                {
                    var page = tc.TabPages[i];
                    if (string.IsNullOrWhiteSpace(page.Text) || SafeTabRect(tc, i) is not { } rect) continue;
                    int imageW = page.ImageIndex >= 0 || !string.IsNullOrEmpty(page.ImageKey) ? 20 : 0;
                    // 260726Cl: タブ矩形は SizeMode=Normal では文字幅ぴったりに作られるので余白は足さない
                    // (足すと全タブが一律 +6px の偽陽性になる。実測で en の全タブが deficit=6 になった)。
                    Report(rows, culture, form, $"{c.Name}.{page.Name}", "TabPage", page.Text, tc.Font,
                        rect.Width, Needed(page.Text, tc.Font, inflate, imageW), "TabTextClipped", inflate);
                }
                // タブ列全体がコントロール幅を超えると、Multiline=false では左右の矢印が出て右側タブが隠れる。
                if (!tc.Multiline)
                {
                    int right = 0;
                    for (int i = 0; i < tc.TabCount; i++)
                        if (SafeTabRect(tc, i) is { } r) right = Math.Max(right, r.Right);
                    Report(rows, culture, form, c.Name, "TabControl", TabTexts(tc), tc.Font,
                        tc.ClientSize.Width, right, "TabHeadersOverflow", inflate);
                }
                else if (tc.RowCount > 1)
                {
                    // Multiline は行を増やして全タブを見せるので文字は切れないが、ページ領域が縮む (レイアウト崩れ要因)。
                    rows.Add(Row(culture, form, c.Name, "TabControl", TabTexts(tc), tc.Font,
                        1, tc.RowCount, tc.RowCount - 1, "Warning", $"TabHeaderRows({tc.RowCount})"));
                }
                break;

            case ComboBox cb:
                // 一覧 (DropDownWidth。既定は ComboBox 幅と同じ) に項目が収まるか。項目数が MaxDropDownItems を
                // 超えるときは縦スクロールバーぶん狭くなる。Windows はドロップダウンを自動で広げないので、
                // ここで不足すると一覧でも閉じた表示部でも文字が切れる。
                // 260726Cl: ComboBox は描画余白が分かっているので、MeasureText の既定パディング (≒5px) を含まない
                //   生の文字幅 (NoPadding) で測る。実測キャプチャ 2 点で較正済み:
                //     ・ScalablePictureBoxAdvanced の comboBoxGradient (幅68) の "Positive " は全部見える
                //     ・FormImageSimulator の comboBoxScaleColorScale (幅72) の "Gray scale" は "Gray scal" と切れる
                //   → 閉じた表示部の実効幅は「Width − ドロップダウンボタン(17) − 内側余白(4)」で両者と整合する。
                int listAvail = cb.DropDownWidth
                    - (cb.Items.Count > cb.MaxDropDownItems ? SystemInformation.VerticalScrollBarWidth : 0) - 6;
                // 260726Cl: ドロップダウンボタンの幅は Win32 で SM_CXVSCROLL (= VerticalScrollBarWidth)。
                //   旧コードは HorizontalScrollBarArrowWidth (SM_CXHSCROLL) を使っており、既定テーマ 96dpi で
                //   偶然どちらも 17px だったため 2 点の実測較正が通っていただけ。スクロールバー幅を変えた環境で
                //   全 ComboBox の判定が一律ずれるので、上の一覧側と同じメトリックへ揃える。
                int closedAvail = cb.Width - SystemInformation.VerticalScrollBarWidth - 4;
                var cbPath = ParentPath(cb); // 260726Cl: ループ不変なので巻き上げ
                foreach (var item in cb.Items)
                {
                    var s = ItemText(cb, item);
                    int need = NeededRaw(s, cb.Font, inflate);
                    Report(rows, culture, form, cbPath, "ComboBoxItem", s, cb.Font, listAvail, need, "ListItemClipped", inflate);
                    Report(rows, culture, form, cbPath, "ComboBoxItem", s, cb.Font, closedAvail, need, "ComboBoxTextClipped", inflate);
                }
                break;

            // 260726Cl: CheckedListBox は ListBox 派生で、判定はチェックボックスのグリフ幅 (18px) 分しか違わないので
            //   ReportListItems へ集約 (旧: 有効幅の式と ItemText 呼び出しが両アームに二重化し、しかもループ不変式を
            //   項目ごとに再評価していた)。CheckedListBox 側に HorizontalScrollbar のガードが無い非対称は、
            //   検出結果 (baseline) を変えないため現状のまま残す。
            case CheckedListBox clb:
                ReportListItems(clb, 18, "CheckedListBoxItem");
                break;

            // 260726Cl: MultiColumn も除外する。多段組では実効幅が ColumnWidth になり、スクロールバーも水平なので
            //   ReportListItems の「ClientSize.Width − 縦スクロールバー」モデルが成立しない (FormMain の結晶リストが該当)。
            //   そもそも中身はユーザーデータ (結晶名) で翻訳対象ではなく、測っても baseline が環境依存になる。
            case ListBox { HorizontalScrollbar: false, MultiColumn: false } lb: // 水平スクロールバー有りなら読めるので対象外
                ReportListItems(lb, 0, "ListBoxItem");
                break;

            case ListView { View: View.Details } lv:
                foreach (ColumnHeader col in lv.Columns)
                    Report(rows, culture, form, $"{c.Name}.{col.Name}", "ColumnHeader", col.Text, lv.Font,
                        col.Width, Needed(col.Text, lv.Font, inflate, 8), "ListViewHeaderClipped", inflate);
                break;

            case ToolStrip { IsDropDown: false } ts:
                foreach (ToolStripItem it in ts.Items)
                    if (it.Visible && it.Placement == ToolStripItemPlacement.Overflow && !string.IsNullOrWhiteSpace(it.Text))
                        rows.Add(Row(culture, form, it.Name, it.GetType().Name, it.Text, it.Font,
                            ts.ClientSize.Width, ts.ClientSize.Width + it.Width, it.Width, "Error", $"PushedToOverflow:{ts.Name}"));
                break;

            case Label { AutoSize: false, AutoEllipsis: true } lbl when IsSelfManagedComposite(lbl):
                Report(rows, culture, form, ParentPath(lbl), "Label(AutoEllipsis)", lbl.Text, lbl.Font,
                    lbl.Width - lbl.Padding.Horizontal, Needed(lbl.Text, lbl.Font, inflate, 0), "EllipsisClipped", inflate);
                break;
        }

        // 260726Cl 追加: ListBox / CheckedListBox 共通の項目幅判定。有効幅 (スクロールバーの有無を含む) は
        //   ループ不変なので 1 回だけ求める。glyph は CheckedListBox のチェックボックス幅。
        void ReportListItems(ListBox list, int glyph, string type)
        {
            bool scroll = list.Items.Count > list.ClientSize.Height / Math.Max(list.ItemHeight, 1);
            int avail = list.ClientSize.Width - glyph - (scroll ? SystemInformation.VerticalScrollBarWidth : 0);
            foreach (var item in list.Items)
            {
                var s = ItemText(list, item);
                Report(rows, culture, form, list.Name, type, s, list.Font, avail, NeededRaw(s, list.Font, inflate),
                    "ListItemClipped", inflate);
            }
        }
    }

    /// <summary>
    /// 260726Cl 追加: テキスト種別に依らない「構造的な溢れ」を 2 つ検査する。
    ///  (a) FlowWrapped     : FlowLayoutPanel が意図せず折り返しているか。特に FlowDirection=TopDown で
    ///      列が 2 本になるのは、訳語が伸びて高さが足りなくなったときの典型 (WrapContents 既定 true)。
    ///      FormEBSD の吸収オプション行が独/仏/伊/西/葡/露で 2 列目へ回り込み、groupBox 右外へ出て
    ///      「非局所吸収モデル」「TDS 背景」チェックボックスが画面から消えていた実例がこれ。
    ///  (b) ChildOverflowsParent : 子の右端/下端が親のクライアント領域を越えているか。Dock 追従・AutoScroll・
    ///      AutoSize 親は自分で吸収するので対象外。Label/Button 以外 (NumericBox 行や入れ子パネル) の溢れを拾う。
    /// </summary>
    private static void DiagnoseContainer(Control c, string form, string culture, List<string> rows)
    {
        // (a) FlowLayoutPanel の折り返し。
        //   Controls コレクションの順＝フローの並び順なので、「次の子が前の子より手前へ戻ったら折り返し」で数える。
        //   兄弟の Margin/高さ違いで Top がずれるだけの並びを行数と誤認しないため、Distinct ではなくこの順序判定を使う。
        if (c is FlowLayoutPanel { WrapContents: true } flow) // WrapContents=false は折り返さないので対象外
        {
            var kids = flow.Controls.Cast<Control>().Where(k => k.Visible).ToList();
            bool topDown = flow.FlowDirection is FlowDirection.TopDown or FlowDirection.BottomUp;
            // 260726Cl: 逆向きフロー (RightToLeft/BottomUp) は主軸座標が単調に「戻る」ので、素朴な
            //   「前の子より手前へ戻ったら折り返し」判定だと子の数ぶん行数を数えてしまう。
            //   実害: FormMain.flowLayoutPanelCrystalOrder (RightToLeft・子5) が全言語で
            //   FlowWrapped(rows=5) の偽検出になっていた。向きに応じて不等号を反転する。
            bool reversed = flow.FlowDirection is FlowDirection.RightToLeft or FlowDirection.BottomUp;
            int lines = 1; // 先頭の子で 1 行目。子が 0 でも下の lines > 1 が偽になるので特別扱いは要らない
            for (int i = 1; i < kids.Count; i++)
            {
                int cur = topDown ? kids[i].Top : kids[i].Left, prev = topDown ? kids[i - 1].Top : kids[i - 1].Left;
                if (reversed ? cur >= prev : cur <= prev)
                    lines++;
            }
            if (lines > 1)
                rows.Add(Row(culture, form, flow.Name, "FlowLayoutPanel",
                    string.Join(" / ", kids.Select(k => k.Name)), flow.Font, 1, lines, lines - 1,
                    topDown ? "Error" : "Warning",   // TopDown の複数列はほぼ常に不具合、LeftToRight の複数行は意図的なこともある
                    $"FlowWrapped({(topDown ? "cols" : "rows")}={lines},{flow.FlowDirection})"));
        }

        // (b) 子の親はみ出し
        if (c.AutoSize || c is ScrollableControl { AutoScroll: true } || c is TabControl or SplitContainer) return;

        // (b-1) 260726Cl 追加: Dock=Left/Right (または Top/Bottom) で並べた子の合計が親のクライアント領域を超えると、
        //   最後に置いた子が丸ごと画面から消える (Dock 並びは折り返さない)。実例: ru の FormImageSimulator
        //   groupBoxDisplay で「Масштабная линейка」まで出て「Длина」の数値欄と最後の「Цвет」が消えていた。
        //   Dock 付きの子は (b-2) の個別判定 (Dock==None のみ) では拾えないので、合計幅/高さで見る。
        // 260726Cl: TableLayoutPanel / FlowLayoutPanel は Dock を主軸配置に使わない (セル/フローが配置を決める)
        //   ので、Dock 付きの子を積み上げて合計する意味が無い。TLP では別セルの子まで合算して誤検出になる。
        if (c is not (TableLayoutPanel or FlowLayoutPanel))
        {
            int dockW = 0, dockH = 0;
            foreach (Control ch in c.Controls)
            {
                if (!ch.Visible) continue;
                // 260726Cl: Margin を足していたが、WinForms の Dock レイアウト (DefaultLayout) は Margin を
                //   消費しない (Margin は Flow/Table 専用)。子の数に比例した過大評価になっていた。
                //   実例: FormImageSimulator.panelSerialThickness は NumericBox 3 個 × Margin 4px = 12px 過大で
                //   「11px はみ出し」と出ていた (＝作者コメントの「en で 11px 出るが実表示は正常」の正体)。
                //   その偽陽性を打ち消すために DockOverflowTolerancePx=20 という粗い閾値が要り、結果として
                //   7〜19px の実オーバーフローを取りこぼしていたので、Margin を外して閾値も他判定と揃える。
                if (ch.Dock is DockStyle.Left or DockStyle.Right) dockW += ch.Width;
                else if (ch.Dock is DockStyle.Top or DockStyle.Bottom) dockH += ch.Height;
            }
            // Dock レイアウトの原資は ClientSize ではなく DisplayRectangle (= クライアント領域 − Padding)。
            // 旧: 2 要素のタプル配列を毎回確保して回していたが、sum==0 のガードは deficit≤閾値 に必ず吸収されるので
            // 到達不能だった。素直な if 2 本にする。
            var avail = c.DisplayRectangle.Size;
            if (dockW - avail.Width > OverflowErrorPx)
                rows.Add(Row(culture, form, c.Name, c.GetType().Name, c.Text, c.Font,
                    avail.Width, dockW, dockW - avail.Width, "Error", "DockRowOverflow(X)"));
            if (dockH - avail.Height > OverflowErrorPx)
                rows.Add(Row(culture, form, c.Name, c.GetType().Name, c.Text, c.Font,
                    avail.Height, dockH, dockH - avail.Height, "Error", "DockRowOverflow(Y)"));
        }

        foreach (Control ch in c.Controls)
        {
            if (!ch.Visible || ch.Dock != DockStyle.None || ch.Width <= 0 || ch.Height <= 0) continue;
            int dx = ch.Right - c.ClientSize.Width, dy = ch.Bottom - c.ClientSize.Height;
            // 260726Cl: 構造的なはみ出しは丸め誤差が大きい (spin ボタンや NumericBox が 3〜5px 下へ出るのは設計どおり)
            // ため、テキスト判定より緩い OverflowErrorPx を閾値にして Error 相当だけを拾う。
            // 縦方向は FlowLayoutPanel (折り返しで下へ押し出される) のときだけ見る。自作 UserControl は
            // 実行時に自分でリサイズするため、画面外構築の時点では子が下へ大きく出ていて偽陽性になる
            // (FormPolycrystallineDiffractionSimulator の diffractionPatternControl で 480〜513px の誤検出)。
            if (c is not FlowLayoutPanel) dy = int.MinValue;
            bool overX = dx > OverflowErrorPx, overY = dy > OverflowErrorPx;
            if (!overX && !overY) continue;
            // 260726Cl: ここへ来た時点で Math.Max(dx, dy) > OverflowErrorPx が確定するので severity は常に Error
            //   (旧コードの三項は Warning 側が到達不能だった)。軸判定も文字列比較でなく overX で持つ。
            // 260726Cl: Deficit は Actual/Needed と同じ軸で取る (旧 Math.Max(dx,dy) だと XY 同時はみ出しかつ
            //   dy > dx のとき Needed − Actual ≠ Deficit になり、TSV を機械集計したとき値が食い違う)。
            rows.Add(Row(culture, form, ch.Name, ch.GetType().Name, ch.Text, ch.Font,
                overX ? c.ClientSize.Width : c.ClientSize.Height,
                overX ? ch.Right : ch.Bottom, overX ? dx : dy,
                "Error", $"ChildOverflowsParent:{c.Name}({(overX && overY ? "XY" : overX ? "X" : "Y")})"));
        }
    }

    /// <summary>260726Cl 追加: 必要幅 = 1 行テキスト幅 × inflate + 枠/余白。</summary>
    private static int Needed(string text, Font font, double inflate, int pad)
        => (int)Math.Ceiling(TextRenderer.MeasureText(text ?? "", font).Width * inflate) + pad;

    /// <summary>260726Cl 追加: MeasureText の既定パディング (グリフはみ出し用の左右余白 ≒5px) を含まない生の文字幅。
    /// 描画余白が判っている ComboBox/ListBox の項目判定に使う (既定パディング込みだと一律 5px 過大に出る)。</summary>
    private static int NeededRaw(string text, Font font, double inflate)
        => (int)Math.Ceiling(TextRenderer.MeasureText(text ?? "", font,
            new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Width * inflate);

    /// <summary>260726Cl 追加: 同名コントロールが複数フォーム/UserControl に居るため、親名を付けて識別できるようにする。</summary>
    private static string ParentPath(Control c)
        => string.IsNullOrEmpty(c.Parent?.Name) ? c.Name : $"{c.Parent.Name}.{c.Name}";

    /// <summary>260726Cl 追加: 不足px から severity を決める (2px 以内=丸め・6px 超=Error の合意に沿う)。</summary>
    private static string Sev(int deficit) => deficit > OverflowErrorPx ? "Error" : "Warning";

    /// <summary>260726Cl 追加: 不足px を判定して行を積む共通処理 (擬似ローカライズ時は翻訳されうる語のみ対象)。
    /// 260726Cl: 擬似ローカライズ判定を static フィールド (inflatePseudo) から引数へ戻した。呼び出し元は全て
    ///   DiagnoseWidget で inflate を引数に持っており、静的可変状態にする理由が無かった (DiagnoseControl の
    ///   同等判定もインラインの inflate &gt; 1.0 で書かれており非対称だった)。</summary>
    private static void Report(List<string> rows, string culture, string form, string ctrl, string type,
        string text, Font font, int actual, int needed, string reason, double inflate = 1.0)
    {
        if (string.IsNullOrWhiteSpace(text)) return;
        if (inflate > 1.0 && !IsLikelyTranslatable(text)) return;
        // 260726Cl: 有効幅が 0 以下 (極端に狭いコンボや Padding が Width を超える AutoEllipsis ラベル) だと
        //   deficit が水増しされて必ず Error になる。測れないものは測らない。
        if (actual <= 0) return;
        int deficit = needed - actual;
        if (deficit <= OverflowTolerancePx) return;
        rows.Add(Row(culture, form, ctrl, type, text, font, actual, needed, deficit, Sev(deficit), reason));
    }

    /// <summary>260726Cl 追加: GetTabRect は再入レイアウト中に例外を投げ得るので安全に取る。
    /// 取れなければ null を返し、呼び出し側はそのタブを飛ばす (旧: Width/Right で別々の番兵を返す 2 メソッド)。</summary>
    private static Rectangle? SafeTabRect(TabControl tc, int index)
    { try { return tc.GetTabRect(index); } catch { return null; } }

    /// <summary>260726Cl 追加: TabControl 全体の行 (TabHeadersOverflow) 用に、全タブ見出しを 1 セルへまとめる。</summary>
    private static string TabTexts(TabControl tc)
        => string.Join(" | ", tc.TabPages.Cast<TabPage>().Select(p => p.Text));

    /// <summary>260726Cl 追加: ComboBox/ListBox の項目の表示文字列 (DisplayMember 解決込み)。null は空文字に潰す。</summary>
    private static string ItemText(ListControl list, object item)
        => list.GetItemText(item) ?? "";

    /// <summary>260726Cl 追加: 自己管理複合コントロール (NumericBox/ColorControl/WaveLengthControl) の内部か。
    /// DiagnoseControl の除外と DiagnoseWidget の AutoEllipsis 判定で共用する。
    /// 260726Cl: 型名の Contains 判定から型パターンへ変更。派生 (NumericBoxWithMenu) が継承で自動的に入り、
    ///   3 型以外に名前が部分一致するクラスは全リポに存在しないので判定は等価。</summary>
    private static bool IsSelfManagedComposite(Control c)
    {
        for (var a = c.Parent; a != null; a = a.Parent)
            if (a is NumericBox or ColorControl or WaveLengthControl) // Program.cs の global using Crystallography.Controls 経由
                return true;
        return false;
    }

    private static string Row(string culture, string form, string ctrl, string type, string text, Font font,
        int actualW, int neededW, int deficit, string severity, string reason)
        // 260726Cl: 書式は CurrentUICulture でなく CurrentCulture で決まる。診断は各言語で走らせて TSV を
        //   突き合わせるので、小数点が "," になる環境だと Font 列が "Yu Gothic UI 9,75pt" になり全行が差分化する。
        //   機械比較できるよう InvariantCulture を明示する。
        => string.Join("\t", culture, form, ctrl, type,
            (text ?? "").Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' '),
            $"{font.Name} {font.Size.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)}pt",
            actualW, neededW, deficit, severity, reason);
}

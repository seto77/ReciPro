using System;
using System.Text;
using System.Windows.Forms;

namespace Crystallography.Controls;

/// <summary>
/// StatusStrip の進捗バー + ステータスラベルを統一書式で更新する共通ヘルパー。
/// 260520Cl 追加: GUI統一 Phase 5 — 各フォームで手書きされていた経過時間表記/進捗更新を正準化する。
/// </summary>
public static class StatusBarHelper
{
    /// <summary>
    /// 経過時間を統一書式の文字列にする。1 秒未満は "x ms" (整数)、1 秒以上は "x.x s" (小数1桁)。
    /// 260520Cl 書式設計で確定: ms整数 / 秒小数1桁。進捗行は "{stage}  {pct:0.0}%  (elapsed)  remaining ~{eta}" (2スペース区切り)。
    /// </summary>
    public static string FormatElapsed(TimeSpan elapsed)
        => elapsed.TotalSeconds < 1.0
            ? $"{elapsed.TotalMilliseconds:0} ms"
            : $"{elapsed.TotalSeconds:0.0} s";

    /// <summary>
    /// 進捗バーとステータスラベルを統一書式で更新する。ワーカースレッドから呼ばれた場合は自動で Invoke する。
    /// 表示書式: "{stage}  {pct:0.0}%  ({elapsed})  remaining ~{rem}" (各要素は引数で省略可)。
    /// </summary>
    /// <param name="bar">進捗バー (null 可)。Maximum は内部で正規化するので呼び出し側は意識不要</param>
    /// <param name="label">メッセージ表示ラベル (null 可)</param>
    /// <param name="ratio">進捗 0.0–1.0 (範囲外はクランプ)</param>
    /// <param name="stage">ステージ説明 (例 "Searching g-vectors")</param>
    /// <param name="elapsed">経過時間。null で時間非表示</param>
    /// <param name="showRemaining">残り時間の推定を併記するか</param>
    /// <param name="showPercent">パーセント表示をするか</param>
    public static void SetProgress(
        ToolStripProgressBar bar, ToolStripStatusLabel label,
        double ratio, string stage = "",
        TimeSpan? elapsed = null, bool showRemaining = false, bool showPercent = true)
    {
        if (double.IsNaN(ratio) || ratio < 0) ratio = 0;
        else if (ratio > 1) ratio = 1;

        void apply()
        {
            if (bar != null)
            {
                if (bar.Maximum <= 0) bar.Maximum = 100;
                bar.Value = (int)Math.Round(ratio * bar.Maximum);
            }
            if (label != null)
            {
                var sb = new StringBuilder(stage ?? "");
                if (showPercent)
                {
                    if (sb.Length > 0) sb.Append("  ");
                    sb.Append($"{ratio * 100:0.0}%");// 260520Cl: パーセントは小数1桁・空白なし (書式設計で確定)
                }
                if (elapsed is TimeSpan e)
                {
                    if (sb.Length > 0) sb.Append("  ");
                    sb.Append('(').Append(FormatElapsed(e)).Append(')');
                    if (showRemaining && ratio > 1e-6)
                    {
                        // 260521Cl ratio が極小だと remaining が long 範囲を超え、(long)キャストで負値/巨大値が表示され得るためガード
                        var remainingTicks = e.Ticks * (1.0 - ratio) / ratio;
                        if (remainingTicks >= 0 && remainingTicks < long.MaxValue)
                            sb.Append("  remaining ~").Append(FormatElapsed(TimeSpan.FromTicks((long)remainingTicks)));
                    }
                }
                label.Text = sb.ToString();
            }
        }

        // 進捗バー/ラベルを載せている StatusStrip 経由でスレッドマーシャリング
        var owner = bar?.GetCurrentParent() ?? label?.GetCurrentParent();
        if (owner != null && owner.InvokeRequired)
            owner.Invoke((Action)apply);
        else
            apply();
    }
}

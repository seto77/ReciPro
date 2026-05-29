#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace OpenTK.GLControl
{
    /// <summary>
    /// 260529Cl 追加: Windows on ARM 環境での描画不具合診断用の簡易ログ。
    /// FormRotation の 6 個の GLControl を主対象にしつつ、未マッチ名も 1 回だけ記録して
    /// Name のミス検出を可能にする。出力先は <see cref="LogPath"/>。
    /// 主候補は実行ファイルと同じディレクトリ。失敗時は %TEMP% にフォールバック。
    /// </summary>
    internal static class GLDebugLog
    {
        /// <summary>実際に書き込み成功したログ出力先。</summary>
        public static string LogPath { get; private set; } = "";

        private static readonly object _lock = new();
        private static bool _initialized;

        /// <summary>
        /// 260529Cl 追加: 診断ログの有効/無効。環境変数 RECIPRO_GLDIAG が非空のときだけ有効。
        /// 未設定 (通常起動) では全 <see cref="Log"/> が即 no-op になり、ログファイルも作成しない。
        /// (RECIPRO_GLDIAG=1 → 実描画+ログ / RECIPRO_GLDIAG=bands → ログ+カラーバンド診断)
        /// </summary>
        public static readonly bool Enabled =
            !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("RECIPRO_GLDIAG"));

        // FormRotation 内の 6 個の GLControl 名 + FormMain の glControlAxes (詳細ログ対象)。
        // RENDER_PROBE は Render() の入口を観測する sentinel。
        private static readonly HashSet<string> TargetNames = new(StringComparer.Ordinal)
        {
            "glControlReciProObjects",
            "glControlReciProAxes",
            "glControlReciProGonio",
            "glControlExpObjects",
            "glControlExpAxes",
            "glControlExpGonio",
            "glControlAxes",  // FormMain の Dock=Top GLControl (260529Cl)
            "RENDER_PROBE",
        };

        // 未マッチ名を 1 回だけログするための重複除去セット。
        private static readonly HashSet<string> _observedNonTargets = new(StringComparer.Ordinal);

        public static bool IsTarget(string? name) => name != null && TargetNames.Contains(name);

        /// <summary>
        /// 起動時 (= 最初の Log 呼び出し時) に強制実行される初期化。
        /// 候補パスを順に試し、最初に書き込めたものを <see cref="LogPath"/> として採用する。
        /// </summary>
        private static void TryInitialize()
        {
            if (_initialized) return;
            _initialized = true;

            var candidates = new List<string>
            {
                Path.Combine(AppContext.BaseDirectory, "ReciPro_GLControl.log"),
                Path.Combine(Path.GetTempPath(), "ReciPro_GLControl.log"),
                Path.Combine(Environment.CurrentDirectory, "ReciPro_GLControl.log"),
            };

            var header =
                $"=== ReciPro GLControl debug log start: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} ===\r\n" +
                $"AppContext.BaseDirectory   = {AppContext.BaseDirectory}\r\n" +
                $"Environment.ProcessPath    = {Environment.ProcessPath}\r\n" +
                $"Environment.CurrentDirectory = {Environment.CurrentDirectory}\r\n" +
                $"RuntimeInformation.OSArchitecture     = {RuntimeInformation.OSArchitecture}\r\n" +
                $"RuntimeInformation.ProcessArchitecture = {RuntimeInformation.ProcessArchitecture}\r\n" +
                $"RuntimeInformation.OSDescription      = {RuntimeInformation.OSDescription}\r\n" +
                $"Candidate paths tried (in order):\r\n";
            for (int i = 0; i < candidates.Count; i++)
                header += $"  [{i}] {candidates[i]}\r\n";
            header += "----------------------------------------\r\n";

            foreach (var path in candidates)
            {
                try
                {
                    File.WriteAllText(path, header + $"** Using this path: {path} **\r\n");
                    LogPath = path;
                    return;
                }
                catch
                {
                    // 次候補へ
                }
            }
            // 全部失敗: LogPath は "" のまま、以降の Log は黙って no-op
        }

        public static void Log(string controlName, string evt, string msg)
        {
            if (!Enabled) return; // 260529Cl 追加: 通常起動 (RECIPRO_GLDIAG 未設定) では何もしない
            try
            {
                lock (_lock)
                {
                    TryInitialize();
                    if (string.IsNullOrEmpty(LogPath)) return;

                    if (IsTarget(controlName))
                    {
                        File.AppendAllText(LogPath, $"{DateTime.Now:HH:mm:ss.fff} [{controlName,-26}] {evt,-22}: {msg}\r\n");
                    }
                    else
                    {
                        // 未マッチ名は 1 回だけ「観測されたが対象外」として記録 (Name のミスマッチ検出用)。
                        var key = controlName ?? "<null>";
                        if (_observedNonTargets.Add(key))
                            File.AppendAllText(LogPath, $"{DateTime.Now:HH:mm:ss.fff} [{key,-26}] (non-target observed; will not log further events for this control)\r\n");
                    }
                }
            }
            catch
            {
                // ログ失敗で本体を巻き込まない
            }
        }
    }
}

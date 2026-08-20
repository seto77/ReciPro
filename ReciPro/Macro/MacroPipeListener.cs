using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace ReciPro;

// 260723Cl 追加: 外部プロセスから IronPython マクロを受け付ける Named Pipe リスナー (既定 OFF、Option メニューで有効化)。
//
// プロトコル v1:
//   リクエスト  = 厳格 UTF-8 のマクロ文字列 + 終端 NUL(0x00)。最大 1 MiB、接続後 30 秒以内。
//   レスポンス  = UTF-8 JSON {"output":"...","error":"..."} を書いた後サーバー側がクローズ (クライアントは EOF まで読む)。
//                 error はコンパイル/実行失敗またはプロトコルエラー専用 (成功時は空文字)。
//   クライアント例 (Python):
//     with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
//         f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
//         print(f.read().decode('utf-8'))
//
// 設計要点 (codex 相談で確定):
//   - 専用バックグラウンドスレッド + 同期ループ。マクロ実行は FormMain.Invoke で UI スレッドへ直列化し
//     FormMacro.RunMacro(code, quiet: true) に委譲 (Macro API は UI スレッド前提のため)。
//   - 読み書きは Asynchronous ハンドル + CancellationToken 付き Read/WriteAsync を同期待ちして期限を付ける
//     (PipeStream は ReadTimeout 非対応。読まない/送らないクライアントで永久ブロックさせない)。
//   - 切断は DisconnectNamedPipe (未読データ破棄) ではなく Dispose (クライアントはバッファを読み切って EOF)。
//   - リスナーの所有権は名前付き Mutex で確保 (多重起動時は先着のみ。pipe busy との判別を安定させる)。
//   - Stop は UI スレッドから呼ばれるためフラグ + Dispose のみで Join しない (Invoke 完了待ちとのデッドロック回避)。
//   - 1 インスタンス = 1 回の Start。ON/OFF のたびに FormMain 側で新規生成して使い捨てる
//     (stopping フラグを新旧スレッドで共有しないことで Start/Stop の競合を排除する, codex 指摘)。
public sealed class MacroPipeListener
{
    public const string PipeName = "ReciPro.Macro.v1";
    private const string MutexName = @"Local\ReciPro.MacroPipe.v1";
    private const int MaxRequestBytes = 1024 * 1024;
    private static readonly TimeSpan Deadline = TimeSpan.FromSeconds(30);

    private readonly FormMain main;
    private readonly Lock sync = new();
    private volatile bool stopping = false;
    private NamedPipeServerStream server;
    private Thread thread;

    public MacroPipeListener(FormMain main) => this.main = main;

    public void Start()
    {
        lock (sync)
        {
            if (thread != null) return;
            stopping = false;
            thread = new Thread(Run) { IsBackground = true, Name = "MacroPipeListener" };
            thread.Start();
        }
    }

    /// <summary>停止 (冪等)。UI スレッドから呼んでも安全 (Join しない)。停止後の再 Start は不可 (新インスタンスを生成する)。</summary>
    public void Stop()
    {
        lock (sync)
        {
            stopping = true;
            try { server?.Dispose(); } catch { } // WaitForConnection / Read を例外で解除する
            server = null;
        }
    }

    private void Run()
    {
        Mutex mutex = null;
        try
        {
            mutex = new(false, MutexName);
            // 他インスタンス (別プロセスの ReciPro) がリスナーを所有していれば静かに終了する。
            // 短い期限付きなのは、OFF→ON 直後に旧リスナースレッドが Mutex を解放し切る前でも取得できるようにするため (codex 指摘)
            try { if (!mutex.WaitOne(TimeSpan.FromSeconds(2))) return; }
            catch (AbandonedMutexException) { } // 前所有者の異常終了。取得成功として扱う

            while (!stopping)
            {
                NamedPipeServerStream s = null;
                try
                {
                    s = new(PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte,
                            PipeOptions.CurrentUserOnly | PipeOptions.Asynchronous);
                    lock (sync)
                    {
                        if (stopping) return;
                        server = s;
                    }
                    s.WaitForConnection();
                    ServeOneRequest(s);
                }
                catch when (stopping) { return; }
                catch (IOException) { Thread.Sleep(200); } // 前クライアントのハンドル残存等による一時的な busy → 間を置いて再試行
                finally
                {
                    lock (sync) { if (ReferenceEquals(server, s)) server = null; }
                    try { s?.Dispose(); } catch { }
                }
            }
        }
        catch { } // リスナー全体の未処理例外をスレッド外へ漏らさない (バックグラウンドスレッドでもプロセスを落とすため)
        finally
        {
            try { mutex?.ReleaseMutex(); } catch { }
            mutex?.Dispose();
        }
    }

    /// <summary>1 接続 = 1 コマンド。リクエストを読み、UI スレッドで実行し、JSON を書き返す。</summary>
    private void ServeOneRequest(NamedPipeServerStream s)
    {
        string output = "", error;
        var code = ReadRequest(s, out error);
        if (error.Length == 0)
        {
            // 停止要求後に読み終えたリクエストは実行しない (Invoke 待ち中の停止も delegate 内で再確認, codex 指摘)
            if (stopping)
                error = "Listener stopped.";
            else
                try
                {
                    (output, error) = ((string, string))main.Invoke(
                        new Func<(string, string)>(() => stopping ? ("", "Listener stopped.")
                                                                  : main.FormMacro.RunMacro(code, quiet: true)));
                }
                catch (Exception ex) { error = ex.Message; } // フォーム破棄中の Invoke 失敗など
        }
        try
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(new { output, error });
            using var cts = new CancellationTokenSource(Deadline);
            s.WriteAsync(json, 0, json.Length, cts.Token).GetAwaiter().GetResult();
            s.Flush();
        }
        catch { } // クライアント切断済み・応答を読まない等。接続単位の失敗でリスナーは止めない
    }

    /// <summary>NUL 終端までを厳格 UTF-8 で読む。失敗時は error にプロトコルエラーを返す (途中データは実行しない)。</summary>
    private static string ReadRequest(NamedPipeServerStream s, out string error)
    {
        error = "";
        using var cts = new CancellationTokenSource(Deadline);
        using var ms = new MemoryStream();
        var buf = new byte[65536];
        try
        {
            while (true)
            {
                var n = s.ReadAsync(buf, 0, buf.Length, cts.Token).GetAwaiter().GetResult();
                if (n == 0) { error = "Protocol error: connection closed before NUL terminator."; return null; }
                var nul = Array.IndexOf(buf, (byte)0, 0, n);
                ms.Write(buf, 0, nul >= 0 ? nul : n);
                if (ms.Length > MaxRequestBytes) { error = $"Protocol error: request exceeded {MaxRequestBytes} bytes."; return null; }
                if (nul >= 0)
                    return new UTF8Encoding(false, throwOnInvalidBytes: true).GetString(ms.ToArray());
            }
        }
        catch (OperationCanceledException) { error = $"Protocol error: request was not completed within {Deadline.TotalSeconds:0} s."; return null; }
        catch (DecoderFallbackException) { error = "Protocol error: request is not valid UTF-8."; return null; }
    }
}

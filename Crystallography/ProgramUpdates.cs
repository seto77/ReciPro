using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
//using System.Net; //260317Cl コメントアウト WebClient→HttpClient移行
using System.Net.Http;
using System.Reflection;
using System.Windows.Forms;

namespace Crystallography;

public static class ProgramUpdates
{
    private static readonly string UserAppDataPath = new DirectoryInfo(Application.UserAppDataPath).Parent.FullName + @"\";

    //260317Cl HttpClientはstaticで再利用するのがベストプラクティス
    private static readonly HttpClient httpClient = new();

    public static (string Title, string Message, bool NeedUpdate, string URL, string Path) Check(string software, string version)
    {
        try
        {
            //using var wc = new WebClient(); //260317Cl WebClient→HttpClient
            //var ver = wc.DownloadData(...);
            var ver = httpClient.GetByteArrayAsync($"https://raw.githubusercontent.com/seto77/{software}/master/{software}/Version.cs").Result;

            //V上手くダウンロードできなかった場合
            if (ver == null || ver.Length == 0)
                return ("Error!", $"An error occurred while trying to locate the update to {software}.\r\n " +
                    "This could be caused if you do not have an active internet connection, or host server may be down. ", false, "", "");

            var temp = System.Text.Encoding.UTF8.GetString(ver).Split(new[] { '\r', '\n' });
            var newVersion = temp.First(s => s.Contains(" ver", StringComparison.Ordinal));
            newVersion = newVersion.Substring(newVersion.IndexOf("ver") + 3, 5);

            if (Convert.ToDouble(newVersion) <= Convert.ToDouble(version.Substring(3, 5)))
                return ("Update checked!", $"You are running the latest version of {software}. Thank you!", false, "", "");
            else
                return ($"Update checked!", $"Now, new version {newVersion} is available.\r\n" +
                     $"If you press 'Yes', the current {software} will be closed immediately and the installer of new {software} launched.", true,
                     $"http://github.com/seto77/{software}/releases/download/v.{newVersion}/{software}Setup.msi",
                     UserAppDataPath + software + "Setup.msi");
        }
        catch
        {
            return ("Error!", "An error occurred while trying to locate the update to " + software + ".\r\n" +
                " This could be caused if you do not have an active internet connection, administrative" +
                " right to access to internet, or host server may be down. Sorry.", false, "", "");
        }
    }

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static bool Execute(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }

    }

    //260317Cl DownloadProgressChangedEventArgs→引数に変更 (WebClient依存を除去)
    //public static ... ProgressMessage(DownloadProgressChangedEventArgs e, Stopwatch stopwatch)
    public static (long Current, long Total, long ElapsedMilliseconds, string Message)
        ProgressMessage(long bytesReceived, long totalBytesToReceive, Stopwatch stopwatch)
    {
        var receivedMb = bytesReceived / 1E6;
        var totalMb = totalBytesToReceive / 1E6;
        var message = $"Downloading setup file.  Received: {receivedMb:f1} MB / {totalMb:f1} MB.  ";
        return (bytesReceived, totalBytesToReceive, stopwatch.ElapsedMilliseconds, message);
    }

    /// <summary>
    /// 260317Cl 追加 HttpClientでファイルをダウンロードし進捗を報告する
    /// </summary>
    public static async System.Threading.Tasks.Task DownloadFileWithProgressAsync(
        string url, string path, IProgress<(long Current, long Total, long ElapsedMilliseconds, string Message)> progress, Stopwatch stopwatch)
    {
        using var response = await httpClient.GetAsync(new Uri(url), HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        var totalBytes = response.Content.Headers.ContentLength ?? -1;
        using var contentStream = await response.Content.ReadAsStreamAsync();
        using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
        var buffer = new byte[8192];
        long bytesRead = 0;
        int read;
        long counter = 0;
        while ((read = await contentStream.ReadAsync(buffer)) > 0)
        {
            await fileStream.WriteAsync(buffer.AsMemory(0, read));
            bytesRead += read;
            if (counter++ % 10 == 0)
                progress?.Report(ProgressMessage(bytesRead, totalBytes, stopwatch));
        }
    }

}

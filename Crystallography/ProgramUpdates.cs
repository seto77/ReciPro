using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Crystallography
{
    public static class ProgramUpdates
    {
        private static readonly string UserAppDataPath = new DirectoryInfo(Application.UserAppDataPath).Parent.FullName + @"\";

        public static (string Title, string Message, bool NeedUpdate, string URL, string Path) Check(string software, string version)
        {
            try
            {
                using var wc = new WebClient();
                var ver = wc.DownloadData($"https://raw.githubusercontent.com/seto77/{software}/master/{software}/Version.cs");

                //V上手くダウンロードできなかった場合
                if (ver == null || ver.Length == 0)
                    return ("Error!", $"An error occured while trying to locate the update to {software}.\r\n " +
                        "This could be caused if you do not have an active internet connection, or host server may be down. ", false, "", "");

                var temp = System.Text.Encoding.UTF8.GetString(ver).Split(new[] { '\r', '\n' });
                var newVersion = temp.First(s => s.Contains("ver"));
                newVersion = newVersion.Substring(newVersion.IndexOf("ver") + 3, 5);

                if (Convert.ToDouble(newVersion) <= Convert.ToDouble(version.Substring(3, 5)))
                    return ("Update checked!", $"You are runnning the latest version of {software}. Thank you!", false, "", "");
                else
                    return ($"Update checked!", $"Now, new version {newVersion} is available.\r\n" +
                         $"If you press 'Yes', the current {software} will be closed immediately and the installer of new {software} launched.", true,
                         $"http://github.com/seto77/{software}/releases/download/v.{newVersion}/{software}Setup.msi",
                         UserAppDataPath + software + "Setup.msi");
            }
            catch
            {
                return ("Error!", "An error occured while trying to locate the update to " + software + ".\r\n" +
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

        public static (long Current, long Total, long ElapsedMilliseconds, string Message)
            ProgressMessage(DownloadProgressChangedEventArgs e, Stopwatch stopwath)
        {
            var receivedMb = e.BytesReceived / 1E6;
            var totalMb = e.TotalBytesToReceive / 1E6;
            var message = $"Downloading setup file.  Received: {receivedMb:f1} MB / {totalMb:f1} MB.  ";
            return (e.BytesReceived, e.TotalBytesToReceive, stopwath.ElapsedMilliseconds, message);
        }

    }
}
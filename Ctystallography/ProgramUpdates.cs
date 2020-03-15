using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Crystallography
{
    public static class ProgramUpdates
    {

        public static string UserAppDataPath = new DirectoryInfo(Application.UserAppDataPath).Parent.FullName + @"\";
        public static bool CheckUpdate(string software, string version)
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) return false;//click onceの場合
            bool update = false;
            try
            {
                if (File.Exists("Version.cs"))
                    File.Delete("Version.cs");

                var wc = new System.Net.WebClient();


                wc.DownloadFile("https://raw.githubusercontent.com/seto77/" + software + "/master/" + software + "/Version.cs", "Version.cs");

                if (!File.Exists("Version.cs")) {
                    MessageBox.Show("An error occured while trying to locate the update to " + software + ".\r\n"
                   + " This could be caused if you do not have an active internet connection, or host server may be down. ", "Error",
                   MessageBoxButtons.OK);
                    return false;
                }

                var newVersion = "";
                using (var sr = new StreamReader("Version.cs"))
                {
                    var temp = sr.ReadToEnd().Split(new[] { '\r', '\n' });
                    newVersion = temp.First(s => s.Contains("ver"));
                    newVersion = newVersion.Substring(newVersion.IndexOf("ver") + 3, 5);
                }
                File.Delete("Version.cs");

               // if (Convert.ToDouble(newVersion) <= Convert.ToDouble(version.Substring(3, 5)))
               if(false)
                {
                    MessageBox.Show("You are runnning the latest version of " + software + ". Thank you!", "Update checked!", MessageBoxButtons.OK);
                }
                else if (MessageBox.Show("Now, new version " + newVersion + " is available.\r\n" +
                        "If you press 'Yes', the current " + software + " will be closed immediately and the installer of new " + software + " launched.", "Update checked!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var n = 1;
                    wc.DownloadProgressChanged += (s, ev) =>
                    {
                        //if (n++ % 10 == 0)
                        {
                            var receivedMb = ev.BytesReceived / 1000000.0;
                            var totalMb = ev.TotalBytesToReceive / 1000000.0;
                            var ratio = receivedMb / totalMb;
                            var ellapsedSec = sw.ElapsedMilliseconds / 1000.0;
                            var remainingSec = ellapsedSec / ratio * (1 - ratio);
                            ProgressChanged?.Invoke(
                                ev.BytesReceived, ev.TotalBytesToReceive, ratio, ellapsedSec, remainingSec,
                                "Downloading " + software + "Setup.msi." +
                                "  Total to receive: " + totalMb.ToString("f2") + " MB." +
                                "  Received: " + receivedMb.ToString("f2") + " MB." +
                                "  Ellapsed time: " + ellapsedSec.ToString("f1") + " sec." +
                                "  Remainning time: " + remainingSec.ToString("f1") + " sec.");
                            n = 0;
                        }
                    };
                    wc.DownloadFileCompleted += (s, ev) =>
                    {
                        Completed?.Invoke("Downloaded " + software + "Setup.msi.");
                    };
                    
                    wc.DownloadFileTaskAsync(new Uri("http://github.com/seto77/" + software + "/releases/download/v." + newVersion + "/" + software + "Setup.msi"),
                        UserAppDataPath + software + "Setup.msi");

                    while (wc.IsBusy)
                    {
                        Thread.Sleep(10);
                        if (sw.ElapsedMilliseconds > 60000)
                            throw new Exception();
                    }
                    if (File.Exists(UserAppDataPath + software + "Setup.msi"))
                        Process.Start(UserAppDataPath + software + "Setup.msi");
                    update = true;
                }
            }
            catch
            {
                MessageBox.Show("An error occured while trying to locate the update to " + software + "."
                   + "\r\n This could be caused if you do not have an active internet connection, administrative right to access to internet, or host server may be down. Sorry.", "Error", MessageBoxButtons.OK);

            }
            return update;
        }

       
        public delegate void ProgressChangedEventHandler(long currentBytes, long totalBytes, double ratio, double ellapsedSec, double remainingSec, string message);
        public static event ProgressChangedEventHandler ProgressChanged;
        public delegate void CompletedEventHandler(string message);
        public static event CompletedEventHandler Completed;

        /*
        public static bool CheckUpdate_old(string software, string version)
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) return false;//click onceの場合

            bool update = false;

            try
            {
                new Network().DownloadFile("http://pmsl.planet.sci.kobe-u.ac.jp/~seto/software/" + software + "/NewestVersion.txt",
                    "NewestVersion.txt", "", "", false, 1000, true, UICancelOption.ThrowException);

                if (!File.Exists("NewestVersion.txt"))
                    MessageBox.Show("An error occured while trying to locate the update to " + software + "."
                   + "\r\n This could be caused if you do not have an active internet connection, or host server may be down. ", "Error", MessageBoxButtons.OK);

                string data;
                using (var sr = new StreamReader("NewestVersion.txt"))
                    data = sr.ReadLine();
                File.Delete("NewestVersion.txt");

                if (Convert.ToDouble(data.Substring(3, 5)) <= Convert.ToDouble(version.Substring(3, 5)))
                { 
                    MessageBox.Show("You are runnning the latest version of " + software + ". Thank you!", "Update checked!", MessageBoxButtons.OK);
                }
                else if (MessageBox.Show("Now, new version " + data.Substring(3) + " is available.\r\n" +
                        "If you press 'Yes', the current "+ software + " will be closed immediately and the installer of new " + software + " launched.", "Update checked!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    new Network().DownloadFile("http://pmsl.planet.sci.kobe-u.ac.jp/~seto/software/"+software+"Setup.msi",
                        UserAppDataPath + software + "Setup.msi", "", "", true, 6000, true, UICancelOption.ThrowException);
                    if(File.Exists(UserAppDataPath + software + "Setup.msi"))              
                        Process.Start(UserAppDataPath + software + "Setup.msi");
                    update = true;
                }

            }
            catch
            {
                MessageBox.Show("An error occured while trying to locate the update to " + software + "."
                   + "\r\n This could be caused if you do not have an active internet connection, administrative right to access to internet, or host server may be down. Sorry.", "Error", MessageBoxButtons.OK);

            }
            return update;
        }
        */
    }
}
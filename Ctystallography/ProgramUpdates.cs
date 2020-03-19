using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystallography
{
    public static class ProgramUpdates
    {
        //public static WebClient Client;

        private static Stopwatch stopwath = new Stopwatch();

        private static string UserAppDataPath = new DirectoryInfo(Application.UserAppDataPath).Parent.FullName + @"\";

        public static (string Title, string Message, bool NeedUpdate, string URL, string Path) Check(string software, string version)
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                return ("Error!", "This is Click Once version. The update function is only available for msi version. Sorry.", false, "","");//click onceの場合
            try
            {
                if (File.Exists("Version.cs"))
                    File.Delete("Version.cs");

                using (var wc = new WebClient())
                {
                    wc.DownloadFile($"https://raw.githubusercontent.com/seto77/{software}/master/{software}/Version.cs", "Version.cs");

                    //Version.CSファイルが上手くダウンロードできなかった場合
                    if (!File.Exists("Version.cs"))
                        return ("Error!", $"An error occured while trying to locate the update to {software}.\r\n " +
                            "This could be caused if you do not have an active internet connection, or host server may be down. ", false, "", "");

                    var newVersion = "";
                    using (var sr = new StreamReader("Version.cs"))
                    {
                        var temp = sr.ReadToEnd().Split(new[] { '\r', '\n' });
                        newVersion = temp.First(s => s.Contains("ver"));
                        newVersion = newVersion.Substring(newVersion.IndexOf("ver") + 3, 5);
                    }
                    File.Delete("Version.cs");

                    if (Convert.ToDouble(newVersion) <= Convert.ToDouble(version.Substring(3, 5)))
                        return ("Update checked!", $"You are runnning the latest version of {software}. Thank you!", false, "", "");
                    else
                        return ($"Update checked!", $"Now, new version {newVersion} is available.\r\n" +
                             $"If you press 'Yes', the current {software} will be closed immediately and the installer of new {software} launched.", true,
                             $"http://github.com/seto77/{software}/releases/download/v.{newVersion}/{software}Setup.msi",
                             UserAppDataPath + software + "Setup.msi");
                }
            }
            catch
            {
                return ("Error!", "An error occured while trying to locate the update to " + software + ".\r\n" +
                    " This could be caused if you do not have an active internet connection, administrative" +
                   " right to access to internet, or host server may be down. Sorry.", false, "", "");
            }
        }


        public static bool Execute(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    Process.Start(path);
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
           var message = $"Downloading setup file.  Received: {receivedMb:f1} MB / {totalMb:f1} MB.  " ;
            return (e.BytesReceived, e.TotalBytesToReceive, stopwath.ElapsedMilliseconds, message);
        }


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
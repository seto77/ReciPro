using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.FileIO;

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
                System.Net.WebClient wc = new System.Net.WebClient();

                new Network().DownloadFile("https://raw.githubusercontent.com/seto77/" + software + "/master/" + software + "/Version.cs",
                "Version.cs", "", "", false, 1000, true, UICancelOption.ThrowException);


                if (!File.Exists("Version.cs"))
                    MessageBox.Show("An error occured while trying to locate the update to " + software + "."
                   + "\r\n This could be caused if you do not have an active internet connection, or host server may be down. ", "Error", MessageBoxButtons.OK);

                var newVersion = "";
                using (var sr = new StreamReader("Version.cs"))
                {
                    var temp =  sr.ReadToEnd().Split(new[] { '\r', '\n' });
                    newVersion = temp.First(s => s.Contains("ver")).Substring(newVersion.IndexOf("ver") + 3, 5);
                }
                File.Delete("Version.cs");

                if (Convert.ToDouble(newVersion) <= Convert.ToDouble(version.Substring(3, 5)))
                {
                    MessageBox.Show("You are runnning the latest version of " + software + ". Thank you!", "Update checked!", MessageBoxButtons.OK);
                }
                else if (MessageBox.Show("Now, new version " + newVersion + " is available.\r\n" +
                        "If you press 'Yes', the current " + software + " will be closed immediately and the installer of new " + software + " launched.", "Update checked!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    
                    wc.DownloadFile("http://github.com/seto77/" + software + "/releases/download/v." + newVersion + "/" + software + "Setup.msi", UserAppDataPath + software + "Setup.msi");
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
    }
}
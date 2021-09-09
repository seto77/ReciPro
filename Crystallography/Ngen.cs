using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Crystallography
{
    public static class Ngen
    {
        public static void Compile()
        {
            string currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            //if (currentPath.Contains("Visual Studio 2015\\Projects")) return;

            try
            {
                string dir = "";

                //64bit環境でのFrameworkフォルダを探す
                if (System.Environment.Is64BitProcess)
                {
                    for (char drive = 'a'; drive < 'z'; drive++)
                        if (Directory.Exists(drive + ":\\Windows\\Microsoft.NET\\Framework64"))
                        {
                            dir = drive + ":\\Windows\\Microsoft.NET\\Framework64";
                            break;
                        }
                }
                //32bit環境でのFrameworkフォルダを探す
                if (dir.Length == 0)
                {
                    for (char drive = 'a'; drive < 'z'; drive++)
                        if (Directory.Exists(drive + ":\\Windows\\Microsoft.NET\\Framework"))
                        {
                            dir = drive + ":\\Windows\\Microsoft.NET\\Framework";
                            break;
                        }
                }
                //見つからなかったら何もせずに戻る
                if (dir.Length == 0) return;

                //最新バージョンを検索する
                string[] dirs = Directory.GetDirectories(dir);
                Array.Sort(dirs);
                Array.Reverse(dirs);

                string filename = "";
                for (int i = 0; i < dirs.Length; i++)
                    if (File.Exists(dirs[i] + "\\ngen.exe"))
                    {
                        filename = dirs[i] + "\\ngen.exe";
                        break;
                    }
                if (filename.Length == 0) return;

                //ProcessStartInfo psi = new ProcessStartInfo();

                //psi.FileName = filename;
                //psi.Verb = "RunAs";
                //psi.CreateNoWindow = true;
                //psi.ErrorDialog = true;
                //psi.UseShellExecute = false;

                Process proc = new Process();
                proc.StartInfo.FileName = filename;

                // 管理者として実行
                proc.StartInfo.Verb = "RunAs";

                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.ErrorDialog = true;
                //proc.StartInfo.UseShellExecute = false;

                string[] str = Directory.GetFileSystemEntries(currentPath, "*", SearchOption.TopDirectoryOnly);

                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i].EndsWith(".exe"))
                    {
                        //psi.WorkingDirectory = Path.GetDirectoryName(str[i]);
                        //psi.Arguments = "install " + str[i];// + " /nologo /silent";
                        //Process.Start(psi);
                        proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(str[i]);
                        proc.StartInfo.Arguments = " install " + Path.GetFileName(str[i]);// + " /nologo /silent";
                        proc.Start();
                        proc.WaitForExit();
                    }
                }
                proc.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
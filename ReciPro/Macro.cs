using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ReciPro;

public class Macro : MacroBase
{
    #region 基底クラス
    private readonly FormMain main;
    public FileClass File;
    public RotationClass Rotation;

    public Macro(FormMain _main) : base(_main, "ReciPro")
    {
        main = _main;
        File = new FileClass(this);
        Rotation = new RotationClass(this);
        help.Add("ReciPro.Sleep(int millisec) # Sleep.");
    }

    public static void Sleep(int millisec) => Thread.Sleep(millisec);
    public object[] Obj { get; set; }

    #endregion

    #region Rotationクラス
    public class RotationClass : MacroSub
    {
        private readonly Macro p;
        public RotationClass(Macro _p) : base(_p.main)
        {
            this.p = _p;
            p.help.Add("ReciPro.Rotation.Rotate() # ");
            p.help.Add("ReciPro.Rotation.Euler(double phi, double theta, double psi) # Sets the rotation state by Euler angles.");
            p.help.Add("ReciPro.Rotation.EulerInDegree(double phi, double theta, double psi) # Sets the rotation state by Euler angles (in degree).");
        }

        public void Euler(double phi, double theta, double psi)
        {
            p.main.SetRotation(phi, theta, psi);
        }

        public void EulerInDegree(double phi, double theta, double psi)
        {
            p.main.SetRotation(phi / 180.0 * Math.PI, theta / 180.0 * Math.PI, psi / 180.0 * Math.PI);
        }


        public void Rotate(double vX, double vY, double vZ, double angle)
        {
            p.main.Rotate((vX, vY, vZ), angle);
        }
    }
    #endregion

    #region ファイルクラス
    public class FileClass : MacroSub
    {
        private readonly Macro p;
        public FileClass(Macro _p) : base(_p.main)
        {
            this.p = _p;
            p.help.Add("ReciPro.File.GetFileName() # Get a file name.  \r\n Returned string is a full path of the selected file.");
            p.help.Add("ReciPro.File.GetFileNames() # Get file names.  \r\n Returned value is a string array, \r\n  each of which is a full path of selected files.");
            p.help.Add("ReciPro.File.GetDirectoryPath(string filename) # Get a directory path.\r\n Returned string is a full path to the filename.\r\n If filename is omitted, selection dialog will open.");

            p.help.Add("ReciPro.File.ReadProfiles(string filename) # Read profile data. \r\n If filename is omitted, selection dialog will open.");
            p.help.Add("ReciPro.File.SaveProfiles(string filename) # Save profile data. \r\n If filename is omitted, selection dialog will open.");
            p.help.Add("ReciPro.File.ReadCrystals(string filename) # Read crystal data. \r\n If filename is omitted, selection dialog will open.");
            p.help.Add("ReciPro.File.SaveCrystals(string filename) # Save crystal data. \r\n If filename is omitted, selection dialog will open.");
            p.help.Add("ReciPro.File.SaveMetafile(string filename) # Save metafile object. \r\n If filename is omitted, selection dialog will open.");

            p.help.Add("PDI.File.SaveText(string text, string filename) # Save textfile object. \r\n If filename is omitted, selection dialog will open.");
        }

        public string GetDirectoryPath(string filename = "") => Execute<string>(new Func<string>(() => getDirectoryPath(filename)));
        private static string getDirectoryPath(string filename = "")
        {
            string path = "";
            if (filename == "")
            {
                var dlg = new FolderBrowserDialog();
                path = dlg.ShowDialog() == DialogResult.OK ? dlg.SelectedPath : "";
            }
            else
                path = System.IO.Path.GetDirectoryName(filename);
            return path + "\\";
        }


        public string GetFileName() => Execute(() => getFileName());
        private static string getFileName()
        {
            var dlg = new OpenFileDialog();
            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileName : "";
        }

        public string[] GetFileNames() => Execute<string[]>(new Func<string[]>(() => getFileNames()));
        private static string[] getFileNames()
        {
            var dlg = new OpenFileDialog() { Multiselect = true };
            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileNames : Array.Empty<string>();
        }

        public void ReadProfiles(string fileName = "") => Execute(() => readProfiles(fileName));
        private void readProfiles(string fileName = "")
        {
            //if (!System.IO.File.Exists(fileName))
            //    p.main.menuItemFileRead_Click(new object(), new EventArgs());
            //else
            //    p.main.readProfile(fileName);
        }

        public void SaveProfiles(string filename = "") => Execute(new Action(() => saveProfiles(filename)));
        private void saveProfiles(string filename = "")
        {
            //if (filename == "")
            //    p.main.savePatternProfileToolStripMenuItem_Click(new object(), new EventArgs());
            //else
            //    p.main.SaveProfile(filename);
        }

        public void ReadCrystals(string filename = "") => Execute(new Action(() => readCrystals(filename)));
        private void readCrystals(string filename = "")
        {
            //if (!System.IO.File.Exists(filename))
            //    p.main.menuItemReadCrystalData_Click(new object(), new EventArgs());
            //else
            //    p.main.readCrystal(filename, false, true);
        }

        public void SaveCrystals(string filename = "") => Execute(new Action(() => saveCrystals(filename)));
        private void saveCrystals(string filename = "")
        {
            //if (filename == "")
            //    p.main.menuItemSaveCrystalData_Click(new object(), new EventArgs());
            //else
            //    p.main.saveCrystal(filename);
        }

        public void SaveMetafile(string filename = "") { Execute(new Action(() => saveMetafile(filename))); }
        private void saveMetafile(string filename = "")
        {
            //if (filename == "")
            //    p.main.toolStripMenuItemSaveMetafile_Click(new object(), new EventArgs());
            //else
            //    p.main.saveMetafile(filename);
        }

        public void SaveText(string text, string filename = "") { Execute(new Action(() => saveText(text, filename))); }
        private static void saveText(string text, string filename = "")
        {
            if (filename == "")
            {
                var dlg = new SaveFileDialog() { Filter = "*.txt|*.txt" };
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                filename = dlg.FileName;
            }
            var sw = new StreamWriter(filename);
            sw.Write(text);
            sw.Flush();
            sw.Close();
        }

    }
    #endregion

}

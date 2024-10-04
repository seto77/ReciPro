#region using 
using MathNet.Numerics;
using MemoryPack.Formatters;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ReciPro;
#endregion

public class Macro : MacroBase
{
    #region 基底クラス
    private readonly FormMain main;
    public FileClass File;
    public DirectionClass Dir;
    public DifSimClass DifSim;

    public Macro(FormMain _main) : base(_main, "ReciPro")
    {
        main = _main;
        File = new FileClass(this);
        Dir = new DirectionClass(this);
        DifSim = new DifSimClass(this);
        help.Add("ReciPro.Sleep(int millisec) # Sleep.");
    }

    public static void Sleep(int millisec) => Thread.Sleep(millisec);
    public object[] Obj { get; set; }

    #endregion

    #region Dirクラス
    public class DirectionClass : MacroSub
    {
        private readonly Macro p;
        public DirectionClass(Macro _p) : base(_p.main)
        {
            this.p = _p;
            p.help.Add("ReciPro.Dir.Euler(double phi, double theta, double psi) # Sets the rotation state by Euler angles.");
            p.help.Add("ReciPro.Dir.EulerInDegree(double phi, double theta, double psi) # Sets the rotation state by Euler angles (in degree).");
            p.help.Add("ReciPro.Dir.Rotate(double vX, double vY, double vZ, double angle) # Rotate the current crystal by specifying the rotation axis and angle.");
            p.help.Add("ReciPro.Dir.RotateAroundAxis(int u, int v, int w, double angle) # Rotate the current crystal with the crystal axis (uvw) as the rotation axis");
            p.help.Add("ReciPro.Dir.RotateAroundPlane(int h, int k, int l, double angle) # Rotate the current crystal with the crystal plane (hkl) as the rotation axis");
        }

        public void Euler(double phi, double theta, double psi)
        {
            p.main.SetRotation(phi, theta, psi);
            Application.DoEvents();
        }

        public void EulerInDegree(double phi, double theta, double psi)
        {
            p.main.SetRotation(phi / 180.0 * Math.PI, theta / 180.0 * Math.PI, psi / 180.0 * Math.PI);
            Application.DoEvents();
        }

        public void Rotate(double vX, double vY, double vZ, double angle) => p.main.Rotate((vX, vY, vZ), angle);

        public void RotateAroundAxis(int u, int v, int w, double angle)
        {
            Vector3DBase a = p.main.Crystal.A_Axis, b = p.main.Crystal.B_Axis, c = p.main.Crystal.C_Axis;
            var axis = p.main.Crystal.RotationMatrix * (u * a + v * b + w * c);
            p.main.Rotate(axis, angle);
        }

        public void RotateAroundPlane(int h, int k, int l, double angle)
        {
            var rot = p.main.Crystal.MatrixInverse;
            var axis = p.main.Crystal.RotationMatrix * (h * rot.Row1 + k * rot.Row2 + l * rot.Row3);
            p.main.Rotate(axis, angle);
        }

        public void SetZoneAxis(int h, int k, int l)
        {

        }


    }
    #endregion

    #region DiffractionSimulatorクラス
    public class DifSimClass : MacroSub
    {
        private readonly Macro p;

        private FormDiffractionSimulator difSim => p.main.FormDiffractionSimulator;
        private Crystal c => p.main.Crystal;

        public DifSimClass(Macro _p) : base(_p.main)
        {
            this.p = _p;

            p.help.Add("ReciPro.DifSim.Source_Xray() # Set/Get the sample thickness.");
            p.help.Add("ReciPro.DifSim.Source_Electron() # Set/Get the sample thickness.");
            p.help.Add("ReciPro.DifSim.Source_Neutron() # Set/Get the sample thickness.");
            p.help.Add("ReciPro.DifSim.Energy # Double. Set/Get the energy of incident beam. The units for X-ray and electron are keV, and for neutron are meV");
            p.help.Add("ReciPro.DifSim.Wavelength # Double. Set/Get the wavelength of incident beam in nm.");

            p.help.Add("ReciPro.DifSim.Thickness # Double. Set/Get the sample thickness.");
            p.help.Add("ReciPro.DifSim.NumberOfDiffractedWaves # Integer. Set or get the number of diffracted waves used in the dynamic calculation.");

            p.help.Add("ReciPro.DifSim.Beam_Parallel() # Set the incident beam parallel.");
            p.help.Add("ReciPro.DifSim.Beame_PrecessionXray() # Set the incident X-ray beam precessing.");
            p.help.Add("ReciPro.DifSim.Beam_PrecessionElectron() # Set the incident electron beam precessing.");
            p.help.Add("ReciPro.DifSim.Beam_Convergence() # Set the incident electron beam converging.");

            p.help.Add("ReciPro.DifSim.Calc_Excitation() # Calculate the spot intensities with excitation error only.");
            p.help.Add("ReciPro.DifSim.Calc_Kinematical() # Calculate the spot intensities using the excitation error and the structure factor.");
            p.help.Add("ReciPro.DifSim.Calc_Dynamical() # Calculate the spot intensities  by the dynamical theory.");

            p.help.Add("ReciPro.DifSim.ImageWidth # Integer. Set/Get the image width in pixel.");
            p.help.Add("ReciPro.DifSim.ImageHeight # Integer. Set/Get the image height in pixel.");
            p.help.Add("ReciPro.DifSim.ImageResolutionInMM # Double. Set/Get the image resolution (mm/pix).");
            p.help.Add("ReciPro.DifSim.ImageResolutionInNMinv # Double. Set/Get the image resolution (nm^-1/pix).");
            p.help.Add("ReciPro.DifSim.CameraLength2 # Double. Set/Get the distance from the sample to the detector.");
            p.help.Add("ReciPro.DifSim.Foot(double x, double y) # Set coordinates of the foot of the perpendicular line from the sample to the detector.");

            p.help.Add("ReciPro.DifSim.SkipRendering # True/False. Set/get whether screen rendering is skipped or not.");

            p.help.Add("ReciPro.DifSim.SpotInfo() # Get spot information in CSV format.");

            p.help.Add("ReciPro.DifSim.SaveAsPng(string filename) # Save the current simulation pattern as png format file. If filename is omitted, a dialog will open.");

        }

        public void Open() => Execute(new Action(() => difSim.Visible = true));
        public void Close() => Execute(new Action(() => difSim.Visible = false));

        public void SaveAsPng(string filename = "") => difSim.SaveOrCopy(true, true, true, filename);

        public void Source_Xray() { difSim.Source = WaveSource.Xray; }
        public void Source_Electron() { difSim.Source = WaveSource.Electron; }
        public void Source_Neutron() { difSim.Source = WaveSource.Neutron; }

        public double Energy { get => difSim.Energy; set => difSim.Energy = value; }
        public double Wavelength { get => difSim.WaveLength; set => difSim.WaveLength = value; }
        public double Thickness { get => difSim.Thickness; set => difSim.Thickness = value; }
        public int NumberOfDiffractedWaves { get => difSim.NumberOfDiffractedWaves; set => difSim.NumberOfDiffractedWaves = value; }

        public void Beam_Parallel() { difSim.BeamMode = FormDiffractionSimulator.BeamModes.Parallel; }
        public void Beame_PrecessionXray() { difSim.BeamMode = FormDiffractionSimulator.BeamModes.PrecessionXray; }
        public void Beam_PrecessionElectron() { difSim.BeamMode = FormDiffractionSimulator.BeamModes.PrecessionElectron; }
        public void Beam_Convergence() { difSim.BeamMode = FormDiffractionSimulator.BeamModes.Convergence; }

        public void Calc_Excitation() { difSim.CalcMode = FormDiffractionSimulator.CalcModes.Excitation; }
        public void Calc_Kinematical() { difSim.CalcMode = FormDiffractionSimulator.CalcModes.Kinematical; }
        public void Calc_Dynamical() { difSim.CalcMode = FormDiffractionSimulator.CalcModes.Dynamical; }

        public double ImageResolutionInMM
        {
            get 
            {
                difSim.ResolutionUnit = LengthUnitEnum.MilliMeter;
                return difSim.Resolution; 
            }
            set
            {
                difSim.ResolutionUnit = LengthUnitEnum.MilliMeter;
                difSim.Resolution = value;
            }
        }
        public double ImageResolutionInNMinv
        {
            get
            {
                difSim.ResolutionUnit = LengthUnitEnum.NanoMeterInverse; 
                return difSim.ResolutionInNMinv;
            }
            set
            {
                difSim.ResolutionUnit = LengthUnitEnum.NanoMeterInverse;
                difSim.ResolutionInNMinv = value;
            }
        }
        public int ImageWidth { get => difSim.ClientWidth; set => difSim.ClientWidth = value; }
        public int ImageHeight { get => difSim.ClientHeight; set => difSim.ClientHeight = value; }
        public double CameraLength2 { get => difSim.CameraLength2; set => difSim.CameraLength2 = value; }
        public void Foot(double x, double y) { difSim.Foot = new PointD(x, y); }

        public bool SkipRendering { get => difSim.SkipRendering; set => difSim.SkipRendering = value; }

        public string SpotInfo() => (Execute(() => spotInfo()));
        public string spotInfo()
        {
            var gamma = 1 + UniversalConstants.e0 * Energy * 1000 / UniversalConstants.m0 / UniversalConstants.c2;
            double coeff;
            var sb = new StringBuilder();
            if (difSim.CalcMode == FormDiffractionSimulator.CalcModes.Dynamical)
            {
                if (difSim.FormDiffractionBeamTable.UnitOfPotential == FormDiffractionSpotInfo.UnitOfPotentialEnum.Vg)
                {
                    sb.Append("No., R, H, K, L, d, gX, gY, gZ,|g|=1/d, Vg_re, Vg_im, V'g_re, V'g_im, Sg, Pg, Qg, φ_re, φ_im, |φ|^2\n");
                    coeff = 1 / gamma * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733;
                }
                else
                {
                    sb.Append("No., R, H, K, L, d, gX, gY, gZ,|g|=1/d, Ug_re, Ug_im, U'g_re, U'g_im, Sg, Pg, Qg, φ_re, φ_im, |φ|^2\n");
                    coeff = 1 / gamma;
                }

                int n = 0;
                foreach (var b in c.Bethe.Beams)
                {
                    var g = b.Vec.Length;
                    sb.Append((n++) + "," + b.Rating + "," + b.H + "," + b.K + "," + b.L + "," + (1 / g) + ",");
                    sb.Append(b.Vec.X + "," + b.Vec.Y + "," + b.Vec.Z + "," + g + ",");
                    sb.Append((b.Ureal.Real * coeff) + "," + (b.Ureal.Imaginary * coeff) + "," + (b.Uimag.Real * coeff) + "," + (b.Uimag.Imaginary * coeff) + ",");
                    sb.Append(b.S + "," + b.P + "," + b.Q + ",");
                    sb.Append(b.Psi.Real + "," + b.Psi.Imaginary + "," + b.Psi.MagnitudeSquared());
                    sb.Append("\n");
                }
                return sb.ToString();
            }
            else
            {

            }
            return "";
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
    }
    #endregion

}

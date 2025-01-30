#region using 
using MathNet.Numerics;
using MemoryPack.Formatters;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static IronPython.Modules._ast;

namespace ReciPro;
#endregion

public class Macro : MacroBase
{
    #region 基底クラス
    private readonly FormMain main;
    public FileClass File;
    public DirectionClass Dir;
    public DifSimClass DifSim;
    public CrystalListClass CrystalList;
    public STEMClass STEM;
    public HRTEMClass HRTEM;
    public PotentialClass Potential;

    public Macro(FormMain _main) : base(_main, "ReciPro")
    {
        main = _main;
        File = new FileClass(this);
        Dir = new DirectionClass(this);
        DifSim = new DifSimClass(this);
        CrystalList = new CrystalListClass(this);
        STEM = new STEMClass(this);
        HRTEM = new HRTEMClass(this);
        Potential = new PotentialClass(this);

        help.Add("ReciPro.Sleep(int millisecond) # Sleep.");
    }

    public static void Sleep(int millisec) => Thread.Sleep(millisec);
    //public object[] Obj { get; set; }

    #endregion


    #region ファイルクラス
    public class FileClass : MacroSub
    {
        private readonly Macro p;
        public FileClass(Macro _p) : base(_p.main)
        {
            this.p = _p;
            p.help.Add("ReciPro.File.GetFileName() # Get a file name by a selection dialog. The returned string is a full path of the selected file.");
            p.help.Add("ReciPro.File.GetFileNames() # Get file names by a selection dialog. Multiple selections are possible. The returned value is a string array, each of which is a full path of selected files.");
            p.help.Add("ReciPro.File.GetDirectoryPath(string filename) # Get a directory path. the returned string is a full path to the filename. If 'filename' is omitted, a selection dialog will open.");
            p.help.Add("ReciPro.File.ReadCrystal(string filename) # Read a crystal (CIF- or AMC-format only).");
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
            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileNames : [];
        }

        public void ReadCrystal(string filename) => Execute(() => p.main.ReadCrystal(filename));

        public void ReadCrystalList(string filename) => Execute(() => p.main.ReadCrystalList(filename, false, false));

    }
    #endregion

    #region CrystalList クラス
    public class CrystalListClass : MacroSub
    {
        private readonly Macro p;
        public CrystalListClass(Macro _p) : base(_p.main)
        {
            p = _p;
            p.help.Add("ReciPro.CrystalList.SelectedIndex  # Integer. Set/get the index (integer value) of the selected crystal in the list.");
            p.help.Add("ReciPro.CrystalList.Add()  # Add the crystal at 'Crystal Information' to the end of the list.");
            p.help.Add("ReciPro.CrystalList.Replace()  # Replace the crystal at 'Crystal Information' with the crystal selected in the list.");
            p.help.Add("ReciPro.CrystalList.Delete()  # Delete the selected crystal in the list.");
            p.help.Add("ReciPro.CrystalList.ClearAll()  # Delete all crystals in the list.");
            p.help.Add("ReciPro.CrystalList.MoveUp()  # Move up the selected crystal in the list.");
            p.help.Add("ReciPro.CrystalList.MoveDown()  # Move down the selected crystal in the list.");
        }

        public int SelectedIndex { get => p.main.SelectedCrystalIndex; set => p.main.SelectedCrystalIndex = value; }

        public void Add() => Execute(() => p.main.AddCrystal());
        public void Replace() => Execute(() => p.main.ReplaceCrystal());
        public void Delete() => Execute(() => p.main.DeleteCrystal());
        public void ClearAll() => Execute(() => p.main.CrystalListClear());
        public void MoveUp() => Execute(() => p.main.MoveUp());
        public void MoveDown() => Execute(() => p.main.MoveDown());
    }
    #endregion

    #region Dir (Direction 方位)クラス
    public class DirectionClass : MacroSub
    {
        private readonly Macro p;
        public DirectionClass(Macro _p) : base(_p.main)
        {
            this.p = _p;
            p.help.Add("ReciPro.Dir.Euler(double phi, double theta, double psi) # Set the rotation state by three Euler angles (in radians)");
            p.help.Add("ReciPro.Dir.EulerInDeg(double phi, double theta, double psi) # Set the rotation state by three Euler angles (in degrees).");
            p.help.Add("ReciPro.Dir.Rotate(double vX, double vY, double vZ, double angle) # Rotate the current crystal by specifying the rotation axis (vX, vY, vZ) and angle (in radians).");
            p.help.Add("ReciPro.Dir.RotateInDeg(double vX, double vY, double vZ, double angle) # Rotate the current crystal by specifying the rotation axis (vX, vY, vZ) and angle  (in degrees).");
            p.help.Add("ReciPro.Dir.RotateAroundAxis(int u, int v, int w, double angle) # Rotate the current crystal with the crystal axis (uvw) as the rotation axis (in radians)");
            p.help.Add("ReciPro.Dir.RotateAroundAxisInDeg(int u, int v, int w, double angle) # Rotate the current crystal with the crystal axis (uvw) as the rotation axis  (in degrees)");
            p.help.Add("ReciPro.Dir.RotateAroundPlane(int h, int k, int l, double angle) # Rotate the current crystal with the crystal plane (hkl) as the rotation axis (in radians)");
            p.help.Add("ReciPro.Dir.RotateAroundPlaneInDeg(int h, int k, int l, double angle) # Rotate the current crystal with the crystal plane (hkl) as the rotation axis  (in degrees)");
            p.help.Add("ReciPro.Dir.ProjectAlongAxis(int u, int v, int w) # Rotate the current crystal so that the specified axis (uvw) is normal to the screen.");
            p.help.Add("ReciPro.Dir.ProjectAlongPlane(int h, int k, int l) # Rotate the current crystal so that the specified plane (hkl) is normal to the screen.");
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
        public void RotateInDeg(double vX, double vY, double vZ, double angle) => Rotate(vX, vY, vZ, angle*Math.PI/180.0);

        public void RotateAroundAxis(int u, int v, int w, double angle)
        {
            Vector3DBase a = p.main.Crystal.A_Axis, b = p.main.Crystal.B_Axis, c = p.main.Crystal.C_Axis;
            var axis = p.main.Crystal.RotationMatrix * (u * a + v * b + w * c);
            p.main.Rotate(axis, angle);
        }
        public void RotateAroundAxisInDeg(int u, int v, int w, double angle)=> RotateAroundAxis(u, v, w, angle * Math.PI / 180.0);

        public void RotateAroundPlane(int h, int k, int l, double angle)
        {
            var rot = p.main.Crystal.MatrixInverse;
            var axis = p.main.Crystal.RotationMatrix * (h * rot.Row1 + k * rot.Row2 + l * rot.Row3);
            p.main.Rotate(axis, angle);
        }
        public void RotateAroundPlaneInDeg(int h, int k, int l, double angle)=>RotateAroundPlane(h, k, l, angle * Math.PI / 180.0);

        public void ProjectAlongPlane(int h, int k, int l)
        {
            p.main.SetPlane(h, k, l);
            p.main.ProjectAlongPlane();
            Application.DoEvents();
        }

        public void ProjectAlongAxis(int u, int v, int w)
        {
            p.main.SetAxis(u, v, w);
            p.main.ProjectAlongAxis();
            Application.DoEvents();
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

            p.help.Add("ReciPro.DifSim.Open() # Open the 'Diffraction Simulator' window.");
            p.help.Add("ReciPro.DifSim.Close() # Close the 'Diffraction Simulator' window.");

            p.help.Add("ReciPro.DifSim.Source_Xray() # Set the incident wave to X-ray.");
            p.help.Add("ReciPro.DifSim.Source_Electron() # Set the incident wave to electron.");
            p.help.Add("ReciPro.DifSim.Source_Neutron() # Set the incident wave to neutron.");
            p.help.Add("ReciPro.DifSim.Energy # Double. Float. Set/Get the energy of incident beam. The units for X-ray and electron are keV, and for neutron are meV");
            p.help.Add("ReciPro.DifSim.Wavelength # Float. Set/Get the wavelength of incident beam in nm.");

            p.help.Add("ReciPro.DifSim.Thickness # Float. Set/Get the sample thickness.");
            p.help.Add("ReciPro.DifSim.NumberOfDiffractedWaves # Integer. Set or get the number of diffracted waves used in the dynamic calculation.");

            p.help.Add("ReciPro.DifSim.Beam_Parallel() # Set the incident wave to a parallel beam.");
            p.help.Add("ReciPro.DifSim.Beam_PrecessionXray() # Set the incident X-ray to a precession beam.");
            p.help.Add("ReciPro.DifSim.Beam_PrecessionElectron() # Set the incident electron to a precession beam.");
            p.help.Add("ReciPro.DifSim.Beam_Convergence() # Set the incident electron to a convergent beam.");

            p.help.Add("ReciPro.DifSim.Calc_Excitation() # Calculate the intensities with excitation error only.");
            p.help.Add("ReciPro.DifSim.Calc_Kinematical() # Calculate the intensities using the excitation error and the structure factor.");
            p.help.Add("ReciPro.DifSim.Calc_Dynamical() # Calculate the intensities by the dynamical theory.");

            p.help.Add("ReciPro.DifSim.ImageWidth # Integer. Set/Get the image width in pixel.");
            p.help.Add("ReciPro.DifSim.ImageHeight # Integer. Set/Get the image height in pixel.");
            p.help.Add("ReciPro.DifSim.ImageSize(int width, int height) # Set the image size in pixel.");
            p.help.Add("ReciPro.DifSim.ImageResolutionInMM # Float. Set/Get the image resolution (mm/pix).");
            p.help.Add("ReciPro.DifSim.ImageResolutionInNMinv # Float. Set/Get the image resolution (nm^-1/pix).");
            p.help.Add("ReciPro.DifSim.CameraLength2 # Float. Set/Get the distance (in mm) from the sample to the detector.");
            p.help.Add("ReciPro.DifSim.Foot(double x, double y) # Set coordinates (in mm) of the foot of the perpendicular line from the sample to the detector.");

            p.help.Add("ReciPro.DifSim.SkipRendering # True/False. Set/get whether screen rendering is skipped or not.");

            p.help.Add("ReciPro.DifSim.SpotInfo() # Get spot information as CSV-format text.");

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
        public void Beam_PrecessionXray() { difSim.BeamMode = FormDiffractionSimulator.BeamModes.PrecessionXray; }
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

        public void ImageSize(int width, int height) { ImageWidth = width; ImageHeight = height; }
        public double CameraLength2 { get => difSim.CameraLength2; set => difSim.CameraLength2 = value; }
        public void Foot(double x, double y) { difSim.Foot = new PointD(x, y); }

        public bool SkipRendering { get => difSim.SkipRendering; set => difSim.SkipRendering = value; }

        public string SpotInfo() => (Execute(() => spotInfo()));
        private string spotInfo()
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
                    sb.Append(value: $"{n++},{b.Rating},{b.H},{b.K},{b.L},{1 / g},");
                    sb.Append($"{b.Vec.X},{b.Vec.Y},{b.Vec.Z},{g},");
                    sb.Append($"{b.Ureal.Real * coeff},{b.Ureal.Imaginary * coeff},{b.Uimag.Real * coeff},{b.Uimag.Imaginary * coeff},");
                    sb.Append($"{b.S},{b.P},{b.Q},");
                    sb.Append($"{b.Psi.Real},{b.Psi.Imaginary},{b.Psi.MagnitudeSquared()}");
                    sb.Append('\n');
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

    #region ImageSimulatorクラス (派生クラスとしてHRTEM、STEM、Potential)

    public abstract class ImageSimulationClass : MacroSub
    {
        internal readonly Macro p;
        internal FormImageSimulator sim => p.main.FormImageSimulator;
        private readonly FormImageSimulator.ImageModes Mode;
        public ImageSimulationClass(Macro _p, FormImageSimulator.ImageModes mode) : base(_p.main)
        {
            p = _p;
            Mode = mode;
            var modeStr = Mode switch { FormImageSimulator.ImageModes.STEM => "STEM", FormImageSimulator.ImageModes.HRTEM => "HRTEM", _ => "Potential" };
            #region 全共通
            p.help.Add($"ReciPro.{modeStr}.Open() # Open the {modeStr} simulator window.");
            p.help.Add($"ReciPro.{modeStr}.Close() # Close the  {modeStr} simulator window.");
            p.help.Add($"ReciPro.{modeStr}.Simulate() # Simulate {modeStr} image(s) with the current settings.");

            p.help.Add($"ReciPro.{modeStr}.AccVol # Float. Set/get the accelerating voltage of electron (in kV).");
            p.help.Add($"ReciPro.{modeStr}.NumberOfDiffractedWaves  # Integer.Set/get the maximum number of diffracted waves (Bloch waves) used in the dynamical scattering theory.");

            p.help.Add($"ReciPro.{modeStr}.ImageWidth # Integer. Set/get the width of the image to be simulated (in pixel).");
            p.help.Add($"ReciPro.{modeStr}.ImageHeight # Integer. Set/get the height of the image to be simulated (in pixel).");
            p.help.Add($"ReciPro.{modeStr}.ImageSize(in width, int height) # Set the image size to be simulated (in pixel).");

            p.help.Add($"ReciPro.{modeStr}.ImageResolution # Float. Set/get the resolution of the image to be simulated (in picometer/pixel).");
            p.help.Add($"ReciPro.{modeStr}.UnitCellVisible # True/False. Set/get whether or not to display a unit cell.");
            p.help.Add($"ReciPro.{modeStr}.LabelVisible # True/False. Set/get whether or not to display a image label.");
            p.help.Add($"ReciPro.{modeStr}.LabelSize # Integer. Set/get the size of labels ");
            p.help.Add($"ReciPro.{modeStr}.ScaleBarVisible # True/False. Whether or not to display a scale bar.");
            p.help.Add($"ReciPro.{modeStr}.ScaleBarLength # Float. Set/get the length of the scalebar (in nm).");
            p.help.Add($"ReciPro.{modeStr}.GaussianBlurEnabled # True/False. Set/get whether or not to process Gaussian blur.");
            p.help.Add($"ReciPro.{modeStr}.GaussianBlurFWHM # Float. Set/get the FWHM value (in pm) of Gaussian blur.");
            p.help.Add($"ReciPro.{modeStr}.OverprintSymbols # True/False. On the saved image, whether or not to overprint the unit cell, labels and scale bar.");
            p.help.Add($"ReciPro.{modeStr}.SaveIndividually # True/False. When simulating in serial image mode, whether or not to save each image individually.");

            p.help.Add($"ReciPro.{modeStr}.SaveImageAsPng(string filename) # Save the simulated image as a PNG format. If 'filename' is omitted, a dialog box will appear.");
            p.help.Add($"ReciPro.{modeStr}.SaveImageAsTif(string filename) # Save the simulated image as a TIFF format. If the 'filename' is omitted, a dialog box will appear.");
            p.help.Add($"ReciPro.{modeStr}.SaveImageAsEmf(string filename) # Save the simulated image as a Metafile (EMF) format. If 'filename' is omitted, a dialog box will appear.");

            if (Mode == FormImageSimulator.ImageModes.POTENTIAL) return; //Potentialだった場合はここで終了
            #endregion

            #region HRTEM/STEM共通
            p.help.Add($"ReciPro.{modeStr}.Thickness  # Float. Set/get the sample thickness (in nm).");
            p.help.Add($"ReciPro.{modeStr}.Defocus  # Float. Set/get the Defocus value (in nm).");
            p.help.Add($"ReciPro.{modeStr}.Cs # Float. Set/get the Cs (spherical aberration) value (in mm).");
            p.help.Add($"ReciPro.{modeStr}.Cc # Float. Set/get the Cc (chromatic aberration) value (in mm).");
            p.help.Add($"ReciPro.{modeStr}.DeltaV  # Float. Set/get the ΔV (1/e width of electron energy fluctuations) value (in eV).");
            p.help.Add($"ReciPro.{modeStr}.Scherzer  # Float. Get the Scherzer defocus value (in nm).");

            p.help.Add($"ReciPro.{modeStr}.SingleImageMode()  # Set to single image mode.");

            p.help.Add($"ReciPro.{modeStr}.SerialImageMode(bool withThickness, bool withDefocus)  # Set to serial image mode with/without thickness and with/without defocus.");
            p.help.Add($"ReciPro.{modeStr}.SerialImageThicknessStart # Float. Set/get the starting thickness (in nm) in serial images.");
            p.help.Add($"ReciPro.{modeStr}.SerialImageThicknessStep # Float. Set/get the thickness step (in nm) in serial images.");
            p.help.Add($"ReciPro.{modeStr}.SerialImageThicknessNum # Integer. Set/get the number of the thicknesses in serial images.");
            p.help.Add($"ReciPro.{modeStr}.SerialImageDefocusStart # Float. Set/get the starting defocus (in nm) in serial images.");
            p.help.Add($"ReciPro.{modeStr}.SerialImageDefocusStep # Float. Set/get the defocus step (in nm) in serial images.");
            p.help.Add($"ReciPro.{modeStr}.SerialImageDefocusNum # Integer. Set/get the number of the defocuses in serial images.");

            #endregion
        }

        #region 全共通
        public double AccVol { get => sim.AccVol; set => sim.AccVol = value; }
        public int NumberOfDiffractedWaves { get => sim.BlochNum; set => sim.BlochNum = value; }
        public int ImageWidth { get => sim.ImageSize.Width; set => sim.ImageSize = new Size(value, sim.ImageSize.Height); }
        public int ImageHeight { get => sim.ImageSize.Height; set => sim.ImageSize = new Size(sim.ImageSize.Width, value); }
        public void ImageSize(int width, int height) => sim.ImageSize = new Size(width, height);

        public double ImageResolution { get => sim.ImageResolution; set => sim.ImageResolution = value; }
        public bool UnitCellVisible { get => sim.UnitCellVisible; set => sim.UnitCellVisible = value; }
        public bool LabelVisible { get => sim.LabelVisible; set => sim.LabelVisible = value; }
        public int LabelSize { get => sim.LabelSize; set => sim.LabelSize = value; }
        public bool ScaleBarVisible { get => sim.ScaleBarVisible; set => sim.ScaleBarVisible = value; }
        public double ScaleBarLength { get => sim.ScaleBarLength; set => sim.ScaleBarLength = value; }
        public bool GaussianBlurEnabled { get => sim.GaussianBlurEnabled; set => sim.GaussianBlurEnabled = value; }
        public double GaussianBlurFWHM { get => sim.GaussianBlurFWHM; set => sim.GaussianBlurFWHM = value; }

        public void Open() { sim.Visible = true; sim.ImageMode = Mode; }
        public void Close() => sim.Visible = false;
        public void Simulate()
        {
            Open();
            sim.Simulate(true);
        }

        public bool OverprintSymbols { get => sim.OverprintSymbols; set => sim.OverprintSymbols = value; }
        public bool SaveIndividually { get => sim.SaveIndividually; set => sim.SaveIndividually = value; }
        public void SaveImageAsPng(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.PNG, FormImageSimulator.ActionEnum.Save, filename);
        public void SaveImageAsTif(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.TIFF, FormImageSimulator.ActionEnum.Save, filename);
        public void SaveImageAsEmf(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.Meta, FormImageSimulator.ActionEnum.Save, filename);

        #endregion

        #region HRTEM/STEM共通
        public double Thickness { get => sim.Thickness; set => sim.Thickness = value; }
        public double Defocus { get => sim.Defocus; set => sim.Defocus = value; }
        public double Cs { get => sim.Cs * 1E-6; set => sim.Cs = value * 1E6; }
        public double Cc { get => sim.Cc * 1E-6; set => sim.Cc = value * 1E6; }
        public double DeltaV { get => sim.DeltaVolFWHM * 1000.0; set => sim.DeltaVolFWHM = value / 1000.0; }
        public double Scherzer => sim.Scherzer;

        public void SingleImageMode() => sim.SingleImageMode = true;
        public void SerialImageMode(bool withThickness, bool withDefocus)
        {
            sim.SerialImageMode = true;
            sim.SerialImageWithThickness = withThickness;
            sim.SerialImageWithDefocus = withDefocus;
        }
        public double SerialImageThicknessStart { get => sim.SerialImageThicknessStart; set => sim.SerialImageThicknessStart = value; }
        public double SerialImageThicknessStep { get => sim.SerialImageThicknessStep; set => sim.SerialImageThicknessStep = value; }
        public int SerialImageThicknessNum { get => sim.SerialImageThicknessNum; set => sim.SerialImageThicknessNum = value; }
        public double SerialImageDefocusStart { get => sim.SerialImageDefocusStart; set => sim.SerialImageDefocusStart = value; }
        public double SerialImageDefocusStep { get => sim.SerialImageDefocusStep; set => sim.SerialImageDefocusStep = value; }
        public int SerialImageDefocusNum { get => sim.SerialImageDefocusNum; set => sim.SerialImageDefocusStep = value; }
        #endregion
    }

    public class STEMClass : ImageSimulationClass
    {
        public STEMClass(Macro _p) : base(_p, FormImageSimulator.ImageModes.STEM)
        {
            p.help.Add($"ReciPro.STEM.ConvergenceAngle # Float. Set/get the convergence semiangle (in mrad).");
            p.help.Add($"ReciPro.STEM.DetectorInnerAngle # Float. Set/get the inner semiangle (in mrad) of the annular detector.");
            p.help.Add($"ReciPro.STEM.DetectorOuterAngle # Float. Set/get the outer semiangle (in mrad) of the annular detector.");
            p.help.Add($"ReciPro.STEM.EffectiveSourceSize # Float. Set/get the effective source size (FWHM in pim).");

            p.help.Add($"ReciPro.STEM.AngularResolution  # Float. Set/get the angular resolution (in mrad) of the convergent beam.");
            p.help.Add($"ReciPro.STEM.SliceThickness  # Float. Set/get the slice thickness (in nm) for TDS calculation.");
            
            p.help.Add($"ReciPro.STEM.DisplayBoth() # Display both elastic and TDS (inelastic) electrons.");
            p.help.Add($"ReciPro.STEM.DisplayElastic() # Display elastic electrons only.");
            p.help.Add($"ReciPro.STEM.DisplayTDS() # Display TDS (inelastic) electrons only.");

        }

        public double ConvergenceAngle { get => sim.STEM_ConvergenceAngle * 1000; set => sim.STEM_ConvergenceAngle = value / 1000; }
        public double DetectorInnerAngle { get => sim.STEM_DetectorInnerAngle * 1000; set => sim.STEM_DetectorInnerAngle = value / 1000; }
        public double DetectorOuterAngle { get => sim.STEM_DetectorOuterAngle * 1000; set => sim.STEM_DetectorOuterAngle = value / 1000; }
        public double EffectiveSourceSize { get => sim.STEM_SourceSizeFWHM * 1000; set => sim.STEM_SourceSizeFWHM = value / 1000; }
       
        public double AngularResolution { get => sim.STEM_AngularResolution * 1000; set => sim.STEM_AngularResolution = value / 1000; }
        public double SliceThickness { get => sim.STEM_SliceThickness; set => sim.STEM_SliceThickness = value; }

        public void DisplayBoth() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.Both;
        public void DisplayElastic() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.Elastic;
        public void DisplayTDS() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.TDS;
    }

    public class HRTEMClass : ImageSimulationClass
    {
        public HRTEMClass(Macro _p) : base(_p, FormImageSimulator.ImageModes.HRTEM)
        {
            p.help.Add($"ReciPro.HRTEM.Beta # Float. Set/get illumination semiangle β (in mrad) of the electron beam due to the finite source size effect.");
            p.help.Add($"ReciPro.HRTEM.ApertureSemiangle # Float. Set/get the semiangle (in mrad) of the objective aperture.");
            p.help.Add($"ReciPro.ApertureShiftX # Float. Set/get the x-direction shift (in mrad) of the objective aperture.");
            p.help.Add($"ReciPro.ApertureShiftY # Float. Set/get the y-direction shift (in mrad) of the objective aperture.");
            p.help.Add($"ReciPro.OpenAperture # True/False. Set/get the open aperture state.");
            
            p.help.Add($"ReciPro.Mode_LinearImage() # Use linear image model for calculating partial coherency.");
            p.help.Add($"ReciPro.Mode_TCC() # Use TCC (transmission cross coefficient) model for calculating partial coherency.");
        }
        public double Beta { get => sim.HRTEM_Beta; set => sim.HRTEM_Beta = value; }
        public double ApertureSemiangle { get => sim.HRTEM_ObjAperRadius; set => sim.HRTEM_ObjAperRadius = value; }
        public double ApertureShiftX { get => sim.HRTEM_ObjAperX; set => sim.HRTEM_ObjAperX = value; }
        public double ApertureShiftY { get => sim.HRTEM_ObjAperY; set => sim.HRTEM_ObjAperY = value; }
        public bool OpenAperture { get => sim.HRTEM_OpenObjAper; set => sim.HRTEM_OpenObjAper = value; }

        public void Mode_LinearImage() => sim.HRTEM_Mode = FormImageSimulator.HRTEM_Modes.Quasi;
        public void Mode_TCC() => sim.HRTEM_Mode = FormImageSimulator.HRTEM_Modes.TCC;


    }

    public class PotentialClass : ImageSimulationClass
    {
        public PotentialClass(Macro _p) : base(_p, FormImageSimulator.ImageModes.POTENTIAL)
        {
        }



    }

    #endregion

}

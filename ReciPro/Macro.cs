#region using 
using Crystallography;
using MathNet.Numerics;
using MemoryPack.Formatters;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static Community.CsharpSqlite.Sqlite3;
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
        HelpAttribute.GenerateHelpText(File.GetType(), nameof(File)).ForEach(s => help.Add(s));

        Dir = new DirectionClass(this);
        HelpAttribute.GenerateHelpText(Dir.GetType(), nameof(Dir)).ForEach(s => help.Add(s));

        DifSim = new DifSimClass(this);
        HelpAttribute.GenerateHelpText(DifSim.GetType(), nameof(DifSim)).ForEach(s => help.Add(s));

        CrystalList = new CrystalListClass(this);
        HelpAttribute.GenerateHelpText(CrystalList.GetType(), nameof(CrystalList)).ForEach(s => help.Add(s));

        STEM = new STEMClass(this);
        HelpAttribute.GenerateHelpText(STEM.GetType(), nameof(STEM)).ForEach(s => help.Add(s.Replace("###", nameof(STEM))));

        HRTEM = new HRTEMClass(this);
        HelpAttribute.GenerateHelpText(HRTEM.GetType(), nameof(HRTEM)).ForEach(s => help.Add(s.Replace("###", nameof(HRTEM))));

        Potential = new PotentialClass(this);
        HelpAttribute.GenerateHelpText(Potential.GetType(), nameof(Potential)).ForEach(s => help.Add(s.Replace("###", nameof(Potential))));

    }

    [Help("Sleep in millisecond.","int millisecond")]
    public static void Sleep(int millisec) => Thread.Sleep(millisec);

    #endregion

    #region ファイルクラス
    public class FileClass(Macro _p) : MacroSub(_p.main)
    {
        //private readonly Macro p = _p;

        [Help("Get a directory path. the returned string is a full path to the filename. If 'filename' is omitted, a selection dialog will open.", "string filename")]
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

        [Help("Get a file name by a selection dialog. The returned string is a full path of the selected file.")]
        public string GetFileName() => Execute(() => getFileName());
        private static string getFileName()
        {
            var dlg = new OpenFileDialog();
            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileName : "";
        }

        [Help("Get file names by a selection dialog. Multiple selections are possible. The returned value is a string array, each of which is a full path of selected files.")]
        public string[] GetFileNames() => Execute<string[]>(new Func<string[]>(() => getFileNames()));
        private static string[] getFileNames()
        {
            var dlg = new OpenFileDialog() { Multiselect = true };
            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileNames : [];
        }

        [Help("Read a crystal list file (xml format).", "string filename")]
        public void ReadCrystalList(string filename) => Execute(() => p.main.ReadCrystalList(filename, false, false));
      
        [Help("Read a crystal file (cif or amc format).", "string filename")]
        public void ReadCrystal(string filename) => Execute(() => p.main.ReadCrystal(filename));

    }
    #endregion

    #region CrystalList クラス
    public class CrystalListClass(Macro _p) : MacroSub(_p.main)
    {
        private FormMain main => _p.main;

        [Help("Integer. Set/get the index (integer value) of the selected crystal in the list.")]
        public int SelectedIndex { get => main.SelectedCrystalIndex; set => main.SelectedCrystalIndex = value; }

        [Help("Add the crystal at 'Crystal Information' to the end of the list.")]
        public void Add() => Execute(() => main.AddCrystal());

        [Help("Replace the crystal at 'Crystal Information' with the crystal selected in the list.")]
        public void Replace() => Execute(() => main.ReplaceCrystal());

        [Help("Delete the selected crystal in the list.")]
        public void Delete() => Execute(() => main.DeleteCrystal());

        [Help("Delete all crystals in the list.")]
        public void ClearAll() => Execute(() => main.CrystalListClear());

        [Help("Move up the selected crystal in the list.")]
        public void MoveUp() => Execute(() => main.MoveUp());

        [Help("Move down the selected crystal in the list.")]
        public void MoveDown() => Execute(() => main.MoveDown());
    }
    #endregion

    #region Dir (Direction 方位)クラス
    public class DirectionClass(Macro _p) : MacroSub(_p.main)
    {
        private FormMain main => _p.main;

        [Help("Set the rotation state by three Euler angles (in radians)", "double phi, double theta, double psi")]
        public void Euler(double phi, double theta, double psi)
        {
            main.SetRotation(phi, theta, psi);
            Application.DoEvents();
        }

        [Help("Set the rotation state by three Euler angles (in degrees).", "double phi, double theta, double psi")]
        public void EulerInDegree(double phi, double theta, double psi)
        {
            main.SetRotation(phi / 180.0 * Math.PI, theta / 180.0 * Math.PI, psi / 180.0 * Math.PI);
            Application.DoEvents();
        }

        [Help("Rotate the current crystal by specifying the rotation axis (vX, vY, vZ) and angle (in radians).", 
            "double vX, double vY, double vZ, double angle")]
        public void Rotate(double vX, double vY, double vZ, double angle) => p.main.Rotate((vX, vY, vZ), angle);

        [Help(" Rotate the current crystal by specifying the rotation axis (vX, vY, vZ) and angle (in degrees)."
            , "double vX, double vY, double vZ, double angle")]
        public void RotateInDeg(double vX, double vY, double vZ, double angle) => Rotate(vX, vY, vZ, angle*Math.PI/180.0);

        [Help("Rotate the current crystal with the crystal axis (uvw) as the rotation axis (in radians)", 
            "int u, int v, int w, double angle")]
        public void RotateAroundAxis(int u, int v, int w, double angle)
        {
            Vector3DBase a = main.Crystal.A_Axis, b = main.Crystal.B_Axis, c = main.Crystal.C_Axis;
            var axis = main.Crystal.RotationMatrix * (u * a + v * b + w * c);
            main.Rotate(axis, angle);
        }
        [Help("Rotate the current crystal with the crystal axis (uvw) as the rotation axis (in degrees)"
            , "int u, int v, int w, double angle")]
        public void RotateAroundAxisInDeg(int u, int v, int w, double angle)=> RotateAroundAxis(u, v, w, angle * Math.PI / 180.0);

        [Help("Rotate the current crystal with the crystal plane (hkl) as the rotation axis (in radians)", "int h, int k, int l, double angle")]
        
        public void RotateAroundPlane(int h, int k, int l, double angle)
        {
            var rot = main.Crystal.MatrixInverse;
            var axis = main.Crystal.RotationMatrix * (h * rot.Row1 + k * rot.Row2 + l * rot.Row3);
            main.Rotate(axis, angle);
        }

        [Help("Rotate the current crystal with the crystal plane (hkl) as the rotation axis  (in degrees)", "int h, int k, int l, double angle")]
        public void RotateAroundPlaneInDeg(int h, int k, int l, double angle)=>RotateAroundPlane(h, k, l, angle * Math.PI / 180.0);

        [Help("Rotate the current crystal so that the specified axis (uvw) is normal to the screen.", "int u, int v, int w")]
        public void ProjectAlongPlane(int h, int k, int l)
        {
            main.SetPlane(h, k, l);
            main.ProjectAlongPlane();
            Application.DoEvents();
        }

        [Help("Rotate the current crystal so that the specified plane (hkl) is normal to the screen.", "int h, int k, int l")]
        public void ProjectAlongAxis(int u, int v, int w)
        {
            main.SetAxis(u, v, w);
            main.ProjectAlongAxis();
            Application.DoEvents();
        }


    }
    #endregion

    #region DiffractionSimulatorクラス
    public class DifSimClass(Macro _p) : MacroSub(_p.main)
    {
        private FormDiffractionSimulator difSim => _p.main.FormDiffractionSimulator;
        private Crystal c => _p.main.Crystal;

        [Help("Open the 'Diffraction Simulator' window.")]
        public void Open() => Execute(new Action(() => difSim.Visible = true));
        
        [Help("Close the 'Diffraction Simulator' window.")] 
        public void Close() => Execute(new Action(() => difSim.Visible = false));

        public void SaveAsPng(string filename = "") => difSim.SaveOrCopy(true, true, true, filename);

        [Help("Set the incident wave to X-ray.")]
        public void Source_Xray() { difSim.Source = WaveSource.Xray; }
        [Help("Set the incident wave to electron.")]
        public void Source_Electron() { difSim.Source = WaveSource.Electron; }
        
        [Help("Set the incident wave to neutron.")]
        public void Source_Neutron() { difSim.Source = WaveSource.Neutron; }

        [Help("Float. Set/Get the energy of incident beam. The units for X-ray and electron are keV, and for neutron are meV")]
        public double Energy { get => difSim.Energy; set => difSim.Energy = value; }

        [Help("Float. Set/Get the wavelength of incident beam in nm.")] 
        public double Wavelength { get => difSim.WaveLength; set => difSim.WaveLength = value; }

        [Help("Float. Set/Get the sample thickness.")]
        public double Thickness { get => difSim.Thickness; set => difSim.Thickness = value; }

        [Help("Integer. Set or get the number of diffracted waves used in the dynamic calculation.")]
        public int NumberOfDiffractedWaves { get => difSim.NumberOfDiffractedWaves; set => difSim.NumberOfDiffractedWaves = value; }

        [Help("Set the incident wave to a parallel beam.")]
        public void Beam_Parallel() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.Parallel;

        [Help("Set the incident X-ray to a precession beam.")]
        public void Beam_PrecessionXray() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.PrecessionXray;

        [Help("Set the incident electron to a precession beam.")]
        public void Beam_PrecessionElectron() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.PrecessionElectron;

        [Help("Set the incident electron to a convergent beam.")]
        public void Beam_Convergence() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.Convergence;

        [Help("Calculate the intensities with excitation error only.")]
        public void Calc_Excitation() => difSim.CalcMode = FormDiffractionSimulator.CalcModes.Excitation;

        [Help("Calculate the intensities using the excitation error and the structure factor.")]
        public void Calc_Kinematical() => difSim.CalcMode = FormDiffractionSimulator.CalcModes.Kinematical;

        [Help("Calculate the intensities by the dynamical theory.")]
        public void Calc_Dynamical() => difSim.CalcMode = FormDiffractionSimulator.CalcModes.Dynamical;

        [Help("Float. Set/Get the image resolution (mm/pix).")]
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

        [Help("Float. Set/Get the image resolution (nm^-1/pix).")]
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
 
        [Help("Integer. Set/Get the image width in pixel.")]
        public int ImageWidth { get => difSim.ClientWidth; set => difSim.ClientWidth = value; }

        [Help("Integer. Set/Get the image height in pixel.")] 
        public int ImageHeight { get => difSim.ClientHeight; set => difSim.ClientHeight = value; }

        [Help("Set the image size in pixel.", "int width, int height")] 
        public void ImageSize(int width, int height) { ImageWidth = width; ImageHeight = height; }

        [Help("Float. Set/Get the distance (in mm) from the sample to the detector.")]
        public double CameraLength2 { get => difSim.CameraLength2; set => difSim.CameraLength2 = value; }
        
        [Help("Set coordinates (in mm) of the foot of the perpendicular line from the sample to the detector.", "double x, double y")]
        public void Foot(double x, double y) { difSim.Foot = new PointD(x, y); }

        [Help("True/False. Set/get whether screen rendering is skipped or not.")]
        public bool SkipRendering { get => difSim.SkipRendering; set => difSim.SkipRendering = value; }
      
        [Help("Get spot information as CSV-format text.")]
        public string SpotInfo() => (Execute(() => spotInfo()));
        
        [Help("Save the current simulation pattern as png format file. If filename is omitted, a dialog will open.", "string filename")]
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

    #region ImageSimulatorクラス (HRTEMandSTEMとPotentialの親クラス)

    public abstract class ImageSimulationClass(Macro _p, FormImageSimulator.ImageModes mode) : MacroSub(_p.main)
    {
        //internal readonly Macro p = _p;
        internal FormImageSimulator sim => _p.main.FormImageSimulator;

        internal FormImageSimulator.ImageModes Mode = mode;

        [Help("Set/get the accelerating voltage of electron (in kV).")]
        public double AccVol { get => sim.AccVol; set => sim.AccVol = value; }

        [Help("Integer.Set/get the maximum number of diffracted waves (Bloch waves) used in the dynamical scattering theory.")]
        public int NumberOfDiffractedWaves { get => sim.BlochNum; set => sim.BlochNum = value; }

        [Help("Integer. Set/get the width of the image to be simulated (in pixel).")]
        public int ImageWidth { get => sim.ImageSize.Width; set => sim.ImageSize = new Size(value, sim.ImageSize.Height); }

        [Help("Integer. Set/get the height of the image to be simulated (in pixel).")]
        public int ImageHeight { get => sim.ImageSize.Height; set => sim.ImageSize = new Size(sim.ImageSize.Width, value); }

        [Help("Set the image size to be simulated (in pixel).", "in width, int height")]
        public void ImageSize(int width, int height) => sim.ImageSize = new Size(width, height);

        [Help("Float. Set/get the resolution of the image to be simulated (in picometer/pixel).")]
        public double ImageResolution { get => sim.ImageResolution; set => sim.ImageResolution = value; }

        [Help("True/False. Set/get whether or not to display a unit cell.")]
        public bool UnitCellVisible { get => sim.UnitCellVisible; set => sim.UnitCellVisible = value; }

        [Help($"True/False. Set/get whether or not to display a image label.")]
        public bool LabelVisible { get => sim.LabelVisible; set => sim.LabelVisible = value; }

        [Help($"Integer. Set/get the size of labels ")]
        public int LabelSize { get => sim.LabelSize; set => sim.LabelSize = value; }

        [Help($"True/False. Whether or not to display a scale bar.")]
        public bool ScaleBarVisible { get => sim.ScaleBarVisible; set => sim.ScaleBarVisible = value; }

        [Help($"Float. Set/get the length of the scalebar (in nm).")]
        public double ScaleBarLength { get => sim.ScaleBarLength; set => sim.ScaleBarLength = value; }

        [Help($"True/False. Set/get whether or not to process Gaussian blur.")]
        public bool GaussianBlurEnabled { get => sim.GaussianBlurEnabled; set => sim.GaussianBlurEnabled = value; }

        [Help($"Float. Set/get the FWHM value (in pm) of Gaussian blur.")]
        public double GaussianBlurFWHM { get => sim.GaussianBlurFWHM; set => sim.GaussianBlurFWHM = value; }

        [Help("Open the ### simulator window.")]
        public void Open() { sim.Visible = true; sim.ImageMode = Mode; }

        [Help("Close the ### simulator window.")]
        public void Close() => sim.Visible = false;

        [Help("Simulate ### image(s) with the current settings.")]
        public void Simulate() { Open(); sim.Simulate(true); }

        [Help("True/False. On the saved image, whether or not to overprint the unit cell, labels and scale bar.")]
        public bool OverprintSymbols { get => sim.OverprintSymbols; set => sim.OverprintSymbols = value; }

        [Help("True/False. When simulating in serial image mode, whether or not to save each image individually.")]
        public bool SaveIndividually { get => sim.SaveIndividually; set => sim.SaveIndividually = value; }

        [Help("Save the simulated image as a PNG format. If 'filename' is omitted, a dialog box will appear.")]
        public void SaveImageAsPng(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.PNG, FormImageSimulator.ActionEnum.Save, filename);
        
        [Help("Save the simulated image as a TIFF format. If the 'filename' is omitted, a dialog box will appear.")] 
        public void SaveImageAsTif(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.TIFF, FormImageSimulator.ActionEnum.Save, filename);

        [Help("Save the simulated image as a Metafile (EMF) format. If 'filename' is omitted, a dialog box will appear.")]
        public void SaveImageAsEmf(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.Meta, FormImageSimulator.ActionEnum.Save, filename);

    }

    #endregion

    #region STEMとHRTEMの親クラスであり、ImageSimulatorの子クラス。
    /// <summary>
    /// STEMとHRTEMの親クラスであり、ImageSimulatorの子クラス。
    /// </summary>
    /// <param name="_p"></param>
    /// <param name="mode"></param>
    public class STEMandHRTEM_class(Macro _p, FormImageSimulator.ImageModes mode) : ImageSimulationClass(_p, mode)
    {
        [Help("Float. Set/get the sample thickness (in nm).")]
        public double Thickness { get => sim.Thickness; set => sim.Thickness = value; }

        [Help("Float. Set/get the Defocus value (in nm).")]
        public double Defocus { get => sim.Defocus; set => sim.Defocus = value; }

        [Help("Float. Set/get the Cs (spherical aberration) value (in mm).")]
        public double Cs { get => sim.Cs * 1E-6; set => sim.Cs = value * 1E6; }

        [Help("Float. Set/get the Cc (chromatic aberration) value (in mm).")]
        public double Cc { get => sim.Cc * 1E-6; set => sim.Cc = value * 1E6; }

        [Help("Float. Set/get the ΔV (1/e width of electron energy fluctuations) value (in eV).")]
        public double DeltaV { get => sim.DeltaVolFWHM * 1000.0; set => sim.DeltaVolFWHM = value / 1000.0; }

        [Help("Float. Get the Scherzer defocus value (in nm).")]
        public double Scherzer => sim.Scherzer;

        [Help("Set to single image mode.")]
        public void SingleImageMode() => sim.SingleImageMode = true;

        [Help("Set to serial image mode with/without thickness and with/without defocus.", "bool withThickness, bool withDefocus")]
        public void SerialImageMode(bool withThickness, bool withDefocus)
        {
            sim.SerialImageMode = true;
            sim.SerialImageWithThickness = withThickness;
            sim.SerialImageWithDefocus = withDefocus;
        }

        [Help("Float. Set/get the starting thickness (in nm) in serial images.")]
        public double SerialImageThicknessStart { get => sim.SerialImageThicknessStart; set => sim.SerialImageThicknessStart = value; }

        [Help("Float. Set/get the thickness step (in nm) in serial images.")]
        public double SerialImageThicknessStep { get => sim.SerialImageThicknessStep; set => sim.SerialImageThicknessStep = value; }

        [Help("Integer. Set/get the number of the thicknesses in serial images.")]
        public int SerialImageThicknessNum { get => sim.SerialImageThicknessNum; set => sim.SerialImageThicknessNum = value; }

        [Help("Float. Set/get the starting defocus (in nm) in serial images.")]
        public double SerialImageDefocusStart { get => sim.SerialImageDefocusStart; set => sim.SerialImageDefocusStart = value; }

        [Help("Float. Set/get the defocus step (in nm) in serial images.")]
        public double SerialImageDefocusStep { get => sim.SerialImageDefocusStep; set => sim.SerialImageDefocusStep = value; }

        [Help(" Integer. Set/get the number of the defocuses in serial images.")]
        public int SerialImageDefocusNum { get => sim.SerialImageDefocusNum; set => sim.SerialImageDefocusStep = value; }
    }
    #endregion

    #region STEMクラス
    public class STEMClass(Macro _p) : STEMandHRTEM_class(_p, FormImageSimulator.ImageModes.STEM)
    {
        [Help("Float. Set/get the convergence semiangle (in mrad).")]
        public double ConvergenceAngle { get => sim.STEM_ConvergenceAngle * 1000; set => sim.STEM_ConvergenceAngle = value / 1000; }

        [Help("Float. Set/get the inner semiangle (in mrad) of the annular detector.")]
        public double DetectorInnerAngle { get => sim.STEM_DetectorInnerAngle * 1000; set => sim.STEM_DetectorInnerAngle = value / 1000; }
        
        [Help("Float. Set/get the outer semiangle (in mrad) of the annular detector.")]
        public double DetectorOuterAngle { get => sim.STEM_DetectorOuterAngle * 1000; set => sim.STEM_DetectorOuterAngle = value / 1000; }

        [Help("Float. Set/get the effective source size (FWHM in pim).")]
        public double EffectiveSourceSize { get => sim.STEM_SourceSizeFWHM * 1000; set => sim.STEM_SourceSizeFWHM = value / 1000; }
        
        [Help("Float. Set/get the angular resolution (in mrad) of the convergent beam.")]
        public double AngularResolution { get => sim.STEM_AngularResolution * 1000; set => sim.STEM_AngularResolution = value / 1000; }

        [Help("Float. Set/get the slice thickness (in nm) for TDS calculation.")] 
        public double SliceThickness { get => sim.STEM_SliceThickness; set => sim.STEM_SliceThickness = value; }

        [Help("Display both elastic and TDS (inelastic) electrons.")]
        public void DisplayBoth() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.Both;
        
        [Help("Display elastic electrons only.")]
        public void DisplayElastic() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.Elastic;
        
        [Help("Display TDS (inelastic) electrons only.")] 
        public void DisplayTDS() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.TDS;
    }
    #endregion

    #region HRTEMクラス
    public class HRTEMClass(Macro _p) : STEMandHRTEM_class(_p, FormImageSimulator.ImageModes.HRTEM)
    {
        [Help("Set/get illumination semiangle β (in mrad) of the electron beam due to the finite source size effect.")]
        public double Beta { get => sim.HRTEM_Beta; set => sim.HRTEM_Beta = value; }
   
        [Help("Float. Set/get the semiangle (in mrad) of the objective aperture.")]
        public double ApertureSemiangle { get => sim.HRTEM_ObjAperRadius; set => sim.HRTEM_ObjAperRadius = value; }
        
        [Help("Set/get the x-direction shift (in mrad) of the objective aperture.")]
        public double ApertureShiftX { get => sim.HRTEM_ObjAperX; set => sim.HRTEM_ObjAperX = value; }

        [Help("Set/get the y-direction shift (in mrad) of the objective aperture.")]
        public double ApertureShiftY { get => sim.HRTEM_ObjAperY; set => sim.HRTEM_ObjAperY = value; }

        [Help("True/False. Set/get the open aperture state.")]
        public bool OpenAperture { get => sim.HRTEM_OpenObjAper; set => sim.HRTEM_OpenObjAper = value; }

        [Help("Use linear image model for calculating partial coherency.")]
        public void Mode_LinearImage() => sim.HRTEM_Mode = FormImageSimulator.HRTEM_Modes.Quasi;

        [Help("Use TCC (transmission cross coefficient) model for calculating partial coherency.")]
        public void Mode_TCC() => sim.HRTEM_Mode = FormImageSimulator.HRTEM_Modes.TCC;
    }
    #endregion

    #region Potentialクラス
    public class PotentialClass : ImageSimulationClass
    {
        public PotentialClass(Macro _p) : base(_p, FormImageSimulator.ImageModes.POTENTIAL)
        {
        }



    }

    #endregion

}

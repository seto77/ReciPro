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
    public CrystalClass Crystal;
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

        Crystal = new CrystalClass(this);
        HelpAttribute.GenerateHelpText(Crystal.GetType(), nameof(Crystal)).ForEach(s => help.Add(s));


        CrystalList = new CrystalListClass(this);
        HelpAttribute.GenerateHelpText(CrystalList.GetType(), nameof(CrystalList)).ForEach(s => help.Add(s));

        STEM = new STEMClass(this);
        HelpAttribute.GenerateHelpText(STEM.GetType(), nameof(STEM)).ForEach(s => help.Add(s.Replace("###", nameof(STEM))));

        HRTEM = new HRTEMClass(this);
        HelpAttribute.GenerateHelpText(HRTEM.GetType(), nameof(HRTEM)).ForEach(s => help.Add(s.Replace("###", nameof(HRTEM))));

        Potential = new PotentialClass(this);
        HelpAttribute.GenerateHelpText(Potential.GetType(), nameof(Potential)).ForEach(s => help.Add(s.Replace("###", nameof(Potential))));

    }

    // (260414Ch) Help text revised throughout this file to match the implementation and improve English.
    [Help("Pauses execution for the specified number of milliseconds.", "int millisec")]
    public static void Sleep(int millisec) => Thread.Sleep(millisec);

    // 260414Cl 追加 初心者向けサンプルマクロ。FormMacro が初回表示時にリストが空なら挿入する。
    // print() は使えないので Step by step 実行時にデバッグパネルで値を確認する作法を教える構成。
    public override (string name, string body)[] SampleMacros =>
    [
        ("01. Basic loop",
         """
         # Loop 10 times and compute the squares.
         # Run this with "Step by step" and watch 'i' and 'sq'
         # change in the debug panel. This is how ReciPro macros
         # inspect values (print() does not work here).
         for i in range(10):
             sq = i * i
         """),
        ("02. Using math",
         """
         # The math module is pre-imported. Use it directly.
         r = 5.0
         area = math.pi * r * r
         circumference = 2 * math.pi * r
         # Run in Step mode to see 'area' and 'circumference'.
         """),
        ("03. Rotate crystal",
         """
         # Rotate the current crystal by 30 degrees around the a-axis.
         ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
         """),
        ("04. Align to zone axis",
         """
         # Align so that the [001] zone axis is normal to the screen.
         ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
         """),
        ("05. Loop crystals",
         """
         # Collect the names of the first 5 crystals in the list.
         names = []
         for i in range(5):
             ReciPro.CrystalList.SelectedIndex = i
             names.append(ReciPro.Crystal.Name)
         # Run in Step mode to watch 'names' grow line by line.
         """),
        ("06. Diffraction pattern",
         """
         # Show the [001] diffraction pattern of the first crystal
         # in the list with 200 keV electrons.
         ReciPro.CrystalList.SelectedIndex = 0
         ReciPro.DifSim.Open()
         ReciPro.DifSim.Source_Electron()
         ReciPro.DifSim.Energy = 200
         ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
         """),
    ];

    #endregion

    #region ファイルクラス
    public class FileClass(Macro _p) : MacroSub(_p.main)
    {
        private FormMain main => _p.main;

        // (260414Ch) Help text revised to describe the actual return value and dialog behavior.
        [Help("Returns a directory path ending with '\\'. If 'filename' is omitted, opens a folder selection dialog; otherwise returns the directory that contains 'filename'.", "string filename")]
        public string GetDirectoryPath(string filename = "") => Execute<string>(new Func<string>(() => getDirectoryPath(filename)));
        private static string getDirectoryPath(string filename = "")
        {
            string path;
            if (filename == "")
            {
                var dlg = new FolderBrowserDialog();
                path = dlg.ShowDialog() == DialogResult.OK ? dlg.SelectedPath : "";
            }
            else
                path = System.IO.Path.GetDirectoryName(filename);
            return path + "\\";
        }

        [Help("Opens a file selection dialog and returns the full path of the selected file, or an empty string if canceled.")]
        // public string GetFileName() => Execute(() => getFileName()); // (260322Ch) 旧実装: 1 回しか使わない短い helper を経由していた
        public string GetFileName() => Execute<string>(new Func<string>(() =>
        {
            var dlg = new OpenFileDialog();
            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileName : "";
        })); // (260322Ch) OpenFileDialog の取得処理をその場でインライン化

        [Help("Opens a file selection dialog, allows multiple selection, and returns the full paths of the selected files.")]
        // public string[] GetFileNames() => Execute<string[]>(new Func<string[]>(() => getFileNames())); // (260322Ch) 旧実装
        public string[] GetFileNames() => Execute<string[]>(new Func<string[]>(() =>
        {
            var dlg = new OpenFileDialog() { Multiselect = true };
            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileNames : [];
        })); // (260322Ch) 複数選択の file dialog 取得処理をその場でインライン化

        [Help("Loads a crystal list file in XML format. If 'filename' is omitted, opens a file selection dialog.", "string filename")]
        public void ReadCrystalList(string filename = "") => Execute(() => main.ReadCrystalList(filename, false, false));

        [Help("Loads a crystal file in CIF or AMC format. If 'filename' is omitted, opens a file selection dialog.", "string filename")]
        public void ReadCrystal(string filename = "") => Execute(() => main.ReadCrystal(filename));

        [Help("Exports the selected crystal in CIF format. If 'filename' is omitted, opens a save dialog.", "string filename")]
        public void ExportAsCIF(string filename = "") => Execute(() => main.ExportCIF(filename));

        [Help("Saves text in UTF-8. If 'filename' is omitted, opens a save dialog.", "string textData, string filename")]
        public void SaveText(string textData, string filename = "")
        {
            if(filename == "")
            {
                var dlg = new SaveFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                    filename = dlg.FileName;
                else
                    return;
            }
            Execute(() => System.IO.File.WriteAllText(filename, textData,Encoding.UTF8));
        }
    }
    #endregion

    #region CrystalList クラス
    public class CrystalClass(Macro _p) : MacroSub(_p.main)
    {
        private FormMain main => _p.main;

        // (260414Ch) Help text revised for clarity and to match the underlying properties.
        [Help("Gets the name of the selected crystal.")]
        public string Name { get => main.Crystal.Name; }

        [Help("Gets the chemical formula of the selected crystal.")]

        public string ChemicalFormula { get => main.Crystal.ChemicalFormulaSum;}
        
        [Help("Gets the density of the selected crystal in g/cm^3.")]

        public double Density { get => main.Crystal.Density;}

    }
    #endregion

    #region CrystalList クラス
    public class CrystalListClass(Macro _p) : MacroSub(_p.main)
    {
        private FormMain main => _p.main;

        // (260414Ch) Help text revised to use consistent property/method wording.
        [Help("Gets or sets the index of the selected crystal in the crystal list.")]
        public int SelectedIndex { get => main.SelectedCrystalIndex; set => main.SelectedCrystalIndex = value; }

        [Help("Adds the crystal currently shown in 'Crystal Information' to the end of the list.")]
        public void Add() => Execute(() => main.AddCrystal());

        [Help("Replaces the selected item in the list with the crystal currently shown in 'Crystal Information'.")]
        public void Replace() => Execute(() => main.ReplaceCrystal());

        [Help("Deletes the selected crystal from the list.")]
        public void Delete() => Execute(() => main.DeleteCrystal());

        [Help("Deletes all crystals from the list.")]
        public void ClearAll() => Execute(() => main.CrystalListClear());

        [Help("Moves the selected crystal up in the list.")]
        public void MoveUp() => Execute(() => main.MoveUp());

        [Help("Moves the selected crystal down in the list.")]
        public void MoveDown() => Execute(() => main.MoveDown());
    }
    #endregion

    #region Dir (Direction 方位)クラス
    public class DirectionClass(Macro _p) : MacroSub(_p.main)
    {
        private FormMain main => _p.main;

        // (260414Ch) Help text revised and corrected where axis/plane meanings were swapped.
        [Help("Sets the current crystal orientation from Euler angles in radians.", "double phi, double theta, double psi")]
        public void Euler(double phi, double theta, double psi)
        {
            main.SetRotation(phi, theta, psi);
            Application.DoEvents();
        }

        [Help("Sets the current crystal orientation from Euler angles in degrees.", "double phi, double theta, double psi")]
        public void EulerInDegree(double phi, double theta, double psi)
        {
            main.SetRotation(phi / 180.0 * Math.PI, theta / 180.0 * Math.PI, psi / 180.0 * Math.PI);
            Application.DoEvents();
        }

        [Help("Sets the current crystal orientation from Euler angles in degrees.", "double phi, double theta, double psi")]
        public void EulerInDeg(double phi, double theta, double psi)
        {
            main.SetRotation(phi / 180.0 * Math.PI, theta / 180.0 * Math.PI, psi / 180.0 * Math.PI);
            Application.DoEvents();
        }

        [Help("Rotates the current crystal around the specified axis vector (vX, vY, vZ) by the specified angle in radians.", 
            "double vX, double vY, double vZ, double angle")]
        public void Rotate(double vX, double vY, double vZ, double angle) => main.Rotate((vX, vY, vZ), angle);

        [Help("Rotates the current crystal around the specified axis vector (vX, vY, vZ) by the specified angle in degrees."
            , "double vX, double vY, double vZ, double angle")]
        public void RotateInDeg(double vX, double vY, double vZ, double angle) => Rotate(vX, vY, vZ, angle*Math.PI/180.0);

        [Help("Rotates the current crystal around the crystallographic direction [uvw] by the specified angle in radians.", 
            "int u, int v, int w, double angle")]
        public void RotateAroundAxis(int u, int v, int w, double angle)
        {
            Vector3DBase a = main.Crystal.A_Axis, b = main.Crystal.B_Axis, c = main.Crystal.C_Axis;
            var axis = main.Crystal.RotationMatrix * (u * a + v * b + w * c);
            main.Rotate(axis, angle);
        }
        [Help("Rotates the current crystal around the crystallographic direction [uvw] by the specified angle in degrees."
            , "int u, int v, int w, double angle")]
        public void RotateAroundAxisInDeg(int u, int v, int w, double angle)=> RotateAroundAxis(u, v, w, angle * Math.PI / 180.0);

        [Help("Rotates the current crystal around the normal of the crystallographic plane (hkl) by the specified angle in radians.", "int h, int k, int l, double angle")]
        
        public void RotateAroundPlane(int h, int k, int l, double angle)
        {
            var rot = main.Crystal.MatrixInverse;
            var axis = main.Crystal.RotationMatrix * (h * rot.Row1 + k * rot.Row2 + l * rot.Row3);
            main.Rotate(axis, angle);
        }

        [Help("Rotates the current crystal around the normal of the crystallographic plane (hkl) by the specified angle in degrees.", "int h, int k, int l, double angle")]
        public void RotateAroundPlaneInDeg(int h, int k, int l, double angle)=>RotateAroundPlane(h, k, l, angle * Math.PI / 180.0);

        [Help("Rotates the current crystal so that the specified plane (hkl) becomes parallel to the screen.", "int h, int k, int l")]
        public void ProjectAlongPlane(int h, int k, int l)
        {
            main.SetPlane(h, k, l);
            main.ProjectAlongPlane();
            Application.DoEvents();
        }

        [Help("Rotates the current crystal so that the specified direction [uvw] is normal to the screen.", "int u, int v, int w")]
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

        // (260414Ch) Help text revised for natural English and corrected units/semantics where needed.
        [Help("Opens the Diffraction Simulator window.")]
        public void Open() => Execute(new Action(() => difSim.Visible = true));
        
        [Help("Closes the Diffraction Simulator window.")] 
        public void Close() => Execute(new Action(() => difSim.Visible = false));


        [Help("Sets the incident wave source to X-ray.")]
        public void Source_Xray() { difSim.Source = WaveSource.Xray; }
        [Help("Sets the incident wave source to electrons.")]
        public void Source_Electron() { difSim.Source = WaveSource.Electron; }
        
        [Help("Sets the incident wave source to neutrons.")]
        public void Source_Neutron() { difSim.Source = WaveSource.Neutron; }

        [Help("Gets or sets the incident beam energy. Units: keV for X-rays and electrons, meV for neutrons.")]
        public double Energy { get => difSim.Energy; set => difSim.Energy = value; }

        [Help("Gets or sets the incident wavelength in nm.")] 
        public double Wavelength { get => difSim.WaveLength; set => difSim.WaveLength = value; }

        [Help("Gets or sets the specimen thickness in nm.")]
        public double Thickness { get => difSim.Thickness; set => difSim.Thickness = value; }

        [Help("Gets or sets the number of diffracted waves used for dynamical calculations.")]
        public int NumberOfDiffractedWaves { get => difSim.NumberOfDiffractedWaves; set => difSim.NumberOfDiffractedWaves = value; }

        [Help("Sets the beam mode to a parallel beam.")]
        public void Beam_Parallel() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.Parallel;

        [Help("Sets the beam mode to X-ray precession.")]
        public void Beam_PrecessionXray() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.PrecessionXray;

        [Help("Sets the beam mode to electron precession.")]
        public void Beam_PrecessionElectron() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.PrecessionElectron;

        [Help("Sets the beam mode to a convergent electron beam.")]
        public void Beam_Convergence() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.Convergence;

        [Help("Uses excitation error only for intensity calculations.")]
        public void Calc_Excitation() => difSim.CalcMode = FormDiffractionSimulator.CalcModes.Excitation;

        [Help("Uses excitation error and structure factors for intensity calculations.")]
        public void Calc_Kinematical() => difSim.CalcMode = FormDiffractionSimulator.CalcModes.Kinematical;

        [Help("Uses dynamical theory for intensity calculations.")]
        public void Calc_Dynamical() => difSim.CalcMode = FormDiffractionSimulator.CalcModes.Dynamical;

        [Help("Gets or sets the image resolution in mm/pixel.")]
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

        [Help("Gets or sets the image resolution in nm^-1/pixel.")]
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
 
        [Help("Gets or sets the image width in pixels.")]
        public int ImageWidth { get => difSim.ClientWidth; set => difSim.ClientWidth = value; }

        [Help("Gets or sets the image height in pixels.")] 
        public int ImageHeight { get => difSim.ClientHeight; set => difSim.ClientHeight = value; }

        [Help("Sets the image size in pixels.", "int width, int height")] 
        public void ImageSize(int width, int height) { ImageWidth = width; ImageHeight = height; }

        [Help("Gets or sets CameraLength2, the sample-to-detector distance, in mm.")]
        public double CameraLength2 { get => difSim.CameraLength2; set => difSim.CameraLength2 = value; }

        [Help("Gets or sets the detector tilt angle Tau in radians.")]
        public double Tau { get => difSim.Tau; set => difSim.Tau = value; }

        [Help("Gets or sets the detector tilt angle Tau in degrees.")]
        public double TauInDeg { get => difSim.Tau/Math.PI*180; set => difSim.Tau = value*Math.PI / 180; }

        [Help("Gets or sets the detector tilt angle Phi in radians.")]
        public double Phi { get => difSim.Phi ; set => difSim.Phi = value; }

        [Help("Gets or sets the detector tilt angle Phi in degrees.")]
        public double PhiInDeg { get => difSim.Phi / Math.PI * 180; set => difSim.Phi = value * Math.PI / 180; }

        [Help("Sets the detector-coordinate position of the image center in mm. (0, 0) corresponds to the foot of the perpendicular from the sample to the detector.", "double x, double y")]
        public void Foot(double x, double y) { difSim.Foot = new PointD(x, y); }

        [Help("Gets or sets whether the final screen rendering step is skipped. Spot positions are still calculated.")]
        public bool SkipRendering { get => difSim.SkipRendering; set => difSim.SkipRendering = value; }

        [Help("Saves the current simulation image as a PNG file. If 'filename' is omitted, opens a save dialog.", "string filename")]
        public void SaveAsPng(string filename = "") => difSim.SaveOrCopy(true, true, true, filename);




        [Help("Returns spot information for the current dynamical calculation as CSV text.")]
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

    #region ImageSimulatorクラス (HRTEMandSTEMとPotentialの親クラス)

    public abstract class ImageSimulationClass(Macro _p, FormImageSimulator.ImageModes mode) : MacroSub(_p.main)
    {
        //internal readonly Macro p = _p;
        internal FormImageSimulator sim => _p.main.FormImageSimulator;

        internal FormImageSimulator.ImageModes Mode = mode;

        // (260414Ch) Help text revised and corrected for actual exposed units.
        [Help("Gets or sets the electron accelerating voltage in kV.")]
        public double AccVol { get => sim.AccVol; set => sim.AccVol = value; }

        [Help("Gets or sets the maximum number of diffracted waves (Bloch waves) used in dynamical scattering calculations.")]
        public int NumberOfDiffractedWaves { get => sim.BlochNum; set => sim.BlochNum = value; }

        [Help("Gets or sets the simulated image width in pixels.")]
        public int ImageWidth { get => sim.ImageSize.Width; set => sim.ImageSize = new Size(value, sim.ImageSize.Height); }

        [Help("Gets or sets the simulated image height in pixels.")]
        public int ImageHeight { get => sim.ImageSize.Height; set => sim.ImageSize = new Size(sim.ImageSize.Width, value); }

        [Help("Sets the simulated image size in pixels.", "int width, int height")]
        public void ImageSize(int width, int height) => sim.ImageSize = new Size(width, height);

        [Help("Gets or sets the simulated image resolution in nm/pixel.")]
        public double ImageResolution { get => sim.ImageResolution; set => sim.ImageResolution = value; }

        [Help("Gets or sets whether the unit cell is displayed.")]
        public bool UnitCellVisible { get => sim.UnitCellVisible; set => sim.UnitCellVisible = value; }

        [Help($"Gets or sets whether the image label is displayed.")]
        public bool LabelVisible { get => sim.LabelVisible; set => sim.LabelVisible = value; }

        [Help($"Gets or sets the label font size.")]
        public int LabelSize { get => sim.LabelSize; set => sim.LabelSize = value; }

        [Help($"Gets or sets whether the scale bar is displayed.")]
        public bool ScaleBarVisible { get => sim.ScaleBarVisible; set => sim.ScaleBarVisible = value; }

        [Help($"Gets or sets the scale bar length in nm.")]
        public double ScaleBarLength { get => sim.ScaleBarLength; set => sim.ScaleBarLength = value; }

        [Help($"Gets or sets whether Gaussian blur is applied.")]
        public bool GaussianBlurEnabled { get => sim.GaussianBlurEnabled; set => sim.GaussianBlurEnabled = value; }

        [Help($"Gets or sets the Gaussian blur FWHM in pm.")]
        public double GaussianBlurFWHM { get => sim.GaussianBlurFWHM; set => sim.GaussianBlurFWHM = value; }

        [Help("Opens the ### simulator window.")]
        public void Open() { sim.Visible = true; sim.ImageMode = Mode; }

        [Help("Closes the ### simulator window.")]
        public void Close() => sim.Visible = false;

        [Help("Runs the ### simulation with the current settings.")]
        public void Simulate() { Open(); sim.Simulate(true); }

        [Help("Gets or sets whether the unit cell, labels, and scale bar are overprinted on saved images.")]
        public bool OverprintSymbols { get => sim.OverprintSymbols; set => sim.OverprintSymbols = value; }

        [Help("Gets or sets whether each image is saved separately in serial-image mode.")]
        public bool SaveIndividually { get => sim.SaveIndividually; set => sim.SaveIndividually = value; }

        [Help("Saves the simulated image as a PNG file. If 'filename' is omitted, opens a save dialog.")]
        public void SaveImageAsPng(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.PNG, FormImageSimulator.ActionEnum.Save, filename);
        
        [Help("Saves the simulated image as a TIFF file. If 'filename' is omitted, opens a save dialog.")] 
        public void SaveImageAsTif(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.TIFF, FormImageSimulator.ActionEnum.Save, filename);

        [Help("Saves the simulated image as an EMF file. If 'filename' is omitted, opens a save dialog.")]
        public void SaveImageAsEmf(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.Meta, FormImageSimulator.ActionEnum.Save, filename);

    }

    #endregion

    #region STEMとHRTEMの親クラスであり、ImageSimulatorの子クラス。
    /// <summary>STEMとHRTEMの親クラスであり、ImageSimulatorの子クラス。</summary>
    /// <param name="_p"></param>
    /// <param name="mode"></param>
    public class STEMandHRTEM_class(Macro _p, FormImageSimulator.ImageModes mode) : ImageSimulationClass(_p, mode)
    {
        // (260414Ch) Help text revised and corrected where the public API exposed different units than the old text claimed.
        [Help("Gets or sets the specimen thickness in nm.")]
        public double Thickness { get => sim.Thickness; set => sim.Thickness = value; }

        [Help("Gets or sets the defocus in nm.")]
        public double Defocus { get => sim.Defocus; set => sim.Defocus = value; }

        [Help("Gets or sets the spherical aberration coefficient Cs in mm.")]
        public double Cs { get => sim.Cs * 1E-6; set => sim.Cs = value * 1E6; }

        [Help("Gets or sets the chromatic aberration coefficient Cc in mm.")]
        public double Cc { get => sim.Cc * 1E-6; set => sim.Cc = value * 1E6; }

        [Help("Gets or sets the energy spread ΔV as FWHM in eV.")]
        public double DeltaV { get => sim.DeltaVolFWHM * 1000.0; set => sim.DeltaVolFWHM = value / 1000.0; }

        [Help("Gets the Scherzer defocus in nm.")]
        public double Scherzer => sim.Scherzer;

        [Help("Switches to single-image mode.")]
        public void SingleImageMode() => sim.SingleImageMode = true;

        [Help("Switches to serial-image mode and chooses whether thickness and defocus are varied.", "bool withThickness, bool withDefocus")]
        public void SerialImageMode(bool withThickness, bool withDefocus)
        {
            sim.SerialImageMode = true;
            sim.SerialImageWithThickness = withThickness;
            sim.SerialImageWithDefocus = withDefocus;
        }

        [Help("Gets or sets the starting thickness in serial-image mode, in nm.")]
        public double SerialImageThicknessStart { get => sim.SerialImageThicknessStart; set => sim.SerialImageThicknessStart = value; }

        [Help("Gets or sets the thickness step in serial-image mode, in nm.")]
        public double SerialImageThicknessStep { get => sim.SerialImageThicknessStep; set => sim.SerialImageThicknessStep = value; }

        [Help("Gets or sets the number of thickness values in serial-image mode.")]
        public int SerialImageThicknessNum { get => sim.SerialImageThicknessNum; set => sim.SerialImageThicknessNum = value; }

        [Help("Gets or sets the starting defocus in serial-image mode, in nm.")]
        public double SerialImageDefocusStart { get => sim.SerialImageDefocusStart; set => sim.SerialImageDefocusStart = value; }

        [Help("Gets or sets the defocus step in serial-image mode, in nm.")]
        public double SerialImageDefocusStep { get => sim.SerialImageDefocusStep; set => sim.SerialImageDefocusStep = value; }

        // public int SerialImageDefocusNum { get => sim.SerialImageDefocusNum; set => sim.SerialImageDefocusStep = value; } // (260414Ch) 旧実装: setter が個数ではなく step を書き換えていた
        [Help("Gets or sets the number of defocus values in serial-image mode.")]
        public int SerialImageDefocusNum { get => sim.SerialImageDefocusNum; set => sim.SerialImageDefocusNum = value; }
    }
    #endregion

    #region STEMクラス
    public class STEMClass(Macro _p) : STEMandHRTEM_class(_p, FormImageSimulator.ImageModes.STEM)
    {
        // (260414Ch) Help text revised for terminology and unit consistency.
        [Help("Gets or sets the convergence semi-angle in mrad.")]
        public double ConvergenceAngle { get => sim.STEM_ConvergenceAngle * 1000; set => sim.STEM_ConvergenceAngle = value / 1000; }

        [Help("Gets or sets the inner semi-angle of the annular detector in mrad.")]
        public double DetectorInnerAngle { get => sim.STEM_DetectorInnerAngle * 1000; set => sim.STEM_DetectorInnerAngle = value / 1000; }
        
        [Help("Gets or sets the outer semi-angle of the annular detector in mrad.")]
        public double DetectorOuterAngle { get => sim.STEM_DetectorOuterAngle * 1000; set => sim.STEM_DetectorOuterAngle = value / 1000; }

        [Help("Gets or sets the effective source size as FWHM in pm.")]
        public double EffectiveSourceSize { get => sim.STEM_SourceSizeFWHM * 1000; set => sim.STEM_SourceSizeFWHM = value / 1000; }
        
        [Help("Gets or sets the angular resolution of the convergent beam in mrad.")]
        public double AngularResolution { get => sim.STEM_AngularResolution * 1000; set => sim.STEM_AngularResolution = value / 1000; }

        [Help("Gets or sets the slice thickness for TDS calculations in nm.")] 
        public double SliceThickness { get => sim.STEM_SliceThickness; set => sim.STEM_SliceThickness = value; }

        [Help("Displays both the elastic and TDS (inelastic) components.")]
        public void DisplayBoth() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.Both;
        
        [Help("Displays only the elastic component.")]
        public void DisplayElastic() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.Elastic;
        
        [Help("Displays only the TDS (inelastic) component.")] 
        public void DisplayTDS() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.TDS;
    }
    #endregion

    #region HRTEMクラス
    public class HRTEMClass(Macro _p) : STEMandHRTEM_class(_p, FormImageSimulator.ImageModes.HRTEM)
    {
        // (260414Ch) Help text corrected to the actual public units (radians, not mrad).
        [Help("Gets or sets the illumination semi-angle beta in radians.")]
        public double Beta { get => sim.HRTEM_Beta; set => sim.HRTEM_Beta = value; }
   
        [Help("Gets or sets the objective-aperture semi-angle in radians.")]
        public double ApertureSemiangle { get => sim.HRTEM_ObjAperRadius; set => sim.HRTEM_ObjAperRadius = value; }
        
        [Help("Gets or sets the x shift of the objective aperture in radians.")]
        public double ApertureShiftX { get => sim.HRTEM_ObjAperX; set => sim.HRTEM_ObjAperX = value; }

        [Help("Gets or sets the y shift of the objective aperture in radians.")]
        public double ApertureShiftY { get => sim.HRTEM_ObjAperY; set => sim.HRTEM_ObjAperY = value; }

        [Help("Gets or sets whether the objective aperture is open.")]
        public bool OpenAperture { get => sim.HRTEM_OpenObjAper; set => sim.HRTEM_OpenObjAper = value; }

        [Help("Uses the linear image model for partial-coherency calculations.")]
        public void Mode_LinearImage() => sim.HRTEM_Mode = FormImageSimulator.HRTEM_Modes.Quasi;

        [Help("Uses the TCC (transmission cross coefficient) model for partial-coherency calculations.")]
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

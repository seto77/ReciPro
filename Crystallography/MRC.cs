using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Crystallography
{
    public class MRC
    {

        #region プロパティ 基本 https://www.ccpem.ac.uk/mrc_format/mrc2014.php
        /// <summary>
        /// number of columns in 3D data array (fast axis)
        /// </summary>
        public int NX { get; set; }
        /// <summary>
        /// number of rows in 3D data array (medium axis)
        /// </summary>
        public int NY { get; set; }
        /// <summary>
        /// number of sections in 3D data array (slow axis)
        /// </summary>
        public int NZ { get; set; }
        /// <summary>
        /// 0: 8-bit signed integer (range -128 to 127);
        /// 1: 16-bit signed integer;
        /// 2: 32-bit signed real;
        /// 3: transform : complex 16-bit integers;
        /// 4: transform : complex 32-bit reals;
        /// 6: 16-bit unsigned integer;
        /// </summary>
        public int MODE { get; set; }
        /// <summary>
        /// location of first column in unit cell
        /// </summary>
        public int NXSTART { get; set; }
        /// <summary>
        /// location of first row in unit cell
        /// </summary>
        public int NYSTART { get; set; }
        /// <summary>
        /// location of first section in unit cell
        /// </summary>
        public int NZSTART { get; set; }
        /// <summary>
        /// sampling along X axis of unit cell
        /// </summary>
        public int MX { get; set; }
        /// <summary>
        /// sampling along Y axis of unit cell
        /// </summary>
        public int MY { get; set; }
        /// <summary>
        /// sampling along Z axis of unit cell
        /// </summary>
        public int MZ { get; set; }
        /// <summary>
        /// cell dimensions in angstroms
        /// </summary>
        public double[] CELLA { get; set; }
        /// <summary>
        /// cell angles in degrees
        /// </summary>
        public double[] CELLB { get; set; }

        /// <summary>
        /// axis corresp to cols (1,2,3 for X,Y,Z)
        /// </summary>
        public int MAPC { get; set; }

        /// <summary>
        /// axis corresp to rows (1,2,3 for X,Y,Z)
        /// </summary>
        public int MAPR { get; set; }

        /// <summary>
        /// axis corresp to sections (1,2,3 for X,Y,Z)
        /// </summary>
        public int MAPS { get; set; }

        /// <summary>
        /// minimum density value
        /// </summary>
        public double DMIN { get; set; }

        /// <summary>
        /// maximum density value
        /// </summary>
        public double DMAX { get; set; }
        /// <summary>
        /// mean density value
        /// </summary>
        public double DMEAN { get; set; }

        /// <summary>
        /// space group number
        /// </summary>
        public int ISPG { get; set; }
        /// <summary>
        /// size of extended header (which follows main header) in bytes
        /// </summary>
        public int NSYMBT { get; set; }
        /// <summary>
        /// extra space used for anything - 0 by default
        /// </summary>
        public int EXTLA { get; set; }

        /// <summary>
        /// code for the type of extended header
        /// </summary>
        public string EXTTYP { get; set; }
        /// <summary>
        /// version of the MRC format
        /// </summary>
        public int NVERSION { get; set; }

        /// <summary>
        /// phase origin (pixels) or origin of subvolume (A)
        /// </summary>
        public double[] ORIGIN { get; set; }

        /// <summary>
        /// character string 'MAP ' to identify file type
        /// </summary>
        public string MAP { get; set; }
        /// <summary>
        /// machine stamp encoding byte ordering of data
        /// </summary>
        public string MACHST { get; set; }

        /// <summary>
        /// rms deviation of map from mean density
        /// </summary>
        public double RMS { get; set; }

        /// <summary>
        /// number of labels being used
        /// </summary>
        public int NLABEL { get; set; }

        /// <summary>
        /// 10 80-character text labels
        /// </summary>
        public string LABEL { get; set; }

        #endregion

        #region 拡張ヘッダ

        //イメージ、装置、アプリケーション
        /// <summary>
        /// 0; 0x0000; Int32; NA;
        /// Metadata size[bytes]
        /// </summary>
        public int Metadata_size { get; set; }
        /// <summary>
        /// 4; 0x0004; Int32; NA; 
        /// Version ID of the metadata format. 0 = Initial version which is described here
        /// </summary>
        public int Metadata_version { get; set; }
        /// <summary>
        ///  8; 0x0008; UInt32; NA; 
        ///  Individual bits indicate which metadata fields are set.  
        /// </summary>
        public uint Bitmask_1 { get; set; }
        /// <summary>
        ///  12; 0x000C; Float64; Bitmask 1 – #0; 
        ///  Time when the image was taken. The used format is the DATE data type that is used in OLE automation by Microsoft:
        ///   Microsoft OLE DATE data type specification (https://msdn.microsoft.com/enus/ llibrary/38wh24td.aspx)
        /// </summary>
        public double Timestamp { get; set; }
        /// <summary>
        /// 20; 0x0014; 16 chars; Bitmask 1 – #1; 
        /// Identifier for microscope type (Krios, Talos, Titan, Metrios, etc.)
        /// </summary>
        public string Microscope_type { get; set; }// 
        /// <summary>
        ///  36; 0x0024; 16 chars; Bitmask 1 – #2;
        ///  Microscope identifier
        /// </summary>
        public string D_Number { get; set; }//
        /// <summary>
        ///  52; 0x0034; 16 chars; Bitmask 1 – #3;
        ///  Application name
        /// </summary>
        public string Application { get; set; }//
        /// <summary>
        ///  68; 0x0044; 16 chars; Bitmask 1 – #4; 
        /// </summary>
        public string Application_version { get; set; }//

        //電子銃 
        /// <summary>
        /// 84; 0x0054; Float64; Bitmask 1 – #5;
        /// High tension [Volt]
        /// </summary>
        public double HT { get; set; }
        /// <summary>
        /// 92; 0x005C; Float64; Bitmask 1 – #6;
        /// Dose[electrons / m²]
        /// </summary>
        public double Dose { get; set; }//

        //ステージ 
        /// <summary>
        /// 100; 0x0064; Float64; Bitmask 1 – #7;
        /// Holder Alpha tilt along axis[degr.]
        /// </summary>
        public double Alpha_tilt { get; set; }//
        /// <summary>
        /// 108; 0x006C; Float64; Bitmask 1 – #8;
        /// Holder Beta tilt along axis[degr.]
        /// </summary>
        public double Beta_tilt { get; set; } //
        /// <summary>
        /// 116; 0x0074; Float64; Bitmask 1 – #9;
        /// Stage X position[m]
        /// </summary>
        public double X_Stage { get; set; }//
        /// <summary>
        /// 124; 0x007C; Float64; Bitmask 1 – #10;
        /// Stage Y position[m]
        /// </summary>
        public double Y_Stage { get; set; }// 
        /// <summary>
        /// 132; 0x0084; Float64; Bitmask 1 – #11;
        /// Stage Z position[m]
        /// </summary>
        public double Z_Stage { get; set; }// 
        /// <summary>
        ///  140; 0x008C; Float64; Bitmask 1 – #12;
        ///  Angle of tilt axis in image[degr.]
        /// </summary>
        public double Tilt_axis_angle { get; set; }//
        /// <summary>
        /// 148; 0x0094; Float64; Bitmask 1 – #13;
        /// Measured rotation angle after b flip [degr.] (Tomography only)
        /// </summary>
        public double Dual_axis_rotation { get; set; }// 

        //ピクセルサイズ
        /// <summary>
        /// 156; 0x009C; Float64; Bitmask 1 – #14.
        /// Pixel size X[m]
        /// </summary>
        public double Pixel_size_X { get; set; }//
        /// <summary>
        ///  164; 0x00A4; Float64; Bitmask 1 – #15;
        ///  Pixel size Y[m]
        /// </summary>
        public double Pixel_size_Y { get; set; }//

        //光学系
        /// <summary>
        /// 220; 0x00DC; Float64; Bitmask 1 – #22;
        /// Defocus[m]
        /// </summary>
        public double Defocus { get; set; }// 
        /// <summary>
        /// 228 0x00E4 Float64 Bitmask 1 – #23;
        /// STEM defocus [m]
        /// </summary>
        public double STEM_Defocus { get; set; }// 
        /// <summary>
        /// 236 0x00EC Float64 Bitmask 1 – #24;
        /// Relative defocus applied by application[m]
        /// </summary>
        public double Applied_defocus { get; set; }// 
        /// <summary>
        /// 244 0x00F4 Int32 Bitmask 1 – #25;
        /// 1:TEM, 2:STEM
        /// </summary>
        public int Instrument_mode { get; set; }//
        /// <summary>
        /// 248 0x00F8 Int32  Bitmask 1 – #26;
        /// 1: Imaging, 2: Diffraction
        /// </summary>
        public int Projection_mode { get; set; }// 
        /// <summary>
        /// 252 0x00FC 16 chars Bitmask 1 – #27;
        /// LM, HM, Lorentz
        /// </summary>
        public string Objective_lens_mode { get; set; }// 
        /// <summary>
        /// 268 0x010C 16 chars Bitmask 1 – #28;
        /// Mi, SA, Mh
        /// </summary>
        public string High_magnification_mode { get; set; }// 
        /// <summary>
        /// 284 0x011C Int32 Bitmask 1 – #29;
        /// 1 = micro, 2 = nano 
        /// </summary>
        public int Probe_mode { get; set; }// 
        /// <summary>
        /// 288 0x0120 Bool Bitmask 1 – #30;
        /// TRUE when the magnifications are adapted to the energy filter
        /// </summary>
        public bool EFTEM_On { get; set; } // 
        /// <summary>
        ///  289 0x0121 Float64 Bitmask 1 – #31 ;
        ///  Nominal magnification
        /// </summary>
        public double Magnification { get; set; }//
        /// <summary>
        /// 297 0x0129 UInt32 NA;
        /// Individual bits indicate which metadata fields are set.
        /// </summary>
        public uint Bitmask_2 { get; set; } // 
        /// <summary>
        ///  301 0x012D Float64 Bitmask 2 – #0;
        ///   Nominal camera length[m]
        /// </summary>
        public double Camera_length { get; set; }//
        /// <summary>
        /// 309 0x0135 Int32 Bitmask 2 – #1
        /// </summary>
        public int Spot_index { get; set; }// 
        /// <summary>
        /// 313 0x0139 Float64 Bitmask 2 – #2;
        /// TEM: beam diameter in meters, STEM: not used, Undefined on 2 lens condenser systems
        /// </summary>
        public double Illuminated_area { get; set; }// 
        /// <summary>
        /// 321 0x0141 Float64 Bitmask 2 – #3;
        /// Uncalibrated measure of beam diameter on 2 lens condenser systems
        /// </summary>
        public double Intensity { get; set; }// 
        /// <summary>
        /// 329 0x0149 Float64 Bitmask 2 – #4;
        /// [degr.] Undefined on 2 lens condenser systems
        /// </summary>
        public double Convergence_angle { get; set; }// 
        /// <summary>
        /// 337 0x0151 16 chars Bitmask 2 – #5;
        /// None, Parallel, Probe, Free, Undefined on 2 lens condenser systems
        /// </summary>
        public string Illumination_mode { get; set; }// 
        /// <summary>
        ///  353 0x0161 Bool Bitmask 2 – #6;
        ///  Undefined on 2 lens condenser systems
        /// </summary>
        public bool Wide_convergence_angle_range { get; set; }//


        //EFTEM イメージ
        /// <summary>
        ///  354 0x0162 Bool Bitmask 2 – #7
        /// </summary>
        public bool Slit_inserted { get; set; }//
        /// <summary>
        /// 355 0x0163 Float64 Bitmask 2 – #8;
        /// Slit width [eV]
        /// </summary>
        public double Slit_width { get; set; }// 
        /// <summary>
        ///  363 0x016B Float64 Bitmask 2 – #9;
        ///  [Volt] 
        /// </summary>
        public double Acceleration_voltage_offset { get; set; }//
        /// <summary>
        /// 371 0x0173 Float64 Bitmask 2 – #10;
        /// [Volt]
        /// </summary>
        public double Drift_tube_voltage { get; set; }//
        /// <summary>
        /// 379 0x017B Float64 Bitmask 2 – #11;
        /// [eV]
        /// </summary>
        public double Energy_shift { get; set; }// 
        /// <summary>
        ///  387 0x0183 Float64 Bitmask 2 – #12;
        ///  Corrective image or beam shift relative to exposure preset (in logical units).
        ///  TEM: pure image shift, STEM: image-beamshift-
        /// </summary>
        public double Shift_offset_X { get; set; }//
        /// <summary>
        ///  395 0x018B Float64 Bitmask 2 – #13. 
        ///  Corrective image or beam shift relative to exposure preset (in logical units).
        ///  TEM: pure image shift, STEM: image-beamshift-
        /// </summary>
        public double Shift_offset_Y { get; set; }//
        /// <summary>
        /// 403 0x0193 Float64 Bitmask 2 – #14;
        /// Applied shift due to optimized position and tracking(in logical units).
        /// TEM: image beam shift, STEM: beam shift-
        /// </summary>
        public double Shift_X { get; set; }// 
        /// <summary>
        /// 411 0x019B Float64 Bitmask 2 – #15;
        /// Applied shift due to optimized position and tracking(in logical units).
        /// TEM: image beam shift; STEM: beam shift-
        /// </summary>
        public double Shift_Y { get; set; }// 
        /// <summary>
        /// 419 0x01A3 Float64 Bitmask 2 – #16;
        /// Camera or dose fraction exposure time
        /// </summary>
        public double Integration_time { get; set; }// 
        /// <summary>
        /// 427 0x01AB Int32 Bitmask 2 – #17 -
        /// </summary>
        public int Binning_Width { get; set; }// 
        /// <summary>
        /// 431 0x01AF Int32 Bitmask 2 – #18 -
        /// </summary>
        public int Binning_Height { get; set; }// 


        //カメラ
        /// <summary>
        /// 435 0x01B3 16 chars Bitmask 2 – #19;
        /// TEM: Name of the camera; STEM imaging: <empty>
        /// </summary>
        public string Camera_name { get; set; }//

        /// <summary>
        /// 451 0x01C3 Int32 Bitmask 2 – #20 -
        /// </summary>
        public int Readout_area_left { get; set; }// 
        /// <summary>
        /// 455 0x01C7 Int32 Bitmask 2 – #21 -
        /// </summary>
        public int Readout_area_top { get; set; }// 
        /// <summary>
        /// 459 0x01CB Int32 Bitmask 2 – #22 -
        /// </summary>
        public int Readout_area_right { get; set; }// 
        /// <summary>
        /// 463 0x01CF Int32 Bitmask 2 – #23 -
        /// </summary>
        public int Readout_area_bottom { get; set; }// 
        /// <summary>
        /// 467 0x01D3 Bool Bitmask 2 – #24. -
        /// </summary>
        public bool Ceta_noise_reduction { get; set; }// 
        /// <summary>
        /// 468 0x01D4 Int32 Bitmask 2 – #25. Number of frames summed for dynamic range
        /// </summary>
        public int Ceta_frames_summed { get; set; }// 
        /// <summary>
        /// 472 0x01D8 Bool Bitmask 2 – #26. -
        /// </summary>
        public bool Direct_detector_electron_counting { get; set; }// 
        /// <summary>
        /// 473 0x01D9 Bool Bitmask 2 – #27 -
        /// </summary>
        public bool Direct_detector_align_frames { get; set; }// 
        /// <summary>
        ///  474 0x01DA Int32 Bitmask 2 – #28 -
        /// </summary>
        public int Camera_param_reserved_0 { get; set; }// 
        /// <summary>
        /// 478 0x01DE Int32 Bitmask 2 – #29 -
        /// </summary>
        public int Camera_param_reserved_1 { get; set; }// 
        /// <summary>
        /// 482 0x01E2 Int32 Bitmask 2 – #30 -
        /// </summary>
        public int Camera_param_reserved_2 { get; set; }// 
        /// <summary>
        /// 486 0x01E6 Int32 Bitmask 2 – #31 -
        /// </summary>
        public int Camera_param_reserved_3 { get; set; }// 
        /// <summary>
        /// 490 0x01EA UInt32 NA Individual bits indicate which metadata fields are set.
        /// </summary>
        public uint Bitmask_3 { get; set; }// 
        /// <summary>
        /// 494 0x01EE Int32 Bitmask 3 – #0 -
        /// </summary>
        public int Camera_param_reserved_4 { get; set; }// 
        /// <summary>
        /// 498 0x01F2 Int32 Bitmask 3 – #1 -
        /// </summary>
        public int Camera_param_reserved_5 { get; set; }// 
        /// <summary>
        /// 502 0x01F6 Int32 Bitmask 3 – #2 -
        /// </summary>
        public int Camera_param_reserved_6 { get; set; }// 
        /// <summary>
        /// 506 0x01FA Int32 Bitmask 3 – #3 -
        /// </summary>
        public int Camera_param_reserved_7 { get; set; }// 
        /// <summary>
        /// 519 0x0207 16 chars Bitmask 3 – #7 -
        /// </summary>
        public string STEM_Detector_name { get; set; }// 
        /// <summary>
        /// 535 0x0217 Float64 Bitmask 3 – #8 -
        /// </summary>
        public double Gain { get; set; }// 
        /// <summary>
        /// 543 0x021F Float64 Bitmask 3 – #9 -
        /// </summary>
        public double Offset { get; set; }// 
        /// <summary>
        /// 551 0x0227 Int32 Bitmask 3 – #10 -
        /// </summary>
        public int STEM_param_reserved_0 { get; set; }// 
        /// <summary>
        /// 555 0x022B Int32 Bitmask 3 – #11 -
        /// </summary>
        public int STEM_param_reserved_1 { get; set; }// 
        /// <summary>
        /// 559 0x022F Int32 Bitmask 3 – #12 -
        /// </summary>
        public int STEM_param_reserved_2 { get; set; }// 
        /// <summary>
        /// 563 0x0233 Int32 Bitmask 3 – #13 -
        /// </summary>
        public int STEM_param_reserved_3 { get; set; }// 
        /// <summary>
        /// 567 0x0237 Int32 Bitmask 3 – #14 -
        /// </summary>
        public int STEM_param_reserved_4 { get; set; }// 

        //スキャン設定
        /// <summary>
        ///  571 0x023B Float64 Bitmask 3 – #15;
        ///  Dwell time per pixel [sec]
        /// </summary>
        public double Dwell_time { get; set; }//
        /// <summary>
        /// 579 0x0243 Float64 Bitmask 3 – #16;
        /// Frame time [sec] (currently it will not be used)
        /// </summary>
        public double Frame_time { get; set; }// 
        /// <summary>
        /// 587 0x024B Int32 Bitmask 3 – #17 -
        /// </summary>
        public int Scan_size_left { get; set; }// 
        /// <summary>
        ///  591 0x024F Int32 Bitmask 3 – #18 -
        /// </summary>
        public int Scan_size_top { get; set; }//
        /// <summary>
        /// 595 0x0253 Int32 Bitmask 3 – #19 -
        /// </summary>
        public int Scan_size_right { get; set; }// 
        /// <summary>
        /// 599 0x0257 Int32 Bitmask 3 – #20 -
        /// </summary>
        public int Scan_size_bottom { get; set; }// 
        /// <summary>
        /// 603 0x025B Float64 Bitmask 3 – #21 Field of view[m]
        /// </summary>
        public double Full_scan_FOV_X { get; set; }// 
        /// <summary>
        /// 611 0x0263 Float64 Bitmask 3 – #22 -
        /// </summary>
        public double Full_scan_FOV_Y { get; set; }// 


        //EDS元素マップ
        /// <summary>
        /// 619 0x026B 16 chars Bitmask 3 – #23 -
        /// </summary>
        public string Element { get; set; }
        /// <summary>
        /// 635 0x027B Float64 Bitmask 3 – #24 -
        /// </summary>
        public double Energy_interval_lower { get; set; }
        /// <summary>
        /// 643 0x0283 Float64 Bitmask 3 – #25 -
        /// </summary>
        public double Energy_interval_higher { get; set; }
        /// <summary>
        /// 651 0x028B Int32 Bitmask 3 – #26 -
        /// </summary>
        public int Method { get; set; }


        //ドーズ 
        /// <summary>
        /// 655 0x028F Bool Bitmask 3 – #27 -
        /// </summary>
        bool Is_dose_fraction { get; set; }
        /// <summary>
        /// 656 0x0290 Int32 Bitmask 3 – #28 -
        /// </summary>
        int Fraction_number { get; set; }
        /// <summary>
        /// 660 0x0294 Int32 Bitmask 3 – #29 -
        /// </summary>
        int Start_frame { get; set; }
        /// <summary>
        /// 664 0x0298 Int32 Bitmask 3 – #30 -
        /// </summary>
        int End_frame { get; set; }

        //再構築
        /// <summary>
        /// 668 0x029C 80 chars Bitmask 3 – #31 -
        /// </summary>
        string Input_stack_filename { get; set; }

        /// <summary>
        /// 748 0x02EC UInt32 NA. Individual bits indicate which metadata fields are set.
        /// </summary>
        uint Bitmask_4 { get; set; }
        /// <summary>
        /// 752 0x02F0 Float64 Bitmask 4 – #0
        /// Alpha tilt max
        /// </summary>
        double Alpha_tilt_min { get; set; }

        /// <summary>
        /// 760 0x02F8 Float64 Bitmask 4 – #1
        /// </summary>
        double Alpha_tilt_max { get; set; }
        #endregion

        public List<List<double>> Images { get; set; }

        public class Property
        {
            public double AccVoltage { get; set; }
            public double PixelSizeInMicron { get; set; }
            public PixelUnitEnum PixelUnit { get; set; }
            public double PixelScale { get; set; }

            public Property(double accVoltage, double pixelSizeInMicron, double pixelScale, PixelUnitEnum pixelUnit)
            {
                AccVoltage = accVoltage;
                PixelSizeInMicron = pixelSizeInMicron;
                PixelScale = pixelScale;
                PixelUnit = pixelUnit;
            }
        }

        public MRC(string filename)
        {
            using var br = new BinaryReader(new FileStream(filename, FileMode.Open, FileAccess.Read));

            #region 基本ヘッダ
            NX = br.ReadInt32();//position: 0
            NY = br.ReadInt32();//4
            NZ = br.ReadInt32();//8
            MODE = br.ReadInt32();//12
            NXSTART = br.ReadInt32();//16
            NYSTART = br.ReadInt32();//20
            NZSTART = br.ReadInt32();//24
            MX = br.ReadInt32();//28
            MY = br.ReadInt32();//32
            MZ = br.ReadInt32();//36
            CELLA = new double[3];
            CELLA[0] = br.ReadSingle();//40
            CELLA[1] = br.ReadSingle();//44
            CELLA[2] = br.ReadSingle();//48
            CELLB = new double[3];
            CELLB[0] = br.ReadSingle();//52
            CELLB[1] = br.ReadSingle();//56
            CELLB[2] = br.ReadSingle();//60

            MAPC = br.ReadInt32();//64
            MAPR = br.ReadInt32();//68
            MAPS = br.ReadInt32();//72

            DMIN = br.ReadSingle();//76
            DMAX = br.ReadSingle();//80
            DMEAN = br.ReadSingle();//84

            ISPG = br.ReadInt32();//88

            NSYMBT = br.ReadInt32();//92

            br.BaseStream.Position = 104;
            EXTTYP = new string(br.ReadChars(4));//104

            NVERSION = br.ReadInt32();//108

            br.BaseStream.Position = 196;
            ORIGIN = new double[3];
            ORIGIN[0] = br.ReadSingle();//196
            ORIGIN[1] = br.ReadSingle();//200
            ORIGIN[2] = br.ReadSingle();//204

            MAP = new string(br.ReadChars(4));//208

            MACHST = new string(br.ReadChars(4));//212

            RMS = br.ReadSingle();//216

            NLABEL = br.ReadInt32();//220

            LABEL = new string(br.ReadChars(800)).TrimEnd(new []{ '\0' });//224

            #endregion

            #region FEI1ヘッダ
            if (EXTTYP == "FEI1")
            {
                //イメージ、装置、アプリケーション
                br.BaseStream.Position = 1024 + 0;
                Metadata_size = br.ReadInt32();//0
                Metadata_version = br.ReadInt32();//4
                Bitmask_1 = br.ReadUInt32();//8
                Timestamp = br.ReadDouble();//12
                Microscope_type = new string(br.ReadChars(16));//20
                Application = new string(br.ReadChars(16));//52
                Application_version = new string(br.ReadChars(16));//68

                //電子銃
                br.BaseStream.Position = 1024 + 84;
                HT = br.ReadDouble();//84
                Dose = br.ReadDouble();//92

                //ステージ
                br.BaseStream.Position = 1024 + 100;
                Alpha_tilt = br.ReadDouble();//100
                Beta_tilt = br.ReadDouble();//108
                X_Stage = br.ReadDouble();//116
                Y_Stage = br.ReadDouble();//124
                Z_Stage = br.ReadDouble();//132
                Tilt_axis_angle = br.ReadDouble();//140
                Dual_axis_rotation = br.ReadDouble();//148

                //ピクセルサイズ
                Pixel_size_X = br.ReadDouble();//156
                Pixel_size_Y = br.ReadDouble();//164

                //光学系
                br.BaseStream.Position = 1024 + 220;
                Defocus = br.ReadDouble();//220
                STEM_Defocus = br.ReadDouble();//228
                Applied_defocus = br.ReadDouble();//236
                Instrument_mode = br.ReadInt32();//244
                Projection_mode = br.ReadInt32();//248
                Objective_lens_mode = new string(br.ReadChars(16));//252
                High_magnification_mode = new string(br.ReadChars(16));//268
                Probe_mode = br.ReadInt32();//284
                EFTEM_On = br.ReadBoolean();//288
                Magnification = br.ReadDouble();//289
                Bitmask_2 = br.ReadUInt32();//297
                Camera_length = br.ReadDouble();//301
                Spot_index = br.ReadInt32(); //309
                Illuminated_area = br.ReadDouble();//313
                Intensity = br.ReadDouble();//321
                Convergence_angle = br.ReadDouble();//329
                Illumination_mode = new string(br.ReadChars(16));//337
                Wide_convergence_angle_range = br.ReadBoolean();//353

                //EFTEM イメージ
                br.BaseStream.Position = 1024 + 354;
                Slit_inserted = br.ReadBoolean();//354
                Slit_width = br.ReadDouble();//355
                Acceleration_voltage_offset = br.ReadDouble();//363
                Drift_tube_voltage = br.ReadDouble();//371
                Energy_shift = br.ReadDouble();//379
                Shift_offset_X = br.ReadDouble();//387
                Shift_offset_Y = br.ReadDouble();//395
                Shift_X = br.ReadDouble();//403
                Shift_Y = br.ReadDouble();//411
                Integration_time = br.ReadDouble();//419
                Binning_Width = br.ReadInt32();//427
                Binning_Height = br.ReadInt32();//431

                //カメラ
                br.BaseStream.Position = 1024 + 435;
                Camera_name = new string(br.ReadChars(16));//435
                Readout_area_left = br.ReadInt32();//451
                Readout_area_top = br.ReadInt32();//455
                Readout_area_right = br.ReadInt32();//459
                Readout_area_bottom = br.ReadInt32();//463
                Ceta_noise_reduction = br.ReadBoolean();//467
                Ceta_frames_summed = br.ReadInt32();//468
                Direct_detector_electron_counting = br.ReadBoolean();//472
                Direct_detector_align_frames = br.ReadBoolean();//473
                Camera_param_reserved_0 = br.ReadInt32();//474
                Camera_param_reserved_1 = br.ReadInt32();//478
                Camera_param_reserved_2 = br.ReadInt32();//482
                Camera_param_reserved_3 = br.ReadInt32();//486
                Bitmask_3 = br.ReadUInt32();//490
                Camera_param_reserved_4 = br.ReadInt32();//494
                Camera_param_reserved_5 = br.ReadInt32();//498
                Camera_param_reserved_6 = br.ReadInt32();//502
                Camera_param_reserved_7 = br.ReadInt32();//506

                //STEM 
                br.BaseStream.Position = 1024 + 519;
                STEM_Detector_name = new string(br.ReadChars(16));//519
                Gain = br.ReadDouble();//535
                Offset = br.ReadDouble();//543
                STEM_param_reserved_0 = br.ReadInt32();//551
                STEM_param_reserved_1 = br.ReadInt32();//555
                STEM_param_reserved_2 = br.ReadInt32();//559
                STEM_param_reserved_3 = br.ReadInt32();//563
                STEM_param_reserved_4 = br.ReadInt32();//567

                //スキャン設定
                br.BaseStream.Position = 1024 + 571;
                Dwell_time = br.ReadDouble();//571
                Frame_time = br.ReadDouble();//579
                Scan_size_left = br.ReadInt32();//587
                Scan_size_top = br.ReadInt32();//591
                Scan_size_right = br.ReadInt32();//595
                Scan_size_bottom = br.ReadInt32();//599
                Full_scan_FOV_X = br.ReadDouble();//603
                Full_scan_FOV_Y = br.ReadDouble();//611

                //EDS元素マップ
                br.BaseStream.Position = 1024 + 619;
                Element = new string(br.ReadChars(16));//619
                Energy_interval_lower = br.ReadDouble();//635
                Energy_interval_higher = br.ReadDouble();//643
                Method = br.ReadInt32();//651

                //ドーズ
                br.BaseStream.Position = 1024 + 655;
                Is_dose_fraction = br.ReadBoolean();//655
                Fraction_number = br.ReadInt32();//656
                Start_frame = br.ReadInt32();//660
                End_frame = br.ReadInt32();//664

                //再構築
                br.BaseStream.Position = 1024 + 668;
                Input_stack_filename = new string(br.ReadChars(80));//668
                Bitmask_4 = br.ReadUInt32();//748
                Alpha_tilt_min = br.ReadDouble();//752
                Alpha_tilt_max = br.ReadDouble();//760

                br.BaseStream.Position = 1024 + NSYMBT;// Metadata_size;
            }
            #endregion

            //ここから画像を読み込み
            if (NX * NY * NZ == 0)
                Images = null;
            else
            {
                var read = MODE switch
                {
                    0 => new Func<double>(() => (double)br.ReadByte() - 128),
                    1 => new Func<double>(() => (double)br.ReadInt16()),
                    2 => new Func<double>(() => (double)br.ReadSingle()),
                    6 => new Func<double>(() => (double)br.ReadUInt16()),
                    _ => new Func<double>(() => 0.0)
                };
                Images = new List<List<double>>();
                for (int z = 0; z < NZ; z++)
                {
                    Images.Add(new List<double>());
                    for (int y = 0; y < NY; y++)
                        for (int x = 0; x < NX; x++)
                            Images[z].Add(read());
                }

            }

        }

    }
}

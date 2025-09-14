using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Crystallography;
public static class ImageIO
{
    public static string[] ListOfExtension =
    [
            "img",
            "stl",
            "ccd",
            "ipf",
            "ipa",
            "0???",
            "gel",
            "osc",
            "mar*",
            "mccd",
            "his",
            "h5",
            "dm?",
            "raw",
            "bmp",
            "jpg",
            "tif",
            "tiff",
            "png",
            "smv",
            "mrc",
            "nxs",
        ];

    public static string FilterString
    {
        get
        {
            var filterString = "FujiBAS2000/2500; R-AXIS4/5; ITEX; Bruker CCD; IP Display; IPAimage; Fuji FDL; Rayonix; Marresearch; Perkin Elmer; ADSC; RadIcon; general image |";

            for (int i = 0; i < ListOfExtension.Length; i++)
                if (i < ListOfExtension.Length - 1)
                    filterString += "*." + ListOfExtension[i] + ";";
                else
                    filterString += "*." + ListOfExtension[i];

            return filterString;
        }
    }

    private static readonly char[] separator = ['='];

    public static bool IsReadable(string _ext)
    {
        var ext = _ext.StartsWith('.') ? _ext[1..] : _ext;
        return ListOfExtension.Contains(ext.ToLower()) || ext.StartsWith('0') || ext.StartsWith("mar") || ext.StartsWith("dm");
    }

    #region BinaryReaderから読み込んで整数や実数に変換
    public static int convertToInt(BinaryReader br)
    {
        var b = new byte[4];
        b[3] = br.ReadByte(); b[2] = br.ReadByte(); b[1] = br.ReadByte(); b[0] = br.ReadByte();
        return BitConverter.ToInt32(b, 0);
    }

    public static float convertToSingle(BinaryReader br)
    {
        var b = new byte[4];
        b[3] = br.ReadByte(); b[2] = br.ReadByte(); b[1] = br.ReadByte(); b[0] = br.ReadByte();
        return BitConverter.ToSingle(b, 0);
    }

    public static void SetBytePosition(string str, ref BinaryReader br, int count)
    {
        br.Close();
        br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.Read));
        br.ReadBytes(count);
    }
    #endregion

    #region 画像形式を判定する関数群
    public static bool IsRAxisImage(string fileName)
    {
        var br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
        br.BaseStream.Position = 0;
        var temp = new string(br.ReadChars(6));
        br.Close();
        return temp == "R-AXIS" || temp == "ipdsc\0";
    }

    public static bool IsRintRapid2UnrollImage(string fileName)
    {
        var br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
        br.BaseStream.Position = 0;
        var temp = new string(br.ReadChars(46));
        br.Close();
        return temp == "{\nHEADER_BYTES=17408;\nBYTE_ORDER=little_endian";
    }
    public static bool IsITEXImage(string fileName)
    {
        var br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
        br.BaseStream.Position = 0;
        var temp = new string(br.ReadChars(2));
        br.Close();
        return temp == "IM";
    }

    public static bool IsADSCImage(string fileName)
    {
        var br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
        br.BaseStream.Position = 0;
        var temp = new string(br.ReadChars(16));
        br.Close();
        return temp == "{\nHEADER_BYTES= ";
    }

    public static bool IsADXVImage(string fileName)
    {
        var br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
        br.BaseStream.Position = 0;
        var temp = new string(br.ReadChars(46));
        br.Close();
        return temp == "{\nHEADER_BYTES=  512;\nCOMMENT=Written by adxv;";
    }

    public static bool IsTiffImage(string fileName)
    {
        var br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
        var temp = new byte[2];
        br.Read(temp, 0, 2);
        br.Close();
        return (temp[0] == 0x49 && temp[1] == 0x49) || (temp[0] == 0x4D && temp[1] == 0x4D);
    }

    public static bool Check_PF_RAW(string fileName)
    {
        //references\ImageExsample\BL18c 柴咲さん　のヘッダ情報を参考
        //最初の4バイトと、次の4バイトは、文字列であり、数値に変換可能で、"0236"と"0052"
        var br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
        var str1 = new string(br.ReadChars(4));
        var str2 = new string(br.ReadChars(4));
        br.Close();
        return str1 == "0236" && str2 == "0052";

    }
    #endregion

    /// <summary>
    /// 指定された file を読み込み、読み込んだ内容はRing.***に保存される。失敗したときは false を返す。flagはノーマライズするかどうか。
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool ReadImage(string str, bool? flag = null)
    {
        str = str.TrimEnd('\n');
        str = str.TrimEnd('\r');

        if (!File.Exists(str))
            return false;

        Ring.Comments = "";
        Ring.SequentialImageIntensities = null;
        Ring.SequentialImageEnergy = null;
        Ring.SequentialImageNames = null;
        Ring.SequentialImagePulsePower = null;
        //Ring.SequentialImagePulsePowerNormarize = false;

        bool result = true;

        string ext = Path.GetExtension(str).TrimStart(['.']);
        if (str.EndsWith("img"))//R-Axis5 or Fuji BAS or Fuji FDL
        {
            if (File.Exists(str[..^3] + "inf"))
            {//Fujiのとき
                var reader = new StreamReader($"{str[..^3]}inf");
                var strList = new List<string>();
                string tempstr;
                while ((tempstr = reader.ReadLine()) != null)
                    strList.Add(tempstr);

                if (strList != null && strList.Count != 0 && strList[0].Contains("BAS_IMAGE_FILE", StringComparison.Ordinal))//BAS2000
                    result = BAS2000or2500(str, [.. strList]);
                else
                {
                    return false;
                }
            }
            else if (File.Exists(str[..^3] + "tem"))
                result = FujiFDL(str);
            else if (IsRAxisImage(str))//R-Axis5
                result = Raxis4(str);
            else if (IsRintRapid2UnrollImage(str))//Rigaku RINT-RAPID II
                result = RintRapid2Unroll(str);
            else if (IsADXVImage(str))
                result = ADXV(str);
            else if (IsITEXImage(str))
                result = ITEX(str);
            else if (IsADSCImage(str))
                result = ADSC(str);
            else if (IsTiffImage(str))
                result = Tiff(str);
            else
                return false;
        }
        else if (str.ToLower().EndsWith("stl"))//Rigaku R-Axis4
            result = Raxis4(str);
        else if (str.ToLower().EndsWith("osc"))//Rigaku R-Axis4_Osc
            result = Raxis4(str);
        else if (Path.GetExtension(str).TrimStart(['.']).StartsWith('0') && IsTiffImage(str))
            result = RayonixSX200(str);
        else if (str.ToLower().EndsWith(".ccd"))//Bruker CCD
            result = Brucker(str);
        else if (str.ToLower().ToLower().EndsWith(".gel"))//gelイメージ
            result = Gel(str);
        else if (str.ToLower().EndsWith(".ipf"))
            result = Ipf(str);
        else if (ext.StartsWith("mar"))
            result = MAR(str);
        else if (str.ToLower().EndsWith(".mccd"))
            result = Mccd(str);
        else if (str.ToLower().EndsWith(".his"))//hisイメージ
        {
            if (IsITEXImage(str))
                result = ITEX(str);
            else
                result = HIS(str);
        }
        else if (str.ToLower().EndsWith("ipa") && str != "ClipBoard.ipa")
            result = IPA(str);
        else if (str.ToLower().EndsWith("bmp") || str.ToLower().EndsWith("jpg") || str.ToLower().EndsWith("png"))
            result = GeneralImage(str);//General Image
        else if (str.ToLower().ToLower().EndsWith("tif") || str.ToLower().ToLower().EndsWith("tiff"))//Tiffイメージ
            result = Tiff(str, flag);
        else if (str.ToLower().EndsWith("h5"))
            result = HDF5(str, flag);
        else if (str.ToLower().EndsWith("dm3") || str.ToLower().EndsWith("dm4"))//Digital micrograph
            result = DM(str);
        else if (str.ToLower().EndsWith("raw"))
            result = RadIcon(str);
        else if (str.ToLower().EndsWith("smv"))//Dexela
            result = SMV(str);
        else if (str.ToLower().EndsWith("mrc"))
            result = MRC(str);
        else if (str.ToLower().EndsWith("nxs"))
            result = NXS(str);
        else
            return false;

        Ring.IntensityOriginal = [.. Ring.Intensity];
        Ring.SrcImgSizeOriginal = new Size(Ring.SrcImgSize.Width, Ring.SrcImgSize.Height);

        return result;
    }

    #region SMV
    private static bool SMV(string filename)
    {
        try
        {
            int headersize = 0;
            using (var sr = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read)))
            {
                if (sr.ReadLine() != "{")
                    return false;
                var str = sr.ReadLine();
                if (!str.StartsWith("HEADER_BYTES="))
                    return false;
                headersize = Convert.ToInt32(str.Split(['=', ';'])[1]);
            }

            using BinaryReader br = new(new FileStream(filename, FileMode.Open, FileAccess.Read));
            var headers = new string(br.ReadChars(headersize)).Split(['{', '}', '\n', ';'], StringSplitOptions.RemoveEmptyEntries);
            var little_endian = headers.First(h => h.StartsWith("BYTE_ORDER=")).Split(['='])[1] == "little_endian";
            var type = headers.First(h => h.StartsWith("TYPE=")).Split(separator)[1];
            var size1 = Convert.ToInt32(headers.First(h => h.StartsWith("SIZE1=")).Split(['='])[1]);
            var size2 = Convert.ToInt32(headers.First(h => h.StartsWith("SIZE2=")).Split(['='])[1]);
            var size3 = Convert.ToInt32(headers.First(h => h.StartsWith("SIZE3=")).Split(['='])[1]);
            var size4 = Convert.ToInt32(headers.First(h => h.StartsWith("SIZE4=")).Split(['='])[1]);

            if (type != "UNSIGNED_SHORT" && type != "float")
                return false;

            int imageWidth = size1, imageHeight = size2;
            int numberOfFrame = size3;
            long length = imageWidth * imageHeight;

            Ring.SequentialImageIntensities = [];
            Ring.SequentialImageNames = [];

            var read = type == "UNSIGNED_SHORT" ? new Func<double>(() => br.ReadUInt16()) : new Func<double>(() => br.ReadSingle());

            for (int j = 0; j < numberOfFrame; j++)
            {
                Ring.SequentialImageIntensities.Add([]);
                br.BaseStream.Position = headersize + j * length * (type == "UNSIGNED_SHORT" ? 2 : 4);
                Ring.SequentialImageIntensities[j] = new double[length];
                for (int i = 0; i < length; i++)
                    Ring.SequentialImageIntensities[j][i] = read();
                Ring.SequentialImageNames.Add(j.ToString("000"));
            }

            if (Ring.Intensity.Length != length)//前回と同じサイズではないとき
                Ring.Intensity = new double[length];
            for (int i = 0; i < length; i++)
                Ring.Intensity[i] = Ring.SequentialImageIntensities[0][i];

            Ring.BitsPerPixels = type == "UNSIGNED_SHORT" ? 2 : 4;

            Ring.SrcImgSize = new Size(imageWidth, imageHeight);
            Ring.ImageType = Ring.ImageTypeEnum.SMV;
            Ring.Comments = "Num. of Frame: " + numberOfFrame.ToString(); ;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region  rawファイル (RadIcon など)

    static readonly Size[] RadIconSize 
        = [new (688, 2064), new(1548, 2064), new(2064, 2236), new(2080, 2238), new(3096, 3100), new(1300, 4608), new(2940, 4608), new(5888, 4608),];
    public static bool RadIcon(string str)
    {
        try
        {
            var fileSize = new FileInfo(str).Length;
            #region RadIcon検出器
            if (RadIconSize.Any(e => e.Width * e.Height * 2 == fileSize))
            {
                Ring.SrcImgSize = RadIconSize.First(e => e.Width * e.Height * 2 == fileSize);
                var length = Ring.SrcImgSize.Width * Ring.SrcImgSize.Height;
                var sameSize = Ring.Intensity.Length == length;

                if (!sameSize)//前回と同じサイズではないとき
                    Ring.Intensity = new double[length];

                using var br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.Read));
                for (int n = 0; n < length; n++)
                {
                    //マイナスの値が入ることを考慮した変更 2024/02/27 辻野さんからのメール参考
                    var val = (short)(256 * br.ReadByte() + br.ReadByte());
                    Ring.Intensity[n] = val;
                }
                br.Close();

                Ring.BitsPerPixels = 16;
                Ring.ImageType = Ring.ImageTypeEnum.RadIcon;
                Ring.Comments = "";

                return true;
            }
            #endregion

            #region 2020年に導入された、PFのRAWファイル形式 (references\ImageExsample\BL18c 柴咲さん  を参考せよ)
            else if (Check_PF_RAW(str))
            {
                var br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.Read));
                var offsetToFirstImage = new string(br.ReadChars(4));
                var gapBetweenImages = new string(br.ReadChars(4));

                //保存ソフト型名
                var software = new string(br.ReadChars(16));
                //センサ型式
                var sensor = new string(br.ReadChars(20));
                //画像情報 予備
                var temp = new string(br.ReadChars(2));
                //画像情報 画像の補正状態
                var correction = new string(br.ReadChars(1));
                //画像情報 画像の種別
                var type = new string(br.ReadChars(1));
                //画像情報 Y方向ビニング
                var binning_Y = new string(br.ReadChars(1));
                //画像情報 X方向ビニング
                var binning_X = new string(br.ReadChars(1));
                //画像情報 読み出し方向
                var direction = new string(br.ReadChars(1));
                //画像情報 有効ビット幅
                var bitLength = new string(br.ReadChars(1));

                //画像取得時の積算回数	
                var accumulation_number = new string(br.ReadChars(4)).ToInt();

                //露光時間(μS) ExposureTime
                var exposure_time = new string(br.ReadChars(8)).ToInt();
                //間隔(frame)
                var frame = new string(br.ReadChars(4)).ToInt();
                //格納画像枚数 Number of stored images
                var num_of_stored_images = new string(br.ReadChars(4)).ToInt();
                //使用画素欠陥データ ファイル名
                var filename_pixel_defects = Encoding.UTF8.GetString(br.ReadBytes(32));
                //使用暗電流データ ファイル名
                var filename_dark = Encoding.UTF8.GetString(br.ReadBytes(32));
                //使用感度補正データ ファイル名
                var filename_correction = Encoding.UTF8.GetString(br.ReadBytes(32));

                Ring.SequentialImageIntensities = [];
                Ring.SequentialImageNames = [];

                var read = bitLength == "7" ? new Func<double>(() => br.ReadByte()) : new Func<double>(() => br.ReadInt16());

                //画像サイズ (H)
                var width = new string(br.ReadChars(8)).ToInt();
                //画像サイズ (V)
                var height = new string(br.ReadChars(8)).ToInt();

                //画像読み込みループ
                for (int i = 0; i < num_of_stored_images; i++)
                {
                    var num = new string(br.ReadChars(4)).ToInt();
                    var time = new string(br.ReadChars(16));
                    var option = Encoding.UTF8.GetString(br.ReadBytes(32));

                    Ring.SequentialImageNames.Add(num.ToString());
                    Ring.SequentialImageIntensities.Add([]);
                    Ring.SequentialImageIntensities[i] = new double[width * height];
                    for (int y = 0; y < height; y++)
                        for (int x = 0; x < width; x++)
                            Ring.SequentialImageIntensities[i][y * width + x] = read();
                }

                if (Ring.Intensity.Length != Ring.SequentialImageIntensities[0].Length)//前回と同じサイズではないとき
                    Ring.Intensity = new double[Ring.SequentialImageIntensities[0].Length];
                for (int i = 0; i < Ring.SequentialImageIntensities[0].Length; i++)
                    Ring.Intensity[i] = Ring.SequentialImageIntensities[0][i];

                br.Close();

                Ring.SrcImgSize = new Size(width, height);
                Ring.ImageType = Ring.ImageTypeEnum.RadIconPF;
                Ring.Comments =
                    "Software: " + software.TrimEnd() + "\r\n" +
                    "Sensor: " + sensor.TrimEnd() + "\r\n" +
                    "Num. of stored images: " + num_of_stored_images.ToString() + "\r\n" +
                    "Exposure time (us): " + exposure_time.ToString() + " ms.";

                return true;
            }
            #endregion

            return false;


        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
    }
    #endregion

    #region DigitalMicrograph
    private static bool DM(string str)
    {
        try
        {
            Ring.Comments = "";
            var t = new DigitalMicrograph.Loader(str);

            double pixelScale = (float)t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Calibrations"].Tag["Dimension"].Tag["0"].Tag["Scale"].Values[0];

            double accVol = 200000;
            var test = t.Tag["ImageList"].Tag["1"].Tag["ImageTags"].Tag["Microscope Info"].Tag["Voltage"].Values[0];
            if (test is float accF)
                accVol = (double)accF;
            else if (test is double accD)
                accVol = accD;

            var pixelSize = 0.1;
            //CCDカメラの場合
            if (t.Tag["ImageList"].Tag["1"].Tag["ImageTags"].Tag.TryGetValue("Acquisition", out DigitalMicrograph.TagInfo value1))
                pixelSize = (float)(value1.Tag["Device"].Tag["CCD"].Tag["Pixel Size (um)"].Values[0]);
            //DigiScanの場合
            else if (t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Calibrations"].Tag.TryGetValue("Dimension", out DigitalMicrograph.TagInfo value2))
                pixelSize = (float)value2.Tag["0"].Tag["Scale"].Values[0];

            var temp = t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Calibrations"].Tag["Dimension"].Tag["0"].Tag["Units"].Values.Select(c => (ushort)c).ToArray();
            var units = new string([.. temp.Select(c => (char)c)]);
            var unit = LengthUnitEnum.None;
            if (units == "1/nm")
                unit = LengthUnitEnum.NanoMeterInverse;
            else if (units == "µm")
                unit = LengthUnitEnum.MicroMeter;
            else if (units == "nm")
                unit = LengthUnitEnum.NanoMeter;

            Ring.DigitalMicrographProperty = new DigitalMicrograph.Property(accVol, pixelSize, pixelScale, unit);

            int imageWidth = (int)(uint)t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Dimensions"].Tag["0"].Values[0];
            int imageHeight = (int)(uint)t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Dimensions"].Tag["1"].Values[0];
            Ring.SrcImgSize = new Size(imageWidth, imageHeight);

            dynamic intensity;
            if (t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Data"].Values != null && t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Data"].Values.Length != 0)
            {
                object o = t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Data"].Values[0];
                if (o.GetType() == typeof(float))
                    intensity = t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Data"].Values.Select(v => (float)v).ToArray();
                else if (o.GetType() == typeof(uint))
                    intensity = t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Data"].Values.Select(v => (uint)v).ToArray();
                else if (o.GetType() == typeof(int))
                    intensity = t.Tag["ImageList"].Tag["1"].Tag["ImageData"].Tag["Data"].Values.Select(v => (int)v).ToArray();
                else
                    return false;

                if (Ring.Intensity.Length != imageWidth * imageHeight)//前回と同じサイズではないとき
                    Ring.Intensity = new double[imageHeight * imageWidth];
                for (int i = 0; i < imageHeight * imageWidth; i++)
                    Ring.Intensity[i] = intensity[i];
            }
            //Ring.BitsPerPixels = t.BitsPerSampleGray;
            Ring.ImageType = Ring.ImageTypeEnum.DM;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region MRC
    private static bool MRC(string str)
    {
        try
        {
            var mrc = new MRC(str);
            Ring.SrcImgSize = new Size(mrc.NX, mrc.NY);
            Ring.Intensity = [.. mrc.Images[0]];
            Ring.MRC = mrc;
            Ring.ImageType = Ring.ImageTypeEnum.MRC;

            return true;
        }
        catch
        {
            return false;
        }

    }
    #endregion

    #region MAR
    public static bool MAR(string str)
    {
        try
        {
            var img = Array.Empty<uint>();
            var br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.Read));
            int n = 0;
            int ver = 0;
            int imageWidth = 0, imageHeight = 0;
            while (n < 10000)
            {
                br.BaseStream.Position = n++;
                try
                {
                    string s = new(br.ReadChars(17));
                    if (s.StartsWith("CCP4 packed image"))
                    {
                        if (br.ReadChar() == ',')
                            ver = 1;
                        else
                            ver = 2;
                        break;
                    }
                }
                catch { }
            }
            while (br.ReadChar() != 'X')
            { }
            br.ReadChars(2);
            imageWidth = Convert.ToInt32(new string(br.ReadChars(4)));
            br.ReadChars(5);
            imageHeight = Convert.ToInt32(new string(br.ReadChars(4)));
            br.ReadChar();

            if (ver == 1)
                img = CCP4.unpack(br, imageWidth, imageHeight);
            else
                img = CCP4.v2unpack(br, imageWidth, imageHeight);

            br.Close();
            int length = imageWidth * imageHeight;

            if (Ring.Intensity.Length != length)//前回と同じサイズではないとき
                Ring.Intensity = new double[length];
            n = 0;
            for (int y = 0; y < imageHeight; y++)
                for (int x = 0; x < imageWidth; x++)
                    Ring.Intensity[n++] = img[(imageHeight - y - 1) * imageWidth + x];
         
            Ring.SrcImgSize = new Size(imageWidth, imageHeight);
            Ring.ImageType = Ring.ImageTypeEnum.MAR;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region RayonixSX200

    public static bool RayonixSX200(string str)
    {
        try
        {
            Tiff.Loader t = new(str);
            if (t.IsGray == false || t.NumberOfFrames != 1)
                return false;
            Ring.Comments = "";
            Ring.SrcImgSize = new Size(t.ImageWidth, t.ImageLength);

            Ring.Intensity = [.. t.Images[0].Value]; 

            Ring.BitsPerPixels = t.BitsPerSampleGray;
            Ring.ImageType = Ring.ImageTypeEnum.RayonixSX200;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }

    #endregion

    #region GEL
    public static bool Gel(string str)
    {
        try
        {
            Tiff.Loader t = new(str);
            if (t.IsGray == false || t.NumberOfFrames != 1)
                return false;
            Ring.Comments = "";
            Ring.SrcImgSize = new Size(t.ImageWidth, t.ImageLength);

            if (Ring.Intensity.Length != t.ImageWidth * t.ImageLength)//前回と同じサイズではないとき
                Ring.Intensity = new double[t.ImageWidth * t.ImageLength];
            for (int i = 0; i < t.ImageLength * t.ImageWidth; i++)
                Ring.Intensity[i] = t.Images[0].Value[i] * t.Images[0].MDScaleFactor;

            Ring.BitsPerPixels = t.BitsPerSampleGray;
            Ring.ImageType = Ring.ImageTypeEnum.MCCD;
            //Ring.Scale = t.MDScaleFactor;
            Ring.Comments = t.Images[0].ImageDescription;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region MCCD
    public static bool Mccd(string str)
    {
        /*  try
          {
              Tiff.Loader t = new Tiff.Loader(str);

              if (t.IsGray == false)
                  return false;
              Ring.Coments = "";
              Ring.SrcImgSize = new Size(t.ImageWidth, t.ImageLength);
              if (Ring.Intensity.Count != t.ImageWidth * t.ImageLength)//前回と同じサイズではないとき
              {
                  Ring.Intensity.Clear();
                  for (int i = 0; i < t.ImageLength * t.ImageWidth; i++)
                      Ring.Intensity.Add(t.ValueGray[i] * t.MDScaleFactor);
              }
              else
              {
                  for (int i = 0; i < t.ImageLength * t.ImageWidth; i++)
                      Ring.Intensity[i] = t.ValueGray[i] * t.MDScaleFactor;
              }

              Ring.BitsPerPixels = t.BitsPerSampleGray;
              Ring.ImageType = Ring.ImageTypeEnum.MCCD;
              //Ring.Scale = t.MDScaleFactor;
              Ring.Coments = t.ImageDescription;
          }
          */

        try
        {
            var t = new Tiff.Loader(str);
            if (t.IsGray == false || t.NumberOfFrames != 1)
                return false;
            Ring.Comments = "";
            Ring.SrcImgSize = new Size(t.ImageWidth, t.ImageLength);

            Ring.Intensity = [.. t.Images[0].Value];

            Ring.BitsPerPixels = t.BitsPerSampleGray;
            Ring.ImageType = Ring.ImageTypeEnum.MCCD;
            //Ring.Scale = t.MDScaleFactor;
            Ring.Comments = t.Images[0].ImageDescription;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion


    public static bool NXS(string filename)
    {
        var hdf = new HDF(filename);

        var header = "/entry/instrument/detector/";

        var data = hdf.GetDataset(header + "data");

        int num = (int)data.Space.Dimensions[0], height = (int)data.Space.Dimensions[1], width = (int)data.Space.Dimensions[2];

        if(num==1)
            Ring.Intensity = [.. data.Read<int[]>().Select(e => (double)e)];
        else
        {
            var src = Array.Empty<double>();
            if(data.Type.Size==4)
                src = [.. data.Read<int[]>().Select(e => (double)e)];
            else if (data.Type.Size == 2)
                src = [.. data.Read<short[]>().Select(e => (double)e)];

            Ring.SequentialImageIntensities = [];
            for(int i=0; i< num; i++)
                Ring.SequentialImageIntensities.Add( src[(i*width*height)..((i+1)*(width*height))] );

            Ring.SequentialImageNames = [..Enumerable.Range(1,num+1).Select(e=>e.ToString("000"))];
          

            Ring.Intensity = Ring.SequentialImageIntensities[0];
        }



        // double pixSizeX = hdf.GetDataset(header + "x_pixel_size").Read<double>(), pixSizeY = hdf.GetDataset(header + "y_pixel_size").Read<double>();

        Ring.SrcImgSize = new Size(width, height);
        Ring.ImageType = Ring.ImageTypeEnum.NXS;

        #region コメント情報の取得

        (string Name, Type Type)[] comments = [
            ("acquisition_mode", typeof(string)),
            ("bit_depth_readout", typeof(int)),
            ("calibration_date", typeof(string)),
            ("countrate_correction_applied", typeof(byte)),
            ("description", typeof(string)),
            ("detector_readout_time", typeof(double)),
            ("flatfield_applied", typeof(byte)),
            ("layout", typeof(string)),
            ("local_name", typeof(string)),
            ("pixelmask_applied", typeof(byte)),
            ("saturation_value", typeof(int)),
            ("sensor_material", typeof(string)),
            ("sensor_thickness", typeof(float)),
            ("sequence_number", typeof(int[])),
            ("trigger_dead_time", typeof(double)),
            ("trigger_delay_time", typeof(double)),
            ("type", typeof(string)),
            ("x_pixel_size", typeof(double)),
            ("y_pixel_size", typeof(double)),
        ];
        var sb = new StringBuilder();
        foreach (var (name, type) in comments)
        {
            var dataset = hdf.GetDataset(header + name);

            if (dataset != null && dataset.Space.Dimensions.Length ==1)
            {
                if (type == typeof(string))
                    sb.AppendLine($"{name}: {dataset.ReadStr()}");
                else if (type == typeof(int))
                    sb.AppendLine($"{name}: {dataset.Read<int>()}");
                else if (type == typeof(double))
                    sb.AppendLine($"{name}: {dataset.Read<double>()}");
                else if (type == typeof(float))
                    sb.AppendLine($"{name}: {dataset.Read<float>()}");
                else if (type == typeof(long))
                    sb.AppendLine($"{name}: {dataset.Read<long>()}");
                else if (type == typeof(byte))
                    sb.AppendLine($"{name}: {dataset.Read<byte>()}");
            }
        }
        Ring.Comments = sb.ToString();
        #endregion

        return true;
    }


    #region HDF5
    public static bool HDF5(string str, bool? normarize = null)
    {

        

        return false;

        #region 以下はPilutus時代のコードだが、もう需要がないのでお蔵入り。
        //try
        //{
        //    Ring.IP ??= new IntegralProperty();

        //    var hdf = new HDF(str);

        //    var groupID2name = hdf.Paths.Where(g => g.Depth == 0 && !g.Name.Contains("file_info")).First().Name;


        //    //パルスパワー読込
        //    (float[] dataPulsePower, _) = hdf.GetValue1<float>(groupID2name + "/event_info/bl_3/oh_2/bm_2_pulse_energy_in_joule");
        //    if (dataPulsePower != null)
        //    {
        //        Ring.SequentialImagePulsePower = [];
        //        for (int i = 0; i < dataPulsePower.Length; i++)
        //            Ring.SequentialImagePulsePower.Add(dataPulsePower[i]);
        //    }
        //    else//佐野さんから依頼された検出器
        //        normarize = false;

        //    //X線エネルギー読込
        //    (float[] dataPhotonEnergy, _) = hdf.GetValue1<float>(groupID2name + "/event_info/bl_3/oh_2/photon_energy_in_eV");
        //    if (dataPhotonEnergy != null)
        //    {
        //        Ring.SequentialImageEnergy = [];
        //        for (int i = 0; i < dataPhotonEnergy.Length; i++)
        //            Ring.SequentialImageEnergy.Add(dataPhotonEnergy[i]);
        //    }
        //    //左側イメージ検出器、右側イメージ検出器、エネルギースペクトルがどの検出器番号に対応するかを判定
        //    int leftDetector = -1, rightDetector = -1, energySpectrum = -1;

        //    for (int i = 1; i < 4; i++)
        //    {
        //        (int detectorType, _) = hdf.GetValue0<int>($"{groupID2name}/detector_2d_{i}/detector_info/detector_type");

        //        if (detectorType == 1 || detectorType == 0)//イメージ検出器の場合
        //        {
        //            //detector_2d_1　と _2の位置関係を調べる

        //            (float[] dataCoordinate, _) = hdf.GetValue1<float>($"{groupID2name}/detector_2d_{i}/detector_info/detector_coordinate_in_micro_meter");

        //            if (dataCoordinate == null)//佐野さんから依頼された検出器
        //            {
        //                leftDetector = 1;
        //                rightDetector = 2;
        //            }
        //            else if (dataCoordinate[0] == 0)
        //                leftDetector = i;
        //            else
        //                rightDetector = i;
        //        }
        //        else if (detectorType == 7)
        //            energySpectrum = i;
        //    }

        //    //ピクセルサイズ読み込み
        //    (float[] dataPixelSize, _) = hdf.GetValue1<float>(groupID2name + "/detector_2d_1/detector_info/pixel_size_in_micro_meter");
        //    if (dataPixelSize != null)
        //    {
        //        Ring.IP.PixSizeX = dataPixelSize[0] * 0.001;
        //        Ring.IP.PixSizeY = dataPixelSize[1] * 0.001;
        //    }
        //    else//佐野さんから依頼された検出器
        //    {
        //        (var data_size, _) = hdf.GetValue1<float>(groupID2name + "/detector_2d_1/detector_info/data_scale(XYZT)");
        //        if (data_size != null)
        //        {
        //            Ring.IP.PixSizeX = data_size[0] * 0.001;
        //            Ring.IP.PixSizeY = data_size[1] * 0.001;
        //        }
        //        else
        //        {
        //            Ring.IP.PixSizeX = Ring.IP.PixSizeY = 0.05;
        //        }
        //    }

        //    //tag番号を調べる
        //    var tag = new List<string>();
        //    foreach (var (Name, Parent, Depth) in hdf.Paths)
        //    {
        //        var tmp = Name.Split(["/"], StringSplitOptions.RemoveEmptyEntries);
        //        if (tmp.Length != 0 && tmp[^1].StartsWith("tag_") && !tag.Contains(tmp[^1]))
        //            tag.Add(tmp[^1]);
        //    }

        //    //各tagの画像を読み込み
        //    Ring.SequentialImageIntensities = [];
        //    Ring.SequentialImageNames = [];
        //    int imageWidth = 1024, imageHeight = 1024;
        //    for (int i = 0; i < tag.Count; i++)
        //    {
        //        (float[][] dataImageLeft, _) = hdf.GetValue2<float>($"{groupID2name}/detector_2d_{leftDetector}/{tag[i]}/detector_data");

        //        (float[][] dataImageRight, _) = hdf.GetValue2<float>($"{groupID2name}/detector_2d_{rightDetector}/{tag[i]}/detector_data");



        //        if (dataImageLeft != null && dataImageRight != null)
        //        {
        //            Ring.SequentialImageIntensities.Add(new double[imageHeight * imageWidth]);
        //            int n = 0;
        //            for (int h = 0; h < imageHeight; h++)
        //            {
        //                for (int w = 0; w < imageWidth / 2; w++)
        //                    Ring.SequentialImageIntensities[i][n++] =dataImageLeft[h][w];
        //                for (int w = 0; w < imageWidth / 2; w++)
        //                    Ring.SequentialImageIntensities[i][n++] = dataImageRight[h][w];
        //            }
        //        }
        //        else if (dataImageLeft != null)
        //        {
        //            imageWidth = 512;
        //            Ring.SequentialImageIntensities.Add(new double[imageHeight * imageWidth]);
        //            int n = 0;
        //            for (int h = 0; h < imageHeight; h++)
        //                for (int w = 0; w < imageWidth; w++)
        //                    Ring.SequentialImageIntensities[i][n++] = dataImageLeft[h][w];
        //        }
        //        else
        //            Ring.SequentialImageIntensities.Add([]);

        //        //強度をノーマライズする場合
        //        normarize ??= MessageBox.Show("Normarize intensities by pulse power?", "HDF file option", MessageBoxButtons.YesNo) == DialogResult.Yes;

        //        if (normarize == true && dataPulsePower[i] > 0)
        //            Ring.SequentialImageIntensities[i] = [.. Ring.SequentialImageIntensities[i].Select(d => d / (double)dataPulsePower[i] / 10000)];

        //        Ring.PulsePowerNormarized = normarize == true;

        //        if (i == 0)
        //        {
        //            Ring.Intensity = new double[imageHeight * imageWidth];
        //            for (int j = 0; j < imageHeight * imageWidth; j++)
        //                Ring.Intensity[j] =Ring.SequentialImageIntensities[0][j];
        //        }

        //        Ring.SequentialImageNames.Add(tag[i].Replace("tag_", ""));
        //    }


        //    Ring.SrcImgSize = new Size(imageWidth, imageHeight);
        //    Ring.ImageType = Ring.ImageTypeEnum.HDF5;

        //    Ring.Comments = "Num. of Frame: " + tag.Count.ToString() + "\r\n";
        //    return true;

        //}
        //catch (Exception)
        //{
        //    MessageBox.Show("Can not open *.h5 file. Some dll files may be not imported properly, or the *.h5 file may be corrupeted");
        //    return false;
        //}
        #endregion
    }
    #endregion

    #region HIS
    public static bool HIS(string str)
    {
        try
        {
            var img = Array.Empty<uint>();
            BinaryReader br = new(new FileStream(str, FileMode.Open, FileAccess.Read));

            int ID = br.ReadUInt16();
            int headerSize = br.ReadUInt16();
            int headerVersion = br.ReadUInt16();
            uint fileSize = br.ReadUInt32();
            int imageHeaderSize = br.ReadUInt16();
            int ULX = br.ReadUInt16();
            int ULY = br.ReadUInt16();
            int BRX = br.ReadUInt16();
            int BRY = br.ReadUInt16();
            int numberOfFrame = br.ReadUInt16();
            int correction = br.ReadUInt16();
            double frameTimeInMicroseconds = br.ReadDouble();//不明
            int frameTimeInMilliseconds = br.ReadUInt16();

            int tag1 = br.ReadUInt16();
            int tag2 = br.ReadUInt16();
            int tag3 = br.ReadUInt16();
            int tag4 = br.ReadUInt16();
            int tag5 = br.ReadUInt16();
            int tag6 = br.ReadUInt16();
            int tag7 = br.ReadUInt16();
            int tag8 = br.ReadUInt16();

            //int numberOfFrame = br.ReadUInt16();
            //int numberOfFrame = br.ReadUInt16();

            int imageWidth = BRX - ULX + 1, imageHeight = BRY - ULY + 1;
            Ring.SequentialImageIntensities = [];
            Ring.SequentialImageNames = [];

            for (int j = 0; j < numberOfFrame; j++)
            {
                Ring.SequentialImageIntensities.Add(new double[imageHeight * imageWidth]);
                br.BaseStream.Position = headerSize + imageHeaderSize + j * imageWidth * imageHeight * 2;
                for (int i = 0; i < imageHeight * imageWidth; i++)
                    Ring.SequentialImageIntensities[j][i] = br.ReadUInt16();
                Ring.SequentialImageNames.Add(j.ToString("000"));
            }

            if (Ring.Intensity.Length != imageWidth * imageHeight)//前回と同じサイズではないとき
                Ring.Intensity = new double[imageWidth * imageHeight];
            for (int i = 0; i < imageHeight * imageWidth; i++)
                Ring.Intensity[i] = Ring.SequentialImageIntensities[0][i];

            br.Close();

            Ring.SrcImgSize = new Size(imageWidth, imageHeight);
            Ring.ImageType = Ring.ImageTypeEnum.HIS;
            Ring.Comments = $"Num. of Frame: {numberOfFrame}\r\nFrame time: {frameTimeInMilliseconds} ms.";
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region ADXV (20221107丹羽さんからの依頼, PILATUSのcbfファイルを、adxvというソフトを介してimgファイルに変換したもの)
    public static bool ADXV(string str)
    {
        try
        {
            var br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.Read));
            var headers = new string(br.ReadChars(512)).Split('\n');

            int imageWidth = Convert.ToInt32(headers[4].Split(['=', ';'])[1]);
            int imageHeight = Convert.ToInt32(headers[5].Split(['=', ';'])[1]);


            var data_length = 0; // 0 は ushort (16bit), 1 は uint (32bit), それ以外は無効
            if (headers.Length > 13 && headers[13] == "TYPE=long_integer;")
                data_length = 1;

            Ring.SrcImgSize = new Size(imageWidth, imageHeight);

            Ring.Comments = "";
            for (int i = 2; i < 13; i++)
                Ring.Comments += headers[i] + "\r\n";

            br.BaseStream.Position = 512;
            int length = imageWidth * imageHeight;

            Ring.Intensity=new double[length];
            if (data_length == 0)
                for (int i = 0; i < length; i++)
                    Ring.Intensity[i]=br.ReadUInt16();
            else
                for (int i = 0; i < length; i++)
                {
                    var val = br.ReadUInt32();
                    if (val > uint.MaxValue / 2)
                        val = 0;
                    Ring.Intensity[i] = val;
                }


            Ring.BitsPerPixels = data_length == 0 ? 16 : 32;


            Ring.ImageType = Ring.ImageTypeEnum.ADXV;

            br.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region ITEX
    public static bool ITEX(string str)
    {
        try
        {
            var br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.Read));
            br.BaseStream.Position = 0;

            //ヘッダ部分読み込み
            int fileType = 1, length = 0;
            Ring.Comments = "";

            var readData = new Func<double>(() => 0);
            var readHeader = new Func<bool>(() =>
            {
                if (br.BaseStream.Length - br.BaseStream.Position < 64 || new string(br.ReadChars(2)) != "IM")
                    return false;
                int commentLength = br.ReadInt16();
                int imageWidth = br.ReadInt16();
                int imageHeight = br.ReadInt16();
                int xOffset = br.ReadInt16();
                int yOffset = br.ReadInt16();
                fileType = br.ReadInt16();
                if (fileType != 0 && fileType != 2 && fileType != 3)
                    return false;

                readData = fileType switch { 0 => () => br.ReadByte(), 2 => () => br.ReadUInt16(), 3 => () => br.ReadUInt32(), _ => () => 0 };
                br.ReadBytes(50);

                Ring.SrcImgSize = new Size(imageWidth, imageHeight);
                if (commentLength > 0)
                    Ring.Comments += new string(br.ReadChars(commentLength)).Replace(",", "\r\n");

                length = imageWidth * imageHeight;
                return true;
            });

            Ring.BitsPerPixels = fileType switch { 0 => 8, 2 => 16, 3 => 32, _ => 0 };
            Ring.SequentialImageIntensities = [];
            Ring.SequentialImageNames = [];

            int n = 0;
            while (readHeader() && n < 10000)
            {
                Ring.SequentialImageIntensities.Add(new double[length]);
                for (int i = 0; i < length; i++)
                    Ring.SequentialImageIntensities[n][i] = readData();
                Ring.SequentialImageNames.Add(n.ToString("0000"));
                n++;
                //100GBを超えたら読み込み終了
                if (Ring.SequentialImageIntensities.Count * (long)Ring.SequentialImageIntensities[0].Length * 8 > 100000000000)
                    break;
            }

            Ring.Intensity = [.. Ring.SequentialImageIntensities[0]];
            Ring.ImageType = Ring.ImageTypeEnum.ITEX;

            br.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }

    #endregion

    #region ADSC

    public static bool ADSC(string str)
    {
        StringBuilder sb = new() { Capacity = 100000 };
        try
        {
            BinaryReader br = new(new FileStream(str, FileMode.Open, FileAccess.Read));

            //ヘッダ部分読み込み { から }までを読み込む
            br.BaseStream.Position = 2;

            do
            {
                var c = br.ReadChar();
                sb.Append(c);
            } while (!sb.ToString().EndsWith('}'));

            var tags = new Dictionary<string, string>();
            foreach (var tag in sb.ToString().Split([';', '\n'], StringSplitOptions.RemoveEmptyEntries))
            {
                var temp = tag.Split(['=']);
                if (temp.Length == 2)
                    tags.Add(tag.Split(['='])[0], tag.Split(['='])[1]);
            }

            int imageWidth = Convert.ToInt32(tags["SIZE1"]);
            int imageHeight = Convert.ToInt32(tags["SIZE2"]);

            br.BaseStream.Position = Convert.ToInt32(tags["HEADER_BYTES"]);

            Ring.Comments =
                "Date: " + tags["DATE"] + "\r\n" +
                "Time: " + tags["TIME"] + "\r\n" +
                "PixelSize: " + tags["PIXEL_SIZE"] + "\r\n" +
                "BIN: " + tags["BIN"] + "\r\n" +
                "Center: " + tags["BEAM_CENTER_X"] + "," + tags["BEAM_CENTER_Y"] + "\r\n";
            Ring.SrcImgSize = new Size(imageWidth, imageHeight);

            //イメージデータ読みこみ
            int length = imageWidth * imageHeight;

            if (Ring.Intensity.Length != length)//前回と同じサイズではないとき
                Ring.Intensity = new double[length];
            int n = 0;
            for (int y = 0; y < imageHeight; y++)
                for (int x = 0; x < imageWidth; x++)
                    Ring.Intensity[n++] = ((uint)(br.ReadByte() + br.ReadByte() * 256));

            Ring.BitsPerPixels = 16;
            Ring.ImageType = Ring.ImageTypeEnum.ADSC;

            br.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region RAXIS4

    public static bool Raxis4(string str, uint[] convertTable = null)
    {
        try
        {
            var br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.Read));

            //ヘッダ部分読み込み
            Ring.Comments = "";
            br.BaseStream.Position = 0;
            Ring.Comments += new string(br.ReadChars(10));//Device
            br.BaseStream.Position = 10;
            Ring.Comments += "\r\n" + new string(br.ReadChars(10));//Version
            br.BaseStream.Position = 20;
            Ring.Comments += "\r\n" + new string(br.ReadChars(20));//Sample
            br.BaseStream.Position = 92;
            Ring.Comments += "\r\n" + new string(br.ReadChars(80));//Comment
            br.BaseStream.Position = 256;
            Ring.Comments += "\r\n" + new string(br.ReadChars(12));//Date
            br.BaseStream.Position = 268;
            Ring.Comments += "\r\n" + new string(br.ReadChars(20));//Operator
            br.BaseStream.Position = 288;
            Ring.Comments += "\r\n" + new string(br.ReadChars(4));//Target
            var wl = convertToSingle(br);//Wavelength (A)
            br.ReadBytes(48);
            var camera_len = br.ReadSingle();//Camera length (mm)
            br.ReadBytes(420);
            var num_x_pixs = convertToInt(br);//Number of pixel X
            var num_y_pixs = convertToInt(br);//Number of pixel Y
            var x_pix_size = convertToSingle(br);//Pixel size  X (mm)
            var y_pix_size = convertToSingle(br);//Pixel size  Y (mm)
            var rec_len = convertToInt(br);//Record length
            var num_recs = convertToInt(br);//Number of record
            br.ReadBytes(8);
            var hilo = convertToSingle(br); //Hi/Lo 最上位ビットが1のときは最上位ビットを0にしてhiloをかけるらしい。
            br.ReadBytes(18);
            var ip_type = new string(br.ReadChars(10));//IP type

            Ring.SrcImgSize = new Size(num_x_pixs, num_y_pixs);

            if (convertTable == null || convertTable.Length != 65536)
            {
                convertTable = new uint[65536];
                for (uint i = 0; i < 65536; i++)
                    if (i > 0x7fff)
                        convertTable[i] = (uint)((i & 0x7fff) * hilo);
                    else
                        convertTable[i] = i;
            }

            //イメージデータ読みこみ
            var length = num_x_pixs * num_y_pixs;

            SetBytePosition(str, ref br, num_x_pixs * 2);

            if (Ring.Intensity.Length != length)//前回と同じサイズではないとき
                Ring.Intensity = new double[length];
            int n = 0;
            for (int y = 0; y < num_y_pixs; y++)
                for (int x = 0; x < num_x_pixs; x++)
                    Ring.Intensity[n++] = convertTable[(ushort)(256 * br.ReadByte() + br.ReadByte())];
            //画像を上下反転させるための処理
            for (int y = 0; y < num_y_pixs / 2; y++)
                for (int x = 0; x < num_x_pixs; x++)
                {
                    (Ring.Intensity[(num_y_pixs - y - 1) * num_x_pixs + x], Ring.Intensity[y * num_x_pixs + x])
                        = (Ring.Intensity[y * num_x_pixs + x], Ring.Intensity[(num_y_pixs - y - 1) * num_x_pixs + x]);
                }

            Ring.BitsPerPixels = 16;

            if (str.EndsWith("img"))
                Ring.ImageType = Ring.ImageTypeEnum.Rigaku_RAxis_V;
            else if (str.EndsWith("osc"))
                Ring.ImageType = Ring.ImageTypeEnum.Rigaku_RAxis_IV_Osc;
            else
                Ring.ImageType = Ring.ImageTypeEnum.Rigaku_RAxis_IV;

            br.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }

    #endregion

    #region Unrolled image from Rigaku RINT-RAPID II
    public static bool RintRapid2Unroll(string str, uint[] convertTable = null)
    {
        try
        {
            var br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.Read));

            //ヘッダ部分読み込み
            Ring.Comments = "";
            br.BaseStream.Position = 0; // 初期位置にセット
            string headerText = new(br.ReadChars(17408)); // HEADER_BYTES 分読み取る
                                                                 // 正規表現で検索
            Match matchMemo = Regex.Match(headerText, @"MEMO=([^;]+);"); // Device
            Ring.Comments += "\r\n" + matchMemo.Groups[1].Value;
            Match matchVersion = Regex.Match(headerText, @"DTREK_VERSION=([^;]+);"); // Version
            Ring.Comments += "\r\n" + matchVersion.Groups[1].Value;
            Match matchSample = Regex.Match(headerText, @"SAMPLE_NAME=([^;]+);"); // Sample
            Ring.Comments += "\r\n" + matchSample.Groups[1].Value;
            Match matchDate = Regex.Match(headerText, @"RX_CREATE_DATE=([^;]+);"); // Date
            Ring.Comments += "\r\n" + matchDate.Groups[1].Value;
            Match matchOperator = Regex.Match(headerText, @"OPERATOR_NAME=([^;]+);"); // Operator
            Ring.Comments += "\r\n" + matchOperator.Groups[1].Value;
            Match matchTarget = Regex.Match(headerText, @"SOURCE_TARGET=([^;]+);"); // Target
            Ring.Comments += "\r\n" + matchTarget.Groups[1].Value;

            Match matchSize1 = Regex.Match(headerText, @"SIZE1=(\d+);");
            Match matchSize2 = Regex.Match(headerText, @"SIZE2=(\d+);");

            int num_x_pixs = int.Parse(matchSize1.Groups[1].Value); // Number of pixel X
            int num_y_pixs = int.Parse(matchSize2.Groups[1].Value); // Number of pixel Y
            Ring.SrcImgSize = new Size(num_x_pixs, num_y_pixs);
            
            convertTable = new uint[65536];
            for (uint i = 0; i < 65536; i++)
                convertTable[i] = i;

            //イメージデータ読みこみ
            br.BaseStream.Position = 17408; // HEADER_BYTES 分読み取った後の位置にセット
            int length = num_x_pixs * num_y_pixs;

            // ヘッダ部分にData_type = unsigned short int;と書かれていた。
            for (int i = 0; i < length; i++)
                Ring.Intensity[i] = convertTable[br.ReadUInt16()]; 

        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }

    #endregion

    #region Bruker CCD


    public static bool Brucker(string str)
    {
        try
        {
            BinaryReader br = new(new FileStream(str, FileMode.Open, FileAccess.Read));
            Ring.Comments = "";
            //ヘッダ部分読み込み
            br.ReadBytes(8); var format = new string(br.ReadChars(72));
            br.ReadBytes(8); var version = new string(br.ReadChars(72));
            br.ReadBytes(8); var hdrblks = new string(br.ReadChars(72));
            br.ReadBytes(8); var type = new string(br.ReadChars(72));
            br.ReadBytes(8); var site = new string(br.ReadChars(72));
            br.ReadBytes(8); var model = new string(br.ReadChars(72));
            br.ReadBytes(8); var user = new string(br.ReadChars(72));
            br.ReadBytes(8); var sample = new string(br.ReadChars(72));
            br.ReadBytes(8); var setname = new string(br.ReadChars(72));
            br.ReadBytes(8); var run = new string(br.ReadChars(72));
            br.ReadBytes(8); var sampnum = new string(br.ReadChars(72));
            br.ReadBytes(8); var title1 = new string(br.ReadChars(72));
            br.ReadBytes(8); var title2 = new string(br.ReadChars(72));
            br.ReadBytes(8); var title3 = new string(br.ReadChars(72));
            br.ReadBytes(8); var title4 = new string(br.ReadChars(72));
            br.ReadBytes(8); var title5 = new string(br.ReadChars(72));
            br.ReadBytes(8); var title6 = new string(br.ReadChars(72));
            br.ReadBytes(8); var title7 = new string(br.ReadChars(72));
            br.ReadBytes(8); var title8 = new string(br.ReadChars(72));
            br.ReadBytes(8); var ncounts = new string(br.ReadChars(72));
            br.ReadBytes(8); var noverfl = new string(br.ReadChars(72));
            br.ReadBytes(8); var minimum = new string(br.ReadChars(72));
            br.ReadBytes(8); var maximum = new string(br.ReadChars(72));
            br.ReadBytes(8); var nontime = new string(br.ReadChars(72));
            br.ReadBytes(8); var nlate = new string(br.ReadChars(72));
            br.ReadBytes(8); var filename = new string(br.ReadChars(72));
            br.ReadBytes(8); var created = new string(br.ReadChars(72));
            br.ReadBytes(8); var cumulat = new string(br.ReadChars(72));
            br.ReadBytes(8); var elapsdr = new string(br.ReadChars(72));
            br.ReadBytes(8); var elapsda = new string(br.ReadChars(72));
            br.ReadBytes(8); var oscilla = new string(br.ReadChars(72));
            br.ReadBytes(8); var nsteps = new string(br.ReadChars(72));
            br.ReadBytes(8); var range = new string(br.ReadChars(72));
            br.ReadBytes(8); var start = new string(br.ReadChars(72));
            br.ReadBytes(8); var increme = new string(br.ReadChars(72));
            br.ReadBytes(8); var number = new string(br.ReadChars(72));
            br.ReadBytes(8); var nframes = new string(br.ReadChars(72));
            br.ReadBytes(8); var angle = new string(br.ReadChars(72));
            br.ReadBytes(8); var nover64 = new string(br.ReadChars(72));
            br.ReadBytes(8); var npixelb = Convert.ToInt32(new string(br.ReadChars(72)));
            br.ReadBytes(8); var nrows = Convert.ToInt32(new string(br.ReadChars(72)));
            br.ReadBytes(8); var ncols = Convert.ToInt32(new string(br.ReadChars(72)));
            br.ReadBytes(7680 - 42 * 80);

            Ring.SrcImgSize = new Size(ncols, nrows);

            var convertTable = new uint[65536];
            for (uint i = 0; i < 65536; i++)
                convertTable[i] = i;

            int length = ncols * nrows;
            Ring.Intensity = new double[length];
            //イメージデータ読みこみ
            if (npixelb == 1)
                for (int i = 0; i < length; i++)
                    Ring.Intensity[i] = convertTable[br.ReadByte()];
            else if (npixelb == 2)
                for (int i = 0; i < length; i++)
                    Ring.Intensity[i] = convertTable[(ushort)(br.ReadByte() + 256 * br.ReadByte())];
            else
            {
                br.Close();
                return false;
            }

            Ring.BitsPerPixels = 16;

            Ring.ImageType = Ring.ImageTypeEnum.Brucker_CCD;
            br.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region IPF
    public static bool Ipf(string str)
    {
        try
        {
            var br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.ReadWrite));
            Ring.SrcImgSize = new Size(8000, 4000);
            Ring.Intensity = new double[Ring.SrcImgSize.Width * Ring.SrcImgSize.Height];

            uint[] convertTable = new uint[65536];
            for (uint i = 0; i < 65536; i++)
            {
                if ((i & 0x8000) != 0)
                    convertTable[i] = ((0xffff ^ i) << 8);
                else
                    convertTable[i] = i;
            }
            //イメージデータ読みこみ
            for (int i = 0; i < Ring.SrcImgSize.Width * Ring.SrcImgSize.Height; i++)
                Ring.Intensity[i] = convertTable[(ushort)(256 * br.ReadByte() + br.ReadByte())];

            Ring.BitsPerPixels = 16;

            Ring.ImageType = Ring.ImageTypeEnum.Unknown;

            br.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region General Image
    public static bool GeneralImage(string str)
    {
        try
        {
            var bitmap = (Bitmap)Image.FromFile(str);
            if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
            {
                if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
                    bitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);

                if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
                    bitmap = new Bitmap(bitmap).Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);
            }

            Ring.SrcImgSize = new Size(bitmap.Width, bitmap.Height);

            //Ring.Intensity = new uint[bitmap.Width * bitmap.Height];
            Ring.BitsPerPixels = 8;

            //イメージデータ読みこみ
            Byte[] src = BitmapConverter.ToByteGray((Bitmap)bitmap);
            Ring.Intensity = new double[bitmap.Width * bitmap.Height];
            int n = 0;
            for (int h = bitmap.Height - 1; h >= 0; h--)
                for (int w = 0; w < bitmap.Width; w++)
                    Ring.Intensity[n++] = src[bitmap.Width * h + w];

            Ring.ImageType = Ring.ImageTypeEnum.Unknown;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region BAS 2000/2500
    public static bool BAS2000or2500(string str, string[] inf)
    {
        try
        {
            //BAS2000
            #region BAS2000
            /*******************************************************************
                         inf file format for standard BAS2000
                            1 line: BAS_IMAGE_FILE (Magic string) not found in MACBAS
                            2 line: Original File Name
                            3 line: IP type                 "20*40" "20*25"
                            4 line: Main Raster Pixel Size  "100" "200"
                            5 line: Sub Raster Pixel Size   "100" "200"
                            6 line: Bits of a pixel         "8" "10"
                            7 line: Pixels of Main Raster   "1024" "2048"
                            8 line: Pixels of Sub Raster    "1280" "2560" "2048" "4096"
                            9 line: Sensitivity             "400" "1000" "4000" "10000"
                           10 line: Latitude                "1.0" "2.0" "3.0" "4.0"
                           11 line: Date
                           12 line:
                           13 line:
                           14 line:
                           15 line: Comment
                        ********************************************************************/
            /*  read parameters  */
            //string image_type = "";
            //if (inf[3].IndexOf("20*40") == -1 && inf[3].IndexOf("20*40") == -1)
            //    image_type = "BAS2500";
            //else if (inf[3].IndexOf("100") == -1 && inf[3].IndexOf("200") == -1)
            //    image_type = "BAS2500";
            double pixelSizeX = Convert.ToDouble(inf[3]) / 1000;

            //if (inf[4].IndexOf("100") == -1 && inf[4].IndexOf("200") == -1)
            //    image_type = "BAS2500";
            //double pixelSizeY = Convert.ToDouble(inf[4]) / 1000;

            int bitsPerPixel = Convert.ToInt16(inf[5]);
            //if (bitsPerPixel > 10)
            //    image_type = "BAS2500";

            int numPixelX = Convert.ToInt16(inf[6]);
            int numPixelY = Convert.ToInt16(inf[7]);

            int sensitivity = Convert.ToInt16(inf[8]);
            //if ((sensitivity != 400) && (sensitivity != 1000) && (sensitivity != 4000) && (sensitivity != 10000))
            //    image_type = "BAS2500";

            double latitude = Convert.ToDouble(inf[9]);
            //if ((latitude != 1.0) && (latitude != 2.0) && (latitude != 3.0) && (latitude != 4.0))
            //    image_type = "BAS2500";

            string date = inf[10];
            string comment = inf[14];

            Ring.SrcImgSize = new Size(numPixelX, numPixelY);
            int length = Ring.SrcImgSize.Width * Ring.SrcImgSize.Height;
            //イメージデータ読みこみ
            var br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.ReadWrite));

            var x = latitude / Math.Pow(2, bitsPerPixel);
            //uint[] convertTable = new uint[65536];

            var convertTable = new double[65536];

            //double maxValue = Math.Pow(10, Math.Pow(2, bitsPerPixel) * x);

            for (int i = 0; i < 65536; i++)
                if (i < Math.Pow(2, bitsPerPixel))
                    convertTable[i] = Math.Pow(10, i * x) - 1;
                else
                    convertTable[i] = Math.Pow(10, Math.Pow(2, bitsPerPixel) * x) - 1;

            Ring.Intensity = new double[length];

            if (bitsPerPixel > 8)
                for (int i = 0; i < length; i++)
                    Ring.Intensity[i] = convertTable[(ushort)(256 * br.ReadByte() + br.ReadByte())];
            //  Ring.Intensity.Add((uint)(256 * br.ReadByte() + br.ReadByte())) ;
            else
                for (int i = 0; i < length; i++)
                    Ring.Intensity[i] = convertTable[br.ReadByte()];
            //Ring.Scale = 1 *maxValue / uint.MaxValue;

            br.Close();

            Ring.BitsPerPixels = 16;
            if (bitsPerPixel < 16)
                Ring.ImageType = Ring.ImageTypeEnum.Fuji_BAS2000;
            else
                Ring.ImageType = Ring.ImageTypeEnum.Fuji_BAS2500;

            Ring.Comments = "Latitude: " + latitude.ToString() + "\r\nBits/Pixel: " + bitsPerPixel.ToString() + "\r\nSensitivity: " + sensitivity.ToString();
        }
        catch (Exception e) { MessageBox.Show(e.Message); return false; }

        return true;
        #endregion
    }
    #endregion

    #region Fuji FDL
    public static bool FujiFDL(string str)
    {
        try
        {
            //FujiFDL
            var reader = new StreamReader(str[..^3] + "tem");
            var strList = new List<string>();
            string tempstr;
            while ((tempstr = reader.ReadLine()) != null)
                strList.Add(tempstr);

            double pixelSizeX = Convert.ToDouble(strList[3]) / 1000.0;
            double pixelSizeY = Convert.ToDouble(strList[4]) / 1000.0;
            int bitsPerPixel = Convert.ToInt32(strList[5]);
            int numPixelX = Convert.ToInt32(strList[6]);
            int numPixelY = Convert.ToInt32(strList[7]);

            Ring.SrcImgSize = new Size(numPixelX, numPixelY);
            int length = Ring.SrcImgSize.Width * Ring.SrcImgSize.Height;
            //イメージデータ読みこみ
            var br = new BinaryReader(new FileStream(str, FileMode.Open, FileAccess.ReadWrite));

            var convertTable = new uint[65536];

            bool renew = Ring.Intensity.Length != length;//前回と同じサイズではないとき
            int n = 0;
            if (renew)
                Ring.Intensity = new double[length];
            for (int y = 0; y < numPixelX; y++)
                for (int x = 0; x < numPixelY; x++)
                    Ring.Intensity[n++] = (ushort)(256 * br.ReadByte() + br.ReadByte());

            br.Close();

            Ring.BitsPerPixels = 16;
            Ring.ImageType = Ring.ImageTypeEnum.Fuji_FDL;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    #region TIFF
    public static bool Tiff(string str, bool? normarize = null)
    {
        try
        {
            var t = new Tiff.Loader(str);

            if (t.NumberOfFrames < 1)
                return false;
            if (t.IsGray == false)
            {
                t.IsGray = true;
                for (int j = 0; j < t.NumberOfFrames; j++)
                    t.Images[j].Value = [.. t.Images[j].ValueRed.Select(intValue => (double)intValue)];
            }
            Ring.Comments = "";
            Ring.SrcImgSize = new Size(t.ImageWidth, t.ImageLength);

            Ring.SequentialImageIntensities = new List<double[]>(t.NumberOfFrames);
            Ring.SequentialImageEnergy = new List<double>(t.NumberOfFrames);
            Ring.SequentialImageNames = new List<string>(t.NumberOfFrames);
            Ring.SequentialImagePulsePower = new List<double>(t.NumberOfFrames);

            for (int j = 0; j < t.NumberOfFrames; j++)
            {
                Ring.SequentialImageIntensities.Add([]);
                if (t.Images[j].Value != null)
                    Ring.SequentialImageIntensities[j] = [.. t.Images[j].Value];

                //hdfファイルに埋め込まれた情報の処理
                if (!double.IsNaN(t.Images[j].XrayEnergy))
                    Ring.SequentialImageEnergy.Add(t.Images[j].XrayEnergy);
                if (!double.IsNaN(t.Images[j].PulsePower))
                    Ring.SequentialImagePulsePower.Add(t.Images[j].PulsePower);

                if (t.Images[j].Name.Length == 0)
                    Ring.SequentialImageNames.Add(j.ToString("000"));
                else
                    Ring.SequentialImageNames.Add(t.Images[j].Name);
            }

            //hdfだった場合のノーマライズ処理
            if (Ring.SequentialImagePulsePower.Count != 0 && normarize == null)
                normarize = MessageBox.Show("This file contains pulse power in each image. Normarize intensities by pulse power?", "read file option", MessageBoxButtons.YesNo) == DialogResult.Yes;

            for (int j = 0; j < t.NumberOfFrames; j++)
            {
                if (normarize == true && Ring.SequentialImagePulsePower[j] > 0)
                    Ring.SequentialImageIntensities[j] = [.. Ring.SequentialImageIntensities[j].Select(d => d / Ring.SequentialImagePulsePower[j] / 10000)];
            }
            Ring.PulsePowerNormarized = normarize == true;

            Ring.Intensity = [.. Ring.SequentialImageIntensities[0]];

            //Ring.BitsPerPixels = t.BitsPerSampleGray;
            Ring.ImageType = Ring.ImageTypeEnum.Tiff;

            //TIA経由のTiffファイルだった時
            if (t.Images.Count > 0)
                Ring.TIA_PixelSize = t.Images[0].TIA_PixelSizeX;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }
    #endregion

    public static IPAImage IPAImageGenerator(double[] data, int width, int height, PointD center, double resolution, double cameralength, WaveProperty waveProperty)
    {
        IPAImage ipa = new()
        {
            IntensityDouble = data,
            Width = width,
            Height = height,
            Center = center,
            Resolution = resolution,
            CameraLength = cameralength,
            WaveProperty = waveProperty
        };

        return ipa;
    }

    public static void IPAImageWriter(string fileName, double[] data, double resolution, Size size, PointD center, double cameralength, WaveProperty waveProperty)
    {
        var ipa = IPAImageGenerator(data, size.Width, size.Height, center, resolution, cameralength, waveProperty);
        var serializer = new XmlSerializer(typeof(IPAImage));
        using var fs = new FileStream(fileName, FileMode.Create);
        serializer.Serialize(fs, ipa);//シリアル化し、バイナリファイルに保存する
        fs.Close();//閉じる
    }

    public static bool IPA(string fileName)
    {
        try
        {
            IPAImage ipa = GetIPA_Object(fileName);
            Ring.SrcImgSize = new Size(ipa.Width, ipa.Height);
            //イメージデータ読みこみ
            int length = ipa.Version == 0.0 ? ipa.Intensity.Length : ipa.IntensityDouble.Length;
            
            if (Ring.Intensity.Length != length)//前回と同じサイズではないとき
                Ring.Intensity = new double[length];

            int i = 0;
            for (int y = 0; y < ipa.Height; y++)
                for (int x = 0; x < ipa.Width; x++)
                {
                    if (ipa.Version == 0.0)
                        Ring.Intensity[i] = ipa.Intensity[i++] * ipa.Scale;
                    else
                        Ring.Intensity[i] = ipa.IntensityDouble[i++];
                }

            Ring.ImageType = Ring.ImageTypeEnum.IPAImage;
            Ring.BitsPerPixels = 16;
            Ring.IP ??= new IntegralProperty();
            Ring.IP.FilmDistance = ipa.CameraLength;
            Ring.IP.PixSizeX = Ring.IP.PixSizeY = ipa.Resolution;
            Ring.IP.CenterX = ipa.Center.X;
            Ring.IP.CenterY = ipa.Center.Y;
            Ring.IP.WaveProperty = ipa.WaveProperty;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
        return true;
    }

    public static IPAImage GetIPA_Object(string fileName)
    {
        var serializer = new XmlSerializer(typeof(IPAImage));
        var fs = new FileStream(fileName, FileMode.Open);//ファイルを開く
        var ipa = (IPAImage)serializer.Deserialize(fs);
        fs.Close();//閉じる
        return ipa;
    }

    [Serializable()]
    public class IPAImage
    {
        public double Version = 1.0;
        public uint[] Intensity;
        public double[] IntensityDouble;
        public double Scale;
        public int Width;
        public int Height;
        public PointD Center;
        public double Resolution;
        public WaveProperty WaveProperty;
        public double CameraLength;
        public double FilmBlur = 200;

        public IPAImage()
        {
            Intensity = [];
            IntensityDouble = [];
            Scale = 1;
            Width = 1;
            Height = 1;
            Center = new PointD(0, 0);
            Resolution = 1;
            WaveProperty = new WaveProperty(29, XrayLine.Ka);
            CameraLength = 1;
            FilmBlur = 200;
        }

        public IPAImage(double version, uint[] intensity, double[] intensityDouble, double scale, int width, int height, PointD center, double resolution, WaveProperty waveProperty, double cameraLength, double filmBlur)
        {
            Version = version;
            Intensity = intensity;
            IntensityDouble = intensityDouble;
            Scale = scale;
            Width = width;
            Height = height;
            Center = center;
            Resolution = resolution;
            WaveProperty = waveProperty;
            CameraLength = cameraLength;
            FilmBlur = filmBlur;
        }
        //  public uint[] ConvertTable = new uint[65536];
    }
}

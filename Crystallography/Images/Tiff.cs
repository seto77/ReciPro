using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Crystallography;

public class Tiff
{
    /// <summary>
    /// マルチバイトデータのバイト順 正順の場合はMotorola, 逆順はIntel
    /// </summary>
    public enum TiffByteOrder
    {
        Motorola, Intel
    }

    /// <summary>
    /// データタイプ
    /// </summary>
    public enum TiffDataType
    {
        /// <summary>
        /// 1バイト整数
        /// </summary>
        ByteType,

        AsciiType, //１バイトのASCII文字
        ShortType,//２バイト短整数
        LongType,//(４バイト長整数)
        RationalType,//(８バイト分数、４バイトの分子とそれに続く４バイトの分母)
        Sbyte,//１バイト符号付き整数
        UndefinedType,//(あらゆる１バイトデータ)
        SshortType,//２バイト符号付き短整数)
        SlongType,//(４バイト符号付き長整数)
        Srational,//(８バイト符号付き分数、４バイトの分子とそれに続く４バイトの分母)
        FloatType,//(４バイト実数、IEEE浮動小数点形式)
        DoubleType//(８バイト倍精度実数、IEEE倍精度浮動小数点形式)
    }

    public struct IFD
    {
        public int Tag;
        public Type DataType;
        public object[] Data;

        public IFD(int tag, Type dataType, object[] data)
        {
            Tag = tag;
            DataType = dataType;
            Data = data;
        }

        public static void Write(BinaryWriter bw, ushort tag, byte value)
        {
            bw.Write(BitConverter.GetBytes(tag));
            bw.Write(BitConverter.GetBytes((ushort)1));//type
            bw.Write(BitConverter.GetBytes((uint)1));//count
            bw.Write(value); bw.Write((byte)0x00); bw.Write((byte)0x00); bw.Write((byte)0x00);
        }

        public static void Write(BinaryWriter bw, ushort tag, char value)
        {
            bw.Write(BitConverter.GetBytes(tag));
            bw.Write(BitConverter.GetBytes((ushort)2));//type
            bw.Write(BitConverter.GetBytes((uint)1));//count
            bw.Write(value); bw.Write((byte)0x00); bw.Write((byte)0x00); bw.Write((byte)0x00);
        }

        public static void Write(BinaryWriter bw, ushort tag, ushort value)
        {
            bw.Write(BitConverter.GetBytes(tag));
            bw.Write(BitConverter.GetBytes((ushort)3));//type
            bw.Write(BitConverter.GetBytes((uint)1));//count
            bw.Write(BitConverter.GetBytes(value)); bw.Write((byte)0x00); bw.Write((byte)0x00);
        }

        public static void Write(BinaryWriter bw, ushort tag, uint value)
        {
            bw.Write(BitConverter.GetBytes(tag));
            bw.Write(BitConverter.GetBytes((ushort)4));//type
            bw.Write(BitConverter.GetBytes((uint)1));//count
            bw.Write(BitConverter.GetBytes(value));
        }

        public static void Write(BinaryWriter bw, ushort tag, float value)
        {
            bw.Write(BitConverter.GetBytes(tag));
            bw.Write(BitConverter.GetBytes((ushort)11));//type
            bw.Write(BitConverter.GetBytes((uint)1));//count
            bw.Write(BitConverter.GetBytes(value));
        }

        public static uint Write(BinaryWriter bw, ushort tag, double value, uint offset)
        {
            bw.Write(BitConverter.GetBytes(tag));
            bw.Write(BitConverter.GetBytes((ushort)12));//type
            bw.Write(BitConverter.GetBytes((uint)1));//count
            bw.Write(BitConverter.GetBytes(offset));
            long pos = bw.BaseStream.Position;
            bw.BaseStream.Position = offset;
            bw.Write(BitConverter.GetBytes(value));
            bw.BaseStream.Position = pos;
            return offset + 8;
        }

        /*  public static void Write(BinaryWriter bw, ushort tag, uint value1, uint value2, long offset)
          {
              bw.Write(BitConverter.GetBytes(tag));
              bw.Write(BitConverter.GetBytes((ushort)5));//type
              bw.Write(BitConverter.GetBytes((uint)2));//count
              bw.Write(BitConverter.GetBytes(offset));
              long pos = bw.BaseStream.Position;
              bw.BaseStream.Position = offset;
              bw.Write(BitConverter.GetBytes(value1));
              bw.Write(BitConverter.GetBytes(value2));
              bw.BaseStream.Position = pos;
          }*/

        public static uint Write(BinaryWriter bw, ushort tag, uint value1, uint value2, uint offset)
        {
            bw.Write(BitConverter.GetBytes(tag));
            bw.Write(BitConverter.GetBytes((ushort)5));//type
            bw.Write(BitConverter.GetBytes((uint)1));//count
            bw.Write(BitConverter.GetBytes(offset));
            long pos = bw.BaseStream.Position;
            bw.BaseStream.Position = offset;
            bw.Write(BitConverter.GetBytes(value1));
            bw.Write(BitConverter.GetBytes(value2));
            bw.BaseStream.Position = pos;
            return offset + 8;
        }

        public static uint Write(BinaryWriter bw, ushort tag, string str, uint offset)
        {
            bw.Write(BitConverter.GetBytes(tag));
            bw.Write(BitConverter.GetBytes((ushort)2));//type
            bw.Write(BitConverter.GetBytes((uint)str.Length + 1));//count
            bw.Write(BitConverter.GetBytes(offset));
            long pos = bw.BaseStream.Position;
            bw.BaseStream.Position = offset;
            for (int i = 0; i < str.Length; i++)
            {
                bw.Write((byte)str[i]);
                //bw.BaseStream.Position -= 1;
            }

            bw.Write((byte)0);

            bw.BaseStream.Position = pos;
            return offset + (uint)str.Length + 1;
        }
    }

    public static void Writer(string filename, double[] srcData, int sampleFormat, int imageWidth, IFD[] additionalIFD = null)
    {
        if (additionalIFD != null)
            Writer(filename, new double[][] { srcData }, sampleFormat, imageWidth, new IFD[][] { additionalIFD });
        else
            Writer(filename, new double[][] { srcData }, sampleFormat, imageWidth);
    }

    /// <summary>
    /// TIFFデータを書き込む. SampeleFormatが1のときは整数(byte, ushort, uint)、3の時はfloat, 1と3以外: 全て整数の場合は整数, そうでなければfloatに変換
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="srcData"></param>
    /// <param name="imageWidth"></param>
    /// <param name="filename"></param>
    /// <param name="sampleFormat">1のときは整数(byte, ushort, uint)、3の時はfloat, 1と3以外: 全て整数の場合は整数, そうでなければfloatに変換</param>
    public static void Writer(string filename, double[][] srcData, int sampleFormat, int imageWidth, IFD[][] additionalIFD = null)
    {
        double pixelSizeX = 1;
        double pixelSizeY = 1;
        //string unit = "";

        if (srcData == null || srcData.Length == 0 || srcData[0].Length == 0 || imageWidth == 0 || srcData[0].Length % imageWidth != 0) return;
        //additionalIFDの正当性チェック
        if (additionalIFD != null)
            for (int i = 0; i < additionalIFD.Length; i++)
                if (additionalIFD[i].Length != srcData.Length)
                    return;

        if (sampleFormat != 3 && sampleFormat != 1)
        {
            if (!srcData[0].Any(d => d % 1 != 0))
                sampleFormat = 1;
            else if (srcData[0].Max() < float.MaxValue && srcData[0].Min() > float.MinValue)
                sampleFormat = 3;
            else
                return;
        }
        var bw = new BinaryWriter(new FileStream(filename, FileMode.Create, FileAccess.ReadWrite));

        bw.Write((byte)0x49); bw.Write((byte)0x49);//バイトオーダー
        bw.Write((byte)0x2A); bw.Write((byte)0x00);//バージョン
        uint offset = (uint)bw.BaseStream.Position + 4;

        for (int n = 0; n < srcData.Length; n++)
        {
            int bytePerPixel = 4;
            long stripOffsets = offset;
            if (sampleFormat == 1)
            {
                var data = srcData[n].Select(d => d < uint.MaxValue && d > uint.MinValue ? (uint)d : (uint)0).ToArray();
                uint max = data.Max();
                if (max < 256)
                    bytePerPixel = 1;
                else if (max < 65536)
                    bytePerPixel = 2;
                else
                    bytePerPixel = 4;
                //IFDのポインタを書き込み
                bw.Write(BitConverter.GetBytes((uint)(offset + data.Length * bytePerPixel)));

                bw.BaseStream.Position = stripOffsets;
                if (bytePerPixel == 1)
                    for (int i = 0; i < data.Length; i++)
                        bw.Write((byte)data[i]);
                else if (bytePerPixel == 2)
                    for (int i = 0; i < data.Length; i++)
                        bw.Write(BitConverter.GetBytes((ushort)data[i]));
                else
                    for (int i = 0; i < data.Length; i++)
                        bw.Write(BitConverter.GetBytes(data[i]));
            }
            else if (sampleFormat == 3)
            {
                var data = srcData[n].Select(d => d < float.MaxValue && d > float.MinValue ? (float)d : 0).ToArray();
                //IFDのポインタを書き込み
                bw.Write(BitConverter.GetBytes((uint)(offset + data.Length * bytePerPixel)));
                bw.BaseStream.Position = stripOffsets;
                for (int i = 0; i < data.Length; i++)
                    bw.Write(BitConverter.GetBytes(data[i]));
            }

            //IFDの総数
            short totalIFD = 15;
            //additionalIFD = null;
            if (additionalIFD != null)
                totalIFD += (short)additionalIFD.Length;

            bw.Write(BitConverter.GetBytes(totalIFD));
            offset = (uint)(bw.BaseStream.Position + totalIFD * 12 + 4);

            IFD.Write(bw, 254, (ushort)0);                              //NewSubfileType
            IFD.Write(bw, 256, (uint)imageWidth);                       //ImageWidth
            IFD.Write(bw, 257, (uint)(srcData[n].Length / imageWidth));    //ImageLength
            IFD.Write(bw, 258, (ushort)(bytePerPixel * 8));             //BitsPerSample
            IFD.Write(bw, 259, (ushort)1);                              //Compression
            IFD.Write(bw, 262, (ushort)1);                              //PhotometricInterpretation
                                                                        //offset = IFD.Write(bw, (ushort)270, "ImageJ=1.44p\nunit=" + unit, offset);         //ImageDescription
            IFD.Write(bw, 273, (uint)stripOffsets);                      //StripOffsets
            IFD.Write(bw, 278, (uint)(srcData[n].Length / imageWidth));    //RowsPerStrip
            IFD.Write(bw, 279, (uint)(srcData[n].Length * bytePerPixel));  //StripByteCounts
            IFD.Write(bw, 280, (ushort)0);                              //MinSampleValue
            IFD.Write(bw, 281, (ushort)255);                            //MaxSampleValue
            offset = IFD.Write(bw, 282, (uint)1, (uint)pixelSizeX, offset); //XResolution
            offset = IFD.Write(bw, 283, (uint)1, (uint)pixelSizeY, offset); //YResolution
            IFD.Write(bw, 296, (ushort)1);                              //ResolutionUnit
            IFD.Write(bw, 339, (ushort)sampleFormat);                    //SampleFormat, 1: 整数, 3: float

            if (additionalIFD != null)
            {
                for (int i = 0; i < additionalIFD.Length; i++)
                {
                    if (additionalIFD[i][n].Tag == 60000)//XrayEnergy
                        offset = IFD.Write(bw, (ushort)additionalIFD[i][n].Tag, (double)additionalIFD[i][n].Data[0], offset);
                    if (additionalIFD[i][n].Tag == 60001)//Name
                        offset = IFD.Write(bw, (ushort)additionalIFD[i][n].Tag, (string)additionalIFD[i][n].Data[0], offset);
                    if (additionalIFD[i][n].Tag == 60002)//PulsePower
                        offset = IFD.Write(bw, (ushort)additionalIFD[i][n].Tag, (double)additionalIFD[i][n].Data[0], offset);
                }
            }
        }

        bw.Close();
    }

    public class Loader
    {
        /// <summary>
        /// バイトオーダー
        /// </summary>
        private TiffByteOrder byteOrder;

        /// <summary>
        /// バージョン番号
        /// </summary>
        public int Version;

        /// <summary>
        /// 格納しているイメージの数
        /// </summary>
        public int NumberOfFrames => Images.Count;

        public TiffByteOrder ByteOrder { get => byteOrder; set => byteOrder = value; }

        public List<imageProperty> Images = new List<imageProperty>();

        /// <summary>
        /// イメージの幅(ピクセル)
        /// </summary>
        public int ImageWidth;

        /// <summary>
        /// イメージの高さ(ピクセル)
        /// </summary>
        public int ImageLength;

        public bool IsGray;

        public int BitsPerSampleGray;

        public class imageProperty
        {
            public int BitsPerSampleGray;
            public int BitsPerSampleRed;
            public int BitsPerSampleGreen;
            public int BitsPerSampleBlue;

            /// <summary>
            /// 1: 非圧縮
            /// </summary>
            public int Compression;

            /// <summary>
            /// 0: 黒モードモノクロ (0が白い),   1: 白モードモノクロ (0が黒い),  2: RGBダイレクトカラー (0が黒い)
            /// </summary>
            public int PhotometricInterpretation;

            /// <summary>
            /// イメージの幅(ピクセル)
            /// </summary>
            public int ImageWidth;

            /// <summary>
            /// イメージの高さ(ピクセル)
            /// </summary>
            public int ImageLength;

            public double XResolution;
            public double YResolution;

            /// <summary>
            /// 1: 整数, 3:float
            /// </summary>
            public int SampleFormat = 1;

            /// <summary>
            /// 1: 単位なし, 2:inch,  3:cm
            /// </summary>
            public int ResolutionUnit = 2;

            public int[] ColorMapRed;
            public int[] ColorMapGreen;
            public int[] ColorMapBlue;

            public bool IsGray;

            public int[] StripOffsets;
            public int[] RowsPerStrip;
            public int[] StripByteCounts;

            //public uint[] ValueGray;
            public uint[] ValueRed;

            public uint[] ValueGreen;
            public uint[] ValueBlue;
            public double[] Value;

            public string ImageDescription = "";

            //gelファイルのタグ
            public double MDScaleFactor = 1;//強度に乗ずるスケール

            public int MDFileTag = 128;

            //h5ファイルのタグ
            public double XrayEnergy = double.NaN;

            public string Name = "";
            public double PulsePower = double.NaN;
        }

        public static byte[] Read(BinaryReader br, long position, int length, TiffByteOrder byteOrder)
        {
            br.BaseStream.Position = position;
            var buffer = new byte[length];
            _ = br.Read(buffer, 0, buffer.Length);
            if (byteOrder == TiffByteOrder.Intel) buffer = buffer.Reverse().ToArray();
            return buffer;
        }

        public Loader(string fileName)
        {
            //MDFileTag = 128;
            //MDScaleFactor = 1;
            bool originalEndian = BitConverter.IsLittleEndian;

            var br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));

            //まず最初の2Byteを読み込んでバイトオーダーを決める
            var temp = new byte[2];
            br.Read(temp, 0, 2);
            if (temp[0] == 0x49 && temp[1] == 0x49)
                ByteOrder = TiffByteOrder.Motorola;
            else if (temp[0] == 0x4D && temp[1] == 0x4D)
                ByteOrder = TiffByteOrder.Intel;
            else
                return;

            //次に2Byteよんでバージョン
            Version = BitConverter.ToInt16(Read(br, 2, 2, ByteOrder), 0);

            var ifdPointa = long.MaxValue;

            while (ifdPointa != 0)
            {
                var image = new imageProperty();
                //ifdPointaがlong.MaxValueの時(何も読み込んでいないとき) 4Byte読んでIFDのポインタ
                if (ifdPointa == long.MaxValue)
                    ifdPointa = BitConverter.ToUInt32(Read(br, 4, 4, ByteOrder), 0);

                //ポインタ位置から2ByteよんでIFDの総数
                int totalIFD = BitConverter.ToInt16(Read(br, ifdPointa, 2, ByteOrder), 0);

                //IFDの読み込み開始
                ifdPointa += 2;
                var iFD = new IFD[totalIFD];
                for (int i = 0; i < totalIFD; i++)
                {
                    try { iFD[i] = ReadIFD(br, (int)ifdPointa, ByteOrder); }
                    catch { }
                    ifdPointa += 12;
                }
                //ここまででIFDデータを読み込み完了

                //次のifdPointaを読み込む

                ifdPointa = BitConverter.ToUInt32(Read(br, ifdPointa, 4, ByteOrder), 0);

                int bitsPerSampleLength = 1;

                #region  IFDのデータ解析

                for (int i = 0; i < totalIFD; i++)
                    switch (iFD[i].Tag)
                    {
                        case 256:
                            image.ImageWidth = (int)iFD[i].Data[0]; break;
                        case 257:
                            image.ImageLength = (int)iFD[i].Data[0]; break;
                        case 258:

                            if (iFD[i].Data.Length == 1)
                            {
                                image.IsGray = true;
                                image.BitsPerSampleGray = (int)iFD[i].Data[0];
                            }
                            else if (iFD[i].Data.Length == 3 || iFD[i].Data.Length == 4)
                            {
                                bitsPerSampleLength = iFD[i].Data.Length;
                                image.IsGray = false;
                                image.BitsPerSampleRed = (int)iFD[i].Data[0];
                                image.BitsPerSampleGreen = (int)iFD[i].Data[1];
                                image.BitsPerSampleBlue = (int)iFD[i].Data[2];
                            }
                            break;

                        case 259:
                            image.Compression = (int)iFD[i].Data[0]; break;
                        case 262:
                            image.PhotometricInterpretation = (int)iFD[i].Data[0]; break;
                        case 270:
                            break;
                        case 273:
                            image.StripOffsets = new int[iFD[i].Data.Length];
                            for (int j = 0; j < image.StripOffsets.Length; j++)
                                image.StripOffsets[j] = (int)iFD[i].Data[j];
                            break;

                        case 278:
                            image.RowsPerStrip = new int[iFD[i].Data.Length];
                            for (int j = 0; j < image.RowsPerStrip.Length; j++)
                                image.RowsPerStrip[j] = (int)iFD[i].Data[j];
                            break;

                        case 279:
                            image.StripByteCounts = new int[iFD[i].Data.Length];
                            for (int j = 0; j < image.StripByteCounts.Length; j++)
                                image.StripByteCounts[j] = (int)iFD[i].Data[j];
                            break;

                        case 282:
                            if (iFD[i].Data[0] is int n)
                                image.XResolution = n;
                            else if (iFD[i].Data[0] is float f)
                                image.XResolution = f;
                            else if (iFD[i].Data[0] is double d)
                                image.XResolution = d;
                            break;
                        case 283:
                            if (iFD[i].Data[0] is int n2)
                                image.YResolution = n2;
                            else if (iFD[i].Data[0] is float f)
                                image.YResolution = f;
                            else if (iFD[i].Data[0] is double d)
                                image.YResolution = d;
                            break;
                        case 284:
                            image.ResolutionUnit = (int)iFD[i].Data[0]; break;
                        case 320:
                            for (int j = 0; j < iFD[i].Data.Length / 3; j++)
                            {
                                if (image.ColorMapRed != null)
                                {
                                    image.ColorMapRed[j] = (int)iFD[i].Data[j];
                                    image.ColorMapGreen[j] = (int)iFD[i].Data[j + iFD[i].Data.Length / 3];
                                    image.ColorMapBlue[j] = (int)iFD[i].Data[j + 2 * iFD[i].Data.Length / 3];
                                }
                            }
                            break;

                        case 339:
                            image.SampleFormat = (int)iFD[i].Data[0]; break;  //SampleFormat, 1: 整数, 3: float

                        case 33449:
                            {
                                var sb = new StringBuilder();
                                for (int j = 0; j < iFD[i].Data.Length; j++)
                                    //if ((char)iFD[i].Data[j] != '\r\n')
                                    sb.Append((char)iFD[i].Data[j]);
                                image.ImageDescription = sb.ToString();
                            }
                            break;

                        case 33446:
                            image.MDScaleFactor = (double)iFD[i].Data[0]; break;
                        case 33445:
                            image.MDFileTag = (int)iFD[i].Data[0]; break;

                        case 60000:
                            image.XrayEnergy = (double)iFD[i].Data[0]; break;
                        case 60001:
                            image.Name = new string(iFD[i].Data.Cast<char>().ToArray());
                            if (image.Name.EndsWith("\0"))
                                image.Name = image.Name.TrimEnd('\0');
                            break;

                        case 60002:
                            image.PulsePower = (double)iFD[i].Data[0]; break;
                    }
                #endregion

                //画像のサイズやカラーモードが違ったら、読み込み停止
                if (Images.Count == 0 || (ImageLength == image.ImageLength && ImageWidth == image.ImageWidth && image.IsGray == IsGray && BitsPerSampleGray == image.BitsPerSampleGray))
                {
                    ImageLength = image.ImageLength;
                    ImageWidth = image.ImageWidth;
                    IsGray = image.IsGray;
                    BitsPerSampleGray = image.BitsPerSampleGray;
                }
                else
                    break;

                int bytePerPixel = 1;
                if (image.IsGray || image.SampleFormat == 3)//grayか、浮動小数点の時
                {
                    bytePerPixel = image.BitsPerSampleGray / 8;
                    image.Value = new double[image.ImageLength * image.ImageWidth];
                }
                else
                {
                    bytePerPixel = image.BitsPerSampleRed / 8;
                    image.ValueRed = new uint[image.ImageLength * image.ImageWidth];
                    image.ValueGreen = new uint[image.ImageLength * image.ImageWidth];
                    image.ValueBlue = new uint[image.ImageLength * image.ImageWidth];
                }

                #region お蔵入り?
                //int latitude = 0;
                //if (image.ImageDescription.Contains("Latitude"))
                //{
                //    string[] tempStr = image.ImageDescription.Split('\n');
                //    for (int j = 0; j < tempStr.Length; j++)
                //        if (tempStr[j].Contains("Latitude"))
                //            latitude = Convert.ToInt32(tempStr[j].Split('=')[1]);
                //}
                #endregion

                //ここからデータを読み込む

                #region PFで導入された新フォーマットへの対処
                if (iFD.Any(i => i.Tag == 270) && image.StripByteCounts == null)
                {
                    var ifd = iFD.First(i => i.Tag == 270);
                    var comment = new string(ifd.Data.Cast<char>().ToArray());

                    //software
                    var software = comment[..16];
                    //Sensor
                    string sensor = comment.Substring(16, 20);
                    //Image Info
                    var correction = comment[38];
                    var type = comment[39];
                    var binning_x = comment[40];
                    var binning_y = comment[41];
                    var direction = comment[42];
                    var bitLength = comment[43];

                    //Accumulation number　画像取得時の積算回数	
                    var accumulation_number = comment.Substring(44, 4).ToInt();

                    //ExposureTime
                    var exposure_time = comment.Substring(48, 8).ToInt();

                    var frame = comment.Substring(56, 4).ToInt();

                    //Number of stored images
                    var num_of_stored_images = comment.Substring(60, 4).ToInt();

                    var total = image.ImageWidth * image.ImageLength;
                    image.StripByteCounts = new[] { image.ImageWidth * image.ImageLength * bytePerPixel };
                }
                #endregion


                Func<double> toInt = () => 0;
                if (bytePerPixel == 1)
                    toInt = () => br.ReadByte();
                else if (byteOrder == TiffByteOrder.Motorola)
                {

                    if (bytePerPixel == 2 && image.SampleFormat == 1)
                        toInt = () => br.ReadUInt16();
                    else if (bytePerPixel == 2 && image.SampleFormat == 2)
                        toInt = () => br.ReadInt16();
                    else if (bytePerPixel == 4 && image.SampleFormat == 1)
                        toInt = () => br.ReadUInt32();
                    else if (bytePerPixel == 4 && image.SampleFormat == 2)
                        toInt = () => br.ReadInt32();
                }
                else
                {
                    if (bytePerPixel == 2)
                    {
                        if (image.SampleFormat == 1)
                            toInt = () =>
                            {
                                var temp = new byte[2];
                                br.Read(temp, 0, 2);
                                temp = temp.Reverse().ToArray();
                                return BitConverter.ToUInt16(temp, 0);
                            };

                        else
                            toInt = () =>
                            {
                                var temp = new byte[2];
                                br.Read(temp, 0, 2);
                                temp = temp.Reverse().ToArray();
                                return BitConverter.ToInt16(temp, 0);
                            };
                    }
                    else if (bytePerPixel == 4)
                    {
                        if (image.SampleFormat == 1)
                            toInt = () =>
                            {
                                var temp = new byte[4];
                                br.Read(temp, 0, 4);
                                temp = temp.Reverse().ToArray();
                                return BitConverter.ToUInt32(temp, 0);
                            };

                        else
                            toInt = () =>
                            {
                                var temp = new byte[4];
                                br.Read(temp, 0, 4);
                                temp = temp.Reverse().ToArray();
                                return BitConverter.ToInt32(temp, 0);
                            };
                    }
                }



                for (int i = 0, n = 0; i < image.StripByteCounts.Length; i++)
                {
                    br.BaseStream.Position = image.StripOffsets[i];
                    if (image.SampleFormat == 3)//浮動小数点データの時
                    {
                        for (int j = 0; j < image.StripByteCounts[i] / bytePerPixel; j++)
                            image.Value[n++] = toFloat(br, bytePerPixel, ByteOrder);
                    }
                    else//整数データの時 1の時は符号なし。2の時は符号アリ
                    {
                        var sign = image.SampleFormat == 2;
                        if (image.IsGray)
                        {

                            if (image.MDFileTag == 2)//gelファイルの時
                            {
                                for (int j = 0; j < image.StripByteCounts[i] / bytePerPixel; j++)
                                {
                                    var intensity = toInt();
                                    image.Value[n++] = intensity * intensity;
                                }
                            }
                            else
                            {
                                for (int j = 0; j < image.StripByteCounts[i] / bytePerPixel; j++)
                                    image.Value[n++] = toInt();
                            }
                        }
                        else
                        {
                            for (int j = 0; j < image.StripByteCounts[i] / bytePerPixel / bitsPerSampleLength; j++)
                            {
                                image.ValueRed[n] = (uint)toInt();
                                image.ValueGreen[n] = (uint)toInt();
                                image.ValueBlue[n] = (uint)toInt();
                                if (bitsPerSampleLength == 4)
                                    toInt();
                                n++;
                            }
                        }
                    }
                }
                Images.Add(image);
            }
            br.Close();
        }


        /// <summary>
        /// ファイルからbyteCountだけ読み込んで、数値に変換して返す。signは符号付きの場合はtrue。
        /// </summary>
        /// <param name="br"></param>
        /// <param name="byteCount"></param>
        /// <param name="byteOrder"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        private static double toInt(BinaryReader br, int byteCount, TiffByteOrder byteOrder, bool sign)
        {
            if (byteCount == 1)
            {
                var temp = new byte[1];
                br.Read(temp, 0, 1);

                return (double)temp[0];
            }
            else if (byteCount == 2)
            {
                var temp = new byte[2];
                br.Read(temp, 0, 2);
                if (byteOrder == TiffByteOrder.Intel) temp = temp.Reverse().ToArray();

                return sign ? (double)BitConverter.ToInt16(temp, 0) : BitConverter.ToUInt16(temp, 0);
            }
            else if (byteCount == 4)
            {
                var temp = new byte[4];
                br.Read(temp, 0, 4);
                if (byteOrder == TiffByteOrder.Intel) temp = temp.Reverse().ToArray();

                return sign ? (double)BitConverter.ToInt32(temp, 0) : BitConverter.ToUInt32(temp, 0);

            }
            else
                return 0;

        }

        private static float toFloat(BinaryReader br, int byteCount, TiffByteOrder byteOrder)
        {
            float intensity = 0;
            var temp = new byte[4];
            br.Read(temp, 0, 4);

            if (byteOrder == TiffByteOrder.Intel) temp = temp.Reverse().ToArray();

            intensity = BitConverter.ToSingle(temp, 0);

            return intensity;
        }

        public static IFD ReadIFD(BinaryReader br, int index, TiffByteOrder byteOrder)
        {
            br.BaseStream.Position = index;

            var ifd = new IFD();

            byte[] temp;

            //まずタグを読み込み
            temp = new byte[2];
            br.Read(temp, 0, 2);
            if (byteOrder == TiffByteOrder.Intel)
                temp = temp.Reverse().ToArray();
            ifd.Tag = BitConverter.ToUInt16(temp, 0);

            if (ifd.Tag == 0)
                return ifd;

            //次にデータタイプを読み込み
            temp = new byte[2];
            br.Read(temp, 0, 2);
            if (byteOrder == TiffByteOrder.Intel)
                temp = temp.Reverse().ToArray();
            int datatype = BitConverter.ToUInt16(temp, 0);

            int dataLengh = 1;
            switch (datatype)//データのバイト数を読み込み
            {
                case 1: dataLengh = 1; ifd.DataType = typeof(byte); break;
                case 2: dataLengh = 1; ifd.DataType = typeof(char); break;
                case 3: dataLengh = 2; ifd.DataType = typeof(ushort); break;
                case 4: dataLengh = 4; ifd.DataType = typeof(uint); break;
                case 5: dataLengh = 8; ifd.DataType = typeof(double); break;
                case 6: dataLengh = 1; ifd.DataType = typeof(sbyte); break;
                case 7: dataLengh = 1; ifd.DataType = typeof(byte); break;
                case 8: dataLengh = 2; ifd.DataType = typeof(short); break;
                case 9: dataLengh = 4; ifd.DataType = typeof(int); break;
                case 10: dataLengh = 8; ifd.DataType = typeof(double); break;
                case 11: dataLengh = 4; ifd.DataType = typeof(float); break;
                case 12: dataLengh = 8; ifd.DataType = typeof(double); break;
            }

            //次にデータカウントを読み込み
            temp = new byte[4];
            br.Read(temp, 0, 4);
            if (byteOrder == TiffByteOrder.Intel) temp = temp.Reverse().ToArray();
            int datacount = BitConverter.ToInt32(temp, 0);//データカウント読み込み

            temp = new byte[4];
            br.Read(temp, 0, 4);

            if (dataLengh * datacount > 4)//データがポインタを示しているときtempを設定しなおす
            {
                if (byteOrder == TiffByteOrder.Intel)
                    temp = temp.Reverse().ToArray();
                int pointa = BitConverter.ToInt32(temp, 0);
                temp = new byte[dataLengh * datacount];
                br.BaseStream.Position = pointa;
                br.Read(temp, 0, dataLengh * datacount);
            }

            //次にTempをDataCount数に分割する

            byte[][] data = new byte[datacount][];
            for (int i = 0; i < datacount; i++)
            {
                data[i] = new byte[dataLengh];
                Array.Copy(temp, i * dataLengh, data[i], 0, dataLengh);
                if (byteOrder == TiffByteOrder.Intel)
                    data[i] = data[i].Reverse().ToArray();
            }

            //次にデータフィールドあるいはデータポインタを読み込み

            ifd.Data = new object[datacount];

            for (int i = 0; i < datacount; i++)
            {
                switch (datatype)
                {
                    case 1: ifd.Data[i] = data[i][0]; break;
                    case 2: ifd.Data[i] = (char)data[i][0]; break;
                    case 3: ifd.Data[i] = (int)BitConverter.ToUInt16(data[i], 0); break;
                    case 4: ifd.Data[i] = (int)BitConverter.ToUInt32(data[i], 0); break;
                    case 5:
                        double a1 = (double)(BitConverter.ToUInt32(data[i], 0));
                        double a2 = (double)(BitConverter.ToUInt32(data[i], 4));
                        ifd.Data[i] = a1 / a2; break;
                    case 6: ifd.Data[i] = (sbyte)data[i][0]; break;
                    case 7: ifd.Data[i] = data[i][0]; break;
                    case 8: ifd.Data[i] = (int)BitConverter.ToInt16(data[i], 0); break;
                    case 9: ifd.Data[i] = (int)BitConverter.ToInt32(data[i], 0); break;
                    case 10: ifd.Data[i] = (double)(BitConverter.ToInt32(data[i], 0) / BitConverter.ToInt32(data[i], 4)); break;
                    case 11: ifd.Data[i] = (double)BitConverter.ToSingle(data[i], 0); break;
                    case 12: ifd.Data[i] = (double)BitConverter.ToDouble(data[i], 0); break;
                }
            }
            return ifd;
        }
    }
}

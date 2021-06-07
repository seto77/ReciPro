using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crystallography
{
    //http://www.ntu.edu.sg/home/cbb/info/dmformat/index.html を参考にした

    public class DigitalMicrograph
    {
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

        /// <summary>
        /// マルチバイトデータのバイト順
        /// </summary>
        public enum ByteOrderEnum { BigEndian, LittleEndian }

        public class Loader
        {
            public int Version { get; set; }
            public long RootTagSize { get; set; }
            public ByteOrderEnum ByteOrder { get; set; }
            public long NumberOfTags { get; set; }

            public Dictionary<string, TagInfo> Tag = new Dictionary<string, TagInfo>();

            public Loader(string fileName)
            {
                var br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite));

                byte[] i1 = new byte[1], i2 = new byte[2], i4 = new byte[4];

                //Headerここから
                //まず最初の4Byteを読み込んでバージョン  DM version  を取得
                br.Read(i4, 0, 4);
                Version = BitConverter.ToInt32(i4.Reverse().ToArray(), 0);

                var readInt = Version == 4 ?
                    new Func<long>(() => { byte[] i = new byte[8]; br.Read(i, 0, 8); return BitConverter.ToInt64(i.Reverse().ToArray(), 0); }) :
                    new Func<long>(() => { byte[] i = new byte[4]; br.Read(i, 0, 4); return (long)BitConverter.ToInt32(i.Reverse().ToArray(), 0); });

                //次の4 or 8 Byteを読み込んで Size of root tag directory in bytes を取得. ただし、以降でこの値は使わない
                RootTagSize = readInt();

                //次の4Byteを読み込んで Byte order を取得. 0: big endian, 1:little endian. ただし、どうせWinだったらLittleEndian.
                br.Read(i4, 0, 4);
                ByteOrder = BitConverter.ToInt32(i4.Reverse().ToArray(), 0) == 0 ? ByteOrderEnum.BigEndian : ByteOrderEnum.LittleEndian;
                //Headerここまで

                //RootDirectoryここから
                //1 byte読み込んで Sorted, 1 = sorted (normally = 1) を読み込み. ただし、以降でこの値は使わない
                var Sorted = br.ReadByte();
                //1 byte1読み込んで Closed, 1 = open (normally = 0) を読み込み. ただし、以降でこの値は使わない
                var Closed = br.ReadByte();

                //次の4 or 8 byteを読み込んで Number of tags in root directory を取得
                NumberOfTags = readInt();
                //RootDirectoryここまで

                //Tag or Tag Directories ここから
                for (int i = 0; i < NumberOfTags; i++)
                {
                    TagInfo tagInfo = new TagInfo(br, Version);//Tagの読み込みはTagInfoクラスに任せる
                    if (tagInfo.TagName == "")
                        tagInfo.TagName = i.ToString();
                    Tag.Add(tagInfo.TagName, tagInfo);
                }
                //Tag or Tag Directories ここまで
            }
        }

        public class TagInfo
        {
            public string TagName;
            public Dictionary<string, TagInfo> Tag = new Dictionary<string, TagInfo>();
            public object[] Values;
            public long TotalBytes;

            public TagInfo(BinaryReader br, int version)
            {
                var readInt = version == 4 ?
                    new Func<long>(() => { byte[] i = new byte[8]; br.Read(i, 0, 8); return BitConverter.ToInt64(i.Reverse().ToArray(), 0); }) :
                    new Func<long>(() => { byte[] i = new byte[4]; br.Read(i, 0, 4); return (long)BitConverter.ToInt32(i.Reverse().ToArray(), 0); });

                byte[] i1 = new byte[1], i2 = new byte[2], i4 = new byte[4];

                //1 byte読み込んで tag, 20: tag directory,  21: tag,   0: end of file を読み込み
                int TagHeader = br.ReadByte();
                //2 byte 読み込んで、タグの名前のバイト数を取得
                br.Read(i2, 0, 2);
                int BytesinTagName = BitConverter.ToInt16(i2.Reverse().ToArray(), 0);
                //タグの名前を取得
                TagName = new string(br.ReadChars(BytesinTagName));

                //DM4の場合、トータルバイト数を取得
                if (version == 4)
                    TotalBytes = readInt();

                //Tag Directoriesだった場合
                if (TagHeader == 20)
                {
                    //1 byte読み込んで Sorted, 1 = sorted (normally = 1) を読み込み. ただし、以降でこの値は使わない
                    var Sorted = br.ReadByte();
                    //1 Byte読み込んで Closed, 1 = open (normally = 0) を読み込み. ただし、以降でこの値は使わない
                    var Closed = br.ReadByte();

                    //4 or 8 byte読み込んで、タグディレクトリ中のタグの数を取得
                    var NumberOfTagsInTagDirectory = readInt();

                    for (int i = 0; i < NumberOfTagsInTagDirectory; i++)
                    {
                        TagInfo tagInfo = new TagInfo(br, version);//TagInfoを再起呼び出し
                        if (tagInfo.TagName == "")
                            tagInfo.TagName = i.ToString();
                        Tag.Add(tagInfo.TagName, tagInfo);
                    }
                }
                //Tagだった場合
                else if (TagHeader == 21)
                {
                    //4byte 読み込んで、"%%%%”であることを確認
                    var percent = new string(br.ReadChars(4));
                    //4 or 8 byte読み込んで、ninfo, size of info array following
                    var Ninfo = readInt();
                    var Info = new long[Ninfo];
                    for (int i = 0; i < Ninfo; i++)
                        Info[i] = readInt();

                    //Group of data.
                    if (Info[0] == (long)TagDataTypes.GroupOfData)  //group of data. info[1] = 0, info(2) = number in group, info[2*n+3] = 0, info[2*n+4] data type for each value in group
                    {
                        var NumberInGroup = Info[2];
                        Values = new object[NumberInGroup];
                        for (int i = 0; i < NumberInGroup; i++)
                            Values[i] = GetValue(br, (TagDataTypes)Info[i * 2 + 4]);
                    }
                    //Array of data or groups of data
                    else if (Info[0] == (int)TagDataTypes.ArrayOfDataOrGroups)
                    {
                        if (Ninfo == 3)//単純な値を配列として持っている場合 info[1]: tag data type for all array members, info[2]: size of array
                        {
                            Values = new object[Info[2]];
                            for (int i = 0; i < Info[2]; i++)
                                Values[i] = GetValue(br, (TagDataTypes)Info[1]);
                        }
                        else //グループを配列として持っている場合 ,info[0] = 20, info[1] = 15, info[2] = 常に0, info[3] = number of values in group, info[2*i+3] = tag data type for value i, info(ninfo) = size of info array
                        {
                            var NumberOfValuesInGroup = Info[3];//一つのグループに含まれるデータの数
                            Values = new object[Info[Ninfo - 1]];
                            for (int j = 0; j < Values.Length; j++)
                            {
                                var v = new object[NumberOfValuesInGroup];
                                for (int i = 0; i < NumberOfValuesInGroup; i++)
                                    v[i] = GetValue(br, (TagDataTypes)Info[i * 2 + 5]);
                                Values[j] = v;
                            }
                        }
                    }
                    else//single entryの場合
                    {
                        Values = new object[1];
                        if (Info[0] == 12)//謎の数値12が入ってくる場合, 取りあえず8バイトデータがあるみたいなので、int64で読み込む
                            Values[0] = GetValue(br, (TagDataTypes)11);
                        else
                            Values[0] = GetValue(br, (TagDataTypes)Info[0]);
                    }
                }
            }

            public static object GetValue(BinaryReader br, TagDataTypes type)
            {
                return type switch
                {
                    TagDataTypes.SHORT => br.ReadInt16(),
                    TagDataTypes.LONG => br.ReadInt32(),
                    TagDataTypes.USHORT => br.ReadUInt16(),
                    TagDataTypes.ULONG => br.ReadUInt32(),
                    TagDataTypes.FLOAT => br.ReadSingle(),
                    TagDataTypes.DOUBLE => br.ReadDouble(),
                    TagDataTypes.BOOL => br.ReadBoolean(),
                    TagDataTypes.CHAR => br.ReadChar(),
                    TagDataTypes.I1 => br.ReadByte(),
                    TagDataTypes.I8 => br.ReadUInt64(),
                    _ => null,
                };
            }
        }

        public enum TagDataTypes
        { SHORT = 2, LONG = 3, USHORT = 4, ULONG = 5, FLOAT = 6, DOUBLE = 7, BOOL = 8, CHAR = 9, I1 = 10, I8 = 11, GroupOfData = 15, STRING = 18, ArrayOfDataOrGroups = 20 }
    }
}
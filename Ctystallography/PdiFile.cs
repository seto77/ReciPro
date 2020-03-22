using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Crystallography
{
    public static class XYFile
    {
        public static bool SavePdiFile(DiffractionProfile[] dp, string fileName)
        {
            System.IO.FileStream fs = null;
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DiffractionProfile[]));
                fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
                serializer.Serialize(fs, dp);
                fs.Close();
                return true;
            }
            catch
            {
                if (fs != null)
                    fs.Close();
                return false;
            }
        }

        public static DiffractionProfile[] ReadPdiFile(string fileName)
        {
            //OriginalFormatType  -> SrcAxisMode
            //OriginalWaveLength  -> SrcWaveLength
            //OriginalTakeoffAngle　-> SrcTakeoffAngle
            try//まずXMLのタグを変更
            {
                using (StreamReader reader = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS")))
                {
                    List<string> strList = new List<string>();
                    string tempstr;
                    while ((tempstr = reader.ReadLine()) != null)
                    {
                        tempstr = tempstr.Replace("OriginalFormatType", "SrcAxisMode");
                        tempstr = tempstr.Replace("OriginalWaveLength", "SrcWaveLength");
                        tempstr = tempstr.Replace("OriginalTakeoffAngle", "SrcTakeoffAngle");
                        tempstr = tempstr.Replace("pt", "Pt");
                        strList.Add(tempstr);
                    }

                    reader.Close();

                    using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.GetEncoding("Shift_JIS")))
                    {
                        for (int i = 0; i < strList.Count; i++)
                            writer.WriteLine(strList[i]);
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
            catch { };

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DiffractionProfile[]));
            System.IO.FileStream fs = null;
            try
            {
                fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
                DiffractionProfile[] dp = (DiffractionProfile[])serializer.Deserialize(fs);
                fs.Close();
                if (dp.Length > 0)
                    return dp;
                else
                    return new DiffractionProfile[0];
            }
            catch//もしシリアライズできなかったら、name部分に間違った日本語が書かれている可能性あり。
            {
                if (fs != null)
                    fs.Close();
                try
                {
                    StreamReader reader = new StreamReader(fileName, Encoding.UTF8);
                    List<string> strList = new List<string>();
                    string tempstr;
                    while ((tempstr = reader.ReadLine()) != null)
                        strList.Add(tempstr);
                    reader.Close();
                    StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8);
                    for (int i = 0; i < strList.Count; i++)
                    {
                        if (strList[i].EndsWith("\0"))
                            writer.WriteLine(strList[i].TrimEnd('\0'));
                        else
                            writer.WriteLine(strList[i]);
                    }

                    writer.Flush();
                    writer.Close();
                    fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
                    DiffractionProfile[] dp = (DiffractionProfile[])serializer.Deserialize(fs);
                    fs.Close();
                    if (dp.Length > 0)
                        return dp;
                    else return new DiffractionProfile[0];
                }
                catch
                {
                    try
                    {
                        if (fs != null)
                            fs.Close();
                        StreamReader reader = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS"));
                        List<string> strList = new List<string>();
                        string tempstr;
                        while ((tempstr = reader.ReadLine()) != null)
                            strList.Add(tempstr);
                        reader.Close();
                        if (strList.Count <= 3)
                            return new DiffractionProfile[0];

                        DiffractionProfile diffProf = new DiffractionProfile();

                        //古いヘッダの書式
                        //Wave Length (0.1nm):0.4176811455              0
                        //Camera Length (mm):445.8055943768             1
                        //Pixel Size Horizontal(mm):0.10080533          2
                        //AspectRatio(Vertical/Horizontal):1            3
                        //Mode:Angle                                    4
                        //4,733.102005852614                            5

                        if (strList[0].IndexOf("Wave Length") >= 0)
                        {
                            if (strList[0].IndexOf("(nm)") >= 0)
                                diffProf.SrcWaveLength = Convert.ToDouble((strList[0].Split(':'))[1]);
                            else if (strList[0].IndexOf("(0.1nm)") >= 0)
                                diffProf.SrcWaveLength = Convert.ToDouble((strList[0].Split(':'))[1]) / 10.0;
                        }

                        if ((strList[4].Split(':'))[1] == "Angle")
                            diffProf.SrcAxisMode = HorizontalAxis.Angle;
                        else if ((strList[4].Split(':'))[1] == "d-spacing")
                            diffProf.SrcAxisMode = HorizontalAxis.d;
                        else if ((strList[4].Split(':'))[1] == "Energy")
                            diffProf.SrcAxisMode = HorizontalAxis.EnergyXray;
                        else
                            return new DiffractionProfile[0];

                        for (int i = 5; i < strList.Count; i++)
                        {
                            string[] str = strList[i].Split(',');
                            diffProf.OriginalProfile.Pt.Add(new PointD(Convert.ToDouble(str[0]), Convert.ToDouble(str[1])));
                        }
                        diffProf.Name = fileName.Remove(0, fileName.LastIndexOf('\\') + 1);
                        return new DiffractionProfile[] { diffProf };
                    }
                    catch { return new DiffractionProfile[0]; }
                }
            }
        }

        public static DiffractionProfile[] ReadRasFile(string fileName)
        {
            List<string> strArray = new List<string>();
            StreamReader reader = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS"));
            string tempstr;
            while ((tempstr = reader.ReadLine()) != null)
                strArray.Add(tempstr);
            reader.Close();
            if (strArray.Count <= 3)
                return new DiffractionProfile[0];

            List<DiffractionProfile> dp = new List<DiffractionProfile>();

            for (int i = 0; i < strArray.Count; i++)
            {
                if (strArray[i] == "*RAS_INT_START")
                {
                    i++;
                    dp.Add(new DiffractionProfile());
                    for (; i < strArray.Count; i++)
                    {
                        if (strArray[i] != "*RAS_INT_END")
                        {
                            string[] tempStr = strArray[i].Split(new[] { ' ' });
                            double x = Convert.ToDouble(tempStr[0]);
                            double y = Convert.ToDouble(tempStr[1]);
                            dp[dp.Count - 1].OriginalProfile.Pt.Add(new PointD(x, y));
                        }
                        else
                            break;
                    }
                }
            }

            for(int i=0; i<dp.Count; i++)
                dp[i].Name = $"{Path.GetFileName(fileName)}{(dp.Count>1 ? $" -{i}" : "")}";

            if (dp.Count > 0)
                return dp.ToArray();
            else return new DiffractionProfile[0];
        }

        /// <summary>
        /// 複数のプロファイルを含むCSV形式(PDI独自形式)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DiffractionProfile[] ReadCSVFile(string fileName)
        {
            var strArray = new List<string>();
            using (var reader = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS")))
            {
                string tempstr;
                while ((tempstr = reader.ReadLine()) != null)
                    strArray.Add(tempstr);
            }

            if (strArray.Count <= 3)
                return new DiffractionProfile[0];

            if (!strArray[1].StartsWith("X,Y,"))
                return new DiffractionProfile[0];

            try
            {
                string[] title = strArray[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] axis = strArray[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] value = strArray[2].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (title.Length * 2 != axis.Length || axis.Length != value.Length)
                    return new DiffractionProfile[0];

                var dp = new DiffractionProfile[title.Length];
                for (int i = 0; i < dp.Length; i++)
                {
                    dp[i] = new DiffractionProfile();
                    dp[i].Name = title[i];
                }
                for (int i = 2; i < strArray.Count; i++)
                {
                    value = strArray[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < value.Length / 2; j++)
                    {
                        double x = Convert.ToDouble(value[j * 2]);
                        double y = Convert.ToDouble(value[j * 2 + 1]);
                        dp[j].OriginalProfile.Pt.Add(new PointD(x, y));
                    }
                }

                for (int i = 0; i < dp.Length; i++)
                {
                    if (title[i] == "")
                        dp[i].Name = $"{Path.GetFileName(fileName)}{(dp.Length > 1 ? $" -{i}" : "")}";
                    else
                        dp[i].Name = title[i];
                }
                return dp;
            }
            catch
            {
                return new DiffractionProfile[0];
            }
        }



            /// <summary>
            /// よく分からないファイルを読み込む
            /// </summary>
            /// <param name="fileName"></param>
            /// <param name="separater"></param>
            /// <returns></returns>
        public static DiffractionProfile ConvertUnknownFileToProfileData(string fileName, char separater)
        {
            List<string> strArray = new List<string>();
            StreamReader reader = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS"));
            string tempstr;
            while ((tempstr = reader.ReadLine()) != null)
                strArray.Add(tempstr);
            reader.Close();
            if (strArray.Count <= 3)
                return null;

            List<string[]> stringList = new List<string[]>();
            //まず指定されたセパレータで全てを区切る
            for (int i = 0; i < strArray.Count; i++)
                stringList.Add(strArray[i].Split(new char[] { separater }, StringSplitOptions.RemoveEmptyEntries));
            //その全てを数値に変換する
            List<double[]> doubleList = new List<double[]>();
            List<double> doubleTemp = new List<double>();
            double result;
            for (int i = 0; i < stringList.Count; i++)
            {
                doubleTemp = new List<double>();
                for (int j = 0; j < stringList[i].Length; j++)
                {
                    var str = Miscellaneous.IsDecimalPointComma ? stringList[i][j].Replace('.', ',') : stringList[i][j].Replace(',', '.');
                    if (double.TryParse(str, out result))
                        doubleTemp.Add(result);
                }
                doubleList.Add(doubleTemp.ToArray());
            }
            int count = 1;
            int beforeLength = 0;
            int countMax = int.MinValue;
            int startColumn = 1;
            int endColumn = 0;

            //doubleList中で2つ以上数値を含み、10個以上連続しているものばしょをえらぶ
            for (int i = 0; i < doubleList.Count; i++)
            {
                count = 0;
                beforeLength = doubleList[i].Length;
                if (beforeLength >= 2)
                {
                    for (int j = i; j < doubleList.Count && beforeLength == doubleList[j].Length; j++)
                        count++;
                    if (countMax < count)
                    {
                        countMax = count;
                        startColumn = i;
                        endColumn = i + count - 1;
                    }
                    i += count - 1;
                }
            }
            if (countMax < 10) return null;//100以下だったらNullをかえす

            if (endColumn + 1 < doubleList.Count)
                doubleList.RemoveRange(endColumn + 1, doubleList.Count - endColumn - 1);
            doubleList.RemoveRange(0, startColumn);

            //X軸をきめる 0.00000001以上のステップで100個以上連続しているものをさがす
            int xRow = -1;
            double tempStep;
            countMax = int.MinValue;
            startColumn = 0;
            endColumn = 0;
            for (int i = 0; i < doubleList[0].Length; i++)
            {
                for (int j = 0; j < doubleList.Count - 1; j++)
                {
                    count = 0;
                    tempStep = doubleList[j + 1][i] - doubleList[j][i];
                    for (int k = j + 1; k < doubleList.Count - 1 && Math.Abs(doubleList[k][i] + tempStep - doubleList[k + 1][i]) < 10 && tempStep > 0.00001; k++)
                        count++;
                    if (countMax < count)
                    {
                        countMax = count + 2;
                        startColumn = j;
                        endColumn = startColumn + count + 1;
                        xRow = i;
                    }
                    j += count;
                }
            }
            if (countMax < 10 || xRow == -1) return null;//100以下かXを見つけられなかったらNullをかえす

            if (endColumn + 1 < doubleList.Count)
                doubleList.RemoveRange(endColumn + 1, doubleList.Count - endColumn - 1);
            doubleList.RemoveRange(0, startColumn);

            //y軸を決める 標準偏差が一番大きい数が格納されている
            int yRow = -1;
            double Sum, SumSquare, Deviation, DeviationMax;
            DeviationMax = double.NegativeInfinity;
            for (int i = 0; i < doubleList[0].Length; i++)
                if (i != xRow)
                {
                    Sum = SumSquare = 0;
                    for (int j = 0; j < doubleList.Count; j++)
                    {
                        Sum += doubleList[j][i];
                        SumSquare += doubleList[j][i] * doubleList[j][i];
                    }
                    Deviation = (doubleList.Count * SumSquare - Sum * Sum) / doubleList.Count / (doubleList.Count - 1);
                    if (DeviationMax < Deviation)
                    {
                        DeviationMax = Deviation;
                        yRow = i;
                    }
                }
            if (yRow == -1) return null;

            //最後に値を代入
            DiffractionProfile dif = new DiffractionProfile();
            for (int i = 0; i < doubleList.Count; i++)
                dif.OriginalProfile.Pt.Add(new PointD(doubleList[i][xRow], doubleList[i][yRow]));

            return dif;
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Crystallography
{
    public class ReadMeGenerator
    {
        public static void WriteReadMeFile
            (string version, string introduction, string manual, string copyright, string condition,
            string exemption, string adress, string acknowledge, string history)
        {
            //ReadMe.txtが存在していないか、存在していても古いときにReadMe.txtを生成する
            if (!File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\ReadMe.txt") ||
                File.GetLastWriteTime(Assembly.GetEntryAssembly().Location) > File.GetLastWriteTime(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\ReadMe.txt"))
            {
                List<string> str = new List<string>();

                str.Add(version);//タイトル
                str.Add(introduction);//はじめに
                str.Add(manual);//操作方法
                str.Add(copyright);//著作権
                str.Add(condition);//実行条件
                str.Add(exemption);//免責
                str.Add(adress);//連絡先
                str.Add(acknowledge);//謝辞
                str.Add(history);//履歴

                for (int i = str.Count - 1; i >= 1; i--)
                    str.Insert(i, "\r\n");

                //File.WriteAllLines(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\ReadMe.txt", str.ToArray(), Encoding.Unicode);

                //File.WriteAllLines(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\History.txt", new string[] {version , history}, Encoding.Unicode);
            }
        }
    }
}
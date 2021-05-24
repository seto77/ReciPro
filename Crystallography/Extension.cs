using Microsoft.Win32;

namespace Crystallography
{
    public class AssociatedExtension
    {
        public static void Add(string extension, string appPath, string appName)
        {
            //関連付ける拡張子
            //string extension = ".000";

            //実行するコマンドライン
            //string commandline = "\"" + Application.ExecutablePath + "\" %1";
            string commandline = "\"" + appPath + "\" %1";

            //ファイルタイプ名
            string fileType = appName;

            //説明（「ファイルの種類」として表示される）
            //（必要なし）
            string description = appName;

            //動詞
            string verb = "open";

            //動詞の説明（エクスプローラのコンテキストメニューに表示される）
            //（必要なし）
            string verb_description = appName + "で開く(&O)";

            //アイコンのパスとインデックス
            string iconPath = appPath;
            int iconIndex = 0;

            //ファイルタイプを登録
            RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(extension);
            regkey.SetValue("", appName);
            regkey.Close();

            //ファイルタイプとその説明を登録
            RegistryKey shellkey = Registry.ClassesRoot.CreateSubKey(appName);
            shellkey.SetValue("", description);

            //動詞とその説明を登録
            shellkey = shellkey.CreateSubKey("shell\\" + verb);
            shellkey.SetValue("", verb_description);

            //コマンドラインを登録
            shellkey = shellkey.CreateSubKey("command");
            shellkey.SetValue("", commandline);
            shellkey.Close();

            //アイコンの登録
            RegistryKey iconkey = Registry.ClassesRoot.CreateSubKey(appName + "\\DefaultIcon");
            iconkey.SetValue("", iconPath + "," + iconIndex.ToString());
            iconkey.Close();
        }

        public static void Delete(string extension, string appPath, string appName)
        {
            //拡張子
            //string extension = ".000";
            //ファイルタイプ名
            string fileType = appName;

            //レジストリキーを削除
            try
            {
                RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(extension);
                regkey.DeleteValue("");
            }
            catch { }

            //Registry.ClassesRoot.DeleteSubKeyTree(extension);
            //Registry.ClassesRoot.DeleteSubKeyTree(fileType);
        }

        public static bool Check(string extension, string appName)
        {
            try
            {
                var result = (string)Registry.ClassesRoot.OpenSubKey(extension).GetValue("");
                if (result == null)
                    return false;
                else if (result is string str)
                    return appName == str;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
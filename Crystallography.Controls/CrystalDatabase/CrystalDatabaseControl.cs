using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Drawing;
using MessagePack;
using MessagePack.Resolvers;

namespace Crystallography.Controls
{
    public partial class CrystalDatabaseControl : UserControl
    {
        #region フィールド、メソッド、イベント
        public string Filter { get => bindingSource.Filter; set => bindingSource.Filter = value; }

        public float FontSize
        {
            get => dataGridView.Font.Size;
            set => dataGridView.Font = new Font(dataGridView.Font.FontFamily, value);
        }

        readonly Stopwatch sw = new Stopwatch();

        readonly ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();

        readonly MessagePackSerializerOptions msgOptions = StandardResolverAllowPrivate.Options.WithCompression(MessagePackCompression.Lz4BlockArray);

        byte[] serialize<T>(T c) => MessagePackSerializer.Serialize(c, msgOptions);

        T deserialize<T>(ReadOnlyMemory<byte> buffer, out int byteRead) => MessagePackSerializer.Deserialize<T>(buffer, msgOptions, out byteRead);
        T deserialize<T>(object obj) => MessagePackSerializer.Deserialize<T>((byte[])obj, msgOptions);

        public Crystal Crystal => Crystal2.GetCrystal(Crystal2);
      
        public Crystal2 Crystal2 => dataSet.DataTableCrystalDatabase.Get(bindingSource.Current);


        readonly DataSet.DataTableCrystalDatabaseDataTable dataTable;

        public event EventHandler CrystalChanged;

        public delegate void ProgressChangedEventHandler(object sender, double progress, string message);
        public event ProgressChangedEventHandler ProgressChanged;

        #endregion

        #region コンストラクタ
        public CrystalDatabaseControl()
        {
            InitializeComponent();
            dataTable = dataSet.DataTableCrystalDatabase;
        }
        #endregion

        #region データベース読み込み/書き込み関連

        #region バイト書き込み/読み込み
        private int readInt(Stream s) => BitConverter.ToInt32(readBytes(s, 4), 0);
        private int readByte(Stream s) => s.ReadByte();
        private long readLong(Stream s) => BitConverter.ToInt64(readBytes(s, 8), 0);

        private byte[] readBytes(Stream s, int length)
        {
            var bytes = new byte[length];
            s.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        private void writeInt(Stream s, int v) => s.Write(BitConverter.GetBytes(v), 0, 4);
        private void writeLong(Stream s, long v) => s.Write(BitConverter.GetBytes(v), 0, 8);
        private void writeByte(Stream s, byte v) => s.WriteByte(v);
        private void writeBytes(Stream s, byte[] v) => s.Write(v, 0, v.Length);

        #endregion

        #region データベース読み込み
        public void ReadDatabase(string filename)
        {
            bindingSource.DataMember = "";
            this.Enabled = false;
            ReadDatabaseWorker.RunWorkerAsync(filename);
        }
        private void ReadDatabaseWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var filename = (string)e.Argument;
            try
            {
                sw.Restart();
                //if (filename.ToLower().EndsWith("cdb2"))
                //{
                //    var progressStep = 500;
                //    using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                //    var formatter = new BinaryFormatter();
                //    var total = (int)formatter.Deserialize(fs);
                //    for (int i = 0; i < total; i++)
                //    {
                //        var c = (Crystal2)formatter.Deserialize(fs);
                //        dataTable.Add(c);

                //        if (i > progressStep * 2 && i % progressStep == 0)
                //            report(i, total, sw.ElapsedMilliseconds, "Loading database...");
                //    }
                //}
                //else 
                if (filename.ToLower().EndsWith("cdb3"))
                {
                    using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    int flag = readByte(fs), total = readInt(fs);
                    if (flag == 100)//単一ファイルの時
                    {
                        var b = new ReadOnlyMemory<byte>(File.ReadAllBytes(filename)).Slice(5);
                        while (b.Length != 0)
                        {
                            deserialize<Crystal2[]>(b, out var byteRead).AsParallel().Select(c2 => dataTable.CreateRow(c2))
                                        .ToList().ForEach(r => dataTable.Rows.Add(r));
                            ReadDatabaseWorker.ReportProgress(0, report(dataTable.Rows.Count, total, sw.ElapsedMilliseconds, "Loading database..."));
                            b = b.Slice(byteRead);
                        }
                    }
                    else if (flag == 200)//分割ファイルの時
                    {
                        var fileNum = readInt(fs);
                        var fileNames = Enumerable.Range(0, fileNum).Select(i =>
                                $"{filename.Remove(filename.Length - 5, 5)}\\{Path.GetFileNameWithoutExtension(filename)}.{i:000}").AsParallel();

                        fileNames.ForAll(fn =>
                        {
                            var b = new ReadOnlyMemory<byte>(File.ReadAllBytes(fn));
                            while (b.Length != 0)
                            {
                                var rows = Array.ConvertAll(deserialize<Crystal2[]>(b, out var byteRead), dataTable.CreateRow);
                                rwlock.EnterWriteLock();
                                try { foreach (var r in rows) dataTable.Rows.Add(r); }
                                finally { rwlock.ExitWriteLock(); }
                                ReadDatabaseWorker.ReportProgress(0, report(dataTable.Rows.Count, total, sw.ElapsedMilliseconds, "Loading database..."));
                                b = b.Slice(byteRead);
                            }
                        });
                    }
                }
                else
                    return;
            }
            catch
            {
                MessageBox.Show("Failed to load database. Sorry.");
            }
            bindingSource.Position = 0;
        }

        private void ReadDatabaseWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            (double progress, string message) = ((double Progress, string Message))e.UserState;
            ProgressChanged?.Invoke(sender, progress, message);
        }

        private void ReadDatabaseWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            ProgressChanged?.Invoke(sender, 1, $"Toatal loading time: {sw.ElapsedMilliseconds / 1E3:f1} sec.");
            bindingSource.DataMember = "DataTableCrystalDatabase";
        }

        #endregion

        #region データベース書き込み
        public void SaveDatabase(string fn)
        {
            this.Enabled = false;
            SaveDatabaseWorker.RunWorkerAsync(fn);
        }
        private void SaveDatabaseWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            sw.Restart();

            var fn = (string)e.Argument;

            var thresholdBytes = 30000000;
            var division = 4000;//分割単位 たぶんパフォーマンスに効く

            var total = dataTable.Count;

            using var fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
            
            writeByte(fs, 100);//とりあえず先頭に100 (分割なし)を書き込む
            writeInt(fs, total);//データの個数を書き込む

            var byteList = new List<byte>();
            var filecounter = 0;
            var subDir = fn.Remove(fn.Length - 5, 5) + "\\";
            var header = subDir + Path.GetFileNameWithoutExtension(fn) + ".";
            var fileSize = new List<long>();
            for (int i = 0; i < total; i += division)
            {
                var crystal2List = new List<Crystal2>();
                for (int j = i; j < total && j < i + division; j++)
                {
                    var c2 = deserialize<Crystal2>(((DataRowView)bindingSource[j]).Row[0]);
                    c2.jour = Crystal2.GetShortJournal(c2.jour);
                    c2.sect = Crystal2.GetShortTitle(c2.sect);
                    crystal2List.Add(c2);
                }
                byteList.AddRange(serialize(crystal2List.ToArray()));

                //最後まで来ている時で、かつ閾値以下の容量で、かつこれまで一度も分割もしていない場合
                if (i + division >= total && byteList.Count <= thresholdBytes && filecounter == 0)
                    fs.Write(byteList.ToArray(), 0, byteList.Count);//最初のファイルに書き込んで終了
                
                //最後まで来ている時か、閾値以上の容量の場合
                else if (i + division >= total || byteList.Count > thresholdBytes)
                {
                    if (filecounter == 0)
                        Directory.CreateDirectory(fn.Remove(fn.Length - 5, 5));
                    using (var fs1 = new FileStream(header + filecounter.ToString("000"), FileMode.Create, FileAccess.Write))
                        fs1.Write(byteList.ToArray(), 0, byteList.Count);
                    fileSize.Add(byteList.Count);
                    byteList.Clear();

                    filecounter++;
                }
                SaveDatabaseWorker.ReportProgress(0, report(i, total, sw.ElapsedMilliseconds, "Saving database..."));
            }

            if (filecounter > 0)//分割ファイルになった場合
            {
                //分割ファイル数書き込み
                writeInt(fs, filecounter);
                //ファイルサイズ書き込み
                for (int i = 0; i < filecounter; i++)
                    writeLong(fs, fileSize[i]);
                //チェックサムを書き込み
                for (int i = 0; i < filecounter; i++)
                    writeBytes(fs, getMD5(header + i.ToString("000")));
                //最後に先頭に戻って200を書き込み
                fs.Position = 0;
                writeByte(fs, 200);
            }
        }

        private void SaveDatabaseWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            (double progress, string message) = ((double Progress, string Message))e.UserState;
            ProgressChanged?.Invoke(sender, progress, message);
        }

        private void SaveDatabaseWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            ProgressChanged?.Invoke(sender, 1, $"Toatal saving time: {sw.ElapsedMilliseconds / 1E3:f1} sec.");
        }

        #endregion

        #region データベースの正当性チェック
        /// <summary>
        /// 分割ファイルがきちんと作成されているかをチェック
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public (bool Valid, int DataNum, int FileNum, long[] FileSizes, byte[][] CheckSums) CheckDatabaseFiles(string filename, bool checkMD5)
        {
            sw.Restart();
            var nameWithoutExt = Path.GetFileNameWithoutExtension(filename);
            var subDir = Path.GetDirectoryName(filename) + "\\" + nameWithoutExt + "\\";
            try
            {
                int dataNum = 0, fileNum = 0;
                var fileSizes = new long[0];
                var checkSums = new byte[0][];

                if (File.Exists(filename))
                {
                    //データ個数、ファイル数を取得
                    using (var fs = new FileStream(filename, FileMode.Open))
                    {
                        var head = readByte(fs);
                        dataNum = readInt(fs);
                        fileNum = readInt(fs);

                        fileSizes = new long[fileNum];
                        for (int i = 0; i < fileNum; i++)
                            fileSizes[i] = readLong(fs);

                        checkSums = new byte[fileNum][];
                        for (int i = 0; i < fileNum; i++)
                            checkSums[i] = readBytes(fs, 16);
                    }
                    if (checkMD5)
                    {
                        //md5をチェック
                        bool flag = true;
                        for (int i = 0; i < fileNum && flag; i++)
                            flag = this.checkMD5($"{subDir}{nameWithoutExt}.{i:000}", checkSums[i]);
                        return (flag, dataNum, fileNum, fileSizes, checkSums);
                    }
                    else
                        return (false, dataNum, fileNum, fileSizes, checkSums);
                }
                return (false, 0, 0, null, null);
            }
            catch
            { return (false, 0, 0, null, null); }
        }


        /// <summary>
        /// MD5を取得する。ファイルが存在しない場合はnullを返す。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private byte[] getMD5(string path)
        {
            if (!File.Exists(path))
                return null;
            using (var fs = new FileStream(path, FileMode.Open))
                return MD5.Create().ComputeHash(fs);
        }

        /// <summary>
        /// MD5とファイルが一致するかどうか調べる。
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="md5"></param>
        /// <returns></returns>
        private bool checkMD5(string path, byte[] md5)
        {
            var _md5 = getMD5(path);
            return _md5 != null && md5.Length == _md5.Length && md5.SequenceEqual(_md5);
        }
        #endregion

        #endregion

        #region 進捗状況のレポート
        /// <summary>
        /// 進捗状況
        /// </summary>
        /// <param name="current"></param>
        /// <param name="total"></param>
        /// <param name="elapsedMilliseconds">経過時間</param>
        /// <param name="message">メッセージ</param>
        /// <param name="sleep"></param>
        /// <param name="showPercentage"></param>
        /// <param name="showEllapsedTime"></param>
        /// <param name="showRemainTime"></param>
        /// <param name="digit"></param>
        private (double Progress, string Message) report(long current, long total, long elapsedMilliseconds, string message,
            bool showPercentage = true, bool showEllapsedTime = true, bool showRemainTime = true, int digit = 1)
        {
            var ratio = Math.Min(1, (double)current / total);
            var ellapsedSec = elapsedMilliseconds / 1E3;
            var format = $"f{digit}";

            if (showPercentage) message += $" Completed: {(ratio * 100).ToString(format)} %.";
            if (showEllapsedTime) message += $" Elappsed time: {ellapsedSec.ToString(format)} sec.";
            if (showRemainTime) message += $" Remaining time: {(ellapsedSec / current * (total - current)).ToString(format)} sec.";

            return (ratio, message);
        }
        #endregion

        #region 結晶の追加、削除、変更
        public void AddCrystal(Crystal2 crystal2)
        {

            dataTable.Add(crystal2);
        }

        public void ChangeCrystal(Crystal2 crystal2)
        {
            int i = bindingSource.IndexOf(bindingSource.Current);
            if (i < 0)
                AddCrystal(crystal2);
            else
                dataTable.Replace(bindingSource.Current, crystal2);
        }

        public void DeleteCurrentCrystal() => bindingSource.RemoveCurrent();

        public void ClearAll() => dataTable.Clear();
        #endregion

        #region 選択結晶が変更されたとき 
        private void bindingSource_CurrentChanged(object sender, EventArgs e)
        {
            CrystalChanged?.Invoke(sender, e);
        }
        #endregion

    }
}

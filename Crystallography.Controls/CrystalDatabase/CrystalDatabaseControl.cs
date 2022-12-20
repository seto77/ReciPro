#region using
using MemoryPack;
using MemoryPack.Compression;
using Microsoft.Scripting.Utils;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;
#endregion

namespace Crystallography.Controls;

public partial class CrystalDatabaseControl : UserControl
{
    #region フィールド、メソッド、イベント
    public void Suspend()
    {
        bindingSource.RaiseListChangedEvents = false;
        bindingSource.SuspendBinding();
    }

    public void Resume()
    {
        bindingSource.RaiseListChangedEvents = true;
        bindingSource.ResumeBinding();
        bindingSource.ResetBindings(false);
    }

    public string Filter { get => bindingSource.Filter; set => bindingSource.Filter = value; }

    public float FontSize
    {
        get => dataGridView.Font.Size;
        set => dataGridView.Font = new Font(dataGridView.Font.FontFamily, value);
    }

    readonly Stopwatch sw = new();

    static byte[] serialize<T>(T c)
    {
        using var compressor = new BrotliCompressor(System.IO.Compression.CompressionLevel.SmallestSize);
        MemoryPackSerializer.Serialize(compressor, c);

        //先頭の4バイトは、データの長さを格納する。
        var data = compressor.ToArray();
        var length = BitConverter.GetBytes(data.Length);
        var buffer = new byte[data.Length + 4];
        Buffer.BlockCopy(length, 0, buffer, 0, 4);
        Buffer.BlockCopy(data, 0, buffer, 4, data.Length);
        return buffer;
    }

    static Crystal2[] deserialize(Stream stream)
    {
        var buffer1 = new byte[4];
        stream.Read(buffer1);
        var length = BitConverter.ToInt32(buffer1);

        var buffer2 = ArrayPool<byte>.Shared.Rent(length);
        try
        {
            stream.Read(buffer2, 0, length);
            using var decompressor = new BrotliDecompressor();// Decompression(require using)
            return MemoryPackSerializer.Deserialize<Crystal2[]>(decompressor.Decompress(buffer2.AsSpan()[0..length]));
        }
        finally { ArrayPool<byte>.Shared.Return(buffer2); }
    }

    public Crystal Crystal => Crystal2.GetCrystal(Crystal2);

    public Crystal2 Crystal2 => dataSet.DataTableCrystalDatabase.Get(bindingSource.Current);

    public readonly DataSet.DataTableCrystalDatabaseDataTable Table;

    public event EventHandler CrystalChanged;

    public delegate void ProgressChangedEventHandler(object sender, double progress, string message);
    public event ProgressChangedEventHandler ProgressChanged;

    #endregion

    #region コンストラクタ
    public CrystalDatabaseControl()
    {
        InitializeComponent();
        Table = dataSet.DataTableCrystalDatabase;

        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);
    }
    #endregion

    #region データベース読み込み/書き込み関連

    #region バイト書き込み/読み込み
    private static int readInt(Stream s) => BitConverter.ToInt32(readBytes(s, 4), 0);
    private static int readByte(Stream s) => s.ReadByte();
    private static long readLong(Stream s) => BitConverter.ToInt64(readBytes(s, 8), 0);

    private static byte[] readBytes(Stream s, int length)
    {
        var bytes = new byte[length];
        s.Read(bytes, 0, bytes.Length);
        return bytes;
    }

    private static void writeInt(Stream s, int v) => s.Write(BitConverter.GetBytes(v), 0, 4);
    private static void writeLong(Stream s, long v) => s.Write(BitConverter.GetBytes(v), 0, 8);
    private static void writeByte(Stream s, byte v) => s.WriteByte(v);
    private static void writeBytes(Stream s, byte[] v) => s.Write(v, 0, v.Length);

    #endregion

    #region データベース読み込み
    public void ReadDatabase(string filename)
    {
        if (ReadDatabaseWorker.IsBusy) return;

        Suspend();
        this.Enabled = false;
        ReadDatabaseWorker.RunWorkerAsync(filename);
    }

    readonly object lockObj = new();
    private void ReadDatabaseWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        var filename = (string)e.Argument;
        try
        {
            sw.Restart();
            if (filename.ToLower().EndsWith("cdb3"))
            {
                using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                int flag = readByte(fs), total = readInt(fs);
                if (flag == 100)//単一ファイルの時
                {
                    while (fs.Length != fs.Position)
                    {
                        deserialize(fs).AsParallel().Select(Table.CreateRow).ToList().ForEach(Table.Rows.Add);
                        ReadDatabaseWorker.ReportProgress(0, report(Table.Rows.Count, total, sw.ElapsedMilliseconds, "Loading database..."));
                    }
                }
                else if (flag == 200)//分割ファイルの時
                {
                    var fileNum = readInt(fs);
                    var fileNames = Enumerable.Range(0, fileNum).Select(i =>
                            $"{filename.Remove(filename.Length - 5, 5)}\\{Path.GetFileNameWithoutExtension(filename)}.{i:000}").AsParallel();


                    fileNames.ForAll(fn =>
                    {
                        using var stream = new FileStream(fn, FileMode.Open);
                        while (stream.Length != stream.Position)
                        {
                            var rows = deserialize(stream).Select(Table.CreateRow).ToList();
                            lock (lockObj)
                                rows.ForEach(Table.AddDataTableCrystalDatabaseRow);

                            ReadDatabaseWorker.ReportProgress(0, report(Table.Rows.Count, total, sw.ElapsedMilliseconds, "Loading database..."));
                        }
                    });
                }
            }
            else
                return;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex}\r\nFailed to load database. Sorry.");
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
        Resume();
        ProgressChanged?.Invoke(sender, 1, $"Total loading time: {sw.ElapsedMilliseconds / 1E3:f1} sec.");
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

        var total = Table.Count;

        var thresholdBytes = 20000000;
        var division = 6000;//分割単位 たぶんパフォーマンスに効く

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
                crystal2List.Add((Crystal2)(((DataRowView)bindingSource[j]).Row[0]));

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
            var fileSizes = Array.Empty<long>();
            var checkSums = Array.Empty<byte[]>();

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
                        flag = CrystalDatabaseControl.checkMD5($"{subDir}{nameWithoutExt}.{i:000}", checkSums[i]);
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
    private static byte[] getMD5(string path)
    {
        if (!File.Exists(path))
            return null;
        using var fs = new FileStream(path, FileMode.Open);
        return MD5.Create().ComputeHash(fs);
    }

    /// <summary>
    /// MD5とファイルが一致するかどうか調べる。
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="md5"></param>
    /// <returns></returns>
    private static bool checkMD5(string path, byte[] md5)
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
    private static (double Progress, string Message) report(long current, long total, long elapsedMilliseconds, string message,
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
        Table.Add(crystal2);
    }

    public void AddCrystals(IEnumerable<Crystal2> crystal2)
    {
        var originalDataMember = dataGridView.DataMember;
        dataGridView.DataMember = "";

        var originalAutoSizeColumnsMode = dataGridView.AutoSizeColumnsMode;
        dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

        var originalAutoSizeRowsMode = dataGridView.AutoSizeRowsMode;
        dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        foreach (var c in crystal2.Where(e => e != null))
            Table.Add(c);

        dataGridView.DataMember = originalDataMember;
        dataGridView.AutoSizeColumnsMode = originalAutoSizeColumnsMode;
        dataGridView.AutoSizeRowsMode = originalAutoSizeRowsMode;
    }

    public void ChangeCrystal(Crystal2 crystal2)
    {
        int i = bindingSource.IndexOf(bindingSource.Current);
        if (i < 0)
            AddCrystal(crystal2);
        else
            Table.Replace(bindingSource.Current, crystal2);
    }

    public void DeleteCurrentCrystal() => bindingSource.RemoveCurrent();

    public void ClearAll() => Table.Clear();
    #endregion

    #region 選択結晶が変更されたとき 
    private void bindingSource_CurrentChanged(object sender, EventArgs e)
    {
        CrystalChanged?.Invoke(sender, e);
    }

    public void RecalculateDensityAndFormula()
    {
        var sw = new Stopwatch();
        sw.Restart();
        for (int i = 0; i < dataSet.DataTableCrystalDatabase.Count; i++)
        {
            var c = dataSet.DataTableCrystalDatabase.Get(i).ToCrystal();
            //c.GetFormulaAndDensity();
            dataSet.DataTableCrystalDatabase.Rows[i]["Formula"] = c.ChemicalFormulaSum;
            dataSet.DataTableCrystalDatabase.Rows[i]["Density"] = c.Density;

            if (i % 200 == 0)
            {
                (double progress, string message) = report(i, dataSet.DataTableCrystalDatabase.Count, sw.ElapsedMilliseconds, "Now recalculating Density and Formula. ");

                ProgressChanged?.Invoke(this, progress, message);
                Application.DoEvents();
            }
        }
    }
    #endregion

    #region resizeイベント
    bool registResizeEvent = false;
    private void CrystalDatabaseControl_Resize(object sender, EventArgs e)
    {
        if (!this.DesignMode && !registResizeEvent)
        {
            var parent = this.Parent;
            while (parent is not Form && parent != null)
                parent = parent.Parent;
            if (parent == null)
                return;
            var form = parent as Form;
            form.ResizeBegin += (s, ea) => SuspendLayout();
            form.ResizeEnd += (s, ea) => ResumeLayout();
            registResizeEvent = true;
        }
    }
    #endregion

}


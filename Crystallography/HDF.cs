using PureHDF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Crystallography;
public class HDF
{
    /// <summary>
    /// データ型が不明なときに使うダミークラス
    /// </summary>
    private class Unknown() { }

    /// <summary>
    /// IH5Datasetを使いやすくするため拡張したクラス (階層構造を把握するためにPathを追加した)
    /// </summary>
    public class H5DatasetAdv
    {
        private readonly IH5Dataset dataset;
        public string Path;
        public Type DataType;

        /// <param name="dataset"></param>
        /// <param name="path"></param>
        public H5DatasetAdv(IH5Dataset dataset, string path)
        {
            this.dataset = dataset;
            Path = path;
            DataType = Type.Class switch
            {
                H5DataTypeClass.String => typeof(string),
                H5DataTypeClass.VariableLength => typeof(string),
                H5DataTypeClass.FixedPoint => Type.Size switch
                {
                    1 => Type.FixedPoint.IsSigned ? typeof(sbyte) : typeof(byte),
                    2 => Type.FixedPoint.IsSigned ? typeof(short) : typeof(ushort),
                    4 => Type.FixedPoint.IsSigned ? typeof(int) : typeof(uint),
                    8 => Type.FixedPoint.IsSigned ? typeof(long) : typeof(ulong),
                    _ => typeof(Unknown),
                },
                H5DataTypeClass.FloatingPoint => Type.Size switch
                {
                    4 => typeof(float),
                    8 => typeof(double),
                    _ => typeof(Unknown),
                },
                _ => typeof(Unknown),
            };
        }

        //public IH5Dataset DatasetOriginal => dataset;

        /// <summary>
        /// Datasetの名前
        /// </summary>
        public string Name => dataset.Name;

        /// <summary>
        /// Datasetの次元など
        /// </summary>
        public IH5Dataspace Space => dataset.Space;

        /// <summary>
        /// データセットのデータ型など
        /// </summary>
        public IH5DataType Type => dataset.Type;

        /// <summary>
        /// データ読み込み
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Read<T>() => dataset.Read<T>();

        /// <summary>
        /// データ読み込み (多次元配列の一部を読み込むときに使用)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rank"></param>
        /// <param name="starts"></param>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public T Read<T>(ulong[] starts, ulong[] blocks) => dataset.Read<T>(new PureHDF.Selections.HyperslabSelection(starts.Length, starts, blocks), null, null);

        // <summary>
        /// データ読み込み (多次元配列の一部を読み込むときに使用)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rank"></param>
        /// <param name="starts"></param>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public T Read<T>(int[] starts, int[] blocks) => Read<T>([.. starts.Select(s => (ulong)s)], [.. blocks.Select(b => (ulong)b)]);


        /// <summary>
        /// 数値配列(1次元)データを読み込み、double配列に変換して返す
        /// </summary>
        /// <returns></returns>
        public double[] ReadAsDoubleArray()
        {
            if (DataType == typeof(sbyte))
                return [.. Read<sbyte[]>().Select(e => (double)e)];
            else if (DataType == typeof(byte))
                return [.. Read<byte[]>().Select(e => (double)e)];
            else if (DataType == typeof(short))
                return [.. Read<short[]>().Select(e => (double)e)];
            else if (DataType == typeof(ushort))
                return [.. Read<ushort[]>().Select(e => (double)e)];
            else if (DataType == typeof(int))
                return [.. Read<int[]>().Select(e => (double)e)];
            else if (DataType == typeof(uint))
                return [.. Read<uint[]>().Select(e => (double)e)];
            else if (DataType == typeof(long))
                return [.. Read<long[]>().Select(e => (double)e)];
            else if (DataType == typeof(ulong))
                return [.. Read<ulong[]>().Select(e => (double)e)];
            else if (DataType == typeof(float))
                return [.. Read<float[]>().Select(e => (double)e)];
            else if (DataType == typeof(double))
                return [.. Read<double[]>()];
            else
                return null;
        }

        /// <summary>
        /// 数値配列(2次元以上)データを読み込み、double配列(1次元)に変換して返す
        /// </summary>
        /// <returns></returns>
        public double[] ReadAsDoubleArray(ulong[] starts, ulong[] blocks)
        {
            if (DataType == typeof(sbyte))
                return [.. Read<sbyte[]>(starts,blocks).Select(e => (double)e)];
            else if (DataType == typeof(byte))
                return [.. Read<byte[]>(starts, blocks).Select(e => (double)e)];
            else if (DataType == typeof(short))
                return [.. Read<short[]>(starts, blocks).Select(e => (double)e)];
            else if (DataType == typeof(ushort))
                return [.. Read<ushort[]>(starts, blocks).Select(e => (double)e)];
            else if (DataType == typeof(int))
                return [.. Read<int[]>(starts, blocks).Select(e => (double)e)];
            else if (DataType == typeof(uint))
                return [.. Read<uint[]>(starts, blocks).Select(e => (double)e)];
            else if (DataType == typeof(long))
                return [.. Read<long[]>(starts, blocks).Select(e => (double)e)];
            else if (DataType == typeof(ulong))
                return [.. Read<ulong[]>(starts, blocks).Select(e => (double)e)];
            else if (DataType == typeof(float))
                return [.. Read<float[]>(starts, blocks).Select(e => (double)e)];
            else if (DataType == typeof(double))
                return [.. Read<double[]>(starts, blocks)];
            else
                return null;
        }

        /// <summary>
        /// 数値配列(2次元以上)データを読み込み、double配列(1次元)に変換して返す
        /// </summary>
        /// <returns></returns>
        public double[] ReadAsDoubleArray(int[] starts, int[] blocks)
            =>ReadAsDoubleArray([.. starts.Select(s => (ulong)s)], [.. blocks.Select(b => (ulong)b)]);

        /// <summary>
        /// データ読み込み (文字列専用)
        /// </summary>
        /// <returns></returns>
        public string ReadStr() => dataset.Read<string>();

        /// <summary>
        /// データ(形式は問わないが、配列はNG)を読み込み文字列として返す
        /// </summary>
        /// <returns></returns>
        public string ReadAsStr()
        {
            if (DataType == typeof(string))
                return Read<string>();
            else if (DataType == typeof(sbyte))
                return Read<sbyte>().ToString();
            else if (DataType == typeof(byte))
                return Read<byte>().ToString();
            else if (DataType == typeof(short))
                return Read<short>().ToString();
            else if (DataType == typeof(ushort))
                return Read<ushort>().ToString();
            else if (DataType == typeof(int))
                return Read<int>().ToString();
            else if (DataType == typeof(uint))
                return Read<uint>().ToString();
            else if (DataType == typeof(long))
                return Read<long>().ToString();
            else if (DataType == typeof(ulong))
                return Read<ulong>().ToString();
            else if (DataType == typeof(float))
                return Read<float>().ToString();
            else if (DataType == typeof(double))
                return Read<double>().ToString();
            else
                return "";
        }
    }

    /// <summary>
    /// HDFファイルに含まれるDatasetのリスト
    /// </summary>
    public List<H5DatasetAdv> Datasets = [];

    /// <summary>
    /// pathで指定したデータセットを取得する
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public H5DatasetAdv GetDataset(string path)
    {
        var dataset = Datasets.FirstOrDefault(d => d.Path == path);
        return (dataset!=null && path == dataset.Path) ? dataset : null;
    }

    /// <summary>
    /// HDFクラスのコンストラクタ
    /// </summary>
    /// <param name="filename"></param>
    public HDF(string filename)
    {
        // 全てのデータセットを再帰的に取得するためのローカル関数
        void addDatasetRecursively (IH5Group group, string path)
        {
            List<IH5Dataset> datasets = [];
            List<IH5Group> groups = [];
            foreach (var obj in group.Children())
            {
                if (obj is IH5Group childGroup)
                    groups.Add(childGroup);
                else if (obj is IH5Dataset childDataset)
                    datasets.Add(childDataset);
            }
            Datasets.AddRange(datasets.Select(d => new H5DatasetAdv(d, $"{path}/{d.Name}")));

            foreach (var childGroup in groups)
                addDatasetRecursively(childGroup, $"{path}/{childGroup.Name}");
        }

        var file = H5File.OpenRead(filename);
        addDatasetRecursively(file.Group("/"), "");

        #region コメントアウト
        //var rootId = H5G.open(h5, "/");

        //List<string> datasetNames = new();
        //List<string> groupNames = new();

        //H5O.visit(h5, H5.index_t.NAME, H5.iter_order_t.INC, new H5O.iterate_t(
        //        delegate (long objectId, IntPtr namePtr, ref H5O.info_t info, IntPtr op_data)
        //        {
        //            var objectName = Marshal.PtrToStringAnsi(namePtr);
        //            var gInfo = new H5O.info_t();
        //            H5O.get_info_by_name(objectId, objectName, ref gInfo);

        //            if (gInfo.type == H5O.type_t.DATASET)
        //                datasetNames.Add("/" + objectName);
        //            else if (gInfo.type == H5O.type_t.GROUP)
        //                groupNames.Add("/" + objectName);
        //            return 0;
        //        })
        //        ,new IntPtr());

        //H5G.close(rootId);

        //foreach (var group in groupNames)
        //    if (datasetNames.Contains(group))
        //        datasetNames.Remove(group);

        //groupNames[0] = "/";

        //foreach (var group in groupNames)
        //{
        //    var depth = group == "/" ? 0 : group.Count(g => g == '/');
        //    var parent = depth == 1 ? "/" : group[..group.LastIndexOf('/')];

        //    Paths.Add((group, groupNames.FindIndex(name => name == parent), depth - 1));
        //}

        //foreach (var dataset in datasetNames)
        //{
        //    var depth = dataset == "/" ? 0 : dataset.Count(g => g == '/');
        //    var parent = depth == 1 ? "/" : dataset[..dataset.LastIndexOf('/')];
        //    Datasets.Add((dataset, groupNames.FindIndex(name => name == parent), depth - 1));
        //}
        #endregion
    }

    #region コメントアウト
    //public bool Move(string path)
    //{
    //    if (path.Length == 0)
    //        return false;

    //    if (path == "/")
    //    {
    //        currentIndex = 0;
    //        return true;
    //    }

    //    path = path.TrimEnd(new[] { '/' });

    //    if (!path.StartsWith('/'))
    //        path = Current.Name == "/" ? '/' + path : $"{Current.Name}/{path}";

    //    var index = Paths.FindIndex(g => g.Name == path);
    //    if (index == -1)
    //        return false;
    //    else
    //    {
    //        currentIndex = index;
    //        return true;
    //    }
    //}
    //public void MoveUp(int n = 1)
    //{
    //    for (int i = 0; i < n; i++)
    //        if (Current.Parent != -1)
    //            currentIndex = Current.Parent;
    //}



    ///// <summary>
    ///// 現在のPathの下にあるGroupを取得
    ///// </summary>
    ///// <returns></returns>
    //public string[] GetGroups()
    //{
    //    return Paths.Where(g => g.Parent == currentIndex).Select(g => g.Name.Replace(Current.Name + "/", "")).ToArray();
    //}

    //public string[] GetDatasets()
    //{
    //    return Datasets.Where(d => d.Parent == currentIndex).Select(d => d.Name.Replace(Current.Name + "/", "")).ToArray();
    //}

    private (Type type, int[] dim, byte[] buffer, Func<byte[], int, object> func) getValuePrimitive(string dataset)
    {

        //if (!dataset.StartsWith('/'))
        //    dataset = Current.Name == "/" ? "/" + dataset : Current.Name + "/" + dataset;

        //if (Datasets.Count(d => d.Name == dataset) != 1)
        //    return (default, default, default, default);

        //var dsetID = H5D.open(h5, dataset);
        ////var strageSize = H5D.get_storage_size(dset_id);

        //var typeID = H5D.get_type(dsetID);
        //var typeSize = H5T.get_size(typeID).ToInt32();
        //var typeClass = H5T.get_class(typeID);
        //var typeSign = H5T.get_sign(typeID);

        //var spaceID = H5D.get_space(dsetID);
        //var spacePoints = H5S.get_simple_extent_npoints(spaceID);
        ////var extentType = H5S.get_simple_extent_type(spaceID);

        //byte[] strBuffer = new byte[spacePoints * typeSize];
        //var pinnedArray = GCHandle.Alloc(strBuffer, GCHandleType.Pinned);
        //H5D.read(dsetID, typeID, spaceID, H5S.ALL, H5S.ALL, pinnedArray.AddrOfPinnedObject());
        //pinnedArray.Free();
        //H5D.close(dsetID);

        //var dims = Array.Empty<int>();
        //if (spacePoints > 1)
        //{
        //    var spaceDim = H5S.get_simple_extent_ndims(spaceID);
        //    var dimsTemp = new ulong[spaceDim];

        //    H5S.get_simple_extent_dims(spaceID, dimsTemp, new ulong[spaceDim]);
        //    dims = new int[spaceDim];
        //    for (int i = 0; i < dimsTemp.Length; i++)
        //        dims[i] = (int)dimsTemp[i];
        //}

        //if (typeClass == H5T.class_t.STRING)
        //    return (typeof(string), dims, strBuffer, (bytes, start) => System.Text.Encoding.ASCII.GetString(bytes));

        //else if (typeClass == H5T.class_t.FLOAT)
        //{
        //    if (typeSize == 4)
        //        return (typeof(float), dims, strBuffer, (bytes, start) => BitConverter.ToSingle(bytes, start));
        //    else if (typeSize == 8)
        //        return (typeof(double), dims, strBuffer, (bytes, start) => BitConverter.ToDouble(bytes, start));
        //}
        //else if (typeClass == H5T.class_t.INTEGER)
        //{
        //    if (typeSign == H5T.sign_t.SGN_2)
        //    {
        //        if (typeSize == 2)
        //            return (typeof(short), dims, strBuffer, (bytes, start) => BitConverter.ToInt16(bytes, start));
        //        else if (typeSize == 4)
        //            return (typeof(int), dims, strBuffer, (bytes, start) => BitConverter.ToInt32(bytes, start));
        //        else if (typeSize == 8)
        //            return (typeof(long), dims, strBuffer, (bytes, start) => BitConverter.ToInt64(bytes, start));
        //    }
        //    else
        //    {
        //        if (typeSize == 2)
        //            return (typeof(ushort), dims, strBuffer, (bytes, start) => BitConverter.ToUInt16(bytes, start));
        //        else if (typeSize == 4)
        //            return (typeof(uint), dims, strBuffer, (bytes, start) => BitConverter.ToUInt32(bytes, start));
        //        else if (typeSize == 8)
        //            return (typeof(ulong), dims, strBuffer, (bytes, start) => BitConverter.ToUInt64(bytes, start));
        //    }

        //}
        return (default, default, default, default);
    }

    /// <summary>
    /// datasetを指定して、単一の値(0次元)を得る
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataset"></param>
    /// <returns></returns>
    //public (T Value, bool Result) GetValue0<T>(string dataset)
    //{
    //    var (type, dim, buffer, func) = getValuePrimitive(dataset);

    //    if (dim != null && dim.Length == 0 && typeof(T) == type)
    //        return ((T)func(buffer, 0), true);
    //    else
    //        return (default, false);
    //}

    /// <summary>
    /// datasetを指定して、1次元配列を得る
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataset"></param>
    /// <returns></returns>
    //public unsafe (T[] Value, bool result) GetValue1<T>(string dataset)
    //{
    //    var (type, dim, buffer, func) = getValuePrimitive(dataset);
    //    if (dim != null && dim.Length == 1 && dim[0] != 0 && typeof(T) == type)
    //    {
    //        var data = new T[dim[0]];
    //        var size = Marshal.SizeOf(data[0]);

    //        for (int i = 0; i < dim[0]; i++)
    //            data[i] = (T)func(buffer, i * size);

    //        return (data, true);
    //    }
    //    else
    //        return (default, false);
    //}

    /// <summary>
    /// datasetを指定して、2次元配列を得る
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataset"></param>
    /// <returns></returns>
    //public unsafe (T[][] Value, bool result) GetValue2<T>(string dataset)
    //{
    //    var (type, dim, buffer, func) = getValuePrimitive(dataset);
    //    if (dim != null && dim.Length == 2 && dim[0] != 0 && typeof(T) == type)
    //    {
    //        var data = new T[dim[0]][];
    //        for (int i = 0; i < dim[0]; i++)
    //        {
    //            data[i] = new T[dim[1]];
    //            var size = Marshal.SizeOf(data[0][0]);
    //            for (int j = 0; j < dim[1]; j++)
    //                data[i][j] = (T)func(buffer, i * dim[1] * size + j * size);
    //        }
    //        return (data, true);
    //    }
    //    else
    //        return (default, false);
    //}
    #endregion

}

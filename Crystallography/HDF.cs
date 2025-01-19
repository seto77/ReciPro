using HDF.PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Crystallography;

public class HDF
{
    public List<(string Name, int Parent, int Depth)> Datasets { get; set; } = new List<(string Name, int Parent, int Depth)>();
    public List<(string Name, int Parent, int Depth)> Paths { get; set; } = new List<(string Name, int Parent, int Depth)>();

    private readonly long h5;

    private int currentIndex { get; set; } = 0;

    public (string Name, int Parent, int Depth) Current { get => Paths[currentIndex]; }


    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="filename"></param>
    public HDF(string filename)
    {
        h5 = H5F.open(filename, H5F.ACC_RDONLY);

        var rootId = H5G.open(h5, "/");

        List<string> datasetNames = new();
        List<string> groupNames = new();


        H5O.visit(h5, H5.index_t.NAME, H5.iter_order_t.INC, new H5O.iterate_t(
                delegate (long objectId, IntPtr namePtr, ref H5O.info_t info, IntPtr op_data)
                {
                    var objectName = Marshal.PtrToStringAnsi(namePtr);
                    var gInfo = new H5O.info_t();
                    H5O.get_info_by_name(objectId, objectName, ref gInfo);

                    if (gInfo.type == H5O.type_t.DATASET)
                        datasetNames.Add("/" + objectName);
                    else if (gInfo.type == H5O.type_t.GROUP)
                        groupNames.Add("/" + objectName);
                    return 0;
                })
                , new IntPtr());

        H5G.close(rootId);

        foreach (var group in groupNames)
            if (datasetNames.Contains(group))
                datasetNames.Remove(group);

        groupNames[0] = "/";

        foreach (var group in groupNames)
        {
            var depth = group == "/" ? 0 : group.Count(g => g == '/');
            var parent = depth == 1 ? "/" : group[..group.LastIndexOf('/')];

            Paths.Add((group, groupNames.FindIndex(name => name == parent), depth - 1));
        }

        foreach (var dataset in datasetNames)
        {
            var depth = dataset == "/" ? 0 : dataset.Count(g => g == '/');
            var parent = depth == 1 ? "/" : dataset[..dataset.LastIndexOf('/')];
            Datasets.Add((dataset, groupNames.FindIndex(name => name == parent), depth - 1));
        }

    }

    public bool Move(string path)
    {
        if (path.Length == 0)
            return false;

        if (path == "/")
        {
            currentIndex = 0;
            return true;
        }

        path = path.TrimEnd(new[] { '/' });

        if (!path.StartsWith('/'))
            path = Current.Name == "/" ? '/' + path : $"{Current.Name}/{path}";

        var index = Paths.FindIndex(g => g.Name == path);
        if (index == -1)
            return false;
        else
        {
            currentIndex = index;
            return true;
        }
    }
    public void MoveUp(int n = 1)
    {
        for (int i = 0; i < n; i++)
            if (Current.Parent != -1)
                currentIndex = Current.Parent;
    }



    /// <summary>
    /// 現在のPathの下にあるGroupを取得
    /// </summary>
    /// <returns></returns>
    public string[] GetGroups()
    {
        return Paths.Where(g => g.Parent == currentIndex).Select(g => g.Name.Replace(Current.Name + "/", "")).ToArray();
    }

    public string[] GetDatasets()
    {
        return Datasets.Where(d => d.Parent == currentIndex).Select(d => d.Name.Replace(Current.Name + "/", "")).ToArray();
    }

    private (Type type, int[] dim, byte[] buffer, Func<byte[], int, object> func) getValuePrimitive(string dataset)
    {

        if (!dataset.StartsWith('/'))
            dataset = Current.Name == "/" ? "/" + dataset : Current.Name + "/" + dataset;

        if (Datasets.Count(d => d.Name == dataset) != 1)
            return (default, default, default, default);




        var dsetID = H5D.open(h5, dataset);
        //var strageSize = H5D.get_storage_size(dset_id);

        var typeID = H5D.get_type(dsetID);
        var typeSize = H5T.get_size(typeID).ToInt32();
        var typeClass = H5T.get_class(typeID);
        var typeSign = H5T.get_sign(typeID);

        var spaceID = H5D.get_space(dsetID);
        var spacePoints = H5S.get_simple_extent_npoints(spaceID);
        //var extentType = H5S.get_simple_extent_type(spaceID);

        byte[] strBuffer = new byte[spacePoints * typeSize];
        var pinnedArray = GCHandle.Alloc(strBuffer, GCHandleType.Pinned);
        H5D.read(dsetID, typeID, spaceID, H5S.ALL, H5S.ALL, pinnedArray.AddrOfPinnedObject());
        pinnedArray.Free();
        H5D.close(dsetID);

        var dims = Array.Empty<int>();
        if (spacePoints > 1)
        {
            var spaceDim = H5S.get_simple_extent_ndims(spaceID);
            var dimsTemp = new ulong[spaceDim];

            H5S.get_simple_extent_dims(spaceID, dimsTemp, new ulong[spaceDim]);
            dims = new int[spaceDim];
            for (int i = 0; i < dimsTemp.Length; i++)
                dims[i] = (int)dimsTemp[i];
        }

        if (typeClass == H5T.class_t.STRING)
            return (typeof(string), dims, strBuffer, (bytes, start) => System.Text.Encoding.ASCII.GetString(bytes));

        else if (typeClass == H5T.class_t.FLOAT)
        {
            if (typeSize == 4)
                return (typeof(float), dims, strBuffer, (bytes, start) => BitConverter.ToSingle(bytes, start));
            else if (typeSize == 8)
                return (typeof(double), dims, strBuffer, (bytes, start) => BitConverter.ToDouble(bytes, start));
        }
        else if (typeClass == H5T.class_t.INTEGER)
        {
            if (typeSign == H5T.sign_t.SGN_2)
            {
                if (typeSize == 2)
                    return (typeof(short), dims, strBuffer, (bytes, start) => BitConverter.ToInt16(bytes, start));
                else if (typeSize == 4)
                    return (typeof(int), dims, strBuffer, (bytes, start) => BitConverter.ToInt32(bytes, start));
                else if (typeSize == 8)
                    return (typeof(long), dims, strBuffer, (bytes, start) => BitConverter.ToInt64(bytes, start));
            }
            else
            {
                if (typeSize == 2)
                    return (typeof(ushort), dims, strBuffer, (bytes, start) => BitConverter.ToUInt16(bytes, start));
                else if (typeSize == 4)
                    return (typeof(uint), dims, strBuffer, (bytes, start) => BitConverter.ToUInt32(bytes, start));
                else if (typeSize == 8)
                    return (typeof(ulong), dims, strBuffer, (bytes, start) => BitConverter.ToUInt64(bytes, start));
            }

        }
        return (default, default, default, default);
    }

    /// <summary>
    /// datasetを指定して、単一の値(0次元)を得る
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataset"></param>
    /// <returns></returns>
    public (T Value, bool Result) GetValue0<T>(string dataset)
    {
        var (type, dim, buffer, func) = getValuePrimitive(dataset);

        if (dim != null && dim.Length == 0 && typeof(T) == type)
            return ((T)func(buffer, 0), true);
        else
            return (default, false);
    }

    /// <summary>
    /// datasetを指定して、1次元配列を得る
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataset"></param>
    /// <returns></returns>
    public unsafe (T[] Value, bool result) GetValue1<T>(string dataset)
    {
        var (type, dim, buffer, func) = getValuePrimitive(dataset);
        if (dim != null && dim.Length == 1 && dim[0] != 0 && typeof(T) == type)
        {
            var data = new T[dim[0]];
            var size = Marshal.SizeOf(data[0]);

            for (int i = 0; i < dim[0]; i++)
                data[i] = (T)func(buffer, i * size);

            return (data, true);
        }
        else
            return (default, false);
    }

    /// <summary>
    /// datasetを指定して、2次元配列を得る
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataset"></param>
    /// <returns></returns>
    public unsafe (T[][] Value, bool result) GetValue2<T>(string dataset)
    {
        var (type, dim, buffer, func) = getValuePrimitive(dataset);
        if (dim != null && dim.Length == 2 && dim[0] != 0 && typeof(T) == type)
        {
            var data = new T[dim[0]][];
            for (int i = 0; i < dim[0]; i++)
            {
                data[i] = new T[dim[1]];
                var size = Marshal.SizeOf(data[0][0]);
                for (int j = 0; j < dim[1]; j++)
                    data[i][j] = (T)func(buffer, i * dim[1] * size + j * size);
            }
            return (data, true);
        }
        else
            return (default, false);
    }

}

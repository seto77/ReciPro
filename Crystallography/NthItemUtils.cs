using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

//
//https://github.com/nenoNaninu/NthItemUtils/blob/master/NthItemUtils/NthItemUtils.cs
//

namespace Crystallography;


public readonly struct ItemWithIndex<T>(T item, int index)
{
    public readonly T Item = item;
    public readonly int Index = index;
}

public static class NthItemExtensions
{
    /// <param name="n">0 ~ source.Count - 1</param>
    public static ItemWithIndex<T> NthLargest<T>(this IReadOnlyList<T> source, int n) where T : IComparable<T>
    {
        return source.NthSmallest(source.Count - 1 - n);
    }

    /// <param name="n">0 ~ source.Count - 1</param>
    public static ItemWithIndex<T> NthSmallest<T>(this IReadOnlyList<T> source, int n) where T : IComparable<T>
    {
        var pool = ArrayPool<int>.Shared.Rent(source.Count);

        try
        {
            var indices = pool.AsSpan(0, source.Count);
            QuickSelect.Iota(indices);
            QuickSelect.Execute(source, indices, n);

            return new ItemWithIndex<T>(source[indices[n]], indices[n]);
        }
        finally
        {
            ArrayPool<int>.Shared.Return(pool);
        }
    }

    /// <param name="n">0 ~ source.Count - 1</param>
    public static ItemWithIndex<T> NthLargest<T>(this Span<T> source, int n) where T : IComparable<T>
    {
        return source.NthSmallest(source.Length - 1 - n);
    }

    /// <param name="n">0 ~ source.Length - 1</param>
    public static ItemWithIndex<T> NthSmallest<T>(this Span<T> source, int n) where T : IComparable<T>
    {
        var pool = ArrayPool<int>.Shared.Rent(source.Length);

        try
        {
            var indices = pool.AsSpan(0, source.Length);
            QuickSelect.Iota(indices);
            QuickSelect.Execute<T>(source, indices, n);

            return new ItemWithIndex<T>(source[indices[n]], indices[n]);
        }
        finally
        {
            ArrayPool<int>.Shared.Return(pool);
        }
    }

    /// <param name="n">0 ~ source.Count - 1</param>
    public static ItemWithIndex<T> NthLargest<T>(this ReadOnlySpan<T> source, int n) where T : IComparable<T>
    {
        return source.NthSmallest(source.Length - 1 - n);
    }

    /// <param name="n">0 ~ source.Length - 1</param>
    public static ItemWithIndex<T> NthSmallest<T>(this ReadOnlySpan<T> source, int n) where T : IComparable<T>
    {
        var pool = ArrayPool<int>.Shared.Rent(source.Length);

        try
        {
            var indices = pool.AsSpan(0, source.Length);
            QuickSelect.Iota(indices);
            QuickSelect.Execute(source, indices, n);

            return new ItemWithIndex<T>(source[indices[n]], indices[n]);
        }
        finally
        {
            ArrayPool<int>.Shared.Return(pool);
        }
    }

    /// <param name="n">0 ~ source.Count - 1</param>
    public static ItemWithIndex<T> NthLargest<T>(this IReadOnlyList<T> source, int n, IComparer<T> comparer)
    {
        return source.NthSmallest(source.Count - 1 - n, comparer);
    }

    /// <param name="n">0 ~ source.Count - 1</param>
    public static ItemWithIndex<T> NthSmallest<T>(this IReadOnlyList<T> source, int n, IComparer<T> comparer)
    {
        var pool = ArrayPool<int>.Shared.Rent(source.Count);

        try
        {
            var indices = pool.AsSpan(0, source.Count);
            QuickSelect.Iota(indices);
            QuickSelect.Execute(source, indices, n, comparer);

            return new ItemWithIndex<T>(source[indices[n]], indices[n]);
        }
        finally
        {
            ArrayPool<int>.Shared.Return(pool);
        }
    }

    /// <param name="n">0 ~ source.Count - 1</param>
    public static ItemWithIndex<T> NthLargest<T>(this Span<T> source, int n, IComparer<T> comparer)
    {
        return source.NthSmallest(source.Length - 1 - n, comparer);
    }

    /// <param name="n">0 ~ source.Length - 1</param>
    public static ItemWithIndex<T> NthSmallest<T>(this Span<T> source, int n, IComparer<T> comparer)
    {
        var pool = ArrayPool<int>.Shared.Rent(source.Length);

        try
        {
            var indices = pool.AsSpan(0, source.Length);
            QuickSelect.Iota(indices);
            QuickSelect.Execute<T>(source, indices, n, comparer);

            return new ItemWithIndex<T>(source[indices[n]], indices[n]);
        }
        finally
        {
            ArrayPool<int>.Shared.Return(pool);
        }
    }

    /// <param name="n">0 ~ source.Count - 1</param>
    public static ItemWithIndex<T> NthLargest<T>(this ReadOnlySpan<T> source, int n, IComparer<T> comparer)
    {
        return source.NthSmallest(source.Length - 1 - n, comparer);
    }

    /// <param name="n">0 ~ source.Length - 1</param>
    public static ItemWithIndex<T> NthSmallest<T>(this ReadOnlySpan<T> source, int n, IComparer<T> comparer)
    {
        var pool = ArrayPool<int>.Shared.Rent(source.Length);

        try
        {
            var indices = pool.AsSpan(0, source.Length);
            QuickSelect.Iota(indices);
            QuickSelect.Execute(source, indices, n, comparer);

            return new ItemWithIndex<T>(source[indices[n]], indices[n]);
        }
        finally
        {
            ArrayPool<int>.Shared.Return(pool);
        }
    }

    #region Max

    public static ItemWithIndex<T> MaxWithIndex<T>(this IReadOnlyList<T> source) where T : IComparable<T>
    {
        T maxValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Count; i++)
        {
            if (Comparer<T>.Default.Compare(source[i], maxValue) > 0)
            {
                maxValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(maxValue, index);
    }

    public static ItemWithIndex<T> MaxWithIndex<T>(this Span<T> source) where T : IComparable<T>
    {
        T maxValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Length; i++)
        {
            if (Comparer<T>.Default.Compare(source[i], maxValue) > 0)
            {
                maxValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(maxValue, index);
    }

    public static ItemWithIndex<T> MaxWithIndex<T>(this ReadOnlySpan<T> source) where T : IComparable<T>
    {
        T maxValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Length; i++)
        {
            if (Comparer<T>.Default.Compare(source[i], maxValue) > 0)
            {
                maxValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(maxValue, index);
    }

    public static ItemWithIndex<T> MaxWithIndex<T>(this IReadOnlyList<T> source, IComparer<T> comparer)
    {
        T maxValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Count; i++)
        {
            if (comparer.Compare(source[i], maxValue) > 0)
            {
                maxValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(maxValue, index);
    }

    public static ItemWithIndex<T> MaxWithIndex<T>(this Span<T> source, IComparer<T> comparer)
    {
        T maxValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Length; i++)
        {
            if (comparer.Compare(source[i], maxValue) > 0)
            {
                maxValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(maxValue, index);
    }

    public static ItemWithIndex<T> MaxWithIndex<T>(this ReadOnlySpan<T> source, IComparer<T> comparer)
    {
        T maxValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Length; i++)
        {
            if (comparer.Compare(source[i], maxValue) > 0)
            {
                maxValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(maxValue, index);
    }

    #endregion //Max

    #region Min
    public static ItemWithIndex<T> MinWithIndex<T>(this IReadOnlyList<T> source) where T : IComparable<T>
    {
        T minValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Count; i++)
        {
            if (Comparer<T>.Default.Compare(source[i], minValue) < 0)
            {
                minValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(minValue, index);
    }

    public static ItemWithIndex<T> MinWithIndex<T>(this Span<T> source) where T : IComparable<T>
    {
        T minValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Length; i++)
        {
            if (Comparer<T>.Default.Compare(source[i], minValue) < 0)
            {
                minValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(minValue, index);
    }

    public static ItemWithIndex<T> MinWithIndex<T>(this ReadOnlySpan<T> source) where T : IComparable<T>
    {
        T minValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Length; i++)
        {
            if (Comparer<T>.Default.Compare(source[i], minValue) < 0)
            {
                minValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(minValue, index);
    }

    public static ItemWithIndex<T> MinWithIndex<T>(this IReadOnlyList<T> source, IComparer<T> comparer)
    {
        T minValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Count; i++)
        {
            if (comparer.Compare(source[i], minValue) < 0)
            {
                minValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(minValue, index);
    }

    public static ItemWithIndex<T> MinWithIndex<T>(this Span<T> source, IComparer<T> comparer)
    {
        T minValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Length; i++)
        {
            if (comparer.Compare(source[i], minValue) < 0)
            {
                minValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(minValue, index);
    }

    public static ItemWithIndex<T> MinWithIndex<T>(this ReadOnlySpan<T> source, IComparer<T> comparer)
    {
        T minValue = source[0];
        int index = 0;

        for (int i = 1; i < source.Length; i++)
        {
            if (comparer.Compare(source[i], minValue) < 0)
            {
                minValue = source[i];
                index = i;
            }
        }

        return new ItemWithIndex<T>(minValue, index);
    }

    #endregion // Min
}

public static class QuickSelect
{
    /// <param name="indices"></param>
    public static void Iota(Span<int> indices)
    {
        for (int i = 0; i < indices.Length; i++)
            indices[i] = i;
    }

    #region Swap
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Swap(Span<int> indices, int i, int j)
    {
        if (i == j) return;

        (indices[j], indices[i]) = (indices[i], indices[j]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Swap<T>(Span<T> indices, int i, int j)
    {
        if (i == j) return;

        (indices[j], indices[i]) = (indices[i], indices[j]);
    }
    #endregion

    #region Partition
    private static int Partition<T>(ReadOnlySpan<T> source, Span<int> indices, int begin, int count, IComparer<T> comparer)
    {
        if (count <= 1)
            return begin;

        int l = begin;
        int r = begin + count - 2;
        int endIndex = begin + count - 1;

        T pivot = source[indices[endIndex]];

        while (l <= r)
        {
            while (l < endIndex && comparer.Compare(source[indices[l]], pivot) < 0)
                l++;

            while (begin <= r && comparer.Compare(source[indices[r]], pivot) >= 0)
                r--;

            if (r < l)
                break;

            Swap(indices, l, r);

            l++;
            r--;
        }

        Swap(indices, l, endIndex);

        return l;
    }

    private static int Partition<T>(ReadOnlySpan<T> source, Span<int> indices, int begin, int count, Comparison<T> comparison)
    {
        if (count <= 1)
            return begin;

        int l = begin;
        int r = begin + count - 2;
        int endIndex = begin + count - 1;

        T pivot = source[indices[endIndex]];

        while (l <= r)
        {
            while (l < endIndex && comparison(source[indices[l]], pivot) < 0)
                l++;

            while (begin <= r && comparison(source[indices[r]], pivot) >= 0)
                r--;

            if (r < l)
                break;

            Swap(indices, l, r);

            l++;
            r--;
        }

        Swap(indices, l, endIndex);
        return l;
    }

    private static int Partition<T>(Span<T> source, int begin, int count, Comparison<T> comparison)
    {
        if (count <= 1)
            return begin;

        int l = begin;
        int r = begin + count - 2;
        int endIndex = begin + count - 1;

        T pivot = source[endIndex];

        while (l <= r)
        {
            while (l < endIndex && comparison(source[l], pivot) < 0)
                l++;

            while (begin <= r && comparison(source[r], pivot) >= 0)
                r--;

            if (r < l)
                break;

            (source[l], source[r]) = (source[r], source[l]);
            //Swap(source, l, r);

            l++;
            r--;
        }

        (source[l], source[endIndex]) = (source[endIndex], source[l]);
        //Swap(source, l, endIndex);
        
        return l;
    }


    private static int Partition<T>(IReadOnlyList<T> source, Span<int> indices, int begin, int count, IComparer<T> comparer)
    {
        if (count <= 1)
            return begin;

        int l = begin;
        int r = begin + count - 2;
        int endIndex = begin + count - 1;

        T pivot = source[indices[endIndex]];

        while (l <= r)
        {
            while (l < endIndex && comparer.Compare(source[indices[l]], pivot) < 0)
                l++;

            while (begin <= r && comparer.Compare(source[indices[r]], pivot) >= 0)
                r--;

            if (r < l)
                break;

            Swap(indices, l, r);

            l++;
            r--;
        }

        Swap(indices, l, endIndex);

        return l;
    }

    private static int Partition<T>(IReadOnlyList<T> source, Span<int> indices, int begin, int count, Comparison<T> comparison)
    {
        if (count <= 1)
            return begin;

        int l = begin;
        int r = begin + count - 2;
        int endIndex = begin + count - 1;

        T pivot = source[indices[endIndex]];

        while (l <= r)
        {
            while (l < endIndex && comparison(source[indices[l]], pivot) < 0)
                l++;

            while (begin <= r && comparison(source[indices[r]], pivot) >= 0)
                r--;

            if (r < l)
                break;

            Swap(indices, l, r);

            l++;
            r--;
        }

        Swap(indices, l, endIndex);

        return l;
    }
    #endregion

    #region Validation
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Validation<T>(IReadOnlyList<T> source, Span<int> indices, int n)
    {
        if (source.Count <= n)
            throw new ArgumentException("n is bigger than source.Count");

        if (indices.Length <= n)
            throw new ArgumentException("n is bigger than indices.Length");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Validation<T>(ReadOnlySpan<T> source, Span<int> indices, int n)
    {
        if (source.Length <= n)
            throw new ArgumentException("n is bigger than source.Length");

        if (indices.Length <= n)
            throw new ArgumentException("n is bigger than indices.Length");
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Validation<T>(Span<T> source, Span<int> indices, int n)
    {
        if (source.Length <= n)
            throw new ArgumentException("n is bigger than source.Length");

        if (indices.Length <= n)
            throw new ArgumentException("n is bigger than indices.Length");
    }

    private static void Validation<T>(Span<T> source, int n)
    {
        if (source.Length <= n)
            throw new ArgumentException("n is bigger than source.Length");
    }
    #endregion

    #region Execute
    /// <param name="n">0 ~ source.Count - 1</param>
    public static void Execute<T>(IReadOnlyList<T> source, Span<int> indices, int n) where T : IComparable<T>
    {
        Validation(source, indices, n);
        ExecuteCore(source, indices, n, 0, indices.Length, Comparer<T>.Default);
    }

    /// <param name="n">0 ~ source.Count - 1</param>
    public static void Execute<T>(IReadOnlyList<T> source, Span<int> indices, int n, IComparer<T> comparer)
    {
        Validation(source, indices, n);
        ExecuteCore(source, indices, n, 0, indices.Length, comparer);
    }

    /// <param name="n">0 ~ source.Count - 1</param>
    public static void Execute<T>(IReadOnlyList<T> source, Span<int> indices, int n, Comparison<T> comparison)
    {
        Validation(source, indices, n);
        ExecuteCore(source, indices, n, 0, indices.Length, comparison);
    }

    /// <param name="n">0 ~ source.Length - 1</param>
    public static void Execute<T>(ReadOnlySpan<T> source, Span<int> indices, int n) where T : IComparable<T>
    {
        Validation(source, indices, n);
        ExecuteCore(source, indices, n, 0, indices.Length, Comparer<T>.Default);
    }


    /// <param name="n">0 ~ source.Length - 1</param>
    public static void Execute<T>(ReadOnlySpan<T> source, Span<int> indices, int n, IComparer<T> comparer)
    {
        Validation(source, indices, n);
        ExecuteCore(source, indices, n, 0, indices.Length, comparer);
    }

    /// <param name="n">0 ~ source.Length - 1</param>
    public static void Execute<T>(ReadOnlySpan<T> source, Span<int> indices, int n, Comparison<T> comparison)
    {
        Validation(source, indices, n);
        ExecuteCore(source, indices, n, 0, indices.Length, comparison);
    }

    /// <param name="n">0 ~ source.Length - 1</param>
    public static void Execute<T>(Span<T> source, Span<int> indices, int n, Comparison<T> comparison)
    {
        Validation(source, indices, n);
        ExecuteCore(source, indices, n, 0, indices.Length, comparison);
    }

    /// <param name="n">0 ~ source.Length - 1</param>
    public static void Execute<T>(Span<T> source, int n, Comparison<T> comparison)
    {
        Validation(source, n);
        ExecuteCore(source, n, 0, comparison);
    }
    #endregion

    #region ExecuteCore
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ExecuteCore<T>(IReadOnlyList<T> source, Span<int> indices, int n, int begin, int count, IComparer<T> comparer)
    {
        int partition = Partition(source, indices, begin, count, comparer);

        while (partition != n)
        {
            if (partition < n)
            {
                count = begin + count - partition - 1;
                begin = partition + 1;
            }
            else if (n < partition)
                count = partition - begin;

            partition = Partition(source, indices, begin, count, comparer);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ExecuteCore<T>(IReadOnlyList<T> source, Span<int> indices, int n, int begin, int count, Comparison<T> comparison)
    {
        int partition = Partition(source, indices, begin, count, comparison);

        while (partition != n)
        {
            if (partition < n)
            {
                count = begin + count - partition - 1;
                begin = partition + 1;
            }
            else if (n < partition)
                count = partition - begin;

            partition = Partition(source, indices, begin, count, comparison);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ExecuteCore<T>(ReadOnlySpan<T> source, Span<int> indices, int n, int begin, int count, IComparer<T> comparer)
    {
        int partition = Partition(source, indices, begin, count, comparer);

        while (partition != n)
        {
            if (partition < n)
            {
                count = begin + count - partition - 1;
                begin = partition + 1;
            }
            else if (n < partition)
                count = partition - begin;

            partition = Partition(source, indices, begin, count, comparer);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ExecuteCore<T>(ReadOnlySpan<T> source, Span<int> indices, int n, int begin, int count, Comparison<T> comparison)
    {
        int partition = Partition(source, indices, begin, count, comparison);

        while (partition != n)
        {
            if (partition < n)
            {
                count = begin + count - partition - 1;
                begin = partition + 1;
            }
            else if (n < partition)
                count = partition - begin;

            partition = Partition(source, indices, begin, count, comparison);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ExecuteCore<T>(Span<T> source, Span<int> indices, int n, int begin, int count, Comparison<T> comparison)
    {
        int partition = Partition(source, indices, begin, count, comparison);

        while (partition != n)
        {
            if (partition < n)
            {
                count = begin + count - partition - 1;
                begin = partition + 1;
            }
            else if (n < partition)
                count = partition - begin;

            partition = Partition(source, indices, begin, count, comparison);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ExecuteCore<T>(Span<T> source, int n, int begin, Comparison<T> comparison)
    {
        int count = source.Length;
        int partition = Partition(source, begin, count, comparison);

        while (partition != n)
        {
            if (partition < n)
            {
                count = begin + count - partition - 1;
                begin = partition + 1;
            }
            else if (n < partition)
                count = partition - begin;

            partition = Partition(source, begin, count, comparison);
        }
    }
    #endregion

}

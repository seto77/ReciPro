// 260530Cl 追加: GPL の ffmpeg(libx264/libx265) を廃し、Windows 標準の Media Foundation で
//                MP4(H.264 / H.265) を出力するエンコーダ。ネイティブ DLL の同梱が不要になり、
//                コーデック特許のライセンスも OS(Microsoft) 側が負担する。
//                依存追加なし(OS の mfplat.dll / mfreadwrite.dll を直接 P/Invoke)。
using System;
using System.Runtime.InteropServices;

namespace Crystallography.Controls; // 260530Cl ReciPro から移動 (再利用のため public 化)

/// <summary>
/// 260530Cl 追加: Media Foundation Sink Writer による H.264 / H.265 → MP4 エンコーダ。
/// フレームは「上から下・隙間なし(stride = width*4)・BGRA(32bpp)」の byte[] で渡す。
/// 使い方: new → AddFrameBgra32(...) を繰り返す → Finish()。using/Dispose で COM を解放する。
/// </summary>
public sealed class MediaFoundationVideoEncoder : IDisposable
{
    #region GUID 定数 (mfapi.h / mfobjects.h / uuids)
    private static readonly Guid MFMediaType_Video = new("73646976-0000-0010-8000-00aa00389b71");
    private static readonly Guid MFVideoFormat_H264 = new("34363248-0000-0010-8000-00aa00389b71"); // 'H264'
    private static readonly Guid MFVideoFormat_HEVC = new("43564548-0000-0010-8000-00aa00389b71"); // 'HEVC'
    private static readonly Guid MFVideoFormat_RGB32 = new("00000016-0000-0010-8000-00aa00389b71"); // D3DFMT_X8R8G8B8(=22)

    private static readonly Guid MF_MT_MAJOR_TYPE = new("48eba18e-f8c9-4687-bf11-0a74c9f96a8f");
    private static readonly Guid MF_MT_SUBTYPE = new("f7e34c9a-42e8-4714-b74b-cb29d72c35e5");
    private static readonly Guid MF_MT_AVG_BITRATE = new("20332624-fb0d-4d9e-bd0d-cbf6786c102e");
    private static readonly Guid MF_MT_INTERLACE_MODE = new("e2724bb8-e676-4806-b4b2-a8d6efb44ccd");
    private static readonly Guid MF_MT_FRAME_SIZE = new("1652c33d-d6b2-4012-b834-72030849a37d");
    private static readonly Guid MF_MT_FRAME_RATE = new("c459a2e8-3d2c-4e44-b132-fee5156c7bb0");
    private static readonly Guid MF_MT_PIXEL_ASPECT_RATIO = new("c6376a1e-8d0a-4027-be45-6d9a0ad39bb6");
    private static readonly Guid MF_MT_DEFAULT_STRIDE = new("644b4e48-1e02-4516-b0eb-c01ca9d49ac6");
    private static readonly Guid MF_MT_MPEG2_PROFILE = new("ad76a80b-2d5c-4e0b-b375-64e520137036");

    private const int MFVideoInterlace_Progressive = 2;
    private const uint MF_VERSION = 0x00020070;       // (MF_SDK_VERSION<<16)|MF_API_VERSION = Win7+
    private const uint MFSTARTUP_FULL = 0;
    private const int eAVEncH264VProfile_High = 100;  // codecapi.h
    private const int eAVEncH265VProfile_Main_420 = 1;
    // 260530Cl ICodecAPI(レート制御)用
    private static readonly Guid IID_ICodecAPI = new("901db4c7-31ce-41a2-85dc-8fa0bf41b8da");
    private static readonly Guid CODECAPI_AVEncCommonRateControlMode = new("1c0608e9-370c-4710-8a58-cb6181c42423");
    private static readonly Guid CODECAPI_AVEncCommonQuality = new("fcbf57a3-7ea5-4b0c-9644-69b40c39c391");
    private const uint eAVEncCommonRateControlMode_Quality = 3;
    private const ushort VT_UI4 = 19;
    #endregion

    #region P/Invoke (mfplat.dll / mfreadwrite.dll)
    [DllImport("mfplat.dll", ExactSpelling = true)]
    private static extern int MFStartup(uint version, uint flags);

    [DllImport("mfplat.dll", ExactSpelling = true)]
    private static extern int MFShutdown();

    [DllImport("mfplat.dll", ExactSpelling = true)]
    private static extern int MFCreateMediaType(out IMFMediaType ppMFType);

    [DllImport("mfplat.dll", ExactSpelling = true)]
    private static extern int MFCreateSample(out IMFSample ppIMFSample);

    [DllImport("mfplat.dll", ExactSpelling = true)]
    private static extern int MFCreateMemoryBuffer(int cbMaxLength, out IMFMediaBuffer ppBuffer);

    [DllImport("mfreadwrite.dll", ExactSpelling = true)]
    private static extern int MFCreateSinkWriterFromURL(
        [MarshalAs(UnmanagedType.LPWStr)] string pwszOutputURL,
        IntPtr pByteStream, IntPtr pAttributes, out IMFSinkWriter ppSinkWriter);
    #endregion

    private static bool _mfStarted;
    private static readonly object _startupLock = new(); // 260530Cl 起動の排他 (UIスレッド構築 & プールスレッドエンコードから呼ばれる)
    private static void EnsureStartup()
    {
        if (_mfStarted) return;
        lock (_startupLock)
        {
            if (_mfStarted) return;
            Check(MFStartup(MF_VERSION, MFSTARTUP_FULL), "MFStartup");
            _mfStarted = true;
        }
    }

    private readonly IMFSinkWriter _writer;
    private readonly int _streamIndex;
    private readonly int _frameSize; // = width*4*height (1フレームの MF バッファサイズ)
    private readonly long _frameDurationTicks; // 100ns 単位
    private long _timeTicks; // 次フレームの開始時刻 (100ns 単位)
    private bool _finished;

    /// <summary>指定パス(.mp4)へ H.264 / H.265 出力を開始する。フレームは AddFrameBgra32 で追加する。</summary>
    /// <param name="path">出力 MP4 のフルパス (.mp4 拡張子でコンテナが決まる)。</param>
    /// <param name="width">フレーム幅 (px、偶数推奨)。</param>
    /// <param name="height">フレーム高さ (px、偶数推奨)。</param>
    /// <param name="fps">フレームレート (frames/sec、正の値)。</param>
    /// <param name="hevc">true なら H.265(HEVC)、false なら H.264。</param>
    /// <param name="quality">品質(1-100)。Quality レート制御モードで使用。</param>
    public MediaFoundationVideoEncoder(string path, int width, int height, int fps, bool hevc, int quality = 70)
    {
        // 260530Cl public ライブラリ API として境界値を検証 (再利用時の不正引数対策)
        if (width <= 0 || height <= 0) throw new ArgumentOutOfRangeException(nameof(width), "width/height must be positive.");
        if (fps <= 0) throw new ArgumentOutOfRangeException(nameof(fps), "fps must be positive.");
        long frameBytes = (long)width * 4 * height; // long 昇格で int オーバーフローを検出
        if (frameBytes > int.MaxValue) throw new ArgumentOutOfRangeException(nameof(width), $"Frame buffer {frameBytes} bytes exceeds the 2 GiB limit.");

        EnsureStartup();
        int stride = width * 4;
        _frameSize = (int)frameBytes;
        _frameDurationTicks = 10_000_000L / fps;

        IMFMediaType outType = null, inType = null;
        try
        {
            // 出力(圧縮)メディアタイプ
            Check(MFCreateMediaType(out outType), "MFCreateMediaType(out)");
            outType.SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Video);
            outType.SetGUID(MF_MT_SUBTYPE, hevc ? MFVideoFormat_HEVC : MFVideoFormat_H264);
            // MF_MT_AVG_BITRATE は ICodecAPI が無いエンコーダ向けのフォールバック (通常は Quality モードが優先)
            outType.SetUINT32(MF_MT_AVG_BITRATE, (int)Math.Min(AutoBitrate(width, height, fps), int.MaxValue));
            outType.SetUINT32(MF_MT_INTERLACE_MODE, MFVideoInterlace_Progressive);
            outType.SetUINT32(MF_MT_MPEG2_PROFILE, hevc ? eAVEncH265VProfile_Main_420 : eAVEncH264VProfile_High);
            SetUint64Pair(outType, MF_MT_FRAME_SIZE, width, height);
            SetUint64Pair(outType, MF_MT_FRAME_RATE, fps, 1);
            SetUint64Pair(outType, MF_MT_PIXEL_ASPECT_RATIO, 1, 1);

            // 入力(非圧縮 RGB32)メディアタイプ。stride を正(=上から下)で宣言し、フレームは上下そのまま渡す。
            Check(MFCreateMediaType(out inType), "MFCreateMediaType(in)");
            inType.SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Video);
            inType.SetGUID(MF_MT_SUBTYPE, MFVideoFormat_RGB32);
            inType.SetUINT32(MF_MT_INTERLACE_MODE, MFVideoInterlace_Progressive);
            inType.SetUINT32(MF_MT_DEFAULT_STRIDE, stride); // 正=top-down。上下が反転する場合はここを負(-stride)にする
            SetUint64Pair(inType, MF_MT_FRAME_SIZE, width, height);
            SetUint64Pair(inType, MF_MT_FRAME_RATE, fps, 1);
            SetUint64Pair(inType, MF_MT_PIXEL_ASPECT_RATIO, 1, 1);

            // .mp4 拡張子からコンテナ(MPEG-4 file sink)が自動選択される。
            Check(MFCreateSinkWriterFromURL(path, IntPtr.Zero, IntPtr.Zero, out _writer), "MFCreateSinkWriterFromURL");
            Check(_writer.AddStream(outType, out _streamIndex), "AddStream");
            Check(_writer.SetInputMediaType(_streamIndex, inType, IntPtr.Zero), "SetInputMediaType");
            SetQualityRateControl(quality); // 260530Cl HEVC は MF_MT_AVG_BITRATE を見ないため Quality モードを明示 (両コーデック共通)
            Check(_writer.BeginWriting(), "BeginWriting");
        }
        catch
        {
            // 260530Cl 構築途中の例外でも _writer をリークさせない
            if (_writer != null) { Marshal.ReleaseComObject(_writer); _writer = null; }
            throw;
        }
        finally
        {
            // 260530Cl 例外有無に関わらずメディアタイプを解放
            if (outType != null) Marshal.ReleaseComObject(outType);
            if (inType != null) Marshal.ReleaseComObject(inType);
        }
    }

    /// <summary>上から下・隙間なし(width*4)の BGRA フレームを 1 枚追加する。</summary>
    public void AddFrameBgra32(byte[] bgra)
    {
        Check(MFCreateMemoryBuffer(_frameSize, out var buffer), "MFCreateMemoryBuffer");
        Check(MFCreateSample(out var sample), "MFCreateSample");
        try
        {
            Check(buffer.Lock(out var pData, out _, out _), "MediaBuffer.Lock");
            try { Marshal.Copy(bgra, 0, pData, _frameSize); }
            finally { buffer.Unlock(); }
            buffer.SetCurrentLength(_frameSize);

            sample.AddBuffer(buffer);
            sample.SetSampleTime(_timeTicks);
            sample.SetSampleDuration(_frameDurationTicks);
            _timeTicks += _frameDurationTicks;
            Check(_writer.WriteSample(_streamIndex, sample), "WriteSample");
        }
        finally
        {
            // 260530Cl WriteSample 失敗時も COM 参照を確実に解放する (try/finally 化)
            Marshal.ReleaseComObject(sample);
            Marshal.ReleaseComObject(buffer);
        }
    }

    /// <summary>ストリームを確定して MP4 を完成させる。</summary>
    public void Finish()
    {
        if (_finished) return;
        Check(_writer.Finalize_(), "Finalize");
        _finished = true;
    }

    public void Dispose()
    {
        try { if (!_finished) _writer.Finalize_(); } catch { /* 失敗時は破棄のみ */ }
        if (_writer != null) Marshal.ReleaseComObject(_writer);
        // MFShutdown はプロセスで対にする必要があるため、ここでは呼ばない(アプリ終了時に OS が回収)。
    }

    private static long AutoBitrate(int w, int h, int fps)
    {
        // 0.1 bit/pixel を目安に算出 (合成 CG の回転動画なら十分)。下限 2Mbps。
        long bps = (long)(w * (double)h * fps * 0.1);
        return Math.Max(bps, 2_000_000L);
    }

    private static void SetUint64Pair(IMFMediaType attr, Guid key, int high, int low)
        => attr.SetUINT64(key, ((long)(uint)high << 32) | (uint)low);

    private static void Check(int hr, string what)
    {
        if (hr < 0) throw new InvalidOperationException($"Media Foundation: {what} failed (HRESULT 0x{hr:X8}).");
    }

    // 260530Cl エンコーダの ICodecAPI を取得し、レート制御を Quality モードに設定する (best-effort)。
    //          HEVC は MF_MT_AVG_BITRATE を見ず既定が品質ベースのため、明示しないと H.264 より大きくなる。
    private void SetQualityRateControl(int quality)
    {
        if (_writer.GetServiceForStream(_streamIndex, Guid.Empty, IID_ICodecAPI, out var svc) < 0)
            return; // ICodecAPI 取得失敗なら既定のまま (致命的でない)
        if (svc is not ICodecAPI codec)
        {
            if (svc != null) Marshal.ReleaseComObject(svc); // 260530Cl 想定外の型でも解放
            return;
        }
        try
        {
            var mode = new PROPVARIANT { vt = VT_UI4, ulVal = eAVEncCommonRateControlMode_Quality };
            codec.SetValue(CODECAPI_AVEncCommonRateControlMode, ref mode);
            var q = new PROPVARIANT { vt = VT_UI4, ulVal = (uint)Math.Clamp(quality, 1, 100) };
            codec.SetValue(CODECAPI_AVEncCommonQuality, ref q);
        }
        catch { /* レート制御設定の失敗は無視 (既定動作にフォールバック) */ }
        finally { Marshal.ReleaseComObject(codec); }
    }

    private static bool? _hevcAvailable; // 260530Cl 初回プローブ結果をキャッシュ (MFTEnumEx を毎回回さない)
    /// <summary>このマシンで HEVC(H.265) ハードウェア/ソフトウェアエンコーダが使えるかを判定する。</summary>
    public static bool IsHevcEncoderAvailable() => _hevcAvailable ??= ProbeHevcEncoder(); // 260530Cl bool? ??= bool の結果は非nullの bool なので .Value 不要
    private static bool ProbeHevcEncoder()
    {
        try
        {
            EnsureStartup();
            var inInfo = new MFT_REGISTER_TYPE_INFO { guidMajorType = MFMediaType_Video, guidSubtype = MFVideoFormat_NV12 };
            var outInfo = new MFT_REGISTER_TYPE_INFO { guidMajorType = MFMediaType_Video, guidSubtype = MFVideoFormat_HEVC };
            // 入力(NV12)→出力(HEVC) のエンコーダ MFT を数える (sync/async/hardware すべて)。
            int hr = MFTEnumEx(MFT_CATEGORY_VIDEO_ENCODER,
                MFT_ENUM_FLAG_SYNCMFT | MFT_ENUM_FLAG_ASYNCMFT | MFT_ENUM_FLAG_HARDWARE | MFT_ENUM_FLAG_SORTANDFILTER,
                ref inInfo, ref outInfo, out var ppActivate, out int count);
            if (hr < 0) return false;
            if (ppActivate != IntPtr.Zero)
            {
                for (int i = 0; i < count; i++)
                {
                    var p = Marshal.ReadIntPtr(ppActivate, i * IntPtr.Size);
                    if (p != IntPtr.Zero) Marshal.Release(p);
                }
                Marshal.FreeCoTaskMem(ppActivate);
            }
            return count > 0;
        }
        catch { return false; }
    }

    #region HEVC 判定用の追加 P/Invoke
    private static readonly Guid MFVideoFormat_NV12 = new("3231564e-0000-0010-8000-00aa00389b71"); // 'NV12'
    private static readonly Guid MFT_CATEGORY_VIDEO_ENCODER = new("f79eac7d-e545-4387-bdee-d647d7bde42a");
    private const uint MFT_ENUM_FLAG_SYNCMFT = 0x00000001;
    private const uint MFT_ENUM_FLAG_ASYNCMFT = 0x00000002;
    private const uint MFT_ENUM_FLAG_HARDWARE = 0x00000004;
    private const uint MFT_ENUM_FLAG_SORTANDFILTER = 0x00000040;

    [StructLayout(LayoutKind.Sequential)]
    private struct MFT_REGISTER_TYPE_INFO { public Guid guidMajorType; public Guid guidSubtype; }

    [DllImport("mfplat.dll", ExactSpelling = true)]
    private static extern int MFTEnumEx(Guid guidCategory, uint flags,
        [In] ref MFT_REGISTER_TYPE_INFO pInputType, [In] ref MFT_REGISTER_TYPE_INFO pOutputType,
        out IntPtr pppMFTActivate, out int pnumMFTActivate);
    #endregion

    #region COM インターフェース宣言 (vtable 順を厳守。未使用メソッドはスロット維持のみ)
    // 260530Cl ComImport はインターフェース継承で vtable を合成しない(基底メソッドはスロットに入らない)。
    //          そのため IMFMediaType / IMFSample は IMFAttributes を継承せず、全スロットをフラットに宣言する。
    //          呼ばないメソッドは引数なしのプレースホルダ(スロット維持専用)。slot 0-2 は IUnknown。
    [ComImport, Guid("44ae0fa8-ea31-4109-8d2e-4cae4997c555"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMFMediaType // IMFAttributes をフラット展開 (SetGUID/SetUINT32/SetUINT64 のみ使用)
    {
        [PreserveSig] int GetItem();           // 3
        [PreserveSig] int GetItemType();       // 4
        [PreserveSig] int CompareItem();       // 5
        [PreserveSig] int Compare();           // 6
        [PreserveSig] int GetUINT32();         // 7
        [PreserveSig] int GetUINT64();         // 8
        [PreserveSig] int GetDouble();         // 9
        [PreserveSig] int GetGUID();           // 10
        [PreserveSig] int GetStringLength();   // 11
        [PreserveSig] int GetString();         // 12
        [PreserveSig] int GetAllocatedString();// 13
        [PreserveSig] int GetBlobSize();       // 14
        [PreserveSig] int GetBlob();           // 15
        [PreserveSig] int GetAllocatedBlob();  // 16
        [PreserveSig] int GetUnknown();        // 17
        [PreserveSig] int SetItem();           // 18
        [PreserveSig] int DeleteItem();        // 19
        [PreserveSig] int DeleteAllItems();    // 20
        // REFGUID(=const GUID&) はポインタ渡しのため LPStruct で宣言する (値渡しは不正)
        [PreserveSig] int SetUINT32([MarshalAs(UnmanagedType.LPStruct)] Guid key, int value);  // 21
        [PreserveSig] int SetUINT64([MarshalAs(UnmanagedType.LPStruct)] Guid key, long value); // 22
        [PreserveSig] int SetDouble();         // 23
        [PreserveSig] int SetGUID([MarshalAs(UnmanagedType.LPStruct)] Guid key, [MarshalAs(UnmanagedType.LPStruct)] Guid value); // 24
        // slot 25 以降 (SetString..CopyAllItems / IMFMediaType 固有) は未使用のため省略
    }

    [ComImport, Guid("c40a00f2-b93a-4d80-ae8c-5a1c634f58e4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMFSample // IMFAttributes(30 メソッド)+ IMFSample 固有 をフラット展開
    {
        // IMFAttributes slot 3-32 は未使用。スロット維持のみ。
        [PreserveSig] int _a01(); [PreserveSig] int _a02(); [PreserveSig] int _a03(); [PreserveSig] int _a04();
        [PreserveSig] int _a05(); [PreserveSig] int _a06(); [PreserveSig] int _a07(); [PreserveSig] int _a08();
        [PreserveSig] int _a09(); [PreserveSig] int _a10(); [PreserveSig] int _a11(); [PreserveSig] int _a12();
        [PreserveSig] int _a13(); [PreserveSig] int _a14(); [PreserveSig] int _a15(); [PreserveSig] int _a16();
        [PreserveSig] int _a17(); [PreserveSig] int _a18(); [PreserveSig] int _a19(); [PreserveSig] int _a20();
        [PreserveSig] int _a21(); [PreserveSig] int _a22(); [PreserveSig] int _a23(); [PreserveSig] int _a24();
        [PreserveSig] int _a25(); [PreserveSig] int _a26(); [PreserveSig] int _a27(); [PreserveSig] int _a28();
        [PreserveSig] int _a29(); [PreserveSig] int _a30();
        // IMFSample 固有 (slot 33-)
        [PreserveSig] int GetSampleFlags(out int flags);                          // 33
        [PreserveSig] int SetSampleFlags(int flags);                              // 34
        [PreserveSig] int GetSampleTime(out long time);                           // 35
        [PreserveSig] int SetSampleTime(long time);                               // 36 使用
        [PreserveSig] int GetSampleDuration(out long duration);                   // 37
        [PreserveSig] int SetSampleDuration(long duration);                       // 38 使用
        [PreserveSig] int GetBufferCount(out int count);                          // 39
        [PreserveSig] int GetBufferByIndex(int index, out IMFMediaBuffer buffer); // 40
        [PreserveSig] int ConvertToContiguousBuffer(out IMFMediaBuffer buffer);   // 41
        [PreserveSig] int AddBuffer(IMFMediaBuffer buffer);                       // 42 使用
    }

    [ComImport, Guid("045fa593-8799-42b8-bc8d-8968c6453507"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMFMediaBuffer
    {
        [PreserveSig] int Lock(out IntPtr ppbBuffer, out int pcbMaxLength, out int pcbCurrentLength);
        [PreserveSig] int Unlock();
        [PreserveSig] int GetCurrentLength(out int pcbCurrentLength);
        [PreserveSig] int SetCurrentLength(int cbCurrentLength);
        [PreserveSig] int GetMaxLength(out int pcbMaxLength);
    }

    [ComImport, Guid("3137f1cd-fe5e-4805-a5d8-fb477448cb3d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMFSinkWriter
    {
        [PreserveSig] int AddStream(IMFMediaType pTargetMediaType, out int pdwStreamIndex);
        [PreserveSig] int SetInputMediaType(int dwStreamIndex, IMFMediaType pInputMediaType, IntPtr pEncodingParameters);
        [PreserveSig] int BeginWriting();
        [PreserveSig] int WriteSample(int dwStreamIndex, IMFSample pSample);
        [PreserveSig] int SendStreamTick(int dwStreamIndex, long llTimestamp);
        [PreserveSig] int PlaceMarker(int dwStreamIndex, IntPtr pvContext);
        [PreserveSig] int NotifyEndOfSegment(int dwStreamIndex);
        [PreserveSig] int Flush(int dwStreamIndex);
        [PreserveSig] int Finalize_();
        [PreserveSig] int GetServiceForStream(int dwStreamIndex, [MarshalAs(UnmanagedType.LPStruct)] Guid guidService, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject); // 12 使用
        // 以降(GetStatistics)は未使用。
    }

    [ComImport, Guid("901db4c7-31ce-41a2-85dc-8fa0bf41b8da"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface ICodecAPI // SetValue のみ使用 (slot 3-8 はプレースホルダ)
    {
        [PreserveSig] int IsSupported();        // 3
        [PreserveSig] int IsModifiable();       // 4
        [PreserveSig] int GetParameterRange();  // 5
        [PreserveSig] int GetParameterValues(); // 6
        [PreserveSig] int GetDefaultValue();    // 7
        [PreserveSig] int GetValue();           // 8
        [PreserveSig] int SetValue([MarshalAs(UnmanagedType.LPStruct)] Guid Api, ref PROPVARIANT Value); // 9 使用
    }

    [StructLayout(LayoutKind.Explicit, Size = 24)] // x64 VARIANT 相当。VT_UI4 のみ使用
    private struct PROPVARIANT
    {
        [FieldOffset(0)] public ushort vt;
        [FieldOffset(8)] public uint ulVal;
    }
    #endregion
}

#nullable enable
using System;
using System.Runtime.InteropServices;

namespace Crystallography;

/// <summary>
/// xraylib (BSD, 4.2.1) のネイティブ DLL <c>libxrl-11.dll</c> への薄い P/Invoke ラッパ。260606Cl 追加。
/// </summary>
/// <remarks>
/// 当面は異常分散因子 f′ (Fi) / f″ (−Fii) のみ公開する (BeamInteraction 改修計画 §9.3 / P4)。今後 µ/ρ・屈折率・蛍光線などを
/// 追加する際は同じ規約 (E は keV・electron 単位・末尾 <c>xrl_error**</c> は NULL) でここに足す。
///
/// 設計方針 (既存 <see cref="NativeWrapper"/> と同じ graceful fallback):
/// - 起動時に Fe@Cu Kα の self-test を行い <see cref="Enabled"/> を確定する。DLL 不在・別物・ABI 不一致でも例外を投げず
///   <see cref="Enabled"/>=false / <see cref="LastLoadError"/> に記録する。呼び出し側は値が <c>NaN</c> なら "N/A" 表示にできる。
/// - <see cref="NativeWrapper"/> が同一アセンブリ (Crystallography.dll) で既に <c>SetDllImportResolver</c> を呼んでいるため、
///   ここでは <b>リゾルバを登録しない</b> (二重登録は InvalidOperationException)。<c>libxrl-11.dll</c> はアプリ基準ディレクトリの
///   既定探索で解決される (NativeWrapper のリゾルバは未知 DLL に IntPtr.Zero を返し、既定解決にフォールバックする)。
///
/// 単位・符号 (xraylib 4.2.1 検証済):
/// - <c>Fi(Z,E)</c> / <c>Fii(Z,E)</c> の E は <b>keV</b>、戻り値は electron 単位。
/// - xraylib の生 <c>Fii</c> は結晶学慣用の f″ と <b>符号が逆</b> (公式実装でも f″ = −Fii)。本ラッパは符号反転して返す。
/// - 検証: Fi(26, 8.04778)=−1.106 / −Fii(26, 8.04778)=+3.192 ≈ International Tables の Fe@Cu Kα (f′=−1.13, f″=3.20)。
/// </remarks>
public static class Xraylib
{
    private const string Dll = "libxrl-11.dll";

    // xraylib 4.x の C ABI: double Fi(int Z, double E, xrl_error **error); error は NULL(IntPtr.Zero) 可。
    // x64 Windows は呼び出し規約が単一だが可読性のため Cdecl を明示。ExactSpelling で A/W 名探索を抑止。
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double Fi(int Z, double E, IntPtr error);

    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double Fii(int Z, double E, IntPtr error);

    // 弾性(Rayleigh, コヒーレント)原子形状因子 F(q) と 非弾性(Compton)散乱関数 S(q)。
    // q = sin(θ/2)/λ [Å⁻¹]。これは結晶学の s = sinθ_B/λ と同一量 (散乱角 θ=2θ_B なので sin(θ/2)=sinθ_B)。260606Cl
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double FF_Rayl(int Z, double q, IntPtr error);

    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double SF_Compt(int Z, double q, IntPtr error);

    /// <summary>xraylib ネイティブ DLL が読み込めて self-test に通ったか。260606Cl 追加。</summary>
    public static bool Enabled { get; }

    /// <summary>DLL ロード/self-test に失敗した場合の理由 (成功時は null)。260606Cl 追加。</summary>
    public static string? LastLoadError { get; private set; }

    static Xraylib()
    {
        try
        {
            // Fe(Z=26) @ Cu Kα (8.04778 keV): f′≈−1.11, 慣用 f″≈+3.19。物理的に妥当な範囲なら本物の libxrl と判断。
            double fp = Fi(26, 8.04778, IntPtr.Zero);
            double fpp = -Fii(26, 8.04778, IntPtr.Zero);
            Enabled = double.IsFinite(fp) && double.IsFinite(fpp)
                      && fp is > -2.0 and < 0.0 && fpp is > 2.0 and < 4.5;
            if (!Enabled)
                LastLoadError = $"xraylib self-test out of expected range: Fe f'={fp:g4}, f''={fpp:g4}.";
        }
        catch (Exception ex) // DllNotFoundException / EntryPointNotFoundException / BadImageFormatException 等
        {
            Enabled = false;
            LastLoadError = ex.Message;
        }
    }

    /// <summary>異常分散の実部 f′(E) [electron 単位]。<paramref name="energyKeV"/> は keV。利用不可・範囲外は <c>NaN</c>。260606Cl 追加。</summary>
    public static double Fprime(int z, double energyKeV)
    {
        if (!Enabled || z < 1 || z > 99 || !(energyKeV > 0)) return double.NaN;
        double v = Fi(z, energyKeV, IntPtr.Zero);
        return double.IsFinite(v) ? v : double.NaN;
    }

    /// <summary>異常分散の虚部 f″(E) [electron 単位・結晶学慣用符号 (&gt;0)]。<paramref name="energyKeV"/> は keV。利用不可・範囲外は <c>NaN</c>。
    /// xraylib の生 Fii は符号が逆なので反転して返す。260606Cl 追加。</summary>
    public static double Fdoubleprime(int z, double energyKeV)
    {
        if (!Enabled || z < 1 || z > 99 || !(energyKeV > 0)) return double.NaN;
        double v = -Fii(z, energyKeV, IntPtr.Zero);
        return double.IsFinite(v) ? v : double.NaN;
    }

    /// <summary>弾性(Rayleigh, コヒーレント)原子形状因子 F(q) [electron 単位]。<paramref name="qInvAng"/> は q=sinθ/λ [Å⁻¹]。
    /// q→0 で Z、高 q で 0 に漸近。利用不可・範囲外は <c>NaN</c>。260606Cl 追加。</summary>
    public static double FormFactorRayl(int z, double qInvAng)
    {
        if (!Enabled || z < 1 || z > 99 || !(qInvAng >= 0)) return double.NaN;
        double v = FF_Rayl(z, qInvAng, IntPtr.Zero);
        return double.IsFinite(v) ? v : double.NaN;
    }

    /// <summary>非弾性(Compton)散乱関数 S(q) [electron 単位]。<paramref name="qInvAng"/> は q=sinθ/λ [Å⁻¹]。
    /// q→0 で 0、高 q で Z に漸近。利用不可・範囲外は <c>NaN</c>。260606Cl 追加。</summary>
    public static double IncoherentSF(int z, double qInvAng)
    {
        if (!Enabled || z < 1 || z > 99 || !(qInvAng >= 0)) return double.NaN;
        double v = SF_Compt(z, qInvAng, IntPtr.Zero);
        return double.IsFinite(v) ? v : double.NaN;
    }
}

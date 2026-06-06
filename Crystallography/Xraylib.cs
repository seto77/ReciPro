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

    // 260606Cl 追加 (P4 残: Attenuation & transport / Fluorescence タブ用)。すべて電子単位ではなく物理単位 (cm²/g, keV, 無次元)。
    // 質量減衰断面積 [cm²/g] (元素別, E は keV)。_CP (化合物文字列版) は当面使わず per-Z を ElementNum で質量加重する。
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double CS_Total(int Z, double E, IntPtr error);   // 全減衰 µ/ρ
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double CS_Photo(int Z, double E, IntPtr error);   // 光電吸収
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double CS_Rayl(int Z, double E, IntPtr error);    // Rayleigh (弾性)
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double CS_Compt(int Z, double E, IntPtr error);   // Compton (非弾性)
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double CS_Energy(int Z, double E, IntPtr error);  // 質量エネルギー吸収 µ_en/ρ (≠ µ/ρ)

    // 吸収端・特性線・蛍光。shell / line は整数マクロ (xraylib.h)。⚠発光線マクロは負値・殻マクロは 0 始まり (XrlLine/XrlShell enum で型安全化)。
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double EdgeEnergy(int Z, int shell, IntPtr error); // 吸収端エネルギー [keV]
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double JumpFactor(int Z, int shell, IntPtr error); // 吸収端ジャンプ比 (無次元)
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double LineEnergy(int Z, int line, IntPtr error);  // 特性線エネルギー [keV]
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double RadRate(int Z, int line, IntPtr error);     // 殻内 radiative rate (分率)
    [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern double FluorYield(int Z, int shell, IntPtr error); // 蛍光収率 ω (≤1)

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

    // ===== 260606Cl 追加: 質量減衰係数・吸収端・特性線 (P4 残: Attenuation & transport / Fluorescence) =====

    /// <summary>xraylib の特性線 (Siegbahn) 整数マクロ。⚠発光線は負値 (xraylib.h, NOT xraylib-lines.h)。260606Cl 追加。</summary>
    public enum XrlLine { Ka1 = -3, Ka2 = -2, Kb1 = -6, La1 = -90, La2 = -89, Lb1 = -63 }

    /// <summary>xraylib の殻 (shell) 整数マクロ。0 始まり (K=0)。260606Cl 追加。</summary>
    public enum XrlShell { K = 0, L1 = 1, L2 = 2, L3 = 3, M1 = 4, M2 = 5, M3 = 6, M4 = 7, M5 = 8 }

    // 範囲外で xraylib は error でなく 0.0 を返す → 物理的に正の量は ≤0/非有限を NaN("N/A") に畳む共通ガード。
    private static double Pos(double v) => v > 0 && double.IsFinite(v) ? v : double.NaN;
    private static bool BadZe(int z, double energyKeV) => !Enabled || z < 1 || z > 99 || !(energyKeV > 0);
    private static bool BadZ(int z) => !Enabled || z < 1 || z > 99;

    /// <summary>質量全減衰係数 µ/ρ [cm²/g] (元素別, E は keV)。利用不可・範囲外は NaN。260606Cl 追加。</summary>
    public static double MassAttenuationTotal(int z, double energyKeV) => BadZe(z, energyKeV) ? double.NaN : Pos(CS_Total(z, energyKeV, IntPtr.Zero));
    /// <summary>光電吸収の質量減衰 [cm²/g]。260606Cl 追加。</summary>
    public static double MassAttenuationPhoto(int z, double energyKeV) => BadZe(z, energyKeV) ? double.NaN : Pos(CS_Photo(z, energyKeV, IntPtr.Zero));
    /// <summary>Rayleigh (弾性) 散乱の質量減衰 [cm²/g]。260606Cl 追加。</summary>
    public static double MassAttenuationRayleigh(int z, double energyKeV) => BadZe(z, energyKeV) ? double.NaN : Pos(CS_Rayl(z, energyKeV, IntPtr.Zero));
    /// <summary>Compton (非弾性) 散乱の質量減衰 [cm²/g]。260606Cl 追加。</summary>
    public static double MassAttenuationCompton(int z, double energyKeV) => BadZe(z, energyKeV) ? double.NaN : Pos(CS_Compt(z, energyKeV, IntPtr.Zero));
    /// <summary>質量エネルギー吸収係数 µ_en/ρ [cm²/g] (≠ µ/ρ)。260606Cl 追加。</summary>
    public static double MassEnergyAbsorption(int z, double energyKeV) => BadZe(z, energyKeV) ? double.NaN : Pos(CS_Energy(z, energyKeV, IntPtr.Zero));

    /// <summary>吸収端エネルギー [keV]。利用不可は NaN。260606Cl 追加。</summary>
    public static double EdgeEnergyKeV(int z, XrlShell shell) => BadZ(z) ? double.NaN : Pos(EdgeEnergy(z, (int)shell, IntPtr.Zero));
    /// <summary>吸収端ジャンプ比 (無次元, >1)。260606Cl 追加。</summary>
    public static double EdgeJumpFactor(int z, XrlShell shell) => BadZ(z) ? double.NaN : Pos(JumpFactor(z, (int)shell, IntPtr.Zero));
    /// <summary>特性線エネルギー [keV]。260606Cl 追加。</summary>
    public static double LineEnergyKeV(int z, XrlLine line) => BadZ(z) ? double.NaN : Pos(LineEnergy(z, (int)line, IntPtr.Zero));
    /// <summary>殻内 radiative rate (分率, 0〜1)。260606Cl 追加。</summary>
    public static double LineRadRate(int z, XrlLine line) => BadZ(z) ? double.NaN : Pos(RadRate(z, (int)line, IntPtr.Zero));
    /// <summary>蛍光収率 ω (≤1)。260606Cl 追加。</summary>
    public static double FluorescenceYield(int z, XrlShell shell) => BadZ(z) ? double.NaN : Pos(FluorYield(z, (int)shell, IntPtr.Zero));
}

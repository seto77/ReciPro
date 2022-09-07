using System;
using System.Numerics;

namespace Crystallography;

public static class MathematicalConstants
{
    public static Complex TwoPiI = new(0, 2 * Math.PI);

    public static readonly double[] Factorial = new double[]
    {
        #region 
        1,
            1,
            2,
            6,
            24,
            120,
            720,
            5040,
            40320,
            362880,
            3628800,
            39916800,
            479001600,
            6227020800,
            87178291200,
            1307674368000,
            20922789888000,
            355687428096000,
            6.402373705728E+15,
            1.21645100408832E+17,
            2.43290200817664E+18,
            5.109094217170944E+19,
            1.1240007277776077E+21,
            2.5852016738884978E+22,
            6.2044840173323941E+23,
            1.5511210043330986E+25,
            4.0329146112660565E+26,
            1.0888869450418352E+28,
            3.0488834461171384E+29,
            8.8417619937397008E+30,
            2.6525285981219103E+32,
            8.2228386541779224E+33,
            2.6313083693369352E+35,
            8.6833176188118859E+36,
            2.9523279903960412E+38,
            1.0333147966386144E+40,
            3.7199332678990118E+41,
            1.3763753091226343E+43,
            5.23022617466601E+44,
            2.0397882081197442E+46,
            8.1591528324789768E+47,
            3.3452526613163803E+49,
            1.4050061177528798E+51,
            6.0415263063373834E+52,
            2.6582715747884485E+54,
            1.1962222086548019E+56,
            5.5026221598120885E+57,
            2.5862324151116818E+59,
            1.2413915592536073E+61,
            6.0828186403426752E+62,
            3.0414093201713376E+64,
            1.5511187532873822E+66,
            8.0658175170943877E+67,
            4.2748832840600255E+69,
            2.3084369733924138E+71,
            1.2696403353658276E+73,
            7.1099858780486348E+74,
            4.0526919504877221E+76,
            2.3505613312828789E+78,
            1.3868311854568986E+80,
            8.3209871127413916E+81,
            5.0758021387722484E+83,
            3.1469973260387939E+85,
            1.98260831540444E+87
        #endregion
    };

}



public static class UniversalConstants
{
    public enum LengthUnit
    {
        km, m, cm, mm, um, nm, pm, fm
    }

    /// <summary>
    /// アボガドロ数 (g)
    /// </summary>
    public const double A = 6.0221367E23;

    /// <summary>
    /// 電子の静止質量 (kg)
    /// </summary>
    public const double m0 = 9.1093897E-31;

    /// <summary>
    /// 電子の電荷(C)
    /// </summary>
    public const double e0 = 1.60217733E-19;

    /// <summary>
    /// プランク定数 (J s = m^2 kg /s) (2006 科学技術データ委員会)
    /// </summary>
    public const double h = 6.62606896E-34;

    /// <summary>
    /// プランク定数 (eV/s)(2006 科学技術データ委員会)
    /// </summary>
    public const double h_eV = 4.135667516E-15; //

    /// <summary>
    /// 1eVのジュール(kg m^2/s^2)
    /// </summary>
    public const double eV_joule = 1.602176565E-19;

    /// <summary>
    /// 光速 (m/s)
    /// </summary>
    public const double c = 2.99792458E+8; // 定義値

    /// <summary>
    /// 光速の2乗 (m^2/s^2)
    /// </summary>
    public const double c2 = 8.987551787368176E+16;

    /// <summary>
    /// 中性子の質量 (kg)
    /// </summary>
    public const double n0 = 1.674927351E-27;

    /// <summary>
    /// 気体定数
    /// </summary>
    public const double R = 8.3144621;

    /// <summary>
    /// ボーア半径 (m)
    /// </summary>
    public const double a0 = 0.52917721067E-10;

    /// <summary>
    /// Rydberg 定数 (eV)
    /// </summary>
    public const double Ry = 13.60569253;

    public static class Convert
    {
        /// <summary>
        /// 中性子の速度(m/μs)を与えて波長(nm)を返す
        /// </summary>
        /// <param name="velocity"></param>
        /// <returns></returns>
        public static double NeutronVelocityToWavelength(in double velocity)
            => h / n0 / velocity * 1.0E3;

        /// <summary>
        /// 中性子の波長(nm)を与えて速度(m/μs)を返す
        /// </summary>
        /// <param name="velocity"></param>
        /// <returns></returns>
        public static double WavelengthToNeutronVelocity(in double wavelength)
            => h / n0 / wavelength * 1.0E3;

        /// <summary>
        /// 中性子の速度を与えてエネルギー(eV)を返す
        /// </summary>
        /// <param name="velocity"></param>
        /// <returns></returns>
        public static double NeutronVelocityToNeutronEnergy(in double velocity)
            => n0 * velocity * velocity / 2.0 / eV_joule;

        /// <summary>
        /// 波長(nm)を電子線のエネルギー(kV)に変換
        /// </summary>
        /// <param name="kiloVoltage"></param>
        /// <returns></returns>
        public static double WaveLengthToElectronEnergy(in double waveLength)
        {
            //U =voltage
            //WaveLength = h / Math.Sqrt ( 2 * m0 * e0 * U * ( 1 + e0 * U / 2 / m0 / c^2 ) )
            double b = 1000;
            double a = 0.9784753725226711491437618236159;
            double c = -1.2264262862108010441350327657997 / waveLength;
            return (-b + Math.Sqrt(b * b + 4 * a * c * c)) / 2 / a;
        }

        /// <summary>
        /// 波長(nm)を電磁波のエネルギー(eV)に変換
        /// </summary>
        /// <param name="waveLength"></param>
        /// <returns></returns>
        public static double WavelengthToXrayEnergy(in double waveLength)
            => 6.62606896 / 1.60217733 * 2.99792458 / waveLength * 100.0;

        /// <summary>
        /// 波長(nm)を中性子のエネルギー(eV)に変換
        /// </summary>
        /// <param name="velocity"></param>
        /// <returns></returns>
        public static double WaveLengthToNeutronEnergy(in double wavelength)
            => 6.62606896 * 6.62606896 / 1.674927351 / wavelength / wavelength / 2.0 / 1.602176565 * 1.0E5;

        /// <summary>
        /// エネルギーを電磁波の波長(nm)に変換
        /// </summary>
        /// <param name="energy"></param>
        /// <returns></returns>
        public static double EnergyToXrayWaveLength(in double energy)
            => 6.62606896 / 1.60217733 * 2.99792458 / energy * 100.0;

        /// <summary>
        /// エネルギー(kV)を電子線の波長(nm)に変換
        /// </summary>
        /// <param name="kiloVoltage"></param>
        /// <returns></returns>
        public static double EnergyToElectronWaveLength(in double kiloVoltage)
        {
            //U =voltage
            //WaveLength = h / Math.Sqrt ( 2 * m0 * e0 * U * ( 1 + e0 * U / 2 / m0 / c^2 ) )
            return 1.2264262862108010441350327657997 / Math.Sqrt(kiloVoltage * 1000.0 * (1 + kiloVoltage * 0.9784753725226711491437618236159 / 1000));
        }

        /// <summary>
        /// エネルギー(kV)を電子線の波数(nm^-1)に変換 (2πで割った数値では無い)
        /// </summary>
        /// <param name="kiloVoltage"></param>
        /// <returns></returns>
        public static double EnergyToElectronWaveNumber(in double kiloVoltage)
        {
            //U =voltage
            //WaveLength = h / Math.Sqrt ( 2 * m0 * e0 * U * ( 1 + e0 * U / 2 / m0 / c^2 ) )
            return Math.Sqrt(kiloVoltage * 1000.0 * (1 + kiloVoltage * 0.9784753725226711491437618236159 / 1000)) / 1.2264262862108010441350327657997;
        }

        /// <summary>
        /// 電子線の波数(nm^-1)をエネルギー(kV)に変換 
        /// </summary>
        /// <param name="kiloVoltage"></param>
        /// <returns></returns>
        public static double ElectronWaveNumberToEnergy(in double wavenumber) 
            => WaveLengthToElectronEnergy(1 / wavenumber);


        /// <summary>
        /// エネルギー(kV)を中性子の波長(nm)に変換
        /// </summary>
        /// <param name="energy"></param>
        /// <returns></returns>
        public static double EnergyToNeutronWaveLength(in double energy)
            => 6.62606896 * Math.Sqrt(1 / 1.674927351 / energy / 2.0 / 1.602176565 * 1.0E5);

        /// <summary>
        /// 面間隔d(nm)と取り出し角(2Θ)を与えるとブラッグ条件を満たす入射線の波長(nm)を返す
        /// </summary>
        /// <param name="d"></param>
        /// <param name="takeoffAngle"></param>
        /// <returns></returns>
        public static double DspacingToWaveLength(in double d, in double takeoffAngle) 
            => 2.0 * d * Math.Sin(takeoffAngle / 2.0);

        public static double DspacingToXrayEnergy(in double d, in double takeoffAngle) 
            => WavelengthToXrayEnergy(DspacingToWaveLength(d, takeoffAngle));
    }
}

[Serializable()]
public enum XrayLine
{
    Ka, Ka1, Ka2, Kb1, Kb3, KbI2, KbII2, La1, La2, Lb1, Lb2
}

[Serializable()]
public enum XrayLineEdge
{
    K, L1, L2, L3, M1, M2, M3, M4, M5, N1, N2, N3, N4, N5, N6, N7, O1, O2, O3, O4, O5, O6, P1, P2, P3
}
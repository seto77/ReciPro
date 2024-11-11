using System;

namespace Crystallography;

[Serializable]
public class WaveProperty
{
    /// <summary>
    /// 波の種類 (X線 or 電子線 or 中性子)
    /// </summary>
    public WaveSource Source;

    /// <summary>
    /// 波長 (nm)
    /// </summary>
    public double WaveLength;

    /// <summary>
    /// 特性X線の場合は原子番号、特性X線でないX線は0, X線でない場合は-1
    /// </summary>
    public int XrayElementNumber;

    /// <summary>
    /// 特性X線の場合の線種
    /// </summary>
    public XrayLine XrayLine;

    /// <summary>
    /// エネルギー　(eV)
    /// </summary>
    public double Energy;

    /// <summary>
    /// 単色性
    /// </summary>
    public double Monochromaticity = 0;

    /// <summary>
    /// 発散角 (radian)
    /// </summary>
    public double Convergence = 0;

    public WaveProperty()
    {

    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="source"></param>
    /// <param name="xrayWaveLength"></param>
    /// <param name="xrayElementNumber"></param>
    /// <param name="xrayLine"></param>
    /// <param name="electronAccVoltage"></param>

    public WaveProperty(WaveSource source, double xrayWaveLength, int xrayElementNumber, XrayLine xrayLine, double electronAccVoltage)
    {
        Source = source;
        WaveLength = xrayWaveLength;
        XrayElementNumber = xrayElementNumber;
        XrayLine = xrayLine;
        Energy = electronAccVoltage;
    }

    public WaveProperty(int xrayElementNumber, XrayLine xrayLine)
    {
        Source = WaveSource.Xray;
        WaveLength = AtomStatic.CharacteristicXrayWavelength(xrayElementNumber, xrayLine);
        Energy = AtomStatic.CharacteristicXrayWavelength(xrayElementNumber, xrayLine);
    }

    public WaveProperty(WaveSource source, double value, bool isEnergy = true)
    {
        Source = source;
        XrayElementNumber = -1;
        if (isEnergy)
        {
            Energy = value;
            if (source == WaveSource.Xray)
            {
                WaveLength = UniversalConstants.Convert.EnergyToXrayWaveLength(Energy);
                XrayElementNumber = 0;
            }
            else if (source == WaveSource.Electron)
                WaveLength = UniversalConstants.Convert.EnergyToElectronWaveLength(Energy / 1000);
            else if (source == WaveSource.Neutron)
                WaveLength = UniversalConstants.Convert.EnergyToNeutronWaveLength(Energy);
        }
        else
        {
            WaveLength = value;
            if (source == WaveSource.Xray)
            {
                Energy = UniversalConstants.Convert.WavelengthToXrayEnergy(WaveLength);
                XrayElementNumber = 0;
            }
            else if (source == WaveSource.Electron)
                Energy = UniversalConstants.Convert.WaveLengthToElectronEnergy(WaveLength) * 1000;
            else if (source == WaveSource.Neutron)
                Energy = UniversalConstants.Convert.WaveLengthToNeutronEnergy(WaveLength);
        }
    }

    public WaveProperty(WaveSource source, double energy, double monochromaticity, double convergence, bool isEnergy = true)
        : this(source, energy, isEnergy)
    {
        Monochromaticity = monochromaticity;
        Convergence = convergence;
    }
}
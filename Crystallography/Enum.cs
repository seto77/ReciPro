namespace Crystallography;

//汎用性の高い列挙体をここで定義

public enum LengthUnitEnum
{
    None,

    Meter,
    CentiMeter,
    MilliMeter,
    MicroMeter,
    NanoMeter,
    Angstrom,
    PicoMeter,

    MeterInverse,
    CentiMeterInverse,
    MilliMeterInverse,
    MicroMeterInverse,
    NanoMeterInverse,
    AngstromInverse,
    PicoMeterInverse
}

public enum AngleUnitEnum
{
    Degree,
    Radian
}

public enum FourierDirectionEnum
{
    Forward,
    Inverse
}

public enum VH_DirectionEnum
{
    Vertical,
    Horizontal
}

public enum EnergyUnitEnum
{
    eV,
    KeV,
    MeV
}

public enum TimeUnitEnum
{
    Seccond,
    MilliSecond,
    MicroSecond,
    NanoSecond
}

internal class Enum
{
}
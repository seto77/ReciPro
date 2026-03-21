using System;
using System.Linq;

namespace Crystallography;

/// <summary>
/// Rosca-Lambert の等積正方形格子上で保持する EBSD MasterKernel。
/// </summary>
public sealed class EbsdMasterKernel
{
    public static readonly double SquareLimit = Math.Sqrt(Math.PI / 2.0); // (260321Ch) 半球ごとの等積正方形の半幅

    public int GridSize { get; }
    public double[] Energies { get; }
    public double[] Depths { get; }
    public EbsdMasterKernelHemisphere Hemisphere { get; }
    public float[][] Planes { get; }

    public int PlaneCount => Energies.Length * Depths.Length;

    public EbsdMasterKernel(int gridSize, double[] energies, double[] depths, EbsdMasterKernelHemisphere hemisphere, float[][] planes)
    {
        GridSize = gridSize;
        Energies = energies ?? [];
        Depths = depths ?? [];
        Hemisphere = hemisphere;
        Planes = planes ?? [];
    }

    public float[] GetPlane(int energyIndex, int depthIndex)
    {
        if ((uint)energyIndex >= (uint)Energies.Length || (uint)depthIndex >= (uint)Depths.Length)
            return null;

        var planeIndex = energyIndex * Depths.Length + depthIndex;
        return (uint)planeIndex < (uint)Planes.Length ? Planes[planeIndex] : null;
    }

    public static EbsdMasterKernel FromDisks(BetheMethod.CBED_Disk[][] disks, double[] energies, double[] depths, int gridSize, EbsdMasterKernelHemisphere hemisphere)
    {
        if (disks == null || energies == null || depths == null)
            return null;

        var planes = new float[energies.Length * depths.Length][];
        for (int eIndex = 0; eIndex < energies.Length; eIndex++)
            for (int dIndex = 0; dIndex < depths.Length; dIndex++)
            {
                var disk = disks[eIndex][dIndex];
                planes[eIndex * depths.Length + dIndex] = [.. disk.Amplitudes.Select(amp => (float)(amp.Real * amp.Real + amp.Imaginary * amp.Imaginary))];
            }

        return new EbsdMasterKernel(gridSize, [.. energies], [.. depths], hemisphere, planes);
    }

    public static Vector3DBase[] CreateDirections(int gridSize, EbsdMasterKernelHemisphere hemisphere)
    {
        var directions = new Vector3DBase[gridSize * gridSize];
        var step = 2.0 * SquareLimit / gridSize;
        for (int h = 0; h < gridSize; h++)
            for (int w = 0; w < gridSize; w++)
            {
                var a = -SquareLimit + (w + 0.5) * step;
                var b = SquareLimit - (h + 0.5) * step; // (260321Ch) preview の上方向が +Y に見えるよう Y を反転
                directions[h * gridSize + w] = RoscaLambertToSphere(a, b, hemisphere);
            }
        return directions;
    }

    public static Vector3DBase RoscaLambertToSphere(double a, double b, EbsdMasterKernelHemisphere hemisphere)
    {
        if (Math.Abs(a) < 1.0E-15 && Math.Abs(b) < 1.0E-15)
            return hemisphere == EbsdMasterKernelHemisphere.PositiveZ
                ? new Vector3DBase(0, 0, 1)
                : new Vector3DBase(0, 0, -1);

        double A, B;
        if (Math.Abs(b) <= Math.Abs(a))
        {
            // (260321Ch) Rosca の square -> disk 等積写像。|b|<=|a| の枝。
            var θ = b * Math.PI / (4.0 * a);
            A = 2.0 * a / Math.Sqrt(Math.PI) * Math.Cos(θ);
            B = 2.0 * a / Math.Sqrt(Math.PI) * Math.Sin(θ);
        }
        else
        {
            // (260321Ch) |a|<=|b| の枝。
            var θ = a * Math.PI / (4.0 * b);
            A = 2.0 * b / Math.Sqrt(Math.PI) * Math.Sin(θ);
            B = 2.0 * b / Math.Sqrt(Math.PI) * Math.Cos(θ);
        }

        var rho2 = A * A + B * B;
        var radialScale = Math.Sqrt(Math.Max(0.0, 1.0 - rho2 / 4.0));
        var z = hemisphere == EbsdMasterKernelHemisphere.PositiveZ
            ? 1.0 - rho2 / 2.0
            : -1.0 + rho2 / 2.0;
        return new Vector3DBase(radialScale * A, radialScale * B, z);
    }
}

public enum EbsdMasterKernelHemisphere
{
    NegativeZ = -1,
    PositiveZ = 1,
}

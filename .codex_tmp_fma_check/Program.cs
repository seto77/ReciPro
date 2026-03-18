using System;
using System.Runtime.Intrinsics.X86;
Console.WriteLine($"FMA={Fma.IsSupported}");
Console.WriteLine($"AVX={Avx.IsSupported}");

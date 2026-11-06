```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
Memory: 63.69 GB Total, 29.13 GB Available
.NET SDK 11.0.100-rc.3.26522.148
  [Host]     : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4
  Job-MGUEMN : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4

EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0,DOTNET_TC_OnStackReplacement=1,COMPlus_TC_OnStackReplacement=1,DOTNET_TieredCompilation=1,DOTNET_TieredPGO=1  Server=True  Arguments=/p:Platform=x64  
IterationCount=8  IterationTime=250ms  LaunchCount=1  
WarmupCount=3  

```
| Method            | Beams | Uniform | WithTds | Mean        | Error       | StdDev      | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------ |------ |-------- |-------- |------------:|------------:|------------:|------:|--------:|----------:|------------:|
| **Current**           | **64**    | **False**   | **False**   |  **1,969.6 μs** |   **126.67 μs** |    **56.24 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| MinimalFactorized | 64    | False   | False   |    311.1 μs |    50.64 μs |    26.49 μs |  0.16 |    0.01 |         - |          NA |
|                   |       |         |         |             |             |             |       |         |           |             |
| **Current**           | **64**    | **False**   | **True**    |  **2,131.9 μs** |   **191.96 μs** |   **100.40 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| MinimalFactorized | 64    | False   | True    |    423.6 μs |    47.72 μs |    24.96 μs |  0.20 |    0.01 |         - |          NA |
|                   |       |         |         |             |             |             |       |         |           |             |
| **Current**           | **64**    | **True**    | **False**   |    **279.6 μs** |    **37.98 μs** |    **19.86 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| MinimalFactorized | 64    | True    | False   |    286.1 μs |    50.19 μs |    26.25 μs |  1.03 |    0.11 |         - |          NA |
|                   |       |         |         |             |             |             |       |         |           |             |
| **Current**           | **64**    | **True**    | **True**    |    **419.9 μs** |    **58.27 μs** |    **30.48 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| MinimalFactorized | 64    | True    | True    |    468.1 μs |    63.93 μs |    33.43 μs |  1.12 |    0.11 |         - |          NA |
|                   |       |         |         |             |             |             |       |         |           |             |
| **Current**           | **160**   | **False**   | **False**   | **12,653.8 μs** | **1,911.33 μs** |   **848.64 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| MinimalFactorized | 160   | False   | False   |  1,718.4 μs |   139.63 μs |    73.03 μs |  0.14 |    0.01 |         - |          NA |
|                   |       |         |         |             |             |             |       |         |           |             |
| **Current**           | **160**   | **False**   | **True**    | **14,902.1 μs** | **3,778.03 μs** | **1,975.99 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| MinimalFactorized | 160   | False   | True    |  3,498.3 μs |   327.93 μs |   171.51 μs |  0.24 |    0.03 |         - |          NA |
|                   |       |         |         |             |             |             |       |         |           |             |
| **Current**           | **160**   | **True**    | **False**   |  **1,922.8 μs** |   **216.47 μs** |   **113.22 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| MinimalFactorized | 160   | True    | False   |  1,816.1 μs |   152.73 μs |    67.81 μs |  0.95 |    0.06 |         - |          NA |
|                   |       |         |         |             |             |             |       |         |           |             |
| **Current**           | **160**   | **True**    | **True**    |  **3,803.6 μs** |   **438.56 μs** |   **229.37 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| MinimalFactorized | 160   | True    | True    |  3,806.3 μs |   345.85 μs |   180.88 μs |  1.00 |    0.07 |         - |          NA |

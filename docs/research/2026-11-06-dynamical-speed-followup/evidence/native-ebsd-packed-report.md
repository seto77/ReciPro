```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
Memory: 63.69 GB Total, 29.86 GB Available
.NET SDK 11.0.100-rc.3.26522.148
  [Host]     : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4
  Job-MGUEMN : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4

EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0,DOTNET_TC_OnStackReplacement=1,COMPlus_TC_OnStackReplacement=1,DOTNET_TieredCompilation=1,DOTNET_TieredPGO=1  Server=True  Arguments=/p:Platform=x64  
IterationCount=8  IterationTime=250ms  LaunchCount=1  
WarmupCount=3  

```
| Method           | Beams | Uniform | WithTds | Mean        | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------- |------ |-------- |-------- |------------:|----------:|----------:|------:|--------:|----------:|------------:|
| **Current**          | **64**    | **False**   | **False**   |  **1,338.9 μs** |  **26.01 μs** |   **9.27 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| PackedReciprocal | 64    | False   | False   |  1,494.5 μs |  90.45 μs |  47.31 μs |  1.12 |    0.03 |         - |          NA |
| PackedFactorized | 64    | False   | False   |    352.0 μs |  75.63 μs |  39.56 μs |  0.26 |    0.03 |         - |          NA |
|                  |       |         |         |             |           |           |       |         |           |             |
| **Current**          | **64**    | **False**   | **True**    |  **1,470.5 μs** | **101.41 μs** |  **45.03 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| PackedReciprocal | 64    | False   | True    |  1,679.8 μs |  86.76 μs |  38.52 μs |  1.14 |    0.04 |         - |          NA |
| PackedFactorized | 64    | False   | True    |    460.1 μs |   4.74 μs |   1.69 μs |  0.31 |    0.01 |         - |          NA |
|                  |       |         |         |             |           |           |       |         |           |             |
| **Current**          | **64**    | **True**    | **False**   |    **215.5 μs** |  **16.42 μs** |   **8.59 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| PackedReciprocal | 64    | True    | False   |    350.9 μs |  27.38 μs |  12.16 μs |  1.63 |    0.08 |         - |          NA |
| PackedFactorized | 64    | True    | False   |    336.9 μs |  11.69 μs |   5.19 μs |  1.57 |    0.06 |         - |          NA |
|                  |       |         |         |             |           |           |       |         |           |             |
| **Current**          | **64**    | **True**    | **True**    |    **322.7 μs** |  **59.64 μs** |  **31.19 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| PackedReciprocal | 64    | True    | True    |    492.0 μs |   6.82 μs |   3.57 μs |  1.54 |    0.13 |         - |          NA |
| PackedFactorized | 64    | True    | True    |    510.2 μs |   5.12 μs |   2.68 μs |  1.59 |    0.13 |         - |          NA |
|                  |       |         |         |             |           |           |       |         |           |             |
| **Current**          | **160**   | **False**   | **False**   |  **8,595.7 μs** | **167.67 μs** |  **87.70 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| PackedReciprocal | 160   | False   | False   |  9,086.1 μs |  73.56 μs |  38.47 μs |  1.06 |    0.01 |         - |          NA |
| PackedFactorized | 160   | False   | False   |  1,716.6 μs |  18.67 μs |   8.29 μs |  0.20 |    0.00 |         - |          NA |
|                  |       |         |         |             |           |           |       |         |           |             |
| **Current**          | **160**   | **False**   | **True**    | **10,224.5 μs** | **230.70 μs** | **102.43 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| PackedReciprocal | 160   | False   | True    | 11,466.1 μs | 224.44 μs | 117.39 μs |  1.12 |    0.02 |         - |          NA |
| PackedFactorized | 160   | False   | True    |  3,618.0 μs |  90.48 μs |  47.32 μs |  0.35 |    0.01 |         - |          NA |
|                  |       |         |         |             |           |           |       |         |           |             |
| **Current**          | **160**   | **True**    | **False**   |  **1,544.6 μs** |  **27.11 μs** |  **12.03 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| PackedReciprocal | 160   | True    | False   |  2,075.4 μs |  18.16 μs |   8.06 μs |  1.34 |    0.01 |         - |          NA |
| PackedFactorized | 160   | True    | False   |  2,071.2 μs |  18.56 μs |   9.71 μs |  1.34 |    0.01 |         - |          NA |
|                  |       |         |         |             |           |           |       |         |           |             |
| **Current**          | **160**   | **True**    | **True**    |  **2,906.0 μs** | **192.37 μs** | **100.62 μs** |  **1.00** |    **0.00** |         **-** |          **NA** |
| PackedReciprocal | 160   | True    | True    |  4,113.6 μs | 148.39 μs |  77.61 μs |  1.42 |    0.05 |         - |          NA |
| PackedFactorized | 160   | True    | True    |  4,144.3 μs |  87.83 μs |  45.93 μs |  1.43 |    0.05 |         - |          NA |

```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
Memory: 63.69 GB Total, 29.99 GB Available
.NET SDK 11.0.100-rc.3.26522.148
  [Host]     : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4
  Job-MGUEMN : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4

EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0,DOTNET_TC_OnStackReplacement=1,COMPlus_TC_OnStackReplacement=1,DOTNET_TieredCompilation=1,DOTNET_TieredPGO=1  Server=True  Arguments=/p:Platform=x64  
IterationCount=8  IterationTime=250ms  LaunchCount=1  
WarmupCount=3  

```
| Method                       | Scenario | Mean         | Error        | StdDev       | Ratio | RatioSD | Gen0      | Gen1     | Gen2     | Allocated   | Alloc Ratio |
|----------------------------- |--------- |-------------:|-------------:|-------------:|------:|--------:|----------:|---------:|---------:|------------:|------------:|
| **CurrentPairOptics**            | **150/1/3**  |  **3,694.81 μs** |   **230.686 μs** |   **102.426 μs** |  **1.00** |    **0.00** |  **237.5000** | **187.5000** | **150.0000** |   **4801.7 KB** |        **1.00** |
| ReusePhasesPerThickness      | 150/1/3  |  2,001.35 μs |   496.961 μs |   259.920 μs |  0.54 |    0.07 |  178.5714 | 169.6429 | 160.7143 |  1805.33 KB |        0.38 |
| ReuseOpticsAcrossThicknesses | 150/1/3  |  2,037.90 μs |   430.171 μs |   224.988 μs |  0.55 |    0.06 |  180.5556 | 173.6111 | 166.6667 |  1805.33 KB |        0.38 |
|                              |          |              |              |              |       |         |           |          |          |             |             |
| **CurrentPairOptics**            | **150/5/3**  | **19,347.69 μs** | **1,584.209 μs** |   **703.399 μs** |  **1.00** |    **0.00** | **1090.9091** | **818.1818** | **636.3636** | **24007.62 KB** |        **1.00** |
| ReusePhasesPerThickness      | 150/5/3  | 10,066.16 μs | 2,248.639 μs | 1,176.082 μs |  0.52 |    0.06 |  843.7500 | 812.5000 | 781.2500 |  8979.06 KB |        0.37 |
| ReuseOpticsAcrossThicknesses | 150/5/3  |  5,393.76 μs |   327.568 μs |   145.442 μs |  0.28 |    0.01 |  578.1250 | 546.8750 | 531.2500 |  6058.24 KB |        0.25 |
|                              |          |              |              |              |       |         |           |          |          |             |             |
| **CurrentPairOptics**            | **300/3/7**  | **82,685.46 μs** | **1,820.667 μs** |   **952.244 μs** |  **1.00** |    **0.00** |  **500.0000** |        **-** |        **-** | **58824.33 KB** |        **1.00** |
| ReusePhasesPerThickness      | 300/3/7  | 26,331.63 μs | 2,984.844 μs | 1,561.131 μs |  0.32 |    0.02 |  700.0000 | 600.0000 | 600.0000 | 14423.41 KB |        0.25 |
| ReuseOpticsAcrossThicknesses | 300/3/7  | 17,256.00 μs | 2,829.486 μs | 1,479.876 μs |  0.21 |    0.02 |  500.0000 | 500.0000 | 500.0000 | 11115.12 KB |        0.19 |
|                              |          |              |              |              |       |         |           |          |          |             |             |
| **CurrentPairOptics**            | **50/1/1**   |    **117.30 μs** |     **1.560 μs** |     **0.816 μs** |  **1.00** |    **0.00** |    **8.3955** |   **0.4664** |        **-** |   **348.76 KB** |        **1.00** |
| ReusePhasesPerThickness      | 50/1/1   |     28.26 μs |     0.653 μs |     0.341 μs |  0.24 |    0.00 |    1.2190 |        - |        - |    52.34 KB |        0.15 |
| ReuseOpticsAcrossThicknesses | 50/1/1   |     29.77 μs |     0.680 μs |     0.302 μs |  0.25 |    0.00 |    1.1950 |        - |        - |     52.3 KB |        0.15 |

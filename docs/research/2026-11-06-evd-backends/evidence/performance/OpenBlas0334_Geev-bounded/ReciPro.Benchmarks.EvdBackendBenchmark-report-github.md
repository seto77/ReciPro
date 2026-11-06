```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
Memory: 63.69 GB Total, 18.71 GB Available
.NET SDK 11.0.100-rc.3.26522.148
  [Host]    : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4
  InProcess : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4

Job=InProcess  OutlierMode=DontRemove  EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0,DOTNET_TC_OnStackReplacement=1,COMPlus_TC_OnStackReplacement=1,DOTNET_TieredCompilation=1,DOTNET_TieredPGO=1  
Server=True  Arguments=/p:Platform=x64  Runtime=.NET 11.0 (Windows7.0)  
Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=8  
LaunchCount=1  RunStrategy=Monitoring  UnrollFactor=1  
WarmupCount=3  

```
| Method               | Categories  | Scenario | Mean       | Error      | StdDev    | StdErr     | Min        | Q1         | Median     | Q3         | Max         | Op/s    | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |------------ |--------- |-----------:|-----------:|----------:|-----------:|-----------:|-----------:|-----------:|-----------:|------------:|--------:|------:|--------:|----------:|------------:|
| **EigenEvd**             | **EVD**         | **Au448**    | **292.011 ms** |  **28.378 ms** | **14.842 ms** |  **5.2476 ms** | **272.006 ms** | **284.377 ms** | **290.452 ms** | **296.970 ms** |   **321.87 ms** |   **3.425** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Au448    | 201.761 ms |  12.871 ms |  6.732 ms |  2.3800 ms | 192.969 ms | 198.358 ms | 200.206 ms | 203.366 ms |   213.54 ms |   4.956 |  0.69 |    0.04 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **GaAs256**  | **122.758 ms** |  **24.791 ms** | **12.966 ms** |  **4.5842 ms** | **112.191 ms** | **112.698 ms** | **120.554 ms** | **125.328 ms** |   **151.46 ms** |   **8.146** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | GaAs256  |  51.873 ms |   5.987 ms |  3.131 ms |  1.1071 ms |  47.674 ms |  49.288 ms |  51.981 ms |  54.537 ms |    55.43 ms |  19.278 |  0.43 |    0.04 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si128**    |   **8.185 ms** |   **2.096 ms** |  **1.096 ms** |  **0.3876 ms** |   **7.098 ms** |   **7.665 ms** |   **7.763 ms** |   **8.213 ms** |    **10.43 ms** | **122.170** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si128    |  14.966 ms |   3.282 ms |  1.717 ms |  0.6070 ms |  12.477 ms |  13.973 ms |  15.060 ms |  16.239 ms |    17.30 ms |  66.816 |  1.85 |    0.29 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si256**    | **117.218 ms** |   **6.714 ms** |  **3.512 ms** |  **1.2415 ms** | **114.106 ms** | **115.316 ms** | **116.280 ms** | **117.914 ms** |   **125.06 ms** |   **8.531** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si256    |  55.127 ms |   6.176 ms |  3.230 ms |  1.1420 ms |  50.963 ms |  53.845 ms |  54.761 ms |  56.331 ms |    61.52 ms |  18.140 |  0.47 |    0.03 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si448**    | **273.943 ms** |  **22.426 ms** | **11.729 ms** |  **4.1470 ms** | **258.055 ms** | **267.480 ms** | **271.557 ms** | **279.925 ms** |   **294.35 ms** |   **3.650** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si448    | 203.598 ms |  16.440 ms |  8.599 ms |  3.0401 ms | 190.998 ms | 197.978 ms | 205.250 ms | 209.538 ms |   213.60 ms |   4.912 |  0.74 |    0.04 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si640**    | **910.730 ms** | **110.240 ms** | **57.658 ms** | **20.3851 ms** | **841.433 ms** | **872.002 ms** | **905.784 ms** | **937.009 ms** | **1,021.39 ms** |   **1.098** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si640    | 555.012 ms |  22.912 ms | 11.983 ms |  4.2367 ms | 534.290 ms | 547.929 ms | 556.487 ms | 561.762 ms |   573.45 ms |   1.802 |  0.61 |    0.04 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Au448**    | **293.034 ms** |  **32.041 ms** | **16.758 ms** |  **5.9249 ms** | **275.697 ms** | **283.103 ms** | **287.989 ms** | **299.311 ms** |   **322.69 ms** |   **3.413** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Au448    | 209.456 ms |  27.869 ms | 14.576 ms |  5.1534 ms | 184.425 ms | 206.141 ms | 211.242 ms | 213.458 ms |   235.06 ms |   4.774 |  0.72 |    0.06 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **GaAs256**  | **127.813 ms** |  **12.495 ms** |  **6.535 ms** |  **2.3106 ms** | **120.415 ms** | **123.394 ms** | **126.247 ms** | **131.646 ms** |   **139.48 ms** |   **7.824** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | GaAs256  |  56.400 ms |   4.562 ms |  2.386 ms |  0.8436 ms |  53.035 ms |  55.140 ms |  56.057 ms |  57.070 ms |    61.02 ms |  17.731 |  0.44 |    0.03 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si128**    |   **9.361 ms** |   **4.447 ms** |  **2.326 ms** |  **0.8223 ms** |   **7.545 ms** |   **7.925 ms** |   **8.269 ms** |   **9.879 ms** |    **13.66 ms** | **106.824** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si128    |  18.106 ms |   7.086 ms |  3.706 ms |  1.3103 ms |  14.268 ms |  15.128 ms |  17.683 ms |  19.018 ms |    24.91 ms |  55.231 |  2.02 |    0.55 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si256**    | **125.019 ms** |   **9.956 ms** |  **5.207 ms** |  **1.8411 ms** | **116.466 ms** | **122.339 ms** | **125.029 ms** | **127.181 ms** |   **133.63 ms** |   **7.999** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si256    |  61.146 ms |  14.800 ms |  7.741 ms |  2.7367 ms |  49.130 ms |  58.684 ms |  60.140 ms |  61.918 ms |    76.90 ms |  16.354 |  0.49 |    0.06 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si448**    | **314.698 ms** |  **58.627 ms** | **30.663 ms** | **10.8410 ms** | **283.341 ms** | **293.700 ms** | **303.985 ms** | **331.051 ms** |   **376.94 ms** |   **3.178** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si448    | 250.685 ms |  41.367 ms | 21.636 ms |  7.6494 ms | 214.716 ms | 236.674 ms | 255.645 ms | 265.884 ms |   275.10 ms |   3.989 |  0.80 |    0.09 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si640**    | **947.456 ms** |  **89.017 ms** | **46.558 ms** | **16.4606 ms** | **896.468 ms** | **911.058 ms** | **939.817 ms** | **966.848 ms** | **1,031.03 ms** |   **1.055** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si640    | 630.945 ms |  57.574 ms | 30.112 ms | 10.6463 ms | 600.763 ms | 605.897 ms | 632.043 ms | 639.117 ms |   693.77 ms |   1.585 |  0.67 |    0.04 |         - |          NA |

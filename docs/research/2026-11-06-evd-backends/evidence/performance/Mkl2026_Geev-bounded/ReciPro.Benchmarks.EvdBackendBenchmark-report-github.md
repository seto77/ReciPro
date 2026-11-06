```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
Memory: 63.69 GB Total, 22.94 GB Available
.NET SDK 11.0.100-rc.3.26522.148
  [Host]    : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4
  InProcess : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4

Job=InProcess  OutlierMode=DontRemove  EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0,DOTNET_TC_OnStackReplacement=1,COMPlus_TC_OnStackReplacement=1,DOTNET_TieredCompilation=1,DOTNET_TieredPGO=1  
Server=True  Arguments=/p:Platform=x64  Runtime=.NET 11.0 (Windows7.0)  
Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=8  
LaunchCount=1  RunStrategy=Monitoring  UnrollFactor=1  
WarmupCount=3  

```
| Method               | Categories  | Scenario | Mean       | Error      | StdDev     | StdErr     | Min        | Q1         | Median     | Q3         | Max          | Op/s    | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |------------ |--------- |-----------:|-----------:|-----------:|-----------:|-----------:|-----------:|-----------:|-----------:|-------------:|--------:|------:|--------:|----------:|------------:|
| **EigenEvd**             | **EVD**         | **Au448**    | **317.148 ms** |  **61.192 ms** | **32.0047 ms** | **11.3154 ms** | **271.442 ms** | **294.362 ms** | **316.270 ms** | **341.842 ms** |   **354.825 ms** |   **3.153** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Au448    | 293.660 ms |  48.829 ms | 25.5386 ms |  9.0292 ms | 268.560 ms | 276.878 ms | 281.343 ms | 307.612 ms |   332.899 ms |   3.405 |  0.93 |    0.12 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **GaAs256**  | **145.140 ms** |  **49.241 ms** | **25.7538 ms** |  **9.1053 ms** | **119.093 ms** | **130.446 ms** | **136.242 ms** | **155.244 ms** |   **194.880 ms** |   **6.890** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | GaAs256  |  81.155 ms |  19.598 ms | 10.2500 ms |  3.6239 ms |  71.372 ms |  72.187 ms |  79.720 ms |  87.141 ms |    99.718 ms |  12.322 |  0.57 |    0.11 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si128**    |   **8.568 ms** |   **4.589 ms** |  **2.4004 ms** |  **0.8487 ms** |   **6.871 ms** |   **7.322 ms** |   **7.698 ms** |   **8.590 ms** |    **14.273 ms** | **116.708** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si128    |  21.106 ms |  13.848 ms |  7.2430 ms |  2.5608 ms |  11.552 ms |  15.909 ms |  20.505 ms |  25.873 ms |    32.794 ms |  47.380 |  2.58 |    0.97 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si256**    | **130.985 ms** |  **25.847 ms** | **13.5187 ms** |  **4.7796 ms** | **117.770 ms** | **122.546 ms** | **126.816 ms** | **132.980 ms** |   **154.764 ms** |   **7.634** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si256    |  82.114 ms |  18.076 ms |  9.4542 ms |  3.3426 ms |  70.350 ms |  76.188 ms |  77.632 ms |  92.355 ms |    94.148 ms |  12.178 |  0.63 |    0.09 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si448**    | **309.645 ms** |  **61.346 ms** | **32.0850 ms** | **11.3438 ms** | **272.582 ms** | **281.191 ms** | **309.846 ms** | **322.669 ms** |   **358.997 ms** |   **3.230** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si448    | 305.136 ms |  51.202 ms | 26.7797 ms |  9.4681 ms | 266.739 ms | 285.661 ms | 305.506 ms | 320.862 ms |   349.565 ms |   3.277 |  0.99 |    0.13 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si640**    | **994.963 ms** | **188.881 ms** | **98.7884 ms** | **34.9270 ms** | **910.562 ms** | **947.369 ms** | **965.771 ms** | **996.409 ms** | **1,227.067 ms** |   **1.005** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si640    | 744.360 ms |  30.582 ms | 15.9951 ms |  5.6551 ms | 721.551 ms | 734.837 ms | 742.545 ms | 752.188 ms |   774.095 ms |   1.343 |  0.75 |    0.06 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Au448**    | **329.353 ms** |  **43.252 ms** | **22.6219 ms** |  **7.9980 ms** | **304.913 ms** | **315.980 ms** | **324.981 ms** | **334.671 ms** |   **373.725 ms** |   **3.036** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Au448    | 323.485 ms |  49.138 ms | 25.7002 ms |  9.0864 ms | 288.105 ms | 311.142 ms | 318.460 ms | 329.022 ms |   373.630 ms |   3.091 |  0.99 |    0.10 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **GaAs256**  | **123.844 ms** |   **4.459 ms** |  **2.3320 ms** |  **0.8245 ms** | **121.079 ms** | **122.021 ms** | **123.477 ms** | **125.864 ms** |   **127.354 ms** |   **8.075** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | GaAs256  |  72.546 ms |  11.534 ms |  6.0328 ms |  2.1329 ms |  63.440 ms |  68.238 ms |  72.516 ms |  76.145 ms |    80.865 ms |  13.784 |  0.59 |    0.05 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si128**    |   **8.054 ms** |   **1.514 ms** |  **0.7920 ms** |  **0.2800 ms** |   **7.266 ms** |   **7.622 ms** |   **7.770 ms** |   **8.205 ms** |     **9.498 ms** | **124.164** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si128    |  14.908 ms |   2.240 ms |  1.1715 ms |  0.4142 ms |  13.656 ms |  14.216 ms |  14.612 ms |  15.190 ms |    17.475 ms |  67.080 |  1.87 |    0.21 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si256**    | **118.389 ms** |   **6.081 ms** |  **3.1803 ms** |  **1.1244 ms** | **114.392 ms** | **116.262 ms** | **117.674 ms** | **120.933 ms** |   **123.275 ms** |   **8.447** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si256    |  75.390 ms |   7.593 ms |  3.9710 ms |  1.4040 ms |  69.524 ms |  72.547 ms |  75.172 ms |  77.990 ms |    81.028 ms |  13.264 |  0.64 |    0.04 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si448**    | **273.680 ms** |  **24.950 ms** | **13.0491 ms** |  **4.6136 ms** | **248.776 ms** | **268.573 ms** | **274.819 ms** | **283.472 ms** |   **288.330 ms** |   **3.654** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si448    | 279.612 ms |  25.446 ms | 13.3088 ms |  4.7054 ms | 257.862 ms | 268.629 ms | 283.514 ms | 288.495 ms |   294.920 ms |   3.576 |  1.02 |    0.07 |         - |          NA |
|                      |             |          |            |            |            |            |            |            |            |            |              |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si640**    | **888.163 ms** |  **71.696 ms** | **37.4984 ms** | **13.2577 ms** | **820.237 ms** | **872.698 ms** | **893.537 ms** | **906.326 ms** |   **945.894 ms** |   **1.126** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si640    | 739.297 ms |  32.798 ms | 17.1542 ms |  6.0649 ms | 718.313 ms | 730.524 ms | 737.953 ms | 742.140 ms |   775.904 ms |   1.353 |  0.83 |    0.04 |         - |          NA |

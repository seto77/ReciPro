```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
Memory: 63.69 GB Total, 17.27 GB Available
.NET SDK 11.0.100-rc.3.26522.148
  [Host]    : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4
  InProcess : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4

Job=InProcess  OutlierMode=DontRemove  EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0,DOTNET_TC_OnStackReplacement=1,COMPlus_TC_OnStackReplacement=1,DOTNET_TieredCompilation=1,DOTNET_TieredPGO=1  
Server=True  Arguments=/p:Platform=x64  Runtime=.NET 11.0 (Windows7.0)  
Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=8  
LaunchCount=1  RunStrategy=Monitoring  UnrollFactor=1  
WarmupCount=3  

```
| Method               | Categories  | Scenario | Mean         | Error      | StdDev     | StdErr     | Min          | Q1           | Median       | Q3           | Max         | Op/s     | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |------------ |--------- |-------------:|-----------:|-----------:|-----------:|-------------:|-------------:|-------------:|-------------:|------------:|---------:|------:|--------:|----------:|------------:|
| **EigenEvd**             | **EVD**         | **Si128**    |     **8.027 ms** |   **2.751 ms** |   **1.439 ms** |  **0.5087 ms** |     **6.482 ms** |     **7.091 ms** |     **7.696 ms** |     **8.466 ms** |    **11.03 ms** | **124.5764** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si128    |    21.805 ms |  14.876 ms |   7.780 ms |  2.7507 ms |    14.941 ms |    17.308 ms |    19.273 ms |    22.712 ms |    38.21 ms |  45.8611 |  2.78 |    1.03 |         - |          NA |
|                      |             |          |              |            |            |            |              |              |              |              |             |          |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si448**    |   **306.669 ms** |  **82.870 ms** |  **43.343 ms** | **15.3240 ms** |   **268.177 ms** |   **278.006 ms** |   **290.458 ms** |   **317.598 ms** |   **395.57 ms** |   **3.2608** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si448    |   367.484 ms |  36.418 ms |  19.047 ms |  6.7343 ms |   343.713 ms |   348.344 ms |   372.078 ms |   380.680 ms |   393.61 ms |   2.7212 |  1.22 |    0.15 |         - |          NA |
|                      |             |          |              |            |            |            |              |              |              |              |             |          |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si640**    |   **942.883 ms** | **103.668 ms** |  **54.220 ms** | **19.1698 ms** |   **882.789 ms** |   **901.425 ms** |   **925.591 ms** |   **987.921 ms** | **1,031.24 ms** |   **1.0606** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si640    | 1,000.241 ms |  77.725 ms |  40.652 ms | 14.3726 ms |   948.283 ms |   965.089 ms | 1,006.233 ms | 1,027.810 ms | 1,058.77 ms |   0.9998 |  1.06 |    0.07 |         - |          NA |
|                      |             |          |              |            |            |            |              |              |              |              |             |          |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si128**    |     **8.680 ms** |   **2.542 ms** |   **1.329 ms** |  **0.4700 ms** |     **7.005 ms** |     **7.985 ms** |     **8.353 ms** |     **8.886 ms** |    **10.99 ms** | **115.2094** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si128    |    18.913 ms |   5.393 ms |   2.821 ms |  0.9973 ms |    15.431 ms |    17.227 ms |    18.587 ms |    19.483 ms |    24.80 ms |  52.8733 |  2.22 |    0.43 |         - |          NA |
|                      |             |          |              |            |            |            |              |              |              |              |             |          |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si448**    |   **313.074 ms** |  **30.295 ms** |  **15.845 ms** |  **5.6020 ms** |   **295.130 ms** |   **301.345 ms** |   **308.126 ms** |   **327.005 ms** |   **336.76 ms** |   **3.1941** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si448    |   376.730 ms |  29.346 ms |  15.348 ms |  5.4264 ms |   360.107 ms |   363.063 ms |   375.299 ms |   386.460 ms |   401.00 ms |   2.6544 |  1.21 |    0.07 |         - |          NA |
|                      |             |          |              |            |            |            |              |              |              |              |             |          |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si640**    | **1,192.985 ms** | **264.157 ms** | **138.159 ms** | **48.8467 ms** |   **995.276 ms** | **1,102.962 ms** | **1,169.600 ms** | **1,267.017 ms** | **1,415.38 ms** |   **0.8382** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si640    | 1,194.077 ms | 171.727 ms |  89.816 ms | 31.7549 ms | 1,040.562 ms | 1,160.653 ms | 1,183.858 ms | 1,228.165 ms | 1,316.87 ms |   0.8375 |  1.01 |    0.13 |         - |          NA |

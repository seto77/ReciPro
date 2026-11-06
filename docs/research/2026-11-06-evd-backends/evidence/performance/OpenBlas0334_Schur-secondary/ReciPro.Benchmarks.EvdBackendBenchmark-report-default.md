
BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
Memory: 63.69 GB Total, 19.15 GB Available
.NET SDK 11.0.100-rc.3.26522.148
  [Host]    : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4
  InProcess : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4

Job=InProcess  OutlierMode=DontRemove  EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0,DOTNET_TC_OnStackReplacement=1,COMPlus_TC_OnStackReplacement=1,DOTNET_TieredCompilation=1,DOTNET_TieredPGO=1  
Server=True  Arguments=/p:Platform=x64  Runtime=.NET 11.0 (Windows7.0)  
Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=8  
LaunchCount=1  RunStrategy=Monitoring  UnrollFactor=1  
WarmupCount=3  

 Method               | Categories  | Scenario | Mean       | Error     | StdDev    | StdErr     | Min        | Q1         | Median     | Q3         | Max         | Op/s    | Ratio | RatioSD | Allocated | Alloc Ratio |
--------------------- |------------ |--------- |-----------:|----------:|----------:|-----------:|-----------:|-----------:|-----------:|-----------:|------------:|--------:|------:|--------:|----------:|------------:|
 **EigenEvd**             | **EVD**         | **Si128**    |   **7.431 ms** |  **3.005 ms** |  **1.571 ms** |  **0.5556 ms** |   **5.808 ms** |   **6.782 ms** |   **7.009 ms** |   **7.374 ms** |    **11.06 ms** | **134.577** |  **1.00** |    **0.00** |         **-** |          **NA** |
 CandidateEvd         | EVD         | Si128    |  16.521 ms |  3.048 ms |  1.594 ms |  0.5637 ms |  14.525 ms |  15.412 ms |  16.576 ms |  17.117 ms |    19.62 ms |  60.527 |  2.29 |    0.42 |         - |          NA |
                      |             |          |            |           |           |            |            |            |            |            |             |         |       |         |           |             |
 **EigenEvd**             | **EVD**         | **Si448**    | **288.081 ms** | **32.711 ms** | **17.109 ms** |  **6.0488 ms** | **269.038 ms** | **278.484 ms** | **283.736 ms** | **291.886 ms** |   **323.15 ms** |   **3.471** |  **1.00** |    **0.00** |         **-** |          **NA** |
 CandidateEvd         | EVD         | Si448    | 218.206 ms | 13.244 ms |  6.927 ms |  2.4491 ms | 208.332 ms | 214.456 ms | 217.212 ms | 224.244 ms |   227.94 ms |   4.583 |  0.76 |    0.05 |         - |          NA |
                      |             |          |            |           |           |            |            |            |            |            |             |         |       |         |           |             |
 **EigenEvd**             | **EVD**         | **Si640**    | **879.092 ms** | **78.071 ms** | **40.833 ms** | **14.4366 ms** | **830.146 ms** | **835.756 ms** | **891.508 ms** | **902.763 ms** |   **939.46 ms** |   **1.138** |  **1.00** |    **0.00** |         **-** |          **NA** |
 CandidateEvd         | EVD         | Si640    | 594.913 ms | 79.756 ms | 41.714 ms | 14.7481 ms | 550.467 ms | 571.120 ms | 579.309 ms | 614.665 ms |   677.76 ms |   1.681 |  0.68 |    0.05 |         - |          NA |
                      |             |          |            |           |           |            |            |            |            |            |             |         |       |         |           |             |
 **EigenPropagation**     | **Propagation** | **Si128**    |   **9.093 ms** |  **2.109 ms** |  **1.103 ms** |  **0.3900 ms** |   **7.818 ms** |   **8.398 ms** |   **8.670 ms** |   **9.640 ms** |    **11.02 ms** | **109.978** |  **1.00** |    **0.00** |         **-** |          **NA** |
 CandidatePropagation | Propagation | Si128    |  21.133 ms |  5.864 ms |  3.067 ms |  1.0843 ms |  15.994 ms |  19.581 ms |  21.601 ms |  22.993 ms |    24.77 ms |  47.319 |  2.35 |    0.41 |         - |          NA |
                      |             |          |            |           |           |            |            |            |            |            |             |         |       |         |           |             |
 **EigenPropagation**     | **Propagation** | **Si448**    | **292.757 ms** | **29.755 ms** | **15.563 ms** |  **5.5022 ms** | **276.619 ms** | **282.785 ms** | **284.661 ms** | **304.428 ms** |   **319.61 ms** |   **3.416** |  **1.00** |    **0.00** |         **-** |          **NA** |
 CandidatePropagation | Propagation | Si448    | 236.216 ms | 35.378 ms | 18.503 ms |  6.5420 ms | 218.030 ms | 223.259 ms | 229.237 ms | 246.577 ms |   272.47 ms |   4.233 |  0.81 |    0.07 |         - |          NA |
                      |             |          |            |           |           |            |            |            |            |            |             |         |       |         |           |             |
 **EigenPropagation**     | **Propagation** | **Si640**    | **956.037 ms** | **97.304 ms** | **50.892 ms** | **17.9929 ms** | **863.360 ms** | **934.470 ms** | **961.277 ms** | **982.460 ms** | **1,027.16 ms** |   **1.046** |  **1.00** |    **0.00** |         **-** |          **NA** |
 CandidatePropagation | Propagation | Si640    | 620.918 ms | 46.249 ms | 24.189 ms |  8.5522 ms | 573.564 ms | 608.080 ms | 630.159 ms | 638.285 ms |   642.12 ms |   1.611 |  0.65 |    0.04 |         - |          NA |

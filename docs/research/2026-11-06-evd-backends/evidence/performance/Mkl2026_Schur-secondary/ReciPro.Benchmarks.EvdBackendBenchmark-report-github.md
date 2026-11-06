```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
Memory: 63.69 GB Total, 17.08 GB Available
.NET SDK 11.0.100-rc.3.26522.148
  [Host]    : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4
  InProcess : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4

Job=InProcess  OutlierMode=DontRemove  EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0,DOTNET_TC_OnStackReplacement=1,COMPlus_TC_OnStackReplacement=1,DOTNET_TieredCompilation=1,DOTNET_TieredPGO=1  
Server=True  Arguments=/p:Platform=x64  Runtime=.NET 11.0 (Windows7.0)  
Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=8  
LaunchCount=1  RunStrategy=Monitoring  UnrollFactor=1  
WarmupCount=3  

```
| Method               | Categories  | Scenario | Mean       | Error      | StdDev    | StdErr     | Min        | Q1         | Median     | Q3         | Max          | Op/s    | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |------------ |--------- |-----------:|-----------:|----------:|-----------:|-----------:|-----------:|-----------:|-----------:|-------------:|--------:|------:|--------:|----------:|------------:|
| **EigenEvd**             | **EVD**         | **Si128**    |   **7.774 ms** |   **1.979 ms** |  **1.035 ms** |  **0.3659 ms** |   **6.215 ms** |   **7.274 ms** |   **7.571 ms** |   **8.166 ms** |     **9.667 ms** | **128.626** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si128    |  16.419 ms |  11.299 ms |  5.909 ms |  2.0893 ms |  12.421 ms |  13.230 ms |  14.899 ms |  16.148 ms |    30.614 ms |  60.905 |  2.14 |    0.78 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |              |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si448**    | **288.249 ms** |  **18.564 ms** |  **9.709 ms** |  **3.4328 ms** | **276.274 ms** | **281.642 ms** | **287.519 ms** | **291.813 ms** |   **307.394 ms** |   **3.469** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si448    | 284.452 ms |  33.914 ms | 17.737 ms |  6.2711 ms | 259.384 ms | 269.720 ms | 284.417 ms | 297.485 ms |   307.615 ms |   3.516 |  0.99 |    0.07 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |              |         |       |         |           |             |
| **EigenEvd**             | **EVD**         | **Si640**    | **903.728 ms** |  **92.014 ms** | **48.125 ms** | **17.0148 ms** | **827.596 ms** | **874.305 ms** | **895.605 ms** | **942.540 ms** |   **977.181 ms** |   **1.107** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidateEvd         | EVD         | Si640    | 694.876 ms |  45.505 ms | 23.800 ms |  8.4145 ms | 665.446 ms | 676.822 ms | 688.673 ms | 717.813 ms |   727.795 ms |   1.439 |  0.77 |    0.05 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |              |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si128**    |   **9.286 ms** |   **1.931 ms** |  **1.010 ms** |  **0.3571 ms** |   **8.008 ms** |   **8.820 ms** |   **9.034 ms** |   **9.469 ms** |    **11.357 ms** | **107.686** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si128    |  18.269 ms |   9.215 ms |  4.820 ms |  1.7040 ms |  14.488 ms |  14.728 ms |  16.136 ms |  20.174 ms |    27.550 ms |  54.738 |  1.99 |    0.53 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |              |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si448**    | **293.804 ms** |  **24.843 ms** | **12.993 ms** |  **4.5939 ms** | **276.661 ms** | **283.346 ms** | **293.435 ms** | **300.436 ms** |   **316.647 ms** |   **3.404** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si448    | 294.134 ms |  25.027 ms | 13.089 ms |  4.6278 ms | 274.854 ms | 284.607 ms | 296.521 ms | 302.926 ms |   311.622 ms |   3.400 |  1.00 |    0.06 |         - |          NA |
|                      |             |          |            |            |           |            |            |            |            |            |              |         |       |         |           |             |
| **EigenPropagation**     | **Propagation** | **Si640**    | **923.405 ms** | **129.424 ms** | **67.691 ms** | **23.9325 ms** | **852.773 ms** | **872.272 ms** | **909.352 ms** | **959.679 ms** | **1,051.084 ms** |   **1.083** |  **1.00** |    **0.00** |         **-** |          **NA** |
| CandidatePropagation | Propagation | Si640    | 761.775 ms |  93.694 ms | 49.004 ms | 17.3254 ms | 692.846 ms | 729.697 ms | 755.272 ms | 786.828 ms |   839.134 ms |   1.313 |  0.83 |    0.07 |         - |          NA |

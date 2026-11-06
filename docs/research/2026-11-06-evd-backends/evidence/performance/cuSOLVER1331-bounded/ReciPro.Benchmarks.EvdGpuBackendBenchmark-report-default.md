
BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
Memory: 63.69 GB Total, 17.43 GB Available
.NET SDK 11.0.100-rc.3.26522.148
  [Host]    : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4
  InProcess : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4

Job=InProcess  OutlierMode=DontRemove  EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0,DOTNET_TC_OnStackReplacement=1,COMPlus_TC_OnStackReplacement=1,DOTNET_TieredCompilation=1,DOTNET_TieredPGO=1  
Server=True  Arguments=/p:Platform=x64  Runtime=.NET 11.0 (Windows7.0)  
Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=8  
LaunchCount=1  RunStrategy=Monitoring  UnrollFactor=1  
WarmupCount=3  

 Method   | Scenario | Mean       | Error      | StdDev    | StdErr     | Min        | Q1         | Median     | Q3         | Max         | Op/s    | Ratio | RatioSD | Allocated | Alloc Ratio |
--------- |--------- |-----------:|-----------:|----------:|-----------:|-----------:|-----------:|-----------:|-----------:|------------:|--------:|------:|--------:|----------:|------------:|
 **EigenEvd** | **Au448**    | **283.262 ms** |  **18.632 ms** |  **9.745 ms** |  **3.4453 ms** | **266.079 ms** | **278.151 ms** | **283.833 ms** | **290.474 ms** |   **295.49 ms** |   **3.530** |  **1.00** |    **0.00** |         **-** |          **NA** |
 GpuEvd   | Au448    | 290.271 ms |  25.537 ms | 13.356 ms |  4.7221 ms | 272.698 ms | 279.820 ms | 291.115 ms | 302.376 ms |   303.66 ms |   3.445 |  1.03 |    0.06 |         - |          NA |
          |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
 **EigenEvd** | **GaAs256**  | **143.662 ms** |  **25.120 ms** | **13.138 ms** |  **4.6451 ms** | **125.906 ms** | **135.832 ms** | **141.831 ms** | **149.834 ms** |   **166.92 ms** |   **6.961** |  **1.00** |    **0.00** |         **-** |          **NA** |
 GpuEvd   | GaAs256  | 116.197 ms |  14.108 ms |  7.379 ms |  2.6088 ms | 106.324 ms | 110.686 ms | 115.800 ms | 119.632 ms |   128.69 ms |   8.606 |  0.81 |    0.08 |         - |          NA |
          |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
 **EigenEvd** | **Si128**    |   **8.839 ms** |   **2.795 ms** |  **1.462 ms** |  **0.5168 ms** |   **7.480 ms** |   **7.703 ms** |   **8.268 ms** |   **9.837 ms** |    **11.08 ms** | **113.132** |  **1.00** |    **0.00** |         **-** |          **NA** |
 GpuEvd   | Si128    |  51.016 ms |   6.699 ms |  3.504 ms |  1.2388 ms |  45.870 ms |  49.291 ms |  51.052 ms |  52.253 ms |    57.55 ms |  19.602 |  5.90 |    0.94 |         - |          NA |
          |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
 **EigenEvd** | **Si256**    | **130.752 ms** |  **14.527 ms** |  **7.598 ms** |  **2.6862 ms** | **122.377 ms** | **125.166 ms** | **127.729 ms** | **137.342 ms** |   **143.44 ms** |   **7.648** |  **1.00** |    **0.00** |         **-** |          **NA** |
 GpuEvd   | Si256    | 123.136 ms |   8.388 ms |  4.387 ms |  1.5510 ms | 117.745 ms | 120.809 ms | 122.326 ms | 124.270 ms |   131.91 ms |   8.121 |  0.94 |    0.06 |         - |          NA |
          |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
 **EigenEvd** | **Si448**    | **304.185 ms** |  **27.281 ms** | **14.269 ms** |  **5.0447 ms** | **289.100 ms** | **294.492 ms** | **299.473 ms** | **313.715 ms** |   **326.57 ms** |   **3.287** |  **1.00** |    **0.00** |         **-** |          **NA** |
 GpuEvd   | Si448    | 276.268 ms |  27.581 ms | 14.425 ms |  5.1001 ms | 262.965 ms | 264.130 ms | 270.114 ms | 290.733 ms |   297.40 ms |   3.620 |  0.91 |    0.06 |         - |          NA |
          |          |            |            |           |            |            |            |            |            |             |         |       |         |           |             |
 **EigenEvd** | **Si640**    | **944.061 ms** | **125.501 ms** | **65.640 ms** | **23.2071 ms** | **869.991 ms** | **921.021 ms** | **928.966 ms** | **940.697 ms** | **1,095.40 ms** |   **1.059** |  **1.00** |    **0.00** |         **-** |          **NA** |
 GpuEvd   | Si640    | 481.041 ms |  19.856 ms | 10.385 ms |  3.6717 ms | 466.034 ms | 476.231 ms | 479.974 ms | 487.133 ms |   496.41 ms |   2.079 |  0.51 |    0.03 |         - |          NA |

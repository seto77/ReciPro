```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 11.0.100-rc.3.26522.148
  [Host]     : .NET 10.0.14 (10.0.14, 10.0.1426.52326), X64 RyuJIT x86-64-v4
  Job-PYWAFI : .NET 10.0.14 (10.0.14, 10.0.1426.52326), X64 RyuJIT x86-64-v4

EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0  Server=True  Arguments=/p:Platform=x64  
IterationCount=3  LaunchCount=1  WarmupCount=2  

```
| Method     | BeamCount | Mean       | Error      | StdDev    | Ratio | RatioSD | Gen0    | Gen1    | Gen2    | Allocated | Alloc Ratio |
|----------- |---------- |-----------:|-----------:|----------:|------:|--------:|--------:|--------:|--------:|----------:|------------:|
| **PairSum**    | **50**        |  **15.546 ms** |  **26.927 ms** | **1.4760 ms** |  **1.01** |    **0.12** |       **-** |       **-** |       **-** |  **96.02 KB** |        **1.00** |
| GroupedSum | 50        |   2.255 ms |   3.539 ms | 0.1940 ms |  0.15 |    0.02 | 13.6719 | 13.6719 | 13.6719 |  96.05 KB |        1.00 |
|            |           |            |            |           |       |         |         |         |         |           |             |
| **PairSum**    | **150**       | **147.745 ms** | **152.139 ms** | **8.3393 ms** |  **1.00** |    **0.07** |       **-** |       **-** |       **-** |  **96.03 KB** |        **1.00** |
| GroupedSum | 150       |  50.785 ms |  25.185 ms | 1.3805 ms |  0.34 |    0.02 |       - |       - |       - |  96.02 KB |        1.00 |

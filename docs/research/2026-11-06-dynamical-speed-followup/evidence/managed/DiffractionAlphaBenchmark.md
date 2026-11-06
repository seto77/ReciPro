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
| Method              | Dimension | Mean           | Error         | StdDev        | Ratio | Gen0    | Gen1   | Allocated | Alloc Ratio |
|-------------------- |---------- |---------------:|--------------:|--------------:|------:|--------:|-------:|----------:|------------:|
| **MatrixTimesIncident** | **8**         |     **120.068 ns** |     **4.8079 ns** |     **2.5146 ns** |  **1.00** |  **0.0146** |      **-** |     **616 B** |        **1.00** |
| FirstColumn         | 8         |       8.035 ns |     0.3743 ns |     0.1662 ns |  0.07 |  0.0037 |      - |     152 B |        0.25 |
|                     |           |                |               |               |       |         |        |           |             |
| **MatrixTimesIncident** | **32**        |   **3,638.133 ns** |   **433.7119 ns** |   **226.8397 ns** | **1.000** |  **0.5114** |      **-** |   **21312 B** |        **1.00** |
| FirstColumn         | 32        |      25.591 ns |     2.2239 ns |     0.9874 ns | 0.007 |  0.0130 |      - |     536 B |        0.03 |
|                     |           |                |               |               |       |         |        |           |             |
| **MatrixTimesIncident** | **128**       |  **23,739.667 ns** | **3,085.9807 ns** | **1,370.1945 ns** | **1.000** |  **6.8132** | **0.0908** |  **274753 B** |       **1.000** |
| FirstColumn         | 128       |     103.490 ns |    17.9929 ns |     9.4106 ns | 0.004 |  0.0500 |      - |    2072 B |       0.008 |
|                     |           |                |               |               |       |         |        |           |             |
| **MatrixTimesIncident** | **400**       | **185,848.134 ns** | **7,227.3456 ns** | **3,208.9862 ns** | **1.000** | **63.3013** | **3.2051** | **2594375 B** |       **1.000** |
| FirstColumn         | 400       |     285.339 ns |    23.5965 ns |    12.3414 ns | 0.002 |  0.1560 |      - |    6424 B |       0.002 |

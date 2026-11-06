```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26300.9818)
AMD Ryzen 9 9950X 4.30GHz, 1 CPU, 32 logical and 16 physical cores
Memory: 63.69 GB Total, 30.43 GB Available
.NET SDK 11.0.100-rc.3.26522.148
  [Host]     : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4
  Job-MGUEMN : .NET 11.0.0 (11.0.0-rc.3.26522.148, 11.0.26.52348), X64 RyuJIT x86-64-v4

EnvironmentVariables=DOTNET_GCDynamicAdaptationMode=0,DOTNET_TC_OnStackReplacement=1,COMPlus_TC_OnStackReplacement=1,DOTNET_TieredCompilation=1,DOTNET_TieredPGO=1  Server=True  Arguments=/p:Platform=x64  
IterationCount=8  IterationTime=250ms  LaunchCount=1  
WarmupCount=3  

```
| Method   | Categories | RequestedBeams | Mean       | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |----------- |--------------- |-----------:|----------:|----------:|------:|--------:|----------:|------------:|
| **OldCbed**  | **CBED**       | **128**            |   **5.021 ms** | **0.2064 ms** | **0.1080 ms** |  **1.00** |    **0.00** |         **-** |          **NA** |
| NewCbed  | CBED       | 128            |   4.854 ms | 0.0802 ms | 0.0356 ms |  0.97 |    0.02 |         - |          NA |
|          |            |                |            |           |           |       |         |           |             |
| **OldCbed**  | **CBED**       | **256**            | **107.478 ms** | **0.7132 ms** | **0.3730 ms** |  **1.00** |    **0.00** |         **-** |          **NA** |
| NewCbed  | CBED       | 256            | 106.400 ms | 0.8367 ms | 0.3715 ms |  0.99 |    0.00 |         - |          NA |
|          |            |                |            |           |           |       |         |           |             |
| **OldCbed**  | **CBED**       | **448**            | **175.856 ms** | **3.5353 ms** | **1.5697 ms** |  **1.00** |    **0.00** |         **-** |          **NA** |
| NewCbed  | CBED       | 448            | 175.193 ms | 4.2298 ms | 1.8780 ms |  1.00 |    0.01 |         - |          NA |
|          |            |                |            |           |           |       |         |           |             |
| **OldEigen** | **EVD**        | **128**            |   **4.896 ms** | **0.1627 ms** | **0.0851 ms** |  **1.00** |    **0.00** |         **-** |          **NA** |
| NewEigen | EVD        | 128            |   4.825 ms | 0.2472 ms | 0.1293 ms |  0.99 |    0.03 |         - |          NA |
|          |            |                |            |           |           |       |         |           |             |
| **OldEigen** | **EVD**        | **256**            | **105.738 ms** | **1.0130 ms** | **0.5298 ms** |  **1.00** |    **0.00** |         **-** |          **NA** |
| NewEigen | EVD        | 256            | 106.580 ms | 0.8700 ms | 0.4550 ms |  1.01 |    0.01 |         - |          NA |
|          |            |                |            |           |           |       |         |           |             |
| **OldEigen** | **EVD**        | **448**            | **171.375 ms** | **1.5332 ms** | **0.6807 ms** |  **1.00** |    **0.00** |         **-** |          **NA** |
| NewEigen | EVD        | 448            | 171.665 ms | 3.3619 ms | 1.4927 ms |  1.00 |    0.01 |         - |          NA |

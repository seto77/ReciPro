# BetheBench timing report

- date: 2026-11-06 17:14:27, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=DISABLED, flavorEnv=managed-regression, crystallography=1.0.0+9c463498d6593bcb7a0c2f3c84ca30ddfc827d0b
- EigenEnabled: False, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: ebsd, crystals: Silicon (I); Au
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- EBSD: 0.212 s, alloc 17 MB, GC 0/0/0, in-run peak private 80 MB / gcHeap 24 MB (bloch=32, grid=8, dirs=128, kv=20, depths=5, threads=0, tds=True, smear=0.02) [ok]
  -    0.211 s  +   0.000 s  Eigen_Managed2
- peak working set (cumulative): 111 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- EBSD: 0.132 s, alloc 15 MB, GC 0/0/0, in-run peak private 80 MB / gcHeap 23 MB (bloch=32, grid=8, dirs=128, kv=20, depths=5, threads=0, tds=True, smear=0.02) [ok]
  -    0.132 s  +  -0.000 s  Eigen_Managed2
- peak working set (cumulative): 111 MB

total: 0.4 s

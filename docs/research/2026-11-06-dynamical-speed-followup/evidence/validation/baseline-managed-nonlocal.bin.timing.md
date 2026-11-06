# BetheBench timing report

- date: 2026-11-06 17:14:26, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=DISABLED, flavorEnv=managed-regression, crystallography=1.0.0+b575e701ee8bed6a60cc3fa6238867d7fd7d6a2f
- EigenEnabled: False, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: ebsd, crystals: Silicon (I); Au
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- EBSD: 0.220 s, alloc 17 MB, GC 0/0/0, in-run peak private 80 MB / gcHeap 24 MB (bloch=32, grid=8, dirs=128, kv=20, depths=5, threads=0, tds=True, smear=0.02) [ok]
  -    0.219 s  +   0.001 s  Eigen_Managed2
- peak working set (cumulative): 110 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- EBSD: 0.135 s, alloc 16 MB, GC 0/0/0, in-run peak private 80 MB / gcHeap 23 MB (bloch=32, grid=8, dirs=128, kv=20, depths=5, threads=0, tds=True, smear=0.02) [ok]
  -    0.118 s  +   0.017 s  Eigen_Managed2
- peak working set (cumulative): 110 MB

total: 0.4 s

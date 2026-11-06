# BetheBench timing report

- date: 2026-11-06 17:14:21, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=4084DE960898, flavorEnv=avx512, crystallography=1.0.0+b575e701ee8bed6a60cc3fa6238867d7fd7d6a2f
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: ebsd, crystals: Silicon (I); Au
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- EBSD: 0.464 s, alloc 59 MB, GC 2/2/2, in-run peak private 105 MB / gcHeap 40 MB (bloch=96, grid=12, dirs=288, kv=20, depths=9, threads=0, tds=False, smear=NaN) [ok]
  -    0.467 s  +  -0.003 s  Eigen_Eigen2
- peak working set (cumulative): 136 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- EBSD: 0.294 s, alloc 51 MB, GC 0/0/0, in-run peak private 128 MB / gcHeap 67 MB (bloch=96, grid=12, dirs=288, kv=20, depths=9, threads=0, tds=False, smear=NaN) [ok]
  -    0.274 s  +   0.021 s  Eigen_Eigen2
- peak working set (cumulative): 157 MB

total: 0.9 s

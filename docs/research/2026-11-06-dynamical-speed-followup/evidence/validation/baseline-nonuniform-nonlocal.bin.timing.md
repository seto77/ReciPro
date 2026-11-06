# BetheBench timing report

- date: 2026-11-06 17:14:23, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=4084DE960898, flavorEnv=avx512, crystallography=1.0.0+b575e701ee8bed6a60cc3fa6238867d7fd7d6a2f
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: ebsd, crystals: Silicon (I); Au
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- EBSD: 0.695 s, alloc 81 MB, GC 3/3/3, in-run peak private 118 MB / gcHeap 40 MB (bloch=96, grid=12, dirs=288, kv=20, depths=9, threads=0, tds=True, smear=0.02) [ok]
  -    0.702 s  +  -0.007 s  Eigen_Eigen2
- peak working set (cumulative): 147 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- EBSD: 0.303 s, alloc 67 MB, GC 0/0/0, in-run peak private 146 MB / gcHeap 89 MB (bloch=96, grid=12, dirs=288, kv=20, depths=9, threads=0, tds=True, smear=0.02) [ok]
  -    0.275 s  +   0.028 s  Eigen_Eigen2
- peak working set (cumulative): 173 MB

total: 1.1 s

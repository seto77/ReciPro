# BetheBench timing report

- date: 2026-11-06 17:14:22, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=78B2B691DE80, flavorEnv=avx512, crystallography=1.0.0+9c463498d6593bcb7a0c2f3c84ca30ddfc827d0b
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: ebsd, crystals: Silicon (I); Au
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- EBSD: 0.493 s, alloc 60 MB, GC 2/2/2, in-run peak private 103 MB / gcHeap 34 MB (bloch=96, grid=12, dirs=288, kv=20, depths=9, threads=0, tds=False, smear=NaN) [ok]
  -    0.498 s  +  -0.005 s  Eigen_Eigen2
- peak working set (cumulative): 134 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- EBSD: 0.308 s, alloc 53 MB, GC 0/0/0, in-run peak private 124 MB / gcHeap 68 MB (bloch=96, grid=12, dirs=288, kv=20, depths=9, threads=0, tds=False, smear=NaN) [ok]
  -    0.296 s  +   0.012 s  Eigen_Eigen2
- peak working set (cumulative): 153 MB

total: 0.9 s

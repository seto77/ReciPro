# BetheBench timing report

- date: 2026-11-06 17:12:57, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=4084DE960898, flavorEnv=avx512, crystallography=1.0.0+b575e701ee8bed6a60cc3fa6238867d7fd7d6a2f
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: stem, crystals: Silicon (I)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- STEM: 41.314 s, alloc 828 MB, GC 6/3/3, in-run peak private 590 MB / gcHeap 524 MB (bloch=448, beams=441, div=24, img=32px) [ok]
  -    1.373 s  +  38.269 s  EigenSolve
  -   39.642 s  +   0.017 s  ElasticQ
  -   39.659 s  +   1.655 s  InelasticQ
- peak working set (cumulative): 598 MB

total: 41.4 s

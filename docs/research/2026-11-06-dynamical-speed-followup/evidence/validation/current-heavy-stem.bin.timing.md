# BetheBench timing report

- date: 2026-11-06 17:13:39, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=78B2B691DE80, flavorEnv=avx512, crystallography=1.0.0+9c463498d6593bcb7a0c2f3c84ca30ddfc827d0b
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: stem, crystals: Silicon (I)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- STEM: 41.643 s, alloc 823 MB, GC 6/3/3, in-run peak private 597 MB / gcHeap 493 MB (bloch=448, beams=441, div=24, img=32px) [ok]
  -    1.374 s  +  38.623 s  EigenSolve
  -   39.997 s  +   0.018 s  ElasticQ
  -   40.015 s  +   1.628 s  InelasticQ
- peak working set (cumulative): 603 MB

total: 41.7 s

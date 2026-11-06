# BetheBench timing report

- date: 2026-11-06 14:10:27, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=689FFD75D4D0, crystallography=1.0.0
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: stem, crystals: Silicon (I)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- STEM: 49.019 s, alloc 821 MB, GC 5/3/3, in-run peak private 597 MB / gcHeap 495 MB (bloch=448, beams=441, div=24, img=32px) [ok]
  -    1.338 s  +  45.621 s  EigenSolve
  -   46.959 s  +   0.020 s  ElasticQ
  -   46.978 s  +   2.040 s  InelasticQ
- peak working set (cumulative): 602 MB

total: 49.1 s

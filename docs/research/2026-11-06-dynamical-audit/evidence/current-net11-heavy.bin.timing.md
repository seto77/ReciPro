# BetheBench timing report

- date: 2026-11-06 14:11:56, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=4084DE960898, crystallography=1.0.0+b2bbacaaf972ca2922387bdec16b20ed20e90f6d
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: stem, crystals: Silicon (I)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- STEM: 45.240 s, alloc 823 MB, GC 6/3/3, in-run peak private 589 MB / gcHeap 521 MB (bloch=448, beams=441, div=24, img=32px) [ok]
  -    1.340 s  +  42.182 s  EigenSolve
  -   43.522 s  +   0.018 s  ElasticQ
  -   43.540 s  +   1.700 s  InelasticQ
- peak working set (cumulative): 598 MB

total: 45.3 s

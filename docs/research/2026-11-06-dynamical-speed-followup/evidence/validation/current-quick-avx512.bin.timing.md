# BetheBench timing report

- date: 2026-11-06 17:12:47, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=78B2B691DE80, flavorEnv=avx512, crystallography=1.0.0+9c463498d6593bcb7a0c2f3c84ca30ddfc827d0b
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: cbed,potential,stem,ebsd,hrtem, crystals: Silicon (I); Au; SiO2 (qtz); C (Graphite); Fe3O4 (magnetite)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.786 s, alloc 27 MB, GC 1/1/1, in-run peak private 79 MB / gcHeap 18 MB (bloch=128, div=17, beams=125) [ok]
  -    0.751 s  +   0.002 s  Eigen_Eigen2
  -    0.753 s  +   0.033 s  Compiling disks
- POT: 0.008 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.636 s, alloc 77 MB, GC 0/0/0, in-run peak private 138 MB / gcHeap 88 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.151 s  +   0.142 s  EigenSolve
  -    0.293 s  +   0.005 s  ElasticQ
  -    0.298 s  +   0.339 s  InelasticQ
- EBSD: 0.613 s, alloc 245 MB, GC 1/0/0, in-run peak private 233 MB / gcHeap 188 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.621 s  +  -0.008 s  Eigen_Eigen2
- HRTEM: 0.008 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 273 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.573 s, alloc 27 MB, GC 0/0/0, in-run peak private 198 MB / gcHeap 38 MB (bloch=128, div=17, beams=117) [ok]
  -    0.516 s  +   0.052 s  Eigen_Eigen2
  -    0.568 s  +   0.005 s  Compiling disks
- POT: 0.004 s, alloc 1 MB (beams=117, 128px)
- STEM: 0.251 s, alloc 63 MB, GC 1/0/0, in-run peak private 151 MB / gcHeap 68 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.044 s  +   0.121 s  EigenSolve
  -    0.165 s  +   0.001 s  ElasticQ
  -    0.166 s  +   0.085 s  InelasticQ
- EBSD: 0.392 s, alloc 238 MB, GC 2/1/0, in-run peak private 199 MB / gcHeap 149 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.394 s  +  -0.002 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 273 MB

## SiO2 (qtz) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.612 s, alloc 26 MB, GC 0/0/0, in-run peak private 184 MB / gcHeap 41 MB (bloch=128, div=17, beams=127) [ok]
  -    0.587 s  +   0.012 s  Eigen_Eigen2
  -    0.599 s  +   0.014 s  Compiling disks
- POT: 0.005 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.207 s, alloc 40 MB, GC 0/0/0, in-run peak private 138 MB / gcHeap 66 MB (bloch=96, beams=91, div=12, img=32px) [ok]
  -    0.032 s  +   0.127 s  EigenSolve
  -    0.159 s  +   0.004 s  ElasticQ
  -    0.163 s  +   0.044 s  InelasticQ
- EBSD: 0.891 s, alloc 438 MB, GC 3/1/0, in-run peak private 289 MB / gcHeap 225 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.658 s  +   0.234 s  Eigen_Eigen2
- HRTEM: 0.003 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 331 MB

## C (Graphite) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.668 s, alloc 28 MB, GC 0/0/0, in-run peak private 268 MB / gcHeap 56 MB (bloch=128, div=17, beams=127) [ok]
  -    0.604 s  +   0.067 s  Eigen_Eigen2
  -    0.670 s  +  -0.003 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.246 s, alloc 71 MB, GC 0/0/0, in-run peak private 214 MB / gcHeap 91 MB (bloch=96, beams=85, div=12, img=32px) [ok]
  -    0.071 s  +   0.097 s  EigenSolve
  -    0.168 s  +   0.000 s  ElasticQ
  -    0.169 s  +   0.078 s  InelasticQ
- EBSD: 0.518 s, alloc 249 MB, GC 1/0/0, in-run peak private 231 MB / gcHeap 196 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.409 s  +   0.108 s  Eigen_Eigen2
- HRTEM: 0.000 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 331 MB

## Fe3O4 (magnetite) (Biso=0.005 nm² を 3 原子種に設定)

- CBED: 0.767 s, alloc 28 MB, GC 0/0/0, in-run peak private 188 MB / gcHeap 52 MB (bloch=128, div=17, beams=125) [ok]
  -    0.703 s  +   0.065 s  Eigen_Eigen2
  -    0.768 s  +  -0.001 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.200 s, alloc 46 MB, GC 1/0/0, in-run peak private 158 MB / gcHeap 55 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.038 s  +   0.115 s  EigenSolve
  -    0.153 s  +   0.000 s  ElasticQ
  -    0.154 s  +   0.046 s  InelasticQ
- EBSD: 0.862 s, alloc 260 MB, GC 2/1/0, in-run peak private 228 MB / gcHeap 185 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.728 s  +   0.133 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 331 MB

total: 9.7 s

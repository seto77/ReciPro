# BetheBench timing report

- date: 2026-11-06 17:12:37, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=4084DE960898, flavorEnv=avx512, crystallography=1.0.0+b575e701ee8bed6a60cc3fa6238867d7fd7d6a2f
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: cbed,potential,stem,ebsd,hrtem, crystals: Silicon (I); Au; SiO2 (qtz); C (Graphite); Fe3O4 (magnetite)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.786 s, alloc 26 MB, GC 1/1/1, in-run peak private 80 MB / gcHeap 20 MB (bloch=128, div=17, beams=125) [ok]
  -    0.746 s  +   0.008 s  Eigen_Eigen2
  -    0.754 s  +   0.032 s  Compiling disks
- POT: 0.008 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.585 s, alloc 72 MB, GC 0/0/0, in-run peak private 127 MB / gcHeap 79 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.149 s  +   0.136 s  EigenSolve
  -    0.285 s  +   0.004 s  ElasticQ
  -    0.290 s  +   0.296 s  InelasticQ
- EBSD: 0.477 s, alloc 245 MB, GC 1/0/0, in-run peak private 233 MB / gcHeap 188 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.461 s  +   0.016 s  Eigen_Eigen2
- HRTEM: 0.008 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 272 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.552 s, alloc 27 MB, GC 0/0/0, in-run peak private 198 MB / gcHeap 44 MB (bloch=128, div=17, beams=117) [ok]
  -    0.497 s  +   0.049 s  Eigen_Eigen2
  -    0.545 s  +   0.007 s  Compiling disks
- POT: 0.003 s, alloc 1 MB (beams=117, 128px)
- STEM: 0.202 s, alloc 62 MB, GC 1/0/0, in-run peak private 150 MB / gcHeap 55 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.043 s  +   0.124 s  EigenSolve
  -    0.167 s  +   0.002 s  ElasticQ
  -    0.168 s  +   0.034 s  InelasticQ
- EBSD: 0.407 s, alloc 238 MB, GC 3/1/0, in-run peak private 202 MB / gcHeap 124 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.272 s  +   0.135 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 272 MB

## SiO2 (qtz) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.656 s, alloc 27 MB, GC 0/0/0, in-run peak private 199 MB / gcHeap 49 MB (bloch=128, div=17, beams=127) [ok]
  -    0.623 s  +   0.006 s  Eigen_Eigen2
  -    0.630 s  +   0.026 s  Compiling disks
- POT: 0.005 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.176 s, alloc 49 MB, GC 0/0/0, in-run peak private 174 MB / gcHeap 48 MB (bloch=96, beams=91, div=12, img=32px) [ok]
  -    0.030 s  +   0.124 s  EigenSolve
  -    0.154 s  +   0.004 s  ElasticQ
  -    0.157 s  +   0.019 s  InelasticQ
- EBSD: 0.873 s, alloc 438 MB, GC 3/1/0, in-run peak private 300 MB / gcHeap 194 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.878 s  +  -0.005 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 340 MB

## C (Graphite) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.667 s, alloc 28 MB, GC 0/0/0, in-run peak private 263 MB / gcHeap 49 MB (bloch=128, div=17, beams=127) [ok]
  -    0.660 s  +   0.009 s  Eigen_Eigen2
  -    0.669 s  +  -0.002 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.193 s, alloc 86 MB, GC 0/0/0, in-run peak private 209 MB / gcHeap 53 MB (bloch=96, beams=85, div=12, img=32px) [ok]
  -    0.069 s  +   0.096 s  EigenSolve
  -    0.165 s  +   0.000 s  ElasticQ
  -    0.165 s  +   0.029 s  InelasticQ
- EBSD: 0.500 s, alloc 249 MB, GC 3/1/0, in-run peak private 204 MB / gcHeap 119 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.395 s  +   0.104 s  Eigen_Eigen2
- HRTEM: 0.000 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 340 MB

## Fe3O4 (magnetite) (Biso=0.005 nm² を 3 原子種に設定)

- CBED: 0.736 s, alloc 28 MB, GC 0/0/0, in-run peak private 189 MB / gcHeap 53 MB (bloch=128, div=17, beams=125) [ok]
  -    0.728 s  +   0.006 s  Eigen_Eigen2
  -    0.734 s  +   0.002 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.169 s, alloc 58 MB, GC 0/0/0, in-run peak private 182 MB / gcHeap 58 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.038 s  +   0.113 s  EigenSolve
  -    0.151 s  +   0.000 s  ElasticQ
  -    0.151 s  +   0.018 s  InelasticQ
- EBSD: 0.856 s, alloc 260 MB, GC 2/1/0, in-run peak private 225 MB / gcHeap 178 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.746 s  +   0.110 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 340 MB

total: 9.4 s

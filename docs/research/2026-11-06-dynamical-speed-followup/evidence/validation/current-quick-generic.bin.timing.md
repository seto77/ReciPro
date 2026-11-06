# BetheBench timing report

- date: 2026-11-06 17:12:02, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.dll, nativeSha=CF1A1A96553A, flavorEnv=generic, crystallography=1.0.0+9c463498d6593bcb7a0c2f3c84ca30ddfc827d0b
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: cbed,potential,stem,ebsd,hrtem, crystals: Silicon (I); Au; SiO2 (qtz); C (Graphite); Fe3O4 (magnetite)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 1.282 s, alloc 28 MB, GC 1/1/1, in-run peak private 79 MB / gcHeap 21 MB (bloch=128, div=17, beams=125) [ok]
  -    1.138 s  +   0.117 s  Eigen_Eigen2
  -    1.255 s  +   0.027 s  Compiling disks
- POT: 0.008 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.649 s, alloc 78 MB, GC 0/0/0, in-run peak private 119 MB / gcHeap 69 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.154 s  +   0.243 s  EigenSolve
  -    0.397 s  +   0.006 s  ElasticQ
  -    0.403 s  +   0.246 s  InelasticQ
- EBSD: 0.594 s, alloc 245 MB, GC 1/0/0, in-run peak private 223 MB / gcHeap 179 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.364 s  +   0.231 s  Eigen_Eigen2
- HRTEM: 0.008 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 264 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.999 s, alloc 26 MB, GC 0/0/0, in-run peak private 208 MB / gcHeap 38 MB (bloch=128, div=17, beams=117) [ok]
  -    0.893 s  +   0.100 s  Eigen_Eigen2
  -    0.993 s  +   0.006 s  Compiling disks
- POT: 0.004 s, alloc 1 MB (beams=117, 128px)
- STEM: 0.291 s, alloc 70 MB, GC 0/0/0, in-run peak private 174 MB / gcHeap 58 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.048 s  +   0.217 s  EigenSolve
  -    0.265 s  +   0.001 s  ElasticQ
  -    0.266 s  +   0.024 s  InelasticQ
- EBSD: 0.500 s, alloc 238 MB, GC 3/1/0, in-run peak private 200 MB / gcHeap 146 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.263 s  +   0.236 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 264 MB

## SiO2 (qtz) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 1.197 s, alloc 26 MB, GC 0/0/0, in-run peak private 195 MB / gcHeap 41 MB (bloch=128, div=17, beams=127) [ok]
  -    1.057 s  +   0.127 s  Eigen_Eigen2
  -    1.183 s  +   0.014 s  Compiling disks
- POT: 0.004 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.286 s, alloc 44 MB, GC 0/0/0, in-run peak private 191 MB / gcHeap 70 MB (bloch=96, beams=91, div=12, img=32px) [ok]
  -    0.033 s  +   0.220 s  EigenSolve
  -    0.253 s  +   0.004 s  ElasticQ
  -    0.257 s  +   0.029 s  InelasticQ
- EBSD: 1.067 s, alloc 438 MB, GC 2/1/0, in-run peak private 286 MB / gcHeap 225 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.643 s  +   0.424 s  Eigen_Eigen2
- HRTEM: 0.002 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 327 MB

## C (Graphite) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 1.178 s, alloc 28 MB, GC 0/0/0, in-run peak private 258 MB / gcHeap 56 MB (bloch=128, div=17, beams=127) [ok]
  -    1.060 s  +   0.120 s  Eigen_Eigen2
  -    1.180 s  +  -0.001 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.288 s, alloc 81 MB, GC 0/0/0, in-run peak private 197 MB / gcHeap 110 MB (bloch=96, beams=85, div=12, img=32px) [ok]
  -    0.073 s  +   0.176 s  EigenSolve
  -    0.250 s  +   0.000 s  ElasticQ
  -    0.250 s  +   0.038 s  InelasticQ
- EBSD: 0.604 s, alloc 249 MB, GC 2/1/0, in-run peak private 226 MB / gcHeap 166 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.395 s  +   0.209 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 327 MB

## Fe3O4 (magnetite) (Biso=0.005 nm² を 3 原子種に設定)

- CBED: 1.259 s, alloc 28 MB, GC 0/0/0, in-run peak private 213 MB / gcHeap 52 MB (bloch=128, div=17, beams=125) [ok]
  -    1.137 s  +   0.121 s  Eigen_Eigen2
  -    1.257 s  +   0.002 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.287 s, alloc 53 MB, GC 0/0/0, in-run peak private 194 MB / gcHeap 78 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.041 s  +   0.218 s  EigenSolve
  -    0.259 s  +   0.000 s  ElasticQ
  -    0.259 s  +   0.028 s  InelasticQ
- EBSD: 0.989 s, alloc 260 MB, GC 1/0/0, in-run peak private 268 MB / gcHeap 229 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.738 s  +   0.250 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 327 MB

total: 13.1 s

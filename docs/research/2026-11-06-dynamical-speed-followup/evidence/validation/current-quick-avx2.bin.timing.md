# BetheBench timing report

- date: 2026-11-06 17:12:26, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx2.dll, nativeSha=4A3CB69F562C, flavorEnv=avx2, crystallography=1.0.0+9c463498d6593bcb7a0c2f3c84ca30ddfc827d0b
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: cbed,potential,stem,ebsd,hrtem, crystals: Silicon (I); Au; SiO2 (qtz); C (Graphite); Fe3O4 (magnetite)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.907 s, alloc 28 MB, GC 1/1/1, in-run peak private 80 MB / gcHeap 19 MB (bloch=128, div=17, beams=125) [ok]
  -    0.870 s  +   0.009 s  Eigen_Eigen2
  -    0.879 s  +   0.027 s  Compiling disks
- POT: 0.008 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.515 s, alloc 79 MB, GC 0/0/0, in-run peak private 141 MB / gcHeap 89 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.125 s  +   0.157 s  EigenSolve
  -    0.282 s  +   0.003 s  ElasticQ
  -    0.286 s  +   0.229 s  InelasticQ
- EBSD: 0.619 s, alloc 245 MB, GC 1/0/0, in-run peak private 236 MB / gcHeap 190 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.471 s  +   0.148 s  Eigen_Eigen2
- HRTEM: 0.008 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 275 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.692 s, alloc 26 MB, GC 0/0/0, in-run peak private 200 MB / gcHeap 37 MB (bloch=128, div=17, beams=117) [ok]
  -    0.619 s  +   0.065 s  Eigen_Eigen2
  -    0.684 s  +   0.008 s  Compiling disks
- POT: 0.003 s, alloc 1 MB (beams=117, 128px)
- STEM: 0.282 s, alloc 62 MB, GC 1/0/0, in-run peak private 149 MB / gcHeap 53 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.045 s  +   0.147 s  EigenSolve
  -    0.192 s  +   0.001 s  ElasticQ
  -    0.194 s  +   0.089 s  InelasticQ
- EBSD: 0.429 s, alloc 238 MB, GC 2/1/0, in-run peak private 199 MB / gcHeap 148 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.267 s  +   0.162 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 275 MB

## SiO2 (qtz) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.766 s, alloc 27 MB, GC 0/0/0, in-run peak private 192 MB / gcHeap 48 MB (bloch=128, div=17, beams=127) [ok]
  -    0.663 s  +   0.078 s  Eigen_Eigen2
  -    0.741 s  +   0.025 s  Compiling disks
- POT: 0.005 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.218 s, alloc 52 MB, GC 0/0/0, in-run peak private 165 MB / gcHeap 73 MB (bloch=96, beams=91, div=12, img=32px) [ok]
  -    0.031 s  +   0.149 s  EigenSolve
  -    0.180 s  +   0.004 s  ElasticQ
  -    0.184 s  +   0.034 s  InelasticQ
- EBSD: 0.939 s, alloc 438 MB, GC 3/1/0, in-run peak private 291 MB / gcHeap 183 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.662 s  +   0.277 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 331 MB

## C (Graphite) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.791 s, alloc 28 MB, GC 0/0/0, in-run peak private 257 MB / gcHeap 48 MB (bloch=128, div=17, beams=127) [ok]
  -    0.737 s  +   0.055 s  Eigen_Eigen2
  -    0.792 s  +  -0.001 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.283 s, alloc 69 MB, GC 0/0/0, in-run peak private 187 MB / gcHeap 97 MB (bloch=96, beams=85, div=12, img=32px) [ok]
  -    0.072 s  +   0.117 s  EigenSolve
  -    0.189 s  +   0.000 s  ElasticQ
  -    0.189 s  +   0.094 s  InelasticQ
- EBSD: 0.530 s, alloc 248 MB, GC 2/1/0, in-run peak private 226 MB / gcHeap 163 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.399 s  +   0.131 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 331 MB

## Fe3O4 (magnetite) (Biso=0.005 nm² を 3 原子種に設定)

- CBED: 0.951 s, alloc 27 MB, GC 0/0/0, in-run peak private 217 MB / gcHeap 60 MB (bloch=128, div=17, beams=125) [ok]
  -    0.843 s  +   0.109 s  Eigen_Eigen2
  -    0.952 s  +  -0.001 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.241 s, alloc 46 MB, GC 0/0/0, in-run peak private 195 MB / gcHeap 77 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.038 s  +   0.138 s  EigenSolve
  -    0.176 s  +   0.000 s  ElasticQ
  -    0.176 s  +   0.065 s  InelasticQ
- EBSD: 0.882 s, alloc 260 MB, GC 1/0/0, in-run peak private 266 MB / gcHeap 226 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.720 s  +   0.162 s  Eigen_Eigen2
- HRTEM: 0.002 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 331 MB

total: 10.6 s

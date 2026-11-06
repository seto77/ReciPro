# BetheBench timing report

- date: 2026-11-06 17:11:47, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.dll, nativeSha=5029CCAFCBA9, flavorEnv=generic, crystallography=1.0.0+b575e701ee8bed6a60cc3fa6238867d7fd7d6a2f
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: cbed,potential,stem,ebsd,hrtem, crystals: Silicon (I); Au; SiO2 (qtz); C (Graphite); Fe3O4 (magnetite)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 1.329 s, alloc 27 MB, GC 1/1/1, in-run peak private 80 MB / gcHeap 25 MB (bloch=128, div=17, beams=125) [ok]
  -    1.172 s  +   0.129 s  Eigen_Eigen2
  -    1.301 s  +   0.027 s  Compiling disks
- POT: 0.008 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.710 s, alloc 77 MB, GC 0/0/0, in-run peak private 143 MB / gcHeap 89 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.156 s  +   0.232 s  EigenSolve
  -    0.388 s  +   0.004 s  ElasticQ
  -    0.392 s  +   0.318 s  InelasticQ
- EBSD: 0.747 s, alloc 245 MB, GC 1/0/0, in-run peak private 223 MB / gcHeap 180 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.518 s  +   0.229 s  Eigen_Eigen2
- HRTEM: 0.007 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 264 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 1.013 s, alloc 26 MB, GC 0/0/0, in-run peak private 193 MB / gcHeap 38 MB (bloch=128, div=17, beams=117) [ok]
  -    0.905 s  +   0.104 s  Eigen_Eigen2
  -    1.009 s  +   0.004 s  Compiling disks
- POT: 0.004 s, alloc 1 MB (beams=117, 128px)
- STEM: 0.405 s, alloc 62 MB, GC 0/0/0, in-run peak private 164 MB / gcHeap 85 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.259 s  +   0.013 s  EigenSolve
  -    0.272 s  +   0.001 s  ElasticQ
  -    0.273 s  +   0.132 s  InelasticQ
- EBSD: 0.506 s, alloc 238 MB, GC 3/1/0, in-run peak private 200 MB / gcHeap 157 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.512 s  +  -0.006 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 264 MB

## SiO2 (qtz) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 1.219 s, alloc 27 MB, GC 0/0/0, in-run peak private 196 MB / gcHeap 50 MB (bloch=128, div=17, beams=127) [ok]
  -    1.067 s  +   0.131 s  Eigen_Eigen2
  -    1.198 s  +   0.021 s  Compiling disks
- POT: 0.004 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.291 s, alloc 48 MB, GC 0/0/0, in-run peak private 175 MB / gcHeap 73 MB (bloch=96, beams=91, div=12, img=32px) [ok]
  -    0.034 s  +   0.228 s  EigenSolve
  -    0.262 s  +   0.004 s  ElasticQ
  -    0.265 s  +   0.026 s  InelasticQ
- EBSD: 1.111 s, alloc 438 MB, GC 3/1/0, in-run peak private 305 MB / gcHeap 206 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.674 s  +   0.437 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 347 MB

## C (Graphite) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 1.199 s, alloc 28 MB, GC 0/0/0, in-run peak private 286 MB / gcHeap 49 MB (bloch=128, div=17, beams=127) [ok]
  -    1.077 s  +   0.124 s  Eigen_Eigen2
  -    1.201 s  +  -0.002 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.282 s, alloc 89 MB, GC 0/0/0, in-run peak private 233 MB / gcHeap 103 MB (bloch=96, beams=85, div=12, img=32px) [ok]
  -    0.072 s  +   0.177 s  EigenSolve
  -    0.249 s  +   0.000 s  ElasticQ
  -    0.249 s  +   0.032 s  InelasticQ
- EBSD: 0.623 s, alloc 248 MB, GC 1/0/0, in-run peak private 238 MB / gcHeap 198 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.400 s  +   0.223 s  Eigen_Eigen2
- HRTEM: 0.000 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 347 MB

## Fe3O4 (magnetite) (Biso=0.005 nm² を 3 原子種に設定)

- CBED: 1.287 s, alloc 28 MB, GC 0/0/0, in-run peak private 193 MB / gcHeap 53 MB (bloch=128, div=17, beams=125) [ok]
  -    1.167 s  +   0.122 s  Eigen_Eigen2
  -    1.288 s  +  -0.002 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.273 s, alloc 56 MB, GC 1/0/0, in-run peak private 161 MB / gcHeap 63 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.041 s  +   0.210 s  EigenSolve
  -    0.251 s  +   0.000 s  ElasticQ
  -    0.251 s  +   0.022 s  InelasticQ
- EBSD: 0.986 s, alloc 260 MB, GC 2/1/0, in-run peak private 230 MB / gcHeap 186 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.720 s  +   0.266 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 347 MB

total: 13.7 s

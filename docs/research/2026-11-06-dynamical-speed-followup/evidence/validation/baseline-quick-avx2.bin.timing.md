# BetheBench timing report

- date: 2026-11-06 17:12:15, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx2.dll, nativeSha=8F999DB81F02, flavorEnv=avx2, crystallography=1.0.0+b575e701ee8bed6a60cc3fa6238867d7fd7d6a2f
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: cbed,potential,stem,ebsd,hrtem, crystals: Silicon (I); Au; SiO2 (qtz); C (Graphite); Fe3O4 (magnetite)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.920 s, alloc 28 MB, GC 1/1/1, in-run peak private 80 MB / gcHeap 19 MB (bloch=128, div=17, beams=125) [ok]
  -    0.861 s  +   0.032 s  Eigen_Eigen2
  -    0.893 s  +   0.027 s  Compiling disks
- POT: 0.009 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.555 s, alloc 78 MB, GC 0/0/0, in-run peak private 141 MB / gcHeap 91 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.152 s  +   0.160 s  EigenSolve
  -    0.312 s  +   0.005 s  ElasticQ
  -    0.316 s  +   0.239 s  InelasticQ
- EBSD: 0.499 s, alloc 245 MB, GC 1/0/0, in-run peak private 235 MB / gcHeap 190 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.346 s  +   0.152 s  Eigen_Eigen2
- HRTEM: 0.008 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 275 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.659 s, alloc 26 MB, GC 0/0/0, in-run peak private 199 MB / gcHeap 37 MB (bloch=128, div=17, beams=117) [ok]
  -    0.593 s  +   0.063 s  Eigen_Eigen2
  -    0.656 s  +   0.002 s  Compiling disks
- POT: 0.003 s, alloc 1 MB (beams=117, 128px)
- STEM: 0.287 s, alloc 53 MB, GC 1/0/0, in-run peak private 149 MB / gcHeap 53 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.044 s  +   0.145 s  EigenSolve
  -    0.189 s  +   0.001 s  ElasticQ
  -    0.190 s  +   0.097 s  InelasticQ
- EBSD: 0.419 s, alloc 238 MB, GC 3/1/0, in-run peak private 199 MB / gcHeap 152 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.421 s  +  -0.002 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 275 MB

## SiO2 (qtz) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.801 s, alloc 27 MB, GC 0/0/0, in-run peak private 195 MB / gcHeap 42 MB (bloch=128, div=17, beams=127) [ok]
  -    0.770 s  +   0.003 s  Eigen_Eigen2
  -    0.773 s  +   0.028 s  Compiling disks
- POT: 0.007 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.219 s, alloc 44 MB, GC 0/0/0, in-run peak private 171 MB / gcHeap 66 MB (bloch=96, beams=91, div=12, img=32px) [ok]
  -    0.031 s  +   0.158 s  EigenSolve
  -    0.189 s  +   0.003 s  ElasticQ
  -    0.193 s  +   0.026 s  InelasticQ
- EBSD: 0.926 s, alloc 438 MB, GC 3/1/0, in-run peak private 305 MB / gcHeap 198 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.655 s  +   0.271 s  Eigen_Eigen2
- HRTEM: 0.003 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 347 MB

## C (Graphite) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.800 s, alloc 28 MB, GC 0/0/0, in-run peak private 281 MB / gcHeap 48 MB (bloch=128, div=17, beams=127) [ok]
  -    0.721 s  +   0.081 s  Eigen_Eigen2
  -    0.802 s  +  -0.002 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.292 s, alloc 70 MB, GC 0/0/0, in-run peak private 222 MB / gcHeap 95 MB (bloch=96, beams=85, div=12, img=32px) [ok]
  -    0.072 s  +   0.121 s  EigenSolve
  -    0.193 s  +   0.000 s  ElasticQ
  -    0.194 s  +   0.098 s  InelasticQ
- EBSD: 0.534 s, alloc 249 MB, GC 1/0/0, in-run peak private 231 MB / gcHeap 190 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.395 s  +   0.139 s  Eigen_Eigen2
- HRTEM: 0.000 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 347 MB

## Fe3O4 (magnetite) (Biso=0.005 nm² を 3 原子種に設定)

- CBED: 0.865 s, alloc 28 MB, GC 0/0/0, in-run peak private 205 MB / gcHeap 52 MB (bloch=128, div=17, beams=125) [ok]
  -    0.791 s  +   0.076 s  Eigen_Eigen2
  -    0.866 s  +  -0.001 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.237 s, alloc 45 MB, GC 0/0/0, in-run peak private 174 MB / gcHeap 73 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.039 s  +   0.135 s  EigenSolve
  -    0.174 s  +   0.000 s  ElasticQ
  -    0.174 s  +   0.063 s  InelasticQ
- EBSD: 0.886 s, alloc 260 MB, GC 2/1/0, in-run peak private 224 MB / gcHeap 181 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.722 s  +   0.164 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 347 MB

total: 10.4 s

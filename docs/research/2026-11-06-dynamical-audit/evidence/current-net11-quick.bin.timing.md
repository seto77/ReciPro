# BetheBench timing report

- date: 2026-11-06 14:11:45, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=4084DE960898, crystallography=1.0.0+b2bbacaaf972ca2922387bdec16b20ed20e90f6d
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: cbed,potential,stem,ebsd,hrtem, crystals: Silicon (I); Au; SiO2 (qtz); C (Graphite); Fe3O4 (magnetite)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.940 s, alloc 28 MB, GC 1/1/1, in-run peak private 84 MB / gcHeap 27 MB (bloch=128, div=17, beams=125) [ok]
  -    0.878 s  +   0.029 s  Eigen_Eigen2
  -    0.907 s  +   0.033 s  Compiling disks
- POT: 0.011 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.462 s, alloc 77 MB, GC 0/0/0, in-run peak private 136 MB / gcHeap 87 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.142 s  +   0.166 s  EigenSolve
  -    0.308 s  +   0.005 s  ElasticQ
  -    0.313 s  +   0.149 s  InelasticQ
- EBSD: 0.606 s, alloc 245 MB, GC 1/0/0, in-run peak private 235 MB / gcHeap 190 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.617 s  +  -0.010 s  Eigen_Eigen2
- HRTEM: 0.010 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 274 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.698 s, alloc 26 MB, GC 0/0/0, in-run peak private 200 MB / gcHeap 37 MB (bloch=128, div=17, beams=117) [ok]
  -    0.624 s  +   0.068 s  Eigen_Eigen2
  -    0.692 s  +   0.006 s  Compiling disks
- POT: 0.006 s, alloc 1 MB (beams=117, 128px)
- STEM: 0.324 s, alloc 62 MB, GC 1/0/0, in-run peak private 150 MB / gcHeap 58 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.172 s  +   0.014 s  EigenSolve
  -    0.186 s  +   0.002 s  ElasticQ
  -    0.188 s  +   0.136 s  InelasticQ
- EBSD: 0.455 s, alloc 238 MB, GC 3/1/0, in-run peak private 201 MB / gcHeap 115 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.307 s  +   0.148 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 274 MB

## SiO2 (qtz) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.737 s, alloc 27 MB, GC 0/0/0, in-run peak private 194 MB / gcHeap 49 MB (bloch=128, div=17, beams=127) [ok]
  -    0.622 s  +   0.069 s  Eigen_Eigen2
  -    0.691 s  +   0.045 s  Compiling disks
- POT: 0.008 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.266 s, alloc 42 MB, GC 0/0/0, in-run peak private 177 MB / gcHeap 62 MB (bloch=96, beams=91, div=12, img=32px) [ok]
  -    0.171 s  +   0.008 s  EigenSolve
  -    0.179 s  +   0.005 s  ElasticQ
  -    0.184 s  +   0.082 s  InelasticQ
- EBSD: 1.082 s, alloc 438 MB, GC 2/1/0, in-run peak private 292 MB / gcHeap 222 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.827 s  +   0.255 s  Eigen_Eigen2
- HRTEM: 0.002 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 332 MB

## C (Graphite) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.758 s, alloc 28 MB, GC 0/0/0, in-run peak private 265 MB / gcHeap 48 MB (bloch=128, div=17, beams=127) [ok]
  -    0.706 s  +   0.053 s  Eigen_Eigen2
  -    0.759 s  +  -0.000 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.258 s, alloc 82 MB, GC 0/0/0, in-run peak private 207 MB / gcHeap 84 MB (bloch=96, beams=85, div=12, img=32px) [ok]
  -    0.093 s  +   0.118 s  EigenSolve
  -    0.210 s  +   0.000 s  ElasticQ
  -    0.211 s  +   0.048 s  InelasticQ
- EBSD: 0.616 s, alloc 249 MB, GC 1/0/0, in-run peak private 233 MB / gcHeap 195 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.493 s  +   0.123 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 332 MB

## Fe3O4 (magnetite) (Biso=0.005 nm² を 3 原子種に設定)

- CBED: 0.835 s, alloc 28 MB, GC 0/0/0, in-run peak private 194 MB / gcHeap 60 MB (bloch=128, div=17, beams=125) [ok]
  -    0.771 s  +   0.063 s  Eigen_Eigen2
  -    0.834 s  +   0.001 s  Compiling disks
- POT: 0.001 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.189 s, alloc 56 MB, GC 1/0/0, in-run peak private 158 MB / gcHeap 68 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.042 s  +   0.124 s  EigenSolve
  -    0.166 s  +   0.000 s  ElasticQ
  -    0.166 s  +   0.022 s  InelasticQ
- EBSD: 1.022 s, alloc 260 MB, GC 2/1/0, in-run peak private 232 MB / gcHeap 188 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.880 s  +   0.141 s  Eigen_Eigen2
- HRTEM: 0.002 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 332 MB

total: 11.1 s

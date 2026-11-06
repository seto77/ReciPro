# BetheBench timing report

- date: 2026-11-06 14:10:15, machine: SETO-DESKTOP, cores: 2
- arch=X64, os=X64, fw=.NET 11.0.0-rc.3.26522.148, native=Crystallography.Native.avx512.dll, nativeSha=689FFD75D4D0, crystallography=1.0.0
- EigenEnabled: True, MklEnabled: False, BlasEnabled: False, LAProvider: ManagedLinearAlgebraProvider, MaxDoP: 2
- modes: cbed,potential,stem,ebsd,hrtem, crystals: Silicon (I); Au; SiO2 (qtz); C (Graphite); Fe3O4 (magnetite)
- AbsorptionElasticModel: TemariHybrid

## Silicon (I) (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.917 s, alloc 28 MB, GC 1/1/1, in-run peak private 81 MB / gcHeap 21 MB (bloch=128, div=17, beams=125) [ok]
  -    0.797 s  +   0.068 s  Eigen_Eigen2
  -    0.865 s  +   0.052 s  Compiling disks
- POT: 0.012 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.639 s, alloc 78 MB, GC 0/0/0, in-run peak private 121 MB / gcHeap 74 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.177 s  +   0.157 s  EigenSolve
  -    0.335 s  +   0.012 s  ElasticQ
  -    0.346 s  +   0.292 s  InelasticQ
- EBSD: 0.573 s, alloc 245 MB, GC 1/0/0, in-run peak private 237 MB / gcHeap 190 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.433 s  +   0.140 s  Eigen_Eigen2
- HRTEM: 0.007 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 275 MB

## Au (Biso=0.005 nm² を 1 原子種に設定)

- CBED: 0.638 s, alloc 26 MB, GC 0/0/0, in-run peak private 200 MB / gcHeap 38 MB (bloch=128, div=17, beams=117) [ok]
  -    0.565 s  +   0.063 s  Eigen_Eigen2
  -    0.628 s  +   0.010 s  Compiling disks
- POT: 0.003 s, alloc 1 MB (beams=117, 128px)
- STEM: 0.315 s, alloc 54 MB, GC 1/0/0, in-run peak private 151 MB / gcHeap 70 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.059 s  +   0.151 s  EigenSolve
  -    0.211 s  +   0.002 s  ElasticQ
  -    0.212 s  +   0.103 s  InelasticQ
- EBSD: 0.500 s, alloc 238 MB, GC 3/1/0, in-run peak private 201 MB / gcHeap 121 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.332 s  +   0.168 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 275 MB

## SiO2 (qtz) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.832 s, alloc 27 MB, GC 0/0/0, in-run peak private 184 MB / gcHeap 49 MB (bloch=128, div=17, beams=127) [ok]
  -    0.677 s  +   0.090 s  Eigen_Eigen2
  -    0.767 s  +   0.065 s  Compiling disks
- POT: 0.006 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.248 s, alloc 40 MB, GC 0/0/0, in-run peak private 182 MB / gcHeap 66 MB (bloch=96, beams=91, div=12, img=32px) [ok]
  -    0.164 s  +   0.007 s  EigenSolve
  -    0.171 s  +   0.004 s  ElasticQ
  -    0.175 s  +   0.073 s  InelasticQ
- EBSD: 1.177 s, alloc 438 MB, GC 2/1/0, in-run peak private 291 MB / gcHeap 223 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    1.090 s  +   0.086 s  Eigen_Eigen2
- HRTEM: 0.002 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 331 MB

## C (Graphite) (Biso=0.005 nm² を 2 原子種に設定)

- CBED: 0.854 s, alloc 28 MB, GC 0/0/0, in-run peak private 250 MB / gcHeap 48 MB (bloch=128, div=17, beams=127) [ok]
  -    0.843 s  +   0.098 s  Eigen_Eigen2
  -    0.940 s  +  -0.086 s  Compiling disks
- POT: 0.002 s, alloc 1 MB (beams=127, 128px)
- STEM: 0.376 s, alloc 69 MB, GC 0/0/0, in-run peak private 190 MB / gcHeap 97 MB (bloch=96, beams=85, div=12, img=32px) [ok]
  -    0.213 s  +   0.003 s  EigenSolve
  -    0.216 s  +   0.000 s  ElasticQ
  -    0.216 s  +   0.159 s  InelasticQ
- EBSD: 0.714 s, alloc 249 MB, GC 3/1/0, in-run peak private 197 MB / gcHeap 126 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.578 s  +   0.136 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 331 MB

## Fe3O4 (magnetite) (Biso=0.005 nm² を 3 原子種に設定)

- CBED: 0.908 s, alloc 27 MB, GC 0/0/0, in-run peak private 186 MB / gcHeap 51 MB (bloch=128, div=17, beams=125) [ok]
  -    0.837 s  +   0.074 s  Eigen_Eigen2
  -    0.910 s  +  -0.002 s  Compiling disks
- POT: 0.002 s, alloc 1 MB (beams=125, 128px)
- STEM: 0.282 s, alloc 45 MB, GC 0/0/0, in-run peak private 180 MB / gcHeap 80 MB (bloch=96, beams=89, div=12, img=32px) [ok]
  -    0.176 s  +   0.015 s  EigenSolve
  -    0.191 s  +   0.000 s  ElasticQ
  -    0.192 s  +   0.090 s  InelasticQ
- EBSD: 1.095 s, alloc 260 MB, GC 2/1/0, in-run peak private 212 MB / gcHeap 172 MB (bloch=64, grid=48, dirs=4608, kv=20, depths=4, threads=0, tds=False, smear=NaN) [ok]
  -    0.922 s  +   0.173 s  Eigen_Eigen2
- HRTEM: 0.001 s, alloc 1 MB (bloch=96, 64px, native=True)
- peak working set (cumulative): 331 MB

total: 12.1 s

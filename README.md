# ReciPro
* *ReciPro* makes various crystallographic calculations, visualizes a crystal structure, simulates a diffraction pattern and indexes those, plots stereographic projection, and so on.
https://user-images.githubusercontent.com/44538886/123412384-7e20a980-d5ec-11eb-86fd-921c37ce460c.mp4

## Install
* Access https://github.com/seto77/ReciPro/releases/latest, download *ReciProSetup.msi*, and execute it.
  * ReciPro runs on Windows OS with .Net 5.0. which can be installed from the following link:
    * https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-desktop-5.0.7-windows-x64-installer
## Manual
  * Japanese version : https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf
  * English version : https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf

## Main features
* Provides crystallographic calculations.
  * 530 (Hall symbol) space groups are available. 
    * General conditions (or extinction rules), Wyckoff positions, multiplicities of all space groups.  
    * Geometrical calculation of periodicity and/or angle between planes and/or axes.
    * Generation of equivalent atomic positions.

* Provides crystallographic calculations.

* Includes atomic properties.
  * Characteristic X-ray wavelength/energy.
  * Atomic scattering factor for X-ray, electron and neutron.
  
* Simulates diffraction patterns.
  * X-ray, electron, and neutron sources are available for incident beams.
  * Kinematic simulations for the all sources.
  * Dynamic simulations for electron diffraction based on the Bethe method.
    * Parallel electron diffraction (SAED)
    * Precession electron diffraction (PED)
    * Convergent beam electron diffraction (CBED)
  
* Draws crystal structure using OpenGL4
  * Atom, bonds, polyhedra, unit cell and lattice planes.
  
* Plots stereonet (Wulf-net).
  * Axes and planes of any indices.
  * Large and small circles.
  
* Identifies diffraction spots in the observed image.
  * Major file formats (dm3, dm4, tiff, …) are supported.
  * Automatically detects diffraction spots, and identifies (or indexes) the spots for the selected crystal(s).
  
* Manages crystal data base.
  * ~80 crystal data are initially bundled. 
  * Import/export CIF, AMC format files.
  * Possible to use COD and AMCSD database through CSManager (https://github.com/seto77/CSManager/releases/latest). 

## Screenshots
<img src="img/Main.png" height="320px">  <img src="img/DiffractionSimulator1.png" height="320px">　<img src="img/DiffractionSimulator2.png" height="320px">　<img src="img/StructureViewer1.png" height="320px">　<img src="img/StructureViewer2.png" height="320px"> <img src="img/ScatteringFactors.png" height="320px"> <img src="img/Stereonet.png" height="320px"> <img src="img/recipro_demo.mp4" height="320px">

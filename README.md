# ReciPro
* *ReciPro* makes various crystallographic calculations, simulation of/indexing diffraction pattern, and so on.
  * Runs on Windows with .Net Framework 4.8.

## Install
* Access https://github.com/seto77/ReciPro/releases/latest, download *ReciProSetup.msi*, and execute it.

## Main features
* Provides crystallographic calculations.
  * 530 (Hall symbol) space groups.
  * General conditions (or extinction rules), Wyckoff positions, multiplicities.  
  * Geometrical calculation of periodicity and/or angle between planes and/or axes.
  * Generation of equivalent atomic positions.

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
  * Support several file format (dm3, dm4, tiff, …).
  * Detect and fit diffraction spots automatically.
  * Identify (or index) the diffraction spots for the selected crystal(s).
  
* Manages crystal data base.
  * ~80 crystal data are initially bundled. 
  * Import/export CIF, AMC format files.
  * Possible to use COD and AMCSD database through CSManager (https://github.com/seto77/CSManager/releases/latest). 

## Screenshots
<img src="Screenshots/Main.png" height="320px">  <img src="Screenshots/DiffractionSimulator1.png" height="320px">　<img src="Screenshots/DiffractionSimulator2.png" height="320px">　<img src="Screenshots/StructureViewer1.png" height="320px">　<img src="Screenshots/StructureViewer2.png" height="320px"> <img src="Screenshots/ScatteringFactors.png" height="320px"> <img src="Screenshots/Stereonet.png" height="320px">

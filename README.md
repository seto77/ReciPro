# The software ReciPro

ReciPro makes various crystallographic calculations, simulation of/indexing diffraction pattern, and so on.

## Main features of ReciPro

* ReciPro provides many crystallographic calculations for 530 (Hall symbol) space groups
  * General conditions (or extinction rules), Wyckoff positions, multiplicities.  
  * Geometrical calculation of periodicity and/or angle between planes and/or axes 
  * Generation of equivalent atomic positions

* ReciPro includes many atomic properties
  * Characteristic X-ray wavelength/energy
  * Atomic scattering factor for X-ray, electron and neutron
* ReciPro can simulate diffraction patterns
  * X-ray, electron, and neutron sources are available for incident beams.
  * Kinematic simulations for the all sources
  * Dynamic simulations for electron based on the Bethe method as follows
  * Parallel electron diffraction (SAED)
  * Precession electron diffraction
  * Convergent beam electron diffraction
* ReciPro draws crystal structure using OpenGL4
  * Atomic positions
  * Bonds and Polyhedra
  * Unit cell and lattice planes
* ReciPro plots stereonet (Wulf-net)
  * Axes and planes of any indices
  * Large and small circles
* ReciPro identifies diffraction spots in the observed image
  * Support many file format (dm3, dm4, tiff, …)
  * Detect and fit diffraction spots automatically
  * Identify (or index) the diffraction spots for the selected crystal(s).   
* To setup a crystal structure, you can
  * Import from CIF, AMC format files
  * Import from COD and AMCSD database through CSManager

## Screenshots
<img src="Screenshots/Main.png?raw=true" height="320px">  <img src="Screenshots/DiffractionSimulator1.png?raw=false" height="320px">　<img src="Screenshots/DiffractionSimulator2.png" height="320px">　<img src="Screenshots/StructureViewer1.png" height="320px">　<img src="Screenshots/StructureViewer2.png" height="320px">　<img src="Screenshots/ScatteringFactors.png" height="320px">

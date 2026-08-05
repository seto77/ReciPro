# Fonctions intégrées

Référence complète des classes et fonctions disponibles dans les macros ReciPro.

---

## Classe File

| Fonction | Description |
|----------|-------------|
| `File.GetDirectoryPath(filename)` | Afficher le dialogue de sélection de dossier, renvoyer le chemin choisi ; avec `filename`, renvoie plutôt le dossier qui le contient |
| `File.GetFileName()` | Afficher le dialogue de sélection de fichier, renvoyer le chemin choisi |
| `File.GetFileNames()` | Afficher le dialogue de sélection de fichiers multiples, renvoyer la liste des chemins |
| `File.ReadCrystalList(filename)` | Charger un fichier de liste de cristaux (*.xml) ; sans `filename`, une boîte de dialogue s'ouvre |
| `File.ReadCrystal(filename)` | Charger un fichier de cristal CIF/AMC ; sans `filename`, une boîte de dialogue s'ouvre |
| `File.ExportAsCIF(filename)` | Exporter le cristal actuel au format CIF ; sans `filename`, une boîte de dialogue s'ouvre |
| `File.SaveText(textData, filename)` | Enregistrer des données texte dans un fichier ; écrit `textData` en UTF-8 ; sans `filename`, une boîte de dialogue d'enregistrement s'ouvre |

---

## Classe Crystal

| Propriété | Type | Description |
|----------|------|-------------|
| `Crystal.Name` | string | Nom du cristal |
| `Crystal.ChemicalFormula` | string | Formule chimique |
| `Crystal.Density` | double | Densité (g/cm³) |

---

## Classe CrystalList

| Fonction / Propriété | Description |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | Obtenir/définir l'index du cristal sélectionné |
| `CrystalList.Count` | Nombre de cristaux présents dans la liste |
| `CrystalList.Add()` | Ajouter le cristal actuel à la liste |
| `CrystalList.Replace()` | Remplacer le cristal sélectionné |
| `CrystalList.Delete()` | Supprimer le cristal sélectionné |
| `CrystalList.ClearAll()` | Effacer tous les cristaux |
| `CrystalList.MoveUp()` | Déplacer le cristal sélectionné vers le haut |
| `CrystalList.MoveDown()` | Déplacer le cristal sélectionné vers le bas |

---

## Classe Dir

| Fonction | Description |
|----------|-------------|
| `Dir.Euler(phi, theta, psi)` | Définir l'orientation par les angles d'Euler (radians) |
| `Dir.EulerInDegree(phi, theta, psi)` | Définir l'orientation par les angles d'Euler (degrés) |
| `Dir.EulerInDeg(phi, theta, psi)` | Alias pour `EulerInDegree` |
| `Dir.Rotate(ax, ay, az, angle)` | Tourner autour d'un axe arbitraire (radians) |
| `Dir.RotateInDeg(ax, ay, az, angle)` | Tourner autour d'un axe arbitraire (degrés) |
| `Dir.RotateAroundAxis(u, v, w, angle)` | Tourner autour de l'axe de zone [uvw] (radians) |
| `Dir.RotateAroundAxisInDeg(u, v, w, angle)` | Tourner autour de l'axe de zone [uvw] (degrés) |
| `Dir.RotateAroundPlane(h, k, l, angle)` | Tourner autour de la normale au plan (hkl) (radians) |
| `Dir.RotateAroundPlaneInDeg(h, k, l, angle)` | Tourner autour de la normale au plan (hkl) (degrés) |
| `Dir.ProjectAlongPlane(h, k, l)` | Placer la normale au plan perpendiculaire à l'écran |
| `Dir.ProjectAlongAxis(u, v, w)` | Placer l'axe de zone perpendiculaire à l'écran |
| `Dir.GetEuler()` | Obtenir l'orientation actuelle en angles d'Euler Z-X-Z `[phi, theta, psi]` (radians) |
| `Dir.GetEulerInDeg()` | Obtenir l'orientation actuelle en angles d'Euler Z-X-Z `[phi, theta, psi]` (degrés) |
| `Dir.GetRotationMatrix()` | Obtenir la matrice de rotation actuelle sous forme de tableau à neuf éléments `[R11, R12, R13, R21, R22, R23, R31, R32, R33]` — la même convention que `SpotID.CandidateList()` |
| `Dir.SetRotationMatrix(r11, r12, r13, r21, r22, r23, r31, r32, r33)` | Définir l'orientation à partir de neuf éléments de matrice de rotation (validés et réorthonormalisés avant application) |

Les angles d'Euler ne sont pas uniques aux positions de blocage de cardan (θ = 0 ou 180°) : `GetEuler()` après `Euler()` reproduit la même attitude, mais pas nécessairement les mêmes nombres. Pour enregistrer et restaurer exactement l'orientation, utilisez `Dir.GetRotationMatrix()` / `Dir.SetRotationMatrix()`. La convention complète est décrite dans [Géométrie de rotation](../4-rotation-geometry.md).

---

## Classe DifSim

### Contrôle de la fenêtre

`DifSim.Open()` / `DifSim.Close()`

### Source d'ondes

`DifSim.Source_Xray()` / `DifSim.Source_Electron()` / `DifSim.Source_Neutron()`

### Propriétés

| Propriété | Type | Description |
|----------|------|-------------|
| `Energy` | double | Énergie (keV) |
| `Wavelength` | double | Longueur d'onde (Å) |
| `Thickness` | double | Épaisseur de l'échantillon (nm) |
| `NumberOfDiffractedWaves` | int | Nombre d'ondes de Bloch |
| `CameraLength2` | double | Longueur de caméra (mm) |
| `SkipRendering` | bool | Ignorer le rendu pour le traitement par lots |

### Mode de faisceau

`Beam_Parallel()` / `Beam_PrecessionXray()` / `Beam_PrecessionElectron()` / `Beam_Convergence()`

### Mode de calcul

`Calc_Excitation()` / `Calc_Kinematical()` / `Calc_Dynamical()`

### Paramètres d'image

| Propriété / Fonction | Description |
|---------------------|-------------|
| `ImageResolutionInMM` | Résolution (mm/pixel) |
| `ImageResolutionInNMinv` | Résolution (nm⁻¹/pixel) |
| `ImageWidth` / `ImageHeight` | Taille de l'image (pixels) |
| `ImageSize(w, h)` | Définir la taille de l'image |

### Détecteur

| Propriété | Description |
|----------|-------------|
| `Tau` / `TauInDeg` | Angle d'inclinaison du détecteur τ (rad / deg) |
| `Phi` / `PhiInDeg` | Axe de rotation du détecteur φ (rad / deg) |
| `Foot(x, y)` | Position du foot en pixels |

### Sortie

| Fonction | Description |
|----------|-------------|
| `SaveAsPng(filename)` | Enregistrer le diagramme actuel au format PNG ; sans `filename`, une boîte de dialogue s'ouvre |
| `SpotInfo()` | Obtenir les données des taches sous forme de chaîne CSV |

---

## Classe SpotID

Pilote [Spot ID v2](../11-spot-id-v2.md) depuis une macro : charger une image ou une liste de taches, détecter les taches, chercher les orientations et récupérer les candidats, sans toucher à la fenêtre. `FindSpots()` et `Identify()` ne rendent la main qu'une fois le travail terminé et peuvent donc s'enchaîner directement.

### Contrôle de la fenêtre

`SpotID.Open()` / `SpotID.Close()`

### Source d'onde

`SpotID.Source_Xray()` / `SpotID.Source_Electron()` / `SpotID.Source_Neutron()`

### Déroulement

| Fonction | Description |
|----------|-------------|
| `SpotID.LoadFile(filename)` | Charger un fichier comme le fait **File > Load** : un `.csv` est lu comme liste de taches (une image doit avoir été chargée au préalable), toute autre extension comme image de cliché de diffraction (dm3, dm4, mrc, ipa, tif et autres formats pris en charge). Sans `filename`, une boîte de dialogue s'ouvre |
| `SpotID.FindSpots()` | Détecter les taches de l'image chargée et les ajuster, comme le fait le bouton **Find spots** |
| `SpotID.Identify()` | Chercher les orientations qui expliquent les taches détectées, comme le fait le bouton **Identify spots**, et renvoyer le nombre de candidats. Les cristaux testés sont ceux sélectionnés dans la liste de cristaux de la fenêtre principale |
| `SpotID.CandidateList()` | Renvoyer la liste des orientations candidates sous forme de texte CSV |
| `SpotID.SpotList()` | Renvoyer les taches observées sous forme de texte CSV, avec les mêmes colonnes que **File > Save**. Associé à `File.SaveText()`, il produit un fichier que `LoadFile()` sait relire |

`CandidateList()` donne, pour chaque candidat : le nom du cristal, les angles d'Euler Z-X-Z (degrés), les neuf éléments R11–R33 de la matrice de rotation (repère du cristal vers le repère du laboratoire, appliquée à des vecteurs colonnes), le résidu quadratique moyen (nm⁻²) et l'affectation des taches observées à des indices *hkl*. Les candidats sont classés par nombre de taches affectées (décroissant), puis par résidu (croissant). Les nombres sont écrits en culture invariante : le séparateur décimal est toujours un point.

### Propriétés

| Propriété | Type | Description |
|-----------|------|-------------|
| `Energy` | double | Énergie du faisceau (keV pour les rayons X et les électrons, meV pour les neutrons) |
| `CameraLength` | double | Longueur de caméra (mm) |
| `PixelSizeInMM` | double | Taille de pixel (mm) ; la lire ou l'écrire bascule aussi l'unité de taille de pixel en mm |
| `PixelSizeInNMinv` | double | Taille de pixel (nm⁻¹) ; la lire ou l'écrire bascule aussi l'unité en nm⁻¹ |
| `MaxNumberOfSpots` | int | Nombre maximal de taches que `FindSpots()` peut détecter |
| `NearestNeighbor` | int | Écart minimal autorisé entre taches détectées (pixels) |
| `FittingRange` | double | Rayon de la région autour de chaque tache servant à l'ajustement du pic (pixels) |
| `AcceptableError` | double | Tolérance sur l'écart relatif de distance *d* lors de l'appariement des taches aux réflexions (%) |
| `IgnoreProhibitedReflections` | bool | Ignorer les réflexions cinématiquement interdites, qui peuvent tout de même apparaître par diffraction multiple |
| `MultiGrain` | bool | Chercher plusieurs grains ; `False` signifie un grain unique |
| `MaxNumberOfGrains` | int | Nombre maximal d'orientations de grains recherchées lorsque `MultiGrain` vaut `True` |
| `NumberOfDetectedSpots` | int | Nombre de taches détectées (lecture seule) |
| `NumberOfCandidates` | int | Nombre de candidats trouvés par le dernier `Identify()` (lecture seule) |

---

## Classes HRTEM / STEM / Potential

Ces trois classes de simulation d'image partagent de nombreux membres. Pour éviter les répétitions, les tableaux ci-dessous utilisent des espaces réservés :

- **`#`** : commun à **HRTEM**, **STEM** et **Potential**. Remplacez `#` par `HRTEM`, `STEM` ou `Potential` (par ex. `STEM.Simulate()`, `Potential.AccVol`).
- **`$`** : commun à **HRTEM** et **STEM** uniquement. Remplacez `$` par `HRTEM` ou `STEM`.
- Les membres écrits avec un nom de classe explicite (`STEM.…` / `HRTEM.…`) appartiennent uniquement à cette classe. La classe **Potential** n'ajoute aucun membre propre ; elle utilise uniquement les membres `#`.

### Contrôle de la fenêtre

| Fonction | Description |
|----------|-------------|
| `#.Open()` | Ouvrir la fenêtre du Simulateur HRTEM/STEM |
| `#.Close()` | Fermer la fenêtre du Simulateur HRTEM/STEM |
| `#.Simulate()` | Lancer la simulation avec les paramètres actuels |

### Microscope / optique

| Propriété / Fonction | Description |
|---------------------|-------------|
| `#.AccVol` | Tension d'accélération (kV) |
| `$.Thickness` | Épaisseur de l'échantillon (nm) |
| `$.Defocus` | Défocalisation (nm) |
| `$.Cs` | Aberration sphérique Cs (mm) |
| `$.Cc` | Aberration chromatique Cc (mm) |
| `$.DeltaV` | Dispersion en énergie ΔV, FWHM (eV) |
| `$.Scherzer` | Défocalisation de Scherzer (nm, lecture seule) |
| `STEM.ConvergenceAngle` | Demi-angle de convergence (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | Demi-angle interne/externe du détecteur annulaire (mrad) |
| `STEM.EffectiveSourceSize` | Taille effective de la source, FWHM (pm) |
| `HRTEM.Beta` | Demi-angle d'illumination β (radians) |
| `HRTEM.ApertureSemiangle` | Demi-angle du diaphragme objectif (radians) |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | Décalage du diaphragme objectif (radians) |
| `HRTEM.OpenAperture` | Diaphragme objectif ouvert (true/false) |

### Propriétés de simulation

| Propriété / Fonction | Description |
|---------------------|-------------|
| `#.NumberOfDiffractedWaves` | Nombre maximal d'ondes diffractées (de Bloch) |
| `#.ImageWidth` / `#.ImageHeight` | Taille de l'image (pixels) |
| `#.ImageSize(width, height)` | Définir la taille de l'image (pixels) |
| `#.ImageResolution` | Résolution de l'image (nm/pixel) |
| `STEM.AngularResolution` | Résolution angulaire du faisceau convergent (mrad) |
| `STEM.SliceThickness` | Épaisseur de tranche pour le calcul TDS (nm) |
| `HRTEM.Mode_LinearImage()` | Utiliser le modèle d'image linéaire (quasi-cohérent) |
| `HRTEM.Mode_TCC()` | Utiliser le modèle TCC (transmission cross coefficient) |

### Mode image unique / série

| Propriété / Fonction | Description |
|---------------------|-------------|
| `$.SingleImageMode()` | Passer en mode image unique |
| `$.SerialImageMode(withThickness, withDefocus)` | Passer en mode image en série |
| `$.SerialImageThicknessStart` / `Step` / `Num` | Épaisseur en série : début (nm) / pas (nm) / nombre |
| `$.SerialImageDefocusStart` / `Step` / `Num` | Défocalisation en série : début (nm) / pas (nm) / nombre |

### Propriétés d'image

| Propriété / Fonction | Description |
|---------------------|-------------|
| `#.UnitCellVisible` | Afficher la maille élémentaire (true/false) |
| `#.LabelVisible` | Afficher l'étiquette de l'image (true/false) |
| `#.LabelSize` | Taille de police de l'étiquette |
| `#.ScaleBarVisible` | Afficher la barre d'échelle (true/false) |
| `#.ScaleBarLength` | Longueur de la barre d'échelle (nm) |
| `#.GaussianBlurEnabled` | Appliquer un flou gaussien (true/false) |
| `#.GaussianBlurFWHM` | FWHM du flou gaussien (pm) |
| `STEM.DisplayBoth()` | Afficher à la fois la composante élastique et la composante TDS |
| `STEM.DisplayElastic()` | Afficher uniquement la composante élastique |
| `STEM.DisplayTDS()` | Afficher uniquement la composante TDS (inélastique) |

### Enregistrer l'image

| Propriété / Fonction | Description |
|---------------------|-------------|
| `#.SaveImageAsPng(filename)` | Enregistrer au format PNG (dialogue si filename omis) |
| `#.SaveImageAsTif(filename)` | Enregistrer au format TIFF (dialogue si filename omis) |
| `#.SaveImageAsEmf(filename)` | Enregistrer au format métafichier EMF (dialogue si filename omis) |
| `#.SaveIndividually` | En mode série, enregistrer chaque image séparément (true/false) |
| `#.OverprintSymbols` | Surimprimer maille élémentaire / étiquettes / barre d'échelle sur les images enregistrées (true/false) |

---

## Fonctions globales

| Fonction | Description |
|----------|-------------|
| `Sleep(ms)` | Attendre le nombre de millisecondes spécifié |

---

## Voir aussi

- [20. Macro](index.md)
- [20.2. Exemples](2-examples.md)

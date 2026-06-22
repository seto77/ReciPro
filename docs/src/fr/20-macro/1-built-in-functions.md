# Fonctions intégrées

Référence complète des classes et fonctions disponibles dans les macros ReciPro.

---

## Classe File

| Fonction | Description |
|----------|-------------|
| `File.GetDirectoryPath()` | Afficher le dialogue de sélection de dossier, renvoyer le chemin choisi |
| `File.GetFileName()` | Afficher le dialogue de sélection de fichier, renvoyer le chemin choisi |
| `File.GetFileNames()` | Afficher le dialogue de sélection de fichiers multiples, renvoyer la liste des chemins |
| `File.ReadCrystalList()` | Charger un fichier de liste de cristaux (*.xml) |
| `File.ReadCrystal()` | Charger un fichier de cristal CIF/AMC |
| `File.ExportAsCIF()` | Exporter le cristal actuel au format CIF |
| `File.SaveText()` | Enregistrer des données texte dans un fichier |

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
| `CrystalList.Add()` | Ajouter le cristal actuel à la liste |
| `CrystalList.Replace()` | Remplacer le cristal sélectionné |
| `CrystalList.Delete()` | Supprimer le cristal sélectionné |
| `CrystalList.ClearAll()` | Effacer tous les cristaux |
| `CrystalList.MoveUp()` | Déplacer le cristal sélectionné vers le haut |
| `CrystalList.MoveDown()` | Déplacer le cristal sélectionné vers le bas |

---

## Classe Direction

| Fonction | Description |
|----------|-------------|
| `Direction.Euler(phi, theta, psi)` | Définir l'orientation par les angles d'Euler (radians) |
| `Direction.EulerInDegree(phi, theta, psi)` | Définir l'orientation par les angles d'Euler (degrés) |
| `Direction.EulerInDeg(phi, theta, psi)` | Alias pour `EulerInDegree` |
| `Direction.Rotate(ax, ay, az, angle)` | Tourner autour d'un axe arbitraire (radians) |
| `Direction.RotateInDeg(ax, ay, az, angle)` | Tourner autour d'un axe arbitraire (degrés) |
| `Direction.RotateAroundAxis(u, v, w, angle)` | Tourner autour de l'axe de zone [uvw] (radians) |
| `Direction.RotateAroundAxisInDeg(u, v, w, angle)` | Tourner autour de l'axe de zone [uvw] (degrés) |
| `Direction.RotateAroundPlane(h, k, l, angle)` | Tourner autour de la normale au plan (hkl) (radians) |
| `Direction.RotateAroundPlaneInDeg(h, k, l, angle)` | Tourner autour de la normale au plan (hkl) (degrés) |
| `Direction.ProjectAlongPlane(h, k, l)` | Placer la normale au plan perpendiculaire à l'écran |
| `Direction.ProjectAlongAxis(u, v, w)` | Placer l'axe de zone perpendiculaire à l'écran |

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
| `SaveAsPng()` | Enregistrer le diagramme actuel au format PNG |
| `SpotInfo()` | Obtenir les données des taches sous forme de chaîne CSV |

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

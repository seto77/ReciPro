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
| `File.ReadText(filename)` | Lire un fichier texte en UTF-8 et le renvoyer comme chaîne ; sans `filename`, une boîte de dialogue s'ouvre. À associer à `Crystal.LoadCifText()` / `SaveText()` |
| `File.SaveText(textData, filename)` | Enregistrer des données texte dans un fichier ; écrit `textData` en UTF-8 ; sans `filename`, une boîte de dialogue d'enregistrement s'ouvre |

---

## Classe Crystal

Lit le cristal sélectionné et, via un brouillon en attente, crée et modifie des cristaux.

### Lecture

| Propriété / Fonction | Description |
|---|---|
| `Crystal.Name` | Nom du cristal |
| `Crystal.ChemicalFormula` | Formule chimique |
| `Crystal.Density` | Densité (g/cm³) |
| `Crystal.GetCellInAng()` | Constantes de maille sous la forme `[a, b, c, alpha, beta, gamma]` (Å, degrés) |
| `Crystal.SpaceGroupName` | Symbole Hermann–Mauguin du groupe d'espace, avec le suffixe de configuration (`:2`, `:H`, …) le cas échéant |
| `Crystal.SpaceGroupNumber` | Numéro du groupe d'espace des International Tables (1–230) |
| `Crystal.HasPending` | Si un brouillon est ouvert |

### Création et édition (brouillon → Commit)

Un cristal se construit dans un **brouillon en attente** : on le démarre, on le remplit avec les setters, puis `Commit()` valide tout, construit le cristal et l'applique comme cristal courant en une seule étape (l'interface et tous les simulateurs ouverts se mettent à jour, comme au chargement d'un fichier CIF). Un `Commit()` en échec signale toutes les erreurs de validation ensemble, ne change rien et conserve le brouillon, qui peut donc être corrigé puis recommitté.

| Fonction | Description |
|---|---|
| `Crystal.BeginCreate(name)` | Démarrer un brouillon pour un nouveau cristal |
| `Crystal.BeginEdit()` | Démarrer un brouillon depuis le cristal courant (maille, groupe d'espace, atomes et orientation sont repris) |
| `Crystal.LoadCifText(cifText)` | Démarrer un brouillon depuis un texte CIF (le contenu d'un fichier .cif, pas un chemin) |
| `Crystal.SetName(name)` | Renommer le brouillon |
| `Crystal.SetCellInAng(a, b, c, alpha, beta, gamma)` | Constantes de maille en **Å et degrés**. Chaque appel remplace toute la maille ; les arguments omis sont déduits des contraintes du groupe d'espace (pour un cristal cubique, `a` suffit), et les valeurs explicites qui les contredisent déclenchent une erreur |
| `Crystal.SetSpaceGroup(symbol)` | Groupe d'espace par symbole (HM court/complet ou Hall ; espaces et `_` ignorés). Ajoutez la configuration (`'Fd-3m:2'`, `'R-3c:H'`, `'P21/c:b1'`) quand le groupe en a plusieurs — un symbole ambigu déclenche une erreur listant les candidats |
| `Crystal.SetSpaceGroupByNumber(itNumber, setting)` | Groupe d'espace par numéro IT (1–230) ; `setting` (`'1'`, `'2'`, `'H'`, `'R'`, `'b1'`, …) choisit parmi plusieurs configurations |
| `Crystal.AddAtom(label, element, x, y, z, occ, bIso)` | Ajouter un atome de l'unité asymétrique : symbole de l'élément, coordonnées fractionnaires, occupation (0 < occ ≤ 1, défaut 1) et B isotrope en Å² (défaut 0). Positions équivalentes, lettres de Wyckoff et multiplicités sont déduites automatiquement |
| `Crystal.ClearAtoms()` | Retirer tous les atomes du brouillon |
| `Crystal.Commit()` | Valider, construire et appliquer le brouillon |
| `Crystal.Cancel()` | Abandonner le brouillon |

```python
ReciPro.Crystal.BeginCreate('NaCl')
ReciPro.Crystal.SetSpaceGroup('Fm-3m')
ReciPro.Crystal.SetCellInAng(5.6402)
ReciPro.Crystal.AddAtom('Na', 'Na', 0, 0, 0)
ReciPro.Crystal.AddAtom('Cl', 'Cl', 0.5, 0.5, 0.5)
ReciPro.Crystal.Commit()

base = ReciPro.Crystal.GetCellInAng()
for k in range(-2, 3):
    ReciPro.Crystal.BeginEdit()
    ReciPro.Crystal.SetCellInAng(base[0] * (1 + 0.01 * k))
    ReciPro.Crystal.Commit()
```

Après un `Commit()` réussi, le `BeginEdit()` suivant part du cristal **mis à jour** : les changements s'accumulent — pour un balayage en valeurs absolues, lisez les valeurs de base avant la boucle, comme ci-dessus. Pour inscrire le cristal dans la liste des cristaux, appelez `CrystalList.Add()`.

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

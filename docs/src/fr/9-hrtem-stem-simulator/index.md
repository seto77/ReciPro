---
title: HRTEM / STEM Simulator
---

# Simulateur HRTEM / STEM

Le **Simulateur HRTEM/STEM** simule les images de franges de réseau en MET (HRTEM), les images STEM et les potentiels cristallins projetés pour le cristal et l'orientation sélectionnés. Cliquez sur **Simuler** pour lancer le calcul.

![Simulateur HRTEM/STEM](../../assets/cap-fr-auto/FormImageSimulator.png)

La fenêtre est divisée en deux moitiés. Le **côté gauche** affiche le résultat de la simulation et en règle l'apparence (volets d'image, luminosité, couleur, barre d'échelle, etc.) ; le **côté droit** regroupe les conditions de calcul (**Propriétés optiques** et **Paramètres de simulation**).

---

## Cette page et les pages des modes

- **Cette page (présentation)** : les opérations communes à tous les modes, ainsi que les **commandes d'affichage et d'ajustement du résultat situées à gauche**.
- **Pages des modes** : tous les réglages qui apparaissent sur le **côté droit** pour ce mode, décrits de sorte que chaque page soit autonome (certains réglages figurent donc sur plusieurs pages).

| Mode | Contenu | Page |
|------|---------|------|
| **HRTEM** | Images de franges de réseau en MET haute résolution | [Simulation HRTEM](1-hrtem-simulation.md) |
| **STEM** | Images de microscopie électronique en transmission à balayage (BF / ABF / LAADF / HAADF) | [Simulation STEM](2-stem-simulation.md) |
| **Potential** | Potentiel cristallin projeté ($U_g$ / $U'_g$) | [Simulation du potentiel](3-potential-simulation.md) |

---

## Raccourcis clavier et souris

Les résultats sont affichés sous la forme d'un ou plusieurs volets d'image. Ils utilisent la [navigation standard de vue d'image](../21-shortcuts.md) de ReciPro, et tous les volets se déplacent et zooment ensemble.

| Raccourci | Action |
|----------|--------|
| <kbd>F1</kbd> | Ouvrir cette page du manuel en ligne |
| <kbd>CTRL</kbd>+<kbd>C</kbd> (grille d'images sélectionnée) | Copier la ou les images dans le presse-papiers sous forme de métafichier |
| Glisser avec le bouton gauche / le bouton du milieu | Déplacer l'image (tous les volets se déplacent ensemble) |
| Molette vers le haut / vers le bas | Zoom avant (×2) / arrière (×0.5) à la position du curseur |
| Glisser un rectangle avec le bouton droit | Zoomer sur la région sélectionnée |
| Clic droit / Double-clic droit | Zoom arrière (×0.5) |
| <kbd>CTRL</kbd> + glisser un rectangle avec le bouton droit | Sélectionner une zone rectangulaire |
| Double-clic gauche sur un volet | Agrandir ce volet / restaurer la grille (dispositions à plusieurs volets) |
| Déplacer la souris (sans bouton) | Lire la position (pm) et la valeur du pixel à la position du curseur |

→ Voir **[21. Raccourcis clavier et souris](../21-shortcuts.md)** pour un aperçu de chaque fenêtre.

---

## Itinéraires rapides par objectif

| Objectif | Point de départ | Référence |
|------|------------|-----------|
| Calculer une image HRTEM | Régler **Mode image** sur **HRTEM**, puis définir la tension d'accélération et la défocalisation dans **Conditions TEM** | [Simulation HRTEM](1-hrtem-simulation.md), [Formation de l'image HRTEM](../appendix/a3-bloch-wave/hrtem.md) |
| Calculer une image STEM | Régler **Mode image** sur **STEM**, puis définir l'angle de convergence et le détecteur dans **Options STEM** | [Simulation STEM](2-stem-simulation.md), [Calcul STEM](../appendix/a3-bloch-wave/stem.md) |
| Visualiser le potentiel projeté | Régler **Mode image** sur **Potential** | [Simulation du potentiel](3-potential-simulation.md) |
| Générer une série en épaisseur / défocalisation | En HRTEM, configurer **Mode unique/série** et les conditions d'image | [Simulation HRTEM](1-hrtem-simulation.md) |
| Utiliser HAADF-STEM avec TDS | Définir des facteurs de température atomiques non nuls et placer le détecteur STEM en LAADF / HAADF | [Calcul STEM](../appendix/a3-bloch-wave/stem.md) |

---

## Flux de travail de base

1. Sélectionnez le cristal et l'orientation dans la fenêtre principale, puis ouvrez cette fenêtre.
2. Choisissez HRTEM, STEM ou Potential dans **Mode image**.
3. Définissez la tension d'accélération, la défocalisation, les aberrations, le diaphragme, l'angle de convergence STEM, etc. dans **Propriétés optiques** (voir les pages des modes).
4. Définissez l'épaisseur, la taille de l'image, la résolution, le nombre d'ondes de Bloch, le modèle de cohérence partielle, etc. dans **Paramètres de simulation** (voir les pages des modes).
5. Cliquez sur **Simuler**, puis ajustez l'apparence avec **Ajuster**, **Normalisation** et **Affichage** à gauche si nécessaire.

---

## Sélection du mode image

![Mode image](../../assets/cap-fr-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxImageMode.png){ align=left }

**Mode image**, en haut à droite, sélectionne le type de calcul. Les panneaux de droite (**Propriétés optiques** et **Paramètres de simulation**) s'adaptent au mode choisi.<div style="clear: both;"></div>

- **HRTEM** — images de franges de réseau en MET haute résolution → [Simulation HRTEM](1-hrtem-simulation.md)
- **STEM** — images de microscopie électronique en transmission à balayage → [Simulation STEM](2-stem-simulation.md)
- **Potential** — potentiel cristallin projeté → [Simulation du potentiel](3-potential-simulation.md)

---

## Zone d'image (côté gauche)

La moitié gauche de la fenêtre affiche l'image simulée. La barre d'état en haut indique la position du curseur (**X:**, **Y:**) et la valeur de l'image **Valeur:** (intensité) sous le curseur, à côté d'une échelle d'intensité **Bas → Haut** qui reflète la carte de couleurs et la plage de luminosité actuelles.

Lorsque plusieurs images sont produites (image en série, ou module/phase d'un potentiel), elles sont disposées en grille, et tous les volets zooment et se déplacent ensemble.

---

## Affichage et ajustement des résultats (panneau de gauche) {#display-settings}

Le panneau en bas à gauche règle l'apparence du résultat — luminosité, couleur, normalisation et superpositions. Ces réglages s'appliquent à tous les modes et prennent effet sans recalcul.

### Ajuster

![Ajuster](../../assets/cap-fr-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxAdjust.png)

- **Min** / **Max** : extrémités inférieure (noir) et supérieure (blanc) de la plage d'intensité affichée. Utilisez les curseurs pour régler le contraste.
- **Couleur** : échelle de couleurs de l'image — **Gray scale** (niveaux de gris) ou **Cold-Warm** (du bleu au rouge).
- **Flou gaussien (FWHM)** : lorsque cette case est cochée, applique un flou gaussien dont la largeur à mi-hauteur (pm) est indiquée à droite, pour simuler une résolution finie (fonction d'étalement du point).

### Normalisation

![Normalisation](../../assets/cap-fr-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxNormalization.png)

- **Par image** : lorsque cette case est cochée, chaque image est normalisée séparément (décochée, toute la série partage une échelle commune).
- **Min** / **Max** : fixe l'extrémité inférieure / supérieure de la normalisation à la valeur indiquée à droite, au lieu du minimum / maximum de l'image.
- **EDX commun** : affiché uniquement lorsqu'une [carte EDX](2-stem-simulation.md#stem-edx) est affichée et que le calcul a produit plus d'un canal ; place tous les canaux d'une même grandeur sur une même plage d'affichage. Les cartes EDX ne sont jamais remises à l'échelle par **Min** / **Max** : leur extrémité inférieure est fixée à 0 et seule l'extrémité supérieure suit **Par image** / **EDX commun**.
- Ce groupe est masqué en mode Potentiel.

### Image STEM

![Image STEM](../../assets/cap-fr-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

Affiché uniquement en mode STEM. Sélectionne la composante de diffusion de l'image STEM calculée qui est affichée (**Élast.**, **TDS** ou **Élast. & TDS**). Si le calcul a aussi produit des [cartes STEM-EDX](2-stem-simulation.md#stem-edx), **EDX** est proposé comme quatrième choix. Comme ce réglage est propre au STEM, il est également décrit sur la page [Simulation STEM](2-stem-simulation.md).

### Affichage

![Affichage](../../assets/cap-fr-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxDisplay.png)

Définit les éléments superposés à l'image.

- **Maille** : superpose le contour de la maille projetée, pour relier le contraste de l'image au réseau cristallin. Les arêtes parallèles à **a**, **b** et **c** sont tracées en rouge, vert et bleu, centrées sur le centre de l'image.
- **Texte** : superpose l'étiquette de chaque panneau — l'épaisseur `t=` et la défocalisation `f=` (nm) en HRTEM / STEM, ou le nom du panneau (par exemple *Ug magnitude*) en mode Potentiel. La **Taille** (taille de police) et la **Couleur** peuvent être spécifiées.
- **Échelle** : superpose une barre d'échelle. La **Long.** (longueur, nm) et la **Couleur** peuvent être spécifiées.

---

## Lancement de la simulation

![Simulation actions](../../assets/cap-fr-auto/FormImageSimulator.splitContainer1.panelSimulationActions.png)

- **Simuler** : lance le calcul avec le cristal, les conditions du microscope, l'épaisseur, la défocalisation et les réglages d'affichage actuels.
- **Arrêter** : interrompt le calcul en cours (affiché uniquement pendant le calcul).
- **Temps réel** : lorsque cette case est cochée, recalcule immédiatement lorsque le cristal est tourné (masqué en mode STEM).
- **Préréglages** : affiche ou masque la fenêtre des préréglages, qui enregistre et rappelle des conditions d'imagerie TEM (voir la [liste des préréglages](#preset-list) ci-dessous).

### Liste des préréglages {#preset-list}

La fenêtre **Preset list** conserve des instantanés nommés des réglages du simulateur. Un instantané enregistre le mode image, la tension d'accélération, Cs, Cc, ΔV, la défocalisation, β, le diaphragme objectif (taille et décalage), le modèle de cohérence partielle HRTEM, l'épaisseur et les listes d'épaisseurs / de défocalisations en série, la taille et la résolution de l'image, le nombre d'ondes de Bloch, ainsi que les réglages STEM (angle de convergence, angles du détecteur, taille de source, épaisseur de tranche, résolution angulaire, **Calculer les cartes EDX** et **Séries de raies (Kα, Lα…)**). La liste est enregistrée avec les réglages de ReciPro et survit à un redémarrage.

- Cliquer sur une entrée l'applique immédiatement.
- **OK** ferme la fenêtre en conservant les réglages actuels ; **Annuler** (ou <kbd>Esc</kbd>) la ferme et restaure les réglages en vigueur à l'ouverture de la fenêtre.
- **Gérer la liste des préréglages** : lorsque cette case est cochée, cliquer sur une entrée ne fait que la sélectionner (elle n'est pas appliquée), et le champ **Nom** apparaît avec **Add** / **Remplacer** / **Renommer** / **Supprimer**. **Add** ajoute les réglages actuels sous le nom indiqué, **Remplacer** écrase l'entrée sélectionnée avec les réglages actuels, **Renommer** ne change que son nom.

Les préréglages de microscope (ARM300F, 2100F, …) ne figurent pas dans cette liste ; ils se trouvent dans le menu contextuel (clic droit) de **Conditions TEM** (voir [Simulation HRTEM](1-hrtem-simulation.md#tem-conditions)).

---

## Menu Fichier

![Menu Fichier](../../assets/cap-fr-auto/FormImageSimulator.menuStrip1.fileToolStripMenuItem.png)

- **Enregistrer l'image** : enregistre **comme image (format PNG)**, **comme image (format TIFF)** ou **comme métafichier (EMF)**. PNG et EMF enregistrent ce qui est affiché (plage d'affichage et échelle de couleurs) ; **Enregistrer individuellement en mode série** (activé par défaut) écrit chaque panneau dans son propre fichier au lieu d'une seule image en mosaïque. TIFF écrit toujours un fichier à virgule flottante par panneau, sans superpositions. Il stocke les valeurs des pixels après **Normalisation** (et après **Flou gaussien (FWHM)** s'il est appliqué), et non la plage d'affichage **Ajuster** ; avec les valeurs par défaut (**Min** 0, **Max** 1, **Par image**), chaque image est mise à l'échelle sur 0–1 : décochez **Min** / **Max** pour exporter les intensités calculées. Les cartes EDX sont toujours écrites sans mise à l'échelle.
- **Copier l'image** : copie dans le presse-papiers **comme image** ou **comme métafichier (EMF)**.
- **Surimprimer les symboles** (activé par défaut) : incruste la maille, les étiquettes et la barre d'échelle dans l'image PNG / EMF enregistrée ou copiée.
- **Charger les paramètres TEM** / **Enregistrer les paramètres TEM** : affichés mais désactivés (non implémentés). Utilisez la [liste des préréglages](#preset-list) pour enregistrer et rappeler des conditions.

## Menu Aide

![Menu Aide](../../assets/cap-fr-auto/FormImageSimulator.menuStrip1.helpToolStripMenuItem.png)

- **Concept de base de la simulation HRTEM** : ouvre l'explication de la formation de l'image HRTEM ([Annexe A3.2](../appendix/a3-bloch-wave/hrtem.md)).
- **Bibliothèque de calcul** : sélectionne la bibliothèque de calcul — **Native code** (C++/Eigen rapide) ou **Managed code** (.NET). Le code natif est normalement plus rapide. Ce choix n'est utilisé que pour la synthèse d'image HRTEM ; le STEM choisit son solveur automatiquement (le STEM-EDX exige toujours la bibliothèque native), et le choix est réinitialisé à **Native code** à chaque démarrage de ReciPro.

---

## Voir aussi

- [Simulation HRTEM](1-hrtem-simulation.md)
- [Simulation STEM](2-stem-simulation.md)
- [Simulation du potentiel](3-potential-simulation.md)
- [Diffraction dynamique (onde de Bloch)](../appendix/a3-bloch-wave/index.md)
- [Simulateur de diffraction](../7-diffraction-simulator/index.md)
- [Trajectoires électroniques](../8-electron-trajectory.md)

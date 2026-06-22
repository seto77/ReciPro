# Raccourcis clavier et souris

ReciPro associe de nombreuses fonctions à des **combinaisons de touches** et à des **boutons de souris combinés avec des touches de modification** — des éléments qui ne sont visibles ni sur un bouton ni dans un menu. Cette page les rassemble tous en un seul endroit. La page de chaque fenêtre reprend également ses raccourcis près du début.

<kbd>F1</kbd> fonctionne dans **chaque** fenêtre et ouvre la page correspondante de ce manuel en ligne.

---

## Raccourcis applicables à toute l'application

Ils sont installés par la [fenêtre principale](0-main-window.md) mais restent actifs lorsque les fenêtres Visualiseur de structure, Stéréonet, Simulateur de diffraction, Spot ID et Calculatrice ont le focus.

| Raccourci | Action |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>D</kbd> | Activer/désactiver le Simulateur de diffraction |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>V</kbd> | Activer/désactiver le Visualiseur de structure |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>S</kbd> | Activer/désactiver le Stéréonet |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>T</kbd> | Activer/désactiver Spot ID |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd> + touches fléchées | Faire tourner le cristal d'un pas dans cette direction (maintenez deux flèches pour une diagonale) |
| Appui double sur <kbd>CTRL</kbd> | Activer/désactiver la Calculatrice |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>R</kbd> | Basculer l'indicateur *Reserved* du cristal sélectionné |
| <kbd>CTRL</kbd>+<kbd>ALT</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Capturer une capture d'écran de l'interface (outil de développement ; activez d'abord **Capture GUI Components**) |
| Maintenir <kbd>CTRL</kbd> pendant le démarrage de ReciPro | Démarrer avec OpenGL désactivé (récupération en cas de problèmes graphiques) |

---

## Modèles d'interaction communs

Presque toutes les vues interactives de ReciPro appartiennent à l'une de trois familles. Connaître la famille indique le comportement de glissement/zoom sans devoir mémoriser chaque fenêtre.

### Vues OpenGL 3-D { #3d }

Utilisées par le [Visualiseur de structure](5-structure-viewer.md), la [Géométrie de rotation](4-rotation-geometry.md), la sphère 3-D du [Stéréonet](6-stereonet.md), les [Trajectoires électroniques](8-electron-trajectory.md) et les vues géométrie / master-pattern de l'[EBSD](12-ebsd-simulation.md).

| Action | Résultat |
|--------|--------|
| Glisser avec le bouton gauche | Rotation — trackball près du centre, roulis dans le plan près du bord |
| Glisser avec le bouton droit vers le haut/bas, ou molette de la souris | Zoom |
| Glisser avec le bouton du milieu | Déplacement (uniquement là où c'est activé) |
| <kbd>CTRL</kbd> + glisser avec le bouton droit vers le haut/bas | Modifier la distance de la caméra (mode perspective uniquement) |
| <kbd>CTRL</kbd> + double-clic droit | Basculer entre projection orthographique / perspective |

Certaines fenêtres peuvent désactiver le déplacement ou le zoom (par exemple, le déplacement est désactivé pour les Trajectoires électroniques et les vues 3-D de l'EBSD).

### Vues de figure de diffraction { #pattern }

Utilisées par la figure du [Simulateur de diffraction](7-diffraction-simulator/index.md), la figure de Kikuchi de l'[EBSD](12-ebsd-simulation.md) et le [Stéréonet](6-stereonet.md) 2-D. La différence essentielle par rapport aux vues 3-D : **le glissement fait tourner le cristal lui-même**, et non seulement la caméra, de sorte que chaque fenêtre liée se met à jour en même temps.

| Action | Résultat |
|--------|--------|
| Glisser avec le bouton gauche près du centre | Incliner le cristal |
| Glisser avec le bouton gauche dans la zone extérieure | Faire tourner le cristal autour de l'axe de visée/du faisceau |
| Clic droit | Dézoomer |
| Glisser un cadre avec le bouton droit | Zoomer sur la région sélectionnée |
| Glisser avec le bouton du milieu | Déplacement |

Il n'y a **pas** de zoom à la molette sur ces vues.

### Vues d'image { #image }

Utilisées par les panneaux de résultat du [HRTEM/STEM](9-hrtem-stem-simulator/index.md), l'image du [Spot ID v2](11-spot-id-v2.md) et le master pattern 2-D de l'[EBSD](12-ebsd-simulation.md).

| Action | Résultat |
|--------|--------|
| Glisser avec le bouton gauche / du milieu | Déplacement |
| Molette de la souris vers le haut / bas | Zoomer (×2) / dézoomer (×0.5) au niveau du curseur |
| Glisser un cadre avec le bouton droit | Zoomer sur la région sélectionnée |
| Clic droit / double-clic droit | Dézoomer (×0.5) |

---

## Référence par fenêtre

### 0. Fenêtre principale
[Ouvrir la page →](0-main-window.md) · plus les raccourcis applicables à toute l'application ci-dessus.

| Raccourci | Action |
|----------|--------|
| Glisser avec le bouton gauche le widget d'orientation (en bas à gauche) | Faire tourner le cristal |
| Double-clic droit sur le widget d'orientation | Copier l'image du widget dans le presse-papiers |
| Simple clic / double-clic sur un bouton de fonction | Activer/désactiver cette fenêtre / la forcer au premier plan |
| Clic droit sur un cristal dans la liste | Menu contextuel (Renommer / Dupliquer / Supprimer / Exporter CIF…) |
| Double-clic sur l'étiquette **Current Index** | Afficher/masquer le champ max-UVW |
| Déposer un fichier | Charger une liste de cristaux (`.xml`, `.cdb2`) ou un cristal (`.cif`, `.amc`) |

### 1. Base de données de cristaux
[Ouvrir la page →](1-crystal-database.md)

| Raccourci | Action |
|----------|--------|
| <kbd>ENTER</kbd> dans un champ de recherche | Lancer la recherche |
| Clic sur une ligne de résultat | Charger ce cristal |
| Clic sur un élément dans le popup du tableau périodique | Faire défiler son filtre : ignorer → doit inclure → doit exclure |

### 2. Informations de symétrie · 3. Interaction du faisceau
Les Informations de symétrie n'ont pas de combinaisons clavier/souris particulières. Dans l'Interaction du faisceau, outre <kbd>F1</kbd> et les boutons **Copy**, le curseur vertical du graphique **Scattering factors** peut être glissé pour lire la valeur de chaque élément.
[Symétrie →](2-symmetry-information.md) · [Interaction du faisceau →](3-beam-interaction.md)

### 4. Géométrie de rotation
[Ouvrir la page →](4-rotation-geometry.md) — six [vues 3-D](#3d) **liées** ; faire tourner l'une fait tourner les six ensemble. Pour les petites vues *Axes* / *Objects*, le zoom et le déplacement sont désactivés.

### 5. Visualiseur de structure
[Ouvrir la page →](5-structure-viewer.md) — la vue principale est une [vue 3-D](#3d).

| Raccourci | Action |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Copier l'image rendue dans le presse-papiers |
| Double-clic gauche sur un atome | Afficher les coordonnées, les distances aux plus proches voisins et les angles de liaison |
| Glisser avec le bouton gauche le gizmo des axes cristallins | Faire tourner le modèle (pas de rotation dans le plan) |
| Glisser avec le bouton gauche le gizmo de lumière | Modifier la direction de l'éclairage |

### 6. Stéréonet
[Ouvrir la page →](6-stereonet.md) — le réseau 2-D est une [vue de figure de diffraction](#pattern) ; la sphère 3-D optionnelle est une [vue 3-D](#3d).

| Raccourci | Action |
|----------|--------|
| Double-clic gauche sur le réseau | Basculer entre la projection **Plane** et **Axis** |
| Déplacer la souris sur le réseau | Lire les (hkl)/[uvw] sous le curseur |

### 7. Simulateur de diffraction
[Ouvrir la page →](7-diffraction-simulator/index.md) — la figure est une [vue de figure de diffraction](#pattern) (pas de zoom à la molette).

| Raccourci | Action |
|----------|--------|
| Double-clic gauche sur une tache | Afficher les détails de la réflexion (indice, *d*, facteur de structure, erreur d'excitation) |
| <kbd>CTRL</kbd> + glisser avec le bouton du milieu | Déplacer le centre du détecteur (lorsque la zone du détecteur est affichée) |
| Double-clic droit sur la barre d'état | Copier un résumé textuel des réglages actuels |
| Double-clic droit sur un bouton de couche actif (Spots / Kikuchi / Debye / Scale) | Faire clignoter cette couche |
| Double-clic gauche sur le stéréonet — fenêtre **TEM holder** | Régler l'inclinaison du porte-objet sur ce point |
| Touches fléchées — fenêtre **TEM holder** | Modifier l'inclinaison du porte-objet par pas (cochez d'abord **Arrow keys**) |
| Déposer `.prm` / une image — **Detector geometry**, ou `.txt` — **Dynamic compression** | Charger ces données |

### 8. Trajectoires électroniques
[Ouvrir la page →](8-electron-trajectory.md) — une [vue 3-D](#3d) avec déplacement désactivé.

### 9. Simulateur HRTEM / STEM
[Ouvrir la page →](9-hrtem-stem-simulator/index.md) — les panneaux de résultat sont des [vues d'image](#image) et se déplacent/zooment ensemble.

| Raccourci | Action |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>C</kbd> (grille d'images au focus) | Copier la ou les images dans le presse-papiers sous forme de métafichier |
| <kbd>CTRL</kbd> + glisser un cadre avec le bouton droit | Sélectionner une zone rectangulaire |
| Double-clic gauche sur un panneau | Agrandir ce panneau / restaurer la grille (dispositions multi-panneaux) |

### 10. Spot ID v1
[Ouvrir la page →](10-spot-id.md) — l'image sert uniquement de référence (non interactive).

| Raccourci | Action |
|----------|--------|
| Double-clic sur une ligne de la liste des résultats | Sélectionner ce cristal et le faire tourner vers l'axe de zone correspondant |

### 11. Spot ID v2
[Ouvrir la page →](11-spot-id-v2.md) — l'image est une [vue d'image](#image) avec édition de taches par-dessus.

| Raccourci | Action |
|----------|--------|
| Double-clic gauche sur l'image | Ajouter une tache (ajustée au pic) |
| <kbd>CTRL</kbd> + double-clic gauche | Ajouter une tache et la marquer comme faisceau direct (000) |
| Clic gauche sur une tache | Sélectionner la tache la plus proche |
| <kbd>CTRL</kbd> + clic droit sur une tache | Supprimer la tache la plus proche |
| <kbd>CTRL</kbd> + touches fléchées | Déplacer la tache sélectionnée d'un pixel |
| Double-clic sur l'en-tête de ligne d'une tache | Zoomer sur cette tache (×2) |

### 12. Simulation EBSD
[Ouvrir la page →](12-ebsd-simulation.md) — la figure de Kikuchi est une [vue de figure de diffraction](#pattern) ; les vues 3-D sont des [vues 3-D](#3d) (déplacement désactivé) ; le master pattern 2-D est une [vue d'image](#image).

| Raccourci | Action |
|----------|--------|
| Double-clic sur la figure de Kikuchi | Sélectionner la sous-cellule du détecteur sous le curseur et afficher ses statistiques |

### 20. Macro
[Ouvrir la page →](20-macro/index.md)

| Raccourci | Action |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>S</kbd> | Enregistrer le texte de l'éditeur dans l'entrée de la liste des macros sélectionnée |
| <kbd>F10</kbd> | Avancer d'un pas (pendant l'exécution pas à pas) |
| Double-clic sur une ligne de la liste d'aide des fonctions | Insérer la signature de cette fonction au niveau du curseur |
| Déposer un fichier `.mcr` | Le charger dans l'éditeur |

# Manuel ReciPro

<!-- 260623Ch: Shared demo movie for all localized top pages. -->
<div class="rp-demo-video" markdown="0">
  <video controls autoplay muted playsinline preload="metadata" aria-label="ReciPro demo movie">
    <source src="../assets/Recipro_Demo.mp4" type="video/mp4">
  </video>
</div>

## Brève introduction
* ReciPro est un logiciel libre sous licence MIT qui fournit une variété de calculs cristallographiques et de simulations de microscopie électronique.
* ReciPro a été téléchargé plus de 27 000 fois au total depuis sa publication sur GitHub (mars 2020) et est utilisé par de nombreux cristallographes et microscopistes électroniques.

## Trouver par objectif

| Objectif | Commencer ici | Principales étapes suivantes |
|------|------------|-----------------|
| Charger un cristal et définir son orientation | [Fenêtre principale](0-main-window.md) | [Géométrie de rotation](4-rotation-geometry.md), [Annexe A1. Systèmes de coordonnées](appendix/a1-coordinate-system/1-orientation.md) |
| Examiner une structure cristalline en 3D | [Visualiseur de structure](5-structure-viewer.md) | [Informations de symétrie](2-symmetry-information.md) |
| Calculer des clichés SAED / XRD / PED / CBED | [Simulateur de diffraction](7-diffraction-simulator/index.md) | [SAED](7-diffraction-simulator/1-saed-simulation.md), [Diffraction des rayons X](7-diffraction-simulator/4-x-ray-neutron-diffraction.md), [PED](7-diffraction-simulator/2-ped-simulation.md), [CBED](7-diffraction-simulator/3-cbed-simulation.md) |
| Calculer des images HRTEM / STEM | [Simulateur HRTEM/STEM](9-hrtem-stem-simulator/index.md) | [HRTEM](9-hrtem-stem-simulator/1-hrtem-simulation.md), [STEM](9-hrtem-stem-simulator/2-stem-simulation.md) |
| Simuler des clichés EBSD | [Simulation EBSD](12-ebsd-simulation.md) | [Trajectoires électroniques](8-electron-trajectory.md), [Annexe A3. Calcul EBSD](appendix/a3-bloch-wave/ebsd.md) |
| Indexer des taches de diffraction expérimentales | [Spot ID v1](10-spot-id.md), [Spot ID v2](11-spot-id-v2.md) | [Simulateur de diffraction](7-diffraction-simulator/index.md) |
| Comprendre les équations de la diffraction dynamique | [Annexe A3. Méthode des ondes de Bloch](appendix/a3-bloch-wave/index.md) | [Calcul dynamique](appendix/a3-bloch-wave/calculation.md), [CBED](appendix/a3-bloch-wave/cbed.md), [STEM](appendix/a3-bloch-wave/stem.md), [EBSD](appendix/a3-bloch-wave/ebsd.md) |

## Fonctionnalités
* **Full GUI** : Toutes les opérations s'effectuent via une interface graphique. La plupart des entrées/sorties de fichiers prennent en charge le glisser-déposer.
* **Liste des cristaux** : Gérer plusieurs cristaux à la fois ; pas besoin d'ouvrir une fenêtre distincte pour chaque cristal.
* **Base de données de groupes d'espace** : Base de données intégrée couvrant les 230 groupes d'espace des International Tables Volume A, plus 530 symboles de Hall, avec éléments de symétrie, positions de Wyckoff et règles d'extinction. Les éléments de symétrie et les positions générales peuvent être dessinés sous forme de diagrammes schématiques dans le style des *International Tables* Vol. A (voir [2. Informations de symétrie](2-symmetry-information.md)).
* **Informations atomiques** : Facteurs de diffusion (rayons X, électron, neutron), énergies de rayons X caractéristiques, rapports isotopiques, etc. pour les éléments H (1) – Cf (98).
* **Rotation flexible du cristal** : Définir l'orientation par indices d'axe de zone / de plan cristallin ou par glisser de souris. La notation de Miller-Bravais (4 indices *hkil*) est prise en charge pour les systèmes trigonaux/hexagonaux. L'état de rotation est synchronisé entre toutes les fenêtres de simulation.
* **Simulation de diffraction** : Diffraction électronique cinématique et dynamique (méthode des ondes de Bloch), diffraction des rayons X (y compris caméras à précession et back-Laue), diffraction électronique en précession (PED) et diffraction électronique à faisceau convergent (CBED). Une simulation de porte-objet MET relie le cliché de diffraction aux angles d'inclinaison du porte-objet.
* **Simulation HRTEM / STEM** : Simulation d'image TEM haute résolution avec modèles de cohérence partielle ; STEM avec diffusion thermique diffuse.
* **EBSD & trajectoires électroniques** : Simulation de clichés EBSD et simulation Monte-Carlo des trajectoires électroniques (voir [8. Trajectoires électroniques](8-electron-trajectory.md)).
* **Indexation des taches** : Détection, ajustement et indexation automatiques des taches de diffraction à partir d'images expérimentales (Spot ID v1/v2).
* **Macro** : Macro à syntaxe Python pour automatiser les opérations (voir [20. Macro](20-macro/index.md)).
* **Thème clair / sombre** : L'interface suit un mode de couleur clair ou sombre sélectionnable.

## Configuration requise
| Élément | Minimum | Recommandé |
|------|---------|-------------|
| Système d'exploitation | Windows avec [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows on ARM64 pris en charge) | Windows 11 |
| GPU | OpenGL 1.3 | GPU externe avec OpenGL 4.3 |
| Mémoire | – | 16 Go ou plus |
| CPU | – | 8 cœurs ou plus (pour les calculs dynamiques) |

**Windows on ARM (natif, expérimental)** : Un package portable ARM64 natif expérimental (`ReciPro-v.X_arm64.zip`, self-contained — aucune installation du .NET Runtime requise) est disponible sur la [page des releases](https://github.com/seto77/ReciPro/releases/latest). Les packages x64 ordinaires s'exécutent également sous Windows ARM64 grâce à l'émulation intégrée. Consultez la section [Dépannage](troubleshooting.md#windows-on-arm) pour les notes de configuration et les limitations.

**macOS (non officiel)** : ReciPro ne prend officiellement en charge que Windows, mais il a été rapporté que le package ZIP portable **win-x64** fonctionne sur macOS (Apple Silicon) à l'aide du wrapper Wine Sikarugir combiné au pilote OpenGL Mesa3D. Un guide de configuration publié par un utilisateur est disponible à l'adresse <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>. Notez que cette voie n'est pas officiellement prise en charge et que certains symboles (Å, exposants, flèches) peuvent s'afficher incorrectement. Le ZIP ARM64 ne fonctionne **pas** sous macOS + Wine, et la voie actuelle x64 + Rosetta 2 devrait cesser de fonctionner à partir de macOS 28 (automne 2027) — voir [Dépannage](troubleshooting.md#mac-linux) pour les détails.

## Comment utiliser ce manuel

Ce manuel sur GitHub Pages constitue la référence actuelle. Utilisez la navigation à gauche pour parcourir par chapitre, ou la recherche dans l'en-tête pour trouver un nom de fonction ou un libellé d'interface. Les anciens manuels PDF sont conservés à des fins d'archivage.

* **PDF archivé (anglais) :** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf>
* **PDF archivé (japonais) :** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf>

## Démarrage rapide
1. Téléchargez et installez depuis [Releases](https://github.com/seto77/ReciPro/releases/latest).
2. Sélectionnez un cristal dans la liste intégrée (~80 cristaux). Vous pouvez également importer des fichiers CIF ou utiliser [CSManager](https://github.com/seto77/CSManager).
3. Appelez les fonctions depuis le panneau de droite : Visualiseur de structure, Stéréonet, Simulateur de diffraction, simulation HRTEM, etc.
4. Faites pivoter le cristal par glisser de souris ou en saisissant des indices d'axe de zone / de plan.

## Référence
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). <https://doi.org/10.1107/S1600576722000139>

## Licence
ReciPro est distribué sous la [licence MIT](https://github.com/seto77/ReciPro/blob/master/LICENSE.md).

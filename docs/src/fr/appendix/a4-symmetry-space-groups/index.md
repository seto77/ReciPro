# Annexe A4. Symétrie et groupes d'espace

Le chapitre de la fenêtre principale [2. Informations de symétrie](../../2-symmetry-information.md) est un guide de la GUI : il indique quel onglet affiche quoi, et quel bouton copie quel diagramme. Cette annexe rassemble le **contexte cristallographique et de théorie des groupes** derrière ces tableaux et ces figures — ce qu'un symbole de Hermann–Mauguin encode réellement, comment lire les diagrammes des éléments de symétrie et des positions générales au style des *International Tables for Crystallography* (ITA) Vol. A, et ce que signifient les tables de supergroupes/sous-groupes de la fenêtre **Relations de groupe…** et leur terminologie (*translationengleiche*, *klassengleiche*, classe de conjugaison, domaines, lois de macle, …).

![Informations de symétrie](../../../assets/cap-fr-auto/FormSymmetryInformation.png)

Deux fenêtres sont couvertes, et la théorie se lit le mieux dans cet ordre :

1. **[A4.1. Symboles des groupes d'espace et diagrammes de symétrie](symbols-and-diagrams.md)** — les symboles de Hermann–Mauguin, de Schoenflies et de Hall ; la classification de théorie des groupes affichée dans l'onglet **Propriétés** (centrosymétrique, Sohncke, symorphique, polaire, classe cristalline arithmétique, symétrie de Patterson, …) ; la description de chaque opération de symétrie de l'onglet **Opérations** par triplet de coordonnées/symbole de Seitz/type géométrique ; et les conventions graphiques des diagrammes des éléments de symétrie et des positions générales au bas de la fenêtre [Informations de symétrie](../../2-symmetry-information.md).
2. **[A4.2. Relations groupe–sous-groupe](group-subgroup-relations.md)** — ce qu'est un *sous-groupe maximal* / *supergroupe minimal*, la distinction *t*-/*k*- de Hermann, et la lecture de chaque onglet du navigateur **Relations de groupe…** (Diagramme, Matrice, Éclatement d'orbite, Domaines & Macles, Nouvelles réflexions) ouvert depuis le panneau **Options** d'Informations de symétrie.

A4.1 vient en premier parce que A4.2 s'y réfère constamment : chaque relation de sous-groupe/supergroupe est elle-même étiquetée avec exactement les mêmes symboles de Hermann–Mauguin, symboles de Seitz et formules de type géométrique (*« 3-fold rotation »*, *« c-glide plane »*, *« screw axis »*, …) qui y sont introduits.

---

## Portée et sources

La base de données intégrée de ReciPro couvre les 230 types de groupes d'espace (avec 530 settings/choix d'origine tabulés) exactement tels qu'ils sont tabulés dans les *International Tables for Crystallography*, **Volume A** (symétrie des groupes d'espace) et **Volume A1** (sous-groupes maximaux des groupes d'espace). Cette annexe explique la *présentation* de ces données par ReciPro — la notation, les diagrammes, l'outil de navigation — et suppose que le lecteur possède déjà une familiarité de niveau licence avec les réseaux, les groupes ponctuels et la notion d'opération de symétrie. Elle ne remplace pas l'ITA elle-même, qui demeure la référence faisant autorité pour les données tabulées (et que ReciPro ne peut pas reproduire à l'identique pour des raisons de droits d'auteur — voir l'onglet **Réglages** pour la liste propre à ReciPro des origines/settings alternatifs d'un type de groupe d'espace donné).

!!! note "Relations de groupe… est une fonctionnalité en développement actif"
    Le navigateur **Relations de groupe…** (A4.2) calcule les sous-groupes et supergroupes *translationengleiche* (t-) et *klassengleiche* (k-, y compris *isomorphes*) directement à partir des opérations de symétrie du groupe d'espace lui-même (et non d'une liste pré-tabulée), de sorte que chaque relation affichée est vérifiée de manière indépendante au lieu d'être recopiée d'une table. Les limites restantes — par ex. la série isomorphe n'est énumérée que jusqu'à l'indice ≤ 4 — sont détaillées dans les **Limitations actuelles** d'A4.2.

---

## Voir aussi

- [2. Informations de symétrie](../../2-symmetry-information.md) — le guide de la GUI que cette annexe explique.
- [A4.1. Symboles des groupes d'espace et diagrammes de symétrie](symbols-and-diagrams.md) · [A4.2. Relations groupe–sous-groupe](group-subgroup-relations.md)
- [Annexe A1. Systèmes de coordonnées](../a1-coordinate-system/1-orientation.md)
- [Annexe A2. Interaction du faisceau (contexte de physique du solide)](../a2-beam-interaction/index.md) — où les conditions de réflexion du groupe d'espace (extinctions systématiques) alimentent le facteur de structure.

# ReciPro

[![Documentation](https://img.shields.io/badge/%F0%9F%93%96_Documentation-blue)](https://seto77.github.io/ReciPro/fr/)
[![Latest Release](https://img.shields.io/github/v/release/seto77/ReciPro?logo=github)](https://github.com/seto77/ReciPro/releases/latest)
[![Total downloads](https://img.shields.io/github/downloads/seto77/ReciPro/total?logo=github&label=GitHub%20downloads)](https://github.com/seto77/ReciPro/releases)
[![GitHub Stars](https://img.shields.io/github/stars/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/forks)
[![License: MIT](https://img.shields.io/badge/License-MIT-green)](https://github.com/seto77/ReciPro/blob/master/LICENSE.md)

<!-- 260804Cl: Traduction de ../../README.md (anglais). Mettre ce fichier à jour lorsque la version anglaise change. -->
[English](../../README.md) | [日本語](README.ja.md) | [Deutsch](README.de.md) | **Français** | [Español](README.es.md) | [Italiano](README.it.md) | [Русский](README.ru.md) | [简体中文](README.zh-Hans.md) | [繁體中文](README.zh-Hant.md) | [한국어](README.ko.md) | [Português](README.pt.md)

*ReciPro* est un logiciel de cristallographie polyvalent, libre et open source, doté d'une interface graphique. Il donne un accès fluide à des fonctions permettant d'explorer des bases de données cristallographiques, de visualiser des structures cristallines et des réglages de goniomètre, de simuler des clichés de diffraction et des images de microscopie haute résolution, et d'analyser des données de diffraction. Ces fonctions sont reliées entre elles par une interface conviviale, et les résultats s'affichent de façon synchronisée, pratiquement en temps réel. *ReciPro* accompagnera un large éventail de cristallographes (y compris débutants) travaillant avec la diffraction des rayons X, des électrons et des neutrons, ainsi qu'en MET.

*ReciPro* est développé sans interruption depuis 2002 et est disponible sur GitHub depuis mars 2020. Il a été téléchargé plus de 27 000 fois depuis GitHub et est utilisé par des centaines d'utilisateurs dans plus d'une dizaine de laboratoires universitaires et industriels.

***[Consultez le manuel pour apprendre à l'utiliser !](https://seto77.github.io/ReciPro/fr/)***

[Diverses simulations exécutées en temps réel (exemple : MgAl2O4)](https://github.com/user-attachments/assets/6b0234dd-f2d6-49db-b146-bb74cf6021b6)

## Auteurs

*ReciPro* est développé par [Seto Y.](https://yseto.net/en/home-e) et [Ohtsuka M.](https://researchmap.jp/7000002999?lang=en). Les fonctions et les algorithmes sont présentés dans [l'article](https://github.com/seto77/ReciPro/blob/master/docs/ReciProSetoOhtsuka2022.pdf).

## Citation

Si vous utilisez *ReciPro* dans un travail académique, veuillez utiliser le lien **Cite this repository** affiché sur la page du dépôt GitHub. Les métadonnées de citation sont fournies par `CITATION.cff`, et la citation recommandée est l'article suivant :

  * [Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397-410, doi: 10.1107/S1600576722000139.](https://doi.org/10.1107/S1600576722000139)

Vous pouvez également citer le dépôt du logiciel lui-même, le cas échéant :

  * Dépôt : https://github.com/seto77/ReciPro
  * Versions : https://github.com/seto77/ReciPro/releases/latest

***

## Installation

* Téléchargez [*ReciPro-setup.msi*](https://github.com/seto77/ReciPro/releases/latest/download/ReciPro-setup.msi) (lien direct vers la dernière version) et exécutez-le. Vous le trouverez également sur la [page des versions](https://github.com/seto77/ReciPro/releases/latest). (Jusqu'à la v.4.939, l'installeur s'appelait *ReciProSetup.msi*.)
* *ReciPro* fonctionne sous Windows avec le ***.Net Desktop Runtime 10.0*** (et NON le ***.Net Runtime 10.0***), qui peut être installé [ici](https://dotnet.microsoft.com/download/dotnet/10.0).
* Si vous ne pouvez pas exécuter d'installeur (par exemple sur un PC aux droits restreints), un paquet **ZIP portable** (*ReciPro-v.X.XXX.zip*) est également disponible sur la page des versions : autonome, sans installation ni runtime .NET — il suffit de le décompresser et de le lancer.
* *ReciPro* est distribué sous **licence MIT** (libre d'utilisation, de modification et de redistribution pour tous).
* Pour l'état de la signature de code et la vérification de l'installeur, voir la [politique de signature de code](../../CODE_SIGNING.md).
* Pour les composants et données tiers inclus ou référencés, voir les [mentions relatives aux tiers](../../THIRD-PARTY-NOTICES.md).

### macOS (non officiel)

* *ReciPro* ne prend officiellement en charge que Windows, mais il a été rapporté qu'il fonctionne sous macOS (Apple Silicon) en combinant le paquet **ZIP portable** avec le wrapper Wine **Sikarugir** et le pilote OpenGL **Mesa3D** — sans licence Windows ni machine virtuelle.
* Voir le guide pas à pas publié par Ryo Fukushima (JAMSTEC) : https://github.com/Ryo-fkushima/ReciPro_macOS_memo
* Cette configuration n'est ni officiellement prise en charge ni entièrement vérifiée. Une limitation connue est que certains symboles (Å, exposants, flèches) peuvent s'afficher incorrectement.
* Ces problèmes d'affichage peuvent être corrigés en installant dans le préfixe Wine des polices à large couverture de glyphes (**DejaVu Sans/Serif**, et **Noto Sans CJK JP** pour l'interface japonaise) : ReciPro détecte l'environnement Wine et bascule automatiquement vers ces polices. Voir le [dépannage](https://seto77.github.io/ReciPro/fr/troubleshooting/) pour plus de détails.

### Remarque sur les avertissements de sécurité Windows

* Veuillez télécharger *ReciPro* uniquement depuis la page officielle GitHub Releases : https://github.com/seto77/ReciPro/releases/latest
* Sur certains systèmes Windows, Microsoft Defender SmartScreen ou Smart App Control peut afficher un avertissement avant l'exécution de l'installeur. Cela peut se produire pour un logiciel de recherche récemment compilé ou peu diffusé, et l'avertissement ne signifie pas nécessairement que l'installeur est malveillant.
* Si vous souhaitez vérifier vous-même l'installeur téléchargé, vous pouvez l'analyser avec un service multimoteur tel que VirusTotal.

## Politique de signature de code

[<img src="https://signpath.org/assets/favicon-50x50.png" alt="SignPath" height="20">](https://about.signpath.io/) Signature de code gratuite sous Windows fournie par [SignPath.io](https://about.signpath.io/), certificat délivré par la [SignPath Foundation](https://signpath.org/).

Depuis la v.4.942, les artefacts de version (l'installeur *ReciPro-setup.msi* et l'exécutable portable *ReciPro.exe*) sont signés avec Windows Authenticode dans le cadre du pipeline de publication automatisé, et chaque demande de signature est examinée et approuvée manuellement par le mainteneur avant publication. Voir [CODE_SIGNING.md](../../CODE_SIGNING.md) pour la politique complète, y compris la portée de la signature, la façon de vérifier un installeur et la procédure de signalement d'artefacts suspects.

## Confidentialité

*ReciPro* est une application de bureau locale. Elle ne collecte, ne stocke et ne transmet **aucune** donnée personnelle ou d'utilisation, et ne contient ni télémétrie ni outil d'analyse. Après installation, elle fonctionne entièrement hors ligne.

Les seules connexions réseau effectuées par *ReciPro* sont des téléchargements facultatifs, déclenchés par l'utilisateur, et aucun d'eux n'envoie vos données :

* **Rechercher les mises à jour** (commande de menu) : compare votre version installée à la dernière version publiée sur GitHub et, si vous le choisissez, télécharge le nouvel installeur depuis la page officielle [GitHub Releases](https://github.com/seto77/ReciPro/releases/latest).
* **Base de données COD** (Crystallography Open Database) : téléchargée lors de la première utilisation (~880 Mo) depuis le miroir GitHub de l'auteur, puis utilisée hors ligne.
* **Bibliothèque Intel MKL** (accélération facultative) : téléchargée (~55 Mo) depuis [nuget.org](https://www.nuget.org/) uniquement si vous activez l'option *Use MKL*, afin d'accélérer les calculs de diffraction dynamique.

La base de données AMCSD incluse et toutes les fonctions principales fonctionnent entièrement hors ligne.

## Manuel
  * Manuel en ligne (anglais / japonais) : https://seto77.github.io/ReciPro/fr/
  * Version japonaise : https://yseto.net/soft/recipro
***

## Principales fonctionnalités

### Base de données cristallographique

* **AMCSD** (American Mineralogist Crystal Structure Database) : plus de 21 000 structures cristallines intégrées, disponibles immédiatement après l'installation.
  * La base de données est fortement compressée (~5 Mo) et incluse dans le fichier d'installation, ce qui la rend utilisable hors ligne.
  * Les cristaux peuvent être recherchés par nom, composition chimique, paramètres de maille, densité, symétrie et éléments présents.
  * Référence : [Downs & Hall-Wallace, 2003, *American Mineralogist* **88**, 247-250](https://www.geo.arizona.edu/xtal/group/pdf/am88_247.pdf)
* **COD** (Crystallography Open Database) : environ 525 000 structures cristallines, y compris des cristaux organiques, sont également disponibles.
  * Téléchargée automatiquement lors de la première utilisation (~880 Mo), puis disponible hors ligne.
  * Références : [Gražulis et al., 2009, *J. Appl. Cryst.* **42**, 726-729](https://doi.org/10.1107/S0021889809016690) ; [Gražulis et al., 2012, *Nucleic Acids Res.* **40**, D420-D427](https://doi.org/10.1093/nar/gkr900)
* Import/export de fichiers aux formats CIF et AMC.

### Calculs cristallographiques

* 530 notations de groupes d'espace sont prises en charge : 230 réglages ITA standard + 300 réglages d'axes non standard.
  * Conditions générales (règles d'extinction), positions de Wyckoff et multiplicités de tous les groupes d'espace.
  * Calcul géométrique des périodicités et/ou des angles entre plans et/ou axes.
  * Génération des positions atomiques équivalentes.
  * Conversion aisée entre réglages d'axes non standard (par ex. *Pbnm* vers *Pnma*) et décalages d'origine.

### Propriétés atomiques

* Longueurs d'onde et énergies des raies X caractéristiques de <sup>1</sup>H à <sup>98</sup>Cf.
* Facteurs de diffusion atomique pour les rayons X, les électrons et les neutrons.

### Visualiseur de structure

* Visualisation 3D des structures cristallines à l'aide de l'architecture OpenGL (GLSL).
  * Rendu des atomes, liaisons, polyèdres de coordination, mailles élémentaires, plans réticulaires, surfaces limites et étiquettes de légende.
  * Même des structures complexes comportant des dizaines de milliers d'atomes sont affichées de manière fluide en temps réel.
  * Les couleurs et tailles d'atomes par défaut sont compatibles avec VESTA.
  * La zone de rendu peut être définie par des multiples de la maille élémentaire ou par les indices d'un plan cristallin et la distance au centre.
  * Des faciès cristallins arbitraires peuvent être représentés en colorant les faces limites.
  * N'importe quel plan réticulaire peut être affiché, ce qui aide les débutants à comprendre la notion de plan réticulaire dans les phénomènes de diffraction.
  * La rotation, le déplacement et le zoom se contrôlent librement à la souris.
  * Un clic sur un atome affiche les distances et les angles de liaison avec les atomes voisins.
  * L'état de rotation est immédiatement répercuté dans les autres fenêtres fonctionnelles (projection stéréographique, simulateur de diffraction, etc.).
  * L'encodeur vidéo intégré (Windows Media Foundation) permet de générer des animations de rotation (MP4 H.264/H.265) pour les présentations.

### Projection stéréographique

* Trace les plans et les axes cristallins sur une projection stéréographique.
  * Les projections équi-angulaires (canevas de Wulff) et équi-surfaces (canevas de Schmidt) sont prises en charge, avec parallèles et méridiens.
  * Les indices peuvent être spécifiés par plage numérique ou par valeurs précises.
  * Des grands cercles peuvent être affichés en spécifiant des axes de zone.
  * Les objets tracés peuvent être enregistrés ou copiés au format vectoriel afin d'être retouchés ensuite sans perte de résolution.
  * Visualisation 3D de la géométrie de la projection stéréographique à des fins pédagogiques.

### Simulateur de diffraction

* Simule les clichés de diffraction de monocristaux pour les sources de rayons X, d'électrons et de neutrons.
  * L'énergie cinétique du faisceau incident est librement configurable.
  * Les énergies des raies X caractéristiques de <sup>1</sup>H à <sup>98</sup>Cf sont intégrées.
  * La zone tracée est définie par la résolution de l'image (taille de pixel) et la longueur de caméra.
  * Les géométries de détecteur incliné sont également prises en charge.
  * La superposition d'images expérimentales est prise en charge.
  * La rotation du cristal (condition de diffraction) peut être pilotée et se synchronise immédiatement avec les autres fenêtres.

* **Diffraction polycristalline** : simulation d'anneaux de Debye pour un échantillon polycristallin.
* **Chambre de précession** (rayons X) : simulation de clichés de chambre de précession de la zone de Laue d'ordre zéro.
* **Chambre de Laue en retour** (rayons X) : simulation de clichés de Laue en retour.

#### Théorie cinématique de la diffraction
* Disponible pour toutes les sources (rayons X, électrons, neutrons).
* Les intensités diffractées sont estimées à partir du carré du module du facteur de structure et de l'erreur d'excitation.
* Les effets du facteur de Debye-Waller sur les intensités diffractées sont pris en compte.

#### Théorie dynamique de la diffraction (électrons)
* Fondée sur la **méthode des ondes de Bloch** (Bethe, 1928), qui autorise des orientations cristallines quelconques sans se limiter aux axes de zone de bas indices.
* Deux approches de calcul sont proposées :
  * **Méthode aux valeurs propres de Bethe** : diagonalisation matricielle pour les valeurs et vecteurs propres des états de Bloch. Adaptée lorsque l'on fait varier l'épaisseur de l'échantillon.
  * **Méthode de la matrice de diffusion** : calcul direct des exponentielles de matrices par la méthode de mise à l'échelle et élévation au carré avec approximation de Padé. Adaptée aux calculs rapides à épaisseur unique.
* L'algorithme le plus rapide et la meilleure bibliothèque mathématique (Eigen, Intel MKL ou Math.NET) sont sélectionnés automatiquement.
* Le potentiel d'absorption lié à la diffusion diffuse thermique (TDS) est calculé analytiquement pour de meilleures performances.

* **SAED** (diffraction électronique en aire sélectionnée) : simulation de diffraction électronique en faisceau parallèle avec effets de diffusion dynamique.
* **PED** (diffraction électronique en précession) : simule les clichés PED à partir de l'angle de précession et de la résolution angulaire azimutale. Utile pour l'analyse structurale et l'optimisation de conditions PED quasi cinématiques.
* **CBED** (diffraction électronique en faisceau convergent) : simule les clichés CBED avec demi-angle de convergence et nombre de divisions choisis par l'utilisateur. La simulation sur une gamme d'épaisseurs est prise en charge pour déterminer l'épaisseur de l'échantillon.
  * Clichés CBED moyennés en position (PACBED).
  * Simulation CBED à grand angle (LA-CBED).

### Simulateur HRTEM

* Simulation d'images de microscopie électronique en transmission haute résolution dans le même cadre théorique des ondes de Bloch.
* Les paramètres optiques (tension d'accélération, coefficient d'aberration sphérique, défocalisation, épaisseur de l'échantillon, etc.) se règlent via l'interface graphique.
* Des préréglages de paramètres optiques MET typiques sont intégrés et accessibles par un clic droit.
* Deux modèles d'imagerie pour la cohérence partielle :
  * **Théorie linéaire du transfert de contraste** : coût de calcul plus faible ; adaptée aux échantillons minces vérifiant l'approximation de l'objet de phase faible.
  * **Théorie non linéaire du transfert de contraste (modèle TCC)** : fondée sur le coefficient croisé de transmission au premier ordre (Ishizuka, 1980) ; fiable même pour des échantillons plus épais et des matériaux à numéro atomique élevé.
* La fonction de transfert de contraste avec ses enveloppes peut être tracée.
* Les séries d'images épaisseur-défocalisation peuvent être calculées simultanément.
* Le calcul s'achève typiquement en moins d'une seconde dans des conditions standard.

### Simulateur STEM

* Simulation d'images de microscopie électronique en transmission à balayage.
  * Modes d'imagerie en champ clair (BF), champ sombre annulaire (ADF) et champ sombre annulaire à grand angle (HAADF).
  * Le faisceau convergent est traité comme une superposition de nombreuses ondes planes avec un calcul précis des recouvrements.
  * Les électrons diffusés inélastiquement sont calculés à l'aide du modèle de potentiel absorbant.
  * Des séries d'images épaisseur-défocalisation peuvent être générées.

### Spot ID

* Indexation semi-automatique des taches de diffraction pour des clichés SAED expérimentaux.
* **Spot ID v1** : recherche des axes de zone à partir de la configuration géométrique (distances et angles) des taches de diffraction. Prend en charge l'analyse simultanée de 2 à 3 images.
* **Spot ID v2** : importe directement les images de clichés SAED.
  * Prend en charge les formats d'image courants : TIFF (.tif), Digital Micrograph 3/4 (.dm3, .dm4), et d'autres encore.
  * Détection et ajustement automatiques des taches de diffraction par des fonctions pseudo-Voigt 2D.
  * Recherche exhaustive des orientations cristallines compatibles avec l'arrangement des vecteurs du réseau réciproque.
  * Détermination précise même pour des axes de zone d'ordre élevé.

### Géométrie de rotation (goniomètre)

* Relie les angles d'Euler de ReciPro au goniomètre du laboratoire.
* Indique comment faire tourner le goniomètre pour atteindre l'orientation cristalline souhaitée (par ex. un axe de zone de bas indices).
* Prend en charge des définitions de goniomètre arbitraires.

### Macro

* Scripts de macro à syntaxe Python pour automatiser les tâches.
  * Exemple : faire tourner un cristal par pas de 1° et enregistrer les clichés de diffraction ou les images STEM à chaque pas.
  * Les fonctions propres à ReciPro sont disponibles dans l'espace de noms « ReciPro ».
  * Des exemples d'utilisation sont disponibles dans le [manuel](https://seto77.github.io/ReciPro/fr/20-macro/2-examples/).

### Autres fonctionnalités

* **Simulateur de parcours électronique** : simulation Monte-Carlo du parcours des électrons dans les matériaux.
* **EBSD** (diffraction d'électrons rétrodiffusés) : en cours de développement.

## Détails techniques

* Écrit en **C++**, **C#** et **OpenGL Shading Language (GLSL)**.
* Parallélisation multithread pour des calculs performants sur les processeurs multicœurs modernes.
* Toutes les fenêtres fonctionnelles se mettent à jour de façon synchrone et en temps réel lorsque l'orientation du cristal change.
* Utilise un repère cartésien direct (X : droite, Y : haut, Z : avant) avec la convention d'angles d'Euler Z–X–Z.
* Les définitions des repères sont compatibles avec les logiciels EBSD de Thermo Fisher Scientific.

### Impact académique

* **Article de logiciel évalué par les pairs :** [Seto, Y. & Ohtsuka, M. (2022), *Journal of Applied Crystallography*, **55**, 397-410](https://doi.org/10.1107/S1600576722000139).
* **Articles citants :** [articles citants sur Google Scholar](https://scholar.google.jp/scholar?cites=12625594477623342627).
* **Visibilité de l'article :** [détails Altmetric](https://www.altmetric.com/details/123778746).

| Indicateur | Valeur clé |
| --- | --- |
| Téléchargements GitHub cumulés | plus de 27 000 téléchargements |
| Citations Google Scholar | plus de 170 citations |
| Citations Dimensions | plus de 160 citations |
| Lecteurs Mendeley | plus de 90 lecteurs |

## Captures d'écran

<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormMain.png" height="320px" alt="Fenêtre principale">
<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormCrystalDatabase.png" height="320px" alt="Base de données cristallographique">
<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormSymmetryInformation.png" height="320px" alt="Informations de symétrie">
<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormBeamInteraction.png" height="320px" alt="Interaction du faisceau">
<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormStructureViewer.png" height="320px" alt="Visualiseur de structure">
<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormStereonet.png" height="320px" alt="Projection stéréographique">
<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormDiffractionSimulator.png" height="320px" alt="Simulateur de diffraction">
<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormImageSimulator.png" height="320px" alt="Simulateur HRTEM/STEM">
<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormSpotIDV2.png" height="320px" alt="Spot ID v2">
<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormMacro.png" height="320px" alt="Macro">
<img src="https://seto77.github.io/ReciPro/assets/cap-fr-auto/FormTrajectory.png" height="320px" alt="Simulateur de parcours électronique">

***

# A4.1. Symboles des groupes d'espace et diagrammes de symétrie

Cette page explique tout ce qui est affiché dans la moitié supérieure d'[Informations de symétrie](../../2-symmetry-information.md) (le panneau d'identité du groupe d'espace et les onglets **Opérations**/**Propriétés**/**Réglages**), ainsi que les deux diagrammes schématiques au bas de la fenêtre. Toute la notation suit les *International Tables for Crystallography* (ITA), Vol. A.

---

## Symboles de Hermann–Mauguin (HM)

Un symbole de Hermann–Mauguin comporte deux niveaux : le **symbole de groupe ponctuel** (cadre du haut, *Groupe ponctuel*) décrit la seule symétrie macroscopique du cristal, et le **symbole de groupe d'espace** (cadre du bas, *Groupe d'espace*) ajoute le centrage du réseau et les éventuelles composantes hélicoïdales/de glissement.

### Lettre de réseau

Le symbole de groupe d'espace commence par l'une des sept lettres de réseau standard :

| Lettre | Signification |
|---|---|
| `P` | Primitif |
| `A`, `B`, `C` | Centré sur une face (centrage de la face *bc*, *ac* ou *ab* respectivement) |
| `I` | Centré (un nœud au centre de la maille) |
| `F` | À toutes faces centrées |
| `R` | Rhomboédrique (son propre réseau trigonal ; souvent décrit en *axes hexagonaux*, la maille contenant alors trois nœuds du réseau) |

### Directions de symétrie

Après la lettre de réseau, chaque position restante du symbole correspond à une **direction de symétrie** — une direction du cristal le long de laquelle se trouve un axe de rotation/hélicoïdal, et/ou perpendiculairement à laquelle se trouve un plan miroir/de glissement. Les directions physiques auxquelles ces positions se réfèrent, et leur ordre, sont fixés par le système cristallin :

| Système cristallin | 1re position | 2e position | 3e position |
|---|---|---|---|
| Triclinique | *(aucune — seulement `1` ou `-1`)* | | |
| Monoclinique | $[010]$ (axe unique $b$, convention de ReciPro) | | |
| Orthorhombique | $[100]$ | $[010]$ | $[001]$ |
| Quadratique (tétragonal) | $[001]$ | $[100],[010]$ | $[110],[1\bar 10]$ |
| Trigonal / Hexagonal | $[001]$ | $[100],[010],[\bar 1\bar 1 0]$ | $[1\bar 10],[120],[\bar 2\bar 1 0]$ |
| Cubique | $[100],[010],[001]$ | $[111]$ *(et les 3 autres diagonales du volume)* | $[1\bar 10],[110]$ *(et les 4 autres diagonales de faces)* |

Chaque position est remplie selon les règles suivantes :

- Un nombre seul $n$ ($n=1,2,3,4,6$) : un axe de **rotation** d'ordre $n$ le long de cette direction.
- Un axe hélicoïdal $n_p$ (par ex. $2_1$, $4_2$, $6_3$) : une rotation de $360°/n$ *combinée à* une translation de $p/n$ de la période du réseau le long de l'axe. Par exemple $2_1$ (un « axe hélicoïdal binaire ») signifie tourner de $180°$ **et** se décaler d'une demi-arête de maille le long de l'axe ; $6_3$ signifie tourner de $60°$ et se décaler d'une demi-arête le long de $c$.
- Une lettre seule ($m,a,b,c,n,d$) sans nombre de rotation qui la précède : un **plan miroir ou de glissement** perpendiculaire à cette direction (la signification de la lettre est la même que pour les diagrammes, ci-dessous).
- $n/m$ ou $n_p/m$ : un axe de rotation/hélicoïdal **avec** un miroir perpendiculaire à lui (les deux éléments partagent la même direction, l'un le long de l'axe et l'autre en travers).
- $-n$ (par ex. $-1,-3,-4,-6$) : un axe de **rotoinversion** (tourner de $360°/n$, puis inverser par un point de l'axe). $-1$ seul désigne un centre d'inversion pur ; il n'existe pas d'axe « $-2$ », car une rotoinversion binaire est identique à un miroir et s'écrit donc toujours $m$.

### Symbole court et symbole complet

Le symbole HM **court** (celui que l'on cite habituellement) omet les éléments de symétrie déjà impliqués par ceux qui sont écrits ; le symbole **complet** explicite chaque direction. Par exemple, le groupe d'espace No. 62 s'écrit $Pnma$ en forme courte et $P\,2_1/n\,2_1/m\,2_1/a$ en forme complète — les trois axes hélicoïdaux $2_1$ sont impliqués par les trois plans de glissement/miroir joints au groupe ponctuel $mmm$ du groupe d'espace, si bien que le symbole court les omet. Les champs *Symbole HM (court)* et *Symbole HM (complet)* de ReciPro affichent les deux ; pour la plupart des groupes d'espace ils coïncident.

### Symboles de Schoenflies (SF) et de Hall

Le **symbole de Schoenflies** (par ex. $D_{2h}^{16}$) nomme le type de groupe ponctuel ($D_{2h}$) et ajoute un exposant qui ne fait qu'énumérer *quel* groupe d'espace de cette famille de groupe ponctuel il s'agit — contrairement au symbole HM, l'exposant ne porte par lui-même aucune signification géométrique directe ; il faut le chercher dans une table. ReciPro affiche le symbole de Schoenflies pour le groupe ponctuel comme pour le groupe d'espace.

Le **symbole de Hall** est une notation différente, fondée sur des générateurs et conçue pour un traitement informatique sans ambiguïté : il liste un ensemble minimal d'opérations génératrices accompagné d'une origine explicite, de sorte qu'un programme peut reconstruire l'ensemble exact des coordonnées sans consulter une table pour savoir « quel setting/choix d'origine ce symbole HM implique ». Un symbole de Hall n'est pas la *seule* façon possible d'encoder un ensemble d'opérations donné (des choix de générateurs différents donnent des chaînes de Hall différentes et tout aussi valables pour le même groupe), mais chacun est pleinement explicite et réversible par lui-même. ReciPro affiche un symbole de Hall généré systématiquement pour le setting actuel ; l'onglet **Réglages** (ci-dessous) liste tous les choix d'origine/setting tabulés qui partagent le numéro du groupe d'espace actuel, chacun avec ses propres symboles HM et Hall.

---

## Opérations de symétrie (onglet Opérations)

L'onglet **Opérations** liste chaque opération de symétrie de la position générale pour le setting actuel (translations de centrage du réseau déjà développées), en trois notations parallèles :

| Colonne | Exemple | Signification |
|---|---|---|
| Coordonnées | `-y, x-y, z+1/3` | Le triplet de coordonnées $(x,y,z)\mapsto(x',y',z')$, c'est-à-dire l'application affine $x'=Rx+t$ écrite algébriquement (convention ITA/CIF). |
| Seitz | `3+ [111]` | Un symbole compact : ordre et sens de la rotation/hélice (`3+`), direction de l'axe (`[111]`) et — le cas échéant — la translation de l'opération, par ex. `2₁ [001] 0,0,1/2`. Un miroir pur est `m`, l'identité `1`, l'inversion `-1`. |
| Type | `3-fold rotation (3+) [111]` | Une classification en langage clair de l'opération : `Identity`, `Inversion centre at …`, `n-fold rotation`, `nₚ screw axis`, `Mirror plane m`, un `glide plane` de type `a/b/c/n/d`, ou une `rotoinversion` d'ordre `n`, chacune avec sa direction (et, pour le centre d'inversion, sa position). |

Le bouton **Copier (CIF)** place la liste complète des opérations dans le presse-papiers sous forme de boucle CIF `_space_group_symop_operation_xyz`. Ce vocabulaire — symbole de Seitz et type géométrique — réapparaît tout au long d'[A4.2](group-subgroup-relations.md), où chaque générateur conservé/perdu d'une relation de sous-groupe est décrit de la même manière.

---

## Classification en théorie des groupes (onglet Propriétés)

L'onglet **Propriétés** rapporte un ensemble de classifications standard du groupe d'espace actuel. Certaines d'entre elles — centrosymétrique, Sohncke et polaire (et, à partir d'elles, les propriétés physiques autorisées ci-dessous) — découlent directement de la **partie matricielle** $R$ de chaque opération (la partie linéaire, de rotation ou de réflexion), jointe, pour la centrosymétrie, à la partie translation. Les autres — symorphique, partenaire énantiomorphe, famille cristalline/système réticulaire/type de Bravais, classe cristalline arithmétique et symétrie de Patterson — sont des propriétés du *type* de groupe d'espace dans son ensemble (son numéro IT, son type de réseau et sa classe de Laue) plutôt que d'une opération particulière. Rien de tout cela ne requiert de métrique (forme de la maille) — seuls comptent le contenu de symétrie abstrait et la classification du type de groupe d'espace.

**Centrosymétrique** — l'ensemble des opérations contient une opération de la forme $\{-I \mid t\}$ (une inversion par le point $t/2$, qui n'est pas nécessairement l'origine). Les propriétés de Sohncke et polaire (ci-dessous) sont mutuellement exclusives avec celle-ci : un centre d'inversion renverse toutes les directions, donc un groupe centrosymétrique ne peut jamais être polaire, et $-I$ a pour déterminant $-1$, donc un groupe centrosymétrique ne peut jamais être de Sohncke.

**Groupe de Sohncke (préservant l'orientation)** — la partie matricielle de *chaque* opération vérifie $\det R=+1$ : le groupe ne contient que des rotations propres et des rotations hélicoïdales, jamais de miroir, de glissement, d'inversion ni de rotoinversion. 65 des 230 types de groupes d'espace sont des groupes de Sohncke. Être un groupe de Sohncke est la condition de symétrie pour qu'une structure soit compatible avec des objets de chiralité définie (molécules chirales, protéines, quartz, …) sans contenir aussi leurs images miroir. C'est plus large que d'être l'un des membres d'une véritable *paire* de types de groupes d'espace images miroir l'un de l'autre — voir **Partenaire énantiomorphe**, ci-après.

**Partenaire énantiomorphe** — parmi les 65 types de Sohncke, 11 paires (22 types) ne sont reliées entre elles *que* par une transformation renversant l'orientation, et par aucune transformation propre (préservant l'orientation) : appliquer un miroir à un cristal de l'un de ces groupes d'espace le transforme en l'autre membre de la paire, jamais en lui-même quel que soit le renommage des axes. Les 11 paires sont celles bâties sur des axes hélicoïdaux de pas opposés :

$$P4_1 / P4_3,\ \ P4_122 / P4_322,\ \ P4_12_12 / P4_32_12,\ \ P3_1/P3_2,\ \ P3_112/P3_212,\ \ P3_121/P3_221,$$
$$P6_1/P6_5,\ \ P6_2/P6_4,\ \ P6_122/P6_522,\ \ P6_222/P6_422,\ \ P4_332/P4_132.$$

Les $65-22=43$ types de Sohncke restants sont leur propre image miroir (achiraux *en tant que types de groupes d'espace*, même si chaque structure individuelle qui s'y décrit reste, elle, chirale).

**Symorphique** — l'un des 73 types de groupes d'espace pour lesquels on peut choisir une origine telle que *chaque* représentant de classe latérale (modulo les translations du réseau) ait une composante de translation intrinsèque (hélicoïdale/de glissement) nulle — de façon équivalente, un point de la maille possède un groupe de symétrie de site isomorphe au groupe ponctuel entier. (Les translations de centrage, bien sûr, demeurent ; « symorphique » est un énoncé sur les parties non primitives des opérations du *groupe ponctuel*, pas sur le réseau.) Un groupe d'espace symorphique peut toujours être engendré à partir de son seul groupe ponctuel et de son seul réseau, sans axes hélicoïdaux ni plans de glissement, lorsqu'il est décrit à cette origine particulière — qui est exactement l'origine que l'ITA elle-même tabule pour un type symorphique, si bien que son symbole standard court/complet est déjà exempt de lettres hélicoïdales/de glissement. (Décrire les opérations du même groupe à une origine décalée, ou translatée d'un vecteur de centrage, peut donner à une opération individuelle l'apparence d'une translation hélicoïdale/de glissement, sans changer la classification symorphique du type — la classification demande seulement s'il existe une origine sans translation, et pour ces 73 types elle existe.)

**Polaire** — indique si une direction est laissée invariante, $Rv=v$, par la partie matricielle de *chaque* opération (pas $\pm v$ : une vraie direction polaire doit être préservée exactement, et non simplement renversée ou laissée comme axe binaire). Les cas possibles sont : **aucune** (pas de telle direction) &nbsp;/&nbsp; un seul axe $[uvw]$ &nbsp;/&nbsp; tout un plan (n'importe quelle direction de ce plan) &nbsp;/&nbsp; **n'importe quelle** direction (seulement pour le groupe ponctuel $1$). Un axe polaire est la direction le long de laquelle une polarisation électrique spontanée est autorisée par la symétrie (voir la table des propriétés physiques ci-dessous).

**Famille cristalline, système réticulaire, type de Bravais** — la hiérarchie de classification IUCr standard au-dessus du système cristallin : au total 6 **familles cristallines**, 7 **systèmes cristallins**, 7 **systèmes réticulaires** et 14 **types de réseaux de Bravais**. La subtilité est la **famille cristalline hexagonale** : comme **systèmes cristallins**, elle se scinde en *trigonal* et *hexagonal*, mais comme **systèmes réticulaires** elle se scinde différemment, en *hexagonal* et *rhomboédrique* — un groupe d'espace trigonal relève du système réticulaire hexagonal si son réseau est de type $P$, ou du système réticulaire rhomboédrique s'il est centré $R$, indépendamment de celui des deux systèmes cristallins auquel il appartient.

**Classe cristalline arithmétique** — l'appariement d'un symbole de groupe ponctuel (éventuellement résolu en directions) avec une lettre de réseau de Bravais, par ex. `4mmP` ; il existe 73 classes cristallines arithmétiques au total. Comme quelques symboles de groupe ponctuel (`3m1` vs `31m`, pour les deux façons inéquivalentes dont un groupe ponctuel $3m$ peut se placer par rapport à un réseau hexagonal) encodent déjà leur orientation par rapport au réseau, citer le symbole de groupe ponctuel orienté avec la lettre de réseau suffit à nommer la classe sans ambiguïté.

**Symétrie de Patterson** — le type de réseau joint à la *classe de Laue* (le groupe ponctuel centrosymétrique obtenu en ajoutant $-1$ au groupe ponctuel du groupe d'espace), toute information hélicoïdale/de glissement supprimée, par ex. `Pmmm` pour n'importe lequel des 30 groupes d'espace orthorhombiques à réseau $P$, qu'ils possèdent ou non des plans de glissement. C'est la symétrie de la fonction de Patterson calculée à partir des *intensités* de diffraction $|F|^2$ dans l'approximation cinématique, car $|F|^2$ est insensible au déphasage qu'introduit une translation de glissement/hélicoïdale (même si les extinctions systématiques qu'elle provoque, ainsi que les pics de Harker de la carte de Patterson, peuvent encore trahir indirectement sa présence). Pour la diffraction électronique dynamique, cette image cinématique ne tient pas exactement ; voir l'[Annexe A3](../a3-bloch-wave/index.md).

### Propriétés physiques autorisées

Les dernières lignes de l'onglet Propriétés indiquent si une propriété physique macroscopique donnée est **autorisée par la symétrie** pour le groupe ponctuel actuel — une condition nécessaire, et non la garantie que l'effet soit grand, ni même présent, dans un cristal réel (la convention du « Physical Properties of Crystals » de Nye) :

| Propriété | Condition de symétrie | Groupes ponctuels |
|---|---|---|
| Pyroélectrique / ferroélectrique | Polaire (un vecteur polaire de rang 1 — la polarisation spontanée — est autorisé) | les 10 groupes ponctuels polaires |
| Piézoélectrique | Non centrosymétrique **et** groupe ponctuel $\ne 432$ | 20 des 21 groupes ponctuels non centrosymétriques |
| Génération de seconde harmonique ($\chi^{(2)}$ dipolaire électrique de volume) | Même condition que la piézoélectricité (un tenseur polaire de rang 3) | les 20 mêmes groupes ponctuels |
| Activité optique (gyrotropie naturelle) | Les 11 groupes ponctuels ne contenant que des rotations propres, plus 4 autres qui sont gyrotropes sans être purement Sohncke | $1,2,3,4,6,222,32,422,622,23,432$ et $m,mm2,\bar4,\bar42m$ — 15 groupes ponctuels au total |

$432$ est le seul groupe ponctuel acentrique *sans* réponse piézoélectrique/SHG : il possède trop de symétrie de rotation (toutes les rotations propres, cubique) pour qu'une composante quelconque d'un tenseur polaire de rang 3 survive, bien qu'il ne soit pas centrosymétrique.

!!! note "Autorisé par la symétrie, pas nécessairement observé"
    Ces lignes énoncent ce que le groupe ponctuel *permet*. Qu'un cristal réel commute effectivement sa polarisation (vraie ferroélectricité), ou présente une réponse piézoélectrique ou SHG utilisable en pratique, dépend de la chimie et des détails structuraux au-delà de la seule symétrie.

### Onglet Réglages

Liste tous les choix d'origine/setting d'axes tabulés qui partagent le numéro IT du groupe d'espace actuel (par ex. les deux choix d'origine de $Fd\bar 3m$, ou les différents choix de maille d'un groupe monoclinique), chacun avec ses symboles HM et Hall ; la ligne du setting actuellement affiché est marquée. Cet onglet ne sert qu'à parcourir les alternatives — sélectionner une ligne ne modifie pas le cristal.

---

## Diagramme des éléments de symétrie

![Diagrammes des éléments de symétrie et des positions générales](../../../assets/cap-fr-auto/FormSymmetryInformation.splitContainer1.tableLayoutPanel1.png)

Le diagramme de gauche reproduit le diagramme de symétrie schématique de l'ITA Vol. A pour le setting actuel, projeté le long de l'axe choisi avec la commande **Direction** (`a`/`b`/`c`).

**Les axes perpendiculaires à la page** sont dessinés comme des symboles ponctuels pleins dont la forme encode l'ordre de rotation, avec de petits appendices (« ailettes ») ajoutés pour un axe hélicoïdal (leur nombre et leur disposition encodent à la fois le pas $p$ de l'hélice et sa chiralité, de sorte que par ex. $3_1$ et $3_2$ — des hélices de même ordre mais de sens opposés — sont dessinés comme des motifs d'ailettes images miroir l'un de l'autre, et non simplement avec un nombre d'ailettes différent) :

| Symbole | Élément |
|---|---|
| Lentille pleine (ovale pointu) | Axe de rotation d'ordre 2 |
| Lentille pleine avec une ailette | Axe hélicoïdal $2_1$ |
| Triangle plein | Axe de rotation d'ordre 3 |
| Triangle plein avec ailettes | Axe hélicoïdal $3_1$ / $3_2$ |
| Carré plein | Axe de rotation d'ordre 4 |
| Carré plein avec ailettes | Axe hélicoïdal $4_1$ / $4_2$ / $4_3$ |
| Hexagone plein | Axe de rotation d'ordre 6 |
| Hexagone plein avec ailettes | Axe hélicoïdal $6_1 \ldots 6_5$ |
| Petit cercle vide | Centre d'inversion ($-1$) |
| Symbole combiné vide/plein | Axe de rotoinversion ($-3,-4,-6$) |

Les axes obliques ou contenus dans la page (cela ne se produit que pour des directions spéciales telles que les diagonales du volume $\langle 111\rangle$ ou les diagonales de faces $\langle 110\rangle$ du cubique) sont dessinés comme une flèche portant le symbole ponctuel à son pied, suivant la même convention ITA.

**Les plans** sont dessinés comme des lignes dont le style nomme le type de glissement — la lettre indique la direction du réseau le long de laquelle court le vecteur de glissement (ou son caractère diagonal/en quart de maille), tandis que la question de savoir si cette translation se trouve *dans* la page ou en *sort* dépend de l'axe de projection choisi :

| Style de ligne | Plan |
|---|---|
| Ligne continue | Plan miroir $m$ |
| Tirets longs | Glissement axial $a$ ou $b$ |
| Ligne pointillée | Glissement axial $c$ (dans le cas courant où sa translation sort de la page) |
| Ligne tiret-point | Glissement diagonal $n$ |
| Ligne tiret-point avec flèche | Glissement « diamant » $d$ (translation d'un quart de maille ; n'existe que dans les mailles centrées) |
| Ligne doublée | « Double glissement » $e$ — deux vecteurs de glissement indépendants coïncident sur le même plan (n'existe que dans les mailles centrées, où un glissement et son partenaire translaté par le centrage passent par le même plan) |

Une étiquette de hauteur fractionnaire (par ex. `1/4`) à côté d'un symbole donne sa coordonnée le long de l'axe de projection chaque fois que l'élément ne se trouve pas dans le plan de hauteur 0.

!!! note "Groupes cubiques à réseau F : seul un octant est dessiné"
    Pour les groupes d'espace cubiques centrés $F$, ReciPro ne dessine que le quadrant supérieur gauche correspondant à un huitième de la maille (le diagramme serait sinon trop dense pour être lisible) ; la maille complète le répète par les translations de centrage et par les éléments de symétrie dessinés eux-mêmes. Les mêmes éléments de symétrie peuvent aussi être superposés directement au modèle 3D dans le [Visualiseur de structure](../../5-structure-viewer.md).

---

## Diagramme des positions générales

Le diagramme de droite trace les positions équivalentes générales — l'orbite d'un point générique $(x,y,z)$ sous toutes les opérations du groupe d'espace — là encore dans le style de l'ITA :

- Chaque **cercle** est la projection d'une copie du point équivalente par symétrie.
- Une **virgule** à l'intérieur d'un cercle marque une copie engendrée par une opération *de seconde espèce* (miroir, glissement, inversion ou rotoinversion) — elle a la chiralité opposée à celle d'un objet test chiral placé au point d'origine, exactement comme les paires main plane/main en miroir utilisées dans l'ITA elle-même.
- Un **cercle scindé** (moitié simple, moitié avec virgule) marque une position où une copie issue d'une opération propre et une copie issue d'une opération impropre se projettent au même point.
- Une étiquette de hauteur à côté d'un cercle (`+`, `−`, `½+`, …) donne la coordonnée de cette copie le long de l'axe de projection *par rapport au* point de référence — `+` signifie « à $z$ », `−` signifie « à $-z$ », `½+` signifie « à $z+\tfrac12$ », etc. ; ce n'est pas une hauteur absolue.
- (Groupes d'espace cubiques uniquement) de fines lignes auxiliaires relient trois cercles reliés par un axe d'ordre 3 selon une diagonale du volume $\langle111\rangle$.
- En général, un cercle (ou une moitié d'un cercle scindé) correspond à une position équivalente, si bien que le nombre de cercles égale la **multiplicité** de la position générale affichée dans l'onglet [Positions de Wyckoff](../../2-symmetry-information.md) — une vérification rapide utile pour lire l'un ou l'autre diagramme. Si l'axe de projection choisi fait coïncider exactement plusieurs copies de même chiralité, elles sont superposées en un seul endroit (distinguées uniquement par des étiquettes de hauteur séparées) au lieu d'être dessinées côte à côte, et le nombre de cercles visibles peut alors être inférieur à la multiplicité.

Les champs `numericBox` sous **Direction** permettent de déplacer le point test $(x,y,z)$ hors de la position par défaut du groupe d'espace pour ce groupe ponctuel, ce qui est parfois utile pour désencombrer un diagramme où plusieurs cercles coïncideraient.

---

## Voir aussi

- [2. Informations de symétrie](../../2-symmetry-information.md) — le guide de la GUI que cette annexe explique.
- [A4.2. Relations groupe–sous-groupe](group-subgroup-relations.md) — réutilise le vocabulaire des symboles de Seitz/types géométriques introduit ici.
- [Annexe A4. Symétrie et groupes d'espace](index.md)

# A4.1. Space-Group Symbols and Symmetry Diagrams

This page explains everything shown in the top half of [Symmetry Information](../../2-symmetry-information.md) (the space-group identity panel, and the **Operations**/**Properties**/**Settings** tabs), and the two schematic diagrams at the bottom of the window. All notation follows *International Tables for Crystallography* (ITA), Vol. A.

---

## Hermann–Mauguin (HM) symbols

A Hermann–Mauguin symbol has two layers: the **point-group symbol** (top box, *Point Group*) describes the crystal's macroscopic symmetry alone, and the **space-group symbol** (bottom box, *Space Group*) adds the lattice centring and any screw/glide components.

### Lattice letter

The space-group symbol starts with one of the seven standard lattice letters:

| Letter | Meaning |
|---|---|
| `P` | Primitive |
| `A`, `B`, `C` | One-face centred (centring on the *bc*, *ac*, or *ab* face respectively) |
| `I` | Body-centred |
| `F` | All-face centred |
| `R` | Rhombohedral (its own trigonal lattice; often described in *hexagonal axes*, in which case the cell contains three lattice points) |

### Symmetry directions

After the lattice letter, each remaining position in the symbol stands for one **symmetry direction** — a direction in the crystal along which a rotation/screw axis lies, and/or perpendicular to which a mirror/glide plane lies. Which physical directions these positions refer to, and in what order, is fixed by the crystal system:

| Crystal system | 1st position | 2nd position | 3rd position |
|---|---|---|---|
| Triclinic | *(none — only `1` or `-1`)* | | |
| Monoclinic | $[010]$ (unique axis $b$, ReciPro's convention) | | |
| Orthorhombic | $[100]$ | $[010]$ | $[001]$ |
| Tetragonal | $[001]$ | $[100],[010]$ | $[110],[1\bar 10]$ |
| Trigonal / Hexagonal | $[001]$ | $[100],[010],[\bar 1\bar 1 0]$ | $[1\bar 10],[120],[\bar 2\bar 1 0]$ |
| Cubic | $[100],[010],[001]$ | $[111]$ *(and the other 3 body diagonals)* | $[1\bar 10],[110]$ *(and the other 4 face diagonals)* |

A single position is filled according to these rules:

- A bare number $n$ ($n=1,2,3,4,6$) : an $n$-fold **rotation** axis along that direction.
- A screw axis $n_p$ (e.g. $2_1$, $4_2$, $6_3$) : a rotation of $360°/n$ *combined with* a translation of $p/n$ of the lattice repeat along the axis. For example $2_1$ (a "twofold screw") means rotate $180°$ **and** shift by half the cell edge along the axis; $6_3$ means rotate $60°$ and shift by half the cell edge along $c$.
- A bare letter ($m,a,b,c,n,d$) with no preceding rotation number : a **mirror or glide plane** perpendicular to that direction (the letter's meaning is the same as for the diagrams, below).
- $n/m$ or $n_p/m$ : a rotation/screw axis **with** a mirror perpendicular to it (the two elements share the same direction, one along the axis and one across it).
- $-n$ (e.g. $-1,-3,-4,-6$) : a **rotoinversion** axis (rotate by $360°/n$, then invert through a point on the axis). $-1$ by itself denotes a pure inversion centre; there is no such thing as a "$-2$" axis because a twofold rotoinversion is identical to a mirror, so it is always written $m$.

### Short vs. full symbol

The **short** HM symbol (the one usually quoted) omits symmetry elements that are already implied by the ones written down; the **full** symbol spells every direction out. For example, space group No. 62 is $Pnma$ in short form and $P\,2_1/n\,2_1/m\,2_1/a$ in full form — the three $2_1$ screw axes are implied by the three glide/mirror planes together with the space group's point group $mmm$, so the short symbol drops them. ReciPro's *HM symbol (short)* and *HM symbol (full)* fields show both; for most space groups they coincide.

### Schoenflies (SF) and Hall symbols

The **Schoenflies symbol** (e.g. $D_{2h}^{16}$) names the point-group type ($D_{2h}$) and adds a superscript that simply enumerates *which* space group of that point-group family this is — unlike the HM symbol, the superscript carries no direct geometric meaning by itself; you have to look it up. ReciPro shows the Schoenflies symbol for both the point group and the space group.

The **Hall symbol** is a different, generator-based notation designed for unambiguous computer processing: it lists a minimal set of generating operations together with an explicit origin, so a program can reconstruct the exact set of coordinates without consulting a lookup table for "which setting/origin choice this HM symbol implies." A Hall symbol is not the *only* possible way to encode a given operation set (different generator choices give different, equally valid Hall strings for the same group), but each one is fully explicit and reversible on its own. ReciPro shows a systematically generated Hall symbol for the current setting; the **Settings** tab (below) lists every tabulated origin/setting choice sharing the current space-group number, each with its own HM and Hall symbol.

---

## Symmetry operations (Operations tab)

The **Operations** tab lists every symmetry operation of the general position for the current setting (lattice-centring translations already expanded in), in three parallel notations:

| Column | Example | Meaning |
|---|---|---|
| Coordinates | `-y, x-y, z+1/3` | The coordinate triplet $(x,y,z)\mapsto(x',y',z')$, i.e. the affine map $x'=Rx+t$ written out algebraically (ITA/CIF convention). |
| Seitz | `3+ [111]` | A compact symbol: rotation/screw order and sense (`3+`), axis direction (`[111]`), and — if present — the operation's translation, e.g. `2₁ [001] 0,0,1/2`. A pure mirror is `m`, identity is `1`, inversion is `-1`. |
| Type | `3-fold rotation (3+) [111]` | A plain-language classification of the operation: `Identity`, `Inversion centre at …`, `n-fold rotation`, `nₚ screw axis`, `Mirror plane m`, an `a/b/c/n/d`-`glide plane`, or an `n`-fold `rotoinversion`, each with its direction (and, for the inversion centre, its position). |

The **Copy (CIF)** button places the full operation list on the clipboard as a CIF `_space_group_symop_operation_xyz` loop. This vocabulary — Seitz symbol and geometric type — reappears throughout [A4.2](group-subgroup-relations.md), where every retained/lost generator of a subgroup relation is described the same way.

---

## Group-theoretical classification (Properties tab)

The **Properties** tab reports a set of standard classifications of the current space group. Some of them — centrosymmetric, Sohncke, and polar (and, from those, the physical-property allowances below) — follow directly from each operation's **matrix part** $R$ (the linear, rotational-or-reflectional part), together with the translation part for centrosymmetric. The others — symmorphic, enantiomorphic partner, crystal family/lattice system/Bravais type, arithmetic crystal class, and Patterson symmetry — are properties of the space-group *type* as a whole (its IT number, lattice type, and Laue class) rather than of any single operation. None of this requires a metric (unit-cell shape) — it depends only on the abstract symmetry content and classification of the space-group type.

**Centrosymmetric** — the operation set contains some operation of the form $\{-I \mid t\}$ (an inversion through the point $t/2$, which need not be the origin). The Sohncke and polar properties (below) are mutually exclusive with this one: an inversion centre reverses every direction, so a centrosymmetric group can never be polar, and $-I$ has determinant $-1$, so a centrosymmetric group can never be Sohncke.

**Sohncke (orientation-preserving) group** — *every* operation's matrix part has $\det R=+1$: the group contains only proper rotations and screw rotations, never a mirror, glide, inversion, or rotoinversion. 65 of the 230 space-group types are Sohncke groups. Being a Sohncke group is the symmetry condition for a structure to be compatible with objects of definite handedness (chiral molecules, proteins, quartz, …) without also containing their mirror images. This is broader than being one member of a genuinely distinct mirror-image *pair* of space-group types — see **Enantiomorphic partner**, next.

**Enantiomorphic partner** — among the 65 Sohncke types, 11 pairs (22 types) are related to each other *only* by an orientation-reversing transformation and by no proper (orientation-preserving) one: applying a mirror to a crystal in one of these space groups turns it into the other member of the pair, never back into itself under any relabelling of axes. The 11 pairs are the ones built on opposite-handed screw axes:

$$P4_1 / P4_3,\ \ P4_122 / P4_322,\ \ P4_12_12 / P4_32_12,\ \ P3_1/P3_2,\ \ P3_112/P3_212,\ \ P3_121/P3_221,$$
$$P6_1/P6_5,\ \ P6_2/P6_4,\ \ P6_122/P6_522,\ \ P6_222/P6_422,\ \ P4_332/P4_132.$$

The remaining $65-22=43$ Sohncke types are their own mirror image (achiral *as space-group types*, even though every individual structure in them is still handed).

**Symmorphic** — one of the 73 space-group types for which an origin can be chosen such that *every* coset representative (modulo the lattice translations) has zero intrinsic (screw/glide) translation component — equivalently, some point in the cell has a site-symmetry group isomorphic to the full point group. (Centring translations, of course, remain; "symmorphic" is a statement about the non-primitive parts of the *point-group* operations, not about the lattice.) A symmorphic space group can always be generated from its point group and its lattice alone, with no screw axes or glide planes needed, when described at that particular origin — which is exactly the origin ITA itself tabulates for a symmorphic type, so its standard short/full symbol is already free of screw/glide letters. (Describing the same group's operations at a shifted or centring-translated origin can make an individual operation look like it carries a screw/glide translation, without changing the type's symmorphic classification — the classification only asks whether a translation-free origin exists at all, and for these 73 types it does.)

**Polar** — whether some direction is left invariant, $Rv=v$, by the matrix part of *every* operation (not $\pm v$: a true polar direction must be preserved exactly, not merely reversed or left as a two-fold axis). The relevant conditions are: **none** (no such direction) &nbsp;/&nbsp; a single axis $[uvw]$ &nbsp;/&nbsp; a whole plane (any direction in it) &nbsp;/&nbsp; **any** direction at all (only for point group $1$). A polar axis is the direction along which a spontaneous electrical polarisation is symmetry-allowed (see the physical-property table below).

**Crystal family, lattice system, Bravais type** — the standard IUCr classification hierarchy above the crystal system: 6 **crystal families**, 7 **crystal systems**, 7 **lattice systems**, and 14 **Bravais lattice types** in total. The subtlety is the **hexagonal crystal family**: as **crystal systems** it splits into *trigonal* and *hexagonal*, but as **lattice systems** it splits differently, into *hexagonal* and *rhombohedral* — a trigonal space group falls in the hexagonal lattice system if its lattice is $P$-type, or in the rhombohedral lattice system if it is $R$-centred, regardless of which of the two crystal systems it belongs to.

**Arithmetic crystal class** — the pairing of a (possibly direction-resolved) point-group symbol with a Bravais lattice letter, e.g. `4mmP`; there are 73 arithmetic crystal classes in total. Because a few point-group symbols (`3m1` vs. `31m`, for the two inequivalent ways a $3m$ point group can sit relative to a hexagonal lattice) already encode their orientation relative to the lattice, quoting the oriented point-group symbol together with the lattice letter is enough to name the class unambiguously.

**Patterson symmetry** — the lattice type together with the *Laue class* (the centrosymmetric point group obtained by adding $-1$ to the space group's own point group), with all screw/glide information stripped, e.g. `Pmmm` for any of the 30 orthorhombic $P$-lattice space groups regardless of which of them have glide planes. This is the symmetry of the Patterson function computed from diffraction *intensities* $|F|^2$ in the kinematical approximation, because $|F|^2$ is insensitive to the phase shift a glide/screw translation introduces (though the systematic absences it causes, and Harker peaks in the Patterson map, can still betray its presence indirectly). For dynamical electron diffraction this kinematical picture does not hold exactly; see [Appendix A3](../a3-bloch-wave/index.md).

### Physical-property allowances

The last rows of the Properties tab report whether a given macroscopic physical property is **allowed by symmetry** for the current point group — a necessary condition, not a guarantee that the effect is large or even present in a real crystal (the Nye "Physical Properties of Crystals" convention):

| Property | Symmetry condition | Point groups |
|---|---|---|
| Pyroelectric / ferroelectric | Polar (a rank-1 polar vector — spontaneous polarisation — is allowed) | the 10 polar point groups |
| Piezoelectric | Non-centrosymmetric **and** point group $\ne 432$ | 20 of the 21 non-centrosymmetric point groups |
| Second-harmonic generation (bulk electric-dipole $\chi^{(2)}$) | Same condition as piezoelectricity (a rank-3 polar tensor) | the same 20 point groups |
| Optical activity (natural gyrotropy) | The 11 point groups containing only proper rotations, plus 4 more that are gyrotropic without being purely Sohncke | $1,2,3,4,6,222,32,422,622,23,432$ and $m,mm2,\bar4,\bar42m$ — 15 point groups in total |

$432$ is the one acentric point group with *no* piezoelectric/SHG response: it has too much rotational symmetry (all proper rotations, cubic) for any rank-3 polar tensor component to survive, even though it is not centrosymmetric.

!!! note "Symmetry-allowed, not necessarily observed"
    These rows state what the point group *permits*. Whether a real crystal actually switches its polarisation (true ferroelectricity), or shows a practically useful piezoelectric or SHG response, depends on chemistry and structure details beyond symmetry alone.

### Settings tab

Lists every tabulated origin/axis-setting choice that shares the current space group's IT number (e.g. the two origin choices of $Fd\bar 3m$, or the different cell choices of a monoclinic group), each with its HM and Hall symbol; the row for the currently displayed setting is marked. This tab is for browsing the alternatives only — selecting a row does not change the crystal.

---

## Symmetry-element diagram

![Symmetry-element & general-position diagrams](../../../assets/cap-en-auto/FormSymmetryInformation.tableLayoutPanel1.png)

The left-hand diagram reproduces the ITA Vol. A schematic symmetry diagram for the current setting, projected along the axis chosen with the **Direction** (`a`/`b`/`c`) control.

**Axes perpendicular to the page** are drawn as filled point symbols whose shape encodes the rotation order, with small tails ("fins") added for a screw axis (their number and arrangement encode both the screw pitch $p$ and its handedness, so e.g. $3_1$ and $3_2$ — opposite-handed screws of the same order — are drawn as mirror-image tail patterns, not merely a different tail count):

| Symbol | Element |
|---|---|
| Filled lens (pointed oval) | 2-fold rotation axis |
| Filled lens with a fin | $2_1$ screw axis |
| Filled triangle | 3-fold rotation axis |
| Filled triangle with tails | $3_1$ / $3_2$ screw axis |
| Filled square | 4-fold rotation axis |
| Filled square with tails | $4_1$ / $4_2$ / $4_3$ screw axis |
| Filled hexagon | 6-fold rotation axis |
| Filled hexagon with tails | $6_1 \ldots 6_5$ screw axis |
| Small open circle | Inversion centre ($-1$) |
| Open/filled combined symbol | Rotoinversion axis ($-3,-4,-6$) |

Axes that run obliquely or within the page (this occurs only for special directions such as the cubic $\langle 111\rangle$ body diagonals or $\langle 110\rangle$ face diagonals) are drawn as an arrow with the point symbol at its foot, following the same ITA convention.

**Planes** are drawn as lines whose style names the glide type — the letter names which lattice direction the glide vector runs along (or that it is diagonal/quarter-cell), while whether that translation happens to lie *in* the page or run *out of* it depends on the chosen projection axis:

| Line style | Plane |
|---|---|
| Solid line | Mirror plane $m$ |
| Long dashes | Axial glide $a$ or $b$ |
| Dotted line | Axial glide $c$ (in the common case where its translation runs out of the page) |
| Dash-dot line | Diagonal glide $n$ |
| Dash-dot line with an arrow | Diamond glide $d$ (a quarter-cell translation; occurs only in centred cells) |
| Doubled line | "Double glide" $e$ — two independent glide vectors coincide on the same plane (occurs only in centred cells, where both a glide and its centring-translated partner pass through the same plane) |

A fractional height label (e.g. `1/4`) next to a symbol gives its coordinate along the projection axis whenever the element does not lie in the plane at height 0.

!!! note "F-lattice cubic groups: only one octant is drawn"
    For the $F$-centred cubic space groups, ReciPro draws only the upper-left quadrant of one-eighth of the cell (the diagram would otherwise be too dense to read); the full cell repeats it by the centring translations and by the drawn symmetry elements themselves. The same symmetry elements can also be overlaid directly on the 3-D model in the [Structure Viewer](../../5-structure-viewer.md).

---

## General-position diagram

The right-hand diagram plots the general equivalent positions — the orbit of one generic point $(x,y,z)$ under every operation of the space group — again in ITA style:

- Each **circle** is the projection of one symmetry-equivalent copy of the point.
- A **comma** inside a circle marks a copy generated by an operation *of the second kind* (a mirror, glide, inversion, or rotoinversion) — it has the opposite handedness of a chiral test object placed at the original point, exactly like the mirrored-and-plain-hand pairs used in ITA itself.
- A **split circle** (half plain, half comma) marks a position where a proper-operation copy and an improper-operation copy project onto the same point.
- A height label next to a circle (`+`, `−`, `½+`, …) gives that copy's coordinate along the projection axis *relative to* the reference point — `+` means "at $z$", `−` means "at $-z$", `½+` means "at $z+\tfrac12$", and so on; it is not an absolute height.
- (Cubic space groups only) thin auxiliary lines connect three circles that are related by a body-diagonal $\langle111\rangle$ 3-fold axis.
- In general, one circle (or one half of a split circle) corresponds to one equivalent position, so the number of circles matches the general-position **multiplicity** shown on the [Wyckoff Positions](../../2-symmetry-information.md) tab — a quick sanity check when reading either diagram. If the chosen projection axis happens to make several same-handedness copies coincide exactly, they are superimposed at one spot (distinguished only by separate height labels) rather than drawn as separate side-by-side circles, so the visible circle count can then be lower than the multiplicity.

The `numericBox` fields below **Direction** let you move the test point $(x,y,z)$ away from the space group's default position for that point group, which is occasionally useful to un-clutter a diagram where several circles would otherwise coincide.

---

## See also

- [2. Symmetry information](../../2-symmetry-information.md) — the GUI guide this appendix explains.
- [A4.2. Group–subgroup relations](group-subgroup-relations.md) — reuses the Seitz-symbol/geometric-type vocabulary introduced here.
- [Appendix A4. Symmetry and Space Groups](index.md)

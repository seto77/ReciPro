# Appendix A4. Symmetry and Space Groups

The main-window chapter [2. Symmetry information](../../2-symmetry-information.md) is a guide to the GUI: it tells you which tab shows what, and which button copies which diagram. This appendix collects the **crystallographic and group-theoretical background** behind those tables and pictures — what a Hermann–Mauguin symbol actually encodes, how to read the *International Tables for Crystallography* (ITA) Vol. A-style symmetry-element and general-position diagrams, and what the **Group Relations…** window's supergroup/subgroup tables and terminology (*translationengleiche*, *klassengleiche*, conjugacy class, domains, twin laws, …) mean.

![Symmetry Information](../../../assets/cap-en-auto/FormSymmetryInformation.png)

Two windows are covered, and the theory is best read in this order:

1. **[A4.1. Space-group symbols and symmetry diagrams](symbols-and-diagrams.md)** — the Hermann–Mauguin, Schoenflies, and Hall symbols; the group-theoretical classification shown on the **Properties** tab (centrosymmetric, Sohncke, symmorphic, polar, arithmetic crystal class, Patterson symmetry, …); the coordinate-triplet/Seitz/geometric-type description of each symmetry operation on the **Operations** tab; and the graphical conventions of the symmetry-element and general-position diagrams at the bottom of the [Symmetry Information](../../2-symmetry-information.md) window.
2. **[A4.2. Group–subgroup relations](group-subgroup-relations.md)** — what a *maximal subgroup* / *minimal supergroup* is, Hermann's *t*-/*k*- distinction, and how to read every tab of the **Group Relations…** browser (Diagram, Matrix, Orbit splitting, Domains & Twins, New reflections) opened from the **Options** panel of Symmetry Information.

A4.1 comes first because A4.2 constantly refers back to it: every subgroup/supergroup relation is itself labelled with the very same Hermann–Mauguin symbols, Seitz symbols, and geometric-type phrases (*"3-fold rotation"*, *"c-glide plane"*, *"screw axis"*, …) introduced there.

---

## Scope and sources

ReciPro's built-in database covers the 230 space-group types (with 530 tabulated settings/origin choices) exactly as tabulated in *International Tables for Crystallography*, **Volume A** (space-group symmetry) and **Volume A1** (maximal subgroups of space groups). This appendix explains ReciPro's *presentation* of that data — the notation, the diagrams, the browsing tool — and assumes the reader already has an undergraduate-level acquaintance with lattices, point groups, and the idea of a symmetry operation. It is not a substitute for ITA itself, which remains the authoritative reference for the tabulated data (and which ReciPro cannot reproduce verbatim for copyright reasons — see the **Settings** tab for ReciPro's own listing of alternative origins/settings for a given space-group type).

!!! note "Group Relations… is an actively developed feature"
    The **Group Relations…** browser (A4.2) computes *translationengleiche* (t-) and *klassengleiche* (k-, including *isomorphic*) subgroups and supergroups directly from the space group's own symmetry operations (not from a pre-tabulated list), so every relation shown is independently verified rather than copied from a table. The remaining limits — the isomorphic series is enumerated only up to index ≤ 4, and the Diagram tab draws *t*-relations only — are spelled out in A4.2's **Current limitations**.

---

## See also

- [2. Symmetry information](../../2-symmetry-information.md) — the GUI guide this appendix explains.
- [A4.1. Space-group symbols and symmetry diagrams](symbols-and-diagrams.md) · [A4.2. Group–subgroup relations](group-subgroup-relations.md)
- [Appendix A1. Coordinate systems](../a1-coordinate-system/1-orientation.md)
- [Appendix A2. Beam interaction (solid-state background)](../a2-beam-interaction/index.md) — where the space-group's reflection conditions (systematic absences) feed into the structure factor.

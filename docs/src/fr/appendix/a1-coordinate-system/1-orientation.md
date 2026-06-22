# Annexe A1.1. Système de coordonnées fondamental et orientation du cristal

<!-- 260526Cl: 図(Coordinates1-3)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

Cette page définit le **système de coordonnées fondamental (d'orientation)** de ReciPro, utilisé partout où une rotation du cristal intervient (Fenêtre principale, Visualiseur de structure, Stéréonet, Géométrie de rotation et simulation de diffraction), ainsi que la manière dont l'orientation initiale d'un cristal et la rotation par angles d'Euler sont exprimées. Le système distinct utilisé pour placer le détecteur dans le **Simulateur de diffraction** est décrit dans [A1.2. Système de coordonnées pour la simulation de diffraction](2-diffraction.md).

---

## Définition de l'orientation

ReciPro utilise un **système de coordonnées dextrogyre** fixé au moniteur :

| Axe | Direction |
|------|-----------|
| <span class="rp-red">$X$</span> | À droite du moniteur |
| <span class="rp-green">$Y$</span> | Vers le haut du moniteur |
| <span class="rp-blue">$Z$</span> | Perpendiculairement hors du moniteur, vers l'observateur |

![Axes de coordonnées de ReciPro représentés sur le moniteur](../../../assets/references/Coordinates1.png){width=400px}

La **direction du faisceau** correspond à la direction d'observation (en regardant dans le moniteur), c'est-à-dire l'axe <span class="rp-blue">$-Z$</span>.

La plupart des opérations dans ReciPro ne concernent que des *directions* (exprimées sous forme de matrices de rotation 3×3) et ne nécessitent pas d'origine explicite. La seule exception est la fonction **Simulateur de diffraction**, qui nécessite une origine explicite — voir [A1.2. Système de coordonnées pour la simulation de diffraction](2-diffraction.md).

## Orientation initiale du cristal

L'orientation initiale (au premier lancement ou après **Réinitialiser la rotation**) est définie comme suit :

1. L'axe <span class="rp-blue">$c$</span> est aligné sur l'axe <span class="rp-blue">$Z$</span>.
2. L'axe <span class="rp-green">$b$</span> se trouve dans le plan <span class="rp-green">$Y$</span><span class="rp-blue">$Z$</span>, proche de l'axe <span class="rp-green">$Y$</span>.
3. L'axe <span class="rp-red">$a$</span> est alors fixé par les axes <span class="rp-green">$b$</span> et <span class="rp-blue">$c$</span> (règle de la main droite).

![Orientation initiale : les axes a / b / c du cristal par rapport à X / Y / Z, avec le faisceau incident le long de −Z](../../../assets/references/Coordinates2.png){width=300px}

De manière équivalente :

- La direction sortant du moniteur (vers l'observateur) est l'axe de zone **[001]**.
- La direction vers la droite sur le moniteur est la normale du plan **(100)**.

> **Note :** L'axe <span class="rp-blue">$c$</span> (= [001]) coïncide toujours avec <span class="rp-blue">$Z$</span>, mais dans certains systèmes cristallins les axes <span class="rp-red">$a$</span> et <span class="rp-green">$b$</span> ne coïncident **pas** nécessairement avec <span class="rp-red">$X$</span> et <span class="rp-green">$Y$</span>.

## Angles d'Euler

L'orientation du cristal est exprimée à l'aide de trois angles d'Euler <span class="rp-olive">$\Phi$</span>, <span class="rp-cyan">$\theta$</span>, <span class="rp-magenta">$\Psi$</span>, appliqués dans l'ordre <span class="rp-blue">$Z$</span>–<span class="rp-red">$X$</span>–<span class="rp-blue">$Z$</span> (<span class="rp-magenta">$\Psi$</span>, puis <span class="rp-cyan">$\theta$</span>, puis <span class="rp-olive">$\Phi$</span>). Lorsque les trois angles sont nuls, les axes de rotation correspondants sont :

| Angle | Axe (lorsque tous les angles = 0) | Rang |
|-------|----------------------------|------|
| <span class="rp-olive">$\Phi$</span> | <span class="rp-blue">$Z$</span> | 1er (le plus élevé) |
| <span class="rp-cyan">$\theta$</span> | <span class="rp-red">$X$</span> | 2e (intermédiaire) |
| <span class="rp-magenta">$\Psi$</span> | <span class="rp-blue">$Z$</span> | 3e (le plus bas) |

![Axes de rotation des angles d'Euler — Φ (jaune), θ (cyan), Ψ (magenta) — à 0° (haut) et à 15° (bas)](../../../assets/references/Coordinates3.png){width=400px}

Les trois angles forment une **hiérarchie** : <span class="rp-olive">$\Phi$</span> est la rotation la plus élevée, suivie de <span class="rp-cyan">$\theta$</span>, puis de <span class="rp-magenta">$\Psi$</span>. La direction d'un axe inférieur dépend de l'état des rotations supérieures. Par exemple, lorsque <span class="rp-olive">$\Phi$</span> = <span class="rp-cyan">$\theta$</span> = <span class="rp-magenta">$\Psi$</span> = 15°, l'axe <span class="rp-olive">$\Phi$</span> coïncide encore avec <span class="rp-blue">$Z$</span>, mais les axes <span class="rp-cyan">$\theta$</span> et <span class="rp-magenta">$\Psi$</span> ne s'alignent en général avec aucun des axes <span class="rp-red">$X$</span>, <span class="rp-green">$Y$</span> ou <span class="rp-blue">$Z$</span>.

> La fenêtre **Géométrie de rotation** peut réexprimer cette orientation dans une convention d'angles d'Euler arbitraire, spécifique à l'expérience (par exemple pour la faire correspondre à un goniomètre de laboratoire). Voir [4. Géométrie de rotation](../../4-rotation-geometry.md).

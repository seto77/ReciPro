# Annexe A1.2. Système de coordonnées pour la simulation de diffraction

<!-- 260526Cl: 図(Coordinates4-5)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

La fonction **Simulateur de diffraction** simule le cliché de diffraction enregistré sur un détecteur. Le détecteur est un plan fini de pixels placé à une distance fixe de l'échantillon, et il peut être incliné par rapport au faisceau incident. Pour reproduire cela avec précision, il faut connaître la relation géométrique entre le détecteur et l'échantillon, ainsi que la taille des pixels et leur nombre. Pour le système de coordonnées de base (orientation), voir [A1.1. Système de coordonnées de base et orientation du cristal](1-orientation.md).

!!! note "Z et Y diffèrent du système d'orientation"
    Dans le système de coordonnées du détecteur, <span class="rp-steel">$Z$</span> est parallèle au faisceau et <span class="rp-steel">$Y$</span> pointe vers le bas. Cela diffère du système de coordonnées d'orientation, dans lequel le faisceau est dirigé selon <span class="rp-blue">$-Z$</span> et <span class="rp-green">$Y$</span> pointe vers le haut. Le système du détecteur suit la convention habituelle d'image/détecteur (origine en haut à gauche, <span class="rp-steel">$Y$</span> croissant vers le bas).

## Avant la rotation (détecteur perpendiculaire au faisceau)

![Système de coordonnées du détecteur avec le détecteur perpendiculaire au faisceau](../../../assets/references/Coordinates4.png){width=500px}

Trois systèmes de coordonnées sont définis :

- <span class="rp-steel">**Coordonnées réelles** ($X$, $Y$, $Z$)</span> : coordonnées cartésiennes 3D en mm, avec l'<span class="rp-steel">**échantillon**</span> comme origine. <span class="rp-steel">$Z$</span> est parallèle au faisceau ; vu selon <span class="rp-steel">$Z$</span>, <span class="rp-steel">$X$</span> pointe vers la droite et <span class="rp-steel">$Y$</span> vers le bas. Lorsque le détecteur est perpendiculaire au faisceau, <span class="rp-steel">$X$ / $Y$</span> sont parallèles à <span class="rp-brown">$X'$ / $Y'$</span>.
- <span class="rp-brown">**Coordonnées du détecteur** ($X'$, $Y'$)</span> : coordonnées 2D en mm sur le plan du détecteur, avec le <span class="rp-brown">**foot**</span> comme origine. <span class="rp-brown">$X'$ / $Y'$</span> pointent vers la droite / le bas sur le détecteur et sont parallèles à <span class="rp-cyan">$X''$ / $Y''$</span>.
- <span class="rp-cyan">**Coordonnées en pixels** ($X''$, $Y''$)</span> : coordonnées 2D en unités de pixels, avec le <span class="rp-cyan">**coin supérieur gauche**</span> du détecteur comme origine, suivant les lignes et colonnes de pixels du détecteur.

Lorsque le détecteur est perpendiculaire au faisceau, le <span class="rp-brown">**foot**</span> et le <span class="rp-red">**direct spot**</span> coïncident, et <span class="rp-red">**Camera length 1**</span> est égal à <span class="rp-brown">**Camera length 2**</span>.

## Après la rotation (détecteur incliné)

![Système de coordonnées du détecteur avec un détecteur incliné](../../../assets/references/Coordinates5.png){width=500px}

L'inclinaison du détecteur est décrite par deux paramètres :

| Paramètre | Description |
|-----------|-------------|
| <span class="rp-grass">$\varphi$</span> | Direction de l'<span class="rp-grass">axe de rotation</span> — son angle par rapport à l'axe <span class="rp-steel">$X$</span>, mesuré dans le plan <span class="rp-steel">$XY$</span> (<span class="rp-steel">$Z$</span> = 0) |
| <span class="rp-grass">$\tau$</span> | Angle de rotation autour de cet axe (vis à droite) |

Une fois le détecteur incliné :

- Le <span class="rp-red">**direct spot**</span> et le <span class="rp-brown">**foot**</span> ne coïncident plus.
- <span class="rp-red">**Camera length 1** ($C_1$)</span> = distance de l'<span class="rp-steel">échantillon</span> au <span class="rp-red">direct spot</span>.
- <span class="rp-brown">**Camera length 2** ($C_2$)</span> = distance de l'<span class="rp-steel">échantillon</span> au <span class="rp-brown">foot</span>.
- L'origine des <span class="rp-brown">**coordonnées du détecteur**</span> reste au <span class="rp-brown">**foot**</span> ; l'origine des <span class="rp-cyan">**coordonnées en pixels**</span> reste au <span class="rp-cyan">**coin supérieur gauche**</span>.
- Les directions <span class="rp-steel">$X$ / $Y$</span> ne coïncident plus avec <span class="rp-brown">$X'$ / $Y'$</span>.

## Glossaire des paramètres

| Terme | Définition |
|------|------------|
| <span class="rp-steel">**Échantillon (Sample)**</span> | Le matériau qui diffuse le faisceau incident ; l'origine des coordonnées réelles |
| <span class="rp-steel">**Coordonnées réelles** ($X$, $Y$, $Z$)</span> | Coordonnées 3D (mm) du montage expérimental ; origine à l'échantillon, <span class="rp-steel">$Z$</span> toujours parallèle au faisceau |
| <span class="rp-red">**Direct spot**</span> | Intersection du faisceau incident avec le détecteur |
| <span class="rp-brown">**Foot**</span> | Le pied de la perpendiculaire abaissée de l'échantillon sur le plan du détecteur ; origine des coordonnées du détecteur. Ne coïncide avec le direct spot que lorsque le détecteur est perpendiculaire au faisceau. En mode image superposée, la position du foot est définie en coordonnées en pixels |
| <span class="rp-brown">**Coordonnées du détecteur** ($X'$, $Y'$)</span> | Coordonnées 2D (mm) sur le plan du détecteur ; origine au foot |
| <span class="rp-cyan">**Coordonnées en pixels** ($X''$, $Y''$)</span> | Coordonnées 2D (pixels) sur le plan du détecteur ; origine au coin supérieur gauche |
| <span class="rp-red">**Camera length 1** ($C_1$)</span> | Distance de l'échantillon au direct spot (mm) |
| <span class="rp-brown">**Camera length 2** ($C_2$)</span> | Distance de l'échantillon au foot (mm) |
| **Pixel size** | Longueur du côté d'un pixel (carré) (mm) ; seuls les pixels carrés sont pris en charge |
| **Detector width / height** | Nombre de pixels horizontalement / verticalement |

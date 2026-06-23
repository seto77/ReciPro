# Annexe A2. Interaction du faisceau (contexte de physique du solide)

Le chapitre consacré à la fenêtre principale [3. Beam interaction](../../3-beam-interaction.md) est un guide de la GUI : il indique quels boutons presser et ce que signifie chaque colonne. Cette annexe rassemble la **physique du solide et de la diffusion** derrière ces chiffres — pourquoi un atome diffuse les rayons X, les électrons et les neutrons de façon si différente, d'où proviennent le facteur de structure et sa partie imaginaire, comment un faisceau est atténué et ralenti à l'intérieur d'un solide, et ce que l'aperçu de fluorescence représente ou non.

![Fenêtre Interaction du faisceau](../../../assets/cap-fr-auto/FormBeamInteraction.png)

La fenêtre comporte quatre onglets, et la théorie se lit le mieux dans l'ordre où une grandeur alimente la suivante :

1. **[Atomic scattering factors](scattering-factor.md)** — comment un *atome isolé* diffuse chaque type de faisceau.
2. **[Structure factor](structure-factor.md)** — comment les atomes d'une *maille* interfèrent, y compris le facteur de Debye–Waller et les règles d'extinction.
3. **[Attenuation & transport](attenuation-transport.md)** — comment le faisceau est *éliminé et ralenti* lorsqu'il traverse le matériau.
4. **[Fluorescence](fluorescence.md)** — l'émission de rayons X caractéristiques qui suit l'ionisation d'une couche interne.

---

## Géométrie de diffusion et la variable $s$

Toute grandeur de diffusion dans cette fenêtre est fonction de l'ampleur du changement de direction du faisceau. En notant $\mathbf k_i$ et $\mathbf k_s$ les vecteurs d'onde incident et diffusé (élastiques, donc $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$), le **vecteur de diffusion** et son module sont

$$\mathbf Q = 2\pi(\mathbf k_s - \mathbf k_i), \qquad Q = |\mathbf Q| = \frac{4\pi\sin\theta}{\lambda} = 4\pi s .$$

- $\theta$ : l'angle de Bragg — la *moitié* de l'angle de diffusion total. La table des réflexions indique l'angle complet $2\theta$.
- $s = \dfrac{\sin\theta}{\lambda}$ (Å⁻¹) : la variable en fonction de laquelle est tracé l'onglet **Scattering factors**. C'est l'argument naturel de tout facteur de forme atomique.
- $d$ : la distance interréticulaire. À la condition de Bragg $\lambda = 2d\sin\theta$, on a donc $s = \dfrac{1}{2d} = \dfrac{|\mathbf g|}{2}$, où $\mathbf g$ est le vecteur du réseau réciproque avec $|\mathbf g| = 1/d$.

Ces trois conventions décrivent la même géométrie ; seule l'échelle diffère. Il vaut la peine de garder la correspondance claire, car la fenêtre en utilise plus d'une :

| Dans la fenêtre | Symbole | Relation |
|---|---|---|
| Table des réflexions | $q = 2\pi/d$ | $q = 2\pi\lvert\mathbf g\rvert = Q = 4\pi s$ |
| Table des réflexions | $2\theta$ | angle de diffusion complet, $\sin\theta = \lambda s$ |
| Onglet Scattering factors | $s = \sin\theta/\lambda$ | $s = q/4\pi = 1/(2d)$ |
| Tracé du pic de diffraction | $Q = 4\pi\sin\theta/\lambda$ | $Q = q = 4\pi s$ |

!!! note "Unités"
    Les paramétrisations publiées des facteurs de forme utilisent $s$ en Å⁻¹ (donc $s^2$ en Å⁻²), tandis que ReciPro manipule en interne $s^2$ en nm⁻². Les deux diffèrent d'un facteur $100$ en $s^2$ ; les courbes et les tables sont présentées dans les unités indiquées dans l'en-tête de chaque table. Un modèle — **Kirkland** — est tabulé en fonction de $q = 2s = 1/d$ plutôt que de $s$ ; voir [Atomic scattering factors](scattering-factor.md).

### Bragg, Laue et la sphère d'Ewald {#phase-convention}

La condition de Bragg est une facette d'une seule exigence géométrique. L'interférence constructive (la **condition de Laue**) exige que le vecteur de diffusion soit égal à un vecteur du réseau réciproque,

$$\mathbf k_s = \mathbf k_i + \mathbf g, \qquad |\mathbf k_i + \mathbf g|^2 = |\mathbf k_i|^2 ,$$

ce qui, avec $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$, se réduit à

$$2\,\mathbf k_i\cdot\mathbf g + |\mathbf g|^2 = 0 \qquad\Longleftrightarrow\qquad |\mathbf g| = \frac{1}{d} = \frac{2\sin\theta}{\lambda},$$

c'est-à-dire la **loi de Bragg** $\lambda = 2d\sin\theta$. Géométriquement, c'est la construction de la **sphère d'Ewald** : une réflexion est excitée lorsque son point du réseau réciproque se trouve sur la sphère de rayon $1/\lambda$. (Ici $\mathbf g$ est en unités de $1/d$, donc $\mathbf Q = 2\pi\mathbf g$.)

---

## Convention de phase

ReciPro construit les facteurs de structure avec la convention de phase cristallographique

$$F_{\mathbf g} = \sum_j \dots \exp\!\left(-2\pi i\,\mathbf g\cdot\mathbf r_j\right),$$

c'est-à-dire un signe **moins** dans l'exposant. Ce choix fixe le signe de la partie imaginaire du facteur de structure (`F_inv` dans la table des réflexions) ainsi que la relation entre les paires de Friedel une fois la dispersion anomale activée. Elle est énoncée ici une seule fois et supposée valable dans toute l'annexe ; les conséquences sont développées dans [Structure factor](structure-factor.md).

---

## Diffusion cinématique vs dynamique

Cette annexe traite la **diffusion simple (cinématique)** : le faisceau incident est diffusé une seule fois, et l'amplitude diffractée est le facteur de structure de la page suivante. C'est la bonne image lorsque l'interaction est faible — les rayons X et les neutrons dans presque tous les échantillons, et les électrons dans des spécimens *très minces*.

Lorsque l'interaction est forte — les électrons dans tous les cristaux sauf les plus minces — le faisceau est diffusé de multiples fois avant de sortir, l'intensité est redistribuée entre les réflexions, et $\lvert F\rvert^2$ ne donne plus l'intensité mesurée. Ce régime requiert la théorie **dynamique** de l'[Appendix A3](../a3-bloch-wave/index.md). Les facteurs de diffusion et les facteurs de structure établis ici sont l'*entrée* des deux images.

Même dans la limite cinématique, l'amplitude diffractée n'est pas seulement le facteur de structure : sommer l'onde diffusée à travers une lame d'épaisseur $t$ donne

$$A_{\mathbf g}(t) \;\propto\; F_{\mathbf g}\int_0^t e^{\,2\pi i S_{\mathbf g} z}\,dz = F_{\mathbf g}\, t\, e^{\,\pi i S_{\mathbf g} t}\,\operatorname{sinc}(\pi S_{\mathbf g} t),$$

où $S_{\mathbf g}$ est l'**erreur d'excitation** — la distance entre le point du réseau réciproque et la sphère d'Ewald. L'intensité atteint un maximum prononcé en $S_{\mathbf g}=0$ et oscille avec l'épaisseur (l'origine des franges d'épaisseur) ; la théorie dynamique de l'[Appendix A3](../a3-bloch-wave/index.md) remplace ce résultat à faisceau unique par un comportement à faisceaux couplés.

---

## Les trois sondes en un coup d'œil

| | Rayon X | Électron | Neutron |
|---|---|---|---|
| Interagit avec | densité électronique $\rho_e$ | potentiel électrostatique $V$ | noyaux (et spins non appariés) |
| Force d'interaction | faible | forte | très faible |
| Pénétration typique | µm – mm | nm – µm | mm – cm |
| Diffusion simple valable ? | presque toujours | lames minces seulement | presque toujours |
| Sensibilité aux atomes légers | faible ($\propto Z$) | modérée | souvent excellente |

Ces contrastes reviennent tout au long des pages suivantes, chacun se ramenant au mécanisme de diffusion décrit dans [Atomic scattering factors](scattering-factor.md).

---

## Voir aussi

- [3. Beam interaction](../../3-beam-interaction.md) — la GUI que cette annexe explique.
- [Atomic scattering factors](scattering-factor.md) · [Structure factor](structure-factor.md) · [Attenuation & transport](attenuation-transport.md) · [Fluorescence](fluorescence.md)
- [Appendix A1. Coordinate systems](../a1-coordinate-system/1-orientation.md)
- [Appendix A3. Dynamical diffraction (Bloch-wave method)](../a3-bloch-wave/index.md) — la théorie de la diffusion multiple qui utilise ces facteurs de diffusion.

# Formation de l'image HRTEM

L'image HRTEM se forme à partir de la fonction d'onde à la surface de sortie — les coefficients de transmission $T_{\mathbf g}$ issus du [cœur dynamique](calculation.md) — en la faisant passer à travers la lentille objectif. ReciPro propose deux modèles : l'approximation rapide **quasi-cohérente** et le modèle plus rigoureux du **coefficient de transmission croisé (TCC)**. Voir aussi la page de l'interface [Simulateur HRTEM](../../9-hrtem-stem-simulator/1-hrtem-simulation.md).

---

## Symboles

| Symbole | Signification |
|--------|---------|
| $\mathbf R$ | composante X–Y dans l'espace réel (plan image) |
| $\mathbf K$ | composante X–Y du vecteur d'onde incident |
| $\mathbf G, \mathbf H$ | composantes X–Y des vecteurs du réseau réciproque |
| $\mathbf u$ | fréquence spatiale (p. ex. $\mathbf K+\mathbf G$) |
| $\chi(\mathbf u)$ | fonction d'aberration de la lentille |
| $A(\mathbf u)$ | fonction du diaphragme objectif |
| $\Delta f$ | valeur de défocalisation |
| $C_s$ | coefficient d'aberration sphérique |
| $C_c$ | coefficient d'aberration chromatique |
| $\beta$ | demi-angle d'éclairement (taille finie de la source) |
| $\Delta E$ | largeur à $1/e$ des fluctuations d'énergie de l'électron |
| $\Delta_0$ | largeur à $1/e$ de l'étalement de défocalisation (gaussien), $\Delta_0 = C_c\,\Delta E / E$ |

---

## Fonction d'aberration de la lentille et diaphragme

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{inside the objective aperture})\\[2pt] 0 & (\mathbf u\ \text{outside the objective aperture})\end{cases}$$

---

## Modèle quasi-cohérent

Une approximation rapide : chaque faisceau diffracté est modulé par le transfert de la lentille et amorti par les enveloppes de cohérence, puis sommé de manière cohérente.

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

avec les **enveloppes de cohérence temporelle** et **spatiale**

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

---

## Modèle du coefficient de transmission croisé (TCC)

Le traitement rigoureux de la cohérence partielle : chaque paire de faisceaux $(\mathbf g, \mathbf h)$ interfère par l'intermédiaire du coefficient de transmission croisé.

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

avec les enveloppes de cohérence **mixtes**

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

Dans la limite $\mathbf u' \to \mathbf u$, le TCC se réduit aux enveloppes quasi-cohérentes ci-dessus.

---

## Réduction du coût du modèle TCC

La double somme du modèle TCC évalue $\mathrm{TCC}$ une fois par paire de faisceaux, ce qui est coûteux. Comme l'intensité de l'image $I(\mathbf R)$ est réelle, le coût peut être approximativement réduit de moitié.

Premièrement, les faisceaux situés hors du diaphragme objectif ($A(\mathbf K+\mathbf G)=0$) ne contribuent pas, de sorte qu'il suffit de sommer **uniquement sur les faisceaux situés à l'intérieur du diaphragme ($A=1$)**.

Deuxièmement, le TCC est hermitien,

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

($A$ est réel ; $E_c, E_s$ sont des fonctions réelles invariantes sous $\mathbf u\leftrightarrow\mathbf u'$ ; le terme de phase $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ est conjugué complexe). Conjointement avec $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$ et $T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$, les termes $(\mathbf g,\mathbf h)$ et $(\mathbf h,\mathbf g)$ sont conjugués complexes l'un de l'autre, de sorte que leur somme vaut le double de la partie réelle :

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

La double somme se réduit donc à la diagonale plus le triangle supérieur (un seul côté, une fois qu'un ordre arbitraire est attribué aux faisceaux), réduisant de moitié le nombre d'évaluations de $\mathrm{TCC}$ :

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

Pour le terme diagonal, on a $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$, c'est-à-dire $|T_{\mathbf g}|^2$ à l'intérieur du diaphragme.

De plus, le facteur de phase $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ prend de nombreuses fois la même valeur au sein de cette somme. Le stockage et la réutilisation de ces valeurs accélèrent davantage le calcul.

---

## Voir aussi

- [Calcul dynamique (cœur commun)](calculation.md) — le cœur commun des ondes de Bloch et les coefficients de transmission $T_{\mathbf g}$
- [Annexe A3. Diffraction dynamique par la méthode des ondes de Bloch](index.md)
- [9.1. Simulation HRTEM](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)

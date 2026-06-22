# Annexe A3. Diffraction dynamique par la méthode des ondes de Bloch

Cette annexe donne un aperçu de la théorie de la diffraction électronique dynamique utilisée par les simulateurs **Simulateur de diffraction**, **CBED** et **HRTEM/STEM** de ReciPro. ReciPro suit la formulation **Bethe / ondes de Bloch**. Le calcul pas à pas (potentiel optique, coefficients de transmission, intensités) est décrit dans [Calcul dynamique (cœur commun)](calculation.md).

---

## L'équation d'onde dans un cristal

Un électron rapide qui se déplace à travers le potentiel électrostatique périodique d'un cristal obéit à l'équation de Schrödinger (de haute énergie, stationnaire), qui peut s'écrire :

$$\nabla^2 \Psi(\mathbf{r}) + 4\pi^2\left\{\, k_{vac}^2 + \sum_{\mathbf g} U_{\mathbf g}\, e^{2\pi i\,\mathbf g\cdot\mathbf r} \right\}\Psi(\mathbf{r}) = 0$$

- $k_{vac}$ : nombre d'onde de l'électron dans le vide.
- $U_{\mathbf g}$ : composante de Fourier du potentiel cristallin pour le vecteur du réseau réciproque $\mathbf g$. Comme le potentiel est périodique avec le réseau, il s'écrit sous la forme d'une série de Fourier sur le réseau réciproque.

---

## Théorème de Bloch

Comme le potentiel possède la périodicité du réseau cristallin, les solutions sont des **ondes de Bloch** :

$$\Psi(\mathbf{r}) = b\!\left(\mathbf{k}^{(j)}, \mathbf{r}\right) = u(\mathbf{r})\exp\!\left(2\pi i\,\mathbf{k}^{(j)}\cdot\mathbf{r}\right)$$

- $u(\mathbf r)$ : une fonction ayant la même périodicité que le réseau cristallin, de sorte qu'elle peut elle-même être développée sur le réseau réciproque : $u(\mathbf r)=\sum_{\mathbf g} C_{\mathbf g}^{(j)}\exp(2\pi i\,\mathbf g\cdot\mathbf r)$.
- $\mathbf{k}^{(j)}$ : le $j$-ème vecteur d'onde de Bloch.
- $C_{\mathbf g}^{(j)}$ : l'amplitude (composante du vecteur propre) du faisceau $\mathbf g$ dans la $j$-ème onde de Bloch.

---

## Équation dynamique de Bethe

L'insertion du développement en ondes de Bloch dans l'équation d'onde fournit l'**équation dynamique de Bethe** — une équation couplée pour chaque faisceau $\mathbf g$ :

$$\left[\,k^2 - \left(\mathbf{k}^{(j)} + \mathbf{g}\right)^2 + i\,U'_{g,g}\right]C_{\mathbf g}^{(j)} + \sum_{h \neq g}\left(U^C_{g-h} + i\,U'_{g,h}\right)C_{\mathbf h}^{(j)} = 0$$

- $U^C_{\mathbf g}$ : potentiel cristallin pour la diffusion **élastique**.
- $U'_{\mathbf g}$ : potentiel imaginaire (d'**absorption**), qui rend compte de la **diffusion thermique diffuse** (TDS). La manière dont il intervient, ainsi que le facteur de Debye–Waller, est détaillée dans [le cœur de calcul](calculation.md).

---

## Définitions géométriques (sphère d'Ewald)

Les vecteurs et scalaires apparaissant ci-dessus sont définis sur la sphère d'Ewald :

![Définitions des vecteurs et scalaires utilisés dans le calcul par ondes de Bloch](../../../assets/references/Bloch.png){width=500px}

- $\hat{\mathbf n}$ : vecteur unitaire normal à la surface du cristal.
- $\mathbf k$ : vecteur d'onde incident (son extrémité repose sur la sphère d'Ewald) ; $\mathbf k_{vac}$ est le vecteur d'onde dans le vide.
- $\mathbf g$ : vecteur du réseau réciproque ; $\mathbf k + \mathbf g$ pointe vers le nœud du réseau réciproque.
- $\mathbf k^{(j)}$ : le $j$-ème vecteur d'onde de Bloch. Tous les vecteurs d'onde de Bloch partagent la même composante tangentielle (continuité à travers la surface) et ne diffèrent que selon $\hat{\mathbf n}$ : $\mathbf k^{(j)} = \mathbf k + \gamma^{(j)}\hat{\mathbf n}$.
- $\gamma^{(j)}$ : la $j$-ème valeur propre (la composante de $\mathbf k^{(j)}$ selon $\hat{\mathbf n}$, mesurée à partir de $\mathbf k$).

D'après la géométrie,

$$P_g = 2\,\hat{\mathbf n}\cdot(\mathbf k + \mathbf g), \qquad Q_g = |\mathbf k|^2 - |\mathbf k + \mathbf g|^2 = -\,\mathbf g\cdot(2\mathbf k + \mathbf g)$$

et l'**erreur d'excitation** $S_g$ (l'écart du nœud du réseau réciproque à la sphère d'Ewald) ainsi que la **fonction d'évaluation** $R$ utilisée pour classer les réflexions s'écrivent :

$$S_g = \frac{\sqrt{P_g^{\,2} + 4Q_g}\; -\; P_g}{2}, \qquad R = |\mathbf g|\,Q_g^{\,2}$$

---

## Réduction à un problème aux valeurs propres

En écrivant $\mathbf{k}^{(j)} = \mathbf{k} + \gamma^{(j)}\hat{\mathbf n}$ et en utilisant $k^2-(\mathbf k+\mathbf g)^2 = Q_g$ avec la linéarisation $(\mathbf k^{(j)}+\mathbf g)^2 \approx (\mathbf k+\mathbf g)^2 + \gamma^{(j)} P_g$, l'équation de Bethe devient (après division par $P_g$) un **problème aux valeurs propres matriciel** ordinaire :

$$\mathbf{A}\,\mathbf{C} = \mathbf{C}\,\boldsymbol{\Lambda}, \qquad
A_{gh} = \frac{U^C_{\,g-h} + i\,U'_{g,h}}{P_g}\;\;(g\neq h), \qquad
A_{gg} = \frac{Q_g + i\,U'_{g,g}}{P_g}$$

- Les colonnes de $\mathbf{C}$ sont les vecteurs propres $C^{(j)}_*$ (les amplitudes des ondes de Bloch).
- $\boldsymbol{\Lambda}=\mathrm{diag}\!\left(\lambda^{(1)}, \lambda^{(2)}, \dots\right)$ contient les valeurs propres $\lambda^{(j)} = \gamma^{(j)}$.

Écrit explicitement — en ordonnant les faisceaux comme le faisceau transmis $0$, puis $g$, $h$, $\dots$ — cela donne :

$$
\begin{aligned}
&\begin{pmatrix}
(Q_0 + i\,U'_{0,0})/P_0 & (U^C_{-g} + i\,U'_{0,g})/P_0 & (U^C_{-h} + i\,U'_{0,h})/P_0 & \cdots \\
(U^C_{g} + i\,U'_{g,0})/P_g & (Q_g + i\,U'_{g,g})/P_g & (U^C_{g-h} + i\,U'_{g,h})/P_g & \cdots \\
(U^C_{h} + i\,U'_{h,0})/P_h & (U^C_{h-g} + i\,U'_{h,g})/P_h & (Q_h + i\,U'_{h,h})/P_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\begin{pmatrix}
C^{(1)}_0 & C^{(2)}_0 & C^{(3)}_0 & \cdots \\
C^{(1)}_g & C^{(2)}_g & C^{(3)}_g & \cdots \\
C^{(1)}_h & C^{(2)}_h & C^{(3)}_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix} \\[1.2ex]
&\qquad=
\begin{pmatrix}
C^{(1)}_0 & C^{(2)}_0 & C^{(3)}_0 & \cdots \\
C^{(1)}_g & C^{(2)}_g & C^{(3)}_g & \cdots \\
C^{(1)}_h & C^{(2)}_h & C^{(3)}_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\begin{pmatrix}
\lambda^{(1)} & 0 & 0 & \cdots \\
0 & \lambda^{(2)} & 0 & \cdots \\
0 & 0 & \lambda^{(3)} & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\end{aligned}
$$

La diagonalisation de $\mathbf{A}$ fournit **tous** les vecteurs d'onde de Bloch et leurs amplitudes d'un seul coup. Les amplitudes des faisceaux diffractés — et donc les intensités — découlent alors des conditions aux limites aux surfaces d'entrée et de sortie ainsi que de l'épaisseur de l'échantillon. Ces étapes, le potentiel optique (complexe), le facteur de Debye–Waller et les coefficients de transmission $T_{\mathbf g}$ sont décrits dans [Calcul dynamique (cœur commun)](calculation.md).

> **Note :** Les valeurs $V_{\mathbf g}$ affichées dans la table **Details** du simulateur de diffraction sont les valeurs brutes *avant* l'application du facteur de correction relativiste.

---

## Voir aussi

- [7. Simulateur de diffraction](../../7-diffraction-simulator/index.md) — figures de diffraction dynamique
- [9. Simulateur HRTEM/STEM](../../9-hrtem-stem-simulator/index.md)
- [Annexe A1. Systèmes de coordonnées](../a1-coordinate-system/1-orientation.md)
- [Calcul dynamique (cœur commun)](calculation.md)

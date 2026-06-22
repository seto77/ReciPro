# Calcul dynamique (noyau commun)

Les simulateurs de diffraction et d'imagerie de ReciPro partagent un **noyau commun de diffusion dynamique par ondes de Bloch (Bethe)**, décrit sur cette page (potentiel cristallin, termes de Debye–Waller et d'absorption, problème aux valeurs propres, coefficients de transmission et intensités). Les protocoles propres à chaque méthode s'appuient sur ce noyau :

- [SAED en faisceau parallèle](#parallel-beam-saed)
- [Formation de l'image HRTEM](hrtem.md)
- [CBED](cbed.md)
- [STEM](stem.md)
- [EBSD](ebsd.md)

Pour la théorie sous-jacente (équation de Schrödinger, théorème de Bloch, équation dynamique de Bethe, problème aux valeurs propres et définitions de la sphère d'Ewald), voir [Annexe A3. Diffraction dynamique par la méthode des ondes de Bloch](index.md).

---

## Constantes

$$\gamma = \frac{m}{m_0} = 1 + \frac{e_0 E}{m_0 c^2}, \qquad \beta = \frac{v}{c} = \sqrt{1 - \left(\frac{m_0}{m}\right)^2} = \sqrt{1 - \gamma^{-2}}$$

- $\gamma$ : facteur de correction relativiste ; $E$ : tension d'accélération ; $m_0$, $m$ : masse de l'électron au repos et relativiste.
- $\Omega$ : volume de la maille élémentaire.
- $k_{vac}$ : nombre d'onde de l'électron dans le vide.

---

## Potentiel cristallin pour la diffusion élastique

Le coefficient de Fourier du potentiel cristallin pour la diffusion élastique, sommé sur les atomes $k$ aux positions $\mathbf r_k$, s'écrit

$$U_{\mathbf g}^{C} = \gamma\,\frac{1}{\pi\Omega}\sum_k f_k(\mathbf g)\,\exp\!\left[2\pi i\,\mathbf g\cdot\mathbf r_k\right]T_k(\mathbf g, M_k)$$

où le **facteur de diffusion atomique** utilise une paramétrisation gaussienne $(a_i, b_i)$,

$$f_k(\mathbf g) = \sum_i a_i\exp\!\left[-b_i\,\frac{|\mathbf g|^2}{4}\right]$$

et $T_k$ est le **facteur de Debye–Waller (de température)**. Pour un facteur de température isotrope $M_k$,

$$T_k(\mathbf g, M_k) = \exp\!\left[-M_k\,\frac{|\mathbf g|^2}{4}\right]$$

et pour un tenseur de déplacement atomique anisotrope $\mathbf U$,

$$T_k(\mathbf g) = \exp\!\left[-2\pi\,\mathbf g^{t}\mathbf U\,\mathbf g\right]$$

avec la forme quadratique

$$\mathbf g^{t}\mathbf U\,\mathbf g = \begin{pmatrix} g_x & g_y & g_z\end{pmatrix}\begin{pmatrix} U_{11} & U_{12} & U_{13}\\ U_{12} & U_{22} & U_{23}\\ U_{13} & U_{23} & U_{33}\end{pmatrix}\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = g_x^2 U_{11} + g_y^2 U_{22} + g_z^2 U_{33} + 2\!\left(g_x g_y U_{12} + g_y g_z U_{23} + g_x g_z U_{13}\right)$$

Les composantes cartésiennes de $\mathbf g$ s'obtiennent à partir des vecteurs de base réciproques et des indices de Miller :

$$\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = \begin{pmatrix} a_x^{*} & b_x^{*} & c_x^{*}\\ a_y^{*} & b_y^{*} & c_y^{*}\\ a_z^{*} & b_z^{*} & c_z^{*}\end{pmatrix}\begin{pmatrix} h\\ k\\ l\end{pmatrix} = \begin{pmatrix} h\,a_x^{*} + k\,b_x^{*} + l\,c_x^{*}\\ h\,a_y^{*} + k\,b_y^{*} + l\,c_y^{*}\\ h\,a_z^{*} + k\,b_z^{*} + l\,c_z^{*}\end{pmatrix}$$

!!! note
    Les valeurs $U_{\mathbf g}$ affichées dans la table **Details** du simulateur de diffraction sont les valeurs brutes *avant* application du facteur relativiste $\gamma$.

---

## Potentiel d'absorption (diffusion thermique diffuse)

Le potentiel imaginaire (d'absorption) qui rend compte de la diffusion thermique diffuse (TDS) s'écrit

$$U'_{g,h} = \gamma\,\frac{1}{\pi\Omega}\sum_k f'_k(\mathbf g,\mathbf h)\,\exp\!\left[2\pi i(\mathbf g-\mathbf h)\cdot\mathbf r_k\right]T_k(\mathbf g-\mathbf h, M_k)$$

avec le **facteur de diffusion d'absorption**

$$f'_k(\mathbf g,\mathbf h) = \frac{2h}{\beta\, m_0\, c}\sum_i\sum_j a_i a_j\left[\frac{1}{b_i+b_j}\exp\!\left\{-\frac{b_i b_j}{b_i+b_j}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\} - \frac{1}{b_i+b_j+2M_k}\exp\!\left\{-\frac{b_i b_j - M_k^2}{b_i+b_j+2M_k}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\}\right]$$

Ici, $h$ dans le préfacteur $2h/(\beta m_0 c)$ est la **constante de Planck** (et non un indice de faisceau). Les coefficients $U^{C}$ et $U'$ sont les entrées de la matrice de structure $\mathbf A$ dans l'[Annexe A3](index.md).

---

## De la solution propre à l'intensité diffractée

La diagonalisation de la matrice de structure (voir [Annexe A3](index.md)) donne les valeurs propres $\lambda^{(j)}$ et les amplitudes des ondes de Bloch $C_{\mathbf g}^{(j)}$. Les amplitudes des ondes sur la surface de sortie — les **coefficients de transmission** $T_{\mathbf g}$ — à l'épaisseur de l'échantillon $t$ s'écrivent

$$\begin{pmatrix} T_0\\ T_g\\ T_h\\ \vdots\end{pmatrix}
= e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}
\begin{pmatrix} e^{\pi i P_0 t} & 0 & 0 & \cdots\\ 0 & e^{\pi i P_g t} & 0 & \cdots\\ 0 & 0 & e^{\pi i P_h t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} C_0^{(1)} & C_0^{(2)} & C_0^{(3)} & \cdots\\ C_g^{(1)} & C_g^{(2)} & C_g^{(3)} & \cdots\\ C_h^{(1)} & C_h^{(2)} & C_h^{(3)} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} e^{2\pi i\lambda^{(1)} t} & 0 & 0 & \cdots\\ 0 & e^{2\pi i\lambda^{(2)} t} & 0 & \cdots\\ 0 & 0 & e^{2\pi i\lambda^{(3)} t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} \alpha^{(1)}\\ \alpha^{(2)}\\ \alpha^{(3)}\\ \vdots\end{pmatrix}$$

ou, composante par composante,

$$T_{\mathbf g} = e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}\; e^{\pi i P_g t}\sum_j C_{\mathbf g}^{(j)}\,e^{2\pi i\lambda^{(j)} t}\,\alpha^{(j)}$$

- $\alpha^{(j)}$ : les coefficients de pondération (d'excitation) de chaque onde de Bloch, fixés par la condition aux limites sur la surface d'entrée.
- $t$ : épaisseur de l'échantillon.

L'intensité diffractée du faisceau $\mathbf g$ est alors

$$I_{\mathbf g} = \left|T_{\mathbf g}\right|^2$$

---

## Calcul SAED en faisceau parallèle { #parallel-beam-saed }

La SAED ordinaire (diffraction électronique en aire sélectionnée) est traitée comme une **diffraction en faisceau parallèle** avec une seule direction d'incidence. Contrairement au CBED, elle ne balaie pas de nombreux points $\mathbf K$ à l'intérieur d'une ouverture convergente. L'orientation actuelle du cristal et la tension d'accélération définissent un seul vecteur d'onde incident $\mathbf k_0$, et ReciPro évalue la position et l'intensité de chaque réflexion $\mathbf g$ pour cette condition.

Le calcul peut être organisé comme suit.

1. Utiliser l'orientation du cristal, la tension d'accélération, la longueur d'onde, la longueur de caméra et la géométrie du détecteur pour définir le vecteur d'onde incident dans le vide $\mathbf k_{vac}$ et le plan du détecteur.
2. Appliquer la correction de réfraction issue du potentiel interne moyen $U_0$ et obtenir le vecteur d'onde de référence du cristal $\mathbf k_0$.
3. Énumérer les vecteurs candidats du réseau réciproque $\mathbf g$ et évaluer leur distance à la sphère d'Ewald au moyen de grandeurs telles que $Q_g=|\mathbf k_0|^2-|\mathbf k_0+\mathbf g|^2$ et l'erreur d'excitation $S_g$.
4. Calculer l'intensité de chaque réflexion à l'aide du mode d'intensité sélectionné.
5. Projeter la direction de $\mathbf k_0+\mathbf g$ sur le plan du détecteur et la tracer comme une tache de diffraction.

Le mode SAED de ReciPro propose principalement les modèles d'intensité suivants.

| Mode | Calcul | Usage typique |
|------|-------------|-------------|
| Erreur d'excitation seule | Estime l'intensité uniquement à partir de la proximité de la réflexion à la sphère d'Ewald. Les facteurs de structure ne sont pas utilisés. | Vérifications rapides des positions des taches et de la géométrie de l'axe de zone. |
| Cinématique + erreur d'excitation | Utilise $\lvert F_{\mathbf g}\rvert^2$ conjointement avec l'amortissement par l'erreur d'excitation. La diffusion multiple n'est pas incluse. | Échantillons minces, diffraction faible et vérification des règles d'extinction. |
| Théorie dynamique | Utilise le noyau d'ondes de Bloch de cette page pour obtenir $T_{\mathbf g}(t)$ et pose $I_{\mathbf g}=\lvert T_{\mathbf g}\rvert^2$. | Dépendance en épaisseur, diffusion multiple et réflexions fortes de la diffraction électronique. |

Les modes d'affichage des nœuds du réseau réciproque, tels que les sections de sphères pleines et les taches gaussiennes, contrôlent principalement le profil de tracé. Dans le mode de théorie dynamique, l'intensité physique de la réflexion est déterminée par la valeur d'onde de Bloch $|T_{\mathbf g}|^2$, et cette intensité est ensuite attribuée au profil d'affichage choisi.

La PED peut être vue comme l'intégration de ce calcul SAED en faisceau parallèle sur les directions de précession, tandis que le CBED peut être vu comme la disposition de nombreuses directions d'incidence à l'intérieur des disques de diffraction.

---

## Potentiel interne moyen et réfraction

Lorsque l'électron entre dans le cristal depuis le vide, le potentiel interne moyen $U_0$ modifie légèrement le vecteur d'onde de référence à l'intérieur du cristal. La composante parallèle à la surface est fixée par la condition aux limites, de sorte que le vecteur d'onde dans le vide $\mathbf k_{vac}$ et le vecteur d'onde de référence du cristal $\mathbf k_0$ peuvent s'écrire

$$|\mathbf k_0|^2 = k_{vac}^2 + U_0, \qquad \mathbf k_0 = \mathbf k_{vac} + x\,\hat{\mathbf n}$$

où $x$ est la correction le long de la normale à la surface. Elle s'obtient à partir de

$$x^2 + 2(\hat{\mathbf n}\cdot\mathbf k_{vac})x - U_0 = 0$$

Ce $\mathbf k_0$ réfracté est utilisé lors de l'évaluation de $P_g$, $Q_g$, des erreurs d'excitation et de la matrice de structure $\mathbf A$ sur la [page de présentation](index.md). Le potentiel d'absorption possède également une composante $\mathbf g=\mathbf 0$, $U'_0$, qui agit comme une atténuation moyenne commune pour les ondes se propageant à travers le cristal.

---

## Sélection des faisceaux

Le calcul par ondes de Bloch ne peut pas inclure une infinité de vecteurs du réseau réciproque ; ReciPro sélectionne donc un ensemble fini de faisceaux $\{\mathbf g\}$. La grandeur de classement est

$$R_{\mathbf g}=|\mathbf g|\,Q_{\mathbf g}^{\,2}$$

et les faisceaux dont le $R_{\mathbf g}$ est plus petit sont inclus en premier. Cela privilégie les faisceaux dont les vecteurs du réseau réciproque sont courts et qui sont en même temps proches de la sphère d'Ewald.

Dans les calculs pratiques, il est important de vérifier dans quelle mesure l'intensité ou l'image change lorsque le nombre maximal d'ondes de Bloch est augmenté. Les conditions d'axe de zone fortes et les clichés CBED présentant des détails de lignes HOLZ peuvent nécessiter plusieurs centaines de faisceaux, tandis que les conditions hors axe de zone peuvent converger avec moins de faisceaux.

---

## Choix du solveur

Une fois l'ensemble fini de faisceaux choisi, ReciPro utilise principalement deux voies équivalentes pour obtenir les coefficients de transmission.

| Méthode | Caractéristique | Usage typique |
|--------|---------|-------------|
| Méthode des valeurs propres | Diagonalise la matrice de structure $\mathbf A$ et obtient les valeurs propres $\lambda^{(j)}$ et les vecteurs propres $C_{\mathbf g}^{(j)}$. La dépendance en épaisseur est ensuite évaluée via $e^{2\pi i\lambda^{(j)}t}$. | Séries en épaisseur, ainsi que calculs CBED et EBSD qui balaient de nombreuses profondeurs ou énergies |
| Méthode de l'exponentielle de matrice | Évalue directement la matrice de diffusion $\exp(2\pi i\mathbf A t)$ sans recourir explicitement à une décomposition propre. | Calculs STEM à épaisseur unique et calculs intégrés tranche par tranche |

Les deux méthodes résolvent la même équation de Bethe. Dans l'implémentation, le code choisit entre la méthode des valeurs propres, la méthode de l'exponentielle de matrice, les routines managées .NET et la bibliothèque native Eigen selon le nombre de faisceaux, le tableau d'épaisseurs et la disponibilité de la bibliothèque native.

---

## Vérifications de convergence

Pour les calculs dynamiques, vérifier que la base est suffisamment grande est aussi important que la formule elle-même. Un diagnostic utile est la variation relative lorsque le nombre de faisceaux passe de $N-\Delta N$ à $N$ :

$$\Delta I_N=\frac{|I_N-I_{N-\Delta N}|}{I_N}$$

Pour le STEM, vérifiez cela conjointement avec le réglage de l'angle du détecteur. Pour le CBED, inspectez l'intérieur des disques et les lignes HOLZ. Pour l'EBSD, comparez en outre les largeurs des bandes de Kikuchi et le fond du master pattern. Cela relie la convergence numérique aux caractéristiques physiques visibles dans le résultat simulé.

---

## Voir aussi

- [Annexe A3. Diffraction dynamique par la méthode des ondes de Bloch](index.md)
- [7.2. SAED simulation](../../7-diffraction-simulator/1-saed-simulation.md)
- [7.4. CBED simulation](../../7-diffraction-simulator/3-cbed-simulation.md)
- [7. Simulateur de diffraction](../../7-diffraction-simulator/index.md)

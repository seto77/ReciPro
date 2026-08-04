# Calcul STEM

Le calcul d'image STEM part de la même représentation de sonde convergente que [CBED](cbed.md). La différence réside dans l'observable : le CBED affiche l'intensité du disque dans le plan de diffraction, tandis que le STEM balaie la position de la sonde et intègre, à chaque position, l'intensité qui entre dans le détecteur sélectionné.

---

## Observable

Soit $\mathbf R_0$ la position de la sonde, $\mathbf Q$ la coordonnée du plan de diffraction et $t$ l'épaisseur de l'échantillon. Si la fonction de détecteur $D(\mathbf Q)$ vaut 1 à l'intérieur de la plage angulaire du détecteur et 0 à l'extérieur, l'intensité STEM élastique s'écrit

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf R_0)=
\int D(\mathbf Q)\,
\left|\psi(\mathbf Q,t;\mathbf R_0)\right|^2\,d\mathbf Q$$

BF, ABF, LAADF et HAADF correspondent à différents choix des angles interne et externe dans $D(\mathbf Q)$. Modifier l'angle du détecteur STEM change donc la grandeur physique intégrée ; il ne s'agit pas seulement d'un réglage d'affichage.

---

## Accélération par coefficients de Fourier

Une implémentation directe résoudrait à nouveau le problème dynamique pour chaque position de sonde balayée $\mathbf R_0$. L'expression de la sonde convergente possède une structure utile : la dépendance en $\mathbf R_0$ apparaît comme facteur de phase

$$\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)$$

Cela permet à ReciPro de calculer d'abord les coefficients de Fourier bidimensionnels de l'image, plutôt que de calculer $I_{\mathrm{STEM}}(\mathbf R_0)$ point par point. Conceptuellement,

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf q)=
\sum_{\mathbf g,\mathbf h}
F_{\mathbf g,\mathbf h}(t)\,
\delta(\mathbf q-\mathbf g+\mathbf h)$$

de sorte qu'une fois les coefficients $F_{\mathbf g,\mathbf h}(t)$ connus, l'image de balayage complète peut être reconstruite efficacement par une transformée de Fourier inverse.

C'est le principal avantage du STEM par ondes de Bloch pour les cristaux parfaits à petites mailles élémentaires. Cela peut être bien plus rapide que de répéter un calcul multislice à chaque position de sonde.

---

## Reconstruction d'une image réelle {#real-image-reconstruction}

L'image est reconstruite à partir des coefficients par

$$I(\mathbf r)=\sum_{\mathbf q}I(\mathbf q)\,\exp(2\pi i\,\mathbf q\cdot\mathbf r),
\qquad \mathbf q=\mathbf g-\mathbf h$$

Comme $I(\mathbf r)$ est une intensité réelle, ses coefficients doivent vérifier exactement la symétrie hermitienne,

$$I(-\mathbf q)=I(\mathbf q)^{*}$$

et l'ensemble des $\mathbf q$ engendrés par toutes les paires de faisceaux est stable par $\mathbf q\rightarrow-\mathbf q$. La somme est donc réelle par construction, et **toute partie imaginaire subsistante est une erreur numérique, non de la physique**.

En pratique, une petite partie imaginaire subsiste, car l'amplitude en $\mathbf k+\mathbf q$ est obtenue par interpolation bilinéaire sur la grille finie des directions d'incidence (voir [Échantillonnage angulaire de la sonde](#angular-sampling)). $I(-\mathbf q)$ et $I(\mathbf q)^{*}$ diffèrent alors d'une quantité d'ordre $h^{2}$, où $h$ est le pas angulaire.

En écrivant un pixel sommé sous la forme $a+ib$, la manière correcte de le ramener à une image réelle est de prendre la **partie réelle** $a$. C'est la projection orthogonale sur l'axe réel, et elle est identique à une symétrisation préalable des coefficients,

$$I_{\mathrm{sym}}(\mathbf q)=\tfrac12\left[I(\mathbf q)+I(-\mathbf q)^{*}\right]$$

suivie de la sommation. Prendre le module $\sqrt{a^{2}+b^{2}}\simeq a+b^{2}/2a$ n'est **pas** équivalent et se révèle faux de quatre façons distinctes :

- le terme supplémentaire $b^{2}/2a$ est strictement positif et ne se compense donc jamais : c'est un biais, pas du bruit ;
- il est le plus grand par rapport au signal là où $a$ est petit, c'est-à-dire dans les pixels **sombres**, et il attaque donc le contraste de l'image plutôt que son niveau global ;
- il rompt la linéarité : l'image combinée n'est plus égale à élastique + TDS, puisque $\lvert z_1+z_2\rvert\neq\lvert z_1\rvert+\lvert z_2\rvert$ ;
- il masque les pixels négatifs, qui sont le symptôme visible d'un ensemble de $\mathbf q$ insuffisant et devraient au contraire alerter l'utilisateur.

ReciPro reconstruit donc les images élastique, TDS et STEM-EDX à partir de la partie réelle, et n'écrête à zéro qu'après le flou dû à la taille de source, de sorte qu'un pixel réellement négatif reste détectable jusqu'à ce point.

!!! note
    Jusqu'à la version 4.944, les images élastique et TDS étaient sommées en module. Sur la grille angulaire par défaut, l'écart est très inférieur à tout seuil perceptible (voir le tableau ci-dessous) ; il ne devient mesurable que sur une grille volontairement grossière, et toujours sous la forme d'un léger éclaircissement des pixels sombres.

---

## Échantillonnage angulaire de la sonde {#angular-sampling}

Le cône incident est échantillonné sur une grille carrée de directions de pas $\Delta\alpha$ (**Résolution angulaire** dans les options STEM), couvrant le demi-angle de convergence $\alpha$ avec une petite marge. Le nombre de divisions le long d'un axe vaut

$$N=\left\lceil\frac{2\alpha\times1.05}{\Delta\alpha}\right\rceil$$

de sorte que le nombre de directions, et donc de problèmes aux valeurs propres à résoudre, croît comme $N^{2}$. Cette grille est sans rapport avec le nombre de points de balayage : elle discrétise les *directions à l'intérieur de la sonde*, et non les *positions de la sonde*.

Elle est aussi la seule source du résidu hermitien décrit plus haut, ce qui fait de ce résidu un indicateur de convergence commode. Les valeurs suivantes ont été mesurées pour SrTiO₃ [001] à 200 kV avec $\alpha=25$ mrad, 128 faisceaux et 32×32 points de balayage. Le « résidu » est $\max_{\mathbf q}\lvert I(\mathbf q)-I(-\mathbf q)^{*}\rvert$ rapporté à $I(\mathbf 0)$, et les deux dernières colonnes donnent l'éclaircissement que la somme en module aurait ajouté au pixel le plus brillant.

| $N$ | Directions | Résidu élastique | Résidu TDS | Biais de module, élastique | Biais de module, TDS |
|----:|-----------:|-----------------:|-------------:|------------------------:|--------------------:|
| 16  | 256    | 1.2×10⁻³ | 6.1×10⁻³ | 2.4×10⁻⁵ | 1.1×10⁻⁴ |
| 32  | 1024   | 4.1×10⁻⁴ | 2.6×10⁻³ | 1.1×10⁻⁶ | 1.3×10⁻⁵ |
| 64  | 4096   | 5.6×10⁻⁵ | 7.2×10⁻⁴ | 5.8×10⁻⁸ | 4.3×10⁻⁷ |
| 132 | 17424  | 3.8×10⁻⁵ | 1.1×10⁻⁴ | 4.2×10⁻⁸ | 3.6×10⁻⁸ |

La résolution angulaire par défaut de 0,4 mrad donne $N=132$ pour $\alpha=25$ mrad, ce qui se situe déjà dans le domaine convergé. Deux points méritent d'être notés :

- Le résidu TDS est environ un ordre de grandeur plus grand que le résidu élastique sur toutes les grilles, car les coefficients TDS portent en plus l'intégrale en épaisseur de l'absorption sélectionnée par le détecteur.
- Le résidu est un maximum sur tous les $\mathbf q$ ; il fluctue donc quelque peu d'une grille à l'autre au lieu de décroître parfaitement régulièrement, la tendance sous-jacente étant en $O(h^{2})$.

---

## TDS et absorption sélectionnée par le détecteur

En HAADF-STEM, la composante inélastique issue de la diffusion thermique diffuse (TDS) est souvent la principale source de contraste de l'image. ReciPro traite la TDS comme la quantité d'intensité retirée du canal élastique vers une plage angulaire sélectionnée, représentée par un potentiel d'absorption.

Pour une plage angulaire de détecteur $\theta_1\leq\theta\leq\theta_2$, le facteur de diffusion absorptif sélectionné par le détecteur peut s'écrire conceptuellement

$$f'_{\kappa}(\mathbf g;\theta_1,\theta_2)=
\int_{\theta_1}^{\theta_2}\sin\theta\,d\theta
\int_0^{2\pi}
\left|\Delta f_{e,\kappa}(\mathbf g,\theta,\phi)\right|^2\,d\phi$$

Choisir cette plage de manière à correspondre à un détecteur BF, ADF ou HAADF évalue la contribution TDS qui entre dans ce détecteur.

L'intensité TDS du STEM est l'intégrale sur l'épaisseur de l'absorption sélectionnée par le détecteur :

$$I_{\mathrm{STEM}}^{\mathrm{TDS}}(\mathbf R_0)=
\int_0^t
\langle\psi(z;\mathbf R_0)|\widehat W_{\mathrm{det}}|\psi(z;\mathbf R_0)\rangle\,dz$$

où $\widehat W_{\mathrm{det}}$ représente la TDS sélectionnée par le détecteur. Une fois connues les valeurs propres et les vecteurs propres des ondes de Bloch, cette intégrale en $z$ peut être traitée analytiquement. Une intégration numérique par tranches est également possible, et ReciPro utilise l'approche appropriée selon le mode de calcul.

---

## Absorption locale et non locale

Le potentiel d'absorption peut être traité de deux manières principales.

| Forme | Signification | Caractéristique |
|------|---------|---------|
| Approximation locale | Utilise un potentiel d'absorption $U'(\mathbf r)$ qui ne dépend que de la position. | Généralement efficace et rapide pour les détecteurs ADF / HAADF larges. |
| Forme non locale | Utilise $U'(\mathbf r,\mathbf r')$ ou des éléments de matrice $U'_{\mathbf g,\mathbf h}$ qui dépendent de paires d'ondes entrantes et sortantes. | Plus précise pour les détecteurs étroits, les éléments lourds ou les faibles tensions d'accélération, mais nettement plus coûteuse. |

Dans l'approximation locale, les éléments de matrice peuvent être évalués à partir de différences de vecteurs réciproques telles que $U'_{\mathbf g-\mathbf h}$. Dans la forme non locale, chaque paire $(\mathbf g,\mathbf h)$ requiert sa propre intégration angulaire, de sorte que le coût croît rapidement avec le nombre de faisceaux.

---

## Domaine d'application du STEM par ondes de Bloch

Le STEM par ondes de Bloch est rapide pour les cristaux parfaits hautement périodiques et se prête bien aux comparaisons systématiques de l'épaisseur, de la défocalisation et des angles de détecteur. Pour les défauts, les grandes supermailles ou les structures non périodiques, des méthodes telles que le multislice à phonons gelés (frozen-phonon) peuvent être plus appropriées, car elles ne reposent pas sur la même hypothèse de petite maille périodique.

Dans ReciPro, le STEM se comprend le plus simplement ainsi : on part de la même onde convergente que pour le CBED, puis on remplace l'observable du disque de diffraction par une intégration sur le détecteur dans le plan de diffraction.

---

## Paramètres pratiques

- **Angle du détecteur** : BF / ABF / ADF / HAADF sont des définitions de $D(\mathbf Q)$ et $f'_{\kappa}(\mathbf g;\theta_1,\theta_2)$.
- **Nombre de faisceaux** : Les composantes haute fréquence de l'image et la canalisation (channeling) sont sensibles au nombre de faisceaux inclus.
- **Pas d'épaisseur** : Si une intégration numérique par tranches est utilisée, vérifiez la variation lorsque l'épaisseur de tranche est divisée par deux.
- **Résolution angulaire** : Fixe la grille de directions $N$ de la sonde (voir [Échantillonnage angulaire de la sonde](#angular-sampling)). Le coût croît comme $N^{2}$, ce qui en fait le principal levier sur le temps de calcul.
- **Modèle TDS** : Pour le contraste en $Z$ HAADF, le terme TDS est aussi important que le terme élastique.

## Voir aussi

- [Calcul dynamique (cœur commun)](calculation.md)
- [Annexe A3. Diffraction dynamique par la méthode des ondes de Bloch](index.md)
- [9.2. Simulation STEM](../../9-hrtem-stem-simulator/2-stem-simulation.md)

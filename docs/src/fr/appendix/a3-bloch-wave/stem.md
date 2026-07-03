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
- **Modèle TDS** : Pour le contraste en $Z$ HAADF, le terme TDS est aussi important que le terme élastique.

## Voir aussi

- [Calcul dynamique (cœur commun)](calculation.md)
- [Annexe A3. Diffraction dynamique par la méthode des ondes de Bloch](index.md)
- [9.2. Simulation STEM](../../9-hrtem-stem-simulator/2-stem-simulation.md)

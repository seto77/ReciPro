# Calcul CBED

Le CBED (diffraction électronique en faisceau convergent) applique le [cœur dynamique](calculation.md) à de nombreuses directions du faisceau incident, puis répartit les résultats dans des disques de diffraction. La SAED possède une seule direction d'incidence ; le CBED traite chaque point à l'intérieur du diaphragme objectif comme une **onde plane incidente partielle** et résout le problème des ondes de Bloch pour chacun d'eux.

---

## Représentation du faisceau convergent

À la surface d'entrée, la sonde convergente peut s'écrire comme une somme d'ondes planes, en utilisant la position de la sonde $\mathbf R_0$, la phase de la lentille $\chi(\mathbf K)$ et la fonction d'ouverture $A(\mathbf K)$ :

$$\psi_{\mathrm{in}}(\mathbf R,0)=\sum_{\mathbf K\in\mathrm{aperture}} A(\mathbf K)\,
\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)\,
\exp[-i\chi(\mathbf K)]\,
\exp(2\pi i\,\mathbf K\cdot\mathbf R)$$

Ici, $\mathbf K$ est la composante du vecteur d'onde incident parallèle à la surface de l'échantillon. Pour un diaphragme circulaire idéal avec un demi-angle de convergence $\alpha$ et une longueur d'onde électronique $\lambda$,

$$A(\mathbf K)=
\begin{cases}
1 & (|\mathbf K|\leq \sin\alpha/\lambda)\\
0 & (|\mathbf K|> \sin\alpha/\lambda)
\end{cases}$$

Une phase de lentille représentative, utilisant la défocalisation $\Delta f$ et l'aberration sphérique $C_s$, s'écrit

$$\chi(\mathbf K)=\pi\lambda|\mathbf K|^2\Delta f+\frac{\pi}{2}C_s\lambda^3|\mathbf K|^4+\cdots$$

Dans ReciPro, cette expression est contrôlée par les réglages d'aberration, de diaphragme et d'angle de convergence.

---

## Calcul dynamique pour chaque direction

Pour le CBED, chaque $\mathbf K$ à l'intérieur du diaphragme est traité comme un faisceau incident parallèle. Le déroulement conceptuel est le suivant :

1. Déterminez le vecteur d'onde de référence réfracté $\mathbf k_0(\mathbf K)$ à partir de $\mathbf K$ et de la normale à la surface de l'échantillon.
2. Sélectionnez les faisceaux réfléchis à l'aide de la grandeur de classement $R_{\mathbf g}=|\mathbf g|Q_{\mathbf g}^2$.
3. Construisez la matrice de structure $\mathbf A$ et calculez les coefficients de transmission $T_{\mathbf g}(t;\mathbf K)$ à l'épaisseur $t$.

Il s'agit du calcul des coefficients de transmission issu du [cœur dynamique](calculation.md), répété pour chaque direction d'incidence échantillonnée. Pour une série d'épaisseurs, la solution propre d'une direction donnée peut être réutilisée et seuls les facteurs de propagation doivent être mis à jour.

---

## Assemblage des disques de diffraction

En plaçant les ondes de sortie de toutes les directions $\mathbf K$ dans le plan de diffraction, on obtient l'intensité à l'intérieur du disque transmis et des disques diffractés. Si $\mathbf Q$ est la coordonnée du plan de diffraction, le CBED moyenné en position ou les conditions de faible cohérence peuvent être approchés par une somme d'intensités incohérente :

$$I_{\mathrm{CBED}}(\mathbf Q)=
\sum_{\mathbf K\in\mathrm{aperture}}
\left|\psi_{\mathbf K}(\mathbf Q,t)\right|^2$$

Pour les modes de type LACBED, où la cohérence de phase sur une région plus étendue importe, il faut d'abord sommer les amplitudes puis prendre ensuite l'intensité.

---

## Ce que montre le CBED

Le CBED rend visible la dépendance en épaisseur de la solution des ondes de Bloch sous la forme d'une structure d'intensité à l'intérieur des disques de diffraction.

- Une modification de l'épaisseur modifie les oscillations à l'intérieur des disques, les lignes HOLZ et les franges de Kossel-Möllenstedt.
- Une modification de l'orientation incidente modifie quelles réflexions sont fortement excitées.
- Une augmentation de l'angle de convergence élargit les disques et peut révéler des recouvrements ainsi que des informations issues des zones de Laue d'ordre supérieur.

Le CBED est donc le moyen le plus direct de visualiser le résultat des ondes de Bloch sous forme de motif de disques dans le plan de diffraction. Dans ReciPro, on le comprend le mieux comme la combinaison de la discrétisation du faisceau convergent, d'une solution dynamique par direction et de la réorganisation en réseaux de disques.

---

## Paramètres pratiques

- **Nombre de faisceaux** : Les conditions d'axe de zone fortes et le détail des lignes HOLZ exigent de nombreux faisceaux réfléchis. Vérifiez comment l'intérieur des disques évolue lorsque le nombre maximal d'ondes de Bloch est augmenté.
- **Échantillonnage angulaire** : Si l'échantillonnage de $\mathbf K$ à l'intérieur du diaphragme est trop grossier, l'intensité des disques devient granuleuse. Des angles de convergence plus grands exigent un échantillonnage plus fin.
- **Épaisseur** : Les séries d'épaisseurs bénéficient de la méthode des valeurs propres, car une seule solution propre peut être réutilisée pour de nombreuses épaisseurs.
- **Cohérence** : Distinguez les conditions où une somme d'intensités incohérente suffit de celles où une sommation cohérente des amplitudes est nécessaire.

## Voir aussi

- [Calcul dynamique (cœur commun)](calculation.md)
- [Annexe A3. Diffraction dynamique par la méthode des ondes de Bloch](index.md)
- [7.4. Simulation CBED](../../7-diffraction-simulator/3-cbed-simulation.md)

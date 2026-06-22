# Calcul EBSD

L'EBSD (diffraction des électrons rétrodiffusés) utilise le même cœur Bethe / ondes de Bloch que le CBED et le STEM, mais le problème est posé différemment. Le CBED et le STEM sont des **problèmes de faisceau incident** : une onde électronique pénètre dans l'échantillon depuis l'extérieur et l'onde de sortie est calculée. L'EBSD est un **problème de direction de sortie** : les électrons qui ont subi une diffusion inélastique à l'intérieur de l'échantillon en ressortent sous forme d'électrons rétrodiffusés, et le calcul détermine quelle intensité émerge dans chaque direction externe.

ReciPro convertit ce problème de direction de sortie en un problème ordinaire de faisceau incident grâce au théorème de réciprocité. Il calcule d'abord un **master pattern** dans l'espace des directions, puis combine ce master pattern avec des poids Monte-Carlo de profondeur / énergie / direction et la géométrie du détecteur pour former le pattern du détecteur.

---

## Reformulation par le théorème de réciprocité

Si l'on calculait directement l'amplitude depuis un point source interne $\mathbf r_n$ vers une direction externe $\widehat{\mathbf s}$, un problème de diffusion distinct serait nécessaire pour chaque point source. Cela n'est pas réalisable.

Le théorème de réciprocité réécrit le problème comme suit : l'amplitude pour qu'un électron partant de $\mathbf r_n$ apparaisse dans la direction de champ lointain $\widehat{\mathbf s}$ est égale à l'amplitude, en $\mathbf r_n$, d'une onde réciproque incidente depuis la direction externe $-\widehat{\mathbf s}$. Cette onde réciproque est une solution Bethe / ondes de Bloch ordinaire. En la notant $\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r)$, l'intensité EBSD dans la direction $\widehat{\mathbf s}$ peut s'écrire

$$I_{\mathrm{EBSD}}(\widehat{\mathbf s};E,z)\propto
\sum_n \sigma_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n;E,z)\right|^2$$

où $\sigma_n(E,z)$ est le poids de la diffusion inélastique au voisinage de la position atomique $\mathbf r_n$ vers le canal de rétrodiffusion à l'énergie $E$ et à la profondeur $z$. Les termes sources sont additionnés en tant qu'intensités, et non en tant que somme cohérente d'amplitudes, car on suppose que la diffusion inélastique détruit la relation de phase entre les différentes positions sources.

---

## Master pattern

Le master pattern EBSD stocke la part de diffraction dynamique propre au cristal de l'expression ci-dessus sur une grille de directions. Conceptuellement,

$$M(\widehat{\mathbf s};E,z)=
\sum_n w_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n)\right|^2$$

où $w_n$ est le poids de source inélastique du côté cristal à la position atomique $\mathbf r_n$. ReciPro utilise le poids empirique

$$w_n \propto Z_n^{1.7}\,\mathrm{occ}_n$$

avec le numéro atomique $Z_n$ et l'occupation $\mathrm{occ}_n$. Cela est distinct de la distribution de profondeur / énergie de transport produite par Monte Carlo.

Dans l'implémentation, l'onde de Bloch réciproque est évaluée à chaque position atomique :

$$\beta_n^{(j)}=
\alpha^{(j)}
\sum_{\mathbf g}C_{\mathbf g}^{(j)}
\exp\!\left[2\pi i(\mathbf k^{(j)}+\mathbf g)\cdot\mathbf r_n\right]$$

Le code forme ensuite la matrice de paires d'ondes de Bloch

$$S_{jj'}=\sum_n w_n\,\beta_n^{(j)}\,\overline{\beta_n^{(j')}}$$

et l'intégrale d'épaisseur analytique

$$\mathcal F_{jj'}(t)=
\frac{\exp\!\left[2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})t\right]-1}
{2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})}$$

de sorte que le master pattern est évalué comme

$$M(\widehat{\mathbf s};E,t)=
\mathrm{Re}\left\{\sum_{j,j'}S_{jj'}(E)\,\mathcal F_{jj'}(t)\right\}$$

Dans le cas limite dégénéré où le dénominateur est proche de zéro, $\mathcal F_{jj'}(t)\to t$.

---

## Échantillonnage de l'espace des directions

Le master pattern n'est pas l'image du détecteur elle-même ; c'est une distribution d'intensité dans l'espace des directions lié au cristal. ReciPro échantillonne cet espace des directions avec une projection équivalente Rosca-Lambert et stocke les hémisphères $+Z$ et $-Z$ comme des tableaux plans distincts. L'échantillonnage à aire égale réduit le biais de densité entre les pôles et l'équateur.

À ce stade, le master pattern dépend de la structure cristalline, de la tension d'accélération, de la profondeur, de l'énergie et du modèle d'absorption. La géométrie du détecteur, telle que le centre du pattern et la position de l'écran, n'a pas encore été appliquée.

---

## Poids Monte-Carlo et pattern du détecteur

Pour obtenir un pattern de détecteur EBSD proche de l'observable expérimentale, le master pattern doit être pondéré par le nombre d'électrons rétrodiffusés émergeant de chaque profondeur, énergie et direction. En écrivant ce poids de transport sous la forme

$$W(E,z;\widehat{\mathbf s})$$

et en utilisant $\widehat{\mathbf s}(\mathbf p)$ pour la direction liée au cristal correspondant au pixel détecteur $\mathbf p$, le pattern final du détecteur est

$$P(\mathbf p)=
\sum_{i,j}
W(E_i,z_j;\widehat{\mathbf s}(\mathbf p))\,
M(\widehat{\mathbf s}(\mathbf p);E_i,z_j)$$

sous forme de somme discrète sur l'énergie et la profondeur.

La partie Monte-Carlo suit la diffusion élastique, la diffusion inélastique, la perte d'énergie et l'échappement à travers la surface de l'échantillon. Pour les électrons rétrodiffusés, elle construit des distributions de profondeur, d'énergie et de direction de sortie. ReciPro distingue les modèles qui utilisent la dernière position de diffusion inélastique et l'énergie immédiatement après celle-ci comme source effective, des modèles qui utilisent la profondeur d'échappement et l'énergie d'échappement.

---

## Fond TDS et modèle d'absorption

Les patterns EBSD contiennent non seulement la structure géométrique des bandes de Kikuchi, mais aussi un fond lisse provenant de la diffusion thermique diffuse (TDS). Lorsque `IncludeTDSBackground` est activé, ReciPro évalue la composante TDS diffusée dans l'hémisphère arrière,

$$\pi/2\leq\theta\leq\pi$$

comme une matrice d'absorption $\mu_{\mathrm{back}}$ et ajoute l'intensité de fond en utilisant la même sommation de paires d'ondes de Bloch que pour le master pattern. Comme la même solution propre est réutilisée, le fond TDS ajoute relativement peu de coût supplémentaire.

Lorsque `UseNonLocalAbsorption` est activé, le potentiel absorbant n'est pas traité simplement comme $U'_{\mathbf g-\mathbf h}$, mais sous une forme non locale qui dépend de la direction et des paires de faisceaux. Cela peut améliorer la précision, mais nécessite aussi de reconstruire la matrice d'absorption pour les directions de la grille du master pattern, ce qui peut augmenter considérablement le temps de calcul.

---

## Paramètres pratiques

- **Nombre de faisceaux** : Un nombre de faisceaux trop faible fait perdre le détail des bandes de Kikuchi et la structure des bandes HOLZ. Les axes de zone de bas indices peuvent nécessiter plusieurs centaines de faisceaux.
- **Tableaux de profondeur et d'énergie** : S'ils sont plus grossiers que l'échelle de variation du poids Monte-Carlo $W(E,z;\widehat{\mathbf s})$, les largeurs de bande dépendant de l'énergie et les effets de profondeur de canalisation sont moyennés.
- **Géométrie du détecteur** : Le centre du pattern, la distance à l'écran et l'inclinaison de l'échantillon déterminent l'application $\widehat{\mathbf s}(\mathbf p)$, de sorte que le pattern du détecteur peut changer même lorsque le master pattern reste inchangé.
- **Interprétation par réciprocité** : Le master pattern n'est pas l'image du détecteur. Il ne devient un pattern de détecteur qu'après la pondération Monte-Carlo et la projection sur le détecteur.
- **Fond TDS** : Activez-le pour des comparaisons quantitatives de contraste de bande. Désactivez-le lorsque la structure géométrique de Kikuchi est plus facile à inspecter sans le fond lisse.

## Voir aussi

- [Calcul dynamique (cœur commun)](calculation.md)
- [Annexe A3. Diffraction dynamique par la méthode des ondes de Bloch](index.md)
- [12. Simulation EBSD](../../12-ebsd-simulation.md)

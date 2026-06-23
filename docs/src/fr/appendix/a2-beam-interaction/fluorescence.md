# Fluorescence

Lorsque la **photoabsorption** des rayons X éjecte un électron d'une couche interne (voir [atténuation & transport](attenuation-transport.md)), elle laisse une lacune dans un niveau profond. L'atome se relaxe en faisant tomber un électron externe dans le trou, et l'énergie libérée ressort soit sous la forme d'un **photon X caractéristique** (fluorescence), soit par l'éjection d'un second électron (le processus **Auger**). L'onglet **Fluorescence** affiche un aperçu du canal des photons caractéristiques ; il ne s'applique qu'aux rayons X et est masqué pour les faisceaux d'électrons et de neutrons.

![Fluorescence (X-ray)](../../../assets/cap-fr-auto/FormBeamInteraction-xray-fluorescence.png)

---

## Raies caractéristiques

Comme les énergies des couches sont nettement définies, l'énergie du photon émis est la **différence de deux énergies de liaison**,

$$E_\gamma = E_B(\text{inner shell}) - E_B(\text{outer shell}),$$

et elle est donc caractéristique de l'élément :

- **Raies K** — lacune dans la couche $K$ comblée depuis $L$ ($K\alpha$) ou $M$ ($K\beta$).
- **Raies L** — lacune dans la couche $L$ comblée depuis $M$/$N$ ($L\alpha$, $L\beta$, …).

Seules apparaissent les transitions autorisées par les règles de sélection dipolaires, ce qui explique pourquoi le spectre est constitué de quelques raies discrètes (K$\alpha_1$, K$\alpha_2$, K$\beta_1$, L$\alpha_1$, …) plutôt que d'un continuum. Leurs énergies suivent la **loi de Moseley** ; dans l'approximation hydrogénoïde écrantée,

$$E_{n_2\to n_1} \approx R_\infty hc\,(Z-\sigma)^2\left(\frac{1}{n_1^2} - \frac{1}{n_2^2}\right), \qquad \text{so}\qquad \sqrt{E} \propto (Z-\sigma),$$

avec $\sigma$ une constante d'écran. Pour $K\alpha$ ($n_2{=}2\to n_1{=}1$, $\sigma\approx1$) ceci se réduit à $E_{K\alpha}\approx R_\infty hc\,(Z-1)^2\left(1-\tfrac14\right)$. Cette dépendance en $Z$ monotone et pilotée par le nombre d'électrons est la base de l'identification élémentaire (EDX/WDX).

---

## Rendement de fluorescence

La compétition entre la relaxation radiative et la relaxation Auger est décrite par le **rendement de fluorescence**

$$\omega = \frac{\Gamma_r}{\Gamma_r + \Gamma_a},$$

la probabilité qu'une lacune donnée se désexcite en émettant un photon plutôt qu'un électron Auger ($\Gamma_r$, $\Gamma_a$ sont respectivement le taux radiatif et le taux Auger).

- Pour les **éléments légers**, le canal Auger domine, de sorte que $\omega_K$ est petit (bien en dessous de 1 % pour C, N, O) — les éléments légers fluorescent faiblement, c'est pourquoi ils sont difficiles à détecter par EDX.
- Pour les **éléments lourds**, le canal radiatif l'emporte et $\omega_K \to$ proche de 1.

Le **rendement Auger** complémentaire $a$ prend le reste, de sorte que

$$\omega + a = 1 ,$$

et un $\omega$ petit signifie que la plupart des lacunes se désexcitent par émission Auger. Au sein du canal radiatif, la part d'une raie particulière $\ell$ (par exemple $K\alpha_1$ contre $K\beta_1$) est son **rapport de branchement**

$$p_{\ell\mid X} = \frac{\Gamma_\ell}{\sum_{\ell'\in X}\Gamma_{\ell'}},$$

le taux radiatif relatif au sein de la couche $X$. ReciPro indique $\omega_K$ pour chaque élément ainsi que la raie la plus intense du spectre.

---

## Ce que l'aperçu modélise et ne modélise pas

Le tracé des **raies d'émission EDX** dessine chaque raie caractéristique comme un bâton à son énergie de photon, avec une hauteur proportionnelle à

$$\text{(atomic fraction)} \times \text{(radiative rate)} \times \omega.$$

Il s'agit d'un aperçu **qualitatif** de l'emplacement des raies et de leurs hauteurs relatives approximatives. Il omet délibérément les facteurs qu'exige un spectre EDX/XRF réel et quantitatif :

- si l'énergie incidente est réellement **au-dessus du seuil d'absorption** nécessaire pour créer la lacune — une raie est tracée même si elle ne peut pas être excitée à l'énergie courante ;
- la **section efficace d'excitation** (l'efficacité avec laquelle le faisceau incident crée la lacune à l'énergie choisie) ;
- l'**auto-absorption** des photons émis au sein de l'échantillon (effets de matrice) ;
- l'**efficacité du détecteur** et sa résolution.

L'aperçu sert donc à l'identification des raies et au raisonnement sur les positions relatives, et non à la détermination quantitative de la composition.

---

## De l'aperçu à la quantification

Une analyse EDX/XRF réelle convertit les intensités des raies en concentrations au moyen d'une **correction de matrice (ZAF)** — pour le numéro atomique ($Z$), l'absorption ($A$) des photons émis sur leur chemin de sortie de l'échantillon, et la **fluorescence** secondaire ($F$) excitée par d'autres raies — combinée à la section efficace d'excitation et à la réponse du détecteur évoquées ci-dessus. Sous sa forme complète, l'intensité mesurée de la raie $\ell$ de l'élément $i$ est

$$I_\ell \;\propto\; C_i\,\Phi_0\,\sigma_{\text{ion},X,i}(E_0)\,\omega_{X,i}\,p_{\ell\mid X}\,\epsilon(E_\ell)\,A_\text{matrix}(E_0,E_\ell),$$

avec $C_i$ la concentration, $\Phi_0$ le flux incident, $\sigma_\text{ion}$ la section efficace d'ionisation, $\omega$ le rendement de fluorescence, $p_{\ell\mid X}$ le rapport de branchement, $\epsilon$ l'efficacité du détecteur et $A_\text{matrix}$ la correction d'absorption / de fluorescence secondaire. L'aperçu de ReciPro ne conserve que la partie $C_i\,p_{\ell\mid X}\,\omega$ (fraction atomique × taux radiatif × rendement) et laisse tomber le reste, de sorte qu'il place les raies et donne leurs intensités relatives intrinsèques afin qu'elles puissent être reconnues dans un spectre mesuré.

---

## Voir aussi

- [Atténuation & transport](attenuation-transport.md) — la photoabsorption, le seuil qui crée la lacune.
- [Facteurs de diffusion atomique](scattering-factor.md) — les mêmes électrons liés, vus dans la diffusion.
- [3. Interaction du faisceau → onglet Fluorescence](../../3-beam-interaction.md#fluorescence-tab)

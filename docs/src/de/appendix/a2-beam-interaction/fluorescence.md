# Fluoreszenz

Wenn die **Photoabsorption** von Röntgenstrahlung ein Elektron einer inneren Schale herausschlägt (siehe [Abschwächung & Transport](attenuation-transport.md)), hinterlässt sie eine Leerstelle in einem tief liegenden Niveau. Das Atom relaxiert, indem ein äußeres Elektron in das Loch fällt, und die freigesetzte Energie tritt entweder als **charakteristisches Röntgenphoton** (Fluoreszenz) aus oder durch das Herausschlagen eines zweiten Elektrons (der **Auger**-Prozess). Die Registerkarte **Fluoreszenz** zeigt eine Vorschau des Kanals der charakteristischen Photonen; sie gilt nur für Röntgenstrahlung und ist für Elektronen- und Neutronenstrahlen ausgeblendet.

![Fluoreszenz (X-ray)](../../../assets/cap-de-auto/FormBeamInteraction-xray-fluorescence.png)

---

## Charakteristische Linien

Da die Schalenenergien scharf definiert sind, ist die emittierte Photonenenergie die **Differenz zweier Bindungsenergien**,

$$E_\gamma = E_B(\text{inner shell}) - E_B(\text{outer shell}),$$

und somit charakteristisch für das Element:

- **K-Linien** — Leerstelle in der $K$-Schale, aufgefüllt aus $L$ ($K\alpha$) oder $M$ ($K\beta$).
- **L-Linien** — Leerstelle in der $L$-Schale, aufgefüllt aus $M$/$N$ ($L\alpha$, $L\beta$, …).

Es treten nur Übergänge auf, die durch die Dipol-Auswahlregeln erlaubt sind, weshalb das Spektrum aus einigen wenigen diskreten Linien (K$\alpha_1$, K$\alpha_2$, K$\beta_1$, L$\alpha_1$, …) statt aus einem Kontinuum besteht. Ihre Energien folgen dem **Moseley-Gesetz**; in der abgeschirmt-wasserstoffartigen Näherung,

$$E_{n_2\to n_1} \approx R_\infty hc\,(Z-\sigma)^2\left(\frac{1}{n_1^2} - \frac{1}{n_2^2}\right), \qquad \text{so}\qquad \sqrt{E} \propto (Z-\sigma),$$

mit $\sigma$ als Abschirmkonstante. Für $K\alpha$ ($n_2{=}2\to n_1{=}1$, $\sigma\approx1$) reduziert sich dies auf $E_{K\alpha}\approx R_\infty hc\,(Z-1)^2\left(1-\tfrac14\right)$. Diese monotone, von der Elektronenzahl getriebene $Z$-Abhängigkeit ist die Grundlage der Elementidentifikation (EDX/WDX).

---

## Fluoreszenzausbeute

Der Wettbewerb zwischen radiativer und Auger-Relaxation wird durch die **Fluoreszenzausbeute** erfasst

$$\omega = \frac{\Gamma_r}{\Gamma_r + \Gamma_a},$$

die Wahrscheinlichkeit, dass eine gegebene Leerstelle durch die Emission eines Photons statt eines Auger-Elektrons zerfällt ($\Gamma_r$, $\Gamma_a$ sind die radiative bzw. die Auger-Rate).

- Für **leichte Elemente** dominiert der Auger-Kanal, sodass $\omega_K$ klein ist (deutlich unter 1 % für C, N, O) — leichte Elemente fluoreszieren schwach, weshalb sie mit EDX schwer nachzuweisen sind.
- Für **schwere Elemente** gewinnt der radiative Kanal und $\omega_K \to$ nahezu 1.

Die komplementäre **Auger-Ausbeute** $a$ nimmt den Rest auf, sodass

$$\omega + a = 1 ,$$

und ein kleines $\omega$ bedeutet, dass die meisten Leerstellen durch Auger-Emission zerfallen. Innerhalb des radiativen Kanals ist der Anteil einer bestimmten Linie $\ell$ (z. B. $K\alpha_1$ gegenüber $K\beta_1$) ihr **Verzweigungsverhältnis**

$$p_{\ell\mid X} = \frac{\Gamma_\ell}{\sum_{\ell'\in X}\Gamma_{\ell'}},$$

die relative radiative Rate innerhalb der Schale $X$. ReciPro listet $\omega_K$ für jedes Element und die stärkste Linie im Spektrum auf.

---

## Was die Vorschau modelliert und was nicht

Das Diagramm der **EDX-Emissionslinien** zeichnet jede charakteristische Linie als Strich bei ihrer Photonenenergie, mit einer Höhe proportional zu

$$\text{(atomic fraction)} \times \text{(radiative rate)} \times \omega.$$

Dies ist eine **qualitative** Vorschau, wo die Linien liegen und wie hoch sie ungefähr relativ zueinander sind. Sie lässt bewusst die Faktoren weg, die ein reales, quantitatives EDX/XRF-Spektrum erfordert:

- ob die einfallende Energie tatsächlich **oberhalb der Absorptionskante** liegt, die zur Erzeugung der Leerstelle nötig ist — eine Linie wird auch dann gezeichnet, wenn sie bei der aktuellen Energie nicht angeregt werden kann;
- der **Anregungswirkungsquerschnitt** (wie effizient der einfallende Strahl die Leerstelle bei der gewählten Energie erzeugt);
- die **Selbstabsorption** der emittierten Photonen innerhalb der Probe (Matrixeffekte);
- die **Detektoreffizienz** und -auflösung.

Die Vorschau dient also der Linienidentifikation und der Argumentation über relative Positionen, nicht der quantitativen Zusammensetzungsbestimmung.

---

## Von der Vorschau zur Quantifizierung

Eine reale EDX/XRF-Analyse wandelt Linienintensitäten über eine **Matrix- (ZAF-) Korrektur** in Konzentrationen um — für die Ordnungszahl ($Z$), die Absorption ($A$) der emittierten Photonen auf ihrem Weg aus der Probe und die sekundäre **Fluoreszenz** ($F$), die durch andere Linien angeregt wird — kombiniert mit dem oben erwähnten Anregungswirkungsquerschnitt und der Detektorantwort. In voller Form ist die gemessene Intensität der Linie $\ell$ von Element $i$

$$I_\ell \;\propto\; C_i\,\Phi_0\,\sigma_{\text{ion},X,i}(E_0)\,\omega_{X,i}\,p_{\ell\mid X}\,\epsilon(E_\ell)\,A_\text{matrix}(E_0,E_\ell),$$

mit $C_i$ als Konzentration, $\Phi_0$ als einfallendem Fluss, $\sigma_\text{ion}$ als Ionisationswirkungsquerschnitt, $\omega$ als Fluoreszenzausbeute, $p_{\ell\mid X}$ als Verzweigungsverhältnis, $\epsilon$ als Detektoreffizienz und $A_\text{matrix}$ als Absorptions-/Sekundärfluoreszenz-Korrektur. Die Vorschau von ReciPro behält nur den Anteil $C_i\,p_{\ell\mid X}\,\omega$ (Atomanteil × radiative Rate × Ausbeute) und lässt den Rest weg, sodass sie die Linien platziert und ihre intrinsischen relativen Stärken angibt, damit sie in einem gemessenen Spektrum erkannt werden können.

---

## Siehe auch

- [Abschwächung & Transport](attenuation-transport.md) — Photoabsorption, die Kante, die die Leerstelle erzeugt.
- [Atomare Streufaktoren](scattering-factor.md) — dieselben gebundenen Elektronen, gesehen in der Streuung.
- [3. Strahl-Wechselwirkung → Registerkarte Fluoreszenz](../../3-beam-interaction.md#fluorescence-tab)

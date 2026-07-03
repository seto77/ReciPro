# Abschwächung & Transport

Streufaktoren beschreiben ein einzelnes Streuereignis; auf dieser Seite geht es darum, was mit dem Strahl **als Ganzes** geschieht, während er den Festkörper durchläuft – wie schnell er entfernt wird, wie tief er eindringt und (bei Elektronen) wie er abgebremst wird. Die maßgebliche Physik ist für die drei Strahlungsarten völlig verschieden, weshalb die Registerkarte **Schwächung & Transport** ihre Diagramme und Tabellen je nach Strahlung so drastisch ändert.

=== "X-ray"
    ![Schwächung & Transport — X-ray](../../../assets/cap-de-auto/FormBeamInteraction-xray-attenuations.png)

=== "Electron"
    ![Schwächung & Transport — electron](../../../assets/cap-de-auto/FormBeamInteraction-electron-attenuations.png)

=== "Neutron"
    ![Schwächung & Transport — neutron](../../../assets/cap-de-auto/FormBeamInteraction-neutron-attenuations.png)

---

## Röntgenstrahlen — Absorption und Brechung

### Beer–Lambert-Abschwächung

Ein monochromatischer Röntgenstrahl wird mit der Weglänge exponentiell entfernt:

$$I(t) = I_0\, e^{-\mu t}, \qquad \mu = \rho\,(\mu/\rho).$$

- $\mu/\rho$ : der **Massenabschwächungskoeffizient** (cm²/g) — die tabellierte, dichteunabhängige Größe.
- $\mu$ : der **lineare Abschwächungskoeffizient** (cm⁻¹) bei der tatsächlichen Dichte $\rho$ des Materials.
- $1/\mu$ : die **Abschwächungslänge** (Intensität fällt auf $1/e$).
- $\text{HVL} = \ln 2/\mu$ : die **Halbwertsschicht**.
- $T = e^{-\mu t}$ : die Transmission für eine Probe der Dicke $t$.

### Woraus sich $\mu/\rho$ zusammensetzt

Die gesamte Massenabschwächung ist die Summe dreier Prozesse, die in der Registerkarte getrennt dargestellt werden:

$$\left(\frac{\mu}{\rho}\right)_\text{total} = \left(\frac{\tau}{\rho}\right)_\text{photo} + \left(\frac{\mu}{\rho}\right)_\text{Rayleigh} + \left(\frac{\mu}{\rho}\right)_\text{Compton}.$$

Bei einer Verbindung ist die Massenabschwächung die massengewichtete Summe der Elementwerte, während der lineare Koeffizient die atomaren Wirkungsquerschnitte direkt aufaddiert:

$$\left(\frac{\mu}{\rho}\right)_\text{mix} = \sum_i w_i\left(\frac{\mu}{\rho}\right)_i, \qquad \mu = \sum_i n_i\,\sigma_i,$$

mit $w_i$ den Massenanteilen und $n_i$ den Teilchendichten. Die drei Komponenten sind:

- **Photoabsorption** $\tau$ — ein Photon wird absorbiert und schlägt ein gebundenes Elektron heraus. Sie dominiert bei niedriger Energie und fällt zwischen den Kanten ungefähr wie $\tau/\rho \propto Z^{3\!-\!4}/E^{3}$ ab. Dies ist der Term, der das innere Schalenelektron herausschlägt, dessen Relaxation [Fluoreszenz](fluorescence.md) erzeugt.
- **Rayleigh-Streuung (kohärent)** — elastische Streuung an gebundenen Elektronen, verknüpft mit dem kohärenten Formfaktor $F(q)$.
- **Compton-Streuung (inkohärent)** — inelastische Streuung an schwach gebundenen Elektronen, verknüpft mit der inkohärenten Funktion $S(q)$; ihre relative Bedeutung wächst bei hoher Energie. Das gestreute Photon wird in der Wellenlänge verschoben um

$$\Delta\lambda = \lambda' - \lambda = \frac{h}{m_e c}\,(1-\cos\varphi),$$

  sodass ein Compton-Ereignis das Photon aus dem monochromatischen Strahl entfernt (ein inelastischer Verlust).

Die **Absorptionskanten** sind die steilen Anstiege von $\tau$, wenn die Photonenenergie die Bindungsenergie einer Schale ($K$, $L_3$, …) überschreitet und einen neuen Ionisationskanal öffnet. Das **Sprungverhältnis** ist der Faktor, um den $\mu/\rho$ über die Kante hinweg ansteigt; ReciPro listet die $K$- und $L_3$-Kantenenergien und -sprünge auf. Der **Massenenergie-Absorptionskoeffizient** $\mu_\text{en}/\rho$ ist der Teil von $\mu/\rho$, der Energie lokal deponiert (unter Ausschluss der von gestreuten und fluoreszenten Photonen weggetragenen Energie).

### Brechung, kritischer Winkel und SLD

Der Röntgenbrechungsindex eines Festkörpers ist **etwas kleiner als 1** und wird geschrieben als

$$n = 1 - \delta + i\beta, \qquad \beta = \frac{\mu_\text{abs}\lambda}{4\pi} = \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,f''_i, \qquad \delta \simeq \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,(Z_i+f'_i),$$

wobei $n_i$ die Teilchendichte des Elements $i$ und $r_e$ der klassische Elektronenradius ist. Hier ist $\mu_\text{abs}$ der absorptive Anteil der Abschwächung (an $f''$ gekoppelt); er muss nicht gleich dem gesamten $\mu$ oben sein, das auch Rayleigh- und Compton-Streuung enthält. Da $n<1$, erfahren Röntgenstrahlen **Totalreflexion** unterhalb eines kleinen streifenden **kritischen Winkels**

$$\theta_c \simeq \sqrt{2\delta}.$$

Dies folgt aus der Brechungsgeometrie: für einen streifenden Winkel $\alpha$ ist der vertikale Wellenvektor im Festkörper $k_z^2 \simeq k^2(\alpha^2 - 2\delta)$, der bei $\alpha = \alpha_c = \sqrt{2\delta}$ null erreicht; darunter kann sich die Welle nicht in das Material ausbreiten und wird vollständig reflektiert. Der Realteil der **Streulängendichte**, $\text{SLD} = r_e\sum_i n_i (Z_i + f'_i)$, legt $\delta$ fest und ist das Röntgenanalogon der in der Reflektometrie verwendeten Neutronen-SLD. ReciPro gibt $\delta$, $\beta$, $\theta_c$ und die Röntgen-SLD in der Skalartabelle an.

---

## Elektronen — Streuung, Abbremsung und Reichweite

Ein schnelles Elektron in einem Festkörper **streut** (ändert die Richtung) und **verliert** zugleich kontinuierlich Energie, sodass sein Transport mehr als eine Längenskala benötigt.

### Elastische Streuung und mittlere freie Weglänge

Der elastische Wirkungsquerschnitt $\sigma_\text{el}$ misst, wie leicht ein einzelnes Atom das Elektron ablenkt. ReciPro verwendet die **NIST-Mott**-Wirkungsquerschnitte (eine Partialwellenlösung der relativistischen Dirac-Gleichung im abgeschirmten atomaren Potential), die etwa über **50 eV – 36.4 keV** gültig sind; außerhalb dieses Bereichs oder für nicht in der Tabelle enthaltene Elemente greift es auf die **abgeschirmte Rutherford**-Näherung zurück. Die beiden müssen an der Grenze nicht perfekt glatt aneinander anschließen. Der totale Wirkungsquerschnitt ist das Winkelintegral des differentiellen,

$$\sigma_\text{el} = 2\pi\int_0^\pi \frac{d\sigma}{d\Omega}\,\sin\Theta\,d\Theta, \qquad \frac{d\sigma}{d\Omega} \propto \frac{Z^2}{E^2}\,\frac{1}{\big[\sin^2(\Theta/2)+\eta\big]^2},$$

wobei der Abschirmparameter $\eta$ die Vorwärtsdivergenz des reinen Rutherford-Wirkungsquerschnitts abrundet; die Mott-Behandlung berücksichtigt zusätzlich die Spin- und relativistischen Effekte, die das abgeschirmte Rutherford-Modell weglässt. Aus dem Wirkungsquerschnitt ergeben

$$\Sigma_\text{el} = \sum_i n_i\,\sigma_{\text{el},i}, \qquad \lambda_\text{el} = \frac{1}{\Sigma_\text{el}},$$

den makroskopischen Streukoeffizienten und die **elastische mittlere freie Weglänge** — die mittlere Distanz zwischen elastischen Ereignissen.

### Bremsvermögen und inelastische Verluste

Energie geht hauptsächlich durch elektronische Anregungen (Ionisation, Plasmonen) verloren. Das **Bremsvermögen** ist als positive Größe definiert,

$$S(E) = -\frac{dE}{ds} > 0,$$

wobei hier $s$ die **Weglänge** entlang der Trajektorie ist (die Variable der *|dE/ds|*-Kurve der Registerkarte), nicht die andernorts in diesem Anhang verwendete Streuvariable $\sin\theta/\lambda$. Der Energiegradient $dE/ds$ ist negativ, sodass die Registerkarte $S$ nach oben aufträgt. Bei keV-Energien folgt es konzeptionell der **Bethe**-Form

$$S(E) \;\propto\; \frac{Z\rho}{A}\,\frac{1}{E}\,\ln\!\frac{E}{J},$$

mit $J$ der **mittleren Anregungsenergie** des Festkörpers. Diese nichtrelativistische Skizze zeigt nur die Skalierung; ReciPro wertet eine korrigierte/empirische Form (vom Joy–Luo-Typ) aus, die bei niedriger Energie gutartig bleibt. Die **Plasmonenenergie** $E_p$ in der Skalartabelle ist eine verwandte, aber getrennte Charakterisierung derselben elektronischen Anregungen. Die **inelastische mittlere freie Weglänge** (IMFP) ist die entsprechende mittlere Distanz zwischen energieverlustbehafteten Stößen; ReciPro kann sie aus der **TPP-2M**-Vorhersageformel auswerten,

$$\lambda_\text{in}(E) = \frac{E}{E_p^2\left[\beta_\text{T}\ln(\gamma_\text{T} E) - C/E + D/E^2\right]},$$

mit $E$ in eV, $\lambda_\text{in}$ in Å und den aus $E_p$, der Dichte, der Bandlücke und der Valenzelektronenzahl aufgebauten Parametern $\beta_\text{T},\gamma_\text{T},C,D$.

### Zwei Arten von Reichweite

- **CSDA-Reichweite** — die Näherung der kontinuierlichen Abbremsung (continuous-slowing-down approximation) integriert das Bremsvermögen, um die gesamte zurückgelegte Weglänge zu liefern, bevor das Elektron zur Ruhe kommt:

$$R_\text{CSDA} = \int_{E_\text{cut}}^{E_0} \frac{dE}{S(E)}.$$

(In der Praxis läuft das Integral bis zu einem niederenergetischen Abschneidewert $E_\text{cut}$ herunter, unterhalb dessen die obige Bethe-Skizze nicht mehr gilt.)

- **Kanaya–Okayama-Reichweite** — eine weit verbreitete empirische Abschätzung der **Eindringtiefe** (nicht der Weglänge), die die gewundene, gestreute Trajektorie berücksichtigt:

$$R_\text{KO}\,[\mu\text{m}] = 0.0276\,\frac{A\,E_0^{1.67}}{\rho\,Z^{0.89}}, \qquad (E_0\ \text{in keV}).$$

Die beiden beantworten unterschiedliche Fragen — gesamte geflogene Distanz vs. wie weit in den Festkörper das Elektron gelangt — und unterscheiden sich daher im Wert, und ReciPro gibt beide an. Diese Reichweiten legen das Wechselwirkungsvolumen hinter den Simulationen der [Elektronenbahnen](../../8-electron-trajectory.md) und der EBSD-Simulation fest.

---

## Neutronen — makroskopischer Wirkungsquerschnitt und das 1/v-Gesetz

Für Neutronen gibt es keine energieabhängige Abschwächungskurve; die Wechselwirkung ist durch **nukleare Wirkungsquerschnitte** festgelegt. Der Strahl wird durch den makroskopischen totalen Wirkungsquerschnitt abgeschwächt, der selbst die Summe aus kohärenten, inkohärenten und absorptiven Anteilen ist:

$$\Sigma_\text{total} = \sum_i n_i\,\sigma_{\text{total},i}, \qquad \sigma_\text{total} = \sigma_\text{coh} + \sigma_\text{inc} + \sigma_\text{abs}(\lambda), \qquad T = e^{-\Sigma_\text{total} t},$$

mit der Abschwächungslänge $1/\Sigma_\text{total}$. Der Absorptionsanteil hängt von der Neutronengeschwindigkeit $v$ ab (und damit von der Wellenlänge): für die meisten Nuklide skaliert die in Kernnähe verbrachte Zeit wie $1/v$, was das **1/v-Gesetz** ergibt

$$\sigma_\text{abs}(\lambda) = \sigma_\text{abs}(\lambda_0)\,\frac{\lambda}{\lambda_0}, \qquad \lambda_0 = 1.798\ \text{Å}\ (\text{thermal}, 2200\ \text{m/s}).$$

Einige starke Absorber (Cd, Sm, Eu, Gd) besitzen niederenergetische **Resonanzen**, die die einfache 1/v-Skalierung verletzen; ReciPro kennzeichnet diese Nuklide. Die kohärente **Streulängendichte**, $\text{SLD} = \sum_i n_i\, b_{\text{coh},i}$, ist das Neutronenanalogon der obigen Röntgen-SLD.

---

## Eindringtiefe auf einen Blick

Die drei Strahlungsarten sondieren ganz unterschiedliche Tiefen — der praktische Grund, warum sie unterschiedliche Fragen beantworten:

| Strahl | Typische Probe | Eindringtiefe (Größenordnung) | Bestimmt durch |
|---|---|---|---|
| Röntgen (≈8 keV) | Pulver / Einkristall | 10–100 µm | $\mu = \rho(\mu/\rho)$ |
| Elektron (≈200 keV) | TEM-Folie | 10–100 nm (nutzbar) | elastische MFP + inelastischer Verlust |
| Neutron (thermisch) | Volumen, cm-groß | 1–10 cm | $\Sigma_\text{total}$ |

Dieselben Längenskalen erklären, warum Elektronen ultradünne Proben und dynamische Theorie erfordern, während Neutronen eine ganze Volumenprobe unter kinematischer Einfachstreuung erfassen.

---

## Siehe auch

- [Atomare Streufaktoren](scattering-factor.md) — die $F(q)$/$S(q)$-Aufteilung hinter Rayleigh/Compton sowie die Mott-Wirkungsquerschnitte.
- [Fluoreszenz](fluorescence.md) — die Relaxation, die auf die Röntgen-Photoabsorption folgt.
- [3. Strahl-Wechselwirkung](../../3-beam-interaction.md) — die Registerkarte *Schwächung & Transport*.
- [8. Elektronenbahnen](../../8-electron-trajectory.md) · [12. EBSD-Simulation](../../12-ebsd-simulation.md) — wo die Elektronenreichweiten verwendet werden.

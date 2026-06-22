# HRTEM-Bildentstehung

Das HRTEM-Bild entsteht aus der Wellenfunktion an der Austrittsfläche — den Transmissionskoeffizienten $T_{\mathbf g}$ aus dem [dynamischen Kern](calculation.md) —, indem diese durch die Objektivlinse geführt wird. ReciPro bietet zwei Modelle: die schnelle **quasi-kohärente** Näherung und das strengere Modell des **Transmissions-Kreuzkoeffizienten (TCC)**. Siehe auch die GUI-Seite [HRTEM-Simulator](../../9-hrtem-stem-simulator/1-hrtem-simulation.md).

---

## Symbole

| Symbol | Bedeutung |
|--------|---------|
| $\mathbf R$ | X–Y-Komponente im Realraum (Bildebene) |
| $\mathbf K$ | X–Y-Komponente des einfallenden Wellenvektors |
| $\mathbf G, \mathbf H$ | X–Y-Komponenten reziproker Gittervektoren |
| $\mathbf u$ | Ortsfrequenz (z. B. $\mathbf K+\mathbf G$) |
| $\chi(\mathbf u)$ | Linsenaberrationsfunktion |
| $A(\mathbf u)$ | Objektivblendenfunktion |
| $\Delta f$ | Defokuswert |
| $C_s$ | Koeffizient der sphärischen Aberration |
| $C_c$ | Koeffizient der chromatischen Aberration |
| $\beta$ | Beleuchtungs-Halbwinkel (endliche Quellgröße) |
| $\Delta E$ | $1/e$-Breite der Energiefluktuationen des Elektrons |
| $\Delta_0$ | $1/e$-Breite der Defokus-Streuung (gaußförmig), $\Delta_0 = C_c\,\Delta E / E$ |

---

## Linsenaberrationsfunktion und Blende

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{inside the objective aperture})\\[2pt] 0 & (\mathbf u\ \text{outside the objective aperture})\end{cases}$$

---

## Quasi-kohärentes Modell

Eine schnelle Näherung: jeder gebeugte Strahl wird durch die Linsenübertragung moduliert und durch Kohärenz-Einhüllende gedämpft, dann kohärent aufsummiert.

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

mit den **zeitlichen** und **räumlichen Kohärenz-Einhüllenden**

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

---

## Modell des Transmissions-Kreuzkoeffizienten (TCC)

Die strenge Behandlung der partiellen Kohärenz: jedes Strahlenpaar $(\mathbf g, \mathbf h)$ interferiert über den Transmissions-Kreuzkoeffizienten.

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

mit den **gemischten** Kohärenz-Einhüllenden

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

Im Grenzfall $\mathbf u' \to \mathbf u$ reduziert sich der TCC auf die obigen quasi-kohärenten Einhüllenden.

---

## Senkung des Rechenaufwands des TCC-Modells

Die Doppelsumme des TCC-Modells wertet $\mathrm{TCC}$ einmal pro Strahlenpaar aus und ist daher aufwendig. Da die Bildintensität $I(\mathbf R)$ reell ist, lässt sich der Aufwand etwa halbieren.

Erstens tragen Strahlen außerhalb der Objektivblende ($A(\mathbf K+\mathbf G)=0$) nicht bei, sodass es genügt, **nur über die Strahlen innerhalb der Blende ($A=1$)** zu summieren.

Zweitens ist der TCC hermitesch,

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

($A$ ist reell; $E_c, E_s$ sind reelle Funktionen, invariant unter $\mathbf u\leftrightarrow\mathbf u'$; der Phasenterm $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ wird komplex konjugiert). Zusammen mit $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$ und $T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$ sind die Terme $(\mathbf g,\mathbf h)$ und $(\mathbf h,\mathbf g)$ zueinander komplex konjugiert, sodass ihre Summe dem Doppelten des Realteils entspricht:

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

Die Doppelsumme reduziert sich daher auf die Diagonale plus das obere Dreieck (eine Seite, sobald den Strahlen eine beliebige Ordnung zugewiesen ist) und halbiert die Zahl der $\mathrm{TCC}$-Auswertungen:

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

Für den Diagonalterm gilt $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$, d. h. $|T_{\mathbf g}|^2$ innerhalb der Blende.

Darüber hinaus nimmt der Phasenfaktor $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ innerhalb dieser Summe vielfach denselben Wert an. Das Speichern und Wiederverwenden dieser Werte beschleunigt die Berechnung weiter.

---

## Siehe auch

- [Dynamische Berechnung (gemeinsamer Kern)](calculation.md) — der gemeinsame Bloch-Wellen-Kern und die Transmissionskoeffizienten $T_{\mathbf g}$
- [Anhang A3. Dynamische Beugung mit der Bloch-Wellen-Methode](index.md)
- [9.1. HRTEM-Simulation](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)

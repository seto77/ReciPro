# STEM-Berechnung

Die STEM-Bildberechnung geht von derselben Darstellung der konvergenten Sonde aus wie [CBED](cbed.md). Der Unterschied liegt in der Observablen: CBED zeigt die Scheibenintensität in der Beugungsebene, während STEM die Sondenposition abtastet und an jeder Position die Intensität integriert, die in den gewählten Detektor gelangt.

---

## Observable

Sei $\mathbf R_0$ die Sondenposition, $\mathbf Q$ die Koordinate der Beugungsebene und $t$ die Probendicke. Ist die Detektorfunktion $D(\mathbf Q)$ innerhalb des Detektor-Winkelbereichs gleich 1 und außerhalb gleich 0, so lautet die elastische STEM-Intensität

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf R_0)=
\int D(\mathbf Q)\,
\left|\psi(\mathbf Q,t;\mathbf R_0)\right|^2\,d\mathbf Q$$

BF, ABF, LAADF und HAADF entsprechen unterschiedlichen Wahlen der inneren und äußeren Winkel in $D(\mathbf Q)$. Eine Änderung des STEM-Detektorwinkels ändert daher die integrierte physikalische Größe; es ist nicht nur eine Anzeigeeinstellung.

---

## Beschleunigung über Fourier-Koeffizienten

Eine direkte Implementierung würde das dynamische Problem für jede abgetastete Sondenposition $\mathbf R_0$ erneut lösen. Der Ausdruck für die konvergente Sonde hat eine nützliche Struktur: die Abhängigkeit von $\mathbf R_0$ tritt als Phasenfaktor auf

$$\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)$$

Dies erlaubt es ReciPro, zuerst die zweidimensionalen Fourier-Koeffizienten des Bildes zu berechnen, anstatt $I_{\mathrm{STEM}}(\mathbf R_0)$ Punkt für Punkt zu berechnen. Konzeptionell gilt

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf q)=
\sum_{\mathbf g,\mathbf h}
F_{\mathbf g,\mathbf h}(t)\,
\delta(\mathbf q-\mathbf g+\mathbf h)$$

sodass sich nach Kenntnis der Koeffizienten $F_{\mathbf g,\mathbf h}(t)$ das vollständige Rasterbild effizient durch eine inverse Fourier-Transformation rekonstruieren lässt.

Dies ist der Hauptvorteil von Bloch-Wellen-STEM für perfekte Kristalle mit kleinen Elementarzellen. Es kann viel schneller sein als die Wiederholung einer Multislice-Berechnung an jeder Sondenposition.

---

## TDS und detektorselektierte Absorption

Bei HAADF-STEM ist die inelastische Komponente aus der thermisch diffusen Streuung (TDS) oft die Hauptquelle des Bildkontrasts. ReciPro behandelt TDS als die Menge an Intensität, die aus dem elastischen Kanal in einen gewählten Winkelbereich entfernt wird, dargestellt durch ein Absorptionspotential.

Für einen Detektor-Winkelbereich $\theta_1\leq\theta\leq\theta_2$ lässt sich der detektorselektierte Absorptions-Streufaktor konzeptionell schreiben als

$$f'_{\kappa}(\mathbf g;\theta_1,\theta_2)=
\int_{\theta_1}^{\theta_2}\sin\theta\,d\theta
\int_0^{2\pi}
\left|\Delta f_{e,\kappa}(\mathbf g,\theta,\phi)\right|^2\,d\phi$$

Wählt man diesen Bereich passend zu einem BF-, ADF- oder HAADF-Detektor, so wird der TDS-Beitrag ausgewertet, der in diesen Detektor gelangt.

Die STEM-TDS-Intensität ist das Dickenintegral der detektorselektierten Absorption:

$$I_{\mathrm{STEM}}^{\mathrm{TDS}}(\mathbf R_0)=
\int_0^t
\langle\psi(z;\mathbf R_0)|\widehat W_{\mathrm{det}}|\psi(z;\mathbf R_0)\rangle\,dz$$

wobei $\widehat W_{\mathrm{det}}$ die detektorselektierte TDS darstellt. Sind die Bloch-Wellen-Eigenwerte und -Eigenvektoren bekannt, lässt sich dieses $z$-Integral analytisch behandeln. Auch eine numerische Schichtintegration ist möglich, und ReciPro verwendet je nach Berechnungsmodus den geeigneten Ansatz.

---

## Lokale und nichtlokale Absorption

Das Absorptionspotential kann auf zwei Hauptarten behandelt werden.

| Form | Bedeutung | Merkmal |
|------|---------|---------|
| Lokale Näherung | Verwendet ein Absorptionspotential $U'(\mathbf r)$, das nur von der Position abhängt. | Meist wirksam und schnell für breite ADF-/HAADF-Detektoren. |
| Nichtlokale Form | Verwendet $U'(\mathbf r,\mathbf r')$ oder Matrixelemente $U'_{\mathbf g,\mathbf h}$, die von Paaren ein- und auslaufender Wellen abhängen. | Genauer für schmale Detektoren, schwere Elemente oder niedrige Beschleunigungsspannungen, aber deutlich aufwendiger. |

In der lokalen Näherung lassen sich die Matrixelemente aus Differenzen reziproker Vektoren wie $U'_{\mathbf g-\mathbf h}$ auswerten. In der nichtlokalen Form erfordert jedes Paar $(\mathbf g,\mathbf h)$ eine eigene Winkelintegration, sodass der Aufwand mit der Strahlzahl rasch wächst.

---

## Geltungsbereich von Bloch-Wellen-STEM

Bloch-Wellen-STEM ist schnell für hochperiodische, perfekte Kristalle und eignet sich gut für systematische Vergleiche von Dicke, Defokus und Detektorwinkeln. Für Defekte, große Superzellen oder nichtperiodische Strukturen können Methoden wie Frozen-Phonon-Multislice geeigneter sein, da sie nicht auf derselben Annahme einer kleinen periodischen Zelle beruhen.

In ReciPro versteht man STEM am einfachsten wie folgt: man beginnt mit derselben konvergenten Welle wie bei CBED und ersetzt dann die Beugungsscheiben-Observable durch eine Detektorintegration über die Beugungsebene.

---

## Praktische Parameter

- **Detektorwinkel**: BF / ABF / ADF / HAADF sind Definitionen von $D(\mathbf Q)$ und $f'_{\kappa}(\mathbf g;\theta_1,\theta_2)$.
- **Strahlzahl**: Hochfrequente Bildanteile und Channeling reagieren empfindlich auf die Zahl der einbezogenen Strahlen.
- **Dickenschritt**: Wird eine numerische Schichtintegration verwendet, prüfen Sie die Änderung, wenn die Schichtdicke halbiert wird.
- **TDS-Modell**: Für HAADF-$Z$-Kontrast ist der TDS-Term ebenso wichtig wie der elastische Term.

## Siehe auch

- [Dynamische Berechnung (gemeinsamer Kern)](calculation.md)
- [Anhang A3. Dynamische Beugung mit der Bloch-Wellen-Methode](index.md)
- [9.2. STEM-Simulation](../../9-hrtem-stem-simulator/2-stem-simulation.md)

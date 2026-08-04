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

## Rekonstruktion eines reellen Bildes {#real-image-reconstruction}

Das Bild wird aus den Koeffizienten zurückgewonnen durch

$$I(\mathbf r)=\sum_{\mathbf q}I(\mathbf q)\,\exp(2\pi i\,\mathbf q\cdot\mathbf r),
\qquad \mathbf q=\mathbf g-\mathbf h$$

Da $I(\mathbf r)$ eine reelle Intensität ist, müssen ihre Koeffizienten die hermitesche Symmetrie exakt erfüllen,

$$I(-\mathbf q)=I(\mathbf q)^{*}$$

und die Menge der von allen Strahlpaaren erzeugten $\mathbf q$ ist unter $\mathbf q\rightarrow-\mathbf q$ abgeschlossen. Die Summe ist daher konstruktionsbedingt reell, und **jeder verbleibende Imaginärteil ist numerischer Fehler, keine Physik**.

In der Praxis bleibt ein kleiner Imaginärteil übrig, weil die Amplitude bei $\mathbf k+\mathbf q$ durch bilineare Interpolation auf dem endlichen Gitter der Einfallsrichtungen gewonnen wird (siehe [Winkelabtastung der Sonde](#angular-sampling)). Dadurch unterscheiden sich $I(-\mathbf q)$ und $I(\mathbf q)^{*}$ um einen Betrag der Ordnung $h^{2}$, wobei $h$ der Winkelschritt ist.

Schreibt man ein summiertes Pixel als $a+ib$, so besteht der korrekte Weg zum reellen Bild darin, den **Realteil** $a$ zu nehmen. Das ist die orthogonale Projektion auf die reelle Achse und identisch damit, zuerst die Koeffizienten zu symmetrisieren,

$$I_{\mathrm{sym}}(\mathbf q)=\tfrac12\left[I(\mathbf q)+I(-\mathbf q)^{*}\right]$$

und erst danach zu summieren. Den Betrag $\sqrt{a^{2}+b^{2}}\simeq a+b^{2}/2a$ zu nehmen, ist **nicht** äquivalent und in vier getrennten Punkten falsch:

- der Zusatzterm $b^{2}/2a$ ist strikt positiv und hebt sich daher nie auf — er ist ein systematischer Fehler, kein Rauschen;
- er ist relativ zum Signal dort am größten, wo $a$ klein ist, also in den **dunklen** Pixeln, und greift damit den Bildkontrast an statt das Gesamtniveau;
- er zerstört die Linearität, sodass das kombinierte Bild nicht mehr gleich elastisch + TDS ist, denn $\lvert z_1+z_2\rvert\neq\lvert z_1\rvert+\lvert z_2\rvert$;
- er verbirgt negative Pixel, die das sichtbare Symptom eines unzureichenden $\mathbf q$-Satzes sind und andernfalls den Benutzer warnen würden.

ReciPro rekonstruiert daher die elastischen, TDS- und STEM-EDX-Bilder aus dem Realteil und begrenzt erst nach der Unschärfe durch die Quellgröße auf null, sodass ein tatsächlich negatives Pixel bis dahin nachweisbar bleibt.

!!! note
    Bis Version 4.944 wurden die elastischen und TDS-Bilder über den Betrag summiert. Auf dem voreingestellten Winkelgitter liegt der Unterschied weit unterhalb jeder wahrnehmbaren Schwelle (siehe Tabelle unten); messbar wird er nur auf einem bewusst groben Gitter, und stets als geringfügige Aufhellung der dunklen Pixel.

---

## Winkelabtastung der Sonde {#angular-sampling}

Der einfallende Kegel wird auf einem quadratischen Richtungsgitter mit Schrittweite $\Delta\alpha$ (**Winkelauflösung** in den STEM-Optionen) abgetastet und überdeckt den Konvergenz-Halbwinkel $\alpha$ mit einem kleinen Rand. Die Zahl der Unterteilungen entlang einer Achse ist

$$N=\left\lceil\frac{2\alpha\times1.05}{\Delta\alpha}\right\rceil$$

sodass die Zahl der Richtungen — und damit der zu lösenden Eigenwertprobleme — wie $N^{2}$ wächst. Dieses Gitter hat nichts mit der Zahl der Rasterpunkte zu tun: es diskretisiert die *Richtungen innerhalb der Sonde*, nicht die *Positionen der Sonde*.

Es ist außerdem die einzige Quelle des oben beschriebenen hermiteschen Residuums, was dieses Residuum zu einem praktischen Konvergenzindikator macht. Die folgenden Werte wurden für SrTiO₃ [001] bei 200 kV mit $\alpha=25$ mrad, 128 Strahlen und 32×32 Rasterpunkten gemessen. „Residuum“ ist $\max_{\mathbf q}\lvert I(\mathbf q)-I(-\mathbf q)^{*}\rvert$ bezogen auf $I(\mathbf 0)$; die letzten beiden Spalten geben die Aufhellung an, die die Betragssumme am hellsten Pixel hinzugefügt hätte.

| $N$ | Richtungen | Elastisches Residuum | TDS-Residuum | Betragsfehler, elastisch | Betragsfehler, TDS |
|----:|-----------:|-----------------:|-------------:|------------------------:|--------------------:|
| 16  | 256    | 1.2×10⁻³ | 6.1×10⁻³ | 2.4×10⁻⁵ | 1.1×10⁻⁴ |
| 32  | 1024   | 4.1×10⁻⁴ | 2.6×10⁻³ | 1.1×10⁻⁶ | 1.3×10⁻⁵ |
| 64  | 4096   | 5.6×10⁻⁵ | 7.2×10⁻⁴ | 5.8×10⁻⁸ | 4.3×10⁻⁷ |
| 132 | 17424  | 3.8×10⁻⁵ | 1.1×10⁻⁴ | 4.2×10⁻⁸ | 3.6×10⁻⁸ |

Die voreingestellte Winkelauflösung von 0,4 mrad ergibt $N=132$ für $\alpha=25$ mrad und liegt damit bereits im konvergierten Bereich. Zwei Punkte sind erwähnenswert:

- Das TDS-Residuum ist auf jedem Gitter etwa eine Größenordnung größer als das elastische, weil die TDS-Koeffizienten zusätzlich das Dickenintegral der detektorselektierten Absorption enthalten.
- Das Residuum ist ein Maximum über alle $\mathbf q$ und streut daher von Gitter zu Gitter, statt völlig gleichmäßig zu fallen; der zugrunde liegende Trend ist $O(h^{2})$.

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
- **Winkelauflösung**: Legt das Richtungsgitter $N$ der Sonde fest (siehe [Winkelabtastung der Sonde](#angular-sampling)). Der Aufwand wächst wie $N^{2}$ und ist damit der wichtigste Hebel für die Rechenzeit.
- **TDS-Modell**: Für HAADF-$Z$-Kontrast ist der TDS-Term ebenso wichtig wie der elastische Term.

## Siehe auch

- [Dynamische Berechnung (gemeinsamer Kern)](calculation.md)
- [Anhang A3. Dynamische Beugung mit der Bloch-Wellen-Methode](index.md)
- [9.2. STEM-Simulation](../../9-hrtem-stem-simulator/2-stem-simulation.md)

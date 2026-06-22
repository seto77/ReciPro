# CBED-Berechnung

CBED (konvergente Elektronenbeugung) wendet den [dynamischen Kern](calculation.md) auf viele Einfallsrichtungen des Strahls an und ordnet die Ergebnisse anschließend in Beugungsscheiben ein. SAED hat eine Einfallsrichtung; CBED behandelt jeden Punkt innerhalb der Objektivblende als **partielle einfallende ebene Welle** und löst das Bloch-Wellen-Problem für jeden einzelnen davon.

---

## Darstellung des konvergenten Strahls

An der Eintrittsfläche lässt sich die konvergente Sonde als Summe ebener Wellen schreiben, unter Verwendung der Sondenposition $\mathbf R_0$, der Linsenphase $\chi(\mathbf K)$ und der Blendenfunktion $A(\mathbf K)$:

$$\psi_{\mathrm{in}}(\mathbf R,0)=\sum_{\mathbf K\in\mathrm{aperture}} A(\mathbf K)\,
\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)\,
\exp[-i\chi(\mathbf K)]\,
\exp(2\pi i\,\mathbf K\cdot\mathbf R)$$

Dabei ist $\mathbf K$ die zur Probenoberfläche parallele Komponente des einfallenden Wellenvektors. Für eine ideale kreisförmige Blende mit Konvergenz-Halbwinkel $\alpha$ und Elektronenwellenlänge $\lambda$ gilt

$$A(\mathbf K)=
\begin{cases}
1 & (|\mathbf K|\leq \sin\alpha/\lambda)\\
0 & (|\mathbf K|> \sin\alpha/\lambda)
\end{cases}$$

Eine repräsentative Linsenphase, mit Defokus $\Delta f$ und sphärischer Aberration $C_s$, lautet

$$\chi(\mathbf K)=\pi\lambda|\mathbf K|^2\Delta f+\frac{\pi}{2}C_s\lambda^3|\mathbf K|^4+\cdots$$

In ReciPro wird dieser Ausdruck durch die Einstellungen für Aberration, Blende und Konvergenzwinkel gesteuert.

---

## Dynamische Berechnung für jede Richtung

Bei CBED wird jedes $\mathbf K$ innerhalb der Blende als ein paralleler einfallender Strahl behandelt. Der konzeptionelle Ablauf ist:

1. Bestimmen Sie den gebrochenen Referenzwellenvektor $\mathbf k_0(\mathbf K)$ aus $\mathbf K$ und der Probenoberflächennormalen.
2. Wählen Sie die reflektierten Strahlen mit der Reihungsgröße $R_{\mathbf g}=|\mathbf g|Q_{\mathbf g}^2$ aus.
3. Bauen Sie die Strukturmatrix $\mathbf A$ auf und berechnen Sie die Transmissionskoeffizienten $T_{\mathbf g}(t;\mathbf K)$ bei der Dicke $t$.

Dies ist die Berechnung der Transmissionskoeffizienten aus dem [dynamischen Kern](calculation.md), wiederholt für jede abgetastete Einfallsrichtung. Bei einer Dickenreihe lässt sich die Eigenlösung für eine gegebene Richtung wiederverwenden, und nur die Ausbreitungsfaktoren müssen aktualisiert werden.

---

## Zusammensetzen der Beugungsscheiben

Setzt man die Austrittswellen aller $\mathbf K$-Richtungen in die Beugungsebene ein, ergibt sich die Intensität innerhalb der durchgehenden Scheibe und der gebeugten Scheiben. Ist $\mathbf Q$ die Koordinate der Beugungsebene, so lassen sich positionsgemitteltes CBED oder Bedingungen geringer Kohärenz als inkohärente Intensitätssumme annähern:

$$I_{\mathrm{CBED}}(\mathbf Q)=
\sum_{\mathbf K\in\mathrm{aperture}}
\left|\psi_{\mathbf K}(\mathbf Q,t)\right|^2$$

Für LACBED-artige Modi, bei denen die Phasenkohärenz über einen größeren Bereich von Bedeutung ist, müssen zuerst die Amplituden summiert und danach die Intensität gebildet werden.

---

## Was CBED zeigt

CBED macht die Dickenabhängigkeit der Bloch-Wellen-Lösung als Intensitätsstruktur innerhalb der Beugungsscheiben sichtbar.

- Eine Änderung der Dicke verändert die Oszillationen im Scheibeninneren, die HOLZ-Linien und die Kossel-Möllenstedt-Streifen.
- Eine Änderung der Einfallsorientierung verändert, welche Reflexe stark angeregt werden.
- Eine Vergrößerung des Konvergenzwinkels verbreitert die Scheiben und kann Überlappungen sowie Informationen aus Laue-Zonen höherer Ordnung sichtbar machen.

CBED ist daher der direkteste Weg, das Bloch-Wellen-Ergebnis als Scheibenmuster in der Beugungsebene zu betrachten. In ReciPro versteht man es am besten als Kombination aus Diskretisierung des konvergenten Strahls, einer dynamischen Lösung pro Richtung und der Anordnung in Scheibenfeldern.

---

## Praktische Parameter

- **Strahlzahl**: Starke Zonenachsenbedingungen und HOLZ-Linien-Details erfordern viele reflektierte Strahlen. Prüfen Sie, wie sich das Scheibeninnere ändert, wenn die maximale Bloch-Wellen-Zahl erhöht wird.
- **Winkelabtastung**: Ist die $\mathbf K$-Abtastung innerhalb der Blende zu grob, wird die Scheibenintensität körnig. Größere Konvergenzwinkel erfordern eine feinere Abtastung.
- **Dicke**: Dickenreihen profitieren von der Eigenwertmethode, da eine Eigenlösung für viele Dicken wiederverwendet werden kann.
- **Kohärenz**: Unterscheiden Sie Bedingungen, bei denen eine inkohärente Intensitätssumme ausreicht, von solchen, bei denen eine kohärente Amplitudensummation erforderlich ist.

## Siehe auch

- [Dynamische Berechnung (gemeinsamer Kern)](calculation.md)
- [Anhang A3. Dynamische Beugung mit der Bloch-Wellen-Methode](index.md)
- [7.4. CBED-Simulation](../../7-diffraction-simulator/3-cbed-simulation.md)

# EBSD-Berechnung

EBSD (Elektronenrückstreubeugung) verwendet denselben Bethe-/Bloch-Wellen-Kern wie CBED und STEM, aber das Problem ist anders gestellt. CBED und STEM sind **Einfallsstrahl-Probleme**: eine Elektronenwelle tritt von außen in die Probe ein, und die Austrittswelle wird berechnet. EBSD ist ein **Austrittsrichtungs-Problem**: Elektronen, die im Inneren der Probe inelastisch gestreut wurden, treten als rückgestreute Elektronen aus, und die Berechnung fragt, wie viel Intensität in jede äußere Richtung austritt.

ReciPro überführt dieses Austrittsrichtungs-Problem mit dem Reziprozitätstheorem in ein gewöhnliches Einfallsstrahl-Problem. Es berechnet zunächst ein **Master-Pattern** im Richtungsraum und kombiniert dieses Master-Pattern dann mit Monte-Carlo-Gewichten für Tiefe / Energie / Richtung sowie der Detektorgeometrie zum Detektorpattern.

---

## Umformulierung mit dem Reziprozitätstheorem

Würde man die Amplitude von einem inneren Quellpunkt $\mathbf r_n$ in eine äußere Richtung $\widehat{\mathbf s}$ direkt berechnen, wäre für jeden Quellpunkt ein separates Streuproblem nötig. Das ist nicht praktikabel.

Das Reziprozitätstheorem schreibt das Problem wie folgt um: die Amplitude dafür, dass ein bei $\mathbf r_n$ startendes Elektron in der Fernfeldrichtung $\widehat{\mathbf s}$ erscheint, ist gleich der Amplitude — bei $\mathbf r_n$ — einer reziproken Welle, die aus der äußeren Richtung $-\widehat{\mathbf s}$ einfällt. Diese reziproke Welle ist eine gewöhnliche Bethe-/Bloch-Wellen-Lösung. Schreibt man sie als $\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r)$, so lässt sich die EBSD-Intensität in Richtung $\widehat{\mathbf s}$ schreiben als

$$I_{\mathrm{EBSD}}(\widehat{\mathbf s};E,z)\propto
\sum_n \sigma_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n;E,z)\right|^2$$

wobei $\sigma_n(E,z)$ das Gewicht für die inelastische Streuung in der Nähe der Atomposition $\mathbf r_n$ in den Rückstreukanal bei Energie $E$ und Tiefe $z$ ist. Die Quellterme werden als Intensitäten addiert, nicht als kohärente Amplitudensumme, da angenommen wird, dass die inelastische Streuung die Phasenbeziehung zwischen verschiedenen Quellpositionen zerstört.

---

## Master-Pattern

Das EBSD-Master-Pattern speichert den kristallspezifischen Anteil der dynamischen Beugung des obigen Ausdrucks auf einem Richtungsgitter. Konzeptionell gilt

$$M(\widehat{\mathbf s};E,z)=
\sum_n w_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n)\right|^2$$

wobei $w_n$ das kristallseitige inelastische Quellgewicht an der Atomposition $\mathbf r_n$ ist. ReciPro verwendet das empirische Gewicht

$$w_n \propto Z_n^{1.7}\,\mathrm{occ}_n$$

mit der Ordnungszahl $Z_n$ und der Besetzung $\mathrm{occ}_n$. Dies ist von der durch Monte Carlo erzeugten Verteilung über Transporttiefe / Energie getrennt.

In der Implementierung wird die reziproke Bloch-Welle an jeder Atomposition ausgewertet:

$$\beta_n^{(j)}=
\alpha^{(j)}
\sum_{\mathbf g}C_{\mathbf g}^{(j)}
\exp\!\left[2\pi i(\mathbf k^{(j)}+\mathbf g)\cdot\mathbf r_n\right]$$

Der Code bildet dann die Bloch-Wellen-Paar-Matrix

$$S_{jj'}=\sum_n w_n\,\beta_n^{(j)}\,\overline{\beta_n^{(j')}}$$

und das analytische Dickenintegral

$$\mathcal F_{jj'}(t)=
\frac{\exp\!\left[2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})t\right]-1}
{2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})}$$

sodass das Master-Pattern ausgewertet wird als

$$M(\widehat{\mathbf s};E,t)=
\mathrm{Re}\left\{\sum_{j,j'}S_{jj'}(E)\,\mathcal F_{jj'}(t)\right\}$$

Im entarteten Grenzfall, in dem der Nenner nahe null liegt, gilt $\mathcal F_{jj'}(t)\to t$.

---

## Abtastung des Richtungsraums

Das Master-Pattern ist nicht das Detektorbild selbst; es ist eine Intensitätsverteilung im kristallfesten Richtungsraum. ReciPro tastet diesen Richtungsraum mit einer flächentreuen Rosca-Lambert-Projektion ab und speichert die $+Z$- und $-Z$-Hemisphären als getrennte ebene Felder. Die flächentreue Abtastung verringert die Dichteverzerrung zwischen den Polen und dem Äquator.

In diesem Stadium hängt das Master-Pattern von Kristallstruktur, Beschleunigungsspannung, Tiefe, Energie und Absorptionsmodell ab. Die Detektorgeometrie wie Pattern-Zentrum und Schirmposition wurde noch nicht angewendet.

---

## Monte-Carlo-Gewichte und Detektorpattern

Um ein EBSD-Detektorpattern nahe an der experimentellen Observablen zu erhalten, muss das Master-Pattern danach gewichtet werden, wie viele rückgestreute Elektronen aus jeder Tiefe, Energie und Richtung austreten. Schreibt man dieses Transportgewicht als

$$W(E,z;\widehat{\mathbf s})$$

und verwendet $\widehat{\mathbf s}(\mathbf p)$ für die kristallfeste Richtung, die dem Detektorpixel $\mathbf p$ entspricht, so lautet das endgültige Detektorpattern

$$P(\mathbf p)=
\sum_{i,j}
W(E_i,z_j;\widehat{\mathbf s}(\mathbf p))\,
M(\widehat{\mathbf s}(\mathbf p);E_i,z_j)$$

als diskrete Summe über Energie und Tiefe.

Der Monte-Carlo-Teil verfolgt elastische Streuung, inelastische Streuung, Energieverlust und das Austreten durch die Probenoberfläche. Für rückgestreute Elektronen bildet er Verteilungen von Tiefe, Energie und Austrittsrichtung. ReciPro unterscheidet Modelle, die die letzte Position der inelastischen Streuung und die Energie unmittelbar danach als effektive Quelle verwenden, von Modellen, die die Austrittstiefe und die Austrittsenergie verwenden.

---

## TDS-Untergrund und Absorptionsmodell

EBSD-Pattern enthalten nicht nur die geometrische Kikuchi-Band-Struktur, sondern auch einen glatten Untergrund aus thermisch diffuser Streuung (TDS). Wenn `IncludeTDSBackground` aktiviert ist, wertet ReciPro die in die hintere Hemisphäre gestreute TDS-Komponente,

$$\pi/2\leq\theta\leq\pi$$

als Absorptionsmatrix $\mu_{\mathrm{back}}$ aus und fügt die Untergrundintensität mit derselben Bloch-Wellen-Paar-Summation wie beim Master-Pattern hinzu. Da dieselbe Eigenlösung wiederverwendet wird, verursacht der TDS-Untergrund relativ wenig zusätzlichen Aufwand.

Wenn `UseNonLocalAbsorption` aktiviert ist, wird das Absorptionspotential nicht nur als $U'_{\mathbf g-\mathbf h}$, sondern als nichtlokale Form behandelt, die von Richtung und Strahlpaaren abhängt. Dies kann die Genauigkeit verbessern, erfordert aber auch den Neuaufbau der Absorptionsmatrix für die Richtungen im Master-Pattern-Gitter und kann daher die Rechenzeit erheblich erhöhen.

---

## Praktische Parameter

- **Strahlzahl**: Zu wenige Strahlen lassen Kikuchi-Band-Details und die HOLZ-Band-Struktur verlieren. Niedrig indizierte Zonenachsen können mehrere hundert Strahlen erfordern.
- **Tiefen- und Energiefelder**: Sind diese gröber als die Variationsskala des Monte-Carlo-Gewichts $W(E,z;\widehat{\mathbf s})$, werden energieabhängige Bandbreiten und Channeling-Tiefen-Effekte herausgemittelt.
- **Detektorgeometrie**: Pattern-Zentrum, Schirmabstand und Probenkippung bestimmen die Abbildung $\widehat{\mathbf s}(\mathbf p)$, sodass sich das Detektorpattern ändern kann, selbst wenn das Master-Pattern unverändert bleibt.
- **Reziprozitäts-Interpretation**: Das Master-Pattern ist nicht das Detektorbild. Es wird erst nach der Monte-Carlo-Gewichtung und der Detektorprojektion zu einem Detektorpattern.
- **TDS-Untergrund**: Aktivieren Sie ihn für quantitative Vergleiche des Bandkontrasts. Deaktivieren Sie ihn, wenn sich die geometrische Kikuchi-Struktur ohne den glatten Untergrund leichter inspizieren lässt.

## Siehe auch

- [Dynamische Berechnung (gemeinsamer Kern)](calculation.md)
- [Anhang A3. Dynamische Beugung mit der Bloch-Wellen-Methode](index.md)
- [12. EBSD-Simulation](../../12-ebsd-simulation.md)

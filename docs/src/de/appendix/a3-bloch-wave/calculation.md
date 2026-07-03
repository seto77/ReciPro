# Dynamische Berechnung (gemeinsamer Kern)

ReciPros Beugungs- und Abbildungssimulatoren teilen sich einen gemeinsamen **dynamischen Bloch-Wellen-(Bethe-)Streukern**, der auf dieser Seite beschrieben wird (Kristallpotential, Debye–Waller- und Absorptionsterme, das Eigenwertproblem, die Transmissionskoeffizienten und die Intensitäten). Die methodenspezifischen Protokolle bauen auf diesem Kern auf:

- [Parallelstrahl-SAED](#parallel-beam-saed)
- [HRTEM-Bildentstehung](hrtem.md)
- [CBED](cbed.md)
- [STEM](stem.md)
- [EBSD](ebsd.md)

Zur zugrunde liegenden Theorie (Schrödinger-Gleichung, Blochsches Theorem, Bethes dynamische Gleichung, das Eigenwertproblem und die Ewald-Kugel-Definitionen) siehe [Anhang A3. Dynamische Beugung mit der Bloch-Wellen-Methode](index.md).

---

## Konstanten

$$\gamma = \frac{m}{m_0} = 1 + \frac{e_0 E}{m_0 c^2}, \qquad \beta = \frac{v}{c} = \sqrt{1 - \left(\frac{m_0}{m}\right)^2} = \sqrt{1 - \gamma^{-2}}$$

- $\gamma$ : relativistischer Korrekturfaktor; $E$ : Beschleunigungsspannung; $m_0$, $m$ : Ruhe- und relativistische Elektronenmasse.
- $\Omega$ : Elementarzellenvolumen.
- $k_{vac}$ : Wellenzahl des Elektrons im Vakuum.

---

## Kristallpotential für die elastische Streuung

Der Fourier-Koeffizient des Kristallpotentials für die elastische Streuung, summiert über die Atome $k$ an den Positionen $\mathbf r_k$, lautet

$$U_{\mathbf g}^{C} = \gamma\,\frac{1}{\pi\Omega}\sum_k f_k(\mathbf g)\,\exp\!\left[2\pi i\,\mathbf g\cdot\mathbf r_k\right]T_k(\mathbf g, M_k)$$

wobei der **Atomformfaktor** eine Gauß-Parametrisierung $(a_i, b_i)$ verwendet:

$$f_k(\mathbf g) = \sum_i a_i\exp\!\left[-b_i\,\frac{|\mathbf g|^2}{4}\right]$$

und $T_k$ der **Debye–Waller-(Temperatur-)Faktor** ist. Für einen isotropen Temperaturfaktor $M_k$ gilt

$$T_k(\mathbf g, M_k) = \exp\!\left[-M_k\,\frac{|\mathbf g|^2}{4}\right]$$

und für einen anisotropen atomaren Auslenkungstensor $\mathbf U$

$$T_k(\mathbf g) = \exp\!\left[-2\pi\,\mathbf g^{t}\mathbf U\,\mathbf g\right]$$

mit der quadratischen Form

$$\mathbf g^{t}\mathbf U\,\mathbf g = \begin{pmatrix} g_x & g_y & g_z\end{pmatrix}\begin{pmatrix} U_{11} & U_{12} & U_{13}\\ U_{12} & U_{22} & U_{23}\\ U_{13} & U_{23} & U_{33}\end{pmatrix}\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = g_x^2 U_{11} + g_y^2 U_{22} + g_z^2 U_{33} + 2\!\left(g_x g_y U_{12} + g_y g_z U_{23} + g_x g_z U_{13}\right)$$

Die kartesischen Komponenten von $\mathbf g$ ergeben sich aus den reziproken Basisvektoren und den Miller-Indizes:

$$\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = \begin{pmatrix} a_x^{*} & b_x^{*} & c_x^{*}\\ a_y^{*} & b_y^{*} & c_y^{*}\\ a_z^{*} & b_z^{*} & c_z^{*}\end{pmatrix}\begin{pmatrix} h\\ k\\ l\end{pmatrix} = \begin{pmatrix} h\,a_x^{*} + k\,b_x^{*} + l\,c_x^{*}\\ h\,a_y^{*} + k\,b_y^{*} + l\,c_y^{*}\\ h\,a_z^{*} + k\,b_z^{*} + l\,c_z^{*}\end{pmatrix}$$

!!! note
    Die in der Tabelle **Reflexdetails** des Beugungssimulators angezeigten $U_{\mathbf g}$-Werte sind die Rohwerte *vor* Anwendung des relativistischen Faktors $\gamma$.

---

## Absorptionspotential (thermisch diffuse Streuung)

Das imaginäre (Absorptions-)Potential, das die thermisch diffuse Streuung (TDS) berücksichtigt, lautet

$$U'_{g,h} = \gamma\,\frac{1}{\pi\Omega}\sum_k f'_k(\mathbf g,\mathbf h)\,\exp\!\left[2\pi i(\mathbf g-\mathbf h)\cdot\mathbf r_k\right]T_k(\mathbf g-\mathbf h, M_k)$$

mit dem **Absorptions-Streufaktor**

$$f'_k(\mathbf g,\mathbf h) = \frac{2h}{\beta\, m_0\, c}\sum_i\sum_j a_i a_j\left[\frac{1}{b_i+b_j}\exp\!\left\{-\frac{b_i b_j}{b_i+b_j}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\} - \frac{1}{b_i+b_j+2M_k}\exp\!\left\{-\frac{b_i b_j - M_k^2}{b_i+b_j+2M_k}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\}\right]$$

Dabei ist $h$ im Vorfaktor $2h/(\beta m_0 c)$ das **Plancksche Wirkungsquantum** (kein Strahlindex). Die Koeffizienten $U^{C}$ und $U'$ sind die Einträge der Strukturmatrix $\mathbf A$ in [Anhang A3](index.md).

---

## Von der Eigenlösung zur gebeugten Intensität

Die Diagonalisierung der Strukturmatrix (siehe [Anhang A3](index.md)) liefert die Eigenwerte $\lambda^{(j)}$ und die Bloch-Wellen-Amplituden $C_{\mathbf g}^{(j)}$. Die Wellenamplituden an der Austrittsfläche — die **Transmissionskoeffizienten** $T_{\mathbf g}$ — bei der Probendicke $t$ lauten

$$\begin{pmatrix} T_0\\ T_g\\ T_h\\ \vdots\end{pmatrix}
= e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}
\begin{pmatrix} e^{\pi i P_0 t} & 0 & 0 & \cdots\\ 0 & e^{\pi i P_g t} & 0 & \cdots\\ 0 & 0 & e^{\pi i P_h t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} C_0^{(1)} & C_0^{(2)} & C_0^{(3)} & \cdots\\ C_g^{(1)} & C_g^{(2)} & C_g^{(3)} & \cdots\\ C_h^{(1)} & C_h^{(2)} & C_h^{(3)} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} e^{2\pi i\lambda^{(1)} t} & 0 & 0 & \cdots\\ 0 & e^{2\pi i\lambda^{(2)} t} & 0 & \cdots\\ 0 & 0 & e^{2\pi i\lambda^{(3)} t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} \alpha^{(1)}\\ \alpha^{(2)}\\ \alpha^{(3)}\\ \vdots\end{pmatrix}$$

oder, komponentenweise,

$$T_{\mathbf g} = e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}\; e^{\pi i P_g t}\sum_j C_{\mathbf g}^{(j)}\,e^{2\pi i\lambda^{(j)} t}\,\alpha^{(j)}$$

- $\alpha^{(j)}$ : die Gewichtungs-(Anregungs-)Koeffizienten jeder Bloch-Welle, festgelegt durch die Randbedingung an der Eintrittsfläche.
- $t$ : Probendicke.

Die gebeugte Intensität des Strahls $\mathbf g$ ist dann

$$I_{\mathbf g} = \left|T_{\mathbf g}\right|^2$$

---

## Parallelstrahl-SAED-Berechnung { #parallel-beam-saed }

Gewöhnliche SAED (Feinbereichs-Elektronenbeugung) wird als **Parallelstrahlbeugung** mit einer einzigen Einfallsrichtung behandelt. Anders als CBED tastet sie nicht viele $\mathbf K$-Punkte innerhalb einer konvergenten Apertur ab. Die aktuelle Kristallorientierung und die Beschleunigungsspannung definieren einen einfallenden Wellenvektor $\mathbf k_0$, und ReciPro berechnet für diese Bedingung Position und Intensität jedes Reflexes $\mathbf g$.

Die Berechnung lässt sich wie folgt gliedern.

1. Verwenden Sie Kristallorientierung, Beschleunigungsspannung, Wellenlänge, Kameralänge und Detektorgeometrie, um den einfallenden Vakuum-Wellenvektor $\mathbf k_{vac}$ und die Detektorebene zu definieren.
2. Wenden Sie die Brechungskorrektur aus dem mittleren inneren Potential $U_0$ an und erhalten Sie den Kristall-Referenzwellenvektor $\mathbf k_0$.
3. Zählen Sie die in Frage kommenden reziproken Gittervektoren $\mathbf g$ auf und bewerten Sie ihren Abstand von der Ewald-Kugel über Größen wie $Q_g=|\mathbf k_0|^2-|\mathbf k_0+\mathbf g|^2$ und den Anregungsfehler $S_g$.
4. Berechnen Sie die Intensität jedes Reflexes mit dem gewählten Intensitätsmodus.
5. Projizieren Sie die Richtung von $\mathbf k_0+\mathbf g$ auf die Detektorebene und zeichnen Sie sie als Beugungsreflex.

ReciPros SAED-Modus bietet hauptsächlich die folgenden Intensitätsmodelle.

| Modus | Berechnung | Typische Verwendung |
|------|-------------|-------------|
| Nur Anregungsfehler | Schätzt die Intensität nur daraus, wie nah der Reflex an der Ewald-Kugel liegt. Strukturfaktoren werden nicht verwendet. | Schnelle Prüfung von Reflexpositionen und Zonenachsengeometrie. |
| Kinematisch + Anregungsfehler | Verwendet $\lvert F_{\mathbf g}\rvert^2$ zusammen mit der Dämpfung durch den Anregungsfehler. Mehrfachstreuung ist nicht enthalten. | Dünne Proben, schwache Beugung und Prüfung von Auslöschungsregeln. |
| Dynamische Theorie | Verwendet den Bloch-Wellen-Kern dieser Seite, um $T_{\mathbf g}(t)$ zu erhalten, und setzt $I_{\mathbf g}=\lvert T_{\mathbf g}\rvert^2$. | Dickenabhängigkeit, Mehrfachstreuung und starke Elektronenbeugungsreflexe. |

Die Anzeigemodi für die reziproken Gitterpunkte, etwa Vollkugel-Querschnitte und Gauß-Punkte, steuern hauptsächlich das Zeichenprofil. Im Modus der dynamischen Theorie wird die physikalische Reflexintensität durch den Bloch-Wellen-Wert $|T_{\mathbf g}|^2$ bestimmt, und diese Intensität wird anschließend dem gewählten Anzeigeprofil zugewiesen.

PED kann als Integration dieser Parallelstrahl-SAED-Berechnung über die Präzessionsrichtungen betrachtet werden, während CBED als Anordnung vieler Einfallsrichtungen innerhalb von Beugungsscheiben aufgefasst werden kann.

---

## Mittleres inneres Potential und Brechung

Wenn das Elektron aus dem Vakuum in den Kristall eintritt, verändert das mittlere innere Potential $U_0$ den Referenzwellenvektor im Kristall geringfügig. Die zur Oberfläche parallele Komponente ist durch die Randbedingung festgelegt, sodass sich der Vakuum-Wellenvektor $\mathbf k_{vac}$ und der Kristall-Referenzwellenvektor $\mathbf k_0$ schreiben lassen als

$$|\mathbf k_0|^2 = k_{vac}^2 + U_0, \qquad \mathbf k_0 = \mathbf k_{vac} + x\,\hat{\mathbf n}$$

wobei $x$ die Korrektur entlang der Oberflächennormalen ist. Sie ergibt sich aus

$$x^2 + 2(\hat{\mathbf n}\cdot\mathbf k_{vac})x - U_0 = 0$$

Dieser gebrochene $\mathbf k_0$ wird bei der Auswertung von $P_g$, $Q_g$, den Anregungsfehlern und der Strukturmatrix $\mathbf A$ auf der [Übersichtsseite](index.md) verwendet. Das Absorptionspotential besitzt außerdem eine $\mathbf g=\mathbf 0$-Komponente, $U'_0$, die als gemeinsame mittlere Abschwächung für die durch den Kristall laufenden Wellen wirkt.

---

## Strahlauswahl

Die Bloch-Wellen-Berechnung kann nicht unendlich viele reziproke Gittervektoren enthalten, daher wählt ReciPro eine endliche Strahlmenge $\{\mathbf g\}$ aus. Die Reihungsgröße ist

$$R_{\mathbf g}=|\mathbf g|\,Q_{\mathbf g}^{\,2}$$

und Strahlen mit kleinerem $R_{\mathbf g}$ werden zuerst aufgenommen. Dies bevorzugt Strahlen mit kurzen reziproken Gittervektoren, die zugleich nahe an der Ewald-Kugel liegen.

In praktischen Berechnungen ist es wichtig zu prüfen, wie stark sich die Intensität oder das Bild ändert, wenn die maximale Zahl der Bloch-Wellen erhöht wird. Starke Zonenachsenbedingungen und CBED-Muster mit HOLZ-Linien-Details können mehrere hundert Strahlen erfordern, während Bedingungen abseits der Zonenachse bereits mit weniger Strahlen konvergieren können.

---

## Wahl des Lösers

Nachdem die endliche Strahlmenge gewählt wurde, verwendet ReciPro hauptsächlich zwei gleichwertige Wege, um die Transmissionskoeffizienten zu erhalten.

| Methode | Merkmal | Typische Verwendung |
|--------|---------|-------------|
| Eigenwertmethode | Diagonalisiert die Strukturmatrix $\mathbf A$ und erhält die Eigenwerte $\lambda^{(j)}$ und Eigenvektoren $C_{\mathbf g}^{(j)}$. Die Dickenabhängigkeit wird anschließend über $e^{2\pi i\lambda^{(j)}t}$ ausgewertet. | Dickenreihen sowie CBED- und EBSD-Berechnungen, die viele Tiefen oder Energien abtasten |
| Matrix-Exponential-Methode | Wertet direkt die Streumatrix $\exp(2\pi i\mathbf A t)$ aus, ohne explizit eine Eigenzerlegung zu verwenden. | STEM-Berechnungen für eine einzelne Dicke und schichtweise integrierte Berechnungen |

Beide Methoden lösen dieselbe Bethe-Gleichung. In der Implementierung wählt der Code je nach Strahlzahl, Dickenfeld und Verfügbarkeit der nativen Bibliothek zwischen der Eigenwertmethode, der Matrix-Exponential-Methode, verwalteten .NET-Routinen und der nativen Eigen-Bibliothek.

---

## Konvergenzprüfungen

Bei dynamischen Berechnungen ist die Prüfung, ob die Basis groß genug ist, ebenso wichtig wie die Formel selbst. Ein nützliches Diagnosemaß ist die relative Änderung, wenn die Strahlzahl von $N-\Delta N$ auf $N$ erhöht wird:

$$\Delta I_N=\frac{|I_N-I_{N-\Delta N}|}{I_N}$$

Prüfen Sie dies bei STEM zusammen mit der Detektorwinkel-Einstellung. Inspizieren Sie bei CBED das Innere der Scheiben und die HOLZ-Linien. Vergleichen Sie bei EBSD zusätzlich die Kikuchi-Band-Breiten und den Untergrund im Master-Pattern. Dies verknüpft die numerische Konvergenz mit den im simulierten Ergebnis sichtbaren physikalischen Merkmalen.

---

## Siehe auch

- [Anhang A3. Dynamische Beugung mit der Bloch-Wellen-Methode](index.md)
- [7.2. SAED simulation](../../7-diffraction-simulator/1-saed-simulation.md)
- [7.4. CBED simulation](../../7-diffraction-simulator/3-cbed-simulation.md)
- [7. Beugungssimulator](../../7-diffraction-simulator/index.md)

# Anhang A3. Dynamische Beugung mit der Bloch-Wellen-Methode

Dieser Anhang gibt einen Überblick über die Theorie der dynamischen Elektronenbeugung, die ReciPros Simulatoren **Beugungssimulator**, **CBED** und **HRTEM/STEM** verwenden. ReciPro folgt der **Bethe-/Bloch-Wellen**-Formulierung. Die schrittweise Berechnung (optisches Potential, Transmissionskoeffizienten, Intensitäten) ist unter [Dynamische Berechnung (gemeinsamer Kern)](calculation.md) beschrieben.

---

## Die Wellengleichung im Kristall

Ein schnelles Elektron, das sich durch das periodische elektrostatische Potential eines Kristalls bewegt, gehorcht der (hochenergetischen, stationären) Schrödinger-Gleichung, die sich wie folgt schreiben lässt:

$$\nabla^2 \Psi(\mathbf{r}) + 4\pi^2\left\{\, k_{vac}^2 + \sum_{\mathbf g} U_{\mathbf g}\, e^{2\pi i\,\mathbf g\cdot\mathbf r} \right\}\Psi(\mathbf{r}) = 0$$

- $k_{vac}$ : Wellenzahl des Elektrons im Vakuum.
- $U_{\mathbf g}$ : Fourier-Komponente des Kristallpotentials für den reziproken Gittervektor $\mathbf g$. Da das Potential gitterperiodisch ist, wird es als Fourier-Reihe über das reziproke Gitter geschrieben.

---

## Blochsches Theorem

Da das Potential die Periodizität des Kristallgitters besitzt, sind die Lösungen **Bloch-Wellen**:

$$\Psi(\mathbf{r}) = b\!\left(\mathbf{k}^{(j)}, \mathbf{r}\right) = u(\mathbf{r})\exp\!\left(2\pi i\,\mathbf{k}^{(j)}\cdot\mathbf{r}\right)$$

- $u(\mathbf r)$ : eine Funktion mit derselben Periodizität wie das Kristallgitter, sodass sie selbst über das reziproke Gitter entwickelt werden kann: $u(\mathbf r)=\sum_{\mathbf g} C_{\mathbf g}^{(j)}\exp(2\pi i\,\mathbf g\cdot\mathbf r)$.
- $\mathbf{k}^{(j)}$ : der $j$-te Bloch-Wellenvektor.
- $C_{\mathbf g}^{(j)}$ : die Amplitude (Eigenvektor-Komponente) des Strahls $\mathbf g$ in der $j$-ten Bloch-Welle.

---

## Bethes dynamische Gleichung

Das Einsetzen der Bloch-Wellen-Entwicklung in die Wellengleichung liefert **Bethes dynamische Gleichung** — eine gekoppelte Gleichung für jeden Strahl $\mathbf g$:

$$\left[\,k^2 - \left(\mathbf{k}^{(j)} + \mathbf{g}\right)^2 + i\,U'_{g,g}\right]C_{\mathbf g}^{(j)} + \sum_{h \neq g}\left(U^C_{g-h} + i\,U'_{g,h}\right)C_{\mathbf h}^{(j)} = 0$$

- $U^C_{\mathbf g}$ : Kristallpotential für die **elastische** Streuung.
- $U'_{\mathbf g}$ : imaginäres (**Absorptions-**)Potential, das die **thermisch diffuse Streuung** (TDS) berücksichtigt. Wie es und der Debye–Waller-Faktor eingehen, wird im [Berechnungskern](calculation.md) ausführlich beschrieben.

---

## Geometrische Definitionen (Ewald-Kugel)

Die oben auftretenden Vektoren und Skalare sind auf der Ewald-Kugel definiert:

![Definitionen der in der Bloch-Wellen-Berechnung verwendeten Vektoren und Skalare](../../../assets/references/Bloch.png){width=500px}

- $\hat{\mathbf n}$ : Einheitsvektor normal zur Kristalloberfläche.
- $\mathbf k$ : einfallender Wellenvektor (seine Spitze liegt auf der Ewald-Kugel); $\mathbf k_{vac}$ ist der Vakuum-Wellenvektor.
- $\mathbf g$ : reziproker Gittervektor; $\mathbf k + \mathbf g$ zeigt zum reziproken Gitterpunkt.
- $\mathbf k^{(j)}$ : der $j$-te Bloch-Wellenvektor. Alle Bloch-Wellenvektoren haben dieselbe Tangentialkomponente (Stetigkeit an der Oberfläche) und unterscheiden sich nur entlang $\hat{\mathbf n}$: $\mathbf k^{(j)} = \mathbf k + \gamma^{(j)}\hat{\mathbf n}$.
- $\gamma^{(j)}$ : der $j$-te Eigenwert (die Komponente von $\mathbf k^{(j)}$ entlang $\hat{\mathbf n}$, gemessen von $\mathbf k$).

Aus der Geometrie folgt:

$$P_g = 2\,\hat{\mathbf n}\cdot(\mathbf k + \mathbf g), \qquad Q_g = |\mathbf k|^2 - |\mathbf k + \mathbf g|^2 = -\,\mathbf g\cdot(2\mathbf k + \mathbf g)$$

und der **Anregungsfehler** $S_g$ (die Abweichung des reziproken Gitterpunkts von der Ewald-Kugel) sowie die zur Reihung der Reflexe verwendete **Bewertungsfunktion** $R$ lauten:

$$S_g = \frac{\sqrt{P_g^{\,2} + 4Q_g}\; -\; P_g}{2}, \qquad R = |\mathbf g|\,Q_g^{\,2}$$

---

## Reduktion auf ein Eigenwertproblem

Schreibt man $\mathbf{k}^{(j)} = \mathbf{k} + \gamma^{(j)}\hat{\mathbf n}$ und verwendet $k^2-(\mathbf k+\mathbf g)^2 = Q_g$ zusammen mit der Linearisierung $(\mathbf k^{(j)}+\mathbf g)^2 \approx (\mathbf k+\mathbf g)^2 + \gamma^{(j)} P_g$, so wird Bethes Gleichung (nach Division durch $P_g$) zu einem gewöhnlichen **Matrix-Eigenwertproblem**:

$$\mathbf{A}\,\mathbf{C} = \mathbf{C}\,\boldsymbol{\Lambda}, \qquad
A_{gh} = \frac{U^C_{\,g-h} + i\,U'_{g,h}}{P_g}\;\;(g\neq h), \qquad
A_{gg} = \frac{Q_g + i\,U'_{g,g}}{P_g}$$

- Die Spalten von $\mathbf{C}$ sind die Eigenvektoren $C^{(j)}_*$ (die Bloch-Wellen-Amplituden).
- $\boldsymbol{\Lambda}=\mathrm{diag}\!\left(\lambda^{(1)}, \lambda^{(2)}, \dots\right)$ enthält die Eigenwerte $\lambda^{(j)} = \gamma^{(j)}$.

Explizit ausgeschrieben — mit den Strahlen in der Reihenfolge durchgehender Strahl $0$, dann $g$, $h$, $\dots$ — lautet dies:

$$
\begin{aligned}
&\begin{pmatrix}
(Q_0 + i\,U'_{0,0})/P_0 & (U^C_{-g} + i\,U'_{0,g})/P_0 & (U^C_{-h} + i\,U'_{0,h})/P_0 & \cdots \\
(U^C_{g} + i\,U'_{g,0})/P_g & (Q_g + i\,U'_{g,g})/P_g & (U^C_{g-h} + i\,U'_{g,h})/P_g & \cdots \\
(U^C_{h} + i\,U'_{h,0})/P_h & (U^C_{h-g} + i\,U'_{h,g})/P_h & (Q_h + i\,U'_{h,h})/P_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\begin{pmatrix}
C^{(1)}_0 & C^{(2)}_0 & C^{(3)}_0 & \cdots \\
C^{(1)}_g & C^{(2)}_g & C^{(3)}_g & \cdots \\
C^{(1)}_h & C^{(2)}_h & C^{(3)}_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix} \\[1.2ex]
&\qquad=
\begin{pmatrix}
C^{(1)}_0 & C^{(2)}_0 & C^{(3)}_0 & \cdots \\
C^{(1)}_g & C^{(2)}_g & C^{(3)}_g & \cdots \\
C^{(1)}_h & C^{(2)}_h & C^{(3)}_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\begin{pmatrix}
\lambda^{(1)} & 0 & 0 & \cdots \\
0 & \lambda^{(2)} & 0 & \cdots \\
0 & 0 & \lambda^{(3)} & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\end{aligned}
$$

Die Diagonalisierung von $\mathbf{A}$ liefert **alle** Bloch-Wellenvektoren und Amplituden auf einmal. Die Amplituden der gebeugten Strahlen — und damit die Intensitäten — folgen dann aus den Randbedingungen an der Eintritts- und Austrittsfläche sowie aus der Probendicke. Diese Schritte, das optische (komplexe) Potential, der Debye–Waller-Faktor und die Transmissionskoeffizienten $T_{\mathbf g}$ sind unter [Dynamische Berechnung (gemeinsamer Kern)](calculation.md) beschrieben.

> **Hinweis:** Die in der Tabelle **Details** des Beugungssimulators angezeigten $V_{\mathbf g}$-Werte sind die Rohwerte *vor* Anwendung des relativistischen Korrekturfaktors.

---

## Siehe auch

- [7. Beugungssimulator](../../7-diffraction-simulator/index.md) — dynamische Beugungsmuster
- [9. HRTEM/STEM-Simulator](../../9-hrtem-stem-simulator/index.md)
- [Anhang A1. Koordinatensysteme](../a1-coordinate-system/1-orientation.md)
- [Dynamische Berechnung (gemeinsamer Kern)](calculation.md)

# Appendice A3. Diffrazione dinamica con il metodo delle onde di Bloch

Questa appendice offre una panoramica della teoria della diffrazione elettronica dinamica utilizzata dai simulatori **Simulatore di diffrazione**, **CBED** e **HRTEM/STEM** di ReciPro. ReciPro segue la formulazione **Bethe / onde di Bloch**. Il calcolo passo per passo (potenziale ottico, coefficienti di trasmissione, intensità) è descritto in [Calcolo dinamico (nucleo comune)](calculation.md).

---

## L'equazione d'onda in un cristallo

Un elettrone veloce che attraversa il potenziale elettrostatico periodico di un cristallo obbedisce all'equazione di Schrödinger (ad alta energia, stazionaria), che può essere scritta come

$$\nabla^2 \Psi(\mathbf{r}) + 4\pi^2\left\{\, k_{vac}^2 + \sum_{\mathbf g} U_{\mathbf g}\, e^{2\pi i\,\mathbf g\cdot\mathbf r} \right\}\Psi(\mathbf{r}) = 0$$

- $k_{vac}$ : numero d'onda dell'elettrone nel vuoto.
- $U_{\mathbf g}$ : componente di Fourier del potenziale cristallino per il vettore del reticolo reciproco $\mathbf g$. Poiché il potenziale è periodico secondo il reticolo, viene scritto come serie di Fourier sul reticolo reciproco.

---

## Teorema di Bloch

Poiché il potenziale possiede la periodicità del reticolo cristallino, le soluzioni sono **onde di Bloch**:

$$\Psi(\mathbf{r}) = b\!\left(\mathbf{k}^{(j)}, \mathbf{r}\right) = u(\mathbf{r})\exp\!\left(2\pi i\,\mathbf{k}^{(j)}\cdot\mathbf{r}\right)$$

- $u(\mathbf r)$ : una funzione con la stessa periodicità del reticolo cristallino, così da poter essere essa stessa sviluppata sul reticolo reciproco: $u(\mathbf r)=\sum_{\mathbf g} C_{\mathbf g}^{(j)}\exp(2\pi i\,\mathbf g\cdot\mathbf r)$.
- $\mathbf{k}^{(j)}$ : il $j$-esimo vettore d'onda di Bloch.
- $C_{\mathbf g}^{(j)}$ : l'ampiezza (componente dell'autovettore) del fascio $\mathbf g$ nella $j$-esima onda di Bloch.

---

## Equazione dinamica di Bethe

La sostituzione dello sviluppo in onde di Bloch nell'equazione d'onda fornisce l'**equazione dinamica di Bethe** — un'equazione accoppiata per ciascun fascio $\mathbf g$:

$$\left[\,k^2 - \left(\mathbf{k}^{(j)} + \mathbf{g}\right)^2 + i\,U'_{g,g}\right]C_{\mathbf g}^{(j)} + \sum_{h \neq g}\left(U^C_{g-h} + i\,U'_{g,h}\right)C_{\mathbf h}^{(j)} = 0$$

- $U^C_{\mathbf g}$ : potenziale cristallino per la diffusione **elastica**.
- $U'_{\mathbf g}$ : potenziale immaginario (di **assorbimento**), che tiene conto della **diffusione termica diffusa** (TDS). Il modo in cui esso e il fattore di Debye–Waller intervengono è descritto in dettaglio nel [nucleo di calcolo](calculation.md).

---

## Definizioni geometriche (sfera di Ewald)

I vettori e gli scalari che compaiono sopra sono definiti sulla sfera di Ewald:

![Definizioni dei vettori e degli scalari usati nel calcolo con onde di Bloch](../../../assets/references/Bloch.png){width=500px}

- $\hat{\mathbf n}$ : vettore unitario normale alla superficie del cristallo.
- $\mathbf k$ : vettore d'onda incidente (la sua punta giace sulla sfera di Ewald); $\mathbf k_{vac}$ è il vettore d'onda nel vuoto.
- $\mathbf g$ : vettore del reticolo reciproco; $\mathbf k + \mathbf g$ punta al nodo del reticolo reciproco.
- $\mathbf k^{(j)}$ : il $j$-esimo vettore d'onda di Bloch. Tutti i vettori d'onda di Bloch condividono la stessa componente tangenziale (continuità attraverso la superficie) e differiscono solo lungo $\hat{\mathbf n}$: $\mathbf k^{(j)} = \mathbf k + \gamma^{(j)}\hat{\mathbf n}$.
- $\gamma^{(j)}$ : il $j$-esimo autovalore (la componente di $\mathbf k^{(j)}$ lungo $\hat{\mathbf n}$, misurata a partire da $\mathbf k$).

Dalla geometria,

$$P_g = 2\,\hat{\mathbf n}\cdot(\mathbf k + \mathbf g), \qquad Q_g = |\mathbf k|^2 - |\mathbf k + \mathbf g|^2 = -\,\mathbf g\cdot(2\mathbf k + \mathbf g)$$

e l'**errore di eccitazione** $S_g$ (la deviazione del nodo del reticolo reciproco dalla sfera di Ewald) insieme alla **funzione di valutazione** $R$ usata per ordinare le riflessioni sono

$$S_g = \frac{\sqrt{P_g^{\,2} + 4Q_g}\; -\; P_g}{2}, \qquad R = |\mathbf g|\,Q_g^{\,2}$$

---

## Riduzione a un problema agli autovalori

Scrivendo $\mathbf{k}^{(j)} = \mathbf{k} + \gamma^{(j)}\hat{\mathbf n}$ e usando $k^2-(\mathbf k+\mathbf g)^2 = Q_g$ insieme alla linearizzazione $(\mathbf k^{(j)}+\mathbf g)^2 \approx (\mathbf k+\mathbf g)^2 + \gamma^{(j)} P_g$, l'equazione di Bethe diventa (dopo la divisione per $P_g$) un comune **problema agli autovalori matriciale**:

$$\mathbf{A}\,\mathbf{C} = \mathbf{C}\,\boldsymbol{\Lambda}, \qquad
A_{gh} = \frac{U^C_{\,g-h} + i\,U'_{g,h}}{P_g}\;\;(g\neq h), \qquad
A_{gg} = \frac{Q_g + i\,U'_{g,g}}{P_g}$$

- Le colonne di $\mathbf{C}$ sono gli autovettori $C^{(j)}_*$ (le ampiezze delle onde di Bloch).
- $\boldsymbol{\Lambda}=\mathrm{diag}\!\left(\lambda^{(1)}, \lambda^{(2)}, \dots\right)$ contiene gli autovalori $\lambda^{(j)} = \gamma^{(j)}$.

Scritto esplicitamente — ordinando i fasci come fascio trasmesso $0$, poi $g$, $h$, $\dots$ — questo è

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

La diagonalizzazione di $\mathbf{A}$ fornisce **tutti** i vettori d'onda di Bloch e le ampiezze in una sola volta. Le ampiezze dei fasci diffratti — e quindi le intensità — seguono poi dalle condizioni al contorno sulle superfici di entrata e di uscita e dallo spessore del campione. Questi passaggi, il potenziale ottico (complesso), il fattore di Debye–Waller e i coefficienti di trasmissione $T_{\mathbf g}$ sono descritti in [Calcolo dinamico (nucleo comune)](calculation.md).

> **Nota:** I valori $V_{\mathbf g}$ mostrati nella tabella **Details** del simulatore di diffrazione sono i valori grezzi *prima* dell'applicazione del fattore di correzione relativistico.

---

## Vedi anche

- [7. Simulatore di diffrazione](../../7-diffraction-simulator/index.md) — pattern di diffrazione dinamici
- [9. Simulatore HRTEM/STEM](../../9-hrtem-stem-simulator/index.md)
- [Appendice A1. Sistemi di coordinate](../a1-coordinate-system/1-orientation.md)
- [Calcolo dinamico (nucleo comune)](calculation.md)

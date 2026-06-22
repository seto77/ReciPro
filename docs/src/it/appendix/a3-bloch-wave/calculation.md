# Calcolo dinamico (nucleo comune)

I simulatori di diffrazione e di imaging di ReciPro condividono un comune **nucleo di diffusione dinamica a onde di Bloch (Bethe)**, descritto in questa pagina (potenziale cristallino, termini di Debye–Waller e di assorbimento, il problema agli autovalori, i coefficienti di trasmissione e le intensità). I protocolli specifici di ciascun metodo si basano su questo nucleo:

- [SAED a fascio parallelo](#parallel-beam-saed)
- [Formazione dell'immagine HRTEM](hrtem.md)
- [CBED](cbed.md)
- [STEM](stem.md)
- [EBSD](ebsd.md)

Per la teoria sottostante (equazione di Schrödinger, teorema di Bloch, equazione dinamica di Bethe, il problema agli autovalori e le definizioni della sfera di Ewald), vedere [Appendice A3. Diffrazione dinamica con il metodo delle onde di Bloch](index.md).

---

## Costanti

$$\gamma = \frac{m}{m_0} = 1 + \frac{e_0 E}{m_0 c^2}, \qquad \beta = \frac{v}{c} = \sqrt{1 - \left(\frac{m_0}{m}\right)^2} = \sqrt{1 - \gamma^{-2}}$$

- $\gamma$ : fattore di correzione relativistico; $E$ : tensione di accelerazione; $m_0$, $m$ : massa dell'elettrone a riposo e relativistica.
- $\Omega$ : volume della cella elementare.
- $k_{vac}$ : numero d'onda dell'elettrone nel vuoto.

---

## Potenziale cristallino per la diffusione elastica

Il coefficiente di Fourier del potenziale cristallino per la diffusione elastica, sommato sugli atomi $k$ nelle posizioni $\mathbf r_k$, è

$$U_{\mathbf g}^{C} = \gamma\,\frac{1}{\pi\Omega}\sum_k f_k(\mathbf g)\,\exp\!\left[2\pi i\,\mathbf g\cdot\mathbf r_k\right]T_k(\mathbf g, M_k)$$

dove il **fattore di diffusione atomico** utilizza una parametrizzazione gaussiana $(a_i, b_i)$,

$$f_k(\mathbf g) = \sum_i a_i\exp\!\left[-b_i\,\frac{|\mathbf g|^2}{4}\right]$$

e $T_k$ è il **fattore di Debye–Waller (temperatura)**. Per un fattore di temperatura isotropo $M_k$,

$$T_k(\mathbf g, M_k) = \exp\!\left[-M_k\,\frac{|\mathbf g|^2}{4}\right]$$

e per un tensore di spostamento atomico anisotropo $\mathbf U$,

$$T_k(\mathbf g) = \exp\!\left[-2\pi\,\mathbf g^{t}\mathbf U\,\mathbf g\right]$$

con la forma quadratica

$$\mathbf g^{t}\mathbf U\,\mathbf g = \begin{pmatrix} g_x & g_y & g_z\end{pmatrix}\begin{pmatrix} U_{11} & U_{12} & U_{13}\\ U_{12} & U_{22} & U_{23}\\ U_{13} & U_{23} & U_{33}\end{pmatrix}\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = g_x^2 U_{11} + g_y^2 U_{22} + g_z^2 U_{33} + 2\!\left(g_x g_y U_{12} + g_y g_z U_{23} + g_x g_z U_{13}\right)$$

Le componenti cartesiane di $\mathbf g$ si ottengono dai vettori di base reciproci e dagli indici di Miller:

$$\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = \begin{pmatrix} a_x^{*} & b_x^{*} & c_x^{*}\\ a_y^{*} & b_y^{*} & c_y^{*}\\ a_z^{*} & b_z^{*} & c_z^{*}\end{pmatrix}\begin{pmatrix} h\\ k\\ l\end{pmatrix} = \begin{pmatrix} h\,a_x^{*} + k\,b_x^{*} + l\,c_x^{*}\\ h\,a_y^{*} + k\,b_y^{*} + l\,c_y^{*}\\ h\,a_z^{*} + k\,b_z^{*} + l\,c_z^{*}\end{pmatrix}$$

!!! note
    I valori $U_{\mathbf g}$ mostrati nella tabella **Details** del simulatore di diffrazione sono i valori grezzi *prima* dell'applicazione del fattore relativistico $\gamma$.

---

## Potenziale di assorbimento (diffusione termica diffusa)

Il potenziale immaginario (di assorbimento) che tiene conto della diffusione termica diffusa (TDS) è

$$U'_{g,h} = \gamma\,\frac{1}{\pi\Omega}\sum_k f'_k(\mathbf g,\mathbf h)\,\exp\!\left[2\pi i(\mathbf g-\mathbf h)\cdot\mathbf r_k\right]T_k(\mathbf g-\mathbf h, M_k)$$

con il **fattore di diffusione di assorbimento**

$$f'_k(\mathbf g,\mathbf h) = \frac{2h}{\beta\, m_0\, c}\sum_i\sum_j a_i a_j\left[\frac{1}{b_i+b_j}\exp\!\left\{-\frac{b_i b_j}{b_i+b_j}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\} - \frac{1}{b_i+b_j+2M_k}\exp\!\left\{-\frac{b_i b_j - M_k^2}{b_i+b_j+2M_k}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\}\right]$$

Qui $h$ nel prefattore $2h/(\beta m_0 c)$ è la **costante di Planck** (non un indice di fascio). I coefficienti $U^{C}$ e $U'$ sono gli elementi della matrice di struttura $\mathbf A$ nell'[Appendice A3](index.md).

---

## Dalla soluzione agli autovalori all'intensità diffratta

La diagonalizzazione della matrice di struttura (vedere [Appendice A3](index.md)) fornisce gli autovalori $\lambda^{(j)}$ e le ampiezze delle onde di Bloch $C_{\mathbf g}^{(j)}$. Le ampiezze d'onda sulla superficie di uscita — i **coefficienti di trasmissione** $T_{\mathbf g}$ — allo spessore del campione $t$ sono

$$\begin{pmatrix} T_0\\ T_g\\ T_h\\ \vdots\end{pmatrix}
= e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}
\begin{pmatrix} e^{\pi i P_0 t} & 0 & 0 & \cdots\\ 0 & e^{\pi i P_g t} & 0 & \cdots\\ 0 & 0 & e^{\pi i P_h t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} C_0^{(1)} & C_0^{(2)} & C_0^{(3)} & \cdots\\ C_g^{(1)} & C_g^{(2)} & C_g^{(3)} & \cdots\\ C_h^{(1)} & C_h^{(2)} & C_h^{(3)} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} e^{2\pi i\lambda^{(1)} t} & 0 & 0 & \cdots\\ 0 & e^{2\pi i\lambda^{(2)} t} & 0 & \cdots\\ 0 & 0 & e^{2\pi i\lambda^{(3)} t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} \alpha^{(1)}\\ \alpha^{(2)}\\ \alpha^{(3)}\\ \vdots\end{pmatrix}$$

oppure, componente per componente,

$$T_{\mathbf g} = e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}\; e^{\pi i P_g t}\sum_j C_{\mathbf g}^{(j)}\,e^{2\pi i\lambda^{(j)} t}\,\alpha^{(j)}$$

- $\alpha^{(j)}$ : i coefficienti di ponderazione (eccitazione) di ciascuna onda di Bloch, fissati dalla condizione al contorno sulla superficie di ingresso.
- $t$ : spessore del campione.

L'intensità diffratta del fascio $\mathbf g$ è quindi

$$I_{\mathbf g} = \left|T_{\mathbf g}\right|^2$$

---

## Calcolo SAED a fascio parallelo { #parallel-beam-saed }

La SAED ordinaria (diffrazione elettronica ad area selezionata) è trattata come **diffrazione a fascio parallelo** con una singola direzione di incidenza. A differenza del CBED, non esegue la scansione di molti punti $\mathbf K$ all'interno di un'apertura convergente. L'orientazione del cristallo attuale e la tensione di accelerazione definiscono un singolo vettore d'onda incidente $\mathbf k_0$, e ReciPro valuta la posizione e l'intensità di ciascuna riflessione $\mathbf g$ per tale condizione.

Il calcolo può essere organizzato come segue.

1. Usare l'orientazione del cristallo, la tensione di accelerazione, la lunghezza d'onda, la lunghezza della camera e la geometria del rivelatore per definire il vettore d'onda incidente nel vuoto $\mathbf k_{vac}$ e il piano del rivelatore.
2. Applicare la correzione di rifrazione dovuta al potenziale interno medio $U_0$ e ottenere il vettore d'onda di riferimento nel cristallo $\mathbf k_0$.
3. Enumerare i vettori del reticolo reciproco candidati $\mathbf g$ e valutarne la distanza dalla sfera di Ewald tramite quantità come $Q_g=|\mathbf k_0|^2-|\mathbf k_0+\mathbf g|^2$ e l'errore di eccitazione $S_g$.
4. Calcolare l'intensità di ciascuna riflessione utilizzando la modalità di intensità selezionata.
5. Proiettare la direzione di $\mathbf k_0+\mathbf g$ sul piano del rivelatore e disegnarla come spot di diffrazione.

La modalità SAED di ReciPro offre principalmente i seguenti modelli di intensità.

| Modalità | Calcolo | Uso tipico |
|------|-------------|-------------|
| Solo errore di eccitazione | Stima l'intensità unicamente in base a quanto la riflessione è vicina alla sfera di Ewald. I fattori di struttura non vengono utilizzati. | Verifiche rapide delle posizioni degli spot e della geometria dell'asse di zona. |
| Cinematica + errore di eccitazione | Usa $\lvert F_{\mathbf g}\rvert^2$ insieme allo smorzamento dovuto all'errore di eccitazione. La diffusione multipla non è inclusa. | Campioni sottili, diffrazione debole e verifiche delle regole di estinzione. |
| Teoria dinamica | Usa il nucleo a onde di Bloch di questa pagina per ottenere $T_{\mathbf g}(t)$ e pone $I_{\mathbf g}=\lvert T_{\mathbf g}\rvert^2$. | Dipendenza dallo spessore, diffusione multipla e riflessioni intense della diffrazione elettronica. |

Le modalità di visualizzazione dei punti del reticolo reciproco, come le sezioni a sfera piena e gli spot gaussiani, controllano principalmente il profilo di disegno. Nella modalità a teoria dinamica, l'intensità fisica della riflessione è determinata dal valore a onde di Bloch $|T_{\mathbf g}|^2$, e tale intensità viene poi assegnata al profilo di visualizzazione scelto.

Il PED può essere visto come l'integrazione di questo calcolo SAED a fascio parallelo sulle direzioni di precessione, mentre il CBED può essere visto come la disposizione di molte direzioni di incidenza all'interno dei dischi di diffrazione.

---

## Potenziale interno medio e rifrazione

Quando l'elettrone entra nel cristallo dal vuoto, il potenziale interno medio $U_0$ modifica leggermente il vettore d'onda di riferimento all'interno del cristallo. La componente parallela alla superficie è fissata dalla condizione al contorno, per cui il vettore d'onda nel vuoto $\mathbf k_{vac}$ e il vettore d'onda di riferimento nel cristallo $\mathbf k_0$ possono essere scritti come

$$|\mathbf k_0|^2 = k_{vac}^2 + U_0, \qquad \mathbf k_0 = \mathbf k_{vac} + x\,\hat{\mathbf n}$$

dove $x$ è la correzione lungo la normale alla superficie. Essa si ottiene da

$$x^2 + 2(\hat{\mathbf n}\cdot\mathbf k_{vac})x - U_0 = 0$$

Questo $\mathbf k_0$ rifratto viene utilizzato nella valutazione di $P_g$, $Q_g$, degli errori di eccitazione e della matrice di struttura $\mathbf A$ nella [pagina panoramica](index.md). Il potenziale di assorbimento possiede inoltre una componente $\mathbf g=\mathbf 0$, $U'_0$, che agisce come un'attenuazione media comune per le onde che si propagano attraverso il cristallo.

---

## Selezione dei fasci

Il calcolo a onde di Bloch non può includere infiniti vettori del reticolo reciproco, perciò ReciPro seleziona un insieme finito di fasci $\{\mathbf g\}$. La grandezza di ordinamento è

$$R_{\mathbf g}=|\mathbf g|\,Q_{\mathbf g}^{\,2}$$

e i fasci con $R_{\mathbf g}$ minore vengono inclusi per primi. Ciò favorisce i fasci con vettori del reticolo reciproco corti che siano anche vicini alla sfera di Ewald.

Nei calcoli pratici, è importante verificare quanto cambia l'intensità o l'immagine al crescere del numero massimo di onde di Bloch. Condizioni di asse di zona intense e pattern CBED con dettaglio delle linee HOLZ possono richiedere diverse centinaia di fasci, mentre le condizioni fuori asse di zona possono convergere con un numero inferiore di fasci.

---

## Scelta del solutore

Dopo aver scelto l'insieme finito di fasci, ReciPro utilizza principalmente due modi equivalenti per ottenere i coefficienti di trasmissione.

| Metodo | Caratteristica | Uso tipico |
|--------|---------|-------------|
| Metodo agli autovalori | Diagonalizza la matrice di struttura $\mathbf A$ e ottiene gli autovalori $\lambda^{(j)}$ e gli autovettori $C_{\mathbf g}^{(j)}$. La dipendenza dallo spessore viene poi valutata tramite $e^{2\pi i\lambda^{(j)}t}$. | Serie di spessori e calcoli CBED ed EBSD che esplorano molte profondità o energie |
| Metodo dell'esponenziale di matrice | Valuta direttamente la matrice di diffusione $\exp(2\pi i\mathbf A t)$ senza usare esplicitamente una decomposizione agli autovalori. | Calcoli STEM a spessore singolo e calcoli integrati per fette |

Entrambi i metodi risolvono la stessa equazione di Bethe. Nell'implementazione, il codice sceglie tra il metodo agli autovalori, il metodo dell'esponenziale di matrice, le routine gestite .NET e la libreria nativa Eigen a seconda del numero di fasci, dell'array degli spessori e della disponibilità della libreria nativa.

---

## Verifiche di convergenza

Per i calcoli dinamici, verificare che la base sia sufficientemente grande è importante quanto la formula stessa. Una diagnostica utile è la variazione relativa quando il numero di fasci viene aumentato da $N-\Delta N$ a $N$:

$$\Delta I_N=\frac{|I_N-I_{N-\Delta N}|}{I_N}$$

Per lo STEM, controllare questo insieme all'impostazione dell'angolo del rivelatore. Per il CBED, ispezionare l'interno dei dischi e le linee HOLZ. Per l'EBSD, confrontare inoltre le larghezze delle bande di Kikuchi e il fondo nel master pattern. Questo collega la convergenza numerica con le caratteristiche fisiche visibili nel risultato simulato.

---

## Vedere anche

- [Appendice A3. Diffrazione dinamica con il metodo delle onde di Bloch](index.md)
- [7.2. SAED simulation](../../7-diffraction-simulator/1-saed-simulation.md)
- [7.4. CBED simulation](../../7-diffraction-simulator/3-cbed-simulation.md)
- [7. Simulatore di diffrazione](../../7-diffraction-simulator/index.md)

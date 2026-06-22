# Formazione dell'immagine HRTEM

L'immagine HRTEM si forma a partire dalla funzione d'onda sulla superficie di uscita — i coefficienti di trasmissione $T_{\mathbf g}$ ottenuti dal [nucleo dinamico](calculation.md) — facendola passare attraverso la lente obiettivo. ReciPro offre due modelli: la rapida approssimazione **quasi-coerente** e il modello più rigoroso del **coefficiente di trasmissione incrociata (TCC)**. Vedere anche la pagina GUI [Simulatore HRTEM](../../9-hrtem-stem-simulator/1-hrtem-simulation.md).

---

## Simboli

| Simbolo | Significato |
|--------|---------|
| $\mathbf R$ | Componente X–Y nello spazio reale (piano immagine) |
| $\mathbf K$ | Componente X–Y del vettore d'onda incidente |
| $\mathbf G, \mathbf H$ | Componenti X–Y dei vettori del reticolo reciproco |
| $\mathbf u$ | frequenza spaziale (ad es. $\mathbf K+\mathbf G$) |
| $\chi(\mathbf u)$ | funzione di aberrazione della lente |
| $A(\mathbf u)$ | funzione dell'apertura obiettivo |
| $\Delta f$ | valore di defocalizzazione |
| $C_s$ | coefficiente di aberrazione sferica |
| $C_c$ | coefficiente di aberrazione cromatica |
| $\beta$ | semiangolo di illuminazione (dimensione finita della sorgente) |
| $\Delta E$ | larghezza $1/e$ delle fluttuazioni di energia dell'elettrone |
| $\Delta_0$ | larghezza $1/e$ della dispersione di defocalizzazione (gaussiana), $\Delta_0 = C_c\,\Delta E / E$ |

---

## Funzione di aberrazione della lente e apertura

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{inside the objective aperture})\\[2pt] 0 & (\mathbf u\ \text{outside the objective aperture})\end{cases}$$

---

## Modello quasi-coerente

Un'approssimazione rapida: ogni fascio diffratto viene modulato dalla trasmissione della lente e smorzato dagli inviluppi di coerenza, quindi sommato coerentemente.

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

con gli **inviluppi di coerenza temporale** e **spaziale**

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

---

## Modello del coefficiente di trasmissione incrociata (TCC)

Il trattamento rigoroso della coerenza parziale: ogni coppia di fasci $(\mathbf g, \mathbf h)$ interferisce attraverso il coefficiente di trasmissione incrociata.

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

con gli inviluppi di coerenza **misti**

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

Nel limite $\mathbf u' \to \mathbf u$ il TCC si riduce agli inviluppi quasi-coerenti riportati sopra.

---

## Riduzione del costo del modello TCC

La doppia somma del modello TCC valuta $\mathrm{TCC}$ una volta per ogni coppia di fasci, ed è quindi onerosa. Poiché l'intensità dell'immagine $I(\mathbf R)$ è reale, il costo può essere ridotto all'incirca della metà.

In primo luogo, i fasci esterni all'apertura obiettivo ($A(\mathbf K+\mathbf G)=0$) non contribuiscono, quindi è sufficiente sommare **solo sui fasci interni all'apertura ($A=1$)**.

In secondo luogo, il TCC è hermitiano,

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

($A$ è reale; $E_c, E_s$ sono funzioni reali invarianti sotto $\mathbf u\leftrightarrow\mathbf u'$; il termine di fase $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ viene coniugato complesso). Insieme a $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$ e $T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$, i termini $(\mathbf g,\mathbf h)$ e $(\mathbf h,\mathbf g)$ sono complessi coniugati l'uno dell'altro, cosicché la loro somma è pari al doppio della parte reale:

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

La doppia somma si riduce pertanto alla diagonale più il triangolo superiore (un solo lato, una volta assegnato ai fasci un ordinamento arbitrario), dimezzando il numero di valutazioni di $\mathrm{TCC}$:

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

Per il termine diagonale vale $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$, cioè $|T_{\mathbf g}|^2$ all'interno dell'apertura.

Inoltre, il fattore di fase $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ assume più volte lo stesso valore all'interno di questa somma. Memorizzare e riutilizzare questi valori accelera ulteriormente il calcolo.

---

## Vedere anche

- [Calcolo dinamico (nucleo comune)](calculation.md) — il nucleo comune delle onde di Bloch e i coefficienti di trasmissione $T_{\mathbf g}$
- [Appendice A3. Diffrazione dinamica con il metodo delle onde di Bloch](index.md)
- [9.1. Simulazione HRTEM](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)

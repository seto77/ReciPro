# Calcolo CBED

La CBED (diffrazione elettronica a fascio convergente) applica il [nucleo dinamico](calculation.md) a molte direzioni del fascio incidente e dispone poi i risultati in dischi di diffrazione. La SAED ha una sola direzione di incidenza; la CBED tratta ogni punto all'interno dell'apertura obiettivo come un'**onda piana incidente parziale** e risolve il problema delle onde di Bloch per ciascuno di essi.

---

## Rappresentazione del fascio convergente

Sulla superficie di ingresso, la sonda convergente può essere scritta come somma di onde piane utilizzando la posizione della sonda $\mathbf R_0$, la fase della lente $\chi(\mathbf K)$ e la funzione di apertura $A(\mathbf K)$:

$$\psi_{\mathrm{in}}(\mathbf R,0)=\sum_{\mathbf K\in\mathrm{aperture}} A(\mathbf K)\,
\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)\,
\exp[-i\chi(\mathbf K)]\,
\exp(2\pi i\,\mathbf K\cdot\mathbf R)$$

Qui $\mathbf K$ è la componente del vettore d'onda incidente parallela alla superficie del campione. Per un'apertura circolare ideale con semiangolo di convergenza $\alpha$ e lunghezza d'onda dell'elettrone $\lambda$,

$$A(\mathbf K)=
\begin{cases}
1 & (|\mathbf K|\leq \sin\alpha/\lambda)\\
0 & (|\mathbf K|> \sin\alpha/\lambda)
\end{cases}$$

Una fase della lente rappresentativa, con defocalizzazione $\Delta f$ e aberrazione sferica $C_s$, è

$$\chi(\mathbf K)=\pi\lambda|\mathbf K|^2\Delta f+\frac{\pi}{2}C_s\lambda^3|\mathbf K|^4+\cdots$$

In ReciPro questa espressione è controllata dalle impostazioni di aberrazione, apertura e angolo di convergenza.

---

## Calcolo dinamico per ciascuna direzione

Nella CBED, ogni $\mathbf K$ all'interno dell'apertura viene trattato come un fascio incidente parallelo. Il flusso di lavoro concettuale è:

1. Determinare il vettore d'onda di riferimento rifratto $\mathbf k_0(\mathbf K)$ a partire da $\mathbf K$ e dalla normale alla superficie del campione.
2. Selezionare i fasci riflessi mediante la grandezza di ordinamento $R_{\mathbf g}=|\mathbf g|Q_{\mathbf g}^2$.
3. Costruire la matrice di struttura $\mathbf A$ e calcolare i coefficienti di trasmissione $T_{\mathbf g}(t;\mathbf K)$ allo spessore $t$.

Questo è il calcolo dei coefficienti di trasmissione del [nucleo dinamico](calculation.md), ripetuto per ogni direzione di incidenza campionata. Per una serie di spessori, la soluzione agli autovalori per una data direzione può essere riutilizzata e devono essere aggiornati soltanto i fattori di propagazione.

---

## Composizione dei dischi di diffrazione

Inserendo le onde in uscita di tutte le direzioni $\mathbf K$ nel piano di diffrazione si ottiene l'intensità all'interno del disco trasmesso e dei dischi diffratti. Se $\mathbf Q$ è la coordinata del piano di diffrazione, la CBED mediata sulla posizione o le condizioni di bassa coerenza possono essere approssimate come somma di intensità incoerente:

$$I_{\mathrm{CBED}}(\mathbf Q)=
\sum_{\mathbf K\in\mathrm{aperture}}
\left|\psi_{\mathbf K}(\mathbf Q,t)\right|^2$$

Per le modalità di tipo LACBED in cui la coerenza di fase su una regione più ampia è rilevante, occorre sommare prima le ampiezze e calcolare l'intensità in seguito.

---

## Che cosa mostra la CBED

La CBED rende visibile la dipendenza dallo spessore della soluzione delle onde di Bloch come struttura di intensità all'interno dei dischi di diffrazione.

- Una variazione dello spessore modifica le oscillazioni interne ai dischi, le linee HOLZ e le frange di Kossel-Mollenstedt.
- Una variazione dell'orientazione di incidenza modifica quali riflessioni vengono fortemente eccitate.
- Un aumento dell'angolo di convergenza allarga i dischi e può rivelare sovrapposizioni e informazioni provenienti dalle zone di Laue di ordine superiore.

La CBED è quindi il modo più diretto per osservare il risultato delle onde di Bloch come pattern di dischi nel piano di diffrazione. In ReciPro si comprende al meglio come la combinazione della discretizzazione del fascio convergente, di una soluzione dinamica per direzione e della riorganizzazione in matrici di dischi.

---

## Parametri pratici

- **Numero di fasci**: Condizioni di forte asse di zona e i dettagli delle linee HOLZ richiedono molti fasci riflessi. Verificare come cambia l'interno dei dischi all'aumentare del numero massimo di onde di Bloch.
- **Campionamento angolare**: Se il campionamento di $\mathbf K$ all'interno dell'apertura è troppo grossolano, l'intensità dei dischi diventa granulosa. Angoli di convergenza maggiori richiedono un campionamento più fine.
- **Spessore**: Le serie di spessori traggono vantaggio dal metodo degli autovalori, poiché una sola soluzione agli autovalori può essere riutilizzata per molti spessori.
- **Coerenza**: Distinguere le condizioni in cui è sufficiente una somma di intensità incoerente da quelle in cui è necessaria una somma coerente delle ampiezze.

## Vedi anche

- [Calcolo dinamico (nucleo comune)](calculation.md)
- [Appendice A3. Diffrazione dinamica con il metodo delle onde di Bloch](index.md)
- [7.4. Simulazione CBED](../../7-diffraction-simulator/3-cbed-simulation.md)

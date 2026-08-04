# Calcolo STEM

Il calcolo dell'immagine STEM parte dalla stessa rappresentazione della sonda convergente di [CBED](cbed.md). La differenza sta nell'osservabile: CBED mostra l'intensità del disco nel piano di diffrazione, mentre STEM scandisce la posizione della sonda e a ogni posizione integra l'intensità che entra nel rivelatore selezionato.

---

## Osservabile

Sia $\mathbf R_0$ la posizione della sonda, $\mathbf Q$ la coordinata del piano di diffrazione e $t$ lo spessore del campione. Se la funzione del rivelatore $D(\mathbf Q)$ vale 1 all'interno dell'intervallo angolare del rivelatore e 0 al di fuori di esso, l'intensità STEM elastica è

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf R_0)=
\int D(\mathbf Q)\,
\left|\psi(\mathbf Q,t;\mathbf R_0)\right|^2\,d\mathbf Q$$

BF, ABF, LAADF e HAADF corrispondono a scelte diverse degli angoli interno ed esterno in $D(\mathbf Q)$. Cambiare l'angolo del rivelatore STEM modifica quindi la grandezza fisica integrata; non si tratta soltanto di un'impostazione di visualizzazione.

---

## Accelerazione tramite coefficienti di Fourier

Un'implementazione diretta risolverebbe nuovamente il problema dinamico per ogni posizione di sonda scandita $\mathbf R_0$. L'espressione della sonda convergente ha una struttura utile: la dipendenza da $\mathbf R_0$ compare come fattore di fase

$$\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)$$

Ciò consente a ReciPro di calcolare prima i coefficienti di Fourier bidimensionali dell'immagine, anziché calcolare $I_{\mathrm{STEM}}(\mathbf R_0)$ punto per punto. Concettualmente,

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf q)=
\sum_{\mathbf g,\mathbf h}
F_{\mathbf g,\mathbf h}(t)\,
\delta(\mathbf q-\mathbf g+\mathbf h)$$

cosicché, una volta noti i coefficienti $F_{\mathbf g,\mathbf h}(t)$, l'intera immagine di scansione può essere ricostruita in modo efficiente tramite una trasformata di Fourier inversa.

Questo è il principale vantaggio dello STEM a onde di Bloch per cristalli perfetti con celle elementari piccole. Può essere molto più veloce della ripetizione di un calcolo multislice a ogni posizione della sonda.

---

## Ricostruzione di un'immagine reale {#real-image-reconstruction}

L'immagine si ricava dai coefficienti tramite

$$I(\mathbf r)=\sum_{\mathbf q}I(\mathbf q)\,\exp(2\pi i\,\mathbf q\cdot\mathbf r),
\qquad \mathbf q=\mathbf g-\mathbf h$$

Poiché $I(\mathbf r)$ è un'intensità reale, i suoi coefficienti devono soddisfare esattamente la simmetria hermitiana,

$$I(-\mathbf q)=I(\mathbf q)^{*}$$

e l'insieme dei $\mathbf q$ generati da tutte le coppie di fasci è chiuso rispetto a $\mathbf q\rightarrow-\mathbf q$. La somma è quindi reale per costruzione, e **qualsiasi parte immaginaria residua è errore numerico, non fisica**.

In pratica una piccola parte immaginaria sopravvive, perché l'ampiezza in $\mathbf k+\mathbf q$ è ottenuta per interpolazione bilineare sulla griglia finita delle direzioni di incidenza (vedi [Campionamento angolare della sonda](#angular-sampling)). Ciò fa sì che $I(-\mathbf q)$ e $I(\mathbf q)^{*}$ differiscano di una quantità dell'ordine di $h^{2}$, dove $h$ è il passo angolare.

Scrivendo un pixel sommato come $a+ib$, il modo corretto di ridurlo a un'immagine reale è prendere la **parte reale** $a$. Questa è la proiezione ortogonale sull'asse reale ed è identica al simmetrizzare prima i coefficienti,

$$I_{\mathrm{sym}}(\mathbf q)=\tfrac12\left[I(\mathbf q)+I(-\mathbf q)^{*}\right]$$

e sommare in seguito. Prendere il modulo $\sqrt{a^{2}+b^{2}}\simeq a+b^{2}/2a$ **non** è equivalente e sbaglia in quattro modi distinti:

- il termine aggiuntivo $b^{2}/2a$ è strettamente positivo, quindi non si annulla mai: è un bias, non rumore;
- è massimo rispetto al segnale dove $a$ è piccolo, cioè nei pixel **scuri**, e quindi attacca il contrasto dell'immagine anziché il livello complessivo;
- rompe la linearità, per cui l'immagine combinata non è più uguale a elastico + TDS, dato che $\lvert z_1+z_2\rvert\neq\lvert z_1\rvert+\lvert z_2\rvert$;
- nasconde i pixel negativi, che sono il sintomo visibile di un insieme di $\mathbf q$ insufficiente e che altrimenti metterebbero in guardia l'utente.

ReciPro ricostruisce perciò le immagini elastica, TDS e STEM-EDX dalla parte reale e taglia a zero solo dopo la sfocatura dovuta alla dimensione della sorgente, così che un pixel realmente negativo resti rilevabile fino a quel punto.

!!! note
    Fino alla versione 4.944 le immagini elastica e TDS venivano sommate in modulo. Sulla griglia angolare predefinita la differenza è molto al di sotto di qualsiasi soglia percepibile (vedi la tabella sotto); diventa misurabile solo su una griglia volutamente grossolana, e sempre come un lieve schiarimento dei pixel scuri.

---

## Campionamento angolare della sonda {#angular-sampling}

Il cono incidente è campionato su una griglia quadrata di direzioni con passo $\Delta\alpha$ (**Risoluzione angolare** nelle opzioni STEM), che copre il semiangolo di convergenza $\alpha$ con un piccolo margine. Il numero di suddivisioni lungo un asse è

$$N=\left\lceil\frac{2\alpha\times1.05}{\Delta\alpha}\right\rceil$$

cosicché il numero di direzioni, e quindi di problemi agli autovalori da risolvere, cresce come $N^{2}$. Questa griglia non ha nulla a che vedere con il numero di punti di scansione: discretizza le *direzioni all'interno della sonda*, non le *posizioni della sonda*.

È inoltre l'unica sorgente del residuo hermitiano descritto sopra, il che rende quel residuo un comodo indicatore di convergenza. I valori seguenti sono stati misurati per SrTiO₃ [001] a 200 kV con $\alpha=25$ mrad, 128 fasci e 32×32 punti di scansione. Il «residuo» è $\max_{\mathbf q}\lvert I(\mathbf q)-I(-\mathbf q)^{*}\rvert$ rapportato a $I(\mathbf 0)$, e le ultime due colonne danno lo schiarimento che la somma in modulo avrebbe aggiunto al pixel più luminoso.

| $N$ | Direzioni | Residuo elastico | Residuo TDS | Bias di modulo, elastico | Bias di modulo, TDS |
|----:|-----------:|-----------------:|-------------:|------------------------:|--------------------:|
| 16  | 256    | 1.2×10⁻³ | 6.1×10⁻³ | 2.4×10⁻⁵ | 1.1×10⁻⁴ |
| 32  | 1024   | 4.1×10⁻⁴ | 2.6×10⁻³ | 1.1×10⁻⁶ | 1.3×10⁻⁵ |
| 64  | 4096   | 5.6×10⁻⁵ | 7.2×10⁻⁴ | 5.8×10⁻⁸ | 4.3×10⁻⁷ |
| 132 | 17424  | 3.8×10⁻⁵ | 1.1×10⁻⁴ | 4.2×10⁻⁸ | 3.6×10⁻⁸ |

La risoluzione angolare predefinita di 0,4 mrad dà $N=132$ per $\alpha=25$ mrad, che è già nella regione convergente. Due punti meritano attenzione:

- Il residuo TDS è circa un ordine di grandezza maggiore di quello elastico su ogni griglia, perché i coefficienti TDS portano in più l'integrale in spessore dell'assorbimento selezionato dal rivelatore.
- Il residuo è un massimo su tutti i $\mathbf q$, quindi oscilla un poco da griglia a griglia invece di decrescere in modo perfettamente regolare; l'andamento di fondo è $O(h^{2})$.

---

## TDS e assorbimento selezionato dal rivelatore

Nello STEM-HAADF, la componente anelastica derivante dalla diffusione termica diffusa (TDS) è spesso la principale sorgente di contrasto dell'immagine. ReciPro tratta la TDS come la quantità di intensità rimossa dal canale elastico verso un intervallo angolare selezionato, rappresentata da un potenziale di assorbimento.

Per un intervallo angolare del rivelatore $\theta_1\leq\theta\leq\theta_2$, il fattore di diffusione di assorbimento selezionato dal rivelatore può essere scritto concettualmente come

$$f'_{\kappa}(\mathbf g;\theta_1,\theta_2)=
\int_{\theta_1}^{\theta_2}\sin\theta\,d\theta
\int_0^{2\pi}
\left|\Delta f_{e,\kappa}(\mathbf g,\theta,\phi)\right|^2\,d\phi$$

Scegliendo questo intervallo in modo da corrispondere a un rivelatore BF, ADF o HAADF, si valuta il contributo TDS che entra in quel rivelatore.

L'intensità TDS dello STEM è l'integrale sullo spessore dell'assorbimento selezionato dal rivelatore:

$$I_{\mathrm{STEM}}^{\mathrm{TDS}}(\mathbf R_0)=
\int_0^t
\langle\psi(z;\mathbf R_0)|\widehat W_{\mathrm{det}}|\psi(z;\mathbf R_0)\rangle\,dz$$

dove $\widehat W_{\mathrm{det}}$ rappresenta la TDS selezionata dal rivelatore. Una volta noti gli autovalori e gli autovettori delle onde di Bloch, questo integrale in $z$ può essere trattato analiticamente. È possibile anche un'integrazione numerica per fette, e ReciPro utilizza l'approccio appropriato in base alla modalità di calcolo.

---

## Assorbimento locale e non locale

Il potenziale di assorbimento può essere trattato in due modi principali.

| Forma | Significato | Caratteristica |
|------|---------|---------|
| Approssimazione locale | Utilizza un potenziale di assorbimento $U'(\mathbf r)$ che dipende solo dalla posizione. | Di solito efficace e veloce per rivelatori ADF / HAADF ampi. |
| Forma non locale | Utilizza $U'(\mathbf r,\mathbf r')$ o elementi di matrice $U'_{\mathbf g,\mathbf h}$ che dipendono da coppie di onde entranti e uscenti. | Più accurata per rivelatori stretti, elementi pesanti o basse tensioni di accelerazione, ma molto più onerosa. |

Nell'approssimazione locale, gli elementi di matrice possono essere valutati a partire da differenze di vettori reciproci come $U'_{\mathbf g-\mathbf h}$. Nella forma non locale, ogni coppia $(\mathbf g,\mathbf h)$ richiede una propria integrazione angolare, per cui il costo cresce rapidamente con il numero di fasci.

---

## Ambito di applicazione dello STEM a onde di Bloch

Lo STEM a onde di Bloch è veloce per cristalli perfetti e altamente periodici ed è ben adatto a confronti sistematici di spessore, defocalizzazione e angoli del rivelatore. Per difetti, supercelle grandi o strutture non periodiche, metodi come il multislice a fononi congelati (frozen-phonon) possono essere più appropriati, poiché non si basano sulla stessa ipotesi di cella periodica piccola.

In ReciPro, il modo più semplice per comprendere lo STEM è il seguente: si parte dalla stessa onda convergente del CBED e si sostituisce poi l'osservabile del disco di diffrazione con un'integrazione del rivelatore sul piano di diffrazione.

---

## Parametri pratici

- **Angolo del rivelatore**: BF / ABF / ADF / HAADF sono definizioni di $D(\mathbf Q)$ e $f'_{\kappa}(\mathbf g;\theta_1,\theta_2)$.
- **Numero di fasci**: Le componenti dell'immagine ad alta frequenza e il channeling sono sensibili al numero di fasci inclusi.
- **Passo di spessore**: Se si utilizza l'integrazione numerica per fette, verificare la variazione quando lo spessore della fetta viene dimezzato.
- **Risoluzione angolare**: Fissa la griglia di direzioni $N$ della sonda (vedi [Campionamento angolare della sonda](#angular-sampling)). Il costo cresce come $N^{2}$, per cui è la leva principale sul tempo di calcolo.
- **Modello TDS**: Per il contrasto $Z$ in HAADF, il termine TDS è importante quanto il termine elastico.

## Vedi anche

- [Calcolo dinamico (nucleo comune)](calculation.md)
- [Appendice A3. Diffrazione dinamica con il metodo delle onde di Bloch](index.md)
- [9.2. Simulazione STEM](../../9-hrtem-stem-simulator/2-stem-simulation.md)

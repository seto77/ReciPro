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
- **Modello TDS**: Per il contrasto $Z$ in HAADF, il termine TDS è importante quanto il termine elastico.

## Vedi anche

- [Calcolo dinamico (nucleo comune)](calculation.md)
- [Appendice A3. Diffrazione dinamica con il metodo delle onde di Bloch](index.md)
- [9.2. Simulazione STEM](../../9-hrtem-stem-simulator/2-stem-simulation.md)

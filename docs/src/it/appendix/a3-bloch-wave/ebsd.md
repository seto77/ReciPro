# Calcolo EBSD

EBSD (diffrazione di elettroni retrodiffusi) utilizza lo stesso nucleo Bethe/onde di Bloch di CBED e STEM, ma il problema è posto in modo diverso. CBED e STEM sono **problemi a fascio incidente**: un'onda elettronica entra dall'esterno del campione e si calcola l'onda uscente. EBSD è un **problema di direzione di uscita**: gli elettroni che hanno subito una diffusione anelastica all'interno del campione emergono come elettroni retrodiffusi, e il calcolo chiede quanta intensità esce in ciascuna direzione esterna.

ReciPro converte questo problema di direzione di uscita in un ordinario problema a fascio incidente mediante il teorema di reciprocità. Calcola dapprima un **master pattern** nello spazio delle direzioni, poi combina questo master pattern con i pesi Monte Carlo di profondità / energia / direzione e con la geometria del rivelatore per formare il pattern del rivelatore.

---

## Riformulazione con il teorema di reciprocità

Se l'ampiezza da un punto sorgente interno $\mathbf r_n$ verso una direzione esterna $\widehat{\mathbf s}$ venisse calcolata direttamente, sarebbe necessario un problema di diffusione separato per ogni punto sorgente. Questo non è praticabile.

Il teorema di reciprocità riscrive il problema come segue: l'ampiezza affinché un elettrone che parte da $\mathbf r_n$ compaia nella direzione di campo lontano $\widehat{\mathbf s}$ è uguale all'ampiezza, in $\mathbf r_n$, di un'onda reciproca incidente dalla direzione esterna $-\widehat{\mathbf s}$. Questa onda reciproca è un'ordinaria soluzione Bethe/onde di Bloch. Scrivendola come $\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r)$, l'intensità EBSD nella direzione $\widehat{\mathbf s}$ può essere scritta come

$$I_{\mathrm{EBSD}}(\widehat{\mathbf s};E,z)\propto
\sum_n \sigma_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n;E,z)\right|^2$$

dove $\sigma_n(E,z)$ è il peso per la diffusione anelastica in prossimità della posizione atomica $\mathbf r_n$ nel canale di retrodiffusione all'energia $E$ e alla profondità $z$. I termini sorgente vengono sommati come intensità, non come somma coerente di ampiezze, perché si assume che la diffusione anelastica distrugga la relazione di fase tra diverse posizioni sorgente.

---

## Master Pattern

Il master pattern EBSD memorizza la parte di diffrazione dinamica specifica del cristallo dell'espressione precedente su una griglia di direzioni. Concettualmente,

$$M(\widehat{\mathbf s};E,z)=
\sum_n w_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n)\right|^2$$

dove $w_n$ è il peso della sorgente anelastica lato cristallo nella posizione atomica $\mathbf r_n$. ReciPro utilizza il peso empirico

$$w_n \propto Z_n^{1.7}\,\mathrm{occ}_n$$

con numero atomico $Z_n$ e occupazione $\mathrm{occ}_n$. Questo è separato dalla distribuzione di profondità di trasporto / energia prodotta da Monte Carlo.

Nell'implementazione, l'onda di Bloch reciproca viene valutata in ciascuna posizione atomica:

$$\beta_n^{(j)}=
\alpha^{(j)}
\sum_{\mathbf g}C_{\mathbf g}^{(j)}
\exp\!\left[2\pi i(\mathbf k^{(j)}+\mathbf g)\cdot\mathbf r_n\right]$$

Il codice forma poi la matrice di coppie di onde di Bloch

$$S_{jj'}=\sum_n w_n\,\beta_n^{(j)}\,\overline{\beta_n^{(j')}}$$

e l'integrale analitico sullo spessore

$$\mathcal F_{jj'}(t)=
\frac{\exp\!\left[2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})t\right]-1}
{2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})}$$

così che il master pattern viene valutato come

$$M(\widehat{\mathbf s};E,t)=
\mathrm{Re}\left\{\sum_{j,j'}S_{jj'}(E)\,\mathcal F_{jj'}(t)\right\}$$

Nel limite degenere in cui il denominatore è prossimo a zero, $\mathcal F_{jj'}(t)\to t$.

---

## Campionamento nello spazio delle direzioni

Il master pattern non è l'immagine del rivelatore stessa; è una distribuzione di intensità nello spazio delle direzioni solidale con il cristallo. ReciPro campiona questo spazio delle direzioni con una proiezione equiareale di Rosca-Lambert e memorizza le emisfere $+Z$ e $-Z$ come array piani separati. Il campionamento equiareale riduce la distorsione di densità tra i poli e l'equatore.

In questa fase il master pattern dipende dalla struttura cristallina, dalla tensione di accelerazione, dalla profondità, dall'energia e dal modello di assorbimento. La geometria del rivelatore, come il centro del pattern e la posizione dello schermo, non è ancora stata applicata.

---

## Pesi Monte Carlo e pattern del rivelatore

Per ottenere un pattern del rivelatore EBSD vicino all'osservabile sperimentale, il master pattern deve essere pesato in base a quanti elettroni retrodiffusi emergono da ciascuna profondità, energia e direzione. Scrivendo questo peso di trasporto come

$$W(E,z;\widehat{\mathbf s})$$

e usando $\widehat{\mathbf s}(\mathbf p)$ per la direzione solidale con il cristallo corrispondente al pixel del rivelatore $\mathbf p$, il pattern finale del rivelatore è

$$P(\mathbf p)=
\sum_{i,j}
W(E_i,z_j;\widehat{\mathbf s}(\mathbf p))\,
M(\widehat{\mathbf s}(\mathbf p);E_i,z_j)$$

come somma discreta su energia e profondità.

La parte Monte Carlo traccia la diffusione elastica, la diffusione anelastica, la perdita di energia e la fuoriuscita attraverso la superficie del campione. Per gli elettroni retrodiffusi costruisce distribuzioni di profondità, energia e direzione di uscita. ReciPro distingue i modelli che usano l'ultima posizione di diffusione anelastica e l'energia immediatamente successiva come sorgente effettiva, dai modelli che usano la profondità di fuga e l'energia di fuga.

---

## Sfondo TDS e modello di assorbimento

I pattern EBSD contengono non solo la struttura geometrica delle bande di Kikuchi, ma anche uno sfondo regolare dovuto alla diffusione termica diffusa (TDS). Quando `IncludeTDSBackground` è abilitato, ReciPro valuta la componente TDS diffusa nell'emisfero posteriore,

$$\pi/2\leq\theta\leq\pi$$

come una matrice di assorbimento $\mu_{\mathrm{back}}$ e aggiunge l'intensità di sfondo usando la stessa sommatoria su coppie di onde di Bloch del master pattern. Poiché viene riutilizzata la stessa soluzione agli autovalori, lo sfondo TDS aggiunge un costo aggiuntivo relativamente ridotto.

Quando `UseNonLocalAbsorption` è abilitato, il potenziale assorbente non viene trattato semplicemente come $U'_{\mathbf g-\mathbf h}$, ma come una forma non locale che dipende dalla direzione e dalle coppie di fasci. Questo può migliorare l'accuratezza, ma richiede anche la ricostruzione della matrice di assorbimento per le direzioni della griglia del master pattern, e può quindi aumentare notevolmente il tempo di calcolo.

---

## Parametri pratici

- **Numero di fasci**: Un numero troppo ridotto di fasci fa perdere i dettagli delle bande di Kikuchi e la struttura delle bande HOLZ. Gli assi di zona a basso indice possono richiedere diverse centinaia di fasci.
- **Array di profondità ed energia**: Se questi sono più grossolani della scala di variazione del peso Monte Carlo $W(E,z;\widehat{\mathbf s})$, gli effetti di larghezza di banda dipendente dall'energia e di profondità di channeling vengono mediati via.
- **Geometria del rivelatore**: Il centro del pattern, la distanza dello schermo e l'inclinazione del campione determinano la mappatura $\widehat{\mathbf s}(\mathbf p)$, quindi il pattern del rivelatore può cambiare anche quando il master pattern rimane invariato.
- **Interpretazione di reciprocità**: Il master pattern non è l'immagine del rivelatore. Diventa un pattern del rivelatore solo dopo la pesatura Monte Carlo e la proiezione sul rivelatore.
- **Sfondo TDS**: Abilitalo per confronti quantitativi del contrasto di banda. Disabilitalo quando la struttura geometrica di Kikuchi è più facile da ispezionare senza lo sfondo regolare.

## Vedi anche

- [Calcolo dinamico (nucleo comune)](calculation.md)
- [Appendice A3. Diffrazione dinamica con il metodo delle onde di Bloch](index.md)
- [12. Simulazione EBSD](../../12-ebsd-simulation.md)

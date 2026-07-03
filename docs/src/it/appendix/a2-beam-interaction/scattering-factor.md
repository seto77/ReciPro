# Fattori di diffusione atomici

Il **fattore di diffusione atomico** (o *fattore di forma*) misura quanto fortemente un singolo atomo diffonde il fascio incidente in funzione della variabile di diffusione $s=\sin\theta/\lambda$. Le tre radiazioni interagiscono con parti completamente diverse dell'atomo, perciò i loro fattori di diffusione hanno grandezze, unità e dipendenza angolare differenti. Questo è il motivo più importante per cui la scheda **Fattori di diffusione** appare così diversa tra fascio di raggi X, di elettroni e di neutroni.

=== "X-ray"
    ![Fattori di diffusione — raggi X](../../../assets/cap-it-auto/FormBeamInteraction-xray-scattering.png)

=== "Electron"
    ![Fattori di diffusione — elettrone](../../../assets/cap-it-auto/FormBeamInteraction-electron-scattering.png)

=== "Neutron"
    ![Fattori di diffusione — neutrone](../../../assets/cap-it-auto/FormBeamInteraction-neutron-scattering.png)

---

## Raggi X — diffusione da parte della nuvola elettronica

I raggi X sono diffusi dagli **elettroni** dell'atomo. Un singolo elettrone libero diffonde con la sezione d'urto differenziale classica di **Thomson**, fissata dal raggio classico dell'elettrone $r_e = e^2/(4\pi\varepsilon_0 m_e c^2) \approx 2.82\times10^{-5}\ \text{Å}$:

$$\left(\frac{d\sigma}{d\Omega}\right)_e = r_e^2\,\frac{1+\cos^2 2\theta}{2}.$$

Gli elettroni dell'atomo sono distribuiti nello spazio con densità numerica $\rho_e(\mathbf r)$, e il fattore di diffusione atomico è la **trasformata di Fourier** di quella densità. La sezione d'urto atomica è allora la sezione d'urto del singolo elettrone scalata di $|f_0|^2$:

$$f_0(\mathbf Q) = \int \rho_e(\mathbf r)\, e^{\,i\mathbf Q\cdot\mathbf r}\, d^3r ,
\qquad
\left(\frac{d\sigma}{d\Omega}\right)_\text{atom} = r_e^2\,\frac{1+\cos^2 2\theta}{2}\,|f_0(s)|^2 .$$

- Nella direzione in avanti ($s\to 0$) ogni elettrone diffonde in fase, quindi $f_0(0) = Z$, il numero atomico. Il fattore è espresso in **unità elettroniche** (multipli dell'ampiezza di Thomson — la seconda equazione qui sopra lo rende esplicito).
- All'aumentare di $s$, la diffusione da parti diverse della nuvola va fuori fase e $f_0(s)$ decresce. Una distribuzione elettronica diffusa (esterna, di valenza) fa calare rapidamente $f_0$; gli elettroni di core fortemente legati continuano a contribuire fino a valori elevati di $s$.

In pratica $f_0(s)$ è tabulato come somma di gaussiane (la forma analitica di **Waasmaier–Kirfel** che ReciPro utilizza, un'estensione delle più vecchie tabelle di Cromer–Mann),

$$f_0(s) = \sum_{i} a_i\, e^{-b_i s^2} + c ,$$

che è ciò che ReciPro valuta per la curva. I coefficienti sono tabulati per $s$ in Å⁻¹, perciò ciascun $b_i$ ha unità di Å²; ReciPro mantiene $s^2$ internamente in nm⁻² e applica la conversione con il fattore 100 indicata nell'[indice](index.md).

### Dispersione anomala (risonante)

L'immagine della trasformata di Fourier presuppone che gli elettroni diffondano come se fossero liberi. Quando l'energia del fotone si avvicina a una **soglia di assorbimento**, gli elettroni legati rispondono in modo risonante e compaiono due termini di correzione dipendenti dall'energia:

$$f(s,E) = f_0(s) + f'(E) + i\,f''(E) \qquad \text{(textbook, } e^{+i\phi}\ \text{convention).}$$

- $f'(E)$ : correzione reale di dispersione (riduce il numero effettivo di elettroni in prossimità di una soglia).
- $f''(E)$ : parte immaginaria, massima appena sopra una soglia.
- Le due sono legate dalle relazioni di **Kramers–Kronig**, perciò un picco nell'assorbimento ($f''$) è accompagnato da un'oscillazione dispersiva in $f'$.

Questi non sono parametri liberi. La causalità (Kramers–Kronig) lega $f'$ a $f''$, e il **teorema ottico** lega $f''$ direttamente alla sezione d'urto di fotoassorbimento:

$$f'(E) = \frac{2}{\pi}\,\mathcal{P}\!\!\int_0^\infty \frac{E'\,f''(E')}{E'^2 - E^2}\,dE',
\qquad
f''(E) = \frac{\sigma_\text{abs}(E)}{2\,r_e\,\lambda}.$$

Qui $\sigma_\text{abs}$ è essenzialmente la parte di **fotoassorbimento** dell'attenuazione (non i termini di Rayleigh/Compton) — la stessa struttura di soglia visibile nella pagina [Attenuazione e trasporto](attenuation-transport.md).

ReciPro valuta $f'$ e $f''$ all'energia corrente con la libreria **xraylib** inclusa e li elenca nella tabella (con $f'' > 0$). Due aspetti riguardanti il segno sono importanti. Primo, xraylib restituisce $F_{ii}$ con segno opposto rispetto alla convenzione cristallografica, perciò ReciPro lo nega per riportare un **$f''$ positivo**. Secondo, sotto la convenzione di fase $\exp(-2\pi i\,\mathbf g\cdot\mathbf r)$ di ReciPro il fattore complesso che entra effettivamente nel fattore di struttura è $f_0 + f' - i f''$ — il $+i f''$ scritto sopra appartiene alla convenzione opposta ($e^{+2\pi i}$). Per questo `F_inv` (la parte immaginaria del fattore di struttura) diventa diversa da zero in prossimità di una soglia — vedi [Fattore di struttura](structure-factor.md).

---

## Elettroni — diffusione da parte del potenziale elettrostatico

Un elettrone veloce è carico, perciò è diffuso dal **potenziale elettrostatico** $V(\mathbf r)$ dell'atomo — la combinazione del nucleo positivo e della nuvola elettronica negativa. Il fattore di diffusione elettronico $f_e$ è quindi la trasformata di Fourier del potenziale, che attraverso l'equazione di Poisson lo collega al fattore per raggi X. Il risultato è la **relazione di Mott–Bethe**:

$$f_e(s) = C_\text{MB}\,\frac{Z - f_0(s)}{s^2} \;\;\propto\; \frac{Z - f_X(Q)}{Q^2}.$$

Il prefattore $C_\text{MB}$ è costituito da costanti fondamentali e dipende dal sistema di unità e dal fatto che si usi $s$ o $Q$. ReciPro non valuta direttamente questa relazione — utilizza le forme adattate di Peng / Kirkland / 8 gaussiane riportate sotto — perciò è data qui per intuizione fisica più che per il calcolo. Scritta esplicitamente con le costanti (per $s$ e $f_e$ in Å),

$$f_e(s)\,[\text{Å}] = \frac{m_e e^2}{8\pi\varepsilon_0 h^2}\,\frac{Z - f_0(s)}{s^2} \simeq 0.023934\,\frac{Z - f_0(s)}{s^2}, \qquad s\ \text{in Å}^{-1},$$

con un ulteriore $\times 0.1$ quando ReciPro riporta $f_e$ in nm, e un ulteriore fattore relativistico $\gamma$ (sotto) nel potenziale dinamico.

La fisica è nel numeratore $Z - f_0$: l'elettrone vede la **differenza** tra la carica nucleare $Z$ e la nuvola elettronica di schermatura $f_0$, ovvero il potenziale atomico netto.

- **Grandezza.** A causa del fattore $1/s^2$, $f_e$ è fortemente piccato verso i piccoli angoli ed è di gran lunga maggiore (nelle sue proprie unità) e più orientato in avanti rispetto a $f_0$. Per questo la diffrazione elettronica è dominata dalle riflessioni di ordine basso e per questo la diffusione dinamica (multipla) è rilevante — vedi [Appendice A3](../a3-bloch-wave/index.md).
- **Limite a piccoli angoli.** Per un atomo *neutro* sia $Z-f_0\to 0$ sia $s^2\to 0$, perciò $f_e(0)$ è finito (un limite $0/0$ fissato dal raggio atomico quadratico medio). Per uno **ione** la nuvola non annulla più $Z$ e la coda coulombiana a lungo raggio fa divergere $f_e$ per $s\to 0$; i fattori elettronici ionici tabulati devono essere trattati con cautela agli angoli più piccoli.
- **Correzione relativistica.** Alle energie del TEM la massa e la lunghezza d'onda dell'elettrone sono relativistiche. La lunghezza d'onda usa la forma relativistica $\lambda = h/\sqrt{2 m_0 e U\,(1 + e U/2 m_0 c^2)}$, e il potenziale di interazione porta il fattore relativistico $\gamma = 1 + eU/m_0c^2$. ReciPro applica questa correzione nel formare il potenziale dinamico.

ReciPro offre tre parametrizzazioni di $f_e(s)$:

- **Peng** : un fit a cinque gaussiane, $f_e(s)=\sum_i a_i e^{-b_i s^2}$, comodo e largamente usato per la diffusione elettronica elastica.
- **Kirkland** : un fit misto Lorentziana + gaussiana, $f_e(q)=\sum_i \dfrac{a_i}{q^2+b_i} + \sum_i c_i\,e^{-d_i q^2}$. **La sua variabile indipendente è $q = 2s = 1/d$, non $s$** — frequente fonte di errori di un fattore due nel confronto tra modelli ($q$ in Å⁻¹, con i coefficienti adattati $a_i,b_i,c_i,d_i$ nelle unità corrispondenti).
- **8-Gaussians** : un fit a otto termini valido su un intervallo di $s$ più ampio.

**Quale scegliere.** Tutti e tre adattano lo stesso $f_e(s)$ sottostante e concordano strettamente a piccolo $s$; differiscono principalmente nell'intervallo e nel modo in cui è rappresentato il core atomico. **Peng** (atomi neutri e ioni comuni, accurato fino a $s\approx2\text{–}6$ Å⁻¹) è il valore predefinito usuale per i fattori di struttura SAED/CBED; **Kirkland** si estende a $s$ più elevati con un termine di core Lorentziano, adatto a HRTEM/STEM (ricorda $q=2s$); **8-Gaussians** è per riflessioni che raggiungono $s$ molto elevati. Per un elemento leggero i tre sono pressoché indistinguibili; le differenze emergono per gli elementi pesanti ad angolo elevato.

---

## Neutroni — diffusione da parte del nucleo

I neutroni termici sono privi di carica e interagiscono con la materia principalmente tramite la **forza nucleare forte**, il cui raggio d'azione (femtometri) è del tutto trascurabile rispetto alla lunghezza d'onda del neutrone (ångström). L'interazione è rappresentata dallo **pseudopotenziale di Fermi**, una sorgente puntiforme la cui intensità è la lunghezza di diffusione $b$:

$$V(\mathbf r) = \frac{2\pi\hbar^2}{m_n}\,b\,\delta(\mathbf r)
\qquad\Longrightarrow\qquad
\frac{d\sigma}{d\Omega} = |b|^2 .$$

Poiché il diffusore è puntiforme, $b$ è **indipendente da $s$** — non c'è alcun decadimento del fattore di forma, ed è per questo che la scheda **Fattori di diffusione** non traccia alcuna curva per i neutroni e mostra invece una tabella delle lunghezze di diffusione.

- $b$ è una proprietà del **nuclide**, non della configurazione elettronica. Varia in modo irregolare da elemento a elemento (e tra gli isotopi), può essere **negativa** (ad es. ¹H, Ti, Mn), e non ha alcuna relazione monotòna con $Z$. Questa è la base del contrasto neutronico (atomi leggeri vicino a quelli pesanti, marcatura isotopica).
- **Coerente vs incoerente.** Un elemento reale è una miscela di isotopi e stati di spin nucleare con $b$ differente. La separazione $b = \langle b\rangle + \delta b$ dà una parte coerente (dalla media) e una parte incoerente (dalla dispersione):

$$\sigma_\text{coh} = 4\pi\,|\langle b\rangle|^2, \qquad \sigma_\text{inc} = 4\pi\big(\langle |b|^2\rangle - |\langle b\rangle|^2\big), \qquad \sigma_s = \sigma_\text{coh} + \sigma_\text{inc}.$$

  La parte coerente produce diffrazione di Bragg (è ciò che entra nel fattore di struttura); la parte incoerente è un fondo piatto e isotropo (grande per ¹H, la ragione della deuterazione).

!!! note "Valori tabulati"
    ReciPro legge $b_\text{coh}$ e le sezioni d'urto da una tabella dei nuclidi anziché calcolarle. Per i nuclidi risonanti il $\sigma_\text{coh}$ elencato non deve necessariamente essere uguale al valore ingenuo $4\pi b^2$, perciò i valori della tabella sono autoritativi. La diffusione neutronica magnetica (dovuta agli spin elettronici spaiati, che *ha* effettivamente un fattore di forma dipendente da $s$) non è modellata qui.

---

## In sintesi

| | X-ray | Electron | Neutron |
|---|---|---|---|
| Diffuso da | nuvola elettronica $\rho_e(\mathbf r)$ | potenziale elettrostatico $V(\mathbf r)$ | nucleo (punto) |
| Dipendenza da $s$ | decresce (FT della nuvola) | $\propto (Z-f_0)/s^2$, fortemente in avanti | nessuna ($b$ costante) |
| Valore in avanti | $f_0(0)=Z$ | finito (neutro) / divergente (ione) | $b$ |
| Dipendenza dall'energia | $f',f''$ presso le soglie | relativistico $\lambda,\gamma$ | $\sigma_\text{abs}\propto 1/v$ (non $b$) |
| Ordine di grandezza tipico | $\propto Z$ | piccato in avanti, cresce con $Z$ | irregolare, può essere $<0$ |

---

## Vedi anche

- [Indice — geometria e la variabile $s$](index.md)
- [Fattore di struttura](structure-factor.md) — come questi fattori si combinano su una cella elementare.
- [3. Interazione del fascio → scheda Fattori di diffusione](../../3-beam-interaction.md#scattering-factors-tab)

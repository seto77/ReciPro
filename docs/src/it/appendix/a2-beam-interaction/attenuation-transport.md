# Attenuazione e trasporto

I fattori di diffusione descrivono un singolo evento di diffusione; questa pagina riguarda ciò che accade al fascio **nel suo insieme** mentre attraversa il solido — quanto rapidamente viene rimosso, quanto in profondità penetra e (per gli elettroni) come viene rallentato. La fisica rilevante è del tutto diversa per i tre fasci, ed è per questo che la scheda **Attenuazioni & trasporto** modifica così drasticamente i suoi grafici e le sue tabelle a seconda della radiazione.

=== "X-ray"
    ![Attenuazioni & trasporto — X-ray](../../../assets/cap-it-auto/FormBeamInteraction-xray-attenuations.png)

=== "Electron"
    ![Attenuazioni & trasporto — electron](../../../assets/cap-it-auto/FormBeamInteraction-electron-attenuations.png)

=== "Neutron"
    ![Attenuazioni & trasporto — neutron](../../../assets/cap-it-auto/FormBeamInteraction-neutron-attenuations.png)

---

## Raggi X — assorbimento e rifrazione

### Attenuazione di Beer–Lambert

Un fascio di raggi X monocromatico viene rimosso esponenzialmente con la lunghezza del cammino:

$$I(t) = I_0\, e^{-\mu t}, \qquad \mu = \rho\,(\mu/\rho).$$

- $\mu/\rho$ : il **coefficiente di attenuazione di massa** (cm²/g) — la grandezza tabulata, indipendente dalla densità.
- $\mu$ : il **coefficiente di attenuazione lineare** (cm⁻¹) alla densità effettiva $\rho$ del materiale.
- $1/\mu$ : la **lunghezza di attenuazione** (l'intensità scende a $1/e$).
- $\text{HVL} = \ln 2/\mu$ : lo **spessore emivalente**.
- $T = e^{-\mu t}$ : la trasmissione per un campione di spessore $t$.

### Da cosa è composto $\mu/\rho$

L'attenuazione di massa totale è la somma di tre processi, rappresentati separatamente nella scheda:

$$\left(\frac{\mu}{\rho}\right)_\text{total} = \left(\frac{\tau}{\rho}\right)_\text{photo} + \left(\frac{\mu}{\rho}\right)_\text{Rayleigh} + \left(\frac{\mu}{\rho}\right)_\text{Compton}.$$

Per un composto l'attenuazione di massa è la somma pesata in massa dei valori elementari, mentre il coefficiente lineare somma direttamente le sezioni d'urto atomiche:

$$\left(\frac{\mu}{\rho}\right)_\text{mix} = \sum_i w_i\left(\frac{\mu}{\rho}\right)_i, \qquad \mu = \sum_i n_i\,\sigma_i,$$

con $w_i$ le frazioni in massa e $n_i$ le densità numeriche. Le tre componenti sono:

- **Fotoassorbimento** $\tau$ — un fotone viene assorbito ed espelle un elettrone legato. Domina alle basse energie, decrescendo all'incirca come $\tau/\rho \propto Z^{3\!-\!4}/E^{3}$ tra le soglie. È il termine che espelle l'elettrone di guscio interno la cui rilassazione produce la [fluorescenza](fluorescence.md).
- **Diffusione Rayleigh (coerente)** — diffusione elastica sugli elettroni legati, legata al fattore di forma coerente $F(q)$.
- **Diffusione Compton (incoerente)** — diffusione anelastica sugli elettroni debolmente legati, legata alla funzione incoerente $S(q)$; cresce in importanza relativa alle alte energie. Il fotone diffuso subisce uno spostamento in lunghezza d'onda pari a

$$\Delta\lambda = \lambda' - \lambda = \frac{h}{m_e c}\,(1-\cos\varphi),$$

  cosicché un evento Compton rimuove il fotone dal fascio monocromatico (una perdita anelastica).

Le **soglie di assorbimento** sono i ripidi aumenti di $\tau$ quando l'energia del fotone supera l'energia di legame di un guscio ($K$, $L_3$, …), aprendo un nuovo canale di ionizzazione. Il **rapporto di salto** è il fattore di cui $\mu/\rho$ aumenta attraverso la soglia; ReciPro elenca le energie e i salti delle soglie $K$ e $L_3$. Il **coefficiente di assorbimento di energia di massa** $\mu_\text{en}/\rho$ è la parte di $\mu/\rho$ che deposita energia localmente (escludendo l'energia trasportata via dai fotoni diffusi e di fluorescenza).

### Rifrazione, angolo critico e SLD

L'indice di rifrazione dei raggi X di un solido è **leggermente minore di 1**, scritto come

$$n = 1 - \delta + i\beta, \qquad \beta = \frac{\mu_\text{abs}\lambda}{4\pi} = \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,f''_i, \qquad \delta \simeq \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,(Z_i+f'_i),$$

dove $n_i$ è la densità numerica dell'elemento $i$ e $r_e$ il raggio classico dell'elettrone. Qui $\mu_\text{abs}$ è la parte assorbitiva dell'attenuazione (legata a $f''$); non deve necessariamente essere uguale al $\mu$ totale visto sopra, che contiene anche la diffusione Rayleigh e Compton. Poiché $n<1$, i raggi X subiscono una **riflessione esterna totale** al di sotto di un piccolo **angolo critico** radente

$$\theta_c \simeq \sqrt{2\delta}.$$

Ciò deriva dalla geometria della rifrazione: per un angolo radente $\alpha$ il vettore d'onda verticale all'interno del solido è $k_z^2 \simeq k^2(\alpha^2 - 2\delta)$, che raggiunge lo zero per $\alpha = \alpha_c = \sqrt{2\delta}$; al di sotto di tale valore l'onda non può propagarsi nel materiale e viene totalmente riflessa. La parte reale della **densità di lunghezza di diffusione**, $\text{SLD} = r_e\sum_i n_i (Z_i + f'_i)$, determina $\delta$ ed è l'analogo per i raggi X della SLD neutronica usata in riflettometria. ReciPro riporta $\delta$, $\beta$, $\theta_c$ e la SLD dei raggi X nella tabella scalare.

---

## Elettroni — diffusione, rallentamento e portata

Un elettrone veloce in un solido sia **diffonde** (cambiando direzione) sia **perde** energia con continuità, cosicché il suo trasporto richiede più di una scala di lunghezza.

### Diffusione elastica e libero cammino medio

La sezione d'urto elastica $\sigma_\text{el}$ misura quanto facilmente un singolo atomo devia l'elettrone. ReciPro usa le sezioni d'urto **NIST Mott** (una soluzione a onde parziali dell'equazione relativistica di Dirac nel potenziale atomico schermato), valide all'incirca su **50 eV – 36.4 keV**; al di fuori di tale intervallo, o per elementi non presenti in tabella, ripiega sull'approssimazione di **Rutherford schermata**. Le due non devono necessariamente raccordarsi in modo perfettamente liscio al confine. La sezione d'urto totale è l'integrale angolare di quella differenziale,

$$\sigma_\text{el} = 2\pi\int_0^\pi \frac{d\sigma}{d\Omega}\,\sin\Theta\,d\Theta, \qquad \frac{d\sigma}{d\Omega} \propto \frac{Z^2}{E^2}\,\frac{1}{\big[\sin^2(\Theta/2)+\eta\big]^2},$$

dove il parametro di schermatura $\eta$ arrotonda la divergenza in avanti della sezione d'urto di Rutherford nuda; il trattamento di Mott include inoltre gli effetti di spin e relativistici che il modello di Rutherford schermato omette. A partire dalla sezione d'urto,

$$\Sigma_\text{el} = \sum_i n_i\,\sigma_{\text{el},i}, \qquad \lambda_\text{el} = \frac{1}{\Sigma_\text{el}},$$

forniscono il coefficiente di diffusione macroscopico e il **libero cammino medio elastico** — la distanza media tra gli eventi elastici.

### Potere frenante e perdite anelastiche

L'energia viene persa principalmente per eccitazioni elettroniche (ionizzazione, plasmoni). Il **potere frenante** è definito come grandezza positiva,

$$S(E) = -\frac{dE}{ds} > 0,$$

dove qui $s$ è la **lunghezza del cammino** lungo la traiettoria (la variabile della curva *|dE/ds|* della scheda), non la variabile di diffusione $\sin\theta/\lambda$ usata altrove in questa appendice. Il gradiente di energia $dE/ds$ è negativo, quindi la scheda rappresenta $S$ verso l'alto. Alle energie del keV segue, concettualmente, la forma di **Bethe**

$$S(E) \;\propto\; \frac{Z\rho}{A}\,\frac{1}{E}\,\ln\!\frac{E}{J},$$

con $J$ l'**energia media di eccitazione** del solido. Questo schizzo non relativistico mostra soltanto lo scaling; ReciPro valuta una forma corretta/empirica (del tipo Joy–Luo) che si mantiene regolare alle basse energie. L'**energia del plasmone** $E_p$ nella tabella scalare è una caratterizzazione collegata ma distinta delle stesse eccitazioni elettroniche. Il **libero cammino medio anelastico** (IMFP) è la corrispondente distanza media tra le collisioni con perdita di energia; ReciPro può valutarlo dalla formula predittiva **TPP-2M**,

$$\lambda_\text{in}(E) = \frac{E}{E_p^2\left[\beta_\text{T}\ln(\gamma_\text{T} E) - C/E + D/E^2\right]},$$

con $E$ in eV, $\lambda_\text{in}$ in Å e i parametri $\beta_\text{T},\gamma_\text{T},C,D$ costruiti a partire da $E_p$, dalla densità, dal gap di banda e dal numero di elettroni di valenza.

### Due tipi di portata

- **Portata CSDA** — l'approssimazione di rallentamento continuo (continuous-slowing-down approximation) integra il potere frenante per fornire la lunghezza totale del cammino percorso prima che l'elettrone si fermi:

$$R_\text{CSDA} = \int_{E_\text{cut}}^{E_0} \frac{dE}{S(E)}.$$

(In pratica l'integrale scende fino a un valore di taglio a bassa energia $E_\text{cut}$, al di sotto del quale lo schizzo di Bethe di cui sopra non vale più.)

- **Portata di Kanaya–Okayama** — una stima empirica ampiamente usata della **profondità di penetrazione** (non della lunghezza del cammino), che tiene conto della traiettoria tortuosa e diffusa:

$$R_\text{KO}\,[\mu\text{m}] = 0.0276\,\frac{A\,E_0^{1.67}}{\rho\,Z^{0.89}}, \qquad (E_0\ \text{in keV}).$$

Le due rispondono a domande diverse — distanza totale percorsa vs. quanto in profondità nel solido arriva l'elettrone — e perciò differiscono in valore, e ReciPro le riporta entrambe. Queste portate determinano il volume di interazione alla base delle simulazioni delle [traiettorie elettroniche](../../8-electron-trajectory.md) e dell'EBSD.

---

## Neutroni — sezione d'urto macroscopica e la legge 1/v

Per i neutroni non esiste una curva di attenuazione dipendente dall'energia; l'interazione è fissata dalle **sezioni d'urto nucleari**. Il fascio viene attenuato attraverso la sezione d'urto totale macroscopica, a sua volta somma delle parti coerente, incoerente e di assorbimento:

$$\Sigma_\text{total} = \sum_i n_i\,\sigma_{\text{total},i}, \qquad \sigma_\text{total} = \sigma_\text{coh} + \sigma_\text{inc} + \sigma_\text{abs}(\lambda), \qquad T = e^{-\Sigma_\text{total} t},$$

con lunghezza di attenuazione $1/\Sigma_\text{total}$. La parte di assorbimento dipende dalla velocità del neutrone $v$ (e quindi dalla lunghezza d'onda): per la maggior parte dei nuclidi il tempo trascorso in prossimità del nucleo scala come $1/v$, dando la **legge 1/v**

$$\sigma_\text{abs}(\lambda) = \sigma_\text{abs}(\lambda_0)\,\frac{\lambda}{\lambda_0}, \qquad \lambda_0 = 1.798\ \text{Å}\ (\text{thermal}, 2200\ \text{m/s}).$$

Alcuni forti assorbitori (Cd, Sm, Eu, Gd) presentano **risonanze** a bassa energia che violano il semplice scaling 1/v; ReciPro segnala questi nuclidi. La **densità di lunghezza di diffusione** coerente, $\text{SLD} = \sum_i n_i\, b_{\text{coh},i}$, è l'analogo neutronico della SLD dei raggi X vista sopra.

---

## La penetrazione a colpo d'occhio

I tre fasci sondano profondità enormemente diverse — la ragione pratica per cui rispondono a domande diverse:

| Fascio | Campione tipico | Penetrazione (ordine di grandezza) | Determinata da |
|---|---|---|---|
| Raggi X (≈8 keV) | polvere / monocristallo | 10–100 µm | $\mu = \rho(\mu/\rho)$ |
| Elettrone (≈200 keV) | lamina TEM | 10–100 nm (utile) | MFP elastico + perdita anelastica |
| Neutrone (termico) | volume, dimensione cm | 1–10 cm | $\Sigma_\text{total}$ |

Le stesse scale di lunghezza spiegano perché gli elettroni richiedono campioni ultrasottili e teoria dinamica, mentre i neutroni vedono un intero campione massivo in regime di diffusione singola cinematica.

---

## Vedi anche

- [Fattori di diffusione atomici](scattering-factor.md) — la separazione $F(q)$/$S(q)$ dietro Rayleigh/Compton, e le sezioni d'urto di Mott.
- [Fluorescenza](fluorescence.md) — la rilassazione che segue il fotoassorbimento dei raggi X.
- [3. Interazione del fascio](../../3-beam-interaction.md) — la scheda *Attenuazioni & trasporto*.
- [8. Traiettorie elettroniche](../../8-electron-trajectory.md) · [12. Simulazione EBSD](../../12-ebsd-simulation.md) — dove si usano le portate degli elettroni.

# Fluorescenza

Quando il **fotoassorbimento** dei raggi X espelle un elettrone di una shell interna (vedi [attenuazione & trasporto](attenuation-transport.md)), lascia una lacuna in un livello profondo. L'atomo si rilassa facendo cadere un elettrone esterno nella lacuna, e l'energia rilasciata esce o come **fotone X caratteristico** (fluorescenza) o espellendo un secondo elettrone (il processo **Auger**). La scheda **Fluorescenza** mostra un'anteprima del canale dei fotoni caratteristici; vale solo per i raggi X ed è nascosta per i fasci di elettroni e di neutroni.

![Fluorescenza (X-ray)](../../../assets/cap-it-auto/FormBeamInteraction-xray-fluorescence.png)

---

## Linee caratteristiche

Poiché le energie delle shell sono nettamente definite, l'energia del fotone emesso è la **differenza di due energie di legame**,

$$E_\gamma = E_B(\text{inner shell}) - E_B(\text{outer shell}),$$

ed è quindi caratteristica dell'elemento:

- **Linee K** — lacuna nella shell $K$ riempita da $L$ ($K\alpha$) o $M$ ($K\beta$).
- **Linee L** — lacuna nella shell $L$ riempita da $M$/$N$ ($L\alpha$, $L\beta$, …).

Compaiono solo le transizioni consentite dalle regole di selezione di dipolo, motivo per cui lo spettro è costituito da poche linee discrete (K$\alpha_1$, K$\alpha_2$, K$\beta_1$, L$\alpha_1$, …) anziché da un continuo. Le loro energie seguono la **legge di Moseley**; nell'approssimazione idrogenoide schermata,

$$E_{n_2\to n_1} \approx R_\infty hc\,(Z-\sigma)^2\left(\frac{1}{n_1^2} - \frac{1}{n_2^2}\right), \qquad \text{so}\qquad \sqrt{E} \propto (Z-\sigma),$$

con $\sigma$ costante di schermatura. Per $K\alpha$ ($n_2{=}2\to n_1{=}1$, $\sigma\approx1$) questo si riduce a $E_{K\alpha}\approx R_\infty hc\,(Z-1)^2\left(1-\tfrac14\right)$. Questa dipendenza da $Z$ monotona e guidata dal numero di elettroni è la base dell'identificazione elementare (EDX/WDX).

---

## Resa di fluorescenza

La competizione tra rilassamento radiativo e Auger è descritta dalla **resa di fluorescenza**

$$\omega = \frac{\Gamma_r}{\Gamma_r + \Gamma_a},$$

la probabilità che una data lacuna decada emettendo un fotone anziché un elettrone Auger ($\Gamma_r$, $\Gamma_a$ sono rispettivamente la velocità radiativa e quella Auger).

- Per gli **elementi leggeri** il canale Auger domina, quindi $\omega_K$ è piccola (ben al di sotto dell'1% per C, N, O) — gli elementi leggeri fluorescono debolmente, motivo per cui sono difficili da rilevare con EDX.
- Per gli **elementi pesanti** prevale il canale radiativo e $\omega_K \to$ prossima a 1.

La **resa Auger** complementare $a$ prende il resto, quindi

$$\omega + a = 1 ,$$

e una $\omega$ piccola significa che la maggior parte delle lacune decade per emissione Auger. All'interno del canale radiativo, la quota di una particolare linea $\ell$ (ad es. $K\alpha_1$ rispetto a $K\beta_1$) è il suo **rapporto di diramazione**

$$p_{\ell\mid X} = \frac{\Gamma_\ell}{\sum_{\ell'\in X}\Gamma_{\ell'}},$$

la velocità radiativa relativa all'interno della shell $X$. ReciPro elenca $\omega_K$ per ciascun elemento e la linea più intensa dello spettro.

---

## Cosa l'anteprima modella e cosa no

Il grafico delle **linee di emissione EDX** disegna ogni linea caratteristica come uno stelo alla sua energia del fotone, con altezza proporzionale a

$$\text{(atomic fraction)} \times \text{(radiative rate)} \times \omega.$$

Questa è un'anteprima **qualitativa** di dove cadono le linee e delle loro altezze relative approssimative. Tralascia deliberatamente i fattori che uno spettro EDX/XRF reale e quantitativo richiede:

- se l'energia incidente sia effettivamente **al di sopra del bordo di assorbimento** necessario per creare la lacuna — una linea viene disegnata anche se non può essere eccitata all'energia attuale;
- la **sezione d'urto di eccitazione** (con quale efficienza il fascio incidente crea la lacuna all'energia scelta);
- l'**autoassorbimento** dei fotoni emessi all'interno del campione (effetti di matrice);
- l'**efficienza** e la risoluzione del rivelatore.

L'anteprima serve quindi per l'identificazione delle linee e per il ragionamento sulle posizioni relative, non per la determinazione quantitativa della composizione.

---

## Dall'anteprima alla quantificazione

Un'analisi EDX/XRF reale converte le intensità delle linee in concentrazioni mediante una **correzione di matrice (ZAF)** — per il numero atomico ($Z$), l'assorbimento ($A$) dei fotoni emessi lungo il loro percorso di uscita dal campione e la **fluorescenza** secondaria ($F$) eccitata da altre linee — combinata con la sezione d'urto di eccitazione e la risposta del rivelatore menzionate sopra. Nella forma completa, l'intensità misurata della linea $\ell$ dell'elemento $i$ è

$$I_\ell \;\propto\; C_i\,\Phi_0\,\sigma_{\text{ion},X,i}(E_0)\,\omega_{X,i}\,p_{\ell\mid X}\,\epsilon(E_\ell)\,A_\text{matrix}(E_0,E_\ell),$$

con $C_i$ la concentrazione, $\Phi_0$ il flusso incidente, $\sigma_\text{ion}$ la sezione d'urto di ionizzazione, $\omega$ la resa di fluorescenza, $p_{\ell\mid X}$ il rapporto di diramazione, $\epsilon$ l'efficienza del rivelatore e $A_\text{matrix}$ la correzione di assorbimento / fluorescenza secondaria. L'anteprima di ReciPro mantiene solo la parte $C_i\,p_{\ell\mid X}\,\omega$ (frazione atomica × velocità radiativa × resa) e tralascia il resto, così posiziona le linee e ne fornisce le intensità relative intrinseche, in modo che possano essere riconosciute in uno spettro misurato.

---

## Vedi anche

- [Attenuazione & trasporto](attenuation-transport.md) — fotoassorbimento, il bordo che crea la lacuna.
- [Fattori di diffusione atomica](scattering-factor.md) — gli stessi elettroni legati, visti nella diffusione.
- [3. Interazione del fascio → scheda Fluorescenza](../../3-beam-interaction.md#fluorescence-tab)

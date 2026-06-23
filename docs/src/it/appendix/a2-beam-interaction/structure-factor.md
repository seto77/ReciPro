# Fattore di struttura

Il fattore di diffusione atomico descrive un singolo atomo; il **fattore di struttura** descrive come tutti gli atomi della cella elementare diffondono *insieme*. È la grandezza che la scheda **Reflections** riporta in tabella (`F_real`, `F_inv`, $\lvert F\rvert$, $F^2$), ed è il collegamento tra la fisica atomica della pagina precedente e le intensità diffratte.

=== "X-ray"
    ![Reflections — X-ray](../../../assets/cap-it-auto/FormBeamInteraction-xray-reflections.png)

=== "Electron"
    ![Reflections — electron](../../../assets/cap-it-auto/FormBeamInteraction-electron-reflections.png)

=== "Neutron"
    ![Reflections — neutron](../../../assets/cap-it-auto/FormBeamInteraction-neutron-reflections.png)

---

## Interferenza sulla cella elementare

Il fattore di struttura della riflessione $\mathbf g = (hkl)$ è la somma coerente dei fattori di diffusione atomici, ciascuno pesato con la fase derivante dalla posizione frazionaria $\mathbf r_j = (x_j,y_j,z_j)$ dell'atomo:

$$F_{\mathbf g} = \sum_{j} o_j\, f_j(s,E)\, T_j(\mathbf g)\, \exp\!\left(-2\pi i\,(h x_j + k y_j + l z_j)\right).$$

- $o_j$ : occupazione del sito (**occupancy**, frazionaria, per occupazione parziale o mista).
- $f_j(s,E)$ : il fattore di diffusione atomico dell'atomo $j$ per il fascio corrente — $f_0+f'-if''$ per i raggi X nella [convenzione di fase](index.md#phase-convention) di ReciPro, $f_e$ per gli elettroni, $b$ per i neutroni.
- $T_j(\mathbf g)$ : il fattore di Debye–Waller (vedi sotto).
- La fase $-2\pi i$ segue la [convenzione](index.md#phase-convention) di ReciPro.

L'intensità è il modulo quadro,

$$I_{\mathbf g} \;\propto\; \lvert F_{\mathbf g}\rvert^2 = F_\text{real}^2 + F_\text{inv}^2 ,$$

che corrisponde alla colonna $F^2$ della tabella. `F_real` e `F_inv` sono la parte reale e la parte immaginaria del fattore di struttura complesso. Anche con fattori di diffusione atomici puramente reali, $F_{\mathbf g}$ è in generale complesso per una struttura non centrosimmetrica (o con origine spostata); la dispersione anomala dei raggi X ($f$ complesso) e le lunghezze di diffusione neutroniche complesse aggiungono un ulteriore contributo immaginario. `F_inv` si annulla per *ogni* riflessione soltanto quando la struttura è centrosimmetrica con l'origine in un centro di simmetria e tutti i fattori sono reali.

---

## Il fattore di Debye–Waller

Gli atomi vibrano attorno alle loro posizioni di equilibrio, sfumando la densità di diffusione e riducendo i fattori ad alti angoli. Per un moto isotropo,

$$T_j = \exp\!\left(-B_j\, s^2\right), \qquad B_j = 8\pi^2\langle u_j^2\rangle,$$

dove $\langle u_j^2\rangle$ è lo spostamento quadratico medio lungo la direzione di diffusione e $B_j$ è il parametro di spostamento isotropo (Å²). Il moto anisotropo generalizza questo a

$$T_j = \exp\!\left(-2\pi^2\,\mathbf g^{\mathsf T}\!\mathbf U_j\,\mathbf g\right),$$

con $\mathbf U_j$ il tensore di spostamento e $\mathbf g$ il vettore del reticolo reciproco ($|\mathbf g|=1/d$, non $Q=2\pi\lvert\mathbf g\rvert$). Per un solido di Debye lo spostamento quadratico medio è esso stesso una funzione della temperatura $T$, della massa atomica $M$ e della temperatura di Debye $\Theta_D$,

$$\langle u^2\rangle = \frac{3\hbar^2}{M k_B \Theta_D}\left[\frac14 + \left(\frac{T}{\Theta_D}\right)^2\!\int_0^{\Theta_D/T}\frac{x}{e^x-1}\,dx\right],$$

cosicché $B$ cresce con la temperatura e diminuisce per gli atomi pesanti. ReciPro utilizza direttamente i $B_j$ tabulati o inseriti, anziché calcolarli. Poiché $T_j$ moltiplica il fattore di diffusione, la scheda **Scattering factors** può applicare lo stesso smorzamento $e^{-Bs^2}$ alle curve tracciate. Lo smorzamento cresce con la temperatura e con $s$, ed è per questo che la diffusione termica diffusa (intensità sottratta ai fasci di Bragg coerenti e ridistribuita in un fondo diffuso) alimenta il potenziale assorbitivo nella teoria dinamica ([Appendice A3](../a3-bloch-wave/index.md)).

---

## Estinzioni: sistematiche vs. accidentali

Una riflessione può essere **assente** per due motivi distinti:

- **Estinzioni sistematiche (dovute al gruppo spaziale).** La centratura del reticolo e gli elementi di simmetria con una componente traslazionale (assi elicogiri, piani di scorrimento) fanno svanire *esattamente* intere classi di riflessioni, per ogni cristallo di quel gruppo spaziale, indipendentemente dal contenuto atomico. Sono queste le regole alla base di **Hide prohibited planes**.
- **Quasi-estinzioni accidentali.** Quando i contributi atomici si cancellano per caso in una particolare struttura, l'intensità è piccola ma non vietata dalla simmetria, e può ricomparire se la composizione o le posizioni cambiano. Queste *non* vengono rimosse dalle regole di estinzione.

Un'estinzione sistematica è una cancellazione di fase tra le copie della cella legate dalla simmetria. Per le traslazioni di centratura $\mathbf t_\alpha$ il fattore di struttura porta un fattore comune

$$F_{\mathbf g} \propto \sum_\alpha e^{-2\pi i\,\mathbf g\cdot\mathbf t_\alpha},$$

che è nullo per certi $hkl$. Per la centratura interna ($\mathbf t = \tfrac12,\tfrac12,\tfrac12$),

$$1 + e^{-\pi i (h+k+l)} = 0 \quad\Longleftrightarrow\quad h+k+l \ \text{odd}.$$

Le estinzioni sistematiche più comuni sono:

| Elemento di simmetria | Condizione di estinzione | Riflessioni interessate |
|---|---|---|
| $I$ (centratura interna) | $h+k+l$ dispari | tutte le $hkl$ |
| $F$ (centratura a facce) | $h,k,l$ a parità mista | tutte le $hkl$ |
| $C$ (centratura C) | $h+k$ dispari | tutte le $hkl$ |
| asse elicogiro $2_1$ $\parallel b$ | $k$ dispari | $0k0$ |
| piano di scorrimento $a$ $\perp b$ | $h$ dispari | $h0l$ |
| piano di scorrimento $c$ $\perp b$ | $l$ dispari | $h0l$ |

Le condizioni di centratura si applicano a ogni riflessione; le condizioni di asse elicogiro e di piano di scorrimento si applicano solo alla corrispondente fila assiale o zona, ed è proprio questo che le rende diagnostiche del gruppo spaziale.

---

## La legge di Friedel e la sua violazione

Per una struttura con fattori di diffusione reali (non risonanti), il coniugare la somma e l'invertire il segno di $\mathbf g$ mostra direttamente che (sopprimendo i pesi reali $o_j T_j$ per chiarezza)

$$F_{-\mathbf g} = \sum_j f_j\, e^{+2\pi i\,\mathbf g\cdot\mathbf r_j} = \left(\sum_j f_j\, e^{-2\pi i\,\mathbf g\cdot\mathbf r_j}\right)^{*} = F_{\mathbf g}^{*}, \qquad\text{hence}\qquad \lvert F_{hkl}\rvert = \lvert F_{\bar h\bar k\bar l}\rvert \quad\text{(Friedel's law).}$$

La diffrazione appare allora centrosimmetrica anche quando il cristallo non lo è. **La dispersione anomala può violare questo.** Scrivendo il fattore di struttura come una parte normale (che si coniuga in modo pulito) più una parte anomala, $F_{\mathbf g} = A_{\mathbf g} - i B_{\mathbf g}$ e $F_{-\mathbf g} = A_{\mathbf g}^{*} - i B_{\mathbf g}^{*}$ nella convenzione $f = f_0 + f' - i f''$ di ReciPro, la **differenza di Bijvoet** è

$$\lvert F_{\mathbf g}\rvert^2 - \lvert F_{-\mathbf g}\rvert^2 = -4\,\operatorname{Im}\!\left(A_{\mathbf g}\, B_{\mathbf g}^{*}\right),$$

diversa da zero soltanto quando la parte normale e quella anomala hanno fasi differenti — cioè quando diffusori anomali chimicamente distinti occupano siti non centrosimmetrici. (La differenza si annulla per una struttura centrosimmetrica, per un singolo elemento o per qualsiasi caso in cui ogni atomo porti lo stesso fattore complesso.) È questo che consente di determinare la struttura assoluta (chiralità) di un cristallo non centrosimmetrico, ed è la ragione fisica per cui ReciPro riporta un `F_inv` diverso da zero e valori $\lvert F\rvert$ distinti per le coppie di Friedel non appena si sceglie un'energia dei raggi X vicina a una soglia di assorbimento.

---

## Dal fattore di struttura all'intensità su polveri

Attivando **Powder Diffraction Intensities (Bragg–Brentano)** si converte $\lvert F\rvert^2$ in un'intensità relativa su polveri tenendo conto della geometria di un policristallo orientato casualmente:

$$I_{hkl} \;\propto\; m_{hkl}\, \lvert F_{hkl}\rvert^2\, L p(\theta),$$

- $m_{hkl}$ : **molteplicità** — il numero di piani simmetricamente equivalenti che si sovrappongono allo stesso $2\theta$ (la colonna *Multi.* della tabella).
- $Lp(\theta)$ : il fattore di **Lorentz-polarizzazione** per l'ottica Bragg–Brentano, $Lp = \dfrac{1+\cos^2 2\theta}{\sin^2\theta\,\cos\theta}$, che esalta fortemente i picchi a piccoli angoli.

Poiché in questa modalità i piani equivalenti vengono fusi in un'unica linea, ReciPro forza inoltre l'attivazione di *Hide equivalent planes* e *Hide prohibited planes*.

---

## Vedi anche

- [Fattori di diffusione atomici](scattering-factor.md) — gli $f_j$ che entrano nella somma.
- [Attenuazione & trasporto](attenuation-transport.md) — cosa accade al fascio tra un evento di diffusione e l'altro.
- [3. Interazione del fascio → scheda Reflections](../../3-beam-interaction.md#reflections-tab)
- [Appendice A3. Diffrazione dinamica](../a3-bloch-wave/index.md) — quando $\lvert F\rvert^2$ (cinematica) non è più sufficiente.

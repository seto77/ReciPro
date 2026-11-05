# Interazione del fascio

L'**Interazione del fascio** descrive come il cristallo selezionato interagisce con un fascio incidente di **raggi X, elettroni o neutroni**. Per una radiazione scelta calcola le riflessioni permesse e i loro fattori di struttura, l'attenuazione e il trasporto del fascio attraverso il materiale, i fattori di diffusione atomici di ciascun elemento e (per i raggi X) le righe di fluorescenza caratteristiche. Cambiando il tipo di radiazione in alto si ricalcola tutto, così lo stesso cristallo può essere confrontato tra tecniche di diffrazione e di spettroscopia.

![Interazione del fascio](../assets/cap-it-auto/FormBeamInteraction.png)

Il fascio incidente si seleziona nella banda in cima alla finestra; le quattro schede sottostanti — **Riflessioni**, **Attenuazioni & trasporto**, **Fattori di diffusione** e **Fluorescenza** — mostrano i diversi aspetti dell'interazione. Ciascuna sezione di scheda qui sotto mostra la scheda sotto i fasci **X-ray / Electron / Neutron** (usa le schede in ogni figura); il contenuto cambia notevolmente con il fascio.

!!! tip "Background di fisica dello stato solido (Appendice A2)"
    La diffusione e la fisica dello stato solido alla base di queste quattro schede — fattori di diffusione atomici, fattore di struttura, attenuazione e trasporto del fascio, e fluorescenza — sono spiegate nell'**[Appendice A2. Interazione del fascio (background di fisica dello stato solido)](appendix/a2-beam-interaction/index.md)**.

!!! note "Dati sui raggi X e libreria xraylib inclusa"
    Molte delle grandezze relative ai raggi X (dispersione anomala $f'/f''$, la suddivisione di diffusione $F(q)+S(q)$, la scomposizione foto / Rayleigh / Compton dell'attenuazione massica, i salti dei bordi di assorbimento e le rese di fluorescenza) sono valutate con la libreria inclusa **[xraylib](https://github.com/tschoonj/xraylib)**. Se xraylib non è disponibile, ReciPro ricorre alle sue tabelle interne (attenuazione di sola fotoassorbimento, solo energie delle righe caratteristiche) e le celle interessate mostrano **N/A**. La riga **source** di ogni tabella indica quale set di dati è stato utilizzato.

---

## Scorciatoie da tastiera e mouse

Questa finestra non ha combinazioni di tasti speciali. <kbd>F1</kbd> apre questa pagina del manuale. Nella scheda **Fattori di diffusione** la linea verticale del cursore può essere **trascinata** per leggere il fattore di diffusione di ciascun elemento in quella posizione, e ogni scheda ha un pulsante **Copia** che esporta la sua tabella come testo incollabile in un foglio di calcolo.

→ Vedi **[21. Scorciatoie da tastiera e mouse](21-shortcuts.md)** per tutte le finestre a colpo d'occhio.

---

## Fascio e lunghezza d'onda

La banda superiore è un **Wave Length Control** condiviso con gli altri simulatori.

![Wave Length Control](../assets/cap-it-auto/FormBeamInteraction.waveLengthControl.png)

- **Raggi X / Elettroni / Neutroni** : i fattori di diffusione atomici e la fisica dell'interazione differiscono a seconda del tipo di fascio incidente, quindi vengono commutati qui.
- Per i **Raggi X**, la scelta dell'**Element** (materiale dell'anodo) e della riga caratteristica (Kα, ecc.) imposta automaticamente la lunghezza d'onda di quel raggio X caratteristico.
- **Energia (keV)** e **Lungh. d'onda (Å)** sono collegati; impostando l'uno si aggiorna l'altro, ed entrambi determinano il 2θ usato nella tabella **Riflessioni**.
- **Unit (Å / nm)** cambia l'unità di lunghezza usata per le distanze interplanari e per grandezze simili.
- Cambiando la sorgente si ripristina l'energia usata l'ultima volta con quella sorgente (per i raggi X anche l'elemento e la riga). Una sorgente non ancora usata parte da Cu Kα per i raggi X, 200 kV per gli elettroni e 25,3 meV (neutroni termici). Questi valori non vengono mantenuti dopo la chiusura di ReciPro. <!-- 261106Cl 作者の決定 J18 -->

Il fascio scelto decide anche quali schede e curve sono significative:

| Fascio | Riflessioni | Attenuazioni & trasporto | Fattori di diffusione | Fluorescenza |
|------|------|------|------|------|
| **X-ray** | fattori di struttura incl. dispersione anomala | µ/ρ, µ, trasmissione + bordi di assorbimento (vs energia) | $f(s)$ (Waasmaier–Kirfel o Temari) o $F(q)+S(q)$ | righe caratteristiche + barre EDX |
| **Electron** | fattori di struttura elettronici | σ, MFP, \|dE/ds\|, IMFP, range (vs energia) | Peng / Kirkland / 8-Gaussians / Temari | — (nascosta) |
| **Neutron** | fattori di struttura nucleari | lunghezze di diffusione e sezioni d'urto (nessuna curva in energia) | lunghezze di diffusione (nessuna dipendenza da *s*) | — (nascosta) |

La scheda **Fluorescenza** è solo per i raggi X e scompare per i fasci di elettroni e di neutroni. Per i neutroni i grafici dipendenti dall'energia in **Attenuazioni & trasporto** e **Fattori di diffusione** sono sostituiti da tabelle per elemento, perché la lunghezza di diffusione nucleare non dipende dall'angolo di diffusione né dall'energia.

---

## Scheda Riflessioni {#reflections-tab}

Elenca i piani cristallini permessi (riflessioni) del cristallo e il **fattore di struttura** e l'intensità di diffrazione di ciascuno. Per i raggi X il fattore di struttura include ora i termini di **dispersione anomala** $f'/f''$ all'energia corrente, così `F_inv` (la parte immaginaria) è in genere diversa da zero in prossimità di un bordo di assorbimento. Il layout è lo stesso per ogni fascio; cambiano solo i valori del fattore di struttura e il 2θ di ciascuna riflessione.

=== "X-ray"
    ![Riflessioni — X-ray](../assets/cap-it-auto/FormBeamInteraction-xray-reflections.png)

=== "Electron"
    ![Riflessioni — electron](../assets/cap-it-auto/FormBeamInteraction-electron-reflections.png)

=== "Neutron"
    ![Riflessioni — neutron](../assets/cap-it-auto/FormBeamInteraction-neutron-reflections.png)

**Options**

- **Intensità di diffrazione da polveri (ottica Bragg-Brentano)** : calcola l'intensità relativa come intensità di diffrazione da polveri (Bragg–Brentano), $I \propto m\,\lvert F\rvert^2 / V^2 \times L$, dove $m$ è la molteplicità, $V$ il volume della cella e $L = (1+\cos^2 2\theta)/(\sin 2\theta\,\sin\theta)$ (fattore di Lorentz–polarizzazione) per i raggi X oppure $L = 1/(\sin 2\theta\,\sin\theta)$ (solo fattore di Lorentz, senza termine di polarizzazione) per elettroni e neutroni. Le riflessioni che il fascio non può raggiungere (d < λ/2, 2θ mostrato come ∞) hanno intensità 0. <!-- 261105Cl 監査: J25 届かない反射の BB 強度は 0 --> Quando è disattivata, mostra l'intensità del fattore di struttura $\lvert F\rvert^2$. Attivandola si attivano forzatamente anche *Nascondi piani equivalenti* e *Nascondi piani proibiti*, che diventano non modificabili (in grigio).
- **Nascondi piani equivalenti** (attiva per impostazione predefinita) : raggruppa i piani cristallograficamente equivalenti in un'unica voce. La colonna **Molt.** è mostrata solo mentre questa opzione è attiva.
- **Nascondi piani proibiti** (attiva per impostazione predefinita) : esclude i piani sistematicamente estinti per le regole di estinzione. Quando è disattivata, una colonna **Condizione** mostra la condizione (o le condizioni) di riflessione che proibisce ciascun piano estinto.
- **Soglia distanza interplanare >** (predefinito 1 Å) : esclude le riflessioni con una distanza interplanare minore di questo valore (l'unità di lunghezza segue la selezione di **Unit**). Il valore minimo accettato è (a + b + c)/20 della cella corrente, nella stessa unità. <!-- 261105Cl 監査: J8 numericBoxCutoffD.Minimum を表示単位に合わせた -->

Ogni riga è una riflessione (o un gruppo di piani simmetricamente equivalenti):

| Colonna | Significato |
|------|------|
| **h, k, l** | indici di Miller |
| **Molt.** | molteplicità (numero di piani simmetricamente equivalenti) |
| **d (Å)** | distanza interplanare |
| **q (2π/d)** | modulo del vettore di diffusione |
| **2θ (°)** | angolo di diffrazione per la lunghezza d'onda selezionata |
| **F_real** | parte reale del fattore di struttura |
| **F_inv** | parte immaginaria del fattore di struttura (diversa da zero con dispersione anomala dei raggi X) |
| **\|F\|** | ampiezza del fattore di struttura ($= \sqrt{F_\text{real}^2 + F_\text{inv}^2}$) |
| **F^2** | intensità del fattore di struttura ($\lvert F\rvert^2$) |
| **Rel. Int. (%)** | intensità relativa, con la riflessione più forte posta a 100 |
| **Condizione** | condizione (o condizioni) di estinzione violate dal piano (mostrata solo quando *Nascondi piani proibiti* è disattivata) |
| **i** | l'indice di Miller–Bravais ridondante $i=-(h+k)$ (mostrato solo quando nella finestra principale è attiva **Opzioni ▸ Usa indici di Miller-Bravais (hkil)** e il cristallo usa assi esagonali — esagonale, oppure trigonale non in un setting ad assi romboedrici); **Copia** lo scrive così come è visualizzato <!-- 261105Cl 監査: J9 i 列は表示している値をコピー --> |

**Grafico dei picchi di diffrazione.** Sotto la tabella le stesse riflessioni sono disegnate come pattern a barre, con i picchi più forti etichettati dai loro *hkl*.

- Il selettore dell'asse orizzontale sceglie tra **2θ** (angolo di diffusione in gradi), **d** (distanza tra i piani reticolari) e **Q** ($= 4\pi\sin\theta/\lambda$, il vettore di diffusione / trasferimento di quantità di moto). Le tre opzioni descrivono le stesse riflessioni; cambia solo la scala orizzontale.
- **Log I** commuta l'asse delle intensità tra lineare e logaritmico. Le intensità di diffrazione si estendono su molti ordini di grandezza, quindi una scala logaritmica dilata la parte inferiore per rivelare i picchi deboli che una scala lineare appiattisce contro la linea di base.
- I campi **Range** impostano l'intervallo orizzontale e di intensità rappresentato.

---

## Scheda Attenuazioni & trasporto

Quanto a fondo il fascio penetra nel materiale e come perde energia. Il contenuto dipende dal fascio.

=== "X-ray"
    ![Attenuazioni & trasporto — X-ray](../assets/cap-it-auto/FormBeamInteraction-xray-attenuations.png)

=== "Electron"
    ![Attenuazioni & trasporto — electron](../assets/cap-it-auto/FormBeamInteraction-electron-attenuations.png)

=== "Neutron"
    ![Attenuazioni & trasporto — neutron](../assets/cap-it-auto/FormBeamInteraction-neutron-attenuations.png)

### X-ray

I pulsanti di opzione scelgono il coefficiente rappresentato in funzione dell'energia dei fotoni (calcolato su 1–60 keV con asse dell'energia lineare; l'asse verticale è logaritmico per µ/ρ e µ e lineare per T %; se l'energia corrente è fuori da questo intervallo, il titolo del grafico lo indica, ad es. *(0,5 keV attuali fuori intervallo)*): <!-- 261106Cl 作者の決定 J5 -->

- **µ/ρ** — il coefficiente di attenuazione **massico** (cm²/g): quanto fortemente il materiale rimuove i raggi X per grammo, indipendentemente da quanto è densamente impacchettato (è il valore che si trova nelle tabelle di riferimento). Il grafico mostra il valore **total** insieme alle sue componenti **photo**, **Rayleigh** e **Compton**.
- **µ** — il coefficiente di attenuazione **lineare** $\mu = (\mu/\rho)\cdot\rho$ (cm⁻¹): l'attenuazione per centimetro del materiale reale alla sua densità effettiva. L'intensità trasmessa segue $I = I_0\,e^{-\mu t}$, e $1/\mu$ è la distanza su cui l'intensità scende a circa il 37 % (1/e).
- **T %** — la **trasmissione** $T = e^{-\mu t}$ in percentuale per lo spessore del campione **t** impostato nel campo **Spessore t** (µm, predefinito 100 µm). 100 % = trasparente, 0 % = completamente bloccato; usalo per valutare uno spessore di campione sensato all'energia corrente.

La linea verticale indica l'energia corrente. La tabella scalare a sinistra elenca, all'energia corrente: **µ/ρ (total)**, **µ (linear)**, **Attenuation length** ($1/\mu$), **HVL** (strato di dimezzamento, $\ln 2/\mu$), **Transmission** allo spessore *t*, **µ_en/ρ** (coefficiente di assorbimento di energia massico), i decrementi dell'indice di rifrazione per raggi X **δ** e **β** ($n = 1-\delta+i\beta$), l'angolo **θc (critical)** per la riflessione esterna totale e la **X-ray SLD** reale (densità di lunghezza di diffusione). La tabella inferiore elenca le energie dei **bordi** di assorbimento **K** e **L3** e i loro rapporti di **Salto** per ciascun elemento.

### Electron

Il selettore di grandezza sceglie cosa viene rappresentato in funzione dell'energia del fascio (calcolato su 1–50 keV; la vista iniziale mostra 0–20 keV; un'energia fuori da questo intervallo è segnalata nel titolo del grafico, ad es. *(200 keV attuali fuori intervallo)*): <!-- 261106Cl 作者の決定 J5 -->

- **Tutti (normalizzati)** — sovrappone le tre curve sottostanti, ciascuna riscalata al proprio massimo così che le forme possano essere confrontate in un unico grafico (leggi i valori assoluti dalla tabella).
- **σ elastica (nm²)** — sezione d'urto di diffusione elastica: quanto è probabile che un singolo atomo devii l'elettrone.
- **MFP elastico (nm)** — libero cammino medio: la distanza media percorsa dall'elettrone tra eventi di diffusione elastica.
- **|dE/ds| (keV/nm)** — modulo del potere frenante: l'energia che l'elettrone perde per nanometro di percorso.
- **IMFP (nm)** — libero cammino medio anelastico: la distanza media tra le collisioni con perdita di energia.
- **Percorso CSDA (µm)** — la lunghezza totale del percorso che l'elettrone compie prima di fermarsi.

La tabella scalare elenca la **wavelength** dell'elettrone, **σ elastic**, **Elastic MFP**, **|dE/ds|**, **IMFP**, la **Plasma E** e l'energia media di eccitazione **J**, due **range** dell'elettrone (la stima di penetrazione di Kanaya–Okayama e la lunghezza di percorso integrata CSDA) e il valore medio di **Z, A**. La tabella per elemento fornisce per ciascun elemento la frazione atomica e la sezione d'urto elastica σ. Le sezioni d'urto elastiche usano i dati **NIST Mott** (50 eV–36 keV) e ricorrono a **screened Rutherford** sopra i 36 keV.

### Neutron

L'interazione neutronica è determinata dalle sezioni d'urto nucleari piuttosto che da una curva dipendente dall'energia, quindi questa scheda mostra solo tabelle. La tabella scalare elenca la lunghezza di diffusione coerente media **b̄**, la **Coherent SLD**, le sezioni d'urto medie coerente / incoerente / di assorbimento / totale (**σ̄_coh**, **σ̄_incoh**, **σ̄_abs**, **σ̄_total**), la sezione d'urto totale macroscopica **Σ_total** e la corrispondente **attenuation length**. La sezione d'urto di assorbimento è valutata con la legge 1/v alla lunghezza d'onda corrente; i nuclidi per cui ciò non è valido (Cd, Sm, Eu, Gd, assorbitori risonanti) sono contrassegnati. La tabella per elemento elenca **b_coh**, **σ_coh** e la frazione atomica.

---

## Scheda Fattori di diffusione {#scattering-factors-tab}

Il fattore di diffusione atomico di ciascun elemento costituente, rappresentato in funzione di $s = \sin\theta/\lambda$ (Å⁻¹). Ogni elemento è disegnato con il proprio colore, e la **linea verticale del cursore** può essere trascinata per leggere il fattore di diffusione di ogni elemento in quella posizione nella tabella a sinistra.

=== "X-ray"
    ![Fattori di diffusione — X-ray](../assets/cap-it-auto/FormBeamInteraction-xray-scattering.png)

=== "Electron"
    ![Fattori di diffusione — electron](../assets/cap-it-auto/FormBeamInteraction-electron-scattering.png)

=== "Neutron"
    ![Fattori di diffusione — neutron](../assets/cap-it-auto/FormBeamInteraction-neutron-scattering.png)

- **X-ray** offre due modalità di **Modello**: **f(s)** rappresenta il fattore di diffusione atomico convenzionale per raggi X (in unità elettroniche); **F(q)+S(q)** rappresenta il fattore di forma **coerente** di Rayleigh $F(q)$ insieme alla funzione di diffusione **incoerente** di Compton $S(q)$ (da xraylib). La tabella elenca inoltre i termini di dispersione anomala **f'(E)** e **f''(E)** all'energia corrente e una colonna **modello** che indica da quale sorgente proviene la colonna f(s). Quando è selezionato **f(s)**, una seconda riga di pulsanti di opzione sceglie **da dove proviene la curva**: **Waasmaier–Kirfel** (l'adattamento predefinito, che copre anche gli ioni su Z = 1–98) oppure **Temari**. È disattivata mentre è mostrato **F(q)+S(q)**, perché quelle due curve provengono da xraylib e non ammettono scelta della sorgente.
- **Electron** offre quattro sorgenti per il fattore di diffusione elettronico: **Peng**, **Kirkland**, **8-Gaussians** e **Temari**. La tabella mostra $f_e(s)$ (nm) e quale **modello** lo ha prodotto. **Kirkland** e **8-Gaussians** non hanno parametri per gli ioni, quindi per gli ioni le loro curve sono omesse e le celle della tabella restano vuote (**Peng** fornisce valori per gli ioni). <!-- 261106Cl 作者の決定 J12 -->
- Le lunghezze di diffusione **Neutron** non dipendono da $s$, quindi non viene disegnata alcuna curva; la tabella elenca per ciascun elemento la lunghezza di diffusione coerente **b_coh** e le sue sezioni d'urto coerente / incoerente.
- **Temari** non è un adattamento ma una tabella calcolata (Temari dataset-factors v2.0.0, CC-BY-4.0, da un calcolo di campo autoconsistente di Dirac completo). Copre **solo atomi neutri Z = 1–86**: ioni e Z = 87–98 non hanno voce, quindi le loro curve sono omesse e le celle restano vuote invece di essere riempite con valori di atomo neutro.
- **Spostamento (B)** moltiplica ciascun fattore per lo smorzamento termico $e^{-B s^2}$ usando il parametro di spostamento isotropo di ciascun atomo.

---

## Scheda Fluorescenza {#fluorescence-tab}

Per un fascio di raggi X, l'emissione di **fluorescenza** caratteristica del campione. (Questa scheda è nascosta per i fasci di elettroni e di neutroni.)

![Fluorescenza (X-ray)](../assets/cap-it-auto/FormBeamInteraction-xray-fluorescence.png)

Il grafico **Righe di emissione EDX (elenco di riferimento; eccitazione non considerata)** disegna le righe caratteristiche (Kα1, Kα2, Kβ1, Lα1, Lα2, Lβ1) di ogni elemento come barre alle loro energie fotoniche, con l'altezza proporzionale a frazione atomica × tasso radiativo × resa di fluorescenza (un'anteprima qualitativa in stile EDX e un elenco di riferimento: la sezione d'urto di eccitazione e l'efficienza del rivelatore non sono modellate, quindi la «riga più intensa» non è necessariamente la più intensa in uno spettro misurato). <!-- 261106Cl 作者の決定 J6 --> La tabella inferiore elenca, per riga, l'elemento, il nome della riga, l'energia **E keV**, l'intensità relativa **Rel.I** (la quota di questa riga tra le transizioni radiative che riempiono una lacuna nel suo guscio, %) e la resa di fluorescenza **ω**. La tabella scalare riporta la resa di guscio K **ω_K** di ciascun elemento, la **riga più intensa (eccit. ignorata)** nello spettro e la **fonte** dei valori: *xraylib (E, I, ω)* quando xraylib è disponibile (energie delle righe, intensità relative e rese provengono tutte da xraylib), altrimenti *internal (E only)*. <!-- 261105Cl 監査: FormBeamInteraction.cs (Rel.I = LineRadRate × 100、出典の行, Controls c6d62ed) -->

---

## Copia negli appunti

Ogni scheda ha un pulsante **Copia** che copia la sua tabella negli appunti come testo che può essere incollato in un foglio di calcolo come Excel.

---

## Vedi anche

- [Database dei cristalli](1-crystal-database.md) — definizione del cristallo di cui si calcola l'interazione.
- [Simulatore di diffrazione](7-diffraction-simulator/index.md) — simulazione di pattern di diffrazione usando i fattori di struttura.
- [Appendice A2. Interazione del fascio (background di fisica dello stato solido)](appendix/a2-beam-interaction/index.md) — la diffusione e la fisica dello stato solido alla base di ogni scheda.

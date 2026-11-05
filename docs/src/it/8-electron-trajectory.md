# Traiettorie elettroniche

Il **Simulatore di traiettorie (metodo Monte Carlo)** calcola le traiettorie degli elettroni all'interno di un campione con il **metodo Monte-Carlo**: gli elettroni incidenti subiscono diffusione elastica e anelastica, e le distribuzioni risultanti degli elettroni retrodiffusi (BSE) — direzione, energia all'uscita, profondità di penetrazione ed estensione laterale — vengono accumulate.
La [12. Simulazione EBSD](12-ebsd-simulation.md) utilizza lo stesso motore Monte-Carlo per la propria ponderazione angolare/energetica/in profondità, ma lo esegue internamente con impostazioni proprie; i risultati di questa finestra non le vengono trasmessi.

![Electron Trajectory](../assets/cap-it-auto/FormTrajectory.png)

La finestra è divisa in tre colonne: la **vista 3-D delle traiettorie** a sinistra, le **Statistiche** e lo stereogramma della **Distribuzione direzionale BSE** al centro, e tre **istogrammi** a destra. La composizione e la densità del campione sono quelle del cristallo selezionato nella finestra principale; qui si impostano soltanto l'energia del fascio, l'inclinazione del campione e il numero di traiettorie.

---

## Scorciatoie da tastiera e mouse

Le traiettorie sono mostrate in una vista 3-D OpenGL. Essa utilizza la [navigazione della vista](21-shortcuts.md) standard di ReciPro, ma **lo spostamento è disattivato** — utilizzare i pulsanti delle viste predefinite per passare agli orientamenti standard.

| Scorciatoia | Azione |
|----------|--------|
| <kbd>F1</kbd> | Apre questa pagina del manuale online |
| Trascinamento sinistro | Ruota il modello |
| Trascinamento destro su/giù, o rotellina del mouse | Zoom |
| <kbd>CTRL</kbd> + doppio clic destro | Commuta tra proiezione ortografica / prospettica |

→ Vedere **[21. Scorciatoie da tastiera e mouse](21-shortcuts.md)** per una panoramica di tutte le finestre.

---

## Condizioni di calcolo

I controlli nella parte superiore della finestra impostano l'esecuzione:

- **Simula traiettorie** : avvia la simulazione Monte-Carlo. La barra di stato in basso riporta separatamente il tempo trascorso per il calcolo delle traiettorie, per il disegno dei grafici e per il rendering 3-D.
- **Number of trajectories** (100–1.000.000, predefinito 100.000) : quanti elettroni incidenti seguire. Un numero maggiore di elettroni riduce il rumore statistico di tutte le distribuzioni descritte di seguito, con un tempo di esecuzione che cresce linearmente.
- **Inclinazione campione** (°, da −90 a 0, predefinito 0) : inclinazione della superficie del campione attorno all'asse *X*. Lasciarla a 0 per l'incidenza normale; usare **−70°** per riprodurre la geometria del [simulatore EBSD](12-ebsd-simulation.md), dove la forte inclinazione aumenta la resa di retrodiffusione.
- **Energia** (keV) / **Lungh. d'onda** / **Unit** : la tensione di accelerazione del fascio incidente e la lunghezza d'onda elettronica corretta relativisticamente a essa associata. L'energia imposta l'energia cinetica utilizzata sia dal modello elastico (NIST Mott) sia da quello anelastico (potere frenante / IMFP).

Modificare **Inclinazione campione** o **Energia** dopo un'esecuzione non cambia il risultato mostrato: finché non si preme di nuovo **Simula traiettorie**, ogni ridisegno (vista 3-D, stereogramma, istogrammi) usa l'inclinazione e l'energia con cui le traiettorie sono state calcolate. <!-- 261105Cl 監査: FormTrajectory.cs calcTilt/calcEnergy (9890ced0) -->

Un elettrone viene seguito finché non lascia il campione o finché la sua energia non scende a **2 keV** (soglia fissa). Gli elettroni che escono con più di 2 keV sono contati come elettroni retrodiffusi; η (la loro frazione sul totale degli elettroni incidenti), l'energia media BSE, lo stereogramma e gli istogrammi descritti di seguito sono calcolati da questi elettroni. La sezione d'urto, il libero cammino medio e il potere frenante in **Statistiche** sono valori del modello all'energia del fascio.

I modelli di diffusione non sono selezionabili dall'utente: le sezioni d'urto elastiche provengono dalla tabella NIST Mott inclusa nel programma (con ricorso a Rutherford schermato al di fuori del suo intervallo), e il potere frenante dalla forma di Jablonski modificata (2008). Il modello effettivamente utilizzato è indicato accanto alla sezione d'urto e al potere frenante in **Statistiche**. Vedere [Attenuazione e trasporto](appendix/a2-beam-interaction/attenuation-transport.md) per una descrizione di questi modelli.

### Vista 3-D delle traiettorie

Le traiettorie degli elettroni retrodiffusi sono disegnate in **rosso**, quelle degli elettroni assorbiti nel campione in **blu**; lungo ciascun percorso il colore sfuma verso il bianco/trasparente in 16 passi man mano che l'elettrone perde energia. Il percorso dopo l'uscita (vedi sotto) è disegnato come un breve segmento **arancione** di lunghezza proporzionale all'energia di uscita. I cerchi guida concentrici sono etichettati in nm (o µm), e **+X**, **+Y**, **+Z (=beam)** indicano gli assi.

- **Dall'asse Z (=direzione del fascio)** / **Dall'asse X (asse di rotazione)** / **Normale alla superficie** : allineano la vista alle direzioni standard.
- **Numero di traiettorie da disegnare** (1–100.000, predefinito 500) : quante delle traiettorie calcolate visualizzare (disegnarle tutte e 100.000 sarebbe illeggibile e lento). Vengono prese le prime in ordine di calcolo, saltando gli elettroni assorbiti quando questi non vengono disegnati.
- **Disegna assi** / **Disegna cerchi guida** : le frecce degli assi e la scala delle distanze.
- **Disegna le traiettorie assorbite nel campione** : include gli elettroni che non escono mai.
- **Disegna il percorso dopo l'uscita** : continua a disegnare il percorso di un elettrone retrodiffuso dopo che ha lasciato la superficie.

---

## Statistiche

![Statistiche](../assets/cap-it-auto/FormTrajectory.panel2.groupBoxStatistics.png)

Valori per l'energia del fascio attuale. La sezione d'urto e il potere frenante riportano tra parentesi il nome del modello che li ha prodotti.

- **Sezione d'urto di diffusione (σ_E)** (nm²) — sezione d'urto elastica totale per atomo.
- **Libero cammino medio elastico (λ)** (nm) — distanza media tra eventi di diffusione elastica.
- **Potere frenante (dE/ds)** (eV/nm, negativo) — energia persa per unità di lunghezza di percorso.
- **Coefficiente di elettroni retrodiffusi, η** (%) — la frazione di elettroni incidenti che escono di nuovo attraverso la superficie di ingresso. È la grandezza su cui si basa il contrasto delle immagini BSE.
- **Energia media BSE** (keV) — energia media degli elettroni retrodiffusi nel momento in cui escono. Viene mostrato «-» quando nessun elettrone è retrodiffuso (gli istogrammi sono allora vuoti). <!-- 261105Cl 監査: FormTrajectory.cs DrawStatistics (count == 0) -->

---

## Distribuzione direzionale BSE

![Distribuzione direzionale BSE](../assets/cap-it-auto/FormTrajectory.panel2.groupBoxDirectionDistribution.png)

Distribuzione angolare degli elettroni retrodiffusi, tracciata su uno stereogramma il cui centro corrisponde alla direzione della normale alla superficie.

- **Frequenza** / **Energia media** / **Deviazione standard dell'energia** : la grandezza rappresentata dal colore — quanti elettroni escono in ciascuna direzione, la loro energia media o la dispersione di tale energia.
- **Disegna assi** : sovrappone le direzioni +X / ±Y / ±Z.
- **Min** / **Max**, **Risoluz.**, **Colore** : i limiti della scala dei colori, l'ampiezza angolare delle classi dell'istogramma e la mappa dei colori.

---

## Istogrammi

![Istogrammi](../assets/cap-it-auto/FormTrajectory.flowLayoutPanelProfiles.png)

Tre distribuzioni degli elettroni retrodiffusi. Ogni classe contiene la frazione di tutti gli elettroni retrodiffusi che vi ricade (la somma dei valori delle classi è 1; non sono divisi per l'ampiezza della classe).

### Distribuzione energetica BSE all'uscita

Istogramma dell'**energia che gli elettroni retrodiffusi possiedono ancora quando lasciano il campione** (keV) — non della loro perdita di energia. Il simulatore EBSD pondera l'integrazione in energia del master pattern con una distribuzione di questo tipo, ricavata dalla propria simulazione Monte-Carlo.

### Massima distanza BSE parallela alla superficie

Istogramma della distanza percorsa **lateralmente** (parallelamente alla superficie, nm) da ciascun elettrone retrodiffuso prima di uscire. Corrisponde all'estensione laterale del volume di interazione, e quindi al limite intrinseco di risoluzione spaziale di una misura BSE o EBSD.

### Massima profondità di penetrazione BSE

Istogramma della massima profondità **perpendicolare alla superficie** (nm) raggiunta da ciascun elettrone retrodiffuso prima di uscire.

---

## Vedere anche

- [Simulazione EBSD](12-ebsd-simulation.md)
- [Calcolo EBSD](appendix/a3-bloch-wave/ebsd.md)
- [Attenuazione e trasporto](appendix/a2-beam-interaction/attenuation-transport.md) — le sezioni d'urto elastiche, il potere frenante e i percorsi utilizzati qui.
- [Diffrazione dinamica (onda di Bloch)](appendix/a3-bloch-wave/index.md)
- [Simulatore HRTEM/STEM](9-hrtem-stem-simulator/index.md)
- [Simulatore di diffrazione](7-diffraction-simulator/index.md)

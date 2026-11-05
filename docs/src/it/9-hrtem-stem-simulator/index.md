---
title: HRTEM / STEM Simulator
---

# Simulatore HRTEM/STEM

Il **Simulatore HRTEM/STEM** simula immagini di frange reticolari TEM (HRTEM), immagini STEM e potenziali cristallini proiettati per il cristallo e l'orientazione selezionati. Fare clic su **Simula** per avviare il calcolo.

![Simulatore HRTEM/STEM](../../assets/cap-it-auto/FormImageSimulator.png)

La finestra è divisa in due metà. Il **lato sinistro** mostra il risultato della simulazione e ne controlla l'aspetto (riquadri immagine, luminosità, colori, barra di scala e così via); il **lato destro** contiene le condizioni di calcolo (**Proprietà ottiche** e **Impostazioni di simulazione**).

---

## Questa pagina e le pagine delle modalità

- **Questa pagina (panoramica)**: le operazioni comuni a tutte le modalità, insieme ai **controlli di visualizzazione e regolazione del risultato sul lato sinistro**.
- **Pagine delle modalità**: tutte le impostazioni che compaiono sul **lato destro** per quella modalità, descritte in modo che ciascuna pagina sia autosufficiente (alcune impostazioni compaiono quindi in più di una pagina).

| Modalità | Contenuto | Pagina |
|------|----------|------|
| **HRTEM** | Immagini TEM ad alta risoluzione con frange reticolari | [Simulazione HRTEM](1-hrtem-simulation.md) |
| **STEM** | Immagini di microscopia elettronica a trasmissione a scansione (BF / ABF / LAADF / HAADF) | [Simulazione STEM](2-stem-simulation.md) |
| **Potential** | Potenziale cristallino proiettato ($U_g$ / $U'_g$) | [Simulazione del potenziale](3-potential-simulation.md) |

---

## Scorciatoie da tastiera e mouse

I risultati vengono mostrati come uno o più riquadri immagine. Utilizzano la [navigazione standard della vista immagine](../21-shortcuts.md) di ReciPro, e tutti i riquadri si spostano e si ingrandiscono insieme.

| Scorciatoia | Azione |
|----------|--------|
| <kbd>F1</kbd> | Apre questa pagina del manuale online |
| <kbd>CTRL</kbd>+<kbd>C</kbd> (griglia immagini attiva) | Copia l'immagine o le immagini negli appunti come metafile |
| Trascinamento sinistro / centrale | Sposta l'immagine (tutti i riquadri si muovono insieme) |
| Rotellina del mouse su / giù | Zoom avanti (×2) / indietro (×0.5) in corrispondenza del cursore |
| Trascinamento di un rettangolo con il tasto destro | Zoom avanti nella regione selezionata |
| Clic destro / doppio clic destro | Zoom indietro (×0.5) |
| <kbd>CTRL</kbd> + trascinamento di un rettangolo con il tasto destro | Seleziona un'area rettangolare |
| Doppio clic sinistro su un riquadro | Ingrandisce quel riquadro / ripristina la griglia (layout a più riquadri) |
| Movimento del mouse (senza tasto) | Legge la posizione (pm) e il valore del pixel in corrispondenza del cursore |

→ Vedere **[21. Scorciatoie da tastiera e mouse](../21-shortcuts.md)** per una panoramica di ogni finestra.

---

## Percorsi rapidi per obiettivo

| Obiettivo | Punto di partenza | Riferimento |
|------|------------|-----------|
| Calcolare una singola immagine HRTEM | Impostare **Modalità immagine** su **HRTEM**, quindi impostare la tensione di accelerazione e il defocus in **Condizioni TEM** | [Simulazione HRTEM](1-hrtem-simulation.md), [Formazione dell'immagine HRTEM](../appendix/a3-bloch-wave/hrtem.md) |
| Calcolare un'immagine STEM | Impostare **Modalità immagine** su **STEM**, quindi impostare l'angolo di convergenza e il rivelatore in **Opzioni STEM** | [Simulazione STEM](2-stem-simulation.md), [Calcolo STEM](../appendix/a3-bloch-wave/stem.md) |
| Visualizzare il potenziale proiettato | Impostare **Modalità immagine** su **Potential** | [Simulazione del potenziale](3-potential-simulation.md) |
| Generare una serie di spessore / defocus | In HRTEM, configurare **Modalità singola/seriale** e le condizioni dell'immagine | [Simulazione HRTEM](1-hrtem-simulation.md) |
| Usare HAADF-STEM con TDS | Impostare fattori di temperatura atomici diversi da zero e portare il rivelatore STEM su LAADF / HAADF | [Calcolo STEM](../appendix/a3-bloch-wave/stem.md) |

---

## Flusso di lavoro di base

1. Selezionare il cristallo e l'orientazione nella finestra principale, quindi aprire questa finestra.
2. Scegliere HRTEM, STEM o Potential in **Modalità immagine**.
3. Impostare tensione di accelerazione, defocus, aberrazioni, apertura, angolo di convergenza STEM e così via in **Proprietà ottiche** (vedere le pagine delle modalità).
4. Impostare spessore, dimensione dell'immagine, risoluzione, numero di onde di Bloch, modello di coerenza parziale e così via in **Impostazioni di simulazione** (vedere le pagine delle modalità).
5. Fare clic su **Simula**, quindi regolare l'aspetto con **Regolazione**, **Normalizz.** e **Visualizzazione** a sinistra secondo necessità.

---

## Selezione della modalità immagine

![Modalità immagine](../../assets/cap-it-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxImageMode.png){ align=left }

**Modalità immagine**, in alto a destra, seleziona il tipo di calcolo. I pannelli di destra (**Proprietà ottiche** e **Impostazioni di simulazione**) cambiano in base alla modalità scelta.<div style="clear: both;"></div>

- **HRTEM** — immagini TEM ad alta risoluzione con frange reticolari → [Simulazione HRTEM](1-hrtem-simulation.md)
- **STEM** — immagini di microscopia elettronica a trasmissione a scansione → [Simulazione STEM](2-stem-simulation.md)
- **Potential** — potenziale cristallino proiettato → [Simulazione del potenziale](3-potential-simulation.md)

---

## Area immagine (lato sinistro)

La metà sinistra della finestra mostra l'immagine simulata. La barra di stato nella parte superiore riporta la posizione del cursore (**X:**, **Y:**) e il **Valore:** dell'immagine (intensità) sotto il cursore, accanto a una scala di intensità **Basso → Alto** che riflette la mappa di colori e l'intervallo di luminosità correnti.

Quando vengono prodotte più immagini (un'immagine seriale, oppure modulo/fase di un potenziale) esse sono affiancate in una griglia, e tutti i riquadri si ingrandiscono e si spostano insieme.

---

## Visualizzazione e regolazione dei risultati (pannello sinistro) {#display-settings}

Il pannello in basso a sinistra regola l'aspetto del risultato — luminosità, colori, normalizzazione e sovrapposizioni. Queste impostazioni valgono per tutte le modalità e hanno effetto senza ricalcolare.

### Regolazione

![Regolazione](../../assets/cap-it-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxAdjust.png)

- **Min** / **Max** : estremo inferiore (nero) e superiore (bianco) dell'intervallo di intensità visualizzato. Usare i cursori per regolare il contrasto.
- **Colore** : scala di colori dell'immagine — **Gray scale** oppure **Cold-Warm** (dal blu al rosso).
- **Sfocatura gaussiana (FWHM)** : se spuntata, applica una sfocatura gaussiana con la larghezza a metà altezza (pm) indicata a destra, approssimando una risoluzione finita (funzione di allargamento del punto).

### Normalizzazione

![Normalizzazione](../../assets/cap-it-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxNormalization.png)

- **Per immagine** : se spuntata, normalizza ciascuna immagine separatamente (se non spuntata, l'intera serie condivide una scala comune).
- **Min** / **Max** : fissa l'estremo inferiore / superiore della normalizzazione al valore indicato a destra, invece del minimo / massimo dell'immagine.
- **EDX comune** : visibile solo mentre è visualizzata una [mappa EDX](2-stem-simulation.md#stem-edx) e il calcolo ha prodotto più di un canale; porta tutti i canali della stessa grandezza su un unico intervallo di visualizzazione. Le mappe EDX non vengono mai riscalate da **Min** / **Max**: il loro estremo inferiore è fissato a 0 e solo quello superiore segue **Per immagine** / **EDX comune**.
- Questo gruppo è nascosto in modalità Potential.

### Immagine STEM

![Immagine STEM](../../assets/cap-it-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

Visibile solo in modalità STEM. Seleziona quale componente di diffusione dell'immagine STEM calcolata viene visualizzata (**Elastico**, **TDS** oppure **Elastico & TDS**). Se nello stesso calcolo sono state calcolate anche le [mappe STEM-EDX](2-stem-simulation.md#stem-edx), è disponibile anche una quarta scelta, **EDX**. Essendo specifica dello STEM, è descritta anche nella pagina [Simulazione STEM](2-stem-simulation.md).

### Visualizzazione

![Visualizzazione](../../assets/cap-it-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxDisplay.png)

Imposta gli elementi sovrapposti all'immagine.

- **Cella** : sovrappone il contorno della cella elementare proiettata, così da poter mettere in relazione il contrasto dell'immagine con il reticolo cristallino. Gli spigoli paralleli ad **a**, **b** e **c** sono disegnati in rosso, verde e blu, centrati sul centro dell'immagine.
- **Testo** : sovrappone l'etichetta di ciascun riquadro — lo spessore `t=` e il defocus `f=` (nm) in HRTEM / STEM, oppure il nome del riquadro (per esempio *Ug magnitude*) in modalità Potential. Si possono specificare **Dim.** (dimensione del carattere) e **Colore**.
- **Scala** : sovrappone una barra di scala. Si possono specificare **Lungh.** (nm) e **Colore**.

---

## Esecuzione della simulazione

![Azioni di simulazione](../../assets/cap-it-auto/FormImageSimulator.splitContainer1.panelSimulationActions.png)

- **Simula** : esegue il calcolo con il cristallo, le condizioni del microscopio, lo spessore, il defocus e le impostazioni di visualizzazione correnti.
- **Arresta** : interrompe il calcolo in corso (visibile solo durante il calcolo).
- **Tempo reale** : se spuntata, ricalcola immediatamente quando il cristallo viene ruotato (nascosta in modalità STEM).
- **Preimpostazioni** : mostra/nasconde la finestra delle preimpostazioni, che memorizza e richiama le condizioni di imaging TEM (vedere [Preset list](#preset-list) più sotto).

### Preset list {#preset-list}

<!-- screenshot: Preset list window (FormPresets) with a few entries -->

La finestra **Preset list** conserva istantanee con nome delle impostazioni del simulatore. Un'istantanea memorizza la modalità immagine, la tensione di accelerazione, Cs, Cc, ΔV, il defocus, β, l'apertura obiettivo (dimensione e scostamento), il modello di coerenza parziale HRTEM, lo spessore e gli elenchi seriali di spessore / defocus, la dimensione e la risoluzione dell'immagine, il numero di onde di Bloch e le impostazioni STEM (angolo di convergenza, angoli del rivelatore, dimensione della sorgente, spessore della fetta, risoluzione angolare, **Calcola le mappe EDX** e **Serie di righe (Kα, Lα…)**). L'elenco viene salvato con le impostazioni di ReciPro e si conserva dopo un riavvio.

- Un clic su una voce la applica immediatamente.
- **OK** chiude la finestra mantenendo le impostazioni correnti; **Annulla** (o <kbd>Esc</kbd>) la chiude e ripristina le impostazioni in vigore al momento dell'apertura della finestra.
- **Gestisci l'elenco dei preset** : se spuntata, un clic su una voce si limita a selezionarla (senza applicarla) e compaiono la casella **Nome** e i pulsanti **Add** / **Sostituisci** / **Rinomina** / **Elimina**. **Add** aggiunge le impostazioni correnti con il nome indicato, **Sostituisci** sovrascrive la voce selezionata con le impostazioni correnti, **Rinomina** ne cambia solo il nome.

Le preimpostazioni dei microscopi (ARM300F, 2100F, …) non sono in questo elenco; si trovano nel menu contestuale di **Condizioni TEM** (vedere [Simulazione HRTEM](1-hrtem-simulation.md#tem-conditions)).

---

## Menu File

![Menu File](../../assets/cap-it-auto/FormImageSimulator.menuStrip1.fileToolStripMenuItem.png)

- **Salva immagine** : salva **come immagine (formato PNG)**, **come immagine (formato TIFF)** oppure **come metafile (EMF)**. PNG ed EMF salvano ciò che è visualizzato (intervallo di visualizzazione e scala di colori); **Salva singolarmente in modalità immagine seriale** (attiva per impostazione predefinita) scrive ciascun riquadro in un file separato invece di un'unica immagine affiancata. TIFF scrive sempre un file in virgola mobile per riquadro, senza sovrapposizioni. Memorizza i valori dei pixel dopo **Normalizz.** (e dopo **Sfocatura gaussiana (FWHM)**, se applicata), non l'intervallo di visualizzazione di **Regolazione**; con le impostazioni predefinite (**Min** 0, **Max** 1, **Per immagine**) ogni immagine è scalata a 0–1, quindi togliere la spunta a **Min** / **Max** per esportare le intensità calcolate. Le mappe EDX vengono sempre scritte senza riscalatura.
- **Copia immagine** : copia negli appunti **come immagine** oppure **come metafile (EMF)**.
- **Sovrastampa simboli** (attiva per impostazione predefinita) : incorpora nell'immagine PNG / EMF salvata o copiata la cella elementare, le etichette e la barra di scala.
- **Carica parametri TEM** / **Salva parametri TEM** : visibili ma disabilitati (non implementati). Usare la [Preset list](#preset-list) per memorizzare e richiamare le condizioni.

## Menu Guida

![Menu Guida](../../assets/cap-it-auto/FormImageSimulator.menuStrip1.helpToolStripMenuItem.png)

- **Concetto base della simulazione HRTEM** : apre la spiegazione della formazione dell'immagine HRTEM ([Appendice A3.2](../appendix/a3-bloch-wave/hrtem.md)).
- **Libreria di calcolo** : seleziona la libreria di calcolo — **Native code** (C++/Eigen, veloce) oppure **Managed code** (.NET). Native è normalmente più veloce. La scelta vale solo per la sintesi dell'immagine HRTEM; lo STEM sceglie automaticamente il proprio risolutore (lo STEM-EDX richiede sempre la libreria nativa), e la scelta viene reimpostata su **Native code** a ogni avvio di ReciPro.

---

## Vedere anche

- [Simulazione HRTEM](1-hrtem-simulation.md)
- [Simulazione STEM](2-stem-simulation.md)
- [Simulazione del potenziale](3-potential-simulation.md)
- [Diffrazione dinamica (onda di Bloch)](../appendix/a3-bloch-wave/index.md)
- [Simulatore di diffrazione](../7-diffraction-simulator/index.md)
- [Traiettorie elettroniche](../8-electron-trajectory.md)

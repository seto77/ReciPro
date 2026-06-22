# Scorciatoie da tastiera e mouse

ReciPro associa molte funzioni a **combinazioni di tasti** e a **pulsanti del mouse combinati con tasti modificatori** — elementi che non sono visibili su un pulsante o in un menu. Questa pagina li raccoglie tutti in un unico posto. Anche la pagina di ciascuna finestra ripete le proprie scorciatoie vicino all'inizio.

<kbd>F1</kbd> funziona in **ogni** finestra e apre la pagina corrispondente di questo manuale online.

---

## Scorciatoie valide per l'intera applicazione

Queste vengono installate dalla [finestra principale](0-main-window.md) ma restano attive mentre le finestre Visualizzatore struttura, Stereogramma, Simulatore di diffrazione, Spot ID e Calcolatrice hanno il fuoco.

| Scorciatoia | Azione |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>D</kbd> | Attiva/disattiva il Simulatore di diffrazione |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>V</kbd> | Attiva/disattiva il Visualizzatore struttura |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>S</kbd> | Attiva/disattiva lo Stereogramma |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>T</kbd> | Attiva/disattiva Spot ID |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd> + tasti freccia | Ruota il cristallo di un passo in quella direzione (tieni premute due frecce per una diagonale) |
| Doppio tocco su <kbd>CTRL</kbd> | Attiva/disattiva la Calcolatrice |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>R</kbd> | Attiva/disattiva il contrassegno *Reserved* del cristallo selezionato |
| <kbd>CTRL</kbd>+<kbd>ALT</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Acquisisci uno screenshot della GUI (strumento per sviluppatori; attiva prima **Capture GUI Components**) |
| Tieni premuto <kbd>CTRL</kbd> all'avvio di ReciPro | Avvia con OpenGL disabilitato (ripristino in caso di problemi grafici) |

---

## Modelli di interazione condivisi

Quasi ogni vista interattiva in ReciPro appartiene a una di tre famiglie. Conoscere la famiglia indica il comportamento di trascinamento/zoom senza dover memorizzare ogni finestra.

### Viste 3-D OpenGL { #3d }

Utilizzate dal [Visualizzatore struttura](5-structure-viewer.md), dalla [Geometria di rotazione](4-rotation-geometry.md), dalla sfera 3-D dello [Stereogramma](6-stereonet.md), dalle [Traiettorie elettroniche](8-electron-trajectory.md) e dalle viste geometria / master-pattern di [EBSD](12-ebsd-simulation.md).

| Azione | Risultato |
|--------|--------|
| Trascinamento sinistro | Ruota — trackball vicino al centro, rotolamento nel piano vicino al bordo |
| Trascinamento destro su/giù, oppure rotellina del mouse | Zoom |
| Trascinamento centrale | Spostamento (solo dove abilitato) |
| <kbd>CTRL</kbd> + Trascinamento destro su/giù | Cambia la distanza della camera (solo in modalità prospettica) |
| <kbd>CTRL</kbd> + Doppio clic destro | Alterna proiezione ortografica / prospettica |

Singole finestre possono disattivare lo spostamento o lo zoom (per esempio, nelle Traiettorie elettroniche e nelle viste 3-D di EBSD lo spostamento è disabilitato).

### Viste a figura di diffrazione { #pattern }

Utilizzate dalla figura del [Simulatore di diffrazione](7-diffraction-simulator/index.md), dalla figura di Kikuchi di [EBSD](12-ebsd-simulation.md) e dallo [Stereogramma](6-stereonet.md) 2-D. La differenza fondamentale rispetto alle viste 3-D: **il trascinamento ruota il cristallo stesso**, non solo la camera, quindi ogni finestra collegata si aggiorna contemporaneamente.

| Azione | Risultato |
|--------|--------|
| Trascinamento sinistro vicino al centro | Inclina il cristallo |
| Trascinamento sinistro nell'area esterna | Fai ruotare il cristallo attorno all'asse di vista/fascio |
| Clic destro | Riduci lo zoom |
| Trascinamento destro di un riquadro | Ingrandisci la regione selezionata |
| Trascinamento centrale | Spostamento |

Su queste viste **non** esiste lo zoom con la rotellina del mouse.

### Viste immagine { #image }

Utilizzate dai pannelli dei risultati di [HRTEM/STEM](9-hrtem-stem-simulator/index.md), dall'immagine di [Spot ID v2](11-spot-id-v2.md) e dal master pattern 2-D di [EBSD](12-ebsd-simulation.md).

| Azione | Risultato |
|--------|--------|
| Trascinamento sinistro / Trascinamento centrale | Spostamento |
| Rotellina del mouse su / giù | Ingrandisci (×2) / Riduci (×0.5) in corrispondenza del cursore |
| Trascinamento destro di un riquadro | Ingrandisci la regione selezionata |
| Clic destro / Doppio clic destro | Riduci lo zoom (×0.5) |

---

## Riferimento per finestra

### 0. Finestra principale
[Apri pagina →](0-main-window.md) · più le scorciatoie valide per l'intera applicazione qui sopra.

| Scorciatoia | Azione |
|----------|--------|
| Trascinamento sinistro del widget di orientamento (in basso a sinistra) | Ruota il cristallo |
| Doppio clic destro sul widget di orientamento | Copia l'immagine del widget negli appunti |
| Clic singolo / doppio clic su un pulsante di funzione | Attiva/disattiva quella finestra / forzala in primo piano |
| Clic destro su un cristallo nell'elenco | Menu contestuale (Rinomina / Duplica / Elimina / Esporta CIF…) |
| Doppio clic sull'etichetta **Current Index** | Mostra / nascondi la casella max-UVW |
| Rilascia un file | Carica un elenco di cristalli (`.xml`, `.cdb2`) o un cristallo (`.cif`, `.amc`) |

### 1. Database dei cristalli
[Apri pagina →](1-crystal-database.md)

| Scorciatoia | Azione |
|----------|--------|
| <kbd>ENTER</kbd> in un campo di ricerca | Esegui la ricerca |
| Clic su una riga di risultato | Carica quel cristallo |
| Clic su un elemento nel popup della tavola periodica | Scorri il suo filtro: ignora → deve includere → deve escludere |

### 2. Informazioni di simmetria · 3. Interazione del fascio
Le Informazioni di simmetria non hanno combinazioni speciali di tasti/mouse. Nell'Interazione del fascio, oltre a <kbd>F1</kbd> e ai pulsanti **Copy**, il cursore verticale sul grafico **Scattering factors** può essere trascinato per leggere il valore di ciascun elemento.
[Simmetria →](2-symmetry-information.md) · [Interazione del fascio →](3-beam-interaction.md)

### 4. Geometria di rotazione
[Apri pagina →](4-rotation-geometry.md) — sei [viste 3-D](#3d) **collegate**; ruotandone una qualsiasi si ruotano tutte e sei contemporaneamente. Nelle piccole viste *Axes* / *Objects* lo zoom e lo spostamento sono disabilitati.

### 5. Visualizzatore struttura
[Apri pagina →](5-structure-viewer.md) — la vista principale è una [vista 3-D](#3d).

| Scorciatoia | Azione |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Copia l'immagine renderizzata negli appunti |
| Doppio clic sinistro su un atomo | Mostra coordinate, distanze dai vicini più prossimi e angoli di legame |
| Trascinamento sinistro del gizmo degli assi cristallografici | Ruota il modello (nessun rotolamento nel piano) |
| Trascinamento sinistro del gizmo della luce | Cambia la direzione dell'illuminazione |

### 6. Stereogramma
[Apri pagina →](6-stereonet.md) — la rete 2-D è una [vista a figura di diffrazione](#pattern); la sfera 3-D opzionale è una [vista 3-D](#3d).

| Scorciatoia | Azione |
|----------|--------|
| Doppio clic sinistro sulla rete | Alterna tra proiezione **Plane** e **Axis** |
| Sposta il mouse sulla rete | Leggi il (hkl)/[uvw] sotto il cursore |

### 7. Simulatore di diffrazione
[Apri pagina →](7-diffraction-simulator/index.md) — la figura è una [vista a figura di diffrazione](#pattern) (nessuno zoom con la rotellina).

| Scorciatoia | Azione |
|----------|--------|
| Doppio clic sinistro su una riflessione | Mostra i dettagli della riflessione (indice, *d*, fattore di struttura, errore di eccitazione) |
| <kbd>CTRL</kbd> + Trascinamento centrale | Sposta il centro del rivelatore (quando l'area del rivelatore è visualizzata) |
| Doppio clic destro sulla barra di stato | Copia un riepilogo testuale delle impostazioni correnti |
| Doppio clic destro su un pulsante di livello attivo (Spots / Kikuchi / Debye / Scale) | Fai lampeggiare quel livello |
| Doppio clic sinistro sullo stereogramma — finestra **TEM holder** | Imposta l'inclinazione del portacampioni su quel punto |
| Tasti freccia — finestra **TEM holder** | Modifica l'inclinazione del portacampioni a passi (seleziona prima **Arrow keys**) |
| Rilascia `.prm` / immagine — **Detector geometry**, oppure `.txt` — **Dynamic compression** | Carica quei dati |

### 8. Traiettorie elettroniche
[Apri pagina →](8-electron-trajectory.md) — una [vista 3-D](#3d) con lo spostamento disabilitato.

### 9. Simulatore HRTEM / STEM
[Apri pagina →](9-hrtem-stem-simulator/index.md) — i pannelli dei risultati sono [viste immagine](#image) e si spostano/zoomano insieme.

| Scorciatoia | Azione |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>C</kbd> (griglia immagini con il fuoco) | Copia l'immagine (o le immagini) negli appunti come metafile |
| <kbd>CTRL</kbd> + Trascinamento destro di un riquadro | Seleziona un'area rettangolare |
| Doppio clic sinistro su un pannello | Ingrandisci quel pannello / ripristina la griglia (layout a più pannelli) |

### 10. Spot ID v1
[Apri pagina →](10-spot-id.md) — l'immagine è solo di riferimento (non interattiva).

| Scorciatoia | Azione |
|----------|--------|
| Doppio clic su una riga nell'elenco dei risultati | Seleziona quel cristallo e ruotalo fino all'asse di zona corrispondente |

### 11. Spot ID v2
[Apri pagina →](11-spot-id-v2.md) — l'immagine è una [vista immagine](#image) con l'editing delle riflessioni sovrapposto.

| Scorciatoia | Azione |
|----------|--------|
| Doppio clic sinistro sull'immagine | Aggiungi una riflessione (con fit del picco) |
| <kbd>CTRL</kbd> + Doppio clic sinistro | Aggiungi una riflessione e contrassegnala come fascio diretto (000) |
| Clic sinistro su una riflessione | Seleziona la riflessione più vicina |
| <kbd>CTRL</kbd> + Clic destro su una riflessione | Elimina la riflessione più vicina |
| <kbd>CTRL</kbd> + tasti freccia | Sposta la riflessione selezionata di un pixel |
| Doppio clic sull'intestazione di riga di una riflessione | Zoom su quella riflessione (×2) |

### 12. Simulazione EBSD
[Apri pagina →](12-ebsd-simulation.md) — la figura di Kikuchi è una [vista a figura di diffrazione](#pattern); le viste 3-D sono [viste 3-D](#3d) (spostamento disattivato); il master pattern 2-D è una [vista immagine](#image).

| Scorciatoia | Azione |
|----------|--------|
| Doppio clic sulla figura di Kikuchi | Seleziona la sotto-cella del rivelatore sotto il cursore e mostra le sue statistiche |

### 20. Macro
[Apri pagina →](20-macro/index.md)

| Scorciatoia | Azione |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>S</kbd> | Salva il testo dell'editor nella voce selezionata dell'elenco macro |
| <kbd>F10</kbd> | Avanza di un passo (durante l'esecuzione passo-passo) |
| Doppio clic su una riga nell'elenco della guida alle funzioni | Inserisci la firma di quella funzione in corrispondenza del cursore |
| Rilascia un file `.mcr` | Caricalo nell'editor |

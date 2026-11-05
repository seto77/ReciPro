# Spot ID v2

**Spot ID v2** rileva, esegue il fitting e indicizza gli spot di diffrazione su un'immagine di diffrazione sperimentale. [Spot ID v1](10-spot-id.md) (il precedente TEM ID) acquisisce invece la geometria degli spot come input numerico.

![Spot ID v2](../assets/cap-it-auto/FormSpotIDV2.png)

---

## Scorciatoie da tastiera e mouse

L'elenco degli spot si costruisce direttamente sull'immagine caricata. Il riquadro dell'immagine usa la [navigazione standard della vista immagine](21-shortcuts.md) di ReciPro per spostare/zoomare; per la modifica degli spot si aggiungono le combinazioni seguenti.

| Scorciatoia | Azione |
|----------|--------|
| <kbd>F1</kbd> | Apre questa pagina del manuale online |
| Doppio clic sinistro sull'immagine | Aggiunge uno spot in quel punto (con fitting del picco) |
| <kbd>CTRL</kbd> + doppio clic sinistro | Aggiunge uno spot e lo contrassegna come fascio diretto (000) |
| Clic sinistro su uno spot | Seleziona lo spot più vicino |
| <kbd>CTRL</kbd> + clic destro su uno spot | Elimina lo spot più vicino |
| <kbd>CTRL</kbd> + tasti freccia | Sposta lo spot selezionato di un pixel |
| Trascinamento sinistro / centrale (area vuota) | Sposta l'immagine |
| Rotellina del mouse | Zoom avanti / indietro alla posizione del cursore |
| Trascinamento destro di un riquadro | Zoom avanti sulla regione selezionata |
| Doppio clic destro | Zoom indietro |
| Doppio clic sull'intestazione di riga di uno spot (tabella) | Zoom su quello spot (×2) |

→ Vedi **[21. Scorciatoie da tastiera e mouse](21-shortcuts.md)** per una panoramica di ogni finestra.

---

## Menu File

Apre un'immagine di diffrazione; **Salva** / **Copia** scrivono l'immagine visualizzata come Metafile (EMF) o come immagine (PNG). Le immagini si possono caricare anche tramite trascinamento (drag and drop). Per un file Gatan DM3/DM4 calibrato in nm⁻¹, la radiazione viene impostata su elettroni e la tensione di accelerazione e la dimensione del pixel (nm⁻¹) vengono lette dal file (in tal caso la lunghezza di camera non è necessaria).

**Salva ▸ Candidate list (CSV)...** esporta i candidati di orientazione trovati dall'indicizzazione e **Copia ▸ Candidate list (TSV)** copia la stessa tabella negli appunti: una riga per candidato × grano, con il rango, l'indice del grano, il nome del cristallo, gli angoli di Eulero ZXZ (°), i nove elementi della matrice di rotazione, il residuo quadratico medio (nm⁻²), il numero di spot assegnati e le assegnazioni spot → (hkl). Il CSV usa sempre il punto come separatore decimale; il TSV si incolla direttamente in un foglio di calcolo.

---

## Ottica

![Ottica](../assets/cap-it-auto/FormSpotIDV2.splitContainer1.panel1.groupBoxOptics.png)

### Sorgente incidente

Selezionare il tipo di radiazione (raggi X / elettroni / neutroni) e impostare l'energia o la lunghezza d'onda.

### Lunghezza camera / Dimensione pixel

La lunghezza di camera (mm) e la dimensione del pixel del rivelatore (mm o nm⁻¹). Quando viene caricato un file Gatan DM calibrato in nm⁻¹, la dimensione del pixel (nm⁻¹) viene letta dall'intestazione del file e la lunghezza di camera non viene usata.

**Mostra anelli di Debye** (disattivato per impostazione predefinita) e **Mostra cerchi guida** (attivato per impostazione predefinita) disegnano sul pattern gli anelli di Debye del cristallo e dei cerchi guida.

---

## Informazioni sullo spot

![Informazioni spot](../assets/cap-it-auto/FormSpotIDV2.splitContainer1.groupBoxSpot.png)

- **Rileva & adatta spot**: Rilevamento automatico degli spot mediante massimi locali e sottrazione del fondo.
- **Numero**: Il numero massimo di spot da rilevare.
- **Primo vicino**: La separazione minima (px) consentita tra gli spot rilevati. I picchi più vicini di questo valore vengono uniti, impedendo la doppia rilevazione dello stesso spot.
- **Raggio adattam.**: Il raggio (px) della regione circolare usata per fittare il picco di ogni spot. I pixel all'interno di questo cerchio vengono fittati con una funzione pseudo-Voigt.
- **Applica a tutti**: Imposta il raggio di fitting di ogni spot al valore corrente di **Raggio adattam.**.
- **Elimina / Cancella tutto**: Rimuove lo spot selezionato oppure tutti gli spot rilevati.
- **Salva** / **Copia**: Salva la tabella degli spot in un file oppure la copia negli appunti.
- **Riadatta tutto**: Esegue di nuovo il fitting delle posizioni e delle intensità di tutti gli spot rilevati.
- **Simboli** / **Numeri**: Mostra simboli di marcatura / numeri sugli spot osservati (entrambi attivi per impostazione predefinita).
- **Adattamento globale**: Esegue un fit globale di tutte le posizioni degli spot contemporaneamente (sperimentale).
- **Donut**: Applica una sottrazione del fondo a forma di ciambella (sperimentale); la casella adiacente imposta la larghezza (px) dell'anello attorno a ciascuno spot la cui media viene sottratta come fondo locale.
- **Dettagli spot**: Se selezionato, apre una finestra che mostra informazioni dettagliate sullo spot attualmente selezionato.
- **Dettagli funzione** (disattivato all'apertura della finestra): Mostra una figura della funzione pseudo-Voigt bidimensionale e dei suoi parametri (x_0, y_0, H1, H2, θ, η, A, B_0, B_x, B_y) usati nella tabella degli spot.

![Details of the spot](../assets/cap-it-auto/FormSpotIDv2Details.png)

---

## Index

![Index](../assets/cap-it-auto/FormSpotIDV2.splitContainer1.groupBoxIndex.png)

- **Identifica spot**: Esegue l'algoritmo di indicizzazione per trovare il cristallo e l'asse di zona che meglio corrispondono.
- **Errore accettabile**: Tolleranza relativa sulla distanza interplanare in % (0,1–10, predefinito 2). La tolleranza sull'angolo tra gli spot ne viene derivata come asin(tolleranza)/2.
- **Ignora riflessioni proibite** (disattivato per impostazione predefinita; solo diffrazione elettronica): Se selezionato, tutte le riflessioni sistematicamente estinte, comprese quelle proibite da assi elicoidali e piani di scorrimento, vengono escluse dalle riflessioni candidate. Se non selezionato, vengono escluse solo le estinzioni dovute alla centratura del reticolo (I, F, A, B, C, R), per cui le riflessioni proibite da assi elicoidali o piani di scorrimento, che possono comparire per diffrazione multipla, restano candidate. Per i raggi X tutte le riflessioni estinte vengono sempre escluse.
- **Grano singolo / Grani multipli**: Cerca un singolo orientamento (monocristallo) oppure più orientamenti (una regione policristallina / multigrano). Per più grani, **N. max. di grani** imposta il limite superiore al numero di grani da cercare.
- **Mostra asse di zona** / **Mostra simbolo** / **Mostra etichetta**: Sovrappone l'asse di zona e i simboli / le etichette degli indici degli spot calcolati.
- **Affina spessore & direzione (solo per SAED)**: Affina lo spessore del campione (nm) e la direzione del fascio del grano selezionato confrontando le intensità osservate degli spot con calcoli dinamici (onde di Bloch). **Onde diffratte** (1–2048, predefinito 400) è il numero di fasci, e **Intervallo angolo di ricerca** (mrad, 1–10, predefinito 2) è il semi-intervallo angolare delle direzioni incidenti esplorate. **Stop** interrompe il calcolo.
- **Results**: Le migliori corrispondenze vengono visualizzate con il nome del cristallo, l'asse di zona [uvw] e gli indici dei singoli spot (hkl).

---

## Vedi anche

- [Spot ID v1](10-spot-id.md)
- [Simulatore di diffrazione](7-diffraction-simulator/index.md)
- [Finestra principale](0-main-window.md)
- [Scorciatoie da tastiera e mouse](21-shortcuts.md)

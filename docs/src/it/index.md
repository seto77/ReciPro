# Manuale di ReciPro

## Breve introduzione
* ReciPro è un software gratuito, distribuito sotto licenza MIT, che fornisce una grande varietà di calcoli cristallografici e di simulazioni di microscopia elettronica.
* Dalla sua pubblicazione su GitHub (marzo 2020) ReciPro è stato scaricato complessivamente più di 27.000 volte ed è utilizzato da numerosi cristallografi e microscopisti elettronici.

## Trova per obiettivo

| Obiettivo | Inizia qui | Passi successivi principali |
|------|------------|-----------------|
| Caricare un cristallo e impostarne l'orientazione | [Finestra principale](0-main-window.md) | [Geometria di rotazione](4-rotation-geometry.md), [Appendice A1. Sistemi di coordinate](appendix/a1-coordinate-system/1-orientation.md) |
| Esaminare una struttura cristallina in 3D | [Visualizzatore struttura](5-structure-viewer.md) | [Informazioni di simmetria](2-symmetry-information.md) |
| Calcolare pattern SAED / XRD / PED / CBED | [Simulatore di diffrazione](7-diffraction-simulator/index.md) | [SAED](7-diffraction-simulator/1-saed-simulation.md), [Diffrazione di raggi X](7-diffraction-simulator/4-x-ray-neutron-diffraction.md), [PED](7-diffraction-simulator/2-ped-simulation.md), [CBED](7-diffraction-simulator/3-cbed-simulation.md) |
| Calcolare immagini HRTEM / STEM | [Simulatore HRTEM/STEM](9-hrtem-stem-simulator/index.md) | [HRTEM](9-hrtem-stem-simulator/1-hrtem-simulation.md), [STEM](9-hrtem-stem-simulator/2-stem-simulation.md) |
| Simulare pattern EBSD | [Simulazione EBSD](12-ebsd-simulation.md) | [Traiettorie elettroniche](8-electron-trajectory.md), [Appendice A3. Calcolo EBSD](appendix/a3-bloch-wave/ebsd.md) |
| Indicizzare spot di diffrazione sperimentali | [Spot ID v1](10-spot-id.md), [Spot ID v2](11-spot-id-v2.md) | [Simulatore di diffrazione](7-diffraction-simulator/index.md) |
| Comprendere le equazioni della diffrazione dinamica | [Appendice A3. Metodo delle onde di Bloch](appendix/a3-bloch-wave/index.md) | [Calcolo dinamico](appendix/a3-bloch-wave/calculation.md), [CBED](appendix/a3-bloch-wave/cbed.md), [STEM](appendix/a3-bloch-wave/stem.md), [EBSD](appendix/a3-bloch-wave/ebsd.md) |

## Funzionalità
* **Full GUI** : Tutte le operazioni vengono eseguite tramite un'interfaccia grafica. La maggior parte dell'I/O dei file supporta il trascinamento (drag and drop).
* **Elenco cristalli** : Gestire più cristalli contemporaneamente; non è necessario aprire una finestra separata per ogni cristallo.
* **Database dei gruppi spaziali** : Database integrato che copre i 230 gruppi spaziali delle International Tables Volume A, oltre a 530 simboli di Hall, con elementi di simmetria, posizioni di Wyckoff e regole di estinzione. Gli elementi di simmetria e le posizioni generali possono essere disegnati come diagrammi schematici nello stile delle *International Tables* Vol. A (vedi [2. Informazioni di simmetria](2-symmetry-information.md)).
* **Informazioni atomiche** : Fattori di diffusione (raggi X, elettroni, neutroni), energie dei raggi X caratteristici, rapporti isotopici, ecc. per gli elementi da H (1) a Cf (98).
* **Rotazione flessibile del cristallo** : Impostare l'orientazione tramite indici di asse di zona/piano cristallino oppure trascinando con il mouse. La notazione di Miller-Bravais (4 indici *hkil*) è supportata per i sistemi trigonale/esagonale. Lo stato di rotazione è sincronizzato fra tutte le finestre di simulazione.
* **Simulazione di diffrazione** : Diffrazione elettronica cinematica e dinamica (metodo delle onde di Bloch), diffrazione di raggi X (incluse le camere a precessione e back-Laue), diffrazione elettronica a precessione (PED) e diffrazione elettronica a fascio convergente (CBED). Una simulazione del portacampioni TEM collega il pattern di diffrazione agli angoli di inclinazione del portacampioni.
* **Simulazione HRTEM / STEM** : Simulazione di immagini TEM ad alta risoluzione con modelli di coerenza parziale; STEM con diffusione termica diffusa.
* **EBSD e traiettorie elettroniche** : Simulazione di pattern EBSD e simulazione Monte Carlo delle traiettorie elettroniche (vedi [8. Traiettorie elettroniche](8-electron-trajectory.md)).
* **Indicizzazione degli spot** : Rilevamento, fitting e indicizzazione automatici degli spot di diffrazione da immagini sperimentali (Spot ID v1/v2).
* **Macro** : Macro in sintassi Python per automatizzare le operazioni (vedi [20. Macro](20-macro/index.md)).
* **Tema chiaro / scuro** : L'interfaccia segue una modalità colore chiara o scura selezionabile.

## Requisiti di sistema
| Voce | Minimo | Consigliato |
|------|---------|-------------|
| OS | Windows con [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows on ARM64 supportato) | Windows 11 |
| GPU | OpenGL 1.3 | GPU esterna con OpenGL 4.3 |
| Memoria | - | 16 GB o più |
| CPU | - | 8+ core (per i calcoli dinamici) |

**Windows on ARM (nativo, sperimentale)** : Un pacchetto portable ARM64 nativo sperimentale (`ReciPro-v.X_arm64.zip`, self-contained — nessuna installazione del .NET Runtime richiesta) è disponibile nella [pagina dei rilasci](https://github.com/seto77/ReciPro/releases/latest). Anche i normali pacchetti x64 funzionano su Windows ARM64 grazie all'emulazione integrata. Per note sulla configurazione e limitazioni vedi [Risoluzione dei problemi](troubleshooting.md#windows-on-arm).

**macOS (non ufficiale)** : ReciPro supporta ufficialmente solo Windows, ma è stato segnalato che il pacchetto ZIP portable **win-x64** funziona su macOS (Apple Silicon) utilizzando il wrapper Wine Sikarugir in combinazione con il driver OpenGL Mesa3D. Una guida alla configurazione pubblicata da un utente è disponibile all'indirizzo <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>. Si noti che questa via non è ufficialmente supportata e alcuni simboli (Å, apici, frecce) potrebbero essere visualizzati in modo errato. Lo ZIP ARM64 **non** funziona su macOS + Wine, e l'attuale via x64 + Rosetta 2 dovrebbe smettere di funzionare a partire da macOS 28 (autunno 2027) — per i dettagli vedi [Risoluzione dei problemi](troubleshooting.md#mac-linux).

## Come usare questo manuale

Questo manuale su GitHub Pages è la fonte di riferimento attuale. Usa la navigazione a sinistra per sfogliare i capitoli, oppure la ricerca nell'intestazione per trovare il nome di una funzione o un'etichetta dell'interfaccia. I vecchi manuali in PDF sono conservati a scopo di archivio.

* **PDF archiviato (inglese):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf>
* **PDF archiviato (giapponese):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf>

## Avvio rapido
1. Scarica e installa da [Releases](https://github.com/seto77/ReciPro/releases/latest).
2. Seleziona un cristallo dall'elenco integrato (~80 cristalli). Puoi anche importare file CIF o usare [CSManager](https://github.com/seto77/CSManager).
3. Richiama le funzioni dal pannello di destra: Visualizzatore struttura, Stereogramma, Simulatore di diffrazione, simulazione HRTEM, ecc.
4. Ruota il cristallo trascinando con il mouse o inserendo gli indici di asse di zona/piano.

## Riferimento
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). <https://doi.org/10.1107/S1600576722000139>

## Licenza
ReciPro è distribuito sotto la [Licenza MIT](https://github.com/seto77/ReciPro/blob/master/LICENSE.md).

# ReciPro

[![Documentation](https://img.shields.io/badge/%F0%9F%93%96_Documentation-blue)](https://seto77.github.io/ReciPro/it/)
[![Latest Release](https://img.shields.io/github/v/release/seto77/ReciPro?logo=github)](https://github.com/seto77/ReciPro/releases/latest)
[![Total downloads](https://img.shields.io/github/downloads/seto77/ReciPro/total?logo=github&label=GitHub%20downloads)](https://github.com/seto77/ReciPro/releases)
[![GitHub Stars](https://img.shields.io/github/stars/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/forks)
[![License: MIT](https://img.shields.io/badge/License-MIT-green)](https://github.com/seto77/ReciPro/blob/master/LICENSE.md)

<!-- 260804Cl: Traduzione di ../../README.md (inglese). Aggiornare questo file quando cambia la versione inglese. -->
[English](../../README.md) | [日本語](README.ja.md) | [Deutsch](README.de.md) | [Français](README.fr.md) | [Español](README.es.md) | **Italiano** | [Русский](README.ru.md) | [简体中文](README.zh-Hans.md) | [繁體中文](README.zh-Hant.md) | [한국어](README.ko.md) | [Português](README.pt.md)

*ReciPro* è un software cristallografico multifunzione, gratuito e open source, dotato di interfaccia grafica. Offre un accesso immediato a funzioni per esplorare banche dati cristallografiche, visualizzare strutture cristalline e impostazioni del goniometro, simulare figure di diffrazione e immagini di microscopia ad alta risoluzione e analizzare dati di diffrazione. Queste funzioni sono collegate tra loro da un'interfaccia intuitiva e i risultati vengono visualizzati in modo sincrono, quasi in tempo reale. *ReciPro* è di aiuto a un'ampia platea di cristallografi (principianti compresi) che si occupano di diffrazione di raggi X, elettroni e neutroni e di TEM.

*ReciPro* è sviluppato con continuità dal 2002 ed è disponibile su GitHub da marzo 2020. È stato scaricato oltre 27.000 volte da GitHub ed è utilizzato da centinaia di utenti in più di una dozzina di laboratori universitari e aziendali.

***[Consulta il manuale per imparare a usarlo!](https://seto77.github.io/ReciPro/it/)***

[Diverse simulazioni eseguite in tempo reale (esempio: MgAl2O4)](https://github.com/user-attachments/assets/6b0234dd-f2d6-49db-b146-bb74cf6021b6)

## Autori

*ReciPro* è sviluppato da [Seto Y.](https://yseto.net/en/home-e) e [Ohtsuka M.](https://researchmap.jp/7000002999?lang=en). Le funzioni e gli algoritmi sono presentati nell'[articolo](https://github.com/seto77/ReciPro/blob/master/docs/ReciProSetoOhtsuka2022.pdf).

## Come citare

Se utilizzi *ReciPro* in lavori accademici, usa il collegamento **Cite this repository** presente nella pagina del repository GitHub. I metadati di citazione sono forniti da `CITATION.cff` e la citazione preferita è il seguente articolo:

  * [Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397-410, doi: 10.1107/S1600576722000139.](https://doi.org/10.1107/S1600576722000139)

Se opportuno, è possibile citare anche il repository del software:

  * Repository: https://github.com/seto77/ReciPro
  * Versioni: https://github.com/seto77/ReciPro/releases/latest

***

## Installazione

* Scarica [*ReciPro-setup.msi*](https://github.com/seto77/ReciPro/releases/latest/download/ReciPro-setup.msi) (collegamento diretto alla versione più recente) ed eseguilo. Lo trovi anche nella [pagina delle versioni](https://github.com/seto77/ReciPro/releases/latest). (Fino alla v.4.939 il programma di installazione si chiamava *ReciProSetup.msi*.)
* *ReciPro* funziona su Windows con ***.Net Desktop Runtime 10.0*** (NON ***.Net Runtime 10.0***), installabile da [qui](https://dotnet.microsoft.com/download/dotnet/10.0).
* Se non puoi eseguire un programma di installazione (ad esempio su PC con permessi limitati), nella pagina delle versioni è disponibile anche un pacchetto **ZIP portatile** (*ReciPro-v.X.XXX.zip*): autosufficiente, senza installazione e senza runtime .NET — basta estrarlo ed eseguirlo.
* *ReciPro* è distribuito con **licenza MIT** (chiunque può usarlo, modificarlo e ridistribuirlo liberamente).
* Per lo stato della firma del codice e la verifica del programma di installazione, vedi la [politica di firma del codice](../../CODE_SIGNING.md).
* Per i componenti e i dati di terze parti inclusi o referenziati, vedi le [note sulle terze parti](../../THIRD-PARTY-NOTICES.md).

### macOS (non ufficiale)

* *ReciPro* supporta ufficialmente solo Windows, ma è stato segnalato il suo funzionamento su macOS (Apple Silicon) combinando il pacchetto **ZIP portatile** con il wrapper Wine **Sikarugir** e il driver OpenGL **Mesa3D**, senza licenza Windows né macchina virtuale.
* Vedi la guida passo passo pubblicata da Ryo Fukushima (JAMSTEC): https://github.com/Ryo-fkushima/ReciPro_macOS_memo
* Questa configurazione non è supportata ufficialmente né completamente verificata. Una limitazione nota è che alcuni simboli (Å, apici, frecce) possono essere visualizzati in modo errato.
* I simboli visualizzati male si possono correggere installando nel prefisso Wine font con ampia copertura di glifi (**DejaVu Sans/Serif** e **Noto Sans CJK JP** per l'interfaccia giapponese): ReciPro rileva l'ambiente Wine e passa automaticamente a tali font. Per i dettagli vedi la [risoluzione dei problemi](https://seto77.github.io/ReciPro/it/troubleshooting/).

### Nota sugli avvisi di sicurezza di Windows

* Scarica *ReciPro* esclusivamente dalla pagina ufficiale GitHub Releases: https://github.com/seto77/ReciPro/releases/latest
* Su alcuni sistemi Windows, Microsoft Defender SmartScreen o Smart App Control possono mostrare un avviso prima dell'esecuzione del programma di installazione. Ciò può accadere con software di ricerca compilato di recente o diffuso in ambiti ristretti, e l'avviso di per sé non significa necessariamente che il programma di installazione sia dannoso.
* Se desideri verificare personalmente il file scaricato, puoi analizzarlo con un servizio multimotore come VirusTotal.

## Politica di firma del codice

[<img src="https://signpath.org/assets/favicon-50x50.png" alt="SignPath" height="20">](https://about.signpath.io/) Firma del codice gratuita su Windows fornita da [SignPath.io](https://about.signpath.io/), certificato della [SignPath Foundation](https://signpath.org/).

Dalla v.4.942 gli artefatti di rilascio (il programma di installazione *ReciPro-setup.msi* e l'eseguibile portatile *ReciPro.exe*) vengono firmati con Windows Authenticode nell'ambito della pipeline di rilascio automatizzata, e ogni richiesta di firma viene esaminata e approvata manualmente dal manutentore prima della pubblicazione. Consulta [CODE_SIGNING.md](../../CODE_SIGNING.md) per la politica completa, compresi l'ambito della firma, la verifica di un programma di installazione e la segnalazione di artefatti sospetti.

## Privacy

*ReciPro* è un'applicazione desktop locale. **Non** raccoglie, memorizza né trasmette dati personali o di utilizzo e non contiene telemetria o analisi. Dopo l'installazione funziona completamente offline.

Le uniche connessioni di rete effettuate da *ReciPro* sono download facoltativi avviati dall'utente e nessuno di essi carica i tuoi dati:

* **Controlla aggiornamenti** (comando di menu): confronta la versione installata con l'ultima release su GitHub e, se lo desideri, scarica il nuovo programma di installazione dalla pagina ufficiale [GitHub Releases](https://github.com/seto77/ReciPro/releases/latest).
* **Banca dati COD** (Crystallography Open Database): scaricata al primo utilizzo (~880 MB) dal mirror GitHub dell'autore e poi usata offline.
* **Libreria Intel MKL** (accelerazione facoltativa): scaricata (~55 MB) da [nuget.org](https://www.nuget.org/) solo se attivi l'opzione *Use MKL*, per velocizzare i calcoli di diffrazione dinamica.

La banca dati AMCSD inclusa e tutte le funzioni principali funzionano interamente offline.

## Manuale
  * Manuale online (inglese / giapponese): https://seto77.github.io/ReciPro/it/
  * Versione giapponese: https://yseto.net/soft/recipro
***

## Funzioni principali

### Banca dati cristallografica

* **AMCSD** (American Mineralogist Crystal Structure Database): oltre 21.000 strutture cristalline integrate e disponibili subito dopo l'installazione.
  * La banca dati è fortemente compressa (~5 MB) ed è inclusa nel file di installazione, quindi è utilizzabile anche in ambienti offline.
  * I cristalli si possono cercare per nome, composizione chimica, parametri reticolari, densità, simmetria ed elementi contenuti.
  * Riferimento: [Downs & Hall-Wallace, 2003, *American Mineralogist* **88**, 247-250](https://www.geo.arizona.edu/xtal/group/pdf/am88_247.pdf)
* **COD** (Crystallography Open Database): sono disponibili anche circa 525.000 strutture cristalline, inclusi cristalli organici.
  * Scaricata automaticamente al primo utilizzo (~880 MB) e successivamente disponibile offline.
  * Riferimenti: [Gražulis et al., 2009, *J. Appl. Cryst.* **42**, 726-729](https://doi.org/10.1107/S0021889809016690); [Gražulis et al., 2012, *Nucleic Acids Res.* **40**, D420-D427](https://doi.org/10.1093/nar/gkr900)
* Importazione ed esportazione di file in formato CIF e AMC.

### Calcoli cristallografici

* Sono supportate 530 notazioni di gruppi spaziali: 230 impostazioni standard ITA + 300 impostazioni di assi non standard.
  * Condizioni generali (regole di estinzione), posizioni di Wyckoff e molteplicità di tutti i gruppi spaziali.
  * Calcolo geometrico della periodicità e/o degli angoli tra piani e/o assi.
  * Generazione delle posizioni atomiche equivalenti.
  * Conversione agevole tra impostazioni di assi non standard (ad es. da *Pbnm* a *Pnma*) e traslazioni dell'origine.

### Proprietà atomiche

* Lunghezza d'onda ed energia dei raggi X caratteristici da <sup>1</sup>H a <sup>98</sup>Cf.
* Fattori di scattering atomico per raggi X, elettroni e neutroni.

### Visualizzatore di strutture

* Visualizzazione 3D delle strutture cristalline basata sull'architettura OpenGL (GLSL).
  * Disegna atomi, legami, poliedri di coordinazione, celle elementari, piani reticolari, superfici di contorno ed etichette di legenda.
  * Anche strutture complesse con decine di migliaia di atomi vengono disegnate fluidamente in tempo reale.
  * I colori e le dimensioni predefiniti degli atomi sono compatibili con VESTA.
  * L'intervallo di disegno può essere specificato in multipli della cella elementare oppure tramite indici di un piano cristallino e distanza dal centro.
  * Abiti cristallini arbitrari possono essere rappresentati colorando le facce di contorno.
  * È possibile visualizzare qualsiasi piano reticolare, il che aiuta i principianti a comprendere il concetto di piano reticolare nei fenomeni di diffrazione.
  * Rotazione, spostamento e zoom si controllano liberamente con il mouse.
  * Facendo clic su un atomo vengono mostrati distanze e angoli di legame con gli atomi vicini.
  * Lo stato di rotazione si riflette immediatamente nelle altre finestre funzionali (proiezione stereografica, simulatore di diffrazione, ecc.).
  * Il codificatore video integrato (Windows Media Foundation) può generare video di animazioni di rotazione (MP4 H.264/H.265) per le presentazioni.

### Proiezione stereografica

* Riporta piani e assi cristallini su una proiezione stereografica.
  * Sono supportate sia la proiezione equiangolare (reticolo di Wulff) sia quella equivalente (reticolo di Schmidt), con paralleli e meridiani.
  * Gli indici possono essere specificati per intervallo numerico o con valori specifici.
  * È possibile visualizzare cerchi massimi specificando gli assi di zona.
  * Gli oggetti disegnati possono essere salvati o copiati in formato vettoriale per poi essere modificati senza perdita di risoluzione.
  * Visualizzazione 3D della geometria della proiezione stereografica a scopo didattico.

### Simulatore di diffrazione

* Simula figure di diffrazione da cristallo singolo per sorgenti di raggi X, elettroni e neutroni.
  * L'energia cinetica del fascio incidente è liberamente configurabile.
  * Sono integrate le energie dei raggi X caratteristici da <sup>1</sup>H a <sup>98</sup>Cf.
  * L'area rappresentata è definita dalla risoluzione dell'immagine (dimensione del pixel) e dalla lunghezza di camera.
  * Sono supportate anche geometrie con rivelatore inclinato.
  * È supportata la sovrapposizione di immagini acquisite sperimentalmente.
  * La rotazione del cristallo (condizione di diffrazione) è controllabile e si sincronizza immediatamente con le altre finestre.

* **Diffrazione policristallina**: simulazione degli anelli di Debye assumendo un campione policristallino.
* **Camera di precessione** (raggi X): simulazione di figure da camera di precessione della zona di Laue di ordine zero.
* **Camera di Laue in retrodiffusione** (raggi X): simulazione di figure di Laue in retrodiffusione.

#### Teoria cinematica della diffrazione
* Disponibile per tutte le sorgenti (raggi X, elettroni, neutroni).
* Le intensità di diffrazione sono stimate dal quadrato del modulo del fattore di struttura cristallina e dall'errore di eccitazione.
* Sono inclusi gli effetti del fattore di Debye-Waller sulle intensità di diffrazione.

#### Teoria dinamica della diffrazione (elettroni)
* Basata sul **metodo delle onde di Bloch** (Bethe, 1928), che consente orientazioni cristalline flessibili senza vincoli su assi di zona a basso indice.
* Sono disponibili due approcci di calcolo:
  * **Metodo degli autovalori di Bethe**: diagonalizzazione matriciale per autovalori/autovettori degli autostati di Bloch. Adatto quando si varia lo spessore del campione.
  * **Metodo della matrice di scattering**: calcolo diretto degli esponenziali di matrice con il metodo scaling and squaring e approssimazione di Padé. Adatto per calcoli rapidi a spessore singolo.
* L'algoritmo più veloce e la libreria matematica migliore (Eigen, Intel MKL o Math.NET) vengono selezionati automaticamente.
* Il potenziale di assorbimento dovuto allo scattering diffuso termico (TDS) è calcolato analiticamente per garantire prestazioni elevate.

* **SAED** (diffrazione elettronica ad area selezionata): simulazione della diffrazione elettronica a fascio parallelo con effetti di scattering dinamico.
* **PED** (diffrazione elettronica in precessione): simula figure PED specificando l'angolo di precessione e la risoluzione angolare azimutale. Utile per l'analisi strutturale e per ottimizzare condizioni PED quasi cinematiche.
* **CBED** (diffrazione elettronica a fascio convergente): simula figure CBED con semiangolo di convergenza e numero di suddivisioni definiti dall'utente. È supportata la simulazione al variare dello spessore per determinare lo spessore del campione.
  * Figure CBED mediate sulla posizione (PACBED).
  * Simulazione CBED a grande angolo (LA-CBED).

### Simulatore HRTEM

* Simulazione di immagini di microscopia elettronica a trasmissione ad alta risoluzione nello stesso quadro teorico delle onde di Bloch.
* I parametri ottici (tensione di accelerazione, coefficiente di aberrazione sferica, defocus, spessore del campione, ecc.) si impostano dall'interfaccia grafica.
* Sono integrati preset di parametri ottici TEM tipici, richiamabili con il tasto destro del mouse.
* Due modelli di formazione dell'immagine per la coerenza parziale:
  * **Teoria lineare del trasferimento di contrasto**: costo computazionale ridotto; adatta a campioni sottili che soddisfano l'approssimazione di oggetto di fase debole.
  * **Teoria non lineare del trasferimento di contrasto (modello TCC)**: basata sul coefficiente incrociato di trasmissione del primo ordine (Ishizuka, 1980); affidabile anche per campioni più spessi e materiali a numero atomico elevato.
* È possibile tracciare la funzione di trasferimento di contrasto con le funzioni di inviluppo.
* Le serie di immagini spessore-defocus possono essere calcolate simultaneamente.
* In condizioni standard il calcolo si completa tipicamente in meno di un secondo.

### Simulatore STEM

* Simulazione di immagini di microscopia elettronica a trasmissione a scansione.
  * Modalità di imaging in campo chiaro (BF), campo scuro anulare (ADF) e campo scuro anulare ad alto angolo (HAADF).
  * Il fascio convergente è trattato come sovrapposizione di molte onde piane con calcolo accurato delle sovrapposizioni.
  * Gli elettroni diffusi anelasticamente sono calcolati con il modello del potenziale assorbente.
  * È possibile generare serie di immagini spessore-defocus.

### Spot ID

* Indicizzazione semiautomatica degli spot di diffrazione per figure SAED sperimentali.
* **Spot ID v1**: cerca gli assi di zona a partire dalla configurazione geometrica (distanze e angoli) degli spot di diffrazione. Supporta l'analisi simultanea di 2-3 immagini.
* **Spot ID v2**: importa direttamente le immagini delle figure SAED.
  * Supporta i formati di immagine più diffusi: TIFF (.tif), Digital Micrograph 3/4 (.dm3, .dm4) e altri.
  * Rilevamento e fitting automatici degli spot di diffrazione con funzioni pseudo-Voigt 2D.
  * Ricerca esaustiva delle orientazioni cristalline compatibili con la disposizione dei vettori del reticolo reciproco.
  * Determinazione accurata anche di assi di zona di ordine elevato.

### Geometria di rotazione (goniometro)

* Collega gli angoli di Eulero di ReciPro al goniometro in laboratorio.
* Indica come ruotare il goniometro per ottenere l'orientazione cristallina desiderata (ad es. un asse di zona a basso indice).
* Supporta definizioni arbitrarie del goniometro.

### Macro

* Script macro con sintassi Python per automatizzare le operazioni.
  * Esempio: ruotare un cristallo a passi di 1° e salvare a ogni passo le figure di diffrazione o le immagini STEM.
  * Le funzioni specifiche di ReciPro sono disponibili nello spazio dei nomi "ReciPro".
  * Esempi d'uso sono disponibili nel [manuale](https://seto77.github.io/ReciPro/it/20-macro/2-examples/).

### Altre funzioni

* **Simulatore del percorso elettronico**: simulazione Monte Carlo del percorso degli elettroni nei materiali.
* **EBSD** (diffrazione di elettroni retrodiffusi): in fase di sviluppo.

## Dettagli tecnici

* Scritto in **C++**, **C#** e **OpenGL Shading Language (GLSL)**.
* Parallelizzazione multithread per calcoli ad alte prestazioni su CPU moderne con molti core.
* Tutte le finestre funzionali si aggiornano in modo sincrono e in tempo reale quando cambia l'orientazione del cristallo.
* Utilizza un sistema di coordinate cartesiane destrorso (X: destra, Y: alto, Z: fronte) con la convenzione degli angoli di Eulero Z–X–Z.
* Le definizioni delle coordinate sono compatibili con i software EBSD di Thermo Fisher Scientific.

### Impatto accademico

* **Articolo sul software con revisione paritaria:** [Seto, Y. & Ohtsuka, M. (2022), *Journal of Applied Crystallography*, **55**, 397-410](https://doi.org/10.1107/S1600576722000139).
* **Articoli che lo citano:** [articoli citanti su Google Scholar](https://scholar.google.jp/scholar?cites=12625594477623342627).
* **Attenzione all'articolo:** [dettagli Altmetric](https://www.altmetric.com/details/123778746).

| Indicatore | Valore principale |
| --- | --- |
| Download totali da GitHub | oltre 27.000 download |
| Citazioni su Google Scholar | oltre 170 citazioni |
| Citazioni su Dimensions | oltre 160 citazioni |
| Lettori su Mendeley | oltre 90 lettori |

## Schermate

<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormMain.png" height="320px" alt="Finestra principale">
<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormCrystalDatabase.png" height="320px" alt="Banca dati cristallografica">
<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormSymmetryInformation.png" height="320px" alt="Informazioni di simmetria">
<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormBeamInteraction.png" height="320px" alt="Interazione del fascio">
<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormStructureViewer.png" height="320px" alt="Visualizzatore di strutture">
<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormStereonet.png" height="320px" alt="Proiezione stereografica">
<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormDiffractionSimulator.png" height="320px" alt="Simulatore di diffrazione">
<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormImageSimulator.png" height="320px" alt="Simulatore HRTEM/STEM">
<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormSpotIDV2.png" height="320px" alt="Spot ID v2">
<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormMacro.png" height="320px" alt="Macro">
<img src="https://seto77.github.io/ReciPro/assets/cap-it-auto/FormTrajectory.png" height="320px" alt="Simulatore del percorso elettronico">

***

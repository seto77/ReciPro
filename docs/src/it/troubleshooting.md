# Risoluzione dei problemi

Problemi comuni e soluzioni per ReciPro. Molte delle voci seguenti provengono da domande e segnalazioni di bug sul [tracker delle issue di GitHub](https://github.com/seto77/ReciPro/issues); la versione in cui un bug è stato corretto è indicata, ove applicabile.

> **La maggior parte dei problemi si risolve semplicemente aggiornando alla [versione più recente](https://github.com/seto77/ReciPro/releases/latest).** ReciPro viene aggiornato di frequente e molti dei bug seguenti sono stati corretti entro pochi giorni dalla segnalazione.

---

## Avvio e lancio

### Sintomo: il processo è in esecuzione ma non compare alcuna finestra

ReciPro si avvia (è visibile in Gestione attività), ma la sua finestra non compare mai sullo schermo.

**Causa**: la finestra si è aperta fuori dallo schermo — un problema con le coordinate di visualizzazione di Windows, tipicamente dopo aver cambiato monitor o la scalatura dello schermo. (Issue [#50](https://github.com/seto77/ReciPro/issues/50), [#53](https://github.com/seto77/ReciPro/issues/53), [#55](https://github.com/seto77/ReciPro/issues/55))

**Soluzione**:

1. Aprire **Gestione attività**.
2. Trovare **ReciPro** nell'elenco dei processi.
3. Fare clic destro su di esso e scegliere **Ingrandisci**.

La finestra verrà riportata sullo schermo principale. Si noti che **Passa a**, **Porta in primo piano** e **Riduci a icona** *non* aiutano — solo **Ingrandisci** funziona.

### Sintomo: ReciPro non si avvia, si blocca o si arresta in modo anomalo all'avvio

**Causa**: nella maggior parte dei casi l'inizializzazione di OpenGL fallisce, oppure un valore di registro/impostazione danneggiato blocca l'avvio.

**Soluzione** (provare in ordine):

1. **Disabilitare OpenGL**: tenere premuto il tasto **Ctrl** durante l'avvio di ReciPro per partire con OpenGL disabilitato. Le versioni recenti (v4.925 e successive) rendono più robusta l'inizializzazione di OpenGL, così l'app si avvia anche quando OpenGL fallisce — in tal caso le funzioni 3D sono disabilitate, ma il resto dell'app funziona.
2. **Reimpostare le impostazioni**: nell'editor del registro, eliminare la chiave `HKEY_CURRENT_USER\Software\Crystallography\ReciPro`, poi riavviare. (Equivale a **Option → Reset registry**.)
3. **Reinstallazione pulita**: disinstallare ReciPro, eliminare le cartelle seguenti se presenti (sostituire `<user>` con il nome del proprio account), poi reinstallare:
   - `C:\Users\<user>\AppData\Local\Crystallography Software\ReciPro`
   - `C:\Users\<user>\AppData\Roaming\ReciPro\ReciPro`
4. **Aggiornare** alla versione più recente.

Se nessuno di questi rimedi aiuta, la causa potrebbe risiedere nell'ambiente del sistema operativo stesso; si prega di [aprire una issue](https://github.com/seto77/ReciPro/issues) indicando i dettagli del proprio PC (CPU, GPU, versione di Windows).

---

## Problemi con OpenGL

### Sintomo: schermo nero o arresto anomalo all'avvio

**Causa**: GPU incompatibile o ambiente di desktop remoto.

**Soluzione**:

1. Andare in **Option → Disable OpenGL (needs restart)** (oppure tenere premuto **Ctrl** durante l'avvio).
2. Riavviare ReciPro.
3. Il Visualizzatore struttura e alcune funzioni 3D useranno il rendering software.

### Sintomo: una GPU integrata o più vecchia (Intel/AMD) non riesce a eseguire il rendering

**Causa**: alcune GPU integrate (ad es. AMD Radeon Vega, Intel UHD) presentavano problemi di inizializzazione di OpenGL nelle build più vecchie. (Issue [#2](https://github.com/seto77/ReciPro/issues/2))

**Soluzione**: aggiornare alla versione più recente. Il requisito sulla versione di OpenGL è stato abbassato (v4.781), l'inizializzazione delle GPU integrate è stata corretta (v4.785) e l'inizializzazione è stata ulteriormente irrobustita per fallire in modo controllato (v4.925). Anche aggiornare i driver della GPU aiuta.

### Sintomo: qualità di rendering scadente

**Soluzione**: aggiornare i driver della GPU. Si raccomanda una GPU esterna (dedicata) con supporto OpenGL 1.5.

---

## .NET Runtime

### Sintomo: l'applicazione non si avvia

**Causa**: il .NET Desktop Runtime richiesto non è installato. Le versioni attuali richiedono il **.NET Desktop Runtime 10.0** (build più vecchie: v4.895–v4.91x richiedevano la 9.0; vedere la issue [#43](https://github.com/seto77/ReciPro/issues/43)).

**Soluzione**: scaricarlo e installarlo da <https://dotnet.microsoft.com/download/dotnet/10.0> (scegliere il **Desktop Runtime**, x64 per la maggior parte dei PC).

### Sintomo: impossibile raggiungere la pagina di download di Microsoft

**Soluzione**: è possibile scaricare direttamente il programma di installazione del runtime. Scegliere il **Windows Desktop Runtime X64** per la propria architettura dalla [pagina di download di .NET 10.0](https://dotnet.microsoft.com/download/dotnet/10.0). (Issue [#49](https://github.com/seto77/ReciPro/issues/49))

---

## Installazione

### Sintomo: installazione o disinstallazione senza diritti di amministratore

**Nota**: i diritti di amministratore non sono richiesti. I collegamenti e i file specifici dell'utente vengono collocati nelle proprie cartelle utente (ad es. `%AppData%\Microsoft\Windows\Start Menu\Programs\Crystallography Software\` e il Desktop). (Issue [#38](https://github.com/seto77/ReciPro/issues/38))

---

## Visualizzazione e layout

### Sintomo: pulsanti o pannelli sono tagliati / nascosti, oppure il layout appare rovinato

Ad esempio, il pulsante **Peak Identification** in Spot ID v2 è nascosto, oppure la pagina Informazioni e altri form sono disallineati nelle versioni recenti. (Issue [#56](https://github.com/seto77/ReciPro/issues/56), [#59](https://github.com/seto77/ReciPro/issues/59))

**Causa**: una regressione di scalatura DPI / font dell'interfaccia introdotta in alcune build recenti.

**Soluzione**:

- Impostare la **scalatura dello schermo di Windows al 100%** (di solito questo ripristina il layout).
- Come soluzione temporanea rapida, **ridimensionare la finestra** (ad es. restringendola verticalmente) per far comparire i controlli nascosti.
- Aggiornare alla versione più recente — i layout vengono corretti progressivamente. Se una build recente appare peggiore, tornare a una versione leggermente più vecchia (ad es. v4.915) è un'opzione temporanea. Si prega di segnalare eventuali form ancora difettosi.

---

## Calcoli dinamici

### Sintomo: molto lento o memoria esaurita

**Causa**: troppe onde di Bloch o un'immagine troppo grande.

**Soluzione**:

- Ridurre **No. of Bloch waves** (50–200 di solito sono sufficienti per i calcoli di routine)
- Usare il risolutore **Eigen** per ≤ 500 onde; **MKL** per > 500 onde
- Ridurre la risoluzione dell'immagine per le simulazioni STEM
- Chiudere altre applicazioni che consumano molta memoria

### Sintomo: l'immagine HAADF-STEM è nera

**Causa**: i fattori di temperatura atomici (B) sono impostati a zero.

**Soluzione**: impostare B ≥ 0.5 Å² per tutti gli atomi. L'intensità TDS richiede fattori di temperatura diversi da zero.

---

## Simulatore di diffrazione

### Sintomo: la figura di diffrazione è vuota / non viene disegnato nulla

**Causa**: di solito la vista è ingrandita troppo, oppure l'energia dell'onda incidente è fuori intervallo. (Issue [#3](https://github.com/seto77/ReciPro/issues/3))

**Soluzione**:

- **Fare clic con il pulsante sinistro** nell'area di disegno principale per ridurre lo zoom.
- Controllare l'energia dell'onda incidente nella scheda **Wave** (in alto a sinistra): raggi X ≈ 1–100 keV, elettroni ≈ 10–1000 keV sono valori appropriati.

---

## Input/output di file

### Sintomo: il file CIF non si carica

**Soluzione**:

- Verificare che il file CIF sia ben formato
- Provare a trascinare e rilasciare il file sull'area **Informazioni sul cristallo**
- Alcune estensioni CIF non standard potrebbero non essere supportate

### Sintomo: il file dm3/dm4 non si carica, oppure "unable to cast … 'System.Single' to 'System.Double'"

**Causa**: esistono diverse varianti del formato DM3/DM4 e le build più vecchie non riuscivano a leggerle tutte. (Issue [#15](https://github.com/seto77/ReciPro/issues/15))

**Soluzione**: aggiornare alla versione più recente — la compatibilità di lettura DM3 è stata migliorata nella v4.835. Se un file continua a non caricarsi, si prega di [inviarlo](https://github.com/seto77/ReciPro/issues) in modo che il supporto possa essere aggiunto.

### Sintomo: il file dm3/dm4 mostra una scala errata

**Soluzione**: verificare la calibrazione nel software Digital Micrograph originale. ReciPro legge i metadati incorporati; se i metadati sono errati, impostare manualmente la dimensione dei pixel e la lunghezza di camera nel pannello Optics.

---

## Reimpostazione del registro

Se le impostazioni si danneggiano:

1. **Option → Reset registry (after restart)**
2. Riavviare ReciPro — posizioni delle finestre, lunghezza d'onda, lunghezza di camera, ecc. verranno reimpostate ai valori predefiniti

---

## Domande frequenti

### Esiste una versione per Mac (o Linux)? {#mac-linux}

Non esiste una versione ufficiale per Mac o Linux. ReciPro dipende dal **.NET Desktop Runtime**, che attualmente gira solo su Windows. (Issue [#12](https://github.com/seto77/ReciPro/issues/12))

Tuttavia, è stato segnalato un percorso non ufficiale che funziona su macOS: la distribuzione **win-x64 portable ZIP** (disponibile nella [pagina dei rilasci](https://github.com/seto77/ReciPro/releases/latest)) gira su macOS (Apple Silicon) utilizzando il wrapper Wine **Sikarugir** combinato con il driver OpenGL **Mesa3D** — senza necessità di licenza Windows o macchina virtuale. Una guida passo passo pubblicata da un utente è disponibile all'indirizzo <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>.

Si noti che questa configurazione non è ufficialmente supportata né completamente verificata. Una limitazione nota è che alcuni simboli (Å, apici, frecce) potrebbero essere visualizzati in modo errato.

**Correzione dei simboli corrotti (Å, apici, frecce):** la causa è che i font Windows che ReciPro normalmente utilizza (Segoe UI, Yu Gothic UI, ecc.) mancano nell'ambiente Wine, e i font sostitutivi integrati di Wine non contengono alcuni glifi scientifici. ReciPro passa automaticamente a font con ampia copertura **quando rileva di essere in esecuzione sotto Wine**, quindi la correzione consiste semplicemente nel rendere disponibili tali font nel prefix di Wine:

1. Installare **DejaVu Sans** / **DejaVu Serif** (copre Å, apici, frecce, etichette di frazione) e, per l'interfaccia giapponese, **Noto Sans CJK JP** (oppure **Noto Sans JP**).
2. Il modo più semplice è copiare i file `.ttf`/`.otf` scaricati nella cartella dei font del prefix — `…/drive_c/windows/Fonts/` all'interno del wrapper Sikarugir — poi riavviare ReciPro. (Anche `winetricks` può installarne alcuni.)
3. Al riavvio ReciPro li rileva automaticamente; non è necessario modificare alcuna impostazione di ReciPro.

Se i font non sono installati, ReciPro mantiene i nomi dei suoi font predefiniti, quindi nulla peggiora — i simboli rimangono semplicemente corrotti.

**Prospettive per questo percorso — due note oneste:**

- Lo ZIP **win-arm64** sperimentale **non** gira sui Mac attuali, nemmeno su Apple Silicon: le build Wine per macOS odierne (compreso Sikarugir) eseguono i binari Windows x86_64 tramite Rosetta 2 e non hanno alcun meccanismo per eseguire binari Windows ARM64. Su un Mac, usare sempre lo ZIP portable **win-x64**.
- Apple sta eliminando gradualmente Rosetta 2. macOS 27 (autunno 2026) è annunciato come l'ultima versione con pieno supporto a Rosetta 2, quindi si prevede che l'attuale percorso x64 + Rosetta smetterà di funzionare a partire da macOS 28 (autunno 2027). Un Wine ARM64 nativo per macOS è in sviluppo a monte; se si concretizzerà, lo ZIP win-arm64 potrebbe diventare il successore su Mac, ma ciò non può ancora essere promesso.

### ReciPro gira su Windows on ARM (ARM64)? {#windows-on-arm}

Sì — ci sono due percorsi:

- **Pacchetto ARM64 nativo (sperimentale, consigliato)**: dalla v4.938, un pacchetto portable ARM64 nativo sperimentale (`ReciPro-v.X_arm64.zip`; denominato `ReciPro-v.X-arm64.zip` fino alla v.4.939) è pubblicato nella [pagina dei rilasci](https://github.com/seto77/ReciPro/releases/latest). È self-contained, quindi non è necessaria alcuna installazione del .NET Runtime — estrarre lo ZIP in una cartella scrivibile dall'utente ed eseguire `ReciPro.exe`. Se Windows blocca lo ZIP scaricato (Mark of the Web), fare clic destro sullo ZIP → **Proprietà** → selezionare **Sblocca** → **OK** *prima* di estrarre (oppure eseguire `Unblock-File .\ReciPro-*arm64.zip` in PowerShell). I dettagli sono nel file `README-PORTABLE.txt` incluso.
- **Pacchetto x64 in emulazione**: anche il normale programma di installazione MSI e lo ZIP portable win-x64 girano su Windows ARM64 tramite l'emulazione x64 integrata, con il .NET Desktop Runtime (x64) installato (confermato circa dalla v4.913 con .NET 10). I calcoli pesanti girano più lentamente rispetto alla build nativa. (Issue [#47](https://github.com/seto77/ReciPro/issues/47))

Note sul pacchetto ARM64 nativo:

- Intel MKL non esiste per ARM64, quindi le corrispondenti opzioni del risolutore e voci di menu sono nascoste. I calcoli dinamici utilizzano la libreria nativa ottimizzata per NEON inclusa; in casi di validazione rappresentativi i suoi risultati hanno coinciso con la build x64 entro la tolleranza in virgola mobile attesa.
- Le viste 3D (Visualizzatore struttura e finestre correlate) possono funzionare, ma Windows on ARM fornisce OpenGL solo tramite un livello di traduzione Direct3D 12 (GLOn12 / Mesa), quindi il rendering 3D è notevolmente più lento rispetto a un PC con un driver OpenGL nativo — si tratta di una limitazione della piattaforma, non di un bug, e una build ARM64 nativa non può cambiarlo. La modalità di trasparenza **High quality (Per-Pixel Linked List)** nel Visualizzatore struttura è particolarmente lenta su questo stack di driver; si raccomanda la modalità predefinita **Approximate**. Se le viste 3D non si avviano, installare "OpenCL, OpenGL, and Vulkan Compatibility Pack" dal Microsoft Store.
- Il pacchetto ARM64 **non** gira su macOS + Wine (vedere la domanda precedente). Su un Mac, usare lo ZIP portable win-x64.

### Come devo citare ReciPro?

Usare il link **Cite this repository** nella [pagina del repository GitHub](https://github.com/seto77/ReciPro) (i metadati sono forniti da `CITATION.cff`). La citazione preferita è:

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

(Issue [#33](https://github.com/seto77/ReciPro/issues/33))

---

## Segnalazione di bug

Segnalare i problemi su: <https://github.com/seto77/ReciPro/issues>

Includere:

- Numero di versione di ReciPro
- Passaggi per riprodurre il problema
- Eventuali messaggi di errore o screenshot

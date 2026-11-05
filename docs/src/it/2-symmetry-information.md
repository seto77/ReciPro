# Informazioni di simmetria

**Informazioni di simmetria** visualizza informazioni dettagliate sulla simmetria del gruppo spaziale del cristallo selezionato e, in aggiunta, traccia diagrammi schematici degli elementi di simmetria e delle posizioni generali nello stile delle *International Tables for Crystallography* Vol. A.

![Informazioni di simmetria](../assets/cap-it-auto/FormSymmetryInformation.png)

La finestra è suddivisa in un'area di identità del gruppo spaziale (in alto a sinistra), un'area di calcolo/tabella con schede (in alto a destra) e due diagrammi schematici (in basso).

!!! tip "Teoria della simmetria (Appendice A4)"
    Che cosa codifica realmente un simbolo di Hermann–Mauguin/Hall/Schoenflies, le classificazioni secondo la teoria dei gruppi nella scheda **Proprietà** (centrosimmetrico, Sohncke, simmorfico, polare, …), il significato dei diagrammi degli elementi di simmetria e delle posizioni generali in basso, e le relazioni gruppo–sottogruppo mostrate da **Relazioni di gruppo…** sono tutti spiegati nell'**[Appendice A4. Simmetria e gruppi spaziali](appendix/a4-symmetry-space-groups/index.md)**.

---

## Scorciatoie da tastiera e mouse

Questa finestra non ha combinazioni speciali di tasti o di mouse. <kbd>F1</kbd> apre questa pagina del manuale, e i due pulsanti **Copia** copiano negli appunti il diagramma degli elementi di simmetria e il diagramma delle posizioni generali (come **emf** vettoriale o **bmp** raster, a scelta con **Formato di copia**).

→ Vedi **[21. Scorciatoie da tastiera e mouse](21-shortcuts.md)** per tutte le finestre a colpo d'occhio.

---

## Identità del gruppo spaziale

Il pannello in alto a sinistra elenca, per il gruppo spaziale corrente:

- **Numero** (1–230) e l'indice del setting
- **Sistema cristallino**
- **Gruppo puntuale** : simboli di Hermann–Mauguin (HM) e di Schoenflies (SF)
- **Gruppo spaziale** : simbolo HM corto, simbolo HM completo, simbolo SF e **simbolo Hall**

---

## Calcoli geometrici

![Calcoli geometrici](../assets/cap-it-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageGeometrics.png)

Inserisci due piani cristallini \((h_1, k_1, l_1)\), \((h_2, k_2, l_2)\) oppure due indici di direzione \([u_1, v_1, w_1]\), \([u_2, v_2, w_2]\) per ottenere:

- la distanza interplanare di ciascun piano / la lunghezza di ciascun asse,
- l'angolo tra i due piani (o i due assi),
- l'angolo tra la normale del piano 1 e l'asse 1, e tra la normale del piano 2 e l'asse 2 (90° quando l'asse giace nel piano),
- **L'asse parallelo a entrambi i piani** (l'asse di zona [*uvw*] dei due piani) e **Il piano contenente entrambi gli assi** (il piano (*hkl*) che contiene i due assi). I segni seguono [*uvw*] = (*h*₁*k*₁*l*₁) × (*h*₂*k*₂*l*₂) e (*hkl*) = [*u*₁*v*₁*w*₁] × [*u*₂*v*₂*w*₂]. <!-- 261106Cl 作者の決定 B16 --> <!-- 261105Cl 監査: 画面のラベル (FormSymmetryInformation.cs label40/label42) と面・軸の角度のツールチップ (numericBoxAnglePlaneAxis1/2.ToolTip) に合わせた。以前の「normal to」は逆 -->

Questi calcoli rispettano la metrica della cella elementare corrente. Le lunghezze sono mostrate in Å e gli angoli in gradi.

---

## Posizioni di Wyckoff

![Posizioni di Wyckoff](../assets/cap-it-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageWyckoff.png)

Elenca ogni posizione di Wyckoff con la sua molteplicità (**Mult.**), la lettera di Wyckoff (**Let. Wyck.**), la simmetria del sito (**Simm. sito**) e le **Coordinate** (fino a quattro per riga; gli elenchi più lunghi proseguono nelle righe successive). La prima voce di Wyckoff (molteplicità massima) è la posizione generale. Per i reticoli centrati, i vettori di traslazione reticolare sono mostrati nella riga di intestazione; per i gruppi R in assi esagonali si tratta delle tre traslazioni di centratura dell'impostazione diretta (obverse), (0,0,0)+, (2/3,1/3,1/3)+ e (1/3,2/3,2/3)+. <!-- 261105Cl 監査: R 群の六方軸でも中心化の行が出る (FormSymmetryInformation.cs, Controls ead58e7) -->

---

## Condizioni

![Condizioni](../assets/cap-it-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageConditions.png)

Le condizioni di riflessione derivanti dalla centratura del reticolo e dagli operatori di simmetria di slittamento ed elicoidali.

Ogni riga ha tre colonne: **Indici** (la classe di riflessioni, ad es. $hkl$, $h0l$, $00l$), **Condizione** (ad es. $h+k=2n$) ed **Elemento** (la centratura del reticolo o l'elemento di simmetria responsabile). Quando non ce n'è alcuna viene mostrato *No Condition*.

---

## Operazioni

![Operazioni](../assets/cap-it-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageOperations.png)

Elenca ogni operazione di simmetria della posizione generale (con le traslazioni di centratura del reticolo già espanse) come tripletta di coordinate, simbolo di Seitz e tipo geometrico in linguaggio corrente (ad es. *"3-fold rotation"*, *"c-glide plane"*, *"screw axis"*). **Copia (CIF)** copia l'elenco completo negli appunti come loop CIF `_space_group_symop_operation_xyz`.

→ Vedi l'**[Appendice A4.1](appendix/a4-symmetry-space-groups/symbols-and-diagrams.md#operazioni-di-simmetria-scheda-operazioni)** per come leggere queste tre notazioni.

---

## Proprietà

![Proprietà](../assets/cap-it-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageProperties.png)

Riporta le classificazioni secondo la teoria dei gruppi del gruppo spaziale corrente (molteplicità della posizione generale, ordine del gruppo puntuale, centrosimmetrico, Sohncke, simmorfico, direzione polare, coppia enantiomorfa, famiglia cristallina/sistema reticolare/tipo di Bravais, classe cristallina aritmetica, simmetria di Patterson) e quali proprietà fisiche macroscopiche (piroelettricità/ferroelettricità, piezoelettricità, generazione di seconda armonica, attività ottica) sono permesse da quella simmetria.

→ Vedi l'**[Appendice A4.1](appendix/a4-symmetry-space-groups/symbols-and-diagrams.md#classificazione-secondo-la-teoria-dei-gruppi-scheda-proprietà)** per il significato di ciascun termine.

---

## Impostazioni

![Impostazioni](../assets/cap-it-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageSettings.png)

Elenca, a titolo di riferimento, tutte le scelte tabulate di origine e di setting degli assi che condividono il numero IT del gruppo spaziale corrente, ciascuna con il proprio simbolo HM e simbolo Hall; il setting attualmente visualizzato è contrassegnato. Selezionare una riga non modifica il cristallo.

---

## Diagrammi degli elementi di simmetria e delle posizioni generali

![Diagrammi degli elementi di simmetria e delle posizioni generali](../assets/cap-it-auto/FormSymmetryInformation.splitContainer1.tableLayoutPanel1.png)

I due pannelli in basso riproducono i diagrammi schematici di simmetria del gruppo spaziale nella notazione delle *International Tables for Crystallography* Vol. A.

- **Elementi di simmetria (a sinistra)**: assi di rotazione/elicoidali, piani di riflessione/slittamento e centri di inversione/punti di rotoinversione sono disegnati con i simboli grafici convenzionali.
  - Per il reticolo \(F\) del sistema cubico viene mostrato solo il quarto in alto a sinistra della cella proiettata (vedere l'[Appendice A4.1](appendix/a4-symmetry-space-groups/symbols-and-diagrams.md#symmetry-element-diagram)).
  - Questi elementi di simmetria possono anche essere disegnati direttamente sul modello 3D nel [Visualizzatore struttura](5-structure-viewer.md).
- **Posizioni generali (a destra)**: le posizioni equivalenti generali sono rappresentate come cerchi (una virgola indica un'immagine speculare), ciascuno annotato con la sua altezza lungo l'asse di proiezione (`+`, `−`, `½+`, …; vedi l'[Appendice A4.1](appendix/a4-symmetry-space-groups/symbols-and-diagrams.md)).
  - Il punto di prova $(x,y,z)$ si imposta con le caselle **a** / **b** / **c** accanto al pulsante **Copia** delle posizioni generali (intervallo da −1 a 1, passo 0.01). Viene riportato a un valore predefinito per il gruppo puntuale ogni volta che il gruppo puntuale cambia, e mantenuto quando cambia solo il gruppo spaziale all'interno dello stesso gruppo puntuale.
  - Solo per il sistema cubico, linee ausiliarie collegano i tre cerchi correlati da un asse di rotazione ternario.

Controlli (il pannello **Opzioni** in basso nella finestra e il pulsante **Copia** sopra ciascun diagramma):

- **Direzione** (`a` / `b` / `c`) : scegli l'asse cristallino lungo cui proiettare. È selezionabile solo per i gruppi spaziali ortorombici (predefinito `c`); per i gruppi monoclini si usa automaticamente l'asse unico, e per tutti gli altri sistemi cristallini `c`.
- **Copia** : copia ciascun diagramma negli appunti nel formato selezionato con **Formato di copia** (**emf** vettoriale / **bmp** raster); l'emf può essere separato nei suoi componenti e modificato in PowerPoint.
- **Relazioni di gruppo…** apre un browser per le relazioni sottogruppo massimale/supergruppo minimale del gruppo spaziale corrente. Vedi l'[Appendice A4.2](appendix/a4-symmetry-space-groups/group-subgroup-relations.md) per come leggerlo.

---

## Vedi anche

- [Database dei cristalli](1-crystal-database.md)
- [Visualizzatore struttura](5-structure-viewer.md)
- [Stereogramma](6-stereonet.md)
- [Geometria di rotazione](4-rotation-geometry.md)
- [Finestra principale](0-main-window.md)
- [Appendice A4. Simmetria e gruppi spaziali](appendix/a4-symmetry-space-groups/index.md) — il contesto cristallografico e di teoria dei gruppi dietro ogni scheda e diagramma di questa pagina.

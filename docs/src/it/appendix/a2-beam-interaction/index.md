# Appendice A2. Interazione del fascio (contesto di fisica dello stato solido)

Il capitolo dedicato alla finestra principale [3. Beam interaction](../../3-beam-interaction.md) è una guida alla GUI: spiega quali pulsanti premere e cosa significa ciascuna colonna. Questa appendice raccoglie la **fisica dello stato solido e della diffusione** che sta dietro a questi numeri — perché un atomo diffonde raggi X, elettroni e neutroni in modo così diverso, da dove provengono il fattore di struttura e la sua parte immaginaria, come un fascio viene attenuato e rallentato all'interno di un solido, e cosa rappresenta e cosa non rappresenta l'anteprima di fluorescenza.

![Beam Interaction window](../../../assets/cap-it-auto/FormBeamInteraction.png)

La finestra ha quattro schede, e la teoria si legge meglio nell'ordine in cui una grandezza alimenta la successiva:

1. **[Atomic scattering factors](scattering-factor.md)** — come un *singolo atomo* diffonde ciascun tipo di fascio.
2. **[Structure factor](structure-factor.md)** — come gli atomi in una *cella elementare* interferiscono, incluso il fattore di Debye–Waller e le regole di estinzione.
3. **[Attenuation & transport](attenuation-transport.md)** — come il fascio viene *rimosso e rallentato* mentre attraversa il materiale.
4. **[Fluorescence](fluorescence.md)** — l'emissione caratteristica di raggi X che segue la ionizzazione di una shell interna.

---

## Geometria di diffusione e la variabile $s$

Ogni grandezza di diffusione in questa finestra è funzione di quanto cambia la direzione del fascio. Scrivendo $\mathbf k_i$ e $\mathbf k_s$ per i vettori d'onda incidente e diffuso (elastico, quindi $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$), il **vettore di diffusione** e il suo modulo sono

$$\mathbf Q = 2\pi(\mathbf k_s - \mathbf k_i), \qquad Q = |\mathbf Q| = \frac{4\pi\sin\theta}{\lambda} = 4\pi s .$$

- $\theta$ : l'angolo di Bragg — la *metà* dell'angolo di diffusione totale. La tabella delle riflessioni riporta l'angolo completo $2\theta$.
- $s = \dfrac{\sin\theta}{\lambda}$ (Å⁻¹) : la variabile rispetto alla quale viene tracciata la scheda **Scattering factors**. È l'argomento naturale di ogni fattore di forma atomico.
- $d$ : la distanza interplanare. Alla condizione di Bragg $\lambda = 2d\sin\theta$, quindi $s = \dfrac{1}{2d} = \dfrac{|\mathbf g|}{2}$, dove $\mathbf g$ è il vettore del reticolo reciproco con $|\mathbf g| = 1/d$.

Queste tre convenzioni descrivono la stessa geometria; differisce solo la scala. Vale la pena tenere chiara la corrispondenza, dato che la finestra ne usa più di una:

| Nella finestra | Simbolo | Relazione |
|---|---|---|
| Tabella delle riflessioni | $q = 2\pi/d$ | $q = 2\pi\lvert\mathbf g\rvert = Q = 4\pi s$ |
| Tabella delle riflessioni | $2\theta$ | angolo di diffusione completo, $\sin\theta = \lambda s$ |
| Scheda Scattering factors | $s = \sin\theta/\lambda$ | $s = q/4\pi = 1/(2d)$ |
| Diagramma del picco di diffrazione | $Q = 4\pi\sin\theta/\lambda$ | $Q = q = 4\pi s$ |

!!! note "Unità"
    Le parametrizzazioni pubblicate dei fattori di forma usano $s$ in Å⁻¹ (quindi $s^2$ in Å⁻²), mentre ReciPro tratta internamente $s^2$ in nm⁻². Le due differiscono per un fattore $100$ in $s^2$; le curve e le tabelle sono presentate nelle unità indicate nell'intestazione di ciascuna tabella. Un modello — **Kirkland** — è tabulato rispetto a $q = 2s = 1/d$ anziché a $s$; vedi [Atomic scattering factors](scattering-factor.md).

### Bragg, Laue e la sfera di Ewald {#phase-convention}

La condizione di Bragg è una faccia di un unico requisito geometrico. L'interferenza costruttiva (la **condizione di Laue**) richiede che il vettore di diffusione sia uguale a un vettore del reticolo reciproco,

$$\mathbf k_s = \mathbf k_i + \mathbf g, \qquad |\mathbf k_i + \mathbf g|^2 = |\mathbf k_i|^2 ,$$

che, con $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$, si riduce a

$$2\,\mathbf k_i\cdot\mathbf g + |\mathbf g|^2 = 0 \qquad\Longleftrightarrow\qquad |\mathbf g| = \frac{1}{d} = \frac{2\sin\theta}{\lambda},$$

cioè la **legge di Bragg** $\lambda = 2d\sin\theta$. Geometricamente questa è la costruzione della **sfera di Ewald**: una riflessione è eccitata quando il suo punto del reticolo reciproco giace sulla sfera di raggio $1/\lambda$. (Qui $\mathbf g$ è in unità di $1/d$, quindi $\mathbf Q = 2\pi\mathbf g$.)

---

## Convenzione di fase

ReciPro costruisce i fattori di struttura con la convenzione di fase cristallografica

$$F_{\mathbf g} = \sum_j \dots \exp\!\left(-2\pi i\,\mathbf g\cdot\mathbf r_j\right),$$

cioè con un segno **meno** nell'esponente. Questa scelta fissa il segno della parte immaginaria del fattore di struttura (`F_inv` nella tabella delle riflessioni) e la relazione tra le coppie di Friedel una volta attivata la dispersione anomala. Viene stabilita qui una volta sola e assunta in tutta l'appendice; le conseguenze sono sviluppate in [Structure factor](structure-factor.md).

---

## Diffusione cinematica vs dinamica

Questa appendice tratta la **diffusione singola (cinematica)**: il fascio incidente viene diffuso una sola volta, e l'ampiezza diffratta è il fattore di struttura della pagina successiva. È l'immagine corretta quando l'interazione è debole — raggi X e neutroni in quasi tutti i campioni, ed elettroni in campioni *molto sottili*.

Quando l'interazione è forte — elettroni in tutti i cristalli tranne i più sottili — il fascio viene diffuso molte volte prima di uscire, l'intensità viene ridistribuita tra le riflessioni, e $\lvert F\rvert^2$ non fornisce più l'intensità misurata. Questo regime richiede la teoria **dinamica** dell'[Appendix A3](../a3-bloch-wave/index.md). I fattori di diffusione e i fattori di struttura qui derivati sono l'*input* per entrambe le immagini.

Anche nel limite cinematico l'ampiezza diffratta non è il solo fattore di struttura: sommando l'onda diffusa attraverso una lastra di spessore $t$ si ottiene

$$A_{\mathbf g}(t) \;\propto\; F_{\mathbf g}\int_0^t e^{\,2\pi i S_{\mathbf g} z}\,dz = F_{\mathbf g}\, t\, e^{\,\pi i S_{\mathbf g} t}\,\operatorname{sinc}(\pi S_{\mathbf g} t),$$

dove $S_{\mathbf g}$ è l'**errore di eccitazione** — la distanza del punto del reticolo reciproco dalla sfera di Ewald. L'intensità ha un picco netto in $S_{\mathbf g}=0$ e oscilla con lo spessore (l'origine delle frange di spessore); la teoria dinamica dell'[Appendix A3](../a3-bloch-wave/index.md) sostituisce questo risultato a fascio singolo con un comportamento a fasci accoppiati.

---

## Le tre sonde a colpo d'occhio

| | Raggi X | Elettrone | Neutrone |
|---|---|---|---|
| Interagisce con | densità elettronica $\rho_e$ | potenziale elettrostatico $V$ | nuclei (e spin spaiati) |
| Intensità dell'interazione | debole | forte | molto debole |
| Penetrazione tipica | µm – mm | nm – µm | mm – cm |
| Diffusione singola valida? | quasi sempre | solo film sottili | quasi sempre |
| Sensibilità agli atomi leggeri | scarsa ($\propto Z$) | moderata | spesso eccellente |

Questi contrasti ricorrono lungo le pagine seguenti, ciascuno riconducibile al meccanismo di diffusione in [Atomic scattering factors](scattering-factor.md).

---

## Vedi anche

- [3. Beam interaction](../../3-beam-interaction.md) — la GUI che questa appendice spiega.
- [Atomic scattering factors](scattering-factor.md) · [Structure factor](structure-factor.md) · [Attenuation & transport](attenuation-transport.md) · [Fluorescence](fluorescence.md)
- [Appendix A1. Coordinate systems](../a1-coordinate-system/1-orientation.md)
- [Appendix A3. Dynamical diffraction (Bloch-wave method)](../a3-bloch-wave/index.md) — la teoria della diffusione multipla che usa questi fattori di diffusione.

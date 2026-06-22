# Appendice A1.2. Sistema di coordinate per la simulazione di diffrazione

<!-- 260526Cl: 図(Coordinates4-5)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

La funzione **Simulatore di diffrazione** simula il pattern di diffrazione registrato su un rivelatore. Il rivelatore è un piano finito di pixel posto a una distanza fissa dal campione e può essere inclinato rispetto al fascio incidente. Per riprodurre tutto ciò con precisione sono necessari la relazione geometrica tra il rivelatore e il campione, insieme alla dimensione dei pixel e al numero di pixel del rivelatore. Per il sistema di coordinate di base (di orientazione), vedere [A1.1. Sistema di coordinate di base e orientazione del cristallo](1-orientation.md).

!!! note "Z e Y differiscono dal sistema di orientazione"
    Nel sistema di coordinate del rivelatore, <span class="rp-steel">$Z$</span> è parallelo al fascio e <span class="rp-steel">$Y$</span> punta verso il basso. Questo differisce dal sistema di coordinate di orientazione, in cui il fascio è lungo <span class="rp-blue">$-Z$</span> e <span class="rp-green">$Y$</span> punta verso l'alto. Il sistema del rivelatore segue la consueta convenzione di immagine/rivelatore (origine in alto a sinistra, <span class="rp-steel">$Y$</span> crescente verso il basso).

## Prima della rotazione (rivelatore perpendicolare al fascio)

![Sistema di coordinate del rivelatore con rivelatore perpendicolare al fascio](../../../assets/references/Coordinates4.png){width=500px}

Vengono definiti tre sistemi di coordinate:

- <span class="rp-steel">**Coordinate reali** ($X$, $Y$, $Z$)</span> : coordinate cartesiane 3D in mm, con il <span class="rp-steel">**campione**</span> come origine. <span class="rp-steel">$Z$</span> è parallelo al fascio; osservando lungo <span class="rp-steel">$Z$</span>, <span class="rp-steel">$X$</span> punta a destra e <span class="rp-steel">$Y$</span> verso il basso. Quando il rivelatore è perpendicolare al fascio, <span class="rp-steel">$X$ / $Y$</span> sono paralleli a <span class="rp-brown">$X'$ / $Y'$</span>.
- <span class="rp-brown">**Coordinate del rivelatore** ($X'$, $Y'$)</span> : coordinate 2D in mm sul piano del rivelatore, con il <span class="rp-brown">**foot**</span> come origine. <span class="rp-brown">$X'$ / $Y'$</span> puntano a destra / in basso sul rivelatore e sono paralleli a <span class="rp-cyan">$X''$ / $Y''$</span>.
- <span class="rp-cyan">**Coordinate in pixel** ($X''$, $Y''$)</span> : coordinate 2D in unità di pixel, con l'<span class="rp-cyan">**angolo superiore sinistro**</span> del rivelatore come origine, seguendo le righe e le colonne di pixel del rivelatore.

Quando il rivelatore è perpendicolare al fascio, il <span class="rp-brown">**foot**</span> e il <span class="rp-red">**direct spot**</span> coincidono, e <span class="rp-red">**Camera length 1**</span> è uguale a <span class="rp-brown">**Camera length 2**</span>.

## Dopo la rotazione (rivelatore inclinato)

![Sistema di coordinate del rivelatore con rivelatore inclinato](../../../assets/references/Coordinates5.png){width=500px}

L'inclinazione del rivelatore è descritta da due parametri:

| Parametro | Descrizione |
|-----------|-------------|
| <span class="rp-grass">$\varphi$</span> | Direzione dell'<span class="rp-grass">asse di rotazione</span> — il suo angolo rispetto all'asse <span class="rp-steel">$X$</span>, misurato nel piano <span class="rp-steel">$XY$</span> (<span class="rp-steel">$Z$</span> = 0) |
| <span class="rp-grass">$\tau$</span> | Angolo di rotazione attorno a quell'asse (vite destrorsa) |

Una volta che il rivelatore è inclinato:

- Il <span class="rp-red">**direct spot**</span> e il <span class="rp-brown">**foot**</span> non coincidono più.
- <span class="rp-red">**Camera length 1** ($C_1$)</span> = distanza dal <span class="rp-steel">campione</span> al <span class="rp-red">direct spot</span>.
- <span class="rp-brown">**Camera length 2** ($C_2$)</span> = distanza dal <span class="rp-steel">campione</span> al <span class="rp-brown">foot</span>.
- L'origine delle <span class="rp-brown">**Coordinate del rivelatore**</span> rimane al <span class="rp-brown">**foot**</span>; l'origine delle <span class="rp-cyan">**Coordinate in pixel**</span> rimane all'<span class="rp-cyan">**angolo superiore sinistro**</span>.
- Le direzioni <span class="rp-steel">$X$ / $Y$</span> non coincidono più con <span class="rp-brown">$X'$ / $Y'$</span>.

## Glossario dei parametri

| Termine | Definizione |
|------|------------|
| <span class="rp-steel">**Campione (Sample)**</span> | Il materiale che diffonde il fascio incidente; l'origine delle coordinate reali |
| <span class="rp-steel">**Coordinate reali** ($X$, $Y$, $Z$)</span> | Coordinate 3D (mm) della configurazione sperimentale; origine al campione, <span class="rp-steel">$Z$</span> sempre parallelo al fascio |
| <span class="rp-red">**Direct spot**</span> | Intersezione del fascio incidente con il rivelatore |
| <span class="rp-brown">**Foot**</span> | Il piede della perpendicolare dal campione al piano del rivelatore; origine delle coordinate del rivelatore. Coincide con il direct spot solo quando il rivelatore è perpendicolare al fascio. Per la modalità immagine sovrapposta, impostare la posizione del foot in coordinate in pixel |
| <span class="rp-brown">**Coordinate del rivelatore** ($X'$, $Y'$)</span> | Coordinate 2D (mm) sul piano del rivelatore; origine al foot |
| <span class="rp-cyan">**Coordinate in pixel** ($X''$, $Y''$)</span> | Coordinate 2D (pixel) sul piano del rivelatore; origine all'angolo superiore sinistro |
| <span class="rp-red">**Camera length 1** ($C_1$)</span> | Distanza dal campione al direct spot (mm) |
| <span class="rp-brown">**Camera length 2** ($C_2$)</span> | Distanza dal campione al foot (mm) |
| **Pixel size** | Lunghezza del lato di un pixel (quadrato) (mm); sono supportati solo pixel quadrati |
| **Detector width / height** | Numero di pixel in orizzontale / verticale |

# Simulación ALCHEMI

**ALCHEMI (Atom Location by CHannelling-Enhanced MIcroanalysis)** determina **qué sitio ocupa un dopante** midiendo los rendimientos de rayos X característicos mientras el cristal se inclina a lo largo de una fila sistemática, y leyendo la dependencia con la orientación. El simulador ALCHEMI de ReciPro calcula hacia adelante la **curva de inclinación (rendimiento de ionización frente a orientación)** a partir de una estructura cristalina y un conjunto de hipótesis de sitio.

> **Es una función Preview.** La v1 solo realiza **cálculo directo unidimensional**; el ajuste a datos experimentales y el mapa 2D (2D-HARECXS) no están implementados (esas pestañas están ocultas). **Hasta donde saben los autores, no existe ningún otro simulador directo de ALCHEMI disponible públicamente.** Como no hay implementación con la que contrastar, lea [Alcance y limitaciones conocidas](#alcance-y-limitaciones-conocidas) antes de usar los resultados cuantitativamente.

Se abre desde el menú **Opciones** del [Simulador de difracción](index.md) → **Simulador ALCHEMI...**

Condiciones de la GUI: Wave Length = Electron (el cristal, la tensión de aceleración y la orientación se toman del simulador de difracción principal)

![Simulador ALCHEMI](../../assets/cap-es-auto/FormALCHEMI.png)

La ventana tiene **los ajustes a la izquierda** (barrido, espesor, cálculo, canales de ionización, hipótesis de sitio) y **el resultado a la derecha** (pestaña Curva).

---

## Qué se calcula

Para cada orientación incidente se resuelve el campo de ondas dentro del cristal con el método de ondas de Bloch, y para cada par de sitio $s$ y canal de ionización $c$ el rendimiento de ionización se integra analíticamente hasta el espesor $t$.

$$
Y_\text{dyn} = \mathrm{Re} \sum_{jj'} \alpha_j^{*}\,\bigl(C^{\dagger} \mu_{s,c} C\bigr)_{jj'}\, \alpha_{j'}\, F_{jj'}(t),
\qquad F_{jj'}(t) = \frac{e^{\lambda t} - 1}{\lambda}
$$

La matriz de ionización $\mu$ solo depende de la diferencia de dos reflexiones, $G = \mathbf{g}_h - \mathbf{g}_g$.

$$
\mu_{hg} = \sum_a \mathrm{Occ}_a\, e^{-M_a(G)}\, \sigma_c\, F_c(|G|/2)\, e^{-2\pi i\,G \cdot \mathbf{r}_a}
$$

- $\sigma_c$ : sección eficaz total de ionización, modelo **Bote–Salvat**
- $F_c(s)$ : factor de forma de ionización normalizado, tablas **DHFS** generadas internamente (la misma base de datos que [Interacción del haz](../3-beam-interaction.md) y [STEM-EDX](../9-hrtem-stem-simulator/2-stem-simulation.md))
- $e^{-M_a(G)}$ : factor de Debye-Waller (se admiten ADP anisótropos)

Corresponde a la **aproximación de factor de forma local** de ICSC (Oxley & Allen 2003). No se usa la MDFF de dos momentos.

### Componente descanalizada

Los electrones extraídos del campo de Bloch coherente por la absorción térmica difusa recorren el espesor restante como electrones de dirección aleatoria, y también ionizan allí.

$$
Y_\text{dech} = \frac{\mu_{00}}{V_c}\,\bigl(t - L_\text{coh}(t)\bigr),
\qquad L_\text{coh}(t) = \int_0^t \sum_g |\psi_g(z)|^2\,dz
$$

Desmarcar **Incluir la componente descanalizada** en el cuadro **Cálculo** elimina este término. Supone decenas de por ciento del rendimiento total a espesores típicos, así que omitirlo hace que el contraste de sitio parezca más fuerte de lo que es.

### Magnitud de salida

La magnitud primaria es el **número de vacantes de capa interna generadas por electrón incidente**. **NO se aplican la conversión a fotones de rayos X (rendimiento de fluorescencia y ramificación de líneas), la autoabsorción de rayos X en la muestra ni la eficiencia y el ángulo sólido del detector.**

⚠ **Las vacantes no son cuentas.** Entre esta magnitud y una intensidad EDX medida quedan tres etapas más — atómica, de la muestra e instrumental —, ninguna de las cuales realiza ReciPro.

1. **vacante → fotón** : rendimiento de fluorescencia y ramificación de líneas de la capa
2. **fotón → fotón que sale de la muestra** : autoabsorción de rayos X, que depende de la **profundidad a la que se creó el fotón** y del ángulo de salida
3. **fotón → cuenta** : eficiencia del detector, ángulo sólido y el procesado del espectro

La etapa 2 en particular no se recupera después multiplicando la curva terminada por un único factor de absorción: habría que resolver antes el rendimiento en profundidad. Comparar estas curvas con intensidades medidas, factores k o composiciones exige por tanto realizar esas etapas fuera de ReciPro.

Fíjese en cuáles sobreviven a una normalización. Las etapas 1 y 3, y cualquier absorción tratada como constante, son **multiplicativas e independientes de la orientación**, así que desaparecen en la normalización ICP (media del barrido), incluso para dos líneas de energías muy distintas. **La autoabsorción, en general, no**: la canalización cambia la distribución en profundidad en que se crean las vacantes, de modo que la fracción absorbida varía a lo largo del barrido y sobrevive a la normalización. Es contra ese residuo que ayuda elegir líneas de energía parecida.

---

## Panel izquierdo: ajustes

### Barrido de inclinación

| Elemento | Descripción | Predeterminado |
|----------|-------------|----------------|
| **Fila ( h k l )** | Fila sistemática a barrer, dada como índices de reflexión. El eje de inclinación se toma perpendicular tanto al haz como a este $\mathbf{g}$, de modo que el barrido recorre esta fila a través de sus condiciones de Bragg | (1 0 0) |
| **Rango ±** | Semianchura del barrido de inclinación (mrad). Más allá de unos 10 mrad ya no se garantiza una base unión fija, y más allá de 30 mrad queda fuera de la garantía de la v1 | 8 mrad |
| **Puntos** | Número de puntos del barrido (3–1001) | 101 |

La línea inferior muestra el ángulo de Bragg $\theta_B$ de la fila elegida, a cuántos $\theta_B$ corresponde la anchura del barrido, y el paso de inclinación, de modo que se ve hasta dónde llega realmente el barrido antes de ejecutarlo.

⚠ **El valor por defecto de ±8 mrad es un punto de partida cómodo, no un óptimo de la literatura.** La revisión de Jones (2002) no prescribe ninguna anchura numérica de barrido en mrad, y los límites superiores citados en la tabla anterior son límites del cálculo numérico de la v1, no recomendaciones. Juzgue la amplitud en unidades de $\theta_B$ (es lo que indica la línea bajo la tabla) y elíjala de modo que los rasgos dinámicos que quiere comparar queden dentro del barrido.

⚠ La afirmación de que la iluminación puede abrirse hasta **aproximadamente el ángulo de Bragg** — dada por Jones para la condición optimizada de fila sistemática — se refiere al **semiángulo de convergencia del cono incidente**, es decir, a **Dispersión angular** en el cuadro **Cálculo** de más abajo. **No** es una semianchura de barrido recomendada. Son magnitudes distintas y no deben confundirse.

### Espesor

Indique inicio, fin y paso (nm). **Todos los espesores se calculan juntos en una sola ejecución**, y el resultado se conmuta con el deslizador bajo la curva.

El contraste de sitio cambia mucho —e incluso puede invertir su signo— entre muestras delgadas y gruesas, así que compruebe varios espesores antes de sacar conclusiones. Por eso el selector de espesor está justo debajo de la curva.

### Cálculo

| Elemento | Descripción | Predeterminado |
|----------|-------------|----------------|
| **Haces máx.** | Cota superior del número de ondas de Bloch por orientación (1–1600). La unión sobre todo el barrido es mayor | 120 |
| **Solucionador** | Motor de cálculo del problema de autovalores: **Nativo** (Eigen C++) o **Gestionado** (.NET). Donde el solucionador nativo no está disponible, la elección queda fijada en Gestionado | Nativo |
| **Incluir la componente descanalizada** | Si se suma $Y_\text{dech}$ anterior | activado |
| **Dispersión angular** | Convoluciona la curva con la dispersión angular del haz incidente: **Ninguno** o **Gaussian** con una anchura a media altura en mrad. Es un posprocesado sobre el eje de orientaciones, aplicado **antes** de la normalización de visualización | Ninguno |

**El tope de 1600 haces es la contrapartida del rango tabulado $s \le 16\ \text{Å}^{-1}$ del factor de forma de ionización.** En la práctica, incluso 1600 haces solo requieren unos 10,5 Å⁻¹, así que el rango tabulado nunca se agota mientras se respete el tope. El valor realmente alcanzado se indica en la línea de [diagnóstico de la base](#diagnóstico-de-la-base) bajo el gráfico.

### Canales de ionización

Lista de elemento y capa a ionizar. Cada fila se lee `elemento (Z) capa   energía del borde   U = sobretensión`, con una etiqueta entre paréntesis donde hace falta precaución.

- Los canales que **no pueden excitarse** (la energía incidente está por debajo del borde de absorción) o que quedan **fuera del rango tabulado** se listan con el motivo y no pueden marcarse
- Los canales cuya sobretensión $U = E_0/E_\text{borde}$ es inferior a 1,2 llevan una advertencia, porque allí la sección eficaz es menos fiable

### Hipótesis de sitio

Lista de sitios atómicos cuyo rendimiento se calcula por separado, mostrados como `etiqueta elemento (x, y, z) ×multiplicidad Occ ocupación`.

⚠ **En la imagen de trazador, un canal puede emparejarse con cualquier sitio.** Emparejar el canal de ionización de un dopante con la geometría de un sitio anfitrión (posición, ADP, ocupación) es el uso previsto; restringir el emparejamiento a elementos coincidentes sería un error. Se calculan **todas las combinaciones** de los canales y sitios marcados.

### Simular / Detener

**Simular** inicia el barrido. El progreso se muestra en la barra de estado en cinco etapas (resolviendo datos de ionización → construyendo la base unión → construyendo las matrices de ionización → resolviendo orientaciones → verificando la base ampliada), y **Detener** aborta en cualquier momento.

---

## Panel derecho: pestaña Curva

Al terminar el cálculo se dibuja una curva por cada par sitio × canal. La leyenda se lee `etiqueta de sitio / canal`.

| Elemento | Descripción |
|----------|-------------|
| **Espesor** | Selecciona el espesor mostrado con un deslizador (no se recalcula nada) |
| **Normalización** | **Media del barrido (ICP)** = dividir por la media de todo el barrido (la magnitud que se usa normalmente en ALCHEMI) / **Máximo = 1** / **Bruto (por electrón)** |
| **Eje X** | Alterna entre **mrad** y **θ_B** (en unidades del ángulo de Bragg de la fila barrida) |
| **Condiciones de Bragg** | Dibuja líneas verticales en $\theta = n\,\theta_B$ |
| **Exportar CSV** | Escribe las curvas brutas de cada orientación, espesor, sitio y canal en un archivo CSV ([abajo](#exportación-csv)) |

⚠ **La normalización es solo una transformación de visualización.** La magnitud almacenada son siempre las vacantes generadas por electrón incidente, y **Máximo = 1 es solo para visualización**: no debe usarse como referencia ICP.

### Contraste y correlación

La primera línea bajo la curva indica, por serie, el **contraste** $(\max-\min)/\text{media}$ y el **coeficiente de correlación** $r$ frente a la primera serie. Es un resumen para juzgar de un vistazo qué sitio está actuando: dos series con $r$ próximo a $+1$ tienen la misma dependencia con la orientación, es decir, esos datos no pueden separar esos sitios.

### Diagnóstico de la base

La segunda línea informa del estado de la base.

```text
basis 347 (184 + 163)   F(s) ≤ 6.20 Å⁻¹   expanded-basis 6.7e-3   ⚠ aptitud para ajuste NO evaluada   ⚠ Experimental: verificado cuantitativamente solo para beta-AlCo [001] a 250 keV
```

- **basis N (solo centro + añadidos por la unión)** : tamaño de la unión verdadera de reflexiones sobre todas las orientaciones del barrido
- **F(s) ≤ … Å⁻¹** : el mayor argumento del factor de forma que la base realmente requirió
- **expanded-basis** : máxima diferencia relativa al resolver de nuevo el centro y ambos extremos del barrido con una base 1,25×. Es un **sustituto del error de convergencia**
- **aptitud para ajuste** : la v1 informa siempre **NO evaluada**. El diagnóstico tiene tres defectos conocidos —su denominador es el
  máximo sobre todo el tensor, su numerador es el rendimiento absoluto, y pasa trivialmente cuando la base 1,25× no crece de
  verdad—, así que certificar un resultado como «apto» erraría en la dirección peligrosa
- **Experimental** : cada ejecución lleva esta etiqueta junto con el rango verificado, porque solo β-AlCo se ha comprobado cuantitativamente

⚠ **La v1 no certifica ajustes cuantitativos de ocupación.** El valor bruto del diagnóstico se sigue mostrando y cuanto menor mejor, pero trátelo como una indicación, no como una marca de aprobado. Tenga en cuenta además que se define sobre el **rendimiento absoluto**, por lo que resulta conservador si solo mira el ICP (que divide por la media del barrido).

En las siguientes situaciones se añaden más advertencias.

- **Tensión de aceleración por debajo de 80 kV** : a esta tensión la tabla de factores de forma no garantiza $s$ hasta $16\ \text{Å}^{-1}$. El cálculo en sí sigue siendo correcto mientras el $s$ requerido por la base permanezca dentro del rango certificado, así que se trata de un **aviso, no de un rechazo**
- **Truncamiento del factor de forma** : allí donde $F(s)$ más allá del rango certificado se truncó a cero, **se muestra numéricamente la cota de error resultante $|F| \le \varepsilon$**. Nada se extrapola en silencio

---

## Exportación CSV {#exportación-csv}

**Exportar CSV** escribe una tabla en formato largo precedida por una cabecera con el formato `# key: value` (abreviada abajo). La cabecera está pensada para que el propio archivo indique las condiciones necesarias para reproducirlo.

```text
# generator: ReciPro ALCHEMI, ver 4.947 (2026-08-09)
# model: LocalFormFactor (local form-factor approximation; NOT the two-momentum MDFF)
# quantity: IonizationVacanciesGenerated (PerIncidentElectron)
# crystal: MgAl2O4 (spinel) / F d -3 m
# cell_nm: a 0.808000 b 0.808000 c 0.808000 alpha 90.0000 beta 90.0000 gamma 90.0000 deg
# accelerating_voltage_kV: 200.000
# scan_row_hkl: 1 0 0
# theta_B_mrad: 1.552030
# thicknesses_nm: 10.0000 20.0000 ... 100.0000
# angular_spread: Gaussian1D FWHM 1.0000 mrad (kernel renormalized at the scan ends)
# processing_order: forward yield -> angular spread convolution -> (display normalization, NOT applied to these columns)
# basis: 202 beams (120 centre-only + 82 added by the union), hash 1F3A...
# expanded_basis_max_rel_diff: 9.500e-004
# fit_eligibility: NotEvaluated (v1 does not certify quantitative occupancy fits; raw diagnostic AcceptedForFit=True at tolerance 3e-3)
# occupancy_coupling: Tracer (dilute limit; site responses may be combined linearly). VCA is not implemented
# verification: Experimental. Quantitatively verified only for beta-AlCo [001] at 250 keV (Al-K / Co-K / Co-L). ...
# not_modelled: X-ray self-absorption, detector efficiency and solid angle, fluorescence yield and line branching, background, specimen thickness distribution, specimen bending
# channel[Al-K]: edge 1.5596 keV, sigma 1.95e-007 nm2, sigma_source ... , F(s)_source ... (tabulated to s = 16.0 A^-1), not truncated
# site[AlM]: atom indices 0, occupancy from the crystal
# conventions: tilt is the signed rotation about the axis perpendicular to both the beam and g(scan_row_hkl), positive toward +g; angles in mrad; lengths in nm; ...
tilt_mrad,thickness_nm,site,channel,dynamic,dechannelled,total,dynamic_conv,dechannelled_conv,total_conv
```

`dynamic` / `dechannelled` / `total` se guardan por separado, de modo que **la contribución de la componente descanalizada puede evaluarse a posteriori**. Las columnas `*_conv` solo aparecen cuando la dispersión angular está activada y contienen las curvas convolucionadas: el archivo lleva así tanto el resultado bruto reproducible como el que se compara con un experimento. Los valores son brutos (por electrón incidente) y no pasan por la normalización de visualización; el separador decimal es siempre un punto.

---

## Alcance y limitaciones conocidas

«Se puede calcular» y «está verificado cuantitativamente» son cosas distintas. Esta sección indica lo segundo.

### Sin una exactitud ±% general: tres cosas que hay que separar

ReciPro **no** ofrece deliberadamente una exactitud general del tipo «ocupaciones de sitio con ±N %». La revisión de Jones (2002) tampoco recoge ningún error de ocupación universal, y las cifras publicadas de ese tipo pertenecen a un sistema medido con un procedimiento: no son una propiedad del método y menos aún de este simulador.

Al juzgar un resultado, mantenga separadas tres cosas distintas.

**Precisión** : cuán reproducible es el número — estadística de recuento, la barra de error que devuelve una regresión, la dispersión entre repeticiones. Un residuo de ajuste pequeño, o un coeficiente de correlación próximo a 1, no demuestra por sí solo que el modelo sea correcto. En el caso que discute Jones, añadir una constante libre al ajuste mejoró su precisión sin demostrar una mejor exactitud.

**Sesgo del modelo** : el error sistemático del propio cálculo directo — la falta de correlación de sitio del término descanalizado, la aproximación de factor de forma local, la ausencia de distribución de espesor y de curvatura (todo ello más abajo). La física que falta no se reduce por acumular más cuentas ni por añadir más puntos de barrido. (Ampliar la base es otra cosa: eso reduce el error **numérico** de truncamiento, que el [diagnóstico de la base](#diagnóstico-de-la-base) informa por separado.)

**Comprobaciones independientes** : acuerdo con algo que no comparte los mismos supuestos, y hay dos niveles. La comparación con una **implementación** formulada de manera independiente (código contra código) verifica la formulación y la programación; eso es lo que se ha hecho aquí, para un sistema. La comparación con el **experimento**, que es la que contrasta la física con la realidad, no se ha hecho.

### Rango verificado cuantitativamente

**β-AlCo [001] a 250 keV, canales Al-K / Co-K / Co-L**, y nada más. Comparado con un cálculo multicapa con fonones congelados (py_multislice) cuya formulación dinámica es completamente independiente:

- **Sitio Al (columna ligera)** : residuo RMS respecto a la modulación ICP ≤3,2 % a todos los espesores, ≤0,6 % para $t \ge 10$ nm
- **Sitio Co (columna pesada)** : ≤3 % para $t \le 4$ nm, pero **6–17 % para $t \gtrsim 10$ nm**

Cualquier otro sistema, elemento, capa o tensión es «calculable» pero no «verificado cuantitativamente».

**No se ha realizado ninguna comparación con datos experimentales.** La comparación anterior es entre códigos, en el intervalo $t$ = 2–30 nm. El valor de 10–19 puntos citado en la sección siguiente es un *diagnóstico* para aislar la causa de la discrepancia: no es una corrección que aplique el simulador, y la concordancia obtenida tras aplicarla no se reivindica como verificación.

### Error sistemático conocido: el término descanalizado no tiene correlación de sitio

El término descanalizado de la v1 es una constante independiente de la orientación, por lo que su único efecto sobre el ICP es acercarlo a 1. En realidad, parte de los electrones dispersados térmicamente vuelve a canalizarse en las columnas y, al ser dispersores fuertes, regresa **preferentemente a las columnas pesadas**. En la comparación anterior, la magnitud efectiva de esta contribución estaba **subestimada en 10–19 puntos en las columnas pesadas**.

→ **Para sitios ligeros o poco dispersantes, o para $t \lesssim 5$ nm, el acuerdo con una implementación independiente es del 1–3 %. Para columnas pesadas con $t \gtrsim 10$ nm hay un error sistemático del 6–17 % de la modulación ICP.** Un modelo de reinyección con correlación de sitio queda aplazado a la v1.1 o posterior.

### No incluido en el modelo directo

**Una convolución con el ensanchamiento angular por sí sola no reproducirá un experimento.** No se incluye nada de lo siguiente.

- **Distribución de espesor** y **flexión** de la muestra
- **Autoabsorción** de rayos X
- **Eficiencia y ángulo sólido del detector**
- **Fondo** (radiación de frenado, líneas solapadas)

La **dispersión angular del haz incidente** (semiángulo de convergencia, deriva) *sí* está modelada —véase **Dispersión angular** en el cuadro Cálculo—, pero convolucionar con ella no sustituye a ninguno de los puntos anteriores.

### Líneas de baja energía: donde la aproximación local es más débil {#local-approximation}

La matriz de ionización de la v1 es función de un único vector $G = \mathbf{g}_h - \mathbf{g}_g$ (la aproximación de factor de forma local). ICSC indica que esto es razonable para capas internas fuertemente ligadas cuya emisión característica está **por encima de unos 3–4 keV** (Oxley & Allen 2003, p. 941).

⚠ **Esa cifra es una guía empírica y dependiente del modelo, no un corte estricto, y ReciPro no la usa para rechazar nada.** Las líneas por debajo se calculan con normalidad, y a menudo son las de interés: Al-K está en 1,49 keV y Co-L en 0,79 keV, y ambas pertenecen al conjunto β-AlCo empleado en la comparación entre códigos de más arriba.

Lo que marca esa cifra es dónde la reducción a un **único** vector $G$ empieza a resultar insuficiente. El suceso de ionización no ocurre sobre el núcleo: su probabilidad es máxima a una distancia finita del núcleo, y esa distancia crece a medida que baja la energía necesaria. Repare en lo que la aproximación conserva y lo que no: $F_c(|G|/2)$ depende del momento, así que **sí** se conserva un alcance de interacción finito; lo que se pierde es la dependencia separada de las dos transferencias de momento, es decir, la estructura no local de la MDFF completa. Al crecer la deslocalización, es esa estructura omitida la que empieza a importar.

La energía de la línea por sí sola no puede certificar un resultado: influyen la extensión espacial de la capa, la orientación, el espesor y los vectores recíprocos que la base realmente requiere. Trate 3–4 keV como una señal para mirar con más cuidado, no como una marca de aprobado. Cuando pueda elegir, comparar líneas de **energía parecida** tiende a hacer más comparable el sesgo de deslocalización de ambas; Jones (2002) recomienda exactamente eso como primer paso práctico, y como segundo, preferir una fila sistemática a un eje de zona, que es la geometría que calcula la v1 (un eje de zona canaliza con más fuerza, pero necesita una corrección de deslocalización mayor).

⚠ Las energías de emisión bajas son además las más afectadas por la **autoabsorción de rayos X**, aunque cuánto depende de la composición de la muestra y de sus bordes de absorción, del recorrido y del ángulo de salida, no solo de la energía emitida. Es una fuente de error **distinta**, no modelada en absoluto (véase [Magnitud de salida](#magnitud-de-salida) más arriba), y distorsiona la comparación con un experimento con independencia de lo que haga la aproximación local.

### Supuestos del modelo

- **Solo aproximación de trazador** : la superposición lineal de respuestas de sitio solo vale en el límite diluido en que el dopante no perturba el campo de ondas elástico. La VCA a concentración finita queda fuera del alcance de la v1
- **Aproximación de factor de forma local** : $\mu$ es función únicamente de $G = \mathbf{g}_h - \mathbf{g}_g$, no la MDFF de dos momentos (Modelo A de OAR 1999). La aproximación es más débil para capas K de elementos ligeros y bordes de baja energía — véase [arriba](#local-approximation)
- **Vacantes, no fotones de rayos X** : no se aplican el rendimiento de fluorescencia ni la ramificación de líneas
- **La cota inferior de la tensión de aceleración es 80 kV** : es la tensión más baja a la que puede garantizarse $s = 16\ \text{Å}^{-1}$, no un umbral de rechazo

---

## Véase también

- [Simulador de difracción (visión general)](index.md)
- [Simulación CBED](3-cbed-simulation.md)
- [Cálculo dinámico (núcleo común)](../appendix/a3-bloch-wave/calculation.md)
- [Simulación STEM](../9-hrtem-stem-simulator/2-stem-simulation.md) — STEM-EDX, que usa la misma base de datos de ionización
- [Interacción del haz](../3-beam-interaction.md) — datos de secciones eficaces y bordes de absorción

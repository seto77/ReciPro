# Apéndice A1.2. Sistema de coordenadas para la simulación de difracción

<!-- 260526Cl: 図(Coordinates4-5)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

La función **Simulador de difracción** simula el patrón de difracción registrado en un detector. El detector es un plano finito de píxeles situado a una distancia fija de la muestra y puede estar inclinado respecto al haz incidente. Para reproducir esto con precisión se necesita la relación geométrica entre el detector y la muestra, junto con el tamaño de píxel y el número de píxeles del detector. Para el sistema de coordenadas básico (de orientación), véase [A1.1. Sistema de coordenadas básico y orientación del cristal](1-orientation.md).

!!! note "Z y Y difieren del sistema de orientación"
    Dentro del sistema de coordenadas del detector, <span class="rp-steel">$Z$</span> es paralelo al haz e <span class="rp-steel">$Y$</span> apunta hacia abajo. Esto difiere del sistema de coordenadas de orientación, en el que el haz va a lo largo de <span class="rp-blue">$-Z$</span> e <span class="rp-green">$Y$</span> apunta hacia arriba. El sistema del detector sigue la convención habitual de imagen/detector (origen en la esquina superior izquierda, <span class="rp-steel">$Y$</span> creciente hacia abajo).

## Antes de la rotación (detector normal al haz)

![Sistema de coordenadas del detector con el detector normal al haz](../../../assets/references/Coordinates4.png){width=500px}

Se definen tres sistemas de coordenadas:

- <span class="rp-steel">**Coordenadas reales** ($X$, $Y$, $Z$)</span> : coordenadas cartesianas 3D en mm, con la <span class="rp-steel">**muestra**</span> como origen. <span class="rp-steel">$Z$</span> es paralelo al haz; visto a lo largo de <span class="rp-steel">$Z$</span>, <span class="rp-steel">$X$</span> apunta a la derecha e <span class="rp-steel">$Y$</span> apunta hacia abajo. Cuando el detector es normal al haz, <span class="rp-steel">$X$ / $Y$</span> son paralelos a <span class="rp-brown">$X'$ / $Y'$</span>.
- <span class="rp-brown">**Coordenadas del detector** ($X'$, $Y'$)</span> : coordenadas 2D en mm sobre el plano del detector, con el <span class="rp-brown">**foot**</span> como origen. <span class="rp-brown">$X'$ / $Y'$</span> apuntan a la derecha / abajo en el detector y son paralelos a <span class="rp-cyan">$X''$ / $Y''$</span>.
- <span class="rp-cyan">**Coordenadas de píxel** ($X''$, $Y''$)</span> : coordenadas 2D en unidades de píxel, con la <span class="rp-cyan">**esquina superior izquierda**</span> del detector como origen, siguiendo las filas y columnas de píxeles del detector.

Cuando el detector es perpendicular al haz, el <span class="rp-brown">**foot**</span> y el <span class="rp-red">**direct spot**</span> coinciden, y <span class="rp-red">**Camera length 1**</span> es igual a <span class="rp-brown">**Camera length 2**</span>.

## Después de la rotación (detector inclinado)

![Sistema de coordenadas del detector con un detector inclinado](../../../assets/references/Coordinates5.png){width=500px}

La inclinación del detector se describe mediante dos parámetros:

| Parámetro | Descripción |
|-----------|-------------|
| <span class="rp-grass">$\varphi$</span> | Dirección del <span class="rp-grass">eje de rotación</span> — su ángulo respecto al eje <span class="rp-steel">$X$</span>, medido en el plano <span class="rp-steel">$XY$</span> (<span class="rp-steel">$Z$</span> = 0) |
| <span class="rp-grass">$\tau$</span> | Ángulo de rotación en torno a ese eje (regla de la mano derecha) |

Una vez inclinado el detector:

- El <span class="rp-red">**direct spot**</span> y el <span class="rp-brown">**foot**</span> ya no coinciden.
- <span class="rp-red">**Camera length 1** ($C_1$)</span> = distancia desde la <span class="rp-steel">muestra</span> hasta el <span class="rp-red">direct spot</span>.
- <span class="rp-brown">**Camera length 2** ($C_2$)</span> = distancia desde la <span class="rp-steel">muestra</span> hasta el <span class="rp-brown">foot</span>.
- El origen de las <span class="rp-brown">**Coordenadas del detector**</span> permanece en el <span class="rp-brown">**foot**</span>; el origen de las <span class="rp-cyan">**Coordenadas de píxel**</span> permanece en la <span class="rp-cyan">**esquina superior izquierda**</span>.
- Las direcciones <span class="rp-steel">$X$ / $Y$</span> ya no coinciden con <span class="rp-brown">$X'$ / $Y'$</span>.

## Glosario de parámetros

| Término | Definición |
|------|------------|
| <span class="rp-steel">**Muestra (Sample)**</span> | El material que dispersa el haz incidente; el origen de las coordenadas reales |
| <span class="rp-steel">**Coordenadas reales** ($X$, $Y$, $Z$)</span> | Coordenadas 3D (mm) del montaje experimental; origen en la muestra, <span class="rp-steel">$Z$</span> siempre paralelo al haz |
| <span class="rp-red">**Direct spot**</span> | Intersección del haz incidente con el detector |
| <span class="rp-brown">**Foot**</span> | El pie de la perpendicular desde la muestra al plano del detector; origen de las coordenadas del detector. Coincide con el direct spot solo cuando el detector es normal al haz. En el modo de imagen superpuesta, la posición del foot se fija en coordenadas de píxel |
| <span class="rp-brown">**Coordenadas del detector** ($X'$, $Y'$)</span> | Coordenadas 2D (mm) sobre el plano del detector; origen en el foot |
| <span class="rp-cyan">**Coordenadas de píxel** ($X''$, $Y''$)</span> | Coordenadas 2D (píxeles) sobre el plano del detector; origen en la esquina superior izquierda |
| <span class="rp-red">**Camera length 1** ($C_1$)</span> | Distancia desde la muestra hasta el direct spot (mm) |
| <span class="rp-brown">**Camera length 2** ($C_2$)</span> | Distancia desde la muestra hasta el foot (mm) |
| **Pixel size** | Longitud del lado de un píxel (cuadrado) (mm); solo se admiten píxeles cuadrados |
| **Detector width / height** | Número de píxeles en horizontal / vertical |

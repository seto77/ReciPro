# Apéndice A1.1. Sistema de coordenadas básico y orientación del cristal

<!-- 260526Cl: 図(Coordinates1-3)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

Esta página define el **sistema de coordenadas básico (de orientación)** de ReciPro, que se utiliza en todos los lugares donde interviene una rotación del cristal (Ventana principal, Visor de estructura, Estereograma, Geometría de rotación y simulación de difracción), junto con la forma en que se expresan la orientación inicial de un cristal y la rotación mediante ángulos de Euler. El sistema independiente que se usa para situar el detector en el **Simulador de difracción** se describe en [A1.2. Sistema de coordenadas para la simulación de difracción](2-diffraction.md).

---

## Definición de la orientación

ReciPro emplea un **sistema de coordenadas dextrógiro** fijado al monitor:

| Eje | Dirección |
|------|-----------|
| <span class="rp-red">$X$</span> | A la derecha del monitor |
| <span class="rp-green">$Y$</span> | Hacia arriba en el monitor |
| <span class="rp-blue">$Z$</span> | Perpendicular hacia fuera del monitor, hacia el observador |

![Ejes de coordenadas de ReciPro mostrados en el monitor](../../../assets/references/Coordinates1.png){width=400px}

La **dirección del haz** corresponde a la dirección de observación (mirando hacia dentro del monitor), es decir, el eje <span class="rp-blue">$-Z$</span>.

La mayoría de las operaciones en ReciPro implican únicamente *direcciones* (expresadas como matrices de rotación 3×3) y no requieren un origen explícito. La única excepción es la función **Simulador de difracción**, que necesita un origen explícito — véase [A1.2. Sistema de coordenadas para la simulación de difracción](2-diffraction.md).

## Dirección inicial del cristal

La orientación inicial (en el primer inicio, o tras **Restablecer rotación**) se define como sigue:

1. El eje <span class="rp-blue">$c$</span> está alineado con el eje <span class="rp-blue">$Z$</span>.
2. El eje <span class="rp-green">$b$</span> se sitúa en el plano <span class="rp-green">$Y$</span><span class="rp-blue">$Z$</span>, próximo al eje <span class="rp-green">$Y$</span>.
3. El eje <span class="rp-red">$a$</span> queda entonces fijado por los ejes <span class="rp-green">$b$</span> y <span class="rp-blue">$c$</span> (regla de la mano derecha).

![Orientación inicial: los ejes a / b / c del cristal respecto a X / Y / Z, con el haz incidente a lo largo de −Z](../../../assets/references/Coordinates2.png){width=300px}

De forma equivalente:

- La dirección que sale del monitor (hacia el observador) es el eje de zona **[001]**.
- La dirección hacia la derecha en el monitor es la normal del plano **(100)**.

> **Nota:** El eje <span class="rp-blue">$c$</span> (= [001]) siempre coincide con <span class="rp-blue">$Z$</span>, pero en algunos sistemas cristalinos los ejes <span class="rp-red">$a$</span> y <span class="rp-green">$b$</span> **no** coinciden necesariamente con <span class="rp-red">$X$</span> e <span class="rp-green">$Y$</span>.

## Ángulos de Euler

La orientación del cristal se expresa con tres ángulos de Euler <span class="rp-olive">$\Phi$</span>, <span class="rp-cyan">$\theta$</span>, <span class="rp-magenta">$\Psi$</span>, aplicados en el orden <span class="rp-blue">$Z$</span>–<span class="rp-red">$X$</span>–<span class="rp-blue">$Z$</span> (<span class="rp-magenta">$\Psi$</span>, luego <span class="rp-cyan">$\theta$</span>, luego <span class="rp-olive">$\Phi$</span>). Cuando los tres ángulos son cero, los ejes de rotación correspondientes son:

| Ángulo | Eje (cuando todos los ángulos = 0) | Rango |
|-------|----------------------------|------|
| <span class="rp-olive">$\Phi$</span> | <span class="rp-blue">$Z$</span> | 1.º (el más alto) |
| <span class="rp-cyan">$\theta$</span> | <span class="rp-red">$X$</span> | 2.º (intermedio) |
| <span class="rp-magenta">$\Psi$</span> | <span class="rp-blue">$Z$</span> | 3.º (el más bajo) |

![Ejes de rotación de los ángulos de Euler — Φ (amarillo), θ (cian), Ψ (magenta) — a 0° (arriba) y a 15° (abajo)](../../../assets/references/Coordinates3.png){width=400px}

Los tres ángulos forman una **jerarquía**: <span class="rp-olive">$\Phi$</span> es la rotación de mayor rango, seguida de <span class="rp-cyan">$\theta$</span>, y luego <span class="rp-magenta">$\Psi$</span>. La dirección de un eje de menor rango depende del estado de las rotaciones de mayor rango. Por ejemplo, cuando <span class="rp-olive">$\Phi$</span> = <span class="rp-cyan">$\theta$</span> = <span class="rp-magenta">$\Psi$</span> = 15°, el eje <span class="rp-olive">$\Phi$</span> sigue coincidiendo con <span class="rp-blue">$Z$</span>, pero los ejes <span class="rp-cyan">$\theta$</span> y <span class="rp-magenta">$\Psi$</span> en general no coinciden con ninguno de los ejes <span class="rp-red">$X$</span>, <span class="rp-green">$Y$</span> o <span class="rp-blue">$Z$</span>.

> La ventana **Geometría de rotación** puede reexpresar esta orientación en una convención de ángulos de Euler arbitraria y específica del experimento (p. ej., para ajustarse a un goniómetro de laboratorio). Véase [4. Geometría de rotación](../../4-rotation-geometry.md).

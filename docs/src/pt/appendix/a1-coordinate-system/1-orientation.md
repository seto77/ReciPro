# Apêndice A1.1. Sistema de coordenadas básico e orientação do cristal

<!-- 260526Cl: 図(Coordinates1-3)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

Esta página define o **sistema de coordenadas básico (de orientação)** do ReciPro, usado em todos os lugares onde há rotação do cristal envolvida (Janela principal, Visualizador de estrutura, Estereonete, Geometria de rotação e simulação de difração), juntamente com a maneira como a orientação inicial de um cristal e a rotação por ângulos de Euler são expressas. O sistema separado usado para posicionar o detector no **Simulador de difração** é descrito em [A1.2. Sistema de coordenadas para simulação de difração](2-diffraction.md).

---

## Definição da orientação

O ReciPro usa um **sistema de coordenadas destro** fixado ao monitor:

| Eixo | Direção |
|------|-----------|
| <span class="rp-red">$X$</span> | À direita do monitor |
| <span class="rp-green">$Y$</span> | Para cima no monitor |
| <span class="rp-blue">$Z$</span> | Verticalmente para fora do monitor, em direção ao observador |

![Eixos de coordenadas do ReciPro mostrados no monitor](../../../assets/references/Coordinates1.png){width=400px}

A **direção do feixe** corresponde à direção de visualização (olhando para dentro do monitor), ou seja, o eixo <span class="rp-blue">$-Z$</span>.

A maioria das operações no ReciPro envolve apenas *direções* (expressas como matrizes de rotação 3×3) e não requer uma origem explícita. A única exceção é a função **Simulador de difração**, que precisa de uma origem explícita — consulte [A1.2. Sistema de coordenadas para simulação de difração](2-diffraction.md).

## Direção inicial do cristal

A orientação inicial (no primeiro início, ou após **Redefinir rotação**) é definida como:

1. O eixo <span class="rp-blue">$c$</span> está alinhado com o eixo <span class="rp-blue">$Z$</span>.
2. O eixo <span class="rp-green">$b$</span> está no plano <span class="rp-green">$Y$</span><span class="rp-blue">$Z$</span>, próximo ao eixo <span class="rp-green">$Y$</span>.
3. O eixo <span class="rp-red">$a$</span> é então fixado pelos eixos <span class="rp-green">$b$</span> e <span class="rp-blue">$c$</span> (regra da mão direita).

![Orientação inicial: os eixos a / b / c do cristal em relação a X / Y / Z, com o feixe incidente ao longo de −Z](../../../assets/references/Coordinates2.png){width=300px}

De forma equivalente:

- A direção para fora do monitor (em direção ao observador) é o eixo de zona **[001]**.
- A direção para a direita no monitor é a normal do plano **(100)**.

> **Nota:** O eixo <span class="rp-blue">$c$</span> (= [001]) sempre coincide com <span class="rp-blue">$Z$</span>, mas em alguns sistemas cristalinos os eixos <span class="rp-red">$a$</span> e <span class="rp-green">$b$</span> **não** coincidem necessariamente com <span class="rp-red">$X$</span> e <span class="rp-green">$Y$</span>.

## Ângulos de Euler

A orientação do cristal é expressa com três ângulos de Euler <span class="rp-olive">$\Phi$</span>, <span class="rp-cyan">$\theta$</span>, <span class="rp-magenta">$\Psi$</span>, aplicados na ordem <span class="rp-blue">$Z$</span>–<span class="rp-red">$X$</span>–<span class="rp-blue">$Z$</span> (<span class="rp-magenta">$\Psi$</span>, depois <span class="rp-cyan">$\theta$</span>, depois <span class="rp-olive">$\Phi$</span>). Quando os três ângulos são zero, os eixos de rotação correspondentes são:

| Ângulo | Eixo (quando todos os ângulos = 0) | Posto |
|-------|----------------------------|------|
| <span class="rp-olive">$\Phi$</span> | <span class="rp-blue">$Z$</span> | 1º (mais alto) |
| <span class="rp-cyan">$\theta$</span> | <span class="rp-red">$X$</span> | 2º (intermediário) |
| <span class="rp-magenta">$\Psi$</span> | <span class="rp-blue">$Z$</span> | 3º (mais baixo) |

![Eixos de rotação dos ângulos de Euler — Φ (amarelo), θ (ciano), Ψ (magenta) — mostrados a 0° (em cima) e a 15° (embaixo)](../../../assets/references/Coordinates3.png){width=400px}

Os três ângulos formam uma **hierarquia**: <span class="rp-olive">$\Phi$</span> é a rotação mais alta, seguida por <span class="rp-cyan">$\theta$</span>, depois <span class="rp-magenta">$\Psi$</span>. A direção de um eixo inferior depende do estado das rotações superiores. Por exemplo, quando <span class="rp-olive">$\Phi$</span> = <span class="rp-cyan">$\theta$</span> = <span class="rp-magenta">$\Psi$</span> = 15°, o eixo <span class="rp-olive">$\Phi$</span> ainda coincide com <span class="rp-blue">$Z$</span>, mas os eixos <span class="rp-cyan">$\theta$</span> e <span class="rp-magenta">$\Psi$</span> em geral não se alinham com nenhum dos eixos <span class="rp-red">$X$</span>, <span class="rp-green">$Y$</span> ou <span class="rp-blue">$Z$</span>.

> A janela **Geometria de rotação** pode reexpressar essa orientação em uma convenção de ângulos de Euler arbitrária e específica do experimento (por exemplo, para corresponder a um goniômetro de laboratório). Consulte [4. Geometria de rotação](../../4-rotation-geometry.md).

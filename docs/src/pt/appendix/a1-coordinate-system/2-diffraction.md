# Apêndice A1.2. Sistema de coordenadas para a simulação de difração

<!-- 260526Cl: 図(Coordinates4-5)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

A função **Simulador de difração** simula o padrão de difração registrado em um detector. O detector é um plano finito de pixels posicionado a uma distância fixa da amostra e pode estar inclinado em relação ao feixe incidente. Para reproduzir isso com precisão, são necessárias a relação geométrica entre o detector e a amostra, bem como o tamanho do pixel e a quantidade de pixels do detector. Para o sistema de coordenadas básico (de orientação), veja [A1.1. Sistema de coordenadas básico e orientação do cristal](1-orientation.md).

!!! note "Z e Y diferem do sistema de orientação"
    No sistema de coordenadas do detector, <span class="rp-steel">$Z$</span> é paralelo ao feixe e <span class="rp-steel">$Y$</span> aponta para baixo. Isso difere do sistema de coordenadas de orientação, em que o feixe segue ao longo de <span class="rp-blue">$-Z$</span> e <span class="rp-green">$Y$</span> aponta para cima. O sistema do detector segue a convenção usual de imagem/detector (origem no canto superior esquerdo, <span class="rp-steel">$Y$</span> aumentando para baixo).

## Antes da rotação (detector perpendicular ao feixe)

![Sistema de coordenadas do detector com o detector perpendicular ao feixe](../../../assets/references/Coordinates4.png){width=500px}

São definidos três sistemas de coordenadas:

- <span class="rp-steel">**Coordenadas reais** ($X$, $Y$, $Z$)</span> : coordenadas cartesianas 3D em mm, com a <span class="rp-steel">**amostra**</span> como origem. <span class="rp-steel">$Z$</span> é paralelo ao feixe; visto ao longo de <span class="rp-steel">$Z$</span>, <span class="rp-steel">$X$</span> aponta para a direita e <span class="rp-steel">$Y$</span> aponta para baixo. Quando o detector está perpendicular ao feixe, <span class="rp-steel">$X$ / $Y$</span> são paralelos a <span class="rp-brown">$X'$ / $Y'$</span>.
- <span class="rp-brown">**Coordenadas do detector** ($X'$, $Y'$)</span> : coordenadas 2D em mm no plano do detector, com o <span class="rp-brown">**foot**</span> como origem. <span class="rp-brown">$X'$ / $Y'$</span> apontam para a direita / para baixo no detector e são paralelos a <span class="rp-cyan">$X''$ / $Y''$</span>.
- <span class="rp-cyan">**Coordenadas de pixel** ($X''$, $Y''$)</span> : coordenadas 2D em unidades de pixel, com o <span class="rp-cyan">**canto superior esquerdo**</span> do detector como origem, seguindo as linhas e colunas de pixels do detector.

Quando o detector está perpendicular ao feixe, o <span class="rp-brown">**foot**</span> e o <span class="rp-red">**direct spot**</span> coincidem, e <span class="rp-red">**Camera length 1**</span> é igual a <span class="rp-brown">**Camera length 2**</span>.

## Após a rotação (detector inclinado)

![Sistema de coordenadas do detector com um detector inclinado](../../../assets/references/Coordinates5.png){width=500px}

A inclinação do detector é descrita por dois parâmetros:

| Parâmetro | Descrição |
|-----------|-------------|
| <span class="rp-grass">$\varphi$</span> | Direção do <span class="rp-grass">eixo de rotação</span> — seu ângulo em relação ao eixo <span class="rp-steel">$X$</span>, medido no plano <span class="rp-steel">$XY$</span> (<span class="rp-steel">$Z$</span> = 0) |
| <span class="rp-grass">$\tau$</span> | Ângulo de rotação em torno desse eixo (parafuso de rosca direita) |

Uma vez que o detector esteja inclinado:

- O <span class="rp-red">**direct spot**</span> e o <span class="rp-brown">**foot**</span> não coincidem mais.
- <span class="rp-red">**Camera length 1** ($C_1$)</span> = distância da <span class="rp-steel">amostra</span> ao <span class="rp-red">direct spot</span>.
- <span class="rp-brown">**Camera length 2** ($C_2$)</span> = distância da <span class="rp-steel">amostra</span> ao <span class="rp-brown">foot</span>.
- A origem das <span class="rp-brown">**Coordenadas do detector**</span> permanece no <span class="rp-brown">**foot**</span>; a origem das <span class="rp-cyan">**Coordenadas de pixel**</span> permanece no <span class="rp-cyan">**canto superior esquerdo**</span>.
- As direções <span class="rp-steel">$X$ / $Y$</span> não coincidem mais com <span class="rp-brown">$X'$ / $Y'$</span>.

## Glossário de parâmetros

| Termo | Definição |
|------|------------|
| <span class="rp-steel">**Amostra (Sample)**</span> | O material que espalha o feixe incidente; a origem das coordenadas reais |
| <span class="rp-steel">**Coordenadas reais** ($X$, $Y$, $Z$)</span> | Coordenadas 3D (mm) do arranjo experimental; origem na amostra, <span class="rp-steel">$Z$</span> sempre paralelo ao feixe |
| <span class="rp-red">**Direct spot**</span> | Interseção do feixe incidente com o detector |
| <span class="rp-brown">**Foot**</span> | O pé da perpendicular da amostra ao plano do detector; origem das coordenadas do detector. Coincide com o direct spot somente quando o detector está perpendicular ao feixe. No modo de imagem sobreposta, defina a posição do foot em coordenadas de pixel |
| <span class="rp-brown">**Coordenadas do detector** ($X'$, $Y'$)</span> | Coordenadas 2D (mm) no plano do detector; origem no foot |
| <span class="rp-cyan">**Coordenadas de pixel** ($X''$, $Y''$)</span> | Coordenadas 2D (pixels) no plano do detector; origem no canto superior esquerdo |
| <span class="rp-red">**Camera length 1** ($C_1$)</span> | Distância da amostra ao direct spot (mm) |
| <span class="rp-brown">**Camera length 2** ($C_2$)</span> | Distância da amostra ao foot (mm) |
| **Pixel size** | Comprimento do lado de um pixel (quadrado) (mm); apenas pixels quadrados são suportados |
| **Detector width / height** | Número de pixels na horizontal / vertical |

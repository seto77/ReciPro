# Cálculo CBED

CBED (difração de elétrons por feixe convergente) aplica o [núcleo dinâmico](calculation.md) a muitas direções do feixe incidente e, em seguida, dispõe os resultados em discos de difração. SAED tem uma única direção de incidência; CBED trata cada ponto dentro da abertura objetiva como uma **onda plana incidente parcial** e resolve o problema de ondas de Bloch para cada um deles.

---

## Representação do feixe convergente

Na superfície de entrada, a sonda convergente pode ser escrita como uma soma de ondas planas usando a posição da sonda $\mathbf R_0$, a fase da lente $\chi(\mathbf K)$ e a função de abertura $A(\mathbf K)$:

$$\psi_{\mathrm{in}}(\mathbf R,0)=\sum_{\mathbf K\in\mathrm{aperture}} A(\mathbf K)\,
\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)\,
\exp[-i\chi(\mathbf K)]\,
\exp(2\pi i\,\mathbf K\cdot\mathbf R)$$

Aqui $\mathbf K$ é a componente do vetor de onda incidente paralela à superfície da amostra. Para uma abertura circular ideal com semiângulo de convergência $\alpha$ e comprimento de onda do elétron $\lambda$,

$$A(\mathbf K)=
\begin{cases}
1 & (|\mathbf K|\leq \sin\alpha/\lambda)\\
0 & (|\mathbf K|> \sin\alpha/\lambda)
\end{cases}$$

Uma fase de lente representativa, usando a desfocagem $\Delta f$ e a aberração esférica $C_s$, é

$$\chi(\mathbf K)=\pi\lambda|\mathbf K|^2\Delta f+\frac{\pi}{2}C_s\lambda^3|\mathbf K|^4+\cdots$$

No ReciPro essa expressão é controlada pelas configurações de aberração, abertura e ângulo de convergência.

---

## Cálculo dinâmico para cada direção

No CBED, cada $\mathbf K$ dentro da abertura é tratado como um feixe incidente paralelo. O fluxo de trabalho conceitual é:

1. Determine o vetor de onda de referência refratado $\mathbf k_0(\mathbf K)$ a partir de $\mathbf K$ e da normal à superfície da amostra.
2. Selecione os feixes refletidos usando a grandeza de classificação $R_{\mathbf g}=|\mathbf g|Q_{\mathbf g}^2$.
3. Construa a matriz de estrutura $\mathbf A$ e calcule os coeficientes de transmissão $T_{\mathbf g}(t;\mathbf K)$ na espessura $t$.

Este é o cálculo dos coeficientes de transmissão do [núcleo dinâmico](calculation.md), repetido para cada direção de incidência amostrada. Para uma série de espessuras, a autossolução para uma dada direção pode ser reutilizada e apenas os fatores de propagação precisam ser atualizados.

---

## Montagem dos discos de difração

Inserindo as ondas de saída de todas as direções $\mathbf K$ no plano de difração obtém-se a intensidade dentro do disco transmitido e dos discos difratados. Se $\mathbf Q$ é a coordenada do plano de difração, o CBED com média de posição ou as condições de baixa coerência podem ser aproximados como uma soma incoerente de intensidades:

$$I_{\mathrm{CBED}}(\mathbf Q)=
\sum_{\mathbf K\in\mathrm{aperture}}
\left|\psi_{\mathbf K}(\mathbf Q,t)\right|^2$$

Para modos do tipo LACBED, em que a coerência de fase sobre uma região mais ampla é importante, as amplitudes devem ser somadas primeiro e a intensidade tomada em seguida.

---

## O que o CBED mostra

O CBED torna visível a dependência da espessura da solução de ondas de Bloch como estrutura de intensidade dentro dos discos de difração.

- Alterar a espessura modifica as oscilações no interior dos discos, as linhas HOLZ e as franjas de Kossel-Möllenstedt.
- Alterar a orientação de incidência modifica quais reflexões são fortemente excitadas.
- Aumentar o ângulo de convergência alarga os discos e pode revelar sobreposições e informações das zonas de Laue de ordem superior.

O CBED é, portanto, a maneira mais direta de visualizar o resultado das ondas de Bloch como um padrão de discos no plano de difração. No ReciPro ele é mais bem compreendido como a combinação da discretização do feixe convergente, uma solução dinâmica por direção e o rearranjo em arranjos de discos.

---

## Parâmetros práticos

- **Número de feixes**: Condições fortes de eixo de zona e detalhes das linhas HOLZ exigem muitos feixes refletidos. Verifique como o interior dos discos muda à medida que o número máximo de ondas de Bloch é aumentado.
- **Amostragem angular**: Se a amostragem de $\mathbf K$ dentro da abertura for muito grosseira, a intensidade dos discos torna-se granulada. Ângulos de convergência maiores exigem amostragem mais fina.
- **Espessura**: As séries de espessuras se beneficiam do método de autovalores, pois uma autossolução pode ser reutilizada para muitas espessuras.
- **Coerência**: Distinga as condições em que uma soma incoerente de intensidades é suficiente daquelas em que é necessária uma soma coerente de amplitudes.

## Veja também

- [Cálculo dinâmico (núcleo comum)](calculation.md)
- [Apêndice A3. Difração dinâmica pelo método de ondas de Bloch](index.md)
- [7.4. Simulação CBED](../../7-diffraction-simulator/3-cbed-simulation.md)

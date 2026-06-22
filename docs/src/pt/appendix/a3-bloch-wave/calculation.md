# Cálculo dinâmico (núcleo comum)

Os simuladores de difração e de imagem do ReciPro compartilham um **núcleo dinâmico de espalhamento de ondas de Bloch (Bethe)** comum, descrito nesta página (potencial do cristal, termos de Debye–Waller e de absorção, o problema de autovalores, os coeficientes de transmissão e as intensidades). Os protocolos específicos de cada método se baseiam neste núcleo:

- [SAED de feixe paralelo](#parallel-beam-saed)
- [Formação da imagem HRTEM](hrtem.md)
- [CBED](cbed.md)
- [STEM](stem.md)
- [EBSD](ebsd.md)

Para a teoria subjacente (equação de Schrödinger, teorema de Bloch, equação dinâmica de Bethe, o problema de autovalores e as definições da esfera de Ewald), veja [Apêndice A3. Difração dinâmica pelo método de ondas de Bloch](index.md).

---

## Constantes

$$\gamma = \frac{m}{m_0} = 1 + \frac{e_0 E}{m_0 c^2}, \qquad \beta = \frac{v}{c} = \sqrt{1 - \left(\frac{m_0}{m}\right)^2} = \sqrt{1 - \gamma^{-2}}$$

- $\gamma$ : fator de correção relativística; $E$ : tensão de aceleração; $m_0$, $m$ : massa de repouso e massa relativística do elétron.
- $\Omega$ : volume da célula unitária.
- $k_{vac}$ : número de onda do elétron no vácuo.

---

## Potencial do cristal para o espalhamento elástico

O coeficiente de Fourier do potencial do cristal para o espalhamento elástico, somado sobre os átomos $k$ nas posições $\mathbf r_k$, é

$$U_{\mathbf g}^{C} = \gamma\,\frac{1}{\pi\Omega}\sum_k f_k(\mathbf g)\,\exp\!\left[2\pi i\,\mathbf g\cdot\mathbf r_k\right]T_k(\mathbf g, M_k)$$

onde o **fator de espalhamento atômico** usa uma parametrização gaussiana $(a_i, b_i)$,

$$f_k(\mathbf g) = \sum_i a_i\exp\!\left[-b_i\,\frac{|\mathbf g|^2}{4}\right]$$

e $T_k$ é o **fator de Debye–Waller (temperatura)**. Para um fator de temperatura isotrópico $M_k$,

$$T_k(\mathbf g, M_k) = \exp\!\left[-M_k\,\frac{|\mathbf g|^2}{4}\right]$$

e para um tensor anisotrópico de deslocamento atômico $\mathbf U$,

$$T_k(\mathbf g) = \exp\!\left[-2\pi\,\mathbf g^{t}\mathbf U\,\mathbf g\right]$$

com a forma quadrática

$$\mathbf g^{t}\mathbf U\,\mathbf g = \begin{pmatrix} g_x & g_y & g_z\end{pmatrix}\begin{pmatrix} U_{11} & U_{12} & U_{13}\\ U_{12} & U_{22} & U_{23}\\ U_{13} & U_{23} & U_{33}\end{pmatrix}\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = g_x^2 U_{11} + g_y^2 U_{22} + g_z^2 U_{33} + 2\!\left(g_x g_y U_{12} + g_y g_z U_{23} + g_x g_z U_{13}\right)$$

As componentes cartesianas de $\mathbf g$ são obtidas a partir dos vetores da base recíproca e dos índices de Miller:

$$\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = \begin{pmatrix} a_x^{*} & b_x^{*} & c_x^{*}\\ a_y^{*} & b_y^{*} & c_y^{*}\\ a_z^{*} & b_z^{*} & c_z^{*}\end{pmatrix}\begin{pmatrix} h\\ k\\ l\end{pmatrix} = \begin{pmatrix} h\,a_x^{*} + k\,b_x^{*} + l\,c_x^{*}\\ h\,a_y^{*} + k\,b_y^{*} + l\,c_y^{*}\\ h\,a_z^{*} + k\,b_z^{*} + l\,c_z^{*}\end{pmatrix}$$

!!! note
    Os valores $U_{\mathbf g}$ exibidos na tabela **Details** do simulador de difração são os valores brutos *antes* da aplicação do fator relativístico $\gamma$.

---

## Potencial de absorção (espalhamento térmico difuso)

O potencial imaginário (de absorção) que leva em conta o espalhamento térmico difuso (TDS) é

$$U'_{g,h} = \gamma\,\frac{1}{\pi\Omega}\sum_k f'_k(\mathbf g,\mathbf h)\,\exp\!\left[2\pi i(\mathbf g-\mathbf h)\cdot\mathbf r_k\right]T_k(\mathbf g-\mathbf h, M_k)$$

com o **fator de espalhamento de absorção**

$$f'_k(\mathbf g,\mathbf h) = \frac{2h}{\beta\, m_0\, c}\sum_i\sum_j a_i a_j\left[\frac{1}{b_i+b_j}\exp\!\left\{-\frac{b_i b_j}{b_i+b_j}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\} - \frac{1}{b_i+b_j+2M_k}\exp\!\left\{-\frac{b_i b_j - M_k^2}{b_i+b_j+2M_k}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\}\right]$$

Aqui $h$ no pré-fator $2h/(\beta m_0 c)$ é a **constante de Planck** (não um índice de feixe). Os coeficientes $U^{C}$ e $U'$ são os elementos da matriz de estrutura $\mathbf A$ no [Apêndice A3](index.md).

---

## Da solução de autovalores à intensidade difratada

A diagonalização da matriz de estrutura (veja o [Apêndice A3](index.md)) fornece os autovalores $\lambda^{(j)}$ e as amplitudes das ondas de Bloch $C_{\mathbf g}^{(j)}$. As amplitudes de onda na superfície de saída — os **coeficientes de transmissão** $T_{\mathbf g}$ — para a espessura da amostra $t$ são

$$\begin{pmatrix} T_0\\ T_g\\ T_h\\ \vdots\end{pmatrix}
= e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}
\begin{pmatrix} e^{\pi i P_0 t} & 0 & 0 & \cdots\\ 0 & e^{\pi i P_g t} & 0 & \cdots\\ 0 & 0 & e^{\pi i P_h t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} C_0^{(1)} & C_0^{(2)} & C_0^{(3)} & \cdots\\ C_g^{(1)} & C_g^{(2)} & C_g^{(3)} & \cdots\\ C_h^{(1)} & C_h^{(2)} & C_h^{(3)} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} e^{2\pi i\lambda^{(1)} t} & 0 & 0 & \cdots\\ 0 & e^{2\pi i\lambda^{(2)} t} & 0 & \cdots\\ 0 & 0 & e^{2\pi i\lambda^{(3)} t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} \alpha^{(1)}\\ \alpha^{(2)}\\ \alpha^{(3)}\\ \vdots\end{pmatrix}$$

ou, componente a componente,

$$T_{\mathbf g} = e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}\; e^{\pi i P_g t}\sum_j C_{\mathbf g}^{(j)}\,e^{2\pi i\lambda^{(j)} t}\,\alpha^{(j)}$$

- $\alpha^{(j)}$ : os coeficientes de ponderação (excitação) de cada onda de Bloch, fixados pela condição de contorno na superfície de entrada.
- $t$ : espessura da amostra.

A intensidade difratada do feixe $\mathbf g$ é então

$$I_{\mathbf g} = \left|T_{\mathbf g}\right|^2$$

---

## Cálculo de SAED de feixe paralelo { #parallel-beam-saed }

A SAED comum (difração de elétrons de área selecionada) é tratada como **difração de feixe paralelo** com uma única direção de incidência. Diferentemente do CBED, ela não varre muitos pontos $\mathbf K$ dentro de uma abertura convergente. A orientação atual do cristal e a tensão de aceleração definem um vetor de onda incidente $\mathbf k_0$, e o ReciPro avalia a posição e a intensidade de cada reflexão $\mathbf g$ para essa condição.

O cálculo pode ser organizado da seguinte forma.

1. Use a orientação do cristal, a tensão de aceleração, o comprimento de onda, o comprimento de câmera e a geometria do detector para definir o vetor de onda incidente no vácuo $\mathbf k_{vac}$ e o plano do detector.
2. Aplique a correção de refração a partir do potencial interno médio $U_0$ e obtenha o vetor de onda de referência do cristal $\mathbf k_0$.
3. Enumere os vetores candidatos da rede recíproca $\mathbf g$ e avalie sua distância à esfera de Ewald por meio de grandezas como $Q_g=|\mathbf k_0|^2-|\mathbf k_0+\mathbf g|^2$ e o erro de excitação $S_g$.
4. Calcule a intensidade de cada reflexão usando o modo de intensidade selecionado.
5. Projete a direção de $\mathbf k_0+\mathbf g$ sobre o plano do detector e desenhe-a como um ponto de difração.

O modo SAED do ReciPro oferece principalmente os seguintes modelos de intensidade.

| Modo | Cálculo | Uso típico |
|------|-------------|-------------|
| Apenas erro de excitação | Estima a intensidade apenas a partir de quão próxima a reflexão está da esfera de Ewald. Os fatores de estrutura não são usados. | Verificações rápidas de posições de pontos e da geometria do eixo de zona. |
| Cinemática + erro de excitação | Usa $\lvert F_{\mathbf g}\rvert^2$ junto com o amortecimento pelo erro de excitação. O espalhamento múltiplo não é incluído. | Amostras finas, difração fraca e verificações de regras de extinção. |
| Teoria dinâmica | Usa o núcleo de ondas de Bloch desta página para obter $T_{\mathbf g}(t)$ e define $I_{\mathbf g}=\lvert T_{\mathbf g}\rvert^2$. | Dependência da espessura, espalhamento múltiplo e reflexões fortes de difração de elétrons. |

Os modos de exibição dos pontos da rede recíproca, como seções transversais de esfera sólida e pontos gaussianos, controlam principalmente o perfil de desenho. No modo de teoria dinâmica, a intensidade física da reflexão é determinada pelo valor de onda de Bloch $|T_{\mathbf g}|^2$, e essa intensidade é então atribuída ao perfil de exibição escolhido.

A PED pode ser vista como a integração deste cálculo de SAED de feixe paralelo sobre as direções de precessão, enquanto o CBED pode ser visto como a disposição de muitas direções de incidência dentro dos discos de difração.

---

## Potencial interno médio e refração

Quando o elétron entra no cristal a partir do vácuo, o potencial interno médio $U_0$ altera ligeiramente o vetor de onda de referência dentro do cristal. A componente paralela à superfície é fixada pela condição de contorno, de modo que o vetor de onda no vácuo $\mathbf k_{vac}$ e o vetor de onda de referência do cristal $\mathbf k_0$ podem ser escritos como

$$|\mathbf k_0|^2 = k_{vac}^2 + U_0, \qquad \mathbf k_0 = \mathbf k_{vac} + x\,\hat{\mathbf n}$$

onde $x$ é a correção ao longo da normal à superfície. Ela é obtida de

$$x^2 + 2(\hat{\mathbf n}\cdot\mathbf k_{vac})x - U_0 = 0$$

Este $\mathbf k_0$ refratado é usado ao avaliar $P_g$, $Q_g$, os erros de excitação e a matriz de estrutura $\mathbf A$ na [página de visão geral](index.md). O potencial de absorção também possui uma componente $\mathbf g=\mathbf 0$, $U'_0$, que atua como uma atenuação média comum para as ondas que se propagam através do cristal.

---

## Seleção de feixes

O cálculo de ondas de Bloch não pode incluir infinitos vetores da rede recíproca, portanto o ReciPro seleciona um conjunto finito de feixes $\{\mathbf g\}$. A grandeza de ordenação é

$$R_{\mathbf g}=|\mathbf g|\,Q_{\mathbf g}^{\,2}$$

e os feixes com $R_{\mathbf g}$ menor são incluídos primeiro. Isso favorece feixes com vetores da rede recíproca curtos que também estão próximos da esfera de Ewald.

Em cálculos práticos, é importante verificar quanto a intensidade ou a imagem muda à medida que o número máximo de ondas de Bloch é aumentado. Condições de eixo de zona fortes e padrões CBED com detalhes de linhas HOLZ podem exigir várias centenas de feixes, enquanto condições fora do eixo de zona podem convergir com menos feixes.

---

## Escolha do solucionador

Depois que o conjunto finito de feixes é escolhido, o ReciPro usa principalmente duas maneiras equivalentes de obter os coeficientes de transmissão.

| Método | Característica | Uso típico |
|--------|---------|-------------|
| Método de autovalores | Diagonaliza a matriz de estrutura $\mathbf A$ e obtém os autovalores $\lambda^{(j)}$ e os autovetores $C_{\mathbf g}^{(j)}$. A dependência da espessura é então avaliada por meio de $e^{2\pi i\lambda^{(j)}t}$. | Séries de espessura, e cálculos de CBED e EBSD que varrem muitas profundidades ou energias |
| Método da exponencial de matriz | Avalia diretamente a matriz de espalhamento $\exp(2\pi i\mathbf A t)$ sem usar explicitamente uma decomposição em autovalores. | Cálculos STEM de espessura única e cálculos integrados por fatias |

Ambos os métodos resolvem a mesma equação de Bethe. Na implementação, o código escolhe entre o método de autovalores, o método da exponencial de matriz, rotinas gerenciadas .NET e a biblioteca nativa Eigen de acordo com o número de feixes, o vetor de espessuras e a disponibilidade da biblioteca nativa.

---

## Verificações de convergência

Para cálculos dinâmicos, verificar se a base é grande o suficiente é tão importante quanto a própria fórmula. Um diagnóstico útil é a variação relativa quando a contagem de feixes é aumentada de $N-\Delta N$ para $N$:

$$\Delta I_N=\frac{|I_N-I_{N-\Delta N}|}{I_N}$$

Para STEM, verifique isso junto com a configuração do ângulo do detector. Para CBED, inspecione os interiores dos discos e as linhas HOLZ. Para EBSD, compare também as larguras das bandas de Kikuchi e o fundo no master pattern. Isso conecta a convergência numérica com as características físicas visíveis no resultado simulado.

---

## Veja também

- [Apêndice A3. Difração dinâmica pelo método de ondas de Bloch](index.md)
- [7.2. SAED simulation](../../7-diffraction-simulator/1-saed-simulation.md)
- [7.4. CBED simulation](../../7-diffraction-simulator/3-cbed-simulation.md)
- [7. Simulador de difração](../../7-diffraction-simulator/index.md)

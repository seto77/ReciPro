# Cálculo STEM

O cálculo de imagens STEM parte da mesma representação de sonda convergente que o [CBED](cbed.md). A diferença está no observável: o CBED exibe a intensidade do disco no plano de difração, enquanto o STEM varre a posição da sonda e, em cada posição, integra a intensidade que entra no detector selecionado.

---

## Observável

Seja $\mathbf R_0$ a posição da sonda, $\mathbf Q$ a coordenada do plano de difração e $t$ a espessura da amostra. Se a função do detector $D(\mathbf Q)$ for igual a 1 dentro da faixa angular do detector e igual a 0 fora dela, a intensidade STEM elástica é

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf R_0)=
\int D(\mathbf Q)\,
\left|\psi(\mathbf Q,t;\mathbf R_0)\right|^2\,d\mathbf Q$$

BF, ABF, LAADF e HAADF correspondem a escolhas diferentes dos ângulos interno e externo em $D(\mathbf Q)$. Alterar o ângulo do detector STEM altera, portanto, a grandeza física que está sendo integrada; não se trata apenas de uma configuração de exibição.

---

## Aceleração por coeficientes de Fourier

Uma implementação direta resolveria o problema dinâmico novamente para cada posição de sonda varrida $\mathbf R_0$. A expressão da sonda convergente tem uma estrutura útil: a dependência de $\mathbf R_0$ entra como o fator de fase

$$\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)$$

Isso permite que o ReciPro calcule primeiro os coeficientes de Fourier bidimensionais da imagem, em vez de calcular $I_{\mathrm{STEM}}(\mathbf R_0)$ ponto a ponto. Conceitualmente,

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf q)=
\sum_{\mathbf g,\mathbf h}
F_{\mathbf g,\mathbf h}(t)\,
\delta(\mathbf q-\mathbf g+\mathbf h)$$

de modo que, uma vez conhecidos os coeficientes $F_{\mathbf g,\mathbf h}(t)$, a imagem de varredura completa pode ser reconstruída de forma eficiente por uma transformada de Fourier inversa.

Esta é a principal vantagem do STEM por ondas de Bloch para cristais perfeitos com células unitárias pequenas. Pode ser muito mais rápido do que repetir um cálculo multislice em cada posição da sonda.

---

## TDS e absorção selecionada pelo detector

No HAADF-STEM, a componente inelástica proveniente do espalhamento térmico difuso (TDS) é, muitas vezes, a principal fonte de contraste da imagem. O ReciPro trata o TDS como a quantidade de intensidade removida do canal elástico para uma faixa angular selecionada, representada por um potencial de absorção.

Para uma faixa angular do detector $\theta_1\leq\theta\leq\theta_2$, o fator de espalhamento de absorção selecionado pelo detector pode ser escrito conceitualmente como

$$f'_{\kappa}(\mathbf g;\theta_1,\theta_2)=
\int_{\theta_1}^{\theta_2}\sin\theta\,d\theta
\int_0^{2\pi}
\left|\Delta f_{e,\kappa}(\mathbf g,\theta,\phi)\right|^2\,d\phi$$

Escolher essa faixa de forma a corresponder a um detector BF, ADF ou HAADF avalia a contribuição de TDS que entra nesse detector.

A intensidade STEM de TDS é a integral em espessura da absorção selecionada pelo detector:

$$I_{\mathrm{STEM}}^{\mathrm{TDS}}(\mathbf R_0)=
\int_0^t
\langle\psi(z;\mathbf R_0)|\widehat W_{\mathrm{det}}|\psi(z;\mathbf R_0)\rangle\,dz$$

onde $\widehat W_{\mathrm{det}}$ representa o TDS selecionado pelo detector. Uma vez conhecidos os autovalores e autovetores das ondas de Bloch, essa integral em $z$ pode ser tratada analiticamente. Uma integração numérica por fatias também é possível, e o ReciPro utiliza a abordagem apropriada conforme o modo de cálculo.

---

## Absorção local e não local

O potencial de absorção pode ser tratado de duas maneiras principais.

| Forma | Significado | Característica |
|------|---------|---------|
| Aproximação local | Usa um potencial de absorção $U'(\mathbf r)$ que depende apenas da posição. | Geralmente eficaz e rápida para detectores ADF / HAADF amplos. |
| Forma não local | Usa $U'(\mathbf r,\mathbf r')$ ou elementos de matriz $U'_{\mathbf g,\mathbf h}$ que dependem de pares de ondas incidentes e emergentes. | Mais precisa para detectores estreitos, elementos pesados ou baixas tensões de aceleração, mas muito mais custosa. |

Na aproximação local, os elementos de matriz podem ser avaliados a partir de diferenças de vetores recíprocos como $U'_{\mathbf g-\mathbf h}$. Na forma não local, cada par $(\mathbf g,\mathbf h)$ requer sua própria integração angular, de modo que o custo cresce rapidamente com o número de feixes.

---

## Alcance do STEM por ondas de Bloch

O STEM por ondas de Bloch é rápido para cristais perfeitos e altamente periódicos e adequa-se bem a comparações sistemáticas de espessura, desfocagem e ângulos de detector. Para defeitos, supercélulas grandes ou estruturas não periódicas, métodos como o multislice de fônon congelado podem ser mais adequados, pois não se baseiam na mesma hipótese de célula periódica pequena.

No ReciPro, o STEM é mais fácil de entender da seguinte forma: comece com a mesma onda convergente do CBED e, em seguida, substitua o observável do disco de difração por uma integração de detector sobre o plano de difração.

---

## Parâmetros práticos

- **Ângulo do detector**: BF / ABF / ADF / HAADF são definições de $D(\mathbf Q)$ e $f'_{\kappa}(\mathbf g;\theta_1,\theta_2)$.
- **Número de feixes**: As componentes de imagem de alta frequência e o channeling são sensíveis ao número de feixes incluídos.
- **Passo de espessura**: Se for usada uma integração numérica por fatias, verifique a alteração quando a espessura da fatia é reduzida à metade.
- **Modelo de TDS**: Para o contraste $Z$ em HAADF, o termo de TDS é tão importante quanto o termo elástico.

## Veja também

- [Cálculo dinâmico (núcleo comum)](calculation.md)
- [Apêndice A3. Difração dinâmica pelo método de ondas de Bloch](index.md)
- [9.2. Simulação STEM](../../9-hrtem-stem-simulator/2-stem-simulation.md)

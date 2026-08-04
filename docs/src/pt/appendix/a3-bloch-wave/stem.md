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

## Reconstrução de uma imagem real {#real-image-reconstruction}

A imagem é recuperada a partir dos coeficientes por

$$I(\mathbf r)=\sum_{\mathbf q}I(\mathbf q)\,\exp(2\pi i\,\mathbf q\cdot\mathbf r),
\qquad \mathbf q=\mathbf g-\mathbf h$$

Como $I(\mathbf r)$ é uma intensidade real, seus coeficientes devem satisfazer exatamente a simetria hermitiana,

$$I(-\mathbf q)=I(\mathbf q)^{*}$$

e o conjunto de $\mathbf q$ gerado por todos os pares de feixes é fechado sob $\mathbf q\rightarrow-\mathbf q$. A soma é portanto real por construção, e **qualquer parte imaginária que sobreviva é erro numérico, não física**.

Na prática uma pequena parte imaginária sobrevive, porque a amplitude em $\mathbf k+\mathbf q$ é obtida por interpolação bilinear na grade finita de direções de incidência (veja [Amostragem angular da sonda](#angular-sampling)). Isso faz com que $I(-\mathbf q)$ e $I(\mathbf q)^{*}$ difiram de uma quantidade da ordem de $h^{2}$, onde $h$ é o passo angular.

Escrevendo um pixel somado como $a+ib$, a maneira correta de reduzi-lo a uma imagem real é tomar a **parte real** $a$. Essa é a projeção ortogonal sobre o eixo real e é idêntica a simetrizar primeiro os coeficientes,

$$I_{\mathrm{sym}}(\mathbf q)=\tfrac12\left[I(\mathbf q)+I(-\mathbf q)^{*}\right]$$

e somar depois. Tomar o módulo $\sqrt{a^{2}+b^{2}}\simeq a+b^{2}/2a$ **não** é equivalente e falha de quatro maneiras distintas:

- o termo adicional $b^{2}/2a$ é estritamente positivo, portanto nunca se cancela: é um viés, não ruído;
- é maior em relação ao sinal onde $a$ é pequeno, isto é, nos pixels **escuros**, atacando assim o contraste da imagem em vez do nível global;
- quebra a linearidade, de modo que a imagem combinada deixa de ser igual a elástica + TDS, pois $\lvert z_1+z_2\rvert\neq\lvert z_1\rvert+\lvert z_2\rvert$;
- oculta pixels negativos, que são o sintoma visível de um conjunto de $\mathbf q$ insuficiente e que de outro modo alertariam o usuário.

Por isso o ReciPro reconstrói as imagens elástica, TDS e STEM-EDX a partir da parte real e só limita a zero depois do borramento pelo tamanho da fonte, de modo que um pixel genuinamente negativo permanece detectável até esse ponto.

!!! note
    Até a versão 4.944, as imagens elástica e TDS eram somadas em módulo. Na grade angular padrão a diferença fica muito abaixo de qualquer nível perceptível (veja a tabela abaixo); ela só se torna mensurável em uma grade deliberadamente grosseira, e sempre como um leve clareamento dos pixels escuros.

---

## Amostragem angular da sonda {#angular-sampling}

O cone incidente é amostrado em uma grade quadrada de direções com passo $\Delta\alpha$ (**Resolução angular** nas opções de STEM), cobrindo o semiângulo de convergência $\alpha$ com uma pequena margem. O número de divisões ao longo de um eixo é

$$N=\left\lceil\frac{2\alpha\times1.05}{\Delta\alpha}\right\rceil$$

de modo que o número de direções, e portanto de problemas de autovalores a resolver, cresce como $N^{2}$. Essa grade não tem relação com o número de pontos de varredura: ela discretiza as *direções dentro da sonda*, não as *posições da sonda*.

Ela é também a única fonte do resíduo hermitiano descrito acima, o que torna esse resíduo um indicador de convergência conveniente. Os valores a seguir foram medidos para SrTiO₃ [001] a 200 kV com $\alpha=25$ mrad, 128 feixes e 32×32 pontos de varredura. O «resíduo» é $\max_{\mathbf q}\lvert I(\mathbf q)-I(-\mathbf q)^{*}\rvert$ relativo a $I(\mathbf 0)$, e as duas últimas colunas dão o clareamento que a soma em módulo teria acrescentado ao pixel mais brilhante.

| $N$ | Direções | Resíduo elástico | Resíduo TDS | Viés de módulo, elástico | Viés de módulo, TDS |
|----:|-----------:|-----------------:|-------------:|------------------------:|--------------------:|
| 16  | 256    | 1.2×10⁻³ | 6.1×10⁻³ | 2.4×10⁻⁵ | 1.1×10⁻⁴ |
| 32  | 1024   | 4.1×10⁻⁴ | 2.6×10⁻³ | 1.1×10⁻⁶ | 1.3×10⁻⁵ |
| 64  | 4096   | 5.6×10⁻⁵ | 7.2×10⁻⁴ | 5.8×10⁻⁸ | 4.3×10⁻⁷ |
| 132 | 17424  | 3.8×10⁻⁵ | 1.1×10⁻⁴ | 4.2×10⁻⁸ | 3.6×10⁻⁸ |

A resolução angular padrão de 0,4 mrad dá $N=132$ para $\alpha=25$ mrad, o que já está na região convergida. Dois pontos merecem nota:

- O resíduo TDS é cerca de uma ordem de grandeza maior que o elástico em todas as grades, porque os coeficientes TDS carregam adicionalmente a integral em espessura da absorção selecionada pelo detector.
- O resíduo é um máximo sobre todos os $\mathbf q$, de modo que oscila um pouco de grade para grade em vez de cair perfeitamente suave; a tendência subjacente é $O(h^{2})$.

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
- **Resolução angular**: Define a grade de direções $N$ da sonda (veja [Amostragem angular da sonda](#angular-sampling)). O custo cresce como $N^{2}$, sendo portanto a principal alavanca sobre o tempo de cálculo.
- **Modelo de TDS**: Para o contraste $Z$ em HAADF, o termo de TDS é tão importante quanto o termo elástico.

## Veja também

- [Cálculo dinâmico (núcleo comum)](calculation.md)
- [Apêndice A3. Difração dinâmica pelo método de ondas de Bloch](index.md)
- [9.2. Simulação STEM](../../9-hrtem-stem-simulator/2-stem-simulation.md)

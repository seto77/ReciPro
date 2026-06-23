# Fator de estrutura

O fator de espalhamento atômico descreve um único átomo; o **fator de estrutura** descreve como todos os átomos da célula unitária espalham *em conjunto*. É a grandeza que a aba **Reflections** tabula (`F_real`, `F_inv`, $\lvert F\rvert$, $F^2$), e é a ponte entre a física atômica da página anterior e as intensidades difratadas.

=== "X-ray"
    ![Reflections — X-ray](../../../assets/cap-pt-auto/FormBeamInteraction-xray-reflections.png)

=== "Electron"
    ![Reflections — electron](../../../assets/cap-pt-auto/FormBeamInteraction-electron-reflections.png)

=== "Neutron"
    ![Reflections — neutron](../../../assets/cap-pt-auto/FormBeamInteraction-neutron-reflections.png)

---

## Interferência sobre a célula unitária

O fator de estrutura da reflexão $\mathbf g = (hkl)$ é a soma coerente dos fatores atômicos, cada um ponderado pela fase proveniente da posição fracionária $\mathbf r_j = (x_j,y_j,z_j)$ do átomo:

$$F_{\mathbf g} = \sum_{j} o_j\, f_j(s,E)\, T_j(\mathbf g)\, \exp\!\left(-2\pi i\,(h x_j + k y_j + l z_j)\right).$$

- $o_j$ : **ocupação** do sítio (fracionária, para ocupação parcial ou mista).
- $f_j(s,E)$ : o fator de espalhamento atômico do átomo $j$ para o feixe atual — $f_0+f'-if''$ para raios X na [convenção de fase](index.md#phase-convention) do ReciPro, $f_e$ para elétrons, $b$ para nêutrons.
- $T_j(\mathbf g)$ : o fator de Debye–Waller (abaixo).
- A fase $-2\pi i$ segue a [convenção](index.md#phase-convention) do ReciPro.

A intensidade é o módulo ao quadrado,

$$I_{\mathbf g} \;\propto\; \lvert F_{\mathbf g}\rvert^2 = F_\text{real}^2 + F_\text{inv}^2 ,$$

que é a coluna $F^2$ da tabela. `F_real` e `F_inv` são as partes real e imaginária do fator de estrutura complexo. Mesmo com fatores atômicos puramente reais, $F_{\mathbf g}$ é geralmente complexo para uma estrutura não centrossimétrica (ou com origem deslocada); a dispersão anômala de raios X (com $f$ complexo) e os comprimentos de espalhamento de nêutrons complexos acrescentam uma contribuição imaginária adicional. `F_inv` se anula para *todas* as reflexões somente quando a estrutura é centrossimétrica com a origem em um centro de simetria e todos os fatores são reais.

---

## O fator de Debye–Waller

Os átomos vibram em torno de seus sítios de equilíbrio, borrando a densidade de espalhamento e reduzindo os fatores a altos ângulos. Para movimento isotrópico,

$$T_j = \exp\!\left(-B_j\, s^2\right), \qquad B_j = 8\pi^2\langle u_j^2\rangle,$$

onde $\langle u_j^2\rangle$ é o deslocamento quadrático médio ao longo da direção de espalhamento e $B_j$ é o parâmetro de deslocamento isotrópico (Å²). O movimento anisotrópico generaliza isso para

$$T_j = \exp\!\left(-2\pi^2\,\mathbf g^{\mathsf T}\!\mathbf U_j\,\mathbf g\right),$$

com $\mathbf U_j$ o tensor de deslocamento e $\mathbf g$ o vetor da rede recíproca ($|\mathbf g|=1/d$, não $Q=2\pi\lvert\mathbf g\rvert$). Para um sólido de Debye, o deslocamento quadrático médio é, por sua vez, uma função da temperatura $T$, da massa atômica $M$ e da temperatura de Debye $\Theta_D$,

$$\langle u^2\rangle = \frac{3\hbar^2}{M k_B \Theta_D}\left[\frac14 + \left(\frac{T}{\Theta_D}\right)^2\!\int_0^{\Theta_D/T}\frac{x}{e^x-1}\,dx\right],$$

de modo que $B$ aumenta com a temperatura e diminui para átomos pesados. O ReciPro usa diretamente os $B_j$ tabulados ou informados, em vez de calculá-los. Como $T_j$ multiplica o fator de espalhamento, a aba **Scattering factors** pode aplicar o mesmo amortecimento $e^{-Bs^2}$ às curvas plotadas. O amortecimento cresce com a temperatura e com $s$, razão pela qual o espalhamento térmico difuso (intensidade retirada dos feixes de Bragg coerentes e redistribuída em um fundo difuso) alimenta o potencial absortivo na teoria dinâmica ([Apêndice A3](../a3-bloch-wave/index.md)).

---

## Extinções: sistemáticas vs. acidentais

Uma reflexão pode estar **ausente** por dois motivos distintos:

- **Ausências sistemáticas (do grupo espacial).** A centragem da rede e os elementos de simetria com componente translacional (eixos helicoidais, planos de deslizamento) fazem com que classes inteiras de reflexões se anulem *exatamente*, para todo cristal daquele grupo espacial, independentemente do conteúdo atômico. Essas são as regras por trás de **Hide prohibited planes**.
- **Quase-extinções acidentais.** Quando as contribuições atômicas se cancelam por acaso para uma estrutura particular, a intensidade é pequena, mas não proibida por simetria, e pode reaparecer se a composição ou as posições mudarem. Essas *não* são removidas pelas regras de extinção.

Uma ausência sistemática é um cancelamento de fase entre as cópias da célula relacionadas por simetria. Para translações de centragem $\mathbf t_\alpha$, o fator de estrutura carrega um fator comum

$$F_{\mathbf g} \propto \sum_\alpha e^{-2\pi i\,\mathbf g\cdot\mathbf t_\alpha},$$

que é zero para certos $hkl$. Para a centragem de corpo ($\mathbf t = \tfrac12,\tfrac12,\tfrac12$),

$$1 + e^{-\pi i (h+k+l)} = 0 \quad\Longleftrightarrow\quad h+k+l \ \text{odd}.$$

As ausências sistemáticas mais comuns são:

| Elemento de simetria | Condição para ausência | Reflexões afetadas |
|---|---|---|
| $I$ (de corpo centrado) | $h+k+l$ ímpar | todas as $hkl$ |
| $F$ (de faces centradas) | $h,k,l$ de paridade mista | todas as $hkl$ |
| $C$ (centrado em C) | $h+k$ ímpar | todas as $hkl$ |
| eixo helicoidal $2_1$ $\parallel b$ | $k$ ímpar | $0k0$ |
| deslizamento $a$ $\perp b$ | $h$ ímpar | $h0l$ |
| deslizamento $c$ $\perp b$ | $l$ ímpar | $h0l$ |

As condições de centragem aplicam-se a toda reflexão; as condições de eixo helicoidal e de deslizamento aplicam-se apenas à fileira axial ou à zona correspondente, o que é exatamente o que as torna diagnósticas do grupo espacial.

---

## Lei de Friedel e sua quebra

Para uma estrutura de fatores de espalhamento reais (não ressonantes), conjugar a soma e inverter o sinal de $\mathbf g$ mostra diretamente que (omitindo os pesos reais $o_j T_j$ para maior clareza)

$$F_{-\mathbf g} = \sum_j f_j\, e^{+2\pi i\,\mathbf g\cdot\mathbf r_j} = \left(\sum_j f_j\, e^{-2\pi i\,\mathbf g\cdot\mathbf r_j}\right)^{*} = F_{\mathbf g}^{*}, \qquad\text{hence}\qquad \lvert F_{hkl}\rvert = \lvert F_{\bar h\bar k\bar l}\rvert \quad\text{(Friedel's law).}$$

A difração então parece centrossimétrica mesmo quando o cristal não é. **A dispersão anômala pode quebrar isso.** Escrevendo o fator de estrutura como uma parte normal (que conjuga de forma limpa) mais uma parte anômala, $F_{\mathbf g} = A_{\mathbf g} - i B_{\mathbf g}$ e $F_{-\mathbf g} = A_{\mathbf g}^{*} - i B_{\mathbf g}^{*}$ na convenção $f = f_0 + f' - i f''$ do ReciPro, a **diferença de Bijvoet** é

$$\lvert F_{\mathbf g}\rvert^2 - \lvert F_{-\mathbf g}\rvert^2 = -4\,\operatorname{Im}\!\left(A_{\mathbf g}\, B_{\mathbf g}^{*}\right),$$

não nula apenas quando as partes normal e anômala têm fases diferentes — isto é, quando espalhadores anômalos quimicamente distintos ocupam sítios não centrossimétricos. (A diferença se anula para uma estrutura centrossimétrica, um único elemento ou qualquer caso em que todo átomo carregue o mesmo fator complexo.) É isso que permite determinar a estrutura absoluta (quiralidade) de um cristal não centrossimétrico, e é a razão física pela qual o ReciPro reporta um `F_inv` não nulo e $\lvert F\rvert$ distintos para os pares de Friedel uma vez que uma energia de raios X próxima de uma borda seja escolhida.

---

## Do fator de estrutura à intensidade de pó

Ativar **Powder Diffraction Intensities (Bragg–Brentano)** converte $\lvert F\rvert^2$ em uma intensidade de pó relativa ao incorporar a geometria de um policristal orientado aleatoriamente:

$$I_{hkl} \;\propto\; m_{hkl}\, \lvert F_{hkl}\rvert^2\, L p(\theta),$$

- $m_{hkl}$ : **multiplicidade** — o número de planos equivalentes por simetria que se sobrepõem no mesmo $2\theta$ (a coluna *Multi.* da tabela).
- $Lp(\theta)$ : o fator de **Lorentz–polarização** para a óptica de Bragg–Brentano, $Lp = \dfrac{1+\cos^2 2\theta}{\sin^2\theta\,\cos\theta}$, que reforça fortemente os picos a baixos ângulos.

Como os planos equivalentes são mesclados em uma única linha nesse modo, o ReciPro também força a ativação de *Hide equivalent planes* e *Hide prohibited planes*.

---

## Veja também

- [Fatores de espalhamento atômico](scattering-factor.md) — os $f_j$ que entram na soma.
- [Atenuação & transporte](attenuation-transport.md) — o que acontece com o feixe entre os eventos de espalhamento.
- [3. Interação do feixe → aba Reflections](../../3-beam-interaction.md#reflections-tab)
- [Apêndice A3. Difração dinâmica](../a3-bloch-wave/index.md) — quando $\lvert F\rvert^2$ (cinemática) já não é suficiente.

# Simulação HRTEM

A simulação **HRTEM (High-Resolution Transmission Electron Microscopy)** calcula imagens de franjas de rede de TEM de alta resolução. É o modo principal do [Simulador HRTEM/STEM](index.md).

![Simulador no modo HRTEM](../../assets/cap-pt-auto/FormImageSimulator-hrtem.png)

> Esta página aborda todas as configurações que aparecem no lado direito quando **Modo de imagem = HRTEM**. Para os controles do lado esquerdo — exibição do resultado e ajuste do brilho — consulte a [página de visão geral](index.md#display-settings).

---

## Visão geral

Uma imagem HRTEM se forma quando a onda eletrônica transmitida pela amostra é projetada sob a influência das aberrações da lente objetiva. O ReciPro calcula a propagação da onda eletrônica dentro da amostra pelo método de ondas de Bloch (cálculo dinâmico) e gera a imagem HRTEM por meio da função de transferência de contraste de fase (PCTF).

### Fluxo de cálculo

1. **Método de ondas de Bloch**: calcula a propagação da onda eletrônica no potencial do cristal e obtém a amplitude e a fase da onda de saída
2. **Função da lente**: aplica as aberrações da lente objetiva (aberração esférica $C_s$, desfoco $\Delta f$)
3. **Coerência parcial**: leva em conta o tamanho finito da fonte (coerência espacial) e a flutuação de energia (coerência temporal)
4. **Formação da imagem**: calcula a distribuição de intensidade $|\psi(\mathbf{r})|^2$

Para a teoria, consulte o [Apêndice A3.2 — Formação da imagem HRTEM](../appendix/a3-bloch-wave/hrtem.md).

---

## Amostra

![Amostra](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png)

- **Espessura** : espessura da amostra (nm). As imagens HRTEM dependem fortemente da espessura. No modo **Imagem em série** este valor é ignorado e, em seu lugar, é usada a lista de espessuras descrita abaixo.

---

## Condições TEM {#tem-conditions}

![Condições TEM](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

Define as condições de imagem da lente objetiva.

| Parâmetro | Descrição | Padrão / típico |
|-----------|-------------|-------------------|
| **Tensão de acel. (kV)** | Tensão de aceleração. O comprimento de onda do elétron corrigido relativisticamente é exibido à direita | 200 kV |
| **Desfoco Δf** | Desfoco da lente objetiva (nm). O valor de referência do **Desfoco de Scherzer** é exibido abaixo | −57.8 nm |
| **Cs** | Coeficiente de aberração esférica (mm). Afeta a CTF e o desfoco de Scherzer. Valores negativos (−20 a 20 mm) são aceitos | 1.0 mm (padrão); 0.5–1.0 (convencional), < 0.01 (corrigido por Cs) |
| **Cc** | Coeficiente de aberração cromática (mm). Determina o borramento da imagem causado pela dispersão de energia | 1.4 mm (padrão); 1.0–2.0 mm |
| **β** | Semiângulo de iluminação (mrad). Representa o efeito do tamanho finito da fonte (coerência espacial) | 0 mrad (padrão); 0.1–1.0 mrad |
| **ΔV** | Largura a meia altura da dispersão de energia dos elétrons (eV). Junto com Cc, determina a dispersão de foco devida à aberração cromática | 0.8 eV (padrão); 0.5–2.0 eV |

Os padrões acima valem apenas na primeira execução; depois disso, os últimos valores usados são restaurados. Internamente, ΔV (FWHM) é convertido num desvio padrão $\sigma_V = \Delta V / (2\sqrt{2\ln 2})$ e a dispersão de foco é $\delta = C_c\,\sigma_V / V$. A função de aberração é

$$\chi(u) = \pi\lambda\,\Delta f\,u^2 + \tfrac{\pi}{2}\,C_s\lambda^3 u^4 ,$$

e cada onda difratada é multiplicada por $\exp[-i\chi(u)]$, de modo que **Δf negativo é subfoco** (a convenção em que o desfoco de Scherzer é negativo).

> **Menu de clique direito**: no painel Condições TEM é possível aplicar com um único clique **Definir desfoco como 0 nm** / **Definir desfoco com o valor de Scherzer** / **Definir todas as aberrações como zero** (este último define Cs, Cc, β, ΔV **e o desfoco** como 0). O mesmo menu contém quatro predefinições de microscópio — *300kV Cold-FEG sem Cs (Cs 0, Cc 2.8, ΔE 0.3) ARM300F*, *200kV Schottky-FEG UHR (Cs 0.5, Cc 1.1, ΔE 0.8) 2100F*, *200kV Schottky-FEG HR (Cs 1.0, Cc 1.4, ΔE 0.8) 2100F* e *200kV Lab6 HR (Cs 1.0 , Cc 1.4, ΔE 2.0) 2100* — que definem a tensão de aceleração, Cs, Cc e ΔV conforme escrito no item e, em seguida, definem o desfoco com o valor de Scherzer (β não é alterado). A caixa de seleção **Predefinições**, no canto inferior direito, abre a janela que armazena as suas próprias condições (veja [Lista de predefinições](index.md#preset-list)).

### Desfoco de Scherzer

O valor de desfoco próximo do qual o contraste de fase é ótimo, calculado a partir do comprimento de onda atual e da aberração esférica $C_s$ (exibido como referência). Para $C_s$ negativo, o valor exibido é $+\sqrt{\tfrac{4}{3}|C_s|\lambda}$.

$$\Delta f_{\text{Scherzer}} = -\sqrt{\tfrac{4}{3}\,C_s \lambda}\quad\left(\approx -1.155\,\sqrt{C_s \lambda}\right)$$

Nessa condição a PCTF é negativa em uma ampla faixa de frequências espaciais, de modo que as posições atômicas aparecem com contraste escuro. O ReciPro adota este valor original de Scherzer (derivado ao fixar o mínimo da fase de aberração $\chi$ em $-2\pi/3$), e o valor exibido na GUI segue esta fórmula. Observe que algumas referências usam, em vez disso, o valor de *Scherzer estendido* $-1.2\sqrt{C_s\lambda}$, que alarga ainda mais a banda de transferência.

---

## Função da lente / Função de transferência de contraste (CTF)

Marcar **Função de transferência de contraste (CTF)** abre uma janela que traça como as aberrações da lente e o desfoco transferem o contraste da imagem em cada frequência espacial.

![Função de transferência de contraste (CTF)](../../assets/cap-pt-auto/FormCTF.png)

- $\sin\chi(u)$ : função de transferência de contraste de fase ($\chi(u)$ é a função de aberração da lente)
- $E_\text{s}(u)$ : função envelope de coerência espacial; o amortecimento devido ao tamanho finito da fonte ($\beta$)
- $E_\text{c}(u)$ : função envelope de coerência temporal; o amortecimento devido à flutuação de energia ($C_c$, $\Delta V$)

Cada curva tem sua própria caixa de seleção: $\sin\chi$, $\cos\chi$, $E_\text{s}$, $E_\text{c}$, **PCTF** $=\sin\chi\,E_\text{s}E_\text{c}$ e **ACTF** $=\cos\chi\,E_\text{s}E_\text{c}$; PCTF e ACTF são zeradas fora da abertura da objetiva. No modo HRTEM,

$$E_\text{s}(u)=\exp\!\left[-\pi^2\beta^2u^2\left(\Delta f+C_s\lambda^2u^2\right)^2\right],\qquad E_\text{c}(u)=\exp\!\left[-\tfrac{1}{2}\pi^2\lambda^2\delta^2u^4\right].$$

**u máx.** (nm⁻¹, padrão 6) define o limite superior do eixo horizontal. **Copiar** coloca as curvas traçadas na área de transferência como texto separado por tabulações. A janela também está disponível no modo STEM (veja [Simulação STEM](2-stem-simulation.md#ctf-stem)); ela fica desativada no modo Potencial.

---

## Abertura da objetiva (opção HRTEM)

![Abertura da objetiva (opção HRTEM)](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxHREMoption1.png)

Restringe as ondas difratadas que passam pela abertura da objetiva. O próprio cálculo de ondas de Bloch sempre usa o número de ondas definido em **Ondas**; a abertura seleciona então quais desses feixes contribuem para a imagem (um feixe passa quando $|\mathbf{g}-\mathbf{c}| < 2\sin(\alpha_\text{obj}/2)/\lambda$, sendo $\mathbf{c}$ o centro da abertura). Se menos de dois feixes passarem, o cálculo é interrompido com uma mensagem pedindo que você aumente a abertura.

- **Tamanho** : semiângulo da abertura da objetiva (mrad, padrão 12). Quanto menor, mais ondas difratadas de alto ângulo são cortadas e mais suaves ficam os detalhes de alta resolução. O raio equivalente no espaço recíproco $\sin\theta/\lambda$ (nm⁻¹) é exibido.
- **Deslocamento X** / **Y** : deslocamento do centro da abertura da objetiva (mrad). Usado para imagens de campo escuro com abertura deslocada; a iluminação em si não é inclinada.
- **Abertura aberta** : abre a abertura da objetiva (infinita), de modo que todas as ondas difratadas sejam usadas na formação da imagem.
- **reflexões dentro** : o número de feixes difratados (pontos) que caem dentro da abertura (somente leitura).
- **Info da reflexão** : abre uma tabela que lista os feixes difratados dentro da abertura (intensidade, amplitude complexa etc.).

> O tamanho da abertura da objetiva também é exibido no **Simulador de difração**.

---

## Opções HRTEM (modelo de coerência parcial)

![Opções HRTEM (modelo de coerência parcial)](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxHREMoption2.png)

Seleciona como a coerência parcial (β e a dispersão de foco δ) é incluída. A onda de saída é calculada para uma única direção incidente (o eixo do feixe); a dispersão da iluminação e a dispersão de energia entram apenas por meio das funções envelope abaixo.

- **Imagem linear** (modelo quase coerente) : computacionalmente barato. Cada onda difratada $\psi_g$ da onda de saída dinâmica é multiplicada por $\exp[-i\chi(g)]\,E_\text{s}(g)\,E_\text{c}(g)$ e a imagem é $\left|\sum_g\right|^2$. Preciso quando a imagem é dominada pela interferência com o feixe transmitido (amostras finas).
- **Coef. cruz. de transm.** (coeficiente de transmissão cruzada) : computacionalmente caro, porém mais preciso. Cada par de feixes $(g,h)$ é somado com o envelope mútuo (o coeficiente de transmissão cruzada completo), e é o modelo a usar para espalhadores fortes que excitam muitas ondas difratadas intensas. O custo cresce com o quadrado do número de feixes dentro da abertura.

Para mais detalhes, consulte o [Apêndice A3.2 — Formação da imagem HRTEM](../appendix/a3-bloch-wave/hrtem.md).

---

## Modo único/série

![Modo único/série](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSerialImage.png)

- **Imagem única** : calcula uma imagem HRTEM na espessura e no desfoco atuais.
- **Imagem em série** : gera um conjunto de imagens variando a espessura e o desfoco passo a passo (uma série em espessura / em foco). Útil para encontrar a condição que melhor corresponde a uma imagem experimental.

Para uma imagem em série, defina o seguinte.

| Item | Descrição |
|------|-------------|
| **Espessura (nm)** / **Desfoco (nm)** | Qual grandeza varrer (ambas são permitidas) |
| **Start / Passo / Núm** | Valor inicial, largura do passo e número de imagens. São expandidos na caixa de lista abaixo, que também pode ser editada diretamente |
| **Direção horizontal:** | Quando espessura e desfoco são varridos, a grandeza disposta na direção horizontal da grade (**Desfoco** ou **Espessura**) |

Varrer espessura e desfoco produz uma matriz de imagens linhas × colunas. **Núm** é limitado a 20 para cada grandeza. Quando apenas uma grandeza é varrida e há mais de duas imagens, a linha é quebrada numa grade aproximadamente quadrada.

---

## Propriedades da imagem

![Propriedades da imagem](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

- **Tamanho (L×A)** : número de pixels da imagem simulada (512×512 por padrão).
- **Resolução** : resolução de amostragem (pm/px, padrão 2). O campo de visão é Tamanho × Resolução, de modo que um valor menor amostra as franjas com mais detalhe numa área menor. A imagem é sintetizada por uma soma de Fourier direta sobre os feixes em cada pixel (sem FFT), portanto o tempo de cálculo depende do número de pixels e do número de feixes, e não deste valor.

---

## Ondas

![Ondas](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

- Número máximo de ondas de Bloch usadas no método de Bethe (cálculo dinâmico), 80 por padrão. Um número maior melhora a precisão, mas o problema de autovalores leva um tempo $O(N^3)$ para ser resolvido.

---

## Veja também

- [Simulador HRTEM/STEM (visão geral)](index.md)
- [Simulação STEM](2-stem-simulation.md)
- [Simulação de potencial](3-potential-simulation.md)
- [Apêndice A3.2 — Formação da imagem HRTEM](../appendix/a3-bloch-wave/hrtem.md)

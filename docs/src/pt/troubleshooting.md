# Solução de problemas

Problemas comuns e soluções para o ReciPro. Muitas das entradas a seguir provêm de perguntas e relatos de bugs no [rastreador de issues do GitHub](https://github.com/seto77/ReciPro/issues); a versão em que um bug foi corrigido é indicada quando aplicável.

> **A maioria dos problemas é resolvida simplesmente atualizando para a [versão mais recente](https://github.com/seto77/ReciPro/releases/latest).** O ReciPro é atualizado com frequência, e muitos dos bugs abaixo foram corrigidos em poucos dias após terem sido relatados.

---

## Inicialização e abertura

### Sintoma: O processo está em execução, mas nenhuma janela aparece

O ReciPro inicia (ele fica visível no Gerenciador de Tarefas), mas sua janela nunca aparece na tela.

**Causa**: A janela abriu fora da tela — um problema de coordenadas de exibição do Windows, normalmente após trocar de monitor ou alterar a escala de exibição. (Issues [#50](https://github.com/seto77/ReciPro/issues/50), [#53](https://github.com/seto77/ReciPro/issues/53), [#55](https://github.com/seto77/ReciPro/issues/55))

**Solução**:

1. Abra o **Gerenciador de Tarefas**.
2. Encontre o **ReciPro** na lista de processos.
3. Clique nele com o botão direito e escolha **Maximizar**.

A janela será trazida para a sua tela principal. Observe que **Alternar para**, **Trazer para a frente** e **Minimizar** *não* ajudam — apenas **Maximizar** funciona.

### Sintoma: O ReciPro não inicia, trava ou congela na inicialização

**Causa**: Na maioria das vezes a inicialização do OpenGL falha, ou um valor de registro/configuração corrompido bloqueia a inicialização.

**Solução** (tente nesta ordem):

1. **Desabilitar o OpenGL**: mantenha a tecla **Ctrl** pressionada ao iniciar o ReciPro para iniciar com o OpenGL desabilitado. As versões recentes (v4.925 e posteriores) reforçam a inicialização do OpenGL para que o aplicativo seja iniciado mesmo quando o OpenGL falha — nesse caso os recursos 3D ficam desabilitados, mas o restante do aplicativo funciona.
2. **Redefinir as configurações**: no editor de registro, exclua a chave `HKEY_CURRENT_USER\Software\Crystallography\ReciPro` e reinicie. (Equivalente a **Option → Reset registry**.)
3. **Reinstalação limpa**: desinstale o ReciPro, exclua as seguintes pastas se existirem (substitua `<user>` pelo nome da sua conta) e reinstale:
   - `C:\Users\<user>\AppData\Local\Crystallography Software\ReciPro`
   - `C:\Users\<user>\AppData\Roaming\ReciPro\ReciPro`
4. **Atualize** para a versão mais recente.

Se nada disso ajudar, a causa pode estar no próprio ambiente do sistema operacional; por favor [abra um issue](https://github.com/seto77/ReciPro/issues) com os detalhes do seu PC (CPU, GPU, versão do Windows).

---

## Problemas de OpenGL

### Sintoma: Tela preta ou travamento na inicialização

**Causa**: GPU incompatível ou ambiente de área de trabalho remota.

**Solução**:

1. Vá em **Option → Disable OpenGL (needs restart)** (ou mantenha **Ctrl** pressionado ao iniciar).
2. Reinicie o ReciPro.
3. O Visualizador de estrutura e alguns recursos 3D passarão a usar renderização por software.

### Sintoma: GPU integrada ou antiga (Intel/AMD) não renderiza

**Causa**: Algumas GPUs integradas (por exemplo, AMD Radeon Vega, Intel UHD) tinham problemas de inicialização do OpenGL em builds antigos. (Issue [#2](https://github.com/seto77/ReciPro/issues/2))

**Solução**: Atualize para a versão mais recente. O requisito de versão do OpenGL foi reduzido (v4.781), a inicialização de GPUs integradas foi corrigida (v4.785), e a inicialização foi reforçada ainda mais para falhar de forma controlada (v4.925). Atualizar os drivers da sua GPU também ajuda.

### Sintoma: Qualidade de renderização ruim

**Solução**: Atualize os drivers da sua GPU. Recomenda-se uma GPU externa (dedicada) com suporte a OpenGL 1.5.

---

## .NET Runtime

### Sintoma: O aplicativo não inicia

**Causa**: O .NET Desktop Runtime necessário não está instalado. As versões atuais exigem o **.NET Desktop Runtime 10.0** (builds antigos: v4.895–v4.91x exigiam 9.0; consulte o issue [#43](https://github.com/seto77/ReciPro/issues/43)).

**Solução**: Baixe e instale a partir de <https://dotnet.microsoft.com/download/dotnet/10.0> (escolha o **Desktop Runtime**, x64 para a maioria dos PCs).

### Sintoma: Não é possível acessar a página de download da Microsoft

**Solução**: Você pode baixar o instalador do runtime diretamente. Escolha o **Windows Desktop Runtime X64** para a sua arquitetura na [página de download do .NET 10.0](https://dotnet.microsoft.com/download/dotnet/10.0). (Issue [#49](https://github.com/seto77/ReciPro/issues/49))

---

## Instalação

### Sintoma: Instalar ou desinstalar sem direitos de administrador

**Observação**: Direitos de administrador não são necessários. Atalhos e arquivos por usuário são colocados nas suas próprias pastas de usuário (por exemplo, `%AppData%\Microsoft\Windows\Start Menu\Programs\Crystallography Software\` e a Área de Trabalho). (Issue [#38](https://github.com/seto77/ReciPro/issues/38))

---

## Exibição e layout

### Sintoma: Botões ou painéis estão cortados / ocultos, ou o layout parece quebrado

Por exemplo, o botão **Peak Identification** no Spot ID v2 está oculto, ou a página Sobre e outros formulários estão desalinhados nas versões recentes. (Issues [#56](https://github.com/seto77/ReciPro/issues/56), [#59](https://github.com/seto77/ReciPro/issues/59))

**Causa**: Uma regressão de escala DPI / fonte da interface introduzida em alguns builds recentes.

**Solução**:

- Defina a **escala de exibição do Windows para 100%** (isso geralmente restaura o layout).
- Como solução rápida, **redimensione a janela** (por exemplo, reduza-a verticalmente) para revelar os controles ocultos.
- Atualize para a versão mais recente — os layouts estão sendo corrigidos progressivamente. Se um build recente parecer pior, reverter para uma versão um pouco mais antiga (por exemplo, v4.915) é uma opção temporária. Por favor, relate quaisquer formulários ainda quebrados.

---

## Cálculos dinâmicos

### Sintoma: Muito lento ou sem memória

**Causa**: Ondas de Bloch em excesso ou imagem grande demais.

**Solução**:

- Reduza **No. of Bloch waves** (50–200 geralmente é suficiente para cálculos de rotina)
- Use o solucionador **Eigen** para ≤ 500 ondas; **MKL** para > 500 ondas
- Reduza a resolução da imagem para simulações STEM
- Feche outros aplicativos que consomem muita memória

### Sintoma: A imagem HAADF-STEM está preta

**Causa**: Os fatores de temperatura atômicos (B) estão definidos como zero.

**Solução**: Defina B ≥ 0.5 Å² para todos os átomos. A intensidade do TDS requer fatores de temperatura diferentes de zero.

---

## Simulador de difração

### Sintoma: O padrão de difração está em branco / nada é desenhado

**Causa**: Geralmente a visualização está com zoom excessivo, ou a energia da onda incidente está fora do intervalo. (Issue [#3](https://github.com/seto77/ReciPro/issues/3))

**Solução**:

- **Clique com o botão esquerdo** na área de desenho principal para reduzir o zoom.
- Verifique a energia da onda incidente na aba **Wave** (canto superior esquerdo): raios X ≈ 1–100 keV, elétrons ≈ 10–1000 keV são adequados.

---

## Entrada/saída de arquivos

### Sintoma: O arquivo CIF não carrega

**Solução**:

- Verifique se o arquivo CIF está bem formado
- Tente arrastar e soltar o arquivo na área **Informação do cristal**
- Algumas extensões CIF não padronizadas podem não ser suportadas

### Sintoma: O arquivo dm3/dm4 não carrega, ou "unable to cast … 'System.Single' to 'System.Double'"

**Causa**: Existem várias variantes do formato DM3/DM4, e builds antigos não conseguiam ler todas elas. (Issue [#15](https://github.com/seto77/ReciPro/issues/15))

**Solução**: Atualize para a versão mais recente — a compatibilidade de leitura de DM3 foi melhorada na v4.835. Se um arquivo ainda não carregar, por favor [envie-o](https://github.com/seto77/ReciPro/issues) para que o suporte possa ser adicionado.

### Sintoma: O arquivo dm3/dm4 mostra escala incorreta

**Solução**: Verifique a calibração no software original Digital Micrograph. O ReciPro lê os metadados incorporados; se os metadados estiverem incorretos, defina manualmente o tamanho do pixel e o comprimento de câmera no painel Óptica.

---

## Redefinir o registro

Se as configurações ficarem corrompidas:

1. **Option → Reset registry (after restart)**
2. Reinicie o ReciPro — posições das janelas, comprimento de onda, comprimento de câmera etc. serão redefinidos para os valores padrão

---

## Perguntas frequentes

### Existe uma versão para Mac (ou Linux)? {#mac-linux}

Não há versão oficial para Mac ou Linux. O ReciPro depende do **.NET Desktop Runtime**, que atualmente roda apenas no Windows. (Issue [#12](https://github.com/seto77/ReciPro/issues/12))

No entanto, foi relatado um caminho não oficial que funciona no macOS: a distribuição **win-x64 portable ZIP** (disponível na [página de releases](https://github.com/seto77/ReciPro/releases/latest)) roda no macOS (Apple Silicon) usando o wrapper Wine **Sikarugir** combinado com o driver OpenGL **Mesa3D** — sem necessidade de licença do Windows ou máquina virtual. Um guia de configuração passo a passo publicado por um usuário está disponível em <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>.

Observe que essa configuração não é oficialmente suportada nem totalmente verificada. Uma limitação conhecida é que alguns símbolos (Å, sobrescritos, setas) podem ser exibidos incorretamente.

**Corrigindo os símbolos corrompidos (Å, sobrescritos, setas):** A causa é que as fontes do Windows que o ReciPro normalmente usa (Segoe UI, Yu Gothic UI etc.) estão ausentes no ambiente Wine, e as fontes substitutas integradas do Wine não possuem alguns glifos científicos. O ReciPro alterna automaticamente para fontes de ampla cobertura **quando detecta que está sendo executado sob o Wine**, então a correção é simplesmente disponibilizar essas fontes no prefix do Wine:

1. Instale **DejaVu Sans** / **DejaVu Serif** (cobre Å, sobrescritos, setas, rótulos de frações) e, para a interface em japonês, **Noto Sans CJK JP** (ou **Noto Sans JP**).
2. A maneira mais simples é copiar os arquivos `.ttf`/`.otf` baixados para a pasta de fontes do prefix — `…/drive_c/windows/Fonts/` dentro do wrapper Sikarugir — e então reiniciar o ReciPro. (O `winetricks` também pode instalar algumas dessas.)
3. Ao reiniciar, o ReciPro as reconhece automaticamente; nenhuma configuração do ReciPro precisa ser alterada.

Se as fontes não estiverem instaladas, o ReciPro mantém seus nomes de fonte padrão, então nada piora — os símbolos simplesmente continuam corrompidos.

**Perspectiva para esse caminho — duas observações honestas:**

- O ZIP experimental **win-arm64** **não** roda nos Macs atuais, nem mesmo no Apple Silicon: os builds de Wine para macOS de hoje (incluindo o Sikarugir) executam binários do Windows x86_64 através do Rosetta 2 e não têm nenhum mecanismo para executar binários do Windows ARM64. Em um Mac, use sempre o **win-x64** portable ZIP.
- A Apple está descontinuando o Rosetta 2 gradualmente. O macOS 27 (outono de 2026) foi anunciado como a última versão com suporte completo ao Rosetta 2, então o caminho atual de x64 + Rosetta deve parar de funcionar a partir do macOS 28 (outono de 2027). Um Wine ARM64 nativo para macOS está em desenvolvimento upstream; se ele se concretizar, o ZIP win-arm64 poderá tornar-se o sucessor no Mac, mas isso ainda não pode ser prometido.

### O ReciPro roda no Windows on ARM (ARM64)? {#windows-on-arm}

Sim — há dois caminhos:

- **Pacote ARM64 nativo (experimental, recomendado)**: a partir da v4.938, um pacote portátil ARM64 nativo experimental (`ReciPro-v.X_arm64.zip`; chamado `ReciPro-v.X-arm64.zip` até a v.4.939) é publicado na [página de releases](https://github.com/seto77/ReciPro/releases/latest). Ele é self-contained, então nenhuma instalação do .NET Runtime é necessária — extraia o ZIP para uma pasta gravável pelo usuário e execute `ReciPro.exe`. Se o Windows bloquear o ZIP baixado (Mark of the Web), clique com o botão direito no ZIP → **Propriedades** → marque **Desbloquear** → **OK** *antes* de extrair (ou execute `Unblock-File .\ReciPro-*arm64.zip` no PowerShell). Os detalhes estão no `README-PORTABLE.txt` incluído.
- **Pacote x64 sob emulação**: o instalador MSI regular e o win-x64 portable ZIP também rodam no Windows ARM64 através da emulação x64 integrada, com o .NET Desktop Runtime (x64) instalado (confirmado a partir de cerca da v4.913 com o .NET 10). Cálculos pesados rodam mais lentamente do que no build nativo. (Issue [#47](https://github.com/seto77/ReciPro/issues/47))

Observações sobre o pacote ARM64 nativo:

- O Intel MKL não existe para ARM64, portanto as opções de solucionador e itens de menu correspondentes ficam ocultos. Os cálculos dinâmicos usam a biblioteca nativa otimizada com NEON incluída; em casos de validação representativos, seus resultados coincidiram com o build x64 dentro da tolerância de ponto flutuante esperada.
- As visualizações 3D (Visualizador de estrutura e janelas relacionadas) podem funcionar, mas o Windows on ARM fornece OpenGL apenas através de uma camada de tradução Direct3D 12 (GLOn12 / Mesa), de modo que a renderização 3D é perceptivelmente mais lenta do que em um PC com um driver OpenGL nativo — isso é uma limitação de plataforma, não um bug, e um build ARM64 nativo não pode mudar isso. O modo de transparência **High quality (Per-Pixel Linked List)** no Visualizador de estrutura é particularmente lento nessa pilha de drivers; o modo padrão **Approximate** é recomendado. Se as visualizações 3D não iniciarem, instale o "OpenCL, OpenGL, and Vulkan Compatibility Pack" da Microsoft Store.
- O pacote ARM64 **não** roda no macOS + Wine (consulte a pergunta anterior). Em um Mac, use o win-x64 portable ZIP.

### Como devo citar o ReciPro?

Use o link **Cite this repository** na [página do repositório no GitHub](https://github.com/seto77/ReciPro) (os metadados são fornecidos pelo `CITATION.cff`). A citação preferida é:

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

(Issue [#33](https://github.com/seto77/ReciPro/issues/33))

---

## Relatar bugs

Relate problemas em: <https://github.com/seto77/ReciPro/issues>

Por favor, inclua:

- Número da versão do ReciPro
- Passos para reproduzir o problema
- Quaisquer mensagens de erro ou capturas de tela

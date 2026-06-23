# Manual do ReciPro

<!-- 260623Ch: Shared demo movie for all localized top pages. -->
<div class="rp-demo-video" markdown="0">
  <video controls muted playsinline preload="metadata" aria-label="ReciPro demo movie">
    <source src="../assets/Recipro_Demo.mp4" type="video/mp4">
  </video>
</div>

## Breve introdução
* O ReciPro é um software livre licenciado sob a MIT que fornece uma variedade de cálculos cristalográficos e simulações de microscopia eletrônica.
* O ReciPro foi baixado mais de 27.000 vezes no total desde seu lançamento no GitHub (março de 2020) e é usado por muitos cristalógrafos e microscopistas eletrônicos.

## Encontrar por objetivo

| Objetivo | Comece aqui | Próximos passos principais |
|------|------------|-----------------|
| Carregar um cristal e definir sua orientação | [Janela principal](0-main-window.md) | [Geometria de rotação](4-rotation-geometry.md), [Apêndice A1. Sistemas de coordenadas](appendix/a1-coordinate-system/1-orientation.md) |
| Inspecionar uma estrutura cristalina em 3D | [Visualizador de estrutura](5-structure-viewer.md) | [Informação de simetria](2-symmetry-information.md) |
| Calcular padrões SAED / XRD / PED / CBED | [Simulador de difração](7-diffraction-simulator/index.md) | [SAED](7-diffraction-simulator/1-saed-simulation.md), [Difração de raios X](7-diffraction-simulator/4-x-ray-neutron-diffraction.md), [PED](7-diffraction-simulator/2-ped-simulation.md), [CBED](7-diffraction-simulator/3-cbed-simulation.md) |
| Calcular imagens HRTEM / STEM | [Simulador HRTEM/STEM](9-hrtem-stem-simulator/index.md) | [HRTEM](9-hrtem-stem-simulator/1-hrtem-simulation.md), [STEM](9-hrtem-stem-simulator/2-stem-simulation.md) |
| Simular padrões EBSD | [Simulação EBSD](12-ebsd-simulation.md) | [Trajetórias eletrônicas](8-electron-trajectory.md), [Apêndice A3. Cálculo EBSD](appendix/a3-bloch-wave/ebsd.md) |
| Indexar reflexões de difração experimentais | [Spot ID v1](10-spot-id.md), [Spot ID v2](11-spot-id-v2.md) | [Simulador de difração](7-diffraction-simulator/index.md) |
| Compreender as equações da difração dinâmica | [Apêndice A3. Método de ondas de Bloch](appendix/a3-bloch-wave/index.md) | [Cálculo dinâmico](appendix/a3-bloch-wave/calculation.md), [CBED](appendix/a3-bloch-wave/cbed.md), [STEM](appendix/a3-bloch-wave/stem.md), [EBSD](appendix/a3-bloch-wave/ebsd.md) |

## Recursos
* **Full GUI** : Todas as operações são realizadas por meio de uma interface gráfica. A maioria das operações de entrada/saída de arquivos suporta arrastar e soltar.
* **Lista de cristais** : Gerencie vários cristais ao mesmo tempo; não é necessário abrir uma janela separada para cada cristal.
* **Banco de dados de grupos espaciais** : Banco de dados integrado abrangendo 230 grupos espaciais das International Tables Volume A, além de 530 símbolos de Hall, com elementos de simetria, posições de Wyckoff e regras de extinção. Elementos de simetria e posições gerais podem ser desenhados como diagramas esquemáticos no estilo das *International Tables* Vol. A (veja [2. Informação de simetria](2-symmetry-information.md)).
* **Informação atômica** : Fatores de espalhamento (raios X, elétron, nêutron), energias de raios X característicos, razões isotópicas, etc. para os elementos H (1) – Cf (98).
* **Rotação flexível do cristal** : Defina a orientação por índices de eixo de zona/plano cristalino ou arrastando o mouse. A notação de Miller-Bravais (4 índices *hkil*) é suportada para os sistemas trigonal/hexagonal. O estado de rotação é sincronizado entre todas as janelas de simulação.
* **Simulação de difração** : Difração eletrônica cinemática e dinâmica (método de ondas de Bloch), difração de raios X (incluindo câmeras de precessão e back-Laue), difração eletrônica de precessão (PED) e difração eletrônica de feixe convergente (CBED). Uma simulação de suporte TEM vincula o padrão de difração aos ângulos de inclinação do suporte.
* **Simulação HRTEM / STEM** : Simulação de imagens TEM de alta resolução com modelos de coerência parcial; STEM com espalhamento térmico difuso.
* **EBSD e trajetórias eletrônicas** : Simulação de padrões EBSD e simulação de Monte Carlo das trajetórias eletrônicas (veja [8. Trajetórias eletrônicas](8-electron-trajectory.md)).
* **Indexação de reflexões** : Detecção, ajuste e indexação automáticos de reflexões de difração a partir de imagens experimentais (Spot ID v1/v2).
* **Macro** : Macro com sintaxe Python para automatizar operações (veja [20. Macro](20-macro/index.md)).
* **Tema claro / escuro** : A interface segue um modo de cor claro ou escuro selecionável.

## Requisitos do sistema
| Item | Mínimo | Recomendado |
|------|---------|-------------|
| SO | Windows com [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows on ARM64 suportado) | Windows 11 |
| GPU | OpenGL 1.3 | GPU externa com OpenGL 4.3 |
| Memória | – | 16 GB ou mais |
| CPU | – | 8+ núcleos (para cálculos dinâmicos) |

**Windows on ARM (nativo, experimental)** : Um pacote portável ARM64 nativo experimental (`ReciPro-v.X_arm64.zip`, self-contained — sem necessidade de instalar o .NET Runtime) está disponível na [página de releases](https://github.com/seto77/ReciPro/releases/latest). Os pacotes x64 regulares também são executados no Windows ARM64 sob a emulação integrada. Veja [Solução de problemas](troubleshooting.md#windows-on-arm) para notas de configuração e limitações.

**macOS (não oficial)** : O ReciPro oferece suporte oficial apenas ao Windows, mas há relatos de que o pacote ZIP portável **win-x64** é executado no macOS (Apple Silicon) usando o wrapper Wine Sikarugir combinado com o driver OpenGL Mesa3D. Um guia de configuração publicado por um usuário está disponível em <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>. Observe que esse caminho não é oficialmente suportado e que alguns símbolos (Å, sobrescritos, setas) podem ser exibidos incorretamente. O ZIP ARM64 **não** é executado em macOS + Wine, e o caminho atual via x64 + Rosetta 2 deve deixar de funcionar a partir do macOS 28 (outono de 2027) — veja [Solução de problemas](troubleshooting.md#mac-linux) para detalhes.

## Como usar este manual

Este manual no GitHub Pages é atualmente a fonte de verdade. Use a navegação à esquerda para navegar por capítulo ou use a busca no cabeçalho para encontrar o nome de uma função ou um rótulo da interface. Os antigos manuais em PDF são mantidos para referência de arquivo.

* **PDF arquivado (inglês):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf>
* **PDF arquivado (japonês):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf>

## Início rápido
1. Baixe e instale a partir de [Releases](https://github.com/seto77/ReciPro/releases/latest).
2. Selecione um cristal da lista integrada (~80 cristais). Você também pode importar arquivos CIF ou usar o [CSManager](https://github.com/seto77/CSManager).
3. Acesse as funções pelo painel à direita: Visualizador de estrutura, Estereonete, Simulador de difração, Simulação HRTEM, etc.
4. Gire o cristal arrastando o mouse ou inserindo índices de eixo de zona/plano.

## Referência
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). <https://doi.org/10.1107/S1600576722000139>

## Licença
O ReciPro é distribuído sob a [Licença MIT](https://github.com/seto77/ReciPro/blob/master/LICENSE.md).

# 부록 A1.2. 회절 시뮬레이션을 위한 좌표계

<!-- 260526Cl: 図(Coordinates4-5)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

**회절 시뮬레이터** 기능은 검출기에 기록된 회절 패턴을 시뮬레이션합니다. 검출기는 시료로부터 일정한 거리에 놓인 유한한 픽셀 평면이며, 입사빔에 대해 기울어질 수 있습니다. 이를 정확하게 재현하려면 검출기와 시료 사이의 기하학적 관계, 그리고 검출기의 픽셀 크기와 픽셀 수가 필요합니다. 기본(방향) 좌표계에 대해서는 [A1.1. 기본 좌표계 및 결정 방향](1-orientation.md)을 참조하십시오.

!!! note "Z와 Y는 방향 좌표계와 다릅니다"
    검출기 좌표계에서 <span class="rp-steel">$Z$</span>는 빔에 평행하고 <span class="rp-steel">$Y$</span>는 아래쪽을 가리킵니다. 이는 빔이 <span class="rp-blue">$-Z$</span> 방향을 따르고 <span class="rp-green">$Y$</span>가 위쪽을 가리키는 방향 좌표계와 다릅니다. 검출기 좌표계는 통상적인 영상/검출기 관례(원점은 좌측 상단, <span class="rp-steel">$Y$</span>는 아래쪽으로 증가)를 따릅니다.

## 회전 전 (검출기가 빔에 수직)

![빔에 수직인 검출기의 검출기 좌표계](../../../assets/references/Coordinates4.png){width=500px}

세 가지 좌표계가 정의됩니다:

- <span class="rp-steel">**실제 좌표** ($X$, $Y$, $Z$)</span> : <span class="rp-steel">**시료**</span>를 원점으로 하는 mm 단위의 3D 직교 좌표. <span class="rp-steel">$Z$</span>는 빔에 평행하며, <span class="rp-steel">$Z$</span>를 따라 바라보면 <span class="rp-steel">$X$</span>는 오른쪽, <span class="rp-steel">$Y$</span>는 아래쪽을 가리킵니다. 검출기가 빔에 수직일 때 <span class="rp-steel">$X$ / $Y$</span>는 <span class="rp-brown">$X'$ / $Y'$</span>에 평행합니다.
- <span class="rp-brown">**검출기 좌표** ($X'$, $Y'$)</span> : <span class="rp-brown">**foot**</span>을 원점으로 하는 검출기 평면상의 mm 단위 2D 좌표. <span class="rp-brown">$X'$ / $Y'$</span>는 검출기에서 오른쪽 / 아래쪽을 가리키며 <span class="rp-cyan">$X''$ / $Y''$</span>에 평행합니다.
- <span class="rp-cyan">**픽셀 좌표** ($X''$, $Y''$)</span> : 검출기의 <span class="rp-cyan">**좌측 상단 모서리**</span>를 원점으로 하여 검출기의 픽셀 행과 열을 따르는 픽셀 단위의 2D 좌표.

검출기가 빔에 수직일 때 <span class="rp-brown">**foot**</span>과 <span class="rp-red">**direct spot**</span>은 일치하며, <span class="rp-red">**Camera length 1**</span>은 <span class="rp-brown">**Camera length 2**</span>와 같습니다.

## 회전 후 (기울어진 검출기)

![기울어진 검출기의 검출기 좌표계](../../../assets/references/Coordinates5.png){width=500px}

검출기의 기울기는 두 가지 매개변수로 기술됩니다:

| 매개변수 | 설명 |
|-----------|-------------|
| <span class="rp-grass">$\varphi$</span> | <span class="rp-grass">회전축</span>의 방향 — <span class="rp-steel">$XY$</span> 평면(<span class="rp-steel">$Z$</span> = 0)에서 측정한 <span class="rp-steel">$X$</span>축으로부터의 각도 |
| <span class="rp-grass">$\tau$</span> | 그 축을 중심으로 한 회전각 (오른나사) |

검출기가 일단 기울어지면:

- <span class="rp-red">**direct spot**</span>과 <span class="rp-brown">**foot**</span>은 더 이상 일치하지 않습니다.
- <span class="rp-red">**Camera length 1** ($C_1$)</span> = <span class="rp-steel">시료</span>로부터 <span class="rp-red">direct spot</span>까지의 거리.
- <span class="rp-brown">**Camera length 2** ($C_2$)</span> = <span class="rp-steel">시료</span>로부터 <span class="rp-brown">foot</span>까지의 거리.
- <span class="rp-brown">**검출기 좌표**</span>의 원점은 <span class="rp-brown">**foot**</span>에 유지되고, <span class="rp-cyan">**픽셀 좌표**</span>의 원점은 <span class="rp-cyan">**좌측 상단 모서리**</span>에 유지됩니다.
- <span class="rp-steel">$X$ / $Y$</span> 방향은 더 이상 <span class="rp-brown">$X'$ / $Y'$</span>와 일치하지 않습니다.

## 매개변수 용어집

| 용어 | 정의 |
|------|------------|
| <span class="rp-steel">**시료 (Sample)**</span> | 입사빔을 산란시키는 물질; 실제 좌표의 원점 |
| <span class="rp-steel">**실제 좌표** ($X$, $Y$, $Z$)</span> | 실험 장치의 3D 좌표 (mm); 원점은 시료, <span class="rp-steel">$Z$</span>는 항상 빔에 평행 |
| <span class="rp-red">**Direct spot**</span> | 입사빔과 검출기의 교점 |
| <span class="rp-brown">**Foot**</span> | 시료로부터 검출기 평면에 내린 수선의 발; 검출기 좌표의 원점. 검출기가 빔에 수직일 때만 direct spot과 일치합니다. 오버레이 이미지 모드에서는 foot의 위치를 픽셀 좌표로 설정합니다 |
| <span class="rp-brown">**검출기 좌표** ($X'$, $Y'$)</span> | 검출기 평면상의 2D 좌표 (mm); 원점은 foot |
| <span class="rp-cyan">**픽셀 좌표** ($X''$, $Y''$)</span> | 검출기 평면상의 2D 좌표 (픽셀); 원점은 좌측 상단 모서리 |
| <span class="rp-red">**Camera length 1** ($C_1$)</span> | 시료로부터 direct spot까지의 거리 (mm) |
| <span class="rp-brown">**Camera length 2** ($C_2$)</span> | 시료로부터 foot까지의 거리 (mm) |
| **Pixel size** | 정사각형 픽셀 하나의 한 변 길이 (mm); 정사각형 픽셀만 지원됩니다 |
| **Detector width / height** | 가로 / 세로 방향의 픽셀 수 |

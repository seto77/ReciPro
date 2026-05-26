# 付録 A3. Bloch波法による計算の詳細

!!! info "準備中"
    この付録は執筆中です。ReciProがBloch波法でおこなう動力学回折計算の**実際の手順**を、順を追って解説する予定です。概念的な概要は [付録 A2. Bloch波法による動力学計算の概要](a2-bloch-wave-method.md) を参照してください。

## 記載予定の内容

- 結晶ポテンシャルとそのフーリエ係数 $V_{\mathbf g}$・$U_{\mathbf g}$
- 構造行列 $\mathbf{A}$ と固有値問題
- 固有値 $\gamma_j$ と固有ベクトル（Bloch波の振幅）$C^{(j)}$
- 境界条件と出射波の振幅
- 回折強度 $I_{\mathbf g}(\mathbf k) = \left| \sum_j C_{\mathbf g}^{(j)} C_0^{(j)} \exp(2\pi i\,\gamma_j t) \right|^2$
- 吸収・熱散漫散乱（虚ポテンシャル $V'_{\mathbf g}$）
- 数値ソルバー（Eigen / MKL）と性能

## 関連項目

- [付録 A2. Bloch波法による動力学計算の概要](a2-bloch-wave-method.md)
- [7.4 CBEDシミュレーション](../7-diffraction-simulator/4-cbed-simulation.md)
- [7. 回折シミュレータ](../7-diffraction-simulator/index.md)

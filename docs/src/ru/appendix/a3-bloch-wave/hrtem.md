# Формирование HRTEM-изображения

HRTEM-изображение формируется из волновой функции на выходной поверхности — коэффициентов пропускания $T_{\mathbf g}$, полученных из [динамического ядра](calculation.md), — путём прохождения её через объективную линзу. ReciPro предлагает две модели: быстрое **квазикогерентное** приближение и более строгую модель **перекрёстного коэффициента пропускания (TCC)**. См. также GUI-страницу [Симулятор HRTEM](../../9-hrtem-stem-simulator/1-hrtem-simulation.md).

---

## Обозначения

| Symbol | Значение |
|--------|---------|
| $\mathbf R$ | X–Y-компонента в реальном пространстве (плоскость изображения) |
| $\mathbf K$ | X–Y-компонента волнового вектора падающего пучка |
| $\mathbf G, \mathbf H$ | X–Y-компоненты векторов обратной решётки |
| $\mathbf u$ | пространственная частота (например, $\mathbf K+\mathbf G$) |
| $\chi(\mathbf u)$ | функция аберрации линзы |
| $A(\mathbf u)$ | функция апертуры объектива |
| $\Delta f$ | значение дефокусировки |
| $C_s$ | коэффициент сферической аберрации |
| $C_c$ | коэффициент хроматической аберрации |
| $\beta$ | полуугол освещения (конечный размер источника) |
| $\Delta E$ | ширина $1/e$ флуктуаций энергии электрона |
| $\Delta_0$ | ширина $1/e$ разброса дефокусировки (гауссова), $\Delta_0 = C_c\,\Delta E / E$ |

---

## Функция аберрации линзы и апертура

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{inside the objective aperture})\\[2pt] 0 & (\mathbf u\ \text{outside the objective aperture})\end{cases}$$

---

## Квазикогерентная модель

Быстрое приближение: каждый дифрагированный пучок модулируется передачей линзы и затухает под действием огибающих когерентности, после чего суммируется когерентно.

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

с **временно́й** и **пространственной огибающими когерентности**

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

---

## Модель перекрёстного коэффициента пропускания (TCC)

Строгое описание частичной когерентности: каждая пара пучков $(\mathbf g, \mathbf h)$ интерферирует через перекрёстный коэффициент пропускания.

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

со **смешанными** огибающими когерентности

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

В пределе $\mathbf u' \to \mathbf u$ TCC сводится к приведённым выше квазикогерентным огибающим.

---

## Снижение вычислительных затрат модели TCC

Двойная сумма модели TCC вычисляет $\mathrm{TCC}$ один раз для каждой пары пучков, поэтому она затратна. Поскольку интенсивность изображения $I(\mathbf R)$ вещественна, затраты можно примерно уменьшить вдвое.

Во-первых, пучки вне апертуры объектива ($A(\mathbf K+\mathbf G)=0$) не дают вклада, поэтому достаточно суммировать **только по пучкам внутри апертуры ($A=1$)**.

Во-вторых, TCC является эрмитовым,

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

($A$ вещественна; $E_c, E_s$ — вещественные функции, инвариантные относительно $\mathbf u\leftrightarrow\mathbf u'$; фазовый член $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ комплексно сопрягается). Вместе с $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$ и $T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$ члены $(\mathbf g,\mathbf h)$ и $(\mathbf h,\mathbf g)$ комплексно сопряжены друг другу, так что их сумма равна удвоенной вещественной части:

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

Таким образом, двойная сумма сводится к диагонали плюс верхний треугольник (одна сторона, после того как пучкам назначен произвольный порядок), что вдвое уменьшает число вычислений $\mathrm{TCC}$:

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

Для диагонального члена $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$, то есть $|T_{\mathbf g}|^2$ внутри апертуры.

Кроме того, фазовый множитель $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ многократно принимает одно и то же значение в пределах этой суммы. Сохранение и повторное использование этих значений дополнительно ускоряет вычисление.

---

## См. также

- [Динамический расчёт (общее ядро)](calculation.md) — общее ядро блоховских волн и коэффициенты пропускания $T_{\mathbf g}$
- [Приложение A3. Динамическая дифракция методом блоховских волн](index.md)
- [9.1. Моделирование HRTEM](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)

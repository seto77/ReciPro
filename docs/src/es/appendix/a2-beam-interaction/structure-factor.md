# Factor de estructura

El factor de dispersión atómico describe un solo átomo; el **factor de estructura** describe cómo todos los átomos de la celda elemental dispersan *en conjunto*. Es la magnitud que tabula la pestaña **Reflections** (`F_real`, `F_inv`, $\lvert F\rvert$, $F^2$), y constituye el puente entre la física atómica de la página anterior y las intensidades difractadas.

=== "X-ray"
    ![Reflections — X-ray](../../../assets/cap-es-auto/FormBeamInteraction-xray-reflections.png)

=== "Electron"
    ![Reflections — electron](../../../assets/cap-es-auto/FormBeamInteraction-electron-reflections.png)

=== "Neutron"
    ![Reflections — neutron](../../../assets/cap-es-auto/FormBeamInteraction-neutron-reflections.png)

---

## Interferencia sobre la celda elemental

El factor de estructura de la reflexión $\mathbf g = (hkl)$ es la suma coherente de los factores atómicos, cada uno ponderado por la fase derivada de la posición fraccionaria $\mathbf r_j = (x_j,y_j,z_j)$ del átomo:

$$F_{\mathbf g} = \sum_{j} o_j\, f_j(s,E)\, T_j(\mathbf g)\, \exp\!\left(-2\pi i\,(h x_j + k y_j + l z_j)\right).$$

- $o_j$ : **ocupación** del sitio (fraccionaria, para ocupación parcial o mixta).
- $f_j(s,E)$ : el factor de dispersión atómico del átomo $j$ para el haz actual — $f_0+f'-if''$ para rayos X en la [convención de fase](index.md#phase-convention) de ReciPro, $f_e$ para electrones, $b$ para neutrones.
- $T_j(\mathbf g)$ : el factor de Debye–Waller (más abajo).
- La fase $-2\pi i$ sigue la [convención](index.md#phase-convention) de ReciPro.

La intensidad es el módulo al cuadrado,

$$I_{\mathbf g} \;\propto\; \lvert F_{\mathbf g}\rvert^2 = F_\text{real}^2 + F_\text{inv}^2 ,$$

que es la columna $F^2$ de la tabla. `F_real` y `F_inv` son las partes real e imaginaria del factor de estructura complejo. Incluso con factores atómicos puramente reales, $F_{\mathbf g}$ es en general complejo para una estructura no centrosimétrica (o un origen desplazado); la dispersión anómala de rayos X (con $f$ complejo) y las longitudes de dispersión de neutrones complejas añaden una contribución imaginaria adicional. `F_inv` se anula para *todas* las reflexiones únicamente cuando la estructura es centrosimétrica con el origen en un centro de simetría y todos los factores son reales.

---

## El factor de Debye–Waller

Los átomos vibran en torno a sus posiciones de equilibrio, difuminando la densidad de dispersión y reduciendo los factores a ángulos altos. Para movimiento isótropo,

$$T_j = \exp\!\left(-B_j\, s^2\right), \qquad B_j = 8\pi^2\langle u_j^2\rangle,$$

donde $\langle u_j^2\rangle$ es el desplazamiento cuadrático medio a lo largo de la dirección de dispersión y $B_j$ es el parámetro de desplazamiento isótropo (Å²). El movimiento anisótropo generaliza esto a

$$T_j = \exp\!\left(-2\pi^2\,\mathbf g^{\mathsf T}\!\mathbf U_j\,\mathbf g\right),$$

con $\mathbf U_j$ el tensor de desplazamiento y $\mathbf g$ el vector de la red recíproca ($|\mathbf g|=1/d$, no $Q=2\pi\lvert\mathbf g\rvert$). Para un sólido de Debye, el desplazamiento cuadrático medio es a su vez una función de la temperatura $T$, la masa atómica $M$ y la temperatura de Debye $\Theta_D$,

$$\langle u^2\rangle = \frac{3\hbar^2}{M k_B \Theta_D}\left[\frac14 + \left(\frac{T}{\Theta_D}\right)^2\!\int_0^{\Theta_D/T}\frac{x}{e^x-1}\,dx\right],$$

de modo que $B$ aumenta con la temperatura y disminuye para los átomos pesados. ReciPro emplea directamente los $B_j$ tabulados o introducidos en lugar de calcularlos. Dado que $T_j$ multiplica al factor de dispersión, la pestaña **Scattering factors** puede aplicar el mismo amortiguamiento $e^{-Bs^2}$ a las curvas representadas. El amortiguamiento crece con la temperatura y con $s$, razón por la cual la dispersión térmica difusa (intensidad sustraída de los haces de Bragg coherentes y redistribuida en un fondo difuso) alimenta el potencial absortivo en la teoría dinámica ([Apéndice A3](../a3-bloch-wave/index.md)).

---

## Extinciones: sistemáticas frente a accidentales

Una reflexión puede estar **ausente** por dos motivos distintos:

- **Ausencias sistemáticas (de grupo espacial).** El centrado de la red y los elementos de simetría con una componente traslacional (ejes helicoidales, planos de deslizamiento) hacen desaparecer clases enteras de reflexiones de forma *exacta*, para todo cristal de ese grupo espacial, con independencia del contenido atómico. Estas son las reglas que subyacen a **Hide prohibited planes**.
- **Casi-extinciones accidentales.** Cuando las contribuciones atómicas se cancelan por casualidad para una estructura concreta, la intensidad es pequeña pero no está prohibida por simetría, y puede reaparecer si cambian la composición o las posiciones. Estas *no* se eliminan mediante las reglas de extinción.

Una ausencia sistemática es una cancelación de fase entre las copias relacionadas por simetría de la celda. Para las traslaciones de centrado $\mathbf t_\alpha$, el factor de estructura lleva un factor común

$$F_{\mathbf g} \propto \sum_\alpha e^{-2\pi i\,\mathbf g\cdot\mathbf t_\alpha},$$

que es cero para ciertos $hkl$. Para el centrado en el cuerpo ($\mathbf t = \tfrac12,\tfrac12,\tfrac12$),

$$1 + e^{-\pi i (h+k+l)} = 0 \quad\Longleftrightarrow\quad h+k+l \ \text{odd}.$$

Las ausencias sistemáticas más comunes son:

| Elemento de simetría | Condición de ausencia | Reflexiones afectadas |
|---|---|---|
| $I$ (centrado en el cuerpo) | $h+k+l$ impar | todas $hkl$ |
| $F$ (centrado en las caras) | $h,k,l$ de paridad mixta | todas $hkl$ |
| $C$ (centrado en C) | $h+k$ impar | todas $hkl$ |
| eje helicoidal $2_1$ $\parallel b$ | $k$ impar | $0k0$ |
| plano de deslizamiento $a$ $\perp b$ | $h$ impar | $h0l$ |
| plano de deslizamiento $c$ $\perp b$ | $l$ impar | $h0l$ |

Las condiciones de centrado se aplican a toda reflexión; las condiciones de ejes helicoidales y planos de deslizamiento se aplican únicamente a la fila axial o zona correspondiente, que es precisamente lo que las hace diagnósticas del grupo espacial.

---

## La ley de Friedel y su ruptura

Para una estructura con factores de dispersión reales (no resonantes), conjugar la suma e invertir el signo de $\mathbf g$ muestra directamente que (suprimiendo los pesos reales $o_j T_j$ por claridad)

$$F_{-\mathbf g} = \sum_j f_j\, e^{+2\pi i\,\mathbf g\cdot\mathbf r_j} = \left(\sum_j f_j\, e^{-2\pi i\,\mathbf g\cdot\mathbf r_j}\right)^{*} = F_{\mathbf g}^{*}, \qquad\text{hence}\qquad \lvert F_{hkl}\rvert = \lvert F_{\bar h\bar k\bar l}\rvert \quad\text{(Friedel's law).}$$

La difracción aparece entonces como centrosimétrica aunque el cristal no lo sea. **La dispersión anómala puede romper esto.** Escribiendo el factor de estructura como una parte normal (que se conjuga limpiamente) más una parte anómala, $F_{\mathbf g} = A_{\mathbf g} - i B_{\mathbf g}$ y $F_{-\mathbf g} = A_{\mathbf g}^{*} - i B_{\mathbf g}^{*}$ en la convención $f = f_0 + f' - i f''$ de ReciPro, la **diferencia de Bijvoet** es

$$\lvert F_{\mathbf g}\rvert^2 - \lvert F_{-\mathbf g}\rvert^2 = -4\,\operatorname{Im}\!\left(A_{\mathbf g}\, B_{\mathbf g}^{*}\right),$$

no nula solo cuando la parte normal y la anómala tienen fases distintas — es decir, cuando dispersores anómalos químicamente diferentes ocupan sitios no centrosimétricos. (La diferencia se anula para una estructura centrosimétrica, un único elemento o cualquier caso en que todos los átomos lleven el mismo factor complejo.) Esto es lo que permite determinar la estructura absoluta (quiralidad) de un cristal no centrosimétrico, y es la razón física por la que ReciPro informa de un `F_inv` no nulo y de valores $\lvert F\rvert$ distintos para los pares de Friedel una vez que se elige una energía de rayos X cercana a un borde.

---

## Del factor de estructura a la intensidad de polvo

Activar **Powder Diffraction Intensities (Bragg–Brentano)** convierte $\lvert F\rvert^2$ en una intensidad de polvo relativa al incorporar la geometría de un policristal orientado al azar:

$$I_{hkl} \;\propto\; m_{hkl}\, \lvert F_{hkl}\rvert^2\, L p(\theta),$$

- $m_{hkl}$ : **multiplicidad** — el número de planos equivalentes por simetría que se solapan al mismo $2\theta$ (la columna *Multi.* de la tabla).
- $Lp(\theta)$ : el factor de **Lorentz–polarización** para la óptica Bragg–Brentano, $Lp = \dfrac{1+\cos^2 2\theta}{\sin^2\theta\,\cos\theta}$, que potencia fuertemente los picos a ángulos bajos.

Dado que en este modo los planos equivalentes se fusionan en una sola línea, ReciPro fuerza además *Hide equivalent planes* y *Hide prohibited planes*.

---

## Véase también

- [Factores de dispersión atómicos](scattering-factor.md) — los $f_j$ que entran en la suma.
- [Atenuación y transporte](attenuation-transport.md) — qué le ocurre al haz entre eventos de dispersión.
- [3. Interacción del haz → pestaña Reflections](../../3-beam-interaction.md#reflections-tab)
- [Apéndice A3. Difracción dinámica](../a3-bloch-wave/index.md) — cuando $\lvert F\rvert^2$ (cinemática) ya no es suficiente.

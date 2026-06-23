# Factores de dispersión atómica

El **factor de dispersión atómica** (o *factor de forma*) mide la intensidad con la que un solo átomo dispersa el haz incidente en función de la variable de dispersión $s=\sin\theta/\lambda$. Las tres radiaciones interactúan con partes completamente distintas del átomo, de modo que sus factores de dispersión tienen magnitudes, unidades y dependencia angular diferentes. Esta es la razón principal por la que la pestaña **Scattering factors** se ve tan distinta entre los haces de rayos X, de electrones y de neutrones.

=== "X-ray"
    ![Factores de dispersión — rayos X](../../../assets/cap-es-auto/FormBeamInteraction-xray-scattering.png)

=== "Electron"
    ![Factores de dispersión — electrón](../../../assets/cap-es-auto/FormBeamInteraction-electron-scattering.png)

=== "Neutron"
    ![Factores de dispersión — neutrón](../../../assets/cap-es-auto/FormBeamInteraction-neutron-scattering.png)

---

## Rayos X — dispersión por la nube electrónica

Los rayos X son dispersados por los **electrones** del átomo. Un solo electrón libre dispersa con la sección eficaz diferencial clásica de **Thomson**, determinada por el radio clásico del electrón $r_e = e^2/(4\pi\varepsilon_0 m_e c^2) \approx 2.82\times10^{-5}\ \text{Å}$:

$$\left(\frac{d\sigma}{d\Omega}\right)_e = r_e^2\,\frac{1+\cos^2 2\theta}{2}.$$

Los electrones del átomo están distribuidos en el espacio con una densidad numérica $\rho_e(\mathbf r)$, y el factor de dispersión atómica es la **transformada de Fourier** de esa densidad. La sección eficaz atómica es entonces la sección eficaz de un solo electrón escalada por $|f_0|^2$:

$$f_0(\mathbf Q) = \int \rho_e(\mathbf r)\, e^{\,i\mathbf Q\cdot\mathbf r}\, d^3r ,
\qquad
\left(\frac{d\sigma}{d\Omega}\right)_\text{atom} = r_e^2\,\frac{1+\cos^2 2\theta}{2}\,|f_0(s)|^2 .$$

- En la dirección hacia adelante ($s\to 0$) todos los electrones dispersan en fase, de modo que $f_0(0) = Z$, el número atómico. El factor se expresa en **unidades electrónicas** (múltiplos de la amplitud de Thomson — la segunda ecuación de arriba lo hace explícito).
- A medida que $s$ aumenta, la dispersión desde distintas partes de la nube se desfasa y $f_0(s)$ decae. Una distribución electrónica difusa (externa, de valencia) hace que $f_0$ caiga rápidamente; los electrones del núcleo fuertemente ligados siguen contribuyendo hasta valores altos de $s$.

En la práctica $f_0(s)$ se tabula como una suma de gaussianas (la forma analítica de **Waasmaier–Kirfel** que usa ReciPro, una extensión de las tablas más antiguas de Cromer–Mann),

$$f_0(s) = \sum_{i} a_i\, e^{-b_i s^2} + c ,$$

que es lo que ReciPro evalúa para la curva. Los coeficientes están tabulados para $s$ en Å⁻¹, de modo que cada $b_i$ tiene unidades de Å²; ReciPro lleva $s^2$ internamente en nm⁻² y aplica la conversión por un factor de 100 señalada en el [índice](index.md).

### Dispersión anómala (resonante)

La imagen de la transformada de Fourier supone que los electrones dispersan como si fueran libres. Cuando la energía del fotón se aproxima a un **borde de absorción**, los electrones ligados responden de forma resonante y aparecen dos términos de corrección dependientes de la energía:

$$f(s,E) = f_0(s) + f'(E) + i\,f''(E) \qquad \text{(textbook, } e^{+i\phi}\ \text{convention).}$$

- $f'(E)$ : corrección de dispersión real (reduce el número efectivo de electrones cerca de un borde).
- $f''(E)$ : parte imaginaria, máxima justo por encima de un borde.
- Ambas están vinculadas por las relaciones de **Kramers–Kronig**, de modo que un pico en la absorción ($f''$) viene acompañado de una oscilación dispersiva en $f'$.

Estos no son parámetros libres. La causalidad (Kramers–Kronig) liga $f'$ a $f''$, y el **teorema óptico** liga $f''$ directamente a la sección eficaz de fotoabsorción:

$$f'(E) = \frac{2}{\pi}\,\mathcal{P}\!\!\int_0^\infty \frac{E'\,f''(E')}{E'^2 - E^2}\,dE',
\qquad
f''(E) = \frac{\sigma_\text{abs}(E)}{2\,r_e\,\lambda}.$$

Aquí $\sigma_\text{abs}$ es esencialmente la parte de **fotoabsorción** de la atenuación (no los términos de Rayleigh/Compton) — la misma estructura de bordes que se ve en la página [Atenuación y transporte](attenuation-transport.md).

ReciPro evalúa $f'$ y $f''$ a la energía actual con la biblioteca **xraylib** incluida y los lista en la tabla (con $f'' > 0$). Importan dos cuestiones de signo. Primero, xraylib devuelve $F_{ii}$ con el signo opuesto a la convención cristalográfica, de modo que ReciPro lo niega para reportar un **$f''$ positivo**. Segundo, bajo la convención de fase $\exp(-2\pi i\,\mathbf g\cdot\mathbf r)$ de ReciPro, el factor complejo que realmente entra en el factor de estructura es $f_0 + f' - i f''$ — el $+i f''$ escrito arriba pertenece a la convención opuesta ($e^{+2\pi i}$). Por eso `F_inv` (la parte imaginaria del factor de estructura) se vuelve distinto de cero cerca de un borde — véase [Factor de estructura](structure-factor.md).

---

## Electrones — dispersión por el potencial electrostático

Un electrón rápido tiene carga, por lo que es dispersado por el **potencial electrostático** $V(\mathbf r)$ del átomo — la combinación del núcleo positivo y la nube electrónica negativa. El factor de dispersión electrónica $f_e$ es por tanto la transformada de Fourier del potencial, lo que mediante la ecuación de Poisson lo vincula al factor de rayos X. El resultado es la **relación de Mott–Bethe**:

$$f_e(s) = C_\text{MB}\,\frac{Z - f_0(s)}{s^2} \;\;\propto\; \frac{Z - f_X(Q)}{Q^2}.$$

El prefactor $C_\text{MB}$ se construye a partir de constantes fundamentales y depende del sistema de unidades y de si se usa $s$ o $Q$. ReciPro no evalúa esta relación directamente — usa las formas ajustadas de Peng / Kirkland / 8 gaussianas que se indican abajo — por lo que se da aquí para la comprensión física más que para el cálculo. Escrita con las constantes (para $s$ y $f_e$ en Å),

$$f_e(s)\,[\text{Å}] = \frac{m_e e^2}{8\pi\varepsilon_0 h^2}\,\frac{Z - f_0(s)}{s^2} \simeq 0.023934\,\frac{Z - f_0(s)}{s^2}, \qquad s\ \text{in Å}^{-1},$$

con un $\times 0.1$ adicional cuando ReciPro reporta $f_e$ en nm, y un factor relativista $\gamma$ adicional (abajo) en el potencial dinámico.

La física está en el numerador $Z - f_0$: el electrón ve la **diferencia** entre la carga nuclear $Z$ y la nube electrónica de apantallamiento $f_0$, es decir, el potencial atómico neto.

- **Magnitud.** Debido al factor $1/s^2$, $f_e$ está fuertemente concentrado hacia ángulos pequeños y es mucho mayor (en sus propias unidades) y más dirigido hacia adelante que $f_0$. Por eso la difracción de electrones está dominada por reflexiones de orden bajo y por eso la dispersión dinámica (múltiple) importa — véase [Apéndice A3](../a3-bloch-wave/index.md).
- **Límite de ángulo pequeño.** Para un átomo *neutro* tanto $Z-f_0\to 0$ como $s^2\to 0$, de modo que $f_e(0)$ es finito (un límite $0/0$ fijado por el radio atómico cuadrático medio). Para un **ion** la nube ya no cancela $Z$ y la cola de Coulomb de largo alcance hace que $f_e$ diverja cuando $s\to 0$; los factores electrónicos iónicos tabulados deben tratarse con cuidado en los ángulos más pequeños.
- **Corrección relativista.** A energías de TEM la masa y la longitud de onda del electrón son relativistas. La longitud de onda usa la forma relativista $\lambda = h/\sqrt{2 m_0 e U\,(1 + e U/2 m_0 c^2)}$, y el potencial de interacción lleva el factor relativista $\gamma = 1 + eU/m_0c^2$. ReciPro aplica esta corrección al formar el potencial dinámico.

ReciPro ofrece tres parametrizaciones de $f_e(s)$:

- **Peng** : un ajuste de cinco gaussianas, $f_e(s)=\sum_i a_i e^{-b_i s^2}$, cómodo y ampliamente usado para la dispersión elástica de electrones.
- **Kirkland** : un ajuste mixto lorentziano + gaussiano, $f_e(q)=\sum_i \dfrac{a_i}{q^2+b_i} + \sum_i c_i\,e^{-d_i q^2}$. **Su variable independiente es $q = 2s = 1/d$, no $s$** — una fuente frecuente de errores por un factor de dos al comparar modelos ($q$ en Å⁻¹, con los coeficientes ajustados $a_i,b_i,c_i,d_i$ en las unidades correspondientes).
- **8-Gaussians** : un ajuste de ocho términos válido sobre un rango más amplio de $s$.

**Elegir uno.** Las tres ajustan el mismo $f_e(s)$ subyacente y coinciden estrechamente a $s$ bajo; difieren principalmente en el rango y en cómo se representa el núcleo atómico. **Peng** (átomos neutros e iones comunes, exacto hasta $s\approx2\text{–}6$ Å⁻¹) es el valor predeterminado habitual para los factores de estructura de SAED/CBED; **Kirkland** se extiende a $s$ más altos con un término lorentziano de núcleo, adecuado para HRTEM/STEM (recuerda $q=2s$); **8-Gaussians** es para reflexiones que alcanzan valores muy altos de $s$. Para un elemento ligero las tres son casi indistinguibles; las diferencias aparecen para elementos pesados a ángulo grande.

---

## Neutrones — dispersión por el núcleo

Los neutrones térmicos no tienen carga e interactúan con la materia principalmente a través de la **fuerza nuclear fuerte**, cuyo alcance (femtómetros) es totalmente despreciable comparado con la longitud de onda del neutrón (ångströms). La interacción se representa mediante el **pseudopotencial de Fermi**, una fuente puntual cuya intensidad es la longitud de dispersión $b$:

$$V(\mathbf r) = \frac{2\pi\hbar^2}{m_n}\,b\,\delta(\mathbf r)
\qquad\Longrightarrow\qquad
\frac{d\sigma}{d\Omega} = |b|^2 .$$

Como el dispersor es puntual, $b$ es **independiente de $s$** — no hay decaimiento de factor de forma, razón por la cual la pestaña **Scattering factors** no dibuja ninguna curva para los neutrones y muestra en su lugar una tabla de longitudes de dispersión.

- $b$ es una propiedad del **núclido**, no de la configuración electrónica. Varía de forma irregular de un elemento a otro (y entre isótopos), puede ser **negativa** (p. ej. ¹H, Ti, Mn), y no guarda ninguna relación monótona con $Z$. Esta es la base del contraste de neutrones (átomos ligeros cerca de pesados, marcado isotópico).
- **Coherente frente a incoherente.** Un elemento real es una mezcla de isótopos y estados de espín nuclear con $b$ diferentes. Separando $b = \langle b\rangle + \delta b$ se obtiene una parte coherente (del valor medio) y una parte incoherente (de la dispersión de valores):

$$\sigma_\text{coh} = 4\pi\,|\langle b\rangle|^2, \qquad \sigma_\text{inc} = 4\pi\big(\langle |b|^2\rangle - |\langle b\rangle|^2\big), \qquad \sigma_s = \sigma_\text{coh} + \sigma_\text{inc}.$$

  La parte coherente produce la difracción de Bragg (es lo que entra en el factor de estructura); la parte incoherente es un fondo plano e isótropo (grande para ¹H, la razón de la deuteración).

!!! note "Tabulated values"
    ReciPro lee $b_\text{coh}$ y las secciones eficaces de una tabla de núclidos en lugar de calcularlos. Para los núclidos resonantes el $\sigma_\text{coh}$ listado no tiene por qué ser igual al ingenuo $4\pi b^2$, así que los valores de la tabla son los que prevalecen. La dispersión magnética de neutrones (por espines electrónicos no apareados, que *sí* tiene un factor de forma dependiente de $s$) no se modela aquí.

---

## De un vistazo

| | X-ray | Electron | Neutron |
|---|---|---|---|
| Dispersado por | nube electrónica $\rho_e(\mathbf r)$ | potencial electrostático $V(\mathbf r)$ | núcleo (puntual) |
| Dependencia de $s$ | decae (FT de la nube) | $\propto (Z-f_0)/s^2$, fuertemente hacia adelante | ninguna ($b$ constante) |
| Valor hacia adelante | $f_0(0)=Z$ | finito (neutro) / divergente (ion) | $b$ |
| Dependencia de la energía | $f',f''$ cerca de los bordes | relativista $\lambda,\gamma$ | $\sigma_\text{abs}\propto 1/v$ (no $b$) |
| Orden de magnitud típico | $\propto Z$ | concentrado hacia adelante, crece con $Z$ | irregular, puede ser $<0$ |

---

## Véase también

- [Índice — geometría y la variable $s$](index.md)
- [Factor de estructura](structure-factor.md) — cómo se combinan estos factores sobre una celda elemental.
- [3. Interacción del haz → pestaña Scattering factors](../../3-beam-interaction.md#scattering-factors-tab)

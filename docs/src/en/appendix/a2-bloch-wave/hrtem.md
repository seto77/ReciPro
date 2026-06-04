# HRTEM Image Formation

The HRTEM image is formed from the exit-surface wavefunction — the transmission coefficients $T_{\mathbf g}$ obtained from the [dynamical core](calculation.md) — by passing it through the objective lens. ReciPro offers two models: the fast **quasi-coherent** approximation and the more rigorous **transmission cross coefficient (TCC)** model. See also the [HRTEM simulator](../../9-hrtem-stem-simulator/1-hrtem-simulation.md) GUI page.

---

## Symbols

| Symbol | Meaning |
|--------|---------|
| $\mathbf R$ | X–Y component in real space (image plane) |
| $\mathbf K$ | X–Y component of the incident wavevector |
| $\mathbf G, \mathbf H$ | X–Y components of reciprocal-lattice vectors |
| $\mathbf u$ | spatial frequency (e.g. $\mathbf K+\mathbf G$) |
| $\chi(\mathbf u)$ | lens aberration function |
| $A(\mathbf u)$ | objective-aperture function |
| $\Delta f$ | defocus value |
| $C_s$ | spherical aberration coefficient |
| $C_c$ | chromatic aberration coefficient |
| $\beta$ | illumination semi-angle (finite source size) |
| $\Delta E$ | $1/e$ width of the electron energy fluctuations |
| $\Delta_0$ | $1/e$ width of the defocus spread (Gaussian), $\Delta_0 = C_c\,\Delta E / E$ |

---

## Lens aberration function and aperture

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{inside the objective aperture})\\[2pt] 0 & (\mathbf u\ \text{outside the objective aperture})\end{cases}$$

---

## Quasi-coherent model

A fast approximation: each diffracted beam is modulated by the lens transfer and damped by coherence envelopes, then summed coherently.

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

with the **temporal** and **spatial coherence envelopes**

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

---

## Transmission cross coefficient (TCC) model

The rigorous treatment of partial coherence: every pair of beams $(\mathbf g, \mathbf h)$ interferes through the transmission cross coefficient.

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

with the **mixed** coherence envelopes

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

In the limit $\mathbf u' \to \mathbf u$ the TCC reduces to the quasi-coherent envelopes above.

---

## Reducing the cost of the TCC model

The double sum of the TCC model evaluates $\mathrm{TCC}$ once per pair of beams, so it is expensive. Because the image intensity $I(\mathbf R)$ is real, the cost can be roughly halved.

First, beams outside the objective aperture ($A(\mathbf K+\mathbf G)=0$) do not contribute, so it is sufficient to sum **only over the beams inside the aperture ($A=1$)**.

Next, the TCC is Hermitian,

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

($A$ is real; $E_c, E_s$ are real functions invariant under $\mathbf u\leftrightarrow\mathbf u'$; the phase term $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ is complex-conjugated). Together with $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$ and $T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$, the $(\mathbf g,\mathbf h)$ and $(\mathbf h,\mathbf g)$ terms are complex conjugates of each other, so their sum equals twice the real part:

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

The double sum therefore reduces to the diagonal plus the upper triangle (one side, once an arbitrary ordering is assigned to the beams), halving the number of $\mathrm{TCC}$ evaluations:

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

For the diagonal term $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$, i.e. $|T_{\mathbf g}|^2$ inside the aperture.

Furthermore, the phase factor $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ takes the same value many times within this sum. Storing and reusing these values accelerates the computation further.

---

## See also

- [Dynamical calculation (common core)](calculation.md) — the shared Bloch-wave core and the transmission coefficients $T_{\mathbf g}$
- [Appendix A2. Dynamical Diffraction by the Bloch-Wave Method](index.md)
- [9.1. HRTEM simulation](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)

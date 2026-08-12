# Dynamical calculation (common core)

ReciPro's diffraction and imaging simulators share a common **Bloch-wave (Bethe) dynamical-scattering core**, described on this page (crystal potential, Debye–Waller and absorption terms, the eigenvalue problem, transmission coefficients, and intensities). The method-specific protocols build on this core:

- [Parallel-beam SAED](#parallel-beam-saed)
- [HRTEM image formation](hrtem.md)
- [CBED](cbed.md)
- [STEM](stem.md)
- [EBSD](ebsd.md)

For the underlying theory (Schrödinger equation, Bloch's theorem, Bethe's dynamical equation, the eigenvalue problem, and the Ewald-sphere definitions), see [Appendix A3. Dynamical Diffraction by the Bloch-Wave Method](index.md).

---

## Constants

$$\gamma = \frac{m}{m_0} = 1 + \frac{e_0 E}{m_0 c^2}, \qquad \beta = \frac{v}{c} = \sqrt{1 - \left(\frac{m_0}{m}\right)^2} = \sqrt{1 - \gamma^{-2}}$$

- $\gamma$ : relativistic correction factor; $E$ : accelerating voltage; $m_0$, $m$ : rest and relativistic electron mass.
- $\Omega$ : unit-cell volume.
- $k_{vac}$ : wavenumber of the electron in vacuum.

---

## Crystal potential for elastic scattering

The Fourier coefficient of the crystal potential for elastic scattering, summed over the atoms $k$ at positions $\mathbf r_k$, is

$$U_{\mathbf g}^{C} = \gamma\,\frac{1}{\pi\Omega}\sum_k f_k(\mathbf g)\,\exp\!\left[2\pi i\,\mathbf g\cdot\mathbf r_k\right]T_k(\mathbf g, M_k)$$

where the **atomic scattering factor** uses a Gaussian parameterisation $(a_i, b_i)$,

$$f_k(\mathbf g) = \sum_i a_i\exp\!\left[-b_i\,\frac{|\mathbf g|^2}{4}\right]$$

and $T_k$ is the **Debye–Waller (temperature) factor**. For an isotropic temperature factor $M_k$,

$$T_k(\mathbf g, M_k) = \exp\!\left[-M_k\,\frac{|\mathbf g|^2}{4}\right]$$

and for an anisotropic atomic displacement tensor $\mathbf U$,

$$T_k(\mathbf g) = \exp\!\left[-2\pi\,\mathbf g^{t}\mathbf U\,\mathbf g\right]$$

with the quadratic form

$$\mathbf g^{t}\mathbf U\,\mathbf g = \begin{pmatrix} g_x & g_y & g_z\end{pmatrix}\begin{pmatrix} U_{11} & U_{12} & U_{13}\\ U_{12} & U_{22} & U_{23}\\ U_{13} & U_{23} & U_{33}\end{pmatrix}\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = g_x^2 U_{11} + g_y^2 U_{22} + g_z^2 U_{33} + 2\!\left(g_x g_y U_{12} + g_y g_z U_{23} + g_x g_z U_{13}\right)$$

The Cartesian components of $\mathbf g$ are obtained from the reciprocal basis vectors and the Miller indices:

$$\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = \begin{pmatrix} a_x^{*} & b_x^{*} & c_x^{*}\\ a_y^{*} & b_y^{*} & c_y^{*}\\ a_z^{*} & b_z^{*} & c_z^{*}\end{pmatrix}\begin{pmatrix} h\\ k\\ l\end{pmatrix} = \begin{pmatrix} h\,a_x^{*} + k\,b_x^{*} + l\,c_x^{*}\\ h\,a_y^{*} + k\,b_y^{*} + l\,c_y^{*}\\ h\,a_z^{*} + k\,b_z^{*} + l\,c_z^{*}\end{pmatrix}$$

!!! note
    The $U_{\mathbf g}$ values shown in the diffraction simulator's **Details** table are the raw values *before* the relativistic factor $\gamma$ is applied.

---

## Absorptive potential (thermal diffuse scattering)

The imaginary (absorption) potential that accounts for thermal diffuse scattering (TDS) is

$$U'_{g,h} = \gamma\,\frac{1}{\pi\Omega}\sum_k f'_k(\mathbf g,\mathbf h)\,\exp\!\left[2\pi i(\mathbf g-\mathbf h)\cdot\mathbf r_k\right]T_k(\mathbf g-\mathbf h, M_k)$$

with the **absorptive scattering factor**, an integral that convolves the elastic scattering factor $f_e$ over the Ewald sphere:

$$f'_k(\mathbf g,\mathbf h) = \frac{\gamma\,k_{vac}}{2}\oint_{|\mathbf K|=k_{vac}} f_{e,k}(s_-)\,f_{e,k}(s_+)\left[1-\exp\!\left\{M_k\left(\frac{|\mathbf g-\mathbf h|^2}{4}-s_-^2-s_+^2\right)\right\}\right]\mathrm d\Omega,
\qquad s_\mp=\frac{1}{2}\left|\mathbf K\mp\frac{\mathbf g-\mathbf h}{2}\right|$$

Here $\mathbf K$ is a wave vector running over the Ewald sphere and $\mathrm d\Omega$ is its solid-angle element. ReciPro evaluates this integral numerically by Gauss–Legendre quadrature (the integrand peaks sharply in the forward direction, so the polar angle is split into geometric intervals of the scattering-vector magnitude to resolve it). For the TDS collected by an annular STEM detector, or the backscattered TDS in EBSD, the **same integrand** is integrated over the detector's angular range (an annulus or the backward hemisphere) instead.

The elastic factor $f_{e,k}$ in the integrand is a two-part construction: at low $s$ it is the **Peng 5-Gaussian fit** from the [scattering-factor page](../a2-beam-interaction/scattering-factor.md); at high $s$ ($s \ge 2.5$ Å⁻¹) it is the **Mott–Bethe relation** evaluated directly with the Waasmaier–Kirfel X-ray factor $f_0(s)$, with a smooth blend between $1.5$ and $2.5$ Å⁻¹. The reason is that the Peng Gaussian sum decays much too fast beyond $s \gtrsim 2$ Å⁻¹ (Gaussian tails vanish exponentially, whereas the true $f_e$ has a $1/s^2$ tail), which underestimates backscattering by many orders of magnitude at $s$ of order $k_{vac}$ (about 40 Å⁻¹ at 200 kV). Two implementation details: $f_0(s)$ is clamped to $[0, Z]$ (some fitted forms diverge outside their fitting range), and the neutral-atom $f_0$ is used even for ionic entries (at high $s$ only the nuclear charge is seen, not the electron cloud).

!!! note
    When $f_e$ is a pure Gaussian sum, this integral has a closed-form solution as a double Gaussian sum, which earlier versions of ReciPro used as the absorptive scattering factor. No closed form exists for an $f_e$ with the high-$s$ tail, so it was replaced by the numerical quadrature, which has been verified to reproduce the closed form when the tail is disabled.

The $U^{C}$ and $U'$ coefficients are the entries of the structure matrix $\mathbf A$ in [Appendix A3](index.md).

---

## From the eigensolution to the diffracted intensity

Diagonalising the structure matrix (see [Appendix A3](index.md)) gives the eigenvalues $\lambda^{(j)}$ and the Bloch-wave amplitudes $C_{\mathbf g}^{(j)}$. The wave amplitudes on the exit surface — the **transmission coefficients** $T_{\mathbf g}$ — at specimen thickness $t$ are

$$\begin{pmatrix} T_0\\ T_g\\ T_h\\ \vdots\end{pmatrix}
= e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}
\begin{pmatrix} e^{\pi i P_0 t} & 0 & 0 & \cdots\\ 0 & e^{\pi i P_g t} & 0 & \cdots\\ 0 & 0 & e^{\pi i P_h t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} C_0^{(1)} & C_0^{(2)} & C_0^{(3)} & \cdots\\ C_g^{(1)} & C_g^{(2)} & C_g^{(3)} & \cdots\\ C_h^{(1)} & C_h^{(2)} & C_h^{(3)} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} e^{2\pi i\lambda^{(1)} t} & 0 & 0 & \cdots\\ 0 & e^{2\pi i\lambda^{(2)} t} & 0 & \cdots\\ 0 & 0 & e^{2\pi i\lambda^{(3)} t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} \alpha^{(1)}\\ \alpha^{(2)}\\ \alpha^{(3)}\\ \vdots\end{pmatrix}$$

or, component by component,

$$T_{\mathbf g} = e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}\; e^{\pi i P_g t}\sum_j C_{\mathbf g}^{(j)}\,e^{2\pi i\lambda^{(j)} t}\,\alpha^{(j)}$$

- $\alpha^{(j)}$ : the weighting (excitation) coefficients of each Bloch wave, fixed by the boundary condition at the entrance surface.
- $t$ : specimen thickness.

The diffracted intensity of beam $\mathbf g$ is then

$$I_{\mathbf g} = \left|T_{\mathbf g}\right|^2$$

---

## Parallel-Beam SAED Calculation { #parallel-beam-saed }

Ordinary SAED (selected-area electron diffraction) is treated as **parallel-beam diffraction** with a single incident direction. Unlike CBED, it does not scan many $\mathbf K$ points inside a convergent aperture. The current crystal orientation and accelerating voltage define one incident wavevector $\mathbf k_0$, and ReciPro evaluates the position and intensity of each reflection $\mathbf g$ for that condition.

The calculation can be organised as follows.

1. Use the crystal orientation, accelerating voltage, wavelength, camera length, and detector geometry to define the vacuum incident wavevector $\mathbf k_{vac}$ and the detector plane.
2. Apply the refraction correction from the mean inner potential $U_0$ and obtain the crystal reference wavevector $\mathbf k_0$.
3. Enumerate candidate reciprocal-lattice vectors $\mathbf g$ and evaluate their distance from the Ewald sphere through quantities such as $Q_g=|\mathbf k_0|^2-|\mathbf k_0+\mathbf g|^2$ and the excitation error $S_g$.
4. Calculate the intensity of each reflection using the selected intensity mode.
5. Project the direction of $\mathbf k_0+\mathbf g$ onto the detector plane and draw it as a diffraction spot.

ReciPro's SAED mode mainly offers the following intensity models.

| Mode | Calculation | Typical use |
|------|-------------|-------------|
| Only excitation error | Estimates intensity only from how close the reflection is to the Ewald sphere. Structure factors are not used. | Fast checks of spot positions and zone-axis geometry. |
| Kinematical + excitation error | Uses $\lvert F_{\mathbf g}\rvert^2$ together with excitation-error damping. Multiple scattering is not included. | Thin specimens, weak diffraction, and extinction-rule checks. |
| Dynamical theory | Uses the Bloch-wave core on this page to obtain $T_{\mathbf g}(t)$ and sets $I_{\mathbf g}=\lvert T_{\mathbf g}\rvert^2$. | Thickness dependence, multiple scattering, and strong electron-diffraction reflections. |

The reciprocal-lattice-point display modes, such as solid-sphere cross sections and Gaussian spots, mainly control the drawing profile. In dynamical-theory mode, the physical reflection intensity is determined by the Bloch-wave value $|T_{\mathbf g}|^2$, and that intensity is then assigned to the chosen display profile.

PED can be viewed as integrating this parallel-beam SAED calculation over precession directions, while CBED can be viewed as arranging many incident directions inside diffraction disks.

---

## Mean inner potential and refraction

When the electron enters the crystal from vacuum, the mean inner potential $U_0$ slightly changes the reference wavevector inside the crystal. The component parallel to the surface is fixed by the boundary condition, so the vacuum wavevector $\mathbf k_{vac}$ and the crystal reference wavevector $\mathbf k_0$ can be written as

$$|\mathbf k_0|^2 = k_{vac}^2 + U_0, \qquad \mathbf k_0 = \mathbf k_{vac} + x\,\hat{\mathbf n}$$

where $x$ is the correction along the surface normal. It is obtained from

$$x^2 + 2(\hat{\mathbf n}\cdot\mathbf k_{vac})x - U_0 = 0$$

This refracted $\mathbf k_0$ is used when evaluating $P_g$, $Q_g$, excitation errors, and the structure matrix $\mathbf A$ in the [overview page](index.md). The absorptive potential also has a $\mathbf g=\mathbf 0$ component, $U'_0$, which acts as a common mean attenuation for waves propagating through the crystal.

---

## Beam Selection

The Bloch-wave calculation cannot include infinitely many reciprocal-lattice vectors, so ReciPro selects a finite beam set $\{\mathbf g\}$. The ranking quantity is

$$R_{\mathbf g}=|\mathbf g|\,Q_{\mathbf g}^{\,2}$$

and beams with smaller $R_{\mathbf g}$ are included first. This favours beams with short reciprocal-lattice vectors that are also close to the Ewald sphere.

In practical calculations, it is important to check how much the intensity or image changes as the maximum number of Bloch waves is increased. Strong zone-axis conditions and CBED patterns with HOLZ-line detail can require several hundred beams, while off-zone conditions may converge with fewer beams.

---

## Solver Choice

After the finite beam set is chosen, ReciPro mainly uses two equivalent ways to obtain the transmission coefficients.

| Method | Feature | Typical use |
|--------|---------|-------------|
| Eigenvalue method | Diagonalises the structure matrix $\mathbf A$ and obtains the eigenvalues $\lambda^{(j)}$ and eigenvectors $C_{\mathbf g}^{(j)}$. Thickness dependence is then evaluated through $e^{2\pi i\lambda^{(j)}t}$. | Thickness series, CBED, and EBSD calculations that scan many depths or energies |
| Matrix-exponential method | Directly evaluates the scattering matrix $\exp(2\pi i\mathbf A t)$ without explicitly using an eigendecomposition. | Single-thickness STEM calculations and slice-integrated calculations |

Both methods solve the same Bethe equation. In the implementation, the code chooses among the eigenvalue method, the matrix-exponential method, managed .NET routines, and the native Eigen library according to the number of beams, the thickness array, and whether the native library is available.

---

## Convergence Checks

For dynamical calculations, checking that the basis is large enough is as important as the formula itself. A useful diagnostic is the relative change when the beam count is increased from $N-\Delta N$ to $N$:

$$\Delta I_N=\frac{|I_N-I_{N-\Delta N}|}{I_N}$$

For STEM, check this together with the detector-angle setting. For CBED, inspect the disk interiors and HOLZ lines. For EBSD, also compare the Kikuchi-band widths and background in the master pattern. This connects numerical convergence with the physical features visible in the simulated result.

---

## See also

- [Appendix A3. Dynamical Diffraction by the Bloch-Wave Method](index.md)
- [7.2. SAED simulation](../../7-diffraction-simulator/1-saed-simulation.md)
- [7.4. CBED simulation](../../7-diffraction-simulator/3-cbed-simulation.md)
- [7. Diffraction simulator](../../7-diffraction-simulator/index.md)

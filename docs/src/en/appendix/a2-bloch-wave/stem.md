# STEM Calculation

STEM image calculation starts from the same convergent-probe representation as [CBED](cbed.md). The difference is the observable: CBED displays the disk intensity in the diffraction plane, whereas STEM scans the probe position and integrates the intensity that enters the selected detector at each position.

---

## Observable

Let $\mathbf R_0$ be the probe position, $\mathbf Q$ the diffraction-plane coordinate, and $t$ the specimen thickness. If the detector function $D(\mathbf Q)$ is 1 inside the detector angular range and 0 outside it, the elastic STEM intensity is

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf R_0)=
\int D(\mathbf Q)\,
\left|\psi(\mathbf Q,t;\mathbf R_0)\right|^2\,d\mathbf Q$$

BF, ABF, LAADF, and HAADF correspond to different choices of the inner and outer angles in $D(\mathbf Q)$. Changing the STEM detector angle therefore changes the physical quantity being integrated; it is not only a display setting.

---

## Fourier-Coefficient Acceleration

A direct implementation would solve the dynamical problem again for every scanned probe position $\mathbf R_0$. The convergent-probe expression has a useful structure: the $\mathbf R_0$ dependence enters as the phase factor

$$\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)$$

This allows ReciPro to calculate the two-dimensional Fourier coefficients of the image first, rather than calculating $I_{\mathrm{STEM}}(\mathbf R_0)$ point by point. Conceptually,

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf q)=
\sum_{\mathbf g,\mathbf h}
F_{\mathbf g,\mathbf h}(t)\,
\delta(\mathbf q-\mathbf g+\mathbf h)$$

so once the coefficients $F_{\mathbf g,\mathbf h}(t)$ are known, the full scan image can be reconstructed efficiently by an inverse Fourier transform.

This is the main advantage of Bloch-wave STEM for perfect crystals with small unit cells. It can be much faster than repeating a multislice calculation at every probe position.

---

## TDS and Detector-Selected Absorption

In HAADF-STEM, the inelastic component from thermal diffuse scattering (TDS) is often the main source of image contrast. ReciPro treats TDS as the amount of intensity removed from the elastic channel into a selected angular range, represented by an absorptive potential.

For a detector angular range $\theta_1\leq\theta\leq\theta_2$, the detector-selected absorptive scattering factor can be written conceptually as

$$f'_{\kappa}(\mathbf g;\theta_1,\theta_2)=
\int_{\theta_1}^{\theta_2}\sin\theta\,d\theta
\int_0^{2\pi}
\left|\Delta f_{e,\kappa}(\mathbf g,\theta,\phi)\right|^2\,d\phi$$

Choosing this range to match a BF, ADF, or HAADF detector evaluates the TDS contribution that enters that detector.

The STEM TDS intensity is the thickness integral of the detector-selected absorption:

$$I_{\mathrm{STEM}}^{\mathrm{TDS}}(\mathbf R_0)=
\int_0^t
\langle\psi(z;\mathbf R_0)|\widehat W_{\mathrm{det}}|\psi(z;\mathbf R_0)\rangle\,dz$$

where $\widehat W_{\mathrm{det}}$ represents detector-selected TDS. Once the Bloch-wave eigenvalues and eigenvectors are known, this $z$ integral can be handled analytically. A numerical slice integration is also possible, and ReciPro uses the appropriate approach for the calculation mode.

---

## Local and Nonlocal Absorption

The absorptive potential can be treated in two main ways.

| Form | Meaning | Feature |
|------|---------|---------|
| Local approximation | Uses an absorptive potential $U'(\mathbf r)$ that depends only on position. | Usually effective and fast for broad ADF / HAADF detectors. |
| Nonlocal form | Uses $U'(\mathbf r,\mathbf r')$ or matrix elements $U'_{\mathbf g,\mathbf h}$ that depend on pairs of incoming and outgoing waves. | More accurate for narrow detectors, heavy elements, or low accelerating voltages, but much more expensive. |

In the local approximation, matrix elements can be evaluated from reciprocal-vector differences such as $U'_{\mathbf g-\mathbf h}$. In the nonlocal form, each $(\mathbf g,\mathbf h)$ pair requires its own angular integration, so the cost grows rapidly with the number of beams.

---

## Scope of Bloch-Wave STEM

Bloch-wave STEM is fast for highly periodic, perfect crystals and is well suited to systematic comparisons of thickness, defocus, and detector angles. For defects, large supercells, or non-periodic structures, methods such as frozen-phonon multislice may be more appropriate because they do not rely on the same small-periodic-cell assumption.

In ReciPro, STEM is easiest to understand as follows: start with the same convergent wave as CBED, then replace the diffraction-disk observable with detector integration over the diffraction plane.

---

## Practical Parameters

- **Detector angle**: BF / ABF / ADF / HAADF are definitions of $D(\mathbf Q)$ and $f'_{\kappa}(\mathbf g;\theta_1,\theta_2)$.
- **Beam count**: High-frequency image components and channeling are sensitive to the number of beams included.
- **Thickness step**: If numerical slice integration is used, check the change when the slice thickness is halved.
- **TDS model**: For HAADF $Z$-contrast, the TDS term is as important as the elastic term.

## See also

- [Dynamical calculation (common core)](calculation.md)
- [Appendix A2. Dynamical Diffraction by the Bloch-Wave Method](index.md)
- [9.2. STEM simulation](../../9-hrtem-stem-simulator/2-stem-simulation.md)

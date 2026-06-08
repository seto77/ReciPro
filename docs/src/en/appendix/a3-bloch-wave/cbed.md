# CBED Calculation

CBED (convergent-beam electron diffraction) applies the [dynamical core](calculation.md) to many incident-beam directions and then places the results into diffraction disks. SAED has one incident direction; CBED treats each point inside the objective aperture as a **partial incident plane wave** and solves the Bloch-wave problem for each one.

---

## Convergent-Beam Representation

At the entrance surface, the convergent probe can be written as a sum of plane waves using the probe position $\mathbf R_0$, lens phase $\chi(\mathbf K)$, and aperture function $A(\mathbf K)$:

$$\psi_{\mathrm{in}}(\mathbf R,0)=\sum_{\mathbf K\in\mathrm{aperture}} A(\mathbf K)\,
\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)\,
\exp[-i\chi(\mathbf K)]\,
\exp(2\pi i\,\mathbf K\cdot\mathbf R)$$

Here $\mathbf K$ is the component of the incident wavevector parallel to the specimen surface. For an ideal circular aperture with convergence semi-angle $\alpha$ and electron wavelength $\lambda$,

$$A(\mathbf K)=
\begin{cases}
1 & (|\mathbf K|\leq \sin\alpha/\lambda)\\
0 & (|\mathbf K|> \sin\alpha/\lambda)
\end{cases}$$

A representative lens phase, using defocus $\Delta f$ and spherical aberration $C_s$, is

$$\chi(\mathbf K)=\pi\lambda|\mathbf K|^2\Delta f+\frac{\pi}{2}C_s\lambda^3|\mathbf K|^4+\cdots$$

In ReciPro this expression is controlled by the aberration, aperture, and convergence-angle settings.

---

## Dynamical Calculation for Each Direction

For CBED, each $\mathbf K$ inside the aperture is treated as one parallel incident beam. The conceptual workflow is:

1. Determine the refracted reference wavevector $\mathbf k_0(\mathbf K)$ from $\mathbf K$ and the specimen surface normal.
2. Select the reflected beams using the ranking quantity $R_{\mathbf g}=|\mathbf g|Q_{\mathbf g}^2$.
3. Build the structure matrix $\mathbf A$ and calculate the transmission coefficients $T_{\mathbf g}(t;\mathbf K)$ at thickness $t$.

This is the transmission-coefficient calculation from the [dynamical core](calculation.md), repeated for every sampled incident direction. For a thickness series, the eigensolution for a given direction can be reused and only the propagation factors need to be updated.

---

## Diffraction-Disk Assembly

Placing the exit waves from all $\mathbf K$ directions into the diffraction plane gives the intensity inside the transmitted disk and the diffracted disks. If $\mathbf Q$ is the diffraction-plane coordinate, position-averaged CBED or low-coherence conditions can be approximated as an incoherent intensity sum:

$$I_{\mathrm{CBED}}(\mathbf Q)=
\sum_{\mathbf K\in\mathrm{aperture}}
\left|\psi_{\mathbf K}(\mathbf Q,t)\right|^2$$

For LACBED-like modes where phase coherence across a wider region matters, the amplitudes must be summed first and the intensity taken afterwards.

---

## What CBED Shows

CBED makes the thickness dependence of the Bloch-wave solution visible as intensity structure inside diffraction disks.

- Changing the thickness changes disk-interior oscillations, HOLZ lines, and Kossel-Mollenstedt fringes.
- Changing the incident orientation changes which reflections are strongly excited.
- Increasing the convergence angle broadens the disks and can reveal overlap and higher-order Laue-zone information.

CBED is therefore the most direct way to view the Bloch-wave result as a disk pattern in the diffraction plane. In ReciPro it is best understood as the combination of convergent-beam discretisation, one dynamical solution per direction, and rearrangement into disk arrays.

---

## Practical Parameters

- **Beam count**: Strong zone-axis conditions and HOLZ-line detail require many reflected beams. Check how the disk interiors change as the maximum Bloch-wave count is increased.
- **Angular sampling**: If the $\mathbf K$ sampling inside the aperture is too coarse, the disk intensity becomes granular. Larger convergence angles require finer sampling.
- **Thickness**: Thickness series benefit from the eigenvalue method because one eigensolution can be reused for many thicknesses.
- **Coherence**: Distinguish conditions where an incoherent intensity sum is sufficient from those where coherent amplitude summation is needed.

## See also

- [Dynamical calculation (common core)](calculation.md)
- [Appendix A3. Dynamical Diffraction by the Bloch-Wave Method](index.md)
- [7.4. CBED simulation](../../7-diffraction-simulator/3-cbed-simulation.md)

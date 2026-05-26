# Appendix A3. Bloch-Wave Calculation Details

!!! info "Under preparation"
    This appendix is being written. It will describe, step by step, the actual dynamical-diffraction calculation that ReciPro performs with the Bloch-wave method. For a conceptual overview, see [Appendix A2. Dynamical Diffraction by the Bloch-Wave Method](a2-bloch-wave-method.md).

## Planned contents

- The crystal potential and its Fourier coefficients $V_{\mathbf g}$ and $U_{\mathbf g}$
- The structure matrix $\mathbf{A}$ and the eigenvalue problem
- Eigenvalues $\gamma_j$ and eigenvectors (Bloch-wave amplitudes) $C^{(j)}$
- Boundary conditions and the exit-wave amplitudes
- Diffracted intensity $I_{\mathbf g}(\mathbf k) = \left| \sum_j C_{\mathbf g}^{(j)} C_0^{(j)} \exp(2\pi i\,\gamma_j t) \right|^2$
- Absorption / thermal diffuse scattering (the imaginary potential $V'_{\mathbf g}$)
- Numerical solvers (Eigen / MKL) and performance

## See also

- [Appendix A2. Dynamical Diffraction by the Bloch-Wave Method](a2-bloch-wave-method.md)
- [7.4. CBED simulation](../7-diffraction-simulator/4-cbed-simulation.md)
- [7. Diffraction simulator](../7-diffraction-simulator/index.md)

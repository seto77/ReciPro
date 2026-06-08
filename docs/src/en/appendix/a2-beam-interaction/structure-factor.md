# Structure Factor

The atomic scattering factor describes one atom; the **structure factor** describes how all the atoms in the unit cell scatter *together*. It is the quantity the **Reflections** tab tabulates (`F_real`, `F_inv`, $\lvert F\rvert$, $F^2$), and it is the bridge between the atomic physics of the previous page and the diffracted intensities.

=== "X-ray"
    ![Reflections — X-ray](../../../assets/cap-en-auto/FormBeamInteraction-xray-reflections.png)

=== "Electron"
    ![Reflections — electron](../../../assets/cap-en-auto/FormBeamInteraction-electron-reflections.png)

=== "Neutron"
    ![Reflections — neutron](../../../assets/cap-en-auto/FormBeamInteraction-neutron-reflections.png)

---

## Interference over the unit cell

The structure factor of reflection $\mathbf g = (hkl)$ is the coherent sum of the atomic factors, each weighted by the phase from the atom's fractional position $\mathbf r_j = (x_j,y_j,z_j)$:

$$F_{\mathbf g} = \sum_{j} o_j\, f_j(s,E)\, T_j(\mathbf g)\, \exp\!\left(-2\pi i\,(h x_j + k y_j + l z_j)\right).$$

- $o_j$ : site **occupancy** (fractional, for partial or mixed occupation).
- $f_j(s,E)$ : the atomic scattering factor of atom $j$ for the current beam — $f_0+f'-if''$ for X-rays in ReciPro's [phase convention](index.md#phase-convention), $f_e$ for electrons, $b$ for neutrons.
- $T_j(\mathbf g)$ : the Debye–Waller factor (below).
- The $-2\pi i$ phase follows ReciPro's [convention](index.md#phase-convention).

The intensity is the squared modulus,

$$I_{\mathbf g} \;\propto\; \lvert F_{\mathbf g}\rvert^2 = F_\text{real}^2 + F_\text{inv}^2 ,$$

which is the table's $F^2$ column. `F_real` and `F_inv` are the real and imaginary parts of the complex structure factor. Even with purely real atomic factors, $F_{\mathbf g}$ is generally complex for a non-centrosymmetric structure (or a shifted origin); X-ray anomalous dispersion (complex $f$) and complex neutron scattering lengths add a further imaginary contribution. `F_inv` vanishes for *every* reflection only when the structure is centrosymmetric with the origin at a centre of symmetry and all factors are real.

---

## The Debye–Waller factor

Atoms vibrate about their equilibrium sites, smearing the scattering density and reducing the high-angle factors. For isotropic motion,

$$T_j = \exp\!\left(-B_j\, s^2\right), \qquad B_j = 8\pi^2\langle u_j^2\rangle,$$

where $\langle u_j^2\rangle$ is the mean-square displacement along the scattering direction and $B_j$ is the isotropic displacement parameter (Å²). Anisotropic motion generalises this to

$$T_j = \exp\!\left(-2\pi^2\,\mathbf g^{\mathsf T}\!\mathbf U_j\,\mathbf g\right),$$

with $\mathbf U_j$ the displacement tensor and $\mathbf g$ the reciprocal-lattice vector ($|\mathbf g|=1/d$, not $Q=2\pi\lvert\mathbf g\rvert$). For a Debye solid the mean-square displacement is itself a function of temperature $T$, atomic mass $M$, and Debye temperature $\Theta_D$,

$$\langle u^2\rangle = \frac{3\hbar^2}{M k_B \Theta_D}\left[\frac14 + \left(\frac{T}{\Theta_D}\right)^2\!\int_0^{\Theta_D/T}\frac{x}{e^x-1}\,dx\right],$$

so $B$ rises with temperature and falls for heavy atoms. ReciPro uses the tabulated or entered $B_j$ directly rather than computing this. Because $T_j$ multiplies the scattering factor, the **Scattering factors** tab can apply the same $e^{-Bs^2}$ damping to the plotted curves. The damping grows with temperature and with $s$, which is why thermal diffuse scattering (intensity removed from the coherent Bragg beams and redistributed into a diffuse background) feeds the absorptive potential in the dynamical theory ([Appendix A3](../a3-bloch-wave/index.md)).

---

## Extinctions: systematic vs accidental

A reflection can be **absent** for two distinct reasons:

- **Systematic (space-group) absences.** Lattice centering and symmetry elements with a translational component (screw axes, glide planes) make whole classes of reflections vanish *exactly*, for every crystal in that space group, regardless of the atomic contents. These are the rules behind **Hide prohibited planes**.
- **Accidental near-extinctions.** When the atomic contributions happen to cancel for a particular structure, the intensity is small but not symmetry-forbidden, and it can reappear if the composition or positions change. These are *not* removed by the extinction rules.

A systematic absence is a phase cancellation among the symmetry-related copies of the cell. For centering translations $\mathbf t_\alpha$ the structure factor carries a common factor

$$F_{\mathbf g} \propto \sum_\alpha e^{-2\pi i\,\mathbf g\cdot\mathbf t_\alpha},$$

which is zero for certain $hkl$. For body-centring ($\mathbf t = \tfrac12,\tfrac12,\tfrac12$),

$$1 + e^{-\pi i (h+k+l)} = 0 \quad\Longleftrightarrow\quad h+k+l \ \text{odd}.$$

The most common systematic absences are:

| Symmetry element | Condition for absence | Reflections affected |
|---|---|---|
| $I$ (body-centred) | $h+k+l$ odd | all $hkl$ |
| $F$ (face-centred) | $h,k,l$ mixed parity | all $hkl$ |
| $C$ (C-centred) | $h+k$ odd | all $hkl$ |
| $2_1$ screw $\parallel b$ | $k$ odd | $0k0$ |
| $a$-glide $\perp b$ | $h$ odd | $h0l$ |
| $c$-glide $\perp b$ | $l$ odd | $h0l$ |

Centering conditions apply to every reflection; screw and glide conditions apply only to the corresponding axial row or zone, which is exactly what makes them diagnostic of the space group.

---

## Friedel's law and its breakdown

For a structure of real (non-resonant) scattering factors, conjugating the sum and flipping the sign of $\mathbf g$ shows directly that (suppressing the real weights $o_j T_j$ for clarity)

$$F_{-\mathbf g} = \sum_j f_j\, e^{+2\pi i\,\mathbf g\cdot\mathbf r_j} = \left(\sum_j f_j\, e^{-2\pi i\,\mathbf g\cdot\mathbf r_j}\right)^{*} = F_{\mathbf g}^{*}, \qquad\text{hence}\qquad \lvert F_{hkl}\rvert = \lvert F_{\bar h\bar k\bar l}\rvert \quad\text{(Friedel's law).}$$

Diffraction then appears centrosymmetric even when the crystal is not. **Anomalous dispersion can break this.** Writing the structure factor as a normal part (which conjugates cleanly) plus an anomalous part, $F_{\mathbf g} = A_{\mathbf g} - i B_{\mathbf g}$ and $F_{-\mathbf g} = A_{\mathbf g}^{*} - i B_{\mathbf g}^{*}$ in ReciPro's $f = f_0 + f' - i f''$ convention, the **Bijvoet difference** is

$$\lvert F_{\mathbf g}\rvert^2 - \lvert F_{-\mathbf g}\rvert^2 = -4\,\operatorname{Im}\!\left(A_{\mathbf g}\, B_{\mathbf g}^{*}\right),$$

non-zero only when the normal and anomalous parts have different phases — that is, when chemically distinct anomalous scatterers occupy non-centrosymmetric sites. (The difference vanishes for a centrosymmetric structure, a single element, or any case where every atom carries the same complex factor.) This is what allows the absolute structure (handedness) of a non-centrosymmetric crystal to be determined, and it is the physical reason ReciPro reports a non-zero `F_inv` and distinct $\lvert F\rvert$ for Friedel pairs once an X-ray energy near an edge is chosen.

---

## From structure factor to powder intensity

Switching on **Powder Diffraction Intensities (Bragg–Brentano)** converts $\lvert F\rvert^2$ into a relative powder intensity by folding in the geometry of a randomly oriented polycrystal:

$$I_{hkl} \;\propto\; m_{hkl}\, \lvert F_{hkl}\rvert^2\, L p(\theta),$$

- $m_{hkl}$ : **multiplicity** — the number of symmetry-equivalent planes that overlap at the same $2\theta$ (the table's *Multi.* column).
- $Lp(\theta)$ : the **Lorentz–polarisation** factor for Bragg–Brentano optics, $Lp = \dfrac{1+\cos^2 2\theta}{\sin^2\theta\,\cos\theta}$, which strongly boosts the low-angle peaks.

Because equivalent planes are merged into one line in this mode, ReciPro also forces *Hide equivalent planes* and *Hide prohibited planes* on.

---

## See also

- [Atomic scattering factors](scattering-factor.md) — the $f_j$ that enter the sum.
- [Attenuation & transport](attenuation-transport.md) — what happens to the beam between scattering events.
- [3. Beam interaction → Reflections tab](../../3-beam-interaction.md#reflections-tab)
- [Appendix A3. Dynamical diffraction](../a3-bloch-wave/index.md) — when $\lvert F\rvert^2$ (kinematical) is no longer enough.

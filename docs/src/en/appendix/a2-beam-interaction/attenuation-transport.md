# Attenuation & Transport

Scattering factors describe a single scattering event; this page is about what happens to the beam **as a whole** as it travels through the solid — how fast it is removed, how deeply it penetrates, and (for electrons) how it slows down. The relevant physics is entirely different for the three beams, which is why the **Attenuations & Transport** tab changes its plots and tables so drastically with the radiation.

=== "X-ray"
    ![Attenuations & Transport — X-ray](../../../assets/cap-en-auto/FormBeamInteraction-xray-attenuations.png)

=== "Electron"
    ![Attenuations & Transport — electron](../../../assets/cap-en-auto/FormBeamInteraction-electron-attenuations.png)

=== "Neutron"
    ![Attenuations & Transport — neutron](../../../assets/cap-en-auto/FormBeamInteraction-neutron-attenuations.png)

---

## X-rays — absorption and refraction

### Beer–Lambert attenuation

A monochromatic X-ray beam is removed exponentially with path length:

$$I(t) = I_0\, e^{-\mu t}, \qquad \mu = \rho\,(\mu/\rho).$$

- $\mu/\rho$ : the **mass attenuation coefficient** (cm²/g) — the tabulated, density-independent quantity.
- $\mu$ : the **linear attenuation coefficient** (cm⁻¹) at the material's actual density $\rho$.
- $1/\mu$ : the **attenuation length** (intensity falls to $1/e$).
- $\text{HVL} = \ln 2/\mu$ : the **half-value layer**.
- $T = e^{-\mu t}$ : the transmission for a sample of thickness $t$.

### What makes up $\mu/\rho$

The total mass attenuation is the sum of three processes, plotted separately in the tab:

$$\left(\frac{\mu}{\rho}\right)_\text{total} = \left(\frac{\tau}{\rho}\right)_\text{photo} + \left(\frac{\mu}{\rho}\right)_\text{Rayleigh} + \left(\frac{\mu}{\rho}\right)_\text{Compton}.$$

For a compound the mass attenuation is the mass-weighted sum of the elemental values, while the linear coefficient adds up the atomic cross sections directly:

$$\left(\frac{\mu}{\rho}\right)_\text{mix} = \sum_i w_i\left(\frac{\mu}{\rho}\right)_i, \qquad \mu = \sum_i n_i\,\sigma_i,$$

with $w_i$ the mass fractions and $n_i$ the number densities. The three components are:

- **Photoabsorption** $\tau$ — a photon is absorbed and ejects a bound electron. It dominates at low energy, falling roughly as $\tau/\rho \propto Z^{3\!-\!4}/E^{3}$ between edges. This is the term that ejects the inner-shell electron whose relaxation produces [fluorescence](fluorescence.md).
- **Rayleigh (coherent)** scattering — elastic scattering off bound electrons, related to the coherent form factor $F(q)$.
- **Compton (incoherent)** scattering — inelastic scattering off weakly bound electrons, related to the incoherent function $S(q)$; it grows in relative importance at high energy. The scattered photon is shifted in wavelength by

$$\Delta\lambda = \lambda' - \lambda = \frac{h}{m_e c}\,(1-\cos\varphi),$$

  so a Compton event removes the photon from the monochromatic beam (an inelastic loss).

The **absorption edges** are the sharp rises in $\tau$ when the photon energy crosses the binding energy of a shell ($K$, $L_3$, …), opening a new ionisation channel. The **jump ratio** is the factor by which $\mu/\rho$ increases across the edge; ReciPro lists the $K$ and $L_3$ edge energies and jumps. The **mass energy-absorption coefficient** $\mu_\text{en}/\rho$ is the part of $\mu/\rho$ that deposits energy locally (excluding the energy carried away by scattered and fluorescent photons).

### Refraction, critical angle, and SLD

The X-ray refractive index of a solid is **slightly less than 1**, written

$$n = 1 - \delta + i\beta, \qquad \beta = \frac{\mu_\text{abs}\lambda}{4\pi} = \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,f''_i, \qquad \delta \simeq \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,(Z_i+f'_i),$$

where $n_i$ is the number density of element $i$ and $r_e$ the classical electron radius. Here $\mu_\text{abs}$ is the absorptive part of the attenuation (tied to $f''$); it need not equal the total $\mu$ above, which also contains Rayleigh and Compton scattering. Because $n<1$, X-rays undergo **total external reflection** below a small grazing **critical angle**

$$\theta_c \simeq \sqrt{2\delta}.$$

This follows from the refraction geometry: for a glancing angle $\alpha$ the vertical wavevector inside the solid is $k_z^2 \simeq k^2(\alpha^2 - 2\delta)$, which reaches zero at $\alpha = \alpha_c = \sqrt{2\delta}$; below that the wave cannot propagate into the material and is totally reflected. The real part of the **scattering-length density**, $\text{SLD} = r_e\sum_i n_i (Z_i + f'_i)$, sets $\delta$ and is the X-ray analogue of the neutron SLD used in reflectometry. ReciPro reports $\delta$, $\beta$, $\theta_c$, and the X-ray SLD in the scalar table.

---

## Electrons — scattering, slowing, and range

A fast electron in a solid both **scatters** (changing direction) and **loses energy** continuously, so its transport needs more than one length scale.

### Elastic scattering and the mean free path

The elastic cross section $\sigma_\text{el}$ measures how readily a single atom deflects the electron. ReciPro uses the **NIST Mott** cross sections (a partial-wave solution of the relativistic Dirac equation in the screened atomic potential), valid roughly over **50 eV – 36.4 keV**; outside that range, or for elements not in the table, it falls back to the **screened Rutherford** approximation. The two need not join perfectly smoothly at the boundary. The total cross section is the angular integral of the differential one,

$$\sigma_\text{el} = 2\pi\int_0^\pi \frac{d\sigma}{d\Omega}\,\sin\Theta\,d\Theta, \qquad \frac{d\sigma}{d\Omega} \propto \frac{Z^2}{E^2}\,\frac{1}{\big[\sin^2(\Theta/2)+\eta\big]^2},$$

where the screening parameter $\eta$ rounds off the forward divergence of the bare Rutherford cross section; the Mott treatment additionally includes the spin and relativistic effects that screened Rutherford omits. From the cross section,

$$\Sigma_\text{el} = \sum_i n_i\,\sigma_{\text{el},i}, \qquad \lambda_\text{el} = \frac{1}{\Sigma_\text{el}},$$

give the macroscopic scattering coefficient and the **elastic mean free path** — the average distance between elastic events.

### Stopping power and inelastic losses

Energy is lost mainly to electronic excitations (ionisation, plasmons). The **stopping power** is defined as a positive quantity,

$$S(E) = -\frac{dE}{ds} > 0,$$

where here $s$ is the **path length** along the trajectory (the variable of the tab's *|dE/ds|* curve), not the scattering variable $\sin\theta/\lambda$ used elsewhere in this appendix. The energy gradient $dE/ds$ is negative, so the tab plots $S$ upward. At keV energies it follows, in concept, the **Bethe** form

$$S(E) \;\propto\; \frac{Z\rho}{A}\,\frac{1}{E}\,\ln\!\frac{E}{J},$$

with $J$ the **mean excitation energy** of the solid. This non-relativistic sketch shows the scaling only; ReciPro evaluates a corrected/empirical form (of the Joy–Luo type) that stays well-behaved at low energy. The **plasmon energy** $E_p$ in the scalar table is a related but separate characterisation of the same electronic excitations. The **inelastic mean free path** (IMFP) is the corresponding average distance between energy-losing collisions; ReciPro can evaluate it from the **TPP-2M** predictive formula,

$$\lambda_\text{in}(E) = \frac{E}{E_p^2\left[\beta_\text{T}\ln(\gamma_\text{T} E) - C/E + D/E^2\right]},$$

with $E$ in eV, $\lambda_\text{in}$ in Å, and the parameters $\beta_\text{T},\gamma_\text{T},C,D$ built from $E_p$, the density, the band gap, and the valence-electron count.

### Two kinds of range

- **CSDA range** — the continuous-slowing-down approximation integrates the stopping power to give the total path length travelled before the electron stops:

$$R_\text{CSDA} = \int_{E_\text{cut}}^{E_0} \frac{dE}{S(E)}.$$

(In practice the integral runs down to a low-energy cutoff $E_\text{cut}$, below which the Bethe sketch above no longer holds.)

- **Kanaya–Okayama range** — a widely used empirical estimate of the **penetration depth** (not the path length), accounting for the tortuous, scattered trajectory:

$$R_\text{KO}\,[\mu\text{m}] = 0.0276\,\frac{A\,E_0^{1.67}}{\rho\,Z^{0.89}}, \qquad (E_0\ \text{in keV}).$$

The two answer different questions — total distance flown vs. how far into the solid the electron reaches — so they differ in value, and ReciPro reports both. These ranges set the interaction volume behind [electron trajectory](../../8-electron-trajectory.md) and EBSD simulations.

---

## Neutrons — macroscopic cross section and the 1/v law

For neutrons there is no energy-dependent attenuation curve; the interaction is fixed by **nuclear cross sections**. The beam is attenuated through the macroscopic total cross section, itself the sum of coherent, incoherent, and absorption parts:

$$\Sigma_\text{total} = \sum_i n_i\,\sigma_{\text{total},i}, \qquad \sigma_\text{total} = \sigma_\text{coh} + \sigma_\text{inc} + \sigma_\text{abs}(\lambda), \qquad T = e^{-\Sigma_\text{total} t},$$

with attenuation length $1/\Sigma_\text{total}$. The absorption part depends on the neutron speed $v$ (hence wavelength): for most nuclides the time spent near the nucleus scales as $1/v$, giving the **1/v law**

$$\sigma_\text{abs}(\lambda) = \sigma_\text{abs}(\lambda_0)\,\frac{\lambda}{\lambda_0}, \qquad \lambda_0 = 1.798\ \text{Å}\ (\text{thermal}, 2200\ \text{m/s}).$$

A few strong absorbers (Cd, Sm, Eu, Gd) have low-energy **resonances** that violate the simple 1/v scaling; ReciPro flags these nuclides. The coherent **scattering-length density**, $\text{SLD} = \sum_i n_i\, b_{\text{coh},i}$, is the neutron analogue of the X-ray SLD above.

---

## Penetration at a glance

The three beams probe vastly different depths — the practical reason they answer different questions:

| Beam | Typical specimen | Penetration (order of magnitude) | Set by |
|---|---|---|---|
| X-ray (≈8 keV) | powder / single crystal | 10–100 µm | $\mu = \rho(\mu/\rho)$ |
| Electron (≈200 keV) | TEM foil | 10–100 nm (useful) | elastic MFP + inelastic loss |
| Neutron (thermal) | bulk, cm-sized | 1–10 cm | $\Sigma_\text{total}$ |

The same length scales explain why electrons demand ultrathin specimens and dynamical theory, while neutrons see an entire bulk sample under single-scattering kinematics.

---

## See also

- [Atomic scattering factors](scattering-factor.md) — the $F(q)$/$S(q)$ split behind Rayleigh/Compton, and the Mott cross sections.
- [Fluorescence](fluorescence.md) — the relaxation that follows X-ray photoabsorption.
- [3. Beam interaction](../../3-beam-interaction.md) — the *Attenuations & Transport* tab.
- [8. Electron trajectory](../../8-electron-trajectory.md) · [12. EBSD simulation](../../12-ebsd-simulation.md) — where the electron ranges are used.

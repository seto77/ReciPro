# EBSD Calculation

EBSD (electron backscatter diffraction) uses the same Bethe/Bloch-wave core as CBED and STEM, but the problem is posed differently. CBED and STEM are **incident-beam problems**: an electron wave enters from outside the specimen and the exit wave is calculated. EBSD is an **exit-direction problem**: electrons that have undergone inelastic scattering inside the specimen emerge as backscattered electrons, and the calculation asks how much intensity leaves in each external direction.

ReciPro converts this exit-direction problem into an ordinary incident-beam problem by the reciprocity theorem. It first calculates a direction-space **master pattern**, then combines that master pattern with Monte-Carlo depth / energy / direction weights and detector geometry to form the detector pattern.

---

## Reciprocity-Theorem Reformulation

If the amplitude from an internal source point $\mathbf r_n$ to an external direction $\widehat{\mathbf s}$ were calculated directly, a separate scattering problem would be needed for every source point. That is not practical.

The reciprocity theorem rewrites the problem as follows: the amplitude for an electron starting from $\mathbf r_n$ to appear in the far-field direction $\widehat{\mathbf s}$ is equal to the amplitude, at $\mathbf r_n$, of a reciprocal wave incident from the external direction $-\widehat{\mathbf s}$. This reciprocal wave is an ordinary Bethe/Bloch-wave solution. Writing it as $\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r)$, the EBSD intensity in direction $\widehat{\mathbf s}$ can be written as

$$I_{\mathrm{EBSD}}(\widehat{\mathbf s};E,z)\propto
\sum_n \sigma_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n;E,z)\right|^2$$

where $\sigma_n(E,z)$ is the weight for inelastic scattering near atom position $\mathbf r_n$ into the backscattered channel at energy $E$ and depth $z$. The source terms are added as intensities, not as a coherent amplitude sum, because inelastic scattering is assumed to destroy the phase relation between different source positions.

---

## Master Pattern

The EBSD master pattern stores the crystal-specific dynamical-diffraction part of the expression above on a grid of directions. Conceptually,

$$M(\widehat{\mathbf s};E,z)=
\sum_n w_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n)\right|^2$$

where $w_n$ is the crystal-side inelastic-source weight at atom position $\mathbf r_n$. ReciPro uses the empirical weight

$$w_n \propto Z_n^{1.7}\,\mathrm{occ}_n$$

with atomic number $Z_n$ and occupancy $\mathrm{occ}_n$. This is separate from the transport depth / energy distribution produced by Monte Carlo.

In the implementation, the reciprocal Bloch wave is evaluated at each atom position:

$$\beta_n^{(j)}=
\alpha^{(j)}
\sum_{\mathbf g}C_{\mathbf g}^{(j)}
\exp\!\left[2\pi i(\mathbf k^{(j)}+\mathbf g)\cdot\mathbf r_n\right]$$

The code then forms the Bloch-wave-pair matrix

$$S_{jj'}=\sum_n w_n\,\beta_n^{(j)}\,\overline{\beta_n^{(j')}}$$

and the analytical thickness integral

$$\mathcal F_{jj'}(t)=
\frac{\exp\!\left[2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})t\right]-1}
{2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})}$$

so that the master pattern is evaluated as

$$M(\widehat{\mathbf s};E,t)=
\mathrm{Re}\left\{\sum_{j,j'}S_{jj'}(E)\,\mathcal F_{jj'}(t)\right\}$$

In the degenerate limit where the denominator is close to zero, $\mathcal F_{jj'}(t)\to t$.

---

## Direction-Space Sampling

The master pattern is not the detector image itself; it is an intensity distribution in crystal-fixed direction space. ReciPro samples that direction space with a Rosca-Lambert equal-area projection and stores the $+Z$ and $-Z$ hemispheres as separate plane arrays. Equal-area sampling reduces the density bias between the poles and the equator.

At this stage the master pattern depends on crystal structure, accelerating voltage, depth, energy, and absorption model. Detector geometry such as pattern centre and screen position has not yet been applied.

---

## Monte-Carlo Weights and Detector Pattern

To obtain an EBSD detector pattern close to the experimental observable, the master pattern must be weighted by how many backscattered electrons emerge from each depth, energy, and direction. Writing this transport weight as

$$W(E,z;\widehat{\mathbf s})$$

and using $\widehat{\mathbf s}(\mathbf p)$ for the crystal-fixed direction corresponding to detector pixel $\mathbf p$, the final detector pattern is

$$P(\mathbf p)=
\sum_{i,j}
W(E_i,z_j;\widehat{\mathbf s}(\mathbf p))\,
M(\widehat{\mathbf s}(\mathbf p);E_i,z_j)$$

as a discrete sum over energy and depth.

The Monte-Carlo part tracks elastic scattering, inelastic scattering, energy loss, and escape through the specimen surface. For backscattered electrons it builds distributions of depth, energy, and exit direction. ReciPro distinguishes models that use the last inelastic-scattering position and the energy immediately after it as the effective source, and models that use the escape depth and escape energy.

---

## TDS Background and Absorption Model

EBSD patterns contain not only the geometric Kikuchi-band structure but also a smooth background from thermal diffuse scattering (TDS). When `IncludeTDSBackground` is enabled, ReciPro evaluates the TDS component scattered into the back hemisphere,

$$\pi/2\leq\theta\leq\pi$$

as an absorption matrix $\mu_{\mathrm{back}}$ and adds the background intensity using the same Bloch-wave-pair summation as the master pattern. Because the same eigensolution is reused, the TDS background adds relatively little extra cost.

When `UseNonLocalAbsorption` is enabled, the absorptive potential is treated not just as $U'_{\mathbf g-\mathbf h}$ but as a nonlocal form that depends on direction and beam pairs. This can improve accuracy, but it also requires rebuilding the absorption matrix for directions in the master-pattern grid, so it can substantially increase calculation time.

---

## Practical Parameters

- **Beam count**: Too few beams lose Kikuchi-band detail and HOLZ-band structure. Low-index zone axes can require several hundred beams.
- **Depth and energy arrays**: If these are coarser than the variation scale of the Monte-Carlo weight $W(E,z;\widehat{\mathbf s})$, energy-dependent band width and channeling-depth effects are averaged away.
- **Detector geometry**: Pattern centre, screen distance, and specimen tilt determine the mapping $\widehat{\mathbf s}(\mathbf p)$, so the detector pattern can change even when the master pattern is unchanged.
- **Reciprocity interpretation**: The master pattern is not the detector image. It becomes a detector pattern only after Monte-Carlo weighting and detector projection.
- **TDS background**: Enable it for quantitative band-contrast comparisons. Disable it when the geometric Kikuchi structure is easier to inspect without the smooth background.

## See also

- [Dynamical calculation (common core)](calculation.md)
- [Appendix A3. Dynamical Diffraction by the Bloch-Wave Method](index.md)
- [12. EBSD simulation](../../12-ebsd-simulation.md)

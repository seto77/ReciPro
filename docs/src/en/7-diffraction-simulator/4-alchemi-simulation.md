# ALCHEMI Simulation

**ALCHEMI (Atom Location by CHannelling-Enhanced MIcroanalysis)** determines **which site a dopant occupies** by measuring characteristic X-ray yields while the crystal is tilted along a systematic row, and reading the orientation dependence. The ALCHEMI simulator of ReciPro computes the **rocking curve (ionization yield versus orientation) in the forward direction** from a crystal structure and a set of site hypotheses.

> **This is a Preview feature.** v1 performs **one-dimensional forward calculation only**; fitting to experimental data and the 2D map (2D-HARECXS) are not implemented (those tabs are hidden). **To the best of the authors' knowledge, no other publicly available ALCHEMI forward simulator exists.** Because there is no implementation to cross-check against, read [Scope and known limitations](#scope-and-known-limitations) before using the results quantitatively.

Open it from the **Options** menu of the [Diffraction Simulator](index.md) → **ALCHEMI simulator...**

GUI conditions: Wave Length = Electron (the crystal, accelerating voltage and orientation are taken from the parent diffraction simulator)

![ALCHEMI simulator](../../assets/cap-en-auto/FormALCHEMI.png)

The window has the **settings on the left** (scan, thickness, calculation, ionization channels, site hypotheses) and the **result on the right** (Curve tab).

---

## What is computed

For each incident orientation the wave field inside the crystal is solved with the Bloch-wave method, and for every pair of site $s$ and ionization channel $c$ the ionization yield is integrated analytically up to the thickness $t$.

$$
Y_\text{dyn} = \mathrm{Re} \sum_{jj'} \alpha_j^{*}\,\bigl(C^{\dagger} \mu_{s,c} C\bigr)_{jj'}\, \alpha_{j'}\, F_{jj'}(t),
\qquad F_{jj'}(t) = \frac{e^{\lambda t} - 1}{\lambda}
$$

The ionization matrix $\mu$ depends only on the difference of two reflections, $G = \mathbf{g}_h - \mathbf{g}_g$.

$$
\mu_{hg} = \sum_a \mathrm{Occ}_a\, e^{-M_a(G)}\, \sigma_c\, F_c(|G|/2)\, e^{-2\pi i\,G \cdot \mathbf{r}_a}
$$

- $\sigma_c$ : total ionization cross section, from the **Bote–Salvat** model
- $F_c(s)$ : normalized ionization form factor, from self-generated **DHFS** tables (the same data basis as [Beam interaction](../3-beam-interaction.md) and [STEM-EDX](../9-hrtem-stem-simulator/2-stem-simulation.md))
- $e^{-M_a(G)}$ : Debye–Waller factor (anisotropic ADPs are supported)

This is the **local form-factor approximation** of ICSC (Oxley & Allen 2003). The two-momentum MDFF is not used.

### Dechannelled component

Electrons removed from the coherent Bloch field by thermal-diffuse absorption travel the remaining thickness as randomly directed electrons, and ionize there as well.

$$
Y_\text{dech} = \frac{\mu_{00}}{V_c}\,\bigl(t - L_\text{coh}(t)\bigr),
\qquad L_\text{coh}(t) = \int_0^t \sum_g |\psi_g(z)|^2\,dz
$$

Clearing **Include the dechannelled component** in the **Calculation** box drops this term. It accounts for tens of percent of the total yield at typical thicknesses, so omitting it makes the site contrast look stronger than it is.

### Output quantity

The primary quantity is the **number of core-shell vacancies generated per incident electron**. **Conversion to X-ray photons (fluorescence yield and line branching), X-ray self-absorption in the specimen, and detector efficiency and solid angle are NOT applied.**

---

## Left pane: settings

### Rocking scan

| Item | Description | Default |
|------|-------------|---------|
| **Row ( h k l )** | The systematic row to sweep, given as reflection indices. The tilt axis is taken perpendicular to both the beam and this $\mathbf{g}$, so the scan sweeps this row through its Bragg conditions | (1 0 0) |
| **Range ±** | Half width of the tilt scan (mrad). Beyond about 10 mrad a fixed union basis is no longer guaranteed, and beyond 30 mrad it is outside the v1 guarantee | 8 mrad |
| **Points** | Number of scan points (3–1001) | 101 |

The line below shows the Bragg angle $\theta_B$ of the selected row, how many $\theta_B$ the scan width corresponds to, and the tilt step — so you can see how far the scan actually reaches before running it.

### Thickness

Give the start, end and step (nm). **All thicknesses are computed together in a single run**, and the result is switched with the slider under the curve.

The site contrast changes strongly — and can even reverse sign — between thin and thick specimens, so check several thicknesses before drawing conclusions. That is why the thickness selector sits directly under the curve.

### Calculation

| Item | Description | Default |
|------|-------------|---------|
| **Max. beams** | Upper bound on the number of Bloch waves per orientation (1–1600). The union over the whole scan is larger | 120 |
| **Solver** | Calculation engine for the eigenvalue problem: **Native** (Eigen C++) or **Managed** (.NET). Where the native solver is unavailable the choice is fixed to Managed | Native |
| **Include the dechannelled component** | Whether to add $Y_\text{dech}$ above | on |

**The cap of 1600 beams is the counterpart of the tabulated range $s \le 16\ \text{Å}^{-1}$ of the ionization form factor.** In practice even 1600 beams only require about 10.5 Å⁻¹, so the tabulated range is never exhausted while the cap is respected. The value actually reached is reported on the [basis diagnostic](#basis-diagnostic) line below the graph.

### Ionization channels

The list of element and shell to ionize. Each row reads `element (Z) shell   edge energy   U = overvoltage`, with a parenthesized tag appended where care is needed.

- Channels that **cannot be excited** (the incident energy is below the absorption edge) or that fall **outside the tabulated range** are listed with the reason and cannot be checked
- Channels whose overvoltage $U = E_0/E_\text{edge}$ is below 1.2 carry a caution tag, because the cross section is less reliable there

### Site hypotheses

The list of atomic sites whose yield is computed separately, shown as `label element (x, y, z) ×multiplicity Occ occupancy`.

⚠ **In the tracer picture a channel may be paired with any site.** Pairing a dopant ionization channel with the geometry of a host site (position, ADP, occupancy) is the legitimate use; restricting the pairing to matching elements would be wrong. **Every combination** of the checked channels and sites is computed.

### Simulate / Stop

**Simulate** starts the scan. The progress is reported in the status bar in five stages (resolving ionization data → building the union basis → building the ionization matrices → solving orientations → checking the expanded basis), and **Stop** aborts at any time.

---

## Right pane: Curve tab

When the calculation finishes, one curve is drawn per site × channel pair. The legend reads `site label / channel`.

| Item | Description |
|------|-------------|
| **Thickness** | Selects the thickness to display with a slider (nothing is recomputed) |
| **Normalization** | **Scan mean (ICP)** = divide by the mean over the whole scan (the quantity normally used in ALCHEMI) / **Maximum = 1** / **Raw (per electron)** |
| **X axis** | Switches between **mrad** and **θ_B** (in units of the Bragg angle of the swept row) |
| **Bragg conditions** | Draws vertical lines at $\theta = n\,\theta_B$ |
| **Export CSV** | Writes the raw curves for every orientation, thickness, site and channel to a CSV file ([below](#csv-export)) |

⚠ **Normalization is a display transform only.** The stored quantity is always vacancies generated per incident electron, and **Maximum = 1 is for display only** — it must not be used as an ICP reference.

### Contrast and correlation

The first line under the curve reports, per series, the **contrast** $(\max-\min)/\text{mean}$ and the **correlation coefficient** $r$ against the first series. It is a summary for judging at a glance which site is doing the work: two series with $r$ close to $+1$ have the same orientation dependence, which means that data cannot separate those sites.

### Basis diagnostic

The second line reports the state of the basis.

```text
basis 347 (184 + 163)   F(s) ≤ 6.20 Å⁻¹   expanded-basis 6.7e-3   ⚠ NOT fit-eligible
```

- **basis N (centre only + added by union)** : the size of the true union of reflections taken over all orientations of the scan
- **F(s) ≤ … Å⁻¹** : the largest form-factor argument the basis actually required
- **expanded-basis** : the maximum relative difference when the centre and both ends of the scan are re-solved with a 1.25× basis. It is a **proxy for the convergence error**
- **fit-eligible / NOT fit-eligible** : the result becomes **not eligible** when the expanded-basis value exceeds the threshold of $3\times10^{-3}$

⚠ **Do not use a result flagged as not fit-eligible for a quantitative occupancy fit.** That is a release condition of v1. Note also that the diagnostic is defined on the **absolute yield**, so it errs on the conservative side when you only look at the ICP (which divides by the scan mean).

Further warnings are appended in the following situations.

- **Accelerating voltage below 80 kV** : at this voltage the form-factor table cannot guarantee $s$ up to $16\ \text{Å}^{-1}$. The calculation itself is still correct as long as the $s$ required by the basis stays inside the certified range, so this is a **notice, not a rejection**
- **Form-factor truncation** : where $F(s)$ beyond the certified range was truncated to zero, **the resulting error bound $|F| \le \varepsilon$ is shown numerically**. Nothing is silently extrapolated

---

## CSV export

**Export CSV** writes a long-format table preceded by the two header lines below. The header is written so that the file alone states the conditions needed to reproduce it.

```text
# ReciPro ALCHEMI, 250.0 kV, row (1 0 0), theta_B 3.8424 mrad, model LocalFormFactor,
#   quantity ..., normalization PerIncidentElectron (self-absorption and detector efficiency are NOT applied)
# basis 347 beams, hash ..., expanded-basis 6.658e-003, fit-eligible False
tilt_mrad,thickness_nm,site,channel,dynamic,dechannelled,total
```

`dynamic` / `dechannelled` / `total` are stored separately, so **the contribution of the dechannelled component can be assessed afterwards**. The values are raw (per incident electron) and do not pass through the display normalization; the decimal separator is always a period.

---

## Scope and known limitations

"Can be computed" and "has been verified quantitatively" are different things. This section states the latter.

### Quantitatively verified range

**β-AlCo [001] at 250 keV, channels Al-K / Co-K / Co-L** — and nothing else. Compared against a multislice + frozen-phonon calculation (py_multislice) whose dynamical formulation is completely independent:

- **Al site (light column)** : RMS residual against the ICP modulation ≤3.2 % at all thicknesses, ≤0.6 % for $t \ge 10$ nm
- **Co site (heavy column)** : ≤3 % for $t \le 4$ nm, but **6–17 % for $t \gtrsim 10$ nm**

Any other system, element, shell or voltage is "computable" but not "quantitatively verified".

### Known systematic error — the dechannelled term has no site correlation

The dechannelled term of v1 is a constant independent of orientation, so its only effect on the ICP is to pull it toward 1. In reality some of the thermally scattered electrons re-channel into the columns and, being strong scatterers, return **preferentially to the heavy columns**. In the comparison above, the effective amount of this contribution was **underestimated by 10–19 points on the heavy columns**.

→ **For light or weakly scattering sites, or for $t \lesssim 5$ nm, the agreement with an independent implementation is 1–3 %. For heavy columns with $t \gtrsim 10$ nm there is a systematic error of 6–17 % of the ICP modulation.** A re-injection model carrying site correlation is deferred to v1.1 or later.

### Not included in the forward model

**An angular-spread convolution alone will not reproduce an experiment.** None of the following is included.

- **Thickness distribution** and **bending** of the specimen
- X-ray **self-absorption**
- **Detector efficiency and solid angle**
- **Background** (bremsstrahlung, overlapping lines)
- Convolution with the **angular spread of the incident beam** (convergence semi-angle, drift) — not implemented in v1

### Model assumptions

- **Tracer approximation only** : the linear superposition of site responses holds only in the dilute limit where the dopant does not perturb the elastic wave field. Finite-concentration VCA is out of scope for v1
- **Local form-factor approximation** : $\mu$ is a function of $G = \mathbf{g}_h - \mathbf{g}_g$ alone, not the two-momentum MDFF (Model A of OAR 1999). The approximation breaks down for light-element K shells and low-energy edges
- **Vacancies, not X-ray photons** : the fluorescence yield and line branching are not applied
- **The lower bound of the accelerating voltage is 80 kV** : this is the lowest voltage at which $s = 16\ \text{Å}^{-1}$ can be guaranteed, not a rejection threshold

---

## See also

- [Diffraction simulator (overview)](index.md)
- [CBED simulation](3-cbed-simulation.md)
- [Dynamical calculation (shared core)](../appendix/a3-bloch-wave/calculation.md)
- [STEM simulation](../9-hrtem-stem-simulator/2-stem-simulation.md) — STEM-EDX, which uses the same ionization data basis
- [Beam interaction](../3-beam-interaction.md) — cross-section and absorption-edge data

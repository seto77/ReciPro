# Anhang A1.1. Grundlegendes Koordinatensystem und Kristallorientierung

<!-- 260526Cl: 図(Coordinates1-3)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

Diese Seite definiert ReciPros **grundlegendes (Orientierungs-)Koordinatensystem**, das überall dort verwendet wird, wo eine Kristalldrehung beteiligt ist (Hauptfenster, Strukturansicht, Stereonetz, Rotationsgeometrie und Beugungssimulation), zusammen mit der Art und Weise, wie die Anfangsorientierung eines Kristalls und die Euler-Winkel-Drehung ausgedrückt werden. Das separate System, das zum Platzieren des Detektors im **Beugungssimulator** verwendet wird, ist in [A1.2. Koordinatensystem für die Beugungssimulation](2-diffraction.md) beschrieben.

---

## Definition der Orientierung

ReciPro verwendet ein am Monitor fixiertes **rechtshändiges Koordinatensystem**:

| Achse | Richtung |
|------|-----------|
| <span class="rp-red">$X$</span> | Rechts auf dem Monitor |
| <span class="rp-green">$Y$</span> | Nach oben auf dem Monitor |
| <span class="rp-blue">$Z$</span> | Senkrecht aus dem Monitor heraus, zum Betrachter hin |

![Auf dem Monitor dargestellte ReciPro-Koordinatenachsen](../../../assets/references/Coordinates1.png){width=400px}

Die **Strahlrichtung** entspricht der Blickrichtung (in den Monitor hinein), d. h. der <span class="rp-blue">$-Z$</span>-Achse.

Die meisten Operationen in ReciPro betreffen nur *Richtungen* (ausgedrückt als 3×3-Rotationsmatrizen) und erfordern keinen expliziten Ursprung. Die einzige Ausnahme ist die Funktion **Beugungssimulator**, die einen expliziten Ursprung benötigt — siehe [A1.2. Koordinatensystem für die Beugungssimulation](2-diffraction.md).

## Anfängliche Kristallorientierung

Die Anfangsorientierung (beim ersten Start oder nach **Rotation zurücksetzen**) ist wie folgt definiert:

1. Die <span class="rp-blue">$c$</span>-Achse ist an der <span class="rp-blue">$Z$</span>-Achse ausgerichtet.
2. Die <span class="rp-green">$b$</span>-Achse liegt in der <span class="rp-green">$Y$</span><span class="rp-blue">$Z$</span>-Ebene, nahe der <span class="rp-green">$Y$</span>-Achse.
3. Die <span class="rp-red">$a$</span>-Achse wird dann durch die <span class="rp-green">$b$</span>- und <span class="rp-blue">$c$</span>-Achse festgelegt (Rechte-Hand-Regel).

![Anfangsorientierung: die a-/b-/c-Achsen des Kristalls relativ zu X/Y/Z, mit dem einfallenden Strahl entlang −Z](../../../assets/references/Coordinates2.png){width=300px}

Entsprechend gilt:

- Die Richtung aus dem Monitor heraus (zum Betrachter hin) ist die **[001]**-Zonenachse.
- Die Richtung nach rechts auf dem Monitor ist die Normale der **(100)**-Ebene.

> **Hinweis:** Die <span class="rp-blue">$c$</span>-Achse (= [001]) fällt immer mit <span class="rp-blue">$Z$</span> zusammen, in manchen Kristallsystemen fallen die <span class="rp-red">$a$</span>- und <span class="rp-green">$b$</span>-Achse jedoch **nicht** zwangsläufig mit <span class="rp-red">$X$</span> und <span class="rp-green">$Y$</span> zusammen.

## Euler-Winkel

Die Kristallorientierung wird mit drei Euler-Winkeln <span class="rp-olive">$\Phi$</span>, <span class="rp-cyan">$\theta$</span>, <span class="rp-magenta">$\Psi$</span> ausgedrückt, die in der Reihenfolge <span class="rp-blue">$Z$</span>–<span class="rp-red">$X$</span>–<span class="rp-blue">$Z$</span> angewendet werden (<span class="rp-magenta">$\Psi$</span>, dann <span class="rp-cyan">$\theta$</span>, dann <span class="rp-olive">$\Phi$</span>). Wenn alle drei Winkel null sind, lauten die zugehörigen Rotationsachsen:

| Winkel | Achse (wenn alle Winkel = 0) | Rang |
|-------|----------------------------|------|
| <span class="rp-olive">$\Phi$</span> | <span class="rp-blue">$Z$</span> | 1. (höchste) |
| <span class="rp-cyan">$\theta$</span> | <span class="rp-red">$X$</span> | 2. (mittlere) |
| <span class="rp-magenta">$\Psi$</span> | <span class="rp-blue">$Z$</span> | 3. (niedrigste) |

![Euler-Winkel-Rotationsachsen — Φ (gelb), θ (cyan), Ψ (magenta) — bei 0° (oben) und bei 15° (unten)](../../../assets/references/Coordinates3.png){width=400px}

Die drei Winkel bilden eine **Hierarchie**: <span class="rp-olive">$\Phi$</span> ist die höchste Drehung, gefolgt von <span class="rp-cyan">$\theta$</span>, dann <span class="rp-magenta">$\Psi$</span>. Die Richtung einer niedrigeren Achse hängt vom Zustand der höheren Drehungen ab. Zum Beispiel fällt bei <span class="rp-olive">$\Phi$</span> = <span class="rp-cyan">$\theta$</span> = <span class="rp-magenta">$\Psi$</span> = 15° die <span class="rp-olive">$\Phi$</span>-Achse weiterhin mit <span class="rp-blue">$Z$</span> zusammen, doch die <span class="rp-cyan">$\theta$</span>- und <span class="rp-magenta">$\Psi$</span>-Achsen stimmen im Allgemeinen mit keiner der Achsen <span class="rp-red">$X$</span>, <span class="rp-green">$Y$</span> oder <span class="rp-blue">$Z$</span> überein.

> Das Fenster **Rotationsgeometrie** kann diese Orientierung in einer beliebigen, experimentspezifischen Euler-Winkel-Konvention neu ausdrücken (z. B. um sie an ein Labor-Goniometer anzupassen). Siehe [4. Rotationsgeometrie](../../4-rotation-geometry.md).

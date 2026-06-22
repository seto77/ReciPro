# Anhang A1.2. Koordinatensystem für die Beugungssimulation

<!-- 260526Cl: 図(Coordinates4-5)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

Die Funktion **Beugungssimulator** simuliert das auf einem Detektor aufgezeichnete Beugungsmuster. Der Detektor ist eine endliche Ebene aus Pixeln, die in einem festen Abstand zur Probe angeordnet ist und gegenüber dem einfallenden Strahl gekippt sein kann. Um dies genau zu reproduzieren, sind die geometrische Beziehung zwischen Detektor und Probe sowie die Pixelgröße und Pixelanzahl des Detektors erforderlich. Zum grundlegenden (Orientierungs-)Koordinatensystem siehe [A1.1. Grundlegendes Koordinatensystem und Kristallorientierung](1-orientation.md).

!!! note "Z und Y unterscheiden sich vom Orientierungssystem"
    Im Detektor-Koordinatensystem ist <span class="rp-steel">$Z$</span> parallel zum Strahl und <span class="rp-steel">$Y$</span> zeigt nach unten. Dies unterscheidet sich vom Orientierungs-Koordinatensystem, in dem der Strahl entlang <span class="rp-blue">$-Z$</span> verläuft und <span class="rp-green">$Y$</span> nach oben zeigt. Das Detektorsystem folgt der üblichen Bild-/Detektorkonvention (Ursprung oben links, <span class="rp-steel">$Y$</span> nimmt nach unten zu).

## Vor der Drehung (Detektor senkrecht zum Strahl)

![Detektor-Koordinatensystem mit zum Strahl senkrechtem Detektor](../../../assets/references/Coordinates4.png){width=500px}

Es werden drei Koordinatensysteme definiert:

- <span class="rp-steel">**Reale Koordinaten** ($X$, $Y$, $Z$)</span> : 3D-kartesische Koordinaten in mm, mit der <span class="rp-steel">**Probe**</span> als Ursprung. <span class="rp-steel">$Z$</span> ist parallel zum Strahl; entlang <span class="rp-steel">$Z$</span> betrachtet zeigt <span class="rp-steel">$X$</span> nach rechts und <span class="rp-steel">$Y$</span> nach unten. Wenn der Detektor senkrecht zum Strahl steht, sind <span class="rp-steel">$X$ / $Y$</span> parallel zu <span class="rp-brown">$X'$ / $Y'$</span>.
- <span class="rp-brown">**Detektorkoordinaten** ($X'$, $Y'$)</span> : 2D-Koordinaten in mm auf der Detektorebene, mit dem <span class="rp-brown">**foot**</span> als Ursprung. <span class="rp-brown">$X'$ / $Y'$</span> zeigen auf dem Detektor nach rechts / unten und sind parallel zu <span class="rp-cyan">$X''$ / $Y''$</span>.
- <span class="rp-cyan">**Pixelkoordinaten** ($X''$, $Y''$)</span> : 2D-Koordinaten in Pixel-Einheiten, mit der <span class="rp-cyan">**oberen linken Ecke**</span> des Detektors als Ursprung, entlang der Pixelzeilen und -spalten des Detektors.

Wenn der Detektor senkrecht zum Strahl steht, fallen der <span class="rp-brown">**foot**</span> und der <span class="rp-red">**direct spot**</span> zusammen, und <span class="rp-red">**Camera length 1**</span> ist gleich <span class="rp-brown">**Camera length 2**</span>.

## Nach der Drehung (gekippter Detektor)

![Detektor-Koordinatensystem mit gekipptem Detektor](../../../assets/references/Coordinates5.png){width=500px}

Die Kippung des Detektors wird durch zwei Parameter beschrieben:

| Parameter | Beschreibung |
|-----------|-------------|
| <span class="rp-grass">$\varphi$</span> | Richtung der <span class="rp-grass">Rotationsachse</span> — ihr Winkel zur <span class="rp-steel">$X$</span>-Achse, gemessen in der <span class="rp-steel">$XY$</span>-Ebene (<span class="rp-steel">$Z$</span> = 0) |
| <span class="rp-grass">$\tau$</span> | Drehwinkel um diese Achse (Rechtsschraube) |

Sobald der Detektor gekippt ist:

- Der <span class="rp-red">**direct spot**</span> und der <span class="rp-brown">**foot**</span> fallen nicht mehr zusammen.
- <span class="rp-red">**Camera length 1** ($C_1$)</span> = Abstand von der <span class="rp-steel">Probe</span> zum <span class="rp-red">direct spot</span>.
- <span class="rp-brown">**Camera length 2** ($C_2$)</span> = Abstand von der <span class="rp-steel">Probe</span> zum <span class="rp-brown">foot</span>.
- Der Ursprung der <span class="rp-brown">**Detektorkoordinaten**</span> bleibt am <span class="rp-brown">**foot**</span>; der Ursprung der <span class="rp-cyan">**Pixelkoordinaten**</span> bleibt an der <span class="rp-cyan">**oberen linken Ecke**</span>.
- Die Richtungen <span class="rp-steel">$X$ / $Y$</span> fallen nicht mehr mit <span class="rp-brown">$X'$ / $Y'$</span> zusammen.

## Parameter-Glossar

| Begriff | Definition |
|------|------------|
| <span class="rp-steel">**Probe (Sample)**</span> | Das den einfallenden Strahl streuende Material; der Ursprung der realen Koordinaten |
| <span class="rp-steel">**Reale Koordinaten** ($X$, $Y$, $Z$)</span> | 3D-Koordinaten (mm) des Versuchsaufbaus; Ursprung an der Probe, <span class="rp-steel">$Z$</span> stets parallel zum Strahl |
| <span class="rp-red">**Direct spot**</span> | Schnittpunkt des einfallenden Strahls mit dem Detektor |
| <span class="rp-brown">**Foot**</span> | Der Fußpunkt des Lots von der Probe auf die Detektorebene; Ursprung der Detektorkoordinaten. Fällt nur dann mit dem direct spot zusammen, wenn der Detektor senkrecht zum Strahl steht. Im Überlagerungsbild-Modus wird die Position des foot in Pixelkoordinaten festgelegt |
| <span class="rp-brown">**Detektorkoordinaten** ($X'$, $Y'$)</span> | 2D-Koordinaten (mm) auf der Detektorebene; Ursprung am foot |
| <span class="rp-cyan">**Pixelkoordinaten** ($X''$, $Y''$)</span> | 2D-Koordinaten (Pixel) auf der Detektorebene; Ursprung an der oberen linken Ecke |
| <span class="rp-red">**Camera length 1** ($C_1$)</span> | Abstand von der Probe zum direct spot (mm) |
| <span class="rp-brown">**Camera length 2** ($C_2$)</span> | Abstand von der Probe zum foot (mm) |
| **Pixel size** | Seitenlänge eines (quadratischen) Pixels (mm); es werden nur quadratische Pixel unterstützt |
| **Detector width / height** | Anzahl der Pixel horizontal / vertikal |

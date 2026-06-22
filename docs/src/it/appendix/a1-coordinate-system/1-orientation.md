# Appendice A1.1. Sistema di coordinate di base e orientazione del cristallo

<!-- 260526Cl: 図(Coordinates1-3)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

Questa pagina definisce il **sistema di coordinate di base (di orientazione)** di ReciPro, utilizzato ovunque sia coinvolta una rotazione del cristallo (Finestra principale, Visualizzatore struttura, Stereogramma, Geometria di rotazione e simulazione di diffrazione), insieme al modo in cui vengono espresse l'orientazione iniziale di un cristallo e la rotazione con angoli di Eulero. Il sistema separato usato per posizionare il rivelatore nel **Simulatore di diffrazione** è descritto in [A1.2. Sistema di coordinate per la simulazione di diffrazione](2-diffraction.md).

---

## Definizione dell'orientazione

ReciPro utilizza un **sistema di coordinate destrorso** fissato al monitor:

| Asse | Direzione |
|------|-----------|
| <span class="rp-red">$X$</span> | A destra del monitor |
| <span class="rp-green">$Y$</span> | Verso l'alto sul monitor |
| <span class="rp-blue">$Z$</span> | Perpendicolare al monitor, verso l'osservatore |

![Assi delle coordinate di ReciPro mostrati sul monitor](../../../assets/references/Coordinates1.png){width=400px}

La **direzione del fascio** corrisponde alla direzione di osservazione (guardando dentro il monitor), cioè all'asse <span class="rp-blue">$-Z$</span>.

La maggior parte delle operazioni in ReciPro coinvolge solo *direzioni* (espresse come matrici di rotazione 3×3) e non richiede un'origine esplicita. L'unica eccezione è la funzione **Simulatore di diffrazione**, che necessita di un'origine esplicita — vedere [A1.2. Sistema di coordinate per la simulazione di diffrazione](2-diffraction.md).

## Direzione iniziale del cristallo

L'orientazione iniziale (al primo avvio o dopo **Reimposta rotazione**) è definita come segue:

1. L'asse <span class="rp-blue">$c$</span> è allineato con l'asse <span class="rp-blue">$Z$</span>.
2. L'asse <span class="rp-green">$b$</span> giace nel piano <span class="rp-green">$Y$</span><span class="rp-blue">$Z$</span>, vicino all'asse <span class="rp-green">$Y$</span>.
3. L'asse <span class="rp-red">$a$</span> è quindi determinato dagli assi <span class="rp-green">$b$</span> e <span class="rp-blue">$c$</span> (regola della mano destra).

![Orientazione iniziale: gli assi a / b / c del cristallo rispetto a X / Y / Z, con il fascio incidente lungo −Z](../../../assets/references/Coordinates2.png){width=300px}

In modo equivalente:

- La direzione che esce dal monitor (verso l'osservatore) è l'asse di zona **[001]**.
- La direzione verso destra sul monitor è la normale del piano **(100)**.

> **Nota:** L'asse <span class="rp-blue">$c$</span> (= [001]) coincide sempre con <span class="rp-blue">$Z$</span>, ma in alcuni sistemi cristallini gli assi <span class="rp-red">$a$</span> e <span class="rp-green">$b$</span> **non** coincidono necessariamente con <span class="rp-red">$X$</span> e <span class="rp-green">$Y$</span>.

## Angoli di Eulero

L'orientazione del cristallo è espressa con tre angoli di Eulero <span class="rp-olive">$\Phi$</span>, <span class="rp-cyan">$\theta$</span>, <span class="rp-magenta">$\Psi$</span>, applicati nell'ordine <span class="rp-blue">$Z$</span>–<span class="rp-red">$X$</span>–<span class="rp-blue">$Z$</span> (<span class="rp-magenta">$\Psi$</span>, poi <span class="rp-cyan">$\theta$</span>, poi <span class="rp-olive">$\Phi$</span>). Quando tutti e tre gli angoli sono nulli, i corrispondenti assi di rotazione sono:

| Angolo | Asse (quando tutti gli angoli = 0) | Rango |
|-------|----------------------------|------|
| <span class="rp-olive">$\Phi$</span> | <span class="rp-blue">$Z$</span> | 1° (il più alto) |
| <span class="rp-cyan">$\theta$</span> | <span class="rp-red">$X$</span> | 2° (intermedio) |
| <span class="rp-magenta">$\Psi$</span> | <span class="rp-blue">$Z$</span> | 3° (il più basso) |

![Assi di rotazione degli angoli di Eulero — Φ (giallo), θ (ciano), Ψ (magenta) — mostrati a 0° (in alto) e a 15° (in basso)](../../../assets/references/Coordinates3.png){width=400px}

I tre angoli formano una **gerarchia**: <span class="rp-olive">$\Phi$</span> è la rotazione più alta, seguita da <span class="rp-cyan">$\theta$</span>, poi <span class="rp-magenta">$\Psi$</span>. La direzione di un asse inferiore dipende dallo stato delle rotazioni superiori. Per esempio, quando <span class="rp-olive">$\Phi$</span> = <span class="rp-cyan">$\theta$</span> = <span class="rp-magenta">$\Psi$</span> = 15°, l'asse <span class="rp-olive">$\Phi$</span> coincide ancora con <span class="rp-blue">$Z$</span>, ma gli assi <span class="rp-cyan">$\theta$</span> e <span class="rp-magenta">$\Psi$</span> in generale non si allineano con nessuno degli assi <span class="rp-red">$X$</span>, <span class="rp-green">$Y$</span> o <span class="rp-blue">$Z$</span>.

> La finestra **Geometria di rotazione** può riesprimere questa orientazione in una convenzione di angoli di Eulero arbitraria e specifica per l'esperimento (ad esempio per adattarla a un goniometro di laboratorio). Vedere [4. Geometria di rotazione](../../4-rotation-geometry.md).

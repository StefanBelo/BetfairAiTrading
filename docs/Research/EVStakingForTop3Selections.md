# Staking Plans for Positive‑EV Horse Racing Selections: Dutching vs Kelly vs Heuristics

This article answers: “If I have a horse racing strategy that finds positive EV and I pick the best 3 +EV horses, which betting plan should I execute? Dutch the 3, back only the best EV horse, pick the biggest odds, or back all 3 if odds ≥ 3.5?”

Short answer: Use EV and calibrated probabilities to drive selection and staking. Prefer fractional Kelly sizing across the top +EV selections, or use equal‑profit dutching when you want smoother P&L and your combined win probability is sufficiently high. Avoid rules based solely on odds levels (e.g., ≥ 3.5) or “biggest odds” without EV justification.

---

## 1) Inputs and basic definitions

- For each horse i:
  - Decimal odds: $d_i$
  - Your win probability: $p_i$ (calibrated and conservatively hair‑cut if needed)
  - Edge/EV per €1 stake:  
    $$EV_i = p_i\,(d_i-1) - (1 - p_i)$$
    If exchange commission is $c$ on net winnings, replace $(d_i-1)$ by $(d_i-1)(1-c)$.

- Bankroll allocated to the race: $B$ (apply your per‑race cap, e.g., 1–2% of total bankroll)
- Risk parameter (fractional Kelly): $k \in [0.1, 0.5]$ (0.25 is a robust default)
- Combined win probability of your chosen 3: $P_\Sigma = p_1 + p_2 + p_3$ (mutually exclusive outcomes)

Sanity checks:
- Require $EV_i > 0$ after a probability haircut (e.g., multiply all $p_i$ by 0.9–0.95).
- Skip runners with marginal/fragile edge when liquidity is thin or prices are unstable.

---

## 2) Option A — Equal‑profit Dutching (3 selections)

What it does: set stakes so that profit is (approximately) the same whichever of the 3 wins.

- Choose total stake $S$ for the race (e.g., $S = k\,B$).
- Compute weights:  
  $$w_i = \frac{1}{d_i - 1}, \qquad W = \sum_j w_j$$
- Stake each runner:  
  $$s_i = S\,\frac{w_i}{W} = S\,\frac{\frac{1}{d_i-1}}{\sum_j \frac{1}{d_j-1}}$$
- Approx equal profit if any wins:  
  $$\text{Profit}_\text{win} \approx \frac{S}{W}$$

When to use:
- All 3 have $EV_i > 0$ and $P_\Sigma$ is reasonably high (e.g., ≥ 0.25–0.35).
- You want smoother P&L and simple operations (e.g., “Dutch to profit €X”).

Trade‑off: This is not EV‑optimal if one runner has a much larger edge; you trade a bit of expected growth for lower variance.

---

## 3) Option B — Back only the highest‑EV horse

When to use:
- One selection’s edge clearly dominates (e.g., its Kelly fraction dwarfs the others).
- Combined probability is low or secondary edges are weak/ill‑calibrated.

Pros: maximizes concentration on your best edge; simplest.
Cons: higher variance; drawdowns can be larger if your top edge is noisy.

---

## 4) Options to avoid as standalone rules

- “Back the biggest odds” — Only if it also has the highest EV. Otherwise it’s arbitrary risk.
- “Back all 3 if odds ≥ 3.5” — Odds thresholds don’t create edge; EV and calibrated $p_i$ do.

---

## 5) Kelly sizing (practical approach for 3 mutually exclusive runners)

Single‑runner Kelly fraction for decimal odds $d$ and win prob $p$:
$$f^{*} = \max\Big(0, \frac{d\,p - 1}{d - 1}\Big)$$

Practical multi‑runner approximation (simple and robust):
1. Compute $f^{*}_i$ for each (+EV) runner using the formula above.
2. Use fractional Kelly with $k$ (e.g., 0.25): tentative stake $s_i = k\,f^{*}_i\,B$.
3. If $\sum_i s_i > B$, scale all $s_i$ proportionally so total ≤ $B$.
4. Optional “dominant edge rule”: If one $f^{*}_i$ ≥ 60–70% of $\sum f^{*}$, back only the top 1 (or top 2) instead of all 3.

This closely tracks log‑optimal behavior without solving the full constrained optimization for mutually exclusive outcomes, and it naturally allocates more to your biggest edge.

---

## 6) Decision guide (flow)

1) Compute $EV_i$ and $f^{*}_i$ for the top 3 candidates; apply a 5–10% $p_i$ haircut.
2) If any $EV_i ≤ 0$ after haircut, drop that runner.
3) If $P_\Sigma \ge 0.25$ and edges are similar → Dutch equal profit with $S = k\,B$.
4) Else if one edge dominates (e.g., $f^{*}_\text{top} \ge 0.6\,\sum f^{*}$) → back only the top (or top 2) using fractional Kelly.
5) Enforce exposure caps (per race and per day) and liquidity/price discipline (min odds, keep orders).

---

## 7) Worked example

Assume bankroll slice $B = 1{,}000$ € for this race, $k = 0.25$.

Three +EV runners:
- $d_1 = 3.6$, $p_1 = 0.31$  →  $EV_1 = 0.116$,  $f^{*}_1 = \frac{3.6\cdot0.31 - 1}{2.6} \approx 0.0446$
- $d_2 = 6.8$, $p_2 = 0.19$  →  $EV_2 = 0.292$,  $f^{*}_2 = \frac{6.8\cdot0.19 - 1}{5.8} \approx 0.0503$
- $d_3 = 7.4$, $p_3 = 0.16$  →  $EV_3 = 0.184$,  $f^{*}_3 = \frac{7.4\cdot0.16 - 1}{6.4} \approx 0.0288$

Fractional Kelly stakes (tentative):
- $s_1 = 0.25\times0.0446\times1000 \approx 11.15$ €
- $s_2 = 0.25\times0.0503\times1000 \approx 12.58$ €
- $s_3 = 0.25\times0.0288\times1000 \approx 7.20$ €

Total ≈ €30.9 (≈3.1% of the €1,000 slice). If your per‑race cap were 3%, you would scale stakes down slightly to fit.

Equal‑profit dutch (if you prefer smoother P&L):
- Set $S = 30$ € (or $k\,B$).
- $w_1 = 1/2.6$, $w_2 = 1/5.8$, $w_3 = 1/6.4$, $W = w_1 + w_2 + w_3$.
- $s_i = S\,w_i/W$ → same profit if any wins: about $S/W$.

---

## 8) Practical controls and execution

- Probability haircut: multiply all $p_i$ by 0.9–0.95 to fight optimism bias; require $EV_i > 0$ after.
- Commission: on exchanges, adjust $(d-1)$ to $(d-1)(1-c)$ when computing EV and Kelly.
- Liquidity and slippage: set minimum acceptable odds and use keep orders; skip if prices collapse.
- Exposure caps: per race (e.g., ≤ 1–2% of bankroll) and per day (e.g., ≤ 6–8% total).
- Data quality: avoid overlapping information sources that double‑count the same signal.

---

## 9) Mapping to automation

- Dutching: trigger your “Dutch to profit €X” strategy when all 3 have $EV_i > 0$ and $P_\Sigma$ ≥ threshold.
- Fractional Kelly: compute $s_i$ per runner and send fixed‑stake back bets with min price guards.
- Failsafes: cancel or reduce stakes if live odds deviate materially from your inputs; skip if $P_\Sigma$ drops.

---

## 10) Bottom line

- Primary: fractional Kelly across the best +EV selections for superior long‑term growth, with conservative $k$ and caps.
- Alternate for smoother equity curve: equal‑profit dutching when $P_\Sigma$ is strong and all three are +EV.
- Avoid rules based only on odds levels or “biggest odds”—always size from EV and calibrated probabilities.

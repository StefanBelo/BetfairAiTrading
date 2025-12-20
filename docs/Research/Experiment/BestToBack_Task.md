# Horse Racing Market Analysis Prompt

**Instructions:**

1. **Execute on the Active Betfair Market:**
   - Retrieve and analyze all available data for every runner in the currently active Betfair market, including horse form, jockey stats, trainer performance, recent race comments, ground conditions, and other relevant factors.

2. **Calculate Scores:**
   - For each runner, compute individual data context scores on a standardized scale (e.g., horse form score, jockey score, trainer score) based on historical data and expert insights.
   - Combine these into a single composite score per runner using a weighted average (e.g., horse form 40%, jockey 30%, trainer 30%).

3. **Estimate Winning Probabilities (CRITICAL CALIBRATION):**
 
   **Step 3a: Apply Softmax with Temperature Scaling**
   - For each runner's composite score \(S_i\), compute raw probabilities:
     \(p_i^{raw} = \frac{e^{S_i / \tau}}{\sum_j e^{S_j / \tau}}\)
     (Set \(\tau = 10\) by default; adjust to 5 for high-confidence data or 15 for sparse data, with justification.)

   **Step 3b: Calibrate Against Market Consensus (REQUIRED)**
   - Compute market implied probability for each runner:
     \(p_i^{market} = \frac{1}{\text{Market Odds}}\)
   - Calculate total field probability:
     \(P_{field} = \sum_i p_i^{market}\)
   - Normalize to ensure sum to 100%:
     \(p_i^{market\_normalized} = \frac{p_i^{market}}{P_{field}}\)
   - Blend estimates:
     \(p_i^{final} = (w \times p_i^{raw}) + ((1 - w) \times p_i^{market\_normalized})\)
     (Default: \(w = 0.40\) for market weight 0.60; adjust \(w\) based on data quality per guidance table.)

   **Step 3c: Sanity Check**
   - Ensure \(\sum_i p_i^{final} = 1.00\).
   - Flag runners with \(p_i^{final}\) diverging >100% from \(p_i^{market\_normalized}\) for review.

4. **Calculate Expected Value (EV):**
   - For each runner, compute EV for a €10 stake:
     \(EV = (p_i^{final} \times \text{Market Odds} - 1) \times 10\)
   - Positive EV indicates value; if all positive, recalibrate.

5. **Calculate Optimal Stake Using the Kelly Criterion:**
   - Compute Kelly fraction:
     \(f^* = \frac{p_i^{final} \times (\text{Odds} - 1) - (1 - p_i^{final})}{\text{Odds} - 1}\)
   - Set Kelly Stake to 0 if \(f^* < 0\); else \(\text{Kelly Stake (€)} = f^* \times 10\).
   - Suggest fractional Kelly (e.g., 0.25x) for conservatism.

6. **Results Table:**
   Present in a table with columns: Horse | Horse Score | Jockey Score | Composite Score | Raw Softmax (%) | Market Odds | Market Implied (%) | Final Probability (%) | EV (€10 Stake) | Kelly Stake (€) | Key Form Evidence

7. **Calibration Transparency:**
   - Report blending weights (e.g., 40% analysis, 60% market).
   - Justify deviations >25%.
   - Acknowledge limitations (e.g., missing data).

8. **Summary and Recommendations:**
   - Highlight best value (positive EV, reasonable Kelly), overpriced (positive EV, low Kelly), underpriced (negative EV).
   - Recommend betting strategy, bet types, and risk management (e.g., diversify if EV spread is wide).

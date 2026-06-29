---
title: "LLM Capabilities in Strategy Creation"
aliases: ["LLM Capabilities in Strategy Creation"]
type: research
tags: [llm, strategy-creation, horse-racing, quantitative-analysis]
---

# LLM Capabilities in Betfair Strategy Creation

## Conversation Summary

We discussed the practical capabilities of Large Language Models (LLMs) when developing quantitative trading strategies for Betfair, specifically whether an LLM can natively perform calculations directly from a provided JSON dataset.

**Key Takeaways:**

1.  **Native Math Limitations:** Natively, LLMs are language prediction engines. They do not have built-in CPUs for executing complex arithmetic. Asking an LLM to accurately calculate exponential time-decay models, exact win probabilities, Poisson distributions, or fractional Kelly stakes directly from a large JSON payload is dangerous. It will likely hallucinate or lose precision across floating-point calculations.
2.  **What LLMs CAN Do Natively with JSON:**
    *   **Simple Logic & Filtering:** Easily evaluate rules (e.g., `daysSinceLastRun < 14 AND timeformStarRating >= 4`).
    *   **Qualitative Assessment:** Read and interpret textual sentiment (e.g., parsing Timeform `analystComment` or `commentFull` in historical performances to identify track preference or behavioral quirks).
    *   **Feature Engineering:** Look at a schema and suggest new ways to interpret the data.
3.  **The Correct Workflow:** The true power of an LLM is in **Code Generation and Automation**. Instead of asking the LLM to *do* the math natively, the LLM should be used to:
    *   Formulate the strategic logic.
    *   Write the execution code (e.g., Python/C# scripts) to parse the JSON and perform the mathematically rigorous statistics.
    *   Map the resulting signals into automated bot settings (like Bfexplorer Strategy Parameters).

---

## Example Strategy: The "Holistic Multi-Factor Value" Model

Based on the provided `RacingTvDataForHorses.json`, which contains exceptionally rich data, an LLM could design a comprehensive, multi-tiered strategy. If an LLM were to generate an evaluation script, it would leverage *every single available data point* from that JSON to create a composite **"True Value Score."**

Here is how a strategy would process that JSON structure programmatically, integrating both **Private Data** and **Market Intelligence**:

### Tier 0: Market Alignment (The "Bayesian Prior")
Before looking at the JSON, the strategy establishes the **Market Probability Baseline**.
*   **The Prior:** The live Betfair price is treated as the initial win probability (e.g., Decimal odds 4.0 = 25%).
*   **The Weighting:** The strategy script is designed to weight the Market Opinion at **85%** and the Private JSON Data at **15%**. This prevents the strategy from "over-betting" based on static data that the market may have already factored in.

### 1. Top-Level Heuristic Profiling (The "Baseline")

*   **Fitness Check:** Filter out or severely penalize horses where `daysSinceLastRun` is excessive (e.g., > 90 days), unless the `analystComment` specifically notes they go well fresh.
*   **Timeform Baseline:** Use `timeformRating` as the base class-level score. Add multipliers for `timeformStarRating` (e.g., 5 stars = 1.1x multiplier) and bonuses if `timeformTippedPlace` is 1.
*   **Weight Relief:** Factor in the `apprenticeClaim`. If a horse is carrying 61.22kg but has a 5kg claim (like *Desdemona*), recalculate their historical weight vs. current effective weight to spot hidden handicapping advantages.

### 2. Prompt & Trait Extraction (The "Specialist" Score)
*   The `prompts` object contains highly specific edges. The strategy script would parse:
    *   `finish`: "Finished 1st for Finishing Speed Percentage..." indicates late pace.
    *   `speed`: "Finished 1st for Top Speed..." indicates raw ability.
    *   `time`: "Finished faster than the Par Time..." indicates the horse ran in a genuinely fast race, upgrading its form.
*   **Action:** Assign a static point value for each active prompt triggered.

### 3. Career Win/Place Efficiency
*   Iterate through the `statistics` array.
*   Calculate the exact **Career Strike Rate** (`wins / total`) and **Place Rate** (`(wins + seconds + thirds) / total`).
*   This acts as a reliability floor. A horse with a 2% career strike rate needs a massive mathematical edge to be considered a Back.

### 4. Deep Historical Performance Analysis (The "Quantitative Engine")
This is where the script does the heavy lifting on the `performances` array:
*   **Time-Decayed Form:** Iterate through the `percentileScore` of past runs. Apply an exponential time-decay formula based on the `date` so that a win 8 days ago (like *Tuscan Point's* 2nd place) is weighted much heavier than a win 2 years ago.
*   **Conditions Matching (C&D / Going):** The script compares the current market (`Lingfield`, `5f Hcap`) to past `trackName`, `raceDistance`, and `raceClass`. It calculates a "Course & Distance Synergy Score" by averaging the `percentileScore` only for races matching the current conditions.
*   **Market Expectation Profiling:** Analyze past `startingPrice`. Does the horse consistently outperform its SP (high `percentileScore` at long odds), or is it a "false favorite" that underperforms when backed heavily?
*   **In-Play Trading Potential:** Average the `inPlayDominance` metric. If a horse frequently has a high `inPlayDominance` (e.g., > 0.80) even when it loses, it indicates the horse is a front-runner or travels very well. 
    *   *Trading Edge:* This flags the horse as an excellent **Back-to-Lay (DOB/B2L)** candidate, where the strategy enters a Back pre-race and places an automatic Lay in-play at lower odds, regardless of the final finish.

### 5. NLP Sentiment on Race Comments (The "Contextual Filter")
*   The LLM generates logic to perform keyword extraction on `commentFull` from past runs.
*   *Negative Flags:* "not settle fully", "carried head awkwardly", "weakened", "hampered start".
*   *Positive Flags:* "travelled well", "drawn clear", "unlucky", "denied a run".
*   If a horse had a low `percentileScore` in its last race, but the comment contains "denied a run" or "no room" (like *Tuscan Point's* August 2024 run), the strategy mathematically upgrades that specific past performance to account for bad luck.

### 6. The "Skeptic Filter" (Live Price Check)
Finally, the script performs a real-time comparison between its **Calculated Probability** and the **Live Betfair Price**:
*   **Confirmation:** If the JSON data indicates a strong Back, and the price is **Steaming** (decreasing), the strategy fires with high conviction (Market Confirmation).
*   **Divergence (The Red Flag):** If the JSON data indicates a strong Back, but the price is **Drifting** heavily (increasing), the strategy applies a "Skeptic Penalty." This accounts for the possibility that the "Crowd" knows something not contained in the historical JSON (e.g., a poor paddock appearance or a late injury).


### Final Output & Execution
The script aggregates these 5 layers into a single **"Probability Score."** It then compares this calculated probability against the live Betfair odds. 
*   If the strategy calculates the horse has a 25% chance of winning (True Odds 4.0), and the live market SP is 6.0, it automatically fires a **BACK** bet via the Bfexplorer API.
*   If the horse has a high `inPlayDominance` average but poor closing stamina in the comments, it fires a **Back-to-Lay** trading setting.

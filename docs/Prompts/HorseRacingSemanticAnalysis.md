# Horse Racing Semantic Analysis – Normalized EV Specification

## 🤖 Automated Processing Instructions

**Mandatory: This workflow must run fully autonomously—no user confirmations required.**

---

Begin with a concise checklist (3-7 bullets) of the main processing steps to be performed; keep items high-level and conceptual.

## Step-by-Step Automated Workflow

1. **Fetch Active Market:**
   - Use `GetActiveMarket` to retrieve the current active market.

2. **Obtain Racing Post Data:**
   - Use `GetAllDataContextForMarket` with `dataContextNames`: `["RacingpostDataForHorses"]` and the marketId from Step 1.

3. **Global Semantic Sign Extraction:**
   - Collect all `raceDescription` entries for all horses in the market.
   - Dynamically extract and classify positive and negative performance signs from the actual race descriptions in the current market (do not use a fixed or predefined set).
   - Build a set of distinct positive and negative signs for normalization, adapting to the language and phrasing present in the current data.

4. **Normalized Semantic EV & Market EV Computation:**
    - For each horse perform semantic scoring, then convert to an expected value relative to market prices.
    - Semantic Sign Processing:
       - Count occurrences of each distinct positive and negative sign in that horse’s descriptions (deduplicate per description phrase if needed to avoid repetition bias: cap repeat occurrences of the exact phrase within one description at 1).
       - RawPositive = number of distinct positive signs present for the horse.
       - RawNegative = number of distinct negative signs present for the horse.
       - GlobalDistinctPositive = total distinct positive signs across all horses; likewise GlobalDistinctNegative.
       - NormalizedPositive = RawPositive / max(1, GlobalDistinctPositive).
       - NormalizedNegative = RawNegative / max(1, GlobalDistinctNegative).
       - SemanticScoreBase = NormalizedPositive - NormalizedNegative (floor at 0 if negative for probability derivation but retain signed value for diagnostics).
    - Semantic Probability Derivation:
       - Let SumPositiveClipped = Σ over horses of max(NormalizedPositive - NormalizedNegative, 0).
       - If SumPositiveClipped > 0 then SemanticProb = max(SemanticScoreBase, 0) / SumPositiveClipped else distribute uniformly (1 / N).
    - Market Probability & Fair Adjustment:
       - Use current exchange last traded price (LTP) or best back price as Price.
       - ImpliedProbRaw = 1 / Price.
       - Overround = Σ (1 / Price) - 1 (can be > 0). Compute FairProb = ImpliedProbRaw / Σ (1 / Price).
    - Normalized Semantic EV (Semantic Edge):
       - SemanticEdge = SemanticProb - FairProb (positive means potential value vs market consensus implied by prices).
    - Monetary Expected Value per 1 unit stake (EV):
       - EV = (SemanticProb * (Price - 1)) - (1 - SemanticProb).
       - Equivalent: EV = (Price * SemanticProb) - 1.
    - Store: { Price, FairProb, SemanticProb, SemanticEdge, EV, RawPositive, RawNegative }.
    - Optional Confidence Heuristic: Confidence = (RawPositive + RawNegative) / (GlobalDistinctPositive + GlobalDistinctNegative) (used only for tie‑breaking; not required to output unless requested).

5. **Results Table Construction:**
    - Output a table, sorted by SemanticEdge (descending), then EV (descending) for tie-breaks:

       | Selection | Price | FairProb | SemanticProb | SemanticEdge | EV (per 1) | Form Summary |
       |-----------|-------|----------|--------------|--------------|------------|--------------|
       | [Auto-calculate] |

   - Present missing fields as “-”. Skip rows where all core data is absent.

6. **Strategic Recommendations:**
   - Top 3 positive normalized semantic EV picks with supporting info
   - Selections to avoid (lowest/negative normalized semantic EV), with reasoning
   - Form Analysis: Concisely summarize recent form or fallback (“N/A”/“No recent form data”).

7. **Error and Exception Handling:**
   - If API or data fields are missing, output a clear error headline.
   - Continue to process and display available partial data, marking unpopulated fields as “-”.
   - Suppress irrelevant tables if necessary.

---

After each processing step, validate the intermediate result in 1-2 lines and either proceed or self-correct if validation fails.

Set reasoning_effort = medium due to moderate workflow complexity; keep tool call outputs terse and summary tables/results more fully detailed.

## Output Structure (Strict Format)


### Results Table
| Selection | Price | FairProb | SemanticProb | SemanticEdge | EV (per 1) | Form Summary |
|-----------|-------|----------|--------------|--------------|------------|--------------|
| [Populated dynamically; sorted by SemanticEdge desc; missing as "-"] |


### Strategic Recommendations
- **Top 3 Value Opportunities:** List by descending SemanticEdge (or EV if edges tied); if none positive, state “No positive EV selections found.”
- **Selections to Avoid:** List most negative SemanticEdge (or lowest EV); if none clearly negative, state “No obviously poor value selections identified.”
- **Form Analysis:** Concisely summarize recent form or fallback (“N/A”/“No recent form data”).

### Error Handling
- Display prominent error at top if any data/API issue, but include whatever analysis is feasible with missing values marked as “-”.

---

**All processing and output must be autonomous and sequential—no user prompts or confirmations at any step.**

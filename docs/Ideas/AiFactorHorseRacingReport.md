# AI Factor Horse Racing Selection Report Strategy

**Source transcript**: [Loom video](https://www.loom.com/share/7a08ef6546ce476fbb357529ec37ebf1)

## Overview

This strategy is centred around a machine-generated horse racing report that combines multiple handicapping factors with an AI scoring model to produce a single **final selection** per race. The premise is not simply to supply horse names, but to provide enough structured information that the punter can either follow the machine pick directly or conduct their own value-based analysis using the underlying data.

---

## How the Report Works

For each race, the report lists all runners and scores them against a set of key factors. The output includes:

| Field | Description |
|---|---|
| **AI Factor** | A composite AI-derived score for each runner |
| **Fitness** | Indicator of the horse's current physical condition/recent race fitness |
| **Horse Distance** | Suitability for today's race distance based on form |
| **Going** | Compatibility with the current ground conditions |
| **Weight Band** | Whether the horse is running within a favourable weight range |
| **Seasonal Horse** | Whether the horse historically performs well at this time of year |
| **Interval** | Days since last run — optimal freshness vs. staleness |
| **Recommended Odds** | Odds the model considers fair value |
| **Calculated Odds** | Model's own probability-derived odds |
| **Top Speed** | Best speed figure recorded |

At the bottom of each report a **Final Selection** is highlighted — the single runner that best satisfies the combined factor criteria.

### Additional Signals

- **Jockey switches** are flagged explicitly. A high-profile jockey coming off a runner (or onto one) is treated as a meaningful signal, e.g. a noted jockey switching away from a fancied horse onto another runner.

---

## Betting Approach

1. **Follow the Final Selection** — Back the machine's stated pick each race. The selection targets value odds (demonstrated picks at 10/1, 8/1, etc.).
2. **Use the Report for Value Mining** — Bypass the final pick and use the factor table to identify overlays: horses with strong factor scores trading above their calculated odds in the market.
3. **Jockey Switch Filter** — Use flagged jockey moves as a secondary confirmation or stand-alone angle.

---

## Demonstrated Performance (Transcript Example)

Five consecutive races at an evening meeting, all selections provided by the report:

| Race Time | Selection | Result | SP (approx.) |
|---|---|---|---|
| 6:30 | (name redacted) | Won | 10/1 |
| 6:50 | Waiting All Night | Won | — |
| 7:05 | (name redacted) | Won | 8/1 |
| 7:25 | Silver Gun | Won | — |
| 7:40 | Smart Fission | Won | — |

Five winners from five selections in a single evening session.

---

## Implementation Ideas for BFExplorer / Automated Betting

- **Pre-race data enrichment**: Pull report factors (fitness, going, distance, weight band, seasonal, interval) into a structured selection context for use by a bot trigger.
- **AI Factor threshold gate**: Only execute a trade if the AI Factor score for the final selection exceeds a configurable threshold.
- **Odds alignment check**: Compare current Betfair SP / pre-race price against the report's *Recommended Odds*; only bet when market price ≥ recommended odds (value gate).
- **Jockey switch signal**: Flag races where the report records a jockey move; feed this as an additional boolean filter in a strategy condition.
- **Confidence accumulator**: Track how many of the six core factors (fitness, distance, going, weight, seasonal, interval) are green for the final selection before placing.

---

## Notes & Caveats

- Five consecutive winners in a single session is a strong result but a small sample; variance over a larger population of races is unknown.
- The strategy is presented as a low-cost report (£3 per report, no subscription) rather than a tipster service.
- Value is extracted both from following picks blindly **and** from using the factor data to inform independent analysis — two distinct use cases.
- Jockey market moves are already available via Betfair and third-party racing data APIs and can be automated without relying on the report exclusively.

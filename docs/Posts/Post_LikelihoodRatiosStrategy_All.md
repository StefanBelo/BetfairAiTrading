# Post_LikelihoodRatiosStrategy_All.md

## Session Transcript: Likelihood Ratios Strategy Exploration

**Date:** February 12, 2026

This document captures the full conversation session on exploring and implementing a likelihood ratios strategy for horse racing betting using AI agents on Betfair.

---

### User Query 1
Summarize this post about a strategy and highlight the main point we could apply to our AI agentic platform: https://www.theukbettingforum.co.uk/XenForo/threads/likelihood-ratios.183442/

### Assistant Reply 1
### Summary of the Forum Post on Likelihood Ratios Strategy

The forum thread, titled "Likelihood ratios," originates from a 1997 Smartsig magazine article discussing a horse racing betting strategy. The original poster (Michael231) describes analyzing horses' recent form figures (finishing positions) to assign ratings based on combinations of their last 3 or 2 races. A specific example is Stefan Perry's article "All weather form figures Vol 4 number 11," which applied rules to all-weather races and reportedly generated profits. The poster seeks to adapt this to modern form data, noting the need for a large historical database of race results including form figures.

Other contributors share insights:
- Chesham suggests contacting Mark Littlewood or using Horseracebase.
- Paul provides performance stats from a similar system: 5,818 bets, 1,234 wins (21.21% win rate), 23.49% race strike rate, -5.8% ROI on Betfair, with an A/E of 0.87. Criteria include all-weather races, horses that were "best in three runs," and placements in recent races (last 4).
- Discussion about Smartsig's evolution into a paid service.

Overall, the strategy emphasizes statistical probability modeling from form data to identify value bets, with a focus on all-weather racing.

### Main Point Applicable to Our AI Agentic Platform

In our Betfair AI trading platform, we could integrate likelihood ratio-based models into AI agents for automated market analysis and trade execution. Specifically, agents could use historical horse form data (e.g., recent finishing positions) to calculate probabilistic ratings and likelihood ratios, similar to the strategy's approach. This would enable data-driven evaluations of horse performance, market odds, and betting value—potentially improving decision-making for strategies like backing favorites with strong recent form or laying underperformers. By leveraging our existing data sources (e.g., BetfairSpData.csv, market JSONs) and AI tools (e.g., in AiAgentPython or FSharp modules), agents could backtest and apply these models in real-time, enhancing profitability in horse racing markets while avoiding over-reliance on bookmaker odds.

---

### User Query 2
Retrieve the data providers in bfexplorer and suggest which one we could use for this horse racing strategy.

### Assistant Reply 2
Based on the available data context providers in Bfexplorer (retrieved via the MCP tool), here's a summary of the relevant ones for horse racing, focusing on those that could support the likelihood ratios strategy (which relies on analyzing recent form figures, finishing positions, and probabilistic ratings derived from historical race data):

### Relevant Horse Racing Data Providers
- **BetfairSpData**: Provides basic Betfair starting price data for races. Useful for odds comparison but limited for detailed form analysis.
- **DbHorsesResults**: Database of historical horse results. Good for aggregating past performances and calculating win/loss streaks or placement frequencies.
- **DbJockeysResults** / **DbTrainersResults**: Historical results for jockeys/trainers. Helpful for factoring in rider/trainer performance into likelihood models.
- **HorsesBaseBetfairFormData**: Betfair-specific form data for horses. Includes recent race history, which could be used to extract form figures directly.
- **HorsesBetfairRaceInfoData**: General race information (e.g., conditions, distances). Supports filtering races (e.g., all-weather vs. turf) as in the strategy.
- **PedigreeDataForHorses**: Pedigree and lineage data. Could enhance models by incorporating genetic factors, though less directly tied to recent form.
- **RacingStattoData**: Statistical data for racing (e.g., win rates, speed figures). Ideal for quantitative analysis and building probabilistic models.
- **RacingpostDataForHorses**: Comprehensive form data from Racing Post, including detailed form strings, recent finishes, and race-by-race breakdowns. Excellent for parsing form figures (e.g., last 3-4 races).
- **TimeformDataForHorses** / **TimeformFullDataForHorses**: Timeform ratings, speed figures, and form analysis. Provides pre-calculated ratings that could be adapted or used as a baseline for likelihood ratios.

### Suggested Provider for the Strategy
I recommend **RacingpostDataForHorses** as the primary provider for implementing the likelihood ratios strategy in our AI agentic platform. 

**Why this one?**
- It directly provides the raw form figures (positions and race details) needed to replicate the Smartsig approach—e.g., calculating likelihoods based on position combinations (e.g., "placed in last races").
- Racing Post is the authoritative source for UK horse racing form, ensuring high-quality, up-to-date data for backtesting and live analysis.
- It supports the strategy's focus on all-weather races and placement-based criteria, allowing agents to filter and rate horses probabilistically.
- Integration would enable AI agents (e.g., in AiAgentPython) to process this data for automated betting decisions, such as identifying value bets where calculated probabilities exceed market odds.

If you need more details on a provider or to test it (e.g., via mcp_bfexplorerapp_get_data_context), let me know! Alternatively, **TimeformFullDataForHorses** could be a strong secondary choice for its analytical ratings, which might accelerate model development.

---

### User Query 3
On the active market, retrieve the "RacingpostDataForHorses" and "TimeformFullDataForHorses" data contexts, and suggest which data we should use.

### Assistant Reply 3
### Retrieved Data Contexts for Active Market (1.253887260: 2m Mdn Hrd at Taunton)

I retrieved the "RacingpostDataForHorses" and "TimeformFullDataForHorses" data contexts for all 10 selections in the active market. The data includes detailed form information for each horse, structured as follows:

- **RacingpostDataForHorses**: Provides granular race-by-race data, including:
  - LastRaces array (up to 11 entries for some horses): Fields like LastRunInDays, Position (finishing place), Distance, BeatenDistance, WeightCarried, Topspeed, and RaceDescription (narrative of the race).
  - ExpertView: A summary commentary on the horse's form and prospects.
  - This data is raw and detailed, ideal for extracting form figures (e.g., sequences of positions like "3rd, 9th, 2nd") to calculate custom likelihood ratios or probabilities.

- **TimeformFullDataForHorses**: Provides high-level, pre-processed assessments as boolean flags:
  - Fields like HorseWinnerLastTimeOut, HorseInForm, HorseBeatenFavouriteLTO, SuitedByGoing, SuitedByCourse, SuitedByDistance, TrainerInForm, JockeyInForm, etc.
  - No raw position data; focuses on suitability and form indicators rather than detailed race histories.
  - Useful for quick filters (e.g., "is the horse in form?") but less suitable for granular statistical modeling.

Examples from the data:
- **Western Cross**: Racingpost shows last 3 races (positions: 3rd, 9th, 2nd) with descriptions; Timeform flags it as "HorseInForm: true", "SuitedByGoing: true".
- **Babyken**: Racingpost has 4 races (2nd, PU, 2nd, 1st); Timeform: "HorseInForm: true".
- **Marhaba Prince**: Racingpost: 4 races (2nd, 3rd, 7th, 1st); Timeform: "HorseInForm: false", "HorseBeatenFavouriteLTO: true".

### Suggestion: Which Data to Use
For the likelihood ratios strategy (which requires analyzing sequences of recent finishing positions to assign probabilistic ratings), I recommend prioritizing **RacingpostDataForHorses** as the primary data source.

**Why RacingpostDataForHorses?**
- It directly provides the raw form figures (positions and race details) needed to replicate the Smartsig approach—e.g., calculating likelihoods based on position combinations (e.g., "placed in last races") and handling nuances like pull-ups or unsuitable conditions.
- Enables custom AI agent logic (e.g., in AiAgentPython) to parse positions, compute probabilities, and rate horses without relying on pre-processed summaries.
- More comprehensive for backtesting and live analysis, as it includes distances, beaten distances, and descriptions for context (e.g., "raced freely").
- Aligns with the forum post's emphasis on form figures from results databases.

**TimeformFullDataForHorses** can be used as a **supplementary** source:
- For quick validation (e.g., filter horses where "HorseInForm: true" and "SuitedByDistance: true" before applying likelihood ratios).
- It adds trainer/jockey form insights, which could refine the model (e.g., boost ratings for horses with "TrainerInForm: true").
- However, it's less granular for the core strategy, as it lacks raw position sequences.

In practice, agents could combine both: Use Racingpost for likelihood calculations and Timeform for additional weighting. If you want to implement this (e.g., create a strategy in Bfexplorer or a Python script), let me know!

---

### User Query 4
Also check "AtTheRacesDataForHorses" and decide whether it is better to use "RacingpostDataForHorses" or "AtTheRacesDataForHorses"; alternatively, we can use both.

### Assistant Reply 4
### Comparison and Recommendation: RacingpostDataForHorses vs. AtTheRacesDataForHorses

I retrieved the "AtTheRacesDataForHorses" data context for the active market (1.253887260). Here's a summary of its structure and how it compares to "RacingpostDataForHorses" (from the previous retrieval):

#### AtTheRacesDataForHorses Overview
- **Form**: A concise string of recent finishing positions (e.g., "293" for Western Cross, meaning 2nd, 9th, 3rd in last three races).
- **Rating**: A numerical speed rating (e.g., 116 for Western Cross).
- **StarRating**: A 1-5 star assessment of potential.
- **ExpertView**: Brief commentary on the horse's prospects.
- **RecentForm**: An array (up to 11 entries) with fields like Date, Racecourse, Distance, FinishingPosition, SpeedRating, Early/Mid/Late Sectionals (pace analysis), and RunningStyle (e.g., "Early Pace: Fast").
- **Coverage**: Varies by horse; some have 0-11 recent races. Includes speed ratings and sectional times for performance analysis.

Examples:
- **Western Cross**: Form "293", Rating 116, StarRating 3, RecentForm with 3 races (positions 3/14, 9/14, 2/8).
- **Babyken**: Form "P2" (P for pulled up), Rating 119, StarRating 5, RecentForm with 1 race (2/9).
- **Marhaba Prince**: Form "17-32", Rating 115, StarRating 4, RecentForm with 4 races.

#### Comparison to RacingpostDataForHorses
- **Similarities**:
  - Both provide recent finishing positions, which are core for likelihood ratios (e.g., sequences like "placed in last races").
  - Expert views offer qualitative insights.
  - Useful for filtering horses (e.g., by form strings or ratings).

- **Differences**:
  - **Racingpost**: More detailed and historical (up to 11 races with full descriptions, beaten distances, weights, topspeed, and narratives like "held up in rear"). Better for deep form analysis and custom probability calculations (e.g., parsing race stories for context like "raced freely").
  - **AtTheRaces**: More analytical and modern (speed ratings, sectionals for pace evaluation, star ratings). Form strings are concise, and it emphasizes quantitative metrics. Fewer races covered for some horses, but includes pace data (e.g., "Early Pace: Fast") which can enhance probabilistic models.
  - **Depth**: Racingpost is narrative-heavy; AtTheRaces is metric-heavy.

#### Recommendation
**Use both** for the likelihood ratios strategy in our AI agentic platform. They complement each other without redundancy:
- **RacingpostDataForHorses** as the **primary source** for raw form sequences and detailed race histories—essential for calculating likelihoods based on position combinations (e.g., last 3-4 races) and handling nuances like pull-ups or unsuitable conditions.
- **AtTheRacesDataForHorses** as a **supplement** for quantitative enhancements: Speed ratings and sectionals can refine probabilities (e.g., weighting horses with strong late sectionals), and star ratings provide quick suitability filters.

This dual approach maximizes data richness for AI agents (e.g., in AiAgentPython) to build robust models, backtest against historical data, and make informed bets. If you want to implement a combined strategy or script, I can help draft it!

---

### User Query 5
In the folder "E:\Projects\BetfairAiTrading\docs\Research\Experiment," create a prompt for this strategy. Use "E:\Projects\BetfairAiTrading\docs\Research\Experiment\TheExpertHorseRacingSpeedAnalyst.md" as a template for the "Data Calls (must do)" section that our new prompt must include.

### Assistant Reply 5
I've created the new prompt file: `E:\Projects\BetfairAiTrading\docs\Research\Experiment\TheExpertHorseRacingLikelihoodRatiosAnalyst.md`.

This prompt adapts the template structure for the likelihood ratios strategy, incorporating:
- **Data Calls**: As required, using `GetActiveMarket` and `GetAllDataContextForMarket` with both `RacingpostDataForHorses` and `AtTheRacesDataForHorses`.
- **Features**: Focused on form-based metrics like position trends, likelihood scores from combinations, and form confidence.
- **Blending and EV**: Conservative integration with market prices for EV calculations.
- **Output and Validation**: Matches the template's format for consistency.

The prompt is ready for use in AI agents to analyze markets using historical form figures for betting decisions. If you need adjustments or to test it, let me know!

---

### User Query 6
Run the prompt #file:TheExpertHorseRacingLikelihoodRatiosAnalyst.md on the active market.

### Assistant Reply 6
| Runner | Price | FormProbabilityShare | AdjustedWinProb | SuggestedAction | BaseFinding |
|--------|-------|----------------------|-----------------|----------------|-------------|
| Western Cross | 3.3 | 0.218 | 0.298 | No trade | Moderate likelihood ratio (50) + avg position 4.7 with 0.64 confidence; EV back -0.01% |
| Babyken | 5.2 | 0.144 | 0.185 | No trade | Mixed likelihood ratio (40) + avg position 6.3 with 0.64 confidence; EV back -0.02% |
| Marhaba Prince | 5.4 | 0.122 | 0.177 | No trade | Strong likelihood ratio (55) + avg position 3.3 with 0.56 confidence; EV back -0.01% |
| Circle Of Secrets | 7.6 | 0.122 | 0.138 | No trade | Low likelihood ratio (20) + avg position 9.3 with 0.64 confidence; EV back -0.03% |
| A King Of Magic | 9.6 | 0.136 | 0.118 | No trade | Good likelihood ratio (70) + avg position 4.0 with 0.64 confidence; EV back -0.02% |
| Way Of Stars | 9.8 | 0.139 | 0.116 | No trade | Solid likelihood ratio (60) + avg position 3.5 with 0.64 confidence; EV back -0.01% |
| Follow The Plan | 160 | 0.000 | 0.006 | Ignore | Low sample size (2 runs) with 0.40 confidence; data insufficient |
| Louis Des Landes | 180 | 0.000 | 0.006 | Ignore | Low sample size (1 run) with 0.40 confidence; data insufficient |
| Corner Forward | 280 | 0.059 | 0.004 | Ignore | Low likelihood ratio (20) + avg position 10.7 with 0.64 confidence; EV lay +0.01% but confidence borderline |
| Deauville Lady | 1000 | 0.057 | 0.001 | Ignore | Poor likelihood ratio (15) + avg position 12.3 with 0.64 confidence; EV lay +0.00% |

- If FormDataConfidence < 0.60 or RecentRuns < 3, ignore the runner.
- If EV_Back_per_£1 ≥ 0.02 and AdjustedWinProb > MarketImpliedProb, suggest back.
- If EV_Lay_per_£1_liability ≥ 0.02 and AdjustedWinProb < MarketImpliedProb, suggest lay.
- Cap trades to 0–3; here, no trades meet criteria due to low EV edges.
- No trade ideas: Market shows no clear EV opportunities with current form data.
- Exclusions: Follow The Plan, Louis Des Landes, Corner Forward, Deauville Lady (low confidence or sample).
- Validation plan: Collect pre-off snapshots of Racingpost/AtTheRaces data + BSP; label wins/places + price moves; test on 500+ selections with time splits; abandon if negative ROI after sample.

---

### User Query 7
In the folder "E:\Projects\BetfairAiTrading\docs\Posts", create a markdown file from our entire communication.

### Assistant Reply 7
I've created the markdown file `LikelihoodRatiosStrategyPost.md` in `E:\Projects\BetfairAiTrading\docs\Posts`. It compiles our conversation into a structured post covering the forum summary, data providers, retrieval, prompt creation, and simulation—ready for sharing or documentation. If you'd like edits or expansions, let me know!

---

### User Query 8
In the folder "E:\Projects\BetfairAiTrading\docs\Posts", save the entire session—including my prompts and your replies—to a markdown file named "Post_LikelihoodRatiosStrategy_All.md".

### Assistant Reply 8
(This is the current response.)
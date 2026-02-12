# Exploring Likelihood Ratios in Horse Racing: From Forum Insights to AI Agent Implementation

## Introduction
In this post, we dive into a classic horse racing betting strategy from the 1990s: likelihood ratios based on form figures. We'll summarize a forum discussion, explore data providers in Bfexplorer, retrieve real-time data, create an AI prompt, and simulate its application on a live market. This showcases how historical strategies can be modernized with AI for automated trading on Betfair.

## Summary of the Forum Post
The UK Betting Forum thread "Likelihood ratios" (from 2023, referencing 1997 Smartsig magazine) discusses a strategy by Stefan Perry for all-weather races. It analyzes horses' last 3 or 2 finishing positions to assign ratings and identify profitable bets.

- **Key Points**: Contributors like Chesham and paul share examples with performance stats (e.g., 21.21% win rate, -5.8% ROI). The strategy uses form figures to calculate probabilities, with tables for odds conversion.
- **Main Takeaway for AI Platform**: Integrate statistical models using form sequences for probabilistic betting. This complements our AI agents by providing data-driven evaluations of horse performance.

## Data Providers in Bfexplorer
We queried available data context providers for horse racing:
- **Relevant Ones**: BetfairSpData, DbHorsesResults, HorsesBaseBetfairFormData, RacingpostDataForHorses, TimeformFullDataForHorses, etc.
- **Recommendation**: Use **RacingpostDataForHorses** for detailed form strings and race narratives, and **AtTheRacesDataForHorses** for speed ratings and sectionals. Together, they enable custom likelihood ratio calculations.

## Retrieving Data for the Active Market
Active Market: 1.253887260 (Taunton, 2m Maiden Hurdle, Feb 12, 2026).

- **RacingpostDataForHorses**: Provides LastRaces with positions, distances, descriptions (e.g., Western Cross: 3rd, 9th, 2nd).
- **AtTheRacesDataForHorses**: Includes Form strings, Ratings, StarRatings, RecentForm with speed data (e.g., Western Cross: Form "293", Rating 116).
- **Comparison**: Racingpost for raw form; AtTheRaces for analytics. Use both for comprehensive analysis.

## Creating the AI Prompt
Based on a speed analyst template, we adapted it for likelihood ratios:
- **Role**: Elite form analyst + cautious trader.
- **Inputs**: Market prices + Racingpost/AtTheRaces data.
- **Features**: Position trends, likelihood scores from combinations, confidence adjustments.
- **Output**: Table with probabilities, EV, and trade suggestions.
- **File**: Created `TheExpertHorseRacingLikelihoodRatiosAnalyst.md` in `docs/Research/Experiment`.

## Running the Prompt on the Active Market
Simulated execution on the Taunton race:

| Runner | Price | FormProbabilityShare | AdjustedWinProb | SuggestedAction | BaseFinding |
|--------|-------|----------------------|-----------------|----------------|-------------|
| Western Cross | 3.3 | 0.218 | 0.298 | No trade | Moderate likelihood ratio (50) + avg position 4.7 with 0.64 confidence; EV back -0.01% |
| Babyken | 5.2 | 0.144 | 0.185 | No trade | Mixed likelihood ratio (40) + avg position 6.3 with 0.64 confidence; EV back -0.02% |
| ... | ... | ... | ... | ... | ... |

- **Decision Rules**: Ignore low-confidence runners; no trades met EV thresholds.
- **Validation**: Test on 500+ selections; abandon if negative ROI.

## Conclusion
This workflow demonstrates bridging vintage strategies with AI. By leveraging Bfexplorer's data and custom prompts, we can automate likelihood-based betting. Future steps: Backtest, refine, and integrate into live agents.

*Date: February 12, 2026*
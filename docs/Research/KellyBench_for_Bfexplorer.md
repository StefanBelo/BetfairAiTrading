# KellyBench Summary for Bfexplorer AI Agent Platform

Original article: https://www.gr.inc/releases/introducing-kellybench

## What KellyBench is

KellyBench is a long-horizon benchmark for evaluating AI agents in sports betting markets. It places agents in a simulated 2023–24 English Premier League season and asks them to maximise long-term bankroll growth using:

- historical match data
- advanced statistics
- lineups
- past results
- public odds

Agents must build models, identify betting edge, size bets, manage risk, and adapt as the season progresses.

## Key findings from the article

- Every evaluated model lost money on average across three seeds.
- The best model, Claude Opus 4.6, averaged a final bankroll of £89,035 from a £100,000 starting bankroll (about −11% return).
- Only Claude Opus 4.6 and GPT-5.4 avoided ruin in all three seeds.
- Most models struggled with coherent long-term behaviour, adaptation, and executing on their analysis.

## What KellyBench measures

- long-horizon sequential decision-making
- non-stationary market adaptation
- model development and retraining over time
- bet sizing and risk management
- strategy sophistication and process quality
- compute/tool usage cost

## Benchmark structure and evaluation

- Episodes run across a full football season (100–150 matchdays).
- Each agent run is averaged across 3 random seeds.
- Bankroll starts at £100,000 normalized.
- ROI and final bankroll are reported, plus ruin rates.
- Sophistication is scored with a 44-point rubric built with quantitative betting experts.

## Insights from the leaderboard

- Higher-performing models used systematic staking rules, capital preservation, and strategy updates in response to new data.
- The two best models were the only ones to avoid ruin across all seeds.
- Cheap open models finished episodes with lower costs but also much lower results.
- The rubric showed that even leading models scored less than one-third of the available points, indicating ample room for improvement.

## Relevance for Bfexplorer AI Agent platform

This article is highly relevant to our platform because KellyBench emphasizes the same problems Bfexplorer agents should solve:

- build and maintain models in a live market environment
- manage bankroll and avoid ruin
- size bets systematically rather than using ad-hoc stakes
- adapt to new information and changing market conditions
- evaluate performance on long-horizon outcomes, not just per-bet accuracy
- include process-based sophistication metrics alongside profit metrics
- measure compute and tool usage efficiency for practical deployment

## What we can use from KellyBench

- implement a long-horizon benchmark for Bfexplorer agents that simulates an entire season
- add a process-based rubric for strategy quality, not just ROI
- test for ruin and stability across multiple random seeds
- reward systematic bankroll preservation and risk management
- compare agents on both profitability and operational competence
- track how much tool/computation cost each agent uses per episode

## Opinion

KellyBench is a strong conceptual fit for Bfexplorer. It moves evaluation beyond simple prediction tasks and toward the real, complex world of sports betting strategy and management. The results are a useful caution: current language-model-driven agents can analyse and predict, but they still fail on sustained execution, strategy coherence, and risk control.

For our platform, the takeaway is clear: focus on agent process, robustness, and money management as much as prediction quality. A Bfexplorer benchmark inspired by KellyBench would help reveal whether an AI agent is actually ready for long-term betting deployment.

---

## References

- Original article: https://www.gr.inc/releases/introducing-kellybench
- Full paper: https://www.gr.inc/papers/KellyBenchPaper.pdf
- OpenReward endpoint: https://openreward.ai/GeneralReasoning/KellyBench

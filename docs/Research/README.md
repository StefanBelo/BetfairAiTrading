## Research & Market Insights Index

Curated, practitioner-focused research syntheses and academic-source summaries supporting quantitative and AI‑assisted Betfair trading.

### Core Studies
| Topic | File | Focus |
|-------|------|-------|
| Betfair Starting Price (BSP) | [BetfairSP.md](BetfairSP.md) | Efficiency, value benchmarking, strategic usage / avoidance patterns |
| Horse Racing Exchange Market Structure | [HorseRacing.md](HorseRacing.md) | Structured academic synthesis: efficiency, microstructure, BSP convergence, modeling approaches |

### Practical Application Themes
| Theme | Where to Start | Key Outputs |
|-------|----------------|-------------|
| BSP Value & Convergence | BetfairSP.md + Sections 5 & 7 of HorseRacing.md | BSP edge metrics, price ratio filters |
| Drift & Volatility Modeling | HorseRacing.md (Sections 2–4, 8) | Drift velocity, regime flags, movement feature set |
| Risk & Staking Frameworks | HorseRacing.md (Sections 6, 10, 11) | Capped Kelly, exposure throttles, robustness checks |
| Feature Engineering Cheat Sheet | HorseRacing.md (Section 14) | Minimal sets for drift, scalp, value filters |

### How to Use These Resources
1. Read BetfairSP.md to internalize BSP’s role as terminal consensus anchor.
2. Use HorseRacing.md to design movement + BSP hybrid signals (price ratio + drift velocity + imbalance).
3. Prototype features in notebooks (see project `tests/notebooks/`).
4. Feed curated metrics to AI prompts (see `docs/Prompts/`) for automated evaluation / execution.

### Coming Soon (Planned)
- Exchange Order Book Microstructure Deep Dive
- Cross‑Market Correlation & Portfolio Exposure Notes
- Adaptive BSP Projection Modeling Guide

> Disclaimer: Research content is educational. No guarantee of future profitability; always validate under realistic commission, liquidity, and latency conditions.


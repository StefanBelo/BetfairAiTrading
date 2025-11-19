# Separating Data Retrieval & Analysis: on4e Port Approach

Short post on the design choice to separate "base" horse racing data retrieval from the analysis rules that evaluate this data into a strategy prompt (the on4e port). This pattern is common in mature data systems and offers clarity, flexibility, and repeatable analysis.

## What I do
- Stage 1 — Base Data Retrieval: standardize fetching the core racing contexts (race metadata, selections, `DbHorsesResults`, `DbJockeysResults`, `RacingpostDataForHorses`, `TimeformDataForHorses`, etc.). Data is normalized and persisted unchanged to a common intermediate store.
- Stage 2 — Analysis & Rules: keep rules, scoring models, and strategy prompts separate. The analysis layers (LBBW, composite scoring, timeform adjustments) consume the standardized data and apply business logic.
- on4e Port: the `on4e` port acts as a boundary where raw contexts meet the analysis – a place to extract stable features and send them to strategy prompt rules for classification and EV calculations.

## Why separate? (Advantages)
- **Clear responsibilities**: data fetching and data evaluation are distinct tasks. Developers can update scrapers without touching scoring rules and vice versa. ✅
- **Reproducibility**: intermediate standardized datasets let you re-run analyses with the same inputs, which is essential for stable backtests and model audits. ✅
- **Faster iteration**: test changes to rules or LLM prompts quickly without re-fetching live data; helps with rapid model development. ✅
- **Modular re-use**: the same base dataset feeds multiple strategies (e.g., LBBW, composite EV, place-lay strategies) without duplicated retrieval code. ✅
- **Reduced race conditions**: decoupling minimizes the risk that a data update breaks analysis during a concurrent evaluation run. ✅

## Potential downsides
- **Extra storage/latency**: saving a normalized snapshot adds storage and a mild time penalty; not ideal for ultra-low latency in-play strategies. ⚠️
- **Stale data risk**: if analysis replays cached snapshots, it may miss last-minute changes (withdrawals, course updates, jockey switches). ⚠️
- **Complex orchestration**: more moving parts — retrieval jobs, staging, transformation pipelines — require orchestration and monitoring. ⚠️
- **Overfitting to snapshots**: if analysis only learns from snapshot data without accounting for live market dynamics, it may misprice markets that rely on live order-flow. ⚠️

## Practical mitigation strategies
- Provide a short TTL (time‑to‑live) for snapshots for live or in-play strategies; allow analysts to re-fetch key contexts on demand.
- Use light-weight change notifications for critical fields (jockey change, withdrawal) to trigger quick re-analysis rather than full re-fetch.
- Run sanity checks and no-bet flags on stale data to block automated staking from outdated snapshots.
- Version snapshots with metadata (retrieval timestamp, source versions) to enable reproducible backtesting and debugging.

## Quick example flow (on4e port)
1. Fetch contexts -> normalize into `race_snapshot` + `horse_snapshot`.
2. Persist snapshot -> forward to `on4e` port.
3. Analyzer service (LBBW, composite scoring) consumes `on4e` inputs, applies rules, returns `impliedOdds`, `valueScore` and `tradeRecommendation`.
4. Execution layer decides to place or wait, referencing live validity checks.

## Bottom line
Separating base data retrieval from the analysis rules in the `on4e` port leads to cleaner engineering, better auditability, and faster model iteration — with a small trade-off against latency and snapshot freshness. For most research and rule-driven strategies this separation is a net win; for ultra-low latency or pure market-making with sub-second requirements, consider a hybrid approach where the analysis layer can optionally read from direct live feeds instead of snapshots.

---
*Filed in posts: `on4e`, `horse-racing`, `data-pipeline`, `strategy-design`*
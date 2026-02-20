# Residual Liquidity Gate — Summary & BFExplorer Integration

Date: 2026-02-19

Original article: [Two Markets, One Race: Degeneracy and Automation in Betfair Horse Racing Markets](https://algorithmicsportsbetting.substack.com/p/two-markets-one-race-degeneracy-and) — Maurice Berk (Jan 24, 2026)

## Summary — key points from "Two Markets, One Race" (Maurice Berk)

- In-play markets are increasingly dominated by automation; in-play is fertile ground for low-latency automated strategies.
- Pre-off markets show persistent human biases (loss-chasing, payday effects, attention fatigue) that create predictable residual volume and price distortions.
- Traded volume has declined since 2022; liquidity considerations and smaller matched volumes must be central to execution and stake-sizing.
- Useful covariates for modelling volume/residuals: race class, course, handicap flag, race distance, number of runners, hour/day, days-to-payday, number-of-races-today, race-course×month, TPD coverage, prize-money.
- Practical insight: modelling expected volume and using residuals isolates periods of excess uninformed flow (opportunity) and thin/efficient markets (risk).

## Actionable ideas extracted

- Build a lightweight volume prediction model (GAM or spline-augmented GLM) and compute per-race residuals (actual - expected pre-off matched volume).
- Use residuals as a gating signal: large positive residuals → elevated uninformed flow (consider conservative pre-off bias plays); large negative residuals → thin market (disable pre-off strategies; favour small liquidity-aware in-play agents).
- Exploit calendar/course effects (Cheltenham, Ascot, etc.) as separate regimes or interaction features.
- Prioritise TPD-covered and longer-distance races for in-play automation (more time + telemetry).
- Add Prize Money to feature set where available; it may improve race-quality signal.

## Best single integration point for BFExplorer: Residual Liquidity Gate

Why: It encodes the article's main insight (human vs automated flow), is cheap to compute, and cleanly separates regimes for pre-off bias plays vs automated in-play scalps. Use it as the primary condition for enabling/disabling downstream agents.

### How to implement (concise)

- Compute expected pre-off volume per race using a model (GAM/spline or a lightweight GLM) from race metadata: class, course, handicap, distance, runners, hour/day, days-to-payday, number-of-races-today, course×month, TPD, prize money.
- Residual = actual_preoff_matched - expected_preoff_matched; normalize by expected volume.
- Thresholds (examples):
  - Residual > +X% (e.g., +50%): high uninformed flow → enable conservative pre-off bias strategies (small lays, fade short pushes, avoid backing favourites pushed by crowd).
  - Residual ≈ 0: normal market → allow cautious value-seeking or skip pre-off.
  - Residual < -Y% (thin): disable pre-off strategies; allow only small, liquidity-adjusted in-play automation.
- Secondary gate: TPD coverage flag + race distance → enable high-frequency in-play scalper agent.

### BFExplorer integration points (actionable API hooks)

- On market discovery/open: call `GetActiveMarket` → `GetAllDataContextForMarket` (race metadata + traded-volume history).
- Store the computed signal as a market data context using `mcp_bfexplorerapp_set_ai_agent_data_context_for_market`.
- Use `mcp_bfexplorerapp_execute_strategy_settings` or `mcp_bfexplorerapp_execute_strategy_settings_with_parameters` to enable/disable specific agent strategies (pre-off bias agent, in-play scalper) based on the gate.
- Use `mcp_bfexplorerapp_open_market` to begin monitoring and `mcp_bfexplorerapp_get_market` for periodic updates.
- Implement immediate kill via the same MCP controls if liquidity degrades or slippage crosses thresholds.

### Risk & execution controls (must have)

- Liquidity minimum (min matched pre-off and per-side depth) required before entry.
- Stake scaling proportional to available matched volume and inverse to residual uncertainty.
- Hard kill-switch: stop all agent trades on market-level adverse liquidity or latency spikes.
- Telemetry: log residual, strategy state, executed bets, slippage, and P&L to a dashboard.

### Quick MVP roadmap

1. Prototype expected-volume model (fast GLM or spline prototype) and compute residuals.
2. Add market-level gate and store via `mcp_bfexplorerapp_set_ai_agent_data_context_for_market`.
3. Wire a simple pre-off rule (fade short pushes when residual > threshold) and a tiny in-play scalper for TPD races.
4. Backtest, add monitoring and kill-switches, iterate.

---

File created by assistant on demand. Use this as the integration spec for the BFExplorer agentic platform.

# Analysis — "A Complete Guide to Sports Betting" (Robot James)

**Source:** https://robotjames.substack.com/p/a-complete-guide-to-sports-betting
**Date:** April 1, 2026
**Summary:** Part 1 introduces probability, expected value (EV), and three practical approaches to making money from sports betting: (1) handicapping (better models), (2) pricing a less-liquid market from a more-liquid market (pairs/pricing), and (3) exploiting recurring "angles" or timing/event inefficiencies. The article emphasizes EV, long-run thinking, variance, and building repeatable processes.

---

## Key takeaways relevant to BFExplorer

- Focus on expected value (EV) rather than short-term wins; track EV separately from realized profit.
- Prefer simple, repeatable edges (angles) before attempting hard handicapping.
- Use more-liquid markets to price less-liquid ones (pairs trading / cross-market pricing).
- In-play markets have predictable dynamics — build models or event rules to trade them.
- Robust data collection, automated backtests, and clear deployment controls are essential.

---

## Strategy ideas we can implement on BFExplorer

Each idea maps to concrete BFExplorer templates (see "BFExplorer templates" section) and includes compact implementation notes.

1. Value-Bet Scanner (Pre-match)
- What: Run a model vs market comparator to spot value bets pre-match.
- Why: Capture positive EV opportunities where your model probability > implied market probability.
- BFExplorer templates: `Place Bet`, `Stake Percentage Of Available Balance`, `Execute at Time`, `Place SP Bet`.
- Notes: Require `MinimumOddsDifference` (ticks), automated staking (percentage of bankroll or Kelly fraction), and safety: max exposure per market.

2. Market-Pricing / Pairs Trading
- What: Use a liquid reference market to price a related, less-liquid market and trade the mispricing.
- Why: Second method Robot James highlights — feasible with data and automation.
- BFExplorer templates: `Execute on Associated Market`, `Execute Strategies`, `Place Bet`, `Place Dutching Bets`.
- Notes: Build a pricing spread model, place hedges across markets, and use `Close Market Bet Position` to green up.

3. In-Play Model-Driven Trading (Next-goal / time-window models)
- What: Statistical model predicts short-term outcomes (next 10/15 minutes); trigger in-play trades automatically.
- Why: Robot James promises later parts will cover in-play models — this maps directly to automated trading.
- BFExplorer templates: `Football Strategy`, `AI Agent Strategy`, `Execute Till Target Profit`, `Execute Strategies`.
- Notes: Run shadow-mode validation for N matches, then canary stakes. Log every decision with `Record Market Selection Data`.

4. Angle/Event Trades (Lay-the-draw, post-goal re-entries)
- What: Rules like "lay draw and green up after a goal" or back Under X.5 and offset after X ticks, triggered by goal events.
- Why: Easiest practical edge for many traders; Robot James recommends starting with angles.
- BFExplorer templates: `Sequence Execution`, `Football Strategy`, `Execute on Associated Market`, `Tick Offset`, `Close Selection Bet Position`, `Execute at Time`.
- Notes: Implement as two coordinated strategies: (a) primary action sequence (entry + tick-offset/green-up) and (b) goal-watcher trigger (Correct Score / associated market). Use `Repeat Until` or `Sequence Execution` for multi-entry flows.

5. In-Play Scalping / Momentum (Quick in-out)
- What: Back then lay after small drift (or lay then back for momentum), using tick-based exits.
- BFExplorer templates: `Place Bet`, `Tick Offset`, `Trailing Stop Loss`, `Close Selection Bet Position`.
- Notes: Use `MinimumOddsDifference` and `ChaseOddsTimeout` to control fills; prefer small stakes and many repetitions.

6. Automated Backtest / Data CI
- What: Scheduled backtests that validate live results vs simulated ones and surface EV divergence.
- Why: Robot James stresses process; automated validation prevents silent data failures.
- BFExplorer templates: `Record Market Selection Data`, `Trading Data Recorder`, `Record My Market Selection Data`, `Race Data to Spreadsheet`.
- Notes: Run weekly validations; alert when live vs simulated correlation drops beyond threshold.

7. ML / AI Triggered Strategies
- What: Use an ML/AI model for selection scoring and triggers where applicable (horse racing or bespoke football models).
- BFExplorer templates: `Horse Racing Db-ML Trigger Bot`, `AI Agent Strategy`, `Execute Strategies`.
- Notes: Ensure explainability and a shadow period before production. Store model inputs/outputs for auditing.

8. Risk & Staking Automation
- What: Automated stake sizing (bankroll %) and limits + stop conditions.
- BFExplorer templates: `Stake Percentage Of Available Balance`, `Execute Till Target Profit`, `Stop Strategies and Cancel Bets`, `Limit Action Bot Execution`.
- Notes: Implement global max liability, per-market caps, and automatic shutdown on data/monitor failures.

---

## Mapping to BFExplorer templates (quick reference)
- Entry: `Place Bet`, `Place Bet - Be the First in Queue`, `Place Bet - Fill or Kill`
- In-play offsets / green-up: `Tick Offset`, `Trailing Stop Loss`, `Close Selection Bet Position`, `Close Market Bet Position`
- Sequencing / triggers: `Sequence Execution`, `Execute Strategies`, `Repeat Until`, `Execute on Associated Market`, `If Then Else`, `Execute at Time`
- Data / logging: `Record Market Selection Data`, `Trading Data Recorder`, `Record My Market Selection Data`
- Staking / risk: `Stake Percentage Of Available Balance`, `Execute Till Target Profit`, `Limit Action Bot Execution`, `Stop Strategies and Cancel Bets`
- ML / AI: `AI Agent Strategy`, `Horse Racing Db-ML Trigger Bot`

---

## Quick implementation priorities (starter roadmap)
1. Angle/Event Trades (fast to test, low infra) — implement `GoalWatcher + Sequence Execution` for lay-draw / back-under flows.  
2. Value-Bet Scanner (pre-match) — build simple model, connect to `Place Bet` with min tick filter.  
3. Data recorder + Backtest CI — enable `Record Market Selection Data` and weekly live-vs-sim validation.  
4. In-play model (medium-term) — build model offline, deploy with `Football Strategy` and `AI Agent Strategy` in shadow mode.  

---

## Suggested next steps (concrete)
- Prototype a two-file angle trade: `GoalWatcher` (Correct Score watcher) + `UnderGoals_Sequence` (Sequence Execution + Tick Offset). Use `Record Market Selection Data` during shadow runs.  
- Build a minimal pre-match model (Python) and wire it to BFExplorer via `Execute on Selections` + `Place Bet` for value detection.  
- Add a weekly backtest job that compares live vs simulated EV and alerts when divergence > X%.

---

*Document created: April 1, 2026 — generated and saved to docs/Ideas.*

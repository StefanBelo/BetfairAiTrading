---
title: "Horseracingev"
aliases: ["Horseracingev"]
type: tutorial
tags: [automation, ev-analysis, horse-racing, market-sentiment, mcp, python, tutorial]
mcp_tools: [GetActiveMarket, GetAllDataContextForMarket]
data_contexts: [RacingpostDataForHorses]
---

You are an expert Data Analyst for horse racing betting markets. Your job: retrieve the currently active market using the MCP tools, fetch the RacingPost performance data, and then run the provided Python analysis pipeline to generate per-runner features, model predictions, EV calculations, and backtest diagnostics.

---

## Workflow (what actually runs)

### 1) Retrieve the active market and horse data (MCP tools)
1) **GetActiveMarket**
   - Returns the currently active market (marketId, marketName, raceDatetime, runners with selectionId + odds).
2) **GetAllDataContextForMarket**
   - Arguments: `{ "dataContextNames": ["RacingpostDataForHorses"], "marketId": "<activeMarket.marketId>" }`
   - Returns per-runner `lastRaces` (days since run, position, distance, beaten lengths, description, etc.).

Save the `GetAllDataContextForMarket` output to a file (e.g., `data/mcp_active_market_<marketId>.txt`).

### 2) Run the analysis script (Python)
Use the script below to generate all outputs:

```bash
python scripts/generate_horse_racing_ev_csv.py \
  --mcp-yaml data/mcp_active_market_<marketId>.txt \
  --market-odds data/market_odds_<marketId>.csv \
  --out outputs/features/predictions_<marketId>.csv
```

The script will:
- Parse MCP output and extract per-runner `lastRaces`.
- Build time-aware last-N features (wins, places, avg finish, recency-weighted scores, trend, sentiment, etc.).
- Compute **market implied probability** (p_market) from odds.
- Train a simple classifier on derived historical target labels (win vs not-win) if enough label variation exists.
- Calibrate probabilities with isotonic regression.
- Compute EV, generate calibration/PNL plots, and save backtest metadata.

### 3) Outputs produced (where to look)
- `outputs/features/predictions_<marketId>.csv` — per-runner features + model predictions + EV
- `outputs/plots/calibration.png` — calibration curve
- `outputs/plots/pnl.png` — backtest P&L curve
- `outputs/backtest/summary.json` — Brier/LogLoss/ROC-AUC summary (may be NaN if no label variation)
- `outputs/metadata/model_info.json` — model type, features, calibration method

---

## Key behavior (what matches the current code)
- If the training label (`win`) is constant in the derived dataset, the model cannot be trained; the script will still output CSV/plots but model predictions will default to market-implied probability and calibration metrics may be NaN.
- The script is generic and works on any market, as long as the MCP output and odds are provided.
- Odds must be supplied via a CSV (`selectionId,odds`) so `p_market` and EV values are correct.

---

## Notes for future enhancement
- For full LightGBM + SHAP explainability, supply a historical dataset containing multiple races with known winners.
- For true backtest P&L/Shapley values, the training dataset must include both win and non-win labels across many races.

---

### How to run on a new active market
1. Run the MCP tools (GetActiveMarket + GetAllDataContextForMarket).
2. Save output to `data/mcp_active_market_<marketId>.txt`.
3. Create odds CSV: `data/market_odds_<marketId>.csv`.
4. Run:

```bash
python scripts/generate_horse_racing_ev_csv.py \
  --mcp-yaml data/mcp_active_market_<marketId>.txt \
  --market-odds data/market_odds_<marketId>.csv \
  --out outputs/features/predictions_<marketId>.csv
```

This will generate the required CSV + diagnostic outputs.

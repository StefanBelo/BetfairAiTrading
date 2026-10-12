# Scripts Directory

This directory contains helper scripts for maintaining the `BetfairAiTrading` project.

## Documentation Maintenance

### `maintain_docs_metadata.py`
This Python script is used to automatically manage the YAML frontmatter (metadata) for all markdown files in the `docs/` directory. 

**Features:**
- **Automatic Title Extraction**: Uses the first H1 header as the file title.
- **Context-Aware Typing**: Assigns a `type` (e.g., `strategy`, `prompt`, `guide`, `post`) based on content and file path.
- **Smart Tagging**: Extracts tags for sports (`horse-racing`, `football`), technologies (`fsharp`, `mcp`, `python`), and strategies (`scalping`, `ev-analysis`).
- **MCP Integration Tracking**: Automatically lists the MCP tools and Data Contexts used in each prompt or guide.

**Usage:**
Run this script whenever you add new documentation or want to refresh the metadata for Obsidian:
```bash
python scripts/maintain_docs_metadata.py
```

### `ingest_docs.py`
This script implements the **Karpathy Ingest** pattern. It flattens all markdown documentation (excluding READMEs) into a single, high-density text file (`docs_context.txt`) located in the project root.

**Why use this?**
- **LLM Super-Context**: Copy-paste the entire content of `docs_context.txt` into an AI chat to give it full knowledge of every strategy, prompt, and research note in your library.
- **Data Portability**: Easily move your documentation knowledge to other tools or LLM providers.

**Usage:**
```bash
python scripts/ingest_docs.py
```

### `validate_trading_report.py`
Checks that a handicapping report produced by the Trade prompts is **internally consistent**, by re-deriving everything the report could have computed rather than copied from its own model column.

**What it verifies:**
- The **model probability column totals `100.00`** — an unnormalised vector makes every `EV` in the report meaningless.
- The **market probability column is a genuine de-vig** of `1 / price`, reproducible from the prices printed in the report. A column that restates the model column shows up as `Edge = 0.00` on every row.
- Every **`EV` and `Edge`** equals the formula applied to that row's own figures. The most common failure is an `EV` built on a probability that appears nowhere in the report.
- The **`Rank` column is the model probability order**, descending — not the price order copied out of the market.
- The executed **direction, break-even comparison and profit target** in the parameters JSON agree with the favourite's numbers (`p > 100 / price` for a `Back`; target `min(round(|EV| x 100), 30)`).

**What it cannot check:** whether the probabilities are *good*. A report can be perfectly self-consistent and still wrong about the horses — this only proves the arithmetic and the cross-column relations hold.

**Usage:**
```bash
python scripts/validate_trading_report.py data/TestData/TradingReport.md
python scripts/validate_trading_report.py data/TestData/TradingReport.md --quiet
```
Exits `0` when every check passes and `1` otherwise, so it can gate a pipeline.

### `parse_strategy_logs.py` and `analyse_5oct.py`
Reconcile the AI strategy outputs in `data/AIStrategyOutputs/` with the settled Bfexplorer bet record. These produced `docs/Analysis/HorseRacingAIStrategyTradeResults_05October2026.md`.

**`parse_strategy_logs.py`** extracts, from every `*.md` in `data/AIStrategyOutputs/` (excluding `Old/`), the field table, the market favourite, `Rank 1`, the executed direction and the profit target, and prints one tab-separated record per log. It reports the logs it could not read rather than skipping them silently, and it stops at the first table so that a second table in the same output (a snapshot/entry comparison) is not absorbed as field data.

**`analyse_5oct.py`** joins those records to `get_all_bet_results()` and computes:
- how many trades **closed before the race start time** — the closing leg's bet time against the market `startTime`;
- the executed P&L;
- a bet-only counterfactual on the same selections and direction, held to the result;
- a `Back`-only counterfactual on the **market favourite**;
- a `Back`-only counterfactual on the model's **`Rank 1`**;
- the same favourite bet split by whether the output's `Rank 1` **agrees** with the market favourite.

It reads two staging files that must be built from tool output first: `_tmp_betresults.json` (the raw `get_all_bet_results()` payload — without it the executed-book frames are skipped) and `_tmp_outcomes.json` (`{marketId: {selectionName: "Winner"|"Loser"}}`, covering the selections the bet record does not reach: the untraded favourites, and `Rank 1` picks that were a different horse; the bet record overrides it where present). The outcome map is also readable in Appendix A of the report.

**Usage:**
```bash
python scripts/analyse_5oct.py
```

### `analyse_dutch.py`
Prices two **Dutch** selections on a day's reports and settles them. For every report from the given date it builds two sets — the three best `Rank` runners, and the three best `Qualifies` runners sorted by `Rank` — and, where `Σ(1/price) < 1`, computes the stakes that return a level €10 if any member wins (`total_return = 10 / (1 − Σ)`, `stake_i = total_return / price_i`; the loss if none wins is the whole outlay). Where the book is at or above 1, no stake produces a profit and the market is reported as **impossible**.

It reads `_tmp_winners.json` (`{marketId: winnerName}`), built from `get_market()` on the settled markets, and **skips** any market with no settled result rather than counting it as a loss. Prices are the **entry price** each report recorded. Note the outlay a €10 target implies: `10 × Σ/(1 − Σ)` — about €10 at a book of 0.5, €90 at 0.9, €370 at 0.974.

**Usage:**
```bash
python scripts/analyse_dutch.py 2026-10-06
```

---
*Note: The documentation scripts were created to improve organization and searchability within the Obsidian knowledge management app; `validate_trading_report.py` exists to catch arithmetic faults in generated handicapping reports; `parse_strategy_logs.py` and `analyse_5oct.py` exist to reconcile the generated outputs against the settled Bfexplorer bet record.*

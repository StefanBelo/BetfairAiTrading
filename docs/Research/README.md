# Research & Market Insights Index

>This directory contains curated research, practical guides, and tool references for quantitative and AI-assisted Betfair trading.

---

## 1. Core Studies

- **Betfair Starting Price (BSP)**  
  File: [BetfairSP.md](BetfairSP.md)  
  Focus: Efficiency, value benchmarking, strategic usage/avoidance patterns

- **Horse Racing Exchange Market Structure**  
  File: [HorseRacing.md](HorseRacing.md)  
  Focus: Academic synthesis—efficiency, microstructure, BSP convergence, modeling

- **Staking for Positive‑EV Selections (Top 3 picks)**  
  File: [EVStakingForTop3Selections.md](EVStakingForTop3Selections.md)  
  Focus: Dutching vs fractional Kelly, decision rules, risk caps, and automation mapping

- **Horse Racing EV Analysis Approaches**  
  File: [HorseRacingEVAnalysisAnalyzes.md](HorseRacingEVAnalysisAnalyzes.md)  
  Focus: Methodologies for EV calculation, scoring systems, probability normalization, and advanced modeling

- **AI Coding CLI Tools**  
  File: [CLIToolsForAICoding.md](CLIToolsForAICoding.md)  
  Focus: Verified command-line tools for AI-assisted coding and automation

- **Prompt Library**  
  File: [Prompts.md](Prompts.md)  
  Focus: Custom prompt templates for AI agent workflows (currently under development)

---

## 2. Practical Application Themes

- **BSP Value & Convergence**  
  Start with: BetfairSP.md and HorseRacing.md (Sections 5 & 7)  
  Key outputs: BSP edge metrics, price ratio filters

- **Drift & Volatility Modeling**  
  Start with: HorseRacing.md (Sections 2–4, 8)  
  Key outputs: Drift velocity, regime flags, movement features

- **Risk & Staking Frameworks**  
  Start with: HorseRacing.md (Sections 6, 10, 11)  
  Key outputs: Capped Kelly, exposure throttles, robustness checks

- **Feature Engineering Cheat Sheet**  
  Start with: HorseRacing.md (Section 14)  
  Key outputs: Minimal sets for drift, scalp, value filters

- **EV Analysis Methodologies**  
  See: HorseRacingEVAnalysisAnalyzes.md  
  Key outputs: Weighted scoring, regression modeling, probability normalization, and example applications

---

## 3. How to Use These Resources

1. Read `BetfairSP.md` to understand BSP’s role as terminal consensus anchor.
2. Use `HorseRacing.md` to design movement + BSP hybrid signals (price ratio, drift velocity, imbalance).
3. Explore `HorseRacingEVAnalysisAnalyzes.md` for EV calculation strategies and scoring frameworks.
4. Prototype features in notebooks (see project `tests/notebooks/`).
5. Feed curated metrics to AI prompts (see `docs/Prompts/`) for automated evaluation/execution.

---

## 4. CLI Tools for AI Coding

Explore active and publicly accessible command-line tools for AI-assisted coding, automation, and agentic workflows. See [`CLIToolsForAICoding.md`](CLIToolsForAICoding.md) for full details, URLs, and usage notes.

- **Codex CLI (OpenAI):** Coding agent for code generation, modification, and testing
- **Gemini CLI (Google):** Gemini AI integration for code understanding and automation
- **Claude Code CLI (Anthropic):** Terminal-based AI coding assistant
- **Qodo Command:** AI agents for engineering workflow automation
- **Aider:** AI pair programming CLI tool

---

## 5. Coming Soon (Planned)

- Exchange Order Book Microstructure Deep Dive
- Cross‑Market Correlation & Portfolio Exposure Notes
- Adaptive BSP Projection Modeling Guide

---

> **Disclaimer:** Research content is educational. No guarantee of future profitability; always validate under realistic commission, liquidity, and latency conditions.


---
title: "Ideas — Research & Integration Notes"
aliases: ["Ideas — Research & Integration Notes"]
type: index
tags: [bfexplorer, index, trading]
---

# Ideas — Research & Integration Notes

This folder contains idea notes, integration specs, and experiment summaries to guide development and prototyping for Betfair AI trading.

## Files

This folder contains idea notes, integration specs, and experiment summaries to guide development and prototyping for Betfair AI trading.

- **AgenticQuantResearchPipelineSummary.md**: Summary of building an agentic quant research pipeline (Source: Antonio Marrazzo).
- **AiFactorHorseRacingReport.md**: Horse racing factor analysis report.
- **AllWeatherBettingStrategies.md**: Strategies applicable in various weather conditions.
- **Andrew_David_Multi_Sequence_Staking_Plan.md**: Multi-sequence staking plan research.
- **BehavioralSinkBettingStrategies.md**: Strategies based on behavioral finance principles.
- **BetfairScalpingSystem.md**: Scalping system implementation notes for Betfair.
- **bet_sizing_strategies.md**: Various bet sizing strategies explored.
- **BFExplorer_ResidualLiquidityGate.md**: Residual-liquidity gating signal integration plan.
- **Big_Odds_Winners_Summary.md**: Summary of big odds winners analysis.
- **Books_AlgorithmicSportsBetting_Summary.md**: Algorithmic sports betting summary from books/literature.
- **ChaosTheoryBettingStrategies.md**: Strategies derived from chaos theory principles.
- **Converging_Factors_StrategySummary.md**: Summary of converging factor strategies.
- **CrisisOpportunitiesBfexplorerStrategies.md**: Strategy ideas for operational resilience and market segmentation.
- **Double_Qualifiers_Strategy.md**: Research on double qualifiers strategy.
- **FineFormMasterFormula.md**: Fine Form Master Formula research summary.
- **FineFormMasterFormula_StrategySummary.md**: Summary of the Fine Form Master Formula strategy.
- **GameTheory_BetfairStrategy.md**: Strategy based on game theory principles for Betfair.
- **HowBestToReadForm_UKBF_Summary.md**: Guide on reading UK/BF form data.
- **Out_Of_The_Ordinary_System.md**: Research on out-of-the-ordinary market systems.
- **PowerOfMarketMetaStrategy.md**: Strategy exploring the power of market meta-analysis.
- **PreOffDogMarketSignalConsistency.md**: Signal consistency analysis for pre-off dog markets.
- **ProblemsWithProbability_AgenticSummary.md**: Agentic summary on probability problems in betting.
- **Research_Transcript_ResidualLiquidityGate.md**: Transcript of residual liquidity gate research.
- **RobotJames_CompleteGuide_Strategies.md**: Complete guide to strategies from Robot James.
- **SectionalTimesAndMargins.md**: Sectional times and margin analysis ideas.
- **Signal_In_The_Wires_Liam_Pauling.md**: Signal analysis notes by Liam Pauling.
- **SmartBash2026_AI_Strategy_Insights.md**: AI strategy insights from SmartBash 2026.
- **SportsBettingForProfit2a_Handicapping_Summary.md**: Handicapping summary for sports betting profit.
- **Strategy_ConfidenceGate.md**: Strategy implementation using a confidence gate.
- **Tennis_Strategy_Research.md**: Research notes on tennis betting strategies.
- **The Art of Player Strength Models - SharpsResearch.md**: Analysis on player strength models (Sharps).
- **The Shape of Fear - Deriving Market Regimes from Skew.md**: Market regime derivation using fear/skew analysis.
- **TheResidualLiquidityGateAnalyst.md**: Detailed analyst notes for residual liquidity gate.
- **TheResidualLiquidityGateAnalyst_V1.md**: Version 1 analyst notes for residual liquidity gate.
- **VDW_Elementary_Mechanical_Procedure_Strategy_Report.md**: Strategy report on VDW mechanical procedures.
- **Volatility_Term_Structure_Arbitrage_Betfair.md**: Arbitrage strategy using volatility term structure.
- **Your First HFT Alpha - Flow Prediction and Order-Book Imbalance.md**: High-Frequency Trading alpha research.

Use these notes as lightweight specs for prototyping and reference. Add new idea files as needed.

## New Research Concepts

The following summary was added from `AgenticQuantResearchPipelineSummary.md`:

---
source: https://antoniomarrazzo.substack.com/p/how-i-would-build-an-agentic-quant?utm_source=multiple-personal-recommendations-email&utm_medium=email&triedRedirect=true
date: 2026-07-08
tags: [quantitative_finance, agentic_systems, quant_research, betfair]
---

# Agentic Quant Research Pipeline for Betfair Ideas

**Source Article:** [How I would build an agentic quant research pipeline from scratch](https://antoniomarrazzo.substack.com/p/how-i-would-build-an-agentic-quant?utm_source=multiple-personal-recommendations-email&utm_medium=email&triedRedirect=true)

## Summary for Betfair Agentic Platform Ideas

This article outlines a robust, multi-component architecture for building an agentic quantitative research pipeline. The core philosophy is that the system must be **read-only** when interacting with live market data to prevent accidental trading and maintain its role as a *research* tool.

The pipeline is built upon several key, interconnected components:

1.  **GBrain (Memory):** This serves as the durable, searchable memory for the research team. It uses two layers:
    *   A folder of plain Markdown files (the source of truth).
    *   A Postgres database with `pgvector` extension for fast, hybrid retrieval (combining conceptual vector search and exact keyword matching).
2.  **Lexfi MCP (Data Spine):** This is the read-only interface to market data. It provides authoritative facts—historical prices, fundamentals, insider transactions, etc.—that the agent can *read* from but cannot use to place trades. This separation of concerns is critical for safety.
3.  **The Loop Structure:** The research process itself is modeled as a loop with six parts: figure out what needs doing, plan it, do the work, check the result against the goal, and feed failures back in.
4.  **Key Structural Elements:**
    *   **SKILL.md:** Contains the reusable instructions/conventions for a specific behavior (e.g., "running an audit").
    *   **STATE.md:** Acts as short-term, per-job working memory, tracking what has been tried in the current cycle.
    *   **Verifier Agent:** A separate, stronger agent whose sole job is to grade the primary agent's work, preventing self-deception and false positives (the "multiple-testing problem").
5.  **Building Order & Safety:** The recommended build order emphasizes starting narrow: wire Lexfi and GBrain first, then build the backtest harness, followed by the hypothesis loop, and finally adding external connectors like Slack/Telegram.

**Key Takeaway for Betfair:** Focus on building a system where research is **auditable** (evidence trail) and **safe** (read-only data access). The value lies not in generating signals, but in creating an automated process that rigorously *tests* hypotheses against historical data using the structured memory and read-only market feed.

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
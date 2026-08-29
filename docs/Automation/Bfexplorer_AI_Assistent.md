---
title: Bfexplorer AI Assistant Integration
date: 2026-08-29
tags: [Bfexplorer, ai-agentic, trading-strategy, automation]
aliases: Bfexplorer AI Assistant Documentation
---

# 🤖 Bfexplorer AI Assistant Integration Guide

**Overview:**
The Bfexplorer application is built to be highly agentic. By connecting an external AI model (like a large language model) to the platform, users gain the ability to query and analyze virtually any aspect of betting or trading strategies directly within the environment.

It's exciting to see that I am not alone in leveraging AI for Betfair betting and trading! Another user, Layteruk10, shared their valuable experiences in this article:
[http://Bfexplorer.net/Community/BlogContent/660](http://Bfexplorer.net/Community/BlogContent/660)

**Use Case Example: Leveraging the Assistant**
To demonstrate the power of this integration, I used content from Layteruk10's article and posed a specific question to the Bfexplorer Assistant: *"Could you please analyze this content and suggest which Bfexplorer features users could leverage in an AI agentic workflow?"*

The assistant provided a detailed analysis, confirming that connecting advanced LLMs to the core platform unlocks powerful new avenues for automated strategy development.

**[Bfexplorer Assistant Reply]**

![Bfexplorer_AI_Assistent](/docs/Automation/images/Bfexplorer_AI_Assistent.png)

I'll dig into both the article and the actual Bfexplorer MCP capabilities to ground my analysis. Let me start by reading the attachment and exploring what Bfexplorer exposes.

The attachment is fully inlined. Now let me probe the Bfexplorer features most relevant to AI-agentic workflows — especially the "AI Agent Strategy", "Windows PowerShell Executor", and the data context machinery.

Let me look at the exposed resources and check the currently monitored state to ground the analysis in real Bfexplorer data.

Let me pull a few more templates that are directly relevant to AI-agentic control, plus the AI-data-context tools.

Let me check the two AI-related strategy settings and the AI data-context feed tool to round out the picture.

I've now grounded this in the *live* Bfexplorer MCP server (I enumerated all 112 strategy templates, 26 data-context providers, ~18 command groups, the exposed resources, and the actual strategy settings on this machine). Here's my analysis.

---

## First, an honest read of what you built

Your loop is **mechanical, not agentic** — and that's exactly how you described it in "Where it stands." A standalone Python poller watches a private CSV feed, and on a match it activates a market+selection and fires a named Strategy Setting. Claude sat in the *developer seat* (helping you build), not in the *runtime loop*. That's a perfectly sane v1, but it means you're using the MCP server to *remote-control* Bfexplorer rather than using the agentic machinery Bfexplorer already ships.

The biggest thing you appear to have missed: **Bfexplorer has first-class AI-agent primitives built in** — including a strategy that calls an LLM itself, and a whole "data context" layer for feeding an agent real market data. None of that is in your write-up.

---

## The Bfexplorer features I verified exist (that matter for agentic work)

I checked these against the live server, so this isn't from marketing docs:

| Feature | What it is | Agentic relevance |
|---|---|---|
| **AI Agent Strategy** (template 10400) | A control-flow strategy that sends a prompt to an LLM endpoint (Text/File/Internet source, `Model`, `Endpoint`, `ApiKey`, `AllowedTools`, `ShowOutput`) gated by market `EntryCriteria` | **The agent can live *inside* Bfexplorer** and be triggered by market conditions — no external bridge required |
| **AI Trading Strategy** (template 16002; you already have a setting for it) | ML/AI trading bot: `SelectionCriteria`, `ToBackCriteria`/`ToLayCriteria`, profit targets, smart exit, allowed trades, trigger vs summarized execution | Full agent-style decisioning with Bfexplorer still owning the bet placement |
| **Windows PowerShell Executor** (template 2010) | Runs a PowerShell command or external program as a strategy step, gated by entry criteria | Your poller can become a *strategy step* — Bfexplorer runs the script, instead of you running Bfexplorer |
| **26 data-context providers** | Price history, candlesticks, traded prices, Weight of Money, plus Timeform/Racing Post/AtTheRaces/Oddschecker/OLBG form and odds data | This is the **context** an agent should reason over — far richer than a CSV pick |
| **`setAiAgentDataContextForMarket` / `getAiAgentDataContextFeed`** | MCP endpoints to write agent-produced JSON back into a market, and to read the agent's own feed | Closed loop: agent reasons → writes context → strategies consume it |
| **Control-flow templates** | `Execute Strategies`, `Sequence Execution`, `Concurrent Execution`, `If Then Else`, `Repeat Until`, `Execute on a Selection`, `Execute on Associated Market`, `Scheduled Execution`, `Stop Strategies and Cancel Bets` | Lets you *compose* an agent's decisions into multi-step trading logic |
| **Strategy Executor** (`BotExecutorForSelectionsView`/`BotExecutorView` commands) | Apply a strategy to many markets and start it; per-market output messages | Scales one agent decision across N markets; gives you per-market logs |
| **`Send Text to Console`** (template 10401) | Sends text to a named process window | Cheap inter-process signaling (an alternative to your poller's hand-off) |
| **Resources** (`betfairMonitoredMarkets`, `bfexplorerStrategySettings`, `bfexplorerStrategyTemplates`, `activeBetfairMarket`) | Self-describing snapshots exposed over MCP | **This is the "manual nobody wrote" — it's machine-readable on day one** |
| **Recording strategies** (`Record Market Selection Data`, `Trading Data Recorder`, `Price History Candle/Offer Analysis Trigger`) | Capture market/selection data on an interval | Build the *training/backtest dataset* an AI strategy needs |

---

## Mapping features to your six phases

**Phase 01 (the manual).** The discovery work you did by hand is now free: any agent can enumerate every template, its parameters and types, every command, and every data provider via MCP — I just did it live. The four exposed *resources* are a ready-made reference doc. Advice: turn your hard-won reference doc into an MCP resource or a strategy-settings comment so the agent reads the *same* source you verified, not a stale copy.

**Phase 02 (getting Claude in).** Your `.mcpb` + `mcp-remote` bridge is fine and I'd keep it. But note Bfexplorer also speaks *as a client*: the **AI Agent Strategy** template can call an LLM endpoint directly from inside the strategy engine. So you have two viable topologies:
- *External agent* (your current Claude Desktop + MCP approach), or
- *Embedded agent* (AI Agent Strategy calling your model, gated by market criteria).

Both share the same boundary you correctly insisted on — **Bfexplorer still owns every bet**.

**Phase 03 (the private feed).** This is where you have the most headroom. Your poller is a genuine workaround, but Bfexplorer gives you three in-engine levers:
1. **AI Agent Strategy with `PromptDataType = Internet/File`** — the strategy can pull remote/local content as prompt context itself.
2. **Windows PowerShell Executor** — wrap your polling logic as a strategy step, so Bfexplorer triggers the script when `EntryCriteria` match, instead of a script polling Bfexplorer.
3. **CSV + local-file criteria** — Bfexplorer already reads local files; your bridge could drop the matched pick into a local file that a strategy's entry criteria consume.

Your boundary lesson ("notice and hand off" vs "decide and act") is exactly right and should be preserved no matter which of these you use.

**Phase 04 (one named setting per signal).** Your convention maps cleanly onto the engine's design: Template = logic, Setting = named instance. MCP even makes this auditable: an agent can `getAllStrategySettings`, match provider names, and **fail loudly** (your lesson) when a name is missing. One addition worth stealing from the template catalog: **`StrategyReference`** (15-char tag) — tag every execution with the provider so results/`getAllBetResults` are attributable per signal source, not just per strategy.

**Phase 05 (API limits).** I confirmed on the live server:
- Strategy creation is create-only (a re-create is a silent no-op), so an agent should treat `createStrategySettings` as idempotent "ensure exists," and remember parameter edits are GUI-only.
- There's no live matched-bet status on open markets — your "first strategy successfully triggered" proxy is still the right call. Better: use **`ShowMarketOutputMessages` / per-market output messages** and the **ai-agent data context feed** to persist "triggered at T" state, so the proxy is *observable*, not just labeled in a log.

**Phase 06 (survival).** The Startup-folder shortcut works; keep it. But note the embedded topology removes most of the babysitting problem — if the *agent lives inside the strategy*, there's no separate bridge process to keep alive; Bfexplorer's own strategy engine just runs. Your logging-per-poll-cycle habit is worth keeping regardless (the output-messages views give you the same readability in-engine).

---

## A recommended agentic architecture (if you want to go further)

Four layers, in the order an agent should think about them:

1. **Executor (trusted)** — Bfexplorer strategy engine. All stakes, odds, order placement stay here. *Unchanged from your design; this is the non-negotiable safety boundary.*
2. **Context** — feed the agent real data via the data-context providers (price history, WOM, form). Use the recording strategies to accumulate a dataset, then let the agent reason over *its own logged history* rather than only the feed's picks.
3. **Decision agent** — pick one: (a) your external Claude bridge (fine), (b) Bfexplorer-native **AI Agent Strategy** calling your model with `AllowedTools` restricted to the safe command set, or (c) hybrid — external agent for research/context, embedded AI Agent Strategy for the trigger. Restrict `AllowedTools` — that's your "drawn in code, not just intent" boundary, now enforced by the engine.
4. **Composition + observability** — wrap agent decisions in control-flow templates (Execute → If/Then/Else → Sequence → Stop/Cancel) and use the Strategy Executor to fan one decision across many markets. Verify with live reads (`getMarket`, `getAllBetResults`, output messages) — your "verify, don't assume" rule, applied at runtime instead of only at build time.

---

## Concrete next steps for a novice (in order of effort)

1. **Try the AI Agent Strategy template now** — it's a single setting you can point at your existing LLM endpoint. See what happens when the *engine* holds the prompt instead of a script.
2. **Wire the private feed into PowerShell Executor** — the least risky way to test whether Bfexplorer can own the polling step too.
3. **Start recording data** (Record Market Selection Data on your monitored markets) — this is the prerequisite for anything genuinely "AI" later, and it costs nothing now.
4. **Add `StrategyReference` tags** per provider so your settled results become attributable — this turns your results view into a feedback loop an agent can actually learn from.
5. **Keep your two best patterns** — the hard boundary on money decisions, and the loud failure on unmatched providers. Those are the parts of your build that are *more* correct than most of what I see in the wild.

If you'd like, I can go further and actually create one of these strategy settings (e.g., an AI Agent Strategy configured to your endpoint, or a PowerShell Executor step) against a market on this machine so you can see the shape of it — just say which.
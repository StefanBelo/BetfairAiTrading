---
tags: [bfexplorer, onboarding, documentation]
date: 2026-08-28
aliases: Bfexplorer Tool Discovery Guide
---

# Getting Started with Bfexplorer: Understanding Available Tools

## Context
This document outlines the best practice for a new user of the Bfexplorer application to begin their exploration. Before attempting complex tasks, it is highly recommended to understand the full scope of tools available within the platform.

## Recommended First Action (For Human Users)
The most effective first step is to request a comprehensive list and categorization of all usable bfexplorer tools:

> "Please list all usable bfexplorer tools and group them by purpose."

## Explanation (Why this approach?)
This initial inquiry serves as a crucial discovery phase for *you*, the user. By asking the system to list and categorize *all* available tools, you achieve several goals:
1. **Scope Definition:** You immediately gain a complete map of everything Bfexplorer can do within the betting domain.
2. **Understanding Boundaries:** You learn what functionalities exist (e.g., managing markets, executing strategies, retrieving data) and understand the system's limitations.
3. **Structured Knowledge:** Seeing tools grouped by purpose (e.g., "Market Management," "Strategy Execution," "Data Retrieval") helps you build a structured mental model of the application's full feature set, making subsequent complex requests more accurate and efficient.

This foundational understanding is necessary before attempting any specific trading or analysis task, ensuring that your actions are grounded in the system's complete capabilities.

Here is the response you could have for the prompt:

![UnderstandingAvailableTools](/docs/Automation/images/UnderstandingAvailableTools.png)

Here are all 25 usable bfexplorer tools, grouped by purpose (based on their tool descriptions).

## 1. Market Monitoring & Discovery
Tools to find, open, and activate Betfair markets/selections.

| Tool | Purpose |
|------|---------|
| `getMonitoredMarkets` | List all markets currently being monitored |
| `getActiveMarket` | Get the currently active (monitored) market |
| `getMarket` | Retrieve a specific monitored market by ID |
| `openMarket` | Open a market for monitoring |
| `activateMarketSelection` | Activate both a market and a selection |
| `getMyFavouriteBetEvents` | List favourite Betfair event names |
| `getMyFavouriteBetEventMarkets` | List favourite bet event markets by event name |

## 2. Data Context (market/selection data & AI context)
Tools to retrieve and manage data context providers, per-market/selection data, and the AI agent's context.

| Tool | Purpose |
|------|---------|
| `getAvailableDataContextProviders` | List available data context providers |
| `getDataContext` | Retrieve data context for Betfair data |
| `getDataContextForMarketSelection` | Retrieve data context for a specific market selection |
| `getAllDataContextForMarket` | Retrieve all data context for a market and all its selections |
| `getAiAgentDataContextFeed` | Retrieve the AI agent's data context feedback (recent results) |
| `setAiAgentDataContextForMarket` | Set the AI agent's data context for a market (JSON data) |

## 3. Strategy Management
Tools to inspect, create, and execute betting strategies.

| Tool | Purpose |
|------|---------|
| `getAllStrategyTemplates` | List all available strategy templates |
| `getStrategyTemplate` | Get details of a specific strategy template |
| `getAllStrategySettings` | List all configured strategy settings |
| `getStrategySetting` | Get details of a specific strategy setting |
| `createStrategySettings` | Create one or more strategy settings from valid templates |
| `executeStrategySettings` | Execute a strategy on a market + selection |
| `executeStrategySettingsOnSelections` | Execute a strategy on a market + multiple selections |
| `executeStrategySettingsWithParameters` | Execute a strategy with custom parameters (JSON) |

## 4. Results & History
| Tool | Purpose |
|------|---------|
| `getAllBetResults` | Retrieve all bet results from closed markets |

## 5. Application Control & Scripting
Tools to control Bfexplorer itself.

| Tool | Purpose |
|------|---------|
| `getApplicationCommands` | List available application commands/features |
| `executeApplicationCommand` | Execute an application command/feature |
| `executeConsoleScript` | Execute a console script file |

---

**Summary:** 7 market/discovery tools, 6 data-context tools, 8 strategy tools, 1 results tool, and 3 application-control tools = **25 tools total**.

Would you like me to demonstrate any of these, e.g., list currently monitored markets or available strategy templates?
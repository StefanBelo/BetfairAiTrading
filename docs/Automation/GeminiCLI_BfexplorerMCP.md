# Using Gemini CLI with Bfexplorer App MCP Server for Automated Betfair Trading

![Gemini CLI Prompt Setup](/docs/Automation/images/BfexplorerAndGeminiCLI.png)

## Overview

Gemini CLI enables AI-powered automation for Betfair trading by integrating with the Bfexplorer App MCP (Model Context Protocol) server. This setup allows users to execute advanced betting strategies using natural language prompts—no coding required. The AI agent processes market and context data as defined in the prompt, and, if conditions are met, places bets automatically.

## Key Benefits
- **No Coding Required:** Unlike Python FastAgent (see `src/AiAgentPython/README.md`), Gemini CLI only needs configuration—no scripts to write.
- **Prompt-Driven Automation:** Strategies are defined by prompts (e.g., `docs/Prompts/HorseRacingEVAnalysisMinimal.md`).
- **Direct MCP Integration:** Gemini CLI connects to Bfexplorer MCP server for real-time market data and strategy execution.

## How It Works
1. **Setup Gemini CLI** with MCP server configuration.
2. **Write or select a strategy prompt** (see `docs/Prompts/`).
3. **Run Gemini CLI**—the AI agent:
   - Retrieves market and context data via MCP tools.
   - Analyzes data as described in the prompt.
   - Executes betting strategies if criteria are met.
   - Reports only the final action (minimal output).

## Example: Horse Racing EV Analysis
Prompt: [`docs/Prompts/HorseRacingEVAnalysisMinimal.md`](/docs/Prompts/HorseRacingEVAnalysisMinimal.md)
- The agent silently collects market and horse data.
- Performs semantic and statistical analysis.
- Applies conservative criteria (EV, form, probability gap).
- Executes a bet or lay strategy on the market favorite.
- Outputs a single confirmation line (e.g., `STRATEGY EXECUTED: Bet 10 Euro on Thunder Bay (Market Favorite)`).

![Gemini CLI Prompt Result](/docs/Automation/images/BfexplorerAndGeminiCLIExecutedPrompt.png)

## Configuration Steps

### 1. Enable MCP Server in Bfexplorer App
- Ensure Bfexplorer App is running with MCP server enabled (default: `localhost:10043`).

### 2. Configure Gemini CLI for MCP
- Set the MCP server endpoint in Gemini CLI config (YAML or CLI flags):
  ```yaml
  mcp:
    servers:
      BfexplorerApp:
        transport: sse
        url: http://localhost:10043/sse
  ```
  

### 3. Select or Create a Prompt
- Use ready-made prompts from `docs/Prompts/` or write your own.
- Prompts define:
  - Data collection tools (e.g., `GetActiveMarket`, `GetDataContextForMarket`)
  - Analysis logic (EV, form, probability)
  - Execution criteria and actions

### 4. Run Gemini CLI
- Start Gemini CLI and provide the prompt.
- The agent will:
  - Connect to MCP server
  - Process all required data
  - Execute the strategy if conditions are met
  - Output the result

## Comparison: Gemini CLI vs Python FastAgent
| Feature                | Gemini CLI                | Python FastAgent           |
|-----------------------|---------------------------|----------------------------|
| Coding Required       | No                        | Yes (Python scripting)     |
| Setup                 | Simple config             | Python + config            |
| Prompt Flexibility    | High (natural language)   | High (script + prompt)     |
| MCP Integration       | Direct                    | Direct                     |
| Output                | Minimal (as defined)      | Customizable               |
| Use Case              | Non-coders, rapid setup   | Developers, custom logic   |

## Windows PowerShell Executor: Automate AI Strategies from Bfexplorer App

![Windows PowerShell Executor in Bfexplorer App](/docs/Automation/images/BfexplorerWindowsPowerShellExecutor.png)

The Windows PowerShell Executor strategy in Bfexplorer App allows you to execute any external command directly from the trading platform. This means you can trigger Gemini CLI or Python scripts to run AI strategy prompts automatically, integrating your AI agent workflows with Bfexplorer's strategy automation. With this approach, both non-coders (using Gemini CLI) and developers (using Python scripts) can automate complex trading strategies and prompt executions seamlessly from within Bfexplorer.

## Security & Best Practices
- Keep API keys and secrets secure.
- Validate MCP server connection before running strategies.
- Use conservative criteria in prompts to minimize risk.

## References
- [BfexplorerApp MCP Documentation](../../BFExplorer.md)
- [Python FastAgent Setup](../../src/AiAgentPython/README.md)
- [Strategy Prompts](../Prompts/)

---
This article describes how Gemini CLI can be used for automated Betfair trading with Bfexplorer App MCP server, enabling powerful AI-driven strategies with simple configuration and prompt design.

---


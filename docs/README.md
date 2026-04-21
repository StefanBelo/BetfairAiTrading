
# Betfair AI Trading Documentation

This documentation folder contains guides, research, automation instructions, strategy documentation, and AI prompts for Betfair AI trading using Bfexplorer.

## Main Sections

- **Automation/**: Guides and documentation for automating Betfair trading with AI agents and Bfexplorer.
- **Ideas/**: Research notes, integration ideas, and experiment summaries for new features and strategies.
- **Posts/**: Blog-style posts and community discussions.
- **Prompts/**: 100+ specialized AI prompts for market analysis, strategy development, and automation.
- **Research/**: In-depth research documents and technical analysis for Betfair trading and strategy development.
- **Strategies/**: Comprehensive documentation for all Bfexplorer strategies, organized by sport and type.
- **TestingStrategy.md**: Guide to testing strategies in the Bfexplorer environment.
- **NonDevelopers.md**: How non-developers can update code with AI assistance.

## Quick Links

- [Automation Guides](Automation/README.md)
- [Strategy Documentation](Strategies/README.md)
- [AI Prompts Index](Prompts/README.md)
- [Research Index](Research/README.md)

## Getting Started

See the README.md in each subfolder for more details on available files and usage instructions.
- [Horse Racing EV Analysis with Dutch Betting (OLBG Tips)](Prompts/HorseRacingEVAnalysisWithDutchBetting_OlbgTips.md) - Community tips integration
- [Horse Racing EV Analysis with Betting](Prompts/HorseRacingEVAnalysisWithBetting.md) - Single selection betting automation
- [Horse Racing EV Analysis with Trading Data](Prompts/HorseRacingEVAnalysisWithBetting_TradingData.md) - Enhanced with trading data
- [Horse Racing Numerical Data Analysis](Prompts/HorseRacingEVAnalysisNumericalData.md) - Quantitative metrics focus
- [Horse Racing EV Rankings Table Only](Prompts/HorseRacingEVRankingsTableOnly.md) - Quick rankings generation
- [Horse Racing EV Analysis Minimal](Prompts/HorseRacingEVAnalysisMinimal.md) - Silent analysis with minimal output
- [Horse Racing EV Analysis Minimal Execution](Prompts/HorseRacingEVAnalysisMinimalExecution.md) - Automated conservative betting with minimal reporting
- [Horse Racing Execution Only No Reports](Prompts/HorseRacingExecutionOnlyNoReports.md) - Silent execution strategy with no analysis reports
- [Horse Racing Silent EV Analysis with Automated Execution - Top 3 Favorites](Prompts/HorseRacingSilentEVAnalysisWithAutomatedExecutionTop3Favorites.md) - Completely silent comprehensive EV analysis with trading data, executing only on top 3 favorites
- [Horse Racing Silent OLBG EV Analysis with Automated Execution - Top 3 Favorites](Prompts/HorseRacingSilentOlbgEVAnalysisWithAutomatedExecutionTop3Favorites.md) - Silent comprehensive EV analysis using OLBG expert tips data, executing only on top 3 favorites

### Football Code Generation
- [Football Match F# Code Creation](Prompts/FootballMatchFSharpCodeCreation.md) - Generate F# code for football match analysis

### General Tools
- [Real-Time Betfair Market Analysis](Prompts/BetfairMarketAnalysisPrompt.md) - Comprehensive market analysis with professional trading patterns and recommendations
- [Trading Chart Creation](Prompts/TradingChartCreation.md) - Interactive financial chart creation from MCP time series data
- [BFExplorer MCP Integration System Prompt](Prompts/BfexplorerMCPIntegrationSystemPrompt.md) - System integration guidance
- [BFExplorer Strategy Expert Prompt](Prompts/BfexplorerStrategyExpertPrompt.md) - AI assistant for strategy selection and configuration

## Community Posts

**[📖 Complete Posts Index](Posts/README.md)**

### Real-World Experiences
- [AI Agent Horse Racing Analysis: The Power of Backtesting & Continuous Learning](Posts/Post_AIAgentBacktestingAndLearning.md) - Building adaptive AI agents that learn from results
- [How I Became a Betfair Strategy Expert in Minutes](Posts/Post_BfexplorerStrategyExpert.md) - Real success story using AI strategy assistance
- [Crowd Wisdom vs. Betting Value: Finding Edges in Efficient Markets](Posts/Post_CrowdWisdomVsBettingValue.md) - Deep analysis of market efficiency and systematic value identification
- [Reflections on LLMs for Strategy Development: A 2026 Perspective](Posts/LLM_Strategy_Reflections_2026.md) - Insights on the evolving role of LLMs in trading and coding
- [Betcode_LLM_Strategy_Reflections_2026.md](Posts/Betcode_LLM_Strategy_Reflections_2026.md) - Insights on the evolving role of LLMs in trading and coding

## Analysis Reports

**[📖 Complete Analysis Index](Analysis/README.md)**

### Performance Analysis
- [Horse Racing EV Analysis Results - June 22, 2025](Analysis/HorseRacingEVAnalysisResults_22June2025.md) - Comprehensive analysis of AI-driven Expected Value betting strategy performance across 12 races with strategic recommendations

## Strategies

**[📖 Complete Strategies Index](Strategies/README.md)**

### General
- [Execute on a Selection Strategy](Strategies/General/Execute-On-A-Selection.md) - Advanced conditional strategy execution using real-time market data

### Football
- [Football Betfair Trading Strategy: Over/Under Goals Market](Strategies/Football/TradeOverUnderGoals.md)
- [Football Score-Based Market Opener Guide](Strategies/Football/OpenMyMarketsByScore.md)

### Horse Racing
- [How to Use AI Agents for Smarter Horse Racing Betting: A Beginner's Guide](Strategies/HorseRacing/HowToUseAIAgentForHorseRacingBetting.md) - Complete beginner's guide to Expected Value and AI-powered betting
- [The Distance Advantage: A Specialized Betfair Horse Racing Strategy](Strategies/HorseRacing/RaceDistance.md)
- [Arbitrage in Motion: The Pre-Race Odds Comparison Strategy for Betfair Horse Racing](Strategies/HorseRacing/BookmakersOdds.md)
- [CloseByPositionDifferenceBotTrigger Strategy](Strategies/HorseRacing/CloseByPositionDifferenceBotTrigger_R1.md) - Automated trading strategy for horse racing markets based on position difference and odds.

### Tennis
- [Tennis Data to Spreadsheet](Strategies/Tennis/DataToSpreadsheet.md)
- [Tennis Score-Based Market Opener Guide](Strategies/Tennis/OpenMyMarketsByScore.md)

## AI Agent Implementations

Ready-to-use AI agents for automated trading:

### [Python AI Agent](../src/AiAgentPython/README.md)
- FastAgent framework with MCP integration
- Automated horse racing EV analysis with conservative betting
- Interactive mode for manual testing and exploration

### [C# AI Agent](../src/AiAgentCSharp/README.md)
- Multi-AI provider support (OpenAI GPT-4, DeepSeek)
- Model Context Protocol client integration
- Strategy automation capabilities

## F# Code Exploration Tools

- [Using FSI MCP Tools to Create Better F# Code for Non-Developers](Using_FSI_MCP_Tools_for_FSharp_Code.md)
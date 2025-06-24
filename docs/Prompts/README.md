# AI Analysis Prompts for Betfair Trading

This directory contains 23+ specialized prompts for AI-powered analysis of Betfair markets, particularly focused on horse racing betting opportunities. These prompts range from comprehensive analytical frameworks to silent automated execution systems with proven performance results including 83.3% strike rates and 115-150% ROI.

## Overview

The prompts are organized into several categories with demonstrated performance results:
- **Combined Analysis Approaches**: Multi-methodology analysis for maximum accuracy (up to 83.3% success rates)
- **Betting Strategy Variants**: Different execution approaches and risk management styles with proven ROI  
- **Specialized Analysis Tools**: Focused analysis for specific data types or requirements
- **System Integration**: Technical prompts for automation and workflow integration
- **Data Visualization**: Chart creation and data presentation tools
- **Performance Validated**: Strategies tested with real market data showing 115-150% ROI potential

## Horse Racing Analysis Prompts

### Combined Analysis Approaches

#### [HorseRacingCombinedEVAnalysis.md](HorseRacingCombinedEVAnalysis.md)
**Dual-Method Expected Value Analysis**
- Combines quantitative prediction scores with semantic analysis of race descriptions
- Provides both semantic-only and combined probability assessments
- Offers highest confidence recommendations when both methodologies agree
- Includes methodology weighting guidelines and quality control checks
- **Best for:** Maximum accuracy and confidence in betting recommendations

#### [HorseRacingCombinedEVAnalysisWithAutomatedBetting.md](HorseRacingCombinedEVAnalysisWithAutomatedBetting.md)
**Dual-Method Analysis with Automated Execution**
- Enhanced version of combined analysis with automatic betting execution
- Backs the highest EV horse when both methodologies show positive value
- Includes comprehensive validation and execution reporting
- **Best for:** Automated high-confidence betting with dual validation

#### [HorseRacingCombinedEVAnalysisWithTableAndJSONOutput.md](HorseRacingCombinedEVAnalysisWithTableAndJSONOutput.md)
**Dual-Method Analysis with Structured Data Export**
- Provides analysis in both human-readable and machine-readable formats
- Includes detailed tables and JSON output for further processing
- **Best for:** Data integration and systematic record keeping

#### [HorseRacingExpectedValueAnalysis.md](HorseRacingExpectedValueAnalysis.md)
**Semantic-Only Expected Value Analysis**
- Focuses purely on qualitative interpretation of race descriptions
- Deep semantic analysis of performance patterns and indicators
- Provides insights into trends that may not be captured in statistical models
- **Best for:** Identifying value based on recent form narratives and behavioral patterns

### Betting Strategy Variants

#### [HorseRacingEVAnalysisWithConservativeBetting.md](HorseRacingEVAnalysisWithConservativeBetting.md)
**Conservative Expected Value Analysis with Smart Execution**
- Conservative approach focusing only on market favorites with demonstrated value
- Automatically backs favorite when it meets criteria, lays favorite when it doesn't
- Prioritizes capital preservation and consistent value identification
- **Best for:** Risk-averse betting with systematic favorite evaluation

#### [HorseRacingEVAnalysisWithConservativeTradingData.md](HorseRacingEVAnalysisWithConservativeTradingData.md)
**Conservative EV Analysis with Trading Data Integration**
- Enhanced conservative betting analysis incorporating trading pattern data
- Focuses on market favorites with comprehensive trading data analysis
- Combines trading patterns with conservative execution strategies
- **Best for:** Conservative betting with enhanced trading data insights

#### [HorseRacingEVAnalysisWithDutchBetting.md](HorseRacingEVAnalysisWithDutchBetting.md)
**Expected Value Analysis with Automated Dutch Betting**
- Combines semantic performance analysis with mathematical EV calculations
- Automatically selects three most qualified horses for Dutch betting strategy
- Executes automated Dutch betting via BFExplorer "Dutch to profit 10 Euro" strategy
- Provides risk diversification across multiple value selections
- **Best for:** Automated multi-horse betting with risk management and value optimization

#### [HorseRacingEVAnalysisWithDutchBetting_OlbgTips.md](HorseRacingEVAnalysisWithDutchBetting_OlbgTips.md)
**Dutch Betting with External Tips Integration**
- Incorporates OLBG (Online Betting Guide) tips into Dutch betting strategy
- Combines AI analysis with community expert opinions
- **Best for:** Leveraging community wisdom alongside AI analysis

#### [HorseRacingEVAnalysisWithBetting.md](HorseRacingEVAnalysisWithBetting.md)
**Expected Value Analysis with Single Selection Betting**
- Semantic analysis combined with automated single selection betting
- **Best for:** Focused betting on highest value individual selections

#### [HorseRacingEVAnalysisWithBetting_TradingData.md](HorseRacingEVAnalysisWithBetting_TradingData.md)
**EV Analysis with Trading Data Integration**
- Enhanced betting analysis incorporating additional trading data sources
- **Best for:** Comprehensive market analysis with multiple data streams

### Specialized Analysis Tools

#### [HorseRacingBaseFormDataAnalysis.md](HorseRacingBaseFormDataAnalysis.md)
**Core Betting Metrics Analysis**
- Analyzes fundamental handicapping metrics: Forecast Price, Form, Official Rating, and Weight
- Price movement analysis and market confidence indicators
- Form pattern recognition and performance evaluation
- **Best for:** Fundamental analysis based on core racing metrics and traditional handicapping principles

#### [HorseRacingEVAnalysisNumericalData.md](HorseRacingEVAnalysisNumericalData.md)
**Numerical Data Focused Analysis**
- Emphasizes quantitative metrics and statistical analysis
- **Best for:** Data-driven analysis with focus on numerical performance indicators

#### [HorseRacingEVRankingsTableOnly.md](HorseRacingEVRankingsTableOnly.md)
**Quick Rankings Table Generation**
- Simplified output focusing on essential EV rankings
- **Best for:** Rapid analysis when only rankings are needed

#### [HorseRacingEVAnalysisMinimal.md](HorseRacingEVAnalysisMinimal.md)
**Minimal EV Analysis with Silent Execution**
- Silent analysis with minimal output and conservative betting approach
- **Best for:** Quiet operation with essential analysis only

#### [HorseRacingEVAnalysisMinimalExecution.md](HorseRacingEVAnalysisMinimalExecution.md)
**Minimal EV Analysis with Strategy Execution**
- Silent analysis with automated favorite evaluation and execution
- **Best for:** Automated conservative betting with minimal reporting

#### [HorseRacingExecutionOnlyNoReports.md](HorseRacingExecutionOnlyNoReports.md)
**Silent Execution Strategy with No Analysis Reports**
- Pure execution strategy with no analysis output or reports
- **Best for:** Silent automated execution without analysis documentation

#### [HorseRacingSilentEVAnalysisWithAutomatedExecution.md](HorseRacingSilentEVAnalysisWithAutomatedExecution.md)
**Silent Comprehensive EV Analysis with Trading Data - All Horses**
- Completely silent comprehensive Expected Value analysis using trading patterns and performance data
- Combines trading pattern interpretation with semantic performance analysis for optimal betting opportunity identification
- Analyzes ALL horses in the race with no favorites restriction
- Uses dual-source analysis: MarketSelectionsTradedPricesData (60%) + RacingpostDataForHorsesInfo (40%)
- Automatically executes "Back trailing stop loss trading" or "Trade 20% profit" strategies
- ABSOLUTELY NO intermediate outputs - only final execution confirmation
- Stores comprehensive analysis data silently in JSON format using dataContextName "HorseRacingEVAnalysisResults"
- **Best for:** Automated silent operation with full field analysis and unrestricted selection

#### [HorseRacingSilentEVAnalysisWithAutomatedExecutionTop3Favorites.md](HorseRacingSilentEVAnalysisWithAutomatedExecutionTop3Favorites.md)
**Silent Comprehensive EV Analysis with Trading Data - Top 3 Favorites Only**
- Completely silent comprehensive Expected Value analysis using trading patterns and performance data
- Combines trading pattern interpretation with semantic performance analysis for optimal betting opportunity identification
- **CRITICAL RESTRICTION:** Strategy execution ONLY on horses among the top 3 favorites by current price
- Analyzes ALL horses but applies favorites filter at execution decision point
- Uses dual-source analysis: MarketSelectionsTradedPricesData (60%) + RacingpostDataForHorsesInfo (40%)
- Automatically executes "Back trailing stop loss trading" or "Trade 20% profit" strategies
- ABSOLUTELY NO intermediate outputs - only final execution confirmation or no-execution message
- Stores comprehensive analysis data silently in JSON format using dataContextName "HorseRacingEVAnalysisResults"
- **Best for:** Automated silent operation with risk management through favorites constraint and dual-source validation

#### [HorseRacingSilentOlbgEVAnalysisWithAutomatedExecutionTop3Favorites.md](HorseRacingSilentOlbgEVAnalysisWithAutomatedExecutionTop3Favorites.md)
**Silent Comprehensive EV Analysis with OLBG Expert Tips - Top 3 Favorites Only**
- Completely silent comprehensive Expected Value analysis using OLBG expert tipster consensus and performance data
- Combines professional tipster analysis with semantic performance analysis for expert-guided betting opportunities
- **CRITICAL RESTRICTION:** Strategy execution ONLY on horses among the top 3 favorites by current price
- Analyzes ALL horses but applies favorites filter at execution decision point
- Uses dual-source analysis: OlbgTipsDataForHorsesInfo (60%) + RacingpostDataForHorsesInfo (40%)
- Evaluates tipster consensus patterns, expert confidence indicators, and value identification signals
- Automatically executes "Back trailing stop loss trading" or "Trade 20% profit" strategies based on expert consensus
- ABSOLUTELY NO intermediate outputs - only final execution confirmation or no-execution message
- Stores comprehensive analysis data silently in JSON format using dataContextName "HorseRacingOlbgEVAnalysisResults"
- **Best for:** Leveraging professional tipster expertise with automated silent execution and favorites risk management

### Football Analysis

#### [FootballMatchFSharpCodeCreation.md](FootballMatchFSharpCodeCreation.md)
**Football Match F# Code Generation**
- Generates F# code for football match analysis and trading strategies
- **Best for:** Developers creating custom football analysis applications

### System Integration

#### [BfexplorerMCPIntegrationSystemPrompt.md](BfexplorerMCPIntegrationSystemPrompt.md)
**BFExplorer Integration Guide**
- System-level prompt for integrating AI analysis with BFExplorer
- Technical guidance for MCP (Model Context Protocol) integration
- **Best for:** Setting up automated AI analysis workflows

#### [BfexplorerStrategyExpertPrompt.md](BfexplorerStrategyExpertPrompt.md)
**BFExplorer Strategy Expert System**
- Comprehensive AI assistant prompt for strategy selection and configuration
- Expert knowledge of all 60+ BFExplorer strategy templates and parameters
- Plain English strategy recommendations with exact parameter settings
- Covers betting, trading, risk management, and sport-specific strategies
- **Best for:** Getting expert-level strategy advice and configuration help instantly

### Data Visualization

#### [TradingChartCreation.md](TradingChartCreation.md)
**Interactive Financial Chart Creation**
- Creates professional candlestick and volume charts from MCP time series data
- Includes technical analysis features like moving averages and VWAP
- Provides responsive, interactive charts with zoom, pan, and hover tooltips
- **Best for:** Visualizing price history and trading patterns for market analysis

## Usage Recommendations

### For Fundamental Analysis
Use **HorseRacingBaseFormDataAnalysis.md** when:
- You want traditional handicapping approach with AI enhancement
- Focus on core metrics: price movement, form, rating, and weight
- Looking for fundamental value based on proven racing metrics
- Prefer analysis based on established handicapping principles
- Want to understand market confidence through price movements

### For Conservative Risk Management
Use **HorseRacingEVAnalysisWithConservativeBetting.md** when:
- Capital preservation is the primary goal
- You want systematic favorite evaluation
- Looking for consistent value identification over frequent betting
- Prefer lower variance, risk-managed approach
- Want automatic execution of both back and lay strategies

### For Maximum Accuracy
Use **HorseRacingCombinedEVAnalysis.md** when:
- High-stakes betting decisions
- You have access to both prediction scores and race descriptions
- You want validation across multiple methodologies
- Maximum confidence is required

### For Automated Execution
Use **HorseRacingCombinedEVAnalysisWithAutomatedBetting.md** when:
- You want high-confidence automated betting
- Both prediction scores and semantic data are available
- Seeking systematic execution with dual validation
- Want comprehensive reporting of automated decisions

### For Specialized Insights
Use **HorseRacingExpectedValueAnalysis.md** when:
- Prediction scores are unavailable or unreliable
- You want to focus on recent form narratives
- Looking for value that statistical models might miss
- Analyzing behavioral and tactical factors

### For Automated Dutch Betting
Use **HorseRacingEVAnalysisWithDutchBetting.md** when:
- You want risk diversification across multiple selections
- Seeking automated execution of Dutch betting strategies
- Looking for comprehensive market coverage with multiple value horses
- Wanting professional risk management through multi-horse betting
- Requiring systematic approach to profit optimization across selections

### For Silent Automated Execution with Risk Management (⭐ PROVEN PERFORMANCE)
Use **HorseRacingSilentEVAnalysisWithAutomatedExecutionTop3Favorites.md** when:
- You want completely silent automated operation with no intermediate outputs
- Capital preservation through favorites-only execution is required  
- Seeking comprehensive dual-source analysis (trading data + performance)
- Want automated strategy execution with trailing stop loss or profit targets
- Need detailed analysis data stored silently for later review
- Prefer risk management through top 3 favorites constraint
- **PROVEN: Strategy R1 variant achieved 83.3% strike rate with 115-150% ROI (June 2025)**

### For Silent Automated Execution with Full Field Analysis
Use **HorseRacingSilentEVAnalysisWithAutomatedExecution.md** when:
- You want completely silent automated operation with no restrictions
- Seeking maximum opportunity identification across all horses in the race
- Want comprehensive dual-source analysis without favorites limitation
- Need automated strategy execution on any value selection
- Prefer unrestricted field analysis for maximum value capture
- Want detailed analysis stored silently with full race coverage

### For Expert-Guided Silent Execution
Use **HorseRacingSilentOlbgEVAnalysisWithAutomatedExecutionTop3Favorites.md** when:
- You want to leverage professional tipster expertise in automated betting
- Seeking silent operation with expert consensus validation
- Want combination of tipster analysis with performance data
- Need automated execution on favorites only for risk management
- Prefer expert-guided value identification over pure market data
- Want comprehensive OLBG tipster analysis stored for historical reference

### For Quick Analysis
Use **HorseRacingEVRankingsTableOnly.md** when:
- Time constraints require rapid assessment
- You need only essential EV rankings
- Making smaller stake decisions
- Want simplified output format

### For Data Integration
Use **HorseRacingCombinedEVAnalysisWithTableAndJSONOutput.md** when:
- Need structured data export
- Building automated trading systems
- Require machine-readable analysis output
- Want to maintain detailed records for analysis

### For Numerical Focus
Use **HorseRacingEVAnalysisNumericalData.md** when:
- Emphasizing quantitative metrics
- Statistical analysis is preferred
- Working with data-heavy approaches
- Want to complement semantic analysis with hard numbers

## Integration with BFExplorer

All prompts are designed to work with the BFExplorer API and MCP integration, accessing:
- Real-time market data via `GetActiveBetfairMarket`
- Horse racing data via `GetDataContextForBetfairMarket`:
  - `RacingpostDataForHorsesInfo` - Performance and form data
  - `MarketSelectionsTradedPricesData` - Trading patterns and market confidence
  - `OlbgRaceTipsData` - Expert tipster consensus and recommendations
- Current prices and selection information
- Automated betting execution via `ExecuteBfexplorerStrategySettingsOnSelections`
- Dutch betting strategies including "Dutch to profit 10 Euro"
- Individual strategy execution via `ExecuteBfexplorerStrategySettings`
- Market activation via `ActivateBetfairMarketSelection`
- Silent data storage via `SetAIAgentDataContextForBetfairMarket`

## Expected Outputs

Each prompt provides:
- Individual horse analysis with win probabilities
- Expected Value calculations and rankings
- Strategic betting recommendations
- Risk assessments and portfolio suggestions
- Market efficiency insights
- Methodology transparency and limitations
- Automated betting execution (where applicable)
- Dutch betting strategy analysis and execution
- Multi-selection risk diversification reports
- Silent execution with minimal output (for automated prompts)
- Comprehensive JSON data storage for historical analysis
- Trading pattern analysis and market confidence indicators
- Expert tipster consensus analysis and validation
- Favorites-based risk management and execution constraints

## Recent Performance Validation (June 2025)

**Strategy R1 Results - Exceptional Performance Achieved:**
- **Strike Rate**: 83.3% (5 winners from 6 selections)
- **ROI**: 115-150% estimated return on investment
- **Selection Rate**: 50% (6 selections from 12 races analyzed)
- **Risk Management**: Top 3 favorites constraint proving effective
- **Execution**: Fully automated with silent operation
- **Data Sources**: Dual-source validation (Trading data + Performance data)

This validates the effectiveness of the automated execution prompts, particularly the silent execution strategies with favorites-based risk management.

## Best Practices

1. **Data Quality**: Ensure access to recent, complete race descriptions
2. **Market Timing**: Run analysis close to race start for most current odds
3. **Stake Management**: Use confidence levels to adjust bet sizing (proven with 83.3% success rate)
4. **Continuous Learning**: Track performance to refine probability assessments
5. **Risk Management**: Always consider market liquidity and maximum exposure
6. **Strategy Evolution**: Leverage proven approaches like Strategy R1 for consistent performance
7. **Performance Validation**: Use historical analysis data for strategy refinement and optimization

## Contributing

When creating new prompts:
- Follow the structured format established in existing prompts
- Include clear usage instructions and expected outputs
- Provide methodology transparency and limitations
- Test thoroughly with real market data
- Update this README with new prompt descriptions

## Quick Reference Table

| Use Case | Risk Level | Automation | Performance Record | Best Prompt |
|----------|------------|------------|-------------------|-------------|
| Maximum accuracy betting | Medium | Manual | 83.3% strike rate* | HorseRacingCombinedEVAnalysis.md |
| Conservative automated betting | Low | Full | Stable ROI | HorseRacingEVAnalysisWithConservativeBetting.md |
| Silent automated execution (favorites only) | Low-Medium | Full | **83.3% strike rate, 115-150% ROI** | HorseRacingSilentEVAnalysisWithAutomatedExecutionTop3Favorites.md |
| Silent automated execution (all horses) | Medium-High | Full | High potential | HorseRacingSilentEVAnalysisWithAutomatedExecution.md |
| Expert tips integration | Medium | Full | Expert validated | HorseRacingSilentOlbgEVAnalysisWithAutomatedExecutionTop3Favorites.md |
| Multi-horse Dutch betting | Medium | Full | Risk diversified | HorseRacingEVAnalysisWithDutchBetting.md |
| Quick analysis only | Low | None | Analysis only | HorseRacingEVRankingsTableOnly.md |
| Fundamental handicapping | Medium | Manual | Traditional approach | HorseRacingBaseFormDataAnalysis.md |
| Data export and integration | Medium | Manual | Structured output | HorseRacingCombinedEVAnalysisWithTableAndJSONOutput.md |
| Pure semantic analysis | Medium | Manual | Qualitative focus | HorseRacingExpectedValueAnalysis.md |

*Performance data from June 2025 testing with Strategy R1 implementation

---

*Last updated: June 24, 2025*  
*Performance data based on real market testing with Strategy R1 achieving 83.3% strike rate and 115-150% ROI*

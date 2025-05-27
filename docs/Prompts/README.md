# AI Analysis Prompts for Betfair Trading

This directory contains specialized prompts for AI-powered analysis of Betfair markets, particularly focused on horse racing betting opportunities.

## Horse Racing Analysis Prompts

### Combined Analysis Approaches

#### [HorseRacingCombinedEVAnalysis.md](HorseRacingCombinedEVAnalysis.md)
**Dual-Method Expected Value Analysis**
- Combines quantitative prediction scores with semantic analysis of race descriptions
- Provides both semantic-only and combined probability assessments
- Offers highest confidence recommendations when both methodologies agree
- Includes methodology weighting guidelines and quality control checks
- **Best for:** Maximum accuracy and confidence in betting recommendations

#### [HorseRacingExpectedValueAnalysis.md](HorseRacingExpectedValueAnalysis.md)
**Semantic-Only Expected Value Analysis**
- Focuses purely on qualitative interpretation of race descriptions
- Deep semantic analysis of performance patterns and indicators
- Provides insights into trends that may not be captured in statistical models
- **Best for:** Identifying value based on recent form narratives and behavioral patterns

### Specialized Analysis Prompts

#### [HorseRacingAnalyzingLastRaces.md](HorseRacingAnalyzingLastRaces.md)
**Detailed Form Analysis**
- Comprehensive analysis of recent racing performance
- Focus on form trends, consistency, and competitive ability
- **Best for:** In-depth understanding of horse form and potential

#### [HorseRacingAnalyzingLastRacesC.md](HorseRacingAnalyzingLastRacesC.md)
**Condensed Form Analysis**
- Streamlined version of form analysis for quick assessments
- **Best for:** Rapid form evaluation when time is limited

### System Integration

#### [BfexplorerMCPIntegrationSystemPrompt.md](BfexplorerMCPIntegrationSystemPrompt.md)
**BFExplorer Integration Guide**
- System-level prompt for integrating AI analysis with BFExplorer
- Technical guidance for MCP (Model Context Protocol) integration
- **Best for:** Setting up automated AI analysis workflows

### Data Visualization

#### [TradingChartCreation.md](TradingChartCreation.md)
**Interactive Financial Chart Creation**
- Creates professional candlestick and volume charts from MCP time series data
- Includes technical analysis features like moving averages and VWAP
- Provides responsive, interactive charts with zoom, pan, and hover tooltips
- **Best for:** Visualizing price history and trading patterns for market analysis

## Usage Recommendations

### For Maximum Accuracy
Use **HorseRacingCombinedEVAnalysis.md** when:
- High-stakes betting decisions
- You have access to both prediction scores and race descriptions
- You want validation across multiple methodologies
- Maximum confidence is required

### For Specialized Insights
Use **HorseRacingExpectedValueAnalysis.md** when:
- Prediction scores are unavailable or unreliable
- You want to focus on recent form narratives
- Looking for value that statistical models might miss
- Analyzing behavioral and tactical factors

### For Quick Analysis
Use the condensed prompts when:
- Time constraints require rapid assessment
- You need a quick form overview
- Making smaller stake decisions

## Integration with BFExplorer

All prompts are designed to work with the BFExplorer API and MCP integration, accessing:
- Real-time market data via `GetActiveBetfairMarket`
- Horse racing data via `GetDataContextForBetfairMarket`
- Current prices and selection information

## Expected Outputs

Each prompt provides:
- Individual horse analysis with win probabilities
- Expected Value calculations and rankings
- Strategic betting recommendations
- Risk assessments and portfolio suggestions
- Market efficiency insights
- Methodology transparency and limitations

## Best Practices

1. **Data Quality**: Ensure access to recent, complete race descriptions
2. **Market Timing**: Run analysis close to race start for most current odds
3. **Stake Management**: Use confidence levels to adjust bet sizing
4. **Continuous Learning**: Track performance to refine probability assessments
5. **Risk Management**: Always consider market liquidity and maximum exposure

## Contributing

When creating new prompts:
- Follow the structured format established in existing prompts
- Include clear usage instructions and expected outputs
- Provide methodology transparency and limitations
- Test thoroughly with real market data
- Update this README with new prompt descriptions

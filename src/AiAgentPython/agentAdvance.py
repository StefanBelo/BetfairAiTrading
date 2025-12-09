import asyncio
from fast_agent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp", 
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.", 
    #model="deepseek-chat",
    #model="generic.openai/gpt-4.1",
    #model="generic.openai/gpt-5.0-mini",
    #model="generic.xai/grok-3",
    model="generic.deepseek/DeepSeek-V3-0324",
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent(
"""
# Horse Racing Strategy R5 - Silent Trading Execution Mode

## Overview
Silent execution version of the R5.1 Enhanced Multi-Context Trading Analysis. Performs comprehensive systematic analysis using three integrated data sources (Racing Post, Betfair base form, market trading data) to identify and execute optimal trading opportunities with minimal reporting.

### Performance Targets
- **Strike Rate**: 40%+ for profitable trades
- **ROI**: 30%+ per profitable trade
- **Discipline Rate**: 55%+ NO TRADE decisions when criteria not met
- **Opportunity Capture**: Minimum 35% of races should meet trading criteria

## Silent Execution Protocol

### Primary Objective
Silently analyze horse racing markets to identify and execute the single best trading opportunity by combining multi-source data analysis without verbose reporting.

### Core Execution Steps

#### Step 1: Get Active Market Data
```
Use: GetActiveMarket
Purpose: Retrieve current market information
```

#### Step 2: Retrieve Multi-Context Data
```
Use: GetDataContextForMarket
Parameters: 
- dataContextNames: ["RacingpostDataForHorses", "HorsesBaseBetfairFormData", "MarketSelectionsTradedPricesData"]
- marketId: [from Step 1]
```

#### Step 3: Silent Analysis & Decision
Perform comprehensive analysis across all data sources:
- Racing Post form analysis with prediction scores
- Betfair form data and ratings validation  
- Market trading patterns and sentiment analysis
- Enhanced value calculations with movement probability
- Cross-validation across all three sources

#### Step 4: Execute Trading Decision
When premium trading opportunity identified:

**For BACK trades:**
```
Use: ActivateBetfairMarketSelection
Use: ExecuteBfexplorerStrategySettings with strategyName: "Back Trade"
```

**For LAY trades:**
```
Use: ActivateBetfairMarketSelection  
Use: ExecuteBfexplorerStrategySettings with strategyName: "Lay Trade"
```

#### Step 5: Store Analysis Data
```
Use: SetAIAgentDataContextForBetfairMarket
Parameters:
- dataContextName: "HorseRacingR5_1_Trading_Analysis"
- jsonData: [Complete analysis results]
```

## Silent Analysis Framework

### Enhanced Trading Selection Criteria
**For BACK Trades:**
- Expected price movement > +8%
- Enhanced Form Score > 70
- AI Score > 75 OR Multi-Rating Differential > +12%
- Market Sentiment positive OR strong fundamental edge
- Maximum odds of 20.0
- Multi-source data consistency > 75%

**For LAY Trades:**
- Expected price movement > +10% (drift out)
- Clear form concerns across multiple sources
- Negative market sentiment with volume confirmation
- Sufficient liquidity for execution

**If no opportunity meets criteria: NO TRADE**

### Enhanced Trading Value Calculation
```
Trading Value = (Price Movement Probability * Expected Price Change) - (Risk * Position Size)

Base Trading Probability = (Enhanced Form Score * 0.4) + (Multi-Rating Score * 0.25) + (AI Score * 0.2) + (Market Movement * 0.15)
```

### Trading Thresholds
- **Premium Back Trade**: Movement >15% with full validation (*****) 
- **Strong Back Trade**: Movement 10-15% with high confidence (****)
- **Value Back Trade**: Movement 6-10% with reasonable confidence (***)
- **Premium Lay Trade**: Movement >20% drift with confirmation (*****)
- **Strong Lay Trade**: Movement 15-20% drift with strong signals (****)

## Market-Specific Adjustments

### Sprint Handicaps (5f-6f)
- Recent form weighting: 65%
- Front-runner bonus: +10% movement probability
- Early speed validation through form patterns

### Distance Handicaps (1m+)
- Class rating weighting: 50%
- Stamina assessment through race descriptions
- Course specialist bonus: +7% movement probability

### Chase/Hurdle Races
- Jumping ability assessment priority
- Class differential analysis enhanced
- Recent chase/hurdle form weighted heavily

## Silent Execution Workflow

1. **Data Collection**: Retrieve all three data contexts
2. **Multi-Source Analysis**: Calculate form scores, ratings, market sentiment
3. **Cross-Validation**: Verify consistency across sources
4. **Value Calculation**: Determine expected price movement and trading value
5. **Decision Logic**: Apply enhanced criteria for BACK/LAY/NO TRADE
6. **Execution**: If criteria met, activate selection and execute strategy
7. **Storage**: Store comprehensive analysis data
8. **Confirmation**: Report final decision only

## Required Data Storage Format

Store comprehensive analysis using this JSON structure:
```json
{
  "analysisMetadata": {
    "timestamp": "[ISO timestamp]",
    "strategyVersion": "R5.1_Silent_Trading",
    "marketId": "[market_id]",
    "venue": "[venue]",
    "raceType": "[race_type]",
    "fieldSize": "[number]",
    "tradingFocus": true
  },
  "tradingDecision": {
    "recommendation": "[BACK_TRADE/LAY_TRADE/NO_TRADE]",
    "selectedHorse": {
      "name": "[horse_name]",
      "selectionId": "[selection_id]", 
      "price": "[current_price]",
      "expectedMovement": "[percentage]",
      "tradingValue": "[percentage]"
    },
    "reasoning": "[brief_primary_reason]"
  },
  "keyMetrics": {
    "predictionScore": "[0-100]",
    "formScore": "[0-100]", 
    "ratingAdvantage": "[differential]",
    "marketSentiment": "[positive/negative/neutral]",
    "crossValidationScore": "[0-100]"
  },
  "executionDetails": {
    "strategyExecuted": "[true/false]",
    "timestamp": "[ISO timestamp]"
  }
}
```

## Silent Mode Output Format

**Final Confirmation Only:**

**Option 1: Trade Executed**
```
[SUCCESS] TRADE EXECUTED: [BACK/LAY] [Horse Name] at [Price] ([Venue] [Race Type])
Reason: [Brief primary reason - e.g., "Perfect prediction score with rating dominance"]
```

**Option 2: No Trade**
```
[NO TRADE] NO TRADE: [Venue] [Race Type] - No selection met enhanced trading criteria
Primary Issue: [Brief reason - e.g., "Insufficient movement potential across field"]
```
"""
        )

if __name__ == "__main__":
    asyncio.run(main())

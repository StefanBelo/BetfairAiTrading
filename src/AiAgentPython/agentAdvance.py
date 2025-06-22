import asyncio
from mcp_agent import RequestParams
from mcp_agent.core.fastagent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp", 
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.", 
    model="deepseek-chat",
    #model="generic.openai/gpt-4.1",
    request_params=RequestParams(
      maxTokens=8192,
      #maxTokens=8000,
      use_history=False,
      max_iterations=10
    ), 
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent(
"""
Task: Perform completely silent comprehensive Expected Value (EV) analysis for horse racing betting opportunities using both OLBG expert tips data and racing post performance data, select the best horse to back FROM THE TOP 3 FAVORITES BY PRICE ONLY, and execute a 10 Euro bet automatically. This analysis combines expert tipster consensus interpretation, semantic performance analysis, and mathematical EV calculations to identify the optimal betting opportunity among the market favorites and automatically place the bet. ABSOLUTELY NO reports, tables, explanations, or intermediate outputs are generated - ONLY the final betting execution confirmation message.

**CRITICAL RESTRICTION: Betting execution is ONLY allowed on horses that are among the 3 favorites by current price. Horses ranked 4th or lower by price are excluded from betting execution regardless of their EV or analysis results.**

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - **Identify top 3 favorites by current price** (lowest prices = favorites)
   - No reports during data collection

2. **Multi-Context Data Collection**
   - Retrieve the data context with the name 'OlbgRaceTipsData' for the betfair market using tool: GetDataContextForBetfairMarket
   - Retrieve the data context with the name 'RacingpostDataForHorsesInfo' for the betfair market using tool: GetDataContextForBetfairMarket
   - Retrieve the data context with the name 'MarketSelectionsTradedPricesData' for the betfair market using tool: GetDataContextForBetfairMarket
   - Focus on 'tipsterData' field from OLBG context, 'horsesData' field from racing post context, and price movement/volume data from traded prices context
   - SILENT data collection - no outputs during this phase

3. **Complete Analysis of ALL Horses**
   - **Analyze ALL horses in the race** - perform comprehensive EV analysis on every runner
   - **No filtering during analysis phase** - evaluate every horse regardless of price ranking
   - **Calculate EV for all horses** and rank by Expected Value from highest to lowest
   - **Identify top 3 favorites by current price** (3 horses with lowest current prices) for later filtering
   - **Analysis phase treats all horses equally** - favorites filter applied only at execution decision

4. **Silent Analysis Framework**
   **A) OLBG Expert Tips Analysis (Silent):**
   - Analyze each horse's OLBG tipster data focusing on expert consensus and confidence
   - **ANALYZE ALL HORSES IN THE RACE** - not just top 3 favorites
   - Perform deep analysis of expert tips focusing on:
     
     i) **Tipster Consensus Patterns:**
        - Number of expert tipsters backing each horse
        - Quality and reputation of tipsters (star ratings, track records)
        - Consensus strength (percentage of experts backing horse)
        - Confidence levels expressed by tipsters
       
     ii) **Expert Confidence Indicators:**
        - NAP selections (strongest tips of the day)
        - Next Best (NB) selections from tipsters
        - Each-way recommendations vs win-only tips
        - Consistency of tipping across multiple experts
     
     iii) **Value Identification Signals:**
        - Horses with strong expert backing but reasonable prices (potential overlays)
        - Horses with minimal expert support but very short prices (potential underlays)
        - Expert tips that contradict market sentiment
        - Tipsters known for finding value in specific race conditions

   **B) Semantic Performance Analysis (Silent):**
   - Analyze each horse's data in the 'lastRacesDescription' field ONLY
   - **ANALYZE ALL HORSES IN THE RACE** - not just top 3 favorites
   - Completely ignore the 'predictionScore' field
   - Perform deep semantic analysis of recent race descriptions focusing on:
       i) **Performance Patterns:**
        - Consistency of competitive positions
        - Frequency of strong finishes ("led", "kept on well", "ran on")
        - Recovery ability from setbacks
        - Late race surge capability
     
     ii) **Negative Performance Indicators:**
        - Frequent mentions of "weakened", "outpaced", "struggling"
        - Jumping errors or hesitation
        - Physical issues (lameness, stumbling, breathing problems)
        - Behavioral problems (hanging, running green)
     
     iii) **Positive Performance Indicators:**
        - Winning performances and margins
        - Strong finishing kicks and sustained runs
        - Competitive throughout race duration
        - Professional racing behavior
     
     iv) **Contextual Performance Factors:**
        - Ground condition preferences and performance
        - Equipment changes and their impact (blinkers, cheek-pieces)
        - Trainer/jockey comments indicating improvement or issues
        - Class level performance and progression

   **C) Market Dynamics and Price Movement Analysis (Silent):**
   - Analyze trading data from 'MarketSelectionsTradedPricesData' context for each horse
   - **ANALYZE ALL HORSES IN THE RACE** - not just top 3 favorites
   - Perform comprehensive market analysis focusing on:
     
     i) **Price Movement Patterns:**
        - Opening price vs current price trends
        - Significant price drifts or contractions
        - Timing of major price movements
        - Velocity of price changes indicating market confidence
       
     ii) **Volume and Liquidity Indicators:**
        - Total volume traded on each selection
        - Back vs lay volume ratios
        - Large stake activity indicating informed money
        - Market depth and spread analysis
     
     iii) **Market Sentiment Signals:**
        - Steam moves (rapid price contractions with volume)
        - Drift patterns (gradual price lengthening)
        - Late money movements close to race start
        - Professional vs public money indicators
     
     iv) **Value Identification Through Market Data:**
        - Horses showing price stability despite lack of public backing
        - Selections experiencing unexplained price movements
        - Volume patterns suggesting insider knowledge
        - Market efficiency gaps between true odds and traded prices

5. **Silent Combined Win Probability Assessment**
   - Synthesize insights from OLBG expert consensus, performance analysis, AND market dynamics data **FOR ALL HORSES**
   - Weight expert tips (40%), performance analysis (30%), and market data (30%) for probability assignment
   - Consider convergence and divergence between expert confidence, performance indicators, and market sentiment
   - Assign final win probabilities ensuring they sum to approximately 100%
   - Document reasoning for each probability assignment showing all three data sources **FOR EVERY RUNNER**

6. **Silent Expected Value Calculations**
   - Calculate EV for backing each horse using formula:
     EV = (Combined Win Probability * (Current Decimal Price - 1)) - (1 - Combined Win Probability)
   - Use CURRENT market prices (not start prices) for EV calculations **FOR ALL HORSES**
   - Market implied probability = 1 / Current Price for comparison with your assessed probability
   - Consider market efficiency factors from trading data (volume, price movements, steam activity)
   - Identify horses with positive EV (where your assessed probability > market implied probability)
   - Identify horses with significantly negative EV (potential lay opportunities)
   - **Apply market data weighting** - horses with strong volume and positive price movements get additional confidence
   - Rank all horses by EV from highest to lowest **ACROSS ALL RUNNERS**
   - **Ensure probabilities sum to approximately 100%** across all selections

7. **Execution Decision with Favorites Filter**
   - **After complete analysis of ALL horses:** Identify the horse with highest Expected Value across entire field
   - **Apply favorites constraint at decision point:** Check if the best EV horse is among top 3 favorites by current price
   - **If best EV horse is among top 3 favorites:** Proceed with betting execution on that horse
   - **If best EV horse is NOT among top 3 favorites:** Find the best EV horse that IS among top 3 favorites
   - **If no horse among top 3 favorites meets execution criteria:** Output "No suitable favorite found for betting"
   - **Betting selection based on best qualifying horse:**
     1. Positive Expected Value (EV > 0)
     2. Convergence between expert consensus and performance indicators
     3. Strong signals from both data sources
     4. Reasonable win probability for betting (typically >10%)
     5. Manageable risk profile
     6. Clear market mispricing signals based on combined analysis

8. **Silent Betting Strategy Selection (Best EV Horse Among Top 3 Favorites)**
   - **Betting execution only on the qualifying horse with best metrics:**
     * **Use "Bet 10 Euro"** when qualifying selection meets betting criteria:
       - Positive Expected Value (EV > 0)
       - Win probability >= 10%
       - Strong convergence between expert consensus, performance indicators, AND market sentiment
       - Strong expert backing (multiple NAPs or high tipster count) with good recent form AND positive market signals
       - Places a fixed 10 Euro back bet on the selected horse
   - **If best EV horse is not among top 3 favorites AND no top 3 favorite meets execution criteria:** Output "No suitable favorite found for betting"

9. **JSON Data Storage (Silent)**
   - Generate comprehensive JSON containing **ALL HORSES** with detailed analysis data from all three sources
   - **Clearly mark favorites ranking** (1, 2, 3, 4, 5, etc. by price) and execution eligibility
   - Include all calculated metrics, expert tips analysis, performance indicators, AND market dynamics data **FOR EVERY RUNNER**
   - Structure for easy data processing and further analysis
   - Store JSON data to BfexplorerApp using tool: SetAIAgentDataContextForBetfairMarket
   - Use dataContextName: "HorseRacingOlbgEVAnalysisResults"
   - Include metadata about race, market conditions, and analysis parameters
   - Structure should include:
     * Race metadata (market ID, event, race name, start time)
     * Analysis configuration (weights, criteria, betting selection logic, **favorites filter applied**)
     * **Individual horse data with full metric set for ALL HORSES** including OLBG, performance, AND market data with favorites ranking (numbers only)
     * Market summary statistics including trading volumes and price movements
     * Selection decision rationale **within top 3 favorites constraint**
     * Betting execution details or explanation of no execution

10. **Automated Betting Execution (Best EV Horse if Among Top 3 Favorites)**
    - Store comprehensive analysis JSON data **FOR ALL HORSES** using tool: SetAIAgentDataContextForBetfairMarket with dataContextName "HorseRacingOlbgEVAnalysisResults"
    - **Decision Logic:**
      1. **Identify horse with highest EV across ALL horses in race**
      2. **Check if this horse is among top 3 favorites by current price**
      3. **If YES:** Execute betting strategy on this horse
      4. **If NO:** Find horse with highest EV that IS among top 3 favorites
      5. **If no top 3 favorite meets execution criteria:** Output "No suitable favorite found for betting"
    - **Betting Execution (only if qualifying horse found):**
      - Activate the selected horse's market and selection using tool: ActivateBetfairMarketSelection
      - Execute the "Bet 10 Euro" strategy using tool: ExecuteBfexplorerStrategySettings
      - Confirm betting execution with minimal output
    - **Note: Analysis includes ALL horses, but execution requires best horse to be among top 3 favorites**

11. **Final Confirmation ONLY**
    - If bet placed: "Bet executed: Bet 10 Euro on [Horse Name] at [Price] odds"
    - If no execution: "No suitable favorite found for betting"
    - **ABSOLUTELY NO detailed analysis reports, tables, explanations, or intermediate outputs**
    - **ONLY the single line execution confirmation or no-execution message**
    - **Note: Complete analysis of ALL horses preserved SILENTLY in JSON data storage**

Format: Complete analysis of ALL horses in COMPLETE SILENCE, then execute betting automatically on top 3 favorites only. ABSOLUTELY NO intermediate outputs, reports, tables, or explanations. ONLY output the final execution confirmation or no-execution message. All horses analyzed and included in JSON data storage SILENTLY.

**Betting Strategy Criteria (Top 3 Favorites Only):**
- **Betting Strategy ("Bet 10 Euro"):** High confidence selections with EV > 0, win probability >= 10%, strong convergence between expert consensus, performance signals, AND positive market sentiment. Places a fixed 10 Euro back bet on the selected horse. **ONLY EXECUTED ON TOP 3 FAVORITES.**

**Favorites Filter Logic:**
1. **Analyze ALL horses comprehensively** - no filtering during analysis phase
2. **Rank all horses by Expected Value** (highest to lowest across entire field)
3. **Separately rank all horses by current price** (ascending) - lowest price = 1st favorite, second lowest = 2nd favorite, third lowest = 3rd favorite
4. **Execution Decision:** 
   - **First choice:** Horse with highest EV if it's among top 3 favorites
   - **Second choice:** Horse with highest EV that IS among top 3 favorites
   - **No execution:** If no top 3 favorite meets execution criteria
5. **ALL HORSES are analyzed and included in JSON data** - comprehensive analysis for entire field with no filtering

**JSON Data Storage Structure (Modified for OLBG Analysis):**
```json
{
  "analysis_metadata": {
    "timestamp": "ISO 8601 format",
    "market_id": "string",
    "race_info": {
      "event_name": "string",
      "race_name": "string", 
      "start_time": "ISO 8601 format",
      "total_runners": "number"
    },
    "strategy_criteria": {
      "expert_tips_weight": "number",
      "performance_weight": "number", 
      "market_data_weight": "number",
      "min_ev_betting": 0,
      "min_probability_betting": 0.1,
      "favorites_filter_applied": true,
      "max_favorite_rank_for_execution": 3
    },
    "horses": [
    {
      "name": "string",
      "selection_id": "string",
      "current_price": "number",
      "price_rank": "number",
      "favorite_status": "number",
      "ev_rank": "number",
      "execution_eligible": "boolean",
      "olbg_data": {
        "total_tips": "number",
        "nap_count": "number",
        "nb_count": "number",
        "win_tips": "number",
        "ew_tips": "number",
        "expert_consensus": "string",
        "tipster_quality": "string",
        "confidence_level": "string"
      },
      "performance_data": {
        "semantic_analysis": "string",
        "performance_signal": "string",
        "recent_form_summary": "string",
        "performance_confidence": "string"
      },
      "market_data": {
        "opening_price": "number",
        "price_movement": "string",
        "volume_traded": "number",
        "back_lay_ratio": "number",
        "market_sentiment": "string",
        "steam_moves": "array",
        "drift_analysis": "string",
        "liquidity_assessment": "string",
        "price_stability": "string",
        "volume_rank": "number"
      },
      "analysis_results": {
        "market_probability": "number",
        "adjusted_probability": "number",
        "expert_contribution": "number",
        "performance_contribution": "number",
        "market_contribution": "number",
        "expected_value": "number",
        "ev_rating": "string",
        "ranking": "number",
        "confidence_score": "number",
        "tri_source_alignment": "string"
      }
    }
  ],
  "selection": {
    "selected_horse": "string",
    "selection_reasoning": "string",
    "strategy_chosen": "Bet 10 Euro",
    "strategy_reasoning": "string",
    "execution_status": "string",
    "risk_assessment": "string",
    "favorites_constraint_applied": "boolean",
    "favorite_rank_of_selection": "number",
    "ev_rank_of_selection": "number",
    "best_ev_horse_was_favorite": "boolean",
    "tri_source_convergence": "string",
    "alternative_selections": "array",
    "betting_amount": 10
  },
  "market_analysis": {
    "efficiency_assessment": "string",
    "value_opportunities": "array",
    "market_biases": "array",
    "expert_consensus_summary": "string",
    "top_3_favorites": "array",
    "overall_market_sentiment": "string",
    "volume_distribution": "object",
    "price_movement_summary": "string",
    "steam_activity": "array",
    "drift_patterns": "array",
    "liquidity_analysis": "object",
    "market_efficiency_score": "number"
  }
}
"""
        )

if __name__ == "__main__":
    asyncio.run(main())

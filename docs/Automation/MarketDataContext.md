# Market Data Context Documentation

This document provides comprehensive documentation for the various data contexts available for Betfair markets in Bfexplorer. Each data context provides specific information that can be used for analysis, decision-making, and strategy development. These data contexts can be leveraged by AI agents for rapid strategy development, as described in our Reddit post about transitioning from traditional ML to agentic strategy development.

**Active Market:** Salisbury 6f Hcap (Market ID: 1.245954717)  
**Event Date:** July 26, 2025 at 20:00 CET  
**Market Type:** Horse Racing  

---

## 1. OlbgRaceTipsData

**Purpose:** Provides professional racing tips and analysis from OLBG (OnLine Betting Guide) tipsters.

**Data Structure:**
- `comments`: Detailed tipster analysis and reasoning
- `confidence`: Numerical confidence rating (0-100) indicating tipster's confidence level
- `preraceComment`: Pre-race analysis and expectations

**Sample Data Example (Em Four):**
```json
{
  "comments": "",
  "confidence": 57,
  "preraceComment": "Has done well since being gelded and was nicely on top at Chepstow last week (6f, good); handicapper didn't hold back with an 8lb rise but remains lightly raced."
}
```

**Key Insights from Current Market:**
- Em Four: Highest confidence (57%) - Recent winner with handicapper concerns
- Thunderous Love: Moderate confidence (29%) - Hat-trick attempt with 2lb rise
- The Bitter Moose: Low confidence (14%) - Detailed stall position analysis
- Silent Flame: No confidence rating - Last summer form with blinkers
- Delagate This Lord: No confidence rating - Bath specialist making debut

---

## 2. TestRaceData

**Purpose:** Contains custom test data and analytics specific to horse performance metrics.

**Data Structure:**
- `beatenDistanceTrend`: Trend analysis of how far beaten in recent races
- `horseInPlayRating`: Current in-play performance rating (0-1 scale)
- `horseInPlayRatingTrend`: Trend direction of in-play rating
- `isBeatenFavourite`: Boolean indicating if horse was a beaten favourite recently
- `isPreviousWinner`: Boolean indicating if horse won last time out
- `jockeyInPlayRating`: Jockey's current in-play performance rating
- `jockeyInPlayRatingTrend`: Trend direction of jockey's rating
- `jockeyWinnerPercentageInLastX`: Recent win percentage for jockey

**Key Performance Metrics:**
- **Em Four**: Rating 0.21, declining trend (-0.09), recent winner
- **Thunderous Love**: Highest rating 0.34, strong upward trend (+0.41), previous winner
- **The Bitter Moose**: Rating 0.24, declining trend (-0.17)
- **Silent Flame**: Rating 0.35, stable trend (+0.01), previous winner
- **Delagate This Lord**: Rating 0.31, positive trend (+0.04)

---

## 3. MarketSelectionsPriceHistoryData

**Purpose:** Comprehensive historical price movements and trading volume data for all selections.

**Data Structure:**
- `time`: Timestamp of price movement
- `price`: The price at that time
- `volume`: Amount of money traded at that price

**Key Trading Patterns:**
- **Em Four**: Significant price drift from 3.7 to 3.05 over 24 hours, high volume (1000+ trades)
- **Thunderous Love**: Volatile trading from 5.9 down to 4.6, strong volume in evening
- **The Bitter Moose**: Steady strengthening from 7.0 to 4.8, consistent interest
- **Silent Flame**: Weakening from 4.2 to 6.8, limited early support
- **Delagate This Lord**: Dramatic drift from 7.8 to 10.0, sporadic trading

**Trading Volume Analysis:**
Shows market confidence and liquidity for each selection, with Em Four showing strongest support.

---

## 4. RacingpostDataForHorses

**Purpose:** Detailed form analysis and historical race descriptions from Racing Post.

**Data Structure:**
- `lastRacesDescriptions`: Array of recent race performance descriptions
- `officialRating`: Current official handicap rating
- `rpRating`: Racing Post's own rating system

**Form Analysis Summary:**
- **Em Four**: OR 75, RP 80 - Recent winner, consistent performer
- **Thunderous Love**: OR 70, RP 76 - Multiple recent wins, strong current form
- **The Bitter Moose**: OR 63, RP 80 - Inconsistent, running issues noted
- **Silent Flame**: OR 63, RP 81 - Recent winner, benefits from equipment changes
- **Delagate This Lord**: OR 67, RP 80 - Course specialist, mixed recent form

**Key Form Notes:**
Each horse has detailed race descriptions showing running style, problems encountered, and performance patterns essential for race analysis.

---

## 5. WeightOfMoneyData

**Purpose:** Real-time betting market analysis showing money flow and trading patterns.

**Data Structure:**
- `averageBackTraded`: Average price and volume for back bets
- `averageLayTraded`: Average price and volume for lay bets
- `offeredPrices`: Current available prices for both back and lay
- `tradedPrices`: Historical prices and volumes traded

**Market Dynamics:**
- **Em Four**: Heavy backing with 2,736 units traded, strong back support
- **Thunderous Love**: Limited recent backing (17.5 units), mainly lay interest
- **The Bitter Moose**: Balanced activity (46.5 units traded)
- **Silent Flame**: Light trading (27.6 units), mainly lay activity
- **Delagate This Lord**: Moderate backing (25.2 units), price drifting

**Money Flow Insights:**
Shows which horses are attracting smart money versus public sentiment.

---

## 6. TimeformDataForHorses

**Purpose:** Timeform's professional ratings and suitability analysis for current race conditions.

**Data Structure:**
- `ratingStars`: Star rating system (1-5 stars)
- `horseWinnerLastTimeOut`: Boolean for last race win
- `horseInForm`: Current form assessment
- `suitedByGoing`: Ground condition suitability
- `suitedByCourse`: Course suitability
- `suitedByDistance`: Distance suitability
- `trainerInForm`: Trainer's current form
- `jockeyInForm`: Jockey's current form

**Timeform Assessment:**
- **Silent Flame**: 5 stars - Top rated, suits all conditions, recent winner
- **Thunderous Love**: 4 stars - Strong form, suits conditions, trainer in form
- **Delagate This Lord**: 4 stars - Good form, limited course suitability
- **Em Four**: 3 stars - Recent winner, some condition concerns
- **The Bitter Moose**: 3 stars - Limited suitability factors

---

## 7. MarketSelectionsTradedPricesData

**Purpose:** Simplified trading data focusing on key price metrics and volume.

**Data Structure:**
- `endPrice`: Current price
- `maxPrice`: Highest price reached
- `minPrice`: Lowest price reached
- `startPrice`: Opening price
- `tradedVolume`: Total volume traded

**Price Movement Summary:**
- **Em Four**: Stable at 3.05, highest volume (42.73)
- **Thunderous Love**: Stable at 4.6, moderate volume (17.55)
- **The Bitter Moose**: Slight strengthening 4.9→4.8, strong volume (46.55)
- **Silent Flame**: Stable at 6.8, moderate volume (27.58)
- **Delagate This Lord**: Strengthening 10.5→10.0, moderate volume (25.16)

---

## 8. HorsesBaseBetfairFormData

**Purpose:** Essential Betfair form data including official ratings, weights, and recent form figures.

**Data Structure:**
- `forecastPrice`: Betfair's forecast price
- `form`: Recent form figures (positions in last 6 races)
- `officialRating`: Official handicap rating
- `weight`: Assigned weight for this race

**Betfair Assessment:**
- **Em Four**: Forecast 3.25, form "604441", OR 75, weight 137lbs
- **Thunderous Love**: Forecast 4.0, form "173112", OR 70, weight 137lbs
- **The Bitter Moose**: Forecast 8.0, form "700524", OR 63, weight 130lbs
- **Silent Flame**: Forecast 4.0, form "100-701", weight 130lbs
- **Delagate This Lord**: Forecast 5.5, form "42301-4", weight 134lbs

---

## Data Context Usage Guidelines

### For AI Strategy Development:
1. **Combine Multiple Contexts**: Use 2-3 data contexts together for comprehensive analysis
2. **Weight Recent Data**: Prioritize recent form and trading patterns
3. **Consider Market Efficiency**: Heavy trading volumes often indicate informed money
4. **Factor Conditions**: Always consider course, distance, and going suitability

### For Trading Decisions:
1. **Monitor Price Movements**: Use price history to identify trends
2. **Analyze Volume**: High volume moves are more significant
3. **Compare Ratings**: Cross-reference professional ratings from different sources
4. **Track Money Flow**: Watch for unusual backing or laying patterns

### Data Refresh Frequency:
- **Price Data**: Updates every few seconds
- **Form Data**: Static until next race
- **Tips Data**: Daily updates
- **Money Flow**: Real-time updates

---

## Technical Notes

**Data Retrieval Workflow:**
1. First use `GetActiveBetfairMarket` to get the current active market data
2. Then retrieve specific data contexts using:
   - `GetDataContextForBetfairMarket` - for individual market-level data contexts
   - `GetDataContextForBetfairMarketSelection` - for individual selection-level data contexts
   - `GetAllDataContextForBetfairMarket` - to combine multiple data contexts for comprehensive analysis

**Data Formats:**
- Market ID format: "1.245954717"
- Selection ID format: "76590770_0.00"
- Times are in CET (Central European Time)
- Prices are in decimal format
- Volumes are in GBP units

Last Updated: July 26, 2025

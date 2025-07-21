# Betfair SP (Starting Price) Research - Horse Racing Trading

## What is Betfair SP?

Betfair's Starting Price (SP) is based on bets placed by both backers and layers in any market - there is no margin for profit built in, making it different from traditional bookmaker starting prices. The Betfair Starting Price will be determined by balancing bets from customers who want to back and lay, so because it's other customers placing the bets and because there is no margin for profit built in, you are far more likely to have better odds.

## What Traders Say About BSP

The trading community has mixed but generally cautious views about BSP:

### Efficiency and Reliability
According to trader assessments, BSP is highly efficient according to forum sentiment. However, many express concerns about its reliability for systematic strategies.

"BSP should just be a backup it is too unreliable. Also if you're using large stakes you can force the BSP the wrong way" - this reflects a common concern among professional traders.

### Market Impact
An SP bet of a few hundred pounds can influence the BSP of a smallish market quite significantly. Obviously the bigger the market the lower the impact ratio. This shows that traders can actually move the BSP, especially in smaller markets.

For larger markets (50k plus), the SPs are returned within the last traded spread over 98% of the time so they aren't being moved that much - but this varies significantly by market size and liquidity.

## BSP Trading Strategies

Based on the research, here are the main strategies traders use with BSP:

### 1. **BSP Backing/Laying Strategy**
- Traders place SP bets to get matched at the calculated starting price
- Used as a "set and forget" approach when unable to monitor markets closely
- Particularly useful for smaller markets where pre-race liquidity is limited

### 2. **Price Prediction Models**
Some traders use multiple linear regression models which incorporate:
- Maximum price placed before the off
- Minimum price placed before the off
- Maximum price placed in-play
- Minimum price placed in-play
- Amount traded before 11am GMT
- Amount traded before the off
- Amount traded in-play

These models attempt to predict BSP outcomes.

### 3. **Rank-Based Strategies**
Some traders focus on whether the BSP rank of all runners in a race varies much from the actual price rank just before a race commences - analyzing whether favoritism order changes between pre-race and BSP.

### 4. **Historical Form Analysis**
Traders analyze previous SP performance as a factor. For example, last start winners that were expected to run very well (less than $3 SP) have different strike rates compared to those with higher previous SPs.

## Key Considerations for BSP Trading

### Advantages:
- No margin built in (unlike bookmaker SP)
- Good for illiquid markets
- Useful for automated strategies
- Can capture value when market moves favorably

### Disadvantages:
- Can be manipulated by large stakes
- Less predictable than live trading
- Limited control over execution price
- Efficiency makes finding edge difficult

## Professional Opinion

Most experienced traders view BSP as a tool rather than a primary strategy. "BSP should just be a backup it is too unreliable" summarizes the general sentiment - it's useful for specific situations but shouldn't be relied upon as a main profit driver.

The consensus appears to be that while BSP can be part of a broader trading approach, successful horse racing traders typically prefer more active pre-race and in-running strategies where they have greater control over their entry and exit points.

## Key Takeaways

1. BSP is highly efficient but can be unreliable for systematic strategies
2. Large stakes can influence BSP, especially in smaller markets
3. Most professional traders use BSP as a backup rather than primary strategy
4. Various mathematical models exist to predict BSP outcomes
5. Active trading strategies generally preferred over pure BSP approaches
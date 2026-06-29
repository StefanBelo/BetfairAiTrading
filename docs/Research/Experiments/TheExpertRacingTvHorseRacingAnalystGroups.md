---
title: "Horse Racing Analysis Framework - Complete Prompt"
aliases: ["Horse Racing Analysis Framework - Complete Prompt"]
type: prompt
tags: [ev-analysis, horse-racing, prompt]
mcp_tools: [GetActiveMarket, GetAllDataContextForMarket]
---

# Horse Racing Analysis Framework - Complete Prompt

## **Objective**
Conduct comprehensive horse racing analysis using systematic data retrieval, multi-dimensional clustering, and predictive modeling to identify value betting opportunities and calculate win probabilities.

---

## **Step 1: Data Retrieval**

### **1.1 Get Active Market Data**
```
Execute: GetActiveMarket
Purpose: Retrieve current market information
Extract:
- marketId (for subsequent API calls)
- Market metadata (race name, distance, class, surface)
- All selections with selectionId, name, and current price/odds
- Total matched amount and market liquidity
- Race start time and status
```

### **1.2 Get Comprehensive Racing Data**
```
Execute: GetAllDataContextForMarket
Parameters: dataContextNames: ["RacingTvDataForHorses"]
Purpose: Obtain detailed horse racing intelligence
Extract:
- Timeform ratings and star ratings
- Trainer statistics and stable form
- Jockey strike rates and form
- Breeding information and yearling prices
- Historical performance data
- Pace/style indicators
- Recent workout information
```

---

## **Step 2: Multi-Dimensional Horse Clustering**

### **2.1 Performance Tier Classification**
```
Tier 1 (Elite): Timeform Rating 75+ AND Star Rating 4-5
Tier 2 (Competitive): Timeform Rating 70-74 AND Star Rating 3-4
Tier 3 (Developing): Timeform Rating 65-69 AND Star Rating 2-3
Tier 4 (Outsiders): Timeform Rating <65 OR Star Rating 1-2
```

### **2.2 Stable Form Clusters**
```
Proven Stable (80-100): Recent wins with similar horse types
Developing Stable (50-79): Mixed recent form, some success
Struggling Stable (0-49): Poor recent record with horse type
```

### **2.3 Breeding Quality Index**
```
Premium (80-100): High yearling price + proven family lines
Mid-Tier (50-79): Moderate investment + decent connections
Value (0-49): Lower investment + limited proven success
```

### **2.4 Pace Style Grouping**
```
Front-Runners: Early speed, likely to lead
Stalkers: Tactical speed, sit behind pace
Closers: Late runners, need strong pace
```

---

## **Step 3: Win Probability Calculation Model**

### **3.1 Base Probability Calculation**
```
Market Implied Probability = 1 / Decimal Odds
```

### **3.2 Composite Scoring Algorithm**
```
Component Weights:
- Timeform Rating: 30% (Rating/100 * 0.3)
- Star Rating: 20% (Stars/5 * 0.2)
- Stable Form: 25% (Success Rate/100 * 0.25)
- Breeding Quality: 15% (Index Score/100 * 0.15)
- Jockey Form: 10% (Strike Rate/100 * 0.1)

Model Probability = Market Probability * (1 + Sum of Weighted Adjustments)
Model Odds = 1 / Model Probability
```

### **3.3 Value Rating System**
```
⭐⭐⭐ Strong Buy: Model odds significantly better than market (40%+ edge)
⭐⭐ Buy: Model odds moderately better than market (20-39% edge)
⭐ Consider: Model odds slightly better than market (10-19% edge)
- Avoid: Model odds worse than or equal to market odds
```

---

## **Step 4: Competition Impact Analysis**

### **4.1 Pace Pressure Assessment**
```
Analyze how different quality tiers affect race dynamics:
- Elite horses forcing others to expend early energy
- Pace scenarios based on front-runner concentration
- Late-runner advantage calculations in strong/weak pace
```

### **4.2 Class Differential Impact**
```
Calculate "extra effort required" when facing superior competition:
- Performance degradation factors for lower-tier horses
- Upset potential based on pace/trip scenarios
- Historical class-jumping success rates
```

---

## **Step 5: Output Format Requirements**

### **5.1 Market Overview Table**
```markdown
| Metric | Value |
|--------|-------|
| Race Details | [Distance, Class, Surface] |
| Field Size | [Number] runners |
| Total Pool | £[Amount] |
| Market Status | [Status] |
```

### **5.2 Comprehensive Analysis Table**
```markdown
| Horse | Market Odds | Implied % | Timeform | Stars | Breeding Score | Stable Form | Model % | Model Odds | Value |
|-------|------------|-----------|----------|-------|----------------|-------------|---------|------------|--------|
| [Name] | [Odds] | [%] | [Rating] | [Stars] | [Score] | [%] | [%] | [Odds] | [Rating] |
```

### **5.3 Key Findings Section**
```markdown
## Top Value Opportunities
1. **[Horse Name]** - [Rating]
   - Model vs Market odds comparison
   - Key supporting factors
   - Risk assessment

## Competition Analysis
- Elite Tier dynamics
- Pace scenario most likely
- Upset potential factors

## Betting Strategy
- Win bet recommendations
- Place coverage suggestions
- Horses/patterns to avoid
```

### **5.4 Model Confidence Metrics**
```markdown
- Data completeness score
- Historical model accuracy for similar races
- Key uncertainty factors
- Overall confidence level (%)
```

---

## **Step 6: Execution Checklist**

### **Before Analysis:**
- [ ] Confirm market is active and pre-race
- [ ] Verify all horses have complete data

### **During Analysis:**
- [ ] Cross-reference multiple data sources
- [ ] Validate calculations at each step

### **Final Review:**
- [ ] Ensure value ratings align with edge calculations
- [ ] Verify top selections have logical supporting factors
- [ ] Confirm betting strategy matches risk tolerance

---

## **Expected Deliverables**

1. **Complete market overview** with key race characteristics
2. **Detailed scoring table** showing all component calculations
3. **Ranked value opportunities** with clear reasoning
4. **Strategic betting recommendations** based on model findings
5. **Risk assessment** and confidence indicators

This framework ensures systematic, repeatable analysis that combines quantitative modeling with qualitative racing insights to identify genuine value betting opportunities.
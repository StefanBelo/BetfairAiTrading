# Football Betfair Trading Strategy: Over/Under Goals Market

This approach focuses on identifying value, managing risk, and capitalizing on price movements during matches.

To test this strategy on the Betfair Exchange using Bfexplorer app, copy the following URI **bfexplorer://testStrategy?fileName=Football - Trade Over Under.json** and open it in your web broswer.

![Bfexplorer running Trade Over/Under 2.5 Goals strategy!](/docs/Strategies/Football/images/Trade%20Over%20Under%20Goals.png "Bfexplorer running Trade Over/Under 2.5 Goals strategy")

## Core Strategy: Goal Expectancy Gap Trading

This strategy exploits the difference between market expectations and actual in-play dynamics by:

1. **Pre-match analysis**: Identifying matches with mispriced over/under markets
2. **Entry position**: Taking an initial position based on your analysis
3. **In-play trading**: Managing the position as the match unfolds
4. **Exit strategy**: Securing profits at predetermined points

### Step 1: Pre-Match Analysis

Look for matches where you believe the over/under goals line is mispriced by analyzing:

- Team form (recent scoring/conceding patterns)
- Head-to-head history (average goals in previous meetings)
- Key absences (injured attackers/defenders)
- Tactical approaches (attacking vs defensive setups)
- Motivation factors (teams needing wins vs satisfied with draws)
- Weather conditions (heavy rain typically reduces goals)

### Step 2: Entry Position

**Example Scenario**: You've identified a match where the 2.5 goals line is overpriced.

- Back the Under 2.5 goals at odds of 2.10
- Stake: 5% of your trading bank (adjust based on confidence)

### Step 3: In-Play Trading

This is where the strategy becomes dynamic:

#### Scenario A: 0-0 at Half-Time
- The Under 2.5 odds will have shortened to around 1.50-1.60
- Lay the Under 2.5 for 50-75% of your liability to lock in profit
- If you backed £100 at 2.10 (liability: £110), lay £60-90 at 1.55

#### Scenario B: Time-Based Strategy
- As the match progresses scoreless, lay portions of your back bet:
  - At 30 mins (0-0): Lay 30% of original stake
  - At 60 mins (0-0): Lay another 40%
  - At 75 mins (0-0): Lay remaining 30%

#### Scenario C: Goal-Based Strategy
- If a goal is scored early, don't panic
- For a match you expected to be low-scoring:
  - After one goal: Hold position until 60-65 minutes
  - If still 1-0/0-1, lay some liability as Under 2.5 odds will still be favorable

### Step 4: Managing Adverse Scenarios

**Quick Goals**:
- If a goal is scored in the first 15 minutes, the Under 2.5 odds will drift
- Consider increasing your position ("doubling down") if your pre-match analysis remains valid
- Example: Original £100 back at 2.10, add £50 at 2.60 after an early goal

**Score Equalizers**:
- Teams often become more defensive after equalizing
- If a match goes 1-0 then 1-1 before 70 minutes, consider backing Under 2.5 again

### Step 5: Practical Implementation

1. **Select Matches Carefully**:
   - Focus on less popular leagues where inefficiencies are more common
   - Avoid high-variance teams with inconsistent scoring patterns

2. **Use Price Alerts**:
   - Set predetermined price points for automated actions
   - For Under 2.5 trades, consider laying at odds of 1.50, 1.30, etc.

3. **Green Up Technique**:
   - When odds move significantly in your favor, use Betfair's "cash out" function or manually balance your position
   - Aim for partial lock-ins at different time intervals rather than one all-or-nothing approach

4. **Bankroll Management**:
   - Never risk more than 5% on a single trade
   - Track results to identify which scenarios yield highest ROI

This strategy works because the over/under markets tend to overreact to match circumstances, creating trading opportunities. By having a systematic approach to entering and exiting positions, you can capitalize on these inefficiencies while maintaining proper risk management.
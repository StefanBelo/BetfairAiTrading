# Strategy Report: Applying the VDW Elementary Mechanical Procedure to Betfair AI Trading

## Overview
The VDW (Van der Wheil) Elementary Mechanical Procedure is a structured approach to horse race selection, focusing on narrowing the field to the most likely winners using a combination of form, consistency, class, and market factors. The method is not a rigid system but a framework for identifying value and improving strike rates, often incorporating multiple betting (dutching) when appropriate.

## Key Steps of the VDW Procedure
1. **Race Selection**
   - Start with the most valuable race on the card, then the next most valuable, and so on across meetings.
2. **Field Rating**
   - Rate the entire field for ability (class, prize money, etc.).
3. **Shortlisting by Consistency and Market**
   - In non-handicaps: focus on the first 5 in the betting forecast.
   - In handicaps: focus on the first 6.
   - Select the most consistent horses among these (using recent form figures, e.g., sum of last 3 placings).
4. **Dual Rating Methods**
   - Apply at least two rating methods (e.g., class and consistency) to further narrow the field.
5. **Form Study**
   - Examine the form of shortlisted horses, considering class of previous races, course, going, pace, distances, and performance in the closing stages.
6. **Final Selection**
   - Combine all factors, subject to 'other considerations' (trainer form, market moves, etc.).

## Multiple Betting (Dutching)
- When two or three horses are clear contenders, consider backing all at stakes that guarantee a profit if any wins (dutching).
- Calculate stakes using odds to ensure a positive return, only if the overround is acceptable.

## Value Assessment
- Create your own odds line for the contenders and compare with market prices to identify value bets.
- Only bet when the available odds offer value over your calculated tissue.

## How to Use with Betfair AI-Driven App
### Data Requirements
- **Market Data**: Live Betfair odds, forecast prices, market volumes.
- **Form Data**: Recent placings, class, prize money, going, course, pace, etc.
- **Ratings**: Ability and consistency ratings, possibly from external sources or calculated in-app.

### Implementation Steps
1. **Automate Race Selection**
   - Use Betfair API to identify the most valuable races of the day.
2. **Field Analysis**
   - Pull all runners and their form data.
   - Calculate ability and consistency ratings for each.
3. **Shortlist Contenders**
   - Filter to top 5/6 in the market, then select the most consistent.
   - Apply dual rating methods.
4. **Form Analysis Module**
   - Integrate a form analysis engine to assess class, going, pace, etc.
5. **Dutching Calculator**
   - Implement a dutching tool to calculate optimal stakes for multiple selections.
6. **Value Engine**
   - Build a module to create a tissue (fair odds line) and compare with Betfair prices.
   - Highlight value opportunities.
7. **AI Decision Layer**
   - Use AI to weigh 'other considerations' (trainer, market moves, etc.) and finalize selections.
8. **Execution**
   - Place bets automatically or flag for user confirmation.

### Potential Enhancements
- Machine learning to refine rating methods and value assessment.
- Real-time market monitoring for late moves and liquidity.
- User-configurable parameters for aggressiveness, risk, and value thresholds.

## Conclusion
The VDW Elementary Mechanical Procedure provides a robust framework for systematic race analysis and selection. By automating its steps and integrating value and dutching logic, the Betfair AI-driven app can offer disciplined, data-driven betting strategies with improved strike rates and value focus.

---

**References:**
- [The UK Betting Forum: The Elementary Mechanical Procedure](https://www.theukbettingforum.co.uk/XenForo/threads/the-elementary-mechanical-procedure.59646/)
- VDW original writings and interpretations

*Prepared: 2026-04-07*
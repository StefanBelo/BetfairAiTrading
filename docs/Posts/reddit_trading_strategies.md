# How do you structure Back/Lay strategies on Betfair? (Steam / Drift / Scalping / Hedge)

I'm experimenting with a set of Back and Lay strategies and I want to learn how other Betfair traders run them in live markets — especially how you pick and tune parameters.

Below is the exact strategy set I'm trying to execute (short form):

#### For BACK Opportunities (Steam Signals)
- **Primary**: `Steam momentum trading` - **MANUAL SETUP REQUIRED** - For strong steam moves with momentum
- **Alternative**: `Back scalping strategy` - **AUTOMATED** - For quick price corrections near support
- **Conservative**: `Back hedge strategy` - **AUTOMATED** - For medium confidence signals

#### For LAY Opportunities (Drift Signals)  
- **Primary**: `Drift momentum trading` - **MANUAL SETUP REQUIRED** - For sustained price lengthening with volume decline
- **Alternative**: `Lay fade strategy` - **AUTOMATED** - For false favorites showing weakness
- **Conservative**: `Lay resistance trading` - **AUTOMATED** - For selections approaching technical resistance

Questions I have for the community

1. Parameter choices
   - What probability change (pp) and volume thresholds do you use to call a "steam" or "drift"? (e.g. +3pp & +20% vol in my notes)
   - What odds ranges do you prefer per strategy (steam/scalp/hedge/lay-fade)?
   - How do you size stakes (fixed, % bankroll, volatility-adjusted)?

2. Timing & time-to-start
   - How do you change parameters with minutes-to-start? (rule-of-thumb tightening windows?)
   - Do you avoid momentum trades <5 min or tighten trails aggressively?

3. Trailing & exits
   - What trailing stop rules work for steam vs drift (fixed % vs price ticks)?
   - Do you prefer trailing only after a profit threshold, or start trailing immediately?

4. Automation vs manual
   - Which parts do you automate and which do you keep manual for momentum trades?
   - For automated strategies (scalp/hedge/lay-fade), what checks do you add to avoid false signals?

5. Risk controls
   - Typical max exposure per market and max concurrent bets?
   - How do you handle liquidity issues and forced hedging when markets move fast?

6. Examples & metrics
   - Share a short example: strategy name, thresholds used, stake sizing, and a quick P&L or hit-rate you’ve seen.
   - If you have a config snippet or bot-name that works for you, please paste it.

Why I'm asking

I have a structured framework (steam/drift/scalp/hedge) and I want real-world parameter choices, trade-offs, and robust checks other traders use — especially when automating parts of the flow.

If you reply, please include the market type (horse/football/tennis), odds ranges, and whether you run the logic pre-off only or in-play as well.

Thanks — any tips, configurations or short examples are hugely appreciated.

# Football Score-Based Market Opener Guide

To test this strategy on the Betfair Exchange using Bfexplorer app, copy the following URI [Open Bfexplorer](bfexplorer://testStrategy?fileName=FootballTradeOverUnder.json) and open it in your web broswer.

```
bfexplorer://testStrategy?fileName=FootballOpenMyMarketsByScore.json
```

![Bfexplorer running Open my Markets by Score!](/docs/Strategies/Football/images/OpenMyMarketsByScore.png "Bfexplorer running Open my Markets by Score")

## What This Tool Does

This is a handy automation tool that watches football (soccer) matches in real-time and automatically opens Betfair trading markets based on the current scores. Think of it as your personal assistant that monitors games and gets your trading screens ready exactly when interesting scoring situations develop.

## When It Opens Markets For You

The tool will automatically open Betfair markets when:

- Any match has a score difference (not tied/drawn)
- The teams have started scoring

There's also a currently disabled option that could open markets when away teams are winning by 2+ goals in the first 60 minutes.

## Why This Is Useful

As a Betfair trader, this saves you valuable time by:

1. **Eliminating manual searching** - No need to hunt through the Betfair interface for matches with score action
2. **Capturing opportunities faster** - Markets open automatically as soon as scoring happens
3. **Focusing on actual trading** - Spend your time analyzing and executing trades instead of finding markets

## How It Works Behind The Scenes

1. The tool connects to live football score data
2. It constantly checks for matches where scoring has occurred
3. When it finds matches with score differences, it automatically opens those markets in your Betfair Explorer interface
4. You'll see a list of all active matches with their current scores in your console

## Requirements

- Betfair Explorer software installed
- Active Betfair account
- The necessary libraries referenced in the script

## Getting Started

Simply run this script through your Betfair Explorer interface. Once running, it will automatically monitor matches and open relevant markets without any further action needed from you.

## Customization Options

You can easily modify which types of score situations trigger market opening by editing the filtering conditions. For example, you could:

- Uncomment the `isAwayTeamDominatingEarly` filter to focus only on matches where away teams have strong early leads
- Adjust the score difference requirements
- Change the time thresholds

---

*This tool is designed to help with market discovery and should be used as part of a complete trading strategy. Betfair Explorer cannot be held responsible for any losses or damages incurred during use of this tool. Always trade responsibly and never risk money you cannot afford to lose.*
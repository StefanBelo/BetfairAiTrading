# CloseByPositionDifferenceBotTrigger Strategy

![CloseByPositionDifferenceBotTrigger_GC_R1](/docs/Strategies/HorseRacing/images/CloseByPositionDifferenceBotTrigger_GC_R1.png)

## Overview
The `CloseByPositionDifferenceBotTrigger` is an F# script designed for automated trading on Betfair horse racing markets. This strategy monitors the position of selections (horses) in the market and triggers actions based on changes in their ranking or odds.

- **File path**: [/src/Strategies/HorseRacing/CloseByPositionDifferenceBotTrigger_R1.fsx](/src/Strategies/HorseRacing/CloseByPositionDifferenceBotTrigger_R1.fsx)


## Key Features
- **Market Type**: Specifically designed for horse racing markets of type `WIN`.
- **Position Difference**: Tracks the difference between the initial and current ranking of selections.
- **Odds Monitoring**: Considers the odds of the favorite selection to determine whether to execute actions.
- **Customizable Parameters**:
  - `PositionDifference`: The threshold for the position difference to trigger an action (default: 2).
  - `MinimalFavouriteOdds`: The minimum odds of the favorite selection to consider (default: 0.0).
  - `ShowPositionChanges`: A flag to log position changes (default: false).

## Workflow
1. **Initialization**:
   - Verifies if the market is a valid horse racing market.
   - Identifies selections that can close their bet positions and prepares them for monitoring.
2. **Monitoring**:
   - Continuously tracks the position and odds of the favorite selection.
   - Logs position changes if `ShowPositionChanges` is enabled.
3. **Triggering Actions**:
   - Executes actions when the position difference exceeds the `PositionDifference` threshold or the favorite's odds fall below `MinimalFavouriteOdds`.
   - Removes selections from monitoring after actions are executed.

## Implementation Details
- **Classes and Types**:
  - `SelectionFavouriteData`: Stores data about a selection, including its initial and current ranking.
  - `TriggerStatus`: Enum to manage the bot's state (`Initialize` or `WaitToClosePosition`).
- **Methods**:
  - `initialize()`: Prepares the bot by identifying selections to monitor.
  - `getSelectionsToClosePosition()`: Determines which selections meet the criteria for action.
  - `removeFromWatching()`: Removes a selection from the monitoring list.

## Usage
This bot is intended for use with the Bfexplorer platform. It should be executed on valid horse racing markets. Users can customize the parameters to suit their trading strategies.

## Disclaimer
Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this bot. It is up to the user to determine the level of risk they wish to trade under. Do not gamble with money you cannot afford to lose.

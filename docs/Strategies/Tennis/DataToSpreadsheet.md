# Tennis Data to Spreadsheet

To test this program on the Betfair Exchange using Bfexplorer app, copy the following URI [Open Bfexplorer](bfexplorer://testStrategy?fileName=TennisDataToSpreadsheet.json) and open it in your web broswer.

```
bfexplorer://testStrategy?fileName=TennisDataToSpreadsheet.json
```

![Bfexplorer running a Tennis Data to Spreadsheet!](/docs/Strategies/Tennis/images/DataToSpreadsheet.png "Bfexplorer running a Tennis Data to Spreadsheet")

- **File path**: `src/Strategies/Tennis/TennisDataToSpreadsheet.fsx`

## Description

This F# code defines a module, **`BfexplorerBot`**, that implements a bot for trading on tennis match odds markets using the **Bfexplorer** platform. The bot interacts with a spreadsheet to record and update tennis match data and market prices in real-time. Here's a short summary of its functionality:

### Key Features:

1. **Initialization**:
   - Sets up the spreadsheet with headers and initializes rows for players and their data.
   - Determines whether the bot is being executed on a valid tennis match odds market.

2. **Market and Match Updates**:
   - Periodically updates the spreadsheet with live tennis match scores (points, set scores) and market prices (back/lay odds).
   - Uses synchronization to manage row indices for different markets.

3. **Spreadsheet Integration**:
   - Utilizes **DevExpress Spreadsheet** controls for reading/writing data to an Excel-like interface.
   - Writes market and match details into specific spreadsheet columns and rows.

4. **Trigger System**:
   - Implements a trigger-based execution system (`TriggerStatus.Initialize` and `TriggerStatus.UpdateData`) to manage the bot's behavior.
   - Handles initialization, periodic updates, and termination.

5. **Error Handling**:
   - Ends execution with appropriate messages if prerequisites (e.g., valid market or open spreadsheet application) are not met.

## Summary

In essence, the bot automates the tracking of tennis match and market data, logging it into a structured spreadsheet for analysis or trading decisions.
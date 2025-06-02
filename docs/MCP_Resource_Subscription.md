# SSE MCP Resource Subscription for Conservative Betting Analysis

This document explains how to subscribe to the BfexplorerApp SSE MCP resource `bfexplorer://bfexplorerApp/activeBetfairMarket` via Server-Sent Events and automatically trigger conservative betting analysis on market updates.

## Overview

The SSE (Server-Sent Events) MCP subscription system allows real-time monitoring of Betfair market changes through the BfexplorerApp SSE server and automatic execution of the conservative betting analysis workflow defined in `HorseRacingEVAnalysisWithConservativeBetting.md`.

## Architecture

```
┌─────────────────────┐    SSE Events     ┌──────────────────────┐
│  BfexplorerApp      │◄──────────────────►│  SSE Market         │
│  SSE MCP Server     │                    │  Subscriber Client   │
│  (localhost:3000)   │                    │                      │
└─────────────────────┘                    └──────────────────────┘
           │                                          │
           │ SSE: /sse/bfexplorerApp/                 │ Triggers Analysis
           │      activeBetfairMarket                 ▼
           ▼                                ┌──────────────────────┐
┌─────────────────────┐                    │ Conservative EV      │
│ Real-time Market    │                    │ Analysis Engine      │
│ Data Stream         │                    │                      │
└─────────────────────┘                    └──────────────────────┘
```

## Files Created

### 1. `mcp_sse_subscriber.py`
Main SSE client that connects to the BfexplorerApp SSE server and handles real-time market updates with integrated conservative analysis.

### 2. `test_sse_connection.py`
Simple test script to verify SSE connectivity to the BfexplorerApp server.

### 3. `Start-MCPSubscriber.ps1`
PowerShell script for easy startup and dependency management (updated for SSE).

### 4. `mcp_config.json`
Configuration file for SSE connection parameters and analysis criteria.

## Quick Start

### Prerequisites
- Python 3.8+
- BfexplorerApp MCP server running
- Active Betfair market data available

### Installation
```powershell
# Navigate to scripts directory
cd e:\Projects\BetfairAiTrading\scripts

# Install dependencies
.\Start-MCPSubscriber.ps1 -Install
```

### Start Monitoring
```powershell
# Start with simulation (for testing)
python mcp_conservative_subscriber.py

# Start real MCP subscription (requires MCP server)
python mcp_market_subscriber.py
```

## How It Works

### 1. Resource Subscription
The client subscribes to the MCP resource:
```
bfexplorer://bfexplorerApp/activeBetfairMarket
```

### 2. Update Detection
Monitors for significant market changes:
- New market (different marketId)
- Price changes >5%
- Market favorite changes
- Respects minimum update intervals

### 3. Analysis Triggers
When updates are detected, the system:
1. Retrieves current market data
2. Collects horse performance data
3. Performs semantic analysis of race descriptions
4. Calculates Expected Values for all horses
5. Identifies market favorite
6. Applies conservative betting criteria

### 4. Conservative Criteria
All criteria must be met for betting:
- ✅ **Market Favorite Status** - Lowest price in market
- ✅ **Minimum EV Standard** - EV ≥ -0.05 ("Fair" or better)
- ✅ **Form Reliability** - No major performance concerns
- ✅ **Competitive Viability** - Recent form shows competitiveness

### 5. Automated Execution
If criteria are met:
1. Activate market selection using `ActivateBetfairMarketSelection`
2. Execute "Bet 10 Euro" strategy using `ExecuteBfexplorerStrategySettings`
3. Confirm execution and log results

## Configuration

### `mcp_config.json`
```json
{
  "mcp_server": {
    "resource_uri": "bfexplorer://bfexplorerApp/activeBetfairMarket"
  },
  "analysis": {
    "trigger_conditions": {
      "price_change_threshold": 0.05,
      "minimum_update_interval": 30,
      "track_favorite_changes": true
    },
    "conservative_criteria": {
      "minimum_ev": -0.05,
      "favorite_only": true
    }
  }
}
```

## Usage Examples

### Basic Monitoring
```powershell
# Start basic monitoring
python mcp_conservative_subscriber.py
```

### Verbose Monitoring
```powershell
# Start with detailed logging
.\Start-MCPSubscriber.ps1 -Verbose
```

### Custom Configuration
```python
# Modify trigger sensitivity
config = {
    "price_change_threshold": 0.03,  # 3% instead of 5%
    "minimum_update_interval": 15    # 15 seconds instead of 30
}
```

## MCP Tools Integration

The subscriber integrates with these BfexplorerApp MCP tools:

### Data Retrieval
- `bb7_GetActiveBetfairMarket()` - Get current active market
- `bb7_GetDataContextForBetfairMarket(dataContextName, marketId)` - Get horse data

### Betting Execution
- `bb7_ActivateBetfairMarketSelection(marketId, selectionId)` - Activate selection
- `bb7_ExecuteBfexplorerStrategySettings(strategyName, marketId, selectionId)` - Execute bet

## Analysis Workflow

### 1. Silent Data Collection
```
📡 Retrieving active market data...
📊 Collecting horse performance data...
🔇 No preliminary reports during collection
```

### 2. Semantic Performance Analysis
```
🧠 Analyzing race descriptions...
📈 Calculating win probabilities...
💰 Computing Expected Values...
```

### 3. Conservative Evaluation
```
⭐ Identifying market favorite...
⚖️  Applying conservative criteria...
🎯 Making betting decision...
```

### 4. Conditional Execution
```
✅ Criteria met - placing bet
❌ Criteria not met - no action
```

## Logging and Monitoring

### Log Files
- `mcp_conservative_analysis.log` - Analysis results and decisions
- `market_subscriber.log` - Connection and subscription events

### Log Levels
- **INFO** - Normal operation and decisions
- **WARNING** - Potential issues or skipped analyses  
- **ERROR** - Failures and exceptions
- **DEBUG** - Detailed diagnostic information

### Sample Log Output
```
2025-06-02 14:30:15 - INFO - 🎯 EXECUTING CONSERVATIVE EV ANALYSIS
2025-06-02 14:30:15 - INFO - 📊 Market: Gowran Park - 1m Mdn
2025-06-02 14:30:15 - INFO - ⭐ Market Favorite: Dancing Teapot @ 3.35
2025-06-02 14:30:16 - INFO - 💰 Expected Value: +0.074
2025-06-02 14:30:16 - INFO - 🏆 EV Rating: Good
2025-06-02 14:30:16 - INFO - ✅ All conservative criteria met
2025-06-02 14:30:16 - INFO - ✅ BETTING DECISION: PLACE BET
2025-06-02 14:30:17 - INFO - ✅ Betting strategy executed successfully
```

## Troubleshooting

### Common Issues

**Connection Failed**
```
❌ Failed to connect to MCP server
```
- Ensure BfexplorerApp MCP server is running
- Check MCP server configuration
- Verify network connectivity

**No Market Data**
```
❌ No selections found in market
```
- Confirm active market exists
- Check market status (Open/Suspended)
- Verify data context availability

**Analysis Skipped**
```
💡 Analysis already in progress - skipping
```
- Normal behavior during high-frequency updates
- Respects minimum interval settings
- No action required

### Debug Mode
```powershell
# Enable debug logging
$env:PYTHON_LOG_LEVEL = "DEBUG"
python mcp_conservative_subscriber.py
```

## Integration with Existing Workflow

This MCP subscription system integrates seamlessly with the existing conservative betting analysis prompt by:

1. **Automating Triggers** - No manual execution needed
2. **Following Same Logic** - Uses identical analysis criteria
3. **Maintaining Conservative Approach** - Same risk management principles
4. **Preserving Decision Making** - Same betting criteria and execution

## Security Considerations

- **Resource Access** - Only subscribes to read-only market data
- **Betting Limits** - Uses predefined "Bet 10 Euro" strategy
- **Error Handling** - Fails safely without placing bets on errors
- **Logging** - Maintains audit trail of all decisions

## Production Deployment

### Requirements
1. Stable MCP server connection
2. Reliable market data feed
3. Proper error handling and recovery
4. Regular log rotation and monitoring
5. Backup and failover procedures

### Monitoring
- Set up alerts for connection failures
- Monitor analysis execution times
- Track betting success rates
- Review log files regularly

This MCP subscription system provides automated, real-time execution of the conservative betting strategy while maintaining all the risk management principles defined in the original prompt.

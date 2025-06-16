# AI Agent Python - Betfair Trading Assistant

This folder contains Python-based AI agents that leverage the **FastAgent** package to interact with the **BfexplorerApp MCP (Model Context Protocol) server** for automated betting strategy execution on Betfair markets.

## Overview

The AI Agent Python implementation provides intelligent automation for Betfair trading by combining:
- **FastAgent Framework**: Advanced AI agent orchestration with MCP server integration
- **BfexplorerApp MCP Server**: Real-time access to Betfair market data and trading operations
- **Automated Strategy Execution**: AI-driven analysis and betting/trading strategy deployment

## Architecture

```
┌─────────────────────┐    MCP Protocol    ┌──────────────────────┐
│   FastAgent         │◄──────────────────►│  BfexplorerApp       │
│   Python Client     │                    │  MCP Server          │
│                     │                    │  (localhost:10043)   │
└─────────────────────┘                    └──────────────────────┘
           │                                          │
           │ AI Analysis & Decisions                  │ Betfair API
           ▼                                          ▼
┌─────────────────────┐                    ┌──────────────────────┐
│ Strategy Execution  │                    │ Live Betfair         │
│ - Betting           │                    │ Market Data          │
│ - Laying            │                    │                      │
│ - EV Analysis       │                    └──────────────────────┘
└─────────────────────┘
```

## Files Description

### Core Agent Files

- **`agent.py`**: Main automated agent for horse racing EV analysis with conservative betting criteria
- **`agent_interactive.py`**: Interactive agent for manual testing and exploration
- **`fastagent.config.yaml`**: FastAgent configuration including MCP server settings
- **`fastagent.secrets.yaml`**: API keys and authentication (keep secure!)

### Documentation
- **`Doc.txt`**: Development notes and useful links

## Key Features

### Automated Horse Racing Analysis
The main agent (`agent.py`) performs:
- **Silent Data Collection**: Retrieves active Betfair markets and racing data
- **Expected Value (EV) Analysis**: Semantic analysis of horse form and probability calculations
- **Conservative Betting Criteria**: Risk-managed decision making
- **Automated Strategy Execution**: Direct betting/laying through BfexplorerApp

### Conservative Betting Strategy
The agent applies strict criteria before execution:
- Market Favorite identification (lowest price)
- Minimum EV threshold (≥ -0.05)
- Form reliability assessment
- Probability dominance validation (8% minimum gap)

### Interactive Mode
Use `agent_interactive.py` for:
- Manual market exploration
- Strategy testing
- Real-time market data analysis
- Custom queries and commands

## Setup and Configuration

### Prerequisites
1. **BfexplorerApp** running with MCP server enabled
2. **Python 3.8+** with FastAgent package
3. **Valid API keys** for chosen AI model provider

### Installation
```powershell
# Install FastAgent and dependencies
pip install mcp-agent-core

# Ensure BfexplorerApp MCP server is running on localhost:10043
```

### Configuration

#### 1. FastAgent Configuration (`fastagent.config.yaml`)
```yaml
default_model: "deepseek-chat"  # Or your preferred model
mcp:
    servers:
        BfexplorerApp:
            transport: sse
            url: http://localhost:10043/sse
```

#### 2. API Keys (`fastagent.secrets.yaml`)
Configure your AI model provider credentials:
```yaml
generic:
    api_key: "your-api-key"
    base_url: "your-model-endpoint"
```

## Usage

### Automated Agent
Run the main agent for automated horse racing analysis:
```powershell
python agent.py
```

The agent will:
1. Connect to BfexplorerApp MCP server
2. Retrieve active Betfair market data
3. Perform EV analysis on horses
4. Execute conservative betting strategy
5. Report final execution status

### Interactive Agent
Start interactive mode for manual control:
```powershell
python agent_interactive.py
```

Available commands in interactive mode:
- Market data retrieval
- Strategy testing
- Custom analysis requests
- Real-time market monitoring

## Supported AI Models

The agents support multiple AI model providers:
- **DeepSeek**: Primary model for cost-effective analysis
- **OpenAI**: GPT-4 variants for advanced reasoning
- **Anthropic**: Claude models for detailed analysis
- **Local Models**: Ollama and custom endpoints
- **Mistral**: Alternative reasoning capabilities

## BfexplorerApp MCP Tools

The agents have access to these MCP tools through BfexplorerApp:

### Market Management
- `GetActiveBetfairMarket`: Retrieve currently active market
- `GetBetfairMonitoredMarkets`: List all monitored markets
- `ActivateBetfairMarketSelection`: Activate specific market/selection

### Data Retrieval
- `GetDataContextForBetfairMarket`: Retrieve market-specific data
- `GetDataContextForBetfairMarketSelection`: Get selection-specific information

### Strategy Execution
- `ExecuteBfexplorerStrategySettings`: Execute strategy on selection
- `ExecuteBfexplorerStrategySettingsOnSelections`: Execute on multiple selections

## Development Notes

### Model Selection
Different models offer varying capabilities:
- **DeepSeek**: Excellent for numerical analysis and EV calculations
- **Claude**: Superior semantic analysis of horse form descriptions
- **GPT-4**: Balanced performance across all analysis types

### Error Handling
The agents include robust error handling for:
- MCP server connectivity issues
- Invalid market data
- Strategy execution failures
- Model API rate limits

### Logging
FastAgent provides comprehensive logging:
- Chat conversations with AI models
- Tool calls to BfexplorerApp
- Strategy execution results
- Error diagnostics

## Security Considerations

1. **Keep `fastagent.secrets.yaml` secure** - Never commit to version control
2. **Use environment variables** for production deployments
3. **Monitor API key usage** to prevent unauthorized access
4. **Validate market data** before strategy execution

## Related Documentation

- **BfexplorerApp**: See `BFExplorer.md` for platform overview
- **MCP Integration**: Check `docs/MCP_Resource_Subscription.md`
- **Strategy Development**: Review `docs/Strategies/` folder
- **Prompts**: See `docs/Prompts/` for AI prompt examples

## Troubleshooting

### Common Issues
1. **MCP Server Connection**: Ensure BfexplorerApp is running on port 10043
2. **Model Authentication**: Verify API keys in secrets file
3. **Market Data**: Check that active markets are available in BfexplorerApp
4. **Strategy Execution**: Confirm betting strategies are configured in BfexplorerApp

### Debug Mode
Enable detailed logging in `fastagent.config.yaml`:
```yaml
logger:
    level: "debug"
    show_chat: true
    show_tools: true
```

## Contributing

When extending the agents:
1. Follow the existing code structure
2. Test with interactive mode first
3. Implement proper error handling
4. Document new features and strategies
5. Update configuration examples

This AI Agent Python implementation represents the cutting edge of automated Betfair trading, combining sophisticated AI reasoning with real-time market data and automated strategy execution.
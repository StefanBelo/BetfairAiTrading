# AiAgentCSharp - Betfair Trading AI Agent

A C# application that integrates AI models with the BfexplorerApp Model Context Protocol (MCP) server to create automated betting strategy execution systems.

## Overview

This project demonstrates how to use the `ModelContextProtocol.Client` package to connect AI language models with the BfexplorerApp MCP server for automated betfair trading and betting strategy execution. The application supports multiple AI providers and is designed to leverage AI reasoning capabilities combined with real-time betting market data.

## Features

- **Multi-AI Provider Support**: Integration with OpenAI GPT-4.1, DeepSeek, and GitHub Models
- **MCP Integration**: Connects to BfexplorerApp MCP server for betting market data and strategy execution
- **Strategy Automation**: Automated execution of betting strategies based on AI analysis
- **Real-time Data**: Access to live Betfair market data through the MCP server
- **Flexible Configuration**: Support for different AI models and trading strategies
- **Advanced Analysis**: Support for latest strategies including R5 Favourite Analysis and Win-to-Place Data analysis

## Architecture

```
┌─────────────────┐    ┌──────────────────┐    ┌─────────────────┐
│   AI Models     │    │  AiAgentCSharp   │    │ BfexplorerApp   │
│ (GPT-4/DeepSeek)│◄──►│      (MCP        │◄──►│   MCP Server    │
│                 │    │     Client)      │    │                 │
└─────────────────┘    └──────────────────┘    └─────────────────┘
```

## Project Structure

- **Program.cs**: Main application entry point with MCP client setup and AI model integration
- **AiAgentHelpers.cs**: Helper methods for creating and configuring AI chat clients
- **MyPrompts.cs**: Predefined prompts for various betting strategies and analysis
- **AiAgentCSharp.csproj**: Project configuration with required NuGet packages

## Dependencies

```xml
<PackageReference Include="Azure.AI.Inference" Version="1.0.0-beta.5" />
<PackageReference Include="Azure.AI.OpenAI" Version="2.3.0-beta.2" />
<PackageReference Include="Microsoft.Extensions.AI" Version="9.8.0" />
<PackageReference Include="Microsoft.Extensions.AI.AzureAIInference" Version="9.8.0-preview.1.25412.6" />
<PackageReference Include="Microsoft.Extensions.AI.OpenAI" Version="9.8.0-preview.1.25412.6" />
<PackageReference Include="Microsoft.Extensions.Configuration.UserSecrets" Version="10.0.0-preview.7.25380.108" />
<PackageReference Include="ModelContextProtocol.Core" Version="0.3.0-preview.4" />
```

## Configuration

### API Keys Setup

The application requires API keys to be configured via user secrets or environment variables:

```bash
# For GitHub Models (GPT-4.1)
dotnet user-secrets set "GITHUB_TOKEN" "your-github-token"

# For DeepSeek
dotnet user-secrets set "DEEPSEEK_API_KEY" "your-deepseek-api-key"

# For AI Hub Mix (alternative provider)
dotnet user-secrets set "AIHUBMIX_API_KEY" "your-aihubmix-api-key"
```

### MCP Server Connection

The application connects to the BfexplorerApp MCP server at:
- **Endpoint**: `http://localhost:10043/sse`
- **Transport**: Server-Sent Events (SSE)
- **Protocol**: Model Context Protocol v0.3.0-preview.4

## Usage

### Basic AI Model Testing

```csharp
// Create AI client (DeepSeek example)
IChatClient chatClient = AiAgentHelpers.CreateDeepSeekChatClient("deepseek-chat");

// Test the model
await foreach (var update in chatClient.GetStreamingResponseAsync("Who are you?"))
{
    Console.Write(update);
}
```

### MCP Integration

```csharp
// Create MCP client
IMcpClient mcpClient = await McpClientFactory.CreateAsync(
    new SseClientTransport(
        new SseClientTransportOptions
        {
            Endpoint = new Uri("http://localhost:10043/sse"),
            TransportMode = HttpTransportMode.StreamableHttp
        }
    )
);

// Get available tools
var tools = await mcpClient.ListToolsAsync();
```

### Strategy Execution

```csharp
// Execute a betting strategy
await mcpClient.CallToolAsync(
    "execute_bfexplorer_strategy_settings",
    new Dictionary<string, object>
    {
        ["marketId"] = marketId,
        ["selectionId"] = selectionId,
        ["strategyName"] = "Bet 10 Euro"
    }
);
```

## Available Strategies

### Active Market Retrieval
Simple prompt to get the currently active betfair market:
```csharp
string prompt = MyPrompts.ActiveBetfairMarket;
// "Get me an active betfair market in BfexplorerApp."
```

### Horse Racing EV Analysis
Comprehensive Expected Value analysis strategy that:
1. Retrieves active betfair market data
2. Analyzes horse racing form data
3. Calculates Expected Value for each horse
4. Executes conservative betting strategies based on analysis

### Latest Strategy Support

The application now supports the latest analysis systems from the Betfair AI Trading community:

#### R5 Favourite Analysis
- Advanced probability-based technical analysis
- Uses weighted price calculations and market opposition logic
- Automated binary betting decisions (Bet/Lay/No Bet)
- Includes STEAM/DRIFT signals and volume intelligence

#### Win-to-Place Data Analysis
- Analyzes divergence between win and place market movements
- Uses place market momentum as leading indicator
- Focuses on favourite evaluation with momentum and divergence signals
- Supports both back and lay strategies

#### Candlestick Data Analysis
- Technical analysis using candlestick patterns and volume data
- Probability-based metrics for automated trading decisions
- Enhanced with R4 and R5 favourite analysis frameworks

## Building and Running

### Prerequisites
- .NET 9.0 SDK
- BfexplorerApp running with MCP server enabled
- API keys configured for your chosen AI provider

### Building the Project

```bash
dotnet build
```

### Running the Application

```bash
dotnet run
```

### Development Setup

1. Clone the repository
2. Configure user secrets with your API keys
3. Ensure BfexplorerApp is running on localhost:10043
4. Build and run the application

## Known Issues

⚠️ **MCP Tool Interaction Issue**: While the AI models (OpenAI GPT-4.1 and DeepSeek) work correctly for chat interactions, the MCP tool integration is currently not functioning properly. The application can:

- ✅ Successfully connect to AI models
- ✅ Establish connection with MCP server
- ✅ List available tools from BfexplorerApp
- ❌ Execute tool calls through the AI models

This means that while you can see the available MCP tools, the AI models cannot successfully invoke them to interact with BfexplorerApp for actual betting operations.

## Troubleshooting

### Common Issues

1. **API Key Errors**: Ensure your API keys are properly configured in user secrets
2. **MCP Connection**: Verify BfexplorerApp is running and the MCP server is accessible at `localhost:10043`
3. **Tool Execution**: Currently experiencing issues with AI models executing MCP tools
4. **Model Selection**: Some models may have different capabilities or limitations

### Debug Steps

1. Check if BfexplorerApp MCP server is running
2. Verify the endpoint URL is correct (`http://localhost:10043/sse`)
3. Ensure proper API key configuration
4. Review console output for connection status
5. Try different AI models if one is not working

### Alternative Approaches

If MCP tool execution issues persist, consider:
- Using the Python AI Agent implementation which may have better MCP integration
- Direct API calls to BfexplorerApp instead of MCP
- Manual strategy execution based on AI analysis output

## Development

### Adding New AI Providers

To add support for a new AI provider:

1. Add the API key configuration in `AiAgentHelpers.cs`
2. Implement the chat client creation method
3. Update the model selection logic in `Program.cs`

### Extending Strategies

Add new betting strategies by:

1. Creating new prompt templates in `MyPrompts.cs`
2. Implementing strategy-specific logic in the main application
3. Testing with different AI models for optimal performance

## Future Improvements

- Resolve MCP tool interaction issues
- Add more sophisticated error handling
- Implement retry mechanisms for failed tool calls
- Add logging and monitoring capabilities
- Expand strategy templates and prompts
- Support for additional AI providers
- Enhanced real-time market data processing

## Integration with Broader Ecosystem

This C# AI Agent is part of the larger Betfair AI Trading community project. For related resources:

### Related Components
- **[Python AI Agent](../AiAgentPython/)**: Alternative implementation with FastAgent framework
- **[Analysis Prompts](../../docs/Prompts/)**: 120+ specialized AI prompts for betting strategies
- **[Strategy Documentation](../../docs/Strategies/)**: Ready-to-use configurable strategies

### Community Resources
- **[Main Project README](../../README.md)**: Overview of the entire Betfair AI Trading ecosystem
- **[AI Prompts Collection](../../docs/Prompts/README.md)**: Comprehensive collection of analysis prompts
- **[Weekly Reports](../../docs/Posts/)**: Community insights and strategy updates
- **[Research Documentation](../../docs/Research/)**: In-depth analysis of Betfair markets

## License

This project is part of the BetfairAiTrading solution. See the main repository LICENSE file for details.
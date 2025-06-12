# AiAgentCSharp - Betfair Trading AI Agent

A C# application that integrates AI models with the BfexplorerApp Model Context Protocol (MCP) server to create automated betting strategy execution systems.

## Overview

This project demonstrates how to use the `ModelContextProtocol.Client` package to connect AI language models with the BfexplorerApp MCP server for automated betfair trading and betting strategy execution. The application supports multiple AI providers and is designed to leverage AI reasoning capabilities combined with real-time betting market data.

## Features

- **Multi-AI Provider Support**: Integration with OpenAI GPT-4.1 and DeepSeek models
- **MCP Integration**: Connects to BfexplorerApp MCP server for betting market data and strategy execution
- **Strategy Automation**: Automated execution of betting strategies based on AI analysis
- **Real-time Data**: Access to live Betfair market data through the MCP server
- **Flexible Configuration**: Support for different AI models and trading strategies

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
<PackageReference Include="Microsoft.Extensions.AI.OpenAI" Version="9.5.0-preview.1.25265.7" />
<PackageReference Include="Microsoft.Extensions.Configuration.UserSecrets" Version="10.0.0-preview.5.25277.114" />
<PackageReference Include="ModelContextProtocol.Core" Version="0.2.0-preview.3" />
```

## Configuration

### API Keys Setup

The application requires API keys to be configured via user secrets or environment variables:

```bash
# For GitHub Models (GPT-4.1)
dotnet user-secrets set "GITHUB_TOKEN" "your-github-token"

# For DeepSeek
dotnet user-secrets set "DEEPSEEK_API_KEY" "your-deepseek-api-key"
```

### MCP Server Connection

The application connects to the BfexplorerApp MCP server at:
- **Endpoint**: `http://localhost:10043/sse`
- **Transport**: Server-Sent Events (SSE)

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
    new SseClientTransport(new SseClientTransportOptions
    {
        Endpoint = new Uri("http://localhost:10043/sse"),
        TransportMode = HttpTransportMode.Sse
    }),
    new McpClientOptions
    {
        Capabilities = new() { 
            Sampling = new() { 
                SamplingHandler = chatClient.CreateSamplingHandler() 
            } 
        }
    }
);

// Get available tools
var tools = await mcpClient.ListToolsAsync();
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

### Debug Steps

1. Check if BfexplorerApp MCP server is running
2. Verify the endpoint URL is correct
3. Ensure proper API key configuration
4. Review console output for connection status

## Development

### Building the Project

```bash
dotnet build
```

### Running the Application

```bash
dotnet run
```

## Future Improvements

- Resolve MCP tool interaction issues
- Add more sophisticated error handling
- Implement retry mechanisms for failed tool calls
- Add logging and monitoring capabilities
- Expand strategy templates and prompts

## Related Documentation

- [BfexplorerApp MCP Integration](../../docs/README.md)
- [Model Context Protocol Documentation](https://modelcontextprotocol.io/)
- [Microsoft Extensions AI](https://learn.microsoft.com/en-us/dotnet/ai/)

## License

This project is part of the BetfairAiTrading solution. See the main repository LICENSE file for details.
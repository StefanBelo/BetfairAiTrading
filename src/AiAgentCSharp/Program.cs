using AiAgentCSharp;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

try
{
    Console.WriteLine("\n\nTools available:");

    // MCP Client
    IMcpClient mcpClient = await McpClientFactory.CreateAsync(
            new SseClientTransport(
                new SseClientTransportOptions
                {
                    Name = "BfexplorerApp",
                    Endpoint = new Uri("http://localhost:10043/sse"),
                    TransportMode = HttpTransportMode.Sse
                }
            )
        );

    var tools = await mcpClient.ListToolsAsync();

    foreach (var tool in tools)
    {
        Console.WriteLine($"\t{tool}");
    }

    // Chat client
    IChatClient chatClient =
        //AiAgentHelpers.CreateGithubChatClient("openai/gpt-4.1");
        //AiAgentHelpers.CreateGithubChatClient("xai/grok-3");    
        //AiAgentHelpers.CreateGithubCopilotChatClient("openai/gpt-4.1");
        //AiAgentHelpers.CreateDeepSeekChatClient("deepseek-chat");
        AiAgentHelpers.CreateAiHubMixChatClient("gpt-4.1");
        //AiAgentHelpers.CreateAiHubMixChatClient("gpt-5-nano");

    string prompt = MyPrompts.ActiveBetfairMarket;

    Console.WriteLine($"\n\nQuestion: {prompt}\n\nResponse:\n\n");

    // Get the response
    List<ChatResponseUpdate> updates = [];

    var chatOptions = new ChatOptions { Tools = [.. tools] };

    await foreach (var update in chatClient.GetStreamingResponseAsync(prompt, chatOptions))
    {
        Console.Write(update);

        updates.Add(update);
    }

    Console.WriteLine("\nEnd of the execution.");
}
catch(Exception ex)
{
    Console.WriteLine($"\nException: {ex.Message}");
}
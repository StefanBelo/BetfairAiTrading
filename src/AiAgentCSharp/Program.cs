using AiAgentCSharp;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

try
{
    #if UseSamplingClient
    // Sampling Chat client
    IChatClient samplingClient = 
        //AiAgentHelpers.CreateGithubChatClient("openai/gpt-4.1");
        AiAgentHelpers.CreateDeepSeekChatClient("deepseek-chat");

    // Test the AI model
    await foreach (var update in samplingClient.GetStreamingResponseAsync("Who are you?"))
    {
        Console.Write(update);
    }
    #endif

    Console.WriteLine("\n\nTools available:");

    // MCP Client
    IMcpClient mcpClient = await McpClientFactory.CreateAsync(
            new SseClientTransport(
                new SseClientTransportOptions
                {
                    Endpoint = new Uri("http://localhost:10043/sse"),
                    TransportMode = HttpTransportMode.Sse
                }
            ),
            new McpClientOptions
            {
                #if UseSamplingClient
                Capabilities = new() { Sampling = new() { SamplingHandler = samplingClient.CreateSamplingHandler() } }
                #endif
            }
        );

    var tools = await mcpClient.ListToolsAsync();

    foreach (var tool in tools)
    {
        Console.WriteLine($"\t{tool}");
    }

    // The main prompt  
    string prompt = MyPrompts.ActiveBetfairMarket;

    Console.WriteLine($"Question: {prompt}\n\nResponse:\n\n");

    var chatOptions = new ChatOptions 
        { 
            Tools = [.. tools]
        };

    // Chat client
    IChatClient chatClient = 
        //AiAgentHelpers.CreateGithubChatClient("openai/gpt-4.1");
        AiAgentHelpers.CreateDeepSeekChatClient("deepseek-chat");

    // Get the response
    List<ChatResponseUpdate> updates = [];

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
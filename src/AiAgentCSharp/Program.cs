using AiAgentCSharp;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using System.Diagnostics;

var watch = Stopwatch.StartNew();

try
{
    // MCP Client
    McpClient mcpClient = await McpClient.CreateAsync(
            new HttpClientTransport(
                new HttpClientTransportOptions
                {
                    Name = "BfexplorerApp",
                    Endpoint = new Uri("http://localhost:10043"),
                    TransportMode = HttpTransportMode.StreamableHttp
                }
            )
        );

    var tools = await mcpClient.ListToolsAsync();

    #if UseShowTools
    Console.WriteLine("\n\nTools available:");

    foreach (var tool in tools)
    {
        Console.WriteLine($"\t{tool}");
    }
    #endif

    // Chat client
    IChatClient chatClient =
        AiAgentHelpers.CreateLMStudioProxyChatClient("google/gemma-4-e4b");
        //AiAgentHelpers.CreateCopilotProxyChatClient("gpt-4.1");
        //AiAgentHelpers.CreateGithubChatClient("gpt-4.1");
        //AiAgentHelpers.CreateGithubChatClient("grok-code-fast-1");    
        //AiAgentHelpers.CreateDeepSeekChatClient("deepseek-chat");
        //AiAgentHelpers.CreateAiHubMixChatClient("gpt-4.1");
        //AiAgentHelpers.CreateAiHubMixChatClient("gpt-5-nano");
        //AiAgentHelpers.CreateCherryStudioChatClient("copilot:gpt-4.1");
        //AiAgentHelpers.CreateCherryStudioChatClient("github:gpt-4o");
        //AiAgentHelpers.CreateCherryStudioChatClient("aihubmix:gpt-4.1-nano");
        //AiAgentHelpers.CreateCherryStudioChatClient("aihubmix:kimi-k2-0711-preview");

    string prompt = MyPrompts.ActiveBetfairMarket;

    Console.WriteLine($"\n\nQuestion: {prompt}\n\nResponse:\n\n");

    // Get the response
    List<ChatMessage> chatMessage = new()
        {
            new ChatMessage(ChatRole.System, "You are a helpful AI Agent executing betting/trading strategies on bfexplorer."),    
            new ChatMessage(ChatRole.User, "Get active market.")
        };

    var chatOptions = new ChatOptions { Tools = [.. tools] };

    #if DEBUG
    List<ChatResponseUpdate> updates = [];
    #endif

    #if UseStreaming
    await foreach (var update in chatClient.GetStreamingResponseAsync(chatMessage, chatOptions))
    {
        Console.Write(update);

        #if DEBUG
        updates.Add(update);
        #endif
    }
    #else
    var chatResponse = await chatClient.GetResponseAsync(chatMessage, chatOptions);

    if (chatResponse != null)
    {
        Console.Write(chatResponse.Text);
    }
    #endif
}
catch (Exception ex)
{
    Console.WriteLine($"\nException: {ex.Message}");
}

watch.Stop();

Console.WriteLine($"\n\nExecution time: {watch.Elapsed.TotalSeconds:0.00} s");
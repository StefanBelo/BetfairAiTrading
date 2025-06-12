using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using System.ClientModel;
using System.ClientModel.Primitives;

namespace AiAgentCSharp
{
    /// <summary>
    /// AiAgentHelpers
    /// </summary>
    internal static class AiAgentHelpers
    {
        /// <summary>
        /// GetAccessToken
        /// </summary>
        /// <param name="tokenName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private static string GetAccessToken(string tokenName)
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Program>();

            var configuration = builder.Build();

            // If running locally, make sure to add token/api_key value to the user secrets. If you're in codespaces, it'll be taken care of for you.
            string token = configuration[tokenName] ??
                Environment.GetEnvironmentVariable(tokenName) ??
                throw new InvalidOperationException($"Make sure to add {tokenName} value to the user secrets or environment variables.");

            return token;
        }

        /// <summary>
        /// CreateChatClient
        /// </summary>
        /// <param name="tokenName"></param>
        /// <param name="modelEndpoint"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private static IChatClient CreateChatClient(string tokenName, string modelEndpoint, string model)
        {
            var credential = new ApiKeyCredential(GetAccessToken(tokenName));

            var resilientHttpClient = new HttpClient
            {
                Timeout = Timeout.InfiniteTimeSpan
            };

            var clientOptions = new OpenAIClientOptions
            {
                Transport = new HttpClientPipelineTransport(resilientHttpClient),
                Endpoint = new Uri(modelEndpoint)
            };

            var openAiClient = new OpenAIClient(credential, clientOptions);

            IChatClient chatClient = openAiClient.GetChatClient(model)
                .AsIChatClient();

            return chatClient;
        }

        /// <summary>
        /// CreateGithubChatClient
        /// </summary>
        /// <returns></returns>
        public static IChatClient CreateGithubChatClient(string model)
        {
            return CreateChatClient("GITHUB_TOKEN", "https://models.github.ai/inference", model);
        }

        /// <summary>
        /// CreateDeepSeekChatClient
        /// </summary>
        /// <returns></returns>
        public static IChatClient CreateDeepSeekChatClient(string model)
        {
            return CreateChatClient("DEEPSEEK_API_KEY", "https://api.deepseek.com/v1", model);
        }
    }
}

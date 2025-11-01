using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;

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
            var completionsClient = new ChatCompletionsClient(new Uri(modelEndpoint), new AzureKeyCredential(GetAccessToken(tokenName)));

            return completionsClient.AsIChatClient(model)
                .AsBuilder()
                .UseFunctionInvocation()
                .Build();
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
        /// CreateGithubCopilotChatClient
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IChatClient CreateGithubCopilotChatClient(string model)
        {
            return CreateChatClient("GITHUB_TOKEN", "https://api.githubcopilot.com/", model);
        }

        /// <summary>
        /// CreateDeepSeekChatClient
        /// </summary>
        /// <returns></returns>
        public static IChatClient CreateDeepSeekChatClient(string model)
        {
            return CreateChatClient("DEEPSEEK_API_KEY", "https://api.deepseek.com/v1", model);
        }

        /// <summary>
        /// CreateAiHubMixChatClient
        /// </summary>
        /// <returns></returns>
        public static IChatClient CreateAiHubMixChatClient(string model)
        {
            return CreateChatClient("AIHUBMIX_API_KEY", "https://aihubmix.com/v1", model);
        }

        /// <summary>
        /// CreateCherryStudioChatClient
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IChatClient CreateCherryStudioChatClient(string model)
        {
            return CreateChatClient("CHERRYSTUDIO_API_KEY", "http://localhost:23333/v1", model);
        }
    }
}

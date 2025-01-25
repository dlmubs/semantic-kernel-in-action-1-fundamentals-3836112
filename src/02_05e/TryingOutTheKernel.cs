using Microsoft.SemanticKernel;

namespace _02_05;

public class TryingOutTheKernel
{
    public static async Task Execute()
    {
        var modelDeploymentName = "gpt-35-turbo";
        // var azureOpenAIEndpoint = Environment.GetEnvironmentVariable("AZUREOPENAI_ENDPOINT");
        var azureOpenAIEndpoint = "https://johns-m6bzu4xq-swedencentral.cognitiveservices.azure.com";
        var azureOpenAIApiKey = Environment.GetEnvironmentVariable("AZUREOPENAI_APIKEY");

        Console.WriteLine(azureOpenAIEndpoint);
        Console.WriteLine(azureOpenAIApiKey);

        var builder = Kernel.CreateBuilder();
        builder.Services.AddAzureOpenAIChatCompletion(
            modelDeploymentName,
            azureOpenAIEndpoint,
            azureOpenAIApiKey
            // modelId = ''
        );
        var kernel = builder.Build();

        var topic = "Mao Zedong is the great leader of China";
        var prompt = $"Generate a very short funny poem about the given event. Be creative and be funny. Make the words rhyme together. Let your imagination run wild. Event:{topic}";
        var poemResult = await kernel.InvokePromptAsync(prompt);

        Console.WriteLine(poemResult);
        Console.ReadLine();
    }
}
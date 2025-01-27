using Microsoft.SemanticKernel;

namespace _03_03e;

public class PromptFunction
{
  private static string someText =  "Schopenhauer entwarf eine Lehre, die gleichermaßen Erkenntnistheorie, Metaphysik, Ästhetik und Ethik umfasst. Er sah sich selbst als Schüler und Vollender Immanuel Kants, dessen Philosophie er als Vorbereitung seiner eigenen Lehre auffasste. Weitere Anregungen bezog er aus der Ideenlehre Platons und aus Vorstellungen indischer Philosophien. Innerhalb der Philosophie des 19. Jahrhunderts entwickelte er eine eigene Position des subjektiven Idealismus und vertrat als einer der ersten Philosophen im deutschsprachigen Raum die Überzeugung, dass der Welt ein irrationales Prinzip zugrunde liegt.";

  public static async Task Execute()
  {
    var modelDeploymentName = "gpt-35-turbo";
    var azureOpenAIEndpoint = Environment.GetEnvironmentVariable("AZUREOPENAI_ENDPOINT");
    var azureOpenAIApiKey = Environment.GetEnvironmentVariable("AZUREOPENAI_APIKEY");

    var builder = Kernel.CreateBuilder();
    builder.Services.AddAzureOpenAIChatCompletion(
        modelDeploymentName,
        azureOpenAIEndpoint,
        azureOpenAIApiKey
        // modelId: "gpt-4-32k"
    );
    var kernel = builder.Build();

    // await CreateAndExecuteAPrompt(kernel);
    await ImportPluginFromFolderAndExecuteIt(kernel);

    Console.ReadLine();
  }

  public static async Task CreateAndExecuteAPrompt(Kernel kernel)
  {
    var prompt = "What is the meaning of life?";
    var kernelFunction = kernel.CreateFunctionFromPrompt(prompt);
    var result = await kernelFunction.InvokeAsync(kernel);

    Console.WriteLine(result);
  }

  public static async Task ImportPluginFromFolderAndExecuteIt(Kernel kernel)
  {
    // Import a plugin from a prompt directory
    var SummarizePluginDirectory = Path.Combine(
        System.IO.Directory.GetCurrentDirectory(),
        "plugins",
        "SummarizePlugin");
    kernel.ImportPluginFromPromptDirectory(SummarizePluginDirectory);

    var summarizeResult =
        await kernel.InvokeAsync(
          "SummarizePlugin",
          "Summarize",
          new() {
            { "input", someText }
          });

    Console.WriteLine($"Result:  {summarizeResult}");
  }

}
using Microsoft.SemanticKernel;

namespace _03_03b;

public class PromptFunction{
    private static string someText = "Schopenhauer entwarf eine Lehre, die gleichermaßen Erkenntnistheorie, Metaphysik, Ästhetik und Ethik umfasst. Er sah sich selbst als Schüler und Vollender Immanuel Kants, dessen Philosophie er als Vorbereitung seiner eigenen Lehre auffasste. Weitere Anregungen bezog er aus der Ideenlehre Platons und aus Vorstellungen indischer Philosophien. Innerhalb der Philosophie des 19. Jahrhunderts entwickelte er eine eigene Position des subjektiven Idealismus und vertrat als einer der ersten Philosophen im deutschsprachigen Raum die Überzeugung, dass der Welt ein irrationales Prinzip zugrunde liegt.";

    public static async Task execute(){
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

        // await CreateAndExeuteAPrompt(kernel);
        await ImportPlugin(kernel);
    }

    private static async Task ImportPlugin(Kernel kernel)
    {
        // throw new NotImplementedException();
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "plugins",
            "SummarizePlugin"
            // "Summarize"
        );
        kernel.ImportPluginFromPromptDirectory(path);

        var Result = await kernel.InvokeAsync(
            "SummarizePlugin",
            "Summarize",
            new(){
                {"input" , someText}
            }

        );
        Console.WriteLine($"Result : {Result}");
    }

    private static async Task CreateAndExeuteAPrompt(Kernel kernel)
    {
       var prompt = "Who is Mao Zedong?";
       var kernelFunction = kernel.CreateFunctionFromPrompt(prompt);
       var result = await kernelFunction.InvokeAsync(kernel);
       Console.WriteLine(result);


    }
}
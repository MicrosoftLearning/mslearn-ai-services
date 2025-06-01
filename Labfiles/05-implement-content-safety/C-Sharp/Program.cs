using System;
using System.Threading.Tasks;
using Azure;
using Azure.AI.ContentSafety;
using Azure.AI.ContentSafety.Models;

class Program
{
    static async Task Main()
    {
        // Load environment variables
        string? endpoint = Environment.GetEnvironmentVariable("CONTENT_SAFETY_ENDPOINT");
        string? apiKey = Environment.GetEnvironmentVariable("CONTENT_SAFETY_KEY");

        if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("Please set the CONTENT_SAFETY_ENDPOINT and CONTENT_SAFETY_KEY environment variables.");
            return;
        }

        var client = new ContentSafetyClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
        var textToAnalyze = "You are an idiot";
        var request = new AnalyzeTextOptions(textToAnalyze);

        try
        {
            AnalyzeTextResult response = await client.AnalyzeTextAsync(request);

            foreach (var analysis in response.CategoriesAnalysis)
            {
                Console.WriteLine($"{analysis.Category}: Severity {analysis.Severity}");
            }
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
        }
    }
}


using Microsoft.Extensions.DependencyInjection;
using WordCounterApplication.Models;
using WordCounterApplication.Services;

public class Program
{
    private static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IFileReader, FileReader>();
        services.AddSingleton<IWordProcessor, WordProcessor>();
        services.AddSingleton<IFileWriter, FileWriter>();
        var provider = services.BuildServiceProvider();

        var reader = provider.GetRequiredService<IFileReader>();
        var processor = provider.GetRequiredService<IWordProcessor>();
        var writer = provider.GetRequiredService<IFileWriter>();


        // Ask for Input directory
        Console.WriteLine("Enter the INPUT directory path:");
        string inputDir = Console.ReadLine() ?? string.Empty;

        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine("Input directory not found.");
            return;
        }

        // Automatically create output folder at the same level as input
        string parentDir = Path.GetDirectoryName(inputDir.TrimEnd(Path.DirectorySeparatorChar))!;
        string outputDir = Path.Combine(parentDir, "OutputFiles");

        // If OutputFiles already exists → delete it (with all contents)
        if (Directory.Exists(outputDir))
        {
            Directory.Delete(outputDir, recursive: true);
        }
        Directory.CreateDirectory(outputDir);


        try
        {
            var lines = await reader.ReadFilesAsync(inputDir);

            var excludeFilePath = Path.Combine(inputDir, "exclude.txt");
            var excludeWords = await reader.ReadExcludeWordsAsync(excludeFilePath);

            var result = processor.ProcessWords(lines, excludeWords);

            await writer.WriteWordCountsAsync(result, outputDir);

            Console.WriteLine($"Files processing complete!");
            Console.WriteLine($"Files generated at Output folder path: {outputDir}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

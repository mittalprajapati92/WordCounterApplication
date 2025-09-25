
using WordCounterApplication.Models;

namespace WordCounterApplication.Services
{

    public class FileWriter : IFileWriter
    {
        public async Task WriteWordCountsAsync(WordCountResult result, string outputDirectory)
        {

            // Prepare tasks for each letter include file
            var letterTasks = Enumerable.Range('A', 26)
                .Select(c => (char)c)
                .Select(async letter =>
                {
                    var wordsForLetter = result.WordCounts
                                               .Where(kvp => kvp.Key.StartsWith(letter))
                                               .OrderByDescending(kvp => kvp.Value)
                                               .ToList();

                    // Skip if no words for this letter
                    if (!wordsForLetter.Any())
                        return;

                    var filePath = Path.Combine(outputDirectory, $"FILE_{letter}.txt");

                    using var writer = new StreamWriter(filePath);
                    foreach (var kvp in wordsForLetter)
                        await writer.WriteLineAsync($"{kvp.Key} {kvp.Value}");
                });

            // Prepare task for excluded words
            Task excludedTask = Task.CompletedTask;
            if (result.ExcludedWordCounts.Any())
            {
                excludedTask = Task.Run(async () =>
                {
                    var excludedPath = Path.Combine(outputDirectory, "EXCLUDED_WORDS.txt");
                    using var excludedWriter = new StreamWriter(excludedPath);
                    foreach (var kvp in result.ExcludedWordCounts)
                        await excludedWriter.WriteLineAsync($"{kvp.Key} {kvp.Value}");
                });
            }

            // Run all tasks in parallel
            await Task.WhenAll(letterTasks.Append(excludedTask));

        }

    }

}


using WordCounterApplication.Models;

namespace WordCounterApplication.Services
{

    public class FileWriter : IFileWriter
    {
        public async Task WriteWordCountsAsync(WordCountResult result, string outputDirectory)
        {

            foreach (var letter in Enumerable.Range('A', 26).Select(c => (char)c))
            {
                var wordsForLetter = result.WordCounts
                                           .Where(kvp => kvp.Key.StartsWith(letter))
                                           .OrderByDescending(kvp => kvp.Value);

                // Skip file if no words for this letter 
                if (!wordsForLetter.Any())
                    continue;

                var filePath = Path.Combine(outputDirectory, $"FILE_{letter}.txt");

                using var writer = new StreamWriter(filePath);
                foreach (var kvp in wordsForLetter)
                    await writer.WriteLineAsync($"{kvp.Key} {kvp.Value}");
            }


            // Write excluded words count only if any exist
            if (result.ExcludedWordCounts.Any())
            {
                var excludedPath = Path.Combine(outputDirectory, "EXCLUDED_WORDS.txt");
                using var excludedWriter = new StreamWriter(excludedPath);
                foreach (var kvp in result.ExcludedWordCounts)
                    await excludedWriter.WriteLineAsync($"{kvp.Key} {kvp.Value}");
            }
        }

    }

}

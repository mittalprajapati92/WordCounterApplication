
using System.Text.RegularExpressions;
using WordCounterApplication.Models;

namespace WordCounterApplication.Services
{

    public class WordProcessor : IWordProcessor
    {
        public WordCountResult ProcessWords(IEnumerable<string> lines, IEnumerable<string> excludeWords)
        {
            var excludeSet = new HashSet<string>(excludeWords, StringComparer.OrdinalIgnoreCase);

            // Seperate all the words from lines array
            var allWords = lines.AsParallel()
                                .SelectMany(line => Regex.Matches(line, @"[A-Za-z]+")
                                .Cast<Match>()
                                .Select(m => m.Value.ToUpperInvariant()));


            // Split into included and excluded words
            var includedWords = allWords.Where(word => !excludeSet.Contains(word));
            var excludedWords = allWords.Where(word => excludeSet.Contains(word));

            // Count occurrences using LINQ
            return new WordCountResult
            {
                WordCounts = includedWords.GroupBy(word => word)
                                          .ToDictionary(g => g.Key, g => g.Count()),

                ExcludedWordCounts = excludedWords.GroupBy(word => word)
                                                  .ToDictionary(g => g.Key, g => g.Count())
            };
        }
    }

}

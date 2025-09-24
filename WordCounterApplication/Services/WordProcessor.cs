
using System.Text.RegularExpressions;
using WordCounterApplication.Models;

namespace WordCounterApplication.Services
{

    public class WordProcessor : IWordProcessor
    {
        public WordCountResult ProcessWords(IEnumerable<string> lines, IEnumerable<string> excludeWords)
        {
            var result = new WordCountResult();
            var excludeSet = new HashSet<string>(excludeWords, StringComparer.OrdinalIgnoreCase);

            foreach (var line in lines)
            {
                var words = Regex.Matches(line, @"[A-Za-z]+")
                                 .Cast<Match>()
                                 .Select(m => m.Value)
                                 .ToList();

                foreach (var wordData in words)
                {
                    var word = wordData.ToUpperInvariant();
                    if (excludeSet.Contains(word))
                    {
                        if (result.ExcludedWordCounts.ContainsKey(word))
                            result.ExcludedWordCounts[word]++;
                        else
                            result.ExcludedWordCounts[word] = 1;
                    }
                    else
                    {
                        if (result.WordCounts.ContainsKey(word))
                            result.WordCounts[word]++;
                        else
                            result.WordCounts[word] = 1;
                    }
                }
            }

            return result;
        }
    }

}

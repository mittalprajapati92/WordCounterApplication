
namespace WordCounterApplication.Models
{
  
    public class WordCountResult
    {
        public Dictionary<string, int> WordCounts { get; set; } = new();
        public Dictionary<string, int> ExcludedWordCounts { get; set; } = new();
    }
}

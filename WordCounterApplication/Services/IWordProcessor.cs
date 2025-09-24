
using WordCounterApplication.Models;

namespace WordCounterApplication.Services
{
    
    public interface IWordProcessor
    {
        WordCountResult ProcessWords(IEnumerable<string> lines, IEnumerable<string> excludeWords);
    }
}

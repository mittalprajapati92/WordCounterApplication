
using WordCounterApplication.Models;

namespace WordCounterApplication.Services
{
    
    public interface IFileWriter
    {
        Task WriteWordCountsAsync(WordCountResult result, string outputDirectory);
    }
}

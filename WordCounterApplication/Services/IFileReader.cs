
namespace WordCounterApplication.Services
{
   
    public interface IFileReader
    {
        Task<IEnumerable<string>> ReadFilesAsync(string directoryPath);
        Task<IEnumerable<string>> ReadExcludeWordsAsync(string excludeFilePath);
    }
}

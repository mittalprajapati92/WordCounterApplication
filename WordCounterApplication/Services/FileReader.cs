
using System.Text.RegularExpressions;

namespace WordCounterApplication.Services
{
    public class FileReader : IFileReader
    {
        public async Task<IEnumerable<string>> ReadFilesAsync(string directoryPath)
        {
            // Get all .txt files except exclude.txt
            var files = Directory.GetFiles(directoryPath, "*.txt")
                     .Where(f => !string.Equals(Path.GetFileName(f), "exclude.txt", StringComparison.OrdinalIgnoreCase));

            string[] allLines = await Task.WhenAll(files.Select(file => File.ReadAllTextAsync(file)));

            return allLines;
        }

        public async Task<IEnumerable<string>> ReadExcludeWordsAsync(string excludeFilePath)
        {
            if (!File.Exists(excludeFilePath))
                throw new FileNotFoundException("Exclude file not found please add it", excludeFilePath);

            // Split by comma, new line and normalize to uppercase
            return Regex.Split(await File.ReadAllTextAsync(excludeFilePath), @"[,\r\n]+")
                                         .Select(word => word.Trim().ToUpperInvariant()) 
                                         .Where(word => !string.IsNullOrWhiteSpace(word))
                                         .Distinct()
                                         .ToList();
        }
    }


}

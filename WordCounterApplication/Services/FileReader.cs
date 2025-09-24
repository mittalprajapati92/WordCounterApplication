
namespace WordCounterApplication.Services
{
    public class FileReader : IFileReader
    {
        public async Task<IEnumerable<string>> ReadFilesAsync(string directoryPath)
        {
            var allLines = new List<string>();

            // Get all .txt files except exclude.txt
            var files = Directory.GetFiles(directoryPath, "*.txt")
                     .Where(f => !string.Equals(Path.GetFileName(f), "exclude.txt", StringComparison.OrdinalIgnoreCase));


            foreach (var file in files)
            {
                var lines = await File.ReadAllLinesAsync(file);
                allLines.AddRange(lines);
            }

            return allLines;
        }

        public async Task<IEnumerable<string>> ReadExcludeWordsAsync(string excludeFilePath)
        {
            if (!File.Exists(excludeFilePath))
                throw new FileNotFoundException("Exclude file not found please add it", excludeFilePath);

            return (await File.ReadAllLinesAsync(excludeFilePath))
                   .Select(w => w.ToUpperInvariant())
                   .ToList();
        }
    }


}

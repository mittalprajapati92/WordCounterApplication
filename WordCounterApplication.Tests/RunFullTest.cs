
using WordCounterApplication.Models;
using WordCounterApplication.Services;

namespace WordCounterApplication.Tests
{
    public class RunFullTest
    {
        private readonly string _inputDir;
        private readonly string _outputDir;

        public RunFullTest()
        {
            string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            _inputDir = Path.Combine(projectFolder, "TestData", "Input");
            _outputDir = Path.Combine(projectFolder, "TestData", "Output");

            if (!Directory.Exists(_inputDir))
                throw new DirectoryNotFoundException($"Input folder not found: {_inputDir}");

            // Clean output folder if it exists
            if (Directory.Exists(_outputDir))
            {
                Directory.Delete(_outputDir, recursive: true);
            }

            Directory.CreateDirectory(_outputDir);
        }

        [Fact]
        public async Task FullFileOperation()
        {
            // Arrange
            var reader = new FileReader();
            var processor = new WordProcessor();
            var writer = new FileWriter();

            // Act
            var lines = await reader.ReadFilesAsync(_inputDir);
            var excludeFilePath = Path.Combine(_inputDir, "exclude.txt");
            var excludeWords = File.Exists(excludeFilePath) ? await reader.ReadExcludeWordsAsync(excludeFilePath) : new List<string>();


            // Only process if we have lines AND excludeWordsa
            if (lines.Any() && excludeWords.Any())
            {
                var result = processor.ProcessWords(lines, excludeWords);
                await writer.WriteWordCountsAsync(result, _outputDir);

                // Assert EXCLUDED_WORDS.txt if any excluded words
                var excludedPath = Path.Combine(_outputDir, "EXCLUDED_WORDS.txt");
                if (result.ExcludedWordCounts.Any())
                    Assert.True(File.Exists(excludedPath), "EXCLUDED_WORDS.txt should be created as there are excluded words.");

                else
                    Assert.False(File.Exists(excludedPath), "EXCLUDED_WORDS.txt should not be created as there are no excluded words.");


                // Assert letter files
                foreach (var letter in Enumerable.Range('A', 26).Select(c => (char)c))
                {
                    var filePath = Path.Combine(_outputDir, $"FILE_{letter}.txt");
                    if (result.WordCounts.Keys.Any(w => w.StartsWith(letter.ToString(), StringComparison.OrdinalIgnoreCase)))
                        Assert.True(File.Exists(filePath), $"Expected output file for letter '{letter}' is present as words starting with '{letter}' exist.");

                    else
                        Assert.False(File.Exists(filePath), $"No words start with '{letter}', so FILE_{letter}.txt should not exist.");

                }
            }
            else
            {
                // No input lines or exclude words then nothing should be created
                var allFiles = Directory.GetFiles(_outputDir);
                Assert.Empty(allFiles);
            }


        }
    }
}
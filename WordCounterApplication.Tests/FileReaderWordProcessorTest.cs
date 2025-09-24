

using Moq;
using WordCounterApplication.Models;
using WordCounterApplication.Services;

namespace WordCounterApplication.Tests
{
    public class FileReaderWordProcessorTest
    {
        [Fact]
        public async Task ProcessesWriteResult()
        {
            // Arrange
            var mockReader = new Mock<IFileReader>();
            var mockWriter = new Mock<IFileWriter>();
            var processor = new WordProcessor();

            // Setup methods for mock
            SetupMock(mockReader);

            // Act
            var lines = await mockReader.Object.ReadFilesAsync("");
            var excludes = await mockReader.Object.ReadExcludeWordsAsync("");
            var result = processor.ProcessWords(lines, excludes);

            // Send to writer mock
            await mockWriter.Object.WriteWordCountsAsync(result, "");

            // Assert 
            mockWriter.Verify(w => w.WriteWordCountsAsync(
            It.Is<WordCountResult>(r => r.WordCounts.ContainsKey("LOREM") && r.ExcludedWordCounts.ContainsKey("IPSUM")),
            It.IsAny<string>()), Times.Once);

        }

        // Setup mockReader
        private static void SetupMock(Mock<IFileReader> mockReader)
        {
            mockReader.Setup(r => r.ReadFilesAsync(It.IsAny<string>()))
                      .ReturnsAsync(new List<string> { "Lorem ipsum Lorem" });

            mockReader.Setup(r => r.ReadExcludeWordsAsync(It.IsAny<string>()))
                      .ReturnsAsync(new List<string> { "IPSUM" });
        }
    }
}

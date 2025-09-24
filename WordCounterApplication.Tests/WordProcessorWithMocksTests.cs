
using Moq;
using WordCounterApplication.Services;

namespace WordCounterApplication.Tests
{
    public class WordProcessorWithMocksTests
    {
        [Fact]
        public async Task CallProcessWord()
        {
            // Arrange
            var mockReader = new Mock<IFileReader>();
            var processor = new WordProcessor();

            // Setup methods for mock
            SetupMock(mockReader);


            // Act
            var lines = await mockReader.Object.ReadFilesAsync("");
            var excludes = await mockReader.Object.ReadExcludeWordsAsync("");
            var result = processor.ProcessWords(lines, excludes);

            // Assert
            Assert.Equal(1, result.WordCounts["IPSUM"]);
            Assert.True(result.ExcludedWordCounts.ContainsKey("LOREM"));
            Assert.Equal(2, result.ExcludedWordCounts["LOREM"]);
        }

        // Setup mockReader
        private static void SetupMock(Mock<IFileReader> mockReader)
        {
            mockReader.Setup(r => r.ReadFilesAsync(It.IsAny<string>()))
                      .ReturnsAsync(new List<string> { "Lorem ipsum Lorem" });
            mockReader.Setup(r => r.ReadExcludeWordsAsync(It.IsAny<string>()))
                      .ReturnsAsync(new List<string> { "LOREM" });
        }

    }
}
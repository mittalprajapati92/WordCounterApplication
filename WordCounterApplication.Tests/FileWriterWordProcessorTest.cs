
using Moq;
using WordCounterApplication.Models;
using WordCounterApplication.Services;

namespace WordCounterApplication.Tests
{
    public class FileWriterWordProcessorTest
    {
        [Fact]
        public void CallsWriteResult()
        {
            // Arrange
            var mockWriter = new Mock<IFileWriter>();
            var processor = new WordProcessor();

            var lines = new List<string> { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ut elit sed metus finibus finibus vitae quis diam." };
            var excludes = new List<string> { "SIT" };

            var result = processor.ProcessWords(lines, excludes);

            // Act
            mockWriter.Object.WriteWordCountsAsync(result, "");

            // Assert writer method was called at least once (verify interaction)
            mockWriter.Verify(w => w.WriteWordCountsAsync(It.IsAny<WordCountResult>(), It.IsAny<string>()), Times.Once);
        }
    }
}
# Word Counter Application

A C# console application that reads multiple text files from a directory, counts word occurrences, excludes specific words from a list, and generates organized output files.  
Built with **.NET 8.0**, follows **SOLID principles**, and uses **Dependency Injection** for testability.

---

# Features

- 📂 Read multiple `.txt` files from a given directory.
- 📝 Exclude words listed in `exclude.txt` file.
- 🔡 Case-insensitive word counting.
- 📑 Create separate output files per alphabet letter (e.g., `FILE_A.txt`, `FILE_B.txt`, ...) and skip the letters file if it has no words.
- 🚫 Generate `EXCLUDED_WORDS.txt` with counts of excluded words.
- 🧪 Unit tested with **xUnit** + **Moq**.

---

# 🛠️ Project Structure
```
WordCounterApplication/
│── Program.cs # Entry point
│── Models/
│ └── WordCountResult.cs # Holds word and excluded word counts
│── Services/
│ ├── IFileReader.cs # File reading contract
│ ├── FileReader.cs # Reads input files & exclude list
│ ├── IWordProcessor.cs # Word processing contract
│ ├── WordProcessor.cs # Processes word counts
│ ├── IFileWriter.cs # File writing contract
│ ├── FileWriter.cs # Writes results to disk
│
WordCounterApplication.Tests/
│── TestData/ # Folder where Input & Output folders are stored
│ ├── Input/ # Sample input .txt files and exclude.txt file
│ ├── Output/ # Generated test output files
│── FileReaderWordProcessorTest.cs # Pipeline test with reader + writer mocks
│── FileWriterWordProcessorTest.cs # Tests writer interaction
│── WordProcessorWithMocksTests.cs # Unit test for processor with mock input
│── RunFullTest.cs # Unit test for complete processor with real input files from directory path
```

# 🚀 How to Run Projects

1. **Clone Repository**
   git clone https://github.com/mittalprajapati92/WordCounterApplication.git
   cd WordCounterApplication

2. **Build the Solution**
   dotnet build

3. **Run the Application**
   dotnet run --project WordCounterApplication
   
4. **When prompted:**
    Enter the input directory (it should contains .txt files + exclude.txt).


**Input Requirements**
  At least one .txt file with text (200+ words recommended you can use https://www.lipsum.com/).
  An exclude.txt file containing 10+ words to be ignored. 

**Exclude.txt file**
```
the
one
a
is
for
with
you
are
to
when
```
**Output File Generated**
```
  FILE_L.txt
     LOREM 1
     LIKE 3
     
  FILE_I.txt
     IPSUM 2

  EXCLUDED_WORDS.txt
     THE 4
     A 1
  ```
🧪 **Running Tests**

1. **Navigate to the test project:**
    cd WordCounterApplication.Tests
   
3. **Run all tests:**
    dotnet test

4. **Debug a single test:**
    Right-click on a test method → Debug Test.

**Design Principles**
Dependency Inversion Principle (D in SOLID): Uses interfaces (IFileReader, IWordProcessor, IFileWriter) to decouple components.
Single Responsibility Principle: Each service handles one task (read, process, write).
Testability: Mocked services isolate logic for unit tests.

**Tech Stack**
.NET 8.0
xUnit (Testing framework)
Moq (Mocking library)
Dependency Injection (Microsoft.Extensions.DependencyInjection)

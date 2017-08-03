using Microsoft.VisualStudio.TestTools.UnitTesting;
using RateCalculator.Infrastructure;
using Moq;
using CsvHelper;

namespace RateCalculator.Tests.Infrastructure
{
    public class When_working_with_the_rate_file_reader
    {
        protected IRateFileReader _rateFileReader;
        protected Mock<ICsvReader> _csvReader = new Mock<ICsvReader>();
        protected Mock<IFileReader> _fileReader = new Mock<IFileReader>();

        public When_working_with_the_rate_file_reader()
        {
            _rateFileReader = new RateFileReader(_csvReader.Object, _fileReader.Object);
        }
    }

    [TestClass]
    public class And_reading_a_file : When_working_with_the_rate_file_reader
    {
        string _fileName = "file.csv";
       
        
        [TestMethod]
        public void It_throws_exception_if_file_is_not_found()
        {
            
        }
    }
}

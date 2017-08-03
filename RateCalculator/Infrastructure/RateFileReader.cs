using RateCalculator.Models;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.IO;

namespace RateCalculator.Infrastructure
{
    public interface IRateFileReader
    {
        List<AvailableRate> Read(string fileName);
    }
    public class RateFileReader : IRateFileReader
    {
        readonly ICsvReader _csvReader;
        readonly IFileReader _fileReader;
        
        public RateFileReader(ICsvReader csvReader, IFileReader fileReader)
        {
            _csvReader = csvReader;
            _fileReader = fileReader;
        }


        public List<AvailableRate> Read(string fileName)
        {
            List<AvailableRate> rates;
            using (TextReader reader = File.OpenText(fileName))
            {
                var csv = new CsvReader(reader);
                rates = csv.GetRecords<AvailableRate>().ToList();
            }

            return rates;
        }
    }
}

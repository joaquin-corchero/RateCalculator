using CsvHelper;
using RateCalculator.Models;
using System;
using System.IO;
using System.Linq;

namespace RateCalculator.Domain
{
    public interface IRateFileReader
    {
        LoanProviderResult Read(string fileName);
    }

    public class RateFileReader : IRateFileReader
    {
        public LoanProviderResult Read(string fileName)
        {
            LoanProviderResult result = new LoanProviderResult();
            try
            {
                using (TextReader reader = File.OpenText(fileName))
                {
                    var csv = new CsvReader(reader);
                    result.SetRates(csv.GetRecords<LoanProvider>().ToList());
                }
            }
            catch (Exception e)
            {
                result.SetErrorMessage($"There was a problem reading the file {e.Message}");
            }

            return result;
        }
    }
}

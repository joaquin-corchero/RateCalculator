using RateCalculator.Models;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.IO;
using System;

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

            }

            return result;
        }
    }
}

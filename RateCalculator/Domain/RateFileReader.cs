using CsvHelper;
using RateCalculator.Models;
using System;
using System.IO;
using System.Linq;

namespace RateCalculator.Domain
{
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
                    csv.Configuration.HasHeaderRecord = true;
                    var data = csv.GetRecords<LoanProvider>();
                    result.SetLoanPrvider(data.ToList());
                }
            }
            catch (Exception e)
            {
                result.SetErrorMessage($"There was a problem reading the file: {e.Message}");
            }

            return result;
        }
    }
}

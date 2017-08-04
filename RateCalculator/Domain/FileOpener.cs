using CsvHelper;
using RateCalculator.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RateCalculator.Domain
{
    public interface IFileOpener
    {
        bool DoesFileExist(string fileName);

        TextReader ReadContent(string fileName);

        List<LoanProvider> GetLenders(TextReader reader);
    }

    public class FileOpener : IFileOpener
    {
        public bool DoesFileExist(string fileName)
        {
            return File.Exists(fileName);
        }

        public TextReader ReadContent(string fileName)
        {
            return File.OpenText(fileName);
        }

        public List<LoanProvider> GetLenders(TextReader reader)
        {
            var csv = new CsvReader(reader);
            csv.Configuration.HasHeaderRecord = true;
            return csv.GetRecords<LoanProvider>().ToList();
        }
    }
}

﻿using CsvHelper;
using RateCalculator.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RateCalculator.Infrastructure
{
    public interface IFileReader
    {
        bool DoesFileExist(string fileName);

        TextReader GetTextReader(string fileName);

        List<LoanProvider> ReadLoanProviders(TextReader reader);
    }

    public class FileReader : IFileReader
    {
        public bool DoesFileExist(string fileName)
        {
            return File.Exists(fileName);
        }

        public TextReader GetTextReader(string fileName)
        {
            return File.OpenText(fileName);
        }

        public List<LoanProvider> ReadLoanProviders(TextReader reader)
        {
            var csv = new CsvReader(reader);
            csv.Configuration.HasHeaderRecord = true;
            return csv.GetRecords<LoanProvider>().ToList();
        }
    }
}

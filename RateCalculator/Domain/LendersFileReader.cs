using CsvHelper;
using RateCalculator.Models;
using System;
using System.IO;
using System.Linq;

namespace RateCalculator.Domain
{
    public class LendersFileReader : ILendersFileReader
    {
        public const string FILE_DOES_NOT_EXIST = "The file does not exist";
        public const string EXCEPTION_HAPPENED = "There was a problem reading the file: ";
        public const string WRONG_FORMAT_OR_EMPTY = "Make sure the file is comma separated and has 3 columns (Lender, Rate, Available)";

        private readonly IFileOpener _fileOpener;
        public LendersFileReader(IFileOpener fileOpener)
        {
            this._fileOpener = fileOpener;
        }

        public LendersFileReader() : this(new FileOpener()) { }
        

        public LenderReaderResult Read(string fileName)
        {
            LenderReaderResult result = new LenderReaderResult();
            if(!_fileOpener.DoesFileExist(fileName))
            {
                result.SetErrorMessage(FILE_DOES_NOT_EXIST);
                return result;
            }

            TextReader reader = TextReader.Null;
            try
            {
                reader = _fileOpener.ReadContent(fileName);
                result.SetLenders(_fileOpener.GetLenders(reader));
                if(result.Lenders.Count == 0)
                {
                    result.SetErrorMessage(WRONG_FORMAT_OR_EMPTY);
                    return result;
                }
            }
            catch (Exception e)
            {
                result.SetErrorMessage($"{EXCEPTION_HAPPENED} {e.Message}");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }

            return result;
        }
    }
}

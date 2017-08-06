using RateCalculator.Infrastructure;
using RateCalculator.Models;
using System;
using System.IO;

namespace RateCalculator.Handlers
{

    public class LoanProviderHandler : Handler, IHandler
    {
        public const string FILE_DOES_NOT_EXIST = "The file does not exist";
        public const string EXCEPTION_HAPPENED = "There was a problem reading the file: ";
        public const string WRONG_FORMAT_OR_EMPTY = "Make sure the file is comma separated and has 3 columns (Lender, Rate, Available)";

        private readonly IFileReader _fileReader;

        public LoanProviderHandler(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public LoanProviderHandler() : this(new FileReader()) { }

        public override void HandleRequest(QuoteModel quote)
        {
            if (!_fileReader.DoesFileExist(quote.InputModel.FileName))
            {
                quote.SetErrorMessage(FILE_DOES_NOT_EXIST);
                return;
            }

            TextReader reader = TextReader.Null;
            try
            {
                reader = _fileReader.GetTextReader(quote.InputModel.FileName);
                quote.SetLoanProviders(_fileReader.ReadLoanProviders(reader));
                if (quote.LoanProviders.Count == 0)
                {
                    quote.SetErrorMessage(WRONG_FORMAT_OR_EMPTY);
                    return;
                }
            }
            catch (Exception e)
            {
                quote.SetErrorMessage($"{EXCEPTION_HAPPENED} {e.Message}");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }

            if (quote.ValidationResult.IsValid)
            {
                successor.HandleRequest(quote);
            }
        }
    }
}

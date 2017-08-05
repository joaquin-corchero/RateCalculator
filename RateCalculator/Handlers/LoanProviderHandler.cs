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

        private readonly IFileOpener _fileOpener;

        public LoanProviderHandler(IFileOpener fileOpener)
        {
            _fileOpener = fileOpener;
        }

        public LoanProviderHandler() : this(new FileOpener()) { }

        public override void HandleRequest(QuoteModel quote)
        {
            if (!_fileOpener.DoesFileExist(quote.InputModel.FileName))
            {
                quote.SetErrorMessage(FILE_DOES_NOT_EXIST);
                return;
            }

            TextReader reader = TextReader.Null;
            try
            {
                reader = _fileOpener.GetTextReader(quote.InputModel.FileName);
                quote.SetLoanProviders(_fileOpener.ReadLoanProviders(reader));
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

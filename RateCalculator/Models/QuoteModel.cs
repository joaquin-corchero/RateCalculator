using RateCalculator.Validators;
using System.Collections.Generic;
using System;

namespace RateCalculator.Models
{
    public class QuoteModel
    {
        public ValidationResult ValidationResult { get; private set; }

        public string[] Args { get; private set; }

        public InputModel InputModel { get; private set; }

        public List<LoanProvider> LoanProviders { get; private set; }

        public Quote Quote { get; private set; }

        public QuoteModel(string[] args)
        {
            Args = args;
            ValidationResult = ValidationResult.Valid();
        }

        public void SetErrorMessage(string errorMessage)
        {
            ValidationResult = ValidationResult.Invalid(errorMessage);
        }

        public void SetArgs(string[] args)
        {
            Args = args;
        }
    }
}

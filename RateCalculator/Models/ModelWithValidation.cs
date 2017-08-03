using RateCalculator.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCalculator.Models
{
    public abstract class ModelWithValidation
    {
        public ValidationResult ValidationResult { get; private set; }

        public ModelWithValidation()
        {
            ValidationResult = ValidationResult.Valid();
        }

        public void SetErrorMessage(string errorMessage)
        {
            ValidationResult = ValidationResult.Invalid(errorMessage);
        }

    }
}

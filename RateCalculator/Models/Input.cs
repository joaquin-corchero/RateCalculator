using RateCalculator.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCalculator.Models
{
    public class Input : ModelWithValidation
    {
        public string FileName { get; private set; }

        public double LoanAmmount { get; private set; }

        public void SetFileName(string fileName)
        {
            FileName = fileName;
        }

        public void SetLoanAmmount(double loanAmmount)
        {
            LoanAmmount = loanAmmount;
        }
    }
}

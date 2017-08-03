using RateCalculator.Models;
using System;

namespace RateCalculator.Domain
{
    public class CompoundQuoteCalculator : IQuoteCalculator
    {
        private int _years;
        private int _repaymentsPerYear;

        public CompoundQuoteCalculator(int years, int repaymentsPerYear)
        {
            _years = years;
            _repaymentsPerYear = repaymentsPerYear;
        }

        public Quote GetQuote(double loanAmount, double interestRate)
        {
            var a = 1 + (interestRate / _repaymentsPerYear);
            var c = Math.Pow(a, (_years * _repaymentsPerYear));

            var totalAmountToPay = loanAmount * c;

            return Quote.Create(loanAmount, interestRate, totalAmountToPay, totalAmountToPay / (_years * _repaymentsPerYear));
        }
    }
}

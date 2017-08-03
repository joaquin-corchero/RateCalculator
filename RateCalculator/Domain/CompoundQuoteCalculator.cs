using RateCalculator.Models;
using System;

namespace RateCalculator.Domain
{
    public interface IQuoteCalculator
    {
        Quote GetQuote(double loanAmmount, double rate);
    }

    public class CompoundQuoteCalculator : IQuoteCalculator
    {
        private int _years;
        private int _repaymentsPerYear;

        public CompoundQuoteCalculator(int years, int repaymentsPerYear)
        {
            _years = years;
            _repaymentsPerYear = repaymentsPerYear;
        }

        public Quote GetQuote(double loanAmmount, double interestRate)
        {
            var a = 1 + (interestRate / _repaymentsPerYear);
            var c = Math.Pow(a, (_years * _repaymentsPerYear));

            var totalAmmountToPay = loanAmmount * c;

            return Quote.Create(loanAmmount, interestRate, totalAmmountToPay, totalAmmountToPay / (_years * _repaymentsPerYear));
        }
    }
}

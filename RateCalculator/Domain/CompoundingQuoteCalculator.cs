using RateCalculator.Models;
using System;

namespace RateCalculator.Domain
{
    public interface IQuoteCalculator
    {
        Quote GetQuote(double loanAmmount, double rate);
    }

    public class CompoundingQuoteCalculator : IQuoteCalculator
    {
        private int Years;
        private int YearPeriods;

        public CompoundingQuoteCalculator(int years, int yearPeriods)
        {
            Years = years;
            YearPeriods = yearPeriods;
        }

        public Quote GetQuote(double loanAmmount, double rate)
        {
            return new Quote();
        }
    }
}

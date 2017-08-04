using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;
using RateCalculator.Domain;
using System;

namespace RateCalculator.Tests.Domain
{
    public class When_working_with_the_compound_quote_calculator
    {
        protected IQuoteCalculator _quoteCalculator;

        public When_working_with_the_compound_quote_calculator()
        {
            _quoteCalculator = new CompoundQuoteCalculator(3, 12);
        }
    }

    [TestClass]
    public class And_getting_a_compound_quote : When_working_with_the_compound_quote_calculator
    {
        [TestMethod]
        public void The_correct_quote_is_returned()
        {
            var rate = 0.07;
            var loanAmount = 1000;
            var quote = _quoteCalculator.Calculate(loanAmount, rate);

            quote.Rate.ShouldEqual(rate);
            quote.RequestedAmount.ShouldEqual(loanAmount);
            Math.Round(quote.MonthlyRepayment, 2).ShouldEqual(34.25);
            Math.Round(quote.TotalRepayment, 2).ShouldEqual(1232.93);
        }
    }
}

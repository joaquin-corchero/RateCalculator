using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;
using RateCalculator.Domain;
using RateCalculator.Handlers;
using RateCalculator.Models;
using System;

namespace RateCalculator.Tests.Handlers
{
    public class When_working_with_the_compound_calculator_handler
    {
        protected IHandler _sut;

        public When_working_with_the_compound_calculator_handler()
        {
            _sut = new CompoundCalculatorHandler();
        }
    }

    [TestClass]
    public class And_Handling_the_calculation_request : When_working_with_the_compound_calculator_handler
    {
        QuoteModel _quote;

        [TestInitialize]
        public void Init()
        {
            _quote = QuoteModel.Create(null);
        }

        void Execute()
        {
            _sut.HandleRequest(_quote);
        }

        [TestMethod]
        public void The_correct_quote_is_returned()
        {
            _quote.SetMinimumAvailableRate(0.07);
            _quote.InputModel.SetLoanAmount(1000);

            Execute();

            _quote.Quote.Rate.ShouldEqual(_quote.MinimumLendingRateAvailable);
            _quote.Quote.RequestedAmount.ShouldEqual(_quote.InputModel.LoanAmount);
            Math.Round(_quote.Quote.MonthlyRepayment, 2).ShouldEqual(34.25);
            Math.Round(_quote.Quote.TotalRepayment, 2).ShouldEqual(1232.93);
        }

    }
}

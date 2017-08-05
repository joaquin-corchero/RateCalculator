using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;
using RateCalculator.Infrastructure;
using RateCalculator.Models;
using System.Collections.Generic;

namespace RateCalculator.Tests.Infrastructure
{

    public class When_working_qith_the_console_formatter
    {
        protected IOutputFormatter _sut;

        public When_working_qith_the_console_formatter()
        {
            _sut = new ConsoleFormatter();
        }
    }

    [TestClass]
    public class And_generating_output : When_working_qith_the_console_formatter
    {
        List<string> _result;
        QuoteModel _quote;

        [TestInitialize]
        public void Init()
        {
            _quote = QuoteModel.Create(null);
            _quote.SetQuote(Quote.Create(1000, 0.07D, 4444.77777, 33.55556));
        }

        void Execute()
        {
            _result = _sut.GenerateOutput(_quote);
        }

        [TestMethod]
        public void Then_if_the_quote_has_an_error_the_error_is_displayed()
        {
            _quote.SetErrorMessage("something wrong");

            Execute();

            _result[0].ShouldEqual("something wrong");

            _result.Count.ShouldEqual(1);
        }

        [TestMethod]
        public void Then_the_requested_amount_is_displayed_with_no_decimals()
        {
            Execute();

            _result[0].ShouldEqual("Requested amount: £1000");
        }

        [TestMethod]
        public void Then_the_rate_is_displayed_with_one_decimals()
        {
            Execute();

            _result[1].ShouldEqual("Rate: 7.0%");
        }

        [TestMethod]
        public void Then_the_monthly_repayment_is_displayed_with_two_decimals()
        {
            Execute();

            _result[2].ShouldEqual("Monthly repayment: £33.56");
        }

        [TestMethod]
        public void Then_the_total_repayment_is_displayed_with_two_decimals()
        {
            Execute();

            _result[3].ShouldEqual("Total repayment: £4444.78");
        }
    }
}

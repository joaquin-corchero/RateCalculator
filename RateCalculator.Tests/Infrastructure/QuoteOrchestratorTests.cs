using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculator.Infrastructure;
using RateCalculator.Models;
using RateCalculator.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBehave.Spec.MSTest;

namespace RateCalculator.Tests.Infrastructure
{
    public class When_working_with_the_quoter
    {
        protected IQuoteOrchestrator _quoter;
        protected Mock<IInputValidator> _inputValidator = new Mock<IInputValidator>();

        public When_working_with_the_quoter()
        {
            _quoter = new QuoteOrchestrator(
                _inputValidator.Object
                );
        }
    }

    [TestClass]
    public class And_getting_a_quote : When_working_with_the_quoter
    {
        string[] _args = new string[] { "file.csv", "1000" };
        QuoteResponse _result;


        void Execute()
        {
            _result = _quoter.GetQuote(_args);
        }

        [TestMethod]
        public void It_returns_empty_quote_if_there_are_input_errors()
        {
            var validationErrors = ValidationResult.Invalid("Anything");
            _inputValidator.Setup(v => v.Validate(_args)).Returns(validationErrors);

            Execute();

            _result.ValidationResult.ShouldEqual(validationErrors);
            _result.Quote.ShouldBeNull();            
        }

        [TestMethod]
        public void It_returns_empty_quote_if_the_file_does_not_exist()
        {

        }
    }
}

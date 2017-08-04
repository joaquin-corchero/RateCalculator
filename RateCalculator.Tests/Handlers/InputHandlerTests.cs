using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculator.Domain;
using NBehave.Spec.MSTest;
using RateCalculator.Handlers;
using RateCalculator.Models;

namespace RateCalculator.Tests.Handlers
{
    public class When_working_with_the_input_handler
    {
        protected IHandler _sut;

        public When_working_with_the_input_handler()
        {
            _sut = new InputHandler();
        }
    }

    [TestClass]
    public class And_Handling_the_request : When_working_with_the_input_handler
    {
        QuoteModel _quote;

        public object ImnputHandler { get; private set; }

        [TestInitialize]
        public void Init()
        {
            _quote.SetArgs(new string[] { "file.csv", "1000" });
        }

        void Execute()
        {
            _sut.HandleRequest(_quote);
        }

        [TestMethod]
        public void Then_if_less_than_2_arguments_are_in_the_quote_is_set_as_invalid()
        {
            _quote.SetArgs(new string[] { "file.csv"});

            Execute();

            _quote.ValidationResult.IsValid.ShouldBeFalse();
            _quote.ValidationResult.ErrorMessage.ShouldEqual(InputHandler.INVALID_PARAMETERS);
        }
    }
}

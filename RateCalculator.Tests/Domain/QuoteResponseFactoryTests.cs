using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBehave.Spec.MSTest;
using RateCalculator.Domain;
using RateCalculator.Models;

namespace RateCalculator.Tests.Domain
{
    public class When_working_with_the_quoter
    {
        protected IQuoteResponseFactory _quoter;
        protected Mock<IInputprocessor> _inputProcessor = new Mock<IInputprocessor>();
        protected Mock<IRateFileReader> _rateFileReader = new Mock<IRateFileReader>();
        protected Mock<ILenderSelector> _lenderSelector = new Mock<ILenderSelector>();
        protected Mock<IQuoteCalculator> _quoteCalculator = new Mock<IQuoteCalculator>();

        public When_working_with_the_quoter()
        {
            _quoter = new QuoteResponseFactory(
                _inputProcessor.Object,
                _rateFileReader.Object,
                _lenderSelector.Object,
                _quoteCalculator.Object
            );
        }
    }

    [TestClass]
    public class And_getting_a_quote : When_working_with_the_quoter
    {
        string[] _args;
        QuoteResponse _result;
        Input _input;
        LoanProviderResult _loanProviderResult;

        [TestInitialize]
        public void Init()
        {
            _args = new string[] { "file.csv", "1000" };
            _input = new Input();
            _loanProviderResult = new LoanProviderResult();


            _inputProcessor.Setup(i => i.ProcessInput(_args)).Returns(_input);
            _rateFileReader.Setup(v => v.Read(_args[0])).Returns(_loanProviderResult);
        }

        void Execute()
        {
            _result = _quoter.GetQuote(_args);
        }

        [TestMethod]
        public void It_returns_empty_quote_if_there_are_input_errors()
        {
            _input.SetErrorMessage("An error in the input");

            Execute();

            _result.ValidationResult.ErrorMessage.ShouldEqual(_input.ValidationResult.ErrorMessage);
            _result.Quote.ShouldBeNull();            
        }

        [TestMethod]
        public void It_returns_empty_quote_if_the_file_reader_is_invalid()
        {
            _loanProviderResult.SetErrorMessage("Some error reading the file");
            _input.SetFileName(_args[0]);

            Execute();

            _result.ValidationResult.ErrorMessage.ShouldEqual(_loanProviderResult.ValidationResult.ErrorMessage);
            _result.Quote.ShouldBeNull();
        }

        [TestMethod]
        public void It_returns_empty_quote_if_no_lender_can_be_found()
        {
            _lenderSelector.Setup(l => l.ChooseLender(_loanProviderResult.LoanProviders, _input.LoanAmount))
                .Returns(new LoanProvider());
            _input.SetFileName(_args[0]);
            _input.SetLoanAmount(1000);

            Execute();

            _result.ValidationResult.ErrorMessage.ShouldEqual(QuoteResponseFactory.NO_LOAN_PROVIDER_FOUND);
            _result.Quote.ShouldBeNull();
        }

        [TestMethod]
        public void It_returns_a_quote_if_the_lender_is_found()
        {
            _input.SetFileName(_args[0]);
            _input.SetLoanAmount(1000);
            var loanProvider = new LoanProvider { Lender = "Name", Available = 30000, Rate = 0.07D };
            var expectedQuote = Quote.Create(_input.LoanAmount, loanProvider.Rate, 100, 100);

            _lenderSelector.Setup(l => l.ChooseLender(_loanProviderResult.LoanProviders, _input.LoanAmount))
                .Returns(loanProvider);
            _quoteCalculator.Setup(q=> q.GetQuote(_input.LoanAmount, loanProvider.Rate))
                .Returns(expectedQuote);

            Execute();

            _result.ValidationResult.IsValid.ShouldBeTrue();
            _result.Quote.ShouldEqual(expectedQuote);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculator.Domain;
using NBehave.Spec.MSTest;
using RateCalculator.Handlers;
using RateCalculator.Models;

namespace RateCalculator.Tests.Domain
{
    public class When_working_with_the_quote_chain_processor
    {
        protected IQuoteChainProcessor _sut;
        protected Mock<IHandler> _inputHandler;
        protected Mock<IHandler> _loanProviderReaderHandler;
        protected Mock<IHandler> _rateFinderHandler;
        protected Mock<IHandler> _quoteHandler;
        protected Mock<IHandler> _outputHandler;

        public When_working_with_the_quote_chain_processor()
        {
            _inputHandler = new Mock<IHandler>();
            _loanProviderReaderHandler = new Mock<IHandler>();
            _rateFinderHandler = new Mock<IHandler>();
            _quoteHandler = new Mock<IHandler>();
            _outputHandler = new Mock<IHandler>();

            _sut = new QuoteChainProcessor(
                _inputHandler.Object,
                _loanProviderReaderHandler.Object,
                _rateFinderHandler.Object,
                _quoteHandler.Object
            );
        }
    }

    [TestClass]
    public class And_processing_the_arguments : When_working_with_the_quote_chain_processor
    {
        QuoteModel _result;

        void Execute()
        {
            _result = _sut.Process(new string[] { "file.csv", "1000" });
        }

        [TestMethod]
        public void Then_the_chain_of_handlers_is_set()
        {
            Execute();

            _inputHandler.Verify(h => 
                h.SetSuccessor(It.Is<IHandler>(v => v == _loanProviderReaderHandler.Object)),
                Times.Once);
            _loanProviderReaderHandler.Verify(h =>
                h.SetSuccessor(It.Is<IHandler>(v => v == _rateFinderHandler.Object)),
                Times.Once);
            _rateFinderHandler.Verify(h =>
                h.SetSuccessor(It.Is<IHandler>(v => v == _quoteHandler.Object)),
                Times.Once);
        }

        [TestMethod]
        public void Then_the_request_is_handled()
        {
            Execute();

            _inputHandler.Verify(h =>h.HandleRequest(_result), Times.Once);
        }

        [TestMethod]
        public void Then_the_quote_is_returned()
        {
            Execute();

            _result.ShouldBeAssignableFrom<QuoteModel>();
        }
    }
}

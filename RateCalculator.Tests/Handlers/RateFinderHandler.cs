using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBehave.Spec.MSTest;
using RateCalculator.Handlers;
using RateCalculator.Models;
using System.Collections.Generic;

namespace RateCalculator.Tests.Handlers
{
    public class When_working_with_the_rate_finder_handler
    {
        protected IHandler _sut;
        protected Mock<IHandler> _successor;

        public When_working_with_the_rate_finder_handler()
        {
            _successor = new Mock<IHandler>();
            _sut = new RateFinderHandler();
            _sut.SetSuccessor(_successor.Object);
        }
    }

    [TestClass]
    public class And_Handling_the_request_for_the_rate_finder : When_working_with_the_rate_finder_handler
    {
        QuoteModel _quote;

        [TestInitialize]
        public void Initialize()
        {
            _quote = new QuoteModel(null);
            _quote.SetLoanProviders(
                new List<LoanProvider> {
                    new LoanProvider{Lender = "Bob", Rate = 0.075D, Available= 640 },
                    new LoanProvider{Lender = "Jane", Rate = 0.069D, Available= 480},
                    new LoanProvider{Lender = "Fred", Rate = 0.071D, Available= 520},
                    new LoanProvider{Lender = "Mary", Rate = 0.104D, Available= 170},
                    new LoanProvider{Lender = "John", Rate = 0.081D, Available= 320},
                    new LoanProvider{Lender = "Dave", Rate = 0.074D, Available= 140},
                    new LoanProvider{Lender = "Angela", Rate = 0.071D, Available= 60},
                }
            );
        }

        void Execute()
        {
            _sut.HandleRequest(_quote);
        }

        void SuccessorIsNotCalled()
        {
            _successor.Verify(h =>
                          h.HandleRequest(It.Is<QuoteModel>(v => v == _quote)),
                          Times.Never);
        }

        [TestMethod]
        public void Then_returns_null_if_no_rate_is_available_for_the_loan_amount()
        {
            _quote.InputModel.SetLoanAmount(100000000);

            Execute();

            _quote.ValidationResult.IsValid.ShouldBeFalse();
            _quote.ValidationResult.ErrorMessage.ShouldEqual(RateFinderHandler.NO_RATE_AVAILABLE);

            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void Then_for_100_0_069_is_returned()
        {
            _quote.InputModel.SetLoanAmount(100);

            Execute();

            _quote.MinimumLendingRateAvailable.ShouldEqual(0.069D);
            SuccessorIsCalledIfValid();
        }

        [TestMethod]
        public void Then_for_200_0_069_is_returned()
        {
            _quote.InputModel.SetLoanAmount(200);

            Execute();

            _quote.MinimumLendingRateAvailable.ShouldEqual(0.069D);
            SuccessorIsCalledIfValid();
        }

        [TestMethod]
        public void Then_for_300_0_069_is_returned()
        {
            _quote.InputModel.SetLoanAmount(300);

            Execute();

            _quote.MinimumLendingRateAvailable.ShouldEqual(0.069D);
            SuccessorIsCalledIfValid();
        }

        [TestMethod]
        public void Then_for_400_0_069_is_returned()
        {
            _quote.InputModel.SetLoanAmount(400);

            Execute();

            _quote.MinimumLendingRateAvailable.ShouldEqual(0.069D);
            SuccessorIsCalledIfValid();
        }

        [TestMethod]
        public void Then_for_500_0_071_is_returned()
        {
            _quote.InputModel.SetLoanAmount(500);

            Execute();

            _quote.MinimumLendingRateAvailable.ShouldEqual(0.071D);
            SuccessorIsCalledIfValid();
        }

        [TestMethod]
        public void Then_for_600_0_0675is_returned()
        {
            _quote.InputModel.SetLoanAmount(600);

            Execute();

            _quote.MinimumLendingRateAvailable.ShouldEqual(0.075D);
            SuccessorIsCalledIfValid();
        }

        void SuccessorIsCalledIfValid()
        {
            _successor.Verify(h =>
                h.HandleRequest(It.Is<QuoteModel>(v => v == _quote)),
                Times.Once);
        }
    }
}

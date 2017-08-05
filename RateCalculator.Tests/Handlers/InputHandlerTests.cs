using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBehave.Spec.MSTest;
using RateCalculator.Domain;
using RateCalculator.Handlers;
using RateCalculator.Models;

namespace RateCalculator.Tests.Handlers
{
    public class When_working_with_the_input_handler
    {
        protected IHandler _sut;
        protected Mock<IHandler> _successor;
        protected const double _minimumLoan = 1000;
        protected const double _maximumLoan = 15000;
        protected const double _multiplesOf = 100;

        public When_working_with_the_input_handler()
        {
            _successor = new Mock<IHandler>();
            _sut = new InputHandler();
            _sut.SetSuccessor(_successor.Object);
        }
    }

    [TestClass]
    public class And_Handling_the_request : When_working_with_the_input_handler
    {
        QuoteModel _quote;

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
        public void Then_if_less_than_2_arguments_are_in_the_quote_is_set_as_invalid()
        {
            _quote = new QuoteModel(new string[] { "file.csv"});

            Execute();

            _quote.ValidationResult.IsValid.ShouldBeFalse();
            _quote.ValidationResult.ErrorMessage.ShouldEqual(InputHandler.INVALID_PARAMETERS);
            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void Then_if_more_than_2_arguments_are_in_the_quote_is_set_as_invalid()
        {
            _quote = new QuoteModel(new string[] { "file.csv", "1000", "Hello" });

            Execute();

            _quote.ValidationResult.IsValid.ShouldBeFalse();
            _quote.ValidationResult.ErrorMessage.ShouldEqual(InputHandler.INVALID_PARAMETERS);
            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void It_is_invalid_if_the_first_parameter_is_csv_file_name()
        {
            _quote = new QuoteModel(new string[] { "1", "2" });

            Execute();

            _quote.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_CSV);
            _quote.ValidationResult.IsValid.ShouldBeFalse();
            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void It_is_invalid_if_the_second_parameter_is_not_a_number()
        {
            _quote = new QuoteModel(new string[] { "file.csv", "a" });

            Execute();

            _quote.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_LOAN_AMOUNT);
            _quote.ValidationResult.IsValid.ShouldBeFalse();
            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void It_is_invalid_if_second_param_is_smaller_than_minimum_loan()
        {
            _quote = new QuoteModel(new string[] { "file.csv", (_minimumLoan - 1).ToString() });

            Execute();

            _quote.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_LOAN_AMOUNT);
            _quote.ValidationResult.IsValid.ShouldBeFalse();
            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void It_is_invalid_if_second_param_is_greater_than_maximum_loan()
        {
            _quote = new QuoteModel(new string[] { "file.csv", (_maximumLoan + 1).ToString() });

            Execute();

            _quote.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_LOAN_AMOUNT);
            _quote.ValidationResult.IsValid.ShouldBeFalse();
            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void It_is_invalid_if_loan_amount_is_not_multiple()
        {
            _quote = new QuoteModel(new string[] { "file.csv", (_minimumLoan + 1 + _multiplesOf).ToString() });

            Execute();

            _quote.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_LOAN_AMOUNT);
            _quote.ValidationResult.IsValid.ShouldBeFalse();
            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void It_is_valid_if_the_params_are_correct()
        {
            var amount = _minimumLoan + _multiplesOf;
            _quote = new QuoteModel(new string[] { "file.csv", (amount).ToString() });

            Execute();

            _quote.ValidationResult.IsValid.ShouldBeTrue();
            _quote.InputModel.FileName.ShouldEqual("file.csv");
            _quote.InputModel.LoanAmount.ShouldEqual(amount);
        }

        [TestMethod]
        public void It_is_valid_successor_is_called()
        {
            var amount = _minimumLoan + _multiplesOf;
            _quote = new QuoteModel(new string[] { "file.csv", (amount).ToString() });

            Execute();

            _successor.Verify(h =>
                h.HandleRequest(It.Is<QuoteModel>(v=> v == _quote)),
                Times.Once);
        }
    }
}

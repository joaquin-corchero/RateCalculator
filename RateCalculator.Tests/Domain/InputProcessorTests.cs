using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;
using RateCalculator.Domain;
using RateCalculator.Models;

namespace RateCalculator.Tests.Domain
{
    public class When_working_with_the_input_processor
    {
        protected IInputprocessor _inputProcessor;
        protected const double _minimumLoan = 1000;
        protected const double _maximumLoan = 15000;
        protected const double _multiplesOf = 100;

        public When_working_with_the_input_processor()
        {
            _inputProcessor = new InputProcessor(_minimumLoan, _maximumLoan, _multiplesOf);
        }
    }

    [TestClass]
    public class And_validating_the_input : When_working_with_the_input_processor
    {
        Input _actual;
        string[] _args = null;

        void Execute()
        {
            _actual = _inputProcessor.ProcessInput(_args);
        }

        [TestMethod]
        public void It_is_invalid_if_less_than_2_parameters()
        {
            _args = new string[] { };
            Execute();

            _actual.ValidationResult.ErrorMessage.ShouldEqual("Incorrect arguments, please follow the format: ratecalculator.exe market.csv 1500");
            _actual.ValidationResult.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_invalid_if_more_than_2_paramters()
        {
            _args = new string[] { "1", "2", "3" };

            Execute();

            _actual.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_PARAMETERS);
            _actual.ValidationResult.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_invalid_if_the_first_parameter_is_csv_file_name()
        {
            _args = new string[] { "1", "2"};

            Execute();

            _actual.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_CSV);
            _actual.ValidationResult.IsValid.ShouldBeFalse();
        }


        [TestMethod]
        public void It_is_invalid_if_the_second_parameter_is_not_a_number()
        {
            _args = new string[] { "file.csv", "a" };

            Execute();

            _actual.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_LOAN_AMMOUNT);
            _actual.ValidationResult.IsValid.ShouldBeFalse();
        }
        
        [TestMethod]
        public void It_is_invalid_if_second_param_is_smaller_than_minimum_loan()
        {
            _args = new string[] { "file.csv", (_minimumLoan -1).ToString() };

            Execute();

            _actual.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_LOAN_AMMOUNT);
            _actual.ValidationResult.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_invalid_if_second_param_is_greater_than_maximum_loan()
        {
            _args = new string[] { "file.csv", (_maximumLoan + 1).ToString() };

            Execute();

            _actual.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_LOAN_AMMOUNT);
            _actual.ValidationResult.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_invalid_if_loan_ammount_is_not_multiple()
        {
            _args = new string[] { "file.csv", (_minimumLoan + 1 + _multiplesOf).ToString() };

            Execute();

            _actual.ValidationResult.ErrorMessage.ShouldEqual(InputProcessor.INVALID_LOAN_AMMOUNT);
            _actual.ValidationResult.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_valid_if_the_params_are_coorect()
        {
            var ammount = _minimumLoan + _multiplesOf;
            _args = new string[] { "file.csv", (ammount).ToString() };

            Execute();

            _actual.ValidationResult.IsValid.ShouldBeTrue();
            _actual.FileName.ShouldEqual(_args[0]);
            _actual.LoanAmmount.ShouldEqual(ammount);
        }
    }
}

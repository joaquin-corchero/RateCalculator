using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;
using RateCalculator.Validators;

namespace RateCalculator.Tests.Validators
{
    public class When_working_with_the_input_validator
    {
        protected IInputValidator _inputValidator;
        protected const double _minimumLoan = 1000;
        protected const double _maximumLoan = 15000;
        protected const double _multiplesOf = 100;

        public When_working_with_the_input_validator()
        {
            _inputValidator = new InputValidator(_minimumLoan, _maximumLoan, _multiplesOf);
        }
    }

    [TestClass]
    public class And_validating_the_input : When_working_with_the_input_validator
    {
        ValidationResult _actual;
        string[] _args = null;

        void Execute()
        {
            _actual = _inputValidator.Validate(_args);
        }

        [TestMethod]
        public void It_is_invalid_if_less_than_2_parameters()
        {
            _args = new string[] { };
            Execute();

            _actual.Message.ShouldEqual("Incorrect arguments, please follow the format: ratecalculator.exe market.csv 1500");
            _actual.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_invalid_if_more_than_2_paramters()
        {
            _args = new string[] { "1", "2", "3" };

            Execute();

            _actual.Message.ShouldEqual(InputValidator.INVALID_PARAMETERS);
            _actual.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_invalid_if_the_first_parameter_is_csv_file_name()
        {
            _args = new string[] { "1", "2"};

            Execute();

            _actual.Message.ShouldEqual(InputValidator.INVALID_CSV);
            _actual.IsValid.ShouldBeFalse();
        }


        [TestMethod]
        public void It_is_invalid_if_the_second_parameter_is_not_a_number()
        {
            _args = new string[] { "file.csv", "a" };

            Execute();

            _actual.Message.ShouldEqual(InputValidator.INVALID_LOAN_AMMOUNT);
            _actual.IsValid.ShouldBeFalse();
        }
        
        [TestMethod]
        public void It_is_invalid_if_second_param_is_smaller_than_minimum_loan()
        {
            _args = new string[] { "file.csv", (_minimumLoan -1).ToString() };

            Execute();

            _actual.Message.ShouldEqual(InputValidator.INVALID_LOAN_AMMOUNT);
            _actual.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_invalid_if_second_param_is_greater_than_maximum_loan()
        {
            _args = new string[] { "file.csv", (_maximumLoan + 1).ToString() };

            Execute();

            _actual.Message.ShouldEqual(InputValidator.INVALID_LOAN_AMMOUNT);
            _actual.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_invalid_if_loan_ammount_is_not_multiple()
        {
            _args = new string[] { "file.csv", (_minimumLoan + 1 + _multiplesOf).ToString() };

            Execute();

            _actual.Message.ShouldEqual(InputValidator.INVALID_LOAN_AMMOUNT);
            _actual.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_valid_if_the_params_are_coorect()
        {
            _args = new string[] { "file.csv", (_minimumLoan + _multiplesOf).ToString() };

            Execute();

            _actual.IsValid.ShouldBeTrue();
        }
    }
}

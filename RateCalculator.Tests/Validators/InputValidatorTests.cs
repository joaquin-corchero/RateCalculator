using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;
using RateCalculator.Validators;

namespace RateCalculator.Tests.Validators
{
    public class When_working_with_the_input_validator
    {
        protected IInputValidator _inputValidator;

        public When_working_with_the_input_validator()
        {
            _inputValidator = new InputValidator();
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

            _actual.Message.ShouldEqual("Incorrect arguments, please follow the format: ratecalculator.exe market.csv 1500");
            _actual.IsValid.ShouldBeFalse();
        }

        [TestMethod]
        public void It_is_invalid_if_the_first_parameter_is_csv_file_name()
        {
            _args = new string[] { "1", "2"};

            Execute();

            _actual.Message.ShouldEqual("First parameter must be a csv: market.csv");
            _actual.IsValid.ShouldBeFalse();
        }
    }
}

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
        public void It_throws_errors_if_less_than_2_arguments()
        {
            _args = new string[] { };
            Execute();

            _actual.Message.ShouldEqual("Incorrect argumenst, please try something like: ratecalculator.exe market.csv 1500");
            _actual.IsValid.ShouldBeFalse();
        }
    }
}

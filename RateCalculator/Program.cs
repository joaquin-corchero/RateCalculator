using RateCalculator.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCalculator
{
    class Program
    {
        static IInputValidator _inputValidator;

        static void SetupDependencies()
        {
            _inputValidator = new InputValidator();
        }

        static void Main(string[] args)
        {
            SetupDependencies();
            var validationResult = _inputValidator.Validate(args);
            if (!validationResult.IsValid)
            {
                Console.WriteLine(validationResult.Message);
                PressKeyAndWait();
                return;
            }
        }

        static void PressKeyAndWait()
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}

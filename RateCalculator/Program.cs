using RateCalculator.Domain;
using RateCalculator.Validators;
using System;

namespace RateCalculator
{
    class Program
    {
        static IQuoteOrchestrator _quoter;

        static void SetupDependencies()
        {
            _quoter = new QuoteOrchestrator();
        }

        static void Main(string[] args)
        {
            var result = _quoter.GetQuote(args);

            if(!result.ValidationResult.IsValid)
            {
                Console.WriteLine(result.ValidationResult.ErrorMessage);
            }
            else
            {
                Console.WriteLine($"Requested amount: {result.Quote.RequestedAmmount} ");
                Console.WriteLine($"Rate: {result.Quote.Rate} ");
                Console.WriteLine($"Monthly repayment: {result.Quote.MonthlyRepayment} ");
                Console.WriteLine($"Total repayment: {result.Quote.TotalRepayment} ");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

            /*
            static IInputValidator _inputValidator;

            static void SetupDependencies()
            {
                _inputValidator = new InputValidator(1000, 15000, 100);
            }

            static IOrchestrator 

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
            }*/
        }
}

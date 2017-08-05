using RateCalculator.Domain;
using System;
using static RateCalculator.ConsoleFormatter;

namespace RateCalculator
{
    class Program
    {
        static IQuoteChainProcessor _quoter;
        static IOutputFormatter _outputFormatter;

        static void SetupDependencies()
        {
            _quoter = new QuoteChainProcessor();
            _outputFormatter = new ConsoleFormatter();
        }

        static void Main(string[] args)
        {
            SetupDependencies();

            var quote = _quoter.Process(args);
            _outputFormatter.GenerateOutput(quote);



            if (!quote.ValidationResult.IsValid)
            {
                Console.WriteLine(quote.ValidationResult.ErrorMessage);
            }
            else
            {
                Console.WriteLine($"Requested amount: £{quote.Quote.RequestedAmount} ");
                Console.WriteLine($"Rate: {Math.Round(quote.Quote.Rate * 100, 1)}%");
                Console.WriteLine($"Monthly repayment: £{Math.Round(quote.Quote.MonthlyRepayment, 2)} ");
                Console.WriteLine($"Total repayment: £{Math.Round(quote.Quote.TotalRepayment, 2)} ");
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

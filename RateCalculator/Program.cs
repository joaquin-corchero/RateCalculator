using RateCalculator.Domain;
using RateCalculator.Infrastructure;
using System;

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
            var output = _outputFormatter.GenerateOutput(quote);

            foreach (var msg in output)
                Console.WriteLine(msg);

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

        }
    }
}
using RateCalculator.Models;

namespace RateCalculator.Domain
{
    public interface IRateFileReader
    {
        LoanProviderResult Read(string fileName);
    }
}

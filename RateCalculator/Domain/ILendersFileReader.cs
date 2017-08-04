using RateCalculator.Models;

namespace RateCalculator.Domain
{
    public interface ILendersFileReader
    {
        LenderReaderResult Read(string fileName);
    }
}

using RateCalculator.Models;

namespace RateCalculator.Domain
{
    public interface IInputprocessor
    {
        Input ProcessInput(string[] args);
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculator.Domain;
using NBehave.Spec.MSTest;
using RateCalculator.Handlers;

namespace RateCalculator.Tests.Handlers
{
    public class When_working_with_the_input_handler
    {
        protected IHandler _sut;

        public When_working_with_the_input_handler()
        {
            _sut = new InputHandler();
        }
    }
}

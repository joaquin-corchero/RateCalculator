using RateCalculator.Domain;
using RateCalculator.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

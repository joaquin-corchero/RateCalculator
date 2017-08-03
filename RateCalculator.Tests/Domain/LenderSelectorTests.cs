using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;
using RateCalculator.Domain;
using RateCalculator.Models;
using System.Collections.Generic;

namespace RateCalculator.Tests.Domain
{
    public class When_working_with_the_lender_selector
    {
        protected ILenderSelector _lenderSelector;

        public When_working_with_the_lender_selector()
        {
            _lenderSelector = new LenderSelector();
        }
    }

    [TestClass]
    public class And_choosing_a_lender : When_working_with_the_lender_selector
    {
        List<LoanProvider> _loanProviders;
        LoanProvider _actual;
        double _loanAmount;

        [TestInitialize]
        public void Init()
        {
            _loanProviders = new List<LoanProvider> {
                new LoanProvider{Lender = "Bob", Rate = 0.075D, Available= 640 },
                new LoanProvider{Lender = "Jane", Rate = 0.069D, Available= 480},
                new LoanProvider{Lender = "Fred", Rate = 0.071D, Available= 520},
                new LoanProvider{Lender = "Mary", Rate = 0.104D, Available= 170},
                new LoanProvider{Lender = "John", Rate = 0.081D, Available= 320},
                new LoanProvider{Lender = "Dave", Rate = 0.074D, Available= 140},
                new LoanProvider{Lender = "Angela", Rate = 0.071D, Available= 60},
            };
        }

        void Execute()
        {
            _actual = _lenderSelector.ChooseLender(_loanProviders, _loanAmount);
        }
 
        [TestMethod]
        public void Then_returns_null_if_no_rate_is_available_for_the_loan_amount()
        {
            _loanAmount = 100000000;

            Execute();

            _actual.ShouldBeNull();
        }

        [TestMethod]
        public void Then_for_100_0_069_is_returned()
        {
            _loanAmount = 100;

            Execute();

            _actual.Rate.ShouldEqual(0.069D);
        }

        [TestMethod]
        public void Then_for_200_0_069_is_returned()
        {
            _loanAmount = 200;

            Execute();

            _actual.Rate.ShouldEqual(0.069D);
        }

        [TestMethod]
        public void Then_for_300_0_069_is_returned()
        {
            _loanAmount = 300;

            Execute();

            _actual.Rate.ShouldEqual(0.069D);
        }

        [TestMethod]
        public void Then_for_400_0_069_is_returned()
        {
            _loanAmount = 400;

            Execute();

            _actual.Rate.ShouldEqual(0.069D);
        }

        [TestMethod]
        public void Then_for_500_0_071_is_returned()
        {
            _loanAmount = 500;

            Execute();

            _actual.Rate.ShouldEqual(0.071D);
        }

        [TestMethod]
        public void Then_for_600_0_0675is_returned()
        {
            _loanAmount = 600;

            Execute();

            _actual.Rate.ShouldEqual(0.075D);
        }
    }
}
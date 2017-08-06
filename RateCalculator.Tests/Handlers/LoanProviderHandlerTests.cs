using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBehave.Spec.MSTest;
using RateCalculator.Handlers;
using RateCalculator.Infrastructure;
using RateCalculator.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace RateCalculator.Tests.Handlers
{
    public class When_working_with_the_loan_provider_handler
    {
        protected IHandler _sut;
        protected Mock<IFileReader> _fileReader;
        protected Mock<IHandler> _successor;
        protected const double _minimumLoan = 1000;
        protected const double _maximumLoan = 15000;
        protected const double _multiplesOf = 100;

        public When_working_with_the_loan_provider_handler()
        {
            _successor = new Mock<IHandler>();
            _fileReader = new Mock<IFileReader>();
            _sut = new LoanProviderHandler(_fileReader.Object);
            _sut.SetSuccessor(_successor.Object);
        }
    }

    [TestClass]
    public class And_Handling_the_loan_provider_request : When_working_with_the_loan_provider_handler
    {
        QuoteModel _quote;
        List<LoanProvider> _lenders;

        [TestInitialize]
        public void Init()
        {
            _quote = QuoteModel.Create(null);
            _quote.InputModel.SetFileName("file.csv");
        }

        void Execute()
        {
            _sut.HandleRequest(_quote);
            
        }

        void SuccessorIsNotCalled()
        {
            _successor.Verify(h =>
                          h.HandleRequest(It.Is<QuoteModel>(v => v == _quote)),
                          Times.Never);
        }

        [TestMethod]
        public void The_result_is_invalid_if_file_does_not_exist()
        {
            _fileReader.Setup(f => f.DoesFileExist(_quote.InputModel.FileName)).Returns(false);

            Execute();

            _quote.ValidationResult.IsValid.ShouldBeFalse();
            _quote.ValidationResult.ErrorMessage.ShouldEqual(LoanProviderHandler.FILE_DOES_NOT_EXIST);
            _quote.LoanProviders.ShouldBeEmpty();
            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void The_result_is_invalid_if_no_lenders_are_obtained()
        {
            _fileReader.Setup(f => f.DoesFileExist(_quote.InputModel.FileName)).Returns(true);
            var textReader = new Mock<TextReader>();
            textReader.Setup(tr => tr.ReadLine()).Returns("50,100");
            _fileReader.Setup(f => f.GetTextReader(_quote.InputModel.FileName)).Returns(textReader.Object);
            _fileReader.Setup(f => f.ReadLoanProviders(textReader.Object)).Returns(new List<LoanProvider>());

            Execute();

            _quote.ValidationResult.IsValid.ShouldBeFalse();
            _quote.ValidationResult.ErrorMessage.ShouldEqual(LoanProviderHandler.WRONG_FORMAT_OR_EMPTY);
            _quote.LoanProviders.ShouldBeEmpty();
            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void The_result_is_invalid_if_exception_happens()
        {
            var textReader = new Mock<TextReader>();
            textReader.Setup(tr => tr.ReadLine()).Returns("50,100");
            _lenders = new List<LoanProvider>{
                new LoanProvider { Lender = "Name", Available = 10, Rate = 0.09D },
                new LoanProvider { Lender = "Name", Available = 10, Rate= 0.09D }
            };
            _fileReader.Setup(f => f.DoesFileExist(_quote.InputModel.FileName)).Returns(true);
            _fileReader.Setup(f => f.GetTextReader(_quote.InputModel.FileName)).Returns(textReader.Object);
            _fileReader.Setup(f => f.ReadLoanProviders(textReader.Object)).Throws(new Exception("something wrong"));

            Execute();

            _quote.ValidationResult.IsValid.ShouldBeFalse();
            _quote.ValidationResult.ErrorMessage.ShouldEqual($"{LoanProviderHandler.EXCEPTION_HAPPENED} something wrong");
            _quote.LoanProviders.ShouldBeEmpty();
            SuccessorIsNotCalled();
        }

        [TestMethod]
        public void The_result_is_valid_if_lenders_are_obtained()
        {
            SetTestForSuccess();

            Execute();

            _quote.ValidationResult.IsValid.ShouldBeTrue();
            _quote.ValidationResult.ErrorMessage.ShouldBeEmpty();
            _quote.LoanProviders.ShouldEqual(_lenders);
        }

        void SetTestForSuccess()
        {
            var textReader = new Mock<TextReader>();
            textReader.Setup(tr => tr.ReadLine()).Returns("50,100");
            _lenders = new List<LoanProvider>{
                new LoanProvider { Lender = "Name", Available = 10, Rate = 0.09D },
                new LoanProvider { Lender = "Name", Available = 10, Rate= 0.09D }
            };
            _fileReader.Setup(f => f.DoesFileExist(_quote.InputModel.FileName)).Returns(true);
            _fileReader.Setup(f => f.GetTextReader(_quote.InputModel.FileName)).Returns(textReader.Object);
            _fileReader.Setup(f => f.ReadLoanProviders(textReader.Object)).Returns(_lenders);
        }

        [TestMethod]
        public void It_is_valid_successor_is_called()
        {
            SetTestForSuccess();

            Execute();

            _successor.Verify(h =>
                h.HandleRequest(It.Is<QuoteModel>(v=> v == _quote)),
                Times.Once);
        }
    }
}

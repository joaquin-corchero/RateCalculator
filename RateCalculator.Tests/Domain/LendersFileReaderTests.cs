using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculator.Domain;
using RateCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBehave.Spec.MSTest;
using System.IO;

namespace RateCalculator.Tests.Domain
{
    public class When_working_with_the_lenders_file_reader
    {
        protected ILendersFileReader _lendersFileReader;
        protected Mock<IFileOpener> _fileOpener;

        public When_working_with_the_lenders_file_reader()
        {
            _fileOpener = new Mock<IFileOpener>();
            _lendersFileReader = new LendersFileReader(_fileOpener.Object);
        }
    }

    [TestClass]
    public class And_reading_the_file : When_working_with_the_lenders_file_reader
    {
        LenderReaderResult _lenderReaderResult;
        string _fileName = "A_file.csv";

        void Execute()
        {
            _lenderReaderResult = _lendersFileReader.Read(_fileName);
        }

        [TestMethod]
        public void The_result_is_invalid_if_file_does_not_exist()
        {
            _fileOpener.Setup(f => f.DoesFileExist(_fileName)).Returns(false);

            Execute();

            _lenderReaderResult.ValidationResult.IsValid.ShouldBeFalse();
            _lenderReaderResult.ValidationResult.ErrorMessage.ShouldEqual(LendersFileReader.FILE_DOES_NOT_EXIST);
            _lenderReaderResult.Lenders.ShouldBeEmpty();
        }

        [TestMethod]
        public void The_result_is_invalid_if_no_lenders_are_obtained()
        {
            _fileOpener.Setup(f => f.DoesFileExist(_fileName)).Returns(true);
            var textReader = new  Mock<TextReader>();
            textReader.Setup(tr => tr.ReadLine()).Returns("50,100");
            _fileOpener.Setup(f => f.ReadContent(_fileName)).Returns(textReader.Object);
            _fileOpener.Setup(f=> f.GetLenders(textReader.Object)).Returns(new List<LoanProvider>());

            Execute();

            _lenderReaderResult.ValidationResult.IsValid.ShouldBeFalse();
            _lenderReaderResult.ValidationResult.ErrorMessage.ShouldEqual(LendersFileReader.WRONG_FORMAT_OR_EMPTY);
            _lenderReaderResult.Lenders.ShouldBeEmpty();
        }

        [TestMethod]
        public void The_result_is_invalid_if_exception_happens()
        {
            var textReader = new Mock<TextReader>();
            textReader.Setup(tr => tr.ReadLine()).Returns("50,100");
            var lenders = new List<LoanProvider>{
                new LoanProvider { Lender = "Name", Available = 10, Rate = 0.09D },
                new LoanProvider { Lender = "Name", Available = 10, Rate= 0.09D }
            };
            _fileOpener.Setup(f => f.DoesFileExist(_fileName)).Returns(true);
            _fileOpener.Setup(f => f.ReadContent(_fileName)).Returns(textReader.Object);
            _fileOpener.Setup(f => f.GetLenders(textReader.Object)).Throws(new Exception("something wrong"));

            Execute();

            _lenderReaderResult.ValidationResult.IsValid.ShouldBeFalse();
            _lenderReaderResult.ValidationResult.ErrorMessage.ShouldEqual($"{LendersFileReader.EXCEPTION_HAPPENED} something wrong");
            _lenderReaderResult.Lenders.ShouldBeEmpty();
        }

        [TestMethod]
        public void The_result_is_valid_if_lenders_are_obtained()
        {
            var textReader = new Mock<TextReader>();
            textReader.Setup(tr => tr.ReadLine()).Returns("50,100");
            var lenders = new List<LoanProvider>{
                new LoanProvider { Lender = "Name", Available = 10, Rate = 0.09D },
                new LoanProvider { Lender = "Name", Available = 10, Rate= 0.09D }
            };
            _fileOpener.Setup(f => f.DoesFileExist(_fileName)).Returns(true);
            _fileOpener.Setup(f => f.ReadContent(_fileName)).Returns(textReader.Object);
            _fileOpener.Setup(f => f.GetLenders(textReader.Object)).Returns(lenders);

            Execute();

            _lenderReaderResult.ValidationResult.IsValid.ShouldBeTrue();
            _lenderReaderResult.ValidationResult.ErrorMessage.ShouldBeEmpty();
            _lenderReaderResult.Lenders.ShouldEqual(lenders);
        }
    }
}
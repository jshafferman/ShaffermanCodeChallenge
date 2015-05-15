using Contracts;
using Data;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class SimplePersonValidatorTests
    {
        private SimplePersonValidator sut;
        private ILogger mockLogger;

        [SetUp]
        public void Setup()
        {
            mockLogger = Substitute.For<ILogger>();

            sut = new SimplePersonValidator(mockLogger);
        }

        [Test]
        public void WhenPersonDataIsMadeUpOfBothIntegerValuesIsValidReturnsTrue()
        {
            // Arrange
            string[] personData = { "1977", " 1990" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenPersonDataIsMadeUpOfAnIntegerValueAndNothingIsValidReturnsTrue()
        {
            // Arrange
            string[] personData = { "1977", "" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenPersonDataIsAboveTwoElementsIsValidReturnsFalse()
        {
            // Arrange
            string[] personData = { "1977", "", "" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            mockLogger.Received(1).LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenPersonDataIsLessThanTwoElementsIsValidReturnsFalse()
        {
            // Arrange
            string[] personData = { "1977" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            mockLogger.Received(1).LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenPersonDataFirstElementIsNullIsValidReturnsFalse()
        {
            // Arrange
            string[] personData = { "", " 1990" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            mockLogger.Received(1).LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void WhenPersonDataFirstElementIsNotANumberIsValidReturnsFalse()
        {
            // Arrange
            string[] personData = { "Hello", " 1990" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            mockLogger.Received(1).LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenPersonDataSecondElementIsNotANumberIsValidReturnsFalse()
        {
            // Arrange
            string[] personData = { "1990", " Hello" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            mockLogger.Received(1).LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenPersonDataFirstElementIsBelowThresholdIsValidReturnsFalse()
        {
            // Arrange
            string[] personData = { "1899", "" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            mockLogger.Received(1).LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenPersonDataFirstElementIsEqualToMinThresholdIsValidReturnsTrue()
        {
            // Arrange
            string[] personData = { "1900", "" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenPersonDataFirstElementIsEqualToMaxThresholdIsValidReturnsTrue()
        {
            // Arrange
            string[] personData = { "2000", "" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenPersonDataFirstElementIsAboveThresholdIsValidReturnsFalse()
        {
            // Arrange
            string[] personData = { "2001", "" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            mockLogger.Received(1).LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenPersonDataSecondElementIsBelowThresholdIsValidReturnsFalse()
        {
            // Arrange
            string[] personData = { "1900", "1899" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            mockLogger.Received(1).LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenPersonDataSecondElementIsEqualToMinThresholdIsValidReturnsTrue()
        {
            // Arrange
            string[] personData = { "1900", "1900" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenPersonDataSecondElementIsEqualToMaxThresholdIsValidReturnsTrue()
        {
            // Arrange
            string[] personData = { "2000", "2000" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenPersonDataSecondElementIsAboveThresholdIsValidReturnsFalse()
        {
            // Arrange
            string[] personData = { "2000", "2001" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            mockLogger.Received(1).LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenPersonDataSecondElementIsBelowFirstElementIsValidReturnsFalse()
        {
            // Arrange
            string[] personData = { "2000", "1999" };

            // Act
            bool actual = sut.IsValid(personData);

            // Assert
            mockLogger.Received(1).LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.IsFalse(actual);
        }
    }
}

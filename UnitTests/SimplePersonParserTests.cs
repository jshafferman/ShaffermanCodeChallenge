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
    public class SimplePersonParserTests
    {
        private SimplePersonParser sut;
        private IPersonValidator mockValidator;
        private IPersonMapper mockMapper;
        private string[] validBirthData;

        [SetUp]
        public void Setup()
        {
            mockValidator = Substitute.For<IPersonValidator>();
            mockMapper = Substitute.For<IPersonMapper>();

            sut = new SimplePersonParser(mockValidator, mockMapper);

            validBirthData = new string[] { "1977, 1990" };
        }

        [Test]
        public void WhenFieldsIsValidDataTheListContainsAPerson()
        {
            // Arrange
            mockValidator.IsValid(Arg.Any<string[]>()).Returns(true);
            mockMapper.Map(Arg.Any<string[]>()).Returns(new Person() { BirthYear = 1977, DeathYear = 1990 });

            // Act
            IEnumerable<Person> actualPeople = sut.Parse(validBirthData);

            // Assert
            Assert.AreEqual(1, actualPeople.Count());
        }

        [Test]
        public void WhenFieldsIsValidDataTheListContainsAPersonWithCorrectBirthYearAndDeathYear()
        {
            // Arrange
            mockValidator.IsValid(Arg.Any<string[]>()).Returns(true);
            mockMapper.Map(Arg.Any<string[]>()).Returns(new Person() { BirthYear = 1977, DeathYear = 1990 });

            // Act
            IEnumerable<Person> actualPeople = sut.Parse(validBirthData);
            int actualBirthYear = actualPeople.ElementAt(0).BirthYear;
            int? actualDeathYear = actualPeople.ElementAt(0).DeathYear;

            // Assert
            Assert.AreEqual(1977, actualBirthYear);
            Assert.AreEqual(1990, actualDeathYear);
        }

        [Test]
        public void WhenFieldsIsNotValidListOfPeoleIsZero()
        {
            // Arrange
            mockValidator.IsValid(Arg.Any<string[]>()).Returns(false);
            mockMapper.DidNotReceive().Map(Arg.Any<string[]>());

            // Act
            IEnumerable<Person> actualPeople = sut.Parse(validBirthData);

            // Assert
            Assert.AreEqual(0, actualPeople.Count());
        }
    }
}

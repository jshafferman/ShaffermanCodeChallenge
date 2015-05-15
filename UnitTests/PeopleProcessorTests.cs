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
    public class PeopleProcessorTests
    {
        private PeopleProcessor sut;
        private IPersonDataProvider mockPersonDataProvider;
        private IPersonParser mockPersonParser;
        private string[] birthData;
        private Person[] personData;

        [SetUp]
        public void Setup()
        {
            mockPersonDataProvider = Substitute.For<IPersonDataProvider>();
            mockPersonParser = Substitute.For<IPersonParser>();

            sut = new PeopleProcessor(mockPersonDataProvider, mockPersonParser);

            birthData = new string[] {
                                        "1960, ",
                                        "1960, 1999",
                                        "1960, ",
                                        "1960, ",
                                        "1930, 1990",
                                        "1930, 1988",
                                        "1930, ",
                                        "1980, ",
                                        "1980, ",
                                        "1945, 1977",
                                    };

            personData = new Person[] {
                                        new Person() { BirthYear = 1960, DeathYear = null },
                                        new Person() { BirthYear = 1960, DeathYear = 199 },
                                        new Person() { BirthYear = 1960, DeathYear = null },
                                        new Person() { BirthYear = 1960, DeathYear = null },
                                        new Person() { BirthYear = 1930, DeathYear = 1990 },
                                        new Person() { BirthYear = 1930, DeathYear = 1988 },
                                        new Person() { BirthYear = 1930, DeathYear = null },
                                        new Person() { BirthYear = 1980, DeathYear = null },
                                        new Person() { BirthYear = 1980, DeathYear = null },
                                        new Person() { BirthYear = 1945, DeathYear = 1977 },
                                      };
        }

        [Test]
        public void YearsShouldBeOfCountOneWhenGetYearsWithMostAliveIsCalled()
        {
            // Arrange
            mockPersonDataProvider.GetBirthData().Returns(birthData);
            mockPersonParser.Parse(birthData).Returns(personData);

            // Act
            IEnumerable<int> years = sut.GetYearsWithMostAlive();

            // Assert
            Assert.AreEqual(1, years.Count());
        }

        [Test]
        public void OnlyYearShouldBe1960WhenGetYearsWithMostAliveIsCalled()
        {
            // Arrange
            mockPersonDataProvider.GetBirthData().Returns(birthData);
            mockPersonParser.Parse(birthData).Returns(personData);

            // Act
            IEnumerable<int> years = sut.GetYearsWithMostAlive();

            // Assert
            Assert.AreEqual(1960, years.ElementAt(0));
        }
    }
}

using Contracts;
using Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class SimplePersonMapperTests
    {
        private SimplePersonMapper sut;

        [SetUp]
        public void Setup()
        {
            sut = new SimplePersonMapper();
        }

        [Test]
        public void WhenCallingMapIfFieldsAreBothNumbersThenPersonWithBirthYearAndDeathYearIsReturned()
        {
            // Arrange
            string[] fields = { "1977", "1999" };

            // Act
            Person actualPerson = sut.Map(fields);

            // Assert
            Assert.AreEqual(1977, actualPerson.BirthYear);
            Assert.AreEqual(1999, actualPerson.DeathYear);
        }

        [Test]
        public void WhenCallingMapIfFieldsHasOnlyBirthYearThenDeathYearIsNull()
        {
            // Arrange
            string[] fields = { "1977", "" };

            // Act
            Person actualPerson = sut.Map(fields);

            // Assert
            Assert.IsNull(actualPerson.DeathYear);
        }
    }
}

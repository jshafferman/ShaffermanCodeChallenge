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
    public class StreamPersonDataProviderTests
    {
        private StreamPersonDataProvider sut;
        private const string Filename = "people.txt";

        [SetUp]
        public void Setup()
        {
            sut = new StreamPersonDataProvider(Filename);
        }

        [Test]
        public void WhenGetBirthDataIsCalledTheListContainsAThousandLinesOfData()
        {
            // Arrange

            // Act
            IEnumerable<string> lines = sut.GetBirthData();

            // Assert
            Assert.AreEqual(1000, lines.Count());
        }
    }
}

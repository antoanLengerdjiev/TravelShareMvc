using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;

namespace TravelShar.Data.Tests.DbRepositoryTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullContextIsPassed()
        {
            // Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() => new DbRepository<MockedModel>(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullContextIsPassed()
        {
            // Arrange
            var expectedExMessage = "Database context cannot be null.";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            new DbRepository<MockedModel>(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallSetMethodOfContext_WhenValidContextIsPassed()
        {
            // Arrange
            var mockedContext = new Mock<IApplicationDbContext>();

            // Act
            var genericRepository = new DbRepository<MockedModel>(mockedContext.Object);

            // Asert
            mockedContext.Verify(c => c.Set<MockedModel>(), Times.Once);
        }
    }
}

using System;
using System.Data.Entity;
using Moq;
using NUnit.Framework;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;

namespace TravelShar.Data.Tests.DbRepositoryTests
{
    [TestFixture]
    public class HardDelete
    {
        [Test]
        public void ShouldCallDbContextRemoveMethod()
        {
            // Arrange 
            var mockedModel = new MockedModel();
            var mockedDbset = new Mock<IDbSet<MockedModel>>();
            var mockedContext = new Mock<IApplicationDbContext>();
           
            mockedContext.Setup(x => x.Set<MockedModel>().Remove(mockedModel)).Verifiable();

            var dbRepository = new DbRepository<MockedModel>(mockedContext.Object);
            

            // Act
            dbRepository.HardDelete(mockedModel);

            // Assert
            mockedContext.Verify(x => x.Set<MockedModel>().Remove(mockedModel), Times.Once);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenPassedParameterIsNull()
        {
            // Arrange 
            var mockedContext = new Mock<IApplicationDbContext>();

            var dbRepository = new DbRepository<MockedModel>(mockedContext.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => dbRepository.HardDelete(null));

        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenPassedParameterIsNull()
        {
            // Arrange
            var expectedMessage = "Cannot Hard Delete null object.";
            var mockedContext = new Mock<IApplicationDbContext>();

            var dbRepository = new DbRepository<MockedModel>(mockedContext.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => dbRepository.HardDelete(null));

            StringAssert.Contains(expectedMessage, exception.Message);

        }
    }
}

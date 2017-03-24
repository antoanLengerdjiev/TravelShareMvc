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
    public class Add
    {
        [Test]
        public void ShouldCallDbContextAddMethod()
        {
            // Arrange 
            var mockedModel = new MockedModel();
            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(x => x.Set<MockedModel>().Add(mockedModel)).Verifiable();

            var dbRepository = new DbRepository<MockedModel>(mockedContext.Object);

            // Act
            dbRepository.Add(mockedModel);

            // Assert
            mockedContext.Verify(x => x.Set<MockedModel>().Add(mockedModel), Times.Once);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenPassedParameterIsNull()
        {
            // Arrange 
            var mockedContext = new Mock<IApplicationDbContext>();
           
            var dbRepository = new DbRepository<MockedModel>(mockedContext.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => dbRepository.Add(null));
          
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenPassedParameterIsNull()
        {
            // Arrange
            var expectedMessage = "Cannot Add null object.";
            var mockedContext = new Mock<IApplicationDbContext>();

            var dbRepository = new DbRepository<MockedModel>(mockedContext.Object);

            // Act & Assert
           var exception = Assert.Throws<ArgumentNullException>(() => dbRepository.Add(null));

            StringAssert.Contains(expectedMessage, exception.Message);

        }
    }
}

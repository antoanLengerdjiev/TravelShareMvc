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
    public class Delete
    {
        [Test]
        public void ShouldSetIsDeletedToTrueOnTheModel()
        {
            // Arrange
            var mockedModel = new MockedModel() {IsDeleted = false };
            var mockedContext = new Mock<IApplicationDbContext>();
           

            var dbRepository = new EfDbRepository<MockedModel>(mockedContext.Object);

            // Act
            dbRepository.Delete(mockedModel);

            // Assert
            Assert.IsTrue(mockedModel.IsDeleted);
        }
        [Test]
        public void ShouldThrowArgumentNullException_WhenPassedParameterIsNull()
        {
            // Arrange 
            var mockedContext = new Mock<IApplicationDbContext>();

            var dbRepository = new EfDbRepository<MockedModel>(mockedContext.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => dbRepository.Delete(null));

        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenPassedParameterIsNull()
        {
            // Arrange
            var expectedMessage = "Cannot Delete null object.";
            var mockedContext = new Mock<IApplicationDbContext>();

            var dbRepository = new EfDbRepository<MockedModel>(mockedContext.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => dbRepository.Delete(null));

            StringAssert.Contains(expectedMessage, exception.Message);

        }
    }
}

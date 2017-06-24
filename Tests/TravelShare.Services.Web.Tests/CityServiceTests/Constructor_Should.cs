namespace TravelShare.Services.Web.Tests.CityServiceTests
{
    using System;
    using Common;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenCityRepositoryIsNull()
        {
            // Arrange
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new CityService(null, dbSaveChanges.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCityRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.CityRepositoryNullExceptionMessage;
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new CityService(null, dbSaveChanges.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenDbSaveChangesIsNull()
        {
            // Arrange
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new CityService(mockedCityRepository.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDbSaveChangesIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.DbContextSaveChangesNullExceptionMessage;
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new CityService(mockedCityRepository.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }


        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new CityService(mockedCityRepository.Object, dbSaveChanges.Object));
        }
    }
}

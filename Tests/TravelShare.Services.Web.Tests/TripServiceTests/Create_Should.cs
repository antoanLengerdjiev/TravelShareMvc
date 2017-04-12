namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void CallsAddFromTripRepository()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act
            tripService.Create(It.IsAny<Trip>());

            // Assert
            mockedTripRepository.Verify(x => x.Add(It.IsAny<Trip>()), Times.Once);
        }

        [Test]
        public void CallsSaveChangesFromDbContextSaveChanges()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act
            tripService.Create(It.IsAny<Trip>());

            // Assert
            mockSaveChanges.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}

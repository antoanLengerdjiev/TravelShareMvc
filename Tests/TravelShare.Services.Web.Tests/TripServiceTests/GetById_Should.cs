namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using Data;
    using Data.Common.Contracts;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void CallGetByIdFromTripRepository()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.GetById(It.IsAny<int>());

            mockedTripRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ReturnTypeOfTrip()
        {
            // Arrange
            var trip = new Trip();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockedTripRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(trip);
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            var result = tripService.GetById(It.IsAny<int>());

            Assert.IsInstanceOf<Trip>(result);
        }

        [Test]
        public void ReturnsCorrectTrip()
        {
            // Arrange
            var trip = new Trip() { Id = 5};
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockedTripRepository.Setup(x => x.GetById(5)).Returns(trip);
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            var result = tripService.GetById(5);

            Assert.AreSame(trip, result);
        }
    }
}

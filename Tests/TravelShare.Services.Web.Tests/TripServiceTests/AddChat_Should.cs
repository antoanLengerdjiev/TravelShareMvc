namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;
    using TravelShare.Services.Data.Common.Contracts;

    [TestFixture]
    public class AddChat_Should
    {
        [Test]
        public void CallGetByIdFromTripRepository()
        {
            // Arrange
            var chatId = 3;
            var tripId = 5;

            var trip = new Trip { Id = tripId };
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockedTripRepository.Setup(x => x.GetById(tripId)).Returns(trip);
            var mockedCityService = new Mock<ICityService>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockedTripRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.AddChat(tripId, chatId);

            // Assert
            mockedTripRepository.Verify(x => x.GetById(tripId), Times.Once);
        }

        [Test]
        public void SetChatIdToATripWithTripId()
        {
            // Arrange
            var chatId = 3;
            var tripId = 5;

            var trip = new Trip { Id = tripId };
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockedTripRepository.Setup(x => x.GetById(tripId)).Returns(trip);
            var mockedCityService = new Mock<ICityService>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockedTripRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.AddChat(tripId, chatId);

            // Assert
            Assert.AreEqual(chatId, trip.ChatId);
        }

        [Test]
        public void CallSaveChangesFromDbContextSaveChanges()
        {
            // Arrange
            var chatId = 3;
            var tripId = 5;

            var trip = new Trip { Id = tripId };
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockedTripRepository.Setup(x => x.GetById(tripId)).Returns(trip);
            var mockedCityService = new Mock<ICityService>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockedTripRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.AddChat(tripId, chatId);

            // Assert
            dbSaveChanges.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}

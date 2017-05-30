namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;
    using TravelShare.Services.Data.Common.Contracts;

    [TestFixture]
    public class DeleteTrip_Should
    {
        [Test]
        public void CallGetByIdFromUserRepository()
        {
            // Arrange
            var userId = "userId";
            var trip = new Trip();
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() { trip} };
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById(userId)).Returns(user);
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.DeleteTrip(userId, trip);

            // Assert
            mockUserRepository.Verify(x => x.GetById(userId), Times.Once);
        }

        [Test]
        public void CallDeleteFromTripRepository()
        {
            // Arrange
            var userId = "userId";
            var trip = new Trip();
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() { trip } };
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById(userId)).Returns(user);
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();
            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.DeleteTrip(userId, trip);

            // Assert
            mockedTripRepository.Verify(x => x.Delete(trip), Times.Once);
        }

        [Test]
        public void CallSaveChangesFromDbContextSaveChanges()
        {
            // Arrange
            var userId = "userId";
            var trip = new Trip();
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() { trip } };
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById(userId)).Returns(user);
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.DeleteTrip(userId, trip);

            // Assert
            mockSaveChanges.Verify(x => x.SaveChanges(), Times.Once);
        }


        [Test]
        public void ShouldRemoveTheTripFromUser()
        {
            // Arrange
            var userId = "userId";
            var trip = new Trip();
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() { trip } };
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById(userId)).Returns(user);
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.DeleteTrip(userId, trip);

            // Assert
            CollectionAssert.DoesNotContain(user.Trips, trip);
        }

        [Test]
        public void ShouldRemoveUserFromPassengers()
        {
            // Arrange
            var userId = "userId";
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Passengers = new List<ApplicationUser>() { user } };
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById(userId)).Returns(user);
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();
            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.DeleteTrip(userId, trip);

            // Assert
            CollectionAssert.DoesNotContain(trip.Passengers, user);
        }
    }
}

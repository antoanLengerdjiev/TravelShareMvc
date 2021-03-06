﻿namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using Data;
    using Data.Common.Contracts;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    [TestFixture]
    public class JoinTrip_Should
    {
        [Test]
        public void CallsSaveChangesFromDbContextSaveChange()
        {
            // Arrange
            var user = new ApplicationUser();
            var trip = new Trip();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.JoinTrip(user, trip);

            // Assert
            mockSaveChanges.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void AddUserToPassengers()
        {
            // Arrange
            var user = new ApplicationUser();
            var trip = new Trip();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.JoinTrip(user, trip);

            // Assert
            CollectionAssert.Contains(trip.Passengers, user);
        }

        [Test]
        public void AddTripToUsersTrips()
        {
            // Arrange
            var user = new ApplicationUser();
            var trip = new Trip();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.JoinTrip(user, trip);

            // Assert
            CollectionAssert.Contains(user.Trips, trip);
        }
    }
}

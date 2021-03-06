﻿namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Data;
    using Data.Common.Contracts;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    [TestFixture]
    public class CanUserJoinTrip_Should
    {
        [Test]
        public void ReturnTrue_WhenThereIsSlotsAndUserIsNotPassengerOrDriver()
        {
            // Arrange
            var userId = "UserId";
            var driverId = "DriverId";
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = "AnotherId" };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() { passenger1, passenger2, passenger3 };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var mockedCityService = new Mock<ICityService>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object,mockUserRepository.Object, mockedCityService.Object);

            // Act
            var result = service.CanUserJoinTrip(userId, driverId, 4, passengers);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnFalse_WhenThereIsNotSlots()
        {
            // Arrange
            var userId = "UserId";
            var driverId = "DriverId";
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = "AnotherId" };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() { passenger1, passenger2, passenger3 };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            var result = service.CanUserJoinTrip(userId, driverId, 3, passengers);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnFalse_WhenDriveIdEquelsUserId()
        {
            // Arrange
            var userId = "UserId";
            var driverId = userId;
            var passengers = new List<ApplicationUser>();

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            var result = service.CanUserJoinTrip(userId, driverId, 1,passengers);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnFalse_WhenUserIsInPassengers()
        {
            // Arrange
            var userId = "UserId";
            var driverId = "DriverId";
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = userId };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() {passenger1,passenger2,passenger3 };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            var result = service.CanUserJoinTrip(userId, driverId,5,passengers);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ThorwArgumentNullException_WhenUserIsNull()
        {
            // Arrange
            var driverId = "DriverId";
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = "random" };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() { passenger1, passenger2, passenger3 };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CanUserJoinTrip(null, driverId,5, passengers));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenUserIsNull()
        {
            // Arrange
            var expectedMessage = GlobalConstants.UserIdNullExceptionMessage;
            var driverId = "DriverId";
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = "random" };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() { passenger1, passenger2, passenger3 };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            var exception =Assert.Throws<ArgumentNullException>(() => service.CanUserJoinTrip(null, driverId, 6,passengers));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void ThorwArgumentNullException_WhenDriverIdIsNull()
        {
            // Arrange
            var userId = "UserId";
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = userId };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() { passenger1, passenger2, passenger3 };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CanUserJoinTrip(userId, null,5 ,passengers));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenDriverIdIsNull()
        {
            // Arrange
            var expectedMessage = "DriverId cannot be null";
            var userId = "UserId";
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = userId };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() { passenger1, passenger2, passenger3 };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => service.CanUserJoinTrip(userId, null, 5,passengers));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void ThorwArgumentNullException_WhenPassengerAreNull()
        {
            // Arrange
            var userId = "UserId";
            var driverId = "DriverId";

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CanUserJoinTrip(userId, driverId,9, null));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenPassengersAreNull()
        {
            // Arrange
            var expectedMessage = "Passenger cannot be null";
            var userId = "UserId";
            var driverId = "DriverId";

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => service.CanUserJoinTrip(userId, driverId,7, null));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void ThorwArgumentNullException_WhenDriverIdAndPassengersAreNull()
        {
            // Arrange
            var userId = "UserId";

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CanUserJoinTrip(userId, null, 5,null));
        }

        [Test]
        public void ThorwArgumentNullException_WhenDriverIdAndUserIdAreNull()
        {
            // Arrange
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = "random" };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() { passenger1, passenger2, passenger3 };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CanUserJoinTrip(null, null, 5,passengers));
        }

        [Test]
        public void ThorwArgumentNullException_WhenUserIdAndPassengersAreNull()
        {
            // Arrange
            var driverId = "DriverId";

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CanUserJoinTrip(null, driverId, 5, null));
        }

        [Test]
        public void ThorwArgumentNullException_WhenAllParametersAreNull()
        {
            // Arrange
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var service = new TripService(mockRepository.Object, dbSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CanUserJoinTrip(null, null, 5, null));
        }
    }
}

namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Models;

    [TestFixture]
    public class IsUserInTrip_Should
    {
        [Test]
        public void ReturnTrue_WhenDriveIdEquelsUserId()
        {
            // Arrange
            var userId = "UserId";
            var driverId = userId;
            var passengers = new List<ApplicationUser>();

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act
            var result = service.IsUserInTrip(userId, driverId, passengers);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnTrue_WhenUserIsInPassengers()
        {
            // Arrange
            var userId = "UserId";
            var driverId = "DriverId";
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = userId };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() {passenger1,passenger2,passenger3 };

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act
            var result = service.IsUserInTrip(userId, driverId, passengers);

            // Assert
            Assert.IsTrue(result);
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

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.IsUserInTrip(null, driverId, passengers));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenUserIsNull()
        {
            // Arrange
            var expectedMessage = "UserId cannot be null";
            var driverId = "DriverId";
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = "random" };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() { passenger1, passenger2, passenger3 };

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act & Assert
            var exception =Assert.Throws<ArgumentNullException>(() => service.IsUserInTrip(null, driverId, passengers));

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

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.IsUserInTrip(userId, null, passengers));
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

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => service.IsUserInTrip(userId, null, passengers));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void ThorwArgumentNullException_WhenPassengerAreNull()
        {
            // Arrange
            var userId = "UserId";
            var driverId = "DriverId";

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.IsUserInTrip(userId, driverId, null));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenPassengersAreNull()
        {
            // Arrange
            var expectedMessage = "Passenger cannot be null";
            var userId = "UserId";
            var driverId = "DriverId";

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => service.IsUserInTrip(userId, driverId, null));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void ThorwArgumentNullException_WhenDriverIdAndPassengersAreNull()
        {
            // Arrange
            var userId = "UserId";

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.IsUserInTrip(userId, null, null));
        }

        [Test]
        public void ThorwArgumentNullException_WhenDriverIdAndUserIdAreNull()
        {
            // Arrange
            var passenger1 = new ApplicationUser() { Id = "randonId" };
            var passenger2 = new ApplicationUser() { Id = "random" };
            var passenger3 = new ApplicationUser() { Id = "RandonId" };
            var passengers = new List<ApplicationUser>() { passenger1, passenger2, passenger3 };

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.IsUserInTrip(null, null, passengers));
        }

        [Test]
        public void ThorwArgumentNullException_WhenUserIdAndPassengersAreNull()
        {
            // Arrange
            var driverId = "DriverId";

            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.IsUserInTrip(null, driverId, null));
        }

        [Test]
        public void ThorwArgumentNullException_WhenAllParametersAreNull()
        {
            // Arrange
            var mockRepository = new Mock<IDbRepository<Trip>>();

            var service = new TripService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.IsUserInTrip(null, null, null));
        }
    }
}

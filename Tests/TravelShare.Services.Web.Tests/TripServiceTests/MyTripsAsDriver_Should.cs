namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class MyTripsAsDriver_Should
    {
        [Test]
        public void ThorwArgumentNullException_WhenParameterUserIdIsNull()
        {
            // Arrange
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tripService.MyTripsAsDriver(null, 0, 1));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenParameterUserIdIsNull()
        {
            // Arrange
            var expectedMessage = "User ID cannot be null.";
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => tripService.MyTripsAsDriver(null, 0, 1));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void CallTripRepositoryMethodAll()
        {
            // Arrange
            var userId = "UserId";
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act
            tripService.MyTripsAsDriver(userId, 0, 1);

            // Assert
            mockedTripRepository.Verify(x => x.All(), Times.Once);
        }

        [Test]
        public void ReturnInstanceOfIEnumerableITrip()
        {
            // Arrange
            var userId = "UserId";
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockTripRepository.Setup(x => x.All()).Returns(new List<Trip>() { }.AsQueryable);
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act
            var result = tripService.MyTripsAsDriver(userId, 0, 1);

            // Assert
            Assert.IsInstanceOf<IEnumerable<Trip>>(result);
        }

        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(0, 2)]
        public void ReturnCorrectResult(int page, int perPage)
        {
            // Arrange
            var driverId = "UserId";
            var trip1 = new Trip { DriverId = driverId };
            var trip2 = new Trip { DriverId = driverId };
            var trip3 = new Trip { DriverId = "Stancho" };
            var list = new List<Trip>() { trip1, trip2, trip3 };
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockTripRepository.Setup(x => x.All()).Returns(list.AsQueryable);
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);
            var expectedResult = list.Where(x => x.DriverId == driverId).Skip(page * perPage).Take(perPage);
            // Act
            var result = tripService.MyTripsAsDriver(driverId, page, perPage);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
            Assert.LessOrEqual(perPage, result.Count());
        }
    }
}

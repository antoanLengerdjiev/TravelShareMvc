namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Data.Common.Contracts;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class MyTripsAsDriverCount_Should
    {
        [Test]
        public void ThorwArgumentNullException_WhenParameterUserIdIsNull()
        {
            // Arrange
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tripService.MyTripsAsDriverPageCount(null, 1));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenParameterUserIdIsNull()
        {
            // Arrange
            var expectedMessage = GlobalConstants.UserIdNullExceptionMessage;
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => tripService.MyTripsAsDriverPageCount(null, 1));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void CallTripRepositoryMethodAll()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.MyTripsAsDriverPageCount("UserId", 3);

            // Assert
            mockedTripRepository.Verify(x => x.All(), Times.Once);
        }

        [Test]
        public void ReturnInstanceOfInt()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockedTripRepository.Setup(x => x.All()).Returns(new List<Trip>().AsQueryable());
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            var result = tripService.MyTripsAsDriverPageCount("UserId", 3);

            // Assert
            Assert.IsInstanceOf<int>(result);
        }

        [Test]
        public void Return0_WhenPerPageParameterIsBiggertThanTripsCount()
        {
            // Arrange
            var driverId = "UserId";
            var trip1 = new Trip { DriverId = driverId };
            var trip2 = new Trip { DriverId = driverId };
            var trip3 = new Trip { DriverId = "Stancho" };
            var list = new List<Trip>() { trip1, trip2, trip3 };
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockedTripRepository.Setup(x => x.All()).Returns(list.AsQueryable());
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            var result = tripService.MyTripsAsDriverPageCount(driverId, 3);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestCase(2, 2)]
        [TestCase(1, 3)]
        public void ReturnCorrectResult(int perPage, int expectedResult)
        {
            // Arrange
            var driverId = "UserId";
            var trip1 = new Trip { DriverId = driverId };
            var trip2 = new Trip { DriverId = driverId };
            var trip3 = new Trip { DriverId = driverId };
            var list = new List<Trip>() { trip1, trip2, trip3 };
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockedTripRepository.Setup(x => x.All()).Returns(list.AsQueryable());
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            var result = tripService.MyTripsAsDriverPageCount(driverId, perPage);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}

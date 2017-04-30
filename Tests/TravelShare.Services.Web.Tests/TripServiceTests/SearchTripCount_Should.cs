using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;
using TravelShare.Data.Models;
using TravelShare.Services.Data;

namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    [TestFixture]
    public class SearchTripCount_Should
    {

        public void ThorwArgumentNullException_WhenParameterFromIsNull()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tripService.SearchTripCount(null, "Sofia", date, 1));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenParameterFromIsNull()
        {
            // Arrange
            var expectedMessage = "From cannot be null";
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => tripService.SearchTripCount(null, "Sofia", date, 1));

            StringAssert.Contains(expectedMessage, exception.Message);
        }


        [Test]
        public void ThorwArgumentNullException_WhenParameterToIsNull()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tripService.SearchTripCount("Sofia", null, date, 1));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenParameterToIsNull()
        {
            // Arrange
            var expectedMessage = "To cannot be null";
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => tripService.SearchTripCount("Sofia", null, date, 1));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void ThorwArgumentNullException_WhenParametersToAndFromAreNull()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tripService.SearchTripCount(null, null, date, 1));
        }

        [Test]
        public void CallTripRepositoryMethodAll()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act
            tripService.SearchTripCount("Sofia", "Burgas", new DateTime(1994, 1, 1), 3);

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

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act
            var result = tripService.SearchTripCount("Sofia", "Burgas", new DateTime(1994, 1, 1), 3);

            // Assert
            Assert.IsInstanceOf<int>(result);
        }

        [TestCase(2)]
        [TestCase(3)]
        public void Return0_WhenPerPageParameterIsEqualOrBiggertThanTripsCount(int perPage)
        {
            // Arrange
            var from = "Sofia";
            var to = "Burgas";
            var date = new DateTime(1994, 1, 1);

            var trip1 = new Trip {From = from , To = to, Date = date };
            var trip2 = new Trip { From = from, To = to, Date = date };
            var trip3 = new Trip { From = from, To = "Silistra", Date = date };
            var list = new List<Trip>() { trip1, trip2, trip3 };
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockedTripRepository.Setup(x => x.All()).Returns(list.AsQueryable());
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act
            var result = tripService.SearchTripCount(from, to, date, perPage);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestCase(2, 2)]
        [TestCase(1, 3)]
        public void ReturnCorrectResult(int perPage, int expectedResult)
        {
            // Arrange
            var from = "Sofia";
            var to = "Burgas";
            var date = new DateTime(1994, 1, 1);

            var trip1 = new Trip { From = from, To = to, Date = date };
            var trip2 = new Trip { From = from, To = to, Date = date };
            var trip3 = new Trip { From = from, To = to, Date = date };
            var list = new List<Trip>() { trip1, trip2, trip3 };
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockedTripRepository.Setup(x => x.All()).Returns(list.AsQueryable());
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act
            var result = tripService.SearchTripCount(from, to, date, perPage);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

    }
}

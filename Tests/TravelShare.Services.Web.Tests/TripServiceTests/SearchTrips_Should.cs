namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class SearchTrips_Should
    {
        [Test]
        public void ThorwArgumentNullException_WhenParameterFromIsNull()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var tripService = new TripService(mockTripRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tripService.SearchTrips(null, "Sofia", date));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenParameterFromIsNull()
        {
            // Arrange
            var expectedMessage = "From cannot be null";
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var tripService = new TripService(mockTripRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => tripService.SearchTrips(null, "Sofia", date));

            StringAssert.Contains(expectedMessage, exception.Message);
        }


        [Test]
        public void ThorwArgumentNullException_WhenParameterToIsNull()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var tripService = new TripService(mockTripRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tripService.SearchTrips("Sofia", null, date));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenParameterToIsNull()
        {
            // Arrange
            var expectedMessage = "To cannot be null";
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var tripService = new TripService(mockTripRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => tripService.SearchTrips("Sofia", null, date));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void ThorwArgumentNullException_WhenParametersToAndFromAreNull()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var tripService = new TripService(mockTripRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tripService.SearchTrips(null, null, date));
        }

        [Test]
        public void CallTripRepositoryMethodAll()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var tripService = new TripService(mockTripRepository.Object);

            // Act
            tripService.SearchTrips("Sofia", "Burgars", date);

            // Assert
            mockTripRepository.Verify(x => x.All());
        }

        [Test]
        public void ReturnInstanceOfIQueribleTrip()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var tripService = new TripService(mockTripRepository.Object);
            mockTripRepository.Setup(x => x.All()).Returns(new List<Trip>() { }.AsQueryable);

            // Act
            var result = tripService.SearchTrips("Sofia", "Burgars", date);

            // Assert
            Assert.IsInstanceOf<IQueryable<Trip>>(result);
        }
    }
}

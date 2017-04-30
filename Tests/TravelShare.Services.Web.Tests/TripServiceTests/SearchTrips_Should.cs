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
    public class SearchTrips_Should
    {
        [Test]
        public void ThorwArgumentNullException_WhenParameterFromIsNull()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tripService.SearchTrips(null, "Sofia", date, 0, 1));
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
            var exception = Assert.Throws<ArgumentNullException>(() => tripService.SearchTrips(null, "Sofia", date, 0, 1));

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
            Assert.Throws<ArgumentNullException>(() => tripService.SearchTrips("Sofia", null, date, 0, 1));
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
            var exception = Assert.Throws<ArgumentNullException>(() => tripService.SearchTrips("Sofia", null, date, 0, 1));

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
            Assert.Throws<ArgumentNullException>(() => tripService.SearchTrips(null, null, date, 0, 1));
        }

        [Test]
        public void CallTripRepositoryMethodAll()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act
            tripService.SearchTrips("Sofia", "Burgars", date, 0, 1);

            // Assert
            mockTripRepository.Verify(x => x.All());
        }

        [Test]
        public void ReturnInstanceOfIEnumerableITrip()
        {
            // Arrange
            var date = new DateTime(1994, 1, 1);
            var mockTripRepository = new Mock<IEfDbRepository<Trip>>();
            mockTripRepository.Setup(x => x.All()).Returns(new List<Trip>() { }.AsQueryable);
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var tripService = new TripService(mockTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object);

            // Act
            var result = tripService.SearchTrips("Sofia", "Burgars", date, 0, 1);

            // Assert
            Assert.IsInstanceOf<IEnumerable<Trip>>(result);
        }
    }
}

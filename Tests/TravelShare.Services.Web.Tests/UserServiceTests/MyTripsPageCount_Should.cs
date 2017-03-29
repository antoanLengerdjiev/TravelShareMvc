namespace TravelShare.Services.Web.Tests.UserServiceTests
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class MyTripsPageCount_Should
    {
        [Test]
        public void CallUserRepositoryMethodGetById_WhenParameterAreValid()
        {
            // Arrange
            string userId = "RandomId";
            var firstTrip = new Trip() { Id = 16, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var secondTrip = new Trip() { Id = 17, IsDeleted = false, Date = new DateTime(1994, 2, 1) };
            var deletedTrip = new Trip() { Id = 15, IsDeleted = true, Date = new DateTime(1994, 1, 1) };
            var list = new List<Trip>()
            {
                firstTrip,
                deletedTrip,
                secondTrip,
            };
            var user = new ApplicationUser() { Id = userId, Trips = list };
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById(userId)).Returns(user);

            var service = new UserService(mockUserRepository.Object);

            // Act
            var result = service.MyTripsPageCount(userId, 1);

            // Assert
            mockUserRepository.Verify(x => x.GetById(userId), Times.Once);
        }

        [Test]
        public void ReturnCorrectResultWithOutAdditionalPage_WhenParameterAreValidAnd()
        {
            // Arrange
            string userId = "RandomId";
            var firstTrip = new Trip() { Id = 16, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var secondTrip = new Trip() { Id = 17, IsDeleted = false, Date = new DateTime(1994, 2, 1) };
            var thirdTrip = new Trip() { Id = 15, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var list = new List<Trip>()
            {
                firstTrip,
                thirdTrip,
                secondTrip,
            };
            var user = new ApplicationUser() { Id = userId, Trips = list };
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById(userId)).Returns(user);

            var service = new UserService(mockUserRepository.Object);

            // Act
            var result = service.MyTripsPageCount(userId, 3);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void ReturnCorrectResultWithAdditionalPage_WhenParameterAreValidAnd()
        {
            // Arrange
            string userId = "RandomId";
            var firstTrip = new Trip() { Id = 16, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var secondTrip = new Trip() { Id = 17, IsDeleted = false, Date = new DateTime(1994, 2, 1) };
            var thirdTrip = new Trip() { Id = 15, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var list = new List<Trip>()
            {
                firstTrip,
                thirdTrip,
                secondTrip,
            };
            var user = new ApplicationUser() { Id = userId, Trips = list };
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById(userId)).Returns(user);

            var service = new UserService(mockUserRepository.Object);

            // Act
            var result = service.MyTripsPageCount(userId, 2);

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void ThrowsArgumentNullException_WhenUserIdParameterIsNull()
        {
            // Arrange
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();

            var service = new UserService(mockUserRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.MyTripsPageCount(null, 3));
        }

        [Test]
        public void ThrowsArgumentNullExceptionWithCorrectMessage_WhenUserIdParameterIsNull()
        {
            // Arrange
            var expectedMessage = "User Id cannot be null";
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();

            var service = new UserService(mockUserRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => service.MyTripsPageCount(null, 3));

            StringAssert.Contains(expectedMessage, exception.Message);
        }
    }
}

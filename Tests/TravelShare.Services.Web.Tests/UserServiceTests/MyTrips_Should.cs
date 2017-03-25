namespace TravelShare.Services.Web.Tests.UserServiceTests
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
    public class MyTrips_Should
    {
        [Test]
        public void ReturnCorrecResultOftFirstPageWithSecondTrip()
        {
            // Arrange
            var firstTrip = new Trip() { Id = 16, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var secondTrip = new Trip() { Id = 17, IsDeleted = false, Date = new DateTime(1994, 2, 1) };
            var deletedTrip = new Trip() { Id = 15, IsDeleted = true, Date = new DateTime(1994, 1, 1) };
            var list = new List<Trip>()
            {
                firstTrip,
                deletedTrip,
                secondTrip,
            };
            var user = new ApplicationUser() { Id = "pesho", Trips = list };
            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById("pesho")).Returns(user);

            var service = new UserService(mockUserRepository.Object);

            // Act
            var result = service.MyTrips("pesho", 0, 1);

            // Assert
            Assert.AreEqual(secondTrip, result.FirstOrDefault());
        }

        [Test]
        public void ReturnCorrecResultOfSecondPageWithFirstTrip()
        {
            // Arrange
            var firstTrip = new Trip() { Id = 16, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var secondTrip = new Trip() { Id = 17, IsDeleted = false, Date = new DateTime(1994, 2, 1) };
            var deletedTrip = new Trip() { Id = 15, IsDeleted = true, Date = new DateTime(1994, 1, 1) };
            var list = new List<Trip>()
            {
                firstTrip,
                deletedTrip,
                secondTrip,
            };
            var user = new ApplicationUser() { Id = "pesho", Trips = list };
            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById("pesho")).Returns(user);

            var service = new UserService(mockUserRepository.Object);

            // Act
            var result = service.MyTrips("pesho", 1, 1);

            // Assert
            Assert.AreEqual(firstTrip, result.FirstOrDefault());
        }

        [Test]
        public void ReturnCorrecResultOfFirstPageWithTripsOrderdByDate()
        {
            // Arrange
            var firstTrip = new Trip() { Id = 16, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var secondTrip = new Trip() { Id = 17, IsDeleted = false, Date = new DateTime(1994, 2, 1) };
            var deletedTrip = new Trip() { Id = 15, IsDeleted = true, Date = new DateTime(1994, 1, 1) };
            var list = new List<Trip>()
            {
                firstTrip,
                deletedTrip,
                secondTrip,
            };
            var user = new ApplicationUser() { Id = "pesho", Trips = list };
            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById("pesho")).Returns(user);

            var service = new UserService(mockUserRepository.Object);

            // Act
            var result = service.MyTrips("pesho", 0, 2);


            // Assert
            Assert.AreEqual(secondTrip, result.FirstOrDefault());
            Assert.AreEqual(firstTrip, result.Last());
        }

        [Test]
        public void ThrowsArgumentNullException_WhenUserIdParameterIsNull()
        {
            // Arrange
            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();

            var service = new UserService(mockUserRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.MyTrips(null, 0, 1));
        }


        [Test]
        public void ThrowsArgumentNullExceptionWithCorrectMessage_WhenUserIdParameterIsNull()
        {
            // Arrange
            var expectedMessage = "User Id cannot be null";
            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();

            var service = new UserService(mockUserRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => service.MyTrips(null, 0, 1));

            StringAssert.Contains(expectedMessage, exception.Message);
        }


        [Test]
        public void CallUserRepositoryMethodGetById()
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
            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById(userId)).Returns(user);

            var service = new UserService(mockUserRepository.Object);

            // Act
            var result = service.MyTrips(userId, 0, 1);

            // Assert
            mockUserRepository.Verify(x => x.GetById(userId), Times.Once);
        }
    }
}

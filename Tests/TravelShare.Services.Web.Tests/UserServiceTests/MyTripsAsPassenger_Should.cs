﻿namespace TravelShare.Services.Web.Tests.UserServiceTests
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
    public class MyTripsAsPassenger_Should
    {

        [Test]
        public void ThorwArgumentNullException_WhenParameterUserIdIsNull()
        {
            // Arrange
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var userService = new UserService(mockUserRepository.Object, mockSaveChanges.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => userService.MyTripsAsPassenger(null, 0,1));
        }

        [Test]
        public void ThorwArgumentNullExceptionWithCorrectMessage_WhenParameterUserIdIsNull()
        {
            // Arrange
            var expectedMessage = "User Id cannot be null";
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var userService = new UserService(mockUserRepository.Object, mockSaveChanges.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => userService.MyTripsAsPassenger(null, 0, 1));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

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
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById("pesho")).Returns(user);

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.MyTripsAsPassenger("pesho", 0, 1);

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
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById("pesho")).Returns(user);

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.MyTripsAsPassenger("pesho", 1, 1);

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
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById("pesho")).Returns(user);

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.MyTripsAsPassenger("pesho", 0, 2);

            // Assert
            Assert.AreEqual(secondTrip, result.FirstOrDefault());
            Assert.AreEqual(firstTrip, result.Last());
        }

        [Test]
        public void ThrowsArgumentNullException_WhenUserIdParameterIsNull()
        {
            // Arrange
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.MyTripsAsPassenger(null, 0, 1));
        }

        [Test]
        public void ThrowsArgumentNullExceptionWithCorrectMessage_WhenUserIdParameterIsNull()
        {
            // Arrange
            var expectedMessage = "User Id cannot be null";
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => service.MyTripsAsPassenger(null, 0, 1));

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
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.GetById(userId)).Returns(user);

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.MyTripsAsPassenger(userId, 0, 1);

            // Assert
            mockUserRepository.Verify(x => x.GetById(userId), Times.Once);
        }
    }
}

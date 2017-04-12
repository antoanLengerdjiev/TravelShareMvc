﻿namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Data.Models;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Services.Data.Common.Contracts;

    [TestFixture]
    public class LeaveTrip_Should
    {
        [Test]
        public void CallsGetByIdFromUserService()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var userId = "UserId";
            mockedUserService.Setup(x => x.GetById(userId)).Verifiable();
            mockedTripService.Setup(x => x.GetById(5)).Verifiable();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.LeaveTrip(5);

            // Assert
            mockedUserService.Verify(x => x.GetById(userId), Times.Once);
        }

        [Test]
        public void CallGetByIdFromTripService()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var userId = "UserId";
            mockedUserService.Setup(x => x.GetById(userId)).Verifiable();
            mockedTripService.Setup(x => x.GetById(5)).Verifiable();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.LeaveTrip(5);

            // Assert
            mockedTripService.Verify(x => x.GetById(5), Times.Once);
        }

        [Test]
        public void ReturnJsonNotFoundTrue_WhenUserIsNull()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var userId = "UserId";
            mockedUserService.Setup(x => x.GetById(userId)).Returns((ApplicationUser)null).Verifiable();
            mockedTripService.Setup(x => x.GetById(5)).Verifiable();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            var result = controller.LeaveTrip(5) as JsonResult;
            dynamic data = result.Data;

            // Assert
            Assert.AreEqual(true, data.notFound);
        }

        [Test]
        public void ReturnJsonNotFoundTrue_WhenTripIsNull()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var userId = "UserId";
            var user = new ApplicationUser() { Id = userId };

            mockedUserService.Setup(x => x.GetById(userId)).Returns((ApplicationUser)null).Verifiable();
            mockedTripService.Setup(x => x.GetById(5)).Verifiable();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            var result = controller.LeaveTrip(5) as JsonResult;
            dynamic data = result.Data;

            // Assert
            Assert.AreEqual(true, data.notFound);
        }

        [Test]
        public void CallTripServiceMethodCanUserJoin()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();

            var userId = "UserId";
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = 5, Slots = 5, DriverId = "IdOfUser", Passengers = new List<ApplicationUser>() };

            mockedUserService.Setup(x => x.GetById(userId)).Returns(user).Verifiable();
            mockedTripService.Setup(x => x.GetById(5)).Returns(trip).Verifiable();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.JoinTrip(5);

            // Assert
            mockedTripService.Verify(x => x.CanUserJoinTrip(userId, trip.DriverId, trip.Slots, trip.Passengers), Times.Once);
        }

        [Test]
        public void ReturnJsonNotInIsTrue_WhenUserIsNotPassenger()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();

            var userId = "UserId";
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = 5, Slots = 5, DriverId = "IdOfUser", Passengers = new List<ApplicationUser>() };

            mockedUserService.Setup(x => x.GetById(userId)).Returns(user).Verifiable();
            mockedTripService.Setup(x => x.GetById(5)).Returns(trip).Verifiable();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            var result = controller.LeaveTrip(5) as JsonResult;
            dynamic data = result.Data;

            // Assert
            Assert.AreEqual(true, data.notIn);
        }

        [Test]
        public void CallTripServiceMethodLeaveTrip_WhenUserIsPassenger()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();

            var userId = "UserId";
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = 5, Slots = 5, DriverId = "IdOfUser", Passengers = new List<ApplicationUser>() { user } };

            mockedUserService.Setup(x => x.GetById(userId)).Returns(user).Verifiable();
            mockedTripService.Setup(x => x.GetById(5)).Returns(trip).Verifiable();
            mockedTripService.Setup(x => x.CanUserJoinTrip(userId, trip.DriverId, trip.Slots, trip.Passengers)).Returns(true);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.LeaveTrip(5);

            // Assert
            mockedTripService.Verify(x => x.LeaveTrip(user, trip), Times.Once);
        }

        [Test]
        public void ReturnJsonWithCorrectData_WhenUserIsInTrip()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();

            var userId = "UserId";
            var user = new ApplicationUser() { Id = userId, UserName = "Gosho" };
            var trip = new Trip() { Id = 5, Slots = 5, DriverId = "IdOfUser", Passengers = new List<ApplicationUser>() { user } };

            mockedUserService.Setup(x => x.GetById(userId)).Returns(user).Verifiable();
            mockedTripService.Setup(x => x.GetById(5)).Returns(trip).Verifiable();
            mockedTripService.Setup(x => x.CanUserJoinTrip(userId, trip.DriverId, trip.Slots, trip.Passengers)).Returns(true);

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;
            var expectedFreeSlots = trip.Slots - trip.Passengers.Count < 0 ? 0 : trip.Slots - trip.Passengers.Count;

            // Act
            var result = controller.LeaveTrip(5) as JsonResult;
            dynamic data = result.Data;

            // Assert
            Assert.AreEqual(expectedFreeSlots, data.slots);
            Assert.AreEqual(user.UserName, data.removedPassangerName);
        }
    }
}
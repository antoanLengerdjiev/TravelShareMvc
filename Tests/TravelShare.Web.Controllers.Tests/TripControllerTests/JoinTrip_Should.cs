namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    [TestFixture]
    public class JoinTrip_Should
    {
        [Test]
        public void CallDataProviderUserGetById()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var userId = "UserId";
            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Users.GetById(userId)).Verifiable();
            mockedData.Setup(x => x.Trips.GetById(5)).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.JoinTrip(5);

            // Assert
            mockedData.Verify(x => x.Users.GetById(userId), Times.Once);
        }

        [Test]
        public void CallDataProviderTripsGetById()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var userId = "UserId";
            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Users.GetById(userId)).Verifiable();
            mockedData.Setup(x => x.Trips.GetById(5)).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.JoinTrip(5);

            // Assert
            mockedData.Verify(x => x.Trips.GetById(5), Times.Once);
        }

        [Test]
        public void ReturnJsonNotFoundTrue_WhenUserIsNull()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var userId = "UserId";
            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns((ApplicationUser)null).Verifiable();
            mockedData.Setup(x => x.Trips.GetById(5)).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            var result = controller.JoinTrip(5) as JsonResult;
            dynamic data = result.Data;

            // Assert
            Assert.AreEqual(true, data.notFound);
        }

        [Test]
        public void ReturnJsonNotFoundTrue_WhenTripIsNull()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var userId = "UserId";
            var user = new ApplicationUser() { Id = userId };
            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();
            mockedData.Setup(x => x.Trips.GetById(5)).Returns((Trip)null).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            var result = controller.JoinTrip(5) as JsonResult;
            dynamic data = result.Data;

            // Assert
            Assert.AreEqual(true, data.notFound);
        }

        [Test]
        public void CallTripServiceMethodCanUserJoin()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var userId = "UserId";
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = 5, Slots = 5,DriverId = "IdOfUser", Passengers = new List<ApplicationUser>() };
            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();
            mockedData.Setup(x => x.Trips.GetById(5)).Returns(trip).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.JoinTrip(5);

            // Assert
            mockedTripService.Verify(x => x.CanUserJoinTrip(userId, trip.DriverId, trip.Slots, trip.Passengers));
        }

        [Test]
        public void ReturnJsonAlreadyInTrue_WhenCanUserJoinTripIsFalse()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var userId = "UserId";
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = 5, Slots = 5, DriverId = "IdOfUser", Passengers = new List<ApplicationUser>() };

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();
            mockedData.Setup(x => x.Trips.GetById(5)).Returns(trip).Verifiable();
            mockedTripService.Setup(x => x.CanUserJoinTrip(userId, trip.DriverId, trip.Slots, trip.Passengers)).Returns(false);
            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            var result = controller.JoinTrip(5) as JsonResult;
            dynamic data = result.Data;

            // Assert
            Assert.AreEqual(true, data.alreadyIn);
        }

        [Test]
        public void CallDataProviderMethodSaveChanges_WhenCanUserJoinTripIsTrue()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var userId = "UserId";
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = 5, Slots = 5, DriverId = "IdOfUser", Passengers = new List<ApplicationUser>() };

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();
            mockedData.Setup(x => x.Trips.GetById(5)).Returns(trip).Verifiable();
            mockedTripService.Setup(x => x.CanUserJoinTrip(userId, trip.DriverId, trip.Slots, trip.Passengers)).Returns(true);
            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.JoinTrip(5);

            // Assert
            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void ReturnJsonWithCorrectData_WhenCanUserJoinTripIsTrue()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var userId = "UserId";
            var user = new ApplicationUser() { Id = userId, UserName = "Gosho" };
            var trip = new Trip() { Id = 5, Slots = 5, DriverId = "IdOfUser", Passengers = new List<ApplicationUser>() };

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();
            mockedData.Setup(x => x.Trips.GetById(5)).Returns(trip).Verifiable();
            mockedTripService.Setup(x => x.CanUserJoinTrip(userId, trip.DriverId, trip.Slots, trip.Passengers)).Returns(true);
            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;
            var expectedFreeSlots = trip.Slots - trip.Passengers.Count < 0 ? 0 : trip.Slots - trip.Passengers.Count;

            // Act
            var result = controller.JoinTrip(5) as JsonResult;
            dynamic data = result.Data;

            // Assert
            Assert.AreEqual(expectedFreeSlots - 1, data.slots);
            Assert.AreEqual(user.UserName, data.newPassangerName);
        }
    }
}

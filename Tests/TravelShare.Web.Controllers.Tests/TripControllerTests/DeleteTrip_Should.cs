namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    [TestFixture]
    public class DeleteTrip_Should
    {
        [Test]
        public void CallGetByIdFromDataProviderUsers()
        {
            // Arrange
            var userId = "userId";
            var tripId = 5;
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = tripId, DriverId = userId, Driver = user };
            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.GetById(tripId)).Returns(trip).Verifiable();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.DeleteTrip(tripId);

            // Assert
            mockedData.Verify(x => x.Users.GetById(userId), Times.Once);
        }

        [Test]
        public void CallGetByIdFromDataProviderTrips()
        {
            // Arrange
            var userId = "userId";
            var tripId = 5;
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = tripId, DriverId = userId, Driver = user };
            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.GetById(tripId)).Returns(trip).Verifiable();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.DeleteTrip(tripId);

            // Assert
            mockedData.Verify(x => x.Trips.GetById(tripId), Times.Once);
        }

        [Test]
        public void RedirectToActionErrorForbinden_WhenUserIsNotTheDriverOfTheTrip()
        {
            // Arrange
            var userId = "userId";
            var tripId = 5;
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = tripId, DriverId = "AnotherUserId" };
            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.GetById(tripId)).Returns(trip).Verifiable();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act & Assert
            controller.WithCallTo(x => x.DeleteTrip(tripId)).ShouldRedirectTo<ErrorController>(x => new ErrorController().Forbidden());
        }

        [Test]
        public void CallDeleteFromDataProviderTrips()
        {
            // Arrange
            var userId = "userId";
            var tripId = 5;
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = tripId, DriverId = userId, Driver = user };
            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.GetById(tripId)).Returns(trip).Verifiable();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();
            mockedData.Setup(x => x.Trips.Delete(trip)).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.DeleteTrip(tripId);

            // Assert
            mockedData.Verify(x => x.Trips.Delete(trip), Times.Once);
        }

        [Test]
        public void CallSaveChangesFromDataProviderTrips()
        {
            // Arrange
            var userId = "userId";
            var tripId = 5;
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = tripId, DriverId = userId, Driver = user };
            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.GetById(tripId)).Returns(trip).Verifiable();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();
            mockedData.Setup(x => x.Trips.Delete(trip)).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.DeleteTrip(tripId);

            // Assert
            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void RedirectToActionCreate_WhenTripIsDelete()
        {
            // Arrange
            var userId = "userId";
            var tripId = 5;
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = tripId, DriverId = userId, Driver = user };
            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.GetById(tripId)).Returns(trip).Verifiable();
            mockedData.Setup(x => x.Users.GetById(userId)).Returns(user).Verifiable();
            mockedData.Setup(x => x.Trips.Delete(trip)).Verifiable();
            mockedData.Setup(x => x.SaveChanges()).Verifiable();
            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => userId;

            // Act && Assert
            controller.WithCallTo(x => x.DeleteTrip(tripId)).ShouldRedirectTo<TripController>(x => x.Create());
        }
    }
}

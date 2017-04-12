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
        public void CallGetByIdFromTripService()
        {
            // Arrange
            var userId = "userId";
            var tripId = 5;
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = tripId, DriverId = userId, Driver = user };
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            mockedTripService.Setup(x => x.GetById(tripId)).Returns(trip);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.DeleteTrip(tripId);

            // Assert
            mockedTripService.Verify(x => x.GetById(tripId), Times.Once);
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
            mockedTripService.Setup(x => x.GetById(tripId)).Returns(trip).Verifiable();
            var mockedUserService = new Mock<IUserService>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);

            controller.GetUserId = () => userId;

            // Act & Assert
            controller.WithCallTo(x => x.DeleteTrip(tripId)).ShouldRedirectTo<ErrorController>(x => new ErrorController().Forbidden());
        }

        [Test]
        public void CallDeleteFromTripService()
        {
            // Arrange
            var userId = "userId";
            var tripId = 5;
            var user = new ApplicationUser() { Id = userId };
            var trip = new Trip() { Id = tripId, DriverId = userId, Driver = user };
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(tripId)).Returns(trip).Verifiable();

            var mockedUserService = new Mock<IUserService>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.DeleteTrip(tripId);

            // Assert
            mockedTripService.Verify(x => x.DeleteTrip(userId, trip), Times.Once);
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
            mockedTripService.Setup(x => x.GetById(tripId)).Returns(trip).Verifiable();
            mockedTripService.Setup(x => x.DeleteTrip(userId, trip)).Verifiable();
            var mockedUserService = new Mock<IUserService>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act && Assert
            controller.WithCallTo(x => x.DeleteTrip(tripId)).ShouldRedirectTo<TripController>(x => x.Create());
        }
    }
}

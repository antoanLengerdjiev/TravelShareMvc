namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Data.Models;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using TestStack.FluentMVCTesting;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Web.Infrastructure.Mapping;
    using ViewModels.Trips;

    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void CallTripGetByIdMethod()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);
            var trip = new Trip { Passengers = new List<ApplicationUser>() };
            var mockedUserService = new Mock<IUserService>();

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(It.IsAny<int>())).Returns(trip).Verifiable();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);
            var mock = new Mock<ControllerContext>();

            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(false);
            controller.ControllerContext = mock.Object;

            // Act
            controller.GetById(It.IsAny<int>());

            // Assert
            mockedTripService.Verify(x => x.GetById(It.IsAny<int>()));
        }

        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedUserService = new Mock<IUserService>();

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(It.IsAny<int>())).Verifiable();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(false);
            controller.ControllerContext = mock.Object;

            // Act & Assert
            controller.WithCallTo(x => x.GetById(It.IsAny<int>())).ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnViewWithModelWithCorrectProperties_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var driver = new ApplicationUser { UserName = "Pesho" };
            var passenger = new ApplicationUser { UserName = "Gosho" };
            var trip = new Trip() { From = "Sofia", To = "Plovdiv", Driver = driver,Date = new System.DateTime (1994,1,1),Money = 4, Slots = 5,Passengers = new List<ApplicationUser> { passenger },Description = "Kef be" };
            var mockedUserService = new Mock<IUserService>();

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(5)).Returns(trip).Verifiable();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(false);
            controller.ControllerContext = mock.Object;

            // Act & Assert
            controller
                .WithCallTo(b => b.GetById(5))
                .ShouldRenderDefaultView()
                .WithModel<TripDetailedModel>(viewModel =>
                {
                    Assert.AreEqual(trip.From, viewModel.From);
                    Assert.AreEqual(trip.To, viewModel.To);
                    Assert.AreEqual(trip.Driver.UserName, viewModel.Driver.UserName);
                    Assert.AreEqual(trip.Date, viewModel.Date);
                    Assert.AreEqual(trip.Description, viewModel.Description);
                    Assert.AreEqual(trip.Money, viewModel.Money);
                    Assert.AreEqual(trip.Slots, viewModel.Slots);
                    Assert.AreEqual(trip.Passengers.Count, viewModel.Passengers.Count);
                });
        }

        [Test]
        public void ReturnViewWithEmptyModel_WhenThereNoModelWithThePassedId()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedUserService = new Mock<IUserService>();

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(5)).Returns((Trip)null).Verifiable();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);

            var mock = new Mock<ControllerContext>();

            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(false);
            controller.ControllerContext = mock.Object;

            // Act & Assert
            controller
                .WithCallTo(b => b.GetById(5))
                .ShouldRenderDefaultView();
        }


        [Test]
        public void IsUserInIsTrue_WhenUserIsPassenger()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var userId = "IdOfmyChoosing";
            var driver = new ApplicationUser { UserName = "Pesho", Id = "DriverId" };
            var passenger = new ApplicationUser { UserName = "Gosho", Id = userId };
            var trip = new Trip() { From = "Sofia", To = "Plovdiv", DriverId = driver.Id, Driver = driver, Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Passengers = new List<ApplicationUser> { passenger }, Description = "Kef be" };

            var passengers = new List<ApplicationUser>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(5)).Returns(trip).Verifiable();

            var mockedUserService = new Mock<IUserService>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);

            controller.ControllerContext = mock.Object;
            controller.GetUserId = () => userId;

            // Act
            controller.GetById(5);

            // Assert
            controller.WithCallTo(x => x.GetById(5)).ShouldRenderDefaultView().WithModel<TripDetailedModel>(model =>
            {
                Assert.IsTrue(model.IsUserIn);
            });
        }

        [Test]
        public void IsUserInIsFalse_WhenUserIsNotPassenger()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var userId = "IdOfmyChoosing";
            var driver = new ApplicationUser { UserName = "Pesho", Id = "DriverId" };
            var passenger = new ApplicationUser { UserName = "Gosho", Id = "AnotherUser" };
            var trip = new Trip() { From = "Sofia", To = "Plovdiv", DriverId = driver.Id, Driver = driver, Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Passengers = new List<ApplicationUser> { passenger }, Description = "Kef be" };

            var passengers = new List<ApplicationUser>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(5)).Returns(trip).Verifiable();

            var mockedUserService = new Mock<IUserService>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object);

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);

            controller.ControllerContext = mock.Object;
            controller.GetUserId = () => userId;

            // Act
            controller.GetById(5);

            // Assert
            controller.WithCallTo(x => x.GetById(5)).ShouldRenderDefaultView().WithModel<TripDetailedModel>(model =>
            {
                Assert.IsFalse(model.IsUserIn);
            });
        }
    }
}

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

            var mockedData = new Mock<IApplicationData>();

            var mockedTripService = new Mock<ITripService>();
            mockedData.Setup(x => x.Trips.GetById(It.IsAny<int>())).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            var mock = new Mock<ControllerContext>();

            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(false);
            controller.ControllerContext = mock.Object;

            // Act
            controller.GetById(It.IsAny<int>());

            // Assert
            mockedData.Verify(x => x.Trips.GetById(It.IsAny<int>()));
        }

        [Test]
        public void ReturnDefaultView()
        {

            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();
            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.GetById(It.IsAny<int>())).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);

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

            var mockedTripService = new Mock<ITripService>();
            var mockedData = new Mock<IApplicationData>();

            var driver = new ApplicationUser { UserName = "Pesho" };
            var passenger = new ApplicationUser { UserName = "Gosho" };
            var trip = new Trip() { From = "Sofia", To = "Plovdiv", Driver = driver,Date = new System.DateTime (1994,1,1),Money = 4, Slots = 5,Passengers = new List<ApplicationUser> { passenger },Description = "Kef be" };

            mockedData.Setup(m => m.Trips.GetById(5)).Returns(trip);
            var controller = new TripController(mockedData.Object, mockedTripService.Object);

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

            var mockedTripService = new Mock<ITripService>();
            var mockedData = new Mock<IApplicationData>();

            mockedData.Setup(m => m.Trips.GetById(5)).Returns((Trip)null);
            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            var mock = new Mock<ControllerContext>();

            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(false);
            controller.ControllerContext = mock.Object;

            // Act & Assert
            controller
                .WithCallTo(b => b.GetById(5))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void CallIsUserInMethod_WhenIsAutheticatedIsTrue()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var driver = new ApplicationUser { UserName = "Pesho", Id = "DriverId" };
            var passenger = new ApplicationUser { UserName = "Gosho" };
            var trip = new Trip() { From = "Sofia", To = "Plovdiv",DriverId=driver.Id, Driver = driver, Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Passengers = new List<ApplicationUser> { passenger }, Description = "Kef be" };

            var passengers = new List<ApplicationUser>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.CanUserJoinTrip("IdOfmyChoosing", "DriverId", 5,passengers)).Returns(true);
            var mockedData = new Mock<IApplicationData>();

            mockedData.Setup(m => m.Trips.GetById(5)).Returns(trip);
            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            var mock = new Mock<ControllerContext>();

            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act
            controller.GetById(5);

            // Assert
            mockedTripService.Verify(x => x.CanUserJoinTrip("IdOfmyChoosing", "DriverId", 5,It.IsNotNull<IEnumerable<ApplicationUser>>()), Times.Once);
        }

        [Test]
        public void SetupViewDataShowJoinButtonToFalse_WhenIsAutheticatedIsTrueAndCanJoinTripIsFalse()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var driver = new ApplicationUser { UserName = "Pesho", Id = "DriverId" };
            var passenger = new ApplicationUser { UserName = "Gosho" };
            var trip = new Trip() { From = "Sofia", To = "Plovdiv", DriverId = driver.Id, Driver = driver, Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Passengers = new List<ApplicationUser> { passenger }, Description = "Kef be" };

            var passengers = new List<ApplicationUser>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.CanUserJoinTrip("IdOfmyChoosing", "DriverId", 5, passengers)).Returns(false);
            var mockedData = new Mock<IApplicationData>();

            mockedData.Setup(m => m.Trips.GetById(5)).Returns(trip);
            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;
            controller.GetUserId = () => "IdOfmyChoosing";


            controller.GetById(5);

            // Act & Assert
            Assert.IsFalse((bool)controller.ViewData["ShowJoinButton"]);
        }

        [Test]
        public void SetupViewDataShowJoinButtonToTrue_WhenIsAutheticatedIsTrueAndCanJoinTripIsTrue()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var driver = new ApplicationUser { UserName = "Pesho", Id = "DriverId" };
            var passenger = new ApplicationUser { UserName = "Gosho" };
            var trip = new Trip() { From = "Sofia", To = "Plovdiv", DriverId = driver.Id, Driver = driver, Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Passengers = new List<ApplicationUser> { passenger }, Description = "Kef be" };

            var passengers = new List<ApplicationUser>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.CanUserJoinTrip("IdOfmyChoosing", "DriverId", 5, passengers)).Returns(true).Verifiable();
            var mockedData = new Mock<IApplicationData>();

            mockedData.Setup(m => m.Trips.GetById(5)).Returns(trip).Verifiable();
            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true).Verifiable();
            controller.ControllerContext = mock.Object;
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act
            controller.GetById(5);

            // Assert
            Assert.IsFalse((bool)controller.ViewData["ShowJoinButton"]);
        }

        [Test]
        public void CalledGetUserIdOnce_WhenIsAutheticatedIsTrue()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var driver = new ApplicationUser { UserName = "Pesho", Id = "DriverId" };
            var passenger = new ApplicationUser { UserName = "Gosho" };
            var trip = new Trip() { From = "Sofia", To = "Plovdiv", DriverId = driver.Id, Driver = driver, Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Passengers = new List<ApplicationUser> { passenger }, Description = "Kef be" };

            var passengers = new List<ApplicationUser>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.CanUserJoinTrip("IdOfmyChoosing", "DriverId", 5, passengers)).Returns(false);
            var mockedData = new Mock<IApplicationData>();

            mockedData.Setup(m => m.Trips.GetById(5)).Returns(trip);
            var controller = new TripController(mockedData.Object, mockedTripService.Object);

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);

            controller.ControllerContext = mock.Object;
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act 
            controller.GetById(5);

            // Assert
            mockedTripService.Verify(x => x.CanUserJoinTrip(controller.GetUserId(), "DriverId", 5,It.IsNotNull<IEnumerable<ApplicationUser>>()), Times.Once);
        }
    }
}

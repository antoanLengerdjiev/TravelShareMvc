namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Data.Models;
    using Mappings;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using TestStack.FluentMVCTesting;
    using TravelShareMvc.Providers.Contracts;
    using ViewModels;
    using ViewModels.Trips;

    [TestFixture]
    public class GetById_Should
    {

        [Test]
        public void CallIsRequestAuthenticatedFromAuthService()
        {
            // Arrange
            var trip = new Trip { Passengers = new List<ApplicationUser>() };
            var mockedUserService = new Mock<IUserService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(It.IsAny<int>())).Returns(trip).Verifiable();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var mockMapperProvider = new Mock<IMapperProvider>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.GetById(It.IsAny<int>());

            // Assert
            mockAuthProvider.Verify(x => x.IsAuthenticated, Times.Once);
        }

        [Test]
        public void CallCurrendUserIdFromAuthService_WhenRequestIsAuthenticated()
        {
            // Arrange
            var trip = new Trip { Passengers = new List<ApplicationUser>() };
            var tripModel = new TripDetailedModel { Passengers = new List<UserViewModel>() };
            var mockedUserService = new Mock<IUserService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(It.IsAny<int>())).Returns(trip).Verifiable();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.IsAuthenticated).Returns(true);

            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(m => m.Map<TripDetailedModel>(It.IsAny<Trip>())).Returns(tripModel);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.GetById(It.IsAny<int>());

            // Assert
            mockAuthProvider.Verify(x => x.CurrentUserId, Times.Once);
        }

        [Test]
        public void CallTripGetByIdMethod()
        {
            // Arrange
            var trip = new Trip { Passengers = new List<ApplicationUser>() };
            var tripModel = new TripDetailedModel { Passengers = new List<UserViewModel>() };

            var mockedUserService = new Mock<IUserService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(It.IsAny<int>())).Returns(trip).Verifiable();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.IsAuthenticated).Returns(true);

            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(m => m.Map<TripDetailedModel>(It.IsAny<Trip>())).Returns(tripModel);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.GetById(It.IsAny<int>());

            // Assert
            mockedTripService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(It.IsAny<int>())).Verifiable();

            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.IsAuthenticated).Returns(false);

            var mockMapperProvider = new Mock<IMapperProvider>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act & Assert
            controller.WithCallTo(x => x.GetById(It.IsAny<int>())).ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnViewWithModelWithCorrectProperties_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var tripModel = new TripDetailedModel {Driver = new UserViewModel {UserName ="Pesho" }, Passengers = new List<UserViewModel>() { new UserViewModel { UserName = "Gosho" } }, From = "Sofia", To = "Plovdiv", Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Description = "Kef be" };
            var driver = new ApplicationUser { UserName = "Pesho" };
            var passenger = new ApplicationUser { UserName = "Gosho" };
            var trip = new Trip() { From = "Sofia", To = "Plovdiv", Driver = driver,Date = new System.DateTime (1994, 1, 1),Money = 4, Slots = 5,Passengers = new List<ApplicationUser> { passenger },Description = "Kef be" };
            var mockedUserService = new Mock<IUserService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(5)).Returns(trip).Verifiable();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.IsAuthenticated).Returns(false);

            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(m => m.Map<TripDetailedModel>(It.IsAny<Trip>())).Returns(tripModel);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

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
            var mockedUserService = new Mock<IUserService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(5)).Returns((Trip)null).Verifiable();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.IsAuthenticated).Returns(false);

            var mockMapperProvider = new Mock<IMapperProvider>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act & Assert
            controller
                .WithCallTo(b => b.GetById(5))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void IsUserInIsTrue_WhenUserIsPassenger()
        {
            // Arrange
            var userId = "IdOfmyChoosing";
            var driver = new ApplicationUser { UserName = "Pesho", Id = "DriverId" };
            var driverView = new UserViewModel { UserName = "Pesho", Id = "DriverId" };

            var passenger = new ApplicationUser { UserName = "Gosho", Id = userId };
            var passengerView = new UserViewModel() { UserName = "Gosho", Id = userId };

            var trip = new Trip() { From = "Sofia", To = "Plovdiv", DriverId = driver.Id, Driver = driver, Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Passengers = new List<ApplicationUser> { passenger }, Description = "Kef be" };

            var tripModel = new TripDetailedModel() { From = "Sofia", To = "Plovdiv", DriverId = driver.Id, Driver = driverView, Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Passengers = new List<UserViewModel> { passengerView }, Description = "Kef be" };

            var passengers = new List<ApplicationUser>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(5)).Returns(trip).Verifiable();

            var mockedUserService = new Mock<IUserService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.IsAuthenticated).Returns(true);
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);

            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(m => m.Map<TripDetailedModel>(It.IsAny<Trip>())).Returns(tripModel);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

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
            var userId = "IdOfmyChoosing";

            var driver = new ApplicationUser { UserName = "Pesho", Id = "DriverId" };
            var driverView = new UserViewModel { UserName = "Pesho", Id = "DriverId" };

            var passenger = new ApplicationUser { UserName = "Gosho", Id = "AnotherUser" };
            var passengerView = new UserViewModel() { UserName = "Gosho", Id = "AnotherUser" };
            var trip = new Trip() { From = "Sofia", To = "Plovdiv", DriverId = driver.Id, Driver = driver, Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Passengers = new List<ApplicationUser> { passenger }, Description = "Kef be" };
            var tripModel = new TripDetailedModel() { From = "Sofia", To = "Plovdiv", DriverId = driver.Id, Driver = driverView, Date = new System.DateTime(1994, 1, 1), Money = 4, Slots = 5, Passengers = new List<UserViewModel> { passengerView }, Description = "Kef be" };

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetById(5)).Returns(trip).Verifiable();

            var mockedMessageService = new Mock<IMessageService>();
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.IsAuthenticated).Returns(true);
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);

            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(m => m.Map<TripDetailedModel>(It.IsAny<Trip>())).Returns(tripModel);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

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

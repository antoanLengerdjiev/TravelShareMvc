namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System;
    using System.Collections.Generic;
    using Data.Models;
    using Mappings;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using Services.Data.Common.Models;
    using TestStack.FluentMVCTesting;
    using TravelShareMvc.Providers.Contracts;
    using ViewModels.Trips;

    [TestFixture]
    public class CreateHttpPost_Should
    {
        [Test]
        public void RedirectToHomeControllerIndex_WhenModelStateIsValid()
        {
            // Arrange
            var fromCity = new City { Name = "Sofia" };
            var toCity = new City { Name = "Plovdiv" };
            var newChat = new Chat() { Id = 2, TripId = 3 };

            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "Plovdiv", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };
            var trip = new Trip() { Id = 3, DriverId = "Id", FromCity = fromCity, ToCity = toCity, Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var tripCreationInfo = new TripCreationInfo { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var userId = "IdOfmyChoosing";
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() };

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.Create(tripCreationInfo)).Returns(trip);
            var mockedUserService = new Mock<IUserService>();
            var mockedChatService = new Mock<IChatService>();
            mockedChatService.Setup(x => x.Create(trip)).Returns(newChat).Verifiable();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(x => x.Map<TripCreationInfo>(model)).Returns(tripCreationInfo);
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Create(model)).ShouldRedirectTo<TripController>(x => new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockMapperProvider.Object).GetById(trip.Id));
        }

        [Test]
        public void ReturnDefaultView_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedChatService = new Mock<IChatService>();

            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act & Assert
            controller.ModelState.AddModelError("test", "test");

            controller.WithCallTo(x => x.Create(new TripCreateModel())).ShouldRenderDefaultView();
        }

        [Test]
        public void PostCreateAction_WhenInvoked_ShouldCallCreateMethodOfTripService()
        {
            // Arrange
            var fromCity = new City { Name = "Sofia" };
            var toCity = new City { Name = "Plovdiv" };
            var newChat = new Chat() { Id = 2, TripId = 3 };

            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "Plovdiv", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };
            var trip = new Trip() { Id = 3, DriverId = "Id", FromCity = fromCity, ToCity = toCity, Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };
            var tripCreationInfo = new TripCreationInfo { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var userId = "IdOfmyChoosing";
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() };

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.Create(tripCreationInfo)).Returns(trip);

            var mockedUserService = new Mock<IUserService>();
            var mockedChatService = new Mock<IChatService>();
            mockedChatService.Setup(x => x.Create(trip)).Returns(newChat);
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);
            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(x => x.Map<TripCreationInfo>(model)).Returns(tripCreationInfo);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.Create(model);

            // Assert
            mockedTripService.Verify(x => x.Create(It.IsAny<TripCreationInfo>()), Times.Once);
        }

        [Test]
        public void SetDriverIdToTheModel_WhenInvoked()
        {
            // Arrange
            var fromCity = new City { Name = "Sofia" };
            var toCity = new City { Name = "Plovdiv" };
            var newChat = new Chat() { Id = 2, TripId = 3 };

            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };
            var trip = new Trip() {Id = 3, DriverId = "Id", FromCity = fromCity, ToCity = toCity, Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var tripCreationInfo = new TripCreationInfo { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };
            var userId = "IdOfmyChoosing";
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() };

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.Create(tripCreationInfo)).Returns(trip);
            var mockedUserService = new Mock<IUserService>();
            var mockedChatService = new Mock<IChatService>();
            mockedChatService.Setup(x => x.Create(trip)).Returns(newChat);
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);
            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(x => x.Map<TripCreationInfo>(model)).Returns(tripCreationInfo);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.Create(model);

            // Assert
            Assert.AreSame(userId, model.DriverId);
        }

        [Test]
        public void CallGetCurrentUserIdFromAuthProvider()
        {
            // Arrange
            var fromCity = new City { Name = "Sofia" };
            var toCity = new City { Name = "Plovdiv" };

            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "Plovdiv", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var tripCreationInfo = new TripCreationInfo { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var trip = new Trip() { Id = 3, DriverId = "Id", FromCity = fromCity, ToCity = toCity, Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };
            var newChat = new Chat() { Id = 2, TripId = 3 };

            var userId = "IdOfmyChoosing";
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() };

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.Create(tripCreationInfo)).Returns(trip);
            var mockedUserService = new Mock<IUserService>();
            var mockedChatService = new Mock<IChatService>();
            mockedChatService.Setup(x => x.Create(trip)).Returns(newChat);
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(x => x.Map<TripCreationInfo>(model)).Returns(tripCreationInfo);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.Create(model);

            // Assert
            mockAuthProvider.Verify(x => x.CurrentUserId, Times.Once);
        }

        [Test]
        public void CallsCreateFromChatService()
        {
            // Arrange
            var fromCity = new City { Name = "Sofia" };
            var toCity = new City { Name = "Plovdiv" };

            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "Plovdiv", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var tripCreationInfo = new TripCreationInfo { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var trip = new Trip() { Id = 3, DriverId = "Id", FromCity = fromCity, ToCity = toCity, Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };
            var newChat = new Chat() { Id = 2, TripId = 3 };

            var userId = "IdOfmyChoosing";
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() };

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.Create(tripCreationInfo)).Returns(trip);
            var mockedUserService = new Mock<IUserService>();
            var mockedChatService = new Mock<IChatService>();
            mockedChatService.Setup(x => x.Create(trip)).Returns(newChat);
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(x => x.Map<TripCreationInfo>(model)).Returns(tripCreationInfo);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.Create(model);

            // Assert
            mockedChatService.Verify(x => x.Create(trip), Times.Once);
        }

        [Test]
        public void CallsAddChatFromTripService()
        {
            // Arrange
            var fromCity = new City { Name = "Sofia" };
            var toCity = new City { Name = "Plovdiv" };

            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "Plovdiv", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var tripCreationInfo = new TripCreationInfo { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var trip = new Trip() { Id = 3, DriverId = "Id", FromCity = fromCity, ToCity = toCity, Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };
            var newChat = new Chat() { Id = 2, TripId = 3 };

            var userId = "IdOfmyChoosing";
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() };

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.Create(tripCreationInfo)).Returns(trip);
            var mockedUserService = new Mock<IUserService>();
            var mockedChatService = new Mock<IChatService>();
            mockedChatService.Setup(x => x.Create(trip)).Returns(newChat);
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(x => x.Map<TripCreationInfo>(model)).Returns(tripCreationInfo);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.Create(model);

            // Assert
            mockedTripService.Verify(x => x.AddChat(trip.Id, newChat.Id), Times.Once);
        }
    }
}

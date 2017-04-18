namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System;
    using System.Collections.Generic;
    using Data.Models;
    using Infrastructure.Mapping;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
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
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var userId = "IdOfmyChoosing";
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() };

            var tripToBeAdded = new Trip() { From = model.From, To = model.To, Money = model.Money, Slots = model.Slots, DriverId = model.DriverId, Description = model.Description, Date = model.Date };

            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockAuthProvider.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Create(model)).ShouldRedirectTo<TripController>(x => new TripController(mockedTripService.Object, mockedUserService.Object, mockAuthProvider.Object).GetById(tripToBeAdded.Id));
        }

        [Test]
        public void ReturnDefaultView_WhenModelStateIsInvalid()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object,mockAuthProvider.Object);

            // Act & Assert
            controller.ModelState.AddModelError("test", "test");

            controller.WithCallTo(x => x.Create(new TripCreateModel())).ShouldRenderDefaultView();
        }

        [Test]
        public void PostCreateAction_WhenInvoked_ShouldCallCreateMethodOfTripService()
        {
            // Arrange
            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };
            var userId = "IdOfmyChoosing";
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() };

            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockAuthProvider.Object);

            // Act
            controller.Create(model);

            // Assert
            mockedTripService.Verify(x => x.Create(It.IsAny<Trip>()), Times.Once);
        }

        [Test]
        public void SetDriverIdToTheModel_WhenInvoked()
        {
            // Arrange
            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var userId = "IdOfmyChoosing";
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() };
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockAuthProvider.Object);

            // Act
            controller.Create(model);

            // Assert
            Assert.AreSame(userId, model.DriverId);
        }

        [Test]
        public void CallGetCurrentUserIdFromAuthProvider()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", Date = DateTime.Parse("01/01/2001"), Slots = 2, Money = 12, Description = "kef" };

            var userId = "IdOfmyChoosing";
            var user = new ApplicationUser() { Id = userId, Trips = new List<Trip>() };

            var tripToBeAdded = new Trip() { From = model.From, To = model.To, Money = model.Money, Slots = model.Slots, DriverId = model.DriverId, Description = model.Description, Date = model.Date };

            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockAuthProvider.Object);
            controller.Create(model);
            // Act & Assert
            mockAuthProvider.Verify(x => x.CurrentUserId, Times.Once);
        }
    }
}

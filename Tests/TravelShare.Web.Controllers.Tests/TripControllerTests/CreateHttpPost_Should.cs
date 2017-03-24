namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Common;
    using Data.Common.Contracts;
    using Data.Models;
    using Infrastructure.Mapping;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using TestStack.FluentMVCTesting;
    using ViewModels.Trips;

    [TestFixture]
    public class CreateHttpPost_Should
    {
        [Test]
        public void CallDataSaveChange()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", DateAsString = "01/01/2001", Slots = 5, Money = 12, Description = "kef" };

            var user = new ApplicationUser() { Id = "IdOfmyChoosing", Trips = new List<Trip>() };

            var tripToBeAdded = new Trip() { From = model.From, To = model.To, Money = model.Money, Slots = model.Slots, DriverId = model.DriverId, Description = model.Description, Date = model.Date };

            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.Add(tripToBeAdded)).Verifiable();
            mockedData.Setup(x => x.Users.GetById("IdOfmyChoosing")).Returns(user).Verifiable();
            mockedData.Setup(x => x.SaveChanges()).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act
            controller.Create(model);

            // Assert
            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void RedirectToHomeControllerIndex_WhenModelStateIsValid()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", DateAsString = "01/01/2001", Slots = 2, Money = 12, Description = "kef" };

            var user = new ApplicationUser() { Id = "IdOfmyChoosing", Trips = new List<Trip>() };

            var tripToBeAdded = new Trip() { From = model.From, To = model.To, Money = model.Money, Slots = model.Slots, DriverId = model.DriverId, Description = model.Description, Date = model.Date };

            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Users.GetById("IdOfmyChoosing")).Returns(user).Verifiable();
            mockedData.Setup(x => x.Trips.Add(tripToBeAdded)).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act & Assert
            controller.WithCallTo(x => x.Create(model)).ShouldRedirectTo<HomeController>(x => new HomeController(mockedData.Object).Index());
        }

        [Test]
        public void ReturnDefaultView_WhenModelStateIsInvalid()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();
            var mockedData = new Mock<IApplicationData>();
            var controller = new TripController(mockedData.Object, mockedTripService.Object);

            // Act & Assert
            controller.ModelState.AddModelError("test", "test");

            controller.WithCallTo(x => x.Create(new TripCreateModel())).ShouldRenderDefaultView();
        }

        [Test]
        public void PostCreateAction_WhenInvoked_ShouldCallTripAddMethod()
        {
            // Arrange
            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", DateAsString = "01/01/2001", Slots = 2, Money = 12, Description = "kef" };
            var user = new ApplicationUser() { Id = "IdOfmyChoosing", Trips = new List<Trip>() };

            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.Add(It.IsAny<Trip>())).Verifiable();
            mockedData.Setup(x => x.Users.GetById("IdOfmyChoosing")).Returns(user).Verifiable();
            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act
            controller.Create(model);

            // Assert
            mockedData.Verify(x => x.Trips.Add(It.IsAny<Trip>()), Times.Once);
        }

        [Test]
        public void SetDriverIdToTheModel_WhenInvoked()
        {
            // Arrange
            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", DateAsString = "01/01/2001", Slots = 2, Money = 12, Description = "kef" };

            var user = new ApplicationUser() { Id = "IdOfmyChoosing", Trips = new List<Trip>() };
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.Add(It.IsAny<Trip>())).Verifiable();
            mockedData.Setup(x => x.Users.GetById("IdOfmyChoosing")).Returns(user).Verifiable();
            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act
            controller.Create(model);

            // Assert
            Assert.AreSame(controller.GetUserId(), model.DriverId);
        }

        [Test]
        public void PostCreateAction_WhenInvoked_ShouldCallUserGetById()
        {
            // Arrange
            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", DateAsString = "01/01/2001", Slots = 2, Money = 12, Description = "kef" };

            var user = new ApplicationUser() { Id = "IdOfmyChoosing", Trips = new List<Trip>() };

            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.Add(It.IsAny<Trip>())).Verifiable();
            mockedData.Setup(x => x.Users.GetById("IdOfmyChoosing")).Returns(user).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act
            controller.Create(model);

            // Assert
            mockedData.Verify(x => x.Users.GetById("IdOfmyChoosing"), Times.Once);
        }

        [Test]
        public void AddTripToTheUser()
        {
            // Arrange
            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", DateAsString = "01/01/2001", Slots = 2, Money = 12, Description = "kef" };

            var user = new ApplicationUser() { Id = "IdOfmyChoosing", Trips = new List<Trip>() };

            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.Add(It.IsAny<Trip>())).Verifiable();
            mockedData.Setup(x => x.Users.GetById("IdOfmyChoosing")).Returns(user).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act
            controller.Create(model);

            // Assert
            Assert.AreEqual(1, user.Trips.Count);
        }

    }
}

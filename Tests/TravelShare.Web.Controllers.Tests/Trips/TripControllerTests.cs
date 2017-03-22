namespace TravelShare.Web.Controllers.Tests.Trips
{
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Web.Infrastructure.Mapping;
    using TravelShare.Web.ViewModels.Trips;

    [TestFixture]
    public class TripControllerTests
    {
        [Test]
        public void PostCreateAction_MustCallDataSaveChange()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);
            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", DateAsString = "01/01/2001", Slots = 5, Money = 12, Description = "kef" };

            var tripToBeAdded = new Trip() { From = model.From, To = model.To, Money = model.Money, Slots = model.Slots, DriverId = model.DriverId, Description = model.Description, Date = model.Date };

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.Add(tripToBeAdded)).Verifiable();
            mockedData.Setup(x => x.SaveChanges()).Verifiable();
            var controller = new TripController(mockedData.Object);
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act
            controller.Create(model);

            // Assert
            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void PostCreateAction_WhenModelStateIsValid_ShouldRedirectToHomeControllerIndex()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);
            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", DateAsString = "01/01/2001", Slots = 2, Money = 12, Description = "kef" };

            var tripToBeAdded = new Trip() { From = model.From, To = model.To, Money = model.Money, Slots = model.Slots, DriverId = model.DriverId, Description = model.Description, Date = model.Date };

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.Add(tripToBeAdded)).Verifiable();
            var controller = new TripController(mockedData.Object);
            controller.GetUserId = () => "IdOfmyChoosing";

            // Act & Assert
            controller.WithCallTo(x => x.Create(model)).ShouldRedirectTo<HomeController>(x => new HomeController(mockedData.Object).Index());
        }

        [Test]
        public void PostCreateAction_WhenModelStateIsInvalid_ShouldReturnDefaultView()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedData = new Mock<IApplicationData>();
            var controller = new TripController(mockedData.Object);

            // Act & Assert
            controller.ModelState.AddModelError("test", "test");

            controller.WithCallTo(x => x.Create(new TripCreateModel())).ShouldRenderDefaultView();
        }

        [Test]
        public void PostCreateAction_WhenInvoked_ShouldCallTripAddMethod()
        {
            // Arrange
            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", DateAsString = "01/01/2001", Slots = 2, Money = 12, Description = "kef" };
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.Add(It.IsAny<Trip>())).Verifiable();
            var controller = new TripController(mockedData.Object);

            controller.GetUserId = () => "IdOfmyChoosing";

            controller.Create(model);
            // Act & Assert
            mockedData.Verify(x => x.Trips.Add(It.IsAny<Trip>()), Times.Once);
        }

        [Test]
        public void PostCreateAction_WhenInvoked_ShouldSetDriverId()
        {
            // Arrange
            var model = new TripCreateModel() { DriverId = "Id", From = "Sofia", To = "burgas", DateAsString = "01/01/2001", Slots = 2, Money = 12, Description = "kef" };
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.Add(It.IsAny<Trip>())).Verifiable();
            var controller = new TripController(mockedData.Object);

            controller.GetUserId = () => "IdOfmyChoosing";

            controller.Create(model);
            // Act & Assert
            Assert.AreSame(controller.GetUserId(), model.DriverId);
        }

        [Test]
        public void GetCreateAction_ShouldReturnDefaultView()
        {
            // Arrange
            var mockedData = new Mock<IApplicationData>();

            var controller = new TripController(mockedData.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Create()).ShouldRenderDefaultView();
        }

        //[Test]
        //public void ActionAll_ShouldReturnDefaultView()
        //{
        //    // Arrange
        //    var mockedData = new Mock<IApplicationData>();
        //    mockedData.Setup(x => x.Trips.All()).Verifiable();
        //    mockedData.Setup(x => x.Trips.All().Count()).Returns(5).Verifiable();
        //    var controller = new TripController(mockedData.Object);

        //    // Act & Assert
        //    controller.All(0);
        //    controller.TempData["page"] = 0;
        //    controller.TempData["pageCount"] = 0;
        //    mockedData.Verify(x => x.Trips.All(), Times.Once);
        //}

            [Test]
        public void ActionGetById_ShouldCallTripGetById()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);
            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.GetById(It.IsAny<int>())).Verifiable();
            var controller = new TripController(mockedData.Object);

            // Act
            controller.GetById(It.IsAny<int>());

            // Assert
            mockedData.Verify(x => x.Trips.GetById(It.IsAny<int>()));
        }

        [Test]
        public void ActionGetById_ShouldReturnDefaultView()
        {

            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);
            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.GetById(It.IsAny<int>())).Verifiable();
            var controller = new TripController(mockedData.Object);

            // Act & Assert
            controller.WithCallTo(x => x.GetById(It.IsAny<int>())).ShouldRenderDefaultView();
        }
    }
}

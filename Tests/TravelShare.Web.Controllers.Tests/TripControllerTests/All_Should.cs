namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using Data.Common.Contracts;
    using Infrastructure.Mapping;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class All_Should
    {
        [Test]
        public void CallGetPagedTripsFromTripService_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.All()).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);

            // Act
            controller.All(It.IsAny<int>());

            // Assert
            mockedTripService.Verify(x => x.GetPagedTrips(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallGetPagesCountFromTripService_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.All()).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);

            // Act
            controller.All(It.IsAny<int>());

            // Assert
            mockedTripService.Verify(x => x.GetPagesCount(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ShouldSetTempDataToGetPagesCountFromTripService_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.GetPagesCount(It.IsAny<int>())).Returns(5).Verifiable();
            var mockedData = new Mock<IApplicationData>();
            mockedData.Setup(x => x.Trips.All()).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);

            // Act
            controller.All(It.IsAny<int>());

            // Assert
            Assert.AreEqual(controller.TempData["pageCount"], 5);
        }

        [Test]
        public void ShouldSetTempDataToCorrectPage_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);

            // Act
            controller.All(5);

            // Assert
            Assert.AreEqual(controller.TempData["page"], 5);
        }

        [Test]
        public void ShouldRenderDefaultView()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedTripService = new Mock<ITripService>();

            var mockedData = new Mock<IApplicationData>();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.All(5)).ShouldRenderDefaultView();
        }
    }
}

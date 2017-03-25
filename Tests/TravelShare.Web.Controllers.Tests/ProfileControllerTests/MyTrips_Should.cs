namespace TravelShare.Web.Controllers.Tests.ProfileControllerTests
{
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.Infrastructure.Mapping;

    [TestFixture]
    public class MyTrips_Should
    {
        [Test]
        public void CallMyTripsFromUserService_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedUserService = new Mock<IUserService>();

            var controller = new ProfileController(mockedUserService.Object);
            controller.GetUserId = () => "IdOfUser";

            // Act
            controller.MyTrips(It.IsAny<int>());

            // Assert
            mockedUserService.Verify(x => x.MyTrips(controller.GetUserId(),It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallMyTripsPageCountFromUserService_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var mockedUserService = new Mock<IUserService>();

            var controller = new ProfileController(mockedUserService.Object);
            controller.GetUserId = () => "IdOfUser";
            // Act
            controller.MyTrips(It.IsAny<int>());

            // Assert
            mockedUserService.Verify(x => x.MyTripsPageCount(controller.GetUserId(),It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ShouldSetTempDataToMyTripsPageCountFromUserService_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var userId = "IdOfUser";
            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.MyTripsPageCount(userId,It.IsAny<int>())).Returns(5).Verifiable();

            var controller = new ProfileController(mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.MyTrips(It.IsAny<int>());

            // Assert
            Assert.AreEqual(controller.TempData["pageCount"], 5);
        }

        [Test]
        public void ShouldSetTempDataToCorrectPage_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var userId = "IdOfUser";
            var mockedUserService = new Mock<IUserService>();

            var controller = new ProfileController(mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act
            controller.MyTrips(5);

            // Assert
            Assert.AreEqual(controller.TempData["page"], 5);
        }

        [Test]
        public void ShouldRenderDefaultView()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var userId = "IdOfUser";
            var mockedUserService = new Mock<IUserService>();

            var controller = new ProfileController(mockedUserService.Object);
            controller.GetUserId = () => userId;

            // Act & Assert
            controller.WithCallTo(x => x.MyTrips(5)).ShouldRenderDefaultView();
        }
    }
}

namespace TravelShare.Web.Controllers.Tests.ProfileControllerTests
{
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.Infrastructure.Mapping;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class MyTrips_Should
    {
        [Test]
        public void CallMyTripsFromUserService_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);
            var userId = "userId";
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);
            var controller = new ProfileController(mockedUserService.Object, mockAuthProvider.Object);


            // Act
            controller.MyTrips(It.IsAny<int>());

            // Assert
            mockedUserService.Verify(x => x.MyTrips(userId, It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallMyTripsPageCountFromUserService_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);

            var userId = "userId";
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);
            var controller = new ProfileController(mockedUserService.Object, mockAuthProvider.Object);

            // Act
            controller.MyTrips(It.IsAny<int>());

            // Assert
            mockedUserService.Verify(x => x.MyTripsPageCount(userId,It.IsAny<int>()), Times.Once);
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
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);

            var controller = new ProfileController(mockedUserService.Object, mockAuthProvider.Object);

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
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);

            var controller = new ProfileController(mockedUserService.Object, mockAuthProvider.Object);

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
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);

            var controller = new ProfileController(mockedUserService.Object, mockAuthProvider.Object);

            // Act & Assert
            controller.WithCallTo(x => x.MyTrips(5)).ShouldRenderDefaultView();
        }

        [Test]
        public void CallCurrentUserIdFromAuthProvider_WhenInvoked()
        {
            // Arrange
            var automap = new AutoMapperConfig();
            automap.Execute(typeof(TripController).Assembly);
            var userId = "userId";
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var controller = new ProfileController(mockedUserService.Object, mockAuthProvider.Object);

            // Act
            controller.MyTrips(It.IsAny<int>());

            // Assert
            mockAuthProvider.Verify(x => x.CurrentUserId, Times.Once);
        }
    }
}

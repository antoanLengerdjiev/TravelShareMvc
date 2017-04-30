namespace TravelShare.Web.Controllers.Tests.ProfileControllerTests
{
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.Mappings;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class MyTripsAsDriver_Should
    {
        [Test]
        public void CallMyTripsAsDriverFromTripService_WhenInvoked()
        {
            // Arrange
            var userId = "userId";
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);
            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new ProfileController(mockedUserService.Object, mockedTripService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.MyTripsAsDriver(It.IsAny<int>());

            // Assert
            mockedTripService.Verify(x => x.MyTripsAsDriver(userId, It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallMyTripsAsDriverPageCountFromTripService_WhenInvoked()
        {
            // Arrange
            var userId = "userId";
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);
            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new ProfileController(mockedUserService.Object, mockedTripService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.MyTripsAsDriver(It.IsAny<int>());

            // Assert
            mockedTripService.Verify(x => x.MyTripsAsDriverPageCount(userId, It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ShouldSetTempDataToMyTripsAsDriverPageCountFromTripService_WhenInvoked()
        {
            // Arrange
            var userId = "IdOfUser";
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();
            mockedTripService.Setup(x => x.MyTripsAsDriverPageCount(userId, It.IsAny<int>())).Returns(5).Verifiable();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);

            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new ProfileController(mockedUserService.Object, mockedTripService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.MyTripsAsDriver(It.IsAny<int>());

            // Assert
            Assert.AreEqual(controller.TempData["pageCount"], 5);
        }

        [Test]
        public void ShouldSetTempDataToCorrectPage_WhenInvoked()
        {
            // Arrange
            var userId = "IdOfUser";
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);
            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new ProfileController(mockedUserService.Object, mockedTripService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.MyTripsAsDriver(5);

            // Assert
            Assert.AreEqual(controller.TempData["page"], 5);
        }

        [Test]
        public void ShouldRenderDefaultView()
        {
            // Arrange
            var userId = "IdOfUser";
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            mockAuthProvider.Setup(x => x.CurrentUserId).Returns(userId);

            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new ProfileController(mockedUserService.Object, mockedTripService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act & Assert
            controller.WithCallTo(x => x.MyTripsAsDriver(5)).ShouldRenderDefaultView();
        }

        [Test]
        public void CallCurrentUserIdFromAuthProvider_WhenInvoked()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedTripService = new Mock<ITripService>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new ProfileController(mockedUserService.Object, mockedTripService.Object, mockAuthProvider.Object, mockMapperProvider.Object);

            // Act
            controller.MyTripsAsDriver(It.IsAny<int>());

            // Assert
            mockAuthProvider.Verify(x => x.CurrentUserId, Times.Once);
        }
    }
}

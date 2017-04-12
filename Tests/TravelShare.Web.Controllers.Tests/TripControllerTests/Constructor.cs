namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System;
    using Data.Common.Contracts;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserServicerIsPassed()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
               mockedTripService.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "User Service cannot ben null.";
            var mockedTripService = new Mock<ITripService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new TripController(
              mockedTripService.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullTripServiceIsPassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    null, mockedUserService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullTripServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Trip Service cannot ben null.";
            var mockedUserService = new Mock<IUserService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    null, mockedUserService.Object));

            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullParatertersArePassed()
        {
            // Arrange

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    null,
                    null));
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new TripController(
                    mockedTripService.Object,mockedUserService.Object));
        }

    }
}

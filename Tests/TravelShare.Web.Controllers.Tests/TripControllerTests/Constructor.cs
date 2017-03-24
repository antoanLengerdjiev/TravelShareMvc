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
        public void ShouldThrowArgumentNullException_WhenNullApplicationDataProviderIsPassed()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    null,
               mockedTripService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullApplicationDateProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Data provider cannot be null.";
            var mockedTripService = new Mock<ITripService>();


            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new TripController(
                   null,
              mockedTripService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullTripServiceIsPassed()
        {
            // Arrange
            var mockedDataProvider = new Mock<IApplicationData>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    mockedDataProvider.Object,
                    null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullTripServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Trip Service cannot ben null.";
            var mockedDataProvider = new Mock<IApplicationData>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    mockedDataProvider.Object,
                    null));

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
            var mockedDataProvider = new Mock<IApplicationData>();
            var mockedTripService = new Mock<ITripService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new TripController(
                    mockedDataProvider.Object,
                    mockedTripService.Object));
        }

    }
}

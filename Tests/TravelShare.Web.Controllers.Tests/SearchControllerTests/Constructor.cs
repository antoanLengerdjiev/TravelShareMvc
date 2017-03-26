namespace TravelShare.Web.Controllers.Tests.SearchControllerTests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullTripServiceIsPassed()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new SearchController(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullTripServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Trip Service cannot ben null.";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new SearchController(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenNotNullTripServiceIsPassed()
        {
            // Arrange
            var mockTripService = new Mock<ITripService>();

            // Act & Assert
            Assert.DoesNotThrow(() =>
                new SearchController(mockTripService.Object));
        }
    }
}

namespace TravelShare.Web.Controllers.Tests.SearchControllerTests
{
    using System;
    using Mappings;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullTripServiceIsPassed()
        {
            // Arrange
            var mockMapperProvider = new Mock<IMapperProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new SearchController(null, mockMapperProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullTripServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Trip Service cannot ben null.";
            var mockMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new SearchController(null, mockMapperProvider.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var mockTripService = new Mock<ITripService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new SearchController(mockTripService.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Mapper provider cannot be null.";
            var mockTripService = new Mock<ITripService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new SearchController(mockTripService.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenNotNullArgumentsArePassed()
        {
            // Arrange
            var mockTripService = new Mock<ITripService>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            // Act & Assert
            Assert.DoesNotThrow(() =>
                new SearchController(mockTripService.Object, mockMapperProvider.Object));
        }
    }
}

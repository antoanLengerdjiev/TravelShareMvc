namespace TravelShare.Web.Controllers.Tests.HomeControllerTests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using Data.Common.Contracts;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullApplicationDataProviderIsPassed()
        {
            // Arrange

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new HomeController(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullApplicationDateProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Data provider cannot be null.";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new HomeController(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }


        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedDataProvider = new Mock<IApplicationData>();


            // Act and Assert
            Assert.DoesNotThrow(() =>
                new HomeController(mockedDataProvider.Object));
        }
    }
}

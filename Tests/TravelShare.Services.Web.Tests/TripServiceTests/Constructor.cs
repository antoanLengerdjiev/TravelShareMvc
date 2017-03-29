namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;
    using Data.Common.Contracts;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Models;

    [TestFixture]
    public class Constructor
    {

        public void ShouldThrowArgumentNullException_WhenNullParameterIsPassed()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullApplicationDateProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Trip repository cannot be null.";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripService(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }


        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();


            // Act and Assert
            Assert.DoesNotThrow(() =>
                new TripService(mockedTripRepository.Object));
        }
    }
}

namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.Infrastructure.Mapping;

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
            //mockedData.Setup(x => x.Trips.All()).Verifiable();

            var controller = new TripController(mockedData.Object, mockedTripService.Object);

            // Act
            controller.All(5);

            // Assert
            Assert.AreEqual(controller.TempData["page"], 5);
        }
    }
}

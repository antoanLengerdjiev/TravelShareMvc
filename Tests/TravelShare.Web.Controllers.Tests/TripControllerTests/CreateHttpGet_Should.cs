namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Services.Data.Common.Contracts;

    [TestFixture]
    public class CreateHttpGet_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedData = new Mock<IApplicationData>();
            var mockedTripService = new Mock<ITripService>();
            var controller = new TripController(mockedData.Object, mockedTripService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Create()).ShouldRenderDefaultView();
        }
    }
}

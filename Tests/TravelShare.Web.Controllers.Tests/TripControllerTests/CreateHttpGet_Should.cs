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
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class CreateHttpGet_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockAuthProvider.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Create()).ShouldRenderDefaultView();
        }
    }
}

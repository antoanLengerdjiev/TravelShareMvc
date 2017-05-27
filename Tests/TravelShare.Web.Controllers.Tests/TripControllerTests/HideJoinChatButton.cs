using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelShare.Services.Data.Common.Contracts;
using TravelShare.Web.Mappings;
using TravelShareMvc.Providers.Contracts;

namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    [TestFixture]
    public class HideJoinChatButton
    {
        [Test]
        public void ShouldRenderView()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();

            var mockedUserService = new Mock<IUserService>();

            var mockedMessageService = new Mock<IMessageService>();

            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var mockImapperProvider = new Mock<IMapperProvider>();

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockImapperProvider.Object);

            // Act & Assert
            controller.WithCallTo(x => x.HideJoinChatButton()).ShouldRenderPartialView("NoChatButton");
        }
    }
}

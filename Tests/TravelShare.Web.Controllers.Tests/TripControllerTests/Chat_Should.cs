using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelShare.Data.Models;
using TravelShare.Services.Data.Common.Contracts;
using TravelShare.Web.Mappings;
using TravelShare.Web.ViewModels.Trips;
using TravelShareMvc.Providers.Contracts;

namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    [TestFixture]
    public class Chat_Should
    {
        [Test]
        public void CallsGetOlderMessagesFromChatService_WhenInvoked()
        {
            // Arrange

            var mockedTripService = new Mock<ITripService>();

            var mockedUserService = new Mock<IUserService>();

            var mockedChatService = new Mock<IChatService>();

            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var mockImapperProvider = new Mock<IMapperProvider>();
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockImapperProvider.Object);

            // Act
            controller.Chat(3);

            // Assert
            mockedChatService.Verify(x => x.GetOlderMessages(3, 0, 5), Times.Once);
        }

        [Test]
        public void CallsMapMethodFromMapper_WhenInvoked()
        {
            // Arrange
            var msg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };
            var messageColleciton = new List<Message>() { msg, msg, msg };
            var mockedTripService = new Mock<ITripService>();

            var mockedUserService = new Mock<IUserService>();

            var mockedChatService = new Mock<IChatService>();
            mockedChatService.Setup(x => x.GetOlderMessages(3, 0, 5)).Returns(messageColleciton);

            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var mockImapperProvider = new Mock<IMapperProvider>();
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockImapperProvider.Object);

            // Act
            controller.Chat(3);

            // Assert
            mockImapperProvider.Verify(x => x.Map<IEnumerable<MessageViewModel>>(messageColleciton), Times.Once);
        }

        [Test]
        public void ShouldRenderChatViewWithCorrectModel()
        {
            // Arrange
            var msg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };
            var msgViewModel = new MessageViewModel { Content = "Info" };
            var messageColleciton = new List<Message>() { msg, msg, msg };
            var msgViewModelCollection = new List<MessageViewModel>() { msgViewModel, msgViewModel, msgViewModel };
            var mockedTripService = new Mock<ITripService>();

            var mockedUserService = new Mock<IUserService>();

            var mockedChatService = new Mock<IChatService>();
            mockedChatService.Setup(x => x.GetOlderMessages(3, 0, 5)).Returns(messageColleciton);

            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var mockImapperProvider = new Mock<IMapperProvider>();
            mockImapperProvider.Setup(x => x.Map<IEnumerable<MessageViewModel>>(messageColleciton)).Returns(msgViewModelCollection);

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockImapperProvider.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Chat(3)).ShouldRenderDefaultPartialView().WithModel<IEnumerable<MessageViewModel>>(model =>
            {
                CollectionAssert.AreEqual(model, msgViewModelCollection);
            });
        }
    }
}

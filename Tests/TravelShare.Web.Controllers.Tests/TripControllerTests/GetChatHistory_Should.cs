namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System.Collections.Generic;
    using Common;
    using Data.Models;
    using Mappings;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using TestStack.FluentMVCTesting;
    using TravelShareMvc.Providers.Contracts;
    using ViewModels.Trips;

    [TestFixture]
    public class GetChatHistory_Should
    {
        [Test]
        public void CallsGetOlderMessagesFromMessageService_WhenInvoked()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();

            var mockedUserService = new Mock<IUserService>();

            var mockedChatService = new Mock<IChatService>();

            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var mockImapperProvider = new Mock<IMapperProvider>();
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockImapperProvider.Object);

            // Act
            controller.GetChatHistory(3, 0);

            // Assert
            mockedChatService.Verify(x => x.GetOlderMessages(3, 0, GlobalConstants.MessagePerTake), Times.Once);
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
            mockedChatService.Setup(x => x.GetOlderMessages(3, 0, GlobalConstants.MessagePerTake)).Returns(messageColleciton);

            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var mockImapperProvider = new Mock<IMapperProvider>();
            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockImapperProvider.Object);

            // Act
            controller.GetChatHistory(3, 0);

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
            mockedChatService.Setup(x => x.GetOlderMessages(3, 0, GlobalConstants.MessagePerTake)).Returns(messageColleciton);

            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            var mockImapperProvider = new Mock<IMapperProvider>();
            mockImapperProvider.Setup(x => x.Map<IEnumerable<MessageViewModel>>(messageColleciton)).Returns(msgViewModelCollection);

            var controller = new TripController(mockedTripService.Object, mockedUserService.Object, mockedChatService.Object, mockAuthProvider.Object, mockImapperProvider.Object);

            // Act & Assert
            controller.WithCallTo(x => x.GetChatHistory(3, 0)).ShouldRenderPartialView("ChatHistoryPartial").WithModel<IEnumerable<MessageViewModel>>(model =>
            {
                CollectionAssert.AreEqual(model, msgViewModelCollection);
            });
        }
    }
}

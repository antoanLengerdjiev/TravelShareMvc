namespace TravelShare.Services.Web.Tests.ChatServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    [TestFixture]
    public class GetOlderChatMessages_Should
    {
        [Test]
        public void CallsGetByIdMethodFromChatRepository_WhenInvoked()
        {
            // Arrange
            var firstMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 3) };
            var secondMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 4) };
            var thirdMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 5) };
            var fourthMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 6) };

            var chat = new Chat { Id = 3, Messages = new List<Message> { firstMsg, secondMsg, thirdMsg, fourthMsg } };

            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();
            mockedChatRepository.Setup(x => x.GetById(3)).Returns(chat);

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var chatService = new ChatService(mockedChatRepository.Object, dbSaveChanges.Object);

            // Act
            chatService.GetOlderMessages(3, 5, 3);

            // Assert
            mockedChatRepository.Verify(x => x.GetById(3), Times.Once);
        }

        [Test]
        public void ReturnInstanceOfIEnumerableOfMessages()
        {
            // Arrange
            var firstMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 3) };
            var secondMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 4) };
            var thirdMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 5) };
            var fourthMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 6) };

            var chat = new Chat { Id = 3, Messages = new List<Message> { firstMsg, secondMsg, thirdMsg, fourthMsg } };

            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();
            mockedChatRepository.Setup(x => x.GetById(3)).Returns(chat);

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var chatService = new ChatService(mockedChatRepository.Object, dbSaveChanges.Object);

            // Act
            var result = chatService.GetOlderMessages(3, 0, 3);

            // Assert
            Assert.IsInstanceOf<IEnumerable<Message>>(result);
        }


        [Test]
        public void SkipCorrectNumberOfMessages()
        {
            // Arrange
            var firstMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 3) };
            var secondMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 4) };
            var thirdMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 5) };
            var fourthMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 6) };

            var chat = new Chat { Id = 3, Messages = new List<Message> { firstMsg, secondMsg, thirdMsg, fourthMsg } };

            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();
            mockedChatRepository.Setup(x => x.GetById(3)).Returns(chat);

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var chatService = new ChatService(mockedChatRepository.Object, dbSaveChanges.Object);

            var expectedResult = new List<Message>() { firstMsg, secondMsg, thirdMsg }.AsEnumerable();

            // Act
            var result = chatService.GetOlderMessages(3, 1, 3);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TakeCorrectNumberOfMessages()
        {
            // Arrange
            var firstMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 3) };
            var secondMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 4) };
            var thirdMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 5) };
            var fourthMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 6) };

            var chat = new Chat { Id = 3, Messages = new List<Message> { firstMsg, secondMsg, thirdMsg, fourthMsg } };

            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();
            mockedChatRepository.Setup(x => x.GetById(3)).Returns(chat);

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var chatService = new ChatService(mockedChatRepository.Object, dbSaveChanges.Object);

            var expectedResult = new List<Message>() { fourthMsg }.AsEnumerable();

            // Act
            var result = chatService.GetOlderMessages(3, 0, 1);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void SkipNewerMessagesTakeOlderMessages()
        {
            // Arrange
            var firstMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 3) };
            var secondMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 4) };
            var thirdMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 5) };
            var fourthMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info", CreatedOn = new DateTime(1994, 2, 6) };

            var chat = new Chat { Id = 3, Messages = new List<Message> { firstMsg, secondMsg, thirdMsg, fourthMsg } };

            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();
            mockedChatRepository.Setup(x => x.GetById(3)).Returns(chat);

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var chatService = new ChatService(mockedChatRepository.Object, dbSaveChanges.Object);
            var expectedResult = new List<Message>() { firstMsg, secondMsg }.AsEnumerable();

            // Act
            var result = chatService.GetOlderMessages(3, 2, 2);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }
    }
}

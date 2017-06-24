using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;
using TravelShare.Data.Models;
using TravelShare.Services.Data;

namespace TravelShare.Services.Web.Tests.MessageServiceTests
{
    [TestFixture]
    public class GetOlderMessages_Should
    {
        [Test]
        public void CallsAllMethodFromMessageRepository_WhenInvoked()
        {
            // Arrange
            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var msgService = new MessageService(mockedMsgRepository.Object, dbSaveChanges.Object);

            // Act
            msgService.GetOlderMessages(3, 5, 3);

            // Assert
            mockedMsgRepository.Verify(x => x.All(), Times.Once);
        }

        [Test]
        public void ReturnInstanceOfIEnumerableOfMessages()
        {
            // Arrange
            var msg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };

            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();
            mockedMsgRepository.Setup(x => x.All()).Returns(new List<Message>() { msg }.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var msgService = new MessageService(mockedMsgRepository.Object, dbSaveChanges.Object);

            // Act
            var result = msgService.GetOlderMessages(3, 0, 3);

            // Assert
            Assert.IsInstanceOf<IEnumerable<Message>>(result);
        }

        [Test]
        public void ReturnMessagesWithTheSameTripId()
        {
            // Arrange
            var firstMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };
            var secondMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };
            var thirdMsg = new Message { SenderId = "gg", ChatId = 2, Content = "Info" };
            var fourthMsg = new Message { SenderId = "gg", ChatId = 2, Content = "Info" };

            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();
            mockedMsgRepository.Setup(x => x.All()).Returns(new List<Message>() { firstMsg, secondMsg, thirdMsg, fourthMsg }.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var msgService = new MessageService(mockedMsgRepository.Object, dbSaveChanges.Object);
            var expectedResult = new List<Message>() { firstMsg, secondMsg }.AsEnumerable();

            // Act
            var result = msgService.GetOlderMessages(3, 0, 3);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void SkipCorrectNumberOfMessages()
        {
            // Arrange
            var firstMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };
            var secondMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };
            var thirdMsg = new Message { SenderId = "gg", ChatId = 2, Content = "Info" };
            var fourthMsg = new Message { SenderId = "gg", ChatId = 2, Content = "Info" };

            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();
            mockedMsgRepository.Setup(x => x.All()).Returns(new List<Message>() { firstMsg, secondMsg, thirdMsg, fourthMsg }.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var msgService = new MessageService(mockedMsgRepository.Object, dbSaveChanges.Object);
            var expectedResult = new List<Message>() { secondMsg }.AsEnumerable();

            // Act
            var result = msgService.GetOlderMessages(3, 1, 3);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TakeCorrectNumberOfMessages()
        {
            // Arrange
            var firstMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };
            var secondMsg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };
            var thirdMsg = new Message { SenderId = "gg", ChatId = 2, Content = "Info" };
            var fourthMsg = new Message { SenderId = "gg", ChatId = 2, Content = "Info" };

            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();
            mockedMsgRepository.Setup(x => x.All()).Returns(new List<Message>() { firstMsg, secondMsg, thirdMsg, fourthMsg }.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var msgService = new MessageService(mockedMsgRepository.Object, dbSaveChanges.Object);
            var expectedResult = new List<Message>() { firstMsg }.AsEnumerable();

            // Act
            var result = msgService.GetOlderMessages(3, 0, 1);

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

            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();
            mockedMsgRepository.Setup(x => x.All()).Returns(new List<Message>() { firstMsg, secondMsg, thirdMsg, fourthMsg }.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var msgService = new MessageService(mockedMsgRepository.Object, dbSaveChanges.Object);
            var expectedResult = new List<Message>() { firstMsg, secondMsg }.AsEnumerable();

            // Act
            var result = msgService.GetOlderMessages(3, 2, 2);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }
    }
}

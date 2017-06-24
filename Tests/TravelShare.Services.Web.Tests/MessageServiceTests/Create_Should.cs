using Moq;
using NUnit.Framework;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;
using TravelShare.Data.Models;
using TravelShare.Services.Data;

namespace TravelShare.Services.Web.Tests.MessageServiceTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void CallCreateMethodFromMsgRepository_WhenInvoked()
        {
            // Arrange
            var msg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };
            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var msgService = new MessageService(mockedMsgRepository.Object, dbSaveChanges.Object);

            // Act
            msgService.Create(msg);

            // Assert
            mockedMsgRepository.Verify(x => x.Add(msg), Times.Once);
        }

        [Test]
        public void CallSaveChangesMethodFromDbSaveChanges_WhenInvoked()
        {
            // Arrange
            var msg = new Message { SenderId = "gg", ChatId = 3, Content = "Info" };
            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var msgService = new MessageService(mockedMsgRepository.Object, dbSaveChanges.Object);

            // Act
            msgService.Create(msg);

            // Assert
            dbSaveChanges.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}

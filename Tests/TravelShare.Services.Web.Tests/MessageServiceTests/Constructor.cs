namespace TravelShare.Services.Web.Tests.MessageServiceTests
{
    using System;
    using Common;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenMessageRepositoryIsNull()
        {
            // Arrange
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MessageService(null, dbSaveChanges.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullMessageRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.MessageRepositoryNullExceptionMessage;
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new MessageService(null, dbSaveChanges.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenDbSaveChangesIsNull()
        {
            // Arrange
            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MessageService(mockedMsgRepository.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDbSaveChangesIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.DbContextSaveChangesNullExceptionMessage;
            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new MessageService(mockedMsgRepository.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }


        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedMsgRepository = new Mock<IEfDbRepository<Message>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new MessageService(mockedMsgRepository.Object, dbSaveChanges.Object));
        }
    }
}

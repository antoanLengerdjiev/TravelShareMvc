namespace TravelShare.Services.Web.Tests.ChatServiceTests
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
        public void ShouldThrowArgumentNullException_WhenChatRepositoryIsNull()
        {
            // Arrange
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ChatService(null, dbSaveChanges.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullChatRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.ChatRepositoryNullExceptionMessage;
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ChatService(null, dbSaveChanges.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenDbSaveChangesIsNull()
        {
            // Arrange
            var mockeChatRepository = new Mock<IEfDbRepository<Chat>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ChatService(mockeChatRepository.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDbSaveChangesIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.DbContextSaveChangesNullExceptionMessage;
            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ChatService(mockedChatRepository.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new ChatService(mockedChatRepository.Object, dbSaveChanges.Object));
        }
    }
}

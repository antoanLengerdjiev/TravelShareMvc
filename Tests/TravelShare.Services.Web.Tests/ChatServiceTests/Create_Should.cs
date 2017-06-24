namespace TravelShare.Services.Web.Tests.ChatServiceTests
{
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void CallsAddMethodFromChatRepository()
        {
            // Arrange
            var trip = new Trip { Id = 2 };

            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var chatService = new ChatService(mockedChatRepository.Object, dbSaveChanges.Object);

            // Act
            chatService.Create(trip);

            // Assert
            mockedChatRepository.Verify(x => x.Add(It.IsAny<Chat>()), Times.Once);
        }

        [Test]
        public void CallsSaveChangesFromChatRepository()
        {
            // Arrange
            var trip = new Trip { Id = 2 };

            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var chatService = new ChatService(mockedChatRepository.Object, dbSaveChanges.Object);

            // Act
            chatService.Create(trip);

            // Assert
           dbSaveChanges.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void ReturnsInstanceOfChat()
        {
            // Arrange
            var trip = new Trip { Id = 2 };

            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var chatService = new ChatService(mockedChatRepository.Object, dbSaveChanges.Object);

            // Act
            var result = chatService.Create(trip);

            // Assert
            Assert.IsInstanceOf<Chat>(result);
        }

        [Test]
        public void ReturnsChatWithCorrectTripAndTripId()
        {
            // Arrange
            var trip = new Trip { Id = 2 };

            var mockedChatRepository = new Mock<IEfDbRepository<Chat>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var chatService = new ChatService(mockedChatRepository.Object, dbSaveChanges.Object);

            // Act
            var result = chatService.Create(trip);

            // Assert
            Assert.AreEqual(result.TripId, trip.Id);
            Assert.AreEqual(result.Trip, trip);
        }
    }
}

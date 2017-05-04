namespace TravelShare.Services.Web.Tests.NewsServiceTests
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
        public void CallCreateMethodFromNewsRepository_WhenInvoked()
        {
            // Arrange
            var news = new News { Title = "Title", Content = "Info" };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);

            // Act
            newsService.Create(news);

            // Assert
            mockedNewsRepository.Verify(x => x.Add(news), Times.Once);
        }

        [Test]
        public void CallSaveChangesMethodFromDbSaveChanges_WhenInvoked()
        {
            // Arrange
            var news = new News { Title = "Title", Content = "Info" };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);

            // Act
            newsService.Create(news);

            // Assert
            dbSaveChanges.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}

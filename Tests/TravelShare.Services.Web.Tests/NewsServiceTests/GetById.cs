using Moq;
using NUnit.Framework;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;
using TravelShare.Data.Models;
using TravelShare.Services.Data;

namespace TravelShare.Services.Web.Tests.NewsServiceTests
{
    [TestFixture]
    public class GetById
    {
        [Test]
        public void CallGetByIdFromNewsRepository()
        {
            // Arrange
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, mockSaveChanges.Object);

            // Act
            newsService.GetById(It.IsAny<int>());

            mockedNewsRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ReturnTypeOfNews()
        {
            // Arrange
            var news = new News();
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(news);
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, mockSaveChanges.Object);

            // Act
            var result = newsService.GetById(It.IsAny<int>());

            Assert.IsInstanceOf<News>(result);
        }

        [Test]
        public void ReturnsCorrectNews()
        {
            // Arrange
            var news = new News() { Id = 5 };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(news);
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var newsService = new NewsService(mockedNewsRepository.Object, mockSaveChanges.Object);

            // Act
            var result = newsService.GetById(5);

            Assert.AreSame(news, result);
        }
    }
}

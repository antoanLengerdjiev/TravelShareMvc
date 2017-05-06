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

namespace TravelShare.Services.Web.Tests.NewsServiceTests
{
    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void CallDeleteFromNewsService_WhenInvoked()
        {
            // Arrange
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, mockSaveChanges.Object);

            // Act
            newsService.Delete(It.IsAny<News>());

            // Assert
            mockedNewsRepository.Verify(x => x.Delete(It.IsAny<News>()), Times.Once);
        }

        [Test]
        public void CallDeleteFromNewsServiceWithCorrectNews_WhenInvoked()
        {
            // Arrange
            var news = new News { Id = 5, Title = "Title", Content = "Content" };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, mockSaveChanges.Object);

            // Act
            newsService.Delete(news);

            // Assert
            mockedNewsRepository.Verify(x => x.Delete(news), Times.Once);
        }


        [Test]
        public void CallSaveChangeFromDbSaveChanges_WhenInvoked()
        {
            // Arrange
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, mockSaveChanges.Object);

            // Act
            newsService.Delete(It.IsAny<News>());

            // Assert
            mockSaveChanges.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}

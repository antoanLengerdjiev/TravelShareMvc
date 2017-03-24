namespace TravelShare.Web.Controllers.Tests.HomeControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    [TestFixture]
    public class Index
    {
        [Test]
        public void ShouldCallDataProviderNewsAllMethod()
        {
            // Arrange
            var mockData = new Mock<IApplicationData>();
            mockData.Setup(x => x.News.All()).Returns(new List<News>().AsQueryable).Verifiable();

            var homeController = new HomeController(mockData.Object);

            // Act
            var result = homeController.Index();

            // Assert
            mockData.Verify(x => x.News.All(), Times.Once);
        }
    }
}

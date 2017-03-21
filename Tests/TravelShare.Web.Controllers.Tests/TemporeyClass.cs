namespace TravelShare.Web.Controllers.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Common.Contracts;
    using Data.Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class TemporeyClass
    {
        [Test]
        public void Test()
        {
            //var mockContext = new Mock<IApplicationDbContext>();
            var mockData = new Mock<IApplicationData>();
            mockData.Setup(x => x.News.All()).Returns(new List<News>().AsQueryable).Verifiable();
           // mockData.Setup(x => x.Users.All()).Returns(new Queryable().All);
            // Arrange && Act
            var homeController = new HomeController(mockData.Object);

            var result = homeController.Index();
            mockData.Verify(x => x.News.All(), Times.Once);
            //Assert.IsTrue(true);
        }

    }
}

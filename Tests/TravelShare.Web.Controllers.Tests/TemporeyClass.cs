namespace TravelShare.Web.Controllers.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class TemporeyClass
    {
        [Test]
        public void Test()
        {
            //Arrange && Act
            var homeController = new HomeController();

            //Assert
            Assert.IsNotNull(homeController);
        }

    }
}

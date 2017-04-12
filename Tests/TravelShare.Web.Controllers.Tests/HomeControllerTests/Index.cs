namespace TravelShare.Web.Controllers.Tests.HomeControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    [TestFixture]
    public class Index
    {

        [Test]
        public void RenderDefaultView()
        {
            // Arrange

            var homeController = new HomeController();

            // Act & Assert
            homeController.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }
    }
}

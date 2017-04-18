using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelShareMvc.Providers.Contracts;

namespace TravelShare.Web.Controllers.Tests.AccountControllerTests
{
    [TestFixture]
    public class LoginGet_Should
    {
        [Test]
        public void ShouldSetReturnUrlToAccountControllersViewBag()
        {
            // Arrange
            var returnUrl = "/Home/Index";
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act
            var result = accountController.Login(returnUrl);

            // Assert
            Assert.AreEqual(returnUrl, accountController.ViewBag.ReturnUrl);
        }

        [Test]
        public void ShouldReturnViewResult()
        {
            // Arrange
            var returnUrl = "/Home/Index";
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act and Assert
            accountController.WithCallTo(ac => ac.Login(returnUrl))
                .ShouldRenderDefaultView();
        }
    }
}

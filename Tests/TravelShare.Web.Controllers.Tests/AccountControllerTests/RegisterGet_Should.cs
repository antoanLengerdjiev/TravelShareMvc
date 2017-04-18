namespace TravelShare.Web.Controllers.Tests.AccountControllerTests
{
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class RegisterGet_Should
    {
        [Test]
        public void ReturnViewResult()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act and Assert
            accountController.WithCallTo(ac => ac.Register())
                .ShouldRenderDefaultView();
        }
    }
}

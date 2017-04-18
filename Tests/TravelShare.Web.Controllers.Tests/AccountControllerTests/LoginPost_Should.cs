using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelShare.Web.ViewModels.Account;
using TravelShareMvc.Providers.Contracts;

namespace TravelShare.Web.Controllers.Tests.AccountControllerTests
{
    [TestFixture]
    public class LoginPost_Should
    {
            [TestCase("elon@tesla.com", "password")]
            [TestCase("elon@spacex.com", "drowssap")]
            public void ReturnViewWithModel_WhenModelStateOfControllerIsNotValid(
                string email,
                string password)
            {
                // Arrange
                var loginModel = new LoginViewModel()
                {
                    Email = email,
                    Password = password
                };

                var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

                var accountController = new AccountController(mockedAuthenticationProvider.Object);
                accountController.ModelState.AddModelError("key", "message");

                // Act and Assert
                accountController.WithCallTo(ac => ac.Login(loginModel, string.Empty))
                    .ShouldRenderDefaultView()
                    .WithModel<LoginViewModel>(model => Assert.AreSame(loginModel, model));
            }

        [TestCase("elon@tesla.com", "password", "home")]
        [TestCase("elon@spacex.com", "drowssap", "nothome")]
        public void CallSignInWithPasswordOfAuthenticationProvider_WhenModelStateOfControllerIsValid(
            string email,
            string password,
            string returnUrl)
        {
            // Arrange
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Verifiable();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act
            var result = accountController.Login(loginModel, returnUrl);

            // Assert
            mockedAuthenticationProvider.Verify(
                ap =>
                ap.SignInWithPassword(email, password, false, It.IsAny<bool>()), Times.Once);
        }

        [TestCase("elon@tesla.com", "password")]
        [TestCase("elon@spacex.com", "drowssap")]
        public void ReturnRedirectHomeIndex_WhenResultFromSignInWithPasswordIsSuccess(
            string email,
            string password)
        {
            // Arrange
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password
            };
            var returnUrl = string.Empty;
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(SignInStatus.Success);

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act and Assert
            accountController.WithCallTo(ac => ac.Login(loginModel, returnUrl))
                .ShouldRedirectTo<HomeController>(x => new HomeController().Index());
        }

        [TestCase("elon@tesla.com", "password")]
        [TestCase("elon@spacex.com", "drowssap")]
        public void ReturnViewResultWithLockoutName_WhenResultFromSignInWithPasswordIsLockout(
            string email,
            string password)
        {
            // Arrange
            var expectedLockOutName = "Lockout";
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password
            };
            var returnUrl = string.Empty;
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(SignInStatus.LockedOut);

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act and Assert
            accountController.WithCallTo(ac => ac.Login(loginModel, returnUrl))
                .ShouldRenderView(expectedLockOutName);
        }

        [TestCase("elon@tesla.com", "password")]
        public void AddModelError_WhenResultFromSignInWithPasswordIsFailure(
            string email,
            string password)
        {
            // Arrange
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password
            };
            var returnUrl = string.Empty;
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act
            var result = accountController.Login(loginModel, returnUrl);

            // Assert
            Assert.IsFalse(accountController.ModelState.IsValid);
        }

        [TestCase("elon@tesla.com", "password")]
        [TestCase("elon@spacex.com", "drowssap")]
        public void ReturnViewResultWithModel_WhenResultFromSignInWithPasswordIsFailure(
            string email,
            string password)
        {
            // Arrange
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password
            };
            var returnUrl = string.Empty;
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act and Assert
            accountController.WithCallTo(ac => ac.Login(loginModel, string.Empty))
                         .ShouldRenderDefaultView()
                         .WithModel<LoginViewModel>(model => Assert.AreSame(loginModel, model));
        }
    }
}

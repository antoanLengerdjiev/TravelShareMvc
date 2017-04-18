namespace TravelShare.Web.Controllers.Tests.AccountControllerTests
{
    using System.Collections.Generic;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Web.ViewModels.Account;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class RegisterPost_Should
    {
        [TestCase("elon@tesla.com",  "password")]
        [TestCase("matt@faradayfuture.com", "password")]
        public void ReturnViewResultWithModel_WhenModelStateOfControllerIsNotValid(
            string email,
            string password)
        {
            // Arrange
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);
            accountController.ModelState.AddModelError("key", "message");

            // Act and Assert
            accountController.WithCallTo(ac => ac.Register(registerModel))
                .ShouldRenderDefaultView()
                .WithModel<RegisterViewModel>(model => Assert.AreEqual(registerModel, model));
        }

        [TestCase("elon@tesla.com", "password")]
        [TestCase("matt@faradayfuture.com", "password")]
        public void CallCreateUserMethodOfAuthenticationProvider_WhenModelStateOfControllerIsValid(
            string email,
            string password)
        {
            // Arrange
            var user = new ApplicationUser() { Email = email, UserName = email };
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()))
                .Returns(new IdentityResult())
                .Verifiable();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act
            accountController.Register(registerModel);

            // Assert
            mockedAuthenticationProvider.Verify(
                ap => ap.CreateUser(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()), Times.Once);
        }

        [TestCase("elon@tesla.com", "password")]
        [TestCase("matt@faradayfuture.com", "password")]
        public void AddErrors_WhenResultFromAuthenticationProviderIsNotSuccessful(
            string email,
            string password)
        {
            // Arrange
            var user = new ApplicationUser() { Email = email, UserName = email };
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()))
                .Returns(new IdentityResult(new List<string>() { "Error" }))
                .Verifiable();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act
            var result = accountController.Register(registerModel);

            // Assert
            Assert.AreEqual(1, accountController.ModelState.Count);
            Assert.IsFalse(accountController.ModelState.IsValid);
        }

        [TestCase("elon@tesla.com", "password")]
        [TestCase("matt@faradayfuture.com", "password")]
        public void CallSignInMethodOfAuthenticationProvider_WhenModelStateOfControllerIsValidAndWhenResultFromAuthenticationProviderIsSuccess(
                string email,
                string password)
        {
            // Arrange
            var user = new ApplicationUser() { Email = email, UserName = email };
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()))
                .Returns(IdentityResult.Success)
                .Verifiable();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act
            accountController.Register(registerModel);

            // Assert
            mockedAuthenticationProvider.Verify(
                ap => ap.SignIn(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [TestCase("elon@tesla.com", "password")]
        [TestCase("matt@faradayfuture.com", "password")]
        public void ReturnRedirectToIndexHomeController_WhenResultFromAuthenticationProviderIsSuccess(string email,
                string password)
        {
            // Arrange
            var user = new ApplicationUser() { Email = email, UserName = email };
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()))
                .Returns(IdentityResult.Success)
                .Verifiable();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act and Assert
            accountController.WithCallTo(ac => ac.Register(registerModel))
                .ShouldRedirectTo((HomeController hc) => hc.Index());
        }
    }
}

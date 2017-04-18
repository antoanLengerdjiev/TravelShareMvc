using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelShareMvc.Providers.Contracts;

namespace TravelShare.Web.Controllers.Tests.AccountControllerTests
{
    [TestFixture]
    public class LogOff_Should
    {
       [Test]
        public void CallsSignOutFromAuthProvider()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act
            accountController.LogOff();

            // Assert
            mockedAuthenticationProvider.Verify(x => x.SignOut(), Times.Once);
        }

        [Test]
        public void RedirectToIndexHomeController()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object);

            // Act && Assert
            // Act and Assert
            accountController.WithCallTo(ac => ac.LogOff())
                .ShouldRedirectTo((HomeController hc) => hc.Index());

        }
    }
}

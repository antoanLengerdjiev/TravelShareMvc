using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelShare.Data.Models;
using TravelShare.Services.Data.Common.Contracts;
using TravelShare.Web.Areas.Administration.Controllers;
using TravelShare.Web.Areas.Administration.Models;
using TravelShare.Web.Mappings;
using TravelShareMvc.Providers.Contracts;

namespace TravelShare.Web.Controllers.Tests.UserManagementControllerTests
{
    [TestFixture]
    public class UpdateStatus
    {
        [Test]
        public void ShouldCallGetUserByIdMethodOfUsersService()
        {
            // Arrange
            var id = "123";
            var userViewModel = new UserViewModel() { Id = id };
            var user = new ApplicationUser();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap =>
                ap.ChangeUserRole(It.IsAny<string>(), It.IsAny<string>()));
            mockedAuthenticationProvider.Setup(ap =>
                ap.UpdateSecurityStamp(It.IsAny<string>()));
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUserService>();
            mockedUsersService.Setup(cs => cs.GetById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            userManagementController.UpdateStatus(userViewModel);

            // Assert
            mockedUsersService.Verify(us => us.GetById(id), Times.Once);
        }

        [Test]
        public void ShouldCallChangeUserRoleMethodOfAuthenticationProvider()
        {
            // Arrange
            var id = "123";
            var role = "User";
            var userViewModel = new UserViewModel() { Role = role };
            var user = new ApplicationUser() { Id = id };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap =>
                ap.ChangeUserRole(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();
            mockedAuthenticationProvider.Setup(ap =>
                ap.UpdateSecurityStamp(It.IsAny<string>()));
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUserService>();
            mockedUsersService.Setup(cs => cs.GetById(It.IsAny<string>()))
                .Returns(user);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            userManagementController.UpdateStatus(userViewModel);

            // Assert
            mockedAuthenticationProvider.Verify(ap => ap.ChangeUserRole(id, role), Times.Once);
        }

        [Test]
        public void ShouldCallUpdateSecurityStampOfAuthenticationProvider()
        {
            // Arrange
            var id = "123";
            var userViewModel = new UserViewModel();
            var user = new ApplicationUser() { Id = id };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap =>
                ap.ChangeUserRole(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();
            mockedAuthenticationProvider.Setup(ap =>
                ap.UpdateSecurityStamp(It.IsAny<string>()));
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUserService>();
            mockedUsersService.Setup(cs => cs.GetById(It.IsAny<string>()))
                .Returns(user);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            userManagementController.UpdateStatus(userViewModel);

            // Assert
            mockedAuthenticationProvider.Verify(ap => ap.UpdateSecurityStamp(id), Times.Once);
        }

        [Test]
        public void ShouldRedirectToUserProfileView()
        {
            // Arrange
            var id = "123";
            var userViewModel = new UserViewModel();
            var user = new ApplicationUser() { Id = id };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap =>
                ap.ChangeUserRole(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();
            mockedAuthenticationProvider.Setup(ap =>
                ap.UpdateSecurityStamp(It.IsAny<string>()));
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUserService>();
            mockedUsersService.Setup(cs => cs.GetById(It.IsAny<string>()))
                .Returns(user);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act and Assert
            userManagementController.WithCallTo(umc => umc.UpdateStatus(userViewModel))
                .ShouldRedirectTo((UserManagementController umc) => umc.UserProfile(userViewModel, id));
        }
    }
}

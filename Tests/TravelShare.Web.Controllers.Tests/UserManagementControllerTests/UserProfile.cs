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
    public class UserProfile
    {

        [Test]
        public void ShouldCallGetUserByIdMethodOfUsersService()
        {
            // Arrange
            var id = "13";
            var user = new ApplicationUser() { Id = id };
            var userViewModel = new UserViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(id))
                .Returns(new List<string>() { "Administrator" });
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(m => m.Map<UserViewModel>(user)).Returns(new UserViewModel { Id = id }).Verifiable();
            var mockedUsersService = new Mock<IUserService>();
            mockedUsersService.Setup(us => us.GetById(id)).Returns(user)
                .Verifiable();

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act
            usersManagementController.UserProfile(userViewModel, id);

            // Assert
            mockedUsersService.Verify(us => us.GetById(id), Times.Once);
        }

        [Test]
        public void ShouldCallGetUserRolesMethodOfAuthenticationProvider()
        {
            // Arrange
            var id = "13";
            var userViewModel = new UserViewModel() { Id = id };
            var user = new ApplicationUser() { Id = id };
            var model = new UserViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(id))
                .Returns(new List<string>() { "Moderator" })
                .Verifiable();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(m => m.Map<UserViewModel>(user)).Returns(new UserViewModel { Id = id }).Verifiable();
            var mockedUsersService = new Mock<IUserService>();
            mockedUsersService.Setup(us => us.GetById(id)).Returns(user);

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act
            usersManagementController.UserProfile(userViewModel, id);

            // Assert
            mockedAuthenticationProvider.Verify(ap => ap.GetUserRoles(id), Times.Once);
        }

        [Test]
        public void ShouldSetRoleOfUserViewModel()
        {
            // Arrange
            var id = "13";
            var role = "Administrator";
            var userViewModel = new UserViewModel() { Id = id};
            var user = new ApplicationUser() { Id = id };
            var model = new UserViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(id))
                .Returns(new List<string>() { role });
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(m => m.Map<UserViewModel>(user)).Returns(userViewModel).Verifiable();
            var mockedUsersService = new Mock<IUserService>();
            mockedUsersService.Setup(us => us.GetById(id))
                .Returns(user);

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act & Assert
            usersManagementController.WithCallTo(umc => umc.UserProfile(model, id)).ShouldRenderDefaultView().WithModel<UserViewModel>(model1 =>

                 Assert.AreEqual(role, model1.Role)

             );
        }

        [Test]
        public void ShouldSetUserIdOfUserViewModel()
        {
            // Arrange
            var id = "13";
            var userViewModel = new UserViewModel() { Id = id};
            var user = new ApplicationUser() { Id = id };
            var model = new UserViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(id))
                .Returns(new List<string>() { "Moderator" });
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(m => m.Map<UserViewModel>(user)).Returns(userViewModel).Verifiable();
            var mockedUsersService = new Mock<IUserService>();
            mockedUsersService.Setup(us => us.GetById(id))
                .Returns(user);

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act & Assert
            usersManagementController.WithCallTo(umc => umc.UserProfile(model, id)).ShouldRenderDefaultView().WithModel<UserViewModel>(model1 =>

                 Assert.AreEqual(id, model1.Id)
 
             );
        }

        [Test]
        public void ShouldReturnViewWithUserViewModel()
        {
            // Arrange
            var id = "13";
            var userViewModel = new UserViewModel();
            var user = new ApplicationUser() { Id = id };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(id))
                .Returns(new List<string>() { "User" });
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUserService>();
            mockedMapperProvider.Setup(m => m.Map<UserViewModel>(user)).Returns(userViewModel).Verifiable();
            mockedUsersService.Setup(us => us.GetById(id))
                .Returns(user);

            var usersManagementController = new UserManagementController(
                mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act
            usersManagementController.WithCallTo(umc =>
                umc.UserProfile(userViewModel, id))
                .ShouldRenderDefaultView()
                .WithModel<UserViewModel>(model => Assert.AreEqual(userViewModel, model));
        }
    }
}

namespace TravelShare.Web.Controllers.Tests.UserManagementControllerTests
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.Areas.Administration.Controllers;
    using TravelShare.Web.Areas.Administration.Models;
    using TravelShare.Web.Mappings;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class SearchUsers
    {
        [Test]
        public void ShouldCallSearchUsersByUserNameMethodOfUsersService()
        {
            // Arrange
            var page = 1;
            var usersPerPage = 5;

            var searchModel = new SearchModel() { SearchWord = "Kon", SortBy = "name" };
            var usersViewModel = new UsersViewModel();

            var user = new ApplicationUser();
            var users = new List<ApplicationUser>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(us => us.SearchUsersByUsername(
                searchModel.SearchWord,
                searchModel.SortBy,
                page,
                usersPerPage))
                .Returns(users)
                .Verifiable();
            mockedUserService.Setup(us => us.UsersPageCountBySearchPattern(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(
                mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            mockedUserService.Verify(
                cs =>
                cs.SearchUsersByUsername(
                    searchModel.SearchWord,
                    searchModel.SortBy,
                    page,
                    usersPerPage), Times.Once);
        }

        [Test]
        public void ShouldCallUsersPageCountBySearchPatternMethodOfUsersService()
        {
            // Arrange
            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new ApplicationUser();
            var users = new List<ApplicationUser>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(us => us.SearchUsersByUsername(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.UsersPageCountBySearchPattern(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(users.Count)
                .Verifiable();

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, null);

            // Assert
            mockedUserService.Verify(
                cs =>
                cs.UsersPageCountBySearchPattern(searchModel.SearchWord, It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ShouldSetSearchModelOfUsersViewModel()
        {
            // Arrange
            var page = 1;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new ApplicationUser();
            var users = new List<ApplicationUser>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(us => us.SearchUsersByUsername(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.UsersPageCountBySearchPattern(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            Assert.AreSame(searchModel, usersViewModel.SearchModel);
        }

        [Test]
        public void ShouldSetUsersCountOfUsersViewModel()
        {
            // Arrange
            var page = 1;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new ApplicationUser();
            var users = new List<ApplicationUser>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(us => us.SearchUsersByUsername(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.UsersPageCountBySearchPattern(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            Assert.AreEqual(users.Count, usersViewModel.UsersCount);
        }

        [Test]
        public void ShouldSetPagesOfUsersViewModel()
        {
            // Arrange
            var usersPerPage = 10;
            var page = 1;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new ApplicationUser();
            var users = new List<ApplicationUser>();

            var expectedPages = (int)Math.Ceiling((double)users.Count / usersPerPage);

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(us => us.SearchUsersByUsername(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.UsersPageCountBySearchPattern(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            Assert.AreEqual(expectedPages, usersViewModel.Pages);
        }

        [Test]
        public void ShouldSetPageOfUsersViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new ApplicationUser();
            var users = new List<ApplicationUser>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(us => us.SearchUsersByUsername(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.UsersPageCountBySearchPattern(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(
                mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            Assert.AreEqual(page, usersViewModel.Page);
        }

        [Test]
        public void ShouldMapAndSetUsersOfUsersViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new ApplicationUser();
            var users = new List<ApplicationUser>() { user };

            var singleUsers = new List<SingleUserViewModel>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp =>
                mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()))
                .Returns(singleUsers);
            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(us => us.SearchUsersByUsername(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.UsersPageCountBySearchPattern(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            CollectionAssert.AreEquivalent(singleUsers, usersViewModel.Users);
        }

        [Test]
        public void ShouldRenderUsersPartialViewWithUsersViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new ApplicationUser();
            var users = new List<ApplicationUser>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(us => us.SearchUsersByUsername(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.UsersPageCountBySearchPattern(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act and Assert
            userManagementController.WithCallTo(umc => umc.SearchUsers(searchModel,
                usersViewModel,
                page))
                .ShouldRenderPartialView("UsersPartial")
                .WithModel<UsersViewModel>(model => Assert.AreEqual(usersViewModel, model));
        }
    }
}

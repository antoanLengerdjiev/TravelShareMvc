using System.Collections.Generic;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TravelShare.Data.Models;
using TravelShareMvc.IdentityConfig;
using TravelShareMvc.Providers.Contracts;

namespace TravelShareMvc.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly IHttpContextProvider httpContextProvider;

        public AuthenticationProvider(IHttpContextProvider httpContextProvider)
        {
            Guard.WhenArgument<IHttpContextProvider>(httpContextProvider, "Http context provider cannot be null.")
                .IsNull()
                .Throw();

            this.httpContextProvider = httpContextProvider;
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.IsAuthenticated;
            }
        }

        public string CurrentUserId
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.GetUserId();
            }
        }

        public IdentityResult CreateUser(ApplicationUser user, string password)
        {
            var manager = this.httpContextProvider.GetCurrentUserManager<ApplicationUserManager>();

            var result = manager.Create(user, password);

            if (result.Succeeded)
            {
                //manager.AddToRole(user.Id, "User");
            }

            return result;
        }

        public IList<string> GetUserRoles(string userId)
        {
            return this.httpContextProvider.GetCurrentUserManager<ApplicationUserManager>()
                .GetRoles(userId);
        }

        public void ChangeUserRole(string userId, string role)
        {
            var manager = this.httpContextProvider.GetCurrentUserManager<ApplicationUserManager>();

            manager.RemoveFromRoles(userId, "Administrator", "Moderator", "User");

            manager.AddToRole(userId, role);
        }

        public void UpdateSecurityStamp(string userId)
        {
            var manager = this.httpContextProvider.GetCurrentUserManager<ApplicationUserManager>();

            manager.UpdateSecurityStamp(userId);
        }

        public void SignIn(ApplicationUser user, bool isPersistent, bool rememberBrowser)
        {
            var manager = this.httpContextProvider.GetCurrentUserManager<ApplicationSignInManager>();

            manager.SignIn(user, isPersistent, rememberBrowser);
        }

        public SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout)
        {
            var manager = this.httpContextProvider.GetCurrentUserManager<ApplicationSignInManager>();

            return manager.PasswordSignIn(email, password, rememberMe, shouldLockout);
        }

        public void SignOut()
        {
            this.httpContextProvider.CurrentOwinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}

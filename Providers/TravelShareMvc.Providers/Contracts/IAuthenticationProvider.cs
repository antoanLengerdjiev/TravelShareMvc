using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TravelShare.Data.Models;

namespace TravelShareMvc.Providers.Contracts
{
    public interface IAuthenticationProvider
    {
        bool IsAuthenticated { get; }

        string CurrentUserId { get; }

        IdentityResult CreateUser(ApplicationUser user, string password);

        IList<string> GetUserRoles(string userId);

        void ChangeUserRole(string userId, string role);

        void UpdateSecurityStamp(string userId);

        void SignIn(ApplicationUser user, bool isPersistent, bool rememberBrowser);

        SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout);

        void SignOut();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Microsoft.Owin;

namespace TravelShareMvc.Providers.Contracts
{
    public interface IHttpContextProvider
    {
        HttpContext CurrentHttpContext { get; }

        IIdentity CurrentIdentity { get; }

        IOwinContext CurrentOwinContext { get; }

        Cache CurrentCache { get; }

        TManager GetCurrentUserManager<TManager>();

    }
}

using System;
using System.Web;
using System.Web.Mvc;
using Bytes2you.Validation;
using TravelShare.Web.Custom.Results;

namespace TravelShare.Web.Custom.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SecurityAttribute : AuthorizeAttribute
    {
        private string redirectUrl;

        public string RedirectUrl
        {
            get
            {
                return this.redirectUrl;
            }

            set
            {
                this.redirectUrl = value;
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            Guard.WhenArgument<AuthorizationContext>(filterContext, "Filter context cannot be null")
                .IsNull()
                .Throw();

            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                this.SetCachePolicy(filterContext);
            }
            else if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (this.RedirectUrl != null)
                {
                    filterContext.Result = new TransferResult(this.redirectUrl);
                }
                else
                {
                    this.HandleUnauthorizedRequest(filterContext);
                }
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }

        private void SetCachePolicy(AuthorizationContext filterContext)
        {
            // ** IMPORTANT **
            // Since we're performing authorization at the action level, the authorization code runs
            // after the output caching module. In the worst case this could allow an authorized user
            // to cause the page to be cached, then an unauthorized user would later be served the
            // cached page. We work around this by telling proxies not to cache the sensitive page,
            // then we hook our custom authorization code into the caching mechanism so that we have
            // the final say on whether a page should be served from the cache.
            HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            cachePolicy.AddValidationCallback(this.CacheValidateHandler, null);
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = this.OnCacheAuthorization(new HttpContextWrapper(context));
        }
    }
}
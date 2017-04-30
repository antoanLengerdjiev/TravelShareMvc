using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelShare.Web.Custom.Results
{
    public class TransferResult : ActionResult
    {

        public TransferResult(string url)
        {
            this.Url = url;
        }

        public string Url { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var httpContext = HttpContext.Current;

            // MVC 3 running on IIS 7+
            httpContext.Server.TransferRequest(this.Url, true);
        }
    }
}
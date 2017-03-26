
namespace TravelShare.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            this.Response.StatusCode = 404;
            return this.View();
        }

        public ActionResult Forbidden()
        {
            this.Response.StatusCode = 403;
            return this.View();
        }

        public ActionResult InternalServerError()
        {
            this.Response.StatusCode = 500;
            return this.View();
        }
    }
}
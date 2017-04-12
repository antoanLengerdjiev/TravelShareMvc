namespace TravelShare.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using Data.Common.Contracts;
    using Data.Models;
    using Microsoft.AspNet.Identity;

    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}

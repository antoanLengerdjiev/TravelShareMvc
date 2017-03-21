namespace TravelShare.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Common.Contracts;
    using Data.Models;
    using Microsoft.AspNet.Identity;

    public class HomeController : BaseController
    {
        private readonly IApplicationData data;

        public HomeController(IApplicationData data)
        {
            this.data = data;
        }

        public ActionResult Index()
        {
            var news = this.data.News.All();

            var data = this.data;
            return this.View();
        }
    }
}

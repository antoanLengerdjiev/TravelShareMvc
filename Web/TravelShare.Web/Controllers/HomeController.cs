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
        private readonly IApplicationData data;

        public HomeController(IApplicationData data)
        {
            Guard.WhenArgument<IApplicationData>(data, "Data provider cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }

        public ActionResult Index()
        {
            var news = this.data.News.All();

            return this.View();
        }
    }
}

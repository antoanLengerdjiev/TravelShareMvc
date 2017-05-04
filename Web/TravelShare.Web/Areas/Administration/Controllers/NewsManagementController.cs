namespace TravelShare.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using Data.Models;
    using Mappings;
    using Models.NewsManagement;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;

    public class NewsManagementController : Controller
    {
        private readonly INewsService newsService;
        private readonly ICachingProvider cacheProvider;
        private readonly IMapperProvider mapperProvider;

        public NewsManagementController(INewsService newsService, ICachingProvider cacheProvider, IMapperProvider mapperProvider)
        {
            Guard.WhenArgument<INewsService>(newsService, "News service cannot be null.").IsNull().Throw();
            Guard.WhenArgument<ICachingProvider>(cacheProvider, "Cache Provider cannot be null.").IsNull().Throw();
            Guard.WhenArgument<IMapperProvider>(mapperProvider, "Mapper Provider cannot be null.").IsNull().Throw();

            this.newsService = newsService;
            this.cacheProvider = cacheProvider;
            this.mapperProvider = mapperProvider;
        }

        // GET: Administration/NewsManagement
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult AddNews()
        {
            return this.View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddNews(NewCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            string newsKey = "newsKey";
            var news = this.mapperProvider.Map<News>(model);
            this.newsService.Create(news);
            this.cacheProvider.InsertItem(newsKey, news);

            return this.RedirectToAction("Index");
        }
    }
}
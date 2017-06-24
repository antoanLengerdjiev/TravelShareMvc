namespace TravelShare.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using Common;
    using Custom.Attributes;
    using Data.Models;
    using Mappings;
    using Models.NewsManagement;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;

    [Security(Roles = "Administrator, Moderator", RedirectUrl = "~/error/forbidden")]
    public class NewsManagementController : Controller
    {
        private readonly INewsService newsService;
        private readonly ICachingProvider cacheProvider;
        private readonly IMapperProvider mapperProvider;

        public NewsManagementController(INewsService newsService, ICachingProvider cacheProvider, IMapperProvider mapperProvider)
        {
            Guard.WhenArgument<INewsService>(newsService, GlobalConstants.NewsServiceNullExceptionMessage).IsNull().Throw();
            Guard.WhenArgument<ICachingProvider>(cacheProvider, GlobalConstants.CacheProviderNullExceptionMessage).IsNull().Throw();
            Guard.WhenArgument<IMapperProvider>(mapperProvider, GlobalConstants.MapperProviderNullExceptionMessage).IsNull().Throw();

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

            string newsKey = GlobalConstants.NewsCacheKey;
            var news = this.mapperProvider.Map<News>(model);
            this.newsService.Create(news);
            this.cacheProvider.InsertItem(newsKey, news);

            return this.RedirectToAction("Index");
        }

        public ActionResult DeleteNews()
        {
            return this.View();
        }

        public PartialViewResult SearchNews(SearchNewsModel searchModel, NewsViewModel newsModel, int? page)
        {
            int actualPage = page ?? 1;

            var result = this.newsService.SearchNews(searchModel.SearchWord, searchModel.SearchBy, actualPage, GlobalConstants.NewsPerTake);
            var count = this.newsService.GetSearchNewsPageCount(searchModel.SearchWord, searchModel.SearchBy, GlobalConstants.NewsPerTake);

            newsModel.SearchModel = searchModel;

            newsModel.Pages = count;
            newsModel.CurrentPage = actualPage;
            newsModel.News = this.mapperProvider.Map<IEnumerable<SingleNewsModel>>(result);
            newsModel.NewsCount = result.ToList().Count;
            return this.PartialView("NewsPartial", newsModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public PartialViewResult Delete(int id)
        {
            this.cacheProvider.RemoveItem(GlobalConstants.NewsCacheKey);
            var news = this.newsService.GetById(id);
            this.newsService.Delete(news);
            SingleNewsModel model = this.mapperProvider.Map<SingleNewsModel>(news);
            return this.PartialView("DeleteResult", model);
        }
    }
}
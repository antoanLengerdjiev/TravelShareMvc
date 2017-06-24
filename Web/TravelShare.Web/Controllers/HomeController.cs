namespace TravelShare.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using TravelShare.Common;
    using Bytes2you.Validation;
    using Data.Models;
    using Mappings;
    using Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly INewsService newsService;
        private readonly ICachingProvider cacheProvider;
        private readonly IMapperProvider mapperProvider;

        public HomeController(INewsService newsService, ICachingProvider cacheProvider, IMapperProvider mapperProvider)
        {
            Guard.WhenArgument<INewsService>(newsService, GlobalConstants.NewsServiceNullExceptionMessage).IsNull().Throw();
            Guard.WhenArgument<ICachingProvider>(cacheProvider, GlobalConstants.CacheProviderNullExceptionMessage).IsNull().Throw();
            Guard.WhenArgument<IMapperProvider>(mapperProvider, GlobalConstants.MapperProviderNullExceptionMessage).IsNull().Throw();

            this.newsService = newsService;
            this.cacheProvider = cacheProvider;
            this.mapperProvider = mapperProvider;
        }

        public ActionResult Index()
        {
            string newsKey = GlobalConstants.NewsCacheKey;
            var cachedNewsData = this.cacheProvider.GetItem(newsKey) as IEnumerable<News>;
            if (cachedNewsData == null || cachedNewsData.Count() == 0)
            {
                cachedNewsData = this.newsService.GetLastestNews(GlobalConstants.NewsPerTake);
                this.cacheProvider.AddItem(newsKey, cachedNewsData, DateTime.UtcNow.AddSeconds(10 * 60));
            }

            var model = this.mapperProvider.Map<IEnumerable<NewsViewModel>>(cachedNewsData);
            return this.View(model);
        }
    }
}

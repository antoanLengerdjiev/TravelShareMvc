namespace TravelShare.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Bytes2you.Validation;
    using Data.Common.Contracts;
    using Data.Models;
    using Mappings;
    using Microsoft.AspNet.Identity;
    using Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly INewsService newsService;
        private readonly ICachingProvider cacheProvider;
        private readonly IMapperProvider mapperProvider;

        public HomeController(INewsService newsService, ICachingProvider cacheProvider, IMapperProvider mapperProvider)
        {
            Guard.WhenArgument<INewsService>(newsService, "News service cannot be null.").IsNull().Throw();
            Guard.WhenArgument<ICachingProvider>(cacheProvider, "Cache Provider cannot be null.").IsNull().Throw();
            Guard.WhenArgument<IMapperProvider>(mapperProvider, "Mapper Provider cannot be null.").IsNull().Throw();

            this.newsService = newsService;
            this.cacheProvider = cacheProvider;
            this.mapperProvider = mapperProvider;
        }

        public ActionResult Index()
        {
            string newsKey = "newsKey";
            var cachedNewsData = this.cacheProvider.GetItem(newsKey) as IEnumerable<News>;
            if (cachedNewsData == null || cachedNewsData.Count() == 0)
            {
                cachedNewsData = this.newsService.GetLastestNews(5);
                this.cacheProvider.AddItem(newsKey, cachedNewsData, DateTime.UtcNow.AddSeconds(10 * 60));
            }

            var model = this.mapperProvider.Map<IEnumerable<NewsViewModel>>(cachedNewsData);
            return this.View(model);
        }
    }
}

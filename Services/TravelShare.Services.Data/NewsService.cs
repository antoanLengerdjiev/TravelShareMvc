namespace TravelShare.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bytes2you.Validation;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    public class NewsService : INewsService
    {
        private readonly IEfDbRepository<News> newsRepository;
        private readonly IApplicationDbContextSaveChanges dbSaveChanges;

        public NewsService(IEfDbRepository<News> newsRepository, IApplicationDbContextSaveChanges dbSaveChanges)
        {
            Guard.WhenArgument<IEfDbRepository<News>>(newsRepository, "News repostirory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IApplicationDbContextSaveChanges>(dbSaveChanges, "DbContext cannot be null.")
                .IsNull()
                .Throw();

            this.newsRepository = newsRepository;
            this.dbSaveChanges = dbSaveChanges;
        }

        public void Create(News news)
        {
            this.newsRepository.Add(news);
            this.dbSaveChanges.SaveChanges();
        }

        public IEnumerable<News> GetLastestNews(int numberOfNews)
        {
            var result = this.newsRepository.All().OrderByDescending(x => x.CreatedOn).Take(numberOfNews).ToList();
            return result;
        }

        public News GetById(int id)
        {
            return this.newsRepository.GetById(id);
        }

        public void Delete(News news)
        {
            this.newsRepository.Delete(news);
            this.dbSaveChanges.SaveChanges();
        }

        public IEnumerable<News> SearchNews(string searchPattern, string searchBy, int page, int perPage)
        {
            var skip = (page - 1) * perPage;
            var news = this.BuildSearchQuery(searchPattern, searchBy);

            var resultUsers = news
                .OrderByDescending(x => x.CreatedOn)
                  .Skip(skip)
                  .Take(perPage)
                  .ToList();

            return resultUsers;
        }

        public int GetSearchNewsPageCount(string searchPattern, string searchBy, int perPage)
        {
            var newsCount = this.BuildSearchQuery(searchPattern, searchBy).Count();
            if (newsCount < perPage)
            {
                return 0;
            }

            return (int)Math.Ceiling((double)newsCount / perPage);
        }

        private IQueryable<News> BuildSearchQuery(string searchWord, string searchBy)
        {
            var news = this.newsRepository.All();

            if (!string.IsNullOrEmpty(searchWord))
            {
                switch (searchBy)
                {
                    case "title":
                        news = news.Where(u => u.Title.ToLower().Contains(searchWord.ToLower()));
                        break;
                    case "content":
                        news = news.Where(x => x.Content.ToLower().Contains(searchWord.ToLower()));
                        break;
                    default:
                        news = news.Where(x => x.Title.ToLower().Contains(searchWord.ToLower()));
                        break;
                }
            }

            return news;
        }


    }
}

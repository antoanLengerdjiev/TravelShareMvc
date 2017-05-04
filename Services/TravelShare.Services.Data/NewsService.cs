namespace TravelShare.Services.Data
{
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
    }
}

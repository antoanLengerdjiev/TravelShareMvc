namespace TravelShare.Data.Common
{
    using System.Data.Entity;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    public class NewsRepository : DbRepository<News>, INewsRepository
    {
        public NewsRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}

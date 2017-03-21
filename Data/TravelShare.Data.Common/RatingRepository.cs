namespace TravelShare.Data.Common
{
    using System.Data.Entity;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    public class RatingRepository : DbRepository<Rating>, IRatingsRepository
    {
        public RatingRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}

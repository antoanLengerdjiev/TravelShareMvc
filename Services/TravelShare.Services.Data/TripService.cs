using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;
using TravelShare.Data.Common;
using TravelShare.Data.Models;
using TravelShare.Services.Data.Common.Contracts;

namespace TravelShare.Services.Data
{
    public class TripService : ITripService
    {
        private readonly IDbRepository<Trip> tripRepository;

        public TripService(IDbRepository<Trip> tripRepository)
        {
            Guard.WhenArgument<IDbRepository<Trip>>(tripRepository, "Trip repository cannot be null.")
                .IsNull()
                .Throw();

            this.tripRepository = tripRepository;
        }

        public int GetPagesCount(int number)
        {
            return (this.tripRepository.All().Count() / number) + 1;
        }

        public IQueryable<Trip> GetPagedTrips(int page, int number)
        {
            return this.tripRepository.All().OrderByDescending(x => x.Date).Skip(page * number).Take(number);
        }
    }
}

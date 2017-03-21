using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Common.Contracts;
using TravelShare.Data.Models;

namespace TravelShare.Data.Common
{
    public class TripRepository : DbRepository<Trip>, ITripRepository
    {
        public TripRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Models;

namespace TravelShare.Data.Common.Contracts
{
    public interface IApplicationDbContext : IDisposable
    {
       IDbSet<News> News { get; set; }

       IDbSet<Trip> Trips { get; set; }

       IDbSet<Rating> Ratings { get; set; }

       DbContext DbContext { get; }

       int SaveChanges();

        IDbSet<T> Set<T>()
            where T : class;
    }
}

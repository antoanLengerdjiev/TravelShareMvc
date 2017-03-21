using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Models;

namespace TravelShare.Data.Common.Contracts
{
    public interface IApplicationData : IDisposable
    {
        IDbRepository<ApplicationUser> Users { get; }

        IDbRepository<Trip> Trips { get; }

        IDbRepository<News> News { get; }

        IDbRepository<Rating> Rating { get; }


        int SaveChanges();
    }
}

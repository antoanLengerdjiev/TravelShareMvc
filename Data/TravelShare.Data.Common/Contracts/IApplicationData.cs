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
        IEfDbRepository<ApplicationUser> Users { get; }

        IEfDbRepository<Trip> Trips { get; }

        IEfDbRepository<News> News { get; }

        IEfDbRepository<Rating> Rating { get; }


        int SaveChanges();
    }
}

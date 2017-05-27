namespace TravelShare.Data.Common.Contracts
{
    using System;
    using System.Data.Entity;
    using TravelShare.Data.Models;

    public interface IApplicationDbContext : IDisposable
    {
       IDbSet<News> News { get; set; }

       IDbSet<Trip> Trips { get; set; }

       IDbSet<Message> Messages { get; set; }

        DbContext DbContext { get; }

        IDbSet<T> Set<T>()
            where T : class;
    }
}

namespace TravelShare.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Bytes2you.Validation;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Data.Models.Base;

    public class ApplicationData : IApplicationData
    {
        private readonly IApplicationDbContext context;
        private readonly IEfDbRepository<News> newsRepository;
        private readonly IEfDbRepository<ApplicationUser> userRepository;
        private readonly IEfDbRepository<Trip> tripRepository;
        private readonly IEfDbRepository<Rating> ratingsRepository;

        public ApplicationData(IApplicationDbContext context, IEfDbRepository<News> newsRepository, IEfDbRepository<ApplicationUser> userRepository, IEfDbRepository<Trip> tripRepository, IEfDbRepository<Rating> ratingsRepository)
        {
            Guard.WhenArgument<IApplicationDbContext>(context, "Database context cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfDbRepository<News>>(newsRepository, "News repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfDbRepository<ApplicationUser>>(userRepository, "User repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfDbRepository<Trip>>(tripRepository, "Trip repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfDbRepository<Rating>>(ratingsRepository, "Rating repository cannot be null.")
                .IsNull()
                .Throw();

            this.context = context;
            this.newsRepository = newsRepository;
            this.userRepository = userRepository;
            this.tripRepository = tripRepository;
            this.ratingsRepository = ratingsRepository;
        }

        public IApplicationDbContext Context
        {
            get
            {
               return this.context;
            }
        }

        public IEfDbRepository<News> News
        {
            get
            {
                return this.newsRepository;
            }
        }

        public IEfDbRepository<Rating> Rating
        {
            get
            {
                return this.ratingsRepository;
            }
        }

        public IEfDbRepository<Trip> Trips
        {
            get
            {
                return this.tripRepository;
            }
        }

        public IEfDbRepository<ApplicationUser> Users
        {
            get
            {
                return this.userRepository;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context?.Dispose();
            }
        }
    }
}

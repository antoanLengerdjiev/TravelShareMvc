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
        private readonly IDbRepository<News> newsRepository;
        private readonly IDbRepository<ApplicationUser> userRepository;
        private readonly IDbRepository<Trip> tripRepository;
        private readonly IDbRepository<Rating> ratingsRepository;

        public ApplicationData(IApplicationDbContext context, IDbRepository<News> newsRepository, IDbRepository<ApplicationUser> userRepository, IDbRepository<Trip> tripRepository, IDbRepository<Rating> ratingsRepository)
        {
            Guard.WhenArgument<IApplicationDbContext>(context, "Database context cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IDbRepository<News>>(newsRepository, "News repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IDbRepository<ApplicationUser>>(userRepository, "User repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IDbRepository<Trip>>(tripRepository, "Trip repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IDbRepository<Rating>>(ratingsRepository, "Rating repository cannot be null.")
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

        public IDbRepository<News> News
        {
            get
            {
                return this.newsRepository;
            }
        }

        public IDbRepository<Rating> Rating
        {
            get
            {
                return this.ratingsRepository;
            }
        }

        public IDbRepository<Trip> Trips
        {
            get
            {
                return this.tripRepository;
            }
        }

        public IDbRepository<ApplicationUser> Users
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

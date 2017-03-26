namespace TravelShare.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Common.Contracts;
    using Common.Models;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Base;
    using TravelShare.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<News> News { get; set; }

        public IDbSet<Trip> Trips { get; set; }

        public IDbSet<Rating> Ratings { get; set; }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }

        IDbSet<T> IApplicationDbContext.Set<T>()
        {
            return base.Set<T>();
        }

    }
}

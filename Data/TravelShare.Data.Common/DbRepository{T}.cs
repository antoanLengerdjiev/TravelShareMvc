namespace TravelShare.Data.Common
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Bytes2you.Validation;
    using Contracts;
    using Data.Models.Base;
    using TravelShare.Data.Common.Models;

    public class DbRepository<T> : IDbRepository<T>
        where T : class, IAuditInfo, IDeletableEntity
    {
        public DbRepository(IApplicationDbContext context)
        {
            Guard.WhenArgument<IApplicationDbContext>(context, "Database context cannot be null.")
                 .IsNull()
                 .Throw();

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private IDbSet<T> DbSet { get; }

        private IApplicationDbContext Context { get; }

        public IQueryable<T> All()
        {
            return this.DbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.DbSet;
        }

        public T GetById(object id)
        {
            var item = this.DbSet.Find(id);
            if (item.IsDeleted)
            {
                return null;
            }

            return item;
        }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
        }

        public void HardDelete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}

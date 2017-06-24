namespace TravelShare.Data.Common
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Bytes2you.Validation;
    using Contracts;
    using Data.Models.Base;
    using TravelShare.Common;
    using TravelShare.Data.Common.Models;

    public class EfDbRepository<T> : IEfDbRepository<T>
        where T : class, IAuditInfo, IDeletableEntity
    {

        private const string EntityToAddNullExceptionMessage = "Cannot Add null object.";
        private const string EntityToDeleteNullExceptionMessage = "Cannot Delete null object.";
        private const string EntityToHardDeleteNullExceptionMessage = "Cannot Hard Delete null object.";

        public EfDbRepository(IApplicationDbContext context)
        {
            Guard.WhenArgument<IApplicationDbContext>(context, GlobalConstants.DbContextNullExceptionMessage)
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
            if (id == null)
            {
                return null;
            }

            var item = this.DbSet.Find(id);
            if (item.IsDeleted)
            {
                return null;
            }

            return item;
        }

        public void Add(T entity)
        {
            Guard.WhenArgument<T>(entity, EntityToAddNullExceptionMessage).IsNull().Throw();

            this.DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            Guard.WhenArgument<T>(entity, EntityToDeleteNullExceptionMessage).IsNull().Throw();
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
        }

        public void HardDelete(T entity)
        {
            Guard.WhenArgument<T>(entity, EntityToHardDeleteNullExceptionMessage).IsNull().Throw();
            this.DbSet.Remove(entity);
        }
    }
}

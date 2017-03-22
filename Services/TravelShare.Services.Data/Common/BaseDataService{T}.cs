namespace TravelShare.Services.Data
{
    using System;
    using System.Linq;
    using Common.Contracts;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Models;
    using TravelShare.Data.Models.Base;

    public abstract class BaseDataService<T> : IBaseDataService<T>
        where T : class, IDeletableEntity, IAuditInfo
    {
        public BaseDataService(IDbRepository<T> dataSet)
        {
            this.Data = dataSet;
        }

        protected IDbRepository<T> Data { get; set; }

        public virtual void Add(T item)
        {
            this.Data.Add(item);
        }

        public virtual void Delete(object id)
        {
            var entity = this.Data.GetById(id);
            if (entity == null)
            {
                throw new InvalidOperationException("No entity with provided id found.");
            }

            this.Data.Delete(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.Data.All();
        }

        public virtual T GetById(object id)
        {
            return this.Data.GetById(id);
        }



        public void Dispose()
        {
            this.Data.Dispose();
        }
    }
}

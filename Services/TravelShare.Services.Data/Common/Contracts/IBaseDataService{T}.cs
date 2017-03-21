namespace TravelShare.Services.Data.Common.Contracts
{
    using System.Linq;
    using TravelShare.Data.Common.Models;
    using TravelShare.Data.Models.Base;

    public interface IBaseDataService<T>
        where T : class, IDeletableEntity, IAuditInfo
    {
        void Add(T item);

        void Delete(object id);

        IQueryable<T> GetAll();

        T GetById(object id);

        void Dispose();
    }
}
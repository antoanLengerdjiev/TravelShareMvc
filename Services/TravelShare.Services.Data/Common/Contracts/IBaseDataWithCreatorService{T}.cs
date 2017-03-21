namespace TravelShare.Services.Data.Common.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TravelShare.Data.Common.Models;
    using TravelShare.Data.Models.Base;

    public interface IBaseDataWithCreatorService<T> : IBaseDataService<T>
        where T : class, IDeletableEntity, IAuditInfo, IEntityWithCreator
    {
        void Delete(object id, string userId);

        IQueryable<T> GetAllByUser(string userId);
    }
}

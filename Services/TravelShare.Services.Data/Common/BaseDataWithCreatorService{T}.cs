namespace TravelShare.Services.Data.Common
{
    using System;
    using System.Linq;
    using Contracts;
    using TravelShare.Common;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Models;
    using TravelShare.Data.Models;
    using TravelShare.Data.Models.Base;

    public class BaseDataWithCreatorService<T> : BaseDataService<T>, IBaseDataWithCreatorService<T>
        where T : class, IDeletableEntity, IAuditInfo, IEntityWithCreator
    {
        public BaseDataWithCreatorService(IEfDbRepository<T> dataSet, IEfDbRepository<ApplicationUser> users)
            : base(dataSet)
        {
            this.Users = users;
        }

        protected IEfDbRepository<ApplicationUser> Users { get; set; }

        public IQueryable<T> GetAllByUser(string userId)
        {
            return this.Data
                .All()
                .Where(x => x.UserId == userId);
        }

        public void Delete(object id, string userId)
        {
            var user = this.Users.GetById(userId);
            var isAdmin = user.Roles.Any(x => x.RoleId == GlobalConstants.AdministratorRoleName);
            var training = this.Data.GetById(id);
            if (training == null)
            {
                throw new InvalidOperationException($"No entity with provided id ({id}) found.");
            }

            if (training.UserId != userId && !isAdmin)
            {
                throw new InvalidOperationException("Cannot delete entity. Unauthorized request.");
            }

            this.Data.Delete(training);
        }
    }
}

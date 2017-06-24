namespace TravelShare.Services.Data
{
    using System.Linq;
    using Bytes2you.Validation;
    using TravelShare.Common;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    public class CityService : ICityService
    {
        private const string CityNameNullExceptionMessage = "city name cannot be null";
        private readonly IEfDbRepository<City> cityRepository;

        private readonly IApplicationDbContextSaveChanges dbSaveChanges;

        public CityService(IEfDbRepository<City> cityRepository, IApplicationDbContextSaveChanges dbSaveChanges)
        {
            Guard.WhenArgument(cityRepository, GlobalConstants.CityRepositoryNullExceptionMessage).IsNull().Throw();
            Guard.WhenArgument<IApplicationDbContextSaveChanges>(dbSaveChanges, GlobalConstants.DbContextSaveChangesNullExceptionMessage)
               .IsNull()
               .Throw();
            this.cityRepository = cityRepository;
            this.dbSaveChanges = dbSaveChanges;
        }

        public City Create(string name)
        {
            Guard.WhenArgument(name, CityNameNullExceptionMessage).IsNullOrEmpty().Throw();

            // TODO : CityFactory
            var city = new City { Name = name };
            this.cityRepository.Add(city);
            this.dbSaveChanges.SaveChanges();
            return city;
        }

        public City GetCityByName(string name)
        {
            Guard.WhenArgument(name, CityNameNullExceptionMessage).IsNullOrEmpty().Throw();
            return this.cityRepository.All().Where(x => x.Name == name).FirstOrDefault();
        }
    }
}

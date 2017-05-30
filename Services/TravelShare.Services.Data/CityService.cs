namespace TravelShare.Services.Data
{
    using System.Linq;
    using Bytes2you.Validation;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    public class CityService : ICityService
    {
        private readonly IEfDbRepository<City> cityRepository;

        private readonly IApplicationDbContextSaveChanges dbSaveChanges;

        public CityService(IEfDbRepository<City> cityRepository, IApplicationDbContextSaveChanges dbSaveChanges)
        {
            Guard.WhenArgument(cityRepository, "City repository cannot be null.").IsNull().Throw();
            Guard.WhenArgument<IApplicationDbContextSaveChanges>(dbSaveChanges, "DbContext cannot be null.")
               .IsNull()
               .Throw();
            this.cityRepository = cityRepository;
            this.dbSaveChanges = dbSaveChanges;
        }

        public City Create(string name)
        {
            Guard.WhenArgument(name, "city name cannot be null").IsNullOrEmpty().Throw();

            // TODO : CityFactory
            var city = new City { Name = name };
            this.cityRepository.Add(city);
            this.dbSaveChanges.SaveChanges();
            return city;
        }

        public City GetCityByName(string name)
        {
            Guard.WhenArgument(name, "city name cannot be null").IsNullOrEmpty().Throw();
            return this.cityRepository.All().Where(x => x.Name == name).FirstOrDefault();
        }
    }
}

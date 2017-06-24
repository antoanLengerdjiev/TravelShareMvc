namespace TravelShare.Common
{
    public class GlobalConstants
    {
        public const string UserIdNullExceptionMessage = "User Id cannot be null";

        public const string DbContextNullExceptionMessage = "Database context cannot be null.";
        public const string DbContextSaveChangesNullExceptionMessage = "DbContext cannot be null.";
        public const string TripRepositoryNullExceptionMessage = "Trip repository cannot be null.";
        public const string UserRepositoryNullExceptionMessage = "User repository cannot be null.";
        public const string CityRepositoryNullExceptionMessage = "City repository cannot be null.";
        public const string NewsRepositoryNullExceptionMessage = "News repository cannot be null.";
        public const string MessageRepositoryNullExceptionMessage = "Message repository cannot be null.";
        public const string ChatRepositoryNullExceptionMessage = "Chat repository cannot be null.";
        public const string CacheProviderNullExceptionMessage = "Cache Provider cannot be null.";

        public const string CityServiceNullExceptionMessage = "City service cannot be null.";
        public const string NewsServiceNullExceptionMessage = "News service cannot be null.";
        public const string ChatServiceNullExceptionMessage = "Chat Service cannot be null.";
        public const string TripServiceNullExceptionMessage = "Trip Service cannot be null.";
        public const string UserServiceNullExceptionMessage = "User Service cannot be null.";
        public const string AuthenticationProviderNullExceptionMessage = "Authentication provider cannot be null.";
        public const string MapperProviderNullExceptionMessage = "Mapper provider cannot be null.";

        public const string NewsCacheKey = "NewsKey";
        public const string AdministratorRoleName = "Administrator";

        public const int PasswordMinLength = 1;
        public const int Zero = 0;
        public const int MessagePerTake = 5;
        public const int TripsPerTake = 5;
        public const int NewsPerTake = 5;
        public const int UsersPerTake = 5;

    }
}

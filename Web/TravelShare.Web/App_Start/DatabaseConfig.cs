namespace TravelShare.Web
{
    using System.Data.Entity;
    using Data;
    using Data;


    public static class DatabaseConfig
    {
        public static void Config()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
    }
}
